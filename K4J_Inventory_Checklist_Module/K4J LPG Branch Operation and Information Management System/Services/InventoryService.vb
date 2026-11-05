Imports System.Data
Imports MySql.Data.MySqlClient

' ==========================================================
' SERVICE LAYER - INVENTORY
' Business rules, validation and role-based access control.
'   New Balance = Previous Balance + Quantity In - Quantity Out
' Every stock change runs in ONE database transaction that
' (1) locks the balance row, (2) updates the balance, and
' (3) writes the movement. Either all three happen or none.
' ==========================================================

Public Module InventoryService

    Private Const MaxQuantity As Integer = 1000000

    ' ---------------------------------------------------------
    ' READ OPERATIONS
    ' ---------------------------------------------------------

    Public Function GetProducts() As List(Of ProductInfo)
        Authorization.RequireSignedIn()
        Return Db.ReadOnlyWork(Of List(Of ProductInfo))(
            Function(conn) InventoryRepository.GetActiveProducts(conn))
    End Function

    Public Function GetBranches() As DataTable
        Authorization.RequireSuperAdmin()
        Return Db.ReadOnlyWork(Of DataTable)(
            Function(conn) InventoryRepository.GetBranches(conn))
    End Function

    ''' <summary>
    ''' Balances with one row per branch and product (Filled / Empty / Revolving / Damaged / General
    ''' side by side). Branches are never merged here. Branch Admin always gets their own branch.
    ''' </summary>
    Public Function GetBalancePivot(branchId As Integer?) As DataTable
        Dim user As SessionUser = Authorization.RequireSignedIn()
        Dim effective As Integer? = Authorization.ResolveBranch(user, branchId)

        Dim raw As DataTable = Db.InTransaction(Of DataTable)(
            Function(conn, tx)
                If user.IsBranchAdmin Then
                    InventoryRepository.EnsureRowsForBranch(conn, tx, user.BranchID.Value)
                End If
                Return InventoryRepository.GetBalances(conn, tx, effective)
            End Function)
        Return BuildPivot(raw)
    End Function

    ''' <summary>Super Admin comparison: one row per product and condition, one column per branch plus a combined total.</summary>
    Public Function GetComparison() As DataTable
        Authorization.RequireSuperAdmin()

        Dim raw As DataTable = Nothing
        Dim branches As DataTable = Nothing
        Db.ReadOnlyWork(Of Integer)(
            Function(conn)
                raw = InventoryRepository.GetBalances(conn, Nothing, Nothing)
                branches = InventoryRepository.GetBranches(conn)
                Return 0
            End Function)

        Dim result As New DataTable()
        result.Columns.Add("Product", GetType(String))
        result.Columns.Add("Condition", GetType(String))
        For Each b As DataRow In branches.Rows
            result.Columns.Add(Convert.ToString(b("BranchName")), GetType(Integer))
        Next
        result.Columns.Add("Both branches combined", GetType(Integer))

        Dim index As New Dictionary(Of String, DataRow)()
        For Each r As DataRow In raw.Rows
            Dim key As String = Convert.ToString(r("ProductID")) & "|" & Convert.ToString(r("StockCondition"))
            Dim row As DataRow = Nothing
            If Not index.TryGetValue(key, row) Then
                row = result.NewRow()
                row("Product") = Convert.ToString(r("ProductName"))
                row("Condition") = StockConditions.DisplayName(Convert.ToString(r("StockCondition")))
                For Each b As DataRow In branches.Rows
                    row(Convert.ToString(b("BranchName"))) = 0
                Next
                row("Both branches combined") = 0
                result.Rows.Add(row)
                index(key) = row
            End If
            Dim qty As Integer = Convert.ToInt32(r("Quantity"))
            Dim branchColumn As String = Convert.ToString(r("BranchName"))
            If result.Columns.Contains(branchColumn) Then
                row(branchColumn) = Convert.ToInt32(row(branchColumn)) + qty
                row("Both branches combined") = Convert.ToInt32(row("Both branches combined")) + qty
            End If
        Next
        Return result
    End Function

    Public Function GetMovements(branchId As Integer?, movementType As String,
                                 fromDate As Date?, toDate As Date?) As DataTable
        Dim user As SessionUser = Authorization.RequireSignedIn()
        Dim effective As Integer? = Authorization.ResolveBranch(user, branchId)
        Dim typeFilter As String = If(String.IsNullOrWhiteSpace(movementType), Nothing, movementType)
        Return Db.ReadOnlyWork(Of DataTable)(
            Function(conn) InventoryRepository.GetMovements(conn, effective, typeFilter, fromDate, toDate))
    End Function

    ''' <summary>Current balance of one item in the Branch Admin's own branch (0 if there is no row yet).</summary>
    Public Function GetCurrentBalance(productId As Integer, condition As String) As Integer
        Dim user As SessionUser = Authorization.RequireBranchAdmin()
        Return Db.ReadOnlyWork(Of Integer)(
            Function(conn)
                Dim dt As DataTable = Db.QueryTable(conn, Nothing,
                    "SELECT Quantity FROM Inventory WHERE BranchID = @b AND ProductID = @p AND StockCondition = @c",
                    Db.Param("@b", user.BranchID.Value), Db.Param("@p", productId), Db.Param("@c", condition))
                If dt.Rows.Count = 0 Then Return 0
                Return Convert.ToInt32(dt.Rows(0)("Quantity"))
            End Function)
    End Function

    ' ---------------------------------------------------------
    ' WRITE OPERATIONS (Branch Admin, own branch only)
    ' The branch is never passed in: it always comes from the signed-in user.
    ' ---------------------------------------------------------

    Public Sub RecordIncoming(productId As Integer, condition As String, quantity As Integer, reason As String)
        Dim user As SessionUser = Authorization.RequireBranchAdmin()
        ValidateQuantity(quantity)
        ValidateReason(reason)
        Db.InTransaction(Of Integer)(
            Function(conn, tx)
                ApplyMovement(conn, tx, user, productId, condition, MovementTypes.Incoming,
                              quantity, 0, reason.Trim(), Nothing)
                Return 0
            End Function)
    End Sub

    Public Sub RecordOutgoing(productId As Integer, condition As String, quantity As Integer, reason As String)
        Dim user As SessionUser = Authorization.RequireBranchAdmin()
        ValidateQuantity(quantity)
        ValidateReason(reason)
        Db.InTransaction(Of Integer)(
            Function(conn, tx)
                ApplyMovement(conn, tx, user, productId, condition, MovementTypes.Outgoing,
                              0, quantity, reason.Trim(), Nothing)
                Return 0
            End Function)
    End Sub

    ''' <summary>Authorized adjustment: set the balance to a new value. The reason and the user are recorded.</summary>
    Public Sub RecordAdjustment(productId As Integer, condition As String, newBalance As Integer, reason As String)
        Dim user As SessionUser = Authorization.RequireBranchAdmin()
        If newBalance < 0 OrElse newBalance > MaxQuantity Then
            Throw New BusinessRuleException("The new balance must be between 0 and " & MaxQuantity.ToString("N0") & ".")
        End If
        ValidateReason(reason)

        Db.InTransaction(Of Integer)(
            Function(conn, tx)
                InventoryRepository.EnsureRowsForBranch(conn, tx, user.BranchID.Value)
                Dim current As InventoryBalance = InventoryRepository.LockBalance(
                    conn, tx, user.BranchID.Value, productId, condition)
                If current Is Nothing Then Throw New BusinessRuleException("That item does not exist in your branch.")

                Dim delta As Integer = newBalance - current.Quantity
                If delta = 0 Then Throw New BusinessRuleException("The new balance is the same as the current balance.")

                ApplyMovement(conn, tx, user, productId, condition, MovementTypes.Adjustment,
                              If(delta > 0, delta, 0), If(delta < 0, -delta, 0), reason.Trim(), Nothing)
                Return 0
            End Function)
    End Sub

    ''' <summary>
    ''' Resolves a checklist discrepancy. Applies the line's Difference (Physical - System) to the
    ''' current balance as an ADJUSTMENT linked to that checklist line. A line can be adjusted once.
    ''' </summary>
    Public Sub AdjustFromChecklistLine(detailId As Integer, reason As String)
        Dim user As SessionUser = Authorization.RequireBranchAdmin()
        ValidateReason(reason)

        Db.InTransaction(Of Integer)(
            Function(conn, tx)
                Dim target As AdjustmentTarget = ChecklistRepository.LockLineForAdjustment(conn, tx, detailId)
                If target Is Nothing Then Throw New BusinessRuleException("That checklist line was not found.")
                If target.BranchID <> user.BranchID.Value Then
                    Throw New UnauthorizedAccessException("You can only adjust stock of your own branch.")
                End If
                If target.ChecklistStatus <> ChecklistStatuses.Submitted Then
                    Throw New BusinessRuleException("Submit the checklist before adjusting stock.")
                End If
                If Not target.Difference.HasValue OrElse target.Difference.Value = 0 Then
                    Throw New BusinessRuleException("This line has no difference to adjust.")
                End If
                If ChecklistRepository.CountAdjustments(conn, tx, detailId) > 0 Then
                    Throw New BusinessRuleException("This discrepancy was already adjusted.")
                End If

                Dim diff As Integer = target.Difference.Value
                ApplyMovement(conn, tx, user, target.ProductID, target.StockCondition, MovementTypes.Adjustment,
                              If(diff > 0, diff, 0), If(diff < 0, -diff, 0),
                              "Checklist adjustment: " & reason.Trim(), detailId)
                Return 0
            End Function)
    End Sub

    ' ---------------------------------------------------------
    ' CORE: the only place a balance changes
    ' ---------------------------------------------------------

    Private Sub ApplyMovement(conn As MySqlConnection, tx As MySqlTransaction, user As SessionUser,
                              productId As Integer, condition As String, movementType As String,
                              qtyIn As Integer, qtyOut As Integer, description As String,
                              checklistDetailId As Integer?)

        Dim branchId As Integer = user.BranchID.Value

        Dim category As String = InventoryRepository.GetProductCategory(conn, tx, productId)
        If category Is Nothing Then Throw New BusinessRuleException("That product is not available.")
        If Array.IndexOf(StockConditions.ForCategory(category), condition) < 0 Then
            Throw New BusinessRuleException("That stock condition does not apply to this product.")
        End If

        InventoryRepository.EnsureRowsForBranch(conn, tx, branchId)
        Dim current As InventoryBalance = InventoryRepository.LockBalance(conn, tx, branchId, productId, condition)
        If current Is Nothing Then Throw New BusinessRuleException("That item does not exist in your branch.")

        Dim previousBalance As Integer = current.Quantity
        Dim newBalance As Integer = previousBalance + qtyIn - qtyOut     ' the system formula
        If newBalance < 0 Then
            Throw New BusinessRuleException("Not enough stock. The current balance is " & previousBalance.ToString("N0") & ".")
        End If

        InventoryRepository.SetQuantity(conn, tx, current.InventoryID, newBalance)
        InventoryRepository.InsertMovement(conn, tx, New MovementRecord With {
            .BranchID = branchId,
            .ProductID = productId,
            .StockCondition = condition,
            .MovementType = movementType,
            .QuantityIn = qtyIn,
            .QuantityOut = qtyOut,
            .PreviousBalance = previousBalance,
            .NewBalance = newBalance,
            .Description = description,
            .RecordedBy = user.UserID,
            .ChecklistDetailID = checklistDetailId
        })
    End Sub

    ' ---------------------------------------------------------
    ' VALIDATION AND HELPERS
    ' ---------------------------------------------------------

    Private Sub ValidateQuantity(quantity As Integer)
        If quantity <= 0 Then Throw New BusinessRuleException("Enter a quantity greater than zero.")
        If quantity > MaxQuantity Then Throw New BusinessRuleException("That quantity is too large.")
    End Sub

    Private Sub ValidateReason(reason As String)
        If String.IsNullOrWhiteSpace(reason) Then Throw New BusinessRuleException("Enter a reason or description.")
        If reason.Trim().Length > 200 Then Throw New BusinessRuleException("Keep the reason under 200 characters.")
    End Sub

    Private Function BuildPivot(raw As DataTable) As DataTable
        Dim result As New DataTable()
        result.Columns.Add("BranchName", GetType(String))
        result.Columns.Add("ProductID", GetType(Integer))
        result.Columns.Add("Product", GetType(String))
        result.Columns.Add("Filled", GetType(Integer))
        result.Columns.Add("Empty", GetType(Integer))
        result.Columns.Add("Revolving", GetType(Integer))
        result.Columns.Add("Damaged", GetType(Integer))
        result.Columns.Add("General", GetType(Integer))
        result.Columns.Add("TotalCylinders", GetType(Integer))

        Dim index As New Dictionary(Of String, DataRow)()
        For Each r As DataRow In raw.Rows
            Dim key As String = Convert.ToString(r("BranchID")) & "|" & Convert.ToString(r("ProductID"))
            Dim row As DataRow = Nothing
            If Not index.TryGetValue(key, row) Then
                row = result.NewRow()
                row("BranchName") = Convert.ToString(r("BranchName"))
                row("ProductID") = Convert.ToInt32(r("ProductID"))
                row("Product") = Convert.ToString(r("ProductName"))
                result.Rows.Add(row)
                index(key) = row
            End If

            Dim qty As Integer = Convert.ToInt32(r("Quantity"))
            Select Case Convert.ToString(r("StockCondition"))
                Case StockConditions.Filled
                    row("Filled") = qty
                Case StockConditions.EmptyCylinder
                    row("Empty") = qty
                Case StockConditions.Revolving
                    row("Revolving") = qty
                Case StockConditions.Damaged
                    row("Damaged") = qty
                Case StockConditions.General
                    row("General") = qty
            End Select
        Next

        ' Not-applicable cells stay empty (DBNull); the total only exists for cylinders.
        For Each row As DataRow In result.Rows
            If Not (row.IsNull("Filled") AndAlso row.IsNull("Empty") AndAlso
                    row.IsNull("Revolving") AndAlso row.IsNull("Damaged")) Then
                Dim total As Integer = 0
                For Each c As String In New String() {"Filled", "Empty", "Revolving", "Damaged"}
                    If Not row.IsNull(c) Then total += Convert.ToInt32(row(c))
                Next
                row("TotalCylinders") = total
            End If
        Next
        Return result
    End Function

End Module

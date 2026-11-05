Imports System.Data
Imports MySql.Data.MySqlClient

' ==========================================================
' DATA ACCESS LAYER - INVENTORY AND MOVEMENTS
' Only SQL lives here. Authorization and business rules are in
' InventoryService. Methods take an open connection and an optional
' transaction so the service layer controls the transaction.
' ==========================================================

Public Module InventoryRepository

    Private Const ConditionOrder As String =
        "FIELD(i.StockCondition, 'FILLED', 'EMPTY', 'REVOLVING', 'DAMAGED', 'GENERAL')"

    Public Function GetActiveProducts(conn As MySqlConnection) As List(Of ProductInfo)
        Dim dt As DataTable = Db.QueryTable(conn, Nothing,
            "SELECT ProductID, ProductCode, ProductName, Category " &
            "FROM Product WHERE IsActive = 1 ORDER BY Category, ProductName")
        Dim list As New List(Of ProductInfo)()
        For Each r As DataRow In dt.Rows
            list.Add(New ProductInfo With {
                .ProductID = Convert.ToInt32(r("ProductID")),
                .ProductCode = Db.Text(r, "ProductCode"),
                .ProductName = Db.Text(r, "ProductName"),
                .Category = Db.Text(r, "Category")
            })
        Next
        Return list
    End Function

    Public Function GetBranches(conn As MySqlConnection) As DataTable
        Return Db.QueryTable(conn, Nothing,
            "SELECT BranchID, BranchName FROM Branch WHERE IsActive = 1 ORDER BY BranchName")
    End Function

    Public Function GetProductCategory(conn As MySqlConnection, tx As MySqlTransaction,
                                       productId As Integer) As String
        Dim result As Object = Db.Scalar(conn, tx,
            "SELECT Category FROM Product WHERE ProductID = @p AND IsActive = 1",
            Db.Param("@p", productId))
        If result Is Nothing Then Return Nothing
        Return Convert.ToString(result)
    End Function

    ''' <summary>Creates any missing balance rows (quantity 0) for every active product in the branch.</summary>
    Public Sub EnsureRowsForBranch(conn As MySqlConnection, tx As MySqlTransaction, branchId As Integer)
        Db.Execute(conn, tx,
            "INSERT IGNORE INTO Inventory (BranchID, ProductID, StockCondition, Quantity) " &
            "SELECT @branch, p.ProductID, c.Cond, 0 " &
            "FROM Product p " &
            "JOIN (SELECT 'FILLED' AS Cond UNION ALL SELECT 'EMPTY' UNION ALL SELECT 'REVOLVING' " &
            "      UNION ALL SELECT 'DAMAGED' UNION ALL SELECT 'GENERAL') c " &
            "  ON (p.Category = 'LPG_CYLINDER' AND c.Cond <> 'GENERAL') " &
            "  OR (p.Category = 'ACCESSORY' AND c.Cond = 'GENERAL') " &
            "WHERE p.IsActive = 1",
            Db.Param("@branch", branchId))
    End Sub

    ''' <summary>One row per branch, product and stock condition. branchId Nothing = every branch.</summary>
    Public Function GetBalances(conn As MySqlConnection, tx As MySqlTransaction,
                                branchId As Integer?) As DataTable
        Return Db.QueryTable(conn, tx,
            "SELECT i.InventoryID, i.BranchID, b.BranchName, p.ProductID, p.ProductCode, " &
            "       p.ProductName, p.Category, i.StockCondition, i.Quantity " &
            "FROM Inventory i " &
            "JOIN Branch b ON b.BranchID = i.BranchID " &
            "JOIN Product p ON p.ProductID = i.ProductID " &
            "WHERE p.IsActive = 1 AND (@branch IS NULL OR i.BranchID = @branch) " &
            "ORDER BY b.BranchName, p.Category, p.ProductName, " & ConditionOrder,
            Db.Param("@branch", branchId))
    End Function

    ''' <summary>Reads one balance and locks the row until the transaction ends.</summary>
    Public Function LockBalance(conn As MySqlConnection, tx As MySqlTransaction,
                                branchId As Integer, productId As Integer,
                                condition As String) As InventoryBalance
        Dim dt As DataTable = Db.QueryTable(conn, tx,
            "SELECT InventoryID, Quantity FROM Inventory " &
            "WHERE BranchID = @b AND ProductID = @p AND StockCondition = @c FOR UPDATE",
            Db.Param("@b", branchId), Db.Param("@p", productId), Db.Param("@c", condition))
        If dt.Rows.Count = 0 Then Return Nothing
        Return New InventoryBalance With {
            .InventoryID = Convert.ToInt32(dt.Rows(0)("InventoryID")),
            .Quantity = Convert.ToInt32(dt.Rows(0)("Quantity"))
        }
    End Function

    ''' <summary>Locks every balance row of a branch (used while a checklist is submitted).</summary>
    Public Sub LockBranchInventory(conn As MySqlConnection, tx As MySqlTransaction, branchId As Integer)
        Db.QueryTable(conn, tx,
            "SELECT InventoryID FROM Inventory WHERE BranchID = @b FOR UPDATE",
            Db.Param("@b", branchId))
    End Sub

    Public Sub SetQuantity(conn As MySqlConnection, tx As MySqlTransaction,
                           inventoryId As Integer, newQuantity As Integer)
        Db.Execute(conn, tx,
            "UPDATE Inventory SET Quantity = @q WHERE InventoryID = @id",
            Db.Param("@q", newQuantity), Db.Param("@id", inventoryId))
    End Sub

    Public Sub InsertMovement(conn As MySqlConnection, tx As MySqlTransaction, m As MovementRecord)
        Db.Execute(conn, tx,
            "INSERT INTO InventoryMovement " &
            "(BranchID, ProductID, StockCondition, MovementType, QuantityIn, QuantityOut, " &
            " PreviousBalance, NewBalance, Description, RecordedBy, ChecklistDetailID) " &
            "VALUES (@branch, @product, @cond, @type, @qin, @qout, @prev, @new, @desc, @user, @detail)",
            Db.Param("@branch", m.BranchID),
            Db.Param("@product", m.ProductID),
            Db.Param("@cond", m.StockCondition),
            Db.Param("@type", m.MovementType),
            Db.Param("@qin", m.QuantityIn),
            Db.Param("@qout", m.QuantityOut),
            Db.Param("@prev", m.PreviousBalance),
            Db.Param("@new", m.NewBalance),
            Db.Param("@desc", m.Description),
            Db.Param("@user", m.RecordedBy),
            Db.Param("@detail", m.ChecklistDetailID))
    End Sub

    Public Function GetMovements(conn As MySqlConnection, branchId As Integer?, movementType As String,
                                 fromDate As Date?, toDate As Date?) As DataTable
        Return Db.QueryTable(conn, Nothing,
            "SELECT m.MovementID, b.BranchName, p.ProductName, m.StockCondition, m.MovementType, " &
            "       m.QuantityIn, m.QuantityOut, m.PreviousBalance, m.NewBalance, m.MovementDate, " &
            "       m.Description, u.FullName AS RecordedByName " &
            "FROM InventoryMovement m " &
            "JOIN Branch b ON b.BranchID = m.BranchID " &
            "JOIN Product p ON p.ProductID = m.ProductID " &
            "JOIN `User` u ON u.UserID = m.RecordedBy " &
            "WHERE (@branch IS NULL OR m.BranchID = @branch) " &
            "  AND (@type IS NULL OR m.MovementType = @type) " &
            "  AND (@from IS NULL OR m.MovementDate >= @from) " &
            "  AND (@to IS NULL OR m.MovementDate < DATE_ADD(@to, INTERVAL 1 DAY)) " &
            "ORDER BY m.MovementDate DESC, m.MovementID DESC LIMIT 500",
            Db.Param("@branch", branchId),
            Db.Param("@type", movementType),
            Db.Param("@from", fromDate),
            Db.Param("@to", toDate))
    End Function

End Module

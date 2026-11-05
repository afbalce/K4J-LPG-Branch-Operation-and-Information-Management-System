Imports System.Data
Imports MySql.Data.MySqlClient

' ==========================================================
' DATA ACCESS LAYER - OPENING / CLOSING CHECKLISTS
' Only SQL lives here. Rules and authorization are in ChecklistService.
' Difference (Physical - System) is a generated column in the database.
' ==========================================================

Public Module ChecklistRepository

    Private Const HeaderSelect As String =
        "SELECT c.ChecklistID, c.BranchID, b.BranchName, c.ChecklistType, c.ChecklistDate, c.Status, " &
        "       u.FullName AS SubmittedByName, c.SubmissionDate " &
        "FROM InventoryChecklist c " &
        "JOIN Branch b ON b.BranchID = c.BranchID " &
        "LEFT JOIN `User` u ON u.UserID = c.SubmittedBy "

    Private Function ToHeader(r As DataRow) As ChecklistHeader
        Return New ChecklistHeader With {
            .ChecklistID = Convert.ToInt32(r("ChecklistID")),
            .BranchID = Convert.ToInt32(r("BranchID")),
            .BranchName = Db.Text(r, "BranchName"),
            .ChecklistType = Db.Text(r, "ChecklistType"),
            .ChecklistDate = Convert.ToDateTime(r("ChecklistDate")),
            .Status = Db.Text(r, "Status"),
            .SubmittedByName = Db.Text(r, "SubmittedByName"),
            .SubmissionDate = Db.DateOrNothing(r, "SubmissionDate")
        }
    End Function

    Public Function FindHeader(conn As MySqlConnection, tx As MySqlTransaction, branchId As Integer,
                               checklistType As String, checklistDate As Date) As ChecklistHeader
        Dim dt As DataTable = Db.QueryTable(conn, tx,
            HeaderSelect & "WHERE c.BranchID = @b AND c.ChecklistType = @t AND c.ChecklistDate = @d",
            Db.Param("@b", branchId), Db.Param("@t", checklistType), Db.Param("@d", checklistDate.Date))
        If dt.Rows.Count = 0 Then Return Nothing
        Return ToHeader(dt.Rows(0))
    End Function

    Public Function FindHeaderById(conn As MySqlConnection, tx As MySqlTransaction,
                                   checklistId As Integer) As ChecklistHeader
        Dim dt As DataTable = Db.QueryTable(conn, tx,
            HeaderSelect & "WHERE c.ChecklistID = @id", Db.Param("@id", checklistId))
        If dt.Rows.Count = 0 Then Return Nothing
        Return ToHeader(dt.Rows(0))
    End Function

    Public Function CreateHeader(conn As MySqlConnection, tx As MySqlTransaction, branchId As Integer,
                                 checklistType As String, checklistDate As Date, userId As Integer) As Integer
        Return Db.InsertAndGetId(conn, tx,
            "INSERT INTO InventoryChecklist (BranchID, ChecklistType, ChecklistDate, Status, CreatedBy) " &
            "VALUES (@b, @t, @d, 'DRAFT', @u)",
            Db.Param("@b", branchId), Db.Param("@t", checklistType),
            Db.Param("@d", checklistDate.Date), Db.Param("@u", userId))
    End Function

    ''' <summary>Adds a line for every branch balance not yet on the checklist (existing lines are kept).</summary>
    Public Sub InsertMissingLines(conn As MySqlConnection, tx As MySqlTransaction,
                                  checklistId As Integer, branchId As Integer)
        Db.Execute(conn, tx,
            "INSERT IGNORE INTO InventoryChecklistDetail " &
            "(ChecklistID, BranchID, ProductID, StockCondition, SystemQuantity) " &
            "SELECT @c, i.BranchID, i.ProductID, i.StockCondition, i.Quantity " &
            "FROM Inventory i JOIN Product p ON p.ProductID = i.ProductID " &
            "WHERE i.BranchID = @b AND p.IsActive = 1",
            Db.Param("@c", checklistId), Db.Param("@b", branchId))
    End Sub

    ''' <summary>Draft checklists always show the current system balance.</summary>
    Public Sub RefreshSystemQuantities(conn As MySqlConnection, tx As MySqlTransaction, checklistId As Integer)
        Db.Execute(conn, tx,
            "UPDATE InventoryChecklistDetail d " &
            "JOIN Inventory i ON i.BranchID = d.BranchID AND i.ProductID = d.ProductID " &
            "                AND i.StockCondition = d.StockCondition " &
            "SET d.SystemQuantity = i.Quantity " &
            "WHERE d.ChecklistID = @c",
            Db.Param("@c", checklistId))
    End Sub

    ''' <summary>Copies the physical counts of the latest submitted closing checklist before the date.</summary>
    Public Sub ApplyPreviousClosing(conn As MySqlConnection, tx As MySqlTransaction, checklistId As Integer,
                                    branchId As Integer, checklistDate As Date)
        Dim previous As Object = Db.Scalar(conn, tx,
            "SELECT ChecklistID FROM InventoryChecklist " &
            "WHERE BranchID = @b AND ChecklistType = 'CLOSING' AND Status = 'SUBMITTED' " &
            "  AND ChecklistDate < @d ORDER BY ChecklistDate DESC LIMIT 1",
            Db.Param("@b", branchId), Db.Param("@d", checklistDate.Date))
        If previous Is Nothing Then Return

        Db.Execute(conn, tx,
            "UPDATE InventoryChecklistDetail d " &
            "JOIN InventoryChecklistDetail prev ON prev.ChecklistID = @prev " &
            "     AND prev.ProductID = d.ProductID AND prev.StockCondition = d.StockCondition " &
            "SET d.PreviousClosingQuantity = prev.PhysicalQuantity " &
            "WHERE d.ChecklistID = @c",
            Db.Param("@prev", Convert.ToInt32(previous)), Db.Param("@c", checklistId))
    End Sub

    Public Function GetLines(conn As MySqlConnection, tx As MySqlTransaction,
                             checklistId As Integer) As List(Of ChecklistLine)
        Dim dt As DataTable = Db.QueryTable(conn, tx,
            "SELECT d.DetailID, d.ProductID, p.ProductName, d.StockCondition, d.PreviousClosingQuantity, " &
            "       d.SystemQuantity, d.PhysicalQuantity, d.Remarks, " &
            "       (SELECT COUNT(*) FROM InventoryMovement m WHERE m.ChecklistDetailID = d.DetailID) AS AdjustCount " &
            "FROM InventoryChecklistDetail d " &
            "JOIN Product p ON p.ProductID = d.ProductID " &
            "WHERE d.ChecklistID = @c " &
            "ORDER BY p.Category, p.ProductName, " &
            "FIELD(d.StockCondition, 'FILLED', 'EMPTY', 'REVOLVING', 'DAMAGED', 'GENERAL')",
            Db.Param("@c", checklistId))

        Dim list As New List(Of ChecklistLine)()
        For Each r As DataRow In dt.Rows
            list.Add(New ChecklistLine With {
                .DetailID = Convert.ToInt32(r("DetailID")),
                .ProductID = Convert.ToInt32(r("ProductID")),
                .ProductName = Db.Text(r, "ProductName"),
                .StockCondition = Db.Text(r, "StockCondition"),
                .PreviousClosingQuantity = Db.IntOrNothing(r, "PreviousClosingQuantity"),
                .SystemQuantity = Convert.ToInt32(r("SystemQuantity")),
                .PhysicalQuantity = Db.IntOrNothing(r, "PhysicalQuantity"),
                .Remarks = Db.Text(r, "Remarks"),
                .Adjusted = Convert.ToInt32(r("AdjustCount")) > 0
            })
        Next
        Return list
    End Function

    Public Sub UpdateLine(conn As MySqlConnection, tx As MySqlTransaction, checklistId As Integer,
                          line As ChecklistLine)
        Db.Execute(conn, tx,
            "UPDATE InventoryChecklistDetail SET PhysicalQuantity = @phys, Remarks = @rem " &
            "WHERE DetailID = @id AND ChecklistID = @c",
            Db.Param("@phys", line.PhysicalQuantity),
            Db.Param("@rem", If(String.IsNullOrWhiteSpace(line.Remarks), Nothing, line.Remarks.Trim())),
            Db.Param("@id", line.DetailID),
            Db.Param("@c", checklistId))
    End Sub

    ''' <summary>Returns True when this call changed the checklist from DRAFT to SUBMITTED.</summary>
    Public Function MarkSubmitted(conn As MySqlConnection, tx As MySqlTransaction, checklistId As Integer,
                                  userId As Integer) As Boolean
        Dim affected As Integer = Db.Execute(conn, tx,
            "UPDATE InventoryChecklist SET Status = 'SUBMITTED', SubmittedBy = @u, SubmissionDate = NOW() " &
            "WHERE ChecklistID = @c AND Status = 'DRAFT'",
            Db.Param("@u", userId), Db.Param("@c", checklistId))
        Return affected = 1
    End Function

    ''' <summary>Submitted checklists for the Super Admin review screen. Any filter may be Nothing.</summary>
    Public Function GetSubmissions(conn As MySqlConnection, branchId As Integer?, checklistType As String,
                                   fromDate As Date?, toDate As Date?) As DataTable
        Return Db.QueryTable(conn, Nothing,
            "SELECT c.ChecklistID, b.BranchName, c.ChecklistType, c.ChecklistDate, " &
            "       u.FullName AS SubmittedByName, c.SubmissionDate, " &
            "       (SELECT COUNT(*) FROM InventoryChecklistDetail d " &
            "         WHERE d.ChecklistID = c.ChecklistID AND d.Difference <> 0) AS Discrepancies, " &
            "       (SELECT COUNT(*) FROM InventoryChecklistDetail d " &
            "         WHERE d.ChecklistID = c.ChecklistID AND d.Difference <> 0 " &
            "           AND EXISTS (SELECT 1 FROM InventoryMovement m WHERE m.ChecklistDetailID = d.DetailID)) AS AdjustedCount " &
            "FROM InventoryChecklist c " &
            "JOIN Branch b ON b.BranchID = c.BranchID " &
            "LEFT JOIN `User` u ON u.UserID = c.SubmittedBy " &
            "WHERE c.Status = 'SUBMITTED' " &
            "  AND (@branch IS NULL OR c.BranchID = @branch) " &
            "  AND (@type IS NULL OR c.ChecklistType = @type) " &
            "  AND (@from IS NULL OR c.ChecklistDate >= @from) " &
            "  AND (@to IS NULL OR c.ChecklistDate <= @to) " &
            "ORDER BY c.ChecklistDate DESC, c.ChecklistID DESC LIMIT 200",
            Db.Param("@branch", branchId), Db.Param("@type", checklistType),
            Db.Param("@from", fromDate), Db.Param("@to", toDate))
    End Function

    ''' <summary>Locks one checklist line so two people cannot adjust the same discrepancy twice.</summary>
    Public Function LockLineForAdjustment(conn As MySqlConnection, tx As MySqlTransaction,
                                          detailId As Integer) As AdjustmentTarget
        Dim dt As DataTable = Db.QueryTable(conn, tx,
            "SELECT d.DetailID, d.BranchID, d.ProductID, d.StockCondition, d.Difference, c.Status " &
            "FROM InventoryChecklistDetail d " &
            "JOIN InventoryChecklist c ON c.ChecklistID = d.ChecklistID " &
            "WHERE d.DetailID = @id FOR UPDATE",
            Db.Param("@id", detailId))
        If dt.Rows.Count = 0 Then Return Nothing
        Dim r As DataRow = dt.Rows(0)
        Return New AdjustmentTarget With {
            .DetailID = Convert.ToInt32(r("DetailID")),
            .BranchID = Convert.ToInt32(r("BranchID")),
            .ProductID = Convert.ToInt32(r("ProductID")),
            .StockCondition = Db.Text(r, "StockCondition"),
            .Difference = Db.IntOrNothing(r, "Difference"),
            .ChecklistStatus = Db.Text(r, "Status")
        }
    End Function

    Public Function CountAdjustments(conn As MySqlConnection, tx As MySqlTransaction, detailId As Integer) As Integer
        Dim result As Object = Db.Scalar(conn, tx,
            "SELECT COUNT(*) FROM InventoryMovement WHERE ChecklistDetailID = @id",
            Db.Param("@id", detailId))
        If result Is Nothing Then Return 0
        Return Convert.ToInt32(result)
    End Function

End Module

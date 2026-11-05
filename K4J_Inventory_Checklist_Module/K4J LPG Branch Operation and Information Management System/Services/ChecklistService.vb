Imports System.Data
Imports MySql.Data.MySqlClient

' ==========================================================
' SERVICE LAYER - OPENING AND CLOSING CHECKLISTS
'   Difference = Physical Quantity - System Quantity
'   negative = missing stock, positive = excess stock
' A checklist NEVER changes inventory balances. A discrepancy is only
' corrected later through InventoryService.AdjustFromChecklistLine,
' which needs a reason and records the user responsible.
' ==========================================================

Public Module ChecklistService

    Private Const MaxRemarksLength As Integer = 255
    Private Const MaxQuantity As Integer = 1000000

    ''' <summary>
    ''' Opens today's checklist for the Branch Admin's own branch. Creates a draft with one line per
    ''' inventory item the first time. Drafts always show the current system quantity.
    ''' </summary>
    Public Function LoadToday(checklistType As String) As ChecklistData
        Dim user As SessionUser = Authorization.RequireBranchAdmin()
        ValidateType(checklistType)
        Dim branchId As Integer = user.BranchID.Value
        Dim today As Date = Date.Today

        Return Db.InTransaction(Of ChecklistData)(
            Function(conn, tx)
                InventoryRepository.EnsureRowsForBranch(conn, tx, branchId)

                Dim header As ChecklistHeader =
                    ChecklistRepository.FindHeader(conn, tx, branchId, checklistType, today)

                If header Is Nothing Then
                    Dim newId As Integer =
                        ChecklistRepository.CreateHeader(conn, tx, branchId, checklistType, today, user.UserID)
                    ChecklistRepository.InsertMissingLines(conn, tx, newId, branchId)
                    header = ChecklistRepository.FindHeaderById(conn, tx, newId)
                ElseIf header.Status = ChecklistStatuses.Draft Then
                    ChecklistRepository.InsertMissingLines(conn, tx, header.ChecklistID, branchId)
                    ChecklistRepository.RefreshSystemQuantities(conn, tx, header.ChecklistID)
                End If

                If header.Status = ChecklistStatuses.Draft AndAlso checklistType = ChecklistTypes.Opening Then
                    ChecklistRepository.ApplyPreviousClosing(conn, tx, header.ChecklistID, branchId, today)
                End If

                Return New ChecklistData With {
                    .Header = header,
                    .Lines = ChecklistRepository.GetLines(conn, tx, header.ChecklistID)
                }
            End Function)
    End Function

    ''' <summary>Saves the counts and remarks entered so far. Incomplete counts are allowed.</summary>
    Public Sub SaveDraft(checklistId As Integer, lines As List(Of ChecklistLine))
        Dim user As SessionUser = Authorization.RequireBranchAdmin()

        Db.InTransaction(Of Integer)(
            Function(conn, tx)
                RequireEditableOwnChecklist(conn, tx, user, checklistId)
                For Each line As ChecklistLine In lines
                    ValidateLineValues(line)
                    ChecklistRepository.UpdateLine(conn, tx, checklistId, line)
                Next
                Return 0
            End Function)
    End Sub

    ''' <summary>
    ''' Validates and submits. Every item needs a physical count, and every item with a difference
    ''' needs a remark. If stock changed while the admin was counting, nothing is saved.
    ''' </summary>
    Public Sub Submit(checklistId As Integer, lines As List(Of ChecklistLine))
        Dim user As SessionUser = Authorization.RequireBranchAdmin()

        Db.InTransaction(Of Integer)(
            Function(conn, tx)
                Dim header As ChecklistHeader = RequireEditableOwnChecklist(conn, tx, user, checklistId)

                ' Hold the branch balances still while the checklist is being submitted.
                InventoryRepository.LockBranchInventory(conn, tx, header.BranchID)
                ChecklistRepository.RefreshSystemQuantities(conn, tx, checklistId)
                Dim current As List(Of ChecklistLine) = ChecklistRepository.GetLines(conn, tx, checklistId)

                Dim currentById As New Dictionary(Of Integer, ChecklistLine)()
                For Each c As ChecklistLine In current
                    currentById(c.DetailID) = c
                Next

                If lines.Count <> current.Count Then
                    Throw New ChecklistChangedException("The checklist changed. It will be reloaded.")
                End If

                Dim missingCounts As Integer = 0
                Dim missingRemarks As Integer = 0
                For Each line As ChecklistLine In lines
                    Dim match As ChecklistLine = Nothing
                    If Not currentById.TryGetValue(line.DetailID, match) Then
                        Throw New ChecklistChangedException("The checklist changed. It will be reloaded.")
                    End If
                    If match.SystemQuantity <> line.SystemQuantity Then
                        Throw New ChecklistChangedException(
                            "Stock changed while you were counting. The checklist will be reloaded with the new system quantities.")
                    End If

                    ValidateLineValues(line)
                    If Not line.PhysicalQuantity.HasValue Then
                        missingCounts += 1
                    ElseIf line.Difference.Value <> 0 AndAlso String.IsNullOrWhiteSpace(line.Remarks) Then
                        missingRemarks += 1
                    End If
                Next

                If missingCounts > 0 Then
                    Throw New BusinessRuleException(
                        "Enter a physical count for every item (" & missingCounts.ToString() & " missing).")
                End If
                If missingRemarks > 0 Then
                    Throw New BusinessRuleException(
                        "Add a remark for every item with a difference (" & missingRemarks.ToString() & " missing).")
                End If

                For Each line As ChecklistLine In lines
                    ChecklistRepository.UpdateLine(conn, tx, checklistId, line)
                Next

                If Not ChecklistRepository.MarkSubmitted(conn, tx, checklistId, user.UserID) Then
                    Throw New BusinessRuleException("This checklist was already submitted.")
                End If
                Return 0
            End Function)
    End Sub

    ' ---------------------------------------------------------
    ' SUPER ADMIN (read-only review)
    ' ---------------------------------------------------------

    Public Function GetSubmissions(branchId As Integer?, checklistType As String,
                                   fromDate As Date?, toDate As Date?) As DataTable
        Authorization.RequireSuperAdmin()
        Dim typeFilter As String = If(String.IsNullOrWhiteSpace(checklistType), Nothing, checklistType)
        Return Db.ReadOnlyWork(Of DataTable)(
            Function(conn) ChecklistRepository.GetSubmissions(conn, branchId, typeFilter, fromDate, toDate))
    End Function

    Public Function GetChecklistLines(checklistId As Integer) As List(Of ChecklistLine)
        Dim user As SessionUser = Authorization.RequireSignedIn()
        Return Db.ReadOnlyWork(Of List(Of ChecklistLine))(
            Function(conn)
                Dim header As ChecklistHeader = ChecklistRepository.FindHeaderById(conn, Nothing, checklistId)
                If header Is Nothing Then Throw New BusinessRuleException("That checklist was not found.")
                Authorization.ResolveBranch(user, header.BranchID)
                Return ChecklistRepository.GetLines(conn, Nothing, checklistId)
            End Function)
    End Function

    ' ---------------------------------------------------------
    ' HELPERS
    ' ---------------------------------------------------------

    Private Function RequireEditableOwnChecklist(conn As MySqlConnection, tx As MySqlTransaction,
                                                 user As SessionUser, checklistId As Integer) As ChecklistHeader
        Dim header As ChecklistHeader = ChecklistRepository.FindHeaderById(conn, tx, checklistId)
        If header Is Nothing Then Throw New BusinessRuleException("That checklist was not found.")
        If header.BranchID <> user.BranchID.Value Then
            Throw New UnauthorizedAccessException("You can only work on your own branch's checklist.")
        End If
        If header.Status <> ChecklistStatuses.Draft Then
            Throw New BusinessRuleException("This checklist was already submitted and can no longer be edited.")
        End If
        Return header
    End Function

    Private Sub ValidateType(checklistType As String)
        If checklistType <> ChecklistTypes.Opening AndAlso checklistType <> ChecklistTypes.Closing Then
            Throw New BusinessRuleException("Unknown checklist type.")
        End If
    End Sub

    Private Sub ValidateLineValues(line As ChecklistLine)
        If line.PhysicalQuantity.HasValue Then
            If line.PhysicalQuantity.Value < 0 OrElse line.PhysicalQuantity.Value > MaxQuantity Then
                Throw New BusinessRuleException(
                    "The physical count for " & line.ProductName & " must be between 0 and " & MaxQuantity.ToString("N0") & ".")
            End If
        End If
        If Not String.IsNullOrEmpty(line.Remarks) AndAlso line.Remarks.Trim().Length > MaxRemarksLength Then
            Throw New BusinessRuleException("Keep each remark under " & MaxRemarksLength.ToString() & " characters.")
        End If
    End Sub

End Module

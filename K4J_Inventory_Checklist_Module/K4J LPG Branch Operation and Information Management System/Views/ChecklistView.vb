Imports System.Drawing
Imports System.Windows.Forms

' ==========================================================
' OPENING / CLOSING CHECKLIST SCREEN (Branch Admin)
' One control serves both checklists; the type decides the columns.
'   Difference = Physical - System  (negative = missing, positive = excess)
' The grid is calculated live; ChecklistService does the real validation.
' A checklist never changes stock. After submission, a difference can
' be resolved with "Adjust selected line", which needs a reason.
' ==========================================================

Public Class ChecklistView
    Inherits UserControl

    Private ReadOnly _type As String
    Private _data As ChecklistData
    Private _loading As Boolean

    Private grid As StyledDataGridView
    Private lblTitle As Label
    Private lblHelp As Label
    Private lblPill As Label
    Private lblSummary As Label
    Private lblMessage As Label
    Private btnSave As Button
    Private btnSubmit As Button
    Private btnAdjust As Button
    Private btnReload As Button

    Private Const MaxCount As Integer = 1000000

    Public Sub New(checklistType As String)
        _type = checklistType
        Me.Dock = DockStyle.Fill
        Me.BackColor = UiTheme.PageGrey
        Me.Padding = New Padding(18, 8, 18, 12)
        Me.Font = UiTheme.BodyFont()
        BuildUi()
    End Sub

    ' ---------------------------------------------------------
    ' UI
    ' ---------------------------------------------------------

    Private Sub BuildUi()
        grid = New StyledDataGridView()
        grid.Dock = DockStyle.Fill
        UiTheme.StyleGrid(grid)

        AddColumn("Product", "Product", 190, True)
        AddColumn("Condition", "Condition", 110, True)
        If _type = ChecklistTypes.Opening Then AddColumn("Previous", "Previous closing", 95, True)
        AddColumn("System", "System quantity", 90, True)
        AddColumn("Physical", "Physical count", 95, False)
        AddColumn("Difference", "Difference", 80, True)
        AddColumn("Remarks", "Remarks", 220, False)
        AddColumn("Adjusted", "Stock adjustment", 110, True)
        UiTheme.RightAlign(grid, "Previous", "System", "Physical", "Difference")
        grid.Columns("Physical").DefaultCellStyle.BackColor = Color.FromArgb(255, 251, 235)
        AddHandler grid.CellEndEdit, AddressOf OnCellEndEdit
        Me.Controls.Add(grid)

        Dim bottom As New Panel()
        bottom.Dock = DockStyle.Bottom
        bottom.Height = 82

        Dim buttons As New FlowLayoutPanel()
        buttons.Dock = DockStyle.Fill
        buttons.Padding = New Padding(0, 6, 0, 0)
        btnSave = UiTheme.MakeButton("Save draft", False)
        btnSubmit = UiTheme.MakeButton("Submit checklist", True)
        btnAdjust = UiTheme.MakeButton("Adjust selected line", False)
        btnReload = UiTheme.MakeButton("Reload", False)
        AddHandler btnSave.Click, AddressOf OnSaveDraft
        AddHandler btnSubmit.Click, AddressOf OnSubmit
        AddHandler btnAdjust.Click, AddressOf OnAdjust
        AddHandler btnReload.Click, Sub(s, e) LoadData(True)
        buttons.Controls.AddRange(New Control() {btnSave, btnSubmit, btnAdjust, btnReload})
        bottom.Controls.Add(buttons)

        lblMessage = UiTheme.MakeLabel("", 9.5F, True)
        lblMessage.AutoSize = False
        lblMessage.Dock = DockStyle.Top
        lblMessage.Height = 30
        lblMessage.TextAlign = ContentAlignment.MiddleLeft
        bottom.Controls.Add(lblMessage)
        Me.Controls.Add(bottom)

        Dim top As New Panel()
        top.Dock = DockStyle.Top
        top.Height = 122

        lblTitle = UiTheme.MakeLabel("", 13.0F, True)
        lblTitle.Location = New Point(0, 6)
        lblPill = UiTheme.MakeLabel("", 9.0F, True)
        lblPill.Location = New Point(0, 64)
        lblPill.Padding = New Padding(8, 3, 8, 3)
        lblSummary = UiTheme.MakeLabel("", 9.5F, False, True)
        lblSummary.Location = New Point(0, 94)
        lblHelp = UiTheme.MakeLabel(
            If(_type = ChecklistTypes.Opening,
               "Count every item and enter the physical quantity. Previous closing is the last submitted closing count.",
               "Count every item at closing time. System quantity is the expected stock after today's movements."),
            9.0F, False, True)
        lblHelp.Location = New Point(0, 36)
        top.Controls.AddRange(New Control() {lblTitle, lblPill, lblSummary, lblHelp})
        Me.Controls.Add(top)
    End Sub

    Private Sub AddColumn(name As String, header As String, weight As Single, isReadOnly As Boolean)
        Dim c As New DataGridViewTextBoxColumn()
        c.Name = name
        c.HeaderText = header
        c.FillWeight = weight
        c.ReadOnly = isReadOnly
        c.SortMode = DataGridViewColumnSortMode.NotSortable
        grid.Columns.Add(c)
    End Sub

    ' ---------------------------------------------------------
    ' LOADING
    ' ---------------------------------------------------------

    Private Sub OnViewLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.DesignMode Then Return
        AppSession.TryDevSignIn(False)
        LoadData(False)
    End Sub

    ''' <summary>Loads today's checklist. keepEntries re-applies counts typed but not yet saved.</summary>
    Private Sub LoadData(keepEntries As Boolean)
        Dim typed As New Dictionary(Of Integer, ChecklistLine)()
        If keepEntries AndAlso _data IsNot Nothing Then
            For Each l As ChecklistLine In _data.Lines
                typed(l.DetailID) = l
            Next
        End If

        Try
            Dim fresh As ChecklistData = ChecklistService.LoadToday(_type)
            If fresh.Header.Status = ChecklistStatuses.Draft Then
                For Each l As ChecklistLine In fresh.Lines
                    Dim old As ChecklistLine = Nothing
                    If typed.TryGetValue(l.DetailID, old) Then
                        l.PhysicalQuantity = old.PhysicalQuantity
                        l.Remarks = old.Remarks
                    End If
                Next
            End If
            _data = fresh
            Render()
        Catch ex As Exception
            _data = Nothing
            grid.Rows.Clear()
            lblTitle.Text = If(_type = ChecklistTypes.Opening, "Opening checklist", "Closing checklist")
            lblPill.Text = ""
            lblSummary.Text = ""
            SetEnabled(False, False)
            ShowMessage(ex.Message, True)
            UiTheme.ShowError(Me, ex)
        End Try
    End Sub

    Private Sub Render()
        _loading = True
        Try
            Dim header As ChecklistHeader = _data.Header
            Dim isDraft As Boolean = (header.Status = ChecklistStatuses.Draft)

            lblTitle.Text = If(_type = ChecklistTypes.Opening, "Opening checklist", "Closing checklist") &
                            "  -  " & header.BranchName & "  -  " & header.ChecklistDate.ToString("dddd, dd MMM yyyy")

            If isDraft Then
                lblPill.Text = "Draft - not submitted"
                lblPill.BackColor = Color.FromArgb(224, 231, 255)
                lblPill.ForeColor = Color.FromArgb(55, 48, 163)
            Else
                lblPill.Text = "Submitted " & If(header.SubmissionDate.HasValue, header.SubmissionDate.Value.ToString("dd MMM yyyy HH:mm"), "") &
                               " by " & header.SubmittedByName
                lblPill.BackColor = Color.FromArgb(220, 252, 231)
                lblPill.ForeColor = Color.FromArgb(21, 101, 52)
            End If

            grid.ReadOnly = Not isDraft
            grid.Rows.Clear()
            For Each line As ChecklistLine In _data.Lines
                Dim index As Integer = grid.Rows.Add()
                Dim row As DataGridViewRow = grid.Rows(index)
                row.Tag = line
                row.Cells("Product").Value = line.ProductName
                row.Cells("Condition").Value = StockConditions.DisplayName(line.StockCondition)
                If _type = ChecklistTypes.Opening Then
                    row.Cells("Previous").Value = If(line.PreviousClosingQuantity.HasValue,
                                                     line.PreviousClosingQuantity.Value.ToString("N0"), "-")
                End If
                RefreshRow(row)
            Next
        Finally
            _loading = False
        End Try

        UpdateSummary()
        SetEnabled(_data.Header.Status = ChecklistStatuses.Draft, _data.Header.Status = ChecklistStatuses.Submitted)
        ShowMessage("", False)
    End Sub

    Private Sub RefreshRow(row As DataGridViewRow)
        Dim line As ChecklistLine = DirectCast(row.Tag, ChecklistLine)
        Dim isDraft As Boolean = (_data.Header.Status = ChecklistStatuses.Draft)

        row.Cells("System").Value = line.SystemQuantity.ToString("N0")
        row.Cells("Physical").Value = If(line.PhysicalQuantity.HasValue, line.PhysicalQuantity.Value.ToString(), "")

        Dim diffCell As DataGridViewCell = row.Cells("Difference")
        Dim diff As Integer? = line.Difference
        If diff.HasValue Then
            diffCell.Value = If(diff.Value > 0, "+", "") & diff.Value.ToString()
            diffCell.Style.ForeColor = If(diff.Value < 0, UiTheme.DangerRed,
                                          If(diff.Value > 0, UiTheme.WarningAmber, UiTheme.SuccessGreen))
            diffCell.Style.Font = UiTheme.BodyFont(9.5F, True)
        Else
            diffCell.Value = ""
        End If

        Dim needsRemark As Boolean = diff.HasValue AndAlso diff.Value <> 0 AndAlso String.IsNullOrWhiteSpace(line.Remarks)
        row.Cells("Remarks").Value = line.Remarks
        row.Cells("Remarks").Style.BackColor = If(needsRemark AndAlso isDraft, UiTheme.MissingFill, Color.White)

        If isDraft Then
            row.Cells("Adjusted").Value = ""
        ElseIf diff.HasValue AndAlso diff.Value <> 0 Then
            row.Cells("Adjusted").Value = If(line.Adjusted, "Adjusted", "Needs review")
            row.Cells("Adjusted").Style.ForeColor = If(line.Adjusted, UiTheme.SuccessGreen, UiTheme.WarningAmber)
        Else
            row.Cells("Adjusted").Value = "-"
        End If
    End Sub

    Private Sub UpdateSummary()
        If _data Is Nothing Then Return
        Dim counted As Integer = 0
        Dim different As Integer = 0
        For Each l As ChecklistLine In _data.Lines
            If l.PhysicalQuantity.HasValue Then
                counted += 1
                If l.Difference.Value <> 0 Then different += 1
            End If
        Next
        lblSummary.Text = counted.ToString() & " of " & _data.Lines.Count.ToString() & " items counted   |   " &
                          different.ToString() & " with a difference"
    End Sub

    Private Sub SetEnabled(canEdit As Boolean, canAdjust As Boolean)
        btnSave.Visible = canEdit
        btnSubmit.Visible = canEdit
        btnAdjust.Visible = canAdjust
        btnReload.Visible = True
    End Sub

    Private Sub ShowMessage(text As String, isError As Boolean)
        lblMessage.Text = text
        lblMessage.ForeColor = If(isError, UiTheme.DangerRed, UiTheme.SuccessGreen)
    End Sub

    ' ---------------------------------------------------------
    ' EDITING
    ' ---------------------------------------------------------

    Private Sub OnCellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If _loading OrElse e.RowIndex < 0 OrElse _data Is Nothing Then Return
        Dim row As DataGridViewRow = grid.Rows(e.RowIndex)
        Dim line As ChecklistLine = DirectCast(row.Tag, ChecklistLine)
        Dim columnName As String = grid.Columns(e.ColumnIndex).Name

        If columnName = "Physical" Then
            Dim text As String = Convert.ToString(row.Cells("Physical").Value)
            text = If(text, "").Trim()
            If text.Length = 0 Then
                line.PhysicalQuantity = Nothing
                ShowMessage("", False)
            Else
                Dim value As Integer
                If Integer.TryParse(text, value) AndAlso value >= 0 AndAlso value <= MaxCount Then
                    line.PhysicalQuantity = value
                    ShowMessage("", False)
                Else
                    ShowMessage("Enter a whole number of 0 or more for " & line.ProductName & ".", True)
                End If
            End If
        ElseIf columnName = "Remarks" Then
            line.Remarks = Convert.ToString(row.Cells("Remarks").Value)
        End If

        RefreshRow(row)
        UpdateSummary()
    End Sub

    Private Sub OnSaveDraft(sender As Object, e As EventArgs)
        If _data Is Nothing Then Return
        grid.EndEdit()
        Try
            ChecklistService.SaveDraft(_data.Header.ChecklistID, _data.Lines)
            ShowMessage("Draft saved. Inventory balances were not changed.", False)
        Catch ex As Exception
            UiTheme.ShowError(Me, ex)
        End Try
    End Sub

    Private Sub OnSubmit(sender As Object, e As EventArgs)
        If _data Is Nothing Then Return
        grid.EndEdit()

        Dim answer As DialogResult = MessageBox.Show(Me.FindForm(),
            "Submit this checklist? You will not be able to edit the counts afterwards.",
            "Submit checklist", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If answer <> DialogResult.Yes Then Return

        Try
            ChecklistService.Submit(_data.Header.ChecklistID, _data.Lines)
            LoadData(False)
            ShowMessage("Checklist submitted. Use Adjust selected line to resolve any difference.", False)
        Catch ex As ChecklistChangedException
            UiTheme.ShowError(Me, ex)
            LoadData(True)
        Catch ex As Exception
            UiTheme.ShowError(Me, ex)
        End Try
    End Sub

    Private Sub OnAdjust(sender As Object, e As EventArgs)
        If _data Is Nothing OrElse grid.CurrentRow Is Nothing Then
            ShowMessage("Select a line first.", True)
            Return
        End If

        Dim line As ChecklistLine = DirectCast(grid.CurrentRow.Tag, ChecklistLine)
        If Not line.Difference.HasValue OrElse line.Difference.Value = 0 Then
            ShowMessage("The selected line has no difference to adjust.", True)
            Return
        End If
        If line.Adjusted Then
            ShowMessage("The selected line was already adjusted.", True)
            Return
        End If

        Using dlg As New ChecklistAdjustmentDialog(line)
            If dlg.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                LoadData(False)
                ShowMessage("Stock adjusted and recorded in the movement history.", False)
            End If
        End Using
    End Sub

End Class

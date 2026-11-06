Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

' ==========================================================
' SUPER ADMIN GLOBAL INVENTORY SCREEN (read-only)
'   - Stock by branch : each branch shown on its own rows (never merged)
'   - Compare branches: JPang vs Daet side by side, plus a combined total
'   - Movements       : incoming / outgoing / adjustment history
'   - Checklists      : submitted opening and closing checklists, with
'                       discrepancies and whether they were adjusted
' The Super Admin cannot change anything here; the service layer
' refuses all write operations for this role.
' ==========================================================

Public Class GlobalInventoryView
    Inherits UserControl

    Private cboBranch As ComboBox
    Private lblStatus As Label
    Private gridStock As StyledDataGridView
    Private gridCompare As StyledDataGridView
    Private gridMovements As StyledDataGridView
    Private gridChecklists As StyledDataGridView
    Private gridLines As StyledDataGridView

    Private dtpMoveFrom As DateTimePicker
    Private dtpMoveTo As DateTimePicker
    Private cboMoveType As ComboBox
    Private dtpCheckFrom As DateTimePicker
    Private dtpCheckTo As DateTimePicker
    Private cboCheckType As ComboBox

    Private _loadingBranches As Boolean

    Public Sub New()
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
        Dim tabs As New TabControl()
        tabs.Dock = DockStyle.Fill
        tabs.Font = UiTheme.BodyFont()

        ' --- Stock by branch
        Dim pageStock As New TabPage("Stock by branch")
        pageStock.BackColor = Color.White
        gridStock = NewGrid()
        pageStock.Controls.Add(gridStock)

        ' --- Compare branches
        Dim pageCompare As New TabPage("Compare branches")
        pageCompare.BackColor = Color.White
        gridCompare = NewGrid()
        pageCompare.Controls.Add(gridCompare)
        Dim note As Label = UiTheme.MakeLabel(
            "Each branch has its own stock. The combined column only adds both branches together for a total.",
            9.0F, False, True)
        note.Dock = DockStyle.Top
        note.AutoSize = False
        note.Height = 30
        note.TextAlign = ContentAlignment.MiddleLeft
        note.Padding = New Padding(8, 0, 0, 0)
        pageCompare.Controls.Add(note)

        ' --- Movements
        Dim pageMoves As New TabPage("Movements")
        pageMoves.BackColor = Color.White
        gridMovements = NewGrid()
        AddHandler gridMovements.CellFormatting, AddressOf OnCellFormatting
        pageMoves.Controls.Add(gridMovements)
        pageMoves.Controls.Add(BuildMovementFilters())

        ' --- Checklists
        Dim pageChecks As New TabPage("Checklists")
        pageChecks.BackColor = Color.White
        gridLines = NewGrid()
        pageChecks.Controls.Add(gridLines)
        gridChecklists = NewGrid()
        gridChecklists.Dock = DockStyle.Top
        gridChecklists.Height = 210
        AddHandler gridChecklists.CellFormatting, AddressOf OnCellFormatting
        AddHandler gridChecklists.SelectionChanged, AddressOf OnChecklistSelected
        pageChecks.Controls.Add(gridChecklists)
        pageChecks.Controls.Add(BuildChecklistFilters())

        tabs.TabPages.Add(pageStock)
        tabs.TabPages.Add(pageCompare)
        tabs.TabPages.Add(pageMoves)
        tabs.TabPages.Add(pageChecks)
        Me.Controls.Add(tabs)

        lblStatus = UiTheme.MakeLabel("", 9.0F, False, True)
        lblStatus.AutoSize = False
        lblStatus.Dock = DockStyle.Bottom
        lblStatus.Height = 24
        lblStatus.TextAlign = ContentAlignment.MiddleLeft
        Me.Controls.Add(lblStatus)

        Dim bar As New FlowLayoutPanel()
        bar.Dock = DockStyle.Top
        bar.Height = 48
        bar.Padding = New Padding(0, 6, 0, 6)
        Dim caption As Label = UiTheme.MakeLabel("Branch", 9.0F, True)
        caption.Margin = New Padding(0, 8, 6, 0)
        cboBranch = New ComboBox()
        cboBranch.DropDownStyle = ComboBoxStyle.DropDownList
        cboBranch.Width = 260
        AddHandler cboBranch.SelectedIndexChanged, Sub(s, e) If Not _loadingBranches Then LoadAll()
        Dim refresh As Button = UiTheme.MakeButton("Refresh", False)
        refresh.Margin = New Padding(12, 0, 0, 0)
        AddHandler refresh.Click, Sub(s, e) LoadAll()
        bar.Controls.AddRange(New Control() {caption, cboBranch, refresh})
        Me.Controls.Add(bar)
    End Sub

    Private Function NewGrid() As StyledDataGridView
        Dim g As New StyledDataGridView()
        g.Dock = DockStyle.Fill
        g.ReadOnly = True
        UiTheme.StyleGrid(g)
        Return g
    End Function

    Private Function BuildMovementFilters() As Panel
        Dim panel As New FlowLayoutPanel()
        panel.Dock = DockStyle.Top
        panel.Height = 48
        panel.Padding = New Padding(8, 8, 8, 4)
        panel.BackColor = Color.White

        dtpMoveFrom = NewDatePicker(Date.Today.AddDays(-30))
        dtpMoveTo = NewDatePicker(Date.Today)
        cboMoveType = New ComboBox()
        cboMoveType.DropDownStyle = ComboBoxStyle.DropDownList
        cboMoveType.Width = 140
        cboMoveType.Items.Add(New ChoiceItem("All movements", Nothing))
        cboMoveType.Items.Add(New ChoiceItem("Incoming", MovementTypes.Incoming))
        cboMoveType.Items.Add(New ChoiceItem("Outgoing", MovementTypes.Outgoing))
        cboMoveType.Items.Add(New ChoiceItem("Adjustments", MovementTypes.Adjustment))
        cboMoveType.SelectedIndex = 0

        Dim apply As Button = UiTheme.MakeButton("Apply", False)
        apply.MinimumSize = New Size(80, 28)
        AddHandler apply.Click, Sub(s, e) SafeRun(AddressOf LoadMovements)

        AddFilterLabel(panel, "From")
        panel.Controls.Add(dtpMoveFrom)
        AddFilterLabel(panel, "To")
        panel.Controls.Add(dtpMoveTo)
        panel.Controls.Add(cboMoveType)
        panel.Controls.Add(apply)
        Return panel
    End Function

    Private Function BuildChecklistFilters() As Panel
        Dim panel As New FlowLayoutPanel()
        panel.Dock = DockStyle.Top
        panel.Height = 48
        panel.Padding = New Padding(8, 8, 8, 4)
        panel.BackColor = Color.White

        dtpCheckFrom = NewDatePicker(Date.Today.AddDays(-30))
        dtpCheckTo = NewDatePicker(Date.Today)
        cboCheckType = New ComboBox()
        cboCheckType.DropDownStyle = ComboBoxStyle.DropDownList
        cboCheckType.Width = 140
        cboCheckType.Items.Add(New ChoiceItem("Opening and closing", Nothing))
        cboCheckType.Items.Add(New ChoiceItem("Opening only", ChecklistTypes.Opening))
        cboCheckType.Items.Add(New ChoiceItem("Closing only", ChecklistTypes.Closing))
        cboCheckType.SelectedIndex = 0

        Dim apply As Button = UiTheme.MakeButton("Apply", False)
        apply.MinimumSize = New Size(80, 28)
        AddHandler apply.Click, Sub(s, e) SafeRun(AddressOf LoadChecklists)

        AddFilterLabel(panel, "From")
        panel.Controls.Add(dtpCheckFrom)
        AddFilterLabel(panel, "To")
        panel.Controls.Add(dtpCheckTo)
        panel.Controls.Add(cboCheckType)
        panel.Controls.Add(apply)
        Return panel
    End Function

    Private Function NewDatePicker(initial As Date) As DateTimePicker
        Dim d As New DateTimePicker()
        d.Format = DateTimePickerFormat.Short
        d.Width = 110
        d.Value = initial
        Return d
    End Function

    Private Sub AddFilterLabel(panel As FlowLayoutPanel, text As String)
        Dim l As Label = UiTheme.MakeLabel(text, 9.0F, True)
        l.Margin = New Padding(6, 6, 4, 0)
        panel.Controls.Add(l)
    End Sub

    ' ---------------------------------------------------------
    ' DATA
    ' ---------------------------------------------------------

    Private Sub OnViewLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.DesignMode Then Return
        AppSession.TryDevSignIn(True)
        Try
            LoadBranches()
            LoadAll()
        Catch ex As Exception
            lblStatus.Text = ex.Message
            UiTheme.ShowError(Me, ex)
        End Try
    End Sub

    Private Sub SafeRun(work As Action)
        Try
            work()
        Catch ex As Exception
            UiTheme.ShowError(Me, ex)
        End Try
    End Sub

    Private Sub LoadBranches()
        Dim branches As DataTable = InventoryService.GetBranches()
        _loadingBranches = True
        Try
            cboBranch.Items.Clear()
            cboBranch.Items.Add(New ChoiceItem("All branches (shown separately)", Nothing))
            For Each r As DataRow In branches.Rows
                cboBranch.Items.Add(New ChoiceItem(Convert.ToString(r("BranchName")), Convert.ToInt32(r("BranchID"))))
            Next
            cboBranch.SelectedIndex = 0
        Finally
            _loadingBranches = False
        End Try
    End Sub

    Private Function SelectedBranchId() As Integer?
        Dim item As ChoiceItem = TryCast(cboBranch.SelectedItem, ChoiceItem)
        If item Is Nothing OrElse item.Value Is Nothing Then Return Nothing
        Return Convert.ToInt32(item.Value)
    End Function

    Private Function FilterValue(combo As ComboBox) As String
        Dim item As ChoiceItem = TryCast(combo.SelectedItem, ChoiceItem)
        If item Is Nothing OrElse item.Value Is Nothing Then Return Nothing
        Return Convert.ToString(item.Value)
    End Function

    Private Sub LoadAll()
        Try
            Dim user As SessionUser = Authorization.RequireSuperAdmin()
            lblStatus.Text = "Signed in as " & user.FullName & " (Super Admin, read-only)"
            LoadStock()
            LoadCompare()
            LoadMovements()
            LoadChecklists()
        Catch ex As Exception
            lblStatus.Text = ex.Message
            UiTheme.ShowError(Me, ex)
        End Try
    End Sub

    Private Sub LoadStock()
        Dim dt As DataTable = InventoryService.GetBalancePivot(SelectedBranchId())
        gridStock.DataSource = dt
        gridStock.Columns("ProductID").Visible = False
        gridStock.Columns("BranchName").HeaderText = "Branch"
        gridStock.Columns("BranchName").FillWeight = 90
        gridStock.Columns("Product").FillWeight = 190
        gridStock.Columns("Revolving").HeaderText = "Revolving (field)"
        gridStock.Columns("General").HeaderText = "Accessories"
        gridStock.Columns("TotalCylinders").HeaderText = "Total cylinders"
        For Each name As String In New String() {"Filled", "Empty", "Revolving", "Damaged", "General", "TotalCylinders"}
            gridStock.Columns(name).DefaultCellStyle.NullValue = "-"
        Next
        UiTheme.RightAlign(gridStock, "Filled", "Empty", "Revolving", "Damaged", "General", "TotalCylinders")
    End Sub

    Private Sub LoadCompare()
        Dim dt As DataTable = InventoryService.GetComparison()
        gridCompare.DataSource = dt
        gridCompare.Columns("Product").FillWeight = 190
        For i As Integer = 2 To gridCompare.Columns.Count - 1
            UiTheme.RightAlign(gridCompare, gridCompare.Columns(i).Name)
        Next
        gridCompare.Columns(gridCompare.Columns.Count - 1).DefaultCellStyle.Font = UiTheme.BodyFont(9.5F, True)
    End Sub

    Private Sub LoadMovements()
        Dim dt As DataTable = InventoryService.GetMovements(
            SelectedBranchId(), FilterValue(cboMoveType), dtpMoveFrom.Value.Date, dtpMoveTo.Value.Date)
        gridMovements.DataSource = dt
        gridMovements.Columns("MovementID").Visible = False
        gridMovements.Columns("BranchName").HeaderText = "Branch"
        gridMovements.Columns("BranchName").FillWeight = 80
        gridMovements.Columns("MovementDate").HeaderText = "Date and time"
        gridMovements.Columns("MovementDate").DefaultCellStyle.Format = "dd MMM yyyy HH:mm"
        gridMovements.Columns("MovementDate").FillWeight = 130
        gridMovements.Columns("ProductName").HeaderText = "Product"
        gridMovements.Columns("ProductName").FillWeight = 140
        gridMovements.Columns("StockCondition").HeaderText = "Condition"
        gridMovements.Columns("MovementType").HeaderText = "Type"
        gridMovements.Columns("QuantityIn").HeaderText = "In"
        gridMovements.Columns("QuantityOut").HeaderText = "Out"
        gridMovements.Columns("PreviousBalance").HeaderText = "Previous"
        gridMovements.Columns("NewBalance").HeaderText = "New balance"
        gridMovements.Columns("Description").HeaderText = "Reason"
        gridMovements.Columns("Description").FillWeight = 220
        gridMovements.Columns("RecordedByName").HeaderText = "Recorded by"
        UiTheme.RightAlign(gridMovements, "QuantityIn", "QuantityOut", "PreviousBalance", "NewBalance")
    End Sub

    Private Sub LoadChecklists()
        Dim dt As DataTable = ChecklistService.GetSubmissions(
            SelectedBranchId(), FilterValue(cboCheckType), dtpCheckFrom.Value.Date, dtpCheckTo.Value.Date)
        gridChecklists.DataSource = dt
        gridChecklists.Columns("ChecklistID").Visible = False
        gridChecklists.Columns("BranchName").HeaderText = "Branch"
        gridChecklists.Columns("ChecklistType").HeaderText = "Type"
        gridChecklists.Columns("ChecklistDate").HeaderText = "Checklist date"
        gridChecklists.Columns("ChecklistDate").DefaultCellStyle.Format = "dd MMM yyyy"
        gridChecklists.Columns("SubmittedByName").HeaderText = "Submitted by"
        gridChecklists.Columns("SubmissionDate").HeaderText = "Submitted at"
        gridChecklists.Columns("SubmissionDate").DefaultCellStyle.Format = "dd MMM yyyy HH:mm"
        gridChecklists.Columns("Discrepancies").HeaderText = "Items with difference"
        gridChecklists.Columns("AdjustedCount").HeaderText = "Adjusted"
        UiTheme.RightAlign(gridChecklists, "Discrepancies", "AdjustedCount")
        ShowChecklistLines()
    End Sub

    Private Sub OnChecklistSelected(sender As Object, e As EventArgs)
        SafeRun(AddressOf ShowChecklistLines)
    End Sub

    Private Sub ShowChecklistLines()
        If gridChecklists.CurrentRow Is Nothing OrElse gridChecklists.Columns.Count = 0 Then
            gridLines.DataSource = Nothing
            Return
        End If
        Dim idCell As Object = gridChecklists.CurrentRow.Cells("ChecklistID").Value
        If idCell Is Nothing Then
            gridLines.DataSource = Nothing
            Return
        End If

        Dim lines As List(Of ChecklistLine) = ChecklistService.GetChecklistLines(Convert.ToInt32(idCell))
        Dim table As New DataTable()
        table.Columns.Add("Product", GetType(String))
        table.Columns.Add("Condition", GetType(String))
        table.Columns.Add("Previous closing", GetType(String))
        table.Columns.Add("System", GetType(Integer))
        table.Columns.Add("Physical", GetType(Integer))
        table.Columns.Add("Difference", GetType(Integer))
        table.Columns.Add("Remarks", GetType(String))
        table.Columns.Add("Adjustment", GetType(String))
        For Each l As ChecklistLine In lines
            Dim r As DataRow = table.NewRow()
            r("Product") = l.ProductName
            r("Condition") = StockConditions.DisplayName(l.StockCondition)
            r("Previous closing") = If(l.PreviousClosingQuantity.HasValue, l.PreviousClosingQuantity.Value.ToString("N0"), "-")
            r("System") = l.SystemQuantity
            If l.PhysicalQuantity.HasValue Then
                r("Physical") = l.PhysicalQuantity.Value
                r("Difference") = l.Difference.Value
            End If
            r("Remarks") = l.Remarks
            If l.Difference.HasValue AndAlso l.Difference.Value <> 0 Then
                r("Adjustment") = If(l.Adjusted, "Adjusted", "Not adjusted")
            Else
                r("Adjustment") = "-"
            End If
            table.Rows.Add(r)
        Next
        gridLines.DataSource = table
        gridLines.Columns("Product").FillWeight = 180
        gridLines.Columns("Remarks").FillWeight = 220
        UiTheme.RightAlign(gridLines, "Previous closing", "System", "Physical", "Difference")
    End Sub

    Private Sub OnCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.Value Is Nothing OrElse e.ColumnIndex < 0 Then Return
        Dim g As DataGridView = DirectCast(sender, DataGridView)
        Dim name As String = g.Columns(e.ColumnIndex).Name
        If name = "StockCondition" Then
            e.Value = StockConditions.DisplayName(Convert.ToString(e.Value))
            e.FormattingApplied = True
        ElseIf name = "MovementType" OrElse name = "ChecklistType" Then
            Dim text As String = Convert.ToString(e.Value).ToLowerInvariant()
            If text.Length > 0 Then
                e.Value = Char.ToUpperInvariant(text(0)) & text.Substring(1)
                e.FormattingApplied = True
            End If
        End If
    End Sub

End Class

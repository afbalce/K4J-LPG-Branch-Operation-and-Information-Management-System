Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

' ==========================================================
' BRANCH ADMIN INVENTORY SCREEN
' Shows the branch's stock balances (filled, empty, revolving,
' damaged tracked separately), lets the admin record incoming,
' outgoing and adjustment movements, and shows movement history.
' This class only draws the screen. Rules live in InventoryService.
' ==========================================================

Public Class BranchInventoryView
    Inherits UserControl

    Private tabs As TabControl
    Private gridBalances As StyledDataGridView
    Private gridMovements As StyledDataGridView
    Private lblStatus As Label
    Private lblFilled As Label
    Private lblEmpty As Label
    Private lblRevolving As Label
    Private lblDamaged As Label
    Private btnIn As Button
    Private btnOut As Button
    Private btnAdjust As Button
    Private btnRefresh As Button
    Private dtpFrom As DateTimePicker
    Private dtpTo As DateTimePicker
    Private cboType As ComboBox

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
        ' Added first = docks last (fills what is left). Top panels are added after it.
        tabs = New TabControl()
        tabs.Dock = DockStyle.Fill
        tabs.Font = UiTheme.BodyFont()

        Dim pageBalances As New TabPage("Stock balances")
        pageBalances.BackColor = Color.White
        gridBalances = New StyledDataGridView()
        gridBalances.Dock = DockStyle.Fill
        gridBalances.ReadOnly = True
        UiTheme.StyleGrid(gridBalances)
        pageBalances.Controls.Add(gridBalances)

        Dim pageMovements As New TabPage("Movement history")
        pageMovements.BackColor = Color.White
        gridMovements = New StyledDataGridView()
        gridMovements.Dock = DockStyle.Fill
        gridMovements.ReadOnly = True
        UiTheme.StyleGrid(gridMovements)
        AddHandler gridMovements.CellFormatting, AddressOf OnMovementCellFormatting
        pageMovements.Controls.Add(gridMovements)
        pageMovements.Controls.Add(BuildMovementFilters())

        tabs.TabPages.Add(pageBalances)
        tabs.TabPages.Add(pageMovements)
        Me.Controls.Add(tabs)

        lblStatus = UiTheme.MakeLabel("", 9.0F, False, True)
        lblStatus.Dock = DockStyle.Bottom
        lblStatus.AutoSize = False
        lblStatus.Height = 24
        lblStatus.TextAlign = ContentAlignment.MiddleLeft
        Me.Controls.Add(lblStatus)

        Dim buttons As New FlowLayoutPanel()
        buttons.Dock = DockStyle.Top
        buttons.Height = 50
        buttons.Padding = New Padding(0, 8, 0, 8)
        btnIn = UiTheme.MakeButton("Stock in", True)
        btnOut = UiTheme.MakeButton("Stock out", False)
        btnAdjust = UiTheme.MakeButton("Adjust balance", False)
        btnRefresh = UiTheme.MakeButton("Refresh", False)
        AddHandler btnIn.Click, Sub(s, e) OpenDialog(MovementTypes.Incoming)
        AddHandler btnOut.Click, Sub(s, e) OpenDialog(MovementTypes.Outgoing)
        AddHandler btnAdjust.Click, Sub(s, e) OpenDialog(MovementTypes.Adjustment)
        AddHandler btnRefresh.Click, Sub(s, e) LoadAll()
        buttons.Controls.AddRange(New Control() {btnIn, btnOut, btnAdjust, btnRefresh})
        Me.Controls.Add(buttons)

        Dim cards As New FlowLayoutPanel()
        cards.Dock = DockStyle.Top
        cards.Height = 80
        cards.Padding = New Padding(0, 4, 0, 8)
        cards.Controls.Add(MakeCard("Filled cylinders", lblFilled))
        cards.Controls.Add(MakeCard("Empty cylinders", lblEmpty))
        cards.Controls.Add(MakeCard("Revolving (in the field)", lblRevolving))
        cards.Controls.Add(MakeCard("Damaged cylinders", lblDamaged))
        Me.Controls.Add(cards)
    End Sub

    Private Function MakeCard(title As String, ByRef valueLabel As Label) As Panel
        Dim card As New Panel()
        card.Size = New Size(200, 66)
        card.BackColor = Color.White
        card.Margin = New Padding(0, 0, 12, 0)
        Dim caption As Label = UiTheme.MakeLabel(title, 8.5F, False, True)
        caption.Location = New Point(12, 8)
        valueLabel = UiTheme.MakeLabel("0", 18.0F, True)
        valueLabel.Location = New Point(12, 28)
        card.Controls.Add(caption)
        card.Controls.Add(valueLabel)
        Return card
    End Function

    Private Function BuildMovementFilters() As Panel
        Dim panel As New FlowLayoutPanel()
        panel.Dock = DockStyle.Top
        panel.Height = 48
        panel.Padding = New Padding(8, 8, 8, 4)
        panel.BackColor = Color.White

        dtpFrom = New DateTimePicker()
        dtpFrom.Format = DateTimePickerFormat.Short
        dtpFrom.Width = 110
        dtpFrom.Value = Date.Today.AddDays(-30)
        dtpTo = New DateTimePicker()
        dtpTo.Format = DateTimePickerFormat.Short
        dtpTo.Width = 110
        dtpTo.Value = Date.Today

        cboType = New ComboBox()
        cboType.DropDownStyle = ComboBoxStyle.DropDownList
        cboType.Width = 130
        cboType.Items.Add(New ChoiceItem("All movements", Nothing))
        cboType.Items.Add(New ChoiceItem("Incoming", MovementTypes.Incoming))
        cboType.Items.Add(New ChoiceItem("Outgoing", MovementTypes.Outgoing))
        cboType.Items.Add(New ChoiceItem("Adjustments", MovementTypes.Adjustment))
        cboType.SelectedIndex = 0

        Dim apply As Button = UiTheme.MakeButton("Apply", False)
        apply.MinimumSize = New Size(80, 28)
        AddHandler apply.Click, Sub(s, e) LoadMovements()

        panel.Controls.Add(UiTheme.MakeLabel("From", 9.0F, True))
        panel.Controls.Add(dtpFrom)
        panel.Controls.Add(UiTheme.MakeLabel("To", 9.0F, True))
        panel.Controls.Add(dtpTo)
        panel.Controls.Add(cboType)
        panel.Controls.Add(apply)
        For Each c As Control In panel.Controls
            If TypeOf c Is Label Then c.Margin = New Padding(6, 6, 4, 0)
        Next
        Return panel
    End Function

    ' ---------------------------------------------------------
    ' DATA
    ' ---------------------------------------------------------

    Private Sub OnViewLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.DesignMode Then Return
        AppSession.TryDevSignIn(False)
        LoadAll()
    End Sub

    Private Sub LoadAll()
        Try
            Dim user As SessionUser = Authorization.RequireBranchAdmin()
            lblStatus.Text = "Branch: " & user.BranchName & "   |   Signed in as " & user.FullName
            LoadBalances()
            LoadMovements()
            SetActionsEnabled(True)
        Catch ex As Exception
            SetActionsEnabled(False)
            lblStatus.Text = ex.Message
            UiTheme.ShowError(Me, ex)
        End Try
    End Sub

    Private Sub SetActionsEnabled(enabled As Boolean)
        btnIn.Enabled = enabled
        btnOut.Enabled = enabled
        btnAdjust.Enabled = enabled
    End Sub

    Private Sub LoadBalances()
        Dim dt As DataTable = InventoryService.GetBalancePivot(Nothing)
        gridBalances.DataSource = dt

        gridBalances.Columns("BranchName").Visible = False
        gridBalances.Columns("ProductID").Visible = False
        gridBalances.Columns("Product").HeaderText = "Product"
        gridBalances.Columns("Product").FillWeight = 190
        gridBalances.Columns("Revolving").HeaderText = "Revolving (field)"
        gridBalances.Columns("General").HeaderText = "Accessories"
        gridBalances.Columns("TotalCylinders").HeaderText = "Total cylinders"

        For Each name As String In New String() {"Filled", "Empty", "Revolving", "Damaged", "General", "TotalCylinders"}
            gridBalances.Columns(name).DefaultCellStyle.NullValue = "-"
        Next
        UiTheme.RightAlign(gridBalances, "Filled", "Empty", "Revolving", "Damaged", "General", "TotalCylinders")

        lblFilled.Text = SumOf(dt, "Filled")
        lblEmpty.Text = SumOf(dt, "Empty")
        lblRevolving.Text = SumOf(dt, "Revolving")
        lblDamaged.Text = SumOf(dt, "Damaged")
    End Sub

    Private Function SumOf(dt As DataTable, column As String) As String
        Dim total As Integer = 0
        For Each r As DataRow In dt.Rows
            If Not r.IsNull(column) Then total += Convert.ToInt32(r(column))
        Next
        Return total.ToString("N0")
    End Function

    Private Sub LoadMovements()
        Dim selectedType As ChoiceItem = TryCast(cboType.SelectedItem, ChoiceItem)
        Dim typeValue As String = Nothing
        If selectedType IsNot Nothing AndAlso selectedType.Value IsNot Nothing Then
            typeValue = Convert.ToString(selectedType.Value)
        End If

        Dim dt As DataTable = InventoryService.GetMovements(
            Nothing, typeValue, dtpFrom.Value.Date, dtpTo.Value.Date)
        gridMovements.DataSource = dt

        gridMovements.Columns("MovementID").Visible = False
        gridMovements.Columns("BranchName").Visible = False
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

    Private Sub OnMovementCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.Value Is Nothing OrElse e.ColumnIndex < 0 Then Return
        Dim name As String = gridMovements.Columns(e.ColumnIndex).Name
        If name = "StockCondition" Then
            e.Value = StockConditions.DisplayName(Convert.ToString(e.Value))
            e.FormattingApplied = True
        ElseIf name = "MovementType" Then
            Dim text As String = Convert.ToString(e.Value).ToLowerInvariant()
            e.Value = Char.ToUpperInvariant(text(0)) & text.Substring(1)
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub OpenDialog(movementType As String)
        Try
            Dim products As List(Of ProductInfo) = InventoryService.GetProducts()
            Using dlg As New StockMovementDialog(movementType, products)
                If dlg.ShowDialog(Me.FindForm()) = DialogResult.OK Then
                    LoadAll()
                    lblStatus.Text = "Saved. " & lblStatus.Text
                End If
            End Using
        Catch ex As Exception
            UiTheme.ShowError(Me, ex)
        End Try
    End Sub

End Class

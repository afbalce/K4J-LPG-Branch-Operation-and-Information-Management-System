Imports System.Drawing
Imports System.Windows.Forms

' ==========================================================
' DIALOG: STOCK IN / STOCK OUT / ADJUSTMENT (Branch Admin)
' The dialog only collects input and calls InventoryService.
' All rules (branch, stock level, reason) are enforced there.
' ==========================================================

Public Class StockMovementDialog
    Inherits Form

    Private ReadOnly _mode As String
    Private ReadOnly _products As List(Of ProductInfo)

    Private cboProduct As ComboBox
    Private cboCondition As ComboBox
    Private lblCurrent As Label
    Private lblQuantity As Label
    Private nudQuantity As NumericUpDown
    Private txtReason As TextBox
    Private btnSave As Button
    Private btnCancel As Button

    Public Sub New(mode As String, products As List(Of ProductInfo))
        _mode = mode
        _products = products
        BuildUi()
        LoadProducts()
    End Sub

    Private Sub BuildUi()
        Dim title As String
        Select Case _mode
            Case MovementTypes.Incoming
                title = "Record incoming stock"
            Case MovementTypes.Outgoing
                title = "Record outgoing stock"
            Case Else
                title = "Adjust stock balance"
        End Select

        Me.Text = title
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowInTaskbar = False
        Me.ClientSize = New Size(440, 380)
        Me.BackColor = Color.White
        Me.Font = UiTheme.BodyFont()

        Dim heading As Label = UiTheme.MakeLabel(title, 13.0F, True)
        heading.Location = New Point(20, 16)
        Me.Controls.Add(heading)

        Dim y As Integer = 58
        AddCaption("Product", y)
        cboProduct = New ComboBox()
        cboProduct.DropDownStyle = ComboBoxStyle.DropDownList
        cboProduct.SetBounds(20, y + 22, 400, 28)
        AddHandler cboProduct.SelectedIndexChanged, AddressOf OnProductChanged
        Me.Controls.Add(cboProduct)

        y += 62
        AddCaption("Stock condition", y)
        cboCondition = New ComboBox()
        cboCondition.DropDownStyle = ComboBoxStyle.DropDownList
        cboCondition.SetBounds(20, y + 22, 400, 28)
        AddHandler cboCondition.SelectedIndexChanged, AddressOf OnConditionChanged
        Me.Controls.Add(cboCondition)

        y += 62
        lblCurrent = UiTheme.MakeLabel("Current balance: -", 9.5F, False, True)
        lblCurrent.Location = New Point(20, y)
        Me.Controls.Add(lblCurrent)

        lblQuantity = UiTheme.MakeLabel(If(_mode = MovementTypes.Adjustment, "New balance", "Quantity"), 9.0F, True)
        lblQuantity.Location = New Point(20, y + 26)
        Me.Controls.Add(lblQuantity)

        nudQuantity = New NumericUpDown()
        nudQuantity.Minimum = If(_mode = MovementTypes.Adjustment, 0D, 1D)
        nudQuantity.Maximum = 1000000D
        nudQuantity.Value = nudQuantity.Minimum
        nudQuantity.SetBounds(20, y + 48, 160, 28)
        Me.Controls.Add(nudQuantity)

        y += 88
        AddCaption(If(_mode = MovementTypes.Adjustment, "Reason for adjustment (required)", "Reason or reference (required)"), y)
        txtReason = New TextBox()
        txtReason.Multiline = True
        txtReason.MaxLength = 200
        txtReason.SetBounds(20, y + 22, 400, 52)
        Me.Controls.Add(txtReason)

        btnSave = UiTheme.MakeButton("Save", True)
        btnSave.SetBounds(228, 332, 100, 34)
        AddHandler btnSave.Click, AddressOf OnSave
        Me.Controls.Add(btnSave)

        btnCancel = UiTheme.MakeButton("Cancel", False)
        btnCancel.SetBounds(336, 332, 84, 34)
        btnCancel.DialogResult = DialogResult.Cancel
        Me.Controls.Add(btnCancel)
        Me.CancelButton = btnCancel
    End Sub

    Private Sub AddCaption(text As String, y As Integer)
        Dim l As Label = UiTheme.MakeLabel(text, 9.0F, True)
        l.Location = New Point(20, y)
        Me.Controls.Add(l)
    End Sub

    Private Sub LoadProducts()
        cboProduct.Items.Clear()
        For Each p As ProductInfo In _products
            cboProduct.Items.Add(New ChoiceItem(p.ProductName, p))
        Next
        If cboProduct.Items.Count > 0 Then cboProduct.SelectedIndex = 0
    End Sub

    Private Function SelectedProduct() As ProductInfo
        Dim item As ChoiceItem = TryCast(cboProduct.SelectedItem, ChoiceItem)
        If item Is Nothing Then Return Nothing
        Return DirectCast(item.Value, ProductInfo)
    End Function

    Private Function SelectedCondition() As String
        Dim item As ChoiceItem = TryCast(cboCondition.SelectedItem, ChoiceItem)
        If item Is Nothing Then Return Nothing
        Return Convert.ToString(item.Value)
    End Function

    Private Sub OnProductChanged(sender As Object, e As EventArgs)
        Dim product As ProductInfo = SelectedProduct()
        cboCondition.Items.Clear()
        If product Is Nothing Then Return
        For Each c As String In StockConditions.ForCategory(product.Category)
            cboCondition.Items.Add(New ChoiceItem(StockConditions.DisplayName(c), c))
        Next
        If cboCondition.Items.Count > 0 Then cboCondition.SelectedIndex = 0
    End Sub

    Private Sub OnConditionChanged(sender As Object, e As EventArgs)
        Dim product As ProductInfo = SelectedProduct()
        Dim condition As String = SelectedCondition()
        If product Is Nothing OrElse condition Is Nothing Then Return
        Try
            Dim balance As Integer = InventoryService.GetCurrentBalance(product.ProductID, condition)
            lblCurrent.Text = "Current balance: " & balance.ToString("N0")
            If _mode = MovementTypes.Adjustment Then
                nudQuantity.Value = Math.Min(nudQuantity.Maximum, Math.Max(nudQuantity.Minimum, balance))
            End If
        Catch ex As Exception
            lblCurrent.Text = "Current balance: unavailable"
        End Try
    End Sub

    Private Sub OnSave(sender As Object, e As EventArgs)
        Dim product As ProductInfo = SelectedProduct()
        Dim condition As String = SelectedCondition()
        If product Is Nothing OrElse condition Is Nothing Then
            MessageBox.Show(Me, "Choose a product and a stock condition.", "Missing information",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim quantity As Integer = Convert.ToInt32(nudQuantity.Value)
        Try
            Select Case _mode
                Case MovementTypes.Incoming
                    InventoryService.RecordIncoming(product.ProductID, condition, quantity, txtReason.Text)
                Case MovementTypes.Outgoing
                    InventoryService.RecordOutgoing(product.ProductID, condition, quantity, txtReason.Text)
                Case Else
                    InventoryService.RecordAdjustment(product.ProductID, condition, quantity, txtReason.Text)
            End Select
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            UiTheme.ShowError(Me, ex)
        End Try
    End Sub

End Class

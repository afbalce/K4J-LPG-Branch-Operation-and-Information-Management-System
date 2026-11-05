Imports System.Drawing
Imports System.Windows.Forms

' ==========================================================
' DIALOG: ADJUST STOCK FOR ONE CHECKLIST DISCREPANCY
' The balance is only changed after the Branch Admin confirms
' and gives a reason. The reason and the user are recorded.
' ==========================================================

Public Class ChecklistAdjustmentDialog
    Inherits Form

    Private ReadOnly _line As ChecklistLine
    Private txtReason As TextBox

    Public Sub New(line As ChecklistLine)
        _line = line
        BuildUi()
    End Sub

    Private Sub BuildUi()
        Me.Text = "Adjust stock for discrepancy"
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowInTaskbar = False
        Me.ClientSize = New Size(440, 300)
        Me.BackColor = Color.White
        Me.Font = UiTheme.BodyFont()

        Dim heading As Label = UiTheme.MakeLabel("Adjust stock for discrepancy", 13.0F, True)
        heading.Location = New Point(20, 16)
        Me.Controls.Add(heading)

        Dim diff As Integer = _line.Difference.Value
        Dim direction As String = If(diff < 0, "missing", "excess")
        Dim info As Label = UiTheme.MakeLabel(
            _line.ProductName & " - " & StockConditions.DisplayName(_line.StockCondition) & vbCrLf &
            "System " & _line.SystemQuantity.ToString("N0") & ", counted " & _line.PhysicalQuantity.Value.ToString("N0") &
            " (" & If(diff > 0, "+", "") & diff.ToString("N0") & " " & direction & ")")
        info.Location = New Point(20, 56)
        Me.Controls.Add(info)

        Dim note As Label = UiTheme.MakeLabel(
            "Saving applies the difference to the current balance and records your name and reason.", 9.0F, False, True)
        note.MaximumSize = New Size(400, 0)
        note.Location = New Point(20, 104)
        Me.Controls.Add(note)

        Dim caption As Label = UiTheme.MakeLabel("Reason for adjustment (required)", 9.0F, True)
        caption.Location = New Point(20, 150)
        Me.Controls.Add(caption)

        txtReason = New TextBox()
        txtReason.Multiline = True
        txtReason.MaxLength = 170
        txtReason.SetBounds(20, 172, 400, 56)
        Me.Controls.Add(txtReason)

        Dim btnSave As Button = UiTheme.MakeButton("Adjust stock", True)
        btnSave.SetBounds(200, 248, 130, 34)
        AddHandler btnSave.Click, AddressOf OnSave
        Me.Controls.Add(btnSave)

        Dim btnCancel As Button = UiTheme.MakeButton("Cancel", False)
        btnCancel.SetBounds(338, 248, 82, 34)
        btnCancel.DialogResult = DialogResult.Cancel
        Me.Controls.Add(btnCancel)
        Me.CancelButton = btnCancel
    End Sub

    Private Sub OnSave(sender As Object, e As EventArgs)
        Try
            InventoryService.AdjustFromChecklistLine(_line.DetailID, txtReason.Text)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            UiTheme.ShowError(Me, ex)
        End Try
    End Sub

End Class

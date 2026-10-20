<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsGeneralPanel
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        RoundedTitlePanel1 = New RoundedTitlePanel()
        RoundedTitlePanel2 = New RoundedTitlePanel()
        SuspendLayout()
        ' 
        ' RoundedTitlePanel1
        ' 
        RoundedTitlePanel1.BackColor = Color.Transparent
        RoundedTitlePanel1.BorderColor = Color.FromArgb(CByte(223), CByte(149), CByte(63))
        RoundedTitlePanel1.FillColor = Color.FromArgb(CByte(245), CByte(239), CByte(232))
        RoundedTitlePanel1.Location = New Point(13, 16)
        RoundedTitlePanel1.Name = "RoundedTitlePanel1"
        RoundedTitlePanel1.Size = New Size(742, 256)
        RoundedTitlePanel1.TabIndex = 0
        ' 
        ' RoundedTitlePanel2
        ' 
        RoundedTitlePanel2.BackColor = Color.Transparent
        RoundedTitlePanel2.BorderColor = Color.FromArgb(CByte(223), CByte(149), CByte(63))
        RoundedTitlePanel2.FillColor = Color.FromArgb(CByte(245), CByte(239), CByte(232))
        RoundedTitlePanel2.Location = New Point(13, 338)
        RoundedTitlePanel2.Name = "RoundedTitlePanel2"
        RoundedTitlePanel2.Size = New Size(742, 225)
        RoundedTitlePanel2.TabIndex = 1
        ' 
        ' SettingsGeneralPanel
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(RoundedTitlePanel2)
        Controls.Add(RoundedTitlePanel1)
        Name = "SettingsGeneralPanel"
        Size = New Size(770, 586)
        ResumeLayout(False)
    End Sub

    Friend WithEvents RoundedTitlePanel1 As RoundedTitlePanel
    Friend WithEvents RoundedTitlePanel2 As RoundedTitlePanel

End Class

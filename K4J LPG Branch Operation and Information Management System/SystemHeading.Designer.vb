<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SystemHeading
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
        panelLabel = New Label()
        panelDesc = New Label()
        SuspendLayout()
        ' 
        ' panelLabel
        ' 
        panelLabel.AutoSize = True
        panelLabel.Font = New Font("Barlow Condensed", 12F, FontStyle.Bold)
        panelLabel.Location = New Point(34, 6)
        panelLabel.Name = "panelLabel"
        panelLabel.Size = New Size(117, 33)
        panelLabel.TabIndex = 0
        panelLabel.Text = "Panel Name"
        ' 
        ' panelDesc
        ' 
        panelDesc.AutoSize = True
        panelDesc.Font = New Font("Inter", 5F)
        panelDesc.ForeColor = Color.FromArgb(CByte(153), CByte(161), CByte(175))
        panelDesc.Location = New Point(34, 35)
        panelDesc.Name = "panelDesc"
        panelDesc.Size = New Size(90, 12)
        panelDesc.TabIndex = 1
        panelDesc.Text = "Panel Description "
        ' 
        ' SystemHeading
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(240), CByte(243), CByte(247))
        Controls.Add(panelDesc)
        Controls.Add(panelLabel)
        Name = "SystemHeading"
        Size = New Size(924, 60)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents panelLabel As Label
    Friend WithEvents panelDesc As Label

End Class

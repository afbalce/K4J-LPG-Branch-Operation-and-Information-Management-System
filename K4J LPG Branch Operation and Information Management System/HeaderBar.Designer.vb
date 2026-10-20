<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HeaderBar
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
        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(HeaderBar))
        headerClockIcon = New PictureBox()
        headerNotificationIcon = New PictureBox()
        roleHeadingLabel = New Label()
        panelHeadingLabel = New Label()
        dateTimePlaceholder = New RoundedTitlePanel()
        DateandTime = New Label()
        dateTimePlaceholder.SuspendLayout()
        SuspendLayout()
        ' 
        ' roleHeadingLabel
        ' 
        roleHeadingLabel.AutoSize = True
        roleHeadingLabel.Font = New Font("Inter", 7F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        roleHeadingLabel.ForeColor = Color.FromArgb(CByte(153), CByte(161), CByte(175))
        roleHeadingLabel.Location = New Point(39, 20)
        roleHeadingLabel.Name = "roleHeadingLabel"
        roleHeadingLabel.Size = New Size(83, 17)
        roleHeadingLabel.TabIndex = 0
        roleHeadingLabel.Text = "User Role  /"
        ' 
        ' panelHeadingLabel
        ' 
        panelHeadingLabel.AutoSize = True
        panelHeadingLabel.Font = New Font("Inter SemiBold", 7.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        panelHeadingLabel.ForeColor = Color.FromArgb(CByte(54), CByte(65), CByte(83))
        panelHeadingLabel.Location = New Point(128, 20)
        panelHeadingLabel.Name = "panelHeadingLabel"
        panelHeadingLabel.Size = New Size(90, 17)
        panelHeadingLabel.TabIndex = 1
        panelHeadingLabel.Text = "Panel Name"
        ' 
        ' dateTimePlaceholder
        ' 
        dateTimePlaceholder.BackColor = Color.Transparent
        dateTimePlaceholder.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        dateTimePlaceholder.Controls.Add(DateandTime)
        dateTimePlaceholder.Controls.Add(headerClockIcon)
        dateTimePlaceholder.CornerRadius = 10
        dateTimePlaceholder.FillColor = Color.FromArgb(CByte(249), CByte(250), CByte(251))
        dateTimePlaceholder.Location = New Point(630, 10)
        dateTimePlaceholder.Name = "dateTimePlaceholder"
        dateTimePlaceholder.Size = New Size(229, 32)
        dateTimePlaceholder.TabIndex = 2
        ' 
        ' DateandTime
        ' 
        DateandTime.AutoSize = True
        DateandTime.Font = New Font("DM Mono", 7F)
        DateandTime.Location = New Point(36, 7)
        DateandTime.Name = "DateandTime"
        DateandTime.Size = New Size(184, 18)
        DateandTime.TabIndex = 0
        DateandTime.Text = "Oct 16, 2026 | 2:41 AM"
        '
        ' Header icons
        '
        headerClockIcon.Name = "headerClockIcon"
        headerClockIcon.Image = CType(resources.GetObject("headerClockIcon.Image"), Image)
        headerClockIcon.BackColor = Color.Transparent
        headerClockIcon.SizeMode = PictureBoxSizeMode.Zoom
        headerClockIcon.Location = New Point(10, 8)
        headerClockIcon.Size = New Size(16, 16)
        headerClockIcon.TabStop = False
        headerNotificationIcon.Name = "headerNotificationIcon"
        headerNotificationIcon.Image = CType(resources.GetObject("headerNotificationIcon.Image"), Image)
        headerNotificationIcon.BackColor = Color.Transparent
        headerNotificationIcon.SizeMode = PictureBoxSizeMode.Zoom
        headerNotificationIcon.Location = New Point(882, 15)
        headerNotificationIcon.Size = New Size(20, 20)
        headerNotificationIcon.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        headerNotificationIcon.Cursor = Cursors.Hand
        headerNotificationIcon.TabStop = False
        ' 
        ' HeaderBar
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(dateTimePlaceholder)
        Controls.Add(headerNotificationIcon)
        Controls.Add(panelHeadingLabel)
        Controls.Add(roleHeadingLabel)
        Name = "HeaderBar"
        Size = New Size(924, 50)
        dateTimePlaceholder.ResumeLayout(False)
        dateTimePlaceholder.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents roleHeadingLabel As Label
    Friend WithEvents panelHeadingLabel As Label
    Friend WithEvents dateTimePlaceholder As RoundedTitlePanel
    Friend WithEvents DateandTime As Label
    Friend WithEvents headerClockIcon As PictureBox
    Friend WithEvents headerNotificationIcon As PictureBox

End Class

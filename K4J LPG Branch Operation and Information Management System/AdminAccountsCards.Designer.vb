<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminAccountsCards
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
        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(AdminAccountsCards))
        totalAdminCard = New RoundedTitlePanel()
        activeCard = New RoundedTitlePanel()
        inactiveCard = New RoundedTitlePanel()
        totalAdminLabel = New SpacedLabel()
        activeLabel = New SpacedLabel()
        inactiveLabel = New SpacedLabel()
        totalAdminNumber = New Label()
        activeAdminNumber = New Label()
        inactiveAdminNumber = New Label()
        totalAdminIconPlaceholder = New RoundedTitlePanel()
        totalAdminIconPlaceholderImage = New PictureBox()
        activeIconPlaceholder = New RoundedTitlePanel()
        activeIconPlaceholderImage = New PictureBox()
        inactiveIconPlaceholder = New RoundedTitlePanel()
        inactiveIconPlaceholderImage = New PictureBox()
        totalAdminCard.SuspendLayout()
        activeCard.SuspendLayout()
        inactiveCard.SuspendLayout()
        SuspendLayout()
        ' 
        ' totalAdminCard
        ' 
        totalAdminCard.BackColor = Color.Transparent
        totalAdminCard.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        totalAdminCard.BorderOpacity = 80
        totalAdminCard.Controls.Add(totalAdminNumber)
        totalAdminCard.Controls.Add(totalAdminLabel)
        totalAdminCard.FillColor = Color.White
        totalAdminCard.ForeColor = Color.Coral
        totalAdminCard.Location = New Point(5, 5)
        totalAdminCard.Name = "totalAdminCard"
        totalAdminCard.Size = New Size(300, 95)
        totalAdminCard.TabIndex = 0
        ' 
        ' activeCard
        ' 
        activeCard.BackColor = Color.Transparent
        activeCard.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        activeCard.BorderOpacity = 80
        inactiveCard.Controls.Add(inactiveAdminNumber)
        activeCard.Controls.Add(activeAdminNumber)
        activeCard.Controls.Add(activeLabel)
        activeCard.FillColor = Color.White
        activeCard.Location = New Point(312, 5)
        activeCard.Name = "activeCard"
        activeCard.Size = New Size(300, 95)
        activeCard.TabIndex = 1
        ' 
        ' inactiveCard
        ' 
        inactiveCard.BackColor = Color.Transparent
        inactiveCard.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        inactiveCard.BorderOpacity = 80
        inactiveCard.Controls.Add(inactiveLabel)
        inactiveCard.FillColor = Color.White
        inactiveCard.Location = New Point(621, 5)
        inactiveCard.Name = "inactiveCard"
        inactiveCard.Size = New Size(300, 95)
        inactiveCard.TabIndex = 1
        ' 
        ' totalAdminLabel
        ' 
        totalAdminLabel.AutoSize = True
        totalAdminLabel.BackColor = Color.Transparent
        totalAdminLabel.Font = New Font("Inter SemiBold", 7F, FontStyle.Bold)
        totalAdminLabel.ForeColor = Color.FromArgb(CByte(153), CByte(161), CByte(175))
        totalAdminLabel.LetterSpacing = 1F
        totalAdminLabel.Location = New Point(21, 18)
        totalAdminLabel.Name = "totalAdminLabel"
        totalAdminLabel.Size = New Size(114, 17)
        totalAdminLabel.TabIndex = 0
        totalAdminLabel.Text = "TOTAL ADMIN  "
        totalAdminLabel.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' activeLabel
        ' 
        activeLabel.AutoSize = True
        activeLabel.BackColor = Color.Transparent
        activeLabel.Font = New Font("Inter SemiBold", 7F, FontStyle.Bold)
        activeLabel.ForeColor = Color.FromArgb(CByte(153), CByte(161), CByte(175))
        activeLabel.LetterSpacing = 1F
        activeLabel.Location = New Point(17, 18)
        activeLabel.Name = "activeLabel"
        activeLabel.Size = New Size(62, 17)
        activeLabel.TabIndex = 1
        activeLabel.Text = "ACTIVE "
        activeLabel.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' inactiveLabel
        ' 
        inactiveLabel.AutoSize = True
        inactiveLabel.BackColor = Color.Transparent
        inactiveLabel.Font = New Font("Inter SemiBold", 7F, FontStyle.Bold)
        inactiveLabel.ForeColor = Color.FromArgb(CByte(153), CByte(161), CByte(175))
        inactiveLabel.LetterSpacing = 1F
        inactiveLabel.Location = New Point(15, 18)
        inactiveLabel.Name = "inactiveLabel"
        inactiveLabel.Size = New Size(73, 17)
        inactiveLabel.TabIndex = 2
        inactiveLabel.Text = "INACTIVE"
        inactiveLabel.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' totalAdminNumber
        ' 
        totalAdminNumber.AutoSize = True
        totalAdminNumber.Font = New Font("DM Mono Medium", 14.9999981F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        totalAdminNumber.ForeColor = Color.FromArgb(CByte(16), CByte(24), CByte(40))
        totalAdminNumber.Location = New Point(21, 35)
        totalAdminNumber.Name = "totalAdminNumber"
        totalAdminNumber.Size = New Size(55, 39)
        totalAdminNumber.TabIndex = 3
        totalAdminNumber.Text = "00"
        ' 
        ' activeAdminNumber
        ' 
        activeAdminNumber.AutoSize = True
        activeAdminNumber.Font = New Font("DM Mono Medium", 14.9999981F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        activeAdminNumber.ForeColor = Color.FromArgb(CByte(16), CByte(24), CByte(40))
        activeAdminNumber.Location = New Point(17, 35)
        activeAdminNumber.Name = "activeAdminNumber"
        activeAdminNumber.Size = New Size(55, 39)
        activeAdminNumber.TabIndex = 4
        activeAdminNumber.Text = "00"
        ' 
        ' inactiveAdminNumber
        ' 
        inactiveAdminNumber.AutoSize = True
        inactiveAdminNumber.Font = New Font("DM Mono Medium", 14.9999981F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        inactiveAdminNumber.ForeColor = Color.FromArgb(CByte(16), CByte(24), CByte(40))
        inactiveAdminNumber.Location = New Point(17, 35)
        inactiveAdminNumber.Name = "inactiveAdminNumber"
        inactiveAdminNumber.Size = New Size(55, 39)
        inactiveAdminNumber.TabIndex = 5
        inactiveAdminNumber.Text = "00"
        ' 
        ' AdminAccountsCards
        ' 
        totalAdminCard.Controls.Add(totalAdminIconPlaceholder)
        totalAdminIconPlaceholder.Name = "totalAdminIconPlaceholder"
        totalAdminIconPlaceholder.FillColor = Color.FromArgb(242, 106, 0)
        totalAdminIconPlaceholder.BorderThickness = 0
        totalAdminIconPlaceholder.CornerRadius = 14
        totalAdminIconPlaceholder.Location = New Point(230, 22)
        totalAdminIconPlaceholder.Size = New Size(50, 50)
        totalAdminIconPlaceholder.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        totalAdminIconPlaceholder.Controls.Add(totalAdminIconPlaceholderImage)
        totalAdminIconPlaceholderImage.Name = "totalAdminIconPlaceholderImage"
        totalAdminIconPlaceholderImage.BackColor = Color.Transparent
        totalAdminIconPlaceholderImage.Location = New Point(12, 12)
        totalAdminIconPlaceholderImage.Size = New Size(26, 26)
        totalAdminIconPlaceholderImage.SizeMode = PictureBoxSizeMode.Zoom
        totalAdminIconPlaceholderImage.TabStop = False
        totalAdminIconPlaceholderImage.Image = CType(resources.GetObject("totalAdminIconPlaceholderImage.Image"), Image)
        activeCard.Controls.Add(activeIconPlaceholder)
        activeIconPlaceholder.Name = "activeIconPlaceholder"
        activeIconPlaceholder.FillColor = Color.FromArgb(0, 157, 106)
        activeIconPlaceholder.BorderThickness = 0
        activeIconPlaceholder.CornerRadius = 14
        activeIconPlaceholder.Location = New Point(230, 22)
        activeIconPlaceholder.Size = New Size(50, 50)
        activeIconPlaceholder.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        activeIconPlaceholder.Controls.Add(activeIconPlaceholderImage)
        activeIconPlaceholderImage.Name = "activeIconPlaceholderImage"
        activeIconPlaceholderImage.BackColor = Color.Transparent
        activeIconPlaceholderImage.Location = New Point(12, 12)
        activeIconPlaceholderImage.Size = New Size(26, 26)
        activeIconPlaceholderImage.SizeMode = PictureBoxSizeMode.Zoom
        activeIconPlaceholderImage.TabStop = False
        activeIconPlaceholderImage.Image = CType(resources.GetObject("activeIconPlaceholderImage.Image"), Image)
        inactiveCard.Controls.Add(inactiveIconPlaceholder)
        inactiveIconPlaceholder.Name = "inactiveIconPlaceholder"
        inactiveIconPlaceholder.FillColor = Color.FromArgb(107, 114, 128)
        inactiveIconPlaceholder.BorderThickness = 0
        inactiveIconPlaceholder.CornerRadius = 14
        inactiveIconPlaceholder.Location = New Point(230, 22)
        inactiveIconPlaceholder.Size = New Size(50, 50)
        inactiveIconPlaceholder.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        inactiveIconPlaceholder.Controls.Add(inactiveIconPlaceholderImage)
        inactiveIconPlaceholderImage.Name = "inactiveIconPlaceholderImage"
        inactiveIconPlaceholderImage.BackColor = Color.Transparent
        inactiveIconPlaceholderImage.Location = New Point(12, 12)
        inactiveIconPlaceholderImage.Size = New Size(26, 26)
        inactiveIconPlaceholderImage.SizeMode = PictureBoxSizeMode.Zoom
        inactiveIconPlaceholderImage.TabStop = False
        inactiveIconPlaceholderImage.Image = CType(resources.GetObject("inactiveIconPlaceholderImage.Image"), Image)
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(inactiveCard)
        Controls.Add(activeCard)
        Controls.Add(totalAdminCard)
        Name = "AdminAccountsCards"
        Size = New Size(924, 105)
        totalAdminCard.ResumeLayout(False)
        totalAdminCard.PerformLayout()
        activeCard.ResumeLayout(False)
        activeCard.PerformLayout()
        inactiveCard.ResumeLayout(False)
        inactiveCard.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents totalAdminCard As RoundedTitlePanel
    Friend WithEvents totalAdminLabel As SpacedLabel
    Friend WithEvents activeCard As RoundedTitlePanel
    Friend WithEvents inactiveCard As RoundedTitlePanel
    Friend WithEvents totalAdminNumber As Label
    Friend WithEvents inactiveAdminNumber As Label
    Friend WithEvents activeAdminNumber As Label
    Friend WithEvents activeLabel As SpacedLabel
    Friend WithEvents inactiveLabel As SpacedLabel

    Friend WithEvents totalAdminIconPlaceholder As RoundedTitlePanel
    Friend WithEvents totalAdminIconPlaceholderImage As PictureBox
    Friend WithEvents activeIconPlaceholder As RoundedTitlePanel
    Friend WithEvents activeIconPlaceholderImage As PictureBox
    Friend WithEvents inactiveIconPlaceholder As RoundedTitlePanel
    Friend WithEvents inactiveIconPlaceholderImage As PictureBox
End Class



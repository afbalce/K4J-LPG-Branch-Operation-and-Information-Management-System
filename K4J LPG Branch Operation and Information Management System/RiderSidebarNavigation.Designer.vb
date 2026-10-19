<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RiderSidebarNavigation
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
        k4jTitle = New Label()
        branchLabel = New Label()
        userPlaceholder = New RoundedTitlePanel()
        userInitialsPlaceholder = New RoundedTitlePanel()
        userInitalLabel = New Label()
        userRolePlaceholder = New RoundedTitlePanel()
        userRoleLabel = New Label()
        userNameLabel = New Label()
        signOutButton = New RoundedButton()
        deliveryHistoryButton = New RoundedButton()
        overviewCateg = New SpacedLabel()
        dashboardButton = New RoundedButton()
        myDeliveriesButton = New RoundedButton()
        deliveriesCategory = New SpacedLabel()
        userPlaceholder.SuspendLayout()
        userInitialsPlaceholder.SuspendLayout()
        userRolePlaceholder.SuspendLayout()
        SuspendLayout()
        ' 
        ' k4jTitle
        ' 
        k4jTitle.AutoSize = True
        k4jTitle.Font = New Font("Barlow Condensed Black", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 0)
        k4jTitle.ForeColor = Color.White
        k4jTitle.Location = New Point(84, 16)
        k4jTitle.Name = "k4jTitle"
        k4jTitle.Size = New Size(124, 24)
        k4jTitle.TabIndex = 1
        k4jTitle.Text = "K4J LPG CENTER"
        ' 
        ' branchLabel
        ' 
        branchLabel.AutoSize = True
        branchLabel.Font = New Font("Inter", 7F, FontStyle.Regular, GraphicsUnit.Point, 0)
        branchLabel.ForeColor = Color.FromArgb(90, 122, 158)
        branchLabel.Location = New Point(84, 40)
        branchLabel.Name = "branchLabel"
        branchLabel.Size = New Size(97, 17)
        branchLabel.TabIndex = 2
        branchLabel.Text = "JPANG/DAET"
        ' 
        ' userPlaceholder
        ' 
        userPlaceholder.BackColor = Color.FromArgb(26, 46, 74, 153)
        userPlaceholder.BorderColor = Color.White
        userPlaceholder.BorderOpacity = 7
        userPlaceholder.Controls.Add(userInitialsPlaceholder)
        userPlaceholder.Controls.Add(userRolePlaceholder)
        userPlaceholder.Controls.Add(userNameLabel)
        userPlaceholder.FillColor = Color.FromArgb(26, 46, 74)
        userPlaceholder.FillOpacity = 60
        userPlaceholder.Location = New Point(21, 74)
        userPlaceholder.Name = "userPlaceholder"
        userPlaceholder.Size = New Size(203, 65)
        userPlaceholder.TabIndex = 3
        ' 
        ' userInitialsPlaceholder
        ' 
        userInitialsPlaceholder.BackColor = Color.Transparent
        userInitialsPlaceholder.BorderColor = Color.FromArgb(196, 76, 0)
        userInitialsPlaceholder.Controls.Add(userInitalLabel)
        userInitialsPlaceholder.CornerRadius = 10
        userInitialsPlaceholder.FillColor = Color.FromArgb(196, 76, 0)
        userInitialsPlaceholder.Location = New Point(15, 14)
        userInitialsPlaceholder.Name = "userInitialsPlaceholder"
        userInitialsPlaceholder.Size = New Size(35, 35)
        userInitialsPlaceholder.TabIndex = 2
        ' 
        ' userInitalLabel
        ' 
        userInitalLabel.AutoSize = True
        userInitalLabel.Font = New Font("Inter", 7F, FontStyle.Bold)
        userInitalLabel.ForeColor = Color.White
        userInitalLabel.Location = New Point(5, 10)
        userInitalLabel.Name = "userInitalLabel"
        userInitalLabel.Size = New Size(29, 17)
        userInitalLabel.TabIndex = 3
        userInitalLabel.Text = "UN"
        ' 
        ' userRolePlaceholder
        ' 
        userRolePlaceholder.BackColor = Color.Transparent
        userRolePlaceholder.BorderColor = Color.FromArgb(21, 93, 252)
        userRolePlaceholder.Controls.Add(userRoleLabel)
        userRolePlaceholder.CornerRadius = 5
        userRolePlaceholder.FillColor = Color.FromArgb(21, 93, 252)
        userRolePlaceholder.Location = New Point(63, 29)
        userRolePlaceholder.Name = "userRolePlaceholder"
        userRolePlaceholder.Size = New Size(50, 20)
        userRolePlaceholder.TabIndex = 1
        ' 
        ' userRoleLabel
        ' 
        userRoleLabel.AutoSize = True
        userRoleLabel.Font = New Font("Inter", 5F, FontStyle.Bold)
        userRoleLabel.ForeColor = Color.White
        userRoleLabel.Location = New Point(8, 3)
        userRoleLabel.Name = "userRoleLabel"
        userRoleLabel.Size = New Size(73, 12)
        userRoleLabel.TabIndex = 2
        userRoleLabel.Text = "Rider"
        ' 
        ' userNameLabel
        ' 
        userNameLabel.AutoSize = True
        userNameLabel.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        userNameLabel.ForeColor = Color.White
        userNameLabel.Location = New Point(63, 12)
        userNameLabel.Name = "userNameLabel"
        userNameLabel.Size = New Size(67, 15)
        userNameLabel.TabIndex = 0
        userNameLabel.Text = "Username"
        ' 
        ' signOutButton
        ' 
        signOutButton.BackColor = Color.Transparent
        signOutButton.BackgroundImageLayout = ImageLayout.None
        signOutButton.BorderColor = Color.Transparent
        signOutButton.CornerRadius = 10
        signOutButton.FillColor = Color.Transparent
        signOutButton.FlatAppearance.BorderSize = 0
        signOutButton.FlatStyle = FlatStyle.Flat
        signOutButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        signOutButton.ForeColor = Color.FromArgb(122, 154, 191)
        signOutButton.IconFileName = "dashboard.png"
        signOutButton.IconHeight = 60
        signOutButton.IconWidth = 60
        signOutButton.ImageAlign = ContentAlignment.MiddleLeft
        signOutButton.Location = New Point(21, 674)
        signOutButton.MinimumSize = New Size(50, 25)
        signOutButton.Name = "signOutButton"
        signOutButton.Size = New Size(203, 25)
        signOutButton.TabIndex = 20
        signOutButton.Text = "Sign Out"
        signOutButton.TextAlign = ContentAlignment.MiddleLeft
        signOutButton.TextImageRelation = TextImageRelation.ImageBeforeText
        signOutButton.UseVisualStyleBackColor = False
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' deliveryHistoryButton
        ' 
        deliveryHistoryButton.BackColor = Color.Transparent
        deliveryHistoryButton.BackgroundImageLayout = ImageLayout.None
        deliveryHistoryButton.BorderColor = Color.Transparent
        deliveryHistoryButton.CornerRadius = 10
        deliveryHistoryButton.FillColor = Color.Transparent
        deliveryHistoryButton.FlatAppearance.BorderSize = 0
        deliveryHistoryButton.FlatStyle = FlatStyle.Flat
        deliveryHistoryButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        deliveryHistoryButton.ForeColor = Color.FromArgb(122, 154, 191)
        deliveryHistoryButton.IconFileName = "history.png"
        deliveryHistoryButton.IconHeight = 60
        deliveryHistoryButton.IconWidth = 60
        deliveryHistoryButton.ImageAlign = ContentAlignment.MiddleLeft
        deliveryHistoryButton.Location = New Point(21, 238)
        deliveryHistoryButton.MinimumSize = New Size(50, 25)
        deliveryHistoryButton.Name = "deliveryHistoryButton"
        deliveryHistoryButton.Size = New Size(203, 25)
        deliveryHistoryButton.TabIndex = 32
        deliveryHistoryButton.Text = "Delivery History"
        deliveryHistoryButton.TextAlign = ContentAlignment.MiddleLeft
        deliveryHistoryButton.TextImageRelation = TextImageRelation.ImageBeforeText
        deliveryHistoryButton.UseVisualStyleBackColor = False
        ' 
        ' 
        ' 
        ' 
        ' 
        ' myDeliveriesButton
        ' 
        myDeliveriesButton.BackColor = Color.Transparent
        myDeliveriesButton.BackgroundImageLayout = ImageLayout.None
        myDeliveriesButton.BorderColor = Color.Transparent
        myDeliveriesButton.CornerRadius = 10
        myDeliveriesButton.FillColor = Color.Transparent
        myDeliveriesButton.FlatAppearance.BorderSize = 0
        myDeliveriesButton.FlatStyle = FlatStyle.Flat
        myDeliveriesButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        myDeliveriesButton.ForeColor = Color.FromArgb(122, 154, 191)
        myDeliveriesButton.IconFileName = "delivery-truck.png"
        myDeliveriesButton.IconHeight = 60
        myDeliveriesButton.IconWidth = 60
        myDeliveriesButton.ImageAlign = ContentAlignment.MiddleLeft
        myDeliveriesButton.Location = New Point(21, 207)
        myDeliveriesButton.MinimumSize = New Size(50, 25)
        myDeliveriesButton.Name = "myDeliveriesButton"
        myDeliveriesButton.Size = New Size(203, 25)
        myDeliveriesButton.TabIndex = 27
        myDeliveriesButton.Text = "My Deliveries"
        myDeliveriesButton.TextAlign = ContentAlignment.MiddleLeft
        myDeliveriesButton.TextImageRelation = TextImageRelation.ImageBeforeText
        myDeliveriesButton.UseVisualStyleBackColor = False
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' deliveriesCategory
        ' 
        deliveriesCategory.AutoSize = True
        deliveriesCategory.BackColor = Color.Transparent
        deliveriesCategory.Font = New Font("Inter", 4F, FontStyle.Bold)
        deliveriesCategory.ForeColor = Color.FromArgb(61, 90, 122)
        deliveriesCategory.LetterSpacing = 1F
        deliveriesCategory.Location = New Point(21, 194)
        deliveriesCategory.Name = "deliveriesCategory"
        deliveriesCategory.Size = New Size(50, 10)
        deliveriesCategory.TabIndex = 21
        deliveriesCategory.Text = "DELIVERIES"
        deliveriesCategory.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' RiderSidebarNavigation
        ' 
        overviewCateg.AutoSize = True
        overviewCateg.BackColor = Color.Transparent
        overviewCateg.Font = New Font("Inter", 4F, FontStyle.Bold)
        overviewCateg.ForeColor = Color.FromArgb(61, 90, 122)
        overviewCateg.LetterSpacing = 1F
        overviewCateg.Location = New Point(21, 152)
        overviewCateg.Name = "overviewCateg"
        overviewCateg.Size = New Size(50, 10)
        overviewCateg.TabIndex = 21
        overviewCateg.Text = "OVERVIEW"
        overviewCateg.TextAlign = ContentAlignment.MiddleLeft
        dashboardButton.BackColor = Color.Transparent
        dashboardButton.BackgroundImageLayout = ImageLayout.None
        dashboardButton.BorderColor = Color.Transparent
        dashboardButton.CornerRadius = 10
        dashboardButton.FillColor = Color.Transparent
        dashboardButton.FlatAppearance.BorderSize = 0
        dashboardButton.FlatStyle = FlatStyle.Flat
        dashboardButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        dashboardButton.ForeColor = Color.FromArgb(122, 154, 191)
        dashboardButton.IconFileName = "dashboard.png"
        dashboardButton.IconHeight = 60
        dashboardButton.IconWidth = 60
        dashboardButton.ImageAlign = ContentAlignment.MiddleLeft
        dashboardButton.Location = New Point(21, 165)
        dashboardButton.MinimumSize = New Size(50, 25)
        dashboardButton.Name = "dashboardButton"
        dashboardButton.Size = New Size(203, 25)
        dashboardButton.TabIndex = 27
        dashboardButton.Text = "Dashboard"
        dashboardButton.TextAlign = ContentAlignment.MiddleLeft
        dashboardButton.TextImageRelation = TextImageRelation.ImageBeforeText
        dashboardButton.UseVisualStyleBackColor = False
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(12, 22, 40)
        Controls.Add(overviewCateg)
        Controls.Add(dashboardButton)
        Controls.Add(deliveryHistoryButton)
        Controls.Add(myDeliveriesButton)
        Controls.Add(deliveriesCategory)
        Controls.Add(signOutButton)
        Controls.Add(userPlaceholder)
        Controls.Add(branchLabel)
        Controls.Add(k4jTitle)
        Name = "RiderSidebarNavigation"
        Size = New Size(250, 724)
        userPlaceholder.ResumeLayout(False)
        userPlaceholder.PerformLayout()
        userInitialsPlaceholder.ResumeLayout(False)
        userInitialsPlaceholder.PerformLayout()
        userRolePlaceholder.ResumeLayout(False)
        userRolePlaceholder.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents k4jTitle As Label
    Friend WithEvents branchLabel As Label
    Friend WithEvents userPlaceholder As RoundedTitlePanel
    Friend WithEvents userInitialsPlaceholder As RoundedTitlePanel
    Friend WithEvents userInitalLabel As Label
    Friend WithEvents userRolePlaceholder As RoundedTitlePanel
    Friend WithEvents userRoleLabel As Label
    Friend WithEvents userNameLabel As Label
    Friend WithEvents signOutButton As RoundedButton
    Friend WithEvents deliveryHistoryButton As RoundedButton
    Friend WithEvents myDeliveriesButton As RoundedButton
    Friend WithEvents deliveriesCategory As SpacedLabel

    Friend WithEvents overviewCateg As SpacedLabel
    Friend WithEvents dashboardButton As RoundedButton
End Class

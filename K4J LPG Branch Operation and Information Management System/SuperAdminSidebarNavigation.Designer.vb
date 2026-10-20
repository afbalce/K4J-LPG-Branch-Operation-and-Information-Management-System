<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminSidebarNavigation
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
        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(SuperAdminSidebarNavigation))
        k4jLogoPicture = New PictureBox()
        k4jTitle = New Label()
        Label1 = New Label()
        userPlaceholder = New RoundedTitlePanel()
        userInitialsPlaceholder = New RoundedTitlePanel()
        userInitalLabel = New Label()
        userRolePlaceholder = New RoundedTitlePanel()
        userRoleLabel = New Label()
        userNameLabel = New Label()
        overviewCateg = New SpacedLabel()
        personnelCateg = New SpacedLabel()
        catalogCateg = New SpacedLabel()
        operationsCateg = New SpacedLabel()
        financeCateg = New SpacedLabel()
        systemCateg = New SpacedLabel()
        dashboardButton = New RoundedButton()
        adminAccButton = New RoundedButton()
        riderAccButton = New RoundedButton()
        productsButton = New RoundedButton()
        globalInventoryButton = New RoundedButton()
        customersButton = New RoundedButton()
        deliveriesButton = New RoundedButton()
        eodButton = New RoundedButton()
        reportsButton = New RoundedButton()
        settingsButton = New RoundedButton()
        signOutButton = New RoundedButton()
        userPlaceholder.SuspendLayout()
        userInitialsPlaceholder.SuspendLayout()
        userRolePlaceholder.SuspendLayout()
        SuspendLayout()
        ' 
        ' k4jTitle
        ' 
        k4jTitle.AutoSize = True
        k4jTitle.Font = New Font("Barlow Condensed Black", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        k4jTitle.ForeColor = Color.White
        k4jTitle.Location = New Point(92, 19)
        k4jTitle.Name = "k4jTitle"
        k4jTitle.Size = New Size(124, 24)
        k4jTitle.TabIndex = 0
        k4jTitle.Text = "K4J LPG CENTER"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Inter", 7F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.FromArgb(CByte(90), CByte(122), CByte(158))
        Label1.Location = New Point(92, 43)
        Label1.Name = "Label1"
        Label1.Size = New Size(89, 17)
        Label1.TabIndex = 1
        Label1.Text = "All Branches"
        ' 
        ' userPlaceholder
        ' 
        userPlaceholder.BackColor = Color.FromArgb(CByte(26), CByte(46), CByte(74), CByte(153))
        userPlaceholder.BorderColor = Color.White
        userPlaceholder.BorderOpacity = 7
        userPlaceholder.Controls.Add(userInitialsPlaceholder)
        userPlaceholder.Controls.Add(userRolePlaceholder)
        userPlaceholder.Controls.Add(userNameLabel)
        userPlaceholder.FillColor = Color.FromArgb(CByte(26), CByte(46), CByte(74))
        userPlaceholder.FillOpacity = 60
        userPlaceholder.Location = New Point(20, 79)
        userPlaceholder.Name = "userPlaceholder"
        userPlaceholder.Size = New Size(203, 65)
        userPlaceholder.TabIndex = 2
        ' 
        ' userInitialsPlaceholder
        ' 
        userInitialsPlaceholder.BackColor = Color.Transparent
        userInitialsPlaceholder.BorderColor = Color.FromArgb(CByte(196), CByte(76), CByte(0))
        userInitialsPlaceholder.Controls.Add(userInitalLabel)
        userInitialsPlaceholder.CornerRadius = 10
        userInitialsPlaceholder.FillColor = Color.FromArgb(CByte(196), CByte(76), CByte(0))
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
        userRolePlaceholder.BorderColor = Color.FromArgb(CByte(232), CByte(101), CByte(10))
        userRolePlaceholder.Controls.Add(userRoleLabel)
        userRolePlaceholder.CornerRadius = 5
        userRolePlaceholder.FillColor = Color.FromArgb(CByte(232), CByte(101), CByte(10))
        userRolePlaceholder.Location = New Point(63, 29)
        userRolePlaceholder.Name = "userRolePlaceholder"
        userRolePlaceholder.Size = New Size(85, 20)
        userRolePlaceholder.TabIndex = 1
        ' 
        ' userRoleLabel
        ' 
        userRoleLabel.AutoSize = True
        userRoleLabel.Font = New Font("Inter", 5F, FontStyle.Bold)
        userRoleLabel.ForeColor = Color.White
        userRoleLabel.Location = New Point(8, 3)
        userRoleLabel.Name = "userRoleLabel"
        userRoleLabel.Size = New Size(67, 12)
        userRoleLabel.TabIndex = 2
        userRoleLabel.Text = "Super Admin"
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
        ' overviewCateg
        ' 
        overviewCateg.AutoSize = True
        overviewCateg.BackColor = Color.Transparent
        overviewCateg.Font = New Font("Inter", 4F, FontStyle.Bold)
        overviewCateg.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        overviewCateg.LetterSpacing = 1F
        overviewCateg.Location = New Point(20, 160)
        overviewCateg.Name = "overviewCateg"
        overviewCateg.Size = New Size(50, 10)
        overviewCateg.TabIndex = 2
        overviewCateg.Text = "OVERVIEW "
        overviewCateg.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' personnelCateg
        ' 
        personnelCateg.AutoSize = True
        personnelCateg.BackColor = Color.Transparent
        personnelCateg.Font = New Font("Inter", 4F, FontStyle.Bold)
        personnelCateg.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        personnelCateg.LetterSpacing = 1F
        personnelCateg.Location = New Point(17, 210)
        personnelCateg.Name = "personnelCateg"
        personnelCateg.Size = New Size(57, 10)
        personnelCateg.TabIndex = 3
        personnelCateg.Text = "PERSONNEL  "
        personnelCateg.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' catalogCateg
        ' 
        catalogCateg.AutoSize = True
        catalogCateg.BackColor = Color.Transparent
        catalogCateg.Font = New Font("Inter", 4F, FontStyle.Bold)
        catalogCateg.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        catalogCateg.LetterSpacing = 1F
        catalogCateg.Location = New Point(17, 282)
        catalogCateg.Name = "catalogCateg"
        catalogCateg.Size = New Size(45, 10)
        catalogCateg.TabIndex = 4
        catalogCateg.Text = "CATALOG "
        catalogCateg.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' operationsCateg
        ' 
        operationsCateg.AutoSize = True
        operationsCateg.BackColor = Color.Transparent
        operationsCateg.Font = New Font("Inter", 4F, FontStyle.Bold)
        operationsCateg.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        operationsCateg.LetterSpacing = 1F
        operationsCateg.Location = New Point(18, 354)
        operationsCateg.Name = "operationsCateg"
        operationsCateg.Size = New Size(59, 10)
        operationsCateg.TabIndex = 5
        operationsCateg.Text = "OPERATIONS  "
        operationsCateg.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' financeCateg
        ' 
        financeCateg.AutoSize = True
        financeCateg.BackColor = Color.Transparent
        financeCateg.Font = New Font("Inter", 4F, FontStyle.Bold)
        financeCateg.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        financeCateg.LetterSpacing = 1F
        financeCateg.Location = New Point(20, 426)
        financeCateg.Name = "financeCateg"
        financeCateg.Size = New Size(43, 10)
        financeCateg.TabIndex = 6
        financeCateg.Text = "FINANCE "
        financeCateg.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' systemCateg
        ' 
        systemCateg.AutoSize = True
        systemCateg.BackColor = Color.Transparent
        systemCateg.Font = New Font("Inter", 4F, FontStyle.Bold)
        systemCateg.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        systemCateg.LetterSpacing = 1F
        systemCateg.Location = New Point(18, 498)
        systemCateg.Name = "systemCateg"
        systemCateg.Size = New Size(38, 10)
        systemCateg.TabIndex = 7
        systemCateg.Text = "SYSTEM"
        systemCateg.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' dashboardButton
        ' 
        dashboardButton.BackColor = Color.Transparent
        dashboardButton.BackgroundImageLayout = ImageLayout.None
        dashboardButton.BorderColor = Color.Transparent
        dashboardButton.CornerRadius = 10
        dashboardButton.FillColor = Color.Transparent
        dashboardButton.FlatAppearance.BorderSize = 0
        dashboardButton.FlatStyle = FlatStyle.Flat
        dashboardButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        dashboardButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        dashboardButton.Image = CType(resources.GetObject("dashboardButton.Image"), Image)
        dashboardButton.CenterIconAndText = False
        dashboardButton.IconHeight = 18
        dashboardButton.IconWidth = 18
        dashboardButton.ImageAlign = ContentAlignment.MiddleLeft
        dashboardButton.Location = New Point(17, 173)
        dashboardButton.MinimumSize = New Size(50, 25)
        dashboardButton.Name = "dashboardButton"
        dashboardButton.Size = New Size(206, 25)
        dashboardButton.TabIndex = 8
        dashboardButton.Text = "Dashboard"
        dashboardButton.TextAlign = ContentAlignment.MiddleLeft
        dashboardButton.TextImageRelation = TextImageRelation.ImageBeforeText
        dashboardButton.UseVisualStyleBackColor = False
        ' 
        ' adminAccButton
        ' 
        adminAccButton.BackColor = Color.Transparent
        adminAccButton.BackgroundImageLayout = ImageLayout.None
        adminAccButton.BorderColor = Color.Transparent
        adminAccButton.CornerRadius = 10
        adminAccButton.FillColor = Color.Transparent
        adminAccButton.FlatAppearance.BorderSize = 0
        adminAccButton.FlatStyle = FlatStyle.Flat
        adminAccButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        adminAccButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        adminAccButton.Image = CType(resources.GetObject("adminAccButton.Image"), Image)
        adminAccButton.CenterIconAndText = False
        adminAccButton.IconHeight = 18
        adminAccButton.IconWidth = 18
        adminAccButton.ImageAlign = ContentAlignment.MiddleLeft
        adminAccButton.Location = New Point(17, 223)
        adminAccButton.MinimumSize = New Size(50, 25)
        adminAccButton.Name = "adminAccButton"
        adminAccButton.Size = New Size(206, 25)
        adminAccButton.TabIndex = 9
        adminAccButton.Text = "Admin Accounts"
        adminAccButton.TextAlign = ContentAlignment.MiddleLeft
        adminAccButton.TextImageRelation = TextImageRelation.ImageBeforeText
        adminAccButton.UseVisualStyleBackColor = False
        ' 
        ' riderAccButton
        ' 
        riderAccButton.BackColor = Color.Transparent
        riderAccButton.BackgroundImageLayout = ImageLayout.None
        riderAccButton.BorderColor = Color.Transparent
        riderAccButton.CornerRadius = 10
        riderAccButton.FillColor = Color.Transparent
        riderAccButton.FlatAppearance.BorderSize = 0
        riderAccButton.FlatStyle = FlatStyle.Flat
        riderAccButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        riderAccButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        riderAccButton.Image = CType(resources.GetObject("riderAccButton.Image"), Image)
        riderAccButton.CenterIconAndText = False
        riderAccButton.IconHeight = 18
        riderAccButton.IconWidth = 18
        riderAccButton.ImageAlign = ContentAlignment.MiddleLeft
        riderAccButton.Location = New Point(18, 254)
        riderAccButton.MinimumSize = New Size(50, 25)
        riderAccButton.Name = "riderAccButton"
        riderAccButton.Size = New Size(205, 25)
        riderAccButton.TabIndex = 10
        riderAccButton.Text = "Rider Accounts"
        riderAccButton.TextAlign = ContentAlignment.MiddleLeft
        riderAccButton.TextImageRelation = TextImageRelation.ImageBeforeText
        riderAccButton.UseVisualStyleBackColor = False
        ' 
        ' productsButton
        ' 
        productsButton.BackColor = Color.Transparent
        productsButton.BackgroundImageLayout = ImageLayout.None
        productsButton.BorderColor = Color.Transparent
        productsButton.CornerRadius = 10
        productsButton.FillColor = Color.Transparent
        productsButton.FlatAppearance.BorderSize = 0
        productsButton.FlatStyle = FlatStyle.Flat
        productsButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        productsButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        productsButton.Image = CType(resources.GetObject("productsButton.Image"), Image)
        productsButton.CenterIconAndText = False
        productsButton.IconHeight = 18
        productsButton.IconWidth = 18
        productsButton.ImageAlign = ContentAlignment.MiddleLeft
        productsButton.Location = New Point(16, 295)
        productsButton.MinimumSize = New Size(50, 25)
        productsButton.Name = "productsButton"
        productsButton.Size = New Size(207, 25)
        productsButton.TabIndex = 11
        productsButton.Text = "Products"
        productsButton.TextAlign = ContentAlignment.MiddleLeft
        productsButton.TextImageRelation = TextImageRelation.ImageBeforeText
        productsButton.UseVisualStyleBackColor = False
        ' 
        ' globalInventoryButton
        ' 
        globalInventoryButton.BackColor = Color.Transparent
        globalInventoryButton.BackgroundImageLayout = ImageLayout.None
        globalInventoryButton.BorderColor = Color.Transparent
        globalInventoryButton.CornerRadius = 10
        globalInventoryButton.FillColor = Color.Transparent
        globalInventoryButton.FlatAppearance.BorderSize = 0
        globalInventoryButton.FlatStyle = FlatStyle.Flat
        globalInventoryButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        globalInventoryButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        globalInventoryButton.Image = CType(resources.GetObject("globalInventoryButton.Image"), Image)
        globalInventoryButton.CenterIconAndText = False
        globalInventoryButton.IconHeight = 18
        globalInventoryButton.IconWidth = 18
        globalInventoryButton.ImageAlign = ContentAlignment.MiddleLeft
        globalInventoryButton.Location = New Point(17, 326)
        globalInventoryButton.MinimumSize = New Size(50, 25)
        globalInventoryButton.Name = "globalInventoryButton"
        globalInventoryButton.Size = New Size(206, 25)
        globalInventoryButton.TabIndex = 12
        globalInventoryButton.Text = "Global Inventory"
        globalInventoryButton.TextAlign = ContentAlignment.MiddleLeft
        globalInventoryButton.TextImageRelation = TextImageRelation.ImageBeforeText
        globalInventoryButton.UseVisualStyleBackColor = False
        ' 
        ' customersButton
        ' 
        customersButton.BackColor = Color.Transparent
        customersButton.BackgroundImageLayout = ImageLayout.None
        customersButton.BorderColor = Color.Transparent
        customersButton.CornerRadius = 10
        customersButton.FillColor = Color.Transparent
        customersButton.FlatAppearance.BorderSize = 0
        customersButton.FlatStyle = FlatStyle.Flat
        customersButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        customersButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        customersButton.Image = CType(resources.GetObject("customersButton.Image"), Image)
        customersButton.CenterIconAndText = False
        customersButton.IconHeight = 18
        customersButton.IconWidth = 18
        customersButton.ImageAlign = ContentAlignment.MiddleLeft
        customersButton.Location = New Point(17, 367)
        customersButton.MinimumSize = New Size(50, 25)
        customersButton.Name = "customersButton"
        customersButton.Size = New Size(206, 25)
        customersButton.TabIndex = 13
        customersButton.Text = "Customers"
        customersButton.TextAlign = ContentAlignment.MiddleLeft
        customersButton.TextImageRelation = TextImageRelation.ImageBeforeText
        customersButton.UseVisualStyleBackColor = False
        ' 
        ' deliveriesButton
        ' 
        deliveriesButton.BackColor = Color.Transparent
        deliveriesButton.BackgroundImageLayout = ImageLayout.None
        deliveriesButton.BorderColor = Color.Transparent
        deliveriesButton.CornerRadius = 10
        deliveriesButton.FillColor = Color.Transparent
        deliveriesButton.FlatAppearance.BorderSize = 0
        deliveriesButton.FlatStyle = FlatStyle.Flat
        deliveriesButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        deliveriesButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        deliveriesButton.Image = CType(resources.GetObject("deliveriesButton.Image"), Image)
        deliveriesButton.CenterIconAndText = False
        deliveriesButton.IconHeight = 18
        deliveriesButton.IconWidth = 18
        deliveriesButton.ImageAlign = ContentAlignment.MiddleLeft
        deliveriesButton.Location = New Point(17, 398)
        deliveriesButton.MinimumSize = New Size(50, 25)
        deliveriesButton.Name = "deliveriesButton"
        deliveriesButton.Size = New Size(206, 25)
        deliveriesButton.TabIndex = 14
        deliveriesButton.Text = "Deliveries"
        deliveriesButton.TextAlign = ContentAlignment.MiddleLeft
        deliveriesButton.TextImageRelation = TextImageRelation.ImageBeforeText
        deliveriesButton.UseVisualStyleBackColor = False
        ' 
        ' eodButton
        ' 
        eodButton.BackColor = Color.Transparent
        eodButton.BackgroundImageLayout = ImageLayout.None
        eodButton.BorderColor = Color.Transparent
        eodButton.CornerRadius = 10
        eodButton.FillColor = Color.Transparent
        eodButton.FlatAppearance.BorderSize = 0
        eodButton.FlatStyle = FlatStyle.Flat
        eodButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        eodButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        eodButton.Image = CType(resources.GetObject("eodButton.Image"), Image)
        eodButton.CenterIconAndText = False
        eodButton.IconHeight = 18
        eodButton.IconWidth = 18
        eodButton.ImageAlign = ContentAlignment.MiddleLeft
        eodButton.Location = New Point(20, 439)
        eodButton.MinimumSize = New Size(50, 25)
        eodButton.Name = "eodButton"
        eodButton.Size = New Size(203, 25)
        eodButton.TabIndex = 15
        eodButton.Text = "EOD Sales"
        eodButton.TextAlign = ContentAlignment.MiddleLeft
        eodButton.TextImageRelation = TextImageRelation.ImageBeforeText
        eodButton.UseVisualStyleBackColor = False
        ' 
        ' reportsButton
        ' 
        reportsButton.BackColor = Color.Transparent
        reportsButton.BackgroundImageLayout = ImageLayout.None
        reportsButton.BorderColor = Color.Transparent
        reportsButton.CornerRadius = 10
        reportsButton.FillColor = Color.Transparent
        reportsButton.FlatAppearance.BorderSize = 0
        reportsButton.FlatStyle = FlatStyle.Flat
        reportsButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        reportsButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        reportsButton.Image = CType(resources.GetObject("reportsButton.Image"), Image)
        reportsButton.CenterIconAndText = False
        reportsButton.IconHeight = 18
        reportsButton.IconWidth = 18
        reportsButton.ImageAlign = ContentAlignment.MiddleLeft
        reportsButton.Location = New Point(20, 470)
        reportsButton.MinimumSize = New Size(50, 25)
        reportsButton.Name = "reportsButton"
        reportsButton.Size = New Size(203, 25)
        reportsButton.TabIndex = 16
        reportsButton.Text = "Reports"
        reportsButton.TextAlign = ContentAlignment.MiddleLeft
        reportsButton.TextImageRelation = TextImageRelation.ImageBeforeText
        reportsButton.UseVisualStyleBackColor = False
        ' 
        ' settingsButton
        ' 
        settingsButton.BackColor = Color.Transparent
        settingsButton.BackgroundImageLayout = ImageLayout.None
        settingsButton.BorderColor = Color.Transparent
        settingsButton.CornerRadius = 10
        settingsButton.FillColor = Color.Transparent
        settingsButton.FlatAppearance.BorderSize = 0
        settingsButton.FlatStyle = FlatStyle.Flat
        settingsButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        settingsButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        settingsButton.Image = CType(resources.GetObject("settingsButton.Image"), Image)
        settingsButton.CenterIconAndText = False
        settingsButton.IconHeight = 18
        settingsButton.IconWidth = 18
        settingsButton.ImageAlign = ContentAlignment.MiddleLeft
        settingsButton.Location = New Point(17, 511)
        settingsButton.MinimumSize = New Size(50, 25)
        settingsButton.Name = "settingsButton"
        settingsButton.Size = New Size(206, 25)
        settingsButton.TabIndex = 17
        settingsButton.Text = "Settings"
        settingsButton.TextAlign = ContentAlignment.MiddleLeft
        settingsButton.TextImageRelation = TextImageRelation.ImageBeforeText
        settingsButton.UseVisualStyleBackColor = False
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
        signOutButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        signOutButton.Image = CType(resources.GetObject("signOutButton.Image"), Image)
        signOutButton.CenterIconAndText = False
        signOutButton.IconHeight = 18
        signOutButton.IconWidth = 18
        signOutButton.ImageAlign = ContentAlignment.MiddleLeft
        signOutButton.Location = New Point(20, 675)
        signOutButton.MinimumSize = New Size(50, 25)
        signOutButton.Name = "signOutButton"
        signOutButton.Size = New Size(203, 25)
        signOutButton.TabIndex = 18
        signOutButton.Text = "Sign Out"
        signOutButton.TextAlign = ContentAlignment.MiddleLeft
        signOutButton.TextImageRelation = TextImageRelation.ImageBeforeText
        signOutButton.UseVisualStyleBackColor = False
        ' 
        ' SuperAdminSidebarNavigation
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(12), CByte(22), CByte(40))
        Controls.Add(signOutButton)
        Controls.Add(settingsButton)
        Controls.Add(reportsButton)
        Controls.Add(eodButton)
        Controls.Add(deliveriesButton)
        Controls.Add(customersButton)
        Controls.Add(globalInventoryButton)
        Controls.Add(productsButton)
        Controls.Add(riderAccButton)
        Controls.Add(adminAccButton)
        Controls.Add(dashboardButton)
        Controls.Add(systemCateg)
        Controls.Add(financeCateg)
        Controls.Add(operationsCateg)
        Controls.Add(catalogCateg)
        Controls.Add(personnelCateg)
        Controls.Add(overviewCateg)
        Controls.Add(userPlaceholder)
        Controls.Add(Label1)
        Controls.Add(k4jTitle)
        Name = "SuperAdminSidebarNavigation"
        Size = New Size(250, 724)
        userPlaceholder.ResumeLayout(False)
        userPlaceholder.PerformLayout()
        userInitialsPlaceholder.ResumeLayout(False)
        userInitialsPlaceholder.PerformLayout()
        userRolePlaceholder.ResumeLayout(False)
        userRolePlaceholder.PerformLayout()
        k4jLogoPicture.Name = "k4jLogoPicture"
        k4jLogoPicture.Image = CType(resources.GetObject("k4jLogoPicture.Image"), Image)
        k4jLogoPicture.SizeMode = PictureBoxSizeMode.Zoom
        k4jLogoPicture.BackColor = Color.Transparent
        k4jLogoPicture.Location = New Point(15, 8)
        k4jLogoPicture.Size = New Size(60, 60)
        k4jLogoPicture.TabStop = False
        Controls.Add(k4jLogoPicture)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents k4jTitle As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents userPlaceholder As RoundedTitlePanel
    Friend WithEvents userRolePlaceholder As RoundedTitlePanel
    Friend WithEvents userNameLabel As Label
    Friend WithEvents userRoleLabel As Label
    Friend WithEvents overviewCateg As SpacedLabel
    Friend WithEvents personnelCateg As SpacedLabel
    Friend WithEvents catalogCateg As SpacedLabel
    Friend WithEvents operationsCateg As SpacedLabel
    Friend WithEvents financeCateg As SpacedLabel
    Friend WithEvents systemCateg As SpacedLabel
    Friend WithEvents dashboardButton As RoundedButton
    Friend WithEvents adminAccButton As RoundedButton
    Friend WithEvents riderAccButton As RoundedButton
    Friend WithEvents productsButton As RoundedButton
    Friend WithEvents globalInventoryButton As RoundedButton
    Friend WithEvents customersButton As RoundedButton
    Friend WithEvents deliveriesButton As RoundedButton
    Friend WithEvents eodButton As RoundedButton
    Friend WithEvents reportsButton As RoundedButton
    Friend WithEvents settingsButton As RoundedButton
    Friend WithEvents signOutButton As RoundedButton
    Friend WithEvents userInitialsPlaceholder As RoundedTitlePanel
    Friend WithEvents userInitalLabel As Label

    Friend WithEvents k4jLogoPicture As PictureBox
End Class

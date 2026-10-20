<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminSidebarNaviagtion
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
        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(AdminSidebarNaviagtion))
        k4jLogoPicture = New PictureBox()
        k4jTitle = New Label()
        branchLabel = New Label()
        userPlaceholder = New RoundedTitlePanel()
        userInitialsPlaceholder = New RoundedTitlePanel()
        userInitalLabel = New Label()
        userRolePlaceholder = New RoundedTitlePanel()
        userRoleLabel = New Label()
        userNameLabel = New Label()
        signOutButton = New RoundedButton()
        SpacedLabel1 = New SpacedLabel()
        closingChecklistButton = New RoundedButton()
        eodButton = New RoundedButton()
        createOrderButton = New RoundedButton()
        customersButton = New RoundedButton()
        globalInventoryButton = New RoundedButton()
        productsButton = New RoundedButton()
        dashboardButton = New RoundedButton()
        financeCateg = New SpacedLabel()
        ordersCateg = New SpacedLabel()
        catalogCateg = New SpacedLabel()
        customerCateg = New SpacedLabel()
        SpacedLabel2 = New SpacedLabel()
        assignRiderButton = New RoundedButton()
        deliveriesButton = New RoundedButton()
        debtButton = New RoundedButton()
        openingChecklistButton = New RoundedButton()
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
        k4jTitle.Location = New Point(84, 16)
        k4jTitle.Name = "k4jTitle"
        k4jTitle.Size = New Size(124, 24)
        k4jTitle.TabIndex = 1
        k4jTitle.Text = "K4J LPG CENTER"
        ' 
        ' branchLabel
        ' 
        branchLabel.AutoSize = True
        branchLabel.Font = New Font("Inter", 7F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        branchLabel.ForeColor = Color.FromArgb(CByte(90), CByte(122), CByte(158))
        branchLabel.Location = New Point(84, 40)
        branchLabel.Name = "branchLabel"
        branchLabel.Size = New Size(97, 17)
        branchLabel.TabIndex = 2
        branchLabel.Text = "JPANG/DAET"
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
        userPlaceholder.Location = New Point(21, 74)
        userPlaceholder.Name = "userPlaceholder"
        userPlaceholder.Size = New Size(203, 65)
        userPlaceholder.TabIndex = 3
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
        userRolePlaceholder.BorderColor = Color.FromArgb(CByte(21), CByte(93), CByte(252))
        userRolePlaceholder.Controls.Add(userRoleLabel)
        userRolePlaceholder.CornerRadius = 5
        userRolePlaceholder.FillColor = Color.FromArgb(CByte(21), CByte(93), CByte(252))
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
        userRoleLabel.Size = New Size(73, 12)
        userRoleLabel.TabIndex = 2
        userRoleLabel.Text = "Branch Admin"
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
        signOutButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        signOutButton.Image = CType(resources.GetObject("signOutButton.Image"), Image)
        signOutButton.CenterIconAndText = False
        signOutButton.IconHeight = 18
        signOutButton.IconWidth = 18
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
        ' SpacedLabel1
        ' 
        SpacedLabel1.AutoSize = True
        SpacedLabel1.BackColor = Color.Transparent
        SpacedLabel1.Font = New Font("Inter", 4F, FontStyle.Bold)
        SpacedLabel1.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        SpacedLabel1.LetterSpacing = 1F
        SpacedLabel1.Location = New Point(15, 172)
        SpacedLabel1.Name = "SpacedLabel1"
        SpacedLabel1.Size = New Size(0, 10)
        SpacedLabel1.TabIndex = 19
        SpacedLabel1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' closingChecklistButton
        ' 
        closingChecklistButton.BackColor = Color.Transparent
        closingChecklistButton.BackgroundImageLayout = ImageLayout.None
        closingChecklistButton.BorderColor = Color.Transparent
        closingChecklistButton.CornerRadius = 10
        closingChecklistButton.FillColor = Color.Transparent
        closingChecklistButton.FlatAppearance.BorderSize = 0
        closingChecklistButton.FlatStyle = FlatStyle.Flat
        closingChecklistButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        closingChecklistButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        closingChecklistButton.Image = CType(resources.GetObject("closingChecklistButton.Image"), Image)
        closingChecklistButton.CenterIconAndText = False
        closingChecklistButton.IconHeight = 18
        closingChecklistButton.IconWidth = 18
        closingChecklistButton.ImageAlign = ContentAlignment.MiddleLeft
        closingChecklistButton.Location = New Point(21, 484)
        closingChecklistButton.MinimumSize = New Size(50, 25)
        closingChecklistButton.Name = "closingChecklistButton"
        closingChecklistButton.Size = New Size(203, 25)
        closingChecklistButton.TabIndex = 35
        closingChecklistButton.Text = "Closing Checklist"
        closingChecklistButton.TextAlign = ContentAlignment.MiddleLeft
        closingChecklistButton.TextImageRelation = TextImageRelation.ImageBeforeText
        closingChecklistButton.UseVisualStyleBackColor = False
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
        eodButton.Location = New Point(21, 515)
        eodButton.MinimumSize = New Size(50, 25)
        eodButton.Name = "eodButton"
        eodButton.Size = New Size(203, 25)
        eodButton.TabIndex = 34
        eodButton.Text = "EOD Sales"
        eodButton.TextAlign = ContentAlignment.MiddleLeft
        eodButton.TextImageRelation = TextImageRelation.ImageBeforeText
        eodButton.UseVisualStyleBackColor = False
        ' 
        ' createOrderButton
        ' 
        createOrderButton.BackColor = Color.Transparent
        createOrderButton.BackgroundImageLayout = ImageLayout.None
        createOrderButton.BorderColor = Color.Transparent
        createOrderButton.CornerRadius = 10
        createOrderButton.FillColor = Color.Transparent
        createOrderButton.FlatAppearance.BorderSize = 0
        createOrderButton.FlatStyle = FlatStyle.Flat
        createOrderButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        createOrderButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        createOrderButton.Image = CType(resources.GetObject("createOrderButton.Image"), Image)
        createOrderButton.CenterIconAndText = False
        createOrderButton.IconHeight = 18
        createOrderButton.IconWidth = 18
        createOrderButton.ImageAlign = ContentAlignment.MiddleLeft
        createOrderButton.Location = New Point(21, 319)
        createOrderButton.MinimumSize = New Size(50, 25)
        createOrderButton.Name = "createOrderButton"
        createOrderButton.Size = New Size(203, 25)
        createOrderButton.TabIndex = 33
        createOrderButton.Text = "Create Order"
        createOrderButton.TextAlign = ContentAlignment.MiddleLeft
        createOrderButton.TextImageRelation = TextImageRelation.ImageBeforeText
        createOrderButton.UseVisualStyleBackColor = False
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
        customersButton.Location = New Point(21, 206)
        customersButton.MinimumSize = New Size(50, 25)
        customersButton.Name = "customersButton"
        customersButton.Size = New Size(203, 25)
        customersButton.TabIndex = 32
        customersButton.Text = "Customers"
        customersButton.TextAlign = ContentAlignment.MiddleLeft
        customersButton.TextImageRelation = TextImageRelation.ImageBeforeText
        customersButton.UseVisualStyleBackColor = False
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
        globalInventoryButton.Location = New Point(21, 278)
        globalInventoryButton.MinimumSize = New Size(50, 25)
        globalInventoryButton.Name = "globalInventoryButton"
        globalInventoryButton.Size = New Size(203, 25)
        globalInventoryButton.TabIndex = 31
        globalInventoryButton.Text = "Inventory"
        globalInventoryButton.TextAlign = ContentAlignment.MiddleLeft
        globalInventoryButton.TextImageRelation = TextImageRelation.ImageBeforeText
        globalInventoryButton.UseVisualStyleBackColor = False
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
        productsButton.Location = New Point(21, 247)
        productsButton.MinimumSize = New Size(50, 25)
        productsButton.Name = "productsButton"
        productsButton.Size = New Size(203, 25)
        productsButton.TabIndex = 30
        productsButton.Text = "Products"
        productsButton.TextAlign = ContentAlignment.MiddleLeft
        productsButton.TextImageRelation = TextImageRelation.ImageBeforeText
        productsButton.UseVisualStyleBackColor = False
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
        dashboardButton.Location = New Point(21, 165)
        dashboardButton.MinimumSize = New Size(50, 25)
        dashboardButton.Name = "dashboardButton"
        dashboardButton.Size = New Size(203, 25)
        dashboardButton.TabIndex = 27
        dashboardButton.Text = "Dashboard"
        dashboardButton.TextAlign = ContentAlignment.MiddleLeft
        dashboardButton.TextImageRelation = TextImageRelation.ImageBeforeText
        dashboardButton.UseVisualStyleBackColor = False
        ' 
        ' financeCateg
        ' 
        financeCateg.AutoSize = True
        financeCateg.BackColor = Color.Transparent
        financeCateg.Font = New Font("Inter", 4F, FontStyle.Bold)
        financeCateg.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        financeCateg.LetterSpacing = 1F
        financeCateg.Location = New Point(21, 440)
        financeCateg.Name = "financeCateg"
        financeCateg.Size = New Size(95, 10)
        financeCateg.TabIndex = 25
        financeCateg.Text = "DAILY OPERATIONS       "
        financeCateg.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ordersCateg
        ' 
        ordersCateg.AutoSize = True
        ordersCateg.BackColor = Color.Transparent
        ordersCateg.Font = New Font("Inter", 4F, FontStyle.Bold)
        ordersCateg.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        ordersCateg.LetterSpacing = 1F
        ordersCateg.Location = New Point(21, 306)
        ordersCateg.Name = "ordersCateg"
        ordersCateg.Size = New Size(37, 10)
        ordersCateg.TabIndex = 24
        ordersCateg.Text = "ORDERS"
        ordersCateg.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' catalogCateg
        ' 
        catalogCateg.AutoSize = True
        catalogCateg.BackColor = Color.Transparent
        catalogCateg.Font = New Font("Inter", 4F, FontStyle.Bold)
        catalogCateg.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        catalogCateg.LetterSpacing = 1F
        catalogCateg.Location = New Point(21, 234)
        catalogCateg.Name = "catalogCateg"
        catalogCateg.Size = New Size(45, 10)
        catalogCateg.TabIndex = 23
        catalogCateg.Text = "CATALOG "
        catalogCateg.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' customerCateg
        ' 
        customerCateg.AutoSize = True
        customerCateg.BackColor = Color.Transparent
        customerCateg.Font = New Font("Inter", 4F, FontStyle.Bold)
        customerCateg.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        customerCateg.LetterSpacing = 1F
        customerCateg.Location = New Point(21, 193)
        customerCateg.Name = "customerCateg"
        customerCateg.Size = New Size(59, 10)
        customerCateg.TabIndex = 22
        customerCateg.Text = "CUSTOMERS  "
        customerCateg.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' SpacedLabel2
        ' 
        SpacedLabel2.AutoSize = True
        SpacedLabel2.BackColor = Color.Transparent
        SpacedLabel2.Font = New Font("Inter", 4F, FontStyle.Bold)
        SpacedLabel2.ForeColor = Color.FromArgb(CByte(61), CByte(90), CByte(122))
        SpacedLabel2.LetterSpacing = 1F
        SpacedLabel2.Location = New Point(21, 152)
        SpacedLabel2.Name = "SpacedLabel2"
        SpacedLabel2.Size = New Size(50, 10)
        SpacedLabel2.TabIndex = 21
        SpacedLabel2.Text = "OVERVIEW "
        SpacedLabel2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' assignRiderButton
        ' 
        assignRiderButton.BackColor = Color.Transparent
        assignRiderButton.BackgroundImageLayout = ImageLayout.None
        assignRiderButton.BorderColor = Color.Transparent
        assignRiderButton.CornerRadius = 10
        assignRiderButton.FillColor = Color.Transparent
        assignRiderButton.FlatAppearance.BorderSize = 0
        assignRiderButton.FlatStyle = FlatStyle.Flat
        assignRiderButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        assignRiderButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        assignRiderButton.Image = CType(resources.GetObject("assignRiderButton.Image"), Image)
        assignRiderButton.CenterIconAndText = False
        assignRiderButton.IconHeight = 18
        assignRiderButton.IconWidth = 18
        assignRiderButton.ImageAlign = ContentAlignment.MiddleLeft
        assignRiderButton.Location = New Point(21, 350)
        assignRiderButton.MinimumSize = New Size(50, 25)
        assignRiderButton.Name = "assignRiderButton"
        assignRiderButton.Size = New Size(203, 25)
        assignRiderButton.TabIndex = 37
        assignRiderButton.Text = "Assign Rider"
        assignRiderButton.TextAlign = ContentAlignment.MiddleLeft
        assignRiderButton.TextImageRelation = TextImageRelation.ImageBeforeText
        assignRiderButton.UseVisualStyleBackColor = False
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
        deliveriesButton.Location = New Point(21, 381)
        deliveriesButton.MinimumSize = New Size(50, 25)
        deliveriesButton.Name = "deliveriesButton"
        deliveriesButton.Size = New Size(203, 25)
        deliveriesButton.TabIndex = 38
        deliveriesButton.Text = "Deliveries"
        deliveriesButton.TextAlign = ContentAlignment.MiddleLeft
        deliveriesButton.TextImageRelation = TextImageRelation.ImageBeforeText
        deliveriesButton.UseVisualStyleBackColor = False
        ' 
        ' debtButton
        ' 
        debtButton.BackColor = Color.Transparent
        debtButton.BackgroundImageLayout = ImageLayout.None
        debtButton.BorderColor = Color.Transparent
        debtButton.CornerRadius = 10
        debtButton.FillColor = Color.Transparent
        debtButton.FlatAppearance.BorderSize = 0
        debtButton.FlatStyle = FlatStyle.Flat
        debtButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        debtButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        debtButton.Image = CType(resources.GetObject("debtButton.Image"), Image)
        debtButton.CenterIconAndText = False
        debtButton.IconHeight = 18
        debtButton.IconWidth = 18
        debtButton.ImageAlign = ContentAlignment.MiddleLeft
        debtButton.Location = New Point(21, 412)
        debtButton.MinimumSize = New Size(50, 25)
        debtButton.Name = "debtButton"
        debtButton.Size = New Size(203, 25)
        debtButton.TabIndex = 39
        debtButton.Text = "Debt / Unpaid "
        debtButton.TextAlign = ContentAlignment.MiddleLeft
        debtButton.TextImageRelation = TextImageRelation.ImageBeforeText
        debtButton.UseVisualStyleBackColor = False
        ' 
        ' openingChecklistButton
        ' 
        openingChecklistButton.BackColor = Color.Transparent
        openingChecklistButton.BackgroundImageLayout = ImageLayout.None
        openingChecklistButton.BorderColor = Color.Transparent
        openingChecklistButton.CornerRadius = 10
        openingChecklistButton.FillColor = Color.Transparent
        openingChecklistButton.FlatAppearance.BorderSize = 0
        openingChecklistButton.FlatStyle = FlatStyle.Flat
        openingChecklistButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        openingChecklistButton.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        openingChecklistButton.Image = CType(resources.GetObject("openingChecklistButton.Image"), Image)
        openingChecklistButton.CenterIconAndText = False
        openingChecklistButton.IconHeight = 18
        openingChecklistButton.IconWidth = 18
        openingChecklistButton.ImageAlign = ContentAlignment.MiddleLeft
        openingChecklistButton.Location = New Point(20, 453)
        openingChecklistButton.MinimumSize = New Size(50, 25)
        openingChecklistButton.Name = "openingChecklistButton"
        openingChecklistButton.Size = New Size(203, 25)
        openingChecklistButton.TabIndex = 40
        openingChecklistButton.Text = "Opening Checklist"
        openingChecklistButton.TextAlign = ContentAlignment.MiddleLeft
        openingChecklistButton.TextImageRelation = TextImageRelation.ImageBeforeText
        openingChecklistButton.UseVisualStyleBackColor = False
        ' 
        ' AdminSidebarNaviagtion
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(12), CByte(22), CByte(40))
        Controls.Add(openingChecklistButton)
        Controls.Add(debtButton)
        Controls.Add(deliveriesButton)
        Controls.Add(assignRiderButton)
        Controls.Add(closingChecklistButton)
        Controls.Add(eodButton)
        Controls.Add(createOrderButton)
        Controls.Add(customersButton)
        Controls.Add(globalInventoryButton)
        Controls.Add(productsButton)
        Controls.Add(dashboardButton)
        Controls.Add(financeCateg)
        Controls.Add(ordersCateg)
        Controls.Add(catalogCateg)
        Controls.Add(customerCateg)
        Controls.Add(SpacedLabel2)
        Controls.Add(SpacedLabel1)
        Controls.Add(signOutButton)
        Controls.Add(userPlaceholder)
        Controls.Add(branchLabel)
        Controls.Add(k4jTitle)
        Name = "AdminSidebarNaviagtion"
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
    Friend WithEvents branchLabel As Label
    Friend WithEvents userPlaceholder As RoundedTitlePanel
    Friend WithEvents userInitialsPlaceholder As RoundedTitlePanel
    Friend WithEvents userInitalLabel As Label
    Friend WithEvents userRolePlaceholder As RoundedTitlePanel
    Friend WithEvents userRoleLabel As Label
    Friend WithEvents userNameLabel As Label
    Friend WithEvents signOutButton As RoundedButton
    Friend WithEvents SpacedLabel1 As SpacedLabel
    Friend WithEvents closingChecklistButton As RoundedButton
    Friend WithEvents eodButton As RoundedButton
    Friend WithEvents createOrderButton As RoundedButton
    Friend WithEvents customersButton As RoundedButton
    Friend WithEvents globalInventoryButton As RoundedButton
    Friend WithEvents productsButton As RoundedButton
    Friend WithEvents dashboardButton As RoundedButton
    Friend WithEvents financeCateg As SpacedLabel
    Friend WithEvents ordersCateg As SpacedLabel
    Friend WithEvents catalogCateg As SpacedLabel
    Friend WithEvents customerCateg As SpacedLabel
    Friend WithEvents SpacedLabel2 As SpacedLabel
    Friend WithEvents assignRiderButton As RoundedButton
    Friend WithEvents deliveriesButton As RoundedButton
    Friend WithEvents debtButton As RoundedButton
    Friend WithEvents openingChecklistButton As RoundedButton

    Friend WithEvents k4jLogoPicture As PictureBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        PanelContainer = New SplitContainer()
        SidebarPanel = New Panel()
        credits = New Label()
        desc1 = New Label()
        desc2 = New Label()
        RoundedTitlePanel = New RoundedTitlePanel()
        BOPLabel = New Label()
        MSLabel = New Label()
        K4JLabel = New Label()
        RightPanel = New Panel()
        adminPanel = New RoundedTitlePanel()
        adminDesc = New Label()
        adminTitlePanel = New RoundedTitlePanel()
        adminLabel = New Label()
        riderPanel = New RoundedTitlePanel()
        riderDesc = New Label()
        riderTitlePanel = New RoundedTitlePanel()
        riderLabel = New Label()
        sAdminPanel = New RoundedTitlePanel()
        sAdminDesc = New Label()
        sAdminTitlePanel = New RoundedTitlePanel()
        sAdminLabel = New Label()
        LoginContainer = New RoundedTitlePanel()
        RoundedTitlePanel2 = New RoundedTitlePanel()
        forgotpass = New Label()
        signinButton = New RoundedButton()
        BranchDropdown = New RoundedComboBox()
        RoleDropdown = New RoundedComboBox()
        PassField = New RoundedTextField()
        EmailField = New RoundedTextField()
        BranchLabel = New Label()
        RoleLabel = New Label()
        PassLabel = New Label()
        EmailLabel = New Label()
        desc3 = New Label()
        WbLabel = New Label()
        CType(PanelContainer, ComponentModel.ISupportInitialize).BeginInit()
        PanelContainer.Panel1.SuspendLayout()
        PanelContainer.Panel2.SuspendLayout()
        PanelContainer.SuspendLayout()
        SidebarPanel.SuspendLayout()
        RoundedTitlePanel.SuspendLayout()
        RightPanel.SuspendLayout()
        adminPanel.SuspendLayout()
        adminTitlePanel.SuspendLayout()
        riderPanel.SuspendLayout()
        riderTitlePanel.SuspendLayout()
        sAdminPanel.SuspendLayout()
        sAdminTitlePanel.SuspendLayout()
        LoginContainer.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelContainer
        ' 
        PanelContainer.Dock = DockStyle.Fill
        PanelContainer.Location = New Point(0, 0)
        PanelContainer.Name = "PanelContainer"
        ' 
        ' PanelContainer.Panel1
        ' 
        PanelContainer.Panel1.Controls.Add(SidebarPanel)
        ' 
        ' PanelContainer.Panel2
        ' 
        PanelContainer.Panel2.Controls.Add(RightPanel)
        PanelContainer.Size = New Size(1178, 724)
        PanelContainer.SplitterDistance = 400
        PanelContainer.TabIndex = 0
        ' 
        ' SidebarPanel
        ' 
        SidebarPanel.AutoSize = True
        SidebarPanel.BackColor = Color.FromArgb(CByte(12), CByte(22), CByte(40))
        SidebarPanel.Controls.Add(credits)
        SidebarPanel.Controls.Add(desc1)
        SidebarPanel.Controls.Add(desc2)
        SidebarPanel.Controls.Add(RoundedTitlePanel)
        SidebarPanel.Controls.Add(MSLabel)
        SidebarPanel.Controls.Add(K4JLabel)
        SidebarPanel.Dock = DockStyle.Fill
        SidebarPanel.Location = New Point(0, 0)
        SidebarPanel.Name = "SidebarPanel"
        SidebarPanel.Size = New Size(400, 724)
        SidebarPanel.TabIndex = 0
        ' 
        ' credits
        ' 
        credits.AutoSize = True
        credits.Font = New Font("Inter", 5F)
        credits.ForeColor = Color.FromArgb(CByte(45), CByte(74), CByte(106))
        credits.Location = New Point(39, 680)
        credits.Name = "credits"
        credits.Size = New Size(283, 12)
        credits.TabIndex = 4
        credits.Text = "© 2025 LPG Management System. Jpang & Daet Branches."
        ' 
        ' desc1
        ' 
        desc1.AutoSize = True
        desc1.BackColor = Color.Transparent
        desc1.Font = New Font("Inter", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        desc1.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        desc1.Location = New Point(32, 346)
        desc1.Name = "desc1"
        desc1.Size = New Size(272, 15)
        desc1.TabIndex = 3
        desc1.Text = "Integrated system for inventory tracking, order"
        ' 
        ' desc2
        ' 
        desc2.AutoSize = True
        desc2.Font = New Font("Inter", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        desc2.ForeColor = Color.FromArgb(CByte(122), CByte(154), CByte(191))
        desc2.Location = New Point(32, 361)
        desc2.Name = "desc2"
        desc2.Size = New Size(334, 15)
        desc2.TabIndex = 2
        desc2.Text = "management, delivery monitoring, and financial reporting."
        ' 
        ' RoundedTitlePanel
        ' 
        RoundedTitlePanel.BackColor = Color.FromArgb(CByte(255), CByte(247), CByte(237))
        RoundedTitlePanel.BorderColor = Color.FromArgb(CByte(255), CByte(214), CByte(168))
        RoundedTitlePanel.BorderThickness = 2
        RoundedTitlePanel.Controls.Add(BOPLabel)
        RoundedTitlePanel.CornerRadius = 10
        RoundedTitlePanel.FillColor = Color.FromArgb(CByte(255), CByte(247), CByte(237))
        RoundedTitlePanel.Location = New Point(32, 207)
        RoundedTitlePanel.Name = "RoundedTitlePanel"
        RoundedTitlePanel.Size = New Size(334, 24)
        RoundedTitlePanel.TabIndex = 1
        ' 
        ' BOPLabel
        ' 
        BOPLabel.AutoSize = True
        BOPLabel.Font = New Font("DM Mono Medium", 4.99999952F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BOPLabel.ForeColor = Color.FromArgb(CByte(202), CByte(53), CByte(0))
        BOPLabel.Location = New Point(7, 6)
        BOPLabel.Name = "BOPLabel"
        BOPLabel.Size = New Size(189, 13)
        BOPLabel.TabIndex = 2
        BOPLabel.Text = "BRANCH OPERATIONS PLATFORM"
        ' 
        ' MSLabel
        ' 
        MSLabel.AutoSize = True
        MSLabel.Font = New Font("Barlow Condensed Black", 17.9999981F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        MSLabel.ForeColor = Color.White
        MSLabel.Location = New Point(32, 283)
        MSLabel.Name = "MSLabel"
        MSLabel.Size = New Size(315, 49)
        MSLabel.TabIndex = 1
        MSLabel.Text = "MANAGEMENT SYSTEM"
        ' 
        ' K4JLabel
        ' 
        K4JLabel.AutoSize = True
        K4JLabel.Font = New Font("Barlow Condensed Black", 17.9999981F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        K4JLabel.ForeColor = SystemColors.Control
        K4JLabel.Location = New Point(32, 234)
        K4JLabel.Name = "K4JLabel"
        K4JLabel.Size = New Size(245, 49)
        K4JLabel.TabIndex = 0
        K4JLabel.Text = "K4J LPG CENTER"
        ' 
        ' RightPanel
        ' 
        RightPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink
        RightPanel.Controls.Add(adminPanel)
        RightPanel.Controls.Add(riderPanel)
        RightPanel.Controls.Add(sAdminPanel)
        RightPanel.Controls.Add(LoginContainer)
        RightPanel.Dock = DockStyle.Fill
        RightPanel.Location = New Point(0, 0)
        RightPanel.Name = "RightPanel"
        RightPanel.Size = New Size(774, 724)
        RightPanel.TabIndex = 0
        ' 
        ' adminPanel
        ' 
        adminPanel.BackColor = Color.White
        adminPanel.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        adminPanel.BorderThickness = 2
        adminPanel.Controls.Add(adminDesc)
        adminPanel.Controls.Add(adminTitlePanel)
        adminPanel.CornerRadius = 12
        adminPanel.FillColor = Color.White
        adminPanel.Location = New Point(334, 533)
        adminPanel.Name = "adminPanel"
        adminPanel.Size = New Size(125, 70)
        adminPanel.TabIndex = 2
        ' 
        ' adminDesc
        ' 
        adminDesc.AutoSize = True
        adminDesc.Font = New Font("Inter", 5F)
        adminDesc.ForeColor = Color.FromArgb(CByte(153), CByte(161), CByte(175))
        adminDesc.Location = New Point(32, 40)
        adminDesc.Name = "adminDesc"
        adminDesc.Size = New Size(60, 12)
        adminDesc.TabIndex = 5
        adminDesc.Text = "Branch ops"
        ' 
        ' adminTitlePanel
        ' 
        adminTitlePanel.BackColor = Color.FromArgb(CByte(219), CByte(234), CByte(254))
        adminTitlePanel.BorderColor = Color.FromArgb(CByte(190), CByte(219), CByte(255))
        adminTitlePanel.BorderThickness = 2
        adminTitlePanel.Controls.Add(adminLabel)
        adminTitlePanel.CornerRadius = 5
        adminTitlePanel.FillColor = Color.FromArgb(CByte(219), CByte(234), CByte(254))
        adminTitlePanel.Location = New Point(35, 15)
        adminTitlePanel.Name = "adminTitlePanel"
        adminTitlePanel.Size = New Size(50, 20)
        adminTitlePanel.TabIndex = 4
        ' 
        ' adminLabel
        ' 
        adminLabel.AutoSize = True
        adminLabel.Font = New Font("Inter", 5F, FontStyle.Bold)
        adminLabel.ForeColor = Color.FromArgb(CByte(20), CByte(71), CByte(230))
        adminLabel.Location = New Point(7, 4)
        adminLabel.Name = "adminLabel"
        adminLabel.Size = New Size(36, 12)
        adminLabel.TabIndex = 4
        adminLabel.Text = "Admin"
        ' 
        ' riderPanel
        ' 
        riderPanel.BackColor = Color.White
        riderPanel.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        riderPanel.BorderThickness = 2
        riderPanel.Controls.Add(riderDesc)
        riderPanel.Controls.Add(riderTitlePanel)
        riderPanel.CornerRadius = 12
        riderPanel.FillColor = Color.White
        riderPanel.Location = New Point(470, 533)
        riderPanel.Name = "riderPanel"
        riderPanel.Size = New Size(125, 70)
        riderPanel.TabIndex = 2
        ' 
        ' riderDesc
        ' 
        riderDesc.AutoSize = True
        riderDesc.Font = New Font("Inter", 5F)
        riderDesc.ForeColor = Color.FromArgb(CByte(153), CByte(161), CByte(175))
        riderDesc.Location = New Point(25, 40)
        riderDesc.Name = "riderDesc"
        riderDesc.Size = New Size(74, 12)
        riderDesc.TabIndex = 6
        riderDesc.Text = "Deliveries only"
        ' 
        ' riderTitlePanel
        ' 
        riderTitlePanel.BackColor = Color.FromArgb(CByte(208), CByte(250), CByte(229))
        riderTitlePanel.BorderColor = Color.FromArgb(CByte(164), CByte(244), CByte(207))
        riderTitlePanel.BorderThickness = 2
        riderTitlePanel.Controls.Add(riderLabel)
        riderTitlePanel.CornerRadius = 5
        riderTitlePanel.FillColor = Color.FromArgb(CByte(208), CByte(250), CByte(229))
        riderTitlePanel.Location = New Point(35, 15)
        riderTitlePanel.Name = "riderTitlePanel"
        riderTitlePanel.Size = New Size(50, 20)
        riderTitlePanel.TabIndex = 4
        ' 
        ' riderLabel
        ' 
        riderLabel.AutoSize = True
        riderLabel.Font = New Font("Inter", 5F, FontStyle.Bold)
        riderLabel.ForeColor = Color.FromArgb(CByte(0), CByte(122), CByte(85))
        riderLabel.Location = New Point(10, 4)
        riderLabel.Name = "riderLabel"
        riderLabel.Size = New Size(31, 12)
        riderLabel.TabIndex = 5
        riderLabel.Text = "Rider"
        ' 
        ' sAdminPanel
        ' 
        sAdminPanel.BackColor = Color.White
        sAdminPanel.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        sAdminPanel.BorderThickness = 2
        sAdminPanel.Controls.Add(sAdminDesc)
        sAdminPanel.Controls.Add(sAdminTitlePanel)
        sAdminPanel.CornerRadius = 12
        sAdminPanel.FillColor = Color.White
        sAdminPanel.Location = New Point(195, 533)
        sAdminPanel.Name = "sAdminPanel"
        sAdminPanel.Size = New Size(125, 70)
        sAdminPanel.TabIndex = 1
        ' 
        ' sAdminDesc
        ' 
        sAdminDesc.AutoSize = True
        sAdminDesc.Font = New Font("Inter", 5F)
        sAdminDesc.ForeColor = Color.FromArgb(CByte(153), CByte(161), CByte(175))
        sAdminDesc.Location = New Point(25, 40)
        sAdminDesc.Name = "sAdminDesc"
        sAdminDesc.Size = New Size(76, 12)
        sAdminDesc.TabIndex = 4
        sAdminDesc.Text = "Both Branches"
        ' 
        ' sAdminTitlePanel
        ' 
        sAdminTitlePanel.BackColor = Color.FromArgb(CByte(255), CByte(237), CByte(212))
        sAdminTitlePanel.BorderColor = Color.FromArgb(CByte(255), CByte(214), CByte(168))
        sAdminTitlePanel.BorderThickness = 2
        sAdminTitlePanel.Controls.Add(sAdminLabel)
        sAdminTitlePanel.CornerRadius = 5
        sAdminTitlePanel.FillColor = Color.FromArgb(CByte(255), CByte(237), CByte(212))
        sAdminTitlePanel.Location = New Point(24, 15)
        sAdminTitlePanel.Name = "sAdminTitlePanel"
        sAdminTitlePanel.Size = New Size(76, 20)
        sAdminTitlePanel.TabIndex = 3
        ' 
        ' sAdminLabel
        ' 
        sAdminLabel.AutoSize = True
        sAdminLabel.Font = New Font("Inter", 5F, FontStyle.Bold)
        sAdminLabel.ForeColor = Color.FromArgb(CByte(202), CByte(53), CByte(0))
        sAdminLabel.Location = New Point(5, 4)
        sAdminLabel.Name = "sAdminLabel"
        sAdminLabel.Size = New Size(67, 12)
        sAdminLabel.TabIndex = 3
        sAdminLabel.Text = "Super Admin"
        ' 
        ' LoginContainer
        ' 
        LoginContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink
        LoginContainer.BackColor = Color.White
        LoginContainer.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        LoginContainer.BorderThickness = 2
        LoginContainer.Controls.Add(RoundedTitlePanel2)
        LoginContainer.Controls.Add(forgotpass)
        LoginContainer.Controls.Add(signinButton)
        LoginContainer.Controls.Add(BranchDropdown)
        LoginContainer.Controls.Add(RoleDropdown)
        LoginContainer.Controls.Add(PassField)
        LoginContainer.Controls.Add(EmailField)
        LoginContainer.Controls.Add(BranchLabel)
        LoginContainer.Controls.Add(RoleLabel)
        LoginContainer.Controls.Add(PassLabel)
        LoginContainer.Controls.Add(EmailLabel)
        LoginContainer.Controls.Add(desc3)
        LoginContainer.Controls.Add(WbLabel)
        LoginContainer.CornerRadius = 12
        LoginContainer.FillColor = Color.White
        LoginContainer.Location = New Point(195, 108)
        LoginContainer.Name = "LoginContainer"
        LoginContainer.Size = New Size(400, 400)
        LoginContainer.TabIndex = 0
        ' 
        ' RoundedTitlePanel2
        ' 
        RoundedTitlePanel2.BackColor = Color.FromArgb(CByte(245), CByte(239), CByte(232))
        RoundedTitlePanel2.BorderColor = Color.FromArgb(CByte(223), CByte(149), CByte(63))
        RoundedTitlePanel2.BorderThickness = 2
        RoundedTitlePanel2.CornerRadius = 12
        RoundedTitlePanel2.FillColor = Color.FromArgb(CByte(245), CByte(239), CByte(232))
        RoundedTitlePanel2.Location = New Point(141, 405)
        RoundedTitlePanel2.Name = "RoundedTitlePanel2"
        RoundedTitlePanel2.Size = New Size(123, 70)
        RoundedTitlePanel2.TabIndex = 2
        ' 
        ' forgotpass
        ' 
        forgotpass.AutoSize = True
        forgotpass.Font = New Font("Inter", 6F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        forgotpass.ForeColor = Color.FromArgb(CByte(232), CByte(101), CByte(10))
        forgotpass.Location = New Point(122, 359)
        forgotpass.Name = "forgotpass"
        forgotpass.Size = New Size(143, 15)
        forgotpass.TabIndex = 11
        forgotpass.Text = "Forgot your password?"
        ' 
        ' signinButton
        ' 
        signinButton.AutoSizeMode = AutoSizeMode.GrowAndShrink
        signinButton.BackColor = Color.Transparent
        signinButton.BorderColor = Color.FromArgb(CByte(232), CByte(101), CByte(10))
        signinButton.CornerRadius = 10
        signinButton.FillColor = Color.FromArgb(CByte(232), CByte(101), CByte(10))
        signinButton.FlatAppearance.BorderSize = 0
        signinButton.FlatStyle = FlatStyle.Flat
        signinButton.Font = New Font("Inter SemiBold", 7F, FontStyle.Bold)
        signinButton.ForeColor = Color.White
        signinButton.Location = New Point(32, 312)
        signinButton.MinimumSize = New Size(50, 25)
        signinButton.Name = "signinButton"
        signinButton.Size = New Size(323, 35)
        signinButton.TabIndex = 10
        signinButton.Text = "Sign In"
        signinButton.UseVisualStyleBackColor = False
        ' 
        ' BranchDropdown
        ' 
        BranchDropdown.ArrowColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        BranchDropdown.AutoSizeMode = AutoSizeMode.GrowAndShrink
        BranchDropdown.BackColor = Color.Transparent
        BranchDropdown.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        BranchDropdown.BorderThickness = 2
        BranchDropdown.CornerRadius = 5
        BranchDropdown.DropDownHeight = 106
        BranchDropdown.FillColor = Color.White
        BranchDropdown.Font = New Font("Segoe UI", 11F)
        BranchDropdown.HintColor = Color.Gray
        BranchDropdown.HintFont = New Font("Inter", 6F)
        BranchDropdown.HintFontSize = 6F
        BranchDropdown.HintText = "Select Branch"
        BranchDropdown.Location = New Point(205, 246)
        BranchDropdown.MinimumSize = New Size(90, 48)
        BranchDropdown.Name = "BranchDropdown"
        BranchDropdown.Size = New Size(150, 48)
        BranchDropdown.TabIndex = 9
        BranchDropdown.TabStop = False
        BranchDropdown.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        BranchDropdown.TextPadding = 10
        ' 
        ' RoleDropdown
        ' 
        RoleDropdown.ArrowColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        RoleDropdown.AutoSizeMode = AutoSizeMode.GrowAndShrink
        RoleDropdown.BackColor = Color.Transparent
        RoleDropdown.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        RoleDropdown.BorderThickness = 2
        RoleDropdown.CornerRadius = 5
        RoleDropdown.DropDownHeight = 106
        RoleDropdown.FillColor = Color.White
        RoleDropdown.Font = New Font("Segoe UI", 11F)
        RoleDropdown.HintColor = Color.Gray
        RoleDropdown.HintFont = New Font("Inter", 6F)
        RoleDropdown.HintFontSize = 6F
        RoleDropdown.HintText = "Select Role"
        RoleDropdown.Location = New Point(32, 246)
        RoleDropdown.MinimumSize = New Size(90, 48)
        RoleDropdown.Name = "RoleDropdown"
        RoleDropdown.Size = New Size(150, 48)
        RoleDropdown.TabIndex = 8
        RoleDropdown.TabStop = False
        RoleDropdown.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        RoleDropdown.TextPadding = 10
        ' 
        ' PassField
        ' 
        PassField.AutoSizeMode = AutoSizeMode.GrowAndShrink
        PassField.BackColor = Color.Transparent
        PassField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        PassField.BorderThickness = 2
        PassField.CornerRadius = 5
        PassField.FillColor = Color.White
        PassField.Font = New Font("Inter", 7F)
        PassField.HintColor = Color.Gray
        PassField.HintFont = New Font("Inter", 6F)
        PassField.HintFontSize = 6F
        PassField.HintText = "Enter your password"
        PassField.Location = New Point(35, 174)
        PassField.MinimumSize = New Size(70, 50)
        PassField.Name = "PassField"
        PassField.Size = New Size(320, 50)
        PassField.TabIndex = 7
        PassField.TextAlignment = HorizontalAlignment.Left
        PassField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        PassField.TextPadding = 12
        PassField.UseSystemPasswordChar = False
        ' 
        ' EmailField
        ' 
        EmailField.AutoSizeMode = AutoSizeMode.GrowAndShrink
        EmailField.BackColor = Color.Transparent
        EmailField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        EmailField.BorderThickness = 2
        EmailField.CornerRadius = 10
        EmailField.FillColor = Color.White
        EmailField.Font = New Font("Inter", 7F)
        EmailField.HintColor = Color.Gray
        EmailField.HintFont = New Font("Inter", 6F)
        EmailField.HintFontSize = 6F
        EmailField.HintText = "Enter your email address"
        EmailField.Location = New Point(35, 105)
        EmailField.MinimumSize = New Size(70, 50)
        EmailField.Name = "EmailField"
        EmailField.Size = New Size(320, 50)
        EmailField.TabIndex = 6
        EmailField.TextAlignment = HorizontalAlignment.Left
        EmailField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        EmailField.TextPadding = 12
        EmailField.UseSystemPasswordChar = False
        ' 
        ' BranchLabel
        ' 
        BranchLabel.AutoSize = True
        BranchLabel.BackColor = Color.Transparent
        BranchLabel.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BranchLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        BranchLabel.Location = New Point(205, 228)
        BranchLabel.Name = "BranchLabel"
        BranchLabel.Size = New Size(59, 15)
        BranchLabel.TabIndex = 5
        BranchLabel.Text = "BRANCH"
        ' 
        ' RoleLabel
        ' 
        RoleLabel.AutoSize = True
        RoleLabel.BackColor = Color.Transparent
        RoleLabel.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        RoleLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        RoleLabel.Location = New Point(32, 228)
        RoleLabel.Name = "RoleLabel"
        RoleLabel.Size = New Size(38, 15)
        RoleLabel.TabIndex = 4
        RoleLabel.Text = "ROLE"
        ' 
        ' PassLabel
        ' 
        PassLabel.AutoSize = True
        PassLabel.BackColor = Color.Transparent
        PassLabel.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        PassLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        PassLabel.Location = New Point(32, 157)
        PassLabel.Name = "PassLabel"
        PassLabel.Size = New Size(77, 15)
        PassLabel.TabIndex = 3
        PassLabel.Text = "PASSWORD"
        ' 
        ' EmailLabel
        ' 
        EmailLabel.AutoSize = True
        EmailLabel.BackColor = Color.Transparent
        EmailLabel.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        EmailLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        EmailLabel.Location = New Point(32, 86)
        EmailLabel.Name = "EmailLabel"
        EmailLabel.Size = New Size(105, 15)
        EmailLabel.TabIndex = 2
        EmailLabel.Text = "EMAIL ADDRESS"
        ' 
        ' desc3
        ' 
        desc3.AutoSize = True
        desc3.BackColor = Color.Transparent
        desc3.Font = New Font("Inter", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        desc3.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        desc3.Location = New Point(32, 55)
        desc3.Name = "desc3"
        desc3.Size = New Size(211, 15)
        desc3.TabIndex = 1
        desc3.Text = "Sign in back to your branch account"
        ' 
        ' WbLabel
        ' 
        WbLabel.AutoSize = True
        WbLabel.BackColor = Color.Transparent
        WbLabel.Font = New Font("Inter", 9F, FontStyle.Bold)
        WbLabel.Location = New Point(32, 30)
        WbLabel.Name = "WbLabel"
        WbLabel.Size = New Size(139, 21)
        WbLabel.TabIndex = 0
        WbLabel.Text = "Welcome back"
        ' 
        ' LoginForm
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(PanelContainer)
        Name = "LoginForm"
        Text = "Form1"
        PanelContainer.Panel1.ResumeLayout(False)
        PanelContainer.Panel1.PerformLayout()
        PanelContainer.Panel2.ResumeLayout(False)
        CType(PanelContainer, ComponentModel.ISupportInitialize).EndInit()
        PanelContainer.ResumeLayout(False)
        SidebarPanel.ResumeLayout(False)
        SidebarPanel.PerformLayout()
        RoundedTitlePanel.ResumeLayout(False)
        RoundedTitlePanel.PerformLayout()
        RightPanel.ResumeLayout(False)
        adminPanel.ResumeLayout(False)
        adminPanel.PerformLayout()
        adminTitlePanel.ResumeLayout(False)
        adminTitlePanel.PerformLayout()
        riderPanel.ResumeLayout(False)
        riderPanel.PerformLayout()
        riderTitlePanel.ResumeLayout(False)
        riderTitlePanel.PerformLayout()
        sAdminPanel.ResumeLayout(False)
        sAdminPanel.PerformLayout()
        sAdminTitlePanel.ResumeLayout(False)
        sAdminTitlePanel.PerformLayout()
        LoginContainer.ResumeLayout(False)
        LoginContainer.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents PanelContainer As SplitContainer
    Friend WithEvents SidebarPanel As Panel
    Friend WithEvents K4JLabel As Label
    Friend WithEvents MSLabel As Label
    Friend WithEvents RoundedTitlePanel As RoundedTitlePanel
    Friend WithEvents BOPLabel As Label
    Friend WithEvents desc2 As Label
    Friend WithEvents desc1 As Label
    Friend WithEvents credits As Label
    Friend WithEvents RightPanel As Panel
    Friend WithEvents riderPanel As RoundedTitlePanel
    Friend WithEvents sAdminPanel As RoundedTitlePanel
    Friend WithEvents LoginContainer As RoundedTitlePanel
    Friend WithEvents RoundedTitlePanel2 As RoundedTitlePanel
    Friend WithEvents forgotpass As Label
    Friend WithEvents BranchDropdown As RoundedComboBox
    Friend WithEvents RoleDropdown As RoundedComboBox
    Friend WithEvents PassField As RoundedTextField
    Friend WithEvents EmailField As RoundedTextField
    Friend WithEvents BranchLabel As Label
    Friend WithEvents RoleLabel As Label
    Friend WithEvents PassLabel As Label
    Friend WithEvents EmailLabel As Label
    Friend WithEvents desc3 As Label
    Friend WithEvents WbLabel As Label
    Friend WithEvents adminPanel As RoundedTitlePanel
    Friend WithEvents sAdminTitlePanel As RoundedTitlePanel
    Friend WithEvents signinButton As RoundedButton
    Friend WithEvents adminTitlePanel As RoundedTitlePanel
    Friend WithEvents riderTitlePanel As RoundedTitlePanel
    Friend WithEvents sAdminLabel As Label
    Friend WithEvents adminLabel As Label
    Friend WithEvents riderLabel As Label
    Friend WithEvents sAdminDesc As Label
    Friend WithEvents adminDesc As Label
    Friend WithEvents riderDesc As Label

End Class

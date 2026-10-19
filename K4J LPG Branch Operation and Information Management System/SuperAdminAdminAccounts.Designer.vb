<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminAdminAccounts
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        AdminAccountsMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        adminAccountsContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        SystemHeading1 = New SystemHeading()
        SidebarAndContent = New SplitContainer()
        sidebarPanel = New Panel()
        settingsSidebarPanel = New RoundedTitlePanel()
        SecurityButton = New RoundedButton()
        BranchButton = New RoundedButton()
        GeneralButton = New RoundedButton()
        CType(AdminAccountsMainContainer, ComponentModel.ISupportInitialize).BeginInit()
        AdminAccountsMainContainer.Panel1.SuspendLayout()
        AdminAccountsMainContainer.Panel2.SuspendLayout()
        AdminAccountsMainContainer.SuspendLayout()
        CType(adminAccountsContentContainer, ComponentModel.ISupportInitialize).BeginInit()
        adminAccountsContentContainer.Panel1.SuspendLayout()
        adminAccountsContentContainer.Panel2.SuspendLayout()
        adminAccountsContentContainer.SuspendLayout()
        CType(mainContentHolder, ComponentModel.ISupportInitialize).BeginInit()
        mainContentHolder.Panel1.SuspendLayout()
        mainContentHolder.Panel2.SuspendLayout()
        mainContentHolder.SuspendLayout()
        CType(SidebarAndContent, ComponentModel.ISupportInitialize).BeginInit()
        SidebarAndContent.Panel1.SuspendLayout()
        SidebarAndContent.SuspendLayout()
        sidebarPanel.SuspendLayout()
        settingsSidebarPanel.SuspendLayout()
        SuspendLayout()
        ' 
        ' AdminAccountsMainContainer
        ' 
        AdminAccountsMainContainer.Dock = DockStyle.Fill
        AdminAccountsMainContainer.Location = New Point(0, 0)
        AdminAccountsMainContainer.Name = "AdminAccountsMainContainer"
        ' 
        ' AdminAccountsMainContainer.Panel1
        ' 
        AdminAccountsMainContainer.Panel1.Controls.Add(SuperAdminSidebarNavigation1)
        ' 
        ' AdminAccountsMainContainer.Panel2
        ' 
        AdminAccountsMainContainer.Panel2.Controls.Add(adminAccountsContentContainer)
        AdminAccountsMainContainer.Size = New Size(1178, 724)
        AdminAccountsMainContainer.SplitterDistance = 250
        AdminAccountsMainContainer.TabIndex = 0
        ' 
        ' SuperAdminSidebarNavigation1
        ' 
        SuperAdminSidebarNavigation1.BackColor = Color.FromArgb(CByte(12), CByte(22), CByte(40))
        SuperAdminSidebarNavigation1.Dock = DockStyle.Left
        SuperAdminSidebarNavigation1.Location = New Point(0, 0)
        SuperAdminSidebarNavigation1.Name = "SuperAdminSidebarNavigation1"
        SuperAdminSidebarNavigation1.Size = New Size(250, 724)
        SuperAdminSidebarNavigation1.TabIndex = 0
        ' 
        ' adminAccountsContentContainer
        ' 
        adminAccountsContentContainer.Dock = DockStyle.Top
        adminAccountsContentContainer.FixedPanel = FixedPanel.Panel1
        adminAccountsContentContainer.IsSplitterFixed = True
        adminAccountsContentContainer.Location = New Point(0, 0)
        adminAccountsContentContainer.Name = "adminAccountsContentContainer"
        adminAccountsContentContainer.Orientation = Orientation.Horizontal
        ' 
        ' adminAccountsContentContainer.Panel1
        ' 
        adminAccountsContentContainer.Panel1.Controls.Add(HeaderBar1)
        ' 
        ' adminAccountsContentContainer.Panel2
        ' 
        adminAccountsContentContainer.Panel2.Controls.Add(mainContentHolder)
        adminAccountsContentContainer.Size = New Size(924, 724)
        adminAccountsContentContainer.TabIndex = 0
        ' 
        ' HeaderBar1
        ' 
        HeaderBar1.BackColor = Color.White
        HeaderBar1.Dock = DockStyle.Top
        HeaderBar1.Location = New Point(0, 0)
        HeaderBar1.Margin = New Padding(0)
        HeaderBar1.Name = "HeaderBar1"
        HeaderBar1.Size = New Size(924, 56)
        HeaderBar1.TabIndex = 0
        ' 
        ' mainContentHolder
        ' 
        mainContentHolder.Dock = DockStyle.Fill
        mainContentHolder.FixedPanel = FixedPanel.Panel1
        mainContentHolder.IsSplitterFixed = True
        mainContentHolder.Location = New Point(0, 0)
        mainContentHolder.Name = "mainContentHolder"
        mainContentHolder.Orientation = Orientation.Horizontal
        ' 
        ' mainContentHolder.Panel1
        ' 
        mainContentHolder.Panel1.Controls.Add(SystemHeading1)
        ' 
        ' mainContentHolder.Panel2
        ' 
        mainContentHolder.Panel2.Controls.Add(SidebarAndContent)
        mainContentHolder.Size = New Size(924, 670)
        mainContentHolder.SplitterDistance = 80
        mainContentHolder.TabIndex = 0
        ' 
        ' SystemHeading1
        ' 
        SystemHeading1.BackColor = Color.FromArgb(CByte(241), CByte(245), CByte(249))
        SystemHeading1.Dock = DockStyle.Fill
        SystemHeading1.Location = New Point(0, 0)
        SystemHeading1.Margin = New Padding(0)
        SystemHeading1.Name = "SystemHeading1"
        SystemHeading1.Size = New Size(924, 82)
        SystemHeading1.TabIndex = 0
        ' 
        ' SidebarAndContent
        ' 
        SidebarAndContent.Dock = DockStyle.Fill
        SidebarAndContent.Location = New Point(0, 0)
        SidebarAndContent.Name = "SidebarAndContent"
        ' 
        ' SidebarAndContent.Panel1
        ' 
        SidebarAndContent.Panel1.CausesValidation = False
        SidebarAndContent.Panel1.Controls.Add(sidebarPanel)
        SidebarAndContent.Size = New Size(924, 586)
        SidebarAndContent.SplitterDistance = 150
        SidebarAndContent.TabIndex = 0
        ' 
        ' sidebarPanel
        ' 
        sidebarPanel.Controls.Add(settingsSidebarPanel)
        sidebarPanel.Dock = DockStyle.Fill
        sidebarPanel.Location = New Point(0, 0)
        sidebarPanel.Name = "sidebarPanel"
        sidebarPanel.Size = New Size(150, 586)
        sidebarPanel.TabIndex = 0
        ' 
        ' settingsSidebarPanel
        ' 
        settingsSidebarPanel.BackColor = Color.Transparent
        settingsSidebarPanel.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        settingsSidebarPanel.BorderOpacity = 80
        settingsSidebarPanel.Controls.Add(SecurityButton)
        settingsSidebarPanel.Controls.Add(BranchButton)
        settingsSidebarPanel.Controls.Add(GeneralButton)
        settingsSidebarPanel.FillColor = Color.White
        settingsSidebarPanel.Location = New Point(15, 15)
        settingsSidebarPanel.Name = "settingsSidebarPanel"
        settingsSidebarPanel.Size = New Size(120, 150)
        settingsSidebarPanel.TabIndex = 0
        ' 
        ' SecurityButton
        ' 
        SecurityButton.BackColor = Color.Transparent
        SecurityButton.BorderColor = Color.Transparent
        SecurityButton.CornerRadius = 5
        SecurityButton.FillColor = Color.Transparent
        SecurityButton.FlatAppearance.BorderSize = 0
        SecurityButton.FlatStyle = FlatStyle.Flat
        SecurityButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        SecurityButton.ForeColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        SecurityButton.ImageAlign = ContentAlignment.MiddleLeft
        SecurityButton.Location = New Point(15, 96)
        SecurityButton.MinimumSize = New Size(50, 10)
        SecurityButton.Name = "SecurityButton"
        SecurityButton.Size = New Size(90, 30)
        SecurityButton.TabIndex = 2
        SecurityButton.Text = "Security"
        SecurityButton.TextAlign = ContentAlignment.MiddleLeft
        SecurityButton.TextImageRelation = TextImageRelation.ImageBeforeText
        SecurityButton.UseVisualStyleBackColor = False
        ' 
        ' BranchButton
        ' 
        BranchButton.BackColor = Color.Transparent
        BranchButton.BorderColor = Color.Transparent
        BranchButton.CornerRadius = 5
        BranchButton.FillColor = Color.Transparent
        BranchButton.FlatAppearance.BorderSize = 0
        BranchButton.FlatStyle = FlatStyle.Flat
        BranchButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BranchButton.ForeColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        BranchButton.ImageAlign = ContentAlignment.MiddleLeft
        BranchButton.Location = New Point(15, 60)
        BranchButton.MinimumSize = New Size(50, 10)
        BranchButton.Name = "BranchButton"
        BranchButton.Size = New Size(90, 30)
        BranchButton.TabIndex = 1
        BranchButton.Text = "Branch"
        BranchButton.TextAlign = ContentAlignment.MiddleLeft
        BranchButton.TextImageRelation = TextImageRelation.ImageBeforeText
        BranchButton.UseVisualStyleBackColor = False
        ' 
        ' GeneralButton
        ' 
        GeneralButton.BackColor = Color.Transparent
        GeneralButton.BorderColor = Color.Transparent
        GeneralButton.CornerRadius = 5
        GeneralButton.FillColor = Color.Transparent
        GeneralButton.FlatAppearance.BorderSize = 0
        GeneralButton.FlatStyle = FlatStyle.Flat
        GeneralButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GeneralButton.ForeColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        GeneralButton.ImageAlign = ContentAlignment.MiddleLeft
        GeneralButton.Location = New Point(15, 24)
        GeneralButton.MinimumSize = New Size(50, 10)
        GeneralButton.Name = "GeneralButton"
        GeneralButton.Size = New Size(90, 30)
        GeneralButton.TabIndex = 0
        GeneralButton.Text = "General"
        GeneralButton.TextAlign = ContentAlignment.MiddleLeft
        GeneralButton.TextImageRelation = TextImageRelation.ImageBeforeText
        GeneralButton.UseVisualStyleBackColor = False
        ' 
        ' SuperAdminAdminAccounts
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(AdminAccountsMainContainer)
        Name = "SuperAdminAdminAccounts"
        Text = "SuperAdminAdminAccounts"
        AdminAccountsMainContainer.Panel1.ResumeLayout(False)
        AdminAccountsMainContainer.Panel2.ResumeLayout(False)
        CType(AdminAccountsMainContainer, ComponentModel.ISupportInitialize).EndInit()
        AdminAccountsMainContainer.ResumeLayout(False)
        adminAccountsContentContainer.Panel1.ResumeLayout(False)
        adminAccountsContentContainer.Panel2.ResumeLayout(False)
        CType(adminAccountsContentContainer, ComponentModel.ISupportInitialize).EndInit()
        adminAccountsContentContainer.ResumeLayout(False)
        mainContentHolder.Panel1.ResumeLayout(False)
        mainContentHolder.Panel2.ResumeLayout(False)
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        SidebarAndContent.Panel1.ResumeLayout(False)
        CType(SidebarAndContent, ComponentModel.ISupportInitialize).EndInit()
        SidebarAndContent.ResumeLayout(False)
        sidebarPanel.ResumeLayout(False)
        settingsSidebarPanel.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents AdminAccountsMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents adminAccountsContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
    Friend WithEvents SidebarAndContent As SplitContainer
    Friend WithEvents sidebarPanel As Panel
    Friend WithEvents settingsSidebarPanel As RoundedTitlePanel
    Friend WithEvents GeneralButton As RoundedButton
    Friend WithEvents SecurityButton As RoundedButton
    Friend WithEvents BranchButton As RoundedButton
End Class


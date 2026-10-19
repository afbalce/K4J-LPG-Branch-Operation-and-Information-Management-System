<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminSettings
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
        SettingsMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        settingsContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        SystemHeading1 = New SystemHeading()
        SplitContainer1 = New SplitContainer()
        SettingsSidebarNavigation = New Panel()
        settingsSidebarPanel = New RoundedTitlePanel()
        SecurityButton = New RoundedButton()
        BranchButton = New RoundedButton()
        GeneralButton = New RoundedButton()
        CType(SettingsMainContainer, ComponentModel.ISupportInitialize).BeginInit()
        SettingsMainContainer.Panel1.SuspendLayout()
        SettingsMainContainer.Panel2.SuspendLayout()
        SettingsMainContainer.SuspendLayout()
        CType(settingsContentContainer, ComponentModel.ISupportInitialize).BeginInit()
        settingsContentContainer.Panel1.SuspendLayout()
        settingsContentContainer.Panel2.SuspendLayout()
        settingsContentContainer.SuspendLayout()
        CType(mainContentHolder, ComponentModel.ISupportInitialize).BeginInit()
        mainContentHolder.Panel1.SuspendLayout()
        mainContentHolder.Panel2.SuspendLayout()
        mainContentHolder.SuspendLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.SuspendLayout()
        SettingsSidebarNavigation.SuspendLayout()
        settingsSidebarPanel.SuspendLayout()
        SuspendLayout()
        ' 
        ' SettingsMainContainer
        ' 
        SettingsMainContainer.Dock = DockStyle.Fill
        SettingsMainContainer.Location = New Point(0, 0)
        SettingsMainContainer.Name = "SettingsMainContainer"
        ' 
        ' SettingsMainContainer.Panel1
        ' 
        SettingsMainContainer.Panel1.Controls.Add(SuperAdminSidebarNavigation1)
        ' 
        ' SettingsMainContainer.Panel2
        ' 
        SettingsMainContainer.Panel2.Controls.Add(settingsContentContainer)
        SettingsMainContainer.Size = New Size(1178, 724)
        SettingsMainContainer.SplitterDistance = 250
        SettingsMainContainer.TabIndex = 0
        ' 
        ' SuperAdminSidebarNavigation1
        ' 
        SuperAdminSidebarNavigation1.BackColor = Color.FromArgb(CByte(12), CByte(22), CByte(40))
        SuperAdminSidebarNavigation1.Dock = DockStyle.Fill
        SuperAdminSidebarNavigation1.Location = New Point(0, 0)
        SuperAdminSidebarNavigation1.Name = "SuperAdminSidebarNavigation1"
        SuperAdminSidebarNavigation1.Size = New Size(250, 724)
        SuperAdminSidebarNavigation1.TabIndex = 0
        ' 
        ' settingsContentContainer
        ' 
        settingsContentContainer.Dock = DockStyle.Fill
        settingsContentContainer.FixedPanel = FixedPanel.Panel1
        settingsContentContainer.IsSplitterFixed = True
        settingsContentContainer.Location = New Point(0, 0)
        settingsContentContainer.Name = "settingsContentContainer"
        settingsContentContainer.Orientation = Orientation.Horizontal
        ' 
        ' settingsContentContainer.Panel1
        ' 
        settingsContentContainer.Panel1.Controls.Add(HeaderBar1)
        ' 
        ' settingsContentContainer.Panel2
        ' 
        settingsContentContainer.Panel2.Controls.Add(mainContentHolder)
        settingsContentContainer.Size = New Size(924, 724)
        settingsContentContainer.TabIndex = 0
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
        mainContentHolder.Panel2.Controls.Add(SplitContainer1)
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
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.Location = New Point(0, 0)
        SplitContainer1.Name = "SplitContainer1"
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(SettingsSidebarNavigation)
        SplitContainer1.Size = New Size(924, 586)
        SplitContainer1.SplitterDistance = 150
        SplitContainer1.TabIndex = 0
        ' 
        ' SettingsSidebarNavigation
        ' 
        SettingsSidebarNavigation.Controls.Add(settingsSidebarPanel)
        SettingsSidebarNavigation.Dock = DockStyle.Fill
        SettingsSidebarNavigation.Location = New Point(0, 0)
        SettingsSidebarNavigation.Name = "SettingsSidebarNavigation"
        SettingsSidebarNavigation.Size = New Size(150, 586)
        SettingsSidebarNavigation.TabIndex = 0
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
        settingsSidebarPanel.Location = New Point(3, 3)
        settingsSidebarPanel.Name = "settingsSidebarPanel"
        settingsSidebarPanel.Size = New Size(145, 115)
        settingsSidebarPanel.TabIndex = 2
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
        SecurityButton.Location = New Point(15, 77)
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
        BranchButton.Location = New Point(15, 41)
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
        GeneralButton.Location = New Point(15, 5)
        GeneralButton.MinimumSize = New Size(50, 10)
        GeneralButton.Name = "GeneralButton"
        GeneralButton.Size = New Size(90, 30)
        GeneralButton.TabIndex = 0
        GeneralButton.Text = "General"
        GeneralButton.TextAlign = ContentAlignment.MiddleLeft
        GeneralButton.TextImageRelation = TextImageRelation.ImageBeforeText
        GeneralButton.UseVisualStyleBackColor = False
        ' 
        ' SuperAdminSettings
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(SettingsMainContainer)
        Name = "SuperAdminSettings"
        Text = "SuperAdminSettings"
        SettingsMainContainer.Panel1.ResumeLayout(False)
        SettingsMainContainer.Panel2.ResumeLayout(False)
        CType(SettingsMainContainer, ComponentModel.ISupportInitialize).EndInit()
        SettingsMainContainer.ResumeLayout(False)
        settingsContentContainer.Panel1.ResumeLayout(False)
        settingsContentContainer.Panel2.ResumeLayout(False)
        CType(settingsContentContainer, ComponentModel.ISupportInitialize).EndInit()
        settingsContentContainer.ResumeLayout(False)
        mainContentHolder.Panel1.ResumeLayout(False)
        mainContentHolder.Panel2.ResumeLayout(False)
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        SplitContainer1.Panel1.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        SettingsSidebarNavigation.ResumeLayout(False)
        settingsSidebarPanel.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents SettingsMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents settingsContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SettingsSidebarNavigation As Panel
    Friend WithEvents SecurityButton As RoundedButton
    Friend WithEvents BranchButton As RoundedButton
    Friend WithEvents GeneralButton As RoundedButton
    Protected WithEvents settingsSidebarPanel As RoundedTitlePanel
End Class



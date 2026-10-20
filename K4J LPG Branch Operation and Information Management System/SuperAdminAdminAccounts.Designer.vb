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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SuperAdminAdminAccounts))
        AdminAccountsMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        adminAccountsContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        addAdminButton = New RoundedButton()
        SystemHeading1 = New SystemHeading()
        mainContents = New SplitContainer()
        AdminAccountsCards1 = New AdminAccountsCards()
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
        CType(mainContents, ComponentModel.ISupportInitialize).BeginInit()
        mainContents.Panel1.SuspendLayout()
        mainContents.SuspendLayout()
        SuspendLayout()
        ' 
        ' AdminAccountsMainContainer
        ' 
        AdminAccountsMainContainer.Dock = DockStyle.Fill
        AdminAccountsMainContainer.FixedPanel = FixedPanel.Panel1
        AdminAccountsMainContainer.IsSplitterFixed = True
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
        SuperAdminSidebarNavigation1.Dock = DockStyle.Fill
        SuperAdminSidebarNavigation1.Location = New Point(0, 0)
        SuperAdminSidebarNavigation1.Name = "SuperAdminSidebarNavigation1"
        SuperAdminSidebarNavigation1.Size = New Size(250, 724)
        SuperAdminSidebarNavigation1.TabIndex = 0
        ' 
        ' adminAccountsContentContainer
        ' 
        adminAccountsContentContainer.Dock = DockStyle.Fill
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
        adminAccountsContentContainer.SplitterDistance = 56
        adminAccountsContentContainer.TabIndex = 0
        ' 
        ' HeaderBar1
        ' 
        HeaderBar1.BackColor = Color.White
        HeaderBar1.Dock = DockStyle.Fill
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
        mainContentHolder.Panel1.Controls.Add(addAdminButton)
        mainContentHolder.Panel1.Controls.Add(SystemHeading1)
        ' 
        ' mainContentHolder.Panel2
        ' 
        mainContentHolder.Panel2.Controls.Add(mainContents)
        mainContentHolder.Size = New Size(924, 664)
        mainContentHolder.SplitterDistance = 80
        mainContentHolder.TabIndex = 0
        ' 
        ' addAdminButton
        ' 
        addAdminButton.BackColor = Color.Transparent
        addAdminButton.BorderColor = Color.FromArgb(CByte(232), CByte(101), CByte(10))
        addAdminButton.CornerRadius = 10
        addAdminButton.FillColor = Color.FromArgb(CByte(232), CByte(101), CByte(10))
        addAdminButton.FlatAppearance.BorderSize = 0
        addAdminButton.FlatStyle = FlatStyle.Flat
        addAdminButton.Font = New Font("Inter SemiBold", 7F, FontStyle.Bold)
        addAdminButton.ForeColor = Color.White
        addAdminButton.IconHeight = 16
        addAdminButton.IconTextSpacing = 8
        addAdminButton.IconWidth = 16
        addAdminButton.Image = CType(resources.GetObject("addAdminButton.Image"), Image)
        addAdminButton.ImageAlign = ContentAlignment.MiddleLeft
        addAdminButton.Location = New Point(750, 25)
        addAdminButton.MinimumSize = New Size(50, 25)
        addAdminButton.Name = "addAdminButton"
        addAdminButton.Size = New Size(160, 32)
        addAdminButton.TabIndex = 1
        addAdminButton.Text = " Add Admin"
        addAdminButton.TextImageRelation = TextImageRelation.ImageBeforeText
        addAdminButton.UseVisualStyleBackColor = False
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
        ' mainContents
        ' 
        mainContents.Dock = DockStyle.Fill
        mainContents.FixedPanel = FixedPanel.Panel1
        mainContents.IsSplitterFixed = True
        mainContents.Location = New Point(0, 0)
        mainContents.Name = "mainContents"
        mainContents.Orientation = Orientation.Horizontal
        ' 
        ' mainContents.Panel1
        ' 
        mainContents.Panel1.Controls.Add(AdminAccountsCards1)
        mainContents.Size = New Size(924, 580)
        mainContents.SplitterDistance = 105
        mainContents.TabIndex = 0
        ' 
        ' AdminAccountsCards1
        ' 
        AdminAccountsCards1.Dock = DockStyle.Top
        AdminAccountsCards1.Location = New Point(0, 0)
        AdminAccountsCards1.Name = "AdminAccountsCards1"
        AdminAccountsCards1.Size = New Size(924, 115)
        AdminAccountsCards1.TabIndex = 1
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
        mainContents.Panel1.ResumeLayout(False)
        CType(mainContents, ComponentModel.ISupportInitialize).EndInit()
        mainContents.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents AdminAccountsMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents adminAccountsContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
    Friend WithEvents mainContents As SplitContainer
    Friend WithEvents AdminAccountsCards1 As AdminAccountsCards
    Friend WithEvents addAdminButton As RoundedButton
End Class




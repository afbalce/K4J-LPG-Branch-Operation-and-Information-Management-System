<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminRiderAccounts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SuperAdminRiderAccounts))
        RiderAccountsMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        riderAccountsContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        addRiderButton = New RoundedButton()
        SystemHeading1 = New SystemHeading()
        mainContents = New SplitContainer()
        RiderAccountsCards1 = New RiderAccountsCards()
        RiderAccountsManagement1 = New RiderAccountsManagement()
        CType(RiderAccountsMainContainer, ComponentModel.ISupportInitialize).BeginInit()
        RiderAccountsMainContainer.Panel1.SuspendLayout()
        RiderAccountsMainContainer.Panel2.SuspendLayout()
        RiderAccountsMainContainer.SuspendLayout()
        CType(riderAccountsContentContainer, ComponentModel.ISupportInitialize).BeginInit()
        riderAccountsContentContainer.Panel1.SuspendLayout()
        riderAccountsContentContainer.Panel2.SuspendLayout()
        riderAccountsContentContainer.SuspendLayout()
        CType(mainContentHolder, ComponentModel.ISupportInitialize).BeginInit()
        mainContentHolder.Panel1.SuspendLayout()
        mainContentHolder.Panel2.SuspendLayout()
        mainContentHolder.SuspendLayout()
        CType(mainContents, ComponentModel.ISupportInitialize).BeginInit()
        mainContents.Panel1.SuspendLayout()
        mainContents.Panel2.SuspendLayout()
        mainContents.SuspendLayout()
        SuspendLayout()
        ' 
        ' RiderAccountsMainContainer
        ' 
        RiderAccountsMainContainer.Dock = DockStyle.Fill
        RiderAccountsMainContainer.FixedPanel = FixedPanel.Panel1
        RiderAccountsMainContainer.IsSplitterFixed = True
        RiderAccountsMainContainer.Location = New Point(0, 0)
        RiderAccountsMainContainer.Name = "RiderAccountsMainContainer"
        ' 
        ' RiderAccountsMainContainer.Panel1
        ' 
        RiderAccountsMainContainer.Panel1.Controls.Add(SuperAdminSidebarNavigation1)
        ' 
        ' RiderAccountsMainContainer.Panel2
        ' 
        RiderAccountsMainContainer.Panel2.Controls.Add(riderAccountsContentContainer)
        RiderAccountsMainContainer.Size = New Size(1178, 724)
        RiderAccountsMainContainer.SplitterDistance = 250
        RiderAccountsMainContainer.TabIndex = 0
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
        ' riderAccountsContentContainer
        ' 
        riderAccountsContentContainer.Dock = DockStyle.Fill
        riderAccountsContentContainer.FixedPanel = FixedPanel.Panel1
        riderAccountsContentContainer.IsSplitterFixed = True
        riderAccountsContentContainer.Location = New Point(0, 0)
        riderAccountsContentContainer.Name = "riderAccountsContentContainer"
        riderAccountsContentContainer.Orientation = Orientation.Horizontal
        ' 
        ' riderAccountsContentContainer.Panel1
        ' 
        riderAccountsContentContainer.Panel1.Controls.Add(HeaderBar1)
        ' 
        ' riderAccountsContentContainer.Panel2
        ' 
        riderAccountsContentContainer.Panel2.Controls.Add(mainContentHolder)
        riderAccountsContentContainer.Size = New Size(924, 724)
        riderAccountsContentContainer.SplitterDistance = 56
        riderAccountsContentContainer.TabIndex = 0
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
        mainContentHolder.Panel1.Controls.Add(addRiderButton)
        mainContentHolder.Panel1.Controls.Add(SystemHeading1)
        ' 
        ' mainContentHolder.Panel2
        ' 
        mainContentHolder.Panel2.Controls.Add(mainContents)
        mainContentHolder.Size = New Size(924, 664)
        mainContentHolder.SplitterDistance = 80
        mainContentHolder.TabIndex = 0
        ' 
        ' addRiderButton
        ' 
        addRiderButton.BackColor = Color.Transparent
        addRiderButton.BorderColor = Color.FromArgb(CByte(232), CByte(101), CByte(10))
        addRiderButton.CornerRadius = 10
        addRiderButton.FillColor = Color.FromArgb(CByte(232), CByte(101), CByte(10))
        addRiderButton.FlatAppearance.BorderSize = 0
        addRiderButton.FlatStyle = FlatStyle.Flat
        addRiderButton.Font = New Font("Inter SemiBold", 7F, FontStyle.Bold)
        addRiderButton.ForeColor = Color.White
        addRiderButton.IconHeight = 16
        addRiderButton.IconTextSpacing = 8
        addRiderButton.IconWidth = 16
        addRiderButton.Image = CType(resources.GetObject("addRiderButton.Image"), Image)
        addRiderButton.ImageAlign = ContentAlignment.MiddleLeft
        addRiderButton.Location = New Point(750, 25)
        addRiderButton.MinimumSize = New Size(50, 25)
        addRiderButton.Name = "addRiderButton"
        addRiderButton.Size = New Size(160, 32)
        addRiderButton.TabIndex = 1
        addRiderButton.Text = "Add Rider"
        addRiderButton.TextImageRelation = TextImageRelation.ImageBeforeText
        addRiderButton.UseVisualStyleBackColor = False
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
        mainContents.Panel1.AutoScroll = True
        mainContents.Panel1.Controls.Add(RiderAccountsCards1)
        ' 
        ' mainContents.Panel2
        ' 
        mainContents.Panel2.Controls.Add(RiderAccountsManagement1)
        mainContents.Size = New Size(924, 580)
        mainContents.SplitterDistance = 105
        mainContents.TabIndex = 0
        ' 
        ' RiderAccountsCards1
        ' 
        RiderAccountsCards1.BackColor = Color.FromArgb(CByte(241), CByte(245), CByte(249))
        RiderAccountsCards1.Dock = DockStyle.Top
        RiderAccountsCards1.Location = New Point(0, 0)
        RiderAccountsCards1.Name = "RiderAccountsCards1"
        RiderAccountsCards1.Padding = New Padding(6, 10, 6, 10)
        RiderAccountsCards1.Size = New Size(924, 105)
        RiderAccountsCards1.TabIndex = 0
        ' 
        ' RiderAccountsManagement1
        ' 
        RiderAccountsManagement1.Dock = DockStyle.Fill
        RiderAccountsManagement1.Location = New Point(0, 0)
        RiderAccountsManagement1.Name = "RiderAccountsManagement1"
        RiderAccountsManagement1.Size = New Size(924, 471)
        RiderAccountsManagement1.TabIndex = 0
        ' 
        ' SuperAdminRiderAccounts
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(RiderAccountsMainContainer)
        Name = "SuperAdminRiderAccounts"
        Text = "SuperAdminRiderAccounts"
        RiderAccountsMainContainer.Panel1.ResumeLayout(False)
        RiderAccountsMainContainer.Panel2.ResumeLayout(False)
        CType(RiderAccountsMainContainer, ComponentModel.ISupportInitialize).EndInit()
        RiderAccountsMainContainer.ResumeLayout(False)
        riderAccountsContentContainer.Panel1.ResumeLayout(False)
        riderAccountsContentContainer.Panel2.ResumeLayout(False)
        CType(riderAccountsContentContainer, ComponentModel.ISupportInitialize).EndInit()
        riderAccountsContentContainer.ResumeLayout(False)
        mainContentHolder.Panel1.ResumeLayout(False)
        mainContentHolder.Panel2.ResumeLayout(False)
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        mainContents.Panel1.ResumeLayout(False)
        mainContents.Panel2.ResumeLayout(False)
        CType(mainContents, ComponentModel.ISupportInitialize).EndInit()
        mainContents.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents RiderAccountsMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents riderAccountsContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
    Friend WithEvents mainContents As SplitContainer
    Friend WithEvents RiderAccountsManagement1 As RiderAccountsManagement
    Friend WithEvents addRiderButton As RoundedButton
    Friend WithEvents RiderAccountsCards1 As RiderAccountsCards
End Class






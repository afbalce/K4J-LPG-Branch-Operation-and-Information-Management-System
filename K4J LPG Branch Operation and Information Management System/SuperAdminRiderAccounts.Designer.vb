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
        RiderAccountsMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        riderAccountsContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        SystemHeading1 = New SystemHeading()
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
        mainContentHolder.SuspendLayout()
        SuspendLayout()
        ' 
        ' RiderAccountsMainContainer
        ' 
        RiderAccountsMainContainer.Dock = DockStyle.Fill
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
        SuperAdminSidebarNavigation1.Dock = DockStyle.Left
        SuperAdminSidebarNavigation1.Location = New Point(0, 0)
        SuperAdminSidebarNavigation1.Name = "SuperAdminSidebarNavigation1"
        SuperAdminSidebarNavigation1.Size = New Size(250, 724)
        SuperAdminSidebarNavigation1.TabIndex = 0
        ' 
        ' riderAccountsContentContainer
        ' 
        riderAccountsContentContainer.Dock = DockStyle.Top
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
        riderAccountsContentContainer.TabIndex = 0
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
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents RiderAccountsMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents riderAccountsContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
End Class


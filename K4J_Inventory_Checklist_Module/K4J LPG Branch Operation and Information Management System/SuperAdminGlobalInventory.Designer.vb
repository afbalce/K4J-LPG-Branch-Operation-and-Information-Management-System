<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminGlobalInventory
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
        GlobalInventoryMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        globalInventoryContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        SystemHeading1 = New SystemHeading()
        CType(GlobalInventoryMainContainer, ComponentModel.ISupportInitialize).BeginInit()
        GlobalInventoryMainContainer.Panel1.SuspendLayout()
        GlobalInventoryMainContainer.Panel2.SuspendLayout()
        GlobalInventoryMainContainer.SuspendLayout()
        CType(globalInventoryContentContainer, ComponentModel.ISupportInitialize).BeginInit()
        globalInventoryContentContainer.Panel1.SuspendLayout()
        globalInventoryContentContainer.Panel2.SuspendLayout()
        globalInventoryContentContainer.SuspendLayout()
        CType(mainContentHolder, ComponentModel.ISupportInitialize).BeginInit()
        mainContentHolder.Panel1.SuspendLayout()
        mainContentHolder.SuspendLayout()
        SuspendLayout()
        ' 
        ' GlobalInventoryMainContainer
        ' 
        GlobalInventoryMainContainer.Dock = DockStyle.Fill
        GlobalInventoryMainContainer.Location = New Point(0, 0)
        GlobalInventoryMainContainer.Name = "GlobalInventoryMainContainer"
        ' 
        ' GlobalInventoryMainContainer.Panel1
        ' 
        GlobalInventoryMainContainer.Panel1.Controls.Add(SuperAdminSidebarNavigation1)
        ' 
        ' GlobalInventoryMainContainer.Panel2
        ' 
        GlobalInventoryMainContainer.Panel2.Controls.Add(globalInventoryContentContainer)
        GlobalInventoryMainContainer.Size = New Size(1178, 724)
        GlobalInventoryMainContainer.SplitterDistance = 250
        GlobalInventoryMainContainer.TabIndex = 0
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
        ' globalInventoryContentContainer
        ' 
        globalInventoryContentContainer.Dock = DockStyle.Top
        globalInventoryContentContainer.FixedPanel = FixedPanel.Panel1
        globalInventoryContentContainer.IsSplitterFixed = True
        globalInventoryContentContainer.Location = New Point(0, 0)
        globalInventoryContentContainer.Name = "globalInventoryContentContainer"
        globalInventoryContentContainer.Orientation = Orientation.Horizontal
        ' 
        ' globalInventoryContentContainer.Panel1
        ' 
        globalInventoryContentContainer.Panel1.Controls.Add(HeaderBar1)
        ' 
        ' globalInventoryContentContainer.Panel2
        ' 
        globalInventoryContentContainer.Panel2.Controls.Add(mainContentHolder)
        globalInventoryContentContainer.Size = New Size(924, 724)
        globalInventoryContentContainer.TabIndex = 0
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
        ' SuperAdminGlobalInventory
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(GlobalInventoryMainContainer)
        Name = "SuperAdminGlobalInventory"
        Text = "SuperAdminGlobalInventory"
        GlobalInventoryMainContainer.Panel1.ResumeLayout(False)
        GlobalInventoryMainContainer.Panel2.ResumeLayout(False)
        CType(GlobalInventoryMainContainer, ComponentModel.ISupportInitialize).EndInit()
        GlobalInventoryMainContainer.ResumeLayout(False)
        globalInventoryContentContainer.Panel1.ResumeLayout(False)
        globalInventoryContentContainer.Panel2.ResumeLayout(False)
        CType(globalInventoryContentContainer, ComponentModel.ISupportInitialize).EndInit()
        globalInventoryContentContainer.ResumeLayout(False)
        mainContentHolder.Panel1.ResumeLayout(False)
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents GlobalInventoryMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents globalInventoryContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
End Class


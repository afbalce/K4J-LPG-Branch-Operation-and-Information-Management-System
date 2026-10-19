<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminProductManagement
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
        ProductManagementMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        productManagementContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        SystemHeading1 = New SystemHeading()
        CType(ProductManagementMainContainer, ComponentModel.ISupportInitialize).BeginInit()
        ProductManagementMainContainer.Panel1.SuspendLayout()
        ProductManagementMainContainer.Panel2.SuspendLayout()
        ProductManagementMainContainer.SuspendLayout()
        CType(productManagementContentContainer, ComponentModel.ISupportInitialize).BeginInit()
        productManagementContentContainer.Panel1.SuspendLayout()
        productManagementContentContainer.Panel2.SuspendLayout()
        productManagementContentContainer.SuspendLayout()
        CType(mainContentHolder, ComponentModel.ISupportInitialize).BeginInit()
        mainContentHolder.Panel1.SuspendLayout()
        mainContentHolder.SuspendLayout()
        SuspendLayout()
        ' 
        ' ProductManagementMainContainer
        ' 
        ProductManagementMainContainer.Dock = DockStyle.Fill
        ProductManagementMainContainer.Location = New Point(0, 0)
        ProductManagementMainContainer.Name = "ProductManagementMainContainer"
        ' 
        ' ProductManagementMainContainer.Panel1
        ' 
        ProductManagementMainContainer.Panel1.Controls.Add(SuperAdminSidebarNavigation1)
        ' 
        ' ProductManagementMainContainer.Panel2
        ' 
        ProductManagementMainContainer.Panel2.Controls.Add(productManagementContentContainer)
        ProductManagementMainContainer.Size = New Size(1178, 724)
        ProductManagementMainContainer.SplitterDistance = 250
        ProductManagementMainContainer.TabIndex = 0
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
        ' productManagementContentContainer
        ' 
        productManagementContentContainer.Dock = DockStyle.Top
        productManagementContentContainer.FixedPanel = FixedPanel.Panel1
        productManagementContentContainer.IsSplitterFixed = True
        productManagementContentContainer.Location = New Point(0, 0)
        productManagementContentContainer.Name = "productManagementContentContainer"
        productManagementContentContainer.Orientation = Orientation.Horizontal
        ' 
        ' productManagementContentContainer.Panel1
        ' 
        productManagementContentContainer.Panel1.Controls.Add(HeaderBar1)
        ' 
        ' productManagementContentContainer.Panel2
        ' 
        productManagementContentContainer.Panel2.Controls.Add(mainContentHolder)
        productManagementContentContainer.Size = New Size(924, 724)
        productManagementContentContainer.TabIndex = 0
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
        ' SuperAdminProductManagement
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(ProductManagementMainContainer)
        Name = "SuperAdminProductManagement"
        Text = "SuperAdminProductManagement"
        ProductManagementMainContainer.Panel1.ResumeLayout(False)
        ProductManagementMainContainer.Panel2.ResumeLayout(False)
        CType(ProductManagementMainContainer, ComponentModel.ISupportInitialize).EndInit()
        ProductManagementMainContainer.ResumeLayout(False)
        productManagementContentContainer.Panel1.ResumeLayout(False)
        productManagementContentContainer.Panel2.ResumeLayout(False)
        CType(productManagementContentContainer, ComponentModel.ISupportInitialize).EndInit()
        productManagementContentContainer.ResumeLayout(False)
        mainContentHolder.Panel1.ResumeLayout(False)
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ProductManagementMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents productManagementContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
End Class


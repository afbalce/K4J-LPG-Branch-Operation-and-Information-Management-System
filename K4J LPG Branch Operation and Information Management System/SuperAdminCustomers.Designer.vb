<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminCustomers
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
        CustomersMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        customersContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        SystemHeading1 = New SystemHeading()
        CType(CustomersMainContainer, ComponentModel.ISupportInitialize).BeginInit()
        CustomersMainContainer.Panel1.SuspendLayout()
        CustomersMainContainer.Panel2.SuspendLayout()
        CustomersMainContainer.SuspendLayout()
        CType(customersContentContainer, ComponentModel.ISupportInitialize).BeginInit()
        customersContentContainer.Panel1.SuspendLayout()
        customersContentContainer.Panel2.SuspendLayout()
        customersContentContainer.SuspendLayout()
        CType(mainContentHolder, ComponentModel.ISupportInitialize).BeginInit()
        mainContentHolder.Panel1.SuspendLayout()
        mainContentHolder.SuspendLayout()
        SuspendLayout()
        ' 
        ' CustomersMainContainer
        ' 
        CustomersMainContainer.Dock = DockStyle.Fill
        CustomersMainContainer.Location = New Point(0, 0)
        CustomersMainContainer.Name = "CustomersMainContainer"
        ' 
        ' CustomersMainContainer.Panel1
        ' 
        CustomersMainContainer.Panel1.Controls.Add(SuperAdminSidebarNavigation1)
        ' 
        ' CustomersMainContainer.Panel2
        ' 
        CustomersMainContainer.Panel2.Controls.Add(customersContentContainer)
        CustomersMainContainer.Size = New Size(1178, 724)
        CustomersMainContainer.SplitterDistance = 250
        CustomersMainContainer.TabIndex = 0
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
        ' customersContentContainer
        ' 
        customersContentContainer.Dock = DockStyle.Top
        customersContentContainer.FixedPanel = FixedPanel.Panel1
        customersContentContainer.IsSplitterFixed = True
        customersContentContainer.Location = New Point(0, 0)
        customersContentContainer.Name = "customersContentContainer"
        customersContentContainer.Orientation = Orientation.Horizontal
        ' 
        ' customersContentContainer.Panel1
        ' 
        customersContentContainer.Panel1.Controls.Add(HeaderBar1)
        ' 
        ' customersContentContainer.Panel2
        ' 
        customersContentContainer.Panel2.Controls.Add(mainContentHolder)
        customersContentContainer.Size = New Size(924, 724)
        customersContentContainer.TabIndex = 0
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
        ' SuperAdminCustomers
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(CustomersMainContainer)
        Name = "SuperAdminCustomers"
        Text = "SuperAdminCustomers"
        CustomersMainContainer.Panel1.ResumeLayout(False)
        CustomersMainContainer.Panel2.ResumeLayout(False)
        CType(CustomersMainContainer, ComponentModel.ISupportInitialize).EndInit()
        CustomersMainContainer.ResumeLayout(False)
        customersContentContainer.Panel1.ResumeLayout(False)
        customersContentContainer.Panel2.ResumeLayout(False)
        CType(customersContentContainer, ComponentModel.ISupportInitialize).EndInit()
        customersContentContainer.ResumeLayout(False)
        mainContentHolder.Panel1.ResumeLayout(False)
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents CustomersMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents customersContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
End Class


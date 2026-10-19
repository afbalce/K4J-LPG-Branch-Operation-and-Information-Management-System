<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminDeliveryManagement
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
        DeliveryManagementMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        deliveryManagementContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        SystemHeading1 = New SystemHeading()
        CType(DeliveryManagementMainContainer, ComponentModel.ISupportInitialize).BeginInit()
        DeliveryManagementMainContainer.Panel1.SuspendLayout()
        DeliveryManagementMainContainer.Panel2.SuspendLayout()
        DeliveryManagementMainContainer.SuspendLayout()
        CType(deliveryManagementContentContainer, ComponentModel.ISupportInitialize).BeginInit()
        deliveryManagementContentContainer.Panel1.SuspendLayout()
        deliveryManagementContentContainer.Panel2.SuspendLayout()
        deliveryManagementContentContainer.SuspendLayout()
        CType(mainContentHolder, ComponentModel.ISupportInitialize).BeginInit()
        mainContentHolder.Panel1.SuspendLayout()
        mainContentHolder.SuspendLayout()
        SuspendLayout()
        ' 
        ' DeliveryManagementMainContainer
        ' 
        DeliveryManagementMainContainer.Dock = DockStyle.Fill
        DeliveryManagementMainContainer.Location = New Point(0, 0)
        DeliveryManagementMainContainer.Name = "DeliveryManagementMainContainer"
        ' 
        ' DeliveryManagementMainContainer.Panel1
        ' 
        DeliveryManagementMainContainer.Panel1.Controls.Add(SuperAdminSidebarNavigation1)
        ' 
        ' DeliveryManagementMainContainer.Panel2
        ' 
        DeliveryManagementMainContainer.Panel2.Controls.Add(deliveryManagementContentContainer)
        DeliveryManagementMainContainer.Size = New Size(1178, 724)
        DeliveryManagementMainContainer.SplitterDistance = 250
        DeliveryManagementMainContainer.TabIndex = 0
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
        ' deliveryManagementContentContainer
        ' 
        deliveryManagementContentContainer.Dock = DockStyle.Top
        deliveryManagementContentContainer.FixedPanel = FixedPanel.Panel1
        deliveryManagementContentContainer.IsSplitterFixed = True
        deliveryManagementContentContainer.Location = New Point(0, 0)
        deliveryManagementContentContainer.Name = "deliveryManagementContentContainer"
        deliveryManagementContentContainer.Orientation = Orientation.Horizontal
        ' 
        ' deliveryManagementContentContainer.Panel1
        ' 
        deliveryManagementContentContainer.Panel1.Controls.Add(HeaderBar1)
        ' 
        ' deliveryManagementContentContainer.Panel2
        ' 
        deliveryManagementContentContainer.Panel2.Controls.Add(mainContentHolder)
        deliveryManagementContentContainer.Size = New Size(924, 724)
        deliveryManagementContentContainer.TabIndex = 0
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
        ' SuperAdminDeliveryManagement
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(DeliveryManagementMainContainer)
        Name = "SuperAdminDeliveryManagement"
        Text = "SuperAdminDeliveryManagement"
        DeliveryManagementMainContainer.Panel1.ResumeLayout(False)
        DeliveryManagementMainContainer.Panel2.ResumeLayout(False)
        CType(DeliveryManagementMainContainer, ComponentModel.ISupportInitialize).EndInit()
        DeliveryManagementMainContainer.ResumeLayout(False)
        deliveryManagementContentContainer.Panel1.ResumeLayout(False)
        deliveryManagementContentContainer.Panel2.ResumeLayout(False)
        CType(deliveryManagementContentContainer, ComponentModel.ISupportInitialize).EndInit()
        deliveryManagementContentContainer.ResumeLayout(False)
        mainContentHolder.Panel1.ResumeLayout(False)
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents DeliveryManagementMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents deliveryManagementContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
End Class


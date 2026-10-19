<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RiderDelivery
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        RiderSidebarNavigation1 = New RiderSidebarNavigation()
        RightPanel = New Panel()
        ContentPanel = New Panel()
        DashboardPanel = New Panel()
        DeliveryHistoryPanel = New Panel()
        MyDeliveriesPanel = New Panel()
        SystemHeading1 = New SystemHeading()
        HeaderBar1 = New HeaderBar()
        RightPanel.SuspendLayout()
        ContentPanel.SuspendLayout()
        SuspendLayout()
        ' 
        ' RiderSidebarNavigation1
        ' 
        RiderSidebarNavigation1.BackColor = Color.FromArgb(CByte(12), CByte(22), CByte(40))
        RiderSidebarNavigation1.Dock = DockStyle.Left
        RiderSidebarNavigation1.Location = New Point(0, 0)
        RiderSidebarNavigation1.Name = "RiderSidebarNavigation1"
        RiderSidebarNavigation1.Size = New Size(250, 724)
        RiderSidebarNavigation1.TabIndex = 0
        ' 
        ' RightPanel
        ' 
        RightPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        RightPanel.BackColor = Color.FromArgb(CByte(241), CByte(245), CByte(249))
        RightPanel.Controls.Add(ContentPanel)
        RightPanel.Controls.Add(SystemHeading1)
        RightPanel.Controls.Add(HeaderBar1)
        RightPanel.Location = New Point(250, 0)
        RightPanel.Name = "RightPanel"
        RightPanel.Size = New Size(934, 741)
        RightPanel.TabIndex = 1
        ' 
        ' ContentPanel
        ' 
        ContentPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ContentPanel.Controls.Add(DeliveryHistoryPanel)
        ContentPanel.Controls.Add(MyDeliveriesPanel)
        ContentPanel.Location = New Point(0, 132)
        ContentPanel.Name = "ContentPanel"
        ContentPanel.Size = New Size(934, 609)
        ContentPanel.TabIndex = 2
        ' 
        ' DeliveryHistoryPanel
        ' 
        DeliveryHistoryPanel.Dock = DockStyle.Fill
        DeliveryHistoryPanel.Location = New Point(0, 0)
        DeliveryHistoryPanel.Name = "DeliveryHistoryPanel"
        DeliveryHistoryPanel.Size = New Size(934, 609)
        DeliveryHistoryPanel.TabIndex = 1
        DeliveryHistoryPanel.Visible = False
        ' 
        ' MyDeliveriesPanel
        ' 
        MyDeliveriesPanel.Dock = DockStyle.Fill
        MyDeliveriesPanel.Location = New Point(0, 0)
        MyDeliveriesPanel.Name = "MyDeliveriesPanel"
        MyDeliveriesPanel.Size = New Size(934, 609)
        MyDeliveriesPanel.TabIndex = 0
        ' 
        ' SystemHeading1
        ' 
        SystemHeading1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        SystemHeading1.BackColor = Color.FromArgb(CByte(241), CByte(245), CByte(249))
        SystemHeading1.Location = New Point(0, 50)
        SystemHeading1.Margin = New Padding(0)
        SystemHeading1.Name = "SystemHeading1"
        SystemHeading1.Size = New Size(934, 82)
        SystemHeading1.TabIndex = 1
        ' 
        ' HeaderBar1
        ' 
        HeaderBar1.BackColor = Color.White
        HeaderBar1.Dock = DockStyle.Top
        HeaderBar1.Location = New Point(0, 0)
        HeaderBar1.Margin = New Padding(0)
        HeaderBar1.Name = "HeaderBar1"
        HeaderBar1.Size = New Size(934, 50)
        HeaderBar1.TabIndex = 0
        ' 
        ' RiderDelivery
        ' 
        DashboardPanel.Name = "DashboardPanel"
        DashboardPanel.Dock = DockStyle.Fill
        DashboardPanel.TabIndex = 2
        ContentPanel.Controls.Add(DashboardPanel)
        MyDeliveriesPanel.BringToFront()
        AutoScaleMode = AutoScaleMode.None
        ClientSize = New Size(1178, 724)
        Controls.Add(RightPanel)
        Controls.Add(RiderSidebarNavigation1)
        Name = "RiderDelivery"
        Text = "My Deliveries - K4J LPG CENTER"
        RightPanel.ResumeLayout(False)
        ContentPanel.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents RiderSidebarNavigation1 As RiderSidebarNavigation
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents SystemHeading1 As SystemHeading
    Friend WithEvents RightPanel As Panel
    Friend WithEvents ContentPanel As Panel
    Friend WithEvents MyDeliveriesPanel As Panel
    Friend WithEvents DeliveryHistoryPanel As Panel
    Friend WithEvents DashboardPanel As Panel
End Class




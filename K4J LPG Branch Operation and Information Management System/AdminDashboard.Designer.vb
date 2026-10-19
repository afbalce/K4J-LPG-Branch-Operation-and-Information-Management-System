<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminDashboard
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
        AdminSidebarNavigation1 = New AdminSidebarNaviagtion()
        RightPanel = New Panel()
        HeaderBar1 = New HeaderBar()
        SystemHeading1 = New SystemHeading()
        ContentPanel = New Panel()
        RightPanel.SuspendLayout()
        SuspendLayout()

        AdminSidebarNavigation1.BackColor = Color.FromArgb(12, 22, 40)
        AdminSidebarNavigation1.Dock = DockStyle.Left
        AdminSidebarNavigation1.Name = "AdminSidebarNavigation1"
        AdminSidebarNavigation1.Size = New Size(250, 741)
        AdminSidebarNavigation1.TabIndex = 0

        RightPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        RightPanel.BackColor = Color.FromArgb(241, 245, 249)
        RightPanel.Location = New Point(250, 0)
        RightPanel.Name = "RightPanel"
        RightPanel.Size = New Size(934, 741)
        RightPanel.TabIndex = 1
        RightPanel.Controls.Add(ContentPanel)
        RightPanel.Controls.Add(SystemHeading1)
        RightPanel.Controls.Add(HeaderBar1)

        HeaderBar1.Dock = DockStyle.Top
        HeaderBar1.Name = "HeaderBar1"
        HeaderBar1.Size = New Size(934, 50)
        HeaderBar1.TabIndex = 0

        SystemHeading1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        SystemHeading1.Location = New Point(0, 50)
        SystemHeading1.Name = "SystemHeading1"
        SystemHeading1.Size = New Size(934, 82)
        SystemHeading1.TabIndex = 1

        ContentPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ContentPanel.Location = New Point(0, 132)
        ContentPanel.Name = "ContentPanel"
        ContentPanel.Size = New Size(934, 609)
        ContentPanel.TabIndex = 2

        AutoScaleMode = AutoScaleMode.None
        Size = New Size(1200, 780)
        Controls.Add(RightPanel)
        Controls.Add(AdminSidebarNavigation1)
        Name = "AdminDashboard"
        Text = "Dashboard - K4J LPG CENTER"
        RightPanel.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents AdminSidebarNavigation1 As AdminSidebarNaviagtion
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents SystemHeading1 As SystemHeading
    Friend WithEvents RightPanel As Panel
    Friend WithEvents ContentPanel As Panel
End Class




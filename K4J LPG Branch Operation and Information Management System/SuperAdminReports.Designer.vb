<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminReports
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
        ReportsMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        reportsContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        SystemHeading1 = New SystemHeading()
        CType(ReportsMainContainer, ComponentModel.ISupportInitialize).BeginInit()
        ReportsMainContainer.Panel1.SuspendLayout()
        ReportsMainContainer.Panel2.SuspendLayout()
        ReportsMainContainer.SuspendLayout()
        CType(reportsContentContainer, ComponentModel.ISupportInitialize).BeginInit()
        reportsContentContainer.Panel1.SuspendLayout()
        reportsContentContainer.Panel2.SuspendLayout()
        reportsContentContainer.SuspendLayout()
        CType(mainContentHolder, ComponentModel.ISupportInitialize).BeginInit()
        mainContentHolder.Panel1.SuspendLayout()
        mainContentHolder.SuspendLayout()
        SuspendLayout()
        ' 
        ' ReportsMainContainer
        ' 
        ReportsMainContainer.Dock = DockStyle.Fill
        ReportsMainContainer.Location = New Point(0, 0)
        ReportsMainContainer.Name = "ReportsMainContainer"
        ' 
        ' ReportsMainContainer.Panel1
        ' 
        ReportsMainContainer.Panel1.Controls.Add(SuperAdminSidebarNavigation1)
        ' 
        ' ReportsMainContainer.Panel2
        ' 
        ReportsMainContainer.Panel2.Controls.Add(reportsContentContainer)
        ReportsMainContainer.Size = New Size(1178, 724)
        ReportsMainContainer.SplitterDistance = 250
        ReportsMainContainer.TabIndex = 0
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
        ' reportsContentContainer
        ' 
        reportsContentContainer.Dock = DockStyle.Top
        reportsContentContainer.FixedPanel = FixedPanel.Panel1
        reportsContentContainer.IsSplitterFixed = True
        reportsContentContainer.Location = New Point(0, 0)
        reportsContentContainer.Name = "reportsContentContainer"
        reportsContentContainer.Orientation = Orientation.Horizontal
        ' 
        ' reportsContentContainer.Panel1
        ' 
        reportsContentContainer.Panel1.Controls.Add(HeaderBar1)
        ' 
        ' reportsContentContainer.Panel2
        ' 
        reportsContentContainer.Panel2.Controls.Add(mainContentHolder)
        reportsContentContainer.Size = New Size(924, 724)
        reportsContentContainer.TabIndex = 0
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
        ' SuperAdminReports
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(ReportsMainContainer)
        Name = "SuperAdminReports"
        Text = "SuperAdminReports"
        ReportsMainContainer.Panel1.ResumeLayout(False)
        ReportsMainContainer.Panel2.ResumeLayout(False)
        CType(ReportsMainContainer, ComponentModel.ISupportInitialize).EndInit()
        ReportsMainContainer.ResumeLayout(False)
        reportsContentContainer.Panel1.ResumeLayout(False)
        reportsContentContainer.Panel2.ResumeLayout(False)
        CType(reportsContentContainer, ComponentModel.ISupportInitialize).EndInit()
        reportsContentContainer.ResumeLayout(False)
        mainContentHolder.Panel1.ResumeLayout(False)
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ReportsMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents reportsContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
End Class


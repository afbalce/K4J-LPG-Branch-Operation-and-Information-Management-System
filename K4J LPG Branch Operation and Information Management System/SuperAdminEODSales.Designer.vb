<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminEODSales
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
        EODSalesMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        eODSalesContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        SystemHeading1 = New SystemHeading()
        CType(EODSalesMainContainer, ComponentModel.ISupportInitialize).BeginInit()
        EODSalesMainContainer.Panel1.SuspendLayout()
        EODSalesMainContainer.Panel2.SuspendLayout()
        EODSalesMainContainer.SuspendLayout()
        CType(eODSalesContentContainer, ComponentModel.ISupportInitialize).BeginInit()
        eODSalesContentContainer.Panel1.SuspendLayout()
        eODSalesContentContainer.Panel2.SuspendLayout()
        eODSalesContentContainer.SuspendLayout()
        CType(mainContentHolder, ComponentModel.ISupportInitialize).BeginInit()
        mainContentHolder.Panel1.SuspendLayout()
        mainContentHolder.SuspendLayout()
        SuspendLayout()
        ' 
        ' EODSalesMainContainer
        ' 
        EODSalesMainContainer.Dock = DockStyle.Fill
        EODSalesMainContainer.Location = New Point(0, 0)
        EODSalesMainContainer.Name = "EODSalesMainContainer"
        ' 
        ' EODSalesMainContainer.Panel1
        ' 
        EODSalesMainContainer.Panel1.Controls.Add(SuperAdminSidebarNavigation1)
        ' 
        ' EODSalesMainContainer.Panel2
        ' 
        EODSalesMainContainer.Panel2.Controls.Add(eODSalesContentContainer)
        EODSalesMainContainer.Size = New Size(1178, 724)
        EODSalesMainContainer.SplitterDistance = 250
        EODSalesMainContainer.TabIndex = 0
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
        ' eODSalesContentContainer
        ' 
        eODSalesContentContainer.Dock = DockStyle.Top
        eODSalesContentContainer.FixedPanel = FixedPanel.Panel1
        eODSalesContentContainer.IsSplitterFixed = True
        eODSalesContentContainer.Location = New Point(0, 0)
        eODSalesContentContainer.Name = "eODSalesContentContainer"
        eODSalesContentContainer.Orientation = Orientation.Horizontal
        ' 
        ' eODSalesContentContainer.Panel1
        ' 
        eODSalesContentContainer.Panel1.Controls.Add(HeaderBar1)
        ' 
        ' eODSalesContentContainer.Panel2
        ' 
        eODSalesContentContainer.Panel2.Controls.Add(mainContentHolder)
        eODSalesContentContainer.Size = New Size(924, 724)
        eODSalesContentContainer.TabIndex = 0
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
        ' SuperAdminEODSales
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(EODSalesMainContainer)
        Name = "SuperAdminEODSales"
        Text = "SuperAdminEODSales"
        EODSalesMainContainer.Panel1.ResumeLayout(False)
        EODSalesMainContainer.Panel2.ResumeLayout(False)
        CType(EODSalesMainContainer, ComponentModel.ISupportInitialize).EndInit()
        EODSalesMainContainer.ResumeLayout(False)
        eODSalesContentContainer.Panel1.ResumeLayout(False)
        eODSalesContentContainer.Panel2.ResumeLayout(False)
        CType(eODSalesContentContainer, ComponentModel.ISupportInitialize).EndInit()
        eODSalesContentContainer.ResumeLayout(False)
        mainContentHolder.Panel1.ResumeLayout(False)
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents EODSalesMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents eODSalesContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
End Class


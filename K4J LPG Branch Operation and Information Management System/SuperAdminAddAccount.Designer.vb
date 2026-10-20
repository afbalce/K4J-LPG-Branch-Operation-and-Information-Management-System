<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminAddAccount
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
        AddAccountMainContainer = New SplitContainer()
        SuperAdminSidebarNavigation1 = New SuperAdminSidebarNavigation()
        addAccountContentContainer = New SplitContainer()
        HeaderBar1 = New HeaderBar()
        mainContentHolder = New SplitContainer()
        SystemHeading1 = New SystemHeading()
        CType(AddAccountMainContainer, ComponentModel.ISupportInitialize).BeginInit()
        AddAccountMainContainer.Panel1.SuspendLayout()
        AddAccountMainContainer.Panel2.SuspendLayout()
        AddAccountMainContainer.SuspendLayout()
        CType(addAccountContentContainer, ComponentModel.ISupportInitialize).BeginInit()
        addAccountContentContainer.Panel1.SuspendLayout()
        addAccountContentContainer.Panel2.SuspendLayout()
        addAccountContentContainer.SuspendLayout()
        CType(mainContentHolder, ComponentModel.ISupportInitialize).BeginInit()
        mainContentHolder.Panel1.SuspendLayout()
        mainContentHolder.SuspendLayout()
        SuspendLayout()
        ' 
        ' AddAccountMainContainer
        ' 
        AddAccountMainContainer.Dock = DockStyle.Fill
        AddAccountMainContainer.FixedPanel = FixedPanel.Panel1
        AddAccountMainContainer.IsSplitterFixed = True
        AddAccountMainContainer.Location = New Point(0, 0)
        AddAccountMainContainer.Name = "AddAccountMainContainer"
        ' 
        ' AddAccountMainContainer.Panel1
        ' 
        AddAccountMainContainer.Panel1.Controls.Add(SuperAdminSidebarNavigation1)
        ' 
        ' AddAccountMainContainer.Panel2
        ' 
        AddAccountMainContainer.Panel2.Controls.Add(addAccountContentContainer)
        AddAccountMainContainer.Size = New Size(1178, 724)
        AddAccountMainContainer.SplitterDistance = 250
        AddAccountMainContainer.TabIndex = 0
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
        ' addAccountContentContainer
        ' 
        addAccountContentContainer.Dock = DockStyle.Fill
        addAccountContentContainer.FixedPanel = FixedPanel.Panel1
        addAccountContentContainer.IsSplitterFixed = True
        addAccountContentContainer.Location = New Point(0, 0)
        addAccountContentContainer.Name = "addAccountContentContainer"
        addAccountContentContainer.Orientation = Orientation.Horizontal
        ' 
        ' addAccountContentContainer.Panel1
        ' 
        addAccountContentContainer.Panel1.Controls.Add(HeaderBar1)
        ' 
        ' addAccountContentContainer.Panel2
        ' 
        addAccountContentContainer.Panel2.Controls.Add(mainContentHolder)
        addAccountContentContainer.Size = New Size(924, 724)
        addAccountContentContainer.SplitterDistance = 56
        addAccountContentContainer.TabIndex = 0
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
        mainContentHolder.Panel1.Controls.Add(SystemHeading1)
        ' 
        ' mainContentHolder.Panel2
        ' 
        mainContentHolder.Panel2.AutoScroll = True
        mainContentHolder.Panel2.BackColor = Color.FromArgb(CByte(241), CByte(245), CByte(249))
        mainContentHolder.Size = New Size(924, 664)
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
        ' SuperAdminAddAccount
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(AddAccountMainContainer)
        Name = "SuperAdminAddAccount"
        Text = "Add Admin Account"
        AddAccountMainContainer.Panel1.ResumeLayout(False)
        AddAccountMainContainer.Panel2.ResumeLayout(False)
        CType(AddAccountMainContainer, ComponentModel.ISupportInitialize).EndInit()
        AddAccountMainContainer.ResumeLayout(False)
        addAccountContentContainer.Panel1.ResumeLayout(False)
        addAccountContentContainer.Panel2.ResumeLayout(False)
        CType(addAccountContentContainer, ComponentModel.ISupportInitialize).EndInit()
        addAccountContentContainer.ResumeLayout(False)
        mainContentHolder.Panel1.ResumeLayout(False)
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents AddAccountMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents addAccountContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents SystemHeading1 As SystemHeading
End Class




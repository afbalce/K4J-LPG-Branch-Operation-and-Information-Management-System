<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SuperAdminAddAdminAccount
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
        headingSplitContainer = New SplitContainer()
        backButton = New RoundedButton()
        superAdminLabel = New Label()
        SystemHeading1 = New SystemHeading()
        AddEmployeeAccount1 = New AddEmployeeAccount()
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
        mainContentHolder.Panel2.SuspendLayout()
        mainContentHolder.SuspendLayout()
        CType(headingSplitContainer, ComponentModel.ISupportInitialize).BeginInit()
        headingSplitContainer.Panel1.SuspendLayout()
        headingSplitContainer.Panel2.SuspendLayout()
        headingSplitContainer.SuspendLayout()
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
        mainContentHolder.Panel1.Controls.Add(headingSplitContainer)
        ' 
        ' mainContentHolder.Panel2
        ' 
        mainContentHolder.Panel2.AutoScroll = True
        mainContentHolder.Panel2.BackColor = Color.FromArgb(CByte(241), CByte(245), CByte(249))
        mainContentHolder.Panel2.Controls.Add(AddEmployeeAccount1)
        mainContentHolder.Size = New Size(924, 664)
        mainContentHolder.SplitterDistance = 80
        mainContentHolder.TabIndex = 0
        ' 
        ' headingSplitContainer
        ' 
        headingSplitContainer.BackColor = Color.FromArgb(CByte(241), CByte(245), CByte(249))
        headingSplitContainer.Dock = DockStyle.Fill
        headingSplitContainer.FixedPanel = FixedPanel.Panel1
        headingSplitContainer.IsSplitterFixed = True
        headingSplitContainer.Location = New Point(0, 0)
        headingSplitContainer.Name = "headingSplitContainer"
        ' 
        ' headingSplitContainer.Panel1
        ' 
        headingSplitContainer.Panel1.Controls.Add(backButton)
        ' 
        ' headingSplitContainer.Panel2
        ' 
        headingSplitContainer.Panel2.Controls.Add(superAdminLabel)
        headingSplitContainer.Panel2.Controls.Add(SystemHeading1)
        headingSplitContainer.Size = New Size(924, 80)
        headingSplitContainer.SplitterWidth = 1
        headingSplitContainer.TabIndex = 0
        headingSplitContainer.TabStop = False
        ' 
        ' backButton
        ' 
        backButton.AccessibleName = "Back to Admin Accounts"
        backButton.BackColor = Color.Transparent
        backButton.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        backButton.BorderThickness = 1
        backButton.CornerRadius = 8
        backButton.Cursor = Cursors.Hand
        backButton.FillColor = Color.White
        backButton.FlatStyle = FlatStyle.Flat
        backButton.Font = New Font("Inter", 6.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        backButton.ForeColor = Color.FromArgb(CByte(100), CByte(116), CByte(139))
        backButton.ImageAlign = ContentAlignment.MiddleLeft
        backButton.Location = New Point(5, 20)
        backButton.MinimumSize = New Size(30, 25)
        backButton.Name = "backButton"
        backButton.Size = New Size(40, 40)
        backButton.TabIndex = 0
        backButton.Text = "←"
        backButton.HorizontalContentInset = 0
        backButton.DrawBackArrow = True
        backButton.TextImageRelation = TextImageRelation.ImageBeforeText
        backButton.UseVisualStyleBackColor = False
        ' 
        ' superAdminLabel
        ' 
        superAdminLabel.AutoSize = True
        superAdminLabel.BackColor = Color.Transparent
        superAdminLabel.Font = New Font("Inter", 6.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        superAdminLabel.ForeColor = Color.FromArgb(CByte(153), CByte(161), CByte(175))
        superAdminLabel.Location = New Point(32, 0)
        superAdminLabel.Name = "superAdminLabel"
        superAdminLabel.Size = New Size(94, 15)
        superAdminLabel.TabIndex = 1
        superAdminLabel.Text = "Super Admin ->"
        ' 
        ' SystemHeading1
        ' 
        SystemHeading1.BackColor = Color.FromArgb(CByte(241), CByte(245), CByte(249))
        SystemHeading1.Dock = DockStyle.Top
        SystemHeading1.Location = New Point(0, 0)
        SystemHeading1.Margin = New Padding(0)
        SystemHeading1.Name = "SystemHeading1"
        SystemHeading1.Size = New Size(873, 82)
        SystemHeading1.TabIndex = 0
        ' 
        ' AddEmployeeAccount1
        ' 
        AddEmployeeAccount1.Dock = DockStyle.Fill
        AddEmployeeAccount1.Location = New Point(0, 0)
        AddEmployeeAccount1.Name = "AddEmployeeAccount1"
        AddEmployeeAccount1.Size = New Size(924, 580)
        AddEmployeeAccount1.TabIndex = 0
        ' 
        ' SuperAdminAddAdminAccount
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 724)
        Controls.Add(AddAccountMainContainer)
        Name = "SuperAdminAddAdminAccount"
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
        mainContentHolder.Panel2.ResumeLayout(False)
        CType(mainContentHolder, ComponentModel.ISupportInitialize).EndInit()
        mainContentHolder.ResumeLayout(False)
        headingSplitContainer.Panel1.ResumeLayout(False)
        headingSplitContainer.Panel2.ResumeLayout(False)
        headingSplitContainer.Panel2.PerformLayout()
        CType(headingSplitContainer, ComponentModel.ISupportInitialize).EndInit()
        headingSplitContainer.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents AddAccountMainContainer As SplitContainer
    Friend WithEvents SuperAdminSidebarNavigation1 As SuperAdminSidebarNavigation
    Friend WithEvents addAccountContentContainer As SplitContainer
    Friend WithEvents HeaderBar1 As HeaderBar
    Friend WithEvents mainContentHolder As SplitContainer
    Friend WithEvents headingSplitContainer As SplitContainer
    Friend WithEvents backButton As RoundedButton
    Friend WithEvents SystemHeading1 As SystemHeading
    Friend WithEvents AddEmployeeAccount1 As AddEmployeeAccount
    Friend WithEvents superAdminLabel As Label
End Class






<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminAccountsManagement
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminAccountsManagement))
        adminaccContentHolder = New RoundedTitlePanel()
        StyledDataGridView1 = New EmployeeAccountsGrid()
        colID = New DataGridViewTextBoxColumn()
        colName = New DataGridViewTextBoxColumn()
        colRole = New DataGridViewTextBoxColumn()
        colBranch = New DataGridViewTextBoxColumn()
        colStatus = New DataGridViewTextBoxColumn()
        colPhone = New DataGridViewTextBoxColumn()
        colEmail = New DataGridViewTextBoxColumn()
        colLastLogin = New DataGridViewTextBoxColumn()
        colActions = New DataGridViewTextBoxColumn()
        statusComboBox = New RoundedComboBox()
        branchesComboBox = New RoundedComboBox()
        searchButton = New RoundedButton()
        adminaccContentHolder.SuspendLayout()
        CType(StyledDataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' adminaccContentHolder
        ' 
        adminaccContentHolder.BackColor = Color.Transparent
        adminaccContentHolder.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        adminaccContentHolder.BorderOpacity = 80
        adminaccContentHolder.Controls.Add(StyledDataGridView1)
        adminaccContentHolder.Controls.Add(statusComboBox)
        adminaccContentHolder.Controls.Add(branchesComboBox)
        adminaccContentHolder.Controls.Add(searchButton)
        adminaccContentHolder.FillColor = Color.White
        adminaccContentHolder.Location = New Point(3, 3)
        adminaccContentHolder.Name = "adminaccContentHolder"
        adminaccContentHolder.Size = New Size(918, 465)
        adminaccContentHolder.TabIndex = 1
        ' 
        ' StyledDataGridView1
        ' 
        StyledDataGridView1.AllowUserToAddRows = False
        StyledDataGridView1.AutoGenerateColumns = False
        StyledDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        StyledDataGridView1.Columns.AddRange(New DataGridViewColumn() {colID, colName, colEmail, colPhone, colBranch, colRole, colLastLogin, colStatus, colActions})
        colID.Name = "colID"
        colID.HeaderText = "ID"
        colID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        colID.DefaultCellStyle.Padding = New Padding(10, 0, 10, 0)
        colID.MinimumWidth = 70
        colName.Name = "colName"
        colName.HeaderText = "NAME"
        colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        colName.DefaultCellStyle.Padding = New Padding(30, 3, 4, 3)
        colName.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        colName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        colName.FillWeight = 193F
        colName.MinimumWidth = 150
        colEmail.Name = "colEmail"
        colEmail.HeaderText = "EMAIL"
        colEmail.FillWeight = 153F
        colEmail.MinimumWidth = 101
        colPhone.Name = "colPhone"
        colPhone.HeaderText = "PHONE"
        colPhone.FillWeight = 111F
        colPhone.MinimumWidth = 74
        colBranch.Name = "colBranch"
        colBranch.HeaderText = "BRANCH"
        colBranch.FillWeight = 126F
        colBranch.MinimumWidth = 92
        colRole.Name = "colRole"
        colRole.HeaderText = "ROLE"
        colRole.FillWeight = 160F
        colRole.MinimumWidth = 118
        colRole.DefaultCellStyle.Padding = New Padding(8, 0, 8, 0)
        colRole.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        colLastLogin.Name = "colLastLogin"
        colLastLogin.HeaderText = "LAST LOGIN"
        colLastLogin.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        colLastLogin.FillWeight = 103F
        colLastLogin.MinimumWidth = 85
        colStatus.Name = "colStatus"
        colStatus.HeaderText = "STATUS"
        colStatus.FillWeight = 90F
        colStatus.MinimumWidth = 64
        colActions.Name = "colActions"
        colActions.HeaderText = "ACTIONS"
        colActions.FillWeight = 66F
        colActions.MinimumWidth = 49
        StyledDataGridView1.BackgroundColor = Color.White
        StyledDataGridView1.BodyBackgroundColor = Color.White
        StyledDataGridView1.BorderStyle = BorderStyle.None
        StyledDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None
        StyledDataGridView1.ColumnHeadersVisible = True
        StyledDataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.FromArgb(249, 250, 251)
        DataGridViewCellStyle1.Font = New Font("Inter SemiBold", 10F, FontStyle.Regular, GraphicsUnit.Pixel)
        DataGridViewCellStyle1.ForeColor = Color.FromArgb(100, 116, 139)
        DataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(249, 250, 251)
        DataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(71, 85, 105)
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        StyledDataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        StyledDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        StyledDataGridView1.ColumnHeadersHeight = 32
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = Color.White
        DataGridViewCellStyle2.Font = New Font("Inter Medium", 12F, FontStyle.Regular, GraphicsUnit.Pixel)
        DataGridViewCellStyle2.ForeColor = Color.FromArgb(CByte(54), CByte(65), CByte(83))
        DataGridViewCellStyle2.SelectionBackColor = Color.White
        DataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(CByte(15), CByte(23), CByte(42))
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        StyledDataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        StyledDataGridView1.EnableHeadersVisualStyles = False
        StyledDataGridView1.HeaderBackgroundColor = Color.FromArgb(249, 250, 251)
        StyledDataGridView1.Location = New Point(14, 61)
        StyledDataGridView1.Name = "StyledDataGridView1"
        StyledDataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        StyledDataGridView1.RowHeadersVisible = False
        StyledDataGridView1.RowHeadersWidth = 62
        StyledDataGridView1.RowTemplate.Height = 48
        StyledDataGridView1.Size = New Size(890, 389)
        StyledDataGridView1.TabIndex = 4
        ' 
        ' statusComboBox
        ' 
        statusComboBox.ArrowColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        statusComboBox.BackColor = Color.Transparent
        statusComboBox.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        statusComboBox.BorderThickness = 2
        statusComboBox.CornerRadius = 15
        statusComboBox.DropDownHeight = 106
        statusComboBox.FillColor = Color.White
        statusComboBox.Font = New Font("Segoe UI", 11F)
        statusComboBox.HintColor = Color.FromArgb(CByte(16), CByte(24), CByte(40))
        statusComboBox.HintFont = New Font("Inter", 7F)
        statusComboBox.HintFontSize = 7F
        statusComboBox.HintText = "All Status"
        statusComboBox.Location = New Point(494, 12)
        statusComboBox.MinimumSize = New Size(60, 28)
        statusComboBox.Name = "statusComboBox"
        statusComboBox.Size = New Size(410, 36)
        statusComboBox.TabIndex = 3
        statusComboBox.TabStop = False
        statusComboBox.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        statusComboBox.TextPadding = 12
        ' 
        ' branchesComboBox
        ' 
        branchesComboBox.ArrowColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        branchesComboBox.BackColor = Color.Transparent
        branchesComboBox.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        branchesComboBox.BorderThickness = 2
        branchesComboBox.CornerRadius = 15
        branchesComboBox.DropDownHeight = 106
        branchesComboBox.FillColor = Color.White
        branchesComboBox.Font = New Font("Segoe UI", 11F)
        branchesComboBox.HintColor = Color.FromArgb(CByte(16), CByte(24), CByte(40))
        branchesComboBox.HintFont = New Font("Inter", 7F)
        branchesComboBox.HintFontSize = 7F
        branchesComboBox.HintText = "All Branches"
        branchesComboBox.Location = New Point(70, 12)
        branchesComboBox.MinimumSize = New Size(60, 28)
        branchesComboBox.Name = "branchesComboBox"
        branchesComboBox.Size = New Size(410, 36)
        branchesComboBox.TabIndex = 2
        branchesComboBox.TabStop = False
        branchesComboBox.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        branchesComboBox.TextPadding = 12
        ' 
        ' searchButton
        ' 
        searchButton.BackColor = Color.Transparent
        searchButton.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        searchButton.FillColor = Color.White
        searchButton.FlatAppearance.BorderSize = 0
        searchButton.FlatStyle = FlatStyle.Flat
        searchButton.Font = New Font("Segoe UI", 11F)
        searchButton.ForeColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        searchButton.IconHeight = 24
        searchButton.IconWidth = 24
        searchButton.Image = CType(resources.GetObject("searchButton.Image"), Image)
        searchButton.ImageAlign = ContentAlignment.MiddleLeft
        searchButton.Location = New Point(14, 12)
        searchButton.MinimumSize = New Size(45, 25)
        searchButton.Name = "searchButton"
        searchButton.Size = New Size(46, 36)
        searchButton.TabIndex = 1
        searchButton.Text = ""
        searchButton.TextImageRelation = TextImageRelation.ImageBeforeText
        searchButton.UseVisualStyleBackColor = False
        ' 
        ' AdminAccountsManagement
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(adminaccContentHolder)
        Name = "AdminAccountsManagement"
        Size = New Size(924, 471)
        adminaccContentHolder.ResumeLayout(False)
        CType(StyledDataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents adminaccContentHolder As RoundedTitlePanel
    Friend WithEvents branchesComboBox As RoundedComboBox
    Friend WithEvents searchButton As RoundedButton
    Friend WithEvents statusComboBox As RoundedComboBox
    Friend WithEvents StyledDataGridView1 As EmployeeAccountsGrid
    Friend WithEvents colID As DataGridViewTextBoxColumn
    Friend WithEvents colName As DataGridViewTextBoxColumn
    Friend WithEvents colRole As DataGridViewTextBoxColumn
    Friend WithEvents colBranch As DataGridViewTextBoxColumn
    Friend WithEvents colStatus As DataGridViewTextBoxColumn
    Friend WithEvents colPhone As DataGridViewTextBoxColumn
    Friend WithEvents colEmail As DataGridViewTextBoxColumn
    Friend WithEvents colLastLogin As DataGridViewTextBoxColumn
    Friend WithEvents colActions As DataGridViewTextBoxColumn

End Class



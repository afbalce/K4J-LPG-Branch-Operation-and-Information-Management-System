Public Class AdminAccountsManagement
    Protected Overridable ReadOnly Property AccountRole As String
        Get
            Return "Admin"
        End Get
    End Property

    Friend Event AccountsReloaded()
    Private branchRows As DataTable
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If DesignMode OrElse ComponentModel.LicenseManager.UsageMode = ComponentModel.LicenseUsageMode.Designtime Then Return
        ' Keep the designer columns unbound so its grid designer retains every heading.
        For Each column As DataGridViewColumn In StyledDataGridView1.Columns
            If column IsNot colActions AndAlso column.Name <> "colToday" Then column.DataPropertyName = column.Name.Substring(3)
        Next
        adminaccContentHolder.Dock = DockStyle.None
        colBranch.FillWeight = 126F
        StyledDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        StyledDataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        StyledDataGridView1.ReadOnly = True
        StyledDataGridView1.AllowUserToAddRows = False
        StyledDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        BackColor = Color.FromArgb(241, 245, 249)
        searchButton.AccessibleName = "Apply account filters"
        searchButton.IconFileName = "search.png"
        AddHandler Me.Resize, AddressOf Arrange
        Arrange(Me, EventArgs.Empty)
        statusComboBox.Items.Clear()
        statusComboBox.Items.AddRange({"All Status", "Active", "On Leave", "Resigned", "Terminated"})
        statusComboBox.SelectedIndex = 0
        ReloadAccounts()
    End Sub

    Private Sub Arrange(sender As Object, e As EventArgs)
        Dim scale = DeviceDpi / 96.0R
        Dim inset = CInt(14 * scale), gap = CInt(10 * scale), h = CInt(36 * scale)
        Dim top = CInt(14 * scale), gridY = top * 2 + h
        Dim headerH = CInt(36 * scale), rowH = CInt(50 * scale)
        Dim cardWidth = Math.Max(1, ClientSize.Width - inset * 2)
        adminaccContentHolder.SetBounds(inset, gap, cardWidth, Math.Max(1, ClientSize.Height - 2 * gap))
        searchButton.SetBounds(inset, top, CInt(46 * scale), h)
        Dim x = searchButton.Right + gap
        Dim w = Math.Max(1, (cardWidth - x - inset - gap) \ 2)
        branchesComboBox.SetBounds(x, top, w, h)
        statusComboBox.SetBounds(x + w + gap, top, w, h)
        StyledDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        StyledDataGridView1.ColumnHeadersHeight = headerH
        StyledDataGridView1.RowTemplate.MinimumHeight = rowH
        StyledDataGridView1.RowTemplate.Height = rowH
        colID.DefaultCellStyle.Padding = New Padding(CInt(10 * scale), 0, CInt(10 * scale), 0)
        colName.DefaultCellStyle.Padding = New Padding(CInt(30 * scale), 3, CInt(4 * scale), 3)
        Dim gridWidth = statusComboBox.Right - searchButton.Left
        StyledDataGridView1.SetBounds(searchButton.Left, gridY, Math.Max(1, gridWidth), Math.Max(1, adminaccContentHolder.Height - gridY - 3))
    End Sub

    Private Shared Function DisplayBranchName(value As Object) As String
        Dim branch = Convert.ToString(value).Trim()
        Select Case branch.ToUpperInvariant()
            Case "SAMPLE JPANG BRANCH"
                Return "JPang Branch"
            Case "SAMPLE DAET BRANCH"
                Return "Daet Branch"
            Case Else
                Return branch
        End Select
    End Function

    Friend Sub ReloadAccounts() Handles searchButton.Click
        Try
            Dim previous As Integer = -1
            If branchRows IsNot Nothing AndAlso branchesComboBox.SelectedIndex > 0 Then previous = CInt(branchRows.Rows(branchesComboBox.SelectedIndex - 1)("branch_id"))
            branchRows = EmployeeData.Branches()
            branchesComboBox.Items.Clear()
            branchesComboBox.Items.Add("All Branches")
            branchesComboBox.SelectedIndex = 0
            For Each row As DataRow In branchRows.Rows
                Dim index = branchesComboBox.Items.Add(DisplayBranchName(row("branch_name")))
                If CInt(row("branch_id")) = previous Then branchesComboBox.SelectedIndex = index
            Next
            Dim branch As Object = DBNull.Value
            Dim status As Object = DBNull.Value
            If branchesComboBox.SelectedIndex > 0 Then branch = branchRows.Rows(branchesComboBox.SelectedIndex - 1)("branch_id")
            If statusComboBox.SelectedIndex > 0 Then status = statusComboBox.SelectedItem
            clickedAccount = Nothing
            StyledDataGridView1.DataSource = EmployeeData.Accounts(AccountRole, branch, status)
            StyledDataGridView1.CurrentCell = Nothing
            StyledDataGridView1.ClearSelection()
            RaiseEvent AccountsReloaded()
            Arrange(Me, EventArgs.Empty)
        Catch ex As Exception
            MessageBox.Show("Could not load " & AccountRole & " accounts: " & ex.Message)
        End Try
    End Sub
End Class


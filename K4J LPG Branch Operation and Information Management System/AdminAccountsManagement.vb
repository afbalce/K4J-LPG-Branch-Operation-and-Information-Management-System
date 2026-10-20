
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class AdminAccountsManagement

    Private searchIcon As Image

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        If DesignMode OrElse
           System.ComponentModel.LicenseManager.UsageMode =
           System.ComponentModel.LicenseUsageMode.Designtime Then Return

        ' Load MySQL records into StyledDataGridView
        LoadBranches()

        ' Existing search button icon code
        Dim matches = Controls.Find("searchButton", True)
        If matches.Length = 0 Then Return

        Dim rounded = TryCast(matches(0), RoundedButton)
        If rounded IsNot Nothing Then
            rounded.IconFileName = "search.png"
            rounded.IconWidth = CInt(18 * DeviceDpi / 96.0R)
            rounded.IconHeight = rounded.IconWidth
            rounded.ImageAlign = ContentAlignment.MiddleCenter
            Return
        End If

        Dim button = TryCast(matches(0), Button)
        If button Is Nothing OrElse searchIcon IsNot Nothing Then Return

        Dim path = IO.Path.Combine(
            AppContext.BaseDirectory, "Icons", "search.png")

        If Not IO.File.Exists(path) Then Return

        Dim iconSize = Math.Max(
            1, CInt(18 * DeviceDpi / 96.0R))

        Using source = Image.FromFile(path)
            searchIcon = New Bitmap(
                source, New Size(iconSize, iconSize))
        End Using

        button.Image = searchIcon
        button.ImageAlign = ContentAlignment.MiddleCenter
        button.TextImageRelation =
            TextImageRelation.ImageBeforeText

        AddHandler Disposed, Sub() searchIcon.Dispose()

    End Sub

    Private Sub LoadBranches()

        Dim connectionString As String =
            "Server=localhost;Port=3306;" &
            "Database=k4j_database;" &
            "Uid=root;Pwd=;"

        Dim query As String =
            "SELECT branch_id, branch_name, location, " &
            "manager_name, contact_number " &
            "FROM tbl_branch ORDER BY branch_id"

        Try
            Dim matches = Controls.Find(
                "StyledDataGridView1", True)

            If matches.Length = 0 Then
                MessageBox.Show(
                    "StyledDataGridView1 was not found.")
                Return
            End If

            Dim grid = TryCast(
                matches(0), DataGridView)

            If grid Is Nothing Then
                MessageBox.Show(
                    "StyledDataGridView must inherit DataGridView.")
                Return
            End If

            Using conn As New MySqlConnection(connectionString)
                Using adapter As New MySqlDataAdapter(query, conn)

                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    grid.AutoGenerateColumns = True
                    grid.DataSource = dt

                    grid.ReadOnly = True
                    grid.AllowUserToAddRows = False

                    grid.AutoSizeColumnsMode =
                        DataGridViewAutoSizeColumnsMode.Fill

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(
                "Error loading records: " & ex.Message,
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
        End Try

    End Sub
End Class

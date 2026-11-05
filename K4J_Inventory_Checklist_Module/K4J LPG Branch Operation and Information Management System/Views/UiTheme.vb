Imports System.Drawing
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

' ==========================================================
' UI HELPERS FOR THE INVENTORY AND CHECKLIST SCREENS
' Colors are taken from the existing project (navy sidebar,
' orange accent, light grey page).
' ==========================================================

Public Module UiTheme

    Public ReadOnly Navy As Color = Color.FromArgb(12, 22, 40)
    Public ReadOnly Orange As Color = Color.FromArgb(232, 101, 10)
    Public ReadOnly PageGrey As Color = Color.FromArgb(241, 245, 249)
    Public ReadOnly TextDark As Color = Color.FromArgb(54, 65, 83)
    Public ReadOnly TextMuted As Color = Color.FromArgb(100, 116, 139)
    Public ReadOnly LineGrey As Color = Color.FromArgb(229, 231, 235)
    Public ReadOnly DangerRed As Color = Color.FromArgb(220, 53, 69)
    Public ReadOnly SuccessGreen As Color = Color.FromArgb(21, 128, 61)
    Public ReadOnly WarningAmber As Color = Color.FromArgb(180, 83, 9)
    Public ReadOnly MissingFill As Color = Color.FromArgb(254, 226, 226)

    Public Function BodyFont(Optional size As Single = 9.5F, Optional bold As Boolean = False) As Font
        Return New Font("Segoe UI", size, If(bold, FontStyle.Bold, FontStyle.Regular))
    End Function

    Public Function MakeButton(text As String, primary As Boolean) As Button
        Dim b As New Button()
        b.Text = text
        b.AutoSize = True
        b.MinimumSize = New Size(100, 34)
        b.Padding = New Padding(10, 0, 10, 0)
        b.Font = BodyFont(9.5F, True)
        b.FlatStyle = FlatStyle.Flat
        b.Cursor = Cursors.Hand
        b.Margin = New Padding(0, 0, 8, 0)
        If primary Then
            b.BackColor = Orange
            b.ForeColor = Color.White
            b.FlatAppearance.BorderSize = 0
        Else
            b.BackColor = Color.White
            b.ForeColor = TextDark
            b.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225)
            b.FlatAppearance.BorderSize = 1
        End If
        Return b
    End Function

    Public Function MakeLabel(text As String, Optional size As Single = 9.5F,
                              Optional bold As Boolean = False, Optional muted As Boolean = False) As Label
        Dim l As New Label()
        l.Text = text
        l.AutoSize = True
        l.Font = BodyFont(size, bold)
        l.ForeColor = If(muted, TextMuted, TextDark)
        l.BackColor = Color.Transparent
        Return l
    End Function

    ''' <summary>Applies the shared look to a grid (works for any DataGridView).</summary>
    Public Sub StyleGrid(grid As DataGridView)
        grid.Font = BodyFont(9.5F)
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.AllowUserToResizeRows = False
        grid.RowHeadersVisible = False
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.MultiSelect = False
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        grid.ColumnHeadersHeight = 38
        grid.RowTemplate.Height = 34
        grid.ColumnHeadersDefaultCellStyle.Font = BodyFont(9.0F, True)
        grid.ColumnHeadersDefaultCellStyle.Padding = New Padding(6, 0, 0, 0)
        grid.DefaultCellStyle.Padding = New Padding(6, 0, 0, 0)
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255)
        grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42)
        grid.DefaultCellStyle.ForeColor = TextDark
        grid.BackgroundColor = Color.White
        grid.BorderStyle = BorderStyle.None
    End Sub

    Public Sub RightAlign(grid As DataGridView, ParamArray columnNames As String())
        For Each name As String In columnNames
            If grid.Columns.Contains(name) Then
                grid.Columns(name).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                grid.Columns(name).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
        Next
    End Sub

    ''' <summary>Shows an error in plain language. Business-rule messages are shown as they are.</summary>
    Public Sub ShowError(owner As IWin32Window, ex As Exception)
        If TypeOf ex Is BusinessRuleException Then
            MessageBox.Show(owner, ex.Message, "Cannot continue", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf TypeOf ex Is UnauthorizedAccessException Then
            MessageBox.Show(owner, ex.Message, "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        ElseIf TypeOf ex Is MySqlException Then
            MessageBox.Show(owner,
                "The database reported a problem. Check that MySQL is running in XAMPP and that the k4j_lpg database was created." &
                vbCrLf & vbCrLf & ex.Message,
                "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            MessageBox.Show(owner, "Something went wrong: " & ex.Message,
                            "Unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

End Module

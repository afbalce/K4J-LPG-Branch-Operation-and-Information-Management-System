Imports System.Windows.Forms

' Flow: Login -> Access (role recognized) -> Dashboard -> (Sign Out) -> Login
Module Program

    <STAThread>
    Sub Main()
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)   ' crisp text/graphics on scaled displays
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Do
            Dim account As UserAccount

            Using login As New LoginForm()
                If login.ShowDialog() <> DialogResult.OK Then Exit Do   ' window closed = exit app
                account = login.Account
            End Using

            Using access As New AccessForm(account)
                If access.ShowDialog() <> DialogResult.OK Then Continue Do  ' closed = back to login
            End Using

            If account.Role <> "Super Admin" Then
                MessageBox.Show("The " & account.Role & " dashboard has not been built yet.",
                                "K4J LPG Center", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Continue Do
            End If

            Using dash As New DashboardForm(account)
                If dash.ShowDialog() <> DialogResult.Retry Then Exit Do   ' Retry = Sign Out
            End Using
        Loop
    End Sub

End Module

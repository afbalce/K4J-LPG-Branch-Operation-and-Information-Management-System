Public Class UserAccount
    Public Property Email As String
    Public Property Password As String
    Public Property FullName As String
    Public Property Role As String      ' "Super Admin", "Admin", "Rider"
    Public Property Branch As String    ' "N/A" for Super Admin
End Class

Public Module AccountStore

    Private ReadOnly Accounts As List(Of UserAccount) = BuildAccounts()

    ' ---- HARDCODED ACCOUNTS (replace with database lookup later) ----
    '   Super Admin ->  Email: jestinevia@gmail.com   Password: SuperAdmin@123
    Private Function BuildAccounts() As List(Of UserAccount)
        Dim list As New List(Of UserAccount)

        Dim sa As New UserAccount()
        sa.Email = "jestinevia@gmail.com"
        sa.Password = "SuperAdmin@123"
        sa.FullName = "Jestin Evia"
        sa.Role = "Super Admin"
        sa.Branch = "N/A"
        list.Add(sa)

        ' Add Admin / Rider test accounts the same way when needed, e.g.:
        ' Dim ad As New UserAccount()
        ' ad.Email = "admin@k4j.com" : ad.Password = "Admin@123"
        ' ad.FullName = "Branch Admin" : ad.Role = "Admin" : ad.Branch = "Daet"
        ' list.Add(ad)

        Return list
    End Function

    Public Function Authenticate(email As String, password As String, role As String, branch As String) As UserAccount
        For Each a In Accounts
            If String.Equals(a.Email, email, StringComparison.OrdinalIgnoreCase) AndAlso
               a.Password = password AndAlso a.Role = role Then
                If role = "Super Admin" OrElse a.Branch = branch Then Return a
            End If
        Next
        Return Nothing
    End Function

End Module
Imports System.Data
Imports MySql.Data.MySqlClient

' ==========================================================
' SESSION AND ROLE-BASED AUTHORIZATION
'
' The login form must call AppSession.SignIn(...) after it has
' verified the password. Every service method in the inventory and
' checklist module reads the signed-in user from here and enforces
' the role and branch rules itself, so the UI cannot bypass them.
' ==========================================================

Public Module AppSession

    ' DEVELOPMENT ONLY: while the login form is not connected to the
    ' database, the inventory pages sign in a demo account automatically.
    ' Set DevAutoSignIn to False once the real login calls SignIn.
    Public Const DevAutoSignIn As Boolean = True
    Public DevBranchAdminEmail As String = "jpang.admin@k4j.local"   ' use daet.admin@k4j.local to test Daet
    Public Const DevSuperAdminEmail As String = "superadmin@k4j.local"

    Public Property Current As SessionUser

    Public Sub SignIn(user As SessionUser)
        Current = user
    End Sub

    Public Sub SignOut()
        Current = Nothing
    End Sub

    ''' <summary>Signs in the demo account when nobody is signed in (development only).</summary>
    Public Function TryDevSignIn(superAdminPage As Boolean) As Boolean
        If Current IsNot Nothing Then Return True
        If Not DevAutoSignIn Then Return False
        Try
            Dim email As String = If(superAdminPage, DevSuperAdminEmail, DevBranchAdminEmail)
            Dim user As SessionUser = UserRepository.FindActiveByEmail(email)
            If user Is Nothing Then Return False
            Current = user
            Return True
        Catch
            Return False
        End Try
    End Function

End Module

''' <summary>Role and branch checks used by every service method.</summary>
Public Module Authorization

    Public Function RequireSignedIn() As SessionUser
        If AppSession.Current Is Nothing Then
            Throw New UnauthorizedAccessException("You are not signed in.")
        End If
        Return AppSession.Current
    End Function

    Public Function RequireSuperAdmin() As SessionUser
        Dim user As SessionUser = RequireSignedIn()
        If Not user.IsSuperAdmin Then
            Throw New UnauthorizedAccessException("Only the Super Admin can do this.")
        End If
        Return user
    End Function

    ''' <summary>Branch Admin with an assigned branch. Super Admin is view-only and is refused here.</summary>
    Public Function RequireBranchAdmin() As SessionUser
        Dim user As SessionUser = RequireSignedIn()
        If Not user.IsBranchAdmin Then
            Throw New UnauthorizedAccessException("Only a Branch Admin can record inventory or submit checklists.")
        End If
        If Not user.BranchID.HasValue Then
            Throw New UnauthorizedAccessException("Your account is not assigned to a branch.")
        End If
        Return user
    End Function

    ''' <summary>
    ''' Super Admin: any branch (Nothing = all branches, shown separately).
    ''' Branch Admin: always their own branch; asking for another branch is refused.
    ''' </summary>
    Public Function ResolveBranch(user As SessionUser, requested As Integer?) As Integer?
        If user.IsSuperAdmin Then Return requested
        If Not user.BranchID.HasValue Then
            Throw New UnauthorizedAccessException("Your account is not assigned to a branch.")
        End If
        If requested.HasValue AndAlso requested.Value <> user.BranchID.Value Then
            Throw New UnauthorizedAccessException("You can only access your own branch.")
        End If
        Return user.BranchID
    End Function

End Module

Public Module UserRepository

    Public Function FindActiveByEmail(email As String) As SessionUser
        Return Db.ReadOnlyWork(Of SessionUser)(
            Function(conn)
                Dim dt As DataTable = Db.QueryTable(conn, Nothing,
                    "SELECT u.UserID, u.FullName, u.Email, u.RoleID, u.BranchID, b.BranchName " &
                    "FROM `User` u LEFT JOIN Branch b ON b.BranchID = u.BranchID " &
                    "WHERE u.Email = @email AND u.IsActive = 1",
                    Db.Param("@email", email))
                If dt.Rows.Count = 0 Then Return Nothing
                Dim r As DataRow = dt.Rows(0)
                Return New SessionUser With {
                    .UserID = Convert.ToInt32(r("UserID")),
                    .FullName = Db.Text(r, "FullName"),
                    .Email = Db.Text(r, "Email"),
                    .RoleID = Convert.ToInt32(r("RoleID")),
                    .BranchID = Db.IntOrNothing(r, "BranchID"),
                    .BranchName = Db.Text(r, "BranchName")
                }
            End Function)
    End Function

End Module

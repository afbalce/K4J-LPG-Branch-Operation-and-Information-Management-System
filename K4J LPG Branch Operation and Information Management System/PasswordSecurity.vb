Imports System
Imports System.Security.Cryptography

Public Module PasswordSecurity

    Private Const WorkFactor As Integer = 600000

    Public Function HashPassword(password As String) As String

        If password Is Nothing Then Throw New ArgumentNullException(NameOf(password))

        Dim salt(15) As Byte
        Using rng As RandomNumberGenerator = RandomNumberGenerator.Create()
            rng.GetBytes(salt)
        End Using

        Dim hash As Byte() = Rfc2898DeriveBytes.Pbkdf2(
        password, salt, WorkFactor, HashAlgorithmName.SHA256, 32)

        Return "PBKDF2-SHA256:" & WorkFactor.ToString() & ":" &
            Convert.ToBase64String(salt) & ":" &
            Convert.ToBase64String(hash)

    End Function

    Public Function VerifyPassword(password As String, storedHash As String) As Boolean

        If password Is Nothing OrElse String.IsNullOrWhiteSpace(storedHash) Then Return False

        Dim parts As String() = storedHash.Split(":"c)
        If parts.Length <> 4 OrElse parts(0) <> "PBKDF2-SHA256" Then Return False

        Dim count As Integer
        If Not Integer.TryParse(parts(1), count) OrElse
           count < 10000 OrElse count > 2000000 Then Return False

        Try
            Dim salt As Byte() = Convert.FromBase64String(parts(2))
            Dim expected As Byte() = Convert.FromBase64String(parts(3))
            If salt.Length <> 16 OrElse expected.Length <> 32 Then Return False

            Dim actual As Byte() = Rfc2898DeriveBytes.Pbkdf2(
        password, salt, count, HashAlgorithmName.SHA256, 32)

            Dim difference As Integer = 0
            For i As Integer = 0 To expected.Length - 1
                difference = difference Or (CInt(actual(i)) Xor CInt(expected(i)))
            Next
            Return difference = 0

        Catch ex As FormatException
            Return False
        End Try

    End Function

End Module

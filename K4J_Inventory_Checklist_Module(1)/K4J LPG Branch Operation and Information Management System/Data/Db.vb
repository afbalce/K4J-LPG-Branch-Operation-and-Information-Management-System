Imports System.Data
Imports MySql.Data.MySqlClient

' ==========================================================
' DATA ACCESS LAYER - CONNECTION AND QUERY HELPERS
' All SQL in this module family is parameterized.
' ==========================================================

Public Module Db

    ' XAMPP defaults: user root, empty password, database k4j_lpg.
    ' To use other credentials without editing code, set the environment
    ' variable K4J_DB_CONNECTION to a full MySQL connection string.
    Private Const DefaultConnectionString As String =
        "Server=localhost;Port=3306;Database=k4j_lpg;Uid=root;Pwd=;CharSet=utf8mb4;"

    Public Function GetConnectionString() As String
        Dim fromEnvironment As String = Environment.GetEnvironmentVariable("K4J_DB_CONNECTION")
        If Not String.IsNullOrWhiteSpace(fromEnvironment) Then Return fromEnvironment
        Return DefaultConnectionString
    End Function

    Public Function OpenConnection() As MySqlConnection
        Dim conn As New MySqlConnection(GetConnectionString())
        Try
            conn.Open()
        Catch
            conn.Dispose()
            Throw
        End Try
        Return conn
    End Function

    ''' <summary>Runs read-only work on a fresh connection.</summary>
    Public Function ReadOnlyWork(Of T)(work As Func(Of MySqlConnection, T)) As T
        Using conn As MySqlConnection = OpenConnection()
            Return work(conn)
        End Using
    End Function

    ''' <summary>Runs work inside one transaction: commit on success, rollback on any error.</summary>
    Public Function InTransaction(Of T)(work As Func(Of MySqlConnection, MySqlTransaction, T)) As T
        Using conn As MySqlConnection = OpenConnection()
            Using tx As MySqlTransaction = conn.BeginTransaction()
                Try
                    Dim result As T = work(conn, tx)
                    tx.Commit()
                    Return result
                Catch
                    Try
                        tx.Rollback()
                    Catch
                        ' connection already lost; nothing more to undo
                    End Try
                    Throw
                End Try
            End Using
        End Using
    End Function

    Public Function Param(name As String, value As Object) As MySqlParameter
        If value Is Nothing Then Return New MySqlParameter(name, DBNull.Value)
        Return New MySqlParameter(name, value)
    End Function

    Private Function BuildCommand(conn As MySqlConnection, tx As MySqlTransaction,
                                  sql As String, parameters As MySqlParameter()) As MySqlCommand
        Dim cmd As New MySqlCommand(sql, conn, tx)
        If parameters IsNot Nothing Then
            For Each p As MySqlParameter In parameters
                cmd.Parameters.Add(p)
            Next
        End If
        Return cmd
    End Function

    Public Function QueryTable(conn As MySqlConnection, tx As MySqlTransaction,
                               sql As String, ParamArray parameters As MySqlParameter()) As DataTable
        Using cmd As MySqlCommand = BuildCommand(conn, tx, sql, parameters)
            Using adapter As New MySqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function Execute(conn As MySqlConnection, tx As MySqlTransaction,
                            sql As String, ParamArray parameters As MySqlParameter()) As Integer
        Using cmd As MySqlCommand = BuildCommand(conn, tx, sql, parameters)
            Return cmd.ExecuteNonQuery()
        End Using
    End Function

    Public Function Scalar(conn As MySqlConnection, tx As MySqlTransaction,
                           sql As String, ParamArray parameters As MySqlParameter()) As Object
        Using cmd As MySqlCommand = BuildCommand(conn, tx, sql, parameters)
            Dim result As Object = cmd.ExecuteScalar()
            If result Is Nothing OrElse IsDBNull(result) Then Return Nothing
            Return result
        End Using
    End Function

    Public Function InsertAndGetId(conn As MySqlConnection, tx As MySqlTransaction,
                                   sql As String, ParamArray parameters As MySqlParameter()) As Integer
        Using cmd As MySqlCommand = BuildCommand(conn, tx, sql, parameters)
            cmd.ExecuteNonQuery()
            Return Convert.ToInt32(cmd.LastInsertedId)
        End Using
    End Function

    ' ---- DataRow readers -------------------------------------------------

    Public Function IntOrNothing(row As DataRow, column As String) As Integer?
        If row.IsNull(column) Then Return Nothing
        Return Convert.ToInt32(row(column))
    End Function

    Public Function DateOrNothing(row As DataRow, column As String) As Date?
        If row.IsNull(column) Then Return Nothing
        Return Convert.ToDateTime(row(column))
    End Function

    Public Function Text(row As DataRow, column As String) As String
        If row.IsNull(column) Then Return String.Empty
        Return Convert.ToString(row(column))
    End Function

End Module

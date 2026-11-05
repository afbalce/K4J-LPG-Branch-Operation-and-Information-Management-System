Imports MySql.Data.MySqlClient

Friend Module EmployeeData
    Friend Function Connect() As MySqlConnection
        Return New MySqlConnection(If(Environment.GetEnvironmentVariable("K4J_MYSQL_CONNECTION"), "Server=localhost;Port=3306;Database=k4j_database;Uid=root;Pwd=;Connection Timeout=5"))
    End Function

    Friend Function ReadTable(sql As String, ParamArray parameters As MySqlParameter()) As DataTable
        Using db = Connect(), cmd As New MySqlCommand(sql, db), adapter As New MySqlDataAdapter(cmd)
            For Each parameter In parameters
                cmd.Parameters.Add(parameter)
            Next
            Dim result As New DataTable()
            adapter.Fill(result)
            Return result
        End Using
    End Function

    Friend Function Branches() As DataTable
        Return ReadTable("SELECT branch_id, branch_name FROM tbl_branch ORDER BY branch_name,branch_id")
    End Function

    Friend Function Accounts(role As String, branch As Object, status As Object) As DataTable
        Return ReadTable("SELECT u.user_id AS ID, CONCAT_WS(' ', u.first_name, p.middle_name, u.last_name, p.name_extension) AS Name, CASE WHEN u.role='Admin' THEN 'Branch Admin' ELSE u.role END AS Role, u.completed_deliveries AS CompletedDeliveries, u.last_login AS LastLogin, u.username AS Username, b.branch_name AS Branch, u.employment_status AS Status, u.contact_number AS Phone, p.email AS Email, u.date_joined AS Hired, TIMESTAMPDIFF(YEAR,p.date_of_birth,CURDATE()) AS Age FROM tbl_user u JOIN tbl_branch b ON b.branch_id=u.branch_id LEFT JOIN tbl_employee_profile p ON p.user_id=u.user_id WHERE u.role=@role AND (@branch IS NULL OR u.branch_id=@branch) AND (@status IS NULL OR u.employment_status=@status) ORDER BY u.user_id",
            New MySqlParameter("@role", role), New MySqlParameter("@branch", branch), New MySqlParameter("@status", status))
    End Function
End Module


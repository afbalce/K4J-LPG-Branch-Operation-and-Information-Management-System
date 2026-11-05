Imports MySql.Data.MySqlClient

Public Class AddEmployeeAccount
    Friend EmployeeRole As String
    Public Event Finished As EventHandler
    Private branchOptions As DataTable

    Private Sub PrepareEmployeeForm()
        passField.UseSystemPasswordChar = True
        For Each field In {idNumberField, branchIDField, ageField}
            For Each editor As TextBox In field.Controls.OfType(Of TextBox)()
                editor.ReadOnly = True
            Next
        Next
        FillChoices(extensionComboBox, {"Jr.", "Sr.", "II", "III", "IV"})
        FillChoices(genderComboBox, {"Female", "Male", "Non-binary", "Prefer not to say"})
        FillChoices(employeeRelationshipComboBox, {"Parent", "Spouse", "Sibling", "Child", "Guardian", "Other"})
        FillChoices(employmentStatusComboBox, {"Active", "On Leave", "Resigned", "Terminated"})
        employmentStatusComboBox.SelectedIndex = 0
        Try
            branchOptions = EmployeeData.Branches()
            branchAssignmentComboBox.Items.Clear()
            For Each row As DataRow In branchOptions.Rows
                branchAssignmentComboBox.Items.Add(row("branch_name").ToString())
            Next
        Catch ex As MySqlException
            MessageBox.Show("Could not load branch assignments: " & ex.Message)
        End Try
    End Sub

    Private Sub FillChoices(combo As RoundedComboBox, choices As String())
        combo.Items.Clear()
        combo.Items.AddRange(choices)
        combo.SelectedIndex = -1
    End Sub

    Private Sub BranchChanged(sender As Object, e As EventArgs) Handles branchAssignmentComboBox.SelectedIndexChanged
        Dim index = branchAssignmentComboBox.SelectedIndex
        branchIDField.Text = If(index < 0 OrElse branchOptions Is Nothing, "", branchOptions.Rows(index)("branch_id").ToString())
    End Sub

    Private Sub BirthdayChanged(sender As Object, e As EventArgs) Handles dateOfBirth.ValueChanged
        If Not dateOfBirth.HasSelectedDate OrElse dateOfBirth.Value.Date > Date.Today Then
            ageField.Text = ""
            Return
        End If
        Dim birth = dateOfBirth.Value.Date
        Dim years = Date.Today.Year - birth.Year
        If birth.AddYears(years) > Date.Today Then years -= 1
        ageField.Text = years.ToString()
    End Sub

    Private Function OptionalValue(value As String) As Object
        Return If(String.IsNullOrWhiteSpace(value), CType(DBNull.Value, Object), value.Trim())
    End Function

    Private Sub SaveEmployee(sender As Object, e As EventArgs) Handles createAccountButton.Click
        If EmployeeRole <> "Admin" AndAlso EmployeeRole <> "Rider" Then Return
        Dim email As Net.Mail.MailAddress = Nothing
        If String.IsNullOrWhiteSpace(usernameField.Text) OrElse String.IsNullOrWhiteSpace(passField.Text) OrElse
            String.IsNullOrWhiteSpace(firstNameLabel.Text) OrElse String.IsNullOrWhiteSpace(lastNameField.Text) OrElse
            String.IsNullOrWhiteSpace(contactNumberField.Text) OrElse Not Net.Mail.MailAddress.TryCreate(emailField.Text.Trim(), email) OrElse
            email.Address <> emailField.Text.Trim() OrElse branchAssignmentComboBox.SelectedIndex < 0 OrElse
            employmentStatusComboBox.SelectedIndex < 0 OrElse Not dateHiredPicker.HasSelectedDate OrElse Not dateOfBirth.HasSelectedDate OrElse
            dateOfBirth.Value.Date > Date.Today OrElse dateOfBirth.Value.Date > dateHiredPicker.Value.Date Then
            MessageBox.Show("Enter the name, username, password, email and contact number. Select a branch, employment status, valid birth date and hire date.")
            Return
        End If
        Try
            Using db = EmployeeData.Connect()
                db.Open()
                Using tx = db.BeginTransaction(), user As New MySqlCommand("INSERT INTO tbl_user(first_name,last_name,username,password,contact_number,user_level,role,branch_id,employment_status,date_joined) VALUES(@first,@last,@name,@hash,@phone,@level,@role,@branch,@status,@hired)", db, tx)
                    user.Parameters.AddWithValue("@first", firstNameLabel.Text.Trim())
                    user.Parameters.AddWithValue("@last", lastNameField.Text.Trim())
                    user.Parameters.AddWithValue("@name", usernameField.Text.Trim())
                    user.Parameters.AddWithValue("@hash", PasswordSecurity.HashPassword(passField.Text))
                    user.Parameters.AddWithValue("@phone", contactNumberField.Text.Trim())
                    user.Parameters.AddWithValue("@level", If(EmployeeRole = "Admin", "Manager", "Employees"))
                    user.Parameters.AddWithValue("@role", EmployeeRole)
                    user.Parameters.AddWithValue("@branch", branchOptions.Rows(branchAssignmentComboBox.SelectedIndex)("branch_id"))
                    user.Parameters.AddWithValue("@status", employmentStatusComboBox.SelectedItem)
                    user.Parameters.AddWithValue("@hired", dateHiredPicker.Value.Date)
                    user.ExecuteNonQuery()
                    Using profile As New MySqlCommand("INSERT INTO tbl_employee_profile(user_id,email,middle_name,name_extension,date_of_birth,gender,emergency_contact_person,emergency_contact_number,emergency_relationship) VALUES(@id,@email,@middle,@ext,@dob,@gender,@person,@phone,@relationship)", db, tx)
                        profile.Parameters.AddWithValue("@id", user.LastInsertedId)
                        profile.Parameters.AddWithValue("@email", email.Address)
                        profile.Parameters.AddWithValue("@middle", OptionalValue(middleNameField.Text))
                        profile.Parameters.AddWithValue("@ext", OptionalValue(CStr(extensionComboBox.SelectedItem)))
                        profile.Parameters.AddWithValue("@dob", dateOfBirth.Value.Date)
                        profile.Parameters.AddWithValue("@gender", OptionalValue(CStr(genderComboBox.SelectedItem)))
                        profile.Parameters.AddWithValue("@person", OptionalValue(eContactPersonNameField.Text))
                        profile.Parameters.AddWithValue("@phone", OptionalValue(eContactNumberField.Text))
                        profile.Parameters.AddWithValue("@relationship", OptionalValue(CStr(employeeRelationshipComboBox.SelectedItem)))
                        profile.ExecuteNonQuery()
                    End Using
                    tx.Commit()
                    MessageBox.Show(EmployeeRole & " account created. Employee ID: " & user.LastInsertedId.ToString())
                End Using
            End Using
        Catch ex As MySqlException When ex.Number = 1062
            MessageBox.Show("That username or email is already in use.")
            Return
        Catch ex As MySqlException
            MessageBox.Show("The account could not be saved: " & ex.Message)
            Return
        End Try
        ResetAndReturn()
    End Sub

    Private Sub CancelEntry(sender As Object, e As EventArgs) Handles cancelButton.Click
        ResetAndReturn()
    End Sub

    Private Sub ResetAndReturn()
        For Each field In detailsContainer.Controls.OfType(Of RoundedTextField)()
            field.Text = ""
        Next
        For Each combo In detailsContainer.Controls.OfType(Of RoundedComboBox)()
            combo.SelectedIndex = -1
        Next
        dateOfBirth.ClearDate()
        dateHiredPicker.ClearDate()
        employmentStatusComboBox.SelectedIndex = 0
        RaiseEvent Finished(Me, EventArgs.Empty)
    End Sub
End Class


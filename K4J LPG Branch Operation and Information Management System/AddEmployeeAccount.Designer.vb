<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AddEmployeeAccount
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(AddEmployeeAccount))
        createAccountButton = New RoundedButton()
        cancelButton = New RoundedButton()
        accCredentialsTitle = New Label()
        employeInfoTitle = New Label()
        employementDetailsTitle = New Label()
        idLabel = New Label()
        usernameLabel = New Label()
        detailsContainer = New RoundedTitlePanel()
        branchAssignmentComboBox = New RoundedComboBox()
        employmentStatusComboBox = New RoundedComboBox()
        dateHiredPicker = New RoundedDatePicker()
        employementStatusLabel = New Label()
        dateHiredLabel = New Label()
        branchIDField = New RoundedTextField()
        branchIDLabel = New Label()
        branchAssignmentLabel = New Label()
        dateOfBirth = New RoundedDatePicker()
        employeeRelationshipComboBox = New RoundedComboBox()
        employeeRelationshipLabel = New Label()
        eContactNumberField = New RoundedTextField()
        eContactNumber = New Label()
        eContactPersonNameField = New RoundedTextField()
        emergencyContactPersonField = New Label()
        genderComboBox = New RoundedComboBox()
        ageField = New RoundedTextField()
        contactNumberField = New RoundedTextField()
        contactNumberLabel = New Label()
        ageLabel = New Label()
        extensionComboBox = New RoundedComboBox()
        extensionLabel = New Label()
        lastNameField = New RoundedTextField()
        lastNameLabel = New Label()
        middleNameField = New RoundedTextField()
        middleNameLabel = New Label()
        genderLabel = New Label()
        dateofBirthLabel = New Label()
        firstNameLabel = New RoundedTextField()
        fNameLabel = New Label()
        passField = New RoundedTextField()
        emailField = New RoundedTextField()
        passLabel = New Label()
        emailAddLabel = New Label()
        usernameField = New RoundedTextField()
        idNumberField = New RoundedTextField()
        detailsContainer.SuspendLayout()
        SuspendLayout()
        ' 
        ' createAccountButton
        ' 
        createAccountButton.BackColor = Color.Transparent
        createAccountButton.BorderColor = Color.Transparent
        createAccountButton.BorderThickness = 0
        createAccountButton.CornerRadius = 8
        createAccountButton.FillColor = Color.FromArgb(CByte(232), CByte(101), CByte(10))
        createAccountButton.FlatAppearance.BorderSize = 0
        createAccountButton.FlatStyle = FlatStyle.Flat
        createAccountButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        createAccountButton.ForeColor = Color.White
        createAccountButton.IconFileName = "admin_hover_white.png"
        createAccountButton.IconWidth = 18
        createAccountButton.IconHeight = 18
        createAccountButton.Image = CType(resources.GetObject("createAccountButton.Image"), Image)
        createAccountButton.IconTextSpacing = 8
        createAccountButton.ImageAlign = ContentAlignment.MiddleLeft
        createAccountButton.Location = New Point(24, 524)
        createAccountButton.MinimumSize = New Size(50, 25)
        createAccountButton.Name = "createAccountButton"
        createAccountButton.Size = New Size(140, 30)
        createAccountButton.TabIndex = 0
        createAccountButton.Text = "Create Account"
        createAccountButton.TextImageRelation = TextImageRelation.ImageBeforeText
        createAccountButton.UseVisualStyleBackColor = False
        ' 
        ' cancelButton
        ' 
        cancelButton.BackColor = Color.Transparent
        cancelButton.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        cancelButton.CornerRadius = 10
        cancelButton.FillColor = Color.White
        cancelButton.FlatAppearance.BorderSize = 0
        cancelButton.FlatStyle = FlatStyle.Flat
        cancelButton.Font = New Font("Inter SemiBold", 6F, FontStyle.Bold)
        cancelButton.ForeColor = Color.Black
        cancelButton.ImageAlign = ContentAlignment.MiddleLeft
        cancelButton.Location = New Point(170, 522)
        cancelButton.MinimumSize = New Size(50, 25)
        cancelButton.Name = "cancelButton"
        cancelButton.Size = New Size(90, 35)
        cancelButton.TabIndex = 1
        cancelButton.Text = "Cancel"
        cancelButton.TextImageRelation = TextImageRelation.ImageBeforeText
        cancelButton.UseVisualStyleBackColor = False
        ' 
        ' accCredentialsTitle
        ' 
        accCredentialsTitle.AutoSize = True
        accCredentialsTitle.Font = New Font("Inter", 6F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        accCredentialsTitle.ForeColor = Color.FromArgb(CByte(16), CByte(24), CByte(40))
        accCredentialsTitle.Location = New Point(30, 20)
        accCredentialsTitle.Name = "accCredentialsTitle"
        accCredentialsTitle.Size = New Size(155, 15)
        accCredentialsTitle.TabIndex = 2
        accCredentialsTitle.Text = "ACCOUNT CREDENTIALS"
        ' 
        ' employeInfoTitle
        ' 
        employeInfoTitle.AutoSize = True
        employeInfoTitle.Font = New Font("Inter", 6F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        employeInfoTitle.ForeColor = Color.FromArgb(CByte(16), CByte(24), CByte(40))
        employeInfoTitle.Location = New Point(30, 168)
        employeInfoTitle.Name = "employeInfoTitle"
        employeInfoTitle.Size = New Size(158, 15)
        employeInfoTitle.TabIndex = 3
        employeInfoTitle.Text = "EMPLOYEE INFORMATION"
        ' 
        ' employementDetailsTitle
        ' 
        employementDetailsTitle.AutoSize = True
        employementDetailsTitle.Font = New Font("Inter", 6F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        employementDetailsTitle.ForeColor = Color.FromArgb(CByte(16), CByte(24), CByte(40))
        employementDetailsTitle.Location = New Point(30, 371)
        employementDetailsTitle.Name = "employementDetailsTitle"
        employementDetailsTitle.Size = New Size(145, 15)
        employementDetailsTitle.TabIndex = 4
        employementDetailsTitle.Text = "EMPLOYMENT DETAILS"
        ' 
        ' idLabel
        ' 
        idLabel.AutoSize = True
        idLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        idLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        idLabel.Location = New Point(30, 47)
        idLabel.Name = "idLabel"
        idLabel.Size = New Size(134, 12)
        idLabel.TabIndex = 5
        idLabel.Text = "IDENTIFICATION NUMBER"
        ' 
        ' usernameLabel
        ' 
        usernameLabel.AutoSize = True
        usernameLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        usernameLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        usernameLabel.Location = New Point(468, 47)
        usernameLabel.Name = "usernameLabel"
        usernameLabel.Size = New Size(62, 12)
        usernameLabel.TabIndex = 8
        usernameLabel.Text = "USERNAME"
        ' 
        ' detailsContainer
        ' 
        detailsContainer.BackColor = Color.Transparent
        detailsContainer.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        detailsContainer.BorderOpacity = 80
        detailsContainer.Controls.Add(branchAssignmentComboBox)
        detailsContainer.Controls.Add(employmentStatusComboBox)
        detailsContainer.Controls.Add(dateHiredPicker)
        detailsContainer.Controls.Add(employementStatusLabel)
        detailsContainer.Controls.Add(dateHiredLabel)
        detailsContainer.Controls.Add(branchIDField)
        detailsContainer.Controls.Add(branchIDLabel)
        detailsContainer.Controls.Add(branchAssignmentLabel)
        detailsContainer.Controls.Add(dateOfBirth)
        detailsContainer.Controls.Add(employeeRelationshipComboBox)
        detailsContainer.Controls.Add(employeeRelationshipLabel)
        detailsContainer.Controls.Add(eContactNumberField)
        detailsContainer.Controls.Add(eContactNumber)
        detailsContainer.Controls.Add(eContactPersonNameField)
        detailsContainer.Controls.Add(emergencyContactPersonField)
        detailsContainer.Controls.Add(genderComboBox)
        detailsContainer.Controls.Add(ageField)
        detailsContainer.Controls.Add(contactNumberField)
        detailsContainer.Controls.Add(contactNumberLabel)
        detailsContainer.Controls.Add(ageLabel)
        detailsContainer.Controls.Add(extensionComboBox)
        detailsContainer.Controls.Add(extensionLabel)
        detailsContainer.Controls.Add(lastNameField)
        detailsContainer.Controls.Add(lastNameLabel)
        detailsContainer.Controls.Add(middleNameField)
        detailsContainer.Controls.Add(middleNameLabel)
        detailsContainer.Controls.Add(genderLabel)
        detailsContainer.Controls.Add(dateofBirthLabel)
        detailsContainer.Controls.Add(firstNameLabel)
        detailsContainer.Controls.Add(fNameLabel)
        detailsContainer.Controls.Add(passField)
        detailsContainer.Controls.Add(emailField)
        detailsContainer.Controls.Add(passLabel)
        detailsContainer.Controls.Add(emailAddLabel)
        detailsContainer.Controls.Add(usernameField)
        detailsContainer.Controls.Add(idNumberField)
        detailsContainer.Controls.Add(usernameLabel)
        detailsContainer.Controls.Add(idLabel)
        detailsContainer.Controls.Add(employementDetailsTitle)
        detailsContainer.Controls.Add(employeInfoTitle)
        detailsContainer.Controls.Add(accCredentialsTitle)
        detailsContainer.Controls.Add(cancelButton)
        detailsContainer.Controls.Add(createAccountButton)
        detailsContainer.FillColor = Color.White
        detailsContainer.Location = New Point(3, 3)
        detailsContainer.Name = "detailsContainer"
        detailsContainer.Size = New Size(918, 574)
        detailsContainer.TabIndex = 0
        ' 
        ' branchAssignmentComboBox
        ' 
        branchAssignmentComboBox.ArrowColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        branchAssignmentComboBox.BackColor = Color.Transparent
        branchAssignmentComboBox.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        branchAssignmentComboBox.BorderThickness = 2
        branchAssignmentComboBox.CornerRadius = 8
        branchAssignmentComboBox.DropDownHeight = 106
        branchAssignmentComboBox.FillColor = Color.White
        branchAssignmentComboBox.Font = New Font("Segoe UI", 11F)
        branchAssignmentComboBox.HintColor = Color.Gray
        branchAssignmentComboBox.HintFont = New Font("Inter", 5F)
        branchAssignmentComboBox.HintFontSize = 5F
        branchAssignmentComboBox.HintText = "Select Branch"
        branchAssignmentComboBox.Location = New Point(30, 411)
        branchAssignmentComboBox.MinimumSize = New Size(90, 28)
        branchAssignmentComboBox.Name = "branchAssignmentComboBox"
        branchAssignmentComboBox.Size = New Size(419, 35)
        branchAssignmentComboBox.TabIndex = 52
        branchAssignmentComboBox.TabStop = False
        branchAssignmentComboBox.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        branchAssignmentComboBox.TextPadding = 12
        ' 
        ' employmentStatusComboBox
        ' 
        employmentStatusComboBox.ArrowColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        employmentStatusComboBox.BackColor = Color.Transparent
        employmentStatusComboBox.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        employmentStatusComboBox.BorderThickness = 2
        employmentStatusComboBox.CornerRadius = 8
        employmentStatusComboBox.DropDownHeight = 106
        employmentStatusComboBox.FillColor = Color.White
        employmentStatusComboBox.Font = New Font("Segoe UI", 11F)
        employmentStatusComboBox.HintColor = Color.Gray
        employmentStatusComboBox.HintFont = New Font("Inter", 5F)
        employmentStatusComboBox.HintFontSize = 5F
        employmentStatusComboBox.HintText = "Select Status"
        employmentStatusComboBox.Location = New Point(473, 468)
        employmentStatusComboBox.MinimumSize = New Size(90, 28)
        employmentStatusComboBox.Name = "employmentStatusComboBox"
        employmentStatusComboBox.Size = New Size(419, 35)
        employmentStatusComboBox.TabIndex = 51
        employmentStatusComboBox.TabStop = False
        employmentStatusComboBox.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        employmentStatusComboBox.TextPadding = 12
        ' 
        ' dateHiredPicker
        ' 
        dateHiredPicker.BackColor = Color.Transparent
        dateHiredPicker.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        dateHiredPicker.BorderOpacity = 80
        dateHiredPicker.CalendarBackColor = Color.White
        dateHiredPicker.CalendarTitleBackColor = Color.FromArgb(CByte(30), CByte(48), CByte(84))
        dateHiredPicker.CalendarTitleForeColor = Color.White
        dateHiredPicker.CalendarTrailingForeColor = Color.Gray
        dateHiredPicker.CornerRadius = 8
        dateHiredPicker.DateFont = New Font("Inter", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dateHiredPicker.FillColor = Color.White
        dateHiredPicker.Font = New Font("Segoe UI", 9F)
        dateHiredPicker.HintColor = Color.FromArgb(CByte(150), CByte(150), CByte(150))
        dateHiredPicker.HintFont = New Font("Inter", 6F)
        dateHiredPicker.IconColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        dateHiredPicker.Location = New Point(30, 468)
        dateHiredPicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        dateHiredPicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        dateHiredPicker.MinimumSize = New Size(115, 28)
        dateHiredPicker.Name = "dateHiredPicker"
        dateHiredPicker.Size = New Size(419, 38)
        dateHiredPicker.TabIndex = 50
        dateHiredPicker.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        ' 
        ' employementStatusLabel
        ' 
        employementStatusLabel.AutoSize = True
        employementStatusLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        employementStatusLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        employementStatusLabel.Location = New Point(473, 453)
        employementStatusLabel.Name = "employementStatusLabel"
        employementStatusLabel.Size = New Size(120, 12)
        employementStatusLabel.TabIndex = 49
        employementStatusLabel.Text = "EMPLOYMENT STATUS"
        ' 
        ' dateHiredLabel
        ' 
        dateHiredLabel.AutoSize = True
        dateHiredLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        dateHiredLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        dateHiredLabel.Location = New Point(30, 453)
        dateHiredLabel.Name = "dateHiredLabel"
        dateHiredLabel.Size = New Size(64, 12)
        dateHiredLabel.TabIndex = 48
        dateHiredLabel.Text = "DATE HIRED"
        ' 
        ' branchIDField
        ' 
        branchIDField.BackColor = Color.Transparent
        branchIDField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        branchIDField.BorderThickness = 2
        branchIDField.CornerRadius = 8
        branchIDField.FillColor = Color.White
        branchIDField.Font = New Font("Inter", 6F)
        branchIDField.HintColor = Color.Gray
        branchIDField.HintFont = New Font("Inter", 5F)
        branchIDField.HintFontSize = 5F
        branchIDField.HintText = "(Automatically Generated)"
        branchIDField.Location = New Point(473, 411)
        branchIDField.MinimumSize = New Size(70, 28)
        branchIDField.Name = "branchIDField"
        branchIDField.Size = New Size(420, 35)
        branchIDField.TabIndex = 47
        branchIDField.TextAlignment = HorizontalAlignment.Left
        branchIDField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        branchIDField.TextPadding = 12
        branchIDField.UseSystemPasswordChar = False
        ' 
        ' branchIDLabel
        ' 
        branchIDLabel.AutoSize = True
        branchIDLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        branchIDLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        branchIDLabel.Location = New Point(473, 396)
        branchIDLabel.Name = "branchIDLabel"
        branchIDLabel.Size = New Size(61, 12)
        branchIDLabel.TabIndex = 45
        branchIDLabel.Text = "BRANCH ID"
        ' 
        ' branchAssignmentLabel
        ' 
        branchAssignmentLabel.AutoSize = True
        branchAssignmentLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        branchAssignmentLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        branchAssignmentLabel.Location = New Point(30, 396)
        branchAssignmentLabel.Name = "branchAssignmentLabel"
        branchAssignmentLabel.Size = New Size(120, 12)
        branchAssignmentLabel.TabIndex = 44
        branchAssignmentLabel.Text = "BRANCH ASSIGNMENT"
        ' 
        ' dateOfBirth
        ' 
        dateOfBirth.BackColor = Color.Transparent
        dateOfBirth.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        dateOfBirth.BorderOpacity = 80
        dateOfBirth.CalendarBackColor = Color.White
        dateOfBirth.CalendarTitleBackColor = Color.FromArgb(CByte(30), CByte(48), CByte(84))
        dateOfBirth.CalendarTitleForeColor = Color.White
        dateOfBirth.CalendarTrailingForeColor = Color.Gray
        dateOfBirth.CornerRadius = 8
        dateOfBirth.DateFont = New Font("Inter", 6F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dateOfBirth.FillColor = Color.White
        dateOfBirth.Font = New Font("Segoe UI", 9F)
        dateOfBirth.HintColor = Color.FromArgb(CByte(150), CByte(150), CByte(150))
        dateOfBirth.HintFont = New Font("Inter", 6F)
        dateOfBirth.IconColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        dateOfBirth.Location = New Point(274, 263)
        dateOfBirth.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        dateOfBirth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        dateOfBirth.MinimumSize = New Size(115, 28)
        dateOfBirth.Name = "dateOfBirth"
        dateOfBirth.Size = New Size(226, 38)
        dateOfBirth.TabIndex = 43
        dateOfBirth.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        ' 
        ' employeeRelationshipComboBox
        ' 
        employeeRelationshipComboBox.ArrowColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        employeeRelationshipComboBox.BackColor = Color.Transparent
        employeeRelationshipComboBox.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        employeeRelationshipComboBox.BorderThickness = 2
        employeeRelationshipComboBox.CornerRadius = 8
        employeeRelationshipComboBox.DropDownHeight = 106
        employeeRelationshipComboBox.FillColor = Color.White
        employeeRelationshipComboBox.Font = New Font("Segoe UI", 11F)
        employeeRelationshipComboBox.HintColor = Color.Gray
        employeeRelationshipComboBox.HintFont = New Font("Inter", 5F)
        employeeRelationshipComboBox.HintFontSize = 5F
        employeeRelationshipComboBox.HintText = "Select Relationship"
        employeeRelationshipComboBox.Location = New Point(611, 323)
        employeeRelationshipComboBox.MinimumSize = New Size(90, 28)
        employeeRelationshipComboBox.Name = "employeeRelationshipComboBox"
        employeeRelationshipComboBox.Size = New Size(280, 35)
        employeeRelationshipComboBox.TabIndex = 42
        employeeRelationshipComboBox.TabStop = False
        employeeRelationshipComboBox.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        employeeRelationshipComboBox.TextPadding = 12
        ' 
        ' employeeRelationshipLabel
        ' 
        employeeRelationshipLabel.AutoSize = True
        employeeRelationshipLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        employeeRelationshipLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        employeeRelationshipLabel.Location = New Point(611, 308)
        employeeRelationshipLabel.Name = "employeeRelationshipLabel"
        employeeRelationshipLabel.Size = New Size(177, 12)
        employeeRelationshipLabel.TabIndex = 41
        employeeRelationshipLabel.Text = "RELATIONSHIP TO THE EMPLOYEE"
        ' 
        ' eContactNumberField
        ' 
        eContactNumberField.BackColor = Color.Transparent
        eContactNumberField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        eContactNumberField.BorderThickness = 2
        eContactNumberField.CornerRadius = 8
        eContactNumberField.FillColor = Color.White
        eContactNumberField.Font = New Font("Inter", 6F)
        eContactNumberField.HintColor = Color.Gray
        eContactNumberField.HintFont = New Font("Inter", 5F)
        eContactNumberField.HintFontSize = 5F
        eContactNumberField.HintText = "Enter Contact Number"
        eContactNumberField.Location = New Point(318, 323)
        eContactNumberField.MinimumSize = New Size(70, 28)
        eContactNumberField.Name = "eContactNumberField"
        eContactNumberField.Size = New Size(280, 35)
        eContactNumberField.TabIndex = 40
        eContactNumberField.TextAlignment = HorizontalAlignment.Left
        eContactNumberField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        eContactNumberField.TextPadding = 12
        eContactNumberField.UseSystemPasswordChar = False
        ' 
        ' eContactNumber
        ' 
        eContactNumber.AutoSize = True
        eContactNumber.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        eContactNumber.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        eContactNumber.Location = New Point(318, 308)
        eContactNumber.Name = "eContactNumber"
        eContactNumber.Size = New Size(168, 12)
        eContactNumber.TabIndex = 39
        eContactNumber.Text = "EMERGENCY CONTACT NUMBER"
        ' 
        ' eContactPersonNameField
        ' 
        eContactPersonNameField.BackColor = Color.Transparent
        eContactPersonNameField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        eContactPersonNameField.BorderThickness = 2
        eContactPersonNameField.CornerRadius = 8
        eContactPersonNameField.FillColor = Color.White
        eContactPersonNameField.Font = New Font("Inter", 6F)
        eContactPersonNameField.HintColor = Color.Gray
        eContactPersonNameField.HintFont = New Font("Inter", 5F)
        eContactPersonNameField.HintFontSize = 5F
        eContactPersonNameField.HintText = "Enter Full Name"
        eContactPersonNameField.Location = New Point(30, 323)
        eContactPersonNameField.MinimumSize = New Size(70, 28)
        eContactPersonNameField.Name = "eContactPersonNameField"
        eContactPersonNameField.Size = New Size(280, 35)
        eContactPersonNameField.TabIndex = 38
        eContactPersonNameField.TextAlignment = HorizontalAlignment.Left
        eContactPersonNameField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        eContactPersonNameField.TextPadding = 12
        eContactPersonNameField.UseSystemPasswordChar = False
        ' 
        ' emergencyContactPersonField
        ' 
        emergencyContactPersonField.AutoSize = True
        emergencyContactPersonField.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        emergencyContactPersonField.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        emergencyContactPersonField.Location = New Point(30, 308)
        emergencyContactPersonField.Name = "emergencyContactPersonField"
        emergencyContactPersonField.Size = New Size(166, 12)
        emergencyContactPersonField.TabIndex = 36
        emergencyContactPersonField.Text = "EMERGENCY CONTACT PERSON"
        ' 
        ' genderComboBox
        ' 
        genderComboBox.ArrowColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        genderComboBox.BackColor = Color.Transparent
        genderComboBox.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        genderComboBox.BorderThickness = 2
        genderComboBox.CornerRadius = 8
        genderComboBox.DropDownHeight = 106
        genderComboBox.FillColor = Color.White
        genderComboBox.Font = New Font("Segoe UI", 11F)
        genderComboBox.HintColor = Color.Gray
        genderComboBox.HintFont = New Font("Inter", 5F)
        genderComboBox.HintFontSize = 5F
        genderComboBox.HintText = "Select Gender"
        genderComboBox.Location = New Point(723, 266)
        genderComboBox.MinimumSize = New Size(90, 28)
        genderComboBox.Name = "genderComboBox"
        genderComboBox.Size = New Size(165, 35)
        genderComboBox.TabIndex = 33
        genderComboBox.TabStop = False
        genderComboBox.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        genderComboBox.TextPadding = 12
        ' 
        ' ageField
        ' 
        ageField.BackColor = Color.Transparent
        ageField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        ageField.BorderThickness = 2
        ageField.CornerRadius = 8
        ageField.FillColor = Color.White
        ageField.Font = New Font("Inter", 6F)
        ageField.HintColor = Color.Gray
        ageField.HintFont = New Font("Inter", 5F)
        ageField.HintFontSize = 5F
        ageField.HintText = "Enter age"
        ageField.Location = New Point(510, 266)
        ageField.MinimumSize = New Size(70, 28)
        ageField.Name = "ageField"
        ageField.Size = New Size(200, 35)
        ageField.TabIndex = 32
        ageField.TextAlignment = HorizontalAlignment.Left
        ageField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        ageField.TextPadding = 12
        ageField.UseSystemPasswordChar = False
        ' 
        ' contactNumberField
        ' 
        contactNumberField.BackColor = Color.Transparent
        contactNumberField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        contactNumberField.BorderThickness = 2
        contactNumberField.CornerRadius = 8
        contactNumberField.FillColor = Color.White
        contactNumberField.Font = New Font("Inter", 6F)
        contactNumberField.HintColor = Color.Gray
        contactNumberField.HintFont = New Font("Inter", 5F)
        contactNumberField.HintFontSize = 5F
        contactNumberField.HintText = "Enter Contact Number"
        contactNumberField.Location = New Point(30, 266)
        contactNumberField.MinimumSize = New Size(70, 28)
        contactNumberField.Name = "contactNumberField"
        contactNumberField.Size = New Size(230, 35)
        contactNumberField.TabIndex = 31
        contactNumberField.TextAlignment = HorizontalAlignment.Left
        contactNumberField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        contactNumberField.TextPadding = 12
        contactNumberField.UseSystemPasswordChar = False
        ' 
        ' contactNumberLabel
        ' 
        contactNumberLabel.AutoSize = True
        contactNumberLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        contactNumberLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        contactNumberLabel.Location = New Point(30, 251)
        contactNumberLabel.Name = "contactNumberLabel"
        contactNumberLabel.Size = New Size(102, 12)
        contactNumberLabel.TabIndex = 30
        contactNumberLabel.Text = "CONTACT NUMBER"
        ' 
        ' ageLabel
        ' 
        ageLabel.AutoSize = True
        ageLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        ageLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        ageLabel.Location = New Point(510, 251)
        ageLabel.Name = "ageLabel"
        ageLabel.Size = New Size(25, 12)
        ageLabel.TabIndex = 29
        ageLabel.Text = "AGE"
        ' 
        ' extensionComboBox
        ' 
        extensionComboBox.ArrowColor = Color.FromArgb(CByte(80), CByte(80), CByte(80))
        extensionComboBox.BackColor = Color.Transparent
        extensionComboBox.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        extensionComboBox.BorderThickness = 2
        extensionComboBox.CornerRadius = 8
        extensionComboBox.DropDownHeight = 106
        extensionComboBox.FillColor = Color.White
        extensionComboBox.Font = New Font("Segoe UI", 11F)
        extensionComboBox.HintColor = Color.Gray
        extensionComboBox.HintFont = New Font("Inter", 5F)
        extensionComboBox.HintFontSize = 5F
        extensionComboBox.HintText = "e.g. JR "
        extensionComboBox.Location = New Point(749, 209)
        extensionComboBox.MinimumSize = New Size(90, 28)
        extensionComboBox.Name = "extensionComboBox"
        extensionComboBox.Size = New Size(139, 35)
        extensionComboBox.TabIndex = 28
        extensionComboBox.TabStop = False
        extensionComboBox.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        extensionComboBox.TextPadding = 12
        ' 
        ' extensionLabel
        ' 
        extensionLabel.AutoSize = True
        extensionLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        extensionLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        extensionLabel.Location = New Point(753, 195)
        extensionLabel.Name = "extensionLabel"
        extensionLabel.Size = New Size(65, 12)
        extensionLabel.TabIndex = 27
        extensionLabel.Text = "EXTENSION"
        ' 
        ' lastNameField
        ' 
        lastNameField.BackColor = Color.Transparent
        lastNameField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        lastNameField.BorderThickness = 2
        lastNameField.CornerRadius = 8
        lastNameField.FillColor = Color.White
        lastNameField.Font = New Font("Inter", 6F)
        lastNameField.HintColor = Color.Gray
        lastNameField.HintFont = New Font("Inter", 5F)
        lastNameField.HintFontSize = 5F
        lastNameField.HintText = "Enter Last Name"
        lastNameField.Location = New Point(510, 209)
        lastNameField.MinimumSize = New Size(70, 28)
        lastNameField.Name = "lastNameField"
        lastNameField.Size = New Size(230, 35)
        lastNameField.TabIndex = 26
        lastNameField.TextAlignment = HorizontalAlignment.Left
        lastNameField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        lastNameField.TextPadding = 12
        lastNameField.UseSystemPasswordChar = False
        ' 
        ' lastNameLabel
        ' 
        lastNameLabel.AutoSize = True
        lastNameLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        lastNameLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        lastNameLabel.Location = New Point(514, 195)
        lastNameLabel.Name = "lastNameLabel"
        lastNameLabel.Size = New Size(65, 12)
        lastNameLabel.TabIndex = 25
        lastNameLabel.Text = "LAST NAME"
        ' 
        ' middleNameField
        ' 
        middleNameField.BackColor = Color.Transparent
        middleNameField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        middleNameField.BorderThickness = 2
        middleNameField.CornerRadius = 8
        middleNameField.FillColor = Color.White
        middleNameField.Font = New Font("Inter", 6F)
        middleNameField.HintColor = Color.Gray
        middleNameField.HintFont = New Font("Inter", 5F)
        middleNameField.HintFontSize = 5F
        middleNameField.HintText = "Enter Middle Name"
        middleNameField.Location = New Point(270, 209)
        middleNameField.MinimumSize = New Size(70, 28)
        middleNameField.Name = "middleNameField"
        middleNameField.Size = New Size(230, 35)
        middleNameField.TabIndex = 24
        middleNameField.TextAlignment = HorizontalAlignment.Left
        middleNameField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        middleNameField.TextPadding = 12
        middleNameField.UseSystemPasswordChar = False
        ' 
        ' middleNameLabel
        ' 
        middleNameLabel.AutoSize = True
        middleNameLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        middleNameLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        middleNameLabel.Location = New Point(278, 195)
        middleNameLabel.Name = "middleNameLabel"
        middleNameLabel.Size = New Size(76, 12)
        middleNameLabel.TabIndex = 23
        middleNameLabel.Text = "MIDDLE NAME"
        ' 
        ' genderLabel
        ' 
        genderLabel.AutoSize = True
        genderLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        genderLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        genderLabel.Location = New Point(723, 251)
        genderLabel.Name = "genderLabel"
        genderLabel.Size = New Size(46, 12)
        genderLabel.TabIndex = 20
        genderLabel.Text = "GENDER"
        ' 
        ' dateofBirthLabel
        ' 
        dateofBirthLabel.AutoSize = True
        dateofBirthLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        dateofBirthLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        dateofBirthLabel.Location = New Point(274, 251)
        dateofBirthLabel.Name = "dateofBirthLabel"
        dateofBirthLabel.Size = New Size(82, 12)
        dateofBirthLabel.TabIndex = 19
        dateofBirthLabel.Text = "DATE OF BIRTH"
        ' 
        ' firstNameLabel
        ' 
        firstNameLabel.BackColor = Color.Transparent
        firstNameLabel.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        firstNameLabel.BorderThickness = 2
        firstNameLabel.CornerRadius = 8
        firstNameLabel.FillColor = Color.White
        firstNameLabel.Font = New Font("Inter", 6F)
        firstNameLabel.HintColor = Color.Gray
        firstNameLabel.HintFont = New Font("Inter", 5F)
        firstNameLabel.HintFontSize = 5F
        firstNameLabel.HintText = "Enter First Name"
        firstNameLabel.Location = New Point(30, 209)
        firstNameLabel.MinimumSize = New Size(70, 28)
        firstNameLabel.Name = "firstNameLabel"
        firstNameLabel.Size = New Size(230, 35)
        firstNameLabel.TabIndex = 17
        firstNameLabel.TextAlignment = HorizontalAlignment.Left
        firstNameLabel.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        firstNameLabel.TextPadding = 12
        firstNameLabel.UseSystemPasswordChar = False
        ' 
        ' fNameLabel
        ' 
        fNameLabel.AutoSize = True
        fNameLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        fNameLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        fNameLabel.Location = New Point(30, 195)
        fNameLabel.Name = "fNameLabel"
        fNameLabel.Size = New Size(68, 12)
        fNameLabel.TabIndex = 15
        fNameLabel.Text = "FIRST NAME"
        ' 
        ' passField
        ' 
        passField.BackColor = Color.Transparent
        passField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        passField.BorderThickness = 2
        passField.CornerRadius = 8
        passField.FillColor = Color.White
        passField.Font = New Font("Inter", 6F)
        passField.HintColor = Color.Gray
        passField.HintFont = New Font("Inter", 5F)
        passField.HintFontSize = 5F
        passField.HintText = "Enter Password"
        passField.Location = New Point(468, 119)
        passField.MinimumSize = New Size(70, 28)
        passField.Name = "passField"
        passField.Size = New Size(420, 35)
        passField.TabIndex = 14
        passField.TextAlignment = HorizontalAlignment.Left
        passField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        passField.TextPadding = 12
        passField.UseSystemPasswordChar = False
        ' 
        ' emailField
        ' 
        emailField.BackColor = Color.Transparent
        emailField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        emailField.BorderThickness = 2
        emailField.CornerRadius = 8
        emailField.FillColor = Color.White
        emailField.Font = New Font("Inter", 6F)
        emailField.HintColor = Color.Gray
        emailField.HintFont = New Font("Inter", 5F)
        emailField.HintFontSize = 5F
        emailField.HintText = "Enter Email Address"
        emailField.Location = New Point(30, 119)
        emailField.MinimumSize = New Size(70, 28)
        emailField.Name = "emailField"
        emailField.Size = New Size(420, 35)
        emailField.TabIndex = 13
        emailField.TextAlignment = HorizontalAlignment.Left
        emailField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        emailField.TextPadding = 12
        emailField.UseSystemPasswordChar = False
        ' 
        ' passLabel
        ' 
        passLabel.AutoSize = True
        passLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        passLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        passLabel.Location = New Point(468, 104)
        passLabel.Name = "passLabel"
        passLabel.Size = New Size(63, 12)
        passLabel.TabIndex = 12
        passLabel.Text = "PASSWORD"
        ' 
        ' emailAddLabel
        ' 
        emailAddLabel.AutoSize = True
        emailAddLabel.Font = New Font("Inter SemiBold", 5F, FontStyle.Bold)
        emailAddLabel.ForeColor = Color.FromArgb(CByte(106), CByte(114), CByte(130))
        emailAddLabel.Location = New Point(30, 104)
        emailAddLabel.Name = "emailAddLabel"
        emailAddLabel.Size = New Size(87, 12)
        emailAddLabel.TabIndex = 11
        emailAddLabel.Text = "EMAIL ADDRESS"
        ' 
        ' usernameField
        ' 
        usernameField.BackColor = Color.Transparent
        usernameField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        usernameField.BorderThickness = 2
        usernameField.CornerRadius = 8
        usernameField.FillColor = Color.White
        usernameField.Font = New Font("Inter", 6F)
        usernameField.HintColor = Color.Gray
        usernameField.HintFont = New Font("Inter", 5F)
        usernameField.HintFontSize = 5F
        usernameField.HintText = "Enter Username"
        usernameField.Location = New Point(468, 62)
        usernameField.MinimumSize = New Size(70, 28)
        usernameField.Name = "usernameField"
        usernameField.Size = New Size(420, 35)
        usernameField.TabIndex = 10
        usernameField.TextAlignment = HorizontalAlignment.Left
        usernameField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        usernameField.TextPadding = 12
        usernameField.UseSystemPasswordChar = False
        ' 
        ' idNumberField
        ' 
        idNumberField.BackColor = Color.Transparent
        idNumberField.BorderColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        idNumberField.BorderThickness = 2
        idNumberField.CornerRadius = 8
        idNumberField.FillColor = Color.White
        idNumberField.Font = New Font("Inter", 6F)
        idNumberField.HintColor = Color.Gray
        idNumberField.HintFont = New Font("Inter", 5F)
        idNumberField.HintFontSize = 5F
        idNumberField.HintText = "(Automatically Generated)"
        idNumberField.Location = New Point(30, 62)
        idNumberField.MinimumSize = New Size(70, 28)
        idNumberField.Name = "idNumberField"
        idNumberField.Size = New Size(420, 35)
        idNumberField.TabIndex = 9
        idNumberField.TextAlignment = HorizontalAlignment.Left
        idNumberField.TextColor = Color.FromArgb(CByte(50), CByte(50), CByte(50))
        idNumberField.TextPadding = 12
        idNumberField.UseSystemPasswordChar = False
        ' 
        ' AddEmployeeAccount
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(detailsContainer)
        Name = "AddEmployeeAccount"
        Size = New Size(924, 580)
        detailsContainer.ResumeLayout(False)
        detailsContainer.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents createAccountButton As RoundedButton
    Friend WithEvents cancelButton As RoundedButton
    Friend WithEvents accCredentialsTitle As Label
    Friend WithEvents employeInfoTitle As Label
    Friend WithEvents employementDetailsTitle As Label
    Friend WithEvents idLabel As Label
    Friend WithEvents usernameLabel As Label
    Friend WithEvents detailsContainer As RoundedTitlePanel
    Friend WithEvents idNumberField As RoundedTextField
    Friend WithEvents usernameField As RoundedTextField
    Friend WithEvents passField As RoundedTextField
    Friend WithEvents emailField As RoundedTextField
    Friend WithEvents passLabel As Label
    Friend WithEvents emailAddLabel As Label
    Friend WithEvents extensionLabel As Label
    Friend WithEvents lastNameField As RoundedTextField
    Friend WithEvents lastNameLabel As Label
    Friend WithEvents middleNameField As RoundedTextField
    Friend WithEvents middleNameLabel As Label
    Friend WithEvents eContactPersonNameField As RoundedTextField
    Friend WithEvents dateofBirthLabel As Label
    Friend WithEvents firstNameLabel As RoundedTextField
    Friend WithEvents fNameLabel As Label
    Friend WithEvents extensionComboBox As RoundedComboBox
    Friend WithEvents ageLabel As Label
    Friend WithEvents genderLabel As Label
    Friend WithEvents genderComboBox As RoundedComboBox
    Friend WithEvents ageField As RoundedTextField
    Friend WithEvents contactNumberField As RoundedTextField
    Friend WithEvents contactNumberLabel As Label
    Friend WithEvents RoundedTextField1 As RoundedTextField
    Friend WithEvents emergencyContactPersonField As Label
    Friend WithEvents eContactNumberField As RoundedTextField
    Friend WithEvents eContactNumber As Label
    Friend WithEvents employeeRelationshipComboBox As RoundedComboBox
    Friend WithEvents employeeRelationshipLabel As Label
    Friend WithEvents dateOfBirth As RoundedDatePicker
    Friend WithEvents branchAssignmentComboBox As RoundedComboBox
    Friend WithEvents employmentStatusComboBox As RoundedComboBox
    Friend WithEvents dateHiredPicker As RoundedDatePicker
    Friend WithEvents employementStatusLabel As Label
    Friend WithEvents dateHiredLabel As Label
    Friend WithEvents branchIDField As RoundedTextField
    Friend WithEvents branchIDLabel As Label
    Friend WithEvents branchAssignmentLabel As Label

End Class


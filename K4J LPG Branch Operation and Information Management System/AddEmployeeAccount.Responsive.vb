Partial Public Class AddEmployeeAccount
    Private layoutReady As Boolean
    Private updatingLayout As Boolean
    Private nextTabIndex As Integer
    Private contentScale As Double = 1.0R
    Private ReadOnly layoutFonts As New Dictionary(Of String, Font)

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If DesignMode OrElse System.ComponentModel.LicenseManager.UsageMode =
            System.ComponentModel.LicenseUsageMode.Designtime Then Return
        Dock = DockStyle.Fill
        AutoScroll = True
        createAccountButton.SetIconBeforeText("admin_hover_white.png", "Create Account", 18, 18, 8)
        detailsContainer.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        layoutReady = True
        UpdateResponsiveLayout()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        UpdateResponsiveLayout()
    End Sub

    Protected Overrides Sub OnDpiChangedAfterParent(e As EventArgs)
        MyBase.OnDpiChangedAfterParent(e)
        UpdateResponsiveLayout()
    End Sub

    Private Function Px(value As Integer) As Integer
        Return CInt(value * contentScale * DeviceDpi / 96.0R)
    End Function

    Private Function LayoutFont(points As Single, style As FontStyle) As Font
        Dim key = points.ToString("F2", Globalization.CultureInfo.InvariantCulture) & ":" & CInt(style)
        If Not layoutFonts.ContainsKey(key) Then
            layoutFonts.Add(key, New Font("Segoe UI", points, style, GraphicsUnit.Point))
        End If
        Return layoutFonts(key)
    End Function

    Private Sub ApplyTypography()
        ' Scale from a fixed baseline, never from the previous resize's font.
        Dim textSize = CSng(Math.Round(Math.Clamp(10 * contentScale, 9, 11), 1))
        Dim labelSize = CSng(Math.Round(Math.Clamp(9 * contentScale, 8, 10), 1))
        For Each control As Control In detailsContainer.Controls
            Dim title = control Is accCredentialsTitle OrElse control Is employeInfoTitle OrElse
                control Is employementDetailsTitle
            control.Font = LayoutFont(If(TypeOf control Is Label AndAlso Not title, labelSize, textSize),
                If(title OrElse TypeOf control Is RoundedButton, FontStyle.Bold, FontStyle.Regular))
            If TypeOf control Is RoundedTextField Then
                DirectCast(control, RoundedTextField).HintFont = LayoutFont(textSize, FontStyle.Regular)
            ElseIf TypeOf control Is RoundedComboBox Then
                DirectCast(control, RoundedComboBox).HintFont = LayoutFont(textSize, FontStyle.Regular)
            ElseIf TypeOf control Is RoundedDatePicker Then
                Dim picker = DirectCast(control, RoundedDatePicker)
                picker.DateFont = LayoutFont(textSize, FontStyle.Regular)
                picker.HintFont = LayoutFont(textSize, FontStyle.Regular)
            End If
        Next
        createAccountButton.IconWidth = Px(18)
        createAccountButton.IconHeight = Px(18)
        createAccountButton.IconTextSpacing = Px(8)
    End Sub

    Private Sub ReleaseLayoutFonts(sender As Object, e As EventArgs) Handles Me.Disposed
        For Each cachedFont As Font In layoutFonts.Values
            cachedFont.Dispose()
        Next
        layoutFonts.Clear()
    End Sub

    Private Sub UpdateResponsiveLayout()
        If Not layoutReady OrElse updatingLayout OrElse ClientSize.Width <= 0 Then Return
        updatingLayout = True
        SuspendLayout()
        detailsContainer.SuspendLayout()
        Try
            Dim dpiScale = DeviceDpi / 96.0R
            contentScale = Math.Round(Math.Clamp(Math.Min(ClientSize.Width / dpiScale / 920.0R,
                ClientSize.Height / dpiScale / 580.0R), 0.85R, 1.1R), 2)
            ApplyTypography()
            ' Reserve scrollbar space to keep the column breakpoint stable.
            Dim viewportWidth = ClientSize.Width + If(VerticalScroll.Visible,
                SystemInformation.VerticalScrollBarWidth, 0)
            Dim width = Math.Max(Px(260), viewportWidth -
                SystemInformation.VerticalScrollBarWidth - Px(6))
            Dim inset = Px(If(width < Px(500), 16, 30))
            Dim innerWidth = width - inset * 2
            Dim columns = If(innerWidth >= Px(780), 4,
                If(innerWidth >= Px(460), 2, 1))
            Dim y = Px(20)
            nextTabIndex = 0

            PlaceTitle(accCredentialsTitle, inset, innerWidth, y)
            PlaceFields({idLabel, usernameLabel, emailAddLabel, passLabel},
                {idNumberField, usernameField, emailField, passField},
                Math.Min(2, columns), inset, innerWidth, y)
            PlaceTitle(employeInfoTitle, inset, innerWidth, y)
            PlaceFields({fNameLabel, middleNameLabel, lastNameLabel, extensionLabel},
                {firstNameLabel, middleNameField, lastNameField, extensionComboBox},
                columns, inset, innerWidth, y)
            PlaceFields({contactNumberLabel, dateofBirthLabel, ageLabel, genderLabel},
                {contactNumberField, dateOfBirth, ageField, genderComboBox},
                columns, inset, innerWidth, y)
            PlaceFields({emergencyContactPersonField, eContactNumber, employeeRelationshipLabel},
                {eContactPersonNameField, eContactNumberField, employeeRelationshipComboBox},
                If(columns = 4, 3, columns), inset, innerWidth, y)
            PlaceTitle(employementDetailsTitle, inset, innerWidth, y)
            PlaceFields({branchAssignmentLabel, branchIDLabel, dateHiredLabel, employementStatusLabel},
                {branchAssignmentComboBox, branchIDField, dateHiredPicker, employmentStatusComboBox},
                Math.Min(2, columns), inset, innerWidth, y)

            createAccountButton.SetBounds(inset, y, Px(180), Px(36))
            createAccountButton.TabIndex = nextTabIndex
            Dim buttonX = createAccountButton.Right + Px(8)
            If buttonX + Px(90) > width - inset Then
                buttonX = inset
                y += Px(43)
            End If
            cancelButton.SetBounds(buttonX, y, Px(90), Px(35))
            cancelButton.TabIndex = nextTabIndex + 1
            Dim height = y + Px(55)
            Dim host = TryCast(TopLevelControl, Form)
            If host IsNot Nothing AndAlso host.WindowState = FormWindowState.Maximized Then
                ' Use spare vertical space without enlarging fonts or forcing scrolling.
                Dim availableHeight = Math.Max(height, ClientSize.Height - Px(6))
                Dim verticalScale = Math.Min(1.35R, availableHeight / CDbl(height))
                For Each child As Control In detailsContainer.Controls
                    Dim top = CInt(child.Top * verticalScale)
                    Dim bottom = CInt(child.Bottom * verticalScale)
                    child.SetBounds(child.Left, top, child.Width,
                        If(TypeOf child Is Label, child.Height, bottom - top))
                Next
                height = CInt(height * verticalScale)
            End If
            detailsContainer.SetBounds(Px(3) + AutoScrollPosition.X,
                Px(3) + AutoScrollPosition.Y, width, height)
            AutoScrollMinSize = New Size(width + Px(6), height + Px(6))
        Finally
            detailsContainer.ResumeLayout(False)
            ResumeLayout(True)
            updatingLayout = False
        End Try
    End Sub

    Private Sub PlaceTitle(title As Label, x As Integer, width As Integer, ByRef y As Integer)
        title.AutoSize = False
        Dim height = Math.Max(Px(20), title.PreferredHeight)
        title.SetBounds(x, y, width, height)
        y += height + Px(5)
    End Sub

    Private Sub PlaceFields(labels As Label(), fields As Control(), columns As Integer,
                            x As Integer, width As Integer, ByRef y As Integer)
        Dim gap = Px(12)
        Dim cellWidth = (width - gap * (columns - 1)) \ columns
        For rowStart = 0 To fields.Length - 1 Step columns
            Dim rowCount = Math.Min(columns, fields.Length - rowStart)
            Dim labelHeight = Px(15)
            Dim fieldHeight = Px(34)
            For offset = 0 To rowCount - 1
                Dim index = rowStart + offset
                labelHeight = Math.Max(labelHeight,
                    labels(index).GetPreferredSize(New Size(cellWidth, 0)).Height)
                fieldHeight = Math.Max(fieldHeight, fields(index).MinimumSize.Height)
            Next
            For offset = 0 To rowCount - 1
                Dim index = rowStart + offset
                Dim left = x + offset * (cellWidth + gap)
                labels(index).AutoSize = False
                labels(index).SetBounds(left, y, cellWidth, labelHeight)
                fields(index).Anchor = AnchorStyles.Top Or AnchorStyles.Left
                fields(index).SetBounds(left, y + labelHeight + Px(3), cellWidth, fieldHeight)
                fields(index).TabIndex = nextTabIndex
                nextTabIndex += 1
            Next
            y += labelHeight + Px(3) + fieldHeight + Px(8)
        Next
    End Sub
End Class


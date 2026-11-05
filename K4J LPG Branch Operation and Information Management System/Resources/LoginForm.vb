Public Class LoginForm
    Inherits Form

    Public Property Account As UserAccount

    Private ReadOnly pnlLeft As New Panel()
    Private ReadOnly pnlRight As New Panel()
    Private ReadOnly leftBlock As New Panel()
    Private ReadOnly group As New Panel()
    Private ReadOnly txtEmail As New TextBox()
    Private ReadOnly txtPassword As New PasswordBox()
    Private ReadOnly cboRole As New ComboBox()
    Private ReadOnly cboBranch As New ComboBox()
    Private ReadOnly btnEye As New RoundButton()
    Private ReadOnly pnlBranch As New RoundedPanel()
    Private ReadOnly lblBranchNA As New Label()
    Private ReadOnly btnSignIn As New RoundButton()

    Public Sub New()
        Text = "K4J LPG Center Management System"
        ClientSize = New Size(1280, 833)
        MinimumSize = New Size(1000, 700)
        StartPosition = FormStartPosition.CenterScreen
        WindowState = FormWindowState.Maximized
        BackColor = PageBg
        Font = Px(13)
        KeyPreview = True
        DoubleBuffered = True

        BuildLeftPanel()
        BuildRightPanel()
        Controls.Add(pnlRight)
        Controls.Add(pnlLeft)

        cboRole.SelectedIndex = 0   ' Super Admin -> branch locked to N/A
        ScaleUi(Me, False)          ' make the whole interface bigger (see UiScale in Theme.vb)
    End Sub

    ' ---------------- Left (dark) panel ----------------
    Private Sub BuildLeftPanel()
        pnlLeft.Dock = DockStyle.Left
        pnlLeft.Width = 480
        pnlLeft.BackColor = Navy

        leftBlock.Size = New Size(410, 190)
        leftBlock.BackColor = Color.Transparent

        Dim badge = MakePill("BRANCH OPERATIONS PLATFORM", Hx("#FFF3E8"), Hx("#F59E5B"), Hx("#C2410C"),
                             Mono(10), 410, 20, ContentAlignment.MiddleLeft)
        badge.Radius = 3
        badge.Location = New Point(0, 0)
        leftBlock.Controls.Add(badge)

        Dim title As New Label() With {.Text = "K4J LPG CENTER" & vbCrLf & "MANAGEMENT SYSTEM",
                                       .Font = Heading(40), .ForeColor = Color.White, .AutoSize = True,
                                       .Location = New Point(0, 36), .BackColor = Color.Transparent}
        leftBlock.Controls.Add(title)

        Dim subtitle As New Label() With {
            .Text = "Integrated system for inventory tracking, order management, delivery monitoring, and financial reporting.",
            .Font = Px(14), .ForeColor = Hx("#8FA3C4"), .AutoSize = False, .Size = New Size(350, 44),
            .Location = New Point(0, 146), .BackColor = Color.Transparent}
        leftBlock.Controls.Add(subtitle)

        pnlLeft.Controls.Add(leftBlock)
        leftBlock.Left = 35

        Dim footer As New Label() With {
            .Text = CopyMark & " 2025 LPG Management System. Jpang & Daet Branches.",
            .Font = Px(11), .ForeColor = Hx("#3F5173"), .Dock = DockStyle.Bottom, .Height = 52,
            .TextAlign = ContentAlignment.TopLeft, .Padding = New Padding(35, 0, 0, 0)}
        pnlLeft.Controls.Add(footer)

        AddHandler pnlLeft.Resize, Sub(s, e) leftBlock.Top = Math.Max(20, (pnlLeft.Height - leftBlock.Height) \ 2 - 20)
        leftBlock.Top = (833 - leftBlock.Height) \ 2 - 20
    End Sub

    ' ---------------- Right panel ----------------
    Private Sub BuildRightPanel()
        pnlRight.Dock = DockStyle.Fill
        pnlRight.BackColor = PageBg

        group.Size = New Size(398, 468)
        group.BackColor = Color.Transparent
        pnlRight.Controls.Add(group)
        AddHandler pnlRight.Resize, Sub(s, e) CenterGroup()
        CenterGroup()

        Dim card As New RoundedPanel() With {.Bounds = New Rectangle(0, 0, 398, 392), .Radius = 12, .Shadow = True}
        group.Controls.Add(card)

        card.Controls.Add(MakeLabel("Welcome back", 28, 28, Px(19, FontStyle.Bold), Ink))
        card.Controls.Add(MakeLabel("Sign in to your branch account", 28, 56, Px(13), Muted))

        ' Email
        card.Controls.Add(MakeLabel("EMAIL ADDRESS", 28, 96, Px(11, FontStyle.Bold), Muted))
        NativeHelper.SetPlaceholder(txtEmail, "Your email address")
        card.Controls.Add(MakeInput(txtEmail, 28, 115, 342, Nothing))

        ' Password (+ eye toggle)
        card.Controls.Add(MakeLabel("PASSWORD", 28, 163, Px(11, FontStyle.Bold), Muted))
        NativeHelper.SetPlaceholder(txtPassword, "Your password")
        Dim pw = MakeInput(txtPassword, 28, 182, 342, btnEye)
        card.Controls.Add(pw)
        btnEye.Size = New Size(28, 28)
        btnEye.Location = New Point(342 - 34, 3)
        btnEye.FillColor = Color.Transparent
        btnEye.TextColor = Hx("#111827")
        btnEye.IconGlyph = Glyphs.EyeOff
        btnEye.IconSize = 16
        AddHandler btnEye.Click, AddressOf ToggleEye

        ' Role + Branch
        card.Controls.Add(MakeLabel("ROLE", 28, 231, Px(11, FontStyle.Bold), Muted))
        card.Controls.Add(MakeLabel("BRANCH", 204, 231, Px(11, FontStyle.Bold), Muted))
        cboRole.Items.AddRange(New Object() {"Super Admin", "Admin", "Rider"})
        card.Controls.Add(MakeCombo(cboRole, 28, 250, 166))
        ConfigureCombo(pnlBranch, cboBranch, 204, 250, 166)
        ' Plain "N/A" label shown instead of the combo box when Super Admin is selected (no inner border)
        lblBranchNA.Text = "N/A"
        lblBranchNA.Font = Px(13)
        lblBranchNA.ForeColor = Hx("#9CA3AF")
        lblBranchNA.BackColor = Color.Transparent
        lblBranchNA.AutoSize = False
        lblBranchNA.Bounds = New Rectangle(12, 0, 166 - 24, 30)
        lblBranchNA.TextAlign = ContentAlignment.MiddleLeft
        pnlBranch.Controls.Add(lblBranchNA)
        card.Controls.Add(pnlBranch)
        AddHandler cboRole.SelectedIndexChanged, AddressOf RoleChanged

        ' Sign in
        btnSignIn.Text = "Sign In"
        btnSignIn.IconGlyph = Glyphs.Lock
        btnSignIn.IconSize = 14
        btnSignIn.Font = Px(14, FontStyle.Bold)
        btnSignIn.FillColor = Orange
        btnSignIn.HoverColor = Hx("#D45A07")
        btnSignIn.Bounds = New Rectangle(28, 294, 342, 36)
        AddHandler btnSignIn.Click, Sub(s, e) DoSignIn()
        card.Controls.Add(btnSignIn)

        Dim forgot As New Label() With {.Text = "Forgot your password?", .Font = Px(12, FontStyle.Bold),
                                        .ForeColor = Orange, .AutoSize = False, .Bounds = New Rectangle(28, 344, 342, 18),
                                        .TextAlign = ContentAlignment.MiddleCenter, .Cursor = Cursors.Hand,
                                        .BackColor = Color.Transparent}
        AddHandler forgot.Click, Sub(s, e) MessageBox.Show("Please contact your system administrator to reset your password.",
                                                             "Forgot password", MessageBoxButtons.OK, MessageBoxIcon.Information)
        card.Controls.Add(forgot)

        ' Role legend cards
        AddRoleCard(0, "Super Admin", "Both branches", Hx("#FFEBD5"), Hx("#F8C99A"), Hx("#C2410C"))
        AddRoleCard(136, "Admin", "Branch ops", Hx("#DBE8FF"), Hx("#B3CCFF"), Hx("#1D4ED8"))
        AddRoleCard(271, "Rider", "Deliveries only", Hx("#D8F7E6"), Hx("#A7E8C3"), Hx("#047857"))
    End Sub

    Private Sub CenterGroup()
        group.Location = New Point(Math.Max(0, (pnlRight.Width - group.Width) \ 2),
                                   Math.Max(0, (pnlRight.Height - group.Height) \ 2))
    End Sub

    Private Sub AddRoleCard(x As Integer, name As String, caption As String, fill As Color, border As Color, fore As Color)
        Dim rc As New RoundedPanel() With {.Bounds = New Rectangle(x, 405, 127, 63), .Radius = 10}
        Dim pill = MakePill(name, fill, border, fore, Px(11, FontStyle.Bold), -1, 18)
        pill.Location = New Point((127 - pill.Width) \ 2, 14)
        rc.Controls.Add(pill)
        Dim cap As New Label() With {.Text = caption, .Font = Px(11), .ForeColor = Hx("#9CA3AF"), .AutoSize = False,
                                     .Bounds = New Rectangle(0, 38, 127, 16), .TextAlign = ContentAlignment.MiddleCenter,
                                     .BackColor = Color.Transparent}
        rc.Controls.Add(cap)
        group.Controls.Add(rc)
    End Sub

    ' Rounded white input wrapper around a borderless TextBox (optionally leaving room for a button)
    Private Function MakeInput(tb As TextBox, x As Integer, y As Integer, w As Integer, extra As Control) As RoundedPanel
        Dim p As New RoundedPanel() With {.Bounds = New Rectangle(x, y, w, 34), .Radius = 7}
        tb.BorderStyle = BorderStyle.None
        tb.Font = Px(14)
        tb.ForeColor = Ink
        tb.BackColor = Color.White
        tb.Width = w - 28 - If(extra Is Nothing, 0, 32)
        tb.Location = New Point(14, (34 - tb.PreferredHeight) \ 2)
        p.Controls.Add(tb)
        If extra IsNot Nothing Then p.Controls.Add(extra)
        AddHandler tb.Enter, Sub(s, e)
                                 p.BorderColor = Orange
                                 p.Invalidate()
                             End Sub
        AddHandler tb.Leave, Sub(s, e)
                                 p.BorderColor = LineC
                                 p.Invalidate()
                             End Sub
        Return p
    End Function

    Private Function MakeCombo(cbo As ComboBox, x As Integer, y As Integer, w As Integer) As RoundedPanel
        Dim p As New RoundedPanel()
        ConfigureCombo(p, cbo, x, y, w)
        Return p
    End Function

    Private Sub ConfigureCombo(p As RoundedPanel, cbo As ComboBox, x As Integer, y As Integer, w As Integer)
        p.Bounds = New Rectangle(x, y, w, 30)
        p.Radius = 7
        cbo.DropDownStyle = ComboBoxStyle.DropDownList
        cbo.FlatStyle = FlatStyle.Flat
        cbo.Font = Px(13)
        cbo.BackColor = Color.White
        cbo.ForeColor = Ink
        cbo.Width = w - 12
        cbo.Location = New Point(6, (30 - cbo.Height) \ 2)
        p.Controls.Add(cbo)
    End Sub

    ' ---------------- Behaviour ----------------
    Private Sub ToggleEye(sender As Object, e As EventArgs)
        txtPassword.Reveal = Not txtPassword.Reveal
        btnEye.IconGlyph = If(txtPassword.Reveal, Glyphs.Eye, Glyphs.EyeOff)
        btnEye.Invalidate()
    End Sub

    Private Sub RoleChanged(sender As Object, e As EventArgs)
        cboBranch.Items.Clear()
        If CStr(cboRole.SelectedItem) = "Super Admin" Then
            cboBranch.Items.Add("N/A")          ' the only entry; control is disabled so nothing can be picked
            cboBranch.SelectedIndex = 0
            cboBranch.Enabled = False
            cboBranch.Visible = False        ' hide the combo so no rectangle border is drawn
            lblBranchNA.Visible = True
            pnlBranch.FillColor = Hx("#F9FAFB")
        Else
            cboBranch.Items.AddRange(New Object() {"Jose Panganiban", "Daet"})
            cboBranch.Enabled = True
            cboBranch.Visible = True
            lblBranchNA.Visible = False
            pnlBranch.FillColor = Color.White
            cboBranch.SelectedIndex = -1
        End If
        pnlBranch.Invalidate()
    End Sub

    Private Sub DoSignIn()
        Dim email As String = txtEmail.Text.Trim()
        Dim role As String = CStr(cboRole.SelectedItem)
        Dim branch As String = If(cboBranch.SelectedItem Is Nothing, "", CStr(cboBranch.SelectedItem))

        If email = "" OrElse txtPassword.Password = "" Then
            MessageBox.Show("Please enter your email address and password.", "Sign In",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If role <> "Super Admin" AndAlso branch = "" Then
            MessageBox.Show("Please select a branch.", "Sign In", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim acc = AccountStore.Authenticate(email, txtPassword.Password, role, branch)
        If acc Is Nothing Then
            MessageBox.Show("Invalid email, password, role, or branch.", "Sign In Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPassword.Clear()
            txtPassword.Focus()
            Return
        End If

        Account = acc
        DialogResult = DialogResult.OK
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            DoSignIn()
        End If
        MyBase.OnKeyDown(e)
    End Sub
End Class

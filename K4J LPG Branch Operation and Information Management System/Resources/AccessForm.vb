' "Account Recognized" screen. Content (role label, colors, permissions) changes with the role that logged in.
Public Class AccessForm
    Inherits Form

    Private ReadOnly _acc As UserAccount
    Private ReadOnly group As New Panel()

    Public Sub New(acc As UserAccount)
        _acc = acc
        Text = "K4J LPG Center Management System"
        ClientSize = New Size(1280, 833)
        MinimumSize = New Size(700, 640)
        StartPosition = FormStartPosition.CenterScreen
        WindowState = FormWindowState.Maximized
        BackColor = PageBg
        Font = Px(13)
        DoubleBuffered = True

        group.BackColor = Color.Transparent
        Controls.Add(group)
        BuildCard()
        ScaleUi(Me, False)
        AddHandler Resize, Sub(s, e) CenterGroup()
        CenterGroup()
    End Sub

    Private Sub CenterGroup()
        group.Location = New Point(Math.Max(0, (ClientSize.Width - group.Width) \ 2),
                                   Math.Max(0, (ClientSize.Height - group.Height) \ 2))
    End Sub

    Private Sub BuildCard()
        ' ---- Role-specific content ----
        Dim roleTitle As String, accent As Color, tint As Color, tintBorder As Color
        Dim perms As String()
        Select Case _acc.Role
            Case "Super Admin"
                roleTitle = "Super Administrator"
                accent = Orange : tint = Hx("#FFF5EA") : tintBorder = Hx("#FCD9AC")
                perms = {"Manage Admins & Riders", "Products & Global Pricing", "All Inventories",
                         "Deliveries & Reports", "System Settings"}
            Case "Admin"
                roleTitle = "Administrator"
                accent = BlueC : tint = Hx("#EEF4FF") : tintBorder = Hx("#BFD3FB")
                perms = {"Branch Orders & Customers", "Branch Inventory", "Deliveries Monitoring",
                         "EOD Sales & Branch Reports"}
            Case Else
                roleTitle = "Rider"
                accent = GreenC : tint = Hx("#ECFDF5") : tintBorder = Hx("#A7F3D0")
                perms = {"View Assigned Deliveries", "Update Delivery Status", "Customer Delivery Details"}
        End Select

        Const W As Integer = 390
        Dim card As New RoundedPanel() With {.Radius = 14, .Shadow = True, .Width = W}
        group.Controls.Add(card)

        ' Icon
        Dim icon As New RoundedPanel() With {.Bounds = New Rectangle((W - 50) \ 2, 29, 50, 50), .Radius = 13, .FillColor = accent, .BorderColor = accent}
        icon.Controls.Add(New Label() With {.Text = Glyphs.Home, .Font = IconFont(24), .ForeColor = Color.White,
                                            .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter,
                                            .BackColor = Color.Transparent})
        card.Controls.Add(icon)

        ' "Account Recognized" badge
        Dim badge = MakePill("Account Recognized", Hx("#E7F9EF"), Hx("#A7E8C3"), Hx("#047857"), Mono(11), -1, 20)
        badge.Location = New Point((W - badge.Width) \ 2, 93)
        card.Controls.Add(badge)

        ' Name + subtitle
        card.Controls.Add(New Label() With {.Text = _acc.FullName, .Font = Px(22, FontStyle.Bold), .ForeColor = Ink,
                                            .AutoSize = False, .Bounds = New Rectangle(0, 119, W, 30),
                                            .TextAlign = ContentAlignment.MiddleCenter, .BackColor = Color.Transparent})
        card.Controls.Add(New Label() With {.Text = "Your account has been successfully verified", .Font = Px(13), .ForeColor = Muted,
                                            .AutoSize = False, .Bounds = New Rectangle(0, 153, W, 20),
                                            .TextAlign = ContentAlignment.MiddleCenter, .BackColor = Color.Transparent})

        ' Rows: Role (+ Branch for Admin / Rider)
        Dim y As Integer = 190
        AddRow(card, "Role", roleTitle, accent, y)
        y += 48
        If _acc.Role <> "Super Admin" Then
            y += 10
            AddRow(card, "Branch", _acc.Branch, accent, y)
            y += 48
        End If

        ' Permissions box
        y += 22
        Dim boxH As Integer = 37 + perms.Length * 17 + 13
        Dim box As New RoundedPanel() With {.Bounds = New Rectangle(28, y, W - 56, boxH), .Radius = 10,
                                            .FillColor = tint, .BorderColor = tintBorder}
        box.Controls.Add(MakeLabel("ACCESS PERMISSIONS", 15, 15, Px(11, FontStyle.Bold), Hx("#4B5563")))
        For i As Integer = 0 To perms.Length - 1
            box.Controls.Add(MakeLabel(Glyphs.CheckCircle, 15, 37 + i * 17, IconFont(12), GreenC))
            box.Controls.Add(MakeLabel(perms(i), 36, 36 + i * 17, Px(12), Hx("#4B5563")))
        Next
        card.Controls.Add(box)
        y += boxH + 18

        ' Continue button
        Dim btn As New RoundButton() With {.Text = "Continue to Dashboard " & ArrowR, .Font = Px(14, FontStyle.Bold),
                                           .FillColor = accent, .Bounds = New Rectangle(28, y, W - 56, 36)}
        btn.HoverColor = ControlPaint.Dark(accent, 0.05F)
        AddHandler btn.Click, Sub(s, e) DialogResult = DialogResult.OK
        card.Controls.Add(btn)
        y += 36 + 28

        card.Height = y + 6
        group.Size = card.Size
    End Sub

    Private Sub AddRow(card As Control, caption As String, value As String, accent As Color, y As Integer)
        Dim row As New RoundedPanel() With {.Bounds = New Rectangle(28, y, card.Width - 56, 48), .Radius = 10,
                                            .FillColor = Hx("#F8F9FB"), .BorderColor = LineC}
        row.Controls.Add(MakeLabel(caption, 14, 14, Px(14, FontStyle.Bold), Hx("#374151")))
        Dim pill = MakePill(value, accent, accent, Color.White, Px(12, FontStyle.Bold), -1, 22)
        pill.Location = New Point(row.Width - 12 - pill.Width, 13)
        row.Controls.Add(pill)
        card.Controls.Add(row)
    End Sub
End Class

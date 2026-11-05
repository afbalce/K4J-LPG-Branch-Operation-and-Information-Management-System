Friend Class SignOutDialog
    Inherits Form

    Public Sub New()
        SuspendLayout()
        Text = "Sign out"
        Font = New Font("Segoe UI", 10.0F)
        AutoScaleDimensions = New SizeF(96, 96)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(440, 176)
        FormBorderStyle = FormBorderStyle.FixedDialog
        StartPosition = FormStartPosition.CenterParent
        ShowInTaskbar = False
        ShowIcon = False
        MaximizeBox = False
        MinimizeBox = False
        BackColor = Color.White

        Dim icon As New RoundedButton With {
            .Location = New Point(24, 26), .Size = New Size(46, 46),
            .CornerRadius = 12, .BorderThickness = 0, .FillColor = Color.FromArgb(232, 101, 10),
            .IconFileName = "logout_hover_white.png", .IconSize = 22, .Text = "",
            .TabStop = False, .Enabled = False, .AccessibleRole = AccessibleRole.Graphic
        }
        Dim heading As New Label With {
            .Text = "Sign out of your account?", .AutoSize = True,
            .Font = New Font("Segoe UI Semibold", 15.0F), .ForeColor = Color.FromArgb(15, 23, 42),
            .Location = New Point(82, 21)
        }
        Dim description As New Label With {
            .Text = "You’ll return to the login screen.", .AutoSize = True,
            .ForeColor = Color.FromArgb(71, 85, 105), .Location = New Point(84, 55)
        }
        Dim footer As New Panel With {
            .Dock = DockStyle.Bottom, .Height = 70, .BackColor = Color.FromArgb(248, 250, 252)
        }
        Dim separator As New Panel With {.Dock = DockStyle.Top, .Height = 1, .BackColor = Color.FromArgb(226, 232, 240)}
        Dim stay As New RoundedButton With {
            .Text = "Stay signed in", .DialogResult = DialogResult.Cancel,
            .Location = New Point(152, 16), .Size = New Size(138, 38), .CornerRadius = 8,
            .FillColor = Color.White, .BorderColor = Color.FromArgb(203, 213, 225),
            .BorderThickness = 1, .ForeColor = Color.FromArgb(30, 41, 59), .TabIndex = 0
        }
        Dim leave As New RoundedButton With {
            .Text = "Sign out", .DialogResult = DialogResult.OK,
            .Location = New Point(300, 16), .Size = New Size(116, 38), .CornerRadius = 8,
            .FillColor = Color.FromArgb(185, 68, 5), .BorderThickness = 0,
            .ForeColor = Color.White, .TabIndex = 1
        }
        For Each button In {stay, leave}
            Dim normalBorder = button.BorderColor
            Dim normalThickness = button.BorderThickness
            AddHandler button.Enter, Sub()
                                         button.BorderColor = Color.FromArgb(51, 65, 85)
                                         button.BorderThickness = 2
                                     End Sub
            AddHandler button.Leave, Sub()
                                         button.BorderColor = normalBorder
                                         button.BorderThickness = normalThickness
                                     End Sub
        Next
        footer.Controls.AddRange({separator, stay, leave})
        Controls.AddRange({icon, heading, description, footer})
        CancelButton = stay
        ActiveControl = stay
        ResumeLayout(True)
    End Sub
End Class

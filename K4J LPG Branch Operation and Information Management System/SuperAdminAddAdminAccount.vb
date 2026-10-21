Public Class SuperAdminAddAdminAccount
    Private layoutReady As Boolean
    Private arranging As Boolean
    Private accountsBreadcrumb As Label
    Private breadcrumbFont As New Font("Segoe UI", 8.0F, FontStyle.Regular)
    Private backArrowFont As New Font("Segoe UI", 12.0F, FontStyle.Regular)

    Public Event NavigateRequested(pageKey As String)
    Public Event SignOutRequested()

    Private Sub PageLoaded(sender As Object, e As EventArgs) Handles MyBase.Load
        HeaderBar1.SetParentPageName("Super Admin")
        HeaderBar1.SetPageName("Add Admin")
        SystemHeading1.SetHeading("ADD ADMIN ACCOUNT", "Create a new branch administrator")
        mainContentHolder.Panel1.BackColor = SystemHeading1.BackColor
        SuperAdminSidebarNavigation1.SetActivePage("Admin Accounts")
        Dim existing = Controls.Find("superAdminLabel", True)
        If existing.Length > 0 Then accountsBreadcrumb = TryCast(existing(0), Label)
        If accountsBreadcrumb Is Nothing Then accountsBreadcrumb = New Label With {.Name = "superAdminLabel"}
        With accountsBreadcrumb
            .Parent = headingSplitContainer.Panel2
            .AutoSize = False
            .Text = "Admin Accounts " & ChrW(&H2192)
            .Font = breadcrumbFont
            .ForeColor = Color.FromArgb(148, 163, 184)
            .BackColor = SystemHeading1.BackColor
            .TextAlign = ContentAlignment.MiddleLeft
            .Cursor = Cursors.Hand
            .TabStop = True
            .AccessibleName = "Back to Admin Accounts"
        End With
        AddHandler accountsBreadcrumb.Click, AddressOf BackToAccounts
        backButton.Font = backArrowFont
        backButton.Text = ChrW(&H2190)
        backButton.HorizontalContentInset = 0
        backButton.TextAlign = ContentAlignment.MiddleCenter
        backButton.DrawBackArrow = True
        AddHandler backButton.Click, AddressOf BackToAccounts
        AddHandler accountsBreadcrumb.KeyDown,
            Sub(s, args)
                If args.KeyCode = Keys.Enter OrElse args.KeyCode = Keys.Space Then
                    args.SuppressKeyPress = True
                    BackToAccounts(s, EventArgs.Empty)
                End If
            End Sub
        WindowSizing.ApplyStartingSize(Me)
        layoutReady = True
        ArrangePage()
    End Sub

    Private Function Px(value As Integer) As Integer
        Return CInt(value * DeviceDpi / 96.0R)
    End Function

    Private Sub ArrangePage() Handles MyBase.Resize
        If Not layoutReady OrElse arranging OrElse WindowState = FormWindowState.Minimized Then Return
        arranging = True
        SuspendLayout()
        Try
            SetSectionSize(AddAccountMainContainer, Math.Min(Math.Clamp(CInt(ClientSize.Width * 0.18), Px(180), Px(280)), Math.Max(1, ClientSize.Width - Px(300))))
            SetSectionSize(addAccountContentContainer, Px(50))
            HeaderBar1.RefreshHeaderLayout()
            Dim headingScale = Math.Clamp(mainContentHolder.ClientSize.Width /
                (DeviceDpi / 96.0R) / 920.0R, 0.85R, 1.25R)
            Dim breadcrumbPoints = CSng(Math.Round(Math.Clamp(8.5R * headingScale, 8.0R, 10.5R), 1))
            Dim arrowPoints = CSng(Math.Round(Math.Clamp(12.0R * headingScale, 11.0R, 15.0R), 1))
            If Math.Abs(breadcrumbFont.Size - breadcrumbPoints) > 0.01F Then
                Dim previous = breadcrumbFont
                breadcrumbFont = New Font("Segoe UI", breadcrumbPoints, FontStyle.Regular)
                accountsBreadcrumb.Font = breadcrumbFont
                previous.Dispose()
            End If
            If Math.Abs(backArrowFont.Size - arrowPoints) > 0.01F Then
                Dim previous = backArrowFont
                backArrowFont = New Font("Segoe UI", arrowPoints, FontStyle.Regular)
                backButton.Font = backArrowFont
                previous.Dispose()
            End If
            Dim buttonSize = Px(CInt(Math.Clamp(36 * headingScale, 32, 44)))
            SystemHeading1.Dock = DockStyle.None
            SetSectionSize(headingSplitContainer, buttonSize + Px(12))
            SystemHeading1.SetBounds(-Px(12), 0, headingSplitContainer.Panel2.ClientSize.Width + Px(12), SystemHeading1.Height)
            SystemHeading1.RefreshHeadingLayout()
            Dim titles = SystemHeading1.Controls.Find("panelLabel", True)
            Dim left = If(titles.Length > 0, titles(0).Left, Px(18))
            accountsBreadcrumb.SetBounds(SystemHeading1.Left + left, Px(4), Math.Max(1, SystemHeading1.Width - left * 2), accountsBreadcrumb.PreferredHeight)
            Dim titleTop = If(titles.Length > 0, titles(0).Top, Px(6))
            SystemHeading1.Top = Math.Max(0, accountsBreadcrumb.Bottom + Px(1) - titleTop)
            Dim headingHeight = Math.Max(SystemHeading1.Bottom + Px(4), buttonSize + Px(12))
            SetSectionSize(mainContentHolder, headingHeight)
            backButton.SetBounds(Math.Max(0, (headingSplitContainer.Panel1.ClientSize.Width - buttonSize) \ 2),
                Math.Max(0, (headingHeight - buttonSize) \ 2), buttonSize, buttonSize)
            backButton.CornerRadius = Px(7)
            backButton.BringToFront()
            accountsBreadcrumb.BringToFront()
            mainContentHolder.Panel2.Padding = New Padding(Px(18), Px(8), Px(18), Px(18))
            ' The embedded form owns scrolling; avoid nested scrollbars.
            mainContentHolder.Panel2.AutoScroll = False
        Finally
            ResumeLayout(True)
            arranging = False
        End Try
    End Sub

    Private Sub BackToAccounts(sender As Object, e As EventArgs)
        If Not TopLevel Then
            If NavigateRequestedEvent IsNot Nothing Then
                RaiseEvent NavigateRequested("Admin Accounts")
            Else
                Close()
            End If
            Return
        End If
        If Modal Then
            DialogResult = DialogResult.Cancel
            Close()
            Return
        End If
        If NavigateRequestedEvent IsNot Nothing Then
            RaiseEvent NavigateRequested("Admin Accounts")
        Else
            Dim accounts = My.Forms.SuperAdminAdminAccounts
            accounts.Show()
            accounts.Activate()
            Hide()
            AddHandler accounts.FormClosed, Sub() Close()
        End If
    End Sub

    Private Sub ReleaseBreadcrumbFont(sender As Object, e As EventArgs) Handles Me.Disposed
        breadcrumbFont.Dispose()
        backArrowFont.Dispose()
    End Sub

    Private Sub SetSectionSize(container As SplitContainer, desired As Integer)
        Dim length = If(container.Orientation = Orientation.Vertical, container.ClientSize.Width, container.ClientSize.Height)
        Dim available = Math.Max(0, length - container.SplitterWidth - container.Panel2MinSize)
        Dim target = Math.Max(container.Panel1MinSize, Math.Min(desired, available))
        If target <= available Then container.SplitterDistance = target
    End Sub

    Protected Overrides Sub OnDpiChanged(e As DpiChangedEventArgs)
        MyBase.OnDpiChanged(e)
        ArrangePage()
    End Sub

    Private Sub SidebarNavigate(pageKey As String) Handles SuperAdminSidebarNavigation1.NavigateRequested
        RaiseEvent NavigateRequested(pageKey)
    End Sub

    Private Sub SidebarSignOut() Handles SuperAdminSidebarNavigation1.SignOutRequested
        RaiseEvent SignOutRequested()
    End Sub
End Class



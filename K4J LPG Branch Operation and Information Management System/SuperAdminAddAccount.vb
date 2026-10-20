Public Class SuperAdminAddAccount
    Private layoutReady As Boolean
    Private arranging As Boolean

    Public Event NavigateRequested(pageKey As String)
    Public Event SignOutRequested()

    Private Sub PageLoaded(sender As Object, e As EventArgs) Handles MyBase.Load
        HeaderBar1.SetParentPageName("Super Admin")
        HeaderBar1.SetPageName("Add Admin")
        SystemHeading1.SetHeading("ADD ADMIN ACCOUNT", "Create a new branch administrator")
        SuperAdminSidebarNavigation1.SetActivePage("Admin Accounts")
        Dim work = Screen.FromControl(Me).WorkingArea
        MinimumSize = New Size(Math.Min(work.Width, Px(640)), Math.Min(work.Height, Px(480)))
        Size = New Size(Math.Min(work.Width, Px(1200)), Math.Min(work.Height, Px(780)))
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
            SystemHeading1.Dock = DockStyle.Top
            SystemHeading1.RefreshHeadingLayout()
            SetSectionSize(mainContentHolder, Math.Max(Px(65), SystemHeading1.Height))
            mainContentHolder.Panel2.Padding = New Padding(Px(18), Px(8), Px(18), Px(18))
        Finally
            ResumeLayout(True)
            arranging = False
        End Try
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

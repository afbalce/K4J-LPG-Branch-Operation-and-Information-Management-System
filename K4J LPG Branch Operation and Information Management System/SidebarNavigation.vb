Imports System.Runtime.CompilerServices

Friend Module SidebarNavigation
    Private ReadOnly windows As New ConditionalWeakTable(Of Form, PageHost)()

    Friend Sub OpenPage(sidebar As UserControl, page As String)
        Dim host = TryCast(sidebar.TopLevelControl, Form)
        If host Is Nothing OrElse Not sidebar.Visible Then Return
        Dim target = Destination(sidebar, page)
        If target Is Nothing OrElse sidebar.FindForm().GetType() Is target Then Return
        Dim pages = windows.GetValue(host, Function(window) New PageHost(window))
        pages.Open(target, sidebar)
    End Sub

    Private Function Destination(sidebar As UserControl, page As String) As Type
        If TypeOf sidebar Is SuperAdminSidebarNavigation Then
            Select Case page
                Case "Dashboard" : Return GetType(SuperAdminDashboard)
                Case "Admin Accounts" : Return GetType(SuperAdminAdminAccounts)
                Case "Rider Accounts" : Return GetType(SuperAdminRiderAccounts)
                Case "Products" : Return GetType(SuperAdminProductManagement)
                Case "Global Inventory" : Return GetType(SuperAdminGlobalInventory)
                Case "Customers" : Return GetType(SuperAdminCustomers)
                Case "Deliveries" : Return GetType(SuperAdminDeliveryManagement)
                Case "EOD Sales" : Return GetType(SuperAdminEODSales)
                Case "Reports" : Return GetType(SuperAdminReports)
                Case "Settings" : Return GetType(SuperAdminSettings)
            End Select
        End If
        If TypeOf sidebar Is AdminSidebarNaviagtion Then
            Select Case page
                Case "Dashboard" : Return GetType(AdminDashboard)
                Case "Customers" : Return GetType(AdminCustomers)
                Case "Products" : Return GetType(AdminProductManagement)
                Case "Global Inventory" : Return GetType(AdminGlobalInventory)
                Case "Create Order" : Return GetType(AdminCreateOrder)
                Case "Assign Rider" : Return GetType(AdminAssignRider)
                Case "Deliveries" : Return GetType(AdminDeliveryManagement)
                Case "Debt / Unpaid" : Return GetType(AdminDebtManagement)
                Case "Opening Checklist" : Return GetType(AdminOpeningChecklist)
                Case "Closing Checklist" : Return GetType(AdminClosingChecklist)
                Case "EOD Sales" : Return GetType(AdminEODSales)
            End Select
        End If
        If TypeOf sidebar Is RiderSidebarNavigation Then
            Select Case page
                Case "Dashboard" : Return GetType(RiderDashboard)
                Case "My Deliveries" : Return GetType(RiderDelivery)
                Case "Delivery History" : Return GetType(RiderDeliveryHistory)
            End Select
        End If
        Return Nothing
    End Function

    Private Class PageHost
        Private ReadOnly window As Form
        Private ReadOnly originalTitle As String
        Private ReadOnly originalControls As New List(Of Control)
        Private current As Form

        Friend Sub New(host As Form)
            window = host
            originalTitle = host.Text
        End Sub

        Friend Sub Open(target As Type, sidebar As UserControl)
            If current IsNot Nothing AndAlso current.GetType() Is target Then Return
            If target Is window.GetType() Then
                If current IsNot Nothing Then current.Dispose()
                current = Nothing
                For Each child In originalControls
                    child.Show()
                Next
                originalControls.Clear()
                window.Text = originalTitle
                Return
            End If

            Dim nextPage = DirectCast(Activator.CreateInstance(target), Form)
            Try
                nextPage.TopLevel = False
                nextPage.ShowInTaskbar = False
                nextPage.FormBorderStyle = FormBorderStyle.None
                nextPage.Dock = DockStyle.Fill
                window.Controls.Add(nextPage)
                CopyUser(sidebar, nextPage)
                nextPage.Show()
                ' Some standalone forms configure their window border during Load.
                nextPage.FormBorderStyle = FormBorderStyle.None
                nextPage.BringToFront()
            Catch
                nextPage.Dispose()
                Throw
            End Try

            If current Is Nothing Then
                For Each child As Control In window.Controls
                    If child Is nextPage OrElse Not child.Visible Then Continue For
                    If Not TypeOf child Is Form Then originalControls.Add(child)
                    child.Hide()
                Next
            Else
                current.Dispose()
            End If
            current = nextPage
            window.Text = nextPage.Text
            nextPage.Select()
        End Sub
    End Class

    Friend Sub CopyUser(source As UserControl, page As Form)
        Dim matches = page.Controls.Find(source.Name, True)
        If matches.Length = 0 Then Return
        If TypeOf source Is SuperAdminSidebarNavigation Then
            Dim previous = DirectCast(source, SuperAdminSidebarNavigation)
            Dim destination = TryCast(matches(0), SuperAdminSidebarNavigation)
            If destination IsNot Nothing Then destination.SetUser(previous.userNameLabel.Text, previous.userRoleLabel.Text)
        End If
        If TypeOf source Is AdminSidebarNaviagtion Then
            Dim previous = DirectCast(source, AdminSidebarNaviagtion)
            Dim destination = TryCast(matches(0), AdminSidebarNaviagtion)
            If destination IsNot Nothing Then destination.SetUser(previous.userNameLabel.Text, previous.userRoleLabel.Text, previous.BranchName)
        End If
        If TypeOf source Is RiderSidebarNavigation Then
            Dim previous = DirectCast(source, RiderSidebarNavigation)
            Dim destination = TryCast(matches(0), RiderSidebarNavigation)
            If destination IsNot Nothing Then destination.SetUser(previous.userNameLabel.Text, previous.userRoleLabel.Text, previous.BranchName)
        End If
    End Sub

    Friend Function ConfirmSignOut(sidebar As UserControl) As Boolean
        Using dialog As New SignOutDialog()
            Return dialog.ShowDialog(sidebar.TopLevelControl) = DialogResult.OK
        End Using
    End Function

    Friend Sub SignOut(sidebar As UserControl)
        Dim host = TryCast(sidebar.TopLevelControl, Form)
        If host Is Nothing OrElse host.IsDisposed Then Return
        My.Application.ReturnToLogin(host)
    End Sub
End Module

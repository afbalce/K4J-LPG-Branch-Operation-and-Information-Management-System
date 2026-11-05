
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class SuperAdminRiderAccounts

    ' ==========================================
    ' RESPONSIVE SETTINGS
    ' ==========================================

    Private Const StartMaximized As Boolean = False


    Private Const SidebarWidthRatio As Double = 0.18R

    Private Const SidebarMinWidth As Integer = 180
    Private Const SidebarMaxWidth As Integer = 280

    Private Const MinimumContentWidth As Integer = 300

    Private Const HeaderHeight As Integer = 50

    ' ==========================================
    ' CONTROLS
    ' ==========================================

    Private _sidebar As SuperAdminSidebarNavigation
    Private _sidebarHost As Control

    Private _rightHost As Panel
    Private _header As HeaderBar
    Private _systemHeading As SystemHeading
    Private _addRiderAction As RoundedButton
    Private _addRiderFont As Font
    Private _addRiderPage As SuperAdminAddRiderAccount

    ' ==========================================
    ' STATE
    ' ==========================================

    Private _ready As Boolean = False
    Private _busy As Boolean = False

    ' ==========================================
    ' DPI SCALING
    ' ==========================================

    Private Function Px(value As Integer) As Integer

        Return Math.Max(
            1,
            CInt(
                value *
                Math.Max(0.75R, Me.DeviceDpi / 96.0R)
            )
        )

    End Function

    ' ==========================================
    ' FORM LOAD
    ' ==========================================

    Private Sub SuperAdminRiderAccounts_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        _ready = False

        Me.SuspendLayout()

        Try

            Me.AutoScroll = False
            Me.AutoScrollMinSize = Size.Empty
            Me.AutoSize = False

            Me.Padding = New Padding(0)

            Me.FormBorderStyle =
                FormBorderStyle.Sizable

            Me.MaximizeBox = True
            Me.MinimizeBox = True
            Me.MaximumSize = Size.Empty

            ' ==================================
            ' GET SCREEN WORKING AREA
            ' ==================================

            WindowSizing.ApplyStartingSize(Me)

            ' ==================================
            ' SETUP SIDEBAR AND HEADER
            ' ==================================

            SetupSidebar()
            SetupHeader()
            SetupSystemHeading()
            SetupAddRiderAction()

            If _sidebar IsNot Nothing Then

                _sidebar.SetActivePage("Rider Accounts")

            End If

            _ready = True

        Finally

            Me.ResumeLayout(True)

        End Try

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' FORM SHOWN
    ' ==========================================

    Private Sub SuperAdminRiderAccounts_Shown(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Shown

        If StartMaximized Then

            Me.WindowState =
                FormWindowState.Maximized

        End If

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' FIND EXISTING SIDEBAR
    ' ==========================================

    Private Function FindSidebar(
        root As Control
    ) As SuperAdminSidebarNavigation

        If root Is Nothing Then Return Nothing

        Dim sidebar As SuperAdminSidebarNavigation =
            TryCast(
                root,
                SuperAdminSidebarNavigation
            )

        If sidebar IsNot Nothing Then
            Return sidebar
        End If

        For Each child As Control In root.Controls

            Dim found As SuperAdminSidebarNavigation =
                FindSidebar(child)

            If found IsNot Nothing Then
                Return found
            End If

        Next

        Return Nothing

    End Function

    ' ==========================================
    ' FIND EXISTING HEADER
    ' ==========================================

    Private Function FindHeader(
        root As Control
    ) As HeaderBar

        If root Is Nothing Then Return Nothing

        Dim bar As HeaderBar =
            TryCast(root, HeaderBar)

        If bar IsNot Nothing Then Return bar

        For Each child As Control In root.Controls

            Dim found As HeaderBar =
                FindHeader(child)

            If found IsNot Nothing Then
                Return found
            End If

        Next

        Return Nothing

    End Function

    ' ==========================================
    ' SIDEBAR SETUP
    ' ==========================================

    Private Sub SetupSidebar()
        _sidebar = SuperAdminSidebarNavigation1
        _sidebarHost = RiderAccountsMainContainer.Panel1
        RiderAccountsMainContainer.Dock = DockStyle.Fill
        RiderAccountsMainContainer.FixedPanel = FixedPanel.Panel1
        RiderAccountsMainContainer.IsSplitterFixed = True
        RiderAccountsMainContainer.Panel1MinSize = 0
        RiderAccountsMainContainer.Panel2MinSize = 0
        _sidebar.Dock = DockStyle.Fill
        _sidebar.Margin = Padding.Empty
        _sidebar.AutoScroll = False
    End Sub

    ' ==========================================
    ' HEADER SETUP
    ' ==========================================

    Private Sub SetupHeader()
        _rightHost = RiderAccountsMainContainer.Panel2
        _rightHost.BackColor = Color.FromArgb(241, 245, 249)
        riderAccountsContentContainer.Dock = DockStyle.Fill
        riderAccountsContentContainer.Panel1MinSize = 0
        riderAccountsContentContainer.Panel2MinSize = 0
        _header = HeaderBar1
        _header.Dock = DockStyle.Fill
        _header.Margin = Padding.Empty
        _header.SetParentPageName("Super Admin")
        _header.SetPageName("Rider Accounts")
    End Sub

    ' ==========================================
    ' SYSTEM HEADING BELOW HEADERBAR
    ' ==========================================

    Private Sub SetupSystemHeading()
        mainContentHolder.Dock = DockStyle.Fill
        mainContentHolder.Panel1MinSize = 0
        mainContentHolder.Panel2MinSize = 0
        _systemHeading = SystemHeading1
        _systemHeading.Dock = DockStyle.Top
        _systemHeading.Margin = Padding.Empty
        _systemHeading.SetHeading("RIDER ACCOUNTS", "Manage rider accounts and information")
    End Sub

    Private Function FindSystemHeading(root As Control) As SystemHeading
        If root Is Nothing Then Return Nothing

        Dim found As SystemHeading = TryCast(root, SystemHeading)
        If found IsNot Nothing Then Return found

        For Each child As Control In root.Controls
            found = FindSystemHeading(child)
            If found IsNot Nothing Then Return found
        Next

        Return Nothing
    End Function

    Private Sub SetupAddRiderAction()
        _addRiderAction = addRiderButton
        RemoveHandler _addRiderAction.Click, AddressOf OpenAddRiderAccount
        AddHandler _addRiderAction.Click, AddressOf OpenAddRiderAccount
        _addRiderAction.BringToFront()
        AddHandler Disposed, Sub()
                                 If _addRiderFont IsNot Nothing Then _addRiderFont.Dispose()
                             End Sub
    End Sub

    Private Sub OpenAddRiderAccount(sender As Object, e As EventArgs)
        If _addRiderPage IsNot Nothing AndAlso Not _addRiderPage.IsDisposed Then
            Using transition As New PanelTransition(Me)
                _addRiderPage.Show()
                _addRiderPage.BringToFront()
            End Using
            Return
        End If
        Dim page As New SuperAdminAddRiderAccount With {
            .TopLevel = False,
            .FormBorderStyle = FormBorderStyle.None,
            .Dock = DockStyle.Fill,
            .ShowInTaskbar = False}
        _addRiderPage = page
        AddHandler page.NavigateRequested,
            Sub(pageKey)
                If pageKey = "Rider Accounts" Then
                    Using transition As New PanelTransition(Me)
                        page.Hide()
                        RiderAccountsManagement1.ReloadAccounts()
                        _addRiderAction.Focus()
                    End Using
                End If
            End Sub
        AddHandler page.FormClosed,
            Sub()
                Controls.Remove(page)
                _addRiderPage = Nothing
                _addRiderAction.Focus()
            End Sub
        Using transition As New PanelTransition(Me)
            SidebarNavigation.CopyUser(_sidebar, page)
            Controls.Add(page)
            page.Show()
            page.BringToFront()
        End Using
    End Sub

    Private Sub LayoutAddRiderAction()
        If _addRiderAction Is Nothing Then Return
        Dim scale = Math.Clamp(_systemHeading.Width / CDbl(Px(924)), 0.85R, 1.1R)
        Dim points = CSng(Math.Clamp(10.0R * scale, 9.0R, 11.0R))
        If _addRiderFont Is Nothing OrElse Math.Abs(_addRiderFont.SizeInPoints - points) > 0.05F Then
            Dim previous = _addRiderFont
            _addRiderFont = New Font("Segoe UI", points, FontStyle.Bold)
            _addRiderAction.Font = _addRiderFont
            If previous IsNot Nothing Then previous.Dispose()
        End If
        Dim icon = Px(CInt(16 * scale))
        _addRiderAction.IconWidth = icon
        _addRiderAction.IconHeight = icon
        _addRiderAction.IconTextSpacing = Px(8)
        Dim width = Math.Max(Px(CInt(138 * scale)),
                             TextRenderer.MeasureText("Add Rider", _addRiderFont).Width + icon + Px(32))
        Dim height = Math.Max(Px(CInt(34 * scale)), _addRiderFont.Height + Px(14))
        Dim parent = _addRiderAction.Parent
        _addRiderAction.SetBounds(Math.Max(0, parent.ClientSize.Width - Px(18) - width),
                                 Math.Max(0, (parent.ClientSize.Height - height) \ 2), width, height)
        If parent Is _systemHeading Then
            For Each child As Control In _systemHeading.Controls
                Dim label = TryCast(child, Label)
                If label Is Nothing Then Continue For
                label.AutoEllipsis = True
                label.Width = Math.Max(1, _addRiderAction.Left - Px(12) - label.Left)
            Next
        End If
        _addRiderAction.BringToFront()
    End Sub

    ' ==========================================
    ' CALCULATE SIDEBAR WIDTH
    ' ==========================================

    Private Function GetSidebarWidth() As Integer

        Dim width As Integer =
            Me.ClientSize.Width

        If width <= 0 Then Return 1

        Dim preferred As Integer =
            CInt(width * SidebarWidthRatio)

        Dim bounded As Integer =
            Math.Max(
                Px(SidebarMinWidth),
                Math.Min(
                    Px(SidebarMaxWidth),
                    preferred
                )
            )

        Dim contentReserved As Integer =
            Math.Min(
                Px(MinimumContentWidth),
                CInt(width * 0.6R)
            )

        Return Math.Max(
            1,
            Math.Min(
                bounded,
                width - contentReserved
            )
        )

    End Function

    ' ==========================================
    ' MAIN RESPONSIVE LAYOUT
    ' ==========================================

    Private Sub UpdateResponsiveLayout()
        If Not _ready OrElse _busy OrElse WindowState = FormWindowState.Minimized Then Return
        If ClientSize.Width < 1 OrElse ClientSize.Height < 1 Then Return
        _busy = True
        SuspendLayout()
        Try
            SetSectionSize(RiderAccountsMainContainer, GetSidebarWidth())
            SetSectionSize(riderAccountsContentContainer, Px(HeaderHeight))
            _header.RefreshHeaderLayout()
            _systemHeading.RefreshHeadingLayout()
            SetSectionSize(mainContentHolder, Math.Max(Px(65), _systemHeading.Height))
            LayoutAddRiderAction()
            Dim scrollHeight = If(mainContents.Width < RiderAccountsCards.StripWidth,
                                  SystemInformation.HorizontalScrollBarHeight, 0)
            SetSectionSize(mainContents, RiderAccountsCards.StripHeight + scrollHeight)
        Finally
            ResumeLayout(True)
            _busy = False
        End Try
    End Sub

    Private Sub SetSectionSize(container As SplitContainer, desiredSize As Integer)
        Dim length = If(container.Orientation = Orientation.Vertical, container.ClientSize.Width, container.ClientSize.Height)
        Dim available = Math.Max(0, length - container.SplitterWidth - container.Panel2MinSize)
        Dim target = Math.Max(container.Panel1MinSize, Math.Min(desiredSize, available))
        If target <= available AndAlso container.SplitterDistance <> target Then
            container.SplitterDistance = target
        End If
    End Sub

    Protected Overrides Sub OnDpiChanged(e As DpiChangedEventArgs)
        MyBase.OnDpiChanged(e)
        UpdateResponsiveLayout()
    End Sub

    ' ==========================================
    ' FORM RESIZE EVENTS
    ' ==========================================

    Private Sub SuperAdminRiderAccounts_Resize(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Resize

        UpdateResponsiveLayout()

    End Sub

    Private Sub SuperAdminRiderAccounts_ClientSizeChanged(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.ClientSizeChanged

        UpdateResponsiveLayout()

    End Sub

    Private Sub SuperAdminRiderAccounts_ResizeEnd(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.ResizeEnd

        UpdateResponsiveLayout()

    End Sub

    Private Sub UpdateRiderTotals() Handles RiderAccountsManagement1.AccountsReloaded
        Dim rows = TryCast(RiderAccountsManagement1.StyledDataGridView1.DataSource, DataTable)
        If rows Is Nothing Then Return
        RiderAccountsCards1.ShowTotals(rows)
    End Sub

    ' ==========================================
    ' AVAILABLE RIDER ACCOUNTS CONTENT AREA
    ' ==========================================

    Public ReadOnly Property RiderAccountsContentBounds As Rectangle
        Get
            Dim content = mainContentHolder.Panel2
            If IsHandleCreated AndAlso content.IsHandleCreated Then
                Return RectangleToClient(content.RectangleToScreen(content.ClientRectangle))
            End If
            Return New Rectangle(RiderAccountsMainContainer.Panel2.Left,
                                 riderAccountsContentContainer.Panel2.Top + mainContentHolder.Panel2.Top,
                                 content.ClientSize.Width, content.ClientSize.Height)
        End Get
    End Property

End Class







Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class AdminCustomers

    ' ==========================================
    ' RESPONSIVE SETTINGS
    ' ==========================================

    Private Const StartMaximized As Boolean = False

    Private Const InitialWidthRatio As Double = 0.92R
    Private Const InitialHeightRatio As Double = 0.92R

    Private Const SidebarWidthRatio As Double = 0.18R

    Private Const SidebarMinWidth As Integer = 180
    Private Const SidebarMaxWidth As Integer = 280

    Private Const MinimumContentWidth As Integer = 300

    Private Const HeaderHeight As Integer = 50

    ' ==========================================
    ' CONTROLS
    ' ==========================================

    Private _sidebar As AdminSidebarNaviagtion
    Private _sidebarHost As Control

    Private _rightHost As Panel
    Private _header As HeaderBar
    Private _systemHeading As SystemHeading

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

    Private Sub AdminCustomers_Load(
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

            Dim work As Rectangle =
                Screen.FromControl(Me).WorkingArea

            Dim minWidth As Integer =
                Math.Min(work.Width, Px(520))

            Dim minHeight As Integer =
                Math.Min(work.Height, Px(360))

            Me.MinimumSize =
                New Size(minWidth, minHeight)

            ' ==================================
            ' INITIAL WINDOW DIMENSIONS
            ' ==================================

            Me.WindowState =
                FormWindowState.Normal

            Me.StartPosition =
                FormStartPosition.Manual

            Me.Size = New Size(
                Math.Min(work.Width, Math.Max(minWidth, CInt(work.Width * 0.92R))),
                Math.Min(work.Height, Math.Max(minHeight, CInt(work.Height * 0.92R))))

            Me.Location = New Point(
                work.Left + (work.Width - Me.Width) \ 2,
                work.Top + (work.Height - Me.Height) \ 2
            )

            ' ==================================
            ' SETUP SIDEBAR AND HEADER
            ' ==================================

            SetupSidebar()
            SetupHeader()
            SetupSystemHeading()

            If _sidebar IsNot Nothing Then

                _sidebar.SetActivePage("Customers")

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

    Private Sub AdminCustomers_Shown(
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
    ) As AdminSidebarNaviagtion

        If root Is Nothing Then Return Nothing

        Dim sidebar As AdminSidebarNaviagtion =
            TryCast(
                root,
                AdminSidebarNaviagtion
            )

        If sidebar IsNot Nothing Then
            Return sidebar
        End If

        For Each child As Control In root.Controls

            Dim found As AdminSidebarNaviagtion =
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

        _sidebar = FindSidebar(Me)

        If _sidebar Is Nothing Then

            _sidebar =
                New AdminSidebarNaviagtion()

            _sidebar.Name =
                "adminCustomersSidebarNavigation"

            Me.Controls.Add(_sidebar)

        End If

        Dim parentControl As Control =
            _sidebar.Parent

        If TypeOf parentControl Is Panel AndAlso
           parentControl.Parent Is Me AndAlso
           parentControl.Controls.Count = 1 Then

            _sidebarHost = parentControl

            _sidebar.Dock = DockStyle.Fill
            _sidebar.Margin = New Padding(0)

        Else

            If _sidebar.Parent IsNot Me Then

                Me.Controls.Add(_sidebar)

            End If

            _sidebarHost = _sidebar

        End If

        ' ==================================
        ' REMOVE SIZE RESTRICTIONS
        ' ==================================

        _sidebarHost.AutoSize = False

        _sidebarHost.MinimumSize = Size.Empty
        _sidebarHost.MaximumSize = Size.Empty

        _sidebarHost.Margin = New Padding(0)
        _sidebarHost.Padding = New Padding(0)

        ' ==================================
        ' DOCK LEFT
        ' ==================================

        _sidebarHost.Dock = DockStyle.Left

        _sidebarHost.Width =
            GetSidebarWidth()

        ' ==================================
        ' DISABLE OUTER SCROLLBARS
        ' ==================================

        _sidebar.AutoScroll = False
        _sidebar.AutoScrollMinSize = Size.Empty

        Dim host As ScrollableControl =
            TryCast(
                _sidebarHost,
                ScrollableControl
            )

        If host IsNot Nothing Then

            host.AutoScroll = False
            host.AutoScrollMinSize = Size.Empty

        End If

    End Sub

    ' ==========================================
    ' HEADER SETUP
    ' ==========================================

    Private Sub SetupHeader()

        _header = FindHeader(Me)

        ' ==================================
        ' REUSE EXISTING RIGHT PANEL
        ' ==================================

        If _header IsNot Nothing Then

            Dim topControl As Control = _header

            While topControl.Parent IsNot Nothing AndAlso
                  topControl.Parent IsNot Me

                topControl = topControl.Parent

            End While

            If TypeOf topControl Is Panel AndAlso
               topControl.Parent Is Me AndAlso
               topControl IsNot _sidebarHost Then

                _rightHost =
                    DirectCast(topControl, Panel)

            End If

        End If

        ' ==================================
        ' CREATE PANEL IF MISSING
        ' ==================================

        If _rightHost Is Nothing Then

            _rightHost = New Panel()

            _rightHost.Name =
                "adminCustomersRightPanel"

            Me.Controls.Add(_rightHost)

        End If

        ' ==================================
        ' RIGHT PANEL SETTINGS
        ' ==================================

        _rightHost.Dock = DockStyle.None

        _rightHost.Anchor =
            AnchorStyles.Top Or AnchorStyles.Left

        _rightHost.AutoSize = False
        _rightHost.AutoScroll = False

        _rightHost.AutoScrollMinSize = Size.Empty

        _rightHost.MinimumSize = Size.Empty
        _rightHost.MaximumSize = Size.Empty

        _rightHost.Margin = New Padding(0)
        _rightHost.Padding = New Padding(0)

        _rightHost.BackColor =
            Color.FromArgb(241, 245, 249)

        ' ==================================
        ' HEADERBAR
        ' ==================================

        If _header Is Nothing Then

            _header = New HeaderBar()

            _header.Name =
                "adminCustomersHeaderBar"

        End If

        If _header.Parent IsNot _rightHost Then

            _header.Parent = _rightHost

        End If

        _header.AutoSize = False

        _header.MinimumSize = Size.Empty
        _header.MaximumSize = Size.Empty

        _header.Padding = New Padding(0)
        _header.Margin = New Padding(0)

        _header.Dock = DockStyle.Top

        _header.Height = Px(HeaderHeight)

        ' ==================================
        ' HEADER TEXT
        ' ==================================

        _header.SetParentPageName("Branch Admin")
        _header.SetPageName("Customers")

        ' ==================================
        ' CONTROL ORDER
        ' ==================================

        _rightHost.BringToFront()
        _sidebarHost.BringToFront()
        _header.BringToFront()

    End Sub

    ' ==========================================
    ' SYSTEM HEADING BELOW HEADERBAR
    ' ==========================================

    Private Sub SetupSystemHeading()

        If _rightHost Is Nothing Then Return

        _systemHeading = FindSystemHeading(Me)

        If _systemHeading Is Nothing Then
            _systemHeading = New SystemHeading()
            _systemHeading.Name = "adminCustomersSystemHeading"
        End If

        If _systemHeading.Parent IsNot _rightHost Then
            _systemHeading.Parent = _rightHost
        End If

        _systemHeading.Dock = DockStyle.None
        _systemHeading.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _systemHeading.MinimumSize = Size.Empty
        _systemHeading.MaximumSize = Size.Empty
        _systemHeading.AutoSize = False
        _systemHeading.AutoScroll = False
        _systemHeading.Margin = New Padding(0)
        _systemHeading.Padding = New Padding(0)

        ' Use your existing Designer label text, or set a default if empty.
        _systemHeading.SetHeading(
            "CUSTOMERS",
            "Manage branch customer information")

        _systemHeading.BringToFront()

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

        If Not _ready OrElse _busy Then Return

        If Me.WindowState =
            FormWindowState.Minimized Then Return

        If Me.ClientSize.Width < 1 OrElse
           Me.ClientSize.Height < 1 Then Return

        _busy = True

        Me.SuspendLayout()

        Try

            Me.AutoScroll = False
            Me.AutoScrollMinSize = Size.Empty

            ' ==================================
            ' RESIZE SIDEBAR
            ' ==================================

            If _sidebarHost IsNot Nothing AndAlso
               Not _sidebarHost.IsDisposed Then

                _sidebarHost.Dock = DockStyle.Left

                Dim targetWidth As Integer =
                    GetSidebarWidth()

                If _sidebarHost.Width <> targetWidth Then

                    _sidebarHost.Width = targetWidth

                End If

                If _sidebarHost IsNot _sidebar AndAlso
                   _sidebar IsNot Nothing Then

                    _sidebar.Dock = DockStyle.Fill

                End If

            End If

            ' ==================================
            ' RESIZE RIGHT PANEL
            ' ==================================

            If _rightHost IsNot Nothing AndAlso
               Not _rightHost.IsDisposed Then

                Dim leftEdge As Integer =
                    If(
                        _sidebarHost Is Nothing,
                        0,
                        _sidebarHost.Right
                    )

                leftEdge =
                    Math.Max(
                        0,
                        Math.Min(
                            Me.ClientSize.Width,
                            leftEdge
                        )
                    )

                Dim availableWidth As Integer =
                    Math.Max(
                        0,
                        Me.ClientSize.Width - leftEdge
                    )

                ' NO GAP BETWEEN SIDEBAR
                ' AND RIGHT PANEL

                _rightHost.SetBounds(
                    leftEdge,
                    0,
                    availableWidth,
                    Me.ClientSize.Height
                )

            End If

            ' ==================================
            ' RESIZE HEADERBAR
            ' ==================================

            If _header IsNot Nothing AndAlso
               Not _header.IsDisposed Then

                _header.Dock = DockStyle.Top

                _header.Height = Px(HeaderHeight)

                ' UPDATE THE BREADCRUMB, CLOCK
                ' AND NOTIFICATION POSITIONS

                _header.RefreshHeaderLayout()

            End If

            ' ==================================
            ' RESIZE SYSTEM HEADING BELOW HEADER
            ' ==================================

            If _systemHeading IsNot Nothing AndAlso
               Not _systemHeading.IsDisposed AndAlso
               _rightHost IsNot Nothing Then

                Dim headingTop As Integer = Px(HeaderHeight)
                Dim headingWidth As Integer =
                    Math.Max(1, _rightHost.ClientSize.Width)

                _systemHeading.SetBounds(0, headingTop,
                    headingWidth, _systemHeading.Height)
                _systemHeading.RefreshHeadingLayout()
                ContentPanel.SetBounds(0, _systemHeading.Bottom,
                    _rightHost.ClientSize.Width,
                    Math.Max(0, _rightHost.ClientSize.Height - _systemHeading.Bottom))

            End If

        Finally

            Me.ResumeLayout(True)

            _busy = False

        End Try

    End Sub

    ' ==========================================
    ' FORM RESIZE EVENTS
    ' ==========================================

    Private Sub AdminCustomers_Resize(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Resize

        UpdateResponsiveLayout()

    End Sub

    Private Sub AdminCustomers_ClientSizeChanged(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.ClientSizeChanged

        UpdateResponsiveLayout()

    End Sub

    Private Sub AdminCustomers_ResizeEnd(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.ResizeEnd

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' AVAILABLE PAGE CONTENT AREA
    ' ==========================================

    Public ReadOnly Property ContentBounds As Rectangle

        Get

            Dim leftEdge As Integer =
                If(
                    _sidebarHost Is Nothing,
                    0,
                    _sidebarHost.Right
                )

            Dim topEdge As Integer = Px(HeaderHeight)
            If _systemHeading IsNot Nothing AndAlso Not _systemHeading.IsDisposed Then
                topEdge += _systemHeading.Height
            End If

            Return New Rectangle(
                leftEdge,
                topEdge,
                Math.Max(
                    0,
                    Me.ClientSize.Width - leftEdge
                ),
                Math.Max(
                    0,
                    Me.ClientSize.Height - topEdge
                )
            )

        End Get

    End Property

    ' The application can supply the authenticated user and assigned branch.
    Public Sub SetUser(userName As String, role As String, branchName As String)
        AdminSidebarNavigation1.SetUser(userName, role, branchName)
    End Sub

    Public Event NavigateRequested(pageKey As String)
    Public Event SignOutRequested()

    Private Sub SidebarNavigate(pageKey As String) Handles AdminSidebarNavigation1.NavigateRequested
        RaiseEvent NavigateRequested(pageKey)
    End Sub

    Private Sub SidebarSignOut() Handles AdminSidebarNavigation1.SignOutRequested
        RaiseEvent SignOutRequested()
    End Sub

End Class





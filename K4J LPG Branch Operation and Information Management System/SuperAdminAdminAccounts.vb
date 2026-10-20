
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class SuperAdminAdminAccounts

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

    Private _sidebar As SuperAdminSidebarNavigation
    Private _sidebarHost As Control

    Private _rightHost As Panel
    Private _header As HeaderBar
    Private _systemHeading As SystemHeading
    Private _addAdminAction As RoundedButton
    Private _addAdminFont As Font

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

    Private Sub SuperAdminAdminAccounts_Load(
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

            Dim startWidth As Integer =
                Math.Min(
                    work.Width,
                    Math.Max(
                        minWidth,
                        CInt(
                            work.Width * InitialWidthRatio
                        )
                    )
                )

            Dim startHeight As Integer =
                Math.Min(
                    work.Height,
                    Math.Max(
                        minHeight,
                        CInt(
                            work.Height * InitialHeightRatio
                        )
                    )
                )

            Me.Size = New Size(
                startWidth,
                startHeight
            )

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
            SetupAddAdminAction()

            If _sidebar IsNot Nothing Then

                _sidebar.SetActivePage("Admin Accounts")

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

    Private Sub SuperAdminAdminAccounts_Shown(
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
        _sidebarHost = AdminAccountsMainContainer.Panel1
        AdminAccountsMainContainer.Dock = DockStyle.Fill
        AdminAccountsMainContainer.FixedPanel = FixedPanel.Panel1
        AdminAccountsMainContainer.IsSplitterFixed = True
        AdminAccountsMainContainer.Panel1MinSize = 0
        AdminAccountsMainContainer.Panel2MinSize = 0
        _sidebar.Dock = DockStyle.Fill
        _sidebar.Margin = Padding.Empty
        _sidebar.AutoScroll = False
    End Sub

    ' ==========================================
    ' HEADER SETUP
    ' ==========================================

    Private Sub SetupHeader()
        _rightHost = AdminAccountsMainContainer.Panel2
        _rightHost.BackColor = Color.FromArgb(241, 245, 249)
        adminAccountsContentContainer.Dock = DockStyle.Fill
        adminAccountsContentContainer.Panel1MinSize = 0
        adminAccountsContentContainer.Panel2MinSize = 0
        _header = HeaderBar1
        _header.Dock = DockStyle.Fill
        _header.Margin = Padding.Empty
        _header.SetParentPageName("Super Admin")
        _header.SetPageName("Admin Accounts")
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
        _systemHeading.SetHeading("ADMIN ACCOUNTS", "Manage branch administrator accounts")
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

    Private Sub SetupAddAdminAction()
        ' Reuse the Designer button when present; otherwise create it only at runtime.
        Dim existing = Controls.Find("addAdminButton", True)
        If existing.Length > 0 Then _addAdminAction = TryCast(existing(0), RoundedButton)
        If _addAdminAction Is Nothing Then
            _addAdminAction = New RoundedButton With {
                .Name = "addAdminButton",
                .FillColor = Color.FromArgb(242, 106, 0),
                .ForeColor = Color.White,
                .BorderThickness = 0,
                .CornerRadius = 10
            }
            _systemHeading.Controls.Add(_addAdminAction)
        End If
        _addAdminAction.SetIconBeforeText("plus.png", "Add Admin")
        _addAdminAction.BringToFront()
        AddHandler Disposed, Sub()
                                 If _addAdminFont IsNot Nothing Then _addAdminFont.Dispose()
                             End Sub
    End Sub

    Private Sub LayoutAddAdminAction()
        If _addAdminAction Is Nothing Then Return
        Dim scale = Math.Clamp(_systemHeading.Width / CDbl(Px(924)), 0.85R, 1.1R)
        Dim points = CSng(Math.Clamp(10.0R * scale, 9.0R, 11.0R))
        If _addAdminFont Is Nothing OrElse Math.Abs(_addAdminFont.SizeInPoints - points) > 0.05F Then
            Dim previous = _addAdminFont
            _addAdminFont = New Font("Segoe UI", points, FontStyle.Bold)
            _addAdminAction.Font = _addAdminFont
            If previous IsNot Nothing Then previous.Dispose()
        End If
        Dim icon = Px(CInt(16 * scale))
        _addAdminAction.IconWidth = icon
        _addAdminAction.IconHeight = icon
        _addAdminAction.IconTextSpacing = Px(8)
        Dim width = Math.Max(Px(CInt(138 * scale)),
                             TextRenderer.MeasureText("Add Admin", _addAdminFont).Width + icon + Px(32))
        Dim height = Math.Max(Px(CInt(34 * scale)), _addAdminFont.Height + Px(14))
        Dim parent = _addAdminAction.Parent
        _addAdminAction.SetBounds(Math.Max(0, parent.ClientSize.Width - Px(18) - width),
                                 Math.Max(0, (parent.ClientSize.Height - height) \ 2), width, height)
        If parent Is _systemHeading Then
            For Each child As Control In _systemHeading.Controls
                Dim label = TryCast(child, Label)
                If label Is Nothing Then Continue For
                label.AutoEllipsis = True
                label.Width = Math.Max(1, _addAdminAction.Left - Px(12) - label.Left)
            Next
        End If
        _addAdminAction.BringToFront()
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
            SetSectionSize(AdminAccountsMainContainer, GetSidebarWidth())
            SetSectionSize(adminAccountsContentContainer, Px(HeaderHeight))
            _header.RefreshHeaderLayout()
            _systemHeading.RefreshHeadingLayout()
            SetSectionSize(mainContentHolder, Math.Max(Px(65), _systemHeading.Height))
            LayoutAddAdminAction()
            ' Keep the cards aligned with SystemHeading's 18-pixel logical inset.
            AdminAccountsCards1.Dock = DockStyle.Fill
            AdminAccountsCards1.Padding = New Padding(Px(18), Px(6), Px(18), Px(6))
            SetSectionSize(mainContents, Px(120))
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

    Private Sub SuperAdminAdminAccounts_Resize(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Resize

        UpdateResponsiveLayout()

    End Sub

    Private Sub SuperAdminAdminAccounts_ClientSizeChanged(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.ClientSizeChanged

        UpdateResponsiveLayout()

    End Sub

    Private Sub SuperAdminAdminAccounts_ResizeEnd(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.ResizeEnd

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' AVAILABLE ADMIN ACCOUNTS CONTENT AREA
    ' ==========================================

    Public ReadOnly Property AdminAccountsContentBounds As Rectangle
        Get
            Dim content = mainContentHolder.Panel2
            If IsHandleCreated AndAlso content.IsHandleCreated Then
                Return RectangleToClient(content.RectangleToScreen(content.ClientRectangle))
            End If
            Return New Rectangle(AdminAccountsMainContainer.Panel2.Left,
                                 adminAccountsContentContainer.Panel2.Top + mainContentHolder.Panel2.Top,
                                 content.ClientSize.Width, content.ClientSize.Height)
        End Get
    End Property

End Class



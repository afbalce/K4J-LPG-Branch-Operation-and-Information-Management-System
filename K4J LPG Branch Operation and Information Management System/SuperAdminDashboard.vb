
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class SuperAdminDashboard

    ' ==========================================
    ' RESPONSIVE SETTINGS
    ' ==========================================

    Private Const StartMaximized As Boolean = False


    Private Const SidebarWidthRatio As Double = 0.18

    Private Const MinimumSidebarWidth As Integer = 180
    Private Const MaximumSidebarWidth As Integer = 280

    Private Const MinimumContentWidth As Integer = 300

    ' ==========================================
    ' STATE
    ' ==========================================

    Private layoutReady As Boolean = False
    Private updatingLayout As Boolean = False

    ' ==========================================
    ' SIDEBAR REFERENCES
    ' ==========================================

    Private sidebarNavigation As SuperAdminSidebarNavigation =
        Nothing

    Private sidebarContainer As Control = Nothing

    ' ==========================================
    ' DPI SCALING
    ' ==========================================

    Private Function Px(value As Integer) As Integer

        Dim scale As Double =
            Math.Max(0.75, Me.DeviceDpi / 96.0R)

        Return Math.Max(1, CInt(value * scale))

    End Function

    ' ==========================================
    ' FORM LOAD
    ' ==========================================

    Private Sub SuperAdminDashboard_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        layoutReady = False

        Me.SuspendLayout()

        Try

            ' ==================================
            ' WINDOW SETTINGS
            ' ==================================

            Me.FormBorderStyle = FormBorderStyle.Sizable

            Me.MaximizeBox = True
            Me.MinimizeBox = True

            Me.MaximumSize = Size.Empty

            ' IMPORTANT:
            ' PREVENT FORM-LEVEL SCROLLBARS

            Me.AutoScroll = False
            Me.AutoSize = False

            Me.HorizontalScroll.Visible = False

            ' ==================================
            ' GET SCREEN WORKING AREA
            ' ==================================

            WindowSizing.ApplyStartingSize(Me)

            ' ==================================
            ' SETUP SIDEBAR
            ' ==================================

            SetupSidebarNavigation()

            layoutReady = True

        Finally

            Me.ResumeLayout(True)

        End Try

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' FORM SHOWN
    ' ==========================================

    Private Sub SuperAdminDashboard_Shown(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Shown

        If StartMaximized Then
            Me.WindowState = FormWindowState.Maximized
        End If

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' FIND EXISTING SIDEBAR
    ' ==========================================

    Private Function FindSidebarNavigation(
        root As Control
    ) As SuperAdminSidebarNavigation

        If root Is Nothing Then Return Nothing

        If TypeOf root Is SuperAdminSidebarNavigation Then

            Return DirectCast(
                root,
                SuperAdminSidebarNavigation
            )

        End If

        For Each child As Control In root.Controls

            Dim found As SuperAdminSidebarNavigation =
                FindSidebarNavigation(child)

            If found IsNot Nothing Then
                Return found
            End If

        Next

        Return Nothing

    End Function

    ' ==========================================
    ' SETUP RESPONSIVE SIDEBAR
    ' ==========================================

    Private Sub SetupSidebarNavigation()

        sidebarNavigation =
            FindSidebarNavigation(Me)

        If sidebarNavigation Is Nothing Then

            MessageBox.Show(
                "SuperAdminSidebarNavigation was not found." &
                Environment.NewLine &
                "Please add the sidebar UserControl " &
                "in the SuperAdminDashboard Designer.",
                "Sidebar Not Found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Return

        End If

        ' ==================================
        ' CHECK EXISTING SIDEBAR CONTAINER
        ' ==================================

        Dim existingParent As Control =
            sidebarNavigation.Parent

        If existingParent IsNot Nothing AndAlso
           TypeOf existingParent Is Panel AndAlso
           existingParent.Parent Is Me AndAlso
           existingParent.Controls.Count = 1 Then

            sidebarContainer = existingParent

            ' SIDEBAR FILLS ITS CONTAINER

            sidebarNavigation.Dock = DockStyle.Fill

            sidebarNavigation.Margin = New Padding(0)

        Else

            ' SIDEBAR DIRECTLY ON THE FORM

            If sidebarNavigation.Parent IsNot Me Then

                Me.Controls.Add(sidebarNavigation)

            End If

            sidebarContainer = sidebarNavigation

        End If

        ' ==================================
        ' REMOVE FIXED SIZE RESTRICTIONS
        ' ==================================

        sidebarContainer.MinimumSize = Size.Empty
        sidebarContainer.MaximumSize = Size.Empty

        sidebarContainer.AutoSize = False
        sidebarContainer.Margin = New Padding(0)
        sidebarContainer.Padding = New Padding(0)

        ' ==================================
        ' DISABLE UNWANTED SCROLLING
        ' ==================================

        sidebarNavigation.AutoScroll = False

        Dim scrollingContainer As ScrollableControl =
            TryCast(sidebarContainer, ScrollableControl)

        If scrollingContainer IsNot Nothing Then

            scrollingContainer.AutoScroll = False

        End If

        ' ==================================
        ' DOCK SIDEBAR ON THE LEFT
        ' ==================================

        sidebarContainer.Dock = DockStyle.Left

        sidebarContainer.Width = GetSidebarWidth()

        sidebarContainer.BringToFront()

    End Sub

    ' ==========================================
    ' CALCULATE RESPONSIVE SIDEBAR WIDTH
    ' ==========================================

    Private Function GetSidebarWidth() As Integer

        Dim formWidth As Integer =
            Me.ClientSize.Width

        If formWidth <= 0 Then Return 1

        ' ==================================
        ' WIDTH BASED ON WINDOW SIZE
        ' ==================================

        Dim preferredWidth As Integer =
            CInt(formWidth * SidebarWidthRatio)

        ' ==================================
        ' MINIMUM AND MAXIMUM LIMITS
        ' ==================================

        Dim minimumWidth As Integer =
            Px(MinimumSidebarWidth)

        Dim maximumWidth As Integer =
            Px(MaximumSidebarWidth)

        Dim sidebarWidth As Integer =
            Math.Max(
                minimumWidth,
                Math.Min(
                    maximumWidth,
                    preferredWidth
                )
            )

        ' ==================================
        ' ALWAYS LEAVE ROOM FOR CONTENT
        ' ==================================

        Dim reservedContentWidth As Integer =
            Math.Min(
                Px(MinimumContentWidth),
                CInt(formWidth * 0.6)
            )

        Dim availableSidebarWidth As Integer =
            Math.Max(
                1,
                formWidth - reservedContentWidth
            )

        sidebarWidth =
            Math.Min(
                sidebarWidth,
                availableSidebarWidth
            )

        Return Math.Max(1, sidebarWidth)

    End Function

    ' ==========================================
    ' RESIZE SIDEBAR
    ' ==========================================

    Private Sub ResizeSidebarNavigation()

        If sidebarNavigation Is Nothing Then Return
        If sidebarContainer Is Nothing Then Return

        If sidebarNavigation.IsDisposed OrElse
           sidebarContainer.IsDisposed Then Return

        If Me.ClientSize.Width <= 0 OrElse
           Me.ClientSize.Height <= 0 Then Return

        ' ==================================
        ' CALCULATE NEW WIDTH
        ' ==================================

        Dim newWidth As Integer =
            GetSidebarWidth()

        ' ==================================
        ' USE DOCKING INSTEAD OF MANUAL BOUNDS
        ' ==================================

        If sidebarContainer.Dock <> DockStyle.Left Then

            sidebarContainer.Dock = DockStyle.Left

        End If

        If sidebarContainer.Width <> newWidth Then

            sidebarContainer.Width = newWidth

        End If

        ' ==================================
        ' FILL PARENT IF SIDEBAR IS NESTED
        ' ==================================

        If sidebarContainer IsNot sidebarNavigation Then

            If sidebarNavigation.Dock <> DockStyle.Fill Then

                sidebarNavigation.Dock = DockStyle.Fill

            End If

        End If

        ' ==================================
        ' AVOID HORIZONTAL FORM SCROLLING
        ' ==================================

        Me.AutoScroll = False

    End Sub

    ' ==========================================
    ' MAIN RESPONSIVE LAYOUT
    ' ==========================================

    Private Sub UpdateResponsiveLayout()

        If Not layoutReady Then Return
        If updatingLayout Then Return

        If Me.WindowState =
            FormWindowState.Minimized Then Return

        If Me.ClientSize.Width <= 0 OrElse
           Me.ClientSize.Height <= 0 Then Return

        updatingLayout = True

        Me.SuspendLayout()

        Try

            ' ==================================
            ' RESPONSIVE SIDEBAR
            ' ==================================

            ResizeSidebarNavigation()

            ' ==================================
            ' FUTURE MAIN DASHBOARD CONTENT
            ' ==================================

            ' The sidebar resizes automatically.
            '
            ' When dashboard pages are added,
            ' their containers can be docked
            ' to Fill in the remaining area.

        Finally

            Me.ResumeLayout(True)

            updatingLayout = False

        End Try

    End Sub

    ' ==========================================
    ' WINDOW SIZE CHANGED
    ' ==========================================

    Private Sub SuperAdminDashboard_ClientSizeChanged(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.ClientSizeChanged

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' WINDOW RESIZE COMPLETED
    ' ==========================================

    Private Sub SuperAdminDashboard_ResizeEnd(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.ResizeEnd

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' MAXIMIZE / RESTORE / MINIMIZE
    ' ==========================================

    Private Sub SuperAdminDashboard_Resize(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Resize

        If Me.WindowState =
            FormWindowState.Minimized Then Return

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' AVAILABLE MAIN CONTENT AREA
    ' ==========================================

    Public ReadOnly Property DashboardContentBounds As Rectangle

        Get

            Dim sidebarWidth As Integer = 0

            If sidebarContainer IsNot Nothing AndAlso
               Not sidebarContainer.IsDisposed Then

                sidebarWidth = sidebarContainer.Width

            End If

            Return New Rectangle(
                sidebarWidth,
                0,
                Math.Max(
                    0,
                    Me.ClientSize.Width - sidebarWidth
                ),
                Math.Max(
                    0,
                    Me.ClientSize.Height
                )
            )

        End Get

    End Property

End Class


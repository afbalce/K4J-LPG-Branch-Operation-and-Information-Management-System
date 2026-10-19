
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Windows.Forms

Public Class SuperAdminSettings

    ' ==========================================
    ' WINDOW SETTINGS
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
    ' SETTINGS BUTTON COLORS
    ' ==========================================

    Private ReadOnly _normalFill As Color =
        Color.White

    Private ReadOnly _normalText As Color =
        Color.FromArgb(73, 87, 109)

    Private ReadOnly _hoverFill As Color =
        Color.FromArgb(232, 101, 10)

    ' ==========================================
    ' EXISTING COMPONENTS
    ' ==========================================

    Private _sidebar As SuperAdminSidebarNavigation
    Private _header As HeaderBar
    Private _systemHeading As SystemHeading

    ' ==========================================
    ' STATE
    ' ==========================================

    Private _ready As Boolean = False
    Private _busy As Boolean = False

    Private _activeSettingsPage As String = "General"

    ' ==========================================
    ' FONT SETTINGS
    ' ==========================================

    Private _fontFamilyName As String = "Segoe UI"
    Private _fontStyle As FontStyle = FontStyle.Bold

    Private ReadOnly _buttonFonts As New List(Of Font)

    ' ==========================================
    ' DPI SCALING
    ' Used by the main window layout only.
    ' ==========================================

    Private Function Px(value As Integer) As Integer

        Return Math.Max(
            1,
            CInt(
                value *
                Math.Max(
                    0.75R,
                    Me.DeviceDpi / 96.0R
                )
            )
        )

    End Function

    ' ==========================================
    ' DETECT INTER SEMIBOLD
    ' ==========================================

    Private Sub SetupInterFont()

        Using installed As New InstalledFontCollection()

            Dim hasSemiBold As Boolean = False
            Dim hasInter As Boolean = False

            For Each family As FontFamily In installed.Families

                If String.Equals(
                    family.Name,
                    "Inter SemiBold",
                    StringComparison.OrdinalIgnoreCase
                ) Then

                    hasSemiBold = True

                ElseIf String.Equals(
                    family.Name,
                    "Inter",
                    StringComparison.OrdinalIgnoreCase
                ) Then

                    hasInter = True

                End If

            Next

            If hasSemiBold Then

                _fontFamilyName = "Inter SemiBold"
                _fontStyle = FontStyle.Regular

            ElseIf hasInter Then

                _fontFamilyName = "Inter"
                _fontStyle = FontStyle.Bold

            Else

                _fontFamilyName = "Segoe UI"
                _fontStyle = FontStyle.Bold

            End If

        End Using

    End Sub

    ' ==========================================
    ' APPLY INTER SEMIBOLD
    ' Preserve each button's Designer font size.
    ' ==========================================

    Private Sub ApplyButtonFont(
        btn As RoundedButton
    )

        If btn Is Nothing Then Return

        Dim originalSize As Single =
            btn.Font.SizeInPoints

        Dim newFont As New Font(
            _fontFamilyName,
            originalSize,
            _fontStyle,
            GraphicsUnit.Point
        )

        btn.Font = newFont

        _buttonFonts.Add(newFont)

    End Sub

    ' ==========================================
    ' FORM LOAD
    ' ==========================================

    Private Sub SuperAdminSettings_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        _ready = False

        Me.SuspendLayout()

        Try

            ' ==================================
            ' WINDOW CONFIGURATION
            ' ==================================

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
            ' SCREEN WORKING AREA
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
            ' INITIAL WINDOW SIZE
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

            Me.Size =
                New Size(startWidth, startHeight)

            Me.Location = New Point(
                work.Left + (work.Width - Me.Width) \ 2,
                work.Top + (work.Height - Me.Height) \ 2
            )

            ' ==================================
            ' INITIALIZE COMPONENTS
            ' ==================================

            SetupInterFont()

            SetupSidebar()
            SetupHeader()
            SetupSystemHeading()

            ' Settings controls use their
            ' original Designer layout.
            SetupSettingsNavigation()

            If _sidebar IsNot Nothing Then

                _sidebar.SetActivePage("Settings")

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

    Private Sub SuperAdminSettings_Shown(
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
    ' MAIN SIDEBAR
    ' Responsive resizing remains enabled.
    ' ==========================================

    Private Sub SetupSidebar()

        _sidebar = SuperAdminSidebarNavigation1

        SettingsMainContainer.Dock =
            DockStyle.Fill

        SettingsMainContainer.IsSplitterFixed = True

        SettingsMainContainer.Panel1MinSize = 0
        SettingsMainContainer.Panel2MinSize = 0

        _sidebar.Dock = DockStyle.Fill

    End Sub

    ' ==========================================
    ' HEADER
    ' Responsive resizing remains enabled.
    ' ==========================================

    Private Sub SetupHeader()

        SettingsMainContainer.Panel2.BackColor =
            Color.FromArgb(241, 245, 249)

        settingsContentContainer.Dock =
            DockStyle.Fill

        settingsContentContainer.Panel1MinSize = 0
        settingsContentContainer.Panel2MinSize = 0

        _header = HeaderBar1

        _header.Dock = DockStyle.Fill

        _header.SetParentPageName("Super Admin")
        _header.SetPageName("Settings")

    End Sub

    ' ==========================================
    ' SYSTEM HEADING
    ' Responsive resizing remains enabled.
    ' ==========================================

    Private Sub SetupSystemHeading()

        _systemHeading = SystemHeading1

        mainContentHolder.Dock =
            DockStyle.Fill

        mainContentHolder.Panel1MinSize = 0
        mainContentHolder.Panel2MinSize = 0

        _systemHeading.Dock = DockStyle.Top

        _systemHeading.SetHeading(
            "SYSTEM SETTINGS",
            "Global configuration and system preferences"
        )

    End Sub

    ' ==========================================
    ' SETTINGS NAVIGATION
    '
    ' NO CUSTOM RESIZING.
    ' NO REPARENTING.
    ' NO SETBOUNDS.
    ' NO CHANGING DOCK OR ANCHOR.
    '
    ' All five controls retain their
    ' original Visual Studio Designer layout.
    ' ==========================================

    Private Sub SetupSettingsNavigation()

        ' ==================================
        ' USE EXISTING DESIGNER CONTROLS
        ' ==================================

        ' SettingsSidebarNavigation
        ' settingsSidebarPanel
        ' GeneralButton
        ' BranchButton
        ' SecurityButton

        ' The size, location, parent,
        ' Dock and Anchor properties
        ' are not changed here.

        ' ==================================
        ' BUTTON FONTS
        ' Preserve Designer font sizes.
        ' ==================================

        ApplyButtonFont(GeneralButton)
        ApplyButtonFont(BranchButton)
        ApplyButtonFont(SecurityButton)

        ' ==================================
        ' LEFT TEXT ALIGNMENT
        ' ==================================

        GeneralButton.TextAlign =
            ContentAlignment.MiddleLeft

        BranchButton.TextAlign =
            ContentAlignment.MiddleLeft

        SecurityButton.TextAlign =
            ContentAlignment.MiddleLeft

        ' ==================================
        ' REGISTER BUTTON EVENTS
        ' ==================================

        RegisterSettingsButton(GeneralButton)
        RegisterSettingsButton(BranchButton)
        RegisterSettingsButton(SecurityButton)

        ' ==================================
        ' DEFAULT ACTIVE BUTTON
        ' ==================================

        SetActiveSettingsPage("General")

    End Sub

    ' ==========================================
    ' REGISTER SETTINGS BUTTON EVENTS
    ' ==========================================

    Private Sub RegisterSettingsButton(
        btn As RoundedButton
    )

        If btn Is Nothing Then Return

        ' Avoid duplicate event handlers.

        RemoveHandler btn.Click,
            AddressOf SettingsButton_Click

        RemoveHandler btn.MouseEnter,
            AddressOf SettingsButton_MouseEnter

        RemoveHandler btn.MouseLeave,
            AddressOf SettingsButton_MouseLeave

        AddHandler btn.Click,
            AddressOf SettingsButton_Click

        AddHandler btn.MouseEnter,
            AddressOf SettingsButton_MouseEnter

        AddHandler btn.MouseLeave,
            AddressOf SettingsButton_MouseLeave

    End Sub

    ' ==========================================
    ' IDENTIFY SETTINGS BUTTON
    ' ==========================================

    Private Function GetSettingsPage(
        btn As Control
    ) As String

        If Object.ReferenceEquals(
            btn,
            GeneralButton
        ) Then

            Return "General"

        End If

        If Object.ReferenceEquals(
            btn,
            BranchButton
        ) Then

            Return "Branch"

        End If

        If Object.ReferenceEquals(
            btn,
            SecurityButton
        ) Then

            Return "Security"

        End If

        Return String.Empty

    End Function

    ' ==========================================
    ' SETTINGS BUTTON CLICK
    ' ==========================================

    Private Sub SettingsButton_Click(
        sender As Object,
        e As EventArgs
    )

        Dim page As String =
            GetSettingsPage(
                TryCast(sender, Control)
            )

        If page = String.Empty Then Return

        SetActiveSettingsPage(page)

    End Sub

    ' ==========================================
    ' SET ACTIVE SETTINGS PAGE
    ' ==========================================

    Public Sub SetActiveSettingsPage(
        pageKey As String
    )

        If String.IsNullOrWhiteSpace(pageKey) Then
            Return
        End If

        Select Case pageKey.Trim().ToLowerInvariant()

            Case "general"
                _activeSettingsPage = "General"

            Case "branch", "branches"
                _activeSettingsPage = "Branch"

            Case "security"
                _activeSettingsPage = "Security"

            Case Else
                Return

        End Select

        For Each btn As RoundedButton In
            New RoundedButton() {
                GeneralButton,
                BranchButton,
                SecurityButton
            }

            Dim isSelected As Boolean =
                GetSettingsPage(btn) = _activeSettingsPage

            ApplySettingsButtonStyle(
                btn,
                isSelected,
                False
            )

        Next

    End Sub

    ' ==========================================
    ' ORANGE HOVER
    ' ==========================================

    Private Sub SettingsButton_MouseEnter(
        sender As Object,
        e As EventArgs
    )

        Dim btn As RoundedButton =
            TryCast(sender, RoundedButton)

        If btn Is Nothing Then Return

        If GetSettingsPage(btn) =
            _activeSettingsPage Then

            Return

        End If

        ApplySettingsButtonStyle(
            btn,
            False,
            True
        )

    End Sub

    ' ==========================================
    ' MOUSE LEAVE
    ' ==========================================

    Private Sub SettingsButton_MouseLeave(
        sender As Object,
        e As EventArgs
    )

        Dim btn As RoundedButton =
            TryCast(sender, RoundedButton)

        If btn Is Nothing Then Return

        Dim selected As Boolean =
            GetSettingsPage(btn) = _activeSettingsPage

        ApplySettingsButtonStyle(
            btn,
            selected,
            False
        )

    End Sub

    ' ==========================================
    ' APPLY BUTTON COLORS
    '
    ' Appearance only.
    ' Does not resize or reposition buttons.
    ' ==========================================

    Private Sub ApplySettingsButtonStyle(
        btn As RoundedButton,
        selected As Boolean,
        hovered As Boolean
    )

        If btn Is Nothing Then Return

        If selected OrElse hovered Then

            ' Orange selected/hover background.

            btn.FillColor = _hoverFill

            btn.ForeColor = Color.White

            btn.BorderColor = _hoverFill
            btn.BorderThickness = 0

        Else

            ' White normal background.

            btn.FillColor = _normalFill

            btn.ForeColor = _normalText

            btn.BorderColor = Color.Transparent
            btn.BorderThickness = 0

        End If

        btn.TextAlign =
            ContentAlignment.MiddleLeft

        btn.Invalidate()

    End Sub

    ' ==========================================
    ' SAFE SPLITTER DISTANCE
    '
    ' Used only for the main form containers.
    ' NOT used for SplitContainer1.
    ' ==========================================

    Private Sub SetSplitDistanceSafe(
        split As SplitContainer,
        preferred As Integer
    )

        If split Is Nothing Then Return

        If split.Panel1Collapsed OrElse
           split.Panel2Collapsed Then Return

        Dim available As Integer

        If split.Orientation =
            Orientation.Vertical Then

            available = split.ClientSize.Width

        Else

            available = split.ClientSize.Height

        End If

        If available <= split.SplitterWidth Then
            Return
        End If

        Dim maximumDistance As Integer =
            available -
            split.SplitterWidth -
            split.Panel2MinSize

        Dim minimumDistance As Integer =
            split.Panel1MinSize

        If maximumDistance < minimumDistance Then
            Return
        End If

        Dim newDistance As Integer =
            Math.Max(
                minimumDistance,
                Math.Min(
                    preferred,
                    maximumDistance
                )
            )

        If split.SplitterDistance = newDistance Then
            Return
        End If

        Try

            split.SplitterDistance = newDistance

        Catch ex As ArgumentOutOfRangeException

            System.Diagnostics.Debug.WriteLine(
                ex.Message
            )

        Catch ex As InvalidOperationException

            System.Diagnostics.Debug.WriteLine(
                ex.Message
            )

        End Try

    End Sub

    ' ==========================================
    ' MAIN SIDEBAR WIDTH
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

        Dim reserved As Integer =
            Math.Min(
                Px(MinimumContentWidth),
                CInt(width * 0.6R)
            )

        Return Math.Max(
            1,
            Math.Min(
                bounded,
                width - reserved
            )
        )

    End Function

    ' ==========================================
    ' MAIN RESPONSIVE WINDOW LAYOUT
    '
    ' Only the main layout is resized.
    '
    ' These are NOT manually resized:
    '
    ' SettingsSidebarNavigation
    ' settingsSidebarPanel
    ' GeneralButton
    ' BranchButton
    ' SecurityButton
    ' ==========================================

    Private Sub UpdateResponsiveLayout()

        If Not _ready Then Return
        If _busy Then Return

        If Me.WindowState =
            FormWindowState.Minimized Then Return

        _busy = True

        Try

            Me.SuspendLayout()

            ' ==================================
            ' MAIN LEFT SIDEBAR
            ' ==================================

            SetSplitDistanceSafe(
                SettingsMainContainer,
                GetSidebarWidth()
            )

            ' ==================================
            ' HEADER HEIGHT
            ' ==================================

            SetSplitDistanceSafe(
                settingsContentContainer,
                Px(HeaderHeight)
            )

            If _header IsNot Nothing Then

                _header.RefreshHeaderLayout()

            End If

            ' ==================================
            ' SYSTEM HEADING
            ' ==================================

            If _systemHeading IsNot Nothing Then

                _systemHeading.RefreshHeadingLayout()

                SetSplitDistanceSafe(
                    mainContentHolder,
                    Math.Max(
                        Px(65),
                        _systemHeading.Height
                    )
                )

            End If

            ' ==================================
            ' SETTINGS NAVIGATION
            '
            ' NO RESIZING HERE.
            ' ==================================

        Finally

            Me.ResumeLayout(True)

            _busy = False

        End Try

    End Sub

    ' ==========================================
    ' FORM RESIZE
    ' ==========================================

    Private Sub SuperAdminSettings_Resize(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Resize

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' CLIENT SIZE CHANGED
    ' ==========================================

    Private Sub SuperAdminSettings_ClientSizeChanged(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.ClientSizeChanged

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' RESIZE END
    ' ==========================================

    Private Sub SuperAdminSettings_ResizeEnd(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.ResizeEnd

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' SETTINGS CONTENT BOUNDS
    ' ==========================================

    Public ReadOnly Property SettingsContentBounds As Rectangle

        Get

            If Not SplitContainer1.IsHandleCreated Then

                Return Rectangle.Empty

            End If

            Return New Rectangle(
                Me.PointToClient(
                    SplitContainer1.Panel2.PointToScreen(
                        Point.Empty
                    )
                ),
                SplitContainer1.Panel2.ClientSize
            )

        End Get

    End Property

    ' ==========================================
    ' CLEANUP
    ' ==========================================

    Private Sub SuperAdminSettings_FormClosed(
        sender As Object,
        e As FormClosedEventArgs
    ) Handles MyBase.FormClosed

        For Each ownedFont As Font In _buttonFonts

            ownedFont.Dispose()

        Next

        _buttonFonts.Clear()

    End Sub

End Class

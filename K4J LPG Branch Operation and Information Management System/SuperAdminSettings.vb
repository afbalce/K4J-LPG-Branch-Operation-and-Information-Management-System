
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

    Private _settingsVerticalOnly As VScrollBar
    Private _settingsLayoutPending As Boolean
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
    ' Used by the window and settings navigation layouts.
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
    ' Use larger, readable settings navigation text.
    ' ==========================================

    Private Sub ApplyButtonFont(
        btn As RoundedButton
    )

        If btn Is Nothing Then Return

        Dim buttonFontSize As Single = 11.0F

        Dim newFont As New Font(
            _fontFamilyName,
            buttonFontSize,
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

            WindowSizing.ApplyStartingSize(Me)

            ' ==================================
            ' INITIALIZE COMPONENTS
            ' ==================================

            SetupInterFont()

            SetupSidebar()
            SetupHeader()
            SetupSystemHeading()

            ' Configure the responsive settings navigation.
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
    ' ==========================================

    Private Sub SetupSettingsNavigation()

        SplitContainer1.Panel1MinSize = 0
        SplitContainer1.Panel2MinSize = 0
        SplitContainer1.IsSplitterFixed = True
        SettingsSidebarNavigation.Dock = DockStyle.None
        SettingsSidebarNavigation.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        SettingsSidebarNavigation.Padding = New Padding(Px(3))
        SettingsSidebarNavigation.AutoScroll = False
        SettingsSidebarNavigation.AutoScrollMinSize = Size.Empty
        settingsSidebarPanel.Dock = DockStyle.None
        settingsSidebarPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        _settingsVerticalOnly = New VScrollBar With {
            .Name = "settingsVerticalOnly",
            .Width = SystemInformation.VerticalScrollBarWidth,
            .Visible = False,
            .Minimum = 0
        }
        SettingsSidebarNavigation.Controls.Add(_settingsVerticalOnly)
        _settingsVerticalOnly.BringToFront()
        AddHandler _settingsVerticalOnly.ValueChanged, AddressOf SettingsVertical_ValueChanged
        For Each control As Control In New Control() {SettingsSidebarNavigation,
            settingsSidebarPanel, GeneralButton, BranchButton, SecurityButton}
            AddHandler control.MouseWheel, AddressOf SettingsNavigation_MouseWheel
        Next

        For Each button As RoundedButton In {GeneralButton, BranchButton, SecurityButton}
            button.Dock = DockStyle.None
            button.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        Next

        ' ==================================
        ' BUTTON FONTS
        ' Use larger settings navigation text.
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

        ApplySettingsButtonStyle(btn, GetSettingsPage(btn) = _activeSettingsPage, True)

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

        btn.FillColor = If(hovered, _hoverFill, Color.Transparent)
        btn.ForeColor = If(hovered, Color.White, _normalText)
        btn.BorderColor = If(selected, Color.FromArgb(223, 149, 63), Color.Transparent)
        btn.BorderThickness = If(selected, 2, 0)

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
    ' RESPONSIVE WINDOW AND SETTINGS LAYOUT
    ' ==========================================

    Private Sub UpdateSettingsNavigationLayout()

        Dim availableWidth As Integer = SplitContainer1.ClientSize.Width
        Dim preferredWidth As Integer = Math.Max(Px(160), Math.Min(Px(220), CInt(availableWidth * 0.22R)))
        SetSplitDistanceSafe(SplitContainer1, Math.Min(preferredWidth, CInt(availableWidth * 0.45R)))

        Dim inset As Integer = Px(8)
        Dim gap As Integer = Px(6)
        ' Match SuperAdminSidebarNavigation: size to the visible viewport
        ' and reserve space for a vertical-only scrollbar.
        Dim navigationLimit As Size = SplitContainer1.Panel1.ClientSize
        SettingsSidebarNavigation.MaximumSize = New Size(Math.Max(1, navigationLimit.Width), Math.Max(1, navigationLimit.Height))
        SettingsSidebarNavigation.Bounds = SplitContainer1.Panel1.ClientRectangle
        Dim viewport As Size = SettingsSidebarNavigation.ClientSize
        Dim padding As Padding = SettingsSidebarNavigation.Padding
        Dim panelWidth As Integer = Math.Max(1,
            viewport.Width - padding.Horizontal - _settingsVerticalOnly.Width)
        panelWidth = Math.Min(Px(220), panelWidth)
        settingsSidebarPanel.MaximumSize = New Size(panelWidth, 0)
        Dim buttonWidth As Integer = Math.Max(1, panelWidth - inset * 2)
        ' Normalize physical pixels before choosing a point size (avoid double DPI scaling).
        Dim widthScale As Double = Math.Max(0.0R, Math.Min(1.0R,
            (viewport.Width - Px(160)) / CDbl(Px(220) - Px(160))))
        Dim responsiveFontSize As Single = CSng(Math.Round((8.0R + 3.0R * widthScale) * 4.0R) / 4.0R)
        Dim top As Integer = inset

        For Each button As RoundedButton In {GeneralButton, BranchButton, SecurityButton}
            If Math.Abs(button.Font.SizeInPoints - responsiveFontSize) > 0.01F Then
                Dim previousFont As Font = button.Font
                Dim nextFont As New Font(_fontFamilyName, responsiveFontSize, _fontStyle, GraphicsUnit.Point)
                button.Font = nextFont
                _buttonFonts.Add(nextFont)
                If _buttonFonts.Remove(previousFont) Then previousFont.Dispose()
            End If
            ' Measure again after scaling so labels cannot overlap the next button.
            Dim labelSize As Size = TextRenderer.MeasureText(button.Text, button.Font,
                New Size(Math.Max(1, buttonWidth - Px(12)), Integer.MaxValue),
                TextFormatFlags.WordBreak Or TextFormatFlags.NoPrefix)
            Dim buttonHeight As Integer = Math.Max(Px(30), labelSize.Height + Px(10))
            button.MinimumSize = New Size(1, Px(25))
            button.MaximumSize = New Size(buttonWidth, buttonHeight)
            button.SetBounds(inset, top, buttonWidth, buttonHeight)
            top += buttonHeight + gap
        Next

        Dim totalHeight As Integer = top - gap + inset
        Dim viewportHeight As Integer = Math.Max(1, viewport.Height - padding.Vertical)
        Dim maxOffset As Integer = Math.Max(0, totalHeight - viewportHeight)
        _settingsVerticalOnly.SmallChange = Px(35)
        _settingsVerticalOnly.LargeChange = viewportHeight
        _settingsVerticalOnly.Maximum = Math.Max(0, totalHeight - 1)
        _settingsVerticalOnly.Value = Math.Min(_settingsVerticalOnly.Value, maxOffset)
        _settingsVerticalOnly.SetBounds(Math.Max(0, viewport.Width - _settingsVerticalOnly.Width),
            0, _settingsVerticalOnly.Width, viewport.Height)
        _settingsVerticalOnly.Visible = maxOffset > 0
        settingsSidebarPanel.SetBounds(padding.Left,
            padding.Top - _settingsVerticalOnly.Value, panelWidth, totalHeight)

    End Sub

    Private Sub SettingsVertical_ValueChanged(sender As Object, e As EventArgs)
        settingsSidebarPanel.Top = SettingsSidebarNavigation.Padding.Top - _settingsVerticalOnly.Value
    End Sub

    Private Sub SettingsNavigation_MouseWheel(sender As Object, e As MouseEventArgs)
        If _settingsVerticalOnly Is Nothing OrElse Not _settingsVerticalOnly.Visible OrElse e.Delta = 0 Then Return
        Dim maxOffset As Integer = Math.Max(0,
            _settingsVerticalOnly.Maximum - _settingsVerticalOnly.LargeChange + 1)
        Dim steps As Integer = Math.Max(1, Math.Abs(e.Delta) \ 120)
        Dim delta As Integer = If(e.Delta > 0, -Px(40), Px(40)) * steps
        _settingsVerticalOnly.Value = Math.Max(0, Math.Min(maxOffset, _settingsVerticalOnly.Value + delta))
    End Sub

    Private Sub SettingsNavigation_SizeChanged(sender As Object, e As EventArgs) _
        Handles SplitContainer1.SizeChanged, SettingsSidebarNavigation.SizeChanged

        UpdateResponsiveLayout()

    End Sub

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
            ' Size the nested settings sidebar after the main layout.
            ' ==================================

            UpdateSettingsNavigationLayout()

        Finally

            Me.ResumeLayout(True)

            ' Parent docking has settled after maximize or restore.
            UpdateSettingsNavigationLayout()

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
        ' Recheck after all nested docked containers finish processing Resize.
        If _ready AndAlso IsHandleCreated AndAlso Not _settingsLayoutPending Then
            _settingsLayoutPending = True
            BeginInvoke(New Action(Sub()
                                       _settingsLayoutPending = False
                                       If Not IsDisposed Then UpdateResponsiveLayout()
                                   End Sub))
        End If

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



Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms

' HeaderBar.vb - code-behind only.
' Existing Designer controls:
' panelHeadingLabel, roleHeadingLabel,
' dateTimePlaceholder, DateandTime.

Public Class HeaderBar

    Public Event NotificationClicked()

    Private ReadOnly _timer As New System.Windows.Forms.Timer()

    Private ReadOnly _muted As Color =
        Color.FromArgb(148, 163, 184)

    Private ReadOnly _dark As Color =
        Color.FromArgb(30, 41, 59)

    Private ReadOnly _orange As Color =
        Color.FromArgb(232, 101, 10)

    Private _slash As Label
    Private _clockIcon As PictureBox
    Private _bellIcon As PictureBox
    Private _dot As Panel
    Private _divider As Panel

    Private _dateFont As Font
    Private _parentFont As Font
    Private _pageFont As Font
    Private _slashFont As Font

    Private _ready As Boolean = False
    Private _resizing As Boolean = False
    Private _showFullDate As Boolean = True

    ' ==========================================
    ' RESPONSIVE BREADCRUMB FONT SETTINGS
    ' ==========================================

    Private Const BreadcrumbBaseFontSize As Single = 9.0F

    Private Const BreadcrumbMinimumFontSize As Single = 9.0F

    Private Const BreadcrumbMaximumFontSize As Single = 15.0F

    Private Const BreadcrumbMaximumScale As Double = 1.5R

    Private Const BreadcrumbMinimumScale As Double = 0.85R

    Private Const BreadcrumbReferenceWidth As Integer = 1050

    ' ==========================================
    ' CONSTRUCTOR
    ' ==========================================

    Public Sub New()

        InitializeComponent()

        Me.DoubleBuffered = True
        Me.BackColor = Color.White

        Me.AutoSize = False
        Me.AutoScroll = False

        Me.Dock = DockStyle.Top
        Me.Height = 70

        Me.Padding = New Padding(0)
        Me.Margin = New Padding(0)

        _timer.Interval = 1000

        AddHandler _timer.Tick,
            AddressOf ClockTick

    End Sub

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

    Private Function S(
        value As Integer,
        factor As Double
    ) As Integer

        Return Math.Max(
            1,
            CInt(Px(value) * factor)
        )

    End Function

    ' ==========================================
    ' FONT AVAILABILITY
    ' ==========================================

    Private Function IsFontInstalled(
        familyName As String
    ) As Boolean

        For Each family As FontFamily In FontFamily.Families

            If String.Equals(
                family.Name,
                familyName,
                StringComparison.OrdinalIgnoreCase
            ) Then

                Return True

            End If

        Next

        Return False

    End Function

    ' ==========================================
    ' APPLY RESPONSIVE FONT
    ' ==========================================

    Private Sub SetResponsiveFont(
        ctrl As Control,
        family As String,
        points As Single,
        style As FontStyle,
        ByRef owned As Font
    )

        If owned IsNot Nothing AndAlso
           Math.Abs(owned.SizeInPoints - points) < 0.12F AndAlso
           String.Equals(
               owned.FontFamily.Name,
               family,
               StringComparison.OrdinalIgnoreCase
           ) AndAlso
           owned.Style = style Then

            Return

        End If

        Dim nextFont As New Font(
            family,
            points,
            style,
            GraphicsUnit.Point
        )

        Dim oldFont As Font = owned

        ctrl.Font = nextFont
        owned = nextFont

        If oldFont IsNot Nothing Then
            oldFont.Dispose()
        End If

    End Sub

    ' ==========================================
    ' LOAD
    ' ==========================================

    Protected Overrides Sub OnLoad(
        e As EventArgs
    )

        MyBase.OnLoad(e)

        If _ready OrElse
           Me.DesignMode OrElse
           LicenseManager.UsageMode =
           LicenseUsageMode.Designtime Then

            Return

        End If

        SetupControls()

        _ready = True

        RefreshHeaderLayout()

        _timer.Start()

    End Sub

    ' ==========================================
    ' SETUP DESIGNER CONTROLS
    ' ==========================================

    Private Sub SetupControls()

        Me.SuspendLayout()

        Try

            If panelHeadingLabel.Parent IsNot Me Then
                panelHeadingLabel.Parent = Me
            End If

            If roleHeadingLabel.Parent IsNot Me Then
                roleHeadingLabel.Parent = Me
            End If

            If dateTimePlaceholder.Parent IsNot Me Then
                dateTimePlaceholder.Parent = Me
            End If

            If DateandTime.Parent IsNot dateTimePlaceholder Then
                DateandTime.Parent = dateTimePlaceholder
            End If

            If String.IsNullOrWhiteSpace(
                panelHeadingLabel.Text
            ) Then

                panelHeadingLabel.Text = "Super Admin"

            End If

            If String.IsNullOrWhiteSpace(
                roleHeadingLabel.Text
            ) Then

                roleHeadingLabel.Text = "Settings"

            End If

            For Each lbl As Label In
                New Label() {
                    panelHeadingLabel,
                    roleHeadingLabel
                }

                lbl.Dock = DockStyle.None

                lbl.Anchor =
                    AnchorStyles.Top Or AnchorStyles.Left

                lbl.AutoSize = False
                lbl.AutoEllipsis = False

                lbl.UseCompatibleTextRendering = False

                lbl.BackColor = Color.Transparent

                lbl.Padding = New Padding(0)
                lbl.Margin = New Padding(0)

                lbl.TextAlign =
                    ContentAlignment.MiddleLeft

            Next

            panelHeadingLabel.ForeColor = _muted
            roleHeadingLabel.ForeColor = _dark

            ' ==================================
            ' DATE TIME PLACEHOLDER
            ' ==================================

            With dateTimePlaceholder

                .Dock = DockStyle.None

                .Anchor =
                    AnchorStyles.Top Or AnchorStyles.Left

                .AutoSize = False

                .MinimumSize = Size.Empty
                .MaximumSize = Size.Empty

                .Margin = New Padding(0)
                .Padding = New Padding(0)

                .BackColor =
                    Color.FromArgb(248, 250, 252)

            End With

            ' ==================================
            ' DATE AND TIME TEXT
            ' ==================================

            With DateandTime

                .Dock = DockStyle.None

                .AutoSize = False
                .AutoEllipsis = False

                .UseCompatibleTextRendering = False

                .Margin = New Padding(0)
                .Padding = New Padding(0)

                .BackColor = Color.Transparent

                .ForeColor =
                    Color.FromArgb(100, 116, 139)

                .TextAlign =
                    ContentAlignment.MiddleLeft

            End With

            ' ==================================
            ' BREADCRUMB SEPARATOR
            ' ==================================

            _slash = New Label With {
                .Name = "headerSlash",
                .Text = "/",
                .AutoSize = False,
                .BackColor = Color.Transparent,
                .ForeColor = _muted,
                .TextAlign = ContentAlignment.MiddleCenter
            }

            Me.Controls.Add(_slash)

            ' ==================================
            ' CLOCK ICON
            ' ==================================

            _clockIcon = headerClockIcon

            ' ==================================
            ' NOTIFICATION ICON
            ' ==================================

            _bellIcon = headerNotificationIcon

            AddHandler _bellIcon.Click,
                AddressOf NotificationClick

            ' ==================================
            ' NOTIFICATION DOT
            ' ==================================

            _dot = New Panel With {
                .Name = "headerNotificationDot",
                .BackColor = Color.Transparent,
                .Cursor = Cursors.Hand
            }

            Me.Controls.Add(_dot)

            AddHandler _dot.Paint,
                AddressOf DrawNotificationDot

            AddHandler _dot.Click,
                AddressOf NotificationClick

            ' ==================================
            ' BOTTOM BORDER
            ' ==================================

            _divider = New Panel With {
                .Dock = DockStyle.Bottom,
                .Height = 1,
                .BackColor = Color.FromArgb(226, 232, 240)
            }

            Me.Controls.Add(_divider)

        Finally

            Me.ResumeLayout(True)

        End Try

    End Sub

    ' ==========================================
    ' FIND ICON
    ' ==========================================

    Private Function FindIcon(
        fileName As String
    ) As String

        Dim folder As New DirectoryInfo(
            AppDomain.CurrentDomain.BaseDirectory
        )

        For level As Integer = 0 To 7

            If folder Is Nothing Then Exit For

            For Each subfolder As String In {
                "Icons",
                "icons",
                "Images",
                "Assets",
                "Resources",
                ""
            }

                Dim fullPath As String =
                    Path.Combine(
                        folder.FullName,
                        subfolder,
                        fileName
                    )

                If File.Exists(fullPath) Then
                    Return fullPath
                End If

            Next

            folder = folder.Parent

        Next

        Return String.Empty

    End Function

    ' ==========================================
    ' LOAD PNG
    ' ==========================================

    Private Sub LoadPng(
        target As PictureBox,
        fileName As String
    )

        Dim filePath As String =
            FindIcon(fileName)

        If filePath = String.Empty Then

            target.Visible = False

            Diagnostics.Debug.WriteLine(
                "Missing HeaderBar icon: " & fileName
            )

            Return

        End If

        Try

            Using img As Image =
                Image.FromFile(filePath)

                target.Image = New Bitmap(img)

            End Using

            target.Visible = True

        Catch ex As Exception

            target.Visible = False

            Diagnostics.Debug.WriteLine(
                ex.Message
            )

        End Try

    End Sub

    ' ==========================================
    ' SET BREADCRUMB TEXT
    ' ==========================================

    Public Sub SetParentPageName(
        value As String
    )

        panelHeadingLabel.Text =
            If(value, String.Empty)

        RefreshHeaderLayout()

    End Sub

    Public Sub SetPageName(
        value As String
    )

        roleHeadingLabel.Text =
            If(value, String.Empty)

        RefreshHeaderLayout()

    End Sub

    ' ==========================================
    ' PHILIPPINE TIME
    ' ==========================================

    Private Function PhTime() As DateTimeOffset

        Return DateTimeOffset.UtcNow.ToOffset(
            TimeSpan.FromHours(8)
        )

    End Function

    Private Function ClockText(
        fullDate As Boolean
    ) As String

        Dim formatText As String =
            If(
                fullDate,
                "MMM dd, yyyy  ·  hh:mm tt",
                "hh:mm tt"
            )

        Return PhTime().ToString(
            formatText,
            CultureInfo.GetCultureInfo("en-US")
        )

    End Function

    Private Sub UpdateClock()

        DateandTime.Text =
            ClockText(_showFullDate)

    End Sub

    Private Sub ClockTick(
        sender As Object,
        e As EventArgs
    )

        UpdateClock()

    End Sub

    ' ==========================================
    ' MEASURE TEXT
    ' ==========================================

    Private Function TextWidth(
        value As String,
        f As Font
    ) As Integer

        Return TextRenderer.MeasureText(
            value,
            f,
            New Size(10000, 10000),
            TextFormatFlags.SingleLine Or
            TextFormatFlags.NoPadding Or
            TextFormatFlags.NoPrefix
        ).Width

    End Function

    ' ==========================================
    ' NOTIFICATION
    ' ==========================================

    Private Sub NotificationClick(
        sender As Object,
        e As EventArgs
    )

        RaiseEvent NotificationClicked()

    End Sub

    Private Sub DrawNotificationDot(
        sender As Object,
        e As PaintEventArgs
    )

        e.Graphics.SmoothingMode =
            SmoothingMode.AntiAlias

        Using b As New SolidBrush(_orange)

            e.Graphics.FillEllipse(
                b,
                0,
                0,
                Math.Max(1, _dot.Width - 1),
                Math.Max(1, _dot.Height - 1)
            )

        End Using

    End Sub

    ' ==========================================
    ' HEADER RESIZE
    ' ==========================================

    Protected Overrides Sub OnResize(
        e As EventArgs
    )

        MyBase.OnResize(e)

        RefreshHeaderLayout()

    End Sub

    ' ==========================================
    ' FULL RESPONSIVE HEADER LAYOUT
    ' ==========================================

    Public Sub RefreshHeaderLayout()

        If Not _ready OrElse _resizing Then Return

        Dim w As Integer = Me.ClientSize.Width
        Dim h As Integer = Me.ClientSize.Height

        If w < 1 OrElse h < 1 Then Return

        _resizing = True

        Me.SuspendLayout()

        Try

            ' ==================================
            ' GENERAL SCALE
            ' ==================================

            Dim factor As Double =
                Math.Max(
                    0.74R,
                    Math.Min(
                        1.23R,
                        Math.Min(
                            w / CDbl(Px(1030)),
                            h / CDbl(Px(65))
                        )
                    )
                )

            ' ==================================
            ' DM MONO CLOCK FONT
            ' ==================================

            Dim dateFamily As String =
                If(
                    IsFontInstalled("DM Mono"),
                    "DM Mono",
                    "Consolas"
                )

            SetResponsiveFont(
                DateandTime,
                dateFamily,
                CSng(Math.Max(7.7R, 11.0R * factor)),
                FontStyle.Regular,
                _dateFont
            )

            ' ==================================
            ' RESPONSIVE BREADCRUMB SCALE
            ' ==================================

            Dim breadcrumbFactor As Double =
                Math.Max(
                    BreadcrumbMinimumScale,
                    Math.Min(
                        BreadcrumbMaximumScale,
                        w / CDbl(
                            Px(BreadcrumbReferenceWidth)
                        )
                    )
                )


            ' KEEP FONT BETWEEN 14 AND 15 POINTS

            Dim breadcrumbSize As Single =
                CSng(Math.Max(
                    BreadcrumbMinimumFontSize,
                    Math.Min(
                        BreadcrumbMaximumFontSize,
                        BreadcrumbBaseFontSize * breadcrumbFactor
                    )
                ))


            ' ==================================
            ' INTER REGULAR - SUPER ADMIN
            ' ==================================

            Dim interFamily As String =
                If(
                    IsFontInstalled("Inter"),
                    "Inter",
                    "Segoe UI"
                )

            SetResponsiveFont(
                panelHeadingLabel,
                interFamily,
                breadcrumbSize,
                FontStyle.Regular,
                _parentFont
            )

            ' ==================================
            ' INTER SEMIBOLD - SETTINGS
            ' ==================================

            Dim semiboldFamily As String =
                If(
                    IsFontInstalled("Inter SemiBold"),
                    "Inter SemiBold",
                    interFamily
                )

            Dim pageStyle As FontStyle =
                If(
                    String.Equals(
                        semiboldFamily,
                        "Inter SemiBold",
                        StringComparison.OrdinalIgnoreCase
                    ),
                    FontStyle.Regular,
                    FontStyle.Bold
                )

            SetResponsiveFont(
                roleHeadingLabel,
                semiboldFamily,
                breadcrumbSize,
                pageStyle,
                _pageFont
            )

            ' ==================================
            ' SEPARATOR FONT
            ' ==================================

            SetResponsiveFont(
                _slash,
                interFamily,
                breadcrumbSize,
                FontStyle.Regular,
                _slashFont
            )

            ' ==================================
            ' RESPONSIVE LABEL HEIGHT
            ' ==================================

            Dim middle As Integer = h \ 2

            Dim leftMargin As Integer = Px(18)
            Dim rightMargin As Integer = S(20, factor)

            Dim measuredLabelHeight As Integer =
                Math.Max(
                    TextRenderer.MeasureText(
                        "Ag",
                        panelHeadingLabel.Font
                    ).Height,
                    TextRenderer.MeasureText(
                        "Ag",
                        roleHeadingLabel.Font
                    ).Height
                )

            Dim labelH As Integer =
                Math.Min(
                    h,
                    Math.Max(
                        Px(24),
                        measuredLabelHeight + Px(8)
                    )
                )

            Dim labelY As Integer =
                Math.Max(
                    0,
                    middle - labelH \ 2
                )

            ' ==================================
            ' NOTIFICATION ICON
            ' ==================================

            Dim bellSize As Integer =
                Math.Min(
                    Math.Max(1, h - Px(4)),
                    Math.Max(Px(15), S(22, factor))
                )

            Dim bellX As Integer =
                Math.Max(
                    0,
                    w - rightMargin - bellSize
                )

            Dim bellY As Integer =
                Math.Max(
                    0,
                    middle - bellSize \ 2
                )

            _bellIcon.SetBounds(
                bellX,
                bellY,
                bellSize,
                bellSize
            )

            Dim dotSize As Integer =
                Math.Max(
                    Px(4),
                    S(6, factor)
                )

            _dot.SetBounds(
                bellX + bellSize - dotSize \ 2,
                Math.Max(
                    0,
                    bellY - dotSize \ 3
                ),
                dotSize,
                dotSize
            )

            _dot.Invalidate()

            ' ==================================
            ' DATE TIME CONTAINER
            ' ==================================

            Dim holderH As Integer =
                Math.Min(
                    Math.Max(1, h - Px(10)),
                    Math.Max(Px(30), S(38, factor))
                )

            Dim clockIconSize As Integer =
                Math.Max(
                    1,
                    Math.Min(
                        holderH - Px(8),
                        S(18, factor)
                    )
                )

            Dim iconLeft As Integer = S(13, factor)
            Dim iconGap As Integer = S(10, factor)
            Dim rightPad As Integer = S(8, factor)

            Dim extraTextSafety As Integer =
                S(20, factor)

            Dim boxGap As Integer = S(15, factor)

            Dim textX As Integer =
                iconLeft + clockIconSize + iconGap

            ' ==================================
            ' MEASURE DATE/TIME WIDTH
            ' ==================================

            Dim fullW As Integer =
                Math.Max(
                    S(230, factor),
                    textX +
                    TextWidth(
                        ClockText(True),
                        DateandTime.Font
                    ) +
                    rightPad +
                    extraTextSafety
                )

            Dim shortW As Integer =
                Math.Max(
                    S(90, factor),
                    textX +
                    TextWidth(
                        ClockText(False),
                        DateandTime.Font
                    ) +
                    rightPad +
                    extraTextSafety
                )

            Dim pageNameWidth As Integer =
                TextWidth(
                    roleHeadingLabel.Text,
                    roleHeadingLabel.Font
                ) + Px(18)

            Dim reservedForPage As Integer =
                Math.Max(
                    S(85, factor),
                    pageNameWidth
                )

            Dim maxClockW As Integer =
                Math.Max(
                    0,
                    bellX -
                    boxGap -
                    leftMargin -
                    reservedForPage
                )

            Dim showClock As Boolean = False

            If fullW <= maxClockW Then

                _showFullDate = True
                showClock = True

            ElseIf shortW <= maxClockW Then

                _showFullDate = False
                showClock = True

            Else

                _showFullDate = False

            End If

            ' ==================================
            ' POSITION DATE/TIME PLACEHOLDER
            ' ==================================

            Dim holderW As Integer =
                If(_showFullDate, fullW, shortW)

            Dim holderX As Integer =
                Math.Max(
                    0,
                    bellX - boxGap - holderW
                )

            dateTimePlaceholder.Visible = showClock

            If showClock Then

                dateTimePlaceholder.SetBounds(
                    holderX,
                    Math.Max(
                        0,
                        middle - holderH \ 2
                    ),
                    holderW,
                    holderH
                )

                _clockIcon.SetBounds(
                    iconLeft,
                    (holderH - clockIconSize) \ 2,
                    clockIconSize,
                    clockIconSize
                )

                DateandTime.SetBounds(
                    textX,
                    0,
                    Math.Max(
                        1,
                        holderW - textX - rightPad
                    ),
                    holderH
                )

                DateandTime.Visible = True

            End If

            UpdateClock()

            ' ==================================
            ' RESPONSIVE BREADCRUMB WIDTH
            ' ==================================

            Dim breadcrumbRight As Integer =
                If(
                    showClock,
                    holderX - S(12, factor),
                    bellX - S(12, factor)
                )

            Dim available As Integer =
                Math.Max(
                    0,
                    breadcrumbRight - leftMargin
                )

            ' MEASURE TEXT AFTER APPLYING FONT

            Dim parentW As Integer =
                TextWidth(
                    panelHeadingLabel.Text,
                    panelHeadingLabel.Font
                ) + Px(12)

            Dim pageW As Integer =
                TextWidth(
                    roleHeadingLabel.Text,
                    roleHeadingLabel.Font
                ) + Px(12)

            Dim slashW As Integer =
                Math.Max(
                    Px(12),
                    TextWidth("/", _slash.Font) + Px(4)
                )

            Dim gap As Integer = Px(6)

            Dim needed As Integer =
                parentW +
                slashW +
                2 * gap +
                pageW

            Dim showParent As Boolean =
                available >= needed

            panelHeadingLabel.Visible = showParent
            _slash.Visible = showParent

            roleHeadingLabel.Visible =
                available >= Math.Min(pageW, Px(18))

            Dim pageX As Integer = leftMargin

            ' ==================================
            ' SUPER ADMIN / SEPARATOR
            ' ==================================

            If showParent Then

                panelHeadingLabel.SetBounds(
                    leftMargin,
                    labelY,
                    parentW,
                    labelH
                )

                Dim slashX As Integer =
                    leftMargin + parentW + gap

                _slash.SetBounds(
                    slashX,
                    labelY,
                    slashW,
                    labelH
                )

                pageX =
                    slashX + slashW + gap

            End If

            ' ==================================
            ' SETTINGS LABEL
            ' ==================================

            roleHeadingLabel.SetBounds(
                pageX,
                labelY,
                Math.Max(
                    1,
                    breadcrumbRight - pageX
                ),
                labelH
            )

            ' ==================================
            ' CONTROL LAYER ORDER
            ' ==================================

            panelHeadingLabel.BringToFront()
            _slash.BringToFront()
            roleHeadingLabel.BringToFront()

            dateTimePlaceholder.BringToFront()

            _bellIcon.BringToFront()
            _dot.BringToFront()

            _clockIcon.BringToFront()
            DateandTime.BringToFront()

        Finally

            Me.ResumeLayout(True)

            _resizing = False

        End Try

    End Sub

    ' ==========================================
    ' CLEANUP
    ' ==========================================

    Private Sub HeaderBar_Disposed(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Disposed

        _timer.Stop()
        _timer.Dispose()

        If _dateFont IsNot Nothing Then
            _dateFont.Dispose()
        End If

        If _parentFont IsNot Nothing Then
            _parentFont.Dispose()
        End If

        If _pageFont IsNot Nothing Then
            _pageFont.Dispose()
        End If

        If _slashFont IsNot Nothing Then
            _slashFont.Dispose()
        End If

        If _clockIcon IsNot Nothing AndAlso
           _clockIcon.Image IsNot Nothing Then

            _clockIcon.Image.Dispose()
            _clockIcon.Image = Nothing

        End If

        If _bellIcon IsNot Nothing AndAlso
           _bellIcon.Image IsNot Nothing Then

            _bellIcon.Image.Dispose()
            _bellIcon.Image = Nothing

        End If

    End Sub

End Class

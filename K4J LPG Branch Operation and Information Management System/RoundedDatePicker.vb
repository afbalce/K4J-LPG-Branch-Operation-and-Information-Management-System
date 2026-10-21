
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Globalization
Imports System.Windows.Forms

Public Class RoundedDatePicker
    Inherits UserControl

    ' =====================================
    ' APPEARANCE SETTINGS
    ' =====================================

    Private Const MinimumControlHeight As Integer = 28

    Private _cornerRadius As Integer = 12
    Private _borderThickness As Integer = 2
    Private _borderOpacity As Integer = 100

    Private _borderColor As Color =
        Color.FromArgb(223, 149, 63)

    Private _fillColor As Color = Color.White

    Private _textColor As Color =
        Color.FromArgb(50, 50, 50)

    Private _iconColor As Color =
        Color.FromArgb(80, 80, 80)

    Private _textPadding As Integer = 12

    ' =====================================
    ' FONT SETTINGS
    ' =====================================

    Private _dateFont As Font =
        New Font("Segoe UI", 9.0F, FontStyle.Regular)

    Private _hintFont As Font =
        New Font("Segoe UI", 9.0F, FontStyle.Regular)

    ' =====================================
    ' HINT SETTINGS
    ' =====================================

    Private _hintText As String = "mm/dd/yyyy"

    Private _hintColor As Color =
        Color.FromArgb(150, 150, 150)

    Private _hasSelectedDate As Boolean = False

    ' =====================================
    ' DATE SETTINGS
    ' =====================================

    Private _selectedDate As DateTime = Date.Today
    Private _dateFormat As String = "MM/dd/yyyy"

    Private Shared ReadOnly EarliestDate As New DateTime(1753, 1, 1)
    Private Shared ReadOnly LatestDate As New DateTime(9998, 12, 31)

    Private _minDate As DateTime = EarliestDate
    Private _maxDate As DateTime = LatestDate

    ' =====================================
    ' CALENDAR COLORS
    ' =====================================

    Private _calendarBackColor As Color = Color.White

    Private _calendarTitleBackColor As Color =
        Color.FromArgb(30, 48, 84)

    Private _calendarTitleForeColor As Color = Color.White

    Private _calendarTrailingForeColor As Color = Color.Gray

    ' =====================================
    ' POPUP CONTROLS
    ' =====================================

    Private _popup As ToolStripDropDown = Nothing
    Private _calendar As MonthCalendar = Nothing

    ' =====================================
    ' CONSTRUCTOR
    ' =====================================

    Public Sub New()

        MyBase.New()

        Me.SetStyle(
            ControlStyles.UserPaint Or
            ControlStyles.AllPaintingInWmPaint Or
            ControlStyles.OptimizedDoubleBuffer Or
            ControlStyles.ResizeRedraw Or
            ControlStyles.SupportsTransparentBackColor,
            True
        )

        Me.DoubleBuffered = True
        Me.BackColor = Color.Transparent

        Me.MinimumSize = New Size(115, MinimumControlHeight)
        Me.Size = New Size(200, 28)

        Me.Font = New Font(
            "Segoe UI",
            9.0F,
            FontStyle.Regular
        )

        Me.Cursor = Cursors.Hand
        Me.TabStop = True

        AddHandler Me.Disposed,
            AddressOf RoundedDatePicker_Disposed

    End Sub

    ' =====================================
    ' ROUNDED APPEARANCE PROPERTIES
    ' =====================================

    <Category("Rounded Appearance")>
    <DefaultValue(12)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property CornerRadius As Integer

        Get
            Return _cornerRadius
        End Get

        Set(value As Integer)
            _cornerRadius = Math.Max(0, value)
            Me.Invalidate()
        End Set

    End Property

    <Category("Rounded Appearance")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BorderColor As Color

        Get
            Return _borderColor
        End Get

        Set(value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set

    End Property

    <Category("Rounded Appearance")>
    <DefaultValue(2)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BorderThickness As Integer

        Get
            Return _borderThickness
        End Get

        Set(value As Integer)
            _borderThickness = Math.Max(0, Math.Min(20, value))
            Me.Invalidate()
        End Set

    End Property

    <Category("Rounded Appearance")>
    <DefaultValue(100)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BorderOpacity As Integer

        Get
            Return _borderOpacity
        End Get

        Set(value As Integer)
            _borderOpacity = Math.Max(0, Math.Min(100, value))
            Me.Invalidate()
        End Set

    End Property

    <Category("Rounded Appearance")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property FillColor As Color

        Get
            Return _fillColor
        End Get

        Set(value As Color)
            _fillColor = value
            Me.Invalidate()
        End Set

    End Property

    <Category("Rounded Appearance")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property TextColor As Color

        Get
            Return _textColor
        End Get

        Set(value As Color)
            _textColor = value
            Me.Invalidate()
        End Set

    End Property

    <Category("Rounded Appearance")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property IconColor As Color

        Get
            Return _iconColor
        End Get

        Set(value As Color)
            _iconColor = value
            Me.Invalidate()
        End Set

    End Property

    <Category("Rounded Appearance")>
    <DefaultValue(12)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property TextPadding As Integer

        Get
            Return _textPadding
        End Get

        Set(value As Integer)
            _textPadding = Math.Max(0, value)
            Me.Invalidate()
        End Set

    End Property

    ' =====================================
    ' DATE FONT PICKER
    ' =====================================

    <Category("Font Appearance")>
    <Description("Choose the date font family, size, and style.")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property DateFont As Font

        Get
            Return _dateFont
        End Get

        Set(value As Font)

            If value Is Nothing Then Return

            _dateFont = value
            Me.Invalidate()

        End Set

    End Property

    ' =====================================
    ' HINT FONT PICKER
    ' =====================================

    <Category("Font Appearance")>
    <Description("Choose the hint font family, size, and style.")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property HintFont As Font

        Get
            Return _hintFont
        End Get

        Set(value As Font)

            If value Is Nothing Then Return

            _hintFont = value
            Me.Invalidate()

        End Set

    End Property

    ' =====================================
    ' HINT TEXT
    ' =====================================

    <Category("Hint Appearance")>
    <Description("Text shown before choosing a date.")>
    <DefaultValue("mm/dd/yyyy")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property HintText As String

        Get
            Return _hintText
        End Get

        Set(value As String)

            _hintText = If(value, "")
            Me.Invalidate()

        End Set

    End Property

    ' =====================================
    ' HINT COLOR
    ' =====================================

    <Category("Hint Appearance")>
    <Description("Color of the placeholder text.")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property HintColor As Color

        Get
            Return _hintColor
        End Get

        Set(value As Color)

            _hintColor = value
            Me.Invalidate()

        End Set

    End Property

    ' =====================================
    ' HAS SELECTED DATE
    ' =====================================

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public ReadOnly Property HasSelectedDate As Boolean

        Get
            Return _hasSelectedDate
        End Get

    End Property

    ' =====================================
    ' SELECTED DATE
    ' =====================================

    <Category("Date Settings")>
    <Description("Gets or sets the selected date.")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Value As DateTime

        Get
            Return _selectedDate
        End Get

        Set(value As DateTime)

            Dim newDate As DateTime = ClampDate(value.Date)

            Dim changed As Boolean =
                Not _hasSelectedDate OrElse
                _selectedDate.Date <> newDate

            _selectedDate = newDate
            _hasSelectedDate = True

            If _calendar IsNot Nothing AndAlso
               Not _calendar.IsDisposed Then

                _calendar.SetDate(_selectedDate)

            End If

            Me.Invalidate()

            If changed Then
                RaiseEvent ValueChanged(Me, EventArgs.Empty)
            End If

        End Set

    End Property

    ' =====================================
    ' CLEAR DATE
    ' =====================================

    Public Sub ClearDate()

        If Not _hasSelectedDate Then Return

        _hasSelectedDate = False
        _selectedDate = ClampDate(Date.Today)

        Me.Invalidate()

        RaiseEvent ValueChanged(
            Me,
            EventArgs.Empty
        )

    End Sub

    ' =====================================
    ' DESIGNER VALUE SERIALIZATION
    ' =====================================

    Public Function ShouldSerializeValue() As Boolean

        Return _hasSelectedDate

    End Function

    Public Sub ResetValue()

        ClearDate()

    End Sub

    ' =====================================
    ' DATE FORMAT
    ' =====================================

    <Category("Date Settings")>
    <DefaultValue("MM/dd/yyyy")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property DateFormat As String

        Get
            Return _dateFormat
        End Get

        Set(value As String)

            If String.IsNullOrWhiteSpace(value) Then
                _dateFormat = "MM/dd/yyyy"
            Else
                _dateFormat = value
            End If

            Me.Invalidate()

        End Set

    End Property

    ' =====================================
    ' MINIMUM DATE
    ' =====================================

    <Category("Date Settings")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property MinDate As DateTime

        Get
            Return _minDate
        End Get

        Set(value As DateTime)

            _minDate = LimitCalendarDate(value)

            If _maxDate < _minDate Then
                _maxDate = _minDate
            End If

            CloseCalendar()
            RevalidateSelectedDate()

        End Set

    End Property

    ' =====================================
    ' MAXIMUM DATE
    ' =====================================

    <Category("Date Settings")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property MaxDate As DateTime

        Get
            Return _maxDate
        End Get

        Set(value As DateTime)

            _maxDate = LimitCalendarDate(value)

            If _minDate > _maxDate Then
                _minDate = _maxDate
            End If

            CloseCalendar()
            RevalidateSelectedDate()

        End Set

    End Property

    ' =====================================
    ' CALENDAR APPEARANCE
    ' =====================================

    <Category("Calendar Appearance")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property CalendarBackColor As Color

        Get
            Return _calendarBackColor
        End Get

        Set(value As Color)

            _calendarBackColor = value

            If _calendar IsNot Nothing AndAlso
               Not _calendar.IsDisposed Then

                _calendar.BackColor = value

            End If

        End Set

    End Property

    <Category("Calendar Appearance")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property CalendarTitleBackColor As Color

        Get
            Return _calendarTitleBackColor
        End Get

        Set(value As Color)

            _calendarTitleBackColor = value

            If _calendar IsNot Nothing AndAlso
               Not _calendar.IsDisposed Then

                _calendar.TitleBackColor = value

            End If

        End Set

    End Property

    <Category("Calendar Appearance")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property CalendarTitleForeColor As Color

        Get
            Return _calendarTitleForeColor
        End Get

        Set(value As Color)

            _calendarTitleForeColor = value

            If _calendar IsNot Nothing AndAlso
               Not _calendar.IsDisposed Then

                _calendar.TitleForeColor = value

            End If

        End Set

    End Property

    <Category("Calendar Appearance")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property CalendarTrailingForeColor As Color

        Get
            Return _calendarTrailingForeColor
        End Get

        Set(value As Color)

            _calendarTrailingForeColor = value

            If _calendar IsNot Nothing AndAlso
               Not _calendar.IsDisposed Then

                _calendar.TrailingForeColor = value

            End If

        End Set

    End Property

    ' =====================================
    ' EVENTS
    ' =====================================

    Public Event ValueChanged As EventHandler

    ' =====================================
    ' DATE RANGE HELPERS
    ' =====================================

    Private Shared Function LimitCalendarDate(
        dateValue As DateTime
    ) As DateTime

        Dim result As DateTime = dateValue.Date

        If result < EarliestDate Then Return EarliestDate
        If result > LatestDate Then Return LatestDate

        Return result

    End Function

    Private Function ClampDate(
        dateValue As DateTime
    ) As DateTime

        Dim result As DateTime = LimitCalendarDate(dateValue)

        If result < _minDate Then Return _minDate
        If result > _maxDate Then Return _maxDate

        Return result

    End Function

    Private Sub RevalidateSelectedDate()

        If _hasSelectedDate Then

            Me.Value = _selectedDate

        Else

            _selectedDate = ClampDate(_selectedDate)
            Me.Invalidate()

        End If

    End Sub

    ' =====================================
    ' CREATE ROUNDED PATH
    ' =====================================

    Private Function CreateRoundedPath(
        rect As RectangleF,
        radius As Single
    ) As GraphicsPath

        Dim path As New GraphicsPath()

        If rect.Width <= 0 OrElse rect.Height <= 0 Then
            Return path
        End If

        radius = Math.Max(
            0.0F,
            Math.Min(
                radius,
                Math.Min(rect.Width, rect.Height) / 2.0F
            )
        )

        If radius <= 0 Then

            path.AddRectangle(rect)
            Return path

        End If

        Dim diameter As Single = radius * 2.0F

        path.AddArc(
            rect.X, rect.Y,
            diameter, diameter,
            180, 90
        )

        path.AddArc(
            rect.Right - diameter, rect.Y,
            diameter, diameter,
            270, 90
        )

        path.AddArc(
            rect.Right - diameter,
            rect.Bottom - diameter,
            diameter, diameter,
            0, 90
        )

        path.AddArc(
            rect.X,
            rect.Bottom - diameter,
            diameter, diameter,
            90, 90
        )

        path.CloseFigure()
        Return path

    End Function

    ' =====================================
    ' PAINT CONTROL
    ' =====================================

    Protected Overrides Sub OnPaint(
        e As PaintEventArgs
    )

        MyBase.OnPaint(e)

        Dim w As Integer = Me.ClientSize.Width
        Dim h As Integer = Me.ClientSize.Height

        If w <= 2 OrElse h <= 2 Then Return

        Dim g As Graphics = e.Graphics

        g.SmoothingMode = SmoothingMode.AntiAlias

        ' =================================
        ' FILL BACKGROUND
        ' =================================

        Dim fillRect As New RectangleF(
            0, 0,
            CSng(w - 1),
            CSng(h - 1)
        )

        Using fillPath As GraphicsPath =
            CreateRoundedPath(
                fillRect,
                CSng(_cornerRadius)
            )

            Using brush As New SolidBrush(_fillColor)
                g.FillPath(brush, fillPath)
            End Using

        End Using

        ' =================================
        ' DRAW BORDER
        ' =================================

        If _borderThickness > 0 AndAlso
           _borderOpacity > 0 Then

            Dim thickness As Single =
                CSng(Math.Min(
                    _borderThickness,
                    Math.Min(w, h)
                ))

            Dim inset As Single =
                thickness / 2.0F + 0.5F

            Dim borderRect As New RectangleF(
                inset,
                inset,
                Math.Max(
                    1.0F,
                    CSng(w) - inset * 2.0F - 1.0F
                ),
                Math.Max(
                    1.0F,
                    CSng(h) - inset * 2.0F - 1.0F
                )
            )

            Dim alpha As Integer =
                CInt(Math.Round(
                    _borderColor.A *
                    (_borderOpacity / 100.0R)
                ))

            Dim actualBorderColor As Color =
                Color.FromArgb(
                    alpha,
                    _borderColor.R,
                    _borderColor.G,
                    _borderColor.B
                )

            Using borderPath As GraphicsPath =
                CreateRoundedPath(
                    borderRect,
                    Math.Max(
                        0.0F,
                        CSng(_cornerRadius) - inset
                    )
                )

                Using pen As New Pen(
                    actualBorderColor,
                    thickness
                )

                    g.DrawPath(pen, borderPath)

                End Using

            End Using

        End If

        ' =================================
        ' DETERMINE HINT OR DATE
        ' =================================

        Dim displayText As String
        Dim displayFont As Font
        Dim displayColor As Color

        If _hasSelectedDate Then

            Try

                displayText = _selectedDate.ToString(
                    _dateFormat,
                    CultureInfo.CurrentCulture
                )

            Catch ex As FormatException

                displayText =
                    _selectedDate.ToString("MM/dd/yyyy")

            End Try

            displayFont = _dateFont
            displayColor = _textColor

        Else

            displayText = _hintText
            displayFont = _hintFont
            displayColor = _hintColor

        End If

        If Not Me.Enabled Then
            displayColor = SystemColors.GrayText
        End If

        ' =================================
        ' DRAW TEXT
        ' =================================

        Dim leftPadding As Integer =
            Math.Max(
                _textPadding,
                _borderThickness + 5
            )

        Dim iconSpace As Integer = 38

        Dim textRect As New Rectangle(
            leftPadding,
            2,
            Math.Max(1, w - leftPadding - iconSpace),
            Math.Max(1, h - 4)
        )

        Dim flags As TextFormatFlags =
            TextFormatFlags.Left Or
            TextFormatFlags.VerticalCenter Or
            TextFormatFlags.SingleLine Or
            TextFormatFlags.EndEllipsis Or
            TextFormatFlags.NoPadding

        TextRenderer.DrawText(
            g,
            displayText,
            displayFont,
            textRect,
            displayColor,
            flags
        )

        ' =================================
        ' CALENDAR ICON
        ' =================================

        DrawCalendarIcon(g, w, h)

    End Sub

    ' =====================================
    ' DRAW CALENDAR ICON
    ' =====================================

    Private Sub DrawCalendarIcon(
        g As Graphics,
        w As Integer,
        h As Integer
    )

        Dim iconSize As Integer =
            Math.Min(16, Math.Max(10, h - 10))

        Dim x As Integer = w - iconSize - 12
        Dim y As Integer = (h - iconSize) \ 2

        Dim drawColor As Color =
            If(Me.Enabled, _iconColor, SystemColors.GrayText)

        Using pen As New Pen(drawColor, 1.4F)

            pen.StartCap = LineCap.Round
            pen.EndCap = LineCap.Round

            ' Outer box
            g.DrawRectangle(
                pen,
                x,
                y + 2,
                iconSize - 1,
                iconSize - 3
            )

            ' Header line
            g.DrawLine(
                pen,
                x,
                y + 6,
                x + iconSize - 1,
                y + 6
            )

            ' Left ring
            g.DrawLine(
                pen,
                x + 4, y,
                x + 4, y + 4
            )

            ' Right ring
            g.DrawLine(
                pen,
                x + iconSize - 5, y,
                x + iconSize - 5, y + 4
            )

        End Using

        Using brush As New SolidBrush(drawColor)

            For row As Integer = 0 To 1
                For col As Integer = 0 To 2

                    g.FillRectangle(
                        brush,
                        x + 3 + col * 4,
                        y + 8 + row * 4,
                        2, 2
                    )

                Next
            Next

        End Using

    End Sub

    ' =====================================
    ' SHOW CALENDAR
    ' =====================================

    Private Sub ShowCalendar()

        If LicenseManager.UsageMode =
           LicenseUsageMode.Designtime Then Return

        If Not Me.Enabled Then Return

        If _popup IsNot Nothing Then

            If _popup.Visible Then
                _popup.Close()
                Return
            End If

        End If

        Dim newCalendar As New MonthCalendar()

        With newCalendar

            .MaxSelectionCount = 1
            .ShowToday = True
            .ShowTodayCircle = True

            .BackColor = _calendarBackColor
            .TitleBackColor = _calendarTitleBackColor
            .TitleForeColor = _calendarTitleForeColor
            .TrailingForeColor = _calendarTrailingForeColor

            .MinDate = _minDate
            .MaxDate = _maxDate

            .SetDate(ClampDate(_selectedDate))
            .Margin = Padding.Empty

        End With

        _calendar = newCalendar

        AddHandler _calendar.DateSelected,
            AddressOf Calendar_DateSelected

        Dim host As New ToolStripControlHost(_calendar)

        host.Margin = Padding.Empty
        host.Padding = Padding.Empty
        host.AutoSize = True

        Dim newPopup As New ToolStripDropDown()

        With newPopup

            .Padding = Padding.Empty
            .Margin = Padding.Empty
            .AutoSize = True

            .BackColor = _calendarBackColor
            .DropShadowEnabled = True
            .AutoClose = True

            .Items.Add(host)

        End With

        _popup = newPopup

        AddHandler _popup.Closed,
            AddressOf Popup_Closed

        ' POSITION BELOW THE CONTROL
        Dim popupPoint As Point =
            Me.PointToScreen(
                New Point(0, Me.Height)
            )

        Dim screenBounds As Rectangle =
            Screen.FromControl(Me).WorkingArea

        Dim popupWidth As Integer = _calendar.Width
        Dim popupHeight As Integer = _calendar.Height

        If popupPoint.Y + popupHeight >
           screenBounds.Bottom Then

            popupPoint.Y =
                Me.PointToScreen(Point.Empty).Y -
                popupHeight

        End If

        popupPoint.X = Math.Max(
            screenBounds.Left,
            Math.Min(
                popupPoint.X,
                screenBounds.Right - popupWidth
            )
        )

        popupPoint.Y = Math.Max(
            screenBounds.Top,
            popupPoint.Y
        )

        _popup.Show(
            popupPoint.X,
            popupPoint.Y
        )

    End Sub

    ' =====================================
    ' DATE SELECTED
    ' =====================================

    Private Sub Calendar_DateSelected(
        sender As Object,
        e As DateRangeEventArgs
    )

        Me.Value = e.Start.Date
        CloseCalendar()

    End Sub

    ' =====================================
    ' CLOSE CALENDAR
    ' =====================================

    Private Sub CloseCalendar()

        If _popup IsNot Nothing AndAlso
           Not _popup.IsDisposed Then

            _popup.Close()

        End If

    End Sub

    ' =====================================
    ' POPUP CLOSED
    ' =====================================

    Private Sub Popup_Closed(
        sender As Object,
        e As ToolStripDropDownClosedEventArgs
    )

        Dim closedPopup As ToolStripDropDown =
            TryCast(sender, ToolStripDropDown)

        If closedPopup Is Nothing Then Return

        RemoveHandler closedPopup.Closed,
            AddressOf Popup_Closed

        If _calendar IsNot Nothing Then

            RemoveHandler _calendar.DateSelected,
                AddressOf Calendar_DateSelected

        End If

        If Object.ReferenceEquals(closedPopup, _popup) Then
            _popup = Nothing
            _calendar = Nothing
        End If

        closedPopup.Dispose()
        Me.Invalidate()

    End Sub

    ' =====================================
    ' MOUSE CLICK
    ' =====================================

    Protected Overrides Sub OnMouseDown(
        e As MouseEventArgs
    )

        MyBase.OnMouseDown(e)

        If e.Button = MouseButtons.Left Then

            Me.Focus()
            ShowCalendar()

        End If

    End Sub

    ' =====================================
    ' KEYBOARD SUPPORT
    ' =====================================

    Protected Overrides Sub OnKeyDown(
        e As KeyEventArgs
    )

        MyBase.OnKeyDown(e)

        If e.KeyCode = Keys.Enter OrElse
           e.KeyCode = Keys.Space OrElse
           (e.Alt AndAlso e.KeyCode = Keys.Down) Then

            ShowCalendar()

            e.Handled = True
            e.SuppressKeyPress = True

        ElseIf e.KeyCode = Keys.Escape Then

            CloseCalendar()
            e.Handled = True

        End If

    End Sub

    ' =====================================
    ' FONT CHANGED
    ' =====================================

    Protected Overrides Sub OnFontChanged(
        e As EventArgs
    )

        MyBase.OnFontChanged(e)
        Me.Invalidate()

    End Sub

    ' =====================================
    ' ENABLED CHANGED
    ' =====================================

    Protected Overrides Sub OnEnabledChanged(
        e As EventArgs
    )

        MyBase.OnEnabledChanged(e)

        If Not Me.Enabled Then
            CloseCalendar()
        End If

        Me.Invalidate()

    End Sub

    ' =====================================
    ' DISPOSE POPUP
    ' =====================================

    Private Sub RoundedDatePicker_Disposed(
        sender As Object,
        e As EventArgs
    )

        If _popup IsNot Nothing Then

            Dim popupToDispose As ToolStripDropDown =
                _popup

            _popup = Nothing
            _calendar = Nothing

            RemoveHandler popupToDispose.Closed,
                AddressOf Popup_Closed

            popupToDispose.Dispose()

        End If

    End Sub

End Class

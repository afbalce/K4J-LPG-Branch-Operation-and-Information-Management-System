
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class RoundedComboBox
    Inherits UserControl

    Private ReadOnly cmbInput As New ComboBox()
    Private ReadOnly lblDisplay As New Label()
    Private ReadOnly pnlArrow As New Panel()

    ' =========================
    ' DEFAULT DESIGN SETTINGS
    ' =========================

    Private _cornerRadius As Integer = 15

    Private _borderColor As Color =
        Color.FromArgb(223, 149, 63)

    Private _borderThickness As Integer = 2
    Private _fillColor As Color = Color.White

    Private _textColor As Color =
        Color.FromArgb(50, 50, 50)

    Private _textPadding As Integer = 12

    Private _arrowColor As Color =
        Color.FromArgb(80, 80, 80)

    ' =========================
    ' HINT TEXT SETTINGS
    ' =========================

    Private _hintText As String = "Select an option..."
    Private _hintColor As Color = Color.Gray

    Private _hintFont As Font =
        New Font("Segoe UI", 11, FontStyle.Regular)

    Private _updatingLayout As Boolean = False

    ' =========================
    ' CONSTRUCTOR
    ' =========================

    Public Sub New()

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
        Me.Size = New Size(250, 30)
        Me.MinimumSize = New Size(90, 30)
        Me.TabStop = False

        Me.Font = New Font(
            "Segoe UI",
            11,
            FontStyle.Regular
        )

        ' INTERNAL COMBOBOX
        With cmbInput
            .FlatStyle = FlatStyle.Flat
            .DropDownStyle = ComboBoxStyle.DropDownList
            .DrawMode = DrawMode.Normal
            .BackColor = _fillColor
            .ForeColor = _textColor
            .Font = Me.Font
            .IntegralHeight = False
            .TabStop = True
        End With

        ' VISIBLE HINT AND SELECTED TEXT
        With lblDisplay
            .AutoSize = False
            .Text = _hintText
            .Font = _hintFont
            .ForeColor = _hintColor
            .BackColor = _fillColor
            .TextAlign = ContentAlignment.MiddleLeft
            .AutoEllipsis = True
            .Cursor = Cursors.Hand
            .TabStop = False
        End With

        ' CUSTOM ARROW
        With pnlArrow
            .BackColor = _fillColor
            .Cursor = Cursors.Hand
            .TabStop = False
        End With

        Me.Controls.Add(cmbInput)
        Me.Controls.Add(lblDisplay)
        Me.Controls.Add(pnlArrow)

        lblDisplay.BringToFront()
        pnlArrow.BringToFront()

        ' EVENTS
        AddHandler cmbInput.SelectedIndexChanged,
            AddressOf Combo_SelectedIndexChanged

        AddHandler lblDisplay.MouseDown,
            AddressOf OpenDropdown

        AddHandler pnlArrow.MouseDown,
            AddressOf OpenDropdown

        AddHandler pnlArrow.Paint,
            AddressOf Arrow_Paint

        UpdateComboLayout()
        UpdateDisplay()

    End Sub

    ' =========================
    ' CORNER RADIUS
    ' =========================

    <Category("Rounded Appearance")>
    <Description("Adjusts the corner radius.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property CornerRadius As Integer

        Get
            Return _cornerRadius
        End Get

        Set(value As Integer)
            _cornerRadius = Math.Max(0, value)
            UpdateComboLayout()
            Me.Invalidate()
        End Set

    End Property

    ' =========================
    ' BORDER COLOR
    ' =========================

    <Category("Rounded Appearance")>
    <Description("Changes the border color.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property BorderColor As Color

        Get
            Return _borderColor
        End Get

        Set(value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set

    End Property

    ' =========================
    ' BORDER THICKNESS
    ' =========================

    <Category("Rounded Appearance")>
    <Description("Adjusts the border thickness.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property BorderThickness As Integer

        Get
            Return _borderThickness
        End Get

        Set(value As Integer)
            _borderThickness = Math.Max(0, value)
            UpdateComboLayout()
            Me.Invalidate()
        End Set

    End Property

    ' =========================
    ' FILL COLOR
    ' =========================

    <Category("Rounded Appearance")>
    <Description("Changes the background color.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property FillColor As Color

        Get
            Return _fillColor
        End Get

        Set(value As Color)

            _fillColor = value

            cmbInput.BackColor = value
            lblDisplay.BackColor = value
            pnlArrow.BackColor = value

            Me.Invalidate()

        End Set

    End Property

    ' =========================
    ' TEXT COLOR
    ' =========================

    <Category("Rounded Appearance")>
    <Description("Changes selected text color.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property TextColor As Color

        Get
            Return _textColor
        End Get

        Set(value As Color)

            _textColor = value
            cmbInput.ForeColor = value

            UpdateDisplay()
            Me.Invalidate()

        End Set

    End Property

    ' =========================
    ' TEXT PADDING
    ' =========================

    <Category("Rounded Appearance")>
    <Description("Adjusts the horizontal text padding.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property TextPadding As Integer

        Get
            Return _textPadding
        End Get

        Set(value As Integer)

            _textPadding = Math.Max(0, value)
            UpdateComboLayout()

        End Set

    End Property

    ' =========================
    ' ARROW COLOR
    ' =========================

    <Category("Rounded Appearance")>
    <Description("Changes dropdown arrow color.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property ArrowColor As Color

        Get
            Return _arrowColor
        End Get

        Set(value As Color)

            _arrowColor = value
            pnlArrow.Invalidate()

        End Set

    End Property

    ' =========================
    ' HINT TEXT
    ' =========================

    <Category("Hint Appearance")>
    <Description("Placeholder when no item is selected.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property HintText As String

        Get
            Return _hintText
        End Get

        Set(value As String)

            _hintText = If(value, "")
            UpdateDisplay()

        End Set

    End Property

    ' =========================
    ' HINT COLOR
    ' =========================

    <Category("Hint Appearance")>
    <Description("Changes placeholder color.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property HintColor As Color

        Get
            Return _hintColor
        End Get

        Set(value As Color)

            _hintColor = value
            UpdateDisplay()

        End Set

    End Property

    ' =========================
    ' HINT FONT
    ' =========================

    <Category("Hint Appearance")>
    <Description("Changes placeholder font and style.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property HintFont As Font

        Get
            Return _hintFont
        End Get

        Set(value As Font)

            If value Is Nothing Then Return

            _hintFont = value

            UpdateDisplay()
            UpdateComboLayout()

        End Set

    End Property

    ' =========================
    ' HINT FONT SIZE
    ' =========================

    <Category("Hint Appearance")>
    <Description("Adjusts placeholder font size.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property HintFontSize As Single

        Get
            Return _hintFont.Size
        End Get

        Set(value As Single)

            If Single.IsNaN(value) OrElse
               Single.IsInfinity(value) OrElse
               value <= 0 Then Return

            _hintFont = New Font(
                _hintFont.FontFamily,
                value,
                _hintFont.Style,
                _hintFont.Unit
            )

            UpdateDisplay()
            UpdateComboLayout()

        End Set

    End Property

    ' =========================
    ' COMBOBOX ITEMS
    ' =========================

    <Category("Data")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Content)>
    Public ReadOnly Property Items As ComboBox.ObjectCollection

        Get
            Return cmbInput.Items
        End Get

    End Property

    ' =========================
    ' SELECTED INDEX
    ' =========================

    <Category("Data")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    <DefaultValue(-1)>
    Public Property SelectedIndex As Integer

        Get
            Return cmbInput.SelectedIndex
        End Get

        Set(value As Integer)
            cmbInput.SelectedIndex = value
        End Set

    End Property

    ' =========================
    ' SELECTED ITEM
    ' =========================

    <Browsable(False)>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Hidden)>
    Public Property SelectedItem As Object

        Get
            Return cmbInput.SelectedItem
        End Get

        Set(value As Object)
            cmbInput.SelectedItem = value
        End Set

    End Property

    ' =========================
    ' DROPDOWN HEIGHT
    ' =========================

    <Category("Behavior")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property DropDownHeight As Integer

        Get
            Return cmbInput.DropDownHeight
        End Get

        Set(value As Integer)

            cmbInput.DropDownHeight =
                Math.Max(1, value)

        End Set

    End Property

    ' =========================
    ' SELECTED INDEX EVENT
    ' =========================

    Public Event SelectedIndexChanged As EventHandler

    Private Sub Combo_SelectedIndexChanged(
        sender As Object,
        e As EventArgs
    )

        UpdateDisplay()

        RaiseEvent SelectedIndexChanged(
            Me,
            EventArgs.Empty
        )

    End Sub

    ' =========================
    ' UPDATE DISPLAY TEXT
    ' =========================

    Private Sub UpdateDisplay()

        If cmbInput Is Nothing OrElse
           lblDisplay Is Nothing Then Return

        If cmbInput.SelectedIndex = -1 Then

            lblDisplay.Text = _hintText
            lblDisplay.Font = _hintFont
            lblDisplay.ForeColor = _hintColor

        Else

            lblDisplay.Text =
                cmbInput.GetItemText(
                    cmbInput.SelectedItem
                )

            lblDisplay.Font = Me.Font
            lblDisplay.ForeColor = _textColor

        End If

        lblDisplay.Invalidate()

    End Sub

    ' =========================
    ' OPEN DROPDOWN
    ' =========================

    Private Sub OpenDropdown(
        sender As Object,
        e As MouseEventArgs
    )

        If e.Button <> MouseButtons.Left Then Return
        If Not Me.Enabled Then Return

        cmbInput.Focus()

        cmbInput.DroppedDown =
            Not cmbInput.DroppedDown

    End Sub

    ' =========================
    ' DRAW CUSTOM ARROW
    ' =========================

    Private Sub Arrow_Paint(
        sender As Object,
        e As PaintEventArgs
    )

        e.Graphics.SmoothingMode =
            SmoothingMode.AntiAlias

        Dim centerX As Integer =
            pnlArrow.Width \ 2

        Dim centerY As Integer =
            pnlArrow.Height \ 2

        Dim points As Point() = {
            New Point(centerX - 5, centerY - 2),
            New Point(centerX, centerY + 3),
            New Point(centerX + 5, centerY - 2)
        }

        Using pen As New Pen(
            _arrowColor,
            2.0F
        )

            pen.StartCap = LineCap.Round
            pen.EndCap = LineCap.Round
            pen.LineJoin = LineJoin.Round

            e.Graphics.DrawLines(
                pen,
                points
            )

        End Using

    End Sub

    ' =========================
    ' ROUNDED RECTANGLE PATH
    ' =========================

    Private Function CreateRoundedPath(
        rect As RectangleF,
        radius As Single
    ) As GraphicsPath

        Dim path As New GraphicsPath()

        radius = Math.Max(
            0.0F,
            Math.Min(
                radius,
                Math.Min(
                    rect.Width,
                    rect.Height
                ) / 2.0F
            )
        )

        If radius <= 0 Then

            path.AddRectangle(rect)
            Return path

        End If

        Dim diameter As Single = radius * 2

        path.AddArc(
            rect.X,
            rect.Y,
            diameter,
            diameter,
            180,
            90
        )

        path.AddArc(
            rect.Right - diameter,
            rect.Y,
            diameter,
            diameter,
            270,
            90
        )

        path.AddArc(
            rect.Right - diameter,
            rect.Bottom - diameter,
            diameter,
            diameter,
            0,
            90
        )

        path.AddArc(
            rect.X,
            rect.Bottom - diameter,
            diameter,
            diameter,
            90,
            90
        )

        path.CloseFigure()

        Return path

    End Function

    ' =========================
    ' DRAW ROUNDED CONTROL
    ' =========================

    Protected Overrides Sub OnPaint(
        e As PaintEventArgs
    )

        MyBase.OnPaint(e)

        e.Graphics.SmoothingMode =
            SmoothingMode.AntiAlias

        If Me.Width <= 1 OrElse
           Me.Height <= 1 Then Return

        Dim fillRect As New RectangleF(
            0,
            0,
            Me.Width - 1,
            Me.Height - 1
        )

        ' DRAW BACKGROUND
        Using fillPath As GraphicsPath =
            CreateRoundedPath(
                fillRect,
                _cornerRadius
            )

            Using brush As New SolidBrush(_fillColor)

                e.Graphics.FillPath(
                    brush,
                    fillPath
                )

            End Using

        End Using

        ' DRAW BORDER
        If _borderThickness > 0 Then

            Dim inset As Single =
                _borderThickness / 2.0F

            Dim borderRect As New RectangleF(
                inset,
                inset,
                Math.Max(
                    1,
                    Me.Width - _borderThickness - 1
                ),
                Math.Max(
                    1,
                    Me.Height - _borderThickness - 1
                )
            )

            Dim borderRadius As Single =
                Math.Max(
                    0,
                    _cornerRadius - inset
                )

            Using borderPath As GraphicsPath =
                CreateRoundedPath(
                    borderRect,
                    borderRadius
                )

                Using pen As New Pen(
                    _borderColor,
                    _borderThickness
                )

                    e.Graphics.DrawPath(
                        pen,
                        borderPath
                    )

                End Using

            End Using

        End If

    End Sub

    ' =========================
    ' FIXED COMBOBOX LAYOUT
    ' =========================

    Private Sub UpdateComboLayout()

        If cmbInput Is Nothing OrElse
           lblDisplay Is Nothing OrElse
           pnlArrow Is Nothing Then Return

        If _updatingLayout Then Return

        _updatingLayout = True

        Try

            ' HORIZONTAL PADDING
            Dim paddingX As Integer =
                Math.Max(
                    _textPadding,
                    Math.Max(
                        _borderThickness + 4,
                        _cornerRadius \ 2 + 2
                    )
                )

            ' PROTECT TOP AND BOTTOM BORDER
            Dim marginY As Integer =
                Math.Max(
                    5,
                    _borderThickness + 3
                )

            cmbInput.Font = Me.Font

            ' GET ACTUAL COMBOBOX HEIGHT
            Dim comboHeight As Integer =
                cmbInput.PreferredHeight

            ' AUTOMATIC MINIMUM HEIGHT
            Dim minimumHeight As Integer =
                comboHeight + marginY * 2

            ' AUTOMATIC MINIMUM WIDTH
            Dim minimumWidth As Integer =
                Math.Max(
                    90,
                    paddingX * 2 + 60
                )

            Dim requiredMinimum As New Size(
                minimumWidth,
                minimumHeight
            )

            ' PREVENT CONTROL FROM BECOMING
            ' SMALLER THAN ITS CONTENT
            If Me.MinimumSize <> requiredMinimum Then

                Me.MinimumSize = requiredMinimum

            End If

            ' AVAILABLE WIDTH
            Dim availableWidth As Integer =
                Math.Max(
                    30,
                    Me.ClientSize.Width - paddingX * 2
                )

            ' VERTICALLY CENTER THE COMBOBOX
            Dim comboY As Integer =
                Math.Max(
                    marginY,
                    (Me.ClientSize.Height - comboHeight) \ 2
                )

            ' INTERNAL COMBOBOX
            cmbInput.SetBounds(
                paddingX,
                comboY,
                availableWidth,
                comboHeight
            )

            ' CUSTOM ARROW WIDTH
            Dim arrowWidth As Integer =
                Math.Min(
                    32,
                    Math.Max(
                        20,
                        availableWidth \ 3
                    )
                )

            ' TEXT DISPLAY WIDTH
            Dim displayWidth As Integer =
                Math.Max(
                    1,
                    availableWidth - arrowWidth
                )

            ' HINT / SELECTED TEXT LABEL
            lblDisplay.SetBounds(
                paddingX,
                comboY,
                displayWidth,
                comboHeight
            )

            ' CUSTOM DROPDOWN ARROW
            pnlArrow.SetBounds(
                paddingX + displayWidth,
                comboY,
                arrowWidth,
                comboHeight
            )

            lblDisplay.BringToFront()
            pnlArrow.BringToFront()

            pnlArrow.Invalidate()
            Me.Invalidate()

        Finally

            _updatingLayout = False

        End Try

    End Sub

    ' =========================
    ' RESIZE EVENT
    ' =========================

    Protected Overrides Sub OnResize(
        e As EventArgs
    )

        MyBase.OnResize(e)

        UpdateComboLayout()
        Me.Invalidate()

    End Sub

    ' =========================
    ' FONT CHANGE EVENT
    ' =========================

    Protected Overrides Sub OnFontChanged(
        e As EventArgs
    )

        MyBase.OnFontChanged(e)

        If cmbInput IsNot Nothing Then

            cmbInput.Font = Me.Font

            UpdateComboLayout()
            UpdateDisplay()

        End If

    End Sub

    ' =========================
    ' FOCUS EVENT
    ' =========================

    Protected Overrides Sub OnEnter(
        e As EventArgs
    )

        MyBase.OnEnter(e)

        If cmbInput IsNot Nothing AndAlso
           Not cmbInput.Focused Then

            cmbInput.Focus()

        End If

    End Sub

    ' =========================
    ' MOUSE DOWN EVENT
    ' =========================

    Protected Overrides Sub OnMouseDown(
        e As MouseEventArgs
    )

        MyBase.OnMouseDown(e)

        If e.Button = MouseButtons.Left AndAlso
           Me.Enabled Then

            cmbInput.Focus()
            cmbInput.DroppedDown = True

        End If

    End Sub

End Class

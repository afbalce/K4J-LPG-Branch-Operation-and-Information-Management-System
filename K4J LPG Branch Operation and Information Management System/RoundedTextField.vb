
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class RoundedTextField
    Inherits UserControl

    Private ReadOnly txtInput As New TextBox()
    Private ReadOnly lblHint As New Label()

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

    ' =========================
    ' HINT TEXT SETTINGS
    ' =========================

    Private _hintText As String = "Enter text..."

    Private _hintColor As Color = Color.Gray

    Private _hintFont As Font =
        New Font("Segoe UI", 11, FontStyle.Regular)

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
        Me.Size = New Size(250, 42)
        Me.MinimumSize = New Size(70, 28)
        Me.BackColor = Color.Transparent
        Me.TabStop = True

        Me.Font = New Font(
            "Segoe UI",
            11,
            FontStyle.Regular
        )

        ' =========================
        ' INTERNAL TEXTBOX
        ' =========================

        With txtInput
            .BorderStyle = BorderStyle.None
            .BackColor = _fillColor
            .ForeColor = _textColor
            .Font = Me.Font
            .Multiline = False
            .TabStop = False
        End With

        ' =========================
        ' HINT LABEL
        ' =========================

        With lblHint
            .Text = _hintText
            .Font = _hintFont
            .ForeColor = _hintColor
            .BackColor = _fillColor
            .AutoSize = False
            .AutoEllipsis = True
            .TextAlign = ContentAlignment.MiddleLeft
            .Cursor = Cursors.IBeam
        End With

        Me.Controls.Add(txtInput)
        Me.Controls.Add(lblHint)

        lblHint.BringToFront()

        ' =========================
        ' EVENT HANDLERS
        ' =========================

        AddHandler txtInput.TextChanged,
            AddressOf Input_TextChanged

        AddHandler txtInput.GotFocus,
            AddressOf Input_GotFocus

        AddHandler txtInput.LostFocus,
            AddressOf Input_LostFocus

        AddHandler lblHint.Click,
            AddressOf Hint_Click

        AddHandler lblHint.MouseDown,
            AddressOf Hint_MouseDown

        UpdateTextBoxLayout()
        UpdateHintVisibility()

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
            UpdateTextBoxLayout()
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
            UpdateTextBoxLayout()
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

            txtInput.BackColor = value
            lblHint.BackColor = value

            Me.Invalidate()

        End Set

    End Property

    ' =========================
    ' TEXT COLOR
    ' =========================

    <Category("Rounded Appearance")>
    <Description("Changes the actual input text color.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property TextColor As Color

        Get
            Return _textColor
        End Get

        Set(value As Color)

            _textColor = value
            txtInput.ForeColor = value

            Me.Invalidate()

        End Set

    End Property

    ' =========================
    ' TEXT PADDING
    ' =========================

    <Category("Rounded Appearance")>
    <Description("Adjusts horizontal text padding.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property TextPadding As Integer

        Get
            Return _textPadding
        End Get

        Set(value As Integer)

            _textPadding = Math.Max(0, value)
            UpdateTextBoxLayout()

        End Set

    End Property

    ' =========================
    ' HINT TEXT
    ' =========================

    <Category("Hint Appearance")>
    <Description("Text displayed when the textbox is empty.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    <DefaultValue("Enter text...")>
    Public Property HintText As String

        Get
            Return _hintText
        End Get

        Set(value As String)

            _hintText = If(value, "")

            lblHint.Text = _hintText

            UpdateHintVisibility()

        End Set

    End Property

    ' =========================
    ' HINT COLOR
    ' =========================

    <Category("Hint Appearance")>
    <Description("Changes the hint text color.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property HintColor As Color

        Get
            Return _hintColor
        End Get

        Set(value As Color)

            _hintColor = value
            lblHint.ForeColor = value

            Me.Invalidate()

        End Set

    End Property

    ' =========================
    ' HINT FONT
    ' =========================

    <Category("Hint Appearance")>
    <Description("Changes the hint font family and style.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property HintFont As Font

        Get
            Return _hintFont
        End Get

        Set(value As Font)

            If value Is Nothing Then Return

            _hintFont = value

            lblHint.Font = _hintFont

            UpdateTextBoxLayout()
            Me.Invalidate()

        End Set

    End Property

    ' =========================
    ' HINT FONT SIZE
    ' =========================

    <Category("Hint Appearance")>
    <Description("Adjusts hint font size independently.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property HintFontSize As Single

        Get
            Return _hintFont.Size
        End Get

        Set(value As Single)

            If Single.IsNaN(value) OrElse
               Single.IsInfinity(value) Then Return

            Dim newSize As Single =
                Math.Max(1.0F, value)

            _hintFont = New Font(
                _hintFont.FontFamily,
                newSize,
                _hintFont.Style,
                _hintFont.Unit
            )

            lblHint.Font = _hintFont

            UpdateTextBoxLayout()
            Me.Invalidate()

        End Set

    End Property

    ' =========================
    ' PASSWORD CHARACTER
    ' =========================

    <Category("Behavior")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property UseSystemPasswordChar As Boolean

        Get
            Return txtInput.UseSystemPasswordChar
        End Get

        Set(value As Boolean)
            txtInput.UseSystemPasswordChar = value
            UpdateHintVisibility()
        End Set

    End Property

    ' =========================
    ' TEXT ALIGNMENT
    ' =========================

    <Category("Appearance")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property TextAlignment As HorizontalAlignment

        Get
            Return txtInput.TextAlign
        End Get

        Set(value As HorizontalAlignment)

            txtInput.TextAlign = value

            Select Case value

                Case HorizontalAlignment.Center
                    lblHint.TextAlign =
                        ContentAlignment.MiddleCenter

                Case HorizontalAlignment.Right
                    lblHint.TextAlign =
                        ContentAlignment.MiddleRight

                Case Else
                    lblHint.TextAlign =
                        ContentAlignment.MiddleLeft

            End Select

        End Set

    End Property

    ' =========================
    ' ACTUAL TEXT
    ' =========================

    <Browsable(True)>
    Public Overrides Property Text As String

        Get

            If txtInput Is Nothing Then Return ""

            Return txtInput.Text

        End Get

        Set(value As String)

            If txtInput IsNot Nothing Then

                txtInput.Text = If(value, "")

            End If

        End Set

    End Property

    ' =========================
    ' HINT VISIBILITY
    ' =========================

    Private Sub UpdateHintVisibility()

        If txtInput Is Nothing OrElse
           lblHint Is Nothing Then Return

        lblHint.Visible =
            String.IsNullOrEmpty(txtInput.Text) AndAlso
            Not txtInput.Focused AndAlso
            Not String.IsNullOrEmpty(_hintText)

        If lblHint.Visible Then
            lblHint.BringToFront()
        End If

    End Sub

    ' =========================
    ' INPUT EVENTS
    ' =========================

    Private Sub Input_TextChanged(
        sender As Object,
        e As EventArgs
    )

        UpdateHintVisibility()

        MyBase.OnTextChanged(EventArgs.Empty)

    End Sub

    Private Sub Input_GotFocus(
        sender As Object,
        e As EventArgs
    )

        UpdateHintVisibility()

    End Sub

    Private Sub Input_LostFocus(
        sender As Object,
        e As EventArgs
    )

        UpdateHintVisibility()

    End Sub

    Private Sub Hint_Click(
        sender As Object,
        e As EventArgs
    )

        txtInput.Focus()

    End Sub

    Private Sub Hint_MouseDown(
        sender As Object,
        e As MouseEventArgs
    )

        txtInput.Focus()

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
                Math.Min(rect.Width, rect.Height) / 2.0F
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
    ' DRAW CONTROL
    ' =========================

    Protected Overrides Sub OnPaint(
        e As PaintEventArgs
    )

        MyBase.OnPaint(e)

        e.Graphics.SmoothingMode =
            SmoothingMode.AntiAlias

        If Me.Width <= 1 OrElse Me.Height <= 1 Then
            Return
        End If

        Dim fillRect As New RectangleF(
            0,
            0,
            Me.Width - 1,
            Me.Height - 1
        )

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
    ' TEXTBOX AND HINT POSITION
    ' =========================

    Private Sub UpdateTextBoxLayout()

        If txtInput Is Nothing OrElse
           lblHint Is Nothing Then Return

        Dim paddingX As Integer =
            Math.Max(
                _textPadding,
                Math.Max(
                    _borderThickness + 4,
                    _cornerRadius \ 2 + 2
                )
            )

        Dim availableWidth As Integer =
            Math.Max(
                1,
                Me.Width - paddingX * 2
            )

        ' ACTUAL TEXTBOX
        txtInput.Width = availableWidth

        txtInput.Location = New Point(
            paddingX,
            Math.Max(
                0,
                (Me.Height - txtInput.Height) \ 2
            )
        )

        ' HINT LABEL
        Dim hintHeight As Integer =
            lblHint.Font.Height + 4

        lblHint.Size = New Size(
            availableWidth,
            hintHeight
        )

        lblHint.Location = New Point(
            paddingX,
            Math.Max(
                0,
                (Me.Height - hintHeight) \ 2
            )
        )

    End Sub

    ' =========================
    ' CONTROL EVENTS
    ' =========================

    Protected Overrides Sub OnResize(
        e As EventArgs
    )

        MyBase.OnResize(e)

        UpdateTextBoxLayout()
        Me.Invalidate()

    End Sub

    Protected Overrides Sub OnFontChanged(
        e As EventArgs
    )

        MyBase.OnFontChanged(e)

        If txtInput IsNot Nothing Then

            txtInput.Font = Me.Font
            UpdateTextBoxLayout()

        End If

    End Sub

    Protected Overrides Sub OnEnter(
        e As EventArgs
    )

        MyBase.OnEnter(e)

        If txtInput IsNot Nothing Then
            txtInput.Focus()
        End If

    End Sub

    Protected Overrides Sub OnMouseDown(
        e As MouseEventArgs
    )

        MyBase.OnMouseDown(e)

        txtInput.Focus()

    End Sub

End Class

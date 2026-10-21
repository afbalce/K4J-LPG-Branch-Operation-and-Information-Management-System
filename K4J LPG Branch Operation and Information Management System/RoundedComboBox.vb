
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class RoundedComboBox
    Inherits UserControl

    ' =====================================
    ' INTERNAL CONTROLS
    ' =====================================

    Private ReadOnly cmbInput As New ComboBox()
    Private ReadOnly displaySurface As ComboSurface

    ' =====================================
    ' DEFAULT SETTINGS
    ' =====================================

    Private Const MinControlHeight As Integer = 28

    Private _cornerRadius As Integer = 12

    Private _borderColor As Color =
        Color.FromArgb(223, 149, 63)

    Private _borderThickness As Integer = 2
    Private _fillColor As Color = Color.White

    Private _textColor As Color =
        Color.FromArgb(50, 50, 50)

    Private _textPadding As Integer = 12

    Private _arrowColor As Color =
        Color.FromArgb(80, 80, 80)

    ' =====================================
    ' HINT SETTINGS
    ' =====================================

    Private _hintText As String = "Select an option..."
    Private _hintColor As Color = Color.Gray

    Private _hintFont As Font =
        New Font("Segoe UI", 9.0F, FontStyle.Regular)

    ' =====================================
    ' CONSTRUCTOR
    ' =====================================

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

        Me.MinimumSize = New Size(90, MinControlHeight)
        Me.Size = New Size(250, MinControlHeight)

        Me.TabStop = False

        Me.Font = New Font(
            "Segoe UI",
            10.0F,
            FontStyle.Regular
        )

        ' INITIALIZE CUSTOM SURFACE HERE
        displaySurface = New ComboSurface(Me)

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

        ' CUSTOM DISPLAY SURFACE
        With displaySurface

            .Dock = DockStyle.Fill
            .BackColor = Color.Transparent
            .Cursor = Cursors.Hand
            .TabStop = False

        End With

        Me.Controls.Add(cmbInput)
        Me.Controls.Add(displaySurface)

        displaySurface.BringToFront()

        ' EVENTS
        AddHandler cmbInput.SelectedIndexChanged,
            AddressOf Combo_SelectedIndexChanged

        AddHandler displaySurface.MouseClick,
            AddressOf Surface_MouseClick

        UpdateComboLayout()

    End Sub

    ' =====================================
    ' CUSTOM PAINTING SURFACE
    ' =====================================

    Private NotInheritable Class ComboSurface
        Inherits Control

        Private ReadOnly _owner As RoundedComboBox

        Public Sub New(parentControl As RoundedComboBox)

            MyBase.New()

            _owner = parentControl

            Me.SetStyle(
                ControlStyles.UserPaint Or
                ControlStyles.AllPaintingInWmPaint Or
                ControlStyles.OptimizedDoubleBuffer Or
                ControlStyles.ResizeRedraw Or
                ControlStyles.SupportsTransparentBackColor,
                True
            )

            Me.BackColor = Color.Transparent

        End Sub

        Protected Overrides Sub OnPaint(
            e As PaintEventArgs
        )

            MyBase.OnPaint(e)

            _owner.DrawComboSurface(
                e.Graphics,
                Me.ClientSize
            )

        End Sub

    End Class

    ' =====================================
    ' CORNER RADIUS
    ' =====================================

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
            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' BORDER COLOR
    ' =====================================

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
            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' BORDER THICKNESS
    ' =====================================

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
            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' FILL COLOR
    ' =====================================

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

            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' TEXT COLOR
    ' =====================================

    <Category("Rounded Appearance")>
    <Description("Changes the selected text color.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property TextColor As Color

        Get
            Return _textColor
        End Get

        Set(value As Color)

            _textColor = value
            cmbInput.ForeColor = value

            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' TEXT PADDING
    ' =====================================

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
            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' ARROW COLOR
    ' =====================================

    <Category("Rounded Appearance")>
    <Description("Changes the dropdown arrow color.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property ArrowColor As Color

        Get
            Return _arrowColor
        End Get

        Set(value As Color)

            _arrowColor = value
            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' HINT TEXT
    ' =====================================

    <Category("Hint Appearance")>
    <Description("Placeholder displayed when no item is selected.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property HintText As String

        Get
            Return _hintText
        End Get

        Set(value As String)

            _hintText = If(value, "")
            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' HINT COLOR
    ' =====================================

    <Category("Hint Appearance")>
    <Description("Changes the placeholder color.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property HintColor As Color

        Get
            Return _hintColor
        End Get

        Set(value As Color)

            _hintColor = value
            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' HINT FONT
    ' =====================================

    <Category("Hint Appearance")>
    <Description("Changes the placeholder font.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property HintFont As Font

        Get
            Return _hintFont
        End Get

        Set(value As Font)

            If value Is Nothing Then Return

            _hintFont = value
            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' HINT FONT SIZE
    ' =====================================

    <Category("Hint Appearance")>
    <Description("Adjusts the placeholder font size.")>
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

            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' COMBOBOX ITEMS
    ' =====================================

    <Category("Data")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Content)>
    Public ReadOnly Property Items As ComboBox.ObjectCollection

        Get
            Return cmbInput.Items
        End Get

    End Property

    ' =====================================
    ' SELECTED INDEX
    ' =====================================

    <Category("Data")>
    <DefaultValue(-1)>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible)>
    Public Property SelectedIndex As Integer

        Get
            Return cmbInput.SelectedIndex
        End Get

        Set(value As Integer)

            cmbInput.SelectedIndex = value
            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' SELECTED ITEM
    ' =====================================

    <Browsable(False)>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Hidden)>
    Public Property SelectedItem As Object

        Get
            Return cmbInput.SelectedItem
        End Get

        Set(value As Object)

            cmbInput.SelectedItem = value
            RefreshSurface()

        End Set

    End Property

    ' =====================================
    ' DROPDOWN HEIGHT
    ' =====================================

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

    ' =====================================
    ' SELECTION CHANGED EVENT
    ' =====================================

    Public Event SelectedIndexChanged As EventHandler

    Private Sub Combo_SelectedIndexChanged(
        sender As Object,
        e As EventArgs
    )

        RefreshSurface()

        RaiseEvent SelectedIndexChanged(
            Me,
            EventArgs.Empty
        )

    End Sub

    ' =====================================
    ' OPEN DROPDOWN
    ' =====================================

    Private Sub Surface_MouseClick(
        sender As Object,
        e As MouseEventArgs
    )

        If e.Button <> MouseButtons.Left Then Return
        If Not Me.Enabled Then Return
        If Me.DesignMode Then Return

        cmbInput.Focus()

        cmbInput.DroppedDown =
            Not cmbInput.DroppedDown

    End Sub

    ' =====================================
    ' CREATE ROUNDED RECTANGLE
    ' =====================================

    Private Function CreateRoundedPath(
        rect As RectangleF,
        radius As Single
    ) As GraphicsPath

        Dim path As New GraphicsPath()

        If rect.Width <= 0 OrElse
           rect.Height <= 0 Then

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

    ' =====================================
    ' DRAW COMPLETE CUSTOM COMBOBOX
    ' =====================================

    Private Sub DrawComboSurface(
        g As Graphics,
        controlSize As Size
    )

        If controlSize.Width <= 2 OrElse
           controlSize.Height <= 2 Then Return

        g.SmoothingMode = SmoothingMode.AntiAlias

        Dim w As Integer = controlSize.Width
        Dim h As Integer = controlSize.Height

        ' =================================
        ' BACKGROUND
        ' =================================

        Dim fillRect As New RectangleF(
            0,
            0,
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
        ' ROUNDED BORDER
        ' =================================

        If _borderThickness > 0 Then

            Dim thickness As Single =
                CSng(
                    Math.Min(
                        _borderThickness,
                        Math.Min(w, h)
                    )
                )

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

            Dim adjustedRadius As Single =
                Math.Max(
                    0.0F,
                    CSng(_cornerRadius) - inset
                )

            Using borderPath As GraphicsPath =
                CreateRoundedPath(
                    borderRect,
                    adjustedRadius
                )

                Using borderPen As New Pen(
                    _borderColor,
                    thickness
                )

                    g.DrawPath(
                        borderPen,
                        borderPath
                    )

                End Using

            End Using

        End If

        ' =================================
        ' DISPLAY TEXT
        ' =================================

        Dim isHint As Boolean =
            (cmbInput.SelectedIndex = -1)

        Dim displayText As String
        Dim displayFont As Font
        Dim displayColor As Color

        If isHint Then

            displayText = _hintText
            displayFont = _hintFont
            displayColor = _hintColor

        Else

            displayText =
                cmbInput.GetItemText(
                    cmbInput.SelectedItem
                )

            displayFont = Me.Font
            displayColor = _textColor

        End If

        If Not Me.Enabled Then
            displayColor = SystemColors.GrayText
        End If

        Dim paddingX As Integer =
            Math.Max(
                _textPadding,
                _borderThickness + 5
            )

        Dim arrowX As Integer =
            w - Math.Max(
                18,
                _textPadding + 6
            )

        Dim textWidth As Integer =
            Math.Max(
                1,
                arrowX - paddingX - 13
            )

        ' KEEP TEXT INSIDE THE 28PX HEIGHT
        Dim textRect As New Rectangle(
            paddingX,
            2,
            textWidth,
            Math.Max(1, h - 4)
        )

        Dim textFlags As TextFormatFlags =
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
            textFlags
        )

        ' =================================
        ' DROPDOWN ARROW
        ' =================================

        Dim centerX As Single = CSng(arrowX)
        Dim centerY As Single = CSng(h) / 2.0F

        Dim arrowPoints As PointF() = {
            New PointF(centerX - 5.0F, centerY - 2.0F),
            New PointF(centerX, centerY + 3.0F),
            New PointF(centerX + 5.0F, centerY - 2.0F)
        }

        Using arrowPen As New Pen(
            _arrowColor,
            1.8F
        )

            arrowPen.StartCap = LineCap.Round
            arrowPen.EndCap = LineCap.Round
            arrowPen.LineJoin = LineJoin.Round

            g.DrawLines(
                arrowPen,
                arrowPoints
            )

        End Using

    End Sub

    ' =====================================
    ' UPDATE LAYOUT
    ' =====================================

    Private Sub UpdateComboLayout()

        If cmbInput Is Nothing OrElse
           displaySurface Is Nothing Then Return

        Dim availableWidth As Integer =
            Math.Max(
                1,
                Me.ClientSize.Width
            )

        ' KEEP NATIVE COMBOBOX BEHIND SURFACE
        cmbInput.SetBounds(
            0,
            0,
            availableWidth,
            cmbInput.PreferredHeight
        )

        cmbInput.DropDownWidth = availableWidth

        ' ENSURE CUSTOM SURFACE IS VISIBLE
        displaySurface.Dock = DockStyle.Fill
        displaySurface.BringToFront()

        RefreshSurface()

    End Sub

    ' =====================================
    ' REFRESH CUSTOM SURFACE
    ' =====================================

    Private Sub RefreshSurface()

        If displaySurface IsNot Nothing Then
            displaySurface.Invalidate()
        End If

        Me.Invalidate()

    End Sub

    ' =====================================
    ' RESIZE EVENT
    ' =====================================

    Protected Overrides Sub OnResize(
        e As EventArgs
    )

        MyBase.OnResize(e)

        UpdateComboLayout()

    End Sub

    ' =====================================
    ' FONT CHANGE EVENT
    ' =====================================

    Protected Overrides Sub OnFontChanged(
        e As EventArgs
    )

        MyBase.OnFontChanged(e)

        If cmbInput IsNot Nothing Then

            cmbInput.Font = Me.Font
            UpdateComboLayout()

        End If

    End Sub

    ' =====================================
    ' ENABLED CHANGE
    ' =====================================

    Protected Overrides Sub OnEnabledChanged(
        e As EventArgs
    )

        MyBase.OnEnabledChanged(e)

        If cmbInput IsNot Nothing Then
            cmbInput.Enabled = Me.Enabled
        End If

        RefreshSurface()

    End Sub

    ' =====================================
    ' CONTROL FOCUS
    ' =====================================

    Protected Overrides Sub OnEnter(
        e As EventArgs
    )

        MyBase.OnEnter(e)

        If cmbInput IsNot Nothing AndAlso
           Not cmbInput.Focused Then

            cmbInput.Focus()

        End If

    End Sub

End Class

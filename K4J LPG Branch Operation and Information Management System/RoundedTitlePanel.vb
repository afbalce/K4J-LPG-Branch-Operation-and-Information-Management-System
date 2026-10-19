
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class RoundedTitlePanel
    Inherits Panel

    ' =====================================
    ' DEFAULT COLORS
    ' =====================================

    Private _borderColor As Color =
        Color.FromArgb(223, 149, 63)

    Private _fillColor As Color =
        Color.FromArgb(245, 239, 232)

    ' =====================================
    ' DEFAULT OPACITY (0 TO 100)
    ' =====================================

    Private _borderOpacity As Integer = 100
    Private _fillOpacity As Integer = 100

    ' =====================================
    ' DEFAULT APPEARANCE
    ' =====================================

    Private _borderThickness As Integer = 2
    Private _cornerRadius As Integer = 12

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

        Me.Size = New Size(700, 100)

        Me.BackColor = Color.Transparent

        UpdatePanelRegion()

    End Sub

    ' =====================================
    ' BORDER COLOR
    ' =====================================

    <Category("Custom Appearance")>
    <Description("Changes the border color.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible
    )>
    Public Property BorderColor As Color

        Get
            Return _borderColor
        End Get

        Set(value As Color)

            _borderColor = value

            Me.Invalidate()

        End Set

    End Property

    ' =====================================
    ' BORDER OPACITY
    ' =====================================

    <Category("Custom Appearance")>
    <Description("Border opacity from 0 (invisible) to 100 (fully visible).")>
    <DefaultValue(100)>
    Public Property BorderOpacity As Integer

        Get
            Return _borderOpacity
        End Get

        Set(value As Integer)

            _borderOpacity =
                Math.Max(0, Math.Min(100, value))

            Me.Invalidate()

        End Set

    End Property

    ' =====================================
    ' FILL COLOR
    ' =====================================

    <Category("Custom Appearance")>
    <Description("Changes the fill/background color of the panel.")>
    <DesignerSerializationVisibility(
        DesignerSerializationVisibility.Visible
    )>
    Public Property FillColor As Color

        Get
            Return _fillColor
        End Get

        Set(value As Color)

            _fillColor = value

            Me.Invalidate()

        End Set

    End Property

    ' =====================================
    ' FILL OPACITY
    ' =====================================

    <Category("Custom Appearance")>
    <Description("Fill opacity from 0 (transparent) to 100 (fully visible).")>
    <DefaultValue(100)>
    Public Property FillOpacity As Integer

        Get
            Return _fillOpacity
        End Get

        Set(value As Integer)

            _fillOpacity =
                Math.Max(0, Math.Min(100, value))

            Me.Invalidate()

        End Set

    End Property

    ' =====================================
    ' BORDER THICKNESS
    ' =====================================

    <Category("Custom Appearance")>
    <Description("Changes the border thickness.")>
    <DefaultValue(2)>
    Public Property BorderThickness As Integer

        Get
            Return _borderThickness
        End Get

        Set(value As Integer)

            _borderThickness = Math.Max(0, value)

            Me.Invalidate()

        End Set

    End Property

    ' =====================================
    ' CORNER RADIUS
    ' =====================================

    <Category("Custom Appearance")>
    <Description("Changes the corner roundness.")>
    <DefaultValue(12)>
    Public Property CornerRadius As Integer

        Get
            Return _cornerRadius
        End Get

        Set(value As Integer)

            _cornerRadius = Math.Max(0, value)

            UpdatePanelRegion()

            Me.Invalidate()

        End Set

    End Property

    ' =====================================
    ' CREATE COLOR WITH OPACITY
    ' =====================================

    Private Function ApplyOpacity(
        baseColor As Color,
        opacityPercent As Integer
    ) As Color

        ' COMBINES THE COLOR'S EXISTING
        ' ALPHA WITH THE OPACITY PROPERTY

        Dim alpha As Integer =
            CInt(
                Math.Round(
                    baseColor.A *
                    (opacityPercent / 100.0)
                )
            )

        alpha = Math.Max(
            0,
            Math.Min(255, alpha)
        )

        Return Color.FromArgb(
            alpha,
            baseColor.R,
            baseColor.G,
            baseColor.B
        )

    End Function

    ' =====================================
    ' UPDATE PANEL REGION
    ' =====================================

    Private Sub UpdatePanelRegion()

        If Me.Width <= 0 OrElse
           Me.Height <= 0 Then Return

        Dim rect As New RectangleF(
            0,
            0,
            Me.Width,
            Me.Height
        )

        Using path As GraphicsPath =
            GetRoundedPath(
                rect,
                CSng(_cornerRadius)
            )

            Dim oldRegion As Region = Me.Region

            Me.Region = New Region(path)

            If oldRegion IsNot Nothing Then

                oldRegion.Dispose()

            End If

        End Using

    End Sub

    ' =====================================
    ' PAINT TRANSPARENT BACKGROUND
    ' =====================================

    Protected Overrides Sub OnPaintBackground(
        e As PaintEventArgs
    )

        ' BACKCOLOR IS TRANSPARENT SO WINDOWS FORMS
        ' CAN PAINT THE PARENT BACKGROUND BEHIND
        ' THE SEMI-TRANSPARENT FILL.

        MyBase.OnPaintBackground(e)

    End Sub

    ' =====================================
    ' DRAW ROUNDED PANEL
    ' =====================================

    Protected Overrides Sub OnPaint(
        e As PaintEventArgs
    )

        MyBase.OnPaint(e)

        If Me.Width <= 1 OrElse
           Me.Height <= 1 Then Return

        e.Graphics.SmoothingMode =
            SmoothingMode.AntiAlias

        e.Graphics.PixelOffsetMode =
            PixelOffsetMode.HighQuality

        ' =====================================
        ' APPLY FILL OPACITY
        ' =====================================

        Dim actualFillColor As Color =
            ApplyOpacity(
                _fillColor,
                _fillOpacity
            )

        ' =====================================
        ' APPLY BORDER OPACITY
        ' =====================================

        Dim actualBorderColor As Color =
            ApplyOpacity(
                _borderColor,
                _borderOpacity
            )

        ' =====================================
        ' DRAW FILL
        ' =====================================

        Dim fillRect As New RectangleF(
            0,
            0,
            Me.Width,
            Me.Height
        )

        Using fillPath As GraphicsPath =
            GetRoundedPath(
                fillRect,
                CSng(_cornerRadius)
            )

            If actualFillColor.A > 0 Then

                Using brush As New SolidBrush(
                    actualFillColor
                )

                    e.Graphics.FillPath(
                        brush,
                        fillPath
                    )

                End Using

            End If

        End Using

        ' =====================================
        ' DRAW BORDER
        ' =====================================

        Dim thickness As Integer =
            Math.Min(
                _borderThickness,
                Math.Max(
                    0,
                    Math.Min(Me.Width, Me.Height) - 1
                )
            )

        If thickness > 0 AndAlso
           actualBorderColor.A > 0 Then

            Dim inset As Single =
                thickness / 2.0F + 0.5F

            Dim borderRect As New RectangleF(
                inset,
                inset,
                Me.Width - (inset * 2.0F) - 1.0F,
                Me.Height - (inset * 2.0F) - 1.0F
            )

            If borderRect.Width > 0 AndAlso
               borderRect.Height > 0 Then

                Dim adjustedRadius As Single =
                    Math.Max(
                        0.0F,
                        CSng(_cornerRadius) - inset
                    )

                Using borderPath As GraphicsPath =
                    GetRoundedPath(
                        borderRect,
                        adjustedRadius
                    )

                    Using pen As New Pen(
                        actualBorderColor,
                        CSng(thickness)
                    )

                        pen.LineJoin =
                            LineJoin.Round

                        e.Graphics.DrawPath(
                            pen,
                            borderPath
                        )

                    End Using

                End Using

            End If

        End If

    End Sub

    ' =====================================
    ' RESPONSIVE RESIZE
    ' =====================================

    Protected Overrides Sub OnSizeChanged(
        e As EventArgs
    )

        MyBase.OnSizeChanged(e)

        UpdatePanelRegion()

        Me.Invalidate()

    End Sub

    ' =====================================
    ' CREATE ROUNDED RECTANGLE
    ' =====================================

    Private Function GetRoundedPath(
        rect As RectangleF,
        radius As Single
    ) As GraphicsPath

        Dim path As New GraphicsPath()

        If rect.Width <= 0 OrElse
           rect.Height <= 0 Then

            Return path

        End If

        Dim r As Single =
            Math.Min(
                Math.Max(0.0F, radius),
                Math.Min(
                    rect.Width,
                    rect.Height
                ) / 2.0F
            )

        If r <= 0 Then

            path.AddRectangle(rect)

            Return path

        End If

        Dim d As Single = r * 2.0F

        path.StartFigure()

        ' TOP LEFT

        path.AddArc(
            rect.Left,
            rect.Top,
            d,
            d,
            180,
            90
        )

        ' TOP RIGHT

        path.AddArc(
            rect.Right - d,
            rect.Top,
            d,
            d,
            270,
            90
        )

        ' BOTTOM RIGHT

        path.AddArc(
            rect.Right - d,
            rect.Bottom - d,
            d,
            d,
            0,
            90
        )

        ' BOTTOM LEFT

        path.AddArc(
            rect.Left,
            rect.Bottom - d,
            d,
            d,
            90,
            90
        )

        path.CloseFigure()

        Return path

    End Function

End Class

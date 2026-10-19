
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.ComponentModel

Public Class PasswordEyeButton
    Inherits Control

    Private _passwordVisible As Boolean = False
    Private _hovered As Boolean = False

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property PasswordVisible As Boolean
        Get
            Return _passwordVisible
        End Get
        Set(value As Boolean)
            _passwordVisible = value
            Me.Invalidate()
        End Set
    End Property

    Public Sub New()

        Me.SetStyle(
            ControlStyles.UserPaint Or
            ControlStyles.AllPaintingInWmPaint Or
            ControlStyles.OptimizedDoubleBuffer Or
            ControlStyles.ResizeRedraw Or
            ControlStyles.Selectable,
            True
        )

        Me.DoubleBuffered = True

        Me.Size = New Size(28, 28)
        Me.BackColor = Color.White
        Me.Cursor = Cursors.Hand
        Me.TabStop = True

        Me.AccessibleName = "Show password"

    End Sub

    Protected Overrides Sub OnMouseEnter(e As EventArgs)

        MyBase.OnMouseEnter(e)
        _hovered = True
        Me.Invalidate()

    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)

        MyBase.OnMouseLeave(e)
        _hovered = False
        Me.Invalidate()

    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)

        If e.KeyCode = Keys.Space OrElse
           e.KeyCode = Keys.Enter Then

            Me.OnClick(EventArgs.Empty)
            e.Handled = True
            e.SuppressKeyPress = True

        Else

            MyBase.OnKeyDown(e)

        End If

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        MyBase.OnPaintBackground(e)

        e.Graphics.SmoothingMode =
            SmoothingMode.AntiAlias

        Dim cx As Single = Me.Width / 2.0F
        Dim cy As Single = Me.Height / 2.0F

        Dim halfW As Single =
            Math.Max(
                4.0F,
                Math.Min(9.0F, (Me.Width - 6) / 2.0F)
            )

        Dim halfH As Single = halfW * 0.6F

        Dim eyeColor As Color =
            If(
                _hovered,
                Color.FromArgb(232, 93, 4),
                Color.FromArgb(120, 128, 140)
            )

        Using pen As New Pen(eyeColor, 1.7F)

            pen.StartCap = LineCap.Round
            pen.EndCap = LineCap.Round
            pen.LineJoin = LineJoin.Round

            Using path As New GraphicsPath()

                ' TOP EYE CURVE
                path.AddBezier(
                    cx - halfW, cy,
                    cx - halfW / 2, cy - halfH,
                    cx + halfW / 2, cy - halfH,
                    cx + halfW, cy
                )

                ' BOTTOM EYE CURVE
                path.AddBezier(
                    cx + halfW, cy,
                    cx + halfW / 2, cy + halfH,
                    cx - halfW / 2, cy + halfH,
                    cx - halfW, cy
                )

                e.Graphics.DrawPath(pen, path)

            End Using

            ' PUPIL
            e.Graphics.DrawEllipse(
                pen,
                cx - 2.7F,
                cy - 2.7F,
                5.4F,
                5.4F
            )

            ' SLASH WHEN PASSWORD IS VISIBLE
            If _passwordVisible Then

                e.Graphics.DrawLine(
                    pen,
                    cx - halfW - 2,
                    cy + halfH + 2,
                    cx + halfW + 2,
                    cy - halfH - 2
                )

            End If

        End Using

    End Sub

End Class



Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Globalization
Imports System.Windows.Forms

Public Class SpacedLabel
    Inherits Label

    ' ==========================================
    ' DEFAULT SETTINGS
    ' ==========================================

    Private _letterSpacing As Single = 0.0F

    ' ==========================================
    ' CONSTRUCTOR
    ' ==========================================

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

        Me.AutoSize = False

        Me.Size = New Size(300, 45)

        Me.BackColor = Color.Transparent

        Me.ForeColor = Color.Black

        Me.Font = New Font(
            "Segoe UI",
            14.0F,
            FontStyle.Bold
        )

        Me.TextAlign = ContentAlignment.MiddleLeft

    End Sub

    ' ==========================================
    ' LETTER SPACING PROPERTY
    ' ==========================================

    <Category("Custom Appearance")>
    <Description("Adjusts the spacing between letters in pixels. Positive values increase spacing; negative values decrease spacing.")>
    <DefaultValue(0.0F)>
    Public Property LetterSpacing As Single

        Get
            Return _letterSpacing
        End Get

        Set(value As Single)

            If Single.IsNaN(value) OrElse
               Single.IsInfinity(value) Then Return

            If _letterSpacing <> value Then

                _letterSpacing = value

                Me.Invalidate()

            End If

        End Set

    End Property

    ' ==========================================
    ' MEASURE A TEXT ELEMENT
    ' ==========================================

    Private Function MeasureElement(
        g As Graphics,
        element As String,
        format As StringFormat
    ) As Single

        Return g.MeasureString(
            element,
            Me.Font,
            PointF.Empty,
            format
        ).Width

    End Function

    ' ==========================================
    ' MEASURE LINE WIDTH
    ' ==========================================

    Private Function MeasureLineWidth(
        g As Graphics,
        line As String,
        format As StringFormat
    ) As Single

        If String.IsNullOrEmpty(line) Then
            Return 0.0F
        End If

        Dim width As Single = 0.0F
        Dim count As Integer = 0

        Dim enumerator As TextElementEnumerator =
            StringInfo.GetTextElementEnumerator(line)

        While enumerator.MoveNext()

            Dim element As String =
                enumerator.GetTextElement()

            width += MeasureElement(
                g,
                element,
                format
            )

            count += 1

        End While

        If count > 1 Then

            width += (count - 1) * _letterSpacing

        End If

        Return Math.Max(0.0F, width)

    End Function

    ' ==========================================
    ' DRAW LETTER-SPACED LINE
    ' ==========================================

    Private Sub DrawSpacedLine(
        g As Graphics,
        line As String,
        x As Single,
        y As Single,
        brush As Brush,
        format As StringFormat
    )

        If String.IsNullOrEmpty(line) Then Return

        Dim currentX As Single = x

        Dim enumerator As TextElementEnumerator =
            StringInfo.GetTextElementEnumerator(line)

        While enumerator.MoveNext()

            Dim element As String =
                enumerator.GetTextElement()

            ' DRAW INDIVIDUAL LETTER

            g.DrawString(
                element,
                Me.Font,
                brush,
                currentX,
                y,
                format
            )

            ' ADVANCE TO NEXT LETTER

            currentX += MeasureElement(
                g,
                element,
                format
            ) + _letterSpacing

        End While

    End Sub

    ' ==========================================
    ' CUSTOM LABEL PAINTING
    ' ==========================================

    Protected Overrides Sub OnPaint(
        e As PaintEventArgs
    )

        If Me.Width <= 0 OrElse
           Me.Height <= 0 Then Return

        If String.IsNullOrEmpty(Me.Text) Then Return

        Dim g As Graphics = e.Graphics

        g.SmoothingMode =
            SmoothingMode.AntiAlias

        g.TextRenderingHint =
            TextRenderingHint.AntiAliasGridFit

        ' ======================================
        ' AVAILABLE CONTENT AREA
        ' ======================================

        Dim area As New RectangleF(
            Me.Padding.Left,
            Me.Padding.Top,
            Math.Max(
                0,
                Me.ClientSize.Width -
                Me.Padding.Horizontal
            ),
            Math.Max(
                0,
                Me.ClientSize.Height -
                Me.Padding.Vertical
            )
        )

        If area.Width <= 0 OrElse
           area.Height <= 0 Then Return

        ' ======================================
        ' SUPPORT MULTIPLE LINES
        ' ======================================

        Dim normalizedText As String =
            Me.Text.Replace(
                vbCrLf,
                vbLf
            ).Replace(
                vbCr,
                vbLf
            )

        Dim lines As String() =
            normalizedText.Split(
                New Char() {ControlChars.Lf}
            )

        Dim lineHeight As Single =
            Me.Font.GetHeight(g) * 1.12F

        Dim totalHeight As Single =
            lines.Length * lineHeight

        ' ======================================
        ' VERTICAL ALIGNMENT
        ' ======================================

        Dim startY As Single = area.Top

        Select Case Me.TextAlign

            Case ContentAlignment.MiddleLeft,
                 ContentAlignment.MiddleCenter,
                 ContentAlignment.MiddleRight

                startY =
                    area.Top +
                    (area.Height - totalHeight) / 2.0F

            Case ContentAlignment.BottomLeft,
                 ContentAlignment.BottomCenter,
                 ContentAlignment.BottomRight

                startY =
                    area.Bottom - totalHeight

        End Select

        ' ======================================
        ' TEXT FORMAT
        ' ======================================

        Using format As New StringFormat(
            StringFormat.GenericTypographic
        )

            format.FormatFlags =
                format.FormatFlags Or
                StringFormatFlags.MeasureTrailingSpaces

            Using brush As New SolidBrush(Me.ForeColor)

                Dim previous As GraphicsState = g.Save()

                Try

                    g.SetClip(area)

                    For i As Integer = 0 To lines.Length - 1

                        Dim line As String = lines(i)

                        Dim lineWidth As Single =
                            MeasureLineWidth(
                                g,
                                line,
                                format
                            )

                        ' ==========================
                        ' HORIZONTAL ALIGNMENT
                        ' ==========================

                        Dim x As Single = area.Left

                        Select Case Me.TextAlign

                            Case ContentAlignment.TopCenter,
                                 ContentAlignment.MiddleCenter,
                                 ContentAlignment.BottomCenter

                                x =
                                    area.Left +
                                    (area.Width - lineWidth) / 2.0F

                            Case ContentAlignment.TopRight,
                                 ContentAlignment.MiddleRight,
                                 ContentAlignment.BottomRight

                                x =
                                    area.Right - lineWidth

                        End Select

                        ' ==========================
                        ' DRAW THE LINE
                        ' ==========================

                        DrawSpacedLine(
                            g,
                            line,
                            x,
                            startY + i * lineHeight,
                            brush,
                            format
                        )

                    Next

                Finally

                    g.Restore(previous)

                End Try

            End Using

        End Using

    End Sub

    ' ==========================================
    ' UPDATE WHEN TEXT CHANGES
    ' ==========================================

    Protected Overrides Sub OnTextChanged(
        e As EventArgs
    )

        MyBase.OnTextChanged(e)

        Me.Invalidate()

    End Sub

    ' ==========================================
    ' UPDATE WHEN FONT CHANGES
    ' ==========================================

    Protected Overrides Sub OnFontChanged(
        e As EventArgs
    )

        MyBase.OnFontChanged(e)

        Me.Invalidate()

    End Sub

    ' ==========================================
    ' UPDATE WHEN SIZE CHANGES
    ' ==========================================

    Protected Overrides Sub OnSizeChanged(
        e As EventArgs
    )

        MyBase.OnSizeChanged(e)

        Me.Invalidate()

    End Sub

End Class

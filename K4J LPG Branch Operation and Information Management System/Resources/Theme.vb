Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Runtime.InteropServices

' ===================== Colors, fonts, helpers =====================
Public Module Theme
    ' Global size multiplier for the whole interface (1.18 = 18% bigger). Change here to resize everything.
    Public UiScale As Single = 1.18F

    Public Function Sc(v As Integer) As Integer
        Return CInt(Math.Round(v * UiScale))
    End Function

    ' Scales every control under 'root' (positions, sizes, fonts, paddings). Children first so inherited fonts are read before they change.
    Public Sub ScaleUi(root As Control, includeRoot As Boolean)
        If Not TypeOf root Is DataGridView Then
            For Each c As Control In root.Controls
                ScaleUi(c, True)
            Next
        End If
        If Not includeRoot Then Return

        Dim fnt As Font = root.Font
        root.Font = New Font(fnt.FontFamily, fnt.Size * UiScale, fnt.Style, fnt.Unit)

        Dim b As Rectangle = root.Bounds
        Dim nb As New Rectangle(Sc(b.X), Sc(b.Y), Sc(b.Width), Sc(b.Height))
        Dim isAutoLabel As Boolean = (TypeOf root Is Label) AndAlso DirectCast(root, Label).AutoSize
        Dim isAutoHeight As Boolean = (TypeOf root Is TextBox) OrElse (TypeOf root Is ComboBox)
        Select Case root.Dock
            Case DockStyle.None
                If isAutoLabel Then
                    root.Location = nb.Location
                ElseIf isAutoHeight Then
                    root.Location = nb.Location
                    root.Width = nb.Width
                Else
                    root.Bounds = nb
                End If
            Case DockStyle.Left, DockStyle.Right
                root.Width = nb.Width
            Case DockStyle.Top, DockStyle.Bottom
                root.Height = nb.Height
        End Select

        Dim p As Padding = root.Padding
        root.Padding = New Padding(Sc(p.Left), Sc(p.Top), Sc(p.Right), Sc(p.Bottom))
        Dim m As Padding = root.Margin
        root.Margin = New Padding(Sc(m.Left), Sc(m.Top), Sc(m.Right), Sc(m.Bottom))

        If TypeOf root Is RoundedPanel Then
            Dim rp = DirectCast(root, RoundedPanel)
            rp.Radius = Sc(rp.Radius)
        ElseIf TypeOf root Is RoundButton Then
            Dim rb = DirectCast(root, RoundButton)
            rb.Radius = Sc(rb.Radius)
            rb.IconSize = rb.IconSize * UiScale
        ElseIf TypeOf root Is DataGridView Then
            Dim g = DirectCast(root, DataGridView)
            g.ColumnHeadersHeight = Sc(g.ColumnHeadersHeight)
            g.RowTemplate.Height = Sc(g.RowTemplate.Height)
            ScaleCellStyle(g.ColumnHeadersDefaultCellStyle)
            ScaleCellStyle(g.DefaultCellStyle)
        End If
    End Sub

    Private Sub ScaleCellStyle(st As DataGridViewCellStyle)
        If st.Font IsNot Nothing Then
            st.Font = New Font(st.Font.FontFamily, st.Font.Size * UiScale, st.Font.Style, st.Font.Unit)
        End If
        st.Padding = New Padding(Sc(st.Padding.Left), Sc(st.Padding.Top), Sc(st.Padding.Right), Sc(st.Padding.Bottom))
    End Sub

    Public Function Hx(hex As String) As Color
        Return ColorTranslator.FromHtml(hex)
    End Function

    Public ReadOnly Navy As Color = Hx("#0B1426")
    Public ReadOnly NavyLight As Color = Hx("#111D35")
    Public ReadOnly NavyLine As Color = Hx("#1B2740")
    Public ReadOnly Orange As Color = Hx("#E8670C")
    Public ReadOnly PageBg As Color = Hx("#EFF2F7")
    Public ReadOnly Ink As Color = Hx("#111827")
    Public ReadOnly Muted As Color = Hx("#6B7280")
    Public ReadOnly LineC As Color = Hx("#E5E7EB")
    Public ReadOnly BlueC As Color = Hx("#1D63D6")
    Public ReadOnly GreenC As Color = Hx("#059669")
    Public ReadOnly PurpleC As Color = Hx("#7C3AED")
    Public ReadOnly AmberC As Color = Hx("#F59E0B")
    Public ReadOnly RedC As Color = Hx("#DC2626")

    ' Special characters (kept as code points so the file encoding never matters)
    Public ReadOnly Peso As String = ChrW(&H20B1)
    Public ReadOnly EmDash As String = ChrW(&H2014)
    Public ReadOnly MidDot As String = ChrW(&HB7)
    Public ReadOnly CopyMark As String = ChrW(&HA9)
    Public ReadOnly ArrowR As String = ChrW(&H2192)
    Public ReadOnly BigDot As String = ChrW(&H25CF)

    Public Function Px(size As Single, Optional style As FontStyle = FontStyle.Regular) As Font
        Return New Font("Segoe UI", size, style, GraphicsUnit.Pixel)
    End Function

    Public Function Mono(size As Single, Optional style As FontStyle = FontStyle.Regular) As Font
        Return New Font("Consolas", size, style, GraphicsUnit.Pixel)
    End Function

    Public Function IconFont(size As Single) As Font
        Return New Font("Segoe MDL2 Assets", size, FontStyle.Regular, GraphicsUnit.Pixel)
    End Function

    ' Condensed heading font (falls back if not installed)
    Public Function Heading(size As Single) As Font
        Dim names As String() = {"Bahnschrift SemiBold Condensed", "Bahnschrift Condensed", "Arial Narrow", "Segoe UI"}
        Using fc As New InstalledFontCollection()
            For Each n In names
                For Each ff In fc.Families
                    If ff.Name = n Then
                        Dim st As FontStyle = If(n.StartsWith("Bahnschrift"), FontStyle.Regular, FontStyle.Bold)
                        Return New Font(n, size, st, GraphicsUnit.Pixel)
                    End If
                Next
            Next
        End Using
        Return New Font("Segoe UI", size, FontStyle.Bold, GraphicsUnit.Pixel)
    End Function

    Public Function RoundedPath(r As Rectangle, radius As Integer) As GraphicsPath
        Dim p As New GraphicsPath()
        Dim d As Integer = Math.Max(1, radius * 2)
        p.AddArc(r.X, r.Y, d, d, 180, 90)
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90)
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
        p.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
        p.CloseFigure()
        Return p
    End Function

    Public Function MakeLabel(text As String, x As Integer, y As Integer, f As Font, fore As Color) As Label
        Return New Label() With {.Text = text, .Location = New Point(x, y), .Font = f, .ForeColor = fore,
                                 .AutoSize = True, .BackColor = Color.Transparent}
    End Function

    Public Function MakePill(text As String, fill As Color, border As Color, fore As Color, f As Font,
                             Optional w As Integer = -1, Optional h As Integer = 20,
                             Optional align As ContentAlignment = ContentAlignment.MiddleCenter) As RoundedPanel
        Dim width As Integer = w
        If width <= 0 Then
            width = TextRenderer.MeasureText(text, f, New Size(2000, 100), TextFormatFlags.NoPadding).Width + 22
        End If
        Dim p As New RoundedPanel() With {.Size = New Size(width, h), .Radius = Math.Min(6, h \ 2),
                                          .FillColor = fill, .BorderColor = border}
        Dim padL As Integer = If(align = ContentAlignment.MiddleLeft, 7, 0)
        p.Controls.Add(New Label() With {.Text = text, .Font = f, .ForeColor = fore, .Dock = DockStyle.Fill,
                                         .TextAlign = align, .BackColor = Color.Transparent,
                                         .Padding = New Padding(padL, 0, 0, 0)})
        Return p
    End Function
End Module

' Segoe MDL2 Assets glyphs (built into Windows 10/11)
Public Module Glyphs
    Public ReadOnly Lock As String = ChrW(&HE72E)
    Public ReadOnly Eye As String = ChrW(&HE7B3)
    Public ReadOnly EyeOff As String = ChrW(&HED1A)
    Public ReadOnly CheckCircle As String = ChrW(&HE930)
    Public ReadOnly Home As String = ChrW(&HE80F)
    Public ReadOnly Grid As String = ChrW(&HE80A)
    Public ReadOnly Refresh As String = ChrW(&HE72C)
    Public ReadOnly Settings As String = ChrW(&HE713)
    Public ReadOnly People As String = ChrW(&HE716)
    Public ReadOnly Contact As String = ChrW(&HE77B)
    Public ReadOnly Shop As String = ChrW(&HE719)
    Public ReadOnly Cart As String = ChrW(&HE7BF)
    Public ReadOnly Download As String = ChrW(&HE896)
    Public ReadOnly Power As String = ChrW(&HE7E8)
    Public ReadOnly Clock As String = ChrW(&HE121)
    Public ReadOnly Bell As String = ChrW(&HEA8F)
    Public ReadOnly Warning As String = ChrW(&HE7BA)
    Public ReadOnly Pin As String = ChrW(&HE707)
    Public ReadOnly Chart As String = ChrW(&HE9D9)
    Public ReadOnly Flame As String = ChrW(&HD83D) & ChrW(&HDD25)
End Module

' ===================== Native helper (placeholder text) =====================
Public Class NativeHelper
    <DllImport("user32.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As String) As IntPtr
    End Function

    Public Shared Sub SetPlaceholder(tb As TextBox, text As String)
        If tb.IsHandleCreated Then
            SendMessage(tb.Handle, &H1501, New IntPtr(1), text)
        Else
            AddHandler tb.HandleCreated, Sub(s, e) SendMessage(tb.Handle, &H1501, New IntPtr(1), text)
        End If
    End Sub
End Class

' ===================== RoundedPanel =====================
Public Class RoundedPanel
    Inherits Panel

    Public Property Radius As Integer = 8
    Public Property FillColor As Color = Color.White
    Public Property BorderColor As Color = Hx("#E5E7EB")
    Public Property Shadow As Boolean = False

    Public Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or
                 ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim shape As Rectangle
        If Shadow Then
            For k As Integer = 4 To 1 Step -1
                Dim sr As New Rectangle(k - 1, k + 1, Width - 1 - 2 * (k - 1), Height - 6)
                Using sp = RoundedPath(sr, Radius + 1)
                    Using sb As New SolidBrush(Color.FromArgb(12, 15, 23, 42))
                        g.FillPath(sb, sp)
                    End Using
                End Using
            Next
            shape = New Rectangle(0, 0, Width - 1, Height - 6)
        Else
            shape = New Rectangle(0, 0, Width - 1, Height - 1)
        End If
        Using p = RoundedPath(shape, Radius)
            Using b As New SolidBrush(FillColor)
                g.FillPath(b, p)
            End Using
            Using pen As New Pen(BorderColor)
                g.DrawPath(pen, p)
            End Using
        End Using
    End Sub
End Class

' ===================== RoundButton =====================
Public Class RoundButton
    Inherits Control

    Public Property Radius As Integer = 8
    Public Property FillColor As Color = Hx("#E8670C")
    Public Property HoverColor As Color = Color.Empty
    Public Property BorderColor As Color = Color.Transparent
    Public Property TextColor As Color = Color.White
    Public Property IconGlyph As String = ""
    Public Property IconSize As Single = 14
    Public Property IconColor As Color = Color.Empty
    Public Property Stacked As Boolean = False
    Public Property LeftAlign As Boolean = False
    Private _hover As Boolean

    Public Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or
                 ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        Cursor = Cursors.Hand
        Font = Px(13, FontStyle.Bold)
    End Sub

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        _hover = True : Invalidate() : MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        _hover = False : Invalidate() : MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim fill As Color = FillColor
        If _hover AndAlso Not HoverColor.IsEmpty Then fill = HoverColor
        Using p = RoundedPath(New Rectangle(0, 0, Width - 1, Height - 1), Radius)
            Using b As New SolidBrush(fill)
                g.FillPath(b, p)
            End Using
            If BorderColor.A > 0 Then
                Using pen As New Pen(BorderColor)
                    g.DrawPath(pen, p)
                End Using
            End If
        End Using

        Dim flags As TextFormatFlags = TextFormatFlags.NoPadding Or TextFormatFlags.NoPrefix Or TextFormatFlags.SingleLine
        Dim big As New Size(Integer.MaxValue, Integer.MaxValue)
        Dim ic As Color = If(IconColor.IsEmpty, TextColor, IconColor)
        Dim hasIcon As Boolean = IconGlyph <> ""
        Dim hasText As Boolean = Text <> ""

        Using iconF = IconFont(IconSize)
            Dim isz As Size = If(hasIcon, TextRenderer.MeasureText(g, IconGlyph, iconF, big, flags), Size.Empty)
            Dim tsz As Size = If(hasText, TextRenderer.MeasureText(g, Text, Font, big, flags), Size.Empty)

            If Stacked Then
                Dim gap As Integer = Sc(4)
                Dim y0 As Integer = (Height - (isz.Height + gap + tsz.Height)) \ 2
                If hasIcon Then TextRenderer.DrawText(g, IconGlyph, iconF, New Point((Width - isz.Width) \ 2, y0), ic, flags)
                If hasText Then TextRenderer.DrawText(g, Text, Font, New Point((Width - tsz.Width) \ 2, y0 + isz.Height + gap), TextColor, flags)
            Else
                Dim iconW As Integer = If(LeftAlign, Sc(22), isz.Width)
                Dim gap As Integer = If(hasIcon AndAlso hasText, If(LeftAlign, Sc(6), Sc(8)), 0)
                Dim total As Integer = (If(hasIcon, iconW, 0)) + gap + tsz.Width
                Dim x0 As Integer = If(LeftAlign, Sc(12), (Width - total) \ 2)
                If hasIcon Then TextRenderer.DrawText(g, IconGlyph, iconF, New Point(x0, (Height - isz.Height) \ 2), ic, flags)
                If hasText Then TextRenderer.DrawText(g, Text, Font, New Point(x0 + If(hasIcon, iconW, 0) + gap, (Height - tsz.Height) \ 2), TextColor, flags)
            End If
        End Using
    End Sub
End Class

' ===================== Bar chart (daily sales trend) =====================
Public Class BarChartControl
    Inherits Control

    Private _labels As New List(Of String)
    Private _jpang As New List(Of Double)
    Private _daet As New List(Of Double)

    Public Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        BackColor = Color.White
    End Sub

    ''' <summary>Feed the chart. Lists must be the same length as labels (stacked bars: Jpang + Daet).</summary>
    Public Sub SetData(labels As IEnumerable(Of String), jpang As IEnumerable(Of Double), daet As IEnumerable(Of Double))
        _labels = New List(Of String)(labels)
        _jpang = New List(Of Double)(jpang)
        _daet = New List(Of Double)(daet)
        While _jpang.Count < _labels.Count : _jpang.Add(0) : End While
        While _daet.Count < _labels.Count : _daet.Add(0) : End While
        Invalidate()
    End Sub

    Public Sub ClearData()
        _labels.Clear() : _jpang.Clear() : _daet.Clear() : Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim plot As New Rectangle(Sc(54), Sc(14), Math.Max(10, Width - Sc(54) - Sc(24)), Math.Max(10, Height - Sc(14) - Sc(30)))
        Dim n As Integer = _labels.Count
        Dim maxV As Double = 28000
        Dim mx As Double = 0
        For i As Integer = 0 To n - 1
            mx = Math.Max(mx, _jpang(i) + _daet(i))
        Next
        If mx > maxV Then maxV = Math.Ceiling(mx / 4000) * 4000

        Using lf = Mono(10 * UiScale)
            Using gridPen As New Pen(Hx("#E5E7EB"))
                gridPen.DashStyle = DashStyle.Dash
                For i As Integer = 0 To 4
                    Dim yy As Integer = plot.Bottom - CInt(plot.Height * i / 4)
                    g.DrawLine(gridPen, plot.Left, yy, plot.Right, yy)
                    Dim lbl As String = Peso & CInt(maxV * i / 4 / 1000).ToString() & "k"
                    TextRenderer.DrawText(g, lbl, lf, New Rectangle(0, yy - Sc(8), plot.Left - Sc(6), Sc(16)), Hx("#9CA3AF"),
                                          TextFormatFlags.Right Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPadding)
                Next
            End Using

            If n > 0 Then
                Dim slot As Single = CSng(plot.Width) / n
                Dim bw As Single = Math.Min(12.0F * UiScale, slot * 0.45F)
                Dim stepN As Integer = Math.Max(1, CInt(Math.Ceiling(n * 40.0 * UiScale / plot.Width)))
                Using ba As New SolidBrush(Orange), bb As New SolidBrush(BlueC)
                    For i As Integer = 0 To n - 1
                        Dim cx As Single = plot.Left + slot * (i + 0.5F)
                        Dim hA As Single = CSng(plot.Height * _jpang(i) / maxV)
                        Dim hB As Single = CSng(plot.Height * _daet(i) / maxV)
                        g.FillRectangle(ba, cx - bw / 2, plot.Bottom - hA, bw, hA)
                        g.FillRectangle(bb, cx - bw / 2, plot.Bottom - hA - hB, bw, hB)
                        If i Mod stepN = 0 Then
                            TextRenderer.DrawText(g, _labels(i), lf, New Rectangle(CInt(cx) - Sc(30), plot.Bottom + Sc(6), Sc(60), Sc(16)),
                                                  Hx("#9CA3AF"), TextFormatFlags.HorizontalCenter Or TextFormatFlags.NoPadding)
                        End If
                    Next
                End Using
            Else
                ' Empty state: only axes + gridlines are drawn until data is supplied
            End If
        End Using
    End Sub
End Class

' ===================== Donut chart (branch split) =====================
Public Class DonutChartControl
    Inherits Control

    Private _jpang As Double
    Private _daet As Double

    Public Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        BackColor = Color.White
    End Sub

    Public Sub SetData(jpang As Double, daet As Double)
        _jpang = jpang : _daet = daet : Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim th As Integer = Sc(26)
        Dim d As Integer = Math.Min(Width, Height) - th - 4
        Dim rc As New Rectangle((Width - d) \ 2, (Height - d) \ 2, d, d)
        Using pen As New Pen(Hx("#EEF0F3"), th)
            g.DrawEllipse(pen, rc)
            Dim tot As Double = _jpang + _daet
            If tot > 0 Then
                Dim sw As Single = CSng(360 * _jpang / tot)
                pen.Color = Orange
                If sw > 0 Then g.DrawArc(pen, rc, -90.0F, sw)
                pen.Color = BlueC
                If sw < 360 Then g.DrawArc(pen, rc, -90.0F + sw, 360.0F - sw)
            End If
        End Using
    End Sub
End Class

' ===================== PasswordBox (no Caps Lock warning balloon) =====================
' Masks the text itself instead of using the Windows password style, so Windows has nothing to warn about.
Public Class PasswordBox
    Inherits TextBox

    Private Shared ReadOnly BulletChar As Char = ChrW(&H2022)
    Private _real As String = ""
    Private _shown As String = ""
    Private _busy As Boolean = False
    Private _reveal As Boolean = False

    ' The actual password typed by the user.
    Public ReadOnly Property Password As String
        Get
            Return _real
        End Get
    End Property

    ' True shows the characters, False shows dots.
    Public Property Reveal As Boolean
        Get
            Return _reveal
        End Get
        Set(value As Boolean)
            _reveal = value
            Redraw(SelectionStart)
        End Set
    End Property

    Private Sub Redraw(sel As Integer)
        _busy = True
        _shown = If(_reveal, _real, New String(BulletChar, _real.Length))
        MyBase.Text = _shown
        SelectionStart = Math.Min(sel, _shown.Length)
        _busy = False
    End Sub

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        If _busy Then Return
        Dim nt As String = MyBase.Text
        If nt <> _shown Then
            Dim p As Integer = 0
            Dim maxP As Integer = Math.Min(_shown.Length, nt.Length)
            While p < maxP AndAlso _shown(p) = nt(p)
                p += 1
            End While
            Dim q As Integer = 0
            Dim maxQ As Integer = Math.Min(_shown.Length, nt.Length) - p
            While q < maxQ AndAlso _shown(_shown.Length - 1 - q) = nt(nt.Length - 1 - q)
                q += 1
            End While
            Dim inserted As String = nt.Substring(p, nt.Length - p - q)
            _real = _real.Substring(0, p) & inserted & _real.Substring(_real.Length - q)
            Redraw(SelectionStart)
        End If
        MyBase.OnTextChanged(e)
    End Sub
End Class

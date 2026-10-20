Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

''' <summary>A DataGridView with configurable backgrounds and translucent cell separators.</summary>
<ToolboxItem(True)>
Public Class StyledDataGridView
    Inherits DataGridView

    Private separatorColor As Color = Color.FromArgb(229, 231, 235)
    Private separatorOpacity As Integer = 100
    Private separatorThickness As Integer = 1
    Private horizontalLines As Boolean = True
    Private verticalLines As Boolean

    Public Sub New()
        DoubleBuffered = True
        EnableHeadersVisualStyles = False
        BorderStyle = BorderStyle.None
        CellBorderStyle = DataGridViewCellBorderStyle.None
        ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        RowHeadersVisible = False
        AllowUserToAddRows = False
        BackgroundColor = Color.White
        DefaultCellStyle.BackColor = Color.White
        DefaultCellStyle.ForeColor = Color.FromArgb(54, 65, 83)
        DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255)
        DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42)
        ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251)
        ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(153, 161, 175)
        ColumnHeadersHeight = 36
        RowTemplate.Height = 48
    End Sub

    <Category("Table Appearance"),
     Description("Background of the column headings. Keep EnableHeadersVisualStyles set to False."),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property HeaderBackgroundColor As Color
        Get
            Return ColumnHeadersDefaultCellStyle.BackColor
        End Get
        Set(value As Color)
            EnableHeadersVisualStyles = False
            ColumnHeadersDefaultCellStyle.BackColor = value
            Invalidate()
        End Set
    End Property

    <Category("Table Appearance"),
     Description("Default row background and the empty area below the rows. Explicit cell or alternating-row styles take precedence."),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BodyBackgroundColor As Color
        Get
            Return DefaultCellStyle.BackColor
        End Get
        Set(value As Color)
            BackgroundColor = value
            DefaultCellStyle.BackColor = value
            Invalidate()
        End Set
    End Property

    <Category("Table Lines"),
     Description("Color of the header and body separators."),
     DefaultValue(GetType(Color), "229, 231, 235")>
    Public Property LineColor As Color
        Get
            Return separatorColor
        End Get
        Set(value As Color)
            separatorColor = value
            Invalidate()
        End Set
    End Property

    <Category("Table Lines"), DefaultValue(100),
     Description("Separator opacity from 0 (invisible) to 100 (opaque).")>
    Public Property LineOpacity As Integer
        Get
            Return separatorOpacity
        End Get
        Set(value As Integer)
            separatorOpacity = Math.Clamp(value, 0, 100)
            Invalidate()
        End Set
    End Property

    <Category("Table Lines"), DefaultValue(1),
     Description("Separator thickness in logical pixels, from 0 to 10. Scales with display DPI.")>
    Public Property LineThickness As Integer
        Get
            Return separatorThickness
        End Get
        Set(value As Integer)
            separatorThickness = Math.Clamp(value, 0, 10)
            Invalidate()
        End Set
    End Property

    <Category("Table Lines"), DefaultValue(True)>
    Public Property ShowHorizontalLines As Boolean
        Get
            Return horizontalLines
        End Get
        Set(value As Boolean)
            horizontalLines = value
            Invalidate()
        End Set
    End Property

    <Category("Table Lines"), DefaultValue(False)>
    Public Property ShowVerticalLines As Boolean
        Get
            Return verticalLines
        End Get
        Set(value As Boolean)
            verticalLines = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnCellPainting(e As DataGridViewCellPaintingEventArgs)
        MyBase.OnCellPainting(e)
        If e.Handled Then Return

        ' Keep native text, images, selection, sorting glyphs and focus rendering.
        e.Paint(e.ClipBounds, e.PaintParts And Not DataGridViewPaintParts.Border)
        e.Handled = True
        If separatorThickness = 0 OrElse separatorOpacity = 0 Then Return
        If Not horizontalLines AndAlso Not verticalLines Then Return

        Dim thickness = Math.Max(1, CInt(separatorThickness * DeviceDpi / 96.0R))
        Dim bounds = e.CellBounds
        Dim h = Math.Min(thickness, bounds.Height)
        Dim w = Math.Min(thickness, bounds.Width)
        Dim state = e.Graphics.Save()
        Try
            e.Graphics.SetClip(e.ClipBounds, Drawing2D.CombineMode.Intersect)
            Using brush As New SolidBrush(Color.FromArgb(
                CInt(separatorColor.A * separatorOpacity / 100.0R), separatorColor))
                If horizontalLines Then
                    e.Graphics.FillRectangle(brush, bounds.Left, bounds.Bottom - h, bounds.Width, h)
                End If
                If verticalLines Then
                    ' Avoid painting intersections twice, which would increase opacity.
                    e.Graphics.FillRectangle(brush, bounds.Right - w, bounds.Top,
                                             w, Math.Max(0, bounds.Height - If(horizontalLines, h, 0)))
                End If
            End Using
        Finally
            e.Graphics.Restore(state)
        End Try
    End Sub
End Class

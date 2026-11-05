Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Runtime.InteropServices

<ToolboxItem(False)>
Public Class EmployeeAccountsGrid
    Inherits StyledDataGridView

    ' WinForms measures fonts with GDI+ and draws cell text with GDI.
    Private Class GridTypeface
        Private ReadOnly families As New PrivateFontCollection()
        Private ReadOnly pinnedBytes As GCHandle
        Public ReadOnly Face As Font

        <DllImport("gdi32.dll", ExactSpelling:=True)>
        Private Shared Function AddFontMemResourceEx(address As IntPtr, length As UInteger,
                reserved As IntPtr, ByRef count As UInteger) As IntPtr
        End Function

        Public Sub New(resourceName As String, size As Single)
            Using stream = GetType(EmployeeAccountsGrid).Assembly.GetManifestResourceStream(resourceName)
                Dim bytes(CInt(stream.Length) - 1) As Byte
                stream.ReadExactly(bytes)
                pinnedBytes = GCHandle.Alloc(bytes, GCHandleType.Pinned)
                Dim address = pinnedBytes.AddrOfPinnedObject()
                Dim count As UInteger
                AddFontMemResourceEx(address, CUInt(bytes.Length), IntPtr.Zero, count)
                families.AddMemoryFont(address, bytes.Length)
            End Using
            Face = New Font(families.Families(0), size, FontStyle.Regular, GraphicsUnit.Pixel)
        End Sub
    End Class

    Private Shared ReadOnly medium As New GridTypeface("Inter-Medium.ttf", 12)
    Private Shared ReadOnly semibold As New GridTypeface("Inter-SemiBold.ttf", 10)

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        DefaultCellStyle.Font = medium.Face
        ColumnHeadersDefaultCellStyle.Font = semibold.Face
        RoundCorners()
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        If IsHandleCreated Then RoundCorners()
    End Sub

    Private Sub RoundCorners()
        If Width < 2 OrElse Height < 2 Then Return
        Dim radius = Math.Min(8 * DeviceDpi / 96.0F, Math.Min(Width, Height) / 2.0F)
        Using outline As New GraphicsPath()
            outline.AddRoundedRectangle(New RectangleF(0, 0, Width, Height), New SizeF(radius, radius))
            Region = New Region(outline)
        End Using
    End Sub

    Protected Overrides Sub OnCellPainting(e As DataGridViewCellPaintingEventArgs)
        If e.RowIndex <> -1 OrElse e.ColumnIndex < 0 Then
            MyBase.OnCellPainting(e)
            Return
        End If

        ' The native header reserves space for a sort arrow even on unsorted columns.
        e.PaintBackground(e.ClipBounds, False)
        Dim bounds = e.CellBounds
        Dim order = Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection
        Dim labelBounds = bounds
        If order <> SortOrder.None Then
            Dim arrow = Math.Max(2, CInt(3 * DeviceDpi / 96.0F))
            Dim x = bounds.Right - 2 * arrow - 2, y = bounds.Top + bounds.Height \ 2
            Dim direction = If(order = SortOrder.Ascending, 1, -1)
            Using brush As New SolidBrush(e.CellStyle.ForeColor)
                e.Graphics.FillPolygon(brush, {New Point(x - arrow, y + direction * arrow),
                    New Point(x + arrow, y + direction * arrow), New Point(x, y - direction * arrow)})
            End Using
            labelBounds.Inflate(-(3 * arrow + 2), 0)
        End If
        TextRenderer.DrawText(e.Graphics, Columns(e.ColumnIndex).HeaderText, e.CellStyle.Font,
            labelBounds, e.CellStyle.ForeColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or
            TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPrefix)
        Using line As New Pen(LineColor)
            e.Graphics.DrawLine(line, bounds.Left, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1)
        End Using
        e.Handled = True
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        ' The root form's Load handler does not run on the Visual Studio design surface.
        If DesignMode AndAlso Rows.Count = 0 AndAlso (Columns.Count = 9 OrElse Columns.Count = 8) AndAlso
            Columns.Contains("colActions") AndAlso DataSource Is Nothing Then
            If Columns.Contains("colToday") Then
                Rows.Add("R-001", "Mark Andrie Baylon" & Environment.NewLine & "Joined 2022-06-01", "0917-111-2222", "Daet", 4, 312, "Active", "✎")
                Rows.Add("R-002", "Jacob M. Pepino" & Environment.NewLine & "Joined 2022-09-15", "0928-222-3333", "Jpang", 3, 278, "Active", "✎")
            Else
                Rows.Add("ADM-01", "Alex Santos" & Environment.NewLine & "Joined 2022-06-01", "alex@example.com", "0917-555-0101", "JPang Branch", "JPang Branch Admin", "Jul 24, 2025", "Active", "✎")
                Rows.Add("ADM-02", "Sam Reyes" & Environment.NewLine & "Joined 2022-09-15", "sam@example.com", "0928-555-0102", "Daet Branch", "Daet Branch Admin", "Jul 23, 2025", "Active", "✎")
            End If
            ClearSelection()
        End If
        MyBase.OnPaint(e)
    End Sub
End Class

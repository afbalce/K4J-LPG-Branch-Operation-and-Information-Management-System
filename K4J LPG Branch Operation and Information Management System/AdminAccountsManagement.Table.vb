Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Partial Public Class AdminAccountsManagement
    Private Shared ReadOnly ink As Color = Color.FromArgb(30, 41, 59)
    Private Shared ReadOnly muted As Color = Color.FromArgb(100, 116, 139)

    Private clickedAccount As DataRow

    Private Sub HighlightClickedAccount(sender As Object, e As DataGridViewCellMouseEventArgs) Handles StyledDataGridView1.CellMouseDown
        If e.Button <> MouseButtons.Left OrElse e.RowIndex < 0 Then Return
        Dim item = TryCast(StyledDataGridView1.Rows(e.RowIndex).DataBoundItem, DataRowView)
        clickedAccount = item?.Row
        StyledDataGridView1.Invalidate()
    End Sub

    Private Sub FormatAccount(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles StyledDataGridView1.CellFormatting
        If e.RowIndex < 0 Then Return
        Dim item = TryCast(StyledDataGridView1.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If clickedAccount IsNot Nothing AndAlso item?.Row Is clickedAccount Then
            e.CellStyle.BackColor = Color.FromArgb(241, 242, 244)
            e.CellStyle.SelectionBackColor = e.CellStyle.BackColor
        End If
        Dim column = StyledDataGridView1.Columns(e.ColumnIndex)
        If column Is colID AndAlso IsNumeric(e.Value) Then
            e.Value = If(AccountRole = "Rider", "R-" & CInt(e.Value).ToString("000"), "ADM-" & CInt(e.Value).ToString("00"))
            e.FormattingApplied = True
        ElseIf column Is colName AndAlso item IsNot Nothing Then
            If TypeOf item("Hired") Is DateTime Then e.Value = Convert.ToString(e.Value) & Environment.NewLine & "Joined " & CDate(item("Hired")).ToString("yyyy-MM-dd")
            e.FormattingApplied = True
        ElseIf column Is colRole AndAlso item IsNot Nothing Then
            Dim branch = DisplayBranchName(item("Branch"))
            If branch.Length > 0 Then
                e.Value = branch & If(branch.EndsWith("Branch", StringComparison.OrdinalIgnoreCase), " Admin", " Branch Admin")
                e.FormattingApplied = True
            End If
        ElseIf column Is colBranch Then
            Dim branch = DisplayBranchName(e.Value)
            If AccountRole = "Rider" AndAlso branch.EndsWith(" Branch", StringComparison.OrdinalIgnoreCase) Then branch = branch.Substring(0, branch.Length - 7)
            e.Value = branch
            e.FormattingApplied = True
        ElseIf column Is colEmail AndAlso String.IsNullOrWhiteSpace(Convert.ToString(e.Value)) Then
            e.Value = "—"
            e.FormattingApplied = True
        ElseIf column Is colLastLogin Then
            e.Value = If(TypeOf e.Value Is DateTime, CDate(e.Value).ToString("MMM dd, yyyy") & Environment.NewLine & CDate(e.Value).ToString("hh:mm tt"), "Never")
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub DrawAccountCell(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles StyledDataGridView1.CellPainting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        Dim column = StyledDataGridView1.Columns(e.ColumnIndex)
        If column IsNot colName AndAlso column IsNot colBranch AndAlso column IsNot colStatus AndAlso column IsNot colActions Then Return
        Dim g = e.Graphics, b = e.CellBounds
        Dim state = g.Save()
        Try
            g.SetClip(Rectangle.Intersect(b, e.ClipBounds), CombineMode.Intersect)
            e.PaintBackground(e.ClipBounds, True)
            g.SmoothingMode = SmoothingMode.AntiAlias
            Dim s = DeviceDpi / 96.0F
            Dim pad = CInt(4 * s), cy = b.Top + b.Height \ 2
            Dim value = Convert.ToString(e.FormattedValue)
            Dim flags = TextFormatFlags.VerticalCenter Or TextFormatFlags.SingleLine Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPrefix
            If column Is colName Then
                Dim size = Math.Min(CInt(22 * s), b.Height - 4)
                Dim lines = value.Split({Environment.NewLine}, 2, StringSplitOptions.None)
                If lines.Length = 2 Then
                    Dim textLeft = b.Left + e.CellStyle.Padding.Left
                    Dim textWidth = Math.Max(1, b.Right - textLeft - pad)
                    Dim wrap = TextFormatFlags.WordBreak Or TextFormatFlags.NoPrefix
                    Using dateFont As New Font(e.CellStyle.Font.FontFamily, e.CellStyle.Font.Size * 0.85F, FontStyle.Regular, e.CellStyle.Font.Unit)
                        Dim nameHeight = TextRenderer.MeasureText(g, lines(0), e.CellStyle.Font, New Drawing.Size(textWidth, Integer.MaxValue), wrap).Height
                        Dim top = Math.Max(b.Top, cy - (nameHeight + dateFont.Height) \ 2)
                        TextRenderer.DrawText(g, lines(0), e.CellStyle.Font, New Rectangle(textLeft, top, textWidth, nameHeight), ink, wrap)
                        TextRenderer.DrawText(g, lines(1), dateFont, New Rectangle(textLeft, top + nameHeight, textWidth, dateFont.Height + 2), muted, flags)
                    End Using
                Else
                    e.PaintContent(e.ClipBounds)
                End If
                Dim tile As New Rectangle(b.Left + pad, cy - size \ 2, size, size)
                PaintPill(g, tile, If(AccountRole = "Rider", Color.FromArgb(209, 250, 229), Color.FromArgb(219, 234, 254)), Color.Transparent, 6 * s)
                TextRenderer.DrawText(g, If(value.Length > 0, value.Substring(0, 1).ToUpperInvariant(), ""), e.CellStyle.Font, tile, If(AccountRole = "Rider", Color.SeaGreen, Color.RoyalBlue), flags Or TextFormatFlags.HorizontalCenter)
            ElseIf column Is colActions Then
                Dim middle = b.Left + b.Width \ 2
                Dim x = b.Left + b.Width \ 4, tx = middle + b.Width \ 4
                Dim d = Math.Max(3, CInt(5 * s))
                Using pen As New Pen(muted, Math.Max(1.2F, s))
                    g.DrawPolygon(pen, {New Point(x-d,cy+d), New Point(x-d+1,cy), New Point(x+d-2,cy-d), New Point(x+d,cy-d+2), New Point(x,cy+d-1)})
                    pen.Color = Color.IndianRed
                    g.DrawRectangle(pen, tx-d+1, cy-d+2, 2*d-2, 2*d-1)
                    g.DrawLine(pen, tx-d-1, cy-d, tx+d+1, cy-d)
                    g.DrawLine(pen, tx-2, cy-d-2, tx+2, cy-d-2)
                End Using
            Else
                Dim fill = Color.FromArgb(239, 246, 255), edge = Color.FromArgb(191, 219, 254), fg = Color.RoyalBlue
                If column Is colStatus Then
                    Dim active = value = "Active"
                    fill = If(active, Color.Honeydew, Color.FromArgb(248, 250, 252))
                    edge = If(active, Color.FromArgb(110, 231, 183), Color.LightGray)
                    fg = If(active, Color.SeaGreen, muted)
                ElseIf value.IndexOf("Daet", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    fill = Color.FromArgb(245, 243, 255)
                    fg = Color.FromArgb(109, 40, 217)
                    edge = Color.FromArgb(221, 214, 254)
                End If
                Dim available = Math.Max(1, b.Width - 2 * pad)
                Dim badgeWidth = Math.Min(available, TextRenderer.MeasureText(value, e.CellStyle.Font).Width + 2 * pad)
                Dim badgeFlags = flags Or TextFormatFlags.HorizontalCenter
                Dim h = CInt(21 * s)
                If column Is colBranch Then
                    badgeFlags = TextFormatFlags.WordBreak Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix
                    Dim measured = TextRenderer.MeasureText(g, value, e.CellStyle.Font, New Size(badgeWidth, Integer.MaxValue), badgeFlags)
                    h = Math.Min(b.Height - 2, Math.Max(h, measured.Height + pad))
                End If
                Dim badge As New Rectangle(b.Left + (b.Width - badgeWidth) \ 2, cy - h \ 2, badgeWidth, h)
                PaintPill(g, badge, fill, edge, 3 * s)
                TextRenderer.DrawText(g, value, e.CellStyle.Font, badge, fg, badgeFlags)
            End If
            Using line As New Pen(Color.FromArgb(229, 231, 235))
                g.DrawLine(line, b.Left, b.Bottom - 1, b.Right, b.Bottom - 1)
            End Using
            e.Handled = True
        Finally
            g.Restore(state)
        End Try
    End Sub

    Private Shared Sub PaintPill(g As Graphics, b As Rectangle, fill As Color, edge As Color, r As Single)
        Dim d = Math.Min(2 * r, Math.Min(b.Height-1, b.Width-1))
        If d <= 0 Then Return
        Using path As New GraphicsPath(), brush As New SolidBrush(fill), pen As New Pen(edge)
            path.AddArc(b.Left, b.Top, d, d, 180, 90)
            path.AddArc(b.Right-d-1, b.Top, d, d, 270, 90)
            path.AddArc(b.Right-d-1, b.Bottom-d-1, d, d, 0, 90)
            path.AddArc(b.Left, b.Bottom-d-1, d, d, 90, 90)
            path.CloseFigure()
            g.FillPath(brush, path)
            If edge.A > 0 Then g.DrawPath(pen, path)
        End Using
    End Sub

    Private Sub RowAction(sender As Object, e As DataGridViewCellMouseEventArgs) Handles StyledDataGridView1.CellMouseClick
        If e.RowIndex < 0 OrElse e.ColumnIndex <> colActions.Index OrElse e.Button <> MouseButtons.Left Then Return
        Dim row = TryCast(StyledDataGridView1.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If row Is Nothing Then Return
        Dim id = CInt(row("ID")), name = CStr(row("Name"))
        If e.X < colActions.Width \ 2 Then
            Dim menu As New ContextMenuStrip()
            For Each status In {"Active", "On Leave", "Resigned", "Terminated"}
                Dim selectedStatus = status
                Dim item = DirectCast(menu.Items.Add(status, Nothing, Sub() ApplyAccountAction(id, selectedStatus)), ToolStripMenuItem)
                item.Checked = status = CStr(row("Status"))
            Next
            AddHandler menu.Closed, Sub() menu.Dispose()
            menu.Show(Cursor.Position)
        ElseIf MessageBox.Show(Me, "Delete the account for " & name & "?", "Delete account", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            ApplyAccountAction(id, Nothing)
        End If
    End Sub

    Private Sub ApplyAccountAction(id As Integer, status As String)
        Try
            Using db = EmployeeData.Connect()
                db.Open()
                Using tx = db.BeginTransaction(), cmd = db.CreateCommand()
                    cmd.Transaction = tx
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.Parameters.AddWithValue("@role", AccountRole)
                    If status Is Nothing Then
                        cmd.CommandText = "DELETE p FROM tbl_employee_profile p JOIN tbl_user u ON u.user_id=p.user_id WHERE u.user_id=@id AND u.role=@role"
                        cmd.ExecuteNonQuery()
                        cmd.CommandText = "DELETE FROM tbl_user WHERE user_id=@id AND role=@role"
                    Else
                        cmd.Parameters.AddWithValue("@status", status)
                        cmd.CommandText = "UPDATE tbl_user SET employment_status=@status WHERE user_id=@id AND role=@role"
                    End If
                    cmd.ExecuteNonQuery()
                    tx.Commit()
                End Using
            End Using
            ReloadAccounts()
        Catch ex As MySqlException When ex.Number = 1451
            MessageBox.Show(Me, "This account is linked to other records. Change its status instead of deleting it.")
        Catch ex As MySqlException
            MessageBox.Show(Me, "Could not update the account: " & ex.Message)
        End Try
    End Sub
End Class

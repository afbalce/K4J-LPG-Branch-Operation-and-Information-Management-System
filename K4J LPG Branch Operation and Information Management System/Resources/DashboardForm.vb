' Super Admin dashboard. No data is loaded: every number/chart/table is empty until the Public Set*/Add* methods are called.
Public Class DashboardForm
    Inherits Form

    ' ---- Events for the future backend ----
    Public Event NavigateRequested(page As String)
    Public Event QuickActionClicked(action As String)
    Public Event BranchFilterChanged(branch As String)   ' "Both", "Jpang", "Daet"
    Public Event RefreshRequested()

    Private ReadOnly _acc As UserAccount
    Private ReadOnly content As New Panel()
    Private ReadOnly page As New Panel()
    Private ReadOnly lblSubtitle As New Label()
    Private ReadOnly lblTrendSub As New Label()
    Private ReadOnly lblDate As New Label()
    Private ReadOnly statValues As New List(Of Label)
    Private ReadOnly statNotes As New List(Of Label)
    Private ReadOnly salesChart As New BarChartControl()
    Private ReadOnly donut As New DonutChartControl()
    Private ReadOnly lblJpangAmt As New Label()
    Private ReadOnly lblDaetAmt As New Label()
    Private ReadOnly fillJpang As New Panel()
    Private ReadOnly fillDaet As New Panel()
    Private ReadOnly grid As New DataGridView()
    Private ReadOnly alertsHost As New FlowLayoutPanel()
    Private ReadOnly lblAlertCount As New Label()
    Private ReadOnly segButtons As New List(Of RoundButton)
    Private ReadOnly navButtons As New List(Of RoundButton)
    Private ReadOnly clock As New System.Windows.Forms.Timer()
    Private _scope As String = "Both"
    Private _alertCount As Integer = 0
    Private Const TrackW As Integer = 299

    Public Sub New(acc As UserAccount)
        _acc = acc
        Text = "K4J LPG Center - Super Admin Dashboard"
        ClientSize = New Size(1280, 800)
        MinimumSize = New Size(1100, 600)
        StartPosition = FormStartPosition.CenterScreen
        WindowState = FormWindowState.Maximized
        BackColor = PageBg
        Font = Px(13)
        DoubleBuffered = True

        content.Dock = DockStyle.Fill
        content.BackColor = PageBg
        content.AutoScroll = True
        content.AutoScrollMinSize = New Size(1060, 1075)

        page.Size = New Size(1060, 1075)
        page.BackColor = PageBg
        content.Controls.Add(page)
        AddHandler content.Resize, Sub(s, e) CenterPage()

        Dim topbar = BuildTopbar()
        Dim side = BuildSidebar()
        BuildContent()

        ' Add order matters for docking: Fill first, then Top, then Left
        Controls.Add(content)
        Controls.Add(topbar)
        Controls.Add(side)

        UpdateClock()
        AddHandler clock.Tick, Sub(s, e) UpdateClock()
        clock.Interval = 30000
        clock.Start()
        UpdateSubtitles()

        ScaleUi(Me, False)
        content.AutoScrollMinSize = New Size(Sc(1060), Sc(1075))
    End Sub

    Private Sub CenterPage()
        page.Left = Math.Max(0, (content.ClientSize.Width - page.Width) \ 2)
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        clock.Stop()
        MyBase.OnFormClosed(e)
    End Sub

    ' =====================================================================
    '  PUBLIC API - call these once the backend exists
    ' =====================================================================
    Public Sub SetStats(totalSalesToday As String, totalKgSold As String, activeCustomers As String, pendingDeliveries As String)
        statValues(0).Text = totalSalesToday
        statValues(1).Text = totalKgSold
        statValues(2).Text = activeCustomers
        statValues(3).Text = pendingDeliveries
    End Sub

    ''' <summary>Optional text under a stat, e.g. SetStatNote(0, "+12.4% vs yesterday").</summary>
    Public Sub SetStatNote(index As Integer, note As String)
        statNotes(index).Text = note
    End Sub

    Public Sub SetSalesTrend(labels As IEnumerable(Of String), jpang As IEnumerable(Of Double), daet As IEnumerable(Of Double))
        salesChart.SetData(labels, jpang, daet)
    End Sub

    Public Sub SetBranchSplit(jpangSales As Double, daetSales As Double)
        donut.SetData(jpangSales, daetSales)
        Dim tot As Double = jpangSales + daetSales
        lblJpangAmt.Text = Peso & jpangSales.ToString("N0")
        lblDaetAmt.Text = Peso & daetSales.ToString("N0")
        Dim tw As Integer = fillJpang.Parent.Width
        fillJpang.Width = If(tot > 0, CInt(tw * jpangSales / tot), 0)
        fillDaet.Width = If(tot > 0, CInt(tw * daetSales / tot), 0)
    End Sub

    Public Sub AddTransaction(txnId As String, customer As String, branch As String, product As String,
                              amount As String, rider As String, status As String, payment As String)
        grid.Rows.Add(txnId, customer, branch, product, amount, rider, status, payment)
    End Sub

    Public Sub ClearTransactions()
        grid.Rows.Clear()
    End Sub

    Public Sub AddInventoryAlert(title As String, detail As String, isCritical As Boolean)
        Dim fill As Color = If(isCritical, Hx("#FEECEC"), Hx("#FEF8DC"))
        Dim border As Color = If(isCritical, Hx("#F7C6C6"), Hx("#F3E3A0"))
        Dim iconC As Color = If(isCritical, RedC, AmberC)
        Dim p As New RoundedPanel() With {.Size = New Size(TrackW, 48), .Radius = 6, .FillColor = fill,
                                          .BorderColor = border, .Margin = New Padding(0, 0, 0, 8)}
        p.Controls.Add(MakeLabel(Glyphs.Warning, 12, 16, IconFont(14), iconC))
        p.Controls.Add(MakeLabel(title, 34, 8, Px(12, FontStyle.Bold), Ink))
        p.Controls.Add(MakeLabel(detail, 34, 26, Px(11), Muted))
        ScaleUi(p, True)
        alertsHost.Controls.Add(p)
        _alertCount += 1
        lblAlertCount.Text = _alertCount.ToString()
    End Sub

    Public Sub ClearInventoryAlerts()
        alertsHost.Controls.Clear()
        _alertCount = 0
        lblAlertCount.Text = "0"
    End Sub

    ' =====================================================================
    '  TOP BAR
    ' =====================================================================
    Private Function BuildTopbar() As Panel
        Dim bar As New Panel() With {.Dock = DockStyle.Top, .Height = 48, .BackColor = Color.White}
        AddHandler bar.Paint, Sub(s, e)
                                  Using pen As New Pen(LineC)
                                      e.Graphics.DrawLine(pen, 0, bar.Height - 1, bar.Width, bar.Height - 1)
                                  End Using
                              End Sub

        Dim crumbFont = Px(12)
        Dim l1 = MakeLabel("Super Admin /", 21, 15, crumbFont, Hx("#9CA3AF"))
        Dim wCrumb As Integer = TextRenderer.MeasureText("Super Admin /", crumbFont, New Size(500, 30), TextFormatFlags.NoPadding).Width
        Dim l2 = MakeLabel("Dashboard", 21 + wCrumb + 6, 15, Px(12, FontStyle.Bold), Ink)
        bar.Controls.Add(l1)
        bar.Controls.Add(l2)

        Dim chip As New RoundedPanel() With {.Size = New Size(181, 26), .Radius = 6, .BorderColor = LineC}
        chip.Location = New Point(1060 - 181 - 60, 11)
        chip.Controls.Add(MakeLabel(Glyphs.Clock, 10, 6, IconFont(13), Muted))
        lblDate.Font = Mono(11)
        lblDate.ForeColor = Muted
        lblDate.AutoSize = True
        lblDate.BackColor = Color.Transparent
        lblDate.Location = New Point(30, 5)
        chip.Controls.Add(lblDate)
        bar.Controls.Add(chip)

        Dim bell = MakeLabel(Glyphs.Bell, 1060 - 34, 14, IconFont(16), Hx("#374151"))
        bell.Cursor = Cursors.Hand
        bar.Controls.Add(bell)
        AddHandler bar.Resize, Sub(s, e)
                                   chip.Left = bar.Width - chip.Width - Sc(60)
                                   bell.Left = bar.Width - Sc(34)
                               End Sub
        Return bar
    End Function

    Private Sub UpdateClock()
        lblDate.Text = DateTime.Now.ToString("MMM d, yyyy") & " " & MidDot & " " & DateTime.Now.ToString("hh:mm tt")
    End Sub

    ' =====================================================================
    '  SIDEBAR
    ' =====================================================================
    Private Function BuildSidebar() As Panel
        Dim side As New Panel() With {.Dock = DockStyle.Left, .Width = 220, .BackColor = Navy}

        ' Logo + title
        Dim logo As New RoundedPanel() With {.Bounds = New Rectangle(14, 14, 30, 30), .Radius = 8, .FillColor = Orange, .BorderColor = Orange}
        logo.Controls.Add(New Label() With {.Text = Glyphs.Flame, .Font = New Font("Segoe UI Emoji", 14, FontStyle.Regular, GraphicsUnit.Pixel),
                                            .ForeColor = Color.White, .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter,
                                            .BackColor = Color.Transparent})
        side.Controls.Add(logo)
        side.Controls.Add(MakeLabel("K4J LPG CENTER", 52, 14, Px(13, FontStyle.Bold), Color.White))
        side.Controls.Add(MakeLabel("All Branches", 52, 31, Px(11), Hx("#7F8FAE")))
        side.Controls.Add(New Panel() With {.Bounds = New Rectangle(0, 58, 220, 1), .BackColor = NavyLine})

        ' User card
        Dim uc As New RoundedPanel() With {.Bounds = New Rectangle(11, 70, 198, 54), .Radius = 10,
                                           .FillColor = NavyLight, .BorderColor = Hx("#243252")}
        Dim av As New RoundedPanel() With {.Bounds = New Rectangle(10, 13, 28, 28), .Radius = 7, .FillColor = Orange, .BorderColor = Orange}
        av.Controls.Add(New Label() With {.Text = Initials(_acc.FullName), .Font = Px(11, FontStyle.Bold), .ForeColor = Color.White,
                                          .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter, .BackColor = Color.Transparent})
        uc.Controls.Add(av)
        uc.Controls.Add(MakeLabel(_acc.FullName, 46, 9, Px(12, FontStyle.Bold), Color.White))
        Dim rolePill = MakePill(_acc.Role, Orange, Orange, Color.White, Px(10, FontStyle.Bold), -1, 16)
        rolePill.Location = New Point(46, 29)
        uc.Controls.Add(rolePill)
        side.Controls.Add(uc)

        ' Navigation
        Dim y As Integer = 142
        AddNavSection(side, y, "OVERVIEW", {"Dashboard|" & Glyphs.Grid})
        AddNavSection(side, y, "PERSONNEL", {"Admin Accounts|" & Glyphs.Contact, "Rider Accounts|" & Glyphs.People})
        AddNavSection(side, y, "CATALOG", {"Products|" & Glyphs.Shop, "Global Inventory|" & Glyphs.Cart})
        AddNavSection(side, y, "OPERATIONS", {"Customers|" & Glyphs.People, "Deliveries|" & Glyphs.Pin})
        AddNavSection(side, y, "FINANCE", {"EOD Sales|" & Peso, "Reports|" & Glyphs.Chart})
        AddNavSection(side, y, "SYSTEM", {"Settings|" & Glyphs.Settings})
        SetActiveNav("Dashboard")

        ' Sign out (bottom)
        Dim bottom As New Panel() With {.Dock = DockStyle.Bottom, .Height = 52, .BackColor = Navy}
        AddHandler bottom.Paint, Sub(s, e)
                                     Using pen As New Pen(NavyLine)
                                         e.Graphics.DrawLine(pen, 0, 0, bottom.Width, 0)
                                     End Using
                                 End Sub
        Dim so As New RoundButton() With {.Text = "Sign Out", .IconGlyph = Glyphs.Power, .IconSize = 14, .LeftAlign = True,
                                          .Font = Px(12, FontStyle.Bold), .FillColor = Color.Transparent,
                                          .HoverColor = NavyLight, .TextColor = Hx("#B6C0D4"), .Radius = 6,
                                          .Bounds = New Rectangle(11, 11, 198, 30)}
        AddHandler so.Click, Sub(s, e) DialogResult = DialogResult.Retry
        bottom.Controls.Add(so)
        side.Controls.Add(bottom)
        Return side
    End Function

    Private Sub AddNavSection(side As Panel, ByRef y As Integer, header As String, items As String())
        side.Controls.Add(MakeLabel(header, 17, y, Px(10, FontStyle.Bold), Hx("#5B6B8C")))
        y += 25
        For Each it In items
            Dim parts = it.Split("|"c)
            Dim page As String = parts(0)
            Dim b As New RoundButton() With {.Text = page, .IconGlyph = parts(1), .IconSize = 14, .LeftAlign = True,
                                             .Font = Px(12, FontStyle.Bold), .FillColor = Color.Transparent,
                                             .HoverColor = NavyLight, .TextColor = Hx("#B6C0D4"), .Radius = 6,
                                             .Tag = page, .Bounds = New Rectangle(11, y, 198, 30)}
            If parts(1) = Peso Then b.IconSize = 15
            AddHandler b.Click, Sub(s, e)
                                    SetActiveNav(page)
                                    If page <> "Dashboard" Then RaiseEvent NavigateRequested(page)
                                End Sub
            navButtons.Add(b)
            side.Controls.Add(b)
            y += 30
        Next
        y += 1
    End Sub

    Private Sub SetActiveNav(page As String)
        For Each b In navButtons
            Dim active As Boolean = CStr(b.Tag) = page
            b.FillColor = If(active, Orange, Color.Transparent)
            b.HoverColor = If(active, Orange, NavyLight)
            b.TextColor = If(active, Color.White, Hx("#B6C0D4"))
            b.Invalidate()
        Next
    End Sub

    Private Function Initials(name As String) As String
        Dim parts = name.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        If parts.Length = 0 Then Return "?"
        If parts.Length = 1 Then Return parts(0).Substring(0, 1).ToUpper()
        Return (parts(0).Substring(0, 1) & parts(parts.Length - 1).Substring(0, 1)).ToUpper()
    End Function

    ' =====================================================================
    '  MAIN CONTENT
    ' =====================================================================
    Private Function NewCard(x As Integer, y As Integer, w As Integer, h As Integer) As RoundedPanel
        Dim c As New RoundedPanel() With {.Bounds = New Rectangle(x, y, w, h + 6), .Radius = 10, .Shadow = True}
        page.Controls.Add(c)
        Return c
    End Function

    Private Sub CardHeader(card As Control, title As String, subtitle As Label, lineY As Integer)
        card.Controls.Add(MakeLabel(title, 18, 16, Px(14, FontStyle.Bold), Ink))
        If subtitle IsNot Nothing Then
            subtitle.Font = Px(11) : subtitle.ForeColor = Muted : subtitle.AutoSize = True
            subtitle.BackColor = Color.Transparent : subtitle.Location = New Point(18, 38)
            card.Controls.Add(subtitle)
        End If
        card.Controls.Add(New Panel() With {.Bounds = New Rectangle(1, lineY, card.Width - 2, 1), .BackColor = Hx("#EEF0F3")})
    End Sub

    Private Sub BuildContent()
        ' ---- Page header ----
        page.Controls.Add(MakeLabel("SUPER ADMIN DASHBOARD", 21, 18, Heading(24), Ink))
        lblSubtitle.Font = Px(12) : lblSubtitle.ForeColor = Hx("#9CA3AF") : lblSubtitle.AutoSize = True
        lblSubtitle.BackColor = Color.Transparent : lblSubtitle.Location = New Point(21, 50)
        page.Controls.Add(lblSubtitle)

        ' Segmented branch filter
        Dim seg As New RoundedPanel() With {.Bounds = New Rectangle(809, 29, 142, 26), .Radius = 6, .BorderColor = LineC}
        Dim names As String() = {"Both", "Jpang", "Daet"}
        Dim xs As Integer() = {1, 45, 97}
        Dim ws As Integer() = {44, 52, 44}
        For i As Integer = 0 To 2
            Dim nm As String = names(i)
            Dim b As New RoundButton() With {.Text = nm, .Font = Px(12, FontStyle.Bold), .Radius = 5,
                                             .Bounds = New Rectangle(xs(i), 1, ws(i), 24), .Tag = nm}
            AddHandler b.Click, Sub(s, e) SelectScope(nm)
            segButtons.Add(b)
            seg.Controls.Add(b)
        Next
        page.Controls.Add(seg)
        SelectScope("Both", False)

        Dim refresh As New RoundButton() With {.Text = "Refresh", .IconGlyph = Glyphs.Refresh, .IconSize = 12,
                                               .Font = Px(12, FontStyle.Bold), .FillColor = Color.White, .BorderColor = LineC,
                                               .TextColor = Hx("#374151"), .HoverColor = Hx("#F9FAFB"), .Radius = 6,
                                               .Bounds = New Rectangle(959, 29, 80, 26)}
        AddHandler refresh.Click, Sub(s, e) RaiseEvent RefreshRequested()
        page.Controls.Add(refresh)

        BuildStatCards()
        BuildSalesTrend()
        BuildBranchSplit()
        BuildTransactions()
        BuildAlerts()
        BuildQuickActions()
    End Sub

    Private Sub SelectScope(scope As String, Optional raiseIt As Boolean = True)
        _scope = scope
        For Each b In segButtons
            Dim on_ As Boolean = CStr(b.Tag) = scope
            b.FillColor = If(on_, Navy, Color.Transparent)
            b.TextColor = If(on_, Color.White, Hx("#4B5563"))
            b.Invalidate()
        Next
        UpdateSubtitles()
        If raiseIt Then RaiseEvent BranchFilterChanged(scope)
    End Sub

    Private Sub UpdateSubtitles()
        Dim scopeText As String = If(_scope = "Both", "Consolidated view " & EmDash & " Both Branches",
                                     If(_scope = "Jpang", "Jose Panganiban Branch", "Daet Branch"))
        lblSubtitle.Text = scopeText & " " & MidDot & " " & DateTime.Now.ToString("MMMM d, yyyy")
        lblTrendSub.Text = DateTime.Now.ToString("MMMM yyyy") & " " & EmDash & " " & If(_scope = "Both", "Both Branches", If(_scope = "Jpang", "Jpang", "Daet"))
    End Sub

    ' ---- Stat cards ----
    Private Sub BuildStatCards()
        Dim titles As String() = {"TOTAL SALES TODAY", "TOTAL KG SOLD", "ACTIVE CUSTOMERS", "PENDING DELIVERIES"}
        Dim notes As String() = {"vs yesterday", "both branches", "Jpang + Daet", "across all riders"}
        Dim cols As Color() = {Orange, Hx("#1E5BFF"), Hx("#8B2BE2"), Hx("#F59E0B")}
        Dim glyph As String() = {"$", Glyphs.Shop, Glyphs.People, Glyphs.Pin}
        Dim xs As Integer() = {21, 279, 537, 795}

        For i As Integer = 0 To 3
            Dim card = NewCard(xs(i), 83, 243, 101)
            card.Controls.Add(MakeLabel(titles(i), 18, 19, Px(11, FontStyle.Bold), Hx("#6B7280")))

            Dim val As New Label() With {.Text = EmDash, .Font = Mono(26, FontStyle.Bold), .ForeColor = Ink,
                                         .AutoSize = True, .Location = New Point(16, 36), .BackColor = Color.Transparent}
            card.Controls.Add(val)
            statValues.Add(val)

            Dim note As New Label() With {.Text = notes(i), .Font = Px(11), .ForeColor = Hx("#9CA3AF"),
                                          .AutoSize = True, .Location = New Point(18, 74), .BackColor = Color.Transparent}
            card.Controls.Add(note)
            statNotes.Add(note)

            Dim ib As New RoundedPanel() With {.Bounds = New Rectangle(243 - 18 - 36, 18, 36, 36), .Radius = 9,
                                               .FillColor = cols(i), .BorderColor = cols(i)}
            Dim gl As New Label() With {.Text = glyph(i), .ForeColor = Color.White, .Dock = DockStyle.Fill,
                                        .TextAlign = ContentAlignment.MiddleCenter, .BackColor = Color.Transparent}
            gl.Font = If(glyph(i) = "$", Px(16, FontStyle.Bold), IconFont(16))
            ib.Controls.Add(gl)
            card.Controls.Add(ib)
        Next
    End Sub

    ' ---- Daily sales trend ----
    Private Sub BuildSalesTrend()
        Dim card = NewCard(21, 203, 673, 313)
        CardHeader(card, "Daily Sales Trend", lblTrendSub, 63)

        Dim export As New RoundButton() With {.Text = "Export", .IconGlyph = Glyphs.Download, .IconSize = 12,
                                              .Font = Px(12, FontStyle.Bold), .FillColor = Color.Transparent,
                                              .TextColor = Hx("#374151"), .HoverColor = Hx("#F3F4F6"), .Radius = 6,
                                              .Bounds = New Rectangle(673 - 18 - 80, 16, 80, 26)}
        card.Controls.Add(export)

        salesChart.Bounds = New Rectangle(12, 70, 649, 206)
        card.Controls.Add(salesChart)

        ' Legend
        Dim lx As Integer = 673 \ 2 - 62
        card.Controls.Add(MakeLabel(BigDot, lx, 280, Px(14), Orange))
        card.Controls.Add(MakeLabel("Jpang", lx + 16, 282, Px(12), Hx("#4B5563")))
        card.Controls.Add(MakeLabel(BigDot, lx + 76, 280, Px(14), BlueC))
        card.Controls.Add(MakeLabel("Daet", lx + 92, 282, Px(12), Hx("#4B5563")))
    End Sub

    ' ---- Branch split ----
    Private Sub BuildBranchSplit()
        Dim card = NewCard(710, 203, 329, 313)
        Dim sub_ As New Label() With {.Text = "Today's sales"}
        CardHeader(card, "Branch Split", sub_, 63)

        donut.Bounds = New Rectangle(94, 76, 142, 142)
        card.Controls.Add(donut)

        AddBranchRow(card, "Jose Panganiban Branch", lblJpangAmt, fillJpang, Orange, 222)
        AddBranchRow(card, "Daet Branch", lblDaetAmt, fillDaet, BlueC, 258)
    End Sub

    Private Sub AddBranchRow(card As Control, name As String, amt As Label, fill As Panel, col As Color, y As Integer)
        card.Controls.Add(MakeLabel(name, 15, y, Px(12), Hx("#4B5563")))
        amt.Text = EmDash : amt.Font = Mono(12, FontStyle.Bold) : amt.ForeColor = Ink : amt.AutoSize = False
        amt.TextAlign = ContentAlignment.TopRight : amt.BackColor = Color.Transparent
        amt.Bounds = New Rectangle(card.Width - 15 - 110, y, 110, 18)
        card.Controls.Add(amt)

        Dim track As New Panel() With {.Bounds = New Rectangle(15, y + 20, TrackW, 4), .BackColor = Hx("#EEF0F3")}
        fill.BackColor = col
        fill.Bounds = New Rectangle(0, 0, 0, 4)
        track.Controls.Add(fill)
        card.Controls.Add(track)
    End Sub

    ' ---- Recent transactions ----
    Private Sub BuildTransactions()
        Dim card = NewCard(21, 532, 673, 304)
        Dim sub_ As New Label() With {.Text = "Today across all branches"}
        CardHeader(card, "Recent Transactions", sub_, 63)

        Dim va As New Label() With {.Text = "View All " & ArrowR, .Font = Px(12, FontStyle.Bold), .ForeColor = Ink,
                                    .AutoSize = False, .TextAlign = ContentAlignment.MiddleRight, .Cursor = Cursors.Hand,
                                    .Bounds = New Rectangle(673 - 18 - 100, 18, 100, 20), .BackColor = Color.Transparent}
        AddHandler va.Click, Sub(s, e) RaiseEvent NavigateRequested("Transactions")
        card.Controls.Add(va)

        grid.Bounds = New Rectangle(1, 64, 671, 304 - 64)
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.AllowUserToResizeRows = False
        grid.ReadOnly = True
        grid.RowHeadersVisible = False
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.BorderStyle = BorderStyle.None
        grid.BackgroundColor = Color.White
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        grid.GridColor = Hx("#EEF0F3")
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        grid.ColumnHeadersHeight = 38
        grid.RowTemplate.Height = 41
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        grid.ScrollBars = ScrollBars.Vertical
        With grid.ColumnHeadersDefaultCellStyle
            .BackColor = Hx("#F9FAFB") : .ForeColor = Hx("#6B7280") : .Font = Px(11, FontStyle.Bold)
            .SelectionBackColor = Hx("#F9FAFB") : .SelectionForeColor = Hx("#6B7280")
            .Padding = New Padding(10, 0, 0, 0)
        End With
        With grid.DefaultCellStyle
            .BackColor = Color.White : .ForeColor = Ink : .Font = Px(12)
            .SelectionBackColor = Hx("#FFF7ED") : .SelectionForeColor = Ink
            .Padding = New Padding(10, 0, 0, 0)
        End With

        AddCol("colTxn", "TXN ID", 80)
        AddCol("colCustomer", "CUSTOMER", 120)
        AddCol("colBranch", "BRANCH", 80)
        AddCol("colProduct", "PRODUCT", 130)
        AddCol("colAmount", "AMOUNT", 85)
        AddCol("colRider", "RIDER", 75)
        AddCol("colStatus", "STATUS", 85)
        AddCol("colPayment", "PAYMENT", 85)
        AddHandler grid.CellFormatting, AddressOf Grid_CellFormatting
        card.Controls.Add(grid)
    End Sub

    Private Sub AddCol(name As String, header As String, weight As Single)
        Dim idx = grid.Columns.Add(name, header)
        grid.Columns(idx).FillWeight = weight
        grid.Columns(idx).SortMode = DataGridViewColumnSortMode.NotSortable
    End Sub

    Private Sub Grid_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.Value Is Nothing Then Return
        Dim v As String = e.Value.ToString()
        Select Case grid.Columns(e.ColumnIndex).Name
            Case "colAmount"
                e.CellStyle.ForeColor = Orange
                e.CellStyle.Font = Mono(12 * UiScale, FontStyle.Bold)
            Case "colTxn"
                e.CellStyle.Font = Mono(12 * UiScale)
                e.CellStyle.ForeColor = Muted
            Case "colBranch"
                e.CellStyle.ForeColor = If(v = "Jpang", Orange, BlueC)
                e.CellStyle.Font = Px(12 * UiScale, FontStyle.Bold)
            Case "colStatus"
                e.CellStyle.Font = Px(12 * UiScale, FontStyle.Bold)
                Select Case v
                    Case "Delivered" : e.CellStyle.ForeColor = GreenC
                    Case "Pending" : e.CellStyle.ForeColor = Hx("#D97706")
                    Case "Cancelled" : e.CellStyle.ForeColor = RedC
                End Select
        End Select
    End Sub

    ' ---- Inventory alerts ----
    Private Sub BuildAlerts()
        Dim card = NewCard(710, 532, 329, 302)
        card.Controls.Add(MakeLabel("Inventory Alerts", 18, 16, Px(14, FontStyle.Bold), Ink))

        Dim badge As New RoundedPanel() With {.Bounds = New Rectangle(329 - 18 - 26, 14, 26, 22), .Radius = 5,
                                              .FillColor = Hx("#FEECEC"), .BorderColor = Hx("#F7C6C6")}
        lblAlertCount.Text = "0"
        lblAlertCount.Font = Mono(12, FontStyle.Bold) : lblAlertCount.ForeColor = RedC
        lblAlertCount.Dock = DockStyle.Fill : lblAlertCount.TextAlign = ContentAlignment.MiddleCenter
        lblAlertCount.BackColor = Color.Transparent
        badge.Controls.Add(lblAlertCount)
        card.Controls.Add(badge)

        card.Controls.Add(New Panel() With {.Bounds = New Rectangle(1, 49, 327, 1), .BackColor = Hx("#EEF0F3")})

        alertsHost.Bounds = New Rectangle(15, 63, 302, 228)
        alertsHost.FlowDirection = FlowDirection.TopDown
        alertsHost.WrapContents = False
        alertsHost.AutoScroll = True
        alertsHost.BackColor = Color.White
        card.Controls.Add(alertsHost)
    End Sub

    ' ---- Quick actions ----
    Private Sub BuildQuickActions()
        Dim card = NewCard(710, 849, 329, 188)
        card.Controls.Add(MakeLabel("Quick Actions", 18, 16, Px(14, FontStyle.Bold), Ink))
        card.Controls.Add(New Panel() With {.Bounds = New Rectangle(1, 47, 327, 1), .BackColor = Hx("#EEF0F3")})

        AddQuick(card, "Add Admin", Glyphs.Contact, 11, 59, Hx("#EAF1FF"), Hx("#BCD0FB"), Hx("#1D4ED8"))
        AddQuick(card, "Add Rider", Glyphs.People, 168, 59, Hx("#E8FBF1"), Hx("#A9EBC6"), Hx("#047857"))
        AddQuick(card, "Add Product", Glyphs.Shop, 11, 122, Hx("#FFF3E6"), Hx("#FBCB98"), Hx("#C2410C"))
        AddQuick(card, "Reports", Glyphs.Chart, 168, 122, Hx("#F3EEFF"), Hx("#D8C9FB"), Hx("#6D28D9"))
    End Sub

    Private Sub AddQuick(card As Control, text As String, glyph As String, x As Integer, y As Integer,
                         fill As Color, border As Color, fore As Color)
        Dim b As New RoundButton() With {.Text = text, .IconGlyph = glyph, .IconSize = 14, .Stacked = True,
                                         .Font = Px(12, FontStyle.Bold), .FillColor = fill, .BorderColor = border,
                                         .TextColor = fore, .HoverColor = ControlPaint.Light(fill, 0.4F), .Radius = 8,
                                         .Bounds = New Rectangle(x, y, 150, 54)}
        AddHandler b.Click, Sub(s, e) RaiseEvent QuickActionClicked(text)
        card.Controls.Add(b)
    End Sub
End Class

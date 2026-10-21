Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class AdminAccountsCards
    Private cards As RoundedTitlePanel()
    Private titles As Label()
    Private numbers As Label()
    Private holders As RoundedTitlePanel()
    Private pictures As PictureBox()
    Private ReadOnly ownedFonts As New Dictionary(Of Label, Font)
    Private originalFonts As Font()
    Private hostWindow As Form
    Private ready As Boolean
    Private arranging As Boolean

    Public Sub New()
        InitializeComponent()
        DoubleBuffered = True
        InitializeCards()
        ArrangeCards()
        AddHandler Disposed, AddressOf ReleaseResources
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If LicenseManager.UsageMode = LicenseUsageMode.Designtime OrElse DesignMode Then Return
        If Not ready Then InitializeCards()
        AttachWindow()
        ArrangeCards()
    End Sub

    Private Function FindNamed(Of T As Control)(ParamArray names As String()) As T
        For Each controlName In names
            Dim matches = Controls.Find(controlName, True)
            If matches.Length > 0 AndAlso TypeOf matches(0) Is T Then Return DirectCast(matches(0), T)
        Next
        Throw New InvalidOperationException("AdminAccountsCards is missing " & names(0))
    End Function

    Private Sub InitializeCards()
        cards = {FindNamed(Of RoundedTitlePanel)("totalAdminCard"), FindNamed(Of RoundedTitlePanel)("activeCard", "RoundedTitlePanel1"), FindNamed(Of RoundedTitlePanel)("inactiveCard", "RoundedTitlePanel2")}
        titles = {FindNamed(Of Label)("totalAdminLabel"), FindNamed(Of Label)("activeLabel"), FindNamed(Of Label)("inactiveLabel")}
        numbers = {FindNamed(Of Label)("totalAdminNumber"), FindNamed(Of Label)("activeAdminNumber", "Label1"), FindNamed(Of Label)("inactiveAdminNumber", "Label2")}
        originalFonts = {titles(0).Font, titles(1).Font, titles(2).Font, numbers(0).Font, numbers(1).Font, numbers(2).Font}
        holders = New RoundedTitlePanel(2) {}
        pictures = New PictureBox(2) {}
        Dim names = {"totalAdminIconPlaceholder", "activeIconPlaceholder", "inactiveIconPlaceholder"}
        Dim files = {"admin_hover_white.png", "check.png", "x.png"}
        Dim colors = {Color.FromArgb(242, 106, 0), Color.FromArgb(0, 157, 106), Color.FromArgb(107, 114, 128)}
        For index = 0 To 2
            cards(index).Dock = DockStyle.None
            cards(index).Anchor = AnchorStyles.Top Or AnchorStyles.Left
            cards(index).Controls.Add(titles(index))
            cards(index).Controls.Add(numbers(index))
            titles(index).AutoSize = False
            numbers(index).AutoSize = False
            numbers(index).TextAlign = ContentAlignment.MiddleLeft
            Dim found = Controls.Find(names(index), True)
            If found.Length = 0 AndAlso index = 0 Then found = Controls.Find("totalAdminIconHolder", True)
            holders(index) = If(found.Length > 0, TryCast(found(0), RoundedTitlePanel), Nothing)
            If holders(index) Is Nothing Then
                holders(index) = New RoundedTitlePanel With {.Name = names(index), .FillColor = colors(index), .BorderThickness = 0, .CornerRadius = 14}
                cards(index).Controls.Add(holders(index))
            End If
            pictures(index) = FindNamed(Of PictureBox)(names(index) & "Image")
        Next
        ready = True
    End Sub

    Private Sub AttachWindow()
        Dim current = FindForm()
        If current Is hostWindow Then Return
        If hostWindow IsNot Nothing Then RemoveHandler hostWindow.Resize, AddressOf HostResized
        hostWindow = current
        If hostWindow IsNot Nothing Then AddHandler hostWindow.Resize, AddressOf HostResized
    End Sub

    Private Sub HostResized(sender As Object, e As EventArgs)
        ArrangeCards()
    End Sub

    Protected Overrides Sub OnParentChanged(e As EventArgs)
        MyBase.OnParentChanged(e)
        If ready Then AttachWindow()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        ArrangeCards()
    End Sub

    Protected Overrides Sub OnPaddingChanged(e As EventArgs)
        MyBase.OnPaddingChanged(e)
        ArrangeCards()
    End Sub

    Protected Overrides Sub OnDpiChangedAfterParent(e As EventArgs)
        MyBase.OnDpiChangedAfterParent(e)
        ArrangeCards()
    End Sub

    Private Sub ArrangeCards()
        If Not ready OrElse arranging OrElse ClientSize.Width < 1 OrElse ClientSize.Height < 1 Then Return
        If hostWindow IsNot Nothing AndAlso hostWindow.WindowState = FormWindowState.Minimized Then Return
        arranging = True
        SuspendLayout()
        Try
            ' The supplied 924 x 105 Designer uses a 144-DPI font baseline.
            Dim dpi = DeviceDpi / 144.0F
            Dim zoom = Math.Clamp(Math.Min(ClientSize.Width / (924.0F * dpi), ClientSize.Height / (105.0F * dpi)), 0.85F, 1.4F)
            Dim unit = dpi * zoom
            Dim margin = Math.Max(2, CInt(5 * dpi))
            Dim gap = Math.Max(3, CInt(8 * dpi))
            Dim leftInset = If(Padding.Left > 0, Padding.Left, margin)
            Dim rightInset = If(Padding.Right > 0, Padding.Right, margin)
            Dim topInset = If(Padding.Top > 0, Padding.Top, margin)
            Dim bottomInset = If(Padding.Bottom > 0, Padding.Bottom, margin)
            Dim available = Math.Max(3, ClientSize.Width - leftInset - rightInset - 2 * gap)
            Dim height = Math.Max(1, ClientSize.Height - topInset - bottomInset)
            For i = 0 To 2
                Dim left = leftInset + CInt(available * i / 3.0F) + i * gap
                Dim width = Math.Max(1, CInt(available * (i + 1) / 3.0F) - CInt(available * i / 3.0F))
                cards(i).SetBounds(left, topInset, width, height)
                Dim pad = Math.Max(4, CInt(20 * unit))
                Dim iconSize = Math.Max(1, Math.Min(CInt(44 * unit), Math.Min(height - 2 * margin, width \ 4)))
                holders(i).Dock = DockStyle.None
                holders(i).SetBounds(Math.Max(0, width - pad - iconSize), Math.Max(0, (height - iconSize) \ 2), iconSize, iconSize)
                holders(i).CornerRadius = Math.Max(3, CInt(14 * unit))
                Dim inset = Math.Max(1, CInt(iconSize * 0.24F))
                pictures(i).SetBounds(inset, inset, Math.Max(1, iconSize - inset * 2), Math.Max(1, iconSize - inset * 2))
                Dim textWidth = Math.Max(1, holders(i).Left - pad - CInt(10 * unit))
                SetScaledFont(titles(i), originalFonts(i), Math.Clamp(6.5F * zoom, 6.5F, 9.0F))
                SetScaledFont(numbers(i), originalFonts(i + 3), Math.Clamp(13.5F * zoom, 12.0F, 19.0F))
                Dim titleHeight = Math.Max(titles(i).Font.Height + 2, CInt(22 * unit))
                Dim numberHeight = Math.Max(numbers(i).Font.Height + 2, CInt(40 * unit))
                Dim top = Math.Max(0, (height - titleHeight - numberHeight) \ 2)
                titles(i).SetBounds(pad, top, textWidth, titleHeight)
                numbers(i).SetBounds(pad, top + titleHeight, textWidth, Math.Min(numberHeight, Math.Max(1, height - top - titleHeight)))
                Dim spaced = TryCast(titles(i), SpacedLabel)
                If spaced IsNot Nothing Then spaced.LetterSpacing = Math.Clamp(unit, 0.5F, 1.5F)
            Next
        Finally
            ResumeLayout(False)
            arranging = False
        End Try
    End Sub

    Private Sub SetScaledFont(label As Label, original As Font, points As Single)
        If Math.Abs(label.Font.SizeInPoints - points) < 0.05F Then Return
        Dim oldFont As Font = Nothing
        ownedFonts.TryGetValue(label, oldFont)
        Dim replacement = New Font(original.FontFamily, points, original.Style, GraphicsUnit.Point)
        label.Font = replacement
        ownedFonts(label) = replacement
        If oldFont IsNot Nothing Then oldFont.Dispose()
    End Sub

    Private Sub ReleaseResources(sender As Object, e As EventArgs)
        If hostWindow IsNot Nothing Then RemoveHandler hostWindow.Resize, AddressOf HostResized
        If pictures IsNot Nothing Then
            For Each picture In pictures
                If picture IsNot Nothing AndAlso picture.Image IsNot Nothing Then picture.Image.Dispose()
            Next
        End If
        For Each ownedFont As Font In ownedFonts.Values
            ownedFont.Dispose()
        Next
        ownedFonts.Clear()
    End Sub
End Class




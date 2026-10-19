Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Windows.Forms

Public Class RoundedButton
    Inherits Button

    ' APPEARANCE
    Private _cornerRadius As Integer = 15
    Private _borderColor As Color = Color.FromArgb(223, 149, 63)
    Private _borderThickness As Integer = 2
    Private _fillColor As Color = Color.FromArgb(245, 239, 232)

    ' ICON SETTINGS
    Private _iconFileName As String = String.Empty
    Private _iconWidth As Integer = 20
    Private _iconHeight As Integer = 20
    Private _iconTextSpacing As Integer = 10
    Private _centerIconAndText As Boolean = True
    Private _loadedFileIcon As System.Drawing.Image = Nothing
    Private _resolvedIconPath As String = String.Empty

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
        Me.FlatStyle = FlatStyle.Flat
        Me.FlatAppearance.BorderSize = 0
        Me.UseVisualStyleBackColor = False

        Me.Size = New Size(160, 45)
        Me.MinimumSize = New Size(50, 25)
        Me.Font = New Font("Segoe UI", 11.0F, FontStyle.Regular)
        Me.ForeColor = Color.FromArgb(50, 50, 50)

        Me.Text = "Button"
        Me.TextAlign = ContentAlignment.MiddleCenter
        Me.ImageAlign = ContentAlignment.MiddleLeft
        Me.TextImageRelation = TextImageRelation.ImageBeforeText
        Me.Cursor = Cursors.Hand

    End Sub

    ' =====================================
    ' ROUNDED APPEARANCE PROPERTIES
    ' =====================================

    <Category("Rounded Appearance")>
    <DefaultValue(15)>
    Public Property CornerRadius As Integer
        Get
            Return _cornerRadius
        End Get
        Set(value As Integer)
            _cornerRadius = Math.Max(0, value)
            Me.Invalidate()
        End Set
    End Property

    <Category("Rounded Appearance")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("Rounded Appearance")>
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

    <Category("Rounded Appearance")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property FillColor As Color
        Get
            Return _fillColor
        End Get
        Set(value As Color)
            _fillColor = value
            Me.Invalidate()
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property TextColor As Color
        Get
            Return Me.ForeColor
        End Get
        Set(value As Color)
            Me.ForeColor = value
            Me.Invalidate()
        End Set
    End Property

    ' =====================================
    ' ICON FILENAME
    ' =====================================

    <Category("Rounded Icon")>
    <Description("Filename in the project's Icons folder, such as dashboard.png.")>
    <DefaultValue("")>
    Public Property IconFileName As String
        Get
            Return _iconFileName
        End Get
        Set(value As String)
            Dim nextValue As String = If(value, String.Empty).Trim()

            If String.Equals(
                _iconFileName,
                nextValue,
                StringComparison.Ordinal
            ) Then Return

            _iconFileName = nextValue
            LoadFileIcon()
            Me.Invalidate()
        End Set
    End Property

    ' =====================================
    ' INDEPENDENT ICON WIDTH
    ' =====================================

    <Category("Rounded Icon")>
    <Description("Icon width in pixels.")>
    <DefaultValue(20)>
    Public Property IconWidth As Integer
        Get
            Return _iconWidth
        End Get
        Set(value As Integer)
            _iconWidth = Math.Max(1, Math.Min(512, value))
            Me.Invalidate()
        End Set
    End Property

    ' =====================================
    ' INDEPENDENT ICON HEIGHT
    ' =====================================

    <Category("Rounded Icon")>
    <Description("Icon height in pixels.")>
    <DefaultValue(20)>
    Public Property IconHeight As Integer
        Get
            Return _iconHeight
        End Get
        Set(value As Integer)
            _iconHeight = Math.Max(1, Math.Min(512, value))
            Me.Invalidate()
        End Set
    End Property

    <Category("Rounded Icon")>
    <Description("Spacing between icon and text.")>
    <DefaultValue(10)>
    Public Property IconTextSpacing As Integer
        Get
            Return _iconTextSpacing
        End Get
        Set(value As Integer)
            _iconTextSpacing = Math.Max(0, Math.Min(200, value))
            Me.Invalidate()
        End Set
    End Property

    <Category("Rounded Icon")>
    <Description("When True, the icon and text are centered together as one group (for icon before/after text).")>
    <DefaultValue(True)>
    Public Property CenterIconAndText As Boolean
        Get
            Return _centerIconAndText
        End Get
        Set(value As Boolean)
            _centerIconAndText = value
            Me.Invalidate()
        End Set
    End Property

    ' COMPATIBILITY WITH OLDER CODE
    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property IconSize As Integer
        Get
            Return _iconWidth
        End Get
        Set(value As Integer)
            IconWidth = value
            IconHeight = value
        End Set
    End Property

    ' HELPFUL FOR DIAGNOSING MISSING FILES
    <Category("Rounded Icon")>
    <Description("True when the icon file has been found and loaded.")>
    Public ReadOnly Property IconFileLoaded As Boolean
        Get
            Return _loadedFileIcon IsNot Nothing
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property ResolvedIconPath As String
        Get
            Return _resolvedIconPath
        End Get
    End Property

    ' =====================================
    ' ONE-CALL HELPER: ICON BEFORE TEXT
    ' =====================================

    ''' <summary>
    ''' Puts an icon before the button text in one call.
    ''' Example: btn.SetIconBeforeText("dashboard.png", "Dashboard", 24, 24, 8)
    ''' </summary>
    Public Sub SetIconBeforeText(
        iconFile As String,
        buttonText As String,
        Optional width As Integer = 20,
        Optional height As Integer = 20,
        Optional spacing As Integer = 10
    )

        Me.TextImageRelation = TextImageRelation.ImageBeforeText
        Me.ImageAlign = ContentAlignment.MiddleLeft
        Me.TextAlign = ContentAlignment.MiddleCenter

        Me.IconWidth = width
        Me.IconHeight = height
        Me.IconTextSpacing = spacing
        Me.Text = buttonText
        Me.IconFileName = iconFile

    End Sub

    ' =====================================
    ' FIND IMAGE INSIDE ICONS FOLDER
    ' =====================================

    Private Function FindIconFile() As String

        If String.IsNullOrWhiteSpace(_iconFileName) Then
            Return String.Empty
        End If

        Dim name As String = _iconFileName.Trim().Trim(""""c)

        Try
            If File.Exists(name) Then
                Return Path.GetFullPath(name)
            End If
        Catch
        End Try

        Dim origins As New List(Of String) From {
            AppDomain.CurrentDomain.BaseDirectory,
            Environment.CurrentDirectory
        }

        Try
            origins.Add(
                Path.GetDirectoryName(Me.GetType().Assembly.Location)
            )
        Catch
        End Try

        For Each origin As String In origins

            If String.IsNullOrWhiteSpace(origin) Then Continue For

            Try
                Dim directory As New DirectoryInfo(origin)

                ' SEARCH UPWARD FROM THE BUILD FOLDER
                ' UNTIL THE PROJECT'S ICONS FOLDER IS FOUND

                For depth As Integer = 0 To 9

                    If directory Is Nothing Then Exit For

                    Dim candidate As String =
                        Path.Combine(directory.FullName, name)

                    If File.Exists(candidate) Then
                        Return candidate
                    End If

                    Dim iconPath As String =
                        Path.Combine(
                            directory.FullName,
                            "Icons",
                            Path.GetFileName(name)
                        )

                    If File.Exists(iconPath) Then
                        Return iconPath
                    End If

                    directory = directory.Parent

                Next

            Catch
            End Try

        Next

        Return String.Empty

    End Function

    ' =====================================
    ' LOAD PNG / JPG / JPEG
    ' =====================================

    Private Sub LoadFileIcon()

        If _loadedFileIcon IsNot Nothing Then
            _loadedFileIcon.Dispose()
            _loadedFileIcon = Nothing
        End If

        _resolvedIconPath = String.Empty

        If String.IsNullOrWhiteSpace(_iconFileName) Then Return

        Dim foundPath As String = FindIconFile()

        If foundPath.Length = 0 Then Return

        Try

            Using original As System.Drawing.Image =
                System.Drawing.Image.FromFile(foundPath)

                _loadedFileIcon = New Bitmap(original)

            End Using

            _resolvedIconPath = foundPath

        Catch

            _loadedFileIcon = Nothing

        End Try

    End Sub

    Public Sub ReloadIcon()
        LoadFileIcon()
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)

        MyBase.OnHandleCreated(e)

        If _loadedFileIcon Is Nothing AndAlso
           _iconFileName.Length > 0 Then

            LoadFileIcon()
            Me.Invalidate()

        End If

    End Sub

    ' =====================================
    ' ROUNDED RECTANGLE PATH
    ' =====================================

    Private Shared Function RoundedPath(
        rect As RectangleF,
        radius As Single
    ) As GraphicsPath

        Dim path As New GraphicsPath()

        If rect.Width <= 0 OrElse rect.Height <= 0 Then
            Return path
        End If

        Dim r As Single =
            Math.Max(
                0.0F,
                Math.Min(
                    radius,
                    Math.Min(rect.Width, rect.Height) / 2.0F
                )
            )

        If r <= 0 Then
            path.AddRectangle(rect)
            Return path
        End If

        Dim d As Single = r * 2.0F

        path.AddArc(rect.Left, rect.Top, d, d, 180, 90)
        path.AddArc(rect.Right - d, rect.Top, d, d, 270, 90)
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
        path.AddArc(rect.Left, rect.Bottom - d, d, d, 90, 90)

        path.CloseFigure()

        Return path

    End Function

    ' =====================================
    ' IMAGE ALIGNMENT
    ' =====================================

    Private Shared Function AlignRectangle(
        area As Rectangle,
        width As Integer,
        height As Integer,
        alignment As ContentAlignment
    ) As Rectangle

        Dim x As Integer = area.Left
        Dim y As Integer = area.Top

        Select Case alignment

            Case ContentAlignment.TopCenter,
                 ContentAlignment.MiddleCenter,
                 ContentAlignment.BottomCenter

                x += (area.Width - width) \ 2

            Case ContentAlignment.TopRight,
                 ContentAlignment.MiddleRight,
                 ContentAlignment.BottomRight

                x = area.Right - width

        End Select

        Select Case alignment

            Case ContentAlignment.MiddleLeft,
                 ContentAlignment.MiddleCenter,
                 ContentAlignment.MiddleRight

                y += (area.Height - height) \ 2

            Case ContentAlignment.BottomLeft,
                 ContentAlignment.BottomCenter,
                 ContentAlignment.BottomRight

                y = area.Bottom - height

        End Select

        Return New Rectangle(x, y, width, height)

    End Function

    ' =====================================
    ' TEXT FORMAT FLAGS
    ' =====================================

    Private Function BuildTextFlags() As TextFormatFlags

        Dim flags As TextFormatFlags =
            TextFormatFlags.SingleLine Or
            TextFormatFlags.EndEllipsis Or
            TextFormatFlags.NoPrefix

        Select Case Me.TextAlign

            Case ContentAlignment.TopLeft,
                 ContentAlignment.MiddleLeft,
                 ContentAlignment.BottomLeft

                flags = flags Or TextFormatFlags.Left

            Case ContentAlignment.TopRight,
                 ContentAlignment.MiddleRight,
                 ContentAlignment.BottomRight

                flags = flags Or TextFormatFlags.Right

            Case Else

                flags = flags Or TextFormatFlags.HorizontalCenter

        End Select

        Select Case Me.TextAlign

            Case ContentAlignment.TopLeft,
                 ContentAlignment.TopCenter,
                 ContentAlignment.TopRight

                flags = flags Or TextFormatFlags.Top

            Case ContentAlignment.BottomLeft,
                 ContentAlignment.BottomCenter,
                 ContentAlignment.BottomRight

                flags = flags Or TextFormatFlags.Bottom

            Case Else

                flags = flags Or TextFormatFlags.VerticalCenter

        End Select

        Return flags

    End Function

    ' =====================================
    ' CUSTOM BUTTON DRAWING
    ' =====================================

    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        MyBase.OnPaintBackground(e)

        Dim w As Integer = Me.ClientSize.Width
        Dim h As Integer = Me.ClientSize.Height

        If w <= 2 OrElse h <= 2 Then Return

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        ' DRAW BUTTON FILL

        Using fillPath As GraphicsPath =
            RoundedPath(
                New RectangleF(0.5F, 0.5F, w - 1.0F, h - 1.0F),
                _cornerRadius
            )

            Using brush As New SolidBrush(_fillColor)
                g.FillPath(brush, fillPath)
            End Using

        End Using

        ' DRAW BORDER

        Dim thickness As Single =
            CSng(
                Math.Min(
                    _borderThickness,
                    Math.Max(0, Math.Min(w, h) - 2)
                )
            )

        If thickness > 0 Then

            Dim inset As Single = thickness / 2.0F + 0.5F

            Dim borderRect As New RectangleF(
                inset,
                inset,
                w - 1.0F - 2.0F * inset,
                h - 1.0F - 2.0F * inset
            )

            If borderRect.Width > 0 AndAlso
               borderRect.Height > 0 Then

                Using path As GraphicsPath =
                    RoundedPath(
                        borderRect,
                        Math.Max(0.0F, _cornerRadius - inset)
                    )

                    Using pen As New Pen(_borderColor, thickness)
                        g.DrawPath(pen, path)
                    End Using

                End Using

            End If

        End If

        ' CONTENT AREA

        Dim content As New Rectangle(
            10 + Me.Padding.Left,
            3 + Me.Padding.Top,
            w - 20 - Me.Padding.Horizontal,
            h - 6 - Me.Padding.Vertical
        )

        If content.Width <= 0 OrElse
           content.Height <= 0 Then Return

        Dim textRect As Rectangle = content
        Dim flags As TextFormatFlags = BuildTextFlags()

        ' ICON FILE TAKES PRIORITY.
        ' IF NONE IS LOADED, USE THE BUILT-IN IMAGE PROPERTY.

        Dim src As System.Drawing.Image =
            If(_loadedFileIcon, Me.Image)

        If src IsNot Nothing AndAlso
           src.Width > 0 AndAlso src.Height > 0 Then

            Dim iw As Integer =
                Math.Min(_iconWidth, content.Width)

            Dim ih As Integer =
                Math.Min(_iconHeight, content.Height)

            Dim iconRect As Rectangle =
                AlignRectangle(
                    content,
                    iw,
                    ih,
                    Me.ImageAlign
                )

            Dim hasText As Boolean =
                Not String.IsNullOrEmpty(Me.Text)

            If hasText Then

                Select Case Me.TextImageRelation

                    Case TextImageRelation.ImageBeforeText

                        iconRect.Y =
                            content.Top +
                            (content.Height - ih) \ 2

                        ' MEASURE TEXT SO ICON + TEXT CAN BE CENTERED AS A GROUP
                        Dim textW As Integer =
                            TextRenderer.MeasureText(
                                g,
                                Me.Text,
                                Me.Font,
                                New Size(Integer.MaxValue, content.Height),
                                TextFormatFlags.SingleLine Or
                                TextFormatFlags.NoPrefix Or
                                TextFormatFlags.NoPadding
                            ).Width

                        Dim groupW As Integer =
                            iw + _iconTextSpacing + textW

                        Dim startX As Integer = content.Left

                        If _centerIconAndText AndAlso
                           groupW < content.Width Then

                            startX =
                                content.Left +
                                (content.Width - groupW) \ 2

                            ' TEXT RECT IS EXACTLY THE TEXT WIDTH (PLUS A LITTLE SLACK)
                            iconRect.X = startX

                            Dim tLeft As Integer =
                                startX + iw + _iconTextSpacing

                            textRect = New Rectangle(
                                tLeft,
                                content.Top,
                                Math.Max(0, content.Right - tLeft),
                                content.Height
                            )

                            flags =
                                (flags And Not TextFormatFlags.HorizontalCenter And
                                 Not TextFormatFlags.Right) Or
                                TextFormatFlags.Left

                        Else

                            iconRect.X = content.Left

                            Dim textLeft As Integer =
                                iconRect.Right + _iconTextSpacing

                            textRect = New Rectangle(
                                textLeft,
                                content.Top,
                                Math.Max(0, content.Right - textLeft),
                                content.Height
                            )

                        End If

                    Case TextImageRelation.TextBeforeImage

                        iconRect.Y =
                            content.Top +
                            (content.Height - ih) \ 2

                        Dim textW2 As Integer =
                            TextRenderer.MeasureText(
                                g,
                                Me.Text,
                                Me.Font,
                                New Size(Integer.MaxValue, content.Height),
                                TextFormatFlags.SingleLine Or
                                TextFormatFlags.NoPrefix Or
                                TextFormatFlags.NoPadding
                            ).Width

                        Dim groupW2 As Integer =
                            iw + _iconTextSpacing + textW2

                        If _centerIconAndText AndAlso
                           groupW2 < content.Width Then

                            Dim startX2 As Integer =
                                content.Left +
                                (content.Width - groupW2) \ 2

                            textRect = New Rectangle(
                                startX2,
                                content.Top,
                                textW2,
                                content.Height
                            )

                            iconRect.X =
                                startX2 + textW2 + _iconTextSpacing

                            flags =
                                (flags And Not TextFormatFlags.HorizontalCenter And
                                 Not TextFormatFlags.Right) Or
                                TextFormatFlags.Left

                        Else

                            iconRect.X = content.Right - iw

                            textRect = New Rectangle(
                                content.Left,
                                content.Top,
                                Math.Max(
                                    0,
                                    iconRect.Left -
                                    _iconTextSpacing -
                                    content.Left
                                ),
                                content.Height
                            )

                        End If

                    Case TextImageRelation.ImageAboveText

                        iconRect.X =
                            content.Left +
                            (content.Width - iw) \ 2

                        iconRect.Y = content.Top

                        Dim textTop As Integer =
                            iconRect.Bottom + _iconTextSpacing

                        textRect = New Rectangle(
                            content.Left,
                            textTop,
                            content.Width,
                            Math.Max(0, content.Bottom - textTop)
                        )

                    Case TextImageRelation.TextAboveImage

                        iconRect.X =
                            content.Left +
                            (content.Width - iw) \ 2

                        iconRect.Y = content.Bottom - ih

                        textRect = New Rectangle(
                            content.Left,
                            content.Top,
                            content.Width,
                            Math.Max(
                                0,
                                iconRect.Top -
                                _iconTextSpacing -
                                content.Top
                            )
                        )

                    Case TextImageRelation.Overlay

                        textRect = content

                End Select

            End If

            ' DRAW IMAGE

            Dim previousInterpolation As InterpolationMode =
                g.InterpolationMode

            g.InterpolationMode =
                InterpolationMode.HighQualityBicubic

            g.DrawImage(src, iconRect)

            g.InterpolationMode = previousInterpolation

        End If

        ' DRAW TEXT

        If String.IsNullOrEmpty(Me.Text) OrElse
           textRect.Width <= 0 OrElse
           textRect.Height <= 0 Then Return

        Dim textColor As Color =
            If(
                Me.Enabled,
                Me.ForeColor,
                SystemColors.GrayText
            )

        TextRenderer.DrawText(
            g,
            Me.Text,
            Me.Font,
            textRect,
            textColor,
            flags
        )

    End Sub

    ' =====================================
    ' REFRESH DRAWING
    ' =====================================

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnFontChanged(e As EventArgs)
        MyBase.OnFontChanged(e)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnForeColorChanged(e As EventArgs)
        MyBase.OnForeColorChanged(e)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnEnabledChanged(e As EventArgs)
        MyBase.OnEnabledChanged(e)
        Me.Invalidate()
    End Sub

    ' =====================================
    ' CLEAN UP LOADED IMAGE
    ' =====================================

    Protected Overrides Sub Dispose(disposing As Boolean)

        If disposing Then

            If _loadedFileIcon IsNot Nothing Then
                _loadedFileIcon.Dispose()
                _loadedFileIcon = Nothing
            End If

        End If

        MyBase.Dispose(disposing)

    End Sub

End Class

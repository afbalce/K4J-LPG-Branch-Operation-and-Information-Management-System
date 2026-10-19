
Imports System
Imports System.Drawing
Imports System.Windows.Forms

' ==========================================
' RESPONSIVE SYSTEM HEADING USERCONTROL
' ==========================================
' Existing Designer controls:
' panelLabel
' panelDesc
' ==========================================

Public Class SystemHeading

    ' ==========================================
    ' RESPONSIVE SETTINGS
    ' ==========================================

    Private Const ReferenceWidth As Integer = 950

    Private Const MinimumScale As Double = 0.85R
    Private Const MaximumScale As Double = 1.35R

    ' ==========================================
    ' TITLE FONT - BARLOW CONDENSED BOLD
    ' ==========================================

    Private Const TitleBaseSize As Single = 15.0F

    Private Const MinimumTitleFont As Single = 12.0F
    Private Const MaximumTitleFont As Single = 18.0F

    ' ==========================================
    ' DESCRIPTION FONT - INTER REGULAR
    ' ==========================================

    Private Const DescriptionBaseSize As Single = 10.0F

    Private Const MinimumDescFont As Single = 8.5F
    Private Const MaximumDescFont As Single = 12.0F

    ' ==========================================
    ' SPACING
    ' ==========================================

    Private Const BaseLeftMargin As Integer = 18
    Private Const BaseRightMargin As Integer = 18

    Private Const BaseTopMargin As Integer = 6
    Private Const BaseLabelGap As Integer = 2
    Private Const BaseBottomMargin As Integer = 6

    ' ==========================================
    ' COLORS
    ' ==========================================

    Private ReadOnly _titleColor As Color =
        Color.FromArgb(30, 41, 59)

    Private ReadOnly _descriptionColor As Color =
        Color.FromArgb(148, 163, 184)

    Private ReadOnly _backgroundColor As Color =
        Color.FromArgb(241, 245, 249)

    ' ==========================================
    ' STATE
    ' ==========================================

    Private _ready As Boolean = False
    Private _busy As Boolean = False

    Private _titleFont As Font = Nothing
    Private _descriptionFont As Font = Nothing

    ' ==========================================
    ' CONSTRUCTOR
    ' ==========================================

    Public Sub New()

        InitializeComponent()

        Me.DoubleBuffered = True

        Me.AutoSize = False
        Me.AutoScroll = False

        Me.AutoScrollMinSize = Size.Empty

        Me.MinimumSize = Size.Empty
        Me.MaximumSize = Size.Empty

        Me.Margin = New Padding(0)
        Me.Padding = New Padding(0)

        Me.BackColor = _backgroundColor

        Me.Height = 65

        ConfigureDesignerLabels()

        _ready = True

        AddHandler Me.Disposed,
            AddressOf SystemHeading_Disposed

        UpdateHeadingLayout()

    End Sub

    ' ==========================================
    ' DPI SCALING
    ' ==========================================

    Private Function Px(
        value As Integer
    ) As Integer

        Return Math.Max(
            1,
            CInt(
                value *
                Math.Max(0.75R, Me.DeviceDpi / 96.0R)
            )
        )

    End Function

    ' ==========================================
    ' CHECK INSTALLED FONTS
    ' ==========================================

    Private Function InstalledFontOr(
        requested As String,
        fallback As String
    ) As String

        For Each family As FontFamily In FontFamily.Families

            If String.Equals(
                family.Name,
                requested,
                StringComparison.OrdinalIgnoreCase
            ) Then

                Return family.Name

            End If

        Next

        Return fallback

    End Function

    ' ==========================================
    ' RESPONSIVE SCALE
    ' ==========================================

    Private Function GetResponsiveScale() As Double

        Dim availableWidth As Integer =
            Math.Max(1, Me.ClientSize.Width)

        Return Math.Max(
            MinimumScale,
            Math.Min(
                MaximumScale,
                availableWidth / CDbl(Px(ReferenceWidth))
            )
        )

    End Function

    ' ==========================================
    ' CONFIGURE DESIGNER LABELS
    ' ==========================================

    Private Sub ConfigureDesignerLabels()

        Me.SuspendLayout()

        Try

            For Each lbl As Label In New Label() {
                panelLabel,
                panelDesc
            }

                lbl.Dock = DockStyle.None

                lbl.Anchor =
                    AnchorStyles.Top Or AnchorStyles.Left

                lbl.AutoSize = False
                lbl.AutoEllipsis = False

                lbl.Margin = New Padding(0)
                lbl.Padding = New Padding(0)

                lbl.BackColor = Color.Transparent

                lbl.TextAlign =
                    ContentAlignment.MiddleLeft

            Next

            ' ==================================
            ' TITLE COLOR AND FONT
            ' ==================================

            panelLabel.ForeColor = _titleColor

            panelLabel.Font = New Font(
                InstalledFontOr(
                    "Barlow Condensed",
                    "Arial Narrow"
                ),
                TitleBaseSize,
                FontStyle.Bold,
                GraphicsUnit.Point
            )

            ' ==================================
            ' DESCRIPTION COLOR AND FONT
            ' ==================================

            panelDesc.ForeColor = _descriptionColor

            panelDesc.Font = New Font(
                InstalledFontOr(
                    "Inter",
                    "Segoe UI"
                ),
                DescriptionBaseSize,
                FontStyle.Regular,
                GraphicsUnit.Point
            )

        Finally

            Me.ResumeLayout(True)

        End Try

    End Sub

    ' ==========================================
    ' APPLY RESPONSIVE FONT
    ' ==========================================

    Private Sub ApplyFont(
        ctrl As Control,
        family As String,
        sizeInPoints As Single,
        style As FontStyle,
        ByRef ownedFont As Font
    )

        If ctrl Is Nothing OrElse ctrl.IsDisposed Then
            Return
        End If

        If ownedFont IsNot Nothing AndAlso
           Math.Abs(
               ownedFont.SizeInPoints - sizeInPoints
           ) < 0.1F AndAlso
           ownedFont.Style = style AndAlso
           String.Equals(
               ownedFont.FontFamily.Name,
               family,
               StringComparison.OrdinalIgnoreCase
           ) Then

            Return

        End If

        Dim nextFont As New Font(
            family,
            sizeInPoints,
            style,
            GraphicsUnit.Point
        )

        Dim previous As Font = ownedFont

        ctrl.Font = nextFont
        ownedFont = nextFont

        If previous IsNot Nothing Then
            previous.Dispose()
        End If

    End Sub

    ' ==========================================
    ' SET HEADING TEXT
    ' ==========================================

    Public Sub SetHeading(
        title As String,
        description As String
    )

        panelLabel.Text =
            If(title, String.Empty)

        panelDesc.Text =
            If(description, String.Empty)

        UpdateHeadingLayout()

    End Sub

    ' ==========================================
    ' PUBLIC REFRESH
    ' ==========================================

    Public Sub RefreshHeadingLayout()

        UpdateHeadingLayout()

    End Sub

    ' ==========================================
    ' RESPONSIVE HEADING LAYOUT
    ' ==========================================

    Private Sub UpdateHeadingLayout()

        If Not _ready OrElse
           _busy OrElse
           Me.IsDisposed Then

            Return

        End If

        Dim w As Integer = Me.ClientSize.Width

        If w < 1 Then Return

        _busy = True

        Me.SuspendLayout()

        Try

            ' ==================================
            ' CALCULATE RESPONSIVE SCALE
            ' ==================================

            Dim scale As Double =
                GetResponsiveScale()

            ' ==================================
            ' RESPONSIVE TITLE FONT SIZE
            ' ==================================

            Dim titleSize As Single =
                CSng(
                    Math.Max(
                        MinimumTitleFont,
                        Math.Min(
                            MaximumTitleFont,
                            TitleBaseSize * scale
                        )
                    )
                )

            ' ==================================
            ' RESPONSIVE DESCRIPTION FONT SIZE
            ' ==================================

            Dim descSize As Single =
                CSng(
                    Math.Max(
                        MinimumDescFont,
                        Math.Min(
                            MaximumDescFont,
                            DescriptionBaseSize * scale
                        )
                    )
                )

            ' ==================================
            ' APPLY BARLOW CONDENSED BOLD
            ' ==================================

            ApplyFont(
                panelLabel,
                InstalledFontOr(
                    "Barlow Condensed",
                    "Arial Narrow"
                ),
                titleSize,
                FontStyle.Bold,
                _titleFont
            )

            ' ==================================
            ' APPLY INTER REGULAR
            ' ==================================

            ApplyFont(
                panelDesc,
                InstalledFontOr(
                    "Inter",
                    "Segoe UI"
                ),
                descSize,
                FontStyle.Regular,
                _descriptionFont
            )

            ' ==================================
            ' RESPONSIVE SPACING
            ' ==================================

            Dim marginLeft As Integer =
                Px(BaseLeftMargin)

            Dim marginRight As Integer =
                Px(BaseRightMargin)

            Dim marginTop As Integer =
                Px(BaseTopMargin)

            Dim labelGap As Integer =
                Px(BaseLabelGap)

            Dim marginBottom As Integer =
                Px(BaseBottomMargin)

            ' ==================================
            ' AVAILABLE TEXT WIDTH
            ' ==================================

            Dim textW As Integer =
                Math.Max(
                    1,
                    w - marginLeft - marginRight
                )

            ' ==================================
            ' TITLE HEIGHT
            ' ==================================

            Dim titleH As Integer =
                Math.Max(
                    Px(18),
                    TextRenderer.MeasureText(
                        If(panelLabel.Text, ""),
                        panelLabel.Font,
                        New Size(textW, 10000),
                        TextFormatFlags.WordBreak Or
                        TextFormatFlags.NoPrefix
                    ).Height + Px(2)
                )

            ' ==================================
            ' DESCRIPTION HEIGHT
            ' ==================================

            Dim descH As Integer =
                Math.Max(
                    Px(14),
                    TextRenderer.MeasureText(
                        If(panelDesc.Text, ""),
                        panelDesc.Font,
                        New Size(textW, 10000),
                        TextFormatFlags.WordBreak Or
                        TextFormatFlags.NoPrefix
                    ).Height + Px(2)
                )

            ' ==================================
            ' POSITION TITLE
            ' ==================================

            panelLabel.SetBounds(
                marginLeft,
                marginTop,
                textW,
                titleH
            )

            ' ==================================
            ' POSITION DESCRIPTION
            ' ==================================

            panelDesc.SetBounds(
                marginLeft,
                marginTop + titleH + labelGap,
                textW,
                descH
            )

            ' ==================================
            ' AUTOMATIC HEADING HEIGHT
            ' ==================================

            Dim desiredHeight As Integer =
                marginTop +
                titleH +
                labelGap +
                descH +
                marginBottom

            If Me.Height <> desiredHeight Then
                Me.Height = desiredHeight
            End If

        Finally

            Me.ResumeLayout(True)

            _busy = False

        End Try

    End Sub

    ' ==========================================
    ' WINDOW RESIZING
    ' ==========================================

    Protected Overrides Sub OnResize(
        e As EventArgs
    )

        MyBase.OnResize(e)

        UpdateHeadingLayout()

    End Sub

    ' ==========================================
    ' CONTROL LOAD
    ' ==========================================

    Protected Overrides Sub OnLoad(
        e As EventArgs
    )

        MyBase.OnLoad(e)

        UpdateHeadingLayout()

    End Sub

    ' ==========================================
    ' CLEANUP
    ' ==========================================

    Private Sub SystemHeading_Disposed(
        sender As Object,
        e As EventArgs
    )

        If _titleFont IsNot Nothing Then
            _titleFont.Dispose()
        End If

        If _descriptionFont IsNot Nothing Then
            _descriptionFont.Dispose()
        End If

    End Sub
End Class

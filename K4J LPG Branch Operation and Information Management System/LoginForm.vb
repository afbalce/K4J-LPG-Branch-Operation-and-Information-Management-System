
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.ComponentModel

' THIS FILE CONTAINS LOGINFORM AND PASSWORD EYE ICON
Public Class LoginForm

    ' ==========================================
    ' RESPONSIVE SETTINGS
    ' ==========================================

    Private Const StartMaximized As Boolean = False
    Private Const RoundedPanelHeightScale As Double = 0.75

    Private Const FieldHintFontScale As Double = 0.7
    Private Const ComboHintFontScale As Double = 0.5

    Private Const DesignLoginWidth As Double = 400.0
    Private Const DesignLoginHeight As Double = 398.0

    Private Const DesignRoleHeight As Double = 68.0
    Private Const DesignRoleGap As Double = 16.0
    Private Const DesignRoleVerticalGap As Double = 26.0

    Private Const LoginWidthRatio As Double = 0.88
    Private Const MaxLoginScale As Double = 1.6

    Private layoutReady As Boolean = False
    Private updatingLayout As Boolean = False

    ' ==========================================
    ' RESPONSIVE DATA
    ' ==========================================

    Private ReadOnly fontCache As New Dictionary(
        Of String, Font
    )(StringComparer.Ordinal)

    Private ReadOnly watchedParents As New HashSet(Of Control)
    Private ReadOnly leftLabels As New List(Of LabelInfo)
    Private ReadOnly otherTitlePanels As New List(Of PanelInfo)
    Private ReadOnly roles As New List(Of RoleInfo)
    Private ReadOnly inputFonts As New Dictionary(Of Control, Font)

    Private loginCard As Control = Nothing

    ' ==========================================
    ' PASSWORD EYE BUTTON
    ' ==========================================

    Private passwordToggleButton As LoginFormEyeToggle = Nothing
    Private passwordVisible As Boolean = False
    Private ReadOnly passwordEyeTooltip As New ToolTip()

    Private Class LabelInfo
        Public Property Target As Control
        Public Property Bounds As Rectangle
        Public Property ParentSize As Size
        Public Property BaseFont As Font
        Public Property BottomAligned As Boolean
    End Class

    Private Class PanelInfo
        Public Property Target As Control
        Public Property Bounds As Rectangle
        Public Property ParentSize As Size
    End Class

    Private Class RoleInfo
        Public Property Card As Control
        Public Property Badge As Control
        Public Property Heading As Label
        Public Property Description As Label
        Public Property HeadingFont As Font
        Public Property DescriptionFont As Font
    End Class

    ' ==========================================
    ' FORM LOAD
    ' ==========================================

    Private Sub LoginForm_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        layoutReady = False

        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.WindowState = FormWindowState.Normal

        RegisterLeftLabel(K4JLabel)
        RegisterLeftLabel(MSLabel)
        RegisterLeftLabel(BOPLabel)

        RegisterLeftLabel(desc1)
        RegisterLeftLabel(desc2)
        RegisterLeftLabel(credits, True)

        FindLoginCard()
        SetupRoleCards()
        SetupLoginControls()
        RegisterTitlePanels(Me)

        BOPLabel.BringToFront()

        WindowSizing.ApplyStartingSize(Me)

        layoutReady = True

        UpdateResponsiveLayout()
        SetupPasswordToggle()

    End Sub

    ' ==========================================
    ' FORM SHOWN
    ' ==========================================

    Private Sub LoginForm_Shown(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Shown

        If StartMaximized Then
            Me.WindowState = FormWindowState.Maximized
        End If

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' WATCH PARENT RESIZING
    ' ==========================================

    Private Sub WatchParent(parent As Control)

        If parent Is Nothing OrElse parent Is Me Then Return

        If watchedParents.Add(parent) Then
            AddHandler parent.Resize,
                AddressOf ParentContainer_Resize
        End If

    End Sub

    ' ==========================================
    ' REGISTER LEFT LABEL
    ' ==========================================

    Private Sub RegisterLeftLabel(
        target As Control,
        Optional bottomAligned As Boolean = False
    )

        If target Is Nothing OrElse
           target.Parent Is Nothing Then Return

        leftLabels.Add(New LabelInfo With {
            .Target = target,
            .Bounds = target.Bounds,
            .ParentSize = target.Parent.ClientSize,
            .BaseFont = target.Font,
            .BottomAligned = bottomAligned
        })

        If TypeOf target Is Label Then

            DirectCast(target, Label).AutoSize = False
            DirectCast(target, Label).AutoEllipsis = False

        End If

        If target.Dock = DockStyle.None Then
            target.Anchor = AnchorStyles.Left Or AnchorStyles.Top
        End If

        WatchParent(target.Parent)

    End Sub

    ' ==========================================
    ' FIND WHITE LOGIN CARD
    ' ==========================================

    Private Sub FindLoginCard()

        Dim candidate As Control = WbLabel.Parent

        While candidate IsNot Nothing AndAlso
              candidate IsNot Me

            Dim p As Control = candidate.Parent

            If p IsNot Nothing AndAlso
               candidate.Dock = DockStyle.None AndAlso
               candidate.Contains(EmailField) AndAlso
               candidate.Contains(PassField) AndAlso
               candidate.Contains(signinButton) AndAlso
               p.ClientSize.Width > 0 AndAlso
               p.ClientSize.Height > 0 AndAlso
               candidate.Width < p.ClientSize.Width * 0.9 AndAlso
               candidate.Height < p.ClientSize.Height * 0.9 Then

                loginCard = candidate

                loginCard.Anchor =
                    AnchorStyles.Top Or AnchorStyles.Left

                WatchParent(p)

                Return

            End If

            candidate = candidate.Parent

        End While

    End Sub

    ' ==========================================
    ' SET CONTROL PARENT
    ' ==========================================

    Private Sub PutInParent(
        child As Control,
        parent As Control
    )

        If child Is Nothing OrElse parent Is Nothing Then Return

        If Not Object.ReferenceEquals(child.Parent, parent) Then
            parent.Controls.Add(child)
        End If

        child.Dock = DockStyle.None
        child.Anchor = AnchorStyles.Top Or AnchorStyles.Left

    End Sub

    ' ==========================================
    ' SETUP LOGIN CONTROLS
    ' ==========================================

    Private Sub SetupLoginControls()

        If loginCard Is Nothing Then Return

        Dim items As Control() = {
            WbLabel,
            desc3,
            EmailLabel,
            EmailField,
            PassLabel,
            PassField,
            RoleLabel,
            RoleDropdown,
            BranchLabel,
            BranchDropdown,
            signinButton,
            forgotpass
        }

        For Each child As Control In items

            PutInParent(child, loginCard)
            inputFonts(child) = child.Font

        Next

        If TypeOf forgotpass Is Label Then

            DirectCast(forgotpass, Label).AutoSize = False

            DirectCast(forgotpass, Label).TextAlign =
                ContentAlignment.MiddleCenter

        End If

        WatchParent(loginCard)

    End Sub

    ' ==========================================
    ' SETUP ROLE CARDS
    ' ==========================================

    Private Sub SetupRoleCards()

        If loginCard Is Nothing OrElse
           loginCard.Parent Is Nothing Then Return

        Dim p As Control = loginCard.Parent

        SetupRole(
            sAdminPanel,
            sAdminTitlePanel,
            sAdminLabel,
            sAdminDesc,
            p
        )

        SetupRole(
            adminPanel,
            adminTitlePanel,
            adminLabel,
            adminDesc,
            p
        )

        SetupRole(
            riderPanel,
            riderTitlePanel,
            riderLabel,
            riderDesc,
            p
        )

    End Sub

    ' ==========================================
    ' SETUP INDIVIDUAL ROLE CARD
    ' ==========================================

    Private Sub SetupRole(
        card As Control,
        badge As Control,
        heading As Label,
        description As Label,
        parent As Control
    )

        If card Is Nothing OrElse
           badge Is Nothing OrElse
           heading Is Nothing OrElse
           description Is Nothing Then Return

        PutInParent(card, parent)
        PutInParent(badge, card)
        PutInParent(heading, badge)
        PutInParent(description, card)

        heading.AutoSize = False
        description.AutoSize = False

        heading.TextAlign = ContentAlignment.MiddleCenter
        description.TextAlign = ContentAlignment.MiddleCenter

        roles.Add(New RoleInfo With {
            .Card = card,
            .Badge = badge,
            .Heading = heading,
            .Description = description,
            .HeadingFont = heading.Font,
            .DescriptionFont = description.Font
        })

        card.Visible = True
        badge.Visible = True
        heading.Visible = True
        description.Visible = True

        WatchParent(parent)

    End Sub

    ' ==========================================
    ' IDENTIFY ROLE PANELS
    ' ==========================================

    Private Function IsRolePanel(
        target As Control
    ) As Boolean

        For Each info As RoleInfo In roles

            If Object.ReferenceEquals(target, info.Card) OrElse
               Object.ReferenceEquals(target, info.Badge) Then

                Return True

            End If

        Next

        Return False

    End Function

    ' ==========================================
    ' REGISTER OTHER TITLE PANELS
    ' ==========================================

    Private Sub RegisterTitlePanels(root As Control)

        For Each child As Control In root.Controls

            If TypeOf child Is RoundedTitlePanel AndAlso
               Not IsRolePanel(child) AndAlso
               Not Object.ReferenceEquals(child, loginCard) Then

                otherTitlePanels.Add(New PanelInfo With {
                    .Target = child,
                    .Bounds = child.Bounds,
                    .ParentSize = root.ClientSize
                })

                If child.Dock = DockStyle.None Then

                    child.Anchor =
                        AnchorStyles.Left Or AnchorStyles.Top

                End If

                WatchParent(root)

            End If

            RegisterTitlePanels(child)

        Next

    End Sub

    ' ==========================================
    ' FONT CACHE
    ' ==========================================

    Private Function CachedFont(
        baseFont As Font,
        requestedSize As Double
    ) As Font

        Dim size As Single =
            CSng(
                Math.Max(
                    6.0,
                    Math.Min(
                        50.0,
                        Math.Round(
                            requestedSize * 2.0,
                            MidpointRounding.AwayFromZero
                        ) / 2.0
                    )
                )
            )

        Dim key As String =
            baseFont.FontFamily.Name & "|" &
            CInt(baseFont.Style).ToString() & "|" &
            CInt(size * 2.0F).ToString()

        Dim result As Font = Nothing

        If fontCache.TryGetValue(key, result) Then
            Return result
        End If

        result = New Font(
            baseFont.FontFamily,
            size,
            baseFont.Style,
            GraphicsUnit.Point
        )

        fontCache.Add(key, result)

        Return result

    End Function

    ' ==========================================
    ' RESPONSIVE FONT FITTING
    ' ==========================================

    Private Function FitFont(
        value As String,
        sourceFont As Font,
        scale As Double,
        availableWidth As Integer,
        availableHeight As Integer
    ) As Font

        Dim original As Font = If(sourceFont, Me.Font)

        Dim size As Double =
            Math.Max(6.0, original.SizeInPoints * scale)

        Do

            Dim f As Font = CachedFont(original, size)

            Dim needed As Size =
                TextRenderer.MeasureText(
                    If(String.IsNullOrEmpty(value), " ", value),
                    f,
                    New Size(100000, 100000),
                    TextFormatFlags.NoPadding Or
                    TextFormatFlags.NoPrefix
                )

            If (needed.Width <= availableWidth AndAlso
                needed.Height <= availableHeight) OrElse
                size <= 6.0 Then

                Return f

            End If

            size = Math.Max(6.0, size - 0.5)

        Loop

    End Function

    ' ==========================================
    ' RESIZE WHITE LOGIN CARD
    ' ==========================================

    Private Sub ResizeLoginCard()

        If loginCard Is Nothing OrElse
           loginCard.Parent Is Nothing OrElse
           loginCard.IsDisposed Then Return

        Dim parent As Control = loginCard.Parent

        Dim pw As Integer = parent.ClientSize.Width
        Dim ph As Integer = parent.ClientSize.Height

        If pw < 50 OrElse ph < 100 Then Return

        Dim widthScale As Double =
            (pw * LoginWidthRatio) / DesignLoginWidth

        Dim heightScale As Double =
            (ph - 36.0) /
            (DesignLoginHeight +
             DesignRoleVerticalGap +
             DesignRoleHeight)

        Dim fitWidthScale As Double =
            (pw - 28.0) / DesignLoginWidth

        Dim s As Double =
            Math.Min(
                MaxLoginScale,
                Math.Min(
                    widthScale,
                    Math.Min(heightScale, fitWidthScale)
                )
            )

        If s <= 0 Then Return

        Dim cardW As Integer =
            Math.Max(1, CInt(DesignLoginWidth * s))

        Dim cardH As Integer =
            Math.Max(1, CInt(DesignLoginHeight * s))

        Dim groupH As Integer =
            cardH +
            CInt(DesignRoleVerticalGap * s) +
            CInt(DesignRoleHeight * s)

        loginCard.SetBounds(
            Math.Max(0, (pw - cardW) \ 2),
            Math.Max(0, (ph - groupH) \ 2),
            cardW,
            cardH
        )

        loginCard.Visible = True

    End Sub

    ' ==========================================
    ' RESIZE ROLE CARDS
    ' ==========================================

    Private Sub LayoutRoleCards()

        If loginCard Is Nothing OrElse
           roles.Count <> 3 OrElse
           loginCard.Parent Is Nothing Then Return

        Dim s As Double =
            loginCard.Width / DesignLoginWidth

        Dim gap As Integer =
            Math.Max(4, CInt(DesignRoleGap * s))

        Dim between As Integer =
            Math.Max(5, CInt(DesignRoleVerticalGap * s))

        Dim cardW As Integer =
            Math.Max(1, (loginCard.Width - gap * 2) \ 3)

        Dim totalW As Integer =
            cardW * 3 + gap * 2

        Dim startX As Integer =
            loginCard.Left +
            (loginCard.Width - totalW) \ 2

        Dim cardY As Integer =
            loginCard.Bottom + between

        Dim cardH As Integer =
            Math.Min(
                Math.Max(1, CInt(DesignRoleHeight * s)),
                Math.Max(
                    1,
                    loginCard.Parent.ClientSize.Height -
                    cardY - 6
                )
            )

        For i As Integer = 0 To roles.Count - 1

            Dim item As RoleInfo = roles(i)

            ' OUTER ROLE CARD

            item.Card.SetBounds(
                startX + i * (cardW + gap),
                cardY,
                cardW,
                cardH
            )

            ' TITLE BADGE

            Dim badgeH As Integer =
                Math.Max(
                    1,
                    Math.Min(cardH, CInt(19 * s))
                )

            Dim badgeY As Integer =
                Math.Min(
                    Math.Max(0, CInt(14 * s)),
                    Math.Max(0, cardH - badgeH)
                )

            Dim titleFont As Font =
                FitFont(
                    item.Heading.Text,
                    item.HeadingFont,
                    s,
                    Math.Max(1, cardW - 12),
                    badgeH
                )

            item.Heading.Font = titleFont

            Dim badgeW As Integer =
                Math.Max(
                    1,
                    Math.Min(
                        Math.Max(1, cardW - 8),
                        TextRenderer.MeasureText(
                            item.Heading.Text,
                            titleFont
                        ).Width +
                        Math.Max(8, CInt(12 * s))
                    )
                )

            item.Badge.SetBounds(
                Math.Max(0, (cardW - badgeW) \ 2),
                badgeY,
                badgeW,
                badgeH
            )

            item.Heading.SetBounds(
                0,
                0,
                badgeW,
                badgeH
            )

            ' ROLE DESCRIPTION

            Dim descY As Integer =
                Math.Min(
                    Math.Max(0, CInt(39 * s)),
                    Math.Max(0, cardH - 1)
                )

            Dim descH As Integer =
                Math.Max(
                    1,
                    Math.Min(
                        CInt(17 * s),
                        cardH - descY - 2
                    )
                )

            item.Description.Font =
                FitFont(
                    item.Description.Text,
                    item.DescriptionFont,
                    s,
                    Math.Max(1, cardW - 8),
                    descH
                )

            item.Description.SetBounds(
                4,
                descY,
                Math.Max(1, cardW - 8),
                descH
            )

            item.Card.Visible = True
            item.Badge.Visible = True
            item.Heading.Visible = True
            item.Description.Visible = True

            item.Card.BringToFront()
            item.Badge.BringToFront()
            item.Heading.BringToFront()
            item.Description.BringToFront()

        Next

    End Sub

    ' ==========================================
    ' RESIZE LEFT TITLE PANELS
    ' ==========================================

    Private Sub ResizeTitlePanels()

        For Each info As PanelInfo In otherTitlePanels

            Dim t As Control = info.Target

            If t.IsDisposed OrElse
               t.Parent Is Nothing OrElse
               t.Dock <> DockStyle.None Then Continue For

            Dim sx As Double =
                t.Parent.ClientSize.Width /
                CDbl(Math.Max(1, info.ParentSize.Width))

            Dim sy As Double =
                t.Parent.ClientSize.Height /
                CDbl(Math.Max(1, info.ParentSize.Height))

            Dim b As Rectangle = info.Bounds

            t.SetBounds(
                CInt(b.X * sx),
                CInt(b.Y * sy),
                Math.Max(1, CInt(b.Width * sx)),
                Math.Max(
                    1,
                    CInt(
                        b.Height *
                        sy *
                        RoundedPanelHeightScale
                    )
                )
            )

        Next

    End Sub

    ' ==========================================
    ' FIND LEFT BOP TITLE PANEL
    ' ==========================================

    Private Function FindBOPTitle(
        info As LabelInfo
    ) As Control

        If Not Object.ReferenceEquals(
            info.Target,
            BOPLabel
        ) Then Return Nothing

        Dim center As New Point(
            info.Bounds.Left + info.Bounds.Width \ 2,
            info.Bounds.Top + info.Bounds.Height \ 2
        )

        For Each panel As PanelInfo In otherTitlePanels

            If Object.ReferenceEquals(
                panel.Target.Parent,
                info.Target.Parent
            ) AndAlso
               panel.Bounds.Contains(center) Then

                Return panel.Target

            End If

        Next

        Return Nothing

    End Function

    ' ==========================================
    ' RESIZE LEFT LABELS
    ' ==========================================

    Private Sub ResizeLeftLabels()

        For Each info As LabelInfo In leftLabels

            Dim t As Control = info.Target

            If t.IsDisposed OrElse
               t.Parent Is Nothing Then Continue For

            Dim p As Control = t.Parent

            Dim sx As Double =
                p.ClientSize.Width /
                CDbl(Math.Max(1, info.ParentSize.Width))

            Dim sy As Double =
                p.ClientSize.Height /
                CDbl(Math.Max(1, info.ParentSize.Height))

            Dim scale As Double =
                Math.Max(
                    0.6,
                    Math.Min(2.5, Math.Min(sx, sy))
                )

            Dim x As Integer =
                Math.Max(0, CInt(info.Bounds.X * sx))

            Dim y As Integer =
                Math.Max(0, CInt(info.Bounds.Y * sy))

            Dim w As Integer =
                Math.Max(
                    1,
                    p.ClientSize.Width - x - 10
                )

            Dim availableH As Integer =
                Math.Max(
                    1,
                    p.ClientSize.Height - y - 4
                )

            Dim bop As Control = FindBOPTitle(info)

            If bop IsNot Nothing Then

                x = Math.Max(x, bop.Left + 5)

                w = Math.Max(
                    1,
                    bop.Right - x - 5
                )

                availableH =
                    Math.Max(1, bop.Height - 4)

            End If

            If info.BottomAligned Then

                availableH =
                    Math.Max(
                        1,
                        p.ClientSize.Height - 10
                    )

            End If

            Dim f As Font =
                FitFont(
                    t.Text,
                    info.BaseFont,
                    scale,
                    w,
                    availableH
                )

            Dim measured As Size =
                TextRenderer.MeasureText(
                    If(t.Text, " "),
                    f
                )

            Dim h As Integer =
                Math.Max(
                    1,
                    Math.Min(
                        availableH,
                        Math.Max(
                            CInt(info.Bounds.Height * sy),
                            measured.Height + 4
                        )
                    )
                )

            If bop IsNot Nothing Then

                y = bop.Top +
                    Math.Max(
                        0,
                        (bop.Height - h) \ 2
                    )

            ElseIf info.BottomAligned Then

                Dim margin As Integer =
                    info.ParentSize.Height -
                    info.Bounds.Bottom

                y = Math.Max(
                    0,
                    p.ClientSize.Height -
                    Math.Max(5, CInt(margin * sy)) -
                    h
                )

            End If

            h = Math.Max(
                1,
                Math.Min(
                    h,
                    p.ClientSize.Height - y
                )
            )

            t.Font = f
            t.SetBounds(x, y, w, h)

        Next

    End Sub

    ' ==========================================
    ' DETECT TEXT HINT CONTROLS
    ' ==========================================

    Private Function IsHintControl(
        t As Control
    ) As Boolean

        Return (Object.ReferenceEquals(t, EmailField) OrElse
                Object.ReferenceEquals(t, PassField) OrElse
                Object.ReferenceEquals(t, RoleDropdown) OrElse
                Object.ReferenceEquals(t, BranchDropdown))

    End Function

    ' ==========================================
    ' GET TEXT HINT
    ' ==========================================

    Private Function GetHintText(t As Control) As String

        For Each propName As String In {
            "TextHint",
            "PlaceholderText",
            "HintText",
            "WatermarkText"
        }

            Dim prop = t.GetType().GetProperty(propName)

            If prop IsNot Nothing AndAlso
               prop.CanRead AndAlso
               prop.PropertyType Is GetType(String) Then

                Try

                    Dim value As String =
                        TryCast(prop.GetValue(t), String)

                    If Not String.IsNullOrWhiteSpace(value) Then
                        Return value
                    End If

                Catch
                End Try

            End If

        Next

        If Object.ReferenceEquals(t, EmailField) Then
            Return "Enter your email address"
        End If

        If Object.ReferenceEquals(t, PassField) Then
            Return "Enter your password"
        End If

        If Object.ReferenceEquals(t, RoleDropdown) Then
            Return "Select Role"
        End If

        If Object.ReferenceEquals(t, BranchDropdown) Then
            Return "Select Branch"
        End If

        Return String.Empty

    End Function

    ' ==========================================
    ' APPLY RESPONSIVE TEXT HINT FONT
    ' ==========================================

    Private Sub SetHintFont(
        t As Control,
        f As Font
    )

        Dim fontProperties As String() = {
            "TextHintFont",
            "HintFont",
            "PlaceholderFont",
            "WatermarkFont"
        }

        For Each propName As String In fontProperties

            Dim prop = t.GetType().GetProperty(propName)

            If prop IsNot Nothing AndAlso
               prop.CanWrite AndAlso
               prop.PropertyType Is GetType(Font) Then

                Try
                    prop.SetValue(t, f)
                Catch
                End Try

            End If

        Next

        Dim sizeProperties As String() = {
            "TextHintFontSize",
            "HintFontSize",
            "PlaceholderFontSize",
            "WatermarkFontSize",
            "TextHintSize"
        }

        For Each propName As String In sizeProperties

            Dim prop = t.GetType().GetProperty(propName)

            If prop Is Nothing OrElse
               Not prop.CanWrite Then Continue For

            Try

                If prop.PropertyType Is GetType(Single) Then

                    prop.SetValue(t, CSng(f.SizeInPoints))

                ElseIf prop.PropertyType Is GetType(Double) Then

                    prop.SetValue(t, CDbl(f.SizeInPoints))

                ElseIf prop.PropertyType Is GetType(Integer) Then

                    prop.SetValue(
                        t,
                        CInt(Math.Round(f.SizeInPoints))
                    )

                ElseIf prop.PropertyType Is GetType(Decimal) Then

                    prop.SetValue(t, CDec(f.SizeInPoints))

                End If

            Catch
            End Try

        Next

        SyncInnerFonts(t, f, GetHintText(t))
        t.Invalidate(True)

    End Sub

    ' ==========================================
    ' SYNC INNER CONTROL FONTS
    ' ==========================================

    Private Sub SyncInnerFonts(
        root As Control,
        f As Font,
        hint As String
    )

        For Each child As Control In root.Controls

            If TypeOf child Is TextBoxBase OrElse
               TypeOf child Is ComboBox Then

                child.Font = f

            ElseIf TypeOf child Is Label Then

                Dim n As String =
                    If(child.Name, "").ToLowerInvariant()

                If n.Contains("hint") OrElse
                   n.Contains("placeholder") OrElse
                   n.Contains("watermark") OrElse
                   n.Contains("prompt") OrElse
                   String.Equals(
                       child.Text,
                       hint,
                       StringComparison.OrdinalIgnoreCase
                   ) Then

                    child.Font = f

                End If

            End If

            SyncInnerFonts(child, f, hint)

        Next

    End Sub

    ' ==========================================
    ' POSITION RIGHT LOGIN CONTROLS
    ' ==========================================

    Private Sub PlaceLoginControl(
        t As Control,
        x As Double,
        y As Double,
        w As Double,
        h As Double,
        Optional fontFactor As Double = 1.0
    )

        If loginCard Is Nothing OrElse
           t Is Nothing OrElse
           t.IsDisposed Then Return

        Dim s As Double =
            Math.Min(
                loginCard.Width / DesignLoginWidth,
                loginCard.Height / DesignLoginHeight
            )

        Dim xx As Integer = Math.Max(0, CInt(x * s))
        Dim yy As Integer = Math.Max(0, CInt(y * s))

        Dim ww As Integer =
            Math.Max(
                1,
                Math.Min(
                    CInt(w * s),
                    loginCard.ClientSize.Width - xx
                )
            )

        Dim hh As Integer =
            Math.Max(
                1,
                Math.Min(
                    CInt(h * s),
                    loginCard.ClientSize.Height - yy
                )
            )

        t.SetBounds(xx, yy, ww, hh)

        If Not inputFonts.ContainsKey(t) Then Return

        Dim content As String =
            If(IsHintControl(t), GetHintText(t), t.Text)

        Dim f As Font =
            FitFont(
                content,
                inputFonts(t),
                s * fontFactor,
                Math.Max(1, ww - 12),
                Math.Max(1, hh - 2)
            )

        t.Font = f

        If IsHintControl(t) Then
            SetHintFont(t, f)
        End If

    End Sub

    ' ==========================================
    ' RIGHT LOGIN CONTENT SPACING
    ' ==========================================

    Private Sub LayoutLoginContents()

        If loginCard Is Nothing Then Return

        Const l As Double = 34
        Const full As Double = 322
        Const gap As Double = 24
        Const half As Double = (full - gap) / 2

        ' WELCOME HEADER

        PlaceLoginControl(WbLabel, l, 27, full, 27)
        PlaceLoginControl(desc3, l, 54, full, 18)

        ' EMAIL

        PlaceLoginControl(EmailLabel, l, 85, full, 17)

        PlaceLoginControl(
            EmailField,
            l, 105,
            full, 48,
            FieldHintFontScale
        )

        ' PASSWORD

        PlaceLoginControl(PassLabel, l, 160, full, 17)

        PlaceLoginControl(
            PassField,
            l, 179,
            full, 48,
            FieldHintFontScale
        )

        ' ROLE AND BRANCH

        PlaceLoginControl(
            RoleLabel,
            l, 229,
            half, 17
        )

        PlaceLoginControl(
            BranchLabel,
            l + half + gap, 229,
            half, 17
        )

        PlaceLoginControl(
            RoleDropdown,
            l, 246,
            half, 47,
            ComboHintFontScale
        )

        PlaceLoginControl(
            BranchDropdown,
            l + half + gap, 246,
            half, 47,
            ComboHintFontScale
        )

        ' SIGN IN

        PlaceLoginControl(
            signinButton,
            l, 312,
            full, 35
        )

        ' FORGOT PASSWORD

        PlaceLoginControl(
            forgotpass,
            l, 357,
            full, 23
        )

        If TypeOf forgotpass Is Label Then

            Dim lbl As Label =
                DirectCast(forgotpass, Label)

            lbl.AutoSize = False
            lbl.TextAlign = ContentAlignment.MiddleCenter

        End If

        forgotpass.BringToFront()

    End Sub

    ' ==========================================
    ' FIND INNER PASSWORD TEXTBOX
    ' ==========================================

    Private Function FindPasswordTextBox(
        root As Control
    ) As TextBox

        If root Is Nothing Then Return Nothing

        If TypeOf root Is TextBox Then
            Return DirectCast(root, TextBox)
        End If

        For Each child As Control In root.Controls

            Dim found As TextBox =
                FindPasswordTextBox(child)

            If found IsNot Nothing Then
                Return found
            End If

        Next

        Return Nothing

    End Function

    ' ==========================================
    ' PASSWORD MASKING
    ' ==========================================

    Private Function SetPasswordMask(
        root As Control,
        masked As Boolean
    ) As Boolean

        If root Is Nothing OrElse
           root.IsDisposed Then Return False

        Dim changed As Boolean = False

        If TypeOf root Is TextBox Then

            DirectCast(root, TextBox).UseSystemPasswordChar =
                masked

            changed = True

        Else

            For Each propName As String In {
                "UseSystemPasswordChar",
                "IsPassword",
                "PasswordMode"
            }

                Dim prop =
                    root.GetType().GetProperty(propName)

                If prop Is Nothing OrElse
                   Not prop.CanWrite OrElse
                   prop.PropertyType IsNot GetType(Boolean) Then

                    Continue For

                End If

                Try

                    prop.SetValue(root, masked)

                    changed = True
                    Exit For

                Catch
                End Try

            Next

            If Not changed Then

                Dim prop =
                    root.GetType().GetProperty("PasswordChar")

                If prop IsNot Nothing AndAlso
                   prop.CanWrite AndAlso
                   prop.PropertyType Is GetType(Char) Then

                    Try

                        prop.SetValue(
                            root,
                            If(
                                masked,
                                ChrW(&H25CF),
                                ChrW(0)
                            )
                        )

                        changed = True

                    Catch
                    End Try

                End If

            End If

        End If

        For Each child As Control In root.Controls

            If SetPasswordMask(child, masked) Then
                changed = True
            End If

        Next

        Return changed

    End Function

    ' ==========================================
    ' CREATE PASSWORD EYE ICON
    ' ==========================================

    Private Sub SetupPasswordToggle()

        If passwordToggleButton IsNot Nothing OrElse
           PassField Is Nothing OrElse
           PassField.Parent Is Nothing Then Return

        If Not SetPasswordMask(PassField, True) Then Return

        passwordVisible = False

        passwordToggleButton = New LoginFormEyeToggle With {
            .Name = "passwordToggleButton",
            .PasswordVisible = False,
            .BackColor = Color.White,
            .Size = New Size(26, 26),
            .AccessibleName = "Show password",
            .TabStop = True
        }

        AddHandler passwordToggleButton.Click,
            AddressOf PasswordToggle_Click

        PassField.Parent.Controls.Add(passwordToggleButton)

        passwordEyeTooltip.SetToolTip(
            passwordToggleButton,
            "Show password"
        )

        PositionPasswordToggle()

    End Sub

    ' ==========================================
    ' SHOW/HIDE PASSWORD CLICK
    ' ==========================================

    Private Sub PasswordToggle_Click(
        sender As Object,
        e As EventArgs
    )

        Dim showPassword As Boolean =
            Not passwordVisible

        If Not SetPasswordMask(
            PassField,
            Not showPassword
        ) Then Return

        passwordVisible = showPassword

        passwordToggleButton.PasswordVisible =
            showPassword

        Dim description As String =
            If(
                showPassword,
                "Hide password",
                "Show password"
            )

        passwordToggleButton.AccessibleName = description

        passwordEyeTooltip.SetToolTip(
            passwordToggleButton,
            description
        )

        PositionPasswordToggle()

        Dim input As TextBox =
            FindPasswordTextBox(PassField)

        If input IsNot Nothing Then

            input.Focus()

        Else

            PassField.Focus()

        End If

    End Sub

    ' ==========================================
    ' RESPONSIVE EYE ICON POSITION
    ' ==========================================

    Private Sub PositionPasswordToggle()

        If passwordToggleButton Is Nothing OrElse
           passwordToggleButton.IsDisposed OrElse
           PassField Is Nothing OrElse
           PassField.IsDisposed OrElse
           PassField.Parent Is Nothing Then Return

        Dim iconSize As Integer =
            Math.Min(
                28,
                Math.Max(18, PassField.Height - 12)
            )

        Dim x As Integer =
            PassField.Right - iconSize - 10

        Dim y As Integer =
            PassField.Top +
            (PassField.Height - iconSize) \ 2

        passwordToggleButton.SetBounds(
            x,
            y,
            iconSize,
            iconSize
        )

        passwordToggleButton.Visible =
            (PassField.Width >= 90 AndAlso
             PassField.Height >= 26)

        If passwordToggleButton.Visible Then
            passwordToggleButton.BringToFront()
        End If

    End Sub

    ' ==========================================
    ' UPDATE RESPONSIVE LAYOUT
    ' ==========================================

    Private Sub UpdateResponsiveLayout()

        If Not layoutReady OrElse
           updatingLayout OrElse
           Me.WindowState = FormWindowState.Minimized OrElse
           Me.ClientSize.Width <= 0 OrElse
           Me.ClientSize.Height <= 0 Then Return

        updatingLayout = True

        Me.SuspendLayout()

        Try

            ResizeLoginCard()
            ResizeTitlePanels()
            LayoutRoleCards()
            ResizeLeftLabels()
            LayoutLoginContents()
            PositionPasswordToggle()

        Finally

            Me.ResumeLayout()
            updatingLayout = False

        End Try

    End Sub

    ' ==========================================
    ' PARENT RESIZE
    ' ==========================================

    Private Sub ParentContainer_Resize(
        sender As Object,
        e As EventArgs
    )

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' FORM RESIZE
    ' ==========================================

    Private Sub LoginForm_Resize(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Resize

        UpdateResponsiveLayout()

    End Sub

    ' ==========================================
    ' CLEANUP
    ' ==========================================

    Protected Overrides Sub OnFormClosed(
        e As FormClosedEventArgs
    )

        If passwordToggleButton IsNot Nothing Then

            RemoveHandler passwordToggleButton.Click,
                AddressOf PasswordToggle_Click

        End If

        For Each p As Control In watchedParents

            RemoveHandler p.Resize,
                AddressOf ParentContainer_Resize

        Next

        MyBase.OnFormClosed(e)

        For Each f As Font In fontCache.Values
            f.Dispose()
        Next

        fontCache.Clear()

        passwordEyeTooltip.Dispose()

    End Sub


    ' ==========================================
    ' PASSWORD EYE CONTROL: PRIVATE TO LOGINFORM
    ' PREVENTS DUPLICATE-CLASS COMPILER ERRORS
    ' ==========================================

    Private NotInheritable Class LoginFormEyeToggle
        Inherits Control

        Private _passwordVisible As Boolean = False
        Private _hovered As Boolean = False

        ' ==========================================
        ' PASSWORD VISIBILITY PROPERTY
        ' ==========================================

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property PasswordVisible As Boolean

            Get
                Return _passwordVisible
            End Get

            Set(value As Boolean)

                If _passwordVisible = value Then Return

                _passwordVisible = value
                Me.Invalidate()

            End Set

        End Property

        ' ==========================================
        ' CONSTRUCTOR
        ' ==========================================

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

            Me.BackColor = Color.White
            Me.Size = New Size(26, 26)

            Me.Cursor = Cursors.Hand
            Me.TabStop = True

            Me.AccessibleRole = AccessibleRole.PushButton
            Me.AccessibleName = "Show password"

        End Sub

        ' ==========================================
        ' MOUSE HOVER
        ' ==========================================

        Protected Overrides Sub OnMouseEnter(
            e As EventArgs
        )

            MyBase.OnMouseEnter(e)

            _hovered = True
            Me.Invalidate()

        End Sub

        Protected Overrides Sub OnMouseLeave(
            e As EventArgs
        )

            MyBase.OnMouseLeave(e)

            _hovered = False
            Me.Invalidate()

        End Sub

        ' ==========================================
        ' KEYBOARD FOCUS
        ' ==========================================

        Protected Overrides Sub OnGotFocus(
            e As EventArgs
        )

            MyBase.OnGotFocus(e)
            Me.Invalidate()

        End Sub

        Protected Overrides Sub OnLostFocus(
            e As EventArgs
        )

            MyBase.OnLostFocus(e)
            Me.Invalidate()

        End Sub

        ' ==========================================
        ' ENTER / SPACE KEY SUPPORT
        ' ==========================================

        Protected Overrides Sub OnKeyDown(
            e As KeyEventArgs
        )

            If e.KeyCode = Keys.Space OrElse
               e.KeyCode = Keys.Enter Then

                OnClick(EventArgs.Empty)

                e.Handled = True
                e.SuppressKeyPress = True

            Else

                MyBase.OnKeyDown(e)

            End If

        End Sub

        ' ==========================================
        ' DRAW EYE ICON
        ' ==========================================

        Protected Overrides Sub OnPaint(
            e As PaintEventArgs
        )

            MyBase.OnPaintBackground(e)

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias

            Dim cx As Single = Me.Width / 2.0F
            Dim cy As Single = Me.Height / 2.0F

            Dim halfW As Single =
                Math.Max(
                    4.0F,
                    Math.Min(
                        9.0F,
                        (Me.Width - 6.0F) / 2.0F
                    )
                )

            Dim halfH As Single = halfW * 0.6F

            ' GRAY DEFAULT, ORANGE ON HOVER

            Dim eyeColor As Color =
                If(
                    _hovered OrElse Me.Focused,
                    Color.FromArgb(232, 93, 4),
                    Color.FromArgb(120, 128, 140)
                )

            Using eyePen As New Pen(eyeColor, 1.7F)

                eyePen.StartCap = LineCap.Round
                eyePen.EndCap = LineCap.Round
                eyePen.LineJoin = LineJoin.Round

                ' ==================================
                ' DRAW EYE OUTLINE
                ' ==================================

                Using outline As New GraphicsPath()

                    outline.AddBezier(
                        New PointF(cx - halfW, cy),
                        New PointF(
                            cx - halfW / 2,
                            cy - halfH
                        ),
                        New PointF(
                            cx + halfW / 2,
                            cy - halfH
                        ),
                        New PointF(cx + halfW, cy)
                    )

                    outline.AddBezier(
                        New PointF(cx + halfW, cy),
                        New PointF(
                            cx + halfW / 2,
                            cy + halfH
                        ),
                        New PointF(
                            cx - halfW / 2,
                            cy + halfH
                        ),
                        New PointF(cx - halfW, cy)
                    )

                    e.Graphics.DrawPath(eyePen, outline)

                End Using

                ' ==================================
                ' DRAW EYE PUPIL
                ' ==================================

                Using iris As New SolidBrush(eyeColor)

                    e.Graphics.FillEllipse(
                        iris,
                        cx - 2.4F,
                        cy - 2.4F,
                        4.8F,
                        4.8F
                    )

                End Using

                ' ==================================
                ' DRAW SLASH WHEN PASSWORD IS VISIBLE
                ' ==================================

                If _passwordVisible Then

                    Using underlay As New Pen(
                        Me.BackColor,
                        4.0F
                    )

                        e.Graphics.DrawLine(
                            underlay,
                            cx - halfW - 1,
                            cy + halfH + 2,
                            cx + halfW + 1,
                            cy - halfH - 2
                        )

                    End Using

                    e.Graphics.DrawLine(
                        eyePen,
                        cx - halfW - 1,
                        cy + halfH + 2,
                        cx + halfW + 1,
                        cy - halfH - 2
                    )

                End If

            End Using

        End Sub

    End Class

End Class


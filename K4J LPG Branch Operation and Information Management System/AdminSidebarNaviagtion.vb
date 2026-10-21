
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms

Public Class AdminSidebarNaviagtion

    ' ==========================================
    ' EVENTS
    ' ==========================================

    Public Event NavigateRequested(pageKey As String)
    Public Event SignOutRequested()

    ' ==========================================
    ' STATE
    ' ==========================================

    Private ReadOnly _buttons As New Dictionary(Of String, Control)(
        StringComparer.OrdinalIgnoreCase)

    Private ReadOnly _radii As New Dictionary(Of Control, Integer)
    Private ReadOnly _fonts As New Dictionary(Of Control, Font)

    Private ReadOnly _normalIcons As New Dictionary(Of Control, String)
    Private ReadOnly _whiteIcons As New Dictionary(Of Control, String)

    Private ReadOnly _originalIconColors As New Dictionary(Of Control, Color)

    Private _activePage As String = String.Empty

    Private _layoutReady As Boolean = False
    Private _layoutBusy As Boolean = False
    Private _navigationBusy As Boolean

    ' ==========================================
    ' COLORS
    ' ==========================================

    Private ReadOnly NormalText As Color =
        Color.FromArgb(140, 160, 200)

    Private ReadOnly HoverOrange As Color =
        Color.FromArgb(232, 101, 10)

    ' RED HOVER ONLY FOR THE SIGN OUT BUTTON
    Private ReadOnly SignOutHoverRed As Color =
        Color.FromArgb(220, 53, 69)

    Private ReadOnly ActiveFill As Color =
        Color.FromArgb(30, 48, 84)

    Private ReadOnly ActiveBorder As Color =
        Color.FromArgb(223, 149, 63)

    ' ==========================================
    ' RESPONSIVE SETTINGS
    ' ==========================================

    Private Const HoverRadius As Integer = 6

    Private Const BaseLeft As Integer = 20
    Private Const LogoGap As Integer = 7

    ' ==========================================
    ' PANELS
    ' ==========================================

    Private headerPanel As Panel
    Private navigationPanel As Panel
    Private footerPanel As Panel

    ' Internal viewport: no WinForms automatic scrollbars are enabled.
    ' These names are intentionally unique to avoid Designer conflicts.
    Private _navItemsSurface As Panel
    Private _navVerticalOnly As VScrollBar



    ' INITIALS CONTROLS
    Private _initialsTile As Control
    Private _initialsText As Control

    ' ==========================================
    ' CONSTRUCTOR
    ' ==========================================

    Public Sub New()

        InitializeComponent()
        branchLabel.Text = "Select Branch"

        Me.DoubleBuffered = True
        Me.AutoScroll = False
        Me.AutoSize = False
        Me.AutoScrollMinSize = Size.Empty

        ' REGISTER NAVIGATION BUTTONS

        _buttons("Dashboard") = dashboardButton
        _buttons("Customers") = customersButton
        _buttons("Products") = productsButton
        _buttons("Global Inventory") = globalInventoryButton
        _buttons("Create Order") = createOrderButton
        _buttons("Assign Rider") = assignRiderButton
        _buttons("Deliveries") = deliveriesButton
        _buttons("Debt / Unpaid") = debtButton
        _buttons("Opening Checklist") = openingChecklistButton
        _buttons("Closing Checklist") = closingChecklistButton
        _buttons("EOD Sales") = eodButton

        ' REGISTER NAVIGATION EVENTS

        For Each pair As KeyValuePair(Of String, Control) In _buttons

            AddHandler pair.Value.Click,
                AddressOf Navigation_Click

            AddHandler pair.Value.MouseEnter,
                AddressOf Navigation_Enter

            AddHandler pair.Value.MouseLeave,
                AddressOf Navigation_Leave

            RememberRadius(pair.Value)

        Next

        ' SIGN OUT EVENTS

        AddHandler signOutButton.Click,
            AddressOf SignOut_Click

        AddHandler signOutButton.MouseEnter,
            AddressOf SignOut_Enter

        AddHandler signOutButton.MouseLeave,
            AddressOf SignOut_Leave

        RememberRadius(signOutButton)

        AddHandler Me.Disposed,
            AddressOf Sidebar_Disposed

        ' INITIALIZE ICONS

        ApplyIcons()

        ' NORMAL BUTTON STYLE

        For Each btn As Control In _buttons.Values
            ApplyNormalStyle(btn)
        Next

        ApplyNormalStyle(signOutButton)

    End Sub

    ' ==========================================
    ' DPI RESPONSIVE SCALING
    ' ==========================================

    Private Function Px(value As Integer) As Integer

        Dim scale As Double =
            Math.Max(0.75, Me.DeviceDpi / 96.0R)

        Return Math.Max(
            1,
            CInt(value * scale)
        )

    End Function

    ' ==========================================
    ' COMMON LEFT ALIGNMENT
    ' ==========================================

    Private Function LeftEdge() As Integer

        Return Math.Min(
            Px(BaseLeft),
            Math.Max(
                Px(6),
                Me.ClientSize.Width \ 10
            )
        )

    End Function

    ' ==========================================
    ' IDENTIFY NAVIGATION BUTTON
    ' ==========================================

    Private Function GetPage(
        btn As Control
    ) As String

        If btn Is Nothing Then
            Return String.Empty
        End If

        For Each pair As KeyValuePair(Of String, Control) In _buttons

            If Object.ReferenceEquals(pair.Value, btn) Then
                Return pair.Key
            End If

        Next

        Return String.Empty

    End Function

    ' ==========================================
    ' NAVIGATION CLICK
    ' ==========================================

    Private Sub Navigation_Click(
        sender As Object,
        e As EventArgs
    )

        Dim page As String =
            GetPage(TryCast(sender, Control))

        If page <> String.Empty Then

            If _navigationBusy Then Return
            _navigationBusy = True
            Try
                Dim host As Control = TopLevelControl
                If host Is Nothing Then host = Me
                Using transition As New PanelTransition(host)
                    RaiseEvent NavigateRequested(page)
                End Using
            Finally
                _navigationBusy = False
            End Try

        End If

    End Sub

    ' ==========================================
    ' NAVIGATION HOVER
    ' ==========================================

    Private Sub Navigation_Enter(
        sender As Object,
        e As EventArgs
    )

        Dim btn As Control =
            TryCast(sender, Control)

        If btn Is Nothing Then Return

        ApplyHoverStyle(btn)

    End Sub

    Private Sub Navigation_Leave(
        sender As Object,
        e As EventArgs
    )

        Dim btn As Control =
            TryCast(sender, Control)

        If btn Is Nothing Then Return

        RestoreButtonStyle(btn)

    End Sub

    ' ==========================================
    ' SIGN OUT
    ' ==========================================

    Private Sub SignOut_Click(
        sender As Object,
        e As EventArgs
    )

        RaiseEvent SignOutRequested()

    End Sub

    Private Sub SignOut_Enter(
        sender As Object,
        e As EventArgs
    )

        ' Red background and white text/icon on hover.
        ' Other menu items still use the original orange hover.
        ApplyStyle(
            signOutButton,
            SignOutHoverRed,
            Color.Transparent,
            0,
            Color.White
        )

        SetRadius(signOutButton, True)
        SetIconWhite(signOutButton, True)

    End Sub

    Private Sub SignOut_Leave(
        sender As Object,
        e As EventArgs
    )

        ApplyNormalStyle(signOutButton)

    End Sub

    ' ==========================================
    ' SET ACTIVE PAGE
    ' ==========================================

    Public Sub SetActivePage(
        pageKey As String
    )

        _activePage =
            If(pageKey, String.Empty)

        For Each btn As Control In _buttons.Values

            RestoreButtonStyle(btn)

        Next

    End Sub

    ' ==========================================
    ' SET USER INFORMATION
    ' ==========================================

    ' Supply the branch selected at login or assigned to the current admin.
    <Category("Data"), DefaultValue("")>
    Public Property BranchName As String
        Get
            Return If(branchLabel.Text = "Select Branch", String.Empty, branchLabel.Text)
        End Get
        Set(value As String)
            Dim branch As String = If(value, String.Empty).Trim()
            Select Case branch.ToUpperInvariant()
                Case "JPANG", "JPANG BRANCH", "JOSE PANGANIBAN", "JOSE PANGANIBAN BRANCH"
                    branch = "Jose Panganiban Branch"
                Case "DAET", "DAET BRANCH"
                    branch = "Daet Branch"
                Case ""
                    branch = "Select Branch"
                Case Else
                    Throw New ArgumentException("Choose Jose Panganiban Branch or Daet Branch.", NameOf(value))
            End Select
            branchLabel.Text = branch
            If _layoutReady Then UpdateResponsiveSidebar()
        End Set
    End Property

    Public Sub SetBranch(branchName As String)
        Me.BranchName = branchName
    End Sub

    Public Overloads Sub SetUser(userName As String, role As String, branchName As String)
        SetBranch(branchName)
        SetUser(userName, role)
    End Sub

    Public Overloads Sub SetUser(
        userName As String,
        role As String
    )

        userNameLabel.Text =
            If(userName, String.Empty)

        userRoleLabel.Text =
            If(role, String.Empty)

        UpdateInitials()

        If _layoutReady Then
            UpdateResponsiveSidebar()
        End If

    End Sub

    ' ==========================================
    ' AUTOMATIC USER INITIALS
    ' ==========================================

    Private Sub UpdateInitials()

        Dim fullName As String =
            If(userNameLabel.Text, "").Trim()

        Dim parts As String() =
            fullName.Split(
                New Char() {" "c, ChrW(9)},
                StringSplitOptions.RemoveEmptyEntries
            )

        Dim initials As String = "US"

        If parts.Length >= 2 Then

            initials =
                parts(0).Substring(0, 1) &
                parts(parts.Length - 1).Substring(0, 1)

        ElseIf parts.Length = 1 Then

            initials = parts(0).Substring(
                0,
                Math.Min(2, parts(0).Length)
            )

        End If

        If _initialsText IsNot Nothing Then

            _initialsText.Text =
                initials.ToUpperInvariant()

        End If

    End Sub

    ' ==========================================
    ' NORMAL BUTTON STYLE
    ' ==========================================

    Private Sub ApplyNormalStyle(
        btn As Control
    )

        If btn Is Nothing Then Return

        SetIconWhite(btn, False)
        SetRadius(btn, False)

        ApplyStyle(
            btn,
            Color.Transparent,
            Color.Transparent,
            0,
            NormalText
        )

    End Sub

    ' ==========================================
    ' RESTORE BUTTON STYLE
    ' ==========================================

    Private Sub RestoreButtonStyle(
        btn As Control
    )

        If btn Is Nothing Then Return

        If GetPage(btn).Equals(
            _activePage,
            StringComparison.OrdinalIgnoreCase
        ) AndAlso _activePage <> String.Empty Then

            SetIconWhite(btn, True)
            SetRadius(btn, False)

            ApplyStyle(
                btn,
                ActiveFill,
                ActiveBorder,
                2,
                Color.White
            )

        Else

            ApplyNormalStyle(btn)

        End If

    End Sub

    ' ==========================================
    ' ORANGE HOVER STYLE
    ' ==========================================

    Private Sub ApplyHoverStyle(
        btn As Control
    )

        If btn Is Nothing Then Return

        ApplyStyle(
            btn,
            HoverOrange,
            Color.Transparent,
            0,
            Color.White
        )

        SetRadius(btn, True)
        SetIconWhite(btn, True)

    End Sub

    ' ==========================================
    ' APPLY BUTTON COLORS
    ' ==========================================

    Private Sub ApplyStyle(
        btn As Control,
        fill As Color,
        border As Color,
        thickness As Integer,
        textColor As Color
    )

        Dim rb As RoundedButton =
            TryCast(btn, RoundedButton)

        If rb IsNot Nothing Then

            rb.FillColor = fill
            rb.BorderColor = border
            rb.BorderThickness = thickness
            rb.ForeColor = textColor

        ElseIf btn IsNot Nothing Then

            btn.BackColor = fill
            btn.ForeColor = textColor

        End If

        If btn IsNot Nothing Then
            btn.Invalidate()
        End If

    End Sub

    ' ==========================================
    ' GET CORNER RADIUS PROPERTY
    ' ==========================================

    Private Function RadiusProperty(
        btn As Control
    ) As PropertyDescriptor

        If Not TypeOf btn Is RoundedButton Then
            Return Nothing
        End If

        Dim props As PropertyDescriptorCollection =
            TypeDescriptor.GetProperties(btn)

        For Each propertyName As String In {
            "CornerRadius",
            "BorderRadius",
            "Radius",
            "RoundRadius"
        }

            Dim prop As PropertyDescriptor =
                props.Find(propertyName, True)

            If prop IsNot Nothing AndAlso
               Not prop.IsReadOnly AndAlso
               (prop.PropertyType Is GetType(Integer) OrElse
                prop.PropertyType Is GetType(Single) OrElse
                prop.PropertyType Is GetType(Double)) Then

                Return prop

            End If

        Next

        Return Nothing

    End Function

    ' ==========================================
    ' REMEMBER ORIGINAL RADIUS
    ' ==========================================

    Private Sub RememberRadius(
        btn As Control
    )

        If btn Is Nothing OrElse
           _radii.ContainsKey(btn) Then Return

        Dim prop As PropertyDescriptor =
            RadiusProperty(btn)

        If prop Is Nothing Then Return

        Try

            _radii(btn) =
                Convert.ToInt32(prop.GetValue(btn))

        Catch ex As Exception

            Diagnostics.Debug.WriteLine(ex.Message)

        End Try

    End Sub

    ' ==========================================
    ' HOVER CORNER RADIUS
    ' ==========================================

    Private Sub SetRadius(
        btn As Control,
        hovered As Boolean
    )

        If btn Is Nothing OrElse
           btn.IsDisposed Then Return

        Dim prop As PropertyDescriptor =
            RadiusProperty(btn)

        If prop Is Nothing Then Return

        RememberRadius(btn)

        Dim normal As Integer

        If Not _radii.TryGetValue(
            btn,
            normal
        ) Then Return

        Dim radius As Integer =
            If(
                hovered,
                Math.Min(normal, HoverRadius),
                normal
            )

        Try

            prop.SetValue(
                btn,
                Convert.ChangeType(
                    radius,
                    prop.PropertyType
                )
            )

            btn.Invalidate()

        Catch ex As Exception

            Diagnostics.Debug.WriteLine(ex.Message)

        End Try

    End Sub

    ' ==========================================
    ' APPLY NAVIGATION ICONS
    ' ==========================================

    Private Sub ApplyIcons()
        ConfigureIcon(dashboardButton, "dashboard.png")
        ConfigureIcon(customersButton, "customer.png")
        ConfigureIcon(productsButton, "products.png")
        ConfigureIcon(globalInventoryButton, "inventory-line.png")
        ConfigureIcon(createOrderButton, "cart.png")
        ConfigureIcon(assignRiderButton, "rider.png")
        ConfigureIcon(deliveriesButton, "delivery-truck.png")
        ConfigureIcon(debtButton, "caution.png")
        ConfigureIcon(openingChecklistButton, "checklist.png")
        ConfigureIcon(closingChecklistButton, "checklist.png")
        ConfigureIcon(eodButton, "peso-sign.png")
        ConfigureIcon(signOutButton, "logout.png")
    End Sub

    ' ==========================================
    ' CONFIGURE BUTTON ICON
    ' ==========================================

    Private Sub ConfigureIcon(
        btn As Control,
        fileName As String
    )

        Dim rb As RoundedButton =
            TryCast(btn, RoundedButton)

        If rb Is Nothing Then Return

        rb.TextImageRelation =
            TextImageRelation.ImageBeforeText

        rb.ImageAlign =
            ContentAlignment.MiddleLeft

        rb.TextAlign =
            ContentAlignment.MiddleLeft

        rb.CenterIconAndText = False

        rb.IconWidth = Px(18)
        rb.IconHeight = Px(18)
        rb.IconTextSpacing = Px(11)

        _normalIcons(btn) = fileName

        rb.IconFileName = fileName

        ' CREATE WHITE VERSION OF ICON

        Dim sourcePath As String =
            FindAsset(fileName)

        If sourcePath <> String.Empty Then

            Dim whitePath As String =
                Path.Combine(
                    Path.GetDirectoryName(sourcePath),
                    Path.GetFileNameWithoutExtension(fileName) &
                    "_hover_white.png"
                )

            If CreateWhiteIcon(
                sourcePath,
                whitePath
            ) Then

                _whiteIcons(btn) =
                    Path.GetFileName(whitePath)

            End If

        End If

    End Sub

    ' ==========================================
    ' CREATE WHITE PNG ICON
    ' ==========================================

    Private Function CreateWhiteIcon(
        sourcePath As String,
        targetPath As String
    ) As Boolean

        Try

            If Not File.Exists(targetPath) Then

                Using original As New Bitmap(sourcePath)

                    Using result As New Bitmap(
                        original.Width,
                        original.Height,
                        PixelFormat.Format32bppArgb
                    )

                        For y As Integer = 0 To original.Height - 1

                            For x As Integer = 0 To original.Width - 1

                                Dim pixel As Color =
                                    original.GetPixel(x, y)

                                result.SetPixel(
                                    x,
                                    y,
                                    Color.FromArgb(
                                        pixel.A,
                                        255,
                                        255,
                                        255
                                    )
                                )

                            Next

                        Next

                        result.Save(
                            targetPath,
                            ImageFormat.Png
                        )

                    End Using

                End Using

            End If

            Return File.Exists(targetPath)

        Catch ex As Exception

            Diagnostics.Debug.WriteLine(
                "White icon error: " & ex.Message
            )

            Return False

        End Try

    End Function

    ' ==========================================
    ' CHANGE ICON TO WHITE ON HOVER
    ' ==========================================

    Private Sub SetIconWhite(
        btn As Control,
        makeWhite As Boolean
    )

        Dim rb As RoundedButton =
            TryCast(btn, RoundedButton)

        If rb Is Nothing Then Return

        ' CHECK FOR ICON TINT PROPERTY

        Dim props As PropertyDescriptorCollection =
            TypeDescriptor.GetProperties(rb)

        For Each propertyName As String In {
            "IconTintColor",
            "IconColor",
            "ImageTintColor",
            "IconForeColor"
        }

            Dim prop As PropertyDescriptor =
                props.Find(propertyName, True)

            If prop IsNot Nothing AndAlso
               Not prop.IsReadOnly AndAlso
               prop.PropertyType Is GetType(Color) Then

                Try

                    If Not _originalIconColors.ContainsKey(btn) Then

                        _originalIconColors(btn) =
                            DirectCast(
                                prop.GetValue(rb),
                                Color
                            )

                    End If

                    prop.SetValue(
                        rb,
                        If(
                            makeWhite,
                            Color.White,
                            _originalIconColors(btn)
                        )
                    )

                Catch ex As Exception

                    Diagnostics.Debug.WriteLine(ex.Message)

                End Try

                Exit For

            End If

        Next

        ' SWITCH BETWEEN NORMAL AND WHITE PNG

        Dim fileName As String = Nothing

        If makeWhite Then

            _whiteIcons.TryGetValue(
                btn,
                fileName
            )

        Else

            _normalIcons.TryGetValue(
                btn,
                fileName
            )

        End If

        If Not String.IsNullOrWhiteSpace(fileName) Then

            If rb.IconFileName <> fileName Then

                rb.IconFileName = fileName

            End If

        End If

        rb.Invalidate()

    End Sub

    ' ==========================================
    ' CONTROL LOAD
    ' ==========================================

    Protected Overrides Sub OnLoad(
        e As EventArgs
    )

        MyBase.OnLoad(e)

        If IsInDesignMode() Then Return
        If _layoutReady Then Return

        BuildResponsiveLayout()

        AddHandler Me.Resize,
            AddressOf Sidebar_Resize

    End Sub

    ' ==========================================
    ' DETECT DESIGN MODE
    ' ==========================================

    Private Function IsInDesignMode() As Boolean

        If LicenseManager.UsageMode =
            LicenseUsageMode.Designtime Then Return True

        If Me.DesignMode Then Return True

        Dim current As Control = Me.Parent

        While current IsNot Nothing

            If current.Site IsNot Nothing AndAlso
               current.Site.DesignMode Then

                Return True

            End If

            current = current.Parent

        End While

        Dim processName As String =
            Diagnostics.Process.GetCurrentProcess().ProcessName

        Return processName.Equals(
            "devenv",
            StringComparison.OrdinalIgnoreCase
        ) OrElse
        processName.Equals(
            "DesignToolsServer",
            StringComparison.OrdinalIgnoreCase
        ) OrElse
        processName.Equals(
            "XDesProc",
            StringComparison.OrdinalIgnoreCase
        )

    End Function

    ' ==========================================
    ' RESIZE EVENT
    ' ==========================================

    Private Sub Sidebar_Resize(
        sender As Object,
        e As EventArgs
    )

        UpdateResponsiveSidebar()

    End Sub

    ' ==========================================
    ' CREATE RESPONSIVE PANELS
    ' ==========================================

    Private Sub BuildResponsiveLayout()

        Me.SuspendLayout()

        ' HEADER

        headerPanel = New Panel With {
            .Dock = DockStyle.Top,
            .Height = Px(168),
            .BackColor = Me.BackColor,
            .Margin = New Padding(0)
        }

        ' NAVIGATION VIEWPORT - AUTOMATIC SCROLLING DISABLED

        navigationPanel = New Panel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = False,
            .AutoSize = False,
            .BackColor = Me.BackColor,
            .Margin = New Padding(0),
            .Padding = New Padding(0)
        }

        ' Scrollable menu content is clipped by navigationPanel.
        ' Its X position is always 0; only its Y position moves.
        _navItemsSurface = New Panel With {
            .Name = "sidebarMenuSurface",
            .Dock = DockStyle.None,
            .AutoScroll = False,
            .AutoSize = False,
            .BackColor = Me.BackColor,
            .Margin = New Padding(0),
            .Padding = New Padding(0)
        }

        _navVerticalOnly = New VScrollBar With {
            .Name = "sidebarVerticalOnly",
            .Dock = DockStyle.Right,
            .Width = SystemInformation.VerticalScrollBarWidth,
            .Visible = False,
            .Minimum = 0,
            .SmallChange = Px(35)
        }

        navigationPanel.Controls.Add(_navItemsSurface)
        navigationPanel.Controls.Add(_navVerticalOnly)
        _navVerticalOnly.BringToFront()

        AddHandler _navVerticalOnly.ValueChanged,
            AddressOf NavigationVertical_ValueChanged

        AddHandler navigationPanel.MouseWheel,
            AddressOf Navigation_MouseWheel

        AddHandler _navItemsSurface.MouseWheel,
            AddressOf Navigation_MouseWheel

        ' FOOTER

        footerPanel = New Panel With {
            .Dock = DockStyle.Bottom,
            .Height = Px(54),
            .BackColor = Me.BackColor,
            .Margin = New Padding(0)
        }

        Me.Controls.Add(navigationPanel)
        Me.Controls.Add(footerPanel)
        Me.Controls.Add(headerPanel)

        ' ======================================
        ' HEADER CONTROLS
        ' ======================================

        MoveControl(k4jTitle, headerPanel)
        MoveControl(branchLabel, headerPanel)

        SetupLogo()

        ' ======================================
        ' USER PROFILE CONTROLS
        ' ======================================

        MoveControl(
            userPlaceholder,
            headerPanel
        )

        SetupInitialsControls()

        MoveControl(
            _initialsTile,
            userPlaceholder
        )

        MoveControl(
            _initialsText,
            _initialsTile
        )

        MoveControl(
            userNameLabel,
            userPlaceholder
        )

        MoveControl(
            userRolePlaceholder,
            userPlaceholder
        )

        MoveControl(
            userRoleLabel,
            userRolePlaceholder
        )

        UpdateInitials()

        ' ======================================
        ' NAVIGATION CONTROLS
        ' ======================================

        SpacedLabel1.Visible = False
        Dim menuControls As Control() = {
            SpacedLabel2,
            dashboardButton,
            customerCateg,
            customersButton,
            catalogCateg,
            productsButton,
            globalInventoryButton,
            ordersCateg,
            createOrderButton,
            assignRiderButton,
            deliveriesButton,
            debtButton,
            financeCateg,
            openingChecklistButton,
            closingChecklistButton,
            eodButton
        }

        For Each item As Control In menuControls

            MoveControl(item, _navItemsSurface)

            ' Mouse wheel still works when the pointer is over a menu item.
            AddHandler item.MouseWheel, AddressOf Navigation_MouseWheel

        Next

        ' FOOTER

        MoveControl(
            signOutButton,
            footerPanel
        )

        Me.ResumeLayout(True)

        _layoutReady = True

        AddHandler navigationPanel.Resize,
            AddressOf Sidebar_Resize

        UpdateResponsiveSidebar()

    End Sub

    ' ==========================================
    ' MOVE CONTROLS
    ' ==========================================

    Private Sub MoveControl(
        ctrl As Control,
        destination As Control
    )

        If ctrl Is Nothing OrElse
           destination Is Nothing Then Return

        ctrl.Dock = DockStyle.None

        ' Clear designer sizing constraints so the control can shrink.
        ctrl.AutoSize = False
        ctrl.MinimumSize = Size.Empty
        ctrl.MaximumSize = Size.Empty

        ctrl.Anchor =
            AnchorStyles.Top Or AnchorStyles.Left

        If ctrl.Parent IsNot destination Then

            ctrl.Parent = destination

        End If

    End Sub

    ' ==========================================
    ' FIND EXISTING CONTROLS
    ' ==========================================

    Private Function FindNamedControl(
        root As Control,
        ParamArray names As String()
    ) As Control

        If root Is Nothing Then Return Nothing

        For Each candidateName As String In names

            If String.Equals(
                root.Name,
                candidateName,
                StringComparison.OrdinalIgnoreCase
            ) Then

                Return root

            End If

        Next

        For Each child As Control In root.Controls

            Dim found As Control =
                FindNamedControl(
                    child,
                    names
                )

            If found IsNot Nothing Then
                Return found
            End If

        Next

        Return Nothing

    End Function

    ' ==========================================
    ' FIND INITIALS LABEL
    ' ==========================================

    Private Function FindInitialsText(
        root As Control
    ) As Control

        If root Is Nothing Then Return Nothing

        If TypeOf root Is Label Then

            Dim txt As String =
                If(root.Text, "").
                Trim().
                ToUpperInvariant()

            If txt = "UN" OrElse
               txt = "JE" OrElse
               txt = "US" Then

                Return root

            End If

        End If

        For Each child As Control In root.Controls

            Dim found As Control =
                FindInitialsText(child)

            If found IsNot Nothing Then
                Return found
            End If

        Next

        Return Nothing

    End Function

    ' ==========================================
    ' SETUP USER INITIALS CONTROLS
    ' ==========================================

    Private Sub SetupInitialsControls()

        _initialsText = FindNamedControl(
            Me,
            "userInitalLabel",
            "userInitialLabel",
            "userIntialLabel",
            "userInitialsLabel",
            "userIntialsLabel",
            "initialsLabel",
            "initialLabel"
        )

        _initialsTile = FindNamedControl(
            Me,
            "userInitialsPlaceholder",
            "userIntialsPlaceholder",
            "userInitialPlaceholder",
            "userIntialPlaceholder",
            "userInitialsPanel",
            "userIntialsPanel"
        )

        If _initialsText Is Nothing Then

            _initialsText =
                FindInitialsText(userPlaceholder)

        End If

        If _initialsTile Is Nothing AndAlso
           _initialsText IsNot Nothing Then

            Dim parentControl As Control =
                _initialsText.Parent

            If parentControl IsNot Nothing AndAlso
               parentControl IsNot userPlaceholder AndAlso
               parentControl IsNot Me AndAlso
               parentControl IsNot headerPanel Then

                _initialsTile = parentControl

            End If

        End If

        ' CREATE TILE IF MISSING

        If _initialsTile Is Nothing Then

            _initialsTile = New InitialsTile With {
                .Name = "runtimeInitialsTile",
                .BackColor = HoverOrange
            }

        End If

        ' CREATE LABEL IF MISSING

        If _initialsText Is Nothing Then

            _initialsText = New Label With {
                .Name = "runtimeInitialsLabel",
                .Text = "US",
                .Font = New Font(
                    "Segoe UI",
                    11.0F,
                    FontStyle.Bold
                ),
                .BackColor = Color.Transparent,
                .ForeColor = Color.White,
                .TextAlign = ContentAlignment.MiddleCenter,
                .AutoSize = False
            }

        End If

        _initialsTile.Visible = True
        _initialsText.Visible = True

    End Sub

    ' ==========================================
    ' ROUNDED INITIALS TILE
    ' ==========================================

    Private NotInheritable Class InitialsTile
        Inherits Panel

        Protected Overrides Sub OnResize(
            e As EventArgs
        )

            MyBase.OnResize(e)

            If Me.Width < 2 OrElse
               Me.Height < 2 Then Return

            Dim diameter As Integer =
                Math.Min(
                    14,
                    Math.Min(Me.Width, Me.Height)
                )

            Using shape As New GraphicsPath()

                shape.AddArc(
                    0,
                    0,
                    diameter,
                    diameter,
                    180,
                    90
                )

                shape.AddArc(
                    Me.Width - diameter - 1,
                    0,
                    diameter,
                    diameter,
                    270,
                    90
                )

                shape.AddArc(
                    Me.Width - diameter - 1,
                    Me.Height - diameter - 1,
                    diameter,
                    diameter,
                    0,
                    90
                )

                shape.AddArc(
                    0,
                    Me.Height - diameter - 1,
                    diameter,
                    diameter,
                    90,
                    90
                )

                shape.CloseFigure()

                Dim oldRegion As Region =
                    Me.Region

                Me.Region = New Region(shape)

                If oldRegion IsNot Nothing Then
                    oldRegion.Dispose()
                End If

            End Using

        End Sub

    End Class

    ' ==========================================
    ' SETUP K4J LOGO
    ' ==========================================

    Private Sub SetupLogo()

        If k4jLogoPicture IsNot Nothing Then
            If k4jLogoPicture.Parent IsNot headerPanel AndAlso k4jLogoPicture.Image IsNot Nothing Then
                Using source As New Bitmap(k4jLogoPicture.Image)
                    Dim cropped = source.Clone(GetVisibleBounds(source), PixelFormat.Format32bppArgb)
                    k4jLogoPicture.Image.Dispose()
                    k4jLogoPicture.Image = cropped
                End Using
            End If
            headerPanel.Controls.Add(k4jLogoPicture)
            k4jLogoPicture.Visible = True
            Return
        End If

        k4jLogoPicture = New PictureBox With {
            .Name = "k4jLogoPicture",
            .SizeMode = PictureBoxSizeMode.Zoom,
            .BackColor = Color.Transparent,
            .Visible = False,
            .Margin = New Padding(0),
            .TabStop = False
        }

        headerPanel.Controls.Add(k4jLogoPicture)

        Dim logoPath As String =
            FindAsset("k4jLogo.png")

        If logoPath = String.Empty Then

            Diagnostics.Debug.WriteLine(
                "Missing K4J logo: k4jLogo.png"
            )

            Return

        End If

        Try

            Using source As New Bitmap(logoPath)

                Dim bounds As Rectangle =
                    GetVisibleBounds(source)

                Dim cropped As New Bitmap(
                    bounds.Width,
                    bounds.Height,
                    PixelFormat.Format32bppArgb
                )

                Using g As Graphics =
                    Graphics.FromImage(cropped)

                    g.DrawImage(
                        source,
                        New Rectangle(
                            0,
                            0,
                            cropped.Width,
                            cropped.Height
                        ),
                        bounds,
                        GraphicsUnit.Pixel
                    )

                End Using

                k4jLogoPicture.Image = cropped

            End Using

            k4jLogoPicture.Visible = True
            k4jLogoPicture.BringToFront()

        Catch ex As Exception

            Diagnostics.Debug.WriteLine(
                "Logo loading error: " & ex.Message
            )

        End Try

    End Sub

    ' ==========================================
    ' REMOVE TRANSPARENT IMAGE PADDING
    ' ==========================================

    Private Function GetVisibleBounds(
        img As Bitmap
    ) As Rectangle

        Dim left As Integer = img.Width
        Dim top As Integer = img.Height

        Dim right As Integer = -1
        Dim bottom As Integer = -1

        For y As Integer = 0 To img.Height - 1

            For x As Integer = 0 To img.Width - 1

                If img.GetPixel(x, y).A > 20 Then

                    left = Math.Min(left, x)
                    top = Math.Min(top, y)

                    right = Math.Max(right, x)
                    bottom = Math.Max(bottom, y)

                End If

            Next

        Next

        If right < left OrElse
           bottom < top Then

            Return New Rectangle(
                0,
                0,
                img.Width,
                img.Height
            )

        End If

        Return Rectangle.FromLTRB(
            left,
            top,
            right + 1,
            bottom + 1
        )

    End Function

    ' ==========================================
    ' FIND IMAGE FILE
    ' ==========================================

    Private Function FindAsset(
        fileName As String
    ) As String

        Dim directory As New DirectoryInfo(
            AppDomain.CurrentDomain.BaseDirectory
        )

        For i As Integer = 0 To 6

            If directory Is Nothing Then Exit For

            For Each folder As String In {
                "Icons",
                "Images",
                "Assets",
                "Resources",
                ""
            }

                Dim candidate As String =
                    Path.Combine(
                        directory.FullName,
                        folder,
                        fileName
                    )

                If File.Exists(candidate) Then

                    Return candidate

                End If

            Next

            directory = directory.Parent

        Next

        Return String.Empty

    End Function

    ' ==========================================
    ' RESPONSIVE PROFILE HEIGHT
    ' ==========================================

    Private Function ProfileHeight() As Integer

        Return Math.Max(
            Px(52),
            Math.Min(Px(64), CInt(Me.ClientSize.Width * 0.19))
        )

    End Function

    ' ==========================================
    ' MAIN RESPONSIVE LAYOUT
    ' ==========================================

    Private Sub UpdateResponsiveSidebar()

        If Not _layoutReady OrElse
           _layoutBusy Then Return

        If Me.ClientSize.Width <= 0 OrElse
           Me.ClientSize.Height <= 0 Then Return

        _layoutBusy = True

        Me.SuspendLayout()
        headerPanel.SuspendLayout()
        navigationPanel.SuspendLayout()
        _navItemsSurface.SuspendLayout()
        footerPanel.SuspendLayout()

        Try

            Dim sidebarWidth As Integer =
                Me.ClientSize.Width

            Dim x As Integer =
                LeftEdge()

            ' FONT SCALE

            Dim fontScale As Double =
                Math.Max(
                    0.84,
                    Math.Min(
                        1.12,
                        sidebarWidth / CDbl(Px(275))
                    )
                )

            ' ==================================
            ' LOGO DIMENSIONS
            ' ==================================

            Dim logoSize As Integer =
                Math.Max(
                    Px(30),
                    Math.Min(
                        Px(50),
                        CInt(sidebarWidth * 0.17)
                    )
                )

            Dim logoTop As Integer =
                Px(18)

            Dim profileY As Integer =
                logoTop + logoSize + Px(16)

            ' ==================================
            ' HEADER AND FOOTER HEIGHT
            ' ==================================

            headerPanel.Height =
                profileY + ProfileHeight() + Px(13)

            footerPanel.Height = Px(54)

            ' ==================================
            ' K4J LOGO
            ' ==================================

            If k4jLogoPicture IsNot Nothing Then

                k4jLogoPicture.SetBounds(
                    x,
                    logoTop,
                    logoSize,
                    logoSize
                )

            End If

            ' ==================================
            ' BRAND TEXT
            ' ==================================

            Dim textX As Integer =
                x + logoSize + Px(LogoGap)

            Dim textWidth As Integer =
                Math.Max(
                    1,
                    sidebarWidth - textX - Px(8)
                )

            Dim centerY As Integer =
                logoTop + logoSize \ 2

            ' K4J LPG CENTER

            PositionLabel(
                k4jTitle,
                headerPanel,
                New Rectangle(
                    textX,
                    centerY - Px(23),
                    textWidth,
                    Px(24)
                ),
                ContentAlignment.MiddleLeft,
                11.0F,
                fontScale
            )

            FitLabel(
                k4jTitle,
                11.0F,
                fontScale
            )

            ' ALL BRANCHES

            PositionLabel(
                branchLabel,
                headerPanel,
                New Rectangle(
                    textX,
                    centerY - Px(1),
                    textWidth,
                    Px(34)
                ),
                ContentAlignment.MiddleLeft,
                9.0F,
                fontScale
            )

            branchLabel.AutoEllipsis = True

            ' ==================================
            ' PROFILE
            ' ==================================

            LayoutProfile(
                x,
                profileY,
                fontScale
            )

            ' ==================================
            ' NAVIGATION
            ' ==================================

            LayoutNavigation(
                x,
                fontScale
            )

            ' ==================================
            ' FOOTER
            ' ==================================

            LayoutFooter(
                x,
                fontScale
            )

        Finally

            footerPanel.ResumeLayout(True)
            _navItemsSurface.ResumeLayout(True)
            navigationPanel.ResumeLayout(True)
            headerPanel.ResumeLayout(True)
            Me.ResumeLayout(True)

            _layoutBusy = False

        End Try

    End Sub

    ' ==========================================
    ' RESPONSIVE USER PROFILE
    ' ==========================================

    Private Sub LayoutProfile(
        x As Integer,
        y As Integer,
        fontScale As Double
    )

        Dim cardWidth As Integer =
            Math.Max(
                1,
                headerPanel.ClientSize.Width - 2 * x
            )

        Dim cardHeight As Integer =
            ProfileHeight()

        ' PROFILE CARD

        SetBounds(
            userPlaceholder,
            headerPanel,
            New Rectangle(
                x,
                y,
                cardWidth,
                cardHeight
            )
        )

        ' ==================================
        ' INITIALS TILE
        ' ==================================

        Dim avatarSize As Integer =
            Math.Min(
                Px(44),
                Math.Max(
                    Px(32),
                    CInt(cardHeight * 0.7)
                )
            )

        avatarSize = Math.Min(
            avatarSize,
            Math.Max(1, cardWidth - Px(14))
        )

        Dim avatarX As Integer =
            Px(8)

        Dim avatarY As Integer =
            Math.Max(
                0,
                (cardHeight - avatarSize) \ 2
            )

        SetBounds(
            _initialsTile,
            userPlaceholder,
            New Rectangle(
                avatarX,
                avatarY,
                avatarSize,
                avatarSize
            )
        )

        ' INITIALS LABEL

        PositionLabel(
            _initialsText,
            _initialsTile,
            New Rectangle(
                0,
                0,
                avatarSize,
                avatarSize
            ),
            ContentAlignment.MiddleCenter,
            11.0F,
            fontScale
        )

        _initialsText.BackColor =
            Color.Transparent

        ' ==================================
        ' USERNAME AND ROLE
        ' ==================================

        Dim textX As Integer =
            avatarX + avatarSize + Px(8)

        Dim textWidth As Integer =
            Math.Max(
                1,
                cardWidth - textX - Px(6)
            )

        Dim nameHeight As Integer = Px(19)
        Dim badgeHeight As Integer = Px(18)
        Dim lineGap As Integer = Px(1)

        Dim groupTop As Integer =
            Math.Max(
                0,
                (
                    cardHeight -
                    nameHeight -
                    badgeHeight -
                    lineGap
                ) \ 2
            )

        ' USERNAME

        PositionLabel(
            userNameLabel,
            userPlaceholder,
            New Rectangle(
                textX,
                groupTop,
                textWidth,
                nameHeight
            ),
            ContentAlignment.MiddleLeft,
            9.0F,
            fontScale
        )

        FitLabel(userNameLabel, 9.0F, fontScale)

        userNameLabel.BackColor = Color.Transparent
        userNameLabel.BorderStyle = BorderStyle.None

        ' ROLE FONT

        PositionLabel(
            userRoleLabel,
            userRolePlaceholder,
            New Rectangle(
                0,
                0,
                1,
                badgeHeight
            ),
            ContentAlignment.MiddleCenter,
            8.2F,
            fontScale
        )

        Dim roleTextSize As Size =
            TextRenderer.MeasureText(
                If(userRoleLabel.Text, ""),
                userRoleLabel.Font,
                New Size(100000, 100000),
                TextFormatFlags.NoPrefix Or
                TextFormatFlags.NoPadding Or
                TextFormatFlags.SingleLine
            )

        Dim badgeWidth As Integer =
            Math.Max(
                1,
                Math.Min(
                    textWidth,
                    roleTextSize.Width + Px(14)
                )
            )

        ' ROLE BACKGROUND

        SetBounds(
            userRolePlaceholder,
            userPlaceholder,
            New Rectangle(
                textX,
                groupTop + nameHeight + lineGap,
                badgeWidth,
                badgeHeight
            )
        )

        ' ROLE LABEL

        SetBounds(
            userRoleLabel,
            userRolePlaceholder,
            New Rectangle(
                0,
                0,
                badgeWidth,
                badgeHeight
            )
        )

        userRoleLabel.BackColor =
            Color.Transparent

        ' LAYER ORDER

        userPlaceholder.SendToBack()

        _initialsTile.BringToFront()
        _initialsText.BringToFront()

        userNameLabel.BringToFront()
        userRolePlaceholder.BringToFront()
        userRoleLabel.BringToFront()

    End Sub

    ' ==========================================
    ' RESPONSIVE NAVIGATION
    ' ==========================================

    Private Sub LayoutNavigation(
        x As Integer,
        fontScale As Double
    )

        ' Use only the visible navigation width. Reserve the width
        ' for a vertical bar before positioning the buttons, even
        ' when that bar is hidden, so resizing never causes an
        ' unwanted horizontal scrollbar or layout oscillation.

        Dim viewportWidth As Integer =
            Math.Max(1, navigationPanel.ClientSize.Width)

        ' Header and footer heights can change before dock layout
        ' is completed, so calculate the intended viewport height.
        Dim viewportHeight As Integer = Math.Max(
            1,
            Me.ClientSize.Height - headerPanel.Height - footerPanel.Height
        )

        Dim contentWidth As Integer = Math.Max(
            1,
            viewportWidth - _navVerticalOnly.Width
        )

        Dim categoryX As Integer = Math.Min(
            x,
            Math.Max(0, contentWidth - Px(10))
        )

        Dim buttonX As Integer = Math.Max(0, categoryX - Px(10))
        Dim buttonWidth As Integer = Math.Max(
            1,
            contentWidth - buttonX - Px(10)
        )

        ' This value is already in physical pixels; don't DPI-scale it twice.
        Dim buttonHeight As Integer = Math.Max(
            Px(28),
            Math.Min(Px(38), CInt(contentWidth * 0.13))
        )

        ' Keep inner panel width strictly inside the viewport.
        _navItemsSurface.Width = contentWidth

        Dim currentY As Integer = Px(7)

        LayoutSection(
            SpacedLabel2,
            New Control() {dashboardButton},
            currentY, categoryX, buttonX, buttonWidth,
            buttonHeight, fontScale
        )

        LayoutSection(
            customerCateg,
            New Control() {customersButton},
            currentY, categoryX, buttonX, buttonWidth,
            buttonHeight, fontScale
        )

        LayoutSection(
            catalogCateg,
            New Control() {productsButton, globalInventoryButton},
            currentY, categoryX, buttonX, buttonWidth,
            buttonHeight, fontScale
        )

        LayoutSection(
            ordersCateg,
            New Control() {createOrderButton, assignRiderButton, deliveriesButton, debtButton},
            currentY, categoryX, buttonX, buttonWidth,
            buttonHeight, fontScale
        )

        LayoutSection(
            financeCateg,
            New Control() {openingChecklistButton, closingChecklistButton, eodButton},
            currentY, categoryX, buttonX, buttonWidth,
            buttonHeight, fontScale
        )

        Dim totalHeight As Integer = currentY + Px(8)
        Dim maxOffset As Integer = Math.Max(0, totalHeight - viewportHeight)
        Dim needsScroll As Boolean = maxOffset > 0

        ' With WinForms scrollbar semantics, the reachable maximum
        ' Value is Maximum - LargeChange + 1.
        _navVerticalOnly.SmallChange = Px(35)
        _navVerticalOnly.LargeChange = Math.Max(1, viewportHeight)
        _navVerticalOnly.Maximum = Math.Max(0, totalHeight - 1)

        Dim nextValue As Integer = Math.Min(_navVerticalOnly.Value, maxOffset)
        If Not needsScroll Then nextValue = 0

        If _navVerticalOnly.Value <> nextValue Then
            _navVerticalOnly.Value = nextValue
        End If

        _navVerticalOnly.Visible = needsScroll

        _navItemsSurface.SetBounds(
            0,
            -nextValue,
            contentWidth,
            Math.Max(totalHeight, viewportHeight)
        )

    End Sub

    ' ==========================================
    ' SINGLE VERTICAL SCROLLBAR, NO HORIZONTAL BAR
    ' ==========================================

    Private Sub NavigationVertical_ValueChanged(
        sender As Object,
        e As EventArgs
    )
        If _navItemsSurface Is Nothing Then Return
        If _navVerticalOnly Is Nothing Then Return
        _navItemsSurface.Top = -_navVerticalOnly.Value
    End Sub

    Private Sub Navigation_MouseWheel(
        sender As Object,
        e As MouseEventArgs
    )
        If _navVerticalOnly Is Nothing OrElse
           Not _navVerticalOnly.Visible OrElse
           e.Delta = 0 Then Return

        Dim maxOffset As Integer = Math.Max(
            0,
            _navVerticalOnly.Maximum - _navVerticalOnly.LargeChange + 1
        )

        Dim steps As Integer = Math.Max(1, Math.Abs(e.Delta) \ 120)
        Dim delta As Integer = If(e.Delta > 0, -Px(40), Px(40)) * steps

        Dim nextValue As Integer = Math.Max(
            0,
            Math.Min(maxOffset, _navVerticalOnly.Value + delta)
        )

        If _navVerticalOnly.Value <> nextValue Then
            _navVerticalOnly.Value = nextValue
        End If
    End Sub

    ' ==========================================
    ' CATEGORY LABEL + NAVIGATION BUTTONS
    ' ==========================================

    Private Sub LayoutSection(
        heading As Control,
        buttons As Control(),
        ByRef y As Integer,
        categoryX As Integer,
        buttonX As Integer,
        buttonWidth As Integer,
        buttonHeight As Integer,
        fontScale As Double
    )

        PositionLabel(
            heading,
            _navItemsSurface,
            New Rectangle(
                categoryX,
                y,
                Math.Max(1, _navItemsSurface.ClientSize.Width - categoryX - Px(8)),
                Px(17)
            ),
            ContentAlignment.MiddleLeft,
            8.0F,
            fontScale
        )

        y += Px(21)

        For Each btn As Control In buttons

            SetBounds(
                btn,
                _navItemsSurface,
                New Rectangle(buttonX, y, buttonWidth, buttonHeight)
            )

            SetFont(btn, CSng(Math.Max(8.0, 9.5 * fontScale)))

            Dim rb As RoundedButton = TryCast(btn, RoundedButton)
            If rb IsNot Nothing Then
                Dim iconSize As Integer = Math.Max(
                    Px(16),
                    Math.Min(Px(19), buttonHeight \ 2)
                )

                rb.IconWidth = iconSize
                rb.IconHeight = iconSize
                rb.IconTextSpacing = Px(11)
            End If

            y += buttonHeight + Px(2)
        Next

        y += Px(8)

    End Sub

    ' ==========================================
    ' RESPONSIVE SIGN OUT
    ' ==========================================

    Private Sub LayoutFooter(
        x As Integer,
        fontScale As Double
    )

        Dim buttonX As Integer =
            Math.Max(
                0,
                x - Px(10)
            )

        Dim buttonWidth As Integer =
            Math.Max(
                1,
                footerPanel.ClientSize.Width -
                buttonX - Px(10)
            )

        Dim buttonHeight As Integer =
            Px(36)

        Dim bottomY As Integer =
            Math.Max(
                0,
                footerPanel.ClientSize.Height -
                buttonHeight - Px(8)
            )

        SetBounds(
            signOutButton,
            footerPanel,
            New Rectangle(
                buttonX,
                bottomY,
                buttonWidth,
                buttonHeight
            )
        )

        SetFont(
            signOutButton,
            CSng(
                Math.Max(
                    8.0,
                    9.5 * fontScale
                )
            )
        )

    End Sub

    ' ==========================================
    ' SET CONTROL BOUNDS
    ' ==========================================

    Private Overloads Sub SetBounds(
        ctrl As Control,
        requiredParent As Control,
        bounds As Rectangle
    )

        If ctrl Is Nothing OrElse
           ctrl.IsDisposed Then Return

        If ctrl.Parent IsNot requiredParent Then
            Return
        End If

        If ctrl.Bounds <> bounds Then
            ctrl.Bounds = bounds
        End If

    End Sub

    ' ==========================================
    ' LABEL POSITION AND FONT
    ' ==========================================

    Private Sub PositionLabel(
        ctrl As Control,
        parent As Control,
        bounds As Rectangle,
        alignment As ContentAlignment,
        basePt As Single,
        scale As Double
    )

        If ctrl Is Nothing Then Return

        SetBounds(
            ctrl,
            parent,
            bounds
        )

        Dim lbl As Label =
            TryCast(ctrl, Label)

        If lbl IsNot Nothing Then

            lbl.AutoSize = False
            lbl.AutoEllipsis = True
            lbl.TextAlign = alignment

        End If

        ctrl.Padding = New Padding(0)
        ctrl.Margin = New Padding(0)

        SetFont(
            ctrl,
            CSng(
                Math.Max(
                    7.0,
                    basePt * scale
                )
            )
        )

    End Sub

    ' ==========================================
    ' FIT LABEL FONT TO AVAILABLE SPACE
    ' ==========================================

    Private Sub FitLabel(
        ctrl As Control,
        basePt As Single,
        scale As Double
    )

        If ctrl Is Nothing OrElse
           ctrl.Width < 2 Then Return

        Dim fontSize As Single =
            CSng(
                Math.Max(
                    7.0,
                    basePt * scale
                )
            )

        While fontSize > 7.0F

            Using testFont As New Font(
                ctrl.Font.FontFamily,
                fontSize,
                ctrl.Font.Style,
                GraphicsUnit.Point
            )

                Dim measured As Size =
                    TextRenderer.MeasureText(
                        If(ctrl.Text, ""),
                        testFont,
                        New Size(100000, 100000),
                        TextFormatFlags.NoPadding Or
                        TextFormatFlags.NoPrefix Or
                        TextFormatFlags.SingleLine
                    )

                If measured.Width <=
                   ctrl.ClientSize.Width - Px(2) AndAlso
                   measured.Height <=
                   ctrl.ClientSize.Height Then

                    Exit While

                End If

            End Using

            fontSize -= 0.5F

        End While

        SetFont(
            ctrl,
            Math.Max(7.0F, fontSize)
        )

    End Sub

    ' ==========================================
    ' SET RESPONSIVE FONT
    ' ==========================================

    Private Sub SetFont(
        ctrl As Control,
        fontSize As Single
    )

        If ctrl Is Nothing OrElse
           ctrl.IsDisposed Then Return

        If Math.Abs(
            ctrl.Font.SizeInPoints - fontSize
        ) < 0.1F Then Return

        Dim newFont As New Font(
            ctrl.Font.FontFamily,
            fontSize,
            ctrl.Font.Style,
            GraphicsUnit.Point
        )

        Dim oldFont As Font = Nothing

        _fonts.TryGetValue(
            ctrl,
            oldFont
        )

        ctrl.Font = newFont
        _fonts(ctrl) = newFont

        If oldFont IsNot Nothing Then

            oldFont.Dispose()

        End If

    End Sub

    ' ==========================================
    ' CLEANUP
    ' ==========================================

    Private Sub Sidebar_Disposed(
        sender As Object,
        e As EventArgs
    )

        For Each fontItem As Font In _fonts.Values

            fontItem.Dispose()

        Next

        _fonts.Clear()
        _radii.Clear()

        _normalIcons.Clear()
        _whiteIcons.Clear()
        _originalIconColors.Clear()

        If k4jLogoPicture IsNot Nothing AndAlso
           k4jLogoPicture.Image IsNot Nothing Then

            k4jLogoPicture.Image.Dispose()
            k4jLogoPicture.Image = Nothing

        End If

    End Sub

End Class




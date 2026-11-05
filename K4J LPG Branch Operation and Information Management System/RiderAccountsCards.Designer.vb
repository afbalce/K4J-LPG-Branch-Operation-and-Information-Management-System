<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RiderAccountsCards
    Inherits UserControl

    Private Sub InitializeComponent()
        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(RiderAccountsCards))
        cardsLayout = New TableLayoutPanel()
        totalRidersCard = New RoundedTitlePanel()
        totalRidersTitle = New Label()
        totalRidersValue = New Label()
        totalRidersBadge = New RoundedTitlePanel()
        totalRidersImage = New PictureBox()
        activeCard = New RoundedTitlePanel()
        activeTitle = New Label()
        activeValue = New Label()
        activeBadge = New RoundedTitlePanel()
        activeImage = New PictureBox()
        deliveriesTodayCard = New RoundedTitlePanel()
        deliveriesTodayTitle = New Label()
        deliveriesTodayValue = New Label()
        deliveriesTodayBadge = New RoundedTitlePanel()
        deliveriesTodayImage = New PictureBox()
        allTimeCard = New RoundedTitlePanel()
        allTimeTitle = New Label()
        allTimeValue = New Label()
        allTimeBadge = New RoundedTitlePanel()
        allTimeImage = New PictureBox()
        SuspendLayout()
        cardsLayout.SuspendLayout()
        cardsLayout.Dock = DockStyle.Fill
        cardsLayout.Name = "cardsLayout"
        cardsLayout.Size = New Size(912, 85)
        cardsLayout.ColumnCount = 4
        cardsLayout.RowCount = 1
        cardsLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        cardsLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        cardsLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        cardsLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        cardsLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))

        totalRidersCard.SuspendLayout()
        totalRidersCard.Name = "totalRidersCard"
        totalRidersCard.Size = New Size(235, 80)
        totalRidersCard.Dock = DockStyle.Fill
        totalRidersCard.Margin = New Padding(6, 0, 6, 0)
        totalRidersCard.FillColor = Color.White
        totalRidersCard.BorderColor = Color.FromArgb(225, 229, 234)
        totalRidersCard.CornerRadius = 11
        totalRidersCard.BorderThickness = 1
        totalRidersTitle.Name = "totalRidersTitle"
        totalRidersTitle.Text = "TOTAL RIDERS"
        totalRidersTitle.ForeColor = Color.FromArgb(124, 138, 160)
        totalRidersTitle.Font = New Font("Segoe UI Semibold", 8.0F)
        totalRidersTitle.Location = New Point(17, 16)
        totalRidersTitle.Size = New Size(156, 19)
        totalRidersTitle.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        totalRidersTitle.TextAlign = ContentAlignment.MiddleLeft
        totalRidersValue.Name = "totalRidersValue"
        totalRidersValue.Text = "2"
        totalRidersValue.ForeColor = Color.FromArgb(15, 23, 42)
        totalRidersValue.Font = New Font("Segoe UI Semibold", 15.0F)
        totalRidersValue.Location = New Point(15, 33)
        totalRidersValue.Size = New Size(158, 33)
        totalRidersValue.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        totalRidersValue.TextAlign = ContentAlignment.MiddleLeft
        totalRidersBadge.Name = "totalRidersBadge"
        totalRidersBadge.FillColor = Color.FromArgb(235, 103, 8)
        totalRidersBadge.BorderThickness = 0
        totalRidersBadge.CornerRadius = 10
        totalRidersBadge.Size = New Size(34, 34)
        totalRidersBadge.Location = New Point(183, 18)
        totalRidersBadge.Anchor = AnchorStyles.Right
        totalRidersImage.Name = "totalRidersImage"
        totalRidersImage.Image = CType(resources.GetObject("RiderImage"), Image)
        totalRidersImage.SizeMode = PictureBoxSizeMode.Zoom
        totalRidersImage.BackColor = Color.Transparent
        totalRidersImage.SetBounds(8, 8, 18, 18)
        totalRidersBadge.Controls.Add(totalRidersImage)
        totalRidersCard.Controls.Add(totalRidersTitle)
        totalRidersCard.Controls.Add(totalRidersValue)
        totalRidersCard.Controls.Add(totalRidersBadge)
        cardsLayout.Controls.Add(totalRidersCard, 0, 0)
        totalRidersCard.ResumeLayout(False)

        activeCard.SuspendLayout()
        activeCard.Name = "activeCard"
        activeCard.Size = New Size(235, 80)
        activeCard.Dock = DockStyle.Fill
        activeCard.Margin = New Padding(6, 0, 6, 0)
        activeCard.FillColor = Color.White
        activeCard.BorderColor = Color.FromArgb(225, 229, 234)
        activeCard.CornerRadius = 11
        activeCard.BorderThickness = 1
        activeTitle.Name = "activeTitle"
        activeTitle.Text = "ACTIVE"
        activeTitle.ForeColor = Color.FromArgb(124, 138, 160)
        activeTitle.Font = New Font("Segoe UI Semibold", 8.0F)
        activeTitle.Location = New Point(17, 16)
        activeTitle.Size = New Size(156, 19)
        activeTitle.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        activeTitle.TextAlign = ContentAlignment.MiddleLeft
        activeValue.Name = "activeValue"
        activeValue.Text = "2"
        activeValue.ForeColor = Color.FromArgb(15, 23, 42)
        activeValue.Font = New Font("Segoe UI Semibold", 15.0F)
        activeValue.Location = New Point(15, 33)
        activeValue.Size = New Size(158, 33)
        activeValue.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        activeValue.TextAlign = ContentAlignment.MiddleLeft
        activeBadge.Name = "activeBadge"
        activeBadge.FillColor = Color.FromArgb(0, 153, 103)
        activeBadge.BorderThickness = 0
        activeBadge.CornerRadius = 10
        activeBadge.Size = New Size(34, 34)
        activeBadge.Location = New Point(183, 18)
        activeBadge.Anchor = AnchorStyles.Right
        activeImage.Name = "activeImage"
        activeImage.Image = CType(resources.GetObject("CheckImage"), Image)
        activeImage.SizeMode = PictureBoxSizeMode.Zoom
        activeImage.BackColor = Color.Transparent
        activeImage.SetBounds(8, 8, 18, 18)
        activeBadge.Controls.Add(activeImage)
        activeCard.Controls.Add(activeTitle)
        activeCard.Controls.Add(activeValue)
        activeCard.Controls.Add(activeBadge)
        cardsLayout.Controls.Add(activeCard, 1, 0)
        activeCard.ResumeLayout(False)

        deliveriesTodayCard.SuspendLayout()
        deliveriesTodayCard.Name = "deliveriesTodayCard"
        deliveriesTodayCard.Size = New Size(235, 80)
        deliveriesTodayCard.Dock = DockStyle.Fill
        deliveriesTodayCard.Margin = New Padding(6, 0, 6, 0)
        deliveriesTodayCard.FillColor = Color.White
        deliveriesTodayCard.BorderColor = Color.FromArgb(225, 229, 234)
        deliveriesTodayCard.CornerRadius = 11
        deliveriesTodayCard.BorderThickness = 1
        deliveriesTodayTitle.Name = "deliveriesTodayTitle"
        deliveriesTodayTitle.Text = "DELIVERIES TODAY"
        deliveriesTodayTitle.ForeColor = Color.FromArgb(124, 138, 160)
        deliveriesTodayTitle.Font = New Font("Segoe UI Semibold", 8.0F)
        deliveriesTodayTitle.Location = New Point(17, 16)
        deliveriesTodayTitle.Size = New Size(156, 19)
        deliveriesTodayTitle.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        deliveriesTodayTitle.TextAlign = ContentAlignment.MiddleLeft
        deliveriesTodayValue.Name = "deliveriesTodayValue"
        deliveriesTodayValue.Text = "7"
        deliveriesTodayValue.ForeColor = Color.FromArgb(15, 23, 42)
        deliveriesTodayValue.Font = New Font("Segoe UI Semibold", 15.0F)
        deliveriesTodayValue.Location = New Point(15, 33)
        deliveriesTodayValue.Size = New Size(158, 33)
        deliveriesTodayValue.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        deliveriesTodayValue.TextAlign = ContentAlignment.MiddleLeft
        deliveriesTodayBadge.Name = "deliveriesTodayBadge"
        deliveriesTodayBadge.FillColor = Color.FromArgb(23, 94, 255)
        deliveriesTodayBadge.BorderThickness = 0
        deliveriesTodayBadge.CornerRadius = 10
        deliveriesTodayBadge.Size = New Size(34, 34)
        deliveriesTodayBadge.Location = New Point(183, 18)
        deliveriesTodayBadge.Anchor = AnchorStyles.Right
        deliveriesTodayImage.Name = "deliveriesTodayImage"
        deliveriesTodayImage.Image = CType(resources.GetObject("BoxImage"), Image)
        deliveriesTodayImage.SizeMode = PictureBoxSizeMode.Zoom
        deliveriesTodayImage.BackColor = Color.Transparent
        deliveriesTodayImage.Location = New Point(8, 8)
        deliveriesTodayImage.Size = New Size(18, 18)
        deliveriesTodayBadge.Controls.Add(deliveriesTodayImage)
        deliveriesTodayCard.Controls.Add(deliveriesTodayTitle)
        deliveriesTodayCard.Controls.Add(deliveriesTodayValue)
        deliveriesTodayCard.Controls.Add(deliveriesTodayBadge)
        cardsLayout.Controls.Add(deliveriesTodayCard, 2, 0)
        deliveriesTodayCard.ResumeLayout(False)

        allTimeCard.SuspendLayout()
        allTimeCard.Name = "allTimeCard"
        allTimeCard.Size = New Size(235, 80)
        allTimeCard.Dock = DockStyle.Fill
        allTimeCard.Margin = New Padding(6, 0, 6, 0)
        allTimeCard.FillColor = Color.White
        allTimeCard.BorderColor = Color.FromArgb(225, 229, 234)
        allTimeCard.CornerRadius = 11
        allTimeCard.BorderThickness = 1
        allTimeTitle.Name = "allTimeTitle"
        allTimeTitle.Text = "ALL-TIME TOTAL"
        allTimeTitle.ForeColor = Color.FromArgb(124, 138, 160)
        allTimeTitle.Font = New Font("Segoe UI Semibold", 8.0F)
        allTimeTitle.Location = New Point(17, 16)
        allTimeTitle.Size = New Size(156, 19)
        allTimeTitle.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        allTimeTitle.TextAlign = ContentAlignment.MiddleLeft
        allTimeValue.Name = "allTimeValue"
        allTimeValue.Text = "873"
        allTimeValue.ForeColor = Color.FromArgb(15, 23, 42)
        allTimeValue.Font = New Font("Segoe UI Semibold", 15.0F)
        allTimeValue.Location = New Point(15, 33)
        allTimeValue.Size = New Size(158, 33)
        allTimeValue.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        allTimeValue.TextAlign = ContentAlignment.MiddleLeft
        allTimeBadge.Name = "allTimeBadge"
        allTimeBadge.FillColor = Color.FromArgb(126, 46, 246)
        allTimeBadge.BorderThickness = 0
        allTimeBadge.CornerRadius = 10
        allTimeBadge.Size = New Size(34, 34)
        allTimeBadge.Location = New Point(183, 18)
        allTimeBadge.Anchor = AnchorStyles.Right
        allTimeImage.Name = "allTimeImage"
        allTimeImage.Image = CType(resources.GetObject("AnalyticsImage"), Image)
        allTimeImage.SizeMode = PictureBoxSizeMode.Zoom
        allTimeImage.BackColor = Color.Transparent
        allTimeImage.Location = New Point(8, 8)
        allTimeImage.Size = New Size(18, 18)
        allTimeBadge.Controls.Add(allTimeImage)
        allTimeCard.Controls.Add(allTimeTitle)
        allTimeCard.Controls.Add(allTimeValue)
        allTimeCard.Controls.Add(allTimeBadge)
        cardsLayout.Controls.Add(allTimeCard, 3, 0)
        allTimeCard.ResumeLayout(False)
        Name = "RiderAccountsCards"
        BackColor = Color.FromArgb(241, 245, 249)

        AutoScaleMode = AutoScaleMode.None
        Size = New Size(924, 105)
        Padding = New Padding(6, 10, 6, 10)
        Controls.Add(cardsLayout)
        cardsLayout.ResumeLayout(True)
        ResumeLayout(True)
    End Sub

    Friend WithEvents cardsLayout As TableLayoutPanel
    Friend WithEvents totalRidersCard As RoundedTitlePanel
    Friend WithEvents totalRidersTitle As Label
    Friend WithEvents totalRidersValue As Label
    Friend WithEvents totalRidersBadge As RoundedTitlePanel
    Friend WithEvents totalRidersImage As PictureBox
    Friend WithEvents activeCard As RoundedTitlePanel
    Friend WithEvents activeTitle As Label
    Friend WithEvents activeValue As Label
    Friend WithEvents activeBadge As RoundedTitlePanel
    Friend WithEvents activeImage As PictureBox
    Friend WithEvents deliveriesTodayCard As RoundedTitlePanel
    Friend WithEvents deliveriesTodayTitle As Label
    Friend WithEvents deliveriesTodayValue As Label
    Friend WithEvents deliveriesTodayBadge As RoundedTitlePanel
    Friend WithEvents deliveriesTodayImage As PictureBox
    Friend WithEvents allTimeCard As RoundedTitlePanel
    Friend WithEvents allTimeTitle As Label
    Friend WithEvents allTimeValue As Label
    Friend WithEvents allTimeBadge As RoundedTitlePanel
    Friend WithEvents allTimeImage As PictureBox
End Class

Public Class RiderAccountsCards
    Friend Const StripWidth As Integer = 924
    Friend Const StripHeight As Integer = 105
    Private totalsLoaded As Boolean

    Protected Overrides Sub SetBoundsCore(x As Integer, y As Integer, width As Integer, height As Integer, specified As BoundsSpecified)
        MyBase.SetBoundsCore(x, y, StripWidth, StripHeight, specified)
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If totalsLoaded OrElse DesignMode OrElse ComponentModel.LicenseManager.UsageMode = ComponentModel.LicenseUsageMode.Designtime Then Return
        For Each value In {totalRidersValue, activeValue, deliveriesTodayValue, allTimeValue}
            value.Text = "—"
        Next
    End Sub

    Friend Sub ShowTotals(rows As DataTable)
        Dim active = rows.Select("Status = 'Active'").Length
        Dim completed As Long
        For Each row As DataRow In rows.Rows
            If Not row.IsNull("CompletedDeliveries") Then completed += CLng(row("CompletedDeliveries"))
        Next
        totalRidersValue.Text = rows.Rows.Count.ToString("N0")
        activeValue.Text = active.ToString("N0")
        deliveriesTodayValue.Text = "—"
        allTimeValue.Text = completed.ToString("N0")
        totalsLoaded = True
    End Sub
End Class

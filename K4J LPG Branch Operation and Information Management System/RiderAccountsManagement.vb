Public Class RiderAccountsManagement
    Inherits AdminAccountsManagement

    Public Sub New()
        MyBase.New()
        Dim columns = StyledDataGridView1.Columns
        columns.Remove(colEmail)
        columns.Remove(colRole)
        columns.Remove(colLastLogin)
        Dim today As New DataGridViewTextBoxColumn With {
            .Name = "colToday", .HeaderText = "TODAY", .FillWeight = 92, .MinimumWidth = 60,
            .SortMode = DataGridViewColumnSortMode.NotSortable
        }
        today.DefaultCellStyle.NullValue = "—"
        today.DefaultCellStyle.ForeColor = Color.FromArgb(232, 101, 10)
        columns.Insert(colStatus.Index, today)
        columns.Insert(colStatus.Index, New DataGridViewTextBoxColumn With {
            .Name = "colCompletedDeliveries", .HeaderText = "TOTAL", .FillWeight = 92, .MinimumWidth = 60
        })
        colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        colStatus.FillWeight = 124
        colStatus.MinimumWidth = 90
        colActions.FillWeight = 104
        colActions.MinimumWidth = 80
        AddHandler StyledDataGridView1.ClientSizeChanged, AddressOf SizeRiderNameColumn
        SizeRiderNameColumn(Me, EventArgs.Empty)
    End Sub

    Private Sub SizeRiderNameColumn(sender As Object, e As EventArgs)
        Dim scale = StyledDataGridView1.DeviceDpi / 96.0R
        colName.Width = CInt(Math.Clamp(StyledDataGridView1.ClientSize.Width * 0.22, 150 * scale, 220 * scale))
    End Sub

    Protected Overrides ReadOnly Property AccountRole As String
        Get
            Return "Rider"
        End Get
    End Property
End Class

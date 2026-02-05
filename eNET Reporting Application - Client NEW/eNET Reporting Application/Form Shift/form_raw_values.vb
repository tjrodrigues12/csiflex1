Public Class form_raw_values
    Private Sub form_raw_values_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DGV_VALUES.Columns.Item("day_").Visible = False
            DGV_VALUES.Columns.Item("month_").Visible = False
            DGV_VALUES.Columns.Item("year_").Visible = False
            DGV_VALUES.Columns.Item("date_").Visible = False
            If DGV_VALUES.Columns.Item("Partnumber") IsNot Nothing Then DGV_VALUES.Columns.Item("Partnumber").Visible = False
            DGV_VALUES.Columns.Item("time_").HeaderText = "Date"
            DGV_VALUES.Columns.Item("status").HeaderText = "Status"
            DGV_VALUES.Columns.Item("shift").HeaderText = "Shift"
            DGV_VALUES.Columns.Item("cycletime").HeaderText = "Cycle time (seconds)"
            ' Me.Icon = My.Resources.CSIFLEX_logo
            DGV_VALUES.EnableHeadersVisualStyles = False
            DGV_VALUES.BackgroundColor = Color.Gray
            DGV_VALUES.BackgroundColor = Color.White

            DGV_VALUES.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Catch ex As Exception

        End Try

    End Sub
End Class
Public Class ActivatedMaxTime


    Private Sub ActivatedMaxTime_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BT_activate_Click(sender As Object, e As EventArgs) Handles BT_activate.Click
        Report_parts.max_time_activated = Not Report_parts.max_time_activated
        If Report_parts.max_time_activated = False Then
            BT_activate.Text = "Activate the max cycle time"
            Report_parts.min_max_type = "_MIN_ONLY"
            Report_parts.BW_process_shift1.RunWorkerAsync()
            Report_parts.BW_Process_shift2.RunWorkerAsync()
            Report_parts.BW_Process_shift3.RunWorkerAsync()
        Else
            BT_activate.Text = "Disactivate the max cycle time"
            Report_parts.min_max_type = "_MIN_MAX"
            Report_parts.BW_process_shift1.RunWorkerAsync()
            Report_parts.BW_Process_shift2.RunWorkerAsync()
            Report_parts.BW_Process_shift3.RunWorkerAsync()
        End If
    End Sub

End Class
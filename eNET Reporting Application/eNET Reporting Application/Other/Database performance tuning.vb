Public Class Database_performance_tuning


    Private Sub Button1_Click(sender As Object, e As EventArgs)
        help_tuning.helptext.Text = "This is the size of the redo logs. ....?"
        help_tuning.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        help_tuning.helptext.Text = " If you Then are often facing the 'Too many connections’ error, max_connections is too low. You need much more than the default 151 connections. The main drawback of high values for max_connections (like 1000 or more) is that the server will become unresponsive if for any reason it has to run 1000 or more active transactions.
  "
        help_tuning.ShowDialog()


    End Sub

    Private Sub TP_help_Popup(sender As Object, e As PopupEventArgs) Handles TP_help.Popup

    End Sub

    Private Sub poolbuffer_Click(sender As Object, e As EventArgs) Handles poolbuffer.MouseHover
        TP_help.SetToolTip(poolbuffer, "The buffer pool is where data and indexes are cached: " + vbCrLf + "having it as large as possible will ensure you use memory and not disks for most read operations. " + vbCrLf + " Typical values are 50 - 60 % of RAM but beware of setting memory usage too high")
    End Sub
    Private Sub max_conn_Click(sender As Object, e As EventArgs) Handles max_conn.MouseHover
        TP_help.SetToolTip(poolbuffer, " If you Then are often facing the 'Too many connections’ error, max_connections is too low. " + vbCrLf + " You need much more than the default 151 connections. " + vbCrLf + " The main drawback of high values for max_connections (like 1000 or more) is that the server will become unresponsive if for any reason it has to run 500 or more active transactions.
  ")
    End Sub


    Dim ram As String
    Private Sub Database_performance_tuning_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim CMP_inf As New Devices.ComputerInfo
        ram = CMP_inf.TotalPhysicalMemory
        Dim avail_ram As String = CMP_inf.AvailablePhysicalMemory
        LBL_ram.Text = "System memory : " & (((ram / 1024) / 1024) / 1024).ToString()
        LBL_ram_avail.Text = "Available memory : " & (((avail_ram / 1024) / 1024) / 1024).ToString()


    End Sub



    Private Sub TB_pool_buffer_TextChanged(sender As Object, e As EventArgs) Handles TB_pool_buffer.TextChanged
        pool_buffer.Value = Convert.ToInt16(TB_pool_buffer.Text / 10)
    End Sub

    Private Sub pool_buffer_Scroll(sender As Object, e As EventArgs) Handles pool_buffer.Scroll
        TB_pool_buffer.Text = pool_buffer.Value * 10
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        TP_help.SetToolTip(poolbuffer, " The default setting of 1 means that InnoDB is fully ACID compliant. " + vbCrLf + " It is the best value when your primary concern is data safety. " + vbCrLf + " However it can have a significant overhead on systems with slow disks because of the extra fsyncs that are needed to flush each change to the redo logs. Setting it to 2 is a bit less reliable because committed transactions will be flushed to the redo logs only once a second, but that can be acceptable on some situations for a master And that Is definitely a good value for a replica." + vbCrLf + " 0 Is even faster but you are more likely to lose some data in case of a crash.")

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.MouseHover
        TP_help.SetToolTip(poolbuffer, "This is the size of the buffer for transactions that have not been committed yet. The default value (1MB) is usually fine but as soon as you have transactions with large blob/text fields, the buffer can fill up very quickly and trigger extra I/O load. Look at the Innodb_log_waits status variable and if it is not 0, increase innodb_log_buffer_size"
   )

    End Sub
End Class
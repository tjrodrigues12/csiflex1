Public Class DB_init

    Private Sub DB_init_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BW_check_mysql.RunWorkerAsync()
    End Sub

    ''' <summary>
    ''' Function to check if MySql Server is Running or not 
    ''' If it's not running then it will start the Mysql Service
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BW_check_mysql_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW_check_mysql.DoWork
        If CSI_Lib.start_mysqld() Then
            CSI_Lib.Log_server_event("mysqld ok while loading userlogin, continue loading CSIFlex")
            UserLogin.MysQl_ok__ = True
        Else
            ' MessageBox.Show("Could not start Mysql")
            CSI_Lib.Log_server_event("me.close while loading userlogin. mysqld fail")
            UserLogin.MysQl_ok__ = False
            'Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' When Background worker finished it's work it will close the form 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BW_check_mysql_ficish(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW_check_mysql.RunWorkerCompleted
        BW_check_mysql.Dispose()
        Me.Close()
    End Sub

End Class
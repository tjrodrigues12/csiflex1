Imports MySql.Data.MySqlClient

Public Class ConfigureService
    Dim mysqlcon As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
    Private Sub CHKB_LoadingAsCON_CheckedChanged(sender As Object, e As EventArgs) Handles CHKB_LoadingAsCON.CheckedChanged
        UpdateServiceConfiguration(CHKB_LoadingAsCON.Checked)
    End Sub

    Private Sub UpdateServiceConfiguration(loadingAsCon As Boolean)
        Try
            mysqlcon.Open()
            Using mysqlcmd As New MySqlCommand("delete from csi_auth.tbl_serviceconfig;insert into csi_auth.tbl_serviceconfig (LoadingAsCON) values(" + loadingAsCon.ToString() + ");", mysqlcon)
                mysqlcmd.ExecuteNonQuery()
            End Using
            CSI_Library.CSI_Library.loadingasCON = loadingAsCon
            'End Using
        Catch ex As Exception
            CSI_Lib.LogServerError("unable to update service config:" + ex.Message, 1)
            MessageBox.Show("Unable to update service configuration")
        Finally
            mysqlcon.Close()
        End Try
    End Sub

    Private Sub ConfigureService_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            RemoveHandler CHKB_LoadingAsCON.CheckedChanged, AddressOf CHKB_LoadingAsCON_CheckedChanged
            mysqlcon.Open()
            Using mysqlcmd As New MySqlDataAdapter("select LoadingAsCON from csi_auth.tbl_serviceconfig;", mysqlcon)
                    Dim dt As New DataTable
                    mysqlcmd.Fill(dt)
                    CHKB_LoadingAsCON.Checked = dt.Rows(0)("LoadingAsCON")

            End Using
        Catch ex As Exception
            CSI_Lib.LogServerError("unable to load service config:" + ex.Message, 1)
            MessageBox.Show("unable to load service config")
        Finally
            AddHandler CHKB_LoadingAsCON.CheckedChanged, AddressOf CHKB_LoadingAsCON_CheckedChanged
            mysqlcon.Close()
        End Try
    End Sub

    Private Sub BTN_Accept_Click(sender As Object, e As EventArgs) Handles BTN_Accept.Click
        Dispose()
    End Sub
End Class
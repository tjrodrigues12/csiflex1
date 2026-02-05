Imports MySql.Data.MySqlClient

Public Class AddExternalSource

    Public dashboardip As String
    Public dashboardname As String

    Private Sub BTN_Save_Click(sender As Object, e As EventArgs) Handles BTN_Save.Click
        If (TB_ExtName.Text.Length > 0 And TB_ExtIP.Text.Length > 0) Then
            'Try to add device in remote source
            If (AddRemoteDevice()) Then
                Using mysqlcon As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                    mysqlcon.Open()
                    Using mysqlcmd As New MySqlCommand("delete from CSI_database.tbl_externalsource where dashboardip_ = '" + dashboardip + "' and externalname_='" + TB_ExtName.Text + "'", mysqlcon)
                        mysqlcmd.ExecuteNonQuery()
                    End Using
                    Using mysqlcmd As New MySqlCommand("insert into CSI_database.tbl_externalsource set dashboardip_ = '" + dashboardip + "', externalname_='" + TB_ExtName.Text + "', externalip_='" + TB_ExtIP.Text + "'", mysqlcon)
                        mysqlcmd.ExecuteNonQuery()
                    End Using
                    mysqlcon.Close()
                End Using
                Me.Dispose()
            Else
                MessageBox.Show("Unable to add device to remote server")
            End If
        Else
            MessageBox.Show("Please input name and IP")
        End If
    End Sub

    Public Function AddRemoteDevice() As Boolean
        Dim res As Boolean = True
        Dim name As String = dashboardname
        'SetupForm.Devices_TV.Nodes(0).Nodes.Add(name)
        'Dim connectionString As String
        'connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

        Dim cntsql As New MySqlConnection("server=" + TB_ExtIP.Text + ";DATABASE=csi_database;" + CSI_Library.CSI_Library.MySqlServerBaseString)

        Try
            cntsql.Open()

            Dim cmdsql As New MySqlCommand("INSERT INTO CSI_Database.tbl_devices (ip_adress, devicename) VALUES('" + dashboardip + "', '" + dashboardname + "') " +
                                                  "ON DUPLICATE KEY UPDATE ip_adress=('" + dashboardip + "'), devicename=('" + dashboardname + "');", cntsql)
            cmdsql.ExecuteNonQuery()
            Dim MySql As String = "commit"
            cmdsql = New MySqlCommand(MySql, cntsql)
            cmdsql.ExecuteNonQuery()

            Dim MySql2 As String = "insert ignore into CSI_Database.tbl_deviceConfig (name, IP,refreshbrowser,livestatusdelay,datetime,customlogo,temperature,lastcycle,currentcycle,elapsedtime,fullscreen, livestatusposition, IFrame) VALUES('" + dashboardname + "','" + dashboardip + "','done','off','on','off','off','on','on','on','off','right','off');"

            cmdsql = New MySqlCommand(MySql2, cntsql)
            cmdsql.ExecuteNonQuery()

            cntsql.Close()
        Catch ex As Exception
            res = False
        End Try

        Return res
    End Function
End Class
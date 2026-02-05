Imports Renci.SshNet
Imports System.Threading
Imports MySql.Data.MySqlClient

Public Class targetServer

    Private Sub OK_BTN_Click(sender As Object, e As EventArgs) Handles OK_BTN.Click

        Try

            Dim portT As New DataTable
            Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select port From csi_database.tbl_rm_port;", CSI_Library.CSI_Library.MySqlConnectionString)
            dadapter_name.Fill(portT)


            Dim port As String = ""
            If portT.Rows.Count <> 0 Then
                If IsDBNull(portT.Rows(0)("port")) Then
                    port = "8008"
                Else
                    port = portT.Rows(0)("port")
                End If
            End If



            Using client = New SshClient(SetupForm2.IP_TB.Text, "pi", "raspberry")
                client.Connect()

                Using cmd = client.CreateCommand("sudo python editIp.py " + IP_TB.Text + "," + port)
                    cmd.Execute()
                    Console.WriteLine("Command>" + cmd.CommandText)
                    Console.WriteLine("Return Value = {0}", cmd.ExitStatus)
                End Using

            End Using
        Catch ex As Exception
            MessageBox.Show("Device can't be found")
        End Try

        Me.Close()
    End Sub

    Public Sub reboot()
        Try

            Using client = New SshClient(SetupForm2.IP_TB.Text, "pi", "raspberry")
                client.Connect()
                Using cmd2 = client.CreateCommand("sudo reboot")
                    cmd2.Execute()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Device can't be found")
        End Try
    End Sub

    Public command As String = ""
    Public Sub updateNetwork()
        Try

            Using client = New SshClient(SetupForm2.IP_TB.Text, "pi", "raspberry")
                client.Connect()
                NetworkSetting.ShowDialog()
                'Dim command As String = "sudo echo ""allow hotplug wlan0\niface wlan0 inet static\naddress " & NetworkSetting.TB_IP.Text & " \nnetmask " & NetworkSetting.TB_mask.Text & "\ngateway " & NetworkSetting.TB_gatway.Text & "\nwpa-ssid \""" & NetworkSetting.TB_SSID.Text & "\""\" & NetworkSetting.CB_sec.SelectedItem & "\""" & NetworkSetting.TB_PWD.Text & "\"" > /etc/network/interfaces.d/usbdongle"
                If command <> "" Then
                    Dim cmd2 = client.CreateCommand(command)

                    Dim asynch = cmd2.BeginExecute()
                    cmd2.EndExecute(asynch)
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Device can't be found")
        End Try
    End Sub

    Public Sub updateSoft()
        Try

            Using client = New SshClient(SetupForm2.IP_TB.Text, "pi", "raspberry")
                client.Connect()
                Dim cmd2 = client.CreateCommand("sudo apt-get update && sudo apt-get -y upgrade && sudo reboot")
                Dim asynch = cmd2.BeginExecute()
                cmd2.EndExecute(asynch)
            End Using

        Catch ex As Exception
            MessageBox.Show("Device can't be found")
        End Try
    End Sub


    Private Sub CANCEL_BTN_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

End Class
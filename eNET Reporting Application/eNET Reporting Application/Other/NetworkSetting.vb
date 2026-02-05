Imports Renci.SshNet

Public Class NetworkSetting

    Private Sub BTN_ok_Click(sender As Object, e As EventArgs) Handles BTN_ok.Click
        Using client = New SshClient(SetupForm2.IP_TB.Text, "pi", "raspberry")
            client.Connect()
            Using cmd = client.CreateCommand("sudo echo ""allow hotplug wlan0\niface wlan0 inet static\naddress " & TB_IP.Text & " \nnetmask " & TB_mask.Text & "\ngateway " & TB_gatway.Text & "\nwpa-ssid \""" & TB_SSID.Text & "\""\" & CB_sec.SelectedItem & "\""" & TB_PWD.Text & "\"" > /etc/network/interfaces.d/usbdongle""")
                cmd.Execute()
                'Console.WriteLine("Command>" + cmd.CommandText)
                'Console.WriteLine("Return Value = {0}", cmd.ExitStatus)
            End Using
        End Using
    End Sub
End Class
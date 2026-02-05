Public Class IP_choice
    Private Sub ok_Click(sender As Object, e As EventArgs) Handles ok.Click
        If LB_list.SelectedItem.ToString.Length > 0 Then
            CSIFlexClientSetup.CB_IPAddress.Text = LB_list.SelectedItem
            Me.Hide()
            CSIFlexClientSetup.ShowDialog()
            Me.Close()
        Else
            MessageBox.Show("Please select your Machine IP first !")
        End If
    End Sub


    Private Sub IP_choice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim IPv4Address As New List(Of String)
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)
        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                IPv4Address.Add(ipheal.ToString())
            End If
        Next
        If IPv4Address.Count = 1 Then
            CSIFlexClientSetup.CB_IPAddress.Text = IPv4Address(0).ToString()
            Me.Hide()
            'CSIFlexClientSetup.Show()
            Me.Close()
        ElseIf IPv4Address.Count > 1 Then
            LB_list.DataSource = IPv4Address
        End If
    End Sub
End Class
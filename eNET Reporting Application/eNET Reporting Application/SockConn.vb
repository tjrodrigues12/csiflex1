Imports System.Net.Sockets
Imports System.Net
Imports System.Threading
Imports System.Text
Imports System.Net.NetworkInformation

Public Class SockConn

    Public serverSocket_udp As Socket
    Public serverSocket_tcp As TcpListener
    Public devices As Integer = 0


    Public send_to As IPAddress




    Private Sub main_form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub server_udp()
        Dim subnet As String = ""

        Dim localIPtemp As String = ""

        Dim ip() As Net.IPAddress = System.Net.Dns.GetHostAddresses("")
        If ip.Count > 0 Then
            For Each ipadd As Net.IPAddress In ip
                If Split(ipadd.ToString(), ".").Count = 4 Then
                    localIPtemp = ipadd.ToString()
                End If
            Next
        End If

        Dim Interfaces As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
        For Each [Interface] As NetworkInterface In Interfaces
            If [Interface].NetworkInterfaceType = NetworkInterfaceType.Loopback Then
                Continue For
            End If
            Dim UnicastIPInfoCol As UnicastIPAddressInformationCollection = [Interface].GetIPProperties().UnicastAddresses
            For Each UnicatIPInfo As UnicastIPAddressInformation In UnicastIPInfoCol
                If localIPtemp = UnicatIPInfo.Address.ToString() Then
                    subnet = UnicatIPInfo.IPv4Mask.ToString()
                End If

            Next
        Next


        Dim localip = IPAddress.Parse(localIPtemp)
        Dim mask = IPAddress.Parse(subnet)
        Dim ipUnsignedInteger = BitConverter.ToUInt32(localip.GetAddressBytes, 0)
        Dim maskUnsignedInteger = BitConverter.ToUInt32(mask.GetAddressBytes, 0)

        Dim broadcastAddress = New IPAddress(CUInt(ipUnsignedInteger) Or (Not CUInt(maskUnsignedInteger)))

        Dim end_point As IPEndPoint = New IPEndPoint(broadcastAddress, 18322)
        Try
            Dim i As Integer = 0
            While (i < 5)
                Dim send_buffer As Byte() = Encoding.ASCII.GetBytes("server")
                serverSocket_udp.SendTo(send_buffer, end_point)
                Thread.Sleep(200)
                serverSocket_udp.SendTo(send_buffer, end_point)
                Thread.Sleep(200)
                serverSocket_udp.SendTo(send_buffer, end_point)
                Thread.Sleep(2000)
                i += 1
            End While
        Catch ex As Exception

        Finally
            serverSocket_udp.Close()
            serverSocket_tcp.Stop()

        End Try
        Try
            'Label2.Invoke(Sub() Label2.Text = "Done, " + devices.ToString() + " devices found.")
            btn_scan.Invoke(Sub() btn_scan.Text = "Done")
            Thread.Sleep(1500)

            'CloseMe()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub server_tcp()
        serverSocket_tcp.Start()
        Try
            While (True)

                Dim TcpClient As TcpClient
                TcpClient = serverSocket_tcp.AcceptTcpClient()
                Dim receiver As New Thread(AddressOf recieve_From_client)
                receiver.Start(TcpClient)

            End While


        Catch ex As Exception
            serverSocket_tcp.Stop()
        End Try

    End Sub

    Private Sub recieve_From_client(cs As Object)

        Dim clientSocket As TcpClient = DirectCast(cs, TcpClient)

        Dim ipend As Net.IPEndPoint = clientSocket.Client.RemoteEndPoint
        dgv_1.Invoke(Sub() dgv_1.Rows.Add(ipend.ToString().Split(":")(0)))
        devices += 1

    End Sub

    Private Sub btn_scan_Click(sender As Object, e As EventArgs) Handles btn_scan.Click

        'If tb_ipAdress.Text <> "" Then
        btn_scan.Enabled = False
        'initialisation des serveur tcp et udp
        serverSocket_udp = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
        serverSocket_tcp = New TcpListener(IPAddress.Any, 9202)
        dgv_1.Rows.Clear()
        Dim tcp_srv As New Thread(AddressOf server_tcp)
        tcp_srv.Start()
        btn_scan.Text = "Scaning"
        Dim udp_srv As New Thread(AddressOf server_udp)
        udp_srv.Start()

        'Dim p = New Program(tb_ipAdress.Text)
        'p.Run()
        'p.Dispose()

        'End If

    End Sub

    Private Sub CloseMe()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf CloseMe))
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Sub SockConn_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            serverSocket_tcp.Stop()
            serverSocket_udp.Close()
        Catch ex As Exception

        End Try
        
    End Sub
End Class

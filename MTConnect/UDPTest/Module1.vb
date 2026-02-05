Imports System.Net.Sockets
Imports System.Net
Imports System.Text

Module Module1

    Sub Main()

        Dim ipadr As String = "10.0.10.26"
        Dim port As String = "8080"

        ''''''''''''''''''1
        'Dim client = New UdpClient()
        'Dim ep As New IPEndPoint(IPAddress.Parse(ipadr), Integer.Parse(port))
        ' endpoint where server is listening
        'client.Connect(ep)

        '' send data
        'Dim welcome As String = "LOG-IN INC"
        'Dim data = Encoding.ASCII.GetBytes(welcome)
        'client.Send(data, data.Length)

        '' then receive data
        'Dim receivedData = client.Receive(ep)

        'Console.Write("receive data from " + ep.ToString())

        'Console.Read()


        '''''''''''''''''2
        'Dim clientSocket As New System.Net.Sockets.TcpClient()

        'msg("Client Started")
        'clientSocket.Connect(ipadr, Integer.Parse(port))
        'Console.WriteLine("Client Socket Program - Server Connected ...")

        'Dim serverStream As NetworkStream = clientSocket.GetStream()
        'Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("Message from Client$")
        'serverStream.Write(outStream, 0, outStream.Length)
        'serverStream.Flush()

        'Dim inStream(10024) As Byte
        'serverStream.Read(inStream, 0, CInt(clientSocket.ReceiveBufferSize))
        'Dim returndata As String = System.Text.Encoding.ASCII.GetString(inStream)
        'msg("Data from Server : " + returndata)

        '''''''''''''''''''3
        'Dim result As String = SocketSendReceive(ipadr, Integer.Parse(port))

        'Console.WriteLine(result)

        ''''''''''''''''4
        'Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("Message from Client$")
        ' [-c] [-n server] [-p port] [-m message][-t tcp|udp] [-x size]
        'ClientSocket.Main({"randomfilename", "-c", "-n", "10.0.10.26", "-p", "8080", "-m", "LOG-IN INC", "-t", "tcp"})
        If (ClientSocket.TestEnet("10.0.10.26", 8080)) Then
            Console.WriteLine("WOOOOHOOO")
        Else
            Console.WriteLine("BOUUUUU")
        End If

        Console.Read()

    End Sub

    Sub msg(ByVal mesg As String)
        Console.WriteLine(Environment.NewLine + " >> " + mesg)
    End Sub


    Private Function ConnectSocket(server As String, port As Integer) As Socket
        Dim s As Socket = Nothing
        Dim hostEntry As IPHostEntry = Nothing

        ' Get host related information.
        hostEntry = Dns.GetHostEntry(server)

        ' Loop through the AddressList to obtain the supported AddressFamily. This is to avoid 
        ' an exception that occurs when the host host IP Address is not compatible with the address family 
        ' (typical in the IPv6 case). 
        Dim address As IPAddress

        For Each address In hostEntry.AddressList
            Dim endPoint As New IPEndPoint(address, port)
            Dim tempSocket As New Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)

            tempSocket.Connect(endPoint)

            If tempSocket.Connected Then
                s = tempSocket
                Exit For
            End If

        Next address

        Return s
    End Function


    ' This method requests the home page content for the specified server. 
    Private Function SocketSendReceive(server As String, port As Integer) As String
        'Set up variables and String to write to the server. 
        Dim ascii As Encoding = Encoding.ASCII
        Dim request As String = "GET / HTTP/1.1" + ControlChars.Cr + ControlChars.Lf + "Host: " + server + ControlChars.Cr + ControlChars.Lf + "Connection: Close" + ControlChars.Cr + ControlChars.Lf + ControlChars.Cr + ControlChars.Lf
        Dim bytesSent As [Byte]() = ascii.GetBytes(request)
        Dim bytesReceived(255) As [Byte]

        ' Create a socket connection with the specified server and port. 
        Dim s As Socket = ConnectSocket(server, port)

        If s Is Nothing Then
            Return "Connection failed"
        End If
        ' Send request to the server.
        s.Send(bytesSent, bytesSent.Length, 0)

        ' Receive the server  home page content. 
        Dim bytes As Int32

        ' Read the first 256 bytes. 
        Dim page As [String] = "Default HTML page on " + server + ":" + ControlChars.Cr + ControlChars.Lf

        ' The following will block until the page is transmitted. 
        Do
            bytes = s.Receive(bytesReceived, bytesReceived.Length, 0)
            page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes)
        Loop While bytes > 0

        Return page
    End Function

End Module

Imports System.Net.Sockets
Imports System.Net
Imports System.IO
Imports System.Text

'SOURCE:http://www.winsocketdotnetworkprogramming.com/clientserversocketnetworkcommunication8c_1.html

' This is a simple TCP and UDP based client.
Public Class ClientSocket
    ' This routine repeatedly copies a string message into a byte array until filled.
    ' <param name="dataBuffer">Byte buffer to fill with string message</param>
    ' <param name="message">String message to copy</param>
    Private Shared Sub FormatBuffer(ByVal dataBuffer As Byte(), ByVal message As String)

        Dim byteMessage As Byte() = System.Text.Encoding.ASCII.GetBytes(message)
        Dim index As Integer = 0

        ' First convert the string to bytes and then copy into send buffer
        While (index < dataBuffer.GetUpperBound(0))

            Dim j As Integer
            For j = 0 To byteMessage.GetUpperBound(0) - 1

                dataBuffer(index) = byteMessage(j)
                index = index + 1

                ' Make sure we don't go past the send buffer length
                If (index >= dataBuffer.GetUpperBound(0) - 1) Then
                    GoTo AfterLoop
                End If
            Next
AfterLoop:
        End While
    End Sub

    ' Prints simple usage information.
    Private Sub usage()
        Console.WriteLine("usage: Executable_file_name [-c] [-n server] [-p port] [-m message]")
        Console.WriteLine("                        [-t tcp|udp] [-x size]")
        Console.WriteLine("     -c              If UDP connect the socket before sending")
        Console.WriteLine("     -n server       Server name or address to connect/send to")
        Console.WriteLine("     -p port         Port number to connect/send to")
        Console.WriteLine("     -m message      String message to format in request buffer")
        Console.WriteLine("     -t tcp|udp      Indicates to use either the TCP or UDP protocol")
        Console.WriteLine("     -x size         Size of send and receive buffers")
        Console.WriteLine("...Else default values will be used...")
        Console.WriteLine()
    End Sub

    ' This is the main function for the simple client. It parses the command line and creates
    ' a socket of the requested type. For TCP, it will resolve the name and attempt to connect
    ' to each resolved address until a successful connection is made. Once connected a request
    ' message is sent followed by shutting down the send connection. The client then receives
    ' data until the server closes its side at which point the client socket is closed. For
    ' UDP, the socket is created and if indicated connected to the server's address. A single
    ' request datagram message. The client then waits to receive a response and continues to
    ' do so until a zero byte datagram is receive which indicates the end of the response.
    ' <param name="args">Command line arguments</param>
    Public Shared Function TestEnet(ip As String, port As Integer) As Boolean '(ByVal args()) As Boolean

        Dim res As Boolean = False

        Dim sockType As SocketType = SocketType.Stream
        Dim sockProtocol As ProtocolType = ProtocolType.Tcp
        Dim remoteName As String = "localhost"
        Dim textMessage As String = "Client: This is a test from client"
        Dim udpConnect As Boolean = False
        Dim remotePort As Integer = 5150
        Dim bufferSize As Integer = 4096

        'usage()
        Console.WriteLine()

        udpConnect = True
        remoteName = ip
        remotePort = port
        textMessage = "LOG-IN INC"
        sockType = SocketType.Stream
        sockProtocol = ProtocolType.Tcp

        ' Parse the command line
        'Dim args As String() = Environment.GetCommandLineArgs()
        'Dim i As Integer
        'For i = 0 To args.GetUpperBound(0)
        '    Try
        '        Dim CurArg() As Char = args(i).ToCharArray(0, args(i).Length)
        '        If (CurArg(0) = "-") Or (CurArg(0) = "/") Then
        '            Select Case Char.ToLower(CurArg(1), System.Globalization.CultureInfo.CurrentCulture)
        '                Case "c" ' "Connect" the UDP socket to the destination
        '                    udpConnect = True
        '                Case "n" ' Destination address to connect to or send to
        '                    i = i + 1
        '                    remoteName = args(i)
        '                Case "m"       ' Text message to put into the send buffer
        '                    i = i + 1
        '                    textMessage = args(i)
        '                Case "p"       ' Port number for the destination
        '                    i = i + 1
        '                    remotePort = System.Convert.ToInt32(args(i))
        '                Case "t"       ' Specified TCP or UDP
        '                    i = i + 1
        '                    If (args(i) = "tcp") Then
        '                        sockType = SocketType.Stream
        '                        sockProtocol = ProtocolType.Tcp

        '                    ElseIf (args(i) = "udp") Then
        '                        sockType = SocketType.Dgram
        '                        sockProtocol = ProtocolType.Udp
        '                    Else
        '                        usage()
        '                        'Exit Function
        '                    End If
        '                Case "x"       ' Size of the send and receive buffers
        '                    i = i + 1
        '                    bufferSize = System.Convert.ToInt32(args(i))
        '                Case Else
        '                    usage()
        '                    Exit Function
        '            End Select
        '        End If
        '    Catch e As Exception
        '        usage()
        '        Exit Function
        '    End Try
        'Next

        Dim clientSocket As Socket = Nothing
        'Dim resolvedHost As IPHostEntry = Nothing
        Dim destination As IPEndPoint = Nothing
        Dim sendBuffer(bufferSize) As Byte
        Dim recvBuffer(bufferSize) As Byte
        Dim rc As Integer

        ' Format the string message into the send buffer
        FormatBuffer(sendBuffer, textMessage)

        Try
            ' Try to resolve the remote host name or address
            'resolvedHost = Dns.GetHostEntry(remoteName)
            'Console.WriteLine("Client: GetHostEntry() is OK...")

            ' Try each address returned
            Dim addr As IPAddress = IPAddress.Parse(ip)
            'For Each addr In resolvedHost.AddressList
            ' Create a socket corresponding to the address family of the resolved address
            clientSocket = New Socket(addr.AddressFamily, sockType, sockProtocol)
            Console.WriteLine("Client: Socket() is OK...")

            Try
                ' Create the endpoint that describes the destination
                destination = New IPEndPoint(addr, remotePort)
                Console.WriteLine("Client: IPEndPoint() for the destination is OK...")

                If ((sockProtocol = ProtocolType.Udp) And (udpConnect = False)) Then
                    Console.WriteLine("Client: Destination address is: {0}", destination.ToString())
                    GoTo BreakConnectLoop

                Else
                    Console.WriteLine("Client: Attempting connection to: {0}", destination.ToString())
                End If

                clientSocket.Connect(destination)
                Console.WriteLine("Client: Connect() is OK...")
                GoTo BreakConnectLoop

            Catch err As SocketException
                ' Connect failed so close the socket and try the next address
                clientSocket.Close()
                Console.WriteLine("Client: Close() is OK...")
                clientSocket = Nothing
                GoTo ContinueConnectLoop
            End Try
ContinueConnectLoop:

            'Next
BreakConnectLoop:

            ' Make sure we have a valid socket before trying to use it
            If (Not IsNothing(clientSocket)) And (Not IsNothing(destination)) Then
                Try
                    ' Send the request to the server
                    If ((sockProtocol = ProtocolType.Udp) And (udpConnect = False)) Then
                        clientSocket.SendTo(sendBuffer, destination)
                        Console.WriteLine("Client: SendTo() is OK...UDP...")
                    Else
                        rc = clientSocket.Send(sendBuffer)
                        Console.WriteLine("Client: Send() is OK...")
                        Console.WriteLine("Client: Sent request of {0} bytes", rc)

                        ' For TCP, shutdown sending on our side since the client won't send any more data
                        If (sockProtocol = ProtocolType.Tcp) Then
                            clientSocket.Shutdown(SocketShutdown.Send)
                            Console.WriteLine("Client: Shutdown() is OK...")
                        End If
                    End If

                    ' Receive data in a loop until the server closes the connection. For
                    '    TCP this occurs when the server performs a shutdown or closes
                    '    the socket. For UDP, we'll know to exit when the remote host
                    '    sends a zero byte datagram.
                    While (True)
                        If ((sockProtocol = ProtocolType.Tcp) Or (udpConnect = True)) Then
                            rc = clientSocket.Receive(recvBuffer)
                            Console.WriteLine("Client: Receive() is OK...")
                            Console.WriteLine("Client: Read {0} bytes", rc)

                            Dim responseData = System.Text.Encoding.UTF8.GetString(recvBuffer)
                            Console.WriteLine("READ::" + responseData)
                            If (responseData.StartsWith("HTTP")) Then
                                rc = 0
                                res = True
                            End If
                        Else
                            Dim fromEndPoint As IPEndPoint = New IPEndPoint(destination.Address, 0)
                            Dim castFromEndPoint As EndPoint = CType(fromEndPoint, EndPoint)

                            rc = clientSocket.ReceiveFrom(recvBuffer, castFromEndPoint)
                            Console.WriteLine("ReceiveFrom() is OK...")
                            fromEndPoint = CType(castFromEndPoint, IPEndPoint)
                            Console.WriteLine("Client: Read {0} bytes from {1}", rc, fromEndPoint.ToString())

                            Dim responseData = System.Text.Encoding.UTF8.GetString(recvBuffer)
                            Console.WriteLine("READ::" + responseData)
                            If (responseData.StartsWith("HTTP")) Then
                                rc = 0
                                res = True
                            End If
                        End If
                        ' Exit loop if server indicates shutdown
                        If (rc = 0) Then
                            clientSocket.Close()
                            Console.WriteLine("Client: Close() is OK...")
                            GoTo AfterWhile
                        End If
                    End While
AfterWhile:
                Catch err As SocketException
                    Console.WriteLine("Client: Error occurred while sending or receiving data.")
                    Console.WriteLine("   Error: {0}", err.Message)
                End Try
            Else
                Console.WriteLine("Client: Unable to establish connection to server!")
            End If
        Catch err As SocketException
            Console.WriteLine("Client: Socket error occurred: {0}", err.Message)
        End Try

        Return res
    End Function
End Class
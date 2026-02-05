Imports System.Net.Sockets
Imports System.Net
Imports System.Threading
Imports System.Text
Imports System.Collections.Generic
Imports System.IO

Namespace InternalWebServer
    Public Class Server
        Public running As Boolean = False 'Is it running?
        Private timeout As Integer = 8    ' Time limit for data transfers.
        Private charEncoder As Encoding = Encoding.UTF8    ' To encode string
        Private serverSocket As Socket 'Our server socket
        Private contentPath As String 'Root path of our contents

        ' Content types that are supported by our server
        ' You can add more...
        ' To see other types: http://www.webmaster-toolkit.com/mime-types.shtml
        Private extensions As Dictionary(Of String, String) = New Dictionary(Of String, String)() From
            {
    {"htm", "text/html"},
    {"html", "text/html"},
    {"aspx", "text/html"},
    {"vbhtml", "text/html"},
    {"cshtml", "text/html"},
    {"php", "text/html"},
    {"xml", "text/xml"},
    {"txt", "text/plain"},
    {"css", "text/css"},
    {"js", "text/javascript"},
    {"eot", "application/vnd.ms-fontobject"},
    {"woff2", "font/woff2"},
    {"woff", "font/woff"},
    {"ttf", "font/ttf"},
    {"svg", "image/svg+xml"},
    {"png", "image/png"},
    {"gif", "image/gif"},
    {"jpg", "image/jpg"},
    {"jpeg", "image/jpeg"},
    {"zip", "application/zip"}
    }


        Public Sub [stop]()
            If running Then
                running = False

                Try
                    serverSocket.Close()
                Catch
                End Try

                serverSocket = Nothing
            End If
        End Sub
        Public Function start(ByVal ipAddress As IPAddress, ByVal port As Integer, ByVal maxNOfCon As Integer, ByVal contentPath As String) As Boolean
            If running Then Return False

            Try
                serverSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                serverSocket.Bind(New IPEndPoint(ipAddress, port))
                serverSocket.Listen(maxNOfCon)
                serverSocket.ReceiveTimeout = timeout
                serverSocket.SendTimeout = timeout
                running = True
                Me.contentPath = contentPath
            Catch
                Return False
            End Try
            Dim requestListenerT As Thread = New Thread(AddressOf ClientSocketProgramming)
#If False Then
 'Dim requestListenerT As Thread = New Thread(Function()

            '                                                While running
            '                                                    Dim clientSocket As Socket

            '                                                    Try
            '                                                        clientSocket = serverSocket.Accept()
            '                                                        Dim requestHandler As Thread = New Thread(Function()
            '                                                                                                      clientSocket.ReceiveTimeout = timeout
            '                                                                                                      clientSocket.SendTimeout = timeout

            '                                                                                                      Try
            '                                                                                                          handleTheRequest(clientSocket)
            '                                                                                                      Catch

            '                                                                                                          Try
            '                                                                                                              clientSocket.Close()
            '                                                                                                          Catch
            '                                                                                                          End Try
            '                                                                                                      End Try
            '                                                                                                  End Function)
            '                                                        requestHandler.Start()
            '                                                    Catch
            '                                                    End Try
            '                                                End While
            '                                            End Function)
#End If
            requestListenerT.Start()
            Return True
        End Function
        Public Sub ClientSocketProgramming()
            While running
                Dim clientSocket As Socket
                Try
                    clientSocket = serverSocket.Accept()
                    Dim requestHandler As Thread = New Thread(AddressOf RequestHandlerCode)
                    requestHandler.Start(clientSocket)
                Catch ex As Exception
                    'MessageBox.Show("Error in ClientSocketProgramming()" & ex.Message())
                End Try
            End While

        End Sub
        Public Sub RequestHandlerCode(sock As Socket)
            sock.ReceiveTimeout = timeout
            sock.SendTimeout = timeout
            Try
                handleTheRequest(sock)
            Catch
                Try
                    sock.Close()
                Catch
                End Try
            End Try
        End Sub
        Private Sub handleTheRequest(ByVal clientSocket As Socket)
            Dim buffer As Byte() = New Byte(513999) {}
            'Dim buffer As Byte() = New Byte(10240) {}
            Dim receivedBCount As Integer = clientSocket.Receive(buffer)
            Dim strReceived As String = charEncoder.GetString(buffer, 0, receivedBCount)
            Dim httpMethod As String = strReceived.Substring(0, strReceived.IndexOf(" "))
            Dim start As Integer = strReceived.IndexOf(httpMethod) + httpMethod.Length + 1
            Dim length As Integer = strReceived.LastIndexOf("HTTP") - start - 1
            Dim requestedUrl As String = strReceived.Substring(start, length)
            Dim requestedFile As String

            If httpMethod.Equals("GET") OrElse httpMethod.Equals("POST") Then
                requestedFile = requestedUrl.Split("?"c)(0)
            Else
                notImplemented(clientSocket)
                Return
            End If

            requestedFile = requestedFile.Replace("/", "\").Replace("\..", "")
            start = requestedFile.LastIndexOf("."c) + 1

            If start > 0 Then
                length = requestedFile.Length - start
                Dim extension As String = requestedFile.Substring(start, length)

                If extensions.ContainsKey(extension) Then

                    If File.Exists(contentPath & requestedFile) Then
                        sendOkResponse(clientSocket, File.ReadAllBytes(contentPath & requestedFile), extensions(extension))
                    Else
                        notFound(clientSocket)
                    End If
                End If
            Else
                If requestedFile.Substring(length - 1, 1) <> "\" Then requestedFile += "\"

                If File.Exists(contentPath & requestedFile & "index.htm") Then
                    sendOkResponse(clientSocket, File.ReadAllBytes(contentPath & requestedFile & "\index.htm"), "text/html")
                ElseIf File.Exists(contentPath & requestedFile & "1_overview2.html") Then
                    sendOkResponse(clientSocket, File.ReadAllBytes(contentPath & requestedFile & "\1_overview2.html"), "text/html")
                ElseIf File.Exists(contentPath & requestedFile & "Default.aspx") Then
                    sendOkResponse(clientSocket, File.ReadAllBytes(contentPath & requestedFile & "\Default2.aspx"), "text/html")
                Else
                    notFound(clientSocket)
                End If
            End If
        End Sub

        Private Sub notImplemented(ByVal clientSocket As Socket)
            sendResponse(clientSocket, "<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""></head><body><h2>CSIFlex Server</h2><div>501 - Method Not Implemented</div></body></html>", "501 Not Implemented", "text/html")
        End Sub

        Private Sub notFound(ByVal clientSocket As Socket)
            sendResponse(clientSocket, "<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""></head><body><h2>CSIFlex Server</h2><div>404 - Page Not Found</div></body></html>", "404 Not Found", "text/html")
        End Sub

        Private Sub sendOkResponse(ByVal clientSocket As Socket, ByVal bContent As Byte(), ByVal contentType As String)
            sendResponse(clientSocket, bContent, "200 OK", contentType)
        End Sub

        Private Sub sendResponse(ByVal clientSocket As Socket, ByVal strContent As String, ByVal responseCode As String, ByVal contentType As String)
            Dim bContent As Byte() = charEncoder.GetBytes(strContent)
            sendResponse(clientSocket, bContent, responseCode, contentType)
        End Sub

        Private Sub sendResponse(ByVal clientSocket As Socket, ByVal bContent As Byte(), ByVal responseCode As String, ByVal contentType As String)
            Try
                Dim bHeader As Byte() = charEncoder.GetBytes("HTTP/1.1 " & responseCode & vbCrLf & "Server: CSIFlex Web Server" & vbCrLf & "Content-Length: " & bContent.Length.ToString() & vbCrLf & "Connection: close" & vbCrLf & "Content-Type: " & contentType & vbCrLf & vbCrLf)
                clientSocket.Send(bHeader)
                clientSocket.Send(bContent)
                clientSocket.Close()
            Catch
            End Try
        End Sub
    End Class

End Namespace



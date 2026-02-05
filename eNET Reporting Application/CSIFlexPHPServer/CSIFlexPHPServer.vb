Imports System.Web
Imports System.Web.Hosting
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.IO
Imports System.Xml
Imports System.Text
Imports ASPX
Public Class CSIFlexPHPServer
    Inherits System.ServiceProcess.ServiceBase
    Dim myServer As CSIFlexHttpServer
    Public Sub New()
        Me.ServiceName = "CSIFlexPHPServer"
        Me.CanStop = True
        Me.CanPauseAndContinue = True
        Me.AutoLog = True
    End Sub
    Shared Sub Main()
        System.ServiceProcess.ServiceBase.Run(New CSIFlexPHPServer)
    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)
        myServer = New CSIFlexHttpServer()
        Dim thread As New Thread(New ThreadStart(AddressOf myServer.StartListen))
        thread.Start()
    End Sub
    Protected Overrides Sub OnStop()
        myServer.StopListen()
        Threading.Thread.Sleep(1000)
        myServer = Nothing
    End Sub
End Class
Public Class CSIFlexHttpServer
    Private myListener As TcpListener
    Dim xdoc As XDocument
    Dim serverRoot As String
    Dim errorMessage As String
    Dim badRequest As String
    Dim randObj As New Object()
    Dim active As Boolean = True
    Dim SERVER_NAME As String

    Sub New()
        Try
            'load xml-file with all configuration
            xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory & "\serverConfig.xml")
            'two messages about errors
            errorMessage = "<html><body><h2>Requested file not found</h2></body></html>"
            badRequest = "<html><body><h2>Bad Request</h2></body></html>"
            'define the port
            Dim port As Integer = xdoc.Element("configuration").Element("Host").Element("Port").Value
            SERVER_NAME = xdoc.Element("configuration").Element("serverName").Value
            'define the directory of the web pages
            serverRoot = xdoc.Element("configuration").Element("Host").Element("Dir").Value
            myListener = New TcpListener(IPAddress.Any, port)
            myListener.Start()
        Catch ex As Exception
        End Try
    End Sub

    'Get MIME of the file
    Private Function GetMimeType(ByVal extention As String) As String
        For Each xel As XElement In xdoc.Element("configuration").Element("Mime").Elements("Values")
            If xel.Element("Ext").Value = extention Then Return xel.Element("Type").Value
        Next
        Return "text/html"
    End Function

    'Get default web pages
    Private Function Get_DefaultPage(ByVal serverFolder As String) As String
        For Each xel As XElement In xdoc.Element("configuration").Element("Default").Elements("File")
            If File.Exists(serverFolder & "\" & xel.Value) Then
                Return xel.Value
            End If
        Next
        Return ""
    End Function
    'Send content
    Private Sub SendData(ByVal data As Byte(), ByRef sockets As Socket)
        Try
            sockets.Send(data, data.Length, SocketFlags.None)
        Catch ex As Exception
        End Try
    End Sub
    'Overloaded method
    Private Sub SendData(ByVal data As String, ByRef sockets As Socket)
        SendData(Encoding.Default.GetBytes(data), sockets)
    End Sub
    'Send the headers
    Private Sub SendHeader(ByVal HttpVersion As String, ByVal MimeType As String, ByVal totalBytes As Integer, ByVal statusCode As String, ByRef sockets As Socket)

        Dim ss As New StringBuilder()

        If MimeType = "" Then MimeType = "text/html"

        ss.Append(HttpVersion)
        ss.Append(statusCode).AppendLine()
        ss.AppendLine("Sever: CSIFlexMobileServer")
        ss.Append("Content-Type: ")
        ss.Append(MimeType).AppendLine()
        ss.Append("Accept-Ranges: bytes").AppendLine()
        ss.Append("Content-Length: ")
        ss.Append(totalBytes).AppendLine().AppendLine()

        Dim data_ToSend As Byte() = Encoding.ASCII.GetBytes(ss.ToString())
        ss.Clear()
        SendData(data_ToSend, sockets)
    End Sub

    Private Function GetCgiData(ByVal cgiFile As String, ByVal QUERY_STRING As String, ByVal ext As String, ByVal remote_address As String,
                                ByVal SERVER_PROTOCOL As String, ByVal REFERER As String, ByVal REQUESTED_METHOD As String, ByVal USER_AGENT As String, ByVal request As String) As String
        Dim proc As New System.Diagnostics.Process()

        'indicate the executable to get stdout
        If ext = ".php" Then
            proc.StartInfo.FileName = xdoc.Element("configuration").Element("php").Element("Path").Value & "\\php-cgi.exe"
            'if path to the php is not defined
            If Not File.Exists(proc.StartInfo.FileName) Then
                Return errorMessage
            End If
            proc.StartInfo.Arguments = " -q " & cgiFile & " " & QUERY_STRING
        Else
            proc.StartInfo.FileName = cgiFile
            proc.StartInfo.Arguments = QUERY_STRING
        End If
        Dim script_name As String = cgiFile.Substring(cgiFile.LastIndexOf("\"c) + 1)
        'Set some global variables and output parameters
        proc.StartInfo.EnvironmentVariables.Add("REMOTE_ADDR", remote_address.ToString())
        proc.StartInfo.EnvironmentVariables.Add("SCRIPT_NAME", script_name)
        proc.StartInfo.EnvironmentVariables.Add("USER_AGENT", USER_AGENT)
        proc.StartInfo.EnvironmentVariables.Add("REQUESTED_METHOD", REQUESTED_METHOD)
        proc.StartInfo.EnvironmentVariables.Add("REFERER", REFERER)
        proc.StartInfo.EnvironmentVariables.Add("SERVER_PROTOCOL", SERVER_PROTOCOL)
        proc.StartInfo.EnvironmentVariables.Add("QUERY_STRING", request)

        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        Dim str As String = ""

        proc.Start()
        str = proc.StandardOutput.ReadToEnd()
        proc.Close()
        proc.Dispose()

        Return str
    End Function

    'Listen incoming connections
    Protected Friend Sub StartListen()
        While active = True
            Dim sockets As Socket = myListener.AcceptSocket()
            Dim listening As New Thread(AddressOf HttpThread)
            listening.Start(sockets)
        End While
    End Sub
    'Process the requests
    Private Sub HttpThread(ByVal sockets As Socket)
        Dim request As String
        Dim requestedFile As String = ""
        Dim mimeType As String = ""
        Dim filePath As String = ""
        Dim QUERY_STRING As String = ""
        Dim REQUESTED_METHOD As String = ""
        Dim REFERER As String = ""
        Dim USER_AGENT As String = ""
        Dim SERVER_PROTOCOL As String = "HTTP/1.1"
        Dim erMesLen As Integer = errorMessage.Length
        Dim badMesLen As Integer = badRequest.Length
        Dim logStream As StreamWriter
        Dim remoteAddress As String = ""

        If sockets.Connected = True Then
            remoteAddress = sockets.RemoteEndPoint.ToString()
            'get request from the client and decode it
            Dim received() As Byte = New Byte(1024) {}
            Dim i As Integer = sockets.Receive(received, received.Length, 0)
            Dim sBuffer As String = Encoding.ASCII.GetString(received)
            If sBuffer = "" Then
                sockets.Close()
                Exit Sub
            End If

            'Sure that is HTTP -request and get its version
            Dim startPos As Integer = sBuffer.IndexOf("HTTP", 1)
            If startPos = -1 Then
                SendHeader(SERVER_PROTOCOL, "", badMesLen, "400 Bad Request", sockets)
                SendData(badRequest, sockets)
                sockets.Close()
                Exit Sub
            Else
                SERVER_PROTOCOL = sBuffer.Substring(startPos, 8)
            End If

            'Get other request parameters
            Dim params() As String = sBuffer.Split(New Char() {vbNewLine})
            For Each param As String In params
                'Get User-Agent
                If param.Trim.StartsWith("User-Agent") Then
                    USER_AGENT = param.Substring(12)
                    'Get Refferer
                ElseIf param.Trim.StartsWith("Referer") Then
                    REFERER = param.Trim.Substring(9)
                End If
            Next

            'Get request method
            REQUESTED_METHOD = sBuffer.Substring(0, sBuffer.IndexOf(" "))
            Dim lastPos As Integer = sBuffer.IndexOf("/"c) + 1
            request = sBuffer.Substring(lastPos, startPos - lastPos - 1)

            Select Case REQUESTED_METHOD
                Case "POST"
                    requestedFile = request.Replace("/", "\").Trim()
                    QUERY_STRING = params(params.Length - 1).Trim()
                    Exit Select
                Case "GET"
                    lastPos = request.IndexOf("?"c)
                    If lastPos > 0 Then
                        requestedFile = request.Substring(0, lastPos).Replace("/", "\")
                        QUERY_STRING = request.Substring(lastPos + 1)
                    Else
                        requestedFile = request.Substring(0).Replace("/", "\")
                    End If
                    Exit Select
                Case "HEAD" : Exit Select
                Case Else
                    SendHeader(SERVER_PROTOCOL, "", badMesLen, "400 Bad Request", sockets)
                    SendData(badRequest, sockets)
                    sockets.Close()
                    Exit Sub
            End Select

            'Get the full name of the requested file
            If requestedFile.Length = 0 Then
                requestedFile = Get_DefaultPage(serverRoot)
                If requestedFile = "" Then
                    SendHeader(SERVER_PROTOCOL, "", erMesLen, "404 Not Found", sockets)
                    SendData(errorMessage, sockets)
                End If
            End If
            filePath = serverRoot & "\" & requestedFile
            'If the file among forbidden files send the error message
            For Each forbidden As XElement In xdoc.Element("configuration").Element("Forbidden").Elements("Path")
                If filePath.StartsWith(forbidden.Value) Then
                    SendHeader(SERVER_PROTOCOL, "", erMesLen, "404 Not Found", sockets)
                    SendData(errorMessage, sockets)
                    sockets.Close()
                    Exit Sub
                End If
            Next

            'If there is no such file send error message
            If File.Exists(filePath) = False Then
                SendHeader(SERVER_PROTOCOL, "", erMesLen, "404 Not Found", sockets)
                SendData(errorMessage, sockets)
            Else
                Dim ext As String = New FileInfo(filePath).Extension.ToLower()
                mimeType = GetMimeType(ext)

                'process web pages
                If ext = ".aspx" Then
                    'Create an instance of Host class
                    Dim aspxHost As New Host()
                    'Pass to it filename and query string
                    Dim htmlOut As String = aspxHost.CreateHost(requestedFile, serverRoot, QUERY_STRING)
                    erMesLen = htmlOut.Length
                    SendHeader(SERVER_PROTOCOL, mimeType, erMesLen, " 200 OK", sockets)
                    SendData(htmlOut, sockets)
                ElseIf ext = ".php" OrElse ext = ".exe" Then
                    Dim cgi2html As String = GetCgiData(filePath, QUERY_STRING, ext, remoteAddress, SERVER_PROTOCOL, REFERER, REQUESTED_METHOD, USER_AGENT, request)
                    If cgi2html = errorMessage Then
                        SendHeader(SERVER_PROTOCOL, "", erMesLen, "404 Not Found", sockets)
                        SendData(errorMessage, sockets)
                    Else
                        erMesLen = cgi2html.Length
                        SendHeader(SERVER_PROTOCOL, mimeType, erMesLen, " 200 OK", sockets)
                        SendData(cgi2html, sockets)
                    End If
                Else
                    Dim fs As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)
                    Dim bytes() As Byte = New Byte(fs.Length) {}
                    erMesLen = bytes.Length
                    fs.Read(bytes, 0, erMesLen)
                    fs.Close()
                    SendHeader(SERVER_PROTOCOL, mimeType, erMesLen, "200 OK", sockets)
                    SendData(bytes, sockets)
                End If
            End If
            sockets.Close()

            Monitor.Enter(randObj)
            logStream = New StreamWriter("Server.log", True)
            'Output to the server log
            logStream.WriteLine(Date.Now.ToString())
            logStream.WriteLine("Connected to {0}", remoteAddress)
            logStream.WriteLine("Requested path {0}", request)
            logStream.WriteLine("Total bytes {0}", erMesLen)
            logStream.Flush()
            logStream.Close()
            Monitor.Exit(randObj)
        End If
    End Sub

    Protected Friend Sub StopListen()
        active = False
    End Sub
End Class

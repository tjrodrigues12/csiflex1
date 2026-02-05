Imports System.Net
#Disable Warning BC40056 ' Namespace or type specified in the Imports 'General_Setup' doesn't contain any public member or cannot be found. Make sure the namespace or the type is defined and contains at least one public member. Make sure the imported element name doesn't use any aliases.
Imports General_Setup
#Enable Warning BC40056 ' Namespace or type specified in the Imports 'General_Setup' doesn't contain any public member or cannot be found. Make sure the namespace or the type is defined and contains at least one public member. Make sure the imported element name doesn't use any aliases.
Public Class Api
    Public General_Setup As New General_Setup

    'general'
    'network'
    'get'
    'view'
    'Check CSIFlex webserver'
    'post'
    'change'
    'save'

    Public Shared Sub handleRoutes(req As HttpListenerRequest, response As HttpListenerResponse)
        Dim path = req.RawUrl
        If (path.Contains("/api/general")) Then
            general(req, response)
        End If
    End Sub

    Shared Sub general(req As HttpListenerRequest, res As HttpListenerResponse)
        Dim path = req.Url.ToString()
        If (path.Contains("/api/general/change")) Then
            Dim query = req.QueryString
            Dim port = query.GetValues("port")
            General_Setup.update_Port(port(0), res)
            writeMessage(res, port(0))
        End If
    End Sub

    Shared Sub writeMessage(response As HttpListenerResponse, message As String)
        Dim output As System.IO.Stream
        Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(message)
        response.ContentLength64 = buffer.Length
        response.AppendHeader("Access-Control-Allow-Origin", "*")
        output = response.OutputStream
        output.Write(buffer, 0, buffer.Length)
        output.Close()
        output.Dispose()
    End Sub




End Class

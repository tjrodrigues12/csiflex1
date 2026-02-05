'Public Class Client

'End Class

Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Threading
Imports MySql.Data.MySqlClient

Public Class EnetClient

    'End Class



    '=======================================================================================================================
    '-----------------------------------------------------------------------------------------------------------------------
    ' Class Client : connect to the eNET html Server and gives the Live monitoring Data in a datatable
    '-----------------------------------------------------------------------------------------------------------------------
    '=======================================================================================================================

    'Public Class Client

#Region "Variables"
    Public xuser As String
    Public xpassword As String
    Public xproxy As String
    Public xport As String
    Public xproxypassword As String
    Public xproxyuser As String
    Public xphonenumber As String
    Private Shared CSILIB As New CSI_Library(True)
    Public logincookies As CookieContainer
    'Public srv As CSI_Library.Server

    Dim UA As String = "CSIFLEX Reporting Application"



    '-----------------------------------------------------------------------------------------------------------------------
    ' Name :  Data
    ' para :  
    ' Out  :  
    ' Com  : Structure used for the live data
    '-----------------------------------------------------------------------------------------------------------------------
    Public Structure Data
        Public status As String
        Public shift As String
        Public PartNumber As String
        Public CycleCount As String
        Public LastCycle As String
        Public CurrentCycle As String
        Public ElapsedTime As String
        Public feedOverride As String
    End Structure
#End Region

#Region "Methodes"
    '-----------------------------------------------------------------------------------------------------------------------
    ' connect to the eNET html Server with the IP:port and gives the Live monitoring Data in a datatable
    '-----------------------------------------------------------------------------------------------------------------------
    Public Function Run(ip_ As String) As DataTable
        Try
            'Format URI
            If ip_.StartsWith("http") Then

            Else
                ip_ = "http://" & ip_
            End If

            Dim targetURI As New Uri(ip_), page As String = ""
            Dim Request As System.Net.HttpWebRequest, table As New DataTable

            table.Columns.Add("machine", GetType(String))
            table.Columns.Add("Shift", GetType(String))
            table.Columns.Add("status", GetType(String))
            table.Columns.Add("PartNumber", GetType(String))
            table.Columns.Add("Condition", GetType(String))
            table.Columns("Condition").AllowDBNull = True
            table.Columns.Add("CycleCount", GetType(String))
            table.Columns.Add("LastCycle", GetType(String))
            table.Columns.Add("CurrentCycle", GetType(String))
            table.Columns.Add("ElapsedTime", GetType(String))
            table.Columns.Add("feedOverride", GetType(String))
            ' table.Columns.Add("palette", GetType(String))

            Thread.Sleep(1000)

            Try

                'If page = String.Empty Then
                '    page = GetDataFromWebpage(targetURI.AbsoluteUri)
                'End If
                If page = String.Empty Then
                    Request = DirectCast(HttpWebRequest.Create(targetURI), System.Net.HttpWebRequest)
                    With Request
                        .Timeout = 6000
                        .Method = "POST"
                        .KeepAlive = True
                        .ContentType = "text/xml"
                        .ContentLength = 0
                        .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"
                    End With
                    If (Request.GetResponse().ContentLength > 0) Then
                        Dim str As New System.IO.StreamReader(Request.GetResponse().GetResponseStream())
                        page = (str.ReadToEnd)
                        str.Close()
                    End If
                End If




            Catch ex As Exception
                If CSILIB.CheckLic(2) = 0 Then
                    Call CSILIB.LogServerError(ex.Message & " - " & CSILIB.TraceMessage("ERROR during requesting: "), 0)
                End If
            End Try


            Return html_parser(table, page)


        Catch ex As Exception
            'Log(Now & " : " & ex.Message)
            'If srv.CSIF_version = 0 Then
            If (CSILIB.CheckLic(2) = 0) Then
                Call CSILIB.LogServerError(ex.Message & " - " & CSILIB.TraceMessage("ERROR getting data from http server: "), 0)
            End If

            Return Nothing
        End Try

    End Function

    Private Function GetDataFromWebpage(urlval As String)
        Dim request As WebRequest = WebRequest.Create(urlval) '?SEL=-1 '?Refresh=Refresh'?Start=Start+AutoRefresh
        ' If required by the server, set the credentials.
        request.Credentials = CredentialCache.DefaultCredentials
        ' Get the response.
        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
        ' Display the status.
        'Console.WriteLine(response.StatusDescription)
        ' Get the stream containing content returned by the server.
        Dim dataStream As Stream = response.GetResponseStream()
        ' Open the stream using a StreamReader for easy access.
        Dim reader As New StreamReader(dataStream)
        ' Read the content.
        Dim responseFromServer As String = reader.ReadToEnd()
        ' Display the content.
        'MessageBox.Show(responseFromServer)
        ' Cleanup the streams and the response.
        ' Result_Web.DocumentText = responseFromServer
        reader.Close()
        dataStream.Close()
        response.Close()
        Return responseFromServer

    End Function
    '-----------------------------------------------------------------------------------------------------------------------
    ' Name : html_parser   
    ' para : LiveData As Dictionary(Of String, Data) , dic of (machine name , live data) and eNET html page
    ' Out  :Datatable 
    ' Com  :
    '-----------------------------------------------------------------------------------------------------------------------
    Private Function html_parser(table As DataTable, html_file As String) As DataTable

        Dim str As String, str2 As String(), row2 As String()
        Try
            If Not IsNothing(html_file) Then
                If html_file <> "" Then
                    If (Regex.Match(html_file, "Machine Monitoring").Success) Then ' check if it's the right page

                        str = Regex.Replace(html_file, "<td bgcolor=(.{9})>", "")
                        str = Regex.Replace(str, "<font color=(.{9})>", "")
                        str = Regex.Replace(str, "<td bgcolor=(.{9})'>", "")
                        str = Regex.Replace(str, "<font color='#0'>", "")
                        str = Regex.Replace(str, "</td>", "")

                        str2 = Split(str, "</table>")
                        str2 = Split(str2(2), "<tr align=""center"">")

                        Dim first As Boolean = True
                        For Each row In str2
                            If first = True Then
                                first = False
                            Else
                                If (row(1) <> "<") Then
                                    Dim DATA As New Data
                                    Dim r As DataRow = table.NewRow
                                    row2 = Split(row, "</font>")

                                    r("Machine") = row2(0).Remove(row2(0).Count - 5, 5).Replace(vbCrLf, "")
                                    r("ElapsedTime") = row2(6)
                                    r("CurrentCycle") = row2(5)
                                    r("shift") = (row2(0)(row2(0).Count - 1))
                                    r("status") = row2(1)

                                    r("feedOverride") = Replace(row2(7).Replace(" ", ""), "</tr>", "")

                                    r("LastCycle") = row2(4)
                                    r("CycleCount") = row2(3)
                                    r("PartNumber") = row2(2)
                                    If r("PartNumber") = " " Then r("PartNumber") = ""
                                    table.Rows.Add(r)
                                End If
                            End If
                        Next
                        Return table
                    End If
                    Return Nothing
                End If
                Return Nothing
            End If
        Catch ex As Exception

        End Try
        'parsing


    End Function



    'not used
    Private Function remove_tags(file As String) As String
        Return Regex.Replace(file, "<.*?>", "____")
    End Function

    'Not used
    Public Function login(ByVal user As String, ByVal password As String, ByVal port As String, ByVal URL As String)
        xuser = user
        xpassword = password

        xport = port


        ' logincookies = New CookieContainer
        'start the login phase
        'initiate the GET procedure for the login page
        'set the login url
        Dim loginpage As String = URL

        'the GET request
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(loginpage), HttpWebRequest)
        request.UserAgent = UA
        request.Referer = URL
        request.KeepAlive = True

        'set the cookies for the request to the public cookie container
        'request.CookieContainer = New CookieContainer()
        'request.CookieContainer = logincookies

        'get the response from the server
        Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)

        logincookies.Add(response.Cookies)

        'start the stream reader to read the response
        Dim reader As New StreamReader(response.GetResponseStream())
        Dim loginpagecontent As String = reader.ReadToEnd

        '-------------------------------
        'read the login page and get the parameters

        Dim pageGetLogin As String = loginpagecontent

        'Get the parameters
        Dim param1 As String = "this is a parameter value"
        Dim param2 As Object
        Dim param2fin As String
        param2 = Regex.Match(pageGetLogin, "(?<=param2""\s* value="")[\w\W]*?(?="")")
        param2fin = param2.ToString()
        Dim param3 As String = "this is a parameter value"
        Dim param4 As String = "this is a parameter value"
        '----------------------------------

        'creating the POST request
        'declaring the encoding type
        Dim encoding As New UTF8Encoding

        'creating the post data
        'creating the Request
        Dim postLoginRequest As HttpWebRequest = DirectCast(WebRequest.Create(URL & "/loginservice?param1=" & param1 & "&param2=" & param2fin & "&param3=" & param3 & "&param4=" & param4 & "&Passwd=" & password), HttpWebRequest)
        'postLoginRequest.CookieContainer = logincookies
        'postLoginRequest.CookieContainer = cookieContainer
        postLoginRequest.Method = "POST"
        postLoginRequest.KeepAlive = True
        postLoginRequest.AllowAutoRedirect = True
        postLoginRequest.MaximumAutomaticRedirections = 50
        postLoginRequest.ContentType = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
        postLoginRequest.Referer = URL
        postLoginRequest.UserAgent = UA
        postLoginRequest.ContentLength = 0
        postLoginRequest.AutomaticDecompression = DecompressionMethods.GZip

        postLoginRequest.CookieContainer = New CookieContainer
        postLoginRequest.CookieContainer = logincookies

        'getting the response to the POST request

        Dim postLoginResponse As HttpWebResponse
        postLoginResponse = DirectCast(postLoginRequest.GetResponse(), HttpWebResponse)

        logincookies.Add(postLoginResponse.Cookies)

        Dim postLoginReader As New StreamReader(postLoginResponse.GetResponseStream())
        Dim loginResponsePageContent As String = postLoginReader.ReadToEnd


        'show the parameters to check if they are find corectly - to comment out later
        MsgBox(param1 & "--" & param2 & "--" & user & "--" & xport & "--" & xproxyuser & "--" & xproxypassword)


        'serialize the cookies to bin format and save them to the disk
        '----------------------------------

        'set the name for the cookie file
        Dim filename As String = "d:/" & "cookie.dat"
        'delete the old file if it exists
        If (File.Exists(filename)) Then
            File.Delete(filename)
        End If
        'serialize and write the file
        Dim stream As FileStream = File.Create(filename)
        Dim formatter As New BinaryFormatter()
        formatter.Serialize(stream, response.Cookies)
        stream.Close()


        'Return the functions values
        'Return loginpagecontent
        Return loginResponsePageContent
    End Function

#End Region

End Class


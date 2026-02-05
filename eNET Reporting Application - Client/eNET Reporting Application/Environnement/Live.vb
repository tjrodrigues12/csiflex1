Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Runtime.Serialization.Formatters.Binary
Imports CSI_Library

Imports Codeplex.Dashboarding.Dial360
Imports System.Windows.Forms.Integration

Public Class Live



    Public CSI_Lib As New CSI_Library.CSI_Library
    Public srv As New CSI_Library.Server
    Public clt As New CSI_Library.Client
    Public status As String = "Status"
    Public table As New DataTable
    Public dial As New Codeplex.Dashboarding.Dial360

    Public dial2 As New Codeplex.Dashboarding.Dial360
    Public func As New Functions

    Private Sub Live_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Height = 250

        Dim pointCOFF As New Codeplex.Dashboarding.ColorPoint
        pointCOFF.HiColor = System.Windows.Media.Color.FromArgb(150, 255, 0, 0)
        pointCOFF.LowColor = System.Windows.Media.Color.FromArgb(150, 240, 0, 0)
        pointCOFF.Value = 0

        Dim pointCON As New Codeplex.Dashboarding.ColorPoint
        pointCON.HiColor = System.Windows.Media.Color.FromArgb(150, 0, 250, 0)
        pointCON.LowColor = System.Windows.Media.Color.FromArgb(150, 0, 200, 0)
        pointCON.Value = 70

        Dim pointOTHER As New Codeplex.Dashboarding.ColorPoint
        pointOTHER.HiColor = System.Windows.Media.Color.FromArgb(150, 255, 255, 0)
        pointOTHER.LowColor = System.Windows.Media.Color.FromArgb(150, 255, 200, 0)
        pointOTHER.Value = 30
        '=========================================================================
        ' Position + Transparent BG
        ' Me.MdiParent = Form1
        'Me.Location = New Point(Form3.Width, 10)
        'SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        'Me.BackColor = Color.Transparent
        'SetStyle(ControlStyles.DoubleBuffer, True)
        'SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        '=========================================================================

        '  Timer.Start()
        Dim elhost As ElementHost = ElementHost1
        'Dim elhost2 As ElementHost = ElementHost2
        '  elhost2.Location = New Point(175, 0)
        elhost.Height = 165
        elhost.Width = 165
 

        dial.Value = 50
        dial.ValueTextColor = System.Windows.Media.Color.FromArgb(150, 0, 0, 0)
        dial.FaceTextColor = System.Windows.Media.Color.FromArgb(150, 0, 0, 0)
        dial.FaceColorRange.Clear()
        dial.FaceColorRange.Add(pointCOFF)
        dial.FaceColorRange.Add(pointOTHER)
        dial.FaceColorRange.Add(pointCON)

        elhost.Child = dial
        Me.Controls.Add(elhost)
        Label1.Text = status

        Me.Refresh()
        ' DataGridView2.DataSource = clt.Run("http://10.0.0.147")
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
        'Dim drv As DataRowView
        'If e.RowIndex >= 0 Then
        '    If e.RowIndex <= DataGridView1.Rows.Count - 1 Then
        '        drv = table.DefaultView.Item(e.RowIndex)
        '        Dim c As Color
        '        If drv.Item("status").ToString = "Cycle On" Then
        '            c = Color.Green
        '        Else
        '            If drv.Item("status").ToString = "Cycle Off" Then
        '                c = Color.Red
        '            Else
        '                c = Color.Yellow
        '            End If
        '        End If
        '        e.CellStyle.BackColor = c
        '    End If
        'End If
    End Sub

 

#Region "Move the forme"
    '-----------------------------------------------------------------------------------------------------------------------
    ' MOVE THE FORM
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Dim _MouseDown As Boolean, _MouseX As Integer, _MouseY As Integer
    Private Sub LIVE_mouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        _MouseDown = True
        _MouseX = e.X
        _MouseY = e.Y

    End Sub

    Private Sub LIVE_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        _MouseDown = False
    End Sub

    Private Sub LIVE_Mousemove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove


        If _MouseDown = True Then
            Reporting_application.SuspendLayout()
            Me.Left = Me.Left + (e.X - _MouseX)
            Me.Top = Me.Top + (e.Y - _MouseY)
            If Me.Top < 20 Then Me.Top = 0
            If Me.Left < 20 Then Me.Left = 0
            Reporting_application.ResumeLayout(True)

        End If

    End Sub
#End Region




    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick

        '  table = clt.Run("http://10.0.10.61/")
        '   DataGridView1.DataSource = table
        '    DataGridView1.Refresh()

    End Sub


End Class

Public Class Functions

    Public xuser As String
    Public xpassword As String
    Public xproxy As String
    Public xport As String
    Public xproxypassword As String
    Public xproxyuser As String
    Public xphonenumber As String

    Public logincookies As CookieContainer

    Dim UA As String = "CSIFLEX Reporting Application"


    Public Function login(ByVal user As String, ByVal password As String, ByVal port As String, ByVal URL As String)
        xuser = user
        xpassword = password
        xport = port


        logincookies = New CookieContainer
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
        request.CookieContainer = New CookieContainer()
        request.CookieContainer = logincookies

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
        param2fin = param2.ToString
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
        ' MessageBox.Show(param1 & "--" & param2 & "--" & user & "--" & xport & "--" & xproxyuser & "--" & xproxypassword)


        'serialize the cookies to bin format and save them to the disk
        '----------------------------------

        ''set the name for the cookie file
        'Dim filename As String = "d:/" & "cookie.dat"
        ''delete the old file if it exists
        'If (File.Exists(filename)) Then
        '    File.Delete(filename)
        'End If
        ''serialize and write the file
        'Dim stream As FileStream = File.Create(filename)
        'Dim formatter As New BinaryFormatter()
        'formatter.Serialize(stream, response.Cookies)
        'stream.Close()


        'Return the functions values
        'Return loginpagecontent
        Return loginResponsePageContent
    End Function


End Class
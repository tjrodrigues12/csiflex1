'Globalvar
Imports CSI_Library

'file
Imports System.IO

'powerpack
'Imports Microsoft.VisualBasic.PowerPacks


'exemple url to load enet dashboard with auto refresh
'http://10.0.10.26:8080/?SEL=-1&Start=Start

Public Class DashWeb


    Private scrollTimer As New Timer()
    Private refreshTimer As New Timer()
    Private scrollJump As Integer = 1
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath


    Dim cellpadding As Integer = 5
    Dim nbcolumn As Integer = 7

    Dim colors As Dictionary(Of String, Integer)

    Dim scrollcnt As Integer = -50
    Dim firstload As Boolean = True

    Dim autoscrollsetting As Boolean = False
    Dim usecsidashboard As Boolean = False
    Dim lastscrollpos As Integer = 0

    Private Sub DashGrid_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'DGV_LiveStatus.AllowDrop = True
        If File.Exists(rootPath & "\sys\AutoScroll_.csys") Then
            Using reader As StreamReader = New StreamReader(rootPath & "\sys\AutoScroll_.csys")
                autoscrollsetting = Boolean.Parse(reader.ReadLine())
                reader.Close()
            End Using
        End If

        If File.Exists(rootPath & "\sys\UseCSIDashboard_.csys") Then
            Using reader As StreamReader = New StreamReader(rootPath & "\sys\UseCSIDashboard_.csys")
                usecsidashboard = Boolean.Parse(reader.ReadLine())
                reader.Close()
            End Using
        End If


        AddHandler scrollTimer.Tick, AddressOf scrollTimer_Tick
        AddHandler refreshTimer.Tick, AddressOf refreshTimer_Tick

        Me.SuspendLayout()
        Me.MdiParent = Reporting_application
        ' Me.Location = New System.Drawing.Point(285, 25)
        Me.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Size.Width, 25)
        'Resize
        Dim freespace = New Size(Reporting_application.Width - Config_report.Width + 80, Reporting_application.Height - Reporting_application.MenuStrip1.Height)
        Me.Size = freespace
        WB_EnetLive.Size = New Size(Me.Width, Me.Height)


        '>1 because the fct returns ip:port event if they are both empty
        If (GetEnetIp().Length > 1) Then
            'Prevent single machine load
            WB_EnetLive.Navigate("http://" & GetEnetIp() & "/?SEL=-1")
            If (autoscrollsetting) Then
                GetEnetWebPage()
            Else
                GetAutoRefreshEnetPage()
            End If
        Else
            MessageBox.Show("Please provide enet ip to view the live status")
        End If

        'scrollTimer.Start()

        scrollTimer.Interval = 50 '1000 * 5 '1000 * 60 * 5 ' refresh every 5 min
        refreshTimer.Interval = GlobalVariables.refresh_Interval
        'refreshTimer.Start()


    End Sub

    Private Sub DashGrid_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.ResumeLayout()
        'P_Dash.ResumeLayout()
    End Sub


    Private Sub scrollTimer_Tick(sender As Object, e As EventArgs)
        'GC.Collect()
        'GetData()
        scrollcnt = scrollcnt + scrollJump
        WB_EnetLive.ScrollBarsEnabled = True
        WB_EnetLive.Document.Window.ScrollTo(0, scrollcnt)

        '-50 to give a little time before scrolling back to top
        If (scrollcnt - 50 >= WB_EnetLive.Document.Body.ScrollRectangle.Height - WB_EnetLive.Document.Window.Size.Height) Then
            'refresh and reset count
            scrollcnt = -50
            scrollTimer.Stop()
            GetEnetWebPage()
        End If

    End Sub

    Private Sub refreshTimer_Tick(sender As Object, e As EventArgs)
        'GC.Collect()
        'GetData()
        'scrollcnt = scrollcnt + scrollJump
        'WB_EnetLive.ScrollBarsEnabled = True
        'WB_EnetLive.Document.Window.ScrollTo(0, lastscrollpos)

        '-50 to give a little time before scrolling back to top
        'If (scrollcnt - 50 >= WB_EnetLive.Document.Body.ScrollRectangle.Height - WB_EnetLive.Document.Window.Size.Height) Then
        'refresh and reset count
        'scrollcnt = -50
        refreshTimer.Stop()
        GetAutoRefreshEnetPage()
        'End If

    End Sub
    Private Sub GetEnetWebPage()
        Try
            firstload = True
            WB_EnetLive.Navigate("http://" & GetEnetIp() & "/?SEL=-1&Refresh=Refresh")
        Catch ex As Exception

        End Try
    End Sub


    Private Sub GetAutoRefreshEnetPage()
        Try
            firstload = True
            WB_EnetLive.Navigate("http://" & GetEnetIp() & "/?SEL=-1&Start=Start")
        Catch ex As Exception

        End Try

    End Sub
    Private Function GetEnetIp() As String
        Dim res As String
        res = ""

        If (usecsidashboard) Then
            If File.Exists(rootPath & "\sys\UseCSIDashboard_.csys") Then
                Try
                    Using r As StreamReader = New StreamReader(rootPath & "\sys\SrvDBpath.csys", False)
                        res = r.ReadLine.Trim & ":"
                        r.Close()
                    End Using
                    Using r1 As StreamReader = New StreamReader(rootPath & "\sys\RM_port_.csys", False)
                        res = res & r1.ReadLine.Trim
                        r1.Close()
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Unable to read the csi ip ") ' & ex.Message)
                End Try
            End If
        Else
            If File.Exists(rootPath & "\sys\Networkenet_.csys") Then
                Try
                    Using r As StreamReader = New StreamReader(rootPath & "\sys\Networkenet_.csys", False)
                        res = r.ReadLine.Trim
                        r.Close()
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Unable to read the enet ip ") ' & ex.Message)
                End Try
            End If
        End If

        

        Return res
    End Function
  
    Private Sub WB_EnetLive_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WB_EnetLive.DocumentCompleted
        'Dim doc As HtmlDocument = WB_EnetLive.Document
        'doc.Body.ScrollTop = Integer.MaxValue

        'AutoscrollPage()
        If Not usecsidashboard Then
            If (firstload And autoscrollsetting) Then
                Dim doctext As String
                doctext = WB_EnetLive.DocumentText
                doctext = RemoveEnetHeader(doctext)
                'WB_EnetLive.DocumentText = doctext
                WB_EnetLive.Document.Body.InnerHtml = doctext

                firstload = False

                If (autoscrollsetting) Then
                    scrollTimer.Start()
                End If

            Else


                If (firstload) Then
                    WB_EnetLive.Document.Window.ScrollTo(0, lastscrollpos)
                    WB_EnetLive.Document.Window.AttachEventHandler("onscroll", AddressOf OnScrollEventHandler)

                    Dim doctext As String
                    doctext = WB_EnetLive.DocumentText
                    doctext = RemoveEnetHeader(doctext)
                    'WB_EnetLive.DocumentText = doctext
                    WB_EnetLive.Document.Body.InnerHtml = doctext

                    firstload = False

                    refreshTimer.Start()
                End If
            End If
        End If
    End Sub


    Public Sub OnScrollEventHandler(sender As Object, e As EventArgs)
        lastscrollpos = WB_EnetLive.Document.Body.ScrollRectangle.Top
    End Sub

    Private Function RemoveEnetHeader(html As String) As String

        Dim reshtml As String = ""
        Try
            Dim startindex As Integer
            Dim divstring As String = "</th></tr></table>"
            startindex = html.IndexOf(divstring)
            reshtml = html.Substring(startindex + divstring.Length)
            reshtml = html.Substring(0, html.IndexOf("<table width=")) & reshtml
            If (Not autoscrollsetting) Then
                reshtml = reshtml.Remove(reshtml.IndexOf("<META http-equiv"), reshtml.IndexOf(">", reshtml.IndexOf("<META http-equiv")) - reshtml.IndexOf("<META http-equiv") + 1)
            End If
        Catch ex As Exception

        End Try
      
        Return reshtml
    End Function

    Private Sub DashWeb_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        scrollTimer.Stop()
        refreshTimer.Stop()
    End Sub
End Class
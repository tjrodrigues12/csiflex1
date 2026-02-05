Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text.RegularExpressions
Imports System.Threading
Imports CSI_Reporting_Application.InternalWebServerApplication

Public Class InternalWebServer
    Private Sub InternalWebServer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Here Load IP from srv_ip_.csys file and split the IP and fill nudIp1,p2,p3 and p4
        'Then select an available port start from 8000 (8000 is standard)
        'Then start the Server 
        Dim externalIP As String = Nothing
        externalIP = (New WebClient()).DownloadString("http://checkip.dyndns.org/")
        externalIP = (New Regex("\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(externalIP)(0).ToString()
        ipAddresses.Text = "Internal IP: " & Dns.GetHostByName(Dns.GetHostName()).AddressList(0).ToString() & (If(externalIP Is Nothing, "", ", External IP: " & externalIP))
        Dim IPSplit() As String = Dns.GetHostByName(Dns.GetHostName()).AddressList(0).ToString().Split(".")
        nudIP1.Value = IPSplit(0)
        nudIP2.Value = IPSplit(1)
        nudIP3.Value = IPSplit(2)
        nudIP4.Value = IPSplit(3)
    End Sub
    Dim server As Server = New Server()
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Dim ipAddress As IPAddress = IPAddress.Parse(nudIP1.Value.ToString() & "." + nudIP2.Value.ToString() & "." + nudIP3.Value.ToString() & "." + nudIP4.Value.ToString())

        If server.start(ipAddress, Convert.ToInt32(nudPort.Value), 100, txtContentPath.Text) Then
            btnStart.Enabled = False
            btnStop.Enabled = True
            btnTest.Enabled = True
            'Below Code to Auto Open in browser the web page on given 
            'System.Diagnostics.Process.Start("chrome.exe", "http://" & nudIP1.Value.ToString() & "." + nudIP2.Value.ToString() & "." + nudIP3.Value.ToString() & "." + nudIP4.Value.ToString() + (If(nudPort.Value = 80, "", ":" & nudPort.Value.ToString())))
        Else
            MessageBox.Show(Me, "Couldn't start the server. Make sure port " & nudPort.Value.ToString() & " is not being listened by other servers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End If
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        System.Diagnostics.Process.Start("chrome.exe", "http://" & nudIP1.Value.ToString() & "." + nudIP2.Value.ToString() & "." + nudIP3.Value.ToString() & "." + nudIP4.Value.ToString() + (If(nudPort.Value = 80, "", ":" & nudPort.Value.ToString())))
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Try
            server.stop()
            btnStart.Enabled = True
            btnStop.Enabled = False
            btnTest.Enabled = False
        Catch ex As Exception
            MessageBox.Show("Error While try to stop web server" & ex.Message())
        End Try
    End Sub
    Private Sub InternalWebServer_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.Closing
        btnStop.PerformClick()
        'Properties.Settings.[Default].Save()
    End Sub

    Private Sub trayIcon_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles trayIcon.MouseDoubleClick
        Show()
        trayIcon.Visible = False
        Me.WindowState = FormWindowState.Normal
    End Sub
    Private Sub InternalWebServer_SizeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.SizeChanged
        If Me.WindowState = FormWindowState.Minimized Then
            trayIcon.Visible = True
            Me.Hide()
        End If
    End Sub

    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        If folderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            txtContentPath.Text = folderBrowserDialog1.SelectedPath
        End If
    End Sub
    Private startedUp As Boolean = False
    ' Public Property Properties As Object

    'Private Sub Form1_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
    '    'autoStart.Checked = True
    '    If startedUp Then Return
    '    startedUp = True
    '    If autoStart.Checked Then
    '        Me.WindowState = FormWindowState.Minimized
    '        Me.Hide()
    '        btnStart_Click(Nothing, Nothing)
    '    End If
    '    ipAddresses.Text = Nothing
    '    Dim GETInternalIPs As Thread = New Thread(AddressOf HandlesExternalIP)
    'End Sub
    'Sub HandlesExternalIP()
    '    Try
    '        Dim externalIP As String = Nothing
    '        Try
    '            externalIP = (New WebClient()).DownloadString("http://checkip.dyndns.org/")
    '            externalIP = (New Regex("\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(externalIP)(0).ToString()
    '        Catch
    '        End Try
    '        ' Me.Invoke(CType(DNSName(), Action))
    '        DNSName(externalIP)
    '    Catch
    '    End Try
    'End Sub
    'Sub DNSName(extIP As String)
    '    ipAddresses.Text = "Internal IP: " & Dns.GetHostByName(Dns.GetHostName()).AddressList(0).ToString() & (If(extIP Is Nothing, "", ", External IP: " & extIP))
    'End Sub
End Class
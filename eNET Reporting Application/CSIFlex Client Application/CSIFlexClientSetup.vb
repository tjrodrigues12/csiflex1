Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports CSIFlex_Client_Application.InternalWebServer


Public Class CSIFlexClientSetup
    Public startaServer As New InternalWebServer.Server
    Public PortInt As Integer
    Private Sub CSIFlexClientSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IP_choice.ShowDialog()
        Dim IPAddress As String = CB_IPAddress.SelectedText
        If BTN_Start.Enabled Then
            LBL_ServiceResult.Text = "CSIFlex Client is Stopped"
            LBL_ServiceResult.BackColor = Color.Red
        Else
            LBL_ServiceResult.Text = "CSIFlex Client is Running"
            LBL_ServiceResult.BackColor = Color.YellowGreen
        End If
    End Sub
    Public Shared Function PortInUse(ByVal port As Integer) As Boolean
        Dim inUse As Boolean = False
        Dim ipProperties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
        Dim ipEndPoints As IPEndPoint() = ipProperties.GetActiveTcpListeners()

        For Each endPoint As IPEndPoint In ipEndPoints
            If endPoint.Port = port Then
                inUse = True
                Exit For
            End If
        Next
        Return inUse
    End Function
    Private Sub BTN_IPRefresh_Click(sender As Object, e As EventArgs) Handles BTN_IPRefresh.Click
        Dim IPv4Address As New List(Of String)
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)
        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                IPv4Address.Add(ipheal.ToString())
            End If
        Next
        CB_IPAddress.DataSource = IPv4Address
    End Sub
    Private Sub CSIFlexClientSetup_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If (MessageBox.Show("Do you want to close application?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
            'This means close the application
            e.Cancel = False
        Else
            'This hides the form in tray icon section
            trayIcon.Visible = True
            Me.Hide()
            MessageBox.Show("You can reopen this application from icon tray !")
            e.Cancel = True
        End If
    End Sub

    Private Sub BTN_ViewInBrowser_Click(sender As Object, e As EventArgs) Handles BTN_ViewInBrowser.Click
        System.Diagnostics.Process.Start("chrome.exe", "http://" & CB_IPAddress.Text.ToString() & ":" & numPort.Value.ToString())
    End Sub

    Private Sub BTN_Uninstall_Click(sender As Object, e As EventArgs) Handles BTN_Uninstall.Click
        Dim result As Integer = MessageBox.Show("Do you really want to unistall CSIFlex Client ?", "Uninstall Confirmation", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then
            'Do nothing 
        ElseIf result = DialogResult.Yes Then
            'Delete all related Files and Then Exit 
            Me.Hide()
            MessageBox.Show("CSIFlex Client unistalled successfully !")
            Me.Close()
            Environment.Exit(0)
        End If
    End Sub

    Private Sub BTN_Start_Click(sender As Object, e As EventArgs) Handles BTN_Start.Click
        PortInt = Convert.ToInt32(numPort.Value)
        While PortInUse(PortInt)
            PortInt = PortInt + 1
        End While
        numPort.Value = Convert.ToString(PortInt)
        Dim ipAddress As IPAddress = IPAddress.Parse(CB_IPAddress.Text)
        'Now we can use same project as Our Web Pages hub but when we make msi this path changed to CSIFlex Client in Program86 Folder
        Dim PathOfWebFiles = "C:\Users\BDesai\Desktop\Desktop\CSIFlex 1 Backward Code\CSIFlex1\eNET Reporting Application\CSIFlex Client Website\html webpages"
        If startaServer.start(ipAddress, Convert.ToInt32(numPort.Value), 100, PathOfWebFiles) Then
            BTN_Start.Enabled = False
            BTN_Stop.Enabled = True
            BTN_ViewInBrowser.Enabled = True
            'Below Code to Auto Open in browser the web page on given 
            System.Diagnostics.Process.Start("chrome.exe", "http://" & CB_IPAddress.Text.ToString() & ":" & numPort.Value.ToString())
        Else
            MessageBox.Show(Me, "Couldn't start the server. Make sure port " & numPort.Value.ToString() & " is not being listened by other servers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End If
        CB_IPAddress.Enabled = False
        BTN_IPRefresh.Enabled = False
        numPort.Enabled = False
        LBL_ServiceResult.Text = "CSIFlex Client is Running"
        LBL_ServiceResult.BackColor = Color.YellowGreen
    End Sub

    Private Sub BTN_Stop_Click(sender As Object, e As EventArgs) Handles BTN_Stop.Click
        Try
            startaServer.stop()
            BTN_Start.Enabled = True
            BTN_Stop.Enabled = False
            BTN_ViewInBrowser.Enabled = False
            CB_IPAddress.Enabled = True
            BTN_IPRefresh.Enabled = True
            numPort.Enabled = True
            LBL_ServiceResult.Text = "CSIFlex Client is Stopped"
            LBL_ServiceResult.BackColor = Color.Red
        Catch ex As Exception
            ' MessageBox.Show("Error While try to stop web server" & ex.Message())
        End Try
    End Sub
    Private Sub trayIcon_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles trayIcon.MouseDoubleClick
        Me.Show()
        trayIcon.Visible = False
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub CSIFlexClientSetup_SizeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.SizeChanged
        If Me.WindowState = FormWindowState.Minimized Then
            trayIcon.Visible = True
            Me.Hide()
        End If
    End Sub
End Class
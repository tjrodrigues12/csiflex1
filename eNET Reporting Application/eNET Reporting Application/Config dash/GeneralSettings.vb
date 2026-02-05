Imports System.IO
Imports MySql.Data.MySqlClient
Imports CSIFlexServerService
Imports System.Threading
Imports System.ServiceProcess
Imports CSIFLEX.Database.Access

Public Class GeneralSettings

    Public CSI_Lib As New CSI_Library.CSI_Library(True)
    Public ServiceLib As New CSIFlexServerService.ServiceLibrary
    Private rootpath As String = CSI_Library.CSI_Library.serverRootPath
    Public service As ServiceController = New ServiceController("CSIFlexServerService")
    Private load1 As Boolean = True

    Private Sub GeneralSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim line As String = ""
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        Timer1.Start()
        Dim setting As New Dictionary(Of String, String)

        Try

            Dim dTable_message15 As DataTable = MySqlAccess.GetDataTable($"SELECT trendspercent, trendcompare, ProdpercentOn from CSI_database.tbl_deviceConfig2 WHERE deviceId = { SetupForm2.deviceId }")

            If dTable_message15.Rows.Count > 0 Then

                TB_percent.Text = dTable_message15.Rows.Item(0).Item("trendspercent")

                If dTable_message15.Rows.Item(0).Item("trendcompare").ToString() = "shift" Then
                    CB_shiftcompare.SelectedIndex = 0 '"The actual shift" 'dTable_message15.Rows.Item(0).Item("trendcompare")
                Else
                    CB_shiftcompare.SelectedItem = dTable_message15.Rows.Item(0).Item("trendcompare")
                End If

                If dTable_message15.Rows.Item(0).Item("ProdpercentOn").ToString() = "Off" Then
                    RB_1.Checked = True
                    TB_percent.Enabled = True
                    CB_shiftcompare.Enabled = True
                    Label2.Enabled = True
                    Label1.Enabled = True
                Else
                    RB2.Checked = True
                    TB_percent.Enabled = False
                    CB_shiftcompare.Enabled = False
                    Label2.Enabled = False
                    Label1.Enabled = False
                End If
            End If

        Catch ex As Exception
            CSI_Lib.LogServerError("General Setting Loading Error : " & ex.Message, 1)
        End Try

        load1 = False

    End Sub


    Private Sub save(what As String, options As String)


        If what = "prodlist" Then

            If Not Directory.Exists(rootpath + "/sys/generaldashb/") Then Directory.CreateDirectory(rootpath + "/sys/generaldashb/")

            If File.Exists((rootpath + "/sys/generaldashb/" & options & ".csys")) Then File.Delete(rootpath + "/sys/generaldashb/" & options & ".csys")

            Using fs As New StreamWriter(rootpath + "/sys/generaldashb/" & options & ".csys")
                fs.Write("1")
                fs.Close()
            End Using
        ElseIf what = "trends" Then
            If Not Directory.Exists(rootpath + "/sys/generaldashb/") Then Directory.CreateDirectory(rootpath + "/sys/generaldashb/")

            If File.Exists((rootpath + "/sys/generaldashb/" & "trends" & ".csys")) Then File.Delete(rootpath + "/sys/generaldashb/" & "trends" & ".csys")

            Using fs As New StreamWriter(rootpath + "/sys/generaldashb/" & "trends" & ".csys")
                fs.Write(options)
                fs.Close()
            End Using

        End If

        SetupForm2.RefreshDevice(SetupForm2.IP_TB.Text)

    End Sub

    Private Sub TB_percent_TextChanged(sender As Object, e As EventArgs) Handles TB_percent.TextChanged

        If TB_percent.Text = String.Empty Then
            MessageBox.Show("Percentages should not be empty ")
        Else
            Try
                Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                cntsql.Open()
                Dim cmdsql2 As New MySqlCommand("update CSI_database.tbl_deviceConfig2 SET trendspercent = " & TB_percent.Text & " WHERE ip_adress = '" & SetupForm2.IP_TB.Text & "' and  name = '" & SetupForm2.txtDeviceName.Text & "'", cntsql)
                cmdsql2.ExecuteNonQuery()
                cntsql.Close()
            Catch ex As Exception
                MessageBox.Show("Error in Percentage of Trends :" & ex.Message)
            End Try

        End If
    End Sub

    Private Sub CB_shiftcompare_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_shiftcompare.SelectedIndexChanged
        Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        cntsql.Open()
        Dim cmdsql2 As New MySqlCommand("update CSI_database.tbl_deviceConfig2 SET trendcompare = '" & CB_shiftcompare.SelectedItem & "' WHERE ip_adress = '" & SetupForm2.IP_TB.Text & "' and  name = '" & SetupForm2.txtDeviceName.Text & "'", cntsql)
        cmdsql2.ExecuteNonQuery()
        cntsql.Close()
    End Sub

    Private Sub CurrentCycle_CB_CheckedChanged(sender As Object, e As EventArgs) Handles CurrentCycle_CB.CheckedChanged
        'save("prodlist", "CYCLE ON")
    End Sub

    Private Sub ElapsedTime_CB_CheckedChanged(sender As Object, e As EventArgs) Handles ElapsedTime_CB.CheckedChanged
        If load1 = False Then
            If ElapsedTime_CB.Checked = False Then
                If File.Exists((rootpath + " /sys/generaldashb/SETUP.csys")) Then File.Delete(rootpath + "/sys/generaldashb/SETUP.csys")
            Else
                save("prodlist", "SETUP")
            End If

            SetupForm2.RefreshDevice(SetupForm2.IP_TB.Text)
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CB_LOADING.CheckedChanged
        'save("prodlist", "LOADING")
        If load1 = False Then
            If CB_LOADING.Checked = False Then
                If File.Exists((rootpath + "/sys/generaldashb/LOADING.csys")) Then File.Delete(rootpath + "/sys/generaldashb/LOADING.csys")
            Else
                save("prodlist", "LOADING")
            End If

            SetupForm2.RefreshDevice(SetupForm2.IP_TB.Text)
        End If
    End Sub

    Private Sub RB2_CheckedChanged(sender As Object, e As EventArgs) Handles RB2.CheckedChanged, CB_shiftcompare.SelectedIndexChanged, TB_percent.TextChanged

        'New Logic 
        If RB2.Checked = True Then
            Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            cntsql.Open()
            Dim cmdsql2 As New MySqlCommand("update CSI_database.tbl_deviceConfig2 SET ProdpercentOn = 'On' WHERE ip_adress = '" & SetupForm2.IP_TB.Text & "' and  name = '" & SetupForm2.txtDeviceName.Text & "'", cntsql)
            cmdsql2.ExecuteNonQuery()
            cntsql.Close()
            CB_shiftcompare.Enabled = False
            TB_percent.Enabled = False
            Label2.Enabled = False
            Label1.Enabled = False
        Else
            CB_shiftcompare.Enabled = True
            TB_percent.Enabled = True
            Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            cntsql.Open()
            Dim cmdsql2 As New MySqlCommand("update CSI_database.tbl_deviceConfig2 SET ProdpercentOn = 'Off' WHERE ip_adress = '" & SetupForm2.IP_TB.Text & "' and  name = '" & SetupForm2.txtDeviceName.Text & "'", cntsql)
            cmdsql2.ExecuteNonQuery()
            cntsql.Close()
            Label2.Enabled = True
            Label1.Enabled = True
        End If
        SetupForm2.RefreshDevice(SetupForm2.IP_TB.Text)

    End Sub

    Private Sub BTN_DashboardReset_Click(sender As Object, e As EventArgs) Handles BTN_DashboardReset.Click

        Me.BTN_DashboardReset.Enabled = False
        KillingAProcess("CSIFlexServerService")
        ServiceLib.Function_To_LoadHistoryForToday()
        ServiceLib.RefreshAllDevices()

    End Sub

    Public Sub KillingAProcess(ProcessName As String)

        'Dim cmdcommand As String = "taskkill /IM " & System.Convert.ToChar(34).ToString & "" & ProcessName & "" & System.Convert.ToChar(34).ToString & " /F"
        Dim cmdcommand As String = "taskkill /FI " & System.Convert.ToChar(34).ToString & "SERVICES EQ " & ProcessName & System.Convert.ToChar(34).ToString & " /F"
        Dim info As New ProcessStartInfo()
        info.FileName = "cmd"
        info.Arguments = "cmd /c" & cmdcommand
        info.UseShellExecute = False
        info.CreateNoWindow = False
        info.WindowStyle = ProcessWindowStyle.Normal
        info.Verb = "runas"
        Dim process As New Process()
        process.StartInfo = info
        process.Start()
        process.WaitForExit(5000)

    End Sub

    Private Sub CheckForHistoryThread()
        'BTN_DashboardReset.Invoke(New delegate_CheckForHistoryThread(AddressOf CheckForHistoryThread))
        If IsNothing(CSIFlexServerService.ServiceLibrary.thread_Load_History_and_TodayData) = False Then
            If CSIFlexServerService.ServiceLibrary.thread_Load_History_and_TodayData.IsAlive Then
                Me.BTN_DashboardReset.Enabled = False
            Else
                Me.BTN_DashboardReset.Enabled = True
            End If
        End If
    End Sub

    Private Sub GeneralSettings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If IsNothing(CSIFlexServerService.ServiceLibrary.thread_Load_History_and_TodayData) = False Then
            If CSIFlexServerService.ServiceLibrary.thread_Load_History_and_TodayData.IsAlive Then
                MessageBox.Show("Please wait reset process is running now !")
                e.Cancel = True
            Else
                'StartService("CSIFlexServerService")
                ServiceTools.ServiceInstaller.StartService("CSIFlexServerService")
                Thread.Sleep(5000)
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        CheckForHistoryThread()
    End Sub

End Class
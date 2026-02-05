
Imports System.IO
Imports System.Net
Imports System.Threading
Imports CSIFLEX.Database.Access
Imports CSIFLEX.eNetLibrary
Imports CSIFLEX.Server.Settings
Imports MySql.Data.MySqlClient

Partial Public Class SetupForm2

    Dim isInstalledMBServer As Boolean = False

    Private Sub SetupFormGeneral_Load()

        Try
            txtGeneralEnetFolder.Text = ServerSettings.EnetFolder

            txtPort.Text = ServerSettings.RMPort
            ServerPort = txtPort.Text

            'LBL_IP.Text = ServerSettings.ServerIPAddress

            cmbStartWeek.SelectedIndex = ServerSettings.FirstDayOfWeek

            DailyTargets = CSI_Lib.Load_DailyTargets() 'Sets Daily Targets 
            WeeklyTargets = CSI_Lib.Load_WeeklyTargets() 'Sets Weekly Targets 
            MonthlyTargets = CSI_Lib.Load_MonthlyTargets() 'Sets Monthly Targets

            services = New Services()
            AddHandler services.StatusChanged, AddressOf ServiceStatusChanged

            MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.auto_report_config SET ReportTitle = '{CSIFLEXGlobal.CompanyId}' WHERE Task_name = 'SystemMonitoring'")

            Dim isRunReportService = services.IsServiceRunning("CSIFlex_Reports_Generator_Service")
            Dim startReportService = Boolean.Parse(MySqlAccess.ExecuteScalar("SELECT autoreport_status FROM csi_auth.tbl_autoreport_status LIMIT 1;").ToString())

            If Not isRunReportService = startReportService Then
                If startReportService Then
                    services.InstallAndStartService("CSIFlex_Reports_Generator_Service")
                Else
                    services.StopService("CSIFlex_Reports_Generator_Service")
                End If
            End If

            Dim isRunService = services.IsServiceRunning("CSIFlexServerService")
            If Not isRunService Then
                services.InstallAndStartService("CSIFlexServerService")
            End If

            isInstalledMBServer = services.IsServiceInstalled("CSIFLEX.WebApp.Service")

            btnStartMBServer.Visible = isInstalledMBServer
            btnStopMBServer.Visible = isInstalledMBServer
            lblMBServer.Text = IIf(isInstalledMBServer, "", "Not Available")


        Catch ex As Exception

            MessageBox.Show(ex.Message)

            Me.Close()

        End Try

    End Sub

    Private Sub btnBrowserEnetFolder_Click(sender As Object, e As EventArgs) Handles btnBrowserEnetFolder.Click

        Dim folderDlg As New FolderBrowserDialog

        folderDlg.Description = "Choose or Specify a folder for the database"
        Try
            folderDlg.ShowNewFolderButton = True
            If (folderDlg.ShowDialog() = DialogResult.OK) Then
                txtGeneralEnetFolder.Text = folderDlg.SelectedPath
                Dim root As Environment.SpecialFolder = folderDlg.RootFolder
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnDefaultEnetFolder_Click(sender As Object, e As EventArgs) Handles btnDefaultEnetFolder.Click

        txtGeneralEnetFolder.Text = "C:\_eNETDNC"

    End Sub

    Private Sub btnReloadEnetSettings_Click(sender As Object, e As EventArgs) Handles btnReloadEnetSettings.Click

        eNetServer.Instance.ReloadEnetServer()

        send_http_req("reloadMachines")

    End Sub

    Private Sub TB_port_TextChanged(sender As Object, e As EventArgs) Handles txtPort.TextChanged
        If Not ServerPort = txtPort.Text Then
            btnSaveWebServerPort.Enabled = True
        Else
            btnSaveWebServerPort.Enabled = False
        End If
    End Sub

    Private Sub btnSaveWebServerPort_Click(sender As Object, e As EventArgs) Handles btnSaveWebServerPort.Click

        Try
            If File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys") Then
                File.Delete(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
            End If

            Using writer As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                writer.Write(txtPort.Text)
                writer.Close()
            End Using

            Dim sqlCmd As New MySqlCommand()
            sqlCmd.CommandText = "SET SQL_SAFE_UPDATES=0;"
            sqlCmd.CommandText += "UPDATE CSI_database.tbl_RM_Port SET port = @port_;"
            sqlCmd.CommandText += "SET SQL_SAFE_UPDATES=1;"
            sqlCmd.Parameters.AddWithValue("@port_", CInt(txtPort.Text))

            If MySqlAccess.ExecuteNonQuery(sqlCmd) = 0 Then
                sqlCmd.CommandText = "INSERT INTO CSI_database.tbl_RM_Port (port) VALUES (@port_)"
                MySqlAccess.ExecuteNonQuery(sqlCmd)
            End If

            change_IP(True)

            Dim IP_0 As String = ""

            Try
                If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")) Then
                    Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")
                        IP_0 = reader.ReadLine
                        reader.Close()
                    End Using
                End If
            Catch ex As Exception
                CSI_Lib.LogServerError("At reading srvip  from csys: " & ex.Message, 1)
            End Try

            StartServiceOnAnotherPort(IP_0, txtPort.Text)
            ServerPort = txtPort.Text
            btnSaveWebServerPort.Enabled = False

        Catch ex As Exception
            MsgBox("Could not save the port and can't start the server : " & ex.Message)
        End Try

    End Sub

    Private Sub btnChangeIpAddress_Click(sender As Object, e As EventArgs) Handles btnChangeIpAddress.Click

        isComingFromChangeIP = True
        change_IP(False)

        Try
            If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")) Then

                Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")
                    LBL_IP.Text = reader.ReadLine
                    CSI_Library.CSI_Library.LocalHostIP = reader.ReadLine
                    reader.Close()
                End Using

            End If

        Catch ex As Exception
            CSI_Lib.LogServerError("At reading srvip  from csys: " & ex.Message, 1)
        End Try

    End Sub

    Private Sub btnCheckWebServer_Click(sender As Object, e As EventArgs) Handles btnCheckWebServer.Click

        LBL_srvResult.Text = "checking ..."
        LBL_srvResult.BackColor = Color.Yellow

        Try

            Dim portT = MySqlAccess.GetDataTable("Select port From csi_database.tbl_rm_port;")
            Dim request As WebRequest

            If portT.Rows.Count <> 0 Then
                If IsDBNull(portT.Rows(0)("port")) Then
                    request = WebRequest.Create("http://127.0.0.1:8008/readconfig")
                Else
                    request = WebRequest.Create("http://127.0.0.1:" & portT.Rows(0)("port") & "/hi")
                End If
            End If

            request.Method = "POST"

            Dim postData As String = ""
            Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)

            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)

            dataStream.Close()

            LBL_srvResult.Text = "The server is responding."
            LBL_srvResult.BackColor = Color.LimeGreen
            LBL_srvResult.Visible = True
            t3.Interval = 10000

            AddHandler t3.Tick, AddressOf HandleServerResponse
            t3.Start()

        Catch ex As Exception
            CSI_Lib.LogServerError("srv ping req fail :" + ex.Message, 1)
        End Try

    End Sub

    Private Sub btnConfigureReports_Click(sender As Object, e As EventArgs) Handles btnConfigureReports.Click
        If BgWorker_CreateDB.IsBusy Then
            MessageBox.Show("Machine data is being loaded into the database, Reports Configurator will be available soon after !")
        Else
            Dim ReportConfiguration As New AutoReporting() 'New Auto Reporting Form
            ReportConfiguration.StartPosition = FormStartPosition.CenterScreen
            ReportConfiguration.ShowDialog()
        End If
    End Sub

    Private Sub btnConfigureEmail_Click(sender As Object, e As EventArgs) Handles btnConfigureEmail.Click
        Dim emailfrm As New EmailServer
        emailfrm.ShowDialog()
    End Sub

    Private Sub btnConfigureReinmeter_Click(sender As Object, e As EventArgs) Handles btnConfigureReinmeter.Click
        Dim frm_RMConfig As New RMConfig
        frm_RMConfig.ShowDialog()
    End Sub

    Private Sub btnLoadingCycleOn_Click(sender As Object, e As EventArgs) Handles btnLoadingCycleOn.Click
        Dim frm_srvconfig As New ConfigureService
        frm_srvconfig.ShowDialog()
    End Sub

    Private Sub btnImportSettings_Click(sender As Object, e As EventArgs) Handles btnImportSettings.Click

        Dim frmRestoreSettings = New RestoreSettings()
        frmRestoreSettings.StartPosition = FormStartPosition.CenterParent
        frmRestoreSettings.ShowDialog()

        'importDB.ShowDialog()
    End Sub

    Private Sub btnPerformanceTuning_Click(sender As Object, e As EventArgs) Handles btnPerformanceTuning.Click
        Database_performance_tuning.ShowDialog()
    End Sub

    Private Sub btnPerformanceClear_Click(sender As Object, e As EventArgs) Handles btnPerformanceClear.Click

        Dim connectionString As String
        connectionString = CSI_Library.CSI_Library.MySqlConnectionString

        Dim mySqlConn As MySqlConnection = New MySqlConnection(connectionString)
        Dim mySqlComm As MySqlCommand

        Try

            mySqlConn.Open()
            mySqlComm = New MySqlCommand(" DROP DATABASE csi_machineperf", mySqlConn)
            mySqlComm.ExecuteNonQuery()

            mySqlConn.Close()
            MsgBox("Cleared")
        Catch ex As Exception
            CSI_Lib.LogServiceError("Err : " & ex.Message, 1)
            MsgBox(ex.Message)
        Finally
            mySqlConn.Close()
        End Try

    End Sub

    Private Sub cmbStartWeek_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStartWeek.SelectedIndexChanged

        If Not loadingFORM And cmbStartWeek.SelectedIndex >= 0 Then

            ServerSettings.FirstDayOfWeek = cmbStartWeek.SelectedIndex

        End If

    End Sub



    Public Sub btnStartService_Click(sender As Object, e As EventArgs) Handles btnStartServ.Click

        services.InstallAndStartService("CSIFlexServerService")

    End Sub

    Public Sub btnStopService_Click(sender As Object, e As EventArgs) Handles btnStopServ.Click
        'StopService()
        send_http_req("stopThreads")
        Thread.Sleep(3000)
        services.StopService("CSIFlexServerService")

    End Sub

    Public Sub btnStartRepService_Click(sender As Object, e As EventArgs) Handles btnStartRepService.Click

        services.InstallAndStartService("CSIFlex_Reports_Generator_Service")

        Try
            If MySqlAccess.ExecuteNonQuery($"SET SQL_SAFE_UPDATES = 0; UPDATE csi_auth.tbl_autoreport_status SET autoreport_status = 'True'") = 0 Then
                MySqlAccess.ExecuteNonQuery($"INSERT INTO csi_auth.tbl_autoreport_status (autoreport_status) VALUES ('True')")
            End If
        Catch ex As Exception
            MessageBox.Show("Error in Updating Auto Reporting to Database : " & ex.Message())
        End Try

    End Sub

    Public Sub btnStopRepService_Click(sender As Object, e As EventArgs) Handles btnStopRepService.Click

        services.StopService("CSIFlex_Reports_Generator_Service")

        Try
            If MySqlAccess.ExecuteNonQuery($"SET SQL_SAFE_UPDATES = 0; UPDATE csi_auth.tbl_autoreport_status SET autoreport_status = 'False'") = 0 Then
                MySqlAccess.ExecuteNonQuery($"INSERT INTO csi_auth.tbl_autoreport_status (autoreport_status) VALUES ('False')")
            End If
        Catch ex As Exception
            MessageBox.Show("Error in Updating Auto Reporting to Database : " & ex.Message())
        End Try

    End Sub

    Public Sub btnStartMonitoringService(sender As Object, e As EventArgs) Handles btnStartMBServer.Click
        services.InstallAndStartService("CSIFLEX.WebApp.Service")
    End Sub

    Public Sub btnStopMonitoringService(sender As Object, e As EventArgs) Handles btnStopMBServer.Click
        services.StopService("CSIFLEX.WebApp.Service")
    End Sub


    Public Sub ServiceStatusChanged(serviceName As String, newStatus As ServiceTools.ServiceState)

        Dim changeLabel As Label
        Dim btnStart As PictureBox
        Dim btnStop As PictureBox

        If serviceName = "CSIFlexServerService" Then
            changeLabel = lblServiceState
            btnStart = btnStartServ
            btnStop = btnStopServ
            lblStartService.Visible = False
        ElseIf serviceName = "CSIFlex_Reports_Generator_Service" Then
            changeLabel = lblReportServiceState
            btnStart = btnStartRepService
            btnStop = btnStopRepService
        ElseIf serviceName = "CSIFlexMBServer" And isInstalledMBServer Then
            changeLabel = lblMBServer
            btnStart = btnStartMBServer
            btnStop = btnStopMBServer
        ElseIf serviceName = "CSIFLEX.WebApp.Service" Then
            changeLabel = lblMBServer
            btnStart = btnStartMBServer
            btnStop = btnStopMBServer
        Else
            Return
        End If

        btnStart.Visible = False
        btnStop.Visible = False

        Select Case newStatus

            Case ServiceTools.ServiceState.NotFound
                changeLabel.Text = "Not Installed"
                changeLabel.BackColor = Color.LightGray

            Case ServiceTools.ServiceState.Run
                changeLabel.Text = "Running"
                changeLabel.BackColor = Color.LimeGreen
                btnStop.Visible = True

            Case ServiceTools.ServiceState.Starting
                changeLabel.Text = "Starting"
                changeLabel.BackColor = Color.Yellow

                If serviceName = "CSIFlexServerService" Then
                    lblStartService.Visible = True
                End If

            Case ServiceTools.ServiceState.Stop
                changeLabel.Text = "Stopped"
                changeLabel.BackColor = Color.Red
                btnStart.Visible = True

            Case ServiceTools.ServiceState.Stopping
                changeLabel.Text = "Stopping"
                changeLabel.BackColor = Color.Orange

            Case ServiceTools.ServiceState.Unknown
                changeLabel.Text = "Unknown"
                changeLabel.BackColor = Color.Red

            Case Else
                changeLabel.Text = "Unknown"
                changeLabel.BackColor = Color.Red

        End Select

    End Sub

    Public Sub HandleServerResponse()
        LBL_srvResult.Text = String.Empty
        LBL_srvResult.Visible = False
        t3.Dispose()
    End Sub

    Private Sub StartServiceOnAnotherPort(IP_ As String, PORT As String)

        'change ip and port on in the javascript file
        Dim endUserConfigUrl As String = """http://" & IP_ & ":" & PORT & "/enduserconfig"";"
        Dim newestMachinesRecordsUrl As String = """http://" & IP_ & ":" & PORT & "/refresh"";"
        Dim timelineUrl As String = """http://" & IP_ & ":" & PORT & "/timeline"";"
        Dim r As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global.js")
        Dim w As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global-.js")
        Dim line As String = ""

        While ((Not r.EndOfStream))
            line = r.ReadLine()
            If (line.StartsWith("var endUserConfigUrl =")) Then
                w.WriteLine("var endUserConfigUrl =" & endUserConfigUrl)

            ElseIf (line.StartsWith("var newestMachinesRecordsUrl =")) Then
                w.WriteLine("var newestMachinesRecordsUrl =" & newestMachinesRecordsUrl)

            ElseIf (line.StartsWith("var timelineUrl = ")) Then
                w.WriteLine("var timelineUrl = " & timelineUrl)

            Else
                w.WriteLine(line)
            End If

        End While
        r.Close()
        w.Close()
        File.Delete(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global.js")
        Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\Global-.js", "global.js")
        'Process to restart Service on another port 

        Dim serviceexepath As String = String.Empty
        serviceexepath = AppDomain.CurrentDomain.BaseDirectory + "CSIFlexServerServices.exe"

        If (ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlexServerService")) Then

            'If below code not working uncomment above two lines and comment below Logic upto Else 
            While ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Run Or ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Starting
                Do Until ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Stop
                    ServiceTools.ServiceInstaller.StopService("CSIFlexServerService")
                Loop
            End While

            Do Until ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Run ' Or ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Starting
                ServiceTools.ServiceInstaller.StartService("CSIFlexServerService")
                ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlexServerService")
            Loop

            'Update all Focas Machine Agent IPAddress to this Server Address
            Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            Dim NewUpdate As String = String.Empty
            Try
                cntsql.Open()
                NewUpdate = "SET SQL_SAFE_UPDATES=0;UPDATE `csi_auth`.`tbl_csiconnector` SET `AgentIP` = '" & IP_ & "' WHERE ConnectorType ='Focas';SET SQL_SAFE_UPDATES=1;"
                Dim cmdOtherUpdate As New MySqlCommand(NewUpdate, cntsql)
                Dim rdrOtherUpdate As MySqlDataReader = cmdOtherUpdate.ExecuteReader
                cntsql.Close()
            Catch ex As Exception
                MessageBox.Show("Agent IP Update Database Error : " & ex.Message())
            End Try

            LBL_IPChange.Text = "Port changed to : " & PORT
            LBL_IPChange.Visible = True
            LBL_IPChange.BackColor = Color.LimeGreen
            Me.Load_DGV_CSIConnector()
        Else
            'In this case Install the Service 
            ServiceTools.ServiceInstaller.InstallAndStart("CSIFlexServerService", "CSIFlexServerService", serviceexepath)
            ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlexServerService")
            Dim ServiceCont_CSIF As System.ServiceProcess.ServiceController
            ServiceCont_CSIF = New System.ServiceProcess.ServiceController("CSIFlexServerService")
        End If
        t4.Interval = 10000
        AddHandler t4.Tick, AddressOf Tickhandle
        t4.Start()

    End Sub

End Class

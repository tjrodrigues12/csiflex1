#Region "Imports stuff"

Imports System.ComponentModel
Imports System.IO
Imports System.IO.File
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows
Imports CSI_Library
Imports CSI_Library.Extentions
Imports CSIFLEX.Database.Access
Imports CSIFLEX.eNetLibrary
Imports CSIFLEX.eNetLibrary.Data
Imports CSIFLEX.License.Data
Imports CSIFLEX.Server.Library
Imports CSIFLEX.Server.Library.DataModel
Imports CSIFLEX.Server.Settings
Imports CSIFLEX.Utilities
Imports Encryption.Utilities
Imports FocasLibrary.Tools
Imports Microsoft.Win32
Imports MySql.Data.MySqlClient

#End Region
'-----------------------------------------------------------------------------------------------------------------------
'Form 2 : Configuration
'-----------------------------------------------------------------------------------------------------------------------


<System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Unrestricted:=True)>
Partial Public Class SetupForm

#Region "Declarations"

    Private trd As Thread
    Public IPAddressNew As String = String.Empty
    Public isComingFromChangeIP As Boolean = False
    Public firstservicestart As Boolean = True
    Public WanttoRunService As Boolean = True
    Public servicestate As Boolean = False
    Public isrestarting As Boolean = False
    Public ServerPort As String = String.Empty
    Public CSI_Lib As New CSI_Library.CSI_Library(True)
    Public CSIFlexServiceLib As New CSIFlexServerService.ServiceLibrary
    Public clt As New CSI_Library.EnetClient
    Public Shared Thread1 As Thread
    Public thread_CheckServiceStatus As Thread
    Public Shared thread_AutoLoadMachinePerf_thread As Thread
    Public Shared isCalledByMTConnect As Boolean = False
    Public chemin_eNET As String
    Public machine_list As New Dictionary(Of String, String)
    Public lastIp As String
    Public lastIp2 As String
    Public isTrytoRestarting As Boolean = False
    Public lastDevice As String
    Public lastMessage As String
    Public tbmess As Boolean
    Public isUnistalling As Boolean = False
    Public tbip As Boolean
    Public dbUpdate_needed As Boolean = False
    Public dbConnectStr As String
    Public tmp_txtbox1 As String
    Public tmp_txtbox2 As String
    Public targetColor_ As String
    Public identified As String
    Public loaded As Boolean = False
    Public userschanged As Boolean = False
    Public notfirstchangeusers As Boolean = False
    Public list2 As New List(Of String)
    Public configisloading As Boolean = False
    Public fiiliing As Boolean = False ' =true whene user select a row , =false after tree3 been filled
    Public loadingFORM As Boolean = False
    Public userloaded As Boolean = False
    Public tabColor As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Public LastRowData As New Dictionary(Of String, MachinePerfData) ' Table Name, and list of column data 
    Public CSIFService As New CSIFlexServerService.ServiceLibrary
    Public Comput_perf_required As Boolean = False
    Private queryDevice As String = "SELECT * FROM CSI_database.tbl_devices"
    Public Shared thread_Load_History_and_TodayData As Thread
    Private queryMessage As String = "SELECT * FROM CSI_database.tbl_messages"
    Public AllMachines As New Dictionary(Of String, MachineData) 'Monioring id , and list of machine elements 

    Public DisplayMachineList As New Dictionary(Of String, String) 'machinename , rename_machinename
    Public ActiveMachines As New Dictionary(Of String, String) 'machinename,  rename_machinename but only valid machines
    Private bSource As BindingSource
    Public EnetLiveStatusThread As New CSIFlexServerService.ServiceLibrary
    Public copy As String = ""
    Public first As Boolean = True
    Public DeviceType As String = String.Empty

    Public usermachines As String = ""

    Public Shared listOfSourceToAdd As List(Of sourceToAdd)
    Public path As String = CSI_Library.CSI_Library.serverRootPath

    Public t1 As New System.Windows.Forms.Timer
    Public t5 As New System.Windows.Forms.Timer
    Public TimerForService As New System.Windows.Forms.Timer
    Public Count As Integer = 0
    Public ServiceStarted As Boolean = True
    Public t2 As New System.Windows.Forms.Timer
    Public t3 As New System.Windows.Forms.Timer
    Public t4 As New System.Windows.Forms.Timer
    Public timerForShowHide As New System.Windows.Forms.Timer
    Public objAdapterInfo As New FocasLibrary.Components.AdapterInfo()
    Public objMainWindow As New FocasLibrary.MainWindow()

    Private dashboardDevice As DashboardDevice
    Private myUserProfile As UserProfile
    Public deviceId As Integer
    Private services As Services

    Dim timelineEditControl As TimelineEditControl

#End Region

#Region "SetUpForm_Load"

    Private Sub SetupForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CSI_Lib = New CSI_Library.CSI_Library(True)
        CSI_Library.CSI_Library.isServer = True 'Indicated that the this is a server application(not Client)
        listOfSourceToAdd = New List(Of sourceToAdd)
        lastIp = ""
        lastIp2 = ""
        lastDevice = ""
        lastMessage = ""
        loadingFORM = True

        Log.Debug("Start")

        'Dim tab As TabPage = TabControl_DashBoard.TabPages("TP_Timeline")
        'TabControl_DashBoard.TabPages.Remove(tab)

        Try
            ServerSettings.Instance.Init(IO.Path.Combine(CSI_Library.CSI_Library.serverRootPath, "sys"))

            TB_eNETfolder.Text = ServerSettings.EnetFolder

            TB_port.Text = ServerSettings.RMPort
            ServerPort = TB_port.Text

            LBL_IP.Text = ServerSettings.ServerIPAddress

            'eNetServer.Instance.Init(TB_eNETfolder.Text)

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

            Call GetAllENETMachines()
            Call LoadGridviewMachines()
            Call Load_DGV_CSIConnector()

            LoadEnetGroups()

            myUserProfile = New UserProfile(CSIFLEXGlobal.UserName)

            grvConnectors.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            load_colors()

            loadEnetPwd()

            eNETMachineStatus.LoadMachinesStatus()

            Dim checkfortempthread As New Thread(AddressOf checkfortemp)
            checkfortempthread.Name = "tempchecking"
            checkfortempthread.Start()
            checkfortempthread.Priority = ThreadPriority.Normal

            loadingFORM = False

            Dim OnlyIP As String = LBL_IP.Text
            IP_ = OnlyIP.Trim().ToString()

            dashboardDevice = New DashboardDevice()
            dashboardDevice.LoadDeviceByName("Local Host")
            If Not dashboardDevice.DeviceExists And IP_ <> "000.000.000.000" Then
                dashboardDevice.DeviceName = "Local Host"
                dashboardDevice.IpAddress = IP_
                dashboardDevice.Machines = "ALL"
                dashboardDevice.SaveDevice()
            End If

            move_htm_files()

            Dim endUserConfigUrl As String = """http://" & IP_ & ":" & TB_port.Text & "/enduserconfig"";"
            Dim newestMachinesRecordsUrl As String = """http://" & IP_ & ":" & TB_port.Text & "/refresh"";"
            Dim timelineUrl As String = """http://" & IP_ & ":" & TB_port.Text & "/timeline"";"

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
            FileIO.FileSystem.RenameFile(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\Global-.js", "global.js")

        Catch ex As Exception
            loadingFORM = False
            MessageBox.Show(ex.Message)
            Me.Close()
        End Try

        Load_rm_port()

        btnSaveWebServerPort.Enabled = False

        If Not IPAddressNew = String.Empty Then
            LBL_IP.Text = IPAddressNew
        End If

    End Sub

    Public Sub ServiceStatusChanged(serviceName As String, newStatus As ServiceTools.ServiceState)

        Dim changeLabel As Label
        Dim btnStart As PictureBox
        Dim btnStop As PictureBox

        If serviceName = "CSIFlexServerService" Then
            changeLabel = lblServiceState
            btnStart = btnStartServ
            btnStop = btnStopServ
        ElseIf serviceName = "CSIFlex_Reports_Generator_Service" Then
            changeLabel = lblReportServiceState
            btnStart = btnStartRepService
            btnStop = btnStopRepService
        Else
            Return
        End If

        btnStart.Visible = False
        btnStop.Visible = False

        Select Case newStatus

            Case ServiceTools.ServiceState.NotFound
                changeLabel.Text = "Not Installed"
                changeLabel.BackColor = Color.Red

            Case ServiceTools.ServiceState.Run
                changeLabel.Text = "Running"
                changeLabel.BackColor = Color.LimeGreen
                btnStop.Visible = True

            Case ServiceTools.ServiceState.Starting
                changeLabel.Text = "Starting"
                changeLabel.BackColor = Color.Yellow

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

    Public Sub StartMobileServer()
        Try
            Dim PhpServerPath = "C:\ProgramData\CSIFlexMobileServer\CSIFlexPHPServer.exe"
            If (ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlexPHPServer")) Then
                If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexPHPServer") = ServiceTools.ServiceState.Run) Or
                    (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexPHPServer") = ServiceTools.ServiceState.Starting) Then
                Else
                    ServiceTools.ServiceInstaller.StartService("CSIFlexPHPServer")
                    ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlexPHPServer")
                End If
            Else
                ServiceTools.ServiceInstaller.InstallAndStart("CSIFlexPHPServer", "CSIFlexPHPServer", PhpServerPath)
                ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlexPHPServer")
                Dim ServiceCont_CSIF As System.ServiceProcess.ServiceController
                ServiceCont_CSIF = New System.ServiceProcess.ServiceController("CSIFlexPHPServer")
            End If

        Catch ex As Exception
            CSI_Lib.LogServerError("Error in StartMobileServer() :" & ex.Message() & "Stak Trace : " & ex.StackTrace(), 1)
        End Try
    End Sub

    Public Sub LoadHistoryData()
        Try
            Dim years_(-1) As String

            If (File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\years_.csys")) Then
                Using streader As New StreamReader(CSI_Library.CSI_Library.serverRootPath + "\sys\years_.csys")
                    Dim tmp_str As String = streader.ReadLine()
                    If tmp_str IsNot Nothing Then years_ = tmp_str.Split(",")
                End Using
            End If
            If years_ Is Nothing Or years_.Length = 0 Then
                CSI_Lib.Log_server_event("Years_ (in setupform) is nothing, FirstupdateDB executed")
                CSI_Lib.FirstUpdateDB_Mysql(Now.Year)
            Else
                For Each year As String In years_
                    CSI_Lib.Log_server_event("FirstupdateDB executed for year : " & year)
                    If year <> "" Then CSI_Lib.FirstUpdateDB_Mysql(year)
                Next
            End If
            'CSI_Lib.FirstUpdateDB_Mysql(Now.Year)
        Catch ex As Exception
            CSI_Lib.LogServerError(ex.Message, 1)
        End Try
        thread_AutoLoadMachinePerf() 'Function that loads todays machine status history from .MON(_MONITORING Folder) and .SYS_ (_TMP Folder)
    End Sub

    Public Sub thread_AutoLoadMachinePerf()

        GetAllENETMachines()

        Dim Today = DateTime.Now()
        Dim SearchMonthFolder = Today.ToString("yyyy-MM")
        Dim SearchDate = Today.ToString("MMMdd")
        Dim allconst As New AllStringConstants.StringConstant
        Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)

        cntsql.Open()

        Try
            AllMachines.Clear()

            'Read all data from EhubConf File where monitoring is ON
            Dim dtEhub As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf WHERE Monstate='1';")

            If dtEhub.Rows.Count > 0 Then
                For Each row As DataRow In dtEhub.Rows

                    If Not AllMachines.ContainsKey(row("monitoring_id").ToString()) Then
                        AllMachines.Add(row("monitoring_id"), New MachineData)
                    End If

                    AllMachines.Item(row("monitoring_id").ToString()).Machinename_ = row("machine_name").ToString()
                    AllMachines.Item(row("monitoring_id").ToString()).MonitoringFileName_ = row("monitoring_filename").ToString()
                    AllMachines.Item(row("monitoring_id").ToString()).MonitoringState_ = Integer.Parse(row("Monstate"))
                    AllMachines.Item(row("monitoring_id").ToString()).MonitoringID_ = row("monitoring_id").ToString()
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("MysqlConnectionstring Error : " & ex.Message())
        End Try

        Dim displaylistpath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Server\sys\machine_list_.csys"

        If File.Exists(displaylistpath) Then

            Dim fileData() As String = File.ReadAllLines(displaylistpath) 'Read All Lines from machine_list_.csys file

            For Each line1 As String In fileData
                If Not line1 = String.Empty Then

                    Dim machinename As String = line1.Split(",")(0)

                    If Not DisplayMachineList.ContainsKey(machinename) Then
                        DisplayMachineList.Add(machinename, String.Empty)
                    End If

                    DisplayMachineList.Item(machinename) = RenameMachine(machinename)
                End If
            Next
            'Dim listsplit As String() = fileData
        End If

        ' If Directrory Exists 
        Dim folderPath As String = allconst.SERVER_ENET_PATH & allconst.MONITORING_FOLDER_PATH & SearchMonthFolder

        If Directory.Exists(folderPath) Then

            MySqlAccess.Validate_MachinePerf_Database()

            'Delete All the Tables From Database 
            MySqlAccess.Delete_All_MachinePerf_Tables()

            'Create Machine Perf Table 
            'MySqlAccess.Validate_MachinePerf_Perf_Table()


            'Folder with YEAR-CURRENTMONTH exist
            'Now check if Today's File exists file Format is :: MMMdd_MachineName_SHIFT1.MON
            For Each kvp As KeyValuePair(Of String, MachineData) In AllMachines

                'Create Tables Fro Machine
                For Each kvp2 As KeyValuePair(Of String, String) In DisplayMachineList

                    Dim match As Boolean = True

                    If kvp.Value.Machinename_ = kvp2.Key Then 'When machine display list and All Machine name matches only create those tables and ignore others 
                        MySqlAccess.Validate_Perf_Machine_Table(kvp2.Key)
                        Exit For
                    End If
                Next
            Next

            Dim Monnewdate As New DateTime
            Dim newDate As New DateTime
            Dim Monlaststatustimestamp As New DateTime
            Dim laststatustimestamp As New DateTime

            For Each keyvalAllMachine As KeyValuePair(Of String, MachineData) In AllMachines

                If DisplayMachineList.ContainsKey(keyvalAllMachine.Value.Machinename_) Then
                    Dim files As String() = IO.Directory.GetFiles(folderPath, SearchDate & "_" & keyvalAllMachine.Value.Machinename_ & "*")
                    Dim count As Integer = files.Count ' no of files 
                    Dim tempshift As Integer = 0

                    If count = 1 Then
                        'tempshift = 2
                        Dim onefile As String = files(0)
                        Dim splitMonfileForShift As Char = onefile(onefile.Length - 5)

                        tempshift = Convert.ToInt32(splitMonfileForShift.ToString())
                        'One Monfile Curret Shift is 2 

                        For Each monfiles As String In files

                            If File.Exists(monfiles) Then

                                Dim MonFileData As String() = File.ReadAllLines(monfiles)

                                For Each Monlines As String In MonFileData

                                    If Monlines.Contains("_DPRINT_") Or Monlines.Contains("_SH_START") Or Monlines.Contains("_SH_END") Or Monlines.Contains("_SH_") Or (Monlines = String.Empty) Or (Monlines = "") Then 'Or lines1.Contains("NO eMONITOR")
                                        'If above patterns found don't add them to the database table
                                    Else
                                        'Add these entries to database table 
                                        If Monlines.Contains(",") Then 'This handles Empty String So we don't have any , in a string

                                            Dim MonSplitLines As String() = Monlines.Split(",")
                                            Dim Monjoindate As String = MonSplitLines(0) & " " & MonSplitLines(1)
                                            Dim Mononlytime As String() = (Convert.ToDateTime(MonSplitLines(1)).ToString("HH:mm:ss")).Split(":")

                                            '"10/23/19,06:00:01"

                                            Monnewdate = DateTime.ParseExact(Monjoindate, "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)

                                            Dim Monlatestdate = Monnewdate.ToString("yyyy-MM-dd HH:mm:ss")
                                            Dim MonStatus As String = MonSplitLines(2)
                                            Dim part As String = ""
                                            Dim oper As String = ""

                                            If MonStatus = "_CON" Then
                                                MonStatus = "CYCLE ON"
                                            ElseIf MonStatus = "_COFF" Then
                                                MonStatus = "CYCLE OFF"
                                            ElseIf MonStatus = "_SETUP" Then
                                                MonStatus = "SETUP"
                                            ElseIf MonStatus.StartsWith("_PART") Then
                                                part = MonStatus.Split(":")(1)
                                                MonStatus = MonStatus.Split(":")(0)
                                            ElseIf MonStatus.StartsWith("_OPER") Then
                                                oper = MonStatus.Split(":")(1)
                                                MonStatus = MonStatus.Split(":")(0)
                                            End If

                                            Dim MonTIME_s As Integer

                                            MonTIME_s = Convert.ToInt32(Mononlytime(0)) * 3600 + Convert.ToInt32(Mononlytime(1)) * 60 + Convert.ToInt32(Mononlytime(2))

                                            Dim dTable_SelectRowsMon As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) };")

                                            If dTable_SelectRowsMon.Rows.Count = 0 Then

                                                Dim Montimediff = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())
                                                Dim cmdMon As MySqlCommand = New MySqlCommand($"insert ignore into CSI_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } (status, time, cycletime, shift, date, partnumber, operator) VALUES ('{ MonStatus }', { (MonTIME_s) }, '{ Montimediff }', '{ tempshift }', '{ Monlatestdate }', '{ part }', '{ oper }')", cntsql)

                                                cmdMon.ExecuteNonQuery()
                                                Monlaststatustimestamp = Monnewdate
                                                laststatustimestamp = Monnewdate

                                            ElseIf dTable_SelectRowsMon.Rows.Count > 0 Then

                                                Dim Monlastcycletime = DateDiff(DateInterval.Second, Monlaststatustimestamp, Monnewdate)
                                                Dim mysqlUpdateMon As String = "SET @select := (SELECT date FROM csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " order by date desc limit 1);SET SQL_SAFE_UPDATES = 0;update  csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " SET `cycletime` = '" & Monlastcycletime & "' Where date =@select;"
                                                Dim cmdmysqlUpdateMon As New MySqlCommand(mysqlUpdateMon, cntsql)

                                                cmdmysqlUpdateMon.ExecuteNonQuery()

                                                Dim Montimediff1 = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())
                                                Dim cmd As MySqlCommand = New MySqlCommand($"insert ignore into CSI_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } (status, time, cycletime, shift, date, partnumber, operator) VALUES ('{ MonStatus }', { (MonTIME_s) }, '{ Montimediff1 }', '{ tempshift }', '{ Monlatestdate }', '{ part }', '{ oper }')", cntsql)

                                                cmd.ExecuteNonQuery()
                                                Monlaststatustimestamp = Monnewdate
                                                laststatustimestamp = Monnewdate
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        Next
                    ElseIf count = 2 Then

                        'Two Monfiles Curret Shift is 3
                        Dim fileno As Integer = 0

                        For Each monfiles As String In files

                            Dim splitMonfileForShift As Char = monfiles(monfiles.Length - 5)

                            tempshift = Convert.ToInt32(splitMonfileForShift.ToString())

                            If File.Exists(monfiles) Then

                                Dim MonFileData As String() = File.ReadAllLines(monfiles)

                                For Each Monlines As String In MonFileData

                                    If Monlines.Contains("_DPRINT_") Or Monlines.Contains("_SH_START") Or Monlines.Contains("_SH_END") Or Monlines.Contains("_SH_") Or (Monlines = String.Empty) Or (Monlines = "") Then
                                        'If above patterns found don't add them to the database table
                                    Else
                                        'Add these entries to database tables
                                        If Monlines.Contains(",") Then 'This if handles if the string we have is empty 

                                            Dim MonSplitLines As String() = Monlines.Split(",")
                                            Dim Monjoindate As String = $"{ MonSplitLines(0) } { MonSplitLines(1) }"
                                            Dim Mononlytime As String() = (Convert.ToDateTime(MonSplitLines(1)).ToString("HH:mm:ss")).Split(":")

                                            Monnewdate = DateTime.ParseExact(Monjoindate, "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                                            'Monnewdate = Convert.ToDateTime(Monjoindate)

                                            Dim Monlatestdate = Monnewdate.ToString("yyyy-MM-dd HH:mm:ss")
                                            Dim MonStatus As String = MonSplitLines(2)
                                            Dim part As String = ""
                                            Dim oper As String = ""

                                            If MonStatus = "_CON" Then
                                                MonStatus = "CYCLE ON"
                                            ElseIf MonStatus = "_COFF" Then
                                                MonStatus = "CYCLE OFF"
                                            ElseIf MonStatus = "_SETUP" Then
                                                MonStatus = "SETUP"
                                            ElseIf MonStatus.StartsWith("_PART") Then
                                                part = MonStatus.Split(":")(1)
                                                MonStatus = MonStatus.Split(":")(0)
                                            ElseIf MonStatus.StartsWith("_OPER") Then
                                                oper = MonStatus.Split(":")(1)
                                                MonStatus = MonStatus.Split(":")(0)
                                            End If

                                            Dim MonTIME_s As Integer

                                            MonTIME_s = Convert.ToInt32(Mononlytime(0)) * 3600 + Convert.ToInt32(Mononlytime(1)) * 60 + Convert.ToInt32(Mononlytime(2))

                                            Dim dTable_SelectRowsMon As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) };")

                                            If dTable_SelectRowsMon.Rows.Count = 0 Then

                                                Dim Montimediff = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())

                                                MySqlAccess.ExecuteNonQuery($"INSERT IGNORE INTO CSI_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } (status, time, cycletime, shift, date, partnumber, operator) VALUES ('{ MonStatus }', { (MonTIME_s) }, '{ Montimediff }', '{ tempshift }', '{ Monlatestdate }', '{ part }', '{ oper }')")

                                                Monlaststatustimestamp = Monnewdate
                                                laststatustimestamp = Monnewdate

                                            ElseIf dTable_SelectRowsMon.Rows.Count > 0 Then

                                                Dim Monlastcycletime = DateDiff(DateInterval.Second, Monlaststatustimestamp, Monnewdate)

                                                Dim sqlCmd As Text.StringBuilder = New Text.StringBuilder()
                                                sqlCmd.Append($"SET @select := (SELECT date FROM csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } order by date desc limit 1);")
                                                sqlCmd.Append($"SET SQL_SAFE_UPDATES = 0;")
                                                sqlCmd.Append($"update csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } SET `cycletime` = '{ Monlastcycletime }' Where date = @select;")

                                                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                                'Update the last line for cycletime and insert current like now() - dateTimestamp
                                                Dim Montimediff1 = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())

                                                MySqlAccess.ExecuteNonQuery($"INSERT IGNORE INTO CSI_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } (status, time, cycletime, shift, date, partnumber, operator ) VALUES ('{ MonStatus }', { (MonTIME_s) }, '{ Montimediff1 }', '{ tempshift }', '{ Monlatestdate }', '{ part }', '{ oper }')")

                                                Monlaststatustimestamp = Monnewdate
                                                laststatustimestamp = Monnewdate
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                            fileno = 1
                        Next
                    End If

                    'Code Below load Current SHIFT to MachinePerf Database from TMP Folder
                    Dim filePath As String = allconst.SERVER_ENET_PATH & allconst.TMP_FOLDER_PATH

                    If Directory.Exists(filePath) Then

                        Dim tmpfiles As String() = IO.Directory.GetFiles(filePath, keyvalAllMachine.Value.MonitoringFileName_)

                        If tmpfiles.Length > 0 Then

                            If File.Exists(tmpfiles(0)) Then

                                If File.GetLastWriteTime(tmpfiles(0)).Date.ToString("dd-MM-yyyy") = DateTime.Now.Date.ToString("dd-MM-yyyy") Then

                                    Dim fileData1() As String = File.ReadAllLines(tmpfiles(0))

                                    For Each lines1 As String In fileData1
                                        If lines1.Contains("_DPRINT_") Or (lines1 = String.Empty) Or (lines1 = "") Then 'Or lines1.Contains("NO eMONITOR")
                                            'IF Line Contains PARTNo, DPRINT,OPerator or Empty then don't add that line to database
                                        Else
                                            'Add this line to database table
                                            If lines1.Contains(",") Then 'This if handles if the string we have is empty 

                                                Dim Splitlines1 As String() = lines1.Split(",")
                                                Dim joindate As String = Splitlines1(0) & " " & Splitlines1(1)
                                                Dim onlytime As String() = (Convert.ToDateTime(Splitlines1(1)).ToString("HH:mm:ss")).Split(":")

                                                'newDate = Convert.ToDateTime(joindate)
                                                newDate = DateTime.ParseExact(joindate, "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)

                                                Dim latestdate = newDate.ToString("yyyy-MM-dd HH:mm:ss")
                                                Dim Status As String = Splitlines1(2)
                                                Dim part As String = ""
                                                Dim oper As String = ""

                                                If Status = "_CON" Then
                                                    Status = "CYCLE ON"
                                                ElseIf Status = "_COFF" Then
                                                    Status = "CYCLE OFF"
                                                ElseIf Status = "_SETUP" Then
                                                    Status = "SETUP"
                                                ElseIf Status.StartsWith("_PART") Then
                                                    part = Status.Split(":")(1)
                                                    Status = Status.Split(":")(0)
                                                ElseIf Status.StartsWith("_OPER") Then
                                                    oper = Status.Split(":")(1)
                                                    Status = Status.Split(":")(0)
                                                End If

                                                Dim TIME_s As Integer = Convert.ToInt32(onlytime(0)) * 3600 + Convert.ToInt32(onlytime(1)) * 60 + Convert.ToInt32(onlytime(2))

                                                'Select table and check for no of rows
                                                Dim dTable_SelectRows As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) };")

                                                If dTable_SelectRows.Rows.Count = 0 Then

                                                    Dim timediff = DateDiff(DateInterval.Second, newDate, DateTime.Now())

                                                    MySqlAccess.ExecuteNonQuery($"INSERT IGNORE INTO CSI_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } (status, time, cycletime, shift, date, partnumber, operator) VALUES ('{ Status }', { (TIME_s) }, '{ timediff }', '{ (tempshift + 1) }', '{ latestdate }', '{ part }', '{ oper }')")

                                                    laststatustimestamp = newDate

                                                ElseIf dTable_SelectRows.Rows.Count > 0 Then

                                                    Dim lastcycletime = DateDiff(DateInterval.Second, laststatustimestamp, newDate)

                                                    Dim sqlCmd As Text.StringBuilder = New Text.StringBuilder()
                                                    sqlCmd.Append($"SET @select := (SELECT date FROM csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } order by date desc limit 1);")
                                                    sqlCmd.Append($"SET SQL_SAFE_UPDATES = 0;")
                                                    sqlCmd.Append($"update csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } SET `cycletime` = '{ lastcycletime }' Where date = @select;")

                                                    MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                                    'Update the last line for cycletime and insert current like now() - dateTimestamp
                                                    Dim timediff1 = 0
                                                    timediff1 = DateDiff(DateInterval.Second, newDate, DateTime.Now())

                                                    MySqlAccess.ExecuteNonQuery($"INSERT IGNORE INTO CSI_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } (status, time, cycletime, shift, date, partnumber, operator ) VALUES ('{ Status }', { (TIME_s) }, '{ timediff1 }', '{ (tempshift + 1) }', '{ latestdate }', '{ part }', '{ oper }')")

                                                    laststatustimestamp = newDate
                                                End If
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If
                    'Delete last row of table andadd last row from fct_enet_livestatus() thread 
                End If
            Next
        Else
            'MessageBox.Show("Folder : " & SearchMonthFolder & " not exists")
        End If


        RefreshAllDevices()
    End Sub

    Public Function RenameMachine(machine As String) As String
        Dim res As String = machine

        For i = 32 To 47
            res = res.Replace(Chr(i), "_c" & i & "_")
        Next

        For i = 58 To 64
            res = res.Replace(Chr(i), "_c" & i & "_")
        Next

        For i = 91 To 96
            If i <> 95 Then
                res = res.Replace(Chr(i), "_c" & i & "_")
            End If
        Next

        For i = 123 To 126
            res = res.Replace(Chr(i), "_c" & i & "_")
        Next

        Return res
    End Function

    Private Sub LoadEnetGroups()

        Dim eNetPath = CSI_Lib.eNET_path() & "\_SETUP\MonList.sys"
        Dim groupName = ""
        Dim sqlCmd = New Text.StringBuilder()
        Dim line As String

        Using fs As New StreamReader(File.OpenRead(eNetPath))

            sqlCmd.Append($"TRUNCATE TABLE csi_database.tbl_EnetGroups; ")

            While Not (fs.EndOfStream)

                line = fs.ReadLine()

                If line.StartsWith("_MT_") Or line.StartsWith("_ST_") Then
                    groupName = line
                Else
                    If line.Trim().Length > 0 Then
                        sqlCmd.Append($"INSERT INTO                   ")
                        sqlCmd.Append($"  csi_database.tbl_EnetGroups ")
                        sqlCmd.Append($"  (                           ")
                        sqlCmd.Append($"     groupName,               ")
                        sqlCmd.Append($"     machine                  ")
                        sqlCmd.Append($"  )                           ")
                        sqlCmd.Append($"  VALUES                      ")
                        sqlCmd.Append($"  (                           ")
                        sqlCmd.Append($"     '{groupName}',           ")
                        sqlCmd.Append($"     '{line}'                 ")
                        sqlCmd.Append($"  );                          ")
                    End If
                End If
            End While

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())
        End Using
    End Sub

#End Region


#Region "General Tab - Controls"

    Private Sub btnBrowserEnetFolder_Click(sender As Object, e As EventArgs) Handles btnBrowserEnetFolder.Click

        Dim folderDlg As New FolderBrowserDialog

        folderDlg.Description = "Choose or Specify a folder for the database"
        Try
            folderDlg.ShowNewFolderButton = True
            If (folderDlg.ShowDialog() = DialogResult.OK) Then
                TB_eNETfolder.Text = folderDlg.SelectedPath
                Dim root As Environment.SpecialFolder = folderDlg.RootFolder
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub btnDefaultEnetFolder_Click(sender As Object, e As EventArgs) Handles btnDefaultEnetFolder.Click

        TB_eNETfolder.Text = "C:\_eNETDNC"

    End Sub


    Private Sub TB_port_TextChanged(sender As Object, e As EventArgs) Handles TB_port.TextChanged
        If Not ServerPort = TB_port.Text Then
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
                writer.Write(TB_port.Text)
                writer.Close()
            End Using

            Dim sqlCmd As New MySqlCommand()
            sqlCmd.CommandText = "SET SQL_SAFE_UPDATES=0;"
            sqlCmd.CommandText += "UPDATE CSI_database.tbl_RM_Port SET port = @port_;"
            sqlCmd.CommandText += "SET SQL_SAFE_UPDATES=1;"
            sqlCmd.Parameters.AddWithValue("@port_", CInt(TB_port.Text))

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

            StartServiceOnAnotherPort(IP_0, TB_port.Text)
            ServerPort = TB_port.Text
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
            ReportConfiguration.ShowDialog()
        End If
    End Sub


    Private Sub btnConfigureEmail_Click(sender As Object, e As EventArgs) Handles btnConfigureEmail.Click
        Dim emailfrm As New EmailServer
        emailfrm.ShowDialog()
    End Sub


    Public Sub btnStartService_Click(sender As Object, e As EventArgs) Handles btnStartServ.Click

        services.InstallAndStartService("CSIFlexServerService")

    End Sub


    Public Sub btnStartRepService_Click(sender As Object, e As EventArgs) Handles btnStartRepService.Click

        services.InstallAndStartService("CSIFlex_Reports_Generator_Service")

        Try
            If MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.tbl_autoreport_status SET autoreport_status = 'True'") = 0 Then
                MySqlAccess.ExecuteNonQuery($"INSERT INTO csi_auth.tbl_autoreport_status (autoreport_status) VALUES ('True')")
            End If
        Catch ex As Exception
            MessageBox.Show("Error in Updating Auto Reporting to Database : " & ex.Message())
        End Try

    End Sub


    Public Sub btnStopService_Click(sender As Object, e As EventArgs) Handles btnStopServ.Click
        'StopService()
        send_http_req("stopThreads")
        Thread.Sleep(3000)
        services.StopService("CSIFlexServerService")

    End Sub


    Public Sub btnStopRepService_Click(sender As Object, e As EventArgs) Handles btnStopRepService.Click

        services.StopService("CSIFlex_Reports_Generator_Service")

        Try
            If MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.tbl_autoreport_status SET autoreport_status = 'False'") = 0 Then
                MySqlAccess.ExecuteNonQuery($"INSERT INTO csi_auth.tbl_autoreport_status (autoreport_status) VALUES ('False')")
            End If
        Catch ex As Exception
            MessageBox.Show("Error in Updating Auto Reporting to Database : " & ex.Message())
        End Try

    End Sub


    'Public Sub btnStartMonitoringService(sender As Object, e As EventArgs)
    '    services.InstallAndStartService("CSIFlexMBServer")
    'End Sub


    'Public Sub btnStopMonitoringService(sender As Object, e As EventArgs)
    '    services.StopService("CSIFlexMBServer")
    'End Sub


    Private Sub btnConfigureReinmeter_Click(sender As Object, e As EventArgs) Handles btnConfigureReinmeter.Click
        Dim frm_RMConfig As New RMConfig
        frm_RMConfig.ShowDialog()
    End Sub


    Private Sub btnLoadingCycleOn_Click(sender As Object, e As EventArgs) Handles btnLoadingCycleOn.Click
        Dim frm_srvconfig As New ConfigureService
        frm_srvconfig.ShowDialog()
    End Sub


    Private Sub btnImportSettings_Click(sender As Object, e As EventArgs) Handles btnImportSettings.Click
        importDB.ShowDialog()
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

    'Private Sub timerServiceState_Tick(sender As Object, e As EventArgs) Handles timerServiceState.Tick
    '    Try
    '        Dim servicestatus As String = ""
    '        servicestatus = ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService")

    '        btnStartServ.Visible = False
    '        btnStopServ.Visible = False

    '        Select Case servicestatus

    '            Case ServiceTools.ServiceState.NotFound
    '                lblServiceState.Text = "Not Installed"
    '                lblServiceState.BackColor = Color.Red

    '            Case ServiceTools.ServiceState.Run
    '                lblServiceState.Text = "Running"
    '                lblServiceState.BackColor = Color.LimeGreen
    '                btnStopServ.Visible = True

    '            Case ServiceTools.ServiceState.Starting
    '                lblServiceState.Text = "Starting"
    '                lblServiceState.BackColor = Color.Yellow

    '            Case ServiceTools.ServiceState.Stop
    '                lblServiceState.Text = "Stopped"
    '                lblServiceState.BackColor = Color.Red
    '                btnStartServ.Visible = True

    '            Case ServiceTools.ServiceState.Stopping
    '                lblServiceState.Text = "Stopping"
    '                lblServiceState.BackColor = Color.Orange

    '            Case ServiceTools.ServiceState.Unknown
    '                lblServiceState.Text = "Unknown"
    '                lblServiceState.BackColor = Color.Red

    '            Case Else
    '                lblServiceState.Text = "Unknown"
    '                lblServiceState.BackColor = Color.Red

    '        End Select

    '    Catch ex As Exception
    '        CSI_Lib.LogServiceError("Unable to report service state:" + ex.Message, 1)
    '    End Try
    'End Sub

#End Region

    '======================================================================================================================================================================================================

#Region "General Tab - Users"

    Dim loadingUser As Boolean = True

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click

        clearuser()

        GB_Users.Visible = True
        treeviewMachines.Visible = True
        treeviewMachines.ExpandAll()

        Users_TV.Nodes(0).Nodes.Add("New User", "New User")
        Users_TV.SelectedNode = Users_TV.Nodes(0).Nodes(Users_TV.Nodes(0).Nodes.Count - 1)

        cmbUserType.Enabled = True

        txtUserName.Enabled = True
        txtUserName.Select()

        txtPassword.Enabled = True
        PictureBox5.Visible = True

        PictureBox5.Visible = True
        isNewUser = True

    End Sub

    Private Sub Users_TV_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles Users_TV.AfterSelect

        If (Users_TV.SelectedNode.Text = Users_TV.Nodes(0).Text) Then
            GB_Users.Visible = False
            treeviewMachines.Visible = False
        Else
            clearuser()
            loaduser()
            'loadmachine()
            GB_Users.Visible = True
            treeviewMachines.ExpandAll()
            treeviewMachines.Visible = True
            btnSaveUser.Enabled = False
        End If

    End Sub

    Private Sub CB_Type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUserType.SelectedIndexChanged

        enableORnotSave()

        RemoveHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

        If Not loadingUser And treeviewMachines.Nodes.Count > 0 Then 'This checks if All Machines and Group Nodes exists in treeView6

            Dim checkAll As Boolean = (cmbUserType.SelectedItem = "Admin")

            For Each n As TreeNode In treeviewMachines.Nodes
                n.Checked = checkAll

                If n.Nodes.Count > 0 Then
                    For Each n1 As TreeNode In n.Nodes
                        n1.Checked = checkAll
                        If n1.Nodes.Count > 0 Then
                            For Each n2 As TreeNode In n1.Nodes
                                n2.Checked = checkAll
                            Next
                        End If
                    Next
                End If
            Next
        End If

        AddHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

    End Sub


    Private Sub ChangeAllNode(nodeBase As TreeNode, checked As Boolean)

        nodeBase.Checked = checked

        For Each node As TreeNode In nodeBase.Nodes

            If node.Nodes.Count > 0 Then
                ChangeAllNode(node, checked)
            Else
                node.Checked = checked
            End If
        Next

    End Sub

    Private Function ChangeMachineNode(nodeBase As TreeNode, machine As String, checked As Boolean) As Boolean

        Dim grpChecked As Boolean = True

        For Each node As TreeNode In nodeBase.Nodes

            If node.Nodes.Count > 0 Then
                If Not ChangeMachineNode(node, machine, checked) Then grpChecked = False
            Else
                If node.Text = machine Then
                    node.Checked = checked
                End If

                If Not node.Checked Then grpChecked = False
            End If
        Next

        nodeBase.Checked = grpChecked

        Return grpChecked

    End Function

    Private Sub ChangeGroupNode(nodeBase As TreeNode, checked As Boolean)

        nodeBase.Checked = checked

        For Each node As TreeNode In nodeBase.Nodes
            If node.Nodes.Count > 0 Then
                ChangeGroupNode(node, checked)
            Else
                ChangeMachineNode(treeviewMachines.Nodes(0), node.Text, checked)
                If treeviewMachines.Nodes.Count > 1 Then
                    ChangeMachineNode(treeviewMachines.Nodes(1), node.Text, checked)
                End If
            End If
        Next

    End Sub


    Private Sub treeviewMachines_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles treeviewMachines.AfterCheck

        Dim added_machine As New List(Of String)

        Dim nodeChecked As TreeNode = e.Node
        Dim nodeName As String = e.Node.Text
        Dim isNodeChecked As Boolean = e.Node.Checked
        Dim isAllMachines As Boolean = (nodeName = "All Machines")
        Dim isNodeGroup As Boolean = nodeChecked.Nodes.Count > 0
        Dim machineList As String = ""

        enableORnotSave()

        RemoveHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

        If cmbUserType.SelectedItem = "Machine" And e.Node.Checked And Not usermachines = "" Then

            e.Node.Checked = False

            AddHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck
            Return
        End If


        If isNodeGroup Then
            If nodeName = "All Machines" Then
                For Each node As TreeNode In treeviewMachines.Nodes
                    ChangeAllNode(node, isNodeChecked)
                Next
            Else
                ChangeGroupNode(nodeChecked, isNodeChecked)
            End If
        Else
            For Each node As TreeNode In treeviewMachines.Nodes
                ChangeMachineNode(node, nodeName, isNodeChecked)
            Next
        End If

        For Each node As TreeNode In treeviewMachines.Nodes(0).Nodes
            If node.Checked Then
                machineList &= $",{node.Tag}"
            End If
        Next

        If Not String.IsNullOrEmpty(machineList) Then machineList = machineList.Substring(1)



        'If configisloading = False Then

        '    If nodeName = "All Machines" Then

        '        If cmbUserType.SelectedItem = "Machine" Then
        '            isNodeChecked = False
        '        End If

        '        If isNodeChecked = False Then

        '            For Each n As TreeNode In treeviewMachines.Nodes            '(0).Nodes
        '                n.Checked = False
        '            Next

        '            usermachines = ""

        '        End If

        '    End If

        'End If

        'Call CheckAllChildNodes(e.Node)

        'For Each node As TreeNode In treeviewMachines.Nodes
        '    CheckAllNodes(node, e.Node)
        'Next

        'If e.Node.Checked Then
        '    If e.Node.Parent Is Nothing = False Then
        '        Dim allChecked As Boolean = True

        '        Call IsEveryChildChecked(e.Node.Parent, allChecked)

        '        If allChecked Then
        '            e.Node.Parent.Checked = True
        '            Call ShouldParentsBeChecked(e.Node.Parent)
        '        End If
        '    End If
        'Else
        '    Dim parentNode As TreeNode = e.Node.Parent
        '    While parentNode Is Nothing = False
        '        parentNode.Checked = False
        '        parentNode = parentNode.Parent
        '    End While
        'End If

        'Partially Selected Checkbox Logic is here 
        Dim SelectedMachineCount As Integer
        Dim NoofGrps As Integer
        Dim AllMachineCount As Integer
        Dim totalgrp As Integer
        Dim groups_ As String = ""

        totalgrp = 0
        SelectedMachineCount = 0
        NoofGrps = 0
        AllMachineCount = 0

        'All Machines
        For Each SNode As TreeNode In treeviewMachines.Nodes(0).Nodes
            If SNode.Checked Then
                SelectedMachineCount += 1
            End If
            AllMachineCount += 1
        Next

        If SelectedMachineCount < AllMachineCount And SelectedMachineCount > 0 Then
            treeviewMachines.Nodes(0).ForeColor = Color.DarkGray
            treeviewMachines.Nodes(0).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
            treeviewMachines.Nodes(0).Text += String.Empty
        ElseIf SelectedMachineCount = AllMachineCount And SelectedMachineCount > 0 Then
            groups_ += treeviewMachines.Nodes(0).Text & ", "
            treeviewMachines.Nodes(0).ForeColor = Color.Black
            treeviewMachines.Nodes(0).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
            treeviewMachines.Nodes(0).Text += String.Empty
        Else
            treeviewMachines.Nodes(0).ForeColor = Color.Black
            treeviewMachines.Nodes(0).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Regular)
        End If

        'Group of Machines
        NoofGrps = treeviewMachines.Nodes(1).Nodes.Count
        For Each TotalGroups As TreeNode In treeviewMachines.Nodes(1).Nodes
            If TotalGroups.Checked Then
                totalgrp += 1
            End If
        Next

        If totalgrp < NoofGrps And totalgrp > 0 Then
            treeviewMachines.Nodes(1).ForeColor = Color.DarkGray
            treeviewMachines.Nodes(1).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
            treeviewMachines.Nodes(1).Text += String.Empty
        ElseIf totalgrp = NoofGrps And totalgrp > 0 Then
            treeviewMachines.Nodes(1).ForeColor = Color.Black
            treeviewMachines.Nodes(1).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
            treeviewMachines.Nodes(1).Text += String.Empty
        Else
            treeviewMachines.Nodes(1).ForeColor = Color.Black
            treeviewMachines.Nodes(1).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Regular)
        End If

        'Particular Group Machines
        Dim Groups(NoofGrps - 1) As Integer

        For grp As Integer = 0 To (Groups.Length - 1)
            Dim SelectedGroupCount As Integer
            SelectedGroupCount = 0
            Groups.SetValue(treeviewMachines.Nodes(1).Nodes(grp).Nodes.Count, grp)
            For Each GNode As TreeNode In treeviewMachines.Nodes(1).Nodes(grp).Nodes
                If GNode.Checked Then
                    SelectedGroupCount += 1
                End If
            Next
            If SelectedGroupCount < Groups(grp) And SelectedGroupCount > 0 Then
                treeviewMachines.Nodes(1).Nodes(grp).Checked = False
                treeviewMachines.Nodes(1).Nodes(grp).ForeColor = Color.DarkGray
                treeviewMachines.Nodes(1).Nodes(grp).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
                treeviewMachines.Nodes(1).Nodes(grp).Text += String.Empty
                treeviewMachines.Nodes(1).ForeColor = Color.DarkGray
                treeviewMachines.Nodes(1).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
                treeviewMachines.Nodes(1).Text += String.Empty
            ElseIf SelectedGroupCount = Groups(grp) And SelectedGroupCount > 0 Then
                treeviewMachines.Nodes(1).Nodes(grp).Checked = True
                treeviewMachines.Nodes(1).Nodes(grp).ForeColor = Color.Black
                treeviewMachines.Nodes(1).Nodes(grp).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
                treeviewMachines.Nodes(1).Nodes(grp).Text += String.Empty
                groups_ += treeviewMachines.Nodes(1).Nodes(grp).Text & ","
            Else
                treeviewMachines.Nodes(1).Nodes(grp).Checked = False
                treeviewMachines.Nodes(1).Nodes(grp).ForeColor = Color.Black
                treeviewMachines.Nodes(1).Nodes(grp).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Regular)
            End If
        Next

        groups_ = groups_.TrimEnd(",")

        ''Dim value_ As String = ""
        ''Dim machines_readed As List(Of String) = readTreeRecurciveDevice(treeviewMachines)
        ''Dim groups_readed As List(Of String) = readTreeRecurcivegroup(treeviewMachines)

        ''If Not IsNothing(machines_readed) Then
        ''    For Each machine As String In machines_readed
        ''        If value_ = "" Then
        ''            value_ = machine
        ''            added_machine.Add(machine)
        ''        Else
        ''            If Not added_machine.Contains(machine) Then
        ''                value_ = value_ & ", " & machine
        ''                added_machine.Add(machine)
        ''            End If
        ''        End If
        ''    Next
        ''    If Not IsNothing(groups_readed) Then
        ''        For Each machine As String In groups_readed
        ''            If value_ = "" Then
        ''                value_ = machine
        ''                added_machine.Add(machine)
        ''            Else
        ''                If Not added_machine.Contains(machine) Then
        ''                    value_ = value_ & ", " & machine
        ''                    added_machine.Add(machine)
        ''                End If
        ''            End If
        ''        Next
        ''    End If

        ''    If configisloading = False Then

        ''        usermachines = value_

        ''    End If
        ''End If

        usermachines = machineList

        AddHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

    End Sub

    Private Sub TreeView1_NodeMouseClick_1(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Users_TV.NodeMouseClick

        If e.Button = Windows.Forms.MouseButtons.Right Then

            Try
                If e.Node.Text <> Users_TV.SelectedNode.Text Then
                    Users_TV.SelectedNode = e.Node
                End If

                ' Point where the mouse is clicked.
                Dim p As New Drawing.Point(e.X, e.Y)

                ' Get the node that the user has clicked.
                Dim node As TreeNode = Users_TV.GetNodeAt(p)
                If node IsNot Nothing Then

                    ' Select the node the user has clicked.
                    ' The node appears selected until the menu is displayed on the screen.
                    Dim m_OldSelectNode = treeviewDevices.SelectedNode
                    treeviewDevices.SelectedNode = node

                    ' Find the appropriate ContextMenu depending on the selected node.
                    'Dim nodename As String = Convert.ToString(node.Name)

                    If (node.Level = 0) Then
                        Dim cms = New ContextMenuStrip
                        Dim item1 = cms.Items.Add("Add User")
                        item1.Tag = 1
                        AddHandler item1.Click, AddressOf menuChoice2
                        cms.Show(Users_TV, p)
                    ElseIf (node.Level = 2 And Not Users_TV.SelectedNode.Text = "Admin") Then
                        Dim cms = New ContextMenuStrip
                        Dim item2 = cms.Items.Add("Delete User")
                        item2.Tag = 2
                        AddHandler item2.Click, AddressOf menuChoice2
                        cms.Show(Users_TV, p)
                    End If

                End If
            Catch ex As Exception
                MessageBox.Show("Error while adding user, see log")
                CSI_Lib.LogServerError("Error adding user:" + ex.Message, 1)
            End Try
        End If
    End Sub

    Private Sub loaduser()

        Dim userProfile = New UserProfile(Users_TV.SelectedNode.Name)

        loadingUser = True

        clearuser()

        txtUserName.Enabled = False
        txtPassword.Enabled = False
        PictureBox5.Visible = False
        cmbUserType.Enabled = True
        btnSaveUser.Enabled = True
        btnEditPassword.Enabled = True

        If Not userProfile.UserName = "" Then

            txtUserName.Text = userProfile.UserName
            txtLastName.Text = userProfile.LastName
            txtFirstName.Text = userProfile.FirstName
            txtDisplayName.Text = userProfile.DisplayName
            txtUserEmail.Text = userProfile.Email
            txtUserRefID.Text = userProfile.RefId
            txtUserTitle.Text = userProfile.Title
            txtUserDept.Text = userProfile.Dept
            txtUserExtention.Text = userProfile.PhoneExt
            cmbUserType.SelectedIndex = cmbUserType.FindStringExact(userProfile.UserType.FirstUpCase())
            'chkEditTimeline.Checked = userProfile.EditTimeline

            If userProfile.UserName.FirstUpCase() = "Admin" Then
                cmbUserType.Enabled = False
            End If

            isNewUser = False
        ElseIf Users_TV.SelectedNode.Name = "" Then

            btnSaveUser.Enabled = False
            isNewUser = False
        Else

            txtUserName.Enabled = True
            txtUserName.Text = ""
            txtPassword.Enabled = True
            PictureBox5.Visible = True

            isNewUser = True
        End If

        If txtUserName.Text = "Mobile" Then
            txtUserName.Enabled = False
            cmbUserType.Enabled = False
            btnEditPassword.Enabled = False
        End If

        loadingUser = False

        loadmachine()

    End Sub

    Private Sub btnSaveUser_Click(sender As Object, e As EventArgs) Handles btnSaveUser.Click

        If isNewUser Then
            Dim userExists As Boolean = MySqlAccess.ExecuteScalar($"SELECT EXISTS(SELECT username_ FROM csi_auth.users WHERE username_ = '{txtUserName.Text}')")

            If userExists Then
                MessageBox.Show("User Name already exists. Choose another user name.")
                Return
            End If
        End If

        If txtPassword.Enabled And String.IsNullOrEmpty(txtPassword.Text) Then
            MessageBox.Show("Password field is required.")
            Return
        End If

        Dim userType As String = cmbUserType.SelectedItem.ToString()

        If txtUserName.Text = "Mobile" Then
            userType = "Admin"
            txtPassword.Text = ";p4csiflex"
        End If

        Dim sqlCmd As New Text.StringBuilder()

        sqlCmd.Append($"INSERT INTO csi_auth.users ")
        sqlCmd.Append($" (                         ")
        sqlCmd.Append($"    username_   ,          ")
        sqlCmd.Append($"    Name_       ,          ")
        sqlCmd.Append($"    firstname_  ,          ")
        sqlCmd.Append($"    displayname ,          ")
        sqlCmd.Append($"    email_      ,          ")
        sqlCmd.Append($"    usertype    ,          ")
        sqlCmd.Append($"    machines    ,          ")
        sqlCmd.Append($"    refId       ,          ")
        sqlCmd.Append($"    title       ,          ")
        sqlCmd.Append($"    dept        ,          ")
        sqlCmd.Append($"    phoneext    ,          ")
        sqlCmd.Append($"    EditTimeline           ")
        sqlCmd.Append($" )                         ")
        sqlCmd.Append($" VALUES                    ")
        sqlCmd.Append($" (                         ")
        sqlCmd.Append($"    @username_   ,         ")
        sqlCmd.Append($"    @Name_       ,         ")
        sqlCmd.Append($"    @firstname_  ,         ")
        sqlCmd.Append($"    @displayname ,         ")
        sqlCmd.Append($"    @email_      ,         ")
        sqlCmd.Append($"    @usertype    ,         ")
        sqlCmd.Append($"    @machines    ,         ")
        sqlCmd.Append($"    @refId       ,         ")
        sqlCmd.Append($"    @title       ,         ")
        sqlCmd.Append($"    @dept        ,         ")
        sqlCmd.Append($"    @phoneext    ,         ")
        sqlCmd.Append($"    @EditTimeline          ")
        sqlCmd.Append($" )                         ")
        sqlCmd.Append($" ON DUPLICATE KEY UPDATE          ")
        sqlCmd.Append($"    Name_        = @Name_       , ")
        sqlCmd.Append($"    firstname_   = @firstname_  , ")
        sqlCmd.Append($"    displayname  = @displayname , ")
        sqlCmd.Append($"    email_       = @email_      , ")
        sqlCmd.Append($"    usertype     = @usertype    , ")
        sqlCmd.Append($"    machines     = @machines    , ")
        sqlCmd.Append($"    refId        = @refId       , ")
        sqlCmd.Append($"    title        = @title       , ")
        sqlCmd.Append($"    dept         = @dept        , ")
        sqlCmd.Append($"    phoneext     = @phoneext    , ")
        sqlCmd.Append($"    EditTimeline = @Edittimeline  ")

        Dim sqlCommand As New MySqlCommand(sqlCmd.ToString())
        sqlCommand.Parameters.AddWithValue("@username_", txtUserName.Text)
        sqlCommand.Parameters.AddWithValue("@Name_", txtLastName.Text)
        sqlCommand.Parameters.AddWithValue("@firstname_", txtFirstName.Text)
        sqlCommand.Parameters.AddWithValue("@displayname", IIf(String.IsNullOrEmpty(txtDisplayName.Text), $"{txtLastName.Text}, {txtFirstName.Text}", txtDisplayName.Text))
        sqlCommand.Parameters.AddWithValue("@email_", txtUserEmail.Text)
        sqlCommand.Parameters.AddWithValue("@usertype", userType)
        sqlCommand.Parameters.AddWithValue("@machines", usermachines)
        sqlCommand.Parameters.AddWithValue("@refId", txtUserRefID.Text)
        sqlCommand.Parameters.AddWithValue("@title", txtUserTitle.Text)
        sqlCommand.Parameters.AddWithValue("@dept", txtUserDept.Text)
        sqlCommand.Parameters.AddWithValue("@phoneext", txtUserExtention.Text)
        sqlCommand.Parameters.AddWithValue("@EditTimeline", chkEditTimeline.Checked)

        MySqlAccess.ExecuteNonQuery(sqlCommand)

        If txtPassword.Enabled Then

            Dim derivedKey = HashHelper.CreatePBKDF2Hash(txtPassword.Text) 'AES_Encrypt(TB_Pass.Text, "pass").ToString()

            sqlCmd.Clear()
            sqlCmd.Append($"UPDATE csi_auth.users                                        ")
            sqlCmd.Append($" SET                                                         ")
            sqlCmd.Append($"    password_ = '{Convert.ToBase64String(derivedKey.Hash)}', ")
            sqlCmd.Append($"    salt_     = '{Convert.ToBase64String(derivedKey.Salt)}'  ")
            sqlCmd.Append($" WHERE                                                       ")
            sqlCmd.Append($"    username_ = @username_                                   ")

            sqlCommand = New MySqlCommand(sqlCmd.ToString())
            sqlCommand.Parameters.AddWithValue("@username_", txtUserName.Text)

            MySqlAccess.ExecuteNonQuery(sqlCommand)
        End If


        If Users_TV.SelectedNode IsNot Nothing Then

            Dim selectedNode As TreeNode = Users_TV.SelectedNode
            selectedNode.Text = txtUserName.Text
            selectedNode.Name = selectedNode.Text

            If selectedNode.Parent.Text = "User Types" Then

                Users_TV.SelectedNode.Remove()

                Dim groupNode As TreeNode

                For Each n As TreeNode In Users_TV.Nodes(0).Nodes
                    If n.Text = userType Then
                        groupNode = n
                    End If
                Next

                If groupNode IsNot Nothing Then
                    groupNode.Nodes.Add(selectedNode)
                Else
                    Users_TV.Nodes(0).Nodes.Add(cmbUserType.SelectedItem.ToString()).Nodes.Add(selectedNode)
                End If

            End If

            'Users_TV.SelectedNode.Text = TB_User.Text
            loaduser()
        End If

        Dim admin As Boolean = False
        If (cmbUserType.SelectedItem.ToString() = "Admin") Then
            admin = True
        End If

        'If Not String.IsNullOrEmpty(TB_Pass.Text) Then
        '    setMysqlAccess(TB_User.Text, TB_Pass.Text, admin)
        'End If

        txtPassword.Text = ""

        enableORnotSave()

        MessageBox.Show("Your changes have been saved !")

    End Sub

    Private Sub btnEditPassword_Click(sender As Object, e As EventArgs) Handles btnEditPassword.Click
        txtPassword.Enabled = Not txtPassword.Enabled
        PictureBox5.Visible = txtPassword.Enabled
    End Sub

#End Region

    '======================================================================================================================================================================================================

#Region "Dashboard functions"


    Private Sub TreeView4_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TV_LivestatusMachine.AfterCheck

        If e.Node.ForeColor = Color.Red And treeViewClick Then
            RemoveHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck
            e.Node.Checked = Not e.Node.Checked
            AddHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck
            treeViewClick = False
            Return
        End If

        LiveStatusMachineAfterCheck(e.Node)

    End Sub

    Private Sub TreeView4_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TV_LivestatusMachine.NodeMouseDoubleClick, TV_LivestatusMachine.NodeMouseClick

        treeViewClick = True

        If e.Button = Windows.Forms.MouseButtons.Right Then
            TV_LivestatusMachine.SelectedNode = e.Node
        End If

    End Sub

    Private Sub LiveStatusMachineAfterCheck(treeNode As TreeNode)

        If fiiliing = False Then
            Dim added_machine As New List(Of String)

            RemoveHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck

            If configisloading = False Then
                If treeNode.Text = "All machines" Then
                    If treeNode.Checked = False Then

                        For Each n As TreeNode In TV_LivestatusMachine.Nodes            '(0).Nodes
                            n.Checked = False
                        Next

                        dashboardDevice.Machines = ""
                        dashboardDevice.Groups = ""
                        dashboardDevice.SaveDevice()

                    End If
                End If
            End If

            Call CheckAllChildNodes(treeNode)

            For Each node As TreeNode In TV_LivestatusMachine.Nodes
                CheckAllNodes(node, treeNode)
            Next

            If treeNode.Checked Then
                If treeNode.Parent Is Nothing = False Then
                    Dim allChecked As Boolean = True

                    Call IsEveryChildChecked(treeNode.Parent, allChecked)

                    If allChecked Then
                        treeNode.Parent.Checked = True
                        Call ShouldParentsBeChecked(treeNode.Parent)
                    End If
                End If
            Else
                Dim parentNode As TreeNode = treeNode.Parent
                While parentNode Is Nothing = False
                    parentNode.Checked = False
                    parentNode = parentNode.Parent
                End While
            End If

            'Partially Selected Checkbox Logic is here 
            Dim SelectedMachineCount As Integer
            Dim NoofGrps As Integer
            Dim AllMachineCount As Integer
            Dim totalgrp As Integer
            Dim groups_ As String = ""

            totalgrp = 0
            SelectedMachineCount = 0
            NoofGrps = 0
            AllMachineCount = 0

            'All Machines
            For Each SNode As TreeNode In TV_LivestatusMachine.Nodes(0).Nodes
                If SNode.Checked Then
                    SelectedMachineCount += 1
                End If
                AllMachineCount += 1
            Next

            If SelectedMachineCount < AllMachineCount And SelectedMachineCount > 0 Then
                TV_LivestatusMachine.Nodes(0).ForeColor = Color.DarkGray
                TV_LivestatusMachine.Nodes(0).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                TV_LivestatusMachine.Nodes(0).Text += String.Empty
            ElseIf SelectedMachineCount = AllMachineCount And SelectedMachineCount > 0 Then
                groups_ += TV_LivestatusMachine.Nodes(0).Text & ", "
                TV_LivestatusMachine.Nodes(0).ForeColor = Color.Black
                TV_LivestatusMachine.Nodes(0).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                TV_LivestatusMachine.Nodes(0).Text += String.Empty
            Else
                TV_LivestatusMachine.Nodes(0).ForeColor = Color.Black
                TV_LivestatusMachine.Nodes(0).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Regular)
            End If

            'Group of Machines
            NoofGrps = TV_LivestatusMachine.Nodes(1).Nodes.Count
            For Each TotalGroups As TreeNode In TV_LivestatusMachine.Nodes(1).Nodes
                If TotalGroups.Checked Then
                    totalgrp += 1
                End If
            Next

            If totalgrp < NoofGrps And totalgrp > 0 Then
                TV_LivestatusMachine.Nodes(1).ForeColor = Color.DarkGray
                TV_LivestatusMachine.Nodes(1).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                TV_LivestatusMachine.Nodes(1).Text += String.Empty
            ElseIf totalgrp = NoofGrps And totalgrp > 0 Then
                TV_LivestatusMachine.Nodes(1).ForeColor = Color.Black
                TV_LivestatusMachine.Nodes(1).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                TV_LivestatusMachine.Nodes(1).Text += String.Empty
            Else
                TV_LivestatusMachine.Nodes(1).ForeColor = Color.Black
                TV_LivestatusMachine.Nodes(1).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Regular)
            End If

            'Particular Group Machines
            Dim Groups(NoofGrps - 1) As Integer

            For grp As Integer = 0 To (Groups.Length - 1)
                Dim SelectedGroupCount As Integer
                SelectedGroupCount = 0
                Groups.SetValue(TV_LivestatusMachine.Nodes(1).Nodes(grp).Nodes.Count, grp)
                For Each GNode As TreeNode In TV_LivestatusMachine.Nodes(1).Nodes(grp).Nodes
                    If GNode.Checked Then
                        SelectedGroupCount += 1
                    End If
                Next
                If SelectedGroupCount < Groups(grp) And SelectedGroupCount > 0 Then
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).Checked = False
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).ForeColor = Color.DarkGray
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).Text += String.Empty
                    TV_LivestatusMachine.Nodes(1).ForeColor = Color.DarkGray
                    TV_LivestatusMachine.Nodes(1).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                    TV_LivestatusMachine.Nodes(1).Text += String.Empty
                ElseIf SelectedGroupCount = Groups(grp) And SelectedGroupCount > 0 Then
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).Checked = True
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).ForeColor = Color.Black
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).Text += String.Empty
                    groups_ += TV_LivestatusMachine.Nodes(1).Nodes(grp).Text & ","
                Else
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).Checked = False
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).ForeColor = Color.Black
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Regular)
                End If
            Next

            groups_ = groups_.TrimEnd(",")

            Dim value_ As String = "", machines_readed As List(Of String) = readTreeRecurciveDevice(TV_LivestatusMachine), groups_readed As List(Of String) = readTreeRecurcivegroup(TV_LivestatusMachine)

            If Not IsNothing(machines_readed) Then
                For Each machine As String In machines_readed
                    If value_ = "" Then
                        value_ = machine
                        added_machine.Add(machine)
                    Else
                        If Not added_machine.Contains(machine) Then
                            value_ = value_ & ", " & machine
                            added_machine.Add(machine)
                        End If
                    End If
                Next
                If Not IsNothing(groups_readed) Then
                    For Each machine As String In groups_readed
                        If value_ = "" Then
                            value_ = machine
                            added_machine.Add(machine)
                        Else
                            If Not added_machine.Contains(machine) Then
                                value_ = value_ & ", " & machine
                                added_machine.Add(machine)
                            End If
                        End If
                    Next
                End If

                If configisloading = False Then

                    dashboardDevice.Machines = value_
                    dashboardDevice.Groups = groups_
                    dashboardDevice.SaveDevice()

                End If
            End If

            AddHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck

        End If

        RefreshDevice(IP_TB.Text)

    End Sub

    Private Function load_userConfig(mysqlcntstr As String)

        Try
            inLoadingDeviceMode = True

            deviceId = CInt(treeviewDevices.SelectedNode.Name)

            dashboardDevice = New DashboardDevice()
            dashboardDevice.LoadDevice(deviceId)

            'Dim dtDeviceConfig As DataTable = MySqlAccess.GetDataTable($"SELECT * from CSI_database.tbl_deviceConfig WHERE name = '{ Devices_TV.SelectedNode.Text }'")

            Name_TB.Text = dashboardDevice.DeviceName

            IP_TB.Text = dashboardDevice.IpAddress

            LSD_TB.Checked = dashboardDevice.LiveStatusDelay

            DateTime_TB.Checked = dashboardDevice.DateTime

            TB_format.Enabled = dashboardDevice.DateTime

            TB_format.Text = dashboardDevice.DateFormat

            Temp_CB.Checked = dashboardDevice.Temperature

            City_TB.Text = dashboardDevice.DetailTemperature

            Degree_CB.SelectedIndex = Degree_CB.FindStringExact(dashboardDevice.Degree)

            Url_TB.Text = dashboardDevice.DetailIFrame

            Sec_TB.Text = dashboardDevice.DetailLiveStatusDelay

            Logo_TB.Checked = dashboardDevice.CustomLogo

            txtLogoPath.Text = dashboardDevice.DetailCustomLogo

            chkLogoBarHidden.Checked = dashboardDevice.LogoBarHidden

            chkDarkTheme.Checked = dashboardDevice.DarkTheme

            MESS_CB.Checked = dashboardDevice.Messages

            LSFullscreen_CB.Checked = dashboardDevice.FullScreen

            If dashboardDevice.DeviceType = "Computer" Then
                CB__DeviceType.SelectedIndex = 0
            Else
                CB__DeviceType.SelectedIndex = 1
            End If

            IF_ON.Checked = dashboardDevice.IFrame
            IF_OFF.Checked = Not dashboardDevice.IFrame

            IFFullscreen_CB.Enabled = True
            IF_Rot_CB.Enabled = True

            If Not dashboardDevice.IFrame Then
                IF_Rot_CB.Checked = False
                IF_Rot_CB.Enabled = False
            Else
                IF_Rot_CB.Checked = dashboardDevice.Rotation
            End If

            Name_TB.Enabled = True
            IP_TB.Enabled = True
            CB__DeviceType.Enabled = True

            If dashboardDevice.DeviceName = "Local Host" Then
                Name_TB.Enabled = False
                CB__DeviceType.Enabled = False
            End If

            BTN_ping.Text = "Ping"
            BTN_ping.BackColor = Color.White


            Dim dtab As List(Of String) = New List(Of String)
            Dim ind As Integer = 0
            Dim cpt As Integer = 1
            Dim temps As String = ""

            Dim dtMessages As DataTable = MySqlAccess.GetDataTable($"SELECT messages from CSI_database.tbl_messages WHERE deviceId = { deviceId } order by Priority")

            Mess_DGV.Rows.Clear()

            For Each r As DataRow In dtMessages.Rows
                temps = r.Item(0).ToString()
                If (r.Item(0).ToString().Contains("_CON")) Then
                    temps = r.Item(0).ToString().Substring(5, r.Item(0).ToString().Length - 5)
                End If
                Mess_DGV.Rows.Add(temps, cpt.ToString())
                cpt += 1
            Next

            Dim phrase As String = ","
            Dim Occurrences As Integer = 0

            Dim intCursor As Integer = 0

            Dim input As String = dashboardDevice.Machines

            Do Until intCursor >= input.Length

                Dim strCheckThisString As String = Mid(LCase(input), intCursor + 1, (Len(input) - intCursor))

                Dim intPlaceOfPhrase As Integer = InStr(strCheckThisString, phrase)
                If intPlaceOfPhrase > 0 Then
                    Occurrences += 1
                    intCursor += (intPlaceOfPhrase + Len(phrase) - 1)
                Else
                    intCursor = input.Length
                End If
            Loop

            Dim dt As List(Of String) = input.Split(",").Select(Function(s) s.Trim()).ToList()

            'input = input + ",."
            'For i = 0 To Occurrences
            '    Dim temp As String = input.Substring(0, input.IndexOf(","))
            '    dt.Add(temp)
            '    input = input.Substring(input.IndexOf(",") + 1, input.Count - 1 - input.IndexOf(","))
            'Next

            For Each p As TreeNode In TV_LivestatusMachine.Nodes
                p.Checked = False
                For Each n As TreeNode In p.Nodes
                    n.Checked = False
                    For Each m As TreeNode In n.Nodes
                        m.Checked = False
                    Next
                Next
            Next

            Dim dtMachines As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf;")
            Dim rowMachine As DataRow
            Dim machineId As Integer = 0

            For Each machine As String In dt

                If Integer.TryParse(machine, machineId) Then
                    rowMachine = dtMachines.Rows.Cast(Of DataRow).Where(Function(r) r.Item("Id").ToString() = machine).FirstOrDefault()
                Else
                    rowMachine = dtMachines.Rows.Cast(Of DataRow).Where(Function(r) r.Item("Machine_name").ToString() = machine Or r.Item("EnetMachineName").ToString() = machine).FirstOrDefault()
                End If

                If Not IsNothing(rowMachine) Then

                    machineId = rowMachine.Item("Id")

                    'machine = machine.Trim()

                    For Each p As TreeNode In TV_LivestatusMachine.Nodes

                        If p.Tag = machineId Then
                            p.Checked = True
                        Else

                            For Each n As TreeNode In p.Nodes

                                If n.Tag = machineId Then
                                    n.Checked = True
                                Else
                                    For Each m As TreeNode In n.Nodes
                                        If m.Tag = machineId Then
                                            m.Checked = True
                                        End If
                                    Next
                                End If

                            Next

                        End If

                    Next

                End If

            Next

            inLoadingDeviceMode = False

        Catch ex As Exception

            MessageBox.Show("Unable to load device configuration, there might be a problem with your database. See log for details")

            Log.Error("Unable to load device config.", ex)

        End Try

    End Function

    Private Sub UpdateDevice(sender As Object, e As EventArgs) Handles Temp_CB.CheckedChanged,
                                                                       Name_TB.Validated,
                                                                       LSFullscreen_CB.CheckedChanged,
                                                                       Logo_TB.CheckedChanged,
                                                                       IP_TB.Validated,
                                                                       IF_Rot_CB.CheckedChanged,
                                                                       Degree_CB.SelectedIndexChanged,
                                                                       DateTime_TB.CheckedChanged,
                                                                       chkLogoBarHidden.CheckedChanged,
                                                                       chkDarkTheme.CheckedChanged,
                                                                       IF_ON.CheckedChanged,
                                                                       IF_OFF.CheckedChanged,
                                                                       MESS_CB.CheckedChanged,
                                                                       CB__DeviceType.SelectedIndexChanged,
                                                                       Url_TB.Validated,
                                                                       TB_format.Validated,
                                                                       Sec_TB.Validated,
                                                                       txtLogoPath.Validated,
                                                                       City_TB.Validated

        If dashboardDevice IsNot Nothing And Not inLoadingDeviceMode Then

            If Not dashboardDevice.IpAddress = "" Then

                If dashboardDevice.IpAddress <> IP_TB.Text Then
                    dashboardDevice.UpdateIpAddress(IP_TB.Text)
                    RefreshDevice(IP_TB.Text)
                    Return
                End If

                dashboardDevice.DeviceName = Name_TB.Text

                dashboardDevice.LiveStatusDelay = True
                dashboardDevice.DetailLiveStatusDelay = Sec_TB.Text
                If String.IsNullOrEmpty(Sec_TB.Text) Then
                    dashboardDevice.DetailLiveStatusDelay = "0"
                End If

                dashboardDevice.DateTime = DateTime_TB.Checked
                dashboardDevice.DateFormat = TB_format.Text

                dashboardDevice.Temperature = Temp_CB.Checked
                dashboardDevice.Degree = Degree_CB.SelectedItem.ToString()

                lblTemperature.Text = ""

                If dashboardDevice.Temperature And City_TB.Text <> "" Then
                    Dim ret = CSIFLEX.Utilities.OpenWeather.GetTemperature(City_TB.Text, dashboardDevice.Degree)

                    If ret.Contains("(404)") Then
                        lblTemperature.Text = "City not found"
                    Else
                        lblTemperature.Text = ret
                        dashboardDevice.DetailTemperature = City_TB.Text.Replace(" ", "")
                    End If
                Else
                    dashboardDevice.DetailTemperature = City_TB.Text
                End If

                dashboardDevice.CustomLogo = Logo_TB.Checked
                dashboardDevice.DetailCustomLogo = txtLogoPath.Text

                dashboardDevice.Messages = MESS_CB.Checked

                dashboardDevice.LogoBarHidden = chkLogoBarHidden.Checked
                dashboardDevice.DarkTheme = chkDarkTheme.Checked

                dashboardDevice.IFrame = IF_ON.Checked
                dashboardDevice.DetailIFrame = Url_TB.Text

                If dashboardDevice.IFrame Then
                    IF_Rot_CB.Enabled = True
                Else
                    IF_Rot_CB.Checked = False
                    IF_Rot_CB.Enabled = False
                End If

                dashboardDevice.FullScreen = LSFullscreen_CB.Checked
                dashboardDevice.Rotation = IF_Rot_CB.Checked

                dashboardDevice.DeviceType = "Computer"
                If CB__DeviceType.SelectedIndex = 1 Then
                    dashboardDevice.DeviceType = "LR1"
                End If

                Try
                    dashboardDevice.SaveDevice()

                    treeviewDevices.SelectedNode.Text = Name_TB.Text

                    Dim sqlCd = $"UPDATE CSI_database.tbl_messages SET name = '{ Name_TB.Text }' WHERE name = '{ treeviewDevices.SelectedNode.Text }'"

                Catch ex As Exception

                    MsgBox(" Could not update the settings : " & ex.Message)
                    CSI_Lib.LogServerError(" Could not update the settings : " & ex.Message, 1)
                End Try

                RefreshDevice(IP_TB.Text)
            End If
        End If

    End Sub


#End Region

    '======================================================================================================================================================================================================


#Region "Auxiliar functions"

    Private Sub move_htm_files()

        isComingFromChangeIP = False

        If Not Directory.Exists(CSI_Library.CSI_Library.serverRootPath & "\html\") Then

            CSI_Lib.Log_server_event("html files not found, copying files ...")

            Dim path As String = System.Reflection.Assembly.GetExecutingAssembly().Location
            path = System.IO.Path.GetDirectoryName(path)

            If Directory.Exists(path) Then

                path = path & "\html\"

                If Directory.Exists(path) Then
                    Directory.Move(path, CSI_Library.CSI_Library.serverRootPath & "\html\")
                    change_IP(True)
                End If
            Else
                CSI_Lib.Log_server_event("html files not found in : " & path)
            End If
        Else
            change_IP(True)
        End If

    End Sub

    Private Sub change_IP(checkfile As Boolean)

        Dim IP_0 As String = ""

        If checkfile = True Then
            Try
                If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")) Then

                    Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")
                        IP_0 = reader.ReadLine
                        IP_choice.LB_IPCheckChanged.Text = String.Empty
                        IP_choice.LB_IPCheckChanged.Text = IP_0
                        LBL_IP.Text = IP_0
                        reader.Close()
                    End Using

                End If
            Catch ex As Exception
                CSI_Lib.LogServerError("At reading srvip  from csys: " & ex.Message, 1)
            End Try
        End If

        Dim IPv4Address As New List(Of String)
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                IPv4Address.Add(ipheal.ToString())
            End If
        Next

        If IPv4Address.Count = 0 Then
            MsgBox("No Ip Address found")
            Me.Close()
        End If


        If IPv4Address.Count > 1 And checkfile = True And IPv4Address.Contains(IP_0) Then Return


        If IPv4Address.Count > 1 Then
            Try
                If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")) Then
                    Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")
                        IP_0 = reader.ReadLine
                        IP_choice.LB_IPCheckChanged.Text = String.Empty
                        IP_choice.LB_IPCheckChanged.Text = IP_0
                        reader.Close()
                    End Using
                End If
            Catch ex As Exception
                CSI_Lib.LogServerError("At reading srvip  from csys: " & ex.Message, 1)
            End Try

            IP_choice.LB_list.DataSource = IPv4Address
            IP_choice.ShowDialog()

            If CSI_Library.CSI_Library.IPChange = 1 Then
                LBL_IPChange.Text = "IP did not change !"
                LBL_IPChange.Visible = True
                LBL_IPChange.BackColor = Color.Yellow
                t1.Interval = 10000
                AddHandler t1.Tick, AddressOf Tickhandle
                t1.Start()

            ElseIf CSI_Library.CSI_Library.IPChange = 2 Then

                Dim serviceexepath As String = String.Empty

                serviceexepath = AppDomain.CurrentDomain.BaseDirectory + "CSIFlexServerServices.exe"

                If (ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlexServerService")) Then

                    'If below code not working uncomment above two lines and comment below Logic upto Else 
                    While ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Run Or ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Starting
                        Do Until ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Stop
                            CSI_Lib.KillingAProcess("CSIFlexServerService")
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
                        NewUpdate = "UPDATE `csi_auth`.`tbl_csiconnector` SET `AgentIP` = '" & IP_ & "' WHERE ConnectorType ='Focas';"
                        Dim cmdOtherUpdate As New MySqlCommand(NewUpdate, cntsql)
                        Dim rdrOtherUpdate As MySqlDataReader = cmdOtherUpdate.ExecuteReader
                        cntsql.Close()
                    Catch ex As Exception
                        MessageBox.Show("Agent IP Update Database Error : " & ex.Message())
                    End Try

                    LBL_IPChange.Text = "IP Changed Sucessfully !"
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
                t2.Interval = 10000
                AddHandler t2.Tick, AddressOf Tickhandle
                t2.Start()
            Else
                'Do nothing
            End If

        Else
            If isComingFromChangeIP Then
                isComingFromChangeIP = False
                LBL_IPChange.Text = "Your PC has only one IP Address !"
                LBL_IPChange.Visible = True
                LBL_IPChange.BackColor = Color.LimeGreen
                t5.Interval = 10000
                AddHandler t5.Tick, AddressOf Tickhandle
                t5.Start()
            End If
            IP_ = IPv4Address.Item(0)
        End If

        Try
            ' If File.Exists((CSI_Lib.serverRootPath & "\sys\srv_ip_.csys")) Then
            Using w_ As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")
                w_.WriteLine(IP_)
                LBL_IP.Text = IP_
            End Using
            '  End If

        Catch ex As Exception
            CSI_Lib.LogServerError("At writing srv_ip to csys: " & ex.Message, 1)
        End Try

        Dim PORT As String = ""

        Try
            If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")) Then
                Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                    PORT = reader.ReadLine
                    reader.Close()
                End Using
            Else
                PORT = "8008"
            End If
        Catch ex As Exception
            CSI_Lib.LogServerError("At reading RM Port from csys: " & ex.Message, 1)
        End Try


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
end_:
    End Sub

    ''<PrincipalPermission(SecurityAction.Demand, Role:="Administrators", Authenticated:=True)> _
    'Private Sub InstallAndStartService()

    '    Try
    '        Dim serviceexepath As String = String.Empty

    '        serviceexepath = AppDomain.CurrentDomain.BaseDirectory + "CSIFlexServerService.exe"

    '        'Add your code here for Start Service On StartUp
    '        If (ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlexServerService")) Then

    '            If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Run) Or
    '                (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Starting) Then

    '                If isTrytoRestarting = True Then
    '                    MessageBox.Show("CSIFlex_Server is already running")
    '                    ServiceStarted = True
    '                    isTrytoRestarting = False
    '                End If
    '            Else
    '                ServiceTools.ServiceInstaller.StartService("CSIFlexServerService")
    '                ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlexServerService")
    '                ServiceStarted = True
    '            End If
    '        Else
    '            ServiceTools.ServiceInstaller.InstallAndStart("CSIFlexServerService", "CSIFlexServerService", serviceexepath)
    '            ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlexServerService")
    '            ServiceStarted = True
    '        End If

    '    Catch ex As Exception

    '        MessageBox.Show("There was an error trying to start the Main service. See the log for more details :" & ex.Message)
    '        CSI_Lib.LogServiceError("There was an error trying to start the Main service. See the log for more details :" & ex.Message, 1)
    '    End Try
    'End Sub

    '' <PrincipalPermission(SecurityAction.Demand, Role:="BUILTIN\Administrators", Authenticated:=False)> _
    'Private Sub StopService()

    '    Try
    '        Dim serviceexepath As String = AppDomain.CurrentDomain.BaseDirectory + "CSIFlexServerService.exe"

    '        If (ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlexServerService")) Then

    '            If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Run) Then

    '                CSI_Lib.KillingAProcess("CSIFlexServerService")
    '                ServiceStarted = False

    '            Else
    '                MessageBox.Show("CSIFlex_Server is not running")
    '            End If
    '        Else
    '            MessageBox.Show("Service is not installed")
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show("There was an error trying to stop the service. See the log for more details")
    '        CSI_Lib.LogServiceError(ex.Message, 1)
    '    End Try
    'End Sub


    Function readTreeRecurcivegroup(treeViewMachines As TreeView) As List(Of String)

        checked__treeviewGroup.Clear()

        Dim aNode As TreeNode

        For Each aNode In treeViewMachines.Nodes(1).Nodes
            PrintRecursivegroup(aNode)
        Next
        Return checked__treeviewGroup
    End Function

    Function readTreeRecurciveDevice(treeViewMachines As TreeView) As List(Of String)

        checked__treeviewDevice.Clear()

        Dim aNode As TreeNode

        For Each aNode In treeViewMachines.Nodes(0).Nodes
            PrintRecursiveDevice(aNode)
        Next
        Return checked__treeviewDevice

    End Function


#End Region


    Dim DailyTargets As Dictionary(Of String, Integer)
    Dim WeeklyTargets As Dictionary(Of String, Integer)
    Dim MonthlyTargets As Dictionary(Of String, Integer)


    Private Sub MainFrm_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        If Me.WindowState = FormWindowState.Minimized Then

            Edit_eNET.Hide()

        End If

        Edit_eNET.BringToFront()

    End Sub


    Public IP_ As String = ""


    Private Sub Tickhandle()
        LBL_IPChange.Visible = False
        LBL_IPChange.Text = String.Empty
        CSI_Library.CSI_Library.IPChange = 0
        t1.Dispose()
        t2.Dispose()
        t4.Dispose()
        t5.Dispose()
    End Sub


    Private Sub TimerToShowHide()
        If txtPassword.PasswordChar = "*"c Then
            txtPassword.PasswordChar = ControlChars.NullChar
        ElseIf txtPassword.PasswordChar = ControlChars.NullChar Then
            txtPassword.PasswordChar = "*"c
        End If
        timerForShowHide.Dispose()
    End Sub


    Private Sub Load_rm_port()

        Dim cnt As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try
            Dim port_ As String = "8008"
            If File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys") Then

                Using rdr As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                    port_ = rdr.ReadLine
                    ServerPort = port_
                    rdr.Close()
                End Using
            Else
                Using wrt As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                    wrt.WriteLine(port_)
                    wrt.Close()
                End Using
            End If

            cnt.Open()
            If cnt.State = ConnectionState.Open Then

                If (MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_RM_Port SET port = { port_ }") = 0) Then
                    MySqlAccess.ExecuteNonQuery($"INSERT INTO CSI_database.tbl_RM_Port ( port ) VALUES ( {port_} )")
                End If

            End If

        Catch ex As Exception
            If cnt.State = ConnectionState.Open Then cnt.Close()
            MsgBox("Could not load or open a port")
            CSI_Lib.LogServerError(ex.Message, 1)
        End Try

    End Sub


    Private Sub SET_CSIF_PI()
        If File.Exists(path & "\sys\CSIF_IP.csys") Then
            File.Delete(path & "\sys\CSIF_IP.csys")
            Using rW_IP As StreamWriter = New StreamWriter(path & "\sys\CSIF_IP.csys")
                rW_IP.WriteLine(GetIPv4Address())
                rW_IP.Close()
            End Using
        Else

            Using rW_IP As StreamWriter = New StreamWriter(path & "\sys\CSIF_IP.csys")
                rW_IP.WriteLine(GetIPv4Address())
                rW_IP.Close()
            End Using
        End If
    End Sub


    Public Shared Function CheckForInternetConnection() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    Return True
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function


    Private Sub checkfortemp()
        If CheckForInternetConnection() = False Then
            Temp_CB.Enabled = False
            'Label18.Enabled = False
            Label24.Enabled = False
            City_TB.Enabled = False
            Degree_CB.Enabled = False
            Label23.Enabled = False
        Else
            Temp_CB.Enabled = True
            ' Label18.Enabled = True
            Label24.Enabled = True
            City_TB.Enabled = True
            Degree_CB.Enabled = True
            Label23.Enabled = True
        End If
    End Sub


    Public Function GetIPv4Address() As String

        GetIPv4Address = String.Empty

        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                GetIPv4Address = ipheal.ToString()
            End If
        Next

    End Function


    Function GetEnetGraphColor(eNETrep As String) As Dictionary(Of String, Integer)

        Dim res As Dictionary(Of String, Integer)

        Dim file As System.IO.StreamReader

        Dim color_list As New Dictionary(Of String, Integer), line As String()

        If My.Computer.FileSystem.FileExists(eNETrep + "\_SETUP\GraphColor.sys") Then

            file = My.Computer.FileSystem.OpenTextFileReader(eNETrep + "\_SETUP\GraphColor.sys")
            While Not file.EndOfStream
                line = file.ReadLine().Split(",")
                If line(1) <> "" Then
                    If color_list.ContainsKey(line(1)) Then

                    Else
                        color_list.Add(line(1).ToUpperInvariant, line(0))
                    End If

                End If

            End While
            file.Close()
        End If

        res = color_list
        Return res

    End Function

    Public Sub loadEnetPwd()

        Try
            Dim enetPath As String

            If ServerSettings.EnetFolder <> "" Then

                enetPath = IO.Path.Combine(eNetServer.EnetSetupFolder, "Options.sys")

                If File.Exists(enetPath) Then

                    Dim line As String = ""
                    Dim idx As Integer = 0
                    Dim ftpIp As String = ""
                    Dim ftpPwd As String = ""

                    Using reader As StreamReader = New StreamReader(enetPath)
                        While Not reader.EndOfStream
                            idx += 1
                            line = reader.ReadLine()

                            If idx = 25 Then
                                ftpIp = Util.ConvertHexToIpAddress(line)
                            End If
                            If idx = 26 Then
                                ftpPwd = line
                            End If
                        End While
                    End Using

                    Dim dt = MySqlAccess.GetDataTable($"SELECT ftpip FROM csi_auth.ftpconfig WHERE ftpip = '{ftpIp}'")

                    If dt.Rows.Count > 0 Then
                        MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.ftpconfig SET ftppwd = '{ftpPwd}' WHERE ftpip = '{ftpIp}'")
                    Else
                        MySqlAccess.ExecuteNonQuery($"TRUNCATE csi_auth.ftpconfig; INSERT INTO csi_auth.ftpconfig (ftpip, ftppwd) VALUES ('{ftpIp}', '{ftpPwd}')")
                    End If
                End If
            Else
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub

    Public Sub load_colors()
        Try
            Dim chemin_Color As String = ""
            Dim scolor As String = ""

            If Exists(path & "\sys\SetupColor_.csys") Then
                Using reader As StreamReader = New StreamReader(path & "\sys\SetupColor_.csys")
                    chemin_Color = reader.ReadLine()
                End Using
            ElseIf Exists(path & "\sys\setup_.csys") Then
                Using reader As StreamReader = New StreamReader(path & "\sys\setup_.csys")
                    chemin_Color = reader.ReadLine()
                End Using
            Else
                MessageBox.Show("Please specify the eNET folder")
            End If

            Dim int As Integer = 0
            Dim alpha As Integer = 255
            Dim backcolor As Color
            Dim MyImageList As New ImageList()
            Dim colors As Dictionary(Of String, Integer)
            colors = GetEnetGraphColor(chemin_Color)

            Dim sql As String

            For Each status As String In colors.Keys

                backcolor = System.Drawing.ColorTranslator.FromWin32(colors(status))
                scolor = System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(backcolor.R, backcolor.G, backcolor.B))

                tabColor.Add(status, scolor)

                sql = $"insert into CSI_database.tbl_colors  ( statut,  color)  values ( '{ tabColor.Keys(int) }', '{ tabColor.Values(int) }')  ON DUPLICATE KEY update statut = ('{ tabColor.Keys(int) }'), color = ('{ tabColor.Values(int) }')"

                int = int + 1

                Try
                    MySqlAccess.ExecuteNonQuery(sql)

                Catch x As Exception
                    MessageBox.Show(x.ToString())
                End Try
            Next

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            savedatagridview1()
        Catch

        End Try

    End Sub

    Private Sub SetupForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Try
            If Not (TabControl_DashBoard.SelectedTab.Text = "eNETDNC") And (TargetsChanged = True) Then
                TargetsChanged = False
                UpdateTargetsTitle()
                RefreshAllDevices()

            End If

            If Not (TabControl_DashBoard.SelectedTab.Text = "eNETDNC") And (Comput_perf_required = True) Then
                ComputePerfReq()
                Comput_perf_required = False
            End If

            If Not isUnistalling Then
                If BgWorker_CreateDB.IsBusy Then

                    Dim Result = MessageBox.Show("Database is being created, Do you want to exit?.", "CAUTION", MessageBoxButton.OKCancel, MessageBoxImage.Information)
                    If (Result = MessageBoxResult.OK) Then
                        UserLogin.Close()
                        Form1.Close()
                        Environment.Exit(0)
                    Else
                        e.Cancel = True
                        GoTo end___
                    End If
                ElseIf BgWorker_importDB.IsBusy Then
                    Dim Result = MessageBox.Show("CSV file is being imported, Do you want to exit?.", "CAUTION", MessageBoxButton.OKCancel, MessageBoxImage.Information)
                    If (Result = MessageBoxResult.OK) Then
                        UserLogin.Close()
                        Form1.Close()
                        Environment.Exit(0)
                    Else
                        e.Cancel = True
                        GoTo end___
                    End If
                Else

                    Dim Result = MessageBox.Show("Do you want to exit the application ?.", "Exit", MessageBoxButton.OKCancel, MessageBoxImage.Information)

                    If (Result = MessageBoxResult.OK) Then
                        UserLogin.Close()
                        Form1.Close()
                        'Environment.Exit(0)
                    Else
                        e.Cancel = True
                        GoTo end___
                    End If
                End If
            Else
                Environment.Exit(0)
            End If

end___:

        Catch ex As Exception

        End Try

    End Sub


#Region "Browse"

    ''-----------------------------------------------------------------------------------------------------------------------
    ''  Browse, eNET folder
    ''-----------------------------------------------------------------------------------------------------------------------
    'Private Sub Button1_Click(sender As Object, e As EventArgs)
    '    Dim folderDlg As New FolderBrowserDialog
    '    folderDlg.Description = "Specify the eNET folder"

    '    Try
    '        folderDlg.ShowNewFolderButton = False
    '        If (folderDlg.ShowDialog() = DialogResult.OK) Then
    '            TextBox1.Text = folderDlg.SelectedPath
    '            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    'End Sub





    '-----------------------------------------------------------------------------------------------------------------------
    '  Browse, db folder
    '-----------------------------------------------------------------------------------------------------------------------


    '-----------------------------------------------------------------------------------------------------------------------
    ' Default chemin enet
    '-----------------------------------------------------------------------------------------------------------------------
    'Private Sub Button5_Click(sender As Object, e As EventArgs)
    '    TextBox1.Text = "C:\_eNETDNC"
    'End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' Default chemin bdd
    '-----------------------------------------------------------------------------------------------------------------------


    '-----------------------------------------------------------------------------------------------------------------------
    ' Browse autoreport folder
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Button8_Click(sender As Object, e As EventArgs)
        Dim folderDlg As New FolderBrowserDialog

        folderDlg.ShowNewFolderButton = False
        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            'TextBox3.Text = folderDlg.SelectedPath
            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
        End If


        Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\Setup_Export.csys")
            '   writer.Write(TextBox3.Text)
            writer.Close()
        End Using
    End Sub

#End Region

    Public Function mois_(MAXMOI As Integer)
        Try
            Select Case MAXMOI
                Case "01"
                    Return "Jan"
                Case "02"
                    Return "Feb"
                Case "03"
                    Return "Mar"
                Case "04"
                    Return "Apr"
                Case "05"
                    Return "May"
                Case "06"
                    Return "Jun"
                Case "07"
                    Return "Jul"
                Case "08"
                    Return "Aug"
                Case "09"
                    Return "Sep"
                Case "10"
                    Return "Oct"
                Case "11"
                    Return "Nov"
                Case "12"
                    Return "Dec"
                Case Else
                    Return Nothing
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function


    Public machineListe As String() ' machine liste from eNET

    '===================================================================================================
    ' Show Licence window
    '===================================================================================================
    Private Sub Button11_Click(sender As Object, e As EventArgs)
        Login.ShowDialog()
    End Sub


#Region "Auto reporting"

    '-----------------------------------------------------------------------------------------------------------------------
    ' SetupForm auto reporting
    '-----------------------------------------------------------------------------------------------------------------------
    '    Private Sub auto()
    '        If CheckBox1.Checked = False Then ' No AutoReports
    '            If File.Exists(path & "\sys\Auto_.csys") Then
    '                System.IO.File.Delete(path & "\sys\Auto_.csys")
    '            End If

    '            'Stop the service
    '            Try
    '                Dim sc As New ServiceController("CSIservice")
    '                Dim present As Boolean = False
    '                Dim services() As ServiceController = ServiceController.GetServices
    '                For Each service In services
    '                    If service.DisplayName = "CSIservice" Then
    '                        present = True
    '                        GoTo continue_
    '                    End If
    '                Next
    '                If present = False Then GoTo NotContinue

    'continue_:
    '                If sc.Status.Equals(ServiceControllerStatus.Stopped) Or sc.Status.Equals(ServiceControllerStatus.StopPending) Then
    '                Else
    '                    sc.Stop()
    '                    Process.Start("cmd.exe", "/c sc delete CSIservice")
    '                End If
    'NotContinue:



    '            Catch ex As Exception
    '                MessageBox.Show(CSI_Lib.TraceMessage(ex.ToString()))
    '            End Try

    '        Else
    '            Dim lastdate As String = ""

    '            'If File.Exists(Windows.Forms.Application.StartupPath & "\CSIservice.exe") Then
    '            'Else

    '            '    Dim path_exe As String = System.Windows.Forms.Application.StartupPath & "\CSIservice.exe"
    '            '    path_exe = System.Windows.Forms.Application.StartupPath & "\CSIservice.exe"
    '            '    IO.File.WriteAllBytes(path_exe, My.Resources.CSIService)
    '            '    path_exe = System.Windows.Forms.Application.StartupPath & "\srvany.exe"
    '            '    IO.File.WriteAllBytes(path_exe, My.Resources.CSIService)
    '            '    path_exe = System.Windows.Forms.Application.StartupPath & "\Instsrv.exe"
    '            '    IO.File.WriteAllBytes(path_exe, My.Resources.Instsrv)



    '            'End If

    '            '    If TextBox3.Text = "" Then
    '            '        MessageBox.Show("Please specify a reporting folder")
    '            '        Me.Show()
    '            '    Else
    '            '        If File.Exists(path & "\sys\Auto_.sys") Then
    '            '            Using reader As StreamReader = New StreamReader(path & "\sys\Auto_.sys")
    '            '                lastdate = reader.ReadLine()
    '            '                lastdate = reader.ReadLine()
    '            '                reader.Close()
    '            '            End Using
    '            '            System.IO.File.Delete(path & "\sys\Auto_.sys")
    '            '        End If

    '            'Using writer As StreamWriter = New StreamWriter(path & "\sys\Auto_.sys")
    '            '    writer.Write(TextBox3.Text & vbCrLf)
    '            '    If lastdate <> "" Then

    '            '        writer.Write(lastdate & vbCrLf)
    '            '    Else
    '            '        writer.Write(String.Format("{0:dd/MM/yyyy}", DateTime.Now) & vbCrLf)
    '            '    End If

    '            '    If DateTimePicker1.Value.Minute >= 10 Then
    '            '        writer.Write(DateTimePicker1.Value.Hour.ToString() & ":" & DateTimePicker1.Value.Minute.ToString())
    '            '    Else
    '            '        writer.Write(DateTimePicker1.Value.Hour.ToString() & ":0" & DateTimePicker1.Value.Minute.ToString())
    '            '    End If

    '            '    writer.Close()

    '            'End Using

    '            '        Call install_start_Service()

    '            '    End If
    '        End If

    '    End Sub


    '    '-----------------------------------------------------------------------------------------------------------------------
    '    ' Service install and start
    '    '-----------------------------------------------------------------------------------------------------------------------
    '    Private Sub install_start_Service()
    '        Try
    '            'Create the bat file
    '            If (File.Exists(Forms.Application.StartupPath & "\Install_as_Service.bat")) Then
    '                File.Delete(Forms.Application.StartupPath & "\Install_as_Service.bat")
    '            End If

    '            Using sw As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\Install_as_Service.bat")
    '                sw.WriteLine("instsrv CSIservice " & """" & Forms.Application.StartupPath & "\srvany.exe""") '& vbLf & "TIMEOUT /T 10" & vbLf & "sc.exe config CSIservice obj= localsystem")
    '                sw.Close()
    '            End Using
    '        Catch ex As Exception
    '            MessageBox.Show(CSI_Lib.TraceMessage(ex.Message))
    '            GoTo Stop_
    '        End Try

    '        Try
    '            'lanche the bat file
    '            Dim psi As New ProcessStartInfo(Forms.Application.StartupPath & "\Install_as_Service.bat")

    '            psi.RedirectStandardError = True
    '            psi.RedirectStandardOutput = True
    '            psi.CreateNoWindow = False
    '            psi.WindowStyle = ProcessWindowStyle.Hidden
    '            psi.UseShellExecute = False


    '            Dim process As Process = process.Start(psi)

    '            Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\CSIservice", True)

    '            key.SetValue("ObjectName", "Localsystem")
    '            key = key.CreateSubKey("Parameters")
    '            key.SetValue("Application", Forms.Application.StartupPath & "\CSIservice.exe")
    '            key.Close()

    '            Dim sc As New ServiceController("CSIservice")
    '            Dim account As New ProcessStartInfo()
    '            If sc.Status.Equals(ServiceControllerStatus.Stopped) Or sc.Status.Equals(ServiceControllerStatus.StopPending) Then sc.Start()

    '            process.Start("cmd.exe", "/c sc description CSIservice ""CSI Flex Real time Intelligent Reporting application service"" ")

    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    'stop_:
    '    End Sub



    '-----------------------------------------------------------------------------------------------------------------------
    ' Activat/Deactivat the autoreporting checkbox if no path
    '-----------------------------------------------------------------------------------------------------------------------
    'Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs)
    '    If chkAutoReport.Text <> "" Then
    '        chkAutoReport.Enabled = True
    '    Else
    '        chkAutoReport.Enabled = False
    '    End If
    'End Sub


#End Region


#Region "eNET Tab"

    Private Sub LoadEnetSettings()

        Dim path = CSI_Library.CSI_Library.serverRootPath

        lblEnetCheckResult.Text = ""
        lblEnetFolderStatus.Text = ""
        btnEnetSaveChanges.Enabled = False
        btnEnetSaveChanges.BackColor = Color.Transparent

        If pnlMachines.Location.Y = 5 Then Return

        grpEnetSettings.Visible = False
        pnlMachines.Location = New Drawing.Point(8, 5)
        pnlMachines.Size = New Drawing.Size(pnlMachines.Size.Width, pnlMachines.Size.Height + 225)

        originalEnetFolder = ServerSettings.EnetFolder
        txtEnetFolder.Text = originalEnetFolder
        txtEnetFolder_TextChanged(txtEnetFolder, New EventArgs())

        originalEnetHttpIp = ServerSettings.EnetIPAddress
        txtEnetHttpIp.Text = originalEnetHttpIp

        'If File.Exists(path & "\sys\setup_.csys") Then
        '    Using reader As StreamReader = New StreamReader(path & "\sys\setup_.csys")
        '        originalEnetFolder = reader.ReadLine()
        '        txtEnetFolder.Text = originalEnetFolder
        '        txtEnetFolder_TextChanged(txtEnetFolder, New EventArgs())
        '    End Using
        'End If

        'If File.Exists(path & "\sys\Networkenet_.csys") Then
        '    Using reader As StreamReader = New StreamReader(path & "\sys\Networkenet_.csys")
        '        originalEnetHttpIp = reader.ReadLine()
        '        txtEnetHttpIp.Text = originalEnetHttpIp
        '    End Using
        'End If

    End Sub

    Private Sub btnEnetFolderDefault_Click(sender As Object, e As EventArgs) Handles btnEnetFolderDefault.Click
        txtEnetFolder.Text = "C:\_eNETDNC"
    End Sub

    Private Sub btnEnetFolderBrowser_Click(sender As Object, e As EventArgs) Handles btnEnetFolderBrowser.Click

        Dim folderDlg As New FolderBrowserDialog

        folderDlg.Description = "Specify the eNET folder"

        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            txtEnetFolder.Text = folderDlg.SelectedPath
        End If

    End Sub

    Private Sub btnCheckEnetHttpIp_Click(sender As Object, e As EventArgs) Handles btnCheckEnetHttpIp.Click

        'Dim dt As DataTable = clt.Run(txtEnetHttpIp.Text)

        eNetServer.Instance.LoadMachinesStatus()
        Dim machines As List(Of eNetDashboardMachine) = eNetServer.Instance.GetMachinesStatus()

        If machines IsNot Nothing Then

            'If dt.Rows.Count = 0 Then
            '    dt = clt.Run(TB_EnetIp.Text)
            'End If

            lstEnetMachines.Items.Clear()

            For Each machine In machines
                lstEnetMachines.Items.Add(machine.MachineName)
            Next
            lblEnetCheckResult.ForeColor = Color.Green
            lblEnetCheckResult.Text = $"Connection established, { machines.Count.ToString() } machines available."
            eNetServer.Instance.ReloadMachines()
        Else
            lblEnetCheckResult.ForeColor = Color.Red
            lblEnetCheckResult.Text = "No connection available."
        End If

    End Sub

    Private Sub txtEnetFolder_TextChanged(sender As Object, e As EventArgs) Handles txtEnetFolder.TextChanged

        lstEnetYears.Items.Clear()

        If Not originalEnetFolder = txtEnetFolder.Text Or Not originalEnetHttpIp = txtEnetHttpIp.Text Then
            btnEnetSaveChanges.BackColor = Color.DarkSalmon
            btnEnetSaveChanges.Enabled = True
        Else
            btnEnetSaveChanges.BackColor = Color.Transparent
            btnEnetSaveChanges.Enabled = False
        End If

        If String.IsNullOrWhiteSpace(txtEnetFolder.Text) Then Return

        Try
            If Directory.Exists(txtEnetFolder.Text) Then

                lblEnetFolderStatus.ForeColor = Color.Green
                lblEnetFolderStatus.Text = $"Folder found"

                Dim files As String() = Directory.GetFiles($"{txtEnetFolder.Text}\_REPORTS\")

                For Each file In files

                    Dim fileInfo = New FileInfo(file)

                    If fileInfo.Name.StartsWith("_MACHINE_") And fileInfo.Extension = ".CSV" Then
                        Dim year = fileInfo.Name.Substring(fileInfo.Name.Length - 8, 4)
                        lstEnetYears.Items.Add(year)
                    End If
                Next

            Else
                lblEnetFolderStatus.ForeColor = Color.Red
                lblEnetFolderStatus.Text = $"Folder not found"
            End If
        Catch ex As Exception
            lblEnetFolderStatus.ForeColor = Color.Red
            lblEnetFolderStatus.Text = $"Folder not found"
        End Try

    End Sub

    Private Sub txtEnetHttpIp_TextChanged(sender As Object, e As EventArgs) Handles txtEnetHttpIp.TextChanged

        lblEnetCheckResult.Text = ""
        lstEnetMachines.Items.Clear()

        If Not originalEnetFolder = txtEnetFolder.Text Or Not originalEnetHttpIp = txtEnetHttpIp.Text Then
            btnEnetSaveChanges.BackColor = Color.DarkSalmon
            btnEnetSaveChanges.Enabled = True
        Else
            btnEnetSaveChanges.BackColor = Color.Transparent
            btnEnetSaveChanges.Enabled = False
        End If

    End Sub

    Private Sub btnEnetSaveChanges_Click(sender As Object, e As EventArgs) Handles btnEnetSaveChanges.Click

        Try
            ServerSettings.EnetFolder = txtEnetFolder.Text
            ServerSettings.EnetIPAddress = txtEnetHttpIp.Text

            'Using writer As StreamWriter = New StreamWriter(path & "\sys\setup_.csys", False)
            '    writer.Write(txtEnetFolder.Text)
            'End Using

            'Using writer As StreamWriter = New StreamWriter(path & "\sys\Networkenet_.csys", False)
            '    writer.Write(txtEnetHttpIp.Text)
            'End Using

            originalEnetFolder = txtEnetFolder.Text
            originalEnetHttpIp = txtEnetHttpIp.Text
            btnEnetSaveChanges.BackColor = Color.Transparent
            btnEnetSaveChanges.Enabled = False

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnLoadEnetHistory_Click(sender As Object, e As EventArgs) Handles btnLoadEnetHistory.Click

        Dim years As String = ""

        Me.Cursor = Cursors.WaitCursor

        For Each item In lstEnetYears.Items
            If item.Checked Then
                years += item.text.ToString() + ","
            End If
        Next

        If years.Length = 0 Then Return

        years = years.Substring(0, years.Length - 1)

        Using writer As StreamWriter = New StreamWriter(path & "\sys\years_.csys", False)
            writer.Write(years)
        End Using

        LoadHistoryData()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnEnetSettings_Click(sender As Object, e As EventArgs) Handles btnEnetSettings.Click

        If Not grpEnetSettings.Visible Then
            grpEnetSettings.Visible = True
            pnlMachines.Location = New Drawing.Point(8, 235)
            pnlMachines.Size = New Drawing.Size(pnlMachines.Size.Width, pnlMachines.Size.Height - 225)
        Else
            grpEnetSettings.Visible = False
            pnlMachines.Location = New Drawing.Point(8, 5)
            pnlMachines.Size = New Drawing.Size(pnlMachines.Size.Width, pnlMachines.Size.Height + 225)
        End If

    End Sub

    Private Sub btnEnetCancelChanges_Click(sender As Object, e As EventArgs) Handles btnEnetCancelChanges.Click

        LoadEnetSettings()
    End Sub

#End Region



#Region "Machines connection setup"

    Private Sub gridviewMachines_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles gridviewMachines.CellEndEdit

        If gridviewMachines.Columns(e.ColumnIndex).HeaderText = "Source" Then

            CheckConnection(e)

        End If

        If gridviewMachines.Columns(e.ColumnIndex).HeaderText = "Machine Name" Then
            ' DataGridView1.Rows(e.RowIndex ).Cells("Machine").Value 
        End If

    End Sub

    Private Sub gridviewMachines_SelectionChanged(sender As Object, e As EventArgs) Handles gridviewMachines.SelectionChanged

        Dim grid As DataGridView = sender

        If grid.SelectedCells.Count > 0 Then

            lblMachineId.Text = gridviewMachines.Rows(grid.SelectedCells(0).RowIndex).Cells("Id").Value
            lblMachineName.Text = gridviewMachines.Rows(grid.SelectedCells(0).RowIndex).Cells("EnetMachineName").Value
            lblEnetPos.Text = ""
            lblDepartment.Text = ""
            lblProtocol.Text = ""
            lblFtpFileName.Text = ""
            lblEnetDashboard.Text = ""
            lblEnetMonList.Text = ""

            Dim machine = eNetServer.Machines.Find(Function(m) m.MachineName = lblMachineName.Text)

            If machine IsNot Nothing Then
                lblEnetPos.Text = machine.EnetPos
                lblDepartment.Text = machine.Department
                lblProtocol.Text = machine.Protocol
                lblFtpFileName.Text = machine.FTPFileName
                lblEnetDashboard.Text = IIf(machine.IsInMonList, "YES", "NOT")
                lblEnetMonList.Text = IIf(machine.IsInMonList, "YES", "NOT")
                lblCycleOnUdpCommand.Text = IIf(machine.Cmd_UDPCON Is Nothing, "", machine.Cmd_UDPCON)
            End If

        End If

    End Sub


    Private Sub chkEnetAllPositions_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnetAllPositions.CheckedChanged

        If chkEnetAllPositions.Checked And gridviewMachines.Rows.Count < 128 Then

            Dim id As Integer = 0
            Dim Label As String = ""
            Dim enetMach As String = ""

            Dim targetTable As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf")

            Dim noMonitoredMachines As List(Of eNetMachineConfig) = eNetServer.Machines.FindAll(Function(m) eNetServer.MonitoredMachines.Find(Function(m2) m2.MachineName = m.MachineName) Is Nothing)

            For Each machine As eNetMachineConfig In noMonitoredMachines

                Dim targetMachine As DataRow = targetTable.Select().Where(Function(m) m.Item("machine_name") = machine.MachineName).First()

                If targetMachine IsNot Nothing Then
                    id = Integer.Parse(targetMachine.Item("id").ToString())
                    Label = targetMachine.Item("machine_label").ToString()
                    enetMach = targetMachine.Item("EnetMachineName").ToString()
                End If

                Dim newRow As DataGridViewRow = New DataGridViewRow()
                newRow.CreateCells(gridviewMachines, id, False, machine.MachineName, enetMach, Label, "eNET", "check", "", 0, 0, 0, "False")
                newRow.DefaultCellStyle.ForeColor = Color.Gray
                newRow.DefaultCellStyle.BackColor = Color.LightGray
                newRow.DefaultCellStyle.SelectionBackColor = Color.LightCoral
                newRow.DefaultCellStyle.SelectionForeColor = Color.Black

                gridviewMachines.Rows.Add(newRow)
            Next
        Else

            Dim row As DataGridViewRow

            Do
                row = gridviewMachines.Rows.Cast(Of DataGridViewRow).Where(Function(r) Not Boolean.Parse(r.Cells("isMonitored").Value)).FirstOrDefault()

                If row IsNot Nothing Then
                    gridviewMachines.Rows.Remove(row)
                End If
            Loop While row IsNot Nothing

        End If

    End Sub


    Private Sub CheckConnection(e As DataGridViewCellEventArgs)

        If (Not IsNothing(gridviewMachines.Rows(e.RowIndex).Cells(3).Value)) Then

            Dim IPENET As String = ""

            If File.Exists(path & "\sys\Networkenet_.csys") Then
                Using reader As StreamReader = New StreamReader(path & "\sys\Networkenet_.csys")
                    IPENET = reader.ReadLine
                    reader.Close()
                End Using
            End If

            Dim dt As DataTable = clt.Run(IPENET)

            gridviewMachines.Rows(e.RowIndex).Cells(5).Value = "Check IP"
            gridviewMachines.Rows(e.RowIndex).Cells(5).Style.BackColor = Color.Red

            If dt IsNot Nothing Then
                For Each machine As DataRow In dt.Rows

                    gridviewMachines.Rows(e.RowIndex).Cells(5).Value = "offLine"
                    gridviewMachines.Rows(e.RowIndex).Cells(5).Style.BackColor = Color.Red

                    If (machine.Item("machine")) = gridviewMachines.Rows(e.RowIndex).Cells(1).Value Then

                        If machine.Item("status") <> "NO EMONITOR" Then

                            gridviewMachines.Rows(e.RowIndex).Cells(5).Style.BackColor = Color.GreenYellow
                            gridviewMachines.Rows(e.RowIndex).Cells(5).Value = "Passed : " & machine.Item("status").ToString()
                            Exit For
                        End If
                    End If
                Next
            End If
        End If

    End Sub

    '===================================================================================================
    ' CHECK THE CONNECTION
    '===================================================================================================
    Private Sub gridviewMachines_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles gridviewMachines.CellClick

        Try
            If (e.ColumnIndex = gridviewMachines.Columns("Check").Index And e.RowIndex >= 0) Then

                CheckConnection(e)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        '   https://www.youtube.com/watch?v=CHyESZfaxxE
    End Sub


    '===================================================================================================
    ' Ping ip:port or ip or uri
    '===================================================================================================
    Public Function mtconnect_ping(ip As String)

        Try
            Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(ip), HttpWebRequest)

            myHttpWebRequest.Timeout = 2000

            Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)

            If myHttpWebResponse.StatusCode = HttpStatusCode.OK Then
                Return True
            Else
                MessageBox.Show("CSIFLEX cannot connect to the MTConnect Agent : " & myHttpWebResponse.StatusDescription)
                Return False
            End If

        Catch ex As Exception
            If ex.Message = "The operation has timed out" Then
                Return False
            Else
                MessageBox.Show("CSIFLEX cannot reach the MTConnect Agent : " & ex.Message)
                Return False
            End If
        End Try

    End Function


    '===================================================================================================
    ' add a new row in the datagridview1
    '===================================================================================================
    'Public loadingmachines As Boolean = False


    'Private Sub datagridview1_newrow(sender As Object, e As DataGridViewRowsAddedEventArgs)

    '    If Not LoadingSources And Not loadingmachines Then
    '        'Last Row:
    '        gridviewMachines.Rows(gridviewMachines.Rows.Count - 1).Cells(3).Value = "Check"
    '    End If
    'End Sub


    '===================================================================================================
    '  Save the data
    '===================================================================================================
    Public Sub savedatagridview1()

        Using writer As StreamWriter = New StreamWriter(path & "\sys\machine_list_.csys", False)
            Dim display_CheckBox As String = ""

            For Each row As DataGridViewRow In gridviewMachines.Rows
                If Not IsNothing(row.Cells(1).Value) Then ' DONT TAKE AN EMPTY ROW
                    If row.Cells(0).Value = False Then
                        display_CheckBox = 0
                    Else
                        display_CheckBox = 1
                    End If
                    writer.WriteLine(row.Cells(1).Value.ToString() & "," & row.Cells(2).Value & "," & display_CheckBox.ToString())
                End If
            Next
            writer.Close()
        End Using
    End Sub


    Dim TargetsChanged As Boolean = False
    Dim originalEnetFolder As String = ""
    Dim originalEnetHttpIp As String = ""

    Private Sub TabControl1_select(sender As Object, e As EventArgs) Handles TabControl_DashBoard.SelectedIndexChanged

        If (TabControl_DashBoard.SelectedTab.Text) = "Sources" Then

            Load_groupes(CSI_Library.CSI_Library.MySqlConnectionString)
        End If

        If TabControl_DashBoard.SelectedTab.Text = "Users" Then

            Users_TV.Nodes(0).Nodes.Clear()
            Load_groupes(CSI_Library.CSI_Library.MySqlConnectionString)

            Dim dtUsers As DataTable = MySqlAccess.GetDataTable("SELECT * from CSI_auth.users ORDER BY UserType, UserName_")

            Dim userType = ""
            Dim userTypeNode As TreeNode

            For Each user As DataRow In dtUsers.Rows

                If userTypeNode Is Nothing Or Not user("UserType").ToString().FirstUpCase() = userType Then

                    userType = user("UserType").ToString().FirstUpCase()

                    If userTypeNode Is Nothing Then userTypeNode = New TreeNode(userType)

                    If userTypeNode.Nodes.Count > 0 Then
                        Users_TV.Nodes(0).Nodes.Add(userTypeNode)
                        userTypeNode = New TreeNode(userType)
                    End If
                End If

                userTypeNode.Nodes.Add(user("UserName_").ToString(), user("UserName_").ToString())
            Next

            If userTypeNode.Nodes.Count > 0 Then
                Users_TV.Nodes(0).Nodes.Add(userTypeNode)
                userTypeNode = New TreeNode()
            End If


            'For Each r As DataRow In dTable_message23.Rows
            '    Users_TV.Nodes(0).Nodes.Add(r(0).ToString())
            'Next
            Users_TV.ExpandAll()


            treeviewMachines.Nodes.Clear()
            treeviewMachines.Nodes.Add("All Machines")

            For Each row As DataGridViewRow In gridviewMachines.Rows

                Dim node_ As New TreeNode, node_0 As New TreeNode

                If Not IsNothing(row.Cells("machines").Value) And Boolean.Parse(row.Cells("isMonitored").Value) Then
                    node_.Tag = row.Cells("Id").Value
                    node_.Name = row.Cells("machines").Value.ToString()
                    node_.Text = row.Cells("machines").Value.ToString()

                    treeviewMachines.Nodes(0).Nodes.Add(node_)

                End If
            Next

            'add other groups
            For Each node In TV_GroupsOfMachines.Nodes
                treeviewMachines.Nodes.Add(node.clone)
            Next

            treeviewMachines.Visible = False

            GB_Users.Visible = False
            'This code selects the first element of User's tree
            Dim nodes As TreeNodeCollection = Users_TV.Nodes(0).Nodes
            If nodes.Count > 0 Then
                ' Select the root node
                Users_TV.SelectedNode = Users_TV.Nodes(0).Nodes(0)
            End If
        End If

        If TabControl_DashBoard.SelectedTab.Text <> "Servers" Then
            DONOT_refresh_mon_state__()
        End If

        If TabControl_DashBoard.SelectedTab.Text = "eNETDNC" Then

            LoadEnetSettings()

            Call Load_groupes(CSI_Library.CSI_Library.MySqlConnectionString)

            If TV_GroupsOfMachines.Nodes(0).Nodes.Count > 0 Then
                TV_GroupsOfMachines.Nodes(0).Expand()
            End If

        End If

        If Not (TabControl_DashBoard.SelectedTab.Text = "eNETDNC") And (TargetsChanged = True) And (Comput_perf_required = False) Then
            TargetsChanged = False
            UpdateTargetsTitle()
            RefreshAllDevices()
        End If

        If Not (TabControl_DashBoard.SelectedTab.Text = "eNETDNC") And (Comput_perf_required = True) Then
            ComputePerfReq()
            Comput_perf_required = False
        End If

        If TabControl_DashBoard.SelectedTab.Text = "Dashboards" Then

            RemoveHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck

            load_DeviceName()

            Call Load_groupes(CSI_Library.CSI_Library.MySqlConnectionString)

            Try
                If (treeviewDevices.SelectedNode.Text = treeviewDevices.Nodes(0).Text Or treeviewDevices.SelectedNode.Text = Nothing) Then
                    Panel_dashConfig.Visible = False
                    TV_LivestatusMachine.Visible = False
                End If
            Catch ex As Exception
                Panel_dashConfig.Visible = False
                TV_LivestatusMachine.Visible = False
            End Try

            TV_LivestatusMachine.Nodes.Clear()

            'add All Machines node
            TV_LivestatusMachine.Nodes.Add("All machines")

            For Each row As DataGridViewRow In gridviewMachines.Rows
                'add machine
                Dim node_ As New TreeNode, node_0 As New TreeNode
                If Not IsNothing(row.Cells("machines").Value) And Boolean.Parse(row.Cells("isMonitored").Value) Then
                    node_.Name = row.Cells("machines").Value.ToString()
                    node_.Text = row.Cells("machines").Value.ToString()
                    node_.Tag = row.Cells("Id").Value.ToString()
                    TV_LivestatusMachine.Nodes(0).Nodes.Add(node_)
                End If
            Next

            'add other groups
            For Each node In TV_GroupsOfMachines.Nodes
                TV_LivestatusMachine.Nodes.Add(node.clone)
            Next

            AddHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck

            Dim nodes As TreeNodeCollection = treeviewDevices.Nodes(0).Nodes

            If nodes.Count > 0 Then
                ' Select the root node
                treeviewDevices.SelectedNode = treeviewDevices.Nodes(0).Nodes(0)
                configisloading = True

                load_userConfig(CSI_Library.CSI_Library.MySqlConnectionString)
                configisloading = False
            End If

        End If

        If TabControl_DashBoard.SelectedTab.Text = "About/License" Then

            Dim sb = New System.Text.StringBuilder()
            sb.Append("{\rtf1\ansi")
            sb.Append("---- \b Version 1.9.2.22\b0  :  2021 February 08 \line \line ")
            sb.Append("\b Fixe :\b0  Fixed issues with the support for Monitoring Units \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.21\b0  :  2021 January 29 \line \line ")
            sb.Append("\b Fixe :\b0  Fixed issues with the support for Monitoring Units \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.19\b0  :  2021 January 11 \line \line ")
            sb.Append("\b Improvement :\b0  Implemented the support for Monitoring Units \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.18\b0  :  2021 January 08 \line \line ")
            sb.Append("\b Improvement :\b0  Implemented the support for Monitoring Units \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.16\b0  :  2020 October 26 \line \line ")
            sb.Append("\b Fixe :\b0  Fixed issues with connectors \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.15\b0  :  2020 September 22 \line \line ")
            sb.Append("\b Improvement :\b0  Created the property Machine Name that can be different of the eNETDNC Machine Name \b0 . \line \line ")
            sb.Append("\b Improvement :\b0  It's possible select if the dashboard will show the Machine Name, Label, or eNETDNC Machine Name \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.14\b0  :  2020 August 23 \line \line ")
            sb.Append("\b Fixe :\b0  Fixed issue with logs files \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.13\b0  :  2020 August 20 \line \line ")
            sb.Append("\b Fixe :\b0  Fixed issue with logs files \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.12\b0  :  2020 July 16 \line \line ")
            sb.Append("\b Fixe :\b0  Fixed issue with installation of MYSQL service \b0 . \line \line ")
            sb.Append("\b Fixe :\b0  Fixed issue with initialization of the database \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.11\b0  :  2020 July 07 \line \line ")
            sb.Append("\b Fixe :\b0  Fixed issue with dashboard timeline \b0 . \line \line ")
            sb.Append("\b Fixe :\b0  Fixed issue when add machines connections \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.10\b0  :  2020 May 28 \line \line ")
            sb.Append("\b Fixe :\b0  Fixed issue with Downtime reports \b0 . \line \line ")
            sb.Append("\b Improvement :\b0  Created views of operator table on database \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.9\b0  :  2020 June 04 \line \line ")
            sb.Append("\b Improvement :\b0  Created views of machines' tables on database \b0 . \line \line ")
            sb.Append("\b Improvement :\b0  Created user to access the views \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.8\b0  :  2020 May 28 \line \line ")
            sb.Append("\b Fixe :\b0  Fixed issues saving Part Number info in the database \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.6\b0  :  2020 April 20 \line \line ")
            sb.Append("\b Fixe :\b0  Rules do status CYCLE OFF in the connectors \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.5\b0  :  2020 April 10 \line \line ")
            sb.Append("\b Fixe :\b0  Rules do status changes in the connectors \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.4\b0  :  2020 March 23 \line \line ")
            sb.Append("\b Improvement :\b0  Health monitoring report \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.3\b0  :  2020 March 19 \line \line ")
            sb.Append("\b Improvement :\b0  Improvement CSIFLEX to don't use the eNETDNC dashboard \b0 . \line \line ")
            sb.Append("\b Improvement :\b0  User Mobile for Mobile App \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.2\b0  :  2020 March 11 \line \line ")
            sb.Append("\b Improvement :\b0  Improvement CSIFLEX to don't use the eNETDNC dashboard \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.1\b0  :  2020 March 06 \line \line ")
            sb.Append("\b Improvement :\b0  Improvements for CSIFLEX Mobile \b0 . \line \line ")
            sb.Append("\b Improvement :\b0  Restart the Reporting Service when it breaks \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.0\b0  :  2020 February 25 \line \line ")
            sb.Append("\b Improvement :\b0  Improvement in the UDP communication  \b0 . \line \line ")
            sb.Append("\b Fixe :\b0  Fixed problems with MTC/Focas Connectors settings  \b0 . \line \line ")
            sb.Append("\b Fixe :\b0  Fixed problems with Users settings  \b0 . \line \line ")
            sb.Append("\b Fixe :\b0  Fixed problem with the duplicity of machines in eNET  \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.53\b0  :  2020 February 06 \line \line ")
            sb.Append("\b Fixe :\b0  Problems with 'Configure reports' form  \b0 . \line \line ")
            sb.Append("\b Fixe :\b0  Problems with 'eNETDNC' form  \b0 . \line \line ")
            sb.Append("\b Fixe :\b0  Problems with Dashboard's pie chart and targets  \b0 . \line \line ")
            sb.Append("\b Fixe :\b0  Problems with initialization of the CSIFLEX service  \b0 . \line \line ")
            sb.Append("\b Fixe :\b0  Some issues in the Reporting service  \b0 . \line \line ")
            sb.Append("\b Fixe :\b0  New CSIFLEX Logo  \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.52\b0  :  2020 January 29 \line \line ")
            sb.Append("\b New :\b0  CSI Connector - Function to control and update the adapters services. \b0 . \line \line ")
            sb.Append("\b Fixe :\b0  Duplication of dashboard devices  \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.51\b0  :  2020 January 16 \line \line ")
            sb.Append("\b New :\b0  New adapter that allows recovering 'Parts Required' and 'Part Count' from FOCAS machines to show in the dashboards. \b0 . \line \line ")
            sb.Append("\b New :\b0  The changes include: \b0 . \line \line ")
            sb.Append("\b New :\b0   - Mapping the fields from the adapter in the 'Condition Editor' form. \b0 . \line \line ")
            sb.Append("\b New :\b0   - Setting in the dashboards settings the format with that the information will be showing in the dashboard. \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.50\b0  :  2020 January 09 \line \line ")
            sb.Append("\b New :\b0  Report feature - Allow selecting the reports to generate automatically at any time. \b0 . \line \line ")
            sb.Append("\b New :\b0  New mechanism for event and error logging. \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.49\b0  :  2020 January 02 \line \line ")
            sb.Append("\b Fixe :\b0  Report improvements  \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.48\b0  :  2019 December 20 \line \line ")
            sb.Append("\b Fixe :\b0  Report improvements  \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.47\b0  :  2019 December 16 \line \line ")
            sb.Append("\b Fixe :\b0  Report improvements  \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.46\b0  :  2019 December 09 \line \line ")
            sb.Append("\b Fixe :\b0  Dashboard improvements  \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.45\b0  :  2019 December 03 \line \line ")
            sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.43\b0  :  2019 November 27 \line \line ")
            sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.42\b0  :  2019 November 21 \line \line ")
            sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.41\b0  :  2019 November 18 \line \line ")
            sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.1.40\b0  :  2019 November 12 \line \line ")
            sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.39\b0  :  2019 November 04 \line \line ")
            'sb.Append("\b Fixe :\b0  Reports and CSV file loading fixes  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.38\b0  :  2019 October 21 \line \line ")
            'sb.Append("\b Fixe :\b0  License fixes  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.37\b0  :  2019 October 16 \line \line ")
            'sb.Append("\b Fixe :\b0  Timeline fixes  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.36\b0  :  2019 September 24 \line \line ")
            'sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.35\b0  :  2019 September 24 \line \line ")
            'sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.34\b0  :  2019 September 24 \line \line ")
            'sb.Append("\b Fixe :\b0  New Downtime Consolidated Report  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.33\b0  :  2019 September 16 \line \line ")
            'sb.Append("\b Fixe :\b0  New Downtime Report  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.32\b0  :  2019 September 16 \line \line ")
            'sb.Append("\b Fixe :\b0  New Downtime Report  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.31\b0  :  2019 August 29 \line \line ")
            'sb.Append("\b Fixe :\b0  New service of weather information  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.30\b0  :  2019 August 26 \line \line ")
            'sb.Append("\b Fixe :\b0  New settings for dashboard  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.29\b0  :  2019 August 20 \line \line ")
            'sb.Append("\b Fixe :\b0  New layout  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.28\b0  :  2019 July 29 \line \line ")
            'sb.Append("\b Fixe :\b0  New license system  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.27\b0  :  2019 July 24 \line \line ")
            'sb.Append("\b Fixe :\b0  Dashboard and database improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.26\b0  :  2019 July 19 \line \line ")
            'sb.Append("\b Fixe :\b0  Timeline bar resizable  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.25\b0  :  2019 July 2 \line \line ")
            'sb.Append("\b Fixe :\b0  Auto Reporting. New service  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.24\b0  :  2019 April  10 \line \line ")
            'sb.Append("\b Fixe :\b0  Dashboard fixes  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.23\b0  :  2019 April  6 \line \line ")
            'sb.Append("\b Fixe :\b0  Auto Reporting  \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  New User Types support: Operator a Supervisor\b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.22\b0  :  2019 March  10 \line \line ")
            'sb.Append("\b Fixe :\b0  email supports non ssl  \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  fix report generation \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.21\b0  :  2019 January  29 \line \line ")
            'sb.Append("\b Fixe :\b0  Single Agent per Adapter \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Focas Libraries Updated \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.20\b0  :  2018 November  30 \line \line ")
            'sb.Append("\b Fixe :\b0  F2(Rename) and Delete Buttons' Functionality added to Add/Delete Groups,Users and Dashboards \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Database Optimization \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  MTConnect/Focas Machine Management updated. \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Automated Installation\b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Machine List Update without restarting the application\b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Machines delete working successfully\b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed few Bugs\b0. \line \line ")

            'sb.Append("--- \b Version 1.8.6.11\b0  :  2017 June 02 \line \line ")
            'sb.Append("\b Fixe :\b0  Stability fixe with the dashboards configuration\b0 . \line \line ")

            'sb.Append("--- \b Version 1.8.6.10\b0  :  2017 May 17 \line \line ")
            'sb.Append("\b New :\b0  Display the targets in the dashboard\b0 . \line \line ")
            'sb.Append("\b New :\b0  Display the machine utilization by groups in the dashboard\b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Stability fixe. \line \line ")

            'sb.Append("--- \b Version 1.8.6.9\b0  :  2016 Dec 20 \line \line ")
            'sb.Append("\b Fixe : \b0 Status Notification delay not working. \line \line ")
            'sb.Append("\b Fixe : \b0 Status Notification delay save. \line \line ")
            'sb.Append("\b Fixe : \b0 Update Button. \line \line ")
            'sb.Append("\b Fixe : \b0 Stability fixe. \line \line ")

            'sb.Append("--- \b Version 1.8.6.8\b0  :  2016 Nov 25 \line \line ")
            'sb.Append("\b Fixe : \b0 Interface update. \line \line ")
            'sb.Append("\b New : \b0  CSIFlex will send the status 'MCH UNREACHABLE' if an MTConnect Machine does not respond after 2sec.  \line \line ")
            'sb.Append("\b Fixe : \b0  MTConnect Library stability fixe \line \line ")
            'sb.Append("\b Fixe : \b0  MTConnect connection TimeOut fixe \line \line ")
            'sb.Append("\b Fixe : \b0  The PartNumbers with MTConnect  \line \line ")
            'sb.Append("\b Fixe : \b0 Connect a device with the MAC address  \line \line ")
            'sb.Append("\b Fixe : \b0  Stability fixe with the Utilization computing  \line \line ")
            'sb.Append("\b Fixe : \b0  Performances.  \line \line ")

            'sb.Append("--- \b Version 1.8.6.7\b0  :  2016 Aug 26 \line \line ")
            'sb.Append("\b New : \b0 CSIFlex works now without wamp. \line \line ")
            'sb.Append("\b New : \b0 Embedded web server more resource efficient. \line \line ")
            'sb.Append("\b New : \b0 A new integrated web dashboard. \line \line ")
            'sb.Append("\b Fixe : \b0 New architecture to generate the automated reports. \line \line ")
            'sb.Append("\b Fixe : \b0 Stability. \line \line ")

            'sb.Append("--- \b Version 1.8.6.6\b0  :  2016 may 31 \line \line ")
            'sb.Append("\b New : \b0  The Advanced MTConnect/Focas Editor, to define status, conditions and notifications with mtconnect/Focas machines. \line \line ")
            'sb.Append("\b New : \b0  Remember Login/password. CSIFlex will store one login/password per windows username. \line \line ")
            'sb.Append("\b New : \b0  Test email button in the email server config window. \line \line ")
            'sb.Append("\b Fixe : \b0  Stability improvements, bug fixes. \line \line ")

            'sb.Append("--- \b Version 1.8.6.5\b0  :  2016 Apr 04 \line \line ")
            'sb.Append("\b Fixe : \b0  Stability improvements, bug fixes. \line \line ")
            'sb.Append("\b New : \b0  This 'What's new' window. \line \line ")

            sb.Append("}")
            RTB_liste.Rtf = sb.ToString()

        End If

        'If TabControl_DashBoard.SelectedTab.Tag = "Timeline" Then

        '    If TabControl_DashBoard.SelectedTab.Controls.Count = 0 Then

        '        timelineEditControl = New TimelineEditControl()
        '        TabControl_DashBoard.SelectedTab.Controls.Add(timelineEditControl)

        '        timelineEditControl.Location = New Drawing.Point(8, 3)
        '        timelineEditControl.Width = TabControl_DashBoard.SelectedTab.Width - 16
        '        timelineEditControl.Height = TabControl_DashBoard.SelectedTab.Height - 6
        '        timelineEditControl.Anchor = AnchorStyles.Bottom Or AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        '    Else

        '        timelineEditControl.Reset()

        '    End If

        'End If

        If TabControl_DashBoard.SelectedTab.Text = "CSI Connector" Then

            'btnMonitoringUnits.Enabled = CBool(MySqlAccess.ExecuteScalar("SELECT EXISTS(SELECT * FROM csi_auth.tbl_monitoringunits);"))

        End If

    End Sub


    Public Sub load_DeviceName()

        Dim dTable_name = MySqlAccess.GetDataTable("SELECT deviceId, name FROM csi_database.tbl_deviceconfig")

        treeviewDevices.Nodes.Item(0).Nodes.Clear()
        For Each row As DataRow In dTable_name.Rows
            treeviewDevices.Nodes.Item(0).Nodes.Add(row.Item(0).ToString(), row.Item(1).ToString())
        Next

    End Sub

    Private Sub addmenu(text As String)
        Dim ToolStripMenuItem As New ToolStripMenuItem(text)

        ToolStripMenuItem.Text = text

    End Sub

    '===================================================================================================
    ' Fill the datagridview 
    '===================================================================================================
    Public Sub LoadGridviewMachines()

        'Dim currentrow As String()
        Dim id As Integer = 0
        Dim machineName As String = ""
        Dim readed As String = ""
        Dim label As String = ""
        Dim enetMach As String = ""
        Dim chked As New DataGridViewCheckBoxCell

        chked.Value = False
        gridviewMachines.Rows.Clear()

        If Not eNetServer.eNetLoaded Then Return

        Dim targetTable As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf")

        Dim monitoredMachines As List(Of eNetMachineConfig) = eNetServer.MonitoredMachines

        For Each machine As eNetMachineConfig In monitoredMachines

            Dim DailyTarget As Integer = 0
            Dim Weeklytarget As Integer = 0
            Dim MonthlyTarget As Integer = 0

            Dim targetMachine As DataRow = targetTable.Select().Where(Function(m) m.Item("EnetMachineName") = machine.MachineName).First()

            If targetMachine IsNot Nothing Then
                machineName = targetMachine.Item("Machine_Name").ToString()
                id = Integer.Parse(targetMachine.Item("id").ToString())
                label = targetMachine.Item("machine_label").ToString()
                enetMach = targetMachine.Item("EnetMachineName").ToString()
                Weeklytarget = Convert.ToInt32(targetMachine.Item("MCH_WeeklyTarget").ToString())
                MonthlyTarget = Convert.ToInt32(targetMachine.Item("MCH_MonthlyTarget").ToString())
            End If

            gridviewMachines.Rows.Add(id, False, machineName, enetMach, label, "eNET", "check", "", DailyTarget, Weeklytarget, MonthlyTarget, "True")
            gridviewMachines.Sort(gridviewMachines.Columns(1), ListSortDirection.Ascending)
        Next

        Dim qtt = gridviewMachines.Rows.Count
        lblQttMachines.Text = $"{qtt} Machine{IIf(qtt > 1, "s", "")}"

        chkEnetAllPositions_CheckedChanged(Me, EventArgs.Empty)

        Return
    End Sub

    Public Sub GetAllENETMachines() 'This Function is used to load Ehub Config Files in Database 

        Dim StringMonId As String = ""

        Try

            Dim dTable_CheckTableisEmpty = MySqlAccess.GetDataTable("SELECT count(*) as Count FROM csi_auth.tbl_ehub_conf;")

            Dim IsInsert As Boolean = False

            IsInsert = (dTable_CheckTableisEmpty.Rows.Item(0).Item("Count") = 0)

            Dim ehubfilepath As String = ""
            Dim Monsetupfilepath As String = ""
            Dim allconst As New AllStringConstants.StringConstant

            ehubfilepath = allconst.SERVER_ENET_PATH & allconst.EHUB_CONF_FILE_NAME
            Monsetupfilepath = allconst.SERVER_ENET_PATH & allconst.MON_SETUP_FILE_NAME

            If File.Exists(ehubfilepath) And File.Exists(Monsetupfilepath) Then

                Dim fileData() As String = File.ReadAllLines(ehubfilepath) 'Read All Lines from EhubConf file
                Dim MonData() As String = File.ReadAllLines(Monsetupfilepath) 'Read All Lines from MonSetUp file

                Dim splitInput As New List(Of String)
                Dim splitMonData As New List(Of String)
                Dim machines As New List(Of MachineEhub)

                Dim machine As New MachineEhub()
                Dim nextLine As Boolean = False

                For Each line As String In fileData
                    If nextLine Then
                        machine.MachinePosition = line.Replace(":", "")
                        machine.MachineLabel = $"MCH_{machine.MachinePosition.Replace(",", "")}"
                        nextLine = False

                    ElseIf line.Contains("NM:") Then
                        splitInput.Add(line.Replace("NM:", "")) 'Name of the ENET Machine
                        machine.MachineName = line.Replace("NM:", "")

                    ElseIf line.Contains("FI:") Then
                        splitInput.Add(line.Replace("FI:", "")) 'Connection Type_ This value doesn't define all parameters but when it's 1 then it indicates FTP Connection
                        machine.IsFtpConnection = (line.Replace("FI:", "").Trim() = 1)
                        machine.ConnectionType = If(machine.IsFtpConnection, 5, 0)

                        machines.Add(machine)
                        machine = New MachineEhub()

                    End If

                    If line.Trim() = "" Then
                        nextLine = True
                    End If
                Next

                nextLine = False
                For Each line As String In MonData

                    If nextLine Then
                        machine = machines.Find(Function(m) m.MachinePosition = line.Replace(":", ""))
                        nextLine = False
                        If Not machine Is Nothing Then StringMonId = $"Mon {machine.MachinePosition}"

                    ElseIf line.Contains("ON:") Then 'Machine Monitoring Status If 1 then Monitoring is ON IF 0 then Monitoring is OFF
                        splitMonData.Add(line.Replace("ON:", ""))
                        machine.IsMonitoring = (line.Replace("ON:", "") = 1)

                    ElseIf line.Contains("TH:") Then
                        splitMonData.Add(line.Split(":")(1)) 'Stores the TH Parameter Values
                        machine.ThParameter = line.Split(":")(1)

                    ElseIf line.Contains("DD:") Then
                        Dim FileFTPName As String = line.Split(",")(1)
                        splitMonData.Add(FileFTPName)
                        machine.FtpFileName = FileFTPName

                    ElseIf line.Contains("DA:") Then
                        splitMonData.Add(line.Split(":")(1))
                        machine.DaParameter = line.Split(":")(1)

                    End If

                    If line.Trim() = "" Then
                        nextLine = True
                    End If
                Next

                Dim fileLength As Integer = splitInput.Count
                Dim MonFilelength As Integer = splitMonData.Count
                Dim k As Integer, l As Integer, p As Integer, q As Integer, querycount As Integer, Counter As Integer, r As Integer
                Dim MonitoringId As String = ""
                Dim MachineName As String = ""
                Dim Machine_Filename As String = ""
                Dim MachineLabel As String = ""
                Dim TH_Value As Integer
                Dim Monstate As Integer
                Dim FTPFileName As String
                Dim EnetDept As String
                'Dim MonSetUpId As Integer, 
                Dim Con_type As Integer

                k = 1
                l = 1
                p = 0
                q = 1
                r = 0
                querycount = 0
                Counter = 0

                While q < fileLength

                    If k <= 16 And l <= 8 Then

                        MonitoringId = Convert.ToString(k) + "," + Convert.ToString(l)
                        MachineLabel = "MCH_" + Convert.ToString(k) + Convert.ToString(l)
                        Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & ".SYS_"
                        StringMonId = Convert.ToString(MonitoringId)
                        MachineName = Convert.ToString(splitInput(p))

                        'MonSetUpId = 0
                        If (Convert.ToInt32(splitInput(q))) = 1 Then 'Machines whose (FI:1) For future it will be called as Connection Type 5 where  (FI:1,EH:0 and EO:0) 
                            Con_type = 5
                        Else
                            Con_type = 0
                        End If

                        Monstate = Convert.ToInt32(splitMonData(r))
                        TH_Value = Convert.ToInt32(splitMonData(r + 1)) 'Represent TH value
                        FTPFileName = splitMonData(r + 2).ToString()
                        EnetDept = splitMonData(r + 3).ToString()

                        If TH_Value = 1 Then
                            '2 Head MonitorData010.SYS_,MonitorData011.SYS_
                            Machine_Filename = $"MonitorData{ (k - 1).ToString("x") & Convert.ToString(l - 1) }0.SYS_,MonitorData{ (k - 1).ToString("x") & Convert.ToString(l - 1) }1.SYS_"

                        ElseIf TH_Value = 2 Then
                            '2 Pallet MonitorData010.SYS_,MonitorData011.SYS_
                            Machine_Filename = $"MonitorData{ (k - 1).ToString("x") & Convert.ToString(l - 1) }0.SYS_,MonitorData{ (k - 1).ToString("x") & Convert.ToString(l - 1) }1.SYS_"

                        Else
                            Machine_Filename = $"MonitorData{ (k - 1).ToString("x") & Convert.ToString(l - 1) }.SYS_"

                        End If

                        Dim sqlCmd As New Text.StringBuilder()

                        If IsInsert Then
                            sqlCmd.Append($"INSERT INTO csi_auth.tbl_ehub_conf ")
                            sqlCmd.Append($" (                                 ")
                            sqlCmd.Append($"    monitoring_id      ,           ")
                            sqlCmd.Append($"    machine_name       ,           ")
                            sqlCmd.Append($"    EnetMachineName    ,           ")
                            sqlCmd.Append($"    Con_type           ,           ")
                            sqlCmd.Append($"    monitoring_filename,           ")
                            sqlCmd.Append($"    Monstate           ,           ")
                            sqlCmd.Append($"    machine_label      ,           ")
                            sqlCmd.Append($"    ftpfilename        ,           ")
                            sqlCmd.Append($"    CurrentStatus      ,           ")
                            sqlCmd.Append($"    CurrentPartNumber  ,           ")
                            sqlCmd.Append($"    EnetDept           ,           ")
                            sqlCmd.Append($"    TH_State                       ")
                            sqlCmd.Append($" )                                 ")
                            sqlCmd.Append($" VALUES                            ")
                            sqlCmd.Append($" (                                 ")
                            sqlCmd.Append($"    '{StringMonId}'     ,          ")
                            sqlCmd.Append($"    '{MachineName}'     ,          ")
                            sqlCmd.Append($"    '{MachineName}'     ,          ")
                            sqlCmd.Append($"    '{Con_type}'        ,          ")
                            sqlCmd.Append($"    '{Machine_Filename}',          ")
                            sqlCmd.Append($"    '{Monstate}'        ,          ")
                            sqlCmd.Append($"    '{MachineLabel}'    ,          ")
                            sqlCmd.Append($"    '{FTPFileName}'     ,          ")
                            sqlCmd.Append($"    ''                  ,          ")
                            sqlCmd.Append($"    ''                  ,          ")
                            sqlCmd.Append($"    '{EnetDept}'        ,          ")
                            sqlCmd.Append($"    '{TH_Value}'                   ")
                            sqlCmd.Append($" )                                 ")

                        Else
                            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_ehub_conf               ")
                            sqlCmd.Append($"  SET                                              ")
                            sqlCmd.Append($"      EnetMachineName     = '{ MachineName }'     ,")
                            sqlCmd.Append($"      Con_type            = '{ Con_type }'        ,")
                            sqlCmd.Append($"      monitoring_filename = '{ Machine_Filename }',")
                            sqlCmd.Append($"      Monstate            = '{ Monstate }'        ,")
                            sqlCmd.Append($"      ftpfilename         = '{ FTPFileName }'     ,")
                            sqlCmd.Append($"      EnetDept            = '{ EnetDept }'        ,")
                            sqlCmd.Append($"      TH_State            = '{ TH_Value }'         ")
                            sqlCmd.Append($"  WHERE                                            ")
                            sqlCmd.Append($"      monitoring_id       = '{ MonitoringId }'     ")

                        End If

                        querycount = MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                        If querycount > 1 Then
                            Counter += 1
                        End If

                        l += 1
                        p += 2
                        q += 2
                        r += 4

                        If l > 8 Then
                            l = 1
                            k += 1
                        End If
                    End If

                End While

            Else
                MessageBox.Show("EhubConf.sys Or MonSetUp.sys file is not exists or you don't have authority to access it")
            End If

        Catch ex As Exception
            MessageBox.Show($"EhubConf Table has error while inserting data ({ StringMonId }): " & ex.Message)
        End Try

    End Sub


    Dim setSortOrder As ListSortDirection
    Dim idSortColumn As Integer = -1

    Public Sub Load_DGV_CSIConnector()

        Try
            If idSortColumn < 0 Then idSortColumn = 0

            Select Case grvConnectors.SortOrder
                Case SortOrder.Ascending
                    setSortOrder = ListSortDirection.Ascending
                Case SortOrder.Descending
                    setSortOrder = ListSortDirection.Descending
                Case SortOrder.None
                    setSortOrder = ListSortDirection.Ascending
            End Select

            grvConnectors.DataSource = Nothing

            Dim sqlCmd As Text.StringBuilder = New Text.StringBuilder()

            sqlCmd.Append("SELECT                        ")
            sqlCmd.Append("    MachineName             , ")
            sqlCmd.Append("    MachineIP               , ")
            sqlCmd.Append("    MTCMachine              , ")
            sqlCmd.Append("    ConnectorType           , ")
            sqlCmd.Append("    eNETMachineName         , ")
            sqlCmd.Append("    FocasPort               , ")
            sqlCmd.Append("    ControllerType          , ")
            sqlCmd.Append("    AgentIP                 , ")
            sqlCmd.Append("    AgentPort               , ")
            sqlCmd.Append("    Manufacturer            , ")
            sqlCmd.Append("    AdapterPort             , ")
            sqlCmd.Append("    Id                        ")
            sqlCmd.Append("FROM                          ")
            sqlCmd.Append("    CSI_auth.tbl_CSIConnector ")

            Dim results As DataTable = MySqlAccess.GetDataTable(sqlCmd.ToString())

            grvConnectors.DataSource = results
            grvConnectors.Columns(0).HeaderCell.Value = "Machine Name"
            grvConnectors.Columns(1).HeaderCell.Value = "Machine IP"
            grvConnectors.Columns(2).HeaderCell.Value = "MTC Machine"
            grvConnectors.Columns(3).HeaderCell.Value = "Connection Type"
            grvConnectors.Columns(4).HeaderCell.Value = "eNET Machine Name"
            grvConnectors.Columns(5).HeaderCell.Value = "Focas Port"
            grvConnectors.Columns(6).HeaderCell.Value = "Controller Type"
            grvConnectors.Columns(7).HeaderCell.Value = "Agent Machine IP"
            grvConnectors.Columns(8).HeaderCell.Value = "Agent Machine Port"
            grvConnectors.Columns(9).HeaderCell.Value = "Manufacturer"
            grvConnectors.Columns(10).HeaderCell.Value = "AdapterPort"
            grvConnectors.Columns(11).HeaderCell.Value = "Id"
            grvConnectors.Columns(5).Visible = False
            grvConnectors.Columns(6).Visible = False
            grvConnectors.Columns(7).Visible = True
            grvConnectors.Columns(8).Visible = False
            grvConnectors.Columns(9).Visible = False
            grvConnectors.Columns(10).Visible = False
            grvConnectors.Columns(11).Visible = False

            grvConnectors.Sort(grvConnectors.Columns(idSortColumn), setSortOrder)


        Catch ex As Exception
            MessageBox.Show("Error :::" + ex.Message)
        End Try

    End Sub

    'Private Sub grvConnectors_ColumnSortModeChanged(sender As Object, e As EventArgs) Handles grvConnectors.Sorted

    '    idSortColumn = grvConnectors.SortedColumn.Index

    'End Sub


#End Region


#Region "add eNET source"

    '===================================================================================================
    ' OK button to add an eNET connection an save the path
    '===================================================================================================
    'Private Sub Button22_Click(sender As Object, e As EventArgs)
    '    Try
    '        Form1.chemin_eNET = Edit_eNET.TB_EnetPath.Text 'TextBox5.Text
    '        Using writer As StreamWriter = New StreamWriter(path & "\sys\setup_.csys")
    '            writer.Write(Edit_eNET.TB_EnetPath.Text)
    '            writer.Close()
    '        End Using

    '        Using writer As StreamWriter = New StreamWriter(path & "\sys\Networkenet_.csys", False)
    '            writer.Write(Edit_eNET.TB_EnetIp.Text)
    '            writer.Close()
    '        End Using

    '        Me.gridviewMachines.Visible = True
    '        'Panel2.Visible = False
    '        Edit_eNET.Hide()
    '    Catch ex As Exception
    '        MessageBox.Show("Unable to save the eNET path : " & ex.Message)
    '    End Try
    'End Sub

#End Region


#Region "DataGridView2"

    ' Save datagridview2 button
    '===================================================================================================

    Private Sub Button20_Click(sender As Object, e As EventArgs)

    End Sub

    ''' <summary>
    ''' AES_Enc
    ''' </summary>
    ''' <returns>Encrypted </returns>

    Public Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            AES.Padding = System.Security.Cryptography.PaddingMode.Zeros
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
            Return Nothing

        End Try
    End Function

#End Region


#Region "Right clic/Context menu"

    '===================================================================================================
    ' Right clic on datagridview1 , add 'add to ' in menu
    '===================================================================================================

    Private Sub gridviewMachines_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gridviewMachines.MouseUp

        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim hti As DataGridView.HitTestInfo = gridviewMachines.HitTest(e.X, e.Y)

            If gridviewMachines.SelectedCells(0).RowIndex <> hti.RowIndex Then
                'DGV_Sources.ClearSelection()
                gridviewMachines.CurrentCell = gridviewMachines.Rows(hti.RowIndex).Cells(hti.ColumnIndex)
            End If

            If gridviewMachines.SelectedCells.Count > 0 And Boolean.Parse(gridviewMachines.Rows(hti.RowIndex).Cells("IsMonitored").Value) Then
                add_menu.DropDownItems.Clear()
                For Each node As TreeNode In TV_GroupsOfMachines.Nodes(0).Nodes
                    Dim item_ As New ToolStripMenuItem(node.Text, Nothing, AddressOf AddMachineToGroup, node.Text)
                    item_.Text = node.Text
                    ' ContextMenuStrip1.Items
                    ' ContextMenuStrip1.Items.Add(item_)
                    If Not add_menu.DropDownItems.ContainsKey(item_.Text) Then add_menu.DropDownItems.Add(item_)
                Next
                CMS_GridEdit.Show(gridviewMachines, New System.Drawing.Point(e.X, e.Y), ToolStripDropDownDirection.Default)
            End If
        End If

    End Sub

    Public Sub AddMachineToGroup(ByVal sender As Object, ByVal e As EventArgs)

        Dim rowIndex = gridviewMachines.SelectedCells(0).RowIndex

        If gridviewMachines.SelectedCells.Count > 0 And Boolean.Parse(gridviewMachines.Rows(rowIndex).Cells("IsMonitored").Value) Then
            Dim alist As New List(Of Integer)
            For Each CELL_ As DataGridViewCell In gridviewMachines.SelectedCells
                If Not alist.Contains(CELL_.RowIndex) Then
                    alist.Add(CELL_.RowIndex)


                    Dim row_ As DataGridViewRow = gridviewMachines.Rows.Item(CELL_.RowIndex)

                    Dim node_ As New TreeNode, node_0 As New TreeNode
                    If Not IsNothing(row_.Cells.Item("Machines").Value) Then

                        node_.Name = row_.Cells.Item("Machines").Value.ToString()
                        node_.Text = row_.Cells.Item("Machines").Value.ToString()
                        node_.Tag = row_.Cells.Item("Id").Value.ToString()

                        Dim g As String = CType(sender, ToolStripMenuItem).Text

                        For Each n As TreeNode In TV_GroupsOfMachines.Nodes(0).Nodes
                            For Each n2 As TreeNode In n.Nodes
                                If n.Text = g Then
                                    If (n2.Text = node_.Text) Then
                                        GoTo end_
                                    End If
                                End If
                            Next
                        Next

                        Dim treeNode As New TreeNode
                        For Each n As TreeNode In TV_GroupsOfMachines.Nodes(0).Nodes
                            If (n.Text = CType(sender, ToolStripMenuItem).Text) Then
                                treeNode = n
                            End If
                        Next

                        If (treeNode.Text.Length > 0) Then
                            TV_GroupsOfMachines.SelectedNode = treeNode
                            If Not (TV_GroupsOfMachines.Nodes(0).Nodes(treeNode.Index).Nodes.ContainsKey(node_.Name)) Then
                                TV_GroupsOfMachines.Nodes(0).Nodes(treeNode.Index).Nodes.Add(node_)
                            End If

                            Dim cmd As Text.StringBuilder = New Text.StringBuilder()
                            cmd.Append($"INSERT IGNORE INTO CSI_Database.tbl_Groups ")
                            cmd.Append($"(                                          ")
                            cmd.Append($"   `users`                               , ")
                            cmd.Append($"   `groups`                              , ")
                            cmd.Append($"   `machines`                            , ")
                            cmd.Append($"   `MachineId`                             ")
                            cmd.Append($")                                          ")
                            cmd.Append($"VALUES                                     ")
                            cmd.Append($"(                                          ")
                            cmd.Append($"   '{ CSI_Library.CSI_Library.username }', ")
                            cmd.Append($"   '{ treeNode.Text }'                   , ")
                            cmd.Append($"   '{ node_.Text }'                      , ")
                            cmd.Append($"    { node_.Tag }                          ")
                            cmd.Append($");                                         ")

                            MySqlAccess.ExecuteNonQuery(cmd.ToString())

                            Comput_perf_required = True
                        Else
                            MessageBox.Show("Unable to find group, please retry.")
                        End If
                    End If
                End If
end_:

            Next

        End If

        CSIFlexServiceLib.read_dashboard_config_fromdb()

        UpdateTargetsTitle()

        RefreshAllDevices()

        ComputePerfReq()

    End Sub

    ' Delete node in tr2/1 this i changed
    '======================================================================
    Private Sub Delete_treenode_treeview2(sender As Object, e As EventArgs) Handles DeleteGroupToolStripMenuItem.Click

        Dim tn As TreeNode = TV_GroupsOfMachines.SelectedNode

        If tn IsNot Nothing And Not tn.Text = "Groups of machines" Then
            selected_node_1 = Nothing
            Dim iRet = MessageBox.Show("Are you certain you want to delete " & tn.Text & "?", "Delete group", MessageBoxButton.YesNo, MessageBoxImage.Question)
            If iRet = MessageBoxResult.Yes Then
                TV_GroupsOfMachines.Nodes.Remove(tn)
                'Dim th As New Thread(AddressOf save_groupes)
                'th.Start("Delete|||" & tn.Text)

                'DELETE GROUP TABLE AND ROW IN LINKGROUP TABLE IN BD
                Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                Try
                    cntsql.Open()
                    Dim mysql3 As String = "delete from CSI_database.tbl_Groups WHERE `groups` = '" + tn.Text + "' and users = '" + CSI_Library.CSI_Library.username + "';"
                    mysql3 += "SELECT * from CSI_database.tbl_Groups;"
                    Dim cmdCreateDeviceTable3 As New MySqlCommand(mysql3, cntsql)
                    Dim mysqlReader3 As MySqlDataReader = cmdCreateDeviceTable3.ExecuteReader
                    cntsql.Close()
                    cntsql.Open()
                    Dim SearchGroup As String = "SELECT count(*) from CSI_database.tbl_devices where `groups` LIKE '%" + tn.Text + ", %';"
                    Dim cmdSearchGroup As New MySqlCommand(SearchGroup, cntsql)
                    Dim RowsAffected = cmdSearchGroup.ExecuteScalar()
                    cntsql.Close()
                    If RowsAffected = 0 Then
                        cntsql.Open()
                        Dim NewUpdateDeletedGroup As String = String.Empty
                        NewUpdateDeletedGroup = "UPDATE  CSI_database.tbl_devices SET `groups` = replace(`groups`, ', " + tn.Text + "', '');"
                        Dim cmdOtherUpdateDeletedGroup As New MySqlCommand(NewUpdateDeletedGroup, cntsql)
                        Dim rdrOtherUpdateDeletedGroup As MySqlDataReader = cmdOtherUpdateDeletedGroup.ExecuteReader
                        cntsql.Close()
                    Else
                        cntsql.Open()
                        Dim UpdateDeletedGroup As String = "UPDATE  CSI_database.tbl_devices SET `groups` = replace(`groups`, '" + tn.Text + ", ', '');"
                        Dim cmdUpdateDeletedGroup As New MySqlCommand(UpdateDeletedGroup, cntsql)
                        Dim rdrUpdateDeletedGroup As MySqlDataReader = cmdUpdateDeletedGroup.ExecuteReader
                        cntsql.Close()
                    End If
                    'cntsql.Open()
                    'Dim RowsAffected = cmdUpdateDeletedGroup.ExecuteScalar()
                    'cntsql.Close()
                    cntsql.Open()
                    Dim SelectDeviceTable As String = "SELECT * FROM csi_database.tbl_devices;"
                    Dim cmdSelectDeviceTable As New MySqlCommand(SelectDeviceTable, cntsql)
                    Dim rdrSelectDeviceTable As MySqlDataReader = cmdSelectDeviceTable.ExecuteReader
                    cntsql.Close()
                Catch ex As Exception
                    MessageBox.Show("Error in Deleting Group ::" & ex.Message())
                    cntsql.Close()
                End Try
                TV_GroupsOfMachines.Nodes(0).Nodes.Clear()
                Load_groupes(CSI_Library.CSI_Library.MySqlConnectionString)
                Dim GETAllDevices As String = "SELECT IP FROM csi_database.tbl_deviceconfig;"
                Dim dtTable As New DataTable
                Dim cmdGETAllDevices As New MySqlCommand(GETAllDevices, cntsql)
                Dim dbAdapter As New MySqlDataAdapter(cmdGETAllDevices)
                cntsql.Open()
                dbAdapter.Fill(dtTable)
                cntsql.Close()
                Dim oProducts = New List(Of String)
                For iIndex As Integer = 0 To dtTable.Rows.Count - 1
                    oProducts.Add(dtTable.Rows(iIndex)("IP"))
                Next
                Dim LISTSize As Integer
                LISTSize = oProducts.Count
                For Count As Integer = 0 To (LISTSize - 1)
                    RefreshDevice(oProducts(Count).ToString())
                    '    Count = Count + 1
                Next
                CSIFlexServiceLib.read_dashboard_config_fromdb()
                RefreshAllDevices()
                ComputePerfReq()
                'RefreshAllDevices()
                'ComputePerfReq()
                Comput_perf_required = True
            End If

        End If

    End Sub

    'Private Sub Delete_treenode_treeview1(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click

    '    Dim tn As TreeNode = selected_node_1
    '    selected_node_2 = Nothing
    '    If tn IsNot Nothing And Not tn.Text = "Sources" Then
    '        Dim iRet = MessageBox.Show("Are you certain you want to delete " & tn.Text & "?", "Delete source", MessageBoxButton.YesNo, MessageBoxImage.Question)
    '        If iRet = MessageBoxResult.Yes Then
    '            TV_Sources.Nodes.Remove(selected_node_1)
    '            deleteMachineFromSource(selected_node_1.Name, selected_node_1.Text)
    '        End If
    '    End If

    'End Sub

    Private Sub deleteMachineFromSource(name As String, sourceName As String)

        Dim tempSource As sourceToAdd = listOfSourceToAdd.Find(Function(x) x.name = name And x.sourceName = sourceName)
        Dim indexOfSource As Integer = listOfSourceToAdd.FindIndex(Function(x) x.name = name And x.sourceName = sourceName)

        For Each machToDelete As String In tempSource.listOfMachine

            Dim rowToDel As Integer = -1

            For Each Rowz As DataGridViewRow In gridviewMachines.Rows

                If (Rowz.Cells(1).Value.ToString().Equals(machToDelete)) Then
                    rowToDel = Rowz.Index
                    Exit For
                End If

            Next

            If (rowToDel <> -1) Then
                gridviewMachines.Rows.RemoveAt(rowToDel)
            End If
        Next

        listOfSourceToAdd.RemoveAt(indexOfSource)

    End Sub

    '============================================
    ' TREEVIEW 1/2 RENAME
    '============================================
    Public ancient_name As String

    Private Sub Rename_treenode_treeview2(sender As Object, e As EventArgs) Handles RenameGroupToolStripMenuItem.Click
        ' TreeView2.Nodes(0).Nodes(TreeView2.Nodes(0).Nodes.Count - 1).Remove()
        'txtbox.ShowDialog()
        If Not (IsNothing(TV_GroupsOfMachines.SelectedNode)) And Not TV_GroupsOfMachines.SelectedNode.Text = "Groups of machines" Then
            TV_GroupsOfMachines.LabelEdit = True
            ancient_name = TV_GroupsOfMachines.SelectedNode.Text
            TV_GroupsOfMachines.SelectedNode.BeginEdit()
            'TreeView2.LabelEdit = False
        End If
        'CSIFlexServiceLib.read_dashboard_config_fromdb()
        RefreshAllDevices()
    End Sub

    Private Sub DeleteFuctionsForDashboards(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles treeviewDevices.KeyDown

        If e.KeyCode = Keys.Delete Then

            Dim tn As TreeNode = treeviewDevices.SelectedNode

            Dim iRet = MessageBox.Show("Are you certain you want to delete " & tn.Text & "?", "Delete Device", MessageBoxButton.YesNo, MessageBoxImage.Question)

            If iRet = MessageBoxResult.Yes Then

                Dim sqlCmd As New Text.StringBuilder()
                sqlCmd.Append($"delete from CSI_database.tbl_devices       WHERE ip_adress = '{ IP_TB.Text }' and  deviceName = '{ Name_TB.Text }'; ")
                sqlCmd.Append($"delete from CSI_database.tbl_deviceconfig  WHERE IP        = '{ IP_TB.Text }' and  name = '{ Name_TB.Text }'; ")
                sqlCmd.Append($"delete from CSI_database.tbl_deviceconfig2 WHERE IP_adress = '{ IP_TB.Text }' and  name = '{ Name_TB.Text }'; ")
                sqlCmd.Append($"delete from CSI_database.tbl_messages      WHERE IP_adress = '{ IP_TB.Text }'; ")
                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                treeviewDevices.SelectedNode.Remove()
                send_http_req()
            End If
        End If

    End Sub

    Private Sub RenameUsingF2Key_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TV_GroupsOfMachines.KeyDown
        If e.KeyCode = Keys.F2 Then
            'If F2 key is pressed
            If Not (IsNothing(TV_GroupsOfMachines.SelectedNode)) And Not TV_GroupsOfMachines.SelectedNode.Text = "Groups of machines" And TV_GroupsOfMachines.SelectedNode.Level = 1 Then
                TV_GroupsOfMachines.LabelEdit = True
                ancient_name = TV_GroupsOfMachines.SelectedNode.Text
                TV_GroupsOfMachines.SelectedNode.BeginEdit()
                'TreeView2.LabelEdit = False
            End If
            'CSIFlexServiceLib.read_dashboard_config_fromdb()
            RefreshAllDevices()
        ElseIf e.KeyCode = Keys.Delete Then
            Dim tn As TreeNode = TV_GroupsOfMachines.SelectedNode

            If tn IsNot Nothing And Not tn.Text = "Groups of machines" And tn.Level = 1 Then
                selected_node_1 = Nothing
                Dim iRet = MessageBox.Show("Are you certain you want to delete " & tn.Text & "?", "Delete group", MessageBoxButton.YesNo, MessageBoxImage.Question)
                If iRet = MessageBoxResult.Yes Then
                    TV_GroupsOfMachines.Nodes.Remove(tn)
                    'Dim th As New Thread(AddressOf save_groupes)
                    'th.Start("Delete|||" & tn.Text)

                    'DELETE GROUP TABLE AND ROW IN LINKGROUP TABLE IN BD
                    Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                    Try
                        cntsql.Open()
                        Dim mysql3 As String = "delete from CSI_database.tbl_Groups WHERE `groups` = '" + tn.Text + "' and users = '" + CSI_Library.CSI_Library.username + "';"
                        mysql3 += "SELECT * from CSI_database.tbl_Groups;"
                        Dim cmdCreateDeviceTable3 As New MySqlCommand(mysql3, cntsql)
                        Dim mysqlReader3 As MySqlDataReader = cmdCreateDeviceTable3.ExecuteReader
                        cntsql.Close()
                        cntsql.Open()
                        Dim SearchGroup As String = "SELECT count(*) from CSI_database.tbl_devices where `groups` LIKE '%" + tn.Text + ", %';"
                        Dim cmdSearchGroup As New MySqlCommand(SearchGroup, cntsql)
                        Dim RowsAffected = cmdSearchGroup.ExecuteScalar()
                        cntsql.Close()
                        If RowsAffected = 0 Then
                            cntsql.Open()
                            Dim NewUpdateDeletedGroup As String = String.Empty
                            NewUpdateDeletedGroup = "UPDATE  CSI_database.tbl_devices SET `groups` = replace(`groups`, ', " + tn.Text + "', '');"
                            Dim cmdOtherUpdateDeletedGroup As New MySqlCommand(NewUpdateDeletedGroup, cntsql)
                            Dim rdrOtherUpdateDeletedGroup As MySqlDataReader = cmdOtherUpdateDeletedGroup.ExecuteReader
                            cntsql.Close()
                        Else
                            cntsql.Open()
                            Dim UpdateDeletedGroup As String = "UPDATE  CSI_database.tbl_devices SET `groups` = replace(`groups`, '" + tn.Text + ", ', '');"
                            Dim cmdUpdateDeletedGroup As New MySqlCommand(UpdateDeletedGroup, cntsql)
                            Dim rdrUpdateDeletedGroup As MySqlDataReader = cmdUpdateDeletedGroup.ExecuteReader
                            cntsql.Close()
                        End If
                        'cntsql.Open()
                        'Dim RowsAffected = cmdUpdateDeletedGroup.ExecuteScalar()
                        'cntsql.Close()
                        cntsql.Open()
                        Dim SelectDeviceTable As String = "SELECT * FROM csi_database.tbl_devices;"
                        Dim cmdSelectDeviceTable As New MySqlCommand(SelectDeviceTable, cntsql)
                        Dim rdrSelectDeviceTable As MySqlDataReader = cmdSelectDeviceTable.ExecuteReader
                        cntsql.Close()
                    Catch ex As Exception
                        MessageBox.Show("Error in Deleting Group ::" & ex.Message())
                        cntsql.Close()
                    End Try

                    TV_GroupsOfMachines.Nodes(0).Nodes.Clear()

                    Load_groupes(CSI_Library.CSI_Library.MySqlConnectionString)

                    Dim GETAllDevices As String = "SELECT IP FROM csi_database.tbl_deviceconfig;"
                    Dim dtTable As New DataTable
                    Dim cmdGETAllDevices As New MySqlCommand(GETAllDevices, cntsql)
                    Dim dbAdapter As New MySqlDataAdapter(cmdGETAllDevices)
                    cntsql.Open()
                    dbAdapter.Fill(dtTable)
                    cntsql.Close()
                    Dim oProducts = New List(Of String)
                    For iIndex As Integer = 0 To dtTable.Rows.Count - 1
                        oProducts.Add(dtTable.Rows(iIndex)("IP"))
                    Next
                    Dim LISTSize As Integer
                    LISTSize = oProducts.Count
                    For Count As Integer = 0 To (LISTSize - 1)
                        RefreshDevice(oProducts(Count).ToString())
                        '    Count = Count + 1
                    Next
                    CSIFlexServiceLib.read_dashboard_config_fromdb()
                    RefreshAllDevices()
                    ComputePerfReq()
                    'RefreshAllDevices()
                    'ComputePerfReq()
                    Comput_perf_required = True
                End If
            End If
        End If
    End Sub

    '============================================
    ' TREEVIEW 1/2 select a node with Right clic to rename/delete 
    '============================================

    Public selected_node_1 As TreeNode

    Public selected_node_2 As TreeNode

#End Region


    Public LoadingSources As Boolean = False

    Private Sub Load_sources()

        If File.Exists(path & "\sys\machines_sources_.csys") Then
            Try
                LoadingSources = True
                'TV_Sources.Nodes(0).Nodes.Clear()
                Using reader As StreamReader = New StreamReader(path & "\sys\machines_sources_.csys")

                    While Not reader.EndOfStream
                        Dim readed() As String = reader.ReadLine.Split(",")

                        Dim param1 = "", param2 = "", param3 = ""
                        If (readed.Count >= 3) Then
                            param1 = readed(1)
                            param2 = readed(0)
                            param3 = readed(2)
                        ElseIf (readed.Count = 2) Then
                            param1 = readed(1)
                            param2 = readed(0)
                        ElseIf (readed.Count = 1) Then
                            param2 = readed(0)
                        End If
                        listOfSourceToAdd.Add(New sourceToAdd(param1, param2, param3)) 'IIf(readed(1) IsNot Nothing, readed(1), ""), IIf(readed(0) IsNot Nothing, readed(0), ""), IIf(readed(2) IsNot Nothing, readed(2), "")))

                    End While
                    reader.Close()
                End Using
                LoadingSources = False
            Catch ex As Exception
                ' MessageBox.Show("Unable to Load the machines groups : " & ex.Message)
                CSI_Lib.LogServerError(ex.ToString(), 1)
            End Try
        End If

    End Sub

    '============================================
    ' Load groupe with their machines and add them in the treeview
    '============================================
    Public Sub Load_groupes(mysqlcntstr As String)

        Try
            Dim dtGroups As DataTable = MySqlAccess.GetDataTable("SELECT * from CSI_database.tbl_Groups order by `groups`, machines")

            Dim dtMachines As DataTable = MySqlAccess.GetDataTable("SELECT * from csi_auth.tbl_ehub_conf")

            TV_GroupsOfMachines.Nodes(0).Nodes.Clear()

            Dim i As Integer = -1
            Dim group As String = ""
            Dim node As TreeNode

            For Each row As DataRow In dtGroups.Rows

                If Not row("groups").ToString() = group Then
                    node = New TreeNode(row("groups").ToString())
                    node.Name = row("groups").ToString()
                    node.Tag = "0"
                    TV_GroupsOfMachines.Nodes(0).Nodes.Add(node)
                    group = row("groups").ToString()
                    i += 1
                End If

                Dim rowMachine As DataRow = dtMachines.Select().Where(Function(m) m.Item("id").ToString() = row("machineId").ToString()).FirstOrDefault()

                If Not IsNothing(rowMachine) Then
                    node = New TreeNode()
                    node.Name = rowMachine("Machine_Name").ToString()
                    node.Text = rowMachine("Machine_Name").ToString()
                    node.Tag = rowMachine("Id").ToString()
                    node.ForeColor = Color.Gray
                    TV_GroupsOfMachines.Nodes(0).Nodes(i).Nodes.Add(node)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Unable to Load the machines groups : " & ex.Message)
        End Try

    End Sub

    Public GroupsTable As New DataTable()

    Public Group_dic As New Dictionary(Of String, List(Of String))

    '===================================================================================================
    ' Save datagridview2 / users
    '===================================================================================================
    Public DGV2MOD As Boolean = False

    Public Shared Function IfNullObj(ByVal o As Object, Optional ByVal DefaultValue As String = "") As String
        Dim ret As String = ""
        Try
            If o Is DBNull.Value Then
                ret = DefaultValue
            Else
                ret = o.ToString()
            End If
            Return ret
        Catch ex As Exception
            Return ret
        End Try
    End Function

    Public Shared Function DataGridViewToDataTable(ByVal dtg As DataGridView,
        Optional ByVal DataTableName As String = "myDataTable") As DataTable
        Try
            Dim dt As New DataTable(DataTableName)
            Dim row As DataRow
            Dim TotalDatagridviewColumns As Integer = dtg.ColumnCount - 1
            'Add Datacolumn
            For Each c As DataGridViewColumn In dtg.Columns
                Dim idColumn As DataColumn = New DataColumn()
                idColumn.ColumnName = c.Name
                dt.Columns.Add(idColumn)
            Next
            'Now Iterate thru Datagrid and create the data row
            For Each dr As DataGridViewRow In dtg.Rows
                'Iterate thru datagrid
                row = dt.NewRow 'Create new row
                'Iterate thru Column 1 up to the total number of columns
                For cn As Integer = 0 To TotalDatagridviewColumns
                    row.Item(cn) = IfNullObj(dr.Cells(cn).Value) ' This Will handle error datagridviewcell on NULL Values
                Next
                'Now add the row to Datarow Collection
                dt.Rows.Add(row)
            Next
            'Now return the data table
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '===================================================================================================
    ' Load users to dgv2
    '===================================================================================================
    Public usersVSmachines As New Dictionary(Of String, List(Of String)), loading As Boolean = False

    Public deviceVSmachines As New Dictionary(Of String, List(Of String))

    Private Sub load_users()

        Dim CSILIB As New CSI_Library.CSI_Library(True)

        loading = True
        '*************************************************************************************************************************************************'
        '**** DB Connection
        '*************************************************************************************************************************************************'

        Dim mysqlcnt As MySqlConnection
        Try
            mysqlcnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            mysqlcnt.Open()
            'Checks if connection to databasee is established 
            If mysqlcnt.State = 1 Then
            Else
                MessageBox.Show("Connection to the database failed")
                GoTo End_
            End If
        Catch ex As Exception
            MessageBox.Show(" Unable to establish a connection to the database : " & ex.Message, vbCritical + vbSystemModal)
            GoTo End_
        End Try

        '*************************************************************************************************************************************************'
        '**** DB Connection -END
        '*************************************************************************************************************************************************'
        Dim mysqladap As New MySqlDataAdapter, mysqlreader As MySqlDataReader, mysqlreader2 As MySqlDataReader, mysqlreader3 As MySqlDataReader
        Dim table_ As New DataTable, table2_ As New DataTable
        'Checks if User exists in csi_auth.Users table By Defalt it has atleast one user i.e.'admin' 
        Dim command As New MySqlCommand("SELECT * FROM csi_auth.Users", mysqlcnt)
        mysqlreader = command.ExecuteReader
        table_.Load(mysqlreader)
        'create the connection string

#Region "Load Device Information from table `tbl_devices` in csi_databse"
        'Check for devices if they already exists loads their machines and groups information into Datatable
        'create an OleDbDataAdapter to execute the query
        Dim mySqlComm As New MySqlCommand(queryDevice, mysqlcnt)
        mysqlreader2 = mySqlComm.ExecuteReader

        'create a DataTable to hold the query results
        Dim dTable_device As New DataTable()
        dTable_device.Load(mysqlreader2)
#End Region

#Region "Load Messages from Table `tbl_messages` in csi_database"
        Dim mySqlComm2 As New MySqlCommand(queryMessage, mysqlcnt)
        mysqlreader3 = mySqlComm2.ExecuteReader

        Dim dTable_message As New DataTable()
        dTable_message.Load(mysqlreader3)
#End Region
        mysqlcnt.Close()

        usersVSmachines.Clear()
        deviceVSmachines.Clear()

End_:
        loading = False
    End Sub

    Public selectedtype As String = ""

    Function usertype() As String
        SelectUserType.ShowDialog()
        Return selectedtype

    End Function

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        If ((gridviewMachines.SelectedRows.Count = 1 Or gridviewMachines.SelectedCells.Count = 1) And gridviewMachines.Rows(gridviewMachines.SelectedCells.Item(0).RowIndex).Cells("Source").Value.ToString() = "eNET") Then
            Edit_eNET.Show()
            Edit_eNET.comingfromform1 = False
        ElseIf gridviewMachines.Rows(gridviewMachines.SelectedCells.Item(0).RowIndex).Cells("Source").Value.ToString() = "eNET" Then
            Edit_eNET.Show()
            Edit_eNET.comingfromform1 = False
        End If
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' Read the treeview and fills the checked machines in a list of string (declared as public)
    ''-----------------------------------------------------------------------------------------------------------------------

#Region "Read the treeview 3 "

    Public Shared checked__treeview3 As New List(Of String)
    Public Shared checked__treeviewDevice As New List(Of String)
    Public Shared checked__treeviewUser As New List(Of String)
    Public Shared checked__treeviewCMP As New List(Of String)
    Public Shared checked__treeviewGroup As New List(Of String)
    Public Shared checked__treeviewGroup2 As New List(Of String)

    Function readTreeRecurciveUser() As List(Of String)
        checked__treeviewUser.Clear()
        Dim aNode As TreeNode

        For Each aNode In treeviewMachines.Nodes(0).Nodes
            PrintRecursiveUser(aNode)
        Next
        Return checked__treeviewUser
    End Function

    'Function readTreeRecurciveCMP() As List(Of String)
    '    checked__treeviewCMP.Clear()
    '    Dim aNode As TreeNode

    '    For Each aNode In TV_CurrPerfGroup.Nodes(0).Nodes
    '        PrintRecursiveCMP(aNode)
    '    Next
    '    Return checked__treeviewCMP
    'End Function

    Function readTreeRecurcivegroup2() As List(Of String)
        checked__treeviewGroup2.Clear()
        Dim aNode As TreeNode

        For Each aNode In treeviewMachines.Nodes(1).Nodes
            PrintRecursivegroup2(aNode)
        Next
        Return checked__treeviewGroup2
    End Function

    Private Sub PrintRecursive(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            checked__treeview3.Add(n.Text)
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursive(aNode)
        Next
    End Sub

    Private Sub PrintRecursiveDevice(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            checked__treeviewDevice.Add(n.Tag)
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursiveDevice(aNode)
        Next
    End Sub

    Private Sub PrintRecursiveUser(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            checked__treeviewUser.Add(n.Text)
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursiveUser(aNode)
        Next
    End Sub

    Private Sub PrintRecursiveCMP(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            checked__treeviewCMP.Add(n.Text)
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursiveCMP(aNode)
        Next
    End Sub

    Private Sub PrintRecursivegroup(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            checked__treeviewGroup.Add(n.Tag)
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursivegroup(aNode)
        Next
    End Sub

    Private Sub PrintRecursivegroup2(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            checked__treeviewGroup2.Add(n.Text)
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursivegroup2(aNode)
        Next
    End Sub

    Sub unchecktreedash()
        fiiliing = True

        Dim aNode As TreeNode
        For Each aNode In TV_LivestatusMachine.Nodes
            unchecktreeRecursive(aNode)
        Next

        fiiliing = False
    End Sub

    Sub unchecktreeDevice()
        fiiliing = True

        Dim aNode As TreeNode
        For Each aNode In treeviewDevices.Nodes
            unchecktreeRecursive(aNode)
        Next
        fiiliing = False
    End Sub

    Private Sub unchecktreeRecursive(ByVal n As TreeNode)
        If n.Checked = True Then n.Checked = False
        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            unchecktreeRecursive(aNode)
        Next
    End Sub

    Private Sub unchecktreeRecursiveDevice(ByVal n As TreeNode)
        If n.Checked = True Then n.Checked = False
        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            unchecktreeRecursiveDevice(aNode)
        Next
    End Sub

    Private Sub CheckAllChildNodes(ByVal parentNode As TreeNode)

        For Each childNode As TreeNode In parentNode.Nodes

            If Not (childNode.Checked = parentNode.Checked) Then
                childNode.Checked = parentNode.Checked
                For Each node As TreeNode In TV_LivestatusMachine.Nodes
                    CheckAllNodes(node, childNode)
                Next
            End If

            CheckAllChildNodes(childNode)
        Next
    End Sub

    Private Sub CheckAllNodes(ByVal Node As TreeNode, modified_node_ As TreeNode)
        For Each childNode As TreeNode In Node.Nodes
            'childNode.Checked = Node.Checked
            If childNode.Text = modified_node_.Text Then
                childNode.Checked = modified_node_.Checked
            End If
            CheckAllNodes(childNode, modified_node_)
        Next

    End Sub

    Private Sub IsEveryChildChecked(ByVal parentNode As TreeNode, ByRef checkValue As Boolean)
        For Each node As TreeNode In parentNode.Nodes
            Call IsEveryChildChecked(node, checkValue)
            If Not node.Checked Then
                checkValue = False
            End If
        Next
    End Sub

    Private Sub ShouldParentsBeChecked(ByVal startNode As TreeNode)
        If startNode.Parent Is Nothing = False Then
            Dim allChecked As Boolean = True
            Call IsEveryChildChecked(startNode.Parent, allChecked)
            If allChecked Then
                startNode.Parent.Checked = True
                Call ShouldParentsBeChecked(startNode.Parent)
            End If
        End If
    End Sub

#End Region


    Private myTimer As System.Threading.Timer

    Private Sub DONOT_refresh_mon_state__()
        '  Dim myCallback As New System.Threading.TimerCallback(AddressOf Task1)
        Try
            If myTimer IsNot Nothing Then myTimer.Dispose()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub BgWorker_CreateDB_DoWork(sender As Object, e As DoWorkEventArgs) Handles BgWorker_CreateDB.DoWork


        Dim total = eNETHistory.UpdateMachinesDatabase()

        'CSI_Lib.LogServiceAction($"=====>> Imported from CSV file: { total }", True)

        Log.Debug($"=====>> Imported from CSV file: { total }")

        Return

        'If (TB_eNETfolder.Text <> "") Then

        '    CSI_Library.CSI_Library.isServer = True

        '    If (CSI_Lib.getFirstUpdateOrNot()) Then
        '        Try
        '            Dim years_(-1) As String

        '            If (File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\years_.csys")) Then
        '                Using streader As New StreamReader(CSI_Library.CSI_Library.serverRootPath + "\sys\years_.csys")
        '                    Dim tmp_str As String = streader.ReadLine()
        '                    If tmp_str IsNot Nothing Then years_ = tmp_str.Split(",")
        '                End Using
        '            End If

        '            If years_ Is Nothing Or years_.Length = 0 Then
        '                CSI_Lib.Log_server_event("Years_ (in setupform) is nothing, FirstupdateDB executed")
        '                CSI_Lib.FirstUpdateDB_Mysql(Now.Year)
        '            Else
        '                For Each year As String In years_
        '                    CSI_Lib.Log_server_event("FirstupdateDB executed for year : " & year)
        '                    If year <> "" Then CSI_Lib.FirstUpdateDB_Mysql(year)
        '                Next
        '            End If

        '        Catch ex As Exception
        '            CSI_Lib.LogServerError(ex.Message, 1)
        '        End Try
        '    End If
        'End If

    End Sub

    Private Sub BgWorker_importDB_DoWork(sender As Object, e As DoWorkEventArgs) Handles BgWorker_importDB.DoWork

        Dim importpath As String = e.Argument
        If isUnistalling Then
        Else
            CSI_Lib.ImportDB_Mysql(importpath)
        End If
        CSI_Lib.ImportDB_Mysql(importpath)
        MessageBox.Show("importpath for CSV File : " & importpath)
    End Sub

    Private Sub BgWorker_CreateDB_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BgWorker_CreateDB.RunWorkerCompleted
        'first_creation_of_the_DB = True
        'StatusStrip1.Text = ""
        TSSL_DBCreation.Text = ""
        'MessageBox.Show("Initial database creation completed", "Database creation", MessageBoxButton.OK, MessageBoxImage.Information)
        'syncDB.Close()
    End Sub

    Public Function pingHost(IpOrName As String) As Boolean
        Dim pingable As Boolean = False

        Dim pinger As Ping = New Ping()

        Try
            Dim reply As PingReply = pinger.Send(IpOrName)
            If (reply.Status = IPStatus.Success) Then

                pingable = True

            End If
        Catch ex As Exception
            pingable = False
        End Try

        Return pingable
    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

    End Sub

    Dim inLoadingDeviceMode = False

    Private Sub Con_TB_CheckedChanged(sender As Object, e As EventArgs) Handles Con_TB.CheckedChanged

        If Con_TB.Checked = True Then
            Con_GB.Enabled = True
            Con_GB.Visible = True
            If Mess_DGV.SelectedCells.Item(0).ColumnIndex = 0 Then
                Mess_DGV.SelectedCells.Item(0).Value = "Select a period"
            Else
                Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(0).Value = "Select a period"
            End If
        Else
            Con_GB.Enabled = False
            Con_GB.Visible = False
            If Mess_DGV.SelectedCells.Item(0).ColumnIndex = 0 Then
                'Mess_DGV.SelectedCells.Item(0).Value = "Select a system message type"
            Else
                'Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(0).Value = "Select a system message type"
            End If
            Validate_BTN.Enabled = False
            Day_RB.Checked = False
            Week_RB.Checked = False
            Month_RB.Checked = False
        End If

    End Sub

    Private Sub Day_RB_CheckedChanged(sender As Object, e As EventArgs) Handles Day_RB.CheckedChanged


        If (Day_RB.Checked = True) Then

            Text_TB.Text = "Daily Best CYCLE ON Machine"
            Dim max As Integer = 1
            Dim temp As String = ""
            For i = 0 To max
                For Each c As DataGridViewCell In Mess_DGV.Rows.Item(0).Cells
                    If (Text_TB.Text = c.Value.ToString()) Then
                        temp = i.ToString()
                    End If
                Next
            Next
            Text_TB.Text += temp
            Validate_BTN.Enabled = True
        End If


    End Sub

    Private Sub Week_RB_CheckedChanged(sender As Object, e As EventArgs) Handles Week_RB.CheckedChanged
        If (Week_RB.Checked = True) Then

            Text_TB.Text = "Weekly Best CYCLE ON Machine"
            Dim max As Integer = 1
            Dim temp As String = ""
            For i = 0 To max
                For Each c As DataGridViewCell In Mess_DGV.Rows.Item(0).Cells
                    If (Text_TB.Text = c.Value.ToString()) Then
                        temp = i.ToString()
                    End If
                Next
            Next
            Text_TB.Text += temp
            Validate_BTN.Enabled = True
        End If
    End Sub

    Private Sub Month_RB_CheckedChanged(sender As Object, e As EventArgs) Handles Month_RB.CheckedChanged
        If (Month_RB.Checked = True) Then

            Text_TB.Text = "Monthly Best CYCLE ON Machine"
            Dim max As Integer = 1
            Dim temp As String = ""
            For i = 0 To max
                For Each c As DataGridViewCell In Mess_DGV.Rows.Item(0).Cells
                    If (Text_TB.Text = c.Value.ToString()) Then
                        temp = i.ToString()
                    End If
                Next
            Next
            Text_TB.Text += temp
            Validate_BTN.Enabled = True
        End If
    End Sub

    Sub send_http_req(Optional extension As String = "readconfig")

        Try

            Dim portT As DataTable = MySqlAccess.GetDataTable("SELECT port FROM csi_database.tbl_rm_port;")

            Dim request As WebRequest

            If portT.Rows.Count > 0 Then
                If IsDBNull(portT.Rows(0)("port")) Then
                    request = WebRequest.Create($"http://127.0.0.1:8008/{ extension }")
                Else
                    request = WebRequest.Create($"http://127.0.0.1:{ portT.Rows(0)("port") }/{ extension }")
                End If
            Else
                request = WebRequest.Create("http://127.0.0.1:8008/readconfig")
            End If

            Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes("")

            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length

            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

        Catch ex As Exception

            Log.Error(ex)

        End Try

    End Sub

    Private Function Srv_read_req() As Boolean
        Try


            Dim portT As New DataTable
            Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select port From csi_database.tbl_rm_port;", CSI_Library.CSI_Library.MySqlConnectionString)
            dadapter_name.Fill(portT)

            Dim request As WebRequest

            If portT.Rows.Count <> 0 Then
                If IsDBNull(portT.Rows(0)("port")) Then
                    request = WebRequest.Create("http://127.0.0.1:8008/readconfig")
                Else
                    request = WebRequest.Create("http://127.0.0.1:" & portT.Rows(0)("port") & "/readconfig")
                End If
            End If

            request.Method = "POST"

            Dim postData As String = ""
            Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
            ' Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded"
            ' Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length
            ' Get the request stream.
            Dim dataStream As Stream = request.GetRequestStream()
            ' Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length)
            ' Close the Stream object.

            Dim myWebResponse As WebResponse = request.GetResponse()
            Dim dataStreamR As Stream = myWebResponse.GetResponseStream()
            Dim reader As New StreamReader(dataStreamR)
            Dim responseFromServer As String = reader.ReadToEnd()


            dataStream.Close()
            ' Get the response.
            If responseFromServer = "ok,thanks ;)" Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

            Log.Error(ex)
            CSI_Lib.LogServerError("Unable to send http req" + ex.Message, 1)

            Return False
        End Try
    End Function

    Private Sub Browse_Logo_Click(sender As Object, e As EventArgs) Handles Browse_Logo.Click

        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.ShowDialog()

        Dim newLogoFullPath As String = ""

        txtLogoPath.Text = openFileDialog1.SafeFileName.ToString()

        newLogoFullPath = openFileDialog1.FileName.ToString()

        Try
            Dim logoFolder = IO.Path.Combine(CSI_Library.CSI_Library.serverRootPath, "html", "html", "img")
            Dim logoFullPath = IO.Path.Combine(logoFolder, txtLogoPath.Text)

            'If Not Directory.Exists(logoFolder) Then
            '    Directory.CreateDirectory(logoFolder)
            'End If

            If (IO.File.Exists(logoFullPath)) Then
                File.Delete(logoFullPath)
            End If

            FileCopy(newLogoFullPath, logoFullPath)

        Catch ex As Exception
            Log.Error(ex)
        End Try

        Try

            MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_deviceConfig SET detail_customlogo = '{ txtLogoPath.Text }' WHERE deviceId = { deviceId }")

        Catch ex As Exception
            Log.Error(ex)
        End Try

    End Sub

    Private Sub Devices_TV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles treeviewDevices.NodeMouseClick

        If e.Button = Windows.Forms.MouseButtons.Right Then

            Try
                If e.Node.Text <> treeviewDevices.SelectedNode.Text Then
                    treeviewDevices.SelectedNode = e.Node
                End If

                ' Point where the mouse is clicked.
                Dim p As New Drawing.Point(e.X, e.Y)

                ' Get the node that the user has clicked.
                Dim node As TreeNode = treeviewDevices.GetNodeAt(p)
                If node IsNot Nothing Then

                    ' Select the node the user has clicked.
                    ' The node appears selected until the menu is displayed on the screen.
                    Dim m_OldSelectNode = treeviewDevices.SelectedNode
                    treeviewDevices.SelectedNode = node

                    ' Find the appropriate ContextMenu depending on the selected node.
                    If (node.Level = 0) Then
                        Dim cms = New ContextMenuStrip
                        Dim item1 = cms.Items.Add("Add Device")
                        item1.Tag = 1

                        AddHandler item1.Click, AddressOf menuChoice

                        cms.Show(treeviewDevices, p)

                    ElseIf (node.Level = 1) Then
                        Dim cms = New ContextMenuStrip

                        If CB__DeviceType.Text = "LR1" Then
                            Dim item5 = cms.Items.Add("Change Target Server")
                            Dim item8 = cms.Items.Add("Change Date and Time")
                            Dim item6 = cms.Items.Add("Duplicate Device")
                            Dim item7 = cms.Items.Add("Reboot Device")
                            Dim item10 = cms.Items.Add("Refresh Device")
                            Dim item11 = cms.Items.Add("Update Device")
                            Dim item12 = cms.Items.Add("Change network setting")
                            Dim item2 = cms.Items.Add("Remove Device")
                            Dim item14 = cms.Items.Add("See Preview")
                            item2.Tag = 2
                            item5.Tag = 5
                            item6.Tag = 6
                            item7.Tag = 7
                            item8.Tag = 8
                            item10.Tag = 10
                            item11.Tag = 11
                            item12.Tag = 12
                            item14.Tag = 14
                            AddHandler item5.Click, AddressOf menuChoice
                            AddHandler item6.Click, AddressOf menuChoice
                            AddHandler item7.Click, AddressOf menuChoice
                            AddHandler item2.Click, AddressOf menuChoice
                            AddHandler item8.Click, AddressOf menuChoice
                            AddHandler item10.Click, AddressOf menuChoice
                            AddHandler item11.Click, AddressOf menuChoice
                            AddHandler item12.Click, AddressOf menuChoice
                            AddHandler item14.Click, AddressOf menuChoice

                        Else 'CB__DeviceType.Text = "" Then
                            Dim item6 = cms.Items.Add("Duplicate Device")
                            item6.Tag = 6
                            AddHandler item6.Click, AddressOf menuChoice

                            If treeviewDevices.SelectedNode.Text <> "Local Host" Then
                                Dim item2 = cms.Items.Add("Remove Device")
                                item2.Tag = 2
                                AddHandler item2.Click, AddressOf menuChoice
                            End If

                        End If
                        cms.Show(treeviewDevices, p)
                    End If

                End If
            Catch ex As Exception
                MessageBox.Show("Error while adding device, see log")
                CSI_Lib.LogServerError("Error adding device to dashboard:" + ex.Message, 1)
            End Try
        End If
    End Sub

    Private Sub menuChoice(ByVal sender As Object, ByVal e As EventArgs)

        Dim item = CType(sender, ToolStripMenuItem)
        Dim selection = CInt(item.Tag)

        If (selection = 1) Then
            AddDevice.Show()
            send_http_req()
        End If

        If (selection = 2) Then

            Dim tn As TreeNode = treeviewDevices.SelectedNode
            Dim iRet = MessageBox.Show("Are you certain you want to delete " & tn.Text & "?", "Delete Device", MessageBoxButton.YesNo, MessageBoxImage.Question)

            If iRet = MessageBoxResult.Yes Then

                'Dim device As New DashboardDevice(IP_TB.Text)
                Dim device As New DashboardDevice(CInt(tn.Name))
                device.DeleteDevice()

                treeviewDevices.SelectedNode.Remove()
                send_http_req()

            End If
        End If

        If (selection = 3) Then
            treeviewDevices.Nodes.Add("Group " + treeviewDevices.Nodes.Count.ToString())
        End If

        If (selection = 4) Then
            'Devices_TV.LabelEdit = True
            'Devices_TV.SelectedNode.BeginEdit()
            'While Devices_TV.SelectedNode.IsEditing
            'Name_TB.
            'End While
            'Name_TB.Text = Devices_TV.SelectedNode.Text
            'Devices_TV.LabelEdit = False
        End If

        If (selection = 5) Then
            targetServer.Show()
        End If

        If (selection = 6) Then 'Duplicate a Device Code Here
            copy = treeviewDevices.SelectedNode.Text   'This saves name of the Device in a variable
            DeviceType = CB__DeviceType.SelectedItem.ToString()
            CSI_Library.CSI_Library.DuplicateDevice = True

            AddDevice.Show()
        End If

        If (selection = 7) Then
            targetServer.reboot()
        End If

        If (selection = 11) Then
            targetServer.updateSoft()
        End If

        If (selection = 12) Then
            targetServer.updateNetwork()
        End If

        If (selection = 8) Then
            TimeDate.Show()
        End If

        If (selection = 9) Then
            SockConn.Show()
        End If

        If (selection = 10) Then
            RefreshDevice(IP_TB.Text)
        End If
        If (selection = 14) Then
            'MessageBox.Show("Preview of Raspberry Pie is Selected")
            Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            Try
                cntsql.Open()
                Dim mysql_str As String = "SELECT devicetype FROM CSI_database.tbl_deviceconfig2 where IP_adress = '" & Me.IP_TB.Text & "' and name = '" + Me.Name_TB.Text + "'"
                Dim cmd_devicetype As New MySqlCommand(mysql_str, cntsql)
                Dim mysqlReader23 As MySqlDataReader = cmd_devicetype.ExecuteReader
                Dim dTable_type As New DataTable()
                dTable_type.Load(mysqlReader23)
                cntsql.Close()
                Dim type As String = ""
                If dTable_type.Rows.Count <> 0 Then
                    type = dTable_type.Rows(0).Item(0)
                End If
                If type = "LR1" Then
                    LR1_preview.ShowDialog()
                Else
                    MessageBox.Show("The preview is available only for LR1s")
                End If

            Catch ex As Exception
                If cntsql.State = ConnectionState.Open Then cntsql.Close()
                MsgBox("Could not display a preview : " & ex.Message)
            End Try
        End If
    End Sub

    Public Sub RefreshDevice(deviceip As String)
        Try
            If Not inLoadingDeviceMode Then

                Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                cntsql.Open()
                Dim cmdsql As New MySqlCommand("update csi_database.tbl_deviceconfig set refreshbrowser='refreshing' where IP='" & deviceip & "'", cntsql)
                cmdsql.ExecuteNonQuery()
                cntsql.Close()
                send_http_req()
            End If
        Catch ex As Exception
            CSI_Lib.LogServerError("Unable to ask for refresh:" + ex.Message, 1)
        End Try
    End Sub

    Public Sub RefreshAllDevices()
        Try
            If Not inLoadingDeviceMode Then
                send_http_req()

                Dim portT As New DataTable

                Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select port From csi_database.tbl_rm_port;", CSI_Library.CSI_Library.MySqlConnectionString)
                dadapter_name.Fill(portT)

                Dim request As WebRequest

                If portT.Rows.Count <> 0 Then
                    If IsDBNull(portT.Rows(0)("port")) Then
                        request = WebRequest.Create("http://127.0.0.1:8008/readPerf")
                    Else
                        request = WebRequest.Create("http://127.0.0.1:" & portT.Rows(0)("port") & "/readPerf")
                    End If
                End If



                request.Method = "POST"

                Dim postData As String = ""
                Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
                ' Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded"
                ' Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length
                ' Get the request stream.
                Dim dataStream As Stream = request.GetRequestStream()
                ' Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length)
                ' Close the Stream object.




                dataStream.Close()
            End If
        Catch ex As Exception
            CSI_Lib.LogServerError("Unable to ask for refresh:" + ex.Message, 1)
        End Try
    End Sub

    Public Sub ComputePerfReq()
        Try
            If Not inLoadingDeviceMode Then
                Srv_read_req()
                Dim portT As New DataTable
                Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select port From csi_database.tbl_rm_port;", CSI_Library.CSI_Library.MySqlConnectionString)
                dadapter_name.Fill(portT)

                Dim request As WebRequest

                If portT.Rows.Count <> 0 Then
                    If IsDBNull(portT.Rows(0)("port")) Then
                        request = WebRequest.Create("http://127.0.0.1:8008/ComputePerf")
                    Else
                        request = WebRequest.Create("http://127.0.0.1:" & portT.Rows(0)("port") & "/ComputePerf")
                    End If
                End If



                request.Method = "POST"

                Dim postData As String = ""
                Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
                ' Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded"
                ' Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length
                ' Get the request stream.
                Dim dataStream As Stream = request.GetRequestStream()
                ' Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length)
                ' Close the Stream object.




                dataStream.Close()
            End If
        Catch ex As Exception
            CSI_Lib.LogServerError("Unable to ask for refresh:" + ex.Message, 1)
        End Try
    End Sub

    Private Sub Devices_TV_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treeviewDevices.AfterSelect

        If (treeviewDevices.SelectedNode.Text = treeviewDevices.Nodes(0).Text) Then
            Panel_dashConfig.Visible = False
            TV_LivestatusMachine.Visible = False
        Else
            Panel_dashConfig.Visible = True
            TV_LivestatusMachine.Visible = True
            Delete_BTN.Enabled = False
            Text_TB.Text = ""
            Text_TB.Visible = False
            MT_GB.Enabled = False
            configisloading = True

            treeViewClick = False

            load_userConfig(CSI_Library.CSI_Library.MySqlConnectionString)

            configisloading = False

            If Not (Mess_DGV.Rows.Count = 0) Then
                Delete_BTN.Enabled = True
            End If
        End If

    End Sub

    Dim treeViewClick As Boolean = False

    Dim treeViewEnable As Boolean = True

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Add_BTN.Click
        UM_RB.Checked = False
        RadioButton2.Checked = False
        Mess_DGV.Rows.Add()
        Delete_BTN.Enabled = True
        Mess_DGV.SelectedCells.Item(0).Selected = False
        Mess_DGV.Rows(Mess_DGV.Rows.Count - 1).Cells(0).Selected = True
        If Not (Mess_DGV.Rows.Count - 1 = 0) Then
            Mess_DGV.Rows(Mess_DGV.Rows.Count - 2).Cells(0).Selected = False
        End If
        Mess_DGV.SelectedCells.Item(0).Value = "Select a message type"
        Mess_DGV.Rows(Mess_DGV.Rows.Count - 1).Cells(1).Value = Mess_DGV.Rows.Count.ToString()
        MT_GB.Enabled = True
        UM_RB.Checked = False
        RadioButton2.Checked = False
        Text_TB.Text = ""
        Text_TB.Visible = False
        Con_TB.Checked = False
        Day_RB.Checked = False
        Week_RB.Checked = False
        Month_RB.Checked = False
        Mess_DGV.Enabled = False
        Button4.Enabled = False
        Button6.Enabled = False
        Add_BTN.Enabled = False
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles UM_RB.CheckedChanged
        If UM_RB.Checked = True Then
            Text_TB.Visible = True
            Label20.Visible = True
            Con_TB.Enabled = False
            Con_TB.Visible = False
            Label15.Visible = False
            If Not (Mess_DGV.Rows.Count = 0) Then
                If Mess_DGV.SelectedCells.Item(0).ColumnIndex = 0 Then
                    If Mess_DGV.SelectedCells.Item(0).Value = "Select a period" Then
                        Day_RB.Checked = False
                        Week_RB.Checked = False
                        Month_RB.Checked = False
                        Con_TB.Checked = False
                        Text_TB.Text = ""
                        Mess_DGV.SelectedCells.Item(0).Value = "Enter a message text"
                    End If
                    If Mess_DGV.SelectedCells.Item(0).Value = "Select a message type" Then
                        Mess_DGV.SelectedCells.Item(0).Value = "Enter a message text"
                    End If
                Else
                    If Mess_DGV.SelectedCells.Item(0).Value = "" Then
                        Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(0).Value = "Enter a message text"
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            Con_TB.Enabled = True
            Con_TB.Visible = True
            Label15.Visible = True

            Text_TB.Visible = False
            Label20.Visible = False
            If Mess_DGV.SelectedCells.Item(0).ColumnIndex = 0 Then
                If Mess_DGV.SelectedCells.Item(0).Value = "Select a message type" Then
                    Mess_DGV.SelectedCells.Item(0).Value = "Select a system message "
                End If
            Else
                If Mess_DGV.SelectedCells.Item(0).Value = "" Then
                    Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(0).Value = "Select a system message "
                End If
            End If
        End If

    End Sub

    Private Sub Text_TB_TextChanged(sender As Object, e As EventArgs) Handles Text_TB.TextChanged
        If Text_TB.Text = "" Then
            Validate_BTN.Enabled = False
        Else
            Validate_BTN.Enabled = True
        End If


    End Sub

    Private Sub Delete_BTN_Click_(sender As Object, e As EventArgs) Handles Delete_BTN.Click

        Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        cntsql.Open()

        Dim temp As String = ""
        Dim priority As String = Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString()
        Dim message As String = Mess_DGV.SelectedCells.Item(0).Value.ToString()

        If (Mess_DGV.SelectedCells.Item(0).Value.ToString() = "Daily Best CYCLE ON Machine") Then
            message = "_COND" + message
        End If
        If (Mess_DGV.SelectedCells.Item(0).Value.ToString() = "Weekly Best CYCLE ON Machine") Then
            message = "_CONW" + message
        End If
        If (Mess_DGV.SelectedCells.Item(0).Value.ToString() = "Monthly Best CYCLE ON Machine") Then
            message = "_CONM" + message
        End If

        Try
            Dim cmdsql As New MySqlCommand($"DELETE FROM CSI_Database.tbl_messages WHERE messages = '{ message }' AND IP_adress = '{ IP_TB.Text }' AND Priority = '{ priority }'", cntsql)
            cmdsql.ExecuteNonQuery()
        Catch
        End Try

        Mess_DGV.Rows.Remove(Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex))

        If Mess_DGV.Rows.Count = 0 Then
            Delete_BTN.Enabled = False
            UM_RB.Checked = False

            Text_TB.Visible = False
            Text_TB.Text = ""
            MT_GB.Enabled = False
            Label20.Visible = False
        End If
        Try
            If Mess_DGV.SelectedCells.Item(0).Value = Nothing Then
            End If
        Catch ex As Exception
            Try
                Mess_DGV.Rows(Mess_DGV.Rows.Count - 1).Cells(0).Selected = True
            Catch ex2 As Exception

            End Try

        End Try

        'For Each cell As DataGridViewCell In Mess_DGV.Rows.Item(0).Cells
        '    MessageBox.Show(cell()
        'Next
        If Not (Mess_DGV.Rows.Count = 0) Then
            If Not (Mess_DGV.SelectedCells.Item(0).RowIndex = 0) Then
                For i = Mess_DGV.SelectedCells.Item(0).RowIndex To Mess_DGV.Rows.Count - Mess_DGV.SelectedCells.Item(0).RowIndex
                    Mess_DGV.Rows(i).Cells(1).Value = i + 1
                Next
            Else
                'Mess_DGV.Rows(0).Cells(1).Value = 1
                For i = 0 To Mess_DGV.Rows.Count - 1
                    Mess_DGV.Rows(i).Cells(1).Value = i + 1
                Next

            End If

        End If

        For i = 0 To Mess_DGV.Rows.Count - 1
            Try
                Dim cmdsql As New MySqlCommand("update CSI_Database.tbl_messages set Priority = '" + Mess_DGV.Rows(i).Cells(1).Value.ToString() + "' where messages = '" + Mess_DGV.Rows(i).Cells(0).Value.ToString() + "'", cntsql)
                cmdsql.ExecuteNonQuery()
            Catch
            End Try

        Next
        Add_BTN.Enabled = True
        cntsql.Close()
        send_http_req()
    End Sub

    Private Sub Mess_DGV_SelectionChanged(sender As Object, e As EventArgs)

        Try
            If Mess_DGV.SelectedCells.Item(0).ColumnIndex = 1 Then
                RemoveHandler Mess_DGV.SelectionChanged, AddressOf Mess_DGV_SelectionChanged
                Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(0).Selected = True
                Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Selected = False
                AddHandler Mess_DGV.SelectionChanged, AddressOf Mess_DGV_SelectionChanged
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        Dim temptext As String = ""
        Try
            If Not Mess_DGV.SelectedCells.Item(0).Value = Nothing Then
                If Mess_DGV.SelectedCells.Item(0).ColumnIndex = 0 Then
                    temptext = Mess_DGV.SelectedCells.Item(0).Value.ToString()
                    Mess_DGV.SelectedCells.Item(0).Value = Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex - 1).Cells.Item(0).Value
                    Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex - 1).Cells.Item(0).Value = temptext
                    RemoveHandler Mess_DGV.SelectionChanged, AddressOf Mess_DGV_SelectionChanged
                    Dim temp As Integer = Mess_DGV.SelectedCells.Item(0).RowIndex
                    Mess_DGV.Rows(temp).Cells.Item(0).Selected = False
                    Mess_DGV.Rows(temp - 1).Cells.Item(0).Selected = True
                    AddHandler Mess_DGV.SelectionChanged, AddressOf Mess_DGV_SelectionChanged
                End If

                Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                cntsql.Open()
                Try
                    Dim cmdsql As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = 'temp' where Messages = '" + Mess_DGV.SelectedCells.Item(0).Value.ToString() + "'", cntsql)
                    cmdsql.Parameters.Add(New MySqlParameter("@message", Text_TB.Text))
                    cmdsql.ExecuteNonQuery()
                    Dim cmdsql2 As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex + 1).Cells(1).Value.ToString() + "' where Priority = '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "'", cntsql)
                    cmdsql2.Parameters.Add(New MySqlParameter("@message", Text_TB.Text))
                    cmdsql2.ExecuteNonQuery()
                    Dim cmdsql3 As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "' where Priority = 'temp'", cntsql)
                    cmdsql3.Parameters.Add(New MySqlParameter("@message", Text_TB.Text))
                    cmdsql3.ExecuteNonQuery()
                Catch ex As Exception

                Finally
                    cntsql.Close()
                End Try



            End If
        Catch
        End Try
    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Dim temptext As String = ""
        Try
            If Not Mess_DGV.SelectedCells.Item(0).Value = Nothing Then
                If Mess_DGV.SelectedCells.Item(0).ColumnIndex = 0 Then
                    temptext = Mess_DGV.SelectedCells.Item(0).Value.ToString()
                    Mess_DGV.SelectedCells.Item(0).Value = Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex + 1).Cells.Item(0).Value
                    Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex + 1).Cells.Item(0).Value = temptext
                    RemoveHandler Mess_DGV.SelectionChanged, AddressOf Mess_DGV_SelectionChanged
                    Dim temp As Integer = Mess_DGV.SelectedCells.Item(0).RowIndex
                    Mess_DGV.Rows(temp).Cells.Item(0).Selected = False
                    Mess_DGV.Rows(temp + 1).Cells.Item(0).Selected = True
                    AddHandler Mess_DGV.SelectionChanged, AddressOf Mess_DGV_SelectionChanged
                End If
                Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                cntsql.Open()
                Try
                    Dim cmdsql As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = 'temp' where Messages = '" + Mess_DGV.SelectedCells.Item(0).Value.ToString() + "'", cntsql)
                    cmdsql.Parameters.Add(New MySqlParameter("@message", Text_TB.Text))
                    cmdsql.ExecuteNonQuery()
                    Dim cmdsql2 As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex - 1).Cells(1).Value.ToString() + "' where Priority = '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "'", cntsql)
                    cmdsql2.Parameters.Add(New MySqlParameter("@message", Text_TB.Text))
                    cmdsql2.ExecuteNonQuery()
                    Dim cmdsql3 As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "' where Priority = 'temp'", cntsql)
                    cmdsql3.Parameters.Add(New MySqlParameter("@message", Text_TB.Text))
                    cmdsql3.ExecuteNonQuery()
                Catch ex As Exception

                Finally
                    cntsql.Close()
                End Try



            End If
        Catch
        End Try
    End Sub

    Private Sub Validate_BTN_Click(sender As Object, e As EventArgs) Handles Validate_BTN.Click

        'Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        'cntsql.Open()

        Dim sqlCmd As Text.StringBuilder = New System.Text.StringBuilder()

        Dim priority As String = Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString()
        Dim oldMessage As String = Mess_DGV.SelectedCells.Item(0).Value.ToString()
        Dim newMessage As String = Text_TB.Text

        If RadioButton2.Checked Then

            If Day_RB.Checked Then
                oldMessage = "_COND" + oldMessage
                newMessage = "_COND" + newMessage
            ElseIf Week_RB.Checked Then
                oldMessage = "_CONW" + oldMessage
                newMessage = "_CONW" + newMessage
            ElseIf Month_RB.Checked Then
                oldMessage = "_CONM" + oldMessage
                newMessage = "_CONM" + newMessage
            End If

        End If

        If Mess_DGV.SelectedCells.Item(0).Value = "Enter a message text" Or Mess_DGV.SelectedCells.Item(0).Value = "Select a period" Then

            sqlCmd.Append($"INSERT INTO CSI_Database.tbl_messages ")
            sqlCmd.Append($" (                                    ")
            sqlCmd.Append($"   deviceId   ,                       ")
            sqlCmd.Append($"   name       ,                       ")
            sqlCmd.Append($"   ip_adress  ,                       ")
            sqlCmd.Append($"   messages   ,                       ")
            sqlCmd.Append($"   Priority                           ")
            sqlCmd.Append($" ) VALUES (                           ")
            sqlCmd.Append($"    { deviceId }     ,                ")
            sqlCmd.Append($"   '{ Name_TB.Text }',                ")
            sqlCmd.Append($"   '{ IP_TB.Text }'  ,                ")
            sqlCmd.Append($"   @message          ,                ")
            sqlCmd.Append($"   '{ priority }'                     ")
            sqlCmd.Append($" )                                    ")
            sqlCmd.Append($" ON DUPLICATE KEY                     ")
            sqlCmd.Append($"      UPDATE messages=(@message)      ")

        Else
            sqlCmd.Append($"UPDATE CSI_Database.tbl_messages ")
            sqlCmd.Append($" SET                             ")
            sqlCmd.Append($"     messages = @message,        ")
            sqlCmd.Append($"     Priority = '{ priority }'   ")
            sqlCmd.Append($" WHERE                           ")
            sqlCmd.Append($"     deviceId =  { deviceId }    ")
            sqlCmd.Append($" AND messages = '{ oldMessage }' ")
        End If

        Dim command As New MySqlCommand(sqlCmd.ToString())
        command.Parameters.Add(New MySqlParameter("@message", newMessage))

        MySqlAccess.ExecuteNonQuery(command)


        'If UM_RB.Checked = True Then
        '    If Mess_DGV.SelectedCells.Item(0).Value = "Enter a message text" Then

        '        Dim cmdsql As New MySqlCommand("INSERT INTO CSI_Database.tbl_messages (name, ip_adress, messages, Priority) VALUES('" + Name_TB.Text + "', '" + IP_TB.Text + "', @message , '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "') " +
        '                                                   "ON DUPLICATE KEY UPDATE messages=(@message)", cntsql)
        '        cmdsql.Parameters.Add(New MySqlParameter("@message", Text_TB.Text))
        '        cmdsql.ExecuteNonQuery()
        '    Else
        '        Dim cmdsql As New MySqlCommand("update CSI_Database.tbl_messages SET messages=(@message) and Priority = '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "' where messages = '" + Mess_DGV.SelectedCells.Item(0).Value.ToString() + "'", cntsql)
        '        cmdsql.Parameters.Add(New MySqlParameter("@message", Text_TB.Text))
        '        cmdsql.ExecuteNonQuery()
        '    End If
        'End If

        'If RadioButton2.Checked = True Then
        '    If (Day_RB.Checked) Then
        '        If Mess_DGV.SelectedCells.Item(0).Value = "Select a period" Then
        '            Dim cmdsql As New MySqlCommand("INSERT INTO CSI_Database.tbl_messages (name, ip_adress, messages, Priority) VALUES('" + Name_TB.Text + "', '" + IP_TB.Text + "', @message , '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "') " +
        '                                                       "ON DUPLICATE KEY UPDATE messages=(@message)", cntsql)
        '            cmdsql.Parameters.Add(New MySqlParameter("@message", "_COND" + Text_TB.Text))
        '            cmdsql.ExecuteNonQuery()
        '        Else
        '            Dim cmdsql As New MySqlCommand("update CSI_Database.tbl_messages SET messages=(@message) and Priority = '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "' where messages =  '_COND" + Mess_DGV.SelectedCells.Item(0).Value.ToString() + "'", cntsql)
        '            cmdsql.Parameters.Add(New MySqlParameter("@message", "_COND" + Text_TB.Text))
        '            cmdsql.ExecuteNonQuery()
        '        End If

        '    End If

        '    If (Week_RB.Checked) Then
        '        If Mess_DGV.SelectedCells.Item(0).Value = "Select a period" Then
        '            Dim cmdsql As New MySqlCommand("INSERT INTO CSI_Database.tbl_messages (name, ip_adress, messages, Priority) VALUES('" + Name_TB.Text + "', '" + IP_TB.Text + "', @message , '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "') " +
        '                                                       "ON DUPLICATE KEY UPDATE messages=(@message)", cntsql)
        '            cmdsql.Parameters.Add(New MySqlParameter("@message", "_CONW" + Text_TB.Text))
        '            cmdsql.ExecuteNonQuery()
        '        Else
        '            Dim cmdsql As New MySqlCommand("update CSI_Database.tbl_messages SET messages='" + Text_TB.Text + "' and Priority = '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "' where messages = '_CONW" + Mess_DGV.SelectedCells.Item(0).Value.ToString() + "'", cntsql)
        '            'cmdsql.Parameters.Add(New MySqlParameter("@message", "_CONW" + Text_TB.Text))
        '            cmdsql.ExecuteNonQuery()
        '        End If
        '    End If

        '    If (Month_RB.Checked) Then
        '        If Mess_DGV.SelectedCells.Item(0).Value = "Select a period" Then
        '            Dim cmdsql As New MySqlCommand("INSERT INTO CSI_Database.tbl_messages (name, ip_adress, messages, Priority) VALUES('" + Name_TB.Text + "', '" + IP_TB.Text + "', @message , '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "') " +
        '                                                       "ON DUPLICATE KEY UPDATE messages=(@message)", cntsql)
        '            cmdsql.Parameters.Add(New MySqlParameter("@message", "_CONM" + Text_TB.Text))
        '            cmdsql.ExecuteNonQuery()
        '        Else
        '            Dim cmdsql As New MySqlCommand("update CSI_Database.tbl_messages SET messages=(@message) and Priority = '" + Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "' where messages = '_CONM" + Mess_DGV.SelectedCells.Item(0).Value.ToString() + "'", cntsql)
        '            cmdsql.Parameters.Add(New MySqlParameter("@message", "_CONM" + Text_TB.Text))
        '            cmdsql.ExecuteNonQuery()
        '        End If
        '    End If

        'End If

        'cntsql.Close()

        If Mess_DGV.SelectedCells.Item(0).ColumnIndex = 0 Then
            Mess_DGV.SelectedCells.Item(0).Value = Text_TB.Text
        Else
            Mess_DGV.Rows(Mess_DGV.SelectedCells.Item(0).RowIndex).Cells(0).Value = Text_TB.Text
        End If
        If Text_TB.Text = "" Then
            Validate_BTN.Enabled = False
        Else
            Validate_BTN.Enabled = True
        End If
        Text_TB.Text = ""
        Con_GB.Visible = False
        Text_TB.Visible = False
        Label15.Visible = False
        Con_TB.Visible = False
        MT_GB.Enabled = False
        Label20.Visible = False
        Mess_DGV.Enabled = True
        Button4.Enabled = True
        Button6.Enabled = True
        Add_BTN.Enabled = True

        RefreshDevice(IP_TB.Text)

    End Sub

    Private Sub IP_TB_Validated_1(sender As Object, e As EventArgs)
        Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        cntsql.Open()
        Try
            Dim MySql As String = "update CSI_database.tbl_devices SET ip_adress = '" & IP_TB.Text & "' WHERE deviceName = '" & treeviewDevices.SelectedNode.Text & "'"
            Dim cmd As New MySqlCommand(MySql, cntsql)
            cmd.ExecuteNonQuery()
            MySql = "commit"
            cmd = New MySqlCommand(MySql, cntsql)
            cmd.ExecuteNonQuery()
            MySql = "update CSI_database.tbl_deviceconfig SET ip = '" & IP_TB.Text & "' WHERE Name = '" & treeviewDevices.SelectedNode.Text & "'"
            cmd = New MySqlCommand(MySql, cntsql)
            cmd.ExecuteNonQuery()
            MySql = "commit"
            MySql = "update CSI_database.tbl_deviceconfig2 SET IP_adress = '" & IP_TB.Text & "' WHERE Name = '" & treeviewDevices.SelectedNode.Text & "'"
            cmd = New MySqlCommand(MySql, cntsql)
            cmd.ExecuteNonQuery()
            MySql = "commit"
            cmd = New MySqlCommand(MySql, cntsql)
            cmd.ExecuteNonQuery()
            MySql = "update CSI_database.tbl_messages SET IP_adress = '" & IP_TB.Text & "' WHERE name = '" & treeviewDevices.SelectedNode.Text & "'"
            cmd = New MySqlCommand(MySql, cntsql)
            cmd.ExecuteNonQuery()
            MySql = "commit"
            cmd = New MySqlCommand(MySql, cntsql)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Could not update the IP : " + ex.Message)
        End Try

        cntsql.Close()
    End Sub

    '<PrincipalPermission(SecurityAction.Demand, Role:="Administrators")> _
    '<PrincipalPermission(SecurityAction.Demand, Role:="BUILTIN\Administrators", Authenticated:=False)> _
    Private Sub ____UninstallService()
        Try
            Dim Program86Path = CSI_Library.CSI_Library.ServerProgramFilesPath
            Dim ProgramDataPath = CSI_Library.CSI_Library.serverRootPath
            Dim PathForMobileServer = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSIFlexMobileServer"
            Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            Dim ListOfServices As New List(Of String)
            ListOfServices.Clear()
            ListOfServices.Add("MTCFocasAgent Service")
            ListOfServices.Add("CSIFlexServerService")
            ListOfServices.Add("CSIFlex_Reports_Generator_Service")
            ListOfServices.Add("CSIFlexPHPServer")
            ListOfServices.Add("MYSQLSERVICE")
            Dim info As New ProcessStartInfo()
            Dim uninstallProcess As New Process()
            Dim DatabasePathForEXE = Program86Path & "\CSI Flex Server\mysql\mysql-5.7.21-win32\bin\mysqld.exe"
            Dim Found As Boolean = False
            Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\", True)
            Dim regKey3 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\", True)
            Dim AllInstalledApps = regKey.GetSubKeyNames()
            Dim Another = regKey3.GetSubKeyNames()
            Dim Combined = AllInstalledApps.Concat(Another).ToArray()
            Dim result = MessageBox.Show("Do you want to keep CSIFlex Configuration Settings ?", "Uninstall CSIFlex Server", MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Yes Then
                isUnistalling = True
                'Stop all Adapters if they exists 
                cntsql.Open()
                Dim mysqlSelectAllAdapters As String = "SELECT * FROM csi_auth.tbl_csiconnector;"
                Dim cmdSelectAllAdapters As New MySqlCommand(mysqlSelectAllAdapters, cntsql)
                Dim mysqlReaderSelectAllAdapters As MySqlDataReader = cmdSelectAllAdapters.ExecuteReader
                Dim dTable_SelectSelectAllAdapters As New DataTable()
                dTable_SelectSelectAllAdapters.Load(mysqlReaderSelectAllAdapters)
                If dTable_SelectSelectAllAdapters.Rows.Count > 0 Then
                    For Each row As DataRow In dTable_SelectSelectAllAdapters.Rows
                        ListOfServices.Add(row("AdapterServiceName"))
                        If (ServiceTools.ServiceInstaller.GetServiceStatus(row("AdapterServiceName")) = ServiceTools.ServiceState.Run) Then
                            'FocasLibrary.Tools.AdapterManagement.Stop(row("AdapterServiceName"))
                            CSI_Lib.KillingAProcess(row("AdapterServiceName"))
                        End If
                        FocasLibrary.Tools.AdapterManagement.Uninstall(row("AdapterServiceName"))
                    Next
                End If
                cntsql.Close()
                'While ServiceTools.ServiceInstaller.ServiceIsInstalled("MTCFocasAgent Service")
                If (ServiceTools.ServiceInstaller.GetServiceStatus("MTCFocasAgent Service") = ServiceTools.ServiceState.Run) Then
                    'FocasLibrary.Tools.AgentManagement.Stop()
                    CSI_Lib.KillingAProcess("MTCFocasAgent Service")
                End If
                Dim regKey6 As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\MTCFocasAgent Service", True)
                regKey6.SetValue("Start", 4)
                regKey6.Close()
                'HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MTCFocasAgent Service :: Location of the service in registry
                'Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\MTCFocasAgent Service", True)
                'Dim startvalue As Decimal
                'Dim info As New ProcessStartInfo()
                'Dim uninstallProcess As New Process()
                'For Each ProcessName As String In ListOfServices
                '    Dim nos = regKey.GetSubKeyNames() '.Where(Function(n) n.ToLower().Contains(ProcessName))
                '    For Each names As String In nos
                '        MessageBox.Show(names)
                '    Next
                '    Dim ns = regKey.GetSubKeyNames().Where(Function(n) n.Equals(ProcessName))
                '    If ns.Count > 0 Then
                '        For Each vsKey As String In regKey.GetSubKeyNames()
                '            Dim productKey As RegistryKey = regKey.OpenSubKey(vsKey)
                '            Dim displayName As String = Convert.ToString(productKey.GetValue("DisplayName"))
                '            Dim uninstallString As String = Convert.ToString(productKey.GetValue("UninstallString"))
                '            If displayName.Equals(ProcessName) Then
                '                Dim ProductId As String = uninstallString.Substring(uninstallString.IndexOf("{"))
                '                uninstallProcess.StartInfo.FileName = "MsiExec.exe"
                '                uninstallProcess.StartInfo.Arguments = " /x " & ProductId & " /Qn"
                '                uninstallProcess.Start()
                '            End If
                '        Next
                '    End If
                'Next
                FocasLibrary.Tools.AdapterManagement.Uninstall("MTCFocasAgent Service")
                ' End While
                'While ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlexServerService")
                If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reports_Generator_Service") = ServiceTools.ServiceState.Run) Then
                    CSI_Lib.KillingAProcess("CSIFlex_Reports_Generator_Service")
                End If
                FocasLibrary.Tools.AdapterManagement.Uninstall("CSIFlex_Reports_Generator_Service")
                If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Run) Then
                    'FocasLibrary.Tools.AdapterManagement.Stop("CSIFlexServerService")
                    CSI_Lib.KillingAProcess("CSIFlexServerService")
                End If
                FocasLibrary.Tools.AdapterManagement.Uninstall("CSIFlexServerService")
                'Before Stopping MySql Server We need to Stop PHP Server for Mobile Also
                If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexPHPServer") = ServiceTools.ServiceState.Run) Then
                    'FocasLibrary.Tools.AdapterManagement.Stop("CSIFlexPHPServer")
                    CSI_Lib.KillingAProcess("CSIFlexPHPServer")
                End If
                FocasLibrary.Tools.AdapterManagement.Uninstall("CSIFlexPHPServer")
                'Unlock below code for MYSQLSERVICE
                If (ServiceTools.ServiceInstaller.GetServiceStatus("MYSQLSERVICE") = ServiceTools.ServiceState.Run) Then
                    'FocasLibrary.Tools.AdapterManagement.StopMysql("MYSQLSERVICE")
                    CSI_Lib.KillingAProcess("MYSQLSERVICE")
                End If
                FocasLibrary.Tools.AdapterManagement.Uninstall("MYSQLSERVICE")
                'Unistall from Control Panel 

                For Each names In Combined
                    Dim regKey1 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\" & names, True)
                    Dim regKey4 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" & names, True)
                    If regKey1.GetValue("DisplayName") = "CSIFlex Server" Then
                        Dim uninstallString As String = regKey1.GetValue("UninstallString")
                        Dim ProductId As String = uninstallString.Substring(uninstallString.IndexOf("{"))
                        uninstallProcess.StartInfo.FileName = "MsiExec.exe"
                        uninstallProcess.StartInfo.Arguments = " /x " & ProductId & " /Qn"
                        uninstallProcess.Start()
                        'MessageBox.Show("Installation Path : " & regKey1.GetValue("InstallSource") & Environment.NewLine & "Unistallation String : " & regKey1.GetValue("UninstallString") & Environment.NewLine & "Version : " & regKey1.GetValue("DisplayVersion"))
                        Found = True
                        Exit For
                    ElseIf regKey4.GetValue("DisplayName") = "CSIFlex Server" Then
                        Dim uninstallString As String = regKey4.GetValue("UninstallString")
                        Dim ProductId As String = uninstallString.Substring(uninstallString.IndexOf("{"))
                        uninstallProcess.StartInfo.FileName = "MsiExec.exe"
                        uninstallProcess.StartInfo.Arguments = " /x " & ProductId & " /Qn"
                        uninstallProcess.Start()
                        'MessageBox.Show("Installation Path : " & regKey4.GetValue("InstallSource") & Environment.NewLine & "Unistallation String : " & regKey4.GetValue("UninstallString") & Environment.NewLine & "Version : " & regKey4.GetValue("DisplayVersion"))
                        Found = True
                        Exit For
                    End If
                Next
                'Show All .dll Files in Program86/CSIFlex Server Folder
                'We are let the thread pause to properly stop all the services 
                Thread.Sleep(10000)
                'Dim files() As String
                'files = Directory.GetFiles(CSI_Library.CSI_Library.ServerProgramFilesPath, "*.dll", SearchOption.TopDirectoryOnly)
                'For Each FileName As String In files
                '    File.SetAttributes(FileName, FileAttributes.Normal + FileAttributes.System)
                'Next
                'Directory.Delete(PathForMobileServer, True)
                MessageBox.Show("CSIFlex Server is uninstalled successfully ! You must need to restart your computer to apply all changes ! To use it again install CSIFlex Setup (.exe) File ! Thank you !")
                Me.Close()
            ElseIf result = DialogResult.No Then
                isUnistalling = True
                'Stop all Adapters if they exists 
                cntsql.Open()
                Dim mysqlSelectAllAdapters As String = "SELECT * FROM csi_auth.tbl_csiconnector;"
                Dim cmdSelectAllAdapters As New MySqlCommand(mysqlSelectAllAdapters, cntsql)
                Dim mysqlReaderSelectAllAdapters As MySqlDataReader = cmdSelectAllAdapters.ExecuteReader
                Dim dTable_SelectSelectAllAdapters As New DataTable()
                dTable_SelectSelectAllAdapters.Load(mysqlReaderSelectAllAdapters)
                If dTable_SelectSelectAllAdapters.Rows.Count > 0 Then
                    For Each row As DataRow In dTable_SelectSelectAllAdapters.Rows
                        ListOfServices.Add(row("AdapterServiceName"))
                        If (ServiceTools.ServiceInstaller.GetServiceStatus(row("AdapterServiceName")) = ServiceTools.ServiceState.Run) Then
                            'FocasLibrary.Tools.AdapterManagement.Stop(row("AdapterServiceName"))
                            CSI_Lib.KillingAProcess(row("AdapterServiceName"))
                        End If
                        FocasLibrary.Tools.AdapterManagement.Uninstall(row("AdapterServiceName"))
                    Next
                End If
                cntsql.Close()
                'While ServiceTools.ServiceInstaller.ServiceIsInstalled("MTCFocasAgent Service")
                If (ServiceTools.ServiceInstaller.GetServiceStatus("MTCFocasAgent Service") = ServiceTools.ServiceState.Run) Then
                    'FocasLibrary.Tools.AgentManagement.Stop()
                    CSI_Lib.KillingAProcess("MTCFocasAgent Service")
                End If
                Dim regKey6 As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\MTCFocasAgent Service", True)
                regKey6.SetValue("Start", 4)
                regKey6.Close()
                FocasLibrary.Tools.AdapterManagement.Uninstall("MTCFocasAgent Service")
                'End While
                'While ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlexServerService")
                If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reports_Generator_Service") = ServiceTools.ServiceState.Run) Then
                    CSI_Lib.KillingAProcess("CSIFlex_Reports_Generator_Service")
                End If
                FocasLibrary.Tools.AdapterManagement.Uninstall("CSIFlex_Reports_Generator_Service")
                If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Run) Then
                    'FocasLibrary.Tools.AdapterManagement.Stop("CSIFlexServerService")
                    CSI_Lib.KillingAProcess("CSIFlexServerService")
                End If
                FocasLibrary.Tools.AdapterManagement.Uninstall("CSIFlexServerService")
                'Before Stopping MySql Server We need to Stop PHP Server for Mobile Also
                If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexPHPServer") = ServiceTools.ServiceState.Run) Then
                    'FocasLibrary.Tools.AdapterManagement.Stop("CSIFlexPHPServer")
                    CSI_Lib.KillingAProcess("CSIFlexPHPServer")
                End If
                FocasLibrary.Tools.AdapterManagement.Uninstall("CSIFlexPHPServer")
                'Unlock below code for MYSQLSERVICE
                If (ServiceTools.ServiceInstaller.GetServiceStatus("MYSQLSERVICE") = ServiceTools.ServiceState.Run) Then
                    'FocasLibrary.Tools.AdapterManagement.StopMysql("MYSQLSERVICE")
                    CSI_Lib.KillingAProcess("MYSQLSERVICE")
                End If
                FocasLibrary.Tools.AdapterManagement.Uninstall("MYSQLSERVICE")
                'Now remove all the Processes from the Registry 
                'Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{5D03D875-5E8D-45F0-A048-5F85E47C267F}
                'Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\") 'HKEY_LOCAL_MACHINE\
                'Dim info As New ProcessStartInfo()
                'Dim uninstallProcess As New Process()
                'For Each ProcessName As String In ListOfServices
                '    'Dim ns = regKey.GetSubKeyNames().Where(Function(n) n.ToLower().Contains(ProcessName))
                '    Dim ns = regKey.GetSubKeyNames().Where(Function(n) n.Equals(ProcessName))
                '    If ns.Count > 0 Then
                '        For Each vsKey As String In regKey.GetSubKeyNames()
                '            Dim productKey As RegistryKey = regKey.OpenSubKey(vsKey)
                '            Dim displayName As String = Convert.ToString(productKey.GetValue("DisplayName"))
                '            Dim uninstallString As String = Convert.ToString(productKey.GetValue("UninstallString"))
                '            If displayName.Equals(ProcessName) Then
                '                Dim ProductId As String = uninstallString.Substring(uninstallString.IndexOf("{"))
                '                uninstallProcess.StartInfo.FileName = "MsiExec.exe"
                '                uninstallProcess.StartInfo.Arguments = " /x " & ProductId & " /Qn"
                '                uninstallProcess.Start()
                '            End If
                '        Next
                '    End If
                'Next
                For Each names In Combined
                    Dim regKey1 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\" & names, True)
                    Dim regKey4 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" & names, True)
                    If regKey1.GetValue("DisplayName") = "CSIFlex Server" Then
                        Dim uninstallString As String = regKey1.GetValue("UninstallString")
                        Dim ProductId As String = uninstallString.Substring(uninstallString.IndexOf("{"))
                        uninstallProcess.StartInfo.FileName = "MsiExec.exe"
                        uninstallProcess.StartInfo.Arguments = " /x " & ProductId & " /Qn"
                        uninstallProcess.Start()
                        'MessageBox.Show("Installation Path : " & regKey1.GetValue("InstallSource") & Environment.NewLine & "Unistallation String : " & regKey1.GetValue("UninstallString") & Environment.NewLine & "Version : " & regKey1.GetValue("DisplayVersion"))
                        Found = True
                        Exit For
                    ElseIf regKey4.GetValue("DisplayName") = "CSIFlex Server" Then
                        Dim uninstallString As String = regKey4.GetValue("UninstallString")
                        Dim ProductId As String = uninstallString.Substring(uninstallString.IndexOf("{"))
                        uninstallProcess.StartInfo.FileName = "MsiExec.exe"
                        uninstallProcess.StartInfo.Arguments = " /x " & ProductId & " /Qn"
                        uninstallProcess.Start()
                        'MessageBox.Show("Installation Path : " & regKey4.GetValue("InstallSource") & Environment.NewLine & "Unistallation String : " & regKey4.GetValue("UninstallString") & Environment.NewLine & "Version : " & regKey4.GetValue("DisplayVersion"))
                        Found = True
                        Exit For
                    End If
                Next
                'Show All .dll Files in Program86/CSIFlex Server Folder
                Thread.Sleep(10000)
                'Dim files() As String
                'files = Directory.GetFiles(CSI_Library.CSI_Library.ServerProgramFilesPath, "*.dll", SearchOption.TopDirectoryOnly)
                'For Each FileName As String In files
                '    File.SetAttributes(FileName, FileAttributes.Normal + FileAttributes.System)
                'Next
                'Directory.Delete(Program86Path, True)
                Directory.Delete(ProgramDataPath, True)
                'Directory.Delete(PathForMobileServer, True)
                MessageBox.Show("CSIFlex Server is uninstalled successfully ! You must need to restart your computer to apply all changes ! To use it again install CSIFlex Setup (.exe) File ! Thank you !")
                Me.Close()
            ElseIf result = DialogResult.Cancel Then
                'Do nothing but cancel the operation
            End If
            isUnistalling = False
        Catch ex As Exception
            MessageBox.Show("There was an error trying to uninstall the service. See the log for more details" & ex.Message.ToString() & "StackTrace :" & ex.StackTrace())
            'CSI_Lib.LogServiceError(ex.Message, 1)
            Me.Close()
            Directory.Delete("C:\Program Files (x86)\CSI Flex Server", True)
            isUnistalling = False
        End Try
    End Sub

    Private Sub BGW_ServiceState_DoWork(sender As Object, e As DoWorkEventArgs)
        Try
            Dim worker As BackgroundWorker = DirectCast(sender, BackgroundWorker)
            Dim servicestatus As String = ""
            While Not worker.CancellationPending
                'Do your stuff here
                servicestatus = ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService")

                'Select Case servicestatus
                '    Case ServiceTools.ServiceState.NotFound
                '        LBL_ServiceState.Text = "Not Installed"
                '    Case ServiceTools.ServiceState.Run
                '        LBL_ServiceState.Text = "Running"
                '    Case ServiceTools.ServiceState.Starting
                '        LBL_ServiceState.Text = "Starting"
                '    Case ServiceTools.ServiceState.Stop
                '        LBL_ServiceState.Text = "Stopped"
                '    Case ServiceTools.ServiceState.Stopping
                '        LBL_ServiceState.Text = "Stopping"
                '    Case ServiceTools.ServiceState.Unknown
                '        LBL_ServiceState.Text = "Unknown"
                '    Case Else
                '        LBL_ServiceState.Text = "Unknown"
                'End Select

                'If (servicestatus = ServiceTools.ServiceState.Run) Then
                '    LBL_ServiceState.BackColor = Color.Green
                'Else
                '    LBL_ServiceState.BackColor = Color.Red
                'End If

                worker.ReportProgress(0, servicestatus)
                Thread.Sleep(300)
            End While
        Catch ex As Exception
            CSI_Lib.LogServiceError("Unable to report state:" + ex.Message, 1)
        End Try
    End Sub

    Private Sub BGW_ServiceState_ProgressChanged(sender As Object, e As ProgressChangedEventArgs)
        Dim servicestatus As Object = e.UserState
        Dim percentage As Integer = e.ProgressPercentage

        Select Case servicestatus
            Case ServiceTools.ServiceState.NotFound
                lblServiceState.Text = "Not Installed"
            Case ServiceTools.ServiceState.Run
                lblServiceState.Text = "Running"
            Case ServiceTools.ServiceState.Starting
                lblServiceState.Text = "Starting"
            Case ServiceTools.ServiceState.Stop
                lblServiceState.Text = "Stopped"
            Case ServiceTools.ServiceState.Stopping
                lblServiceState.Text = "Stopping"
            Case ServiceTools.ServiceState.Unknown
                lblServiceState.Text = "Unknown"
            Case Else
                lblServiceState.Text = "Unknown"
        End Select

        If (servicestatus = ServiceTools.ServiceState.Run) Then
            lblServiceState.BackColor = Color.LimeGreen
            'LBL_ServiceState.BackColor = Color.Green
        Else
            lblServiceState.BackColor = Color.Red
        End If
    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs)

        If BgWorker_importDB.IsBusy Then
            MessageBox.Show("Another CSV file is being imported, Please wait.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Information)
        ElseIf BgWorker_CreateDB.IsBusy Then
            MessageBox.Show("Initial database creation is in process, Please wait.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Information)
        Else

            Dim folderDlg As New OpenFileDialog
            'Dim notImport As Boolean = True
            Try
                If (folderDlg.ShowDialog()) Then

                    If Not (folderDlg.FileName.Substring(folderDlg.FileName.Length - 4, 4) = ".CSV") Then
                        MessageBox.Show("You must select a .CSV file")
                    Else

                        BgWorker_importDB.RunWorkerAsync(folderDlg.FileName)
                        'End If
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

    End Sub

    Private Sub AddGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddGroupToolStripMenuItem.Click

        'If TV_GroupsOfMachines.Nodes(0).Nodes.Count = 0 Then
        '    TV_GroupsOfMachines.ExpandAll()
        'End If
        'This function adds new group to the Group Treeview
        Dim groupname As String = "Group" & TV_GroupsOfMachines.Nodes(0).Nodes.Count.ToString()
        TV_GroupsOfMachines.Nodes(0).Nodes.Add(groupname, groupname)
        TV_GroupsOfMachines.Nodes(0).Expand()
        AddGroup(groupname)
        ancient_name = groupname
        TV_GroupsOfMachines.LabelEdit = True
        TV_GroupsOfMachines.Nodes(0).Nodes(TV_GroupsOfMachines.Nodes(0).Nodes.Count - 1).BeginEdit()
        'TV_GroupsOfMachines.LabelEdit = False
    End Sub

    Private Sub AddGroup(groupname As String)

        Dim sqlCmd As String = $"INSERT IGNORE INTO CSI_Database.tbl_Groups (users, `groups`) VALUES('{ CSI_Library.CSI_Library.username }', '{ groupname }' )"
        MySqlAccess.ExecuteNonQuery(sqlCmd)

        Comput_perf_required = True
        CSIFlexServiceLib.read_dashboard_config_fromdb()
        UpdateTargetsTitle()
        RefreshAllDevices()
        ComputePerfReq()
    End Sub

    Private Sub TV_GroupsOfMachines_MouseUp(sender As Object, e As MouseEventArgs) Handles TV_GroupsOfMachines.MouseUp

        If e.Button = Windows.Forms.MouseButtons.Right Then


            ' Point where the mouse is clicked.
            Dim p As New Drawing.Point(e.X, e.Y)

            ' Get the node that the user has clicked.
            Dim node As TreeNode = TV_GroupsOfMachines.GetNodeAt(p)
            If node IsNot Nothing Then

                ' Select the node the user has clicked.
                ' The node appears selected until the menu is displayed on the screen.
                Dim m_OldSelectNode = TV_GroupsOfMachines.SelectedNode
                TV_GroupsOfMachines.SelectedNode = node

                ' Find the appropriate ContextMenu depending on the selected node.
                Dim nodename As String = Convert.ToString(node.Name)

                If (node.Level = 0) Then
                    CMS_GroupEdit_NEW.Items(0).Visible = True
                    CMS_GroupEdit_NEW.Items(1).Visible = False
                    CMS_GroupEdit_NEW.Items(2).Visible = False
                    CMS_GroupEdit_NEW.Items(3).Visible = False
                    CMS_GroupEdit_NEW.Show(TV_GroupsOfMachines, p)
                ElseIf (node.Level = 1) Then
                    CMS_GroupEdit_NEW.Items(0).Visible = False
                    CMS_GroupEdit_NEW.Items(1).Visible = True
                    CMS_GroupEdit_NEW.Items(2).Visible = True
                    CMS_GroupEdit_NEW.Items(3).Visible = False
                    CMS_GroupEdit_NEW.Show(TV_GroupsOfMachines, p)
                ElseIf (node.Level = 2) Then
                    CMS_GroupEdit_NEW.Items(0).Visible = False
                    CMS_GroupEdit_NEW.Items(1).Visible = False
                    CMS_GroupEdit_NEW.Items(2).Visible = False
                    CMS_GroupEdit_NEW.Items(3).Visible = True
                    CMS_GroupEdit_NEW.Show(TV_GroupsOfMachines, p)
                End If

                'Select Case Convert.ToString(node.Name)
                '    Case "Groups of machines"
                '        CMS_GroupEdit_NEW.Items(0).Visible = True
                '        CMS_GroupEdit_NEW.Items(1).Visible = False
                '        CMS_GroupEdit_NEW.Items(2).Visible = False
                '        CMS_GroupEdit_NEW.Show(TV_GroupsOfMachines, p)
                '        Exit Select
                '    case
                'End Select

                '' Highlight the selected node.
                'TreeView2.SelectedNode = m_OldSelectNode
                'm_OldSelectNode = Nothing
            End If

        End If
    End Sub

    'THIS METHOD DELETES MACHINES FROM THE GROUPS AND UPDATE THE GROUP TABLE
    Private Sub DeleteMachineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteMachineToolStripMenuItem.Click

        Dim tn As TreeNode = TV_GroupsOfMachines.SelectedNode

        If tn IsNot Nothing And Not tn.Text = "Groups of machines" Then

            selected_node_1 = Nothing

            'DELETE A MACHINE FROM GROUP TABLE 
            'Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)

            Try
                Dim groupName = TV_GroupsOfMachines.SelectedNode.Parent.FullPath.Split("\").Last()
                Dim machineId = TV_GroupsOfMachines.SelectedNode.Tag

                Dim cmd As Text.StringBuilder = New Text.StringBuilder()
                cmd.Append($"DELETE FROM                          ")
                cmd.Append($"   CSI_database.tbl_Groups           ")
                cmd.Append($"WHERE                                ")
                cmd.Append($"      `groups`    = '{ groupName }'  ")
                cmd.Append($"  AND `machineId` =  { machineId }   ")

                MySqlAccess.ExecuteNonQuery(cmd.ToString())

                'cntsql.Open()
                ''Dim mysql3 As String = "delete from CSI_database.tbl_Groups WHERE groups = '" + TV_GroupsOfMachines.SelectedNode.Parent.FullPath.Split("\").Last() + "' and users = '" + CSI_Library.CSI_Library.username + "' and machines = '" + tn.Text + "';SELECT * FROM csi_database.tbl_groups;"
                'Dim mysql3 As String = "delete from CSI_database.tbl_Groups WHERE `groups` = '" + TV_GroupsOfMachines.SelectedNode.Parent.FullPath.Split("\").Last() + "' and machines = '" + tn.Text + "';SELECT * FROM csi_database.tbl_groups;"
                'Dim cmdCreateDeviceTable3 As New MySqlCommand(mysql3, cntsql)
                'Dim mysqlReader3 As MySqlDataReader = cmdCreateDeviceTable3.ExecuteReader
                'cntsql.Close()

                'RefreshAllDevices()
                'MessageBox.Show("Group (Path) :" + TV_GroupsOfMachines.SelectedNode.Parent.FullPath.Split("\").Last() + "Machine :" + tn.Text)
            Catch ex As Exception
                'cntsql.Close()
                'CSIFlexServiceLib.read_dashboard_config_fromdb()
                'RefreshAllDevices()
                'RefreshAllDevices()

                Log.Error(ex)
            End Try

            TV_GroupsOfMachines.Nodes(0).Nodes.Clear()

            Load_groupes(CSI_Library.CSI_Library.MySqlConnectionString)

            Comput_perf_required = True

        End If

        CSIFlexServiceLib.read_dashboard_config_fromdb()

        RefreshAllDevices()

        ComputePerfReq()

    End Sub

    Private Sub TV_GroupsOfMachines_AfterLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles TV_GroupsOfMachines.AfterLabelEdit
        'MessageBox.Show("node name:" & e.Node.Name & "  node label:" & e.Label)

        Dim groupname As String = e.Label
        If groupname = "" Then
            groupname = "Group" & (TV_GroupsOfMachines.Nodes(0).Nodes.Count - 1).ToString()
        End If
        TV_GroupsOfMachines.SelectedNode.Name = e.Label
        TV_GroupsOfMachines.LabelEdit = False

        Dim mySqlCnt As New MySqlConnection

        mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)

        mySqlCnt.Open()

        Try
            Dim mysql As String = "update CSI_database.tbl_groups SET `groups` = '" + groupname + "' WHERE `groups` = '" + ancient_name + "' and users = '" + CSI_Library.CSI_Library.username + "';"
            mysql += "UPDATE  CSI_database.tbl_devices SET `groups` = replace(`groups`, '" + ancient_name + "','" + groupname + "');"
            Dim cmd As New MySqlCommand(mysql, mySqlCnt)
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        mySqlCnt.Close()
        TV_GroupsOfMachines.Nodes(0).Nodes.Clear()
        Load_groupes(CSI_Library.CSI_Library.MySqlConnectionString)
        Dim GETAllDevices As String = "SELECT IP FROM csi_database.tbl_deviceconfig;"
        Dim dtTable As New DataTable
        Dim cmdGETAllDevices As New MySqlCommand(GETAllDevices, mySqlCnt)
        Dim dbAdapter As New MySqlDataAdapter(cmdGETAllDevices)
        mySqlCnt.Open()
        dbAdapter.Fill(dtTable)
        mySqlCnt.Close()
        Dim oProducts = New List(Of String)
        For iIndex As Integer = 0 To dtTable.Rows.Count - 1
            oProducts.Add(dtTable.Rows(iIndex)("IP"))
        Next
        Dim LISTSize As Integer
        LISTSize = oProducts.Count
        For Count As Integer = 0 To (LISTSize - 1)
            RefreshDevice(oProducts(Count).ToString())
            '    Count = Count + 1
        Next
        CSIFlexServiceLib.read_dashboard_config_fromdb()
        RefreshAllDevices()
        ComputePerfReq()
        'RefreshAllDevices()
        'ComputePerfReq()

        Comput_perf_required = True
    End Sub

    Private Sub BTN_AddMTC_Click(sender As Object, e As EventArgs) Handles BTN_AddMTC.Click

        Try

            Dim license As New CSILicenseLibrary()

            If Not license.IsLicenseValid(LicenseProducts.CSIFLEXFocasMtc, grvConnectors.Rows.Count + 1) Then
                MessageBox.Show("You don't have license to execute this action. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com")
                Return
            End If

            Dim frm_mtcadd As New MtcFocasADD()

            frm_mtcadd.ShowDialog()

        Catch ex As Exception

            MessageBox.Show("Error adding MTConnect machine, see log.")

            Log.Error(ex)

        End Try
    End Sub

    Private Sub BTN_EditMtc_Click(sender As Object, e As EventArgs) Handles BTN_EditMtc.Click

        If grvConnectors.SelectedCells.Count > 0 Then

            Dim selectedrowindex As Integer = grvConnectors.SelectedCells(0).RowIndex
            Dim selectedRow As DataGridViewRow = grvConnectors.Rows(selectedrowindex)

            Dim ConnectionType = Convert.ToString(selectedRow.Cells("ConnectorType").Value)

            Dim connectorId = Convert.ToInt16(selectedRow.Cells("Id").Value)

            'Dim frm_mtcadd As New MtcFocasADD(ConnectionType, Convert.ToString(selectedRow.Cells("MachineName").Value))
            Dim frm_mtcadd As New MtcFocasADD(connectorId)

            frm_mtcadd.ShowDialog()

            Load_DGV_CSIConnector()
        Else
            MessageBox.Show("Please select a Machine to Edit")
        End If

    End Sub

    Private Sub BTN_DeleteMachine_Click(sender As Object, e As EventArgs) Handles BTN_DeleteMachine.Click

        'We have two types of machines here MTConnect and Focas So Delete Logic is Different 
        If grvConnectors.SelectedCells.Count <= 0 Then
            MessageBox.Show("Please select a Machine to Delete")
            Return
        End If

        Dim result = MessageBox.Show("Do you really want to delete this Machine ?", "Delete Machine", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then

            Dim selectedrowindex As Integer = grvConnectors.SelectedCells(0).RowIndex
            Dim selectedRow As DataGridViewRow = grvConnectors.Rows(selectedrowindex)

            Dim connectorId As Integer = CInt(selectedRow.Cells("Id").Value)
            Dim connector = New Connector(connectorId)

            Dim machinename As String = Convert.ToString(selectedRow.Cells("MachineName").Value)
            Dim machineip As String = Convert.ToString(selectedRow.Cells("MachineIP").Value)
            Dim MachineType As String = Convert.ToString(selectedRow.Cells("ConnectorType").Value)
            Dim enetMachinename As String = Convert.ToString(selectedRow.Cells("eNETMachineName").Value)

            Me.Cursor = Cursors.WaitCursor

            If MachineType = "MTConnect" Then

                'If Directory.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & selectedRow.Cells("eNETMachineName").Value) Then
                '    Directory.Delete(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & selectedRow.Cells("eNETMachineName").Value, True)
                'End If

            ElseIf MachineType = "Focas" Then

                'objAdapterInfo.ServiceName = "MTCFocasAdapter-" + machinename.Replace(" ", "-")
                objAdapterInfo.ServiceName = connector.AdapterServiceName

                'objAdapterInfo.Path = System.IO.Path.Combine(Paths.ADAPTERS, machinename)
                objAdapterInfo.Path = System.IO.Path.Combine(Paths.ADAPTERS, connector.AdapterServiceName)

                objAdapterInfo.DeviceName = machinename

                objMainWindow.UninstallAdapter(objAdapterInfo)
                objMainWindow.UninstallAgent(connector.MachineName, connector.AgentServiceName)

                Dim res = IsDirectoryEmpty(Paths.ADAPTERS)

                'If Directory.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & selectedRow.Cells("eNETMachineName").Value) Then
                '    Directory.Delete(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & selectedRow.Cells("eNETMachineName").Value, True)
                'End If

            End If

            connector.DeleteConnector()

            'Dim cmd As Text.StringBuilder = New Text.StringBuilder()
            'cmd.Append($"SET SQL_SAFE_UPDATES = 0; ")

            'cmd.Append($"DELETE FROM CSI_auth.tbl_CSIConnector       WHERE Id          = { connectorId } ; ")
            'cmd.Append($"DELETE FROM CSI_auth.tbl_mtcfocasconditions WHERE ConnectorId = { connectorId } ; ")
            'cmd.Append($"DELETE FROM CSI_auth.tbl_csiothersettings   WHERE ConnectorId = { connectorId } ; ")
            'cmd.Append($"UPDATE IGNORE csi_auth.tbl_ehub_conf SET MTC_Machine_name = '' WHERE MTC_Machine_name = '{ machinename }';")

            'MySqlAccess.ExecuteNonQuery(cmd.ToString())

            If CSI_Lib.MCHS_.ContainsKey(enetMachinename) Then
                CSI_Lib.MCHS_.Remove(enetMachinename)
            End If

            Me.Cursor = Cursors.Default
            MessageBox.Show("eNETMachine " + enetMachinename + " configurations deleted successfully !")
            Load_DGV_CSIConnector()

        ElseIf result = DialogResult.No Then
            'Do nothing here 
        End If

    End Sub

    Public Function IsDirectoryEmpty(path As String) As Boolean
        Return Not Directory.EnumerateFileSystemEntries(path).Any()
    End Function

    Private Sub TV_GroupsOfMachines_BeforeLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles TV_GroupsOfMachines.BeforeLabelEdit
        'e.CancelEdit = (e.Node.FullPath.IndexOf(TV_GroupsOfMachines.PathSeparator) = -1)
    End Sub

    Private Sub BTN_About_Click(sender As Object, e As EventArgs) Handles BTN_About.Click
        Dim aboutform As New AboutBox
        aboutform.ShowDialog()
    End Sub

    Private Sub enableORnotSave()
        Try
            If (txtUserName.Text <> "" And txtLastName.Text <> "" And txtFirstName.Text <> "" And cmbUserType.SelectedItem <> Nothing) Then
                btnSaveUser.Enabled = True
            Else
                btnSaveUser.Enabled = False
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub User_TextChanged(sender As Object, e As EventArgs) Handles txtUserName.TextChanged, txtLastName.TextChanged, txtFirstName.TextChanged, txtPassword.TextChanged, txtUserTitle.TextChanged, txtUserRefID.TextChanged, txtUserExtention.TextChanged, txtUserEmail.TextChanged, txtUserDept.TextChanged, txtDisplayName.TextChanged ', chkEditTimeline.CheckedChanged
        enableORnotSave()
    End Sub

    Private Sub setMysqlAccess(username As String, pwd As String, admin As Boolean)
        Try
            Using mysqlcnt As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                mysqlcnt.Open()

                Try
                    Dim trydrop As String = "DROP USER '" + username + "'@'localhost';DROP USER '" + username + "'@'%';"
                    Dim cmd As New MySqlCommand(trydrop, mysqlcnt)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception

                End Try

                Dim access As String = ""
                If (admin) Then
                    access = "GRANT SELECT,INSERT,UPDATE,DELETE"
                Else
                    access = "GRANT SELECT"
                End If

                'Dim mysql7 As String = "UPDATE mysql.user SET Password = PASSWORD('CSIF1337') WHERE User = 'root'; FLUSH PRIVILEGES;"
                Dim mysql7 As String = "CREATE USER '" + username + "'@'localhost' IDENTIFIED BY '" + pwd + "';" +
                    access +
                    "     ON `csi%`.*" +
                    "     TO '" + username + "'@'localhost';" +
                    " CREATE USER '" + username + "'@'%' IDENTIFIED BY '" + pwd + "';" +
                    access +
                    "      ON `csi%`.*" +
                    "      TO '" + username + "'@'%';"


                Dim cmdCreateDeviceTable7 As New MySqlCommand(mysql7, mysqlcnt)
                cmdCreateDeviceTable7.ExecuteNonQuery()
                mysqlcnt.Close()
            End Using
        Catch ex As Exception
            CSI_Lib.LogServerError("Unable to set mysql user rights pwd:" + ex.Message, 1)
        End Try

    End Sub

    Private Sub clearuser()

        txtUserName.Text = ""
        txtLastName.Text = ""
        txtFirstName.Text = ""
        txtDisplayName.Text = ""
        txtPassword.Text = ""
        txtUserEmail.Text = ""
        txtUserRefID.Text = ""
        txtUserTitle.Text = ""
        txtUserDept.Text = ""
        txtUserExtention.Text = ""
        chkEditTimeline.Checked = False
        'CB_Type.SelectedIndex = -1

        cmbUserType.Enabled = True

        RemoveHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

        If treeviewMachines.Nodes.Count > 0 Then 'This checks if All Machines and Group Nodes exists in treeView6

            For Each n As TreeNode In treeviewMachines.Nodes
                n.Checked = False

                If n.Nodes.Count > 0 Then
                    For Each n1 As TreeNode In n.Nodes
                        n1.Checked = False
                        If n1.Nodes.Count > 0 Then
                            For Each n2 As TreeNode In n1.Nodes
                                n2.Checked = False
                            Next
                        End If
                    Next
                End If
            Next
        End If

        usermachines = ""

        AddHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

    End Sub

    Private Sub menuChoice2(ByVal sender As Object, ByVal e As EventArgs)
        Dim item = CType(sender, ToolStripMenuItem)
        Dim selection = CInt(item.Tag)
        If (selection = 1) Then
            clearuser()
            GB_Users.Visible = True
            treeviewMachines.Visible = True
            treeviewMachines.ExpandAll()
            Users_TV.Nodes(0).Nodes.Add("New User")
            Users_TV.SelectedNode = Users_TV.Nodes(0).Nodes("New User")
        End If
        If (selection = 2) Then
            Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            cntsql.Open()
            Dim cmdsql As New MySqlCommand("delete from CSI_auth.users where username_ = '" & Users_TV.SelectedNode.Text & "'", cntsql)
            cmdsql.ExecuteNonQuery()
            Users_TV.SelectedNode.Remove()
        End If
    End Sub

    Private Sub loadmachine()

        Dim tableMachines = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_ehub_conf")

        Dim dTable_message12 As DataTable = MySqlAccess.GetDataTable($"SELECT machines from CSI_auth.users WHERE username_ = '{ Users_TV.SelectedNode.Text }'")

        If dTable_message12.Rows.Count > 0 Then

            Dim phrase As String = ","
            Dim Occurrences As Integer = 0
            Dim intCursor As Integer = 0
            Dim input As String = dTable_message12.Rows(0).Item(0).ToString()

            Do Until intCursor >= input.Length

                Dim strCheckThisString As String = Mid(LCase(input), intCursor + 1, (Len(input) - intCursor))

                Dim intPlaceOfPhrase As Integer = InStr(strCheckThisString, phrase)

                If intPlaceOfPhrase > 0 Then

                    Occurrences += 1
                    intCursor += (intPlaceOfPhrase + Len(phrase) - 1)
                Else

                    intCursor = input.Length
                End If
            Loop

            For Each p As TreeNode In treeviewMachines.Nodes
                p.Checked = False
                For Each n As TreeNode In p.Nodes
                    n.Checked = False
                    For Each m As TreeNode In n.Nodes
                        m.Checked = False
                    Next
                Next
            Next

            Dim dt As List(Of String) = input.Split(",").Select(Function(s) s.Trim()).ToList()

            If dt.Count = 0 Then
                Return
            End If

            Dim machId As Integer
            Dim row As DataRow

            For Each machine As String In dt

                If Not Integer.TryParse(machine, machId) Then
                    row = tableMachines.Rows.Cast(Of DataRow).Where(Function(r) r.Item("EnetMachineName").ToString() = machine).FirstOrDefault()
                    machId = row.Item("Id")
                End If

                For Each p As TreeNode In treeviewMachines.Nodes

                    If p.Tag = machId Then
                        p.Checked = True
                    Else
                        For Each n As TreeNode In p.Nodes
                            If n.Tag = machId Then
                                n.Checked = True
                            Else
                                For Each m As TreeNode In n.Nodes
                                    If m.Tag = machId Then
                                        m.Checked = True
                                    End If
                                Next
                            End If
                        Next
                    End If

                Next

            Next

        End If
    End Sub

    Private Function GetExternalSrcMachineList(mysqlcntstr As String) As List(Of String)
        Dim res As New List(Of String)

        Try
            Dim mySqlCnt As New MySqlConnection

            mySqlCnt = New MySqlConnection(mysqlcntstr) 'CSI_Library.CSI_Library.MySqlConnectionString)

            mySqlCnt.Open()

            Dim mysql23 As String = "SELECT machines from csi_auth.users where username_ = '" + CSI_Library.CSI_Library.username + "'"
            Dim cmdCreateDeviceTable23 As New MySqlCommand(mysql23, mySqlCnt)
            Dim mysqlReader23 As MySqlDataReader = cmdCreateDeviceTable23.ExecuteReader
            Dim dTable_message23 As New DataTable()
            dTable_message23.Load(mysqlReader23)
            If (dTable_message23.Rows.Count >= 1) Then
                Dim tempres As List(Of String) = dTable_message23.Rows(0)(0).ToString().Split(",").ToList()
                For Each tmach As String In tempres
                    tmach = tmach.Trim
                    If (tmach.Length > 0) Then
                        res.Add(tmach)
                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show("Unable to load the machines: " & ex.Message)
        End Try

        Return res
    End Function

    Private Sub BgWorker_importDB_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BgWorker_importDB.RunWorkerCompleted
        MessageBox.Show("CSV file successfully imported", "Database import", MessageBoxButton.OK, MessageBoxImage.Information)
    End Sub

    'Private Sub BTN_ConfigFTP_Click(sender As Object, e As EventArgs)
    '    Try
    '        CreateFtpConfigTableIfNotExist()
    '        Dim frm_ftpConfig As New Config()
    '        frm_ftpConfig.ShowDialog()

    '    Catch ex As Exception
    '        CSI_Lib.LogServerError("FTP Config error:" & ex.Message, 1)
    '    End Try

    'End Sub

    Private Sub CreateFtpConfigTableIfNotExist()

        Dim mySqlCnt As New MySqlConnection

        Try
            Dim OnlyIP As String = String.Empty

            Dim dTable_message13 As DataTable = MySqlAccess.GetDataTable($"SELECT * from CSI_auth.ftpconfig;")

            If dTable_message13.Rows.Count = 0 Then
                'This means We are adding it for the first time With By Default IP and Password

                If File.Exists(path & "\sys\Networkenet_.csys") Then
                    Using reader As StreamReader = New StreamReader(path & "\sys\Networkenet_.csys")
                        Dim IPStringAndPort As String() = reader.ReadLine().Split(":"c)
                        OnlyIP = IPStringAndPort(0)
                        '= reader.ReadLine()
                        reader.Close()
                    End Using
                End If

                MySqlAccess.ExecuteNonQuery($"insert into CSI_auth.ftpconfig  (ftpip,ftppwd) Values ('{ OnlyIP }','ENET') ;")

            End If

        Catch ex As Exception
            CSI_Lib.LogServerError("FTP Config error:" & ex.Message, 1)
        End Try

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Changes.ShowDialog()

    End Sub

    Private Sub Config_trends_Click(sender As Object, e As EventArgs) Handles btnAdvSettings.Click
        config_.ShowDialog()
        dashboardDevice.ReloadDevice()
    End Sub

    Private Sub TV_CurrPerfGroup_AfterSelect(sender As Object, e As TreeViewEventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles BTN_gensett.Click
        'GeneralSettings.Show()
        GeneralSettings.ShowDialog() 'Difference between showDialog and Show is ShowDialog shows as Modal and Until it close it will not let user perform work with it's parent form
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
            'FocasLibrary.Tools.AdapterManagement.Stop("CSIFlexServerService")
            'FocasLibrary.Tools.AdapterManagement.Start("CSIFlexServerService")
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

    Private Sub SetupForm_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing

        CSI_Lib.Log_server_event(vbCrLf & "-----------------------------------------------------")
        CSI_Lib.Log_server_event("CSIFlex server closed")
        CSI_Lib.Log_server_event("-----------------------------------------------------" & vbCrLf)

    End Sub

    Private Function convert_mac_to_ip(mac As String) As String

        Dim startInfo As New ProcessStartInfo()
        startInfo.CreateNoWindow = True
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardOutput = True
        startInfo.FileName = "arp"
        startInfo.Arguments = "-a"

        Try
            Dim process__1 As Process = Process.Start(startInfo)
            Dim out As String = ""
            Dim splitted_out As String()

            While Not process__1.StandardOutput.EndOfStream
                out = process__1.StandardOutput.ReadLine()
                out = Regex.Replace(out, "\s+", " ")
                splitted_out = out.Split(" ")
                If splitted_out.Count > 1 Then
                    If splitted_out(2) = mac.ToLower Then Return splitted_out(1)
                End If
            End While
        Catch ex As Exception
            MessageBox.Show("Could not find the ip address associated with the device MAC address: " & ex.Message)
            Return "0.0.0.0" 'error
        Finally

        End Try
        Return "0.0.0.0" 'error
    End Function

    Private Sub BTN_ping_Click(sender As Object, e As EventArgs) Handles BTN_ping.Click

        Dim DeviceIP As String = IP_TB.Text

        Dim macRegex As Regex = New Regex("^([0-9a-fA-F]{2}(?:(?:-[0-9a-fA-F]{2}){5}|(?::[0-9a-fA-F]{2}){5}|[0-9a-fA-F]{10}))$")

        Dim match As Match = macRegex.Match(DeviceIP)

        If match.Success Then
            DeviceIP = convert_mac_to_ip(DeviceIP.Replace(":", "-"))
        End If

        If DeviceIP <> "0.0.0.0" And DeviceIP <> "" Then
            Dim result As Boolean = pingHost(DeviceIP)
            If result = True Then
                BTN_ping.Text = "Device responded"
                BTN_ping.BackColor = Color.LightGreen
            Else
                BTN_ping.Text = "No response"
                BTN_ping.BackColor = Color.Red
            End If
        Else
            BTN_ping.Text = "Bad IP/MAC"
            BTN_ping.BackColor = Color.Red
        End If

    End Sub

#Region "Updates"



    Public Sub HandleServerResponse()
        LBL_srvResult.Text = String.Empty
        LBL_srvResult.Visible = False
        t3.Dispose()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AddDevice.Show()
        send_http_req()
    End Sub

    Private Sub BTN_UPDATES_Click(sender As Object, e As EventArgs)
        Try
            If BG_updates.IsBusy = False Then
                BG_updates.RunWorkerAsync()
            End If
        Catch ex As Exception

            CSI_Lib.LogServerError("update fail :" + ex.Message, 1)
        End Try
    End Sub

    Private Sub BG_updates_DoWork(sender As Object, e As DoWorkEventArgs) Handles BG_updates.DoWork
        Try
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create("ftp://csiflex.org/%2F/CSIFlexUpdate"), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.ListDirectory
            BG_updates.ReportProgress(1)

            ' This example assumes the FTP site uses anonymous logon.
            request.Credentials = New NetworkCredential("asalmi@csiflex.org", "t4Solutions")
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Dim responseStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(responseStream)
            Dim resultData As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)

            Dim Version As Integer

            While (Not reader.EndOfStream)
                Dim Line As String = reader.ReadLine
                If Line.StartsWith("CSI Flex Server Update ") Then
                    Dim splitted As String() = Line.Split(" ")
                    Dim STRVersion As String = splitted(splitted.Length - 1)
                    Dim STRVersionSplitted As String() = STRVersion.Split(".")
                    Version = STRVersionSplitted(0)
                End If
            End While
            If Version > 1000 Then 'CSI_DATA.CSIFLEX_VERSION Then
                'request.Method = WebRequestMethods.Ftp.DownloadFile("New-Server.txt")

                ' read file New-Server.txt ... 
                e.Result = "True-" & Version.ToString()
                BG_updates.ReportProgress(2)
            Else
                e.Result = "False"
                BG_updates.ReportProgress(3)
            End If


            reader.Close()
            response.Close()

            Dim SizeRequest As FtpWebRequest = DirectCast(WebRequest.Create("ftp://csiflex.org/%2F/CSIFlexUpdate/CSI Flex Server Update " & Version.ToString() & ".exe"), FtpWebRequest)
            SizeRequest.Method = WebRequestMethods.Ftp.GetFileSize
            SizeRequest.Credentials = New NetworkCredential("asalmi@csiflex.org", "t4Solutions")
            Dim SizeResponse As FtpWebResponse = SizeRequest.GetResponse()
            Dim Size As Long = SizeResponse.ContentLength
            e.Result = e.Result + "-" + Size.ToString()


        Catch ex As Exception

            e.Result = "Error"
            CSI_Lib.LogServerError("Could Not check updates, " + ex.Message, 1)
        End Try

    End Sub

    Private Sub BG_updates_progress(sender As Object, e As ProgressChangedEventArgs) Handles BG_updates.ProgressChanged

        'If e.ProgressPercentage = 1 Then
        '    LBL_UpdateInformation.Text = "Checking ... "
        '    LBL_UpdateInformation.BackColor = Color.Yellow
        'ElseIf e.ProgressPercentage = 2 Then
        '    LBL_UpdateInformation.Text = "Update found. "
        '    LBL_UpdateInformation.BackColor = Color.LimeGreen
        'ElseIf e.ProgressPercentage = 3 Then
        '    LBL_UpdateInformation.Text = "System up to date."
        '    LBL_UpdateInformation.BackColor = Color.White
        'End If
    End Sub

    Private Sub BG_updates_Complete(sender As Object, e As RunWorkerCompletedEventArgs) Handles BG_updates.RunWorkerCompleted
        Try
            If DirectCast(e.Result, String).StartsWith("True") Then
                Dim Res As DialogResult = MessageBox.Show("There Is a New update for CSIFLex, do you want to download it ?.", "Update available", vbYesNo)
                If (Res = DialogResult.No) Then

                Else
                    Dim strtmp As String() = DirectCast(e.Result, String).Split("-")
                    If Not BG_DL_update.IsBusy Then BG_DL_update.RunWorkerAsync(strtmp(1))
                    If Not BG_checkfilesize.IsBusy Then BG_checkfilesize.RunWorkerAsync(e.Result)
                End If
            ElseIf DirectCast(e.Result, String) = "False" Then
                MessageBox.Show("You are using the lattest version of CSIFlex", "No update available")
            Else
                MessageBox.Show("CSIFlex Is Not able to check the updates, retry later.", ":  (")
            End If
        Catch ex As Exception

            CSI_Lib.LogServerError("fail after update check :" + ex.Message, 1)
        End Try
    End Sub

    Private Sub DownLoadUpdate(Version As Integer)
        Try
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create("ftp://csiflex.org/%2F/CSIFlexUpdate/CSI Flex Server Update " & Version.ToString() & ".exe"), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.DownloadFile

            ' This example assumes the FTP site uses anonymous logon.
            request.Credentials = New NetworkCredential("asalmi@csiflex.org", "t4Solutions")
            Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
            Dim responseStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(responseStream)

            If Directory.Exists(CSI_Library.CSI_Library.serverRootPath + "/updates/") Then
            Else
                Directory.CreateDirectory(CSI_Library.CSI_Library.serverRootPath + "/updates/")
            End If
            If File.Exists(CSI_Library.CSI_Library.serverRootPath + "/updates/CSI Flex Server Update " & Version.ToString() & ".exe") Then
                File.Delete(CSI_Library.CSI_Library.serverRootPath + "/updates/CSI Flex Server Update " & Version.ToString() & ".exe")
            End If

            Using writer As New FileStream(CSI_Library.CSI_Library.serverRootPath + "/updates/CSI Flex Server Update " & Version.ToString() & ".exe", FileMode.Create)

                Dim length As Long = response.ContentLength
                Dim bufferSize As Integer = 2048
                Dim readCount As Integer
                Dim buffer As Byte() = New Byte(2047) {}

                readCount = responseStream.Read(buffer, 0, bufferSize)
                While readCount > 0
                    writer.Write(buffer, 0, readCount)
                    readCount = responseStream.Read(buffer, 0, bufferSize)
                End While
            End Using

            reader.Close()
            response.Close()
        Catch ex As Exception
            'reader.Close()
            '  response.Close()

            CSI_Lib.LogServerError("Could not download the update, " + ex.Message, 1)
        End Try
    End Sub

    Private Sub BG_DL_update_DoWork(sender As Object, e As DoWorkEventArgs) Handles BG_DL_update.DoWork


        BG_DL_update.ReportProgress(1)
        DownLoadUpdate(e.Argument)
        BG_DL_update.ReportProgress(2)
        e.Result = e.Argument
    End Sub

    Private Sub BG_DL_update_progress(sender As Object, e As ProgressChangedEventArgs) Handles BG_DL_update.ProgressChanged
        'If e.ProgressPercentage = 1 Then
        '    LBL_UpdateInformation.Text = "Downloading ... "
        '    LBL_UpdateInformation.BackColor = Color.Yellow
        'Else
        '    LBL_UpdateInformation.Text = "Downloaded."
        '    LBL_UpdateInformation.BackColor = Color.LimeGreen
        'End If
    End Sub

    Private Sub BG_DL_update_Complete(sender As Object, e As RunWorkerCompletedEventArgs) Handles BG_DL_update.RunWorkerCompleted
        ContinueCheckin = False
        Try
            Dim Res As DialogResult = MessageBox.Show("The new update has been downloaded, do you want to update CSIFlex Now ?. This action will temporarily close the CSIFlex service.", "Update available", vbYesNo)
            Dim version As String = e.Result

            If (Res = DialogResult.No) Then
                'open folder
                Process.Start(CSI_Library.CSI_Library.serverRootPath + "/updates/")
                'LBL_UpdateInformation.Text = ""
                'LBL_UpdateInformation.BackColor = Color.White
                'PB_updates.Value = 0
                'PB_updates.Visible = False

            Else

                BG_installUdate.RunWorkerAsync(version)
                services.StopService("CSIFlexServerService")
                Me.Close()

            End If
        Catch ex As Exception

            CSI_Lib.LogServerError("fail after update install starts :" + ex.Message, 1)
        End Try

    End Sub

    Private Sub BG_installUdate_DoWork(sender As Object, e As DoWorkEventArgs) Handles BG_installUdate.DoWork
        Try
            Dim version As String = e.Argument
            If File.Exists(CSI_Library.CSI_Library.serverRootPath + "/updates/CSI Flex Server Update " & version & ".exe") Then
                System.Diagnostics.Process.Start(CSI_Library.CSI_Library.serverRootPath + "/updates/CSI Flex Server Update " & version & ".exe")
            End If
        Catch ex As Exception
            CSI_Lib.LogServerError("fail after update install starts :" + ex.Message, 1)
        End Try
    End Sub

    Dim ContinueCheckin As Boolean = True

    Private Sub BG_checkfilesize_DoWork(sender As Object, e As DoWorkEventArgs) Handles BG_checkfilesize.DoWork
        Try
            Dim strtmp As String() = DirectCast(e.Argument, String).Split("-")
            Dim Version As String = strtmp(1)
            Dim Filesize As Integer = Convert.ToInt32(strtmp(2)) / 100
            Dim ActualSize As Integer
            Dim infoReader As System.IO.FileInfo
            While (Filesize > ActualSize Or ContinueCheckin)
                If (File.Exists(CSI_Library.CSI_Library.serverRootPath + "/updates/CSI Flex Server Update " & Version.ToString() & ".exe")) Then
                    infoReader = My.Computer.FileSystem.GetFileInfo(CSI_Library.CSI_Library.serverRootPath + "/updates/CSI Flex Server Update " & Version.ToString() & ".exe")
                    ActualSize = infoReader.Length / 100
                    BG_checkfilesize.ReportProgress(ActualSize * 100 / Filesize)
                End If
                Thread.Sleep(10)
            End While
            If (ContinueCheckin = False) Or (Filesize > ActualSize) Then BG_checkfilesize.ReportProgress(100)
        Catch ex As Exception

            CSI_Lib.LogServerError("fail while checking file size :" + ex.Message, 1)
        End Try
    End Sub

    Private Sub BG_checkfilesize_progress(sender As Object, e As ProgressChangedEventArgs) Handles BG_checkfilesize.ProgressChanged
        'If e.ProgressPercentage = 100 Then
        '    LBL_UpdateInformation.Text = "Downloaded"
        '    LBL_UpdateInformation.BackColor = Color.White
        'Else
        '    LBL_UpdateInformation.Text = "Downloading ... " + e.ProgressPercentage.ToString() + "%"
        'End If

        'PB_updates.Visible = True
        'PB_updates.Value = e.ProgressPercentage
    End Sub

    Private Sub gridviewMachines_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles gridviewMachines.CellValueChanged

        If (e.RowIndex < 0) Then
            Return
        End If

        Dim id = gridviewMachines.CurrentRow.Cells("Id").Value
        Dim machineName = gridviewMachines.CurrentRow.Cells("Machines").Value
        Dim machineLabel = gridviewMachines.CurrentRow.Cells("MachineLabel").Value
        Dim dailyTarget = gridviewMachines.CurrentRow.Cells("DailyTarget").Value
        Dim weeklyTarget = gridviewMachines.CurrentRow.Cells("WeeklyTarget").Value
        Dim monthlyTarget = gridviewMachines.CurrentRow.Cells("MonthlyTarget").Value

        Dim sqlCmd As Text.StringBuilder = New Text.StringBuilder()

        sqlCmd.Append($"UPDATE csi_auth.tbl_ehub_conf                     ")
        sqlCmd.Append($"    SET                                           ")
        sqlCmd.Append($"        `machine_name`      = '{ machineName  }', ")
        sqlCmd.Append($"        `machine_label`     = '{ machineLabel }', ")
        sqlCmd.Append($"        `MCH_WeeklyTarget`  =  { weeklyTarget } , ")
        sqlCmd.Append($"        `MCH_MonthlyTarget` =  { monthlyTarget}   ")
        sqlCmd.Append($"    WHERE                                         ")
        sqlCmd.Append($"        `Id`  = { id };                           ")

        MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())


        'If (e.ColumnIndex > 5 And e.RowIndex >= 0) Then

        '    If IsNumeric(gridviewMachines.Item(e.ColumnIndex, e.RowIndex).Value) Then

        '        'I change here Column index from 1 to 2 because we have changed it to machinelabel
        '        If gridviewMachines.Columns(e.ColumnIndex).Name = "Dailytarget" Then
        '            CSI_Lib.updateDailytarget(gridviewMachines.Item(1, e.RowIndex).Value, gridviewMachines.Item(e.ColumnIndex, e.RowIndex).Value)
        '            TargetsChanged = True

        '        ElseIf gridviewMachines.Columns(e.ColumnIndex).Name = "Weeklytarget" Then
        '            CSI_Lib.updateWeeklytarget(gridviewMachines.Item(1, e.RowIndex).Value, gridviewMachines.Item(e.ColumnIndex, e.RowIndex).Value)
        '            TargetsChanged = True

        '        ElseIf gridviewMachines.Columns(e.ColumnIndex).Name = "Monthlytarget" Then
        '            CSI_Lib.updateMonthlytarget(gridviewMachines.Item(1, e.RowIndex).Value, gridviewMachines.Item(e.ColumnIndex, e.RowIndex).Value)
        '            TargetsChanged = True
        '        End If

        '        RefreshAllDevices()
        '    Else
        '        MsgBox("The targets are numbers ! '" & gridviewMachines.Item(e.ColumnIndex, e.RowIndex).Value & "' is not a number on this planet.")
        '        gridviewMachines.Item(e.ColumnIndex, e.RowIndex).Value = 0
        '    End If

        '    'save in dictionary
        '    'check if value is between 0 and 100

        'ElseIf (e.ColumnIndex = 2 And e.RowIndex >= 0) Then '
        '    'Update MAchineLabels 
        '    CSI_Lib.UpdateMachineLabel(gridviewMachines.Item(1, e.RowIndex).Value, gridviewMachines.Item(e.ColumnIndex, e.RowIndex).Value)
        'End If

        Call LoadGridviewMachines()

    End Sub

    Public Groups_dic As Dictionary(Of String, List(Of String))

    Private Sub UpdateTargetsTitle()
        Try

            Dim cmmd As String = "SELECT * from csi_machineperf.tbl_perf where machinename_ like 'TitleTarget_Grp_%'"
            Dim dadapter_name As New MySqlDataAdapter(cmmd, CSI_Library.CSI_Library.MySqlConnectionString)
            Dim PERF_TBL As New DataTable
            dadapter_name.Fill(PERF_TBL)
            Dim Groups_perf_w As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
            Dim Groups_perf_m As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
            Groups_dic = LoadGroups()

            Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)

            MonthlyTargets.Clear()
            WeeklyTargets.Clear()

            For Each ro As DataRow In PERF_TBL.Rows
                'Dim Group As String = ro(0).replace("TitleTarget_Grp_", "")
                Dim Group As String = Convert.ToString(ro(0)).Replace("TitleTarget_Grp_", "")
                'Dim TMP_str_w As String = ro(1).ToString().Replace("Grp_" + Group + " ", "")
                Dim TMP_str_w As String = Convert.ToString(ro(1)).Replace("Grp_" + Group + " ", "")
                'Dim TMP_str_m As String = ro(2).ToString().Replace("Grp_" + Group + " ", "")
                Dim TMP_str_m As String = Convert.ToString(ro(2)).ToString().Replace("Grp_" + Group + " ", "")
                Dim w_H As String = Regex.Split(TMP_str_w, " of ")(0).Replace("h", "")
                Dim w_M As String = Regex.Split(TMP_str_m, " of ")(0).Replace("h", "")
                Groups_perf_w.Add(Group, Convert.ToInt32(Regex.Split(TMP_str_w, " of ")(0).Replace("h", "")))
                Groups_perf_m.Add(Group, Convert.ToInt32(Regex.Split(TMP_str_m, " of ")(0).Replace("h", "")))
            Next

            For Each row In gridviewMachines.Rows
                For Each Group In Groups_dic.Keys
                    Dim machinename As String = gridviewMachines.Item(1, row.index).Value.ToString()
                    If Groups_dic(Group).Contains(machinename) Then
                        If MonthlyTargets.ContainsKey(Group) Then
                            MonthlyTargets(Group) = MonthlyTargets(Group) + Convert.ToInt32(gridviewMachines.Item(8, row.index).Value.ToString())
                            WeeklyTargets(Group) = WeeklyTargets(Group) + Convert.ToInt32(gridviewMachines.Item(7, row.index).Value.ToString())
                        Else
                            MonthlyTargets.Add((Group), Convert.ToInt32(gridviewMachines.Item(8, row.index).Value.ToString()))
                            WeeklyTargets.Add((Group), Convert.ToInt32(gridviewMachines.Item(7, row.index).Value.ToString()))
                        End If

                    End If
                Next
            Next

            cntsql.Open()
            For Each Group In MonthlyTargets.Keys
                Dim TargetTile_Monthly As String = "Grp_" + Group & " " & Math.Round(Groups_perf_m(Group)) & "h of " & Math.Round(MonthlyTargets(Group)) & "h"
                Dim TargetTile_weekly As String = "Grp_" + Group & " " & Math.Round(Groups_perf_w(Group)) & "h of " & Math.Round(WeeklyTargets(Group)) & "h"
                cmmd = "update csi_machineperf.tbl_perf Set monthly_ = '" + TargetTile_Monthly + "', weekly_ = '" + TargetTile_weekly + "' where machinename_ = 'TitleTarget_Grp_" + Group + "'"
                Dim cmdsql2 As New MySqlCommand(cmmd, cntsql)
                cmdsql2.ExecuteNonQuery()

            Next
            cntsql.Close()

        Catch ex As Exception
            CSI_Lib.LogServiceError("Err while trying to read the perf from the database : " & ex.Message, 1)
        Finally

        End Try
    End Sub

    Public Function LoadGroups() As Dictionary(Of String, List(Of String))

        Dim tmp_tbl_ As New DataTable
        Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select * FROM csi_database.tbl_groups", CSI_Library.CSI_Library.MySqlConnectionString)
        dadapter_name.Fill(tmp_tbl_)

        Dim ___ As New Dictionary(Of String, List(Of String))

        For Each ro As DataRow In tmp_tbl_.Rows
            If ___.ContainsKey(ro("groups")) Then
                If Not ro("machines") = "" Then ___(ro("groups")).Add(ro("machines"))
            Else
                ___.Add(ro("groups"), New List(Of String))
                ___(ro("groups")).Add(ro("machines"))
            End If
        Next

        Return ___

    End Function

    Private Sub PictureBox5_MouseDown(sender As Object, e As EventArgs) Handles PictureBox5.MouseDown ', PictureBox5.MouseHover
        txtPassword.PasswordChar = ControlChars.NullChar
    End Sub

    Private Sub PictureBox5_MouseUp(sender As Object, e As EventArgs) Handles PictureBox5.MouseUp ', PictureBox5.MouseLeave
        txtPassword.PasswordChar = "*"c
    End Sub

    Dim isNewUser As Boolean = False

    Private actionToPerform As [Delegate]

    Private mydqlaccess As Object

    Private Sub Button7_Click_2(sender As Object, e As EventArgs) Handles Button7.Click
        'actionToPerform = New MethodInvoker(AddressOf AddGroupToolStripMenuItem.Click)
        AddGroupToolStripMenuItem_Click(sender, e)
    End Sub


    'Private Sub ___xBTN_HexaDecimalVal_Click(sender As Object, e As EventArgs)

    '    Dim Found As Boolean = False
    '    Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\", True)
    '    Dim regKey3 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\", True)
    '    Dim AllInstalledApps = regKey.GetSubKeyNames()
    '    Dim Another = regKey3.GetSubKeyNames()
    '    Dim Combined = AllInstalledApps.Concat(Another).ToArray()

    '    For Each names In Combined
    '        Dim regKey1 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\" & names, True)
    '        Dim regKey4 As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" & names, True)
    '        If regKey1.GetValue("DisplayName") = "CSIFlex Server" Then
    '            MessageBox.Show("Installation Path : " & regKey1.GetValue("InstallSource") & Environment.NewLine & "Unistallation String : " & regKey1.GetValue("UninstallString") & Environment.NewLine & "Version : " & regKey1.GetValue("DisplayVersion"))
    '            Found = True
    '            Exit For
    '        ElseIf regKey4.GetValue("DisplayName") = "CSIFlex Server" Then
    '            MessageBox.Show("Installation Path : " & regKey4.GetValue("InstallSource") & Environment.NewLine & "Unistallation String : " & regKey4.GetValue("UninstallString") & Environment.NewLine & "Version : " & regKey4.GetValue("DisplayVersion"))
    '            Found = True
    '            Exit For
    '        End If
    '    Next

    '    regKey.Close()
    '    regKey3.Close()

    'End Sub

    Private Sub btnLicenseRequest_Click(sender As Object, e As EventArgs) Handles btnLicenseRequest.Click
        LicenseManagement.ShowDialog()
    End Sub

    Private Sub cmbImportGroups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbImportGroups.SelectedIndexChanged

        If cmbImportGroups.SelectedIndex < 0 Then
            Return
        End If

        Dim username As String = CSI_Library.CSI_Library.username
        Dim newNode As TreeNode
        Dim sqlCmd As Text.StringBuilder = New Text.StringBuilder()
        Dim refresh As Boolean = False

        If cmbImportGroups.SelectedIndex = 0 Then

            If Not MessageBox.Show("Do you confirm the creation of groups of machines using the eNETDNC Departments?", "Creation of groups", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel) = MessageBoxResult.Yes Then
                cmbImportGroups.SelectedIndex = -1
                Return
            End If

            For Each machine As eNetMachineConfig In eNetServer.MonitoredMachines

                If Not TV_GroupsOfMachines.Nodes(0).Nodes.ContainsKey(machine.Department) Then
                    newNode = New TreeNode()
                    newNode.Name = machine.Department
                    newNode.Text = machine.Department

                    newNode.Nodes.Add(New TreeNode())
                    newNode.Nodes(0).Name = machine.MachineName
                    newNode.Nodes(0).Text = machine.MachineName
                    newNode.Nodes(0).ForeColor = Color.Gray

                    TV_GroupsOfMachines.Nodes(0).Nodes.Add(newNode)

                    sqlCmd.Clear()
                    sqlCmd.Append($"INSERT IGNORE INTO          ")
                    sqlCmd.Append($"    CSI_Database.tbl_Groups ")
                    sqlCmd.Append($"(                           ")
                    sqlCmd.Append($"    users    ,              ")
                    sqlCmd.Append($"    `groups`                ")
                    sqlCmd.Append($") VALUES (                  ")
                    sqlCmd.Append($"    '{username}' ,          ")
                    sqlCmd.Append($"    '{machine.Department}'  ")
                    sqlCmd.Append($");                          ")
                    sqlCmd.Append($"INSERT IGNORE INTO          ")
                    sqlCmd.Append($"    CSI_Database.tbl_Groups ")
                    sqlCmd.Append($"(                           ")
                    sqlCmd.Append($"    users    ,              ")
                    sqlCmd.Append($"    `groups` ,              ")
                    sqlCmd.Append($"    machines                ")
                    sqlCmd.Append($") VALUES (                  ")
                    sqlCmd.Append($"    '{username}' ,          ")
                    sqlCmd.Append($"    '{machine.Department}' ,")
                    sqlCmd.Append($"    '{machine.MachineName}' ")
                    sqlCmd.Append($");                          ")

                    MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())
                    refresh = True

                Else
                    Dim node = TV_GroupsOfMachines.Nodes(0).Nodes.Find(machine.Department, False).First()

                    If Not node.Nodes.ContainsKey(machine.MachineName) Then
                        newNode = New TreeNode()
                        newNode.Name = machine.MachineName
                        newNode.Text = machine.MachineName
                        newNode.ForeColor = Color.Gray

                        node.Nodes.Add(newNode)

                        sqlCmd.Clear()
                        sqlCmd.Append($"INSERT IGNORE INTO          ")
                        sqlCmd.Append($"    CSI_Database.tbl_Groups ")
                        sqlCmd.Append($"(                           ")
                        sqlCmd.Append($"    users    ,              ")
                        sqlCmd.Append($"    `groups` ,              ")
                        sqlCmd.Append($"    machines                ")
                        sqlCmd.Append($") VALUES (                  ")
                        sqlCmd.Append($"    '{username}' ,          ")
                        sqlCmd.Append($"    '{machine.Department}' ,")
                        sqlCmd.Append($"    '{machine.MachineName}' ")
                        sqlCmd.Append($");                          ")

                        MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())
                        refresh = True

                    End If
                End If
            Next

        End If

        If refresh Then
            Comput_perf_required = True
            CSIFlexServiceLib.read_dashboard_config_fromdb()
            UpdateTargetsTitle()
            RefreshAllDevices()
            ComputePerfReq()
        End If

    End Sub

    Private Sub btnMonitoringUnits_Click(sender As Object, e As EventArgs) Handles btnMonitoringUnits.Click

        Dim form As MonitoringUnitsList = New MonitoringUnitsList()

        form.StartPosition = FormStartPosition.CenterScreen
        form.ShowDialog()

    End Sub

    'Private Sub grvConnectors_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grvConnectors.CellContentDoubleClick

    '    'BTN_EditMtc_Click(Me, New EventArgs())

    'End Sub

    'Private Sub PictureBox5_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox5.MouseDown

    'End Sub

    'Private Sub PictureBox5_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox5.MouseUp

    'End Sub

    'Private Sub treeviewMachines_DoubleClick(sender As Object, e As EventArgs) Handles treeviewMachines.DoubleClick

    '    'Dim node As TreeNode = CType(sender, TreeNode)

    '    'node.Checked = Not node.Checked
    'End Sub


    'Private Sub DGV_Sources_KeyDown(sender As Object, e As KeyEventArgs) Handles DGV_Sources.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        e.Handled = True
    '    End If
    'End Sub

    'Private Sub DGV_Sources_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DGV_Sources.KeyPress
    '    MessageBox.Show(e.KeyChar)
    'End Sub

    'Private Sub DGV_Sources_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DGV_Sources.CellBeginEdit

    'End Sub

    'Private Sub DGV_Sources_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGV_Sources.EditingControlShowing

    '    If Me.DGV_Sources.CurrentCell.ColumnIndex = 0 And Not e.Control Is Nothing Then
    '        Dim tb As TextBox = CType(e.Control, TextBox)
    '        AddHandler tb.KeyDown, AddressOf TextBox_KeyDown
    '        AddHandler tb.KeyPress, AddressOf TextBox_KeyPress
    '    End If
    'End Sub

    'Dim flag = False

    'Private Sub TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        flag = True
    '    End If
    'End Sub

    'Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    e.Handled = flag
    '    flag = False
    'End Sub

#End Region

    Public Function LR1orNotLR1(IP As String, name As String) As Boolean

        Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)

        Try
            cntsql.Open()
            Dim mysql_str As String = "SELECT devicetype FROM CSI_database.tbl_deviceconfig2 where IP_adress = '" & IP & "' and name = '" + name + "'"
            Dim cmd_devicetype As New MySqlCommand(mysql_str, cntsql)
            Dim mysqlReader23 As MySqlDataReader = cmd_devicetype.ExecuteReader
            Dim dTable_type As New DataTable()
            dTable_type.Load(mysqlReader23)
            cntsql.Close()

            Dim type As String = ""
            If dTable_type.Rows.Count <> 0 Then
                type = dTable_type.Rows(0).Item(0)
            End If

            If type = "LR1" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            If cntsql.State = ConnectionState.Open Then cntsql.Close()
            MsgBox("Could not check if this device is an LR1 : " & ex.Message)
            Return False
        End Try
    End Function

End Class


Public Class MachineEhub

    Public Property MachineName As String = ""

    Public Property MachinePosition As String = ""

    Public Property MachineLabel As String = ""

    Public Property IsFtpConnection As Boolean = False

    Public Property ConnectionType As Integer = 0

    Public Property FtpFileName As String = ""

    Public Property IsMonitoring As Boolean = False

    Public Property ThParameter As String = ""

    Public Property DaParameter As String = ""

End Class


Public Class MachineData

    Private Property Machinename As String = ""

    Private Property MonitoringFileName As String = ""

    Private Property MonitoringID As String = ""

    Private Property MonitoringState As Integer = 0

    Property Machinename_ As String
        Get
            Return Machinename
        End Get
        Set(value As String)
            Machinename = value
        End Set
    End Property

    Property MonitoringFileName_ As String
        Get
            Return MonitoringFileName
        End Get
        Set(value As String)
            MonitoringFileName = value
        End Set
    End Property

    Property MonitoringID_ As String
        Get
            Return MonitoringID
        End Get
        Set(value As String)
            MonitoringID = value
        End Set
    End Property

    Property MonitoringState_ As Integer
        Get
            Return MonitoringState
        End Get
        Set(value As Integer)
            MonitoringState = value
        End Set
    End Property

End Class


Public Class MachinePerfData

    Private Property TableName As String = ""

    Private Property Status As String = ""

    Private Property TimeOfEvent As DateTime

    Private Property cycletime As Integer = 0

    Private Property tablerowcount As Integer = 0

    Property TableName_ As String
        Get
            Return TableName
        End Get
        Set(value As String)
            TableName = value
        End Set
    End Property

    Property Status_ As String
        Get
            Return Status
        End Get
        Set(value As String)
            Status = value
        End Set
    End Property

    Property TimeOfEvent_ As DateTime
        Get
            Return TimeOfEvent
        End Get
        Set(value As DateTime)
            TimeOfEvent = value
        End Set
    End Property

    Property cycletime_ As Integer
        Get
            Return cycletime
        End Get
        Set(value As Integer)
            cycletime = value
        End Set
    End Property

    Property tablerowcount_ As Integer
        Get
            Return tablerowcount
        End Get
        Set(value As Integer)
            tablerowcount = value
        End Set
    End Property

End Class


Public Class sourceToAdd

    Public name As String

    Public sourceName As String

    Public listOfMachine As List(Of String)

    Sub New(name As String, sourceName As String)
        Me.name = name
        Me.sourceName = sourceName
        listOfMachine = New List(Of String)
    End Sub

    Sub New(name As String, sourceName As String, machineListStr As String)
        Me.sourceName = sourceName
        listOfMachine = New List(Of String)

        For Each machineName In machineListStr.Split(";")
            If (machineName <> "") Then
                listOfMachine.Add(machineName)
            End If
        Next

    End Sub

End Class



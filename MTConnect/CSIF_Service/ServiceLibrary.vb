Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports CSI_Library
Imports CSIFLEX.Database.Access
Imports CSIFLEX.eNetLibrary
Imports CSIFLEX.eNetLibrary.Data
Imports CSIFLEX.License.Data
Imports CSIFLEX.Server.Library
Imports CSIFLEX.Server.Library.DataModel
Imports CSIFLEX.Server.Settings
Imports CSIFLEX.Utilities
Imports CSIFLEX.FocasConnector

Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports OpenNETCF.MTConnect
Imports RpnParser
Imports CSIFLEX

'Api practice

Public Class ServiceLibrary

    'livestatus
    Dim enetstatuscolor As New DataTable
    Dim IsReportingON As Boolean = False

    'ip
    Dim enetip As String

    Dim dtMtcMachines As New DataTable()
    Dim dtMonitoringUnits As New DataTable()

    Dim ftpmachlist As New List(Of CSIFLEX.eNetLibrary.Data.eNetMachineConfig)
    Dim reptablepresent As Boolean = False
    Dim csi_lib As New CSI_Library.CSI_Library(True)
    Dim _shutdownEvent As New ManualResetEvent(False)

    Dim license As New CSILicenseLibrary()

    'enet thread
    Public thread_enet_livestatus As Thread
    Dim thread_Check_Forever_Reporting As Thread
    Dim thread_ReportTimeCheckThread As Thread
    Dim Rainmeter_Server As Thread

    Dim check_mtc As Thread

    Dim mch__ As CSI_Library.MCH_

    'mtc thread
    Dim thread_mtc_livestatus As Thread
    Dim thread_reload_temp_files As Thread
    Public Shared thread_Load_History_and_TodayData As Thread
    Public Shared thread_to_run_Reporting_Service As Thread
    Public AllMachines As New Dictionary(Of String, MachineData) 'Monioring id , and list of machine elements 
    Public DisplayMachineList As New Dictionary(Of String, String) 'machinename , rename_machinename
    Public Shared thread_Test_PieChart As Thread
    Public Shared DicOfReporting As New Dictionary(Of String, ReportingData) 'key is Task_name and Other Reporting data as Values 
    Public Shared isReportingDBChanged As New Boolean
    Public Shared ReportingStatus As Boolean
    Public Shared RestartTimeOfReporting As DateTime

    'timers
    Dim mysqlupdate_timer As New System.Timers.Timer
    Dim config_timer As New System.Timers.Timer
    Dim performance_timer As New System.Timers.Timer
    Dim performance_timer2 As New System.Timers.Timer
    Dim testftp_timer As New System.Timers.Timer
    Dim ADV_MTC_TIMER As New System.Timers.Timer
    Dim flush_log_timer As New System.Timers.Timer

    Dim timerTemperature As New Timers.Timer

    'service config vars
    Dim loadingAsCON As Boolean = False
    Dim ftpip As String = ""
    Dim ftppwd As String = ""

    Dim useUDP = True

    Dim weatherDictionary As Dictionary(Of String, String)

    'Common vars
    Dim enethttploaded As Boolean = True

    'Autoreporting
    Dim myreports As Autoreporting

    Public RMport_ As String

    Public Function StartThreadsAsync() As Task

#If DEBUG Then
        Thread.Sleep(20000)
#End If

        Try
            Log.Instance.Init("CSIFLEXService")
            Log.Info("CSIFLEX Service Started")
            Log.Info($"========================================================================================")

            If csi_lib.start_mysqld() Then

                MySqlAccess.Validate_Database()

                If Not license.IsLicenseValid(LicenseProducts.CSIFLEXServer) Then
                    Log.Fatal("The CSIFLEX Server License is not valid.")
                    End
                End If

                Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

                CSI_Library.CSI_Library.isServer = True

                ServerSettings.Instance.Init(Path.Combine(CSI_Library.CSI_Library.serverRootPath, "sys"))
                ServerSettings.CompanyName = license.CurrentLicense(LicenseProducts.CSIFLEXServer).CompanyName

                eNetServer.Instance.Init(ServerSettings.EnetFolder)

                LoadServerIp()

                GetEnetIp() 'To Load ENET IP ADDRESS
                GetEnetColors() 'To Load Colour Schemas From ENET

                StatusColor.Instance.Init()

                CreateFtpConfigTableIfNotExist()

                GetServiceConfigs() 'TO load ENET FTPIP and FTPPassword and also load  Loading as CYCLE ON or not parameter from Database

                LoadMachineList() 'To load EhubConf and Monsetup files and store it to List(Of MachineConfig)

                read_RM_port() 'Load Rain Meter Port from File 

                csi_lib.readsetup_for_adv_mtc() 'If any MTConnect/Focas Already exists then store all of them to MCHS_ Dictionary with all their parameters

                Dim machinesId As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)()
                Dim dbMachines As DataTable = MySqlAccess.GetDataTable("SELECT Id, EnetMachineName FROM csi_auth.tbl_ehub_conf;")
                For Each row As DataRow In dbMachines.Rows
                    machinesId.Add(row("Id"), row("EnetMachineName"))
                Next
                eNetServer.Instance.ImportMachinesId(machinesId)

                Try
                    LoadTemperatures()

                    eNETTempFiles.Instance.Init()
                    'LoadTodayHistoryData()
                Catch ex As Exception
                    Log.Error("LoadTodayHistoryData", ex)
                End Try

                Function_To_LoadHistoryForToday()

                'Try
                '    thread_reload_temp_files = New Thread(AddressOf LoadTodayHistoryData)
                '    thread_reload_temp_files.Name = "Load today history thread"
                '    thread_reload_temp_files.IsBackground = True
                '    thread_reload_temp_files.SetApartmentState(ApartmentState.STA)
                '    thread_reload_temp_files.Start()
                'Catch ex As Exception
                '    Log.Error("Thread LoadTodayHistoryData", ex)
                'End Try

                Try
                    thread_enet_livestatus = New Thread(AddressOf fct_enet_livestatus)
                    thread_enet_livestatus.Name = "Enet Livestatus thread"
                    thread_enet_livestatus.IsBackground = True
                    thread_enet_livestatus.SetApartmentState(ApartmentState.STA)
                    thread_enet_livestatus.Start()
                Catch ex As Exception
                    Log.Error("thread_enet_livestatus", ex)
                End Try


                Rainmeter_Server = New Thread(AddressOf Run_srv)
                Rainmeter_Server.Name = "Internal Server"
                Rainmeter_Server.IsBackground = True
                Rainmeter_Server.Start()


                thread_mtc_livestatus = New Thread(AddressOf fct_mtc_livestatus)
                thread_mtc_livestatus.Name = "Get MTC Livestatus thread"
                thread_mtc_livestatus.IsBackground = True
                thread_mtc_livestatus.Start()


                Dim threadBackup = New Thread(AddressOf BackupSettings)
                threadBackup.IsBackground = True
                threadBackup.Start()

                Start_update_mysqlDB()

                StartConfigUpdate()

                StartPerformanceUpdate()

                StartTemperatureLoad()

                Log.Info("Threads Started")

                Dim watcher As FileSystemWatcher = New FileSystemWatcher()

                watcher.Path = eNetServer.EnetReportsFolder
                watcher.Filter = "*.csv"
                watcher.NotifyFilter = NotifyFilters.Size Or NotifyFilters.LastWrite

                AddHandler watcher.Changed, AddressOf FileOnChange

                watcher.EnableRaisingEvents = True

            Else
                Log.Fatal("Could not start Mysql -- the service will not starts.")
            End If

        Catch ex As Exception
            Log.Fatal(ex)
        End Try

#Disable Warning BC42105 ' Function 'StartThreadsAsync' doesn't return a value on all code paths. A null reference exception could occur at run time when the result is used.
    End Function

    Private Sub BackupSettings()

        While Not _shutdownEvent.WaitOne(0)

            Dim mySqlFolder = "C:\\CSIFLEX\\MySQL\\mysql-8.0.18-winx64\\bin"

            If Not Directory.Exists(mySqlFolder) Then
                mySqlFolder = "C:\\Program Files (x86)\\CSI Flex Server\\mysql\\mysql-8.0.18-winx64\\bin"
            End If

            If Not Directory.Exists(mySqlFolder) Then
                mySqlFolder = "C:\\Program Files (x86)\\CSI Flex Server\\mysql\\mysql-5.7.21-win32\\bin"
            End If

            CSIFLEX.Server.Library.BackupSettings.Backup(mySqlFolder)

            Thread.Sleep(3600000)

        End While

    End Sub

    Public Sub FileOnChange(source As Object, e As FileSystemEventArgs)

        Dim inf As New FileInfo(e.FullPath)
        Log.Info($"====>> File changed {inf.Name} - File size {inf.Length / 1000}")

    End Sub

    Public Sub AlwaysCheckForTimeStampMeet()
        Try

            LoadTimesList()
            If DicOfReporting.Count > 0 Then
                Dim FirstElement = DicOfReporting.First()
                Dim FirstKeyVal = FirstElement.Key
                Dim FirtValues = FirstElement.Value
                If DateTime.Now.ToString("HH:mm") > FirtValues.TimeOfReport Then
                    ReportGenerator()
                Else
                    thread_ReportTimeCheckThread = New Thread(AddressOf ReportTimeCheckThread)
                    thread_ReportTimeCheckThread.Name = "Thread to Check Reporting Service"
                    thread_ReportTimeCheckThread.IsBackground = True
                    thread_ReportTimeCheckThread.Start(FirtValues)
                End If
            End If
        Catch ex As Exception
            csi_lib.LogServerError("Error in ReportScheduler : " & ex.Message & ex.StackTrace, 1)
        End Try

    End Sub

    Public Sub ReportGenerator()
        'If DateTime.Now.ToString("HH:mm") > FirtValues.TimeOfReport Then
        Try

            Dim serviceexepath As String = String.Empty
            serviceexepath = AppDomain.CurrentDomain.BaseDirectory + "CSIFlex Reports Generator Service.exe"
            'Start Reporting Service 
            If (ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlex_Reports_Generator_Service")) Then
                If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reports_Generator_Service") = ServiceTools.ServiceState.Run) Or
                            (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reports_Generator_Service") = ServiceTools.ServiceState.Starting) Then
                Else
                    ServiceTools.ServiceInstaller.StartService("CSIFlex_Reports_Generator_Service")
                    ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlex_Reports_Generator_Service")
                End If
            Else
                ServiceTools.ServiceInstaller.InstallAndStart("CSIFlex_Reports_Generator_Service", "CSIFlex_Reports_Generator_Service", serviceexepath)
                ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlex_Reports_Generator_Service")
            End If
        Catch ex As Exception
            ' csi_lib.LogServiceError("There was an Error trying To start the Reporting Service. See the log For more details :" & ex.Message, 1)
        End Try

        While True
            If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reports_Generator_Service") = ServiceTools.ServiceState.Run) Then
            Else
                If IsNothing(thread_ReportTimeCheckThread) = False Then
                    If thread_ReportTimeCheckThread.IsAlive Then
                        thread_ReportTimeCheckThread.Abort()
                    End If
                End If
                'Thread.Sleep(10000)
                AlwaysCheckForTimeStampMeet()
            End If
        End While

        ' End If
    End Sub


    Public Sub ReportTimeCheckThread(FirtValues As ReportingData)
        While Not DateTime.Now.AddMinutes(1).ToString("HH:mm") <= FirtValues.TimeOfReport

        End While
        ReportGenerator()
    End Sub

    Public Sub LoadTimesList()

        If DicOfReporting.Count > 0 Then
            DicOfReporting.Clear()
        End If

        'Add all the times from Table 
        Try
            Dim dtAllTimes = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.view_auto_report_config where done <> '{ DateTime.Now.DayOfWeek.ToString() }' Order By Time_ ASC;")

            If dtAllTimes.Rows.Count > 0 Then
                For Each rows As DataRow In dtAllTimes.Rows
                    If Not DicOfReporting.ContainsKey(rows.Item("Task_name").ToString) Then
                        'If Dictionary doesn't have the Task Name alredy saved 
                        DicOfReporting.Add(rows.Item("Task_name").ToString(),
                                           New ReportingData(rows.Item("Task_name").ToString,
                                           rows.Item("Time_").ToString,
                                           rows.Item("MailTo").ToString,
                                           rows.Item("done").ToString,
                                           rows.Item("short_FileName").ToString))
                    End If
                Next
                isReportingDBChanged = True
            End If
        Catch ex As Exception
            Log.Error("Error while adding times to Dictionary of Reporting.", ex)
        End Try

    End Sub

    Public Sub Function_To_LoadHistoryForToday()
        thread_Load_History_and_TodayData = New Thread(AddressOf LoadTodayHistoryData)
        thread_Load_History_and_TodayData.Name = "Load Today's History Data"
        thread_Load_History_and_TodayData.IsBackground = True
        thread_Load_History_and_TodayData.Start()
    End Sub

    Public Sub LoadTodayHistoryData()

        Log.Debug("LoadTodayHistoryData")

        Dim idx = 1

        While Not _shutdownEvent.WaitOne(0)

            Log.Info($"LoadTodayHistoryData - Loop Start: {idx}")

            Try
                Dim sqlCmmd = New Text.StringBuilder()
                Dim tempFiles = eNetDataSource.GetTempFiles()

                MySqlAccess.Delete_All_MachinePerf_Tables()

                MySqlAccess.ExecuteNonQuery("TRUNCATE TABLE CSI_Database.tbl_renameMachines")

                Try
                    For Each machine As eNetMachineConfig In eNetServer.MonitoredMachines

                        Dim status = ""
                        Dim partNr = ""
                        Dim operat = ""
                        Dim tempFile = tempFiles.FindAll(Function(r) r.MachineName = machine.MachineName And r.InTimeline)

                        MySqlAccess.Validate_Perf_Machine_Table(machine.MachineName)

                        MySqlQueries.InsertRenameMachine(machine.MachineName)

                        eNETTempFiles.AddMachine(machine.MachineId)

                        Dim curStatus = ""
                        Dim lstLine = ""
                        Dim qttLines = 0

                        For Each item As eNetTempFileModel In tempFile

                            eNETTempFiles.AddTempFileLine(machine.MachineId, item.Line)

                            lstLine = item.Line
                            status = item.Status

                            If status.StartsWith("_CON") Then status = "CYCLE ON"
                            If status.StartsWith("_COFF") Then status = "CYCLE OFF"
                            If status.StartsWith("_SETUP") Then status = "SETUP"

                            If machine.AlwaysRecordCycleOn Then

                                If curStatus = "SETUP" And status = "CYCLE ON" Then
                                    status = "SETUP-CYCLE ON"
                                Else
                                    curStatus = status
                                End If

                            End If

                            sqlCmmd.Clear()
                            sqlCmmd.Append($"INSERT IGNORE INTO        ")
                            sqlCmmd.Append($"    CSI_machineperf.{ machine.MachineDbTable } ")
                            sqlCmmd.Append($" (                        ")
                            sqlCmmd.Append($"    status              , ")
                            sqlCmmd.Append($"    time                , ")
                            sqlCmmd.Append($"    cycletime           , ")
                            sqlCmmd.Append($"    shift               , ")
                            sqlCmmd.Append($"    date                , ")
                            sqlCmmd.Append($"    partnumber          , ")
                            sqlCmmd.Append($"    operator            , ")
                            sqlCmmd.Append($"    comments              ")
                            sqlCmmd.Append($" )                        ")
                            sqlCmmd.Append($" VALUES                   ")
                            sqlCmmd.Append($" (                        ")
                            sqlCmmd.Append($"    '{ status }'        , ")
                            sqlCmmd.Append($"     { item.TimeId }    , ")
                            sqlCmmd.Append($"    '{ item.CycleTime }', ")
                            sqlCmmd.Append($"    '{ item.Shift }'    , ")
                            sqlCmmd.Append($"    '{ item.DateTime.ToString("yyyy-MM-dd HH:mm:ss") }', ")
                            sqlCmmd.Append($"    '{ item.PartNr }'   , ")
                            sqlCmmd.Append($"    '{ item.Operator }' , ")
                            sqlCmmd.Append($"    '{ item.Comment }'    ")
                            sqlCmmd.Append($" )                        ")

                            MySqlAccess.ExecuteNonQuery(sqlCmmd.ToString())
                            qttLines += 1
                        Next
                        Log.Info($"LoadHistoryData - Machine: {machine.MachineName}, Lines: {qttLines}, Status: {status}, Last line: {lstLine}")
                    Next

                Catch ex As Exception
                    Log.Error("1", ex)
                End Try

                Try
                    tempFiles = Nothing

                    AllMachines.Clear()

                    Dim dtMachines As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf WHERE Monstate='1';")
                    Dim monitId As String = ""

                    For Each row As DataRow In dtMachines.Rows
                        monitId = row("monitoring_id").ToString()
                        If Not AllMachines.ContainsKey(monitId) Then
                            AllMachines.Add(monitId, New MachineData())
                        End If

                        AllMachines.Item(monitId).MachineName_ = row("machine_name").ToString()
                        AllMachines.Item(monitId).EnetMachineName_ = row("EnetMachineName").ToString()
                        AllMachines.Item(monitId).MachineLabel_ = row("machine_label").ToString()
                        AllMachines.Item(monitId).MonitoringFileName_ = row("monitoring_filename").ToString()
                        AllMachines.Item(monitId).MonitoringState_ = row("Monstate").ToString()
                        AllMachines.Item(monitId).MonitoringID_ = row("monitoring_id").ToString()
                    Next

                    'Return

                Catch ex As Exception
                    Log.Error("1", ex)
                    Return
                End Try
            Catch ex As Exception
                Log.Error("0", ex)
                Return
            End Try

            Thread.Sleep(60000)

            Log.Info($"LoadTodayHistoryData - Loop End: {idx}")
            idx += 1
        End While

        If thread_Load_History_and_TodayData IsNot Nothing Then
            If thread_Load_History_and_TodayData.IsAlive Then
                thread_Load_History_and_TodayData.Abort()
            End If
        End If

    End Sub

    Private Sub CreateFtpConfigTableIfNotExist()

        Try

            Dim dTable_message13 = MySqlAccess.GetDataTable("SELECT * from CSI_auth.ftpconfig;")

            If dTable_message13.Rows.Count = 0 Then

                MySqlAccess.ExecuteNonQuery($"INSERT INTO CSI_auth.ftpconfig ( ftpip, ftppwd ) VALUES ('{ eNetServer.Connections.FtpIp }', '{eNetServer.Connections.FtpPassword}') ;")

            End If

        Catch ex As Exception
            csi_lib.LogServerError("FTP Config error:" & ex.Message, 1)
        End Try

    End Sub


    Public Sub RefreshAllDevices()

        Try

            Dim port_number = LoadPortNumberFromDB()

            If String.IsNullOrEmpty(port_number) Then
                port_number = "8008"
            End If
            send_http_req(port_number)

            Dim request As WebRequest = WebRequest.Create("http://127.0.0.1:" & port_number & "/readPerf")

#Disable Warning BC42104 ' Variable 'request' is used before it has been assigned a value. A null reference exception could result at runtime.
            request.Method = "POST"
#Enable Warning BC42104 ' Variable 'request' is used before it has been assigned a value. A null reference exception could result at runtime.

            Dim postData As String = ""
            Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)

            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length

            Dim dataStream As Stream = request.GetRequestStream()

            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

        Catch ex As Exception
            Log.Error("Unable to ask for refresh", ex)
        End Try

    End Sub

    Sub send_http_req(port_num As String)

        Try
            Dim request As WebRequest = WebRequest.Create("http://127.0.0.1:" & port_num & "/readconfig")
            request.Method = "POST"

            Dim postData As String = ""
            Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)

            ' Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded"

            ' Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length

            Dim dataStream As Stream = request.GetRequestStream()

            ' Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length)

            ' Close the Stream object.
            dataStream.Close()

        Catch ex As Exception

            Log.Error(ex)
            csi_lib.LogServerError("Unable to send http req" + ex.Message, 1)

        End Try

    End Sub

    Public Sub GetAllEnetMachines2()

        Try
            For Each machine As eNetMachineConfig In eNetServer.Machines

                Dim sqlCmd As Text.StringBuilder = New StringBuilder()

                Dim conType = IIf(machine.Protocol = "FTP", 5, 0)
                Dim machLabel = $"MCH_{machine.EnetMachineId}"
                Dim monState As Integer = IIf(machine.IsMonitored, 1, 0)
                Dim thValue As Integer = 0
                If machine.TwoHeads Then
                    thValue = 1
                End If
                If machine.TwoPallets Then
                    thValue = 2
                End If

                sqlCmd.Append($"INSERT INTO csi_auth.tbl_ehub_conf                  ")
                sqlCmd.Append($" (                                                  ")
                sqlCmd.Append($"    `monitoring_id`       ,                         ")
                sqlCmd.Append($"    `machine_name`        ,                         ")
                sqlCmd.Append($"    `EnetMachineName`     ,                         ")
                sqlCmd.Append($"    `Con_type`            ,                         ")
                sqlCmd.Append($"    `monitoring_filename` ,                         ")
                sqlCmd.Append($"    `Monstate`            ,                         ")
                sqlCmd.Append($"    `machine_label`       ,                         ")
                sqlCmd.Append($"    `ftpfilename`         ,                         ")
                sqlCmd.Append($"    `CurrentStatus`       ,                         ")
                sqlCmd.Append($"    `CurrentPartNumber`   ,                         ")
                sqlCmd.Append($"    `EnetDept`            ,                         ")
                sqlCmd.Append($"    `TH_State`            ,                         ")
                sqlCmd.Append($"    `Redirect`                                      ")
                sqlCmd.Append($" )                                                  ")
                sqlCmd.Append($" VALUES                                             ")
                sqlCmd.Append($" (                                                  ")
                sqlCmd.Append($"    '{ machine.EnetPos }'             ,             ")
                sqlCmd.Append($"    '{ machine.MachineName }'         ,             ")
                sqlCmd.Append($"    '{ machine.MachineName }'         ,             ")
                sqlCmd.Append($"    '{ conType }'                     ,             ")
                sqlCmd.Append($"    '{ machine.MonitoringFileName }'  ,             ")
                sqlCmd.Append($"    '{ monState }'                    ,             ")
                sqlCmd.Append($"    '{ machLabel }'                   ,             ")
                sqlCmd.Append($"    '{ machine.FTPFileName }'         ,             ")
                sqlCmd.Append($"    ''                                ,             ")
                sqlCmd.Append($"    ''                                ,             ")
                sqlCmd.Append($"    '{ machine.Department }'          ,             ")
                sqlCmd.Append($"    '{ thValue }'                     ,             ")
                sqlCmd.Append($"    '{ machine.RedirectDPrint }'                    ")
                sqlCmd.Append($" )                                                  ")
                sqlCmd.Append($" ON DUPLICATE KEY UPDATE                            ")
                sqlCmd.Append($"    `EnetMachineName` = '{ machine.MachineName }',  ")
                sqlCmd.Append($"    `Con_type`        = '{ conType }'            ,  ")
                sqlCmd.Append($"    `MonState`        = '{ monState }'           ,  ")
                sqlCmd.Append($"    `ftpfilename`     = '{ machine.FTPFileName }',  ")
                sqlCmd.Append($"    `EnetDept`        = '{ machine.Department }' ,  ")
                sqlCmd.Append($"    `TH_State`        = '{ thValue }'            ,  ")
                sqlCmd.Append($"    `Redirect`        = '{ machine.RedirectDPrint }';  ")

                sqlCmd.Append($"UPDATE IGNORE `csi_auth`.`tbl_ehub_conf`            ")
                sqlCmd.Append($" SET                                                ")
                sqlCmd.Append($"    `Machine_Name`    = '{ machine.MachineName }'   ")
                sqlCmd.Append($" WHERE                                              ")
                sqlCmd.Append($"     `monitoring_id`  = '{ machine.EnetPos }'       ")
                sqlCmd.Append($" AND `Machine_Name`   = ''                    ;     ")

                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())
            Next

        Catch ex As Exception
            Log.Error(ex)
        End Try

    End Sub

    Public Sub StopThreads()

        Try

            If Not thread_mtc_livestatus.Join(3000) Then
                ' give the thread 3 seconds to stop
                thread_mtc_livestatus.Abort()
            End If

            DisconnectEnetLivestatus()
            DisconnectMtcLivestatus()

            mysqlupdate_timer.Stop()
            config_timer.Stop()
            ADV_MTC_TIMER.Stop()

        Catch ex As Exception
            Log.Error(ex)
        End Try

        Log.Debug("Thread stopped ")

    End Sub

#Region "adv mtc"

    Dim temp_mtc_adv_livestatus As New DataTable

    Private Function evaluate_status() As DataTable

        Dim status_result As String = ""
        '---------------------------------------------------------------------------------------------------
        'table to be returned:
        Dim status_result_tbl As New DataTable
        Dim prime = status_result_tbl.Columns.Add("MachineId", GetType(Integer))

        status_result_tbl.Columns.Add("MachineName", GetType(String))
        status_result_tbl.Columns.Add("MTCMachineName", GetType(String))
        status_result_tbl.Columns.Add("Status", GetType(String))
        status_result_tbl.Columns.Add("Program", GetType(String))
        status_result_tbl.Columns.Add("PartNumber", GetType(String))
        status_result_tbl.Columns.Add("MonitBoardId", GetType(Integer))
        status_result_tbl.Columns.Add("MonitActive", GetType(Boolean))
        status_result_tbl.Columns.Add("MonitOverride", GetType(Boolean))
        status_result_tbl.Columns.Add("MonitCSD", GetType(Boolean))
        status_result_tbl.Columns.Add("Pallet", GetType(String))

        status_result_tbl.PrimaryKey = New DataColumn() {prime}

        '---------------------------------------------------------------------------------------------------
        ' Find the status:
        For Each MCH_ In csi_lib.MCHS_.Values.ToList

            status_result = ""
            Dim r As DataRow = status_result_tbl.NewRow

            r("MachineId") = MCH_.MachineId
            r("MachineName") = MCH_.MachineName
            r("MTCMachineName") = MCH_.MTCMachineName

            Log.Debug($"===> Evaluate Status - Machine name: {MCH_.MachineName} - {MCH_.MachineId} - {MCH_.current}")

            If MCH_.unreachable Then

                status_result = "NOeMONITORING"

            Else

                For Each status__ In MCH_.Conditions_expression_.Keys.ToList

                    '-------------------------------------------------------------------------------------------
                    ' csi_lib.Evaluate_logic = Get current values + evaluate logic
                    Evaluate_logic(MCH_.Conditions_expression_(status__), MCH_)

                    '-------------------------------------------------------------------------------------------
                    ' Interpret logical result:
                    If MCH_.rpn_result_intern_ IsNot Nothing Then

                        If MCH_.rpn_result_intern_.NumValue = 1 Then
                            If status_result = "" Then
                                status_result = status__
                            Else
                                status_result = "Conflicting stat"
                            End If
                        End If

                    End If
                Next
            End If

            'Log.Info($"*=*=*=* { MCH_.MachineId } - Set new status: {status_result} ")

            'Log.Debug($"===> Machine name: {MCH_.MachineName} - status_result : { status_result } - Before")

            If status_result = "CYCLE START DISABLE" Then

                If MCH_.current() = "CYCLE OFF" Or MCH_.current() = "CYCLE ON" Then

                    MCH_.current() = status_result
                    status_result = "CYCLE OFF"

                ElseIf MCH_.current() = "CYCLE START DISABLE" Then

                    status_result = MCH_.current()

                Else

                    MCH_.current() = "CYCLE OFF"
                    status_result = MCH_.current()

                End If

            Else

                If status_result = "" Or status_result = Nothing Then
                    MCH_.current() = "CYCLE OFF"
                Else
                    MCH_.current() = status_result
                End If
                status_result = MCH_.current()

            End If

            'Log.Info($"*=*=*=* { MCH_.MachineId } - Current status: {MCH_.current()} ({status_result})")

            'Log.Debug($"=====> Machine name: {MCH_.MachineName} - status_result : { status_result } - After")

            '---------------------------------------------------------------------------------------------------
            'Other infos :

            If MCH_.current_other.ContainsKey("ProgramNumber") Then

                If MCH_.current_other("ProgramNumber") <> "NOT AVAILABLE" Then
                    r("Program") = MCH_.current_other("ProgramNumber")
                End If

            End If

            If MCH_.current_other.ContainsKey("PartNumber") Then
                If MCH_.current_other("PartNumber") <> "UNAVAILABLE" And MCH_.current_other("PartNumber") <> "NOT AVAILABLE" Then
                    r("PartNumber") = MCH_.current_other("PartNumber")
                Else
                    r("PartNumber") = ""
                End If
            End If

            r("MonitBoardId") = MCH_.MonitoringBoardId
            If r("MonitBoardId") > 0 Then
                If MCH_.current_other.ContainsKey("pallet") Then
                    If MCH_.current_other("pallet") <> "UNAVAILABLE" And MCH_.current_other("pallet") <> "NOT AVAILABLE" Then
                        r("Pallet") = MCH_.current_other("pallet")
                    Else
                        r("Pallet") = ""
                    End If
                Else
                    r("Pallet") = ""
                End If
            End If

            r("Status") = status_result
            status_result_tbl.Rows.Add(r)
        Next

        Return status_result_tbl

    End Function

    Private Function interpret_expression(expression As String) As String

        expression = expression.Replace(" And ", " && ")
        expression = expression.Replace(" And ", " && ")
        expression = expression.Replace(" And ", " && ")
        expression = expression.Replace(" Or ", " || ")
        expression = expression.Replace(" Or ", " && ")
        expression = expression.Replace(" Or ", " && ")

        expression = expression.Replace(" AND ", " && ")
        expression = expression.Replace(" AND ", " && ")
        expression = expression.Replace(" AND ", " && ")
        expression = expression.Replace(" OR ", " || ")
        expression = expression.Replace(" OR ", " && ")
        expression = expression.Replace(" OR ", " && ")
        expression = expression.Replace("=/=", " != ")

        Return expression

    End Function

    Public Sub Evaluate_logic(expression As String, ByRef MCH__ As MCH_)

        Try
            Dim rpn_result As RpnOperand

            rpn_result = Nothing
            MCH__.rpn_result_intern_ = Nothing
            '-------------------------------------------------------------------------------------------

            If expression <> "" Then

                expression = interpret_expression(expression)

                Dim Parser_ As ITokeniser = New MathParser()
                Dim tokens As List(Of RpnElement) = Parser.ParseExpression(Parser_, expression)
                Dim ndx As Integer = 0
                Dim current_value = New List(Of String)

                Dim BufferList_ As List(Of String) = MCH__.Current_selected_

                For Each element As String In BufferList_
                    If Not current_value.Contains(element) Then current_value.Add(element)
                Next

                Dim rpn As New Rpn_function.Rpn

                If MCH__.MachineName = "" Then
                    rpn_result = rpn.RpnEval(tokens, current_value)
                Else
                    MCH__.rpn_result_intern_ = rpn.RpnEval(tokens, current_value)
                End If
            End If

        Catch ex As Exception
            Log.Error($"Machine: {MCH__.MachineName}, expression: {expression}")
            Log.Error(ex)
        End Try

    End Sub

#End Region


#Region "Http Server"

    Dim srv As New HttpListener
    Dim refresh_page As New Dictionary(Of String, Boolean)

    Private Sub Run_srv()

        Try
            read_dashboard_config_fromdb()

            Read_perf_from_db()

            Fill_REALTIME_dic()

            compute_trends()

            If String.IsNullOrEmpty(RMport_) Then RMport_ = "8008"
            srv.Prefixes.Add("http://+:" & RMport_ & "/")

            srv.Start()

            rm_server_listen()

            srv.Stop()

        Catch ex As Exception

            Log.Error(ex)

        End Try

        Log.Debug("B-09")

    End Sub

    Private Sub read_RM_port()
        Try
            If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")) Then

                Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")

                    RMport_ = reader.ReadLine

                    reader.Close()

                End Using
            Else
                RMport_ = "8008"
            End If
        Catch ex As Exception
            csi_lib.LogServerError("At reading RM Port from csys: " & ex.Message, 1)
        End Try
    End Sub

    Private Sub rm_server_listen()
        While True
            Dim context = srv.GetContext()
            Task.Factory.StartNew(Function() Process_req(context))
            Thread.Sleep(100)
        End While
        srv.Close()
    End Sub

    Private Function Process_req(context As HttpListenerContext)

        Dim request As HttpListenerRequest = context.Request
        Dim response As HttpListenerResponse = context.Response

        Dim rootpath As String = CSI_Library.CSI_Library.serverRootPath + "\html\html"

        Dim RAW_URL As String() = Convert.ToString(request.RawUrl).Split("?")
        If Convert.ToString(request.RawUrl) = "/?SEL=-1" Then RAW_URL(0) = "/?SEL=-1"

        If (RAW_URL(0).Contains("/api")) Then
            Api.handleRoutes(request, response)
        End If

        Select Case RAW_URL(0)
            Case "/reloadMachines"

                SendStatusToEnetUDP(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, "16,8", "PART,0,0,1")
                eNetServer.Instance.ReloadMachines()
                Log.Debug("RELOAD MACHINES")

            Case "/stopThreads"

                Log.Debug("STOPPING THREADS")
                StopThreads()

                Log.Debug("THREADS STOPPEND")

            Case "/conditions"

                If RAW_URL.Count = 2 Then
                    csi_lib.Log_server_event("(web srv) - conditions request for machine: " & Convert.ToString(RAW_URL(1)))
                Else
                    csi_lib.Log_server_event("(web srv) - conditions request for all machines. ")
                End If
            Case "/readMTCconfig"

                Try
                    csi_lib.readsetup_for_adv_mtc()

                    Dim output As System.IO.Stream
                    Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes("ok,thanks ;) ")
                    response.ContentLength64 = buffer.Length
                    response.AppendHeader("Access-Control-Allow-Origin", "*")
                    output = response.OutputStream
                    output.Write(buffer, 0, buffer.Length)
                    Thread.Sleep(100)
                    output.Close()
                    output.Dispose()
                    config_changed = True

                Catch ex As Exception
                    csi_lib.LogServiceError("Err while process Websrv req: ACK TO CSIF SERVER /readMTCconfig : " & ex.Message & ex.StackTrace, 1)
                End Try

            Case "/readconfig"

                read_dashboard_config_fromdb()

                LoadTemperatures()

                For Each key In refresh_page.Keys.ToList
                    refresh_page(key) = True
                Next

                Try
                    Dim output As System.IO.Stream
                    Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes("ok,thanks ;) ")
                    response.ContentLength64 = buffer.Length
                    response.AppendHeader("Access-Control-Allow-Origin", "*")
                    output = response.OutputStream
                    output.Write(buffer, 0, buffer.Length)
                    Thread.Sleep(100)
                    output.Close()
                    output.Dispose()
                    config_changed = True

                Catch ex As Exception
                    csi_lib.LogServiceError("Err while process Websrv req: ACK TO CSIF SERVER /readconfig : " & ex.Message & ex.StackTrace, 1)
                End Try

            Case "/readPerf" 'Read Performance Table MEans Pie Chart And Bar Chart

                Read_perf_from_db()

                For Each key In refresh_page.Keys.ToList
                    refresh_page(key) = True
                Next

                Try

                    Dim output As System.IO.Stream
                    Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes("ok,thanks ;) ")
                    response.ContentLength64 = buffer.Length
                    response.AppendHeader("Access-Control-Allow-Origin", "*")
                    output = response.OutputStream
                    output.Write(buffer, 0, buffer.Length)
                    Thread.Sleep(100)
                    output.Close()
                    output.Dispose()
                    config_changed = True

                Catch ex As Exception
                    csi_lib.LogServiceError("Err while process Websrv req: ACK TO CSIF SERVER /readPerf : " & ex.Message & ex.StackTrace, 1)
                End Try

            Case "/enduserconfig"

                If license.IsLicenseValid(LicenseProducts.CSIFLEXServer) Then

                    send_config(context)

                    If refresh_page.ContainsKey(Convert.ToString(context.Request.LocalEndPoint)) Then
                        refresh_page(Convert.ToString(context.Request.LocalEndPoint)) = False
                    Else
                        refresh_page.Add(Convert.ToString(context.Request.LocalEndPoint), False)
                    End If

                End If

            Case "/ComputePerf" 'Case Where we read all machine data to Compute Pie Chart and Targets Bar Chart Information
                If perf_computed___ = True Then

                    Task.Factory.StartNew(Function() compute_perf_from_db())

                    For Each key In refresh_page.Keys.ToList
                        refresh_page(key) = True
                    Next

                    Try

                        Dim output As System.IO.Stream
                        Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes("ok,thanks ;) ")
                        response.ContentLength64 = buffer.Length
                        response.AppendHeader("Access-Control-Allow-Origin", "*")
                        output = response.OutputStream
                        output.Write(buffer, 0, buffer.Length)
                        Thread.Sleep(100)
                        output.Close()
                        output.Dispose()
                        config_changed = True

                        'csi_lib.Log_server_event("(web srv) - Command recieved to read the enduserconfig.")
                    Catch ex As Exception
                        csi_lib.LogServiceError("Err while process Websrv req: ACK TO CSIF SERVER /ComputePerf : " & ex.Message & ex.StackTrace, 1)
                    End Try
                End If

            Case "/timeline"

                send_timeline(context)

            Case "/refresh"
                'Every 2 seconds page refreshed the code is on JavaScript Side 
                refresh_dash(context)

                If refresh_page.ContainsKey(Convert.ToString(context.Request.LocalEndPoint)) Then
                    '   refresh_page(context.Request.LocalEndPoint.ToString()) = False
                Else
                    refresh_page.Add(Convert.ToString(context.Request.LocalEndPoint), False)
                End If

            Case "/mobile"
                'Every 2 seconds page refreshed the code is on JavaScript Side 
                refresh_dash(context, True)

                If refresh_page.ContainsKey(Convert.ToString(context.Request.LocalEndPoint)) Then
                    '   refresh_page(context.Request.LocalEndPoint.ToString()) = False
                Else
                    refresh_page.Add(Convert.ToString(context.Request.LocalEndPoint), False)
                End If

            Case "/client" 'Copy of ENET Webpage localhost:8080

                Dim output As System.IO.Stream
                Try

                    Dim responseString As String = If(enet_page2 = Nothing, "", enet_page2)
                    Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(responseString)
                    response.ContentLength64 = buffer.Length
                    response.AppendHeader("Access-Control-Allow-Origin", "*")
                    output = response.OutputStream
                    output.Write(buffer, 0, buffer.Length)
                    Thread.Sleep(100)
                    output.Close()
                    output.Dispose()

                Catch ex As Exception
                    csi_lib.LogServiceError("Err while process Websrv req: /client : " & ex.Message & ex.StackTrace, 1)
                End Try


            Case "/IP"


                Dim output As System.IO.Stream
                Try

                    Dim responseString As String = Convert.ToString(context.Request.LocalEndPoint)
                    Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(responseString)
                    response.ContentLength64 = buffer.Length
                    response.AppendHeader("Access-Control-Allow-Origin", "*")
                    output = response.OutputStream
                    output.Write(buffer, 0, buffer.Length)
                    Thread.Sleep(100)
                    output.Close()
                    output.Dispose()

                Catch ex As Exception
                    csi_lib.LogServiceError("Err while process Websrv req /IP : " & ex.Message & ex.StackTrace, 1)
                End Try

            Case "/hi"
                Try

                    Dim output As System.IO.Stream
                    Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes("hi :) " + vbCrLf + Convert.ToString(real_time_dic_filled))
                    response.ContentLength64 = buffer.Length
                    response.AppendHeader("Access-Control-Allow-Origin", "*")
                    output = response.OutputStream
                    output.Write(buffer, 0, buffer.Length)
                    Thread.Sleep(100)
                    output.Close()
                    output.Dispose()
                    config_changed = True

                    csi_lib.Log_server_event("(web srv) - hi recieved :).")
                Catch ex As Exception
                    csi_lib.LogServiceError("Err while process Websrv req: ACK TO hi :( " & ex.Message, 1)
                End Try

            Case "/resps"

                returnFile(context, rootpath + "/resps2.html")


            Case "/DL_CLIENT"

                Try

                    Dim output As System.IO.Stream
                    Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes("the DL is not available :/, stay tuned ;) ")
                    response.ContentLength64 = buffer.Length
                    response.AppendHeader("Access-Control-Allow-Origin", "*")
                    output = response.OutputStream
                    output.Write(buffer, 0, buffer.Length)
                    Thread.Sleep(100)
                    output.Close()
                    output.Dispose()

                Catch ex As Exception
                    csi_lib.LogServiceError("Err while process Websrv req: DL_client response: " & ex.Message, 1)
                End Try
            Case "/api"
                'Not used was put just for learning purposes @Maksim
                Api.handleRoutes(request, response)


            Case "GenerateReports"



            Case "/?SEL=-1" 'Copy of ENET Webpage localhost:8080

                Dim output As System.IO.Stream
                Try

                    Dim responseString As String = If(enet_page = Nothing, "", enet_page)
                    Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(responseString)
                    response.ContentLength64 = buffer.Length
                    response.AppendHeader("Access-Control-Allow-Origin", "*")
                    output = response.OutputStream
                    output.Write(buffer, 0, buffer.Length)
                    Thread.Sleep(100)
                    output.Close()
                    output.Dispose()

                Catch ex As Exception
                    csi_lib.LogServiceError("Err while process Websrv req: /?SEL=-1 : " & ex.Message & ex.StackTrace, 1)
                End Try

            Case "/"

                returnFile(context, rootpath + "\index.html")

            Case Else

                If RAW_URL(0).StartsWith("/saved_pages") Then
                    send_resps_file(RAW_URL(0), context)
                End If
                returnFile(context, rootpath + request.RawUrl)
        End Select


        Return True

    End Function

    Private Sub send_resps_file(what As String, context As HttpListenerContext)

        Dim rootpath As String = CSI_Library.CSI_Library.serverRootPath + "\html\html"
        Dim small_text As String = ""
        Dim big_text As String = ""
        Dim name As String = ""

        Dim filepath As String = rootpath + "\Notification\resps.html"
        Dim resps As String = ""

        If (File.Exists(filepath)) Then
            Using reader As StreamReader = New StreamReader(filepath)
                resps = reader.ReadToEnd
                reader.Close()
            End Using
        End If

        If (File.Exists(rootpath + what)) Then
            Using reader As StreamReader = New StreamReader(rootpath + what)
                name = reader.ReadLine
                small_text = reader.ReadLine
                big_text = reader.ReadToEnd
                reader.Close()
            End Using
        End If

        resps = resps.Replace("[TEXT]", small_text)
        resps = resps.Replace("[BIG_TEXT]", big_text)
        resps = resps.Replace("{mchName}", name)

        Dim output As System.IO.Stream
        Try

            Dim responseString As String = resps
            Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(responseString)
            context.Response.ContentLength64 = buffer.Length
            context.Response.AppendHeader("Access-Control-Allow-Origin", "*")
            output = context.Response.OutputStream
            output.Write(buffer, 0, buffer.Length)
            Thread.Sleep(100)
            output.Close()
            output.Dispose()

        Catch ex As Exception
            csi_lib.LogServiceError("Err while process Websrv req:  send_resps_file Function called :" & ex.Message & ex.StackTrace, 1)
        End Try

    End Sub

    Private Const bufferSize As Integer = 1024 * 512    '512KB

    Private Sub returnFile(context As HttpListenerContext, filePath As String)

        Try
            context.Response.ContentType = getcontentType(Path.GetExtension(filePath))
            Dim buffer = New Byte(bufferSize - 1) {}
            Using fs = File.OpenRead(filePath)
                context.Response.ContentLength64 = fs.Length
                Dim read As Integer
                While (InlineAssignHelper(read, fs.Read(buffer, 0, buffer.Length))) > 0
                    context.Response.OutputStream.Write(buffer, 0, read)
                End While
            End Using

        Catch ex As Exception
            csi_lib.LogServiceError("Err while sending a file (websrv): " & filePath & ex.Message, 1)


            context.Response.StatusCode = HttpStatusCode.NotFound
            context.Response.ContentType = "text/plain"

            context.Response.StatusDescription = ex.Message

        End Try
        context.Response.OutputStream.Close()
    End Sub

    Dim _DATASET_to_be_sent As New DataSet("Refreshed_data")
    Dim config_table As New DataTable("config")
    Dim tbl_device_machines_groups As New DataTable("tbl_device_machines_groups")
    Dim RealTime_datatable As New DataTable("values")
    Dim tbl_messages As New DataTable("tbl_messages")
    Dim config_changed As Boolean = False

    Private Sub refresh_dash(context As HttpListenerContext, Optional isMobile As Boolean = False)

        Try
            Dim JSON As String = ""
            ' we need to load RealTimeTable here just before the refresh 
            Dim TMP__DATASET_to_be_sent As New DataSet("Refreshed_data")
            'filter + Check if everything is ok in the table

            Dim tble_2_send = VALIDATE_DATA(RealTime_datatable, context, isMobile)

            TMP__DATASET_to_be_sent.Tables.Add(tble_2_send)

            'SEND THE DATASET TO THE DASHBOARD ///////////////////////////////////////////////////////////////////
            If TMP__DATASET_to_be_sent IsNot Nothing Then
                JSON = Convet_DataTable_To_JSONW(TMP__DATASET_to_be_sent)
            End If

            JSON = JSON.Remove(JSON.Length - 1)
            If refresh_page.ContainsKey(Convert.ToString(context.Request.LocalEndPoint)) Then
                JSON = JSON + ",""Additional_infos"":[{""config_changed"":""" + Convert.ToString(refresh_page(Convert.ToString(context.Request.LocalEndPoint))) + """,{WeatherTag},{ClockTag}}]}"
            Else
                JSON = JSON + ",""Additional_infos"":[{""config_changed"":""False"",{WeatherTag},{ClockTag}}]}"
            End If

            JSON = JSON.Replace("{ClockTag}", $"""Clock"":""{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }""")

            Dim IpAddress = context.Request.RemoteEndPoint.Address.ToString()

            If Not weatherDictionary.ContainsKey(IpAddress) Then
                IpAddress = IpAddress.Substring(0, IpAddress.LastIndexOf(".")) + ".*"
            End If

            If weatherDictionary.ContainsKey(IpAddress) Then
                JSON = JSON.Replace("{WeatherTag}", $"""Weather"":""{ weatherDictionary(IpAddress) }""")
            Else
                JSON = JSON.Replace("{WeatherTag}", """Weather"":""False""")
            End If

            Send_response(context, JSON)
            TMP__DATASET_to_be_sent.Dispose()

        Catch ex As Exception
            ' csi_lib.LogServiceError("Err while sending JSON response (websrv): " & ex.Message + " , " + vbCrLf + "___" + ex.StackTrace, 1)
        End Try

        context.Response.OutputStream.Close()

    End Sub

    Private Sub add_utilization(ByRef tbl As DataTable)

        Dim timeCON As Object
        Dim timeLOAD As Object
        Dim timeSETUP As Object
        Dim timeCOFF As Object
        Dim timeOTHER As Object
        Dim percCON As Object
        Dim percSETUP As Object
        Dim percCOFF As Object
        Dim percOTHER As Object

        Dim total As Object
        Dim seconds As Double = 0

        Dim maxtime As New Dictionary(Of String, Dictionary(Of String, Integer))
        Dim perf_dic As Dictionary(Of String, Integer)

        Dim machineId As String = ""
        Dim enetMachine As String = ""
        Dim rapidOverrideValue As String = ""
        Dim errorPos As Integer = 0

        Try

            Dim dtUser As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.users")

            For Each ro As DataRow In tbl.Rows
                Try
                    perf_dic = New Dictionary(Of String, Integer) ' to clear it
                    machineId = ro("MachineId")
                    enetMachine = ro("EnetMachine")

                    errorPos = 1

                    If realtime_dic.ContainsKey(machineId) Then

                        If Not (realtime_dic(machineId).Rows.Count = 0) Then

                            'Calculate the percent of each status
                            total = realtime_dic(machineId).Compute("SUM(cycletime)", "")
                            timeCON = realtime_dic(machineId).Compute("SUM(cycletime)", "status = 'CYCLE ON'")
                            timeLOAD = realtime_dic(machineId).Compute("SUM(cycletime)", "status = 'LOADING'")
                            timeCOFF = realtime_dic(machineId).Compute("SUM(cycletime)", "status = 'CYCLE OFF'")
                            timeSETUP = realtime_dic(machineId).Compute("SUM(cycletime)", "status = 'SETUP'")

                            errorPos = 2

                            timeCON = If(IsDBNull(timeCON), 0, timeCON)
                            timeCON = If(IsDBNull(timeLOAD), timeCON, timeCON + timeLOAD)
                            timeCOFF = If(IsDBNull(timeCOFF), 0, timeCOFF)
                            timeSETUP = If(IsDBNull(timeSETUP), 0, timeSETUP)
                            timeOTHER = total - timeCON - timeCOFF - timeSETUP

                            errorPos = 3

                            If total > 0 Then
                                percCON = If(IsDBNull(timeCON), 0, timeCON) * 100 / total
                                percCOFF = If(IsDBNull(timeCOFF), 0, timeCOFF) * 100 / total
                                percSETUP = If(IsDBNull(timeSETUP), 0, timeSETUP) * 100 / total
                            Else
                                percCON = 0
                                percCOFF = 0
                                percSETUP = 0
                            End If

                            errorPos = 4

                            perf_dic.Add("CON", percCON)
                            perf_dic.Add("COFF", percCOFF)
                            perf_dic.Add("SETUP", percSETUP)

                            errorPos = 5

                            percOTHER = 100 - Math.Round(percCON) - Math.Round(percCOFF) - Math.Round(percSETUP)
                            perf_dic.Add("OTHER", percOTHER)

                            perf_dic.Add("timeCON", If(IsDBNull(timeCON), 0, timeCON))
                            perf_dic.Add("timeCOFF", If(IsDBNull(timeCOFF), 0, timeCOFF))
                            perf_dic.Add("timeSETUP", If(IsDBNull(timeSETUP), 0, timeSETUP))
                            perf_dic.Add("timeOTHER", If(IsDBNull(timeOTHER), 0, timeOTHER))

                            ro.Item("Utilization") = JsonConvert.SerializeObject(perf_dic)

                            errorPos = 6

                            If trend_dic.ContainsKey(machineId) Then ro.Item("Trend") = trend_dic(machineId)

                            If Not maxtime.ContainsKey(machineId) Then
                                maxtime.Add(machineId, New Dictionary(Of String, Integer))
                                maxtime(machineId).Add("CYCLE ON", 0)
                                maxtime(machineId).Add("SETUP", 0)
                            End If

                            errorPos = 7

                            If ro("CurrentCycle") = "--:--:--" Then ro("CurrentCycle") = "00:00:00"
                            If ro("ElapsedTime") = "--:--:--" Then ro("ElapsedTime") = "00:00:00"
                            If ro("ElapsedTime")(ro("ElapsedTime").length - 1) = "-1" Then ro("ElapsedTime") = "00:00:00"

                            '----- PROGRESSION - CON
                            If ro("Status") = "CYCLE ON" Then
                                seconds = totalSeconds(ro("CurrentCycle"))
                                If maxtime(machineId)("CYCLE ON") < seconds Then
                                    maxtime(machineId)("CYCLE ON") = seconds 'totalSeconds(ro("CurrentCycle")) ' ro("maxCONtime")
                                    ro("Progression") = 100
                                Else
                                    If maxtime(machineId)("CYCLE ON") = 0 Then
                                        ro("Progression") = 0
                                    Else
                                        ro("Progression") = seconds * 100 / maxtime(machineId)("CYCLE ON")
                                    End If
                                End If
                            Else
                                ro("Progression") = 0
                            End If

                            errorPos = 8

                            '-------- PROGRESSION SETUP
                            If ro("Status") = "SETUP" Then
                                seconds = totalSeconds(ro("ElapsedTime"))
                                If maxtime(machineId)("SETUP") < seconds Then
                                    ro("Progression") = 100
                                    maxtime(machineId)("SETUP") = seconds
                                Else
                                    If maxtime(machineId)("SETUP") = 0 Then
                                        ro("Progression") = 0
                                    Else
                                        ro("Progression") = seconds * 100 / maxtime(machineId)("SETUP")
                                    End If
                                End If
                            Else
                                ro("Progression") = 0
                            End If
                            '  End If

                            errorPos = 9

                            Dim mch = AllMachines.FirstOrDefault(Function(v) v.Value.MachineName_ = machineId)
                            If Not String.IsNullOrEmpty(mch.Key) Then
                                ro("Label") = mch.Value.MachineLabel_
                            End If

                            ro("OperatorName") = ""

                            If Not String.IsNullOrEmpty(ro("OperatorRefId")) Then

                                Dim oper = dtUser.AsEnumerable.Where(Function(u) u.Item("RefId").ToString() = ro("OperatorRefId")).FirstOrDefault()

                                If oper IsNot Nothing Then ro("OperatorName") = oper("displayname").ToString()
                            End If
                            errorPos = 10

                        End If
                    End If

                    errorPos = 11

                    If csi_lib.MCHS_.ContainsKey(machineId) Then

                        If csi_lib.MCHS_(machineId).current_other IsNot Nothing Then

                            Dim current_mch = csi_lib.MCHS_(machineId)

                            Dim enetStatus = eNetServer.Instance.GetMachinesStatus().FirstOrDefault(Function(m) m.MachineName = current_mch.MachineName)

                            If enetStatus.FeedOverride > -1 Then current_mch.FeedRate_Value = enetStatus.FeedOverride
                            If enetStatus.SpindleOverride > -1 Then current_mch.Spindle_Value = enetStatus.SpindleOverride

                            If String.IsNullOrEmpty(current_mch.PartNumber_Value) And Not String.IsNullOrEmpty(enetStatus.PartNumber) Then
                                current_mch.PartNumber_Value = enetStatus.PartNumber
                            End If

                            Dim newRec As New StringBuilder()
                            newRec.Append($"Status:{current_mch.current},")
                            newRec.Append($"SystemStatus:{enetStatus.Status},")
                            newRec.Append($"Shift:{enetStatus.Shift},")
                            newRec.Append($"PartNumber:{current_mch.PartNumber_Value},")
                            newRec.Append($"Operation:{current_mch.Operation_Value},")
                            newRec.Append($"OperatorRefId:{current_mch.Operator_RefId},")
                            newRec.Append($"ProgNumber:{current_mch.progno_val},")
                            newRec.Append($"PartCount:{current_mch.PartCount_Value},")
                            newRec.Append($"PartRequired:{current_mch.RequiredParts_Value},")
                            newRec.Append($"FeedOverride:{current_mch.FeedRate_Value},")
                            newRec.Append($"RapidOverride:{current_mch.Rapid_Value},")
                            newRec.Append($"SpindleOverride:{current_mch.Spindle_Value}")

                            If current_mch.LastRecord <> newRec.ToString() Then

                                Log.Info($"Machine: {machineId} - newRec: {newRec.ToString()}")

                                If Not String.IsNullOrEmpty(current_mch.PartNumber_Value) Then
                                    ro("PartNumber") = current_mch.PartNumber_Value
                                End If

                                errorPos = 12

                                Log.Debug($"*******>> Machine: {machineId}, Feedrate Override: {current_mch.FeedRate_Value}")
                                If String.IsNullOrEmpty(current_mch.FeedRate_Variable) Then
                                    ro("FeedOverride") = current_mch.FeedRate_Value
                                    UpdateFeedrateoverDB(current_mch.ConnectorId, current_mch.FeedRate_Value)  ' we need it for Mobile Application to show 
                                Else
                                    ro("FeedOverride") = Nothing
                                    UpdateFeedrateoverDB(current_mch.ConnectorId, "")  ' we need it for Mobile Application to show 
                                End If

                                errorPos = 13

                                Log.Debug($"*******>> Machine: {machineId}, Spindle Override: {current_mch.Spindle_Value}")
                                If String.IsNullOrEmpty(current_mch.Spindle_Variable) Then
                                    ro("SpindleOverride") = current_mch.Spindle_Value
                                    UpdateSpindleoverDB(current_mch.ConnectorId, current_mch.Spindle_Value) ' we need it for Mobile Application to show 
                                Else
                                    ro("SpindleOverride") = Nothing
                                    UpdateSpindleoverDB(current_mch.ConnectorId, "") ' we need it for Mobile Application to show 
                                End If

                                errorPos = 14

                                Log.Debug($"*******>> Machine: {machineId}, Rapid Override: {current_mch.Rapid_Value} - { current_mch.ConnectorType }")
                                If String.IsNullOrEmpty(current_mch.Rapid_Variable) Then

                                    rapidOverrideValue = current_mch.Rapid_Value

                                    ro("RapidOverride") = rapidOverrideValue
                                    UpdateRapidoverDB(current_mch.ConnectorId, rapidOverrideValue) ' we need it for Mobile Application to show 
                                Else
                                    ro("RapidOverride") = Nothing
                                    UpdateRapidoverDB(current_mch.ConnectorId, "") ' we need it for Mobile Application to show 
                                End If

                                errorPos = 15

                                If Not String.IsNullOrEmpty(current_mch.PartCount_Variable) Then
                                    ro("PartCount") = current_mch.PartCount_Value
                                    UpdatePartCountDB(current_mch.ConnectorId, current_mch.PartCount_Value)  ' we need it for Mobile Application to show 
                                End If

                                errorPos = 16

                                If Not String.IsNullOrEmpty(current_mch.RequiredParts_Variable) Then
                                    ro("PartsRequired") = current_mch.RequiredParts_Value
                                    UpdatePartRequiredDB(current_mch.ConnectorId, current_mch.RequiredParts_Value)  ' we need it for Mobile Application to show 
                                End If

                                Log.Debug($"*******>> Machine: {machineId}, FOvr: {ro("FeedOverride")}, SOvr: {ro("SpindleOverride")}, ROvr: {ro("RapidOverride")}")
                                errorPos = 17

                                If current_mch.Min_Fover IsNot Nothing Then
                                    ro("MIN_Fover") = current_mch.Min_Fover
                                End If
                                If current_mch.Max_Fover IsNot Nothing Then
                                    ro("MAX_Fover") = current_mch.Max_Fover
                                End If
                                If current_mch.Min_Sover IsNot Nothing Then
                                    ro("MIN_Sover") = current_mch.Min_Sover
                                End If
                                If current_mch.Max_Sover IsNot Nothing Then
                                    ro("MAX_Sover") = current_mch.Max_Sover
                                End If
                                If current_mch.Min_Rover IsNot Nothing Then
                                    ro("MIN_Rover") = current_mch.Min_Rover
                                End If
                                If current_mch.Max_Rover IsNot Nothing Then
                                    ro("MAX_Rover") = current_mch.Max_Rover
                                End If

                                ro("PartNumber") = current_mch.PartNumber_Value
                                ro("OperatorRefId") = current_mch.Operator_RefId

                                errorPos = 18

                                Try

                                    Dim sqlCmd As New StringBuilder()
                                    sqlCmd.Append($"INSERT INTO csi_database.tbl_machinestate ")
                                    sqlCmd.Append($"(                                     ")
                                    sqlCmd.Append($"    MachineId       ,                 ")
                                    sqlCmd.Append($"    EventDateTime   ,                 ")
                                    sqlCmd.Append($"    Shift           ,                 ")
                                    sqlCmd.Append($"    Status          ,                 ")
                                    sqlCmd.Append($"    SystemStatus    ,                 ")
                                    sqlCmd.Append($"    Comment         ,                 ")
                                    sqlCmd.Append($"    PartNumber      ,                 ")
                                    sqlCmd.Append($"    ProgramNumber   ,                 ")
                                    sqlCmd.Append($"    Operation       ,                 ")
                                    sqlCmd.Append($"    OperatorRefId   ,                 ")
                                    sqlCmd.Append($"    PartCount       ,                 ")
                                    sqlCmd.Append($"    PartRequired    ,                 ")
                                    sqlCmd.Append($"    TotalPartCount  ,                 ")
                                    sqlCmd.Append($"    FeedOverride    ,                 ")
                                    sqlCmd.Append($"    RapidOverride   ,                 ")
                                    sqlCmd.Append($"    SpindleOverride                   ")
                                    sqlCmd.Append($") VALUES (                            ")
                                    sqlCmd.Append($"    @MachineId       ,                ")
                                    sqlCmd.Append($"    @EventDateTime   ,                ")
                                    sqlCmd.Append($"    @Shift           ,                ")
                                    sqlCmd.Append($"    @Status          ,                ")
                                    sqlCmd.Append($"    @SystemStatus    ,                ")
                                    sqlCmd.Append($"    @Comment         ,                ")
                                    sqlCmd.Append($"    @PartNumber      ,                ")
                                    sqlCmd.Append($"    @ProgramNumber   ,                ")
                                    sqlCmd.Append($"    @Operation       ,                ")
                                    sqlCmd.Append($"    @OperatorRefId   ,                ")
                                    sqlCmd.Append($"    @PartCount       ,                ")
                                    sqlCmd.Append($"    @PartRequired    ,                ")
                                    sqlCmd.Append($"    @TotalPartCount  ,                ")
                                    sqlCmd.Append($"    @FeedOverride    ,                ")
                                    sqlCmd.Append($"    @RapidOverride   ,                ")
                                    sqlCmd.Append($"    @SpindleOverride                  ")
                                    sqlCmd.Append($");                                    ")

                                    Dim sqlCommand As New MySqlCommand(sqlCmd.ToString())
                                    sqlCommand.Parameters.AddWithValue("@MachineId", machineId)
                                    sqlCommand.Parameters.AddWithValue("@EventDateTime", current_mch.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"))
                                    sqlCommand.Parameters.AddWithValue("@Shift", enetStatus.Shift)
                                    sqlCommand.Parameters.AddWithValue("@Status", current_mch.current)
                                    sqlCommand.Parameters.AddWithValue("@SystemStatus", enetStatus.Status)
                                    sqlCommand.Parameters.AddWithValue("@Comment", enetStatus.Comment)
                                    sqlCommand.Parameters.AddWithValue("@PartNumber", current_mch.PartNumber_Value)
                                    sqlCommand.Parameters.AddWithValue("@ProgramNumber", current_mch.progno_val)
                                    sqlCommand.Parameters.AddWithValue("@Operation", current_mch.Operation_Value)
                                    sqlCommand.Parameters.AddWithValue("@OperatorRefId", current_mch.Operation_Value)
                                    sqlCommand.Parameters.AddWithValue("@PartCount", IIf(String.IsNullOrEmpty(current_mch.PartCount_Value), 0, current_mch.PartCount_Value))
                                    sqlCommand.Parameters.AddWithValue("@PartRequired", IIf(String.IsNullOrEmpty(current_mch.RequiredParts_Value), 0, current_mch.RequiredParts_Value))
                                    sqlCommand.Parameters.AddWithValue("@TotalPartCount", 0)
                                    sqlCommand.Parameters.AddWithValue("@FeedOverride", IIf(String.IsNullOrEmpty(current_mch.FeedRate_Value), Nothing, current_mch.FeedRate_Value))
                                    sqlCommand.Parameters.AddWithValue("@RapidOverride", IIf(String.IsNullOrEmpty(current_mch.Rapid_Value), Nothing, current_mch.Rapid_Value))
                                    sqlCommand.Parameters.AddWithValue("@SpindleOverride", IIf(String.IsNullOrEmpty(current_mch.Spindle_Value), Nothing, current_mch.Spindle_Value))

                                    MySqlAccess.ExecuteNonQuery(sqlCommand)

                                    current_mch.LastRecord = newRec.ToString()

                                Catch ex As Exception
                                    Log.Error($"Database Update Machine State Problem in ServiceLibrary. {current_mch.MachineName}, {newRec.ToString()}", ex)
                                End Try

                            End If

                            Try
                                If Not current_mch.current.ToUpper().Contains("EMONIT") And current_mch.SaveDataRaw Then

                                    Const PRODUCTION = "CYCLE ON;CYCLE OFF"

                                    If Not current_mch.SaveDataRawProdOnly Or PRODUCTION.Contains(enetStatus.Status.ToUpper()) Or (current_mch.SaveDataRawSetup And enetStatus.Status.ToUpper() = "SETUP") Then

                                        'Dim json As String = JsonConvert.SerializeObject(current_mch.entity_client.CurrentXml)

                                        Dim sqlCmd As New StringBuilder()
                                        sqlCmd.Append($"INSERT INTO csi_database.tbl_fulldata ")
                                        sqlCmd.Append($"(                                     ")
                                        sqlCmd.Append($"    MachineId       ,                 ")
                                        sqlCmd.Append($"    EventDateTime   ,                 ")
                                        sqlCmd.Append($"    Status          ,                 ")
                                        sqlCmd.Append($"    ENETStatus      ,                 ")
                                        sqlCmd.Append($"    FullData                          ")
                                        sqlCmd.Append($") VALUES (                            ")
                                        sqlCmd.Append($"    @MachineId       ,                ")
                                        sqlCmd.Append($"    @EventDateTime   ,                ")
                                        sqlCmd.Append($"    @Status          ,                ")
                                        sqlCmd.Append($"    @ENETStatus      ,                ")
                                        sqlCmd.Append($"    @FullData                         ")
                                        sqlCmd.Append($");                                    ")

                                        Dim sqlCommand As New MySqlCommand(sqlCmd.ToString())
                                        sqlCommand.Parameters.AddWithValue("@MachineId", machineId)
                                        sqlCommand.Parameters.AddWithValue("@EventDateTime", current_mch.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"))
                                        sqlCommand.Parameters.AddWithValue("@Status", current_mch.current)
                                        sqlCommand.Parameters.AddWithValue("@ENETStatus", enetStatus)
                                        sqlCommand.Parameters.AddWithValue("@FullData", current_mch.entity_client.CurrentXml)

                                        MySqlAccess.ExecuteNonQuery(sqlCommand)
                                    End If

                                    current_mch.SavedFirstNoEmonitoring = False

                                ElseIf Not current_mch.SaveDataRawProdOnly And current_mch.current.ToUpper().Contains("EMONIT") And Not current_mch.SavedFirstNoEmonitoring Then

                                    Dim sqlCmd As New StringBuilder()
                                    sqlCmd.Append($"INSERT INTO csi_database.tbl_fulldata ")
                                    sqlCmd.Append($"(                                     ")
                                    sqlCmd.Append($"    MachineId       ,                 ")
                                    sqlCmd.Append($"    EventDateTime   ,                 ")
                                    sqlCmd.Append($"    ENETStatus      ,                 ")
                                    sqlCmd.Append($"    Status                            ")
                                    sqlCmd.Append($") VALUES (                            ")
                                    sqlCmd.Append($"    { machineId }   ,                 ")
                                    sqlCmd.Append($"   '{ current_mch.Timestamp.ToString("yyyy-MM-dd HH:mm:ss") }', ")
                                    sqlCmd.Append($"   '{ enetStatus }' ,                 ")
                                    sqlCmd.Append($"   '{ current_mch.current }'          ")
                                    sqlCmd.Append($");                                    ")
                                    MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                    current_mch.SavedFirstNoEmonitoring = True
                                End If

                            Catch ex As Exception
                                Log.Error($"Database Update Full Data Problem in ServiceLibrary. {current_mch.MachineName}", ex)
                            End Try

                        End If
                    End If
                Catch ex As Exception
                    Log.Error($"Error: Machine: {machineId}, Pos: {errorPos}", ex)
                End Try
            Next
        Catch ex As Exception
            Log.Error(ex)
        End Try
    End Sub

    Private Function totalSeconds(time As String) As Double
        Dim tmp_time() As String = time.Split(":")

        Return CInt(tmp_time(0)) * 3600 + CInt(tmp_time(1)) * 60 + CInt(tmp_time(2))

    End Function

    Private Sub Send_response(context As HttpListenerContext, data As String)
        Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(data)
        context.Response.ContentLength64 = buffer.Length
        context.Response.AppendHeader("Access-Control-Allow-Origin", "*")
        Dim output As System.IO.Stream
        output = context.Response.OutputStream
        output.Write(buffer, 0, buffer.Length)
        Thread.Sleep(100)
        output.Close()
        output.Dispose()

    End Sub

    Private Sub send_timeline(context As HttpListenerContext)

        Try
            Dim realtime_dic_BUFFER As New Dictionary(Of String, DataTable) '= realtime_dic

            Dim requesterIP = Convert.ToString(context.Request.RemoteEndPoint.Address)

            If Not machine_list.ContainsKey(requesterIP) Then
                requesterIP = requesterIP.Substring(0, requesterIP.LastIndexOf(".")) + ".*"
            End If

            For Each key In realtime_dic.Keys

                If machine_list.ContainsKey(requesterIP) Then

                    Dim machines As String = Convert.ToString(machine_list(requesterIP).Rows(0).Item("machines"))

                    If machines.Contains(key) Then
                        realtime_dic_BUFFER.Add(key, realtime_dic(key))
                    End If
                End If
            Next

            Dim ret = Convet_Dictionary_To_JSONW(realtime_dic_BUFFER)

            Send_response(context, ret)

        Catch ex As Exception
            csi_lib.LogServiceError("Err while sending the timeline (websrv): " & ex.Message + " , " + vbCrLf + "___" + ex.StackTrace, 1)
        End Try

    End Sub

    Public ARP_LOOKUP As Dictionary(Of String, String)

    Private Function AddGroups(PERF_TBL As DataTable, T_W As Dictionary(Of String, String), T_M As Dictionary(Of String, String)) As DataTable ', T_D As Dictionary(Of String, String)

        Dim CompletePERF_TBL As DataTable = New DataTable
        CompletePERF_TBL.Columns.Add("machinename_")
        CompletePERF_TBL.Columns.Add("weekly_")
        CompletePERF_TBL.Columns.Add("monthly_")

        For Each Group In groups_list("groups").Keys

            Dim r As DataRow = CompletePERF_TBL.NewRow()
            r("machinename_") = "Grp_" & Group
            r("weekly_") = "[{""status"":""CYCLE ON"",""cycletime"":" + Convert.ToString(WeeklyGroupPerf("Grp_" & Group)("CYCLE ON")).Replace(",", ".") + "},{""status"":""CYCLE OFF"",""cycletime"":" + Convert.ToString(WeeklyGroupPerf("Grp_" & Group)("CYCLE OFF")).Replace(",", ".") + "},{""status"":""SETUP"",""cycletime"":" + Convert.ToString(WeeklyGroupPerf("Grp_" & Group)("SETUP")).Replace(",", ".") + "},{""status"":""OTHER"",""cycletime"":" + Convert.ToString(WeeklyGroupPerf("Grp_" & Group)("OTHER")).Replace(",", ".") + "}]"
            r("monthly_") = "[{""status"":""CYCLE ON"",""cycletime"":" + Convert.ToString(MonthlyGroupPerf("Grp_" & Group)("CYCLE ON")).Replace(",", ".") + "},{""status"":""CYCLE OFF"",""cycletime"":" + Convert.ToString(MonthlyGroupPerf("Grp_" & Group)("CYCLE OFF")).Replace(",", ".") + "},{""status"":""SETUP"",""cycletime"":" + Convert.ToString(MonthlyGroupPerf("Grp_" & Group)("SETUP")).Replace(",", ".") + "},{""status"":""OTHER"",""cycletime"":" + Convert.ToString(MonthlyGroupPerf("Grp_" & Group)("OTHER")).Replace(",", ".") + "}]"
            CompletePERF_TBL.Rows.Add(r)

            Dim r2 As DataRow = CompletePERF_TBL.NewRow()
            r2("machinename_") = "Sec_Grp_" & Group
            r2("weekly_") = "[{""status"":""CYCLE ON"",""cycletime"":" + Convert.ToString(WeeklyPerf_Seconds("Sec_Grp_" & Group)("CYCLE ON")).Replace(",", ".") + "},{""status"":""CYCLE OFF"",""cycletime"":" + Convert.ToString(WeeklyPerf_Seconds("Sec_Grp_" & Group)("CYCLE OFF")).Replace(",", ".") + "},{""status"":""SETUP"",""cycletime"":" + Convert.ToString(WeeklyPerf_Seconds("Sec_Grp_" & Group)("SETUP")).Replace(",", ".") + "},{""status"":""OTHER"",""cycletime"":" + Convert.ToString(WeeklyPerf_Seconds("Sec_Grp_" & Group)("OTHER")).Replace(",", ".") + "}]"
            r2("monthly_") = "[{""status"":""CYCLE ON"",""cycletime"":" + Convert.ToString(MonthlyPerf_Seconds("Sec_Grp_" & Group)("CYCLE ON")).Replace(",", ".") + "},{""status"":""CYCLE OFF"",""cycletime"":" + Convert.ToString(MonthlyPerf_Seconds("Sec_Grp_" & Group)("CYCLE OFF")).Replace(",", ".") + "},{""status"":""SETUP"",""cycletime"":" + Convert.ToString(MonthlyPerf_Seconds("Sec_Grp_" & Group)("SETUP")).Replace(",", ".") + "},{""status"":""OTHER"",""cycletime"":" + Convert.ToString(MonthlyPerf_Seconds("Sec_Grp_" & Group)("OTHER")).Replace(",", ".") + "}]"
            CompletePERF_TBL.Rows.Add(r2)


            Dim r3 As DataRow = CompletePERF_TBL.NewRow()
            r3("machinename_") = "TitleTarget_Grp_" & Group
            Try
                If T_W(Group) Is Nothing Then
                Else
                    r3("weekly_") = T_W(Group)
                End If
            Catch ex As Exception

            End Try
            Try
                If T_M(Group) Is Nothing Then
                Else
                    r3("monthly_") = T_M(Group)
                End If
            Catch ex As Exception

            End Try

            CompletePERF_TBL.Rows.Add(r3)
        Next

        Return CompletePERF_TBL

    End Function

    Private Function DropGroups(Perf As DataTable)
        Dim i As Integer = Perf.Rows.Count - 1
        While (i >= 0)
            Dim r As DataRow = Perf(i)
            If Convert.ToString(r("machinename_")).StartsWith("Grp_") Then
                r.Delete()
            End If
            i = i - 1
        End While
        Return Perf
    End Function

    Private Function DropMachines(Perf As DataTable)

        Dim i As Integer = Perf.Rows.Count - 1

        While (i >= 0)
            Dim r As DataRow = Perf(i)

            If Not Convert.ToString(r("machinename_")).StartsWith("Grp_") And Not Convert.ToString(r("machinename_")).StartsWith("Sec_Grp") And Not Convert.ToString(r("machinename_")).StartsWith("TitleTarget_Grp_") Then
                r.Delete()
            End If
            i = i - 1
        End While

        Return Perf

    End Function

    Private Sub send_config(context As HttpListenerContext)

        Dim JSON As String = ""
        Dim IP___ As String = Convert.ToString(context.Request.RemoteEndPoint.Address)
        If IP___ = "::1" Then IP___ = "127.0.0.1"
        Dim registred As Boolean = False

        Try

            Dim PieChartBy As String = ""
            Dim PieChartDisplayWhat As String = ""

            Dim requesterIP As String = IP___

            Log.Debug($"Dashboard - Requester for end user config: { requesterIP }")

            If Not device_config.ContainsKey(requesterIP) Then
                requesterIP = IP___.Substring(0, IP___.LastIndexOf(".")) + ".*"
            End If

            If PieChartSetting_Dic.ContainsKey(requesterIP) Then PieChartBy = PieChartSetting_Dic(requesterIP)

            If PieChartSetting_TargetDic.ContainsKey(requesterIP) Then PieChartDisplayWhat = PieChartSetting_TargetDic(requesterIP)


            Dim TMP__DATASET_to_be_sent As New DataSet("Refreshed_data")

            If PieChartDisplayWhat = "DisplayPerf" Then

                If PieChartBy = "ByMachines" Then
                    TMP__DATASET_to_be_sent.Tables.Add(DropGroups(PERF_TBL.Copy))
                ElseIf PieChartBy = "ByGroups" Then
                    TMP__DATASET_to_be_sent.Tables.Add(DropMachines(PERF_TBL.Copy))
                Else
                    TMP__DATASET_to_be_sent.Tables.Add(PERF_TBL.Copy)
                End If

            ElseIf PieChartDisplayWhat = "DisplayTarget" Then

                If PieChartBy = "ByMachines" Then
                    TMP__DATASET_to_be_sent.Tables.Add(DropGroups(PERF_TBL.Copy))
                ElseIf PieChartBy = "ByGroups" Then
                    TMP__DATASET_to_be_sent.Tables.Add(DropMachines(PERF_TBL.Copy))
                Else
                    TMP__DATASET_to_be_sent.Tables.Add(PERF_TBL.Copy)
                End If

            ElseIf PieChartDisplayWhat = "none" Then

                If PieChartBy = "ByMachines" Then
                    TMP__DATASET_to_be_sent.Tables.Add(DropGroups(PERF_TBL.Copy))
                ElseIf PieChartBy = "ByGroups" Then
                    TMP__DATASET_to_be_sent.Tables.Add(DropMachines(PERF_TBL.Copy))
                Else
                    TMP__DATASET_to_be_sent.Tables.Add(PERF_TBL.Copy)
                End If

            ElseIf PieChartDisplayWhat = "Both" Then

                If PieChartBy = "ByMachines" Then
                    TMP__DATASET_to_be_sent.Tables.Add(DropGroups(PERF_TBL.Copy))
                ElseIf PieChartBy = "ByGroups" Then
                    TMP__DATASET_to_be_sent.Tables.Add(DropMachines(PERF_TBL.Copy))
                Else
                    TMP__DATASET_to_be_sent.Tables.Add(PERF_TBL.Copy)
                End If

            End If

            TMP__DATASET_to_be_sent.AcceptChanges()

            TMP__DATASET_to_be_sent.Tables.Add(tbl_COLORS.Copy)

            Dim THE_MAC As String = ""

            'RELOAD CONFIG AND CHECK MAC WITH ARP CMD
            read_dashboard_config_fromdb()

            If device_config.ContainsKey(requesterIP) Then

                TMP__DATASET_to_be_sent.Tables.Add(device_config(requesterIP).Copy)
                registred = True

            Else

                If device_config.ContainsKey(requesterIP) Then
                    TMP__DATASET_to_be_sent.Tables.Add(device_config(requesterIP).Copy)
                    registred = True
                Else
                    TMP__DATASET_to_be_sent.Tables.Add("config")
                End If

            End If

            If machine_list.ContainsKey(requesterIP) Then
                TMP__DATASET_to_be_sent.Tables.Add(machine_list(requesterIP).Copy)
            Else
                If Not THE_MAC Is Nothing Then
                    If machine_list.ContainsKey(THE_MAC) Then
                        TMP__DATASET_to_be_sent.Tables.Add(machine_list(THE_MAC).Copy)
                    Else
                        TMP__DATASET_to_be_sent.Tables.Add("tbl_device_machines_groups")
                    End If
                Else
                    TMP__DATASET_to_be_sent.Tables.Add("tbl_device_machines_groups")
                End If

            End If

            If message_list.ContainsKey(requesterIP) Then

                TMP__DATASET_to_be_sent.Tables.Add(message_list(requesterIP).Copy)
            Else
                If Not THE_MAC Is Nothing Then
                    If message_list.ContainsKey(THE_MAC) Then
                        TMP__DATASET_to_be_sent.Tables.Add(message_list(THE_MAC).Copy)
                    Else
                        TMP__DATASET_to_be_sent.Tables.Add("tbl_messages")
                    End If
                Else
                    TMP__DATASET_to_be_sent.Tables.Add("tbl_messages")
                End If
            End If

            'SEND THE DATASET TO THE DASHBOARD ///////////////////////////////////////////////////////////////////

            If TMP__DATASET_to_be_sent IsNot Nothing Then
                JSON = Convet_DataTable_To_JSONW(TMP__DATASET_to_be_sent)
            End If

            If groups_list.Count <> 0 Then
                JSON = JSON.Remove(JSON.Length - 1)
                JSON = JSON + "," + Convert.ToString(JsonConvert.SerializeObject(groups_list)).Remove(0, 1)
            End If

            JSON = JSON.Remove(JSON.Length - 1)

            JSON = JSON + $",""serverTime"":""{Now.ToString()}"""

            If registred = False Then
                JSON = JSON + ",""Additional_infos"":[{""Error"":""" + "Not Registered user/device" + """}]}"
            ElseIf perf_computed___ = False Then
                JSON = JSON + ",""Additional_infos"":[{""Error"":""" + "STILL COMPUTING MON/WEEKLY PERF" + """}]}"
            Else
                JSON = JSON + ",""Additional_infos"":[{""Error"":""" + "None" + """}]}"
            End If

            Send_response(context, JSON)

            config_changed = False

            TMP__DATASET_to_be_sent.Dispose()

            'if LR1 
            If Convert.ToString(device_config(requesterIP).Rows.Item(0).Item("devicetype")) = "Computer" Then SendDateTime(requesterIP)

        Catch ex As Exception
            Log.Error(ex)
        End Try

        context.Response.OutputStream.Close()

    End Sub

    Dim List_of_conds As String = ""

    Private Sub SendDateTime(ip As String)
        Try

            'Using client = New SshClient(ip, "pi", "raspberry")
            '    client.Connect()
            '    Dim d As String = """" + Now.Date.Day.ToString() + " " + MonthName(Now.Date.Month.ToString()) + " " + Now.Date.Year.ToString + " " + Now.TimeOfDay.Hours.ToString() + ":" + Now.TimeOfDay.Minutes.ToString() + ":" + Now.TimeOfDay.Seconds.ToString() + """"
            '    Using cmd2 = client.CreateCommand("sudo date -s " + d)
            '        cmd2.Execute()
            '    End Using
            'End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Function VALIDATE_DATA(ByRef rt_datatable As DataTable, context As HttpListenerContext, Optional isMobile As Boolean = False) As DataTable

        Dim tmp_tabl As DataTable = rt_datatable.Clone()
        Dim idSort As Integer = 0

        Try
            If rt_datatable.Rows.Count <> 0 Or rt_datatable.Columns.Count <> 0 Then
                Dim keys(1) As DataColumn
                keys(0) = tmp_tabl.Columns(0)
                tmp_tabl.PrimaryKey = keys

                Dim machines

                Dim requesterIP = Convert.ToString(context.Request.RemoteEndPoint.Address)

                Dim Match = Regex.Match(requesterIP, "^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$")


                If Match.Success And Not machine_list.ContainsKey(requesterIP) Then
                    requesterIP = requesterIP.Substring(0, requesterIP.LastIndexOf(".")) + ".*"
                End If

                If machine_list.ContainsKey(requesterIP) Or isMobile Then

                    If isMobile Then

                        machines = eNetServer.MonitoredMachines.Select(Function(x) x.MachineId.ToString()).ToArray()

                    Else

                        machines = Convert.ToString(machine_list(requesterIP).Rows(0).Item("machines")).Split(",").Select(Function(s) s.Trim()).ToArray()

                        idSort = Integer.Parse(machine_list(requesterIP).Rows(0).Item("OrderBy"))

                    End If

                    For Each row As DataRow In rt_datatable.Rows

                        If Array.Exists(Of String)(machines, Function(m) m = row("MachineId") Or m = row("EnetMachine")) Then     'machines.Contains(row("MachineId")) Or machines.Contains(row("EnetMachine"))
                            Dim newrow = tmp_tabl.NewRow()
                            newrow.ItemArray = row.ItemArray.Clone()
                            tmp_tabl.Rows.Add(newrow)
                        End If

                        Dim mch = row("MachineId")
                        Dim pnr = row("PartNumber")
                    Next

                End If

                For Each mch As MCH_ In csi_lib.MCHS_.Values
                    If mch.IN_alarms = True Then
                        Dim i As Integer = tmp_tabl.Rows.IndexOf(tmp_tabl.Rows.Find(mch.MachineName))
                        If i > -1 Then
                            Dim Alarm_type As String = "Warning"
                            For Each item In mch.List_of_alarms
                                If item.StartsWith("Fault") Then
                                    Alarm_type = "Fault"
                                    Exit For
                                End If
                            Next
                            tmp_tabl.Rows.Item(i).Item(4) = Alarm_type 'select_worst_alarm(mch.List_of_alarms)
                            tmp_tabl.Rows.Item(i).Item("Alarm_details") = JsonConvert.SerializeObject(mch.List_of_alarms)
                        End If
                    End If
                Next

            End If
        Catch ex As Exception
            Log.Error($"Err while validating the requester IP Address ({ context.Request.RemoteEndPoint.Address })", ex)
        End Try

        Dim sort As String = "machineName ASC"

        Select Case idSort
            Case 1
                sort = "machineName DESC"
            Case 2
                sort = "label ASC"
            Case 3
                sort = "label DESC"
            Case 4
                sort = "EnetMachine ASC"
            Case 5
                sort = "EnetMachine DESC"
        End Select

        tmp_tabl.DefaultView.Sort = sort
        tmp_tabl = tmp_tabl.DefaultView.ToTable

        Return tmp_tabl

    End Function

    Dim tbl_COLORS As New DataTable("COLORS")
    Dim device_config As New Dictionary(Of String, DataTable)
    Dim machine_list As New Dictionary(Of String, DataTable)
    Dim message_list As New Dictionary(Of String, DataTable)
    Dim groups_list As New Dictionary(Of String, Dictionary(Of String, List(Of String))) 'As New Dictionary(Of String, List(Of String))
    Dim general_config As New Dictionary(Of String, String)

    Private Sub read_general_config()

        Try
            Dim line As String = ""

            Dim setting As New Dictionary(Of String, String)
            Dim rootpath As String = CSI_Library.CSI_Library.serverRootPath
            general_config.Clear()
            general_config.Add("prod", "CYCLE ON")

            If File.Exists(rootpath + "/sys/generaldashb/SETUP.csys") Then
                general_config("prod") = general_config("prod") + ",SETUP"
            End If

            If File.Exists(rootpath + "/sys/generaldashb/LOADING.csys") Then
                general_config("prod") = general_config("prod") + ",LOADING"
            End If

            If File.Exists(rootpath + "/sys/generaldashb/trends.csys") Then
                Dim options() As String
                Using fs As New StreamReader(rootpath + "/sys/generaldashb/" & "trends" & ".csys")
                    options = fs.ReadLine().Split(",")
                    If options.Count > 1 Then
                        general_config.Add("trends", "percent")
                        general_config("trends") = general_config("trends") + "," + options(1)

                        If options.Count = 3 Then
                            general_config("trends") = general_config("trends") + "," + options(2)
                        Else
                            general_config("trends") = general_config("trends") + "," + "The actual shift"
                        End If
                    Else
                        general_config.Add("trends", "prod")

                    End If
                    fs.Close()
                End Using
            End If

        Catch ex As Exception
            Log.Error("Err while retr general config from the disque files (websrv)", ex)
        End Try

    End Sub

    Dim DailyTargets As New Dictionary(Of String, Integer)
    Dim WeeklyTargets As New Dictionary(Of String, Integer)
    Dim MonthlyTargets As New Dictionary(Of String, Integer)
    Dim PieChartSetting_Dic As New Dictionary(Of String, String)
    Dim PieChartSetting_TargetDic As New Dictionary(Of String, String)

    Public Sub read_dashboard_config_fromdb()

        Dim tableS__ As New DataSet

        Try

            read_general_config() ' general_config is filled now

            device_config.Clear()
            device_config = New Dictionary(Of String, DataTable)

            config_table.Rows.Clear()

            config_table = MySqlAccess.GetDataTable("SELECT * FROM csi_database.tbl_deviceconfig")
            config_table.TableName = "config"

            Try
                PieChartSetting_Dic.Clear()
                PieChartSetting_TargetDic.Clear()

                DailyTargets = csi_lib.Load_DailyTargets()
                WeeklyTargets = csi_lib.Load_WeeklyTargets()
                MonthlyTargets = csi_lib.Load_MonthlyTargets()

                For Each row As DataRow In config_table.Rows

                    Dim tmp_tbl As DataTable = config_table.Clone

                    tmp_tbl.ImportRow(row)

                    Dim IP__ As String = ""
                    Dim Buffer As String() = row("IP").split("-")

                    If Buffer.Count > 1 Then
                        IP__ = convert_mac_to_ip(row("IP"))
                    Else
                        IP__ = row("IP")
                    End If

                    tmp_tbl.Rows(0).Item(1) = IP__

                    If Not device_config.ContainsKey(IP__) Then
                        device_config.Add(IP__, tmp_tbl)
                        device_config(IP__).Columns.Add("timeline")
                        device_config(IP__).Columns.Add("trends")
                        device_config(IP__).Columns.Add("dateformat")
                        device_config(IP__).Columns.Add("scale")
                        device_config(IP__).Columns.Add("devicetype")
                        device_config(IP__).Columns.Add("lsByGroup")
                        device_config(IP__).Columns.Add("Browserzoom")
                        device_config(IP__).Columns.Add("FeedrateOver")
                        device_config(IP__).Columns.Add("SpindleOver")
                        device_config(IP__).Columns.Add("RapidOver")
                        device_config(IP__).Columns.Add("PartNumberColumn")
                        device_config(IP__).Columns.Add("CountColumn")
                        device_config(IP__).Columns.Add("CountFormat")
                        device_config(IP__).Columns.Add("Operator")
                        device_config(IP__).Columns.Add("OperatorName")
                        device_config(IP__).Columns.Add("TimeLineBarHeight")
                        device_config(IP__).Columns.Add("MachineNameText")
                        device_config(IP__).Columns.Add("MachineNameWidth")
                        device_config(IP__).Columns.Add("RemoveLastRow")
                        device_config(IP__).Columns.Add("UseMachineLabel")
                        device_config(IP__).Columns.Add("OrderBy")
                        device_config(IP__).Columns.Add("DisplayPressure")
                    End If

                    If Not PieChartSetting_Dic.ContainsKey(IP__) Then
                        If config_table.Columns.Contains("PieChartBy") Then
                            PieChartSetting_Dic.Add(IP__, If(IsDBNull(row("PieChartBy")), "ByMachines", row("PieChartBy")))
                        Else
                            PieChartSetting_Dic.Add(IP__, "ByMachines")
                        End If
                    Else
                        csi_lib.LogServiceError($"read_dashboard_config_fromdb : ip {IP__} for {row("IP")}:{row("IP")} already exist in PieChartSetting_Dic", 1)
                    End If

                    If Not PieChartSetting_TargetDic.ContainsKey(IP__) Then
                        If config_table.Columns.Contains("DisplayWhat") Then
                            PieChartSetting_TargetDic.Add(IP__, If(IsDBNull(row("DisplayWhat")), "DisplayPerf", row("DisplayWhat")))
                        Else
                            PieChartSetting_TargetDic.Add(IP__, "DisplayPerf")
                        End If
                    End If

                Next
            Catch ex As Exception
                Log.Error("Err while retr device config from csi_database.tbl_deviceconfig (websrv)", ex)
            End Try

            Try
                Dim config_table2 As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_database.tbl_deviceconfig2")

                For Each row As DataRow In config_table2.Rows

                    Dim tmp_tbl As DataTable = config_table2.Clone
                    tmp_tbl.ImportRow(row)

                    Dim IP__ As String = ""
                    Dim Buffer As String() = row("IP_Adress").split("-")
                    If Buffer.Count > 1 Then
                        IP__ = convert_mac_to_ip(row("IP_Adress"))
                    Else
                        IP__ = row("IP_Adress")
                    End If
                    tmp_tbl.Rows(0).Item(1) = IP__

                    If device_config.ContainsKey(IP__) Then
                        device_config(IP__).Rows(0)("timeline") = If(IsDBNull(row("timeline")), False, row("timeline"))
                        device_config(IP__).Rows(0)("trends") = If(IsDBNull(row("trends")), False, row("trends"))
                        device_config(IP__).Rows(0)("dateformat") = If(IsDBNull(row("DateFormat")), False, row("DateFormat"))
                        device_config(IP__).Rows(0)("scale") = If(IsDBNull(row("scale")), False, row("scale"))
                        device_config(IP__).Rows(0)("devicetype") = If(IsDBNull(row("devicetype")), False, row("devicetype"))
                        device_config(IP__).Rows(0)("lsByGroup") = If(IsDBNull(row("DisplayByGroups")), 0, Convert.ToBoolean(row("DisplayByGroups")))
                        device_config(IP__).Rows(0)("Browserzoom") = If(IsDBNull(row("browserzoom")), False, row("browserzoom"))
                        device_config(IP__).Rows(0)("FeedrateOver") = If(IsDBNull(row("FeedrateOver")), False, row("FeedrateOver"))
                        device_config(IP__).Rows(0)("SpindleOver") = If(IsDBNull(row("SpindleOver")), False, row("SpindleOver"))
                        device_config(IP__).Rows(0)("RapidOver") = If(IsDBNull(row("RapidOver")), False, row("RapidOver"))
                        device_config(IP__).Rows(0)("PartNumberColumn") = If(IsDBNull(row("PartNumberColumn")), False, row("PartNumberColumn"))
                        device_config(IP__).Rows(0)("Operator") = If(IsDBNull(row("Operator")), False, row("Operator"))
                        device_config(IP__).Rows(0)("OperatorName") = If(IsDBNull(row("OperatorName")), False, row("OperatorName"))
                        device_config(IP__).Rows(0)("CountColumn") = If(IsDBNull(row("CountColumn")), False, row("CountColumn"))
                        device_config(IP__).Rows(0)("CountFormat") = If(IsDBNull(row("CountFormat")), "", row("CountFormat"))
                        device_config(IP__).Rows(0)("TimeLineBarHeight") = If(IsDBNull(row("TimeLineBarHeight")), 100, row("TimeLineBarHeight"))
                        device_config(IP__).Rows(0)("MachineNameText") = If(IsDBNull(row("MachineNameText")), 100, row("MachineNameText"))
                        device_config(IP__).Rows(0)("MachineNameWidth") = If(IsDBNull(row("MachineNameWidth")), 100, row("MachineNameWidth"))
                        device_config(IP__).Rows(0)("RemoveLastRow") = If(IsDBNull(row("RemoveLastRow")), False, row("RemoveLastRow"))
                        device_config(IP__).Rows(0)("UseMachineLabel") = If(IsDBNull(row("UseMachineLabel")), False, row("UseMachineLabel"))
                        device_config(IP__).Rows(0)("OrderBy") = If(IsDBNull(row("OrderBy")), 0, row("OrderBy"))
                        device_config(IP__).Rows(0)("DisplayPressure") = row("DisplayPressure")
                    End If
                Next
            Catch ex As Exception
                Log.Error("Err while retr deviceconfig2 from csi_database.tbl_deviceconfig2 (websrv)", ex)
            End Try

            Try
                Dim cmd As Text.StringBuilder = New StringBuilder()
                cmd.Append($"SELECT                         ")
                cmd.Append($"    D.Id        ,              ")
                cmd.Append($"    D.IP_adress ,              ")
                cmd.Append($"    D.machines  ,              ")
                cmd.Append($"    D.groups    ,              ")
                cmd.Append($"    C.OrderBy                  ")
                cmd.Append($"FROM                           ")
                cmd.Append($"    csi_database.tbl_devices D ")
                cmd.Append($"    INNER Join csi_database.tbl_deviceconfig2 C ON D.Id = C.deviceId")

                tbl_device_machines_groups.Rows.Clear()
                tbl_device_machines_groups = MySqlAccess.GetDataTable(cmd.ToString())
                tbl_device_machines_groups.TableName = "tbl_device_machines_groups"

                machine_list.Clear()

                For Each row As DataRow In tbl_device_machines_groups.Rows
                    Dim tmp_tbl As DataTable = tbl_device_machines_groups.Clone

                    tmp_tbl.ImportRow(row)

                    Dim IP__ As String = ""
                    Dim Buffer As String() = row("IP_Adress").split("-")
                    If Buffer.Count > 1 Then
                        IP__ = convert_mac_to_ip(row("IP_Adress"))
                    Else
                        IP__ = row("IP_Adress")
                    End If
                    tmp_tbl.Rows(0).Item("IP_Adress") = IP__


                    If Not machine_list.ContainsKey(IP__) Then
                        machine_list.Add(IP__, tmp_tbl)
                    Else
                        machine_list(IP__).ImportRow(row)
                    End If
                Next
            Catch ex As Exception
                Log.Error("Err while retr groups config from csi_database.tbl_deviceconfig (websrv)", ex)
            End Try

            Try
                tbl_messages.Clear()
                tbl_messages = MySqlAccess.GetDataTable("Select * FROM csi_database.tbl_messages order by Priority", CSI_Library.CSI_Library.MySqlConnectionString)

                message_list.Clear()

                For Each row As DataRow In tbl_messages.Rows

                    Dim deviceId As Integer = Integer.Parse(row("DeviceId"))

                    Dim tmp_tbl As DataTable = tbl_messages.Clone
                    tmp_tbl.ImportRow(row)

                    Dim IP__ As String = ""
                    Dim Buffer As String() = row("IP_Adress").split("-")
                    If Buffer.Count > 1 Then
                        IP__ = convert_mac_to_ip(row("IP_Adress"))
                    Else
                        IP__ = row("IP_Adress")
                    End If
                    tmp_tbl.Rows(0).Item("IP_Adress") = IP__

                    If Not message_list.ContainsKey(IP__) Then
                        message_list.Add(IP__, tmp_tbl)
                    Else
                        message_list(IP__).ImportRow(row)
                    End If
                Next
            Catch ex As Exception
                Log.Error("Err while retr msg config from csi_database.tbl_messages (websrv)", ex)
            End Try

            tbl_COLORS = New DataTable("COLORS")
            tbl_COLORS = MySqlAccess.GetDataTable("Select * FROM csi_database.tbl_colors")
            tbl_COLORS.TableName = "COLORS"

            tbl_COLORS.Rows.Add("DISCONNECTED", "#ABABAB")

            Dim CON_Row() As DataRow

            CON_Row = tbl_COLORS.Select("statut = 'CYCLE ON'")
            CON_Row(0)("color") = "#4db34d"

            CON_Row = tbl_COLORS.Select("statut = 'LOCKED'")
            If CON_Row.Count = 0 Then tbl_COLORS.Rows.Add("LOCKED", "#808080")

            CON_Row = tbl_COLORS.Select("statut = 'NOT CONNECTED'")
            If CON_Row.Count = 0 Then tbl_COLORS.Rows.Add("NOT CONNECTED", "#A9A9A9")

            CON_Row = tbl_COLORS.Select("statut = 'SERVER OFFLINE'")
            If CON_Row.Count = 0 Then tbl_COLORS.Rows.Add("SERVER OFFLINE", "#A9A9A9")



            groups_list.Clear()

            Dim tblGroups = MySqlAccess.GetDataTable("Select * FROM csi_database.tbl_groups")
            Dim dicGroups As New Dictionary(Of String, List(Of String))

            For Each groupRow As DataRow In tblGroups.Rows
                If dicGroups.ContainsKey(groupRow("groups")) Then
                    If Not groupRow("machineId") = "0" Then dicGroups(groupRow("groups")).Add(groupRow("machineId"))
                Else
                    dicGroups.Add(groupRow("groups"), New List(Of String))
                    dicGroups(groupRow("groups")).Add(groupRow("machineId"))
                End If
            Next

            groups_list.Add("groups", dicGroups)

        Catch ex As Exception
            Log.Error("Err while retr device config from database (websrv)", ex)
        End Try

    End Sub

    Public Sub UpdateRapidoverDB(ConnectorId As Integer, rapidoverValue As String)

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues ")
            sqlCmd.Append($"   SET                                            ")

            If (String.IsNullOrEmpty(rapidoverValue)) Then
                sqlCmd.Append($"      Rapid_Value = NULL                      ")
            Else
                sqlCmd.Append($"      Rapid_Value = '{ rapidoverValue }'      ")
            End If

            sqlCmd.Append($"   WHERE                                          ")
            sqlCmd.Append($"      ConnectorId     =  { ConnectorId }          ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            Log.Error("Database Update Rapid Override Problem in ServiceLibrary", ex)
        End Try
    End Sub

    Public Sub UpdateSpindleoverDB(ConnectorId As Integer, spindleoverValue As String)

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues ")
            sqlCmd.Append($"   SET                                            ")

            If (String.IsNullOrEmpty(spindleoverValue)) Then
                sqlCmd.Append($"      Spindle_Value = NULL                    ")
            Else
                sqlCmd.Append($"      Spindle_Value = '{ spindleoverValue }'  ")
            End If

            sqlCmd.Append($"   WHERE                                          ")
            sqlCmd.Append($"      ConnectorId       =  { ConnectorId }        ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            Log.Error("Database Update Spindle Override Problem in ServiceLibrary.", ex)
        End Try

    End Sub

    Public Sub UpdateFeedrateoverDB(ConnectorId As Integer, feedrateoverValue As String)

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues  ")
            sqlCmd.Append($"   SET                                             ")

            If (String.IsNullOrEmpty(feedrateoverValue)) Then
                sqlCmd.Append($"      FeedRate_Value = NULL                    ")
            Else
                sqlCmd.Append($"      FeedRate_Value = '{ feedrateoverValue }' ")
            End If

            sqlCmd.Append($"   WHERE                                           ")
            sqlCmd.Append($"      ConnectorId        =  { ConnectorId }        ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            Log.Error("Database Update Feedrate Override Problem in ServiceLibrary", ex)
        End Try

    End Sub

    Public Sub UpdatePartCountDB(ConnectorId As Integer, partCountValue As String)

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues ")
            sqlCmd.Append($"   SET                                            ")
            sqlCmd.Append($"      PartCount_Value = '{ partCountValue }'      ")
            sqlCmd.Append($"   WHERE                                          ")
            sqlCmd.Append($"      ConnectorId       =  { ConnectorId }        ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            Log.Error("Database Update Part Count Problem in ServiceLibrary", ex)
        End Try

    End Sub

    Public Sub UpdatePartRequiredDB(ConnectorId As Integer, partReqValue As String)

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues ")
            sqlCmd.Append($"   SET                                            ")
            sqlCmd.Append($"      PartRequired_Value = '{ partReqValue }'     ")
            sqlCmd.Append($"   WHERE                                          ")
            sqlCmd.Append($"      ConnectorId    =  { ConnectorId }           ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            Log.Error("Database Update Parts required Problem in ServiceLibrary", ex)
        End Try

    End Sub

    Private Function convert_mac_to_ip(mac As String) As String

        Dim startInfo As New ProcessStartInfo()
        startInfo.CreateNoWindow = True
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardOutput = True
        startInfo.FileName = "arp"
        startInfo.Arguments = "-a"

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

        Return "0.0.0.0" 'error

    End Function

    Public Function compute_perf_from_db2() As DataTable

        Dim tablePerformance As TablePerformance = New TablePerformance()

        tablePerformance.CalcPerformance()

        PERF_TBL = MySqlAccess.GetDataTable("SELECT * FROM csi_machineperf.tbl_perf;")
        PERF_TBL.TableName = "perf"

    End Function

    Dim PERF_TBL As New DataTable("perf")
    Dim PERF_Group_TBL As New DataTable("perf_group")

    Dim PERF_GroupTarget_TBL As New DataTable("perf_group_target")
    Dim PERF_target_TBL As New DataTable("perf_target")

    Public WeeklyPerf_Seconds As New Dictionary(Of String, Dictionary(Of String, Double)) '[machine/group, [status,cycletime]]
    Public MonthlyPerf_Seconds As New Dictionary(Of String, Dictionary(Of String, Double)) '[machine/group, [status,cycletime]]
    Public DailyPerf_Seconds As New Dictionary(Of String, Dictionary(Of String, Double)) '[machine/group, [status,cycletime]]

    Public MonthlyGroupPerf As New Dictionary(Of String, Dictionary(Of String, Double)) '[machine/group, [status,cycletime]]
    Public WeeklyGroupPerf As New Dictionary(Of String, Dictionary(Of String, Double)) '[machine/group, [status,cycletime]]
    Public DailyGroupPerf As New Dictionary(Of String, Dictionary(Of String, Double)) '[machine/group, [status,cycletime]]

    Public Function compute_perf_from_db() As DataTable 'type = month, week, day ----- merge = true : CON/COFF/SETP, false : CON/COFF/SETUP/OTHER

        perf_computed___ = False

        If groups_list.Count = 0 Then
            read_dashboard_config_fromdb()
        End If

        Try

            MonthlyPerf_Seconds.Clear()
            WeeklyPerf_Seconds.Clear()

            Dim cmmd As String = ""

            MySqlAccess.ExecuteNonQuery("TRUNCATE csi_machineperf.tbl_perf")

            Dim dset_perf_weekly As New DataSet("PERF_weekly")
            Dim dset_perf_monthly As New DataSet("PERF_monthly")

            PERF_TBL = New DataTable("perf")
            PERF_TBL.Columns.Add("machinename_")
            PERF_TBL.Columns.Add("weekly_")
            PERF_TBL.Columns.Add("monthly_")

            MonthlyGroupPerf = New Dictionary(Of String, Dictionary(Of String, Double)) '[machine/group, [status,cycletime]]
            WeeklyGroupPerf = New Dictionary(Of String, Dictionary(Of String, Double)) '[machine/group, [status,cycletime]]6 

            For Each Group In groups_list("groups").Keys

                MonthlyGroupPerf.Add("Grp_" & Group, New Dictionary(Of String, Double))
                MonthlyGroupPerf("Grp_" & Group).Add("OTHER", 0)
                MonthlyGroupPerf("Grp_" & Group).Add("CYCLE ON", 0)
                MonthlyGroupPerf("Grp_" & Group).Add("CYCLE OFF", 0)
                MonthlyGroupPerf("Grp_" & Group).Add("SETUP", 0)
                MonthlyGroupPerf("Grp_" & Group).Add("TOTAL", 0)

                WeeklyGroupPerf.Add("Grp_" & Group, New Dictionary(Of String, Double))
                WeeklyGroupPerf("Grp_" & Group).Add("OTHER", 0)
                WeeklyGroupPerf("Grp_" & Group).Add("CYCLE ON", 0)
                WeeklyGroupPerf("Grp_" & Group).Add("CYCLE OFF", 0)
                WeeklyGroupPerf("Grp_" & Group).Add("SETUP", 0)
                WeeklyGroupPerf("Grp_" & Group).Add("TOTAL", 0)

                If Not MonthlyTargets.ContainsKey(Group) Then
                    MonthlyTargets.Add(Group, 0)
                Else
                    MonthlyTargets(Group) = 0
                End If

                If Not WeeklyTargets.ContainsKey(Group) Then
                    WeeklyTargets.Add(Group, 0)
                Else
                    WeeklyTargets(Group) = 0
                End If
            Next

            Try
                If Not WeeklyTargets.ContainsKey("All Machines") Then
                    WeeklyTargets.Add("All Machines", 0)
                Else
                    WeeklyTargets("All Machines") = 0
                End If
            Catch ex As Exception

            End Try
            Try
                If Not MonthlyTargets.ContainsKey("All Machines") Then
                    MonthlyTargets.Add("All Machines", 0)
                Else
                    MonthlyTargets("All Machines") = 0
                End If
            Catch ex As Exception

            End Try

            'machine list:
            Dim mch_tbl As New DataTable
            Dim dadapter_name As New MySqlDataAdapter("SELECT * FROM csi_database.tbl_renamemachines", CSI_Library.CSI_Library.MySqlConnectionString)
            dadapter_name.Fill(mch_tbl)


            Dim month As Date = Now.AddDays(-((Now.Date.Day - 1)))
            Dim StringMonth As String = month.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))

            Dim week As Date = Now.AddDays(-(Now.DayOfWeek - ServerSettings.FirstDayOfWeek))

            Dim StringWeek As String = week.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))

            Dim todayDate As Date = DateTime.Now().Date.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))

            Dim JSON_w As String = "", JSON_m As String = "" ', JSON_d As String = ""

            Dim AllMachineNames As New List(Of String)

            AllMachineNames.Clear()

            Dim readed As String = String.Empty
            Dim currentrow As String()
            Dim path = csi_lib.getRootPath()

            If File.Exists(path & "\sys\machine_list_.csys") Then

                Using reader As StreamReader = New StreamReader(path & "\sys\machine_list_.csys")

                    While Not reader.EndOfStream

                        readed = reader.ReadLine()
                        If readed <> "" Then
                            currentrow = readed.Split(",")
                            If currentrow(0) <> "" Then
                                AllMachineNames.Add(currentrow(0).ToString())
                            End If
                        End If
                    End While
                End Using
            End If

            For Each mch As DataRow In mch_tbl.Rows

                Dim mch_name = Convert.ToString(mch.Item(1))

                Dim mch_t_name = Convert.ToString(mch.Item(0))

                If mch_t_name.StartsWith("tbl_") Then
                    mch_t_name = mch_t_name.Substring(4)
                End If

                If mch_name <> "" And AllMachineNames.Contains(mch_name) Then

                    Dim queryCmd = New Text.StringBuilder()

                    queryCmd.Append($"SET @v1 = (SELECT date FROM csi_machineperf.tbl_{ mch_t_name } order by csi_machineperf.tbl_{ mch_t_name }.date desc limit 1);")
                    queryCmd.Append($"SET @v2 = (Select Max(date) from csi_machineperf.tbl_{ mch_t_name });")

                    queryCmd.Append($"Update csi_machineperf.tbl_{ mch_t_name } set cycletime = TIMESTAMPDIFF( SECOND, @v1, now() ) where csi_machineperf.tbl_{ mch_t_name }.date = @v1;")

                    queryCmd.Append($"Select                          ")
                    queryCmd.Append($"    `status`,                   ")
                    queryCmd.Append($"    sum(cycletime) as cycletime ")
                    queryCmd.Append($"from                            ")
                    queryCmd.Append($"(                               ")
                    queryCmd.Append($"    (                           ")
                    queryCmd.Append($"         select                 ")
                    queryCmd.Append($"             csi_database.tbl_{mch_t_name}.status,       ")
                    queryCmd.Append($"	            csi_database.tbl_{mch_t_name}.cycletime    ")
                    queryCmd.Append($"         from                                            ")
                    queryCmd.Append($"	            csi_database.tbl_{mch_t_name}              ")
                    queryCmd.Append($"         where                                           ")
                    queryCmd.Append($"	                csi_database.tbl_{mch_t_name}.Date_ >= ")

                    ' This line will be complete with a date using String.Format()
                    queryCmd.Append("                      '{0}'                               ")

                    queryCmd.Append($"             And csi_database.tbl_{mch_t_name}.Date_ <  '{todayDate.ToString("yyyy-MM-dd")}' ")
                    queryCmd.Append($"             And NOT status  LIKE '_PARTNO%'  ")
                    queryCmd.Append($"    )                           ")
                    queryCmd.Append($"    union all                   ")
                    queryCmd.Append($"    (                           ")
                    queryCmd.Append($"         select                 ")
                    queryCmd.Append($"             tab2.status,       ")
                    queryCmd.Append($"             TIMESTAMPDIFF( SECOND, tab2.date, tab1.date ) AS cycletime ")
                    queryCmd.Append($"         from                   ")
                    queryCmd.Append($"         (                      ")
                    queryCmd.Append($"             SELECT             ")
                    queryCmd.Append($"                 b.status,      ")
                    queryCmd.Append($"                 @rn1:=@rn1+1 AS `rank`, ")
                    queryCmd.Append($"                 b.time,        ")
                    queryCmd.Append($"                 b.date,        ")
                    queryCmd.Append($"                 b.cycletime,   ")
                    queryCmd.Append($"                 b.shift        ")
                    queryCmd.Append($"             FROM               ")
                    queryCmd.Append($"                 csi_machineperf.tbl_{mch_t_name} b, ")
                    queryCmd.Append($"                 (SELECT @rn1:=0) t1                 ")
                    queryCmd.Append($"         ) as tab1,             ")
                    queryCmd.Append($"         (                      ")
                    queryCmd.Append($"             SELECT             ")
                    queryCmd.Append($"                 a.status,      ")
                    queryCmd.Append($"                 @rn:=@rn+1 AS `rank`, ")
                    queryCmd.Append($"                 a.time,        ")
                    queryCmd.Append($"                 a.date,        ")
                    queryCmd.Append($"                 a.cycletime,   ")
                    queryCmd.Append($"                 a.shift        ")
                    queryCmd.Append($"             FROM               ")
                    queryCmd.Append($"                 csi_machineperf.tbl_{mch_t_name} a, ")
                    queryCmd.Append($"                 (SELECT @rn:=0) t2                  ")
                    queryCmd.Append($"         ) as tab2              ")
                    queryCmd.Append($"         WHERE                  ")
                    queryCmd.Append($"             tab1.rank = tab2.rank + 1 ")
                    queryCmd.Append($"    )                                  ")
                    queryCmd.Append($"    union all                          ")
                    queryCmd.Append($"    (                                  ")
                    queryCmd.Append($"         SELECT                        ")
                    queryCmd.Append($"             csi_machineperf.tbl_{mch_t_name}.status,           ")
                    queryCmd.Append($"             csi_machineperf.tbl_{mch_t_name}.cycletime         ")
                    queryCmd.Append($"         From                                                   ")
                    queryCmd.Append($"             csi_machineperf.tbl_{mch_t_name}                   ")
                    queryCmd.Append($"         order by                                               ")
                    queryCmd.Append($"             csi_machineperf.tbl_{mch_t_name}.date desc limit 1 ")
                    queryCmd.Append($"    )                                                           ")
                    queryCmd.Append($") as vij                                                        ")
                    queryCmd.Append($"group by                                                        ")
                    queryCmd.Append($"    vij.status")

                    cmmd = String.Format(queryCmd.ToString(), month.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")))

                    dset_perf_monthly.Tables.Add(mch_name)

                    dadapter_name = New MySqlDataAdapter(cmmd, CSI_Library.CSI_Library.MySqlConnectionString)

                    dadapter_name.Fill(dset_perf_monthly.Tables(mch_name))

                    Try
                        MonthlyPerf_Seconds.Add(mch_name, groupe_CON_COFF_SETUP_OTHER(dset_perf_monthly.Tables(mch_name)))
                    Catch ex As Exception
                        'General_Setup.WriteToFile("Error :" & ex.Message)
                    End Try

                    JSON_m = JsonConvert.SerializeObject(dset_perf_monthly.Tables(mch_name))

                    cmmd = String.Format(queryCmd.ToString(), week.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")))

                    dset_perf_weekly.Tables.Add(mch_name)

                    dadapter_name = New MySqlDataAdapter(cmmd, CSI_Library.CSI_Library.MySqlConnectionString)
                    dadapter_name.Fill(dset_perf_weekly.Tables(mch_name))

                    Try
                        WeeklyPerf_Seconds.Add(mch_name, groupe_CON_COFF_SETUP_OTHER(dset_perf_weekly.Tables(mch_name)))
                    Catch ex As Exception

                    End Try

                    JSON_w = JsonConvert.SerializeObject(dset_perf_weekly.Tables(mch_name))

                    'Get Monthly and Weekly Data 
                    Dim MonthlyTargetsFromTable As New Integer
                    Dim WeeklyTargetsFromTable As New Integer

                    Dim dTable_selectMonthlyWeeklyTargets = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_ehub_conf where machine_name = '{ mch_name }';")

                    If dTable_selectMonthlyWeeklyTargets.Rows.Count > 0 Then
                        MonthlyTargetsFromTable = Convert.ToInt32(dTable_selectMonthlyWeeklyTargets.Rows.Item(0).Item("MCH_MonthlyTarget"))
                        WeeklyTargetsFromTable = Convert.ToInt32(dTable_selectMonthlyWeeklyTargets.Rows.Item(0).Item("MCH_WeeklyTarget"))
                    Else
                        MonthlyTargetsFromTable = 0
                        WeeklyTargetsFromTable = 0
                    End If

                    'Conpute perf for groups:
                    For Each group In groups_list("groups").Keys

                        If groups_list("groups")(group).Contains(mch_name) Then

                            MonthlyGroupPerf("Grp_" & group)("OTHER") = MonthlyGroupPerf("Grp_" & group)("OTHER") + MonthlyPerf_Seconds((mch.Item(1)))("OTHER") / 3600
                            MonthlyGroupPerf("Grp_" & group)("CYCLE ON") = MonthlyGroupPerf("Grp_" & group)("CYCLE ON") + MonthlyPerf_Seconds((mch.Item(1)))("CYCLE ON") / 3600
                            MonthlyGroupPerf("Grp_" & group)("CYCLE OFF") = MonthlyGroupPerf("Grp_" & group)("CYCLE OFF") + MonthlyPerf_Seconds((mch.Item(1)))("CYCLE OFF") / 3600
                            MonthlyGroupPerf("Grp_" & group)("SETUP") = MonthlyGroupPerf("Grp_" & group)("SETUP") + MonthlyPerf_Seconds((mch.Item(1)))("SETUP") / 3600
                            MonthlyGroupPerf("Grp_" & group)("TOTAL") = MonthlyGroupPerf("Grp_" & group)("TOTAL") + MonthlyPerf_Seconds((mch.Item(1)))("TOTAL") / 3600

                            WeeklyGroupPerf("Grp_" & group)("OTHER") = WeeklyGroupPerf("Grp_" & group)("OTHER") + WeeklyPerf_Seconds((mch.Item(1)))("OTHER") / 3600
                            WeeklyGroupPerf("Grp_" & group)("CYCLE ON") = WeeklyGroupPerf("Grp_" & group)("CYCLE ON") + WeeklyPerf_Seconds((mch.Item(1)))("CYCLE ON") / 3600
                            WeeklyGroupPerf("Grp_" & group)("CYCLE OFF") = WeeklyGroupPerf("Grp_" & group)("CYCLE OFF") + WeeklyPerf_Seconds((mch.Item(1)))("CYCLE OFF") / 3600
                            WeeklyGroupPerf("Grp_" & group)("SETUP") = WeeklyGroupPerf("Grp_" & group)("SETUP") + WeeklyPerf_Seconds((mch.Item(1)))("SETUP") / 3600
                            WeeklyGroupPerf("Grp_" & group)("TOTAL") = WeeklyGroupPerf("Grp_" & group)("TOTAL") + WeeklyPerf_Seconds((mch.Item(1)))("TOTAL") / 3600

                            Try
                                MonthlyTargets(group) = MonthlyTargets(group) + MonthlyTargetsFromTable
                            Catch ex As Exception

                            End Try
                            Try
                                WeeklyTargets(group) = WeeklyTargets(group) + WeeklyTargetsFromTable
                            Catch ex As Exception

                            End Try

                        End If
                    Next

                    Try
                        MonthlyTargets("All Machines") = MonthlyTargets("All Machines") + MonthlyTargetsFromTable
                    Catch ex As Exception

                    End Try
                    Try
                        WeeklyTargets("All Machines") = WeeklyTargets("All Machines") + WeeklyTargetsFromTable
                    Catch ex As Exception

                    End Try

                    cmmd = $"INSERT INTO csi_machineperf.tbl_perf(machinename_, weekly_, monthly_) VALUES ('{ mch_name }', '{ JSON_w }', '{ JSON_m }') ON DUPLICATE KEY UPDATE weekly_='{ JSON_w }', monthly_='{ JSON_m }';"
                    MySqlAccess.ExecuteNonQuery(cmmd)

                    'PUT THE PERF IN JSON:
                    '--------------------------------------------'
                    ' MCH       '   weekly JSON   ' monthly JSON '
                    '-----------'-----------------'--------------'

                    PERF_TBL.Rows.Add(mch_name, JSON_w, JSON_m) ', JSON_d
                End If
            Next

            Dim TargetTitle As String = "Target : "

            Dim TitlesMonthly As New Dictionary(Of String, String)
            Dim TitlesWeekly As New Dictionary(Of String, String)

            Dim TargetTile_Monthly As String = String.Empty
            Dim TargetTile_Weekly As String = String.Empty

            For Each Group In WeeklyGroupPerf.Keys

                Dim groupName As String = Group.Replace("Grp_", "")

                Try
                    If Not IsNothing(MonthlyTargets(groupName)) Then
                        TargetTile_Monthly = Group & " " & Math.Round(MonthlyGroupPerf(Group)("CYCLE ON")) & "h of " & Math.Round(MonthlyTargets(groupName)) & "h"
                        TitlesMonthly.Add(groupName, TargetTile_Monthly)
                    End If

                Catch ex As Exception

                End Try

                MonthlyPerf_Seconds.Add("Sec_" + Group, New Dictionary(Of String, Double))
                MonthlyPerf_Seconds("Sec_" + Group).Add("CYCLE ON", MonthlyGroupPerf(Group)("CYCLE ON"))
                MonthlyPerf_Seconds("Sec_" + Group).Add("CYCLE OFF", MonthlyGroupPerf(Group)("CYCLE OFF") + MonthlyGroupPerf(Group)("SETUP") + MonthlyGroupPerf(Group)("OTHER"))
                MonthlyPerf_Seconds("Sec_" + Group).Add("SETUP", 0)
                MonthlyPerf_Seconds("Sec_" + Group).Add("OTHER", 0)

                Try
                    If Not IsNothing(WeeklyTargets(groupName)) Then
                        TargetTile_Weekly = Group & " " & Math.Round(WeeklyGroupPerf(Group)("CYCLE ON")) & "h of " & Math.Round(WeeklyTargets(groupName)) & "h"
                        TitlesWeekly.Add(groupName, TargetTile_Weekly)
                    End If

                Catch ex As Exception

                End Try

                WeeklyPerf_Seconds.Add("Sec_" + Group, New Dictionary(Of String, Double))
                WeeklyPerf_Seconds("Sec_" + Group).Add("CYCLE ON", WeeklyGroupPerf(Group)("CYCLE ON"))
                WeeklyPerf_Seconds("Sec_" + Group).Add("CYCLE OFF", WeeklyGroupPerf(Group)("CYCLE OFF") + WeeklyGroupPerf(Group)("SETUP") + WeeklyGroupPerf(Group)("OTHER"))
                WeeklyPerf_Seconds("Sec_" + Group).Add("SETUP", 0)
                WeeklyPerf_Seconds("Sec_" + Group).Add("OTHER", 0)

                If MonthlyGroupPerf(Group)("TOTAL") = 0 Then
                    MonthlyGroupPerf(Group)("CYCLE ON") = 0
                    MonthlyGroupPerf(Group)("CYCLE OFF") = 0
                    MonthlyGroupPerf(Group)("SETUP") = 0
                    MonthlyGroupPerf(Group)("OTHER") = 0
                Else
                    MonthlyGroupPerf(Group)("CYCLE ON") = MonthlyGroupPerf(Group)("CYCLE ON") * 100 / MonthlyGroupPerf(Group)("TOTAL")
                    MonthlyGroupPerf(Group)("CYCLE OFF") = MonthlyGroupPerf(Group)("CYCLE OFF") * 100 / MonthlyGroupPerf(Group)("TOTAL")
                    MonthlyGroupPerf(Group)("SETUP") = MonthlyGroupPerf(Group)("SETUP") * 100 / MonthlyGroupPerf(Group)("TOTAL")
                    MonthlyGroupPerf(Group)("OTHER") = MonthlyGroupPerf(Group)("OTHER") * 100 / MonthlyGroupPerf(Group)("TOTAL")
                End If

                If WeeklyGroupPerf(Group)("TOTAL") = 0 Then
                    WeeklyGroupPerf(Group)("CYCLE ON") = 0
                    WeeklyGroupPerf(Group)("CYCLE OFF") = 0
                    WeeklyGroupPerf(Group)("SETUP") = 0
                    WeeklyGroupPerf(Group)("OTHER") = 0
                Else
                    WeeklyGroupPerf(Group)("CYCLE ON") = WeeklyGroupPerf(Group)("CYCLE ON") * 100 / WeeklyGroupPerf(Group)("TOTAL")
                    WeeklyGroupPerf(Group)("CYCLE OFF") = WeeklyGroupPerf(Group)("CYCLE OFF") * 100 / WeeklyGroupPerf(Group)("TOTAL")
                    WeeklyGroupPerf(Group)("SETUP") = WeeklyGroupPerf(Group)("SETUP") * 100 / WeeklyGroupPerf(Group)("TOTAL")
                    WeeklyGroupPerf(Group)("OTHER") = WeeklyGroupPerf(Group)("OTHER") * 100 / WeeklyGroupPerf(Group)("TOTAL")
                End If

                Dim txt As Text.StringBuilder = New StringBuilder()

                txt.Append($"[")
                txt.Append($"{{ ""status"":""CYCLE OFF"", ""cycletime"":{ Convert.ToString(WeeklyGroupPerf(Group)("CYCLE OFF")).Replace(",", ".") } }},")
                txt.Append($"{{ ""status"":""CYCLE ON"" , ""cycletime"":{ Convert.ToString(WeeklyGroupPerf(Group)("CYCLE ON")).Replace(",", ".")  } }},")
                txt.Append($"{{ ""status"":""OTHER""    , ""cycletime"":{ Convert.ToString(WeeklyGroupPerf(Group)("OTHER")).Replace(",", ".")     } }},")
                txt.Append($"{{ ""status"":""SETUP""    , ""cycletime"":{ Convert.ToString(WeeklyGroupPerf(Group)("SETUP")).Replace(",", ".")     } }} ")
                txt.Append($"]")

                JSON_w = txt.ToString()

                txt.Clear()
                txt.Append($"[")
                txt.Append($"{{ ""status"":""CYCLE OFF"", ""cycletime"":{ Convert.ToString(MonthlyGroupPerf(Group)("CYCLE OFF")).Replace(",", ".") } }},")
                txt.Append($"{{ ""status"":""CYCLE ON"" , ""cycletime"":{ Convert.ToString(MonthlyGroupPerf(Group)("CYCLE ON")).Replace(",", ".")  } }},")
                txt.Append($"{{ ""status"":""OTHER""    , ""cycletime"":{ Convert.ToString(MonthlyGroupPerf(Group)("OTHER")).Replace(",", ".")     } }},")
                txt.Append($"{{ ""status"":""SETUP""    , ""cycletime"":{ Convert.ToString(MonthlyGroupPerf(Group)("SETUP")).Replace(",", ".")     } }} ")
                txt.Append($"]")

                JSON_m = txt.ToString()

                Try
                    cmmd = $"INSERT INTO csi_machineperf.tbl_perf(machinename_, weekly_, monthly_) VALUES ('{ Convert.ToString(Group) }', '{ JSON_w }', '{ JSON_m }') ON DUPLICATE KEY UPDATE weekly_ = '{ JSON_w }', monthly_ = '{ JSON_m }';"
                    MySqlAccess.ExecuteNonQuery(cmmd)

                    txt.Clear()
                    txt.Append($"[")
                    txt.Append($"{{ ""status"":""CYCLE OFF"", ""cycletime"":{ Convert.ToString(WeeklyPerf_Seconds("Sec_" + Group)("CYCLE OFF")).Replace(",", ".") } }},")
                    txt.Append($"{{ ""status"":""CYCLE ON"" , ""cycletime"":{ Convert.ToString(WeeklyPerf_Seconds("Sec_" + Group)("CYCLE ON")).Replace(",", ".")  } }},")
                    txt.Append($"{{ ""status"":""OTHER""    , ""cycletime"":{ Convert.ToString(WeeklyPerf_Seconds("Sec_" + Group)("OTHER")).Replace(",", ".")     } }},")
                    txt.Append($"{{ ""status"":""SETUP""    , ""cycletime"":{ Convert.ToString(WeeklyPerf_Seconds("Sec_" + Group)("SETUP")).Replace(",", ".")     } }} ")
                    txt.Append($"]")

                    JSON_w = txt.ToString()

                    txt.Clear()
                    txt.Append($"[")
                    txt.Append($"{{ ""status"":""CYCLE OFF"", ""cycletime"":{ Convert.ToString(MonthlyPerf_Seconds("Sec_" + Group)("CYCLE OFF")).Replace(",", ".") } }},")
                    txt.Append($"{{ ""status"":""CYCLE ON"" , ""cycletime"":{ Convert.ToString(MonthlyPerf_Seconds("Sec_" + Group)("CYCLE ON")).Replace(",", ".")  } }},")
                    txt.Append($"{{ ""status"":""OTHER""    , ""cycletime"":{ Convert.ToString(MonthlyPerf_Seconds("Sec_" + Group)("OTHER")).Replace(",", ".")     } }},")
                    txt.Append($"{{ ""status"":""SETUP""    , ""cycletime"":{ Convert.ToString(MonthlyPerf_Seconds("Sec_" + Group)("SETUP")).Replace(",", ".")     } }} ")
                    txt.Append($"]")

                    JSON_m = txt.ToString()

                    cmmd = $" INSERT INTO csi_machineperf.tbl_perf(machinename_, weekly_, monthly_) VALUES ('Sec_{ Convert.ToString(Group) }', '{ JSON_w }', '{ JSON_m }') ON DUPLICATE KEY UPDATE weekly_= '{ JSON_w }', monthly_ = '{ JSON_m }'; "
                    cmmd += $"INSERT INTO csi_machineperf.tbl_perf(machinename_, weekly_, monthly_) VALUES ('TitleTarget_{ Convert.ToString(Group) }', '{ TargetTile_Weekly }', '{ TargetTile_Monthly }') ON DUPLICATE KEY UPDATE weekly_ = '{ TargetTile_Weekly }', monthly_ = '{ TargetTile_Monthly }';"
                    MySqlAccess.ExecuteNonQuery(cmmd)

                Catch ex As Exception
                    Log.Error("Error in tbl_perf while inserting Sec_,Normal and Monthly Target Values.", ex)
                End Try
            Next

            ' add groups + groups perf in the machine list
            Dim CompletePERF_TBL As New DataTable("perf")
            CompletePERF_TBL = AddGroups(PERF_TBL.Copy, TitlesWeekly, TitlesMonthly)
            CompletePERF_TBL.Merge(PERF_TBL.Copy)
            CompletePERF_TBL.TableName = "perf"

            PERF_TBL = CompletePERF_TBL

            perf_computed___ = True

        Catch ex As Exception

            Log.Error("Err while computing perf for the pie chart.", ex)
            perf_computed___ = False

        End Try

#Disable Warning BC42105 ' Function 'compute_perf_from_db' doesn't return a value on all code paths. A null reference exception could occur at run time when the result is used.
    End Function

#Enable Warning BC42105 ' Function 'compute_perf_from_db' doesn't return a value on all code paths. A null reference exception could occur at run time when the result is used.

    Private Function groupe_CON_COFF_SETUP_OTHER(ByRef tbl As DataTable) As Dictionary(Of String, Double)

        Dim Perf_Seconds As New Dictionary(Of String, Double)

        Try

            Dim other_CTIME As Double = 0

            If tbl.Rows.Count = 0 Then

                Perf_Seconds.Add("OTHER", 0)
                tbl.Rows.Add("OTHER", 0)

                tbl.Rows.Add("CYCLE ON", 0)
                Perf_Seconds.Add("CYCLE ON", 0)

                tbl.Rows.Add("CYCLE OFF", 0)
                Perf_Seconds.Add("CYCLE OFF", 0)

                tbl.Rows.Add("SETUP", 0)
                Perf_Seconds.Add("SETUP", 0)

                Perf_Seconds.Add("TOTAL", 0)

            Else

                Dim total_CTIME As Double = tbl.Compute("Sum(cycletime)", "")

                Perf_Seconds.Add("TOTAL", total_CTIME)

                If (If(IsDBNull(total_CTIME), 0, total_CTIME) <> 0) Then
                    Dim tmp_tbl As DataTable = tbl.Copy
                    tbl.Rows.Clear()

                    Dim CON_OK As Boolean = False
                    Dim COFF_OK As Boolean = False
                    Dim SETUP_OK As Boolean = False

                    For Each row As DataRow In tmp_tbl.Rows

                        If (row(0) <> "CYCLE ON") And (row(0) <> "CYCLE OFF") And (row(0) <> "SETUP") Then
                            other_CTIME = If(IsDBNull(row(1)), 0, row(1)) + other_CTIME
                        Else
                            If row(0) = "CYCLE ON" Then CON_OK = True
                            If row(0) = "CYCLE OFF" Then COFF_OK = True
                            If row(0) = "SETUP" Then SETUP_OK = True

                            tbl.Rows.Add(row(0), If(IsDBNull(row(1)), 0, row(1)) * 100 / total_CTIME)
                            Perf_Seconds.Add(row(0), row(1))
                        End If
                    Next
                    Perf_Seconds.Add("OTHER", other_CTIME)
                    tbl.Rows.Add("OTHER", other_CTIME * 100 / total_CTIME)

                    If CON_OK = False Then
                        tbl.Rows.Add("CYCLE ON", 0)
                        Perf_Seconds.Add("CYCLE ON", 0)
                    End If
                    If COFF_OK = False Then
                        tbl.Rows.Add("CYCLE OFF", 0)
                        Perf_Seconds.Add("CYCLE OFF", 0)
                    End If
                    If SETUP_OK = False Then
                        tbl.Rows.Add("SETUP", 0)
                        Perf_Seconds.Add("SETUP", 0)
                    End If

                Else
                    Perf_Seconds.Add("OTHER", 0)
                    tbl.Rows.Add("OTHER", 0)

                    tbl.Rows.Add("CYCLE ON", 0)
                    Perf_Seconds.Add("CYCLE ON", 0)

                    tbl.Rows.Add("CYCLE OFF", 0)
                    Perf_Seconds.Add("CYCLE OFF", 0)

                    tbl.Rows.Add("SETUP", 0)
                    Perf_Seconds.Add("SETUP", 0)

                End If
            End If

            Dim dv As DataView = tbl.DefaultView
            dv.Sort = "status asc"
            tbl = dv.ToTable()

        Catch ex As Exception
            csi_lib.LogServerError("Err while groupping _CON_COFF_SETUP_OTHER (computing perf for the pie chart) : " + ex.Message + " , " + vbCrLf + "___" + ex.StackTrace, 1)
            perf_computed___ = False


        End Try
        Return Perf_Seconds

    End Function

    Public Async Sub Read_perf_from_db()

        MySqlAccess.Validate_MachinePerf_Perf_Table()

        Try

            PERF_TBL.Clear()
            PERF_TBL = MySqlAccess.GetDataTable("SELECT * FROM csi_machineperf.tbl_perf")
            PERF_TBL.TableName = "perf"

            If PERF_TBL.Rows.Count = 0 Then
                Await Task.Factory.StartNew(Function() compute_perf_from_db2())
            Else
                perf_computed___ = True
            End If

        Catch ex As Exception

            Log.Error($"Err while trying to read the perf from the database.", ex)

        End Try

    End Sub

    Public Function Convet_DataTable_To_JSONW(table As DataSet) As String
        Dim JSONString As String = String.Empty
        JSONString = JsonConvert.SerializeObject(table)
        Return JSONString
    End Function

    Public Function Convet_Dictionary_To_JSONW(dic As Dictionary(Of String, DataTable)) As String
        Dim JSONString As String = String.Empty
        JSONString = JsonConvert.SerializeObject(dic)
        Return JSONString
    End Function

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function

    'Private Shared Sub return404(context As HttpListenerContext)
    '    context.Response.StatusCode = 404
    '    context.Response.Close()
    'End Sub

    Private Shared Function getcontentType(extension As String) As String
        Select Case extension
            Case ".avi"
                Return "video/x-msvideo"
            Case ".css"
                Return "text/css"
            Case ".doc"
                Return "application/msword"
            Case ".gif"
                Return "image/gif"
            Case ".htm", ".html"
                Return "text/html"
            Case ".jpg", ".jpeg"
                Return "image/jpeg"
            Case ".js"
                Return "application/x-javascript"
            Case ".mp3"
                Return "audio/mpeg"
            Case ".png"
                Return "image/png"
            Case ".pdf"
                Return "application/pdf"
            Case ".ppt"
                Return "application/vnd.ms-powerpoint"
            Case ".zip"
                Return "application/zip"
            Case ".txt"
                Return "text/plain"
            Case ".json"
                Return "text/javascript"
            Case Else
                Return "application/octet-stream"
        End Select
    End Function

#End Region


#Region "enet_livestatus"

    Dim realtime_dic As New Dictionary(Of String, DataTable) ' dic of (mch_name, real time datatable)
    Dim rm_port_number As String

    Private Sub fct_enet_livestatus()


        Dim dt As New DataTable("values")

        Dim machinedic As New Dictionary(Of String, EMachine)(StringComparer.CurrentCultureIgnoreCase)

        Dim monsetupfile As String = Path.Combine(eNetServer.EnetSetupFolder, "MonSetup.sys")

        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        Dim oldFileHash As Byte() = New Byte() {}

        rm_port_number = LoadPortNumberFromDB()
        Init_RealTime_datatable()

        Dim isNewDay As Boolean = False

        Dim sqlCmd As Text.StringBuilder = New StringBuilder()

        Log.Info("Started task eNET Live Status")

        While Not _shutdownEvent.WaitOne(0)

            If isNewDay Then
                If Not license.IsLicenseValid(LicenseProducts.CSIFLEXServer) Then
                    Exit While
                End If
                isNewDay = False
            End If

            Try
                'If MonSetup file data is changed then read Monsetup file for all machines to get TH parameters value otherwise don't do anything 
                Dim newFileHash = GetFileHash(monsetupfile)

                If compareFileHash(oldFileHash, newFileHash) Then
                    'if Both are same then do nothing
                Else
                    'Load values of TH into database 
                    GetAllEnetMachines2()
                    'If both the hash values are different 
                    oldFileHash = newFileHash
                End If

                If real_time_dic_filled = True Then
                    '''''''''''''GET
                    Try
                        dt = GetEnetPage()

                        If dt.Rows.Count <> 0 Then
                            'RealTime_datatable passed to /refresh page that's 
                            add_utilization(dt)
                            RealTime_datatable = dt
                        End If

                    Catch ex As Exception
                        Log.Error($"ERROR getting data from enet http server ({enetip})", ex)
                    End Try

                    Try
                        Dim newdicMachines As New Dictionary(Of Integer, EMachine)

                        If Not (dt Is Nothing) Then

                            For Each row As DataRow In dt.Rows
                                Try
                                    Dim machineId = row.Item("MachineId")
                                    Dim machineStatus = row.Item("Status")

                                    'Log.Info($">>>>> Machine: {machineId}, Operator: {row.Item("OperatorRefId")}")

                                    newdicMachines.Add(row.Item("MachineId"), New EMachine(row, "enet"))
                                Catch ex As Exception
                                    Log.Error($"Machine: { row.Item("Machine") } Status: { row.Item("Status") } PartNumber: { row.Item("PartNumber") } CycleCount: { row.Item("CycleCount") }", ex)
                                End Try
                            Next
                        End If

                        For Each mch In newdicMachines

                            Dim machine = mch.Value
                            Dim machineId = mch.Key
                            Dim machineTableName = csi_lib.RenameMachine(machine.EnetMchName)
                            Dim shift = machine.Shift

                            If Not realtime_dic.ContainsKey(machineId) Then

                                realtime_dic.Add(machineId, New DataTable)
                                realtime_dic(machineId).Columns.Add("status", GetType(String))
                                realtime_dic(machineId).Columns.Add("time", GetType(Integer))
                                realtime_dic(machineId).Columns.Add("cycletime", GetType(Integer))
                                realtime_dic(machineId).Columns.Add("shift", GetType(Integer))
                                realtime_dic(machineId).Columns.Add("date", GetType(DateTime))
                                realtime_dic(machineId).Columns.Add("comment", GetType(String))
                                realtime_dic(machineId).Columns.Add("feedrate", GetType(DateTime))
                                realtime_dic(machineId).Columns.Add("spindle", GetType(DateTime))

                                Dim keys(2) As DataColumn
                                keys(0) = realtime_dic(machineId).Columns(0)
                                keys(1) = realtime_dic(machineId).Columns(1)
                                realtime_dic(machineId).PrimaryKey = keys

                                MySqlAccess.Validate_Perf_Machine_Table(machine.EnetMchName)

                                Dim dtSelectHist As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM CSI_machineperf.tbl_{ machineTableName } WHERE Shift = { shift } AND status NOT LIKE '\_%' ORDER BY date")

                                For Each row As DataRow In dtSelectHist.Rows
                                    realtime_dic(machineId).Rows.Add(row("status"), row("time"), row("cycletime"), row("shift"), row("date"), row("comments"))
                                Next

                            End If

                            If (machinedic.ContainsKey(machineId)) Then

                                Dim _curstatus = ""
                                Dim _time = 0
                                Dim _shift = ""
                                Dim _partNr = ""
                                Dim _operator = ""

                                Dim lastRecord = MySqlAccess.GetDataTable($"SELECT status, time, shift, partnumber, operator FROM CSI_machineperf.tbl_{machineTableName} WHERE status NOT LIKE '\_%' ORDER BY Id DESC LIMIT 1;")

                                If lastRecord.Rows.Count > 0 Then
                                    _curstatus = lastRecord.Rows(0)("status")
                                    _time = lastRecord.Rows(0)("time")
                                    _shift = lastRecord.Rows(0)("shift")
                                    _partNr = lastRecord.Rows(0)("partnumber")
                                    _operator = lastRecord.Rows(0)("operator")
                                End If

                                'Log.Info($">>> Machine: {machine.MchId} - Operator: {machine.OperatorRefId}")

                                '-------- NEW SHIFT
                                If Not (machinedic.Item(machineId).Shift = shift) Then ' LastShift = CurrentShift

                                    Log.Info($"Change of shift. Machine: { machine.MchName }, Old: { machinedic.Item(machineId).Shift }, New: { shift }")

                                    ':: Special Case ::::  Code For Update CYCLE ON Shift Change 
                                    'On Shift change update ENET Machine TMP File for Machine In Cycle ON During Shift Change
                                    'First Perform this change and then DElete Previous Shift Data from MachinePerf Table
                                    'ignore _PARTNO and _DPRINT_ and take only the first line that has real status and stop there 

                                    Try

                                        Dim dTable_SelectEhub As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf where CurrentStatus = 'CYCLE ON' and Con_type ='5' ;")

                                        If dTable_SelectEhub.Rows.Count > 0 Then

                                            Try
                                                For Each dr As DataRow In dTable_SelectEhub.Rows

                                                    Dim Filename As String = String.Empty
                                                    Filename = dr("monitoring_filename").ToString()

                                                    Dim TMP_MonitoringFile As String = Path.Combine(eNetServer.EnetTempFolder, Filename)

                                                    If File.Exists(TMP_MonitoringFile) Then

                                                        Dim lineCount As Integer = 0
                                                        Dim readAllLines() As String = FileUtils.AllLinesFromTxtFile(TMP_MonitoringFile).ToArray()

                                                        For I As Integer = 0 To readAllLines.Length - 1

                                                            If readAllLines(I).Contains("_PARTNO") Or readAllLines(I).Contains("_DPRINT_") Or readAllLines(I).Contains("_OPERATOR") Or readAllLines(I).Contains("NO eMONITOR") Then

                                                            Else

                                                                While lineCount = 0

                                                                    Dim SplitLine() As String

                                                                    SplitLine = readAllLines(I).Split(",")
                                                                    SplitLine(2) = "_CON"

                                                                    Dim newString As String = String.Empty

                                                                    For j As Integer = 0 To SplitLine.Length - 1
                                                                        If j < SplitLine.Length - 1 Then
                                                                            If j = 0 Then
                                                                                newString = SplitLine(j) + ","
                                                                            Else
                                                                                newString = newString + SplitLine(j) + ","
                                                                            End If
                                                                        Else
                                                                            newString = newString + SplitLine(j)
                                                                        End If
                                                                    Next
                                                                    readAllLines(I) = String.Empty
                                                                    readAllLines(I) = newString
                                                                    lineCount = lineCount + 1
                                                                End While
                                                            End If
                                                        Next
                                                        IO.File.WriteAllLines(TMP_MonitoringFile, readAllLines) 'assuming you want to write the file
                                                    End If
                                                Next

                                            Catch ex As Exception
                                                Log.Error("TMP_MonitoringFile read error", ex)
                                            End Try

                                        End If

                                    Catch ex As Exception
                                        Log.Error("Error in Special Case ::::  Code For Update CYCLE ON Shift Change", ex)
                                    End Try

                                    realtime_dic(machineId).Rows.Clear()
                                    realtime_dic(machineId).Rows.Add(machine.Statut, machine.StatusDateSeconds, 0, shift, machine.StatusDateTime)

                                    If (shift <= 1) Then

                                        MySqlAccess.ExecuteNonQuery($"TRUNCATE TABLE CSI_machineperf.tbl_{machineTableName}")

                                        isNewDay = True

                                        Log.Info($"It's a new day. Machine: {machineId}, Shift: {shift} - { machine.Statut } - { machine.StatusDateString }.")

                                    End If

                                    If _curstatus.ToUpper().StartsWith("SETUP") And machine.Statut = "CYCLE ON" Then
                                        machine.Statut = "SETUP-CYCLE ON"
                                    End If

                                    sqlCmd.Clear()
                                    If _curstatus.Equals(machine.Statut) And _shift.Equals(machine.Shift) And _partNr.Equals(machine.PartNo) And _operator.Equals(machine.OperatorRefId) Then
                                        sqlCmd.Append($"UPDATE                                       ")
                                        sqlCmd.Append($"    CSI_machineperf.tbl_{ machineTableName } ")
                                        sqlCmd.Append($" SET                                         ")
                                        sqlCmd.Append($"    cycletime = 0                            ")
                                        sqlCmd.Append($" WHERE                                       ")
                                        sqlCmd.Append($"    status  = '{ _curstatus }'  AND          ")
                                        sqlCmd.Append($"    time    =  { _time }                   ; ")
                                    Else

                                        Log.Info($"### A-17: MachineId: {machineId}, Status: {machine.Statut}, Time: {machine.StatusDateSeconds}")

                                        sqlCmd.Append($"INSERT IGNORE INTO                           ")
                                        sqlCmd.Append($"    CSI_machineperf.tbl_{ machineTableName } ")
                                        sqlCmd.Append($" (                                           ")
                                        sqlCmd.Append($"    status        ,                          ")
                                        sqlCmd.Append($"    time          ,                          ")
                                        sqlCmd.Append($"    cycletime     ,                          ")
                                        sqlCmd.Append($"    shift         ,                          ")
                                        sqlCmd.Append($"    date          ,                          ")
                                        sqlCmd.Append($"    partnumber    ,                          ")
                                        sqlCmd.Append($"    operator      ,                          ")
                                        sqlCmd.Append($"    comments                                 ")
                                        sqlCmd.Append($" ) VALUES (                                  ")
                                        sqlCmd.Append($"    '{ machine.Statut }'           ,         ")
                                        sqlCmd.Append($"     { machine.StatusDateSeconds } ,         ")
                                        sqlCmd.Append($"     0                             ,         ")
                                        sqlCmd.Append($"     { machine.Shift }             ,         ")
                                        sqlCmd.Append($"    '{ machine.StatusDateString }' ,         ")
                                        sqlCmd.Append($"    '{ machine.PartNo }'           ,         ")
                                        sqlCmd.Append($"    '{ machine.OperatorRefId }'    ,         ")
                                        sqlCmd.Append($"    '{ machine.Comment }'                    ")
                                        sqlCmd.Append($" )                                         ; ")
                                    End If

                                    MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                    '--------NEw STATUS
                                ElseIf (Not String.IsNullOrEmpty(machine.Statut) And Not machinedic.Item(machineId).Statut = machine.Statut And
                                       (Not machinedic.Item(machineId).Statut.Contains(machine.Statut) Or machine.Statut = "SETUP")) Then
                                    ' Not machinedic.Item(machineId).PartNo = machine.PartNo Or
                                    ' Not machinedic.Item(machineId).feedOverride = machine.feedOverride Or
                                    ' Not machinedic.Item(machineId).OperatorRefId = machine.OperatorRefId Or
                                    ' Not machinedic.Item(machineId).spindleOverride = machine.spindleOverride Then

                                    Dim diff As Integer = 0
                                    Dim lastItemIdx = realtime_dic(machineId).Rows.Count - 1

                                    If machinedic.Item(machineId).Statut = "LOCKED" Then

                                        realtime_dic(machineId).Rows(lastItemIdx).Item("status") = machine.Statut

                                        diff = Now.TimeOfDay.TotalSeconds - realtime_dic(machineId).Rows(lastItemIdx).Item("time")

                                        If diff < -1 Then diff = diff + 86400
                                        If diff < 0 Then diff = 0

                                        realtime_dic(machineId).Rows(lastItemIdx).Item("cycletime") = diff

                                        sqlCmd.Clear()
                                        sqlCmd.Append($"UPDATE                                                                           ")
                                        sqlCmd.Append($"    CSI_machineperf.tbl_{ machineTableName }                                     ")
                                        sqlCmd.Append($" SET                                                                             ")
                                        sqlCmd.Append($"    status    = '{ machine.Statut }',                                            ")
                                        sqlCmd.Append($"    cycletime =  { realtime_dic(machineId).Rows(lastItemIdx).Item("cycletime") } ")
                                        sqlCmd.Append($" WHERE                                                                           ")
                                        sqlCmd.Append($"       status = 'LOCKED'                                                         ")
                                        sqlCmd.Append($"   AND time   =  { realtime_dic(machineId).Rows(lastItemIdx).Item("time") }      ")

                                        MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                    Else

                                        If machinedic.Item(machineId).Statut = machine.Statut Then
                                            machine.StatusDateTime = DateTime.Now
                                            machine.StatusDateSeconds = CLng(machine.StatusDateTime.TimeOfDay.TotalSeconds)
                                            machine.StatusDateString = machine.StatusDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                                        End If

                                        If _curstatus.ToUpper().StartsWith("SETUP") And machine.Statut = "CYCLE ON" Then
                                            machine.Statut = "SETUP-CYCLE ON"
                                        End If

                                        Try
                                            realtime_dic(machineId).Rows.Add(machine.Statut, machine.StatusDateSeconds, 0, shift, machine.StatusDateTime)
                                        Catch ex As Exception
                                        End Try

                                        If lastItemIdx > 0 Then
                                            diff = machine.StatusDateSeconds - realtime_dic(machineId).Rows(lastItemIdx).Item("time")
                                        End If

                                        If diff < -1 Then diff = diff + 86400
                                        If diff < 0 Then diff = 0
                                        If lastItemIdx > 0 Then realtime_dic(machineId).Rows(lastItemIdx).Item("cycletime") = diff

                                        Log.Info($"### A-21: MachineId: {machineId}, Status: {machine.Statut}, Time: {machine.StatusDateSeconds}")

                                        sqlCmd.Clear()
                                        sqlCmd.Append($"INSERT IGNORE INTO                           ")
                                        sqlCmd.Append($"    CSI_machineperf.tbl_{ machineTableName } ")
                                        sqlCmd.Append($" (                                           ")
                                        sqlCmd.Append($"    status        ,                          ")
                                        sqlCmd.Append($"    time          ,                          ")
                                        sqlCmd.Append($"    cycletime     ,                          ")
                                        sqlCmd.Append($"    shift         ,                          ")
                                        sqlCmd.Append($"    date          ,                          ")
                                        sqlCmd.Append($"    partnumber    ,                          ")
                                        sqlCmd.Append($"    operator      ,                          ")
                                        sqlCmd.Append($"    comments                                 ")
                                        sqlCmd.Append($" ) VALUES (                                  ")
                                        sqlCmd.Append($"    '{ machine.Statut }'           ,         ")
                                        sqlCmd.Append($"     { machine.StatusDateSeconds } ,         ")
                                        sqlCmd.Append($"     0                             ,         ")
                                        sqlCmd.Append($"     { machine.Shift }             ,         ")
                                        sqlCmd.Append($"    '{ machine.StatusDateString }' ,         ")
                                        sqlCmd.Append($"    '{ machine.PartNo }'           ,         ")
                                        sqlCmd.Append($"    '{ machine.OperatorRefId }'    ,         ")
                                        sqlCmd.Append($"    '{ machine.Comment }'                    ")
                                        sqlCmd.Append($" )                                           ")
                                        sqlCmd.Append($" ON DUPLICATE KEY UPDATE                     ")
                                        sqlCmd.Append($"    partnumber = '{ machine.PartNo }';       ")

                                        MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                        If lastItemIdx > 0 Then

                                            sqlCmd.Clear()
                                            sqlCmd.Append($"UPDATE                                                                          ")
                                            sqlCmd.Append($"    CSI_machineperf.tbl_{ machineTableName }                                    ")
                                            sqlCmd.Append($" SET                                                                            ")
                                            sqlCmd.Append($"    cycletime =  { realtime_dic(machineId).Rows(lastItemIdx).Item("cycletime") }")
                                            sqlCmd.Append($" WHERE                                                                          ")
                                            sqlCmd.Append($"    status = '{ realtime_dic(machineId).Rows(lastItemIdx).Item("status") }'     ")
                                            sqlCmd.Append($"  AND time =  { realtime_dic(machineId).Rows(lastItemIdx).Item("time") }        ")
                                            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                        End If

                                    End If

                                    'machinedic.Item(machineId).Statut = machine.Statut
                                    'machinedic.Item(machineId).PartNo = machine.PartNo

                                    '------- Same status as last status (NO STATUS CHANGE)
                                Else

                                    MySqlAccess.Validate_Perf_Machine_Table(machine.EnetMchName)

                                    If realtime_dic(machineId).Rows.Count > 0 Then

                                        Dim diff As Integer = Now.TimeOfDay.TotalSeconds - realtime_dic(machineId).Rows(realtime_dic(machineId).Rows.Count - 1).Item("time")
                                        If diff < -5 Then diff = diff + 86400
                                        If diff < 0 Then diff = 0

                                        realtime_dic(machineId).Rows(realtime_dic(machineId).Rows.Count - 1).Item("cycletime") = diff

                                        Dim rowMch = realtime_dic(machineId).Rows(realtime_dic(machineId).Rows.Count - 1)

                                        sqlCmd.Clear()
                                        sqlCmd.Append($"UPDATE                                        ")
                                        sqlCmd.Append($"    CSI_machineperf.tbl_{ machineTableName }  ")
                                        sqlCmd.Append($" SET                                          ")
                                        sqlCmd.Append($"    cycletime =  { rowMch.Item("cycletime") } ")
                                        sqlCmd.Append($" WHERE                                        ")
                                        sqlCmd.Append($"    status = '{ rowMch.Item("status") }'      ")
                                        sqlCmd.Append($"  AND time =  { rowMch.Item("time") }         ")
                                        MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                    End If
                                End If

                                If (Not machinedic.Item(machineId).Statut = machine.Statut And (Not machinedic.Item(machineId).Statut.Contains(machine.Statut) Or machine.Statut = "SETUP")) Or
                                        Not machinedic.Item(machineId).Shift = machine.Shift Or
                                        Not machinedic.Item(machineId).PartNo = machine.PartNo Or
                                        Not machinedic.Item(machineId).OperatorRefId = machine.OperatorRefId Or
                                        Not machinedic.Item(machineId).feedOverride = machine.feedOverride Or
                                        Not machinedic.Item(machineId).spindleOverride = machine.spindleOverride Then

                                    If Not csi_lib.MCHS_.ContainsKey(machineId) Then
                                        sqlCmd.Clear()
                                        sqlCmd.Append($"INSERT INTO csi_database.tbl_machinestate ")
                                        sqlCmd.Append($"(                                     ")
                                        sqlCmd.Append($"    MachineId       ,                 ")
                                        sqlCmd.Append($"    EventDateTime   ,                 ")
                                        sqlCmd.Append($"    Status          ,                 ")
                                        sqlCmd.Append($"    Shift           ,                 ")
                                        sqlCmd.Append($"    SystemStatus    ,                 ")
                                        sqlCmd.Append($"    Comment         ,                 ")
                                        sqlCmd.Append($"    PartNumber      ,                 ")
                                        sqlCmd.Append($"    ProgramNumber   ,                 ")
                                        sqlCmd.Append($"    Operation       ,                 ")
                                        sqlCmd.Append($"    OperatorRefId   ,                 ")
                                        sqlCmd.Append($"    PartCount       ,                 ")
                                        sqlCmd.Append($"    PartRequired    ,                 ")
                                        sqlCmd.Append($"    TotalPartCount  ,                 ")
                                        sqlCmd.Append($"    FeedOverride    ,                 ")
                                        sqlCmd.Append($"    RapidOverride   ,                 ")
                                        sqlCmd.Append($"    SpindleOverride                   ")
                                        sqlCmd.Append($") VALUES (                            ")
                                        sqlCmd.Append($"    @MachineId       ,                ")
                                        sqlCmd.Append($"    @EventDateTime   ,                ")
                                        sqlCmd.Append($"    @Status          ,                ")
                                        sqlCmd.Append($"    @Shift           ,                ")
                                        sqlCmd.Append($"    @SystemStatus    ,                ")
                                        sqlCmd.Append($"    @Comment         ,                ")
                                        sqlCmd.Append($"    @PartNumber      ,                ")
                                        sqlCmd.Append($"    @ProgramNumber   ,                ")
                                        sqlCmd.Append($"    @Operation       ,                ")
                                        sqlCmd.Append($"    @OperatorRefId   ,                ")
                                        sqlCmd.Append($"    @PartCount       ,                ")
                                        sqlCmd.Append($"    @PartRequired    ,                ")
                                        sqlCmd.Append($"    @TotalPartCount  ,                ")
                                        sqlCmd.Append($"    @FeedOverride    ,                ")
                                        sqlCmd.Append($"    @RapidOverride   ,                ")
                                        sqlCmd.Append($"    @SpindleOverride                  ")
                                        sqlCmd.Append($");                                    ")

                                        Dim sqlCommand As New MySqlCommand(sqlCmd.ToString())
                                        sqlCommand.Parameters.AddWithValue("@MachineId", machineId)
                                        sqlCommand.Parameters.AddWithValue("@EventDateTime", machine.StatusDateString)
                                        sqlCommand.Parameters.AddWithValue("@Status", machine.Statut)
                                        sqlCommand.Parameters.AddWithValue("@Shift", machine.Shift)
                                        sqlCommand.Parameters.AddWithValue("@SystemStatus", machine.Statut)
                                        sqlCommand.Parameters.AddWithValue("@Comment", machine.Comment)
                                        sqlCommand.Parameters.AddWithValue("@PartNumber", machine.PartNo)
                                        sqlCommand.Parameters.AddWithValue("@ProgramNumber", "")
                                        sqlCommand.Parameters.AddWithValue("@Operation", "")
                                        sqlCommand.Parameters.AddWithValue("@OperatorRefId", machine.OperatorRefId)
                                        sqlCommand.Parameters.AddWithValue("@PartCount", 0)
                                        sqlCommand.Parameters.AddWithValue("@PartRequired", 0)
                                        sqlCommand.Parameters.AddWithValue("@TotalPartCount", 0)
                                        sqlCommand.Parameters.AddWithValue("@FeedOverride", IIf(String.IsNullOrEmpty(machine.feedOverride), Nothing, machine.feedOverride))
                                        sqlCommand.Parameters.AddWithValue("@RapidOverride", Nothing)
                                        sqlCommand.Parameters.AddWithValue("@SpindleOverride", IIf(String.IsNullOrEmpty(machine.spindleOverride), Nothing, machine.spindleOverride))

                                        MySqlAccess.ExecuteNonQuery(sqlCommand)
                                    End If

                                End If

                            Else

                                Dim _curstatus = machine.Statut
                                Dim _time = machine.StatusDateSeconds
                                Dim _shift = shift
                                Dim _datetime = machine.StatusDateTime

                                Dim lastRecord = MySqlAccess.GetDataTable($"SELECT status, time, shift, date FROM CSI_machineperf.tbl_{machineTableName} ORDER BY Id DESC LIMIT 1;")

                                If lastRecord.Rows.Count > 0 Then
                                    _curstatus = lastRecord.Rows(0)("status")
                                    _time = lastRecord.Rows(0)("time")
                                    _shift = lastRecord.Rows(0)("shift")
                                    _datetime = lastRecord.Rows(0)("date")
                                End If

                                sqlCmd.Clear()
                                If _curstatus = machine.Statut And _shift = machine.Shift Then
                                    sqlCmd.Append($"UPDATE                                       ")
                                    sqlCmd.Append($"    CSI_machineperf.tbl_{ machineTableName } ")
                                    sqlCmd.Append($" SET                                         ")
                                    sqlCmd.Append($"    cycletime = 0                            ")
                                    sqlCmd.Append($" WHERE                                       ")
                                    sqlCmd.Append($"    status  = '{ _curstatus }'  AND          ")
                                    sqlCmd.Append($"    time    =  { _time }                   ; ")
                                Else

                                    Log.Info($"### A-26: MachineId: {machineId}, Status: {machine.Statut}, Time: {machine.StatusDateSeconds}")

                                    sqlCmd.Append($"INSERT IGNORE INTO                           ")
                                    sqlCmd.Append($"    CSI_machineperf.tbl_{ machineTableName } ")
                                    sqlCmd.Append($" (                                           ")
                                    sqlCmd.Append($"    status        ,                          ")
                                    sqlCmd.Append($"    time          ,                          ")
                                    sqlCmd.Append($"    cycletime     ,                          ")
                                    sqlCmd.Append($"    shift         ,                          ")
                                    sqlCmd.Append($"    date                                     ")
                                    sqlCmd.Append($" ) VALUES (                                  ")
                                    sqlCmd.Append($"    '{ machine.Statut }'           ,         ")
                                    sqlCmd.Append($"     { machine.StatusDateSeconds } ,         ")
                                    sqlCmd.Append($"     0                             ,         ")
                                    sqlCmd.Append($"     { machine.Shift }             ,         ")
                                    sqlCmd.Append($"    '{ machine.StatusDateString }'           ")
                                    sqlCmd.Append($" )                                           ")

                                    _curstatus = machine.Statut
                                    _time = machine.StatusDateSeconds
                                    _shift = shift
                                    _datetime = machine.StatusDateTime
                                End If

                                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                Try
                                    machinedic.Add(machineId, machine)
                                    realtime_dic(machineId).Rows.Add(_curstatus, _time, 0, _shift, _datetime)
                                Catch ex As Exception
                                    'Log.Error(ex)
                                End Try

                            End If
                            machinedic.Item(machineId) = machine
                        Next

                    Catch ex As Exception

                        Log.Error("Error getting live status from enet", ex)

                    End Try

                Else
                    Log.Debug("A-28")
                End If

            Catch ex As Exception
                Log.Error("Error in Sub fct_enet_livestatus", ex)
            End Try

            Thread.Sleep(500)
        End While

    End Sub

    Private Sub Init_RealTime_datatable()

        RealTime_datatable = New DataTable("values")
        RealTime_datatable.TableName = "values"
        Dim prime = RealTime_datatable.Columns.Add("MachineId", GetType(Integer))
        RealTime_datatable.Columns.Add("Machine", GetType(String))
        RealTime_datatable.Columns.Add("EnetMachine", GetType(String))
        RealTime_datatable.Columns.Add("MachineName", GetType(String))
        RealTime_datatable.Columns.Add("Label", GetType(String))
        RealTime_datatable.Columns.Add("Shift", GetType(String))
        RealTime_datatable.Columns.Add("Status", GetType(String))
        RealTime_datatable.Columns.Add("StatusColor", GetType(String))
        RealTime_datatable.Columns.Add("PartNumber", GetType(String))
        RealTime_datatable.Columns.Add("PartCount", GetType(String))
        RealTime_datatable.Columns.Add("Alarm", GetType(String))
        RealTime_datatable.Columns.Add("Operator", GetType(String))
        RealTime_datatable.Columns.Add("OperatorName", GetType(String))
        RealTime_datatable.Columns("Alarm").AllowDBNull = True
        RealTime_datatable.Columns.Add("CycleCount", GetType(String))
        RealTime_datatable.Columns.Add("LastCycle", GetType(String))
        RealTime_datatable.Columns.Add("IdealCycleTime", GetType(String))
        RealTime_datatable.Columns.Add("SetupTime", GetType(String))
        RealTime_datatable.Columns.Add("LoadingTime", GetType(String))
        RealTime_datatable.Columns.Add("IdealOtherTime", GetType(String))
        RealTime_datatable.Columns.Add("CurrentCycle", GetType(String))
        RealTime_datatable.Columns.Add("ElapsedTime", GetType(String))
        RealTime_datatable.Columns.Add("feedOverride", GetType(String))
        RealTime_datatable.Columns.Add("SpindleOverride", GetType(String))
        RealTime_datatable.Columns.Add("RapidOverride", GetType(String))
        RealTime_datatable.Columns.Add("MIN_Fover", GetType(String))
        RealTime_datatable.Columns.Add("MAX_Fover", GetType(String))
        RealTime_datatable.Columns.Add("MIN_Sover", GetType(String))
        RealTime_datatable.Columns.Add("MAX_Sover", GetType(String))
        RealTime_datatable.Columns.Add("MIN_Rover", GetType(String))
        RealTime_datatable.Columns.Add("MAX_Rover", GetType(String))
        RealTime_datatable.Columns.Add("Alarm_details", GetType(String))
        RealTime_datatable.Columns.Add("Utilization")
        RealTime_datatable.Columns.Add("Trend")
        RealTime_datatable.Columns.Add("Progression")
        RealTime_datatable.Columns.Add("MonitoringUnit")

        RealTime_datatable.PrimaryKey = New DataColumn() {prime}
    End Sub

    Private Shared Function LoadPortNumberFromDB() As String

        Dim port_number As String = ""

        Try
            Dim portT As New DataTable
            Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select port From csi_database.tbl_rm_port;", CSI_Library.CSI_Library.MySqlConnectionString)
            dadapter_name.Fill(portT)
            If portT.Rows.Count <> 0 And Not IsDBNull(portT.Rows(0)("port")) Then
                port_number = portT.Rows(0)("port")
            End If
        Catch ex As Exception

        End Try

        Return port_number

    End Function

#End Region

    Public Shared Function GetFileHash(ByVal fileName As String) As Byte()
        Dim sha1 As HashAlgorithm = HashAlgorithm.Create()
        Dim retVal As Byte()
        Using stream As FileStream = New FileStream(fileName, FileMode.Open, FileAccess.Read)
            retVal = sha1.ComputeHash(stream)
            stream.Close()
        End Using
        Return retVal
    End Function

    Public Shared Function compareFileHash(ByVal tmpHash As Byte(), ByVal tmpNewHash As Byte()) As Boolean
        Dim bEqual As Boolean = False
        If tmpHash Is Nothing OrElse tmpNewHash Is Nothing Then Return bEqual

        If tmpNewHash.Length = tmpHash.Length Then
            Dim i As Integer = 0

            While (i < tmpNewHash.Length) AndAlso (tmpNewHash(i) = tmpHash(i))
                i += 1
            End While

            If i = tmpNewHash.Length Then
                bEqual = True
            End If
        End If

        Return bEqual
    End Function

    Public real_time_dic_filled As Boolean = False

    Public Sub Fill_REALTIME_dic()
        Dim cnt As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
            ' This represent timeline length that user want to see on dashboards 0 = current shift, 1 = Whole day shift, 2 = ...... so on 
            Dim TLDisplay_Period As Integer = 0
            Dim enet_page As DataTable

            If CSI_Library.CSI_Library.isServer Then
                enet_page = RealTime_datatable.Copy
            Else
                enet_page = GetEnetPage()
            End If

            If enet_page.Rows.Count <> 0 Then

                realtime_dic.Clear()

                cnt.Open()
                Dim get_names As String = "SELECT * FROM csi_database.tbl_renamemachines"
                'Dim cmd As MySqlCommand = New MySqlCommand(get_names, cnt)

                Dim dadapter_name As New MySqlDataAdapter(get_names, cnt)
                Dim tbl_names As New DataTable
                dadapter_name.Fill(tbl_names)

                Dim index__ As Integer = 0

                Dim table_like As DataTable

                For Each row As DataRow In tbl_names.Rows

                    table_like = New DataTable
                    dadapter_name = New MySqlDataAdapter("SHOW TABLES FROM CSI_machineperf Like 'tbl_" & row(0) & "'", cnt)
                    dadapter_name.Fill(table_like)

                    If Not realtime_dic.ContainsKey(row(1)) Then
                        realtime_dic.Add(row(1), New DataTable)
                        realtime_dic(row(1)).Columns.Add("status", GetType(String))
                        realtime_dic(row(1)).Columns.Add("time", GetType(Integer))
                        realtime_dic(row(1)).Columns.Add("cycletime", GetType(Integer))
                        realtime_dic(row(1)).Columns.Add("shift", GetType(Integer))
                        realtime_dic(row(1)).Columns.Add("date", GetType(DateTime))

                        Dim keys(2) As DataColumn
                        keys(0) = realtime_dic(row(1)).Columns(0)
                        keys(1) = realtime_dic(row(1)).Columns(1)
                        realtime_dic(row(1)).PrimaryKey = keys
                    End If

                    If table_like.Rows.Count <> 0 Then
                        realtime_dic(row(1)).Rows.Clear()


                        Dim Tbl_buffer As New DataTable
                        Tbl_buffer.Columns.Add("status", GetType(String))
                        Tbl_buffer.Columns.Add("time", GetType(Integer))
                        Tbl_buffer.Columns.Add("cycletime", GetType(Integer))
                        Tbl_buffer.Columns.Add("shift", GetType(Integer))
                        Tbl_buffer.Columns.Add("date", GetType(DateTime))

                        If TLDisplay_Period = 0 Then
                            'Current Shift
                            dadapter_name = New MySqlDataAdapter($"SET @current_shift := (SELECT MAX(shift) From CSI_machineperf.tbl_{ row(0) }); Select * From CSI_machineperf.tbl_{ row(0) } Where shift = @current_shift AND status NOT LIKE '\_%';", cnt)
                        ElseIf TLDisplay_Period = 1 Then 'NOT WORKING
                            'Whole DAy
                            dadapter_name = New MySqlDataAdapter($"SELECT * from CSI_machineperf.tbl_{ row(0) } WHERE status NOT LIKE '\_%';", cnt)
                        End If

                        dadapter_name.Fill(Tbl_buffer)

                        If Tbl_buffer.Rows.Count <> 0 Then

                            realtime_dic(row(1)) = Tbl_buffer.Copy

                            Dim shift As DataRow() = enet_page.Select("machine = '" & row(1) & "'")

                            If shift.Count > 0 Then
                                index__ = If(realtime_dic(row(1)).Rows.Count > 0, realtime_dic(row(1)).Rows.Count - 1, 0)

                                If shift(0).Item(1) <> realtime_dic(row(1)).Rows(0).Item(3) Or DateDiff(DateInterval.Hour, Now().Date, realtime_dic(row(1)).Rows(index__).Item(4)) > 24 Then

                                    If realtime_dic(row(1)).Rows.Count <> 0 Then
                                        realtime_dic(row(1)).Rows.Clear()
                                    End If

                                End If
                            End If
                        End If
                    End If

                    If realtime_dic.ContainsKey(row(1)) Then

                        index__ = If(realtime_dic(row(1)).Rows.Count > 0, realtime_dic(row(1)).Rows.Count - 1, 0)
                        If realtime_dic(row(1)).Rows.Count <> 0 Then
                            Dim DISCONNECTION_TIME_ As Integer = realtime_dic(row(1)).Rows.Item(index__).Item("time") + realtime_dic(row(1)).Rows.Item(index__).Item("cycletime")


                            Dim DISCONNECTED_DATE As Date = realtime_dic(row(1)).Rows.Item(index__).Item("date")
                            DISCONNECTED_DATE = DISCONNECTED_DATE.AddSeconds(realtime_dic(row(1)).Rows.Item(index__).Item("cycletime"))

                            Dim str_date As String = Format(DISCONNECTED_DATE, "yyyy-MM-dd HH:mm:ss")

                            Dim cycletime As Integer = Now.Subtract(DISCONNECTED_DATE).Seconds + Now.Subtract(DISCONNECTED_DATE).Minutes * 60 + Now.Subtract(DISCONNECTED_DATE).Hours * 3600
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            csi_lib.LogServiceError(" Unable to retrieve the timeline data : " & ex.Message, 0)

        Finally
            real_time_dic_filled = True
            If cnt.State = ConnectionState.Open Then
                cnt.Close()
            End If
        End Try
    End Sub

    Dim compute_trends_timer As New System.Timers.Timer()

    Private Sub compute_trends()
        compute_trends_timer = New System.Timers.Timer(1000 * 60 * 1) '1 minutes
        AddHandler compute_trends_timer.Elapsed, AddressOf Lanch_compute
        compute_trends_timer.AutoReset = False
        compute_trends_timer.Start()
    End Sub

    Dim trend_dic As New Dictionary(Of String, Integer)

    Private Sub Lanch_compute()
        Compute()
        compute_trends_timer.Start()
    End Sub

    Dim number As Integer = 0

    Private Sub Compute()

        Dim Total_prod_time As Object = 0
        Dim prod_time As Object = 0
        Dim trend_percent As Integer = 0 '%
        Dim total_shift As Integer = 0 'in sec
        Dim percent_shift As Integer = 0
        Dim percent_compared As Integer = 0

        Dim prod_mode As Boolean = False
        Dim percent As Integer = 20
        Dim comparto As String = ""
        Try
            If general_config.ContainsKey("trends") Then
                Dim s() As String = general_config("trends").Split(",")
                If s.Count = 3 Then
                    percent = s(1)
                    comparto = s(2)
                    prod_mode = False
                Else
                    prod_mode = True
                End If
            End If


            For Each key In realtime_dic.Keys
                If realtime_dic(key).Rows.Count <> 0 Then

                    prod_time = 0

                    If prod_mode = False Then

                        'total time in sec
                        total_shift = realtime_dic(key).Rows(realtime_dic(key).Rows.Count - 1).Item("time") - realtime_dic(key).Rows(0).Item("time") + realtime_dic(key).Rows(realtime_dic(key).Rows.Count - 1).Item("cycletime")
                        'csi_lib.LogServerError("total_shift ::" & total_shift.ToString(), 0)
                        'sum total prod
                        Total_prod_time = realtime_dic(key).Compute("SUM(cycletime)", "status = 'CYCLE ON'")
                        'csi_lib.LogServerError("Total_prod_time  ::" & Total_prod_time.ToString(), 0)

                        'find the status that began before the time limit and ended after
                        'csi_lib.LogServerError("Error while computing the trends : And Count Value is :" & (realtime_dic(key).Rows(realtime_dic(key).Rows.Count - 1).Item("time")).ToString(), 0)
                        ' This is old One :::: Dim limit_time() As DataRow = realtime_dic(key).Select("time > '" & (realtime_dic(key).Rows(realtime_dic(key).Rows.Count - 1).Item("time") - (total_shift * percent / 100)).ToString() & "'")
                        'I change here
                        Dim limit_time() As DataRow = realtime_dic(key).Select("time > '" & Convert.ToInt32(realtime_dic(key).Rows(realtime_dic(key).Rows.Count - 1).Item("time") - (total_shift * percent / 100)) & "'")

                        Dim status As String
                        Dim time As Integer
                        Dim ctime As Integer

                        If limit_time.Count <> 0 Then
                            If realtime_dic(key).Rows.Count = limit_time.Count Then
                                status = realtime_dic(key).Rows(0).Item("status")
                                time = realtime_dic(key).Rows(0).Item("time")
                                ctime = realtime_dic(key).Rows(0).Item("cycletime")
                            Else
                                status = realtime_dic(key).Rows(realtime_dic(key).Rows.Count - (limit_time.Count + 1)).Item("status")
                                time = realtime_dic(key).Rows(realtime_dic(key).Rows.Count - (limit_time.Count + 1)).Item("time")
                                ctime = realtime_dic(key).Rows(realtime_dic(key).Rows.Count - (limit_time.Count + 1)).Item("cycletime")
                            End If

                            If general_config.ContainsKey("prod") Then
                                If general_config("prod").Contains(status) Then
                                    Dim Now_sec As Integer = Now.Second + Now.Minute * 60 + Now.Hour * 3600
                                    Dim percent_time As Integer = Now_sec - total_shift * percent / 100
                                    prod_time = ctime - (percent_time - time)
                                    ' prod_time = ctime - ((realtime_dic(key).Rows(realtime_dic(key).Rows.Count - 1).Item("time") + realtime_dic(key).Rows(realtime_dic(key).Rows.Count - 1).Item("cycletime") - total_shift * percent / 100) - time)
                                Else
                                    'do nothing
                                End If
                            End If
                        End If

                        '+setup
                        Dim TOTAL_SETUP_TIME As Object = 0
                        Dim SETUP_TIME As Object = 0
                        If general_config.ContainsKey("prod") Then
                            If general_config("prod").Contains("SETUP") Then
                                TOTAL_SETUP_TIME = realtime_dic(key).Compute("SUM(cycletime)", "status = 'SETUP'")
                                'I change here
                                SETUP_TIME = realtime_dic(key).Compute("SUM(cycletime)", "time > '" & Convert.ToInt32(realtime_dic(key).Rows(realtime_dic(key).Rows.Count - 1).Item("time") - (total_shift * percent / 100)) & "' AND status = 'SETUP'")
                                prod_time = SETUP_TIME
                                Total_prod_time = If(IsDBNull(Total_prod_time), 0, Total_prod_time) + If(IsDBNull(TOTAL_SETUP_TIME), 0, TOTAL_SETUP_TIME)
                            End If
                        End If

                        '+loading 
                        Dim TOTAL_LOADING_TIME As Object = 0
                        Dim LOADING_TIME As Object = 0
                        If general_config.ContainsKey("prod") Then
                            If general_config("prod").Contains("LOADING") Then
                                TOTAL_LOADING_TIME = realtime_dic(key).Compute("SUM(cycletime)", "status = 'LOADING'")
                                'I change here
                                LOADING_TIME = realtime_dic(key).Compute("SUM(cycletime)", "time > '" & Convert.ToInt32(realtime_dic(key).Rows(realtime_dic(key).Rows.Count - 1).Item("time") - (total_shift * percent / 100)) & "' AND status = 'LOADING_TIME'")
                                prod_time = If(IsDBNull(prod_time), 0, prod_time) + If(IsDBNull(LOADING_TIME), 0, LOADING_TIME)
                                Total_prod_time = If(IsDBNull(Total_prod_time), 0, Total_prod_time) + If(IsDBNull(TOTAL_LOADING_TIME), 0, TOTAL_LOADING_TIME)
                            End If
                        End If

                        ' 20 = var

                        'sum last % , prod
                        'I change here
                        'This line is old one ::: Dim CON_TIME As Object = realtime_dic(key).Compute("SUM(cycletime)", "time > '" & Convert.ToInt32(realtime_dic(key).Rows(realtime_dic(key).Rows.Count - 1).Item("time") - (total_shift * percent / 100)) & "' AND status = 'CYCLE ON'")
                        Dim CON_TIME As Object = realtime_dic(key).Compute("SUM(cycletime)", "time > '" & Convert.ToInt32(realtime_dic(key).Rows(realtime_dic(key).Rows.Count - 1).Item("time") - (total_shift * percent / 100)) & "' AND status = 'CYCLE ON'")
                        prod_time = If(IsDBNull(prod_time), 0, prod_time) + If(IsDBNull(CON_TIME), 0, CON_TIME)

                        If Not IsDBNull(Total_prod_time) And Not IsDBNull(prod_time) And total_shift <> 0 Then
                            If Total_prod_time <> 0 Then

                                percent_shift = Total_prod_time * 100 / total_shift
                                percent_compared = prod_time * 100 / (total_shift * percent / 100)

                                If percent_shift <> 0 Then
                                    trend_percent = percent_compared * 100 / percent_shift
                                    If trend_percent > 100 Then trend_percent = 100
                                    If trend_percent < 0 Then trend_percent = 0
                                Else
                                    trend_percent = 0
                                End If

                                If trend_dic.ContainsKey(key) Then
                                    trend_dic(key) = trend_percent
                                Else
                                    trend_dic.Add(key, trend_percent)
                                End If
                            End If
                        Else
                            If trend_dic.ContainsKey(key) Then
                                '  trend_dic(key) = trend_percent
                            Else
                                trend_dic.Add(key, 0)
                            End If
                        End If

                    Else
                        ' trend_dic.Add(key, give_CON_percent(key))
                        If trend_dic.ContainsKey(key) Then
                            trend_dic(key) = give_CON_percent(key)
                        Else
                            trend_dic.Add(key, give_CON_percent(key))
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Log.Error("Error while computing the trends", ex)
        End Try

    End Sub

    Private Function give_CON_percent(mch As String) As Integer

        Dim dv As DataView = RealTime_datatable.DefaultView
        dv.RowFilter = "machine='" & mch & "'"


        Dim util As String()
        If dv.Count <> 0 Then
            util = Convert.ToString(dv(0)("Utilization")).Split(",")
            If util.Count <> 0 Then
                util(0) = util(0).Replace("{""cOn"":", "")
                util(0) = util(0).Replace("{""CON"":", "")
            End If
            If util(0) = "" Then
                Return 0
            Else
                Return CInt(util(0))
            End If
        Else
            Return 0
        End If

    End Function

#Region "get_enet_livestatus"

    Public enet_page As String
    Public enet_page2 As String

    Private Function GetEnetPage() As DataTable
        ' Try
        Dim table As New DataTable
        table.TableName = "values"
        Dim prime = table.Columns.Add("Machine", GetType(String))
        table.Columns.Add("MachineId", GetType(Integer))
        table.Columns.Add("EnetMachine", GetType(String))
        table.Columns.Add("MachineName", GetType(String))
        table.Columns.Add("Label", GetType(String))
        table.Columns.Add("Shift", GetType(String))
        table.Columns.Add("Status", GetType(String))
        table.Columns.Add("NextStatus", GetType(String))
        table.Columns.Add("StatusColor", GetType(String))
        table.Columns.Add("StatusDatetime", GetType(String))
        table.Columns.Add("PartNumber", GetType(String))
        table.Columns.Add("PartCount", GetType(String))
        table.Columns.Add("PartsRequired", GetType(String))
        table.Columns.Add("OperatorRefId", GetType(String))
        table.Columns.Add("OperatorName", GetType(String))
        table.Columns.Add("Alarm", GetType(String))
        table.Columns("Alarm").AllowDBNull = True
        table.Columns.Add("CycleCount", GetType(String))
        table.Columns.Add("LastCycle", GetType(String))
        table.Columns.Add("IdealCycleTime", GetType(String))
        table.Columns.Add("SetupTime", GetType(String))
        table.Columns.Add("LoadingTime", GetType(String))
        table.Columns.Add("IdealOtherTime", GetType(String))
        table.Columns.Add("CurrentCycle", GetType(String))
        table.Columns.Add("ElapsedTime", GetType(String))
        table.Columns.Add("FeedOverride", GetType(String))
        table.Columns.Add("SpindleOverride", GetType(String))
        table.Columns.Add("RapidOverride", GetType(String))
        table.Columns.Add("MIN_Fover", GetType(String))
        table.Columns.Add("MAX_Fover", GetType(String))
        table.Columns.Add("MIN_Sover", GetType(String))
        table.Columns.Add("MAX_Sover", GetType(String))
        table.Columns.Add("MIN_Rover", GetType(String))
        table.Columns.Add("MAX_Rover", GetType(String))
        table.Columns.Add("Alarm_details", GetType(String))

        table.Columns.Add("Comment", GetType(String))
        table.Columns.Add("Utilization")
        table.Columns.Add("Trend")
        table.Columns.Add("Progression")
        table.Columns.Add("Pallet")
        table.Columns.Add("MonitoringUnit", GetType(String))

        table.PrimaryKey = New DataColumn() {prime}

        'Format URI
        Dim uri As String = enetip
        If uri.StartsWith("http") Then
        Else
            uri = "http://" & uri
        End If

        Dim targetURI As New Uri(uri), page As String = ""
        Dim Request As System.Net.HttpWebRequest
        ' table.Columns.Add("palette", GetType(String))

        Try
            Log.Debug($"LoadMachineStatus()")

            eNetServer.Instance.LoadMachinesStatus()
            Dim machinesStatus As List(Of eNetDashboardMachine) = eNetServer.Instance.GetMachinesStatus()
            Dim dtMachines As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.view_machines_settings_values;")

            enet_page2 = eNetServer.Instance.GetHtmlDashboard()

            Request = DirectCast(HttpWebRequest.Create(targetURI), System.Net.HttpWebRequest)
            With Request
                .Timeout = 6000
                .Method = "POST"
                '.Method = "GET"
                .KeepAlive = True
                .ContentLength =
                        .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"
            End With

            Try
                If (Request.GetResponse().ContentLength > 0) Then
                    Dim str As New System.IO.StreamReader(Request.GetResponse().GetResponseStream())
                    page = (str.ReadToEnd)
                    enet_page = page
                    'If(page.IndexOf(""))'Detect BAD eHUB
                    str.Close()
                End If
            Catch ex As Exception
            End Try

            For Each machine As eNetDashboardMachine In machinesStatus

                Dim rowMachine As DataRow = dtMachines.Rows.Cast(Of DataRow).Where(Function(r) r.Item("EnetMachineName").ToString() = machine.MachineName).FirstOrDefault()
                Dim newRow As DataRow = table.NewRow
                Dim machineId As Integer = rowMachine.Item("Id")
                Dim currentStatus As String = machine.Status
                Dim nextStatus As String = IIf(Not IsDBNull(rowMachine.Item("NextStatus")), rowMachine.Item("NextStatus"), "")

                newRow("Machine") = machine.MachineName     ' rowMachine.Item("machine_name")
                newRow("MachineId") = machineId
                newRow("EnetMachine") = machine.MachineName
                newRow("MachineName") = rowMachine.Item("machine_name")
                newRow("Label") = rowMachine.Item("machine_label")
                newRow("ElapsedTime") = TimeSpan.FromSeconds(machine.ElapsedTime).ToString("c")
                newRow("CurrentCycle") = TimeSpan.FromSeconds(machine.CurrentCycle).ToString("c")
                newRow("Shift") = machine.Shift
                newRow("Status") = currentStatus
                newRow("NextStatus") = nextStatus
                newRow("StatusColor") = StatusColor.GetColor(machine.Status.ToUpper())
                newRow("StatusDatetime") = machine.StatusDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                newRow("FeedOverride") = IIf(machine.FeedOverride >= 0, machine.FeedOverride, Nothing)
                newRow("SpindleOverride") = IIf(machine.SpindleOverride >= 0, machine.SpindleOverride, Nothing)
                newRow("LastCycle") = TimeSpan.FromSeconds(machine.LastCycle).ToString("c")
                newRow("CycleCount") = eNETTempFiles.GetCycleCount(machineId)
                newRow("PartNumber") = machine.PartNumber
                newRow("OperatorRefId") = machine.OperatorRefId
                newRow("Comment") = machine.Comment

                dtMachines.Rows.Remove(rowMachine)

                newRow("IdealCycleTime") = 0
                newRow("SetupTime") = 0
                newRow("LoadingTime") = 0
                newRow("IdealOtherTime") = 0

                If Not String.IsNullOrEmpty(machine.PartNumber) Then
                    Dim dt = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.tbl_historypartnumbers WHERE MachineId = {machineId} AND PartNumber = '{machine.PartNumber}' ORDER BY Id DESC LIMIT 1;")
                    If dt.Rows.Count > 0 Then
                        newRow("IdealCycleTime") = dt.Rows(0)("CycleTime")
                        newRow("SetupTime") = dt.Rows(0)("SetupTime")
                        newRow("LoadingTime") = dt.Rows(0)("PartLoad")
                    End If
                End If

                If csi_lib.MCHS_.ContainsKey(machineId) Then

                    Dim mch = csi_lib.MCHS_(machineId)

                    newRow("FeedOverride") = IIf(Not String.IsNullOrEmpty(mch.FeedRate_Variable), mch.FeedRate_Value, Nothing)
                    newRow("SpindleOverride") = IIf(Not String.IsNullOrEmpty(mch.Spindle_Variable), mch.Spindle_Value, Nothing)
                    newRow("RapidOverride") = IIf(Not String.IsNullOrEmpty(mch.Rapid_Variable), mch.Rapid_Value, Nothing)
                    newRow("PartCount") = IIf(Not String.IsNullOrEmpty(mch.PartCount_Variable), mch.PartCount_Value, Nothing)
                    newRow("PartsRequired") = IIf(Not String.IsNullOrEmpty(mch.RequiredParts_Variable), mch.RequiredParts_Value, Nothing)

                    newRow("MIN_Fover") = mch.Min_Fover
                    newRow("MAX_Fover") = mch.Max_Fover
                    newRow("MIN_Sover") = mch.Min_Sover
                    newRow("MAX_Sover") = mch.Max_Sover
                    newRow("MIN_Rover") = mch.Min_Rover
                    newRow("MAX_Rover") = mch.Max_Rover
                End If


                If MonitoringBoardsService.MonitoringBoardsStatus Is Nothing Then
                    Log.Info($"MonitoringBoardsService.monitoringBoardsStatus is NULL")
                    MonitoringBoardsService.MonitoringBoardsStatus = New List(Of MonitoringBoardStatus)()
                End If

                If MonitoringBoardsService.MonitoringBoardsStatus.Any(Function(s) s.MachineId = machineId) Then
                    Dim json = JsonConvert.SerializeObject(MonitoringBoardsService.MonitoringBoardsStatus.First(Function(s) s.MachineId = machineId))
                    newRow("MonitoringUnit") = json
                End If

                table.Rows.Add(newRow)

                If Not String.IsNullOrEmpty(nextStatus) Then

                    If currentStatus = "CYCLE OFF" Then
                        Dim enetMachine As eNetMachineConfig = eNetServer.Machines.FirstOrDefault(Function(m) m.MachineName = machine.MachineName)
                        Dim cmd = enetMachine.GetCommand(nextStatus)
                        eNetServer.SendStatusToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, cmd)
                    End If

                    If Not currentStatus.Equals("CYCLE ON") Then
                        MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.tbl_machines_settings SET NextStatus = '' WHERE MachineId = {machineId}")
                    End If

                End If

            Next

        Catch ex As Exception

            Log.Error(ex)

        End Try

        If (table.Rows.Count <= 0) Then
            TestEnetHTTP()
        End If

        Return table

    End Function

    Private Sub TestEnetHTTP()

        enethttploaded = False
        Try
            'Format URI
            Dim uri As String = enetip '& "/?SEL=-1&Refresh=Refresh"
            If Not (uri = Nothing) Then


                If uri.StartsWith("http") Then
                Else
                    uri = "http://" & uri
                End If

                Dim targetURI As New Uri(uri), page As String = ""
                Dim Request As System.Net.HttpWebRequest

                Request = DirectCast(HttpWebRequest.Create(targetURI), System.Net.HttpWebRequest)
                With Request
                    .Timeout = 6000
                    '.Method = "POST"
                    .Method = "GET"
                    .KeepAlive = True
                    .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"
                End With

                If (Request.GetResponse().ContentLength > 0) Then
                    Dim str As New System.IO.StreamReader(Request.GetResponse().GetResponseStream())
                    page = (str.ReadToEnd)
                    str.Close()
                End If

                If (page.Length > 0) Then
                    csi_lib.LogServiceError(vbCrLf & "Enet loaded. " & vbCrLf, 0)
                    enethttploaded = True
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    'Enet data structure
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

#Region "update_enet_livestatus"

    Class Legend
        Public _status As String
        Public _color As String
        Public _width As Double


        Public Sub New(status As String, color As String, width As Double)
            _status = status
            _color = color
            _width = width
        End Sub

        Public Sub New()

        End Sub

        Public Function LegendStr() As String
            Return _status + ":" + _color + ":" + _width.ToString(CultureInfo.InvariantCulture) + ";"
        End Function

    End Class

    Private Sub DisconnectEnetLivestatus()
        Dim cnt As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try
            cnt.Open()
            Dim cmd2 As New MySqlCommand("UPDATE CSI_database.tbl_livestatut  SET  statut_ = 'NOT CONNECTED',updatedat_=NOW(),lastcycle_='00:00:00',currentcycle_='00:00:00',elapsedtime_='00:00:00';", cnt)
            cmd2.ExecuteNonQuery()
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            cnt.Close()
        End Try
    End Sub

#End Region

#Region "mtc_livestatus"

    Dim intAddress

    Private Sub fct_mtc_livestatus()

        Try

            Dim temp_mtclivestatus As New DataTable

            Dim address = IPAddress.Parse(CSI_Library.CSI_Library.LocalHostIP)
            Dim bytes = address.GetAddressBytes()
            Dim inProduction As Boolean
            Dim production = "CYCLE ON;CYCLE OFF;LOCKED"
            Dim MonitBoardId As Integer

            Array.Reverse(bytes) 'flip big-endian(network order) To little-endian
            intAddress = BitConverter.ToUInt32(bytes, 0)

            If useUDP Then

                Try
                    Array.Reverse(bytes) 'flip big-endian(network order) To little-endian
                    Dim intAddress = BitConverter.ToUInt32(bytes, 0)

                    Dim openUDP = "REMOTE_VIEW," + intAddress.ToString()

                    SendOverUDP(eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, openUDP)

                Catch ex As Exception
                    Log.Error("Open UDP connection", ex)
                End Try

            End If

            GetMTCMachinesIp()

            For Each mach In dtMtcMachines.Rows

                Dim enetMachName As String = ""
                Dim enetMachine As eNetMachineConfig
                Dim machineId As Integer = 0
                Dim port As Integer = 0

                Try

                    enetMachName = mach("eNETMachineName")
                    enetMachine = eNetServer.Machines.FirstOrDefault(Function(m) m.MachineName = enetMachName)
                    machineId = mach("MachineId")

                    csi_lib.MCHS_(machineId).MachineIp = mach("MachineIp")

                    Integer.TryParse(mach("FocasPort"), port)

                    csi_lib.MCHS_(machineId).MachinePort = port

                    If Not enetMachine.Cmd_Others.ContainsKey("NOeMONITORING") Then
                        eNetServer.Instance.AddStatusMachine(enetMachine.EnetPos, "NOeMONITORING", "NOeMONITORING")
                    End If

                Catch ex As Exception

                    Log.Error($"C-04 - {enetMachName}, {machineId.ToString()}, {port}, {IsNothing(enetMachine)}", ex)
                End Try

            Next

            Try

                dtMonitoringUnits = MySqlAccess.GetDataTable("SELECT * FROM monitoring.monitoringboards WHERE Target <> '' AND Target <> '0' AND Target <> NULL;")

                For Each unit In dtMonitoringUnits.Rows

                    Dim enetMachName As String = unit("eNETMachineName")
                    Dim enetMachine As eNetMachineConfig = eNetServer.Machines.FirstOrDefault(Function(m) m.MachineName = enetMachName)

                    If Not enetMachine.Cmd_Others.ContainsKey("LOCKED") Then
                        eNetServer.Instance.AddStatusMachine(enetMachine.EnetPos, "LOCKED", "LOCKED")
                    End If
                Next

            Catch ex As Exception
                Log.Error(ex)
            End Try

            Dim currentPallet As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)()
            Dim saveCounter As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)()
            Dim logCounter As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)()

            While Not _shutdownEvent.WaitOne(0)

                Try

                    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

                    temp_mtclivestatus.Clear()
                    temp_mtclivestatus = evaluate_status()

                Catch ex As Exception

                    Log.Error("ERROR getting data from MTConnect agent.", ex)
                    DisconnectMtcLivestatus()

                End Try

                Dim errorPoint = 0

                Try
                    If RealTime_datatable.Rows.Count > 0 Then

                        Dim snapshot = RealTime_datatable.Copy()
                        Dim keys(1) As DataColumn

                        keys(0) = snapshot.Columns(2)
                        snapshot.PrimaryKey = keys

                        Dim cmd_sent = False

                        For Each mtcMachLiveStatus In temp_mtclivestatus.Rows

                            errorPoint = 1

                            Dim machineId As Integer = mtcMachLiveStatus("machineId")
                            Dim machname As String = mtcMachLiveStatus("machineName")

                            errorPoint = 10000 + machineId

                            If Not saveCounter.ContainsKey(machineId) Then
                                saveCounter.Add(machineId, 51)
                                logCounter.Add(machineId, 51)
                            End If

                            Dim old_row As DataRow = snapshot.Rows.Find(machname)

                            MonitBoardId = mtcMachLiveStatus("MonitBoardId")

                            errorPoint = 20000 + machineId

                            If old_row Is Nothing And snapshot.Rows.Count > 0 Then
                                Log.Debug($"== Old row is nothing. Machine: {machineId} - {snapshot.Rows(0)(0)} - {snapshot.Rows(0)(1)} - {snapshot.Rows(0)(2)} - {snapshot.Rows(0)(3)}")
                            End If

                            Dim enetMachine As eNetMachineConfig = eNetServer.Machines.FirstOrDefault(Function(m) m.MachineName = machname)

                            If enetMachine Is Nothing Or String.IsNullOrEmpty(enetMachine.MachineName) Then
                                Log.Error($"{machname} not found in eNET machines list.")
                                Throw New Exception($"{machname} not found in eNET machines list.")
                            End If

                            errorPoint = 30000 + machineId

                            Try
                                'Get Ftp config for this machine
                                Dim machconfig = ftpmachlist.Find(Function(x) x.MachineName.Equals(machname))
                                Dim cmd As String = String.Empty

                                'send partno cmd
                                If machconfig IsNot Nothing Or useUDP Then

                                    errorPoint = 40000 + machineId

                                    Dim new_status As String = Nothing
                                    Dim old_status As String = Nothing

                                    If Not IsDBNull(mtcMachLiveStatus("status")) Then
                                        new_status = mtcMachLiveStatus("status")
                                        If new_status = "CYCLE START DISABLE" Then
                                            new_status = "LOCKED"
                                        End If
                                    End If

                                    If old_row IsNot Nothing Then
                                        If Not IsDBNull(old_row("status")) Then
                                            old_status = old_row("status")
                                            inProduction = production.Contains(old_status)
                                        End If
                                    End If

                                    Dim isMonitoring As Boolean = False
                                    Dim isSensorAvailable As Boolean = False
                                    Dim isMonitOverrideOn As Boolean = False
                                    Dim isCycleStartDisableOn As Boolean = False
                                    Dim IsCriticalStopAllowed As Boolean = False
                                    Dim logMCS As Boolean = False
                                    Dim csdOnSetup As Boolean = False
                                    Dim conDuringSetup As Boolean = False
                                    Dim new_pallet As String = ""
                                    Dim old_pallet As String = ""

                                    If new_status = "Conflicting stat" Then new_status = "CYCLE ON"

                                    Dim monitoringBoard = MonitoringBoardsService.MonitoringBoardsStatus.FirstOrDefault(Function(b) b.MachineId = machineId)

                                    errorPoint = 50000 + machineId

                                    Try
                                        If monitoringBoard IsNot Nothing Then
                                            isMonitoring = monitoringBoard.IsMonitoring
                                            isSensorAvailable = monitoringBoard.IsSensorAvailable
                                        End If
                                    Catch ex As Exception
                                        isMonitoring = False
                                    End Try

                                    If MonitBoardId > 0 And monitoringBoard IsNot Nothing Then

                                        errorPoint = 60000 + machineId

                                        isMonitOverrideOn = monitoringBoard.IsOverride
                                        isCycleStartDisableOn = monitoringBoard.IsCSD

                                        csdOnSetup = csi_lib.MCHS_(machineId).CsdOnSetup
                                        conDuringSetup = csi_lib.MCHS_(machineId).COnDuringSetup
                                        IsCriticalStopAllowed = csi_lib.MCHS_(machineId).EnableMCS

                                        'Send pallet to Monitoring Boards Service
                                        new_pallet = IIf(Not IsDBNull(mtcMachLiveStatus("Pallet")), mtcMachLiveStatus("Pallet"), "")

                                        old_pallet = monitoringBoard.CurrentPallet
                                        If csi_lib.MCHS_(machineId).Sensors.Count > 0 Then
                                            old_pallet = csi_lib.MCHS_(machineId).Sensors(0).CurrentPallet
                                        ElseIf MonitBoardId > 0 Then
                                            Log.Info($"****> No Sensor available, machine: {machname} ({machineId})")
                                        End If

                                        If Not currentPallet.ContainsKey(machineId) Then
                                            currentPallet.Add(machineId, old_pallet)
                                        ElseIf Not String.IsNullOrEmpty(old_pallet) Then
                                            currentPallet(machineId) = old_pallet
                                        Else
                                            old_pallet = currentPallet(machineId)
                                        End If


                                        If logCounter(machineId) > 50 Then
                                            Log.Info($"********> Machine: {machname} ({machineId}) - MB: { MonitBoardId } - Status: { old_status } - New Status: { new_status } - CSD: { isCycleStartDisableOn } - MO: { isMonitOverrideOn } - Pallet: {old_pallet} / {new_pallet}")
                                            logCounter(machineId) = 0
                                        End If
                                        logCounter(machineId) += 1

                                    End If

                                    '#End If

                                    Dim saveChange As Boolean = False

                                    'send status cmd
                                    If Not String.IsNullOrEmpty(new_status) And (new_status <> old_status Or MonitBoardId > 0) Then

                                        errorPoint = 70000 + machineId

                                        cmd_sent = False
                                        cmd = enetMachine.GetCommand(new_status)
                                        'cmd = enetMachine.GetCommand(new_status, old_status)

                                        saveChange = True

                                        If MonitBoardId > 0 And isMonitoring And isSensorAvailable Then

                                            errorPoint = 80000 + machineId

                                            'Dim monitBoardStatus As MonitoringBoardStatus = csi_lib.MCHS_(machineId).MonitBoardStatus
                                            Dim monitBoardStatus As MonitoringBoardStatus = MonitoringBoardsService.MonitoringBoardsStatus.FirstOrDefault(Function(b) b.MachineId = machineId)

                                            Dim activatedCriticalStop = monitBoardStatus.ActivatedCriticalStop

                                            Dim IsPressureOkay = monitBoardStatus.CurrentPressure > monitBoardStatus.CriticalPressure

                                            If Not IsPressureOkay Then

                                                Log.Info($"======>>> { machname } Delay Critical Stop. ===> Delay: {monitBoardStatus.Delay}, Started Delay: { monitBoardStatus.StartedDelay }")

                                                Dim timeNow = Now.TimeOfDay

                                                If monitBoardStatus.Delay > 0 And Not monitBoardStatus.StartedDelay Then

                                                    Log.Info($"======>>> { machname } Delay Critical Stop. ===> 1")
                                                    monitBoardStatus.StartedDelay = True
                                                    monitBoardStatus.StartCriticalStop = timeNow

                                                ElseIf monitBoardStatus.Delay > 0 Then

                                                    Dim secsNow = Now.TimeOfDay.TotalSeconds
                                                    Dim timeStart = monitBoardStatus.StartCriticalStop.TotalSeconds

                                                    activatedCriticalStop = (secsNow - timeStart) > monitBoardStatus.Delay
                                                    Log.Info($"======>>> { machname } Delay Critical Stop. ===> 2, activatedCriticalStop: { activatedCriticalStop } => {secsNow}, {timeStart}")

                                                Else

                                                    Log.Info($"======>>> { machname } Delay Critical Stop. ===> 3")
                                                    activatedCriticalStop = True

                                                End If
                                            Else
                                                monitBoardStatus.StartedDelay = False
                                            End If

                                            Log.Info($"======>>> { machname } Delay Critical Stop. ===> 4 - {monitBoardStatus.StartedDelay} - AactivatedCriticalStop: { activatedCriticalStop }")
                                            Log.Info($"============> {machname} ({machineId}) - MB: { MonitBoardId } - IsMonitoring: { isMonitoring } - Status: { old_status } - New Status: { new_status } - CSD: { isCycleStartDisableOn } - MO: { isMonitOverrideOn }")
                                            Log.Info($"============> {machname} ({machineId}) - MB: { monitBoardStatus.BoardId } - Pallet: { monitBoardStatus.CurrentPallet } - Pressure: { monitBoardStatus.CurrentPressure } - Critical Pressure: { monitBoardStatus.CriticalPressure } - ActivedCS: {activatedCriticalStop} - PressureOK: {IsPressureOkay}")

                                            'If old_status = "NO eMONITORING" And (new_status = "CYCLE ON" Or new_status = "CYCLE OFF") And Not isMonitOverrideOn And Not isCycleStartDisableOn Then
                                            If old_status.Contains("eMONIT") And (new_status = "CYCLE ON" Or new_status = "CYCLE OFF") And IsPressureOkay Then

                                                If new_status = "CYCLE ON" Then
                                                    Log.Info("== 01a ==> The machine's status will be changed to CYCLE ON .")

                                                    eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, cmd)
                                                Else
                                                    Log.Info("== 01b ==> The machine's status will be changed to PRODUCTION and CYCLE OFF.")

                                                    eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, enetMachine.Cmd_PROD)
                                                    eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, cmd)
                                                End If

                                                MonitoringBoardsService.SetMonitoringOverride(MonitBoardId, False)
                                                MonitoringBoardsService.SetCycleStartDisable(MonitBoardId, False)

                                                monitBoardStatus.ActivatedCriticalStop = False

                                            ElseIf new_status = "LOCKED" And inProduction And Not isMonitOverrideOn And Not isCycleStartDisableOn Then

                                                Log.Info("== 02 ==> The Cycle Start Disable will be launched by delay time")
                                                Log.Info("== 02 ==> The machine's status will be changed to LOCKED, the monitoring override will be set ON and the Cycle Start Disable will be launched.")

                                                cmd = "LOCKED"

                                                eNetServer.SendStatusToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, cmd)

                                                MonitoringBoardsService.SetMonitoringOverride(MonitBoardId, True)
                                                MonitoringBoardsService.SetCycleStartDisable(MonitBoardId, True)


                                            ElseIf Not inProduction And isMonitOverrideOn And isCycleStartDisableOn Then

                                                Log.Info("== 03 ==> The machine was locked and the status was changed to a not production status.")
                                                Log.Info("== 03 ==> The Cycle Start Disable will be set OFF.")

                                                MonitoringBoardsService.SetCycleStartDisable(MonitBoardId, False)


                                            ElseIf inProduction And isMonitOverrideOn And Not isCycleStartDisableOn Then

                                                Log.Info("== 04 ==> The machine was not in production, but it was put in production through the eNET.")
                                                Log.Info("== 04 ==> The Monitoring Override will be set OFF and the Focas status will be reseted.")

                                                MonitoringBoardsService.SetMonitoringOverride(MonitBoardId, False)
                                                csi_lib.MCHS_(machineId).current() = old_status


                                            ElseIf (new_status = "CYCLE ON" Or new_status = "CYCLE OFF") And inProduction And Not isCycleStartDisableOn And Not isMonitOverrideOn And Not activatedCriticalStop And Not IsPressureOkay Then

                                                Log.Info("== 05 ==> The machine is in production and the pressure is low.")

                                                If IsCriticalStopAllowed And new_status = "CYCLE ON" Then
                                                    Log.Info("== 05 ==> The machine's status will be changed to LOCKED and the monitoring override will be set ON.")
                                                    Log.Info("== 05 ==> It will be pushed a 'Machine Critical Stop' to the machine.")

                                                    eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, "LOCKED")

                                                    FocasConnector.SendG008_6(csi_lib.MCHS_(machineId).MachineIp, csi_lib.MCHS_(machineId).MachinePort)
                                                End If

                                                logMCS = True
                                                monitBoardStatus.ActivatedCriticalStop = True
                                                MonitoringBoardsService.SetMonitoringOverride(MonitBoardId, True)
                                                MonitoringBoardsService.SetCycleStartDisable(MonitBoardId, True)

                                                For Each sensor As Monitoring In csi_lib.MCHS_(machineId).Sensors
                                                    sensor.IsCSD = True
                                                Next

                                            ElseIf (new_status = "CYCLE ON" Or new_status = "CYCLE OFF") And inProduction And isCycleStartDisableOn And Not isMonitOverrideOn And Not activatedCriticalStop And Not IsPressureOkay Then

                                                Log.Info("== 05a ==> The machine is in production and the Cycle Start Disable was launched by Monitoring Board.")

                                                If IsCriticalStopAllowed And new_status = "CYCLE ON" Then
                                                    Log.Info("== 05a ==> The machine's status will be changed to LOCKED and the monitoring override will be set ON.")
                                                    Log.Info("== 05a ==> It will be pushed a 'Machine Critical Stop' to the machine.")

                                                    eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, "LOCKED")

                                                    FocasConnector.SendG008_6(csi_lib.MCHS_(machineId).MachineIp, csi_lib.MCHS_(machineId).MachinePort)
                                                End If

                                                logMCS = True
                                                monitBoardStatus.ActivatedCriticalStop = True

                                                'MonitoringBoardsService.SetMonitoringOverride(MonitBoardId, True)


                                            ElseIf new_status = "CYCLE ON" And old_status = "LOCKED" And isCycleStartDisableOn And Not activatedCriticalStop And Not IsPressureOkay Then

                                                Log.Info("== 06 ==> The machine is LOCKED and the Cycle Start Disable is ON but it was put in CYCLE ON.")

                                                If IsCriticalStopAllowed Then
                                                    Log.Info("== 06 ==> It will be pushed a 'Machine Critical Stop' to the machine.")

                                                    FocasConnector.SendG008_6(csi_lib.MCHS_(machineId).MachineIp, csi_lib.MCHS_(machineId).MachinePort)
                                                End If

                                                logMCS = True
                                                monitBoardStatus.ActivatedCriticalStop = True


                                            ElseIf (new_status = "CYCLE ON" Or new_status = "CYCLE OFF") And isCycleStartDisableOn And activatedCriticalStop And IsPressureOkay Then

                                                Log.Info("== 06a ==> The machine is in production and the Cycle Start Disable by low pressure was launched by Monitoring Board and now the pressure is well.")

                                                MonitoringBoardsService.SetMonitoringOverride(MonitBoardId, False)
                                                MonitoringBoardsService.SetCycleStartDisable(MonitBoardId, False)

                                                If new_status = "CYCLE ON" Then

                                                    eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, cmd)

                                                ElseIf new_status = "CYCLE OFF" Then

                                                    eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, enetMachine.Cmd_PROD)
                                                    eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, cmd)

                                                ElseIf new_status.Contains("eMONIT") And inProduction Then

                                                    eNetServer.SendStatusToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, cmd)

                                                End If

                                                logMCS = False
                                                monitBoardStatus.ActivatedCriticalStop = False


                                            ElseIf (new_status = "CYCLE ON" Or new_status = "CYCLE OFF") And Not inProduction And Not old_status.Contains("eMONIT") And Not isMonitOverrideOn Then

                                                If Not old_status = "SETUP" Or (old_status = "SETUP" And Not csdOnSetup) Then

                                                    Log.Info("== 07 ==> The machine's status was changed to a not production status.")
                                                    Log.Info("== 07 ==> The monitoring override will be set ON.")

                                                    MonitoringBoardsService.SetMonitoringOverride(MonitBoardId, True)

                                                End If


                                            ElseIf ((Not new_status = "LOCKED" And inProduction) Or new_status.Contains("eMONIT") Or new_status = "CYCLE ON") And Not isCycleStartDisableOn Then

                                                Log.Info("== 08 ==> Normal production.")
                                                Log.Info($"== 08 ==> The machine's status will be changed to {new_status}.")

                                                If new_status = "CYCLE ON" Then

                                                    eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, cmd)

                                                ElseIf new_status = "CYCLE OFF" Then

                                                    eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, enetMachine.Cmd_PROD)
                                                    eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, cmd)

                                                ElseIf new_status.Contains("eMONIT") And inProduction Then

                                                    eNetServer.SendStatusToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, cmd)

                                                End If

                                                If isCycleStartDisableOn Then
                                                    MonitoringBoardsService.SetCycleStartDisable(MonitBoardId, False)
                                                End If
                                                If isMonitOverrideOn Then
                                                    MonitoringBoardsService.SetMonitoringOverride(MonitBoardId, False)
                                                End If

                                            Else
                                                saveChange = False
                                            End If

                                        ElseIf new_status <> old_status Then

                                            saveChange = False

                                            Log.Debug($"============> NO Monitoring Board setted. {machname} ({machineId}) - Status: { old_status } - New Status: { new_status }")

                                            If old_status.Contains("eMONIT") Then

                                            End If

                                            Select Case new_status
                                                Case "CYCLE ON"
                                                    If old_status = "SETUP" And csi_lib.MCHS_(machineId).COnDuringSetup And enetMachine.CommandsAvailable.ContainsKey("SETUP-CYCLE ON") Then

                                                        eNetServer.SendStatusToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, enetMachine.CommandsAvailable("SETUP-CYCLE ON"))

                                                    ElseIf old_status <> "SETUP-CYCLE ON" Then  'And (old_status <> "SETUP" Or Not enetMachine.AlwaysRecordCycleOn) 
                                                        'If old_status <> "SETUP-CYCLE ON" Then

                                                        Log.Info($"Status Change 1 - Machine: {machname}, Status: { old_status }, New Status: { new_status }")

                                                        eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, cmd)

                                                    End If

                                                Case "CYCLE OFF"

                                                    If inProduction Or
                                                        old_status.Contains("eMONITOR") Or
                                                        old_status.ToUpper().Equals("FOV_CYCLE_OFF") Or
                                                        old_status.ToUpper().Equals("END SETUP") Or
                                                        old_status.ToUpper().Equals("ESTP") Then

                                                        Log.Info($"Status Change 2 - Machine: {machname}, Status: { old_status }, New Status: { new_status }")

                                                        eNetServer.SendStatusToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, enetMachine.Cmd_PROD)
                                                        eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, cmd)

                                                    ElseIf old_status = "SETUP-CYCLE ON" Then

                                                        Log.Info($"Status Change 3 - Machine: {machname}, Status: { old_status }, New Status: { new_status }")

                                                        eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, cmd)
                                                        eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, enetMachine.Cmd_SETUP)
                                                        'eNetServer.SendStatusToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, enetMachine.Cmd_SETUP)

                                                    End If

                                                Case "NOeMONITORING"

                                                    If inProduction Then

                                                        Log.Info($"Status Change 4 - Machine: {machname}, Status: { old_status }, New Status: { new_status }")

                                                        eNetServer.SendStatusToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, new_status)

                                                    End If

                                                Case "NO eMONITORING"

                                                    If inProduction Then

                                                        Log.Info($"Status Change 4 - Machine: {machname}, Status: { old_status }, New Status: { new_status }")

                                                        eNetServer.SendStatusToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, new_status)

                                                    End If

                                                Case Else

                                                    Log.Info($"Status Change 5 - Machine: {machname}, Status: { old_status }, New Status: { new_status }")

                                                    If old_status = "CYCLE ON" Then

                                                        eNetServer.SendStatusToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, enetMachine.Cmd_PROD)
                                                        eNetServer.SendCMDEnetFTP(eNetServer.Connections.FtpIp, eNetServer.Connections.FtpPassword, enetMachine.FTPFileName, enetMachine.MachineName, enetMachine.Cmd_COFF)

                                                    End If

                                                    eNetServer.SendStatusToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, new_status)

                                            End Select

                                        End If

                                        cmd_sent = True

                                        Log.Debug("========================================================================================================= End")

                                    End If

                                    If MonitBoardId > 0 And (saveCounter(machineId) > 50 Or saveChange) Then

                                        errorPoint = 90000 + machineId

                                        If csi_lib.MCHS_(machineId).Sensors.Count = 0 Then
                                            Log.Info($"--> Machine {machineId}. No sensors to save")
                                        End If

                                        For Each sensor In csi_lib.MCHS_(machineId).Sensors

                                            Dim strCmd As StringBuilder = New StringBuilder()
                                            strCmd.Append($"INSERT INTO monitoring.sensorcurrentreadings")
                                            strCmd.Append($" (                                       ")
                                            strCmd.Append($"    MachineId       ,                    ")
                                            strCmd.Append($"    BoardName       ,                    ")
                                            strCmd.Append($"    Timestamp       ,                    ")
                                            strCmd.Append($"    CurrentTime     ,                    ")
                                            strCmd.Append($"    IsMonitoring    ,                    ")
                                            strCmd.Append($"    IsSensorAvailable ,                  ")
                                            strCmd.Append($"    IsOverride      ,                    ")
                                            strCmd.Append($"    IsAlarming      ,                    ")
                                            strCmd.Append($"    IsCSD           ,                    ")
                                            strCmd.Append($"    IsMCS           ,                    ")
                                            strCmd.Append($"    CurrentPallet   ,                    ")
                                            strCmd.Append($"    SensorName      ,                    ")
                                            strCmd.Append($"    SensorGroup     ,                    ")
                                            strCmd.Append($"    Metric          ,                    ")
                                            strCmd.Append($"    Value                                ")
                                            strCmd.Append($" )                                       ")
                                            strCmd.Append($" VALUES                                  ")
                                            strCmd.Append($" (                                       ")
                                            strCmd.Append($"     { machineId }              ,        ")
                                            strCmd.Append($"    '{ sensor.DeviceName }'     ,        ")
                                            strCmd.Append($"    '{ sensor.Timestamp.ToString("yyyy-MM-dd HH:mm:ss") }', ")
                                            strCmd.Append($"    '{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }'    , ")
                                            strCmd.Append($"     { sensor.IsMonitoring }    ,        ")
                                            strCmd.Append($"     { sensor.IsAvailable }     ,        ")
                                            strCmd.Append($"     { sensor.IsOverride }      ,        ")
                                            strCmd.Append($"     { sensor.IsAlarming }      ,        ")
                                            strCmd.Append($"     { sensor.IsCSD }           ,        ")
                                            strCmd.Append($"     { logMCS }                 ,        ")
                                            strCmd.Append($"    '{ sensor.CurrentPallet }'  ,        ")
                                            strCmd.Append($"    '{ sensor.SensorName }'     ,        ")
                                            strCmd.Append($"    '{ sensor.SensorId }'       ,        ")
                                            strCmd.Append($"    '{ sensor.SensorType }'     ,        ")
                                            strCmd.Append($"     { IIf(sensor.IsMonitoring And sensor.IsAvailable, sensor.Value, 0) } ")
                                            strCmd.Append($" );                                      ")
                                            MySqlAccess.ExecuteNonQuery(strCmd.ToString())

                                        Next

                                        saveCounter(machineId) = 0
                                    End If

                                    errorPoint = 100000 + machineId

                                    saveCounter(machineId) += 1

                                    Dim new_part As String = ""
                                    Dim old_part As String = ""

                                    Dim partNumber = IIf(Not IsDBNull(mtcMachLiveStatus("PartNumber")), mtcMachLiveStatus("PartNumber"), "")
                                    Dim progNumber = IIf(Not IsDBNull(mtcMachLiveStatus("Program")), mtcMachLiveStatus("Program"), "")

                                    'part Number cmd
                                    If Not String.IsNullOrEmpty(partNumber) And partNumber.ToUpper() <> "UNAVAILABLE" And partNumber.ToUpper() <> "NOT AVAILABLE" Then
                                        new_part = partNumber
                                    ElseIf Not String.IsNullOrEmpty(progNumber) Then
                                        new_part = progNumber
                                    End If

                                    If old_row IsNot Nothing Then
                                        If Not IsDBNull(old_row("PartNumber")) Then
                                            old_part = old_row("PartNumber")
                                        ElseIf Not IsDBNull(old_row("Program")) Then
                                            old_part = old_row("Program")
                                        End If
                                    End If

                                    errorPoint = 110000 + machineId

                                    'Check if ENET Partnumber for Machine is Equal to the which we are SEnding now   
                                    If Not String.IsNullOrEmpty(new_part) And new_part.ToUpper() <> "UNAVAILABLE" And new_part.ToUpper() <> "NOT AVAILABLE" Then

                                        Log.Debug($"eNET New Part Number: {new_part} - Old part number: {old_part}")

                                        If old_part.Replace(vbCr, "").Replace(vbLf, "") <> new_part Then

                                            If CheckConnInfo(machconfig.FTPFileName) Then

                                                Log.Info($"==========================================================================================================================================================================")
                                                Log.Info($"Send command to change part number. Machine: {enetMachine.MachineName} ({enetMachine.EnetPos}), Command: {enetMachine.Cmd_UDPPARTNO & new_part}")

                                                'SendCMDEnetFTP(ftpip, ftppwd, machconfig.FTPFileName, machconfig.MachineName, machconfig.Cmd_PARTNO & new_part)
                                                eNetServer.SendPartNumToEnetOverUdp(CSI_Library.CSI_Library.LocalHostIP,
                                                                                eNetServer.Connections.UdpIp,
                                                                                eNetServer.Connections.UdpPort,
                                                                                enetMachine.EnetPos,
                                                                                enetMachine.Cmd_UDPPARTNO,
                                                                                new_part)

                                                Dim row = RealTime_datatable.Rows.Find(machname)

                                                If row IsNot Nothing Then
                                                    row("PartNumber") = new_part
                                                End If
                                            End If
                                        End If
                                    End If

                                    errorPoint = 120000 + machineId

                                    If MonitBoardId > 0 And Not String.IsNullOrEmpty(new_pallet) And old_pallet <> new_pallet Then

                                        Log.Info($"Pallet change {machname} - Current Pallet: {old_pallet}, New Pallet: {new_pallet}")

                                        MonitoringBoardsService.SetPallet(MonitBoardId, new_pallet)

                                        Dim row = RealTime_datatable.Rows.Find(machname)

                                        If row IsNot Nothing Then
                                            row("Pallet") = new_pallet
                                        End If

                                        old_pallet = new_pallet
                                    End If

                                Else

                                    Log.Error($"Machine not configured in enet: {machname}")
                                End If

                            Catch ex As Exception

                                Log.Error($"ERROR sending ftp command for machine : {machname}", ex)
                            End Try
                        Next

                        snapshot.Clear()
                        snapshot.Dispose()
                    End If

                Catch ex As Exception

                    Log.Error($"ERROR ({errorPoint}) sending data to enet.", ex)

                    DisconnectMtcLivestatus()
                End Try

                Thread.Sleep(1000)

            End While

        Catch ex As Exception
            Log.Error(ex)
        End Try

    End Sub

    Private Sub SendCMDEnetFTP(ip As String, pwd As String, filename As String, machinename As String, command As String)

        Dim request As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create("ftp://" & ip & "/" & filename), System.Net.FtpWebRequest)

        request.Credentials = New System.Net.NetworkCredential(machinename, pwd)
        request.UsePassive = False
        request.UseBinary = False
        request.KeepAlive = True
        request.Timeout = 4000
        request.Method = System.Net.WebRequestMethods.Ftp.UploadFile

        Log.Debug($"Sending FTP command - Host: {request.RequestUri.ToString()} - Credentials: {machinename} / {pwd} - Command: {command}")

        Try
            Dim file_b() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(command)

            Using strz As System.IO.Stream = request.GetRequestStream()
                strz.Write(file_b, 0, file_b.Length)
                strz.Close()
            End Using

        Catch ex As Exception

            Log.Error($"Err while sending ftp => Machine: {machinename}, Command: {command}, File name: {filename}, {request.RequestUri.AbsolutePath}", ex)

        Finally
            request.Abort()
        End Try

    End Sub

    Private Sub SendStatusToEnetUDP(localIp As String, enetIp As String, enetPort As Integer, machinePosition As String, status As String)

        Dim address = IPAddress.Parse(localIp)
        Dim bytes = address.GetAddressBytes()

        Array.Reverse(bytes) 'flip big-endian(network order) To little-endian

        Dim intAddress = BitConverter.ToUInt32(bytes, 0)

        Dim machine_position = ""
        Dim parts = machinePosition.Split(",")
        Dim status2send = status

        If status = "PRODUCTION" Then
            status2send = "_" & status
        End If

        'machine position must be 0 based
        machine_position = parts.Aggregate(Function(x, y) $"{Convert.ToUInt32(x) - 1},{Convert.ToUInt32(y) - 1}")

        Dim data = String.Format("_SETMON_STATUS,{0},{1},{2}", intAddress.ToString(), machine_position, status2send)

        SendOverUDP(enetIp, enetPort, data)

    End Sub

    Private Sub SendOverUDP(remoteIp As String, remotePort As Integer, data As String)

        Try

            Log.Debug($"Sending UDP data to enet. Remote IP: { remoteIp }, Remote Port: { remotePort }, Data: { data }")

            Dim udp_client = New UdpClient()
            udp_client.Connect(remoteIp, remotePort)

            Dim remoteView = Encoding.ASCII.GetBytes($"REMOTE_VIEW,{ intAddress.ToString() }")
            udp_client.Send(remoteView, remoteView.Length)

            Dim data_bytes = Encoding.ASCII.GetBytes(data)
            udp_client.Send(data_bytes, data_bytes.Length)
            udp_client.Close()

        Catch ex As Exception

            Log.Error($"Sending UDP data to enet. Remote IP: { remoteIp }, Remote Port: { remotePort }, Data: { data }", ex)

        End Try

    End Sub

    Private Function CheckConnInfo(ftpfilename) As Boolean
        Dim res As Boolean = False

        If (ftpip.Length > 0) And (ftpfilename.Length > 0) Then
            res = True
        End If
        If (res = False) Then
            csi_lib.LogServiceError($"CheckConnInfo: No connection information in enet's FTP server. filename: {ftpfilename}  ftpip:{ftpip}", 1)
        End If
        Return res
    End Function

    Private Sub LoadMachineList()

        Try
            Dim machlist As New List(Of eNetMachineConfig)
            Dim eNET_path As String = csi_lib.eNET_path
            Dim ehubfile As String = eNET_path & "\_SETUP\eHUBConf.sys"
            Dim monsetupfile As String = eNET_path & "\_SETUP\MonSetup.sys"
            Dim filesexists As Boolean = False
            Dim computername As String = "localhost"

            If (eNET_path.StartsWith("\\")) Then
                Dim sharenameindex As Integer = eNET_path.IndexOf("\", 2)
                If (sharenameindex > 2) Then
                    computername = eNET_path.Substring(2, sharenameindex - 2)
                End If
            End If

            'Here We need to do Something because software doesn't have access to Ehub and Monsetup Files
            If Not filesexists Then
                If (File.Exists(ehubfile) And File.Exists(monsetupfile)) Then
                    filesexists = True
                End If
            End If
            If (filesexists) Then
                'split per machine section
                'parse each section to find FI:1
                'if found, add machine to list
                Dim line As String = ""
                Dim tempsection As String = ""
                Dim ehubsections As New List(Of String)

                Using fs As New StreamReader(File.OpenRead(ehubfile))
                    While Not (fs.EndOfStream)
                        line = fs.ReadLine()
                        If (line.Length > 0) Then
                            tempsection = tempsection & Environment.NewLine & line
                        Else
                            If (tempsection.Length > 0) Then
                                ehubsections.Add(tempsection)
                                tempsection = ""
                            End If
                        End If
                    End While
                    fs.Close()

                End Using

                For Each section In ehubsections

                    If (section.IndexOf("FI:1") > 0) Then

                        Dim machname As String = GetEnetValue(section, "NM:")
                        Dim two_heads As Boolean = If(GetEnetValue(section, "TH:") = "0", False, True)
                        Dim machpos As String = section.Trim.Substring(0, section.IndexOf(":") - 2)
                        Dim sendfolder As String = GetEnetValue(section, "DT:")
                        Dim receivefolder As String = GetEnetValue(section, "DR:")

                        machlist.Add(New eNetMachineConfig(machname, machpos, two_heads, sendfolder, receivefolder))

                    End If

                Next

                'Call csi_lib.LogServiceError("!! machlist count" & machlist.Count, 0)
                'read monsetup and split per machine section
                'for each section
                'match machpos
                'Dim monsetuptext As String = File.ReadAllText(monsetupfile)
                Dim monsetupsections As New List(Of String) 'monsetupfile.Split(Environment.NewLine)
                Dim lastline As String = ""
                Dim emptyparam As Boolean = False

                Using fs As New StreamReader(File.OpenRead(monsetupfile))

                    While Not (fs.EndOfStream)

                        line = fs.ReadLine()

                        If (line.Length > 0) Then
                            tempsection = tempsection & Environment.NewLine & line
                            emptyparam = False
                        Else
                            If (tempsection.Length > 0) And Not emptyparam Then
                                If (lastline.StartsWith("DF:")) Then
                                    emptyparam = True
                                Else
                                    monsetupsections.Add(tempsection)
                                    tempsection = ""
                                End If
                            End If
                        End If
                        lastline = line
                    End While
                    fs.Close()
                End Using
                'New Code \By Bhavik To Filter New Version of ENET MonsetUp File
                Dim ListWithNoSpace As String = String.Empty
                For Each lines As String In monsetupsections
                    lines.Replace(vbCrLf & vbCrLf, vbCrLf)
                    ListWithNoSpace = ListWithNoSpace & lines
                Next
                monsetupsections.Clear()
                For I As Integer = 1 To 16
                    For J As Integer = 1 To 8
                        monsetupsections.Add(GetEnetMachinesByFilter(ListWithNoSpace, $"{I},{J}:", $"{I},{J + 1}:"))
                    Next
                Next
                For Each mach In machlist
                    'Call csi_lib.LogServiceError("!! For Each mach In machlist" & mach.MachineName, 0)
                    For Each section In monsetupsections
                        Dim sectionpos As String = section.Trim.Substring(0, section.IndexOf(":"))
                        If (sectionpos = mach.EnetPos) Then
                            'Dim cmd_dprod As String = GetEnetValue(section, "DD:").Split(",")(0)
                            Dim ftpfilename As String = GetEnetValue(section, "DD:").Split(",")(1)
                            Dim cmd_partno As String = GetEnetValue(section, "N1:")
                            Dim cmd_CON As String = GetEnetValue(section, "PS:")
                            Dim cmd_COFF As String = GetEnetValue(section, "DE:")
                            Dim cmd_prod As String = GetEnetValue(section, "PR:")
                            Dim cmd_setup As String = GetEnetValue(section, "TR:")
                            Dim tnameothers As String = GetEnetMultiValue(section, "DF:", "CM:")
                            Dim name_others As New List(Of String)
                            If (tnameothers.Length > 0) Then
                                name_others = tnameothers.Split(Environment.NewLine).ToList
                            End If
                            Dim tcmdothers As String = GetEnetMultiValue(section, "CM:", "AC:")
                            Dim cmd_others As New List(Of String)
                            If (tcmdothers.Length > 0) Then
                                cmd_others = tcmdothers.Split(Environment.NewLine).ToList
                            End If
                            Dim others As New Dictionary(Of String, String)
                            Dim cnt_others As Integer = If(name_others.Count <= cmd_others.Count, name_others.Count, cmd_others.Count)
                            For i = 0 To cnt_others - 1
                                others.Add(name_others(i).Trim, cmd_others(i).Trim)
                            Next

                            mach.FTPFileName = ftpfilename
                            mach.Cmd_PARTNO = cmd_partno
                            mach.Cmd_CON = cmd_CON
                            mach.Cmd_COFF = cmd_COFF
                            mach.Cmd_PROD = cmd_prod
                            mach.Cmd_SETUP = cmd_setup
                            mach.Cmd_Others = others
                            Exit For
                        End If
                    Next
                Next

                ftpmachlist = machlist

            Else
                Call csi_lib.LogServiceError("Enet is missing configuration files eHUBCONF.sys or MonSetup.sys:" & ehubfile, 0)
            End If
        Catch ex As Exception
            Call csi_lib.LogServiceError("ERROR unable to create ftp machine list" & ex.Message & ex.StackTrace, 0)
        End Try

    End Sub

    Private Shared Function GetEnetMachinesByFilter(section As String, startparameter As String, endparameter As String) As String

        Dim value As String = ""
        Dim startSize As Int16 = startparameter.Length

        Dim startpos = section.IndexOf(startparameter) + startparameter.Length
        Dim endpos = section.IndexOf(endparameter)

        If endpos = -1 Then
            Dim endparaupdate = endparameter.Split(",")
            Dim newfirstdigit = Convert.ToString(Convert.ToInt32(endparaupdate(0)) + 1)
            Dim newendparameter = newfirstdigit & ",1:"
            endpos = section.IndexOf(newendparameter)
        End If

        If endpos = -1 Then
            'case when we are at the last machine
            If startpos - startSize < 0 Then
                value = section.Substring(startpos - 3).Trim
            Else
                value = section.Substring(startpos - startSize).Trim
            End If
        Else
            If startpos - startSize < 0 Then
                'case when we have machine numbers like 1,1 upto 9,8
                value = section.Substring(startpos - 3, (endpos - startpos) + 3).Trim
            Else
                'case when we have machine numbers like 10,1 upto 16,8
                value = section.Substring(startpos - startSize, (endpos - startpos) + startSize).Trim
            End If
        End If

        Return value

    End Function

    Private Function GetEnetValue(section As String, parameter As String) As String
        Dim value As String = ""
        Try
            Dim startpos = section.IndexOf(parameter) + parameter.Length
            Dim endpos = section.IndexOf(Environment.NewLine, section.IndexOf(parameter) + 1)
            value = section.Substring(startpos, endpos - startpos)
        Catch ex As Exception
            csi_lib.LogServerError("Error in GetEnetValue for Parameter :" & parameter & Environment.NewLine & " Error Message :" & ex.Message() & Environment.NewLine & " StackTrace :" & ex.StackTrace(), 1)
        End Try
        Return value
    End Function

    Private Function GetEnetMultiValue(section As String, startparameter As String, endparameter As String) As String
        Dim value As String = ""
        Try
            Dim startpos = section.IndexOf(startparameter) + startparameter.Length
            Dim endpos = section.IndexOf(endparameter)
            value = section.Substring(startpos, endpos - startpos).Trim
        Catch ex As Exception
            csi_lib.LogServerError("Error in GetEnetMultiValue for Parameters :" & startparameter & " , " & endparameter & Environment.NewLine & " Error Message :" & ex.Message() & Environment.NewLine & " StackTrace :" & ex.StackTrace(), 1)
        End Try
        Return value
    End Function

    Private Sub DisconnectMtcLivestatus()

        Try
            GetMTCMachinesIp()

            Log.Debug("DISCONECTING MTC MACHINES")

            For Each mach In dtMtcMachines.Rows

                Dim enetMachName As String = mach("eNETMachineName")
                Dim machineName As String = mach("MachineName")
                Dim enetMachine As eNetMachineConfig = eNetServer.Machines.FirstOrDefault(Function(m) m.MachineName = enetMachName)

                Log.Debug($"Disconecting {machineName} - {enetMachName}")

                Dim command As String = ""
                Dim message As String = ""
                Try

                    If enetMachine.Cmd_Others.ContainsKey("NOeMONITORING") Then
                        command = enetMachine.Cmd_Others("NOeMONITORING")
                    End If

                    If useUDP Then
                        If command = "" Then command = enetMachine.Cmd_PROD
                        SendStatusToEnetUDP(CSI_Library.CSI_Library.LocalHostIP, eNetServer.Connections.UdpIp, eNetServer.Connections.UdpPort, enetMachine.EnetPos, command)
                        message = $"Disconecting {enetMachine.MachineName}, Protocol: UDP, Command: {command}"
                    Else
                        If command = "" Then command = enetMachine.Cmd_COFF
                        SendCMDEnetFTP(ftpip, ftppwd, enetMachine.FTPFileName, enetMachine.MachineName, command)
                        message = $"Disconecting {enetMachine.MachineName}, Protocol: FTP, Command: {command}"
                    End If

                    Log.Debug(message)

                Catch ex As Exception
                    Log.Error(ex)
                End Try
            Next

        Catch ex As Exception
            Log.Error(ex)
        End Try
    End Sub

#End Region

#Region "Autoreporting"

    'Public Sub StartAutoreporting(val As Boolean)
    '    reptablepresent = check_tableExists("csi_auth", "Auto_Report_config")
    '    While (val = True)
    '        GenerateReports()
    '    End While
    'End Sub

    ' for debug only
    'Private Sub writeinfile(text As String)
    '    Dim FILE_NAME As String = "C:\Users\asalm\Desktop\debug.txt"
    '    Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)

    '    objWriter.WriteLine(text)
    '    objWriter.WriteLine(" ")

    '    objWriter.Close()
    'End Sub

    'Private Sub GenerateReports()

    '    Dim DayName As String = Convert.ToString(Now.DayOfWeek)
    '    Dim autoReport As Boolean = getAutoReportOrNot()

    '    Try
    '        If (autoReport = True) Then

    '            'Dim Dataset_AutoReport As DataSet = New DataSet()
    '            'Dim conn As MySqlConnection = New MySqlConnection()
    '            'Dim adap As New MySqlDataAdapter()

    '            'conn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString

    '            Try
    '                'conn.Open()

    '                'If (DayName <> Convert.ToString(Convert.ToInt32(System.DateTime.Now.DayOfWeek))) Then
    '                '    resetDoneReport()
    '                'End If

    '                'Dim dayOfNow As DateTime = Now()
    '                ''DayName = Convert.ToString(dayOfNow.DayOfWeek)
    '                'DayName = Convert.ToString(dayOfNow.DayOfWeek)
    '                ''Dim DayName1 As String = dayOfNow.DayOfWeek.ToString()
    '                Dim timeNow = Now.ToString("HH:mm")
    '                'Dim QryStr As String = String.Format($"SELECT * FROM csi_auth.Auto_Report_config where (Day_ like '%{ DateTime.Now.DayOfWeek.ToString() }%' or Day_ like '%everyday%' ) and Time_ like '{ timeNow }' and done <> '{ DayName }'")
    '                Dim fileToSend As String

    '                'adap = New MySqlDataAdapter(QryStr, conn)
    '                'adap.Fill(Dataset_AutoReport, "tableDGV")

    '                Dim listOfMachine As String()

    '                Dim reports = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.View_Auto_Report_config where (Day_ like '%{ DateTime.Now.DayOfWeek.ToString() }%' or Day_ like '%everyday%' ) and Time_ like '{ timeNow }' and done <> '{ DayName }'")

    '                For Each row As DataRow In reports.Rows

    '                    Dim custommsg As String = Convert.ToString(row("CustomMsg"))
    '                    If custommsg.Length = 0 Then
    '                        If Convert.ToString(row("ReportPeriod")).Contains("Today") Then
    '                            'Today
    '                            custommsg = $"Your Today's CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
    '                        ElseIf Convert.ToString(row("ReportPeriod")).Contains("Monthly") Then
    '                            'Monthly Reporting
    '                            custommsg = "Your Monthly CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
    '                        ElseIf Convert.ToString(row("ReportPeriod")).Contains("Weekly") Then
    '                            'Weekly Reporting
    '                            custommsg = "Your Weekly CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
    '                        ElseIf Convert.ToString(row("ReportPeriod")).Contains("Yesterday") Then
    '                            'Daily ( Yesterday ) Availability - PDF
    '                            custommsg = "Your Daily CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
    '                        End If
    '                    Else
    '                        custommsg = Convert.ToString(row("CustomMsg"))
    '                    End If

    '                    Try
    '                        listOfMachine = Convert.ToString(row("MachineToReport")).Split(";")

    '                        Dim SEvent(0 To listOfMachine.Count() - 1, 1) As String
    '                        For i = 0 To listOfMachine.Count() - 1

    '                            SEvent(i, 0) = listOfMachine(i)
    '                            SEvent(i, 1) = "SETUP"
    '                        Next

    '                        Dim period = row("ReportPeriod").ToString()
    '                        Dim startdate As DateTime
    '                        Dim enddate As DateTime

    '                        If period.Contains("Today") Then
    '                            Dim hour = Convert.ToInt16(row("shift_starttime").ToString().Split(":")(0))
    '                            Dim minute = Convert.ToInt16(row("shift_starttime").ToString().Split(":")(1))
    '                            startdate = Today.Date.Add(New TimeSpan(hour, minute, 0))

    '                            hour = Convert.ToInt16(row("shift_endtime").ToString().Split(":")(0))
    '                            minute = Convert.ToInt16(row("shift_endtime").ToString().Split(":")(1))
    '                            enddate = Today.Date.Add(New TimeSpan(hour, minute, 0))

    '                        ElseIf period.Contains("Yesterday") Then
    '                            startdate = Today.Date.AddDays(-1)
    '                            enddate = Today.Date.AddSeconds(-1)

    '                        ElseIf period.Contains("Weekly") Then
    '                            startdate = Today.Date.AddDays(Integer.Parse(row("dayback").ToString()) * -1)
    '                            enddate = Today.Date.AddSeconds(-1)

    '                        ElseIf period.Contains("Monthly") Then
    '                            startdate = Today.Date.AddMonths(-1)
    '                            enddate = Today.Date.AddSeconds(-1)

    '                        End If

    '                        fileToSend = csi_lib.generateReport(
    '                                    listOfMachine,
    '                                    row("ReportType").ToString(),
    '                                    row("ReportPeriod").ToString(),
    '                                    startdate,
    '                                    enddate,
    '                                    row("Output_Folder").ToString(),
    '                                    SEvent,
    '                                    True,
    '                                    row("short_FileName").ToString(),
    '                                    row("Task_name").ToString())

    '                        Try
    '                            If Not (row("MailTo") Is Nothing) Then
    '                                Dim subject As String = $"CSIFLEX {row("ReportType").ToString()} Report {row("ReportPeriod").ToString()} - {row("ReportTitle").ToString()}"
    '                                csi_lib.sendReportByMail(row("MailTo"), fileToSend, custommsg, subject)
    '                            End If
    '                        Catch ex As Exception
    '                            csi_lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
    '                        End Try

    '                        updateDoneReport(row("Id"))

    '                        'If Convert.ToString(row("Report_Type")) = "Daily ( Today ) Availability - PDF" Then
    '                        '    Dim todaystart As String = row("shift_starttime").ToString()
    '                        '    Dim Todayend As String = row("shift_endtime").ToString()
    '                        '    Dim todaystart_hour_min() As String = todaystart.Split(":")
    '                        '    Dim todayend_hour_min() As String = Todayend.Split(":")
    '                        '    Dim startdate As DateTime
    '                        '    Dim enddate As DateTime
    '                        '    startdate = New DateTime(dayOfNow.Year, dayOfNow.Month, dayOfNow.Day, todaystart_hour_min(0), todaystart_hour_min(1), 0)
    '                        '    enddate = New DateTime(dayOfNow.Year, dayOfNow.Month, dayOfNow.Day, todayend_hour_min(0), todayend_hour_min(1), 0)
    '                        '    fileToSend = csi_lib.generateReport(listOfMachine, Convert.ToString(row("ReportType")), row("ReportPeriod").ToString(), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, row("short_FileName").ToString(), Convert.ToString(row("Task_name")))

    '                        '    Try
    '                        '        If Not (row("MailTo") Is Nothing) Then
    '                        '            csi_lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
    '                        '        End If
    '                        '    Catch ex As Exception
    '                        '        csi_lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
    '                        '    End Try
    '                        '    updateDoneReport(row("Task_name"))
    '                        '    ' Forms.MessageBox.Show(startdate)
    '                        '    'Forms.MessageBox.Show(enddate)

    '                        'ElseIf Convert.ToString(row("Report_Type")) = "Monthly Availability - PDF" And Convert.ToInt32(Convert.ToString(dayOfNow.Day)) = 1 Then
    '                        '    'run monthly for last month

    '                        '    Dim startdate As DateTime
    '                        '    Dim enddate As DateTime
    '                        '    enddate = dayOfNow
    '                        '    startdate = dayOfNow.AddMonths(-1)
    '                        '    startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
    '                        '    enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

    '                        '    fileToSend = csi_lib.generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, row("short_FileName").ToString(), Convert.ToString(row("Task_name")))

    '                        '    Try
    '                        '        If Not (row("MailTo") Is Nothing) Then
    '                        '            csi_lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
    '                        '        End If
    '                        '    Catch ex As Exception
    '                        '        csi_lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
    '                        '    End Try
    '                        '    updateDoneReport(row("Task_name"))

    '                        'ElseIf (Convert.ToString(row("Report_Type")) = "Weekly Availability - PDF") Then

    '                        '    'run weekly report for last week
    '                        '    Dim startdate As DateTime
    '                        '    Dim enddate As DateTime

    '                        '    Dim days As Integer = 7
    '                        '    Try
    '                        '        If Not (row("dayback") Is Nothing) Then
    '                        '            days = Integer.Parse(Convert.ToString(row("dayback")))
    '                        '        End If
    '                        '    Catch ex As Exception

    '                        '    End Try


    '                        '    enddate = dayOfNow.AddDays(-1)
    '                        '    'startdate = dayOfNow.AddDays(-(days - 1))
    '                        '    startdate = dayOfNow.AddDays(-days)
    '                        '    startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
    '                        '    enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

    '                        '    fileToSend = csi_lib.generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, row("short_FileName").ToString(), Convert.ToString(row("Task_name")))

    '                        '    Try
    '                        '        If Not (row("MailTo") Is Nothing) Then
    '                        '            csi_lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
    '                        '        End If
    '                        '    Catch ex As Exception
    '                        '        csi_lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
    '                        '    End Try
    '                        '    updateDoneReport(row("Task_name"))

    '                        'ElseIf (Convert.ToString(row("Report_Type")) = "Daily ( Yesterday ) Availability - PDF") Then
    '                        '    'run daily for last day

    '                        '    Dim startdate As DateTime
    '                        '    Dim enddate As DateTime
    '                        '    enddate = dayOfNow.AddDays(-1)
    '                        '    startdate = dayOfNow.AddDays(-1)
    '                        '    startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
    '                        '    enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

    '                        '    fileToSend = csi_lib.generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, row("short_FileName").ToString(), Convert.ToString(row("Task_name")))

    '                        '    Try
    '                        '        If Not (row("MailTo") Is Nothing) Then
    '                        '            csi_lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
    '                        '        End If
    '                        '    Catch ex As Exception
    '                        '        csi_lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
    '                        '    End Try
    '                        '    updateDoneReport(row("Task_name"))
    '                        'Else
    '                        '    csi_lib.LogServiceError(DateTime.Now.ToString() + "invalid report type:" + row("Report_Type").ToString() + "TASK:" + row("Task_name").ToString(), 1)
    '                        'End If

    '                    Catch ex As Exception
    '                        csi_lib.LogServiceError("Error generating autoreports, MSG:" & ex.ToString(), 1)

    '                    End Try
    '                Next
    '            Catch ex As Exception
    '                csi_lib.LogServerError("Error in database in GenerateReports() on ServiceLibrary.vb " & ex.Message & " Stacktrace : " & ex.StackTrace(), 1)
    '            End Try
    '        End If

    '    Catch ex As Exception
    '        csi_lib.LogServiceError("Error in GenerateReports():" & ex.ToString() + vbCrLf + "__>" + ex.StackTrace, 1)
    '    End Try
    'End Sub

    Private Function getAutoReportOrNot() As Boolean
        Dim reportOrNot As Boolean = False
        Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try
            cntsql.Open()
            Dim mysql23 As String = "SELECT * FROM csi_auth.tbl_autoreport_status;"
            Dim cmdCreateDeviceTable23 As New MySqlCommand(mysql23, cntsql)
            Dim mysqlReader23 As MySqlDataReader = cmdCreateDeviceTable23.ExecuteReader
            Dim dTable_message23 As New DataTable()
            dTable_message23.Load(mysqlReader23)
            If dTable_message23.Rows.Count > 0 Then
                reportOrNot = dTable_message23.Rows(0)("autoreport_status").ToString()   'dTable_message23.Columns("autoreport_status").ToString()
            End If
            'cntsql.Close()
        Catch ex As Exception
            Log.Error("Error in loading cAuto Reporting from Database", ex)
        Finally
            cntsql.Close()
        End Try

        Return reportOrNot
    End Function

    Public Function check_tableExists(db_name As String, tbl_name As String) As Boolean
        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try
            mySqlCnt.Open()
            Dim mysql As String = "SELECT count(*) FROM information_schema.tables WHERE table_schema = '" + db_name + "'   AND table_name = '" + tbl_name + "';"
            Dim cmdCreateDeviceTable As New MySqlCommand(mysql, mySqlCnt)
            Dim yes = cmdCreateDeviceTable.ExecuteScalar()
            'mySqlCnt.Close()
            If (yes = 1) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            csi_lib.LogServerError("Error in database check_tableExists on ServiceLibrary.vb " & ex.Message() & " Stacktrace : " & ex.StackTrace(), 1)
        Finally
            mySqlCnt.Close()
        End Try
#Disable Warning BC42353 ' Function 'check_tableExists' doesn't return a value on all code paths. Are you missing a 'Return' statement?
    End Function

#Enable Warning BC42353 ' Function 'check_tableExists' doesn't return a value on all code paths. Are you missing a 'Return' statement?

    'Private Sub resetDoneReport()
    '    Dim sql As String = ("update  CSI_auth.Auto_Report_config set done = 10")
    '    Dim connection As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
    '    Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)

    '    Try
    '        connection.Open()
    '        mysqlcomm.ExecuteNonQuery()
    '        connection.Close()
    '    Catch ex As Exception
    '        csi_lib.LogServiceError(ex.ToString(), 1)
    '        If connection.State = ConnectionState.Open Then connection.Close()
    '    End Try
    'End Sub

    'Private Sub updateDoneReport(reportId As String)

    '    Try
    '        MySqlAccess.ExecuteNonQuery($"update CSI_auth.Auto_Report_status set Status = '{ Convert.ToString(Now.DayOfWeek) }' where ReportId = { reportId };")
    '    Catch ex As Exception
    '        Log.Error(ex)
    '    End Try

    'End Sub

#End Region

#Region "update_DB mysql"

    Private Sub Start_update_mysqlDB()

        csi_lib.Log_server_event("DB update check:")
        InitialDBFinished()

        mysqlupdate_timer = New System.Timers.Timer(1000 * 60 * 5) '15 minutes
        AddHandler mysqlupdate_timer.Elapsed, AddressOf update_timer_Ticked
        mysqlupdate_timer.AutoReset = False
        mysqlupdate_timer.Start()

    End Sub

    Private Sub update_timer_Ticked()

        Try
            Log.Debug("DB update timer ticked:")

            mysqlupdate_timer.Stop()

            InitialDBFinished()

        Catch ex As Exception
            Log.Error("General error in mysql_update service", ex)
        Finally
            mysqlupdate_timer.Start()
        End Try

    End Sub

    Private Sub InitialDBFinished()

        Dim initialdbload As Boolean = True

        Dim mysqlcon As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)

        Try
            mysqlcon.Open()
            Using mysqlcmd As New MySqlDataAdapter("select initialdbload from csi_auth.tbl_updatestatus;", mysqlcon)
                Dim dt As New DataTable
                mysqlcmd.Fill(dt)
                initialdbload = dt.Rows(0)("initialdbload")
            End Using
            mysqlcon.Close()
        Catch ex As Exception
            csi_lib.LogServiceError("InitialDBFinished:unable to load service config:" + ex.Message, 1)
            If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
        End Try

        Update_Mysql()

    End Sub

    Private perf_computed___ As Boolean = False

    Private Sub Update_Mysql()

        Try
            csi_lib.UpdateDB_Mysql()
        Catch ex As Exception
            Log.Error("Unable to update mysql db", ex)
        End Try

    End Sub
#End Region

#Region "Config_Update"

    Private Sub StartConfigUpdate()

        config_timer = New System.Timers.Timer(1000 * 60 * 1) '5 minutes actually every 1 minute 
        AddHandler config_timer.Elapsed, AddressOf config_timer_Ticked
        config_timer.AutoReset = False
        config_timer.Start()
    End Sub

    'Public Sub StartPerformanceUpdate1()
    '    performance_timer2 = New System.Timers.Timer(1000 * 60 * 60) 'Every 1 Hour calculate the MAchine data For Donut and Bar Chart for Targets
    '    AddHandler performance_timer2.Elapsed, AddressOf Performance_timer_Ticked2
    '    performance_timer2.AutoReset = False
    '    performance_timer2.Start()
    'End Sub

    Private Sub StartPerformanceUpdate()
        performance_timer = New System.Timers.Timer(1000 * 60 * 60) 'Every one hour calculate the MAchine data For Donut and Bar Chart for Targets
        AddHandler performance_timer.Elapsed, AddressOf Performance_timer_Ticked
        performance_timer.AutoReset = False
        performance_timer.Start()
    End Sub

    Private Sub StartTemperatureLoad()
        timerTemperature = New Timers.Timer(1000 * 60 * 1)
        AddHandler timerTemperature.Elapsed, AddressOf LoadTemperatures
        timerTemperature.AutoReset = False
        timerTemperature.Start()
    End Sub

    Private Async Sub LoadTemperatures()
        timerTemperature.Stop()
        Dim weather = New WeatherManager()
        weatherDictionary = Await weather.GetWeather()
        timerTemperature.Start()
    End Sub

    Private Sub Performance_timer_Ticked()

        Try
            performance_timer.Stop()

            Read_perf_from_db()

            RefreshAllDevices()

        Catch ex As Exception
            Log.Error($"Error while updating Donut and Machine Target.", ex)
        End Try

        performance_timer.Start()

    End Sub

    'Public Sub Performance_timer_Ticked2()
    '    Dim Count As New Integer
    '    If Count <= 5 Then
    '        Try
    '            performance_timer2.Stop()
    '            Read_perf_from_db()
    '        Catch ex As Exception
    '            Log.Error($"Error while updating Donut and Machine Target.", ex)
    '        End Try
    '        Count = Count + 1
    '        performance_timer2.Start()
    '    Else
    '        performance_timer2.Stop()
    '        Count = 0
    '    End If
    'End Sub

    Private Sub config_timer_Ticked()
        Try
            config_timer.Stop()
            'Update ip
            UpdateConfig()
        Catch ex As Exception
            csi_lib.LogServiceError("General error while updating ip:" & ex.ToString(), 1)
        Finally
            config_timer.Start()
        End Try
    End Sub

    Private Sub UpdateConfig()
        GetEnetIp()
        'GetMTCMachinesIp()
        GetEnetColors()
        GetServiceConfigs()
        LoadMachineList()
        csi_lib.readsetup_for_adv_mtc()
        CheckAutoReportChange()
        'GenerateReports()
    End Sub

    Private Sub CheckAutoReportChange()

        Dim reportchanged As Boolean = False
        Dim directory As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Server"

        If (File.Exists(directory & "\sys\reportingchanged.csys")) Then
            Using reader As StreamReader = New StreamReader(directory & "\sys\reportingchanged.csys")
                reportchanged = reader.ReadLine()
                reader.Close()
            End Using
        End If

        If (reportchanged) Then
            Try
                Using writer As StreamWriter = New StreamWriter(csi_lib.getRootPath() & "\sys\reportingchanged.csys", False)
                    writer.Write(False)
                    writer.Close()
                End Using
            Catch ex As Exception
                csi_lib.LogServerError("Unable to write reportingchanged. Exception:" + ex.Message, 1)
            End Try

            'Here I changed Bhavik Desai Revert Back using uncomment below code
            If (check_tableExists("csi_auth", "Auto_Report_config") And getAutoReportOrNot()) Then
                If Not (myreports Is Nothing) Then
                    myreports.Dispose()
                End If
                myreports = New Autoreporting
            End If
        End If
    End Sub

    Public Sub LoadServerIp()
        Try
            If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")) Then
                Dim lines = File.ReadAllLines((CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys"))
                CSI_Library.CSI_Library.LocalHostIP = lines(0)
            End If
        Catch ex As Exception
            Call csi_lib.LogServiceError("At reading srvip  from csys: " & ex.Message, 1)
        End Try
    End Sub

    Private Sub GetEnetIp()
        Try
            Dim ip_ As String = Nothing
            Dim directory As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
            Dim filepath As String = directory & "\CSI Flex Server\sys\Networkenet_.csys"
            If (File.Exists(filepath)) Then
                Using reader As StreamReader = New StreamReader(filepath)
                    ip_ = reader.ReadLine
                    reader.Close()
                End Using
            End If

            enetip = ip_
        Catch ex As Exception
            enetip = ""
            Call csi_lib.LogServiceError("ERROR unable to retreive enet ip: " & ex.Message, 1)
        End Try
    End Sub

    Private Sub GetMTCMachinesIp()

        Try
            dtMtcMachines = MySqlAccess.GetDataTable("SELECT MachineId, Machinename, eNETMachineName, MachineIP, FocasPort, ConnectorType, CurrentStatus FROM CSI_auth.tbl_CSIConnector")
        Catch ex As Exception
            Log.Error("Unable to retrieve the MTC Machines' IPs", ex)
            dtMtcMachines = New DataTable
        End Try

    End Sub

    Private Sub GetEnetColors()
        Dim tempdt As New DataTable

        Using conn As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            Try
                Dim cmd As New MySqlCommand("SELECT statut,color FROM CSI_database.tbl_colors", conn)
                conn.Open()
                Dim adapter As New MySqlDataAdapter(cmd)
                'enetstatuscolor = New DataTable
                adapter.Fill(tempdt)
                conn.Close()
                enetstatuscolor = tempdt

            Catch ex As Exception
                'enetstatuscolor = New DataTable
                Call csi_lib.LogServiceError("ERROR unable to retreive enet colors " & ex.Message, 1)
                If conn.State = ConnectionState.Open Then conn.Close()
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
    End Sub

    Private Sub GetServiceConfigs()

        Using conn As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            Try
                Dim cmd As New MySqlCommand("SELECT loadingAsCON FROM csi_auth.tbl_serviceconfig", conn)
                conn.Open()
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable
                adapter.Fill(dt)
                loadingAsCON = dt.Rows(0)("LoadingAsCON")
                conn.Close()
            Catch ex As Exception
                loadingAsCON = False
                Call csi_lib.LogServiceError("ERROR unable to retreive service configs " & ex.Message, 1)
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using

        Using conn As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            Try
                Dim cmd As New MySqlCommand("SELECT ftpip, ftppwd FROM csi_auth.ftpconfig", conn)
                conn.Open()
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable
                adapter.Fill(dt)
                If dt.Rows.Count > 0 Then
                    ftpip = dt.Rows(0)("ftpip")
                    ftppwd = dt.Rows(0)("ftppwd")
                Else
                    'ftpip = String.Empty
                    'ftppwd = String.Empty
                End If
                conn.Close()

            Catch ex As Exception
                Call csi_lib.LogServiceError("ERROR unable to retreive ftp configs " & ex.Message, 1)
                If ex.HResult = -2147467259 Then ' 2147467259 = table not existes
                    ftpip = ""
                    ftppwd = ""
                End If
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
    End Sub

#End Region

End Class

Public Class __cReportingData
    Public Task_name As String
    Public TimeOfReport As String
    Public MailTo As String
    Public isDone As String
    Public isShortFileName As String

    Public Sub New(task_name As String, timeOfReport As String, mailTo As String, isdone As String, isshortfilename As String)
        Try
            Me.Task_name = task_name
            Me.TimeOfReport = timeOfReport
            Me.MailTo = mailTo
            Me.isDone = isdone
            Me.isShortFileName = isshortfilename
        Catch ex As Exception
        End Try
    End Sub
End Class


Public Class __cMachineData

    Private Property MachineName As String = ""
    Private Property EnetMachineName As String = ""
    Private Property MachineLabel As String = ""
    Private Property MonitoringFileName As String = ""
    Private Property MonitoringID As String = ""
    Private Property MonitoringState As Integer = 0

    Property MachineName_ As String
        Get
            Return MachineName
        End Get
        Set(value As String)
            MachineName = value
        End Set
    End Property

    Property EnetMachineName_ As String
        Get
            Return EnetMachineName
        End Get
        Set(value As String)
            EnetMachineName = value
        End Set
    End Property

    Property MachineLabel_ As String
        Get
            Return MachineLabel
        End Get
        Set(value As String)
            MachineLabel = value
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

#If False Then
::::::CLASS MachinePerfData For CurrentLastRowData and LastRowData
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
#End If

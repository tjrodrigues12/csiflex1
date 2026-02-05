Imports System.IO
Imports System.Net
Imports System.Threading
Imports CSIFLEX.Utilities
Imports MySql.Data.MySqlClient

Public Class LoadHistory
    Public Shared thread_Load_History_and_TodayData As Thread
    Public DisplayMachineList As New Dictionary(Of String, String) 'machinename , rename_machinename
    Public AllMachines As New Dictionary(Of String, MachineData) 'Monioring id , and list of machine elements 
    Private Sub LoadHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.Visible = False
        Me.Hide()
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        'LoadHistoryData()
        'Thread for Auto Reporting  Edit HERE BHavik DEsai New CODE
        thread_Load_History_and_TodayData = New Thread(AddressOf LoadHistoryData)
        thread_Load_History_and_TodayData.Name = "Load History and Today's Data"
        thread_Load_History_and_TodayData.IsBackground = True
        thread_Load_History_and_TodayData.Start()
        BackgroundWorker1.RunWorkerAsync()
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

    Public Sub GetAllENETMachines() 'This Function is used to load Ehub Config Files in Database 
        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try
            mySqlCnt.Open()
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
                For Each line As String In fileData
                    If line.Contains("NM:") Then
                        splitInput.Add(line.Replace("NM:", "")) 'Name of the ENET Machine
                    ElseIf line.Contains("FI:") Then
                        splitInput.Add(line.Replace("FI:", "")) 'Connection Type_ This value doesn't define all parameters but when it's 1 then it indicates FTP Connection
                    End If
                Next
                For Each line1 As String In MonData
                    If line1.Contains("ON:") Then 'Machine Monitoring Status If 1 then Monitoring is ON IF 0 then Monitoring is OFF
                        splitMonData.Add(line1.Replace("ON:", ""))
                    ElseIf line1.Contains("TH:") Then
                        splitMonData.Add(line1.Split(":")(1)) 'Stores the TH Parameter Values
                    ElseIf line1.Contains("DD:") Then
                        Dim FileFTPName As String = line1.Split(",")(1)
                        splitMonData.Add(FileFTPName)
                    ElseIf line1.Contains("DA") Then
                        splitMonData.Add(line1.Split(":")(1))
                    End If
                Next
                Dim fileLength As Integer = splitInput.Count
                Dim MonFilelength As Integer = splitMonData.Count
                Dim k As Integer, l As Integer, p As Integer, q As Integer, querycount As Integer, Counter As Integer, r As Integer
                Dim MonitoringId As String = ""
                Dim MachineName As String = ""
                Dim StringMonId As String = ""
                Dim Machine_Filename As String = ""
                Dim MachineLabel As String = ""
                Dim TH_Value As Integer
                Dim Monstate As Integer
                Dim FTPFileName As String
                Dim EnetDept As String
                'Dim MonSetUpId As Integer, 
                Dim Con_type As Integer
                Dim InsertQuerry As String
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
                            Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & "0" & ".SYS_" & "," & "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & "1" & ".SYS_"
                            InsertQuerry = "INSERT into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`,`Monstate`,`machine_label`,`ftpfilename`,`CurrentStatus`,`CurrentPartNumber`,`EnetDept`,`TH_State`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "','" & Monstate & "','" & MachineLabel & "','" & FTPFileName & "','','','" & EnetDept & "','" & TH_Value & "')ON DUPLICATE KEY UPDATE `machine_name`='" & MachineName & "',`Con_type`='" & Con_type & "';"
                        ElseIf TH_Value = 2 Then
                            '2 Pallet MonitorData010.SYS_,MonitorData011.SYS_
                            Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & "0" & ".SYS_" & "," & "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & "1" & ".SYS_"
                            InsertQuerry = "INSERT into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`,`Monstate`,`machine_label`,`ftpfilename`,`CurrentStatus`,`CurrentPartNumber`,`EnetDept`,`TH_State`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "','" & Monstate & "','" & MachineLabel & "','" & FTPFileName & "','','','" & EnetDept & "','" & TH_Value & "')ON DUPLICATE KEY UPDATE `machine_name`='" & MachineName & "',`Con_type`='" & Con_type & "';"
                        Else
                            Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & ".SYS_"
                            InsertQuerry = "INSERT into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`,`Monstate`,`machine_label`,`ftpfilename`,`CurrentStatus`,`CurrentPartNumber`,`EnetDept`,`TH_State`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "','" & Monstate & "','" & MachineLabel & "','" & FTPFileName & "','','','" & EnetDept & "','" & TH_Value & "')ON DUPLICATE KEY UPDATE `machine_name`='" & MachineName & "',`Con_type`='" & Con_type & "';"
                        End If
                        'InsertQuerry = "REPLACE into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "');"
                        Dim cmdEhubConfInsert = New MySqlCommand(InsertQuerry, mySqlCnt)
                        querycount = cmdEhubConfInsert.ExecuteNonQuery()
                        'querycount = Convert.ToInt32(cmdEhubConfInsert.ExecuteScalar())
                        'querycount = mysql_affected_rows()
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
                If Counter > 0 Then  'This Logic checking if there is any change in EHUb Config File 
                    'MessageBox.Show("Please Change the eNET Machine Name corresponfing to your Machine Name !")
                Else
                    'MessageBox.Show("No  Updates in EHubConfig File !")
                End If
            Else
                MessageBox.Show("EhubConf.sys Or MonSetUp.sys file is not exists or you don't have authority to access it")
            End If
        Catch ex As Exception
            MessageBox.Show("EhubConf Table has error while inserting data : " & ex.Message)
        Finally
            mySqlCnt.Close()
        End Try
    End Sub
    'Public Sub GetAllENETMachines() 'This Function is used to load Ehub Config Files in Database 
    '    Dim mySqlCnt As New MySqlConnection
    '    mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
    '    Try
    '        mySqlCnt.Open()
    '        Dim ehubfilepath As String = ""
    '        Dim Monsetupfilepath As String = ""
    '        Dim allconst As New AllStringConstants.StringConstant
    '        ehubfilepath = allconst.SERVER_ENET_PATH & allconst.EHUB_CONF_FILE_NAME
    '        Monsetupfilepath = allconst.SERVER_ENET_PATH & allconst.MON_SETUP_FILE_NAME
    '        If File.Exists(ehubfilepath) And File.Exists(Monsetupfilepath) Then
    '            Dim fileData() As String = File.ReadAllLines(ehubfilepath) 'Read All Lines from EhubConf file
    '            Dim MonData() As String = File.ReadAllLines(Monsetupfilepath) 'Read All Lines from MonSetUp file
    '            Dim splitInput As New List(Of String)
    '            Dim splitMonData As New List(Of String)
    '            For Each line As String In fileData
    '                If line.Contains("NM:") Then
    '                    splitInput.Add(line.Replace("NM:", "")) 'Name of the ENET Machine
    '                ElseIf line.Contains("FI:") Then
    '                    splitInput.Add(line.Replace("FI:", "")) 'Connection Type_ This value doesn't define all parameters but when it's 1 then it indicates FTP Connection
    '                End If
    '            Next
    '            For Each line1 As String In MonData
    '                If line1.Contains("ON:") Then 'Machine Monitoring Status If 1 then Monitoring is ON IF 0 then Monitoring is OFF
    '                    splitMonData.Add(line1.Replace("ON:", ""))
    '                ElseIf line1.Contains("DD:") Then
    '                    Dim FileFTPName As String = line1.Split(",")(1)
    '                    splitMonData.Add(FileFTPName)
    '                ElseIf line1.Contains("DA") Then
    '                    splitMonData.Add(line1.Split(":")(1))
    '                End If
    '            Next
    '            Dim fileLength As Integer = splitInput.Count
    '            Dim MonFilelength As Integer = splitMonData.Count
    '            Dim k As Integer, l As Integer, p As Integer, q As Integer, querycount As Integer, Counter As Integer, r As Integer
    '            Dim MonitoringId As String = ""
    '            Dim MachineName As String = ""
    '            Dim StringMonId As String = ""
    '            Dim Machine_Filename As String = ""
    '            Dim MachineLabel As String = ""
    '            Dim Monstate As Integer
    '            Dim FTPFileName As String
    '            Dim EnetDept As String
    '            'Dim MonSetUpId As Integer, 
    '            Dim Con_type As Integer
    '            Dim InsertQuerry As String
    '            k = 1
    '            l = 1
    '            p = 0
    '            q = 1
    '            r = 0
    '            querycount = 0
    '            Counter = 0
    '            While q < fileLength
    '                If k <= 16 And l <= 8 Then
    '                    MonitoringId = Convert.ToString(k) + "," + Convert.ToString(l)
    '                    MachineLabel = "MCH_" + Convert.ToString(k) + Convert.ToString(l)
    '                    Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & ".SYS_"
    '                    StringMonId = Convert.ToString(MonitoringId)
    '                    MachineName = Convert.ToString(splitInput(p))
    '                    'MonSetUpId = 0
    '                    If (Convert.ToInt32(splitInput(q))) = 1 Then 'Machines whose (FI:1) For future it will be called as Connection Type 5 where  (FI:1,EH:0 and EO:0) 
    '                        Con_type = 5
    '                    Else
    '                        Con_type = 0
    '                    End If
    '                    Monstate = Convert.ToInt32(splitMonData(r))
    '                    FTPFileName = splitMonData(r + 1).ToString()
    '                    EnetDept = splitMonData(r + 2).ToString()
    '                    'InsertQuerry = "REPLACE into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "');"
    '                    InsertQuerry = "INSERT into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`,`Monstate`,`machine_label`,`ftpfilename`,`CurrentStatus`,`CurrentPartNumber`,`EnetDept`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "','" & Monstate & "','" & MachineLabel & "','" & FTPFileName & "','','','" & EnetDept & "')ON DUPLICATE KEY UPDATE `machine_name`='" & MachineName & "',`Con_type`='" & Con_type & "';"
    '                    Dim cmdEhubConfInsert = New MySqlCommand(InsertQuerry, mySqlCnt)
    '                    querycount = cmdEhubConfInsert.ExecuteNonQuery()
    '                    'querycount = Convert.ToInt32(cmdEhubConfInsert.ExecuteScalar())
    '                    'querycount = mysql_affected_rows()
    '                    If querycount > 1 Then
    '                        Counter += 1
    '                    End If
    '                    l += 1
    '                    p += 2
    '                    q += 2
    '                    r += 3
    '                    If l > 8 Then
    '                        l = 1
    '                        k += 1
    '                    End If
    '                End If
    '            End While
    '            If Counter > 0 Then  'This Logic checking if there is any change in EHUb Config File 
    '                'MessageBox.Show("Please Change the eNET Machine Name corresponfing to your Machine Name !")
    '            Else
    '                'MessageBox.Show("No  Updates in EHubConfig File !")
    '            End If
    '        Else
    '            MessageBox.Show("EhubConf.sys Or MonSetUp.sys file is not exists or you don't have authority to access it")
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("EhubConf Table has error while inserting data : " & ex.Message)
    '    Finally
    '        mySqlCnt.Close()
    '    End Try
    'End Sub
    Public Sub thread_AutoLoadMachinePerf()
        GetAllENETMachines()
        Dim Today = DateTime.Now()
        Dim SearchMonthFolder = Today.ToString("yyyy-MM")
        Dim SearchDate = Today.ToString("MMMdd")
        'If TB_TMP.Text.Length > 0 And TB_Monitoring.Text.Length > 0 Then
        Dim allconst As New AllStringConstants.StringConstant
        Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try
            cntsql.Open()
            AllMachines.Clear()
            'Read all data from EhubConf File where monitoring is ON
            Dim mysql23 As String = "SELECT * FROM csi_auth.tbl_ehub_conf WHERE Monstate='1';"
            Dim cmdCreateDeviceTable23 As New MySqlCommand(mysql23, cntsql)
            Dim mysqlReader23 As MySqlDataReader = cmdCreateDeviceTable23.ExecuteReader
            Dim dTable_message23 As New DataTable()
            dTable_message23.Load(mysqlReader23)
            If dTable_message23.Rows.Count > 0 Then
                For Each row As DataRow In dTable_message23.Rows
                    If Not AllMachines.ContainsKey(row("monitoring_id").ToString()) Then
                        AllMachines.Add(row("monitoring_id"), New MachineData)
                    End If
                    AllMachines.Item(row("monitoring_id").ToString()).Machinename_ = row("machine_name").ToString()
                    AllMachines.Item(row("monitoring_id").ToString()).MonitoringFileName_ = row("monitoring_filename").ToString()
                    AllMachines.Item(row("monitoring_id").ToString()).MonitoringState_ = Integer.Parse(row("Monstate"))
                    AllMachines.Item(row("monitoring_id").ToString()).MonitoringID_ = row("monitoring_id").ToString()
                Next
            End If

            'Now get all renamevalues from database 
            'read machine_list_.csys
            'Update The Belowq Code When User Remove Monitoring For Machine but Machine still in Monlist File (Hint :: Get From Database)
            'Dim AllTablesMonitoringON As String = "SELECT * FROM csi_auth.tbl_ehub_conf where Monstate = '1'; "
            'Dim cmdAllTablesMonitoringON As New MySqlCommand(AllTablesMonitoringON, cntsql)
            'Dim mysqlReaderAllTablesMonitoringON As MySqlDataReader = cmdAllTablesMonitoringON.ExecuteReader
            'Dim dTable_AllTablesMonitoringON As New DataTable()
            'dTable_AllTablesMonitoringON.Load(mysqlReaderAllTablesMonitoringON)
            'If dTable_AllTablesMonitoringON.Rows.Count > 0 Then
            '    For Each Row As DataRow In dTable_AllTablesMonitoringON.Rows
            '        If Not DisplayMachineList.ContainsKey(Row.Item("machine_name").ToString()) Then
            '            DisplayMachineList.Add(Row.Item("machine_name").ToString(), String.Empty)
            '        End If
            '        DisplayMachineList.Item(Row.Item("machine_name").ToString()) = RenameMachine(machinename)
            '    Next
            'End If
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
            ' Dim source As DirectoryInfo = New DirectoryInfo(pathconst.SERVER_ENET_PATH & pathconst.MONITORING_FOLDER_PATH & SearchMonthFolder)
            Dim folderPath As String = allconst.SERVER_ENET_PATH & allconst.MONITORING_FOLDER_PATH & SearchMonthFolder
            If Directory.Exists(folderPath) Then
                'DELETE DATABASE 
                'Dim DropTable As String = "DROP DATABASE csi_machineperf;"
                'Dim cmddroptable As New MySqlCommand(DropTable, cntsql)
                'cmddroptable.ExecuteNonQuery()
                Dim CreateDB As String = "CREATE DATABASE IF NOT EXISTS csi_machineperf;"
                Dim cmdCreateDB As New MySqlCommand(CreateDB, cntsql)
                cmdCreateDB.ExecuteNonQuery()
                'Delete All the Tables From Database 
                Dim DeleteAllTables As String = "SELECT CONCAT('DROP TABLE IF EXISTS `', table_schema, '`.`', table_name, '`;') As AllTables FROM   information_schema.tables WHERE  table_schema = 'csi_machineperf'; "
                Dim cmdDeleteAllTables As New MySqlCommand(DeleteAllTables, cntsql)
                Dim mysqlReaderDeleteAllTables As MySqlDataReader = cmdDeleteAllTables.ExecuteReader
                Dim dTable_DeleteAllTables As New DataTable()
                dTable_DeleteAllTables.Load(mysqlReaderDeleteAllTables)
                If dTable_DeleteAllTables.Rows.Count > 0 Then
                    For Each Row As DataRow In dTable_DeleteAllTables.Rows
                        Dim SelectedTable As String = Row("AllTables").ToString()
                        Dim cmdSelectedTable As New MySqlCommand(SelectedTable, cntsql)
                        cmdSelectedTable.ExecuteNonQuery()
                    Next
                End If
                'Create Machine Perf Table 
                Dim cmdCREATTION As New MySqlCommand("CREATE TABLE if not exists csi_machineperf.tbl_perf(machinename_ varchar(255), weekly_ MEDIUMTEXT, monthly_ MEDIUMTEXT,  PRIMARY KEY (machinename_));", cntsql)
                cmdCREATTION.ExecuteNonQuery()
                'Folder with YEAR-CURRENTMONTH exist
                'Now check if Today's File exists file Format is :: MMMdd_MachineName_SHIFT1.MON
                For Each kvp As KeyValuePair(Of String, MachineData) In AllMachines
                    'Create Tables Fro Machine
                    For Each kvp2 As KeyValuePair(Of String, String) In DisplayMachineList
                        Dim match As Boolean = True
                        If kvp.Value.Machinename_ = kvp2.Key Then 'When machine display list and All Machine name matches only create those tables and ignore others 
                            Dim cmdCreateTables As MySqlCommand = New MySqlCommand("CREATE TABLE if not exists CSI_machineperf.tbl_" & kvp2.Value & " (status varchar(255), time integer, cycletime integer, shift integer, date DATETIME, UNIQUE KEY (time,status))", cntsql)
                            cmdCreateTables.ExecuteNonQuery()
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
                                        If Monlines.Contains("_PARTNO") Or Monlines.Contains("_DPRINT_") Or Monlines.Contains("_OPERATOR") Or Monlines.Contains("_SH_START") Or Monlines.Contains("_SH_END") Or Monlines.Contains("_SH_") Or (Monlines = String.Empty) Or (Monlines = "") Then 'Or lines1.Contains("NO eMONITOR")
                                            'If above patterns found don't add them to the database table
                                        Else
                                            'Add these entries to database table 
                                            If Monlines.Contains(",") Then 'This handles Empty String So we don't have any , in a string
                                                Dim MonSplitLines As String() = Monlines.Split(",")
                                                Dim Monjoindate As String = MonSplitLines(0) & "," & MonSplitLines(1)
                                                Dim Mononlytime As String() = (Convert.ToDateTime(MonSplitLines(1)).ToString("HH:mm:ss")).Split(":")
                                                Monnewdate = Convert.ToDateTime(Monjoindate)
                                                Dim Monlatestdate = Monnewdate.ToString("yyyy-MM-dd HH:mm:ss")
                                                Dim MonStatus As String = MonSplitLines(2)
                                                If MonStatus = "_CON" Then
                                                    MonStatus = "CYCLE ON"
                                                ElseIf MonStatus = "_COFF" Then
                                                    MonStatus = "CYCLE OFF"
                                                ElseIf MonStatus = "_SETUP" Then
                                                    MonStatus = "SETUP"
                                                End If
                                                'MessageBox.Show(Monlatestdate & " And " & MonStatus)
                                                Dim MonTIME_s As Integer
                                                MonTIME_s = Convert.ToInt32(Mononlytime(0)) * 3600 + Convert.ToInt32(Mononlytime(1)) * 60 + Convert.ToInt32(Mononlytime(2))
                                                ' MessageBox.Show("Seconds : " & MonTIME_s)

                                                Dim mysqlSelectRowsMon As String = "SELECT * FROM csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & ";"
                                                Dim cmdSelectRowsMon As New MySqlCommand(mysqlSelectRowsMon, cntsql)
                                                Dim mysqlReaderSelectRowsMon As MySqlDataReader = cmdSelectRowsMon.ExecuteReader
                                                Dim dTable_SelectRowsMon As New DataTable()
                                                dTable_SelectRowsMon.Load(mysqlReaderSelectRowsMon)
                                                If dTable_SelectRowsMon.Rows.Count = 0 Then
                                                    Dim Montimediff = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())
                                                    Dim cmdMon As MySqlCommand = New MySqlCommand("insert ignore into CSI_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & "(status,time,cycletime,shift,date) VALUES ('" & MonStatus & "', " & (MonTIME_s) & " , '" & Montimediff & "','" & tempshift & "', '" & Monlatestdate & "')", cntsql)
                                                    'CSI_Lib.LogServerError("cmdMon from count = 1 case MONITORING File INSERT QUERY LINE 370 ::: " & cmdMon.CommandText.ToString(),1)
                                                    cmdMon.ExecuteNonQuery()
                                                    Monlaststatustimestamp = Monnewdate
                                                    laststatustimestamp = Monnewdate
                                                ElseIf dTable_SelectRowsMon.Rows.Count > 0 Then
                                                    Dim Monlastcycletime = DateDiff(DateInterval.Second, Monlaststatustimestamp, Monnewdate)
                                                    'SET @select := (SELECT date FROM csi_machineperf.tbl_amada order by date desc limit 1,1);
                                                    'SET SQL_SAFE_UPDATES = 0;update csi_machineperf.tbl_amada SET cycletime='7896' Where date =@select;
                                                    ' Old :::: Dim mysqlUpdateMon As String = "update ignore CSI_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " SET `cycletime` = '" & Monlastcycletime & "';"
                                                    Dim mysqlUpdateMon As String = "SET @select := (SELECT date FROM csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " order by date desc limit 1);SET SQL_SAFE_UPDATES = 0;update  csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " SET `cycletime` = '" & Monlastcycletime & "' Where date =@select;"
                                                    ' CSI_Lib.LogServerError("mysqlUpdateMon from count = 1 case MONITORING File UPDATE QUERY LINE 380 ::: " & mysqlUpdateMon, 1)
                                                    Dim cmdmysqlUpdateMon As New MySqlCommand(mysqlUpdateMon, cntsql)
                                                    cmdmysqlUpdateMon.ExecuteNonQuery()
                                                    'Update the last line for cycletime and insert current like now() - dateTimestamp
                                                    Dim Montimediff1 = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())
                                                    Dim cmd As MySqlCommand = New MySqlCommand("insert ignore into CSI_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & "(status,time,cycletime,shift,date) VALUES ('" & MonStatus & "', " & (MonTIME_s) & " ,'" & Montimediff1 & "' ,'" & tempshift & "', '" & Monlatestdate & "')", cntsql)
                                                    'CSI_Lib.LogServerError("cmd from count = 1 case MONITORING File INSERT QUERY LINE 386 ::: " & cmd.CommandText.ToString(), 1)
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
                            'tempshift = 3
                            'Two Monfiles Curret Shift is 3
                            Dim fileno As Integer = 0
                            'Dim Monlaststatustimestamp As New DateTime
                            For Each monfiles As String In files
                                Dim splitMonfileForShift As Char = monfiles(monfiles.Length - 5)
                                tempshift = Convert.ToInt32(splitMonfileForShift.ToString())
                                If File.Exists(monfiles) Then
                                    Dim MonFileData As String() = File.ReadAllLines(monfiles)
                                    For Each Monlines As String In MonFileData
                                        If Monlines.Contains("_PARTNO") Or Monlines.Contains("_DPRINT_") Or Monlines.Contains("_OPERATOR") Or Monlines.Contains("_SH_START") Or Monlines.Contains("_SH_END") Or Monlines.Contains("_SH_") Or (Monlines = String.Empty) Or (Monlines = "") Then 'Or lines1.Contains("NO eMONITOR")
                                            'If above patterns found don't add them to the database table
                                        Else
                                            'Add these entries to database tables
                                            If Monlines.Contains(",") Then 'This if handles if the string we have is empty 
                                                Dim MonSplitLines As String() = Monlines.Split(",")
                                                Dim Monjoindate As String = MonSplitLines(0) & "," & MonSplitLines(1)
                                                Dim Mononlytime As String() = (Convert.ToDateTime(MonSplitLines(1)).ToString("HH:mm:ss")).Split(":")
                                                Monnewdate = Convert.ToDateTime(Monjoindate)
                                                Dim Monlatestdate = Monnewdate.ToString("yyyy-MM-dd HH:mm:ss")
                                                Dim MonStatus As String = MonSplitLines(2)
                                                If MonStatus = "_CON" Then
                                                    MonStatus = "CYCLE ON"
                                                ElseIf MonStatus = "_COFF" Then
                                                    MonStatus = "CYCLE OFF"
                                                ElseIf MonStatus = "_SETUP" Then
                                                    MonStatus = "SETUP"
                                                End If
                                                'MessageBox.Show(Monlatestdate & " And " & MonStatus)
                                                Dim MonTIME_s As Integer
                                                MonTIME_s = Convert.ToInt32(Mononlytime(0)) * 3600 + Convert.ToInt32(Mononlytime(1)) * 60 + Convert.ToInt32(Mononlytime(2))
                                                ' MessageBox.Show("Seconds : " & MonTIME_s)

                                                Dim mysqlSelectRowsMon As String = "SELECT * FROM csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & ";"
                                                Dim cmdSelectRowsMon As New MySqlCommand(mysqlSelectRowsMon, cntsql)
                                                Dim mysqlReaderSelectRowsMon As MySqlDataReader = cmdSelectRowsMon.ExecuteReader
                                                Dim dTable_SelectRowsMon As New DataTable()
                                                dTable_SelectRowsMon.Load(mysqlReaderSelectRowsMon)
                                                If dTable_SelectRowsMon.Rows.Count = 0 Then
                                                    Dim Montimediff = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())
                                                    Dim cmdMon As MySqlCommand = New MySqlCommand("insert ignore into CSI_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & "(status,time,cycletime,shift,date) VALUES ('" & MonStatus & "', " & (MonTIME_s) & " , '" & Montimediff & "','" & tempshift & "', '" & Monlatestdate & "')", cntsql)
                                                    'CSI_Lib.LogServerError("cmdMon from count = 2 case MONITORING File INSERT QUERY LINE 425 ::: " & cmdMon.CommandText.ToString(), 1)
                                                    cmdMon.ExecuteNonQuery()
                                                    Monlaststatustimestamp = Monnewdate
                                                    laststatustimestamp = Monnewdate
                                                ElseIf dTable_SelectRowsMon.Rows.Count > 0 Then
                                                    Dim Monlastcycletime = DateDiff(DateInterval.Second, Monlaststatustimestamp, Monnewdate)
                                                    Dim mysqlUpdateMon As String = "SET @select := (SELECT date FROM csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " order by date desc limit 1);SET SQL_SAFE_UPDATES = 0;update csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " SET `cycletime` = '" & Monlastcycletime & "' Where date =@select;"
                                                    ' CSI_Lib.LogServerError("mysqlUpdateMon from count = 2 case MONITORING File UPDATE QUERY LINE 432 ::: " & mysqlUpdateMon, 1)
                                                    Dim cmdmysqlUpdateMon As New MySqlCommand(mysqlUpdateMon, cntsql)
                                                    cmdmysqlUpdateMon.ExecuteNonQuery()
                                                    'Update the last line for cycletime and insert current like now() - dateTimestamp
                                                    Dim Montimediff1 = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())
                                                    Dim cmd As MySqlCommand = New MySqlCommand("insert ignore into CSI_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & "(status,time,cycletime,shift,date) VALUES ('" & MonStatus & "', " & (MonTIME_s) & " ,'" & Montimediff1 & "' ,'" & tempshift & "', '" & Monlatestdate & "')", cntsql)
                                                    ' CSI_Lib.LogServerError("cmd from count = 2 case MONITORING File INSERT QUERY LINE 438 ::: " & cmd.CommandText.ToString(), 1)
                                                    cmd.ExecuteNonQuery()
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
                                            If lines1.Contains("_PARTNO") Or lines1.Contains("_DPRINT_") Or lines1.Contains("_OPERATOR") Or (lines1 = String.Empty) Or (lines1 = "") Then 'Or lines1.Contains("NO eMONITOR")
                                                'IF Line Contains PARTNo, DPRINT,OPerator or Empty then don't add that line to database
                                            Else
                                                'Add this line to database table
                                                If lines1.Contains(",") Then 'This if handles if the string we have is empty 
                                                    Dim Splitlines1 As String() = lines1.Split(",")
                                                    Dim joindate As String = Splitlines1(0) & "," & Splitlines1(1)
                                                    Dim onlytime As String() = (Convert.ToDateTime(Splitlines1(1)).ToString("HH:mm:ss")).Split(":")
                                                    newDate = Convert.ToDateTime(joindate)
                                                    Dim latestdate = newDate.ToString("yyyy-MM-dd HH:mm:ss")
                                                    Dim Status As String = Splitlines1(2)
                                                    If Status = "_CON" Then
                                                        Status = "CYCLE ON"
                                                    ElseIf Status = "_COFF" Then
                                                        Status = "CYCLE OFF"
                                                    ElseIf Status = "_SETUP" Then
                                                        Status = "SETUP"
                                                    End If
                                                    'If Status = String.Empty Then

                                                    'End If
                                                    'MessageBox.Show(latestdate & " And " & Status)
                                                    Dim TIME_s As Integer
                                                    TIME_s = Convert.ToInt32(onlytime(0)) * 3600 + Convert.ToInt32(onlytime(1)) * 60 + Convert.ToInt32(onlytime(2))
                                                    'MessageBox.Show("Seconds : " & TIME_s)
                                                    'laststatustimestamp  = Monnewdate
                                                    'Select table and check for no of rows
                                                    Dim mysqlSelectRows As String = "SELECT * FROM csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & ";"
                                                    Dim cmdSelectRows As New MySqlCommand(mysqlSelectRows, cntsql)
                                                    Dim mysqlReaderSelectRows As MySqlDataReader = cmdSelectRows.ExecuteReader
                                                    Dim dTable_SelectRows As New DataTable()
                                                    dTable_SelectRows.Load(mysqlReaderSelectRows)
                                                    If dTable_SelectRows.Rows.Count = 0 Then
                                                        Dim timediff = 0
                                                        timediff = DateDiff(DateInterval.Second, newDate, DateTime.Now())
                                                        Dim cmd As MySqlCommand = New MySqlCommand("insert ignore into CSI_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & "(status,time,cycletime,shift,date) VALUES ('" & Status & "', " & (TIME_s) & " , '" & timediff & "','" & (tempshift + 1) & "', '" & latestdate & "')", cntsql)
                                                        'CSI_Lib.LogServerError("cmd for machine " & keyvalAllMachine.Value.Machinename_ & " case _TMP_ File INSERT QUERY LINE 482 ::: " & cmd.CommandText.ToString(), 1)
                                                        cmd.ExecuteNonQuery()
                                                        laststatustimestamp = newDate
                                                    ElseIf dTable_SelectRows.Rows.Count > 0 Then
                                                        Dim lastcycletime = DateDiff(DateInterval.Second, laststatustimestamp, newDate)
                                                        'Dim mysqlUpdate As String = "update ignore CSI_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " SET `cycletime` = '" & lastcycletime & "';"
                                                        Dim mysqlUpdate As String = "SET @select := (SELECT date FROM csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " order by date desc limit 1);update  csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " SET `cycletime` = '" & lastcycletime & "' Where date =@select;"
                                                        'CSI_Lib.LogServerError("mysqlUpdate for machine " & keyvalAllMachine.Value.Machinename_ & " case _TMP_ File  UPDATE QUERY LINE 489 ::: " & mysqlUpdate, 1)
                                                        Dim cmdmysqlUpdate As New MySqlCommand(mysqlUpdate, cntsql)
                                                        cmdmysqlUpdate.ExecuteNonQuery()
                                                        'Update the last line for cycletime and insert current like now() - dateTimestamp
                                                        Dim timediff1 = 0
                                                        timediff1 = DateDiff(DateInterval.Second, newDate, DateTime.Now())
                                                        Dim cmd As MySqlCommand = New MySqlCommand("insert ignore into CSI_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & "(status,time,cycletime,shift,date) VALUES ('" & Status & "', " & (TIME_s) & " ,'" & timediff1 & "' ,'" & (tempshift + 1) & "', '" & latestdate & "')", cntsql)
                                                        'CSI_Lib.LogServerError("cmd for machine " & keyvalAllMachine.Value.Machinename_ & " case _TMP_ File  INSERT QUERY LINE 496 ::: " & cmd.CommandText.ToString(), 1)
                                                        cmd.ExecuteNonQuery()
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
        Catch ex As Exception
            'If cntsql.State = ConnectionState.Open Then cntsql.Close()
        Finally
            cntsql.Close()
        End Try
        RefreshAllDevices()
        'If isCalledByMTConnect Then
        '    If thread_AutoLoadMachinePerf_thread.IsAlive Then
        '        thread_AutoLoadMachinePerf_thread.Abort()
        '    End If
        'Else
        'UNLOCK THIS THREAD:::::::::::::::::
        If thread_Load_History_and_TodayData.IsAlive Then
            thread_Load_History_and_TodayData.Abort()
            'Me.Close()
        End If
        Dim serviceLib As New CSIFlexServerService.ServiceLibrary
        serviceLib.Fill_REALTIME_dic()
        'Me.Close()
        'End If
    End Sub

    Public Sub RefreshAllDevices()

        Try
            'If Loading_TV_LivestatusMachine = False Then
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
            'End If

        Catch ex As Exception

            Log.Error("Unable to ask for refresh", ex)
            CSI_Lib.LogServerError("Unable to ask for refresh:" + ex.Message, 1)
        End Try

    End Sub

    Sub send_http_req()

        Try
            'If TabControl_DashBoard.SelectedTab IsNot Nothing Then
            'If TabControl_DashBoard.SelectedTab.Text = "Dashboards" Then
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

            dataStream.Close()
            ' Get the response.
            '  End If
            ' End If
        Catch ex As Exception

            Log.Error(ex)
            CSI_Lib.LogServerError("Unable to send http req" + ex.Message, 1)

        End Try

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

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        While True
            Thread.Sleep(1000)
            If Not thread_Load_History_and_TodayData.IsAlive Then
                Exit While
            End If
        End While
        Me.Close()
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object,
                                                     ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) _
                                                     Handles BackgroundWorker1.RunWorkerCompleted
        Me.Close()
    End Sub
End Class
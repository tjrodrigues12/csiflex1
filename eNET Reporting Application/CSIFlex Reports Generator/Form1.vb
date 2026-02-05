Imports System.IO
Imports System.IO.Compression
Imports System.Windows
Imports System.Globalization
'Imports CSI_Library
Imports System.Runtime.CompilerServices
Imports System.Text
Imports MySql.Data.MySqlClient
Imports System.Threading
Imports CSIFlex_Reporting
Imports System.ServiceProcess

Public Class CSIFlex_Reporting

    Private conn As MySqlConnection
    Private adap As MySqlDataAdapter
    Private ds As DataSet
    Private cmdBuild As MySqlCommandBuilder
    Private AddingBool As Boolean
    Private path As String ' = CSI_Lib.getRootPath()
    Private tasktochange As Integer = 0
    'Private taskname As String = ""
    Private modified As Boolean = False
    Private custommsg As String
    Public thread_Start_AutoReporting As Thread
    'Public Shared MySqlConnectionString As String = "server=192.168.1.131;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;SslMode=none;" 'SOMR
    Public Shared MySqlConnectionString As String = "server=localhost;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;SslMode=none;" 'SOMR
    Public Shared serverRootPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Server"  '"C:\ProgramData\CSI Flex Server"
    Public Shared ClientRootPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Client"  '"C:\ProgramData\CSI Flex Client"
    Public Shared MySqlServerBaseString As String = "user=client;password=csiflex123;port=3306;"

    Public Shared sqlitedbpath As String = "URI=file:" + ClientRootPath + "\sys\csisqlite.db3" '"URI=file:sys/csisqlite.db3"
    ' Public Shared CSI_Lib As New   Report_Generator
    Private Sub CSIFlex_Reporting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'CSI_Lib = New   Report_Generator

        path = Report_Generator.getRootPath()
        'Default Make Auto Reporting ON 
        createCSVforAutoReporting()
        loadCSVforAutoReporting()
        LockUnlockForm(False)
        CleanOldStuff()
        BTN_Remove.Enabled = True
        RB_Shift1.Enabled = False
        RB_Shift2.Enabled = False
        RB_Shift3.Enabled = False
        InstallAndStartService()
        'Do
        '    InstallAndStartService()
        'Loop While ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reporting") = ServiceTools.ServiceState.Stop 'Or ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reporting") = ServiceTools.ServiceState.Starting
    End Sub
    Private Sub InstallAndStartService()
        Try
            Dim serviceexepath As String = String.Empty
            serviceexepath = AppDomain.CurrentDomain.BaseDirectory.Replace("\bin\Debug", "\CSIFlex_Reporting\bin\Debug") + "CSIFlex_Reporting.exe"
            'serviceexepath = AppDomain.CurrentDomain.BaseDirectory + 'C:\Users\BDesai\Desktop\CSIFlex Reports Generator\CSIFlex Reports Generator Service\bin\Debug
            'Add your code here for Start Service On StartUp
            If (ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlex_Reporting")) Then
                If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reporting") = ServiceTools.ServiceState.Run) Or
                    (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reporting") = ServiceTools.ServiceState.Starting) Then
                    ' MessageBox.Show("CSIFlex Reporting Service is already running")
                Else
                    'MessageBox.Show("CSIFlex_Server is already installed")
                    ServiceTools.ServiceInstaller.StartService("CSIFlex_Reporting")
                    ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlex_Reporting")
                End If
                PB_Status.Visible = True
            Else
                ServiceTools.ServiceInstaller.InstallAndStart("CSIFlex_Reporting", "CSIFlex_Reporting", serviceexepath)
                ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlex_Reporting")
                Dim ServiceCont_CSIF As System.ServiceProcess.ServiceController
                ServiceCont_CSIF = New System.ServiceProcess.ServiceController("CSIFlex_Reporting")
                PB_Status.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error trying to start the service. See the log for more details :" & ex.Message)
            'CSI_Lib.LogServiceError(ex.Message, 1)
        End Try
    End Sub
    Private Sub StartStop()

        Dim service As ServiceController = New ServiceController("CSIFlex Report Service")

        If ((service.Status.Equals(ServiceControllerStatus.Stopped)) Or (service.Status.Equals(ServiceControllerStatus.StopPending))) Then

            service.Start()

        Else

            service.Stop()

        End If

    End Sub
    'Public Sub InstallService(Path As String)
    '    Dim dirPath As String = Path
    '    Dim cmd As String = "\"" & dirPath & " \ " install"
    'End Sub
    'Public Sub StartService(Path As String)

    'End Sub
#If False Then
      public static void Install(string exePath)
        {
            string dirPath = Path.GetDirectoryName(exePath);

            string cmd = "\"" + exePath + "\" install";
            // string cmd = "sc create ""+Names.AGENT_SERVICE_NAME+"" binPath= "+Paths.ADAPTERS +"";
            var info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "cmd /c cd \"" + dirPath + "\" & " + cmd;
            info.UseShellExecute = true;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();
        }

     public static void Start(string serviceName)
        {
            string cmd = "sc start " + serviceName;

            var info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "cmd /c" + cmd;
            info.UseShellExecute = true;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";

            var process = new Process();
            process.StartInfo = info;

            process.Start();
            process.WaitForExit();
            ServiceController sc;
            sc = new ServiceController(serviceName);
            sc.WaitForStatus(ServiceControllerStatus.Running);
        }
#End If

    Private Sub createCSVforAutoReporting()

        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(Report_Generator.MySqlConnectionString)
        mySqlCnt.Open()
        Dim table_ As New DataTable

        Dim mysql As String = "CREATE TABLE if not exists CSI_auth.Auto_Report_config (Task_name varchar(255) PRIMARY KEY, Day_ TEXT, Time_ TEXT,  Report_Type text, Output_Folder TEXT, MachineToReport TEXT, MailTo TEXT, Email_Time TEXT, done varchar(255) not null DEFAULT '0',  dayback TEXT, timeback TEXT, CustomMsg TEXT,shift_number varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,shift_starttime varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  shift_endtime varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL);"
        Dim cmdCreateDeviceTable As New MySqlCommand(mysql, mySqlCnt)
        cmdCreateDeviceTable.ExecuteNonQuery()
        Dim command As New MySqlCommand("SELECT * FROM csi_auth.auto_report_config;", mySqlCnt)
        Dim mysqlreader As MySqlDataReader = command.ExecuteReader
        table_.Load(mysqlreader)
        If table_.Columns.Count > 12 Then
            'Do nothing 
        Else
            '            ALTER TABLE `csi_auth`.`auto_report_config` 
            'ADD COLUMN `auto_report_configcol` VARCHAR(45) NULL AFTER `shift_endtime`;
            Dim AlterTable As String = " ALTER TABLE csi_auth.auto_report_config 
            ADD COLUMN `shift_number` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL AFTER `CustomMsg`,
            ADD COLUMN `shift_starttime` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL AFTER `shift_number`,
            ADD COLUMN `shift_endtime` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL AFTER `shift_starttime`;"
            Dim cmdAlterTable As New MySqlCommand(AlterTable, mySqlCnt)
            cmdAlterTable.ExecuteNonQuery()
        End If
        mySqlCnt.Close()

    End Sub
    Private Sub loadCSVforAutoReporting()


        ds = New DataSet()


        Dim tbl As New DataTable("tableDGV")

        tbl.Columns.Add("Task_name", GetType(String))
        tbl.Columns.Add("Day_", GetType(String))
        tbl.Columns.Add("Time_", GetType(String))
        tbl.Columns.Add("Report_Type", GetType(String))
        tbl.Columns.Add("Output_Folder", GetType(String))
        tbl.Columns.Add("MachineToReport", GetType(String))
        tbl.Columns.Add("MailTo", GetType(String))
        tbl.Columns.Add("Email_Time", GetType(String))

        For Each r As DataRow In DGV_tasks.Rows
            tbl.Rows.Add(DGV_tasks.Rows)
        Next

        ds.Tables.Add(tbl)




        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(Report_Generator.MySqlConnectionString)
        mySqlCnt.Open()


        Dim filePath As String = "CSI_auth.Auto_Report_config"
        Dim QryStr As String = String.Format("SELECT  * FROM {0} ", filePath)
        'Dim QryStr As String = String.Format("SELECT   Task_name,Time_,Report_Type,Output_Folder,CustomMsg,shift_number,shift_starttime,shift_endtime FROM ", filePath)

        adap = New MySqlDataAdapter(QryStr, mySqlCnt)
        adap.Fill(ds, "tableDGV")
        DGV_tasks.DataSource = ds.Tables(0)


        mySqlCnt.Close()

    End Sub
    Private Sub LockUnlockForm(enabledOrNot As Boolean)

        If DGV_tasks.Rows.Count = 0 Then
            BTN_Remove.Enabled = False
        Else
            BTN_Remove.Enabled = Not enabledOrNot
        End If


        BTN_Cancel.Enabled = enabledOrNot
        TB_TaskName.Enabled = enabledOrNot
        'BTN_CustomEmail.Enabled = enabledOrNot
        TB_OutPutFolder.Enabled = enabledOrNot
        DTP_Time.Enabled = enabledOrNot
        Try
            DGV_tasks.SelectedCells.Item(0).Selected = Not enabledOrNot
        Catch ex As Exception

        End Try


        DGV_tasks.Enabled = Not enabledOrNot
        CB_DOW.Enabled = enabledOrNot
        DGV_MachineList.Enabled = enabledOrNot
        dgv_mail.Enabled = enabledOrNot
        CBX_ReportType.Enabled = enabledOrNot
        BTN_Output.Enabled = enabledOrNot



        If enabledOrNot = False Then
            DGV_MachineList.ForeColor = Color.Gray
            dgv_mail.ForeColor = Color.Gray
            DGV_tasks.ForeColor = Color.Black
        Else
            DGV_MachineList.ForeColor = Color.Black
            dgv_mail.ForeColor = Color.Black
            DGV_tasks.ForeColor = Color.Gray
        End If

    End Sub
    Private Sub ClearBoard()

        Dim week As String() = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Every day"}

        CB_DOW.SelectedItem = -1
        For Each Day As String In week

            'If If(DGV_tasks.Rows.Count = 0, False, DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("Day").Value.ToString().Contains(Day)) Then
            'Else
            'DGV_DayOfWeek.Rows.Add({"false", Day})
        Next

    End Sub
    Private Function GetCustomMsg(taskname As String) As String
        Dim res As String = String.Empty '"Your report was generated succesfully"
        Dim sql As String = ("select CustomMsg from CSI_auth.Auto_Report_config where Task_name = '" + taskname + "'")
        Dim dt As New DataTable
        Try
            Using connection As New MySqlConnection(Report_Generator.MySqlConnectionString)
                connection.Open()
                Using mysqladap As New MySqlDataAdapter(sql, connection)
                    mysqladap.Fill(dt)
                End Using
                connection.Close()
            End Using

            If (dt.Rows.Count > 0) Then
                res = dt.Rows(0)("CustomMsg").ToString()
            End If
        Catch ex As Exception
            'CSI_Lib.LogServerError("Unable to retrieve custom message:" + ex.Message, 1)
            MessageBox.Show("Unable to retrieve custom message:" + ex.Message)
        End Try

        Return res
    End Function
    Private Sub clearForm()
        TB_TaskName.Text = String.Empty
        TB_OutPutFolder.Text = String.Empty
        CBX_ReportType.SelectedIndex = -1
        cb_d.Enabled = False
        cb_d.SelectedIndex = -1
        CB_DOW.SelectedIndex = -1
        CB_DOW.Enabled = False
        CB_WKND.Checked = True
        RB_Shift1.Enabled = False
        RB_Shift2.Enabled = False
        RB_Shift3.Enabled = False
        RB_Shift1.Checked = False
        RB_Shift2.Checked = False
        RB_Shift3.Checked = False
    End Sub
    Sub ClearGridDay()
        Dim week As String() = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Every day"}

        CB_DOW.SelectedItem = -1

    End Sub

    Sub ClearGridMachine()
        ':::::::::::::::::::: New Logic Get List From Database ::::::::::::::::::::::
        DGV_MachineList.Rows.Clear()
        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(Report_Generator.MySqlConnectionString)
        mySqlCnt.Open()
        Dim table_ As New DataTable
        Dim command As New MySqlCommand("SELECT * FROM csi_database.tbl_renamemachines;", mySqlCnt)
        Dim mysqlreader As MySqlDataReader = command.ExecuteReader
        table_.Load(mysqlreader)
        If table_.Rows.Count > 0 Then
            For Each row As DataRow In table_.Rows
                Dim MachineName = row("original_name").ToString()
                If Not MachineName = String.Empty Then
                    DGV_MachineList.Rows.Add({"false", MachineName})
                End If
            Next
        Else
        End If
        mySqlCnt.Close()
#If False Then
         ':::::::::::::::::::: Old Logic Get List From File \sys\machine_list_.csys::::::::::::::::::::::
        Dim readed As String


        Using reader As StreamReader = New StreamReader(path & "\sys\machine_list_.csys")
            DGV_MachineList.Rows.Clear()
            While Not reader.EndOfStream
                readed = reader.ReadLine().Split(",")(0)
                If Not readed.Contains("__") And Not (readed.Trim = "") Then
                    DGV_MachineList.Rows.Add({"false", readed})
                    'If If(DGV_tasks.Rows.Count = 0, False, DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("MachineName").Value.ToString().Contains(readed)) Then
                    '    DGV_MachineList.Rows.Add({"true", readed})
                    'Else
                    '    DGV_MachineList.Rows.Add({"false", readed})
                    'End If
                End If
            End While
        End Using
#End If


    End Sub

    Sub ClearGridMail()

        Dim table_ As New DataTable

        table_.Columns.Add("email_", GetType(String))

        Dim MySqlcnt As New MySqlConnection(Report_Generator.MySqlConnectionString)

        MySqlcnt.Open()

        Dim command As New MySqlCommand("SELECT email_ FROM csi_auth.Users", MySqlcnt)
        Dim mysqlreader As MySqlDataReader = command.ExecuteReader
        table_.Load(mysqlreader)

        MySqlcnt.Close()

        dgv_mail.Rows.Clear()

        Dim i As Integer = 0


        For Each row_ As DataRow In table_.Rows
            dgv_mail.Rows.Add({"false", row_("email_")})
        Next
        dgv_mail.PerformLayout()
        'Dim arr As List(Of String) = New List(Of String)
        'arr.Add("asom@moldmaster.com")
        'arr.Add("bpatel@moldmaster.com")
        'arr.Add("Brossm@moldmaster.com")
        'arr.Add("BChisolm@moldmaster.com")
        'arr.Add("Esomthingm@moldmaster.com")
        'arr.Add("Emiddm@moldmaster.com")
        'arr.Add("Krud@moldmaster.com")
        'arr.Add("M5pierce@moldmaster.com")
        'arr.Add("Mtaylor@moldmaster.com")
        'arr.Add("ammsolfm@moldmaster.com")
        'arr.Add("Mkhanl@moldmaster.com")
        'arr.Add("Ploi@moldmaster.com")
        'arr.Add("Phallerr@moldmaster.com")
        'arr.Add("Pharrris@moldmaster.com")
        'arr.Add("Rvangg@moldmaster.com")
        'arr.Add("Stpay@moldmaster.com")
        'arr.Add("Sssaavom@moldmaster.com")
        'arr.Add("sIzu@moldmaster.com")
        'For Each item In arr
        '    dgv_mail.Rows.Add({"true", item})
        'Next
        'Add Random email for testing scroll exception
        'For i = 0 To 30
        '    dgv_mail.Rows.Add({"false", "email" + i.ToString() + "@outlook.com"})
        'Next


    End Sub
    Private Function getListOfSelectedMachine() As String

        Dim result As New System.Text.StringBuilder()
        Dim returnStr As String
        For i As Integer = 0 To DGV_MachineList.RowCount - 1

            If (Boolean.Parse(DGV_MachineList.Rows.Item(i).Cells(0).Value) = True) Then
                result.Append(DGV_MachineList.Rows.Item(i).Cells(1).Value)
                result.Append(If(i = DGV_MachineList.RowCount - 1, "", ";"))
            End If
        Next

        Try
            If result.ToString().EndsWith(";") Then result.Remove(result.Length - 1, 1)
            returnStr = result.ToString() '.Substring(0, result.ToString().Length - 1)
        Catch ex As Exception
            returnStr = ""
        End Try


        Return (returnStr)

    End Function


    Private Function getListOfDaySelected() As String

        'Dim result As New System.Text.StringBuilder()
        'If Boolean.Parse(DGV_DayOfWeek.Rows.Item(7).Cells(0).Value) = True Then
        '    For i As Integer = 0 To 6
        '        result.Append(DGV_DayOfWeek.Rows.Item(i).Cells(1).Value)
        '        result.Append(If(i = DGV_DayOfWeek.RowCount - 2, "", ";"))
        '    Next
        'Else
        '    For i As Integer = 0 To 6

        '        If (Boolean.Parse(DGV_DayOfWeek.Rows.Item(i).Cells(0).Value) = True) Then

        '            result.Append(DGV_DayOfWeek.Rows.Item(i).Cells(1).Value)
        '            result.Append(If(i = DGV_DayOfWeek.RowCount - 2, "", ";"))
        '        End If
        '    Next
        'End If
        Try
            If (CBX_ReportType.SelectedIndex = 1 Or CBX_ReportType.SelectedIndex = 0) Then
                If CB_WKND.Checked Then
                    Return "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday"
                Else
                    'Return "Tuesday, Wednesday, Thursday, Friday, Saturday"
                    Return "Monday, Tuesday, Wednesday, Thursday, Friday"
                End If

            Else
                Return CB_DOW.SelectedItem.ToString()
            End If

        Catch ex As Exception
            Return Nothing
        End Try


        'Return (result.ToString())

    End Function
    Private Function getlistOfMailSelected() As String

        Dim result As New System.Text.StringBuilder()
        Dim returnStr As String
        For i As Integer = 0 To dgv_mail.RowCount - 1

            If (Boolean.Parse(dgv_mail.Rows.Item(i).Cells(0).Value) = True) Then
                result.Append(dgv_mail.Rows.Item(i).Cells(1).Value)
                result.Append(If(i = dgv_mail.RowCount - 1, "", ";"))
            End If
        Next

        Try
            returnStr = result.ToString().Substring(0, result.ToString().Length)
        Catch ex As Exception
            returnStr = ""
        End Try

        Return returnStr

    End Function
    Private Sub ReportsChanged()
        Try
            'If File.Exists(path & "\sys\reportingchanged.csys") Then File.Delete(path & "\sys\reportingchanged.csys")
            Using writer As StreamWriter = New StreamWriter(path & "\sys\reportingchanged.csys", False)
                writer.Write(True)
                writer.Close()
            End Using
        Catch ex As Exception
            'CSI_Lib.LogServerError("Unable to write reportingchanged. Exception:" + ex.Message, 1)
            MessageBox.Show("Unable to write reportingchanged. Exception:" + ex.Message)
        End Try
    End Sub
    Private Sub loadMachineGrid()
        ':::::::::::::::::::: New Logic Get List From Database ::::::::::::::::::::::
        DGV_MachineList.Rows.Clear()
        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(Report_Generator.MySqlConnectionString)
        mySqlCnt.Open()
        Dim table_ As New DataTable
        Dim command As New MySqlCommand("SELECT * FROM csi_database.tbl_renamemachines;", mySqlCnt)
        Dim mysqlreader As MySqlDataReader = command.ExecuteReader
        table_.Load(mysqlreader)
        If table_.Rows.Count > 0 Then
            For Each row As DataRow In table_.Rows
                Dim MachineName = row("original_name").ToString()
                If Not MachineName = String.Empty Then
                    If If(DGV_tasks.Rows.Count = 0, False, DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("MachineName").Value.ToString().Contains(MachineName)) Then
                        DGV_MachineList.Rows.Add({"true", MachineName})
                    Else
                        DGV_MachineList.Rows.Add({"false", MachineName})
                    End If
                End If
            Next
        Else
        End If
        mySqlCnt.Close()
#If False Then
         ':::::::::::::::::::: Old Logic Get List From File \sys\machine_list_.csys::::::::::::::::::::::
        Dim readed As String

        Using reader As StreamReader = New StreamReader(path & "\sys\machine_list_.csys")
            DGV_MachineList.Rows.Clear()
            While Not reader.EndOfStream
                readed = reader.ReadLine().Split(",")(0)
                If Not readed.Contains("__") And Not (readed.Trim = "") Then
                    If If(DGV_tasks.Rows.Count = 0, False, DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("MachineName").Value.ToString().Contains(readed)) Then
                        DGV_MachineList.Rows.Add({"true", readed})
                    Else
                        DGV_MachineList.Rows.Add({"false", readed})
                    End If
                End If
            End While
        End Using
#End If

    End Sub
    Private Sub loadMailGrid()

        ClearGridMail()

        For Each row_ As DataGridViewRow In dgv_mail.Rows
            Dim b = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells(7).Value



            If Not (DGV_tasks.Rows.Count = 0 Or (b Is Nothing)) Then

                If (b.Contains(row_.Cells(1).Value.ToString())) Then
                    row_.Cells(0).Value = "true"
                Else
                    row_.Cells(0).Value = "false"
                End If

            Else

                row_.Cells(0).Value = "false"

            End If


        Next

    End Sub
    Private Sub bindDGVtoForm()

        loadMachineGrid()

        loadMailGrid()
        TB_TaskName.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("task_name").Value.ToString()

        DTP_Time.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("Time").Value.ToString()
        CBX_ReportType.SelectedIndex = CBX_ReportType.FindStringExact(DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("Report_Type").Value.ToString())
        TB_OutPutFolder.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("Output_Folder").Value.ToString()
        cb_d.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("dayback").Value.ToString()
        'shift_numbers
        If DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("shift_number").Value.ToString() = "1" Then
            RB_Shift1.Checked = True
        ElseIf DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("shift_number").Value.ToString() = "2" Then
            RB_Shift2.Checked = True
        ElseIf DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("shift_number").Value.ToString() = "3" Then
            RB_Shift3.Checked = True
        End If
        cb_t.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("shift_starttime").Value.ToString()
        CB_endtime.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("shift_endtime").Value.ToString()
        'cb_t.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("shift_endtime").Value.ToString()
        'If DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("Report_Type").Value.ToString() = "Daily ( Today ) Availability - PDF" Then
        '    If DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("shift_number").Value.ToString() = "1" Then
        '        CB_Shift1.Checked = True
        '        CB_Shift1.Enabled = True
        '    ElseIf DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("shift_number").Value.ToString() = "2" Then
        '        CB_Shift2.Checked = True
        '        CB_Shift2.Enabled = True
        '    ElseIf DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("shift_number").Value.ToString() = "3" Then
        '        CB_Shift3.Checked = True
        '        CB_Shift3.Enabled = True
        '    Else
        '        'means empty
        '        CB_Shift1.Enabled = False
        '        CB_Shift2.Enabled = False
        '        CB_Shift3.Enabled = False
        '    End If
        '    TB_TaskName.Enabled = True
        '    CBX_ReportType.Enabled = True
        '    DTP_Time.Enabled = True
        '    TB_OutPutFolder.Enabled = True
        '    CB_DOW.Enabled = False
        '    CB_DOW.SelectedIndex = -1
        'Else

        'End If
        loadDayOfWeekGrid()
        '    cb_t.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("timeback").Value.ToString().Substring(12)

    End Sub
    Private Sub CleanOldStuff()
        BTN_Add.Text = "Add New"
        BTN_CustomEmail.Enabled = False
        BTN_Modify.Text = "Modify"
        BTN_Modify.Enabled = False
        CB_WKND.Checked = False
        CB_WKND.Enabled = False
        AddingBool = False
        LockUnlockForm(False)
        BTN_Remove.Enabled = False
        BTN_Add.Enabled = True
        TB_TaskName.Clear()
        TB_OutPutFolder.Clear()
        'CB_Shift1.Enabled = False
        'CB_Shift2.Enabled = False
        'CB_Shift3.Enabled = False
        'CB_Shift1.Checked = False
        'CB_Shift2.Checked = False
        'CB_Shift3.Checked = False
        ClearGridDay()
        ClearGridMail()
        ClearGridMachine()
        CBX_ReportType.SelectedIndex = -1
        CB_DOW.SelectedIndex = -1
    End Sub
    Private Sub loadDayOfWeekGrid()

        Dim week As String() = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}


        CB_DOW.SelectedItem = -1


        For Each Day As String In week

            If If(DGV_tasks.Rows.Count = 0, False, DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("Day").Value.ToString().Contains(Day)) Then
                CB_DOW.SelectedItem = Day
                If CBX_ReportType.SelectedIndex = 1 Then
                    If Day = "Sunday" Then
                        CB_WKND.Checked = True
                        CB_DOW.SelectedIndex = -1
                    Else
                        CB_DOW.SelectedIndex = -1
                    End If
                Else
                    CB_WKND.Checked = False
                End If

            End If

        Next

    End Sub
    Private Sub BTN_Add_Click(sender As Object, e As EventArgs) Handles BTN_Add.Click
        If AddingBool = False Then
            custommsg = String.Empty
            'Old Code
            BTN_Modify.Enabled = False
            BTN_Remove.Enabled = False
            clearForm()
            'loadMachineGrid()
            'loadDayOfWeekGrid()
            LockUnlockForm(True)
            BTN_Add.Text = "Save"
            AddingBool = True
            'ClearBoard()
            'TB_TaskName.Clear()
            'TB_OutPutFolder.Clear()
            cb_d.Enabled = False
            cb_t.Enabled = True
            CB_endtime.Enabled = True
            ClearGridDay()
            ClearGridMail()
            ClearGridMachine()
            'CBX_ReportType.SelectedIndex = -1
            cb_d.SelectedIndex = -1
            CB_DOW.SelectedIndex = -1
            CB_DOW.Enabled = False
            CB_WKND.Enabled = True
            CB_WKND.Checked = True
            clearForm()
            'New Code
            'CleanOldStuff()
        ElseIf (getListOfSelectedMachine() <> "" And (getListOfDaySelected() <> "" Or (CBX_ReportType.SelectedIndex = 0 Or CBX_ReportType.SelectedIndex = 1 Or CBX_ReportType.SelectedIndex = 3)) And TB_TaskName.Text.ToString() <> "" And TB_OutPutFolder.Text.ToString() <> "") And (cb_d.Text <> "" Or (CBX_ReportType.SelectedIndex = 0 Or CBX_ReportType.SelectedIndex = 1 Or CBX_ReportType.SelectedIndex = 3)) Then
            cb_d.Enabled = False
            cb_t.Enabled = False
            CB_endtime.Enabled = False

            If IsNumeric(TB_TaskName.Text.ToString()) = False Then
                cmdBuild = New MySqlCommandBuilder(adap)

                'If (BTN_Modify.Text = "Modify") Then
                '    TB_TaskName.Text = taskname
                'End If

                'Dim custommsg = GetCustomMsg(TB_TaskName.Text)
                Dim filtered = ds.Tables(0).AsEnumerable().Where(Function(r) r.Field(Of [String])("Task_name").Equals(TB_TaskName.Text))

                If (modified) Then
                    DGV_tasks.Rows.RemoveAt(tasktochange)
                    ds.Tables(0).Rows.RemoveAt(tasktochange)
                    modified = False
                End If

                If (filtered.Count = 0) Then
                    Try
                        Dim newRowToDGV As DataRow = ds.Tables(0).NewRow()

                        newRowToDGV("Task_name") = TB_TaskName.Text
                        newRowToDGV("Day_") = getListOfDaySelected()
                        newRowToDGV("Time_") = DTP_Time.Text
                        newRowToDGV("Report_Type") = CBX_ReportType.Text
                        newRowToDGV("Output_Folder") = TB_OutPutFolder.Text
                        newRowToDGV("MachineToReport") = getListOfSelectedMachine()
                        newRowToDGV("MailTo") = getlistOfMailSelected()
                        newRowToDGV("dayback") = cb_d.Text
                        newRowToDGV("timeback") = "1900-01-01 " + cb_t.Text
                        newRowToDGV("CustomMsg") = custommsg
                        If RB_Shift1.Checked = True Then
                            newRowToDGV("shift_number") = "1"
                        ElseIf RB_Shift2.Checked = True Then
                            newRowToDGV("shift_number") = "2"
                        ElseIf RB_Shift3.Checked = True Then
                            newRowToDGV("shift_number") = "3"
                        Else
                            newRowToDGV("shift_number") = ""
                        End If
                        newRowToDGV("shift_starttime") = cb_t.Text
                        newRowToDGV("shift_endtime") = CB_endtime.Text
                        ds.Tables(0).Rows.Add(newRowToDGV)

                        'If ds.Tables(0).Rows.Count = 1 Then
                        '    Dim sql As String = ("delete from CSI_auth.Auto_Report_config")
                        '    Dim connection As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                        '    Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)

                        '    Try
                        '        connection.Open()
                        '        mysqlcomm.ExecuteNonQuery()
                        '        connection.Close()
                        '    Catch
                        '    End Try
                        'End If


                        adap.Update(ds, ds.Tables(0).TableName)
                        ReportsChanged()



                        'BTN_Add.Text = "Add New"
                        AddingBool = False

                        LockUnlockForm(False)

                        'ClearGridDay()
                        'ClearGridMachine()

                        'loadMachineGrid()
                        'loadDayOfWeekGrid()

                        'clearForm()
                        CleanOldStuff()
                        BTN_Remove.Enabled = True
                        BTN_Modify.Enabled = True
                        If Not (DGV_tasks.CurrentRow Is Nothing) And AddingBool = False Then
                            bindDGVtoForm()
                        End If
                        CB_WKND.Enabled = False
                        clearForm()
                    Catch ex As Exception
                        'CSI_Lib.LogServerError("Unable to add report:" + ex.Message, 1)
                        MessageBox.Show("Unable to add report:" + ex.Message)
                    End Try
                Else
                    MessageBox.Show("This task name is already in use")
                End If


            Else
                MessageBox.Show("Please do not enter numbers")
            End If

        Else
            MessageBox.Show("Error! Make sure you have completed all the steps below: " & vbNewLine & vbNewLine & "-Enter a task name" & vbNewLine & "-Enter an output destination" & vbNewLine & "-Select at least one machine" & vbNewLine & "-Select one day")
        End If
    End Sub
    Private Sub DGV_tasks_SelectionChanged(sender As Object, e As EventArgs) Handles DGV_tasks.SelectionChanged
        Try
            If DGV_tasks.SelectedCells.Item(0).RowIndex >= 0 Then
                BTN_Modify.Enabled = True
                BTN_CustomEmail.Enabled = False
                BTN_Remove.Enabled = True
                'RB_Shift1.Enabled = False
                'RB_Shift2.Enabled = False
                'RB_Shift3.Enabled = False
            End If
        Catch ex As Exception
            If BTN_Modify.Text = "Modify" Then
                BTN_Modify.Enabled = False
                BTN_CustomEmail.Enabled = False
                'BTN_Remove.Enabled = False
            End If

        End Try

        If Not (DGV_tasks.CurrentRow Is Nothing) And AddingBool = False Then
            bindDGVtoForm()
        End If
    End Sub
    Public Sub GenerateReports()
        If TB_TaskName.Text.Length > 0 Then
            Dim DayName As String = Convert.ToString(Now.DayOfWeek)
            'Dim autoReport As Boolean = getAutoReportOrNot()

            Try
                If (check_tableExists("csi_auth", "Auto_Report_config")) Then 'And autoReport = True

                    'csi_lib.Log_server_event(Date.Now & ", " & "Autoreporting : generating reports")

                    Dim Dataset_AutoReport As DataSet = New DataSet()
                    Dim conn As MySqlConnection = New MySqlConnection()
                    Dim adap As New MySqlDataAdapter()
                    Dim listOfMachine As String()

                    conn.ConnectionString = Report_Generator.MySqlConnectionString
                    conn.Open()

                    If (DayName <> Convert.ToString(Convert.ToInt32(System.DateTime.Now.DayOfWeek))) Then
                        resetDoneReport()
                    End If

                    Dim dayOfNow As DateTime = Now()
                    'DayName = Convert.ToString(dayOfNow.DayOfWeek)
                    DayName = Convert.ToString(dayOfNow.DayOfWeek)
                    'Dim DayName1 As String = dayOfNow.DayOfWeek.ToString()
                    Dim timeNow As String = dayOfNow.ToString("HH:mm")

                    Dim QryStr As String = "SELECT * FROM csi_auth.auto_report_config WHERE Task_name = '" & TB_TaskName.Text & "';" 'TB_TaskName.Text 
                    Dim fileToSend As String

                    adap = New MySqlDataAdapter(QryStr, conn)
                    adap.Fill(Dataset_AutoReport, "tableDGV")

                    'Generate Reports for each row which not yet been generated
                    For Each row As DataRow In Dataset_AutoReport.Tables("tableDGV").Rows
                        Dim custommsg As String = Convert.ToString(row("CustomMsg"))
                        If custommsg.Length = 0 Then
                            If Convert.ToString(row("Report_Type")) = "Daily ( Today ) Availability - PDF" Then
                                'Today
                                custommsg = "Your Today's CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                            ElseIf Convert.ToString(row("Report_Type")) = "Monthly Availability - PDF" Then
                                'Monthly Reporting
                                custommsg = "Your Monthly CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                            ElseIf Convert.ToString(row("Report_Type")) = "Weekly Availability - PDF" Then
                                'Weekly Reporting
                                custommsg = "Your Weekly CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                            ElseIf Convert.ToString(row("Report_Type")) = "Daily ( Yesterday ) Availability - PDF" Then
                                'Daily ( Yesterday ) Availability - PDF
                                custommsg = "Your Daily CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                            End If
                        Else
                            custommsg = Convert.ToString(row("CustomMsg"))
                        End If
                        Try

                            listOfMachine = Convert.ToString(row("MachineToReport")).Split(";")



                            Dim SEvent(0 To listOfMachine.Count() - 1, 1) As String
                            For i = 0 To listOfMachine.Count() - 1
                                SEvent(i, 0) = listOfMachine(i)
                                SEvent(i, 1) = "SETUP"
                            Next
                            If Convert.ToString(row("Report_Type")) = "Daily ( Today ) Availability - PDF" Then
                                'MessageBox.Show(System.DateTime.Now.ToString("yyyy-MM-dd"))
                                Dim TodayDate As String = System.DateTime.Now.ToString("dd")
                                Dim TodayMonth As String = System.DateTime.Now.ToString("MM")
                                Dim YearNow As String = System.DateTime.Now.ToString("yyyy")
                                Dim todaystart As String = row("shift_starttime").ToString()
                                Dim Todayend As String = row("shift_endtime").ToString()
                                Dim todaystart_split_date_time() As String = todaystart.Split(":")
                                Dim todayend_split_date_time() As String = Todayend.Split(":")
                                Dim startdate As DateTime
                                Dim enddate As DateTime
                                startdate = New DateTime(YearNow, TodayMonth, TodayDate, todaystart_split_date_time(0), todaystart_split_date_time(1), 0)
                                enddate = New DateTime(YearNow, TodayMonth, TodayDate, todayend_split_date_time(0), todayend_split_date_time(1), 0)
                                fileToSend = Report_Generator.generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, Convert.ToString(row("Task_name")))

                                Try
                                    If Not (row("MailTo") Is Nothing) Then
                                        Report_Generator.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                                    End If
                                Catch ex As Exception
                                    'CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                                    MessageBox.Show("Error trying to send report by email:" & ex.ToString())
                                End Try
                                updateDoneReport(row("Task_name"))
                                ' Forms.MessageBox.Show(startdate)
                                'Forms.MessageBox.Show(enddate)
                            ElseIf Convert.ToString(row("Report_Type")) = "Monthly Availability - PDF" And Convert.ToInt32(Convert.ToString(dayOfNow.Day)) = 1 Then
                                'run monthly for last month

                                Dim startdate As DateTime
                                Dim enddate As DateTime
                                enddate = dayOfNow
                                startdate = dayOfNow.AddMonths(-1)
                                startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                                enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                                fileToSend = Report_Generator.generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, Convert.ToString(row("Task_name")))

                                Try
                                    If Not (row("MailTo") Is Nothing) Then
                                        Report_Generator.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                                    End If
                                Catch ex As Exception
                                    'CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                                    MessageBox.Show("Error trying to send report by email:" & ex.ToString())
                                End Try
                                updateDoneReport(row("Task_name"))

                            ElseIf (Convert.ToString(row("Report_Type")) = "Weekly Availability - PDF") Then

                                'run weekly report for last week
                                Dim startdate As DateTime
                                Dim enddate As DateTime

                                Dim days As Integer = 7
                                Try
                                    If Not (row("dayback") Is Nothing) Then
                                        days = Integer.Parse(Convert.ToString(row("dayback")))
                                    End If
                                Catch ex As Exception

                                End Try


                                enddate = dayOfNow.AddDays(-1)
                                'startdate = dayOfNow.AddDays(-(days - 1))
                                startdate = dayOfNow.AddDays(-days)
                                startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                                enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                                fileToSend = Report_Generator.generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, Convert.ToString(row("Task_name")))

                                Try
                                    If Not (row("MailTo") Is Nothing) Then
                                        Report_Generator.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                                    End If
                                Catch ex As Exception
                                    'CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                                    MessageBox.Show("Error trying to send report by email:" & ex.ToString())
                                End Try
                                updateDoneReport(row("Task_name"))

                            ElseIf (Convert.ToString(row("Report_Type")) = "Daily ( Yesterday ) Availability - PDF") Then
                                'run daily for last day

                                Dim startdate As DateTime
                                Dim enddate As DateTime
                                enddate = dayOfNow.AddDays(-1)
                                startdate = dayOfNow.AddDays(-1)
                                startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                                enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                                fileToSend = Report_Generator.generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, Convert.ToString(row("Task_name")))

                                Try
                                    If Not (row("MailTo") Is Nothing) Then
                                        Report_Generator.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                                    End If
                                Catch ex As Exception
                                    'CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                                    MessageBox.Show("Error trying to send report by email:" & ex.ToString())
                                End Try
                                updateDoneReport(row("Task_name"))
                            Else
                                'CSI_Lib.LogServiceError(DateTime.Now.ToString() + "invalid report type:" + row("Report_Type").ToString() + "TASK:" + row("Task_name").ToString(), 1)
                                MessageBox.Show(DateTime.Now.ToString() + "invalid report type:" + row("Report_Type").ToString() + "TASK:" + row("Task_name").ToString())
                            End If

                        Catch ex As Exception
                            'CSI_Lib.LogServiceError("Error generating autoreports, MSG:" & ex.ToString(), 1)
                            MessageBox.Show("Error generating autoreports, MSG:" & ex.ToString())
                            If conn.State = ConnectionState.Open Then conn.Close()

                        End Try
                    Next
                    If conn.State = ConnectionState.Open Then conn.Close()
                End If


            Catch ex As Exception
                'CSI_Lib.LogServiceError("Error in GenerateReports():" & ex.ToString() + vbCrLf + "__>" + ex.StackTrace, 1)
                MessageBox.Show("Error in GenerateReports():" & ex.ToString() + vbCrLf + "__>" + ex.StackTrace)
            End Try
        Else
            MessageBox.Show("Please select a report to generate !")
        End If

    End Sub
    Private Sub Btn_gen_Click(sender As Object, e As EventArgs) Handles Btn_gen.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            GenerateReports()
            Me.Cursor = Cursors.Default
            'Dim Type As String = ""
            'If CBX_ReportType.SelectedItem.ToString().Contains("Weekly") Then
            '    Type = "Weekly"
            'ElseIf CBX_ReportType.SelectedItem.ToString().Contains("Monthly") Then
            '    Type = "Monthly"
            'ElseIf CBX_ReportType.SelectedItem.ToString().Contains("Today") Then
            '    Type = "Today"
            'ElseIf CBX_ReportType.SelectedItem.ToString().Contains("Daily") Then
            '    Type = "Daily"
            'End If
            ''Dim dayOfNow As DateTime = Now()
            ''Dim timeNow As String = dayOfNow.ToString("HH:mm")
            '' from Servicelibrary.vb ln 3483

            'Try
            '    Dim Dataset_AutoReport As DataSet = New DataSet()
            '    Dim conn As MySqlConnection = New MySqlConnection()
            '    Dim adap As New MySqlDataAdapter()

            '    conn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString
            '    conn.Open()

            '    Dim dayOfNow As DateTime = Now()
            '    Dim DayName As String = dayOfNow.DayOfWeek.ToString()
            '    Dim timeNow As String = dayOfNow.ToString("HH:mm")
            '    Dim QryStr As String
            '    Dim custommsg As String
            '    QryStr = String.Format("SELECT * FROM csi_auth.{0} where (Task_name like '%" + TB_TaskName.Text + "%'  )", "Auto_Report_config")


            '    Dim fileToSend As String = ""

            '    adap = New MySqlDataAdapter(QryStr, conn)
            '    adap.Fill(Dataset_AutoReport, "tableDGV")
            '    If Dataset_AutoReport.Tables(0).Rows.Count > 0 Then
            '        For Each row As DataRow In Dataset_AutoReport.Tables("tableDGV").Rows
            '            Dim listOfMachine As String()
            '            listOfMachine = row("MachineToReport").ToString().Split(";")

            '            Dim SEvent(0 To listOfMachine.Count() - 1, 1) As String
            '            For i = 0 To listOfMachine.Count() - 1
            '                SEvent(i, 0) = listOfMachine(i)
            '                SEvent(i, 1) = "SETUP"
            '            Next
            '            If (Convert.ToString(row("CustomMsg")).Length = 0) Then
            '                custommsg = "Your " & Type & " CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
            '            Else
            '                custommsg = Convert.ToString(row("CustomMsg"))
            '            End If
            '            If row("Report_Type").ToString() = "Monthly Availability - PDF" Or Convert.ToInt32(dayOfNow.Day.ToString()) = 1 Then
            '                '     If row("Report_Type").ToString() = "Monthly Availability - PDF" Or Convert.ToInt32(dayOfNow.Day.ToString()) = 1 Then
            '                'run monthly for last month
            '                Dim startdate As DateTime
            '                Dim enddate As DateTime
            '                enddate = dayOfNow
            '                startdate = dayOfNow.AddMonths(-1)
            '                startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
            '                enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

            '                fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("Task_name").ToString())

            '                Try
            '                    If Not (row("MailTo") Is Nothing) Then
            '                        CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
            '                    End If
            '                Catch ex As Exception
            '                    CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
            '                End Try


            '            ElseIf (row("Report_Type").ToString() = "Weekly Availability - PDF") Then

            '                'run weekly report for last week
            '                Dim startdate As DateTime
            '                Dim enddate As DateTime

            '                Dim days As Integer = 7
            '                Try
            '                    If Not (row("dayback") Is Nothing) Then
            '                        days = Integer.Parse(row("dayback").ToString())
            '                    End If
            '                Catch ex As Exception

            '                End Try


            '                enddate = dayOfNow.AddDays(-1)
            '                'startdate = dayOfNow.AddDays(-(days - 1))
            '                startdate = dayOfNow.AddDays(-days)
            '                startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
            '                enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

            '                fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("Task_name").ToString())

            '                Try
            '                    If Not (row("MailTo") Is Nothing) Then
            '                        CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
            '                    End If
            '                Catch ex As Exception
            '                    CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
            '                End Try


            '            ElseIf (row("Report_Type").ToString() = "Daily ( Yesterday ) Availability - PDF") Then
            '                'run daily for last day
            '                Dim startdate As DateTime
            '                Dim enddate As DateTime
            '                enddate = dayOfNow.AddDays(-1)
            '                startdate = dayOfNow.AddDays(-1)
            '                startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
            '                enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

            '                fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("Task_name").ToString())

            '                Try
            '                    If Not (row("MailTo") Is Nothing) Then
            '                        CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
            '                    End If
            '                Catch ex As Exception
            '                    CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
            '                End Try

            '            Else
            '                CSI_Lib.LogServiceError(DateTime.Now.ToString() + "invalid report type:" + row("Report_Type").ToString() + "TASK:" + row("Task_name").ToString(), 1)
            '            End If

            '            If fileToSend = "" Then
            '                ' ????
            '                ' check the file warning
            '                'DisplayRectangle message
            '            End If
            '        Next
            '    Else
            '        Forms.MessageBox.Show("No Records Found ! Please Save this Settings before generating reports")
            '        BTN_Add.Focus()
            '    End If
        Catch ex As Exception
            Forms.MessageBox.Show("Error generating Manual Reports, MSG:" & ex.ToString())
            ' If conn IsNot Nothing Then If conn.State = ConnectionState.Open Then conn.Close()
        End Try

        '      CSI_Lib.generateReport(read_tree(), Type, DTP_Start.Value, DTP_End.Value, tb_pathReport.Text, SEvent, False)

    End Sub
    Private Sub CSIFlex_Reporting_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If (conn IsNot Nothing) Then
            conn.Close()
        End If
        ToCSV(ds.Tables("tableDGV"), path & "\sys\Auto_Report_config.csv", True)
        'Dim result As Integer = MessageBox.Show("Do you want to stop Report Generator Service ?", "Confirmation", MessageBoxButtons.YesNoCancel)
        'If result = DialogResult.Cancel Then
        '    e.Cancel = True
        'ElseIf result = DialogResult.No Then
        '    'This closes the form without stopping Reporting Service 
        'ElseIf result = DialogResult.Yes Then
        '    'This will stop Reporting Service and close the form
        '    StopService()
        'End If
        'While ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reporting") = ServiceTools.ServiceState.Run Or ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reporting") = ServiceTools.ServiceState.Stopping
        '    StopService()
        'End While
    End Sub
    Private Sub StopService()
        Try
            Dim serviceexepath As String = String.Empty
            serviceexepath = AppDomain.CurrentDomain.BaseDirectory.Replace("\bin\Debug", "\CSIFlex Reports Generator Service\bin\Debug") + "CSIFlex Reports Generator Service.exe"
            If (ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlex_Reporting")) Then
                If (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlex_Reporting") = ServiceTools.ServiceState.Run) Then
                    ServiceTools.ServiceInstaller.StopService("CSIFlex_Reporting")
                Else
                    MessageBox.Show("CSIFlex Reporting Service is stopped")
                End If
            Else
                MessageBox.Show("CSIFlex Reporting Servic is not installed")
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error trying to stop the service. See the log for more details : " & ex.Message.ToString())
            'CSI_Lib.LogServiceError(ex.Message, 1)

        End Try
    End Sub
    Public Sub ToCSV(table As DataTable, filePath As String, firstFill As Boolean)



    End Sub
    Private Sub BTN_Remove_Click(sender As Object, e As EventArgs) Handles BTN_Remove.Click

        'cmdBuild = New MySqlCommandBuilder(adap)


        If Not (DGV_tasks.CurrentRow Is Nothing) Then
            Dim FindMyRow As DataRow = ds.Tables("tableDGV").Rows(DGV_tasks.CurrentRow.Index)


            Dim sql As String = ("delete from CSI_auth.Auto_Report_config where Task_name = '" + FindMyRow("Task_name") + "'")
            Dim connection As MySqlConnection = New MySqlConnection(Report_Generator.MySqlConnectionString)
            Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)

            Try

                connection.Open()
                mysqlcomm.ExecuteNonQuery()
                ReportsChanged()
                connection.Close()

            Catch ex As Exception

                Forms.MessageBox.Show(ex.ToString())
            End Try


            FindMyRow.Delete()


        End If

        ds.AcceptChanges()
        adap.Update(ds, "tableDGV")
        ReportsChanged()

        'loadMachineGrid()
        'loadDayOfWeekGrid()

        If (DGV_tasks.Rows.Count = 0) Then
            BTN_Remove.Enabled = False
            ClearGridDay()
            ClearGridMachine()
            clearForm()
        End If



    End Sub

    Private Sub CBX_ReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBX_ReportType.SelectedIndexChanged
        If Not (BTN_Add.Text = "Add New" And BTN_Modify.Text = "Modify") Then
            If CBX_ReportType.SelectedIndex = 0 Then
                'CB_Shift1.Enabled = True
                'CB_Shift1.Checked = True
                'CB_Shift2.Enabled = False
                'CB_Shift3.Enabled = False
                RB_Shift1.Checked = True
                RB_Shift1.Enabled = True
                RB_Shift2.Enabled = True
                RB_Shift3.Enabled = True
                CB_DOW.SelectedIndex = -1
                CB_DOW.Enabled = False
                CB_WKND.Enabled = True
                CB_WKND.Checked = True
                cb_d.Enabled = False
                'cb_t.Enabled = False
                'CB_endtime.Enabled = False
                cb_t.Enabled = True
                CB_endtime.Enabled = True
                cb_d.SelectedIndex = -1
            ElseIf CBX_ReportType.SelectedIndex = 1 Then
                CB_DOW.Enabled = False
                CB_DOW.SelectedIndex = -1
                CB_WKND.Enabled = True
                cb_d.Enabled = False
                cb_t.Enabled = False
                CB_endtime.Enabled = False
                CB_WKND.Checked = True
                cb_d.SelectedIndex = -1
                RB_Shift1.Enabled = False
                RB_Shift2.Enabled = False
                RB_Shift3.Enabled = False
                RB_Shift1.Checked = False
                RB_Shift2.Checked = False
                RB_Shift3.Checked = False
            ElseIf CBX_ReportType.SelectedIndex = 2 Then
                CB_DOW.Enabled = True
                CB_WKND.Enabled = True
                CB_WKND.Checked = True
                cb_d.Enabled = True
                cb_t.Enabled = False
                CB_endtime.Enabled = False

                RB_Shift1.Enabled = False
                RB_Shift2.Enabled = False
                RB_Shift3.Enabled = False
                RB_Shift1.Checked = False
                RB_Shift2.Checked = False
                RB_Shift3.Checked = False
            ElseIf CBX_ReportType.SelectedIndex = 3 Then
                CB_DOW.SelectedIndex = -1
                CB_DOW.Enabled = False
                CB_WKND.Enabled = True
                CB_WKND.Checked = True
                cb_d.Enabled = False
                cb_t.Enabled = False
                CB_endtime.Enabled = False

                cb_d.SelectedIndex = -1
                RB_Shift1.Enabled = False
                RB_Shift2.Enabled = False
                RB_Shift3.Enabled = False
                RB_Shift1.Checked = False
                RB_Shift2.Checked = False
                RB_Shift3.Checked = False
            End If
        End If
    End Sub

    Private Sub BTN_Next_Click(sender As Object, e As EventArgs)


        Dim newCurrentRow As Integer = DGV_tasks.CurrentRow.Index + 1
        If (newCurrentRow >= DGV_tasks.RowCount) Then
            newCurrentRow = 0
        End If

        DGV_tasks.CurrentCell = DGV_tasks.Rows(newCurrentRow).Cells(0)

    End Sub

    Private Sub BTN_Previous_Click(sender As Object, e As EventArgs)
        Dim newCurrentRow As Integer = DGV_tasks.CurrentRow.Index - 1

        If (newCurrentRow < 0) Then
            newCurrentRow = DGV_tasks.RowCount - 1
        End If

        DGV_tasks.CurrentCell = DGV_tasks.Rows(newCurrentRow).Cells(0)
    End Sub

    Private Sub BTN_Cancel_Click(sender As Object, e As EventArgs) Handles BTN_Cancel.Click
        BTN_Add.Text = "Add New"
        BTN_CustomEmail.Enabled = False
        BTN_Modify.Text = "Modify"
        BTN_Modify.Enabled = False

        CB_WKND.Checked = True
        CB_WKND.Enabled = False
        cb_t.Enabled = False
        CB_endtime.Enabled = False
        AddingBool = False
        LockUnlockForm(False)
        BTN_Remove.Enabled = False
        BTN_Add.Enabled = True
        TB_TaskName.Clear()
        TB_OutPutFolder.Clear()

        ClearGridDay()
        ClearGridMail()
        ClearGridMachine()
        CBX_ReportType.SelectedIndex = -1
        CB_DOW.SelectedIndex = -1
        RB_Shift1.Enabled = False
        RB_Shift2.Enabled = False
        RB_Shift3.Enabled = False
        RB_Shift1.Checked = False
        RB_Shift2.Checked = False
        RB_Shift3.Checked = False
    End Sub

    Private Sub BTN_Output_Click(sender As Object, e As EventArgs) Handles BTN_Output.Click
        Dim folderDlg As New FolderBrowserDialog

        folderDlg.ShowNewFolderButton = False
        folderDlg.ShowNewFolderButton = True
        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            TB_OutPutFolder.Text = folderDlg.SelectedPath
            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
        End If

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BTN_Modify.Click
        Try

            If (BTN_Modify.Text = "Modify") Then
                If Not DGV_tasks.SelectedCells.Item(0).Value.ToString() = "" Then
                    'If (DGV_tasks.SelectedCells.Item(2).Value.ToString() = "Daily ( Today ) Availability - PDF") Then
                    '    If DGV_tasks.SelectedCells.Item(10).Value.ToString() = "1" Then

                    '    ElseIf DGV_tasks.SelectedCells.Item(10).Value.ToString() = "2" Then
                    '        CB_Shift2.Checked = True
                    '        CB_Shift2.Enabled = True
                    '    ElseIf DGV_tasks.SelectedCells.Item(10).Value.ToString() = "3" Then
                    '        CB_Shift3.Checked = True
                    '        CB_Shift3.Enabled = True
                    '    End If
                    'End If
                    tasktochange = DGV_tasks.SelectedCells.Item(0).RowIndex

                    'ClearGridMachine()
                    clearForm()
                    LockUnlockForm(True)
                    ClearGridMachine()
                    loadMachineGrid()

                    BTN_CustomEmail.Enabled = True
                    BTN_Add.Enabled = False
                    BTN_Remove.Enabled = False
                    BTN_Modify.Text = "Save"
                    BTN_Modify.Enabled = True
                    If CBX_ReportType.SelectedIndex = 0 Then
                        CB_DOW.Enabled = False
                        CB_WKND.Enabled = True
                        CB_WKND.Checked = True
                        cb_t.Enabled = True
                        CB_endtime.Enabled = True
                        If DGV_tasks.CurrentRow.Cells.Item(13).Value.ToString = "1" Then
                            RB_Shift1.Checked = True
                            RB_Shift1.Enabled = True
                            RB_Shift2.Enabled = True
                            RB_Shift3.Enabled = True
                            cb_t.Text = DGV_tasks.CurrentRow.Cells.Item(14).Value.ToString()
                            CB_endtime.Text = DGV_tasks.CurrentRow.Cells.Item(15).Value.ToString()
                            CB_WKND.Checked = True
                        ElseIf DGV_tasks.CurrentRow.Cells.Item(13).Value.ToString = "2" Then
                            RB_Shift2.Checked = True
                            RB_Shift1.Enabled = True
                            RB_Shift2.Enabled = True
                            RB_Shift3.Enabled = True
                            cb_t.Text = DGV_tasks.CurrentRow.Cells.Item(14).Value.ToString()
                            CB_endtime.Text = DGV_tasks.CurrentRow.Cells.Item(15).Value.ToString()
                            CB_WKND.Checked = True
                        ElseIf DGV_tasks.CurrentRow.Cells.Item(13).Value.ToString = "3" Then
                            RB_Shift3.Checked = True
                            RB_Shift1.Enabled = True
                            RB_Shift2.Enabled = True
                            RB_Shift3.Enabled = True
                            cb_t.Text = DGV_tasks.CurrentRow.Cells.Item(14).Value.ToString()
                            CB_endtime.Text = DGV_tasks.CurrentRow.Cells.Item(15).Value.ToString()
                            CB_WKND.Checked = True
                        End If
                    ElseIf CBX_ReportType.SelectedIndex = 1 Then
                        cb_d.Enabled = True
                        cb_t.Enabled = True
                        CB_endtime.Enabled = True
                        cb_t.Text = DGV_tasks.CurrentRow.Cells.Item(14).Value.ToString()
                        CB_endtime.Text = DGV_tasks.CurrentRow.Cells.Item(15).Value.ToString()
                        CB_WKND.Checked = True
                    Else
                        CB_DOW.Enabled = True
                        CB_WKND.Enabled = False
                        cb_t.Text = DGV_tasks.CurrentRow.Cells.Item(14).Value.ToString()
                        CB_endtime.Text = DGV_tasks.CurrentRow.Cells.Item(15).Value.ToString()
                    End If

                Else
                    AddingBool = True
                End If
                loadDayOfWeekGrid()
                CB_DOW.SelectedItem = -1
                'Report_Type

            Else
                If Directory.Exists(TB_OutPutFolder.Text) Then
                    'taskname = DGV_tasks.Rows(tasktochange).Cells(1).Value.ToString()
                    custommsg = GetCustomMsg(DGV_tasks.Rows(tasktochange).Cells(1).Value.ToString())
                    Dim sql As String = ("delete from CSI_auth.Auto_Report_config where Task_name = '" + DGV_tasks.Rows(tasktochange).Cells(1).Value.ToString() + "'")
                    Dim connection As MySqlConnection = New MySqlConnection(Report_Generator.MySqlConnectionString)
                    Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)

                    Try
                        connection.Open()
                        mysqlcomm.ExecuteNonQuery()
                        ReportsChanged()
                        connection.Close()
                    Catch
                    End Try
                    'DGV_tasks.Rows.RemoveAt(tasktochange)
                    'ds.Tables(0).Rows.RemoveAt(tasktochange)
                    modified = True
                    BTN_Modify.Text = "Modify"
                    BTN_CustomEmail.Enabled = False
                    AddingBool = True
                    BTN_Add_Click(BTN_Add, New EventArgs())
                    BTN_Add.Enabled = True
                    BTN_Remove.Enabled = True
                    BTN_Cancel.Enabled = False
                    CB_WKND.Enabled = False
                Else
                    MsgBox("The path that you have selected does not exists.")
                End If
            End If
            loadDayOfWeekGrid()
            CB_DOW.SelectedItem = -1
            'If DGV_tasks.SelectedCells.Item(2).Value.ToString() = "Daily ( Today ) Availability - PDF" Then
            '    CB_DOW.Enabled = False
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BTN_CustomEmail_Click(sender As Object, e As EventArgs) Handles BTN_CustomEmail.Click
        Dim frm_custommsg As New CustomEmailMsg()
        frm_custommsg.taskname = TB_TaskName.Text
        frm_custommsg.ShowDialog()
    End Sub
    'Public Sub StartAutoreporting(val As Boolean)
    '    While (val = True)
    '        GenerateReports()
    '    End While
    'End Sub

    'Public Sub thread_AutoReporting()
    '    StartAutoreporting(True)
    'End Sub

    Private Function getAutoReportOrNot() As Boolean
        Dim reportOrNot As Boolean = False
        Try
            Dim cntsql As New MySqlConnection(Report_Generator.MySqlConnectionString)
            cntsql.Open()
            Dim mysql23 As String = "SELECT * FROM csi_auth.tbl_autoreport_status;"
            Dim cmdCreateDeviceTable23 As New MySqlCommand(mysql23, cntsql)
            Dim mysqlReader23 As MySqlDataReader = cmdCreateDeviceTable23.ExecuteReader
            Dim dTable_message23 As New DataTable()
            dTable_message23.Load(mysqlReader23)
            If dTable_message23.Rows.Count > 0 Then
                reportOrNot = dTable_message23.Rows(0)("autoreport_status").ToString()   'dTable_message23.Columns("autoreport_status").ToString()
            End If
            cntsql.Close()
        Catch ex As Exception
            Forms.MessageBox.Show("Error in loading Auto Reporting from Database : " & ex.Message())
        End Try
        'Dim directory As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Server"

        'If (File.Exists(directory & "\sys\auto_reporting.csys")) Then
        '    Using reader As StreamReader = New StreamReader(directory & "\sys\auto_reporting.csys")
        '        reportOrNot = reader.ReadLine()
        '        reader.Close()
        '    End Using
        'End If

        Return reportOrNot
    End Function
    Private Sub resetDoneReport()
        Dim sql As String = ("update  CSI_auth.Auto_Report_config set done = 0")
        Dim connection As MySqlConnection = New MySqlConnection(Report_Generator.MySqlConnectionString)
        Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)

        Try
            connection.Open()
            mysqlcomm.ExecuteNonQuery()
            connection.Close()
        Catch ex As Exception
            'CSI_Lib.LogServiceError(ex.ToString(), 1)
            MessageBox.Show("resetDoneReport Error: " & ex.Message.ToString())
            If connection.State = ConnectionState.Open Then connection.Close()
        End Try
    End Sub
    Public Function check_tableExists(db_name As String, tbl_name As String) As Boolean
        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(Report_Generator.MySqlConnectionString)
        mySqlCnt.Open()

        Dim mysql As String = "SELECT count(*) FROM information_schema.tables WHERE table_schema = '" + db_name + "'   AND table_name = '" + tbl_name + "';"
        Dim cmdCreateDeviceTable As New MySqlCommand(mysql, mySqlCnt)
        Dim yes = cmdCreateDeviceTable.ExecuteScalar()
        mySqlCnt.Close()

        If (yes = 1) Then
            Return True
        Else
            Return False
        End If

    End Function
    Private Sub BG_generate_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BG_generate.DoWork

        Dim DayName As String = Convert.ToString(Now.DayOfWeek)
        Dim autoReport As Boolean = getAutoReportOrNot()

        Try
            If (check_tableExists("csi_auth", "Auto_Report_config") And autoReport = True) Then

                'csi_lib.Log_server_event(Date.Now & ", " & "Autoreporting : generating reports")

                Dim Dataset_AutoReport As DataSet = New DataSet()
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim adap As New MySqlDataAdapter()
                Dim listOfMachine As String()

                conn.ConnectionString = (Report_Generator.MySqlConnectionString)
                conn.Open()

                If (DayName <> Convert.ToString(Convert.ToInt32(System.DateTime.Now.DayOfWeek))) Then
                    resetDoneReport()
                End If

                Dim dayOfNow As DateTime = Now()
                'DayName = Convert.ToString(dayOfNow.DayOfWeek)
                DayName = Convert.ToString(dayOfNow.DayOfWeek)
                'Dim DayName1 As String = dayOfNow.DayOfWeek.ToString()
                Dim timeNow As String = dayOfNow.ToString("HH:mm")
                Dim QryStr As String = String.Format("SELECT * FROM csi_auth.{0} where (Day_ like '%" + DateTime.Now.DayOfWeek.ToString() + "%' or Day_ like '%everyday%' ) and Time_ like '" + timeNow + "' and done <> '" + DayName + "' ", "Auto_Report_config")
                Dim fileToSend As String

                adap = New MySqlDataAdapter(QryStr, conn)
                adap.Fill(Dataset_AutoReport, "tableDGV")


                For Each row As DataRow In Dataset_AutoReport.Tables("tableDGV").Rows
                    Dim custommsg As String = Convert.ToString(row("CustomMsg"))
                    If custommsg.Length = 0 Then
                        If Convert.ToString(row("Report_Type")) = "Daily ( Today ) Availability - PDF" Then
                            'Today
                            custommsg = "Your Today's CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                        ElseIf Convert.ToString(row("Report_Type")) = "Monthly Availability - PDF" Then
                            'Monthly Reporting
                            custommsg = "Your Monthly CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                        ElseIf Convert.ToString(row("Report_Type")) = "Weekly Availability - PDF" Then
                            'Weekly Reporting
                            custommsg = "Your Weekly CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                        ElseIf Convert.ToString(row("Report_Type")) = "Daily ( Yesterday ) Availability - PDF" Then
                            'Daily ( Yesterday ) Availability - PDF
                            custommsg = "Your Daily CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                        End If
                    Else
                        custommsg = Convert.ToString(row("CustomMsg"))
                    End If
                    Try

                        listOfMachine = Convert.ToString(row("MachineToReport")).Split(";")



                        Dim SEvent(0 To listOfMachine.Count() - 1, 1) As String
                        For i = 0 To listOfMachine.Count() - 1

                            SEvent(i, 0) = listOfMachine(i)
                            SEvent(i, 1) = "SETUP"
                        Next

                        If Convert.ToString(row("Report_Type")) = "Monthly Availability - PDF" And Convert.ToInt32(Convert.ToString(dayOfNow.Day)) = 1 Then
                            'run monthly for last month

                            Dim startdate As DateTime
                            Dim enddate As DateTime
                            enddate = dayOfNow
                            startdate = dayOfNow.AddMonths(-1)
                            startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                            enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                            fileToSend = Report_Generator.generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, Convert.ToString(row("Task_name")))

                            Try
                                If Not (row("MailTo") Is Nothing) Then
                                    Report_Generator.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                                End If
                            Catch ex As Exception
                                'CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                                MessageBox.Show("Error trying to send report by email:" & ex.ToString())
                            End Try
                            updateDoneReport(row("Task_name"))

                        ElseIf (Convert.ToString(row("Report_Type")) = "Weekly Availability - PDF") Then

                            'run weekly report for last week
                            Dim startdate As DateTime
                            Dim enddate As DateTime

                            Dim days As Integer = 7
                            Try
                                If Not (row("dayback") Is Nothing) Then
                                    days = Integer.Parse(Convert.ToString(row("dayback")))
                                End If
                            Catch ex As Exception

                            End Try


                            enddate = dayOfNow.AddDays(-1)
                            'startdate = dayOfNow.AddDays(-(days - 1))
                            startdate = dayOfNow.AddDays(-days)
                            startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                            enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                            fileToSend = Report_Generator.generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, Convert.ToString(row("Task_name")))

                            Try
                                If Not (row("MailTo") Is Nothing) Then
                                    Report_Generator.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                                End If
                            Catch ex As Exception
                                'CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                                MessageBox.Show("Error trying to send report by email:" & ex.ToString())
                            End Try
                            updateDoneReport(row("Task_name"))

                        ElseIf (Convert.ToString(row("Report_Type")) = "Daily ( Yesterday ) Availability - PDF") Then
                            'run daily for last day

                            Dim startdate As DateTime
                            Dim enddate As DateTime
                            enddate = dayOfNow.AddDays(-1)
                            startdate = dayOfNow.AddDays(-1)
                            startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                            enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                            fileToSend = Report_Generator.generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, Convert.ToString(row("Task_name")))

                            Try
                                If Not (row("MailTo") Is Nothing) Then
                                    Report_Generator.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                                End If
                            Catch ex As Exception
                                'CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                                MessageBox.Show("Error trying to send report by email:" & ex.ToString())
                            End Try
                            updateDoneReport(row("Task_name"))
                        Else
                            'CSI_Lib.LogServiceError(DateTime.Now.ToString() + "invalid report type:" + row("Report_Type").ToString() + "TASK:" + row("Task_name").ToString(), 1)
                            MessageBox.Show(DateTime.Now.ToString() + "invalid report type:" + row("Report_Type").ToString() + "TASK:" + row("Task_name").ToString())
                        End If

                    Catch ex As Exception
                        'CSI_Lib.LogServiceError("Error generating autoreports, MSG:" & ex.ToString(), 1)
                        MessageBox.Show("Error generating autoreports, MSG:" & ex.ToString())
                        If conn.State = ConnectionState.Open Then conn.Close()

                    End Try
                Next
                If conn.State = ConnectionState.Open Then conn.Close()
            End If


        Catch ex As Exception
            'CSI_Lib.LogServiceError("Error in GenerateReports():" & ex.ToString() + vbCrLf + "__>" + ex.StackTrace, 1)
            MessageBox.Show("Error in GenerateReports():" & ex.ToString() + vbCrLf + "__>" + ex.StackTrace)
        End Try
        'Dim Type As String = ""
        'If CBX_ReportType.SelectedItem.ToString().Contains("Weekly") Then
        '    Type = "Weekly"
        'ElseIf CBX_ReportType.SelectedItem.ToString().Contains("Monthly") Then
        '    Type = "Monthly"
        'ElseIf CBX_ReportType.SelectedItem.ToString().Contains("Daily") Then
        '    Type = "Daily"
        'End If

        '' from Servicelibrary.vb ln 3483
        'Dim custommsg As String
        'Try
        '    Dim Dataset_AutoReport As DataSet = New DataSet()
        '    Dim conn As MySqlConnection = New MySqlConnection()
        '    Dim adap As New MySqlDataAdapter()

        '    conn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString
        '    conn.Open()

        '    Dim dayOfNow As DateTime = Now()
        '    Dim DayName As String = dayOfNow.DayOfWeek.ToString()
        '    Dim timeNow As String = dayOfNow.ToString("HH:mm")
        '    Dim QryStr As String

        '    QryStr = String.Format("SELECT * FROM csi_auth.{0} where (Task_name like '%" + TB_TaskName.Text + "%'  )", "Auto_Report_config")


        '    Dim fileToSend As String = ""

        '    adap = New MySqlDataAdapter(QryStr, conn)
        '    adap.Fill(Dataset_AutoReport, "tableDGV")

        '    For Each row As DataRow In Dataset_AutoReport.Tables("tableDGV").Rows
        '        Dim listOfMachine As String()
        '        listOfMachine = row("MachineToReport").ToString().Split(";")

        '        Dim SEvent(0 To listOfMachine.Count() - 1, 1) As String
        '        For i = 0 To listOfMachine.Count() - 1
        '            SEvent(i, 0) = listOfMachine(i)
        '            SEvent(i, 1) = "SETUP"
        '        Next
        '        If (Convert.ToString(row("CustomMsg")).Length = 0) Then
        '            custommsg = "Your " & Type & " CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
        '        Else
        '            custommsg = Convert.ToString(row("CustomMsg"))
        '        End If
        '        If row("Report_Type").ToString() = "Monthly Availability - PDF" And Convert.ToInt32(dayOfNow.Day.ToString()) = 1 Then
        '            'run monthly for last month
        '            Dim startdate As DateTime
        '            Dim enddate As DateTime
        '            enddate = dayOfNow
        '            startdate = dayOfNow.AddMonths(-1)
        '            startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
        '            enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

        '            fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("Task_name").ToString())

        '            Try
        '                If Not (row("MailTo") Is Nothing) Then
        '                    CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
        '                End If
        '            Catch ex As Exception
        '                CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
        '            End Try


        '        ElseIf (row("Report_Type").ToString() = "Weekly Availability - PDF") Then

        '            'run weekly report for last week
        '            Dim startdate As DateTime
        '            Dim enddate As DateTime

        '            Dim days As Integer = 7
        '            Try
        '                If Not (row("dayback") Is Nothing) Then
        '                    days = Integer.Parse(row("dayback").ToString())
        '                End If
        '            Catch ex As Exception

        '            End Try


        '            enddate = dayOfNow.AddDays(-1)
        '            'startdate = dayOfNow.AddDays(-(days - 1))
        '            startdate = dayOfNow.AddDays(-days)
        '            startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
        '            enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

        '            fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("Task_name").ToString())

        '            Try
        '                If Not (row("MailTo") Is Nothing) Then
        '                    CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
        '                End If
        '            Catch ex As Exception
        '                CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
        '            End Try


        '        ElseIf (row("Report_Type").ToString() = "Daily ( Yesterday ) Availability - PDF") Then
        '            'run daily for last day
        '            Dim startdate As DateTime
        '            Dim enddate As DateTime
        '            enddate = dayOfNow.AddDays(-1)
        '            startdate = dayOfNow.AddDays(-1)
        '            startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
        '            enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

        '            fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("Task_name").ToString())

        '            Try
        '                If Not (row("MailTo") Is Nothing) Then
        '                    CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
        '                End If
        '            Catch ex As Exception
        '                CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
        '            End Try

        '        Else
        '            CSI_Lib.LogServiceError(DateTime.Now.ToString() + "invalid report type:" + row("Report_Type").ToString() + "TASK:" + row("Task_name").ToString(), 1)
        '        End If

        '        If fileToSend = "" Then
        '            ' ????
        '            ' check the file warning
        '            'DisplayRectangle message
        '        End If
        '    Next


        'Catch ex As Exception
        'Forms.MessageBox.Show("Error generating autoreports, MSG:" & ex.ToString())
        'If conn IsNot Nothing Then If conn.State = ConnectionState.Open Then conn.Close()

        'End Try

    End Sub
    Private Sub updateDoneReport(taskname As String)
        Dim sql As String = ("update  CSI_auth.Auto_Report_config set done = '" + Convert.ToString(Now.DayOfWeek) + "' where Task_name = '" + taskname + "'")
        Dim connection As MySqlConnection = New MySqlConnection(Report_Generator.MySqlConnectionString)
        Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)

        Try
            connection.Open()
            mysqlcomm.ExecuteNonQuery()
            connection.Close()
        Catch ex As Exception
            'CSI_Lib.LogServiceError("Error trying to update auto_report_config table:" & ex.ToString(), 1)
            MessageBox.Show("Error trying to update auto_report_config table:" & ex.ToString())
            If connection.State = ConnectionState.Open Then connection.Close()
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PB_Unistall.Click
        'First Stop the Service then Delete Service 
        StopReportingService("CSIFlex_Reporting")
        UninstallReportingService("CSIFlex_Reporting")
        MessageBox.Show("Reporting Service uninstalled successfully ! If you want to install it again please restart the reporting application")
        Me.Close()
    End Sub

    Public Sub StopReportingService(ServiceName As String)
        Dim cmd As String = "sc stop " + ServiceName
        Dim info = New ProcessStartInfo()
        info.FileName = "cmd"
        info.Arguments = "cmd /c" + cmd
        info.UseShellExecute = True
        info.CreateNoWindow = True
        info.WindowStyle = ProcessWindowStyle.Hidden
        info.Verb = "runas"
        Dim process As New Process()
        process.StartInfo = info
        process.Start()
        process.WaitForExit()
        Dim sc As ServiceController
        sc = New ServiceController(ServiceName)
        sc.WaitForStatus(ServiceControllerStatus.Stopped)
    End Sub

    Public Sub UninstallReportingService(ServiceName As String)
        Dim cmd As String = "sc delete " + ServiceName
        Dim info = New ProcessStartInfo()
        info.FileName = "cmd"
        info.Arguments = "cmd /c" + cmd
        info.UseShellExecute = True
        info.CreateNoWindow = True
        info.WindowStyle = ProcessWindowStyle.Hidden
        info.Verb = "runas"
        Dim Process = New Process()
        Process.StartInfo = info
        Process.Start()
        Process.WaitForExit()
    End Sub
End Class

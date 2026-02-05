Imports System.IO
Imports System.Windows
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities
Imports MySql.Data.MySqlClient

Public Class Reporting_availability

    Private license As Integer
    Private active_machines As List(Of String)
    Private TypeDePeriode As String
    Private Qry_Tbl_MachineName As String
    Private Qry_Tbl_DataMachine As String
    Public CSI_Lib As CSI_Library.CSI_Library
    'Public Shared cs As String = CSI_Library.CSI_Library.sqlitedbpath
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath

    Dim pathReport As String = ""

    Private Shared startDate As Date
    Private Shared endDate As Date
    Private Shared reportType As String
    Private Shared reportPeriod As String

    Private Sub SetupForm_Reporting_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Computer.FileSystem.FileExists(rootPath & "\sys\" + "Reporting_availability_vb") Then
            Reporting_application.Deserialise(Me, rootPath & "\sys\" + "Reporting_availability_vb")
        End If

        Me.MdiParent = Reporting_application

        Dim db_authPath As String = Nothing

        Dim imglst As New ImageList
        CSI_Lib = New CSI_Library.CSI_Library(False)
        license = CSI_Lib.CheckLic(2)
        Dim list2 As New List(Of String)

        Dim mt_present As Boolean = False
        Dim st_present As Boolean = False

        Dim prefixTemp As String ' temp string, multiple use

        CSI_Lib.connectionString = CSIFLEXSettings.Instance.ConnectionString
        txtPathReport.Text = CSIFLEXSettings.Instance.ReportFolder

        tvMachine.Nodes.Clear()

        cmbReportType.SelectedIndex = 0
        cmbSorting.SelectedIndex = 0
        cmbScale.SelectedIndex = 0

        Dim dtMachines As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf WHERE MonState = 1", CSIFLEXSettings.Instance.ConnectionString)

        Dim i As Integer = 0
        Dim j As Integer = -1
        Dim k As Integer = -1
        Dim cpt As Integer = 0
        Dim machName As String
        Dim machineId As Integer

        Dim allMachinesNode As New TreeNode
        allMachinesNode.ImageKey = "0"
        allMachinesNode.Text = "All Machines"
        tvMachine.Nodes.Add(allMachinesNode)

        For Each userMachine As KeyValuePair(Of Integer, Tuple(Of String, String)) In CSIFLEXSettings.UserMachines

            machName = userMachine.Value.Item1
            machineId = userMachine.Key

            prefixTemp = Strings.Left(machName, 3)

            Dim node As New TreeNode

            If Strings.Len(machName) > 4 Then
                node.ImageKey = Strings.Right(machName, Strings.Len(machName) - 4)
                node.Text = Strings.Right(machName, Strings.Len(machName) - 4)
            Else
                node.ImageKey = machName
                node.Text = machName
            End If

            Select Case prefixTemp
                Case "_MT"
                    mt_present = True
                    st_present = False

                    list2.Add(machName)
                    tvMachine.Nodes(0).Nodes.Add(node)
                    cpt = -1
                    j = j + 1
                Case "_ST"
                    st_present = True
                    cpt += 1
                    If mt_present = False Then
                        tvMachine.Nodes(0).Nodes.Add(Strings.Right(machName, Strings.Len(machName) - 4), Strings.Right(machName, Strings.Len(machName) - 4))
                    Else
                        tvMachine.Nodes(0).Nodes(j).Nodes.Add(Strings.Right(machName, Strings.Len(machName) - 4), Strings.Right(machName, Strings.Len(machName) - 4))
                    End If

                    k += 1

                Case Else

                    If (prefixTemp <> "_ST") And (prefixTemp <> "_MT") Then

                        If st_present = False And mt_present = True Then

                            tvMachine.Nodes(0).Nodes(j).Nodes.Add(machineId, machName)
                            cpt += 1

                        ElseIf mt_present = False And st_present = True Then

                            tvMachine.Nodes(0).Nodes(k).Nodes.Add(machineId, machName)

                        ElseIf mt_present = False And st_present = False Then

                            tvMachine.Nodes(0).Nodes.Add(machineId, machName)

                        Else

                            tvMachine.Nodes(0).Nodes(j).Nodes(cpt).Nodes.Add(machineId, machName)

                        End If

                    End If
            End Select

            i = i + 1

        Next


        Dim reports As DataTable = MySqlAccess.GetDataTable("SELECT task_name, ReportTitle, MachineToReport FROM csi_auth.auto_report_config WHERE Enabled AND task_name <> 'CSIFLEXClientReport' AND task_name <> 'SystemMonitoring';", CSIFLEXSettings.Instance.ConnectionString)

        cmbReports.Items.Clear()
        cmbReports.Items.Add(New DictionaryEntry("None", "None"))

        For Each report As DataRow In reports.Rows

            Dim mchs() As String = report("MachineToReport").ToString().Split(";")
            Dim include As Boolean = True

            For Each mch As String In mchs
                If Not CSIFLEXSettings.UserMachines.Any(Function(m) m.Key.ToString() = mch Or m.Value.Item1 = mch Or m.Value.Item2 = mch) Then include = False
            Next

            If include Then
                cmbReports.Items.Add(New DictionaryEntry($"{report("task_name").ToString()} - {report("ReportTitle").ToString()}", report("task_name").ToString()))
            End If

        Next
        cmbReports.ValueMember = "Value"
        cmbReports.DisplayMember = "Key"
        cmbReports.SelectedIndex = 0

        'If System.IO.File.Exists(rootPath & "\sys\defaultReportFolder_.csys") Then
        '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\defaultReportFolder_.csys")
        '        pathReport = reader.ReadLine()
        '        txtPathReport.Clear()
        '        txtPathReport.Text = pathReport
        '        reader.Close()
        '    End Using
        'End If

    End Sub

    Private Sub BTN_BrowseFolder_Click(sender As Object, e As EventArgs) Handles btnBrowseFolder.Click
        Dim folderDlg As New FolderBrowserDialog

        folderDlg.ShowNewFolderButton = False
        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            txtPathReport.Text = folderDlg.SelectedPath
            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
        End If


    End Sub

    Public Function read_tree() As String()

        active_machines = New List(Of String)()

        Dim aNode As TreeNode

        For Each aNode In tvMachine.Nodes(0).Nodes
            PrintRecursive(aNode)
        Next

        'ReDim Preserve active_machines(UBound(active_machines) - 1)

        active_machines.Sort()

        Return active_machines.ToArray()

    End Function

    Private Sub PrintRecursive(ByVal n As TreeNode)

        If n.Checked = True And n.Nodes.Count = 0 Then

            Dim id = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.MachineName = n.Text).Id

            If n.Text <> "" And Not active_machines.Contains(id) Then

                active_machines.Add(id)

                'ReDim Preserve active_machines(UBound(active_machines) + 1)

            End If

        End If

        Dim aNode As TreeNode

        For Each aNode In n.Nodes
            PrintRecursive(aNode)
        Next
    End Sub

    Sub generateSqlQuery()

        Dim ListOfMachineSelected As String() = read_tree()
        Dim i As Integer = 0

        Qry_Tbl_MachineName = ""
        Qry_Tbl_DataMachine = ""

        For Each machine In ListOfMachineSelected
            If (machine <> "") Then
                machine = CSI_Lib.RenameMachine(machine)

                Qry_Tbl_MachineName += "SELECT 'tbl_" + machine + "' AS MchName, status FROM tbl_" + machine + " "
                Qry_Tbl_DataMachine += "SELECT 'tbl_" + machine + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, iif(status = '_COFF', cycletime,0) as COFF, "
                Qry_Tbl_DataMachine += " iif(status = '_CON', cycletime,0) as CON, iif(status = '_SETUP', cycletime,0) as SETUP, iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP', cycletime,0) as OTHER  from tbl_" + machine + " "

                If Not i = (ListOfMachineSelected.Length - 1) Then

                    Qry_Tbl_MachineName += " union "
                    Qry_Tbl_DataMachine += " union "

                End If
                i += 1
            End If
        Next



    End Sub

    Private Sub setControl(enable As Boolean)

        'DTP_Start.Enabled = enable
        grpDowntime.Enabled = IIf(Not enable, enable, IIf(cmbReportType.SelectedIndex = 1, True, False))
        cmbReportType.Enabled = enable
        dtpReportDate.Enabled = enable
        txtPathReport.Enabled = enable
        tvMachine.Enabled = enable
        GB_ReportType.Enabled = enable
        btnSetDefaultPath.Enabled = enable
        btnBrowseFolder.Enabled = enable

    End Sub

    Public Function FileInUse(ByVal sFile As String) As Boolean
        If System.IO.File.Exists(sFile) Then
            Try
                Dim F As Short = FreeFile()
                FileOpen(F, sFile, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FileClose(F)
            Catch
                Return True
            End Try
        End If
        Return False
    End Function

    Private Sub BTN_GenerateReport_Click(sender As Object, e As EventArgs) Handles btnGenerateReport.Click

        Log.Info("======>> Start Report")

        If String.IsNullOrEmpty(txtPathReport.Text) Then
            MessageBox.Show("Please select a folder")
            Return
        End If

        SaveReport()
        btnGenerateReport.Enabled = False
        setControl(False)
        PB_loading.Visible = True
        BackgroundWorker.RunWorkerAsync()

        'MySqlAccess.ExecuteNonQuery("DELETE FROM csi_auth.auto_report_config WHERE Task_name = 'CSIFLEXClientReport';", connectionString)

    End Sub

    Public Sub generateReportPDF()

        Dim machineList As String() = read_tree()
        Dim SEvent(0 To read_tree().Count() - 1, 1) As String
        For i = 0 To read_tree().Count() - 1
            SEvent(i, 1) = machineList(i)
            SEvent(i, 1) = "SETUP"
        Next

        CSI_Lib.generateReport(read_tree(), reportType, reportPeriod, False, startDate, endDate, txtPathReport.Text, SEvent, True, "False", "CSIFLEXClientReport", CSIFLEXSettings.Instance.ConnectionString)

        Log.Info("======>> Report Finished")

        Dim result As DialogResult = MessageBox.Show("Your report(s) are ready. Do you want to open the folder?", "Report(s) completed", MessageBoxButton.YesNo, MessageBoxImage.Asterisk)
        If (result = Forms.DialogResult.Yes) Then
            Process.Start(txtPathReport.Text)
        End If

    End Sub

    Public Function toMonday(datee As Date) As DateTime
        Dim today As Date = datee
        Dim dayIndex As Integer = today.DayOfWeek
        If dayIndex < DayOfWeek.Monday Then
            dayIndex += 7
        End If
        Dim dayDiff As Integer = dayIndex - DayOfWeek.Monday
        Dim monday As Date = today.AddDays(-dayDiff)
        Return monday
    End Function

    Public Function toFirst(datee As Date) As DateTime
        Return DateAdd("m", DatePart("m", datee), DateSerial(Year(datee), 1, 1))
    End Function

    Public Function IntToMonday(integ As Integer, param As DateTime) As DateTime

        Dim ret As DateTime

        ret = DateAdd("ww", integ - 1, DateSerial(Year(param), 1, 1))

        Return toMonday(ret)

    End Function

    Public Function IntToFirst(integ As Integer, param As DateTime) As DateTime

        Dim ret As DateTime = New DateTime(Year(param), integ, 1)
        Return ret

    End Function

    Private Sub SetupForm_Reporting_MaximizedBoundsChanged(sender As Object, e As EventArgs) Handles Me.MaximizedBoundsChanged

    End Sub

    'Private Sub frm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    '    Me.Hide()
    '    e.Cancel = True
    'End Sub

    Private Sub TreeView_machine_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles tvMachine.AfterCheck
        RemoveHandler tvMachine.AfterCheck, AddressOf TreeView_machine_AfterCheck

        Call CheckAllChildNodes(e.Node)

        Dim machines As String() = read_tree()


        If e.Node.Checked Then
            If e.Node.Parent Is Nothing = False Then
                Dim allChecked As Boolean = True
                Call IsEveryChildChecked(e.Node.Parent, allChecked)
                If allChecked Then
                    e.Node.Parent.Checked = True
                    Call ShouldParentsBeChecked(e.Node.Parent)
                End If
            End If
        Else
            Dim parentNode As TreeNode = e.Node.Parent
            While parentNode Is Nothing = False
                parentNode.Checked = False
                parentNode = parentNode.Parent
            End While
        End If

        AddHandler tvMachine.AfterCheck, AddressOf TreeView_machine_AfterCheck
    End Sub

    Private Sub IsEveryChildChecked(ByVal parentNode As TreeNode, ByRef checkValue As Boolean)
        For Each node As TreeNode In parentNode.Nodes
            Call IsEveryChildChecked(node, checkValue)
            If Not node.Checked Then
                checkValue = False
            End If
        Next
    End Sub

    Private Sub CheckAllChildNodes(ByVal parentNode As TreeNode)
        For Each childNode As TreeNode In parentNode.Nodes
            childNode.Checked = parentNode.Checked
            CheckAllChildNodes(childNode)
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

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted

        PB_loading.Visible = False
        btnGenerateReport.Enabled = True
        setControl(True)
    End Sub

    Private Sub BTN_SetDefaultPath_Click(sender As Object, e As EventArgs) Handles btnSetDefaultPath.Click

        txtPathReport.Text = CSIFLEXSettings.Instance.ReportFolder
        Return

        'If File.Exists(rootPath & "\sys\defaultReportFolder_.csys") Then
        '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\defaultReportFolder_.csys")
        '        txtPathReport.Clear()
        '        txtPathReport.Text = reader.ReadLine()
        '        reader.Close()
        '    End Using
        'Else
        '    MessageBox.Show("Please specify a default folder in the ""Setup"" form")
        'End If

        'If txtPathReport.Text = "" Then
        '    MessageBox.Show("Please specify a default folder in the ""Setup"" form")
        'End If

    End Sub

    Private Sub Reporting_availability_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Reporting_application.Serialise(Me, rootPath & "\sys\" + "Reporting_availability_vb")
    End Sub

    Private Sub CB_Period_CheckedChanged(sender As Object, e As EventArgs)


        'DTP_Start.Visible = True
        'DTP_Start.Enabled = True
        dtpReportDate.Visible = True
        dtpReportDate.Enabled = True
        GB_ReportType.Enabled = False
        GB_ReportType.Visible = False
        tvMachine.Height = 284
        tvMachine.Location = New System.Drawing.Point(tvMachine.Location.X, 87)
        rbDaily.Enabled = True
        rbWeekly.Enabled = True
        rbMonthly.Enabled = True

    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker.DoWork
        Try
            generateReportPDF()
        Catch ex As Exception

            Log.Error(ex)
            MessageBox.Show("Unable to generate report. Make sure your computer has ReportViewer 2015 runtime")

        End Try
    End Sub

    Private Sub Period_Changed(sender As Object, e As EventArgs) Handles rbWeekly.CheckedChanged, rbMonthly.CheckedChanged, rbDaily.CheckedChanged, dtpReportDate.ValueChanged

        endDate = dtpReportDate.Value.Date + New TimeSpan(23, 59, 59)

        If rbDaily.Checked Then
            startDate = endDate.Date + New TimeSpan(0, 0, 0)
            If dtpReportDate.Value.Date = Today Then
                reportPeriod = "Today"
            Else
                reportPeriod = "Yesterday"
            End If
        End If

        If rbWeekly.Checked Then
            startDate = endDate.AddDays(-6)
            reportPeriod = "Weekly"
        End If

        If rbMonthly.Checked Then

            If Date.DaysInMonth(endDate.Year, endDate.Month) = endDate.Day Then
                startDate = New Date(endDate.Year, endDate.Month, 1)
            Else
                Try
                    startDate = New Date(endDate.Year, endDate.Month - 1, endDate.Day + 1)
                Catch ex As Exception
                    startDate = New Date(endDate.Year, endDate.Month - 1, Date.DaysInMonth(endDate.Year, endDate.Month - 1)).AddDays(1)
                End Try
            End If

            reportPeriod = "Monthly"
        End If

        lblPeriod.Text = ""
        If Not rbDaily.Checked Then lblPeriod.Text = $"Report from {startDate.ToString("dd-MMM-yyyy (ddd)")} to {endDate.ToString("dd-MMM-yyyy (ddd)")}"

    End Sub

    Private Sub cmbReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbReportType.SelectedIndexChanged

        If cmbReportType.SelectedIndex >= 0 Then reportType = cmbReportType.SelectedItem.ToString()

        grpDowntime.Enabled = cmbReportType.SelectedIndex = 1

    End Sub

    Private Sub SaveReport()

        Dim reportId As Integer = MySqlAccess.ExecuteScalar("SELECT Id FROM csi_auth.auto_report_config WHERE Task_Name = 'CSIFLEXClientReport'", CSIFLEXSettings.Instance.ConnectionString)

        Dim sqlCmd As New Text.StringBuilder()

        If reportId = 0 Then
            sqlCmd.Append($"INSERT INTO csi_auth.auto_report_config  ")
            sqlCmd.Append($" (                                       ")
            sqlCmd.Append($"    Task_name        ,                   ")
            sqlCmd.Append($"    Day_             ,                   ")
            sqlCmd.Append($"    Time_            ,                   ")
            sqlCmd.Append($"    ReportType       ,                   ")
            sqlCmd.Append($"    ReportTitle      ,                   ")
            sqlCmd.Append($"    ReportPeriod     ,                   ")
            sqlCmd.Append($"    Output_Folder    ,                   ")
            sqlCmd.Append($"    MachineToReport  ,                   ")
            sqlCmd.Append($"    done             ,                   ")
            sqlCmd.Append($"    Scale            ,                   ")
            sqlCmd.Append($"    Production       ,                   ")
            sqlCmd.Append($"    Setup            ,                   ")
            sqlCmd.Append($"    OnlySummary      ,                   ")
            sqlCmd.Append($"    Sorting          ,                   ")
            sqlCmd.Append($"    shift_number     ,                   ")
            sqlCmd.Append($"    shift_starttime  ,                   ")
            sqlCmd.Append($"    shift_endtime    ,                   ")
            sqlCmd.Append($"    short_filename                       ")
            sqlCmd.Append($" )                                       ")
            sqlCmd.Append($" VALUES                                  ")
            sqlCmd.Append($" (                                       ")
            sqlCmd.Append($"    @Task_name       ,                   ")
            sqlCmd.Append($"    @Day_            ,                   ")
            sqlCmd.Append($"    @Time_           ,                   ")
            sqlCmd.Append($"    @ReportType      ,                   ")
            sqlCmd.Append($"    @ReportTitle     ,                   ")
            sqlCmd.Append($"    @ReportPeriod    ,                   ")
            sqlCmd.Append($"    @Output_Folder   ,                   ")
            sqlCmd.Append($"    @MachineToReport ,                   ")
            sqlCmd.Append($"    @done            ,                   ")
            sqlCmd.Append($"    @Scale           ,                   ")
            sqlCmd.Append($"    @Production      ,                   ")
            sqlCmd.Append($"    @Setup           ,                   ")
            sqlCmd.Append($"    @OnlySummary     ,                   ")
            sqlCmd.Append($"    @Sorting         ,                   ")
            sqlCmd.Append($"    @shift_number    ,                   ")
            sqlCmd.Append($"    @shift_starttime ,                   ")
            sqlCmd.Append($"    @shift_endtime   ,                   ")
            sqlCmd.Append($"    @short_filename                      ")
            sqlCmd.Append($" )                                     ; ")

            sqlCmd.Append($"INSERT INTO csi_auth.auto_report_status  ")
            sqlCmd.Append($" (                                       ")
            sqlCmd.Append($"    ReportId         ,                   ")
            sqlCmd.Append($"    Status                               ")
            sqlCmd.Append($" )                                       ")
            sqlCmd.Append($" VALUES                                  ")
            sqlCmd.Append($" (                                       ")
            sqlCmd.Append($"    LAST_INSERT_ID(),                    ")
            sqlCmd.Append($"    'Pending'                            ")
            sqlCmd.Append($" );                                      ")
        Else
            sqlCmd.Append($"UPDATE csi_auth.auto_report_config SET   ")
            sqlCmd.Append($"    Day_            = @Day_            , ")
            sqlCmd.Append($"    Time_           = @Time_           , ")
            sqlCmd.Append($"    ReportType      = @ReportType      , ")
            sqlCmd.Append($"    ReportTitle     = @ReportTitle     , ")
            sqlCmd.Append($"    ReportPeriod    = @ReportPeriod    , ")
            sqlCmd.Append($"    Output_Folder   = @Output_Folder   , ")
            sqlCmd.Append($"    MachineToReport = @MachineToReport , ")
            sqlCmd.Append($"    done            = @done            , ")
            sqlCmd.Append($"    Scale           = @Scale           , ")
            sqlCmd.Append($"    Production      = @Production      , ")
            sqlCmd.Append($"    Setup           = @Setup           , ")
            sqlCmd.Append($"    OnlySummary     = @OnlySummary     , ")
            sqlCmd.Append($"    Sorting         = @Sorting         , ")
            sqlCmd.Append($"    shift_number    = @shift_number    , ")
            sqlCmd.Append($"    shift_starttime = @shift_starttime , ")
            sqlCmd.Append($"    shift_endtime   = @shift_endtime   , ")
            sqlCmd.Append($"    short_filename  = @short_filename    ")
            sqlCmd.Append($"WHERE Id = { reportId }                  ")
        End If


        Dim sqlCommand As New MySqlCommand(sqlCmd.ToString())
        sqlCommand.Parameters.AddWithValue("@Task_name", "CSIFLEXClientReport")
        sqlCommand.Parameters.AddWithValue("@Day_", "Now")
        sqlCommand.Parameters.AddWithValue("@Time_", "00:00")
        sqlCommand.Parameters.AddWithValue("@ReportType", reportType)
        sqlCommand.Parameters.AddWithValue("@ReportTitle", reportTitle)
        sqlCommand.Parameters.AddWithValue("@ReportPeriod", reportPeriod)
        sqlCommand.Parameters.AddWithValue("@Output_Folder", txtPathReport.Text)
        sqlCommand.Parameters.AddWithValue("@MachineToReport", String.Join(";", read_tree()))
        sqlCommand.Parameters.AddWithValue("@done", "")
        sqlCommand.Parameters.AddWithValue("@Scale", cmbScale.SelectedItem)
        sqlCommand.Parameters.AddWithValue("@Production", chkProduction.Checked)
        sqlCommand.Parameters.AddWithValue("@Setup", chkSetup.Checked)
        sqlCommand.Parameters.AddWithValue("@OnlySummary", chkOnlySumary.Checked)
        sqlCommand.Parameters.AddWithValue("@Sorting", cmbSorting.SelectedItem)
        sqlCommand.Parameters.AddWithValue("@shift_number", "1,2,3")
        sqlCommand.Parameters.AddWithValue("@shift_starttime", Now.ToString("HH:mm"))
        sqlCommand.Parameters.AddWithValue("@shift_endtime", Now.ToString("HH:mm"))
        sqlCommand.Parameters.AddWithValue("@short_filename", shortFileName)

        MySqlAccess.ExecuteNonQuery(sqlCommand, CSIFLEXSettings.Instance.ConnectionString)

    End Sub

    Dim reportTitle As String = ""
    Dim shortFileName As String = "False"

    Private Sub cmbReports_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbReports.SelectedIndexChanged

        cmbReportType.SelectedIndex = 0
        cmbSorting.SelectedIndex = 0
        cmbScale.SelectedIndex = 0
        rbDaily.Checked = True
        rbWeekly.Checked = False
        rbMonthly.Checked = False
        chkOnlySumary.Checked = False
        chkProduction.Checked = True
        chkSetup.Checked = True

        Dim lstMachines As New List(Of String)

        If cmbReports.SelectedIndex > 0 Then

            Dim dtReport As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.auto_report_config WHERE task_name = '{cmbReports.SelectedItem.Value.ToString()}';", CSIFLEXSettings.Instance.ConnectionString)

            If dtReport.Rows.Count > 0 Then
                Dim report As DataRow = dtReport(0)

                reportTitle = report("ReportTitle").ToString()
                shortFileName = report("short_filename").ToString()

                cmbReportType.SelectedIndex = cmbReportType.FindStringExact(report("ReportType").ToString())
                cmbSorting.SelectedIndex = cmbSorting.FindStringExact(report("Sorting").ToString())
                cmbScale.SelectedIndex = cmbScale.FindStringExact(report("Scale").ToString())

                rbDaily.Checked = (report("ReportPeriod").ToString() = "Today" Or report("ReportPeriod").ToString() = "Yesterday")
                rbWeekly.Checked = report("ReportPeriod").ToString() = "Weekly"
                rbMonthly.Checked = report("ReportPeriod").ToString() = "Monthly"

                chkOnlySumary.Checked = Boolean.Parse(report("OnlySummary").ToString())
                chkProduction.Checked = Boolean.Parse(report("Production").ToString())
                chkSetup.Checked = Boolean.Parse(report("Setup").ToString())

                Dim machines() As String = report("MachineToReport").ToString().Split(";").Select(Function(m) m.Trim()).ToArray()

                If machines(0) <> "All machines" Then
                    For Each mach As String In machines
                        lstMachines.Add(CSIFLEXSettings.MachinesIdNames.FirstOrDefault(Function(m) m.Key.ToString() = mach Or m.Value.Item1 = mach Or m.Value.Item2 = mach).Value.Item1)
                    Next
                Else
                    lstMachines.AddRange(CSIFLEXSettings.MachinesIdNames.Select(Function(m) m.Value.Item1).ToArray())
                End If

                selectMachinesNodes(lstMachines, tvMachine.Nodes(0))

            End If
        ElseIf tvMachine.Nodes.Count > 0 Then
            reportTitle = ""
            shortFileName = "False"
            selectMachinesNodes(Nothing, tvMachine.Nodes(0))
        End If

    End Sub

    Private Sub selectMachinesNodes(machines As List(Of String), node As TreeNode)

        If node.Nodes.Count > 0 Then

            For Each child As TreeNode In node.Nodes
                selectMachinesNodes(machines, child)
            Next

        ElseIf machines Is Nothing Then
            node.Checked = False
        Else
            node.Checked = machines.Contains(node.Text)
        End If

    End Sub

End Class
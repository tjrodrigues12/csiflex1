Imports System.Text.RegularExpressions
Imports System.Windows
Imports CSIFLEX.Database.Access
Imports MySql.Data.MySqlClient

Public Class AutoReporting

    Private path As String
    Private custommsg As String
    Private loadingForm As Boolean = False
    Private taskName As String
    Private reportId_ As Integer

    Dim editMode As Boolean = False

    Private Sub AutoReporting_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CSI_Lib = New CSI_Library.CSI_Library(True)

        path = CSI_Lib.getRootPath()

        loadingForm = True

        ClearAndLockForm()

        LoadMachineList()

        LoadEmailList()

        LoadReportList()

        loadingForm = False

        If dgvReports.Rows.Count > 0 Then
            dgvReports.Rows(0).Selected = True
        End If

        Dim screenHeight = SystemParameters.VirtualScreenHeight

        If Me.Height > screenHeight Then
            Me.Height = screenHeight * 0.95
        End If

        GroupBox2.Controls.Add(panelToday)
        panelToday.Location = New Drawing.Point(2, 146)

        GroupBox2.Controls.Add(panelWeekly)
        panelWeekly.Location = New Drawing.Point(2, 146)

        'grpToday.Location = New Drawing.Point(6, 125)
        'grpWeekly.Location = New Drawing.Point(6, 125)
        'grpDowntime.Location = New Drawing.Point(6, 125)

        cmbSelectReports.SelectedIndex = 0

    End Sub


    Private Sub ClearAndLockForm()

        btnAddNew.Text = "Add New"
        btnModify.Text = "Modify"
        btnAddNew.Enabled = True
        btnRemove.Enabled = False
        btnModify.Enabled = False
        btnGenerate.Enabled = False
        btnCancel.Enabled = False

        txtTaskName.Enabled = False
        txtReportTitle.Enabled = False
        cbReportType.SelectedIndex = -1
        cbReportType.Enabled = False
        cbPeriod.SelectedIndex = -1
        cbPeriod.Enabled = False

        'grpToday.Enabled = False
        'grpWeekly.Enabled = False
        'grpDowntime.Enabled = False
        grpGeneration.Enabled = False

        panelToday.Enabled = False
        panelWeekly.Enabled = False
        panelDowntime.Enabled = False

        'grpToday.Visible = False
        'grpWeekly.Visible = False
        'grpDowntime.Visible = False

        panelToday.Visible = False
        panelWeekly.Visible = False
        panelDowntime.Visible = True

    End Sub


    Private Sub LoadMachineList()

        Dim machines = MySqlAccess.GetDataTable("SELECT id, machine_name, EnetMachineName FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1")

        For Each row As DataRow In machines.Rows
            dgvMachineList.Rows.Add({"false", row("id").ToString(), row("machine_name").ToString(), row("EnetMachineName").ToString()})
        Next

    End Sub


    Private Sub LoadEmailList()

        Dim emails = MySqlAccess.GetDataTable("SELECT email_ FROM csi_auth.Users WHERE email_ IS NOT NULL AND NOT email_ = '' AND usertype <> 'machine'")

        For Each row As DataRow In emails.Rows
            dgvEmail.Rows.Add({"false", row("email_").ToString()})
        Next

    End Sub


    Private Sub LoadReportList()

        Dim sqlCmd As New Text.StringBuilder()
        sqlCmd.Append("SELECT                          ")
        sqlCmd.Append("     Id             ,            ")
        sqlCmd.Append("     Enabled        ,            ")
        sqlCmd.Append("     task_name      ,            ")
        sqlCmd.Append("     ReportType     ,            ")
        sqlCmd.Append("     ReportPeriod   ,            ")
        sqlCmd.Append("     Time_          ,            ")
        sqlCmd.Append("     Output_Folder               ")
        sqlCmd.Append(" FROM                           ")
        sqlCmd.Append("     CSI_auth.Auto_Report_config ")
        sqlCmd.Append(" WHERE                          ")
        sqlCmd.Append("     task_name <> 'CSIFLEXClientReport' ")
        sqlCmd.Append(" AND task_name <> 'SystemMonitoring'    ")

        dgvReports.DataSource = MySqlAccess.GetDataTable(sqlCmd.ToString())

    End Sub


    Private Sub dgvReports_SelectionChanged(sender As Object, e As EventArgs) Handles dgvReports.SelectionChanged

        If Not loadingForm Then
            ClearAndLockForm()
            btnModify.Enabled = True
            btnGenerate.Enabled = True

            LoadReport()
        End If

    End Sub


    Private Sub LoadReport()

        Try

            If dgvReports.CurrentCell Is Nothing Then Return

            Dim rowIndex = dgvReports.SelectedCells.Item(0).RowIndex
            Dim reportName = dgvReports.Rows(rowIndex).Cells("Task_name").Value.ToString()

            reportId_ = dgvReports.Rows(rowIndex).Cells("ReportId").Value

            Dim report = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.auto_report_config WHERE Id = { reportId_ }").Rows(0)

            txtTaskName.Text = report("Task_name").ToString()
            txtReportTitle.Text = report("ReportTitle").ToString()
            cbReportType.SelectedIndex = cbReportType.FindStringExact(report("ReportType").ToString())

            Dim period As String = report("ReportPeriod").ToString()
            If period = "Today" Or period = "Yesterday" Then period = $"Daily - {report("ReportPeriod").ToString()}"
            cbPeriod.SelectedIndex = cbPeriod.FindStringExact(period)

            cbDayOfWeek.SelectedIndex = cbDayOfWeek.FindStringExact(report("Day_").ToString())
            'cbDays.SelectedIndex = cbDays.FindStringExact(report("dayback").ToString())

            chkShift1.Checked = report("shift_number").ToString().Contains("1")
            chkShift2.Checked = report("shift_number").ToString().Contains("2")
            chkShift3.Checked = report("shift_number").ToString().Contains("3")

            cbChartSort.SelectedIndex = cbChartSort.FindStringExact(report("Sorting").ToString())
            cbScale.SelectedIndex = cbScale.FindStringExact(report("Scale").ToString())
            chkOnlySumary.Checked = Boolean.Parse(report("OnlySummary").ToString())
            chkProduction.Checked = Boolean.Parse(report("Production").ToString())
            chkSetup.Checked = Boolean.Parse(report("Setup").ToString())
            chkDisableReport.Checked = Not Boolean.Parse(report("Enabled").ToString())

            chkHideEvents.Checked = Integer.Parse(report("EventMinMinutes").ToString()) > 0
            txtEventMinutes.Text = IIf(Integer.Parse(report("EventMinMinutes").ToString()) > 0, report("EventMinMinutes").ToString(), "")

            dtpGenerationTime.Text = report("Time_").ToString()
            chkSunday.Checked = report("Day_").ToString().Contains("Sunday")
            chkMonday.Checked = report("Day_").ToString().Contains("Monday")
            chkTuesday.Checked = report("Day_").ToString().Contains("Tuesday")
            chkWednesday.Checked = report("Day_").ToString().Contains("Wednesday")
            chkThursday.Checked = report("Day_").ToString().Contains("Thursday")
            chkFriday.Checked = report("Day_").ToString().Contains("Friday")
            chkSaturday.Checked = report("Day_").ToString().Contains("Saturday")
            chkShortFileName.Checked = report("short_filename").ToString() = "True"
            chkShowSetupCycleOnTime.Checked = Integer.Parse(report("ShowConInSetup").ToString()) > 0
            chkShowOnTimeline.Visible = Integer.Parse(report("ShowConInSetup").ToString()) > 0
            chkShowOnTimeline.Checked = Integer.Parse(report("ShowConInSetup").ToString()) > 1
            txtOutputFolder.Text = report("Output_folder").ToString()

            Dim reg = New Regex("^[0-6]-[0-6]$")
            Dim timeback = report("timeback").ToString()

            If Not reg.Match(timeback).Success Then
                cmbWeekStart.SelectedIndex = 0
                cmbWeekEnd.SelectedIndex = 6
            Else
                cmbWeekStart.SelectedIndex = timeback.Split("-")(0)
                cmbWeekEnd.SelectedIndex = timeback.Split("-")(1)
            End If

            For Each row As DataGridViewRow In dgvEmail.Rows
                If report("MailTo").ToString().Contains(row.Cells(1).Value) Then
                    row.Cells(0).Value = "true"
                Else
                    row.Cells(0).Value = "false"
                End If
            Next

            Dim machines = report("MachineToReport").ToString().Split(";").Select(Function(m) m.Trim()).ToArray()
            Dim allMachines = report("MachineToReport").ToString().ToUpper().StartsWith("ALL")

            For Each row As DataGridViewRow In dgvMachineList.Rows
                If allMachines Or machines.Contains(row.Cells("MachName").Value) Or machines.Contains(row.Cells("MachId").Value) Or machines.Contains(row.Cells("EnetMachName").Value) Then
                    row.Cells("ReportOrNot").Value = "true"
                Else
                    row.Cells("ReportOrNot").Value = "false"
                End If
            Next

        Catch ex As Exception

        End Try

        btnModify.Enabled = True
        btnGenerate.Enabled = True
        btnRemove.Enabled = True

    End Sub


    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click

        If btnAddNew.Text = "Add New" Then

            reportId_ = 0

            taskName = ""

            btnAddNew.Text = "Save"
            btnRemove.Enabled = False
            btnModify.Enabled = False
            btnGenerate.Enabled = False
            btnCancel.Enabled = True

            txtTaskName.Enabled = True
            txtReportTitle.Enabled = True
            cbReportType.Enabled = True
            cbPeriod.Enabled = True

            txtTaskName.Clear()
            txtReportTitle.Clear()
            cbReportType.SelectedIndex = 0
            cbPeriod.SelectedIndex = -1
            txtTaskName.Select()

            'grpToday.Enabled = True
            panelToday.Enabled = True
            chkAllShifts.Checked = True

            'grpWeekly.Enabled = True
            panelWeekly.Enabled = True
            cbDayOfWeek.SelectedIndex = 1
            'cbDays.SelectedIndex = -1
            cmbWeekStart.SelectedIndex = 0
            cmbWeekEnd.SelectedIndex = 6

            'grpDowntime.Enabled = True
            panelDowntime.Enabled = (cbReportType.Text = "Downtime")

            cbScale.SelectedIndex = 0
            cbChartSort.SelectedIndex = 0
            chkProduction.Checked = True
            chkSetup.Checked = True
            chkOnlySumary.Checked = False
            chkHideEvents.Checked = False
            txtEventMinutes.Text = ""
            grpGeneration.Enabled = True

            chkSunday.Checked = False
            chkMonday.Checked = True
            chkTuesday.Checked = True
            chkWednesday.Checked = True
            chkThursday.Checked = True
            chkFriday.Checked = True
            chkSaturday.Checked = False

            chkShortFileName.Checked = False
            chkShowSetupCycleOnTime.Checked = False
            chkDisableReport.Checked = False
            txtOutputFolder.Clear()

            chkAllShifts.Checked = True

            For Each row As DataGridViewRow In dgvEmail.Rows
                row.Cells(0).Value = "false"
            Next

            For Each row As DataGridViewRow In dgvMachineList.Rows
                row.Cells(0).Value = "false"
            Next

        Else
            taskName = txtTaskName.Text

            Try
                If SaveReport(True) Then
                    ClearAndLockForm()
                    LoadReportList()
                End If
            Catch ex As Exception
                MessageBox.Show($"Error trying to save the report.\n{ex.Message}")
            End Try
        End If

    End Sub


    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click

        If String.IsNullOrEmpty(txtTaskName.Text) Then
            Return
        End If

        If MessageBox.Show("Do you confirm that you want to delete this report?", "Remove", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) = MessageBoxResult.No Then
            Return
        End If

        MySqlAccess.ExecuteNonQuery($"DELETE FROM csi_auth.auto_report_status WHERE ReportId = { reportId_ }; DELETE FROM csi_auth.auto_report_config WHERE Id = { reportId_ };")

        ClearAndLockForm()
        LoadReportList()

    End Sub


    Private Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click

        If btnModify.Text = "Modify" Then
            taskName = txtTaskName.Text

            btnModify.Text = "Save"
            btnAddNew.Enabled = False
            btnRemove.Enabled = False
            btnGenerate.Enabled = False
            btnCancel.Enabled = True

            txtTaskName.Enabled = True
            txtReportTitle.Enabled = True
            cbReportType.Enabled = True
            cbPeriod.Enabled = True

            'grpToday.Enabled = True
            'grpWeekly.Enabled = True
            'grpDowntime.Enabled = True
            grpGeneration.Enabled = True
            panelDowntime.Enabled = (cbReportType.Text = "Downtime")

            panelToday.Enabled = True
            panelWeekly.Enabled = True

        Else
            Try
                If SaveReport() Then
                    ClearAndLockForm()
                    LoadReportList()
                End If
            Catch ex As Exception
                MessageBox.Show($"Error trying to save the report./n{ex.Message}")
            End Try
        End If

    End Sub


    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

        If String.IsNullOrEmpty(txtTaskName.Text) Then
            Return
        End If

        Me.Cursor = Cursors.WaitCursor

        Dim report = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.auto_report_config WHERE Id = { reportId_ };").Rows(0)

        If report Is Nothing Then
            Return
        End If

        Dim listOfMachine = report("MachineToReport").ToString().Split(";")

        Dim SEvent(0 To listOfMachine.Count() - 1, 1) As String

        For i = 0 To listOfMachine.Count() - 1
            SEvent(i, 0) = listOfMachine(i)
            SEvent(i, 1) = "SETUP"
        Next

        Dim startdate As DateTime
        Dim enddate As DateTime = Today.Date.AddSeconds(-1)

        If cbPeriod.SelectedItem.ToString().Contains("Today") Then
            Dim hour = Convert.ToInt16(report("shift_starttime").ToString().Split(":")(0))
            Dim minute = Convert.ToInt16(report("shift_starttime").ToString().Split(":")(1))
            startdate = Today.Date

            hour = Convert.ToInt16(report("shift_endtime").ToString().Split(":")(0))
            minute = Convert.ToInt16(report("shift_endtime").ToString().Split(":")(1))
            enddate = Today.Date.Add(New TimeSpan(hour, minute, 0))

        ElseIf cbPeriod.SelectedItem.ToString().Contains("Yesterday") Then
            startdate = Today.Date.AddDays(-1)

        ElseIf cbPeriod.SelectedItem.ToString().Contains("Weekly") Then
            startdate = Today.Date.AddDays(Integer.Parse(report("dayback").ToString()) * -1)

        ElseIf cbPeriod.SelectedItem.ToString().Contains("Monthly") Then
            startdate = Today.Date.AddMonths(-1)

        End If

        Dim fileToSend As String

        Try
            fileToSend = CSI_Lib.generateReport(
                listOfMachine,
                report("ReportType").ToString(),
                report("ReportPeriod").ToString(),
                True,
                startdate,
                enddate,
                report("Output_Folder").ToString(),
                SEvent,
                True,
                report("short_FileName").ToString(),
                report("Task_name").ToString())

        Catch ex As Exception

            Me.Cursor = Cursors.Default
            MessageBox.Show("Error generating report, MSG:" & ex.ToString())
            Return
        End Try

        If Not String.IsNullOrEmpty(report("MailTo").ToString()) Then

            Dim custommsg As String = Convert.ToString(report("CustomMsg"))

            If custommsg.Length = 0 Then
                If report("ReportPeriod").ToString().Contains("Today") Then
                    'Today
                    custommsg = $"Your Today's CSIFlex report On { Now.ToString("MMMM, dd yyyy hh:mm") }"
                ElseIf report("ReportPeriod").ToString().Contains("Monthly") Then
                    'Monthly Reporting
                    custommsg = $"Your Monthly CSIFlex report On { Now.ToString("MMMM, dd yyyy hh:mm") }"
                ElseIf report("ReportPeriod").ToString().Contains("Weekly") Then
                    'Weekly Reporting
                    custommsg = $"Your Weekly CSIFlex report On { Now.ToString("MMMM, dd yyyy hh:mm") }"
                ElseIf report("ReportPeriod").ToString().Contains("Yesterday") Then
                    'Daily ( Yesterday ) Availability - PDF
                    custommsg = $"Your Daily CSIFlex report On { Now.ToString("MMMM, dd yyyy hh:mm") }"
                End If
            Else
                custommsg = report("CustomMsg").ToString()
            End If

            Try
                Dim subject As String = $"CSIFLEX Report - {report("ReportTitle").ToString()}"

                If String.IsNullOrEmpty(report("ReportTitle").ToString()) Then subject = $"CSIFLEX {report("ReportType").ToString()} Report {report("ReportPeriod").ToString()}"

                CSI_Lib.sendReportByMail(report("MailTo"), fileToSend, custommsg, subject)

            Catch ex As Exception
                MessageBox.Show("Error trying to send report by email, MSG:" & ex.ToString())

            End Try
        End If

        Me.Cursor = Cursors.Default

    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        ClearAndLockForm()
        LoadReportList()

    End Sub


    Private Sub btnCustomEmail_Click(sender As Object, e As EventArgs) Handles btnCustomEmail.Click
        Dim frm_custommsg As New CustomEmailMsg()
        frm_custommsg.taskname = txtTaskName.Text
        frm_custommsg.ShowDialog()
    End Sub


    Private Sub cbReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbReportType.SelectedIndexChanged
        If cbReportType.SelectedItem = "Downtime" Then
            'grpDowntime.Visible = True
            panelDowntime.Enabled = True
        Else
            'grpDowntime.Visible = False
            panelDowntime.Enabled = False
        End If
    End Sub


    Private Sub cbPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPeriod.SelectedIndexChanged

        'grpToday.Visible = False
        'grpWeekly.Visible = False

        panelToday.Visible = False
        panelWeekly.Visible = False

        If cbPeriod.SelectedItem = Nothing Then
            Return
        End If

        If cbPeriod.SelectedItem.ToString().StartsWith("Daily") Then
            'grpToday.Visible = True
            panelToday.Visible = True
            panelWeekly.SendToBack()

        ElseIf cbPeriod.SelectedItem = "Weekly" Then
            'grpWeekly.Visible = True
            panelWeekly.Visible = True
            panelToday.SendToBack()

        End If

        If cbPeriod.SelectedItem.ToString().Contains("Daily") Then
            chkSunday.Enabled = True
            chkMonday.Enabled = True
            chkTuesday.Enabled = True
            chkWednesday.Enabled = True
            chkThursday.Enabled = True
            chkFriday.Enabled = True
            chkSaturday.Enabled = False
        Else
            chkSunday.Enabled = False
            chkMonday.Enabled = False
            chkTuesday.Enabled = False
            chkWednesday.Enabled = False
            chkThursday.Enabled = False
            chkFriday.Enabled = False
            chkSaturday.Enabled = False
        End If

    End Sub


    Private Function GetListOfSelectedMachine() As String

        Dim result As New Text.StringBuilder()

        For Each row As DataGridViewRow In dgvMachineList.Rows
            If (Boolean.Parse(row.Cells("ReportOrNot").Value)) Then
                If result.Length > 0 Then result.Append(";")
                result.Append(row.Cells("MachId").Value)
            End If
        Next

        Return result.ToString()

    End Function


    Private Function GetListOfMailSelected() As String

        Dim result As New Text.StringBuilder()

        For Each row As DataGridViewRow In dgvEmail.Rows
            If (Boolean.Parse(row.Cells(0).Value)) Then
                If result.Length > 0 Then result.Append(";")
                result.Append(row.Cells(1).Value)
            End If
        Next

        Return result.ToString()

    End Function


    Private Function getListOfDaySelected() As String

        If cbPeriod.SelectedIndex = -1 Then Return ""

        If cbPeriod.SelectedItem.ToString().Contains("Daily") Then

            Dim lstDays = New List(Of String)()
            If chkSunday.Checked Then lstDays.Add("Sunday")
            If chkMonday.Checked Then lstDays.Add("Monday")
            If chkTuesday.Checked Then lstDays.Add("Tuesday")
            If chkWednesday.Checked Then lstDays.Add("Wednesday")
            If chkThursday.Checked Then lstDays.Add("Thursday")
            If chkFriday.Checked Then lstDays.Add("Friday")
            If chkSaturday.Checked Then lstDays.Add("Saturday")

            Return String.Join(", ", lstDays.ToArray())

            'If chkSunday.Checked Then
            '    Return "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday"
            'Else
            '    If cbPeriod.SelectedItem.ToString().Contains("Today") Then
            '        Return "Monday, Tuesday, Wednesday, Thursday, Friday"
            '    Else
            '        Return "Tuesday, Wednesday, Thursday, Friday, Saturday"
            '    End If
            'End If
        ElseIf cbPeriod.SelectedItem.ToString().Contains("Weekly") Then
            Return cbDayOfWeek.SelectedItem.ToString()
        ElseIf cbPeriod.SelectedItem.ToString().Contains("Monthly") Then
            Return "01"
        Else
            Return ""
        End If

    End Function


    Private Function SaveReport(Optional isNew As Boolean = False) As Boolean

        If String.IsNullOrWhiteSpace(txtTaskName.Text) Then
            MessageBox.Show("You must inform a task name!")
            txtTaskName.Select()
            Return False
        End If

        If cbPeriod.SelectedIndex = -1 Then
            MessageBox.Show("You must inform a periodicity for the report!")
            cbPeriod.Select()
            Return False
        End If

        If panelWeekly.Visible And cbDayOfWeek.SelectedIndex = -1 Then
            MessageBox.Show("You must inform a day of week to generate the report!")
            cbDayOfWeek.Select()
            Return False
        End If

        'If grpWeekly.Visible And cbDays.SelectedIndex = -1 Then
        '    MessageBox.Show("You must inform how many days to generate the report!")
        '    cbDays.Select()
        '    Return False
        'End If

        If panelToday.Visible And Not chkShift1.Checked And Not chkShift2.Checked And Not chkShift3.Checked Then
            MessageBox.Show("You must select a shift to generate the report!")
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtOutputFolder.Text) Then
            MessageBox.Show("You must inform a folder to generate the report!")
            txtOutputFolder.Select()
            Return False
        End If

        If GetListOfSelectedMachine().Length = 0 Then
            MessageBox.Show("You must select one or more machines to generate the report!")
            Return False
        End If

        If isNew Then
            Dim existTaskName = MySqlAccess.ExecuteScalar($"SELECT Task_name FROM csi_auth.auto_report_config WHERE Task_name = '{taskName}'")

            If existTaskName IsNot Nothing Then
                If Not MessageBox.Show($"The report ""{taskName}"" already exists. Would you like to replace it?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) = MessageBoxResult.Yes Then
                    Return False
                End If
            End If
        End If

        If Not txtTaskName.Text = taskName Then
            MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.auto_report_config SET Task_name = '{txtTaskName.Text}' WHERE Id = { reportId_ }")
        End If

        Dim period As String = cbPeriod.SelectedItem
        If period.Contains("Today") Then period = "Today"
        If period.Contains("Yesterday") Then period = "Yesterday"

        Dim shifts As String = ""
        If chkShift1.Checked Then shifts = "1"
        If chkShift2.Checked Then shifts += ",2"
        If chkShift3.Checked Then shifts += ",3"
        If shifts.StartsWith(",") Then shifts = shifts.Substring(1)

        Dim done As String = ""
        If dtpGenerationTime.Value < Now Then
            done = Today.ToString("yyyy-MM-dd")
        End If

        Dim sqlCmd As New Text.StringBuilder()

        If reportId_ = 0 Then
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
            sqlCmd.Append($"    MailTo           ,                   ")
            sqlCmd.Append($"    done             ,                   ")
            sqlCmd.Append($"    dayback          ,                   ")
            sqlCmd.Append($"    timeback         ,                   ")
            sqlCmd.Append($"    CustomMsg        ,                   ")
            sqlCmd.Append($"    Scale            ,                   ")
            sqlCmd.Append($"    Production       ,                   ")
            sqlCmd.Append($"    Setup            ,                   ")
            sqlCmd.Append($"    OnlySummary      ,                   ")
            sqlCmd.Append($"    Enabled          ,                   ")
            sqlCmd.Append($"    Sorting          ,                   ")
            sqlCmd.Append($"    EventMinMinutes  ,                   ")
            sqlCmd.Append($"    shift_number     ,                   ")
            sqlCmd.Append($"    shift_starttime  ,                   ")
            sqlCmd.Append($"    shift_endtime    ,                   ")
            sqlCmd.Append($"    short_filename   ,                   ")
            sqlCmd.Append($"    ShowConInSetup                       ")
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
            sqlCmd.Append($"    @MailTo          ,                   ")
            sqlCmd.Append($"    @done            ,                   ")
            sqlCmd.Append($"    @dayback         ,                   ")
            sqlCmd.Append($"    @timeback        ,                   ")
            sqlCmd.Append($"    @CustomMsg       ,                   ")
            sqlCmd.Append($"    @Scale           ,                   ")
            sqlCmd.Append($"    @Production      ,                   ")
            sqlCmd.Append($"    @Setup           ,                   ")
            sqlCmd.Append($"    @OnlySummary     ,                   ")
            sqlCmd.Append($"    @Enabled         ,                   ")
            sqlCmd.Append($"    @Sorting         ,                   ")
            sqlCmd.Append($"    @EventMinMinutes ,                   ")
            sqlCmd.Append($"    @shift_number    ,                   ")
            sqlCmd.Append($"    @shift_starttime ,                   ")
            sqlCmd.Append($"    @shift_endtime   ,                   ")
            sqlCmd.Append($"    @short_filename  ,                   ")
            sqlCmd.Append($"    @ShowConInSetup                      ")
            sqlCmd.Append($" );                                      ")

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
            sqlCmd.Append($"    MailTo          = @MailTo          , ")
            sqlCmd.Append($"    dayback         = @dayback         , ")
            sqlCmd.Append($"    timeback        = @timeback        , ")
            sqlCmd.Append($"    CustomMsg       = @CustomMsg       , ")
            sqlCmd.Append($"    Scale           = @Scale           , ")
            sqlCmd.Append($"    Production      = @Production      , ")
            sqlCmd.Append($"    Setup           = @Setup           , ")
            sqlCmd.Append($"    OnlySummary     = @OnlySummary     , ")
            sqlCmd.Append($"    Enabled         = @Enabled         , ")
            sqlCmd.Append($"    Sorting         = @Sorting         , ")
            sqlCmd.Append($"    EventMinMinutes = @EventMinMinutes , ")
            sqlCmd.Append($"    shift_number    = @shift_number    , ")
            sqlCmd.Append($"    shift_starttime = @shift_starttime , ")
            sqlCmd.Append($"    shift_endtime   = @shift_endtime   , ")
            sqlCmd.Append($"    short_filename  = @short_filename  , ")
            sqlCmd.Append($"    ShowConInSetup  = @ShowConInSetup    ")
            sqlCmd.Append($"WHERE Id = { reportId_ }               ; ")

            sqlCmd.Append($"UPDATE csi_auth.auto_report_status SET   ")
            sqlCmd.Append($"    Status          = @Done              ")
            sqlCmd.Append($"WHERE ReportId = { reportId_ }         ; ")

        End If


        Dim sqlCommand As New MySqlCommand(sqlCmd.ToString())
        sqlCommand.Parameters.AddWithValue("@Task_name", txtTaskName.Text)
        sqlCommand.Parameters.AddWithValue("@Day_", getListOfDaySelected())
        sqlCommand.Parameters.AddWithValue("@Time_", dtpGenerationTime.Text)
        sqlCommand.Parameters.AddWithValue("@ReportType", cbReportType.SelectedItem)
        sqlCommand.Parameters.AddWithValue("@ReportTitle", txtReportTitle.Text)
        sqlCommand.Parameters.AddWithValue("@ReportPeriod", period)
        sqlCommand.Parameters.AddWithValue("@Output_Folder", txtOutputFolder.Text)
        sqlCommand.Parameters.AddWithValue("@MachineToReport", GetListOfSelectedMachine())
        sqlCommand.Parameters.AddWithValue("@MailTo", GetListOfMailSelected())
        sqlCommand.Parameters.AddWithValue("@done", done)
        sqlCommand.Parameters.AddWithValue("@dayback", 7)
        sqlCommand.Parameters.AddWithValue("@timeback", $"{cmbWeekStart.SelectedIndex}-{cmbWeekEnd.SelectedIndex}")
        sqlCommand.Parameters.AddWithValue("@CustomMsg", custommsg)
        sqlCommand.Parameters.AddWithValue("@Scale", cbScale.SelectedItem)
        sqlCommand.Parameters.AddWithValue("@Production", chkProduction.Checked)
        sqlCommand.Parameters.AddWithValue("@Setup", chkSetup.Checked)
        sqlCommand.Parameters.AddWithValue("@OnlySummary", chkOnlySumary.Checked)
        sqlCommand.Parameters.AddWithValue("@Sorting", cbChartSort.SelectedItem)
        sqlCommand.Parameters.AddWithValue("@EventMinMinutes", IIf(String.IsNullOrEmpty(txtEventMinutes.Text), 0, txtEventMinutes.Text))
        sqlCommand.Parameters.AddWithValue("@Enabled", Not chkDisableReport.Checked)
        sqlCommand.Parameters.AddWithValue("@shift_number", shifts)
        sqlCommand.Parameters.AddWithValue("@shift_starttime", Now.ToString("HH:mm"))
        sqlCommand.Parameters.AddWithValue("@shift_endtime", Now.ToString("HH:mm"))
        sqlCommand.Parameters.AddWithValue("@short_filename", IIf(chkShortFileName.Checked, "True", "False"))
        sqlCommand.Parameters.AddWithValue("@ShowConInSetup", IIf(chkShowSetupCycleOnTime.Checked, IIf(chkShowOnTimeline.Checked, 2, 1), 0))

        MySqlAccess.ExecuteNonQuery(sqlCommand)

        Return True

    End Function


    Private Sub BTN_Output_Click(sender As Object, e As EventArgs) Handles BTN_Output.Click

        Dim folderDlg As New FolderBrowserDialog

        folderDlg.ShowNewFolderButton = False
        folderDlg.ShowNewFolderButton = True
        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            txtOutputFolder.Text = folderDlg.SelectedPath
            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
        End If

    End Sub


    Private Sub chkShifts_CheckedChanged(sender As Object, e As EventArgs) Handles chkShift3.CheckedChanged, chkShift2.CheckedChanged, chkShift1.CheckedChanged, chkAllShifts.CheckedChanged

        Dim chkObj As CheckBox = sender

        If editMode Then Return

        editMode = True

        If chkObj.Name = "chkAllShifts" Then
            chkShift1.Checked = chkAllShifts.Checked
            chkShift2.Checked = chkAllShifts.Checked
            chkShift3.Checked = chkAllShifts.Checked
        Else
            chkAllShifts.Checked = (chkShift1.Checked And chkShift2.Checked And chkShift3.Checked)
        End If

        editMode = False

    End Sub


    Private Sub chkDisableReport_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisableReport.CheckedChanged

        lblReportDisabled.Visible = chkDisableReport.Checked

    End Sub


    Private Sub dgvReports_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvReports.RowPostPaint

        Dim rowIdx = e.RowIndex

        If rowIdx < 0 Then Return

        Dim row = dgvReports.Rows(rowIdx)
        Dim isEnabled As Boolean = Boolean.Parse(row.Cells("Enabled").Value)

        If Not isEnabled Then
            row.DefaultCellStyle.ForeColor = Color.Gray
            row.DefaultCellStyle.SelectionBackColor = Color.LightCoral
            row.DefaultCellStyle.SelectionForeColor = Color.Black
        End If

    End Sub


    Private Sub OnlyNumbers_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtEventMinutes.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub


    Private Sub chkHideEvents_CheckedChanged(sender As Object, e As EventArgs) Handles chkHideEvents.CheckedChanged
        txtEventMinutes.Enabled = chkHideEvents.Checked

        If Not chkHideEvents.Checked Then txtEventMinutes.Text = ""
    End Sub


    Private Sub cmbSelectReports_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSelectReports.SelectedIndexChanged

        Dim period As String

        RemoveHandler dgvReports.SelectionChanged, AddressOf dgvReports_SelectionChanged

        For Each dgrow As DataGridViewRow In dgvReports.Rows

            dgrow.Cells().Item("Generate").Value = False
            period = dgrow.Cells().Item("Periodicity").Value.ToString()

            If CBool(dgrow.Cells().Item("Enabled").Value) Then

                If (cmbSelectReports.SelectedIndex = 1) Or
                   (cmbSelectReports.SelectedIndex = 2 And (period = "Today" Or period = "Yesterday")) Or
                   (cmbSelectReports.SelectedIndex = 3 And period = "Today") Or
                   (cmbSelectReports.SelectedIndex = 4 And period = "Yesterday") Or
                   (cmbSelectReports.SelectedIndex = 5 And period = "Weekly") Or
                   (cmbSelectReports.SelectedIndex = 6 And period = "Monthly") Then
                    dgrow.Cells().Item("Generate").Value = True
                End If
            End If
        Next

        AddHandler dgvReports.SelectionChanged, AddressOf dgvReports_SelectionChanged

    End Sub


    Private Sub btnGenerateSelectedReports_Click(sender As Object, e As EventArgs) Handles btnGenerateSelectedReports.Click

        For Each dgrow As DataGridViewRow In dgvReports.Rows
            If dgrow.Cells().Item("Generate").Value Then
                MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.auto_report_status SET Status = 'forced' WHERE ReportId = {dgrow.Cells().Item("ReportId").Value.ToString()}")
            End If
        Next

        cmbSelectReports.SelectedIndex = 0
        MessageBox.Show("The selected reports are being generated automatically")

    End Sub


    Private Sub dgvReports_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvReports.CurrentCellDirtyStateChanged

        If dgvReports.IsCurrentCellDirty Then
            dgvReports.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub


    Private Sub dgvReports_CellValueChanged(sender As Object, e As EventArgs) Handles dgvReports.CellValueChanged

        Dim checked = False

        For Each dgrow As DataGridViewRow In dgvReports.Rows
            If dgrow.Cells().Item("Generate").Value Then
                checked = True
                Exit For
            End If
        Next

        btnGenerateSelectedReports.Enabled = checked

    End Sub

    Private Sub chkShowSetupCycleOnTime_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowSetupCycleOnTime.CheckedChanged
        chkShowOnTimeline.Visible = chkShowSetupCycleOnTime.Checked
    End Sub

End Class
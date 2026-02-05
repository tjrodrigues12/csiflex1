Imports MySql.Data.MySqlClient
Public Class Autoreporting
    Dim reports As List(Of Report)
    Sub New()
        reports = New List(Of Report)
        Dim Dataset_AutoReport As DataSet = New DataSet()
        Dim conn As MySqlConnection = New MySqlConnection()
        Dim adap As New MySqlDataAdapter()

        conn.ConnectionString = CSI_Library.MySqlConnectionString
        conn.Open()

        Dim QryStr As String = String.Format("SELECT * FROM csi_auth.{0}", "Auto_Report_config")
        adap = New MySqlDataAdapter(QryStr, conn)
        adap.Fill(Dataset_AutoReport, "tableDGV")

        For Each row As DataRow In Dataset_AutoReport.Tables("tableDGV").Rows
            Try
                Dim report_name As String = row("Task_name").ToString()
                Dim report_type As String = row("Report_Type").ToString()
                Dim report_folder As String = row("Output_Folder").ToString()
                Dim listOfMachine As String() = row("MachineToReport").ToString().Split(";")
                Dim custommsg As String = row("CustomMsg").ToString()
                Dim mailto As String = row("MailTo").ToString()

                Dim SEvent(0 To listOfMachine.Count() - 1, 1) As String
                For i = 0 To listOfMachine.Count() - 1
                    SEvent(i, 0) = listOfMachine(i)
                    SEvent(i, 1) = "SETUP"
                Next

                Dim dayback As Integer = 0
                If Not (row("dayback") Is Nothing Or row("dayback").ToString().Length = 0) Then
                    dayback = Integer.Parse(row("dayback").ToString())
                End If

                Dim gen_days As String() = row("Day_").ToString().Replace(" ", "").Split(",")
                Dim gen_time As String = row("Time_").ToString()

                reports.Add(New Report(report_name, report_type, report_folder, listOfMachine, SEvent, dayback, gen_days, gen_time, custommsg, mailto))
            Catch ex As Exception
                Console.WriteLine("Unable to read auto report table. Exception:" + ex.Message)
            End Try
        Next
    End Sub

    Sub Dispose()
        For Each report In reports
            report.Clear()
        Next
        Me.Finalize()
    End Sub
End Class

Public Class Report
    Dim _report_id As String
    Dim _report_type As String
    Dim _report_folder As String
    Dim _report_machines As String()
    Dim _report_events As String(,)
    Dim _report_days As Integer
    Dim _report_gen_on As String()
    Dim _report_gen_at As String
    Dim _report_startdate As DateTime
    Dim _report_enddate As DateTime
    Dim _report_custom_msg As String
    Dim _report_mailto As String

    Dim _tick_at As DateTime

    Dim timer As System.Threading.Timer
    Dim csi_lib As New CSI_Library(True)

    Sub New(id As String, type As String, folder As String, machines As String(), events As String(,), dayback As Integer, gen_days As String(), gen_time As String, custom_msg As String, mailto As String)
        _report_id = id
        _report_type = type
        _report_folder = folder
        _report_machines = machines
        _report_events = events
        _report_days = dayback
        _report_gen_on = gen_days
        _report_gen_at = gen_time
        _report_custom_msg = custom_msg
        _report_mailto = mailto

        'Detect type
        SetTimeframe()
        'Set up timer
        CreateTimer()
    End Sub

    Sub Clear()
        timer.Dispose()
        Me.Finalize()
    End Sub

    Function IsInStrArray(stringToBeFound As String, arr As String()) As Boolean
        IsInStrArray = (UBound(Filter(arr, stringToBeFound)) > -1)
    End Function

    Function DayOfWeekInt(day As String) As Integer
        Select Case day
            Case "Monday"
                DayOfWeekInt = 1
            Case "Tuesday"
                DayOfWeekInt = 2
            Case "Wednesday"
                DayOfWeekInt = 3
            Case "Thursday"
                DayOfWeekInt = 4
            Case "Friday"
                DayOfWeekInt = 5
            Case "Saturday"
                DayOfWeekInt = 6
            Case "Sunday"
                DayOfWeekInt = 7
            Case Else
                DayOfWeekInt = 0
        End Select
    End Function

    Function FindNextOccurence() As String
        Dim found As Boolean = False
        For Each daystr As String In _report_gen_on
            If (Not found And DayOfWeekInt(daystr) > Now.DayOfWeek) Then
                FindNextOccurence = daystr
                found = True
            End If
        Next

        If Not found Then
            FindNextOccurence = _report_gen_on(0)
        End If
    End Function

    Private Sub SetTimeframe()
        If _report_type.Contains("Monthly") Then
            'run monthly for last month
            Dim generation_time As DateTime = DateTime.Parse(_report_gen_at)
            If (Now.Day = 1) And (generation_time.TimeOfDay > Now.TimeOfDay) Then
                'generate later today
                _report_enddate = Now.AddMonths(-1)
                '_next_tick = (generation_time - Now).Milliseconds
                _tick_at = generation_time
            Else
                'generate on next day occurence
                _report_enddate = Now
                _tick_at = New DateTime(Now.AddMonths(1).Year, Now.AddMonths(1).Month, 1, generation_time.Hour, generation_time.Minute, 0)
                '_next_tick = (tick - Now).Milliseconds
            End If

            _report_startdate = New DateTime(_report_enddate.Year, _report_enddate.Month, 1, 0, 0, 0)
            _report_enddate = New DateTime(_report_enddate.Year, _report_enddate.Month, DateTime.DaysInMonth(_report_enddate.Year, _report_enddate.Month), 23, 59, 59)
        ElseIf _report_type.Contains("Weekly") Then
            'run weekly report for last week
            Dim generation_time As DateTime = DateTime.Parse(_report_gen_at)
            'If (_report_gen_on = Now.DayOfWeek.ToString()) And (generation_time.TimeOfDay > Now.TimeOfDay) Then
            If (IsInStrArray(Now.DayOfWeek.ToString(), _report_gen_on)) And (generation_time.TimeOfDay > Now.TimeOfDay) Then
                'generate later today
                _report_enddate = Now
                '_next_tick = (generation_time - Now).Milliseconds
                _tick_at = generation_time
            Else
                'generate on next day occurence
                _report_enddate = GetNextWeekday(DateTime.Now.AddDays(1), DayOfWeekInt(FindNextOccurence()))
                _tick_at = New DateTime(_report_enddate.Year, _report_enddate.Month, _report_enddate.Day, generation_time.Hour, generation_time.Minute, 0)
                '_next_tick = (tick - Now).Milliseconds
            End If

            _report_enddate = _report_enddate.AddDays(-1)
            _report_startdate = _report_enddate.AddDays(-_report_days)
            _report_startdate = New DateTime(_report_startdate.Year, _report_startdate.Month, _report_startdate.Day, 0, 0, 0)
            _report_enddate = New DateTime(_report_enddate.Year, _report_enddate.Month, _report_enddate.Day, 23, 59, 59)
        ElseIf _report_type.Contains("Daily") Then
            'run daily for last day
            Dim generation_time As DateTime = DateTime.Parse(_report_gen_at)
            If IsInStrArray(Now.DayOfWeek.ToString(), _report_gen_on) And (generation_time.TimeOfDay > Now.TimeOfDay) Then
                'generate later today
                _report_enddate = Now.AddDays(-1)
                _tick_at = generation_time
            Else
                'generate on next day occurence
                _report_enddate = GetNextWeekday(DateTime.Now, DayOfWeekInt(FindNextOccurence())).AddDays(-1)
                _tick_at = New DateTime(_report_enddate.AddDays(1).Year, _report_enddate.AddDays(1).Month, _report_enddate.AddDays(1).Day, generation_time.Hour, generation_time.Minute, 0)
            End If

            _report_startdate = _report_enddate
            _report_startdate = New DateTime(_report_startdate.Year, _report_startdate.Month, _report_startdate.Day, 0, 0, 0)
            _report_enddate = New DateTime(_report_enddate.Year, _report_enddate.Month, _report_enddate.Day, 23, 59, 59)
        Else
            csi_lib.LogServiceError(Now.ToString() + "invalid report type:" + _report_type + "TASK:" + _report_id, 1)
        End If
    End Sub

    Private Function GetNextWeekday(start As DateTime, day As DayOfWeek) As DateTime
        ' The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
        Dim daysToAdd As Integer = (CInt(day) - CInt(start.DayOfWeek) + 7) Mod 7
        Return start.AddDays(daysToAdd)
    End Function

    Private Sub CreateTimer()
        Dim disablePeriodic As New TimeSpan(0, 0, 0, 0, -1)
        timer = New System.Threading.Timer(AddressOf timer_Ticked, Nothing, (_tick_at - Now), disablePeriodic)
    End Sub

    Private Async Sub timer_Ticked()
        timer.Dispose()
        Await Task.Run(AddressOf GenerateReport)
        UpdateTick()
    End Sub

    Private Sub GenerateReport()
        Dim fileToSend As String = ""

        '  Commented by Drausio in 2019-06-28
        '
        'Try
        '    csi_lib.LogServiceAction("GenerateReport report---:" + _report_id + " at " + Now.ToString(), 1)
        '    Dim fileToSend_ As String = csi_lib.generateReport(_report_machines, _report_type, _report_startdate, _report_enddate, _report_folder, _report_events, True, "True", _report_id)
        '    fileToSend = fileToSend_
        '    csi_lib.LogServiceError(" report generated.", 1)
        'Catch ex As Exception
        '    csi_lib.LogServiceError("Error trying to generate the report:" & ex.ToString(), 1)
        'End Try

        'Try
        '    If Not (_report_mailto Is Nothing Or _report_mailto.Length = 0 Or fileToSend = "") Then
        '        csi_lib.sendReportByMail(_report_mailto, fileToSend, _report_custom_msg)
        '    Else
        '        csi_lib.LogServiceError("Report not sent: pb with _report_mailto or fileToSend. " & _report_mailto & fileToSend, 1)
        '    End If
        'Catch ex As Exception
        '    csi_lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
        'End Try

        'csi_lib.LogServiceError("report " + _report_id + " completed at " + Now.ToString() + " file:" + fileToSend, 1)

    End Sub

    Private Sub UpdateTick()
        SetTimeframe()
        Dim disablePeriodic As New TimeSpan(0, 0, 0, 0, -1)
        timer = New System.Threading.Timer(AddressOf timer_Ticked, Nothing, (_tick_at - Now), disablePeriodic)
    End Sub
End Class

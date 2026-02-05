Imports System.Threading
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class Report_Generator
    Public Shared CSI_Lib As New CSI_Library.CSI_Library
    Public Shared cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
    Public Shared thread_Start_AutoReporting As Thread
    Public Shared KeepReportingON As Boolean = False
    Public Shared ListOfReportingTime As New List(Of String)
    Public Shared ServiceStarted As Integer = 0
    Public Shared TomorrowDate As Date
    Public Shared IsLopped As Boolean = False
    Private Shared Timer_Reporting As New System.Timers.Timer
    Private Shared Timer_LoadListAtMidNight As New System.Timers.Timer

    'Private dateReporting As DateTime
    Public Sub StartReportingServiceThread()

        thread_Start_AutoReporting = New Thread(AddressOf thread_AutoReporting)
        thread_Start_AutoReporting.Name = "Start Auto Reporting Thread In Service"
        thread_Start_AutoReporting.IsBackground = True

        thread_Start_AutoReporting.Start()
    End Sub

    Private Sub Timer_LoadListAtMidNight_Tick()

        Timer_LoadListAtMidNight.Stop()
        If Timer_Reporting.Enabled = True Then
            Timer_Reporting.Stop()
        End If

        LoadListOfTime()
        Dim SetTimeIntervalForMidnight = Convert.ToInt32((DateTime.Now.AddDays(1).Date.AddSeconds(1) - DateTime.Now).TotalSeconds)
        Timer_LoadListAtMidNight.Interval = SetTimeIntervalForMidnight
        Timer_LoadListAtMidNight.Start()
        Timer_Reporting.Start()

    End Sub


    Private Sub Timer_Reporting_Tick()

        'Stop the Timer and Perform the Task of Reporting
        Timer_Reporting.Stop()
        'Call Reporting Service 
        GenerateReports()

    End Sub

    Public Shared Sub StopReportServiceThread()
        'This below way is give some time to close the thread
        If Not thread_Start_AutoReporting.Join(3000) Then
            ' give the thread 3 seconds to stop
            thread_Start_AutoReporting.Abort()
        End If
    End Sub

    Public Shared Sub thread_AutoReporting()

        IsLopped = True
        StartAutoreporting()

    End Sub

    Public Shared Sub StartAutoreporting()
        While True
            Thread.Sleep(30000) 'sleep thread for 30 seconds to improve performance  
            If IsLopped = True Then
                'Do reporting
                Try
                    Dim threadcollection As ProcessThreadCollection = Process.GetCurrentProcess().Threads
                    For Each thread In threadcollection
                        Console.WriteLine(thread)
                    Next
                    ReportingDecisionMaker()
                    While KeepReportingON = True
                        'Reporting Generate Code 
                        GenerateReports()
                    End While
                Catch ex As Exception
                    CSI_Lib.LogServerError("Error in Running Reporting Service From IsLopped = True : " & ex.Message & "StackTrace :" & ex.StackTrace, 1)
                    If Not thread_Start_AutoReporting.IsAlive Then
                        thread_Start_AutoReporting.Start()
                    End If
                End Try
            ElseIf IsLopped = False Then
                Try
                    'Keep Service Running Until New Entry added, Day Changed 
                    If DateTime.Now.Date = TomorrowDate Then
                        'when day change we are loading the list from database again
                        LoadListOfTime()
                        TomorrowDate = DateTime.Now.AddDays(1).Date
                        IsLopped = True
                    End If
                Catch ex As Exception
                    CSI_Lib.LogServerError("Error in Running Reporting Service  From IsLopped = False : " & ex.Message & "StackTrace :" & ex.StackTrace, 1)
                    If Not thread_Start_AutoReporting.IsAlive Then
                        thread_Start_AutoReporting.Start()
                    End If
                End Try
            End If
        End While
    End Sub

    Public Shared Sub ReportingDecisionMaker()
        If ServiceStarted = 0 Then
            'Start the  service for first time
            LoadListOfTime()
            ServiceStarted = 1
            TomorrowDate = DateTime.Now.AddDays(1).Date
        End If
        If DateTime.Now.Date.ToString("dd/MM/yyyy") = TomorrowDate.ToString("dd/MM/yyyy") Then
            'when day change we are loading the list from database again
            LoadListOfTime()
            TomorrowDate = DateTime.Now.AddDays(1).Date
        End If
        'Load List with New Values
        'Take the First Value From the List to Consider the time when Next Report Will Generate 
        If ListOfReportingTime.Count > 0 Then
            Dim FirstListElement = ListOfReportingTime.First()
            If DateTime.Now.ToString("HH:mm") = FirstListElement Then
                'Means we reached the time of reporting and we will genereate report 
                KeepReportingON = True
            ElseIf DateTime.Now.ToString("HH:mm") > FirstListElement Then
                ListOfReportingTime.RemoveAt(0)
                KeepReportingON = False
            Else
                KeepReportingON = False
                ' we are not at the reporting time yet
            End If
        Else
            'Stop the Looping until we have some values in the List
            IsLopped = False
            StartAutoreporting()
            'KeepReportingON = False
        End If
    End Sub


    Public Shared Sub LoadListOfTime()
        Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        If ListOfReportingTime.Count > 0 Then
            ListOfReportingTime.Clear()
        End If
        Try
            cntsql.Open()
            Dim getAllTimes As String = "SELECT DISTINCT(Time_) FROM csi_auth.auto_report_config where done <> '" & DateTime.Now.DayOfWeek & "' Order By Time_ ASC;"
            Dim cmdgetAllTimes As New MySqlCommand(getAllTimes, cntsql)
            Dim mysqlReadergetAllTimes As MySqlDataReader = cmdgetAllTimes.ExecuteReader()
            Dim dTable_getAllTimes As New DataTable()
            dTable_getAllTimes.Load(mysqlReadergetAllTimes)
            If dTable_getAllTimes.Rows.Count > 0 Then
                For Each rows As DataRow In dTable_getAllTimes.Rows
                    ListOfReportingTime.Add(rows.Item("Time_").ToString)
                Next
            End If
            cntsql.Close()
        Catch ex As Exception
            CSI_Lib.LogServerError("Error while adding times to Dictionary of Reporting :" & ex.Message & ex.StackTrace, 1)
        Finally
            cntsql.Close()
        End Try
    End Sub


    Public Shared Sub GenerateReports()

        CSI_Library.CSI_Library.isServer = True

        'LoadTimesnList() 'For Testing Only
        Dim DayName As String = Convert.ToString(Now.DayOfWeek)

        ' Dim autoReport As Boolean = getAutoReportOrNot()
        Try
            'CSI_Library.CSI_Library.RestartTimeOfReporting = "" This is used to set the restart time of the library 
            'If DateTime.Now.ToString("HH:mm") <= CSI_Library.CSI_Library.RestartTimeOfReporting.ToString("HH:mm") And DateTime.Now.AddMinutes(2).ToString("HH:mm") < CSI_Library.CSI_Library.RestartTimeOfReporting.ToString("HH:mm") Then
            'Do reporting and Execute Database Query
            Dim Dataset_AutoReport As DataSet = New DataSet()
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim adap As New MySqlDataAdapter()
            Dim listOfMachine As String()

            conn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString

            Try
                conn.Open()
                If (DayName <> Convert.ToString(Convert.ToInt32(System.DateTime.Now.DayOfWeek))) Then
                    resetDoneReport()
                End If

                Dim dayOfNow As DateTime = Now()
                DayName = Convert.ToString(dayOfNow.DayOfWeek)
                DayName = Convert.ToString(dayOfNow.DayOfWeek)

                Dim DayName1 As String = dayOfNow.DayOfWeek.ToString()
                Dim timeNow As String = dayOfNow.ToString("HH:mm")

                Dim QryStr As String = String.Format("SELECT * FROM csi_auth.{0} where (Day_ like '%" + DateTime.Now.DayOfWeek.ToString() + "%' or Day_ like '%everyday%' ) and Time_ like '" + timeNow + "' and done <> '" + DayName + "' ", "Auto_Report_config")
                'Query that generate report if we missed it today  :::: Dim QryStr As String = String.Format("SELECT * FROM csi_auth.{0} where (Day_ like '%" + DateTime.Now.DayOfWeek.ToString() + "%' or Day_ like '%everyday%' ) and  done <> '" + DayName + "' ", "Auto_Report_config")

                Dim fileToSend As String

                adap = New MySqlDataAdapter(QryStr, conn)
                adap.Fill(Dataset_AutoReport, "tableDGV")

                If Dataset_AutoReport.Tables("tableDGV").Rows.Count > 0 Then
                    KeepReportingON = True
                    For Each row As DataRow In Dataset_AutoReport.Tables("tableDGV").Rows

                        Dim custommsg As String = Convert.ToString(row("CustomMsg"))

                        If custommsg.Length = 0 Then
                            If Convert.ToString(row("Report_Type")) = "Daily ( Today ) Availability - PDF" Then
                                ' Today
                                custommsg = "Your Today's CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                            ElseIf Convert.ToString(row("Report_Type")) = "Monthly Availability - PDF" Then
                                'Monthly Reporting
                                custommsg = "Your Monthly CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                            ElseIf Convert.ToString(row("Report_Type")) = "Weekly Availability - PDF" Then
                                ' Weekly Reporting
                                custommsg = "Your Weekly CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                            ElseIf Convert.ToString(row("Report_Type")) = "Daily ( Yesterday ) Availability - PDF" Then
                                ' Daily(Yesterday) Availability - PDF
                                custommsg = "Your Daily CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                            End If
                        Else
                            custommsg = Convert.ToString(row("CustomMsg"))
                        End If
                        Try

                            listOfMachine = Convert.ToString(row("MachineToReport")).Split(";")

                            Dim startdate As DateTime
                            Dim enddate As DateTime
                            Dim sendEmail = False

                            Dim SEvent(0 To listOfMachine.Count() - 1, 1) As String
                            For i = 0 To listOfMachine.Count() - 1

                                SEvent(i, 0) = listOfMachine(i)
                                SEvent(i, 1) = "SETUP"
                            Next
                            If Convert.ToString(row("Report_Type")) = "Daily ( Today ) Availability - PDF" Then
                                Dim todaystart As String = row("shift_starttime").ToString()
                                Dim Todayend As String = row("shift_endtime").ToString()
                                Dim todaystart_hour_min() As String = todaystart.Split(":")
                                Dim todayend_hour_min() As String = Todayend.Split(":")

                                startdate = New DateTime(dayOfNow.Year, dayOfNow.Month, dayOfNow.Day, todaystart_hour_min(0), todaystart_hour_min(1), 0)
                                enddate = New DateTime(dayOfNow.Year, dayOfNow.Month, dayOfNow.Day, todayend_hour_min(0), todayend_hour_min(1), 0)
                                sendEmail = True

                            ElseIf Convert.ToString(row("Report_Type")) = "Monthly Availability - PDF" And Convert.ToInt32(Convert.ToString(dayOfNow.Day)) = 1 Then
                                'run monthly for last month
                                enddate = dayOfNow
                                startdate = dayOfNow.AddMonths(-1)
                                startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                                enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                                sendEmail = True

                            ElseIf (Convert.ToString(row("Report_Type")) = "Weekly Availability - PDF") Then

                                ' run weekly report for last week

                                Dim days As Integer = 7
                                Try
                                    If Not (row("dayback") Is Nothing) Then
                                        days = Integer.Parse(Convert.ToString(row("dayback")))
                                    End If
                                Catch ex As Exception

                                End Try
                                enddate = dayOfNow.AddDays(-1)
                                startdate = dayOfNow.AddDays(-(days - 1))
                                startdate = dayOfNow.AddDays(-days)
                                startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                                enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                                sendEmail = True

                            ElseIf (Convert.ToString(row("Report_Type")) = "Daily ( Yesterday ) Availability - PDF") Then
                                'run daily for last day

                                enddate = dayOfNow.AddDays(-1)
                                startdate = dayOfNow.AddDays(-1)
                                startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                                enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                                sendEmail = True
                            Else
                                CSI_Lib.LogServiceError(DateTime.Now.ToString() + "invalid report type:" + row("Report_Type").ToString() + "TASK:" + row("Task_name").ToString(), 1)
                            End If
                            If sendEmail Then

                                '  Commented by Drausio in 2019-06-28
                                '
                                'fileToSend = CSI_Lib.generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, row("short_FileName").ToString(), Convert.ToString(row("Task_name")))

                                'Try
                                '    If Not (row("MailTo") Is Nothing) Then
                                '        CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                                '    End If
                                'Catch ex As Exception
                                '    CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                                'End Try
                                'updateDoneReport(row("Task_name"))
                            End If

                        Catch ex As Exception
                            CSI_Lib.LogServiceError("Error generating autoreports, MSG:" & ex.ToString(), 1)
                            If conn.State = ConnectionState.Open Then conn.Close()

                        End Try
                    Next
                    'Remove the First Element From the List
                    ListOfReportingTime.RemoveAt(0)
                Else
                    KeepReportingON = False
                End If
            Catch ex As Exception
                If conn.State = ConnectionState.Open Then conn.Close()
                CSI_Lib.LogServerError("Error in database in GenerateReports() on ServiceLibrary.vb " & ex.Message & " Stacktrace : " & ex.StackTrace(), 1)
            Finally
                conn.Close()
            End Try
        Catch ex As Exception
            CSI_Lib.LogServiceError("Error in GenerateReports():" & ex.ToString() + vbCrLf + "__>" + ex.StackTrace, 1)
        End Try
    End Sub


    'Public Function generateReport(machineList As String(), TypeDeRapport As String, ReportStartDate As DateTime, ReportEndDate As DateTime, outputreport As String, StatusName As String(,), isSetup As Boolean, ShortFileNameChecked As String, Optional taskname As String = "default") As String


    '    Dim completePath As String = ""
    '    Try
    '        LicenseAffectation()
    '        StatusNameArray = StatusName
    '        OutputReportPath = outputreport

    '        Dim applicationPath As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
    '        Dim path As String = getRootPath()

    '        If Not Directory.Exists(path & "\reports_templates") Then
    '            IO.File.WriteAllBytes(path & "\reports_templates.zip", My.Resources.reports_templates)
    '            ') 'My.Resources.reports_templates

    '            ZipFile.ExtractToDirectory(path & "\reports_templates.zip",
    '                                  path)
    '        End If
    '        If (TypeDeRapport.Contains("Today")) Then
    '            TypeDePeriode = "y"
    '        ElseIf (TypeDeRapport.Contains("Weekly")) Then
    '            'TypeDePeriode = "ww"
    '            TypeDePeriode = "ww"
    '        ElseIf (TypeDeRapport.Contains("Monthly")) Then
    '            TypeDePeriode = "m"
    '        ElseIf (TypeDeRapport.Contains("Yesterday")) Then
    '            TypeDePeriode = "y"
    '        End If

    '        Dim ReportViewer1 As ReportViewer = New ReportViewer()
    '        generateSqlQuery(machineList)
    '        Dim Paramet(2) As ReportParameter

    '        ReportViewer1.ProcessingMode = ProcessingMode.Local
    '        If (TypeDeRapport.Contains("Today")) Then

    '            If isSetup Then
    '                ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\mainDaily.rdlc"
    '            Else
    '                ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\EventMainDaily.rdlc"
    '            End If
    '            ReportViewer1.LocalReport.Refresh()
    '            'Dim ParametersD(1) As ReportParameter
    '            TypeDePeriode = "y"
    '            DateAffectation(ReportStartDate, ReportEndDate)

    '            Paramet(0) = New ReportParameter("reportType", TypeDePeriode)

    '            'SHOULD USE THIS
    '            Paramet(1) = New ReportParameter("startdate", ReportStartDate)
    '            Paramet(2) = New ReportParameter("enddate", ReportEndDate)

    '            ReportViewer1.LocalReport.SetParameters(Paramet)

    '            Call ReloadReport(ReportViewer1)
    '            ReportViewer1.RefreshReport()
    '            completePath = saveReport(ReportViewer1, "Today", taskname, ShortFileNameChecked)
    '        ElseIf (TypeDeRapport.Contains("Weekly")) Then
    '            'TypeDePeriode = "ww"
    '            TypeDePeriode = "ww"
    '            If isSetup Then
    '                ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\mainWeekly.rdlc"
    '            Else
    '                ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\EventMainReport.rdlc"
    '            End If

    '            ReportViewer1.LocalReport.Refresh()

    '            DateAffectation(ReportStartDate, ReportEndDate)

    '            Paramet(0) = New ReportParameter("reportType", TypeDePeriode)
    '            Paramet(1) = New ReportParameter("startdate", ReportStartDate)
    '            Paramet(2) = New ReportParameter("enddate", ReportEndDate)

    '            ReportViewer1.LocalReport.SetParameters(Paramet)
    '            Call ReloadReport(ReportViewer1)
    '            ReportViewer1.RefreshReport()
    '            completePath = saveReport(ReportViewer1, "weekly", taskname, ShortFileNameChecked)
    '        ElseIf (TypeDeRapport.Contains("Monthly")) Then
    '            TypeDePeriode = "m"
    '            If isSetup Then
    '                ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\mainMonthly.rdlc"
    '            Else
    '                ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\EventMainMonthly.rdlc"
    '            End If

    '            ReportViewer1.LocalReport.Refresh()

    '            DateAffectation(ReportStartDate, ReportEndDate)

    '            Paramet(0) = New ReportParameter("reportType", TypeDePeriode)
    '            Paramet(1) = New ReportParameter("startdate", ReportStartDate)
    '            Paramet(2) = New ReportParameter("enddate", ReportEndDate)

    '            ReportViewer1.LocalReport.SetParameters(Paramet)
    '            Call ReloadReport(ReportViewer1)
    '            ReportViewer1.RefreshReport()
    '            completePath = saveReport(ReportViewer1, "monthly", taskname, ShortFileNameChecked)
    '        ElseIf (TypeDeRapport.Contains("Yesterday")) Then
    '            'ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\mainDaily.rdlc"

    '            If isSetup Then
    '                ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\mainDaily.rdlc"
    '            Else
    '                ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\EventMainDaily.rdlc"
    '            End If
    '            ReportViewer1.LocalReport.Refresh()
    '            'Dim ParametersD(1) As ReportParameter
    '            TypeDePeriode = "y"
    '            DateAffectation(ReportStartDate, ReportEndDate)

    '            Paramet(0) = New ReportParameter("reportType", TypeDePeriode)

    '            'SHOULD USE THIS
    '            Paramet(1) = New ReportParameter("startdate", ReportStartDate)
    '            Paramet(2) = New ReportParameter("enddate", ReportEndDate)

    '            ReportViewer1.LocalReport.SetParameters(Paramet)
    '            Call ReloadReport(ReportViewer1)
    '            ReportViewer1.RefreshReport()
    '            completePath = saveReport(ReportViewer1, "Yesterday", taskname, ShortFileNameChecked)
    '        End If

    '        Dim repReport As DirectoryInfo = New DirectoryInfo(path & "\reports_templates")

    '        File.Delete(path & "\reports_templates.zip")


    '    Catch ex As Exception
    '        LogServiceError("Error while generating a report : " + ex.Message + vbCrLf + "___>" + ex.StackTrace, 1)
    '        'MessageBox.Show("Error while generating a report : " + ex.Message.ToString() + vbCrLf + "___>" + ex.StackTrace.ToString())
    '        If ex.Message.Contains("ReportViewer") Then WriteWarningReportViewer()
    '    End Try

    '    Return completePath
    'End Function

    'Public Sub LicenseAffectation()

    '    If isLicenseAffect = False Then
    '        license = CheckLic(2)
    '        isLicenseAffect = True
    '    End If

    'End Sub
    'Public Function CheckLic() As String()
    '    Dim resinfos As String()
    '    ReDim resinfos(2)
    '    resinfos(0) = "NOK"
    '    resinfos(1) = ""

    '    Dim rootpath As String
    '    If isServer Then
    '        rootpath = CSI_Library.CSI_Library.serverRootPath
    '    Else
    '        rootpath = CSI_Library.CSI_Library.ClientRootPath
    '    End If
    '    'Dim directory As String = getRootPath(rootpath)

    '    Try

    '        rootpath = rootpath & "\wincsi.dl1"

    '        If File.Exists(rootpath) Then
    '            Dim encrypted_info_str As String = File.ReadAllText(rootpath)
    '            Dim decrypted_info_str = CSI_Lib.AES_Decrypt(encrypted_info_str, "license")
    '            Dim infos As List(Of String) = New List(Of String)
    '            If Not IsNothing(decrypted_info_str) Then
    '                While (decrypted_info_str.IndexOf(":") > 0 And decrypted_info_str.IndexOf(";") > 0)
    '                    infos.Add(decrypted_info_str.Substring(decrypted_info_str.IndexOf(":") + 1, decrypted_info_str.IndexOf(";") - decrypted_info_str.IndexOf(":") - 1))
    '                    decrypted_info_str = decrypted_info_str.Substring(decrypted_info_str.IndexOf(";") + 1, decrypted_info_str.Length - decrypted_info_str.IndexOf(";") - 1)
    '                End While

    '                If (infos(0).Equals(CSI_Lib.GetCPUSerialNumber()) And infos(1).Equals(CSI_Lib.GetDriveSerialNumber("C:"))) Then
    '                    Dim expdate As DateTime = New DateTime
    '                    If (DateTime.TryParse(infos(2), expdate)) Then  'DateTime.FromBinary(infos(2))
    '                        resinfos(1) = expdate.ToString()
    '                        If expdate >= DateTime.Now Then
    '                            resinfos(0) = "ok" 'Welcome.CSIF_version = 2
    '                            resinfos(2) = infos(3)
    '                        Else
    '                            resinfos(0) = "EXP"
    '                        End If
    '                    Else
    '                        'Unable to parse date
    '                    End If
    '                End If
    '            Else
    '                CSI_Lib.LogServerError("The license you provided is invalid, if you think it is an error please contact CSIFlex Support.", 1)
    '                File.Delete(rootpath)
    '                'Environment.Exit(0)
    '            End If


    '            'Return "ok"
    '        Else
    '            'Return "NOK"
    '        End If
    '    Catch ex As Exception
    '        'Return "NOK"
    '    End Try
    '    Return resinfos
    'End Function


    Private Function getAutoReportOrNot() As Boolean

        Dim reportOrNot As Boolean = False
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
            cntsql.Close()
        Catch ex As Exception
            MessageBox.Show("Error in loading Auto Reporting from Database : " & ex.Message())
        End Try

        Return reportOrNot

    End Function


    Private Shared Sub updateDoneReport(taskname As String)
        Dim sql As String = ("update  CSI_auth.Auto_Report_config set done = '" + Convert.ToString(Now.DayOfWeek) + "' where Task_name = '" + taskname + "'")
        Dim connection As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)
        Try
            connection.Open()
            mysqlcomm.ExecuteNonQuery()
            connection.Close()
        Catch ex As Exception
            CSI_Lib.LogServiceError("Error trying to update auto_report_config table when report is done :" & ex.ToString(), 1)
            If connection.State = ConnectionState.Open Then connection.Close()
        End Try
    End Sub

    Public Shared Sub resetDoneReport()
        Dim sql As String = ("update  CSI_auth.Auto_Report_config set done = 10")
        Dim connection As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)

        Try
            connection.Open()
            mysqlcomm.ExecuteNonQuery()
            connection.Close()
        Catch ex As Exception
            CSI_Lib.LogServiceError(ex.ToString(), 1)
            If connection.State = ConnectionState.Open Then connection.Close()
        End Try
    End Sub

    Public Shared Function check_tableExists(db_name As String, tbl_name As String) As Boolean
        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
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
End Class
'Public Class ReportingData
'    Public Task_name As String
'    Public TimeOfReport As String
'    Public MailTo As String
'    Public isDone As String
'    Public isShortFileName As String

'    Public Sub New(task_name As String, timeOfReport As String, mailTo As String, isdone As String, isshortfilename As String)
'        Try
'            Me.Task_name = task_name
'            Me.TimeOfReport = timeOfReport
'            Me.MailTo = mailTo
'            Me.isDone = isdone
'            Me.isShortFileName = isshortfilename
'        Catch ex As Exception
'        End Try
'    End Sub
'End Class

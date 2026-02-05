Imports System.Data.SQLite
Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Management
Imports System.Net.Mail
Imports System.Reflection
Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient

Public Class Report_Generator
    'Public Shared CSI_Lib As New CSI_Library.CSI_Library
    'Public Shared MySqlConnectionString As String = "server=192.168.1.131;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;SslMode=none;" 'SOMR
    Public Shared MySqlConnectionString As String = "server=localhost;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;SslMode=none;" 'SOMR
    Public Shared serverRootPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Server"  '"C:\ProgramData\CSI Flex Server"
    Public Shared ClientRootPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Client"  '"C:\ProgramData\CSI Flex Client"
    Public Shared MySqlServerBaseString As String = "user=client;password=csiflex123;port=3306;"

    Public Shared sqlitedbpath As String = "URI=file:" + ClientRootPath + "\sys\csisqlite.db3" '"URI=file:sys/csisqlite.db3"
    Public Shared isServer As Boolean = False
    Public Shared OutputReportPath As String
    Private Shared Qry_Tbl_MachineName As String
    Private Shared Qry_Tbl_RenameMachine As String
    Private Shared TypeDePeriode As String
    Private Shared reportstartdate As DateTime
    Private Shared reportenddate As DateTime
    Private Shared statusName_ As String = "SETUP"
    Private Shared StatusNameArray As String(,)
    Private Shared machineIndex As Integer

    Public Shared license As Integer
    Public Shared isLicenseAffect As Boolean = False
    Public Shared thread_Start_AutoReporting As Thread

    Public Shared Sub StartThread()
        thread_Start_AutoReporting = New Thread(AddressOf Report_Generator.thread_AutoReporting) With {
            .Name = "Start Auto Reporting",
            .IsBackground = True
        }
        thread_Start_AutoReporting.Start()
    End Sub
    Public Shared Sub StopThread()
        thread_Start_AutoReporting.Abort()
    End Sub
    Public Shared Sub GenerateReports()

        Dim DayName As String = Convert.ToString(Now.DayOfWeek)
        'Dim autoReport As Boolean = getAutoReportOrNot()

        Try
            If (check_tableExists("csi_auth", "Auto_Report_config")) Then 'And autoReport = True

                'csi_lib.Log_server_event(Date.Now & ", " & "Autoreporting : generating reports")

                Dim Dataset_AutoReport As DataSet = New DataSet()
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim adap As New MySqlDataAdapter()
                Dim listOfMachine As String()

                conn.ConnectionString = MySqlConnectionString  'CSI_Library.CSI_Library.MySqlConnectionString
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
                            Dim todaystart As String = row("shift_starttime").ToString()
                            Dim Todayend As String = row("shift_endtime").ToString()
                            Dim todaystart_split_date_time() As String = todaystart.Split(" ")
                            Dim todaystart_date_split() As String = todaystart_split_date_time(0).Split("-")
                            Dim todaystart_time_split() As String = todaystart_split_date_time(1).Split(":")
                            Dim todayend_split_date_time() As String = Todayend.Split(" ")
                            Dim todayend_date_split() As String = todayend_split_date_time(0).Split("-")
                            Dim todayend_time_split() As String = todayend_split_date_time(1).Split(":")
                            Dim startdate As DateTime
                            Dim enddate As DateTime
                            startdate = New DateTime(todaystart_date_split(2), todaystart_date_split(1), todaystart_date_split(0), todaystart_time_split(0), todaystart_time_split(1), 0)
                            enddate = New DateTime(todayend_date_split(2), todayend_date_split(1), todayend_date_split(0), todayend_time_split(0), todayend_time_split(1), 0)
                            fileToSend = generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, Convert.ToString(row("Task_name")))

                            Try
                                If Not (row("MailTo") Is Nothing) Then
                                    sendReportByMail(row("MailTo"), fileToSend, custommsg)
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

                            fileToSend = generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, Convert.ToString(row("Task_name")))

                            Try
                                If Not (row("MailTo") Is Nothing) Then
                                    sendReportByMail(row("MailTo"), fileToSend, custommsg)
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

                            fileToSend = generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, Convert.ToString(row("Task_name")))

                            Try
                                If Not (row("MailTo") Is Nothing) Then
                                    sendReportByMail(row("MailTo"), fileToSend, custommsg)
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

                            fileToSend = generateReport(listOfMachine, Convert.ToString(row("Report_Type")), startdate, enddate, Convert.ToString(row("Output_Folder")), SEvent, True, Convert.ToString(row("Task_name")))

                            Try
                                If Not (row("MailTo") Is Nothing) Then
                                    sendReportByMail(row("MailTo"), fileToSend, custommsg)
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
    End Sub
    Private Shared Function LoadEmailConfig() As DataTable
        Dim results As New DataTable

        Dim cnt As MySqlConnection = New MySqlConnection(MySqlConnectionString)
        Try
            cnt.Open()

            Dim mysqlcmd As New MySqlCommand("Select senderemail, senderpwd, smtphost, smtpport, requirecred from CSI_auth.tbl_emailreports;", cnt)
            Dim mysqladapter As New MySqlDataAdapter(mysqlcmd)

            mysqladapter.Fill(results)

        Catch ex As Exception
            'LogServerError("Unable to load reports email configuration:" + ex.Message, 1)
            MessageBox.Show("Unable to load reports email configuration:" + ex.Message)
        Finally
            cnt.Close()
        End Try

        Return results

    End Function

    Public Shared Sub sendReportByMail(mailadresses As String, filePath As String, custommsg As String)

        Dim emailconfig As New DataTable
        emailconfig = LoadEmailConfig()

        If (emailconfig.Rows.Count > 0) Then

            Try
                Dim smtpport As Integer
                If (Integer.TryParse(emailconfig.Rows(0)("smtpport").ToString(), smtpport)) Then

                    Dim mail As MailMessage = New MailMessage()
                    Dim client As SmtpClient = New SmtpClient(emailconfig.Rows(0)("smtphost").ToString(), smtpport)
                    mail.From = New MailAddress(emailconfig.Rows(0)("senderemail").ToString())
                    client.EnableSsl = True
                    'client.Timeout = 10000
                    client.DeliveryMethod = SmtpDeliveryMethod.Network
                    client.UseDefaultCredentials = False
                    If (emailconfig.Rows(0)("requirecred")) Then
                        client.Credentials = New System.Net.NetworkCredential(emailconfig.Rows(0)("senderemail").ToString(), emailconfig.Rows(0)("senderpwd").ToString())
                    End If
                    mail.Subject = "CSI Flex Server Report"
                    'mail.Body = custommsg
                    If (custommsg.Length > 0) Then
                        mail.Body = custommsg
                    End If


                    For Each email As String In mailsToList(mailadresses)
                        If (email.Length > 0) Then
                            mail.To.Add(New MailAddress(email))
                        End If
                    Next

                    mail.Attachments.Add(New Attachment(filePath))


                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure


                    client.Send(mail)
                End If
            Catch ex As Exception
                'LogServiceError("email error,CONFIG:MailAdresses:" + mailadresses.ToString() + ",port:" + emailconfig.Rows(0)("smtpport").ToString() + ",server:" + emailconfig.Rows(0)("smtphost").ToString() + ",requirecred:" + emailconfig.Rows(0)("requirecred").ToString() + ",email:" + emailconfig.Rows(0)("senderemail").ToString() + ",pwd:" + emailconfig.Rows(0)("senderpwd").ToString() + ",MSG:" + ex.Message, 1)
                MessageBox.Show("email error,CONFIG:MailAdresses:" + mailadresses.ToString() + ",port:" + emailconfig.Rows(0)("smtpport").ToString() + ",server:" + emailconfig.Rows(0)("smtphost").ToString() + ",requirecred:" + emailconfig.Rows(0)("requirecred").ToString() + ",email:" + emailconfig.Rows(0)("senderemail").ToString() + ",pwd:" + emailconfig.Rows(0)("senderpwd").ToString() + ",MSG:" + ex.Message)
            End Try

        Else
            'LogServiceError("email server not configured for autoreports", 1)
            MessageBox.Show("email server not configured for autoreports")
        End If
    End Sub
    Private Shared Function mailsToList(mailadresses As String) As String()

        Return mailadresses.Split(";"c)

    End Function
    Public Shared Sub thread_AutoReporting()
        StartAutoreporting(True)
    End Sub
    Public Shared  Sub LicenseAffectation()

        If isLicenseAffect = False Then
            license = CheckLic(2)
            isLicenseAffect = True
        End If

    End Sub
    Public Shared Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function GetCPUSerialNumber() As String

        'http://msdn.microsoft.com/en-us/library/windows/desktop/aa394373(v=vs.85).aspx

        Dim cpuSerial As String = String.Empty
        'Dim driveFixed As String = System.IO.Path.GetPathRoot()
        'driveFixed = Replace(driveFixed, "\", String.Empty)

        Using querySearch As New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor")

            Using queryCollection As ManagementObjectCollection = querySearch.Get()

                For Each moItem As ManagementObject In queryCollection

                    cpuSerial = CStr(moItem.Item("Manufacturer"))
                    cpuSerial = cpuSerial & " " & CStr(moItem.Item("ProcessorID"))

                    Exit For
                Next
            End Using
        End Using
        Return cpuSerial
    End Function
    Public Shared Function GetDriveSerialNumber(ByVal drive As String) As String

        Dim driveSerial As String = String.Empty
        Dim driveFixed As String = System.IO.Path.GetPathRoot(drive)
        driveFixed = driveFixed.Replace("\", String.Empty)

        Using querySearch As New System.Management.ManagementObjectSearcher("SELECT VolumeSerialNumber FROM Win32_LogicalDisk Where Name = '" & driveFixed & "'")

            Using queryCollection As ManagementObjectCollection = querySearch.Get()

                Dim moItem As ManagementObject

                For Each moItem In queryCollection

                    driveSerial = CStr(moItem.Item("VolumeSerialNumber"))

                    Exit For
                Next
            End Using
        End Using
        Return driveSerial
    End Function
    Public Shared Function CheckLic() As String()
        Dim resinfos As String()
        ReDim resinfos(2)
        resinfos(0) = "NOK"
        resinfos(1) = ""

        Dim rootpath As String
        If isServer Then
            rootpath = serverRootPath
        Else
            rootpath = ClientRootPath
        End If
        'Dim directory As String = getRootPath(rootpath)

        Try

            rootpath = rootpath & "\wincsi.dl1"

            If File.Exists(rootpath) Then
                Dim encrypted_info_str As String = File.ReadAllText(rootpath)
                Dim decrypted_info_str = AES_Decrypt(encrypted_info_str, "license")
                Dim infos As List(Of String) = New List(Of String)
                If Not IsNothing(decrypted_info_str) Then
                    While (decrypted_info_str.IndexOf(":") > 0 And decrypted_info_str.IndexOf(";") > 0)
                        infos.Add(decrypted_info_str.Substring(decrypted_info_str.IndexOf(":") + 1, decrypted_info_str.IndexOf(";") - decrypted_info_str.IndexOf(":") - 1))
                        decrypted_info_str = decrypted_info_str.Substring(decrypted_info_str.IndexOf(";") + 1, decrypted_info_str.Length - decrypted_info_str.IndexOf(";") - 1)
                    End While

                    If (infos(0).Equals(GetCPUSerialNumber()) And infos(1).Equals(GetDriveSerialNumber("C:"))) Then
                        Dim expdate As DateTime = New DateTime
                        If (DateTime.TryParse(infos(2), expdate)) Then  'DateTime.FromBinary(infos(2))
                            resinfos(1) = expdate.ToString()
                            If expdate >= DateTime.Now Then
                                resinfos(0) = "ok" 'Welcome.CSIF_version = 2
                                resinfos(2) = infos(3)
                            Else
                                resinfos(0) = "EXP"
                            End If
                        Else
                            'Unable to parse date
                        End If
                    End If
                Else
                    MessageBox.Show("The license you provided is invalid, if you think it is an error please contact CSIFlex Support.", "Invalid License", MessageBoxButtons.OK)
                    File.Delete(rootpath)
                    'Environment.Exit(0)
                End If


                'Return "ok"
            Else
                'Return "NOK"
            End If
        Catch ex As Exception
            'Return "NOK"
        End Try
        Return resinfos
    End Function
    Public Shared Function getRootPath() As String
        Dim directory As String '= rootPath
        'LicenseAffectation()
        'If license <> 1 And license <> 2 And license <> 3 Then
        '    ' directory = path
        '    directory = serverRootPath
        'End If
        isServer = True 'Make Application As Server NEW CODE ADDED

        If Not isServer Then
            directory = ClientRootPath
        Else
            directory = serverRootPath
        End If

        Return directory
    End Function
    Public Shared Function RenameMachine(machine As String) As String
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
    Public Shared Sub generateSqlQuery(machineList As String())

        Dim ListOfMachineSelected As String() = machineList
        Dim i As Integer = 0

        Qry_Tbl_MachineName = ""
        Qry_Tbl_RenameMachine = "SELECT original_name AS MchName, '" + StatusNameArray(0, 1) + "' as StatusName FROM tbl_renamemachines where"

        For Each machine In ListOfMachineSelected
            If (machine <> "") Then
                machine = RenameMachine(machine)

                Qry_Tbl_MachineName += "SELECT 'tbl_" + machine + "' AS MchName, status FROM tbl_" + machine + " "
                Qry_Tbl_RenameMachine += " table_name = '" + machine + "'"

                If Not i = (ListOfMachineSelected.Length - 1) Then
                    Qry_Tbl_MachineName += " union "
                    Qry_Tbl_RenameMachine += " or"
                End If
                i += 1
            End If
        Next
        '   If Qry_Tbl_RenameMachine.EndsWith(" or") Then Qry_Tbl_RenameMachine.Remove(Qry_Tbl_RenameMachine.Length - 2, 2)
        Qry_Tbl_RenameMachine += " group by MchName"

    End Sub
    Public Shared Sub DateAffectation(ReportStartDate_ As DateTime, ReportEndDate_ As DateTime)
        'dateReporting = ReportDate
        reportstartdate = ReportStartDate_
        reportenddate = ReportEndDate_
    End Sub
    Private Shared Sub ReloadReport(viewer As ReportViewer)
        If (viewer.LocalReport.DataSources.Count > 0) Then
            viewer.LocalReport.DataSources.RemoveAt(0)
        End If

        viewer.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", setMachineName()))
        AddHandler viewer.LocalReport.SubreportProcessing, AddressOf localReport_SubreportProcessing

    End Sub
    Public Shared Function RealNameMachine(machine As String) As String
        Dim res As String = machine

        For i = 32 To 47
            res = res.Replace("_c" & i & "_", Chr(i))
        Next

        For i = 58 To 64
            res = res.Replace("_c" & i & "_", Chr(i))
        Next

        For i = 91 To 96
            If i <> 95 Then
                res = res.Replace("_c" & i & "_", Chr(i))
            End If
        Next

        For i = 123 To 126
            res = res.Replace("_c" & i & "_", Chr(i))
        Next

        Return res
    End Function
    Private Shared Function setMachineData(OnePeriod As Boolean, PeriodType As String, tblmachineName As String) As DataTable
        'Dim BetweenStr
        Dim tableName As String
        Dim originalMachine As String = RealNameMachine(tblmachineName)
        Dim StatusFont As Integer = 7
        originalMachine = originalMachine.Substring(4, originalMachine.Length - 4)

        Dim startDate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
        Dim endDate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

        If statusName_.Length > 20 Then
            StatusFont = 4
        ElseIf statusName_.Length > 7 Then
            StatusFont = 5
        End If

        If (OnePeriod = True) Then
            tableName = "Tbl_DataMachine"
        Else
            If PeriodType = "y" Then
                startDate = reportstartdate.AddDays(-4).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
            ElseIf (PeriodType = "ww") Then
                startDate = reportstartdate.AddDays(-7 * 3).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
            ElseIf (PeriodType = "m") Then
                startDate = reportstartdate.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
            End If

            tableName = "Tbl_DataMachine4wk"
        End If
        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

                Dim adap As New SQLiteDataAdapter()

                Dim query As String = "SELECT shift, mchName, Sum(detailCycleTime) AS Totalcycletime, Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther,'" + statusName_ + "' as StatusName, '7pt' as StatusFont " +
           " FROM (" +
           " SELECT '" + originalMachine + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, " +
           " CASE WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime ELSE 0 END as COFF," +
           " CASE WHEN (status = '_CON' or status = 'CYCLE ON' ) THEN cycletime ELSE 0 END as CON, " +
           " CASE WHEN status = '_SETUP' or status = 'SETUP' THEN cycletime ELSE 0 END as SETUP, " +
           " CASE WHEN (Status<>'_CON' and status<>'CYCLE ON') " +
           " and (Status<>'_COFF' and Status<>'CYCLE OFF')  " +
           " and (Status<>'_SETUP' and  Status<>'SETUP')" +
           " THEN cycletime ELSE 0 END as OTHER, shift" +
           " from " + tblmachineName +
           " where date_ between '" + startDate + "' and '" + endDate + "') as setMachineData" +
           " group by shift"


                adap = New SQLiteDataAdapter(query, sqlConn)
                ' STRFTIME pas sur
                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)


                If (OnePeriod = True) Then
                    Return ds.Tbl_DataMachine
                Else
                    Return ds.Tbl_DataMachine4wk
                End If

            End Using
        Else
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim db_authPath As String = Nothing
            Dim directory As String = getRootPath()
            If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                    db_authPath = reader.ReadLine()
                End Using
            End If
            Dim connectionString As String
            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

            If license = 0 Then
                connectionString = "DATABASE=csi_database;" + MySqlConnectionString
            End If


            Dim query As String = "SELECT shift, mchName, Sum(detailCycleTime) AS Totalcycletime, Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther,'" + statusName_ + "' as StatusName, '7pt' as StatusFont " +
                         " FROM (" +
                         " SELECT '" + originalMachine + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, " +
                         " CASE WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime ELSE 0 END as COFF," +
                         " CASE WHEN (status = '_CON' or status = 'CYCLE ON' ) THEN cycletime ELSE 0 END as CON, " +
                         " CASE WHEN status = '_SETUP' or status = 'SETUP' THEN cycletime ELSE 0 END as SETUP, " +
                         " CASE WHEN (Status<>'_CON' and status<>'CYCLE ON') " +
                         " and (Status<>'_COFF' and Status<>'CYCLE OFF')  " +
                         " and (Status<>'_SETUP' and  Status<>'SETUP')" +
                         " THEN cycletime ELSE 0 END as OTHER, shift" +
                         " from " + tblmachineName + " where "
            '"date_ between '" + startDate + "' and '" + endDate + "') as setMachineData" +
            '" group by shift"

            'if ww then
            '((DATEPART(dw, date_created) + @@DATEFIRST) % 7) NOT IN (0, 1)
            'In mysql:
            'DAYOFWEEK(date_created) NOT IN (1,7)

            Dim daysdiff As Integer = DateDiff(DateInterval.Day, reportstartdate, reportenddate)
            If Not OnePeriod And (PeriodType = "ww") And (daysdiff < 6) Then
                query += " (DAYOFWEEK(date_) NOT IN (1,7)) and"
            End If
            query += " date_ between '" + startDate + "' and '" + endDate + "') as setMachineData" +
       " group by shift"

            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

                Dim adap As MySqlDataAdapter

                adap = New MySqlDataAdapter(query, sqlConn)

                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)


                If (OnePeriod = True) Then
                    Return ds.Tbl_DataMachine
                Else
                    Return ds.Tbl_DataMachine4wk
                End If

            End Using
        End If
    End Function
    Private Shared Function setShiftBarChart(tblmachineName As String) As DataTable


        Dim TableName As String = "TimeLine"
        'Dim choosenDate As String = dateReporting.ToString()"yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))
        Dim startdate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
        Dim enddate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

                Dim query As String = "SELECT CASE WHEN (Status='_CON' or Status='CYCLE ON') then 'CYCLE ON'" +
                " when (Status='_COFF' or Status='CYCLE OFF')  then 'CYCLE OFF'" +
                " when (Status='_SETUP' or  Status='SETUP') THEN 'SETUP' " +
                " ELSE 'OTHER' END as ReasonName," +
                " case when shift=1 and status = 'SETUP' then 1 " +
                " when shift=1 and status <> 'SETUP' THEN 2 " +
                " when shift=2 and status = 'SETUP'  then 3 " +
                " when shift=2 and status <> 'SETUP' THEN 4 " +
                " when shift=3 and status = 'SETUP' then 5 " +
                " when shift=3 and status <> 'SETUP' then 6 end as Priority," +
                " shift, " +
                " sum(cycletime) as cycletime " +
                " from " + tblmachineName +
                " where status not like '_PART%' and Date_ between '" + startdate + "' and '" + enddate + "'" +
                " GROUP by ReasonName, shift, Priority order by Priority"


                Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)

                'JULIANDAY(date('" + String.Format("{0:yyyy-MM-dd}", dateReporting.ToString()"yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))) + "')) - julianday(date([time_]))=0
                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, TableName)

                Return ds.TimeLine

            End Using

        Else
            Try
                Dim db_authPath As String = Nothing
                Dim directory As String = getRootPath()
                If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                    Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                        db_authPath = reader.ReadLine()
                    End Using
                End If
                Dim connectionString As String
                connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

                If license = 0 Then
                    connectionString = "DATABASE=csi_database;" + MySqlConnectionString
                End If

                Dim query As String = "SELECT CASE WHEN (Status='_CON' or Status='CYCLE ON') then 'CYCLE ON'" +
                        " when (Status='_COFF' or Status='CYCLE OFF')  then 'CYCLE OFF'" +
                        " when (Status='_SETUP' or  Status='SETUP') THEN 'SETUP' " +
                        " ELSE 'OTHER' END as ReasonName," +
                        " case when shift=1 and status = 'SETUP' then 1 " +
                        " when shift=1 and status <> 'SETUP' THEN 2 " +
                        " when shift=2 and status = 'SETUP'  then 3 " +
                        " when shift=2 and status <> 'SETUP' THEN 4 " +
                        " when shift=3 and status = 'SETUP' then 5 " +
                        " when shift=3 and status <> 'SETUP' then 6 end as Priority," +
                        " shift, " +
                        " sum(cycletime) as cycletime " +
                        " from " + tblmachineName +
                        " where status not like '_PART%' and Date_ between '" + startdate + "' and '" + enddate + "'" +
                        " GROUP by ReasonName, shift, Priority order by Priority"

                Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

                    Dim adap As MySqlDataAdapter = New MySqlDataAdapter(query, sqlConn)

                    Dim ds As DatasetReporting2 = New DatasetReporting2()

                    Dim returnvalue As Integer = adap.Fill(ds, TableName)

                    Return ds.TimeLine

                End Using

            Catch ex As Exception
                MessageBox.Show("Unable to generate bar chart")
            End Try
        End If

    End Function
    Public Shared Function set4Reason(OnePeriod As Boolean, PeriodType As String, tblmachineName As String) As DataTable

        Dim tableName As String

        Dim startDate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
        Dim endDate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

        Dim originalMachine As String = RealNameMachine(tblmachineName)
        originalMachine = originalMachine.Substring(4, originalMachine.Length - 4)

        If (OnePeriod = True) Then
            tableName = "Tbl_Top4Reason"

        Else

            If PeriodType = "y" Then
                startDate = reportstartdate.AddDays(-4).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
            ElseIf (PeriodType = "ww") Then
                startDate = reportstartdate.AddDays(-7 * 3).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
            ElseIf (PeriodType = "m") Then
                startDate = reportstartdate.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
            End If

            tableName = "Tbl_Top4Reason4wk"

        End If

        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)


                Dim query As String = "select '" + originalMachine + "' as  mchName," +
                            " status as ReasonName, " +
                            " sum(cycletime) as CycleTime" +
                            " from " + tblmachineName +
                            " where ( Status<>'_CON' and Status<>'CYCLE ON'" +
                            " and Status<>'_COFF' and Status<>'CYCLE OFF'" +
                            " and Status<>'_SETUP' and  Status<>'SETUP'" +
                            " and CycleTime >0" +
                            " and Date_ between '" + startDate + "' and '" + endDate + "')" +
                            " group by status" +
                            " order by sum(cycletime) desc limit 4"

                Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)

                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)

                If (OnePeriod = True) Then
                    Return ds.Tbl_Top4Reason
                Else
                    Return ds.Tbl_Top4Reason4wk
                End If
            End Using

        Else
            Dim db_authPath As String = Nothing
            Dim directory As String = getRootPath()
            If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                    db_authPath = reader.ReadLine()
                End Using
            End If
            Dim connectionString As String

            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

            If license = 0 Then
                connectionString = "DATABASE=csi_database;" + MySqlConnectionString
            End If

            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

                Dim query As String = "select '" + originalMachine + "' as  mchName," +
                            " status as ReasonName, " +
                            " sum(cycletime) as CycleTime" +
                            " from " + tblmachineName +
                            " where ( Status<>'_CON' and Status<>'CYCLE ON'" +
                            " and Status<>'_COFF' and Status<>'CYCLE OFF'" +
                            " and Status<>'_SETUP' and  Status<>'SETUP'" +
                            " and CycleTime >0" +
                            " and Date_ between '" + startDate + "' and '" + endDate + "')" +
                            " group by status" +
                            " order by sum(cycletime) desc limit 4"

                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(query, sqlConn)

                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)

                If (OnePeriod = True) Then
                    Return ds.Tbl_Top4Reason
                Else
                    Return ds.Tbl_Top4Reason4wk
                End If
            End Using

        End If

    End Function
    Private Shared Function setHistoryDaily(PeriodType As String, tblmachineName As String) As DataTable

        'Dim BetweenStr, 
        Dim tableName As String

        Dim startDate As String = reportstartdate.AddDays(-13).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
        Dim endDate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

        'BetweenStr = "0 and 13"


        tableName = "Tbl_History18Daily"
        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

                'If PeriodType.Equals("ww") Then
                '    PeriodType = "W"
                'End If
                'If PeriodType.Equals("y") Then
                '    PeriodType = "j"
                'End If

                Dim query As String = "SELECT  mchName, date(detailDate) as  WeekNumber,  Sum(detailCycleTime) AS Totalcycletime, Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther " +
                       " FROM (select '" + tblmachineName + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, " +
                       "       CASE WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime ELSE 0 END as COFF," +
                       "       CASE WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
                       "       CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP," +
                       "       CASE WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
                       "       and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
                       "       and (Status<>'_SETUP' and status<>'SETUP') " +
                       "       THEN cycletime ELSE 0 END as OTHER " +
                       "       from " + tblmachineName +
                       "       where  date_ between  '" + startDate + "' and  '" + endDate + "' ) as tbl  " +
                       " GROUP BY  mchName, strftime('%d', detailDate), detailDate" +
                       " order by detailDate"


                Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)

                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)

                Return ds.Tbl_History18Weekly

            End Using
        Else
            Dim db_authPath As String = Nothing
            Dim directory As String = getRootPath()
            If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                    db_authPath = reader.ReadLine()
                End Using
            End If
            Dim connectionString As String
            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

            If license = 0 Then
                connectionString = "DATABASE=csi_database;" + MySqlConnectionString
            End If

            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

                Dim query As String = "SELECT  mchName, date(detailDate) as  WeekNumber,  Sum(detailCycleTime) AS Totalcycletime, Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther " +
                        " FROM (select '" + tblmachineName + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, " +
                        "       CASE WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime ELSE 0 END as COFF," +
                        "       CASE WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
                        "       CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP," +
                        "       CASE WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
                        "       and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
                        "       and (Status<>'_SETUP' and status<>'SETUP') " +
                        "       THEN cycletime ELSE 0 END as OTHER " +
                        "       from " + tblmachineName +
                        "       where  date_ between  '" + startDate + "' and  '" + endDate + "' ) as tbl  " +
                        " GROUP BY  mchName, WeekNumber " +
                        " order by WeekNumber"

                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(query, sqlConn)

                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)

                Return ds.Tbl_History18Daily

            End Using
        End If

    End Function
    Private Shared Function setPartNo(OnePeriod As Boolean, PeriodType As String, tblmachineName As String) As DataTable
        '   Dim BetweenStr
        Dim tableName As String

        Dim startDate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
        Dim endDate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

        tableName = "tbl_partsNumber"
        If (OnePeriod = True) Then

            'BetweenStr = "0 and 0"

        Else

            'BetweenStr = "0 and 4"

            If PeriodType = "y" Then
                startDate = reportstartdate.AddDays(-4).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
            ElseIf (PeriodType = "ww") Then
                startDate = reportstartdate.AddDays(-7 * 3).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
            ElseIf (PeriodType = "m") Then
                startDate = reportstartdate.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
            End If


        End If

        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)


                Dim query As String = "select '" + tblmachineName + "' as  mchName,  Status as partName, Shift,date_" +
                      " from " + tblmachineName +
                      " where Status like '_Partno%' " +
                      " and CycleTime=0 " +
                      " and Date_ between '" + startDate + "' and '" + endDate + "'" +
                      " GROUP BY  Shift, partName, date_" +
                      " LIMIT 10"

                Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)

                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)

                If (OnePeriod = True) Then
                    Return ds.tbl_partsNumber
                Else
                    Return ds.tbl_partsNumber
                End If
            End Using
        Else
            Dim db_authPath As String = Nothing
            Dim directory As String = getRootPath()
            If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                    db_authPath = reader.ReadLine()
                End Using
            End If
            Dim connectionString As String
            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

            If license = 0 Then
                connectionString = "DATABASE=csi_database;" + MySqlConnectionString
            End If


            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)


                Dim query As String = "select '" + tblmachineName + "' as  mchName,  Status as partName, Shift,date_" +
                            " from " + tblmachineName +
                            " where Status like '_Partno%' " +
                            " and CycleTime=0 " +
                            " and Date_ between '" + startDate + "' and '" + endDate + "'" +
                            " GROUP BY  Shift, partName, date_" +
                            " LIMIT 10"

                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(query, sqlConn)

                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)

                If (OnePeriod = True) Then
                    Return ds.tbl_partsNumber
                Else
                    Return ds.tbl_partsNumber
                End If
            End Using
        End If

    End Function
    Private Shared Function setHistoryWeekly(tblmachineName As String) As DataTable
        Dim tableName As String

        Dim startDate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
        Dim endDate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

        startDate = reportstartdate.AddDays(-7 * 17).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))


        'BetweenStr = "0 and 17"
        tableName = "Tbl_History18Weekly"
        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)


                Dim query As String = "SELECT  mchName," +
                     " date(detailDate) as weeknumber," +
                     " Sum(detailCycleTime) AS Totalcycletime," +
                     " Sum(CON) as CycleOn," +
                     " Sum(COFF) as CycleOff," +
                     " Sum(SETUP) as SumSetup," +
                     " Sum(OTHER) as SumOther " +
                     " FROM" +
                     " (select '" + tblmachineName + "' as  mchName," +
                     " cycletime as detailCycleTime, " +
                     " date_ as detailDate, " +
                     " CASE  WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime  ELSE 0 END as COFF," +
                     " CASE  WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
                     " CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP, " +
                     " CASE " +
                     " WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
                     " and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
                     " and (Status<>'_SETUP' and status<>'SETUP') THEN cycletime " +
                     " ELSE 0 END as OTHER " +
                     " from " + tblmachineName +
                     " where date_ between '" + startDate + "' and '" + endDate + "'" +
                     " ) as tbl" +
                     " GROUP BY  strftime('%W' ,detailDate)  - case strftime('%w' ,detailDate) when 1 then 1 else 0 end, weeknumber  " +
                     " order by weeknumber"
                Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)


                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)

                Return ds.Tbl_History18Weekly

            End Using
        Else
            Dim db_authPath As String = Nothing
            Dim directory As String = getRootPath()
            If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                    db_authPath = reader.ReadLine()
                End Using
            End If
            Dim connectionString As String
            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

            If license = 0 Then
                connectionString = "DATABASE=csi_database;" + MySqlConnectionString
            End If

            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

                Dim query As String = "SELECT  mchName," +
                        " adddate(date(detailDate), INTERVAL 1-DAYOFWEEK(date(detailDate)) DAY) as WeekStart," +
                        " Sum(detailCycleTime) AS Totalcycletime," +
                        " Sum(CON) as CycleOn," +
                        " Sum(COFF) as CycleOff," +
                        " Sum(SETUP) as SumSetup," +
                        " Sum(OTHER) as SumOther " +
                        " FROM" +
                        " (select '" + tblmachineName + "' as  mchName," +
                        " cycletime as detailCycleTime, " +
                        " date_ as detailDate, " +
                        " CASE  WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime  ELSE 0 END as COFF," +
                        " CASE  WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
                        " CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP, " +
                        " CASE " +
                        " WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
                        " and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
                        " and (Status<>'_SETUP' and status<>'SETUP') THEN cycletime " +
                        " ELSE 0 END as OTHER " +
                        " from " + tblmachineName +
                        " where date_ between '" + startDate + "' and '" + endDate + "'" +
                        " ) as tbl" +
                        " GROUP BY WeekStart" +
                        " order by WeekStart"

                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(query, sqlConn)

                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)

                Return ds.Tbl_History18Weekly

            End Using
        End If

    End Function

    Private Shared Function setHistoryMonthly(tblmachineName As String) As DataTable
        Dim tableName As String

        Dim startDate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
        Dim endDate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

        startDate = reportstartdate.AddMonths(-17).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))


        'BetweenStr = "0 and 17"
        tableName = "Tbl_History18Monthly"
        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)


                Dim query As String = "SELECT  mchName," +
                     " date(detailDate) as weeknumber," +
                     " Sum(detailCycleTime) AS Totalcycletime," +
                     " Sum(CON) as CycleOn," +
                     " Sum(COFF) as CycleOff," +
                     " Sum(SETUP) as SumSetup," +
                     " Sum(OTHER) as SumOther " +
                     " FROM" +
                     " (select '" + tblmachineName + "' as  mchName," +
                     " cycletime as detailCycleTime, " +
                     " date_ as detailDate, " +
                     " CASE  WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime  ELSE 0 END as COFF," +
                     " CASE  WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
                     " CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP, " +
                     " CASE " +
                     " WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
                     " and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
                     " and (Status<>'_SETUP' and status<>'SETUP') THEN cycletime " +
                     " ELSE 0 END as OTHER " +
                     " from " + tblmachineName +
                     " where date_ between '" + startDate + "' and '" + endDate + "'" +
                     " ) as tbl" +
                     " GROUP BY  strftime('%W' ,detailDate)  - case strftime('%w' ,detailDate) when 1 then 1 else 0 end, weeknumber  " +
                     " order by weeknumber"
                Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)


                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)

                Return ds.Tbl_History18Monthly

            End Using
        Else
            Dim db_authPath As String = Nothing
            Dim directory As String = getRootPath()
            If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                    db_authPath = reader.ReadLine()
                End Using
            End If
            Dim connectionString As String
            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

            If license = 0 Then
                connectionString = "DATABASE=csi_database;" + MySqlConnectionString
            End If

            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

                Dim query As String = "SELECT  mchName," +
                        " extract(YEAR from detailDate)  as yearnumber," +
                        " extract(MONTH from detailDate)  as monthnumber," +
                        " DATE_FORMAT(detaildate, '%b %y') as xaxis," +
                        " Sum(detailCycleTime) AS Totalcycletime," +
                        " Sum(CON) as CycleOn," +
                        " Sum(COFF) as CycleOff," +
                        " Sum(SETUP) as SumSetup," +
                        " Sum(OTHER) as SumOther " +
                        " FROM" +
                        " (select '" + tblmachineName + "' as  mchName," +
                        " cycletime as detailCycleTime, " +
                        " date_ as detailDate, " +
                        " CASE  WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime  ELSE 0 END as COFF," +
                        " CASE  WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
                        " CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP, " +
                        " CASE " +
                        " WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
                        " and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
                        " and (Status<>'_SETUP' and status<>'SETUP') THEN cycletime " +
                        " ELSE 0 END as OTHER " +
                        " from " + tblmachineName +
                        " where date_ between '" + startDate + "' and '" + endDate + "'" +
                        " ) as tbl" +
                        " GROUP BY yearnumber, monthnumber, xaxis " +
                        " order by yearnumber, monthnumber"

                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(query, sqlConn)

                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, tableName)

                Return ds.Tbl_History18Monthly

            End Using
        End If

    End Function
    Private Shared Function setTimeLine(tblmachineName As String) As DataTable

        Dim TableName As String = "TimeLine"
        Dim startdate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
        Dim enddate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)


                Dim query As String = "SELECT CASE" +
                        " WHEN (Status='_CON' or Status='CYCLE ON') then 'CYCLE ON'" +
                        " when (Status='_COFF' or Status='CYCLE OFF') then 'CYCLE OFF' " +
                        " when (Status='_SETUP' or  Status='SETUP') THEN 'SETUP'" +
                        " ELSE Status END as ReasonName, " +
                        " time_, shift, cycletime" +
                        " from " + tblmachineName +
                        " where status not like '_PART%' and time_ between '" + startdate + "' and '" + enddate + "'"

                Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)

                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, TableName)

                Return ds.TimeLine

            End Using

        Else
            Try
                Dim db_authPath As String = Nothing
                Dim directory As String = getRootPath()
                If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                    Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                        db_authPath = reader.ReadLine()
                    End Using
                End If
                Dim connectionString As String
                connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

                If license = 0 Then
                    connectionString = "DATABASE=csi_database;" + MySqlConnectionString
                End If

                Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

                    Dim query As String = "SELECT CASE" +
                            " WHEN (Status='_CON' or Status='CYCLE ON') then 'CYCLE ON'" +
                            " when (Status='_COFF' or Status='CYCLE OFF') then 'CYCLE OFF' " +
                            " when (Status='_SETUP' or  Status='SETUP') THEN 'SETUP'" +
                            " ELSE Status END as ReasonName, " +
                            " time_, shift, cycletime" +
                            " from " + tblmachineName +
                            " where status not like '_PART%' and time_ between '" + startdate + "' and '" + enddate + "'"

                    Dim adap As MySqlDataAdapter = New MySqlDataAdapter(query, sqlConn)

                    Dim ds As DatasetReporting2 = New DatasetReporting2()

                    Dim returnvalue As Integer = adap.Fill(ds, TableName)

                    Return ds.TimeLine

                End Using

            Catch ex As Exception
                MessageBox.Show("Unable to set timeline")
            End Try
        End If
    End Function
    Private Shared Sub localReport_SubreportProcessing(sender As Object, e As SubreportProcessingEventArgs)

        Dim machine As String = RenameMachine(e.Parameters(0).Values(0))
        For i = 0 To (StatusNameArray.Length / 2) - 1
            If StatusNameArray(i, 0) = RealNameMachine(machine) Then
                statusName_ = StatusNameArray(i, 1)
            End If
        Next

        Try

            If Not machine Like "tbl_*" Then
                machine = "tbl_" + machine
            End If
            Try
                e.DataSources.Add(New ReportDataSource("DataSet_data", setMachineData(True, TypeDePeriode, machine)))
            Catch ex As Exception
                'LogServiceError(ex.ToString(), 1)
                MessageBox.Show("setMachineData Error :" & ex.ToString())
            End Try


            'e.DataSources.Add(New ReportDataSource("DataSet_data", setMachineData(True, TypeDePeriode, machine)))

            Try
                'e.DataSources.Add(New ReportDataSource("DataSet_shiftBarChart", setShiftBarChart(machine, TypeDePeriode)))
                e.DataSources.Add(New ReportDataSource("DataSet_shiftBarChart", setShiftBarChart(machine)))
            Catch ex As Exception
                MessageBox.Show("setShiftBarChart Error :" & ex.ToString())

                'LogServiceError(ex.ToString(), 1)
            End Try

            If (TypeDePeriode = "ww" Or TypeDePeriode = "m") Then
                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_data4", setMachineData(False, TypeDePeriode, machine)))
                Catch ex As Exception
                    MessageBox.Show("setMachineData Error :" & ex.ToString())

                    'LogServiceError(ex.ToString(), 1)
                End Try
            End If


            Try
                e.DataSources.Add(New ReportDataSource("DataSet_4reasons", set4Reason(True, TypeDePeriode, machine)))
            Catch ex As Exception
                MessageBox.Show("set4Reason Error :" & ex.ToString())

                'LogServiceError(ex.ToString(), 1)
            End Try


            If (TypeDePeriode = "ww" Or TypeDePeriode = "m") Then

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_4reasons4", set4Reason(False, TypeDePeriode, machine)))
                Catch ex As Exception
                    MessageBox.Show("set4Reason Error :" & ex.ToString())

                    'LogServiceError(ex.ToString(), 1)
                End Try

            End If

            If (TypeDePeriode = "y") Then

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_History", setHistoryDaily(TypeDePeriode, machine)))
                Catch ex As Exception
                    'LogServiceError(ex.ToString(), 1)
                    MessageBox.Show("setHistoryDaily Error :" & ex.ToString())

                End Try

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_timeLine", setTimeLine(machine)))
                Catch ex As Exception
                    MessageBox.Show("setTimeLine Error :" & ex.ToString())
                    'LogServiceError(ex.ToString(), 1)
                End Try

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_PartNo", setPartNo(True, TypeDePeriode, machine)))
                Catch ex As Exception
                    'LogServiceError(ex.ToString(), 1)
                    MessageBox.Show("setPartNo Error :" & ex.ToString())

                End Try

            ElseIf (TypeDePeriode = "ww") Then

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_History", setHistoryWeekly(machine)))
                Catch ex As Exception
                    MessageBox.Show("setHistoryWeekly Error :" & ex.ToString())

                    'LogServiceError(ex.ToString(), 1)
                End Try

            ElseIf (TypeDePeriode = "m") Then

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_History", setHistoryMonthly(machine)))
                Catch ex As Exception
                    MessageBox.Show("setHistoryMonthly Error :" & ex.ToString())

                    'LogServiceError(ex.ToString(), 1)
                End Try
            ElseIf (TypeDePeriode = "td") Then
                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_History", setHistoryDaily(TypeDePeriode, machine)))
                Catch ex As Exception
                    MessageBox.Show("setHistoryDaily Error :" & ex.ToString())

                    'LogServiceError(ex.ToString(), 1)
                End Try
            End If

        Catch ex As Exception
            MessageBox.Show("localReport_SubreportProcessing Error :" & ex.ToString())

            'LogServiceError(ex.ToString(), 1)
        End Try
        statusName_ = "SETUP"
        machineIndex = machineIndex + 1
    End Sub
    Public Shared Function setMostPredominantStatus(table As DataTable) As DataTable

        For i As Integer = 0 To (StatusNameArray.Length / 2 - 1)
            For index = 0 To table.Rows.Count - 1
                If table.Rows(index).Item("MchName") = StatusNameArray(i, 0) Then
                    table.Rows(index).Item("StatusName") = StatusNameArray(i, 1)
                End If
            Next
        Next



        Return table

    End Function
    Private Shared Function setMachineName() As DataTable

        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)



                Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(
                 Qry_Tbl_RenameMachine, sqlConn)
                '"SELECT DISTINCT MchName FROM ( " + Qry_Tbl_MachineName + " ) tablee where (status NOT LIKE '_PARTN*') group by MchName ", sqlConn)



                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, "tbl_MachineName")

                Return setMostPredominantStatus(ds.Tbl_MachineName)

            End Using
        Else
            Dim db_authPath As String = Nothing
            Dim directory As String = getRootPath()
            If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                    db_authPath = reader.ReadLine()
                End Using
            End If
            Dim connectionString As String
            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

            If license = 0 Then
                connectionString = "DATABASE=csi_database;" + MySqlConnectionString
            End If


            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString) 'db corriger

                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(
                Qry_Tbl_RenameMachine, sqlConn)
                '"SELECT DISTINCT MchName FROM ( " + Qry_Tbl_MachineName + " ) tablee where (status NOT LIKE '_PARTN*') group by MchName ", sqlConn)


                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, "tbl_MachineName")

                Return setMostPredominantStatus(ds.Tbl_MachineName)

            End Using
        End If
    End Function
    Public Shared Function FileInUse(ByVal sFile As String) As Boolean
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
    Private Shared Function saveReport(viewer As ReportViewer, typeDeRap As String, task As String) As String
        Dim warnings As Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim bytes As Byte()
        Dim fs As FileStream
        machineIndex = 0
        bytes = viewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)

        'While (FileInUse(OutputReportPath + "/report " + task + typeDeRap + "_" + dateReporting.ToString()"MMMddyyyy HHmm", CultureInfo.CreateSpecificCulture("en-US")) + DateTime.Now.TimeOfDay.ToString() + ".pdf") = True)
        While (FileInUse(OutputReportPath + "/report " + task + typeDeRap + "_" + DateTime.Now.ToString("MMMddyyyy HHmm", CultureInfo.CreateSpecificCulture("en-US")) + DateTime.Now.TimeOfDay.ToString() + ".pdf") = True)

            'Forms.MessageBox.Show("A file with the same name is already opened, please close:" + System.Environment.NewLine + "- report " + typeDeRap + "_" + dateReporting.ToString("MMMddyyyy", CultureInfo.CreateSpecificCulture("en-US")) + DateTime.Now.TimeOfDay.ToString() + ".pdf")
            MessageBox.Show("A file with the same name is already opened, please close:" + System.Environment.NewLine + "- report " + typeDeRap + "_" + DateTime.Now.ToString("MMMddyyyy", CultureInfo.CreateSpecificCulture("en-US")) + DateTime.Now.TimeOfDay.ToString() + ".pdf")

        End While
        Try
            'fs = New FileStream(OutputReportPath + "/ " + task + " " + typeDeRap + "_" + dateReporting.ToString()"MMMddyyyy", CultureInfo.CreateSpecificCulture("en-US")) + " " + DateTime.Now.TimeOfDay.ToString().Substring(0, DateTime.Now.TimeOfDay.ToString().IndexOf(".")).Replace(":", "-") + ".pdf", FileMode.Create)
            fs = New FileStream(OutputReportPath + "/ " + task + " " + typeDeRap + "_" + DateTime.Now.ToString("MMMddyyyy", CultureInfo.CreateSpecificCulture("en-US")) + " " + DateTime.Now.TimeOfDay.ToString().Substring(0, DateTime.Now.TimeOfDay.ToString().IndexOf(".")).Replace(":", "-") + ".pdf", FileMode.Create)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
        Catch ex As Exception
            'LogServiceError("Error saving report : " + ex.Message, 1)
            MessageBox.Show("Error saving report : " + ex.Message.ToString())
        End Try

        Return fs.Name
    End Function
    Public Shared Function generateReport(machineList As String(), TypeDeRapport As String, ReportStartDate As DateTime, ReportEndDate As DateTime, outputreport As String, StatusName As String(,), isSetup As Boolean, Optional taskname As String = "default") As String


        Dim completePath As String = ""
        Try
            LicenseAffectation()
            StatusNameArray = StatusName
            OutputReportPath = outputreport

            Dim applicationPath As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
            Dim path As String = getRootPath()

            If Not Directory.Exists(path & "\reports_templates") Then
                IO.File.WriteAllBytes(path & "\reports_templates.zip", My.Resources.reports_templates)
                ') 'My.Resources.reports_templates

                ZipFile.ExtractToDirectory(path & "\reports_templates.zip",
                                      path)
            End If
            If (TypeDeRapport.Contains("Today")) Then
                TypeDePeriode = "y"
            ElseIf (TypeDeRapport.Contains("Weekly")) Then
                TypeDePeriode = "ww"
            ElseIf (TypeDeRapport.Contains("Monthly")) Then
                TypeDePeriode = "m"
            ElseIf (TypeDeRapport.Contains("Daily")) Then
                TypeDePeriode = "y"
            End If

            Dim ReportViewer1 As ReportViewer = New ReportViewer()
            generateSqlQuery(machineList)
            Dim Paramet(2) As ReportParameter

            ReportViewer1.ProcessingMode = ProcessingMode.Local
            If (TypeDeRapport.Contains("Today")) Then

                If isSetup Then
                    ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\mainDaily.rdlc"
                Else
                    ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\EventMainDaily.rdlc"
                End If
                ReportViewer1.LocalReport.Refresh()
                'Dim ParametersD(1) As ReportParameter
                TypeDePeriode = "y"
                DateAffectation(ReportStartDate, ReportEndDate)

                Paramet(0) = New ReportParameter("reportType", TypeDePeriode)

                'SHOULD USE THIS
                Paramet(1) = New ReportParameter("startdate", ReportStartDate)
                Paramet(2) = New ReportParameter("enddate", ReportEndDate)

                ReportViewer1.LocalReport.SetParameters(Paramet)

                Call ReloadReport(ReportViewer1)
                ReportViewer1.RefreshReport()
                completePath = saveReport(ReportViewer1, "Today", taskname)
            ElseIf (TypeDeRapport.Contains("Weekly")) Then
                TypeDePeriode = "ww"
                If isSetup Then
                    ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\mainWeekly.rdlc"
                Else
                    ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\EventMainReport.rdlc"
                End If

                ReportViewer1.LocalReport.Refresh()

                DateAffectation(ReportStartDate, ReportEndDate)

                Paramet(0) = New ReportParameter("reportType", TypeDePeriode)
                Paramet(1) = New ReportParameter("startdate", ReportStartDate)
                Paramet(2) = New ReportParameter("enddate", ReportEndDate)

                ReportViewer1.LocalReport.SetParameters(Paramet)
                Call ReloadReport(ReportViewer1)
                ReportViewer1.RefreshReport()
                completePath = saveReport(ReportViewer1, "weekly", taskname)
            ElseIf (TypeDeRapport.Contains("Monthly")) Then
                TypeDePeriode = "m"
                If isSetup Then
                    ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\mainMonthly.rdlc"
                Else
                    ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\EventMainMonthly.rdlc"
                End If

                ReportViewer1.LocalReport.Refresh()

                DateAffectation(ReportStartDate, ReportEndDate)

                Paramet(0) = New ReportParameter("reportType", TypeDePeriode)
                Paramet(1) = New ReportParameter("startdate", ReportStartDate)
                Paramet(2) = New ReportParameter("enddate", ReportEndDate)

                ReportViewer1.LocalReport.SetParameters(Paramet)
                Call ReloadReport(ReportViewer1)
                ReportViewer1.RefreshReport()
                completePath = saveReport(ReportViewer1, "monthly", taskname)
            ElseIf (TypeDeRapport.Contains("Daily")) Then
                'ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\mainDaily.rdlc"

                If isSetup Then
                    ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\mainDaily.rdlc"
                Else
                    ReportViewer1.LocalReport.ReportPath = path + "\reports_templates\EventMainDaily.rdlc"
                End If
                ReportViewer1.LocalReport.Refresh()
                'Dim ParametersD(1) As ReportParameter
                TypeDePeriode = "y"
                DateAffectation(ReportStartDate, ReportEndDate)

                Paramet(0) = New ReportParameter("reportType", TypeDePeriode)

                'SHOULD USE THIS
                Paramet(1) = New ReportParameter("startdate", ReportStartDate)
                Paramet(2) = New ReportParameter("enddate", ReportEndDate)

                ReportViewer1.LocalReport.SetParameters(Paramet)
                Call ReloadReport(ReportViewer1)
                ReportViewer1.RefreshReport()
                completePath = saveReport(ReportViewer1, "daily", taskname)
            End If

            Dim repReport As DirectoryInfo = New DirectoryInfo(path & "\reports_templates")

            File.Delete(path & "\reports_templates.zip")


        Catch ex As Exception
            'LogServiceError("Error while generating a report : " + ex.Message + vbCrLf + "___>" + ex.StackTrace, 1)
            MessageBox.Show("Error while generating a report : " + ex.Message.ToString() + vbCrLf + "___>" + ex.StackTrace.ToString())
            If ex.Message.Contains("ReportViewer") Then WriteWarningReportViewer()
        End Try

        Return completePath
    End Function
    Private Shared Sub WriteWarningReportViewer()
        Try


            Dim directory As String = serverRootPath
            If System.IO.Directory.Exists(serverRootPath & "\Warning.txt") Then File.Delete(directory & "\Warning.txt")

            Using writer As StreamWriter = New StreamWriter(directory & "\Warning.txt", True)
                writer.Write("ReportViewer is missing")
                writer.Close()
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Private Shared Function getAutoReportOrNot() As Boolean
        Dim reportOrNot As Boolean = False
        Try
            Dim cntsql As New MySqlConnection(MySqlConnectionString)
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
        'Dim directory As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Server"

        'If (File.Exists(directory & "\sys\auto_reporting.csys")) Then
        '    Using reader As StreamReader = New StreamReader(directory & "\sys\auto_reporting.csys")
        '        reportOrNot = reader.ReadLine()
        '        reader.Close()
        '    End Using
        'End If

        Return reportOrNot
    End Function
    Public Shared Sub StartAutoreporting(val As Boolean)
        While (val = True)
            GenerateReports()
        End While
    End Sub
    Private Shared Sub updateDoneReport(taskname As String)
        Dim sql As String = ("update  CSI_auth.Auto_Report_config set done = '" + Convert.ToString(Now.DayOfWeek) + "' where Task_name = '" + taskname + "'")
        Dim connection As MySqlConnection = New MySqlConnection(MySqlConnectionString)
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
    Public Shared Sub resetDoneReport()
        Dim sql As String = ("update  CSI_auth.Auto_Report_config set done = 0")
        Dim connection As MySqlConnection = New MySqlConnection(MySqlConnectionString)
        Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)

        Try
            connection.Open()
            mysqlcomm.ExecuteNonQuery()
            connection.Close()
        Catch ex As Exception
            'CSI_Lib.LogServiceError(ex.ToString(), 1)
            MessageBox.Show("resetDoneReport() Error : " & ex.ToString())
            If connection.State = ConnectionState.Open Then connection.Close()
        End Try
    End Sub
    Public Shared Function check_tableExists(db_name As String, tbl_name As String) As Boolean
        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(MySqlConnectionString)
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
'Partial Public Class DatasetReporting2
'End Class


'Partial Public Class DatasetReporting2
'End Class

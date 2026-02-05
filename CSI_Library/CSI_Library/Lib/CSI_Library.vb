Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.IO.File
Imports System.Management
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Sockets
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.ServiceProcess
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms
Imports System.Xml
Imports CSI_Library.CSI_DATA
Imports CSIFLEX.Database.Access
Imports CSIFLEX.eNetLibrary
'Imports CSIFLEX.eNET.Library
Imports CSIFLEX.Reports.Server
Imports CSIFLEX.Reports.Server.Data
Imports CSIFLEX.Reports.Server.DataSource
Imports CSIFLEX.Server.Library
Imports CSIFLEX.Utilities
Imports LumenWorks.Framework.IO.Csv
Imports MathNet.Numerics
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports OpenNETCF.MTConnect


Public Class datatype

    Public Shift As SortedDictionary(Of Double, String)

End Class

Public Class GlobalVariables ' statut live ici
    Public Shared ServerIsListening As Boolean = True
    Public Shared ListOfMachine As New Dictionary(Of String, EMachine)(StringComparer.CurrentCultureIgnoreCase)
    Public Shared mut As New Mutex()
    Public Shared refresh_Interval As Long = 1000
    Public Shared reloadByForm1 As Boolean = False
End Class


Public Class CSI_Library

    Public Shared license As Integer
    Public Shared isLicenseAffect As Boolean = False
    Public Shared isServer As Boolean = False
    Public Shared stat_ As Integer
    Public Shared stat_s As String
    Public verified_month As Boolean = False
    Public M As String = ""

    Public Shared userid As Integer = 0
    Public Shared username As String = ""
    Public Shared userEditTimeline As Boolean = False
    Public Shared userEditPartNumbers As Boolean = False
    Public Shared userMachines As List(Of String)

    Public Shared machinesIdNames As Dictionary(Of String, Tuple(Of String, String))

    Public Shared update As Boolean
    Public Shared eNET_task_mon As String
    Public Shared MTConnect_task_mon As String
    Public Shared logfiles_task_mon As String
    Public Shared main_task_mon As String
    Public Shared db_task_mon As String
    Public Shared MySqlConnectionString As String = "server=localhost;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;Allow User Variables=True;" 'Connect to Local Database (By default) Allow User Variables=True for make changes to csi_machineperf database's tables 
    Public Shared MySqlServerBaseString As String = "user=client;password=csiflex123;port=3306;"

    Public Shared MysqlDataFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\mysql\mysql-5.7.21-win32\Data"
    Public Shared ServerProgramFilesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server"
    Public Shared serverRootPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Server"  '"C:\ProgramData\CSI Flex Server"
    Public Shared ClientRootPath As String = Environment.CurrentDirectory

    Public Shared InstallationPathIsSet = False
    Public Shared InstallationPath As String = String.Empty
    Public Shared LocalHostIP As String = ""
    Public Shared eNET_UDP_PORT As Integer = 17
    Public Shared FilePath_Ehub As String
    Public Shared firstClientUpdate As Boolean = True
    Public Shared firstExecution As Boolean = False
    Public Shared ThreeMonths As Boolean = False
    Public Shared updatestart As Boolean = False
    Public Shared loadingasCON As Boolean = False
    Public Shared DuplicateDevice As Boolean = False
    Public Shared IPChange As Integer = 0
    Public Shared cntsql As New MySqlConnection(MySqlConnectionString)
    Public TargetsMonthly_dic As New Dictionary(Of String, Integer)
    Public TargetsWeekly_dic As New Dictionary(Of String, Integer)
    Public TargetsDaily_dic As New Dictionary(Of String, Integer)
    Public Shared fetchConditionsLock As New Object

    Public Shared MachineId As Integer = 0

    Dim csvFileLength As Int32 = 0
    Public connectionString As String

#Region "little funct"

    Sub New(IsServer As Boolean)

        Me.isServer = IsServer

        Dim db_authPath As String = Nothing
        Dim directory As String = getRootPath()

        If (Not IsServer And File.Exists(directory + "/sys/SrvDBpath.csys")) Then
            Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                db_authPath = reader.ReadLine()
            End Using

            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString
            MySqlConnectionString = connectionString
        Else
            connectionString = MySqlConnectionString
        End If

    End Sub

    Public Shared Sub SetConnString(connStr As String)
        MySqlConnectionString = connStr
    End Sub

    Public Sub KillingAProcess(ProcessName As String)

        Dim cmdcommand As String = "taskkill /FI " & System.Convert.ToChar(34).ToString & "SERVICES EQ " & ProcessName & System.Convert.ToChar(34).ToString & " /F"
        Dim info As New ProcessStartInfo()
        info.FileName = "cmd"
        info.Arguments = "cmd /c " & cmdcommand
        info.UseShellExecute = False
        info.CreateNoWindow = False
        info.WindowStyle = ProcessWindowStyle.Normal
        info.Verb = "runas"

        Dim process As New Process()
        process.StartInfo = info
        process.Start()
        process.WaitForExit(5000)

    End Sub

    Public Function check_DB_connection() As Boolean

        Dim mySqlcnt As MySqlConnection = New MySqlConnection(MySqlConnectionString)
        Try
            mySqlcnt.Open()
            If mySqlcnt.State = ConnectionState.Open Then
                mySqlcnt.Close()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Log.Error("Unable to open a DB connection", ex)
            Return False
        End Try
    End Function

    Public Function Read_colors_from_database(strConn As String) As DataTable

        Try
            Dim tblColors = MySqlAccess.GetDataTable("SELECT * FROM csi_database.tbl_colors", strConn)

            Return tblColors

        Catch ex As Exception

            MsgBox("Unable to read the colors from the database")
            Log.Error(ex)

            Return Nothing
        End Try

    End Function

    Public Function Get_firt_date_database(version_ As Integer) As Date

        Dim db_authPath As String = Nothing
        Dim table_ As DataTable = New DataTable("machine_table")
        Dim adapter As New SQLiteDataAdapter
        Dim firstdate As Date = Date.Today

        If Not (version_ = 3) Then
        Else

            Dim cnt As MySqlConnection
            Try
                cnt = New MySqlConnection(connectionString)
                cnt.Open()

                If cnt.State = 1 Then
                Else
                    MessageBox.Show("Connection to the database failed")
                    Return Nothing
                End If
            Catch ex As Exception
                MessageBox.Show(" Unable to establish a connection to the database ") ' & ex.Message, vbCritical + vbSystemModal)
                Return Nothing
            End Try

            Try
                Dim SQL_adapter As New MySqlDataAdapter
                Dim SQL_reader As MySqlDataReader
                table_ = New DataTable("SQL_table")
                Dim SQL_COMMAND As New MySqlCommand

                Dim query As String = "Select *  FROM CSI_Database.tbl_renamemachines"

                SQL_COMMAND.CommandText = query
                SQL_COMMAND.Connection = cnt
                SQL_reader = SQL_COMMAND.ExecuteReader

                table_.Load(SQL_reader)

                firstdate = New DateTime(Now.Date.Year, 1, 1).Date

            Catch ex As Exception

                MessageBox.Show("Unable to retrieve data from the database ")

                If cnt.State = ConnectionState.Open Then cnt.Close()
                Return Nothing
            End Try

        End If

        Return firstdate

    End Function

    Structure users
        Dim name As String
        Dim password As String
        Dim salt As String
        Dim machines As List(Of String)
        Dim setup__ As List(Of String)
    End Structure


    Structure MachineInfo
        Dim Id As Integer
        Dim MachineName As String
        Dim EnetName As String
    End Structure


    Private Shared MachinesInfoValue As List(Of MachineInfo)

    Public Shared ReadOnly Property MachinesInfo As List(Of MachineInfo)
        Get
            If MachinesInfoValue Is Nothing Then
                LoadMachineNames()
            End If
            Return MachinesInfoValue
        End Get
    End Property

    Private Shared Sub LoadMachineNames()

        MachinesInfoValue = New List(Of MachineInfo)()

        Dim table As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf;", MySqlConnectionString)

        For Each row In table.Rows

            Dim machInfo As MachineInfo = New MachineInfo()
            machInfo.Id = row("Id")
            machInfo.MachineName = row("Machine_Name")
            machInfo.EnetName = row("EnetMachineName")
            MachinesInfoValue.Add(machInfo)

        Next

    End Sub

#Region "log error"

    Public Sub LogServerError(ex As String, highPriority As Boolean)
        Try

            Log.Error(ex)
            Return

            Dim directory As String = serverRootPath


            If System.IO.Directory.Exists(directory) Then

                Using writer As StreamWriter = New StreamWriter(directory & "\Log_Server.txt", True)
                    writer.Write(vbCrLf & Now & " : " & directory & " Ex: " & ex)
                    writer.Close()
                End Using

                Dim counter = My.Computer.FileSystem.GetFiles(directory)
                Dim NbrOfFiles = CStr(counter.Count)
                If NbrOfFiles > 3 Then NbrOfFiles = NbrOfFiles - 3

                Dim infoReader As System.IO.FileInfo
                infoReader = My.Computer.FileSystem.GetFileInfo(directory & "\Log_Server.txt")
                If infoReader.Length > 100000000 Then
                    File.Move(directory & "\Log_Server.txt", directory & "\Log_Server" + NbrOfFiles.ToString() + ".txt")
                    File.Delete(directory & "\Log_Server.txt")
                End If
            Else
                Using writer As StreamWriter = New StreamWriter("C:\Log_Server.txt", True)
                    writer.Write(vbCrLf & Environment.NewLine & Now & " : " & directory & " Ex: " & ex)
                    writer.Close()
                End Using
            End If

            ' End If

        Catch exxx As Exception
            Console.Out.Write(exxx.ToString())

        End Try
    End Sub

    Public Shared Sub LogServiceError(ex As String, highPriority As Boolean)
        Try
            Log.Error(ex)
            Return

            Dim directory As String = serverRootPath

            If System.IO.Directory.Exists(serverRootPath) Then
                Using writer As StreamWriter = New StreamWriter(directory & "\Log_Server.txt", True)
                    writer.Write(vbCrLf & Now & " : " & directory & " Excep: " & ex)
                    writer.Close()
                End Using

                Dim counter = My.Computer.FileSystem.GetFiles(directory)
                Dim NbrOfFiles = CStr(counter.Count)
                If NbrOfFiles > 3 Then NbrOfFiles = NbrOfFiles - 3

                Dim infoReader As System.IO.FileInfo
                infoReader = My.Computer.FileSystem.GetFileInfo(directory & "\Log_Server.txt")
                If infoReader.Length > 100000000 Then
                    File.Move(directory & "\Log_Server.txt", directory & "\Log_Server" + NbrOfFiles.ToString() + ".txt")
                    File.Delete(directory & "\Log_Server.txt")
                End If
            Else
                Using writer As StreamWriter = New StreamWriter("C:\Log_Server.txt", True)
                    writer.Write(vbCrLf & Environment.NewLine & Now & " : " & directory & " Excep: " & ex)
                    writer.Close()
                End Using
            End If
        Catch exxx As Exception
            Console.Out.Write(exxx.ToString())
        End Try
    End Sub

    Public Sub LogClientError(ex As String)

        Log.Error(ex)
        Return

        Try
            Using writer As StreamWriter = New StreamWriter(ClientRootPath & "\ClientLog.txt", True)
                writer.Write(vbCrLf & Now & " : " & ex)
                writer.Close()
            End Using
        Catch exxx As Exception
            Console.Out.Write(exxx.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Funtion that write to EventLog.txt File about the Logs of the application
    ''' </summary>
    ''' <param name="event_"></param>

    Public Sub Log_server_event(event_ As String)
        Try
            'If System.IO.Directory.Exists(serverRootPath) Then
            '    Using writer As StreamWriter = New StreamWriter(serverRootPath & "\EventLog.txt", True)
            '        writer.Write(vbCrLf & Now & " : " & event_)
            '        writer.Close()
            '    End Using
            'Else
            '    Using writer As StreamWriter = New StreamWriter("C:\EventLog.txt", True)
            '        writer.Write(vbCrLf & Now & " : " & event_)
            '        writer.Close()
            '    End Using
            'End If

        Catch exxx As Exception
            Console.Out.Write(exxx.ToString())
        End Try
    End Sub

#End Region

    Public Function GetLoadingAsCON(cntstr As String) As Boolean
        Dim loadingAsCON As Boolean = False

        Try
            Dim dt = MySqlAccess.GetDataTable("SELECT loadingAsCON FROM csi_auth.tbl_serviceconfig")
            loadingAsCON = dt.Rows(0)("LoadingAsCON")
        Catch ex As Exception
            loadingAsCON = False
            Log.Error("ERROR unable to retreive service configs", ex)
        End Try

        Return loadingAsCON

    End Function

    Public Function installReportViewer() As Boolean
        Dim StartInfo As New System.Diagnostics.ProcessStartInfo
        Dim process_ As New Process
        Dim process2_ As New Process


        Dim path As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
        Dim commandSTRING As String

        Try

            If Not File.Exists(path & "\repview_needs.zip") Then
                IO.File.WriteAllBytes(path & "\repview_needs.zip", My.Resources.repview_needs)
            End If

            If Not (File.Exists(path & "\repview_needs\SQLSysClrTypes_x86.msi") And File.Exists(path & "\repview_needs\SQLSysClrTypes_x64.msi") And File.Exists(path & "\repview_needs\ReportViewer.msi")) Then
                ZipFile.ExtractToDirectory(path & "\repview_needs.zip", path)
            End If

            StartInfo.FileName = "cmd" 'starts cmd window
            StartInfo.RedirectStandardInput = True
            StartInfo.RedirectStandardOutput = True
            StartInfo.UseShellExecute = False 'required to redirect
            process_.StartInfo = StartInfo
            process_.Start()

            process2_.StartInfo = StartInfo
            process2_.Start()

            Dim SR As System.IO.StreamReader = process_.StandardOutput
            Dim SW As System.IO.StreamWriter = process_.StandardInput

            Dim SR2 As System.IO.StreamReader = process2_.StandardOutput
            Dim SW2 As System.IO.StreamWriter = process2_.StandardInput

            If Environment.Is64BitOperatingSystem Then
                commandSTRING = "msiexec /i """ & path & "\repview_needs\SQLSysClrTypes_x64.msi"" /quiet /qn /norestart /log """ & path & "/loginstallSqlClr.log"""
                SW.WriteLine(commandSTRING)
                SW.WriteLine("exit")
                Thread.Sleep(500)
            Else
                commandSTRING = "msiexec /i """ & path & "\repview_needs\SQLSysClrTypes_x86.msi"" /quiet /qn /norestart /log """ & path & "/loginstallSqlClr.log"""
                SW.WriteLine(commandSTRING)
                SW.WriteLine("exit")
                Thread.Sleep(500)
            End If

            commandSTRING = "msiexec /i """ & path & "\repview_needs\ReportViewer.msi"" /quiet /qn /norestart /log """ & path & "/loginstallrepview.log"""
            SW2.WriteLine(commandSTRING)
            SW2.WriteLine("exit")
            Return True

        Catch ex As Exception
            Log.Error("Error trying to install the ReportViewer.", ex)
            Return False
        End Try

    End Function

    Public Function getRootPath() As String

        Dim directory As String '= rootPath

        If Not isServer Then
            directory = ClientRootPath
        Else
            directory = serverRootPath
        End If

        Return directory

    End Function

    ''' <summary>
    ''' AES_DEC
    ''' </summary>
    ''' <returns>decrypted </returns>
    Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            AES.Padding = System.Security.Cryptography.PaddingMode.Zeros
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function mailsToList(mailadresses As String) As String()

        Return mailadresses.Split(";"c)

    End Function

    Public Sub sendReportByMail(mailadresses As String, filePath As String, custommsg As String, Optional subject As String = "")

        Dim emailconfig As New DataTable
        emailconfig = LoadEmailConfig()

        If subject = "" Then subject = "CSIFLEX Report"

        If (emailconfig.Rows.Count > 0) Then

            Dim emailSettings = BuildEmailSetting(emailconfig.Rows(0))

            Try

                Dim mail As MailMessage = New MailMessage()
                Dim client As SmtpClient = New SmtpClient(emailSettings.SmtpHost, emailSettings.SmtpPort)

                If Not emailSettings.SenderEmail.Contains("@") Then
                    mail.From = New MailAddress("csiflexreports@gmail.com")
                Else
                    mail.From = New MailAddress(emailSettings.SenderEmail)
                End If

                client.EnableSsl = emailSettings.UseSSL
                client.DeliveryMethod = SmtpDeliveryMethod.Network
                client.UseDefaultCredentials = False

                If (emailSettings.RequireCred) Then
                    client.Credentials = New System.Net.NetworkCredential(emailSettings.SenderEmail, AES_Decrypt(emailSettings.EncryptedPassword, "pass"))
                End If

                mail.Subject = subject
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

            Catch ex As Exception

                Log.Error($"Email error, CONFIG:MailAdresses: { mailadresses.ToString() }, port: { emailSettings.SmtpPort.ToString() }, server: { emailSettings.SmtpHost }, requirecred: { emailSettings.RequireCred.ToString() }, ssl: { emailSettings.UseSSL.ToString() }, email from: { emailSettings.SenderEmail }, pwd: { emailSettings.EncryptedPassword }", ex)

            End Try

        Else
            Log.Error("email server not configured for autoreports")
        End If
    End Sub

    Private Function LoadEmailConfig() As DataTable
        Dim results As New DataTable

        Dim cnt As MySqlConnection = New MySqlConnection(MySqlConnectionString)
        Try
            cnt.Open()

            Dim mysqlcmd As New MySqlCommand("Select * from CSI_auth.tbl_emailreports where isused = 1;", cnt)
            Dim mysqladapter As New MySqlDataAdapter(mysqlcmd)

            mysqladapter.Fill(results)

        Catch ex As Exception
            Log.Error("Unable to load reports email configuration", ex)
        Finally
            cnt.Close()
        End Try

        Return results

    End Function

    Public Function CheckLic() As String()

        Dim resinfos As String()
        ReDim resinfos(2)

        resinfos(0) = "NOK"
        resinfos(1) = ""


        resinfos(0) = "ok"
        resinfos(2) = 3
        Return resinfos

        Dim rootpath As String
        If isServer Then
            rootpath = serverRootPath
        Else
            rootpath = ClientRootPath
        End If

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

    ''' <summary>
    ''' get dirve serial number
    ''' </summary>
    ''' <param name="drive">C:</param>
    ''' <returns>drive serial</returns>
    Public Function GetDriveSerialNumber(ByVal drive As String) As String

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

    ''' <summary>
    ''' get CPU serial number
    ''' </summary>
    ''' <returns>drive serial</returns>
    Public Function GetCPUSerialNumber() As String

        'http://msdn.microsoft.com/en-us/library/windows/desktop/aa394373(v=vs.85).aspx

        Dim cpuSerial As String = String.Empty

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

    '-----------------------------------------------------------------------------------------------------------------------
    ' Gives the eNET path using the file setup_.sys
    '-----------------------------------------------------------------------------------------------------------------------
    Function eNET_path() As String

        Dim directory As String = getRootPath()

        If File.Exists(directory & "\sys\setup_.csys") Then
            Using reader As StreamReader = New StreamReader(directory & "\sys\setup_.csys")

                Dim path As String = reader.ReadLine()
                Return path

            End Using

        Else
            Using writer As StreamWriter = New StreamWriter(directory & "\Log_Server.txt", True)
                writer.Write(vbCrLf & Now & " : " & directory & "\sys\setup_.sys NOT FOUND")
                writer.Close()
            End Using
            Return Nothing
        End If

    End Function


    '-----------------------------------------------------------------------------------------------------------------------
    ' Check if a directory exist, create if if no
    '-----------------------------------------------------------------------------------------------------------------------
    Function dir_(path As String) As String
        Try
            If Not (Directory.Exists(path)) Then
                IO.Directory.CreateDirectory(path)
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
        Return "ok"
    End Function

    '-----------------------------------------------------------------------------------------------------------------------
    ' check the framework version 
    '-----------------------------------------------------------------------------------------------------------------------
    Function verif_FRAMEWORK() As Boolean
        'Dim str As String()
        Dim ok As Boolean
        Try
            '    Dim folder = Directory.GetDirectories(Environment.GetEnvironmentVariable("WINDIR") & "\Microsoft.NET\Framework")
            '    For Each rep In folder
            '        str = rep.Split("\")

            '        If str(UBound(str))(1) = "4" And str(UBound(str))(0) = "v" And str(UBound(str))(3) = "5" Then Return True

            '    Next

            ok = Not (Type.GetType("System.Reflection.ReflectionContext", False) = Nothing)
        Catch ex As Exception

            MessageBox.Show("Error while checking the Framework version : " & ex.Message)
            Return False
        End Try
        Return ok
    End Function


    '-----------------------------------------------------------------------------------------------------------------------
    ' gives the enet path
    '-----------------------------------------------------------------------------------------------------------------------
    Function eNET_path(from As Boolean) As String
        Dim err As String = ""
        Dim Path As String = ""
        Dim checkFileInfo As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim directory As String = getRootPath()

        Try
            If Exists(directory & "\sys\setup_.csys") Then
                Using reader As StreamReader = New StreamReader(directory & "\sys\setup_.csys")
                    Path = reader.ReadLine()
                End Using
            Else
                err = ("setup_.sys not found : ")
            End If
        Catch ex As Exception
            err = err & ex.Message
            Return Nothing
        End Try



        If err <> "" Then
            If from = 0 Then 'Called from the service
                Using writer As StreamWriter = New StreamWriter(checkFileInfo.DirectoryName & "\Log_auto_reporting.csys", True)
                    writer.WriteLine(Now.ToString() & "  :  " & err)
                    writer.Close()
                End Using
                Return True
            Else
                MessageBox.Show(err)
            End If
            GoTo stop_
        End If

        Return Path
stop_:
        Return Nothing

    End Function

    '-----------------------------------------------------------------------------------------------------------------------
    ' gives the database path
    '-----------------------------------------------------------------------------------------------------------------------
    Function db_path(from As Boolean) As String

        Dim err As String = ""
        Dim Path As String = ""
        Dim checkFileInfo As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim directory As String = getRootPath()

        Try
            If Exists(directory & "\sys\setupdb_.csys") Then
                Using reader As StreamReader = New StreamReader(directory & "\sys\setupdb_.csys")
                    Path = reader.ReadLine()
                End Using
            Else
                err = ("setupdb_.sys not found : ")
            End If
        Catch ex As Exception
            err = err & ex.Message
        End Try

        If err <> "" Then
            If from = 0 Then 'Called from the service
                Using writer As StreamWriter = New StreamWriter(directory & "\Log_Server.txt", True)
                    writer.WriteLine(Now.ToString() & "  :  " & err)
                    writer.Close()
                End Using
                Return True
            Else
                MessageBox.Show(err)
            End If
            GoTo stop_
        End If

        Return Path
stop_:
        Return Nothing
    End Function


    '*************************************************************************************************************************************************'
    '**** Load machines names from _SETUP\MonList.sys
    '*************************************************************************************************************************************************'
    Function LoadMachines() As String()

        license = (CheckLic(2))

        Dim eNETpath As String = eNET_path(True), machine(-1) As String, i As Integer = 0

        Dim file As System.IO.StreamReader
        Try
            If Not My.Computer.FileSystem.FileExists(eNETpath + "\_SETUP\MonList.sys") Then
                MessageBox.Show("The file 'Monlist.sys' not found in the eNET repertory")
                Return machine
            End If

            file = My.Computer.FileSystem.OpenTextFileReader(eNETpath + "\_SETUP\MonList.sys")



            While Not file.EndOfStream
                ReDim Preserve machine(i + 1)
                machine(i) = file.ReadLine()
                i = i + 1
            End While
            file.Close()
            Return machine

        Catch ex As Exception
            MessageBox.Show(TraceMessage("Could not load the machines names : " & ex.Message))
            Return machine
        End Try

    End Function

    '-----------------------------------------------------------------------------------------------------------------------
    ' mois_ : 
    ' para : Integer, Month in numbers
    ' out : String, first 3 lettres of the month name
    '-----------------------------------------------------------------------------------------------------------------------
    Public Function strmonth(month As Integer)
        Try
            Select Case month
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

            End Select
        Catch ex As Exception
            MessageBox.Show(TraceMessage(ex.Message))
        End Try
    End Function

#End Region


#Region "Reporting"

    ' Qry by parts if part <> "" , else by machine (if part = "")
    Public Function Get_parts_details(ByRef Part As String, ByRef date_start As Date, ByRef date_end As Date, ByRef MCH As String, version_ As Integer) As DataSet

        Dim DS_Result As DataSet = New DataSet
        Dim cnt_lite As SQLiteConnection
        Dim cnt_MYSQL As MySqlConnection
        Dim table_Partnumbers As DataTable = New DataTable("Partnumbers")
        Dim table_Reportedparts As DataTable = New DataTable("Reportedparts")
        Dim table_Operator As DataTable = New DataTable("Operator")

        If version_ <> 3 Then
        Else
            Dim cntstr As String = ""
            Try

                cnt_MYSQL = New MySqlConnection(connectionString)
                cnt_MYSQL.Open()

                If cnt_MYSQL.State = 1 Then
                Else
                    MessageBox.Show("Connection to the database failed")
                    GoTo End_
                End If

                cntstr = connectionString
            Catch ex As Exception
                MessageBox.Show(" Enable to establish a connection to the database ") ' & ex.Message), vbCritical + vbSystemModal)
                GoTo End_
            End Try
        End If

        Try
            'Queries
            If version_ <> 3 Then

                Dim SQLiteCommande As New SQLiteCommand

                'partnumber
                Dim QryCommande_text As String = "SELECT strftime('%Y-%m-%d %H:%M:%S', start_time) as start_time," +
                    "strftime('%Y-%m-%d %H:%M:%S', end_time) as end_time " +
                    ", elapsed_time, machine, HEADPALLET, shift, partno, total_cycle, good_cycle, short_cycle, long_cycle," +
                    "   avg_good_cycle_time  as avg_good_cycle_time," +
                    "   max_cycle_time  as max_cycle_time," +
                    "   min_cycle_time  as min_cycle_time " +
                            " FROM [tbl_partnumber]  " '+

                If Part <> "" Then
                    QryCommande_text = QryCommande_text & " WHERE Partno = '" & Part & "'"
                Else
                    QryCommande_text = QryCommande_text & " WHERE machine = '" & MCH & "'"
                End If
                If date_start.Date <> date_end.Date Then
                    QryCommande_text = QryCommande_text & " And start_time  between '" + date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' AND '" + date_end.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'"
                Else
                    QryCommande_text = QryCommande_text & " And start_time between '" + date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + date_start.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'"
                End If
                SQLiteCommande.Connection = cnt_lite
                SQLiteCommande.CommandText = QryCommande_text
                Dim reader As SQLiteDataReader = SQLiteCommande.ExecuteReader
                table_Partnumbers.Load(reader)
                reader.Close()

                'reportedpart
                If date_start.Date <> date_end.Date Then
                    QryCommande_text = "Select * from [tbl_reportedparts] where trx_time between '" & date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + date_end.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'"
                Else
                    QryCommande_text = "Select * from [tbl_reportedparts] where trx_time between '" & date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + date_start.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'"
                End If

                If Part = "" Then
                    QryCommande_text = QryCommande_text & " And machine = '" & MCH & "'"
                Else
                    Dim Partno_view As DataView = New DataView(table_Partnumbers)
                    Dim machine_table As DataTable = Partno_view.ToTable(True, "machine")
                    For Each machine As DataRow In machine_table.Rows
                        QryCommande_text = QryCommande_text & " AND machine = '" & machine.Item(0).ToString() & "' UNION "
                    Next
                    If machine_table.Rows.Count <> 0 Then QryCommande_text = QryCommande_text.Substring(0, QryCommande_text.Count - 7)
                End If

                SQLiteCommande.CommandText = QryCommande_text
                SQLiteCommande.Connection = cnt_lite
                reader = SQLiteCommande.ExecuteReader
                table_Reportedparts.Load(reader)
                reader.Close()

                'Operator
                QryCommande_text = "Select OPERATOR, " +
                    "PARTNO, QUANTITY, " +
                    " AVGCYCLETIME  as AVGCYCLETIME, GOODCYCLE,  AVGGOODCYCLETIME  as AVGGOODCYCLETIME, " +
                    "strftime('%Y-%m-%d %H:%M:%S', trx_date) as trx_date, " +
                    "shift, HEADPALLET" +
                " from [tbl_operator]"

                If date_start.Date <> date_end.Date Then
                    QryCommande_text = QryCommande_text & " where trx_date between '" & date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + date_end.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'"
                Else
                    QryCommande_text = QryCommande_text & " where trx_date between '" & date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + date_start.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'"
                End If

                SQLiteCommande.CommandText = QryCommande_text
                SQLiteCommande.Connection = cnt_lite
                reader = SQLiteCommande.ExecuteReader
                table_Operator.Load(reader)

            Else
                Dim QryCommande_text As String
                If date_start.Date <> date_end.Date Then
                    QryCommande_text = "Select * From csi_database.tbl_partnumber Where start_time between '" & date_start.Date & "%' AND '" & date_end.Date & "%' AND end_time between '" & date_start.Date & "%' AND '" & date_end.Date & "%'"
                Else
                    QryCommande_text = "Select * from csi_database.tbl_partnumber where start_time like '" & date_start.Date & "%' AND end_time like '" & date_end.Date & "%'"

                End If

                If Part <> "" Then
                    QryCommande_text = QryCommande_text & " AND Partno = '" & Part & "'"
                Else
                    QryCommande_text = QryCommande_text & " AND machine = '" & MCH & "'"
                End If

                Dim SQL_reader As MySqlDataReader
                Dim SQL_Command As New MySqlCommand
                SQL_Command.CommandText = QryCommande_text
                SQL_Command.Connection = cnt_MYSQL
                SQL_reader = SQL_Command.ExecuteReader
                table_Partnumbers.Load(SQL_reader)

                If date_start.Date <> date_end.Date Then
                    QryCommande_text = "Select * from csi_database.tbl_reportedparts where trx_time between '" & date_start.Date & "%' AND '" & date_end.Date & "%'"
                Else
                    QryCommande_text = "Select * from csi_database.tbl_reportedparts where trx_time like '" & date_start.Date & "%'"
                End If
                If Part = "" Then
                    QryCommande_text = QryCommande_text & " AND machine = '" & MCH & "'"
                Else
                    Dim Partno_view As DataView = New DataView(table_Partnumbers)
                    Dim machine_table As DataTable = Partno_view.ToTable(True, "machine")
                    For Each machine As DataRow In machine_table.Rows
                        QryCommande_text = QryCommande_text & " AND machine = '" & machine.Item(0).ToString() & "' UNION "
                    Next
                    If machine_table.Rows.Count <> 0 Then QryCommande_text = QryCommande_text.Substring(0, QryCommande_text.Count - 7)
                End If

                SQL_Command.CommandText = QryCommande_text
                SQL_Command.Connection = cnt_MYSQL
                SQL_reader = SQL_Command.ExecuteReader
                table_Reportedparts.Load(SQL_reader)

                If Part <> "" Then
                    QryCommande_text = "Select * from csi_database.tbl_operator where trx_date between '" & date_start.Date & "%' AND '" & date_end.Date & "%'"
                    QryCommande_text = QryCommande_text & " AND Partno = '" & Part & "'"
                Else
                    Dim Partno_view As DataView = New DataView(table_Partnumbers)
                    Dim machine_table As DataTable = Partno_view.ToTable(True, "PartNumber")
                    For Each row As DataRow In machine_table.Rows
                        QryCommande_text = "Select * from csi_database.tbl_operator where trx_date between '" & date_start.Date & "%' AND '" & date_end.Date & "%'"
                        QryCommande_text = QryCommande_text & " AND PARTNO = '" & row(0).ToString() & "' UNION "
                    Next
                    QryCommande_text = QryCommande_text.Substring(0, QryCommande_text.Count - 7)
                End If

                SQL_Command.CommandText = QryCommande_text
                SQL_Command.Connection = cnt_MYSQL
                SQL_reader = SQL_Command.ExecuteReader
                table_Operator.Load(SQL_reader)

            End If

        Catch ex As Exception
            MessageBox.Show(" Enable to query the database " & ex.Message)

            GoTo End_
        End Try

        Dim table_mch As DataTable = New DataTable

        If version_ <> 3 Then

            Dim Commande_text As String = ""
            If Part = "" Then
                'Commande_text = Commande_text & " And machine = '" & MCH & "'"
            Else
                Dim Partno_view As DataView = New DataView(table_Partnumbers)
                Dim machine_table As DataTable = Partno_view.ToTable(True, "machine")
                For Each machine As DataRow In machine_table.Rows
                    If date_start.Date <> date_end.Date Then
                        Commande_text = "Select Date_, status, shift, cycletime, partnumber from [tbl_" & RenameMachine(machine.Item(0).ToString()) & "] where ShiftDate between '" & date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + date_end.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'" & " UNION "
                    Else
                        Commande_text = "Select Date_, status, shift, cycletime, partnumber from [tbl_" & RenameMachine(machine.Item(0).ToString()) & "] where ShiftDate between '" & date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + date_start.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'" & " UNION "
                    End If
                Next
                If machine_table.Rows.Count <> 0 Then Commande_text = Commande_text.Substring(0, Commande_text.Count - 7)
            End If

            If Commande_text <> "" Then
                Dim SQLiteCommande As New SQLiteCommand
                SQLiteCommande.CommandText = Commande_text
                SQLiteCommande.Connection = cnt_lite
                Dim reader As SQLiteDataReader = SQLiteCommande.ExecuteReader
                table_mch.Load(reader)
            End If
            table_mch.TableName = "tbl_mch"


        Else
            Dim Commande_text As String = ""
            If Part = "" Then
                'Commande_text = Commande_text & " And machine = '" & MCH & "'"
            Else
                Dim Partno_view As DataView = New DataView(table_Partnumbers)
                Dim machine_table As DataTable = Partno_view.ToTable(True, "machine")
                For Each machine As DataRow In machine_table.Rows
                    If date_start.Date <> date_end.Date Then
                        Commande_text = $"Select Date_, status, shift, cycletime, partnumber from csi_database.tbl_{ RenameMachine(machine.Item(0).ToString()) } where ShiftDate between '{ date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) }' and '{ date_end.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) }' UNION "
                    Else
                        Commande_text = $"Select Date_, status, shift, cycletime, partnumber from csi_database.tbl_{ RenameMachine(machine.Item(0).ToString()) } where ShiftDate between '{ date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) }' and '{ date_start.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) }' UNION "
                    End If
                Next
                If machine_table.Rows.Count <> 0 Then Commande_text = Commande_text.Substring(0, Commande_text.Count - 7)
            End If

            If Commande_text <> "" Then
                Dim mysqlCommande As New MySqlCommand
                mysqlCommande.CommandText = Commande_text
                mysqlCommande.Connection = cnt_MYSQL
                Dim reader As MySqlDataReader = mysqlCommande.ExecuteReader
                table_mch.Load(reader)
            End If
            table_mch.TableName = "tbl_mch"

        End If

        Try
            DS_Result.Tables.Add(table_Partnumbers)
            DS_Result.Tables.Add(table_Reportedparts)
            DS_Result.Tables.Add(table_Operator)
            DS_Result.Tables.Add(table_mch)
        Catch ex As Exception
            MessageBox.Show(" Enable to add the tables to the dataset " & ex.Message)
            GoTo End_
        End Try
End_:
        If cnt_MYSQL.State = ConnectionState.Open Then cnt_MYSQL.Close()
        Return DS_Result
    End Function

    '-----------------------------------------------------------------------------------------------------------------------
    ' Load cycles time by shifts , use datetimepicker 1 and 2 , treeview1, radiobutton 1 and 2 , and checkbox 1
    ' Out periode_
    '-----------------------------------------------------------------------------------------------------------------------
    Function Detailled(date1 As Date, date2 As Date, machine As String(), from As Boolean) As periode()

        Dim tmp_table As DataSet = New DataSet("table")

        Dim ThirdDim As Integer = 0

        Dim i As Integer
        Dim active_machines(1) As String

        Dim shft As Integer

        Dim sorted_stats(4) As String
        Dim percent(7) As Integer

        Dim k As Integer = 0
        Dim final_time As Double
        Dim total1 As Double = 0
        Dim total2 As Double = 0
        Dim total3 As Double = 0

        Dim last_loop_ As Boolean = False

        Dim final_time_other(4) As Double

        final_time = 0

        Dim periode_return As periode()
        Dim mysqldb As Boolean
        mysqldb = True

        If Not (CheckLic(2) = 3) Then
        Else
            '*************************************************************************************************************************************************'
            '**** DB Connection
            '*************************************************************************************************************************************************'
            Dim cnt As MySqlConnection
            Dim cntstr As String = ""
            Try
                cnt = New MySqlConnection(connectionString)
                cnt.Open()

                If cnt.State = 1 Then
                Else
                    MessageBox.Show("Connection to the database failed")
                    GoTo End_
                End If

                cntstr = connectionString
            Catch ex As Exception
                MessageBox.Show(" Enable to establish a connection to the database ") ' & ex.Message), vbCritical + vbSystemModal)
                GoTo End_
            End Try

            '*************************************************************************************************************************************************'
            '**** DB Connection -END
            '*************************************************************************************************************************************************'
            Dim first_insertion As Boolean = True ' for daily reports

            i = 0
            Dim NO_TABLE As Boolean = False
            Try

                '******************************************************************************************************************************
                'For each selected machine:====================================================================================================
                '==============================================================================================================================
                For Each items In machine
                    '  For shft = 1 To 3
                    If items <> "" Then

                        Dim machEnetName = MachinesInfo.FirstOrDefault(Function(m) m.MachineName = machine(i)).EnetName
                        Dim machTableName = RenameMachine(machEnetName)

                        k = 0
                        final_time = Nothing

                        total1 = 0
                        total2 = 0
                        total3 = 0

                        ' Building the QueR ====================================================

                        Dim adapter As New MySqlDataAdapter
                        Dim reader As MySqlDataReader
                        Dim table_ As DataTable = New DataTable("tmp_table")
                        Dim tmp_table_cmd As New MySqlCommand

                        Try
                            tmp_table_cmd.CommandText = ShiftTableQuery(mysqldb, machTableName, date1, date2)
                            tmp_table_cmd.Connection = cnt

                            reader = tmp_table_cmd.ExecuteReader
                            adapter.SelectCommand = tmp_table_cmd
                            table_.Load(reader)
                        Catch ex As Exception

                            MsgBox("There is no data for " & RealNameMachine(machine(i)) & ", It takes at least one shift to have enouth data")

                            NO_TABLE = True
                            GoTo End_

                        End Try

                        Dim periode_ As New periode
                        periode_.shift1 = New Dictionary(Of String, Double)
                        periode_.shift2 = New Dictionary(Of String, Double)
                        periode_.shift3 = New Dictionary(Of String, Double)

                        ' Select available status ====================================================
                        Dim available_status As DataTable

                        Dim VIEW As DataView = New DataView(table_)

                        For shft = 1 To 3

                            VIEW.RowFilter = "SHIFT =" & shft
                            k = 0

                            Dim stat As String()
                            Dim l As Integer = 0

                            available_status = VIEW.ToTable(True, "status")

                            For Each row In available_status.Rows
                                If (available_status(l).Item("status") <> "_SH_START") And (available_status(l).Item("status") <> "_SH_END") Then
                                    ReDim Preserve stat(k + 1)
                                    stat(k) = available_status(l).Item("status")
                                    k = k + 1
                                End If
                                l = l + 1
                            Next

                            If IsNothing(stat) Then
                            Else
                                If UBound(stat) <> 0 Then
                                    ReDim Preserve stat(UBound(stat) - 1)
                                End If
                            End If

                            '======================================================================= all Status in stat()

                            Dim CycleTime As Object

                            If Not IsNothing(stat) Then

                                For l = 0 To UBound(stat)

                                    CycleTime = table_.Compute("sum(cycletime)", "status='" & stat(l) & "' and shift=" & shft)
                                    If IsDBNull(CycleTime) Then CycleTime = 0

                                    If (loadingasCON And stat(l) = "LOADING") Then
                                        stat(l) = "CYCLE ON"
                                    End If

                                    If shft = 1 Then

                                        If stat(l) = "_CON" Or stat(l) = "CYCLE ON" Then
                                            periode_.shift1.Add("CYCLE ON", CycleTime)
                                        ElseIf stat(l) = "_COFF" Or stat(l) = "CYCLE OFF" Then
                                            periode_.shift1.Add("CYCLE OFF", CycleTime)
                                        ElseIf stat(l) = "_SETUP" Or stat(l) = "SETUP" Then
                                            periode_.shift1.Add("SETUP", CycleTime)
                                        Else
                                            periode_.shift1.Add(stat(l), CycleTime)
                                        End If

                                    ElseIf shft = 2 Then

                                        If stat(l) = "_CON" Or stat(l) = "CYCLE ON" Then
                                            periode_.shift2.Add("CYCLE ON", CycleTime)
                                        ElseIf stat(l) = "_COFF" Or stat(l) = "CYCLE OFF" Then
                                            periode_.shift2.Add("CYCLE OFF", CycleTime)
                                        ElseIf stat(l) = "_SETUP" Or stat(l) = "SETUP" Then
                                            periode_.shift2.Add("SETUP", CycleTime)
                                        Else
                                            periode_.shift2.Add(stat(l), CycleTime)
                                        End If

                                    ElseIf shft = 3 Then

                                        If stat(l) = "_CON" Or stat(l) = "CYCLE ON" Then
                                            periode_.shift3.Add("CYCLE ON", CycleTime)
                                        ElseIf stat(l) = "_COFF" Or stat(l) = "CYCLE OFF" Then
                                            periode_.shift3.Add("CYCLE OFF", CycleTime)
                                        ElseIf stat(l) = "_SETUP" Or stat(l) = "SETUP" Then
                                            periode_.shift3.Add("SETUP", CycleTime)
                                        Else
                                            periode_.shift3.Add(stat(l), CycleTime)
                                        End If

                                    End If

                                Next l
                            Else
                                '"""
                            End If
                            'No_stat:
                            stat = Nothing
                        Next shft

                        periode_.shift1.OrderBy((Function(x) x.Key)).ToDictionary(Function(x) x.Key, Function(y) y.Value)
                        periode_.shift2.OrderBy((Function(x) x.Key)).ToDictionary(Function(x) x.Key, Function(y) y.Value)
                        periode_.shift3.OrderBy((Function(x) x.Key)).ToDictionary(Function(x) x.Key, Function(y) y.Value)

                        If date1.Date = date2.Date Then
                            periode_.date_ = date1.Day & " " & strmonth(date1.Month) & " " & date1.Year
                        Else
                            If date1.Day = 1 And date1.Day = System.DateTime.DaysInMonth(date2.Year, date2.Month) Then
                                periode_.date_ = strmonth(date1.Month) & " " & date1.Year
                            Else
                                If date1.Year = date2.Year Then

                                    periode_.date_ = date1.Day & " " & strmonth(date1.Month) & " to " & date2.Day & " " & strmonth(date2.Month) & " " & (date2.Year)
                                Else
                                    periode_.date_ = date1.Day & " " & strmonth(date1.Month) & " " & (date1.Year) & " to " & date2.Day & " " & strmonth(date2.Month) & " " & (date2.Year)
                                End If
                            End If
                        End If

                        periode_.machine_name = machine(i)

                        ReDim Preserve periode_return(i + 1)
                        periode_return(i) = periode_

                        i = i + 1
                    End If
                Next

            Catch ex As Exception
                If Not NO_TABLE Then MessageBox.Show("Error while parsing machine data")
                If cnt.State = ConnectionState.Open Then cnt.Close()

            End Try

            cnt.Close()
            ThirdDim = ThirdDim - 1
        End If
End_:

        Return periode_return

    End Function ' With shifts

    '-----------------------------------------------------------------------------------------------------------------------
    ' Load cycles time hist , use datetimepicker 1 and 2 , treeview1, radiobutton 1 and 2
    ' Out periode_
    '-----------------------------------------------------------------------------------------------------------------------
    Function Evolution(date1 As Date, date2 As Date, machine As String(), days As Integer, from As Boolean) As periode(,)
        Try
            Dim tmp_table As DataSet = New DataSet("table")

            Dim year_start As Integer
            Dim year_end As Integer
            Dim month_start As Integer
            Dim month_end As Integer
            Dim stat As String()
            Dim ThirdDim As Integer = 0
            Dim day_start As Integer
            Dim day_end As Integer
            Dim i As Integer
            Dim active_machines(1) As String
            Dim shft As Integer = 1
            Dim sorted_stats(4) As String
            Dim percent(7) As Integer
            Dim k As Integer = 0
            Dim final_time As Double
            Dim total1 As Double = 0
            Dim total2 As Double = 0
            Dim total3 As Double = 0
            Dim DaysInMonth As Integer
            Dim last_loop_ As Boolean = False
            Dim final_time_other(4) As Double

            For idx = 0 To machine.Count - 1
                machine(idx) = MachinesInfo.FirstOrDefault(Function(m) m.MachineName = machine(idx) Or m.Id.ToString() = machine(idx)).EnetName
            Next

            final_time = 0
            Dim first_insertion As Boolean = True ' for daily reports
            Dim big_periode_return(UBound(machine), 90) As periode

            Dim use_mysql = True

            If Not (CheckLic(2) = 3) Then 'If (isClientSQlite) Then

            Else

                Dim cnt As MySqlConnection
                Dim cntstr As String = ""
                Try
                    cnt = New MySqlConnection(MySqlConnectionString)
                    cnt.Open()

                    If cnt.State = 1 Then
                    Else
                        MessageBox.Show("Connection to the database failed")
                        GoTo End_
                    End If
                    cntstr = connectionString
                Catch ex As Exception
                    MessageBox.Show(" Enable to establish a connection to the database ") ' & ex.Message), vbCritical + vbSystemModal)
                    GoTo End_
                End Try

                i = 0
                For Each items In machine

                    year_start = date1.Year '- 2000
                    year_end = date2.Year '- 2000
                    month_start = date1.Month
                    month_end = date2.Month
                    day_start = date1.Day
                    day_end = date2.Day

                    ' The entire periode is :
                    Dim j As Integer
                    For j = 0 To 89
                        If days = 1 Then

                            If day_end > 1 Then

                                day_start = day_end - 1
                                month_start = month_end
                                year_start = year_end
                            Else
                                If month_end <> 1 Then
                                    month_start = month_end - 1
                                    year_start = year_end
                                    day_start = System.DateTime.DaysInMonth(year_start, month_start) 'System.DateTime.DaysInMonth(year_start + 2000, month_start)
                                Else
                                    month_start = 12
                                    year_start = year_end - 1
                                    day_start = System.DateTime.DaysInMonth(year_start, month_start) 'System.DateTime.DaysInMonth(year_start + 2000, month_start)
                                End If
                            End If

                            day_end = day_start
                            month_end = month_start
                            year_end = year_start
                        Else

                            If day_end > 7 Then
                                day_start = day_end - 6
                                month_start = month_end
                                year_start = year_end
                            Else
                                If month_end <> 1 Then
                                    month_start = month_end - 1
                                    year_start = year_end
                                    day_start = System.DateTime.DaysInMonth(year_start, month_start) - 6 + day_end 'System.DateTime.DaysInMonth(year_start + 2000, month_start) - 6 + day_end
                                Else
                                    month_start = 12
                                    year_start = year_end - 1
                                    day_start = System.DateTime.DaysInMonth(year_start, month_start) - 6 + day_end 'System.DateTime.DaysInMonth(year_start + 2000, month_start) - 6 + day_end
                                End If
                            End If

                            If j <> 0 Then
                                If day_start > 1 Then
                                    day_end = day_start - 1
                                    month_end = month_start
                                    year_end = year_start
                                Else
                                    If month_start <> 1 Then
                                        month_end = month_start - 1
                                        year_end = year_start
                                        day_end = System.DateTime.DaysInMonth(year_end, month_end) 'System.DateTime.DaysInMonth(year_end + 2000, month_end)
                                    Else
                                        month_end = 12
                                        year_end = year_start - 1
                                        day_end = System.DateTime.DaysInMonth(year_end, month_end) 'System.DateTime.DaysInMonth(year_end + 2000, month_end)
                                    End If
                                End If
                            End If

                            If j > (date2.DayOfYear - date1.DayOfYear) / 7 Then Exit For
                        End If
                    Next j

                    machine(i) = RenameMachine(machine(i))

                    k = 0
                    final_time = Nothing

                    total1 = 0
                    total2 = 0
                    total3 = 0

                    ' Building the QueR ====================================================
                    Dim adapter As New MySqlDataAdapter
                    Dim reader As MySqlDataReader

                    Dim table_ As DataTable = New DataTable("tmp_table")
                    Dim tmp_table_cmd As New MySqlCommand

                    Dim tmpdate As Date = date1.AddDays(-90)
                    tmp_table_cmd.CommandText = ShiftTableQuery(use_mysql, machine(i), tmpdate, date2)
                    tmp_table_cmd.Connection = cnt

                    Try
                        reader = tmp_table_cmd.ExecuteReader
                        adapter.SelectCommand = tmp_table_cmd
                        table_.Load(reader)
                    Catch ex As Exception
                        If ex.HResult = -2147467259 Then
                            GoTo End_
                        End If
                    End Try

                    year_start = date1.Year ' - 2000
                    year_end = date2.Year '- 2000
                    month_start = date1.Month
                    month_end = date2.Month
                    day_start = date1.Day
                    day_end = date2.Day

                    ' Select available status ====================================================
                    Dim available_status As DataTable
                    Dim VIEW As DataView = New DataView(table_)
                    Dim VIEW2 As DataView = New DataView(table_)
                    Dim VIEW3 As DataView = New DataView(table_)
                    Dim VIEW4 As DataView = New DataView(table_)
                    k = 0
                    Dim l As Integer = 0

                    available_status = VIEW.ToTable(True, "status")

                    For Each row In available_status.Rows
                        If (available_status(l).Item("status") <> "_SH_START") And (available_status(l).Item("status") <> "_SH_END") Then
                            ReDim Preserve stat(k + 1)
                            stat(k) = available_status(l).Item("status")
                            k = k + 1
                        End If
                        l = l + 1
                    Next


                    If (IsNothing(stat)) Then
                    Else
                        If UBound(stat) = -1 Then
                        Else
                            ReDim Preserve stat(UBound(stat) - 1)
                        End If
                    End If

                    '======================================================================= all Status in stat()


                    '*************************************************************************************************************************************************'
                    '**** LOOP - week after week ' For 1 periode, 4 weeks after , and +
                    '*************************************************************************************************************************************************'


                    Dim first_passage As Boolean = False

                    For column_ = 0 To 89

                        Dim work_table As New DataTable
                        Dim work_table2 As New DataTable
                        Dim work_table3 As New DataTable
                        Dim work_table4 As New DataTable

                        ' VIEW.RowFilter = ""
                        VIEW = New DataView(table_)
                        VIEW2 = New DataView(table_)
                        VIEW3 = New DataView(table_)
                        VIEW4 = New DataView(table_)

                        If year_start = year_end Then
                            If month_start = month_end Then
                                VIEW.RowFilter = ("Year_ = " & year_start & " and month_ = " & month_start & " and day_ >= " & day_start & " and day_ <= " & day_end)
                            Else
                                DaysInMonth = System.DateTime.DaysInMonth(year_start, month_start) '(year_start + 2000, month_start)
                                VIEW.RowFilter = ("Year_ = " & year_start & " and month_ = " & month_start & " and day_ >= " & day_start & " and day_ <= " & DaysInMonth)
                                VIEW2.RowFilter = ("Year_ = " & year_start & " and month_ = " & month_end & " and day_ >= 1 " & " and day_ <= " & day_end)
                                If month_end - month_start <> 1 Then
                                    VIEW3.RowFilter = ("Year_ = " & year_start & " and month_ >= " & month_start + 1 & " and month_ <=" & month_end - 1)
                                End If

                            End If
                        Else

                            If year_end - year_start = 1 Then
                                DaysInMonth = System.DateTime.DaysInMonth(year_start, month_start) '(year_start + 2000, month_start)
                                VIEW.RowFilter = ("Year_ = " & year_start & " and month_ = " & month_start & " and day_ >= " & day_start & " and day_ <= " & DaysInMonth)
                                If month_start <> 12 Then VIEW2.RowFilter = ("Year_ = " & year_start & " and month_ >= " & month_start + 1 & " and day_ >= 1 " & " and [month_] <= 12")
                                If month_end <> 1 Then
                                    VIEW2.RowFilter = ("Year_ = " & year_end & " and month_ >= 1 " & " and [month_] <=" & month_end - 1)

                                    DaysInMonth = System.DateTime.DaysInMonth(year_end, month_end) '(year_end + 2000, month_end)
                                    VIEW3.RowFilter = ("Year_ = " & year_end & " and month_ =  " & month_end & " and [day_] >= 1 and [day_] <=  " & DaysInMonth)
                                End If

                                VIEW4.RowFilter = ("Year_ = " & year_end & " and month_ = " & month_end & " and day_ >= 1 And day_ <= " & DaysInMonth)

                            End If
                        End If


                        work_table = VIEW.ToTable(True)
                        If month_start <> month_end Then
                            work_table2 = VIEW2.ToTable(True)
                        End If

                        If month_end - month_start > 1 Then
                            work_table3 = VIEW3.ToTable(True)
                        End If

                        If year_start <> year_end Then
                            work_table4 = VIEW4.ToTable(True)
                        End If


                        Dim periode_ As New periode
                        periode_.shift1 = New Dictionary(Of String, Double)
                        periode_.shift2 = New Dictionary(Of String, Double)
                        periode_.shift3 = New Dictionary(Of String, Double)

                        Dim CycleTime As Double
                        Dim other As Integer = 0
                        If IsNothing(stat) Then
                        Else
                            For l = 0 To UBound(stat)
                                If stat(l).Contains("_PARTNO:") Then
                                    'Nothing
                                Else

                                    If Not IsDBNull(work_table.Compute("sum(cycletime)", "status='" & stat(l) & "'")) Then
                                        CycleTime = (work_table.Compute("sum(cycletime)", "status='" & stat(l) & "'"))
                                    End If

                                    If (month_start <> month_end) And (year_start = year_end) Then
                                        If Not IsDBNull(work_table2.Compute("sum(cycletime)", "status='" & stat(l) & "'")) Then CycleTime = CycleTime + (work_table2.Compute("sum(cycletime)", "status='" & stat(l) & "'"))
                                    End If

                                    If (month_end - month_start > 1) Then
                                        If Not IsDBNull(work_table3.Compute("sum(cycletime)", "status='" & stat(l) & "'")) Then
                                            CycleTime = CycleTime + Val(work_table3.Compute("sum(cycletime)", "status='" & stat(l) & "'"))
                                        End If

                                    End If

                                    If year_start <> year_end Then
                                        If month_start <> 12 Then
                                            If Not IsDBNull(work_table2.Compute("sum(cycletime)", "status='" & stat(l) & "'")) Then
                                                CycleTime = CycleTime + (work_table2.Compute("sum(cycletime)", "status='" & stat(l) & "'"))
                                            End If
                                        End If
                                        If Not IsDBNull(work_table4.Compute("sum(cycletime)", "status='" & stat(l) & "'")) Then
                                            CycleTime = CycleTime + Val(work_table4.Compute("sum(cycletime)", "status='" & stat(l) & "'"))
                                        End If
                                    End If

                                    If IsDBNull(CycleTime) Then CycleTime = 0

                                    'Dim loadingascon = GetLoadingAsCON(cntstr)
                                    If (loadingasCON And stat(l) = "LOADING") Then
                                        stat(l) = "CYCLE ON"
                                    End If

                                    If (stat(l) = "_CON" Or stat(l) = "CYCLE ON") Then
                                        periode_.shift1.Add("CYCLE ON", CycleTime)
                                    ElseIf (stat(l) = "_COFF" Or stat(l) = "CYCLE OFF") Then
                                        periode_.shift1.Add("CYCLE OFF", CycleTime)
                                    ElseIf (stat(l) = "_SETUP" Or stat(l) = "SETUP") Then
                                        periode_.shift1.Add("SETUP", CycleTime)
                                    Else
                                        other = other + CycleTime
                                    End If

                                End If
                                CycleTime = Nothing
                            Next l
                            periode_.shift1.Add("OTHER", other)
                        End If


                        periode_.date_ = day_start & " " & day_end & " " & month_start & " " & month_end & " " & year_start & " " & year_end
                        periode_.machine_name = machine(i)

                        big_periode_return(i, column_).shift1 = New Dictionary(Of String, Double)


                        big_periode_return(i, column_).date_ = day_start & " " & day_end & " " & month_start & " " & month_end & " " & year_start & " " & year_end
                        big_periode_return(i, column_).machine_name = machine(i)

                        If periode_.shift1.ContainsKey("CYCLE ON") Then
                            big_periode_return(i, column_).shift1.Add("CYCLE ON", periode_.shift1.Item("CYCLE ON"))
                        Else
                            big_periode_return(i, column_).shift1.Add("CYCLE ON", 0)
                        End If

                        If periode_.shift1.ContainsKey("CYCLE OFF") Then
                            big_periode_return(i, column_).shift1.Add("CYCLE OFF", periode_.shift1.Item("CYCLE OFF"))
                        Else
                            big_periode_return(i, column_).shift1.Add("CYCLE OFF", 0)
                        End If

                        If periode_.shift1.ContainsKey("SETUP") Then
                            big_periode_return(i, column_).shift1.Add("SETUP", periode_.shift1.Item("SETUP"))
                        Else
                            big_periode_return(i, column_).shift1.Add("SETUP", 0)
                        End If

                        If periode_.shift1.ContainsKey("OTHER") Then
                            big_periode_return(i, column_).shift1.Add("OTHER", periode_.shift1.Item("OTHER"))
                        Else
                            big_periode_return(i, column_).shift1.Add("OTHER", 0)
                        End If

                        If (DateDiff(DateInterval.Day, date1, date2) > 0 And column_ = 0) Then
                            day_start = day_end
                        End If

                        '  If column_ > 0 Then  ' Column_ = 0 ==> cycle times for the periode
                        If days = 1 Then

                            If column_ > 0 Then
                                If day_end > 1 Then

                                    day_start = day_end - 1
                                    month_start = month_end
                                    year_start = year_end
                                Else
                                    If month_end <> 1 Then
                                        month_start = month_end - 1
                                        year_start = year_end
                                        day_start = System.DateTime.DaysInMonth(year_start, month_start) '(year_start + 2000, month_start)
                                    Else
                                        month_start = 12
                                        year_start = year_end - 1
                                        day_start = System.DateTime.DaysInMonth(year_start, month_start) '(year_start + 2000, month_start)
                                    End If
                                End If

                                day_end = day_start
                                month_end = month_start
                                year_end = year_start
                            Else

                                month_start = month_end
                                year_start = year_end
                            End If
                        Else

                            If column_ > 0 Then
                                If day_start > 1 Then
                                    day_end = day_start - 1
                                    month_end = month_start
                                    year_end = year_start
                                Else
                                    If month_start <> 1 Then
                                        month_end = month_start - 1
                                        year_end = year_start
                                        day_end = System.DateTime.DaysInMonth(year_end, month_end) '(year_end + 2000, month_end)

                                    Else
                                        month_end = 12
                                        year_end = year_start - 1
                                        day_end = System.DateTime.DaysInMonth(year_end, month_end) '(year_end + 2000, month_end)
                                    End If
                                End If

                            End If

                            If day_end > 6 Then
                                day_start = day_end - 6
                                month_start = month_end
                                year_start = year_end
                            Else
                                If month_end <> 1 Then
                                    month_start = month_end - 1
                                    year_start = year_end
                                    day_start = System.DateTime.DaysInMonth(year_start, month_start) - 6 + day_end
                                Else
                                    month_start = 12
                                    year_start = year_end - 1
                                    day_start = System.DateTime.DaysInMonth(year_start, month_start) - 6 + day_end
                                End If
                            End If

                            '   If column_ > 13 Then Exit For ' no more than 3 month
                        End If
                        first_passage = True

                        'End If

                        periode_ = Nothing

                    Next column_
                    i = i + 1

                Next '/item /machine ========================================================================================================
                ''============================================================================================================================

                cnt.Close()
            End If
End_:
            Return big_periode_return

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            MessageBox.Show(ex.Message, "Error computing evolution chart")

        End Try

    End Function ' With shifts

    '-----------------------------------------------------------------------------------------------------------------------
    ' Generate the data for the time line (3 shift)
    ' Out datatable
    '-----------------------------------------------------------------------------------------------------------------------
    Function TimeLine(Date_ As Date, machine As String, shift As Integer) As DataTable

        Dim tmp_table As DataSet = New DataSet("table")

        Dim year_start As Integer = Date_.Year
        Dim year_end As Integer = Date_.Year

        Dim month_start As Integer = Date_.Month
        Dim month_end As Integer = Date_.Month

        Dim day_start As Integer = Date_.Day
        Dim day_end As Integer = Date_.Day
        Dim second_day_end As Integer = Date_.AddDays(1).Day

        If Not (CheckLic(2) = 3) Then 'If isClientSQlite Then

        Else

            Try
                Dim query = $"SELECT * FROM csi_database.tbl_{ machine } USE INDEX() WHERE ShiftDate = '{ Date_.ToString("yyyy-MM-dd") }' AND shift = { shift }"
                Dim table_ As DataTable = MySqlAccess.GetDataTable(query, MySqlConnectionString)
                table_.TableName = "tmp_table"

                Return table_

            Catch ex As Exception

                MessageBox.Show("Unable to generate Timeline")
                Log.Error(ex)

                Return Nothing

            End Try

        End If

    End Function ' With shifts

#End Region

#Region "read eNET setup files"

    ' Gives a dic of strings, string ( groupe,number : mAchine ) 
    'Function Machines_Setup(eNETrep As String) As Dictionary(Of String, String)
    '    Try
    '        Dim file As System.IO.StreamReader, line As String, toreturn As New Dictionary(Of String, String), previous_line As String = "", grnum As String()


    '        If Not My.Computer.FileSystem.FileExists(eNETrep + "\_SETUP\eHUBConf.csys") Then
    '            MessageBox.Show("The eNET file eHUBConf.sys is not accessible. ")
    '            Return Nothing
    '        Else
    '            file = My.Computer.FileSystem.OpenTextFileReader(eNETrep + "\_SETUP\eHUBConf.csys")
    '            While Not file.EndOfStream
    '                line = file.ReadLine()
    '                If line.StartsWith("NM") And previous_line.EndsWith(":") Then
    '                    grnum = previous_line.Split(":")
    '                    toreturn.Add(grnum(0) & ":", line.Substring(3, line.Length - 3))
    '                End If

    '                previous_line = line
    '            End While
    '            file.Close()
    '            Return toreturn
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("CSIFLEX Cannot read eHUBConf.sys : " & ex.Message)
    '        Return Nothing
    '    End Try

    'End Function

    'which dep for each mch ?, gives a dic of str,str (  machine dep)
    'Function Departement_Setup(eNETrep As String, mch_setup As Dictionary(Of String, String)) As Dictionary(Of String, String)
    '    Try
    '        Dim file As System.IO.StreamReader, line As String, toreturn As New Dictionary(Of String, String), Groupe_Number As String = ""

    '        If Not My.Computer.FileSystem.FileExists(eNETrep + "\_SETUP\MonSetup.csys") Then
    '            MessageBox.Show("The eNET file MonSetup.sys is not accessible. ")
    '            Return Nothing
    '        Else
    '            file = My.Computer.FileSystem.OpenTextFileReader(eNETrep + "\_SETUP\MonSetup.csys")
    '            While Not file.EndOfStream
    '                line = file.ReadLine()
    '                If line.Length > 1 Then

    '                    If (line(1) = "," Or line(2) = ",") And line.EndsWith(":") Then Groupe_Number = line
    '                    If line.StartsWith("DA:") Then
    '                        If line.Substring(3, line.Length - 3) <> "" Then toreturn.Add(mch_setup.Item(Groupe_Number), line.Substring(3, line.Length - 3))
    '                    End If
    '                End If
    '            End While
    '            file.Close()
    '            Return toreturn
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("CSIFLEX Cannot read MonSetup.sys : " & ex.Message)
    '        Return Nothing
    '    End Try

    'End Function

    'shift start and end for dep, gives a dic of str,str (dep, start-end) , start-end = sh1_start,sh1_end,sh2_start,sh2_end,sh3_start,sh3_end , /3600 to have hours 
    ' uses a dic of (  machine dep)
    'Function Shift_Setup(eNETrep As String, mch_dep As Dictionary(Of String, String)) As Dictionary(Of String, String)
    '    Try
    '        Dim file As System.IO.StreamReader, line As String(), toreturn As New Dictionary(Of String, String), Groupe_Number As String = ""
    '        Dim read As Integer = 0 ' read every 4 lines
    '        Dim mch_SH_HOURS As New Dictionary(Of String, String)
    '        If Not My.Computer.FileSystem.FileExists(eNETrep + "\_SETUP\ShiftSetup2.csys") Then
    '            MessageBox.Show("The eNET file ShiftSetup2.sys is not accessible. ")
    '            Return Nothing
    '        Else
    '            file = My.Computer.FileSystem.OpenTextFileReader(eNETrep + "\_SETUP\ShiftSetup2.csys")
    '            While Not file.EndOfStream
    '                line = file.ReadLine().Split(",")
    '                If UBound(line) > 1 Then
    '                    If read = 4 Or read = 0 Then
    '                        read = 0
    '                        toreturn.Add(line(0), line(1) & "," & line(2) & "," & line(3) & "," & line(4) & "," & line(5) & "," & line(6))
    '                    End If
    '                    read += 1
    '                End If

    '            End While
    '            file.Close()

    '            If Not IsNothing(toreturn) Then
    '                For Each item In mch_dep
    '                    If item.Value <> "" Then mch_SH_HOURS.Add(item.Key.Replace(" ", ""), toreturn.Item(item.Value)) ' value = dep, key = machine, mch_dep.Item(item.Value) = hours
    '                Next
    '            End If

    '            Return mch_SH_HOURS
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("CSIFLEX Cannot read ShiftSetup2.sys : " & ex.Message)
    '        Return Nothing
    '    End Try

    'End Function

#End Region

    ''' <summary>
    ''' Lods Daily Tarets 
    ''' </summary>
    ''' <returns></returns>
    Public Function Load_DailyTargets() As Dictionary(Of String, Integer)
#If False Then
         If File.Exists(serverRootPath & "\sys\_Dailytargets.bin") Then
            Try
                Dim d As New Dictionary(Of String, Integer)
                If File.Exists(serverRootPath & "\sys\_Dailytargets.bin") Then
                    Dim fs As IO.FileStream = New IO.FileStream(serverRootPath & "\sys\_Dailytargets.bin", IO.FileMode.Open)
                    Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                    d = bf.Deserialize(fs)
                    fs.Close()
                End If
                TargetsDaily_dic = d
                Return d
            Catch ex As Exception
                LogServerError(ex.ToString(), 1)
            End Try
        End If
#End If

        Try
            Dim d As New Dictionary(Of String, Integer)
            d = TargetsDaily_dic
            Return d
        Catch ex As Exception
            LogServerError(ex.ToString(), 1)
        End Try

    End Function

    ''' <summary>
    ''' Load Weekly Targets
    ''' </summary>
    ''' <returns></returns>
    Public Function Load_WeeklyTargets() As Dictionary(Of String, Integer)
#If False Then
        Dim d As New Dictionary(Of String, Integer)
        If File.Exists(serverRootPath & "\sys\_Weeklytargets.bin") Then
            Try
                If File.Exists(serverRootPath & "\sys\_Weeklytargets.bin") Then
                    Dim fs As IO.FileStream = New IO.FileStream(serverRootPath & "\sys\_Weeklytargets.bin", IO.FileMode.Open)
                    Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                    d = bf.Deserialize(fs)
                    fs.Close()
                End If

                TargetsWeekly_dic = d
                Return d

            Catch ex As Exception
                LogServerError(ex.ToString(), 1)
                Return d
            End Try
        End If
#End If
        Dim d As New Dictionary(Of String, Integer)
        Try
            d = TargetsWeekly_dic
            Return d
        Catch ex As Exception
            LogServerError(ex.ToString(), 1)
            Return d
        End Try

    End Function

    ''' <summary>
    ''' Load Monthly Targets 
    ''' </summary>
    ''' <returns></returns>
    Public Function Load_MonthlyTargets() As Dictionary(Of String, Integer)
#If False Then
        Dim d As New Dictionary(Of String, Integer)
        If File.Exists(serverRootPath & "\sys\_Monthlytargets.bin") Then
            Try
                If File.Exists(serverRootPath & "\sys\_Monthlytargets.bin") Then
                    Dim fs As IO.FileStream = New IO.FileStream(serverRootPath & "\sys\_Monthlytargets.bin", IO.FileMode.Open)
                    Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                    d = bf.Deserialize(fs)
                    fs.Close()
                End If

                TargetsMonthly_dic = d
                Return d

            Catch ex As Exception
                LogServerError(ex.ToString(), 1)
                Return d
            End Try
        End If
#End If
        Dim d As New Dictionary(Of String, Integer)
        Try
            d = TargetsMonthly_dic
            Return d
        Catch ex As Exception
            LogServerError(ex.ToString(), 1)
            Return d
        End Try
    End Function


    Public Function TraceMessage(message As String,
        <CallerMemberName> Optional memberName As String = Nothing,
        <CallerFilePath> Optional sourcefilePath As String = Nothing,
        <CallerLineNumber()> Optional sourceLineNumber As Integer = 0) As String

        Dim ReturnValue As String
        ReturnValue = ("message: " & message)
        ReturnValue += System.Environment.NewLine + ("member name: " & memberName)
        ReturnValue += System.Environment.NewLine + ("source file path: " & sourcefilePath)
        ReturnValue += System.Environment.NewLine + ("source line number: " & sourceLineNumber)

        Return ReturnValue
    End Function


#Region "Database Update procedure"

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

    Public Function RealNameMachine(machine As String) As String
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

    Public Sub setFirstUpdateOrNot(initaldbload As Boolean)

        Using mysqlcon As New MySqlConnection(CSI_Library.MySqlConnectionString)
            Try
                mysqlcon.Open()
                'Dim initialload As Boolean = False
                Dim cmd As String = "delete from csi_auth.tbl_updatestatus;insert into csi_auth.tbl_updatestatus (initialdbload) values(" + initaldbload.ToString() + ");"
                Using mysqlcmd As New MySqlCommand(cmd, mysqlcon)
                    mysqlcmd.ExecuteNonQuery()
                End Using
                mysqlcon.Close()

            Catch ex As Exception
                Log.Error("unable to update service status.", ex)
                If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
            Finally
                If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
            End Try
        End Using
    End Sub

    Public Sub ImportDB_Mysql(pathCSV As String)

        Try
            Dim machcsvfile As String = ""

            If System.IO.Path.GetFileName(pathCSV).StartsWith("_MACHINE_") And System.IO.Path.GetExtension(pathCSV) = ".CSV" Then

                If System.IO.Path.GetFileName(pathCSV).Count = 17 Then

                    machcsvfile = pathCSV

                    Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
                    ' Change culture to en-US.

                    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


                    Dim machlastupdate As New Dictionary(Of String, DateTime)
                    Dim machlistdt As New DataTable
                    Dim minlastdate As DateTime = DateTime.Now.AddMonths(-3)

                    Using mysqlcon As New MySqlConnection(MySqlConnectionString)

                        machlistdt = MySqlAccess.GetDataTable("SELECT table_name, original_name FROM csi_database.tbl_renamemachines; ")

                        For Each row In machlistdt.Rows

                            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
                            Using mysqlcmd As New MySqlCommand($"Select min(Date_) as mindate from CSI_Database.tbl_{ row("table_name") }", mysqlcon)
                                Dim reader As MySqlDataReader = mysqlcmd.ExecuteReader()
                                reader.Read()
                                If Not reader("mindate").Equals(DBNull.Value) Then
                                    lastdate = reader.GetDateTime("mindate")
                                End If
                                reader.Close()
                            End Using

                            machlastupdate.Add(row("original_name"), lastdate)

                            If (lastdate < minlastdate) Then
                                minlastdate = lastdate
                            End If

                        Next

                    End Using

                    Dim endline As Long = File.ReadLines(machcsvfile).Count()

                    Try
                        Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                        If (minlastdate < DateTime.Now.AddMonths(-3)) Then
                            firstdate = CreateDateStr(minlastdate)
                        End If

                        For Each match In File.ReadLines(machcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                            endline = match.lineNumber
                            Exit For
                        Next
                    Catch ex As Exception

                        Log.Error("Unable to find end line", ex)

                    End Try

                    beginTransac(True)


                    Using csv As New CsvReader(New StreamReader(machcsvfile), True, ","c)

                        Dim fieldCount As Integer = csv.FieldCount
                        csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                        csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                        Dim headers As String() = csv.GetFieldHeaders()

                        Dim cmd As New MySqlCommand
                        Dim tempdate As New DateTime()
                        Dim temptime As New DateTime()
                        Dim status As String
                        Dim linecnt As Long = 0
                        Dim comment As String = ""
                        While (csv.ReadNextRecord() And linecnt < endline)

                            Try
                                Dim renamedmachine As String = RenameMachine(csv(0))

                                If Not (machlastupdate.ContainsKey(csv(0))) Then
                                    machlastupdate.Add(csv(0), minlastdate)

                                    MySqlQueries.InsertRenameMachine(csv(0))

                                    MySqlAccess.Validate_Database_Machine_Table(csv(0))

                                End If


                                'csv line example
                                'GANESH,07/23/2015,2,23:28:03,01:30:00,7317,CYCLE ON,0,
                                cmd = New MySqlCommand($"Replace into CSI_Database.tbl_{ renamedmachine } (month_, day_, year_, time_, Date_, status, shift, cycletime, partnumber)" +
                                                 "VALUES (@month,@day,@year,@time,@date,@status,@shift,@cycletime, @partnumber);", sqlInserConn)
                                tempdate = csv(1) 'row("DATE")


                                cmd.Parameters.Add(New MySqlParameter("@month", tempdate.Month))
                                cmd.Parameters.Add(New MySqlParameter("@day", tempdate.Day))
                                cmd.Parameters.Add(New MySqlParameter("@year", tempdate.Year))
                                temptime = csv(3) 'row("START TIME")
                                temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                                cmd.Parameters.Add(New MySqlParameter("@time", temptime))
                                cmd.Parameters.Add(New MySqlParameter("@date", temptime))

                                'If row("MACHINE STATUS") = "_PARTNUMBER" Then
                                If csv(6) = "_PARTNUMBER" Then
                                    status = "_PARTNO:" & csv(8).Split(";")(0)
                                    comment = csv(8)
                                Else
                                    status = csv(6) 'row("MACHINE STATUS")
                                End If

                                cmd.Parameters.Add(New MySqlParameter("@status", status))
                                cmd.Parameters.Add(New MySqlParameter("@shift", csv(2))) 'row("SHIFT")))
                                cmd.Parameters.Add(New MySqlParameter("@cycletime", csv(5))) 'row("ELAPSED TIME")))
                                cmd.Parameters.Add(New MySqlParameter("@partnumber", comment))
                                cmd.ExecuteNonQuery() 'async doesnt wait for finish


                            Catch ex As Exception
                                Log.Error("Error parsing csv file", ex)
                            End Try

                            linecnt += 1
                            Log.Debug($"ImportDB_Mysql - {RenameMachine(csv(0))}")

                        End While

                    End Using

                    beginTransac(False)
                    Thread.CurrentThread.CurrentCulture = originalCulture

                End If
            End If

        Catch ex As Exception

            Log.Error("unable to complete database creation", ex)

        End Try

    End Sub

    Public Sub FirstUpdateDB_Mysql(years__ As String)


        Dim sqlConn As MySqlConnection = New MySqlConnection(MySqlConnectionString)
        Try

            MySqlAccess.ExecuteNonQuery("CREATE TABLE if not exists csi_database.tbl_CSIFLEX_VERSION (version integer PRIMARY KEY);")

            MySqlAccess.ExecuteNonQuery($"insert ignore into csi_database.tbl_CSIFLEX_VERSION (version) VALUES('{ CSI_DATA.CSIFLEX_VERSION.ToString() }');")

            MySqlAccess.ExecuteNonQuery($"GRANT SELECT ON *.* to 'CRM'@'%' identified by 'CRM';")

        Catch ex As Exception
        End Try

        'FirstOEEUpdate_MySQL(years__)
        FirstOperatorUpdate_MySQL(years__)
        FirstPartnumberUpdate_MySQL(years__)
        FirstReportedpartUpdate_MySQL(years__)
        'FirstMachineUpdate_MySQL(years__)

        'eNETHistory.StartLoadEnetMachineFile(years__)
        Dim ttl = eNETHistory.UpdateMachinesDatabase(True, Convert.ToInt16(years__))

        Log_server_event($"====>> Imported {years__ } CSV file: {ttl}")

    End Sub

#Region "MySQL First update with previous years"

    Private Sub FirstOEEUpdate_MySQL(years_ As String)

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
            Dim oeecsvfile As String = ""

            For Each File In files

                If System.IO.Path.GetFileName(File).StartsWith("_OEE_" + years_) And System.IO.Path.GetExtension(File) = ".CSV" Then

                    oeecsvfile = File
                    Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture

                    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

                    Dim startline As Long = 0

                    Try
                        Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                        For Each match In System.IO.File.ReadLines(oeecsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                            startline = match.lineNumber - 1
                            Exit For
                        Next
                    Catch ex As Exception
                        Log.Error("Unable to find start line FirstOEEUpdate_MySQL Function.", ex)
                    End Try

                    startline = -1

                    beginTransac(True)

                    Dim cmd As New MySqlCommand

                    Try
                        Dim cmdstr = "CREATE TABLE IF NOT EXISTS CSI_Database.tbl_oee (`MACHINE` varchar(75) NOT NULL,  `trx_time` datetime NOT NULL, `Avail` float NOT NULL, `Performance` float NOT NULL, `Quality` float NOT NULL, `OEE` float NOT NULL,`HEADPALLET` int, index (trx_time));"

                        cmd = New MySqlCommand(cmdstr, sqlInserConn)
                        cmd.ExecuteNonQuery() 'async doesnt wait for finish

                        Dim machlist As New List(Of String)

                        Using csv As New CsvReader(New StreamReader(oeecsvfile), True, ","c)

                            Dim fieldCount As Integer = csv.FieldCount
                            csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                            csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                            Dim headers As String() = csv.GetFieldHeaders()

                            csv.MoveTo(startline)

                            Dim tempdate As New DateTime()
                            Dim temptime As New DateTime()

                            While (csv.ReadNextRecord())

                                Try
                                    Dim renamedmachine As String = RenameMachine(csv(0))

                                    If Not (machlist.Contains(csv(0))) Then
                                        machlist.Add(csv(0))

                                        MySqlQueries.InsertRenameMachine(csv(0))
                                    End If

                                    'csv line example
                                    'OKUMA,09/24/2016,13:25:52,0.05,85.71,83.33,0.04,0
                                    cmd = New MySqlCommand("Replace into CSI_Database.tbl_oee" +
                                                     " (MACHINE, trx_time, Avail, Performance, Quality, OEE, HEADPALLET)" +
                                                     " VALUES (@MACHINE,@trx_time,@Avail,@Performance,@Quality,@OEE,@HEADPALLET);", sqlInserConn)
                                    tempdate = csv(1) 'row("DATE")
                                    temptime = csv(2) 'row("START TIME")
                                    temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

                                    cmd.Parameters.Add(New MySqlParameter("@MACHINE", csv(0)))
                                    cmd.Parameters.Add(New MySqlParameter("@trx_time", temptime))
                                    cmd.Parameters.Add(New MySqlParameter("@Avail", csv(3)))
                                    cmd.Parameters.Add(New MySqlParameter("@Performance", csv(4)))
                                    cmd.Parameters.Add(New MySqlParameter("@Quality", csv(5)))
                                    cmd.Parameters.Add(New MySqlParameter("@OEE", csv(6)))
                                    cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(7))) 'row("SHIFT")))

                                    cmd.ExecuteNonQuery() 'async doesnt wait for finish


                                Catch ex As Exception
                                    Log.Error("Error parsing oee csv file.", ex)
                                End Try
                            End While

                        End Using

                    Catch ex As Exception
                        Log.Error("Error uploading oee csv file FirstOEEUpdate_MySQL Function.", ex)
                    End Try

                    beginTransac(False)
                    Thread.CurrentThread.CurrentCulture = originalCulture
                Else
                    'Log.Error("Error " & years_ & "'s oee csv file Not Found :", 1)
                End If
            Next
        Catch ex As Exception
            Log.Error("unable to complete OEE Table creation.", ex)
        End Try
    End Sub

    Private Sub FirstOperatorUpdate_MySQL(years_ As String)

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
            Dim opercsvfile As String = ""
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_OPERATOR_" + years_) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    opercsvfile = File

                    Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
                    ' Change culture to en-US.

                    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

                    Dim startline As Long = 0

                    Try
                        Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                        For Each match In System.IO.File.ReadLines(opercsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                            startline = match.lineNumber - 1
                            Exit For
                        Next
                    Catch ex As Exception
                        Log.Error("Unable to find start line FirstOperatorUpdate_MySQL.", ex)
                    End Try

                    startline = -1
                    beginTransac(True)

                    Dim cmd As New MySqlCommand

                    Try
                        Dim machlist As New List(Of String)

                        'open the file "data.csv" which is a CSV file with headers
                        Using csv As New CsvReader(New StreamReader(opercsvfile), True, ","c)

                            Dim fieldCount As Integer = csv.FieldCount
                            csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                            csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                            Dim headers As String() = csv.GetFieldHeaders()

                            csv.MoveTo(startline)

                            Dim tempdate As New DateTime()

                            While (csv.ReadNextRecord())

                                Try
                                    'csv line example
                                    '6021,212-100-10,49,00:01:47,0,00:00:00,01/05/2016,1,0
                                    cmd = New MySqlCommand("Replace into CSI_Database.tbl_operator" +
                                                     " (OPERATOR, PARTNO, QUANTITY, AVGCYCLETIME, GOODCYCLE, AVGGOODCYCLETIME, trx_date, shift, HEADPALLET)" +
                                                     " VALUES (@OPERATOR,@PARTNO,@QUANTITY,@AVGCYCLETIME,@GOODCYCLE,@AVGGOODCYCLETIME,@trx_date,@shift,@HEADPALLET);", sqlInserConn)

                                    tempdate = csv(6)
                                    cmd.Parameters.Add(New MySqlParameter("@OPERATOR", csv(0)))
                                    cmd.Parameters.Add(New MySqlParameter("@PARTNO", csv(1)))
                                    cmd.Parameters.Add(New MySqlParameter("@QUANTITY", csv(2)))
                                    cmd.Parameters.Add(New MySqlParameter("@AVGCYCLETIME", csv(3)))
                                    cmd.Parameters.Add(New MySqlParameter("@GOODCYCLE", csv(4)))
                                    cmd.Parameters.Add(New MySqlParameter("@AVGGOODCYCLETIME", csv(5)))
                                    cmd.Parameters.Add(New MySqlParameter("@trx_date", tempdate))
                                    cmd.Parameters.Add(New MySqlParameter("@shift", csv(7)))
                                    cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(8)))

                                    cmd.ExecuteNonQuery() 'async doesnt wait for finish

                                Catch ex As Exception
                                    Log.Error("Error parsing operator csv file.", ex)
                                End Try
                            End While

                        End Using

                    Catch ex As Exception
                        Log.Error("Error uploading operator csv file.", ex)
                    End Try

                    beginTransac(False)
                    Thread.CurrentThread.CurrentCulture = originalCulture
                Else
                    'Log.Error("Error " & years_ & "'s OPERATOR csv file Not Found :", 1)
                End If
            Next
        Catch ex As Exception
            Log.Error("unable to complete _OPERATOR_ Table creation.", ex)
        End Try
    End Sub

    Private Sub FirstPartnumberUpdate_MySQL(years_ As String)

        Try
            Dim files As String() = Directory.GetFiles(Path.Combine(eNET_path(), "_REPORTS"))

            Dim partcsvfile As String = ""

            For Each File In files

                If Path.GetFileName(File).StartsWith("_PARTNUMBER_" + years_) And Path.GetExtension(File) = ".CSV" Then

                    partcsvfile = File
                    Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
                    ' Change culture to en-US.

                    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

                    Dim cmd As New MySqlCommand

                    Try

                        Dim machlist As New List(Of String)

                        Using csv As New CsvReader(New StreamReader(partcsvfile), True, ","c)

                            Dim fieldCount As Integer = csv.FieldCount
                            csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                            csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                            Dim headers As String() = csv.GetFieldHeaders()

                            csv.MoveTo(-1)

                            Dim tempdate As New DateTime()
                            Dim tempStart As New DateTime()
                            Dim tempEnd As New DateTime()
                            Dim sqlCmd As StringBuilder = New StringBuilder()
                            Dim line As Integer = 2

                            While (csv.ReadNextRecord())

                                Try
                                    tempdate = csv(0)
                                    tempStart = csv(1)
                                    tempStart = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, tempStart.Hour, tempStart.Minute, tempStart.Second)
                                    tempEnd = csv(2)
                                    tempEnd = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, tempEnd.Hour, tempEnd.Minute, tempEnd.Second)

                                    sqlCmd.Clear()
                                    sqlCmd.Append($"REPLACE INTO CSI_Database.tbl_partnumber")
                                    sqlCmd.Append($"    (                                   ")
                                    sqlCmd.Append($"        start_time         ,            ")
                                    sqlCmd.Append($"        end_time           ,            ")
                                    sqlCmd.Append($"        elapsed_time       ,            ")
                                    sqlCmd.Append($"        machine            ,            ")
                                    sqlCmd.Append($"        HEADPALLET         ,            ")
                                    sqlCmd.Append($"        shift              ,            ")
                                    sqlCmd.Append($"        Partno             ,            ")
                                    sqlCmd.Append($"        total_cycle        ,            ")
                                    sqlCmd.Append($"        good_cycle         ,            ")
                                    sqlCmd.Append($"        short_cycle        ,            ")
                                    sqlCmd.Append($"        long_cycle         ,            ")
                                    sqlCmd.Append($"        avg_good_cycle_time,            ")
                                    sqlCmd.Append($"        max_cycle_time     ,            ")
                                    sqlCmd.Append($"        min_cycle_time                  ")
                                    sqlCmd.Append($"    )                                   ")
                                    sqlCmd.Append($"    VALUES                              ")
                                    sqlCmd.Append($"    (                                   ")
                                    sqlCmd.Append($"        '{ tempStart.ToString("yyyy-MM-dd HH:mm:ss")}',")
                                    sqlCmd.Append($"        '{ tempEnd.ToString("yyyy-MM-dd HH:mm:ss")}'  ,")
                                    sqlCmd.Append($"         { csv(3) }        ,            ")
                                    sqlCmd.Append($"        '{ csv(4) }'       ,            ")
                                    sqlCmd.Append($"         { csv(5) }        ,            ")
                                    sqlCmd.Append($"         { csv(6) }        ,            ")
                                    sqlCmd.Append($"        '{ csv(7) }'       ,            ")
                                    sqlCmd.Append($"         { csv(8) }        ,            ")
                                    sqlCmd.Append($"         { csv(9) }        ,            ")
                                    sqlCmd.Append($"         { csv(10) }       ,            ")
                                    sqlCmd.Append($"         { csv(11) }       ,            ")
                                    sqlCmd.Append($"        '{ csv(12) }'      ,            ")
                                    sqlCmd.Append($"        '{ csv(13) }'      ,            ")
                                    sqlCmd.Append($"        '{ csv(14) }'                   ")
                                    sqlCmd.Append($"    )                                   ")

                                    MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                    line += 1

                                Catch ex As Exception
                                    Log.Error($"Error parsing partnumber csv file. Line: {line}", ex)
                                End Try
                            End While

                        End Using

                    Catch ex As Exception
                        Log.Error("Error uploading partnumber csv file.", ex)
                    End Try

                    Thread.CurrentThread.CurrentCulture = originalCulture

                Else
                    'Log.Error("Error " & years_ & "'s PARTNUMBER csv file Not Found :", 1)
                End If
            Next

        Catch ex As Exception
            Log.Error("Unable to complete PARTNUMBER Table creation.", ex)
        End Try

    End Sub

    Private Sub FirstReportedpartUpdate_MySQL(years_ As String)
        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
            Dim partcsvfile As String = ""
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_REPORTEDPARTS_" + years_) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    partcsvfile = File
                    Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
                    ' Change culture to en-US.

                    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

                    Dim startline As Long = 0

                    Try
                        Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                        For Each match In System.IO.File.ReadLines(partcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                            startline = match.lineNumber - 1
                            Exit For
                        Next
                    Catch ex As Exception
                        Log.Error("Unable to find start line FirstReportedpartUpdate_MySQL.", ex)
                    End Try

                    startline = -1
                    beginTransac(True)

                    Dim cmd As New MySqlCommand

                    Try
                        'MACHINE NAME, Date, TIME, TOTAL PARTS, BAD PARTS, HEAD / PALLET, IDEAL CYCLETIME
                        'OKUMA, 09 / 24 / 2016, 09: 59:29,3,8,0,1
                        Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_reportedparts (`machine` varchar(255) NOT NULL,  `trx_time` datetime NOT NULL, `total_parts` int NOT NULL, `bad_parts` int NOT NULL," +
                            " `HEADPALLET` int NOT NULL, `ideal_cycle_time` int NOT NULL,index (trx_time));"

                        cmd = New MySqlCommand(cmdstr, sqlInserConn)
                        cmd.ExecuteNonQuery() 'async doesnt wait for finish

                        Dim machlist As New List(Of String)

                        'open the file "data.csv" which is a CSV file with headers
                        Using csv As New CsvReader(New StreamReader(partcsvfile), True, ","c)

                            Dim fieldCount As Integer = csv.FieldCount
                            csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                            csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                            Dim headers As String() = csv.GetFieldHeaders()

                            csv.MoveTo(startline)

                            Dim tempdate As New DateTime()
                            Dim temptime As New DateTime()

                            While (csv.ReadNextRecord())

                                Try
                                    Dim renamedmachine As String = RenameMachine(csv(0))

                                    If Not (machlist.Contains(csv(0))) Then
                                        machlist.Add(csv(0))
                                        MySqlQueries.InsertRenameMachine(csv(0))
                                    End If

                                    cmd = New MySqlCommand("Replace into CSI_Database.tbl_reportedparts" +
                                                     " (machine, trx_time, total_parts, bad_parts, HEADPALLET, ideal_cycle_time)" +
                                                     " VALUES (@machine,@trx_time,@total_parts,@bad_parts,@HEADPALLET,@ideal_cycle_time);", sqlInserConn)

                                    tempdate = csv(1)
                                    temptime = csv(2)
                                    temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

                                    cmd.Parameters.Add(New MySqlParameter("@machine", csv(0)))
                                    cmd.Parameters.Add(New MySqlParameter("@trx_time", temptime))
                                    cmd.Parameters.Add(New MySqlParameter("@total_parts", csv(3)))
                                    cmd.Parameters.Add(New MySqlParameter("@bad_parts", csv(4)))
                                    cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(5)))
                                    cmd.Parameters.Add(New MySqlParameter("@ideal_cycle_time", csv(6)))

                                    cmd.ExecuteNonQuery() 'async doesnt wait for finish

                                Catch ex As Exception
                                    Log.Error("Error parsing partnumber csv file.", ex)
                                End Try
                            End While

                        End Using

                    Catch ex As Exception
                        Log.Error("Error uploading partnumber csv file.", ex)
                    End Try

                    beginTransac(False)
                    Thread.CurrentThread.CurrentCulture = originalCulture
                Else
                    'Log.Error("Error " & years_ & "'s REPORTEDPARTS csv file Not Found :", 1)
                End If
            Next
        Catch ex As Exception
            Log.Error("unable to complete PARTNUMBER Table creation.", ex)
        End Try
    End Sub

    Public Sub FirstMachineUpdate_MySQL(years_ As String)
        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim machcsvfile As String = ""

            For Each File In files
                If Path.GetFileName(File).StartsWith("_MACHINE_" + years_) And Path.GetExtension(File) = ".CSV" Then

                    machcsvfile = File

                    Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
                    ' Change culture to en-US.

                    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

                    Dim startline As Long = 0

                    Try
                        Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                        For Each match In System.IO.File.ReadLines(machcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                            startline = match.lineNumber - 1
                            Exit For
                        Next
                    Catch ex As Exception
                        Log.Error("Unable to find start line FirstMachineUpdate_MySQL.", ex)
                    End Try

                    startline = -1
                    beginTransac(True)

                    Dim machlist As New List(Of String)
                    Dim comment As String = ""

                    'open the file "data.csv" which is a CSV file with headers
                    Using csv As New CsvReader(New StreamReader(machcsvfile), True, ","c)

                        Dim fieldCount As Integer = csv.FieldCount
                        csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                        csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                        Dim headers As String() = csv.GetFieldHeaders()

                        csv.MoveTo(startline)

                        Dim cmd As New MySqlCommand
                        Dim tempdate As New DateTime()
                        Dim temptime As New DateTime()
                        Dim status As String
                        Log_server_event("first Updat of DB :")
                        While (csv.ReadNextRecord())

                            Try
                                Dim renamedmachine As String = RenameMachine(csv(0))

                                If Not (machlist.Contains(csv(0))) Then
                                    machlist.Add(csv(0))

                                    MySqlQueries.InsertRenameMachine(csv(0))

                                    MySqlAccess.Validate_Database_Machine_Table(csv(0))

                                End If

                                'csv line example
                                'GANESH,07/23/2015,2,23:28:03,01:30:00,7317,CYCLE ON,0,
                                cmd = New MySqlCommand($"Replace into CSI_Database.tbl_{ renamedmachine } (month_, day_, year_, ShiftDate, Date_, status, shift, cycletime, partnumber)" +
                                                 "VALUES (@month,@day,@year,@time,@date,@status,@shift,@cycletime,@partnumber);", sqlInserConn)
                                tempdate = csv(1) 'row("DATE")

                                cmd.Parameters.Add(New MySqlParameter("@month", tempdate.Month))
                                cmd.Parameters.Add(New MySqlParameter("@day", tempdate.Day))
                                cmd.Parameters.Add(New MySqlParameter("@year", tempdate.Year))
                                If (tempdate.Year = "2018" And tempdate.Month = "6") Then
                                    Dim dateofno = tempdate.Year + tempdate.Month + tempdate.Day
                                ElseIf tempdate.Month = "12" Then
                                    Dim overmonth = tempdate.Day + tempdate.Month + tempdate.Year
                                End If
                                temptime = csv(3) 'row("START TIME")
                                temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                                cmd.Parameters.Add(New MySqlParameter("@time", temptime))
                                cmd.Parameters.Add(New MySqlParameter("@date", temptime))

                                If csv(6) = "_PARTNUMBER" Then
                                    status = "_PARTNO:" & csv(8).Split(";")(0)
                                    comment = csv(8)
                                Else
                                    status = csv(6) 'row("MACHINE STATUS")
                                End If

                                cmd.Parameters.Add(New MySqlParameter("@status", status))
                                cmd.Parameters.Add(New MySqlParameter("@shift", csv(2))) 'row("SHIFT")))
                                cmd.Parameters.Add(New MySqlParameter("@cycletime", csv(5))) 'row("ELAPSED TIME")))
                                cmd.Parameters.Add(New MySqlParameter("@partnumber", comment))
                                cmd.ExecuteNonQuery() 'async doesnt wait for finish


                            Catch ex As Exception
                                Log.Error("Error parsing csv file.", ex)
                            End Try
                        End While
                        Log_server_event("first update of DB finished:")

                    End Using

                    beginTransac(False)
                    Thread.CurrentThread.CurrentCulture = originalCulture

                    setFirstUpdateOrNot(False)

                Else
                    'Log.Error("Error " & years_ & "'s MACHINE csv file Not Found :", 1)
                End If
            Next
        Catch ex As Exception
            Log.Error("unable to complete database creation.", ex)
        End Try

    End Sub

#End Region

#Region "mySQL Update"

    Private Function check_if_MCH_db_rebuild_is_needed_mysql() As Boolean

        Dim returnvalue As Boolean = False

        Try

            Dim tableExists = CBool(MySqlAccess.ExecuteScalar("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE `TABLE_NAME` LIKE 'tbl_CSIFLEX_VERSION'").ToString())

            If tableExists = True Then

                Dim table_tablenames = MySqlAccess.GetDataTable("Select version FROM csi_database.tbl_CSIFLEX_VERSION")

                If table_tablenames.Rows.Count = 0 Then
                    Return False
                End If

                'because a stupi error ...
                If table_tablenames.Rows(0).Item(0) = 1894 Then
                    MySqlAccess.ExecuteNonQuery("Update csi_database.tbl_CSIFLEX_VERSION SET version =1865 where version =1894")
                    returnvalue = False
                ElseIf table_tablenames.Rows(0).Item(0) < 1864 Then
                    returnvalue = True
                Else
                    returnvalue = False
                End If

            Else

                Dim drop_list As String = ""
                Dim table_tablenames = MySqlAccess.GetDataTable("SELECT table_name from csi_database.tbl_renamemachines")
                Dim machinename As String = ""
                Dim second_table_mch_name As New DataTable

                For Each row As DataRow In table_tablenames.Rows

                    machinename = row.Item(0)

                    second_table_mch_name = MySqlAccess.GetDataTable($"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE `TABLE_NAME` LIKE 'tbl_{ row.Item(0) }'")

                    If second_table_mch_name.Rows.Count <> 0 Then

                        MySqlAccess.ExecuteNonQuery($"drop table csi_database.tbl_{ machinename }")

                    End If
                Next

                returnvalue = True
            End If

            Return returnvalue

        Catch ex As Exception

            Log.Error(ex)

            Return False

        End Try

    End Function

    Public Sub UpdateDB_Mysql()

        If check_if_MCH_db_rebuild_is_needed_mysql() Then
            Dim sqlConn As MySqlConnection = New MySqlConnection(MySqlConnectionString)
            Try
                Dim years_() As String

                If (File.Exists(serverRootPath & "\sys\years_.csys")) Then
                    Using streader As New StreamReader(serverRootPath + "\sys\years_.csys")
                        years_ = streader.ReadLine().Split(",")
                    End Using

                    If years_.Count = 0 Then
                        FirstMachineUpdate_MySQL(Now.Year)
                    Else
                        For Each year As String In years_
                            If year <> "" Then FirstMachineUpdate_MySQL(year)
                        Next
                    End If

                Else
                    FirstMachineUpdate_MySQL(Now.Year)
                End If

                sqlConn.Open()
                Dim cmd As String = "CREATE TABLE if not exists csi_database.tbl_CSIFLEX_VERSION (version integer PRIMARY KEY);"
                Dim cmdCreateDeviceTable_version As New MySqlCommand(cmd, sqlConn)
                cmdCreateDeviceTable_version.ExecuteNonQuery()

                cmd = "DELETE FROM csi_database.tbl_CSIFLEX_VERSION"
                cmdCreateDeviceTable_version = New MySqlCommand(cmd, sqlConn)
                cmdCreateDeviceTable_version.ExecuteNonQuery()


                cmd = "insert into csi_database.tbl_CSIFLEX_VERSION (version) VALUES('" & CSI_DATA.CSIFLEX_VERSION.ToString() & "');"
                cmdCreateDeviceTable_version = New MySqlCommand(cmd, sqlConn)
                cmdCreateDeviceTable_version.ExecuteNonQuery()
                sqlConn.Close()

            Catch ex As Exception
                LogServerError($"CSI_library.UpdateDB_Mysql. Error: {ex.Message}", 1)
                If sqlConn.State = ConnectionState.Open Then sqlConn.Close()
            End Try

            GoTo end_
        End If

        Perf_Computation_needed = True

        Log_server_event("Updating DB at : " + Now)

        'OEEUpdate_MySQL()
        OperatorUpdate_MySQL()
        PartnumberUpdate_MySQL()
        ReportedpartsUpdate_MySQL()
        MachineUpdate_MySQL()

        Log_server_event("Updating DB finished at : " + Now)
end_:
    End Sub

    Public Perf_Computation_needed As Boolean = False

    Public Sub OEEUpdate_MySQL()
        Dim mysqlcon As New MySqlConnection(MySqlConnectionString)
        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim oeecsvfile As String = ""

            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_OEE_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    oeecsvfile = File
                End If
            Next

            If oeecsvfile = "" Then GoTo exit_func
            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)

            mysqlcon.Open()

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
            Using mysqlcmd As New MySqlCommand("Select max(trx_time) as maxdate from CSI_Database.tbl_oee", mysqlcon)
                Dim reader As MySqlDataReader = mysqlcmd.ExecuteReader()
                reader.Read()
                If Not reader("maxdate").Equals(DBNull.Value) Then
                    lastdate = reader.GetDateTime("maxdate")
                End If
                reader.Close()
            End Using

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            mysqlcon.Close()

            Dim startline As Long = 0

            Try
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    firstdate = CreateDateStr(maxlastdate)
                End If

                For Each match In File.ReadLines(oeecsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                Log.Error("Unable to find start line.", ex)
                If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
            End Try

            beginTransac(True)

            'open the file "data.csv" which is a CSV file with headers
            Using csv As New CsvReader(New StreamReader(oeecsvfile), True, ","c)

                Dim fieldCount As Integer = csv.FieldCount
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                Dim headers As String() = csv.GetFieldHeaders()

                csv.MoveTo(startline)

                Dim cmd As New MySqlCommand
                Dim tempdate As New DateTime()
                Dim temptime As New DateTime()

                While (csv.ReadNextRecord())

                    Try
                        'csv line example
                        'OKUMA,09/24/2016,13:25:52,0.05,85.71,83.33,0.04,0
                        cmd = New MySqlCommand("Replace into CSI_Database.tbl_oee" +
                                             " (MACHINE, trx_time, Avail, Performance, Quality, OEE, HEADPALLET)" +
                                             " VALUES (@MACHINE,@trx_time,@Avail,@Performance,@Quality,@OEE,@HEADPALLET);", sqlInserConn)
                        tempdate = csv(1) 'row("DATE")
                        temptime = csv(2) 'row("START TIME")
                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

                        cmd.Parameters.Add(New MySqlParameter("@MACHINE", csv(0)))
                        cmd.Parameters.Add(New MySqlParameter("@trx_time", temptime))
                        cmd.Parameters.Add(New MySqlParameter("@Avail", csv(3)))
                        cmd.Parameters.Add(New MySqlParameter("@Performance", csv(4)))
                        cmd.Parameters.Add(New MySqlParameter("@Quality", csv(5)))
                        cmd.Parameters.Add(New MySqlParameter("@OEE", csv(6)))
                        cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(7))) 'row("SHIFT")))

                        cmd.ExecuteNonQuery() 'async doesnt wait for finish

                    Catch ex As Exception
                        Log.Error("Error parsing oee csv file.", ex)
                    End Try
                End While
            End Using

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

            'isloading = False
exit_func:
        Catch ex As Exception
            If sqlInserConn.State = ConnectionState.Open Then beginTransac(False)
            Log.Error("unable to complete oee table update.", ex)
            If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
        End Try

    End Sub

    Public Sub OperatorUpdate_MySQL()

        Dim mysqlcon As New MySqlConnection(MySqlConnectionString)

        Try

            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim operatorcsvfile As String = ""

            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_OPERATOR_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    operatorcsvfile = File
                End If
            Next

            If operatorcsvfile = "" Then GoTo exit_func
            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)

            mysqlcon.Open()

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)

            Using mysqlcmd As New MySqlCommand("Select max(trx_date) as maxdate from CSI_Database.tbl_operator", mysqlcon)

                Dim reader As MySqlDataReader = mysqlcmd.ExecuteReader()
                reader.Read()

                If Not reader("maxdate").Equals(DBNull.Value) Then
                    lastdate = reader.GetDateTime("maxdate")
                End If
                reader.Close()

            End Using

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            mysqlcon.Close()

            Dim startline As Long = 0

            Try
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    firstdate = CreateDateStr(maxlastdate)
                End If

                For Each match In File.ReadLines(operatorcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                Log.Error("Unable to find start line.", ex)
                If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
            End Try

            beginTransac(True)

            'open the file "data.csv" which is a CSV file with headers
            Using csv As New CsvReader(New StreamReader(operatorcsvfile), True, ","c)

                Dim fieldCount As Integer = csv.FieldCount
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                Dim headers As String() = csv.GetFieldHeaders()

                csv.MoveTo(startline - 1)

                Dim cmd As New MySqlCommand
                Dim tempdate As New DateTime()
                Dim temptime As New DateTime()
                'Dim status As String

                While (csv.ReadNextRecord())

                    Try
                        'csv line example
                        '6021,212-100-10,49,00:01:47,0,00:00:00,01/05/2016,1,0
                        cmd = New MySqlCommand("Replace into CSI_Database.tbl_operator" +
                                         " (OPERATOR, PARTNO, QUANTITY, AVGCYCLETIME, GOODCYCLE, AVGGOODCYCLETIME, trx_date, shift, HEADPALLET)" +
                                         " VALUES (@OPERATOR,@PARTNO,@QUANTITY,@AVGCYCLETIME,@GOODCYCLE,@AVGGOODCYCLETIME,@trx_date,@shift,@HEADPALLET);", sqlInserConn)

                        tempdate = csv(6)
                        cmd.Parameters.Add(New MySqlParameter("@OPERATOR", csv(0)))
                        cmd.Parameters.Add(New MySqlParameter("@PARTNO", csv(1)))
                        cmd.Parameters.Add(New MySqlParameter("@QUANTITY", csv(2)))
                        cmd.Parameters.Add(New MySqlParameter("@AVGCYCLETIME", csv(3)))
                        cmd.Parameters.Add(New MySqlParameter("@GOODCYCLE", csv(4)))
                        cmd.Parameters.Add(New MySqlParameter("@AVGGOODCYCLETIME", csv(5)))
                        cmd.Parameters.Add(New MySqlParameter("@trx_date", tempdate))
                        cmd.Parameters.Add(New MySqlParameter("@shift", csv(7)))
                        cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(8)))

                        cmd.ExecuteNonQuery() 'async doesnt wait for finish

                    Catch ex As Exception
                        Log.Error("Error parsing operator csv file.", ex)
                    End Try
                End While
            End Using

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

            'isloading = False
exit_func:
        Catch ex As Exception
            If sqlInserConn.State = ConnectionState.Open Then beginTransac(False)
            Log.Error("unable to complete operator table update.", ex)
            If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
        End Try

    End Sub

    Public Sub PartnumberUpdate_MySQL()

        Try
            Dim files As String() = Directory.GetFiles(Path.Combine(eNET_path(), "_REPORTS"))

            Dim partnocsvfile As String = ""

            For Each File In files
                If Path.GetFileName(File).StartsWith($"_PARTNUMBER_{DateTime.Now.Year}") And Path.GetExtension(File) = ".CSV" Then
                    partnocsvfile = File
                End If
            Next

            If partnocsvfile = "" Then Return

            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)

            Try
                Dim maxDate = MySqlAccess.ExecuteScalar("Select max(end_time) as maxdate from CSI_Database.tbl_partnumber")

                If Not maxDate Is Nothing Then
                    lastdate = maxDate
                End If
            Catch ex As Exception
                Log.Error(ex)
            End Try

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            Dim startline As Long = 0

            Try
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    firstdate = CreateDateStr(maxlastdate)
                End If

                For Each match In File.ReadLines(partnocsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    startline = match.lineNumber - 1
                    Exit For
                Next

            Catch ex As Exception
                Log.Error("Unable to find start line", ex)
            End Try

            Log.Info("It will start to import PartNro CSV file")
            Dim count = 0

            Using csv As New CsvReader(New StreamReader(partnocsvfile), True, ","c)

                Dim fieldCount As Integer = csv.FieldCount
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                Dim headers As String() = csv.GetFieldHeaders()

                csv.MoveTo(startline - 1)

                Dim cmd As New MySqlCommand
                Dim tempdate As New DateTime()
                Dim tempStart As New DateTime()
                Dim tempEnd As New DateTime()

                Dim sqlCmd As StringBuilder = New StringBuilder()

                While (csv.ReadNextRecord())

                    Try
                        tempdate = csv(0)
                        tempStart = csv(1)
                        tempStart = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, tempStart.Hour, tempStart.Minute, tempStart.Second)
                        tempEnd = csv(2)
                        tempEnd = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, tempEnd.Hour, tempEnd.Minute, tempEnd.Second)

                        sqlCmd.Clear()
                        sqlCmd.Append($"REPLACE INTO CSI_Database.tbl_partnumber")
                        sqlCmd.Append($"    (                                   ")
                        sqlCmd.Append($"        start_time         ,            ")
                        sqlCmd.Append($"        end_time           ,            ")
                        sqlCmd.Append($"        elapsed_time       ,            ")
                        sqlCmd.Append($"        machine            ,            ")
                        sqlCmd.Append($"        HEADPALLET         ,            ")
                        sqlCmd.Append($"        shift              ,            ")
                        sqlCmd.Append($"        Partno             ,            ")
                        sqlCmd.Append($"        total_cycle        ,            ")
                        sqlCmd.Append($"        good_cycle         ,            ")
                        sqlCmd.Append($"        short_cycle        ,            ")
                        sqlCmd.Append($"        long_cycle         ,            ")
                        sqlCmd.Append($"        avg_good_cycle_time,            ")
                        sqlCmd.Append($"        max_cycle_time     ,            ")
                        sqlCmd.Append($"        min_cycle_time                  ")
                        sqlCmd.Append($"    )                                   ")
                        sqlCmd.Append($"    VALUES                              ")
                        sqlCmd.Append($"    (                                   ")
                        sqlCmd.Append($"        '{ tempStart.ToString("yyyy-MM-dd HH:mm:ss")}',")
                        sqlCmd.Append($"        '{ tempEnd.ToString("yyyy-MM-dd HH:mm:ss")}'  ,")
                        sqlCmd.Append($"         { csv(3) }        ,            ")
                        sqlCmd.Append($"        '{ csv(4) }'       ,            ")
                        sqlCmd.Append($"         { csv(5) }        ,            ")
                        sqlCmd.Append($"         { csv(6) }        ,            ")
                        sqlCmd.Append($"        '{ csv(7) }'       ,            ")
                        sqlCmd.Append($"         { csv(8) }        ,            ")
                        sqlCmd.Append($"         { csv(9) }        ,            ")
                        sqlCmd.Append($"         { csv(10) }       ,            ")
                        sqlCmd.Append($"         { csv(11) }       ,            ")
                        sqlCmd.Append($"        '{ csv(12) }'      ,            ")
                        sqlCmd.Append($"        '{ csv(13) }'      ,            ")
                        sqlCmd.Append($"        '{ csv(14) }'                   ")
                        sqlCmd.Append($"    )                                   ")

                        MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())
                        count += 1

                    Catch ex As Exception
                        Log.Error("Error parsing partnumber csv file", ex)
                    End Try

                End While

            End Using

            Log.Info($"Lines imported from PartNro CSV file: {count}")

            Thread.CurrentThread.CurrentCulture = originalCulture

        Catch ex As Exception

            Log.Error("Unable to complete partnumber table update.", ex)

        End Try

    End Sub

    Public Sub ReportedpartsUpdate_MySQL()

        Dim mysqlcon As New MySqlConnection(MySqlConnectionString)

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim reportedpartscsvfile As String = ""

            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_REPORTEDPARTS_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    reportedpartscsvfile = File
                End If
            Next

            If reportedpartscsvfile = "" Then GoTo exit_func

            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)

            mysqlcon.Open()

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
            Using mysqlcmd As New MySqlCommand("Select max(trx_time) as maxdate from CSI_Database.tbl_reportedparts", mysqlcon)
                Dim reader As MySqlDataReader = mysqlcmd.ExecuteReader()
                reader.Read()
                If Not reader("maxdate").Equals(DBNull.Value) Then
                    lastdate = reader.GetDateTime("maxdate")
                End If
                reader.Close()
            End Using

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            mysqlcon.Close()

            Dim startline As Long = 0

            Try
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    firstdate = CreateDateStr(maxlastdate)
                End If

                For Each match In File.ReadLines(reportedpartscsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                Log.Error("Unable to find start line.", ex)
                If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
            End Try

            beginTransac(True)

            'open the file "data.csv" which is a CSV file with headers
            Using csv As New CsvReader(New StreamReader(reportedpartscsvfile), True, ","c)

                Dim fieldCount As Integer = csv.FieldCount
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                Dim headers As String() = csv.GetFieldHeaders()

                csv.MoveTo(startline - 1)

                Dim cmd As New MySqlCommand
                Dim tempdate As New DateTime()
                Dim temptime As New DateTime()

                While (csv.ReadNextRecord())

                    Try
                        cmd = New MySqlCommand("Replace into CSI_Database.tbl_reportedparts" +
                                             " (machine, trx_time, total_parts, bad_parts, HEADPALLET, ideal_cycle_time)" +
                                             " VALUES (@machine,@trx_time,@total_parts,@bad_parts,@HEADPALLET,@ideal_cycle_time);", sqlInserConn)

                        tempdate = csv(1)
                        temptime = csv(2)
                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

                        cmd.Parameters.Add(New MySqlParameter("@machine", csv(0)))
                        cmd.Parameters.Add(New MySqlParameter("@trx_time", temptime))
                        cmd.Parameters.Add(New MySqlParameter("@total_parts", csv(3)))
                        cmd.Parameters.Add(New MySqlParameter("@bad_parts", csv(4)))
                        cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(5)))
                        cmd.Parameters.Add(New MySqlParameter("@ideal_cycle_time", csv(6)))

                        cmd.ExecuteNonQuery() 'async doesnt wait for finish

                    Catch ex As Exception
                        Log.Error("Error parsing reportedparts csv file.", ex)
                    End Try
                End While
            End Using

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture
exit_func:

        Catch ex As Exception
            If sqlInserConn.State = ConnectionState.Open Then beginTransac(False)
            Log.Error("unable to complete reportedparts table update.", ex)
            If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
        End Try

    End Sub

    Public Sub MachineUpdate_MySQL()

        Try
            Dim csvFile = Path.Combine(eNetServer.EnetReportsFolder, $"_MACHINE_{ DateTime.Now.Year }.CSV")

            If Not File.Exists(csvFile) Then
                Log.Error($"The file '{csvFile}' was not found.")
                Return
            End If

            Dim csvFileInfo As New FileInfo(csvFile)

            Log.Info($"=================> CSV File - {csvFile}, Size: {csvFileInfo.Length} ({csvFileLength})")

            If csvFileInfo.Length <> csvFileLength Then

                Dim total = eNETHistory.UpdateMachinesDatabase()
                csvFileLength = csvFileInfo.Length

                Log.Info($"====>> Update database from CSV file - { total }")

            Else
                Log.Debug($"====>> No changes in the CSV file - { csvFile }")
            End If

            Return

        Catch ex As Exception

            Log.Error("Unable to complete database update", ex)

        End Try

    End Sub

#End Region

    Public Function getFirstUpdateOrNotSQLite() As Boolean 'SERVER SIDE

        Dim initialdbload As Boolean = False

        Try
            If (File.Exists(ClientRootPath + "\sys\csisqlite.db3")) Then
                initialdbload = False
            Else
                initialdbload = True
            End If
        Catch ex As Exception
            LogClientError("unable to open sqlite DB:" & ex.Message)
        End Try

        Return initialdbload
        'End If

    End Function

    Private Function CreateDateStr(pdate As DateTime) As String
        Dim res As String = ""

        If pdate.Month < 10 Then
            res = "0"
        End If
        res += pdate.Month.ToString()
        res += "/"

        If pdate.Day < 10 Then
            res += "0"
        End If
        res += pdate.Day.ToString()
        res += "/"

        res += pdate.Year.ToString()

        Return res
    End Function

    Private depart, fin As DateTime
    Private Shared sqlInserConn As New MySqlConnection(MySqlConnectionString)

    Private Sub beginTransac(auto As Boolean)

        license = CheckLic(2)

        Try
            Dim cmd As New MySqlCommand

            If (auto) Then
                sqlInserConn = New MySqlConnection(connectionString)
                sqlInserConn.Open()

                cmd = New MySqlCommand("SET autocommit = 0; START TRANSACTION;", sqlInserConn)
                cmd.ExecuteNonQuery()
                depart = Now
            Else
                cmd = New MySqlCommand("COMMIT; SET autocommit = 1;", sqlInserConn)
                cmd.ExecuteNonQuery()
                fin = Now
                sqlInserConn.Close()

            End If

        Catch ex As Exception
            Log.Error("Error in beginTransaction.", ex)
        End Try

    End Sub

    Public Function ShiftTableQuery(use_mysql As Boolean, renamedmachine As String, Optional startdate As DateTime = Nothing, Optional enddate As DateTime = Nothing) As String

        Dim query As String
        query = $"SELECT 
                    year_, 
                    month_, 
                    day_, 
                    shift, 
                    status, 
                    sum(cycletime) AS cycletime 
                FROM CSI_Database.tbl_{ renamedmachine }"

        If Not IsNothing(startdate) And Not IsNothing(enddate) Then
            query += $" WHERE ShiftDate BETWEEN '{ startdate.ToString("yyyy-MM-dd") }' AND '{ enddate.AddDays(1).ToString("yyyy-MM-dd") }'"
        End If

        query += " GROUP BY year_, month_, day_, shift, status"

        Log.Info($"Query: {query}")

        Return query

    End Function

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

#End Region


#Region "reporting"


#Region "variable reporting"

    Private Qry_Tbl_MachineName As String
    Private Qry_Tbl_RenameMachine As String
    Private TypeDePeriode As String
    Public OutputReportPath As String
    Private reportstartdate As DateTime
    Private reportenddate As DateTime
    Private reportShift As String = ""
    Private statusName_ As String = "SETUP"
    Private StatusNameArray As String(,)
    Private machineIndex As Integer

#End Region

    Sub generateSqlQuery(machineList As String())

        Dim ListOfMachineSelected As String() = machineList
        Dim i As Integer = 0
        Dim machineTableName = ""

        Qry_Tbl_MachineName = ""
        Qry_Tbl_RenameMachine = $"SELECT machine_name AS MchName, '{ StatusNameArray(0, 1) }' as StatusName FROM csi_auth.tbl_ehub_conf where"

        For Each machine In ListOfMachineSelected
            If (machine <> "") Then
                machineTableName = RenameMachine(machine)

                Qry_Tbl_MachineName += $"SELECT 'tbl_{ machineTableName }' AS MchName, status FROM tbl_{ machineTableName } "
                Qry_Tbl_RenameMachine += $" EnetMachineName = '{ machine }'"

                If Not i = (ListOfMachineSelected.Length - 1) Then
                    Qry_Tbl_MachineName += " union "
                    Qry_Tbl_RenameMachine += " or"
                End If
                i += 1
            End If
        Next
        '   If Qry_Tbl_RenameMachine.EndsWith(" or") Then Qry_Tbl_RenameMachine.Remove(Qry_Tbl_RenameMachine.Length - 2, 2)
        Qry_Tbl_RenameMachine += " GROUP BY MchName ORDER BY MchName"

    End Sub

    Public Sub LicenseAffectation()

        If isLicenseAffect = False Then
            license = CheckLic(2)
            isLicenseAffect = True
        End If

    End Sub

    Public Sub DateAffectation(ReportStartDate_ As DateTime, ReportEndDate_ As DateTime)
        reportstartdate = ReportStartDate_
        reportenddate = ReportEndDate_
    End Sub

    Public Function generateReportForService(machineList As String(), TypeDeRapport As String, Period As String, ReportStartDate As DateTime, ReportEndDate As DateTime, outputreport As String, StatusName As String(,), isSetup As Boolean, ShortFileNameChecked As String, Optional taskname As String = "default") As String

        ''-- This method was created only to be used by the new CSIFLEX.Reporting.Service to it can uses the CSIFLEX Server resources
        ''-- Drausio. 04-Jul-2019

        isServer = True

        Return generateReport(machineList, TypeDeRapport, Period, True, ReportStartDate, ReportEndDate, outputreport, StatusName, isSetup, ShortFileNameChecked, taskname)

    End Function

    Dim parameters As ReportParameters

    Public Function generateReport(machineList As String(), TypeDeRapport As String, Period As String, useDefaultDates As Boolean, ReportStartDate As DateTime, ReportEndDate As DateTime, outputreport As String, StatusName As String(,), isSetup As Boolean, ShortFileNameChecked As String, Optional taskname As String = "default", Optional connString As String = "") As String

        Dim completePath As String = ""

        Dim strConnection As String = ""

        If connString <> "" Then strConnection = connString

        Try
            Dim allMachines = (machineList.Count = 1 And machineList(0).ToUpper().StartsWith("ALL"))

            LicenseAffectation()
            StatusNameArray = StatusName
            OutputReportPath = outputreport

            Dim path As String = getRootPath()
            Dim type As String = ""

            reportShift = "1,2,3"

            Dim machines As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1 ORDER BY Machine_Name;", strConnection)
            Dim enetMachinesList As List(Of String) = New List(Of String)()
            Dim newList As List(Of String) = New List(Of String)()
            Dim mchList As List(Of String) = New List(Of String)()

            For Each row As DataRow In machines.Rows
                Dim mchId = row("Id").ToString()
                Dim mchName = row("Machine_Name").ToString()
                Dim mchEnetName = row("EnetMachineName").ToString()

                If allMachines Or machineList.Contains(mchEnetName) Or machineList.Contains(mchId) Then
                    enetMachinesList.Add(mchEnetName)
                    newList.Add(mchEnetName)
                    mchList.Add(mchName)
                End If
            Next

            Dim statusArray(mchList.Count - 1, 1) As String
            Dim idx = 0
            For Each item As String In mchList
                statusArray(idx, 0) = item
                statusArray(idx, 1) = "SETUP"
                idx += 1
            Next
            StatusNameArray = statusArray

            parameters = New ReportParameters()
            parameters.ReportName = taskname
            parameters.ReportType = TypeDeRapport
            parameters.ReportPeriod = Period
            parameters.ReportTitle = ""
            parameters.MachinesName = newList
            parameters.Scale = "Hours"
            parameters.RdlcPath = path + "\reports_templates"
            parameters.OutputPath = OutputReportPath
            parameters.ShortFileName = False
            parameters.Shift = "1,2,3"

            If allMachines Then
                parameters.Machines = Support.GetMachineTables(strConnection)
            Else
                parameters.Machines = Support.GetMachineTables(strConnection).FindAll(Function(m) parameters.MachinesName.Contains(m.EnetMachineName) Or parameters.MachinesName.Contains(m.MachineId))
            End If

            Dim report = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.auto_report_config WHERE Task_name = '{taskname}'", strConnection)

            If report IsNot Nothing And report.Rows.Count > 0 Then
                reportShift = report.Rows(0)("shift_number").ToString()

                parameters.ReportTitle = report.Rows(0)("ReportTitle").ToString()
                parameters.Shift = reportShift
                parameters.Scale = report.Rows(0)("Scale").ToString()
                parameters.ShortFileName = (report.Rows(0)("short_filename").ToString() = "True")
                parameters.ShowConInSetup = report.Rows(0)("ShowConInSetup")
            End If

            parameters.firstWeekDay = 0
            parameters.lastWeekDay = 6

            If useDefaultDates Then

                If (Period.Contains("Weekly")) Then

                    Dim timeback = report.Rows(0)("timeback").ToString()

                    Try
                        If Not String.IsNullOrEmpty(timeback) Then
                            parameters.firstWeekDay = CInt(timeback.Split("-")(0))
                            parameters.lastWeekDay = CInt(timeback.Split("-")(1))
                        End If
                        If parameters.firstWeekDay < 0 Or parameters.firstWeekDay > 6 Then parameters.firstWeekDay = 0
                        If parameters.lastWeekDay < 0 Or parameters.lastWeekDay > 6 Then parameters.lastWeekDay = 6
                    Catch ex As Exception
                        parameters.firstWeekDay = 0
                        parameters.lastWeekDay = 6
                    End Try

                    Dim qttDays = CInt(DateTime.Today.Date.DayOfWeek) - parameters.lastWeekDay
                    If qttDays < 0 Then qttDays += 7
                    Dim endDate = DateTime.Today.Date.AddDays(-(qttDays - 1)).AddSeconds(-1)

                    qttDays = parameters.lastWeekDay - parameters.firstWeekDay
                    If qttDays < 0 Then qttDays += 7
                    Dim startDate = endDate.Date.AddDays(-qttDays)

                    ReportStartDate = startDate
                    ReportEndDate = endDate

                ElseIf (Period.Contains("Monthly")) Then

                    ReportStartDate = DateTime.Today.AddDays(-(DateTime.Today.Day - 1)).AddMonths(-1)
                    ReportEndDate = ReportStartDate.AddMonths(1).AddSeconds(-1)

                End If

            End If

            parameters.Start = ReportStartDate
            parameters.End = ReportEndDate

            Log.Debug($"generateReport-----------------------------------------------------------------------------------")
            Log.Debug($"generateReport---: Report: {parameters.ReportName}, type: {parameters.ReportType}, path: {path}, outputreport:{OutputReportPath}")

            If Not Directory.Exists(path & "\reports_templates") Then

                File.WriteAllBytes(path & "\reports_templates.zip", My.Resources.reports_templates)

                ZipFile.ExtractToDirectory(path & "\reports_templates.zip", path)

                File.Delete(path & "\reports_templates.zip")
            End If

            If TypeDeRapport = "Availability" Then

                Dim ReportViewer1 As ReportViewer = New ReportViewer()

                Log.Debug($"Creating report{vbCrLf}====>Report: {parameters.ReportName}{vbCrLf}====>Machine: {parameters.Machines(0).MachineName} - {parameters.Machines(0).TableName}")

                AvailabilityOldDataSource.ProcessOldReportDataSource(parameters, strConnection)

                generateSqlQuery(enetMachinesList.ToArray())

                Dim Paramet(3) As ReportParameter

                ReportViewer1.ProcessingMode = ProcessingMode.Local

                If (Period.Contains("Today")) Then

                    ReportViewer1.LocalReport.ReportPath = path + $"\reports_templates\{If(isSetup, "mainDaily", "EventMainDaily")}.rdlc"
                    TypeDePeriode = "t"
                    type = "Today"
                    type = DateTime.Today.ToString("yyyyMMMdd")

                ElseIf (Period.Contains("Weekly")) Then

                    ReportViewer1.LocalReport.ReportPath = path + $"\reports_templates\{If(isSetup, "mainWeekly", "EventMainReport")}.rdlc"
                    TypeDePeriode = "ww"
                    type = "Weekly"

                ElseIf (Period.Contains("Monthly")) Then

                    ReportViewer1.LocalReport.ReportPath = path + $"\reports_templates\{If(isSetup, "mainMonthly", "EventMainMonthly")}.rdlc"
                    TypeDePeriode = "m"
                    type = "Monthly"

                ElseIf (Period.Contains("Yesterday")) Then

                    ReportViewer1.LocalReport.ReportPath = path + $"\reports_templates\{If(isSetup, "mainDaily", "EventMainDaily")}.rdlc"
                    TypeDePeriode = "y"
                    type = "Yesterday"
                    type = DateTime.Today.AddDays(-1).ToString("yyyyMMMdd")

                End If

                ReportViewer1.LocalReport.Refresh()
                DateAffectation(ReportStartDate, ReportEndDate)

                Log.Debug($"Report Parameters: Report: {parameters.ReportName}{vbCrLf}====>Report Type: {TypeDePeriode}{vbCrLf}====>Period: {ReportStartDate.ToString()} - {ReportEndDate.ToString()}{vbCrLf}====>Title: {parameters.ReportTitle}")

                Try
                    Paramet(0) = New ReportParameter("reportType", TypeDePeriode)
                    Paramet(1) = New ReportParameter("startdate", ReportStartDate)
                    Paramet(2) = New ReportParameter("enddate", ReportEndDate)
                    Paramet(3) = New ReportParameter("ReportTitle", parameters.ReportTitle)

                    ReportViewer1.LocalReport.SetParameters(Paramet)

                Catch ex As Exception
                    Log.Error($"Error setting parameters: {TypeDePeriode}; {ReportStartDate}; {ReportEndDate}; {parameters.ReportTitle}", ex)
                    ReportViewer1.Dispose()
                    Return completePath
                End Try

                Try
                    Call ReloadReport(ReportViewer1)

                    ReportViewer1.RefreshReport()
                    Log.Debug($"generateReport---: Report: {parameters.ReportName}, saving file type={type} shortNameChecked:{ShortFileNameChecked}")

                Catch ex As Exception
                    Log.Error("Error reloading report", ex)
                    ReportViewer1.Dispose()
                    Return completePath
                End Try

                completePath = saveReport(ReportViewer1, type, taskname, ShortFileNameChecked)
                Log.Debug($"generateReport---: Report: {parameters.ReportName}, file: {completePath} ")

            Else

                Dim downtimeReport = New DowntimeReport(parameters, strConnection)
                completePath = downtimeReport.GenerateReport()
                Log.Debug($"generateReport---: Report: {parameters.ReportName}, file: {completePath} ")

            End If

        Catch ex As Exception

            Log.Error($"Error while generating the report : { taskname }", ex)

            If ex.Message.Contains("Error creating window handle") Then

                Log.Warn("The service will be terminate")
                Environment.Exit(-1)
            End If

        End Try

        Return completePath

    End Function

    'Private Sub WriteWarningReportViewer()
    '    Try
    '        Dim directory As String = serverRootPath
    '        If System.IO.Directory.Exists(serverRootPath & "\Warning.txt") Then File.Delete(directory & "\Warning.txt")

    '        Using writer As StreamWriter = New StreamWriter(directory & "\Warning.txt", True)
    '            writer.Write("ReportViewer is missing")
    '            writer.Close()
    '        End Using

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Function saveReport(viewer As ReportViewer, typeDeRap As String, task As String, shortFileName As String) As String

        Dim warnings As Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim bytes As Byte()
        Dim fs As FileStream
        Dim fileName As String

        machineIndex = 0

        Try
            If shortFileName = "True" Then 'short file name checked
                fileName = Path.Combine(OutputReportPath, $"{task}_{DateTime.Now.ToString("MMMddyy")}.pdf")
            Else
                fileName = Path.Combine(OutputReportPath, $"{task} {typeDeRap}_{DateTime.Now.ToString("MMMddyyyy HH-mm-ss")}.pdf")
            End If

            Dim idx = 1
            Dim fileNameSize = fileName.Length - 4

            While (FileInUse(fileName) = True)

                fileName = $"{fileName.Substring(0, fileNameSize)}({idx}).pdf"
                idx += 1
                'MessageBox.Show("A file with the same name is already opened, please close:" + System.Environment.NewLine + "- report " + typeDeRap + "_" + DateTime.Now.ToString("MMMddyyyy", CultureInfo.CreateSpecificCulture("en-US")) + DateTime.Now.TimeOfDay.ToString() + ".pdf")
            End While

            bytes = viewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)

            fs = New FileStream(fileName, FileMode.Create)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()

        Catch ex As Exception

            Log.Error($"Error saving report. { fileName }", ex)

        End Try

        Return fs.Name

    End Function

    'Public Function IntToMonday(integ As Integer, param As DateTime) As DateTime

    '    Dim ret As DateTime

    '    ret = DateAdd("ww", integ - 1, DateSerial(Year(param), 1, 1))

    '    Return toMonday(ret)

    'End Function

    'Public Function toMonday(datee As Date) As DateTime

    '    Dim today As Date = datee
    '    Dim dayIndex As Integer = today.DayOfWeek

    '    If dayIndex < DayOfWeek.Monday Then
    '        dayIndex += 7
    '    End If

    '    Dim dayDiff As Integer = dayIndex - DayOfWeek.Monday
    '    Dim monday As Date = today.AddDays(-dayDiff)

    '    Return monday

    'End Function

    Private Sub ReloadReport(viewer As ReportViewer)

        If (viewer.LocalReport.DataSources.Count > 0) Then
            viewer.LocalReport.DataSources.RemoveAt(0)
        End If
        Try
            viewer.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", setMachineName()))
        Catch ex As Exception
            Log.Error(ex)
        End Try

        RemoveHandler viewer.LocalReport.SubreportProcessing, AddressOf localReport_SubreportProcessing
        AddHandler viewer.LocalReport.SubreportProcessing, AddressOf localReport_SubreportProcessing

    End Sub

    Private Sub localReport_SubreportProcessing(sender As Object, e As SubreportProcessingEventArgs)

        Dim machine As String = RenameMachine(e.Parameters(0).Values(0))
        Dim machineName = e.Parameters(0).Values(0)
        Dim production = "CYCLE ON;_CON;CYCLE OFF;_COFF;SETUP;_SETUP"

        For i = 0 To (StatusNameArray.Length / 2) - 1
            If StatusNameArray(i, 0) = RealNameMachine(machine) Then
                statusName_ = StatusNameArray(i, 1)
            End If
        Next

        Dim showConInSetup = parameters.ShowConInSetup

        Dim count = 0

        Try
            If Not machine Like "tbl_*" Then
                machine = "tbl_" + machine
            End If

            Dim dsData As List(Of DataSet_data) = AvailabilityOldDataSource.GetDataSet_Data().FindAll(Function(m) m.MchName.ToUpper() = machineName.ToUpper())
            Log.Debug($"Machine: {machine} - dsData records: {dsData.Count}")

            Dim dsHistory As List(Of DataSet_history) = AvailabilityOldDataSource.GetDataSet_History().FindAll(Function(m) m.MchName.ToUpper() = machineName.ToUpper())
            Log.Debug($"Machine: {machine} - dsHistory records: {dsHistory.Count}")

            Dim dsHistoryDaily As List(Of DataSet_history_daily) = AvailabilityOldDataSource.GetDataSet_History_Daily().FindAll(Function(m) m.MchName.ToUpper() = machineName.ToUpper())
            Log.Debug($"Machine: {machine} - dsHistoryDaily records: {dsHistoryDaily.Count}")

            Dim ds4Reasons As List(Of DataSet_4Reasons) = AvailabilityOldDataSource.GetDataSet_4Reasons().FindAll(Function(m) m.MchName.ToUpper() = machineName.ToUpper())
            Log.Debug($"Machine: {machine} - ds4Reasons records: {ds4Reasons.Count}")

            Dim dsTimeline As List(Of DataSet_timeline) = AvailabilityOldDataSource.GetDataSet_Timeline().FindAll(Function(m)
                                                                                                                      count += 1
                                                                                                                      Return m.MchName.ToUpper() = machineName.ToUpper()
                                                                                                                  End Function)
            Log.Debug($"Machine: {machine} - dsTimeline records: {dsTimeline.Count}")

            Dim dsTimelineTmp As New List(Of DataSet_timeline)

            Dim originalCount = dsTimeline.Count

            e.DataSources.Add(New ReportDataSource("DataSet_data", dsData))
            e.DataSources.Add(New ReportDataSource("DataSet_4reasons", ds4Reasons.FindAll(Function(m) m.Relevance = 1).Take(4)))

            If (TypeDePeriode = "ww" Or TypeDePeriode = "m") Then
                e.DataSources.Add(New ReportDataSource("DataSet_History", dsHistory))
                e.DataSources.Add(New ReportDataSource("DataSet_data4", dsHistory.FindAll(Function(m) m.Relevance = 2)))
                e.DataSources.Add(New ReportDataSource("DataSet_4reasons4", ds4Reasons.FindAll(Function(m) m.Relevance = 2).Take(4)))
            Else
                e.DataSources.Add(New ReportDataSource("DataSet_timeLine", dsTimeline))
                e.DataSources.Add(New ReportDataSource("DataSet_History", dsHistoryDaily))
            End If

        Catch ex As Exception
            Log.Error(ex)
        End Try

        statusName_ = "SETUP"

        machineIndex = machineIndex + 1

    End Sub

    Public Function setMostPredominantStatus(table As DataTable) As DataTable

        For i As Integer = 0 To (StatusNameArray.Length / 2 - 1)
            For index = 0 To table.Rows.Count - 1
                If table.Rows(index).Item("MchName") = StatusNameArray(i, 0) Then
                    table.Rows(index).Item("StatusName") = StatusNameArray(i, 1)
                End If
            Next
        Next

        Return table

    End Function

    'Private Function setShiftBarChart(tblmachineName As String) As DataTable

    '    Dim TableName As String = "TimeLine"
    '    'Dim choosenDate As String = dateReporting.ToString()"yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))
    '    Dim startdate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '    Dim enddate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

    '    If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
    '        Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

    '            Dim query As String = "SELECT CASE WHEN (Status='_CON' or Status='CYCLE ON') then 'CYCLE ON'" +
    '            " when (Status='_COFF' or Status='CYCLE OFF')  then 'CYCLE OFF'" +
    '            " when (Status='_SETUP' or  Status='SETUP') THEN 'SETUP' " +
    '            " ELSE 'OTHER' END as ReasonName," +
    '            " case when shift=1 and status = 'SETUP' then 1 " +
    '            " when shift=1 and status <> 'SETUP' THEN 2 " +
    '            " when shift=2 and status = 'SETUP'  then 3 " +
    '            " when shift=2 and status <> 'SETUP' THEN 4 " +
    '            " when shift=3 and status = 'SETUP' then 5 " +
    '            " when shift=3 and status <> 'SETUP' then 6 end as Priority," +
    '            " shift, " +
    '            " sum(cycletime) as cycletime " +
    '            " from " + tblmachineName +
    '            " where status not like '_PART%' and Date_ between '" + startdate + "' and '" + enddate + "'" +
    '            " GROUP by ReasonName, shift, Priority order by Priority"


    '            Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)

    '            'JULIANDAY(date('" + String.Format("{0:yyyy-MM-dd}", dateReporting.ToString()"yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))) + "')) - julianday(date([time_]))=0
    '            Dim ds As DatasetReporting2 = New DatasetReporting2()

    '            Dim returnvalue As Integer = adap.Fill(ds, TableName)

    '            Return ds.TimeLine

    '        End Using

    '    Else
    '        Try
    '            Dim db_authPath As String = Nothing
    '            Dim directory As String = getRootPath()
    '            If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
    '                Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
    '                    db_authPath = reader.ReadLine()
    '                End Using
    '            End If
    '            Dim connectionString As String
    '            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

    '            If license = 0 Then
    '                connectionString = "DATABASE=csi_database;" + MySqlConnectionString
    '            End If

    '            Dim query As String = "SELECT CASE WHEN (Status='_CON' or Status='CYCLE ON') then 'CYCLE ON'" +
    '                    " when (Status='_COFF' or Status='CYCLE OFF')  then 'CYCLE OFF'" +
    '                    " when (Status='_SETUP' or  Status='SETUP') THEN 'SETUP' " +
    '                    " ELSE 'OTHER' END as ReasonName," +
    '                    " case when shift=1 and status = 'SETUP' then 1 " +
    '                    " when shift=1 and status <> 'SETUP' THEN 2 " +
    '                    " when shift=2 and status = 'SETUP'  then 3 " +
    '                    " when shift=2 and status <> 'SETUP' THEN 4 " +
    '                    " when shift=3 and status = 'SETUP' then 5 " +
    '                    " when shift=3 and status <> 'SETUP' then 6 end as Priority," +
    '                    " shift, " +
    '                    " sum(cycletime) as cycletime " +
    '                    " from " + tblmachineName +
    '                    " where status not like '_PART%' and Date_ between '" + startdate + "' and '" + enddate + "'" +
    '                    " GROUP by ReasonName, shift, Priority order by Priority"

    '            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

    '                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(query, sqlConn)

    '                Dim ds As DatasetReporting2 = New DatasetReporting2()

    '                Dim returnvalue As Integer = adap.Fill(ds, TableName)

    '                Return ds.TimeLine

    '            End Using

    '        Catch ex As Exception
    '            MessageBox.Show("Unable to generate bar chart")
    '        End Try
    '    End If

    'End Function

    'Private Function setTimeLine(tblmachineName As String, PeriodType As String) As DataTable

    '    Dim TableName As String = "TimeLine"
    '    Dim startdate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '    Dim enddate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

    '    If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
    '        Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)


    '            Dim query As String = "SELECT CASE" +
    '                    " WHEN (Status='_CON' or Status='CYCLE ON') then 'CYCLE ON'" +
    '                    " when (Status='_COFF' or Status='CYCLE OFF') then 'CYCLE OFF' " +
    '                    " when (Status='_SETUP' or  Status='SETUP') THEN 'SETUP'" +
    '                    " ELSE Status END as ReasonName, " +
    '                    " time_, shift, cycletime" +
    '                    " from " + tblmachineName +
    '                    " where status not like '_PART%' and time_ between '" + startdate + "' and '" + enddate + "'"

    '            Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)

    '            Dim ds As DatasetReporting2 = New DatasetReporting2()

    '            Dim returnvalue As Integer = adap.Fill(ds, TableName)

    '            Return ds.TimeLine

    '        End Using

    '    Else
    '        Try
    '            Dim db_authPath As String = Nothing
    '            Dim directory As String = getRootPath()
    '            If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
    '                Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
    '                    db_authPath = reader.ReadLine()
    '                End Using
    '            End If
    '            Dim connectionString As String
    '            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

    '            If license = 0 Then
    '                connectionString = "DATABASE=csi_database;" + MySqlConnectionString
    '            End If

    '            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

    '                Dim sqlCmd As Text.StringBuilder = New StringBuilder()
    '                Dim machTable = tblmachineName

    '                If PeriodType = "t" Then machTable = $"csi_machineperf.{ tblmachineName }"

    '                sqlCmd.Append($"SELECT                                                              ")
    '                sqlCmd.Append($"    CASE                                                            ")
    '                sqlCmd.Append($"    WHEN Status = '_CON'   OR Status = 'CYCLE ON'  THEN 'CYCLE ON'  ")
    '                sqlCmd.Append($"    WHEN Status = '_COFF'  OR Status = 'CYCLE OFF' THEN 'CYCLE OFF' ")
    '                sqlCmd.Append($"    WHEN Status = '_SETUP' OR Status = 'SETUP'     THEN 'SETUP'     ")
    '                sqlCmd.Append($"    ELSE Status END AS ReasonName   ,    ")
    '                If PeriodType = "t" Then
    '                    sqlCmd.Append($"    date AS time_               ,    ")
    '                Else
    '                    sqlCmd.Append($"    time_                       ,    ")
    '                End If
    '                sqlCmd.Append($"    shift                           ,    ")
    '                sqlCmd.Append($"    cycletime                            ")
    '                sqlCmd.Append($"FROM                                     ")
    '                sqlCmd.Append($"    {machTable}                          ")
    '                sqlCmd.Append($"WHERE                                    ")
    '                sqlCmd.Append($"    Status NOT LIKE '_PART%'  AND        ")

    '                If PeriodType = "t" Then
    '                    sqlCmd.Append($"        shift IN ({ reportShift })                     ")
    '                Else
    '                    sqlCmd.Append($"        date_ BETWEEN '{ startdate }' AND '{ enddate }'")
    '                End If

    '                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(sqlCmd.ToString(), sqlConn)

    '                Dim ds As DatasetReporting2 = New DatasetReporting2()

    '                Dim returnvalue As Integer = adap.Fill(ds, TableName)

    '                Return ds.TimeLine

    '            End Using

    '        Catch ex As Exception
    '            MessageBox.Show("Unable to set timeline")
    '        End Try
    '    End If
    'End Function

    'Private Function setMachineData(OnePeriod As Boolean, PeriodType As String, tblmachineName As String) As DataTable

    '    Dim tableName As String
    '    Dim originalMachine As String = RealNameMachine(tblmachineName)
    '    Dim StatusFont As Integer = 7

    '    originalMachine = originalMachine.Substring(4, originalMachine.Length - 4)

    '    Dim startDate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '    Dim endDate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

    '    If statusName_.Length > 20 Then
    '        StatusFont = 4
    '    ElseIf statusName_.Length > 7 Then
    '        StatusFont = 5
    '    End If

    '    If (OnePeriod = True) Then
    '        tableName = "Tbl_DataMachine"
    '    Else

    '        If PeriodType = "y" Then
    '            startDate = reportstartdate.AddDays(-4).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '            'ElseIf (PeriodType = "ww") Then
    '        ElseIf (PeriodType = "ww") Then
    '            startDate = reportstartdate.AddDays(-7 * 3).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '        ElseIf (PeriodType = "m") Then
    '            startDate = reportstartdate.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '        End If

    '        tableName = "Tbl_DataMachine4wk"

    '    End If

    '    If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then

    '        Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

    '            Dim adap As New SQLiteDataAdapter()

    '            Dim query As String = "SELECT shift, mchName, Sum(detailCycleTime) AS Totalcycletime, Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther,'" + statusName_ + "' as StatusName, '7pt' as StatusFont " +
    '       " FROM (" +
    '       " SELECT '" + originalMachine + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, " +
    '       " CASE WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime ELSE 0 END as COFF," +
    '       " CASE WHEN (status = '_CON' or status = 'CYCLE ON' ) THEN cycletime ELSE 0 END as CON, " +
    '       " CASE WHEN status = '_SETUP' or status = 'SETUP' THEN cycletime ELSE 0 END as SETUP, " +
    '       " CASE WHEN (Status<>'_CON' and status<>'CYCLE ON') " +
    '       " and (Status<>'_COFF' and Status<>'CYCLE OFF')  " +
    '       " and (Status<>'_SETUP' and  Status<>'SETUP')" +
    '       " THEN cycletime ELSE 0 END as OTHER, shift" +
    '       " from " + tblmachineName +
    '       " where date_ between '" + startDate + "' and '" + endDate + "') as setMachineData" +
    '       " group by shift"


    '            adap = New SQLiteDataAdapter(query, sqlConn)
    '            ' STRFTIME pas sur
    '            Dim ds As DatasetReporting2 = New DatasetReporting2()

    '            Dim returnvalue As Integer = adap.Fill(ds, tableName)


    '            If (OnePeriod = True) Then
    '                Return ds.Tbl_DataMachine
    '            Else
    '                Return ds.Tbl_DataMachine4wk
    '            End If

    '        End Using
    '    Else
    '        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '        Dim db_authPath As String = Nothing
    '        Dim directory As String = getRootPath()

    '        If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
    '            Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
    '                db_authPath = reader.ReadLine()
    '            End Using
    '        End If

    '        Dim connectionString As String

    '        connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

    '        If license = 0 Then
    '            connectionString = "DATABASE=csi_database;" + MySqlConnectionString
    '        End If

    '        Dim daysdiff As Integer = DateDiff(DateInterval.Day, reportstartdate, reportenddate)

    '        Dim sqlCmd As Text.StringBuilder = New StringBuilder()
    '        sqlCmd.Append($"SELECT                                      ")
    '        sqlCmd.Append($"    shift                                 , ")
    '        sqlCmd.Append($"    mchName                               , ")
    '        sqlCmd.Append($"    SUM(detailCycleTime) AS Totalcycletime, ")
    '        sqlCmd.Append($"    SUM(CON)             AS CycleOn       , ")
    '        sqlCmd.Append($"    SUM(COFF)            AS CycleOff      , ")
    '        sqlCmd.Append($"    SUM(SETUP)           AS SumSetup      , ")
    '        sqlCmd.Append($"    SUM(OTHER)           AS SumOther      , ")
    '        sqlCmd.Append($"    '{ statusName_ }'    AS StatusName    , ")
    '        sqlCmd.Append($"    '7pt'                AS StatusFont      ")
    '        sqlCmd.Append($"FROM (                                      ")
    '        sqlCmd.Append($"    SELECT                                  ")
    '        sqlCmd.Append($"         '{ originalMachine }' AS  mchName, ")
    '        sqlCmd.Append($"         cycletime as detailCycleTime     , ")

    '        If PeriodType = "t" Then
    '            sqlCmd.Append($"     date  as detailDate              , ")
    '        Else
    '            sqlCmd.Append($"     date_ as detailDate              , ")
    '        End If

    '        sqlCmd.Append($"         CASE WHEN (status =  '_COFF'  OR  status =  'CYCLE OFF') THEN cycletime ELSE 0 END as COFF , ")
    '        sqlCmd.Append($"         CASE WHEN (status =  '_CON'   OR  status =  'CYCLE ON' ) THEN cycletime ELSE 0 END as CON  , ")
    '        sqlCmd.Append($"         CASE WHEN (status =  '_SETUP' OR  status =  'SETUP'    ) THEN cycletime ELSE 0 END as SETUP, ")
    '        sqlCmd.Append($"         CASE WHEN (status <> '_CON'   AND status <> 'CYCLE ON'                                       ")
    '        sqlCmd.Append($"               AND  status <> '_COFF'  AND status <> 'CYCLE OFF'                                      ")
    '        sqlCmd.Append($"               AND  status <> '_SETUP' AND status <> 'SETUP'    ) THEN cycletime ELSE 0 END as OTHER, ")
    '        sqlCmd.Append($"         shift                              ")
    '        sqlCmd.Append($"    FROM                                    ")

    '        If PeriodType = "t" Then
    '            sqlCmd.Append($"        csi_machineperf.{ tblmachineName }             ")
    '            sqlCmd.Append($"    WHERE                                              ")
    '            sqlCmd.Append($"        shift IN ({ reportShift })                     ")
    '        Else
    '            sqlCmd.Append($"        csi_database.{ tblmachineName }                ")
    '            sqlCmd.Append($"    WHERE                                              ")
    '            sqlCmd.Append($"        DAYOFWEEK(date_) NOT IN (1,7) AND              ")
    '            sqlCmd.Append($"        date_ BETWEEN '{ startDate }' AND '{ endDate }'")
    '        End If

    '        sqlCmd.Append($"     ) AS setMachineData                    ")
    '        sqlCmd.Append($"GROUP BY shift")

    '        Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

    '            Dim adap As MySqlDataAdapter

    '            adap = New MySqlDataAdapter(sqlCmd.ToString(), sqlConn)

    '            Dim ds As DatasetReporting2 = New DatasetReporting2()

    '            Dim returnvalue As Integer = adap.Fill(ds, tableName)


    '            If (OnePeriod = True) Then
    '                Return ds.Tbl_DataMachine
    '            Else
    '                Return ds.Tbl_DataMachine4wk
    '            End If

    '        End Using

    '    End If

    'End Function

    'Private Function setPartNo(OnePeriod As Boolean, PeriodType As String, tblmachineName As String) As DataTable

    '    Dim tableName As String
    '    Dim startDate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '    Dim endDate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

    '    tableName = "tbl_partsNumber"
    '    If (OnePeriod = True) Then

    '    Else

    '        If PeriodType = "y" Then
    '            startDate = reportstartdate.AddDays(-4).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '            'ElseIf (PeriodType = "ww") Then
    '        ElseIf (PeriodType = "ww") Then
    '            startDate = reportstartdate.AddDays(-7 * 3).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '        ElseIf (PeriodType = "m") Then
    '            startDate = reportstartdate.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '        End If
    '    End If

    '    If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
    '        Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

    '            Dim query As String = "select '" + tblmachineName + "' as  mchName,  Status as partName, Shift,date_" +
    '                  " from " + tblmachineName +
    '                  " where Status like '_Partno%' " +
    '                  " and CycleTime=0 " +
    '                  " and Date_ between '" + startDate + "' and '" + endDate + "'" +
    '                  " GROUP BY  Shift, partName, date_" +
    '                  " LIMIT 10"

    '            Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)

    '            Dim ds As DatasetReporting2 = New DatasetReporting2()

    '            Dim returnvalue As Integer = adap.Fill(ds, tableName)

    '            If (OnePeriod = True) Then
    '                Return ds.tbl_partsNumber
    '            Else
    '                Return ds.tbl_partsNumber
    '            End If
    '        End Using
    '    Else
    '        Dim db_authPath As String = Nothing
    '        Dim directory As String = getRootPath()
    '        If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
    '            Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
    '                db_authPath = reader.ReadLine()
    '            End Using
    '        End If
    '        Dim connectionString As String
    '        connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

    '        If license = 0 Then
    '            connectionString = "DATABASE=csi_database;" + MySqlConnectionString
    '        End If

    '        Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

    '            Dim query As String = "select '" + tblmachineName + "' as  mchName,  Status as partName, Shift,date_" +
    '                        " from " + tblmachineName +
    '                        " where Status like '_Partno%' " +
    '                        " and CycleTime=0 " +
    '                        " and Date_ between '" + startDate + "' and '" + endDate + "'" +
    '                        " GROUP BY  Shift, partName, date_" +
    '                        " LIMIT 10"

    '            Dim adap As MySqlDataAdapter = New MySqlDataAdapter(query, sqlConn)

    '            Dim ds As DatasetReporting2 = New DatasetReporting2()

    '            Dim returnvalue As Integer = adap.Fill(ds, tableName)

    '            If (OnePeriod = True) Then
    '                Return ds.tbl_partsNumber
    '            Else
    '                Return ds.tbl_partsNumber
    '            End If
    '        End Using
    '    End If

    'End Function

    Public Function set4Reason(OnePeriod As Boolean, PeriodType As String, tblmachineName As String) As DataTable

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

            'Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

            '    Dim query As String = "select '" + originalMachine + "' as  mchName," +
            '                " status as ReasonName, " +
            '                " sum(cycletime) as CycleTime" +
            '                " from " + tblmachineName +
            '                " where ( Status<>'_CON' and Status<>'CYCLE ON'" +
            '                " and Status<>'_COFF' and Status<>'CYCLE OFF'" +
            '                " and Status<>'_SETUP' and  Status<>'SETUP'" +
            '                " and CycleTime >0" +
            '                " and Date_ between '" + startDate + "' and '" + endDate + "')" +
            '                " group by status" +
            '                " order by sum(cycletime) desc limit 4"

            '    Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)

            '    Dim ds As DatasetReporting2 = New DatasetReporting2()

            '    Dim returnvalue As Integer = adap.Fill(ds, tableName)

            '    If (OnePeriod = True) Then
            '        Return ds.Tbl_Top4Reason
            '    Else
            '        Return ds.Tbl_Top4Reason4wk
            '    End If
            'End Using

        Else
            'Dim db_authPath As String = Nothing
            'Dim directory As String = getRootPath()
            'If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
            '    Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
            '        db_authPath = reader.ReadLine()
            '    End Using
            'End If
            'Dim connectionString As String

            'connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

            'If license = 0 Then
            '    connectionString = "DATABASE=csi_database;" + MySqlConnectionString
            'End If

            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

                Dim sqlCmd As Text.StringBuilder = New StringBuilder()
                Dim machTable = tblmachineName

                If PeriodType = "t" Then machTable = $"csi_machineperf.{ tblmachineName }"

                sqlCmd.Append($"SELECT                                   ")
                sqlCmd.Append($"    '{ originalMachine }' AS mchName   , ")
                sqlCmd.Append($"    status                AS ReasonName, ")
                sqlCmd.Append($"    sum(cycletime)        AS CycleTime   ")
                sqlCmd.Append($"FROM                                     ")
                sqlCmd.Append($"    {machTable}                          ")
                sqlCmd.Append($"WHERE                                    ")
                sqlCmd.Append($"    Status <> '_CON'      AND            ")
                sqlCmd.Append($"    Status <> 'CYCLE ON'  AND            ")
                sqlCmd.Append($"    Status <> '_COFF'     AND            ")
                sqlCmd.Append($"    Status <> 'CYCLE OFF' AND            ")
                sqlCmd.Append($"    Status <> '_SETUP'    AND            ")
                sqlCmd.Append($"    Status <> 'SETUP'     AND            ")
                sqlCmd.Append($"    Cycletime > 0         AND            ")

                If PeriodType = "t" Then
                    sqlCmd.Append($"   shift IN ({ reportShift })                         ")
                Else
                    sqlCmd.Append($"   ShiftDate BETWEEN '{ startDate }' AND '{ endDate }'")
                End If

                sqlCmd.Append($"GROUP BY                                 ")
                sqlCmd.Append($"    status                               ")
                sqlCmd.Append($"ORDER BY                                 ")
                sqlCmd.Append($"    SUM(cycletime) DESC                  ")
                sqlCmd.Append($"    LIMIT 4                              ")

                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(sqlCmd.ToString(), sqlConn)

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

    'Private Function getPartNumber() As DataTable

    '    If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
    '        Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

    '            Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(
    '               "SELECT DISTINCT MchName FROM ( " + Qry_Tbl_MachineName + " ) tablee where (status NOT LIKE '_PARTN*') group by MchName ", sqlConn)

    '            Dim ds As DatasetReporting2 = New DatasetReporting2()
    '            Dim returnvalue As Integer = adap.Fill(ds, "tbl_MachineName")

    '            Return ds.Tbl_MachineName

    '        End Using
    '    Else
    '        Dim db_authPath As String = Nothing
    '        Dim directory As String = getRootPath()
    '        If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
    '            Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
    '                db_authPath = reader.ReadLine()
    '            End Using
    '        End If
    '        Dim connectionString As String
    '        connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

    '        If license = 0 Then
    '            connectionString = "DATABASE=csi_database;" + MySqlConnectionString
    '        End If


    '        Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

    '            Dim adap As MySqlDataAdapter = New MySqlDataAdapter(
    '               "SELECT DISTINCT MchName FROM ( " + Qry_Tbl_MachineName + " ) tablee where (status NOT LIKE '_PARTN*') group by MchName ", sqlConn)


    '            Dim ds As DatasetReporting2 = New DatasetReporting2()

    '            Dim returnvalue As Integer = adap.Fill(ds, "tbl_MachineName")

    '            Return ds.Tbl_MachineName

    '        End Using
    '    End If

    'End Function

    Private Function setMachineName() As DataTable

        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
            'Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)
            '    Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(Qry_Tbl_RenameMachine, sqlConn)

            '    Dim ds As DatasetReporting2 = New DatasetReporting2()

            '    Dim returnvalue As Integer = adap.Fill(ds, "tbl_MachineName")

            '    Return setMostPredominantStatus(ds.Tbl_MachineName)

            'End Using
        Else

            'Dim db_authPath As String = Nothing
            'Dim directory As String = getRootPath()

            'If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
            '    Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
            '        db_authPath = reader.ReadLine()
            '    End Using
            'End If

            'Dim connectionString As String

            'connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

            'If license = 0 Then
            '    connectionString = "DATABASE=csi_database;" + MySqlConnectionString
            'End If

            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString) 'db corriger

                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(Qry_Tbl_RenameMachine, sqlConn)
                Dim ds As DatasetReporting2 = New DatasetReporting2()
                Dim returnvalue As Integer = adap.Fill(ds, "tbl_MachineName")

                Return setMostPredominantStatus(ds.Tbl_MachineName)

            End Using

        End If

    End Function

    'Private Function setHistoryDaily(PeriodType As String, tblmachineName As String) As DataTable

    '    Dim tableName As String

    '    Dim startDate As String = reportstartdate.AddDays(-13).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '    Dim endDate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

    '    tableName = "Tbl_History18Daily"
    '    If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
    '        Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

    '            Dim query As String = "SELECT  mchName, date(detailDate) as  WeekNumber,  Sum(detailCycleTime) AS Totalcycletime, Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther " +
    '                   " FROM (select '" + tblmachineName + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, " +
    '                   "       CASE WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime ELSE 0 END as COFF," +
    '                   "       CASE WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
    '                   "       CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP," +
    '                   "       CASE WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
    '                   "       and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
    '                   "       and (Status<>'_SETUP' and status<>'SETUP') " +
    '                   "       THEN cycletime ELSE 0 END as OTHER " +
    '                   "       from " + tblmachineName +
    '                   "       where  date_ between  '" + startDate + "' and  '" + endDate + "' ) as tbl  " +
    '                   " GROUP BY  mchName, strftime('%d', detailDate), detailDate" +
    '                   " order by detailDate"


    '            Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)

    '            Dim ds As DatasetReporting2 = New DatasetReporting2()

    '            Dim returnvalue As Integer = adap.Fill(ds, tableName)

    '            Return ds.Tbl_History18Weekly

    '        End Using
    '    Else
    '        Dim db_authPath As String = Nothing
    '        Dim directory As String = getRootPath()
    '        If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
    '            Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
    '                db_authPath = reader.ReadLine()
    '            End Using
    '        End If
    '        Dim connectionString As String
    '        connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

    '        If license = 0 Then
    '            connectionString = "DATABASE=csi_database;" + MySqlConnectionString
    '        End If

    '        Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)

    '            Dim query As String = "SELECT  mchName, date(detailDate) as  WeekNumber,  Sum(detailCycleTime) AS Totalcycletime, Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther " +
    '                    " FROM (select '" + tblmachineName + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, " +
    '                    "       CASE WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime ELSE 0 END as COFF," +
    '                    "       CASE WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
    '                    "       CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP," +
    '                    "       CASE WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
    '                    "       and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
    '                    "       and (Status<>'_SETUP' and status<>'SETUP') " +
    '                    "       THEN cycletime ELSE 0 END as OTHER " +
    '                    "       from " + tblmachineName +
    '                    "       where  date_ between  '" + startDate + "' and  '" + endDate + "' ) as tbl  " +
    '                    " GROUP BY  mchName, WeekNumber " +
    '                    " order by WeekNumber"

    '            Dim adap As MySqlDataAdapter = New MySqlDataAdapter(query, sqlConn)

    '            Dim ds As DatasetReporting2 = New DatasetReporting2()

    '            Dim returnvalue As Integer = adap.Fill(ds, tableName)

    '            Return ds.Tbl_History18Daily

    '        End Using
    '    End If

    'End Function

    'Public Function IntToFirst(integ As Integer, param As DateTime) As DateTime

    '    Dim ret As DateTime = New DateTime(Year(param), integ, 1)
    '    Return ret

    'End Function

    '    Private Function setHistoryWeekly(tblmachineName As String) As DataTable
    '        Dim tableName As String

    '        Dim startDate As String = reportstartdate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
    '        Dim endDate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

    '        startDate = reportstartdate.AddDays(-7 * 17).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

    '        tableName = "Tbl_History18Weekly"
    '        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
    '            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

    '                Dim query As String = "SELECT  mchName," +
    '                     " date(detailDate) as weeknumber," +
    '                     " Sum(detailCycleTime) AS Totalcycletime," +
    '                     " Sum(CON) as CycleOn," +
    '                     " Sum(COFF) as CycleOff," +
    '                     " Sum(SETUP) as SumSetup," +
    '                     " Sum(OTHER) as SumOther " +
    '                     " FROM" +
    '                     " (select '" + tblmachineName + "' as  mchName," +
    '                     " cycletime as detailCycleTime, " +
    '                     " date_ as detailDate, " +
    '                     " CASE  WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime  ELSE 0 END as COFF," +
    '                     " CASE  WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
    '                     " CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP, " +
    '                     " CASE " +
    '                     " WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
    '                     " and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
    '                     " and (Status<>'_SETUP' and status<>'SETUP') THEN cycletime " +
    '                     " ELSE 0 END as OTHER " +
    '                     " from " + tblmachineName +
    '                     " where date_ between '" + startDate + "' and '" + endDate + "'" +
    '                     " ) as tbl" +
    '                     " GROUP BY  strftime('%W' ,detailDate)  - case strftime('%w' ,detailDate) when 1 then 1 else 0 end, weeknumber  " +
    '                     " order by weeknumber"
    '                Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)


    '                Dim ds As DatasetReporting2 = New DatasetReporting2()

    '                Dim returnvalue As Integer = adap.Fill(ds, tableName)

    '                Return ds.Tbl_History18Weekly

    '            End Using
    '        Else
    '            Dim db_authPath As String = Nothing
    '            Dim directory As String = getRootPath()
    '            If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
    '                Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
    '                    db_authPath = reader.ReadLine()
    '                End Using
    '            End If
    '            Dim connectionString As String
    '            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

    '            If license = 0 Then
    '                connectionString = "DATABASE=csi_database;" + MySqlConnectionString
    '            End If

    '            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)
    '                'New Query Old Query is Commented below this query in #if false block
    '                Dim query As String = "SELECT  MchName," +
    '                        " year(detailDate) as YearNumber," +
    '                        " month(detailDate) as MonthNumber," +
    '                        " Week(detailDate) as WeekNumber," +
    '                        " Sum(CON) as CycleOn," +
    '                        " Sum(COFF) as CycleOff," +
    '                        " Sum(SETUP) as SumSetup," +
    '                        " Sum(OTHER) as SumOther ," +
    '                         " Sum(detailCycleTime) AS Totalcycletime" +
    '                        " FROM" +
    '                        " (select '" + tblmachineName + "' as  MchName," +
    '                        " cycletime as detailCycleTime, " +
    '                        " date_ as detailDate, " +
    '                        " CASE  WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime  ELSE 0 END as COFF," +
    '                        " CASE  WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
    '                        " CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP, " +
    '                        " CASE " +
    '                        " WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
    '                        " and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
    '                        " and (Status<>'_SETUP' and status<>'SETUP') THEN cycletime " +
    '                        " ELSE 0 END as OTHER " +
    '                        " from " + tblmachineName +
    '                        " where date_ between '" + startDate + "' and '" + endDate + "'" +
    '                        " ) as tbl" +
    '                        " GROUP BY WeekNumber" +
    '                        " order by WeekNumber"
    '#If False Then
    '                'Old Querry Which works but not have the perfect columns maching to the report's dataset's columns name
    'Dim query As String = "SELECT  mchName," +
    '                        " adddate(date(detailDate), INTERVAL 1-DAYOFWEEK(date(detailDate)) DAY) as WeekStart," +
    '                        " Sum(detailCycleTime) AS Totalcycletime," +
    '                        " Sum(CON) as CycleOn," +
    '                        " Sum(COFF) as CycleOff," +
    '                        " Sum(SETUP) as SumSetup," +
    '                        " Sum(OTHER) as SumOther " +
    '                        " FROM" +
    '                        " (select '" + tblmachineName + "' as  mchName," +
    '                        " cycletime as detailCycleTime, " +
    '                        " date_ as detailDate, " +
    '                        " CASE  WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime  ELSE 0 END as COFF," +
    '                        " CASE  WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
    '                        " CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP, " +
    '                        " CASE " +
    '                        " WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
    '                        " and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
    '                        " and (Status<>'_SETUP' and status<>'SETUP') THEN cycletime " +
    '                        " ELSE 0 END as OTHER " +
    '                        " from " + tblmachineName +
    '                        " where date_ between '" + startDate + "' and '" + endDate + "'" +
    '                        " ) as tbl" +
    '                        " GROUP BY WeekStart" +
    '                        " order by WeekStart"
    '#End If

    '                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(query, sqlConn)

    '                Dim ds As DatasetReporting2 = New DatasetReporting2()

    '                Dim returnvalue As Integer = adap.Fill(ds, tableName)

    '                Return ds.Tbl_History18Weekly

    '            End Using
    '        End If

    '    End Function

    'Private Function setHistoryMonthly(tblmachineName As String) As DataTable
    '    Dim tableName As String

    '    Dim startDate As String = reportstartdate.AddMonths(-17).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

    '    Dim endDate As String = reportenddate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))

    '    'BetweenStr = "0 and 17"
    '    tableName = "Tbl_History18Monthly"

    '    If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then

    '        Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

    '            Dim query As String = "SELECT  mchName," +
    '                 " date(detailDate) as weeknumber," +
    '                 " Sum(detailCycleTime) AS Totalcycletime," +
    '                 " Sum(CON) as CycleOn," +
    '                 " Sum(COFF) as CycleOff," +
    '                 " Sum(SETUP) as SumSetup," +
    '                 " Sum(OTHER) as SumOther " +
    '                 " FROM" +
    '                 " (select '" + tblmachineName + "' as  mchName," +
    '                 " cycletime as detailCycleTime, " +
    '                 " date_ as detailDate, " +
    '                 " CASE  WHEN (status = '_COFF' or status = 'CYCLE OFF') THEN cycletime  ELSE 0 END as COFF," +
    '                 " CASE  WHEN (status = '_CON' or status = 'CYCLE ON') THEN cycletime ELSE 0 END as CON," +
    '                 " CASE WHEN (status = '_SETUP' or status = 'SETUP') THEN cycletime ELSE 0 END as SETUP, " +
    '                 " CASE " +
    '                 " WHEN (Status<>'_CON' and Status<>'CYCLE ON' ) " +
    '                 " and (Status<>'_COFF' and Status<>'CYCLE OFF') " +
    '                 " and (Status<>'_SETUP' and status<>'SETUP') THEN cycletime " +
    '                 " ELSE 0 END as OTHER " +
    '                 " from " + tblmachineName +
    '                 " where date_ between '" + startDate + "' and '" + endDate + "'" +
    '                 " ) as tbl" +
    '                 " GROUP BY  strftime('%W' ,detailDate)  - case strftime('%w' ,detailDate) when 1 then 1 else 0 end, weeknumber  " +
    '                 " order by weeknumber"
    '            Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(query, sqlConn)


    '            Dim ds As DatasetReporting2 = New DatasetReporting2()

    '            Dim returnvalue As Integer = adap.Fill(ds, tableName)

    '            Return ds.Tbl_History18Monthly

    '        End Using
    '    Else

    '        Dim sqlCmd = New Text.StringBuilder()
    '        sqlCmd.Append($"")
    '        sqlCmd.Append($"SELECT                                         ")
    '        sqlCmd.Append($"    mchName                                  , ")
    '        sqlCmd.Append($"    yearnumber                               , ")
    '        sqlCmd.Append($"    monthnumber                              , ")
    '        sqlCmd.Append($"    DATE_FORMAT(detaildate, '%b %y') as xaxis, ")
    '        sqlCmd.Append($"    Sum(detailCycleTime) As Totalcycletime   , ")
    '        sqlCmd.Append($"    Sum(CON)             As CycleOn          , ")
    '        sqlCmd.Append($"    Sum(COFF)            As CycleOff         , ")
    '        sqlCmd.Append($"    Sum(SETUP)           As SumSetup         , ")
    '        sqlCmd.Append($"    Sum(OTHER)           As SumOther           ")
    '        sqlCmd.Append($"FROM                                           ")
    '        sqlCmd.Append($"	(                                          ")
    '        sqlCmd.Append($"		SELECT                                 ")
    '        sqlCmd.Append($"            '{tblmachineName}' as  mchName   , ")
    '        sqlCmd.Append($"            cycletime  AS detailCycleTime    , ")
    '        sqlCmd.Append($"            date_      AS detailDate         , ")
    '        sqlCmd.Append($"            year_      AS yearnumber         , ")
    '        sqlCmd.Append($"            month_     AS monthnumber        , ")
    '        sqlCmd.Append($"            CASE WHEN (status =  '_COFF'   OR status =  'CYCLE OFF' ) THEN cycletime ELSE 0 END AS COFF  , ")
    '        sqlCmd.Append($"            CASE WHEN (status =  '_CON'    OR status =  'CYCLE ON'  ) THEN cycletime ELSE 0 END AS CON   , ")
    '        sqlCmd.Append($"            CASE WHEN (status =  '_SETUP'  OR status =  'SETUP'     ) THEN cycletime ELSE 0 END AS SETUP , ")
    '        sqlCmd.Append($"            CASE WHEN (status <> '_CON'   AND status <> 'CYCLE ON'  ) AND  ")
    '        sqlCmd.Append($"                      (status <> '_COFF'  AND status <> 'CYCLE OFF' ) AND  ")
    '        sqlCmd.Append($"                      (Status <> '_SETUP' AND status <> 'SETUP'     ) THEN cycletime ELSE 0 END AS OTHER ")
    '        sqlCmd.Append($"		FROM                                            ")
    '        sqlCmd.Append($"            csi_database.{tblmachineName}               ")
    '        sqlCmd.Append($"        WHERE                                           ")
    '        sqlCmd.Append($"            date_ BETWEEN '{startDate}' AND '{endDate}' ")
    '        sqlCmd.Append($"	) AS tbl                                            ")
    '        sqlCmd.Append($"GROUP BY                                                ")
    '        sqlCmd.Append($"    yearnumber,                                         ")
    '        sqlCmd.Append($"    monthnumber                                         ")
    '        sqlCmd.Append($"ORDER BY                                                ")
    '        sqlCmd.Append($"    yearnumber,                                         ")
    '        sqlCmd.Append($"    monthnumber                                         ")

    '        Return MySqlAccess.GetDataTable(sqlCmd.ToString())

    '    End If

    'End Function

#End Region

    Public Shared rpn_result As RpnOperand
    Public Shared Current_selected_ As New List(Of String)

#Region "adv mtconnect"

    'Public Structure MCH_
    '    Dim Conditions_expression As Dictionary(Of String, String)
    '    Dim name As String
    '    Public ip As String
    '    Dim delay As Integer
    '    Dim partno As String
    '    Dim spov As String
    '    Dim frov As String

    '    Dim rpn_result_intern As RpnOperand
    '    Dim Current_selected_ As List(Of String)
    'End Structure

    Public MCHS_ As New Dictionary(Of String, MCH_)
    Public Secondary_values As New Dictionary(Of String, String)

    'Public Sub write_delays(MCH As MCH_) ' STATUS As String) ', dic As Dictionary(Of String, Integer))

    '    Using wrtr As StreamWriter = New StreamWriter(serverRootPath & "\sys\Conditions\" + MCH.MachineName + "\delays.csys")
    '        For Each KEY In MCH.delays
    '            wrtr.WriteLine(KEY + ":" + MCH.delays(KEY))
    '        Next
    '        wrtr.Close()
    '    End Using

    'End Sub

    Public Sub read_other_settings(ByRef MCH As MCH_)

        Try

            Dim conditionsTable As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_mtcfocasconditions WHERE ConnectorId = { MCH.ConnectorId } AND NOT StatusDisabled;")

            Dim NoOfMachines = conditionsTable.Rows.Count

            If NoOfMachines > 0 Then
                '""""""""""""Read all CSI conditions """"""""""""""""""""""""""""""'

                MCHS_.Item(MCH.MachineId).Conditions_expression_.Clear()
                MCHS_.Item(MCH.MachineId).DoNotUpdate = True
                MCH.ClearDelays()

                For Each row As DataRow In conditionsTable.Rows

                    Dim status As String = row("Status").ToString()
                    Dim condition As String = row("Condition").ToString()

                    If Not String.IsNullOrEmpty(status) Then

                        Try
                            'Add Delays For Each Status
                            MCH.delays(status) = row("delay").ToString()
                        Catch ex As Exception
                            Log.Error("Error while Adding Delays For Each Status in Function read_delays()", ex)
                        End Try

                        Try
                            'Adds Status and Conditions
                            MCHS_.Item(MCH.MachineId).Conditions_expression_.Add(status, condition)
                            If Not MCHS_.Item(MCH.MachineId).CsdOnSetup Then
                                MCHS_.Item(MCH.MachineId).CsdOnSetup = row("CsdOnSetup")
                            End If
                            'If Not MCHS_.Item(MCH.MachineId).COnDuringSetup Then
                            '    MCHS_.Item(MCH.MachineId).COnDuringSetup = row("COnDuringSetup")
                            'End If

                            'finding all parts of the conditions split by parentheses 
                            Dim matches As MatchCollection = Regex.Matches(condition, "\(([^)]*)\)")

                            If matches IsNot Nothing And matches.Count > 0 Then

                                For Each match As Match In matches
                                    Try
                                        If match.Success And match.Groups.Count > 1 Then

                                            Dim entry As String = match.Groups(1).Value.Replace("(", "").Replace(")", "").Replace("!", "").Replace(">", "=").Replace("<", "=")
                                            Dim param = entry.Substring(0, entry.IndexOf("=") + 1)

                                            If Not String.IsNullOrEmpty(param) And Not MCHS_.Item(MCH.MachineId).Current_selected_.Any(Function(e) e.StartsWith(param)) Then
                                                MCHS_.Item(MCH.MachineId).Current_selected_.Add(entry)
                                            End If
                                        End If

                                    Catch ex As Exception
                                        Log.Error($"Error while processing match condition. { MCH.MachineName } - { status }", ex)
                                    End Try
                                Next
                            Else
                                'Log.Error("Cannot find match for the following condition : " & condition)
                            End If

                        Catch ex As Exception
                            Log.Error("Error while Adding Status and Conditions in Function read_delays()", ex)
                        End Try

                    End If
                Next

                MCHS_.Item(MCH.MachineId).DoNotUpdate = False

            End If

            Dim sql = $"SELECT * FROM csi_auth.view_csiothersettings WHERE ConnectorId = { MCH.ConnectorId };"

            Dim otherSettingsTable As DataTable = MySqlAccess.GetDataTable(sql)

            Log.Debug($"*-*-*-*-*-*-*>> Machine: {MCH.MachineId}, SQL: {sql}, Rows: {otherSettingsTable.Rows.Count}")

            For Each settingrow As DataRow In otherSettingsTable.Rows
                MCHS_.Item(MCH.MachineId).PartNumber_Variable = settingrow("PartNumber_Variable").ToString()
                'MCHS_.Item(MCH.MachineId).PartNumber_Value = settingrow("PartNumber_Value").ToString()

                MCHS_.Item(MCH.MachineId).PartNumber_Prefix1 = settingrow("PartNumber_Prefix1").ToString()
                MCHS_.Item(MCH.MachineId).PartNumber_Filter1Start = settingrow("PartNumber_Filter1Start").ToString()
                MCHS_.Item(MCH.MachineId).PartNumber_Filter1End = settingrow("PartNumber_Filter1End").ToString()
                MCHS_.Item(MCH.MachineId).PartNumber_Filter2Apply = settingrow("PartNumber_Filter2Apply")
                MCHS_.Item(MCH.MachineId).PartNumber_Prefix2 = settingrow("PartNumber_Prefix2").ToString()
                MCHS_.Item(MCH.MachineId).PartNumber_Filter2Start = settingrow("PartNumber_Filter2Start").ToString()
                MCHS_.Item(MCH.MachineId).PartNumber_Filter2End = settingrow("PartNumber_Filter2End").ToString()
                MCHS_.Item(MCH.MachineId).PartNumber_Filter3Apply = settingrow("PartNumber_Filter3Apply")
                MCHS_.Item(MCH.MachineId).PartNumber_Prefix3 = settingrow("PartNumber_Prefix3").ToString()
                MCHS_.Item(MCH.MachineId).PartNumber_Filter3Start = settingrow("PartNumber_Filter3Start").ToString()
                MCHS_.Item(MCH.MachineId).PartNumber_Filter3End = settingrow("PartNumber_Filter3End").ToString()

                'MCHS_.Item(MCH.MachineId).Operation_Value = settingrow("Operation_Value").ToString()
                MCHS_.Item(MCH.MachineId).Operation_Available = settingrow("Operation_Available")
                MCHS_.Item(MCH.MachineId).Operation_FilterStart = settingrow("Operation_FilterStart").ToString()
                MCHS_.Item(MCH.MachineId).Operation_FilterEnd = settingrow("Operation_FilterEnd").ToString()

                MCHS_.Item(MCH.MachineId).FeedRate_Variable = settingrow("FeedRate_Variable").ToString()
                'MCHS_.Item(MCH.MachineId).FeedRate_Value = settingrow("FeedRate_Value").ToString()
                MCHS_.Item(MCH.MachineId).Min_Fover = settingrow("Feedrate_MIN").ToString()
                MCHS_.Item(MCH.MachineId).Max_Fover = settingrow("Feedrate_MAX").ToString()

                MCHS_.Item(MCH.MachineId).Spindle_Variable = settingrow("Spindle_Variable").ToString()
                'MCHS_.Item(MCH.MachineId).Spindle_Value = settingrow("Spindle_Value").ToString()
                MCHS_.Item(MCH.MachineId).Min_Sover = settingrow("Spindle_MIN").ToString()
                MCHS_.Item(MCH.MachineId).Max_Sover = settingrow("Spindle_MAX").ToString()

                MCHS_.Item(MCH.MachineId).Rapid_Variable = settingrow("Rapid_Variable").ToString()
                'MCHS_.Item(MCH.MachineId).Rapid_Value = settingrow("Rapid_Value").ToString()
                MCHS_.Item(MCH.MachineId).Min_Rover = settingrow("Rapid_MIN").ToString()
                MCHS_.Item(MCH.MachineId).Max_Rover = settingrow("Rapid_MAX").ToString()

                MCHS_.Item(MCH.MachineId).progno = settingrow("ProgramNumber_Variable").ToString()
                'MCHS_.Item(MCH.MachineId).progno_val = settingrow("ProgramNumber_Value").ToString()
                MCHS_.Item(MCH.MachineId).PartCount_Variable = settingrow("PartCount_Variable").ToString()
                MCHS_.Item(MCH.MachineId).RequiredParts_Variable = settingrow("PartRequired_Variable").ToString()
                MCHS_.Item(MCH.MachineId).PRN_startwith = settingrow("ProgramNumber_FilterStart").ToString()
                MCHS_.Item(MCH.MachineId).PRN_endwith = settingrow("ProgramNumber_FilterEnd").ToString()
                MCHS_.Item(MCH.MachineId).Pallet = settingrow("ActivePallet_Var").ToString()
                MCHS_.Item(MCH.MachineId).Pallet_val = settingrow("ActivePallet_Value").ToString()
                MCHS_.Item(MCH.MachineId).Pallet_startwith = settingrow("ActivePallet_StartWith").ToString()
                MCHS_.Item(MCH.MachineId).Pallet_endwith = settingrow("ActivePallet_EndWith").ToString()
                MCHS_.Item(MCH.MachineId).EnableMCS = settingrow("EnableMCS")
                MCHS_.Item(MCH.MachineId).SaveDataRaw = settingrow("SaveDataRaw")
                MCHS_.Item(MCH.MachineId).COnDuringSetup = settingrow("COnDuringSetup")
                MCHS_.Item(MCH.MachineId).SaveDataRawProdOnly = (settingrow("SaveProdOnly") > 0)
                MCHS_.Item(MCH.MachineId).SaveDataRawSetup = (settingrow("SaveProdOnly") > 1)
                MCHS_.Item(MCH.MachineId).WarningPressure = settingrow("WarningPressure").ToString()
                MCHS_.Item(MCH.MachineId).CriticalPressure = settingrow("CriticalPressure").ToString()
                MCHS_.Item(MCH.MachineId).WarningTemperature = settingrow("WarningTemperature").ToString()
                MCHS_.Item(MCH.MachineId).CriticalTemperature = settingrow("CriticalTemperature").ToString()
                MCHS_.Item(MCH.MachineId).MCSDelay = settingrow("MCSDelay").ToString()
                MCHS_.Item(MCH.MachineId).DelayForCycleOff = settingrow("DelayForCycleOff").ToString()
                MCHS_.Item(MCH.MachineId).MonitBoardStatus.Delay = settingrow("MCSDelay").ToString()
                MCHS_.Item(MCH.MachineId).MonitBoardStatus.StartCriticalStop = Nothing

                Log.Debug($"*-*-*-*-*-*-*>> Machine: {MCH.MachineId}, FOvr: {MCHS_.Item(MCH.MachineId).FeedRate_Variable}, SOvr: {MCHS_.Item(MCH.MachineId).Spindle_Variable}, ROvr: {MCHS_.Item(MCH.MachineId).Rapid_Variable}")

            Next

        Catch ex As Exception

            Log.Error("Error in reading Delay Value from table csi_auth.tbl_mtcfocasconditions.", ex)

        End Try

    End Sub

    Public Sub readsetup_for_adv_mtc()

        Dim ConnectorId As Integer
        Dim MachineId As Integer
        Dim MachineName As String = ""
        Dim MTCMachineName As String = ""
        Dim IP_Address As String = ""
        Dim ConnectorType As String = ""

        Try
            Try
                Dim dTable_SelectAllcsiconnector As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_csiconnector ;")
                Dim NoOfMachines = dTable_SelectAllcsiconnector.Rows.Count

                If dTable_SelectAllcsiconnector.Rows.Count > 0 Then

                    Log.Debug(dTable_SelectAllcsiconnector.Rows.Count)

                    '""""""""""""Read all CSI Connector Rows and Put that Data into MCHS_ """"""""""""""""""""""""""""""'
                    For Each row As DataRow In dTable_SelectAllcsiconnector.Rows

                        'Write Code Here 
                        ConnectorId = row("Id")
                        MachineId = row("MachineId")
                        MachineName = row("eNETMachineName").ToString() 'Before This was MachineName coming from Serverside but we need to give here eNETMachinename which are enabled with FTP connection
                        MTCMachineName = row("MTCMachine").ToString() 'Before This was MachineName coming from Serverside but we need to give here eNETMachinename which are enabled with FTP connection
                        ConnectorType = row("ConnectorType").ToString()

                        IP_Address = "http://" & row("AgentIP").ToString() & ":" & row("AgentPort").ToString()

                        If MCHS_.ContainsKey(MachineId) Then
                        Else
                            'get eNETMachinename from Database and Replace it with machinename so all the parameters will be set to enetmachine name instead of MTConnect/Focas MAchine
                            'This is old Code :::
                            MCHS_.Add(MachineId, New MCH_(ConnectorId, MachineId, MachineName, MTCMachineName, serverRootPath, IP_Address))
                            Log.Debug($"MCHS_.Add - {ConnectorId}, {MachineId}, {MachineName}")
                        End If

                        SyncLock fetchConditionsLock
                            Log.Debug($"MCHS_.0 - {ConnectorId}, {MachineId}, {MachineName}")
                            read_other_settings(MCHS_(MachineId))
                        End SyncLock

                        Log.Debug($"MCHS_.1 - {ConnectorId}, {MachineId}, {MachineName}")

                        'Notifications setup
                        MCHS_.Item(MachineId).DATA_SET_Notifications.Tables.Clear()
                        MCHS_.Item(MachineId).ConnectorType = ConnectorType
                        MCHS_.Item(MachineId).MonitoringBoardId = row("MonitoringUnitId")

                        If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "notif_stat.xml") Then
                            Dim xmlFile As XmlReader
                            xmlFile = XmlReader.Create(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "notif_stat.xml")

                            Dim tbl_notif_status As New DataTable("notif_stat")
                            tbl_notif_status.Columns.Add("Activate")
                            tbl_notif_status.Columns.Add("Status")
                            tbl_notif_status.Columns.Add("Delay")
                            tbl_notif_status.ReadXml(xmlFile)
                            xmlFile.Close()
                            MCHS_.Item(MachineId).DATA_SET_Notifications.Tables.Add(tbl_notif_status)
                        End If

                        Log.Debug($"MCHS_.2 - {ConnectorId}, {MachineId}, {MachineName}")

                        If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "notif_cond.xml") Then

                            Dim xmlFile As XmlReader
                            xmlFile = XmlReader.Create(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "notif_cond.xml")

                            Dim tbl_notif_COND As New DataTable("notif_cond")
                            tbl_notif_COND.Columns.Add("Condition")
                            tbl_notif_COND.Columns.Add("Warning")
                            tbl_notif_COND.Columns.Add("Fault (Alarm)")
                            tbl_notif_COND.Columns.Add("Delay")

                            tbl_notif_COND.Clear()

                            tbl_notif_COND.ReadXml(xmlFile)
                            xmlFile.Close()

                            MCHS_.Item(MachineId).DATA_SET_Notifications.Tables.Add(tbl_notif_COND)

                        End If

                        Log.Debug($"MCHS_.3 - {ConnectorId}, {MachineId}, {MachineName}")

                        If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\Sendto.csys") Then

                            Using r As StreamReader = New StreamReader(serverRootPath + "\sys\Conditions\" & MachineName & "\Sendto.csys")
                                Dim t As New DataTable
                                t.Columns.Add()
                                t.Columns.Add()

                                While Not r.EndOfStream
                                    Dim l() As String = r.ReadLine.Split("|")
                                    If l.Count = 2 Then
                                        t.Rows.Add(l(0), l(1))
                                    End If

                                End While

                                r.Close()
                                MCHS_.Item(MachineId).sendtolist = t
                            End Using

                        End If

                        Log.Debug($"MCHS_.4 - {ConnectorId}, {MachineId}, {MachineName}")
                    Next

                End If

            Catch ex As Exception

                Log.Error(ex)
            End Try

        Catch ex As Exception

            MCHS_.Item(MachineId).DoNotUpdate = False

            Log.Error("Could not read mtc setup", ex)
        End Try
    End Sub

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

    'will be used later
    'Private Function find_indexs(expression As String, what As String) As Integer()
    '    Dim index_(3) As Integer
    '    index_(0) = expression.IndexOf(what)
    '    index_(1) = 0
    '    index_(2) = 0
    '    Dim NOTfound As Boolean = True
    '    'previous "("
    '    While NOTfound
    '        If expression(index_(0)) = "(" Then
    '            index_(1) = index_(0)
    '            NOTfound = False
    '        End If

    '        index_(0) -= 1
    '    End While
    '    NOTfound = True
    '    'next")"
    '    While NOTfound
    '        If expression(index_(0)) = ")" Then
    '            index_(2) = index_(0)
    '            NOTfound = False
    '        End If

    '        index_(0) += 1
    '    End While

    '    Return index_
    'End Function

    Public Sub Evaluate_logic(expression As String, ByRef MCH__ As MCH_)

        Try
            rpn_result = Nothing
            MCH__.rpn_result_intern_ = Nothing
            '-------------------------------------------------------------------------------------------

            If expression <> "" Then

                expression = interpret_expression(expression)

                Dim Parser_ As ITokeniser = New MathParser()
                Dim tokens As List(Of RpnElement) = Parser.ParseExpression(Parser_, expression)
                Dim ndx As Integer = 0
                Dim current_value As New List(Of String)

                If MCH__.MachineName = "" Then

                    Dim BufferList_ As List(Of String) = Current_selected_

                    For Each element As String In BufferList_
                        If Not current_value.Contains(element) Then current_value.Add(element)
                    Next

                Else

                    Dim BufferList_ As List(Of String) = MCH__.Current_selected_

                    For Each element As String In BufferList_
                        If Not current_value.Contains(element) Then current_value.Add(element)
                    Next

                End If

                Dim rpn As New Rpn_function.Rpn

                If MCH__.MachineName = "" Then
                    rpn_result = rpn.RpnEval(tokens, current_value)
                Else
                    MCH__.rpn_result_intern_ = rpn.RpnEval(tokens, current_value)
                End If

            End If

        Catch ex As Exception
            'Log.Error("Err while evaluation logic exression : " & ex.Message & vbCrLf & " " & ex.StackTrace & vbCrLf & "details : logic expression was : " & expression & "for machine : " & MCH__.MachineName & vbCrLf, 1)
        End Try

    End Sub

    Public Sub Update_current_selected(value_ As String, data As String, MCH__ As MCH_)

        Dim Buffer_list As List(Of String) = New List(Of String)

        Buffer_list = MCH__.Current_selected_.ToList()

        If value_ = "PartNumber" Or value_ = "Operation" Or value_ = "spov" Or value_ = "frov" Or value_ = "rapidov" Then

            MCH__.update_other(value_, data)

        Else
            If MCH__.DoNotUpdate = False Then

                For Each item_ As String In Buffer_list.ToList

                    If item_.StartsWith(value_) Then
                        Buffer_list.RemoveAt(Buffer_list.IndexOf(item_))
                        Buffer_list.Add(value_ & " = " & data.Trim())
                        GoTo updated
                    End If
                Next

                Buffer_list.Add(value_ & " = " & data.Trim())

            End If
updated:
            MCH__.Current_selected_ = Buffer_list
        End If
    End Sub

#End Region

    Public MySQL_activepath As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)

    Public MySQLd_PROC_info As New ProcessStartInfo(MySQL_activepath & "\CSI Flex Server\mysql\mysql-5.7.21-win32\bin\mysqld.exe") 'MSI PAth

    Public MySQLd_PROC As Process

    Public Function start_mysqld() As Boolean

        Try
            Log.Info("Starting MySQL service.")

            init_mysqld()

            MySqlAccess.SetConnectingString(MySqlConnectionString)
            Return MySqlAccess.hasConnection()

        Catch ex As Exception

            Log.Error("Could not start MySQL.", ex)

            Return False
        End Try

    End Function

    ''' <summary>
    ''' ///// Funtion to start MySQL Server
    ''' </summary>
    ''' <returns></returns>
    ''' 
    Private Function init_mysqld() As Boolean

        Try

            Dim MySqlService As String = ""

            If ServiceTools.ServiceInstaller.ServiceIsInstalled("MySQL") Then
                MySqlService = "MySQL"
            ElseIf ServiceTools.ServiceInstaller.ServiceIsInstalled("MYSQLSERVICE") Then
                MySqlService = "MYSQLSERVICE"
            End If

            If MySqlService <> "" Then

                If ServiceTools.ServiceInstaller.GetServiceStatus(MySqlService) = ServiceTools.ServiceState.Run Then
                    Log.Info($"MySQL Service ( { MySqlService } ) is running.")
                Else
                    FocasLibrary.Tools.AdapterManagement.StartMysql(MySqlService)
                    Log.Info($"MySQL Service ( { MySqlService } ) was started.")
                End If

                Return True
            Else

                MySqlService = "MYSQLSERVICE"
                Dim MysqlEXEPath = ""

                If Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\mysql\mysql-8.0.18-winx64\Data\") Then

                    MysqlEXEPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\mysql\mysql-8.0.18-winx64\bin\mysqld.exe"

                ElseIf Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\mysql\mysql-5.7.21-win32\Data\") Then

                    MysqlEXEPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\mysql\mysql-5.7.21-win32\bin\mysqld.exe"

                End If

                If Not String.IsNullOrEmpty(MysqlEXEPath) Then

                    FocasLibrary.Tools.AdapterManagement.InstallMysql(MysqlEXEPath, MySqlService)

                    If Not (ServiceTools.ServiceInstaller.GetServiceStatus(MySqlService) = ServiceTools.ServiceState.Run) Then
                        FocasLibrary.Tools.AdapterManagement.StartMysql(MySqlService)
                    End If

                    Log.Info("MySQL service installed and started.")
                    Return True

                End If

                Log.Error("It was not possible to install and start the MySql service.")
                Return False

            End If

        Catch ex As Exception

            Log.Error("Error trying to start the MySql service.", ex)
            Return False

        End Try

    End Function

    'Private Function execute_mysqld_process(MySQLd_PROC As Process) As Boolean

    '    Try

    '        Dim MySqlService As String = ""

    '        If ServiceTools.ServiceInstaller.ServiceIsInstalled("MySQL") Then
    '            MySqlService = "MySQL"
    '        ElseIf ServiceTools.ServiceInstaller.ServiceIsInstalled("MYSQLSERVICE") Then
    '            MySqlService = "MYSQLSERVICE"
    '        End If

    '        Dim cmd As String = $"net start {MySqlService}"
    '        Dim info = New ProcessStartInfo()

    '        info.FileName = "cmd"
    '        info.Arguments = "cmd /c" & cmd
    '        info.UseShellExecute = True
    '        info.CreateNoWindow = True
    '        info.WindowStyle = ProcessWindowStyle.Hidden
    '        info.Verb = "runas"

    '        MySQLd_PROC.StartInfo = info
    '        MySQLd_PROC.Start()
    '        MySQLd_PROC.WaitForExit()

    '        Dim sc As ServiceController
    '        sc = New ServiceController(MySqlService)
    '        sc.WaitForStatus(ServiceControllerStatus.Running)

    '        Dim output As String = ""

    '        If MySQLd_PROC Is Nothing Then

    '            Log.Error("MySqld start fail. MySQLd_PROC is nothing.")
    '            Return False
    '        Else

    '            Log.Info("mysqld start success.")
    '            Return True
    '        End If

    '    Catch ex As Exception

    '        Log.Error("Could not exec Mysql", ex)
    '        Return False

    '    End Try

    'End Function

    Public Shared Function GetEmailSettingsFromDB() As EmailSettings
        Dim retVal As EmailSettings = Nothing
        Dim cnt As MySqlConnection = New MySqlConnection(CSI_Library.MySqlConnectionString)
        Try
            cnt.Open()

            Dim mysqlcmd As New MySqlCommand("Select * from CSI_auth.tbl_emailreports WHERE isused = 1", cnt)
            Dim mysqladapter As New MySqlDataAdapter(mysqlcmd)
            Dim results As New DataTable
            mysqladapter.Fill(results)
            Dim defaultIndex As Integer = 0
            If (results.Rows.Count > 0) Then
                retVal = BuildEmailSetting(results.Rows(0))
            End If

        Catch ex As Exception
            Log.Error("Unable to load reports email in use.", ex)
        Finally
            cnt.Close()
        End Try
        Return retVal
    End Function

    Public Shared Function BuildEmailSetting(row As DataRow) As EmailSettings
        Dim retVal As EmailSettings = Nothing
        If row IsNot Nothing Then
            retVal = New EmailSettings
            retVal.SenderEmail = row("senderemail").ToString()
            retVal.EncryptedPassword = row("senderpwd").ToString()
            retVal.SmtpHost = row("smtphost").ToString()
            retVal.SmtpPort = Convert.ToInt32(row("smtpport").ToString())
            retVal.RequireCred = Convert.ToBoolean(row("requirecred"))
            retVal.IsDefault = Convert.ToBoolean(row("isdefault"))
            retVal.IsUsed = Convert.ToBoolean(row("isused"))
            retVal.UseSSL = Convert.ToBoolean(row("usessl"))
        End If
        Return retVal
    End Function

End Class


'Public Class table_filler

'    Public machinename As String
'    Public data As DataTable
'    Public csi_lib As CSI_Library
'    Public filler As Thread

'    Sub New(machineName_ As String, datatab As DataTable)
'        machinename = machineName_
'        data = datatab

'        ' filler = New Thread()

'    End Sub

'End Class

Public Class MCH_
    Shared CSI_LIB As New CSI_Library(True)
    Public current_stream As DataStream
    Public devices

    Public Conditions_expression_ As New Dictionary(Of String, String)

    Private Property connectorId_ As Integer
    Private Property machineId_ As Integer
    Private Property name_ As String = ""
    Private Property ip_ As String = ""
    Private Property mtcMachine_ As String = ""
    ' Private Property delay_ As Integer = 0
    Private Property partNumberVariable As String = ""
    Private Property partNumberValue As String = ""
    Private Property operationValue As String = ""
    Private Property operatorRefId As String = ""
    Private Property progno_ As String = ""
    Private Property progno_value As String = ""
    Private Property Pallet_ As String = ""
    Private Property Pallet_value As String = ""

    Private Property feedRateVariable As String = ""
    Private Property feedRateValue As String = ""
    Private Property Min_Fover_ As String = ""
    Private Property Max_Fover_ As String = ""
    Private Property spindleVariable As String = ""
    Private Property spindleValue As String = ""
    Private Property Min_Sover_ As String = ""
    Private Property Max_Sover_ As String = ""
    Private Property rapidVariable As String = ""
    Private Property rapidValue As String = ""
    Private Property Min_Rover_ As String = ""
    Private Property Max_Rover_ As String = ""
    Private Property ConnectorType_ As String = ""

    Private Property requiredPartsVariable As String = ""
    Private Property requiredPartsValue As Integer = 0

    Private Property partCountVariable As String = ""
    Private Property partCountValue As Integer = 0

    Private Shared Property serverRootPath As String = ""

    Public rpn_result__ As RpnOperand
    Public Current_selected_ As New List(Of String) ' values of var used for the status
    Public DoNotUpdate As Boolean = False
    Public current_other As New Dictionary(Of String, String) ' values of f/r ov, s/p ov and partno

    Private Property delays_ As New Dictionary(Of String, Integer)  ' status - delay
    Private Property Timers_ As New Dictionary(Of String, Timers.Timer) 'status - timer
    ' Public flags_ As New Dictionary(Of String, Boolean) ' status - flag : to know if a timer has been strated or not =/= elapsed
    Private Property tbl_sendto As New DataTable("sendto")

    Public Property DATA_SET_Notifications As New DataSet("notif")
    Private Property Current_cond_ As ICondition()
    Private Property Previous_cond_ As ICondition()


    Public Property MonitBoardStatus As MonitoringBoardStatus

    Enum flag_state
        NOTactivated = 0
        activated = 1
        undefined = 2
    End Enum

    Public flags_ As New Dictionary(Of String, Boolean) ' status - flag : to know if a timer has been strated or not =/= elapsed

    'Private Property MTConnectStatus As String
    Private Property currentstatus_ As String = ""
    'Private Property CheckTimer As Threading.Timer

    Private Property Check_machine_timer As System.Timers.Timer
    Private Property loadSensorsTimer As System.Timers.Timer

    Private Property dicSensorsPallet As Dictionary(Of String, String)

    'Private Property mch_delays_ As Dictionary(Of String, Dictionary(Of String, Integer)) ' mch - delays dic

    Public Sub New(connectorId As Integer, machineId As Integer, name As String, mtcMachine As String, serverRootPath As String, ip As String)

        connectorId_ = connectorId
        machineId_ = machineId
        name_ = name
        mtcMachine_ = mtcMachine
        ip_ = ip

        '  delay_ = 0
        partNumberVariable = ""
        partNumberValue = ""
        operationValue = ""
        operatorRefId = ""
        spindleVariable = ""
        spindleValue = ""
        feedRateVariable = ""
        feedRateValue = ""
        rapidVariable = ""
        rapidValue = ""
        progno_ = ""
        Pallet_ = ""
        Min_Fover = ""
        Max_Fover = ""
        Min_Rover = ""
        Max_Rover = ""
        Min_Sover = ""
        Max_Sover = ""
        ConnectorType = ""
        requiredPartsVariable = ""
        requiredPartsValue = 0
        partCountVariable = ""
        partCountValue = 0
        serverRootPath = serverRootPath
        Sensors = New List(Of Monitoring)()
        MonitBoardStatus = New MonitoringBoardStatus()
        lastRecord_ = ""

        'choices = read_choices_ondisk()

        ReloadConditions()
        entity_client = New EntityClient(ip)
        entity_client.RequestTimeout = 2000

        'CheckTimer = New Threading.Timer(AddressOf CheckMachine)
        'CheckTimer.Change(3000, 1000)

        Check_machine_timer = New System.Timers.Timer(1000) '5 minutes actually every 1 minute 
        AddHandler Check_machine_timer.Elapsed, AddressOf check_machine_timer_Ticked
        Check_machine_timer.AutoReset = False
        Check_machine_timer.Start()

        dicSensorsPallet = New Dictionary(Of String, String)()
        LoadDicSensors()

        loadSensorsTimer = New Timers.Timer(60000)
        AddHandler loadSensorsTimer.Elapsed, AddressOf LoadDicSensors
        loadSensorsTimer.AutoReset = False
        loadSensorsTimer.Start()

    End Sub

    Private Sub check_machine_timer_Ticked()
        Try
            Check_machine_timer.Stop()
            CheckMachine()
        Catch ex As Exception
            Log.Error("General error while updating ip.", ex)
        Finally
            Check_machine_timer.Start()
        End Try
    End Sub

    Private Sub LoadDicSensors()

        Dim dtSensors = MySqlAccess.GetDataTable("SELECT * FROM monitoring.sensors WHERE NOT Deleted;")

        For Each row As DataRow In dtSensors.Rows

            Dim pallet = row("Group")
            Dim name = row("Name")

            If dicSensorsPallet.ContainsKey(name) Then
                dicSensorsPallet(name) = pallet
            Else
                dicSensorsPallet.Add(name, pallet)
            End If
        Next

    End Sub

    Private CheckingMachine As Boolean = False
    Public unreachable As Boolean
    Public entity_client As EntityClient

    Private Sub CheckMachine()
        Try
            'Current_selected_.Clear()
            'Chcek All Parameters from xml and load it to list of list is empty 
            If CheckingMachine = False And IP <> "" Then

                CheckingMachine = True

                Dim crt As ISample
                'Dim m_client As EntityClient = New EntityClient(IP)

                Dim Current_stream As DataStream = entity_client.Current()
                'devices = entity_client.Probe()

                If Current_stream IsNot Nothing Then

                    Dim mbStatus As MonitoringBoardStatus = New MonitoringBoardStatus()
                    mbStatus.IsAvalable = False
                    mbStatus.MachineId = machineId_
                    mbStatus.BoardId = MonitoringBoardId

                    If MonitoringBoardId > 0 Then

                        Dim MUCurrentXml = MonitoringBoardsService.GetCurrentXml(MonitoringBoardId)

                        Current_stream.AddMonitoringUnits(MUCurrentXml)

                        Dim MUDevice = Current_stream.DeviceStreams(Current_stream.DeviceStreams.Count - 1)
                        Dim isAvailable As Boolean
                        Dim isMonitoring As Boolean
                        Dim isOverride As Boolean
                        Dim isAlarming As Boolean
                        Dim isCSD As Boolean
                        Dim palletId As String = ""

                        'Sensors.Clear()

                        For Each component As ComponentStream In MUDevice.ComponentStreams

                            If component.ComponentType = "Device" Then

                                Dim events = component.Events

                                If component.Events.Any(Function(e) e.DataItemID = "avail") Then isAvailable = component.Events.FirstOrDefault(Function(e) e.DataItemID = "avail").Value = "AVAILABLE"
                                If component.Events.Any(Function(e) e.DataItemID = "monitor") Then isMonitoring = component.Events.FirstOrDefault(Function(e) e.DataItemID = "monitor").Value = "ON" And isAvailable
                                If component.Events.Any(Function(e) e.DataItemID = "monover") Then isOverride = component.Events.FirstOrDefault(Function(e) e.DataItemID = "monover").Value = "ON"
                                If component.Events.Any(Function(e) e.DataItemID = "pltid") Then palletId = component.Events.FirstOrDefault(Function(e) e.DataItemID = "pltid").Value

                                If component.Conditions.Any(Function(e) e.DataItemID = "alarm") Then isAlarming = Not component.Conditions.FirstOrDefault(Function(e) e.DataItemID = "alarm").Value = "Normal"
                                If component.Conditions.Any(Function(e) e.DataItemID = "csd") Then isCSD = Not component.Conditions.FirstOrDefault(Function(e) e.DataItemID = "csd").Value = "Normal"

                                mbStatus.MachineId = MachineId
                                mbStatus.IsAvalable = isAvailable
                                mbStatus.CurrentPallet = palletId
                                mbStatus.IsMonitoring = isMonitoring
                                mbStatus.IsOverride = isOverride
                                mbStatus.IsAlarming = isAlarming
                                mbStatus.IsCSD = isCSD

                                Try
                                    If isMonitoring And (String.IsNullOrEmpty(palletId) Or palletId = "UNAVAILABLE") Then
                                        Log.Info($"---> Machine {MachineName}, Pallet NULL: Found {component.Events.Any(Function(e) e.DataItemID = "pltid")} / {component.Events.FirstOrDefault(Function(e) e.DataItemID = "pltid").Value}")
                                    End If
                                Catch ex As Exception
                                    Log.Info($"---> Pallet NOT Found")
                                End Try

                            ElseIf component.ComponentType = "Sensor" Then

                                Dim sensorStatus As SensorStatus = New SensorStatus()
                                Dim sensorIsAvailable = component.Events.FirstOrDefault(Function(e) e.Type = "Availability").Value = "AVAILABLE"

                                sensorStatus.IsAvailable = sensorIsAvailable
                                sensorStatus.SensorId = component.ID
                                sensorStatus.SensorName = component.Name
                                sensorStatus.SensorLabel = dicSensorsPallet(sensorStatus.SensorName)

                                If Not sensorIsAvailable Then
                                    Log.Info($"SENSOR NOT AVAILABLE: Machine {MachineName}, Sensor: {sensorStatus.SensorLabel} - {sensorStatus.SensorName}")
                                End If

                                For Each sensor In component.Samples

                                    If sensorStatus.IsAvailable Then
                                        Select Case sensor.Type.ToUpper()
                                            Case "PRESSURE"
                                                sensorStatus.Pressure = sensor.Value
                                                sensorStatus.PressureTimeStamp = sensor.Timestamp.ToLocalTime()
                                            Case "TEMPERATURE"
                                                sensorStatus.Temperature = sensor.Value
                                                sensorStatus.TemperatureTimeStamp = sensor.Timestamp.ToLocalTime()
                                            Case "POWERFACTOR"
                                                sensorStatus.Battery = sensor.Value
                                        End Select
                                    End If

                                    Dim mchSensor = Sensors.FirstOrDefault(Function(s) s.SensorName = component.Name And s.SensorType = sensor.Type)
                                    Dim isConnected = True

                                    If component.Events.Count > 0 Then
                                        isConnected = component.Events(0).Value.ToString().ToUpper() = "AVAILABLE"
                                    End If

                                    'If isConnected Then
                                    '    Log.Info($"CONNECTED: Device: {MUDevice.Name}, Events: {component.Events.Count}, Event 0: {IIf(component.Events.Count > 0, component.Events(0).ToString(), "")}")
                                    'Else
                                    '    Log.Info($"NOT CONNECTED: Device: {MUDevice.Name}, Events: {component.Events.Count}, Event 0: {IIf(component.Events.Count > 0, component.Events(0).ToString(), "")}")
                                    'End If

                                    mbStatus.CurrentPallet = palletId
                                    mbStatus.WarningPressure = WarningPressure
                                    mbStatus.CriticalPressure = CriticalPressure
                                    mbStatus.Delay = MCSDelay

                                    'If dicSensorsPallet(component.Name) = palletId Then
                                    '    mbStatus.CurrentPressure = sensor.Value
                                    'End If

                                    If mchSensor IsNot Nothing Then

                                        mchSensor.Value = sensor.Value
                                        mchSensor.Timestamp = sensor.Timestamp.ToLocalTime()
                                        mchSensor.IsAvailable = sensorStatus.IsAvailable
                                        mchSensor.IsConnected = isConnected
                                        mchSensor.IsAlarming = isAlarming
                                        mchSensor.IsCSD = isCSD
                                        mchSensor.IsMonitoring = isMonitoring
                                        mchSensor.IsOverride = isOverride
                                        mchSensor.CurrentPallet = palletId
                                        mchSensor.SensorPallet = dicSensorsPallet(component.Name)

                                    Else
                                        Dim monitoring As Monitoring = New Monitoring()

                                        monitoring.DeviceName = MUDevice.Name
                                        monitoring.SensorId = component.ID
                                        monitoring.SensorName = component.Name
                                        monitoring.SensorType = sensor.Type
                                        monitoring.Value = sensor.Value
                                        monitoring.Timestamp = sensor.Timestamp.ToLocalTime()
                                        monitoring.IsAvailable = sensorStatus.IsAvailable
                                        monitoring.IsConnected = isConnected
                                        monitoring.IsAlarming = isAlarming
                                        monitoring.IsCSD = isCSD
                                        monitoring.IsMonitoring = isMonitoring
                                        monitoring.IsOverride = isOverride
                                        monitoring.CurrentPallet = palletId
                                        monitoring.SensorPallet = dicSensorsPallet(component.Name)

                                        Sensors.Add(monitoring)
                                    End If
                                Next

                                If mbStatus.SensorsStatus.Any(Function(s) s.SensorId = sensorStatus.SensorId) Then
                                    sensorStatus.CopyPropertiesTo(Of SensorStatus)(mbStatus.SensorsStatus.First(Function(s) s.SensorId = sensorStatus.SensorId))
                                Else
                                    mbStatus.SensorsStatus.Add(sensorStatus)
                                End If

                            End If
                        Next

                        Try
                            If MonitoringBoardsService.MonitoringBoardsStatus Is Nothing Then
                                Log.Info($"MonitoringBoardsService.monitoringBoardsStatus is NULL")
                                MonitoringBoardsService.MonitoringBoardsStatus = New List(Of MonitoringBoardStatus)()
                            End If

                            If MonitoringBoardsService.MonitoringBoardsStatus.Any(Function(b) b.BoardId = mbStatus.BoardId) Then

                                Dim item = MonitoringBoardsService.MonitoringBoardsStatus.First(Function(b) b.BoardId = mbStatus.BoardId)

                                mbStatus.ActivatedCriticalStop = item.ActivatedCriticalStop
                                mbStatus.StartedDelay = item.StartedDelay
                                mbStatus.StartCriticalStop = item.StartCriticalStop
                                mbStatus.CopyPropertiesTo(Of MonitoringBoardStatus)(item)

                                For Each sensor As SensorStatus In mbStatus.SensorsStatus
                                    Dim itemSensor = item.SensorsStatus.FirstOrDefault(Function(s) s.SensorId = sensor.SensorId)

                                    If itemSensor IsNot Nothing Then
                                        sensor.CopyPropertiesTo(Of SensorStatus)(itemSensor)
                                    Else
                                        item.SensorsStatus.Add(sensor)
                                    End If
                                Next

                            Else

                                MonitoringBoardsService.MonitoringBoardsStatus.Add(mbStatus)

                            End If

                            Log.Debug($"MonitoringBoardsService.monitoringBoardsStatus.add({mbStatus.MachineId}, {mbStatus.BoardId})")

                        Catch ex As Exception
                            Log.Error($"MonitoringBoardsService: ", ex)
                        End Try

                        If isMonitoring And Sensors.Count = 0 Then
                            Log.Error("No Sensors found.")
                        End If
                    End If

                    timestamp_ = Current_stream.CreateTime

                    unreachable = False
                    'partno
                    Update_other_if_found(Current_stream, "PartNumber", PartNumber_Variable)

                    Update_other_if_found(Current_stream, "Operation", PartNumber_Variable)
                    'progno 
                    Update_other_if_found(Current_stream, "ProgramNumber", progno)

                    Update_other_if_found(Current_stream, "PartCountVariable", PartCount_Variable)

                    Update_other_if_found(Current_stream, "RequiredPartsVariable", RequiredParts_Variable)
                    'spov
                    Update_other_if_found(Current_stream, "spov", Spindle_Variable)
                    'rapidov
                    Update_other_if_found(Current_stream, "rapidov", Rapid_Variable)
                    'frov
                    Update_other_if_found(Current_stream, "frov", FeedRate_Variable)

                    Update_other_if_found(Current_stream, "pallet", Pallet)

                    Update_other_if_found(Current_stream, "monitor", "monitor")

                    Update_other_if_found(Current_stream, "monover", "monover")

                    Update_other_if_found(Current_stream, "csd", "csd")

                    Dim newValue As String = ""

                    If Current_selected_.Any() Then

                        Dim tempCollection = Current_selected_.ToList

                        For Each element As String In tempCollection

                            Dim str() As String = element.Split(" = ")
                            Dim ID = str(0)
                            newValue = ""

                            If Not String.IsNullOrEmpty(ID) Then

                                crt = Current_stream.GetSample(ID)

                                If crt Is Nothing Then

                                    Dim evt As IDataElement = Current_stream.AllEvents.FirstOrDefault(Function(e) e.DataItemID = ID)
                                    If evt IsNot Nothing Then
                                        newValue = evt.Value
                                    Else
                                        Dim cond As IDataElement = Current_stream.AllConditions.FirstOrDefault(Function(e) e.DataItemID = ID)
                                        If cond IsNot Nothing Then
                                            newValue = cond.Value
                                        Else
                                            Dim sample As IDataElement = Current_stream.AllSamples.FirstOrDefault(Function(e) e.DataItemID = ID)
                                            If sample IsNot Nothing Then
                                                newValue = sample.Value
                                            End If
                                        End If
                                    End If
                                Else
                                    newValue = crt.Value
                                End If

                                If Not String.IsNullOrEmpty(newValue) Then

                                    Dim index = Current_selected_.IndexOf(element)

                                    If index > -1 Then
                                        Current_selected_(index) = $"{ ID } = { newValue.Trim() }"
                                    End If

                                End If

                            End If
                        Next
                    End If
                    'notification
                    Current_cond = Current_stream.AllConditions
                Else
                    unreachable = True
                End If

            End If

        Catch ex As Exception
            Log.Error($"Error checking the machine { MachineName }", ex)
        Finally
            CheckingMachine = False
        End Try

    End Sub

    Private Sub ReloadConditions()

        Dim ConnectorId = connectorId_

        Try

            Dim conditionsTable As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_mtcfocasconditions WHERE ConnectorId = { ConnectorId };")

            If conditionsTable.Rows.Count > 0 Then

                For Each rows As DataRow In conditionsTable.Rows

                    Dim condition = rows("Condition").ToString()
                    Dim matches As MatchCollection = Regex.Matches(condition, "\(([^)]*)\)")

                    If matches IsNot Nothing And matches.Count > 0 Then

                        For Each match As Match In matches

                            If match.Success And match.Groups.Count > 1 Then

                                Dim entry As String = match.Groups(1).Value.Replace("(", "").Replace(")", "").Replace("!", "").Replace(">", "=").Replace("<", "=")
                                Dim param = entry.Substring(0, entry.IndexOf("=") + 1)

                                If Not String.IsNullOrEmpty(param) And Not Current_selected_.Any(Function(e) e.StartsWith(param)) Then
                                    Current_selected_.Add(param)
                                End If
                            End If
                        Next
                    Else
                        Log.Warn($"CheckMachine: {ConnectorId} ({rows("Status").ToString()}) - Error cannot find match for the following condition {condition}")
                    End If

                Next

            End If

        Catch ex As Exception

            Log.Error("Error While Loading All Elements from Database : ", ex)

        End Try

    End Sub

    Private Sub Update_other_if_found(stream As DataStream, parm As String, value As String)

        Dim crt As ISample

        If Not String.IsNullOrEmpty(value) Then

            crt = stream.GetSample(value)

            If crt Is Nothing Then

                Dim evt As IDataElement = stream.AllEvents.FirstOrDefault(Function(a) a.DataItemID = value)

                If evt IsNot Nothing Then
                    update_other(parm, evt.Value)
                Else
                    Dim cond As IDataElement = stream.AllConditions.FirstOrDefault(Function(a) a.DataItemID = value)
                    If cond IsNot Nothing Then
                        update_other(parm, cond.Value)
                    Else
                        Dim sample As IDataElement = stream.AllSamples.FirstOrDefault(Function(e) e.DataItemID = value)
                        If sample IsNot Nothing Then
                            update_other(parm, sample.Value)
                        End If

                    End If
                End If
            Else
                'AF! always true
                'If partno = partno Then
                update_other(parm, crt.Value)
                'End If
            End If
        End If
    End Sub

    'Private Sub RefreshServiceSettings()
    '    Try
    '        Dim portT As New DataTable
    '        Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select port From csi_database.tbl_rm_port;", CSI_Library.MySqlConnectionString)
    '        dadapter_name.Fill(portT)

    '        Dim request As WebRequest

    '        If portT.Rows.Count <> 0 Then
    '            If IsDBNull(portT.Rows(0)("port")) Then
    '                'LR! This code need to be updated so the port is not fixed. It need to use the port from the webserver configuration.
    '                request = WebRequest.Create("http://127.0.0.1:8008/readconfig")
    '            Else
    '                request = WebRequest.Create("http://127.0.0.1:" & portT.Rows(0)("port") & "/readMTCconfig")
    '            End If
    '        End If

    '        request.Method = "POST"
    '        Dim postData As String = ""
    '        Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
    '        ' Set the ContentType property of the WebRequest.
    '        request.ContentType = "application/x-www-form-urlencoded"
    '        ' Set the ContentLength property of the WebRequest.
    '        request.ContentLength = byteArray.Length
    '        ' Get the request stream.
    '        Dim dataStream As Stream = request.GetRequestStream()
    '        ' Write the data to the request stream.
    '        dataStream.Write(byteArray, 0, byteArray.Length)
    '        ' Close the Stream object.

    '        dataStream.Close()
    '        ' Get the response.

    '    Catch ex As Exception
    '        MsgBox("Could not apply these settings, please restart the CSIFlex service.")
    '        CSI_LIB.Log.Error("Unable to ask for refresh service settings.", ex)
    '    End Try
    'End Sub

    '    Private Sub Update_current_selected(value_ As String, data As String)

    '        'LR! Check this logic. I beleive that if data is Unavailable it should exit not continue
    '        If data = "Unavailable" Then
    '            unreachable = True
    '        Else

    '            Dim Buffer_list As List(Of String) = New List(Of String)
    '            Dim Buffer_list2 As List(Of String) = New List(Of String)

    '            Buffer_list = Current_selected_.ToList()
    '            Buffer_list2 = Current_selected_.ToList()

    '            Try
    '                If data <> "" And DoNotUpdate = False Then

    '                    If value_ = "PartNumber" Or value_ = "Operation" Or value_ = "spov" Or value_ = "frov" Or value_ = "rapidov" Then
    '                        update_other(value_, data)
    '                    Else
    '                        For Each item_ As String In Buffer_list

    '                            'Next 2 lines are not stupide !
    '                            Dim index As Integer = Buffer_list.IndexOf(value_ & " = ")

    '                            If index <> -1 Then
    '                                Buffer_list2.RemoveAt(index)
    '                                Buffer_list2.Add(value_ & " = " & data)
    '                                GoTo updated
    '                            Else
    '                                If item_.StartsWith(value_) Then
    '                                    Buffer_list2.RemoveAt(Buffer_list2.IndexOf(item_))
    '                                    Buffer_list2.Add(value_ & " = " & data)
    '                                    GoTo updated
    '                                End If
    '                            End If

    '                        Next

    '                        Buffer_list2.Add(value_ & " = " & data)
    'updated:
    '                        Current_selected_ = Buffer_list2

    '                    End If
    '                End If
    '            Catch ex As Exception
    '                '    Log.Error("cannot get current values for mtconnect: " & ex.Message & vbCrLf & " " & ex.StackTrace & vbCrLf, 1)
    '            End Try
    '        End If
    '    End Sub

    '    Private Sub Update_current_selectedOLD(value_ As String, data As String)

    '        'LR! Check this logic. I beleive that if data is Unavailable it should exit not continue
    '        If data = "Unavailable" Then
    '            unreachable = True
    '        Else
    '            Dim Buffer_list As List(Of String) = New List(Of String)
    '            Dim Buffer_list2 As List(Of String) = New List(Of String)
    '            Buffer_list = Current_selected_.ToList()
    '            Buffer_list2 = Current_selected_.ToList()
    '            Try
    '                If data <> "" And DoNotUpdate = False Then
    '                    If value_ = "PartNumber" Or value_ = "Operation" Or value_ = "spov" Or value_ = "frov" Or value_ = "rapidov" Then
    '                        update_other(value_, data)
    '                    Else
    '                        For Each item_ As String In Buffer_list
    '                            'Next 2 lines are not stupide !
    '                            Dim index As Integer = Buffer_list.IndexOf(value_ & " = ")
    '                            If index <> -1 Then
    '                                Buffer_list2.RemoveAt(index)
    '                                Buffer_list2.Add(value_ & " = " & data)
    '                                GoTo updated
    '                            Else
    '                                If item_.StartsWith(value_) Then
    '                                    Buffer_list2.RemoveAt(Buffer_list2.IndexOf(item_))
    '                                    Buffer_list2.Add(value_ & " = " & data)
    '                                    GoTo updated
    '                                End If
    '                            End If
    '                        Next
    '                        Buffer_list2.Add(value_ & " = " & data)
    'updated:
    '                        Current_selected_ = Buffer_list2
    '                    End If
    '                End If
    '            Catch ex As Exception


    '                '    Log.Error("cannot get current values for mtconnect: " & ex.Message & vbCrLf & " " & ex.StackTrace & vbCrLf, 1)
    '            End Try
    '        End If
    '    End Sub

    Public Property ConnectorId() As Integer
        Get
            Return connectorId_
        End Get
        Set(ByVal value As Integer)
            connectorId_ = value
        End Set
    End Property

    Public Property MachineId() As Integer
        Get
            Return machineId_
        End Get
        Set(ByVal value As Integer)
            machineId_ = value
        End Set
    End Property

    Property MachineName As String
        Get
            Return name_
        End Get
        Set(value As String)
            name_ = value
        End Set
    End Property

    Property MTCMachineName As String
        Get
            Return mtcMachine_
        End Get
        Set(value As String)
            mtcMachine_ = value
        End Set
    End Property

    Property sendtolist As DataTable
        Get
            Return tbl_sendto
        End Get
        Set(value As DataTable)
            tbl_sendto = value
        End Set
    End Property

    Property current() As String
        Get
            Return currentstatus_
        End Get

        Set(value As String)

            If currentstatus_.Equals(value) Then Return


            Dim delay = 0

            If delays_.ContainsKey(value) Then delay = delays_(value)

            If value = "CYCLE OFF" Then delay = DelayForCycleOff

            If delay > 0 Then

                If flag(value) = True Then

                    If check_timer(value) Then

                        currentstatus_ = value
                        'flag(value) = False

                        stop_timer(value)
                        ClearFlags()
                    Else
                        Log.Info($"*=*=*=* { machineId_ } - Waiting delay ")
                    End If
                Else

                    stop_timer(value)
                    ClearFlags()

                    start_timer(value, delay)

                End If
            Else
                currentstatus_ = value
                ClearFlags()
            End If

        End Set

    End Property

    Property flag(status_ As String) As Boolean
        Get
            If flags_.ContainsKey(status_) Then
                Return flags_(status_)
            Else
                Return False
            End If
        End Get

        Set(value As Boolean)
            If flags_.ContainsKey(status_) Then
                flags_(status_) = value
            Else
                flags_.Add(status_, value)
            End If
        End Set

    End Property

    Private Function ClearFlags()

        Try
            For Each pair As KeyValuePair(Of String, Boolean) In flags_
                flag(pair.Key) = False
            Next
        Catch ex As Exception

        End Try

    End Function

    Public Sub start_timer(status_ As String, delay As Integer)

        If Timers_ Is Nothing Then
            Timers_ = New Dictionary(Of String, Timers.Timer)()
        End If

        If Not Timers_.ContainsKey(status_) Then
            Timers_.Add(status_, New System.Timers.Timer(delay * 1000))
        End If

        If Timers_(status_).Enabled = False Then
            Timers_(status_) = New System.Timers.Timer(delay * 1000)
            Timers_(status_).AutoReset = False
            Timers_(status_).Start()
            flag(status_) = True
        End If

    End Sub

    Public Sub stop_timer(status_ As String)

        If Not Timers_.ContainsKey(status_) Then Return

        If Timers_(status_).Enabled = False Then
            Timers_(status_).Stop()
            flag(status_) = False
        End If

    End Sub

    Public Function check_timer(status_ As String) As Boolean

        If flag(status_) = True Then
            Return Not Timers_(status_).Enabled
        Else
            Return False
        End If

    End Function


    ' Private Property notif_status_timers As Dictionary(Of String, Timers.Timer) ' status - timer
    Private Shared Status_timer As Timer_with_tag
    Private Property notif_cond_timers As New Dictionary(Of String, Timer_with_tag) 'Timers.Timer) ' cond_id - timer

    Private PreviousStatus As String = ""


    Private Sub Send_notif(type As String, Optional item As Object = Nothing) 'item = whitch condition has changed

        Select Case type

'called only if status changed
            Case "status"

                If (PreviousStatus <> currentstatus_) Then
                    If Status_timer IsNot Nothing Then
                        If Status_timer.Enabled Then Status_timer.Stop()
                    End If
                End If

                If DATA_SET_Notifications.Tables("notif_stat") IsNot Nothing Then
                    For Each row As DataRow In DATA_SET_Notifications.Tables("notif_stat").Rows

                        If row(1) = currentstatus_ Then
                            If row(0) = True Then
                                If row(2) = 0 Then
                                    'send notif

                                    send_email(MachineName, "status", currentstatus_, 0, tbl_sendto, current_stream, devices)
                                Else
                                    Status_timer = New Timer_with_tag
                                    Status_timer.Stop()
                                    Status_timer.Interval = row(2) * 1000
                                    Status_timer.tag = currentstatus_
                                    Status_timer.machinename = MachineName
                                    Status_timer.tbl_sendto = tbl_sendto
                                    Status_timer.AutoReset = False
                                    Status_timer.devices = devices
                                    Status_timer.current_stream = current_stream
                                    AddHandler Status_timer.Elapsed, AddressOf MCH_.Status_HandleTimer
                                    Status_timer.Start()

                                End If
                            End If
                        End If
                    Next
                End If

                PreviousStatus = currentstatus_

'Called everytime
            Case "condition"

                If DATA_SET_Notifications.Tables("notif_cond") IsNot Nothing Then

                    For Each row As DataRow In DATA_SET_Notifications.Tables("notif_cond").Rows

                        '0 .Columns. ("Condition")
                        '1 .Columns. ("Warning")
                        '2 .Columns. ("Fault (Alarm)")
                        '3 .Columns. ("Delay")

                        If row(0).ToString().StartsWith(item.DataItemID) Then

                            'warning
                            If row(1) = True And item.value.ToString().StartsWith("Warning") Then


                                If row(3) = 0 Then
                                    'send 
                                    send_email(MachineName, "Warning", item.value, 0, tbl_sendto, current_stream, devices)
                                Else
                                    'wait delay
                                    If notif_cond_timers.ContainsKey(item.DataItemID) Then
                                        notif_cond_timers(item.DataItemID).Stop()
                                        notif_cond_timers(item.DataItemID).Interval = row(3) * 1000
                                        'notif_cond_timers(item.DataItemID).
                                    Else
                                        notif_cond_timers.Add(item.DataItemID, New Timer_with_tag)
                                        notif_cond_timers(item.DataItemID).Interval = row(3) * 1000
                                        notif_cond_timers(item.DataItemID).tag = item
                                        notif_cond_timers(item.DataItemID).machinename = MachineName
                                        notif_cond_timers(item.DataItemID).tbl_sendto = tbl_sendto
                                        notif_cond_timers(item.DataItemID).AutoReset = False
                                        notif_cond_timers(item.DataItemID).devices = devices
                                        notif_cond_timers(item.DataItemID).current_stream = current_stream
                                        AddHandler notif_cond_timers(item.DataItemID).Elapsed, AddressOf MCH_.cond_HandleTimer
                                    End If
                                    notif_cond_timers(item.DataItemID).Start()
                                End If
                            End If

                            'fault
                            If row(2) = True And item.value.ToString().StartsWith("Fault") Then
                                If row(3) = 0 Then
                                    'send 
                                    send_email(MachineName, "Fault", item.value, 0, tbl_sendto, current_stream, devices)
                                Else
                                    'wait delay
                                    If notif_cond_timers.ContainsKey(item.DataItemID) Then
                                        notif_cond_timers(item.DataItemID).Stop()
                                        notif_cond_timers(item.DataItemID).Interval = row(3) * 1000
                                    Else
                                        notif_cond_timers.Add(item.DataItemID, New Timer_with_tag) 'New Timers.Timer(row(3) * 1000))
                                        notif_cond_timers(item.DataItemID).Interval = row(3) * 1000
                                        notif_cond_timers(item.DataItemID).tag = item
                                        notif_cond_timers(item.DataItemID).machinename = MachineName
                                        notif_cond_timers(item.DataItemID).tbl_sendto = tbl_sendto
                                        notif_cond_timers(item.DataItemID).AutoReset = False
                                        AddHandler notif_cond_timers(item.DataItemID).Elapsed, AddressOf MCH_.cond_HandleTimer
                                    End If
                                    notif_cond_timers(item.DataItemID).Start()
                                End If
                            End If

                        End If
                        'End If
                    Next
                End If

                Previous_cond_ = Current_cond_

            Case "other"

        End Select
        'SAVE_MTC_CURRENT(MachineName)
    End Sub

    'Private Function Find_previous_cond_value(cond_ As ICondition) As String

    '    If Previous_cond_ Is Nothing Then Return ""

    '    Dim cond As ICondition = Array.Find(Previous_cond_, Function(x) (x.DataItemID = cond_.DataItemID))

    '    If cond IsNot Nothing Then
    '        Return cond.Value
    '    Else
    '        Return ""
    '    End If
    'End Function

    Private Shared Async Sub Status_HandleTimer(sender As Object, e As EventArgs)
        If Status_timer.Enabled = True Then Status_timer.Stop()
        Status_timer = Nothing

        send_email(sender.machinename, "status", sender.tag, sender.interval, sender.tbl_sendto, sender.current_stream, sender.devices)
    End Sub

    Private Shared Async Sub cond_HandleTimer(sender As Object, e As EventArgs)

        send_email(sender.machinename, "condition", sender.tag.value, sender.interval, sender.tbl_sendto, sender.current_stream, sender.devices)

    End Sub

    Private Property previous_cond As New Dictionary(Of String, String) 'cond_id - value

    Property Current_cond() As ICondition()

        Get
            'For Each item As ICondition In Current_cond_
            '    If item.DataItemID = item_id Then Return item
            'Next

            Return Nothing
        End Get

        Set(value As ICondition())

            Current_cond_ = Nothing
            Current_cond_ = value
            IN_alarms = False
            List_of_alarms.Clear()
            For Each value_ In Current_cond_
                If value_.ConditionType.StartsWith("Fault") Then
                    List_of_alarms.Add(value_.Value.ToString())
                    IN_alarms = True
                End If
                If value_.ConditionType.StartsWith("Warning") Then
                    List_of_alarms.Add(value_.Value.ToString())
                    IN_alarms = True
                End If

                If previous_cond.ContainsKey(value_.DataItemID) Then
                    If Not previous_cond(value_.DataItemID) = value_.Value Then

                        Send_notif("condition", value_)

                        previous_cond(value_.DataItemID) = value_.Value

                        save_item(value_.DataItemID, value_.Value)

                    End If
                Else
                    previous_cond.Add(value_.DataItemID, value_.Value)
                End If
            Next

        End Set

    End Property

    Property IN_alarms As Boolean
    Property List_of_alarms As New List(Of String)


    Property delays(status_ As String) As String
        Get
            If delays_.ContainsKey(status_) Then
                Return delays_(status_)
            Else
                Return 0
            End If

        End Get
        Set(value As String)
            If delays_.ContainsKey(status_) Then
                delays_(status_) = value
            Else
                delays_.Add(status_, value)
            End If
        End Set
    End Property

    ReadOnly Property delays() As ICollection 'List(Of String)
        Get
            Return delays_.Keys
        End Get
    End Property

    Public Sub ClearDelays()
        delays_.Clear()
    End Sub

    Property IP As String
        Get
            Return ip_
        End Get
        Set(value As String)
            ip_ = value
        End Set
    End Property

    Property PartNumber_Variable As String
        Get
            Return partNumberVariable
        End Get
        Set(value As String)
            partNumberVariable = value
        End Set
    End Property

    Property PartNumber_Value As String
        Get
            Return partNumberValue
        End Get
        Set(value As String)
            partNumberValue = value
        End Set
    End Property

    Property Operation_Value As String
        Get
            Return operationValue
        End Get
        Set(value As String)
            operationValue = value
        End Set
    End Property

    Property Operator_RefId As String
        Get
            Return operatorRefId
        End Get
        Set(value As String)
            operatorRefId = value
        End Set
    End Property

    Public Property PartNumber_Prefix1 As String
    Public Property PartNumber_Filter1Start As String
    Public Property PartNumber_Filter1End As String
    Public Property PartNumber_Filter2Apply As Boolean
    Public Property PartNumber_Prefix2 As String
    Public Property PartNumber_Filter2Start As String
    Public Property PartNumber_Filter2End As String
    Public Property PartNumber_Filter3Apply As Boolean
    Public Property PartNumber_Prefix3 As String
    Public Property PartNumber_Filter3Start As String
    Public Property PartNumber_Filter3End As String
    Public Property Operation_Available As Boolean
    Public Property Operation_FilterStart As String
    Public Property Operation_FilterEnd As String
    Public Property PRN_startwith As String
    Public Property PRN_endwith As String
    Public Property Pallet_startwith As String
    Public Property Pallet_endwith As String
    Public Property Pallet_sendToMU As Boolean
    Public Property MonitoringBoardId As Integer
    Public Property CsdOnSetup As Boolean
    Public Property EnableMCS As Boolean
    Public Property COnDuringSetup As Boolean
    Public Property SaveDataRaw As Boolean
    Public Property SaveDataRawProdOnly As Boolean
    Public Property SaveDataRawSetup As Boolean
    Public Property SavedFirstNoEmonitoring As Boolean
    Public Property MachineIp As String
    Public Property MachinePort As Integer
    Public Property Sensors As List(Of Monitoring)
    Public Property WarningPressure As Integer
    Public Property CriticalPressure As Integer
    Public Property WarningTemperature As Integer
    Public Property CriticalTemperature As Integer
    Public Property MCSDelay As Integer
    Public Property DelayForCycleOff As Integer

    Property progno As String
        Get
            Return progno_
        End Get
        Set(value As String)
            progno_ = value
        End Set
    End Property

    Property Pallet As String
        Get
            Return Pallet_
        End Get
        Set(value As String)
            Pallet_ = value
        End Set
    End Property

    Property progno_val As String
        Get
            Return progno_value
        End Get
        Set(value As String)
            progno_value = value
        End Set
    End Property

    Property Pallet_val As String
        Get
            Return Pallet_value
        End Get
        Set(value As String)
            Pallet_value = value
        End Set
    End Property

    Property ConnectorType As String
        Get
            Return ConnectorType_
        End Get
        Set(value As String)
            ConnectorType_ = value
        End Set
    End Property


    Property FeedRate_Variable As String
        Get
            Return feedRateVariable
        End Get
        Set(value As String)
            feedRateVariable = value
        End Set
    End Property

    Property FeedRate_Value As String
        Get
            Return feedRateValue
        End Get
        Set(value As String)
            feedRateValue = value
        End Set
    End Property

    Property Min_Fover As String
        Get
            Return Min_Fover_
        End Get
        Set(value As String)
            Min_Fover_ = value
        End Set
    End Property

    Property Max_Fover As String
        Get
            Return Max_Fover_
        End Get
        Set(value As String)
            Max_Fover_ = value
        End Set
    End Property


    Property Spindle_Variable As String
        Get
            Return spindleVariable
        End Get
        Set(value As String)
            spindleVariable = value
        End Set
    End Property

    Property Spindle_Value As String
        Get
            Return spindleValue
        End Get
        Set(value As String)
            spindleValue = value
        End Set
    End Property

    Property Min_Sover As String
        Get
            Return Min_Sover_
        End Get
        Set(value As String)
            Min_Sover_ = value
        End Set
    End Property

    Property Max_Sover As String
        Get
            Return Max_Sover_
        End Get
        Set(value As String)
            Max_Sover_ = value
        End Set
    End Property


    Property Rapid_Variable As String
        Get
            Return rapidVariable
        End Get
        Set(value As String)
            rapidVariable = value
        End Set
    End Property

    Property Rapid_Value As String
        Get
            Dim rapidValue_ = rapidValue
            If ConnectorType = "Focas" Then
                Select Case rapidValue
                    Case 0
                        rapidValue_ = "100"
                    Case 1
                        rapidValue_ = "50"
                    Case 2
                        rapidValue_ = "25"
                    Case 3
                        rapidValue_ = "0"
                End Select
            End If
            Return rapidValue_
        End Get
        Set(value As String)
            rapidValue = value
        End Set
    End Property

    Property Min_Rover As String
        Get
            Return Min_Rover_
        End Get
        Set(value As String)
            Min_Rover_ = value
        End Set
    End Property

    Property Max_Rover As String
        Get
            Return Max_Rover_
        End Get
        Set(value As String)
            Max_Rover_ = value
        End Set
    End Property


    Property RequiredParts_Variable As String
        Get
            Return requiredPartsVariable
        End Get
        Set(value As String)
            requiredPartsVariable = value
        End Set
    End Property

    Property RequiredParts_Value As String
        Get
            Return requiredPartsValue
        End Get
        Set(value As String)
            requiredPartsValue = value
        End Set
    End Property


    Property PartCount_Variable As String
        Get
            Return partCountVariable
        End Get
        Set(value As String)
            partCountVariable = value
        End Set
    End Property

    Property PartCount_Value As String
        Get
            Return partCountValue
        End Get
        Set(value As String)
            partCountValue = value
        End Set
    End Property


    Dim timestamp_ As DateTime
    Property Timestamp As DateTime
        Get
            Return timestamp_
        End Get
        Set(value As DateTime)
            timestamp_ = value
        End Set
    End Property

    Private lastRecord_ As String
    Public Property LastRecord() As String
        Get
            Return lastRecord_
        End Get
        Set(ByVal value As String)
            lastRecord_ = value
        End Set
    End Property

    'Function update_other(type As String, value As String)
    Public Sub update_other(type As String, value As String)

        If value Is Nothing Then value = ""

        Dim startStr As String = ""
        Dim endStr As String = ""

        If type = "PartNumber" Then

            'startStr = PartNumber_Filter1Start
            'endStr = PartNumber_Filter1End

            Dim partNumberFilter1 = ""
            Dim partNumberFilter2 = ""
            Dim partNumberFilter3 = ""

            partNumberFilter1 = $"{PartNumber_Prefix1}{CutStartEndValue(value, PartNumber_Filter1Start, PartNumber_Filter1End)}"

            If PartNumber_Filter2Apply Then
                partNumberFilter2 = $"{PartNumber_Prefix2}{CutStartEndValue(value, PartNumber_Filter2Start, PartNumber_Filter2End)}"
            End If

            If PartNumber_Filter3Apply Then
                partNumberFilter3 = $"{PartNumber_Prefix3}{CutStartEndValue(value, PartNumber_Filter3Start, PartNumber_Filter3End)}"
            End If

            value = $"{partNumberFilter1}{partNumberFilter2}{partNumberFilter3}"

            partNumberValue = value
        Else

            If type = "ProgramNumber" Then
                startStr = PRN_startwith
                endStr = PRN_endwith
                progno_value = CutStartEndValue(value, startStr, endStr)

            ElseIf type = "Operation" Then
                operationValue = ""
                If Operation_Available Then
                    startStr = Operation_FilterStart
                    endStr = Operation_FilterEnd
                    operationValue = CutStartEndValue(value, startStr, endStr)
                End If

            ElseIf type = "pallet" Then
                startStr = Pallet_startwith
                endStr = Pallet_endwith

            ElseIf type = "spov" Then
                spindleValue = value

            ElseIf type = "rapidov" Then
                rapidValue = value

            ElseIf type = "frov" Then
                feedRateValue = value

            ElseIf type = "PartCountVariable" Then
                partCountValue = IIf(IsNumeric(value), value, 0)

            ElseIf type = "RequiredPartsVariable" Then
                requiredPartsValue = IIf(IsNumeric(value), value, 0)

            End If

            value = CutStartEndValue(value, startStr, endStr)
        End If

        If current_other.ContainsKey(type) Then
            current_other(type) = value
        Else
            current_other.Add(type, value)
        End If

        Return

    End Sub

    Private Function CutStartEndValue(value As String, startCut As String, endCut As String) As String

        Dim endValue = value
        Dim idxCut As Integer = 0


        If String.IsNullOrEmpty(startCut) And String.IsNullOrEmpty(endCut) Then
            Return value
        End If

        If (Not String.IsNullOrEmpty(startCut) And Not endValue.Contains(startCut)) Or (Not String.IsNullOrEmpty(endCut) And Not endValue.Contains(endCut)) Then
            Return ""
        End If


        If Not String.IsNullOrEmpty(startCut) And endValue.Contains(startCut) Then
            idxCut = endValue.IndexOf(startCut) + startCut.Length
            endValue = endValue.Substring(idxCut)
        End If

        If Not String.IsNullOrEmpty(endCut) And endValue.Contains(endCut) Then
            idxCut = endValue.IndexOf(endCut)
            endValue = endValue.Substring(0, idxCut)
        End If

        Return endValue

    End Function


    Property rpn_result_intern_ As RpnOperand
        Get
            Return rpn_result__
        End Get
        Set(value As RpnOperand)
            rpn_result__ = value
        End Set
    End Property

    'Sub clear_condition(value As String)
    '    Conditions_expression_.Clear()
    'End Sub

    'Set machines back to COFF without partnumbers
    'Private Sub Cleanup()
    '    name_ = ""
    '    ip_ = ""

    '    ' delay_ = 0
    '    partNumberVariable = ""
    '    partNumberValue = ""
    '    operationValue = ""
    '    spindleVariable = ""
    '    spindleValue = ""
    '    feedRateVariable = ""
    '    feedRateValue = ""
    '    rapidVariable = ""
    '    rapidValue = ""
    '    partCountVariable = ""
    '    requiredPartsVariable = ""
    'End Sub

    Private choices As New Dictionary(Of String, List(Of String)) ' condition id / list of values

    Public Sub save_item(item_id As String, value As String)

        If choices.ContainsKey(item_id) Then
            If Not choices(item_id).Contains(value) Then
                choices(item_id).Add(value)
                'save_choices_ondisk(choices)
            End If
        Else
            choices.Add(item_id, New List(Of String))
            choices(item_id).Add(value)
            'save_choices_ondisk(choices)
        End If

    End Sub

    Function Get_choices(item_id As String) As List(Of String)
        Dim empty_list As New List(Of String)
        empty_list.Add("")
        If choices IsNot Nothing Then
            If choices.ContainsKey(item_id) Then
                If choices.ContainsKey(item_id) Then
                    Return choices(item_id)
                Else
                    Return empty_list
                End If
            Else
                Return empty_list
            End If
        Else
            Return empty_list
        End If
    End Function


    Shared BLOCK As String = "<html xmlns="" "">
<head><title></title></head>
<body>


<a href=""http://csiflex.com/""><img src = ""http://csiflex.com/sites/default/files/GDS-GRADB.png"" /><br /><br /></a>
<div style = ""border-top:3px solid #22BCE5"">&nbsp;</div>
<span style = ""font-family:Arial;font-size:10pt"">

<p><font size=""4""> Machine <b>{mchName}</b>, </font></p>
 <br /><br />


[TEXT]

<a href=""[LiNK]"">Display details</a>

 <br /><br />
 <br /><br />

<div style = ""border-top:3px solid #22BCE5"">&nbsp;</div>


 <br />
CSIFlex notification system
</span>
</body>
</html>"

    Private Shared Sub send_email(machinename As String, type As String, currentstatus_ As String, delay As Integer, tbl_sendto As DataTable, current_stream As DataStream, devices As Object) 'mailadresses As String, filePath As String, custommsg As String)

        delay = delay / 1000
        Using writer As StreamWriter = New StreamWriter("C:\Log_Server.txt", True)
            writer.Write(vbCrLf & (machinename + " : " + type + " : " + currentstatus_ + " : " + delay.ToString()))
            writer.Close()
        End Using
        Dim email_settings = CSI_Library.GetEmailSettingsFromDB()
        If email_settings IsNot Nothing Then

            Dim status_text As String = ""
            Dim cond_text As String = ""

            Dim key_words As New Dictionary(Of String, String)
            key_words.Add("[MachineName]", machinename)
            key_words.Add("[StatusName]", currentstatus_)
            key_words.Add("[ConditionName]", currentstatus_)
            key_words.Add("[TimeEvent]", Now.AddSeconds(-delay).ToString("yyyy-MMMM-dd HH:mm:ss"))


            If File.Exists(serverRootPath & "\sys\Conditions\" & machinename & "\text.csys") Then

                Using r As StreamReader = New StreamReader(serverRootPath + "\sys\Conditions\" & machinename & "\text.csys")
                    '  Dim t() As String = r.ReadToEnd.Replace(vbCrLf, "").Split("|")
                    Dim t_ As String = r.ReadToEnd.Replace(vbCrLf, "")
                    t_ = t_.Replace("AwaitedDelay", "TimeEvent")

                    For Each key In key_words.Keys
                        t_ = t_.Replace(key, key_words(key))
                    Next

                    Dim t() As String = t_.Split("|")

                    If Not t.Length < 1 Then status_text = t(0)
                    If Not t.Length < 2 Then cond_text = t(1)

                    r.Close()
                End Using
            End If

            If (tbl_sendto.Rows.Count > 0) Then

                Try
                    Dim mail As MailMessage = New MailMessage()
                    mail.IsBodyHtml = True
                    Dim client As SmtpClient = New SmtpClient(email_settings.SmtpHost, email_settings.SmtpPort)
                    If Not email_settings.SenderEmail.Contains("@") Then
                        mail.From = New MailAddress("csiflexreports@gmail.com")
                    Else
                        mail.From = New MailAddress(email_settings.SenderEmail)
                    End If

                    client.EnableSsl = False
                    client.Timeout = 10000
                    client.DeliveryMethod = SmtpDeliveryMethod.Network
                    client.UseDefaultCredentials = False

                    If (email_settings.RequireCred) Then
                        client.Credentials = New System.Net.NetworkCredential(email_settings.SenderEmail, AES_Decrypt(email_settings.EncryptedPassword, "pass"))
                    End If

                    Dim modified_block As String = ""
                    modified_block = BLOCK.Replace("{mchName}", machinename)
                    Dim small_text As String = ""

                    If type = "status" Then
                        'mail.Body = status_text
                        modified_block = modified_block.Replace("[TEXT]", status_text)
                        small_text = status_text
                        mail.Subject = "CSIFlex Notification: " + machinename + " Status: " + currentstatus_
                    Else
                        'mail.Body = cond_text
                        modified_block = modified_block.Replace("[TEXT]", cond_text)
                        small_text = cond_text
                        mail.Subject = "CSIFlex Notification: " + machinename + " on " + currentstatus_
                    End If


                    Dim PORT As String = ""
                    Try
                        If File.Exists((serverRootPath & "\sys\RM_port_.csys")) Then
                            Using reader As StreamReader = New StreamReader(serverRootPath & "\sys\RM_port_.csys")
                                PORT = reader.ReadLine
                                reader.Close()
                            End Using
                        End If
                    Catch ex As Exception
                        Log.Error("At reading RM Port from csys: ", ex)
                    End Try

                    Dim srv_IP As String = ""
                    Dim host_IP = Dns.GetHostEntry(Dns.GetHostName())
                    For Each x In host_IP.AddressList
                        If x.AddressFamily = AddressFamily.InterNetwork Then
                            srv_IP = x.ToString()
                        End If
                    Next

                    If srv_IP = "" Then Throw New Exception("Local IP Address Not Found!")

                    Dim page_ID As String = Generate_html_tree(machinename, current_stream, devices, small_text)
                    modified_block = modified_block.Replace("[LiNK]", "http://" + srv_IP + ":" + PORT + "/saved_pages/" + machinename + "_" + page_ID)

                    mail.Body = modified_block

                    For Each email As DataRow In tbl_sendto.Rows
                        If (email.Item(0) = True) And Not email.Item(1) = "" Then
                            mail.To.Add(New MailAddress(email.Item(1)))
                        End If
                    Next

                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

                    client.Send(mail)

                Catch ex As Exception
                    ' CSI_LIB.Log.Error("email error,CONFIG:MailAdresses:" + mailadresses.ToString() + ",port:" + emailconfig.Rows(0)("smtpport").ToString() + ",server:" + emailconfig.Rows(0)("smtphost").ToString() + ",requirecred:" + emailconfig.Rows(0)("requirecred").ToString() + ",email:" + emailconfig.Rows(0)("senderemail").ToString() + ",pwd:" + emailconfig.Rows(0)("senderpwd").ToString() + ",MSG.", ex)
                    Log.Error("email not sent ", ex)

                End Try

            End If
        Else
            Log.Error("send_email: cannot load email from database")
        End If

    End Sub


    Private Shared Function save_page(text As String, big_text As String, machinename As String)

        Try
            Dim path As String = serverRootPath + "\html\html\saved_pages\"

            If Not Directory.Exists(path) Then Directory.CreateDirectory(path)

            Dim fCount As Integer = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).Length

            Dim s As StreamWriter = New StreamWriter(serverRootPath + "\html\html\saved_pages\" + machinename + "_" + fCount.ToString())
            s.WriteLine(machinename)
            s.WriteLine(text)
            s.WriteLine(big_text)
            s.Close()

            Return fCount.ToString()

        Catch ex As Exception
            Log.Error("Could not save page", ex)
        Finally

        End Try

    End Function


    'Private Shared Sub SAVE_MTC_CURRENT(machinename As String)
    '    'save 

    '    If File.Exists(serverRootPath & "\sys\Conditions\" & machinename & "\" & "ip.csys") Then

    '        Dim IP_ As String

    '        Using streader As New StreamReader(serverRootPath & "\sys\Conditions\" & machinename & "\" & "ip.csys")
    '            IP_ = streader.ReadLine()
    '            streader.Close()

    '        End Using

    '        Dim m_client As EntityClient = New EntityClient(IP_)
    '        Dim Current_stream As DataStream = m_client.Current()
    '        'm_client.CurrentXml

    '    End If

    'End Sub

    Private Shared Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String

        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""

        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            AES.Padding = System.Security.Cryptography.PaddingMode.Zeros
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Shared Function Generate_html_tree(machinename As String, current_stream As DataStream, devices As Object, text As String) 'mailadresses As String, filePath As String, custommsg As String)

        Dim s As String = ""
        Dim h As String = ""
        Dim hh As String = ""
        Dim xml As DataStream = current_stream

        Dim device_index As Integer = 0
        Try

            Dim Current_device As DeviceStream
            Dim value As String = ""

            's = s + "<table style=""width:100%"">"
            For Each device In devices
                If (machinename = device.Name) Then
                    Current_device = current_stream.DeviceStreams(device_index)
                    Dim current_component_stream() As ComponentStream = Current_device.ComponentStreams

                    's = add_LI_parent(device.uuid, device.Name, "")

                    h = ""
                    hh = ""

                    For Each component In device.Components

                        hh = fill_component_(component, Current_device)

                        h = h + add_LI_parent(component.ID, component.name, "<ol class=""dd-list"">" + hh + "</ol>")

                    Next

                    s = s + add_LI_parent(device.uuid, device.Name, "<ol class=""dd-list"">" + h + "</ol>")
                    ' s = s + "<table style=""width:100%"">"
                    For Each dataItem In device.DataItems

                        Dim displayName As String = dataItem.ID

                        For Each Current_comp_stream In current_component_stream
                            If Current_comp_stream.ComponentType = "Device" Then
                                Dim current_events() As IDataElement = Current_comp_stream.Events
                                For Each ev In current_events
                                    If displayName = ev.DataItemID Then
                                        s = s + add_LI(ev.DataItemID, "<td><b>" + ev.Type + "  : </b></td> <td> " + ev.Value + "</td>")

                                    End If
                                Next
                            End If
                        Next

                    Next
                    '  s = s + "</table>"
                End If
                device_index += 1
            Next
            ' s = s + "</table>"
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try

        Return save_page(text, s, machinename)

    End Function

    Private Shared Function fill_component_(component As OpenNETCF.MTConnect.Component, Current_device As DeviceStream) As String


        ' Dim boldFont As Font = New Font(TGV_MTC.DefaultCellStyle.Font.Name, 10, TGV_MTC.DefaultCellStyle.Font.Bold, TGV_MTC.DefaultCellStyle.Font.Unit)
        Dim hh As String = ""
        Dim sub_h As String = ""
        'Dim componentNode As TreeGridNode = Parent.Nodes.Add(component.Name, "")
        'componentNode.Tag = component
        'componentNode.DefaultCellStyle.BackColor = Color.Gray
        'componentNode.DefaultCellStyle.ForeColor = Color.White
        'componentNode.DefaultCellStyle.Font = boldFont

        For Each subcomponent In component.Components
            sub_h = fill_component_(subcomponent, Current_device)
            hh = hh + add_LI_parent(subcomponent.ID, subcomponent.Name, "<ol class=""dd-list"">" + sub_h + "</ol>")
        Next
        sub_h = ""


        Dim found As Boolean = False

        Dim component_index As Integer = -1
        For Each subcomponent In Current_device.ComponentStreams
            component_index += 1
            If subcomponent.Name = component.Name And found = False Then
                found = True
                Exit For
            End If
        Next

        If found = True Or Current_device.ComponentStreams.Count = 0 Then

            Dim Current_dataitems() As IDataElement = Current_device.ComponentStreams(component_index).AllDataItems
            Dim Current_conditions() As IDataElement = Current_device.ComponentStreams(component_index).Conditions

            Dim first__ As Boolean = True
            Dim value As String = ""

            'hh += "<table style=""width:100%"">"

            For Each dataItem In component.DataItems

                Dim displayName As String = dataItem.ID

                value = ""
                If Not (String.IsNullOrEmpty(dataItem.Name)) Then
                    displayName += String.Format(" ({0})", dataItem.Name)

                    For Each CurrentdataItem In Current_dataitems
                        If dataItem.ID = CurrentdataItem.DataItemID Then

                            value = CurrentdataItem.Value
                            Exit For
                        End If
                    Next
                    For Each Currentcondition In Current_conditions
                        If dataItem.ID = Currentcondition.DataItemID Then

                            value = Currentcondition.Value
                        End If

                    Next

                End If

                If Not (String.IsNullOrEmpty(dataItem.ID)) Then
                    For Each Currentcondition In Current_conditions
                        If dataItem.ID = Currentcondition.DataItemID Then

                            value = Currentcondition.Value
                        End If
                    Next

                    For Each CurrentdataItem In Current_dataitems
                        If dataItem.ID = CurrentdataItem.DataItemID Then

                            value = CurrentdataItem.Value
                        End If
                    Next
                End If

                '    Dim notboldFont As Font = New Font(TGV_MTC.DefaultCellStyle.Font.Name, 8, TGV_MTC.DefaultCellStyle.Font.Style, TGV_MTC.DefaultCellStyle.Font.Unit)

                hh += add_LI(dataItem.ID, "<td><b>" + If(dataItem.Name Is Nothing, dataItem.ID, dataItem.Name) + " : </b></td> <td> " + value + "</td>")

            Next
        End If

        'hh += "</table>"
        Return hh

    End Function

    Private Shared Function add_LI_parent(id As String, name As String, inner As String) As String

        Return "<li class=""dd-item"" data-id=" + id + ">" + "<div class=""dd-handle""> " + name + " </div>" + inner + "</li>" + vbCrLf

    End Function

    Private Shared Function add_LI(id As String, inner As String) As String

        Return "<li class=""dd-item"" data-id=" + id + ">" + "<tr>" + inner + "</tr>" + "</li>" + vbCrLf

    End Function



End Class

Public Class EmailSettings
    Public SenderEmail As String
    Public EncryptedPassword As String
    Public SmtpPort As Integer
    Public SmtpHost As String
    Public RequireCred As Boolean
    Public IsDefault As Boolean

    Public IsUsed As Boolean
    Public UseSSL As Boolean
End Class

Class Timer_with_tag
    Inherits Timers.Timer
    Public Property tag As Object
    Public Property machinename As String
    Public Property tbl_sendto As DataTable
    Public Property devices As Object
    Public Property current_stream As DataStream
End Class

Public Class Monitoring
    Public Property DeviceName As String
    Public Property SensorId As String
    Public Property SensorName As String
    Public Property SensorPallet As String
    Public Property SensorType As String
    Public Property CurrentPallet As String
    Public Property Timestamp As DateTime
    Public Property Value As Decimal
    Public Property IsAvailable As Boolean
    Public Property IsConnected As Boolean
    Public Property IsMonitoring As Boolean
    Public Property IsOverride As Boolean
    Public Property IsAlarming As Boolean
    Public Property IsCSD As Boolean
End Class

Public Class xxMonitBoardStatus
    Public Property CurrentPallet As String
    Public Property CurrentPressure As Integer
    Public Property CriticalPressure As Integer
    Public Property IsCSD As Boolean
    Public Property IsNowMCS As Boolean
    Public Property IsInCriticalStop As Boolean
    Public Property IsRestore As Boolean
    Public Property StartedDelay As Boolean
    Public Property MCSDelay As Integer
    Public Property MCSTimeStart As TimeSpan
End Class
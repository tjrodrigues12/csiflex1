Imports System.Threading

Imports System
Imports System.Windows
Imports System.Reflection
Imports System.IO
Imports System.IO.File
Imports System.Windows.Forms

Imports OpenNETCF.MTConnect.Client
Imports OpenNETCF.MTConnect.EntityClient
Imports OpenNETCF.Web

Imports System.IO.Compression
Imports MySql.Data.MySqlClient

Imports System.Management
Imports System.Data.OleDb
Imports System.IO.IOException
Imports System.Text
Imports System.Globalization
Imports CSI_Library.CSI_DATA
Imports System.Net.Sockets

Imports System.IO.Pipes
Imports System.Text.RegularExpressions
Imports System.Runtime.CompilerServices

Imports Microsoft.Reporting.WinForms
Imports Microsoft.VisualBasic

Imports LumenWorks.Framework.IO.Csv
Imports RpnParser
Imports Rpn_function

Imports MathNet.Numerics


Imports System.Data.SQLite
Imports System.Net.Mail
Imports System.ComponentModel
Imports System.Xml
Imports OpenNETCF.MTConnect
Imports System.Xml.XPath
Imports System.Xml.Xsl
Imports System.Net
Imports System.Security.Permissions
Imports System.Security

'Cpu and hdd
'Imports System.Security.Cryptography
'Imports Microsoft.Win32
'Imports System.Runtime.InteropServices


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
    Public Shared username As String = ""
    Public Shared update As Boolean
    Public Shared eNET_task_mon As String
    Public Shared MTConnect_task_mon As String
    Public Shared logfiles_task_mon As String
    Public Shared main_task_mon As String
    Public Shared db_task_mon As String
    'Public Shared MySqlConnectionString As String = "server=localhost;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;SslMode=none;"
    Public Shared MySqlConnectionString As String = "server=192.168.1.131;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;SslMode=none;" 'SOMR
    ' Public Shared MySqlConnectionString As String = "server=10.0.10.189;user=root;password=CSIF1337;port=3306;Convert Zero Datetime=True;SslMode=none;"
    Public Shared MySqlServerBaseString As String = "user=client;password=csiflex123;port=3306;"
    'Public Shared MysqlDataFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\mysql\mysql-5.7.14-win32\data" :::: OLD DB Location
    'Public Shared MysqlDataFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\MySQL\MySQL Server 5.7\Data" ::: Path Local Testing Mesting 
    'Public Shared MysqlDataFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Server\MySQL\MySQL Server 5.7\Data" ::: MSI Location
    'Local Latest :: Public Shared MysqlDataFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\MySQL\MySQL Server 5.7\Data"
    'Local Latest :: Public Shared serverRootPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Server"  '"C:\ProgramData\CSI Flex Server"
    Public Shared MysqlDataFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\mysql\mysql-5.7.21-win32\Data"
    'Public Shared ServerProgramFilesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server"
    Public Shared serverRootPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Server"  '"C:\ProgramData\CSI Flex Server"
    Public Shared ClientRootPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Client"  '"C:\ProgramData\CSI Flex Client"
    Public Shared sqlitedbpath As String = "URI=file:" + ClientRootPath + "\sys\csisqlite.db3" '"URI=file:sys/csisqlite.db3"
    Public Shared LocalHostIP As String = ""
    Public Shared FilePath_Ehub As String
    Public Shared firstClientUpdate As Boolean = True
    Public Shared firstExecution As Boolean = False
    Public Shared ThreeMonths As Boolean = False
    Public Shared updatestart As Boolean = False
    Public Shared loadingasCON As Boolean = False
    Public Shared DuplicateDevice As Boolean = False
    Public Shared IPChange As Integer = 0

    Public TargetsMonthly_dic As New Dictionary(Of String, Integer)
    Public TargetsWeekly_dic As New Dictionary(Of String, Integer)
    Public TargetsDaily_dic As New Dictionary(Of String, Integer)

#Region "little funct"

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
            LogServerError("Unable to open a DB connection : " & Now, 1)
            Return False
        End Try
    End Function

    Public previousMessage As String
    Public Function check_server(SRVIP As String) As String(,)
        Try
            Dim request As Net.WebRequest = Net.WebRequest.Create(SRVIP)
            request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested
            request.Credentials = New Net.NetworkCredential("autodiag", "")
            request.Timeout = 5000

            Dim response As Net.WebResponse = request.GetResponse()

            ' Get the stream containing content returned by the server.
            Dim dataStream As Stream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.
            Dim reader As New StreamReader(dataStream)
            ' Read the content.
            Dim responseFromServer As String = reader.ReadToEnd()
            ' Display the content.
            Dim pattern As String = "\<eNET.Status>(.*)\</eNET.Status>"
            Dim reg As Regex = New Regex(pattern)
            Dim eNET_task_mon As String = reg.Match(responseFromServer).Groups(1).Value

            pattern = "\<mtconnect.Status>(.*)\</mtconnect.Status>"
            reg = New Regex(pattern)
            Dim MTConnect_task_mon As String = reg.Match(responseFromServer).Groups(1).Value


            pattern = "\<LogFile.Status>(.*)\</LogFile.Status>"
            reg = New Regex(pattern)
            Dim logfiles_task_mon As String = reg.Match(responseFromServer).Groups(1).Value

            pattern = "\<main.Status>(.*)\</main.Status>"
            reg = New Regex(pattern)
            Dim main_task_mon As String = reg.Match(responseFromServer).Groups(1).Value

            pattern = "\<db_update.Status>(.*)\</db_update.Status>"
            reg = New Regex(pattern)
            Dim db_task_mon As String = reg.Match(responseFromServer).Groups(1).Value


            ' Clean up the streams and the response.
            reader.Close()
            response.Close()





            Dim list__(4, 2) As String


            list__(0, 0) = "Main Task"
            If main_task_mon IsNot Nothing Then
                If main_task_mon Then
                    list__(0, 1) = "Running"
                Else
                    list__(0, 1) = "Stopped"
                End If
            Else
                list__(0, 1) = "Down"
            End If

            list__(1, 0) = "Database Updater"
            If db_task_mon IsNot Nothing Then
                list__(1, 1) = db_task_mon
            Else
                list__(1, 1) = "Down"
            End If
            list__(2, 0) = "eNET Connector"
            If eNET_task_mon IsNot Nothing Then
                list__(2, 1) = eNET_task_mon

            Else
                list__(2, 1) = "Down"
            End If
            list__(3, 0) = "MTConnect Connector"
            If MTConnect_task_mon IsNot Nothing Then
                list__(3, 1) = MTConnect_task_mon
            Else
                list__(3, 1) = "Down"
            End If
            list__(4, 0) = "LogFile Connector"
            If logfiles_task_mon IsNot Nothing Then
                list__(4, 1) = logfiles_task_mon
            Else
                list__(4, 1) = "Down"
            End If


            Return list__
        Catch ex As Exception
            If ex.Message = previousMessage Then
                Dim list__(4, 2) As String

                list__(0, 0) = "Main Task"
                list__(0, 1) = "NC"
                list__(1, 0) = "Database Updater"
                list__(1, 1) = ""
                list__(2, 0) = "eNET Connector"
                list__(2, 1) = ""
                list__(3, 0) = "MTConnect Connector"
                list__(3, 1) = ""
                list__(4, 0) = "LogFile Connector"
                list__(4, 1) = ""


                Return list__
            Else


                previousMessage = ex.Message


                Dim list__(4, 2) As String


                list__(0, 0) = "Main Task"
                If main_task_mon IsNot Nothing Then
                    If main_task_mon Then
                        list__(0, 1) = "Running"
                    Else
                        list__(0, 1) = "Stopped"
                    End If
                Else
                    list__(0, 1) = "Down"
                End If

                list__(1, 0) = "Database Updater"
                If db_task_mon IsNot Nothing Then
                    list__(1, 1) = db_task_mon
                Else
                    list__(1, 1) = "Down"
                End If
                list__(2, 0) = "eNET Connector"
                If eNET_task_mon IsNot Nothing Then
                    list__(2, 1) = eNET_task_mon

                Else
                    list__(2, 1) = "Down"
                End If
                list__(3, 0) = "MTConnect Connector"
                If MTConnect_task_mon IsNot Nothing Then
                    list__(3, 1) = MTConnect_task_mon
                Else
                    list__(3, 1) = "Down"
                End If
                list__(4, 0) = "LogFile Connector"
                If logfiles_task_mon IsNot Nothing Then
                    list__(4, 1) = logfiles_task_mon
                Else
                    list__(4, 1) = "Down"
                End If


                Return list__
            End If
        End Try
    End Function

    Public Function CheckIfColumnExist(Name As String, datatable As String) As Boolean
        Dim mySqlCnt As MySqlConnection = New MySqlConnection(MySqlConnectionString)
        Try

            mySqlCnt.Open()
            Dim mysql As String = "SHOW COLUMNS FROM " & datatable & " LIKE '" & Name & "';"
            Dim cmd As New MySqlCommand(mysql, mySqlCnt)

            Dim mysqlReader33 As MySqlDataReader = cmd.ExecuteReader
            Dim dTable_message33 As New DataTable()
            dTable_message33.Load(mysqlReader33)
            mySqlCnt.Close()

            If dTable_message33.Rows.Count <> 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LogServerError("CheckIfColumnExist: " + ex.Message, 1)
            mySqlCnt.Close()
            Return False

        End Try
    End Function
    '-----------------------------------------------------------------------------------------------------------------------
    ' Name : Read_colors_from_database   
    ' para : Nothing
    ' Out  : enet status colors in a datatable 
    ' Com  :
    '-----------------------------------------------------------------------------------------------------------------------
    Public Function Read_colors_from_database() As DataTable
        Dim db_authPath As String = Nothing
        Dim temp_table = New DataTable
        Dim sqlConn As New MySqlConnection
        Try

            If (File.Exists(CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
                Using streader As New StreamReader(CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")
                    db_authPath = streader.ReadLine()
                End Using
            End If
            Dim connectionString As String
            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"


            sqlConn = New MySqlConnection(connectionString)
            sqlConn.Open()

            If sqlConn.State <> 1 Then
                MsgBox("No Connecion with the CSIFlex Database")
                Return Nothing
            Else
                Dim tmp_table_cmd As New MySqlCommand
                Dim reader As MySqlDataReader
                Dim Command As String = "SELECT * FROM tbl_colors"

                temp_table.Clear()


                tmp_table_cmd.CommandText = Command
                tmp_table_cmd.Connection = sqlConn

                reader = tmp_table_cmd.ExecuteReader
                temp_table.Load(reader)
                sqlConn.Close()
            End If

            Return temp_table

        Catch ex As Exception
            MsgBox("Unable to read the colors from the database")
            LogClientError(ex.Message)
            If sqlConn.State = ConnectionState.Open Then sqlConn.Close()
            Return Nothing
        End Try
    End Function

    Public Function Get_firt_date_database(version_ As Integer) As Date
        Dim db_authPath As String = Nothing
        Dim table_ As DataTable = New DataTable("machine_table")
        Dim adapter As New SQLiteDataAdapter
        Dim firstdate As Date

        If Not (version_ = 3) Then
            Dim cnt As SQLiteConnection
            Try
                cnt = New System.Data.SQLite.SQLiteConnection(sqlitedbpath)
                cnt.Open()

                If cnt.State = 1 Then
                Else
                    Forms.MessageBox.Show("Connection to the database failed")
                    GoTo end_
                End If
            Catch ex As Exception
                Forms.MessageBox.Show(" Unable to establish a connection to the database ") '& ex.Message), vbCritical + vbSystemModal)
                GoTo end_
            End Try


            Try
                '  machine = RenameMachine(machine)
                Dim SQLiteCommande As New SQLiteCommand
                Dim SQLiteCommande_text As String = ""
                SQLiteCommande.CommandText = "Select *  FROM [tbl_renamemachines] "
                SQLiteCommande.Connection = cnt

                Dim reader As SQLiteDataReader = SQLiteCommande.ExecuteReader
                table_.Load(reader)
                reader.Close()
                SQLiteCommande_text = ""
                For Each row As DataRow In table_.Rows
                    SQLiteCommande_text = SQLiteCommande_text & "Select * from (Select *  FROM [tbl_" & row.Item(0).ToString() & "] LIMIT 1 ) UNION "
                Next
                SQLiteCommande_text = SQLiteCommande_text.Substring(0, SQLiteCommande_text.Length - 7)


                SQLiteCommande.CommandText = SQLiteCommande_text
                reader = SQLiteCommande.ExecuteReader()
                table_ = New DataTable("firstdates_table")
                table_.Load(reader)
                reader.Close()

                table_.DefaultView.Sort = "Date_ ASC"
                firstdate = table_.Rows(0).Item(4)


                cnt.Close()
            Catch ex As Exception
                '  Forms.MessageBox.Show(" unable to retrieve data from the database") '& ex.Message), vbCritical + vbSystemModal)
                LogClientError("Unable to retrieve data from the database: " & ex.Message)
                If cnt.State = ConnectionState.Open Then cnt.Close()
                GoTo end_
            End Try
        Else

            Dim cnt As MySqlConnection
            Try
                'Dim dbConnectStr As String
                'dbConnectStr = "Provider=Microsoft.Jet.mysql.4.0;Data Source=" & db_path(True) & "\CSI_Database.mdb;" 'db corriger


                Dim directory As String = getRootPath()
                If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                    Using reader_path = New StreamReader(directory + "/sys/SrvDBpath.csys")
                        db_authPath = reader_path.ReadLine()
                    End Using
                End If
                Dim connectionString As String
                connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"


                cnt = New MySqlConnection(connectionString)
                cnt.Open()

                If cnt.State = 1 Then
                Else
                    Forms.MessageBox.Show("Connection to the database failed")
                    Return Nothing
                End If
            Catch ex As Exception
                Forms.MessageBox.Show(" Unable to establish a connection to the database ") ' & ex.Message, vbCritical + vbSystemModal)
                Return Nothing
            End Try

            Try
                Dim SQL_adapter As New MySqlDataAdapter
                Dim SQL_reader As MySqlDataReader
                table_ = New DataTable("SQL_table")
                Dim SQL_COMMAND As New MySqlCommand

                Dim query As String = "Select *  FROM CSI_Database.tbl_renamemachines"

                SQL_COMMAND.CommandText = query '"SELECT *  FROM tbl_" & machine & " WHERE year_ = " & year_start & " and month_= " & month_start & " and day_ = " & day_end
                SQL_COMMAND.Connection = cnt
                SQL_reader = SQL_COMMAND.ExecuteReader

                table_.Load(SQL_reader)

                Dim SQL_COMMAND_text As String = ""
                For Each row As DataRow In table_.Rows
                    SQL_COMMAND_text = SQL_COMMAND_text & "Select first FROM tbl_" & row.Item(0).ToString() & " UNION "
                Next
                If SQL_COMMAND_text.Length <> 0 Then
                    SQL_COMMAND_text = SQL_COMMAND_text.Substring(0, SQL_COMMAND_text.Length - 7)

                    SQL_COMMAND_text = "Select * FROM tbl_" & table_.Rows(0).Item(0).ToString() & " LIMIT 1"

                    SQL_COMMAND.CommandText = SQL_COMMAND_text
                    SQL_reader = SQL_COMMAND.ExecuteReader()
                    table_ = New DataTable("firstdates_table")
                    table_.Load(SQL_reader)
                    SQL_reader.Close()


                    cnt.Close()
                    table_.DefaultView.Sort = "Date_ ASC"
                    firstdate = table_.Rows(0).Item(4)
                Else

                    firstdate = Now.Date
                End If

            Catch ex As Exception
                Forms.MessageBox.Show("Unable to retrieve data from the database ") ' & ex.Message, vbCritical + vbSystemModal)

                If cnt.State = ConnectionState.Open Then cnt.Close()
                Return Nothing
            End Try


        End If

end_:



        'If (File.Exists(CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
        '    Using streader As New StreamReader(CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")
        '        db_authPath = streader.ReadLine()
        '    End Using
        'End If

        'Dim connectionString As String
        'connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"


        'Dim sqlConn As MySqlConnection = New MySqlConnection(connectionString)
        'sqlConn.Open()

        'If sqlConn.State <> 1 Then
        '    MsgBox("No Connecion With the CSIFlex Database")
        '    Return Nothing
        'Else
        '    Dim tmp_table_cmd As New MySqlCommand
        '    Dim reader As MySqlDataReader
        '    Dim Command As String = "Select * FROM tbl_colors"

        '    temp_table.Clear()


        '    tmp_table_cmd.CommandText = Command
        '    tmp_table_cmd.Connection = sqlConn

        '    reader = tmp_table_cmd.ExecuteReader
        '    temp_table.Load(reader)
        'End If

        'Return temp_table
        Return firstdate

    End Function



    Structure users
        Dim name As String
        Dim password As String
        Dim machines As List(Of String)
        Dim setup__ As List(Of String)
    End Structure


#Region "log error"
    Public Sub LogServerError(ex As String, highPriority As Boolean)
        Try

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
                    writer.Write(vbCrLf & Now & " : " & directory & " Ex: " & ex)
                    writer.Close()
                End Using
            End If

            ' End If

        Catch exxx As Exception
            Console.Out.Write(exxx.ToString())

        End Try
    End Sub


    Public Sub LogServiceError(ex As String, highPriority As Boolean)
        Try

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
                    writer.Write(vbCrLf & Now & " : " & directory & " Excep: " & ex)
                    writer.Close()
                End Using
            End If
        Catch exxx As Exception
            Console.Out.Write(exxx.ToString())
        End Try
    End Sub


    Public Sub LogClientError(ex As String)
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
            If System.IO.Directory.Exists(serverRootPath) Then
                Using writer As StreamWriter = New StreamWriter(serverRootPath & "\EventLog.txt", True)
                    writer.Write(vbCrLf & Now & " : " & event_)
                    writer.Close()
                End Using
            Else
                Using writer As StreamWriter = New StreamWriter("C:\EventLog.txt", True)
                    writer.Write(vbCrLf & Now & " : " & event_)
                    writer.Close()
                End Using
            End If

        Catch exxx As Exception
            Console.Out.Write(exxx.ToString())
        End Try
    End Sub

    Private Sub CleanTable(tablename As String)
        Try
            Dim sqlConn As MySqlConnection = New MySqlConnection(MySqlConnectionString)
            sqlConn.Open()
            Dim cmd As New MySqlCommand("DELETE FROM csi_database." + tablename + " WHERE time >= (Now() - interval 8 hour);", sqlConn)
            cmd.ExecuteNonQuery()
            sqlConn.Close()

        Catch exxx As Exception
            Console.Out.Write(exxx.ToString())
        End Try
    End Sub

#End Region




    Public Function GetLoadingAsCON(cntstr As String) As Boolean
        Dim loadingAsCON As Boolean = False

        Using conn As New MySqlConnection(cntstr)
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
                LogServiceError("ERROR unable to retreive service configs " & ex.Message, 1)
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End Using
        Return loadingAsCON
    End Function

    Public Function installReportViewer() As Boolean
        Dim StartInfo As New System.Diagnostics.ProcessStartInfo
        Dim process_ As New Process
        Dim process2_ As New Process


        Dim path As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
        Dim commandSTRING As String

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
    End Function

    Public Function getRootPath() As String
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

    Private Sub saveCurrentMachineFile(machineFileName As String)

        Try
            Dim Path As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
            Dim directory As String = getRootPath()
            Using writer As StreamWriter = New StreamWriter(directory & "\sys\currentMachineCSV.csys", False)
                writer.Write(machineFileName)
                writer.Close()
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Public Function CheckMachinefile(machineFileName As String) As Boolean

        Dim returnValue As Boolean


        Dim Path As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim directory As String = getRootPath()


        If File.Exists(directory & "\sys\currentMachineCSV.csys") Then
            Using r As StreamReader = New StreamReader(directory & "\sys\currentMachineCSV.csys", False)

                Dim tempfileName As String = r.ReadLine

                Dim fileYear As String() = tempfileName.Split("_")
                Dim StrYear As String = fileYear(UBound(fileYear)).Substring(0, 4)

                Dim currYear As String() = machineFileName.Split("_")
                Dim currstrYear As String = currYear(UBound(currYear)).Substring(0, 4)


                Dim iYear As Integer
                Dim currIYear As Integer


                If (Int32.TryParse(StrYear, iYear) And Int32.TryParse(currstrYear, currIYear)) Then

                    If (iYear = currIYear) Then

                        returnValue = True

                    Else

                        returnValue = False



                    End If

                End If

                r.Close()

                If returnValue = False Then
                    saveCurrentMachineFile(machineFileName)
                End If

            End Using
        Else
            saveCurrentMachineFile(machineFileName)
            returnValue = False
        End If





        Return returnValue

    End Function



    '-----------------------------------------------------------------------------------------------------------------------
    ' Load the users form the database
    '-----------------------------------------------------------------------------------------------------------------------
    Public Function load_users() As List(Of users)
        'Dim rootpath As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
        'Dim path As String = rootpath.DirectoryName
        ' Dim path As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)

        Dim users As New List(Of users)
        '==================================== username ,-----------what(pass,machines....) , list of mahines, pass, setup,,,,,
        Dim usersVSmachines As New Dictionary(Of String, Dictionary(Of String, List(Of String))), loading As Boolean = False

        loading = True
        '*************************************************************************************************************************************************'
        '**** DB Connection
        '*************************************************************************************************************************************************'
        'Dim cnt As OleDb.OleDbConnection
        Dim mySqlcnt As New MySqlConnection(MySqlConnectionString)
        Try



            'cnt = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Data Source=c:\wamp\www\CSI_auth.mdb;")
            'cnt.Open()
            mySqlcnt.Open()

            If mySqlcnt.State = 1 Then
            Else

                LogServerError("Connection to the database failed", 1)
                GoTo End_
            End If
        Catch ex As Exception
            LogServerError(" Unable to establish a connection to the database : " & ex.Message, 1)
            GoTo End_
        End Try

        '*************************************************************************************************************************************************'
        '**** DB Connection -END
        '*************************************************************************************************************************************************'

        'Dim adapter As New OleDbDataAdapter, reader As OleDbDataReader, 
        Dim mysqladap As New MySqlDataAdapter, mysqlreader As MySqlDataReader, mysqlcomm As MySqlCommand
        Dim table_ As New DataTable

        'Dim command As New OleDbCommand("SELECT * FROM [Users]", cnt)
        mysqlcomm = New MySqlCommand("select * from csi_auth.users", mySqlcnt)

        Try
            mysqlreader = mysqlcomm.ExecuteReader
            table_.Load(mysqlreader)

            usersVSmachines.Clear()
            For Each row_ As DataRow In table_.Rows


                If Not (row_("username_") Is Nothing) And
                    Not IsDBNull(row_("username_")) Then



                    Dim PASSWORD As String = AES_Decrypt(row_("password_").ToString(), "pass")


                    Dim listofmachines As New List(Of String)

                    For Each machine In Split(row_("Machines").ToString(), ", ")
                        listofmachines.Add(machine)
                    Next


                    Dim user As New users
                    user.name = row_("name_")
                    user.password = PASSWORD
                    user.machines = listofmachines
                    user.setup__ = Nothing
                    '   If Not usersVSmachines.ContainsKey(row_("username_").Value) Then usersVSmachines.Add(row_("username_").Value, listofmachines)
                    users.Add(user)
                End If
            Next

            mySqlcnt.Close()
        Catch ex As Exception
            LogServerError(ex.Message, 1)
            If mySqlcnt.State = ConnectionState.Open Then mySqlcnt.Close()
        End Try

End_:
        loading = False
        Return users


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
    Private Function mailsToList(mailadresses As String) As String()

        Return mailadresses.Split(";"c)

    End Function

    Public Sub sendReportByMail(mailadresses As String, filePath As String, custommsg As String)

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
                LogServiceError("email error,CONFIG:MailAdresses:" + mailadresses.ToString() + ",port:" + emailconfig.Rows(0)("smtpport").ToString() + ",server:" + emailconfig.Rows(0)("smtphost").ToString() + ",requirecred:" + emailconfig.Rows(0)("requirecred").ToString() + ",email:" + emailconfig.Rows(0)("senderemail").ToString() + ",pwd:" + emailconfig.Rows(0)("senderpwd").ToString() + ",MSG:" + ex.Message, 1)
            End Try

        Else
            LogServiceError("email server not configured for autoreports", 1)
        End If
    End Sub


    Private Function LoadEmailConfig() As DataTable
        Dim results As New DataTable

        Dim cnt As MySqlConnection = New MySqlConnection(MySqlConnectionString)
        Try
            cnt.Open()

            Dim mysqlcmd As New MySqlCommand("Select senderemail, senderpwd, smtphost, smtpport, requirecred from CSI_auth.tbl_emailreports;", cnt)
            Dim mysqladapter As New MySqlDataAdapter(mysqlcmd)

            mysqladapter.Fill(results)

        Catch ex As Exception
            LogServerError("Unable to load reports email configuration:" + ex.Message, 1)
        Finally
            cnt.Close()
        End Try

        Return results

    End Function


    Public Function IsAlphaNumeric(ByVal strToCheck As String) As Boolean
        Dim pattern As RegularExpressions.Regex = New RegularExpressions.Regex("[^a-zA-Z0-9]")
        Return Not pattern.IsMatch(strToCheck)
    End Function


    Function Effectue_Regression(ByRef data2 As Integer(), ByRef data3 As Integer(), ByRef data4 As Integer(), ByRef data5 As Integer())

        Dim Xbarre As Double
        Dim Ybarre As Double
        Dim Numerateur As Double
        Dim Denominateur As Double
        Dim Y_Moyen As Double
        Dim i As Integer
        Dim m As Double
        Dim b As Double
        Dim Precision As Double

        ' Calcul de XBarre et YBarre
        Xbarre = 0
        Ybarre = 0
        For i = 0 To UBound(data2)
            Xbarre = Xbarre + i
            Ybarre = Ybarre + data2(i)
        Next
        Xbarre = Xbarre / UBound(data2)
        Ybarre = Ybarre / UBound(data2)

        'Calcul de m et b
        Numerateur = 0
        Denominateur = 0

        For i = 0 To UBound(data2) - 1
            Numerateur = Numerateur + (i - Xbarre) * (data2(i) - Ybarre)
            Denominateur = Denominateur + (i - Xbarre) ^ 2
        Next

        ' Pente
        m = Numerateur / Denominateur

        ' Constante
        b = Ybarre - m * Xbarre

        ' Precision de l'estimation
        Precision = 0

        For i = 0 To UBound(data2) - 1
            Precision = Precision + ((m * i + b) - data2(i)) ^ 2
        Next

        Y_Moyen = 0

        For i = 0 To UBound(data2) - 1
            Y_Moyen = Y_Moyen + data2(i) ^ 2
        Next i

        Y_Moyen = Y_Moyen / UBound(data2)

        Precision = Precision / Y_Moyen

        Dim point(UBound(data2)) As Integer
        Dim trouve As Boolean = False
        For i = 1 To UBound(data2)
            If data2(i) = 0 And
                data3(i) = 0 And
                data4(i) = 0 And
                data5(i) = 0 And
                trouve = False Then
                point(i) = 0
            Else
                trouve = True
                point(i) = m * i + b + Precision
            End If
        Next

        Return point

    End Function

    '-----------------------------------------------------------------------------------------------------------------------
    ' check if a lic is present in the HD or regedit and check the stored data
    '-----------------------------------------------------------------------------------------------------------------------

    Public Function CheckLic() As String()
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
                    Forms.MessageBox.Show("The license you provided is invalid, if you think it is an error please contact CSIFlex Support.", "Invalid License", MessageBoxButtons.OK)
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
    '''   
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


    '-----------------------------------------------------------------------------------------------------------------------
    ' Gives the eNET path using the file setup_.sys
    '-----------------------------------------------------------------------------------------------------------------------
    Function eNET_path() As String
        'Dim path As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)

        Dim directory As String = getRootPath()

        If File.Exists(directory & "\sys\setup_.csys") Then
            Using reader As StreamReader = New StreamReader(directory & "\sys\setup_.csys")

                Dim path As String = reader.ReadLine()
                'If (path.StartsWith("\")) And Not (path.StartsWith("\\")) Then
                '    path = "\" & path
                'End If
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
    ' Gives the eNET path using the file setup_.sys
    '-----------------------------------------------------------------------------------------------------------------------
    Function Network() As String




        Dim directory As String = serverRootPath
        'Dim machine_stored As New Dictionary(Of String, String) '=Dic(MachineName, IP)

        If File.Exists(directory & "\sys\Network_.csys") Then
            Using reader As StreamReader = New StreamReader(directory & "\sys\Network_.csys")
                Return reader.ReadLine()
            End Using

        Else
            Using writer As StreamWriter = New StreamWriter(directory & "\Log_Server.txt", True)
                writer.Write(vbCrLf & Now & " : " & directory & "\sys\Network_.sys NOT FOUND")
                writer.Close()
            End Using
            Return Nothing
        End If

    End Function

    Function Networkenet() As String

        Dim directory As String = serverRootPath
        'Dim machine_stored As New Dictionary(Of String, String) '=Dic(MachineName, IP)

        If Not isServer Then
            directory = ClientRootPath
        Else
            directory = serverRootPath
        End If

        If File.Exists(directory & "\sys\Networkenet_.csys") Then
            Using reader As StreamReader = New StreamReader(directory & "\sys\Networkenet_.csys")

                Return reader.ReadLine()
            End Using

        Else
            Using writer As StreamWriter = New StreamWriter(directory & "\Log_Server.txt", True)
                writer.Write(vbCrLf & Now & " : " & directory & "\sys\Networkenet_.sys NOT FOUND")
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
    ' the colors from eNET for each status, gives a dictionary of (color,status), needs the eNET dir rep = Form1.chemin_eNET
    '-----------------------------------------------------------------------------------------------------------------------
    Function colors(eNETrep As String) As Dictionary(Of String, String)
        Dim file As System.IO.StreamReader

        Dim color_list As New Dictionary(Of String, String), line As String()

        If Not My.Computer.FileSystem.FileExists(eNETrep + "\_SETUP\GraphColor.csys") Then





        Else
            file = My.Computer.FileSystem.OpenTextFileReader(eNETrep + "\_SETUP\GraphColor.csys")
            While Not file.EndOfStream
                line = file.ReadLine().Split(",")
                If line(1) <> "" Then
                    If color_list.ContainsKey((UCase(line(1).Replace(" ", "")))) Then

                    Else
                        color_list.Add((UCase(line(1).Replace(" ", ""))), line(0).Replace(" ", ""))
                    End If

                End If

            End While
            file.Close()
        End If

        Return color_list

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

            Forms.MessageBox.Show("Error while checking the Framework version : " & ex.Message)
            Return False
        End Try
        Return ok
    End Function






    '-----------------------------------------------------------------------------------------------------------------------
    ' Main funct , takes the schled dates and time !
    '-----------------------------------------------------------------------------------------------------------------------
    Shared Sub Stand(schH As Integer, schM As Integer)
        Dim waitM As Integer ' sec
        Try
            If (Now.Hour * 3600 + Now.Minute * 60 + Now.Second) < (schH * 3600 + schM * 60) Then
                waitM = (schH * 3600 + schM * 60) - (Now.Hour * 3600 + Now.Minute * 60 + Now.Second)
            Else
                waitM = 12 * 3600 - (Now.Hour * 3600 + Now.Minute * 60 + Now.Second) + (schH * 3600 + schM * 60)
            End If

            Thread.Sleep(waitM * 1000) ' wait
        Catch ex As Exception
            Dim checkFileInfo As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
            Using writer As StreamWriter = New StreamWriter(checkFileInfo.DirectoryName & "\Log_auto_reporting.csys", True)
                writer.WriteLine(Now.ToString() & "  :  " & ex.Message.ToString())
                writer.Close()
            End Using
        End Try

    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' check if the data file is available
    '-----------------------------------------------------------------------------------------------------------------------
    Function CheckFile(file_name As String, from As Boolean) As Boolean
        Dim err As String = ""
        Dim checkFileInfo As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim directory As String = getRootPath()

        Try
            If Exists(directory & "\" & file_name) Then
                'Using reader As StreamReader = New StreamReader(checkFileInfo.DirectoryName & "\" & file_name)
                '    path = reader.ReadLine()
                'End Using
            Else
                err = (file_name & " not found")
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
                Return Nothing
            End If
        End If
        Return Nothing
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
                Forms.MessageBox.Show(err)
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
                Forms.MessageBox.Show(err)
            End If
            GoTo stop_
        End If

        Return Path
stop_:
        Return Nothing
    End Function


    Function Checkpath(path_name As String, from As Boolean) As Boolean
        Dim err As String = ""
        Dim checkFileInfo As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)

        If Dir(path_name, vbDirectory) = "" Then
            err = err & " The database path or eNET path not valide"
        End If


        'If Not My.Computer.FileSystem.FileExists(path_name) Then
        '    err = err & "The eNET folder/Database path ,  is not valide or no MonList.csys"
        'End If

        If err <> "" Then
            If from = 0 Then 'Called from the service
                Using writer As StreamWriter = New StreamWriter(checkFileInfo.DirectoryName & "\Log_auto_reporting.csys", True)
                    writer.WriteLine(Now.ToString() & "  :  " & err)
                    writer.Close()
                End Using
                Return True
            Else
                Return Nothing
            End If
        End If
        Return Nothing
    End Function


    Function is_machine(name As String)

        If name.Contains("_MT_") Or name.Contains("_ST_") Then
            Return False
        Else
            Return True
        End If

        'If name.Length > 7 Then
        '    If (name(4) & name(5) & name(6) & name(7) = "_MT_" Or name(4) & name(5) & name(6) & name(7) = "_ST_") Then
        '        Return False
        '    Else
        '        Return True
        '    End If
        'End If

    End Function


    '-----------------------------------------------------------------------------------------------------------------------
    ' mois_ : 
    ' para : Integer, Month in numbers
    ' out : String, first 3 lettres of the month name
    '-----------------------------------------------------------------------------------------------------------------------
    Public Function mois_(MAXMOI As Integer)

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
        End Select


    End Function

    '*************************************************************************************************************************************************'
    '**** Load machines names from _SETUP\MonList.sys
    '*************************************************************************************************************************************************'
    Function LoadMachines() As String()

        license = (CheckLic(2))

        Dim eNETpath As String = eNET_path(True), machine As String(), i As Integer = 0

        Dim file As System.IO.StreamReader
        Try
            If Not My.Computer.FileSystem.FileExists(eNETpath + "\_SETUP\MonList.sys") Then
                Forms.MessageBox.Show("The file 'Monlist.sys' not found in the eNET repertory")
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
            Forms.MessageBox.Show(TraceMessage("Could not load the machines names : " & ex.Message))
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
            Forms.MessageBox.Show(TraceMessage(ex.Message))
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

        'Connection :
        If version_ <> 3 Then
            Try
                cnt_lite = New SQLiteConnection(sqlitedbpath)
                cnt_lite.Open()
                If cnt_lite.State = 1 Then
                Else
                    Forms.MessageBox.Show("Connection to the database failed")
                    GoTo End_
                End If
            Catch ex As Exception
                Forms.MessageBox.Show(" Enable to establish a connection to the database") ' & ex.Message), vbCritical + vbSystemModal)
                GoTo End_
            End Try
        Else
            Dim cntstr As String = ""
            Try
                Dim db_authPath As String = Nothing
                Dim directory As String = getRootPath()
                If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                    Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                        db_authPath = reader.ReadLine()
                    End Using
                End If

                Dim server = db_authPath
                Dim connectionString As String
                connectionString = "SERVER=" + server + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

                cnt_MYSQL = New MySqlConnection(connectionString)
                cnt_MYSQL.Open()

                If cnt_MYSQL.State = 1 Then
                Else
                    Forms.MessageBox.Show("Connection to the database failed")
                    GoTo End_
                End If

                cntstr = connectionString
            Catch ex As Exception
                Forms.MessageBox.Show(" Enable to establish a connection to the database ") ' & ex.Message), vbCritical + vbSystemModal)
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

                'If Part <> "" Then
                '    QryCommande_text = QryCommande_text & " and Partno = '" & Part & "'"
                'Else
                '    Dim Partno_view As DataView = New DataView(table_Partnumbers)
                '    Dim machine_table As DataTable = Partno_view.ToTable(True, "PARTNO")
                '    For Each row As DataRow In machine_table.Rows
                '        QryCommande_text = QryCommande_text & " and PARTNO = '" & row(0).ToString() & "' UNION "
                '    Next
                '    QryCommande_text = QryCommande_text.Substring(0, QryCommande_text.Count - 7)
                'End If





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
                    Dim machine_table As DataTable = Partno_view.ToTable(True, "PARTNO")
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
            Forms.MessageBox.Show(" Enable to query the database " & ex.Message)

            GoTo End_
        End Try
        ' DS_Result.Tables.Add("partsnumbers")
        '  DS_Result.Tables.Add(table_)

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
                        Commande_text = "Select Date_, status, shift, cycletime, partnumber from [tbl_" & RenameMachine(machine.Item(0).ToString()) & "] where time_ between '" & date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + date_end.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'" & " UNION "
                    Else
                        Commande_text = "Select Date_, status, shift, cycletime, partnumber from [tbl_" & RenameMachine(machine.Item(0).ToString()) & "] where time_ between '" & date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + date_start.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'" & " UNION "
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
                        Commande_text = "Select Date_, status, shift, cycletime, partnumber from csi_database.tbl_" & RenameMachine(machine.Item(0).ToString()) & " where time_ between '" & date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + date_end.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'" & " UNION "
                    Else
                        Commande_text = "Select Date_, status, shift, cycletime, partnumber from csi_database.tbl_" & RenameMachine(machine.Item(0).ToString()) & " where time_ between '" & date_start.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + date_start.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'" & " UNION "
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
            Forms.MessageBox.Show(" Enable to add the tables to the dataset " & ex.Message)
            GoTo End_
        End Try
End_:
        If cnt_MYSQL.State = ConnectionState.Open Then cnt_MYSQL.Close()
        Return DS_Result
    End Function



    'Public Function dateToSeperatedStr(date1 As Date) As String()

    '    Dim StrTemp(3) As String

    '    StrTemp(0) = date1.Year - 2000

    '    StrTemp(1) = date1.Month

    '    StrTemp(2) = date1.Day

    '    Return StrTemp

    'End Function
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

        '  Dim results As DateTime()
        Dim k As Integer = 0
        Dim final_time As Double
        Dim total1 As Double = 0
        Dim total2 As Double = 0
        Dim total3 As Double = 0
        '      Dim rang_ As Integer
        Dim DaysInMonth As Integer

        Dim last_loop_ As Boolean = False

        Dim final_time_other(4) As Double
        ' Dim nb_mch As Integer

        final_time = 0


        Dim periode_return As periode()
        Dim mysqldb As Boolean
        mysqldb = True

        If Not (CheckLic(2) = 3) Then
            '*************************************************************************************************************************************************'
            '**** DB Connection
            '*************************************************************************************************************************************************'
            Dim cnt As SQLiteConnection
            mysqldb = False
            Try
                cnt = New SQLiteConnection(sqlitedbpath)
                cnt.Open()

                If cnt.State = 1 Then
                Else
                    Forms.MessageBox.Show("Connection to the database failed")
                    GoTo End_
                End If
            Catch ex As Exception
                Forms.MessageBox.Show(" Enable to establish a connection to the database ") ' & ex.Message), vbCritical + vbSystemModal)
                GoTo End_
            End Try

            '*************************************************************************************************************************************************'
            '**** DB Connection -END
            '*************************************************************************************************************************************************'

            Dim first_insertion As Boolean = True ' for daily reports

            i = 0


            Try
                '******************************************************************************************************************************
                'For each selected machine:====================================================================================================
                '==============================================================================================================================

                For Each items In machine
                    '  For shft = 1 To 3
                    If items <> "" Then
                        machine(i) = RenameMachine(machine(i))

                        k = 0
                        final_time = Nothing

                        total1 = 0
                        total2 = 0
                        total3 = 0

                        ' Building the QueR ====================================================
                        Dim adapter As New SQLiteDataAdapter
                        Dim reader As SQLiteDataReader
                        Dim table_ As DataTable = New DataTable("tmp_table")
                        Dim tmp_table_cmd As New SQLiteCommand

                        Try
                            'tmp_table_cmd.CommandText = "SELECT *  FROM [shift_tbl_" & machine(i) & "] WHERE [year_] = " & year_start & " and [month_]= " & month_start & " and [day_] between " & day_start & " and " & day_end
                            tmp_table_cmd.CommandText = ShiftTableQuery(mysqldb, machine(i), date1, date2)
                            tmp_table_cmd.Connection = cnt
                            reader = tmp_table_cmd.ExecuteReader
                            adapter.SelectCommand = tmp_table_cmd
                            table_.Load(reader)
                        Catch ex As Exception
                            Dim cmd As New SQLiteCommand("CREATE TABLE if not exists tbl_" & machine(i) & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, Partnumber varchar(255), UNIQUE (time_,status))", cnt)
                            cmd.ExecuteNonQuery()

                            cmd = New SQLiteCommand("insert into tbl_renameMachines  (table_name, original_name) VALUES('" & machine(i) & "' , '" & items & "')", cnt)
                            cmd.ExecuteNonQuery()
                            GoTo End_
                        End Try



                        '====================================================================== tmp_table ready.



                        Dim periode_ As New periode
                        periode_.shift1 = New Dictionary(Of String, Double)
                        periode_.shift2 = New Dictionary(Of String, Double)
                        periode_.shift3 = New Dictionary(Of String, Double)

                        ' Select available status ====================================================
                        Dim available_status As DataTable

                        Dim VIEW As DataView = New DataView(table_)

                        For shft = 1 To 3
                            VIEW.RowFilter = "SHIFT =" & shft
                            'ReDim stat(-1)

                            k = 0
                            'ReDim stat(0)

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


                                'for each shift:
                                ' For shft = 1 To 3
                                For l = 0 To UBound(stat)

                                    CycleTime = table_.Compute("sum(cycletime)", "status='" & stat(l) & "' and shift=" & shft)
                                    If IsDBNull(CycleTime) Then CycleTime = 0

                                    'SQLITE
                                    'Dim loadingascon = GetLoadingAsCON()
                                    'If (loadingascon And stat(1) = "LOADING") Then
                                    '    stat(1) = "CYCLE ON"
                                    'End If

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
No_stat:
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
                Next '/item /machine ======================================================================================+==================
                ''============================================================================================================================
            Catch ex As EvaluateException

            Catch ex As Exception
                'Forms.MessageBox.Show(TraceMessage(ex.Message))
                Forms.MessageBox.Show("Error while parsing machine data")
            End Try

no_loop:
            cnt.Close()
            ThirdDim = ThirdDim - 1
        Else
            '*************************************************************************************************************************************************'
            '**** DB Connection
            '*************************************************************************************************************************************************'
            Dim cnt As MySqlConnection
            Dim cntstr As String = ""
            Try
                Dim db_authPath As String = Nothing
                Dim directory As String = getRootPath()
                If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                    Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                        db_authPath = reader.ReadLine()
                    End Using
                End If

                Dim server = db_authPath
                Dim connectionString As String
                connectionString = "SERVER=" + server + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

                cnt = New MySqlConnection(connectionString)
                cnt.Open()

                If cnt.State = 1 Then
                Else
                    Forms.MessageBox.Show("Connection to the database failed")
                    GoTo End_
                End If

                cntstr = connectionString
            Catch ex As Exception
                Forms.MessageBox.Show(" Enable to establish a connection to the database ") ' & ex.Message), vbCritical + vbSystemModal)
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
                        Try
                            tmp_table_cmd.CommandText = ShiftTableQuery(mysqldb, machine(i), date1, date2)
                            tmp_table_cmd.Connection = cnt

                            reader = tmp_table_cmd.ExecuteReader
                            adapter.SelectCommand = tmp_table_cmd
                            table_.Load(reader)
                        Catch ex As Exception

                            MsgBox("There is no data for " & RealNameMachine(machine(i)) & ", It takes at least one shift to have enouth data")

                            'tmp_table_cmd.Connection = cnt
                            'tmp_table_cmd = New MySqlCommand("CREATE TABLE if not exists CSI_Database.tbl_" & machine(i) & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, Partnumber varchar(255), UNIQUE (time_,status))", cnt)
                            'tmp_table_cmd.ExecuteNonQuery()

                            'tmp_table_cmd = New MySqlCommand("insert into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & machine(i) & "' , '" & items & "')", cnt)
                            'tmp_table_cmd.ExecuteNonQuery()
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
                            'ReDim stat(-1)

                            k = 0
                            'ReDim stat(0)

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


                                'for each shift:
                                ' For shft = 1 To 3
                                For l = 0 To UBound(stat)

                                    CycleTime = table_.Compute("sum(cycletime)", "status='" & stat(l) & "' and shift=" & shft)
                                    If IsDBNull(CycleTime) Then CycleTime = 0

                                    'Dim loadingascon = GetLoadingAsCON(cntstr)
                                    'If (stat(1) IsNot Nothing) Then
                                    '    If (loadingasCON And stat(1) = "LOADING") Then
                                    '        stat(1) = "CYCLE ON"
                                    '    End If
                                    'ElseIf (stat(0) = "LOADING") Then
                                    '    stat(0) = "CYCLE ON"
                                    'End If
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
                Next '/item /machine ======================================================================================+==================
                ''============================================================================================================================

            Catch ex As Exception
                'Forms.MessageBox.Show(TraceMessage(ex.Message))
                If Not NO_TABLE Then Forms.MessageBox.Show("Error while parsing machine data")
                If cnt.State = ConnectionState.Open Then cnt.Close()

            End Try

            cnt.Close()
            ThirdDim = ThirdDim - 1
        End If
End_:

        ' If cnt.State = ConnectionState.Open Then cnt.Close()
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

            final_time = 0
            Dim first_insertion As Boolean = True ' for daily reports
            Dim big_periode_return(UBound(machine), 90) As periode

            Dim use_mysql = True

            If Not (CheckLic(2) = 3) Then 'If (isClientSQlite) Then

                use_mysql = False
                '*************************************************************************************************************************************************'
                '**** DB Connection
                '*************************************************************************************************************************************************'
                Dim cnt As SQLiteConnection
                Try
                    cnt = New System.Data.SQLite.SQLiteConnection(sqlitedbpath)
                    cnt.Open()

                    If cnt.State = 1 Then
                    Else
                        Forms.MessageBox.Show("Connection to the database failed")
                        GoTo End_
                    End If
                Catch ex As Exception
                    Forms.MessageBox.Show(" Enable to establish a connection to the database ") '& ex.Message), vbCritical + vbSystemModal)
                    GoTo End_
                End Try
                '*************************************************************************************************************************************************'
                '**** DB Connection -END
                '*************************************************************************************************************************************************'



                '******************************************************************************************************************************
                'For each selected machine:====================================================================================================
                '==============================================================================================================================


                i = 0


                For Each items In machine

                    year_start = date1.Year '- 2000
                    year_end = date2.Year '- 2000

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

                    'day_end = date2.Day
                    'month_end = date2.Month
                    'year_end = date2.Year ' - 2000

                    '  For shft = 1 To 3
                    machine(i) = RenameMachine(machine(i))

                    k = 0
                    final_time = Nothing

                    total1 = 0
                    total2 = 0
                    total3 = 0

                    ' Building the QueR ====================================================
                    Dim adapter As New SQLiteDataAdapter
                    Dim reader As SQLiteDataReader

                    Dim table_ As DataTable = New DataTable("tmp_table")
                    Dim tmp_table_cmd As New SQLiteCommand

                    Dim tmpdate0 As Date = date1.AddDays(-90)

                    'tmp_table_cmd.CommandText = "SELECT *  FROM [shift_tbl_" & machine(i) & "] WHERE [year_] = " & year_start & " and [month_]= " & month_start & " and [day_] between " & day_start & " and " & day_end
                    tmp_table_cmd.CommandText = ShiftTableQuery(use_mysql, machine(i), tmpdate0, date2)
                    tmp_table_cmd.Connection = cnt
                    reader = tmp_table_cmd.ExecuteReader
                    adapter.SelectCommand = tmp_table_cmd
                    table_.Load(reader)


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
                                'DaysInMonth = System.DateTime.DaysInMonth(year_start + 2000, month_start)
                                DaysInMonth = System.DateTime.DaysInMonth(year_start, month_start)
                                VIEW.RowFilter = ("Year_ = " & year_start & " and month_ = " & month_start & " and day_ >= " & day_start & " and day_ <= " & DaysInMonth)
                                VIEW2.RowFilter = ("Year_ = " & year_start & " and month_ = " & month_end & " and day_ >= 1 " & " and day_ <= " & day_end)
                                If month_end - month_start <> 1 Then
                                    VIEW3.RowFilter = ("Year_ = " & year_start & " and month_ >= " & month_start + 1 & " and month_ <=" & month_end - 1)
                                End If

                            End If
                        Else

                            If year_end - year_start = 1 Then
                                'DaysInMonth = System.DateTime.DaysInMonth(year_start + 2000, month_start)
                                DaysInMonth = System.DateTime.DaysInMonth(year_start, month_start)
                                VIEW.RowFilter = ("Year_ = " & year_start & " and month_ = " & month_start & " and day_ >= " & day_start & " and day_ <= " & DaysInMonth)

                                If month_start <> 12 Then VIEW2.RowFilter = ("Year_ = " & year_start & " and month_ >= " & month_start + 1 & " and day_ >= 1 " & " and [month_] <= 12")

                                If month_end <> 1 Then
                                    VIEW2.RowFilter = ("Year_ = " & year_end & " and month_ >= 1 " & " and [month_] <=" & month_end - 1)

                                    'DaysInMonth = System.DateTime.DaysInMonth(year_end + 2000, month_end)
                                    DaysInMonth = System.DateTime.DaysInMonth(year_end, month_end)
                                    VIEW3.RowFilter = ("Year_ = " & year_end & " and month_ =  " & month_end & " and [day_] >= 1 and [day_] <=  " & DaysInMonth)
                                End If

                                VIEW4.RowFilter = ("Year_ = " & year_end & " and month_ = " & month_end & " and day_ >= 1 And day_ <= " & day_end)

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

                                    'SQLITE
                                    'Dim loadingascon = GetLoadingAsCON()
                                    'If (loadingascon And stat(1) = "LOADING") Then
                                    '    stat(1) = "CYCLE ON"
                                    'End If

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
                                        day_end = System.DateTime.DaysInMonth(year_end, month_end) 'System.DateTime.DaysInMonth(year_end + 2000, month_end)
                                    Else
                                        month_end = 12
                                        year_end = year_start - 1
                                        day_end = System.DateTime.DaysInMonth(year_end, month_end) 'System.DateTime.DaysInMonth(year_end + 2000, month_end)
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
                                    day_start = System.DateTime.DaysInMonth(year_start, month_start) - 6 + day_end 'System.DateTime.DaysInMonth(year_start + 2000, month_start) - 6 + day_end
                                Else
                                    month_start = 12
                                    year_start = year_end - 1
                                    day_start = System.DateTime.DaysInMonth(year_start, month_start) - 6 + day_end 'System.DateTime.DaysInMonth(year_start + 2000, month_start) - 6 + day_end
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

            Else

                '*************************************************************************************************************************************************'
                '**** DB Connection
                '*************************************************************************************************************************************************'
                Dim cnt As MySqlConnection
                Dim cntstr As String = ""
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

                    cnt = New MySqlConnection(connectionString)
                    cnt.Open()

                    If cnt.State = 1 Then
                    Else
                        Forms.MessageBox.Show("Connection to the database failed")
                        GoTo End_
                    End If
                    cntstr = connectionString
                Catch ex As Exception
                    Forms.MessageBox.Show(" Enable to establish a connection to the database ") ' & ex.Message), vbCritical + vbSystemModal)
                    GoTo End_
                End Try
                '*************************************************************************************************************************************************'
                '**** DB Connection -END
                '*************************************************************************************************************************************************'


                '******************************************************************************************************************************
                'For each selected machine:====================================================================================================
                '==============================================================================================================================

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

                    'day_end = date2.Day
                    'month_end = date2.Month
                    'year_end = date2.Year '- 2000

                    '  For shft = 1 To 3
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
                    'tmp_table_cmd.CommandText = "SELECT *  FROM shift_tbl_" & machine(i) & " WHERE year_ = " & year_start & " and month_= " & month_start & " and day_ between " & day_start & " and " & day_end
                    ' tmp_table_cmd.CommandText = ShiftTableQuery(use_mysql, machine(i), date1, date2)
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
            'Forms.MessageBox.Show(ex.Message)
            Forms.MessageBox.Show("Error computing evolution chart")

        End Try

    End Function ' With shifts


    '-----------------------------------------------------------------------------------------------------------------------
    ' Generate the data for the time line (3 shift)
    ' Out datatable
    '-----------------------------------------------------------------------------------------------------------------------
    Function TimeLine(Date_ As Date, machine As String, shift As Integer) As DataTable

        Dim tmp_table As DataSet = New DataSet("table")

        Dim year_start As Integer
        Dim year_end As Integer

        Dim month_start As Integer
        Dim month_end As Integer

        Dim day_start As Integer
        Dim day_end As Integer
        Dim second_day_end As Integer

        If Not (CheckLic(2) = 3) Then 'If isClientSQlite Then
            '*************************************************************************************************************************************************'
            '**** DB Connection
            '*************************************************************************************************************************************************'
            Dim cnt As SQLiteConnection
            Try
                cnt = New SQLiteConnection(sqlitedbpath)
                cnt.Open()

                If cnt.State = 1 Then
                Else
                    Forms.MessageBox.Show("Connection to the database failed")
                    Return Nothing
                End If
            Catch ex As Exception
                Forms.MessageBox.Show(" Enable to establish a connection to the database ") ' & ex.Message), vbCritical + vbSystemModal)
                Return Nothing
            End Try
            '*************************************************************************************************************************************************'
            '**** DB Connection -END
            '*************************************************************************************************************************************************'



            year_start = Date_.Year ' - 2000
            year_end = Date_.Year ' - 2000

            month_start = Date_.Month
            month_end = Date_.Month

            day_start = Date_.Day
            day_end = Date_.Day
            second_day_end = Date_.AddDays(1).Day


            Try
                '******************************************************************************************************************************
                'For each selected machine:====================================================================================================
                '==============================================================================================================================

                '  For shft = 1 To 3
                machine = RenameMachine(machine)

                ' Building the QueR ====================================================
                Dim adapter As New SQLiteDataAdapter
                Dim reader As SQLiteDataReader
                Dim table_ As DataTable = New DataTable("tmp_table")
                Dim tmp_table_cmd As New SQLiteCommand

                Dim query As String
                query = "SELECT *  FROM [tbl_" & machine & "] WHERE [year_] = " & year_start & " and [month_]= " & month_start & " and [day_] = " & day_end
                'overnight work

                '''''''''''''''NO NEED TO CHECK FOR OVERNIGHT BECAUSE THE DAYS DON'T CHANGE IN THE CSV FILE FOR SHIFT 3
                'If (shift > 2) Then
                '    Dim nightshift As New DateTime(year_start + 2000, month_start, day_start, 12, 0, 0)
                '    query = "SELECT month_,  case when strftime('%H', date_) = '23' then " & day_end & " else " & day_end + 1 &
                '    " end as day_ , year_, case when strftime('%H', date_) = '23' then date_ else datetime(date_, '+1 day') end as time_, case when strftime('%H', date_) = '23' then date_ else datetime(date_, '+1 day') end as Date_, status, shift, cycletime   FROM [tbl_" & machine & "] WHERE [year_] = " & year_start & " and [month_]= " & month_start & " and [day_] = " & day_end &
                '        " and [shift] = " + shift.ToString()
                'End If

                tmp_table_cmd.CommandText = query '"SELECT *  FROM [tbl_" & machine & "] WHERE [year_] = " & year_start & " and [month_]= " & month_start & " and [day_] = " & day_end
                tmp_table_cmd.Connection = cnt
                reader = tmp_table_cmd.ExecuteReader
                adapter.SelectCommand = tmp_table_cmd
                table_.Load(reader)
                'firstrow.Item("")
                'table_.Rows.InsertAt(firstRow, 0)
                cnt.Close()
                '====================================================================== tmp_table ready.

                Return table_

            Catch ex As Exception
                Forms.MessageBox.Show("Unable to generate Timeline")
                Return Nothing
            End Try
        Else
            '*************************************************************************************************************************************************'
            '**** DB Connection
            '*************************************************************************************************************************************************'
            Dim cnt As MySqlConnection
            Try
                'Dim dbConnectStr As String
                'dbConnectStr = "Provider=Microsoft.Jet.mysql.4.0;Data Source=" & db_path(True) & "\CSI_Database.mdb;" 'db corriger

                Dim db_authPath As String = Nothing
                Dim directory As String = getRootPath()
                If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                    Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                        db_authPath = reader.ReadLine()
                    End Using
                End If
                Dim connectionString As String
                connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"


                cnt = New MySqlConnection(connectionString)
                cnt.Open()

                If cnt.State = 1 Then
                Else
                    Forms.MessageBox.Show("Connection to the database failed")
                    Return Nothing
                End If
            Catch ex As Exception
                Forms.MessageBox.Show(" Enable to establish a connection to the database ") ' & ex.Message, vbCritical + vbSystemModal)
                Return Nothing
            End Try
            '*************************************************************************************************************************************************'
            '**** DB Connection -END
            '*************************************************************************************************************************************************'



            year_start = Date_.Year ' - 2000
            year_end = Date_.Year '- 2000

            month_start = Date_.Month
            month_end = Date_.Month

            day_start = Date_.Day
            day_end = Date_.Day



            Try
                '******************************************************************************************************************************
                'For each selected machine:====================================================================================================
                '==============================================================================================================================

                '  For shft = 1 To 3
                machine = RenameMachine(machine)

                ' Building the QueR ====================================================
                Dim adapter As New MySqlDataAdapter
                Dim reader As MySqlDataReader
                Dim table_ As DataTable = New DataTable("tmp_table")
                Dim tmp_table_cmd As New MySqlCommand

                Dim query As String
                query = "SELECT *  FROM tbl_" & machine & " WHERE year_ = " & year_start & " and month_= " & month_start & " and day_ = " & day_end
                'overnight work
                'If (shift > 2) Then
                '    Dim nightshift As New DateTime(year_start, month_start, day_start, 12, 0, 0)
                '    query = "SELECT month_, case when EXTRACT(HOUR from date_) = '23' then " & day_end & " else " & day_end + 1 &
                '        " end as day_ , year_, case when EXTRACT(HOUR from date_) = '23' then date_ else DATE_ADD(date_,INTERVAL 1 DAY) end as time_," &
                '        "case when EXTRACT(HOUR from date_) = '23' then date_ else DATE_ADD(date_,INTERVAL 1 DAY) end as Date_, status, shift, cycletime" &
                '        " FROM tbl_" & machine & " WHERE year_ = " & year_start & " and month_= " & month_start & " and day_ = " & day_end & " and shift = 3"
                'End If
                '''''''''''''''NO NEED TO CHECK FOR OVERNIGHT BECAUSE THE DAYS DON'T CHANGE IN THE CSV FILE FOR SHIFT 3

                tmp_table_cmd.CommandText = query '"SELECT *  FROM tbl_" & machine & " WHERE year_ = " & year_start & " and month_= " & month_start & " and day_ = " & day_end
                tmp_table_cmd.Connection = cnt
                reader = tmp_table_cmd.ExecuteReader
                adapter.SelectCommand = tmp_table_cmd
                table_.Load(reader)
                'firstrow.Item("")
                'table_.Rows.InsertAt(firstRow, 0)
                cnt.Close()
                '====================================================================== tmp_table ready.

                Return table_

            Catch ex As Exception
                Forms.MessageBox.Show("Unable to generate Timeline")
                Return Nothing
            End Try
        End If

    End Function ' With shifts


    Function TimeLine_from_MON(Date_ As Date, machine As String, shift As Integer, path_enet As String) As DataTable
        Dim month_ As String

        If Date_.Month > 9 Then
            month_ = Date_.Month.ToString()
        Else
            month_ = "0" & Date_.Month.ToString()
        End If

        Dim file As String = Date_.Date.Year & "-" & month_ & "\" & strmonth(Date_.Date.Month) & Date_.Date.Day & "_" & machine & "_SHIFT" & shift & ".MON"
        Dim dt As New DataTable

        Using Reader As New Microsoft.VisualBasic.FileIO.TextFieldParser(path_enet & "\_MONITORING\" & file)
            Reader.TextFieldType = FileIO.FieldType.Delimited
            Reader.SetDelimiters(",")

            Dim currentRow As String()



            dt.Columns.Add("month_", GetType(Integer))
            dt.Columns.Add("day_", GetType(Integer))
            dt.Columns.Add("year_", GetType(Integer))
            dt.Columns.Add("time_", GetType(DateTime))
            dt.Columns.Add("status", GetType(String))
            dt.Columns.Add("shift", GetType(Integer))
            dt.Columns.Add("cycletime", GetType(Integer))

            'Read every line in the CSV
            While Not Reader.EndOfData
                currentRow = Reader.ReadFields()

                Dim realdate_str As String() = currentRow(1).Split("/") ' Split the date
                'Convert to a correct date
                Dim realdate As New DateTime
                Dim realtime As New DateTime
                DateTime.TryParseExact((realdate_str(1) + "/" + realdate_str(0) + "/" + realdate_str(2)), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, realdate)
                DateTime.TryParseExact((realdate_str(1) + "/" + realdate_str(0) + "/" + realdate_str(2)) + " " + currentRow(3), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, realdate)

                Dim newrow As DataRow = dt.NewRow
                newrow("month_") = realdate.Date.Month
                newrow("day_") = realdate.Date.Day
                newrow("year_") = realdate.Date.Year '- 2000
                newrow("time_") = realtime
                newrow("Status") = currentRow(6)
                newrow("cycletime") = currentRow(5)
                newrow("shift") = shift

                dt.Rows.Add(newrow)

            End While

        End Using
        Return dt
    End Function


#End Region

#Region "read eNET setup files"

    ' Gives a dic of strings, string ( groupe,number : mAchine ) 
    Function Machines_Setup(eNETrep As String) As Dictionary(Of String, String)
        Try
            Dim file As System.IO.StreamReader, line As String, toreturn As New Dictionary(Of String, String), previous_line As String = "", grnum As String()


            If Not My.Computer.FileSystem.FileExists(eNETrep + "\_SETUP\eHUBConf.csys") Then
                Forms.MessageBox.Show("The eNET file eHUBConf.sys is not accessible. ")
                Return Nothing
            Else
                file = My.Computer.FileSystem.OpenTextFileReader(eNETrep + "\_SETUP\eHUBConf.csys")
                While Not file.EndOfStream
                    line = file.ReadLine()
                    If line.StartsWith("NM") And previous_line.EndsWith(":") Then
                        grnum = previous_line.Split(":")
                        toreturn.Add(grnum(0) & ":", line.Substring(3, line.Length - 3))
                    End If

                    previous_line = line
                End While
                file.Close()
                Return toreturn
            End If
        Catch ex As Exception
            Forms.MessageBox.Show("CSIFLEX Cannot read eHUBConf.sys : " & ex.Message)
            Return Nothing
        End Try

    End Function

    'which dep for each mch ?, gives a dic of str,str (  machine dep)
    Function Departement_Setup(eNETrep As String, mch_setup As Dictionary(Of String, String)) As Dictionary(Of String, String)
        Try
            Dim file As System.IO.StreamReader, line As String, toreturn As New Dictionary(Of String, String), Groupe_Number As String = ""

            If Not My.Computer.FileSystem.FileExists(eNETrep + "\_SETUP\MonSetup.csys") Then
                Forms.MessageBox.Show("The eNET file MonSetup.sys is not accessible. ")
                Return Nothing
            Else
                file = My.Computer.FileSystem.OpenTextFileReader(eNETrep + "\_SETUP\MonSetup.csys")
                While Not file.EndOfStream
                    line = file.ReadLine()
                    If line.Length > 1 Then

                        If (line(1) = "," Or line(2) = ",") And line.EndsWith(":") Then Groupe_Number = line
                        If line.StartsWith("DA:") Then
                            If line.Substring(3, line.Length - 3) <> "" Then toreturn.Add(mch_setup.Item(Groupe_Number), line.Substring(3, line.Length - 3))
                        End If
                    End If
                End While
                file.Close()
                Return toreturn
            End If
        Catch ex As Exception
            Forms.MessageBox.Show("CSIFLEX Cannot read MonSetup.sys : " & ex.Message)
            Return Nothing
        End Try

    End Function

    'shift start and end for dep, gives a dic of str,str (dep, start-end) , start-end = sh1_start,sh1_end,sh2_start,sh2_end,sh3_start,sh3_end , /3600 to have hours 
    ' uses a dic of (  machine dep)
    Function Shift_Setup(eNETrep As String, mch_dep As Dictionary(Of String, String)) As Dictionary(Of String, String)
        Try
            Dim file As System.IO.StreamReader, line As String(), toreturn As New Dictionary(Of String, String), Groupe_Number As String = ""
            Dim read As Integer = 0 ' read every 4 lines
            Dim mch_SH_HOURS As New Dictionary(Of String, String)
            If Not My.Computer.FileSystem.FileExists(eNETrep + "\_SETUP\ShiftSetup2.csys") Then
                Forms.MessageBox.Show("The eNET file ShiftSetup2.sys is not accessible. ")
                Return Nothing
            Else
                file = My.Computer.FileSystem.OpenTextFileReader(eNETrep + "\_SETUP\ShiftSetup2.csys")
                While Not file.EndOfStream
                    line = file.ReadLine().Split(",")
                    If UBound(line) > 1 Then
                        If read = 4 Or read = 0 Then
                            read = 0
                            toreturn.Add(line(0), line(1) & "," & line(2) & "," & line(3) & "," & line(4) & "," & line(5) & "," & line(6))
                        End If
                        read += 1
                    End If

                End While
                file.Close()

                If Not IsNothing(toreturn) Then
                    For Each item In mch_dep
                        If item.Value <> "" Then mch_SH_HOURS.Add(item.Key.Replace(" ", ""), toreturn.Item(item.Value)) ' value = dep, key = machine, mch_dep.Item(item.Value) = hours
                    Next
                End If

                Return mch_SH_HOURS
            End If
        Catch ex As Exception
            Forms.MessageBox.Show("CSIFLEX Cannot read ShiftSetup2.sys : " & ex.Message)
            Return Nothing
        End Try

    End Function

#End Region

    Function Read_setup(param As String) As String

        Dim tmp_string As String = "" 'ReadToEnd
        Dim tmp_string_tab As String() ' splitted by line
        Dim value_to_return As String()
        Dim Path As String = Assembly.GetEntryAssembly().Location
        Dim directory As String = getRootPath()

        If Exists(directory & "\sys\CSI_Setup_.csys") Then
            Using reader As StreamReader = New StreamReader(directory & "\sys\CSI_Setup_.csys")
                tmp_string = reader.ReadToEnd
                tmp_string_tab = tmp_string.Split(vbCrLf)
            End Using

            For Each item In tmp_string_tab
                If item.StartsWith(param) Then
                    value_to_return = item.Split(" : ")
                    Return value_to_return(1)
                End If
            Next
            Return ""
        Else
            Forms.MessageBox.Show(TraceMessage("The file CSI_Setup_.sys is not accessible, please restart CSIFLEX to regenerate it"))
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Lods Daily Tarets 
    ''' </summary>
    ''' <returns></returns>
    Public Function Load_DailyTargets() As Dictionary(Of String, Integer)

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

    End Function
    ''' <summary>
    ''' Load Weekly Targets
    ''' </summary>
    ''' <returns></returns>
    Public Function Load_WeeklyTargets() As Dictionary(Of String, Integer)
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

    End Function
    ''' <summary>
    ''' Load Monthly Targets 
    ''' </summary>
    ''' <returns></returns>
    Public Function Load_MonthlyTargets() As Dictionary(Of String, Integer)
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

    End Function


    Public Function updateDailytarget(MachineName As String, Value As Integer) As String
        Dim ReturnState As String = ""
        Try
            If TargetsDaily_dic IsNot Nothing Then
                If TargetsDaily_dic.ContainsKey(MachineName) Then
                    TargetsDaily_dic(MachineName) = Value
                Else
                    TargetsDaily_dic.Add(MachineName, Value)
                End If
            End If

            If File.Exists(serverRootPath & "\sys\" & "_" & "Dailytargets.bin") Then File.Delete((serverRootPath & "\sys\" & "_" & "Dailytargets.bin"))
            Dim fs As IO.FileStream = New IO.FileStream((serverRootPath & "\sys\" & "_" & "Dailytargets.bin"), IO.FileMode.OpenOrCreate)
            Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            bf.Serialize(fs, TargetsDaily_dic)
            fs.Close()

        Catch ex As Exception
            LogServerError("Could not save the updateDailytarget", ex.Message)
            Return "Could not save the updateDailytarget" + ex.Message.ToString()
        End Try
        Return "ok"
    End Function
    Public Function updateWeeklytarget(MachineName As String, Value As Integer) As String
        Dim ReturnState As String = ""
        Try
            If TargetsWeekly_dic IsNot Nothing Then
                If TargetsWeekly_dic.ContainsKey(MachineName) Then
                    TargetsWeekly_dic(MachineName) = Value
                Else
                    TargetsWeekly_dic.Add(MachineName, Value)
                End If
            End If

            If File.Exists(serverRootPath & "\sys\" & "_" & "weeklytargets.bin") Then File.Delete((serverRootPath & "\sys\" & "_" & "weeklytargets.bin"))
            Dim fs As IO.FileStream = New IO.FileStream((serverRootPath & "\sys\" & "_" & "weeklytargets.bin"), IO.FileMode.OpenOrCreate)
            Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            bf.Serialize(fs, TargetsWeekly_dic)
            fs.Close()

        Catch ex As Exception
            LogServerError("Could not save the updateweeklytarget", ex.Message)
            Return "Could not save the updateweeklytarget" + ex.Message.ToString()
        End Try
        Return "ok"
    End Function
    Public Function updateMonthlytarget(MachineName As String, Value As Integer) As String
        Dim ReturnState As String = ""
        Try
            If TargetsMonthly_dic IsNot Nothing Then
                If TargetsMonthly_dic.ContainsKey(MachineName) Then
                    TargetsMonthly_dic(MachineName) = Value
                Else
                    TargetsMonthly_dic.Add(MachineName, Value)
                End If
            End If

            If File.Exists(serverRootPath & "\sys\" & "_" & "Monthlytargets.bin") Then File.Delete((serverRootPath & "\sys\" & "_" & "Monthlytargets.bin"))
            Dim fs As IO.FileStream = New IO.FileStream((serverRootPath & "\sys\" & "_" & "Monthlytargets.bin"), IO.FileMode.OpenOrCreate)
            Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            bf.Serialize(fs, TargetsMonthly_dic)
            fs.Close()

        Catch ex As Exception
            LogServerError("Could not save the updateMonthlytarget", ex.Message)
            Return "Could not save the updateMonthlytarget" + ex.Message.ToString()
        End Try
        Return "ok"
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


    '===============================================================================================
    ' write in THE setup file
    ' RETURN : boolean , true if succes
    ' PARA : string x 2 , param and value
    '===============================================================================================

    Function write_setup(param As String, value As String) As Boolean
        Dim tmp_string As String = "" ' CSI_SETUP_.sys tothe end
        Dim tmp_string_tab As String() '  CSI_SETUP_.sys splited by lines
        '  Dim value_to_return As String()
        Dim found As Boolean = False
        Dim path As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
        Dim directory As String = getRootPath()

        Try
            If Exists(directory & "\sys\CSI_Setup_.csys") Then
                ' Read the actual File :
                Using sr As StreamReader = New StreamReader(directory & "\sys\CSI_Setup_.csys", True)
                    tmp_string = sr.ReadToEnd()
                    sr.Close()
                End Using
                tmp_string_tab = tmp_string.Split(vbCrLf)


                ' Modify the information
                For Each item In tmp_string_tab
                    If item.StartsWith(param) Then
                        item = param + " : " + value
                        found = True
                        Exit For
                    End If
                Next
            End If

            Dim header As String = "***************************" + vbCrLf + "* DO NOT MODIFY THIS FILE *" + vbCrLf + "***************************"
            'write the new value:
            Using sw As StreamWriter = New StreamWriter(directory & "\sys\CSI_Setup_.csys", True)
                sw.WriteLine(header)
                For Each item In tmp_string_tab
                    sw.WriteLine(item)
                Next
                sw.Close()
            End Using

            Return True

        Catch ex As Exception
            Forms.MessageBox.Show(TraceMessage("The file CSI_Setup_.sys is not accessible : " & ex.Message & ""))
            Return False

        End Try
    End Function


#Region "Database Update procedure"

    Public Sub createMySqlDb()

        Dim db_authPath As String = Nothing
        Dim directory As String = getRootPath()

        If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
            Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                db_authPath = reader.ReadLine()
            End Using
        End If

        Dim server = db_authPath
        Dim connectionString As String
        ' connectionString = "SERVER=" + server + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"
        connectionString = MySqlConnectionString

        Dim mySqlConn As MySqlConnection = New MySqlConnection(connectionString)
        Dim mySqlComm As MySqlCommand

        Try

            mySqlConn.Open()

            mySqlComm = New MySqlCommand("CREATE DATABASE IF NOT EXISTS CSI_database", mySqlConn)
            mySqlComm.ExecuteNonQuery()

            mySqlConn.Close()

        Catch ex As Exception

            Console.Out.WriteLine(ex.ToString())

        End Try

    End Sub


    Public Sub StartWamp()

        If (File.Exists("C:\wamp\wampmanager.exe")) Then
            Try
                Process.Start("C:\wamp\wampmanager.exe")
            Catch ex As Exception
                LogServerError("There was an error trying to launch wamp. Please reinstall the software", 1)
                Environment.Exit(0)
            End Try

        Else
            Forms.MessageBox.Show("Please Install Wamp Server before launching CSI Flex Server")
            Environment.Exit(0)
        End If

        Thread.Sleep(2000)

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


    Public Function getFirstUpdateOrNot() As Boolean 'SERVER SIDE
        Dim initialdbload As Boolean = False

        Using mysqlcon As New MySqlConnection(MySqlConnectionString)
            Try
                mysqlcon.Open()
                Using mysqlcmd As New MySqlDataAdapter("select initialdbload from csi_auth.tbl_updatestatus;", mysqlcon)
                    Dim dt As New DataTable
                    mysqlcmd.Fill(dt)
                    initialdbload = dt.Rows(0)("initialdbload")
                End Using
                mysqlcon.Close()
            Catch ex As Exception
                LogServerError("GetFirstUpdateOrNot: unable to load service config:" + ex.Message, 1)
            End Try
        End Using
        Return initialdbload
        'End If

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
                LogServerError("unable to update service status:" + ex.Message, 1)
                If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
            End Try
        End Using
    End Sub

    Public Sub ImportDB_Mysql(pathCSV As String)

        Try
            'Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim machcsvfile As String = ""
            ''Dim MostRecentCSV As Integer = 0
            'For Each File In files
            If System.IO.Path.GetFileName(pathCSV).StartsWith("_MACHINE_") And System.IO.Path.GetExtension(pathCSV) = ".CSV" Then
                If System.IO.Path.GetFileName(pathCSV).Count = 17 Then
                    machcsvfile = pathCSV

                    'Next

                    Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
                    ' Change culture to en-US.

                    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


                    Dim machlastupdate As New Dictionary(Of String, DateTime)
                    Dim machlistdt As New DataTable
                    Dim minlastdate As DateTime = DateTime.Now.AddMonths(-3)

                    Using mysqlcon As New MySqlConnection(MySqlConnectionString)
                        mysqlcon.Open()
                        'Using mysqlcmd As New MySqlCommand("SELECT *", mysqlcon)

                        'mysqlcmd.ExecuteReader()
                        Dim mysqlda As New MySqlDataAdapter("Select table_name,original_name from csi_database.tbl_renamemachines ", mysqlcon)

                        mysqlda.Fill(machlistdt)


                        For Each row In machlistdt.Rows

                            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
                            Using mysqlcmd As New MySqlCommand("Select min(Date_) as mindate from CSI_Database.tbl_" & row("table_name"), mysqlcon)
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

                        'End Using
                        mysqlcon.Close()
                    End Using



                    Dim endline As Long = File.ReadLines(machcsvfile).Count()
                    'Dim endline As Long = 0

                    Try
                        'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                        Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                        If (minlastdate < DateTime.Now.AddMonths(-3)) Then
                            'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                            firstdate = CreateDateStr(minlastdate)
                        End If



                        For Each match In File.ReadLines(machcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                            'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                            endline = match.lineNumber
                            Exit For
                        Next
                    Catch ex As Exception
                        LogServerError("Unable to find end line:" + ex.Message, 1)
                    End Try



                    beginTransac(True)

                    'Dim machlist As New List(Of String)

                    'open the file "data.csv" which is a CSV file with headers
                    Using csv As New CsvReader(New StreamReader(machcsvfile), True, ","c)

                        Dim fieldCount As Integer = csv.FieldCount
                        csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                        csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                        Dim headers As String() = csv.GetFieldHeaders()

                        'csv.MoveTo(startline)

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

                                    'Dim sqlConn As MySqlConnection = New MySqlConnection(MySqlConnectionString)
                                    'sqlConn.Open()
                                    cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqlInserConn)
                                    cmd.ExecuteNonQuery()

                                    cmd = New MySqlCommand("CREATE TABLE if not exists CSI_Database.tbl_" & renamedmachine & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, partnumber varchar(255), UNIQUE KEY (time_,status))", sqlInserConn)
                                    cmd.ExecuteNonQuery()

                                    'sqlConn.Close()
                                End If


                                'csv line example
                                'GANESH,07/23/2015,2,23:28:03,01:30:00,7317,CYCLE ON,0,
                                cmd = New MySqlCommand("Replace into CSI_Database.tbl_" & renamedmachine +
                                                 "(month_, day_, year_, time_, Date_, status, shift, cycletime, partnumber)" +
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
                                LogServerError("Error parsing csv file:" + ex.Message, 1)
                            End Try
                            linecnt += 1
                        End While

                    End Using

                    beginTransac(False)
                    Thread.CurrentThread.CurrentCulture = originalCulture

                End If
            End If
        Catch ex As Exception
            LogServerError("unable to complete database creation:" + ex.Message, 1)
        End Try

    End Sub



    Public Sub FirstUpdateDB_Mysql(years__ As String)


        Dim sqlConn As MySqlConnection = New MySqlConnection(MySqlConnectionString)
        sqlConn.Open()
        Dim cmd As String = "CREATE TABLE if not exists csi_database.tbl_CSIFLEX_VERSION (version integer PRIMARY KEY);"
        Dim cmdCreateDeviceTable_version As New MySqlCommand(cmd, sqlConn)
        cmdCreateDeviceTable_version.ExecuteNonQuery()


        cmd = "insert ignore into csi_database.tbl_CSIFLEX_VERSION (version) VALUES('" & CSI_DATA.CSIFLEX_VERSION.ToString() & "');"
        cmdCreateDeviceTable_version = New MySqlCommand(cmd, sqlConn)
        cmdCreateDeviceTable_version.ExecuteNonQuery()

        cmd = "GRANT SELECT ON *.* to 'CRM'@'%' identified by 'CRM';"

        ' cmd = "Create USER 'CRM'@'%' IDENTIFIED BY 'CRM';"
        cmdCreateDeviceTable_version = New MySqlCommand(cmd, sqlConn)
        cmdCreateDeviceTable_version.ExecuteNonQuery()

        'cmd = "GRANT SELECT ON *.* TO 'CRM'@'%' IDENTIFIED BY 'CRM';"
        'cmdCreateDeviceTable_version = New MySqlCommand(cmd, sqlConn)
        'cmdCreateDeviceTable_version.ExecuteNonQuery()

        sqlConn.Close()




        FirstOEEUpdate_MySQL(years__)
        FirstOperatorUpdate_MySQL(years__)
        FirstPartnumberUpdate_MySQL(years__)
        FirstReportedpartUpdate_MySQL(years__)
        FirstMachineUpdate_MySQL(years__)

    End Sub

#Region "SQL first update"
    'Private Sub FirstOEEUpdate_MySQL()

    '    Try
    '        Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
    '        Dim oeecsvfile As String = ""
    '        For Each File In files
    '            If System.IO.Path.GetFileName(File).StartsWith("_OEE_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
    '                oeecsvfile = File
    '            End If
    '        Next


    '        Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
    '        ' Change culture to en-US.

    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


    '        'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
    '        Dim startline As Long = 0

    '        Try
    '            'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
    '            Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

    '            For Each match In File.ReadLines(oeecsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
    '                'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
    '                startline = match.lineNumber - 1
    '                Exit For
    '            Next
    '        Catch ex As Exception
    '            LogServerError("Unable to find start line:" + ex.Message)
    '        End Try


    '        beginTransac(True)

    '        Dim cmd As New MySqlCommand

    '        Try
    '            Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_oee (`MACHINE` varchar(75) NOT NULL,  `trx_time` datetime NOT NULL, `Avail` float NOT NULL, `Performance` float NOT NULL, `Quality` float NOT NULL, `OEE` float NOT NULL,`HEADPALLET` int);"

    '            cmd = New MySqlCommand(cmdstr, sqlInserConn)

    '            'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

    '            cmd.ExecuteNonQuery() 'async doesnt wait for finish

    '            Dim machlist As New List(Of String)

    '            'open the file "data.csv" which is a CSV file with headers
    '            Using csv As New CsvReader(New StreamReader(oeecsvfile), True, ","c)

    '                Dim fieldCount As Integer = csv.FieldCount
    '                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
    '                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

    '                Dim headers As String() = csv.GetFieldHeaders()

    '                csv.MoveTo(startline)

    '                'Dim cmd As New MySqlCommand
    '                Dim tempdate As New DateTime()
    '                Dim temptime As New DateTime()
    '                'Dim status As String

    '                While (csv.ReadNextRecord())

    '                    Try
    '                        Dim renamedmachine As String = RenameMachine(csv(0))

    '                        If Not (machlist.Contains(csv(0))) Then
    '                            machlist.Add(csv(0))

    '                            cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqlInserConn)
    '                            cmd.ExecuteNonQuery()
    '                        End If

    '                        'csv line example
    '                        'OKUMA,09/24/2016,13:25:52,0.05,85.71,83.33,0.04,0
    '                        cmd = New MySqlCommand("Replace into CSI_Database.tbl_oee" +
    '                                         " (MACHINE, trx_time, Avail, Performance, Quality, OEE, HEADPALLET)" +
    '                                         " VALUES (@MACHINE,@trx_time,@Avail,@Performance,@Quality,@OEE,@HEADPALLET);", sqlInserConn)
    '                        tempdate = csv(1) 'row("DATE")
    '                        temptime = csv(2) 'row("START TIME")
    '                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

    '                        cmd.Parameters.Add(New MySqlParameter("@MACHINE", csv(0)))
    '                        cmd.Parameters.Add(New MySqlParameter("@trx_time", temptime))
    '                        cmd.Parameters.Add(New MySqlParameter("@Avail", csv(3)))
    '                        cmd.Parameters.Add(New MySqlParameter("@Performance", csv(4)))
    '                        cmd.Parameters.Add(New MySqlParameter("@Quality", csv(5)))
    '                        cmd.Parameters.Add(New MySqlParameter("@OEE", csv(6)))
    '                        cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(7))) 'row("SHIFT")))

    '                        cmd.ExecuteNonQuery() 'async doesnt wait for finish


    '                    Catch ex As Exception
    '                        LogServerError("Error parsing oee csv file:" + ex.Message)
    '                    End Try
    '                End While

    '            End Using

    '        Catch ex As Exception
    '            LogServerError("Error uploading oee csv file:" + ex.Message)
    '        End Try

    '        beginTransac(False)
    '        Thread.CurrentThread.CurrentCulture = originalCulture

    '    Catch ex As Exception
    '        LogServerError("unable to complete OEE Table creation:" + ex.Message)
    '    End Try
    'End Sub

    'Private Sub FirstOperatorUpdate_MySQL()

    '    Try
    '        Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
    '        Dim opercsvfile As String = ""
    '        For Each File In files
    '            If System.IO.Path.GetFileName(File).StartsWith("_OPERATOR_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
    '                opercsvfile = File
    '            End If
    '        Next


    '        Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
    '        ' Change culture to en-US.

    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


    '        'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
    '        Dim startline As Long = 0

    '        Try
    '            'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
    '            Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

    '            For Each match In File.ReadLines(opercsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
    '                'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
    '                startline = match.lineNumber - 1
    '                Exit For
    '            Next
    '        Catch ex As Exception
    '            LogServerError("Unable to find start line:" + ex.Message)
    '        End Try


    '        beginTransac(True)

    '        Dim cmd As New MySqlCommand

    '        Try
    '            Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_operator (`OPERATOR` varchar(150) NOT NULL,  `PARTNO` varchar(255) NOT NULL, `QUANTITY` int NOT NULL, `AVGCYCLETIME` time NOT NULL, `GOODCYCLE` int NOT NULL, `AVGGOODCYCLETIME` time NOT NULL, `trx_date` date not null, `shift` int NOT NULL, `HEADPALLET` int);"

    '            cmd = New MySqlCommand(cmdstr, sqlInserConn)

    '            'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

    '            cmd.ExecuteNonQuery() 'async doesnt wait for finish

    '            Dim machlist As New List(Of String)

    '            'open the file "data.csv" which is a CSV file with headers
    '            Using csv As New CsvReader(New StreamReader(opercsvfile), True, ","c)

    '                Dim fieldCount As Integer = csv.FieldCount
    '                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
    '                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

    '                Dim headers As String() = csv.GetFieldHeaders()

    '                csv.MoveTo(startline)

    '                ''Dim cmd As New MySqlCommand
    '                Dim tempdate As New DateTime()
    '                'Dim temptime As New DateTime()
    '                ''Dim status As String

    '                While (csv.ReadNextRecord())

    '                    Try

    '                        'csv line example
    '                        '6021,212-100-10,49,00:01:47,0,00:00:00,01/05/2016,1,0
    '                        cmd = New MySqlCommand("Replace into CSI_Database.tbl_operator" +
    '                                         " (OPERATOR, PARTNO, QUANTITY, AVGCYCLETIME, GOODCYCLE, AVGGOODCYCLETIME, trx_date, shift, HEADPALLET)" +
    '                                         " VALUES (@OPERATOR,@PARTNO,@QUANTITY,@AVGCYCLETIME,@GOODCYCLE,@AVGGOODCYCLETIME,@trx_date,@shift,@HEADPALLET);", sqlInserConn)

    '                        tempdate = csv(6)
    '                        cmd.Parameters.Add(New MySqlParameter("@OPERATOR", csv(0)))
    '                        cmd.Parameters.Add(New MySqlParameter("@PARTNO", csv(1)))
    '                        cmd.Parameters.Add(New MySqlParameter("@QUANTITY", csv(2)))
    '                        cmd.Parameters.Add(New MySqlParameter("@AVGCYCLETIME", csv(3)))
    '                        cmd.Parameters.Add(New MySqlParameter("@GOODCYCLE", csv(4)))
    '                        cmd.Parameters.Add(New MySqlParameter("@AVGGOODCYCLETIME", csv(5)))
    '                        cmd.Parameters.Add(New MySqlParameter("@trx_date", tempdate))
    '                        cmd.Parameters.Add(New MySqlParameter("@shift", csv(7)))
    '                        cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(8)))

    '                        cmd.ExecuteNonQuery() 'async doesnt wait for finish


    '                    Catch ex As Exception
    '                        LogServerError("Error parsing operator csv file:" + ex.Message)
    '                    End Try
    '                End While

    '            End Using

    '        Catch ex As Exception
    '            LogServerError("Error uploading operator csv file:" + ex.Message)
    '        End Try

    '        beginTransac(False)
    '        Thread.CurrentThread.CurrentCulture = originalCulture

    '    Catch ex As Exception
    '        LogServerError("unable to complete OPERATOR Table creation:" + ex.Message)
    '    End Try
    'End Sub

    'Private Sub FirstPartnumberUpdate_MySQL()

    '    Try
    '        Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
    '        Dim partcsvfile As String = ""
    '        For Each File In files
    '            If System.IO.Path.GetFileName(File).StartsWith("_PARTNUMBER_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
    '                partcsvfile = File
    '            End If
    '        Next


    '        Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
    '        ' Change culture to en-US.

    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


    '        'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
    '        Dim startline As Long = 0

    '        Try
    '            'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
    '            Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

    '            For Each match In File.ReadLines(partcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
    '                'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
    '                startline = match.lineNumber - 1
    '                Exit For
    '            Next
    '        Catch ex As Exception
    '            LogServerError("Unable to find start line:" + ex.Message)
    '        End Try


    '        beginTransac(True)

    '        Dim cmd As New MySqlCommand

    '        Try
    '            'DATE    ,START TIME,END TIME,ELAPSED TIME,MACHINE NAME,HEAD/PALLET,SHIFT,PART NUMBER,TOTAL CYC,GOOD CYC,SHORT CYC,LONG CYC,AVG GOOD CYC TIME,MAX CYC TIME,MIN CYC TIME
    '            '01/05/2016, 05:00:02, 12:10:07, 25805, 420, 0, 1, 212-100-10, 50, 0, 50, 0, 00:00:00, 00:08:11, 00:00:02
    '            Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_partnumber (`start_time` datetime NOT NULL,  `end_time` datetime NOT NULL, `elapsed_time` int NOT NULL, `machine` varchar(255) NOT NULL," +
    '                " `HEADPALLET` int NOT NULL, `shift` int NOT NULL, `Partno` varchar(255) not null, `total_cycle` int NOT NULL, `good_cycle` int not null, `short_cycle` int not null," +
    '                " `long_cycle` int not null, `avg_good_cycle_time` time not null, `max_cycle_time` time not null, `min_cycle_time` time not null);"

    '            cmd = New MySqlCommand(cmdstr, sqlInserConn)

    '            'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

    '            cmd.ExecuteNonQuery() 'async doesnt wait for finish

    '            Dim machlist As New List(Of String)

    '            'open the file "data.csv" which is a CSV file with headers
    '            Using csv As New CsvReader(New StreamReader(partcsvfile), True, ","c)

    '                Dim fieldCount As Integer = csv.FieldCount
    '                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
    '                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

    '                Dim headers As String() = csv.GetFieldHeaders()

    '                csv.MoveTo(startline)

    '                ''Dim cmd As New MySqlCommand
    '                Dim tempdate As New DateTime()
    '                Dim temptime As New DateTime()
    '                ''Dim status As String

    '                While (csv.ReadNextRecord())

    '                    Try
    '                        Dim renamedmachine As String = RenameMachine(csv(4))

    '                        If Not (machlist.Contains(csv(4))) Then
    '                            machlist.Add(csv(4))

    '                            cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(4) & "')", sqlInserConn)
    '                            cmd.ExecuteNonQuery()
    '                        End If

    '                        'DATE    ,START TIME,END TIME,ELAPSED TIME,MACHINE NAME,HEAD/PALLET,SHIFT,PART NUMBER,TOTAL CYC,GOOD CYC,SHORT CYC,LONG CYC,AVG GOOD CYC TIME,MAX CYC TIME,MIN CYC TIME
    '                        '01/05/2016, 05:00:02, 12:10:07, 25805, 420, 0, 1, 212-100-10, 50, 0, 50, 0, 00:00:00, 00:08:11, 00:00:02
    '                        '        Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_partnumber (`start_time` datetime NOT NULL,  `end_time` datetime NOT NULL, `elapsed_time` int NOT NULL, `machine` varchar(255) NOT NULL," +
    '                        '" `HEADPALLET` int NOT NULL, `shift` int NOT NULL, `Partno` varchar(255) not null, `total_cycle` int NOT NULL, `good_cycle` int not null, `short_cycle` int not null," +
    '                        '" `long_cycle` int not null, `avg_good_cycle_time` time not null, `max_cycle_time` time not null, `min_cycle_time` time not null);"

    '                        cmd = New MySqlCommand("Replace into CSI_Database.tbl_partnumber" +
    '                                         " (start_time, end_time, elapsed_time, machine, HEADPALLET, shift, Partno, total_cycle, good_cycle, short_cycle, long_cycle, avg_good_cycle_time, max_cycle_time, min_cycle_time)" +
    '                                         " VALUES (@start_time,@end_time,@elapsed_time,@machine,@HEADPALLET,@shift,@Partno,@total_cycle,@good_cycle,@short_cycle,@long_cycle,@avg_good_cycle_time,@max_cycle_time,@min_cycle_time);", sqlInserConn)

    '                        tempdate = csv(0)
    '                        temptime = csv(1)
    '                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
    '                        cmd.Parameters.Add(New MySqlParameter("@start_time", temptime))
    '                        temptime = csv(2)
    '                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
    '                        cmd.Parameters.Add(New MySqlParameter("@end_time", temptime))
    '                        cmd.Parameters.Add(New MySqlParameter("@elapsed_time", csv(3)))
    '                        cmd.Parameters.Add(New MySqlParameter("@machine", csv(4)))
    '                        cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(5)))
    '                        cmd.Parameters.Add(New MySqlParameter("@shift", csv(6)))
    '                        cmd.Parameters.Add(New MySqlParameter("@Partno", csv(7)))
    '                        cmd.Parameters.Add(New MySqlParameter("@total_cycle", csv(8)))
    '                        cmd.Parameters.Add(New MySqlParameter("@good_cycle", csv(9)))
    '                        cmd.Parameters.Add(New MySqlParameter("@short_cycle", csv(10)))
    '                        cmd.Parameters.Add(New MySqlParameter("@long_cycle", csv(11)))
    '                        cmd.Parameters.Add(New MySqlParameter("@avg_good_cycle_time", csv(12)))
    '                        cmd.Parameters.Add(New MySqlParameter("@max_cycle_time", csv(13)))
    '                        cmd.Parameters.Add(New MySqlParameter("@min_cycle_time", csv(14)))


    '                        cmd.ExecuteNonQuery() 'async doesnt wait for finish


    '                    Catch ex As Exception
    '                        LogServerError("Error parsing partnumber csv file:" + ex.Message)
    '                    End Try
    '                End While

    '            End Using

    '        Catch ex As Exception
    '            LogServerError("Error uploading partnumber csv file:" + ex.Message)
    '        End Try

    '        beginTransac(False)
    '        Thread.CurrentThread.CurrentCulture = originalCulture

    '    Catch ex As Exception
    '        LogServerError("unable to complete PARTNUMBER Table creation:" + ex.Message)
    '    End Try
    'End Sub

    'Private Sub FirstReportedpartUpdate_MySQL()
    '    Try
    '        Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
    '        Dim partcsvfile As String = ""
    '        For Each File In files
    '            If System.IO.Path.GetFileName(File).StartsWith("_REPORTEDPARTS_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
    '                partcsvfile = File
    '            End If
    '        Next


    '        Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
    '        ' Change culture to en-US.

    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


    '        'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
    '        Dim startline As Long = 0

    '        Try
    '            'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
    '            Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

    '            For Each match In File.ReadLines(partcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
    '                'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
    '                startline = match.lineNumber - 1
    '                Exit For
    '            Next
    '        Catch ex As Exception
    '            LogServerError("Unable to find start line:" + ex.Message)
    '        End Try


    '        beginTransac(True)

    '        Dim cmd As New MySqlCommand

    '        Try
    '            'MACHINE NAME, Date, TIME, TOTAL PARTS, BAD PARTS, HEAD / PALLET, IDEAL CYCLETIME
    '            'OKUMA, 09 / 24 / 2016, 09: 59:29,3,8,0,1
    '            Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_reportedparts (`machine` varchar(255) NOT NULL,  `trx_time` datetime NOT NULL, `total_parts` int NOT NULL, `bad_parts` int NOT NULL," +
    '                " `HEADPALLET` int NOT NULL, `ideal_cycle_time` int NOT NULL);"

    '            cmd = New MySqlCommand(cmdstr, sqlInserConn)

    '            'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

    '            cmd.ExecuteNonQuery() 'async doesnt wait for finish

    '            Dim machlist As New List(Of String)

    '            'open the file "data.csv" which is a CSV file with headers
    '            Using csv As New CsvReader(New StreamReader(partcsvfile), True, ","c)

    '                Dim fieldCount As Integer = csv.FieldCount
    '                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
    '                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

    '                Dim headers As String() = csv.GetFieldHeaders()

    '                csv.MoveTo(startline)

    '                ''Dim cmd As New MySqlCommand
    '                Dim tempdate As New DateTime()
    '                Dim temptime As New DateTime()
    '                ''Dim status As String

    '                While (csv.ReadNextRecord())

    '                    Try
    '                        Dim renamedmachine As String = RenameMachine(csv(0))

    '                        If Not (machlist.Contains(csv(0))) Then
    '                            machlist.Add(csv(0))

    '                            cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqlInserConn)
    '                            cmd.ExecuteNonQuery()
    '                        End If

    '                        'Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_reportedparts (`machine` varchar(255) NOT NULL,  `trx_time` datetime NOT NULL, `total_parts` int NOT NULL, `bad_parts` int NOT NULL," +
    '                        '" `HEADPALLET` int NOT NULL, `ideal_cycle_time` int NOT NULL);"

    '                        cmd = New MySqlCommand("Replace into CSI_Database.tbl_reportedparts" +
    '                                         " (machine, trx_time, total_parts, bad_parts, HEADPALLET, ideal_cycle_time)" +
    '                                         " VALUES (@machine,@trx_time,@total_parts,@bad_parts,@HEADPALLET,@ideal_cycle_time);", sqlInserConn)

    '                        tempdate = csv(1)
    '                        temptime = csv(2)
    '                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

    '                        cmd.Parameters.Add(New MySqlParameter("@machine", csv(0)))
    '                        cmd.Parameters.Add(New MySqlParameter("@trx_time", temptime))
    '                        cmd.Parameters.Add(New MySqlParameter("@total_parts", csv(3)))
    '                        cmd.Parameters.Add(New MySqlParameter("@bad_parts", csv(4)))
    '                        cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(5)))
    '                        cmd.Parameters.Add(New MySqlParameter("@ideal_cycle_time", csv(6)))

    '                        cmd.ExecuteNonQuery() 'async doesnt wait for finish


    '                    Catch ex As Exception
    '                        LogServerError("Error parsing partnumber csv file:" + ex.Message)
    '                    End Try
    '                End While

    '            End Using

    '        Catch ex As Exception
    '            LogServerError("Error uploading partnumber csv file:" + ex.Message)
    '        End Try

    '        beginTransac(False)
    '        Thread.CurrentThread.CurrentCulture = originalCulture

    '    Catch ex As Exception
    '        LogServerError("unable to complete PARTNUMBER Table creation:" + ex.Message)
    '    End Try
    'End Sub

    'Private Sub FirstMachineUpdate_MySQL()
    '    Try
    '        Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
    '        Dim machcsvfile As String = ""
    '        'Dim MostRecentCSV As Integer = 0
    '        For Each File In files
    '            If System.IO.Path.GetFileName(File).StartsWith("_MACHINE_" + DateTime.Now.Year.ToString()) And
    '                System.IO.Path.GetExtension(File) = ".CSV" Then
    '                If System.IO.Path.GetFileName(File).Count = 17 Then
    '                    machcsvfile = File
    '                End If
    '            End If
    '        Next


    '        'Dim dt As DataTable = New DataTable

    '        Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
    '        ' Change culture to en-US.

    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


    '        'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
    '        Dim startline As Long = 0

    '        Try
    '            'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
    '            Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

    '            For Each match In File.ReadLines(machcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
    '                'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
    '                startline = match.lineNumber - 1
    '                Exit For
    '            Next
    '        Catch ex As Exception
    '            LogServerError("Unable to find start line:" + ex.Message)
    '        End Try


    '        beginTransac(True)

    '        Dim machlist As New List(Of String)

    '        'open the file "data.csv" which is a CSV file with headers
    '        Using csv As New CsvReader(New StreamReader(machcsvfile), True, ","c)

    '            Dim fieldCount As Integer = csv.FieldCount
    '            csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
    '            csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

    '            Dim headers As String() = csv.GetFieldHeaders()

    '            csv.MoveTo(startline)

    '            Dim cmd As New MySqlCommand
    '            Dim tempdate As New DateTime()
    '            Dim temptime As New DateTime()
    '            Dim status As String

    '            While (csv.ReadNextRecord())

    '                Try
    '                    'For Each row As DataRow In result
    '                    '    resultTable.ImportRow(row)
    '                    'Next
    '                    Dim renamedmachine As String = RenameMachine(csv(0))

    '                    If Not (machlist.Contains(csv(0))) Then
    '                        machlist.Add(csv(0))

    '                        'Dim sqlConn As MySqlConnection = New MySqlConnection(MySqlConnectionString)
    '                        'sqlConn.Open()
    '                        cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqlInserConn)
    '                        cmd.ExecuteNonQuery()

    '                        cmd = New MySqlCommand("CREATE TABLE if not exists CSI_Database.tbl_" & renamedmachine & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, UNIQUE KEY (time_,status))", sqlInserConn)
    '                        cmd.ExecuteNonQuery()

    '                        'sqlConn.Close()
    '                    End If

    '                    'csv line example
    '                    'GANESH,07/23/2015,2,23:28:03,01:30:00,7317,CYCLE ON,0,
    '                    cmd = New MySqlCommand("Replace into CSI_Database.tbl_" & renamedmachine +
    '                                     "(month_, day_, year_, time_, Date_, status, shift, cycletime)" +
    '                                     "VALUES (@month,@day,@year,@time,@date,@status,@shift,@cycletime);", sqlInserConn)
    '                    tempdate = csv(1) 'row("DATE")


    '                    cmd.Parameters.Add(New MySqlParameter("@month", tempdate.Month))
    '                    cmd.Parameters.Add(New MySqlParameter("@day", tempdate.Day))
    '                    cmd.Parameters.Add(New MySqlParameter("@year", tempdate.Year))
    '                    temptime = csv(3) 'row("START TIME")
    '                    temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
    '                    cmd.Parameters.Add(New MySqlParameter("@time", temptime))
    '                    cmd.Parameters.Add(New MySqlParameter("@date", temptime))

    '                    'If row("MACHINE STATUS") = "_PARTNUMBER" Then
    '                    If csv(6) = "_PARTNUMBER" Then
    '                        status = "_PARTNO:" & csv(8).Split(";")(0)
    '                    Else
    '                        status = csv(6) 'row("MACHINE STATUS")
    '                    End If

    '                    cmd.Parameters.Add(New MySqlParameter("@status", status))
    '                    cmd.Parameters.Add(New MySqlParameter("@shift", csv(2))) 'row("SHIFT")))
    '                    cmd.Parameters.Add(New MySqlParameter("@cycletime", csv(5))) 'row("ELAPSED TIME")))
    '                    cmd.ExecuteNonQuery() 'async doesnt wait for finish


    '                Catch ex As Exception
    '                    LogServerError("Error parsing csv file:" + ex.Message)
    '                End Try
    '            End While


    '        End Using

    '        beginTransac(False)
    '        Thread.CurrentThread.CurrentCulture = originalCulture

    '        '''''DONE
    '        setFirstUpdateOrNot(False)
    '    Catch ex As Exception
    '        LogServerError("unable to complete database creation:" + ex.Message)
    '    End Try

    'End Sub
#End Region ' not used, keeped as backup

#Region "MySQL First update with previous years"

    Private Sub FirstOEEUpdate_MySQL(years_ As String)

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
            Dim oeecsvfile As String = ""
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_OEE_" + years_) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    oeecsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                For Each match In File.ReadLines(oeecsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServerError("Unable to find start line:" + ex.Message, 1)
            End Try

            startline = -1

            beginTransac(True)

            Dim cmd As New MySqlCommand

            Try
                Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_oee (`MACHINE` varchar(75) NOT NULL,  `trx_time` datetime NOT NULL, `Avail` float NOT NULL, `Performance` float NOT NULL, `Quality` float NOT NULL, `OEE` float NOT NULL,`HEADPALLET` int, index (trx_time));"

                cmd = New MySqlCommand(cmdstr, sqlInserConn)

                'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

                cmd.ExecuteNonQuery() 'async doesnt wait for finish

                Dim machlist As New List(Of String)

                'open the file "data.csv" which is a CSV file with headers
                Using csv As New CsvReader(New StreamReader(oeecsvfile), True, ","c)

                    Dim fieldCount As Integer = csv.FieldCount
                    csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                    csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                    Dim headers As String() = csv.GetFieldHeaders()

                    csv.MoveTo(startline)

                    'Dim cmd As New MySqlCommand
                    Dim tempdate As New DateTime()
                    Dim temptime As New DateTime()
                    'Dim status As String

                    While (csv.ReadNextRecord())

                        Try
                            Dim renamedmachine As String = RenameMachine(csv(0))

                            If Not (machlist.Contains(csv(0))) Then
                                machlist.Add(csv(0))

                                cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqlInserConn)
                                cmd.ExecuteNonQuery()
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
                            LogServerError("Error parsing oee csv file:" + ex.Message, 1)
                        End Try
                    End While

                End Using

            Catch ex As Exception
                LogServerError("Error uploading oee csv file:" + ex.Message, 1)
            End Try

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

        Catch ex As Exception
            LogServerError("unable to complete OEE Table creation:" + ex.Message, 1)
        End Try
    End Sub
    Private Sub FirstOperatorUpdate_MySQL(years_ As String)

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
            Dim opercsvfile As String = ""
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_OPERATOR_" + years_) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    opercsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                For Each match In File.ReadLines(opercsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServerError("Unable to find start line:" + ex.Message, 1)
            End Try

            startline = -1
            beginTransac(True)

            Dim cmd As New MySqlCommand

            Try
                Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_operator (`OPERATOR` varchar(150) NOT NULL,  `PARTNO` varchar(255) NOT NULL, `QUANTITY` int NOT NULL, `AVGCYCLETIME` time NOT NULL, `GOODCYCLE` int NOT NULL, `AVGGOODCYCLETIME` time NOT NULL, `trx_date` date not null, `shift` int NOT NULL, `HEADPALLET` int, index (trx_date));"

                cmd = New MySqlCommand(cmdstr, sqlInserConn)

                'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

                cmd.ExecuteNonQuery() 'async doesnt wait for finish

                Dim machlist As New List(Of String)

                'open the file "data.csv" which is a CSV file with headers
                Using csv As New CsvReader(New StreamReader(opercsvfile), True, ","c)

                    Dim fieldCount As Integer = csv.FieldCount
                    csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                    csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                    Dim headers As String() = csv.GetFieldHeaders()

                    csv.MoveTo(startline)

                    ''Dim cmd As New MySqlCommand
                    Dim tempdate As New DateTime()
                    'Dim temptime As New DateTime()
                    ''Dim status As String

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
                            LogServerError("Error parsing operator csv file:" + ex.Message, 1)
                        End Try
                    End While

                End Using

            Catch ex As Exception
                LogServerError("Error uploading operator csv file:" + ex.Message, 1)
            End Try

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

        Catch ex As Exception
            LogServerError("unable to complete OPERATOR Table creation:" + ex.Message, 1)
        End Try
    End Sub
    Private Sub FirstPartnumberUpdate_MySQL(years_ As String)

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
            Dim partcsvfile As String = ""
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_PARTNUMBER_" + years_) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    partcsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                For Each match In File.ReadLines(partcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServerError("Unable to find start line:" + ex.Message, 1)
            End Try

            startline = -1
            beginTransac(True)

            Dim cmd As New MySqlCommand

            Try
                'DATE    ,START TIME,END TIME,ELAPSED TIME,MACHINE NAME,HEAD/PALLET,SHIFT,PART NUMBER,TOTAL CYC,GOOD CYC,SHORT CYC,LONG CYC,AVG GOOD CYC TIME,MAX CYC TIME,MIN CYC TIME
                '01/05/2016, 05:00:02, 12:10:07, 25805, 420, 0, 1, 212-100-10, 50, 0, 50, 0, 00:00:00, 00:08:11, 00:00:02
                Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_partnumber (`start_time` datetime NOT NULL,  `end_time` datetime NOT NULL, `elapsed_time` int NOT NULL, `machine` varchar(255) NOT NULL," +
                    " `HEADPALLET` int NOT NULL, `shift` int NOT NULL, `Partno` varchar(255) not null, `total_cycle` int NOT NULL, `good_cycle` int not null, `short_cycle` int not null," +
                    " `long_cycle` int not null, `avg_good_cycle_time` time not null, `max_cycle_time` time not null, `min_cycle_time` time not null, index(start_time));"

                cmd = New MySqlCommand(cmdstr, sqlInserConn)

                'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

                cmd.ExecuteNonQuery() 'async doesnt wait for finish



                Dim machlist As New List(Of String)

                'open the file "data.csv" which is a CSV file with headers
                Using csv As New CsvReader(New StreamReader(partcsvfile), True, ","c)

                    Dim fieldCount As Integer = csv.FieldCount
                    csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                    csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                    Dim headers As String() = csv.GetFieldHeaders()

                    csv.MoveTo(startline)

                    ''Dim cmd As New MySqlCommand
                    Dim tempdate As New DateTime()
                    Dim temptime As New DateTime()
                    ''Dim status As String

                    While (csv.ReadNextRecord())

                        Try
                            Dim renamedmachine As String = RenameMachine(csv(4))

                            If Not (machlist.Contains(csv(4))) Then
                                machlist.Add(csv(4))

                                cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(4) & "')", sqlInserConn)
                                cmd.ExecuteNonQuery()
                            End If

                            'DATE    ,START TIME,END TIME,ELAPSED TIME,MACHINE NAME,HEAD/PALLET,SHIFT,PART NUMBER,TOTAL CYC,GOOD CYC,SHORT CYC,LONG CYC,AVG GOOD CYC TIME,MAX CYC TIME,MIN CYC TIME
                            '01/05/2016, 05:00:02, 12:10:07, 25805, 420, 0, 1, 212-100-10, 50, 0, 50, 0, 00:00:00, 00:08:11, 00:00:02
                            '        Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_partnumber (`start_time` datetime NOT NULL,  `end_time` datetime NOT NULL, `elapsed_time` int NOT NULL, `machine` varchar(255) NOT NULL," +
                            '" `HEADPALLET` int NOT NULL, `shift` int NOT NULL, `Partno` varchar(255) not null, `total_cycle` int NOT NULL, `good_cycle` int not null, `short_cycle` int not null," +
                            '" `long_cycle` int not null, `avg_good_cycle_time` time not null, `max_cycle_time` time not null, `min_cycle_time` time not null);"

                            cmd = New MySqlCommand("Replace into CSI_Database.tbl_partnumber" +
                                             " (start_time, end_time, elapsed_time, machine, HEADPALLET, shift, Partno, total_cycle, good_cycle, short_cycle, long_cycle, avg_good_cycle_time, max_cycle_time, min_cycle_time)" +
                                             " VALUES (@start_time,@end_time,@elapsed_time,@machine,@HEADPALLET,@shift,@Partno,@total_cycle,@good_cycle,@short_cycle,@long_cycle,@avg_good_cycle_time,@max_cycle_time,@min_cycle_time);", sqlInserConn)

                            tempdate = csv(0)
                            temptime = csv(1)
                            temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                            cmd.Parameters.Add(New MySqlParameter("@start_time", temptime))
                            temptime = csv(2)
                            temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                            cmd.Parameters.Add(New MySqlParameter("@end_time", temptime))
                            cmd.Parameters.Add(New MySqlParameter("@elapsed_time", csv(3)))
                            cmd.Parameters.Add(New MySqlParameter("@machine", csv(4)))
                            cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(5)))
                            cmd.Parameters.Add(New MySqlParameter("@shift", csv(6)))
                            cmd.Parameters.Add(New MySqlParameter("@Partno", csv(7)))
                            cmd.Parameters.Add(New MySqlParameter("@total_cycle", csv(8)))
                            cmd.Parameters.Add(New MySqlParameter("@good_cycle", csv(9)))
                            cmd.Parameters.Add(New MySqlParameter("@short_cycle", csv(10)))
                            cmd.Parameters.Add(New MySqlParameter("@long_cycle", csv(11)))
                            cmd.Parameters.Add(New MySqlParameter("@avg_good_cycle_time", csv(12)))
                            cmd.Parameters.Add(New MySqlParameter("@max_cycle_time", csv(13)))
                            cmd.Parameters.Add(New MySqlParameter("@min_cycle_time", csv(14)))


                            cmd.ExecuteNonQuery() 'async doesnt wait for finish


                        Catch ex As Exception
                            LogServerError("Error parsing partnumber csv file:" + ex.Message, 1)
                        End Try
                    End While

                End Using

            Catch ex As Exception
                LogServerError("Error uploading partnumber csv file:" + ex.Message, 1)
            End Try

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

        Catch ex As Exception
            LogServerError("unable to complete PARTNUMBER Table creation:" + ex.Message, 1)
        End Try
    End Sub
    Private Sub FirstReportedpartUpdate_MySQL(years_ As String)
        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
            Dim partcsvfile As String = ""
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_REPORTEDPARTS_" + years_) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    partcsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                For Each match In File.ReadLines(partcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServerError("Unable to find start line:" + ex.Message, 1)
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

                'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

                cmd.ExecuteNonQuery() 'async doesnt wait for finish

                Dim machlist As New List(Of String)

                'open the file "data.csv" which is a CSV file with headers
                Using csv As New CsvReader(New StreamReader(partcsvfile), True, ","c)

                    Dim fieldCount As Integer = csv.FieldCount
                    csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                    csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                    Dim headers As String() = csv.GetFieldHeaders()

                    csv.MoveTo(startline)

                    ''Dim cmd As New MySqlCommand
                    Dim tempdate As New DateTime()
                    Dim temptime As New DateTime()
                    ''Dim status As String

                    While (csv.ReadNextRecord())

                        Try
                            Dim renamedmachine As String = RenameMachine(csv(0))

                            If Not (machlist.Contains(csv(0))) Then
                                machlist.Add(csv(0))

                                cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqlInserConn)
                                cmd.ExecuteNonQuery()
                            End If

                            'Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_reportedparts (`machine` varchar(255) NOT NULL,  `trx_time` datetime NOT NULL, `total_parts` int NOT NULL, `bad_parts` int NOT NULL," +
                            '" `HEADPALLET` int NOT NULL, `ideal_cycle_time` int NOT NULL);"

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
                            LogServerError("Error parsing partnumber csv file:" + ex.Message, 1)
                        End Try
                    End While

                End Using

            Catch ex As Exception
                LogServerError("Error uploading partnumber csv file:" + ex.Message, 1)
            End Try

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

        Catch ex As Exception
            LogServerError("unable to complete PARTNUMBER Table creation:" + ex.Message, 1)
        End Try
    End Sub
    Private Sub FirstMachineUpdate_MySQL(years_ As String)
        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim machcsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_MACHINE_" + years_) And
                    System.IO.Path.GetExtension(File) = ".CSV" Then
                    If System.IO.Path.GetFileName(File).Count = 17 Then
                        machcsvfile = File
                    End If
                End If
            Next
            If machcsvfile = "" Then GoTo end_ : 

            'Dim dt As DataTable = New DataTable

            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                For Each match In File.ReadLines(machcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServerError("Unable to find start line:" + ex.Message, 1)
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
                        'For Each row As DataRow In result
                        '    resultTable.ImportRow(row)
                        'Next
                        Dim renamedmachine As String = RenameMachine(csv(0))

                        If Not (machlist.Contains(csv(0))) Then
                            machlist.Add(csv(0))

                            'Dim sqlConn As MySqlConnection = New MySqlConnection(MySqlConnectionString)
                            'sqlConn.Open()
                            cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqlInserConn)
                            cmd.ExecuteNonQuery()

                            cmd = New MySqlCommand("CREATE TABLE if not exists CSI_Database.tbl_" & renamedmachine & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double,Partnumber varchar(255), UNIQUE KEY (time_,status),index (time_))", sqlInserConn)
                            cmd.ExecuteNonQuery()

                            'sqlConn.Close()
                        End If

                        'csv line example
                        'GANESH,07/23/2015,2,23:28:03,01:30:00,7317,CYCLE ON,0,
                        cmd = New MySqlCommand("Replace into CSI_Database.tbl_" & renamedmachine +
                                         "(month_, day_, year_, time_, Date_, status, shift, cycletime, partnumber)" +
                                         "VALUES (@month,@day,@year,@time,@date,@status,@shift,@cycletime,@partnumber);", sqlInserConn)
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
                        LogServerError("Error parsing csv file:" + ex.Message, 1)
                    End Try
                End While
                Log_server_event("first update of DB finished:")

            End Using

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

end_:
            '''''DONE
            setFirstUpdateOrNot(False)
        Catch ex As Exception
            LogServerError("unable to complete database creation:" + ex.Message, 1)
        End Try

    End Sub

#End Region


#Region "mySQL Update"

    Private Function check_if_MCH_db_rebuild_is_needed_mysql() As Boolean
        Dim returnvalue As Boolean = False

        Dim sqlcon As New MySqlConnection(MySqlConnectionString)
        Try
            sqlcon.Open()
            Dim sqlcmd As New MySqlCommand("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE `TABLE_NAME` LIKE 'tbl_CSIFLEX_VERSION'", sqlcon)
            Dim reader As MySqlDataReader = sqlcmd.ExecuteReader


            Dim table_tablenames As New DataTable
            table_tablenames.Load(reader)
            reader.Close()
            Dim tableExists As Boolean = False
            If table_tablenames.Rows.Count <> 0 Then tableExists = True



            If tableExists = True Then
                sqlcmd = New MySqlCommand("Select version FROM csi_database.tbl_CSIFLEX_VERSION", sqlcon)
                table_tablenames = New DataTable
                reader = sqlcmd.ExecuteReader
                table_tablenames.Load(reader)

                'because a stupi error ...
                If table_tablenames.Rows(0).Item(0) = 1894 Then
                    sqlcmd = New MySqlCommand(" update  csi_database.tbl_CSIFLEX_VERSION SET version =1865 where version =1894", sqlcon)
                    sqlcmd.ExecuteNonQuery()
                    returnvalue = False
                End If


                If table_tablenames.Rows(0).Item(0) < 1864 Then

                    returnvalue = True
                Else
                    returnvalue = False
                End If
            Else
                Dim drop_list As String = ""



                sqlcmd = New MySqlCommand("SELECT table_name from csi_database.tbl_renamemachines", sqlcon)
                reader = sqlcmd.ExecuteReader

                table_tablenames = New DataTable
                table_tablenames.Load(reader)
                reader.Close()
                Dim machinename As String = ""
                Dim cmd As MySqlCommand
                Dim second_table_mch_name As New DataTable
                For Each row As DataRow In table_tablenames.Rows
                    machinename = row.Item(0)
                    sqlcmd = New MySqlCommand("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE `TABLE_NAME` LIKE 'tbl_" & row.Item(0) & "'", sqlcon)
                    reader = sqlcmd.ExecuteReader

                    second_table_mch_name.Clear()
                    second_table_mch_name.Load(reader)
                    reader.Close()

                    If second_table_mch_name.Rows.Count <> 0 Then
                        drop_list = "drop table csi_database.tbl_" & machinename
                        cmd = New MySqlCommand(drop_list, sqlcon)
                        cmd.ExecuteNonQuery()
                    End If
                Next



                returnvalue = True
            End If

            sqlcon.Close()
            Return returnvalue
        Catch ex As Exception
            LogServerError(ex.Message, 1)
            If sqlcon.State = ConnectionState.Open Then sqlcon.Close()
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
                LogServerError(ex.Message, 1)
                If sqlConn.State = ConnectionState.Open Then sqlConn.Close()
            End Try

            GoTo end_
        End If
        Perf_Computation_needed = True
        Log_server_event("Updating DB at : " + Now)
        OEEUpdate_MySQL()
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


            'Dim machlastupdate As New Dictionary(Of String, DateTime)
            'Dim machlistdt As New DataTable
            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)


            mysqlcon.Open()

            'For Each row In machlistdt.Rows

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
            Using mysqlcmd As New MySqlCommand("Select max(trx_time) as maxdate from CSI_Database.tbl_oee", mysqlcon)
                Dim reader As MySqlDataReader = mysqlcmd.ExecuteReader()
                reader.Read()
                If Not reader("maxdate").Equals(DBNull.Value) Then
                    lastdate = reader.GetDateTime("maxdate")
                End If
                reader.Close()
            End Using

            ' machlastupdate.Add(row("original_name"), lastdate)

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            'Next

            'End Using
            mysqlcon.Close()




            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                ' Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                    firstdate = CreateDateStr(maxlastdate)
                End If


                For Each match In File.ReadLines(oeecsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServiceError("Unable to find start line:" + ex.Message, 1)
                If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
            End Try


            beginTransac(True)

            'Dim machlist As New List(Of String)

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
                Dim status As String

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
                        LogServiceError("Error parsing oee csv file:" + ex.Message, 1)
                    End Try
                End While
            End Using

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

            'isloading = False
exit_func:
        Catch ex As Exception
            If sqlInserConn.State = ConnectionState.Open Then beginTransac(False)
            LogServiceError("unable to complete oee table update:" + ex.Message, 1)
            If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
        End Try

    End Sub

    Public Sub OperatorUpdate_MySQL()
        Dim mysqlcon As New MySqlConnection(MySqlConnectionString)
        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim operatorcsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_OPERATOR_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    operatorcsvfile = File
                End If
            Next

            If operatorcsvfile = "" Then GoTo exit_func
            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim machlastupdate As New Dictionary(Of String, DateTime)
            'Dim machlistdt As New DataTable
            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)


            mysqlcon.Open()

            'For Each row In machlistdt.Rows

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
            Using mysqlcmd As New MySqlCommand("Select max(trx_date) as maxdate from CSI_Database.tbl_operator", mysqlcon)
                Dim reader As MySqlDataReader = mysqlcmd.ExecuteReader()
                reader.Read()
                If Not reader("maxdate").Equals(DBNull.Value) Then
                    lastdate = reader.GetDateTime("maxdate")
                End If
                reader.Close()
            End Using

            ' machlastupdate.Add(row("original_name"), lastdate)

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            'Next

            'End Using
            mysqlcon.Close()




            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                ' Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                    firstdate = CreateDateStr(maxlastdate)
                End If


                For Each match In File.ReadLines(operatorcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServiceError("Unable to find start line:" + ex.Message, 1)
                If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
            End Try


            beginTransac(True)

            'Dim machlist As New List(Of String)

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
                        LogServiceError("Error parsing operator csv file:" + ex.Message, 1)
                    End Try
                End While
            End Using

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

            'isloading = False
exit_func:
        Catch ex As Exception
            If sqlInserConn.State = ConnectionState.Open Then beginTransac(False)
            LogServiceError("unable to complete operator table update:" + ex.Message, 1)
            If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
        End Try

    End Sub

    Public Sub PartnumberUpdate_MySQL()
        Dim mysqlcon As New MySqlConnection(MySqlConnectionString)
        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim partnocsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_PARTNUMBER_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    partnocsvfile = File
                End If
            Next

            If partnocsvfile = "" Then GoTo exit_func

            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim machlastupdate As New Dictionary(Of String, DateTime)
            'Dim machlistdt As New DataTable
            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)



            mysqlcon.Open()

            'For Each row In machlistdt.Rows

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
            Using mysqlcmd As New MySqlCommand("Select max(end_time) as maxdate from CSI_Database.tbl_partnumber", mysqlcon)
                Dim reader As MySqlDataReader = mysqlcmd.ExecuteReader()
                reader.Read()
                If Not reader("maxdate").Equals(DBNull.Value) Then
                    lastdate = reader.GetDateTime("maxdate")
                End If
                reader.Close()
            End Using

            ' machlastupdate.Add(row("original_name"), lastdate)

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            'Next

            'End Using
            mysqlcon.Close()




            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                ' Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                    firstdate = CreateDateStr(maxlastdate)
                End If


                For Each match In File.ReadLines(partnocsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServiceError("Unable to find start line:" + ex.Message, 1)
                If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
            End Try


            beginTransac(True)

            'Dim machlist As New List(Of String)

            'open the file "data.csv" which is a CSV file with headers
            Using csv As New CsvReader(New StreamReader(partnocsvfile), True, ","c)

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

                        cmd = New MySqlCommand("Replace into CSI_Database.tbl_partnumber" +
                                         " (start_time, end_time, elapsed_time, machine, HEADPALLET, shift, Partno, total_cycle, good_cycle, short_cycle, long_cycle, avg_good_cycle_time, max_cycle_time, min_cycle_time)" +
                                         " VALUES (@start_time,@end_time,@elapsed_time,@machine,@HEADPALLET,@shift,@Partno,@total_cycle,@good_cycle,@short_cycle,@long_cycle,@avg_good_cycle_time,@max_cycle_time,@min_cycle_time);", sqlInserConn)

                        tempdate = csv(0)
                        temptime = csv(1)
                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                        cmd.Parameters.Add(New MySqlParameter("@start_time", temptime))
                        temptime = csv(2)
                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                        cmd.Parameters.Add(New MySqlParameter("@end_time", temptime))
                        cmd.Parameters.Add(New MySqlParameter("@elapsed_time", csv(3)))
                        cmd.Parameters.Add(New MySqlParameter("@machine", csv(4)))
                        cmd.Parameters.Add(New MySqlParameter("@HEADPALLET", csv(5)))
                        cmd.Parameters.Add(New MySqlParameter("@shift", csv(6)))
                        cmd.Parameters.Add(New MySqlParameter("@Partno", csv(7)))
                        cmd.Parameters.Add(New MySqlParameter("@total_cycle", csv(8)))
                        cmd.Parameters.Add(New MySqlParameter("@good_cycle", csv(9)))
                        cmd.Parameters.Add(New MySqlParameter("@short_cycle", csv(10)))
                        cmd.Parameters.Add(New MySqlParameter("@long_cycle", csv(11)))
                        cmd.Parameters.Add(New MySqlParameter("@avg_good_cycle_time", csv(12)))
                        cmd.Parameters.Add(New MySqlParameter("@max_cycle_time", csv(13)))
                        cmd.Parameters.Add(New MySqlParameter("@min_cycle_time", csv(14)))


                        cmd.ExecuteNonQuery() 'async doesnt wait for finish

                    Catch ex As Exception
                        LogServiceError("Error parsing partnumber csv file:" + ex.Message, 1)
                    End Try
                End While
            End Using

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

            'isloading = False
exit_func:
        Catch ex As Exception
            If sqlInserConn.State = ConnectionState.Open Then beginTransac(False)
            LogServiceError("unable to complete partnumber table update:" + ex.Message, 1)
            If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
        End Try

    End Sub

    Public Sub ReportedpartsUpdate_MySQL()
        Dim mysqlcon As New MySqlConnection(MySqlConnectionString)
        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim reportedpartscsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_REPORTEDPARTS_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    reportedpartscsvfile = File
                End If
            Next

            If reportedpartscsvfile = "" Then GoTo exit_func

            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim machlastupdate As New Dictionary(Of String, DateTime)
            'Dim machlistdt As New DataTable
            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)


            mysqlcon.Open()

            'For Each row In machlistdt.Rows

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
            Using mysqlcmd As New MySqlCommand("Select max(trx_time) as maxdate from CSI_Database.tbl_reportedparts", mysqlcon)
                Dim reader As MySqlDataReader = mysqlcmd.ExecuteReader()
                reader.Read()
                If Not reader("maxdate").Equals(DBNull.Value) Then
                    lastdate = reader.GetDateTime("maxdate")
                End If
                reader.Close()
            End Using

            ' machlastupdate.Add(row("original_name"), lastdate)

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            'Next

            'End Using
            mysqlcon.Close()




            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                ' Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                    firstdate = CreateDateStr(maxlastdate)
                End If


                For Each match In File.ReadLines(reportedpartscsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServiceError("Unable to find start line:" + ex.Message, 1)
                If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
            End Try


            beginTransac(True)

            'Dim machlist As New List(Of String)

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
                'Dim status As String

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
                        LogServiceError("Error parsing reportedparts csv file:" + ex.Message, 1)
                    End Try
                End While
            End Using

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

            'isloading = False
exit_func:

        Catch ex As Exception
            If sqlInserConn.State = ConnectionState.Open Then beginTransac(False)
            LogServiceError("unable to complete reportedparts table update:" + ex.Message, 1)
            If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
        End Try

    End Sub

    Public Sub MachineUpdate_MySQL()
        Dim mysqlcon As New MySqlConnection(MySqlConnectionString)
        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim machcsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_MACHINE_" + DateTime.Now.Year.ToString()) And
                    System.IO.Path.GetExtension(File) = ".CSV" Then
                    If System.IO.Path.GetFileName(File).Count = 17 Then
                        machcsvfile = File
                    End If
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

            Dim machlastupdate As New Dictionary(Of String, DateTime)
            Dim machlistdt As New DataTable
            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)


            mysqlcon.Open()
            'Using mysqlcmd As New MySqlCommand("SELECT *", mysqlcon)

            'mysqlcmd.ExecuteReader()
            Dim mysqlda As New MySqlDataAdapter("Select table_name,original_name from csi_database.tbl_renamemachines ", mysqlcon)

            mysqlda.Fill(machlistdt)


            For Each row In machlistdt.Rows

                Dim lastdate As DateTime = New DateTime(1900, 1, 1)
                Using mysqlcmd As New MySqlCommand("Select max(Date_) as maxdate from CSI_Database.tbl_" & row("table_name"), mysqlcon)
                    Dim reader As MySqlDataReader = mysqlcmd.ExecuteReader()
                    reader.Read()
                    If Not reader("maxdate").Equals(DBNull.Value) Then
                        lastdate = reader.GetDateTime("maxdate")
                    End If
                    reader.Close()
                End Using

                machlastupdate.Add(row("original_name"), lastdate)

                If (lastdate > maxlastdate) Then
                    maxlastdate = lastdate
                End If

            Next

            'End Using
            mysqlcon.Close()





            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                ' Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                    firstdate = CreateDateStr(maxlastdate)
                End If


                For Each match In File.ReadLines(machcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServiceError("Unable to find start line:" + ex.Message, 1)
            End Try


            beginTransac(True)

            'Dim machlist As New List(Of String)

            'open the file "data.csv" which is a CSV file with headers
            Using csv As New CsvReader(New StreamReader(machcsvfile), True, ","c)

                Dim fieldCount As Integer = csv.FieldCount
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                Dim headers As String() = csv.GetFieldHeaders()

                csv.MoveTo(startline - 1)

                Dim cmd As New MySqlCommand
                Dim tempdate As New DateTime()
                Dim temptime As New DateTime()
                Dim status As String




                While (csv.ReadNextRecord())

                    Try
                        'For Each row As DataRow In result
                        '    resultTable.ImportRow(row)
                        'Next
                        Dim renamedmachine As String = RenameMachine(csv(0))

                        If Not (machlastupdate.ContainsKey(csv(0))) Then
                            machlastupdate.Add(csv(0), maxlastdate)

                            'Dim sqlConn As MySqlConnection = New MySqlConnection(MySqlConnectionString)
                            'sqlConn.Open()
                            cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqlInserConn)
                            cmd.ExecuteNonQuery()

                            cmd = New MySqlCommand("CREATE TABLE if not exists CSI_Database.tbl_" & renamedmachine & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, UNIQUE KEY (time_,status))", sqlInserConn)
                            cmd.ExecuteNonQuery()

                            'sqlConn.Close()
                        End If

                        'csv line example
                        'GANESH,07/23/2015,2,23:28:03,01:30:00,7317,CYCLE ON,0,
                        cmd = New MySqlCommand("Replace into CSI_Database.tbl_" & renamedmachine +
                                         "(month_, day_, year_, time_, Date_, status, shift, cycletime)" +
                                         "VALUES (@month,@day,@year,@time,@date,@status,@shift,@cycletime);", sqlInserConn)
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
                        Else
                            status = csv(6) 'row("MACHINE STATUS")
                        End If

                        cmd.Parameters.Add(New MySqlParameter("@status", status))
                        cmd.Parameters.Add(New MySqlParameter("@shift", csv(2))) 'row("SHIFT")))
                        cmd.Parameters.Add(New MySqlParameter("@cycletime", csv(5))) 'row("ELAPSED TIME")))
                        cmd.ExecuteNonQuery() 'async doesnt wait for finish

                    Catch ex As Exception
                        LogServiceError("Error parsing csv file:" + ex.Message, 1)
                    End Try
                End While
            End Using

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

            'isloading = False

        Catch ex As Exception
            If sqlInserConn.State = ConnectionState.Open Then beginTransac(False)
            If mysqlcon.State = ConnectionState.Open Then mysqlcon.Close()
            LogServiceError("unable to complete database update:" + ex.Message, 1)
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



#Region "update SQLite"

    Private Function check_if_MCH_db_rebuild_is_needed() As Boolean

        Dim sqlitecon As New SQLiteConnection(sqlitedbpath)
        Try
            sqlitecon.Open()
            Dim sqlitecmd As New SQLiteCommand("Select name  FROM sqlite_master;", sqlitecon)

            Dim reader As SQLiteDataReader = sqlitecmd.ExecuteReader
            Dim table_tablenames As New DataTable
            table_tablenames.Load(reader)
            reader.Close()
            Dim tableExists As Boolean = False
            For Each row As DataRow In table_tablenames.Rows
                If row.Item(0) = "tbl_CSIFLEX_VERSION" Then tableExists = True
            Next

            If tableExists = True Then
                sqlitecmd = New SQLiteCommand("Select version FROM tbl_CSIFLEX_VERSION", sqlitecon)
                table_tablenames = New DataTable
                reader = sqlitecmd.ExecuteReader
                table_tablenames.Load(reader)

                If table_tablenames.Rows(0).Item(0) = 1894 Then

                    sqlitecmd = New SQLiteCommand("update tbl_CSIFLEX_VERSION SET version =1865 where version =1894", sqlitecon)
                    sqlitecmd.ExecuteNonQuery()

                    Return False
                End If

                If table_tablenames.Rows(0).Item(0) < 1894 Then
                    Dim drop_list As String = ""
                    For Each row As DataRow In table_tablenames.Rows
                        drop_list = drop_list & "Drop table " & row.Item(0) & "; "
                    Next

                    Dim cmd As New SQLiteCommand(drop_list, sqlitecon)
                    cmd.ExecuteNonQuery()
                    sqlitecon.Close()

                    Return True
                Else
                    Return False
                End If
            Else
                Dim drop_list As String = ""
                For Each row As DataRow In table_tablenames.Rows
                    If row.Item(0).ToString().StartsWith("tbl_") Then drop_list = drop_list & "Drop table " & row.Item(0) & "; "
                Next

                Dim cmd As New SQLiteCommand(drop_list, sqlitecon)
                cmd.ExecuteNonQuery()


                Return True
            End If

            If sqlitecon.State = ConnectionState.Open Then sqlitecon.Close()
        Catch ex As Exception
            MsgBox("Unable To complete the database update")
            LogClientError(ex.Message)
            If sqlitecon.State = ConnectionState.Open Then sqlitecon.Close()
            Return False
        End Try
    End Function

    Public Sub UpdateDB_SQLite()



        If check_if_MCH_db_rebuild_is_needed() Then
            Dim years_() As String
            If (File.Exists(ClientRootPath & "\sys\years_.csys")) Then
                Using streader As New StreamReader(ClientRootPath + "\sys\years_.csys")
                    Try

                        ' If streader.ReadLine.Empty() Then
                        years_ = streader.ReadLine().Split(",")
                    Catch

                        '  Forms.MessageBox.Show("Stream is problamatic HERE")
                    End Try



                End Using

            End If

            Try
                For Each year As String In years_
                    If String.IsNullOrEmpty(year) Then FirstUpdateDB_SQLite(year)
                Next
            Catch
                '   Forms.MessageBox.Show("Stream is problamatic 2")
            End Try
            GoTo End_

        End If



        OEEUpdate_SQLite()
        OperatorUpdate_SQLite()
        PartnumberUpdate_SQLite()
        ReportedpartsUpdate_SQLite()

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim machcsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_MACHINE_" + DateTime.Now.Year.ToString()) And
                    System.IO.Path.GetExtension(File) = ".CSV" Then
                    If System.IO.Path.GetFileName(File).Count = 17 Then
                        machcsvfile = File
                    End If
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            Dim machlastupdate As New Dictionary(Of String, DateTime)
            Dim machlistdt As New DataTable
            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)

            Using sqlitecon As New SQLiteConnection(sqlitedbpath)
                sqlitecon.Open()
                'Using sqlitecmd As New sqliteCommand("SELECT *", sqlitecon)

                'sqlitecmd.ExecuteReader()
                Dim sqliteda As New SQLiteDataAdapter("Select table_name,original_name from tbl_renamemachines ", sqlitecon)

                sqliteda.Fill(machlistdt)

                '''''''''''''''''SQLITE DATETIME
                '                private string DateTimeSQLite(DateTime datetime)
                '{
                '      string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
                '      return string.Format(dateTimeFormat, datetime.Year,        datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second,datetime.Millisecond);
                '}


                'string insertQuerry = "INSERT INTO Customer VALUES (5, 'Allen', 'Manager', 35,DateTimeSQLite(DateTime.Now), DateTimeSQLite(DateTime.Now))";



                'Then you can convert data in 'datetime' column to standard .Net DateTime format.

                'Query : "SELECT ID, DateModified FROM Customer"
                'string id = reader.GetInt64(0).ToString();
                'DateTime dateTime = (DateTime)reader[1];

                '''''''''''''''''''''''''''''''''''''''''''


                For Each row In machlistdt.Rows

                    Dim lastdate As DateTime = New DateTime(1900, 1, 1)
                    Using sqlitecmd As New SQLiteCommand("Select max(Date_) as 'maxdate' from tbl_" & row("table_name"), sqlitecon)
                        Dim reader As SQLiteDataReader = sqlitecmd.ExecuteReader()
                        reader.Read()
                        If Not reader("maxdate").Equals(DBNull.Value) Then
                            'lastdate = reader.GetDateTime("maxdate")
                            Dim id As String = reader("maxdate")
                            lastdate = DateTime.Parse(id)
                        End If
                        reader.Close()
                    End Using

                    machlastupdate.Add(row("original_name"), lastdate)

                    If (lastdate > maxlastdate) Then
                        maxlastdate = lastdate
                    End If

                Next

                'End Using
                sqlitecon.Close()
            End Using




            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                ' Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                    firstdate = CreateDateStr(maxlastdate)
                End If


                For Each match In File.ReadLines(machcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogClientError("Unable to find start line:" + ex.Message)
            End Try



            'Dim machlist As New List(Of String)

            'open the file "data.csv" which is a CSV file with headers
            Using csv As New CsvReader(New StreamReader(machcsvfile), True, ","c)

                Dim fieldCount As Integer = csv.FieldCount
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                Dim headers As String() = csv.GetFieldHeaders()

                csv.MoveTo(startline)

                Dim sqliteConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)
                sqliteConn.Open()

                Dim sqlitecmd As New SQLiteCommand("BEGIN TRANSACTION;", sqliteConn)
                sqlitecmd.ExecuteNonQuery()



                Dim tempdate As New DateTime()
                Dim temptime As New DateTime()
                Dim status As String

                Dim part_comment As String = ""

                While (csv.ReadNextRecord())

                    Try
                        'For Each row As DataRow In result
                        '    resultTable.ImportRow(row)
                        'Next
                        Dim renamedmachine As String = RenameMachine(csv(0))


                        If Not (machlastupdate.ContainsKey(csv(0))) Then
                            machlastupdate.Add(csv(0), maxlastdate)

                            sqlitecmd = New SQLiteCommand("insert into tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqliteConn)
                            sqlitecmd.ExecuteNonQuery()

                            sqlitecmd = New SQLiteCommand("CREATE TABLE if not exists tbl_" & renamedmachine & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, Partnumber varchar(255), UNIQUE (time_,status))", sqliteConn)
                            sqlitecmd.ExecuteNonQuery()


                        End If

                        'csv line example
                        'GANESH,07/23/2015,2,23:28:03,01:30:00,7317,CYCLE ON,0,
                        sqlitecmd = New SQLiteCommand("Replace into tbl_" & renamedmachine +
                                         "(month_, day_, year_, time_, Date_, status, shift, cycletime, Partnumber)" +
                                         "VALUES (@month,@day,@year,@time,@date,@status,@shift,@cycletime, @partnumber);", sqliteConn)
                        tempdate = csv(1) 'row("DATE")


                        sqlitecmd.Parameters.Add(New SQLiteParameter("@month", tempdate.Month))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@day", tempdate.Day))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@year", tempdate.Year))
                        temptime = csv(3) 'row("START TIME")
                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@time", temptime))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@date", temptime))

                        'If row("MACHINE STATUS") = "_PARTNUMBER" Then
                        If csv(6) = "_PARTNUMBER" Then
                            status = "_PARTNO:" & csv(8).Split(";")(0)
                            part_comment = csv(8)
                        Else
                            status = csv(6) 'row("MACHINE STATUS")
                        End If

                        sqlitecmd.Parameters.Add(New SQLiteParameter("@status", status))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@shift", csv(2))) 'row("SHIFT")))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@cycletime", csv(5))) 'row("ELAPSED TIME")))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@partnumber", part_comment))
                        sqlitecmd.ExecuteNonQuery() 'async doesnt wait for finish



                    Catch ex As Exception
                        LogClientError("Error parsing csv file:" + ex.Message)
                    End Try
                End While

                sqlitecmd = New SQLiteCommand("COMMIT TRANSACTION;", sqliteConn)
                sqlitecmd.ExecuteNonQuery()

                sqliteConn.Close()

            End Using

            'beginSqliteTransac(False)




            Thread.CurrentThread.CurrentCulture = originalCulture

        Catch ex As Exception
            LogClientError("unable to complete database update:" + ex.Message)
        End Try
End_:
    End Sub

    Public Sub OEEUpdate_SQLite()

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim oeecsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_OEE_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    oeecsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim machlastupdate As New Dictionary(Of String, DateTime)
            'Dim machlistdt As New DataTable
            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)


            Dim sqliteCnt As New SQLiteConnection(sqlitedbpath)
            sqliteCnt.Open()

            'For Each row In machlistdt.Rows

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
            ' Dim lastdate
            Using mysqlcmd As New SQLiteCommand("Select max(trx_time) as maxdate from tbl_oee", sqliteCnt)
                Dim reader As SQLiteDataReader = mysqlcmd.ExecuteReader()
                reader.Read()
                If Not reader("maxdate").Equals(DBNull.Value) Then
                    lastdate = reader.GetDateTime(0)
                    'lastdate = reader.GetDateTime("maxdate")
                End If
                reader.Close()
            End Using

            ' machlastupdate.Add(row("original_name"), lastdate)

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            'Next

            'End Using





            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                ' Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                    firstdate = CreateDateStr(maxlastdate)
                End If


                For Each match In File.ReadLines(oeecsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServiceError("Unable to find start line:" + ex.Message, 1)
            End Try

            Dim sqlitecmd As New SQLiteCommand("BEGIN TRANSACTION;", sqliteCnt)
            sqlitecmd.ExecuteNonQuery()


            'Dim machlist As New List(Of String)

            'open the file "data.csv" which is a CSV file with headers
            Using csv As New CsvReader(New StreamReader(oeecsvfile), True, ","c)

                Dim fieldCount As Integer = csv.FieldCount
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                Dim headers As String() = csv.GetFieldHeaders()

                csv.MoveTo(startline - 1)

                Dim cmd As New SQLiteCommand
                Dim tempdate As New DateTime()
                Dim temptime As New DateTime()
                Dim status As String

                While (csv.ReadNextRecord())

                    Try

                        'csv line example
                        'OKUMA,09/24/2016,13:25:52,0.05,85.71,83.33,0.04,0
                        cmd = New SQLiteCommand("Replace into tbl_oee" +
                                             " (MACHINE, trx_time, Avail, Performance, Quality, OEE, HEADPALLET)" +
                                             " VALUES (@MACHINE,@trx_time,@Avail,@Performance,@Quality,@OEE,@HEADPALLET);", sqliteCnt)
                        tempdate = csv(1) 'row("DATE")
                        temptime = csv(2) 'row("START TIME")
                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

                        cmd.Parameters.Add(New SQLiteParameter("@MACHINE", csv(0)))
                        cmd.Parameters.Add(New SQLiteParameter("@trx_time", temptime))
                        cmd.Parameters.Add(New SQLiteParameter("@Avail", csv(3)))
                        cmd.Parameters.Add(New SQLiteParameter("@Performance", csv(4)))
                        cmd.Parameters.Add(New SQLiteParameter("@Quality", csv(5)))
                        cmd.Parameters.Add(New SQLiteParameter("@OEE", csv(6)))
                        cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(7))) 'row("SHIFT")))

                        cmd.ExecuteNonQuery() 'async doesnt wait for finish

                    Catch ex As Exception
                        LogClientError("Error parsing oee csv file:" + ex.Message)
                    End Try
                End While
            End Using

            sqlitecmd = New SQLiteCommand("COMMIT TRANSACTION;", sqliteCnt)
            sqlitecmd.ExecuteNonQuery()
            sqliteCnt.Close()
            Thread.CurrentThread.CurrentCulture = originalCulture

            'isloading = False

        Catch ex As Exception
            LogClientError("unable to complete oee table update:" + ex.Message)
        End Try

    End Sub

    Public Sub OperatorUpdate_SQLite()

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim operatorcsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_OPERATOR_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    operatorcsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim machlastupdate As New Dictionary(Of String, DateTime)
            'Dim machlistdt As New DataTable
            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)


            Dim sqliteCnt As New SQLiteConnection(sqlitedbpath)
            sqliteCnt.Open()

            'For Each row In machlistdt.Rows

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
            Using mysqlcmd As New SQLiteCommand("Select max(trx_date) as maxdate from tbl_operator", sqliteCnt)
                Dim reader As SQLiteDataReader = mysqlcmd.ExecuteReader()
                reader.Read()
                If Not reader("maxdate").Equals(DBNull.Value) Then
                    lastdate = reader.GetDateTime(0)
                End If
                reader.Close()
            End Using

            ' machlastupdate.Add(row("original_name"), lastdate)

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            'Next

            'End Using





            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                ' Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                    firstdate = CreateDateStr(maxlastdate)
                End If


                For Each match In File.ReadLines(operatorcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServiceError("Unable to find start line:" + ex.Message, 1)
            End Try


            'beginTransac(True)

            'Dim machlist As New List(Of String)

            'open the file "data.csv" which is a CSV file with headers
            Dim csv As New CsvReader(New StreamReader(operatorcsvfile), True, ","c)

            Dim fieldCount As Integer = csv.FieldCount
            csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
            csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

            Dim headers As String() = csv.GetFieldHeaders()
            csv.MoveTo(startline - 1)

            Dim cmd As New SQLiteCommand
            Dim tempdate As New DateTime()
            Dim temptime As New DateTime()
            'Dim status As String


            Dim sqlitecmd As New SQLiteCommand("BEGIN TRANSACTION;", sqliteCnt)
            sqlitecmd.ExecuteNonQuery()

            While (csv.ReadNextRecord())

                Try
                    'csv line example
                    '6021,212-100-10,49,00:01:47,0,00:00:00,01/05/2016,1,0
                    cmd = New SQLiteCommand("Replace into tbl_operator" +
                                     " (OPERATOR, PARTNO, QUANTITY, AVGCYCLETIME, GOODCYCLE, AVGGOODCYCLETIME, trx_date, shift, HEADPALLET)" +
                                     " VALUES (@OPERATOR,@PARTNO,@QUANTITY,@AVGCYCLETIME,@GOODCYCLE,@AVGGOODCYCLETIME,@trx_date,@shift,@HEADPALLET);", sqliteCnt)

                    tempdate = csv(6)
                    cmd.Parameters.Add(New SQLiteParameter("@OPERATOR", csv(0)))
                    cmd.Parameters.Add(New SQLiteParameter("@PARTNO", csv(1)))
                    cmd.Parameters.Add(New SQLiteParameter("@QUANTITY", csv(2)))
                    cmd.Parameters.Add(New SQLiteParameter("@AVGCYCLETIME", csv(3)))
                    cmd.Parameters.Add(New SQLiteParameter("@GOODCYCLE", csv(4)))
                    cmd.Parameters.Add(New SQLiteParameter("@AVGGOODCYCLETIME", csv(5)))
                    cmd.Parameters.Add(New SQLiteParameter("@trx_date", tempdate))
                    cmd.Parameters.Add(New SQLiteParameter("@shift", csv(7)))
                    cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(8)))

                    cmd.ExecuteNonQuery() 'async doesnt wait for finish

                Catch ex As Exception
                    LogClientError("Error parsing operator csv file:" + ex.Message)
                End Try
            End While

            sqlitecmd = New SQLiteCommand("COMMIT TRANSACTION;", sqliteCnt)
            sqlitecmd.ExecuteNonQuery()

            sqliteCnt.Close()



            Thread.CurrentThread.CurrentCulture = originalCulture

            'isloading = False

        Catch ex As Exception
            LogClientError("unable to complete operator table update:" + ex.Message)
        End Try

    End Sub

    Public Sub PartnumberUpdate_SQLite()

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim partnocsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_PARTNUMBER_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    partnocsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim machlastupdate As New Dictionary(Of String, DateTime)
            'Dim machlistdt As New DataTable
            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)


            Dim sqliteCnt As New SQLiteConnection(sqlitedbpath)
            sqliteCnt.Open()

            'For Each row In machlistdt.Rows

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
            Using mysqlcmd As New SQLiteCommand("Select max(end_time) as maxdate from  tbl_partnumber", sqliteCnt)
                Dim reader As SQLiteDataReader = mysqlcmd.ExecuteReader()
                reader.Read()
                If Not reader("maxdate").Equals(DBNull.Value) Then
                    lastdate = reader.GetDateTime(0)
                End If
                reader.Close()
            End Using

            ' machlastupdate.Add(row("original_name"), lastdate)

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            'Next






            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                ' Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                    firstdate = CreateDateStr(maxlastdate)
                End If


                For Each match In File.ReadLines(partnocsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogServiceError("Unable to find start line:" + ex.Message, 1)
            End Try


            Dim sqlitecmd As New SQLiteCommand("BEGIN TRANSACTION;", sqliteCnt)
            sqlitecmd.ExecuteNonQuery()

            'Dim machlist As New List(Of String)

            'open the file "data.csv" which is a CSV file with headers
            Using csv As New CsvReader(New StreamReader(partnocsvfile), True, ","c)

                Dim fieldCount As Integer = csv.FieldCount
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                Dim headers As String() = csv.GetFieldHeaders()

                csv.MoveTo(startline - 1)

                Dim cmd As New SQLiteCommand
                Dim tempdate As New DateTime()
                Dim temptime As New DateTime()
                'Dim status As String

                While (csv.ReadNextRecord())

                    Try

                        cmd = New SQLiteCommand("Replace into tbl_partnumber" +
                                         " (start_time, end_time, elapsed_time, machine, HEADPALLET, shift, Partno, total_cycle, good_cycle, short_cycle, long_cycle, avg_good_cycle_time, max_cycle_time, min_cycle_time)" +
                                         " VALUES (@start_time,@end_time,@elapsed_time,@machine,@HEADPALLET,@shift,@Partno,@total_cycle,@good_cycle,@short_cycle,@long_cycle,@avg_good_cycle_time,@max_cycle_time,@min_cycle_time);", sqliteCnt)

                        tempdate = csv(0)
                        temptime = csv(1)
                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                        cmd.Parameters.Add(New SQLiteParameter("@start_time", temptime))
                        temptime = csv(2)
                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                        cmd.Parameters.Add(New SQLiteParameter("@end_time", temptime))
                        cmd.Parameters.Add(New SQLiteParameter("@elapsed_time", csv(3)))
                        cmd.Parameters.Add(New SQLiteParameter("@machine", csv(4)))
                        cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(5)))
                        cmd.Parameters.Add(New SQLiteParameter("@shift", csv(6)))
                        cmd.Parameters.Add(New SQLiteParameter("@Partno", csv(7)))
                        cmd.Parameters.Add(New SQLiteParameter("@total_cycle", csv(8)))
                        cmd.Parameters.Add(New SQLiteParameter("@good_cycle", csv(9)))
                        cmd.Parameters.Add(New SQLiteParameter("@short_cycle", csv(10)))
                        cmd.Parameters.Add(New SQLiteParameter("@long_cycle", csv(11)))
                        cmd.Parameters.Add(New SQLiteParameter("@avg_good_cycle_time", csv(12)))
                        cmd.Parameters.Add(New SQLiteParameter("@max_cycle_time", csv(13)))
                        cmd.Parameters.Add(New SQLiteParameter("@min_cycle_time", csv(14)))


                        cmd.ExecuteNonQuery() 'async doesnt wait for finish


                    Catch ex As Exception
                        LogServiceError("Error parsing partnumber csv file:" + ex.Message, 1)
                    End Try
                End While

            End Using

            sqlitecmd = New SQLiteCommand("COMMIT TRANSACTION;", sqliteCnt)
            sqlitecmd.ExecuteNonQuery()
            sqliteCnt.Close()
            Thread.CurrentThread.CurrentCulture = originalCulture

            'isloading = False

        Catch ex As Exception
            LogServiceError("unable to complete partnumber table update:" + ex.Message, 1)
        End Try

    End Sub

    Public Sub ReportedpartsUpdate_SQLite()

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim reportedpartscsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_REPORTEDPARTS_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    reportedpartscsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim machlastupdate As New Dictionary(Of String, DateTime)
            'Dim machlistdt As New DataTable
            Dim maxlastdate As DateTime = DateTime.Now.AddMonths(-3)


            Dim sqliteCnt As New SQLiteConnection(sqlitedbpath)
            sqliteCnt.Open()

            'For Each row In machlistdt.Rows

            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
            Using mysqlcmd As New SQLiteCommand("Select max(trx_time) as maxdate from tbl_reportedparts", sqliteCnt)
                Dim reader As SQLiteDataReader = mysqlcmd.ExecuteReader()
                reader.Read()
                If Not reader("maxdate").Equals(DBNull.Value) Then
                    lastdate = reader.GetDateTime(0)
                End If
                reader.Close()
            End Using

            ' machlastupdate.Add(row("original_name"), lastdate)

            If (lastdate > maxlastdate) Then
                maxlastdate = lastdate
            End If

            'Next

            'End Using





            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                ' Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                If (maxlastdate > DateTime.Now.AddMonths(-3)) Then
                    'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                    firstdate = CreateDateStr(maxlastdate)
                End If


                For Each match In File.ReadLines(reportedpartscsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogClientError("Unable to find start line:" + ex.Message)
            End Try


            Dim sqlitecmd As New SQLiteCommand("BEGIN TRANSACTION;", sqliteCnt)
            sqlitecmd.ExecuteNonQuery()

            'Dim machlist As New List(Of String)

            'open the file "data.csv" which is a CSV file with headers
            Using csv As New CsvReader(New StreamReader(reportedpartscsvfile), True, ","c)

                Dim fieldCount As Integer = csv.FieldCount
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                Dim headers As String() = csv.GetFieldHeaders()

                csv.MoveTo(startline - 1)

                Dim cmd As New SQLiteCommand
                Dim tempdate As New DateTime()
                Dim temptime As New DateTime()
                'Dim status As String

                While (csv.ReadNextRecord())

                    Try

                        cmd = New SQLiteCommand("Replace into tbl_reportedparts" +
                                             " (machine, trx_time, total_parts, bad_parts, HEADPALLET, ideal_cycle_time)" +
                                             " VALUES (@machine,@trx_time,@total_parts,@bad_parts,@HEADPALLET,@ideal_cycle_time);", sqliteCnt)

                        tempdate = csv(1)
                        temptime = csv(2)
                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

                        cmd.Parameters.Add(New SQLiteParameter("@machine", csv(0)))
                        cmd.Parameters.Add(New SQLiteParameter("@trx_time", temptime))
                        cmd.Parameters.Add(New SQLiteParameter("@total_parts", csv(3)))
                        cmd.Parameters.Add(New SQLiteParameter("@bad_parts", csv(4)))
                        cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(5)))
                        cmd.Parameters.Add(New SQLiteParameter("@ideal_cycle_time", csv(6)))

                        cmd.ExecuteNonQuery() 'async doesnt wait for finish

                    Catch ex As Exception
                        LogClientError("Error parsing reportedparts csv file:" + ex.Message)
                    End Try
                End While
            End Using

            sqlitecmd = New SQLiteCommand("COMMIT TRANSACTION;", sqliteCnt)
            sqlitecmd.ExecuteNonQuery()

            sqliteCnt.Close()
            ' beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

            'isloading = False

        Catch ex As Exception
            LogClientError("unable to complete reportedparts table update:" + ex.Message)
        End Try

    End Sub

    Public Sub ImportDB_sqlite(pathCSV As String)

        Try
            'Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim machcsvfile As String = ""
            ''Dim MostRecentCSV As Integer = 0
            'For Each File In files
            If System.IO.Path.GetFileName(pathCSV).StartsWith("_MACHINE_") And System.IO.Path.GetExtension(pathCSV) = ".CSV" Then
                If System.IO.Path.GetFileName(pathCSV).Count = 17 Then
                    machcsvfile = pathCSV

                    'Next

                    Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
                    ' Change culture to en-US.

                    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


                    Dim machlastupdate As New Dictionary(Of String, DateTime)
                    Dim machlistdt As New DataTable
                    Dim minlastdate As DateTime = DateTime.Now.AddMonths(-3)

                    Using mysqlcon As New MySqlConnection(MySqlConnectionString)
                        mysqlcon.Open()
                        'Using mysqlcmd As New MySqlCommand("SELECT *", mysqlcon)

                        'mysqlcmd.ExecuteReader()
                        Dim mysqlda As New MySqlDataAdapter("Select table_name,original_name from csi_database.tbl_renamemachines ", mysqlcon)

                        mysqlda.Fill(machlistdt)


                        For Each row In machlistdt.Rows

                            Dim lastdate As DateTime = New DateTime(1900, 1, 1)
                            Using mysqlcmd As New MySqlCommand("Select min(Date_) as mindate from CSI_Database.tbl_" & row("table_name"), mysqlcon)
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

                        'End Using
                        mysqlcon.Close()
                    End Using



                    Dim endline As Long = File.ReadLines(machcsvfile).Count()
                    'Dim endline As Long = 0

                    Try
                        'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                        Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))


                        If (minlastdate < DateTime.Now.AddMonths(-3)) Then
                            'firstdate = maxlastdate.Month.ToString() + "/" + maxlastdate.Day.ToString() + "/" + maxlastdate.Year.ToString()
                            firstdate = CreateDateStr(minlastdate)
                        End If



                        For Each match In File.ReadLines(machcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                            'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                            endline = match.lineNumber
                            Exit For
                        Next
                    Catch ex As Exception
                        LogServerError("Unable to find end line:" + ex.Message, 1)
                    End Try



                    beginTransac(True)

                    'Dim machlist As New List(Of String)

                    'open the file "data.csv" which is a CSV file with headers
                    Using csv As New CsvReader(New StreamReader(machcsvfile), True, ","c)

                        Dim fieldCount As Integer = csv.FieldCount
                        csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                        csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                        Dim headers As String() = csv.GetFieldHeaders()

                        'csv.MoveTo(startline)

                        Dim cmd As New MySqlCommand
                        Dim tempdate As New DateTime()
                        Dim temptime As New DateTime()
                        Dim status As String
                        Dim linecnt As Long = 0
                        While (csv.ReadNextRecord() And linecnt < endline)

                            Try
                                Dim renamedmachine As String = RenameMachine(csv(0))

                                If Not (machlastupdate.ContainsKey(csv(0))) Then
                                    machlastupdate.Add(csv(0), minlastdate)

                                    'Dim sqlConn As MySqlConnection = New MySqlConnection(MySqlConnectionString)
                                    'sqlConn.Open()
                                    cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqlInserConn)
                                    cmd.ExecuteNonQuery()

                                    cmd = New MySqlCommand("CREATE TABLE if not exists CSI_Database.tbl_" & renamedmachine & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, UNIQUE KEY (time_,status))", sqlInserConn)
                                    cmd.ExecuteNonQuery()

                                    'sqlConn.Close()
                                End If


                                'csv line example
                                'GANESH,07/23/2015,2,23:28:03,01:30:00,7317,CYCLE ON,0,
                                cmd = New MySqlCommand("Replace into CSI_Database.tbl_" & renamedmachine +
                                                 "(month_, day_, year_, time_, Date_, status, shift, cycletime)" +
                                                 "VALUES (@month,@day,@year,@time,@date,@status,@shift,@cycletime);", sqlInserConn)
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
                                Else
                                    status = csv(6) 'row("MACHINE STATUS")
                                End If

                                cmd.Parameters.Add(New MySqlParameter("@status", status))
                                cmd.Parameters.Add(New MySqlParameter("@shift", csv(2))) 'row("SHIFT")))
                                cmd.Parameters.Add(New MySqlParameter("@cycletime", csv(5))) 'row("ELAPSED TIME")))
                                cmd.ExecuteNonQuery() 'async doesnt wait for finish


                            Catch ex As Exception
                                LogServerError("Error parsing csv file:" + ex.Message, 1)
                            End Try
                            linecnt += 1
                        End While

                    End Using

                    beginTransac(False)
                    Thread.CurrentThread.CurrentCulture = originalCulture

                End If
            End If
        Catch ex As Exception
            LogServerError("unable to complete database creation:" + ex.Message, 1)
        End Try

    End Sub

#End Region


#Region "first update SQLITE"

    'Public Sub FirstUpdateDB_SQLite()



    '    Try
    '        Dim sqliteCnt As New SQLiteConnection
    '        sqliteCnt = New SQLiteConnection(sqlitedbpath)
    '        sqliteCnt.Open()

    '        Dim sqlite3 As String = "CREATE TABLE if not exists tbl_colors (statut VARCHAR(255) PRIMARY KEY, color TEXT);"
    '        Dim cmdCreateDeviceTable3 As New SQLiteCommand(sqlite3, sqliteCnt)
    '        cmdCreateDeviceTable3.ExecuteNonQuery()


    '        Dim sqlite8 As String = "CREATE TABLE if not exists tbl_renamemachines (table_name VARCHAR(255) PRIMARY KEY, original_name VARCHAR(255));"
    '        Dim cmdCreateDeviceTable8 As New SQLiteCommand(sqlite8, sqliteCnt)
    '        cmdCreateDeviceTable8.ExecuteNonQuery()

    '        Dim sqlite9 As String = "CREATE TABLE if not exists tbl_userconfig (name VARCHAR(255) PRIMARY KEY, state TEXT);"
    '        Dim cmdCreateDeviceTable9 As New SQLiteCommand(sqlite9, sqliteCnt)
    '        cmdCreateDeviceTable9.ExecuteNonQuery()

    '        Dim sqlite10 As String = "CREATE TABLE if not exists tbl_Groups (users VARCHAR(255), groups VARCHAR(255), machines VARCHAR(255), CONSTRAINT pk_GM PRIMARY KEY (users,groups,machines));"
    '        Dim cmdCreateDeviceTable10 As New SQLiteCommand(sqlite10, sqliteCnt)
    '        cmdCreateDeviceTable10.ExecuteNonQuery()

    '        sqlite3 = "insert into tbl_colors (statut, color) VALUES('NOT CONNECTED','#ABABAB');"
    '        cmdCreateDeviceTable3 = New SQLiteCommand(sqlite3, sqliteCnt)
    '        cmdCreateDeviceTable3.ExecuteNonQuery()

    '        sqliteCnt.Close()
    '    Catch ex As Exception
    '        LogClientError("Error configuring the database:" + ex.Message)
    '    End Try



    '    Try
    '        Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
    '        Dim machcsvfile As String = ""
    '        'Dim MostRecentCSV As Integer = 0
    '        For Each File In files
    '            If System.IO.Path.GetFileName(File).StartsWith("_MACHINE_" + DateTime.Now.Year.ToString()) And
    '                System.IO.Path.GetExtension(File) = ".CSV" Then
    '                If System.IO.Path.GetFileName(File).Count = 17 Then
    '                    machcsvfile = File
    '                End If
    '            End If
    '        Next

    '        'machcsvfile = "C:\Users\asalm\Desktop\_eNETDNC\_REPORTS\" + "_MACHINE_" + year__ + ".CSV"

    '        'Dim dt As DataTable = New DataTable

    '        Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
    '        ' Change culture to en-US.

    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


    '        'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
    '        Dim startline As Long = 0

    '        Try
    '            'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
    '            Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

    '            For Each match In File.ReadLines(machcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
    '                'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
    '                startline = match.lineNumber - 1
    '                Exit For
    '            Next
    '        Catch ex As Exception
    '            'LogServerError("Unable to find start line:" + ex.Message)
    '            LogClientError("Unable to find start line:" + ex.Message)
    '        End Try


    '        'beginSqliteTransac(True)

    '        Dim machlist As New List(Of String)

    '        'open the file "data.csv" which is a CSV file with headers
    '        Using csv As New CsvReader(New StreamReader(machcsvfile), True, ","c)

    '            Dim fieldCount As Integer = csv.FieldCount
    '            csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
    '            csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

    '            Dim headers As String() = csv.GetFieldHeaders()

    '            csv.MoveTo(startline - 1)


    '            Dim sqliteConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)
    '            sqliteConn.Open()

    '            Dim sqlitecmd As New SQLiteCommand("BEGIN TRANSACTION;", sqliteConn)
    '            sqlitecmd.ExecuteNonQuery()

    '            Dim tempdate As New DateTime()
    '            Dim temptime As New DateTime()
    '            Dim status As String

    '            While (csv.ReadNextRecord())

    '                Try
    '                    'For Each row As DataRow In result
    '                    '    resultTable.ImportRow(row)
    '                    'Next
    '                    Dim renamedmachine As String = RenameMachine(csv(0))


    '                    If Not (machlist.Contains(csv(0))) Then
    '                        machlist.Add(csv(0))


    '                        sqlitecmd = New SQLiteCommand("insert into tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqliteConn)
    '                        sqlitecmd.ExecuteNonQuery()

    '                        sqlitecmd = New SQLiteCommand("CREATE TABLE if not exists tbl_" & renamedmachine & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, UNIQUE (time_,status))", sqliteConn)
    '                        sqlitecmd.ExecuteNonQuery()


    '                    End If

    '                    'csv line example
    '                    'GANESH,07/23/2015,2,23:28:03,01:30:00,7317,CYCLE ON,0,
    '                    sqlitecmd = New SQLiteCommand("Replace into tbl_" & renamedmachine +
    '                                     "(month_, day_, year_, time_, Date_, status, shift, cycletime)" +
    '                                     "VALUES (@month,@day,@year,@time,@date,@status,@shift,@cycletime);", sqliteConn)
    '                    tempdate = csv(1) 'row("DATE")


    '                    sqlitecmd.Parameters.Add(New SQLiteParameter("@month", tempdate.Month))
    '                    sqlitecmd.Parameters.Add(New SQLiteParameter("@day", tempdate.Day))
    '                    sqlitecmd.Parameters.Add(New SQLiteParameter("@year", tempdate.Year))
    '                    temptime = csv(3) 'row("START TIME")
    '                    temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
    '                    sqlitecmd.Parameters.Add(New SQLiteParameter("@time", temptime))
    '                    sqlitecmd.Parameters.Add(New SQLiteParameter("@date", temptime))

    '                    'If row("MACHINE STATUS") = "_PARTNUMBER" Then
    '                    If csv(6) = "_PARTNUMBER" Then
    '                        status = "_PARTNO:" & csv(8).Split(";")(0)
    '                    Else
    '                        status = csv(6) 'row("MACHINE STATUS")
    '                    End If

    '                    sqlitecmd.Parameters.Add(New SQLiteParameter("@status", status))
    '                    sqlitecmd.Parameters.Add(New SQLiteParameter("@shift", csv(2))) 'row("SHIFT")))
    '                    sqlitecmd.Parameters.Add(New SQLiteParameter("@cycletime", csv(5))) 'row("ELAPSED TIME")))
    '                    sqlitecmd.ExecuteNonQuery() 'async doesnt wait for finish



    '                Catch ex As Exception
    '                    LogClientError("Error parsing csv file:" + ex.Message)
    '                End Try
    '            End While

    '            sqlitecmd = New SQLiteCommand("COMMIT TRANSACTION;", sqliteConn)
    '            sqlitecmd.ExecuteNonQuery()

    '            sqliteConn.Close()

    '        End Using

    '        'beginSqliteTransac(False)

    '        Thread.CurrentThread.CurrentCulture = originalCulture

    '        FirstOEEUpdate_SQLite()
    '        FirstOperatorUpdate_SQLite()
    '        FirstPartnumberUpdate_SQLite()
    '        FirstReportedpartUpdate_SQLite()



    '    Catch ex As Exception
    '        LogClientError("unable to complete database creation:" + ex.Message)
    '    End Try

    'End Sub

    'Private Sub FirstOEEUpdate_SQLite()

    '    Try
    '        Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
    '        Dim oeecsvfile As String = ""
    '        For Each File In files
    '            If System.IO.Path.GetFileName(File).StartsWith("_OEE_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
    '                oeecsvfile = File
    '            End If
    '        Next


    '        Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
    '        ' Change culture to en-US.

    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


    '        'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
    '        Dim startline As Long = 0

    '        Try
    '            'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
    '            Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

    '            For Each match In File.ReadLines(oeecsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
    '                'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
    '                startline = match.lineNumber - 1
    '                Exit For
    '            Next
    '        Catch ex As Exception
    '            LogServerError("Unable to find start line:" + ex.Message)
    '        End Try

    '        Dim cmd As New SQLiteCommand

    '        Try

    '            Dim sqliteCnt As New SQLiteConnection
    '            sqliteCnt = New SQLiteConnection(sqlitedbpath)
    '            sqliteCnt.Open()

    '            Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_oee (`MACHINE` varchar(75) NOT NULL,  `trx_time` datetime NOT NULL, `Avail` float NOT NULL, `Performance` float NOT NULL, `Quality` float NOT NULL, `OEE` float NOT NULL,`HEADPALLET` int);"

    '            cmd = New SQLiteCommand(cmdstr, sqliteCnt)

    '            'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

    '            cmd.ExecuteNonQuery() 'async doesnt wait for finish

    '            Dim machlist As New List(Of String)

    '            'open the file "data.csv" which is a CSV file with headers
    '            Using csv As New CsvReader(New StreamReader(oeecsvfile), True, ","c)

    '                Dim fieldCount As Integer = csv.FieldCount
    '                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
    '                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

    '                Dim headers As String() = csv.GetFieldHeaders()

    '                csv.MoveTo(startline - 1)

    '                'Dim cmd As New MySqlCommand
    '                Dim tempdate As New DateTime()
    '                Dim temptime As New DateTime()
    '                'Dim status As String

    '                While (csv.ReadNextRecord())

    '                    Try
    '                        Dim renamedmachine As String = RenameMachine(csv(0))

    '                        If Not (machlist.Contains(csv(0))) Then
    '                            machlist.Add(csv(0))

    '                            cmd = New SQLiteCommand("insert or ignore into tbl_renamemachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqliteCnt)
    '                            cmd.ExecuteNonQuery()
    '                        End If

    '                        'csv line example
    '                        'OKUMA,09/24/2016,13:25:52,0.05,85.71,83.33,0.04,0
    '                        cmd = New SQLiteCommand("Replace into tbl_oee" +
    '                                         " (MACHINE, trx_time, Avail, Performance, Quality, OEE, HEADPALLET)" +
    '                                         " VALUES (@MACHINE,@trx_time,@Avail,@Performance,@Quality,@OEE,@HEADPALLET);", sqliteCnt)
    '                        tempdate = csv(1) 'row("DATE")
    '                        temptime = csv(2) 'row("START TIME")
    '                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

    '                        cmd.Parameters.Add(New SQLiteParameter("@MACHINE", csv(0)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@trx_time", temptime))
    '                        cmd.Parameters.Add(New SQLiteParameter("@Avail", csv(3)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@Performance", csv(4)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@Quality", csv(5)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@OEE", csv(6)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(7))) 'row("SHIFT")))

    '                        cmd.ExecuteNonQuery() 'async doesnt wait for finish


    '                    Catch ex As Exception
    '                        LogServerError("Error parsing oee csv file:" + ex.Message)
    '                    End Try
    '                End While

    '            End Using

    '        Catch ex As Exception
    '            LogClientError("Error uploading oee csv file:" + ex.Message)
    '        End Try


    '        Thread.CurrentThread.CurrentCulture = originalCulture

    '    Catch ex As Exception
    '        LogClientError("unable to complete OEE Table creation:" + ex.Message)
    '    End Try
    'End Sub

    'Private Sub FirstOperatorUpdate_SQLite()

    '    Try
    '        Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
    '        Dim opercsvfile As String = ""
    '        For Each File In files
    '            If System.IO.Path.GetFileName(File).StartsWith("_OPERATOR_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
    '                opercsvfile = File
    '            End If
    '        Next


    '        Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
    '        ' Change culture to en-US.

    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


    '        'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
    '        Dim startline As Long = 0

    '        Try
    '            'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
    '            Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

    '            For Each match In File.ReadLines(opercsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
    '                'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
    '                startline = match.lineNumber - 1
    '                Exit For
    '            Next
    '        Catch ex As Exception
    '            LogClientError("Unable to find start line:" + ex.Message)
    '        End Try


    '       ' beginTransac(True)

    '        Dim cmd As New SQLiteCommand

    '        Try
    '            Dim sqliteCnt As New SQLiteConnection
    '            sqliteCnt = New SQLiteConnection(sqlitedbpath)
    '            sqliteCnt.Open()

    '            Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_operator (`OPERATOR` varchar(150) NOT NULL,  `PARTNO` varchar(255) NOT NULL, `QUANTITY` int NOT NULL, `AVGCYCLETIME` time NOT NULL, `GOODCYCLE` int NOT NULL, `AVGGOODCYCLETIME` time NOT NULL, `trx_date` date not null, `shift` int NOT NULL, `HEADPALLET` int);"

    '            cmd = New SQLiteCommand(cmdstr, sqliteCnt)

    '            'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

    '            cmd.ExecuteNonQuery() 'async doesnt wait for finish

    '            Dim machlist As New List(Of String)

    '            'open the file "data.csv" which is a CSV file with headers
    '            Using csv As New CsvReader(New StreamReader(opercsvfile), True, ","c)

    '                Dim fieldCount As Integer = csv.FieldCount
    '                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
    '                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

    '                Dim headers As String() = csv.GetFieldHeaders()

    '                csv.MoveTo(startline - 1)

    '                ''Dim cmd As New SQLiteCommand
    '                Dim tempdate As New DateTime()
    '                'Dim temptime As New DateTime()
    '                ''Dim status As String

    '                While (csv.ReadNextRecord())

    '                    Try

    '                        'csv line example
    '                        '6021,212-100-10,49,00:01:47,0,00:00:00,01/05/2016,1,0
    '                        cmd = New SQLiteCommand("Replace into tbl_operator" +
    '                                         " (OPERATOR, PARTNO, QUANTITY, AVGCYCLETIME, GOODCYCLE, AVGGOODCYCLETIME, trx_date, shift, HEADPALLET)" +
    '                                         " VALUES (@OPERATOR,@PARTNO,@QUANTITY,@AVGCYCLETIME,@GOODCYCLE,@AVGGOODCYCLETIME,@trx_date,@shift,@HEADPALLET);", sqliteCnt)

    '                        tempdate = csv(6)
    '                        cmd.Parameters.Add(New SQLiteParameter("@OPERATOR", csv(0)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@PARTNO", csv(1)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@QUANTITY", csv(2)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@AVGCYCLETIME", csv(3)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@GOODCYCLE", csv(4)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@AVGGOODCYCLETIME", csv(5)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@trx_date", tempdate))
    '                        cmd.Parameters.Add(New SQLiteParameter("@shift", csv(7)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(8)))

    '                        cmd.ExecuteNonQuery() 'async doesnt wait for finish


    '                    Catch ex As Exception
    '                        LogClientError("Error parsing operator csv file:" + ex.Message)
    '                    End Try
    '                End While

    '            End Using

    '        Catch ex As Exception
    '            LogClientError("Error uploading operator csv file:" + ex.Message)
    '        End Try

    '        beginTransac(False)
    '        Thread.CurrentThread.CurrentCulture = originalCulture

    '    Catch ex As Exception
    '        LogClientError("unable to complete OPERATOR Table creation:" + ex.Message)
    '    End Try
    'End Sub

    'Private Sub FirstPartnumberUpdate_SQLite()

    '    Try
    '        Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
    '        Dim partcsvfile As String = ""
    '        For Each File In files
    '            If System.IO.Path.GetFileName(File).StartsWith("_PARTNUMBER_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
    '                partcsvfile = File
    '            End If
    '        Next


    '        Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
    '        Change culture to en-US.

    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


    '        Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
    '        Dim startline As Long = 0

    '        Try
    '            Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
    '            Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

    '            For Each match In File.ReadLines(partcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
    '                Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
    '                startline = match.lineNumber - 1
    '                Exit For
    '            Next
    '        Catch ex As Exception
    '            LogClientError("Unable to find start line:" + ex.Message)
    '        End Try


    '        beginTransac(True)

    '        Dim cmd As New SQLiteCommand

    '        Try
    '            Dim sqliteCnt As New SQLiteConnection
    '            sqliteCnt = New SQLiteConnection(sqlitedbpath)
    '            sqliteCnt.Open()
    '            Date    , START TIME,END TIME, ELAPSED TIME, MACHINE NAME, HEAD / PALLET, SHIFT, PART NUMBER, TOTAL CYC, GOOD CYC, Short CYC, Long CYC, AVG GOOD CYC TIME, MAX CYC TIME, MIN CYC TIME
    '            01/05/2016, 05:00:02, 12:10:07, 25805, 420, 0, 1, 212-100-10, 50, 0, 50, 0, 00:00:00, 00:08:11, 00:00:02
    '            Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_partnumber (`start_time` datetime NOT NULL,  `end_time` datetime NOT NULL, `elapsed_time` int NOT NULL, `machine` varchar(255) NOT NULL," +
    '                " `HEADPALLET` int NOT NULL, `shift` int NOT NULL, `Partno` varchar(255) not null, `total_cycle` int NOT NULL, `good_cycle` int not null, `short_cycle` int not null," +
    '                " `long_cycle` int not null, `avg_good_cycle_time` time not null, `max_cycle_time` time not null, `min_cycle_time` time not null);"

    '            cmd = New SQLiteCommand(cmdstr, sqliteCnt)

    '            cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

    '            cmd.ExecuteNonQuery() 'async doesnt wait for finish

    '            Dim machlist As New List(Of String)

    '            Open the file "data.csv" which Is a CSV file with headers
    '            Using csv As New CsvReader(New StreamReader(partcsvfile), True, ","c)

    '                Dim fieldCount As Integer = csv.FieldCount
    '                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
    '                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

    '                Dim headers As String() = csv.GetFieldHeaders()

    '                csv.MoveTo(startline - 1)

    '                Dim cmd As New SQLiteCommand
    '                Dim tempdate As New DateTime()
    '                Dim temptime As New DateTime()
    '                Dim status As String

    '                While (csv.ReadNextRecord())

    '                    Try
    '                        Dim renamedmachine As String = RenameMachine(csv(4))

    '                        If Not (machlist.Contains(csv(4))) Then
    '                            machlist.Add(csv(4))

    '                            cmd = New SQLiteCommand("insert or ignore into tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(4) & "')", sqliteCnt)
    '                            cmd.ExecuteNonQuery()
    '                        End If

    '                        Date    , START TIME,END TIME, ELAPSED TIME, MACHINE NAME, HEAD / PALLET, SHIFT, PART NUMBER, TOTAL CYC, GOOD CYC, Short CYC, Long CYC, AVG GOOD CYC TIME, MAX CYC TIME, MIN CYC TIME
    '                        01/05/2016, 05:00:02, 12:10:07, 25805, 420, 0, 1, 212-100-10, 50, 0, 50, 0, 00:00:00, 00:08:11, 00:00:02
    '                                Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_partnumber (`start_time` datetime NOT NULL,  `end_time` datetime NOT NULL, `elapsed_time` int NOT NULL, `machine` varchar(255) NOT NULL," +
    '                        " `HEADPALLET` int NOT NULL, `shift` int NOT NULL, `Partno` varchar(255) not null, `total_cycle` int NOT NULL, `good_cycle` int not null, `short_cycle` int not null," +
    '                        " `long_cycle` int not null, `avg_good_cycle_time` time not null, `max_cycle_time` time not null, `min_cycle_time` time not null);"

    '                        cmd = New SQLiteCommand("Replace into tbl_partnumber" +
    '                                         " (start_time, end_time, elapsed_time, machine, HEADPALLET, shift, Partno, total_cycle, good_cycle, short_cycle, long_cycle, avg_good_cycle_time, max_cycle_time, min_cycle_time)" +
    '                                         " VALUES (@start_time,@end_time,@elapsed_time,@machine,@HEADPALLET,@shift,@Partno,@total_cycle,@good_cycle,@short_cycle,@long_cycle,@avg_good_cycle_time,@max_cycle_time,@min_cycle_time);", sqliteCnt)

    '                        tempdate = csv(0)
    '                        temptime = csv(1)
    '                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
    '                        cmd.Parameters.Add(New SQLiteParameter("@start_time", temptime))
    '                        temptime = csv(2)
    '                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
    '                        cmd.Parameters.Add(New SQLiteParameter("@end_time", temptime))
    '                        cmd.Parameters.Add(New SQLiteParameter("@elapsed_time", csv(3)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@machine", csv(4)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(5)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@shift", csv(6)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@Partno", csv(7)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@total_cycle", csv(8)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@good_cycle", csv(9)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@short_cycle", csv(10)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@long_cycle", csv(11)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@avg_good_cycle_time", csv(12)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@max_cycle_time", csv(13)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@min_cycle_time", csv(14)))


    '                        cmd.ExecuteNonQuery() 'async doesnt wait for finish


    '                    Catch ex As Exception
    '                        LogClientError("Error parsing partnumber csv file:" + ex.Message)
    '                    End Try
    '                End While

    '            End Using

    '        Catch ex As Exception
    '            LogClientError("Error uploading partnumber csv file:" + ex.Message)
    '        End Try

    '        beginTransac(False)
    '        Thread.CurrentThread.CurrentCulture = originalCulture

    '    Catch ex As Exception
    '        LogClientError("unable to complete PARTNUMBER Table creation:" + ex.Message)
    '    End Try
    'End Sub

    'Private Sub FirstReportedpartUpdate_SQLite()
    '    Try
    '        Dim sqliteCnt As New SQLiteConnection
    '        Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
    '        Dim partcsvfile As String = ""
    '        For Each File In files
    '            If System.IO.Path.GetFileName(File).StartsWith("_REPORTEDPARTS_" + DateTime.Now.Year.ToString()) And System.IO.Path.GetExtension(File) = ".CSV" Then
    '                partcsvfile = File
    '            End If
    '        Next


    '        Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
    '        ' Change culture to en-US.

    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


    '        'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
    '        Dim startline As Long = 0

    '        Try

    '            sqliteCnt = New SQLiteConnection(sqlitedbpath)
    '            sqliteCnt.Open()
    '            'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
    '            Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

    '            For Each match In File.ReadLines(partcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
    '                'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
    '                startline = match.lineNumber - 1
    '                Exit For
    '            Next
    '        Catch ex As Exception
    '            LogClientError("Unable to find start line:" + ex.Message)
    '        End Try


    '        'beginTransac(True)

    '        Dim cmd As New SQLiteCommand

    '        Try
    '            'MACHINE NAME, Date, TIME, TOTAL PARTS, BAD PARTS, HEAD / PALLET, IDEAL CYCLETIME
    '            'OKUMA, 09 / 24 / 2016, 09: 59:29,3,8,0,1
    '            Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_reportedparts (`machine` varchar(255) NOT NULL,  `trx_time` datetime NOT NULL, `total_parts` int NOT NULL, `bad_parts` int NOT NULL," +
    '                " `HEADPALLET` int NOT NULL, `ideal_cycle_time` int NOT NULL);"

    '            cmd = New SQLiteCommand(cmdstr, sqliteCnt)

    '            'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

    '            cmd.ExecuteNonQuery() 'async doesnt wait for finish

    '            Dim machlist As New List(Of String)

    '            'open the file "data.csv" which is a CSV file with headers
    '            Using csv As New CsvReader(New StreamReader(partcsvfile), True, ","c)

    '                Dim fieldCount As Integer = csv.FieldCount
    '                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
    '                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

    '                Dim headers As String() = csv.GetFieldHeaders()

    '                csv.MoveTo(startline - 1)

    '                ''Dim cmd As New SQLiteCommand
    '                Dim tempdate As New DateTime()
    '                Dim temptime As New DateTime()
    '                ''Dim status As String

    '                While (csv.ReadNextRecord())

    '                    Try
    '                        Dim renamedmachine As String = RenameMachine(csv(0))

    '                        If Not (machlist.Contains(csv(0))) Then
    '                            machlist.Add(csv(0))

    '                            'cmd = New SQLiteCommand("insert ignore into tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqliteCnt)
    '                            'cmd.ExecuteNonQuery()

    '                            cmd = New SQLiteCommand("insert or ignore into tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqliteCnt)
    '                            cmd.ExecuteNonQuery()
    '                        End If

    '                        'Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_reportedparts (`machine` varchar(255) NOT NULL,  `trx_time` datetime NOT NULL, `total_parts` int NOT NULL, `bad_parts` int NOT NULL," +
    '                        '" `HEADPALLET` int NOT NULL, `ideal_cycle_time` int NOT NULL);"

    '                        cmd = New SQLiteCommand("Replace into tbl_reportedparts" +
    '                                         " (machine, trx_time, total_parts, bad_parts, HEADPALLET, ideal_cycle_time)" +
    '                                         " VALUES (@machine,@trx_time,@total_parts,@bad_parts,@HEADPALLET,@ideal_cycle_time);", sqliteCnt)

    '                        tempdate = csv(1)
    '                        temptime = csv(2)
    '                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

    '                        cmd.Parameters.Add(New SQLiteParameter("@machine", csv(0)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@trx_time", temptime))
    '                        cmd.Parameters.Add(New SQLiteParameter("@total_parts", csv(3)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@bad_parts", csv(4)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(5)))
    '                        cmd.Parameters.Add(New SQLiteParameter("@ideal_cycle_time", csv(6)))

    '                        cmd.ExecuteNonQuery() 'async doesnt wait for finish


    '                    Catch ex As Exception
    '                        LogClientError("Error parsing partnumber csv file:" + ex.Message)
    '                    End Try
    '                End While

    '            End Using

    '        Catch ex As Exception
    '            LogClientError("Error uploading partnumber csv file:" + ex.Message)
    '        End Try

    '        beginTransac(False)
    '        Thread.CurrentThread.CurrentCulture = originalCulture

    '    Catch ex As Exception
    '        LogClientError("unable to complete PARTNUMBER Table creation:" + ex.Message)
    '    End Try
    'End Sub

#End Region ' not used, keeped as backup


#Region "first update SQLITE with previous years"

    Public Sub FirstUpdateDB_SQLite(year__ As String)
        Try
            Dim sqliteCnt As New SQLiteConnection
            sqliteCnt = New SQLiteConnection(sqlitedbpath)
            sqliteCnt.Open()

            Dim sqlite_version As String = "CREATE TABLE if not exists tbl_CSIFLEX_VERSION (version integer PRIMARY KEY);"
            Dim cmdCreateDeviceTable_version As New SQLiteCommand(sqlite_version, sqliteCnt)
            cmdCreateDeviceTable_version.ExecuteNonQuery()

            Dim sqlite3 As String = "CREATE TABLE if not exists tbl_colors (statut VARCHAR(255) PRIMARY KEY, color TEXT);"
            Dim cmdCreateDeviceTable3 As New SQLiteCommand(sqlite3, sqliteCnt)
            cmdCreateDeviceTable3.ExecuteNonQuery()

            Dim sqlite8 As String = "CREATE TABLE if not exists tbl_renamemachines (table_name VARCHAR(255) PRIMARY KEY, original_name VARCHAR(255));"
            Dim cmdCreateDeviceTable8 As New SQLiteCommand(sqlite8, sqliteCnt)
            cmdCreateDeviceTable8.ExecuteNonQuery()

            Dim sqlite9 As String = "CREATE TABLE if not exists tbl_userconfig (name VARCHAR(255) PRIMARY KEY, state TEXT);"
            Dim cmdCreateDeviceTable9 As New SQLiteCommand(sqlite9, sqliteCnt)
            cmdCreateDeviceTable9.ExecuteNonQuery()

            Dim sqlite10 As String = "CREATE TABLE if not exists tbl_Groups (users VARCHAR(255), groups VARCHAR(255), machines VARCHAR(255), CONSTRAINT pk_GM PRIMARY KEY (users,groups,machines));"
            Dim cmdCreateDeviceTable10 As New SQLiteCommand(sqlite10, sqliteCnt)
            cmdCreateDeviceTable10.ExecuteNonQuery()

            sqlite3 = "insert or ignore into tbl_colors (statut, color) VALUES('NOT CONNECTED','#ABABAB');"
            cmdCreateDeviceTable3 = New SQLiteCommand(sqlite3, sqliteCnt)
            cmdCreateDeviceTable3.ExecuteNonQuery()

            sqlite3 = "insert or ignore into tbl_CSIFLEX_VERSION (version) VALUES('" & CSI_DATA.CSIFLEX_VERSION.ToString() & "');"
            cmdCreateDeviceTable3 = New SQLiteCommand(sqlite3, sqliteCnt)
            cmdCreateDeviceTable3.ExecuteNonQuery()


            sqliteCnt.Close()
        Catch ex As Exception
            LogClientError("Error configuring the database:" + ex.Message)
        End Try



        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim machcsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_MACHINE_" + DateTime.Now.Year.ToString()) And
                    System.IO.Path.GetExtension(File) = ".CSV" Then
                    If System.IO.Path.GetFileName(File).Count = 17 Then
                        machcsvfile = File
                    End If
                End If
            Next

            machcsvfile = eNET_path() & "\_REPORTS\" + "_MACHINE_" + year__ + ".CSV"

            'Dim dt As DataTable = New DataTable

            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                For Each match In File.ReadLines(machcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                'LogServerError("Unable to find start line:" + ex.Message)
                LogClientError("Unable to find start line:" + ex.Message)
            End Try


            ' beginSqliteTransac(True)
            startline = -1
            Dim machlist As New List(Of String)

            '  Dim part_perf As New Dictionary(Of String, Integer) ' status, cycletime
            Dim part_comment As String = ""
            'open the file "data.csv" which is a CSV file with headers
            Using csv As New CsvReader(New StreamReader(machcsvfile), True, ","c)

                Dim fieldCount As Integer = csv.FieldCount
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                Dim headers As String() = csv.GetFieldHeaders()

                csv.MoveTo(startline - 1)


                Dim sqliteConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)
                sqliteConn.Open()

                Dim sqlitecmd As New SQLiteCommand("BEGIN TRANSACTION;", sqliteConn)
                sqlitecmd.ExecuteNonQuery()

                Dim tempdate As New DateTime()
                Dim temptime As New DateTime()
                Dim status As String
                Dim comment_ As String = ""
                Dim perf_ As part_perf = New part_perf


                While (csv.ReadNextRecord())

                    Try
                        'For Each row As DataRow In result
                        '    resultTable.ImportRow(row)
                        'Next
                        Dim renamedmachine As String = RenameMachine(csv(0))


                        If Not (machlist.Contains(csv(0))) Then
                            machlist.Add(csv(0))


                            sqlitecmd = New SQLiteCommand("insert or ignore into tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqliteConn)
                            sqlitecmd.ExecuteNonQuery()

                            sqlitecmd = New SQLiteCommand("CREATE TABLE if not exists tbl_" & renamedmachine & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, Partnumber varchar(255), UNIQUE (time_,status))", sqliteConn)
                            sqlitecmd.ExecuteNonQuery()



                        End If

                        'csv line example
                        'GANESH,07/23/2015,2,23:28:03,01:30:00,7317,CYCLE ON,0,
                        sqlitecmd = New SQLiteCommand("Replace into tbl_" & renamedmachine +
                                         "(month_, day_, year_, time_, Date_, status, shift, cycletime, Partnumber)" +
                                         "VALUES (@month,@day,@year,@time,@date,@status,@shift,@cycletime,@Partnumber);", sqliteConn)
                        tempdate = csv(1) 'row("DATE")


                        sqlitecmd.Parameters.Add(New SQLiteParameter("@month", tempdate.Month))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@day", tempdate.Day))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@year", tempdate.Year))
                        temptime = csv(3) 'row("START TIME")
                        temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@time", temptime))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@date", temptime))

                        '
                        If csv(6) = "_PARTNUMBER" Then
                            status = "_PARTNO:" & csv(8).Split(";")(0)
                            part_comment = csv(8)
                        Else


                            status = csv(6)
                        End If

                        sqlitecmd.Parameters.Add(New SQLiteParameter("@status", status))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@shift", csv(2))) 'row("SHIFT")))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@cycletime", csv(5))) 'row("ELAPSED TIME")))
                        sqlitecmd.Parameters.Add(New SQLiteParameter("@Partnumber", part_comment))
                        sqlitecmd.ExecuteNonQuery() 'async doesnt wait for finish



                    Catch ex As Exception
                        LogClientError("Error parsing csv file:" + ex.Message)
                    End Try
                End While

                sqlitecmd = New SQLiteCommand("COMMIT TRANSACTION;", sqliteConn)
                sqlitecmd.ExecuteNonQuery()

                sqliteConn.Close()

            End Using

            'beginSqliteTransac(False)

            Thread.CurrentThread.CurrentCulture = originalCulture

            FirstOEEUpdate_SQLite(year__)
            FirstOperatorUpdate_SQLite(year__)
            FirstPartnumberUpdate_SQLite(year__)
            FirstReportedpartUpdate_SQLite(year__)



        Catch ex As Exception
            LogClientError("unable to complete database creation:" + ex.Message)
        End Try

    End Sub

    Private Sub FirstOEEUpdate_SQLite(year__ As String)

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
            Dim oeecsvfile As String = ""
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_OEE_" + year__) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    oeecsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                For Each match In File.ReadLines(oeecsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogClientError("Unable to find start line:" + ex.Message)
            End Try

            startline = -1
            Dim cmd As New SQLiteCommand

            Try

                Dim sqliteCnt As New SQLiteConnection
                sqliteCnt = New SQLiteConnection(sqlitedbpath)
                sqliteCnt.Open()

                Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_oee (`MACHINE` varchar(75) NOT NULL,  `trx_time` datetime NOT NULL, `Avail` float NOT NULL, `Performance` float NOT NULL, `Quality` float NOT NULL, `OEE` float NOT NULL,`HEADPALLET` int);"

                cmd = New SQLiteCommand(cmdstr, sqliteCnt)

                Dim sqlitecmd As New SQLiteCommand("BEGIN TRANSACTION;", sqliteCnt)
                sqlitecmd.ExecuteNonQuery()

                cmd.ExecuteNonQuery() 'async doesnt wait for finish

                Dim machlist As New List(Of String)

                'open the file "data.csv" which is a CSV file with headers
                Using csv As New CsvReader(New StreamReader(oeecsvfile), True, ","c)

                    Dim fieldCount As Integer = csv.FieldCount
                    csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                    csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                    Dim headers As String() = csv.GetFieldHeaders()

                    csv.MoveTo(startline - 1)

                    'Dim cmd As New MySqlCommand
                    Dim tempdate As New DateTime()
                    Dim temptime As New DateTime()
                    'Dim status As String

                    While (csv.ReadNextRecord())

                        Try
                            Dim renamedmachine As String = RenameMachine(csv(0))

                            If Not (machlist.Contains(csv(0))) Then
                                machlist.Add(csv(0))

                                cmd = New SQLiteCommand("insert or ignore into tbl_renamemachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqliteCnt)
                                cmd.ExecuteNonQuery()
                            End If

                            'csv line example
                            'OKUMA,09/24/2016,13:25:52,0.05,85.71,83.33,0.04,0
                            cmd = New SQLiteCommand("Replace into tbl_oee" +
                                             " (MACHINE, trx_time, Avail, Performance, Quality, OEE, HEADPALLET)" +
                                             " VALUES (@MACHINE,@trx_time,@Avail,@Performance,@Quality,@OEE,@HEADPALLET);", sqliteCnt)
                            tempdate = csv(1) 'row("DATE")
                            temptime = csv(2) 'row("START TIME")
                            temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

                            cmd.Parameters.Add(New SQLiteParameter("@MACHINE", csv(0)))
                            cmd.Parameters.Add(New SQLiteParameter("@trx_time", temptime))
                            cmd.Parameters.Add(New SQLiteParameter("@Avail", csv(3)))
                            cmd.Parameters.Add(New SQLiteParameter("@Performance", csv(4)))
                            cmd.Parameters.Add(New SQLiteParameter("@Quality", csv(5)))
                            cmd.Parameters.Add(New SQLiteParameter("@OEE", csv(6)))
                            cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(7))) 'row("SHIFT")))

                            cmd.ExecuteNonQuery() 'async doesnt wait for finish


                        Catch ex As Exception
                            LogServerError("Error parsing oee csv file:" + ex.Message, 1)
                        End Try
                    End While

                End Using

                sqlitecmd = New SQLiteCommand("COMMIT TRANSACTION;", sqliteCnt)
                sqlitecmd.ExecuteNonQuery()


            Catch ex As Exception
                LogClientError("Error uploading oee csv file:" + ex.Message)
            End Try


            Thread.CurrentThread.CurrentCulture = originalCulture

        Catch ex As Exception
            LogClientError("unable to complete OEE Table creation:" + ex.Message)
        End Try
    End Sub
    Private Sub FirstOperatorUpdate_SQLite(year__ As String)

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
            Dim opercsvfile As String = ""
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_OPERATOR_" + year__) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    opercsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                For Each match In File.ReadLines(opercsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogClientError("Unable to find start line:" + ex.Message)
            End Try

            startline = -1
            ' beginTransac(True)

            Dim cmd As New SQLiteCommand

            Try
                Dim sqliteCnt As New SQLiteConnection
                sqliteCnt = New SQLiteConnection(sqlitedbpath)
                sqliteCnt.Open()

                Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_operator (`OPERATOR` varchar(150) NOT NULL,  `PARTNO` varchar(255) NOT NULL, `QUANTITY` int NOT NULL, `AVGCYCLETIME` varchar(150) NOT NULL, `GOODCYCLE` int NOT NULL, `AVGGOODCYCLETIME` varchar(150) NOT NULL, `trx_date` date not null, `shift` int NOT NULL, `HEADPALLET` int);"

                cmd = New SQLiteCommand(cmdstr, sqliteCnt)

                Dim sqlitecmd As New SQLiteCommand("BEGIN TRANSACTION;", sqliteCnt)
                sqlitecmd.ExecuteNonQuery()

                cmd.ExecuteNonQuery() 'async doesnt wait for finish

                Dim machlist As New List(Of String)

                'open the file "data.csv" which is a CSV file with headers
                Using csv As New CsvReader(New StreamReader(opercsvfile), True, ","c)

                    Dim fieldCount As Integer = csv.FieldCount
                    csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                    csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                    Dim headers As String() = csv.GetFieldHeaders()

                    csv.MoveTo(startline - 1)

                    ''Dim cmd As New SQLiteCommand
                    Dim tempdate As New DateTime()
                    'Dim temptime As New DateTime()
                    ''Dim status As String

                    While (csv.ReadNextRecord())

                        Try

                            'csv line example
                            '6021,212-100-10,49,00:01:47,0,00:00:00,01/05/2016,1,0
                            cmd = New SQLiteCommand("Replace into tbl_operator" +
                                             " (OPERATOR, PARTNO, QUANTITY, AVGCYCLETIME, GOODCYCLE, AVGGOODCYCLETIME, trx_date, shift, HEADPALLET)" +
                                             " VALUES (@OPERATOR,@PARTNO,@QUANTITY,@AVGCYCLETIME,@GOODCYCLE,@AVGGOODCYCLETIME,@trx_date,@shift,@HEADPALLET);", sqliteCnt)

                            tempdate = csv(6)
                            cmd.Parameters.Add(New SQLiteParameter("@OPERATOR", csv(0)))
                            cmd.Parameters.Add(New SQLiteParameter("@PARTNO", csv(1)))
                            cmd.Parameters.Add(New SQLiteParameter("@QUANTITY", csv(2)))
                            cmd.Parameters.Add(New SQLiteParameter("@AVGCYCLETIME", csv(3)))
                            cmd.Parameters.Add(New SQLiteParameter("@GOODCYCLE", csv(4)))
                            cmd.Parameters.Add(New SQLiteParameter("@AVGGOODCYCLETIME", csv(5)))
                            cmd.Parameters.Add(New SQLiteParameter("@trx_date", tempdate))
                            cmd.Parameters.Add(New SQLiteParameter("@shift", csv(7)))
                            cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(8)))

                            cmd.ExecuteNonQuery() 'async doesnt wait for finish


                        Catch ex As Exception
                            LogClientError("Error parsing operator csv file:" + ex.Message)
                        End Try
                    End While

                End Using


                sqlitecmd = New SQLiteCommand("COMMIT TRANSACTION;", sqliteCnt)
                sqlitecmd.ExecuteNonQuery()

            Catch ex As Exception
                LogClientError("Error uploading operator csv file:" + ex.Message)
            End Try


            Thread.CurrentThread.CurrentCulture = originalCulture

        Catch ex As Exception
            LogClientError("unable to complete OPERATOR Table creation:" + ex.Message)
        End Try
    End Sub
    Private Sub FirstPartnumberUpdate_SQLite(year__ As String)

        Try
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
            Dim partcsvfile As String = ""
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_PARTNUMBER_" + year__) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    partcsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try
                'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                For Each match In File.ReadLines(partcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogClientError("Unable to find start line:" + ex.Message)
            End Try

            startline = -1




            Dim cmd As New SQLiteCommand

            Try
                Dim sqliteCnt As New SQLiteConnection
                sqliteCnt = New SQLiteConnection(sqlitedbpath)
                sqliteCnt.Open()
                'DATE    ,START TIME,END TIME,ELAPSED TIME,MACHINE NAME,HEAD/PALLET,SHIFT,PART NUMBER,TOTAL CYC,GOOD CYC,SHORT CYC,LONG CYC,AVG GOOD CYC TIME,MAX CYC TIME,MIN CYC TIME
                '01/05/2016, 05:00:02, 12:10:07, 25805, 420, 0, 1, 212-100-10, 50, 0, 50, 0, 00:00:00, 00:08:11, 00:00:02
                Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_partnumber (`start_time` datetime NOT NULL,  `end_time` datetime NOT NULL, `elapsed_time` int NOT NULL, `machine` varchar(255) NOT NULL," +
                    " `HEADPALLET` int NOT NULL, `shift` int NOT NULL, `Partno` varchar(255) not null, `total_cycle` int NOT NULL, `good_cycle` int not null, `short_cycle` int not null," +
                    " `long_cycle` int not null, `avg_good_cycle_time` varchar(150) not null, `max_cycle_time` varchar(150) not null, `min_cycle_time` varchar(150) not null ,UNIQUE (end_time,machine));"

                cmd = New SQLiteCommand(cmdstr, sqliteCnt)

                'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

                cmd.ExecuteNonQuery() 'async doesnt wait for finish

                Dim sqlitecmd As New SQLiteCommand("BEGIN TRANSACTION;", sqliteCnt)
                sqlitecmd.ExecuteNonQuery()


                Dim machlist As New List(Of String)

                'open the file "data.csv" which is a CSV file with headers
                Using csv As New CsvReader(New StreamReader(partcsvfile), True, ","c)

                    Dim fieldCount As Integer = csv.FieldCount
                    csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                    csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                    Dim headers As String() = csv.GetFieldHeaders()

                    csv.MoveTo(startline - 1)


                    ''Dim cmd As New SQLiteCommand
                    Dim tempdate As New DateTime()
                    Dim temptime As New DateTime()
                    ''Dim status As String

                    While (csv.ReadNextRecord())

                        Try
                            Dim renamedmachine As String = RenameMachine(csv(4))

                            If Not (machlist.Contains(csv(4))) Then
                                machlist.Add(csv(4))

                                cmd = New SQLiteCommand("insert or ignore into tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(4) & "')", sqliteCnt)
                                cmd.ExecuteNonQuery()
                            End If


                            cmd = New SQLiteCommand("Replace into tbl_partnumber" +
                                             " (start_time, end_time, elapsed_time, machine, HEADPALLET, shift, Partno, total_cycle, good_cycle, short_cycle, long_cycle, avg_good_cycle_time, max_cycle_time, min_cycle_time)" +
                                             " VALUES (@start_time,@end_time,@elapsed_time,@machine,@HEADPALLET,@shift,@Partno,@total_cycle,@good_cycle,@short_cycle,@long_cycle,@avg_good_cycle_time,@max_cycle_time,@min_cycle_time);", sqliteCnt)

                            tempdate = csv(0)
                            temptime = csv(1)
                            temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                            cmd.Parameters.Add(New SQLiteParameter("@start_time", temptime))
                            temptime = csv(2)
                            temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)
                            cmd.Parameters.Add(New SQLiteParameter("@end_time", temptime))
                            cmd.Parameters.Add(New SQLiteParameter("@elapsed_time", csv(3)))
                            cmd.Parameters.Add(New SQLiteParameter("@machine", csv(4)))
                            cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(5)))
                            cmd.Parameters.Add(New SQLiteParameter("@shift", csv(6)))
                            cmd.Parameters.Add(New SQLiteParameter("@Partno", csv(7)))
                            cmd.Parameters.Add(New SQLiteParameter("@total_cycle", csv(8)))
                            cmd.Parameters.Add(New SQLiteParameter("@good_cycle", csv(9)))
                            cmd.Parameters.Add(New SQLiteParameter("@short_cycle", csv(10)))
                            cmd.Parameters.Add(New SQLiteParameter("@long_cycle", csv(11)))
                            cmd.Parameters.Add(New SQLiteParameter("@avg_good_cycle_time", csv(12)))
                            cmd.Parameters.Add(New SQLiteParameter("@max_cycle_time", csv(13)))
                            cmd.Parameters.Add(New SQLiteParameter("@min_cycle_time", csv(14)))


                            cmd.ExecuteNonQuery() 'async doesnt wait for finish


                        Catch ex As Exception
                            LogClientError("Error parsing partnumber csv file:" + ex.Message)
                        End Try
                    End While

                End Using

                cmd = New SQLiteCommand("COMMIT TRANSACTION;", sqliteCnt)
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                LogClientError("Error uploading partnumber csv file:" + ex.Message)
            End Try

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

        Catch ex As Exception
            LogClientError("unable to complete PARTNUMBER Table creation:" + ex.Message)
        End Try
    End Sub
    Private Sub FirstReportedpartUpdate_SQLite(year__ As String)
        Try
            Dim sqliteCnt As New SQLiteConnection
            Dim files As String() = System.IO.Directory.GetFiles(eNET_path() & "\_REPORTS\")
            Dim partcsvfile As String = ""
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_REPORTEDPARTS_" + year__) And System.IO.Path.GetExtension(File) = ".CSV" Then
                    partcsvfile = File
                End If
            Next


            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")


            'Dim totalrow As Long = File.ReadLines(machcsvfile).Count()
            Dim startline As Long = 0

            Try

                sqliteCnt = New SQLiteConnection(sqlitedbpath)
                sqliteCnt.Open()
                'Dim firstdate As String = DateTime.Now.AddMonths(-3).Month.ToString() + "/" + DateTime.Now.AddMonths(-3).Day.ToString() + "/" + DateTime.Now.AddMonths(-3).Year.ToString()
                Dim firstdate As String = CreateDateStr(DateTime.Now.AddMonths(-3))

                For Each match In File.ReadLines(partcsvfile).Select(Function(text, index) New With {text, Key .lineNumber = index + 1}).Where(Function(x) x.text.Contains(firstdate))
                    'Console.WriteLine("{0}: {1}", match.lineNumber, match.text)
                    startline = match.lineNumber - 1
                    Exit For
                Next
            Catch ex As Exception
                LogClientError("Unable to find start line:" + ex.Message)
            End Try

            startline = -1
            'beginTransac(True)

            Dim cmd As New SQLiteCommand

            Try
                'MACHINE NAME, Date, TIME, TOTAL PARTS, BAD PARTS, HEAD / PALLET, IDEAL CYCLETIME
                'OKUMA, 09 / 24 / 2016, 09: 59:29,3,8,0,1
                Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_reportedparts (`machine` varchar(255) NOT NULL,  `trx_time` datetime NOT NULL, `total_parts` int NOT NULL, `bad_parts` int NOT NULL," +
                    " `HEADPALLET` int NOT NULL, `ideal_cycle_time` int NOT NULL);"

                cmd = New SQLiteCommand(cmdstr, sqliteCnt)

                'cmd.Parameters.Add(New MySqlParameter("@file", oeecsvfile))

                cmd.ExecuteNonQuery() 'async doesnt wait for finish


                Dim sqlitecmd As New SQLiteCommand("BEGIN TRANSACTION;", sqliteCnt)
                sqlitecmd.ExecuteNonQuery()


                Dim machlist As New List(Of String)

                'open the file "data.csv" which is a CSV file with headers
                Using csv As New CsvReader(New StreamReader(partcsvfile), True, ","c)

                    Dim fieldCount As Integer = csv.FieldCount
                    csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty
                    csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine

                    Dim headers As String() = csv.GetFieldHeaders()

                    csv.MoveTo(startline - 1)

                    ''Dim cmd As New SQLiteCommand
                    Dim tempdate As New DateTime()
                    Dim temptime As New DateTime()
                    ''Dim status As String

                    While (csv.ReadNextRecord())

                        Try
                            Dim renamedmachine As String = RenameMachine(csv(0))

                            If Not (machlist.Contains(csv(0))) Then
                                machlist.Add(csv(0))

                                'cmd = New SQLiteCommand("insert ignore into tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqliteCnt)
                                'cmd.ExecuteNonQuery()

                                cmd = New SQLiteCommand("insert or ignore into tbl_renameMachines  (table_name, original_name) VALUES('" & renamedmachine & "' , '" & csv(0) & "')", sqliteCnt)
                                cmd.ExecuteNonQuery()
                            End If

                            'Dim cmdstr = "CREATE TABLE IF NOT EXISTS tbl_reportedparts (`machine` varchar(255) NOT NULL,  `trx_time` datetime NOT NULL, `total_parts` int NOT NULL, `bad_parts` int NOT NULL," +
                            '" `HEADPALLET` int NOT NULL, `ideal_cycle_time` int NOT NULL);"

                            cmd = New SQLiteCommand("Replace into tbl_reportedparts" +
                                             " (machine, trx_time, total_parts, bad_parts, HEADPALLET, ideal_cycle_time)" +
                                             " VALUES (@machine,@trx_time,@total_parts,@bad_parts,@HEADPALLET,@ideal_cycle_time);", sqliteCnt)

                            tempdate = csv(1)
                            temptime = csv(2)
                            temptime = New DateTime(tempdate.Year, tempdate.Month, tempdate.Day, temptime.Hour, temptime.Minute, temptime.Second)

                            cmd.Parameters.Add(New SQLiteParameter("@machine", csv(0)))
                            cmd.Parameters.Add(New SQLiteParameter("@trx_time", temptime))
                            cmd.Parameters.Add(New SQLiteParameter("@total_parts", csv(3)))
                            cmd.Parameters.Add(New SQLiteParameter("@bad_parts", csv(4)))
                            cmd.Parameters.Add(New SQLiteParameter("@HEADPALLET", csv(5)))
                            cmd.Parameters.Add(New SQLiteParameter("@ideal_cycle_time", csv(6)))

                            cmd.ExecuteNonQuery() 'async doesnt wait for finish


                        Catch ex As Exception
                            LogClientError("Error parsing partnumber csv file:" + ex.Message)
                        End Try
                    End While

                End Using


                sqlitecmd = New SQLiteCommand("COMMIT TRANSACTION;", sqliteCnt)
                sqlitecmd.ExecuteNonQuery()

            Catch ex As Exception
                LogClientError("Error uploading partnumber csv file:" + ex.Message)
            End Try

            beginTransac(False)
            Thread.CurrentThread.CurrentCulture = originalCulture

        Catch ex As Exception
            LogClientError("unable to complete PARTNUMBER Table creation:" + ex.Message)
        End Try
    End Sub


#End Region







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


        Dim db_authPath As String = Nothing
        Dim directory As String = getRootPath()

        If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
            Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                db_authPath = reader.ReadLine()
            End Using
        End If

        Dim connectionString As String


        Dim server = db_authPath

        license = CheckLic(2)

        Try
            If (license = 3) Then
                connectionString = "SERVER=" + server + ";" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"
            Else
                connectionString = MySqlConnectionString + "DATABASE=CSI_database;"  '";" '+ "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"
            End If

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
            LogServerError("Error in beginTransaction:" + ex.ToString(), 1)
        End Try

    End Sub



    Private Sub MysqlCommit()
        Dim db_authPath As String = Nothing
        Dim directory As String = getRootPath()
        If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
            Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                db_authPath = reader.ReadLine()
            End Using
        End If

        Dim connectionString As String


        Dim server = db_authPath


        If (license = 3) Then
            connectionString = "SERVER=" + server + ";" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"
        Else
            connectionString = "SERVER=" + server + ";" + MySqlConnectionString + "DATABASE=CSI_database;"  '";" '+ "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"
        End If

        Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)
            sqlConn.Open()

            Dim cmd As New MySqlCommand("COMMIT;", sqlConn)
            cmd.ExecuteNonQuery()

            sqlConn.Close()
        End Using
    End Sub

    Private Sub SetSqliteAutoCommit(auto As Boolean)

        Try
            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)
                sqlConn.Open()

                Dim cmd As New SQLiteCommand

                If (auto) Then
                    'cmd = New SQLiteCommand("SET autocommit=0;COMMIT;", sqlConn)
                    cmd = New SQLiteCommand("COMMIT TRANSACTION;", sqlConn)
                    cmd.ExecuteNonQuery()
                Else
                    'cmd = New SQLiteCommand("SET autocommit=1;COMMIT;", sqlConn)
                    cmd = New SQLiteCommand("BEGIN TRANSACTION;", sqlConn)
                    cmd.ExecuteNonQuery()
                End If

                sqlConn.Close()
            End Using
        Catch ex As Exception
            LogClientError("Unable to start or close sqlite transaction" + ex.Message)
        End Try

    End Sub

    Private Sub SqliteCommit()
        Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)
            sqlConn.Open()

            Dim cmd As New SQLiteCommand("COMMIT;", sqlConn)
            cmd.ExecuteNonQuery()

            sqlConn.Close()
        End Using
    End Sub


    Public Function ShiftTableQuery(use_mysql As Boolean, renamedmachine As String, Optional startdate As DateTime = Nothing, Optional enddate As DateTime = Nothing) As String
        Dim query As String
        query = "Select year_,month_,day_,shift,status,sum(cycletime) as cycletime"



        If use_mysql Then
            query += " from CSI_Database.tbl_" + renamedmachine
        Else
            query += " from [tbl_" + renamedmachine + "]"
        End If

        If Not IsNothing(startdate) And Not IsNothing(enddate) Then
            query += " where Date_ between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.AddDays(1).ToString("yyyy-MM-dd") + "'"
        End If

        query += " group by year_,month_,day_,shift,status"

        Return query

    End Function



    Private Sub fragmentCSV(csvtable As DataTable, cpt As Integer)

        Dim dt As DataTable = csvtable.Clone
        For i = 0 + (cpt * 500000) To 500000 * (cpt + 1)
            Try
                dt.ImportRow(csvtable.Rows(i))
            Catch ex As Exception
                i = 500000 * (cpt + 1)
            End Try
        Next
        Dim sw As New StreamWriter(eNET_path() & "\_REPORTS\" & "_MACHINE_" + cpt.ToString() + ".CSV", False, UTF8Encoding.[Default])
        Dim a As Double = dt.Rows.Count
        Dim columnNames As IEnumerable(Of String) = csvtable.Columns.Cast(Of DataColumn)().[Select](Function(column) column.ColumnName)


        sw.WriteLine(String.Join(",", columnNames))
        For Each row As DataRow In dt.Rows
            Dim fields As IEnumerable(Of String) = row.ItemArray.[Select](Function(field) String.Concat("""", field.ToString().Replace("""", """"""), """"))
            sw.WriteLine(String.Join(",", fields))
        Next
        sw.Close()
        sw.Dispose()
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

#End Region


    Public Function read_version_clt() As String
        Using reader As StreamReader = New StreamReader(ClientRootPath & "\sys\CSIFLEX_VERSION.csys", False)
            Return reader.ReadLine()
        End Using
    End Function


#Region "reporting"


#Region "variable reporting"

    Private Qry_Tbl_MachineName As String
    Private Qry_Tbl_RenameMachine As String
    Private TypeDePeriode As String
    Public OutputReportPath As String
    'Private dateReporting As DateTime
    Private reportstartdate As DateTime
    Private reportenddate As DateTime
    Private statusName_ As String = "SETUP"
    Private StatusNameArray As String(,)
    Private machineIndex As Integer
    'Private isEventReport As Boolean
#End Region

    Sub generateSqlQuery(machineList As String())

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

    Public Sub LicenseAffectation()

        If isLicenseAffect = False Then
            license = CheckLic(2)
            isLicenseAffect = True
        End If

    End Sub
    Public Sub DateAffectation(ReportStartDate_ As DateTime, ReportEndDate_ As DateTime)
        'dateReporting = ReportDate
        reportstartdate = ReportStartDate_
        reportenddate = ReportEndDate_
    End Sub

    Public Function generateReport(machineList As String(), TypeDeRapport As String, ReportStartDate As DateTime, ReportEndDate As DateTime, outputreport As String, StatusName As String(,), isSetup As Boolean, Optional taskname As String = "default") As String


        Dim completePath As String = ""
        Try
            LicenseAffectation()
            StatusNameArray = StatusName
            OutputReportPath = outputreport

            Dim applicationPath As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
            Dim path As String = getRootPath()

            If Not Directory.Exists(path & "\reports_templates") Then
                IO.File.WriteAllBytes(path & "\reports_templates.zip", My.Resources.reports_templates)

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
            LogServiceError("Error while generating a report : " + ex.Message + vbCrLf + "___>" + ex.StackTrace, 1)
            If ex.Message.Contains("ReportViewer") Then WriteWarningReportViewer()
        End Try

        Return completePath
    End Function

    Private Sub WriteWarningReportViewer()
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
    Private Function saveReport(viewer As ReportViewer, typeDeRap As String, task As String) As String
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
            Forms.MessageBox.Show("A file with the same name is already opened, please close:" + System.Environment.NewLine + "- report " + typeDeRap + "_" + DateTime.Now.ToString("MMMddyyyy", CultureInfo.CreateSpecificCulture("en-US")) + DateTime.Now.TimeOfDay.ToString() + ".pdf")

        End While
        Try
            'fs = New FileStream(OutputReportPath + "/ " + task + " " + typeDeRap + "_" + dateReporting.ToString()"MMMddyyyy", CultureInfo.CreateSpecificCulture("en-US")) + " " + DateTime.Now.TimeOfDay.ToString().Substring(0, DateTime.Now.TimeOfDay.ToString().IndexOf(".")).Replace(":", "-") + ".pdf", FileMode.Create)
            fs = New FileStream(OutputReportPath + "/ " + task + " " + typeDeRap + "_" + DateTime.Now.ToString("MMMddyyyy", CultureInfo.CreateSpecificCulture("en-US")) + " " + DateTime.Now.TimeOfDay.ToString().Substring(0, DateTime.Now.TimeOfDay.ToString().IndexOf(".")).Replace(":", "-") + ".pdf", FileMode.Create)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
        Catch ex As Exception
            LogServiceError("Error saving report : " + ex.Message, 1)
        End Try

        Return fs.Name
    End Function

    Public Function IntToMonday(integ As Integer, param As DateTime) As DateTime
        Dim ret As DateTime

        ret = DateAdd("ww", integ - 1, DateSerial(Year(param), 1, 1))

        Return toMonday(ret)

    End Function

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

    Private Sub ReloadReport(viewer As ReportViewer)
        If (viewer.LocalReport.DataSources.Count > 0) Then
            viewer.LocalReport.DataSources.RemoveAt(0)
        End If

        viewer.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", setMachineName()))
        AddHandler viewer.LocalReport.SubreportProcessing, AddressOf localReport_SubreportProcessing

    End Sub

    Private Sub localReport_SubreportProcessing(sender As Object, e As SubreportProcessingEventArgs)

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
                LogServiceError(ex.ToString(), 1)
            End Try


            'e.DataSources.Add(New ReportDataSource("DataSet_data", setMachineData(True, TypeDePeriode, machine)))

            Try
                'e.DataSources.Add(New ReportDataSource("DataSet_shiftBarChart", setShiftBarChart(machine, TypeDePeriode)))
                e.DataSources.Add(New ReportDataSource("DataSet_shiftBarChart", setShiftBarChart(machine)))
            Catch ex As Exception
                LogServiceError(ex.ToString(), 1)
            End Try

            If (TypeDePeriode = "ww" Or TypeDePeriode = "m") Then
                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_data4", setMachineData(False, TypeDePeriode, machine)))
                Catch ex As Exception
                    LogServiceError(ex.ToString(), 1)
                End Try
            End If


            Try
                e.DataSources.Add(New ReportDataSource("DataSet_4reasons", set4Reason(True, TypeDePeriode, machine)))
            Catch ex As Exception
                LogServiceError(ex.ToString(), 1)
            End Try


            If (TypeDePeriode = "ww" Or TypeDePeriode = "m") Then

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_4reasons4", set4Reason(False, TypeDePeriode, machine)))
                Catch ex As Exception
                    LogServiceError(ex.ToString(), 1)
                End Try

            End If

            If (TypeDePeriode = "y") Then

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_History", setHistoryDaily(TypeDePeriode, machine)))
                Catch ex As Exception
                    LogServiceError(ex.ToString(), 1)
                End Try

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_timeLine", setTimeLine(machine)))
                Catch ex As Exception
                    LogServiceError(ex.ToString(), 1)
                End Try

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_PartNo", setPartNo(True, TypeDePeriode, machine)))
                Catch ex As Exception
                    LogServiceError(ex.ToString(), 1)
                End Try

            ElseIf (TypeDePeriode = "ww") Then

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_History", setHistoryWeekly(machine)))
                Catch ex As Exception
                    LogServiceError(ex.ToString(), 1)
                End Try

            ElseIf (TypeDePeriode = "m") Then

                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_History", setHistoryMonthly(machine)))
                Catch ex As Exception
                    LogServiceError(ex.ToString(), 1)
                End Try
            ElseIf (TypeDePeriode = "td") Then
                Try
                    e.DataSources.Add(New ReportDataSource("DataSet_History", setHistoryDaily(TypeDePeriode, machine)))
                Catch ex As Exception
                    LogServiceError(ex.ToString(), 1)
                End Try
            End If

        Catch ex As Exception
            LogServiceError(ex.ToString(), 1)
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

    Private Function setShiftBarChart(tblmachineName As String) As DataTable


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
                Forms.MessageBox.Show("Unable to generate bar chart")
            End Try
        End If

    End Function

    Private Function setTimeLine(tblmachineName As String) As DataTable

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
                Forms.MessageBox.Show("Unable to set timeline")
            End Try
        End If
    End Function

    Private Function setMachineData(OnePeriod As Boolean, PeriodType As String, tblmachineName As String) As DataTable
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

    Private Function setPartNo(OnePeriod As Boolean, PeriodType As String, tblmachineName As String) As DataTable
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

    Private Function getPartNumber() As DataTable

        If license = 1 Or license = 2 Then 'If CSI_Lib.isClientSQlite Then
            Using sqlConn As SQLiteConnection = New SQLiteConnection(sqlitedbpath)

                Dim adap As SQLiteDataAdapter = New SQLiteDataAdapter(
                   "SELECT DISTINCT MchName FROM ( " + Qry_Tbl_MachineName + " ) tablee where (status NOT LIKE '_PARTN*') group by MchName ", sqlConn)


                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, "tbl_MachineName")

                Return ds.Tbl_MachineName

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

                Dim adap As MySqlDataAdapter = New MySqlDataAdapter(
                   "SELECT DISTINCT MchName FROM ( " + Qry_Tbl_MachineName + " ) tablee where (status NOT LIKE '_PARTN*') group by MchName ", sqlConn)


                Dim ds As DatasetReporting2 = New DatasetReporting2()

                Dim returnvalue As Integer = adap.Fill(ds, "tbl_MachineName")

                Return ds.Tbl_MachineName

            End Using
        End If

    End Function

    Private Function setMachineName() As DataTable

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

    Private Function setHistoryDaily(PeriodType As String, tblmachineName As String) As DataTable

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

    Public Function IntToFirst(integ As Integer, param As DateTime) As DateTime

        Dim ret As DateTime = New DateTime(Year(param), integ, 1)
        Return ret

    End Function

    Private Function setHistoryWeekly(tblmachineName As String) As DataTable
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

    Private Function setHistoryMonthly(tblmachineName As String) As DataTable
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

    Public Sub write_delays(MCH As MCH_) ' STATUS As String) ', dic As Dictionary(Of String, Integer))
        Using wrtr As StreamWriter = New StreamWriter(serverRootPath & "\sys\Conditions\" + MCH.MachineName + "\delays.csys")
            For Each KEY In MCH.delays
                wrtr.WriteLine(KEY + ":" + MCH.delays(KEY))
            Next
            wrtr.Close()
        End Using
    End Sub
    Public Sub read_delays(ByRef MCH As MCH_) ' STATUS As String) ', dic As Dictionary(Of String, Integer))
        If File.Exists((serverRootPath & "\sys\Conditions\" + MCH.MachineName + "\delays.csys")) Then

            Using wrdr As StreamReader = New StreamReader(serverRootPath & "\sys\Conditions\" + MCH.MachineName + "\delays.csys")
                While (Not wrdr.EndOfStream)
                    Dim str() As String = wrdr.ReadLine().Split(":")
                    MCH.delays(str(0)) = str(1)
                End While

                wrdr.Close()
            End Using
        End If
    End Sub


    Public Sub readsetup_for_adv_mtc()
        Dim MachineName As String = ""
        Try
            Dim line() As String
            Dim line_t As String

            If Not Directory.Exists(serverRootPath & "\sys\Conditions\") Then
                Directory.CreateDirectory(serverRootPath & "\sys\Conditions\")
            End If

            Dim di As New DirectoryInfo(serverRootPath & "\sys\Conditions\")
            Dim fiArr As DirectoryInfo() = di.GetDirectories


            For Each mch In MCHS_.ToList
                If Not Directory.Exists(serverRootPath & "\sys\Conditions\" & mch.Key) Then MCHS_.Remove(mch.Key)
            Next
            ':::::::::::::::::::::::::::::::::::::::::NEW CODE:::::::::::::::::::::::::::::::::::::::::::::
            Dim cntsql As New MySqlConnection(CSI_Library.MySqlConnectionString)
            cntsql.Open()
            Dim SelectAllcsiconnector As String = "SELECT * from csi_auth.tbl_csiconnector ;"
            Dim cmdSelectAllcsiconnector As New MySqlCommand(SelectAllcsiconnector, cntsql)
            Dim mysqlReaderSelectAllcsiconnector As MySqlDataReader = cmdSelectAllcsiconnector.ExecuteReader()
            Dim dTable_SelectAllcsiconnector As New DataTable()
            dTable_SelectAllcsiconnector.Load(mysqlReaderSelectAllcsiconnector)
            Dim NoOfMachines = dTable_SelectAllcsiconnector.Rows.Count
            If dTable_SelectAllcsiconnector.Rows.Count > 0 Then
                '""""""""""""Write HERE """"""""""""""""""""""""""""""'
            End If
            cntsql.Close()
            'Add Here New Code o Get (Read Values for All Parameters from Database) ::: Edited by BDesai
            For Each fri In fiArr

                MachineName = fri.Name 'Folder's Name is Machine Name 
                Dim ip_addr As String = "" ' get from database 
                'If machine is Focas then Take Ex: http://<IPAddress>:<Port>/ and for mtconnect directly take from DB check from csi_auth.tbl_csiconnector if this table is emty means we don't have any MTConnect or Focas Machines  
                If File.Exists((serverRootPath & "\sys\Conditions\" & MachineName & "\" & "ip.csys")) Then
                    Using ipR As StreamReader = New StreamReader(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "ip.csys")
                        ip_addr = ipR.ReadLine
                        ' MCHS_.Item(MachineName).IP = ipR.ReadLine
                        ipR.Close()
                    End Using
                Else
                    '  MCHS_.Item(MachineName).IP = ""
                    LogServerError("Could not find  the setup file that countains the ip address of the machine  : " & MachineName, 1)
                End If

                If MCHS_.ContainsKey(MachineName) Then
                Else
                    MCHS_.Add(MachineName, New MCH_(MachineName, serverRootPath, ip_addr))
                End If


                read_delays(MCHS_(MachineName))

                If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\Conditions.csys") Then
                    MCHS_.Item(MachineName).Conditions_expression_.Clear()
                    Using reader As StreamReader = New StreamReader(serverRootPath & "\sys\Conditions\" & MachineName & "\Conditions.csys")
                        While Not (reader.EndOfStream)
                            line_t = reader.ReadLine()
                            line = line_t.Split("|")
                            If line.Length > 0 Then
                                MCHS_.Item(MachineName).Conditions_expression_.Add(line(0), line(1))
                            Else
                                LogServerError("Not reading a correct Conditions expression : " & line_t & " for machine " & MachineName, 1)
                            End If
                        End While
                        reader.Close()
                    End Using
                End If

                Dim line_ As String
                If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\values_.csys") Then
                    MCHS_.Item(MachineName).DoNotUpdate = True
                    Current_selected_.Clear()
                    ' MCHS_.Item(MachineName).Current_selected_.Clear()
                    Using reader As StreamReader = New StreamReader(serverRootPath & "\sys\Conditions\" & MachineName & "\values_.csys")
                        While Not (reader.EndOfStream)
                            line_ = reader.ReadLine()
                            If line.Length > 0 Then
                                Dim found As Boolean = False
                                For Each item In MCHS_.Item(MachineName).Current_selected_.ToList
                                    If item.StartsWith(line_) Then
                                        found = True
                                    End If
                                Next

                                If found = False Then MCHS_.Item(MachineName).Current_selected_.Add(line_)
                            End If
                        End While
                        reader.Close()
                    End Using
                    MCHS_.Item(MachineName).DoNotUpdate = False
                End If

                If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "partno.csys") Then
                    Using partnoR As StreamReader = New StreamReader(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "partno.csys")

                        MCHS_.Item(MachineName).partno = partnoR.ReadLine()
                        MCHS_.Item(MachineName).PTN_startwith = partnoR.ReadLine()
                        MCHS_.Item(MachineName).PTN_endwith = partnoR.ReadLine()
                        partnoR.Close()
                    End Using
                End If

                If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "feedRO.csys") Then
                    Using feedRO As StreamReader = New StreamReader(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "feedRO.csys")

                        MCHS_.Item(MachineName).frov = feedRO.ReadLine()
                        feedRO.Close()
                    End Using
                End If

                If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "spOv.csys") Then
                    Using spov As StreamReader = New StreamReader(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "spOv.csys")

                        MCHS_.Item(MachineName).spov = spov.ReadLine()
                        spov.Close()
                    End Using
                End If

                If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "Progno.csys") Then
                    Using prognoR As StreamReader = New StreamReader(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "Progno.csys")
                        MCHS_.Item(MachineName).progno = prognoR.ReadLine
                        prognoR.Close()
                    End Using
                End If



                'Notifications setup
                MCHS_.Item(MachineName).DATA_SET_Notifications.Tables.Clear()

                If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "notif_stat.xml") Then
                    Dim xmlFile As XmlReader
                    xmlFile = XmlReader.Create(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "notif_stat.xml")


                    Dim tbl_notif_status As New DataTable("notif_stat")
                    tbl_notif_status.Columns.Add("Activate")
                    tbl_notif_status.Columns.Add("Status")
                    tbl_notif_status.Columns.Add("Delay")
                    tbl_notif_status.ReadXml(xmlFile)
                    xmlFile.Close()
                    MCHS_.Item(MachineName).DATA_SET_Notifications.Tables.Add(tbl_notif_status)
                End If

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
                    MCHS_.Item(MachineName).DATA_SET_Notifications.Tables.Add(tbl_notif_COND)
                End If


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
                        MCHS_.Item(MachineName).sendtolist = t
                    End Using
                End If

            Next fri

        Catch ex As Exception
            MCHS_.Item(MachineName).DoNotUpdate = False
            LogServerError("Could not read mtc setup" & ex.Message, 1)
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

        'Dim indexs_during As Dictionary(Of Integer, I_V)
        'If expression.Contains("during") Then


        '    Dim times__ = Regex.Matches(expression, "during")

        '    For Each m In times__
        '        Dim i As Integer = m.index
        '        'Dim indexs_during() As Integer = find_indexs(expression, "during ")
        '        indexs_during.Add(m.index, New I_V(find_indexs(expression, "during"), "during", New Timer, expression))
        '    Next

        'End If
        'If expression.Contains("more than") Then
        '    Dim indexs_more() As Integer = find_indexs(expression, "more than ")
        'End If
        'If expression.Contains("less than") Then
        '    Dim indexs_less() As Integer = find_indexs(expression, "less than ")
        'End If

        Return expression
    End Function
    'will be used later
    Private Function find_indexs(expression As String, what As String) As Integer()
        Dim index_(3) As Integer
        index_(0) = expression.IndexOf(what)
        index_(1) = 0
        index_(2) = 0
        Dim NOTfound As Boolean = True
        'previous "("
        While NOTfound
            If expression(index_(0)) = "(" Then
                index_(1) = index_(0)
                NOTfound = False
            End If

            index_(0) -= 1
        End While
        NOTfound = True
        'next")"
        While NOTfound
            If expression(index_(0)) = ")" Then
                index_(2) = index_(0)
                NOTfound = False
            End If

            index_(0) += 1
        End While

        Return index_
    End Function


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
                        'If element.StartsWith("logic") And element <> "logic = Normal" Then
                        '   
                        'End If
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
            LogServiceError("Err while evaluation logic exression : " & ex.Message & vbCrLf & " " & ex.StackTrace & vbCrLf & "details : logic expression was : " & expression & "for machine : " & MCH__.MachineName & vbCrLf, 1)
        End Try
    End Sub

    'Public Current_stream_XML As String
    'Private Sub current_selected(mch As MCH_)
    '    Try
    '        Dim crt As ISample


    '        Dim m_client As EntityClient = New EntityClient(mch.IP)
    '        '   m_client.RequestTimeout = 2000

    '        Dim Current_stream As DataStream = m_client.Current()
    '        mch.current_stream = Current_stream
    '        mch.devices = m_client.Probe()

    '        If Current_stream IsNot Nothing Then

    '            'partno
    '            crt = Current_stream.GetSample(mch.partno)
    '            If crt Is Nothing Then
    '                Dim Conds() As IDataElement = Current_stream.AllConditions
    '                For Each cond In Conds
    '                    If cond.DataItemID = mch.partno Then
    '                        ' If mch.partno = mch.partno Then mch.update_other("partno", cond.Value)
    '                        mch.update_other("partno", cond.Value)
    '                    End If
    '                Next
    '                Dim Events() As IDataElement = Current_stream.AllEvents
    '                For Each event_ In Events
    '                    If event_.DataItemID = mch.partno Then
    '                        '  If mch.partno = mch.partno Then mch.update_other("partno", event_.Value)
    '                        mch.update_other("partno", event_.Value)
    '                    End If
    '                Next
    '            Else
    '                If Not mch.partno = Nothing Then
    '                    '  If mch.partno = mch.partno Then mch.update_other("partno", crt.Value)
    '                    If mch.partno = mch.partno Then mch.update_other("partno", crt.Value)
    '                End If
    '            End If

    '            'progno 
    '            crt = Current_stream.GetSample(mch.progno)
    '            If crt Is Nothing Then
    '                Dim Conds() As IDataElement = Current_stream.AllConditions
    '                For Each cond In Conds
    '                    If cond.DataItemID = mch.progno Then
    '                        If mch.progno = mch.progno Then mch.update_other("progno", cond.Value)
    '                    End If
    '                Next
    '                Dim Events() As IDataElement = Current_stream.AllEvents
    '                For Each event_ In Events
    '                    If event_.DataItemID = mch.progno Then
    '                        If mch.progno = mch.progno Then mch.update_other("progno", event_.Value)
    '                    End If
    '                Next
    '            Else
    '                If Not mch.progno = Nothing Then
    '                    If mch.progno = mch.progno Then mch.update_other("progno", crt.Value)
    '                End If
    '            End If

    '            'spov
    '            crt = Current_stream.GetSample(mch.spov)
    '            If crt Is Nothing Then
    '                Dim Conds() As IDataElement = Current_stream.AllConditions
    '                For Each cond In Conds
    '                    If cond.DataItemID = mch.spov Then
    '                        If mch.spov = mch.spov Then mch.update_other("spov", cond.Value)
    '                    End If
    '                Next
    '                Dim Events() As IDataElement = Current_stream.AllEvents
    '                For Each event_ In Events
    '                    If event_.DataItemID = mch.spov Then
    '                        If mch.spov = mch.spov Then mch.update_other("spov", event_.Value)
    '                    End If
    '                Next
    '            Else
    '                If Not mch.spov = Nothing Then
    '                    If mch.spov = mch.spov Then mch.update_other("spov", crt.Value)
    '                End If
    '            End If

    '            'frov
    '            crt = Current_stream.GetSample(mch.frov)
    '            If crt Is Nothing Then
    '                Dim Conds() As IDataElement = Current_stream.AllConditions
    '                For Each cond In Conds
    '                    If cond.DataItemID = mch.frov Then
    '                        If mch.frov = mch.frov Then mch.update_other("frov", cond.Value)
    '                    End If
    '                Next
    '                Dim Events() As IDataElement = Current_stream.AllEvents
    '                For Each event_ In Events
    '                    If event_.DataItemID = mch.frov Then
    '                        If mch.frov = mch.frov Then mch.update_other("frov", event_.Value)
    '                    End If
    '                Next
    '            Else
    '                If Not mch.frov = Nothing Then
    '                    If mch.frov = mch.frov Then mch.update_other("frov", crt.Value)
    '                End If
    '            End If
    '        End If

    '        For Each element As String In mch.Current_selected_.ToList

    '            Dim str() As String = element.Split(" = ")
    '            Dim ID = str(0)

    '            If Current_stream IsNot Nothing Then
    '                crt = Current_stream.GetSample(ID)

    '                If crt Is Nothing Then
    '                    Dim Conds() As IDataElement = Current_stream.AllConditions
    '                    For Each cond In Conds
    '                        If cond.DataItemID = ID Then

    '                            Update_current_selected(ID, cond.Value, mch)
    '                        End If
    '                    Next

    '                    Dim Events() As IDataElement = Current_stream.AllEvents
    '                    For Each event_ In Events
    '                        If event_.DataItemID = ID Then

    '                            Update_current_selected(ID, event_.Value, mch)
    '                        End If
    '                    Next

    '                Else
    '                    If Not ID = Nothing Then

    '                        Update_current_selected(ID, crt.Value, mch)
    '                    End If
    '                End If

    '            End If
    '        Next



    '        'notif
    '        If Current_stream IsNot Nothing Then mch.Current_cond = Current_stream.AllConditions

    '    Catch ex As Exception
    '        LogServerError("cannot get current values for mtconnect: " & ex.Message & vbCrLf & " " & ex.StackTrace & vbCrLf, 1)
    '    End Try
    'End Sub

    'Private Sub readsetup()
    '    Try
    '        Dim line() As String
    '        For Each machinename As File.directory In 

    '        If File.Exists((serverRootPath & "\sys\Conditions\" & machinename & "\" & "delay.csys")) Then
    '                Using delayW As StreamReader = New StreamReader(serverRootPath & "\sys\Conditions\" & machinename & "\" & "delay.csys")
    '                    UD_Dalay.Value = delayW.ReadLine
    '                    delayW.Close()
    '                End Using
    '            Else
    '                UD_Dalay.Value = 0
    '            End If


    '            If File.Exists(serverRootPath & "\sys\Conditions\" & machinename & "\" & "grid.xml") Then
    '                Dim xmlFile As XmlReader
    '                xmlFile = XmlReader.Create(serverRootPath & "\sys\Conditions\" & machinename & "\" & "grid.xml")
    '                conditions_array.Clear()
    '                conditions_array.ReadXml(xmlFile)
    '                xmlFile.Close()


    '                Dim index As Integer = 0
    '                If conditions_array.Tables.Contains(CB_status.SelectedItem) Then



    '                    For Each row As DataRow In conditions_array.Tables(CB_status.SelectedItem).Rows
    '                        If row(1) <> "" And row(2) <> "" And row(3) <> "" Then
    '                            DGV_Cond.Rows.Add()

    '                            DGV_Cond.Rows.Item(index).Cells.Item(0).Value = row(0)
    '                            If index = 0 Then
    '                                DGV_Cond.Rows.Item(index).Cells.Item(0).ReadOnly = True
    '                            Else
    '                                DGV_Cond.Rows.Item(index).Cells.Item(0).ReadOnly = False
    '                            End If



    '                            'Dim c As New DataGridViewComboBoxCell

    '                            'c.DisplayMember = row(1)
    '                            If Not DirectCast(DGV_Cond.Columns(1), DataGridViewComboBoxColumn).Items.Contains(row(1)) Then DirectCast(DGV_Cond.Columns(1), DataGridViewComboBoxColumn).Items.Add(row(1))


    '                            DGV_Cond.Rows.Item(index).Cells.Item(1).Value = row(1)

    '                            DGV_Cond.Rows.Item(index).Cells.Item(2).Value = row(2)
    '                            DGV_Cond.Rows.Item(index).Cells.Item(3).Value = row(3)
    '                            DGV_Cond.Rows.Item(index).Cells.Item(4).Value = row(4)
    '                            DGV_Cond.Rows.Item(index).Cells.Item(5).Value = row(5)

    '                            index += 1
    '                        End If
    '                    Next
    '                End If
    '                'DGV_Cond.DataSource = ds.Tables(MachineName)
    '            End If

    '            If File.Exists(serverRootPath & "\sys\Conditions\" & machinename & "\Conditions.csys") Then
    '                Conditions_expression.Clear()
    '                Using reader As StreamReader = New StreamReader(home_path & "\sys\Conditions\" & machinename & "\Conditions.csys")
    '                    While Not (reader.EndOfStream)
    '                        line = reader.ReadLine().Split(":")
    '                        If line.Length > 0 Then Conditions_expression.Add(line(0), line(1))
    '                    End While
    '                    reader.Close()
    '                End Using
    '            End If

    '            If Conditions_expression.Count <> 0 Then
    '                Dim actual_index As Integer = 0
    '                actual_index = CB_status.SelectedIndex
    '                RemoveHandler CB_status.SelectedIndexChanged, AddressOf CB_status_SelectedIndexChanged
    '                CB_status.Items.Clear()

    '                For Each key__ In Conditions_expression.Keys
    '                    CB_status.Items.Add((key__))
    '                Next

    '                CB_status.SelectedIndex = actual_index
    '                AddHandler CB_status.SelectedIndexChanged, AddressOf CB_status_SelectedIndexChanged
    '            End If



    '    Catch ex As Exception
    '        LogServerError(ex.Message, 1)
    '    End Try
    'End Sub
    '    Public Sub Update_current_selected(value_ As String, data As String, MCH__ As String)

    '        For Each item_ As String In Current_selected_
    '            If item_.StartsWith(value_) Then
    '                Current_selected_.RemoveAt(Current_selected_.IndexOf(item_))
    '                Current_selected_.Add(value_ & " = " & data)

    '                GoTo updated
    '            End If
    '        Next
    '        Current_selected_.Add(value_ & " = " & data)

    'updated:
    '    End Sub
    Public Sub Update_current_selected(value_ As String, data As String, MCH__ As MCH_)
        Dim Buffer_list As List(Of String) = New List(Of String)

        Buffer_list = MCH__.Current_selected_.ToList()

        If value_ = "partno" Or value_ = "spov" Or value_ = "frov" Then
            MCH__.update_other(value_, data)
        Else
            If MCH__.DoNotUpdate = False Then
                For Each item_ As String In Buffer_list.ToList
                    If item_.StartsWith(value_) Then
                        Buffer_list.RemoveAt(Buffer_list.IndexOf(item_))
                        Buffer_list.Add(value_ & " = " & data)
                        GoTo updated
                    End If
                Next
                Buffer_list.Add(value_ & " = " & data)
            End If
updated:
            MCH__.Current_selected_ = Buffer_list
        End If
    End Sub

    Public Sub update_values_files(name As String)

        If File.Exists(serverRootPath & "\sys\Conditions\" & name & "\values_.csys") Then File.Delete(serverRootPath & "\sys\Conditions\" & name & "\values_.csys")
        Using writer As StreamWriter = New StreamWriter(serverRootPath & "\sys\Conditions\" & name & "\values_.csys")
            For Each expression In Current_selected_
                writer.WriteLine(expression)
            Next
            writer.Close()
        End Using
    End Sub







#End Region
    Public MySQL_activepath As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
    ' Public MySQLd_PROC_info As New ProcessStartInfo(MySQL_activepath + "\mysql\mysql-5.7.14-win32\bin\mysqld.exe"):::Old DB Location
    Public MySQLd_PROC_info As New ProcessStartInfo(MySQL_activepath & "\CSI Flex Server\mysql\mysql-5.7.21-win32\bin\mysqld.exe") 'MSI PAth
    'Public MySQLd_PROC_info As New ProcessStartInfo(MySQL_activepath + "\CSI Flex Server\MySQL\MySQL Server 5.7\bin\mysqld.exe") :::: MSI Path
    Public MySQLd_PROC As Process

    Public Function start_mysqld() As Boolean
        Try
            Log_server_event("Starting mysqld : " & vbCrLf)
            ' This  a  Locaked ::::::: MySQL_activepath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

#If DEBUG Then
            Log_server_event("mysqld in debug mode")
            ' This  a  Locaked :::::::MySQL_activepath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(MySQL_activepath).ToString()).ToString()).ToString()).ToString()
#Else
            Log_server_event("mysqld in release mode")
#End If


            'If Not Directory.Exists(MySQL_activepath + "\mysql\mysql-5.7.14-win32\") Then
            'LogServerError("Could not start Mysql: the folder \mysql\mysql-5.7.14-win3\ (in program files x86) is missing", 1)
            'Log_server_event("Could not start Mysql: the folder \mysql\mysql-5.7.14-win3\ (in program files x86) is missing")
            'Return False
            ' Else

            init_mysqld()


            ' Dim p_r() As Process = Process.GetProcessesByName("mysqld")
            Dim isRunning As Boolean = False
            'For Each p_r_l As Process In p_r
            '    If (p_r_l.MainModule.FileName.StartsWith(MySQL_activepath, StringComparison.InvariantCultureIgnoreCase)) Then isRunning = True

            'Next
            Dim connected As Boolean = False
            Try
                Dim mySqlConn As MySqlConnection = New MySqlConnection(MySqlConnectionString)
                mySqlConn.Open()

                If mySqlConn.State = ConnectionState.Open Then
                    mySqlConn.Close()
                    Log_server_event("Connection established with mysqld ...")
                    isRunning = True
                Else
                    isRunning = False
                End If


            Catch ex As Exception
                If ex.Message = "Unable to connect to any of the specified MySQL hosts." Then
                    Log_server_event(" No database connection = no mysqld.")
                    isRunning = False
                End If
            End Try



            '   If p_r.Count > 0 Then
            If isRunning = True Then
                Log_server_event("mysqld connection found.")
                Return True
            Else
                Log_server_event("mysqld connection not found. starting mysqld ...")

                Dim _T_ = Task(Of Boolean).Factory.StartNew(Function() execute_mysqld_process(MySQLd_PROC))

                Dim time_0 As TimeSpan = Now.TimeOfDay
                Dim elapsed As Boolean = False
                While (connected = False And elapsed = False)
                    Thread.Sleep(1000)
                    Try
                        Dim mySqlConn As MySqlConnection = New MySqlConnection(MySqlConnectionString)
                        mySqlConn.Open()

                        If mySqlConn.State = ConnectionState.Open Then
                            mySqlConn.Close()
                            Log_server_event("Connection established with mysqld ...")
                            connected = True
                        Else
                            Log_server_event("Connection attempt failed with mysqld ...")
                            connected = False
                        End If


                    Catch ex As Exception
                        If ex.Message = "Unable to connect to any of the specified MySQL hosts." Then
                            Log_server_event("waiting for mysqld ...")
                            connected = False
                        ElseIf ex.Message = "Authentication to host 'localhost' for user 'root' using method 'mysql_native_password' failed with message: Access denied for user 'root'@'localhost' (using password: YES)" Then
                            Dim mySqlConn As MySqlConnection = New MySqlConnection("server=localhost;user=root;port=3306;")
                            Log_server_event("Trying to connect to mysqld with pw ...")
                            Try
                                mySqlConn.Open()
                            Catch ex2 As Exception
                                Log_server_event("Connection attempt failed with mysqld ... ath issue. ")
                                connected = False
                            End Try


                            If mySqlConn.State = ConnectionState.Open Then
                                mySqlConn.Close()
                                Log_server_event("Connection established with mysqld ...")
                                connected = True
                            Else
                                Log_server_event("Connection attempt failed with mysqld ...")
                                connected = False
                            End If
                        End If
                    End Try
                    Dim time_1 As TimeSpan = Now.TimeOfDay
                    If time_1.TotalSeconds - time_0.TotalSeconds > 30 Then elapsed = True

                End While

                If connected = False Then Log_server_event(" mysqld start fail ...")
                Return connected

            End If


            'End If

        Catch ex As Exception
            LogServerError("Could not start Mysql: " & ex.ToString(), 1)
            Log_server_event("mysqld fail, see err log")

            Return False
        End Try
    End Function

    ''' <summary>
    ''' ///// Funtion to start MySQL Server
    ''' </summary>
    ''' <returns></returns>
    Private Function init_mysqld() As Boolean
        Try
            'Public MySQLd_PROC_info As New ProcessStartInfo(MySQL_activepath + "\CSI Flex Server\MySQL\MySQL Server 5.7\bin\mysqld.exe") :::: MSI Path
            If Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\mysql\mysql-5.7.21-win32\Data\") Then
                Dim p_init As New ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\mysql\mysql-5.7.21-win32\bin\mysqld.exe")
                p_init.Arguments = "--initialize-insecure"
                p_init.UseShellExecute = False
                p_init.CreateNoWindow = True
                Dim proc_init As Process = Process.Start(p_init)
                proc_init.WaitForExit()
                Log_server_event("Database --initialize done.")
                If Not Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\mysql\mysql-5.7.21-win32\Data\") Then
                    LogServerError("Could not start Mysql: could not initialize", 1)
                    Log_server_event("Database init fail")
                    Return False
                End If

                Return True
            Else
                Log_server_event("No --initialize required.")
                Return True
            End If
        Catch ex As Exception
            LogServerError("Could not start Mysql: could not initialize: " & ex.ToString(), 1)
            Log_server_event("Database init fail, see err log")
            Return False
        End Try
    End Function
    Private Function execute_mysqld_process(MySQLd_PROC As Process) As Boolean
        Try


            MySQLd_PROC_info = New ProcessStartInfo(MySQL_activepath + "\CSI Flex Server\mysql\mysql-5.7.21-win32\bin\mysqld.exe")
            MySQLd_PROC_info.Arguments = "--console"
            MySQLd_PROC_info.UseShellExecute = False
            MySQLd_PROC_info.CreateNoWindow = True
            MySQLd_PROC_info.RedirectStandardOutput = True
            MySQLd_PROC = Process.Start(MySQLd_PROC_info)

            'Dim standardOutput = New StringBuilder()
            Dim output As String = ""

            'While (Not MySQLd_PROC.HasExited)
            '    output = output & vbCrLf & standardOutput.Append(MySQLd_PROC.StandardOutput.ReadToEnd()).ToString()
            '    If output.Contains("mysql-5.7.14-win32\bin\mysqld.exe: ready for connections") Then Exit While
            'End While

            '  output = MySQLd_PROC.StandardOutput.ReadToEnd()






            If MySQLd_PROC Is Nothing Then

                Log_server_event("mysqld start fail. MySQLd_PROC is nothing.")
                Return False
            Else

                Log_server_event("mysqld start success.")
                Return True
            End If

        Catch ex As Exception
            LogServerError("Could not exec Mysql: " & ex.ToString(), 1)
            Log_server_event("mysqld exec fail, see err log")
            Return False
        End Try
    End Function

#Region "Math stuff"



    'Dim xdata As Double() = New Double() {10, 20, 30}
    'Dim ydata As Double() = New Double() {15, 20, 25}

    ''Dim p As Tuple(Of Double, Double) = Fit.Polynomial(xdata, ydata, 3)
    'Dim p() As Double = Fit.Polynomial(xdata, ydata, 3)
    'Dim a As Double = p(0)
    '' == 10; intercept
    'Dim b As Double = p(1)
    '' == 0.5; slope



#End Region

    Public Sub New()
    End Sub
End Class


Public Class table_filler

    Public machinename As String
    Public data As DataTable
    Public csi_lib As CSI_Library
    Public filler As Thread

    Sub New(machineName_ As String, datatab As DataTable)
        machinename = machineName_
        data = datatab

        ' filler = New Thread()

    End Sub

End Class







Public Class MCH_
    Shared CSI_LIB As New CSI_Library
    'Public current_stream As DataStream
    Public devices

    Public Conditions_expression_ As New Dictionary(Of String, String)
    Private Property name_ As String = ""
    Private Property ip_ As String = ""
    ' Private Property delay_ As Integer = 0
    Private Property partno_ As String = ""
    Private Property partno_value As String = ""
    Private Property spov_ As String = ""
    Private Property frov_ As String = ""
    Private Property progno_ As String = ""

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

    Enum flag_state
        NOTactivated = 0
        activated = 1
        undefined = 2
    End Enum

    Public flags_ As New Dictionary(Of String, Boolean) ' status - flag : to know if a timer has been strated or not =/= elapsed


    Private Property currentstatus_ As String
    Private Property CheckTimer As Threading.Timer

    'Private Property mch_delays_ As Dictionary(Of String, Dictionary(Of String, Integer)) ' mch - delays dic

    Public Sub New(name__ As String, serverRootPath_ As String, ip As String)
        name_ = name__
        ip_ = ip

        '  delay_ = 0
        partno_ = ""
        partno_value = ""
        spov_ = ""
        frov_ = ""
        progno_ = ""
        serverRootPath = serverRootPath_
        choices = read_choices_ondisk()

        'CheckTimer = New Threading.Timer(AddressOf CheckMachine)
        'CheckTimer.Change(0, 3000)

    End Sub
    Private CheckingMachine As Boolean = False
    Public unreachable As Boolean
    'Private Sub CheckMachine()
    '    Try
    '        If CheckingMachine = False And IP <> "" Then
    '            CheckingMachine = True

    '            Dim crt As ISample
    '            Dim m_client As EntityClient = New EntityClient(IP)
    '            m_client.RequestTimeout = 2000

    '            Dim Current_stream As DataStream = m_client.Current()
    '            devices = m_client.Probe()

    '            If Current_stream IsNot Nothing Then
    '                unreachable = False
    '                'partno
    '                crt = Current_stream.GetSample(partno)
    '                If crt Is Nothing Then
    '                    Dim Conds() As IDataElement = Current_stream.AllConditions
    '                    For Each cond In Conds
    '                        If cond.DataItemID = partno Then
    '                            ' If mch.partno = mch.partno Then mch.update_other("partno", cond.Value)
    '                            update_other("partno", cond.Value)
    '                        End If
    '                    Next
    '                    Dim Events() As IDataElement = Current_stream.AllEvents
    '                    For Each event_ In Events
    '                        If event_.DataItemID = partno Then
    '                            '  If mch.partno = mch.partno Then mch.update_other("partno", event_.Value)
    '                            update_other("partno", event_.Value)
    '                        End If
    '                    Next
    '                Else
    '                    If Not partno = Nothing Then
    '                        '  If mch.partno = mch.partno Then mch.update_other("partno", crt.Value)
    '                        If partno = partno Then update_other("partno", crt.Value)
    '                    End If
    '                End If

    '                'progno 
    '                crt = Current_stream.GetSample(progno)
    '                If crt Is Nothing Then
    '                    Dim Conds() As IDataElement = Current_stream.AllConditions
    '                    For Each cond In Conds
    '                        If cond.DataItemID = progno Then
    '                            If progno = progno Then update_other("progno", cond.Value)
    '                        End If
    '                    Next
    '                    Dim Events() As IDataElement = Current_stream.AllEvents
    '                    For Each event_ In Events
    '                        If event_.DataItemID = progno Then
    '                            If progno = progno Then update_other("progno", event_.Value)
    '                        End If
    '                    Next
    '                Else
    '                    If Not progno = Nothing Then
    '                        If progno = progno Then update_other("progno", crt.Value)
    '                    End If
    '                End If

    '                'spov
    '                crt = Current_stream.GetSample(spov)
    '                If crt Is Nothing Then
    '                    Dim Conds() As IDataElement = Current_stream.AllConditions
    '                    For Each cond In Conds
    '                        If cond.DataItemID = spov Then
    '                            If spov = spov Then update_other("spov", cond.Value)
    '                        End If
    '                    Next
    '                    Dim Events() As IDataElement = Current_stream.AllEvents
    '                    For Each event_ In Events
    '                        If event_.DataItemID = spov Then
    '                            If spov = spov Then update_other("spov", event_.Value)
    '                        End If
    '                    Next
    '                Else
    '                    If Not spov = Nothing Then
    '                        If spov = spov Then update_other("spov", crt.Value)
    '                    End If
    '                End If

    '                'frov
    '                crt = Current_stream.GetSample(frov)
    '                If crt Is Nothing Then
    '                    Dim Conds() As IDataElement = Current_stream.AllConditions
    '                    For Each cond In Conds
    '                        If cond.DataItemID = frov Then
    '                            If frov = frov Then update_other("frov", cond.Value)
    '                        End If
    '                    Next
    '                    Dim Events() As IDataElement = Current_stream.AllEvents
    '                    For Each event_ In Events
    '                        If event_.DataItemID = frov Then
    '                            If frov = frov Then update_other("frov", event_.Value)
    '                        End If
    '                    Next
    '                Else
    '                    If Not frov = Nothing Then
    '                        If frov = frov Then update_other("frov", crt.Value)
    '                    End If
    '                End If
    '            Else
    '                unreachable = True
    '            End If
    '            Dim Buffer_list As List(Of String) = New List(Of String)
    '            Buffer_list = Current_selected_.ToList()

    '            For Each element As String In Buffer_list.ToList

    '                Dim str() As String = element.Split(" = ")
    '                Dim ID = str(0)

    '                If Current_stream IsNot Nothing Then
    '                    crt = Current_stream.GetSample(ID)

    '                    If crt Is Nothing Then
    '                        Dim Conds() As IDataElement = Current_stream.AllConditions
    '                        For Each cond In Conds
    '                            If cond.DataItemID = ID Then
    '                                Update_current_selected(ID, cond.Value)
    '                            End If
    '                        Next

    '                        Dim Events() As IDataElement = Current_stream.AllEvents
    '                        For Each event_ In Events
    '                            If event_.DataItemID = ID Then
    '                                Update_current_selected(ID, event_.Value)
    '                            End If
    '                        Next
    '                    Else
    '                        If Not ID = Nothing Then
    '                            Update_current_selected(ID, crt.Value)
    '                        End If
    '                    End If
    '                End If
    '            Next

    '            'notif
    '            If Current_stream IsNot Nothing Then Current_cond = Current_stream.AllConditions
    '            CheckingMachine = False
    '        End If

    '    Catch ex As Exception
    '        CheckingMachine = False

    '        '    LogServerError("cannot get current values for mtconnect: " & ex.Message & vbCrLf & " " & ex.StackTrace & vbCrLf, 1)
    '    End Try

    'End Sub



    Private Sub Update_current_selected(value_ As String, data As String)

        If data = "Unavailable" Then unreachable = True
        Dim Buffer_list As List(Of String) = New List(Of String)
        Dim Buffer_list2 As List(Of String) = New List(Of String)
        Buffer_list = Current_selected_.ToList()
        Buffer_list2 = Current_selected_.ToList()
        Try
            If data <> "" And DoNotUpdate = False Then
                If value_ = "partno" Or value_ = "spov" Or value_ = "frov" Then
                    update_other(value_, data)
                Else
                    For Each item_ As String In Buffer_list
                        'Next 2 lines are not stupide !
                        Dim index As Integer = Buffer_list.IndexOf(value_ & " = ")
                        If index <> -1 Then
                            Buffer_list2.RemoveAt(index)
                            Buffer_list2.Add(value_ & " = " & data)
                            GoTo updated
                        Else
                            If item_.StartsWith(value_) Then
                                Buffer_list2.RemoveAt(Buffer_list2.IndexOf(item_))
                                Buffer_list2.Add(value_ & " = " & data)
                                GoTo updated
                            End If
                        End If
                    Next
                    Buffer_list2.Add(value_ & " = " & data)
updated:
                    Current_selected_ = Buffer_list2
                End If
            End If
        Catch ex As Exception


            '    LogServerError("cannot get current values for mtconnect: " & ex.Message & vbCrLf & " " & ex.StackTrace & vbCrLf, 1)
        End Try
    End Sub

    Property MachineName As String
        Get
            Return name_
        End Get
        Set(value As String)
            name_ = value
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

            If currentstatus_ <> value Then

                If Not delays_.ContainsKey(value) Then
                    currentstatus_ = value
                    'do nothing else
                Else
                    If delays_(value) <> 0 Then
                        If flag(value) = True Then
                            If check_timer(value) Then
                                currentstatus_ = value
                                flag(value) = False
                            Else
                                'do nothing 
                            End If
                        Else
                            start_timer(value)
                            ' flag(value) = True
                        End If
                    Else
                        currentstatus_ = value
                        'do nothing else
                    End If
                End If




                Send_notif("status", "")

            Else

                'do nothing
            End If

        End Set
    End Property

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

                                    'send_email(MachineName, "status", currentstatus_, 0, tbl_sendto, current_stream, devices)
                                Else
                                    Status_timer = New Timer_with_tag
                                    Status_timer.Stop()
                                    Status_timer.Interval = row(2) * 1000
                                    Status_timer.tag = currentstatus_
                                    Status_timer.machinename = MachineName
                                    Status_timer.tbl_sendto = tbl_sendto
                                    Status_timer.AutoReset = False
                                    Status_timer.devices = devices
                                    ' Status_timer.current_stream = current_stream
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
                                    'send_email(MachineName, "Warning", item.value, 0, tbl_sendto, current_stream, devices)
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
                                        ' notif_cond_timers(item.DataItemID).current_stream = current_stream
                                        AddHandler notif_cond_timers(item.DataItemID).Elapsed, AddressOf MCH_.cond_HandleTimer
                                    End If
                                    notif_cond_timers(item.DataItemID).Start()
                                End If
                            End If

                            'fault
                            If row(2) = True And item.value.ToString().StartsWith("Fault") Then
                                If row(3) = 0 Then
                                    'send 
                                    'send_email(MachineName, "Fault", item.value, 0, tbl_sendto, current_stream, devices)
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
        '  SAVE_MTC_CURRENT(MachineName)
    End Sub
    Private Function Find_previous_cond_value(cond_ As ICondition) As String

        If Previous_cond_ Is Nothing Then Return ""

        Dim cond As ICondition = Array.Find(Previous_cond_, Function(x) (x.DataItemID = cond_.DataItemID))

        'For Each cond As ICondition In Previous_cond_
        '    If cond.DataItemID = cond_.DataItemID Then Return cond.Value
        'Next
        If cond IsNot Nothing Then
            Return cond.Value
        Else
            Return ""
        End If
    End Function

    Private Shared Async Sub Status_HandleTimer(sender As Object, e As EventArgs)
        If Status_timer.Enabled = True Then Status_timer.Stop()
        Status_timer = Nothing

        'send_email(sender.machinename, "status", sender.tag, sender.interval, sender.tbl_sendto, sender.current_stream, sender.devices)
    End Sub
    Private Shared Async Sub cond_HandleTimer(sender As Object, e As EventArgs)
        '  Await Task.Start(Sub() send_email(MachineName, "condition", sender.tag.value, sender.interval))
        'send_email(sender.machinename, "condition", sender.tag.value, sender.interval, sender.tbl_sendto, sender.current_stream, sender.devices)
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

    Public Sub start_timer(status_ As String)
        If Timers_ IsNot Nothing Then
            If Not Timers_.ContainsKey(status_) Then
                Timers_.Add(status_, New System.Timers.Timer(delays(status_) * 1000))
            End If

            If Timers_(status_).Enabled = False Then
                Timers_(status_) = New System.Timers.Timer(delays(status_) * 1000)
                Timers_(status_).AutoReset = False
                Timers_(status_).Start()
                flag(status_) = True
            End If

        Else
            If Timers_.ContainsKey(status_) Then
                Timers_(status_) = New System.Timers.Timer(delays(status_) * 1000)
                Timers_(status_).AutoReset = False
                Timers_(status_).Start()
                flag(status_) = True
            End If
        End If
    End Sub
    Public Sub stop_timer(status_ As String)
        If Timers_(status_).Enabled = False Then
            Timers_(status_).Stop()
            flag(status_) = False
        End If
    End Sub
    Public Function check_timer(status_ As String) As Boolean

        If flag(status_) = True Then
            If Timers_(status_).Enabled = True Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

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

    'Property progno As String
    '    Get
    '        Return progno_
    '    End Get
    '    Set(value As String)
    '        progno_ = value
    '    End Set
    'End Property

    Property IP As String
        Get
            Return ip_
        End Get
        Set(value As String)
            ip_ = value
        End Set
    End Property
    'Property delay As String
    '    Get
    '        Return delay_
    '    End Get
    '    Set(value As String)
    '        delay_ = value
    '    End Set
    'End Property

    Property partno As String
        Get
            Return partno_
        End Get
        Set(value As String)

            partno_ = value
            'If current_other.ContainsKey("partno") Then
            '    current_other("partno") = value
            'Else
            '    current_other.Add("partno", value)
            'End If

        End Set
    End Property
    Property partno_val As String
        Get
            Return partno_value
        End Get
        Set(value As String)
            partno_value = value
        End Set
    End Property
    Public Property PTN_startwith As String
    Public Property PTN_endwith As String

    Property progno As String
        Get
            Return progno_
        End Get
        Set(value As String)
            progno_ = value
            'If current_other.ContainsKey("progno") Then
            '    current_other("progno") = value
            'Else
            '    current_other.Add("progno", value)
            'End If

        End Set
    End Property
    Property spov As String
        Get
            Return spov_
        End Get
        Set(value As String)
            spov_ = value

            'If current_other.ContainsKey("spov") Then
            '    current_other("spov") = value
            'Else
            '    current_other.Add("spov", value)
            'End If

        End Set
    End Property
    Property frov As String
        Get
            Return frov_
        End Get
        Set(value As String)
            frov_ = value
            'If current_other.ContainsKey("frov") Then
            '    current_other("frov") = value
            'Else
            '    current_other.Add("frov", value)
            'End If
        End Set
    End Property
    Function update_other(type As String, value As String)

        Select Case type
            Case "partno"
                ' :::::::::::::::::::::::: NEW  CODE ::::::::::::::::::::::::::::
                'here put same logic for PartNumber Filtering  ::::::::::::: 
                If value.Length > 0 And value.Contains(PTN_startwith) And value.Contains(PTN_endwith) Then
                    Dim main = value
                    Dim startstr = PTN_startwith
                    Dim len_startstr = startstr.Length
                    Dim endstr = PTN_endwith
                    Dim len_mainstr = main.Length
                    Dim index_startstr As Integer

                    Dim result As String
                    Dim index_endstr As Integer
                    If Not startstr.Length > 0 Then
                        index_startstr = 0
                    Else
                        index_startstr = main.IndexOf(startstr)
                    End If
                    Dim len_substring = len_mainstr - (index_startstr + len_startstr)
                    Dim substring = main.Substring(index_startstr + len_startstr, len_substring)
                    If endstr.Length > 0 Then
                        Try
                            index_endstr = substring.IndexOf(endstr)
                        Catch ex As Exception
                            Dim erete = ex.ToString()
                        End Try
                        If index_endstr = -1 Then
                            result = "N/A"
                        Else
                            result = substring.Substring(0, index_endstr)
                        End If
                    Else
                        result = substring
                    End If
                    value = result
                    If Not value.Length > 0 Then
                        value = "N/A"
                    End If
                Else
                    value = "N/A"
                End If
                If current_other.ContainsKey("partno") Then
                    current_other("partno") = value
                Else
                    current_other.Add("partno", value)
                End If
#If False Then
  '::::::::::::::::::::::: OLD CODE :::::::::::::::::::::::::::: need to comment it 
                If PTN_startwith <> "" Then
                    If value.StartsWith(PTN_startwith) Then

                        'remove prefixe
                        value = value.Replace(PTN_startwith, "")

                        'check and remove suffix
                        If PTN_endwith <> "" Then value = value.Substring(0, value.IndexOf(PTN_endwith))

                        If current_other.ContainsKey("partno") Then
                            current_other("partno") = value
                        Else
                            current_other.Add("partno", value)
                        End If
                    End If
                Else
                    'check and remove suffix
                    If PTN_endwith <> "" Then value = value.Substring(0, value.IndexOf(PTN_endwith))

                    If current_other.ContainsKey("partno") Then
                        current_other("partno") = value
                    Else
                        current_other.Add("partno", value)
                    End If
                End If
#End If
            Case "frov"
                If current_other.ContainsKey("frov") Then
                    current_other("frov") = value
                Else
                    current_other.Add("frov", value)
                End If
            Case "spov"
                If current_other.ContainsKey("spov") Then
                    current_other("spov") = value
                Else
                    current_other.Add("spov", value)
                End If
            Case "progno"
                If current_other.ContainsKey("progno") Then
                    current_other("progno") = value
                Else
                    current_other.Add("progno", value)
                End If
        End Select
    End Function

    Property rpn_result_intern_ As RpnOperand
        Get
            Return rpn_result__
        End Get
        Set(value As RpnOperand)
            rpn_result__ = value
        End Set
    End Property






    Sub clear_condition(value As String)
        Conditions_expression_.Clear()
    End Sub



    'Set machines back to COFF without partnumbers
    Private Sub Cleanup()
        name_ = ""
        ip_ = ""

        ' delay_ = 0
        partno_ = ""
        partno_value = ""
        spov_ = ""
        frov_ = ""
    End Sub

    Private choices As New Dictionary(Of String, List(Of String)) ' condition id / list of values

    Public Sub save_item(item_id As String, value As String)

        If choices.ContainsKey(item_id) Then
            If Not choices(item_id).Contains(value) Then
                choices(item_id).Add(value)
                save_choices_ondisk(choices)
            End If
        Else
            choices.Add(item_id, New List(Of String))
            choices(item_id).Add(value)
            save_choices_ondisk(choices)
        End If

    End Sub
    Private Sub save_choices_ondisk(d As Dictionary(Of String, List(Of String)))
        If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "choices.bin") Then File.Delete(File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "choices.bin"))
        Dim fs As IO.FileStream = New IO.FileStream(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "choices.bin", IO.FileMode.OpenOrCreate)
        Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        bf.Serialize(fs, d)
        fs.Close()
    End Sub
    Private Function read_choices_ondisk() As Dictionary(Of String, List(Of String))

        Dim d As New Dictionary(Of String, List(Of String))
        If File.Exists(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "choices.bin") Then
            Dim fs As IO.FileStream = New IO.FileStream(serverRootPath & "\sys\Conditions\" & MachineName & "\" & "choices.bin", IO.FileMode.Open)

            Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            d = bf.Deserialize(fs)
            fs.Close()
        End If
        Return d
    End Function

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


    '    Current status : <b> xxxxx </b> , since xxx seconds. <br /><br />
    'Condition alert : <b> xxxxx </b>, since xxx seconds.  <br /><br />  <br /><br />


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

    'Private Shared Sub send_email(machinename As String, type As String, currentstatus_ As String, delay As Integer, tbl_sendto As DataTable, current_stream As DataStream, devices As Object) 'mailadresses As String, filePath As String, custommsg As String)

    '    Dim host As String
    '    Dim smtpport As Integer
    '    Dim sender As String
    '    Dim auth As Boolean
    '    Dim pass As String

    '    delay = delay / 1000

    '    Using writer As StreamWriter = New StreamWriter("C:\Log_Server.txt", True)
    '        writer.Write(vbCrLf & (machinename + " : " + type + " : " + currentstatus_ + " : " + delay.ToString()))
    '        writer.Close()
    '    End Using

    '    Using r As StreamReader = New StreamReader(serverRootPath + "\sys\" & "\email.csys")
    '        host = r.ReadLine
    '        smtpport = r.ReadLine
    '        sender = r.ReadLine
    '        auth = r.ReadLine
    '        pass = AES_Decrypt(r.ReadLine, "pass")

    '        r.Close()
    '    End Using

    '    Dim status_text As String = ""
    '    Dim cond_text As String = ""

    '    Dim key_words As New Dictionary(Of String, String)
    '    key_words.Add("[MachineName]", machinename)
    '    key_words.Add("[StatusName]", currentstatus_)
    '    key_words.Add("[ConditionName]", currentstatus_)
    '    key_words.Add("[TimeEvent]", Now.AddSeconds(-delay).ToString("yyyy-MMMM-dd HH:mm:ss"))


    '    If File.Exists(serverRootPath & "\sys\Conditions\" & machinename & "\text.csys") Then


    '        Using r As StreamReader = New StreamReader(serverRootPath + "\sys\Conditions\" & machinename & "\text.csys")
    '            '  Dim t() As String = r.ReadToEnd.Replace(vbCrLf, "").Split("|")
    '            Dim t_ As String = r.ReadToEnd.Replace(vbCrLf, "")
    '            t_ = t_.Replace("AwaitedDelay", "TimeEvent")

    '            For Each key In key_words.Keys
    '                t_ = t_.Replace(key, key_words(key))
    '            Next

    '            Dim t() As String = t_.Split("|")

    '            If Not t.Length < 1 Then status_text = t(0)
    '            If Not t.Length < 2 Then cond_text = t(1)

    '            r.Close()
    '        End Using
    '    End If

    '    If (tbl_sendto.Rows.Count > 0) Then

    '        Try
    '            Dim mail As MailMessage = New MailMessage()
    '            mail.IsBodyHtml = True
    '            Dim client As SmtpClient = New SmtpClient(host, smtpport)
    '            mail.From = New MailAddress(sender)
    '            client.EnableSsl = False
    '            client.Timeout = 10000
    '            client.DeliveryMethod = SmtpDeliveryMethod.Network
    '            client.UseDefaultCredentials = False

    '            If (auth) Then
    '                client.Credentials = New System.Net.NetworkCredential(sender, pass)
    '            End If

    '            Dim modified_block As String = ""
    '            modified_block = BLOCK.Replace("{mchName}", machinename)
    '            Dim small_text As String = ""

    '            If type = "status" Then
    '                'mail.Body = status_text
    '                modified_block = modified_block.Replace("[TEXT]", status_text)
    '                small_text = status_text
    '                mail.Subject = "CSIFlex Notification: " + machinename + " Status: " + currentstatus_
    '            Else
    '                'mail.Body = cond_text
    '                modified_block = modified_block.Replace("[TEXT]", cond_text)
    '                small_text = cond_text
    '                mail.Subject = "CSIFlex Notification: " + machinename + " on " + currentstatus_
    '            End If


    '            Dim PORT As String = ""
    '            Try
    '                If File.Exists((serverRootPath & "\sys\RM_port_.csys")) Then
    '                    Using reader As StreamReader = New StreamReader(serverRootPath & "\sys\RM_port_.csys")
    '                        PORT = reader.ReadLine
    '                        reader.Close()
    '                    End Using
    '                End If
    '            Catch ex As Exception
    '                CSI_LIB.LogServerError("At reading RM Port from csys: " & ex.Message, 1)
    '            End Try

    '            Dim srv_IP As String = ""
    '            Dim host_IP = Dns.GetHostEntry(Dns.GetHostName())
    '            For Each x In host_IP.AddressList
    '                If x.AddressFamily = AddressFamily.InterNetwork Then
    '                    srv_IP = x.ToString()
    '                End If
    '            Next

    '            If srv_IP = "" Then Throw New Exception("Local IP Address Not Found!")

    '            Dim page_ID As String = Generate_html_tree(machinename, current_stream, devices, small_text)
    '            modified_block = modified_block.Replace("[LiNK]", "http://" + srv_IP + ":" + PORT + "/saved_pages/" + machinename + "_" + page_ID)

    '            mail.Body = modified_block

    '            For Each email As DataRow In tbl_sendto.Rows
    '                If (email.Item(0) = True) And Not email.Item(1) = "" Then
    '                    mail.To.Add(New MailAddress(email.Item(1)))
    '                End If
    '            Next

    '            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

    '            client.Send(mail)

    '        Catch ex As Exception
    '            ' CSI_LIB.LogServiceError("email error,CONFIG:MailAdresses:" + mailadresses.ToString() + ",port:" + emailconfig.Rows(0)("smtpport").ToString() + ",server:" + emailconfig.Rows(0)("smtphost").ToString() + ",requirecred:" + emailconfig.Rows(0)("requirecred").ToString() + ",email:" + emailconfig.Rows(0)("senderemail").ToString() + ",pwd:" + emailconfig.Rows(0)("senderpwd").ToString() + ",MSG:" + ex.Message, 1)
    '            CSI_LIB.LogServiceError("email not sent : " + ex.Message & vbCrLf & ex.StackTrace, 1)

    '        End Try

    '    End If
    'End Sub

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

            ' save '''
            '
            Return fCount.ToString()

        Catch ex As Exception
            CSI_LIB.LogServiceError("Could not save page : " + ex.Message, 1)
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

    'Private Shared Function Generate_html_tree(machinename As String, current_stream As DataStream, devices As Object, text As String) 'mailadresses As String, filePath As String, custommsg As String)

    '    Dim s As String = ""
    '    Dim h As String = ""
    '    Dim hh As String = ""
    '    Dim xml As DataStream = current_stream

    '    Dim device_index As Integer = 0
    '    Try

    '        Dim Current_device As DeviceStream
    '        Dim value As String = ""


    '        's = s + "<table style=""width:100%"">"
    '        For Each device In devices
    '            If (machinename = device.Name) Then
    '                Current_device = current_stream.DeviceStreams(device_index)
    '                Dim current_component_stream() As ComponentStream = Current_device.ComponentStreams

    '                's = add_LI_parent(device.uuid, device.Name, "")

    '                h = ""
    '                hh = ""

    '                For Each component In device.Components

    '                    hh = fill_component_(component, Current_device)

    '                    h = h + add_LI_parent(component.ID, component.name, "<ol class=""dd-list"">" + hh + "</ol>")

    '                Next

    '                s = s + add_LI_parent(device.uuid, device.Name, "<ol class=""dd-list"">" + h + "</ol>")
    '                ' s = s + "<table style=""width:100%"">"
    '                For Each dataItem In device.DataItems

    '                    Dim displayName As String = dataItem.ID

    '                    For Each Current_comp_stream In current_component_stream
    '                        If Current_comp_stream.ComponentType = "Device" Then
    '                            Dim current_events() As IDataElement = Current_comp_stream.Events
    '                            For Each ev In current_events
    '                                If displayName = ev.DataItemID Then
    '                                    s = s + add_LI(ev.DataItemID, "<td><b>" + ev.Type + "  : </b></td> <td> " + ev.Value + "</td>")

    '                                End If
    '                            Next
    '                        End If
    '                    Next

    '                Next
    '                '  s = s + "</table>"
    '            End If
    '            device_index += 1
    '        Next
    '        ' s = s + "</table>"
    '    Catch ex As Exception
    '        ' MsgBox(ex.Message)
    '    End Try



    '    Return save_page(text, s, machinename)


    'End Function

    'Private Shared Function fill_component_(component As OpenNETCF.MTConnect.Component, Current_device As DeviceStream) As String


    '    ' Dim boldFont As Font = New Font(TGV_MTC.DefaultCellStyle.Font.Name, 10, TGV_MTC.DefaultCellStyle.Font.Bold, TGV_MTC.DefaultCellStyle.Font.Unit)
    '    Dim hh As String = ""
    '    Dim sub_h As String = ""
    '    'Dim componentNode As TreeGridNode = Parent.Nodes.Add(component.Name, "")
    '    'componentNode.Tag = component
    '    'componentNode.DefaultCellStyle.BackColor = Color.Gray
    '    'componentNode.DefaultCellStyle.ForeColor = Color.White
    '    'componentNode.DefaultCellStyle.Font = boldFont

    '    For Each subcomponent In component.Components
    '        sub_h = fill_component_(subcomponent, Current_device)
    '        hh = hh + add_LI_parent(subcomponent.ID, subcomponent.Name, "<ol class=""dd-list"">" + sub_h + "</ol>")
    '    Next
    '    sub_h = ""


    '    Dim found As Boolean = False

    '    Dim component_index As Integer = -1
    '    For Each subcomponent In Current_device.ComponentStreams
    '        component_index += 1
    '        If subcomponent.Name = component.Name And found = False Then
    '            found = True
    '            Exit For
    '        End If
    '    Next

    '    If found = True Or Current_device.ComponentStreams.Count = 0 Then
    '        Dim Current_dataitems() As IDataElement = Current_device.ComponentStreams(component_index).AllDataItems
    '        Dim Current_conditions() As IDataElement = Current_device.ComponentStreams(component_index).Conditions


    '        Dim first__ As Boolean = True
    '        Dim value As String = ""

    '        'hh += "<table style=""width:100%"">"

    '        For Each dataItem In component.DataItems




    '            Dim displayName As String = dataItem.ID

    '            value = ""
    '            If Not (String.IsNullOrEmpty(dataItem.Name)) Then
    '                displayName += String.Format(" ({0})", dataItem.Name)

    '                For Each CurrentdataItem In Current_dataitems
    '                    If dataItem.ID = CurrentdataItem.DataItemID Then

    '                        value = CurrentdataItem.Value
    '                        Exit For
    '                    End If
    '                Next
    '                For Each Currentcondition In Current_conditions
    '                    If dataItem.ID = Currentcondition.DataItemID Then

    '                        value = Currentcondition.Value
    '                    End If

    '                Next

    '            End If

    '            If Not (String.IsNullOrEmpty(dataItem.ID)) Then
    '                For Each Currentcondition In Current_conditions
    '                    If dataItem.ID = Currentcondition.DataItemID Then

    '                        value = Currentcondition.Value
    '                    End If
    '                Next

    '                For Each CurrentdataItem In Current_dataitems
    '                    If dataItem.ID = CurrentdataItem.DataItemID Then

    '                        value = CurrentdataItem.Value
    '                    End If
    '                Next
    '            End If

    '            '    Dim notboldFont As Font = New Font(TGV_MTC.DefaultCellStyle.Font.Name, 8, TGV_MTC.DefaultCellStyle.Font.Style, TGV_MTC.DefaultCellStyle.Font.Unit)


    '            hh += add_LI(dataItem.ID, "<td><b>" + If(dataItem.Name Is Nothing, dataItem.ID, dataItem.Name) + " : </b></td> <td> " + value + "</td>")

    '        Next
    '    End If
    '    'hh += "</table>"
    '    Return hh
    'End Function
    Private Shared Function add_LI_parent(id As String, name As String, inner As String) As String

        Return "<li class=""dd-item"" data-id=" + id + ">" + "<div class=""dd-handle""> " + name + " </div>" + inner + "</li>" + vbCrLf

    End Function
    Private Shared Function add_LI(id As String, inner As String) As String

        Return "<li class=""dd-item"" data-id=" + id + ">" + "<tr>" + inner + "</tr>" + "</li>" + vbCrLf

    End Function



End Class

Class Timer_with_tag
    Inherits Timers.Timer
    Public Property tag As Object
    Public Property machinename As String
    Public Property tbl_sendto As DataTable
    Public Property devices As Object
    'Public Property current_stream As DataStream
End Class
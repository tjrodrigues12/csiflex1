'Imports System.Reflection
Imports System.IO
Imports System.Threading
Imports MySql.Data.MySqlClient
Imports CSI_Library


Public Class RainMeterAPI

    'NOT WORKING, MANUAL INSTALL IS PREFERED
    'Public Shared Sub InstallRMAsAdmin()
    '    Try
    '        Dim path As String = System.IO.Path.GetTempPath() 'System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
    '        'Dim commandSTRING As String
    '        path = path & "Rainmeter_3_3_r2461_beta.exe"

    '        If Not File.Exists(path) Then
    '            IO.File.WriteAllBytes(path, My.Resources.Rainmeter_3_3_r2461_beta)
    '        End If


    '        Dim process As New System.Diagnostics.Process()
    '        Dim startInfo As New System.Diagnostics.ProcessStartInfo()
    '        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
    '        'startInfo.Verb = "runas"
    '        startInfo.RedirectStandardInput = True
    '        startInfo.RedirectStandardOutput = True
    '        startInfo.UseShellExecute = False 'required to redirect
    '        startInfo.FileName = "cmd.exe"
    '        startInfo.Arguments = "" '"/S /DESKTOPSHORTCUT=0 /STARTUP=1"
    '        process.StartInfo = startInfo
    '        process.Start()
    '        'process.WaitForExit()


    '        Dim SR As System.IO.StreamReader = process.StandardOutput
    '        Dim SW As System.IO.StreamWriter = process.StandardInput

    '        Dim commandSTRING As String = path & " /S /DESKTOPSHORTCUT=0 /STARTUP=1"
    '        SW.WriteLine(commandSTRING)
    '        Thread.Sleep(2000)
    '        SW.WriteLine("exit")
    '        Thread.Sleep(1000)
    '        'process.WaitForExit()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

    'Public Shared Sub InstallRMStandard()
    '    Try
    '        Dim path As String = System.IO.Path.GetTempPath() 'System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
    '        'Dim commandSTRING As String
    '        path = path & "Rainmeter_3_3_r2461_beta.exe"

    '        If Not File.Exists(path) Then
    '            IO.File.WriteAllBytes(path, My.Resources.Rainmeter_3_3_r2461_beta)
    '        End If


    '        'Dim commandSTRING As String = path & " /S /DESKTOPSHORTCUT=0 /STARTUP=1"
    '        Dim process As New System.Diagnostics.Process()
    '        Dim startInfo As New System.Diagnostics.ProcessStartInfo()
    '        'startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
    '        'startInfo.CreateNoWindow = True
    '        startInfo.UseShellExecute = False 'required to redirect
    '        'startInfo.Verb = "runas"
    '        startInfo.FileName = path '"runas.exe"
    '        process.StartInfo = startInfo
    '        process.Start()
    '        process.WaitForExit()


    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

    'Public Shared Sub ConfigureFirstStart()
    '    Try
    '        Dim path As String = GetInstallationPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\Rainmeter\Defaults\Layouts\illustro default\Rainmeter.ini")

    '        Dim cnt As Integer = 0

    '        While Not File.Exists(path) And cnt < 10
    '            Thread.Sleep(500)
    '            cnt += 1
    '        End While

    '        If (File.Exists(path)) Then
    '            File.Delete(path)
    '            File.WriteAllText(path, "[Rainmeter]")
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Shared Sub CheckResources()
        Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Rainmeter\Skins\CSIFlex\@Resources\"

        If Not (Directory.Exists(path)) Then
            Directory.CreateDirectory(path)
        End If

        path = path & "Background.png"

        If Not File.Exists(path) Then
            My.Resources.Background.Save(path)
        End If
    End Sub


    Public Shared Function StartRM() As Boolean

        Dim started As Boolean = False
        Try

            Dim path As String = GetInstallationPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\Rainmeter\Rainmeter.exe")

            Dim cnt As Integer = 0

            If (Process.GetProcessesByName("Rainmeter").Length > 0) Then
                'Console.WriteLine("Rainmeter is already running.")
                started = True
            End If

            While Not Process.GetProcessesByName("Rainmeter").Length > 0 And cnt < 3
                Dim process As New System.Diagnostics.Process()
                Dim startInfo As New System.Diagnostics.ProcessStartInfo()
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                startInfo.FileName = path '"cmd.exe"
                process.StartInfo = startInfo
                process.Start()
                'process.WaitForExit()
                started = True
                Thread.Sleep(1000)
                cnt += 1
            End While

            CheckResources()

        Catch ex As Exception
            'Throw ex
            System.Console.WriteLine("file not found:" + ex.Message)
        End Try

        Thread.Sleep(1000)

        Return started

    End Function


    Public Shared Function Get_RM_PORT(serverRootPath As String) As String

        Try
            If File.Exists((serverRootPath & "\sys\RM_port_.csys")) Then
                Dim port As String = ""
                Using reader As StreamReader = New StreamReader(serverRootPath & "\sys\RM_port_.csys")
                    port = reader.ReadLine.ToString()
                    reader.Close()
                End Using
                Return port
            End If
        Catch ex As Exception
            MsgBox("Could not load the Rainmeter port")

        End Try


        'If mySqlCnt.State = ConnectionState.Open Then
        '    Dim cmd As New MySqlCommand("SELECT port from CSI_database.tbl_rm_port", mySqlCnt)
        '    Dim rdr As MySqlDataReader = cmd.ExecuteReader
        '    Return rdr.Read
        'End If

    End Function

    Public Shared Sub CreateMachineSkin(machineId As String, machineName As String, ip As String)
        Try
            Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Rainmeter\Skins\CSIFlex\" & machineName & "\"

            If Not (Directory.Exists(path)) Then
                Directory.CreateDirectory(path)
            End If

            Dim filepath As String = path & machineName & ".ini"

            Dim filecontent = My.Resources.CSIFLEX

            filecontent = filecontent.Replace("MACHINEID", machineId)
            filecontent = filecontent.Replace("MACHINENAME", machineName)

            Dim csilib As New CSI_Library(True)
            ' Dim ip As String = csilib.Networkenet


            filecontent = filecontent.Replace("ENETWEBPAGE", ip)

            If Not File.Exists(filepath) Then
                IO.File.WriteAllText(filepath, filecontent)
            End If

            RefreshSkinList()
            ActivateSkin(machineName)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub




    Public Shared Sub RefreshSkinList()
        'Use Bang:!RefreshApp
        Try
            Dim path As String = GetInstallationPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\Rainmeter\Rainmeter.exe")

            Dim process As New System.Diagnostics.Process()
            Dim startInfo As New System.Diagnostics.ProcessStartInfo()
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            startInfo.FileName = path
            startInfo.Arguments = "!RefreshApp"
            process.StartInfo = startInfo
            process.Start()
            process.WaitForExit()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub ActivateSkin(machinename As String)
        'Use bang:!ActivateConfig
        Try
            Dim path As String = GetInstallationPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\Rainmeter\Rainmeter.exe")

            Dim process As New System.Diagnostics.Process()
            Dim startInfo As New System.Diagnostics.ProcessStartInfo()
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            startInfo.FileName = path
            startInfo.Arguments = "!ActivateConfig ""CSIFlex\" & machinename & """"
            process.StartInfo = startInfo
            process.Start()
            process.WaitForExit()

            'Thread.Sleep(1000)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub RemoveSkin(machinename As String)
        'Use bang:!DeactivateConfig
        Try
            Dim path As String = GetInstallationPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\Rainmeter\Rainmeter.exe")

            Dim process As New System.Diagnostics.Process()
            Dim startInfo As New System.Diagnostics.ProcessStartInfo()
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            startInfo.FileName = path
            startInfo.Arguments = "!DeactivateConfig ""CSIFlex\" & machinename & """"
            process.StartInfo = startInfo
            process.Start()

            'Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Rainmeter\Skins\CSIFlex\" & machinename & "\"
            Dim skinpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Rainmeter\Skins\CSIFlex\" & machinename & "\"

            If (Directory.Exists(skinpath)) Then
                Directory.Delete(skinpath, True)
            End If

            'RefreshSkinList()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub QuitRM()
        'Use Bang:!Quit
        Try
            Dim path As String = GetInstallationPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\Rainmeter\Rainmeter.exe")

            Dim process As New System.Diagnostics.Process()
            Dim startInfo As New System.Diagnostics.ProcessStartInfo()
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            startInfo.FileName = path
            startInfo.Arguments = "!Quit"
            process.StartInfo = startInfo
            process.Start()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Shared Function GetInstallationPath(filepath As String) As String
        Dim truepath As String = filepath 'Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) ' & "\Rainmeter\Rainmeter.exe"
        If Not File.Exists(filepath) Then
            'Dim tempfolder As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
            'tempfolder = Replace(tempfolder, "(x86)", "").Trim
            truepath = Replace(filepath, " (x86)", "")
        End If

        Return truepath
    End Function

End Class

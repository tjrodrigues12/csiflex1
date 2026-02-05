Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports CSIFLEX.eNetLibrary
'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel

'=========================================================================================================================================
'=========================================================================================================================================
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄ 
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀  ▀▀▀▀█░█▀▀▀▀ 
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░▌          ▐░▌               ▐░▌     
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░▌          ▐░█▄▄▄▄▄▄▄▄▄      ▐░▌     
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░▌          ▐░░░░░░░░░░░▌     ▐░▌     
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░▌           ▀▀▀▀▀▀▀▀▀█░▌     ▐░▌     
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░▌                    ▐░▌     ▐░▌     
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░█▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄█░▌ ▄▄▄▄█░█▄▄▄▄ 
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  Reporting
'=========================================================================================================================================
'=========================================================================================================================================



Public Class Form1
    Public minyear As Date
    Public maxyear As Date
    Public eHUBConf As New Dictionary(Of String, String) ' Used to get the shift setup
    Public MonSetup As New Dictionary(Of String, String) ' Used to get the shift setup
    Public ShiftSetup As New Dictionary(Of String, String)
    Public before2012 As Boolean = False
    Public CSI_Lib As New CSI_Library.CSI_Library(True)
    Public stat(3) As String
    Public chemin_bd As String
    Public chemin_eNET As String
    Public Image1 As Image, bySetup As Boolean = False
    Public machine_list As New Dictionary(Of String, String)


    Public week_ As String = 15
    Public activated_ As Boolean = False
    Public first_creation_of_the_DB As Boolean = False

    '-----------------------------------------------------------------------------------------------------------------------
    '  form1 load function
    '-----------------------------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' check for the existance of sys and html folders 
    ''' verify for the .NET Framework
    ''' Check Config Data 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Hide()

        'New instance of the lib
        CSI_Lib = New CSI_Library.CSI_Library(True)
        ' set path value to C:\ProgramData\CSI Flex Server
        Dim pathForMobileServer As String = "C:\ProgramData\CSIFlexMobileServer"
        Dim path As String = CSI_Library.CSI_Library.serverRootPath
        ' Set the color to the menu strip
        ' MenuStrip1.ForeColor = Color.Black

        Me.BackColor = System.Drawing.SystemColors.ControlLightLight

        ' check if the sys directory exist, and create it if no:
        Dim dir_ok As String = CSI_Lib.dir_(path & "\sys\")
        If dir_ok <> "ok" Then
            MessageBox.Show("CSIFLEX has encountered an error while creating the sys directory in the CSIFLEX folder : " & dir_ok)
            GoTo End2
        End If
        ' This code check if the html directory is exists or not 
        Dim dir_html As String = CSI_Lib.dir_(path & "\html\")
        If dir_html <> "ok" Then
            MessageBox.Show("CSIFLEX has encountered an error while creating the html directory in the CSIFLEX folder : " & dir_html)
            GoTo End2
        End If

        ' check the framwork version.======================================================

        Dim ok As Boolean = CSI_Lib.verif_FRAMEWORK()
        If ok = True Then
        Else
            MessageBox.Show("Please install the Microsoft .NET Framework 4+ ")

            Dim startInfo As New ProcessStartInfo()
            startInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\sys\" + "netFrameWork_4_5_2.exe"
            Process.Start(startInfo)
            Environment.Exit(1)
        End If
        '==================================================================================




        'Check Config Data ================================================================
        Try
            '/////Read and Write the path of the sys folder in Setupdb_.csys file
            If File.Exists(path & "\sys\Setupdb_.csys") Then
                Using reader As StreamReader = New StreamReader(path & "\sys\Setupdb_.csys")
                    chemin_bd = reader.ReadLine()
                End Using
            Else
                chemin_bd = path & "\sys\"
                Using writer As StreamWriter = New StreamWriter(path & "\sys\Setupdb_.csys")
                    writer.WriteLine(chemin_bd)
                End Using
            End If

            '/////If html folder not exists in Program Data folder then copy the html directory in Program Data folder from Program Files(x86) folder
            If Not Directory.Exists(path & "\html\html\") Then
                My.Computer.FileSystem.CopyDirectory("C:\Program Files (x86)\CSI Flex Server\html\", "C:\ProgramData\CSI Flex Server\html\", True)
            Else

#If Not DEBUG Then
                Dim dirInfo = New DirectoryInfo(path & "\html\html\")
                Dim exeInfo = New FileInfo(Assembly.GetExecutingAssembly().Location)

                If dirInfo.CreationTime < exeInfo.LastWriteTime Then
                    My.Computer.FileSystem.CopyDirectory("C:\Program Files (x86)\CSI Flex Server\html\", "C:\ProgramData\CSI Flex Server\html\", True)
                End If
#End If

            End If

            '/////If MobilePHP Server Folder doesn't exists then Copy the Folder to the ProgramData Folder from Program Files(x86) Folder
            If Not Directory.Exists(pathForMobileServer) Then
                Try
                    My.Computer.FileSystem.CopyDirectory("C:\Program Files (x86)\CSI Flex Server\CSIFlexMobileServer", "C:\ProgramData\CSIFlexMobileServer", True)
                Catch ex As Exception

                End Try
            End If

            Dim defaultFolder = "C:\_eNETDNC"

#If DEBUG Then
            defaultFolder = "\\10.0.10.189\C\_eNETDNC"
#End If

            '/////First time opens Edit_eNET form to set eNET Folder Path
            If Not File.Exists(path & "\sys\setup_.csys") Then

                If Directory.Exists(defaultFolder) Then
                    Using writer As StreamWriter = New StreamWriter(path & "\sys\setup_.csys", False)
                        writer.Write(defaultFolder)
                        writer.Close()
                    End Using
                End If

            End If

            eNetServer.Instance.Init(defaultFolder)

            If Not File.Exists(path & "\sys\Networkenet_.csys") Then

                Dim ipAddress = $"{eNetServer.Connections.HttpIp}:{eNetServer.Connections.HttpPort}"

                Using writer As StreamWriter = New StreamWriter(path & "\sys\Networkenet_.csys", False)
                    writer.Write(ipAddress)
                    writer.Close()
                End Using

            End If

            '/////Reads eNET Folder Path 
            Using reader As StreamReader = New StreamReader(path & "\sys\setup_.csys")
                chemin_eNET = reader.ReadLine()
                reader.Close()
            End Using

            'If Not Exists(path & "\sys\monlist.csys") Then
            '/////Copy monlist.sys file from _eNETDNC/_SETUP folder to \sys\monlist.csys to load the current list of machine displayed by the eNET 
            Try
                File.Copy(chemin_eNET & "\_SETUP\monlist.sys", path & "\sys\monlist.csys", True)
            Catch ex As Exception
                MessageBox.Show("Unable to save the eNET machine list : " & ex.Message)
            End Try

            'End If
            'If Not Exists(path & "\sys\machine_list_.csys") Then
            '///// Code to filter monlist.csys file 
            Using reader As StreamReader = New StreamReader(path & "\sys\monlist.csys")

                Dim readed As String = reader.ReadToEnd
                Dim eachmachine As String() = readed.Split(vbCr)
                'Dim machinedata As String()

                Using writer As New StreamWriter(path & "\sys\machine_list_.csys")

                    Dim eNETsource As String = "eNET,First, "
                    For Each machine In eachmachine
                        Select Case Microsoft.VisualBasic.Strings.Left(machine.Trim(), 3)

                            Case "_MT"

                            Case "_ST"

                            Case Else
                                If (machine.Trim() <> "") And (machine <> "") Then
                                    'machinedata = machine.Split(",")
                                    'If UBound(machinedata) = 1 Then machine_list.Add(machinedata(0), machinedata(1))
                                    'If UBound(machinedata) = 0 Then

                                    'End If
                                    'machine = CSI_Lib.RenameMachine(machine)
                                    machine_list.Add(machine, "eNET")
                                    writer.WriteLine(machine & "," & "eNET" & ",0")
                                    eNETsource = eNETsource + machine.Trim + "; "
                                End If
                        End Select
                    Next
                    Using writer2 As New StreamWriter(path & "\sys\machines_sources_.csys")
                        writer2.WriteLine(eNETsource)
                    End Using


                End Using
            End Using
            'End If
            '///// Read and Write value of Setupdt_.csys file
            If File.Exists(path & "\sys\Setupdt_.csys") Then
                Using reader As StreamReader = New StreamReader(path & "\sys\Setupdt_.csys")
                    week_ = reader.ReadLine()
                    reader.Close()
                End Using
            Else
                Using writer As StreamWriter = New StreamWriter(path & "\sys\Setupdt_.csys")
                    writer.Write("15")
                    week_ = "15"
                    writer.Close()
                End Using
            End If
            '/////If html folder not exists in Program Data folder then copy the html directory in Program Data folder from Program Files(x86) folder
            If dir_html = "ok" Then
            Else
                My.Computer.FileSystem.CopyDirectory("C:\Program Files (x86)\CSI Flex Server\html\", "C:\ProgramData\CSI Flex Server\html\", True)
            End If

            '///// Write into CSIFv_.csys file with Encryption Data 
            Using writer As StreamWriter = New StreamWriter(path & "\sys\CSIFv_.csys", False)
                Dim lib__ As New CSI_Library.CSI_Library(True)

                writer.Write(Login.AES_Encrypt("0:-----", "4Solutions"))
                writer.Close()
            End Using

            'Open Histroy Load Form in hidden mode 
            'LoadHistory.Show()
            'Opens the SetUpForm
            SetupForm2.Show()
            'Call here Code To Start the Internal Web Server
            'InternalWebServer.Show() 'Form to Start the Internal Server 
            If chemin_bd Is Nothing Then

                SetupForm2.ShowDialog()
                'Call here Code To Start the Internal Web Server
                GoTo fin
                If Dir(chemin_bd, vbDirectory) = "" Then
                    SetupForm2.ShowDialog()
                    'Call here Code To Start the Internal Web Server
                    GoTo fin
                End If
            End If
fin:
            Me.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade)
            SetStyle(ControlStyles.DoubleBuffer, True)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            SetStyle(ControlStyles.ResizeRedraw, True)

            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Close()
        End Try
End2:
        'Dim autoreport = New CSIFlexServerService.ServiceLibrary()
        'autoreport.StartAutoreporting(True)
    End Sub

    '///// Stop the attempt of closing the form when Database creation process is going on
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If first_creation_of_the_DB = True Then
            MessageBox.Show("The database is being created, please wait", "", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            e.Cancel = True   'stop the form from closing
        End If
        UserLogin.Close()
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' Bouton Configuration
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub ConfigurationToolStripMenuItem_Click(sender As Object, e As EventArgs)

        bySetup = True
        SetupForm2.Show()
        'Call here Code To Start the Internal Web Server
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' Bouton About
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        AboutBox.Show()
    End Sub

    Private Sub bcolor()

        Dim ctl As Control
        Dim ctlMDI As MdiClient

        ' Loop through all of the form's controls looking ' for the control of type MdiClient. 
        For Each ctl In Me.Controls
            Try ' Attempt to cast the control to type MdiClient. 
                ctlMDI = CType(ctl, MdiClient) ' Set the BackColor of the MdiClient control.
                ctlMDI.BackColor = Me.BackColor
                ctl.ForeColor = Color.Black
            Catch exc As InvalidCastException
            End Try
        Next
    End Sub

    Private Sub Form1_Resize0(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        Me.SuspendLayout()
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        '   Me.Update()
        Me.ResumeLayout()
        Me.Refresh()
    End Sub

End Class

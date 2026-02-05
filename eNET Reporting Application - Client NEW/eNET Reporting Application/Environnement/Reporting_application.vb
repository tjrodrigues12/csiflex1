Imports System
Imports System.Windows
Imports System.Reflection
Imports System.IO.File
Imports System.IO
Imports System.IO.Compression
Imports System.Data.OleDb
Imports System.IO.IOException
Imports System.Text
Imports System.Threading
Imports Microsoft.Win32
'Imports Microsoft.Office.Interop

Imports System.Data.SQLite
Imports MySql.Data.MySqlClient
Imports LumenWorks.Framework.IO.Csv


Imports System.Text.RegularExpressions
Imports System.Net



Imports System.Environment
Imports System.ApplicationIdentity
'Imports Microsoft.Office.Interop.Excel
Imports CSI_Library
Imports System.Globalization
Imports System.Xml


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
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  Felx Reporting Client Side 
'=========================================================================================================================================
'=========================================================================================================================================



Public Class Reporting_application
    Public CSFIFLEX_VERSION As String = "1.8.9.4"

    Public User_Connection_String As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Forms.Application.StartupPath.ToString() &
                                          "\sys\CSI_auth.mdb; Jet OLEDB:Database Password=4Solutions; mode=ReadWrite;"
    Public LocalDB_Connection_String As String = ""
    Public ServerDB_Connection_String As String = ""
    Public Shared cs As String = CSI_Library.CSI_Library.sqlitedbpath
    Public username_ As String
    Public Shared setupForm_Singleton As Reporting_availability
    Public minyear As Date
    Public maxyear As Date
    Public eHUBConf As New Dictionary(Of String, String) ' Used to get the shift setup
    Public MonSetup As New Dictionary(Of String, String) ' Used to get the shift setup
    Public ShiftSetup As New Dictionary(Of String, String)
    Public before2012 As Boolean = False
    Public CSI_Lib As New CSI_Library.CSI_Library
    Public stat(3) As String
    Public chemin_bd As String
    Public chemin_eNET As String
    Public chemin_Color As String
    Public Image1 As Image, bySetup As Boolean = False
    Public machine_list As New Dictionary(Of String, String)

    Public SRV_Version As Integer = 0

    Public First_date As Date

    'Public srv As New ServiceNT
    'Public COM_ As New Communication

    'Public objexcel As Microsoft.Office.Interop.Excel.Application
    'Public xlBook As Microsoft.Office.Interop.Excel.Workbook
    'Public xlsheet As Microsoft.Office.Interop.Excel.Worksheet
    Public week_ As String = 15
    Public year_ As String = 1
    Public activated_ As Boolean = False
    Public first_creation_of_the_DB As Boolean = False
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath

    Public continue_updating As Boolean = True

    Private Sub Reporting_application_FormClosing(sender As Object, e As FormClosingEventArgs) _
     Handles MyBase.FormClosing
        Try
            If first_creation_of_the_DB = True Then
                MessageBox.Show("The database is being created, please wait", MsgBoxStyle.SystemModal + MsgBoxStyle.Critical)
                e.Cancel = True   'stop the form from closing
            End If

            continue_updating = False
            'COM_.Continue_clt_service = False
            CSI_Library.CSI_Library.update = False

        Catch ex As Exception
            CSI_Lib.LogClientError("DB Update pb : " & ex.Message)
        End Try

        '   UserLogin.Close()
    End Sub




    '-----------------------------------------------------------------------------------------------------------------------
    ' Bouton Configuration
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub ConfigurationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfigurationToolStripMenuItem.Click
        bySetup = True
        SetupForm.MdiParent = Me
        SetupForm.Show()


    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' Bouton About
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        AboutBox1.Show()
    End Sub



    Private Sub Reporting_application_Resize0(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        Me.SuspendLayout()
    End Sub



    Private Sub Reporting_application_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        Me.Update()
        Me.ResumeLayout()
        Me.Refresh()

        Config_report.Location = New System.Drawing.Point(0, 0)
        Report_BarChart.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, 0)
        Report_BarChart.Height = Config_report.Height
        Report_BarChart.Refresh()
        Me.PerformLayout()
        ' Machine_util_det.PerformLayout()
        ''  Report_PieChart.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, 25)

        'MsgBox("config_rep_h : " & Config_report.Height & vbCrLf &
        '       "Report_BCt_h : " & Report_BarChart.Height & vbCrLf &
        '       "GrpBox_evo_h : " & Report_BarChart.GroupBox_evo.Height & vbCrLf)




    End Sub
    Private Sub CreateFV_with_license()

        Dim folderpath As String = CSI_Library.CSI_Library.ClientRootPath & "\sys" '\Setup_.csys"
        Dim lib_ As New CSI_Library.CSI_Library
        Dim licenseinfos As String()
        licenseinfos = lib_.CheckLic()
        Dim expiredtext As String = ""
        Dim expirationdate As DateTime

        Dim i As Integer = 0

        Try
            If licenseinfos(0) = "NOK" And Directory.Exists(rootPath) Then
                MsgBox("No license found. Restart the application to get a new license")

                Dim filesys = CreateObject("Scripting.FileSystemObject")
                If filesys.folderexists(CSI_Library.CSI_Library.ClientRootPath) Then
                    filesys.deletefolder(CSI_Library.CSI_Library.ClientRootPath)
                End If

                Environment.Exit(1)

            End If

            If (File.Exists(rootPath & "\sys\CSIFv_.csys")) Then
                Using r As StreamReader = New StreamReader(rootPath & "\sys\CSIFv_.csys", False)
                    Dim lib___ As New CSI_Library.CSI_Library
                    Dim tmp As String() = Split(lib___.AES_Decrypt(r.ReadLine, "4Solutions"), ":")
                    i = tmp(0)

                    r.Close()
                End Using
            End If
        Catch ex As Exception
            CSI_Lib.LogClientError("lic pb : " & ex.Message)
        End Try


        If (licenseinfos(0) = "ok") Then
            'std version
            Try
                expirationdate = DateTime.Parse(licenseinfos(1))
                i = licenseinfos(2)
                Using writer As StreamWriter = New StreamWriter(folderpath & "\CSIFv_.csys")
                    'Dim lib__ As New CSI_Library.CSI_Library

                    writer.Write(Login.AES_Encrypt(i & ":-----", "4Solutions"))
                    writer.Close()
                End Using
            Catch ex As Exception
                CSI_Lib.LogClientError("lic pb : " & ex.Message)
            End Try


            Dim dayremaining As Double = DateDiff(DateInterval.Day, DateTime.Now, expirationdate)
            If (dayremaining > 30) Then
                ToolStripStatusLabel1.ForeColor = Color.Black
            End If
            expiredtext = "Your license expires in " & dayremaining.ToString() & " days"
            If (dayremaining > 365) Then
                expiredtext = ""
            End If

        ElseIf (licenseinfos(0) = "EXP") Then
            'lite version
            'MessageBox.Show("Your license is expired, you are now using CSIFlex lite", MsgBoxStyle., "License expired")
            'Dim newlicens As New RenewLicense
            'newlicens.ShowDialog()

            Try
                expirationdate = DateTime.Parse(licenseinfos(1))

                Using writer As StreamWriter = New StreamWriter(folderpath & "\CSIFv_.csys", False)
                    'Dim lib__ As New CSI_Library.CSI_Library

                    writer.Write(Login.AES_Encrypt("1:-----", "4Solutions"))
                    writer.Close()
                End Using
            Catch ex As Exception
                CSI_Lib.LogClientError("lic pb : " & ex.Message)
            End Try

            'expiredtext = "Your license is expired since " + DateDiff(DateInterval.Day, expirationdate, DateTime.Now).ToString() + " days"
            expiredtext = "You are now using CSI Flex Lite."
        End If

        'Write remaining days before Expiration
        ToolStripStatusLabel1.Text = expiredtext
    End Sub

    Dim last_win_stat As FormWindowState = FormWindowState.Maximized

    Private Sub reporting_application_size_change(sender As Object, e As EventArgs) Handles MyBase.SizeChanged



        Config_report.Height = Me.Height - 112

        Report_BarChart.Height = Config_report.Height
        Report_BarChart.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Size.Width, 0)


        Report_PieChart.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, 0)
        Report_PieChart.Height = Config_report.Height

        Machine_util_det.Location = New System.Drawing.Point(Report_BarChart.Size.Width + Config_report.Size.Width, Report_BarChart.Location.Y)
        Machine_util_det.Height = Report_BarChart.Height

    End Sub


    Private Sub write_version(var As String)
        Try
            Using writer As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.ClientRootPath & "\sys\CSIFLEX_VERSION.csys", False)
                writer.Write(var)
                writer.Close()
            End Using
        Catch ex As Exception
            CSI_Lib.LogClientError("version write pb: " & ex.Message)
        End Try
    End Sub
    Private Function read_version() As String
        Try
            Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\CSIFLEX_VERSION.csys", False)
                Return reader.ReadLine()
            End Using
        Catch ex As Exception
            Return Nothing
            CSI_Lib.LogClientError("version read pb : " & ex.Message)
        End Try
    End Function


    '-----------------------------------------------------------------------------------------------------------------------
    '  form load
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Reporting_application_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CSI_Library.CSI_Library.isServer = False



        Dim splash As New SplashScreen
        splash.Show()


        Me.Height = Screen.PrimaryScreen.Bounds.Height - 200
        Me.Width = Screen.PrimaryScreen.Bounds.Width
        Me.WindowState = FormWindowState.Maximized


        ToolStripStatusLabel2.Text = ""


        'DELETE FILE TO FORCE CHECK THE LICENSE

        CreateFV_with_license()

        If File.Exists(rootPath & "\sys\CSIFv_.csys") Then

            Try
                Using r As StreamReader = New StreamReader(rootPath & "\sys\CSIFv_.csys", False)
                    Dim lib___ As New CSI_Library.CSI_Library
                    Dim tmp As String() = Split(lib___.AES_Decrypt(r.ReadLine, "4Solutions"), ":")
                    Welcome.CSIF_version = tmp(0)

                    r.Close()
                End Using

            Catch ex As Exception
                MessageBox.Show("Unable to read the CSIFlex version ") ' & ex.Message)
                CSI_Lib.LogClientError("Unable to read the CSIFlex version " & ex.Message)
            End Try

        Else
            Welcome.TopMost = True
            If Not (Welcome.ShowDialog() = Forms.DialogResult.OK) Then
                'Dispose()
                Environment.Exit(0)
            End If
            Welcome.TopMost = False
        End If

        ' Set the color in the MDI client.
        For Each ctl As Control In Me.Controls
            If TypeOf ctl Is MdiClient Then
                ' ctl.BackColor = Me.BackColor
                ctl.BackColor = Color.FromArgb(237, 239, 240)
            End If
        Next ctl

        'New instance of the lib
        CSI_Lib = New CSI_Library.CSI_Library


        'Licence Check:====================================================================
        activated_ = True
        'registration for all version
        'If Welcome.CSIF_version <> 1 Then
        Dim data__ As String = CSI_Lib.CheckLic(0)
        If data__ <> "ok" Then
            If data__ = "EXP" Then
                MessageBox.Show("The Licence has expired.")
                Welcome.CSIF_version = 1
            Else
                System.Console.WriteLine(Welcome.CSIF_version)
                If (Login.ShowDialog() = Forms.DialogResult.OK) Then
                Else
                    Environment.Exit(0)
                End If
            End If

        Else
            activated_ = True
        End If


        If Welcome.CSIF_version = 3 Then
            UserLogin.ShowDialog()
        End If

        If Not (Welcome.CSIF_version = CSI_Lib.CheckLic(2)) Then
            MessageBox.Show("Your license is invalid. Please get a new license")
            Try

                Dim path As String = CSI_Library.CSI_Library.ClientRootPath

                File.Delete(path & "\wincsi.dl1")
                'File.Delete(rootpath.DirectoryName & "\sys\")

                Environment.Exit(1)
            Catch ex As Exception
                MsgBox("The application needed to be restart in admin to change license")
                CSI_Lib.LogClientError("The application needed to be restart in admin to change license" & ex.Message)
                Environment.Exit(1)
            End Try

        End If
        'disable std feature if version is lite
        If Welcome.CSIF_version = 1 Then

            ExportToolStripMenuItem.Enabled = False
            ReportToolStripMenuItem.Enabled = False
        End If

        ' Set the color to the menu strip
        MenuStrip1.ForeColor = Color.Black
        ' Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.BackColor = Color.FromArgb(237, 239, 240)
        ' check if the sys directory exist, and create it if no:
        Dim dir_ok As String = CSI_Lib.dir_(rootPath & "\sys\")
        If dir_ok <> "ok" Then
            MessageBox.Show("CSIFLEX has incountered an Error while creating the sys repertory in the CSIFLEX folder : " & dir_ok)
            GoTo End2
        Else
            If File.Exists(rootPath & "\Setup_.sys") Then
                File.Move(rootPath & "\Setup_.sys", rootPath & "\sys\Setup_.csys")
            End If
            If File.Exists(rootPath & "\Setupdb_.sys") Then
                File.Move(rootPath & "\Setupdb_.sys", rootPath & "\sys\Setupdb_.csys")
            End If
            If File.Exists(rootPath & "\Setupdt_.sys") Then
                File.Move(rootPath & "\Setupdt_.sys", rootPath & "\sys\Setupdt_.csys")
            End If
            If File.Exists(rootPath & "\target_.sys") Then
                File.Move(rootPath & "\target_.sys", rootPath & "\sys\target_.csys")
            End If
            If File.Exists(rootPath & "\targetColor_.sys") Then
                File.Move(rootPath & "\targetColor_.sys", rootPath & "\sys\targetColor_.csys")
            End If
            If File.Exists(rootPath & "\Networkenet_.sys") Then
                File.Move(rootPath & "\Networkenet_.sys", rootPath & "\sys\Networkenet_.csys")
            End If
        End If

        'If activated_ = False Then GoTo End2
        'Else
        'activated_ = True
        'End If
        'activated_ = True ' For the show
        '==================================================================================


        ' check the framwork version.======================================================
        Dim ok As Boolean = CSI_Lib.verif_FRAMEWORK()
        If ok = True Then
        Else
            MessageBox.Show("Please instal the Microsoft .NET Framework 4+ ")
            File.WriteAllBytes(rootPath & "\sys\" + "netFrameWork_4_5_2.exe", My.Resources.NetFrameWork_WebInstaller)

            Dim startInfo As New ProcessStartInfo()
            startInfo.FileName = rootPath & "\sys\" + "netFrameWork_4_5_2.exe"
            Process.Start(startInfo)


            GoTo End2
        End If
        '==================================================================================



        'Check Config Data ================================================================
        Try
            System.Console.WriteLine(Welcome.CSIF_version)

            If (Welcome.CSIF_version <> 3 Or Welcome.CSIF_version = 3) Then
                If Exists(rootPath & "\sys\Setup_.csys") Then
                    Using reader As StreamReader = New StreamReader(rootPath & "\sys\Setup_.csys")
                        chemin_eNET = reader.ReadLine()
                    End Using
                Else
                    MessageBox.Show("Please specify the eNET folder")
                    SetupForm.Show()
                    GoTo fin
                End If
            End If

            If (Welcome.CSIF_version <> 3) Then
                Try
                    File.Copy(chemin_eNET & "\_SETUP\MonList.sys", rootPath & "\sys\Monlist.csys", True)
                    File.Delete(rootPath & "\sys\_TreeView.xml")
                Catch ex As Exception
                    MessageBox.Show("Unable to save the eNET machine list. Verify if the file exist in your eNET path.")
                End Try
            End If





            If Exists(rootPath & "\sys\Setupdb_.csys") Then
                Using reader As StreamReader = New StreamReader(rootPath & "\sys\Setupdb_.csys")
                    chemin_bd = reader.ReadLine()
                End Using
            ElseIf (Welcome.CSIF_version <> 3) Then
                MessageBox.Show("Please specify the Database folder")
                SetupForm.Show()
                GoTo fin
            End If



            If Welcome.CSIF_version <> 1 And Welcome.CSIF_version <> 2 Then

                Dim db_authPath As String = Nothing
                If (File.Exists(rootPath + "/sys/SrvDBpath.csys")) Then
                    Using reader As New StreamReader(rootPath + "/sys/SrvDBpath.csys")
                        db_authPath = reader.ReadLine()
                    End Using
                End If
                Dim connectionString As String
                connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_auth;" + CSI_Library.CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

                Dim cnt As MySqlConnection = New MySqlConnection(connectionString)
                cnt.Open()
                Dim da As MySqlCommand = New MySqlCommand("SELECT machines FROM Users WHERE Username_='" & username_ & "'", cnt)
                Dim r As MySqlDataReader = da.ExecuteReader()
                Using w As StreamWriter = New StreamWriter(rootPath & "\sys\monlist.csys")
                    w.WriteLine("_MT_Assets : ")
                    r.Read()
                    Dim tmp As String = r.GetString("machines")


                    Dim list__ As String() = Split(tmp, ", ")
                    For Each machine__ As String In list__
                        If (machine__ <> "") Then
                            w.WriteLine(machine__)
                        End If
                    Next
                End Using
                cnt.Close()

            End If


            If (Welcome.CSIF_version = 3) Then
                Dim machines As String()

                Using reader As StreamReader = New StreamReader(rootPath & "\sys\monlist.csys")
                    Dim i As Integer = 0
                    While Not reader.EndOfStream
                        ReDim Preserve machines(i + 1)
                        machines(i) = reader.ReadLine()
                        i = i + 1
                    End While
                End Using

                Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\machine_list_.csys", False)
                    For Each machine In machines
                        If machine IsNot Nothing Then
                            If machine.StartsWith("_MT") Or machine.StartsWith("_ST") Then
                                'Nothing
                            Else
                                writer.WriteLine(machine & "," & "eNET,1")
                            End If
                        End If
                    Next

                End Using
            Else 'If Not Exists(rootPath & "\sys\machine_list_.csys") Then


                Dim machines As String()

                machines = CSI_Lib.LoadMachines()

                Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\machine_list_.csys", False)
                    For Each machine In machines
                        If machine IsNot Nothing Then
                            If machine.StartsWith("_MT") Or machine.StartsWith("_ST") Then
                                'Nothing
                            Else
                                writer.WriteLine(machine & "," & "eNET,1")
                            End If
                        End If
                    Next

                End Using
            End If

            If Exists(rootPath & "\sys\Setupdt_.csys") Then
                Using reader As StreamReader = New StreamReader(rootPath & "\sys\Setupdt_.csys")
                    week_ = reader.ReadLine()
                    year_ = reader.ReadLine()
                    reader.Close()
                End Using
            Else
                Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Setupdt_.csys")
                    writer.Write("15")
                    week_ = "15"
                    year_ = "0"
                    writer.Close()
                End Using
            End If



        Catch ex As Exception
            MsgBox("Unable to load config files")
            CSI_Lib.LogClientError("Unable to load config files : " & ex.Message)
            GoTo fin
        End Try


        If IsNothing(chemin_bd) Then
            SetupForm.Show()
            GoTo fin
        End If
        If Dir(chemin_bd, vbDirectory) = "" And Welcome.CSIF_version <> 3 Then
            SetupForm.Show()
            GoTo fin
        Else

            Try

                If (Welcome.CSIF_version <> 3) Then
                    Config_report.Enabled = False
                    MenuStrip1.Enabled = False
                    SynchroDB.Show()
                    ToolStripStatusLabel2.Text = "Database update in progress"
                    'If Not System.IO.File.Exists(chemin_bd & "\CSI_Database.mdb") Then Form12.Label1.Text = "CSIFLEX is updating the monitoring data, please wait"
                    BGW_SQLiteUpdate.RunWorkerAsync()
                End If



            Catch ext As Exception
                MsgBox("Unable to update the database")
                CSI_Lib.LogClientError("Unable to load config files : " & ext.Message)
            End Try

        End If

        Report_PieChart.MdiParent = Me
        Config_report.MdiParent = Me
        Machine_util_det.MdiParent = Me

        SynchroDB.MdiParent = Me


        Me.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.ResizeRedraw, True)

        SetStyle(ControlStyles.AllPaintingInWmPaint, True)


fin:
End2:

        If activated_ = False Then Me.Close()


        Dim lic As Integer = Welcome.CSIF_version
        If Welcome.CSIF_version <> 1 Then

            Dim srv_thread As New Thread(AddressOf fct_get_enet_livestatus)

            srv_thread.Name = "CSIFLEX Real time monitoring thread"

            srv_thread.Start()
        End If

        If Welcome.CSIF_version = 3 Then

            Try
                Dim db_authPath As String = Nothing
                Dim directory As String = CSI_Lib.getRootPath()
                If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                    Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                        db_authPath = reader.ReadLine()
                    End Using
                End If
                Dim connectionString As String
                connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + CSI_Library.CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

                CSI_Library.CSI_Library.loadingasCON = CSI_Lib.GetLoadingAsCON(connectionString)
            Catch ex As Exception
                CSI_Lib.LogClientError("pb with the db path : " & ex.Message)
            End Try

        End If

        ' Config_report.Size = New System.Drawing.Size(272, Me.Height - 100)

        Me.MinimizeBox = True

        'loadStartupParams()

        If Welcome.CSIF_version = 3 Then
            If Not File.Exists(rootPath & "\sys\Networkenet_.csys") Then
                Dim add_ip As String
                Try
                    If Exists(rootPath & "\sys\SrvDBpath.csys") Then
                        Using reader As StreamReader = New StreamReader(rootPath & "\sys\SrvDBpath.csys")
                            add_ip = reader.ReadLine()
                            reader.Close()
                        End Using
                    End If


                    Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Networkenet_.csys")
                        writer.Write(add_ip + ":8080")
                        writer.Close()
                    End Using

                    Dim colors__ As DataTable

                    If Not File.Exists(rootPath & "\sys\Color_list_.csys") Then
                        colors__ = CSI_Lib.Read_colors_from_database()
                        Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Color_list_.csys")
                            For Each row In colors__.Rows
                                Dim s As String = row.Item(0).ToString()
                                writer.WriteLine(System.Drawing.ColorTranslator.ToWin32(System.Drawing.ColorTranslator.FromHtml(row.Item(1).ToString())).ToString() + "," + row.item(0).ToString())
                            Next
                            writer.Close()
                        End Using


                    Else
                        File.Delete(rootPath & "\sys\Color_list_.csys")
                        colors__ = CSI_Lib.Read_colors_from_database()
                        Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Color_list_.csys")
                            For Each row In colors__.Rows
                                Dim s As String = row.Item(0).ToString()
                                writer.WriteLine(System.Drawing.ColorTranslator.ToWin32(System.Drawing.ColorTranslator.FromHtml(row.Item(1).ToString())).ToString() + "," + row.item(0).ToString())
                            Next
                            writer.Close()
                        End Using
                    End If

                    chemin_Color = rootPath & "\sys\Color_list_.csys"
                    Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\SetupColor_.csys")
                        writer.Write(rootPath & "\sys\Color_list_.csys")
                        writer.Close()
                    End Using
                Catch ex As Exception
                    CSI_Lib.LogClientError("pb with the config files : " & ex.Message)
                End Try
            End If
        End If

        Try
            If File.Exists(rootPath & "\sys\Color_list_.csys") Then
                File.Delete(rootPath & "\sys\Color_list_.csys")
                Dim colors__ As DataTable = CSI_Lib.Read_colors_from_database()
                Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Color_list_.csys")
                    For Each row In colors__.Rows
                        Dim s As String = row.Item(0).ToString()
                        writer.WriteLine(System.Drawing.ColorTranslator.ToWin32(System.Drawing.ColorTranslator.FromHtml(row.Item(1).ToString())).ToString() + "," + row.item(0).ToString())
                    Next
                    writer.Close()
                End Using
            Else
                Dim colors__ As DataTable = CSI_Lib.Read_colors_from_database()
                Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Color_list_.csys")
                    For Each row In colors__.Rows
                        Dim s As String = row.Item(0).ToString()
                        writer.WriteLine(System.Drawing.ColorTranslator.ToWin32(System.Drawing.ColorTranslator.FromHtml(row.Item(1).ToString())).ToString() + "," + row.item(0).ToString())
                    Next
                    writer.Close()
                End Using
            End If

        Catch ex As Exception
            MsgBox("Could not update the Status colors form the CSIFlex database")
        End Try

        First_date = set_firstdate()

        If Welcome.CSIF_version = 3 Then ' if not version version 3,  Config_report.Show() in SynchroDB after close event
            Config_report.Show()
            check_srv_version.RunWorkerAsync()
        End If

        If Welcome.CSIF_version = 3 Then


            If check_csif_srv_version() Then



                Dim task_rm As Task = Task.Run(Sub()
                                                   Load_rm_port()
                                               End Sub)


            End If
            SRV_UDT_NEEDED = False
        Else
            SRV_UDT_NEEDED = True
        End If


    End Sub

    Public SRV_UDT_NEEDED As Boolean = False
    Private Function check_csif_srv_version() As Boolean
        Try
            Dim db_authPath As String
            If (File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
                Using streader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath + "\sys\SrvDBpath.csys")
                    db_authPath = streader.ReadLine()
                End Using
            End If

            Dim cnt As MySqlConnection = New MySqlConnection("SERVER=" + db_authPath + ";" + "DATABASE=csi_auth;" + CSI_Library.CSI_Library.MySqlServerBaseString)
            cnt.Open()
            Dim sqlitecmd As New MySqlCommand("Select version FROM csi_database.tbl_CSIFLEX_VERSION", cnt)
            Dim table_tablenames As New DataTable
            Dim reader As MySqlDataReader = sqlitecmd.ExecuteReader
            table_tablenames.Load(reader)

            If table_tablenames.Rows(0).Item(0) >= 1865 And table_tablenames.Rows(0).Item(0) <> 1894 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox("Could not check the server version")
            CSI_Lib.LogClientError(ex.Message)
        End Try

    End Function

    Private Sub Load_rm_port()
        Try
            Dim port_ As String = "8008"

            Dim db_authPath As String
            If (File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
                Using streader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath + "\sys\SrvDBpath.csys")
                    db_authPath = streader.ReadLine()
                End Using
            End If


            Dim cnt As MySqlConnection = New MySqlConnection("SERVER=" + db_authPath + ";" + "DATABASE=csi_auth;" + CSI_Library.CSI_Library.MySqlServerBaseString)
            cnt.Open()
            If cnt.State = ConnectionState.Open Then


                Dim query As String = "select port from  CSI_database.tbl_RM_Port"

                Dim cmd As New MySqlCommand(query, cnt)
                Dim dtrdr As MySqlDataReader


                dtrdr = cmd.ExecuteReader
                dtrdr.Read()
                port_ = dtrdr.Item(0)
                cnt.Close()
            End If


            If File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys") Then

                'Using rdr As StreamReader = New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys")
                '    port_ = rdr.ReadLine
                '    rdr.Close()
                'End Using
                File.Delete(CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys")
            End If
            Using wrt As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys")
                wrt.WriteLine(port_)
                wrt.Close()
            End Using


        Catch ex As Exception
            MsgBox("Could not load the rainmeter port")
            CSI_Lib.LogClientError(ex.Message)
        End Try

    End Sub

    Public Function set_firstdate()

        Dim First_date As Date

        Try
            If File.Exists(rootPath & "\sys\firstdate_.csys") Then
                'Select Case FIRST(column_name) FROM table_name
                Using reader As StreamReader = New StreamReader(rootPath & "\sys\firstdate_.csys")
                    First_date = DateTime.Parse(reader.ReadLine().ToString())
                    reader.Close()
                End Using
                Dim _date_ As Date = "#12/16/2000 12:00:52 AM#"
                If First_date < _date_ Then

                    File.Delete(rootPath & "\sys\firstdate_.csys")
                    First_date = CSI_Lib.Get_firt_date_database(Welcome.CSIF_version)

                    Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\firstdate_.csys")
                        writer.WriteLine(First_date.Date.ToString())
                        writer.Close()
                    End Using
                End If

            Else
                First_date = CSI_Lib.Get_firt_date_database(Welcome.CSIF_version)
                Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\firstdate_.csys")
                    writer.WriteLine(First_date.Date.ToString())
                    writer.Close()
                End Using
            End If
            Return First_date
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub loadStartupParams()
        'report dates
        If My.Computer.FileSystem.FileExists(rootPath & "\sys\StartupConfig_.csys") Then
            Using r As StreamReader = New StreamReader(rootPath & "\sys\StartupConfig_.csys", False)
                'Dim lib___ As New CSI_Library.CSI_Library
                Dim tmp As String() = Split(r.ReadLine, ";")
                r.Close()
                If (tmp(0).Contains("Report")) Then
                    Dim nudval As String = tmp(1).Substring(tmp(1).IndexOf("=") + 1)
                    Dim daystoreport As Integer = Int32.Parse(nudval)
                    Dim enddate As Date = Config_report.DTP_EndDate.Value
                    Config_report.DTP_StartDate.Value = Config_report.DTP_StartDate.Value.AddDays(-daystoreport)
                    Config_report.DTP_EndDate.Value = enddate

                    Config_report.BTN_Create.PerformClick()
                Else
                    Config_report.BTN_Dashboard.PerformClick()
                End If
            End Using
        End If
    End Sub


    Private Sub fct_get_enet_livestatus()
        Dim dt As New DataTable
        Dim dic As New Dictionary(Of String, EMachine)(StringComparer.CurrentCultureIgnoreCase)
        While continue_updating

            Try
                dt = GetEnetPage() 'clt.Run(GetEnetIp())
            Catch ex As Exception
                Call CSI_Lib.LogServerError("ERROR getting data from http server: " & ex.Message & " - " & getIPeNET(), 0)
            End Try

            Try
                dic.Clear()
                If Not (dt Is Nothing) Then
                    For Each row As DataRow In dt.Rows
                        Try
                            dic.Add(row.Item("machine"), New EMachine(row, "enet"))
                        Catch ex As Exception
                            Call CSI_Lib.LogServiceError("ERROR machine: " & row.Item("machine") & Environment.NewLine & row.Item("status") & Environment.NewLine & row.Item("PartNumber") & Environment.NewLine & row.Item("PartNumber") & Environment.NewLine & row.Item("CycleCount") & Environment.NewLine & "Message:" & ex.Message, 0)
                        End Try
                    Next
                End If
                'Return dic
                GlobalVariables.ListOfMachine = dic
            Catch ex As Exception
                Call CSI_Lib.LogServerError("Error getting live status from enet:" & ex.Message, 0)
            End Try

            Thread.Sleep(300)
        End While
    End Sub

    Private Function GetEnetPage() As DataTable
        Try
            'Format URI
            Dim enetip As String = getIPeNET()
            If enetip.StartsWith("http") Then
            Else
                enetip = "http://" & enetip
            End If

            Dim targetURI As New Uri(enetip), page As String = ""
            Dim Request As System.Net.HttpWebRequest
            Dim table As New DataTable

            table.Columns.Add("machine", GetType(String))
            table.Columns.Add("Shift", GetType(String))
            table.Columns.Add("status", GetType(String))
            table.Columns.Add("PartNumber", GetType(String))
            table.Columns.Add("Condition", GetType(String))
            table.Columns("Condition").AllowDBNull = True
            table.Columns.Add("CycleCount", GetType(String))
            table.Columns.Add("LastCycle", GetType(String))
            table.Columns.Add("CurrentCycle", GetType(String))
            table.Columns.Add("ElapsedTime", GetType(String))
            table.Columns.Add("feedOverride", GetType(String))
            ' table.Columns.Add("palette", GetType(String))

            Thread.Sleep(1000)

            Try


                Request = DirectCast(HttpWebRequest.Create(targetURI), System.Net.HttpWebRequest)
                With Request
                    .Timeout = 6000
                    .Method = "POST"
                    .KeepAlive = True
                    .ContentType = "text/xml"
                    .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"
                End With

                If (Request.GetResponse().ContentLength > 0) Then
                    Dim str As New System.IO.StreamReader(Request.GetResponse().GetResponseStream())
                    page = (str.ReadToEnd)
                    str.Close()
                End If


            Catch ex As Exception
                Call CSI_Lib.LogServerError("ERROR during requesting: " & ex.Message, 0)
            End Try

            html_parser(table, page)
            Return table


        Catch ex As Exception
            Call CSI_Lib.LogServerError("ERROR getting data from http server: " & ex.Message, 0)
            Return Nothing
        End Try
    End Function


    Private Structure Data
        Public status As String
        Public shift As String
        Public PartNumber As String
        Public CycleCount As String
        Public LastCycle As String
        Public CurrentCycle As String
        Public ElapsedTime As String
        Public feedOverride As String
    End Structure
    Private Sub html_parser(ByRef table As DataTable, html_file As String)
        Dim str As String, str2 As String(), row2 As String()
        Try
            If Not IsNothing(html_file) Then
                If html_file <> "" Then
                    If (Regex.Match(html_file, "DNC Machine Monitoring").Success) Then ' check if it's the right page

                        str = Regex.Replace(html_file, "<td bgcolor=(.{9})>", "")
                        str = Regex.Replace(str, "<font color=(.{9})>", "")
                        str = Regex.Replace(str, "<td bgcolor=(.{9})'>", "")
                        str = Regex.Replace(str, "<font color='#0'>", "")
                        str = Regex.Replace(str, "</td>", "")

                        str2 = Split(str, "</table>")
                        str2 = Split(str2(2), "<tr align=""center"">")

                        Dim first As Boolean = True
                        For Each row In str2
                            If first = True Then
                                first = False
                            Else
                                If (row(1) <> "<") Then
                                    Dim DATA As New Data
                                    Dim r As DataRow = table.NewRow
                                    row2 = Split(row, "</font>")

                                    r("Machine") = row2(0).Remove(row2(0).Count - 5, 5).Replace(vbCrLf, "")
                                    r("ElapsedTime") = row2(6)
                                    r("CurrentCycle") = row2(5)
                                    r("shift") = (row2(0)(row2(0).Count - 1))
                                    r("status") = row2(1)

                                    r("feedOverride") = row2(7).Replace(" ", "").Replace("</tr>", "") 'Replace(row2(7).Replace(" ", ""), "</tr>", "")

                                    r("LastCycle") = row2(4)
                                    r("CycleCount") = row2(3)
                                    r("PartNumber") = row2(2)
                                    If r("PartNumber") = " " Then r("PartNumber") = ""
                                    table.Rows.Add(r)
                                End If
                            End If
                        Next
                        'Return table
                    End If
                    'Return Nothing
                End If
                'Return Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    '============================================================================================================================
    '= Gives the ip of the eNET http server 
    '============================================================================================================================
    Private Function getIPeNET()
        Try
            Dim ip_ As String = Nothing
            Dim path As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
            Dim directory As String = CSI_Lib.getRootPath()

            If (File.Exists(directory & "\sys\Networkenet_.csys")) Then
                Using reader As StreamReader = New StreamReader(directory & "\sys\Networkenet_.csys")
                    ip_ = reader.ReadLine



                    reader.Close()
                End Using
            End If


            Return ip_
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Function ExcelConnection() As String

        Dim strConnect As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\_MACHINE_2013.xlsx; Extended Properties = Excel 12.0 Xml;HDR=YES ; Format=xlsx;"

        Return strConnect
    End Function


    '-----------------------------------------------------------------------------------------------------------------------
    ' 1st button Reporting_application
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub DailyReportToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Config_report.Show()
    End Sub



    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Report_PieChart.Show()
    End Sub


    Private Sub NonModalMessageBox()
        MessageBox.Show("CSIFLEX has generated a database of 1 year, and will continue to generate in background. You can use it until it finishes. ", MsgBoxStyle.SystemModal)
        'StatusStrip1.Text = "Database optimisation in progres..."
    End Sub


    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BGW_SQLiteUpdate.RunWorkerCompleted
        Config_report.Enabled = True
        MenuStrip1.Enabled = True
        First_date = set_firstdate()

        If File.Exists(rootPath & "\sys\Color_list_.csys") Then
            File.Delete(rootPath & "\sys\Color_list_.csys")
            Dim colors__ As DataTable = CSI_Lib.Read_colors_from_database()
            Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Color_list_.csys")
                For Each row In colors__.Rows
                    Dim s As String = row.Item(0).ToString()
                    writer.WriteLine(System.Drawing.ColorTranslator.ToWin32(System.Drawing.ColorTranslator.FromHtml(row.Item(1).ToString())).ToString() + "," + row.item(0).ToString())
                Next
                writer.Close()
            End Using
        End If


        SynchroDB.Close()
        ToolStripStatusLabel2.Text = ""
        '   Form12.Close()

        ''''ALL FORMS ARE LOADED FROM HERE
        '''NEED TO CALL loadStartupParams here otherwise the BTN_Create.PerformClick is not registered
        loadStartupParams()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Config_report.Show()
    End Sub


    Private Sub ConfigurationToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConfigurationToolStripMenuItem1.Click
        ConfigurationToolStripMenuItem_Click(sender, e)
    End Sub


    'Public Function installReportViewer() As Boolean
    '    Dim StartInfo As New System.Diagnostics.ProcessStartInfo
    '    Dim process_ As New Process
    '    Dim process2_ As New Process


    '    Dim path As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
    '    Dim commandSTRING As String

    '    If Not File.Exists(path & "\repview_needs.zip") Then
    '        IO.File.WriteAllBytes(path & "\repview_needs.zip", CSI_Library.My.Resources.repview_needs)
    '    End If

    '    If Not (File.Exists(path & "\SQLSysClrTypes.msi") And File.Exists(path & "\ReportViewer.msi")) Then
    '        ZipFile.ExtractToDirectory(path & "\repview_needs.zip", path)
    '    End If



    '    StartInfo.FileName = "cmd" 'starts cmd window
    '    StartInfo.RedirectStandardInput = True
    '    StartInfo.RedirectStandardOutput = True
    '    StartInfo.UseShellExecute = False 'required to redirect
    '    process_.StartInfo = StartInfo
    '    process_.Start()


    '    process2_.StartInfo = StartInfo
    '    process2_.Start()

    '    Dim SR As System.IO.StreamReader = process_.StandardOutput
    '    Dim SW As System.IO.StreamWriter = process_.StandardInput

    '    Dim SR2 As System.IO.StreamReader = process2_.StandardOutput
    '    Dim SW2 As System.IO.StreamWriter = process2_.StandardInput


    '    commandSTRING = "msiexec /i """ & path & "\SQLSysClrTypes.msi"" /quiet /qn /norestart /log """ & path & "/loginstallSqlClr.log"""
    '    SW.WriteLine(commandSTRING)
    '    SW.WriteLine("exit")
    '    Thread.Sleep(500)

    '    commandSTRING = "msiexec /i """ & path & "\ReportViewer.msi"" /quiet /qn /norestart /log """ & path & "/loginstallrepview.log"""
    '    SW2.WriteLine(commandSTRING)
    '    SW2.WriteLine("exit")


    '    Return True
    'End Function

    Public Function IsInstalled() As Boolean
        Dim uninstallKey As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"
        Dim ReportViewerExist As Boolean = False
        Using rk As RegistryKey = Registry.LocalMachine.OpenSubKey(uninstallKey)
            For Each skName As String In rk.GetSubKeyNames()
                Using sk As RegistryKey = rk.OpenSubKey(skName)

                    If sk.GetValue("DisplayName") & "  " & sk.GetValue("DisplayVersion") = "Microsoft Report Viewer 2012 Runtime  11.1.3452.0" Then
                        ReportViewerExist = True
                    End If

                End Using
            Next
        End Using

        Return ReportViewerExist

    End Function

    Public Sub New()
        InitializeComponent()


        If Not (IsInstalled()) Then

            'installReportViewer()
            CSI_Lib.installReportViewer()
        End If

    End Sub

    Private Shared Function CurrentDomain_AssemblyResolve(sender As Object, args As ResolveEventArgs) As System.Reflection.Assembly
        Return EmbeddedAssembly.[Get](args.Name)
    End Function


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim about As New AboutBox1
        about.ShowDialog()
    End Sub

    Private Sub AvailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AvailToolStripMenuItem.Click

        If setupForm_Singleton Is Nothing Then
            setupForm_Singleton = New Reporting_availability()
        End If

        setupForm_Singleton.Show()
        setupForm_Singleton.Focus()
    End Sub


    Private Sub PartsNoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PartsNoToolStripMenuItem.Click

        Dim test As New Reporting_partsNumber()
        test.Show()
    End Sub

    Private Sub RawDataToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Dim folderDlg As New SaveFileDialog
        folderDlg.ShowDialog()

        If (folderDlg.FileName <> "") Then
            generateCSVaccordingToConfig_report(folderDlg.FileName())
        End If


    End Sub




    Private Sub generateCSVaccordingToConfig_report(filePath As String)


        setData(filePath)
    End Sub

    Private Sub generateCSV_file(filepath As String)
        setData(filepath)
    End Sub

    Public Function ToCSV(table As DataTable, filePath As String, firstFill As Boolean)

        Using sw As StreamWriter = New StreamWriter(filePath, Not firstFill)

            Dim byeList As New List(Of Byte)

            Dim result As New System.Text.StringBuilder()



            If (firstFill = True) Then
                For i As Integer = 0 To table.Columns.Count - 1

                    result.Append(table.Columns(i).ColumnName)
                    result.Append(If(i = table.Columns.Count - 1, vbLf, ","))

                Next
                sw.Write(result)
            End If



            For Each row As DataRow In table.Rows
                result = New System.Text.StringBuilder()


                For i As Integer = 0 To table.Columns.Count - 1

                    result.Append(row(i).ToString())
                    result.Append(If(i = table.Columns.Count - 1, vbLf, ","))

                Next

                sw.Write(result)
            Next

        End Using

    End Function



    Private Function setData(filepath As String) As DataTable

        Dim machine() As String = Config_report.read_tree()

        Dim command As String = ""

        If Not CSI_Lib.CheckLic(2) = 3 Then 'If CSI_Lib.isClientSQlite Then
            Dim reader As SQLiteDataReader
            Dim tmp_table_cmd As New SQLiteCommand

            Dim bigTable, temp_table As New DataTable

            Dim firstFillorNot As Boolean = True


            Using sqlConn As SQLiteConnection = New SQLiteConnection(cs)
                sqlConn.Open()
                If machine.Length > 0 Then
                    For i = 0 To UBound(machine)

                        temp_table = New DataTable

                        machine(i) = CSI_Lib.RenameMachine(machine(i))

                        command = "SELECT '" + machine(i) + "' as MchName, month_, day_, year_, time_, date_, case when Status LIKE '_PARTNO:%' then substr(Status, 9) end, status   as status, shift, cycletime " +
                            " FROM  [tbl_" & machine(i) & "]  " +
                            " WHERE time_  between '" + Config_report.DTP_StartDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + Config_report.DTP_EndDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "'  " +
                            "    "

                        tmp_table_cmd.CommandText = command
                        tmp_table_cmd.Connection = sqlConn

                        reader = tmp_table_cmd.ExecuteReader
                        temp_table.Load(reader)



                        ToCSV(temp_table, filepath, firstFillorNot)




                        If firstFillorNot = True Then
                            firstFillorNot = False
                        End If
                    Next
                End If

                Dim ds As ReportingDataset = New ReportingDataset()

                ' ds.tbl_partsNumber.Merge(bigTable)
                Return bigTable
            End Using

        Else



            Dim reader As MySqlDataReader
            Dim tmp_table_cmd As New MySqlCommand

            Dim bigTable, temp_table As New DataTable

            Dim firstFillorNot As Boolean = True


            Dim db_authPath As String = Nothing
            If (File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
                Using streader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath + "\sys\SrvDBpath.csys")
                    db_authPath = streader.ReadLine()
                End Using
            End If
            Dim connectionString As String
            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_auth;" + CSI_Library.CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"


            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)
                sqlConn.Open()
                If machine.Length > 0 Then
                    For i = 0 To UBound(machine)

                        temp_table = New DataTable

                        machine(i) = CSI_Lib.RenameMachine(machine(i))

                        command = "SELECT '" + machine(i) + "' as MchName, month_, day_, year_, time_, date_,     iif(Status LIKE '_PARTNO:%', Mid(Status, 9) , status )  as status, shift, cycletime " +
                            " FROM  tbl_" & machine(i) & "  " +
                            " WHERE time_  between #" + Config_report.DTP_StartDate.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "# and #" + Config_report.DTP_EndDate.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "#  " +
                            "    "

                        tmp_table_cmd.CommandText = command
                        tmp_table_cmd.Connection = sqlConn

                        reader = tmp_table_cmd.ExecuteReader
                        temp_table.Load(reader)



                        ToCSV(temp_table, filepath, firstFillorNot)




                        If firstFillorNot = True Then
                            firstFillorNot = False
                        End If
                    Next
                End If

                Dim ds As ReportingDataset = New ReportingDataset()

                ' ds.tbl_partsNumber.Merge(bigTable)
                Return bigTable
            End Using
        End If
    End Function



    Public Shared Sub Serialise(c As Control, XmlFileName As String)
        Try
            Dim xmlSerialisedForm As New XmlTextWriter(XmlFileName, System.Text.Encoding.[Default])
            xmlSerialisedForm.Formatting = Formatting.Indented
            xmlSerialisedForm.WriteStartDocument()
            xmlSerialisedForm.WriteStartElement("ChildForm")
            ' enumerate all controls on the form, and serialise them as appropriate
            AddChildControls(xmlSerialisedForm, c)
            xmlSerialisedForm.WriteEndElement()
            ' ChildForm
            xmlSerialisedForm.WriteEndDocument()
            xmlSerialisedForm.Flush()
            xmlSerialisedForm.Close()
        Catch ex As Exception
            MessageBox.Show("Error in serialisation")
        End Try

    End Sub

    Private Shared Sub AddChildControls(xmlSerialisedForm As XmlTextWriter, c As Control)
        For Each childCtrl As Control In c.Controls
            If Not (TypeOf childCtrl Is Label) Then
                ' serialise this control
                xmlSerialisedForm.WriteStartElement("Control")
                xmlSerialisedForm.WriteAttributeString("Type", childCtrl.[GetType]().ToString())
                xmlSerialisedForm.WriteAttributeString("Name", childCtrl.Name)
                If TypeOf childCtrl Is TextBox Then
                    xmlSerialisedForm.WriteElementString("Text", DirectCast(childCtrl, TextBox).Text)
                ElseIf TypeOf childCtrl Is ComboBox Then
                    xmlSerialisedForm.WriteElementString("Text", DirectCast(childCtrl, ComboBox).Text)
                    xmlSerialisedForm.WriteElementString("SelectedIndex", DirectCast(childCtrl, ComboBox).SelectedIndex.ToString())
                ElseIf TypeOf childCtrl Is ListBox Then
                    ' need to account for multiply selected items
                    Dim lst As ListBox = DirectCast(childCtrl, ListBox)
                    If lst.SelectedIndex = -1 Then
                        xmlSerialisedForm.WriteElementString("SelectedIndex", "-1")
                    Else
                        For i As Integer = 0 To lst.SelectedIndices.Count - 1
                            xmlSerialisedForm.WriteElementString("SelectedIndex", (lst.SelectedIndices(i).ToString()))
                        Next
                    End If
                ElseIf TypeOf childCtrl Is CheckBox Then
                    xmlSerialisedForm.WriteElementString("Checked", DirectCast(childCtrl, CheckBox).Checked.ToString())
                ElseIf TypeOf childCtrl Is DateTimePicker Then
                    xmlSerialisedForm.WriteElementString("value", DirectCast(childCtrl, DateTimePicker).Value.ToString())
                End If
                ' this next line was taken from http://stackoverflow.com/questions/391888/how-to-get-the-real-value-of-the-visible-property
                ' which dicusses the problem of child controls claiming to have Visible=false even when they haven't, based on the parent
                ' having Visible=true
                Dim visible As Boolean = CBool(GetType(Control).GetMethod("GetState", BindingFlags.Instance Or BindingFlags.NonPublic).Invoke(childCtrl, New Object() {2}))
                xmlSerialisedForm.WriteElementString("Visible", visible.ToString())
                ' see if this control has any children, and if so, serialise them
                If childCtrl.HasChildren Then
                    If TypeOf childCtrl Is SplitContainer Then
                        ' handle this one as a special case
                        AddChildControls(xmlSerialisedForm, DirectCast(childCtrl, SplitContainer).Panel1)
                        AddChildControls(xmlSerialisedForm, DirectCast(childCtrl, SplitContainer).Panel2)
                    Else
                        AddChildControls(xmlSerialisedForm, childCtrl)
                    End If
                End If
                ' Control
                xmlSerialisedForm.WriteEndElement()
            End If
        Next
    End Sub

    Public Shared Sub Deserialise(c As Control, XmlFileName As String)
        If File.Exists(XmlFileName) Then
            Dim xmlSerialisedForm As New XmlDocument()
            xmlSerialisedForm.Load(XmlFileName)
            Dim topLevel As XmlNode = xmlSerialisedForm.ChildNodes(1)
            For Each n As XmlNode In topLevel.ChildNodes
                SetControlProperties(DirectCast(c, Control), n)
            Next
        End If
    End Sub

    Private Shared Sub SetControlProperties(currentCtrl As Control, n As XmlNode)
        ' get the control's name and type
        Dim controlName As String = n.Attributes("Name").Value
        Dim controlType As String = n.Attributes("Type").Value
        ' find the control
        Dim ctrl As Control() = currentCtrl.Controls.Find(controlName, True)
        ' can't find the control
        If ctrl.Length = 0 Then
        Else
            Dim ctrlToSet As Control = GetImmediateChildControl(ctrl, currentCtrl)
            If ctrlToSet IsNot Nothing Then
                If ctrlToSet.[GetType]().ToString() = controlType Then
                    ' the right type too ;-)
                    Select Case controlType
                        Case "System.Windows.Forms.TextBox"
                            DirectCast(ctrlToSet, System.Windows.Forms.TextBox).Text = n("Text").InnerText
                            Exit Select
                        Case "System.Windows.Forms.ComboBox"
                            DirectCast(ctrlToSet, System.Windows.Forms.ComboBox).Text = n("Text").InnerText
                            DirectCast(ctrlToSet, System.Windows.Forms.ComboBox).SelectedIndex = Convert.ToInt32(n("SelectedIndex").InnerText)
                            Exit Select
                        Case "System.Windows.Forms.ListBox"
                            ' need to account for multiply selected items
                            Dim lst As ListBox = DirectCast(ctrlToSet, ListBox)
                            Dim xnlSelectedIndex As XmlNodeList = n.SelectNodes("SelectedIndex")
                            For i As Integer = 0 To xnlSelectedIndex.Count - 1
                                lst.SelectedIndex = Convert.ToInt32(xnlSelectedIndex(i).InnerText)
                            Next
                            Exit Select
                        Case "System.Windows.Forms.CheckBox"
                            DirectCast(ctrlToSet, System.Windows.Forms.CheckBox).Checked = Convert.ToBoolean(n("Checked").InnerText)
                            Exit Select
                    End Select
                    ctrlToSet.Visible = Convert.ToBoolean(n("Visible").InnerText)
                    ' if n has any children that are controls, deserialise them as well
                    If n.HasChildNodes AndAlso ctrlToSet.HasChildren Then
                        Dim xnlControls As XmlNodeList = n.SelectNodes("Control")
                        For Each n2 As XmlNode In xnlControls
                            SetControlProperties(ctrlToSet, n2)
                        Next
                    End If
                    ' not the right type
                Else
                End If
                ' can't find a control whose parent is the current control
            Else
            End If
        End If
    End Sub

    Private Shared Function GetImmediateChildControl(ctrl As Control(), currentCtrl As Control) As Control
        Dim c As Control = Nothing
        For i As Integer = 0 To ctrl.Length - 1
            If (ctrl(i).Parent.Name = currentCtrl.Name) OrElse (TypeOf currentCtrl Is SplitContainer AndAlso ctrl(i).Parent.Parent.Name = currentCtrl.Name) Then
                c = ctrl(i)
                Exit For
            End If
        Next
        Return c
    End Function


    Private Sub CsvFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CsvFileToolStripMenuItem.Click

        Dim folderDlg As New SaveFileDialog
        folderDlg.Filter = "csv Files | *.csv"
        folderDlg.ShowDialog()

        If (folderDlg.FileName <> "") Then
            '  generateCSVaccordingToConfig_report(folderDlg.FileName())
        End If

    End Sub



    Private Sub TestSqliteToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'CSI_Lib.UpdateDB_Sqlite()
    End Sub

    Private Sub EventReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EventReportToolStripMenuItem.Click
        Event_report.Show()
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs)
        MsgBox(Me.Height)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        MsgBox(Config_report.Size.Height)
    End Sub

    Private Sub Button1_Click_3(sender As Object, e As EventArgs)
        MsgBox(Me.Size.Height)
    End Sub

    Private Sub check_srv_version_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles check_srv_version.DoWork
        If CSI_Lib.CheckLic(2) = 3 Then
            Try
                Dim reader As MySqlDataReader
                Dim cmd As New MySqlCommand

                Dim connectionString As String
                Dim db_authPath As String = Nothing
                If (File.Exists(rootPath + "/sys/SrvDBpath.csys")) Then
                    Using rder As New StreamReader(rootPath + "/sys/SrvDBpath.csys")
                        db_authPath = rder.ReadLine()
                    End Using
                End If
                connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_auth;" + CSI_Library.CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

                Dim cnt As MySqlConnection = New MySqlConnection(connectionString)
                cnt.Open()

                Dim dcmda As MySqlCommand = New MySqlCommand("SELECT * FROM csi_database.tbl_csiflex_version", cnt)
                Dim r As MySqlDataReader = dcmda.ExecuteReader()

                r.Read()
                SRV_Version = r.GetString("version")

                cnt.Close()
            Catch ex As Exception
                If ex.HResult = -2147467259 Then
                    SRV_Version = 0
                Else
                    CSI_Lib.LogClientError(ex.Message)
                End If
            End Try


        Else

        End If

    End Sub



    Private Sub BGW_SQLiteUpdate_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BGW_SQLiteUpdate.DoWork
        If (chemin_eNET <> "") Then

            'CSI_Lib.CreateDB_fromServer(TB_eNETfolder.Text, TB_DBpath.Text)
            CSI_Library.CSI_Library.isServer = False
            'CSI_Lib.UpdateDB_Mysql()

            If (CSI_Lib.getFirstUpdateOrNotSQLite()) Then
                'StatusStrip1.Text = "Database is being created"
                'ToolStripStatusLabel2.Text = "Database is being created"
                'StatusStrip1.Text = "Database is being created"
                Dim years_() As String

                If (File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\years_.csys")) Then
                    Using streader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath + "\sys\years_.csys")
                        years_ = streader.ReadLine().Split(",")
                    End Using
                End If

                For Each year As String In years_
                    If year <> "" Then CSI_Lib.FirstUpdateDB_SQLite(year)
                Next

            Else
                'ToolStripStatusLabel2.Text = "Database is being updated"
                CSI_Lib.UpdateDB_SQLite()
            End If
        End If
    End Sub





End Class

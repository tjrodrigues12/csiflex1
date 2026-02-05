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
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  Reporting Client Side 
'=========================================================================================================================================
'=========================================================================================================================================



Public Class Form1
    Public User_Connection_String As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Forms.Application.StartupPath.ToString & _
                                          "\sys\CSI_auth.mdb; Jet OLEDB:Database Password=4Solutions; mode=ReadWrite;"
    Public LocalDB_Connection_String As String = ""
    Public ServerDB_Connection_String As String = ""

    Public username_ As String
    Public Shared setupForm_Singleton As Reporting_availability
    Public minyear As Date
    Public maxyear As Date
    Public eHUBConf As New Dictionary(Of String, String) ' Used to get the shift setup
    Public MonSetup As New Dictionary(Of String, String) ' Used to get the shift setup
    Public ShiftSetup As New Dictionary(Of String, String)
    Public before2012 As Boolean = False
    Public CSI_Lib As CSI_Library.CSI_Library
    Public stat(3) As String
    Public chemin_bd As String
    Public chemin_eNET As String
    Public Image1 As Image, bySetup As Boolean = False
    Public machine_list As New Dictionary(Of String, String)

    Public srv As New ServiceNT
    Public COM_ As New Communication

    'Public objexcel As Microsoft.Office.Interop.Excel.Application
    'Public xlBook As Microsoft.Office.Interop.Excel.Workbook
    'Public xlsheet As Microsoft.Office.Interop.Excel.Worksheet
    Public week_ As String = 15
    Public activated_ As Boolean = False
    Public first_creation_of_the_DB As Boolean = False


    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) _
     Handles MyBase.FormClosing
        
        If first_creation_of_the_DB = True Then
            MessageBox.Show("The database is being created, please wait", MsgBoxStyle.SystemModal + MsgBoxStyle.Critical)
            e.Cancel = True   'stop the form from closing
        End If

        srv.continue_updating = False
        COM_.Continue_clt_service = False
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

    Private Sub bcolor()
        Try
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
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Form1_Resize0(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        Me.SuspendLayout()
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        '   Me.Update()
        Me.ResumeLayout()
        Me.Refresh()
    End Sub
    Private Sub CreateFV_with_license()
        Dim folderpath As String = Windows.Forms.Application.StartupPath & "\sys" '\Setup_.sys"
        Dim lib_ As New CSI_Library.CSI_Library
        Dim licenseinfos As String()
        licenseinfos = lib_.CheckLic()
        Dim expiredtext As String = ""
        Dim expirationdate As DateTime

        Dim i As Integer = 0


        If (File.Exists(Windows.Forms.Application.StartupPath & "\sys\CSIFv_.sys")) Then
            Using r As StreamReader = New StreamReader(Windows.Forms.Application.StartupPath & "\sys\CSIFv_.sys", False)
                Dim lib___ As New CSI_Library.CSI_Library
                Dim tmp As String() = Split(lib___.AES_Decrypt(r.ReadLine, "4Solutions"), ":")
                i = tmp(0)

                r.Close()
            End Using

            Kill(Windows.Forms.Application.StartupPath & "\sys\CSIFv_.sys")

        End If

        If (licenseinfos(0) = "ok") Then
            'std version
            Try

                expirationdate = DateTime.Parse(licenseinfos(1))
                i = licenseinfos(2)

              

                Using writer As StreamWriter = New StreamWriter(folderpath & "\CSIFv_.sys")
                    'Dim lib__ As New CSI_Library.CSI_Library

                    writer.Write(Login.AES_Encrypt(i & ":-----", "4Solutions"))
                    writer.Close()
                End Using
            Catch ex As Exception
                '  MessageBox.Show("Unable to save the database path : " & ex.Message)
            End Try
            Dim dayremaining As Double = DateDiff(DateInterval.Day, DateTime.Now, expirationdate)
            If (dayremaining > 30) Then
                ToolStripStatusLabel1.ForeColor = Color.Black
            End If
            expiredtext = "Your license expires in " & dayremaining.ToString & " days"
            If (dayremaining > 365) Then
                expiredtext = ""
            End If

        ElseIf (licenseinfos(0) = "EXP") Then
            'lite version
            'MessageBox.Show("Your license is expired, you are now using CSIFlex lite", MsgBoxStyle., "License expired")
            Dim newlicens As New RenewLicense
            newlicens.ShowDialog()

            Try
                expirationdate = DateTime.Parse(licenseinfos(1))

                Using writer As StreamWriter = New StreamWriter(folderpath & "\CSIFv_.sys", False)
                    'Dim lib__ As New CSI_Library.CSI_Library

                    writer.Write(Login.AES_Encrypt("1:-----", "4Solutions"))
                    writer.Close()
                End Using
            Catch ex As Exception
                'MessageBox.Show("Unable to save the database path : " & ex.Message)
            End Try

            'expiredtext = "Your license is expired since " + DateDiff(DateInterval.Day, expirationdate, DateTime.Now).ToString + " days"
            expiredtext = "You are now using CSI Flex Lite."
        End If

        'Write remaining days before Expiration
        ToolStripStatusLabel1.Text = expiredtext
    End Sub


    Private Sub LoadRenamedDLL()
        'Assembly.Load(Windows.Forms.Application.StartupPath & "\CSIFPP.dll")
        'Dim dllPath As String = Path.Combine(Directory.GetCurrentDirectory(), "CSIFPP.dll")
        'Assembly.LoadFile(dllPath)

    End Sub
    '-----------------------------------------------------------------------------------------------------------------------
    '  form load
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Show Splash Screen for 3 seconds
        Dim splash As New SplashScreen
        splash.Show()


        'LoadRenamedDLL()
        ToolStripStatusLabel2.Text = ""

        '  GenerateToolStripMenuItem.Text = "Compare mode"

        'Check if the license file exist, otherwise creates it
        'CheckLicense()

        'If Not (Directory.Exists(Windows.Forms.Application.StartupPath & "\sys")) Then
        '    Directory.CreateDirectory(Windows.Forms.Application.StartupPath & "\sys")
        'End If

        'DELETE FILE TO FORCE CHECK THE LICENSE



        
        CreateFV_with_license()
        'SAVING VERSION SHOULD NOT BE AT THE BEGINNING
        'Try
        '    Using writer As StreamWriter = New StreamWriter(Windows.Forms.Application.StartupPath & "\sys\CSIFv_.sys", False)
        '        'Dim lib__ As New CSI_Library.CSI_Library

        '        writer.Write(Login.AES_Encrypt(Welcome.CSIF_version.ToString() & ":-----", "4Solutions"))
        '        writer.Close()
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show("Unable to save the version : " & ex.Message)
        'End Try

        ' Check the CSI version : Lite/standard ....s



        If File.Exists(Windows.Forms.Application.StartupPath & "\sys\CSIFv_.sys") Then

            Try
                Using r As StreamReader = New StreamReader(Windows.Forms.Application.StartupPath & "\sys\CSIFv_.sys", False)
                    Dim lib___ As New CSI_Library.CSI_Library
                    Dim tmp As String() = Split(lib___.AES_Decrypt(r.ReadLine, "4Solutions"), ":")
                    Welcome.CSIF_version = tmp(0)

                    r.Close()
                End Using

            Catch ex As Exception
                MessageBox.Show("Unable to read the CSIFlex version : " & ex.Message)
            End Try


        Else

            If Not (Welcome.ShowDialog() = Forms.DialogResult.OK) Then
                'Dispose()
                Environment.Exit(0)
            End If
 

            End If

       

            ' Set the color in the MDI client.
            For Each ctl As Control In Me.Controls
                If TypeOf ctl Is MdiClient Then
                    ctl.BackColor = Me.BackColor
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

        'disable std feature if version is lite
        If Welcome.CSIF_version = 1 Then
            ExportToolStripMenuItem.Enabled = False
            ToolStripMenuItem1.Enabled = False
        End If

            ' Set the color to the menu strip
            MenuStrip1.ForeColor = Color.Black
            Me.BackColor = System.Drawing.SystemColors.ControlLightLight

            ' check if the sys directory exist, and create it if no:
            Dim dir_ok As String = CSI_Lib.dir_(System.Windows.Forms.Application.StartupPath & "\sys\")
            If dir_ok <> "ok" Then
                MessageBox.Show("CSIFLEX has incountered an Error while creating the sys repertory in the CSIFLEX folder : " & dir_ok)
                GoTo end2
            Else
                If File.Exists(System.Windows.Forms.Application.StartupPath & "\Setup_.sys") Then
                    File.Move(System.Windows.Forms.Application.StartupPath & "\Setup_.sys", System.Windows.Forms.Application.StartupPath & "\sys\Setup_.sys")
                End If
                If File.Exists(System.Windows.Forms.Application.StartupPath & "\Setupdb_.sys") Then
                    File.Move(System.Windows.Forms.Application.StartupPath & "\Setupdb_.sys", System.Windows.Forms.Application.StartupPath & "\sys\Setupdb_.sys")
                End If
                If File.Exists(System.Windows.Forms.Application.StartupPath & "\Setupdt_.sys") Then
                    File.Move(System.Windows.Forms.Application.StartupPath & "\Setupdt_.sys", System.Windows.Forms.Application.StartupPath & "\sys\Setupdt_.sys")
                End If
                If File.Exists(System.Windows.Forms.Application.StartupPath & "\target_.sys") Then
                    File.Move(System.Windows.Forms.Application.StartupPath & "\target_.sys", System.Windows.Forms.Application.StartupPath & "\sys\target_.sys")
                End If
                If File.Exists(System.Windows.Forms.Application.StartupPath & "\targetColor_.sys") Then
                    File.Move(System.Windows.Forms.Application.StartupPath & "\targetColor_.sys", System.Windows.Forms.Application.StartupPath & "\sys\targetColor_.sys")
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
                File.WriteAllBytes(System.Windows.Forms.Application.StartupPath & "\sys\" + "netFrameWork_4_5_2.exe", My.Resources.NetFrameWork_WebInstaller)

                Dim startInfo As New ProcessStartInfo()
                startInfo.FileName = System.Windows.Forms.Application.StartupPath & "\sys\" + "netFrameWork_4_5_2.exe"
                Process.Start(startInfo)


                GoTo End2
            End If
            '==================================================================================



            'Check Config Data ================================================================
            Try

                If (Welcome.CSIF_version <> 3) Then
                    If Exists(System.Windows.Forms.Application.StartupPath & "\sys\Setup_.sys") Then
                        Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\Setup_.sys")
                            chemin_eNET = reader.ReadLine()
                        End Using
                Else
                    MessageBox.Show("Please specify the eNET folder")
                    SetupForm.Show()
                    GoTo fin
                    End If
                End If

            If Exists(System.Windows.Forms.Application.StartupPath & "\sys\Setupdb_.sys") Then
                Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\Setupdb_.sys")
                    chemin_bd = reader.ReadLine()
                End Using
            ElseIf (Welcome.CSIF_version <> 3) Then
                MessageBox.Show("Please specify the Database folder")
                SetupForm.Show()
                GoTo fin
            End If



            If Welcome.CSIF_version <> 1 And Welcome.CSIF_version <> 2 Then

                Dim db_authPath As String = Nothing

                If (File.Exists(Forms.Application.StartupPath.ToString + "/sys/CSI_auth_path.sys")) Then
                    Using reader As New StreamReader(Forms.Application.StartupPath.ToString + "/sys/CSI_auth_path.sys")
                        db_authPath = reader.ReadLine()
                    End Using
                End If

                User_Connection_String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & db_authPath & "\CSI_auth.mdb; Jet OLEDB:Database Password=4Solutions; mode=ReadWrite;"

                Using w As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\LocalUser_.sys", False)
                    w.WriteLine(username_)
                End Using

                Dim cnt As OleDbConnection = New OleDbConnection(User_Connection_String)
                cnt.Open()

                Dim da As OleDbCommand = New OleDbCommand("SELECT machines FROM Users WHERE Username_='" & username_ & "'", cnt)
                Dim r As OleDbDataReader = da.ExecuteReader()
                Using w As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\monlist.sys")
                    w.WriteLine("_MT_Assets : ")
                    r.Read()
                    Dim list__ As String() = Split(r("machines").ToString(), ", ")
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

                Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\monlist.sys")
                    Dim i As Integer = 0
                    While Not reader.EndOfStream
                        ReDim Preserve machines(i + 1)
                        machines(i) = reader.ReadLine()
                        i = i + 1
                    End While

                End Using

                Using writer As StreamWriter = New StreamWriter(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys", False)
                    For Each machine In machines
                        If machine IsNot Nothing Then
                            If machine.StartsWith("_MT") Or machine.StartsWith("_ST") Then
                                'Nothing
                            Else
                                writer.WriteLine(machine.Replace(" ", "").Replace("-", "").Replace(".", "_") & "," & "eNET,1")
                            End If
                        End If
                    Next

                End Using
            ElseIf Not Exists(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys") Then
                Dim machines As String()

                machines = CSI_Lib.LoadMachines()

                Using writer As StreamWriter = New StreamWriter(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys", False)
                    For Each machine In machines
                        If machine IsNot Nothing Then
                            If machine.StartsWith("_MT") Or machine.StartsWith("_ST") Then
                                'Nothing
                            Else
                                writer.WriteLine(machine.Replace(" ", "").Replace("-", "").Replace(".", "_") & "," & "eNET,1")
                            End If
                        End If
                    Next

                End Using
            End If



            If Exists(System.Windows.Forms.Application.StartupPath & "\sys\Setupdt_.sys") Then
                Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\Setupdt_.sys")
                    week_ = reader.ReadLine()
                    reader.Close()
                End Using
            Else
                Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\Setupdt_.sys")
                    writer.Write("15")
                    week_ = "15"
                    writer.Close()
                End Using
            End If
            '==================================================================================
            'If Exists(System.Windows.Forms.Application.StartupPath & "\Daily.xlsx") Then

            'Else
            '    Dim path_excel
            '    path_excel = System.Windows.Forms.Application.StartupPath & "\Daily.xlsx"
            '    IO.File.WriteAllBytes(path_excel, My.Resources.Daily)

            'End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
            GoTo fin
        End Try

        If IsNothing(chemin_bd) Then
            SetupForm.Show()
            GoTo fin
        End If
            If Dir(chemin_bd, vbDirectory) = "" Then
                SetupForm.Show()
                GoTo fin
            Else

                Try

                '    If BackgroundWorker1.IsBusy <> True Then
                '        ' Start the asynchronous operation.
                '        'Form3.Enabled = False
                '    '   Form12.Show()

                'End If

                If (Welcome.CSIF_version <> 3) Then
                    SynchroDB.Show()
                    ToolStripStatusLabel2.Text = "Update in progress"
                    If Not System.IO.File.Exists(chemin_bd & "\CSI_Database.mdb") Then Form12.Label1.Text = "CSIFLEX is updating the monitoring data, please wait"
                    BackgroundWorker1.RunWorkerAsync()
                End If


            Catch ext As Exception
                MessageBox.Show(ext.Message)
            End Try

        End If



        '  Form6.MdiParent = Me
        Form7.MdiParent = Me
        Form3.MdiParent = Me
        Form8.MdiParent = Me
        'SetupForm.MdiParent = Me

        '  Live.MdiParent = Me
        SynchroDB.MdiParent = Me
        SynchroDB.Location = New System.Drawing.Point(Me.Height / 2, 200)
        Form12.MdiParent = Me
        Form12.Location = New System.Drawing.Point(Me.Height / 2, 100)

        Me.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.ResizeRedraw, True)

        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Form3.Show()


        '  Live.Show()
        ' Monitoring.Show()
fin:
End2:
        If activated_ = False Then Me.Close()


        'eHUBConf = CSI_Lib.Machines_Setup(chemin_eNET)
        'If Not IsNothing(eHUBConf) Then
        '    MonSetup = CSI_Lib.Departement_Setup(chemin_eNET, eHUBConf)
        '    If Not IsNothing(MonSetup) Then
        '        ShiftSetup = CSI_Lib.Shift_Setup(chemin_eNET, MonSetup)
        '    End If
        'End If

        If Welcome.CSIF_version <> 1 Then

            Dim srv_thread As New Thread(Sub()
                                             srv.start_service()
                                         End Sub)

            srv_thread.Name = "CSIFLEX Real time monitoring thread"
            srv_thread.Start()

            Dim Com_thread As New Thread(Sub()

                                             COM_.start()

                                         End Sub)

            Com_thread.Name = "CSIFLEX Real time Communication thread"
            Com_thread.Start()
        End If

    End Sub


    Function ExcelConnection() As String

        Dim strConnect As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\_MACHINE_2013.xlsx; Extended Properties = Excel 12.0 Xml;HDR=YES ; Format=xlsx;"

        Return strConnect
    End Function


    '-----------------------------------------------------------------------------------------------------------------------
    ' 1st button form1
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub DailyReportToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Form3.Show()
    End Sub



    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Form7.Show()
    End Sub



    '    '-----------------------------------------------------------------------------------------------------------------------
    '    ' Export to CSV files
    '    '-----------------------------------------------------------------------------------------------------------------------
    '    Public Sub export_(dir_ As String)

    '        Dim year_start As Integer
    '        Dim year_end As Integer

    '        Dim month_start As Integer
    '        Dim month_end As Integer

    '        Dim ThirdDim As Integer = 0
    '        Dim day_start As Integer
    '        Dim day_end As Integer
    '        Dim indx As Integer
    '        Dim i As Integer
    '        Dim active_machines(1) As String
    '        Dim start_date As DateTime
    '        Dim end_date As DateTime
    '        Dim shft As Integer


    '        Dim sorted_stats(4) As String
    '        Dim percent(7) As Integer
    '        Dim list2 As New List(Of String)
    '        Dim results As DateTime()
    '        Dim k As Integer = 0
    '        Dim final_time As Double
    '        Dim total1 As Double = 0
    '        Dim total2 As Double = 0
    '        Dim total3 As Double = 0
    '        Dim rang_ As Integer

    '        Dim cnt As System.Data.OleDb.OleDbConnection
    '        Dim last_loop_ As Boolean = False

    '        Dim final_time_other(4) As Double
    '        Dim nb_mch As Integer

    '        final_time = 0


    '        '*** DB Querry **********************


    '        '*************************************************************************************************************************************************'
    '        '**** DB Connection
    '        '*************************************************************************************************************************************************'
    '        Dim dbConnectStr As String
    '        dbConnectStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & chemin_bd & "\CSI_Database.mdb;"
    '        cnt = New System.Data.OleDb.OleDbConnection(dbConnectStr)

    '        cnt.Open()
    '        If cnt.State = 1 Then
    '        Else
    '            MessageBox.Show("Connection to the database failed")
    '            GoTo fin
    '        End If
    '        Dim SchemaTable
    '        '*************************************************************************************************************************************************'
    '        '**** DB Connection -END
    '        '*************************************************************************************************************************************************'




    '        '*************************************************************************************************************************************************'
    '        '**** Active machines from the treenode
    '        '*************************************************************************************************************************************************'

    '        i = 0
    '        Dim j As Integer = 0

    '        For Each treeviewnode As TreeNode In Form3.TreeView1.Nodes
    '            If treeviewnode.Checked = True Then
    '                active_machines(i) = treeviewnode.Text
    '                i = i + 1
    '                ReDim Preserve active_machines(i)
    '            End If
    '            For Each treeviewnode2 As TreeNode In treeviewnode.Nodes
    '                For Each treeviewnode3 As TreeNode In treeviewnode2.Nodes
    '                    If treeviewnode3.Checked = True Then
    '                        active_machines(i) = treeviewnode3.Text
    '                        i = i + 1
    '                        ReDim Preserve active_machines(i)
    '                    End If
    '                    k = k + 1
    '                Next
    '                If treeviewnode2.Checked = True Then
    '                    active_machines(i) = treeviewnode2.Text
    '                    i = i + 1
    '                    ReDim Preserve active_machines(i)
    '                End If
    '            Next
    '            j = j + 1
    '        Next

    '        i = 0
    '        j = 0
    '        Dim tmptmp As String
    '        For Each item In active_machines
    '            For Each item2 In list2
    '                tmptmp = Strings.Right(item2, Strings.Len(item2) - 4)
    '                If item = tmptmp Then
    '                    active_machines(i) = ""
    '                End If
    '                j = j + 1
    '            Next
    '            i = i + 1
    '        Next

    '        i = 0
    '        Dim erased As Integer = 0
    '        For Each item In active_machines
    '            If active_machines(i) = "" Or active_machines(i) Is Nothing Then
    '                For j = i To UBound(active_machines) - 1
    '                    active_machines(j) = active_machines(j + 1)
    '                Next j
    '            End If
    '            i = i + 1
    '        Next

    '        For Each item In active_machines
    '            If item = "" Or item Is Nothing Then
    '                erased = erased + 1
    '            End If
    '        Next

    '        ReDim Preserve active_machines(i - erased - 1)
    '        '*************************************************************************************************************************************************'
    '        '**** Active machines from the treenode -END
    '        '*************************************************************************************************************************************************'




    '        Dim first_insertion As Boolean = True ' for daily reports

    '        i = 0
    '        Form4.Show()
    '        Form4.Label1.Text = "preparing data... "
    '        Form4.ProgressBar1.Value = 0



    '        year_start = Form3.DateTimePicker1.Value.Year - 2000
    '        year_end = Form3.DateTimePicker2.Value.Year - 2000

    '        month_start = Form3.DateTimePicker1.Value.Month
    '        month_end = Form3.DateTimePicker2.Value.Month

    '        day_start = Form3.DateTimePicker1.Value.Day
    '        day_end = Form3.DateTimePicker2.Value.Day

    '        If Form3.DateTimePicker1.Value = Form3.DateTimePicker2.Value Then
    '            'Form3.RadioButton1.Checked = True
    '            'Form3.RadioButton2.Checked = False
    '        End If




    '        Dim year_end_tmp As Integer = year_end
    '        Dim month_end_tmp As Integer = month_end
    '        Dim day_end_tmp As Integer = day_end

    '        Dim DaysInMonth As Integer
    '        Dim loop_ As Boolean = True

    '        If Form3.RadioButton2.Checked Then ' DAILY
    '            year_end = year_start
    '            month_end = month_start
    '            day_end = day_start
    '        End If






    '        '******************************************************************************************************************************
    '        'For each selected machine:====================================================================================================
    '        '==============================================================================================================================
    '        For Each items In active_machines

    '            active_machines(i) = Strings.Replace(active_machines(i), " ", "")
    '            active_machines(i) = Strings.Replace(active_machines(i), "-", "")

    '            k = 0
    '            final_time = Nothing

    '            total1 = 0
    '            total2 = 0
    '            total3 = 0



    '            ' Delete the tmp table if exist =======================================
    '            SchemaTable = cnt.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, _
    '                       New Object() {Nothing, Nothing, Nothing, "TABLE"})
    '            For j = 0 To SchemaTable.Rows.Count - 1
    '                If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table") Then
    '                    Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table]", cnt)
    '                    cmd_delete.ExecuteNonQuery()
    '                End If
    '                If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table1") Then
    '                    Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table1]", cnt)
    '                    cmd_delete.ExecuteNonQuery()
    '                End If
    '                If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table2") Then
    '                    Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table2]", cnt)
    '                    cmd_delete.ExecuteNonQuery()
    '                End If
    '                If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table3") Then
    '                    Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table3]", cnt)
    '                    cmd_delete.ExecuteNonQuery()
    '                End If
    '            Next j
    '            '=======================================================================




    '            ' Building the QueR ====================================================
    '            Dim tmp_table_cmd As New OleDbCommand
    '            Dim tmp_table2_cmd As New OleDbCommand
    '            Dim tmp_table1_cmd As New OleDbCommand
    '            Dim tmp_table3_cmd As New OleDbCommand

    '            If year_start = year_end Then
    '                If month_start = month_end Then
    '                    tmp_table_cmd.CommandText = "SELECT * INTO tmp_table FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & " and [month_]= " & month_start & " and [day_] between " & day_start & " and " & day_end
    '                    tmp_table_cmd.Connection = cnt
    '                    tmp_table_cmd.ExecuteNonQuery()
    '                Else
    '                    If month_start < month_end Then
    '                        DaysInMonth = System.DateTime.DaysInMonth(year_start + 2000, month_start)
    '                        tmp_table1_cmd.CommandText = "SELECT * INTO tmp_table1 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] = " & month_start & " and [day_] >=  " & day_start & " and [day_] <= " & DaysInMonth
    '                        tmp_table1_cmd.Connection = cnt
    '                        tmp_table1_cmd.ExecuteNonQuery()
    '                        tmp_table2_cmd.CommandText = "SELECT * INTO tmp_table2 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] = " & month_end & " and [day_] >=  1  and [day_] <= " & day_end
    '                        tmp_table2_cmd.Connection = cnt
    '                        tmp_table2_cmd.ExecuteNonQuery()
    '                        If month_end - month_start <> 1 Then
    '                            tmp_table3_cmd.CommandText = "SELECT * INTO tmp_table3 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] >=" & month_start + 1 & " and [month_] <=" & month_end - 1
    '                            tmp_table3_cmd.Connection = cnt
    '                            tmp_table3_cmd.ExecuteNonQuery()
    '                        End If
    '                        tmp_table_cmd.Connection = cnt
    '                        tmp_table_cmd.CommandText = "SELECT * INTO tmp_table  FROM  tmp_table1"
    '                        tmp_table_cmd.ExecuteNonQuery()
    '                        tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table2"
    '                        tmp_table_cmd.ExecuteNonQuery()
    '                        If month_end - month_start <> 1 Then
    '                            tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table3"
    '                            tmp_table_cmd.ExecuteNonQuery()
    '                        End If
    '                    Else
    '                    End If
    '                End If
    '            Else
    '                If year_end - year_start = 1 Then
    '                    DaysInMonth = System.DateTime.DaysInMonth(year_start + 2000, month_start)
    '                    tmp_table1_cmd.CommandText = "SELECT * INTO tmp_table1 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] = " & month_start & " and [day_] >=  " & day_start & " and [day_] <= " & DaysInMonth
    '                    tmp_table1_cmd.Connection = cnt
    '                    tmp_table1_cmd.ExecuteNonQuery()
    '                    tmp_table3_cmd.CommandText = "SELECT * INTO tmp_table3 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] >=" & month_start + 1 & " and [month_] <=" & "12"
    '                    tmp_table3_cmd.Connection = cnt
    '                    tmp_table3_cmd.ExecuteNonQuery()
    '                    tmp_table_cmd.Connection = cnt
    '                    tmp_table_cmd.CommandText = "SELECT * INTO tmp_table  FROM  tmp_table1"
    '                    tmp_table_cmd.ExecuteNonQuery()
    '                    tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table3"
    '                    tmp_table_cmd.ExecuteNonQuery()
    '                    tmp_table1_cmd.CommandText = "DROP TABLE [tmp_table1]"
    '                    tmp_table1_cmd.Connection = cnt
    '                    tmp_table1_cmd.ExecuteNonQuery()
    '                    tmp_table1_cmd.CommandText = "DROP TABLE [tmp_table3]"
    '                    tmp_table1_cmd.Connection = cnt
    '                    tmp_table1_cmd.ExecuteNonQuery()
    '                    tmp_table1_cmd.CommandText = "SELECT * INTO tmp_table1 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_end & "and [month_] >= 1 " & " and [month_] <=" & month_end - 1
    '                    tmp_table1_cmd.Connection = cnt
    '                    tmp_table1_cmd.ExecuteNonQuery()
    '                    DaysInMonth = System.DateTime.DaysInMonth(year_end + 2000, month_end)
    '                    tmp_table3_cmd.CommandText = "SELECT * INTO tmp_table3 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_end & "and [month_] =" & month_end & " and [day_] >= 1 and [day_] <=  " & DaysInMonth
    '                    tmp_table3_cmd.Connection = cnt
    '                    tmp_table3_cmd.ExecuteNonQuery()
    '                Else
    '                    MessageBox.Show("You can generate reports within two years")
    '                End If
    '            End If
    '            '======================================================================= tmp_table ready.



    '            ' Select available status ====================================================
    '            Dim cmd_status As New OleDbCommand("SELECT DISTINCT [status] FROM [tmp_table]", cnt)
    '            Dim reader_status As OleDbDataReader = cmd_status.ExecuteReader()

    '            stat(0) = "_CON"
    '            stat(1) = "_COFF"
    '            stat(2) = "_SETUP"

    '            k = 3
    '            While reader_status.Read()
    '                If reader_status.HasRows And reader_status.GetValue(0) <> "_CON" And reader_status.GetValue(0) <> "_COFF" And reader_status.GetValue(0) <> "_SETUP" And reader_status.GetValue(0) <> "_SH_START" And reader_status.GetValue(0) <> "_SH_END" And Strings.Mid(reader_status.GetValue(0), 1, 7) <> "_PARTNO" Then
    '                    ReDim Preserve stat(k + 1)
    '                    stat(k) = reader_status.GetValue(0)
    '                    k = k + 1
    '                End If
    '            End While
    '            reader_status.Close()
    '            '======================================================================= all Status in stat()


    '            nb_mch = UBound(active_machines) + 1

    '            Form4.Label1.Text = "Exporting Data " & active_machines(i) & "..."
    '            Form4.ProgressBar1.Value = (((i + 1)) / nb_mch) * 100

    '            'for each status =============================================================================================== 
    '            ' Dim origine_path As String = Forms.Application.StartupPath & "\Daily.xlsx"

    '            'Dim cmd As New OleDbCommand("SELECT * INTO [Excel 8.0;Database=" & origine_path & "].[" & active_machines(i) & "] FROM [tmp_table]", cnt)
    '            'Dim reader As OleDbDataReader = cmd.ExecuteReader()
    '            'reader.Close()

    '            'Dim cmd As New OleDbCommand("SELECT * INTO [Excel 8.0;Database=" & origine_path & "].[sheet" & i + 1 & "] FROM [tmp_table]", cnt)
    '            'Dim reader As OleDbDataReader = cmd.ExecuteReader()
    '            'reader.Close()
    '            i = i + 1
    '        Next
    '        Form4.Label1.Text = "Exported."
    '        Form4.Close()
    'fin:

    '        MessageBox.Show("Data Exported")
    '    End Sub




    'Private Sub PDFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PDFToolStripMenuItem.Click
    '    Dim xlsApp As Object
    '    Dim xlsBook As Object
    '    Dim xlsSheet As Object
    '    Dim xlsBook2 As Object
    '    Dim xlsSheet2 As Object
    '    Dim last_loop_ As Boolean = False
    '    xlsApp = CreateObject("Excel.Application")
    '    xlsApp.Visible = False
    '    Dim rang_ As Integer
    '    Dim sheetindex As Integer
    '    Dim dtpicker(6) As String
    '    Dim Date_ As String


    '    xlsApp.DisplayAlerts = False

    '    Dim i As Integer
    '    Dim j As Integer

    '    Form4.Label1.Text = " Exporting ..."
    '    Form4.ProgressBar1.Value = 5
    '    Form4.Show()

    '    xlsBook2 = xlsApp.Workbooks.Open(Forms.Application.StartupPath & "\Daily.xlsx")
    '    xlsBook = xlsApp.workbooks.add

    '    xlsBook2.sheets("1").copy(after:=xlsBook.worksheets(xlsBook.worksheets.count))
    '    xlsBook.Sheets("sheet1").Delete()
    '    xlsSheet = xlsBook.worksheets(xlsBook.worksheets.count)

    '    For i = 0 To UBound(Form3.general_array, 3) - 1

    '        Form4.Label1.Text = " Exporting ... " & i & " / " & UBound(Form3.general_array, 3)
    '        Form4.ProgressBar1.Value = (i / UBound(Form3.general_array, 3)) * 100

    '        If i <> 0 Then
    '            sheetindex = xlsBook.worksheets.count
    '            xlsBook2.sheets("1").copy(after:=xlsBook.worksheets(sheetindex))
    '            xlsSheet = xlsBook.worksheets(xlsBook.worksheets.count)
    '        End If

    '        For k = 0 To 7

    '            '  shift 1
    '            rang_ = 7 + k
    '            xlsSheet.Range("D" & rang_.ToString).Value = Form3.general_array(k, 1, i)
    '            xlsSheet.Range("E" & rang_.ToString).Value = Form3.general_array(k, 2, i) / 100


    '            '  shift 2
    '            rang_ = 7 + k + 14
    '            xlsSheet.Range("D" & rang_.ToString).Value = Form3.general_array(k + 8, 1, i)
    '            xlsSheet.Range("E" & rang_.ToString).Value = Form3.general_array(k + 8, 2, i) / 100


    '            '  shift 3
    '            rang_ = 7 + k + 28
    '            xlsSheet.Range("D" & rang_.ToString).Value = Form3.general_array(k + 16, 1, i)
    '            xlsSheet.Range("E" & rang_.ToString).Value = Form3.general_array(k + 16, 2, i) / 100


    '        Next k

    '        xlsSheet.Range("B10").Value = Form3.general_array(3, 0, i)
    '        xlsSheet.Range("B11").Value = Form3.general_array(4, 0, i)
    '        xlsSheet.Range("B12").Value = Form3.general_array(5, 0, i)
    '        xlsSheet.Range("B13").Value = "Other"

    '        xlsSheet.Range("B24").Value = Form3.general_array(11, 0, i)
    '        xlsSheet.Range("B25").Value = Form3.general_array(12, 0, i)
    '        xlsSheet.Range("B26").Value = Form3.general_array(13, 0, i)
    '        xlsSheet.Range("B27").Value = "Other"

    '        xlsSheet.Range("B38").Value = Form3.general_array(19, 0, i)
    '        xlsSheet.Range("B39").Value = Form3.general_array(20, 0, i)
    '        xlsSheet.Range("B40").Value = Form3.general_array(21, 0, i)
    '        xlsSheet.Range("B41").Value = "Other"


    '        xlsSheet.Range("A2").Value() = Form3.general_array(0, 0, i)

    '        If Form3.RadioButton2.Checked Then
    '            xlsSheet.name = "Daily-" & Form3.general_array(0, 0, i) & "-" & Form3.general_array(1, 0, i)
    '            xlsSheet.Range("G2").Value() = Form3.general_array(1, 0, i)
    '        End If

    '        If Form3.RadioButton1.Checked Then
    '            xlsSheet.name = "Consolidated-" & xlsSheet.Range("A2").Value()
    '        End If

    '        dtpicker = Split(Form3.general_array(2, 0, i), "-")

    '        If Form3.RadioButton1.Checked Then
    '            If dtpicker(0) = dtpicker(3) And dtpicker(1) = dtpicker(4) And dtpicker(2) = dtpicker(5) Then
    '                Date_ = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2)
    '                xlsSheet.Range("G2").Value() = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2)

    '            Else
    '                Date_ = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2) & "__" & dtpicker(3) & "-" & dtpicker(4) & "-" & dtpicker(5)
    '                xlsSheet.Range("G2").Value() = dtpicker(0) & "-" & dtpicker(1) & "-" & "/" & dtpicker(3) & "-" & dtpicker(4) & "-" & dtpicker(5)
    '            End If
    '        End If


    '        If Form3.RadioButton2.Checked Then
    '            If dtpicker(0) = dtpicker(3) And dtpicker(1) = dtpicker(4) And dtpicker(2) = dtpicker(5) Then
    '                Date_ = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2)
    '                xlsSheet.Range("G2").Value() = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2)

    '            Else
    '                Date_ = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2) & "__" & dtpicker(3) & "-" & dtpicker(4) & "-" & dtpicker(5)
    '                xlsSheet.Range("G2").Value() = Form3.general_array(1, 0, i)
    '            End If
    '        End If



    '    Next i

    '    Form4.ProgressBar1.Value = 100
    '    ' Call Form3.saveas_pdf(Date_)
    '    If Exists(Form3.savepath_pdf) Then
    '        My.Computer.FileSystem.DeleteFile(Form3.savepath_pdf)
    '    End If

    '    xlsBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, Form3.savepath_pdf, XlFixedFormatQuality.xlQualityStandard, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
    '    'Me.Close()
    '    Form4.Close()
    '    'Form6.WebBrowser3.Navigate(Form3.savepath_pdf)
    '    'Form6.MdiParent = Me
    '    'Form6.Show()


    '    xlsBook.close()
    '    xlsBook = Nothing
    '    xlsBook2.close()
    '    xlsBook2 = Nothing
    '    xlsApp.quit()

    '    xlsApp = Nothing

    '    xlsSheet = Nothing

    '    xlsSheet2 = Nothing
    '    last_loop_ = Nothing


    '    rang_ = Nothing
    '    sheetindex = Nothing
    '    dtpicker = Nothing
    '    Date_ = Nothing

    'End Sub




    'Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
    '    Dim xlsApp As Object
    '    Dim xlsBook As Object
    '    Dim xlsSheet As Object
    '    Dim xlsBook2 As Object
    '    Dim xlsSheet2 As Object
    '    Dim last_loop_ As Boolean = False
    '    xlsApp = CreateObject("Excel.Application")
    '    xlsApp.Visible = False
    '    Dim rang_ As Integer
    '    Dim sheetindex As Integer
    '    Dim dtpicker(6) As String
    '    Dim Date_ As String


    '    xlsApp.DisplayAlerts = False

    '    Dim i As Integer
    '    Dim j As Integer

    '    Form4.Label1.Text = " Exporting ..."
    '    Form4.ProgressBar1.Value = 5
    '    Form4.Show()

    '    xlsBook2 = xlsApp.Workbooks.Open(Forms.Application.StartupPath & "\Daily.xlsx")
    '    xlsBook = xlsApp.workbooks.add

    '    xlsBook2.sheets("1").copy(after:=xlsBook.worksheets(xlsBook.worksheets.count))
    '    xlsBook.Sheets("sheet1").Delete()
    '    xlsSheet = xlsBook.worksheets(xlsBook.worksheets.count)

    '    For i = 0 To UBound(Form3.general_array, 3) - 1

    '        Form4.Label1.Text = " Exporting ... " & i & " / " & UBound(Form3.general_array, 3)
    '        Form4.ProgressBar1.Value = (i / UBound(Form3.general_array, 3)) * 100

    '        If i <> 0 Then
    '            sheetindex = xlsBook.worksheets.count
    '            xlsBook2.sheets("1").copy(after:=xlsBook.worksheets(sheetindex))
    '            xlsSheet = xlsBook.worksheets(xlsBook.worksheets.count)
    '        End If

    '        For k = 0 To 7

    '            '  shift 1
    '            rang_ = 7 + k
    '            xlsSheet.Range("D" & rang_.ToString).Value = Form3.general_array(k, 1, i)
    '            xlsSheet.Range("E" & rang_.ToString).Value = Form3.general_array(k, 2, i) / 100


    '            '  shift 2
    '            rang_ = 7 + k + 14
    '            xlsSheet.Range("D" & rang_.ToString).Value = Form3.general_array(k + 8, 1, i)
    '            xlsSheet.Range("E" & rang_.ToString).Value = Form3.general_array(k + 8, 2, i) / 100


    '            '  shift 3
    '            rang_ = 7 + k + 28
    '            xlsSheet.Range("D" & rang_.ToString).Value = Form3.general_array(k + 16, 1, i)
    '            xlsSheet.Range("E" & rang_.ToString).Value = Form3.general_array(k + 16, 2, i) / 100


    '        Next k

    '        xlsSheet.Range("B10").Value = Form3.general_array(3, 0, i)
    '        xlsSheet.Range("B11").Value = Form3.general_array(4, 0, i)
    '        xlsSheet.Range("B12").Value = Form3.general_array(5, 0, i)
    '        xlsSheet.Range("B13").Value = "Other"

    '        xlsSheet.Range("B24").Value = Form3.general_array(11, 0, i)
    '        xlsSheet.Range("B25").Value = Form3.general_array(12, 0, i)
    '        xlsSheet.Range("B26").Value = Form3.general_array(13, 0, i)
    '        xlsSheet.Range("B27").Value = "Other"

    '        xlsSheet.Range("B38").Value = Form3.general_array(19, 0, i)
    '        xlsSheet.Range("B39").Value = Form3.general_array(20, 0, i)
    '        xlsSheet.Range("B40").Value = Form3.general_array(21, 0, i)
    '        xlsSheet.Range("B41").Value = "Other"


    '        xlsSheet.Range("A2").Value() = Form3.general_array(0, 0, i)

    '        If Form3.RadioButton2.Checked Then
    '            xlsSheet.name = "Daily-" & Form3.general_array(0, 0, i) & "-" & Form3.general_array(1, 0, i)
    '            xlsSheet.Range("G2").Value() = Form3.general_array(1, 0, i)
    '        End If

    '        If Form3.RadioButton1.Checked Then
    '            xlsSheet.name = "Consolidated-" & xlsSheet.Range("A2").Value()
    '        End If

    '        dtpicker = Split(Form3.general_array(2, 0, i), "-")

    '        If Form3.RadioButton1.Checked Then
    '            If dtpicker(0) = dtpicker(3) And dtpicker(1) = dtpicker(4) And dtpicker(2) = dtpicker(5) Then
    '                Date_ = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2)
    '                xlsSheet.Range("G2").Value() = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2)

    '            Else
    '                Date_ = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2) & "__" & dtpicker(3) & "-" & dtpicker(4) & "-" & dtpicker(5)
    '                xlsSheet.Range("G2").Value() = dtpicker(0) & "-" & dtpicker(1) & "-" & "/" & dtpicker(3) & "-" & dtpicker(4) & "-" & dtpicker(5)
    '            End If
    '        End If


    '        If Form3.RadioButton2.Checked Then
    '            If dtpicker(0) = dtpicker(3) And dtpicker(1) = dtpicker(4) And dtpicker(2) = dtpicker(5) Then
    '                Date_ = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2)
    '                xlsSheet.Range("G2").Value() = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2)

    '            Else
    '                Date_ = dtpicker(0) & "-" & dtpicker(1) & "-" & dtpicker(2) & "__" & dtpicker(3) & "-" & dtpicker(4) & "-" & dtpicker(5)
    '                xlsSheet.Range("G2").Value() = Form3.general_array(1, 0, i)
    '            End If
    '        End If



    '    Next i

    '    Form4.ProgressBar1.Value = 100


    '    ' Call Form3.saveas_xlsx(Date_)
    '    If Exists(form3.savepath_xlsx) Then
    '        My.Computer.FileSystem.DeleteFile(form3.savepath_xlsx)
    '    End If
    '    xlsBook.saveas(form3.savepath_xlsx)


    '    Form4.Close()
    '    xlsBook.close()
    '    xlsBook = Nothing
    '    xlsBook2.close()
    '    xlsBook2 = Nothing
    '    xlsApp.quit()

    '    xlsApp = Nothing

    '    xlsSheet = Nothing

    '    xlsSheet2 = Nothing
    '    last_loop_ = Nothing


    '    rang_ = Nothing
    '    sheetindex = Nothing
    '    dtpicker = Nothing
    '    Date_ = Nothing

    'End Sub






    Private Sub DATAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DATAToolStripMenuItem.Click
        Dim dir_ As String



        FolderBrowserDialog1.Description = "Select a folder to export the datas"

        FolderBrowserDialog1.SelectedPath = Environment.SpecialFolder.Desktop
        FolderBrowserDialog1.ShowNewFolderButton = True

        FolderBrowserDialog1.ShowDialog()
        dir_ = FolderBrowserDialog1.SelectedPath

        '  Call export_(dir_)


        'Dim _thread As Thread
        '_thread = New Thread(Sub()
        '                         Using frm As New Form13
        '                             System.Windows.Forms.Application.Run(frm)
        '                         End Using
        '                     End Sub)
        '_thread.Priority = ThreadPriority.AboveNormal
        '_thread.Start()

        Call CSI_Lib.auto_report_day(chemin_bd, chemin_eNET, dir_, Form3.DateTimePicker1.Value.Day, Form3.DateTimePicker1.Value.Month, Form3.DateTimePicker1.Value.Year - 2000)
        '   _thread.Abort()


    End Sub



    Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'ToolStripStatusLabel2.Text = "Update in progress"
        If Directory.Exists(chemin_eNET & "\_REPORTS\") Then


            Dim files As String() = System.IO.Directory.GetFiles(chemin_eNET & "\_REPORTS\")
            Dim listofCSV As New List(Of String)

            Try

                For Each File In files
                    If System.IO.Path.GetFileName(File).StartsWith("_MACHINE") And System.IO.Path.GetExtension(File) = ".CSV" Then
                        listofCSV.Add(System.IO.Path.GetFileName(File))
                    End If

                Next


                listofCSV.Sort()
                listofCSV.Reverse()

                Dim file_ As String = listofCSV(0)
                Dim year As String() = listofCSV(0).Split("_")
                'minyear = String.Format("01/01/" & year(2)(0) & year(2)(1) & year(2)(2) & year(2)(3))
                'maxyear = String.Format("31/12/" & year(2)(0) & year(2)(1) & year(2)(2) & year(2)(3))
            Catch ex As Exception
                MessageBox.Show("Problem while reading the eNET report file : " & ex.Message)
            End Try

            If Not System.IO.File.Exists(chemin_bd & "\CSI_Database.mdb") Then  ' If the database file does not exist, => first creation

                first_creation_of_the_DB = True




                CSI_Lib.FirstFill_DB(chemin_bd, chemin_eNET, listofCSV(0))





                BackgroundWorker1.ReportProgress(99)
                ''''''
                Dim first As Boolean = True

                For Each item In listofCSV
                    If first = True Then
                        first = False
                    Else

                        CSI_Lib.not_first_update(chemin_bd, chemin_eNET, item, True)
                    End If
                Next

                BackgroundWorker1.ReportProgress(100)
                first_creation_of_the_DB = False
            Else
                Dim number_of_line_written As Integer
                Dim cnt As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & chemin_bd & "\CSI_Database.mdb;")
                cnt.Open()

                Dim cmd As New OleDbCommand("SELECT * FROM [tbl_Setup_]", cnt)
                Dim Reader As OleDbDataReader = cmd.ExecuteReader()

                Reader.Read()
                If (Reader.GetValue(1) Is DBNull.Value) Then
                    ' PB
                Else
                    number_of_line_written = Reader.GetValue(1)
                    If number_of_line_written = 0 Or number_of_line_written = 1 Then
                        number_of_line_written = 2
                    End If
                End If
                cnt.Close()

                If (number_of_line_written < 3) Then
                    System.IO.File.Delete(chemin_bd & "\CSI_Database.mdb")

                    first_creation_of_the_DB = True

                    CSI_Lib.FirstFill_DB(chemin_bd, chemin_eNET, listofCSV(0))

                    BackgroundWorker1.ReportProgress(99)
                    ''''''
                    Dim first As Boolean = True

                    For Each item In listofCSV
                        If first = True Then
                            first = False
                        Else

                            CSI_Lib.not_first_update(chemin_bd, chemin_eNET, item, True)
                        End If
                    Next

                    BackgroundWorker1.ReportProgress(100)
                    first_creation_of_the_DB = False
                Else
                    CSI_Lib.not_first_update(chemin_bd, chemin_eNET, listofCSV(0), False)
                End If

            End If

            'If Exists(chemin_bd & "\CSI_Database.mdb") Then
            '    Call CSI_Lib.Update_db(chemin_eNET, chemin_bd, True)
            '    first_creation_of_the_DB = False

            'Else
            '    first_creation_of_the_DB = True

            '    Call CSI_Lib.Update_db_reversed_first(chemin_eNET, chemin_bd, True)

            '    BackgroundWorker1.ReportProgress(99)

            '    Call CSI_Lib.Update_db_reversed_second(chemin_eNET, chemin_bd, True)
            '    BackgroundWorker1.ReportProgress(100)
            '    first_creation_of_the_DB = False
            'End If
        Else
            MessageBox.Show("the folder _REPORTS in the eNET folder is missing.")
        End If
    End Sub

    'Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
    '    If Directory.Exists(chemin_eNET & "\_REPORTS\") Then


    '        Dim files As String() = System.IO.Directory.GetFiles(chemin_eNET & "\_REPORTS\")
    '        Dim listofCSV As New List(Of String)

    '        Try

    '            For Each File In files
    '                If System.IO.Path.GetFileName(File).StartsWith("_MACHINE") And System.IO.Path.GetExtension(File) = ".CSV" Then listofCSV.Add(System.IO.Path.GetFileName(File))
    '            Next


    '            listofCSV.Sort()
    '            listofCSV.Reverse()

    '            Dim file_ As String = listofCSV(0)
    '            Dim year As String() = listofCSV(0).Split("_")
    '            'minyear = String.Format("01/01/" & year(2)(0) & year(2)(1) & year(2)(2) & year(2)(3))
    '            'maxyear = String.Format("31/12/" & year(2)(0) & year(2)(1) & year(2)(2) & year(2)(3))
    '        Catch ex As Exception
    '            MessageBox.Show("Problem while reading the eNET report file : " & ex.Message)
    '        End Try

    '        If Not System.IO.File.Exists(chemin_bd & "\CSI_Database.mdb") Then  ' If the database file does not exist, => first creation



    '            first_creation_of_the_DB = True
    '            CSI_Lib.FirstFill_DB(chemin_bd, chemin_eNET, listofCSV(0))
    '            BackgroundWorker1.ReportProgress(99)
    '            ''''''
    '            Dim first As Boolean = True

    '            For Each item In listofCSV
    '                If first = True Then
    '                    first = False
    '                Else
    '                    CSI_Lib.not_first_update(chemin_bd, chemin_eNET, item)
    '                End If
    '            Next

    '            BackgroundWorker1.ReportProgress(100)
    '            first_creation_of_the_DB = False
    '        Else

    '            CSI_Lib.not_first_update(chemin_bd, chemin_eNET, listofCSV(0))
    '        End If

    '        'If Exists(chemin_bd & "\CSI_Database.mdb") Then
    '        '    Call CSI_Lib.Update_db(chemin_eNET, chemin_bd, True)
    '        '    first_creation_of_the_DB = False

    '        'Else
    '        '    first_creation_of_the_DB = True

    '        '    Call CSI_Lib.Update_db_reversed_first(chemin_eNET, chemin_bd, True)

    '        '    BackgroundWorker1.ReportProgress(99)

    '        '    Call CSI_Lib.Update_db_reversed_second(chemin_eNET, chemin_bd, True)
    '        '    BackgroundWorker1.ReportProgress(100)
    '        '    first_creation_of_the_DB = False
    '        'End If
    '    Else

    '    End If
    'End Sub

    Private Sub NonModalMessageBox()
        MessageBox.Show("CSIFLEX has generated a database of 1 year, and will continue to generate in background. You can use it until it finishes. ", MsgBoxStyle.SystemModal)
        'StatusStrip1.Text = "Database optimisation in progres..."
    End Sub

    Private Sub BackgroundWorker1_progress_complete(sender As Object, e As ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        If e.ProgressPercentage = 99 Then

        End If

        If e.ProgressPercentage = 100 Then

        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Form3.Enabled = True
        SynchroDB.Close()
        ToolStripStatusLabel2.Text = ""
        '   Form12.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Form3.Show()
    End Sub

    Private Sub GenerateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerateToolStripMenuItem.Click
        '
        'If Form3.Visible = True Then
        '    GenerateToolStripMenuItem.Text = "Utilization reports"
        '    CF.Show()
        '    Form3.Visible = False


        'Else
        '    GenerateToolStripMenuItem.Text = "Compare mode"
        '    Form3.Show()
        '    Form3.MdiParent = Me
        '    Form3.Left = 0
        '    Form3.Top = 0
        '    Form3.Height = Me.Height - 90

        '    CF.Visible = False

        'End If

        'Form3.Show()
    End Sub
    'Private Sub form1_size(sender As Object, e As EventArgs) Handles Me.SizeChanged
    '    Form3.Height = Me.Height - 90
    'End Sub


    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click


    End Sub

    Private Sub ConfigurationToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConfigurationToolStripMenuItem1.Click
        ConfigurationToolStripMenuItem_Click(sender, e)
    End Sub


    Public Function installReportViewer() As Boolean
        Dim StartInfo As New System.Diagnostics.ProcessStartInfo
        Dim process_ As New Process
        Dim process2_ As New Process


        Dim path As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
        Dim commandSTRING As String

        If Not File.Exists(path & "\repview_needs.zip") Then
            IO.File.WriteAllBytes(path & "\repview_needs.zip", My.Resources.repview_needs)
        End If

        If Not (File.Exists(path & "\SQLSysClrTypes.msi") And File.Exists(path & "\ReportViewer.msi")) Then
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


        commandSTRING = "msiexec /i """ & path & "\SQLSysClrTypes.msi"" /quiet /qn /norestart /log """ & path & "/loginstallSqlClr.log"""
        SW.WriteLine(commandSTRING)
        SW.WriteLine("exit")
        Thread.Sleep(500)

        commandSTRING = "msiexec /i """ & path & "\ReportViewer.msi"" /quiet /qn /norestart /log """ & path & "/loginstallrepview.log"""
        SW2.WriteLine(commandSTRING)
        SW2.WriteLine("exit")






        Return True
    End Function

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
        ' This call is required by the designer.

        'Dim dllPath As String = Path.Combine(Directory.GetCurrentDirectory(), "CSIFPP.dll")
        'Assembly.LoadFile(dllPath)

        'EmbeddedAssembly.Load("CSI_Reporting_Application.Microsoft.VisualBasic.PowerPacks.dll", "Microsoft.VisualBasic.PowerPacks.dll")
        'EmbeddedAssembly.Load("CSI Reporting Application.System.Windows.Forms.Ribbon35.dll", "System.Windows.Forms.Ribbon35.dll")

        'AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve
        'AppDomain.CurrentDomain.AssemblyResolve += New ResolveEventHandler(CurrentDomain_AssemblyResolve)

        If Not (IsInstalled()) Then
            installReportViewer()
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
            generateCSVaccordingToForm3(folderDlg.FileName())
        End If


    End Sub




    Private Sub generateCSVaccordingToForm3(filePath As String)


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

        Dim machine() As String = Form3.read_tree()

        Dim command As String = ""

        Dim reader As OleDbDataReader
        Dim tmp_table_cmd As New OleDbCommand

        Dim bigTable, temp_table As New DataTable

        Dim firstFillorNot As Boolean = True


        Using sqlConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + CSI_Lib.db_path(True) + "\CSI_Database.mdb")
            sqlConn.Open()
            If machine.Length > 0 Then
                For i = 0 To UBound(machine)

                    temp_table = New DataTable

                    machine(i) = Strings.Replace(machine(i), " ", "")
                    machine(i) = Strings.Replace(machine(i), "-", "")

                    command = "SELECT '" + machine(i) + "' as MchName, month_, day_, year_, time_, date_,     iif(Status LIKE '_PARTNO:%', Mid(Status, 9) , status )  as status, shift, cycletime " +
                        " FROM  [tbl_" & machine(i) & "]  " +
                        " WHERE time_  between #" + Form3.DateTimePicker1.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "# and #" + Form3.DateTimePicker2.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "#  " +
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
    End Function



    Public Shared Sub Serialise(c As Control, XmlFileName As String)
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
            generateCSVaccordingToForm3(folderDlg.FileName())
        End If

    End Sub

    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click

    End Sub
End Class

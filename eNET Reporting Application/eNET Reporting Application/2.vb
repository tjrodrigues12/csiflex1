

#Region "Imports stuff"
Imports System
Imports System.Windows
Imports System.Reflection
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Management
Imports System.Diagnostics
Imports System.Threading
Imports System.ComponentModel
Imports Microsoft.Win32
Imports System.ServiceProcess
Imports System.Security.Permissions
Imports System.Collections.Specialized
Imports CSI_Library

'Imports CSi


Imports System.IO.File


Imports System.Xml
Imports System.Xml.XPath
Imports System.Xml.Schema

'Imports OpenNETCF.MTConnect
'Imports OpenNETCF.MTConnect.Client
'Imports OpenNETCF.MTConnect.EntityClient
'Imports OpenNETCF.Web
Imports System.Net
Imports System.IO.Pipes
Imports System.Text.RegularExpressions
Imports CSIServer

#End Region
'-----------------------------------------------------------------------------------------------------------------------
'Form 2 : Configuration
'-----------------------------------------------------------------------------------------------------------------------
Public Class Form2

#Region "Declarations"
    Private trd As Thread
    Public CSI_Lib As New CSI_Library.CSI_Library
    Public clt As New CSI_Library.Client
    Public NT As New CSI_Library.ServiceNT

    Public dbUpdate_needed As Boolean = False
    Public dbConnectStr As String
    Public tmp_txtbox1 As String
    Public tmp_txtbox2 As String
    Public targetColor_ As String
    Public identified As String
    Public loaded As Boolean = False
    Public userschanged As Boolean = False
    Public notfirstchangeusers As Boolean = False
    Public list2 As New List(Of String)

    Public fiiliing As Boolean = False ' =true whene user select a row , =false after tree3 been filled


#End Region

    'Public bgimg As Image = My.Resources.set_of_light_blue_background
    'Private Sub TreeView1_DrawNode(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawTreeNodeEventArgs) Handles TreeView1.DrawNode


    '    '   e.Graphics.DrawRectangle(Pens.OrangeRed, 0, 0, 40, 40)
    '    e.Graphics.DrawImage(bgimg, 0, 0, TreeView1.Width, TreeView1.Height)


    '    e.DrawDefault = True
    'End Sub



    '-----------------------------------------------------------------------------------------------------------------------
    ' Form2 Load
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ok = False

        'If ComboBox1.SelectedValue <> Nothing And ComboBox2.SelectedValue <> Nothing Then Button2.Enabled = True

        '  machineListe = CSI_Lib.LoadMachines()

        Try
            If File.Exists(Windows.Forms.Application.StartupPath & "\sys\Setup_.sys") Then
                Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\Setup_.sys")
                    tmp_txtbox1 = reader.ReadLine()
                    'TextBox1.Text = reader.ReadLine()
                    'tmp_txtbox1 = TextBox1.Text
                    reader.Close()
                End Using
            End If


            If File.Exists(Windows.Forms.Application.StartupPath & "\sys\Setupdb_.sys") Then
                Using reader2 As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\Setupdb_.sys")
                    TextBox2.Text = reader2.ReadLine()
                    tmp_txtbox2 = TextBox2.Text
                    reader2.Close()
                End Using
            End If

            Dim item As String = ""

            If File.Exists(Windows.Forms.Application.StartupPath & "\sys\Setupdt_.sys") Then
                Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\Setupdt_.sys")
                    item = reader.ReadLine()
                    If item IsNot Nothing Then
                        ComboBox1.SelectedIndex = (Val(item(0)))
                        ComboBox2.SelectedIndex = (Val(item(1)))
                    End If
                    reader.Close()
                End Using
            End If

            If File.Exists(Windows.Forms.Application.StartupPath & "\sys\auto_.sys") Then
                Using reader3 As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\auto_.sys")
                    TextBox3.Text = reader3.ReadLine()
                    '   Dim nothing_ As String = reader3.ReadLine()
                    '  DateTimePicker1.Value = ("01/01/2000 " & Format(reader3.ReadLine(), "HH:mm"))

                    CheckBox1.Checked = True
                    reader3.Close()
                End Using
            End If


            Try
                Try
                    Dim theservice As ServiceController = New ServiceController("CSIFLEX Server")
                    If theservice.Status = ServiceControllerStatus.StopPending Or theservice.Status = ServiceControllerStatus.Stopped Then

                        Button13.Text = "Start CSIFLEX Server"
                        Button13.BackColor = Color.Transparent
                    Else

                        Button13.Text = "Stop CSIFLEX Server"
                        Button13.BackColor = Color.GreenYellow
                    End If
                Catch ex As Exception
                    Button13.Text = "Start CSIFLEX Server"
                    Button13.BackColor = Color.Transparent
                End Try
                'If NT.srv.listning = True Then
                '    Button13.Text = "Stop CSIFLEX Server"
                '    Button13.BackColor = Color.GreenYellow
                'Else
                '    Button13.Text = "Start CSIFLEX Server"
                '    Button13.BackColor = Color.Transparent
                'End If
            Catch ex As Exception
                MsgBox("Unable to have the state of the CSIFLEX Server : " & ex.Message)
            End Try


            Try

                If File.Exists(Windows.Forms.Application.StartupPath & "\sys\Network_.sys") Then
                    Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\Network_.sys")
                        Me.TextBox4.Text = reader.ReadLine
                        reader.Close()
                    End Using
                End If

                If File.Exists(Windows.Forms.Application.StartupPath & "\sys\Networkenet_.sys") Then
                    Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\Networkenet_.sys")
                        Me.TextBox6.Text = reader.ReadLine
                        reader.Close()
                    End Using
                End If

            Catch ext As Exception
                MsgBox("Unable to read the ip of the CSIFLEX or eNET server : " & ext.Message)
            End Try

            Call check_licence()
            Call Load_groupes()
            Call Load_sources()
            Call load_users()
            Call Load_datagridview()

        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Close()
        End Try
    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' Form2 Ok
    '-----------------------------------------------------------------------------------------------------------------------
    Public ok As Boolean = False

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click




        'Try
        '    If Exists(System.Windows.Forms.Application.StartupPath & "\sys\target_.sys") Then
        '        System.IO.File.Delete(Windows.Forms.Application.StartupPath & "\sys\target_.sys")
        '        Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\target_.sys")
        '            writer.Write(NumericUpDown1.Value)
        '            writer.Close()
        '        End Using
        '    Else
        '        Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\target_.sys")
        '            writer.Write(NumericUpDown1.Value)
        '            writer.Close()
        '        End Using
        '    End If
        '    Form9.target_ = CInt(NumericUpDown1.Value)
        'Catch ex As Exception
        '    MsgBox("Unable to save the target value : " & ex.Message, vbSystemModal + vbCritical)
        'End Try


        'Try
        '    Form1.chemin_eNET = TextBox1.Text
        '    Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\Setup_.sys")
        '        '  writer.Write(TextBox1.Text)
        '        writer.Close()
        '    End Using
        'Catch ex As Exception
        '    MsgBox("Unable to save the eNET path : " & ex.Message)
        'End Try

        Try
            Form1.chemin_bd = TextBox2.Text
            Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\Setupdb_.sys")
                writer.Write(TextBox2.Text)
                writer.Close()
            End Using
        Catch ex As Exception
            MsgBox("Unable to save the database path : " & ex.Message)
        End Try

        Try

            Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\db_update_rate_.sys")
                writer.Write(NumericUpDown2.Value)
                writer.Close()
            End Using
        Catch ex As Exception
            MsgBox("Unable to save the database update rate : " & ex.Message)
        End Try


        saveDatagridview1()

        'Install Rainmeter
        'If CheckBox5.Checked = True Then
        '    Try
        '        Dim CMDThread As New System.Threading.Thread(AddressOf RM_install)
        '        CMDThread.Start()
        '    Catch ex As Exception
        '        MsgBox("The Rainmeter installation has failed. " & ex.Message)
        '    End Try

        'End If


        'If Form1.bySetup = False Or (tmp_txtbox2 <> TextBox2.Text) Then
        '    Try

        '        If Form1.BackgroundWorker1.IsBusy <> True Then
        '            ' Start the asynchronous operation.
        '            Form1.BackgroundWorker1.RunWorkerAsync()
        '        End If



        '        Form12.Show()

        '    Catch ext As Exception
        '        MsgBox(ext.Message)
        '    End Try
        'End If

        Try

            Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\Network_.sys", False)
                writer.Write(Me.TextBox4.Text)
                writer.Close()
            End Using
            Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\Networkenet_.sys", False)
                writer.Write(Me.TextBox6.Text)
                writer.Close()
            End Using



        Catch ext As Exception
            MsgBox("Unable to save the ip of the CSIFLEX server : " & ext.Message)
        End Try

        Try
            If ComboBox1.SelectedIndex <> -1 And ComboBox2.SelectedIndex <> -1 Then
                If File.Exists(Windows.Forms.Application.StartupPath & "\sys\Setupdt_.sys") Then
                    System.IO.File.Delete(Windows.Forms.Application.StartupPath & "\sys\Setupdt_.sys")
                End If

                Using writer As StreamWriter = New StreamWriter(Windows.Forms.Application.StartupPath & "\sys\Setupdt_.sys")
                    writer.Write(ComboBox1.SelectedIndex & ComboBox2.SelectedIndex)
                    writer.Close()
                End Using
                Form1.week_ = ComboBox1.SelectedIndex & ComboBox2.SelectedIndex
                ok = True
                Me.Close()
            Else
                MsgBox("Please, Specify a week start day and end day")
            End If
            Call auto()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'If Checkpasswords_DGV2() Then
        '    save_users()
        '    '    load_users()
        'End If

    End Sub

    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) _
     Handles MyBase.FormClosing
        If ok = True Then
            If Checkpasswords_DGV2() = False Then
                e.Cancel = True   'stop the form from closing
                GoTo end___
            End If

            save_groupes(Nothing)
            save_sources()
            save_users()

            UserLogin.Close()

            ok = False
        End If
end___:

    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' Form2 Cancel
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        ok = False
    End Sub



#Region "Browse"
    ''-----------------------------------------------------------------------------------------------------------------------
    ''  Browse, eNET folder
    ''-----------------------------------------------------------------------------------------------------------------------
    'Private Sub Button1_Click(sender As Object, e As EventArgs)
    '    Dim folderDlg As New FolderBrowserDialog
    '    folderDlg.Description = "Specify the eNET folder"

    '    Try
    '        folderDlg.ShowNewFolderButton = False
    '        If (folderDlg.ShowDialog() = DialogResult.OK) Then
    '            TextBox1.Text = folderDlg.SelectedPath
    '            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub





    '-----------------------------------------------------------------------------------------------------------------------
    '  Browse, db folder
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim folderDlg As New FolderBrowserDialog

        folderDlg.Description = "Choose or Specify a folder for the database"
        Try
            folderDlg.ShowNewFolderButton = True
            If (folderDlg.ShowDialog() = DialogResult.OK) Then
                TextBox2.Text = folderDlg.SelectedPath
                Dim root As Environment.SpecialFolder = folderDlg.RootFolder
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    '-----------------------------------------------------------------------------------------------------------------------
    ' Default chemin enet
    '-----------------------------------------------------------------------------------------------------------------------
    'Private Sub Button5_Click(sender As Object, e As EventArgs)
    '    TextBox1.Text = "C:\_eNETDNC"
    'End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' Default chemin bdd
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Try

            TextBox2.Text = Forms.Application.StartupPath & "\Database\"
            Form1.chemin_bd = TextBox2.Text

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' Browse autoreport folder
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Button8_Click(sender As Object, e As EventArgs)
        Dim folderDlg As New FolderBrowserDialog

        folderDlg.ShowNewFolderButton = False
        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            TextBox3.Text = folderDlg.SelectedPath
            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
        End If


        Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\Setup_Export.sys")
            writer.Write(TextBox3.Text)
            writer.Close()
        End Using
    End Sub
#End Region


    Public Function mois_(MAXMOI As Integer)
        Try
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
                Case Else
                    Return Nothing
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public machineListe As String() ' machine liste from eNET


    '===================================================================================================
    ' check the user Licence  
    '===================================================================================================
    Private Sub check_licence()
        If File.Exists("C:\Windows\System32\wincsi.dl1") Then
            Dim readed As String = ""
            Using reader As StreamReader = New StreamReader("C:\Windows\System32\wincsi.dl1")
                Dim crypted As String = reader.ReadToEnd
                readed = CSI_Lib.AES_Decrypt(crypted, "pass")
            End Using
            Dim data__ As String() = readed.Split(vbCrLf)
            ' data__ contains :
            '==================================
            ' date / validity periode
            'option 1
            'option 2 
            '...
            '==================================

            ' CSIFLEX will check after if the date has been changed or nor with the dates in the database or enet files.
            Dim decrypted As String = CSI_Lib.AES_Decrypt(data__(0), "pass")

            Label8.Text = "Licence validity :  " & Convert.ToDateTime(decrypted).Date.ToString
            If DateDiff(DateInterval.Year, Convert.ToDateTime(decrypted).Date, Now) > 100 Then Label8.Text = "Unlimited Licence"

        Else

        End If
    End Sub


    '===================================================================================================
    ' Show Licence window
    '===================================================================================================
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        LoginForm.ShowDialog()
    End Sub










#Region "Auto reporting"
    '-----------------------------------------------------------------------------------------------------------------------
    ' Form2 auto reporting
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub auto()
        If CheckBox1.Checked = False Then ' No AutoReports
            If File.Exists(Windows.Forms.Application.StartupPath & "\sys\Auto_.sys") Then
                System.IO.File.Delete(Windows.Forms.Application.StartupPath & "\sys\Auto_.sys")
            End If

            'Stop the service
            Try
                Dim sc As New ServiceController("CSIservice")
                Dim present As Boolean = False
                Dim services() As ServiceController = ServiceController.GetServices
                For Each service In services
                    If service.DisplayName = "CSIservice" Then
                        present = True
                        GoTo continue_
                    End If
                Next
                If present = False Then GoTo NotContinue

continue_:
                If sc.Status.Equals(ServiceControllerStatus.Stopped) Or sc.Status.Equals(ServiceControllerStatus.StopPending) Then
                Else
                    sc.Stop()
                    Process.Start("cmd.exe", "/c sc delete CSIservice")
                End If
NotContinue:
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Else
            Dim lastdate As String = ""

            If File.Exists(Windows.Forms.Application.StartupPath & "\CSIservice.exe") Then
            Else

                Dim path_exe As String = System.Windows.Forms.Application.StartupPath & "\CSIservice.exe"
                path_exe = System.Windows.Forms.Application.StartupPath & "\CSIservice.exe"
                IO.File.WriteAllBytes(path_exe, My.Resources.CSIService)
                path_exe = System.Windows.Forms.Application.StartupPath & "\srvany.exe"
                IO.File.WriteAllBytes(path_exe, My.Resources.CSIService)
                path_exe = System.Windows.Forms.Application.StartupPath & "\Instsrv.exe"
                IO.File.WriteAllBytes(path_exe, My.Resources.Instsrv)



            End If

            If TextBox3.Text = "" Then
                MsgBox("Please specify a reporting folder")
                Me.Show()
            Else
                If File.Exists(Windows.Forms.Application.StartupPath & "\sys\Auto_.sys") Then
                    Using reader As StreamReader = New StreamReader(Windows.Forms.Application.StartupPath & "\sys\Auto_.sys")
                        lastdate = reader.ReadLine()
                        lastdate = reader.ReadLine()
                        reader.Close()
                    End Using
                    System.IO.File.Delete(Windows.Forms.Application.StartupPath & "\sys\Auto_.sys")
                End If

                Using writer As StreamWriter = New StreamWriter(Windows.Forms.Application.StartupPath & "\sys\Auto_.sys")
                    writer.Write(TextBox3.Text & vbCrLf)
                    If lastdate <> "" Then

                        writer.Write(lastdate & vbCrLf)
                    Else
                        writer.Write(String.Format("{0:dd/MM/yyyy}", DateTime.Now) & vbCrLf)
                    End If

                    If DateTimePicker1.Value.Minute >= 10 Then
                        writer.Write(DateTimePicker1.Value.Hour.ToString & ":" & DateTimePicker1.Value.Minute.ToString)
                    Else
                        writer.Write(DateTimePicker1.Value.Hour.ToString & ":0" & DateTimePicker1.Value.Minute.ToString)
                    End If

                    writer.Close()

                End Using

                Call install_start_Service()

            End If
        End If

    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' Service install and start
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub install_start_Service()
        Try
            'Create the bat file
            If (File.Exists(Forms.Application.StartupPath & "\Install_as_Service.bat")) Then
                File.Delete(Forms.Application.StartupPath & "\Install_as_Service.bat")
            End If

            Using sw As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\Install_as_Service.bat")
                sw.WriteLine("instsrv CSIservice " & """" & Forms.Application.StartupPath & "\srvany.exe""") '& vbLf & "TIMEOUT /T 10" & vbLf & "sc.exe config CSIservice obj= localsystem")
                sw.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
            GoTo Stop_
        End Try

        Try
            'lanche the bat file
            Dim psi As New ProcessStartInfo(Forms.Application.StartupPath & "\Install_as_Service.bat")

            psi.RedirectStandardError = True
            psi.RedirectStandardOutput = True
            psi.CreateNoWindow = False
            psi.WindowStyle = ProcessWindowStyle.Hidden
            psi.UseShellExecute = False


            Dim process As Process = process.Start(psi)

            Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\CSIservice", True)

            key.SetValue("ObjectName", "Localsystem")
            key = key.CreateSubKey("Parameters")
            key.SetValue("Application", Forms.Application.StartupPath & "\CSIservice.exe")
            key.Close()

            Dim sc As New ServiceController("CSIservice")
            Dim account As New ProcessStartInfo()
            If sc.Status.Equals(ServiceControllerStatus.Stopped) Or sc.Status.Equals(ServiceControllerStatus.StopPending) Then sc.Start()

            process.Start("cmd.exe", "/c sc description CSIservice ""Cam Solution Real time Intelligent Reporting application service"" ")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
stop_:
    End Sub



    '-----------------------------------------------------------------------------------------------------------------------
    ' Activat/Deactivat the autoreporting checkbox if no path
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs)
        If CheckBox1.Text <> "" Then
            CheckBox1.Enabled = True
        Else
            CheckBox1.Enabled = False
        End If
    End Sub


#End Region


    '#Region "targer line"
    '    '===================================================================================================
    '    ' Set the target line color
    '    '===================================================================================================
    '    Private Sub Button10_Click(sender As Object, e As EventArgs)
    '        Dim colordial As New ColorDialog
    '        colordial.ShowDialog()
    '        If Form9.Visible = True Then Form9.target(NumericUpDown1.Value, colordial.Color.ToArgb)

    '        Try
    '            If Exists(System.Windows.Forms.Application.StartupPath & "\sys\targetColor_.sys") Then
    '                System.IO.File.Delete(Windows.Forms.Application.StartupPath & "\sys\targetColor_.sys")

    '                Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\targetColor_.sys")
    '                    writer.Write(colordial.Color.ToArgb)
    '                    writer.Close()
    '                End Using
    '            Else
    '                Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\targetColor_.sys")
    '                    writer.Write(colordial.Color.ToArgb)
    '                    writer.Close()
    '                End Using
    '            End If

    '            '  Button10.BackColor = Color.FromArgb(colordial.Color.ToArgb)
    '        Catch ex As Exception
    '            MsgBox("Unable to save the color of the target line : " & ex.Message, vbSystemModal + vbCritical)
    '        End Try
    '    End Sub

    '    ''===================================================================================================
    '    '' Target line number 
    '    ''===================================================================================================
    '    'Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs)
    '    '    If Form9.Visible = True Then Form9.target(NumericUpDown1.Value, Button10.BackColor.ToArgb)
    '    'End Sub

    '    '===================================================================================================
    '    ' Remove the settings for the target line
    '    '===================================================================================================
    '    Private Sub Button9_Click(sender As Object, e As EventArgs)
    '        Try
    '            If Exists(System.Windows.Forms.Application.StartupPath & "\sys\target_.sys") Then
    '                System.IO.File.Delete(Windows.Forms.Application.StartupPath & "\sys\target_.sys")
    '            End If
    '        Catch ex As Exception
    '            MsgBox("Unable to save the target value : " & ex.Message, vbSystemModal + vbCritical)
    '        End Try
    '    End Sub

    '#End Region








#Region "Machines connection setup"


    Private Sub DataGridView1_Cellchange(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        If DataGridView1.Columns(e.ColumnIndex).HeaderText = "Source" Then

            Dim check As Action(Of Object) = Sub(s As Object)
                                                 CheckConnection(DirectCast(e, DataGridViewCellEventArgs))
                                             End Sub

            Dim task As New Task(check, e)
            task.Start()

        End If
    End Sub

    Private Sub CheckConnection(e As DataGridViewCellEventArgs)
        Dim devicename As String = Edit_mtconnect.devicename

        If (Not IsNothing(DataGridView1.Rows(e.RowIndex).Cells(2).Value)) Then
            If (DataGridView1.Rows(e.RowIndex).Cells(2).Value <> "eNET") Then
                If Not DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString.StartsWith("http://") Then DataGridView1.Rows(e.RowIndex).Cells(2).Value = "http://" & DataGridView1.Rows(e.RowIndex).Cells(2).Value
                If mtconnect_ping(DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString) = True Then
                    DataGridView1.Rows(e.RowIndex).Cells(4).Value = "Passed"
                    DataGridView1.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.GreenYellow
                    If devicename.ToUpper <> DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString.ToUpper Then

                        Select Case MsgBox("CSIFLEX has detected that the machine " & devicename & "has another name in the MTConnect Agent, witch is " & devicename & vbCrLf & vbCrLf & _
                                           "Do you want to keep the default name ?  ", MsgBoxStyle.YesNo, "Machine Name Conflict")
                            Case MsgBoxResult.Yes
                                DataGridView1.Rows(e.RowIndex).Cells(1).Value = devicename
                            Case MsgBoxResult.No
                                ' Do something if no
                        End Select

                    End If

                Else
                    DataGridView1.Rows(e.RowIndex).Cells(4).Value = "OffLine"
                    DataGridView1.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Red
                End If
            Else
                Dim dt As DataTable = clt.Run(TextBox6.Text)
                If dt IsNot Nothing Then

                    For Each machine In dt.Rows
                        DataGridView1.Rows(e.RowIndex).Cells(4).Value = "offLine"
                        DataGridView1.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Red
                        If (machine.item("machine")) = DataGridView1.Rows(e.RowIndex).Cells(1).Value Then
                            If machine.item("status") <> "NO EMONITOR" Then
                                DataGridView1.Rows(e.RowIndex).Cells(4).Value = "Passed"
                                DataGridView1.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.GreenYellow
                                Exit For
                            End If

                        End If
                    Next

                Else

                End If
            End If
        End If
    End Sub
    '===================================================================================================
    ' CHECK THE CONNECTION
    '===================================================================================================
    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

        Try
            If (e.ColumnIndex = DataGridView1.Columns("Check").Index And e.RowIndex >= 0) Then

                Dim check As Action(Of Object) = Sub(s As Object)
                                                     CheckConnection(DirectCast(e, DataGridViewCellEventArgs))
                                                 End Sub

                Dim task As New Task(check, e)
                task.Start()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        '   https://www.youtube.com/watch?v=CHyESZfaxxE
    End Sub


    '===================================================================================================
    ' Ping ip:port or ip or uri
    '===================================================================================================

    Public Function mtconnect_ping(ip As String)

        Try
            Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(ip), HttpWebRequest)
            ' Set the  'Timeout' property of the HttpWebRequest to 2000 milliseconds.
            myHttpWebRequest.Timeout = 2000
            ' A HttpWebResponse object is created and is GetResponse Property of the HttpWebRequest associated with it  
            Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
            If myHttpWebResponse.StatusCode = HttpStatusCode.OK Then
                'Dim ds As New DataSet

                'Dim m_client As New EntityClient(ip)
                'Dim p As DeviceCollection = m_client.Probe
                'Dim device_Name As String = ""
                'For Each item__ In p
                '    devicename = item__.Name
                'Next


                Return True
            Else
                MsgBox("CSIFLEX cannot connect to the MTConnect Agent : " & myHttpWebResponse.StatusDescription)
                Return False
            End If

        Catch ex As Exception
            If ex.Message = "The operation has timed out" Then
                Return False
            Else
                MsgBox("CSIFLEX cannot reach the MTConnect Agent : " & ex.Message)
                Return False
            End If
        End Try

    End Function

    '===================================================================================================
    ' add a new row in the datagridview1
    '===================================================================================================
    Public loadingmachines As Boolean = False
    Private Sub DataGridView1_newrow(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded

        If Not LoadingSources And Not loadingmachines Then
            'Last Row:
            DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(3).Value = "Check"
        End If
    End Sub


    '===================================================================================================
    '  Save the data
    '===================================================================================================
    Private Sub saveDatagridview1()

        Using writer As StreamWriter = New StreamWriter(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys", False)
            Dim display_CheckBox As String = ""

            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not IsNothing(row.Cells(1).Value) Then ' DONT TAKE AN EMPTY ROW
                    If row.Cells(0).Value = False Then
                        display_CheckBox = 0
                    Else
                        display_CheckBox = 1
                    End If
                    writer.WriteLine(row.Cells(1).Value.ToString.Replace(" ", "").Replace(vbCr, "").Replace(vbCrLf, "") & "," & row.Cells(2).Value & "," & display_CheckBox.ToString)
                End If
            Next
            writer.Close()
        End Using
    End Sub




    '===================================================================================================
    ' Select the Machines tab
    '===================================================================================================
    Private Sub TabControl1_select(sender As Object, e As EventArgs) _
        Handles TabControl1.SelectedIndexChanged
        If (TabControl1.SelectedTab.Text) = "Machines" Then
            '    Call Load_datagridview()
            Button12.Visible = True
            Button14.Visible = True

            'For Each node As TreeNode In TreeView2.Nodes(0).Nodes



            '    Dim item_ As New ToolStripMenuItem(node.Text, Nothing, AddressOf ClickHandler, node.Text)
            '    item_.Text = node.Text
            '    ' ContextMenuStrip1.Items
            '    ContextMenuStrip1.Items.Add(item_)
            '    add_menu.DropDownItems.Add(item_)

            'Next
        Else
            Button12.Visible = False
            Button14.Visible = False
        End If


        If TabControl1.SelectedTab.Text = "Users" Then
            'load_users()
            TreeView3.Nodes.Clear()
            For Each node In TreeView2.Nodes
                TreeView3.Nodes.Add(node.clone)
            Next

        End If

        If TabControl1.SelectedTab.Text = "Servers" Then

            ' check_srv()
        End If

    End Sub

    Private Sub autodiag()

    End Sub
    Private Sub check_srv()
        ListBox1.Items.Clear()

        If File.Exists(System.Windows.Forms.Application.StartupPath & "\Log_Server.txt") Then
            Dim reader As StreamReader = New StreamReader(System.Windows.Forms.Application.StartupPath & "\Log_Server.txt")
            While Not reader.EndOfStream
                Dim line As String = reader.ReadLine.ToString
                ListBox1.Items.Add(line)
            End While
            reader.Close()
        End If
        Dim c As String(,) = CSI_Lib.check_server

        If IsNothing(c) Then
        Else


            DataGridView3.DataSource = Nothing

            If DataGridView3.Columns.Count = 0 Then
                DataGridView3.Columns.Add("Task", "Task")
                DataGridView3.Columns.Add("Status", "Status")
            Else
                For i = 0 To 4
                    DataGridView3.Rows.Remove(DataGridView3.Rows(4 - i))
                Next
            End If

            For i = 0 To 4
                DataGridView3.Rows.Add(c(i, 0), (c(i, 1)))
                If (c(i, 1).ToString) <> "Running" Then
                    DataGridView3.Rows(i).Cells(1).Style.BackColor = Color.Red
                    DataGridView3.Rows(i).Cells(0).Style.BackColor = Color.Red
                Else
                    DataGridView3.Rows(i).Cells(1).Style.BackColor = Color.YellowGreen
                    DataGridView3.Rows(i).Cells(0).Style.BackColor = Color.YellowGreen
                End If
            Next
            DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        End If
    End Sub

    Private Sub addmenu(text As String)
        Dim ToolStripMenuItem As New ToolStripMenuItem(text)

        ToolStripMenuItem.Text = text

    End Sub


    '===================================================================================================
    ' Fill the datagridview
    '===================================================================================================
    Private Sub Load_datagridview()
        Dim currentrow As String()
        Dim readed As String = ""
        Dim chked As New DataGridViewCheckBoxCell
        chked.Value = False
        DataGridView1.Rows.Clear()


        If Exists(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys") Then
            Using reader As StreamReader = New StreamReader(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys")
                While Not reader.EndOfStream
                    readed = reader.ReadLine()
                    If readed <> "" Then
                        currentrow = readed.Split(",")
                        If currentrow(0) <> "" Then
                            If currentrow(2) = 1 Then chked.Value = True
                            DataGridView1.Rows.Add(chked.Value, currentrow(0), currentrow(1), "Check", "")
                            '   machinesTBL.Rows.Add(chked.Value, currentrow(0), currentrow(1), "Check", "")
                        End If
                    End If
                End While
            End Using
        End If



    End Sub

    Private Sub Load_eNET_datagridview()
        Dim currentrow As String()
        Dim readed As String = ""
        Dim chked As New DataGridViewCheckBoxCell
        chked.Value = False
        DataGridView1.Rows.Clear()


        If Exists(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys") Then
            Using reader As StreamReader = New StreamReader(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys")
                While Not reader.EndOfStream
                    readed = reader.ReadLine()
                    If readed <> "" Then
                        currentrow = readed.Split(",")
                        If currentrow(0) <> "" Then
                            If currentrow(2) = 1 Then chked.Value = True
                            If currentrow(1) = "eNET" Then DataGridView1.Rows.Add(chked.Value, currentrow(0), currentrow(1), "Check", "")


                            '   machinesTBL.Rows.Add(chked.Value, currentrow(0), currentrow(1), "Check", "")
                        End If
                    End If
                End While
            End Using
        End If



    End Sub

    Private Sub Load_mtConnect_datagridview()
        Dim currentrow As String()
        Dim readed As String = ""
        Dim chked As New DataGridViewCheckBoxCell
        chked.Value = False
        DataGridView1.Rows.Clear()


        If Exists(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys") Then
            Using reader As StreamReader = New StreamReader(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys")
                While Not reader.EndOfStream
                    readed = reader.ReadLine()
                    If readed <> "" Then
                        currentrow = readed.Split(",")
                        If currentrow(0) <> "" Then
                            If currentrow(2) = 1 Then chked.Value = True
                            If currentrow(1).StartsWith("http") Then DataGridView1.Rows.Add(chked.Value, currentrow(0), currentrow(1), "Check", "")


                            '   machinesTBL.Rows.Add(chked.Value, currentrow(0), currentrow(1), "Check", "")
                        End If
                    End If
                End While
            End Using
        End If



    End Sub

    Private Sub Load_log_datagridview()
        Dim currentrow As String()
        Dim readed As String = ""
        Dim chked As New DataGridViewCheckBoxCell
        chked.Value = False
        DataGridView1.Rows.Clear()


        If Exists(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys") Then
            Using reader As StreamReader = New StreamReader(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys")
                While Not reader.EndOfStream
                    readed = reader.ReadLine()
                    If readed <> "" Then
                        currentrow = readed.Split(",")
                        If currentrow(0) <> "" Then
                            If currentrow(2) = 1 Then chked.Value = True
                            If currentrow(1).StartsWith("Log") Then DataGridView1.Rows.Add(chked.Value, currentrow(0), currentrow(1), "Check", "")


                            '   machinesTBL.Rows.Add(chked.Value, currentrow(0), currentrow(1), "Check", "")
                        End If
                    End If
                End While
            End Using
        End If



    End Sub


    '===================================================================================================
    ' If change occur in the datagridview 2
    '===================================================================================================
    Private Sub CellValueChanged(ByVal sender As Object, _
    ByVal e As DataGridViewCellEventArgs) Handles DataGridView2.CellValidated

        If DGV2MOD = False Then


            If DataGridView2.Columns(e.ColumnIndex).Name = "password_" Then
                If Not (DataGridView2.Rows(e.RowIndex).Cells.Item("password_").Value Is Nothing _
                 Or IsDBNull(DataGridView2.Rows(e.RowIndex).Cells.Item("password_").Value)) Then
                    If Not CSI_Lib.IsAlphaNumeric(DataGridView2.Rows(e.RowIndex).Cells.Item("password_").Value) Then
                        MsgBox("CSIFLEX accepts only AlphaNumerical passwords, please change the password of the user " & DataGridView2.Rows(e.RowIndex).Cells.Item("username_").Value)
                        Exit Sub
                    End If
                    If DataGridView2.Rows(e.RowIndex).Cells.Item("password_").Value.ToString.Count < 3 Then
                        MsgBox("CSIFLEX accepts only passwords of 4 caracteres and more, please change the password of the user " & DataGridView2.Rows(e.RowIndex).Cells.Item("username_").Value)
                        Exit Sub
                    End If
                End If
            End If



            Call check_Datagridview2()

            ' todo after the first time (wich is the first fill of the datagridview2)
            If notfirstchangeusers = True Then
                'PictureBox6.Visible = True
                'Button20.Visible = True
                PictureBox7.Visible = True
                Button23.Visible = True
            End If
            notfirstchangeusers = True

        End If
    End Sub


    'Private Sub connect()
    '    Try
    '        Dim m_client As New EntityClient(TextBox1.Text)

    '        Button1.Enabled = False

    '        Dim devices = m_client.Probe()
    '        '  PopulateTree(devices, TreeView1)

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
#End Region




    '===================================================================================================
    ' START CSIFLEX SERVER
    '===================================================================================================
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        '
        Try
start__:
            Dim services As ServiceController(), running As Boolean = False, _IsInstalled As Boolean = False
            services = ServiceController.GetServices()


            For Each service As ServiceController In services
                If (service.ServiceName.Equals("CSIFLEX Server")) Then
                    _IsInstalled = True
                    If (service.Status = ServiceControllerStatus.ContinuePending) Or (service.Status = ServiceControllerStatus.Paused) Or _
                         (service.Status = ServiceControllerStatus.PausePending) Or (service.Status = ServiceControllerStatus.StartPending) Then

                        ' If the status is StartPending then the service was started via the SCM             
                        running = True
                    End If
                    Exit For
                End If
            Next




            If _IsInstalled Then
                Dim theservice As ServiceController = New ServiceController("CSIFLEX Server")

                If theservice.Status = ServiceControllerStatus.StopPending Or theservice.Status = ServiceControllerStatus.Stopped Then
                    MsgBox("CSIFLEX will start the server service ", MsgBoxStyle.Information, "Information")
                    theservice.Start()
                    ' Started from the SCM
                    'Dim servicestorun As System.ServiceProcess.ServiceBase()
                    'servicestorun = New System.ServiceProcess.ServiceBase() {New CSIServer.CSIServer()}
                    'ServiceBase.Run(servicestorun)
                    theservice = New ServiceController("CSIFLEX Server")
                    If theservice.Status = ServiceControllerStatus.StopPending Or theservice.Status = ServiceControllerStatus.Stopped Then
                        MsgBox("The service had been installed but could not start, try to manually start it via 'services.msc' for more informations", MsgBoxStyle.Information, "Information")
                        Button13.Text = "Start CSIFLEX Server"
                        Button13.BackColor = Color.GreenYellow
                    Else
                        MsgBox("The service is running", MsgBoxStyle.Information, "Information")
                        Button13.Text = "Stop CSIFLEX Server"
                        Button13.BackColor = Color.Red
                    End If

                Else
                    theservice.Stop()
                    Button13.Text = "Start CSIFLEX Server"
                    Button13.BackColor = Color.GreenYellow
                End If
            Else

                Select Case MsgBox("CSIFLEX Will install it self as a service ", MsgBoxStyle.YesNo, " ? ")
                    Case MsgBoxResult.Yes
                        SelfInstaller.InstallMe()
                        MessageBox.Show("Successfully installed the service. ", "Status",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Case Else
                End Select
                GoTo Start__
            End If


            check_srv()


        Catch ex As ArgumentException
            MsgBox(ex.Message)
            check_srv()
        End Try



    End Sub




    '===================================================================================================
    ' ReloAD eNET MACHINES
    '===================================================================================================
    Private Sub Button12_Click_1(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            Dim machines As String() = CSI_Lib.LoadMachines()
redo:

            Dim rowscount As Integer = (DataGridView1.Rows.Count) - 1
            For i = 0 To rowscount
                If DataGridView1.Rows.Item(i).Cells.Item(2).Value IsNot Nothing Then
                    If DataGridView1.Rows.Item(i).Cells.Item(2).Value.ToString = "eNET" Then
                        DataGridView1.Rows.Remove(DataGridView1.Rows(i))
                        GoTo redo
                    End If
                End If


            Next
            For Each row As DataGridViewRow In DataGridView1.Rows

                If row.Cells.Item(2).Value IsNot Nothing Then If row.Cells.Item(2).Value.ToString = "eNET" Then DataGridView1.Rows.Remove(DataGridView1.Rows(row.Index))

            Next
            For Each machine In machines
                If machine IsNot Nothing Then
                    If machine.StartsWith("_MT") Or machine.StartsWith("_ST") Then
                        'Nothing
                    Else
                        DataGridView1.Rows.Add(False, machine, "eNET", " - ", "")
                    End If
                End If
            Next
            loaded = True
        Catch ex As Exception
            MsgBox("Unabled to load the eNET machines : " & ex.Message)
        End Try

    End Sub

    '===================================================================================================
    ' SET LOCAL IP
    '===================================================================================================
    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim ip() As Net.IPAddress = System.Net.Dns.GetHostAddresses("")

        If ip.Count > 0 Then
            For Each ipadd As Net.IPAddress In ip
                If Not ipchoose.ListBox1.Items.Contains(ipadd.ToString & ":1000") Then ipchoose.ListBox1.Items.Add((ipadd.ToString & ":1000"))

            Next
        End If

        ipchoose.ShowDialog()

    End Sub


    '===================================================================================================
    ' Search MTconnect agent
    '===================================================================================================
    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click


        'Search in the LDAP
        '        Dim entry As DirectoryEntry = New DirectoryEntry("LDAP://corp.net/dc=corp,dc=net")
        '        Dim searcher As DirectorySearcher = New DirectorySearcher(entry)

        '        searcher.Filter = "(objectCategory=printQueue)"
        '        searcher.PropertiesToLoad.Add("name")

        'For Each (resEnt As SearchResult in searcher.FindAll())
        '            Response.Write("Printer=" & resEnt.Properties("name")(0))
        '        Next
    End Sub




    '===================================================================================================
    ' Add eNET or MTConnect Source
    '===================================================================================================
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Me.TreeView1.Nodes(0).Nodes.Add(TextBox1.Text, TextBox1.Text)
            Select Case ComboBox5.SelectedItem.ToString.ToUpper
                Case "ENET"
                    ' Edit_eNET.Show()
                    Panel2.Visible = True
                    Panel1.Visible = False
                    DataGridView1.Visible = False

                Case "MTCONNECT"
                    mtcADD.Show()
                    '  Edit_mtconnect.Show()
                Case "LOG FILE"
                    Edit_logfile.Show()

                Case Else
                    MsgBox("CSIFLEX did not recognize this source")
            End Select

            Me.DataGridView1.Visible = True
            Panel1.Visible = False

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Panel1.Visible = False
        DataGridView1.Visible = True
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs)
        Panel2.Visible = False
        DataGridView1.Visible = True
    End Sub

    Private Sub Button19_Click_1(sender As Object, e As EventArgs) Handles Button19.Click
        Panel2.Hide()
        DataGridView1.Show()
    End Sub


#Region "add eNET source"
    '===================================================================================================
    ' OK button to add an eNET connection an save the path
    '===================================================================================================
    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Try
            Form1.chemin_eNET = TextBox5.Text
            Using writer As StreamWriter = New StreamWriter(System.Windows.Forms.Application.StartupPath & "\sys\Setup_.sys")
                writer.Write(TextBox5.Text)
                writer.Close()
            End Using

            Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\Networkenet_.sys", False)
                writer.Write(Me.TextBox7.Text)
                writer.Close()
            End Using

            Me.DataGridView1.Visible = True
            Panel2.Visible = False
        Catch ex As Exception
            MsgBox("Unable to save the eNET path : " & ex.Message)
        End Try
    End Sub


    '===================================================================================================
    ' Check eNET Connection on panel 2
    '===================================================================================================
    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Dim dt As DataTable = clt.Run(TextBox7.Text)
        ' DataGridView1.DataSource = dt
        If dt IsNot Nothing Then
            PictureBox4.Visible = True
            PictureBox3.Visible = False
            ComboBox6.Visible = True
            Dim ii As Integer = 0
            For Each machine As DataRow In dt.Rows
                ComboBox6.Items.Add(machine.Item("machine"))

                Dim row As String() = New String() {"0", machine.Item("machine").ToString, "eNET", "Check", ""}
                DataGridView1.Rows.Add(row)

            Next
            Label21.ForeColor = Color.Green
            Label21.Text = "Connection established," & dt.Rows.Count.ToString & "machines available. "
        Else

            PictureBox3.Visible = True
            ComboBox6.Visible = False
            PictureBox4.Visible = False
            Label21.ForeColor = Color.Red
            Label21.Text = "No connection available."
        End If
    End Sub


    '===================================================================================================
    ' Browse eNET Folder on panel 2
    '===================================================================================================
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Dim folderDlg As New FolderBrowserDialog
        folderDlg.Description = "Specify the eNET folder"

        Try
            folderDlg.ShowNewFolderButton = False
            If (folderDlg.ShowDialog() = DialogResult.OK) Then
                TextBox5.Text = folderDlg.SelectedPath
                Dim root As Environment.SpecialFolder = folderDlg.RootFolder
            End If

            If File.Exists(TextBox5.Text & "\_SETUP\MonList.sys") Then
                PictureBox5.Visible = True
            Else
                MsgBox("CSIFLEX did not found the file MonList.sys in the eNET folder")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    '===================================================================================================
    ' eNET Default on panel 2
    '===================================================================================================
    Private Sub Button17_Click(sender As Object, e As EventArgs)
        TextBox1.Text = "C:\_eNETDNC"
    End Sub
#End Region

#Region "DataGridView2"

    '===================================================================================================
    ' Save datagridview2 button
    '===================================================================================================
    Private Sub Button20_Click(sender As Object, e As EventArgs)

    End Sub



    '===================================================================================================
    ' Reload datagridview2 - users
    '===================================================================================================
    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click



        If MsgBox("Any modification on the user configuration will lost, do you want to continue ? ", _
            MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = vbYes Then
            DataGridView2.DataSource = Nothing
            load_users()
        End If

    End Sub

    ''' <summary>
    ''' AES_Enc
    ''' </summary>
    ''' <returns>Encrypted </returns>

    Public Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
            Return Nothing

        End Try
    End Function

#End Region

    'Private selectedmachinestodrag As String = ""
    'Private Sub DataGridView1_MouseDown(sender As Object, e As MouseEventArgs) Handles DataGridView1.Click

    '    If (e.Button = MouseButtons.Left) And (DataGridView1.SelectedRows.Count <> 0) Then
    '        Dim info As DataGridView.HitTestInfo = DataGridView1.HitTest(e.X, e.Y)
    '        If (info.RowIndex >= 0) Then


    '            DataGridView1.DoDragDrop(DataGridView1.SelectedRows, DragDropEffects.Copy)

    '        End If
    '    End If
    'End Sub



#Region "Right clic/Context menu"



    '===================================================================================================
    ' Right clic on datagridview1 , add 'add to ' in menu
    '===================================================================================================
    Private Sub DataGridView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim hti As DataGridView.HitTestInfo = DataGridView1.HitTest(e.X, e.Y)

            If DataGridView1.SelectedRows.Count > 0 Then
                For Each node As TreeNode In TreeView2.Nodes(0).Nodes
                    Dim item_ As New ToolStripMenuItem(node.Text, Nothing, AddressOf ClickHandler, node.Text)
                    item_.Text = node.Text
                    ' ContextMenuStrip1.Items
                    ' ContextMenuStrip1.Items.Add(item_)
                    If Not add_menu.DropDownItems.ContainsKey(item_.Text) Then add_menu.DropDownItems.Add(item_)
                Next
                ContextMenuStrip1.Show(DataGridView1, New System.Drawing.Point(e.X, e.Y), ToolStripDropDownDirection.Default)
            End If
        End If
    End Sub
    Public Sub ClickHandler(ByVal sender As Object, ByVal e As EventArgs)
        For Each row_ As DataGridViewRow In DataGridView1.SelectedRows

            Dim node_ As New TreeNode, node_0 As New TreeNode
            If Not IsNothing(row_.Cells.Item(1).Value) Then
                node_.Name = row_.Cells.Item(1).Value.ToString
                node_.Text = row_.Cells.Item(1).Value.ToString

                Dim node As TreeNode() = TreeView2.Nodes(0).Nodes.Find(CType(sender, ToolStripMenuItem).Text, True)
                TreeView2.SelectedNode = node(0)
                TreeView2.Nodes(0).Nodes(node(0).Index).Nodes.Add(node_)
            End If
        Next
    End Sub






    '======================================================================
    ' Delete node in tr2/1
    '======================================================================
    Private Sub Delete_treenode_treeview2(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click

        Dim tn As TreeNode = selected_node_2

        If tn IsNot Nothing Then
            selected_node_1 = Nothing
            Dim iRet As Integer
            '    TreeView2.Nodes(0).Nodes(TreeView2.Nodes(0).Nodes.Count - 1).Remove()
            iRet = MsgBox("Are you certain you want to delete " & tn.Text & "?", _
                MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2)
            If iRet = vbYes Then
                TreeView2.Nodes.Remove(selected_node_2)
                'Dim th As New Thread(AddressOf save_groupes)
                'th.Start("Delete|||" & tn.Text)
            End If
        End If

    End Sub
    Private Sub Delete_treenode_treeview1(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click

        Dim tn As TreeNode = selected_node_1
        selected_node_2 = Nothing
        If tn IsNot Nothing Then
            Dim iRet As Integer
            '    TreeView1.Nodes(0).Nodes(TreeView1.Nodes(0).Nodes.Count - 1).Remove()
            iRet = MsgBox("Are you certain you want to delete " & tn.Text & "?", _
                MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2)
            If iRet = vbYes Then
                TreeView1.Nodes.Remove(selected_node_1)
            End If
        End If

    End Sub

    '============================================
    ' TREEVIEW 1/2 RENAME
    '============================================
    Public ancient_name As String
    Private Sub Rename_treenode_treeview2(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        ' TreeView2.Nodes(0).Nodes(TreeView2.Nodes(0).Nodes.Count - 1).Remove()
        'txtbox.ShowDialog()
        If Not (IsNothing(TreeView2.SelectedNode)) Then
            ancient_name = TreeView2.SelectedNode.Text
            TreeView2.SelectedNode.BeginEdit()
        End If


    End Sub
    Private Sub Rename_treenode_treeview1(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        '     TreeView1.Nodes(0).Nodes(TreeView1.Nodes(0).Nodes.Count - 1).Remove()
        '  txtbox.ShowDialog()
        If Not (IsNothing(TreeView1.SelectedNode)) Then TreeView1.SelectedNode.BeginEdit()
    End Sub


    '============================================
    ' TREEVIEW 1/2 select a node with Right clic to rename/delete 
    '============================================
    Public selected_node_1 As TreeNode
    Public selected_node_2 As TreeNode
    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then

            TreeView1.SelectedNode = e.Node
            selected_node_1 = e.Node
            ContextMenuADD.Items.Item(1).Visible = True
            ContextMenuADD.Items.Item(2).Visible = True
        End If
    End Sub
    Private Sub TreeView2_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView2.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            TreeView2.SelectedNode = e.Node
            selected_node_2 = e.Node
            ContextMenuADD2.Items.Item(1).Visible = True
            ContextMenuADD2.Items.Item(2).Visible = True
        End If
    End Sub


    '============================================
    ' Delete/rename not visible if no clic on treeview node
    '============================================
    Private Sub ContextMenuADD2_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuADD2.Closing
        ContextMenuADD2.Items.Item(1).Visible = False
        ContextMenuADD2.Items.Item(2).Visible = False
    End Sub
    Private Sub ContextMenuADD_closing(sender As Object, e As CancelEventArgs) Handles ContextMenuADD.Closing
        ContextMenuADD.Items.Item(1).Visible = False
        ContextMenuADD.Items.Item(2).Visible = False
    End Sub


    '===================================================================================================
    ' ADD SOURCE IN TREEVIEW, name and source choice
    '===================================================================================================
    Private Sub addsource(sender As Object, e As EventArgs) Handles ToolStripadd.Click
        DataGridView1.Visible = False
        Panel1.Visible = True
    End Sub

    '===================================================================================================
    ' ADD a groupe IN TREEVIEW2
    '===================================================================================================
    Private Sub addgroupe(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

        TreeView2.Nodes(0).Nodes.Add("Groupe" & TreeView2.Nodes(0).Nodes.Count.ToString, "Groupe" & TreeView2.Nodes(0).Nodes.Count.ToString)
        Dim th As New Thread(AddressOf save_groupes)
        th.Start("Add|||" & TreeView2.Nodes(0).Nodes.Count.ToString)

    End Sub


#End Region

    '============================================
    ' Save Source/groupe
    '============================================
    Private Sub save_groupes(what_have_been_done As String)
        Try

            Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\machines_Groupes_.sys")
                For Each node_ As TreeNode In TreeView2.Nodes(0).Nodes
                    writer.WriteLine("__" & node_.Text)
                    For Each node__ As TreeNode In node_.Nodes
                        writer.WriteLine(node__.Text)
                    Next
                Next
                writer.Close()
            End Using

            'If Not IsNothing(what_have_been_done) Then

            '    Dim cnt As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & System.Windows.Forms.Application.StartupPath.ToString & "\sys\CSI_auth.odc;")
            '    cnt.Open()
            '    Dim command As New OleDbCommand("", cnt)
            '    Dim command_edit_2 As New OleDbCommand("ALTER TABLE [Users] DROP COLUMN" & ancient_name, cnt)


            '    Select Case what_have_been_done.Split("|||")(0).ToString
            '        Case "Add"
            '            command.CommandText = "ALTER TABLE Users ADD COLUMN Groupe" & what_have_been_done.Split("|||")(3).ToString & " String"
            '            command.ExecuteNonQuery()
            '        Case "Delete"
            '            command.CommandText = "ALTER TABLE [Users] DROP COLUMN " + what_have_been_done.Split("|||")(3).ToString + ";"
            '            command.ExecuteNonQuery()
            '        Case "Edit"
            '            command.CommandText = "UPDATE TABLE [Users] SET " + what_have_been_done.Split("|||")(3).ToString + "=" + ancient_name + ";"
            '            command.ExecuteNonQuery()
            '            command_edit_2.ExecuteNonQuery()
            '        Case Else
            '            MsgBox("CSIFLEX has encountered an issue while updating the user database", MsgBoxStyle.Critical)
            '    End Select
            '    cnt.Close()
            'End If
        Catch ex As Exception
            MsgBox("Unable to save the machines groupes : " & ex.Message)
        End Try
    End Sub


    Private Sub save_sources()
        Try
            Using writer As StreamWriter = New StreamWriter(Forms.Application.StartupPath & "\sys\machines_sources_.sys")
                For Each node_ As TreeNode In TreeView1.Nodes(0).Nodes
                    writer.WriteLine(node_.Text)
                Next
                writer.Close()
            End Using
        Catch ex As Exception
            MsgBox("Unable to save the machines sources : " & ex.Message)
        End Try
    End Sub



    '============================================
    ' Load the Sources and add them to the treeview 
    '============================================
    Public LoadingSources As Boolean = False
    Private Sub Load_sources()

        If File.Exists(Forms.Application.StartupPath & "\sys\machines_sources_.sys") Then
            Try
                LoadingSources = True
                TreeView1.Nodes(0).Nodes.Clear()
                Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\machines_sources_.sys")

                    While Not reader.EndOfStream
                        Dim readed As String = reader.ReadLine


                        TreeView1.Nodes(0).Nodes.Add(readed, readed)
                    End While
                    reader.Close()
                End Using
                LoadingSources = False
            Catch ex As Exception
                MsgBox("Unable to Load the machines groupes : " & ex.Message)
            End Try
        End If
    End Sub

    '============================================
    ' Load groupe with their machines and add them in the treeview
    '============================================
    Private Sub Load_groupes()
        Try
            If File.Exists(Forms.Application.StartupPath & "\sys\machines_groupes_.sys") Then
                TreeView2.Nodes(0).Nodes.Clear()
                Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\machines_groupes_.sys")

                    While Not reader.EndOfStream
                        Dim readed As String = reader.ReadLine
                        If readed.StartsWith("__") Then TreeView2.Nodes(0).Nodes.Add(readed.Replace("__", ""), readed.Replace("__", ""))
                        If Not readed.StartsWith("__") Then TreeView2.Nodes(0).Nodes(TreeView2.Nodes(0).GetNodeCount(False) - 1).Nodes.Add(readed, readed)
                    End While
                    reader.Close()
                End Using
            End If
        Catch ex As Exception
            MsgBox("Unable to Load the machines groupes : " & ex.Message)
        End Try
    End Sub


    '===================================================================================================
    ' Save datagridview2 / users
    '===================================================================================================
    Public DGV2MOD As Boolean = False
    Private Sub save_users()

        Dim usermachines As New Dictionary(Of String, String) ' user, 'machine1, machine2, ...'
        DGV2MOD = True
        For Each row_ As DataGridViewRow In DataGridView2.Rows
            If Not (row_.Cells("password_").Value Is Nothing) And Not IsDBNull((row_.Cells("password_").Value)) Then

                If Not IsDBNull(row_.Cells.Item("password_").Value) Then
                    If Not CSI_Lib.IsAlphaNumeric(row_.Cells.Item("password_").Value) Then
                        Dim index_ As Integer = row_.Index
                        MsgBox("the password must be alphanumerical", vbCritical)
                        DataGridView2.CurrentCell = DataGridView2.Rows(index_).Cells.Item("password_")
                    Else
                        row_.Cells("password_").Value = AES_Encrypt(row_.Cells("password_").Value.ToString, "pass").ToString
                    End If
                End If
                '   If Not IsDBNull(row_.Cells("Machines").Value) Then usermachines.Add(row_.Cells("username_").Value, row_.Cells("Machines").Value)
            End If
        Next



        Dim cnt As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & System.Windows.Forms.Application.StartupPath.ToString & "\sys\CSI_auth.odc;")
        cnt.Open()
        Dim Dadapter As OleDbDataAdapter = New OleDbDataAdapter("SELECT * FROM  [Users]", cnt)
        Dim oledbCmdBuilder As New OleDbCommandBuilder(Dadapter)
        'Dim table_ As New DataTable("Users") '= DataGridView2.DataSource

        'For Each Col As DataGridViewColumn In DataGridView2.Columns
        '    table_.Columns.Add(Col.Name)
        'Next
        'For Each row_ As DataGridViewRow In DataGridView2.Rows
        '    If Not IsNothing(row_.Cells(1).Value) Then table_.Rows.Add(row_)
        'Next

        't _ = DataGridViewToDataTable(DataGridView2, "Users")

        Dim droop As OleDbCommand = New OleDbCommand("DROP TABLE [Users] ", cnt)
        droop.ExecuteNonQuery()

        Dim cmdCREATTION As New OleDbCommand("CREATE TABLE Users (username_ string,Name_ string, password_ string, email_ string, machines  string, usertype string)", cnt)
        cmdCREATTION.ExecuteNonQuery()

        For Each row_ As DataGridViewRow In DataGridView2.Rows
            If Not IsNothing(row_.Cells("username_").Value) Then
                Dim cmd4 As New OleDbCommand("INSERT INTO [Users] VALUES ('" & row_.Cells("username_").Value.ToString & "', '" & row_.Cells("Name_").Value.ToString & "','" & row_.Cells("password_").Value.ToString & "','" & row_.Cells("email_").Value.ToString & "', '" & row_.Cells("machines").Value.ToString & "', '" & row_.Cells("usertype").Value.ToString & "')", cnt)
                cmd4.ExecuteNonQuery()
            End If
        Next

        'Try
        '    Dadapter.Update(table_)
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString)
        'End Try

        'If usermachines.Count <> 0 Then
        '    For Each key_ In usermachines.Keys
        '        Dim updatecommand As New OleDbCommand("Update [Users] set Machines='" & usermachines.Item((key_.ToString)) & "' where username_ = '" & key_.ToString & "'", cnt)
        '        updatecommand.ExecuteNonQuery()
        '    Next
        'End If

        cnt.Close()
        DataGridView2.DataSource = Nothing
        DGV2MOD = False
    End Sub

    Public Shared Function IfNullObj(ByVal o As Object, Optional ByVal DefaultValue As String = "") As String
        Dim ret As String = ""
        Try
            If o Is DBNull.Value Then
                ret = DefaultValue
            Else
                ret = o.ToString
            End If
            Return ret
        Catch ex As Exception
            Return ret
        End Try
    End Function
    Public Shared Function DataGridViewToDataTable(ByVal dtg As DataGridView,
        Optional ByVal DataTableName As String = "myDataTable") As DataTable
        Try
            Dim dt As New DataTable(DataTableName)
            Dim row As DataRow
            Dim TotalDatagridviewColumns As Integer = dtg.ColumnCount - 1
            'Add Datacolumn
            For Each c As DataGridViewColumn In dtg.Columns
                Dim idColumn As DataColumn = New DataColumn()
                idColumn.ColumnName = c.Name
                dt.Columns.Add(idColumn)
            Next
            'Now Iterate thru Datagrid and create the data row
            For Each dr As DataGridViewRow In dtg.Rows
                'Iterate thru datagrid
                row = dt.NewRow 'Create new row
                'Iterate thru Column 1 up to the total number of columns
                For cn As Integer = 0 To TotalDatagridviewColumns
                    row.Item(cn) = IfNullObj(dr.Cells(cn).Value) ' This Will handle error datagridviewcell on NULL Values
                Next
                'Now add the row to Datarow Collection
                dt.Rows.Add(row)
            Next
            'Now return the data table
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    '===================================================================================================
    ' Load users to dgv2
    '===================================================================================================
    Public usersVSmachines As New Dictionary(Of String, List(Of String)), loading As Boolean = False
    Private Sub load_users()
        Dim CSILIB As New CSI_Library.CSI_Library
        loading = True
        '*************************************************************************************************************************************************'
        '**** DB Connection
        '*************************************************************************************************************************************************'
        Dim cnt As OleDb.OleDbConnection
        Try

            cnt = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & System.Windows.Forms.Application.StartupPath.ToString & "\sys\CSI_auth.odc;")
            cnt.Open()

            If cnt.State = 1 Then
            Else
                MsgBox("Connection to the database failed")
                GoTo end_
            End If
        Catch ex As Exception
            MsgBox(" Unable to establish a connection to the database : " & ex.Message, vbCritical + vbSystemModal)
            GoTo end_
        End Try

        '*************************************************************************************************************************************************'
        '**** DB Connection -END
        '*************************************************************************************************************************************************'

        Dim adapter As New OleDbDataAdapter, reader As OleDbDataReader, table_ As New DataTable

        Dim command As New OleDbCommand("SELECT * FROM [Users]", cnt)
        reader = command.ExecuteReader
        table_.Load(reader)
        DataGridView2.DataSource = table_

        For Each col As DataGridViewColumn In DataGridView2.Columns
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next
        usersVSmachines.Clear()
        For Each row_ As DataGridViewRow In DataGridView2.Rows

            If Not (row_.Cells("username_").Value Is Nothing) And _
                Not IsDBNull(row_.Cells("username_").Value) And _
                Not IsDBNull(row_.Cells("password_").Value) And _
                Not (row_.Cells("password_").Value Is Nothing) Then


                Dim PASSWORD As String = CSILIB.AES_Decrypt(row_.Cells("password_").Value, "pass")
                If PASSWORD Is Nothing Then MsgBox("PASSWORD is nothing ")
                row_.Cells("password_").Value = PASSWORD
                Dim listofmachines As New List(Of String)
                For Each machine In row_.Cells("Machines").Value.ToString.Split(", ")
                    listofmachines.Add(machine)
                Next
                If Not usersVSmachines.ContainsKey(row_.Cells("username_").Value) Then usersVSmachines.Add(row_.Cells("username_").Value, listofmachines)

            End If
        Next

        cnt.Close()

End_:
        loading = False
    End Sub



    Private Sub check_Datagridview2()
        If loading = False Then
            usersVSmachines.Clear()
            For Each row_ As DataGridViewRow In DataGridView2.Rows

                If row_.Cells.Item("username_").Value Is Nothing _
                    Or IsDBNull(row_.Cells.Item("username_").Value) Then
                    'DataGridView2.CurrentCell = row_.Cells.Item("username_")
                    GoTo end__


                End If

                Dim listofmachines As New List(Of String)
                Dim S As String() = Split(row_.Cells("Machines").Value.ToString, ", ")
                For Each machine In Split(row_.Cells("Machines").Value.ToString, ", ")
                    listofmachines.Add(machine)
                Next
                If Not usersVSmachines.ContainsKey(row_.Cells("username_").Value) Then
                    usersVSmachines.Add(row_.Cells("username_").Value, listofmachines)
                End If

end__:
            Next
        End If
    End Sub

    Function Checkpasswords_DGV2() As Boolean
        For Each row_ As DataGridViewRow In DataGridView2.Rows

            If Not (row_.Cells.Item("username_").Value Is Nothing _
                  Or IsDBNull(row_.Cells.Item("username_").Value)) Then

                If row_.Cells.Item("password_").Value Is Nothing _
                       Or IsDBNull(row_.Cells.Item("password_").Value) Then

                    MsgBox("Please select a password for the user " & row_.Cells.Item("username_").Value)
                    DataGridView2.CurrentCell = row_.Cells.Item("password_")
                    Return False

                End If
            End If

        Next
        Return True
    End Function
    '-----------------------------------------------------------------------------------------------------------------------
    ' Select a row in the user datagridview
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub DataGridView2_CellContentClick(sender As Object, e As EventArgs) Handles DataGridView2.SelectionChanged, DataGridView2.CellContentClick

        '' Fill the treeview if select a row or a cell


        If DataGridView2.SelectedRows.Count <> 0 Then
            If Not IsDBNull(DataGridView2.Rows(DataGridView2.SelectedRows.Item(0).Index).Cells("machines").Value) Then fillTreeRecurcive()
        Else
            If DataGridView2.SelectedCells.Count <> 0 Then
                If DataGridView2.Columns(DataGridView2.CurrentCell.ColumnIndex).HeaderText.ToLower = "machines" Then
                    If Not IsDBNull(DataGridView2.CurrentCell.Value) Then fillTreeRecurcive()
                End If

                If DataGridView2.SelectedCells.Count = 1 Then
                    If DataGridView2.Columns(DataGridView2.CurrentCell.ColumnIndex).HeaderText.ToLower = "usertype" Then
                        selectedtype = usertype()
                        DataGridView2.SelectedCells.Item(0).Value = selectedtype
                    End If
                End If

            End If
        End If


    End Sub

    Public selectedtype As String = ""
    Function usertype() As String
        SelectUserType.ShowDialog()
        Return selectedtype

    End Function





    Private Sub TreeView2_AfterSelect(sender As Object, e As System.Windows.Forms.NodeLabelEditEventArgs) Handles TreeView2.AfterLabelEdit
        'Dim th As New Thread(AddressOf save_groupes)
        'th.Start("Edited|||" & e.Label.ToString)
    End Sub




    '-----------------------------------------------------------------------------------------------------------------------
    ' Read the treeview and fills the checked machines in a list of string (declared as public)
    '-----------------------------------------------------------------------------------------------------------------------
#Region "Read the treeview 3 "

    Dim checked_in_treeview3 As New List(Of String)
    Function readTreeRecurcive() As List(Of String)
        checked_in_treeview3.Clear()
        Dim aNode As TreeNode

        For Each aNode In TreeView3.Nodes
            PrintRecursive(aNode)
        Next
        Return checked_in_treeview3
    End Function
    Private Sub PrintRecursive(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            checked_in_treeview3.Add(n.Text)
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursive(aNode)
        Next
    End Sub
#End Region
#Region "Fill the treeview 3 "


    Sub fillTreeRecurcive()

        unchecktree()
        fiiliing = True
        Dim aNode As TreeNode
        For Each aNode In TreeView3.Nodes
            fillRecursive(aNode)
        Next
        fiiliing = False
    End Sub


    Private Sub fillRecursive(ByVal n As TreeNode)
        If DataGridView2.SelectedCells.Count <> 0 Then

            'If usersVSmachines.Item(DataGridView2.Rows(DataGridView2.CurrentCell.RowIndex).Cells("username_").Value.ToString).Contains(n.Text) Then
            '    n.Checked = True
            'Else
            '    If DataGridView2.SelectedRows.Count <> 0 Then
            '        If Not IsDBNull(DataGridView2.Rows(DataGridView2.SelectedRows.Item(0).Index).Cells("machines").Value) Then fillTreeRecurcive()
            '    End If

            'End If


            If DataGridView2.SelectedRows.Count <> 0 Then


                If usersVSmachines.Item(DataGridView2.Rows(DataGridView2.SelectedRows.Item(0).Index).Cells("username_").Value.ToString).Contains(n.Text) Then
                    n.Checked = True
                End If



            Else
                If usersVSmachines.Item(DataGridView2.Rows(DataGridView2.CurrentCell.RowIndex).Cells("username_").Value.ToString).Contains(n.Text) Then
                    n.Checked = True
                End If
            End If

            Dim aNode As TreeNode
            For Each aNode In n.Nodes
                fillRecursive(aNode)
            Next
        End If
    End Sub
#End Region


    Sub unchecktree()
        fiiliing = True

        Dim aNode As TreeNode
        For Each aNode In TreeView3.Nodes
            unchecktreeRecursive(aNode)
        Next
        fiiliing = False
    End Sub
    Private Sub unchecktreeRecursive(ByVal n As TreeNode)
        If n.Checked = True Then n.Checked = False
        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            unchecktreeRecursive(aNode)
        Next
    End Sub



#Region "Checkbox treeview 3"
    Private Sub TreeView3_AfterCheck(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeView3.AfterCheck
        If fiiliing = False Then
            RemoveHandler TreeView3.AfterCheck, AddressOf TreeView3_AfterCheck

            Call CheckAllChildNodes(e.Node)

            If e.Node.Checked Then
                If e.Node.Parent Is Nothing = False Then
                    Dim allChecked As Boolean = True
                    Call IsEveryChildChecked(e.Node.Parent, allChecked)
                    If allChecked Then
                        e.Node.Parent.Checked = True
                        Call ShouldParentsBeChecked(e.Node.Parent)
                    End If
                End If
            Else
                Dim parentNode As TreeNode = e.Node.Parent
                While parentNode Is Nothing = False
                    parentNode.Checked = False
                    parentNode = parentNode.Parent
                End While
            End If



            ' list of machines for each user
            If DataGridView2.SelectedCells.Count > 0 Then

                Dim value_ As String = "", machines_readed As List(Of String) = readTreeRecurcive()

                If Not IsNothing(machines_readed) Then
                    For Each machine In machines_readed
                        If value_ = "" Then
                            value_ = machine
                        Else
                            value_ = value_ & ", " & machine
                        End If
                    Next

                    DataGridView2.Rows(DataGridView2.SelectedCells.Item(0).RowIndex).Cells("machines").Value = value_
                End If
            End If
            AddHandler TreeView3.AfterCheck, AddressOf TreeView3_AfterCheck
        End If
    End Sub

    Private Sub CheckAllChildNodes(ByVal parentNode As TreeNode)
        For Each childNode As TreeNode In parentNode.Nodes
            childNode.Checked = parentNode.Checked
            CheckAllChildNodes(childNode)
        Next
    End Sub

    Private Sub IsEveryChildChecked(ByVal parentNode As TreeNode, ByRef checkValue As Boolean)
        For Each node As TreeNode In parentNode.Nodes
            Call IsEveryChildChecked(node, checkValue)
            If Not node.Checked Then
                checkValue = False
            End If
        Next
    End Sub

    Private Sub ShouldParentsBeChecked(ByVal startNode As TreeNode)
        If startNode.Parent Is Nothing = False Then
            Dim allChecked As Boolean = True
            Call IsEveryChildChecked(startNode.Parent, allChecked)
            If allChecked Then
                startNode.Parent.Checked = True
                Call ShouldParentsBeChecked(startNode.Parent)
            End If
        End If
    End Sub
#End Region


    Private Sub Button20_Click_1(sender As Object, e As EventArgs) Handles Button20.Click
        check_srv()

    End Sub


    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim dt As DataTable = clt.Run(TextBox6.Text)
        If dt IsNot Nothing Then
            'PictureBox4.Visible = True
            'PictureBox3.Visible = False
            ComboBox4.Visible = True

            For Each machine In dt.Rows
                ComboBox4.Items.Add(machine.item("machine"))
            Next
            Label17.ForeColor = Color.Green
            Label17.Text = "Connection established," & vbCr & dt.Rows.Count.ToString & "machines available. "
        Else

            'PictureBox3.Visible = True
            ComboBox4.Visible = False
            'PictureBox4.Visible = False
            Label17.ForeColor = Color.Red
            Label17.Text = "No connection available."
        End If
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        Select Case MsgBox("Do you REALLY like to uninstall the CSIFLEX Server Service ?", MsgBoxStyle.YesNo, " ? ")
            Case MsgBoxResult.Yes
                SelfInstaller.UninstallMe()
                MessageBox.Show("Successfully uninstalled the Service. ", "Status",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
            Case Else
        End Select
        Button13.Text = "Start CSIFLEX Server"
        Button13.BackColor = Color.Transparent
    End Sub






    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Dim nt As New ServiceNT
        nt.start_service()
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        'If TreeView1.SelectedNode.Text = "eNET" Then Load_eNET_datagridview()
        'If TreeView1.SelectedNode.Text = "MTConnect" Then Load_mtConnect_datagridview()
        'If TreeView1.SelectedNode.Text = "Log files" Then Load_log_datagridview()
    End Sub
    'Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
    '    If Not LoadingSources Then
    '        loadingmachines = True
    '        Dim machinesTBL As New DataTable
    '        machinesTBL.Rows.Clear()
    '        machinesTBL.Columns.Clear()

    '        machinesTBL.Columns.Add(DataGridView1.Columns.Item(0).Name)
    '        machinesTBL.Columns.Add(DataGridView1.Columns.Item(1).Name)
    '        machinesTBL.Columns.Add(DataGridView1.Columns.Item(2).Name)
    '        machinesTBL.Columns.Add(DataGridView1.Columns.Item(3).Name)
    '        machinesTBL.Columns.Add(DataGridView1.Columns.Item(4).Name)


    '        For Each row_ As DataGridViewRow In DataGridView1.Rows
    '            Dim r As DataRow = machinesTBL.NewRow()

    '            r(0) = row_.Cells.Item(0).Value
    '            r(1) = row_.Cells.Item(1).Value
    '            r(2) = row_.Cells.Item(2).Value
    '            r(3) = row_.Cells.Item(3).Value
    '            r(4) = row_.Cells.Item(4).Value
    '            machinesTBL.Rows.Add(r)

    '        Next
    '        machinesTBL.Select("IP like '*eNET*'", Nothing)


    '        If IsNothing(DataGridView1.DataSource) Then
    '            DataGridView1.Rows.Clear()
    '        Else
    '            DataGridView1.DataSource = Nothing
    '        End If

    '        'Dim cc(5) As DataGridViewColumn
    '        'For Each c As DataGridViewColumn In DataGridView1.Columns
    '        '    cc(c.Index) = c.Clone
    '        'Next
    '        DataGridView1.Columns.Clear()

    '        'For i = 0 To UBound(cc) - 1
    '        '    Dim d As New DataGridViewColumn
    '        '    Dim dcm As New DataGridViewButtonColumn
    '        '    If cc(i).Name = "Check" Then
    '        '        dcm = cc(i).Clone
    '        '        DataGridView1.Columns.Add(dcm)
    '        '    Else
    '        '        d = cc(i).Clone
    '        '        DataGridView1.Columns.Add(d)
    '        '    End If
    '        'Next
    '        '   DataGridView1.AutoGenerateColumns = False
    '        DataGridView1.DataSource = machinesTBL.DefaultView
    '        DataGridView1.Columns.Item("IP").HeaderText = "Source"
    '        Dim dcm As New DataGridViewButtonColumn
    '        dcm.HeaderText = "Check"
    '        DataGridView1.Columns.RemoveAt(3)
    '        DataGridView1.Columns.Insert(3, dcm)
    '        DataGridView1.Columns.Item(0).Visible = False
    '        loadingmachines = False
    '    End If
    'End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        If DataGridView1.Rows(DataGridView1.SelectedCells.Item(0).RowIndex).Cells("IP").Value.ToString.StartsWith("http") Then Edit_mtconnect.Show()
        If DataGridView1.Rows(DataGridView1.SelectedCells.Item(0).RowIndex).Cells("IP").Value = "eNET" Then Edit_eNET.Show()
    End Sub

    Private Sub Button10_Click_1(sender As Object, e As EventArgs)
        Dim srv As New CSI_Library.ServiceNT
        srv.start_service()
    End Sub


    Private Sub Button10_Click_2(sender As Object, e As EventArgs) Handles Button10.Click
        Dim srv As New CSI_Library.ServiceNT
        srv.start_service()
    End Sub
End Class
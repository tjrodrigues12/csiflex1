Imports System.Data.OleDb
Imports System.IO
Imports System.ComponentModel
Imports System.Security.Principal
Imports CSI_Library


Public Class Welcome

    Public CSIF_version As Integer = 0
    '0 Nothing 
    '1 Lite
    '2 Standalone
    '3 Complete with server.
    Public rootpath As String = CSI_Library.CSI_Library.ClientrootPath
    Public ip As String

   

    Public Shared from_ As String

    Private Sub BTN_Ok_Click(sender As Object, e As EventArgs) Handles BTN_Ok.Click

        Dim path_ As String = CSI_Library.CSI_Library.ClientRootPath

        Try

            Dim identity = WindowsIdentity.GetCurrent()
            Dim principal = New WindowsPrincipal(identity)
            'Dim isElevated As Boolean = principal.IsInRole(WindowsBuiltInRole.Administrator)

            'If Not isElevated Then
            '    MsgBox("The application needed to be restart in admin to change license")
            '    Environment.Exit(1)
            'End If

            If File.Exists(path_ & "\wincsi.dl1") Then
                File.Delete(path_ & "\wincsi.dl1")
            End If
        Catch ex As Exception
            MsgBox("The application needed to be restart in admin to change license")
            Environment.Exit(1)
        End Try

        Dim everythingIsOkToStart As Boolean = True
        If Not (Directory.Exists(TB_ServerIp.Text)) And CSIF_version <> 3 Then
            MessageBox.Show("The Enet folder was not found. Please verify the path you provided.")
            Exit Sub
        End If

        'Create sys folder

        If Not (Directory.Exists(rootpath & "\sys\")) Then Directory.CreateDirectory(rootpath & "\sys\")

        'Check for license
        'CheckLicense()

        Select Case CSIF_version
            Case 1
                If TB_ServerIp.Text = "" Then
                    MessageBox.Show("You must specify the eNET folder path ! .", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    everythingIsOkToStart = False
                Else
                    If TB_Server_DB_Location.Text = "" Then
                        MessageBox.Show("You must specify the database path ! .", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        everythingIsOkToStart = False
                    Else
                        Try
                            Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\Setup_.csys", False)
                                writer.Write(TB_ServerIp.Text)
                                writer.Close()
                            End Using
                        Catch ex As Exception
                            MessageBox.Show("Unable to save the eNET path ") ' & ex.Message)
                            everythingIsOkToStart = False
                        End Try


                        Try
                            File.Copy(TB_ServerIp.Text & "\_SETUP\MonList.sys", rootpath & "\sys\Monlist.csys", True)
                        Catch ex As Exception
                            MessageBox.Show("Unable to save the eNET machine list") ' & ex.Message)
                            everythingIsOkToStart = False
                        End Try

                        Try
                            Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\Setupdb_.csys", False)
                                writer.Write(TB_Server_DB_Location.Text)
                                writer.Close()
                            End Using
                        Catch ex As Exception
                            MessageBox.Show("Unable to save the database path") ' & ex.Message)
                            everythingIsOkToStart = False
                        End Try
                    End If


                    Try
                        Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\CSIFv_.csys", False)
                            Dim lib__ As New CSI_Library.CSI_Library

                            writer.Write(Login.AES_Encrypt("1:-----", "4Solutions"))
                            writer.Close()
                        End Using
                    Catch ex As Exception
                        MessageBox.Show("Unable to save the database path ") ' & ex.Message)
                        everythingIsOkToStart = False
                    End Try

                    If everythingIsOkToStart Then
                        UserLogin.Visible = False
                        Reporting_application.Show()
                        Me.Close()
                    End If


                End If



            Case 2

                If TB_ServerIp.Text = "" Then
                    MessageBox.Show("You must specify the eNET folder path ! .")
                    everythingIsOkToStart = False
                Else
                    If TB_Server_DB_Location.Text = "" Then
                        MessageBox.Show("You must specify the database path ! .")
                        everythingIsOkToStart = False
                    Else
                        Try
                            Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\Setup_.csys", False)
                                writer.Write(TB_ServerIp.Text)
                                writer.Close()
                            End Using

                        Catch ex As Exception
                            MessageBox.Show("Unable to save the eNET path ") ' & ex.Message)
                            everythingIsOkToStart = False
                        End Try


                        Try
                            File.Copy(TB_ServerIp.Text & "\_SETUP\MonList.sys", rootpath & "\sys\Monlist.csys", True)
                        Catch ex As Exception
                            MessageBox.Show("Unable to save the eNET machine list. Verify if this file exist in your eNET path.")
                            everythingIsOkToStart = False
                        End Try

                        Try
                            Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\Setupdb_.csys", False)
                                writer.Write(TB_Server_DB_Location.Text)
                                writer.Close()
                            End Using
                        Catch ex As Exception
                            MessageBox.Show("Unable to save the database path") ' & ex.Message)
                            everythingIsOkToStart = False
                        End Try
                    End If


                    Try
                        Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\CSIFv_.csys", False)
                            Dim lib__ As New CSI_Library.CSI_Library

                            writer.Write(Login.AES_Encrypt("2:-----", "4Solutions"))
                            writer.Close()
                        End Using
                    Catch ex As Exception
                        MessageBox.Show("Unable to save the database path") ' & ex.Message)
                        everythingIsOkToStart = False
                    End Try

                    If everythingIsOkToStart Then
                        UserLogin.Visible = False
                        Reporting_application.Show()
                    End If

                    Me.Close()

                End If
                '""

            Case 3
                If "/" = "" Then
                    MessageBox.Show("You must specify the CSIFLEX Server address ! .")
                    everythingIsOkToStart = False
                Else
                    If TB_Server_DB_Location.Text = "" Then
                        MessageBox.Show("You must specify the CSIFLEX Server database path ! .")
                        everythingIsOkToStart = False
                    Else
                        Try
                            Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\SrvDBpath.csys", False)
                                writer.Write(TB_Server_DB_Location.Text)
                                writer.Close()
                            End Using
                        Catch ex As Exception
                            MessageBox.Show("Unable to save the server database path ") ' & ex.Message)
                        End Try

                        If Not (UserLogin.GetFieldValue("SELECT Username_, Password_ FROM Users WHERE Username_='admin'", "Password_") = Nothing) Then


                            Try
                                Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\Setupdb_.csys", False)
                                    writer.Write(TB_Server_DB_Location.Text)
                                    writer.Close()
                                End Using
                            Catch ex As Exception
                                MessageBox.Show("Unable to save the database path ") ' & ex.Message)
                            End Try

                            Try

                                Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\CSIFv_.csys", False)
                                    Dim lib__ As New CSI_Library.CSI_Library

                                    writer.Write(Login.AES_Encrypt("3:-----", "4Solutions"))
                                    writer.Close()
                                End Using

                            Catch ex As Exception
                                MessageBox.Show("Unable to save the database path ") ' & ex.Message)
                            End Try

                            Try
                                Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\Setup_.csys", False)
                                    writer.Write(TB_ServerIp.Text)
                                    writer.Close()
                                End Using

                            Catch ex As Exception
                                MessageBox.Show("Unable to save the eNET path ") ' & ex.Message)
                            End Try




                            'Call Button2_Click(sender, e)
                            If Not Label6.Text = "Not Connected to the server" Then


                                If File.Exists((rootpath & "\sys\CSIServerIP_.csys")) Then

                                    If CB_LocalDatabase.Checked Then
                                        Try
                                            Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\Setupdb_.csys", False)
                                                writer.Write(TB_Server_DB_Location.Text & "\")
                                                writer.Close()
                                            End Using
                                        Catch ex As Exception
                                            MessageBox.Show("Unable to save the database path ") ' & ex.Message)
                                        End Try

                                        'UserLogin.synchro = True
                                        from_ = TB_Server_DB_Location.Text
                                        ' to_ = TextBox4.Text
                                        'SynchroDB.ShowDialog()
                                    Else
                                        Try
                                            Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\Setupdb_.csys", False)
                                                writer.Write(TB_Server_DB_Location.Text)
                                                writer.Close()
                                            End Using
                                        Catch ex As Exception
                                            MessageBox.Show("Unable to save the database path ") ' & ex.Message)
                                        End Try
                                    End If


                                    'UserLogin.Visible = True
                                    Me.Close()
                                End If
                            End If
                        Else
                            everythingIsOkToStart = False
                        End If
                    End If
                End If

        End Select

        If everythingIsOkToStart = True Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
        'End If

        record_years_to_import()

    End Sub

    Private Sub record_years_to_import()
        Try
            If File.Exists(rootpath & "\sys\years_.csys") Then File.Delete(rootpath & "\sys\years_.csys")

            Using writer As StreamWriter = New StreamWriter(rootpath & "\sys\years_.csys", False)
                Dim text As String = ""
                For Each element In years__
                    text = text + "," + element.ToString()
                Next
                writer.Write(text)
                writer.Close()
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        UserLogin.tobeclosed = True

        ' UserLogin.Close()
        Me.Close()

    End Sub



    '-----------------------------------------------------------------------------------------------------------------------
    ' MOVE FORME 
    '  
    '-----------------------------------------------------------------------------------------------------------------------
#Region "form move"
    Dim _MouseDown As Boolean, _MouseX As Integer, _MouseY As Integer
    Private Sub Config_report_Form_mouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        _MouseDown = True
        _MouseX = e.X
        _MouseY = e.Y
    End Sub

    Private Sub Config_report_Form_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        _MouseDown = False
    End Sub


    Private Sub Config_report_Form_Mousemove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove


        If _MouseDown = True Then
            Reporting_application.SuspendLayout()
            Me.Left = Me.Left + (e.X - _MouseX)
            Me.Top = Me.Top + (e.Y - _MouseY)
            If Me.Top < 20 Then Me.Top = 0
            If Me.Left < 20 Then Me.Left = 0
            Reporting_application.ResumeLayout(True)

        End If

    End Sub
#End Region

    Private Sub Welcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = 500

        If File.Exists(rootpath & "\sys\CSIFv_.csys") Then
            'Else
            Try
                Using r As StreamReader = New StreamReader(rootpath & "\sys\CSIFV_.csys", False)
                    Dim lib___ As New CSI_Library.CSI_Library
                    Dim tmp As String() = Split(lib___.AES_Decrypt(r.ReadLine, "4Solutions"), ":")
                    CSIF_version = tmp(0)

                    r.Close()
                End Using
                Reporting_application.Show()
            Catch ex As Exception
                MessageBox.Show("Unable to read the CSIFlex version ") ' & ex.Message)
            End Try


        Else



            check_CSIF_Version()


        End If
    End Sub

    Private Sub check_CSIF_Version()
        CheckRadioButton()


        Select Case CSIF_version
            Case 1
                GroupBox1.Text = "CSIFlex Lite"
                TB_Server_DB_Location.Text = rootpath
                CB_LocalDatabase.Checked = False
                CB_LocalDatabase.Visible = False
                CB_LocalDatabase.Enabled = False
                Label3.Text = "eNETDNC folder path"
                Label2.Text = "Server DB Location"
                TB_ServerIp.Visible = True
                Label4.Visible = False
                BTN_BrowseEnetPath.Visible = True
                BTN_BrowseEnetPath.Enabled = True
                BTN_DefaultFolder.Visible = True
                BTN_DefaultFolder.Enabled = True

                'TextBox1.Location = TextBox7.Location

                TB_ServerIp.Visible = True
                TB_ServerIp.Enabled = True

                BTN_BrowseEnetPath.Visible = True
                BTN_BrowseEnetPath.Enabled = True
                BTN_PathDB.Visible = True
                BTN_PathDB.Enabled = True
                BTN_DefaultFolder.Visible = True
                BTN_DefaultFolder.Enabled = True
                TB_ServerIp.Visible = True
                TB_ServerIp.Enabled = True
                Label3.Visible = True
                Label3.Enabled = True
            Case 2
                GroupBox1.Text = "CSIFlex Standalone"
                TB_Server_DB_Location.Text = rootpath
                CB_LocalDatabase.Checked = False
                CB_LocalDatabase.Visible = False
                CB_LocalDatabase.Enabled = False
                Label3.Text = "eNETDNC folder path"
                Label2.Text = "Server DB Location"
                TB_ServerIp.Visible = True
                Label4.Visible = False
                BTN_BrowseEnetPath.Visible = True
                BTN_BrowseEnetPath.Enabled = True
                BTN_DefaultFolder.Visible = True
                BTN_DefaultFolder.Enabled = True

                'TextBox1.Location = TextBox7.Location

                TB_ServerIp.Visible = True
                TB_ServerIp.Enabled = True

                BTN_BrowseEnetPath.Visible = True
                BTN_BrowseEnetPath.Enabled = True
                BTN_PathDB.Visible = True
                BTN_PathDB.Enabled = True
                BTN_DefaultFolder.Visible = True
                BTN_DefaultFolder.Enabled = True

                TB_ServerIp.Visible = True
                TB_ServerIp.Enabled = True
                Label3.Visible = True
                Label3.Enabled = True
            Case 3
                GroupBox1.Text = "CSIFlex Client"
                TB_Server_DB_Location.Text = ""
                'Always use local db
                CB_LocalDatabase.Checked = True
                CB_LocalDatabase.Visible = False
                CB_LocalDatabase.Enabled = False
                Label2.Text = "Server DB Ip : "
                'TextBox7.Visible = False
                Label4.Visible = False
                Label3.Visible = False

                BTN_PathDB.Visible = False
                BTN_PathDB.Enabled = False

                TB_ServerIp.Visible = False
                TB_ServerIp.Enabled = False
                BTN_BrowseEnetPath.Visible = False
                BTN_BrowseEnetPath.Enabled = False
                BTN_DefaultFolder.Visible = False
                BTN_DefaultFolder.Enabled = False


                'Button2.Location = BTN_DefaultFolder.Location
                'Button2.Visible = False
                'Button2.Enabled = True

        End Select
    End Sub
    Private Sub CheckRadioButton()
        If RD_Version_Lite.Checked = True Then CSIF_version = 1
        If RB_Version_Standard.Checked = True Then CSIF_version = 2
        If RB_Version_ServerBased.Checked = True Then CSIF_version = 3

    End Sub

    Private Sub Welcome_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing



    End Sub

    Private Sub TextBox4_clic(sender As Object, e As EventArgs)
        Dim folderDlg As New FolderBrowserDialog

        folderDlg.Description = "Choose or Specify a folder for the database"
        Try
            folderDlg.ShowNewFolderButton = True
            If (folderDlg.ShowDialog() = DialogResult.OK) Then
                'TextBox4.Text = folderDlg.SelectedPath

                Dim root As Environment.SpecialFolder = folderDlg.RootFolder
            End If
            ' Dim s As String = System.IO.Path.GetFileName(TextBox4.Text.ToString())
            '   If File.Exists(TextBox4.Text.ToString() & "\SQL_eNET.odc") Then My.Computer.FileSystem.RenameFile(TextBox4.Text & "\SQL_eNET.odc", "CSI_Database.mdb")

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub



    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles BTN_PathDB.Click
        Dim folderDlg As New FolderBrowserDialog

        folderDlg.Description = "Choose or Specify a folder for the database"
        Try
            folderDlg.ShowNewFolderButton = True
            If (folderDlg.ShowDialog() = DialogResult.OK) Then
                TB_Server_DB_Location.Text = folderDlg.SelectedPath

                Dim root As Environment.SpecialFolder = folderDlg.RootFolder
            End If
            Dim s As String = System.IO.Path.GetFileName(TB_Server_DB_Location.Text.ToString())
            If File.Exists(TB_Server_DB_Location.Text.ToString() & "\SQL_eNET.odc") Then My.Computer.FileSystem.RenameFile(TB_Server_DB_Location.Text & "\SQL_eNET.odc", "CSI_Database.mdb")

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub



    Private Sub textbox7_TextChanged(sender As Object, e As EventArgs) Handles TB_ServerIp.DoubleClick

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RD_Version_Lite.CheckedChanged
        check_CSIF_Version()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RB_Version_Standard.CheckedChanged
        check_CSIF_Version()
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RB_Version_ServerBased.CheckedChanged
        check_CSIF_Version()
    End Sub

    Public years__ As New List(Of String)

    Private Sub BTN_BrowseEnetPath_Click(sender As Object, e As EventArgs) Handles BTN_BrowseEnetPath.Click
        Dim folderDlg As New FolderBrowserDialog
        years__.Clear()
        folderDlg.Description = "Choose or Specify the eNETDNC folder"
        Try
            folderDlg.ShowNewFolderButton = True
            If (folderDlg.ShowDialog() = DialogResult.OK) Then
                TB_ServerIp.Text = folderDlg.SelectedPath

                Dim root As Environment.SpecialFolder = folderDlg.RootFolder
            End If
            Dim s As String = System.IO.Path.GetFileName(TB_ServerIp.Text.ToString())
            If File.Exists(TB_ServerIp.Text.ToString() & "\SQL_eNET.odc") Then My.Computer.FileSystem.RenameFile(TB_ServerIp.Text & "\SQL_eNET.odc", "CSI_Database.mdb")

            Dim files As String() = System.IO.Directory.GetFiles(TB_ServerIp.Text.ToString() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
            Dim machcsvfile As String = ""
            'Dim MostRecentCSV As Integer = 0
            For Each File In files
                If System.IO.Path.GetFileName(File).StartsWith("_MACHINE_") And
                    System.IO.Path.GetExtension(File) = ".CSV" Then
                    If System.IO.Path.GetFileName(File).Count = 17 Then


                        years__.Add(System.IO.Path.GetFileName(File).Substring(System.IO.Path.GetFileName(File).Length - 8, 4))

                    End If
                End If
            Next
            If years__.Count > 1 Then
                Select_years.ShowDialog()
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles BTN_DefaultFolder.Click
        TB_ServerIp.Text = "C:\_eNETDNC"
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CB_LocalDatabase.CheckedChanged
        If Me.Width < 800 And CB_LocalDatabase.Checked Then
            Me.Width = 500
            ' Button5.Location = New Point(865, 1)
            'Button4.Location = New Point(703, Button4.Location.Y)
            'Button4.Text = "ok"
        Else
            ' Me.Width = 500
            ' Button5.Location = New Point(865, 1)
            'Button4.Location = New Point(703, Button4.Location.Y)
            'Button4.Text = "ok"
        End If

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
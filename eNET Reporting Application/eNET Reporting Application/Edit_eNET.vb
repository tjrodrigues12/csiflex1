Imports System.IO
Imports CSI_Library.EnetClient


Public Class Edit_eNET

    Public clt As New CSI_Library.EnetClient
    Public path As String = CSI_Library.CSI_Library.serverRootPath
    Public machine_list As New Dictionary(Of String, String)
    Public chemin_eNET As String
    Public Shared comingfromform1 As Boolean = False
    Public service As New CSIFlexServerService.ServiceLibrary
    Public ButtonBrowseIsClicked As Boolean = False
    '-----------------------------------------------------------------------------------------------------------------------
    ' MOVE FORME
    '  
    '-----------------------------------------------------------------------------------------------------------------------
#Region "form move"

    Dim _MouseDown As Boolean, _MouseX As Integer, _MouseY As Integer

    Private Sub form_mouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        _MouseDown = True
        _MouseX = e.X
        _MouseY = e.Y
    End Sub

    Private Sub Form_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        _MouseDown = False
    End Sub

    Private Sub Form_Mousemove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove


        If _MouseDown = True Then
            Form1.SuspendLayout()
            Me.Left = Me.Left + (e.X - _MouseX)
            Me.Top = Me.Top + (e.Y - _MouseY)
            If Me.Top < 20 Then Me.Top = 0
            If Me.Left < 20 Then Me.Left = 0
            Form1.ResumeLayout(True)

        End If

    End Sub

#End Region


    '///// Edit_eNET for Load fuctionalities
    Private Sub Edit_eNET_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If File.Exists(path & "\sys\setup_.csys") Then
            Using reader As StreamReader = New StreamReader(path & "\sys\setup_.csys")

                TB_EnetPath.Text = reader.ReadLine()

                reader.Close()
            End Using
        End If
        If File.Exists(path & "\sys\Networkenet_.csys") Then
            Using reader As StreamReader = New StreamReader(path & "\sys\Networkenet_.csys")

                TB_EnetIp.Text = reader.ReadLine()

                reader.Close()
            End Using
        End If
        Me.BringToFront()
    End Sub

    Public years__ As New List(Of String)

    '///// Browse Button Code it browse for eNET Folder and If the data for more than one year exists 
    Private Sub BTN_Browse_Click(sender As Object, e As EventArgs) Handles BTN_Browse.Click

        ButtonBrowseIsClicked = True
        Dim folderDlg As New FolderBrowserDialog
        years__.Clear()
        folderDlg.Description = "Specify the eNET folder"

        Try
            folderDlg.ShowNewFolderButton = False
            If (folderDlg.ShowDialog() = DialogResult.OK) Then
                TB_EnetPath.Text = folderDlg.SelectedPath
                Dim root As Environment.SpecialFolder = folderDlg.RootFolder
            End If

            Dim s As String = System.IO.Path.GetFileName(TB_EnetPath.Text.ToString())
            If File.Exists(TB_EnetPath.Text.ToString() & "\SQL_eNET.odc") Then My.Computer.FileSystem.RenameFile(TB_EnetPath.Text & "\SQL_eNET.odc", "CSI_Database.mdb")

            Dim files As String() = System.IO.Directory.GetFiles(TB_EnetPath.Text.ToString() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
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
            Else
                years__.Add(Now.Year)
                Try
                    If File.Exists(path & "\sys\years_.csys") Then File.Delete(path & "\sys\years_.csys")

                    Using writer As StreamWriter = New StreamWriter(path & "\sys\years_.csys", False)
                        Dim text As String = Now.Year
                        writer.Write(text)
                        writer.Close()
                    End Using

                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub LoadAllYears()
        Try
            years__.Clear()
            If TB_EnetPath.Text.Length > 0 Then
                Dim s As String = System.IO.Path.GetFileName(TB_EnetPath.Text.ToString())
                If File.Exists(TB_EnetPath.Text.ToString() & "\SQL_eNET.odc") Then My.Computer.FileSystem.RenameFile(TB_EnetPath.Text & "\SQL_eNET.odc", "CSI_Database.mdb")

                Dim files As String() = System.IO.Directory.GetFiles(TB_EnetPath.Text.ToString() & "\_REPORTS\") '("C:\_eNETDNC" & "\_REPORTS\")
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
                Else
                    years__.Add(Now.Year)
                    Try
                        If File.Exists(path & "\sys\years_.csys") Then File.Delete(path & "\sys\years_.csys")

                        Using writer As StreamWriter = New StreamWriter(path & "\sys\years_.csys", False)
                            Dim text As String = Now.Year
                            writer.Write(text)
                            writer.Close()
                        End Using

                    Catch ex As Exception

                    End Try
                End If
            Else
                MessageBox.Show("Please Enter ENETDNC Path !")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    '///// Check Button Code which checks if provided IP has the machine displays data on it 
    'If it has data then it will display success
    Private Sub BTN_Check_Click(sender As Object, e As EventArgs) Handles BTN_Check.Click
        ' Pass Enet IP to the RUN function and Get the list of Machines from the Dashboard
        Dim dt As DataTable = clt.Run(TB_EnetIp.Text)
        If dt IsNot Nothing Then

            If dt.Rows.Count = 0 Then
                dt = clt.Run(TB_EnetIp.Text)
            End If

            PictureBox3.Visible = False
            PictureBox2.Visible = True
            ComboBox4.Visible = True
            ComboBox4.Items.Clear()
            For Each machine In dt.Rows
                ComboBox4.Items.Add(machine.item("machine"))
            Next
            Label3.ForeColor = Color.Green
            Label3.Text = "Connection established," & dt.Rows.Count.ToString() & "machines available. "
        Else
            PictureBox3.Visible = True
            PictureBox2.Visible = False
            ComboBox4.Visible = False
            PictureBox3.Visible = False
            Label3.ForeColor = Color.Red
            Label3.Text = "No connection available."
        End If
    End Sub

    '///// Ok Button Code basically Code Flow to Refresh the Grid View 
    Private Sub BTN_Ok_Click(sender As Object, e As EventArgs) Handles BTN_Ok.Click
        Try
            If Not (Me.TB_EnetPath.Text = "") Then
                Using writer As StreamWriter = New StreamWriter(path & "\sys\Networkenet_.csys", False)
                    writer.Write(Me.TB_EnetIp.Text)
                    writer.Close()
                End Using
                Using writer As StreamWriter = New StreamWriter(path & "\sys\setup_.csys", False)
                    writer.Write(Me.TB_EnetPath.Text)
                    writer.Close()
                    'Here we also need to write user credentials if we are connecting to remote PC
                End Using
                Using reader As StreamReader = New StreamReader(path & "\sys\setup_.csys")
                    chemin_eNET = reader.ReadLine()
                    reader.Close()
                End Using
                '==============================================================================
                'Code Flow to Refresh the Grid View 
                '(1) Copy \_SETUP\MonList.sys file to \sys\monlist.csys
                '(2) Write Data of \sys\monlist.csys to \sys\machine_list_.csys
                '(3) Read \sys\machine_list_.csys and write it's value to DGV_Source Gridview using funtion SetupForm2.Load_DGV_Source()
                '==============================================================================
                Try
                    File.Copy(chemin_eNET & "\_SETUP\MonList.sys", path & "\sys\monlist.csys", True)
                Catch ex As Exception
                    MessageBox.Show("Unable to save the eNET machine list : " & ex.Message)
                End Try
                machine_list.Clear() ' Clear Old Values from the machine_list
                Using reader As StreamReader = New StreamReader(path & "\sys\monlist.csys")
                    Dim readed As String = reader.ReadToEnd
                    Dim eachmachine As String() = readed.Split(vbCr)
                    Using writer As New StreamWriter(path & "\sys\machine_list_.csys")
                        Dim eNETsource As String = "eNET,First, "
                        For Each machine In eachmachine
                            Select Case Microsoft.VisualBasic.Strings.Left(machine.Trim(), 3)

                                Case "_MT"

                                Case "_ST"

                                Case Else
                                    If (machine.Trim() <> "") And (machine <> "") Then
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
                If comingfromform1 = False Then
                    SetupForm2.LoadGridviewMachines()
                End If
                'Make sure about below Step
                'service.LoadMachineList()
                Me.Hide()
                'MessageBox.Show("Machine List is updated !")
            Else
                MessageBox.Show("You need to enter your eNET folder")
            End If
            'If ButtonBrowseIsClicked = False Then
            '    LoadAllYears()
            'End If
            record_years_to_import()
        Catch ex As Exception
            MessageBox.Show("Error Detected :" + ex.ToString())
        End Try
    End Sub

    '///// Write years in years_.csys
    Private Sub record_years_to_import()
        Try
            If File.Exists(path & "\sys\years_.csys") Then File.Delete(path & "\sys\years_.csys")

            Using writer As StreamWriter = New StreamWriter(path & "\sys\years_.csys", False)
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

    '///// Cancel Button Code 
    Private Sub BTN_Cancel_Click(sender As Object, e As EventArgs) Handles BTN_Cancel.Click
        Me.Hide()
    End Sub

    '/////  Default Folder Setup for eNETDNC
    Private Sub BTN_Default_Click(sender As Object, e As EventArgs) Handles BTN_Default.Click
        TB_EnetPath.Text = "C:\_eNETDNC"
    End Sub

    Private Sub BTN_ChangeToMTC_Click(sender As Object, e As EventArgs)
        Dim mtc_add As New EnetToMtc
        mtc_add.ShowDialog()
    End Sub
End Class
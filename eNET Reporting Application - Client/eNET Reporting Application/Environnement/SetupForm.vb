Imports System.ComponentModel
Imports System.IO
'Imports CSIComService
Imports System.IO.File
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Windows
Imports CSI_Library
Imports CSIFLEX.Utilities
Imports Microsoft.Win32
Imports MySql.Data.MySqlClient
'Excel
Imports Excel = Microsoft.Office.Interop.Excel

'-----------------------------------------------------------------------------------------------------------------------
'Form 2 : Configuration
'-----------------------------------------------------------------------------------------------------------------------
Public Class SetupForm
    Private trd As Thread
    Public CSI_Lib As New CSI_Library.CSI_Library(False)
    Public clt As New CSI_Library.EnetClient
    'Public NT As New CSI_Library.ServiceNT

    Public dbUpdate_needed As Boolean = False
    Public dbConnectStr As String
    Public tmp_txtbox1 As String
    Public tmp_txtbox2 As String
    Public targetColor_ As String
    Public identified As String
    Public loaded As Boolean = False
    Private TypeDePeriode As String 'Type Periode pour reporting 
    Private RestartServer As Boolean = False
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath


#Region "Browse"

    '-----------------------------------------------------------------------------------------------------------------------
    '  Browse, eNET folder
    '-----------------------------------------------------------------------------------------------------------------------
    'Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    '    Dim folderDlg As New FolderBrowserDialog
    '    folderDlg.Description = "Specify the eNET folder"

    '    Try
    '        folderDlg.ShowNewFolderButton = False
    '        If (folderDlg.ShowDialog() = DialogResult.OK) Then
    '            TB_eNETDNC.Text = folderDlg.SelectedPath

    '            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(" Unable to save th eNET path ") ' & ex.Message)
    '        CSI_Lib.LogClientError(" Unable to save th eNET path : " & ex.Message)
    '    End Try
    'End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    '  Browse, db folder
    '-----------------------------------------------------------------------------------------------------------------------
    'Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs)
    '    Dim folderDlg As New FolderBrowserDialog

    '    folderDlg.Description = "Choose or Specify a folder for the database"
    '    Try
    '        folderDlg.ShowNewFolderButton = True
    '        If (folderDlg.ShowDialog() = DialogResult.OK) Then
    '            TB_Database.Text = folderDlg.SelectedPath
    '            old_db_path = TB_Database.Text
    '            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
    '        End If
    '        Dim s As String = System.IO.Path.GetFileName(TB_Database.Text.ToString())
    '        If File.Exists(TB_Database.Text.ToString() & "\SQL_eNET.odc") Then My.Computer.FileSystem.RenameFile(TB_Database.Text & "\SQL_eNET.odc", "CSI_Database.mdb")

    '    Catch ex As Exception
    '        'MessageBox.Show(ex.Message)
    '        CSI_Lib.LogClientError("error in database folder selection:" + ex.Message)
    '    End Try
    'End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' Default chemin enet
    '-----------------------------------------------------------------------------------------------------------------------
    'Private Sub BTN_eNETDNC_DefaultPath_Click(sender As Object, e As EventArgs)
    '    TB_eNETDNC.Text = "C:\_eNETDNC"
    '    Reporting_application.chemin_eNET = TB_eNETDNC.Text
    'End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' Default chemin bdd
    '-----------------------------------------------------------------------------------------------------------------------
    'Private Sub BTN_Database_DefautlPath_Click(sender As Object, e As EventArgs)

    '    Try

    '        TB_Database.Text = rootPath & "\sys\"
    '        Reporting_application.chemin_bd = TB_Database.Text

    '    Catch ex As Exception

    '        'MessageBox.Show(ex.Message)
    '        CSI_Lib.LogClientError("Error in database default path:" + ex.Message)
    '    End Try

    'End Sub

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
            CSI_Lib.LogClientError("error converting month:" + ex.Message)
            Return Nothing
        End Try
    End Function

    Public old_db_path As String = ""   'The path at the the openning of the form
    Public old_enet_path As String = ""  'The path at the the openning of the form
    Public found As Boolean = False

    Dim RMinstalled As Boolean = False


    Private Sub SetupForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.StartPosition = FormStartPosition.CenterScreen

        txtIPDatabase.Text = CSIFLEXSettings.Instance.DatabaseIp
        cmbFirstDayWeek.SelectedIndex = CSIFLEXSettings.Instance.FirstDayOfWeek
        cmbLastDayWeek.SelectedIndex = CSIFLEXSettings.Instance.LastDayOfWeek
        cmbBeginOfYear.SelectedIndex = 0
        txtDefaultReportFolder.Text = CSIFLEXSettings.Instance.ReportFolder
        btnOthersColor.BackColor = Color.FromArgb(CSIFLEXSettings.Instance.OtherColor)
        cmbBeginOfYear.SelectedIndex = CSIFLEXSettings.Instance.FirstMonthOfYear - 1
        numRefreshRate.Value = CSIFLEXSettings.Instance.RefreshRate
        numTargetLine.Value = CSIFLEXSettings.Instance.TargetLine
        btnTargetColor.BackColor = Color.FromArgb(CSIFLEXSettings.Instance.TargetColor)

        If cmbFirstDayWeek.SelectedValue <> Nothing And cmbLastDayWeek.SelectedValue <> Nothing And cmbBeginOfYear.SelectedValue <> Nothing Then Button2.Enabled = True

        CSI_Lib.connectionString = CSIFLEXSettings.Instance.ConnectionString

        '    machineListe = CSI_Lib.LoadMachines()
        Try
            'If File.Exists(rootPath & "\sys\setup_.csys") Then
            '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\setup_.csys")
            '        TB_eNETDNC.Text = reader.ReadLine()

            '        old_enet_path = TB_eNETDNC.Text
            '        reader.Close()
            '    End Using
            'Else

            '    TB_eNETDNC.Text = "C:\_eNETDNC"
            '    old_enet_path = TB_eNETDNC.Text

            'End If

            If cboxUseEnetColors.Checked Then
                Color_Path.Text = ""
            Else
                If File.Exists(rootPath & "\sys\SetupColor_.csys") Then
                    Using reader As StreamReader = New StreamReader(rootPath & "\sys\SetupColor_.csys")
                        Color_Path.Text = reader.ReadLine()
                        reader.Close()
                    End Using
                End If
            End If

            'If File.Exists(rootPath & "\sys\otherColor_.csys") Then
            '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\otherColor_.csys")
            '        Dim col As String = CInt(reader.ReadLine())
            '        reader.Close()
            '        btnOthersColor.BackColor = Color.FromArgb(col)
            '    End Using
            'Else
            '    Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\otherColor_.csys")
            '        btnOthersColor.BackColor = Color.FromArgb(-32768)
            '        writer.Write(-32768)
            '        writer.Close()

            '    End Using
            'End If

            'If File.Exists(rootPath & "\sys\firstdate_.csys") Then
            '    'Select Case FIRST(column_name) FROM table_name
            '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\firstdate_.csys")
            '        dtpFirstDateParts.Value = DateTime.Parse(reader.ReadLine().ToString())
            '        reader.Close()
            '    End Using
            'End If

            'If File.Exists(rootPath & "\sys\Setupdb_.csys") Then
            '    Using reader2 As StreamReader = New StreamReader(rootPath & "\sys\Setupdb_.csys")
            '        txtIPDatabase.Text = reader2.ReadLine()
            '        old_db_path = txtIPDatabase.Text
            '        reader2.Close()
            '    End Using
            'Else
            '    txtIPDatabase.Text = rootPath & "\sys\"
            '    old_db_path = txtIPDatabase.Text
            'End If

            'Dim item As String = ""

            'If File.Exists(rootPath & "\sys\Setupdt_.csys") Then
            '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\Setupdt_.csys")
            '        item = reader.ReadLine()
            '        If item IsNot Nothing Then
            '            cmbFirstDayWeek.SelectedIndex = (Val(item(0)))
            '            cmbLastDayWeek.SelectedIndex = (Val(item(1)))
            '            item = reader.ReadLine()
            '            If item IsNot Nothing Then
            '                cmbBeginOfYear.SelectedIndex = (Val(item))
            '            End If
            '        End If
            '        reader.Close()
            '    End Using
            'Else
            '    cmbFirstDayWeek.SelectedIndex = 1
            '    cmbLastDayWeek.SelectedIndex = 5
            '    cmbBeginOfYear.SelectedIndex = 0
            'End If

            'If File.Exists(rootPath & "\sys\defaultReportFolder_.csys") Then
            '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\defaultReportFolder_.csys")
            '        txtDefaultReportFolder.Text = reader.ReadLine()
            '        reader.Close()
            '    End Using
            'End If

            'If Welcome.CSIF_version = 3 Then
            '    If File.Exists(rootPath & "\sys\UseCSIDashboard_.csys") Then
            '        CHKB_UseCSIDahboard.Checked = True
            '    Else
            '        CHKB_UseCSIDahboard.Checked = False
            '    End If
            'End If

            'Placed Before target value because setting the numericupdown value calls the valuechanged method using the button background color
            'If Exists(rootPath & "\sys\targetColor_.csys") Then
            '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\targetColor_.csys")
            '        targetColor_ = CInt(reader.ReadLine())
            '        reader.Close()
            '    End Using
            '    BTN_Color.BackColor = Color.FromArgb(targetColor_)
            'End If

            'If Exists(rootPath & "\sys\target_.csys") Then
            '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\target_.csys")
            '        numTargetLine.Value = CInt(reader.ReadLine()) 'this line will call NumericUpDownValueChanged
            '        reader.Close()
            '    End Using
            'End If


            'If Exists(rootPath & "\sys\AutoScroll_.csys") Then
            '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\AutoScroll_.csys")
            '        CHKB_AutoScroll.Checked = Boolean.Parse(reader.ReadLine())
            '        reader.Close()
            '    End Using
            'End If


            'If Exists(rootPath & "\sys\RefreshRate_.csys") Then
            '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\RefreshRate_.csys")
            '        NUD_Refresh.Value = Integer.Parse(reader.ReadLine()) / 1000
            '        reader.Close()
            '    End Using
            'End If

        Catch ex As Exception
            MessageBox.Show("Error loading configuration files")
            CSI_Lib.LogClientError("error loading config:" + ex.Message)
            Me.Close()
        End Try

        'Try

        '    ' the eNET http server IP : 
        '    If File.Exists(rootPath & "\sys\Networkenet_.csys") Then
        '        Using reader As StreamReader = New StreamReader(rootPath & "\sys\Networkenet_.csys")

        '            Me.TB_IpAdress.Text = reader.ReadLine
        '            'Dim netInfo() As String = reader.ReadLine.Split(":")

        '            'Me.TB_IpAdress.Text = netInfo(0)
        '            'Me.TB_Port.Text = netInfo(1)
        '            'reader.Close()
        '        End Using
        '    Else
        '        Using reader As StreamReader = New StreamReader(rootPath & "\sys\SrvDBpath.csys")
        '            Dim netInfo() As String = reader.ReadLine.Split(":")

        '            Me.TB_IpAdress.Text = netInfo(0)
        '            Me.TB_Port.Text = "8080"
        '            reader.Close()
        '        End Using
        '    End If
        'Catch ext As Exception
        '    MessageBox.Show("Unable to read the ip of the enet server ") ' & ext.Message)
        '    CSI_Lib.LogClientError("Unable to read the ip of enet:" + ext.Message)
        'End Try


        Try
            'Load_datagridview()

            'Dim loadmachinelist As New Task(AddressOf Load_datagridview)
            'loadmachinelist.Start()
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            Log.Error("Error loading datagridview", ex)
        End Try

    End Sub


    'Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
    '    Me.Close()
    'End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' Activat/Deactivat the autoreporting checkbox if no path
    '-----------------------------------------------------------------------------------------------------------------------
    'Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs)
    '    If CHKB_UseCSIDahboard.Text <> "" Then
    '        CHKB_UseCSIDahboard.Enabled = True
    '    Else
    '        CHKB_UseCSIDahboard.Enabled = False
    '    End If
    'End Sub


    Private Sub BtnTargetColor_Click(sender As Object, e As EventArgs) Handles btnTargetColor.Click

        Dim colordial As New ColorDialog

        colordial.ShowDialog()

        CSIFLEXSettings.Instance.TargetColor = colordial.Color.ToArgb()
        CSIFLEXSettings.Instance.SaveSettings()

        If Report_BarChart.Visible = True Then Report_BarChart.target(numTargetLine.Value, colordial.Color.ToArgb)

        btnTargetColor.BackColor = Color.FromArgb(colordial.Color.ToArgb)

        'Try
        '    If Exists(rootPath & "\sys\targetColor_.csys") Then
        '        System.IO.File.Delete(rootPath & "\sys\targetColor_.csys")

        '        Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\targetColor_.csys")
        '            writer.Write(colordial.Color.ToArgb)
        '            writer.Close()
        '        End Using
        '    Else
        '        Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\targetColor_.csys")
        '            writer.Write(colordial.Color.ToArgb)
        '            writer.Close()
        '        End Using
        '    End If

        '    CSIFLEXSettings.Instance.Reload()

        '    btnTargetColor.BackColor = Color.FromArgb(colordial.Color.ToArgb)

        'Catch ex As Exception

        '    MessageBox.Show("Unable to save the color of the target line ") ' & ex.Message, vbSystemModal + vbCritical)
        '    CSI_Lib.LogClientError("Unable to save the color of the target line :" + ex.Message)
        'End Try
    End Sub


    Private Sub BtnRemoveTarget_Click(sender As Object, e As EventArgs) Handles btnRemoveTarget.Click

        CSIFLEXSettings.Instance.TargetColor = -32768
        CSIFLEXSettings.Instance.TargetLine = 0
        numTargetLine.Value = 0

        'Try
        '    If Exists(rootPath & "\sys\target_.csys") Then
        '        System.IO.File.Delete(rootPath & "\sys\target_.csys")
        '    End If
        '    numTargetLine.Value = 0
        'Catch ex As Exception
        '    MessageBox.Show("Unable to save the target value ") ' & ex.Message, vbSystemModal + vbCritical)
        '    CSI_Lib.LogClientError("Unable to save the target value " & ex.Message)
        'End Try
    End Sub


    Private Sub BtnLicense_Click(sender As Object, e As EventArgs) Handles btnLicense.Click

        'Dim db_authPath As String = Nothing
        'If (File.Exists(rootPath + "/sys/SrvDBpath.csys")) Then
        '    Using reader As New StreamReader(rootPath + "/sys/SrvDBpath.csys")
        '        db_authPath = reader.ReadLine()
        '    End Using
        'End If
        'Dim connectionString As String
        'connectionString = $"SERVER={ db_authPath };{CSI_Library.CSI_Library.MySqlServerBaseString}"

        Dim licenseForm As New LicenseManagement(CSIFLEXSettings.Instance.ConnectionString)
        licenseForm.ShowDialog()

    End Sub


#Region "Machines connection setup"

    'Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs)

    'End Sub


    'mtconnect_.sys :
    '==========================================================================
    ' machine(str) , ip(str) , display(bool) 
    '
    '==========================================================================


    '===================================================================================================
    ' CHECK THE CONNECTION
    '===================================================================================================
    'Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs)

    '    Try
    '        If (e.ColumnIndex = DGV_Source.Columns("Check").Index And e.RowIndex >= 0) Then

    '            If (Not IsNothing(DGV_Source.Rows(e.RowIndex).Cells(2).Value)) Then

    '                If (DGV_Source.Rows(e.RowIndex).Cells(2).Value <> "eNET") Then

    '                    If Not DGV_Source.Rows(e.RowIndex).Cells(2).Value.ToString().StartsWith("http://") Then DGV_Source.Rows(e.RowIndex).Cells(2).Value = "http://" & DGV_Source.Rows(e.RowIndex).Cells(2).Value

    '                    If mtconnect_ping(DGV_Source.Rows(e.RowIndex).Cells(2).Value.ToString()) = True Then
    '                        DGV_Source.Rows(e.RowIndex).Cells(4).Value = "Passed"
    '                        DGV_Source.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.GreenYellow
    '                    Else
    '                        DGV_Source.Rows(e.RowIndex).Cells(4).Value = "OffLine"
    '                        DGV_Source.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Red
    '                    End If

    '                Else
    '                    'Dim dt As DataTable = clt.Run(TB_IpAdress.Text + ":" + TB_Port.Text)
    '                    Dim dt As DataTable = clt.Run(TB_IpAdress.Text)

    '                    If dt IsNot Nothing Then

    '                        For Each machine In dt.Rows

    '                            If (machine.item("machine")) = DGV_Source.Rows(e.RowIndex).Cells(5).Value Then
    '                                If machine.item("machine") <> "NO EMONITOR" Then
    '                                    DGV_Source.Rows(e.RowIndex).Cells(4).Value = "Passed"
    '                                    DGV_Source.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.GreenYellow
    '                                Else
    '                                    DGV_Source.Rows(e.RowIndex).Cells(4).Value = "offLine"
    '                                    DGV_Source.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Red

    '                                End If
    '                            End If
    '                        Next

    '                    Else

    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        'MessageBox.Show(ex.Message)
    '        CSI_Lib.LogClientError("error loading sources:" + ex.Message)
    '    End Try


    '    '   https://www.youtube.com/watch?v=CHyESZfaxxE
    'End Sub

    '===================================================================================================
    ' Right clic on the datagridview
    '===================================================================================================
    'Private Sub DataGridView1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    'End Sub



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
                Return True
            Else
                MessageBox.Show("CSIFLEX can not connect to the MTConnect Agent") ' & myHttpWebResponse.StatusDescription)
                Return False
            End If

        Catch ex As Exception
            If ex.Message = "The operation has timed out" Then
                Return False
            Else
                MessageBox.Show("CSIFLEX can not reach the MTConnect Agent") ' & ex.Message)
                Return False
            End If
        End Try

    End Function


    '===================================================================================================
    ' add a new row in the datagridview1
    '===================================================================================================
    'Private Sub DataGridView1_newrow(sender As Object, e As DataGridViewRowsAddedEventArgs)

    '    'Last Row:
    '    DGV_Source.Rows(DGV_Source.Rows.Count - 1).Cells(3).Value = "Check"
    'End Sub


    '===================================================================================================
    '  Save the data
    '===================================================================================================
    'Private Sub saveDatagridview1()

    '    Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\machine_list_.csys", False)
    '        Dim display_CheckBox As String = ""
    '        Dim machine_tmp As String
    '        For Each row As DataGridViewRow In DGV_Source.Rows
    '            If Not IsNothing(row.Cells(5).Value) Then ' DONT TAKE AN EMPTY ROW
    '                machine_tmp = row.Cells(5).Value.ToString()
    '                If row.Cells(0).Value = False Then
    '                    display_CheckBox = 0
    '                Else
    '                    display_CheckBox = 1
    '                    'disabled since all the machines are automatically added
    '                    'If Welcome.CSIF_version <> 1 Then recurciveadd(machine_tmp)
    '                End If

    '                If (GlobalVariables.reloadByForm1 = False) Then
    '                    GlobalVariables.reloadByForm1 = True
    '                End If
    '                writer.WriteLine(machine_tmp & "," & row.Cells(2).Value & "," & display_CheckBox.ToString())

    '            End If
    '        Next
    '        writer.Close()
    '    End Using

    'End Sub


    Private Sub recurciveadd(name As String)

        ' checked_in_treeview1.Clear()

        Dim aNode As TreeNode

        For Each aNode In Config_report.TreeView_machine.Nodes()
            addRecursive(aNode, name)
        Next
        If found = False Then
            Dim n As New TreeNode
            n.Name = name
            n.Text = name

            If Welcome.CSIF_version = 1 Then

                If n.Nodes.Count > 0 Then n.ImageIndex = 2
                n.ToolTipText = "No monitoring data available"
            Else
                If n.Nodes.Count > 0 Then n.ImageIndex = 2

            End If
            Config_report.TreeView_machine.Nodes.Add(n)
        End If

    End Sub


    Private Sub addRecursive(ByVal n As TreeNode, name As String)

        Dim aNode As TreeNode
        n.SelectedImageIndex = n.ImageIndex
        If n.Name = name Then
            found = True

        End If


        For Each aNode In n.Nodes
            addRecursive(aNode, name)
        Next

    End Sub


    Private Sub TabControl1_select(sender As Object, e As EventArgs) _
        Handles TabControl1.SelectedIndexChanged

        If (TabControl1.SelectedTab.Text) = "Machines" Then
            'Call Load_datagridview()
            'Button12.Visible = True
            'Button14.Visible = True
        Else
            'Button12.Visible = False
            'Button14.Visible = False
        End If
    End Sub


    '===================================================================================================
    ' Fill the datagridview
    '===================================================================================================
    'Private Sub Load_datagridview()

    '    Dim currentrow As String()
    '    Dim readed As String = ""
    '    Dim chked As New DataGridViewCheckBoxCell
    '    Dim machName As String
    '    Dim enetName As String

    '    chked.Value = False

    '    DGV_Source.Rows.Clear()

    '    If Exists(rootPath & "\sys\machine_list_.csys") Then

    '        Using reader As StreamReader = New StreamReader(rootPath & "\sys\machine_list_.csys")

    '            While Not reader.EndOfStream
    '                readed = reader.ReadLine()

    '                If readed <> "" Then

    '                    currentrow = readed.Split(",")
    '                    If UBound(currentrow) > 1 Then

    '                        If currentrow(0) <> "" Then

    '                            If currentrow(2) = 1 Then chked.Value = True

    '                            machName = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.EnetName = currentrow(0) Or m.Id.ToString() = currentrow(0)).MachineName
    '                            enetName = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.EnetName = currentrow(0) Or m.Id.ToString() = currentrow(0)).EnetName

    '                            DGV_Source.Rows.Add(chked.Value, machName, currentrow(1), "Check", "", enetName)
    '                        End If
    '                    End If
    '                End If
    '            End While

    '        End Using
    '    End If
    'End Sub

#End Region


    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click

        'If DGV_Source.SelectedCells IsNot Nothing Then

        '    identified = DGV_Source.Rows(DGV_Source.SelectedCells(0).RowIndex).Cells("IP").Value
        '    '  Edit.ShowDialog()
        '    If Not identified = "eNET" Then
        '        Edit_mtconnect.IP = identified
        '        Edit_mtconnect.Show()
        '    Else
        '        MessageBox.Show("Feature not available on the current version")

        '    End If
        'End If

    End Sub


    Private Sub EditToolStripadd_Click(sender As Object, e As EventArgs) Handles ContextMenuADD.Click
        Addsource.ShowDialog()


    End Sub


    '===================================================================================================
    ' ReloAD MTCONNECT MACHINES
    '===================================================================================================
    '    Private Sub Button12_Click_1(sender As Object, e As EventArgs)
    '        Try
    '            Dim machines As String() = CSI_Lib.LoadMachines()
    'redo:

    '            Dim rowscount As Integer = (DGV_Source.Rows.Count) - 1
    '            For i = 0 To rowscount
    '                If DGV_Source.Rows.Item(i).Cells.Item(2).Value IsNot Nothing Then
    '                    If DGV_Source.Rows.Item(i).Cells.Item(2).Value.ToString() = "eNET" Then
    '                        DGV_Source.Rows.Remove(DGV_Source.Rows(i))
    '                        GoTo redo
    '                    End If
    '                End If


    '            Next
    '            For Each row As DataGridViewRow In DGV_Source.Rows

    '                If row.Cells.Item(2).Value IsNot Nothing Then If row.Cells.Item(2).Value.ToString() = "eNET" Then DGV_Source.Rows.Remove(DGV_Source.Rows(row.Index))

    '            Next
    '            For Each machine In machines
    '                If machine IsNot Nothing Then
    '                    If machine.StartsWith("_MT") Or machine.StartsWith("_ST") Then
    '                        'Nothing
    '                    Else
    '                        DGV_Source.Rows.Add(False, machine, "eNET", " - ", "")
    '                    End If
    '                End If
    '            Next
    '            loaded = True
    '        Catch ex As Exception
    '            'MessageBox.Show("CSIFLEX to load the eNET machines : " & ex.Message)
    '            MessageBox.Show("Error loading enet machines")
    '            CSI_Lib.LogClientError("Error loading enet machines:" + ex.Message)
    '        End Try

    '    End Sub


    '===================================================================================================
    ' Check the connection with the enet http server
    '===================================================================================================


    Private Sub BTN_Check_Click(sender As Object, e As EventArgs) Handles BTN_Check.Click

        'Dim dt As DataTable = clt.Run(TB_IpAdress.Text + ":" + TB_Port.Text)
        Dim dt As DataTable = clt.Run(TB_IpAdress.Text)

        If dt IsNot Nothing Then

            If dt.Rows.Count = 0 Then
                'dt = clt.Run(TB_IpAdress.Text + ":" + TB_Port.Text)
                dt = clt.Run(TB_IpAdress.Text)
            End If

            PictureBox1.Visible = True
            PictureBox2.Visible = False
            ComboBox4.Visible = True
            Label10.ForeColor = Color.Green
            ComboBox4.Items.Clear()

            For Each machine In dt.Rows
                ComboBox4.Items.Add(machine.item("machine"))
            Next
            Label10.Text = "Connection established," & dt.Rows.Count.ToString() & "machines available. "
            Try

                'Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Network_.csys", False)
                '    writer.Write(Me.TextBox4.Text)
                '    writer.Close()
                'End Using


                If File.Exists(rootPath & "\sys\Networkenet_.csys") Then
                    Using r As StreamReader = New StreamReader(rootPath & "\sys\Networkenet_.csys", False)

                        Dim strTemp As String = r.ReadLine()
                        If (strTemp Is Nothing) Or (strTemp <> Me.TB_IpAdress.Text + ":" + Me.TB_Port.Text) Then
                            RestartServer = True
                        End If

                    End Using
                End If

                Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Networkenet_.csys", False)
                    'writer.Write(Me.TB_IpAdress.Text + ":" + Me.TB_Port.Text)
                    writer.Write(Me.TB_IpAdress.Text)
                    writer.Close()
                End Using



            Catch ext As Exception
                MessageBox.Show("Unable to save the ip of the CSIFLEX server ") ' & ext.Message)
                CSI_Lib.LogClientError("unable to save the ip of the csiflex server:" + ext.Message)
            End Try
        Else
            RestartServer = False
            Label10.ForeColor = Color.Red
            PictureBox2.Visible = True
            ComboBox4.Visible = False
            PictureBox1.Visible = False
            Label10.Text = "No connection available."
        End If

    End Sub


    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles BTN_LoadLicense.Click

        Try


            Dim ofd_license As New OpenFileDialog

            'ofd_license.InitialDirectory = "c:\"
            ofd_license.Filter = "dl1 files (*.dl1)|*.dl1"
            ofd_license.Title = "Select your CSI Flex license file"
            'ofd_license.FilterIndex = 2
            ofd_license.RestoreDirectory = True
            Close()
            ofd_license.ShowDialog()
            If (ofd_license.FileName.Length > 0) Then
                CopyLicense(ofd_license.FileName)
                MessageBox.Show("Please reboot CSIFlex to apply the changes")
            End If
        Catch ex As Exception
            MsgBox("The application needed to be start in admin to change license")
        End Try

    End Sub


    Private Sub CopyLicense(filename As String)
        Dim path As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)

        'File.Copy(filename, path.DirectoryName & "\Twincsi.dl1", True)
        File.Copy(filename, rootPath & "\Twincsi.dl1", True)
        'File.Delete(filename)
        'File.Copy(path.DirectoryName & "\Twincsi.dl1", path.DirectoryName & "\wincsi.dl1", True)
        'File.Delete(path.DirectoryName & "\Twincsi.dl1")
        File.Copy(rootPath & "\Twincsi.dl1", rootPath & "\wincsi.dl1", True)
        File.Delete(rootPath & "\Twincsi.dl1")

        If (File.Exists(rootPath & "\sys\CSIFv_.csys")) Then
            File.Delete(rootPath & "\sys\CSIFv_.csys")
        End If

        'DialogResult = Windows.Forms.DialogResult.OK
        'Me.Close()

    End Sub


    Public Function test_createPivoteTable(_Mach As String, _strtDate As String(), _endDate As String(), xlWorkBook As Excel.Workbook, xlWorkSheet As Excel.Worksheet)

        Dim strtDate As String() = _strtDate
        Dim endDate As String() = _endDate

        Dim connStr As String = "ODBC;DSN=MS Access Database;DBQ=" + CSI_Lib.db_path(True) + "CSI_Database.mdb;DriverId=25;FIL=MS Access;MaxBufferSize=4096;PageTimeout=5;"



        Dim command As String = "SELECT * from tbl_" + _Mach + " where year_  between " + strtDate(0) + " and " + endDate(0) + " and month_ between " + strtDate(1) + " and " + endDate(1) + " and day_ between " + strtDate(2) + " and " + endDate(2)
        Dim workbookPath As String = "c:\temp\ReportTemplate.xlsx"

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim oRng As Excel.Range


        Dim pivotTables As Excel.PivotTables
        Dim pivotTable As Excel.PivotTable



        Dim pivotCache As Excel.PivotCache = xlWorkBook.PivotCaches().Add(Excel.XlPivotTableSourceType.xlExternal, misValue)
        pivotCache.Connection = connStr
        pivotCache.CommandText = command
        pivotCache.CommandType = Excel.XlCmdType.xlCmdSql

        xlWorkSheet = xlWorkBook.ActiveSheet

        oRng = xlWorkSheet.Range("A1")

        pivotTables = xlWorkSheet.PivotTables(misValue)
        pivotTable = pivotTables.Add(pivotCache, oRng, _Mach, misValue, misValue)
        pivotTable.DisplayFieldCaptions = True

        pivotTable.SmallGrid = True
        pivotTable.ShowTableStyleRowStripes = True
        pivotTable.TableStyle2 = "PivotStyleLight1"

        'Ajout du rowHeader colonne sommes des temps
        Dim rowfieldDate As Excel.PivotField = pivotTable.PivotFields("date_")
        rowfieldDate.Caption = "date"
        rowfieldDate.Orientation = Excel.XlPivotFieldOrientation.xlPageField


        Dim rowfieldStatut As Excel.PivotField = pivotTable.PivotFields("status")
        rowfieldStatut.Caption = "Events"
        rowfieldStatut.Orientation = Excel.XlPivotFieldOrientation.xlRowField



        pivotTable.AddDataField(pivotTable.PivotFields("cycletime"), _Mach, Excel.XlConsolidationFunction.xlSum)

        ''Ajout de la colonne calculé sommes des temps en pourcent
        Dim rowfield_PercentOfTotal As Excel.PivotField = pivotTable.PivotFields("cycletime")
        rowfield_PercentOfTotal.Caption = "%"
        rowfield_PercentOfTotal.Orientation = Excel.XlPivotFieldOrientation.xlDataField
        rowfield_PercentOfTotal.Calculation = Excel.XlPivotFieldCalculation.xlPercentOfTotal

        'Differente colonne calculé en ligne
        pivotTable.DataPivotField.Orientation = Excel.XlPivotFieldOrientation.xlColumnField


        Dim xlCharts As Excel.ChartObjects = xlWorkSheet.ChartObjects(misValue)
        Dim myChart As Excel.ChartObject = xlCharts.Add(200, 10, 200, 200)
        Dim chartPage As Excel.Chart = myChart.Chart

        chartPage.SetSourceData(pivotTable.TableRange2, misValue)
        chartPage.ChartType = Excel.XlChartType.xlPie

        'Dim xlCharts2 As Excel.ChartObjects = xlWorkSheet.ChartObjects(misValue)
        'Dim myChart2 As Excel.ChartObject = xlCharts.Add(10, 300, 400, 200)
        'Dim chartPage2 As Excel.Chart = myChart.Chart

        'chartPage2.SetSourceData(pivotTable.TableRange2, misValue)
        'chartPage2.ChartType = Excel.XlChartType.xlColumnClustered

    End Function


    Private Sub TabPage3_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs)

    End Sub


    'Private Sub BTN_ApplyReporting_Click(sender As Object, e As EventArgs)


    '    If (Not File.Exists(rootPath & "\sys\repotSetup.csys")) Then
    '        File.Create(rootPath & "\sys\repotSetup.csys")
    '    End If
    '    Using w As StreamWriter = New StreamWriter(rootPath & "\sys\repotSetup.csys", False)
    '        If (CHKB_UseCSIDahboard.Checked = True) Then
    '            w.WriteLine("reporting=true")
    '            w.WriteLine("path=" + TextBox3.Text)
    '            w.WriteLine("time=" + String.Format("hh:mm", DateTimePicker1.Value))
    '            w.WriteLine("daily=" + CheckBox2.Checked)
    '            w.WriteLine("weekly=" + CheckBox3.Checked)
    '            w.WriteLine("monthly=" + CheckBox3.Checked)
    '        Else
    '            w.WriteLine("reporting=false")
    '        End If
    '    End Using

    'End Sub


    Private Sub BTN_Cancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub


    Private Sub NUD_Refresh_ValueChanged(sender As Object, e As EventArgs) Handles numRefreshRate.ValueChanged
        GlobalVariables.refresh_Interval = numRefreshRate.Value * 1000
    End Sub


    Private Sub BTN_BrowseFolder_Click(sender As Object, e As EventArgs) Handles BTN_BrowseFolder.Click
        Dim folderDlg As New FolderBrowserDialog

        folderDlg.ShowNewFolderButton = False
        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            txtDefaultReportFolder.Text = folderDlg.SelectedPath
            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
        End If
    End Sub


    Private Sub BTN_StartupConfig_Click(sender As Object, e As EventArgs) Handles BTN_StartupConfig.Click
        Dim startupcondifg_form As New StartupConfig
        startupcondifg_form.StartPosition = FormStartPosition.CenterScreen
        startupcondifg_form.ShowDialog()
    End Sub


    Private Sub BTN_BrowseColor_Click(sender As Object, e As EventArgs) Handles BTN_BrowseColor.Click
        Dim folderDlg As DialogResult = OpenFileDialog1.ShowDialog()
        Dim path As String = OpenFileDialog1.FileName
        If folderDlg = Windows.Forms.DialogResult.OK Then
            Color_Path.Text = path
        End If
    End Sub


    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click

        CSIFLEXSettings.Instance.ReportFolder = txtDefaultReportFolder.Text
        CSIFLEXSettings.Instance.RefreshRate = numRefreshRate.Value * 1000
        CSIFLEXSettings.Instance.DatabaseIp = txtIPDatabase.Text
        CSIFLEXSettings.Instance.TargetLine = numTargetLine.Value
        CSIFLEXSettings.Instance.TargetColor = btnTargetColor.BackColor.ToArgb()
        CSIFLEXSettings.Instance.FirstDayOfWeek = cmbFirstDayWeek.SelectedIndex
        CSIFLEXSettings.Instance.LastDayOfWeek = cmbLastDayWeek.SelectedIndex
        CSIFLEXSettings.Instance.FirstMonthOfYear = cmbBeginOfYear.SelectedIndex + 1
        CSIFLEXSettings.Instance.SaveSettings()

        Report_BarChart.target_ = CInt(numTargetLine.Value)

        Try
            'target line
            If Report_BarChart.Visible = True Then Report_BarChart.target(numTargetLine.Value, btnTargetColor.BackColor.ToArgb)

            If cmbFirstDayWeek.SelectedIndex = -1 Then
                cmbFirstDayWeek.SelectedIndex = 1
            End If

            If cmbLastDayWeek.SelectedIndex = -1 Then
                cmbLastDayWeek.SelectedIndex = 5
            End If

            If cmbBeginOfYear.SelectedIndex = -1 Then
                cmbBeginOfYear.SelectedIndex = 0
            End If


            'If RestartServer = True Then
            '    restartEnetServer()
            'End If

            'Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Setupdt_.csys", False)
            '    writer.WriteLine(cmbFirstDayWeek.SelectedIndex & cmbLastDayWeek.SelectedIndex)
            '    writer.WriteLine(cmbBeginOfYear.SelectedIndex)
            '    writer.Close()
            'End Using

            Reporting_application.week_ = cmbFirstDayWeek.SelectedIndex & cmbLastDayWeek.SelectedIndex
            Reporting_application.year_ = cmbBeginOfYear.SelectedIndex
            '   Call auto()
        Catch ex As Exception
            MessageBox.Show("Error in configuration") 'ex.Message)
        End Try

        'Try
        '    If Exists(rootPath & "\sys\target_.csys") Then
        '        System.IO.File.Delete(rootPath & "\sys\target_.csys")
        '        Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\target_.csys")
        '            writer.Write(numTargetLine.Value)
        '            writer.Close()
        '        End Using
        '    Else
        '        Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\target_.csys")
        '            writer.Write(numTargetLine.Value)
        '            writer.Close()
        '        End Using
        '    End If
        '    Report_BarChart.target_ = CInt(numTargetLine.Value)
        'Catch ex As Exception
        '    MessageBox.Show("Unable to save the target value") ' & ex.Message, vbSystemModal + vbCritical)
        '    CSI_Lib.LogClientError("unable to save target value:" + ex.Message)
        'End Try



        'Try
        '    Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\AutoScroll_.csys")
        '        writer.Write(CHKB_AutoScroll.Checked.ToString())
        '        writer.Close()
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show("Unable to save autoscroll setting ") ' & ex.Message)
        '    CSI_Lib.LogClientError("Unable to save autoscroll setting : " & ex.Message)
        'End Try




        If Config_report.Visible = False Then
            Config_report.Show()
            'Else
            ' Config_report.Close()
            ' Config_report.Show()
        End If

        'If Reporting_application.bySetup = False Or (old_db_path <> txtIPDatabase.Text) Then

        '    Try
        '        If Reporting_application.BGW_SQLiteUpdate.IsBusy <> True Then
        '            ' Start the asynchronous operation.
        '            Reporting_application.BGW_SQLiteUpdate.RunWorkerAsync()
        '        End If

        '        SynchroDB.Show()
        '    Catch ext As Exception
        '        MessageBox.Show("Error running update") 'ext.Message)
        '    End Try
        'End If


        'Try
        '    Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\firstdate_.csys.csys")
        '        writer.Write(dtpFirstDateParts.Value)
        '        writer.Close()
        '        Reporting_application.First_date = dtpFirstDateParts.Value
        '    End Using
        'Catch ex As Exception
        '    CSI_Lib.LogClientError("unable to save the first date: " + ex.Message)
        'End Try


        'Try

        '    Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Networkenet_.csys", False)
        '        'writer.Write(Me.TB_IpAdress.Text + ":" + Me.TB_Port.Text)
        '        writer.Write(Me.TB_IpAdress.Text)
        '        writer.Close()
        '    End Using
        '    Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\Network_.csys", False)
        '        writer.Write("127.0.0.1:1001")
        '        writer.Close()
        '    End Using

        'Catch ext As Exception
        '    CSI_Lib.LogClientError("unable to save the ip:" + ext.Message)
        'End Try

        'CSIFLEXSettings.Instance.Reload()

        ' MsgBox("RM_preinstall call")
        'RM_preinstall()
        ' MsgBox("RM_preinstall end")

        Me.Close()
    End Sub


    'Private Sub CHKB_AutoScroll_CheckedChanged(sender As Object, e As EventArgs) Handles CHKB_AutoScroll.CheckedChanged

    '    Try
    '        Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\AutoScroll_.csys")
    '            writer.Write(CHKB_AutoScroll.Checked.ToString())
    '            writer.Close()
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show("Unable to save autoscroll setting ") ' & ex.Message)
    '        CSI_Lib.LogClientError("Unable to save autoscroll setting : " & ex.Message)
    '    End Try
    'End Sub

    'Private Sub CHKB_UseCSIDahboard_CheckedChanged(sender As Object, e As EventArgs) Handles CHKB_UseCSIDahboard.CheckedChanged

    '    Try
    '        If CHKB_UseCSIDahboard.Checked = True Then


    '            Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\UseCSIDashboard_.csys")
    '                writer.Write(CHKB_UseCSIDahboard.Checked.ToString())
    '                writer.Close()
    '            End Using

    '            'Add IP to device tbl
    '            'Dim ip As String = GetLocalIPAddress()
    '            If (CHKB_UseCSIDahboard.Checked) Then
    '                'Dim add_List As String() = Dns.GetHostByName(Dns.GetHostName()).AddressList
    '                Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)

    '                Dim ok___ As Boolean = Check_if_ip_present(h.AddressList)
    '                If ok___ = False Then MessageBox.Show("Please add your ip to the device list in CSI Flex Server", "Add your device", MessageBoxButton.OK, MessageBoxImage.Information)
    '            End If
    '        Else
    '            File.Delete(rootPath & "\sys\UseCSIDashboard_.csys")
    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show("Unable to save csi dashboard setting") ' & ex.Message)
    '        CSI_Lib.LogClientError("Unable to save csi dashboard setting :   " & ex.Message)
    '    End Try
    'End Sub


    Public Function Check_if_ip_present(ip__ As IPAddress()) As Boolean
        'create connection

        Try

            Dim db_authPath As String = Nothing

            If (File.Exists(rootPath + "\sys\SrvDBpath.csys")) Then
                Using reader As New StreamReader(rootPath + "\sys\SrvDBpath.csys")
                    db_authPath = reader.ReadLine()
                End Using
            End If

            'Dim strConn = $"SERVER={ db_authPath };DATABASE=csi_database;{CSI_Library.CSI_Library.MySqlServerBaseString}"
            'Dim dr = MySqlAccess.GetDataTable("SELECT IP_adress FROM tbl_devices", strConn)

            'While (dr.Read)
            '    For Each ipheal As System.Net.IPAddress In ip__
            '        If dr.GetString(0) = ipheal.ToString() Then Return True
            '    Next
            'End While


            If Not (db_authPath = Nothing) Then
                Dim server = db_authPath
                'Dim database = "csi_auth"
                'Dim uid = "client"
                'Dim password = "csiflex123"
                'Dim port = "3306"
                Dim connectionString As String

                connectionString = "SERVER=" + server + ";" + "DATABASE=csi_database;" + CSI_Library.CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"
                Dim mysqlcnt As MySqlConnection = New MySqlConnection(connectionString)


                With mysqlcnt
                    .Open()
                    If .State = ConnectionState.Open Then

                        Dim cmd As MySqlCommand = New MySqlCommand("SELECT IP_adress FROM tbl_devices ", mysqlcnt)
                        'create data reader
                        Dim rdr As MySqlDataReader = cmd.ExecuteReader
                        'loop through result set
                        While (rdr.Read)
                            For Each ipheal As System.Net.IPAddress In ip__
                                If rdr.GetString(0) = ipheal.ToString() Then Return True
                            Next
                        End While
                        'close data reader
                        rdr.Close()
                        ' Close connection
                        .Close()
                    End If
                End With
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to connect to the database")

            Return False
        End Try
        ' GetFieldValue = Nothing

        Return False
    End Function


    Public Function GetLocalIPAddress() As String
        Dim host = Dns.GetHostEntry(Dns.GetHostName())
        For Each x In host.AddressList
            If x.AddressFamily = AddressFamily.InterNetwork Then
                Return x.ToString()
            End If
        Next
        Throw New Exception("Local IP Address Not Found!")
    End Function


    Private Sub BTN_Rainmeter_Click(sender As Object, e As EventArgs) Handles BTN_Rainmeter.Click
        If Reporting_application.SRV_UDT_NEEDED = True Then
            MsgBox("You have to update the CSIFlex server to use rainmeter")
            GoTo end__
        End If

        Dim frm_RMConfig As New RMConfig
        frm_RMConfig.ShowDialog()
end__:
    End Sub


    Private Sub UseEnetColors_checkbox_CheckedChanged(sender As Object, e As EventArgs) Handles cboxUseEnetColors.CheckedChanged
        If cboxUseEnetColors.Checked = True Then
            Color_Path.Enabled = False
            Color_Path.Text = ""
            BTN_BrowseColor.Enabled = False
        Else
            Color_Path.Enabled = True
            BTN_BrowseColor.Enabled = True
        End If
    End Sub


    Private Sub BTN_Import_csv_Click(sender As Object, e As EventArgs)

        ' CSI_Lib.UpdateDB_SQLite_previousyears("2015")

        'Dim folderDlg As New OpenFileDialog
        ''Dim notImport As Boolean = True
        'Try
        '    If (folderDlg.ShowDialog()) Then

        '        If Not (folderDlg.FileName.Substring(folderDlg.FileName.Length - 4, 4) = ".CSV") Then
        '            MessageBox.Show("You must select a .CSV file")
        '        Else
        '            'Dim mySqlCnt As New MySqlConnection

        '            'mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)

        '            'mySqlCnt.Open()

        '            'Dim mysql As String = "SELECT DISTINCT TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME IN ('year_') AND TABLE_SCHEMA='csi_database';"
        '            'Dim cmdCreateDeviceTable As New MySqlCommand(mysql, mySqlCnt)
        '            'Dim mysqlReader As MySqlDataReader = cmdCreateDeviceTable.ExecuteReader
        '            'Dim dTable_year As New DataTable()
        '            'dTable_year.Load(mysqlReader)

        '            'Try
        '            '    For Each y As DataRow In dTable_year.Rows
        '            '        Dim mysql1 As String = "SELECT year_ FROM csi_database." + y.Item(0).ToString() + ";"
        '            '        Dim cmdCreateDeviceTable1 As New MySqlCommand(mysql1, mySqlCnt)
        '            '        Dim mysqlReader1 As MySqlDataReader = cmdCreateDeviceTable1.ExecuteReader
        '            '        Dim dTable_year1 As New DataTable()
        '            '        dTable_year1.Load(mysqlReader1)
        '            '        For Each r As DataRow In dTable_year1.Rows
        '            '            If folderDlg.FileName.Substring(folderDlg.FileName.Length - 8, 4) = r.Item(0).ToString() Then
        '            '                MessageBox.Show("This file have already been imported in your databse")
        '            '                notImport = False
        '            '                Exit For
        '            '            End If
        '            '        Next
        '            '        If notImport = False Then
        '            '            Exit For
        '            '        End If

        '            '    Next
        '            'Catch ex As Exception

        '            'End Try

        '            'If (notImport) Then
        '            'syncDB = New SynchroDB()
        '            'syncDB.Show()
        '            'Dim dbloadingthread As New Thread(AddressOf dbloading)
        '            'dbloadingthread.Name = "dbloading"
        '            'dbloadingthread.Start()
        '            'dbloadingthread.Priority = ThreadPriority.Normal
        '            'CSI_Library.CSI_Library.import = True
        '            'CSI_Library.CSI_Library.ImportPath = folderDlg.FileName
        '            BW_import_csv.RunWorkerAsync(folderDlg.FileName)
        '            'End If
        '        End If
        '    End If



        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try




    End Sub


    Private Sub BW_import_csv_DoWork(sender As Object, e As DoWorkEventArgs) Handles BW_import_csv.DoWork
        Dim importpath As String = e.Argument
        ' CSI_Lib.ImportDB_Mysql(importpath)
        'CSI_Lib.ImportDB_sqlite(importpath)

    End Sub


    Private Sub BtnOthersColor_click(sender As Object, e As EventArgs) Handles btnOthersColor.Click

        Dim colordial As New ColorDialog

        colordial.ShowDialog()

        CSIFLEXSettings.Instance.OtherColor = colordial.Color.ToArgb()
        CSIFLEXSettings.Instance.SaveSettings()

        btnOthersColor.BackColor = Color.FromArgb(colordial.Color.ToArgb)

        'Try
        '    If Exists(rootPath & "\sys\otherColor_.csys") Then System.IO.File.Delete(rootPath & "\sys\otherColor_.csys")

        '    Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\otherColor_.csys")
        '        writer.Write(colordial.Color.ToArgb)
        '        writer.Close()
        '    End Using

        '    btnOthersColor.BackColor = Color.FromArgb(colordial.Color.ToArgb)
        'Catch ex As Exception
        '    MessageBox.Show("Unable to save the aggregated status color ") ' & ex.Message, vbSystemModal + vbCritical)
        '    CSI_Lib.LogClientError("Unable to save the agregated status color  :" + ex.Message)
        'End Try

        If Report_BarChart.Visible = True Then Report_BarChart.Refresh()

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)
        MsgBox(CSI_Lib.Get_firt_date_database(2))
    End Sub

End Class


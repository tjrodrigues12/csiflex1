Imports System.IO
Imports System.IO.Compression
Imports System.Windows
Imports Microsoft.Reporting.WinForms
Imports System.Globalization
Imports CSI_Library
Imports System.Runtime.CompilerServices
Imports System.Data.SQLite
Imports MySql.Data.MySqlClient
Imports CSI_Library.CSI_DATA


Public Class Reporting_availability

    Private license As Integer
    Private active_machines() As String
    Private TypeDePeriode As String
    Private Qry_Tbl_MachineName As String
    Private Qry_Tbl_DataMachine As String
    Public CSI_Lib As CSI_Library.CSI_Library
    Public Shared cs As String = CSI_Library.CSI_Library.sqlitedbpath
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath


    Private Sub SetupForm_Reporting_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'dd MM yyyy HH mm ss
        'DTP_Start.CustomFormat = "dd-MM-yyyy HH:mm"
        'DTP_ReportDate.CustomFormat = "dd-MM-yyyy HH:mm"

        If My.Computer.FileSystem.FileExists(rootPath & "\sys\" + "Reporting_availability_vb") Then
            Reporting_application.Deserialise(Me, rootPath & "\sys\" + "Reporting_availability_vb")
        End If

        'Pirelli Pzero Nero All season P245/45 ZR18 96W

        Dim file As System.IO.StreamReader
        Dim imglst As New ImageList
        CSI_Lib = New CSI_Library.CSI_Library
        license = CSI_Lib.CheckLic(2)
        Dim list2 As New List(Of String)

        Dim mt_present As Boolean = False
        Dim st_present As Boolean = False

        Dim prefixTemp As String ' temp string, multiple use

        Dim machine() As String ' File -> read line -> machine()

        TreeView_machine.Nodes.Clear()


        If Not My.Computer.FileSystem.FileExists(rootPath & "\sys\MonList.csys") Then
            MessageBox.Show("The file 'Monlist.sys' is missing")
        Else

            file = My.Computer.FileSystem.OpenTextFileReader(rootPath & "\sys\MonList.csys")
            Dim i As Integer = 0
            Dim j As Integer = -1
            Dim k As Integer = -1
            Dim cpt As Integer = 0
            While Not file.EndOfStream

                ReDim Preserve machine(i + 1)
                machine(i) = file.ReadLine()

                prefixTemp = Strings.Left(machine(i), 3)

                Dim node As New TreeNode

                If Strings.Len(machine(i)) > 4 Then
                    node.ImageKey = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
                    node.Text = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
                Else
                    node.ImageKey = machine(i)
                    node.Text = machine(i)
                End If

                Select Case prefixTemp
                    Case "_MT"
                        mt_present = True
                        st_present = False

                        list2.Add(machine(i))
                        TreeView_machine.Nodes.Add(node)
                        cpt = -1
                        j = j + 1
                    Case "_ST"
                        st_present = True
                        cpt += 1
                        If mt_present = False Then
                            TreeView_machine.Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
                            'TreeView_machine.Nodes.Add(node)
                        Else
                            TreeView_machine.Nodes(j).Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
                            'TreeView_machine.Nodes(j).Nodes.Add(node)
                        End If

                        k += 1

                    Case Else
                        If (prefixTemp <> "_ST") And (prefixTemp <> "_MT") Then
                            If st_present = False And mt_present = True Then
                                TreeView_machine.Nodes(j).Nodes.Add(machine(i), machine(i))

                                cpt += 1

                            ElseIf mt_present = False And st_present = True Then

                                TreeView_machine.Nodes(k).Nodes.Add(machine(i), machine(i))



                            ElseIf mt_present = False And st_present = False Then

                                TreeView_machine.Nodes.Add(machine(i), machine(i))


                            Else

                                TreeView_machine.Nodes(j).Nodes(cpt).Nodes.Add(machine(i), machine(i))


                            End If

                        End If
                End Select

                i = i + 1
            End While
        End If


    End Sub

    Private Sub BTN_BrowseFolder_Click(sender As Object, e As EventArgs) Handles BTN_BrowseFolder.Click
        Dim folderDlg As New FolderBrowserDialog

        folderDlg.ShowNewFolderButton = False
        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            tb_pathReport.Text = folderDlg.SelectedPath
            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
        End If


    End Sub

    Public Function read_tree() As String()
        ' checked_in_TreeView_machine.Clear()

        ReDim active_machines(0)

        Dim aNode As TreeNode

        For Each aNode In TreeView_machine.Nodes
            PrintRecursive(aNode)
        Next
        ' Return checked_in_TreeView_machine
        ReDim Preserve active_machines(UBound(active_machines) - 1)
        Return active_machines

    End Function


    Private Sub PrintRecursive(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            '   checked_in_TreeView_machine.Add(n.Text)
            If n.Name <> "" Then
                active_machines(UBound(active_machines)) = n.Name
                ReDim Preserve active_machines(UBound(active_machines) + 1)
            End If
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursive(aNode)
        Next
    End Sub


    Sub generateSqlQuery()

        Dim ListOfMachineSelected As String() = read_tree()
        Dim i As Integer = 0

        Qry_Tbl_MachineName = ""
        Qry_Tbl_DataMachine = ""

        For Each machine In ListOfMachineSelected
            If (machine <> "") Then
                machine = CSI_Lib.RenameMachine(machine)

                Qry_Tbl_MachineName += "SELECT 'tbl_" + machine + "' AS MchName, status FROM tbl_" + machine + " "
                Qry_Tbl_DataMachine += "SELECT 'tbl_" + machine + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, iif(status = '_COFF', cycletime,0) as COFF, "
                Qry_Tbl_DataMachine += " iif(status = '_CON', cycletime,0) as CON, iif(status = '_SETUP', cycletime,0) as SETUP, iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP', cycletime,0) as OTHER  from tbl_" + machine + " "

                If Not i = (ListOfMachineSelected.Length - 1) Then

                    Qry_Tbl_MachineName += " union "
                    Qry_Tbl_DataMachine += " union "

                End If
                i += 1
            End If
        Next



    End Sub

    Private Sub setControl(enable As Boolean)
        'DTP_Start.Enabled = enable
        DTP_ReportDate.Enabled = enable
        tb_pathReport.Enabled = enable
        TreeView_machine.Enabled = enable
        GB_ReportType.Enabled = enable
        BTN_SetDefaultPath.Enabled = enable
        BTN_BrowseFolder.Enabled = enable
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



    Private Sub BTN_GenerateReport_Click(sender As Object, e As EventArgs) Handles BTN_GenerateReport.Click
        Dim ok As Boolean = False
        Dim ok2 As Boolean = False
        If Not (RB_Daily.Checked = False And RB_Weekly.Checked = False And RB_Monthly.Checked = False) Then
            If read_tree().Length <> 0 Then
                ok = True
            End If
        End If

        'For i = 0 To TreeView_machine.Nodes.Count()
        '    For j = 0 To TreeView_machine.Nodes.Item(i).Nodes.Item(i).Nodes.Count
        '        Dim tvn As TreeNode = TreeView_machine.Nodes.Item(i).Nodes.Item(j)
        '        If (tvn.Checked = True) Then
        '            ok2 = True
        '        End If
        '    Next
        '    i = i + 1
        'Next

        If (ok = True) Then
            BTN_GenerateReport.Enabled = False
            setControl(False)
            PB_loading.Visible = True
            BackgroundWorker.RunWorkerAsync()
        Else
            MessageBox.Show("You must select a a period of time and a machine")
        End If

    End Sub


    Public Sub generateReportXLS()

    End Sub


    Public Sub generateReportPDF()

        Dim machineList As String() = read_tree()
        Dim SEvent(0 To read_tree().Count() - 1, 1) As String
        For i = 0 To read_tree().Count() - 1
            SEvent(i, 1) = machineList(i)
            SEvent(i, 1) = "SETUP"
        Next

        If Not (tb_pathReport.Text = "") Then

            If (RB_Weekly.Checked) Then
                'CSI_Lib.generateReport(read_tree(), "Weekly", DTP_Start.Value, DTP_End.Value, tb_pathReport.Text, SEvent, True)
                Dim tempsd As DateTime = DTP_ReportDate.Value.AddDays(-6)
                Dim startdate As New DateTime(tempsd.Year, tempsd.Month, tempsd.Day, 0, 0, 0)
                Dim enddate As New DateTime(DTP_ReportDate.Value.Year, DTP_ReportDate.Value.Month, DTP_ReportDate.Value.Day, 23, 59, 59)
                CSI_Lib.generateReport(read_tree(), "Weekly", startdate, enddate, tb_pathReport.Text, SEvent, True)
            End If

            If (RB_Monthly.Checked) Then
                Dim tempsd As DateTime = DTP_ReportDate.Value.AddMonths(-1)
                Dim startdate As New DateTime(tempsd.Year, tempsd.Month, tempsd.Day, 0, 0, 0)
                Dim enddate As New DateTime(DTP_ReportDate.Value.Year, DTP_ReportDate.Value.Month, DTP_ReportDate.Value.Day, 23, 59, 59)
                CSI_Lib.generateReport(read_tree(), "Monthly", startdate, enddate, tb_pathReport.Text, SEvent, True)
            End If

            If (RB_Daily.Checked) Then
                Dim startdate As New DateTime(DTP_ReportDate.Value.Year, DTP_ReportDate.Value.Month, DTP_ReportDate.Value.Day, 0, 0, 0)
                Dim enddate As New DateTime(DTP_ReportDate.Value.Year, DTP_ReportDate.Value.Month, DTP_ReportDate.Value.Day, 23, 59, 59)
                CSI_Lib.generateReport(read_tree(), "Daily", startdate, enddate, tb_pathReport.Text, SEvent, True)
            End If

            Dim result As DialogResult = MessageBox.Show("Your report(s) are ready. Do you want to open the folder?", "Report(s) completed", MessageBoxButton.YesNo, MessageBoxImage.Asterisk)
            If (result = Forms.DialogResult.Yes) Then
                Process.Start(tb_pathReport.Text)
            End If
        Else
            MessageBox.Show("Please select a folder")

        End If

    End Sub

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

    Public Function toFirst(datee As Date) As DateTime
        Return DateAdd("m", DatePart("m", datee), DateSerial(Year(datee), 1, 1))
    End Function

    Public Function IntToMonday(integ As Integer, param As DateTime) As DateTime

        Dim ret As DateTime

        ret = DateAdd("ww", integ - 1, DateSerial(Year(param), 1, 1))

        Return toMonday(ret)

    End Function

    Public Function IntToFirst(integ As Integer, param As DateTime) As DateTime

        Dim ret As DateTime = New DateTime(Year(param), integ, 1)
        Return ret

    End Function


    Private Sub SetupForm_Reporting_MaximizedBoundsChanged(sender As Object, e As EventArgs) Handles Me.MaximizedBoundsChanged

    End Sub


    Private Sub frm_Closing(ByVal sender As Object, _
  ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub TreeView_machine_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeView_machine.AfterCheck
        RemoveHandler TreeView_machine.AfterCheck, AddressOf TreeView_machine_AfterCheck

        Call CheckAllChildNodes(e.Node)

        Dim machines As String() = read_tree()


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

        AddHandler TreeView_machine.AfterCheck, AddressOf TreeView_machine_AfterCheck
    End Sub

    Private Sub IsEveryChildChecked(ByVal parentNode As TreeNode, ByRef checkValue As Boolean)
        For Each node As TreeNode In parentNode.Nodes
            Call IsEveryChildChecked(node, checkValue)
            If Not node.Checked Then
                checkValue = False
            End If
        Next
    End Sub

    Private Sub CheckAllChildNodes(ByVal parentNode As TreeNode)
        For Each childNode As TreeNode In parentNode.Nodes
            childNode.Checked = parentNode.Checked
            CheckAllChildNodes(childNode)
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




    'Private Sub Backc() 'etandWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker.DoWork
    '    Try
    '        generateReportPDF()
    '    Catch ex As Exception
    '        'MessageBox.Show(CSI_Lib.TraceMessage("Erreur: " + ex.Message() + "\n" + ex.Source))
    '        CSI_Lib.LogClientError(ex.Message)
    '        MessageBox.Show("Unable to generate report. Make sure your computer has ReportViewer 2015 runtime")
    '    End Try

    'End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted

        PB_loading.Visible = False
        BTN_GenerateReport.Enabled = True
        setControl(True)
    End Sub

    Private Sub BTN_SetDefaultPath_Click(sender As Object, e As EventArgs) Handles BTN_SetDefaultPath.Click

        If File.Exists(rootPath & "\sys\defaultReportFolder_.csys") Then
            Using reader As StreamReader = New StreamReader(rootPath & "\sys\defaultReportFolder_.csys")
                tb_pathReport.Clear()
                tb_pathReport.Text = reader.ReadLine()
                reader.Close()
            End Using
        Else
            MessageBox.Show("Please specify a default folder in the ""Setup"" form")
        End If

        If tb_pathReport.Text = "" Then
            MessageBox.Show("Please specify a default folder in the ""Setup"" form")
        End If

    End Sub



    Private Sub Reporting_availability_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Reporting_application.Serialise(Me, rootPath & "\sys\" + "Reporting_availability_vb")
    End Sub

    Private Sub CB_Period_CheckedChanged(sender As Object, e As EventArgs)


        'DTP_Start.Visible = True
        'DTP_Start.Enabled = True
        DTP_ReportDate.Visible = True
        DTP_ReportDate.Enabled = True
        GB_ReportType.Enabled = False
        GB_ReportType.Visible = False
        TreeView_machine.Height = 284
        TreeView_machine.Location = New System.Drawing.Point(TreeView_machine.Location.X, 87)
        RB_Daily.Enabled = True
        RB_Weekly.Enabled = True
        RB_Monthly.Enabled = True

    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker.DoWork
        Try
            generateReportPDF()
        Catch ex As Exception
            'MessageBox.Show(CSI_Lib.TraceMessage("Erreur: " + ex.Message() + "\n" + ex.Source))
            CSI_Lib.LogClientError(ex.Message)
            MessageBox.Show("Unable to generate report. Make sure your computer has ReportViewer 2015 runtime")
        End Try
    End Sub
End Class
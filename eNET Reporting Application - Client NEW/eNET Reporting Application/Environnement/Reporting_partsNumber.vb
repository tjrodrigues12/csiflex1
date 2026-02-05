Imports System.IO
Imports System.IO.Compression
Imports System.Windows
Imports System.Data.OleDb
Imports System.Globalization
Imports CSI_Library
Imports System.Runtime.CompilerServices

Imports Microsoft.Office.Interop
Imports System.Text
Imports TreeViewSerialization
Imports System.Data.SQLite

Imports MySql.Data.MySqlClient







Public Class Reporting_partsNumber

    Private active_machines() As String
    Private TypeDePeriode As String
    Private Qry_Tbl_MachineName As String
    Private Qry_Tbl_DataMachine As String
    Public CSI_Lib As CSI_Library.CSI_Library
    Public Shared cs As String = CSI_Library.CSI_Library.sqlitedbpath
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath


    Private Sub setControl(enable As Boolean)

        DTP_EndDate.Enabled = enable
        DTP_StartDate.Enabled = enable
        tb_pathReport.Enabled = enable
        TreeView_machine.Enabled = enable
        CBX_Format.Enabled = enable
        BTN_SetDefaultPath.Enabled = enable
        BTN_BrowseFolder.Enabled = enable

    End Sub


    Private Sub BTN_GenerateReport_Click(sender As Object, e As EventArgs) Handles BTN_GenerateReport.Click
        'generateReport()

        If My.Computer.FileSystem.DirectoryExists(tb_pathReport.Text) Then

            setControl(False)
            If CBX_Format.SelectedIndex = 0 Then
                generateExcelFile()
            Else
                generateCSV_file()
            End If
            setControl(True)

        ElseIf tb_pathReport.Text = "" Then
            MessageBox.Show("Please specify an export folder")

        Else
            MessageBox.Show("Please specify a valid export folder")
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

        ' If tb_pathReport.Text = "" Then
        'MessageBox.Show("Please specify a default folder in the ""Setup"" form")
        ' End If
    End Sub


    Private Sub saveTreeV()
        Dim serializer As TreeViewSerializer = New TreeViewSerializer()
        serializer.SerializeTreeView(Me.TreeView_machine, rootPath & "\sys\" & "Reporting_partsNumber_vb_TreeView.xml")
    End Sub
    Private Sub loadTreeV()
        If My.Computer.FileSystem.FileExists(rootPath & "\sys\" & "Reporting_partsNumber_vb_TreeView.xml") Then
            Me.TreeView_machine.Nodes.Clear()
            Dim serializer As TreeViewSerializer = New TreeViewSerializer()
            serializer.DeserializeTreeView(Me.TreeView_machine, rootPath & "\sys\" & "Reporting_partsNumber_vb_TreeView.xml")
        End If
    End Sub


    Private Sub Reporting_partsNumber_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CBX_Format.SelectedIndex = 0

        If My.Computer.FileSystem.FileExists(rootPath & "\sys\" + "Reporting_partsNumber_vb") Then
            Reporting_application.Deserialise(Me, rootPath & "\sys\" + "Reporting_partsNumber_vb")

        End If

        TB_OutputName.Text = "PartsNumbers_Report_" + Now().ToString("yyyyMMdd")
        Dim file As System.IO.StreamReader
        Dim imglst As New ImageList
        CSI_Lib = New CSI_Library.CSI_Library

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

    'Private Sub ReloadReport(viewer As ReportViewer)

    '    If (viewer.LocalReport.DataSources.Count > 0) Then
    '        viewer.LocalReport.DataSources.RemoveAt(0)
    '    End If

    '    viewer.LocalReport.DataSources.Add(New ReportDataSource("DataSet_partsNo", setPartNo()))


    'End Sub

    Private Function setPartNo() As DataTable


        Try
            Dim machine() As String = read_tree()
            Dim realMachine() As String = read_tree()
            Dim command As String = ""
            Dim bigTable As New DataTable, temp_table As New DataTable
            bigTable.Columns.Add("MchName")
            bigTable.Columns.Add("date")
            bigTable.Columns.Add("partName")
            bigTable.Columns.Add("count")

            Dim startdate As New DateTime(DTP_StartDate.Value.Year, DTP_StartDate.Value.Month, DTP_StartDate.Value.Day, 0, 0, 0)
            Dim enddate As New DateTime(DTP_EndDate.Value.Year, DTP_EndDate.Value.Month, DTP_EndDate.Value.Day, 23, 59, 59)

            If Not CSI_Lib.CheckLic(2) = 3 Then 'If CSI_Lib.isClientSQlite Then

                Dim reader As SQLiteDataReader
                Dim tmp_table_cmd As New SQLiteCommand

                Using sqlConn As SQLiteConnection = New SQLiteConnection(cs)
                    sqlConn.Open()
                    If machine.Length > 0 Then
                        For i = 0 To UBound(machine)
                            temp_table.Clear()
                            machine(i) = CSI_Lib.RenameMachine(machine(i))
                            Dim cmd As New SQLiteCommand("CREATE TABLE if not exists tbl_" & machine(i) & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, UNIQUE (time_,status))", sqlConn)
                            cmd.ExecuteNonQuery()

                            command = "SELECT '" + realMachine(i) + "' as MchName, strftime('%Y-%m-%d', time_) as date, substr(Status, 9)  as partName, count(Status) as 'count' " +
                            " FROM [tbl_" & machine(i) & "]  " +
                            " WHERE [Status] LIKE '_PARTNO:%' and time_  between '" + startdate.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + enddate.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'  " +
                            "    group by Status,strftime('%Y-%m-%d', time_) "

                            tmp_table_cmd.CommandText = command
                            tmp_table_cmd.Connection = sqlConn

                            reader = tmp_table_cmd.ExecuteReader
                            temp_table.Load(reader)
                            'bigTable.Merge(temp_table)
                            Try
                                'bigTable.Merge(temp_table)
                                For Each row As DataRow In temp_table.Rows
                                    Dim trow = bigTable.NewRow()
                                    trow("MchName") = row("MchName")
                                    trow("date") = row("date")
                                    trow("partName") = row("partName")
                                    trow("count") = row("count")
                                    bigTable.Rows.Add(trow)
                                Next
                            Catch ex As Exception
                                CSI_Lib.LogServerError("unable to merge parts table:" + ex.Message, 1)
                            End Try
                        Next
                    End If

                    Dim ds As ReportingDataset = New ReportingDataset()

                    ' ds.tbl_partsNumber.Merge(bigTable)
                    Return bigTable
                End Using

            Else
                Dim reader As MySqlDataReader
                Dim tmp_table_cmd As New MySqlCommand

                Dim db_authPath As String = Nothing
                If (File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
                    Using streader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")
                        db_authPath = streader.ReadLine()
                    End Using
                End If
                Dim connectionString As String
                connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + CSI_Library.CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"


                Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)
                    sqlConn.Open()
                    If machine.Length > 0 Then
                        For i = 0 To UBound(machine)

                            temp_table = New DataTable

                            machine(i) = CSI_Lib.RenameMachine(machine(i))

                            command = "SELECT '" + realMachine(i) + "' as MchName, DATE_FORMAT(time_, '%m-%d-%Y') as 'date', Mid(Status, 9)  as partName, count(Status) as 'count' " +
                                " FROM  tbl_" & machine(i) & "  " +
                                " WHERE Status LIKE '_PARTNO:%' and time_  between '" + startdate.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "' and '" + enddate.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US")) + "'  " +
                                "    group by Status, DATE_FORMAT(time_, '%m-%d-%Y') "

                            tmp_table_cmd.CommandText = command
                            tmp_table_cmd.Connection = sqlConn

                            reader = tmp_table_cmd.ExecuteReader
                            temp_table.Load(reader)
                            Try
                                'bigTable.Merge(temp_table)
                                For Each row As DataRow In temp_table.Rows
                                    Dim trow = bigTable.NewRow()
                                    trow("MchName") = row("MchName")
                                    trow("date") = row("date")
                                    trow("partName") = row("partName")
                                    trow("count") = row("count")
                                    bigTable.Rows.Add(trow)
                                Next
                            Catch ex As Exception
                                CSI_Lib.LogServerError("unable to merge parts table:" + ex.Message, 1)
                            End Try

                        Next
                    End If

                    Dim ds As ReportingDataset = New ReportingDataset()

                    ' ds.tbl_partsNumber.Merge(bigTable)
                    Return bigTable
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error while retrieving parts informations")
            CSI_Lib.LogServerError("unable to get part informations:" + ex.Message, 1)
        End Try

    End Function



    Private Sub generateCSV_file()

        Try
            Dim table As DataTable = setPartNo()
            Dim bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(ToCSV(table))
            Dim streamy As New MemoryStream(bytes)

            Dim reader As New StreamReader(streamy)
            ' Console.WriteLine(reader.ReadToEnd())


            Dim fs As FileStream

            fs = New FileStream(tb_pathReport.Text + "\" + TB_OutputName.Text + "." + CBX_Format.SelectedItem, FileMode.Create)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()


            Dim result As DialogResult = MessageBox.Show("Your part report is ready. Do you want to open the folder?", "Report completed", MessageBoxButton.YesNo, MessageBoxImage.Asterisk)
            If (result = Forms.DialogResult.Yes) Then
                Process.Start(tb_pathReport.Text)
            End If
        Catch ex As Exception
            MessageBox.Show("An error occured while creating your report. See log for details.")
            CSI_Lib.LogServerError("Unable to generate part report csv, MSG:" + ex.Message, 1)
        End Try

    End Sub






    Public Function datatableToArray(dt As DataTable) As Object(,)

        Dim arr As Object(,) = New Object(dt.Rows.Count - 1, dt.Columns.Count - 1) {}
        For r As Integer = 0 To dt.Rows.Count - 1
            Dim dr As DataRow = dt.Rows(r)
            For c As Integer = 0 To dt.Columns.Count - 1
                arr(r, c) = dr(c)
            Next
        Next

        Return arr

    End Function


    Public Shared Function ToCSV(table As DataTable) As String
        Dim result As New System.Text.StringBuilder()




        For i As Integer = 0 To table.Columns.Count - 1
            result.Append(table.Columns(i).ColumnName)
            result.Append(If(i = table.Columns.Count - 1, vbLf, ","))
        Next

        For Each row As DataRow In table.Rows
            For i As Integer = 0 To table.Columns.Count - 1
                result.Append(row(i).ToString())
                result.Append(If(i = table.Columns.Count - 1, vbLf, ","))
            Next
        Next

        Return result.ToString()
    End Function




    Private Sub generateExcelFile()
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value

        Try
            If Not (File.Exists((Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Template_partsReport_xml.xlsx"))) Then
                IO.File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Template_partsReport_xml.xlsx", My.Resources.Template_partsReport_xml)
            End If


            Dim templatePath As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Template_partsReport_xml.xlsx"
            Dim filepath As String = tb_pathReport.Text + "\" + TB_OutputName.Text + "." + CBX_Format.SelectedItem
            File.Copy(templatePath, filepath, True)

            Dim InsertCommand As OleDbCommand = New OleDbCommand()
            Dim sqlInsertQry As String = ""



            xlApp = New Excel.Application()
            Dim workbookPath As String = filepath
            xlWorkBook = xlApp.Workbooks.Open(workbookPath, 0, False, 5, misValue, misValue, True, Excel.XlPlatform.xlWindows, "", True, False, 0)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            Dim valuesTable As DataTable = setPartNo()

            Dim arr As Object(,) = datatableToArray(valuesTable)


            If (valuesTable.Rows.Count <> 0) Then
                xlWorkSheet.Range("A6").Resize(valuesTable.Rows.Count, valuesTable.Columns.Count).Value = arr
            End If

            xlWorkSheet.Range("C:C").NumberFormat = "@"




            xlWorkBook.RefreshAll()

            'xlWorkBook.SaveAs(TB_OutputName.Text, CBX_Format.SelectedItem, misValue, misValue, False, False, Excel.XlSaveAsAccessMode.xlNoChange)
            xlWorkBook.Save()
            xlWorkBook.Close()
            xlApp.Quit()

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)

            'Try
            '    If File.Exists(tb_pathReport.Text + "\" + TB_OutputName.Text + "." + CBX_Format.SelectedItem) Then
            '        File.Delete(tb_pathReport.Text + "\" + TB_OutputName.Text + "." + CBX_Format.SelectedItem)
            '    End If

            '    File.Copy(filePath, tb_pathReport.Text + "\" + TB_OutputName.Text + "." + CBX_Format.SelectedItem)
            '    File.Delete(filePath)
            'Catch ex As Exception

            '    Throw
            'End Try

            Dim result As DialogResult = MessageBox.Show("Your part report is ready. Do you want to open the folder?", "Report completed", MessageBoxButton.YesNo, MessageBoxImage.Asterisk)
            If (result = Forms.DialogResult.Yes) Then
                Process.Start(tb_pathReport.Text)
            End If

        Catch ex As Exception
            MessageBox.Show("Unable to save the report. Check if the file is already open.")
            CSI_Lib.LogServerError("Parts report error:" + ex.Message, 1)
            'MessageBox.Show(ex.ToString())

        End Try

    End Sub


    Private Sub BTN_Daily_Click(sender As Object, e As EventArgs) Handles BTN_Daily.Click
        DTP_EndDate.Value() = DTP_StartDate.Value
    End Sub


    Private Sub BTN_Weekly_Click(sender As Object, e As EventArgs) Handles BTN_Weekly.Click
        DTP_StartDate.Value = DTP_StartDate.Value.AddDays(Val(Reporting_application.week_(0)) - DTP_StartDate.Value.DayOfWeek)
        DTP_EndDate.Value = DTP_EndDate.Value.AddDays(Val(Reporting_application.week_(1)) - DTP_EndDate.Value.DayOfWeek)
    End Sub


    Private Sub BTN_Monthly_Click(sender As Object, e As EventArgs) Handles BTN_Monthly.Click
        DTP_StartDate.Value = New Date(DTP_StartDate.Value.Year, DTP_StartDate.Value.Month, 1)
        DTP_EndDate.Value = New Date(DTP_StartDate.Value.Year, DTP_StartDate.Value.Month, System.DateTime.DaysInMonth(DTP_StartDate.Value.Year, DTP_StartDate.Value.Month))
    End Sub


    Private Sub BTN_Yearly_Click(sender As Object, e As EventArgs) Handles BTN_Yearly.Click
        DTP_StartDate.Value = New Date(DTP_StartDate.Value.Year, 1, 1)
        DTP_EndDate.Value = New Date(DTP_StartDate.Value.Year, 12, 31)
    End Sub

    Private Sub BTN_SetDefaultPath_MouseEnter(sender As Object, e As EventArgs) Handles BTN_SetDefaultPath.MouseEnter

    End Sub

    Private Sub Reporting_partsNumber_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Reporting_application.Serialise(Me, rootPath & "\sys\" + "Reporting_partsNumber_vb")
        saveTreeV()
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




End Class
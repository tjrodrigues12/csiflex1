Imports System.IO
Imports System.IO.Compression
Imports System.Windows
Imports System.Globalization
Imports CSI_Library
Imports System.Runtime.CompilerServices
Imports System.Text
Imports MySql.Data.MySqlClient
Imports CSIFLEX.Database.Access

Public Class CSI_AutoReporting

    Private conn As MySqlConnection
    Private adap As MySqlDataAdapter
    Private ds As DataSet
    Private cmdBuild As MySqlCommandBuilder
    Private AddingBool As Boolean
    Private path As String ' = CSI_Lib.getRootPath()
    Private tasktochange As Integer = 0
    'Private taskname As String = ""
    Private modified As Boolean = False
    Private custommsg As String


    Private Sub CSI_AutoReporting_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CSI_Lib = New CSI_Library.CSI_Library
        path = CSI_Lib.getRootPath()

        BTN_Modify.Enabled = False

        createCSVforAutoReporting()
        loadCSVforAutoReporting()

        LockUnlockForm(False)

    End Sub


    Private Sub createCSVforAutoReporting()

        Try

            'MySqlAccess.Validate_Auth_AutoReportConfig_Table()

        Catch ex As Exception
            CSI_Lib.LogServerError("createCSVforAutoReporting Error related Mysql : " & ex.Message() & ex.StackTrace(), 1)
        Finally
            'mySqlCnt.Close()
        End Try

    End Sub

    Private Sub loadCSVforAutoReporting()


        ds = New DataSet()


        Dim tbl As New DataTable("tableDGV")

        tbl.Columns.Add("Task_name", GetType(String))
        tbl.Columns.Add("Day_", GetType(String))
        tbl.Columns.Add("Time_", GetType(String))
        tbl.Columns.Add("Report_Type", GetType(String))
        tbl.Columns.Add("Output_Folder", GetType(String))
        tbl.Columns.Add("MachineToReport", GetType(String))
        tbl.Columns.Add("MailTo", GetType(String))
        tbl.Columns.Add("Email_Time", GetType(String))

        For Each r As DataRow In DGV_tasks.Rows
            tbl.Rows.Add(DGV_tasks.Rows)
        Next

        ds.Tables.Add(tbl)


        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try
            mySqlCnt.Open()
            Dim filePath As String = "CSI_auth.Auto_Report_config"
            Dim QryStr As String = String.Format("SELECT  * FROM {0} ", filePath)
            adap = New MySqlDataAdapter(QryStr, mySqlCnt)
            adap.Fill(ds, "tableDGV")
            DGV_tasks.DataSource = ds.Tables(0)
        Catch ex As Exception
            CSI_Lib.LogServerError("loadCSVforAutoReporting Error related Mysql : " & ex.Message() & ex.StackTrace(), 1)
        Finally
            mySqlCnt.Close()
        End Try


    End Sub
    Private Sub ClearBoard()

        Dim week As String() = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Every day"}

        CB_DOW.SelectedItem = -1
        For Each Day As String In week

            'If If(DGV_tasks.Rows.Count = 0, False, DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("Day").Value.ToString().Contains(Day)) Then
            'Else
            'DGV_DayOfWeek.Rows.Add({"false", Day})
        Next

    End Sub

    Private Function GetCustomMsg(taskname As String) As String
        Dim res As String = String.Empty '"Your report was generated succesfully"
        Dim sql As String = ("select CustomMsg from CSI_auth.Auto_Report_config where Task_name = '" + taskname + "'")
        Dim dt As New DataTable
        Try
            Using connection As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                Try
                    connection.Open()
                    Using mysqladap As New MySqlDataAdapter(sql, connection)
                        mysqladap.Fill(dt)
                    End Using
                Catch ex As Exception
                    CSI_Lib.LogServerError("GetCustomMsg Error related Mysql : " & ex.Message() & ex.StackTrace(), 1)
                Finally
                    connection.Close()
                End Try
            End Using

            If (dt.Rows.Count > 0) Then
                res = dt.Rows(0)("CustomMsg").ToString()
            End If
        Catch ex As Exception
            CSI_Lib.LogServerError("Unable to retrieve custom message:" + ex.Message, 1)
        End Try

        Return res
    End Function

    Private Sub BTN_Add_Click(sender As Object, e As EventArgs) Handles BTN_Add.Click

        If AddingBool = False Then
            custommsg = String.Empty
            BTN_Modify.Enabled = False
            BTN_Remove.Enabled = False
            clearForm()
            'loadMachineGrid()
            'loadDayOfWeekGrid()
            LockUnlockForm(True)
            BTN_Add.Text = "Save"
            AddingBool = True
            'ClearBoard()
            TB_TaskName.Clear()
            TB_OutPutFolder.Clear()
            cb_d.Enabled = False
            cb_t.Enabled = False
            ClearGridDay()
            ClearGridMail()
            ClearGridMachine()
            CBX_ReportType.SelectedIndex = -1
            cb_d.SelectedIndex = -1
            CB_DOW.SelectedIndex = -1
            CB_DOW.Enabled = False
            CB_WKND.Enabled = False
            CB_WKND.Checked = False
        ElseIf (getListOfSelectedMachine() <> "" And (getListOfDaySelected() <> "" Or (CBX_ReportType.SelectedIndex = 0 Or CBX_ReportType.SelectedIndex = 2)) And TB_TaskName.Text.ToString() <> "" And TB_OutPutFolder.Text.ToString() <> "") And (cb_d.Text <> "" Or (CBX_ReportType.SelectedIndex = 0 Or CBX_ReportType.SelectedIndex = 2)) Then
            cb_d.Enabled = False
            cb_t.Enabled = False

            If IsNumeric(TB_TaskName.Text.ToString()) = False Then
                cmdBuild = New MySqlCommandBuilder(adap)

                'If (BTN_Modify.Text = "Modify") Then
                '    TB_TaskName.Text = taskname
                'End If

                'Dim custommsg = GetCustomMsg(TB_TaskName.Text)
                Dim filtered = ds.Tables(0).AsEnumerable().Where(Function(r) r.Field(Of [String])("Task_name").Equals(TB_TaskName.Text))

                If (modified) Then
                    DGV_tasks.Rows.RemoveAt(tasktochange)
                    ds.Tables(0).Rows.RemoveAt(tasktochange)
                    modified = False
                End If

                If (filtered.Count = 0) Then
                    Try
                        Dim newRowToDGV As DataRow = ds.Tables(0).NewRow()

                        newRowToDGV("Task_name") = TB_TaskName.Text
                        newRowToDGV("Day_") = getListOfDaySelected()
                        newRowToDGV("Time_") = DTP_Time.Text
                        newRowToDGV("Report_Type") = CBX_ReportType.Text
                        newRowToDGV("Output_Folder") = TB_OutPutFolder.Text
                        newRowToDGV("MachineToReport") = getListOfSelectedMachine()
                        newRowToDGV("MailTo") = getlistOfMailSelected()
                        newRowToDGV("dayback") = cb_d.Text
                        newRowToDGV("timeback") = "1900-01-01 " + cb_t.Text
                        newRowToDGV("CustomMsg") = custommsg
                        ds.Tables(0).Rows.Add(newRowToDGV)

                        'If ds.Tables(0).Rows.Count = 1 Then
                        '    Dim sql As String = ("delete from CSI_auth.Auto_Report_config")
                        '    Dim connection As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                        '    Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)

                        '    Try
                        '        connection.Open()
                        '        mysqlcomm.ExecuteNonQuery()
                        '        connection.Close()
                        '    Catch
                        '    End Try
                        'End If


                        adap.Update(ds, ds.Tables(0).TableName)
                        ReportsChanged()



                        BTN_Add.Text = "Add New"
                        AddingBool = False

                        LockUnlockForm(False)

                        ClearGridDay()
                        ClearGridMachine()

                        'loadMachineGrid()
                        'loadDayOfWeekGrid()

                        clearForm()


                        If Not (DGV_tasks.CurrentRow Is Nothing) And AddingBool = False Then
                            bindDGVtoForm()
                        End If
                        CB_WKND.Enabled = False
                    Catch ex As Exception
                        CSI_Lib.LogServerError("Unable to add report:" + ex.Message, 1)
                    End Try
                Else
                    MessageBox.Show("This task name is already in use")
                End If


            Else
                MessageBox.Show("Please do not enter numbers")
            End If

        Else
            MessageBox.Show("Error! Make sure you have completed all the steps below: " & vbNewLine & vbNewLine & "-Enter a task name" & vbNewLine & "-Enter an output destination" & vbNewLine & "-Select at least one machine" & vbNewLine & "-Select one day")
        End If


    End Sub

    Private Sub loadDayOfWeekGrid()

        Dim week As String() = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}


        CB_DOW.SelectedItem = -1


        For Each Day As String In week

            If If(DGV_tasks.Rows.Count = 0, False, DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("Day").Value.ToString().Contains(Day)) Then
                CB_DOW.SelectedItem = Day
                If CBX_ReportType.SelectedIndex = 0 Then
                    If Day = "Sunday" Then
                        CB_WKND.Checked = True
                        CB_DOW.SelectedIndex = -1
                    Else
                        CB_DOW.SelectedIndex = -1
                    End If
                Else
                    CB_WKND.Checked = False
                End If

            End If

        Next

    End Sub


    Private Sub clearForm()
        TB_TaskName.Text = ""
        TB_OutPutFolder.Text = ""
        CBX_ReportType.SelectedIndex = 0
    End Sub

    Private Sub LockUnlockForm(enabledOrNot As Boolean)

        If DGV_tasks.Rows.Count = 0 Then
            BTN_Remove.Enabled = False
        Else
            BTN_Remove.Enabled = Not enabledOrNot
        End If


        BTN_Cancel.Enabled = enabledOrNot
        TB_TaskName.Enabled = enabledOrNot
        'BTN_CustomEmail.Enabled = enabledOrNot
        TB_OutPutFolder.Enabled = enabledOrNot
        DTP_Time.Enabled = enabledOrNot
        Try
            DGV_tasks.SelectedCells.Item(0).Selected = Not enabledOrNot
        Catch ex As Exception

        End Try


        DGV_tasks.Enabled = Not enabledOrNot
        CB_DOW.Enabled = enabledOrNot
        DGV_MachineList.Enabled = enabledOrNot
        dgv_mail.Enabled = enabledOrNot
        CBX_ReportType.Enabled = enabledOrNot
        BTN_Output.Enabled = enabledOrNot



        If enabledOrNot = False Then
            DGV_MachineList.ForeColor = Color.Gray
            dgv_mail.ForeColor = Color.Gray
            DGV_tasks.ForeColor = Color.Black
        Else
            DGV_MachineList.ForeColor = Color.Black
            dgv_mail.ForeColor = Color.Black
            DGV_tasks.ForeColor = Color.Gray
        End If

    End Sub

    Sub ClearGridDay()
        Dim week As String() = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Every day"}

        CB_DOW.SelectedItem = -1

    End Sub

    Sub ClearGridMachine()

        Dim readed As String


        Using reader As StreamReader = New StreamReader(path & "\sys\machine_list_.csys")
            DGV_MachineList.Rows.Clear()
            While Not reader.EndOfStream
                readed = reader.ReadLine().Split(",")(0)
                If Not readed.Contains("__") And Not (readed.Trim = "") Then
                    DGV_MachineList.Rows.Add({"false", readed})
                    'If If(DGV_tasks.Rows.Count = 0, False, DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("MachineName").Value.ToString().Contains(readed)) Then
                    '    DGV_MachineList.Rows.Add({"true", readed})
                    'Else
                    '    DGV_MachineList.Rows.Add({"false", readed})
                    'End If
                End If
            End While
        End Using

    End Sub

    Sub ClearGridMail()

        Dim table_ As New DataTable

        table_.Columns.Add("email_", GetType(String))

        Dim MySqlcnt As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try
            MySqlcnt.Open()
            Dim command As New MySqlCommand("SELECT email_ FROM csi_auth.Users", MySqlcnt)
            Dim mysqlreader As MySqlDataReader = command.ExecuteReader
            table_.Load(mysqlreader)
        Catch ex As Exception
            CSI_Lib.LogServerError("Error of MYSQL in ClearGridMail funtion : " & ex.Message(), 1)
        Finally
            MySqlcnt.Close()
        End Try


        dgv_mail.Rows.Clear()

        Dim i As Integer = 0


        For Each row_ As DataRow In table_.Rows
            dgv_mail.Rows.Add({"false", row_("email_")})
        Next
        dgv_mail.PerformLayout()
        'Dim arr As List(Of String) = New List(Of String)
        'arr.Add("asom@moldmaster.com")
        'arr.Add("bpatel@moldmaster.com")
        'arr.Add("Brossm@moldmaster.com")
        'arr.Add("BChisolm@moldmaster.com")
        'arr.Add("Esomthingm@moldmaster.com")
        'arr.Add("Emiddm@moldmaster.com")
        'arr.Add("Krud@moldmaster.com")
        'arr.Add("M5pierce@moldmaster.com")
        'arr.Add("Mtaylor@moldmaster.com")
        'arr.Add("ammsolfm@moldmaster.com")
        'arr.Add("Mkhanl@moldmaster.com")
        'arr.Add("Ploi@moldmaster.com")
        'arr.Add("Phallerr@moldmaster.com")
        'arr.Add("Pharrris@moldmaster.com")
        'arr.Add("Rvangg@moldmaster.com")
        'arr.Add("Stpay@moldmaster.com")
        'arr.Add("Sssaavom@moldmaster.com")
        'arr.Add("sIzu@moldmaster.com")
        'For Each item In arr
        '    dgv_mail.Rows.Add({"true", item})
        'Next
        'Add Random email for testing scroll exception
        'For i = 0 To 30
        '    dgv_mail.Rows.Add({"false", "email" + i.ToString() + "@outlook.com"})
        'Next


    End Sub

    Private Sub loadMachineGrid()
        Dim readed As String

        Using reader As StreamReader = New StreamReader(path & "\sys\machine_list_.csys")
            DGV_MachineList.Rows.Clear()
            While Not reader.EndOfStream
                readed = reader.ReadLine().Split(",")(0)
                If Not readed.Contains("__") And Not (readed.Trim = "") Then
                    If If(DGV_tasks.Rows.Count = 0, False, DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("MachineName").Value.ToString().Contains(readed)) Then
                        DGV_MachineList.Rows.Add({"true", readed})
                    Else
                        DGV_MachineList.Rows.Add({"false", readed})
                    End If
                End If
            End While
        End Using
    End Sub

    Private Sub loadMailGrid()

        ClearGridMail()

        For Each row_ As DataGridViewRow In dgv_mail.Rows
            Dim b = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells(7).Value



            If Not (DGV_tasks.Rows.Count = 0 Or (b Is Nothing)) Then

                If (b.Contains(row_.Cells(1).Value.ToString())) Then
                    row_.Cells(0).Value = "true"
                Else
                    row_.Cells(0).Value = "false"
                End If

            Else

                row_.Cells(0).Value = "false"

            End If


        Next

    End Sub

    Private Function getlistOfMailSelected() As String

        Dim result As New System.Text.StringBuilder()
        Dim returnStr As String
        For i As Integer = 0 To dgv_mail.RowCount - 1

            If (Boolean.Parse(dgv_mail.Rows.Item(i).Cells(0).Value) = True) Then
                result.Append(dgv_mail.Rows.Item(i).Cells(1).Value)
                result.Append(If(i = dgv_mail.RowCount - 1, "", ";"))
            End If
        Next

        Try
            returnStr = result.ToString().Substring(0, result.ToString().Length)
        Catch ex As Exception
            returnStr = ""
        End Try

        Return returnStr

    End Function

    Private Function getListOfSelectedMachine() As String

        Dim result As New System.Text.StringBuilder()
        Dim returnStr As String
        For i As Integer = 0 To DGV_MachineList.RowCount - 1

            If (Boolean.Parse(DGV_MachineList.Rows.Item(i).Cells(0).Value) = True) Then
                result.Append(DGV_MachineList.Rows.Item(i).Cells(1).Value)
                result.Append(If(i = DGV_MachineList.RowCount - 1, "", ";"))
            End If
        Next

        Try
            If result.ToString().EndsWith(";") Then result.Remove(result.Length - 1, 1)
            returnStr = result.ToString() '.Substring(0, result.ToString().Length - 1)
        Catch ex As Exception
            returnStr = ""
        End Try


        Return (returnStr)

    End Function


    Private Function getListOfDaySelected() As String

        'Dim result As New System.Text.StringBuilder()
        'If Boolean.Parse(DGV_DayOfWeek.Rows.Item(7).Cells(0).Value) = True Then
        '    For i As Integer = 0 To 6
        '        result.Append(DGV_DayOfWeek.Rows.Item(i).Cells(1).Value)
        '        result.Append(If(i = DGV_DayOfWeek.RowCount - 2, "", ";"))
        '    Next
        'Else
        '    For i As Integer = 0 To 6

        '        If (Boolean.Parse(DGV_DayOfWeek.Rows.Item(i).Cells(0).Value) = True) Then

        '            result.Append(DGV_DayOfWeek.Rows.Item(i).Cells(1).Value)
        '            result.Append(If(i = DGV_DayOfWeek.RowCount - 2, "", ";"))
        '        End If
        '    Next
        'End If
        Try
            If (CBX_ReportType.SelectedIndex = 0) Then
                If CB_WKND.Checked Then
                    Return "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday"
                Else
                    Return "Tuesday, Wednesday, Thursday, Friday, Saturday"
                End If

            Else
                Return CB_DOW.SelectedItem.ToString()
            End If

        Catch ex As Exception
            Return Nothing
        End Try


        'Return (result.ToString())

    End Function





    Private Sub CSI_AutoReporting_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If (conn IsNot Nothing) Then
            conn.Close()
        End If
        ToCSV(ds.Tables("tableDGV"), path & "\sys\Auto_Report_config.csv", True)

    End Sub

    Private Sub bindDGVtoForm()

        loadMachineGrid()

        loadMailGrid()
        TB_TaskName.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("task_name").Value.ToString()

        DTP_Time.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("Time").Value.ToString()
        CBX_ReportType.SelectedIndex = CBX_ReportType.FindStringExact(DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("Report_Type").Value.ToString())
        TB_OutPutFolder.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("Output_Folder").Value.ToString()
        cb_d.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("dayback").Value.ToString()
        '    cb_t.Text = DGV_tasks.Rows.Item(DGV_tasks.CurrentRow.Index).Cells("timeback").Value.ToString().Substring(12)
        loadDayOfWeekGrid()

    End Sub


    Private Sub DGV_tasks_SelectionChanged(sender As Object, e As EventArgs) Handles DGV_tasks.SelectionChanged
        Try
            If DGV_tasks.SelectedCells.Item(0).RowIndex >= 0 Then
                BTN_Modify.Enabled = True
                BTN_CustomEmail.Enabled = False
            End If
        Catch ex As Exception
            If BTN_Modify.Text = "Modify" Then
                BTN_Modify.Enabled = False
                BTN_CustomEmail.Enabled = False
            End If

        End Try

        If Not (DGV_tasks.CurrentRow Is Nothing) And AddingBool = False Then
            bindDGVtoForm()
        End If
    End Sub

    Private Sub BTN_Remove_Click(sender As Object, e As EventArgs) Handles BTN_Remove.Click

        'cmdBuild = New MySqlCommandBuilder(adap)


        If Not (DGV_tasks.CurrentRow Is Nothing) Then
            Dim FindMyRow As DataRow = ds.Tables("tableDGV").Rows(DGV_tasks.CurrentRow.Index)


            Dim sql As String = ("delete from CSI_auth.Auto_Report_config where Task_name = '" + FindMyRow("Task_name") + "'")
            Dim connection As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)

            Try

                connection.Open()
                mysqlcomm.ExecuteNonQuery()
                ReportsChanged()
                'connection.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
            Finally
                connection.Close()
            End Try
            FindMyRow.Delete()
        End If
        ds.AcceptChanges()
        adap.Update(ds, "tableDGV")
        ReportsChanged()
        'loadMachineGrid()
        'loadDayOfWeekGrid()

        If (DGV_tasks.Rows.Count = 0) Then
            BTN_Remove.Enabled = False
            ClearGridDay()
            ClearGridMachine()
            clearForm()
        End If



    End Sub

    Public Sub ToCSV(table As DataTable, filePath As String, firstFill As Boolean)



    End Sub


    Private Sub CBX_ReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBX_ReportType.SelectedIndexChanged
        If Not (BTN_Add.Text = "Add New" And BTN_Modify.Text = "Modify") Then
            If CBX_ReportType.SelectedIndex = 0 Then
                CB_DOW.Enabled = False
                CB_DOW.SelectedIndex = -1
                CB_WKND.Enabled = True
                cb_d.Enabled = False
                cb_t.Enabled = False
                cb_d.SelectedIndex = -1
            ElseIf CBX_ReportType.SelectedIndex = 1 Then
                CB_DOW.Enabled = True
                CB_WKND.Enabled = False
                CB_WKND.Checked = False
                cb_d.Enabled = True
                cb_t.Enabled = True
            ElseIf CBX_ReportType.SelectedIndex = 2 Then
                CB_DOW.SelectedIndex = -1
                CB_DOW.Enabled = False
                CB_WKND.Enabled = False
                CB_WKND.Checked = False
                cb_d.Enabled = False
                cb_t.Enabled = False
                cb_d.SelectedIndex = -1
            End If
        Else

        End If

    End Sub

    Private Sub BTN_Next_Click(sender As Object, e As EventArgs)


        Dim newCurrentRow As Integer = DGV_tasks.CurrentRow.Index + 1
        If (newCurrentRow >= DGV_tasks.RowCount) Then
            newCurrentRow = 0
        End If

        DGV_tasks.CurrentCell = DGV_tasks.Rows(newCurrentRow).Cells(0)

    End Sub

    Private Sub BTN_Previous_Click(sender As Object, e As EventArgs)
        Dim newCurrentRow As Integer = DGV_tasks.CurrentRow.Index - 1

        If (newCurrentRow < 0) Then
            newCurrentRow = DGV_tasks.RowCount - 1
        End If

        DGV_tasks.CurrentCell = DGV_tasks.Rows(newCurrentRow).Cells(0)
    End Sub

    Private Sub BTN_Cancel_Click(sender As Object, e As EventArgs) Handles BTN_Cancel.Click
        BTN_Add.Text = "Add New"
        BTN_CustomEmail.Enabled = False
        BTN_Modify.Text = "Modify"
        BTN_Modify.Enabled = False
        CB_WKND.Checked = False
        CB_WKND.Enabled = False
        AddingBool = False
        LockUnlockForm(False)
        BTN_Remove.Enabled = False
        BTN_Add.Enabled = True
        TB_TaskName.Clear()
        TB_OutPutFolder.Clear()

        ClearGridDay()
        ClearGridMail()
        ClearGridMachine()
        CBX_ReportType.SelectedIndex = -1
        CB_DOW.SelectedIndex = -1
    End Sub

    Private Sub BTN_Output_Click(sender As Object, e As EventArgs) Handles BTN_Output.Click
        Dim folderDlg As New FolderBrowserDialog

        folderDlg.ShowNewFolderButton = False
        folderDlg.ShowNewFolderButton = True
        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            TB_OutPutFolder.Text = folderDlg.SelectedPath
            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
        End If

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BTN_Modify.Click
        Try

            If (BTN_Modify.Text = "Modify") Then
                If Not DGV_tasks.SelectedCells.Item(0).Value.ToString() = "" Then
                    tasktochange = DGV_tasks.SelectedCells.Item(0).RowIndex

                    'ClearGridMachine()
                    clearForm()
                    LockUnlockForm(True)
                    ClearGridMachine()
                    loadMachineGrid()

                    BTN_CustomEmail.Enabled = True
                    BTN_Add.Enabled = False
                    BTN_Remove.Enabled = False
                    BTN_Modify.Text = "Save"
                    BTN_Modify.Enabled = True
                    If CBX_ReportType.SelectedIndex = 0 Then
                        CB_DOW.Enabled = False
                        CB_WKND.Enabled = True
                    Else
                        CB_DOW.Enabled = True
                        CB_WKND.Enabled = False
                    End If
                    If CBX_ReportType.SelectedIndex = 1 Then
                        cb_d.Enabled = True
                        cb_t.Enabled = True
                    End If
                Else
                    AddingBool = True
                End If
                loadDayOfWeekGrid()
                CB_DOW.SelectedItem = -1
            Else
                If Directory.Exists(TB_OutPutFolder.Text) Then
                    'taskname = DGV_tasks.Rows(tasktochange).Cells(1).Value.ToString()
                    custommsg = GetCustomMsg(DGV_tasks.Rows(tasktochange).Cells(1).Value.ToString())
                    Dim sql As String = ("delete from CSI_auth.Auto_Report_config where Task_name = '" + DGV_tasks.Rows(tasktochange).Cells(1).Value.ToString() + "'")
                    Dim connection As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                    Dim mysqlcomm As MySqlCommand = New MySqlCommand(sql, connection)

                    Try
                        connection.Open()
                        mysqlcomm.ExecuteNonQuery()
                        ReportsChanged()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message())
                    Finally
                        connection.Close()
                    End Try
                    'DGV_tasks.Rows.RemoveAt(tasktochange)
                    'ds.Tables(0).Rows.RemoveAt(tasktochange)
                    modified = True
                    BTN_Modify.Text = "Modify"
                    BTN_CustomEmail.Enabled = False
                    AddingBool = True
                    BTN_Add_Click(BTN_Add, New EventArgs())
                    BTN_Add.Enabled = True
                    BTN_Remove.Enabled = True
                    BTN_Cancel.Enabled = False
                    CB_WKND.Enabled = False
                Else
                    MsgBox("The path that you have selected does not exists.")
                End If
            End If
            loadDayOfWeekGrid()
            CB_DOW.SelectedItem = -1
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BTN_CustomEmail_Click(sender As Object, e As EventArgs) Handles BTN_CustomEmail.Click
        Dim frm_custommsg As New CustomEmailMsg()
        frm_custommsg.taskname = TB_TaskName.Text
        frm_custommsg.ShowDialog()
    End Sub

    Private Sub ReportsChanged()
        Try
            'If File.Exists(path & "\sys\reportingchanged.csys") Then File.Delete(path & "\sys\reportingchanged.csys")
            Using writer As StreamWriter = New StreamWriter(path & "\sys\reportingchanged.csys", False)
                writer.Write(True)
                writer.Close()
            End Using
        Catch ex As Exception
            CSI_Lib.LogServerError("Unable to write reportingchanged. Exception:" + ex.Message, 1)
        End Try
    End Sub

    Private Sub GrBx_Parameter_Enter(sender As Object, e As EventArgs) Handles GrBx_Parameter.Enter

    End Sub

    Private Sub Btn_gen_Click(sender As Object, e As EventArgs) Handles Btn_gen.Click
        Dim Type As String = ""
        If CBX_ReportType.SelectedItem.ToString().Contains("Weekly") Then
            Type = "Weekly"
        ElseIf CBX_ReportType.SelectedItem.ToString().Contains("Monthly") Then
            Type = "Monthly"
        ElseIf CBX_ReportType.SelectedItem.ToString().Contains("Daily") Then
            Type = "Daily"
        End If
        'Dim dayOfNow As DateTime = Now()
        'Dim timeNow As String = dayOfNow.ToString("HH:mm")
        ' from Servicelibrary.vb ln 3483

        Try
            Dim Dataset_AutoReport As DataSet = New DataSet()
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim adap As New MySqlDataAdapter()

            conn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString
            Try
                conn.Open()

                Dim dayOfNow As DateTime = Now()
                Dim DayName As String = dayOfNow.DayOfWeek.ToString()
                Dim timeNow As String = dayOfNow.ToString("HH:mm")
                Dim QryStr As String
                Dim custommsg As String
                QryStr = String.Format("SELECT * FROM csi_auth.{0} where (Task_name like '%" + TB_TaskName.Text + "%'  )", "Auto_Report_config")


                Dim fileToSend As String = ""

                adap = New MySqlDataAdapter(QryStr, conn)
                adap.Fill(Dataset_AutoReport, "tableDGV")

                For Each row As DataRow In Dataset_AutoReport.Tables("tableDGV").Rows
                    Dim listOfMachine As String()
                    listOfMachine = row("MachineToReport").ToString().Split(";")

                    Dim SEvent(0 To listOfMachine.Count() - 1, 1) As String
                    For i = 0 To listOfMachine.Count() - 1
                        SEvent(i, 0) = listOfMachine(i)
                        SEvent(i, 1) = "SETUP"
                    Next
                    If (Convert.ToString(row("CustomMsg")).Length = 0) Then
                        custommsg = "Your " & Type & " CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                    Else
                        custommsg = Convert.ToString(row("CustomMsg"))
                    End If
                    If row("Report_Type").ToString() = "Monthly Availability - PDF" Or Convert.ToInt32(dayOfNow.Day.ToString()) = 1 Then
                        '     If row("Report_Type").ToString() = "Monthly Availability - PDF" Or Convert.ToInt32(dayOfNow.Day.ToString()) = 1 Then
                        'run monthly for last month
                        Dim startdate As DateTime
                        Dim enddate As DateTime
                        enddate = dayOfNow
                        startdate = dayOfNow.AddMonths(-1)
                        startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                        enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                        fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("short_FileName").ToString(), row("Task_name").ToString())

                        Try
                            If Not (row("MailTo") Is Nothing) Then
                                CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                            End If
                        Catch ex As Exception
                            CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                        End Try


                    ElseIf (row("Report_Type").ToString() = "Weekly Availability - PDF") Then

                        'run weekly report for last week
                        Dim startdate As DateTime
                        Dim enddate As DateTime

                        Dim days As Integer = 7
                        Try
                            If Not (row("dayback") Is Nothing) Then
                                days = Integer.Parse(row("dayback").ToString())
                            End If
                        Catch ex As Exception

                        End Try


                        enddate = dayOfNow.AddDays(-1)
                        'startdate = dayOfNow.AddDays(-(days - 1))
                        startdate = dayOfNow.AddDays(-days)
                        startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                        enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                        fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("short_FileName").ToString(), row("Task_name").ToString())

                        Try
                            If Not (row("MailTo") Is Nothing) Then
                                CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                            End If
                        Catch ex As Exception
                            CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                        End Try


                    ElseIf (row("Report_Type").ToString() = "Daily ( Yesterday ) Availability - PDF") Then
                        'run daily for last day
                        Dim startdate As DateTime
                        Dim enddate As DateTime
                        enddate = dayOfNow.AddDays(-1)
                        startdate = dayOfNow.AddDays(-1)
                        startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                        enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                        fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("short_FileName").ToString(), row("Task_name").ToString())

                        Try
                            If Not (row("MailTo") Is Nothing) Then
                                CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                            End If
                        Catch ex As Exception
                            CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                        End Try

                    Else
                        CSI_Lib.LogServiceError(DateTime.Now.ToString() + "invalid report type:" + row("Report_Type").ToString() + "TASK:" + row("Task_name").ToString(), 1)
                    End If

                    If fileToSend = "" Then
                        ' ????
                        ' check the file warning
                        'DisplayRectangle message
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message())
            Finally
                conn.Close()
            End Try
        Catch ex As Exception
            MessageBox.Show("Error generating autoreports, MSG:" & ex.ToString())
        Finally
            If conn IsNot Nothing Then If conn.State = ConnectionState.Open Then conn.Close()
        End Try
        '      CSI_Lib.generateReport(read_tree(), Type, DTP_Start.Value, DTP_End.Value, tb_pathReport.Text, SEvent, False)
    End Sub

    Private Sub BG_generate_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BG_generate.DoWork
        Dim Type As String = ""
        If CBX_ReportType.SelectedItem.ToString().Contains("Weekly") Then
            Type = "Weekly"
        ElseIf CBX_ReportType.SelectedItem.ToString().Contains("Monthly") Then
            Type = "Monthly"
        ElseIf CBX_ReportType.SelectedItem.ToString().Contains("Daily") Then
            Type = "Daily"
        End If

        ' from Servicelibrary.vb ln 3483
        Dim custommsg As String
        Try
            Dim Dataset_AutoReport As DataSet = New DataSet()
            Dim conn As MySqlConnection = New MySqlConnection()
            Dim adap As New MySqlDataAdapter()

            conn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString
            Try
                conn.Open()

                Dim dayOfNow As DateTime = Now()
                Dim DayName As String = dayOfNow.DayOfWeek.ToString()
                Dim timeNow As String = dayOfNow.ToString("HH:mm")
                Dim QryStr As String

                QryStr = String.Format("SELECT * FROM csi_auth.{0} where (Task_name like '%" + TB_TaskName.Text + "%'  )", "Auto_Report_config")


                Dim fileToSend As String = ""

                adap = New MySqlDataAdapter(QryStr, conn)
                adap.Fill(Dataset_AutoReport, "tableDGV")

                For Each row As DataRow In Dataset_AutoReport.Tables("tableDGV").Rows
                    Dim listOfMachine As String()
                    listOfMachine = row("MachineToReport").ToString().Split(";")

                    Dim SEvent(0 To listOfMachine.Count() - 1, 1) As String
                    For i = 0 To listOfMachine.Count() - 1
                        SEvent(i, 0) = listOfMachine(i)
                        SEvent(i, 1) = "SETUP"
                    Next
                    If (Convert.ToString(row("CustomMsg")).Length = 0) Then
                        custommsg = "Your " & Type & " CSIFlex report On " & Convert.ToString(System.DateTime.Today.ToString("D")) & " " & timeNow
                    Else
                        custommsg = Convert.ToString(row("CustomMsg"))
                    End If
                    If row("Report_Type").ToString() = "Monthly Availability - PDF" And Convert.ToInt32(dayOfNow.Day.ToString()) = 1 Then
                        'run monthly for last month
                        Dim startdate As DateTime
                        Dim enddate As DateTime
                        enddate = dayOfNow
                        startdate = dayOfNow.AddMonths(-1)
                        startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                        enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                        fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("short_FileName").ToString(), row("Task_name").ToString())

                        Try
                            If Not (row("MailTo") Is Nothing) Then
                                CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                            End If
                        Catch ex As Exception
                            CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                        End Try


                    ElseIf (row("Report_Type").ToString() = "Weekly Availability - PDF") Then

                        'run weekly report for last week
                        Dim startdate As DateTime
                        Dim enddate As DateTime

                        Dim days As Integer = 7
                        Try
                            If Not (row("dayback") Is Nothing) Then
                                days = Integer.Parse(row("dayback").ToString())
                            End If
                        Catch ex As Exception

                        End Try


                        enddate = dayOfNow.AddDays(-1)
                        'startdate = dayOfNow.AddDays(-(days - 1))
                        startdate = dayOfNow.AddDays(-days)
                        startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                        enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                        fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("short_FileName").ToString(), row("Task_name").ToString())

                        Try
                            If Not (row("MailTo") Is Nothing) Then
                                CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                            End If
                        Catch ex As Exception
                            CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                        End Try


                    ElseIf (row("Report_Type").ToString() = "Daily ( Yesterday ) Availability - PDF") Then
                        'run daily for last day
                        Dim startdate As DateTime
                        Dim enddate As DateTime
                        enddate = dayOfNow.AddDays(-1)
                        startdate = dayOfNow.AddDays(-1)
                        startdate = New DateTime(startdate.Year, startdate.Month, startdate.Day, 0, 0, 0)
                        enddate = New DateTime(enddate.Year, enddate.Month, enddate.Day, 23, 59, 59)

                        fileToSend = CSI_Lib.generateReport(listOfMachine, row("Report_Type").ToString(), startdate, enddate, row("Output_Folder").ToString(), SEvent, True, row("short_FileName").ToString(), row("Task_name").ToString())

                        Try
                            If Not (row("MailTo") Is Nothing) Then
                                CSI_Lib.sendReportByMail(row("MailTo"), fileToSend, custommsg)
                            End If
                        Catch ex As Exception
                            CSI_Lib.LogServiceError("Error trying to send report by email:" & ex.ToString(), 1)
                        End Try

                    Else
                        CSI_Lib.LogServiceError(DateTime.Now.ToString() + "invalid report type:" + row("Report_Type").ToString() + "TASK:" + row("Task_name").ToString(), 1)
                    End If

                    If fileToSend = "" Then
                        ' ????
                        ' check the file warning
                        'DisplayRectangle message
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message())
            Finally
                conn.Close()
            End Try
        Catch ex As Exception
            MessageBox.Show("Error generating autoreports, MSG:" & ex.ToString())
        Finally
            If conn IsNot Nothing Then If conn.State = ConnectionState.Open Then conn.Close()
        End Try

    End Sub
End Class
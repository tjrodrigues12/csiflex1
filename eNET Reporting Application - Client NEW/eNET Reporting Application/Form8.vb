Imports System.Data.OleDb



Public Class Form8
    Public weeks_of As Integer = 7 'number of days in a week
    Public general_array_weekly(5, 90, 1) As String

    Dim cnt As System.Data.OleDb.OleDbConnection
    Public list2 As New List(Of String)
    Public Date_ As String
    Dim mt_present As Boolean = False
    Dim st_present As Boolean = False
    Public stat(4) As String
    Public dates_(6, 90, 1) As String
    Public periode_Cycles_times(5, 1) As String ' In seconds
    Public periode_Cycles_times_other(1, 4, 1) As String ' In seconds
    Public i As Integer




    '    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '        Me.Top = 0
    '        Me.Left = 0
    '        Me.Height = 200
    '        MonthCalendar1.MaxDate = Now

    '        Dim first_ As Boolean = True

    '        Dim i As Integer
    '        Dim j As Integer
    '        Dim k As Integer

    '        Dim strtmp As String


    '        Dim machine() As String
    '        Dim pos As String = "tvwchild"
    '        TreeView1.Nodes.Clear()

    '        '**** Load machines names from _SETUP\MonList.sys
    '        Dim file As System.IO.StreamReader

    '        Dim fso
    '        fso = CreateObject("Scripting.FileSystemObject")

    '        If Not My.Computer.FileSystem.FileExists(Form1.chemin_eNET + "\_SETUP\MonList.sys") Then
    '            MessageBox.Show("le fichier 'Monlist.sys' present dans le repertoire _SETUP d'eNET est introuvable")
    '            GoTo fin
    '        End If

    '        file = My.Computer.FileSystem.OpenTextFileReader(Form1.chemin_eNET + "\_SETUP\MonList.sys")
    '        i = 0
    '        j = -1
    '        k = -1



    '        While Not file.EndOfStream
    '            ReDim Preserve machine(i + 1)
    '            machine(i) = file.ReadLine()


    '            strtmp = Strings.Left(machine(i), 3)
    '            If (strtmp = "_MT") Then
    '                list2.Add(machine(i))
    '                j = j + 1
    '                mt_present = True
    '                TreeView1.Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
    '            End If


    '            If (strtmp = "_ST") Then
    '                st_present = True
    '                list2.Add(machine(i))
    '                If mt_present = False Then

    '                    TreeView1.Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
    '                Else
    '                    TreeView1.Nodes(j).Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
    '                End If

    '                k = k + 1
    '            End If

    '            If (strtmp <> "_ST") And (strtmp <> "_MT") Then

    '                If st_present = False And mt_present = True Then
    '                    TreeView1.Nodes(j).Nodes.Add(machine(i), machine(i))

    '                Else
    '                    If mt_present = False And st_present = True Then

    '                        TreeView1.Nodes(k).Nodes.Add(machine(i), machine(i))

    '                    Else
    '                        If mt_present = False And st_present = False Then
    '                            TreeView1.Nodes.Add(machine(i), machine(i))

    '                        Else
    '                            TreeView1.Nodes(j).Nodes(k).Nodes.Add(machine(i), machine(i))

    '                        End If
    '                    End If
    '                End If
    '            End If
    '            i = i + 1
    '        End While

    '        file.Close()
    'Fin:


    '    End Sub


    'Private Sub monthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged

    '    If DateDiff(DateInterval.Day, MonthCalendar1.SelectionRange.Start.Date, Now) < weeks_of Then
    '        MonthCalendar1.SetSelectionRange(MonthCalendar1.SelectionRange.Start.Date, DateAdd("d", DateDiff(DateInterval.Day, MonthCalendar1.SelectionRange.Start.Date, Now), MonthCalendar1.SelectionRange.Start.Date))
    '    Else
    '        MonthCalendar1.SetSelectionRange(MonthCalendar1.SelectionRange.Start.Date, DateAdd("d", weeks_of - 1, MonthCalendar1.SelectionRange.Start.Date))
    '    End If
    'End Sub



    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Call weekly()
    'End Sub

    '    '-----------------------------------------------------------------------------------------------------------------------
    '    ' form8.weekly     
    '    '-----------------------------------------------------------------------------------------------------------------------
    '    'Public Sub weekly(dt1 As Date, dt2 As Date)
    '    Public Sub weekly()

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

    '        Dim percent(7) As Integer


    '        ' Dim results As DateTime()
    '        Dim k As Integer = 0
    '        Dim final_time As Double
    '        Dim total1 As Double = 0
    '        Dim total2 As Double = 0
    '        Dim total3 As Double = 0

    '        Dim last_loop_ As Boolean = False

    '        Dim nb_mch As Integer

    '        final_time = 0

    '        '*** DB Querry **********************

    '        '*************************************************************************************************************************************************'
    '        '**** DB Connection
    '        '*************************************************************************************************************************************************'
    '        Dim dbConnectStr As String
    '        dbConnectStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Form1.chemin_bd & "\CSI_Database.mdb;"
    '        cnt = New System.Data.OleDb.OleDbConnection(dbConnectStr)
    '        cnt.Open()
    '        If cnt.State = 1 Then
    '        Else
    '            MessageBox.Show("Connection to the database failed")
    '            GoTo fin
    '        End If
    '        '*************************************************************************************************************************************************'
    '        '**** DB Connection -END
    '        '*************************************************************************************************************************************************'

    '        Dim SchemaTable

    '        i = 0
    '        '  Dim m As Integer
    '        '   Dim n As Integer
    '        Dim j As Integer = 0



    '        '*************************************************************************************************************************************************'
    '        '**** Active machines from the treenode
    '        '*************************************************************************************************************************************************'

    '            For Each treeviewnode As TreeNode In Form3.TreeView1.Nodes
    '                If treeviewnode.Checked = True Then
    '                    active_machines(i) = treeviewnode.Text
    '                    i = i + 1
    '                    ReDim Preserve active_machines(i)
    '                End If
    '                For Each treeviewnode2 As TreeNode In treeviewnode.Nodes   'Me.TreeView1.Nodes(j).Nodes
    '                    For Each treeviewnode3 As TreeNode In treeviewnode2.Nodes ' Me.TreeView1.Nodes(j).Nodes(k).Nodes 'KKK +++ ???? 
    '                        If treeviewnode3.Checked = True Then
    '                            active_machines(i) = treeviewnode3.Text
    '                            i = i + 1
    '                            ReDim Preserve active_machines(i)
    '                        End If
    '                        k = k + 1
    '                    Next
    '                    If treeviewnode2.Checked = True Then
    '                        active_machines(i) = treeviewnode2.Text
    '                        i = i + 1
    '                        ReDim Preserve active_machines(i)
    '                    End If
    '                Next
    '                j = j + 1
    '            Next

    '            i = 0
    '            j = 0
    '            Dim tmptmp As String
    '            For Each item In active_machines
    '                For Each item2 In Form3.list2
    '                    tmptmp = Strings.Right(item2, Strings.Len(item2) - 4)
    '                    If item = tmptmp Then
    '                        active_machines(i) = ""
    '                    End If
    '                    j = j + 1
    '                Next
    '                i = i + 1
    '            Next

    '            i = 0
    '            Dim erased As Integer = 0

    '            For Each item In active_machines

    '                If active_machines(i) = "" Or active_machines(i) Is Nothing Then
    '                    For j = i To UBound(active_machines) - 1
    '                        active_machines(j) = active_machines(j + 1)
    '                    Next j
    '                End If

    '                i = i + 1
    '            Next

    '            For Each item In active_machines
    '                If item = "" Or item Is Nothing Then
    '                    erased = erased + 1
    '                End If
    '            Next
    '            ReDim Preserve active_machines(i - erased - 1)

    '        '*************************************************************************************************************************************************'
    '        '**** end- treenode
    '        '*************************************************************************************************************************************************'

    '        Dim first_insertion As Boolean = True

    '        i = 0
    '        Form4.Show()
    '        Form4.Label1.Text = "preparing ... "
    '        Form4.ProgressBar1.Value = 0

    '        Dim column_ As Integer = 1
    '        Dim year_end_tmp As Integer = year_end
    '        Dim month_end_tmp As Integer = month_end
    '        Dim day_end_tmp As Integer = day_end

    '        Dim DaysInMonth As Integer
    '        Dim loop_ As Boolean = True

    '        Dim sorted_stats(4) As String
    '        Dim final_time_other(4) As Double
    '        Dim stat(3) As String

    '        i = 0

    '        'For each selected machine:====================================================================================================
    '        '==============================================================================================================================
    '        For Each items In active_machines

    '            year_start = Form3.DateTimePicker1.Value.Year - 2000
    '            year_end = Form3.DateTimePicker2.Value.Year - 2000

    '            month_start = Form3.DateTimePicker1.Value.Month
    '            month_end = Form3.DateTimePicker2.Value.Month

    '            day_start = Form3.DateTimePicker1.Value.Day
    '            day_end = Form3.DateTimePicker2.Value.Day


    '            '*************************************************************************************************************************************************'
    '            '**** LOOP - week after week ' For 1 periode, 4 weeks after , and +
    '            '*************************************************************************************************************************************************'
    '            last_loop_ = False
    '            loop_ = True
    '            For column_ = 1 To 55
    '                active_machines(i) = Strings.Replace(active_machines(i), " ", "")
    '                active_machines(i) = Strings.Replace(active_machines(i), "-", "")

    '                k = 0
    '                final_time = Nothing

    '                total1 = 0
    '                total2 = 0
    '                total3 = 0

    '                ' Delete the tmp table if exist =======================================
    '                SchemaTable = cnt.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, _
    '                           New Object() {Nothing, Nothing, Nothing, "TABLE"})
    '                For j = 0 To SchemaTable.Rows.Count - 1
    '                    If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table") Then
    '                        Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table]", cnt)
    '                        cmd_delete.ExecuteNonQuery()
    '                    End If
    '                    If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table1") Then
    '                        Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table1]", cnt)
    '                        cmd_delete.ExecuteNonQuery()
    '                    End If
    '                    If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table2") Then
    '                        Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table2]", cnt)
    '                        cmd_delete.ExecuteNonQuery()
    '                    End If
    '                    If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table3") Then
    '                        Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table3]", cnt)
    '                        cmd_delete.ExecuteNonQuery()
    '                    End If
    '                Next j
    '                '=======================================================================
    '                Dim tmp_table_cmd As New OleDbCommand
    '                Dim tmp_table2_cmd As New OleDbCommand
    '                Dim tmp_table1_cmd As New OleDbCommand
    '                Dim tmp_table3_cmd As New OleDbCommand




    '                If year_start = year_end Then
    '                    If month_start = month_end Then
    '                        tmp_table_cmd.CommandText = "SELECT * INTO tmp_table FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & " and [month_]= " & month_start & " and [day_] between " & day_start & " and " & day_end
    '                        tmp_table_cmd.Connection = cnt
    '                        tmp_table_cmd.ExecuteNonQuery()
    '                    Else
    '                        If month_start < month_end Then
    '                            DaysInMonth = System.DateTime.DaysInMonth(year_start + 2000, month_start)
    '                            tmp_table1_cmd.CommandText = "SELECT * INTO tmp_table1 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] = " & month_start & " and [day_] >=  " & day_start & " and [day_] <= " & DaysInMonth
    '                            tmp_table1_cmd.Connection = cnt
    '                            tmp_table1_cmd.ExecuteNonQuery()

    '                            tmp_table2_cmd.CommandText = "SELECT * INTO tmp_table2 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] = " & month_end & " and [day_] >=  1  and [day_] <= " & day_end
    '                            tmp_table2_cmd.Connection = cnt
    '                            tmp_table2_cmd.ExecuteNonQuery()
    '                            If month_end - month_start <> 1 Then
    '                                tmp_table3_cmd.CommandText = "SELECT * INTO tmp_table3 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] >=" & month_start + 1 & " and [month_] <=" & month_end - 1
    '                                tmp_table3_cmd.Connection = cnt
    '                                tmp_table3_cmd.ExecuteNonQuery()
    '                            End If
    '                            tmp_table_cmd.Connection = cnt
    '                            tmp_table_cmd.CommandText = "SELECT * INTO tmp_table  FROM  tmp_table1"
    '                            tmp_table_cmd.ExecuteNonQuery()
    '                            tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table2"
    '                            tmp_table_cmd.ExecuteNonQuery()
    '                            If month_end - month_start <> 1 Then
    '                                tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table3"
    '                                tmp_table_cmd.ExecuteNonQuery()
    '                            End If
    '                        Else
    '                        End If
    '                    End If
    '                Else
    '                    If year_end - year_start = 1 Then
    '                        DaysInMonth = System.DateTime.DaysInMonth(year_start + 2000, month_start)
    '                        tmp_table1_cmd.CommandText = "SELECT * INTO tmp_table1 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] = " & month_start & " and [day_] >=  " & day_start & " and [day_] <= " & DaysInMonth
    '                        tmp_table1_cmd.Connection = cnt
    '                        tmp_table1_cmd.ExecuteNonQuery()
    '                        tmp_table3_cmd.CommandText = "SELECT * INTO tmp_table3 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] >=" & month_start + 1 & " and [month_] <= 12 "
    '                        tmp_table3_cmd.Connection = cnt
    '                        tmp_table3_cmd.ExecuteNonQuery()
    '                        tmp_table_cmd.Connection = cnt
    '                        tmp_table_cmd.CommandText = "SELECT * INTO tmp_table  FROM  tmp_table1"
    '                        tmp_table_cmd.ExecuteNonQuery()
    '                        tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table3"
    '                        tmp_table_cmd.ExecuteNonQuery()
    '                        tmp_table1_cmd.CommandText = "DROP TABLE [tmp_table1]"
    '                        tmp_table1_cmd.Connection = cnt
    '                        tmp_table1_cmd.ExecuteNonQuery()
    '                        tmp_table1_cmd.CommandText = "DROP TABLE [tmp_table3]"
    '                        tmp_table1_cmd.Connection = cnt
    '                        tmp_table1_cmd.ExecuteNonQuery()
    '                        tmp_table1_cmd.CommandText = "SELECT * INTO tmp_table1 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_end & "and [month_] >= 1 " & " and [month_] <=" & month_end - 1
    '                        tmp_table1_cmd.Connection = cnt
    '                        tmp_table1_cmd.ExecuteNonQuery()
    '                        tmp_table3_cmd.CommandText = "SELECT * INTO tmp_table3 FROM [tbl_" & active_machines(i) & "] WHERE [year_] = " & year_end & "and [month_] =" & month_end & " and [day_] >= 1 and [day_] <=  " & day_end
    '                        tmp_table3_cmd.Connection = cnt
    '                        tmp_table3_cmd.ExecuteNonQuery()
    '                        tmp_table_cmd.Connection = cnt
    '                        tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table1"
    '                        tmp_table_cmd.ExecuteNonQuery()
    '                        tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table3"
    '                        tmp_table_cmd.ExecuteNonQuery()

    '                    Else
    '                        MessageBox.Show("You can generate reports within two years")
    '                    End If
    '                End If



    '                '*************************************************************************************************************************************************'
    '                '**** Availlable status
    '                '*************************************************************************************************************************************************'

    '                Dim cmd_status As New OleDbCommand("SELECT DISTINCT [status] FROM [tmp_table] ", cnt)
    '                Dim reader_status As OleDbDataReader = cmd_status.ExecuteReader()

    '                stat(0) = "_CON"
    '                stat(1) = "_COFF"
    '                stat(2) = "_SETUP"

    '                'Select Other status in the period 
    '                k = 3
    '                While reader_status.Read()
    '                    If reader_status.HasRows And reader_status.GetValue(0) <> "_CON" And reader_status.GetValue(0) <> "_COFF" And reader_status.GetValue(0) <> "_SETUP" And reader_status.GetValue(0) <> "_SH_START" And reader_status.GetValue(0) <> "_SH_END" And Strings.Mid(reader_status.GetValue(0), 1, 7) <> "_PARTNO" Then
    '                        ReDim Preserve stat(k + 1)
    '                        stat(k) = reader_status.GetValue(0)
    '                        k = k + 1
    '                    End If
    '                End While
    '                reader_status.Close()
    '                '*************************************************************************************************************************************************


    '                nb_mch = UBound(active_machines) + 1
    '                Form4.Show()
    '                Form4.Label1.Text = "Generating weekly reports ...: " & active_machines(i) & "."
    '                Form4.ProgressBar1.Value = (33 * (i + 1)) / nb_mch


    '                '*************************************************************************************************************************************************'
    '                '**** Cycle times
    '                '*************************************************************************************************************************************************'

    '                'for each status: on off setup
    '                For k = 0 To 2
    '                    Dim cmd As New OleDbCommand("SELECT * FROM [tmp_table] WHERE [status] = '" & stat(k) & "'", cnt)
    '                    Dim reader As OleDbDataReader = cmd.ExecuteReader()

    '                    final_time = 0
    '                    While reader.Read()
    '                        If reader.HasRows Then
    '                            final_time = final_time + reader.GetValue(6)
    '                        End If
    '                    End While
    '                    reader.Close()

    '                    If final_time > 0 Then
    '                        percent(k) = final_time
    '                        total1 = total1 + final_time
    '                    End If
    '                Next k

    '                'for each status: not( on off setup)
    '                For k = 0 To UBound(sorted_stats)
    '                    sorted_stats(k) = ""
    '                Next
    '                For k = 0 To UBound(final_time_other)
    '                    final_time_other(k) = 0
    '                Next

    '                For k = 3 To UBound(stat) - 1
    '                    Dim cmd_other As New OleDbCommand("SELECT * FROM [tmp_table] WHERE [status] = '" & stat(k) & "'", cnt)
    '                    Dim reader_other As OleDbDataReader = cmd_other.ExecuteReader()

    '                    If k < 6 Then
    '                        sorted_stats(k - 3) = stat(k)
    '                        final_time_other(k - 3) = 0

    '                        While reader_other.Read()
    '                            If reader_other.HasRows Then
    '                                final_time_other(k - 3) = final_time_other(k - 3) + reader_other.GetValue(6)
    '                            End If
    '                        End While
    '                    Else
    '                        While reader_other.Read()
    '                            If reader_other.HasRows Then
    '                                final_time_other(3) = final_time_other(3) + reader_other.GetValue(6)
    '                            End If
    '                        End While
    '                    End If
    '                    reader_other.Close()
    '                Next k
    '                sorted_stats(3) = "Other"

    '                'put other status in general_array_weekly
    '                For k = 0 To 3
    '                    If final_time_other(k) > 0 Then
    '                        percent(k + 3) = final_time_other(k)
    '                        general_array_weekly(k + 3, 0, i) = sorted_stats(k)
    '                        total1 = total1 + final_time_other(k)
    '                    Else
    '                        general_array_weekly(k + 3, 0, i) = sorted_stats(k)
    '                    End If
    '                Next k

    '                ' percentage :
    '                If total1 > 0 Then
    '                    For indx = 0 To 6
    '                        general_array_weekly(indx, column_, i) = (100 * percent(indx) / total1).ToString("00.00")
    '                    Next
    '                    general_array_weekly(7, column_, i) = (100 * (percent(0) + percent(1) + percent(2) + percent(3) + percent(4) + percent(5) + percent(6)) / total1).ToString("00.00")
    '                Else
    '                    For indx = 0 To 7
    '                        general_array_weekly(indx, column_, i) = 0
    '                    Next
    '                End If

    '                For indx = 0 To 6
    '                    percent(indx) = 0
    '                Next
    '                If Form3.from_form3 = True Then
    '                    Date_ = Form3.DateTimePicker1.Value.Date.Day.ToString & "-" & Form3.DateTimePicker1.Value.Date.Month.ToString & "-" & Form3.DateTimePicker1.Value.Date.Year.ToString & "__" & Form3.DateTimePicker2.Value.Date.Day.ToString & "-" & Form3.DateTimePicker2.Value.Date.Month.ToString & "-" & Form3.DateTimePicker2.Value.Date.Year.ToString
    '                Else
    '                    Date_ = MonthCalendar1.SelectionRange.Start.Date.Day & "-" & MonthCalendar1.SelectionRange.Start.Date.Month & "-" & MonthCalendar1.SelectionRange.Start.Date.Year & "__" & MonthCalendar1.SelectionRange.End.Date.Day & "-" & MonthCalendar1.SelectionRange.End.Date.Month & "-" & MonthCalendar1.SelectionRange.End.Date.Year
    '                End If

    '                general_array_weekly(0, 0, i) = active_machines(i)
    '                general_array_weekly(1, 0, i) = day_start.ToString & "-" & month_start.ToString & "-" & year_start.ToString

    '                If Form3.from_form3 = True Then
    '                    general_array_weekly(2, 0, i) = Form3.DateTimePicker1.Value.Date.Day.ToString & "-" & Form3.DateTimePicker1.Value.Date.Month.ToString & "--" & Form3.DateTimePicker2.Value.Date.Day.ToString & "-" & Form3.DateTimePicker2.Value.Date.Month.ToString & "-" & Form3.DateTimePicker2.Value.Date.Year.ToString
    '                Else
    '                    general_array_weekly(2, 0, i) = MonthCalendar1.SelectionRange.Start.Date.Day.ToString & "-" & MonthCalendar1.SelectionRange.Start.Date.Month.ToString & "--" & MonthCalendar1.SelectionRange.End.Date.Day.ToString & "-" & MonthCalendar1.SelectionRange.End.Date.Month.ToString & "-" & MonthCalendar1.SelectionRange.End.Date.Year.ToString
    '                End If

    '                If day_start > 1 Then
    '                    day_end = day_start - 1
    '                    month_end = month_start
    '                    year_end = year_start
    '                Else
    '                    If month_start <> 1 Then
    '                        month_end = month_start - 1
    '                        year_end = year_start
    '                        day_end = System.DateTime.DaysInMonth(year_end + 2000, month_end)
    '                    Else
    '                        month_end = 12
    '                        year_end = year_start - 1
    '                        day_end = System.DateTime.DaysInMonth(year_end + 2000, month_end)
    '                    End If
    '                End If

    '                If day_end > 6 Then
    '                    day_start = day_end - 6
    '                    month_start = month_end
    '                    year_start = year_end
    '                Else
    '                    month_start = month_end - 1
    '                    year_start = year_end

    '                    If month_start = 0 Then
    '                        month_start = 12
    '                        year_start = year_start - 1
    '                    End If

    '                    day_start = System.DateTime.DaysInMonth(year_start + 2000, month_start) - 6 + day_end
    '                End If

    '            Next column_
    '            column_ = 0
    'no_loop:
    '            i = i + 1
    '            ReDim Preserve general_array_weekly(26, 24, i + 1)
    '        Next '/item /machine ========================================================================================================
    '        '============================================================================================================================

    '        ReDim Preserve general_array_weekly(26, 24, i)

    '        cnt.Close()
    '        Form4.Close()

    '        'Call forme_fill(general_array_weekly, i)
    'Fin:
    '    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' form8.Periode     
    '-----------------------------------------------------------------------------------------------------------------------
    Public Sub Periode(dt1 As Date, dt2 As Date, periode_ As String)

        Dim year_start As Integer = dt1.Year - 2000
        Dim year_end As Integer = dt2.Year - 2000

        Dim first_ As Boolean = True
        Dim month_start As Integer = dt1.Month
        Dim month_end As Integer = dt2.Month

        Dim ThirdDim As Integer = 0

        Dim day_start As Integer = dt1.Day
        Dim day_end As Integer = dt2.Day
        Dim indx As Integer

        Dim active_machines(1) As String

        Dim percent(7) As Integer
        Dim final_times(4) As Integer

        ' Dim results As DateTime()
        Dim k As Integer = 0
        Dim final_time As Double
        Dim total1 As Double = 0
        Dim total2 As Double = 0
        Dim total3 As Double = 0

        Dim last_loop_ As Boolean = False

        Dim nb_mch As Integer

        final_time = 0

        '*** DB Querry **********************

        '*************************************************************************************************************************************************'
        '**** DB Connection
        '*************************************************************************************************************************************************'
        Dim dbConnectStr As String
        dbConnectStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Reporting_application.chemin_bd & "\CSI_Database.mdb;"
        cnt = New System.Data.OleDb.OleDbConnection(dbConnectStr)
        cnt.Open()
        If cnt.State = 1 Then
        Else
            MessageBox.Show("Connection to the database failed")
            GoTo fin
        End If
        '*************************************************************************************************************************************************'
        '**** DB Connection -END
        '*************************************************************************************************************************************************'

        Dim SchemaTable

        i = 0
        '  Dim m As Integer
        '   Dim n As Integer
        Dim j As Integer = 0



        '*************************************************************************************************************************************************'
        '**** Active machines from the treenode
        '*************************************************************************************************************************************************'

        For Each treeviewnode As TreeNode In Config_report.TreeView_machine.Nodes
            If treeviewnode.Checked = True Then
                active_machines(i) = treeviewnode.Text
                i = i + 1
                ReDim Preserve active_machines(i)
            End If
            For Each treeviewnode2 As TreeNode In treeviewnode.Nodes   'Me.TreeView1.Nodes(j).Nodes
                For Each treeviewnode3 As TreeNode In treeviewnode2.Nodes ' Me.TreeView1.Nodes(j).Nodes(k).Nodes 'KKK +++ ???? 
                    If treeviewnode3.Checked = True Then
                        active_machines(i) = treeviewnode3.Text
                        i = i + 1
                        ReDim Preserve active_machines(i)
                    End If
                    k = k + 1
                Next
                If treeviewnode2.Checked = True Then
                    active_machines(i) = treeviewnode2.Text
                    i = i + 1
                    ReDim Preserve active_machines(i)
                End If
            Next
            j = j + 1
        Next

        i = 0
        j = 0
        Dim tmptmp As String
        For Each item In active_machines
            For Each item2 In Config_report.list2
                tmptmp = Strings.Right(item2, Strings.Len(item2) - 4)
                If item = tmptmp Then
                    active_machines(i) = ""
                End If
                j = j + 1
            Next
            i = i + 1
        Next

        i = 0
        Dim erased As Integer = 0

        For Each item In active_machines

            If active_machines(i) = "" Or active_machines(i) Is Nothing Then
                For j = i To UBound(active_machines) - 1
                    active_machines(j) = active_machines(j + 1)
                Next j
            End If

            i = i + 1
        Next

        For Each item In active_machines
            If item = "" Or item Is Nothing Then
                erased = erased + 1
            End If
        Next
        ReDim Preserve active_machines(i - erased - 1)

        '*************************************************************************************************************************************************'
        '**** end- treenode
        '*************************************************************************************************************************************************'






        Dim first_insertion As Boolean = True

        i = 0
        Form4.Show()
        Form4.Label1.Text = "preparing ... "
        Form4.ProgressBar1.Value = 0

        Dim column_ As Integer = 1
        'Dim year_end_tmp As Integer = year_end
        'Dim month_end_tmp As Integer = month_end
        'Dim day_end_tmp As Integer = day_end

        Dim DaysInMonth As Integer
        Dim loop_ As Boolean = True

        '  Dim sorted_stats(4) As String
        Dim final_time_other(4) As Double


        i = 0

        'For each selected machine:====================================================================================================
        '==============================================================================================================================
        For Each items In active_machines

            year_start = dt1.Year - 2000
            year_end = dt2.Year - 2000

            month_start = dt1.Month
            month_end = dt2.Month

            day_start = dt1.Day
            day_end = dt2.Day

            '*************************************************************************************************************************************************'
            '**** LOOP - week after week ' For 1 periode, 4 weeks after , and +
            '*************************************************************************************************************************************************'
            last_loop_ = False
            loop_ = True
            For column_ = 1 To 89
                active_machines(i) = Strings.Replace(active_machines(i), " ", "")
                active_machines(i) = Strings.Replace(active_machines(i), "-", "")

                k = 0
                final_time = Nothing

                total1 = 0
                total2 = 0
                total3 = 0

                ' Delete the tmp table if exist =======================================
                SchemaTable = cnt.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, _
                           New Object() {Nothing, Nothing, Nothing, "TABLE"})
                For j = 0 To SchemaTable.Rows.Count - 1
                    If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table") Then
                        Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table]", cnt)
                        cmd_delete.ExecuteNonQuery()
                    End If
                    If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table1") Then
                        Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table1]", cnt)
                        cmd_delete.ExecuteNonQuery()
                    End If
                    If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table2") Then
                        Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table2]", cnt)
                        cmd_delete.ExecuteNonQuery()
                    End If
                    If (SchemaTable.Rows(j)!TABLE_NAME = "tmp_table3") Then
                        Dim cmd_delete As New OleDbCommand("DROP TABLE [tmp_table3]", cnt)
                        cmd_delete.ExecuteNonQuery()
                    End If
                Next j
                '=======================================================================
                Dim tmp_table_cmd As New OleDbCommand
                Dim tmp_table2_cmd As New OleDbCommand
                Dim tmp_table1_cmd As New OleDbCommand
                Dim tmp_table3_cmd As New OleDbCommand

                If year_start = year_end Then
                    If month_start = month_end Then
                        tmp_table_cmd.CommandText = "SELECT * INTO tmp_table FROM [shift_tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & " and [month_]= " & month_start & " and [day_] between " & day_start & " and " & day_end
                        tmp_table_cmd.Connection = cnt
                        tmp_table_cmd.ExecuteNonQuery()
                    Else
                        If month_start < month_end Then
                            DaysInMonth = System.DateTime.DaysInMonth(year_start + 2000, month_start)
                            tmp_table1_cmd.CommandText = "SELECT * INTO tmp_table1 FROM [shift_tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] = " & month_start & " and [day_] >=  " & day_start & " and [day_] <= " & DaysInMonth
                            tmp_table1_cmd.Connection = cnt
                            tmp_table1_cmd.ExecuteNonQuery()

                            tmp_table2_cmd.CommandText = "SELECT * INTO tmp_table2 FROM [shift_tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] = " & month_end & " and [day_] >=  1  and [day_] <= " & day_end
                            tmp_table2_cmd.Connection = cnt
                            tmp_table2_cmd.ExecuteNonQuery()
                            If month_end - month_start <> 1 Then
                                tmp_table3_cmd.CommandText = "SELECT * INTO tmp_table3 FROM [shift_tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] >=" & month_start + 1 & " and [month_] <=" & month_end - 1
                                tmp_table3_cmd.Connection = cnt
                                tmp_table3_cmd.ExecuteNonQuery()
                            End If
                            tmp_table_cmd.Connection = cnt
                            tmp_table_cmd.CommandText = "SELECT * INTO tmp_table  FROM  tmp_table1"
                            tmp_table_cmd.ExecuteNonQuery()
                            tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table2"
                            tmp_table_cmd.ExecuteNonQuery()
                            If month_end - month_start <> 1 Then
                                tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table3"
                                tmp_table_cmd.ExecuteNonQuery()
                            End If
                        Else
                        End If
                    End If
                Else
                    If year_end - year_start = 1 Then
                        DaysInMonth = System.DateTime.DaysInMonth(year_start + 2000, month_start)
                        tmp_table1_cmd.CommandText = "SELECT * INTO tmp_table1 FROM [shift_tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] = " & month_start & " and [day_] >=  " & day_start & " and [day_] <= " & DaysInMonth
                        tmp_table1_cmd.Connection = cnt
                        tmp_table1_cmd.ExecuteNonQuery()
                        tmp_table3_cmd.CommandText = "SELECT * INTO tmp_table3 FROM [shift_tbl_" & active_machines(i) & "] WHERE [year_] = " & year_start & "and [month_] >=" & month_start + 1 & " and [month_] <= 12 "
                        tmp_table3_cmd.Connection = cnt
                        tmp_table3_cmd.ExecuteNonQuery()
                        tmp_table_cmd.Connection = cnt
                        tmp_table_cmd.CommandText = "SELECT * INTO tmp_table  FROM  tmp_table1"
                        tmp_table_cmd.ExecuteNonQuery()
                        tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table3"
                        tmp_table_cmd.ExecuteNonQuery()
                        tmp_table1_cmd.CommandText = "DROP TABLE [tmp_table1]"
                        tmp_table1_cmd.Connection = cnt
                        tmp_table1_cmd.ExecuteNonQuery()
                        tmp_table1_cmd.CommandText = "DROP TABLE [tmp_table3]"
                        tmp_table1_cmd.Connection = cnt
                        tmp_table1_cmd.ExecuteNonQuery()
                        tmp_table1_cmd.CommandText = "SELECT * INTO tmp_table1 FROM [shift_tbl_" & active_machines(i) & "] WHERE [year_] = " & year_end & "and [month_] >= 1 " & " and [month_] <=" & month_end - 1
                        tmp_table1_cmd.Connection = cnt
                        tmp_table1_cmd.ExecuteNonQuery()
                        tmp_table3_cmd.CommandText = "SELECT * INTO tmp_table3 FROM [shift_tbl_" & active_machines(i) & "] WHERE [year_] = " & year_end & "and [month_] =" & month_end & " and [day_] >= 1 and [day_] <=  " & day_end
                        tmp_table3_cmd.Connection = cnt
                        tmp_table3_cmd.ExecuteNonQuery()
                        tmp_table_cmd.Connection = cnt
                        tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table1"
                        tmp_table_cmd.ExecuteNonQuery()
                        tmp_table_cmd.CommandText = "INSERT INTO tmp_table  SELECT * FROM  tmp_table3"
                        tmp_table_cmd.ExecuteNonQuery()

                    Else
                        MessageBox.Show("You can generate reports within two years")
                    End If
                End If



                '*************************************************************************************************************************************************'
                '**** Availlable status
                '*************************************************************************************************************************************************'
                ReDim stat(4)
                Dim cmd_status As New OleDbCommand("SELECT DISTINCT [status] FROM [tmp_table] ", cnt)
                Dim reader_status As OleDbDataReader = cmd_status.ExecuteReader()

                stat(0) = "_CON"
                stat(1) = "_COFF"
                stat(2) = "_SETUP"
                stat(3) = "OTHER"
                'Select Other status in the pe riod 
                k = 4
                While reader_status.Read()
                    If reader_status.HasRows And reader_status.GetValue(0) <> "_CON" And reader_status.GetValue(0) <> "_COFF" And reader_status.GetValue(0) <> "_SETUP" And reader_status.GetValue(0) <> "_SH_START" And reader_status.GetValue(0) <> "_SH_END" And Strings.Mid(reader_status.GetValue(0), 1, 7) <> "_PARTNO" Then
                        ReDim Preserve stat(k + 1)
                        stat(k) = reader_status.GetValue(0)
                        k = k + 1
                    End If
                End While
                ReDim Preserve stat(k - 1)
                reader_status.Close()
                '*************************************************************************************************************************************************

                ' Progress bar
                nb_mch = UBound(active_machines) + 1
                Form4.Show()
                Form4.Label1.Text = "Generating Reports ...: " & active_machines(i) & "."
                Form4.ProgressBar1.Value = (33 * (i + 1)) / nb_mch


                '*************************************************************************************************************************************************'
                '**** Cycle times
                '*************************************************************************************************************************************************'

                'for each status: on off setup
                'stat(0) = _CON
                'stat(1) = _COFF
                'stat(2) = _SETUP
                'stat(3) = OTHER

                For k = 0 To UBound(stat)
                    Dim cmd As New OleDbCommand("SELECT SUM(cycletime) FROM [tmp_table] WHERE [status] = '" & stat(k) & "'", cnt)
                    Dim reader As OleDbDataReader = cmd.ExecuteReader()

                    final_time = 0

                    While reader.Read()
                        If reader.HasRows And Not reader.IsDBNull(0) Then
                            ' final_time = final_time + reader.GetValue(6)
                            final_time = final_time + reader.GetValue(0)
                        End If
                    End While
                    reader.Close()

                    If k < 3 Then
                        If final_time > 0 Then
                            final_times(k) = final_time
                            If column_ = 1 Then periode_Cycles_times(k, i) = final_time
                            total1 = total1 + final_time
                        End If
                    Else
                        final_times(3) = final_times(3) + final_time
                        total1 = total1 + final_time
                    End If
                Next k


                If column_ = 1 Then periode_Cycles_times(3, i) = final_times(3)
                If column_ = 1 Then periode_Cycles_times(4, i) = total1
                ' percentage :
                If total1 > 0 Then
                    For indx = 0 To 3
                        general_array_weekly(indx, column_, i) = (100 * final_times(indx) / total1).ToString("00.00")
                    Next
                    general_array_weekly(4, column_, i) = (100 * (final_times(0) + final_times(1) + final_times(2) + final_times(3)) / total1).ToString("00.00")
                Else
                    For indx = 0 To 4
                        general_array_weekly(indx, column_, i) = 0
                    Next
                End If

                total1 = 0
                For indx = 0 To 4
                    final_times(indx) = 0
                Next


                If Config_report.DTP_StartDate.Value.Date = Config_report.DTB_EndDate.Value.Date Then
                    Date_ = Config_report.DTP_StartDate.Value.Day & " " & SetupForm.mois_(Config_report.DTP_StartDate.Value.Month) & " " & Config_report.DTP_StartDate.Value.Year
                Else
                    If Config_report.DTP_StartDate.Value.Day = 1 And Config_report.DTB_EndDate.Value.Day = System.DateTime.DaysInMonth(Config_report.DTB_EndDate.Value.Year, Config_report.DTB_EndDate.Value.Month) Then
                        Date_ = SetupForm.mois_(Config_report.DTP_StartDate.Value.Month) & " " & Config_report.DTP_StartDate.Value.Year
                    Else
                        If Config_report.DTP_StartDate.Value.Year = Config_report.DTB_EndDate.Value.Year Then

                            Date_ = Config_report.DTP_StartDate.Value.Day & " " & SetupForm.mois_(Config_report.DTP_StartDate.Value.Month) & " to " & Config_report.DTB_EndDate.Value.Day & " " & SetupForm.mois_(Config_report.DTB_EndDate.Value.Month) & " " & (Config_report.DTB_EndDate.Value.Year)
                        Else
                            Date_ = Config_report.DTP_StartDate.Value.Day & " " & SetupForm.mois_(Config_report.DTP_StartDate.Value.Month) & " " & (Config_report.DTP_StartDate.Value.Year) & " to " & Config_report.DTB_EndDate.Value.Day & " " & SetupForm.mois_(Config_report.DTB_EndDate.Value.Month) & " " & (Config_report.DTB_EndDate.Value.Year)
                        End If
                    End If
                End If

                'Date_ = dt1.Day.ToString & "-" & dt1.Month.ToString & "-" & dt1.Year.ToString & "__" & dt2.Day.ToString & "-" & dt2.Month.ToString & "-" & dt2.Year.ToString

                general_array_weekly(0, 0, i) = active_machines(i)
                general_array_weekly(1, 0, i) = day_start.ToString & " " & SetupForm.mois_(month_start) & " " & year_start.ToString
                ' general_array_weekly(2, 0, i) = Form3.DateTimePicker1.Value.Date.Day.ToString & " " & SetupForm.mois_(Form3.DateTimePicker1.Value.Date.Month) & " to " & Form3.DateTimePicker2.Value.Date.Day.ToString & " " & SetupForm.mois_(Form3.DateTimePicker2.Value.Date.Month) & " " & Form3.DateTimePicker2.Value.Date.Year.ToString
                general_array_weekly(2, 0, i) = Date_

                'Periode = 6 : Weeks '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Periode = 1 : Days ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                If Config_report.days = 1 Then

                    If day_end > 1 Then
                        day_start = day_end - 1
                        month_start = month_end
                        year_start = year_end
                    Else
                        If month_end <> 1 Then
                            month_start = month_end - 1
                            year_start = year_end
                            day_start = System.DateTime.DaysInMonth(year_start + 2000, month_start)
                        Else
                            month_start = 12
                            year_start = year_end - 1
                            day_start = System.DateTime.DaysInMonth(year_start + 2000, month_start)
                        End If
                    End If


                    day_end = day_start
                    month_end = month_start
                    year_end = year_start

                    dates_(0, column_, i) = day_start
                    dates_(1, column_, i) = day_end
                    dates_(2, column_, i) = month_start
                    dates_(3, column_, i) = month_end
                    dates_(4, column_, i) = year_start
                    dates_(5, column_, i) = year_end


                Else

                    If first_ = True Then
                        If day_end >= Config_report.DTB_EndDate.Value.DayOfWeek + Val(Reporting_application.week_(1)) Then
                            day_end = day_end - Config_report.DTB_EndDate.Value.DayOfWeek + Val(Reporting_application.week_(1))
                            If day_end > System.DateTime.DaysInMonth(year_end + 2000, month_end) Then
                                day_end = System.DateTime.DaysInMonth(year_end + 2000, month_end)
                            End If
                        Else
                            If month_end <> 1 Then
                                month_end = month_end - 1
                                day_end = System.DateTime.DaysInMonth(year_end + 2000, month_end) - Config_report.DTB_EndDate.Value.DayOfWeek + Val(Reporting_application.week_(1)) + 1
                            Else
                                month_end = 12
                                year_end = year_end - 1
                                day_end = System.DateTime.DaysInMonth(year_end + 2000, month_end) - Config_report.DTB_EndDate.Value.DayOfWeek + Val(Reporting_application.week_(1)) + 1
                            End If
                        End If

                        day_start = day_end - Val(Reporting_application.week_(1)) + 1

                        If day_start < System.DateTime.DaysInMonth(year_start + 2000, month_start) Then
                        Else
                            If month_start <> 12 Then
                                month_start = month_start - 1
                                day_start = day_start - System.DateTime.DaysInMonth(year_start + 2000, month_start)
                            Else
                                month_start = 1
                                year_start = year_start - 1
                                day_start = day_start - System.DateTime.DaysInMonth(year_start + 2000, month_start)
                            End If
                        End If

                        first_ = False
                    Else
                        If day_end > 7 Then
                            day_end = day_end - 7
                        Else
                            month_end = month_end - 1

                            If month_end = 0 Then
                                month_end = 12
                                year_end = year_end - 1
                            End If
                            day_end = System.DateTime.DaysInMonth(year_end + 2000, month_end) - 7 + day_end
                        End If

                        If day_start > 7 Then
                            day_start = day_start - 7
                        Else
                            month_start = month_start - 1

                            If month_start = 0 Then
                                month_start = 12
                                year_start = year_start - 1
                            End If
                            day_start = System.DateTime.DaysInMonth(year_start + 2000, month_start) - 7 + day_start
                        End If
                    End If

                    dates_(0, column_, i) = day_start
                    dates_(1, column_, i) = day_end
                    dates_(2, column_, i) = month_start
                    dates_(3, column_, i) = month_end
                    dates_(4, column_, i) = year_start
                    dates_(5, column_, i) = year_end
                End If
            Next column_
            column_ = 0
no_loop:
            i = i + 1
            ReDim Preserve general_array_weekly(5, 90, i + 1)
            ReDim Preserve periode_Cycles_times(5, i + 1)
            ReDim Preserve dates_(6, 90, i + 1)
        Next '/item /machine ========================================================================================================
        '============================================================================================================================

        ReDim Preserve general_array_weekly(5, 90, i)
        ReDim Preserve dates_(6, 90, i)

        cnt.Close()
        Form4.Close()
Fin:
    End Sub

 


    Function uptimeToDHMS(ByVal inSeconds As Integer) As String
        uptimeToDHMS = ""
        Dim seconds As Integer
        seconds = inSeconds Mod 60
        inSeconds = (inSeconds - seconds) / 60
        Dim minutes As Integer
        minutes = inSeconds Mod 60
        inSeconds = (inSeconds - minutes) / 60
        Dim hours As Integer
        hours = inSeconds Mod 24
        inSeconds = (inSeconds - hours) / 24
        Dim days As Integer
        days = inSeconds
        hours = hours + days * 24

        If seconds = 59 Then
            seconds = 0
            minutes = minutes + 1
        End If

        If minutes = 59 Or minutes = 60 Then
            minutes = 0
            hours = hours + 1
        End If

        If minutes < 10 Then
            If hours < 10 Then
                uptimeToDHMS = "0" & hours & ":" & "0" & minutes '& ":" ' _
            Else
                uptimeToDHMS = hours & ":" & "0" & minutes '& ":" ' _
            End If
        Else
            If hours < 10 Then
                uptimeToDHMS = "0" & hours & ":" & minutes
            Else
                uptimeToDHMS = hours & ":" & minutes '& ":" ' _
            End If
        End If

    End Function

    Public Sub Fill_Combo_Form9(general_array_weekly As String(,,), general_array As String(,,))
        Dim i As Integer


        If Report_BarChart.Visible = True And Report_BarChart.consolidated = False Then
            Report_BarChart.Close()
        End If


        For i = 0 To (UBound(general_array_weekly, 3) - 1)
            Report_BarChart.CB_Report.Items.Add(general_array_weekly(0, 0, i) & " : " & general_array_weekly(2, 0, i))
        Next

        If Report_BarChart.consolidated = False Then Report_BarChart.Show()
    End Sub

    Public Sub fill_form9(general_array_weekly(,,) As String, ThirdDim As Integer)

        ''1period
        'Form9.Label28.Text = Math.Round(Val(general_array_weekly(0, 1, ThirdDim)))
        'Form9.Label27.Text = Math.Round(Val(general_array_weekly(1, 1, ThirdDim)))
        'Form9.Label26.Text = Math.Round(Val(general_array_weekly(2, 1, ThirdDim)))
        'Form9.Label22.Text = Math.Round(Val(general_array_weekly(3, 1, ThirdDim)))
        'Form9.Label21.Text = Math.Round(Val(general_array_weekly(4, 1, ThirdDim)))

        '1period
        Report_BarChart.LBL_CycleOn_Period.Text = (Val(general_array_weekly(0, 1, ThirdDim))).ToString("00.00")
        Report_BarChart.LBL_CycleOff_Period.Text = (Val(general_array_weekly(1, 1, ThirdDim))).ToString("00.00")
        Report_BarChart.LBL_Setup_Period.Text = (Val(general_array_weekly(2, 1, ThirdDim))).ToString("00.00")
        Report_BarChart.LBL_Other_Period.Text = (Val(general_array_weekly(3, 1, ThirdDim))).ToString("00.00")
        Report_BarChart.LBL_Total_Period.Text = (Val(general_array_weekly(4, 1, ThirdDim))).ToString("00.00")

    End Sub

    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    Me.Height = 400
    'End Sub

    'Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
    '    weeks_of = 7
    'End Sub

    'Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
    '    weeks_of = 5

    'End Sub

    'Private Sub TreeView1_Afterselect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeView1.AfterCheck
    '    ' RemoveHandler TreeView1.AfterCheck, AddressOf TreeView1_Aftercheck

    '    Call CheckAllChildNodes(e.Node)
    '    Button1.Enabled = True


    '    ' If e.Node.Checked Then
    '    '    If e.Node.Parent Is Nothing = False Then
    '    '        Dim allChecked As Boolean = True
    '    '        Call IsEveryChildChecked(e.Node.Parent, allChecked)
    '    '        If allChecked Then
    '    '            e.Node.Parent.Checked = True
    '    '            Call ShouldParentsBeChecked(e.Node.Parent)
    '    '        End If
    '    '    End If
    '    'Else
    '    '    Dim parentNode As TreeNode = e.Node.Parent
    '    '    While parentNode Is Nothing = False
    '    '        parentNode.Checked = False
    '    '        parentNode = parentNode.Parent
    '    '    End While
    '    'End If

    '    ' AddHandler TreeView1.AfterCheck, AddressOf TreeView1_Aftercheck
    'End Sub

    'Private Sub CheckAllChildNodes(ByVal parentNode As TreeNode)
    '    For Each childNode As TreeNode In parentNode.Nodes
    '        childNode.Checked = parentNode.Checked
    '        CheckAllChildNodes(childNode)
    '    Next
    'End Sub

    'Private Sub IsEveryChildChecked(ByVal parentNode As TreeNode, ByRef checkValue As Boolean)
    '    For Each node As TreeNode In parentNode.Nodes
    '        Call IsEveryChildChecked(node, checkValue)
    '        If Not node.Checked Then
    '            checkValue = False
    '        End If
    '    Next
    'End Sub

    'Private Sub ShouldParentsBeChecked(ByVal startNode As TreeNode)
    '    If startNode.Parent Is Nothing = False Then
    '        Dim allChecked As Boolean = True
    '        Call IsEveryChildChecked(startNode.Parent, allChecked)
    '        If allChecked Then
    '            startNode.Parent.Checked = True
    '            Call ShouldParentsBeChecked(startNode.Parent)
    '        End If
    '    End If
    'End Sub


End Class
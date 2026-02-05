Imports System.IO
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities
Imports MySql.Data.MySqlClient

Public Class config_
    Dim isLoad As Boolean = False
    Dim Count As Integer = 0
    Dim groups_dic As Dictionary(Of String, List(Of String))
    Dim performance_timer2 As New System.Timers.Timer

    Dim changed As Boolean = False

    Dim orderBy() As String = {"Machine Name: A -> Z", "Machine Name: Z -> A", "Machine Label: A -> Z", "Machine Label: Z -> A", "eNETDNC Machine Name: A -> Z", "eNETDNC Machine Name: Z -> A"}

    Private rootpath As String = CSI_Library.CSI_Library.serverRootPath

    Public deviceId As Integer = 0

    Private Sub config_trends_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If SetupForm2.deviceId = 0 Then Return

        RemoveHandlers()

        isLoad = True

        cmbDisplayMachine.Items.Clear()
        cmbDisplayMachine.Items.Add(New KeyValuePair(Of String, String)("Name", "Machine Name"))
        cmbDisplayMachine.Items.Add(New KeyValuePair(Of String, String)("Label", "Machine Label"))
        cmbDisplayMachine.Items.Add(New KeyValuePair(Of String, String)("eNet", "eNETDNC Name"))
        cmbDisplayMachine.DisplayMember = "Value"
        cmbDisplayMachine.ValueMember = "Key"
        Try

            cmbOrderBy.DataSource = orderBy

            deviceId = SetupForm2.deviceId

            Dim dtDeviceConfig2 = MySqlAccess.GetDataTable($"SELECT * FROM CSI_database.tbl_deviceConfig2 WHERE deviceId = {deviceId}")

            If dtDeviceConfig2.Rows.Count > 0 Then

                CB_timeline.Checked = dtDeviceConfig2.Rows(0)("timeline")
                CB_activate_TREND.Checked = dtDeviceConfig2.Rows(0)("trends")
                chkDisplayByGroups.Checked = If(IsDBNull(dtDeviceConfig2.Rows(0)("DisplayByGroups")), 0, dtDeviceConfig2.Rows(0)("DisplayByGroups"))
                CB_FeedrateOver.Checked = (dtDeviceConfig2.Rows(0)("FeedrateOver") = "on")
                CB_SpindleOver.Checked = (dtDeviceConfig2.Rows(0)("SpindleOver") = "on")
                CB_RapidOver.Checked = (dtDeviceConfig2.Rows(0)("RapidOver") = "on")
                cbCount.Checked = (dtDeviceConfig2.Rows(0)("CountColumn") = "on")
                cbPartNumber.Checked = (dtDeviceConfig2.Rows(0)("PartNumberColumn") = "on")
                chkOperator.Checked = (dtDeviceConfig2.Rows(0)("Operator") = "on")
                rbOperatorName.Checked = (dtDeviceConfig2.Rows(0)("OperatorName") = "on")
                rbOperatorId.Checked = Not rbOperatorName.Checked

                rbOperatorId.Enabled = chkOperator.Checked
                rbOperatorName.Enabled = chkOperator.Checked

                cmbOrderBy.SelectedIndex = cmbOrderBy.FindStringExact(orderBy(Integer.Parse(dtDeviceConfig2.Rows(0)("OrderBy"))))
                cbUseMachineLabel.Checked = (dtDeviceConfig2.Rows(0)("UseMachineLabel") = "on")

                Dim dispMach = dtDeviceConfig2.Rows(0)("UseMachineLabel")
                If dispMach = "off" Or dispMach = "Name" Then
                    cmbDisplayMachine.SelectedIndex = 0
                ElseIf dispMach = "on" Or dispMach = "Label" Then
                    cmbDisplayMachine.SelectedIndex = 1
                ElseIf dispMach = "eNet" Then
                    cmbDisplayMachine.SelectedIndex = 2
                End If

                Dim dispPressure = dtDeviceConfig2.Rows(0)("DisplayPressure")
                If dispPressure = 0 Then
                    chkDisplayPressure.Checked = False
                    cmbDisplayPressure.SelectedIndex = -1
                    cmbDisplayPressure.Enabled = False
                Else
                    chkDisplayPressure.Checked = True
                    cmbDisplayPressure.SelectedIndex = dispPressure - 1
                    cmbDisplayPressure.Enabled = True
                End If

            End If

            AddHandler CB_FeedrateOver.CheckedChanged, AddressOf Settings_Changed
            AddHandler CB_SpindleOver.CheckedChanged, AddressOf Settings_Changed
            AddHandler CB_RapidOver.CheckedChanged, AddressOf Settings_Changed
            AddHandler cbPartNumber.CheckedChanged, AddressOf Settings_Changed
            AddHandler cbCount.CheckedChanged, AddressOf Settings_Changed
            AddHandler chkOperator.CheckedChanged, AddressOf Settings_Changed
            AddHandler cbUseMachineLabel.CheckedChanged, AddressOf Settings_Changed
            AddHandler rbOperatorName.CheckedChanged, AddressOf Settings_Changed
            AddHandler cmbOrderBy.SelectedIndexChanged, AddressOf Settings_Changed
            AddHandler cmbDisplayMachine.SelectedIndexChanged, AddressOf Settings_Changed
            AddHandler chkDisplayPressure.CheckedChanged, AddressOf Settings_Changed
            AddHandler cmbDisplayPressure.SelectedIndexChanged, AddressOf Settings_Changed
            'AddHandler rbOperatorId.CheckedChanged, AddressOf chkOperator_CheckedChanged


            cbCountFormat.DataSource = Nothing
            Dim items As Dictionary(Of String, String) = New Dictionary(Of String, String)
            Dim format As String = dtDeviceConfig2.Rows(0)("CountFormat")
            items.Add("CC", "Cycle Count (eNETDNC)")
            items.Add("PC", "Parts Count")
            items.Add("PL", "Parts Left")
            items.Add("PCRQ", "Parts Count / Required")
            items.Add("PLRQ", "Parts Left / Required")
            cbCountFormat.DisplayMember = "Value"
            cbCountFormat.ValueMember = "Key"
            cbCountFormat.DataSource = New BindingSource(items, Nothing)

            For idx As Integer = 0 To items.Count - 1
                If items.ElementAt(idx).Key = format Then
                    cbCountFormat.SelectedIndex = idx
                End If
            Next

            AddHandler cbCountFormat.SelectedIndexChanged, AddressOf cbCount_CheckedChanged

            Dim dtDeviceConfig = MySqlAccess.GetDataTable($"SELECT * FROM CSI_database.tbl_deviceConfig WHERE deviceId = {deviceId}")

            If (dtDeviceConfig.Rows.Count > 0) Then

                chkPieChart.Checked = (dtDeviceConfig.Rows(0)("piechart") = "on")
                If Not IsDBNull(dtDeviceConfig.Rows(0)("PieChartBy")) Then
                    chkChartByMachines.Checked = (dtDeviceConfig.Rows(0)("PieChartBy") = "Both" Or dtDeviceConfig.Rows(0)("PieChartBy") = "ByMachines")
                    chkChartByGroups.Checked = (dtDeviceConfig.Rows(0)("PieChartBy") = "Both" Or dtDeviceConfig.Rows(0)("PieChartBy") = "ByGroups")
                End If
            End If

            If (dtDeviceConfig.Rows(0)("piecharttime") = "Monthly") Then
                rbtnWeekly.Checked = False
                rbtnMonthly.Checked = True
            Else
                rbtnWeekly.Checked = True
                rbtnMonthly.Checked = False
            End If

            'AddHandler RB_PC_Week.CheckedChanged, AddressOf RB_PC_Week_CheckedChanged
            'AddHandler CB_byMachine.CheckedChanged, AddressOf CB_byMachine_CheckedChanged

            performance_timer2 = New System.Timers.Timer(2000) 'Every 2 seconds calculate the MAchine data For Donut and Bar Chart for Targets
            AddHandler performance_timer2.Elapsed, AddressOf Performance_timer_Ticked2
            performance_timer2.AutoReset = False
            performance_timer2.Start()

            LastCycle_CB.Checked = (dtDeviceConfig.Rows(0)("lastcycle") = "on")
            CurrentCycle_CB.Checked = (dtDeviceConfig.Rows(0)("currentcycle") = "on")
            ElapsedTime_CB.Checked = (dtDeviceConfig.Rows(0)("elapsedtime") = "on")


            Dim sqlCmd As New Text.StringBuilder()

            sqlCmd.Append($"SELECT machines FROM csi_database.tbl_devices WHERE Id = {deviceId}")
            Dim machines As String = MySqlAccess.ExecuteScalar(sqlCmd.ToString()).ToString()

            sqlCmd.Clear()
            sqlCmd.Append($"SELECT                           ")
            sqlCmd.Append($"    DISTINCT `groups`            ")
            sqlCmd.Append($"FROM                             ")
            sqlCmd.Append($"    csi_database.tbl_groups      ")
            sqlCmd.Append($"WHERE                            ")

            Dim intRes As Integer
            If Integer.TryParse(machines.Split(",")(0), intRes) Then
                sqlCmd.Append($"      machineId IN ({ machines })")
            Else
                machines = Join(machines.Split(",").Select(Function(s) $"'{s.Trim()}'").ToArray(), ",")
                sqlCmd.Append($"      machines  IN ({ machines })")
            End If

            sqlCmd.Append($"  AND users = '{ CSI_Library.CSI_Library.username }'")

            Dim dtGroups = MySqlAccess.GetDataTable(sqlCmd.ToString())

            Dim ListOfGroups As New List(Of String)
            For Each Row As DataRow In dtGroups.Rows
                Dim str As String
                str = Row("groups")
                ListOfGroups.Add(str)
            Next Row

            TV_CurrPerfGroup.Nodes.Clear()
            TV_CurrPerfGroup.Nodes.Add("All machines")

            For Each groups As String In ListOfGroups
                TV_CurrPerfGroup.Nodes.Add(groups)
            Next groups

            Dim dtDevices = MySqlAccess.GetDataTable($"SELECT `groups` from CSI_database.tbl_devices WHERE Id = {deviceId}")

            Dim tempG As String = dtDevices.Rows(0).Item(0).ToString()
            Dim dtab As List(Of String) = New List(Of String)
            Dim ind As Integer = 0

            For Each n As TreeNode In TV_CurrPerfGroup.Nodes
                If tempG.Contains(n.Text) Then
                    n.Checked = True
                Else
                    n.Checked = False
                End If
            Next n

            Dim ColOk = CheckColumns("DisplayWhat", "tbl_deviceConfig", "varchar(255)")

            If ColOk = True Then

                Dim dTable_message1700 = MySqlAccess.GetDataTable($"SELECT DisplayWhat from CSI_database.tbl_deviceConfig WHERE deviceId = {deviceId}")

                If (dTable_message1700.Rows(0)(0).ToString() = "Both") Then
                    CB_DisplayPerf.Checked = True
                    chkDisplayGroupTarget.Checked = True
                ElseIf (dTable_message1700.Rows(0)(0).ToString() = "DisplayTarget") Then
                    CB_DisplayPerf.Checked = False
                    chkDisplayGroupTarget.Checked = True
                ElseIf (dTable_message1700.Rows(0)(0).ToString() = "DisplayPerf") Then
                    CB_DisplayPerf.Checked = True
                    chkDisplayGroupTarget.Checked = False
                Else
                    CB_DisplayPerf.Checked = False
                    chkDisplayGroupTarget.Checked = False
                End If
            End If

            AddHandler CB_DisplayPerf.CheckedChanged, AddressOf CB_DisplayPerf_CheckedChanged

            groups_dic = SetupForm2.LoadGroups()

            Try
                While Not tempG.IndexOf(",") = -1
                    dtab.Add(tempG.Substring(ind, tempG.IndexOf(",")))
                    tempG = tempG.Substring(tempG.IndexOf(",") + 2, tempG.Length - tempG.IndexOf(",") - 2)
                    'ind += 2
                End While

                dtab.Add(tempG)

                For Each r As String In dtab
                    For Each n As TreeNode In TV_CurrPerfGroup.Nodes
                        If r = n.Text Then
                            n.Checked = True
                        End If
                    Next
                Next

            Catch ex As Exception

            End Try
        Catch ex As Exception

            MsgBox("Could not load the configuration for this dashboard : " + ex.Message)
        Finally

            AddHandlers()
            isLoad = False
        End Try

    End Sub


    Public Sub Performance_timer_Ticked2()
        Dim objSeerviceLib As New CSIFlexServerService.ServiceLibrary
        If Count <= 3 Then
            Try
                performance_timer2.Stop()
                objSeerviceLib.Read_perf_from_db()
            Catch ex As Exception
                CSI_Lib.LogServiceError("Error in Performance_timer_Ticked2() while updating Donut and Machine Target data :" & ex.Message() & " And Stack Trace : " & ex.StackTrace(), 1)
            End Try
            Count = Count + 1
            performance_timer2.Start()
        Else
            performance_timer2.Stop()
            Count = 0
        End If
    End Sub


    Private Sub RemoveHandlers()
        RemoveHandler LastCycle_CB.CheckedChanged, AddressOf Me.LastCycle_CB_CheckedChanged
        RemoveHandler CurrentCycle_CB.CheckedChanged, AddressOf Me.CurrentCycle_CB_CheckedChanged
        RemoveHandler ElapsedTime_CB.CheckedChanged, AddressOf Me.ElapsedTime_CB_CheckedChanged
        RemoveHandler chkPieChart.CheckedChanged, AddressOf Me.PieChart_CB_CheckedChanged
        RemoveHandler rbtnWeekly.CheckedChanged, AddressOf Me.RB_PC_Week_CheckedChanged
        RemoveHandler TV_CurrPerfGroup.AfterCheck, AddressOf Me.TV_CurrPerfGroup_AfterSelect
        RemoveHandler CB_timeline.CheckedChanged, AddressOf Me.Settings_Changed
        RemoveHandler CB_activate_TREND.CheckedChanged, AddressOf Me.Settings_Changed
        '  RemoveHandler TB_scale.TextChanged, AddressOf Me.TB_scale_TextChanged
        RemoveHandler BTN_Prev.Click, AddressOf Me.BTN_Prev_Click
        RemoveHandler chkDisplayByGroups.CheckedChanged, AddressOf Me.Settings_Changed
        RemoveHandler chkChartByGroups.CheckedChanged, AddressOf Me.chkChartBy_CheckedChanged
        RemoveHandler chkChartByMachines.CheckedChanged, AddressOf Me.chkChartBy_CheckedChanged
        RemoveHandler CB_DisplayPerf.CheckedChanged, AddressOf Me.CB_DisplayPerf_CheckedChanged
        RemoveHandler chkDisplayGroupTarget.CheckedChanged, AddressOf Me.chkDisplayGroupTarget_CheckedChanged
        RemoveHandler cbCountFormat.SelectedIndexChanged, AddressOf Settings_Changed

    End Sub

    Private Sub AddHandlers()
        AddHandler LastCycle_CB.CheckedChanged, AddressOf Me.LastCycle_CB_CheckedChanged
        AddHandler CurrentCycle_CB.CheckedChanged, AddressOf Me.CurrentCycle_CB_CheckedChanged
        AddHandler ElapsedTime_CB.CheckedChanged, AddressOf Me.ElapsedTime_CB_CheckedChanged
        AddHandler chkPieChart.CheckedChanged, AddressOf Me.PieChart_CB_CheckedChanged
        AddHandler rbtnWeekly.CheckedChanged, AddressOf Me.Settings_Changed
        AddHandler TV_CurrPerfGroup.AfterCheck, AddressOf Me.TV_CurrPerfGroup_AfterSelect
        AddHandler CB_timeline.CheckedChanged, AddressOf Me.Settings_Changed
        AddHandler CB_activate_TREND.CheckedChanged, AddressOf Me.Settings_Changed
        '   AddHandler TB_scale.TextChanged, AddressOf Me.TB_scale_TextChanged
        AddHandler BTN_Prev.Click, AddressOf Me.BTN_Prev_Click
        AddHandler chkDisplayByGroups.CheckedChanged, AddressOf Me.Settings_Changed
        AddHandler chkChartByGroups.CheckedChanged, AddressOf Me.chkChartBy_CheckedChanged
        AddHandler chkChartByMachines.CheckedChanged, AddressOf Me.chkChartBy_CheckedChanged
        AddHandler CB_DisplayPerf.CheckedChanged, AddressOf Me.CB_DisplayPerf_CheckedChanged
        AddHandler chkDisplayGroupTarget.CheckedChanged, AddressOf Me.Settings_Changed

    End Sub


    Private Sub Settings_Changed(sender As Object, e As EventArgs)

        Dim dispMch As KeyValuePair(Of String, String) = cmbDisplayMachine.SelectedItem

        Dim sqlCmd As New Text.StringBuilder()
        sqlCmd.Append($"UPDATE                                                                    ")
        sqlCmd.Append($"    csi_database.tbl_deviceconfig2                                        ")
        sqlCmd.Append($" SET                                                                      ")
        sqlCmd.Append($"    timeline         =  { CB_timeline.Checked }                         , ")
        sqlCmd.Append($"    trends           =  { CB_activate_TREND.Checked }                   , ")
        sqlCmd.Append($"    DisplayByGroups  =  { chkDisplayByGroups.Checked }                  , ")
        sqlCmd.Append($"    FeedrateOver     = '{ If(CB_FeedrateOver.Checked, "on", "off") }'   , ")
        sqlCmd.Append($"    SpindleOver      = '{ If(CB_SpindleOver.Checked, "on", "off") }'    , ")
        sqlCmd.Append($"    RapidOver        = '{ If(CB_RapidOver.Checked, "on", "off") }'      , ")
        sqlCmd.Append($"    Operator         = '{ If(chkOperator.Checked, "on", "off") }'       , ")
        sqlCmd.Append($"    OperatorName     = '{ If(rbOperatorName.Checked, "on", "off") }'    , ")
        sqlCmd.Append($"    PartNumberColumn = '{ If(cbPartNumber.Checked, "on", "off") }'      , ")
        sqlCmd.Append($"    CountColumn      = '{ If(cbCount.Checked, "on", "off") }'           , ")
        sqlCmd.Append($"    UseMachineLabel  = '{ dispMch.Key }'                                , ")
        sqlCmd.Append($"    CountFormat      = '{ cbCountFormat.SelectedValue }'                , ")
        sqlCmd.Append($"    OrderBy          =  { cmbOrderBy.SelectedIndex }                    , ")
        sqlCmd.Append($"    DisplayPressure  =  { cmbDisplayPressure.SelectedIndex + 1 }          ")
        sqlCmd.Append($" WHERE ")
        sqlCmd.Append($"    deviceId = { deviceId } ")

        Try
            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())
        Catch ex As Exception
            Log.Error("Updatting tbl_deviceconfig2", ex)
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub


    Private Sub cbUseMachineLabel_CheckedChanged(sender As Object, e As EventArgs)

        Try
            MySqlAccess.ExecuteNonQuery($"update csi_database.tbl_deviceconfig2 SET UseMachineLabel = '{ If(cbUseMachineLabel.Checked, "on", "off") }' WHERE deviceId = {deviceId}")
        Catch ex As Exception
            Console.Out.Write(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub CB_FeedrateOver_CheckedChanged(sender As Object, e As EventArgs)

        Try
            MySqlAccess.ExecuteNonQuery($"update csi_database.tbl_deviceconfig2 SET FeedrateOver = '{ If(CB_FeedrateOver.Checked, "on", "off") }' WHERE deviceId = {deviceId}")
        Catch ex As Exception
            Console.Out.Write(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub CB_SpindleOver_CheckedChanged(sender As Object, e As EventArgs)

        Try
            MySqlAccess.ExecuteNonQuery($"update csi_database.tbl_deviceconfig2 SET SpindleOver = '{ If(CB_SpindleOver.Checked, "on", "off") }' WHERE deviceId = {deviceId}")
        Catch ex As Exception
            Console.Out.Write(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub CB_RapidOver_CheckedChanged(sender As Object, e As EventArgs)

        Try
            MySqlAccess.ExecuteNonQuery($"update csi_database.tbl_deviceconfig2 SET RapidOver = '{ If(CB_RapidOver.Checked, "on", "off") }' WHERE deviceId = {deviceId}")
        Catch ex As Exception
            Console.Out.Write(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub cbPartNumber_CheckedChanged(sender As Object, e As EventArgs)

        Try
            MySqlAccess.ExecuteNonQuery($"update csi_database.tbl_deviceconfig2 SET PartNumberColumn = '{ If(cbPartNumber.Checked, "on", "off") }' WHERE deviceId = {deviceId}")
        Catch ex As Exception
            Console.Out.Write(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub chkOperator_CheckedChanged(sender As Object, e As EventArgs)

        rbOperatorId.Enabled = chkOperator.Checked
        rbOperatorName.Enabled = chkOperator.Checked

        Try
            MySqlAccess.ExecuteNonQuery($"update csi_database.tbl_deviceconfig2 SET Operator = '{ If(chkOperator.Checked, "on", "off") }', OperatorName = '{ If(rbOperatorName.Checked, "on", "off") }'  WHERE deviceId = { deviceId }")
        Catch ex As Exception
            Console.Out.Write(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub cbCount_CheckedChanged(sender As Object, e As EventArgs)

        Try
            cbCountFormat.Enabled = cbCount.Checked

            MySqlAccess.ExecuteNonQuery($"update csi_database.tbl_deviceconfig2 SET CountColumn = '{ If(cbCount.Checked, "on", "off") }', CountFormat = '{ cbCountFormat.SelectedValue }' WHERE deviceId = {deviceId}")
        Catch ex As Exception
            Console.Out.Write(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub LastCycle_CB_CheckedChanged(sender As Object, e As EventArgs) Handles LastCycle_CB.CheckedChanged

        Try
            MySqlAccess.ExecuteNonQuery($"update CSI_database.tbl_deviceConfig SET lastcycle = '{ If(LastCycle_CB.Checked, "on", "off") }' WHERE deviceId = {deviceId}")
        Catch ex As Exception
            Console.Out.Write(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub CurrentCycle_CB_CheckedChanged(sender As Object, e As EventArgs) Handles CurrentCycle_CB.CheckedChanged

        Try
            MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_deviceConfig SET Currentcycle = '{ If(CurrentCycle_CB.Checked, "on", "off") }' WHERE DeviceId = { deviceId }")
        Catch ex As Exception
            Console.Out.Write(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub ElapsedTime_CB_CheckedChanged(sender As Object, e As EventArgs) Handles ElapsedTime_CB.CheckedChanged

        Try
            MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_deviceConfig SET ElapsedTime = '{ If(ElapsedTime_CB.Checked, "on", "off") }' WHERE DeviceId = { deviceId }")
        Catch ex As Exception
            Console.Out.Write(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub PieChart_CB_CheckedChanged(sender As Object, e As EventArgs) Handles chkPieChart.CheckedChanged

        Try
            MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_deviceConfig SET piechart = '{ If(chkPieChart.Checked, "on", "off") }' WHERE DeviceId = { deviceId }")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub RB_PC_Week_CheckedChanged(sender As Object, e As EventArgs)

        Try
            MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_deviceConfig SET piecharttime = '{ If(rbtnWeekly.Checked, "Weekly", "Monthly") }' WHERE deviceId = {deviceId}")
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Public Sub TV_CurrPerfGroup_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TV_CurrPerfGroup.AfterCheck

        If e.Node.Checked = True Then
            Try
                If groups_dic IsNot Nothing Then
                    If groups_dic(e.Node.Text).Count = 0 Then
                        MsgBox("There are no machines in this group.")

                    ElseIf groups_dic(e.Node.Text).Count = 1 Then

                        If groups_dic(e.Node.Text)(0) = "" Then
                            MsgBox("There are no machines in this group.")
                        End If
                    End If
                End If
            Catch
            End Try
        End If

        Dim value As String = ""
        Dim i As Integer = 0
        For Each n As TreeNode In TV_CurrPerfGroup.Nodes
            If n.Checked Then
                If (i > 0) Then
                    value += ", "
                End If
                i += 1
                value += n.Text
            End If
        Next

        MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_devices SET `groups` = '{ value }' WHERE id = { deviceId }")

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub CB_timeline_CheckedChanged(sender As Object, e As EventArgs) Handles CB_timeline.CheckedChanged

        MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_deviceconfig2 SET timeline = { CB_timeline.Checked } WHERE deviceId = { deviceId }")

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub CB_activate_TREND_CheckedChanged(sender As Object, e As EventArgs) Handles CB_activate_TREND.CheckedChanged

        MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_deviceconfig2 SET trends = { CB_activate_TREND.Checked } WHERE deviceId = { deviceId }")

        SetupForm2.RefreshDevice(deviceId)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GeneralSettings.ShowDialog()
    End Sub

    Private Sub BTN_applyscale_Click(sender As Object, e As EventArgs) Handles BTN_applyscale.Click
        TextBrowser_Scale.ShowDialog()
        'Updatescale()
    End Sub

    Private Sub BTN_Prev_Click(sender As Object, e As EventArgs) Handles BTN_Prev.Click
        ' --using Install-Package SSH.NET
        Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try

            cntsql.Open()

            Dim mysql_str As String = "SELECT devicetype FROM CSI_database.tbl_deviceconfig2 where IP_adress = '" & SetupForm2.IP_TB.Text & "' and name = '" + SetupForm2.txtDeviceName.Text + "'"
            Dim cmd_devicetype As New MySqlCommand(mysql_str, cntsql)
            Dim mysqlReader23 As MySqlDataReader = cmd_devicetype.ExecuteReader
            Dim dTable_type As New DataTable()
            dTable_type.Load(mysqlReader23)
            cntsql.Close()

            Dim type As String = ""
            If dTable_type.Rows.Count <> 0 Then
                type = dTable_type.Rows(0).Item(0)
            End If
            If type = "LR1" Then
                LR1_preview.ShowDialog()
            Else
                MessageBox.Show("The preview is available only for LR1s")
            End If

        Catch ex As Exception
            If cntsql.State = ConnectionState.Open Then cntsql.Close()
            MsgBox("Could not display a preview : " & ex.Message)
        End Try

    End Sub

    Public Function CheckColumns(Name As String, TableName As String, Type As String) As Boolean
        Dim command As String = " Select * From CSI_database." + TableName
        Dim temp_table As DataTable = New DataTable
        Dim MySqlCmmd As New MySqlCommand
        Dim reader As MySqlDataReader

        Dim sqlConn As MySqlConnection = New MySqlConnection(CSI_Lib.MySqlConnectionString)

        Try
            sqlConn.Open()

            If sqlConn.State <> 1 Then
                MsgBox("No Connecion with the CSIFlex Database")
                Return Nothing
            Else

                MySqlCmmd.CommandText = command
                MySqlCmmd.Connection = sqlConn

                reader = MySqlCmmd.ExecuteReader
                temp_table.Load(reader)
                sqlConn.Close()
            End If

            For Each Col As DataColumn In temp_table.Columns
                If Col.ColumnName = Name Then
                    Return True
                End If
            Next
            sqlConn.Open()
            command = " ALTER TABLE CSI_database." + TableName + " ADD COLUMN " + Name + " " + Type + " NULL;"
            MySqlCmmd.CommandText = command
            MySqlCmmd.Connection = sqlConn
            MySqlCmmd.ExecuteNonQuery()
            sqlConn.Close()

            Return True
        Catch ex As Exception
            If sqlConn.State = ConnectionState.Open Then sqlConn.Close()
            MsgBox("Could not update the database to add an option. : " & ex.Message)
            Return False
        End Try
    End Function

    Private Sub save(what As String, options As String)

        If what = "ByGroups" Then
            If Not Directory.Exists(rootpath + "/sys/generaldashb/") Then Directory.CreateDirectory(rootpath + "/sys/generaldashb/")

            If File.Exists((rootpath + "/sys/generaldashb/" & "ByGroups" & ".csys")) Then File.Delete(rootpath + "/sys/generaldashb/" & "ByGroups" & ".csys")

            Using fs As New StreamWriter(rootpath + "/sys/generaldashb/" & "ByGroups" & ".csys")
                fs.Write(options)
                fs.Close()
            End Using
        End If

        SetupForm2.RefreshDevice(deviceId)
    End Sub

    Private Sub chkChartBy_CheckedChanged(sender As Object, e As EventArgs)

        If Not chkChartByMachines.Checked And Not chkChartByGroups.Checked Then
            chkChartByMachines.Checked = True
            MsgBox("You have to choose to display the Pie Chart by Machine or by group.")
        End If

        Try
            Dim By As String = ""
            If chkChartByMachines.Checked = False And chkChartByGroups.Checked = True Then By = "ByGroups"
            If chkChartByMachines.Checked = True And chkChartByGroups.Checked = False Then By = "ByMachines"
            If chkChartByMachines.Checked = True And chkChartByGroups.Checked = True Then By = "Both"

            MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_deviceConfig SET PieChartBy = '{ By }' WHERE deviceId = { deviceId }")

            SetupForm2.RefreshDevice(deviceId)

        Catch ex As Exception
            MsgBox("Could not update the Pie Chart setting : " & ex.Message)
        End Try

    End Sub

    Private Sub CB_DisplayPerf_CheckedChanged(sender As Object, e As EventArgs)

        Try
            Dim Disp As String = ""
            If CB_DisplayPerf.Checked = False And chkDisplayGroupTarget.Checked = True Then Disp = "DisplayTarget"
            If CB_DisplayPerf.Checked = True And chkDisplayGroupTarget.Checked = False Then Disp = "DisplayPerf"
            If CB_DisplayPerf.Checked = False And chkDisplayGroupTarget.Checked = False Then Disp = "none"
            If CB_DisplayPerf.Checked = True And chkDisplayGroupTarget.Checked = True Then Disp = "Both"

            Dim ColOk As Boolean = CheckColumns("DisplayWhat", "tbl_deviceConfig", "varchar(255)")
            If ColOk = True Then
                Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                cntsql.Open()
                Dim cmdsql2 As New MySqlCommand("update CSI_database.tbl_deviceConfig SET DisplayWhat = '" & Disp & "' WHERE IP = '" & SetupForm2.IP_TB.Text & "' and  name = '" & SetupForm2.txtDeviceName.Text & "'", cntsql)
                cmdsql2.ExecuteNonQuery()
                cntsql.Close()
            End If

            SetupForm2.RefreshDevice(deviceId)
        Catch ex As Exception
            MsgBox("Could not update the Pie Chart setting : " & ex.Message)
        End Try
    End Sub

    Private Sub chkDisplayGroupTarget_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisplayGroupTarget.CheckedChanged

        Try
            Dim Disp As String = ""
            If CB_DisplayPerf.Checked = False And chkDisplayGroupTarget.Checked = True Then Disp = "DisplayTarget"
            If CB_DisplayPerf.Checked = True And chkDisplayGroupTarget.Checked = False Then Disp = "DisplayPerf"
            If CB_DisplayPerf.Checked = False And chkDisplayGroupTarget.Checked = False Then Disp = "none"
            If CB_DisplayPerf.Checked = True And chkDisplayGroupTarget.Checked = True Then Disp = "Both"

            Dim ColOk As Boolean = CheckColumns("DisplayWhat", "tbl_deviceConfig", "varchar(255)")
            If ColOk = True Then
                Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                cntsql.Open()
                Dim cmdsql2 As New MySqlCommand("update CSI_database.tbl_deviceConfig SET DisplayWhat = '" & Disp & "' WHERE IP = '" & SetupForm2.IP_TB.Text & "' and  name = '" & SetupForm2.txtDeviceName.Text & "'", cntsql)
                cmdsql2.ExecuteNonQuery()
                cntsql.Close()
            End If

            SetupForm2.RefreshDevice(deviceId)
        Catch ex As Exception
            MsgBox("Could not update the Pie Chart setting : " & ex.Message)
        End Try

    End Sub

    Private Sub chkDisplayPressure_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisplayPressure.CheckedChanged

        cmbDisplayPressure.Enabled = chkDisplayPressure.Checked
        If chkDisplayPressure.Checked Then
            cmbDisplayPressure.SelectedIndex = 1
        Else
            cmbDisplayPressure.SelectedIndex = -1
        End If

    End Sub
End Class
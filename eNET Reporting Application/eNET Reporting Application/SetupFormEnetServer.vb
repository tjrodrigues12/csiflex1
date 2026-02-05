Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Windows
Imports CSIFLEX.Database.Access
Imports CSIFLEX.eNetLibrary
Imports CSIFLEX.eNetLibrary.Data
Imports CSIFLEX.Server.Library
Imports CSIFLEX.Server.Settings
Imports CSIFLEX.Utilities
Imports MySql.Data.MySqlClient

Partial Public Class SetupForm2

    Private CSIFlexServiceLib As New CSIFlexServerService.ServiceLibrary

    Private TargetsChanged As Boolean = False
    Private originalEnetFolder As String = ""
    Private originalEnetHttpIp As String = ""
    Private ancient_name As String

    Private DisplayMachineList As New Dictionary(Of String, String)
    Private AllMachines As New Dictionary(Of String, MachineData)
    Private clt As New CSI_Library.EnetClient
    Private selected_node_1 As TreeNode

    Private Sub LoadEnetSettings()

        Dim path = CSI_Library.CSI_Library.serverRootPath

        lblEnetCheckResult.Text = ""
        lblEnetFolderStatus.Text = ""
        btnEnetSaveChanges.Enabled = False
        btnEnetSaveChanges.BackColor = Color.Transparent

        If pnlMachines.Location.Y = 5 Then Return

        grpEnetSettings.Visible = False
        pnlMachines.Location = New Drawing.Point(8, 5)
        pnlMachines.Size = New Drawing.Size(pnlMachines.Size.Width, pnlMachines.Size.Height + 225)

        originalEnetFolder = ServerSettings.EnetFolder
        txtEnetFolder.Text = originalEnetFolder
        txtEnetFolder_TextChanged(txtEnetFolder, New EventArgs())

        originalEnetHttpIp = ServerSettings.EnetIPAddress
        txtEnetHttpIp.Text = originalEnetHttpIp

    End Sub

    Private Sub cmbImportGroups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbImportGroups.SelectedIndexChanged

        If cmbImportGroups.SelectedIndex < 0 Then
            Return
        End If

        Dim username As String = CSI_Library.CSI_Library.username
        Dim newNode As TreeNode
        Dim sqlCmd As Text.StringBuilder = New Text.StringBuilder()
        Dim refresh As Boolean = False

        Dim groupMachines As Dictionary(Of String, List(Of eNetMachineConfig))

        If cmbImportGroups.SelectedIndex = 0 Then

            If Not MessageBox.Show("Do you confirm the creation of groups of machines using the eNETDNC Departments?", "Creation of groups", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel) = MessageBoxResult.Yes Then
                cmbImportGroups.SelectedIndex = -1
                Return
            End If

            groupMachines = New Dictionary(Of String, List(Of eNetMachineConfig))()

            For Each machine As eNetMachineConfig In eNetServer.MonitoredMachines
                Dim machName As String = machine.MachineName
                Dim machGroup As String = machine.Department

                If Not groupMachines.ContainsKey(machGroup) Then
                    groupMachines.Add(machGroup, New List(Of eNetMachineConfig)())
                End If

                groupMachines(machGroup).Add(machine)
            Next

        ElseIf cmbImportGroups.SelectedIndex = 1 Then

            If Not MessageBox.Show("Do you confirm the creation of groups of machines using the eNETDNC Groups?", "Creation of groups", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel) = MessageBoxResult.Yes Then
                cmbImportGroups.SelectedIndex = -1
                Return
            End If

            groupMachines = eNetServer.EnetGroups

        End If

        For Each groupPair As KeyValuePair(Of String, List(Of eNetMachineConfig)) In groupMachines

            sqlCmd.Clear()

            If Not treeviewGroupsOfMachines.Nodes(0).Nodes.ContainsKey(groupPair.Key) Then

                newNode = New TreeNode()
                newNode.Name = groupPair.Key
                newNode.Text = groupPair.Key

                treeviewGroupsOfMachines.Nodes(0).Nodes.Add(newNode)

                sqlCmd.Append($"INSERT IGNORE INTO          ")
                sqlCmd.Append($"    CSI_Database.tbl_Groups ")
                sqlCmd.Append($"(                           ")
                sqlCmd.Append($"    users    ,              ")
                sqlCmd.Append($"    `groups`                ")
                sqlCmd.Append($") VALUES (                  ")
                sqlCmd.Append($"    '{username}' ,          ")
                sqlCmd.Append($"    '{groupPair.Key}'       ")
                sqlCmd.Append($");                          ")

            End If

            Dim groupNode = treeviewGroupsOfMachines.Nodes(0).Nodes.Find(groupPair.Key, False).First()

            For Each mach In groupPair.Value

                If Not groupNode.Nodes.ContainsKey(mach.MachineName) Then

                    newNode = New TreeNode()
                    newNode.Name = mach.MachineName
                    newNode.Text = mach.MachineName
                    newNode.ForeColor = Color.Gray

                    groupNode.Nodes.Add(newNode)

                    sqlCmd.Append($"INSERT IGNORE INTO          ")
                    sqlCmd.Append($"    CSI_Database.tbl_Groups ")
                    sqlCmd.Append($"(                           ")
                    sqlCmd.Append($"    users    ,              ")
                    sqlCmd.Append($"    `groups` ,              ")
                    sqlCmd.Append($"    machines ,              ")
                    sqlCmd.Append($"    machineId               ")
                    sqlCmd.Append($") VALUES (                  ")
                    sqlCmd.Append($"    '{username}' ,          ")
                    sqlCmd.Append($"    '{groupPair.Key}' ,     ")
                    sqlCmd.Append($"    '{mach.MachineName}',   ")
                    sqlCmd.Append($"     {mach.MachineId}       ")
                    sqlCmd.Append($");                          ")

                    refresh = True
                End If
            Next

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Next

        If refresh Then
            Comput_perf_required = True
            CSIFlexServiceLib.read_dashboard_config_fromdb()
            UpdateTargetsTitle()
            RefreshAllDevices()
            ComputePerfReq()
        End If

    End Sub

    Private Sub btnAddGroup_Click_2(sender As Object, e As EventArgs) Handles Button7.Click
        AddGroupToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub btnEnetFolderDefault_Click(sender As Object, e As EventArgs) Handles btnEnetFolderDefault.Click
        txtEnetFolder.Text = "C:\_eNETDNC"
    End Sub

    Private Sub btnEnetFolderBrowser_Click(sender As Object, e As EventArgs) Handles btnEnetFolderBrowser.Click

        Dim folderDlg As New FolderBrowserDialog

        folderDlg.Description = "Specify the eNET folder"

        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            txtEnetFolder.Text = folderDlg.SelectedPath
        End If

    End Sub

    Private Sub btnCheckEnetHttpIp_Click(sender As Object, e As EventArgs) Handles btnCheckEnetHttpIp.Click

        eNetServer.Instance.LoadMachinesStatus()
        Dim machines As List(Of eNetDashboardMachine) = eNetServer.Instance.GetMachinesStatus()

        If machines IsNot Nothing Then

            lstEnetMachines.Items.Clear()

            For Each machine In machines
                lstEnetMachines.Items.Add(machine.MachineName)
            Next
            lblEnetCheckResult.ForeColor = Color.Green
            lblEnetCheckResult.Text = $"Connection established, { machines.Count.ToString() } machines available."
            eNetServer.Instance.ReloadMachines()
        Else
            lblEnetCheckResult.ForeColor = Color.Red
            lblEnetCheckResult.Text = "No connection available."
        End If

    End Sub

    Private Sub txtEnetHttpIp_TextChanged(sender As Object, e As EventArgs) Handles txtEnetHttpIp.TextChanged

        lblEnetCheckResult.Text = ""
        lstEnetMachines.Items.Clear()

        If Not originalEnetFolder = txtEnetFolder.Text Or Not originalEnetHttpIp = txtEnetHttpIp.Text Then
            btnEnetSaveChanges.BackColor = Color.DarkSalmon
            btnEnetSaveChanges.Enabled = True
        Else
            btnEnetSaveChanges.BackColor = Color.Transparent
            btnEnetSaveChanges.Enabled = False
        End If

    End Sub

    Private Sub btnEnetSaveChanges_Click(sender As Object, e As EventArgs) Handles btnEnetSaveChanges.Click

        Try
            ServerSettings.EnetFolder = txtEnetFolder.Text
            ServerSettings.EnetIPAddress = txtEnetHttpIp.Text

            originalEnetFolder = txtEnetFolder.Text
            originalEnetHttpIp = txtEnetHttpIp.Text
            btnEnetSaveChanges.BackColor = Color.Transparent
            btnEnetSaveChanges.Enabled = False

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnLoadEnetHistory_Click(sender As Object, e As EventArgs) Handles btnLoadEnetHistory.Click

        Dim years As String = ""

        Me.Cursor = Cursors.WaitCursor

        For Each item In lstEnetYears.Items
            If item.Checked Then

                eNETHistory.UpdateMachinesDatabase(True, Integer.Parse(item.text.ToString()))

                years += item.text.ToString() + ","
            End If
        Next

        'If years.Length = 0 Then Return

        'years = years.Substring(0, years.Length - 1)

        'Using writer As StreamWriter = New StreamWriter(path & "\sys\years_.csys", False)
        '    writer.Write(years)
        'End Using

        'LoadHistoryData()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnEnetSettings_Click(sender As Object, e As EventArgs) Handles btnEnetSettings.Click

        If Not grpEnetSettings.Visible Then
            grpEnetSettings.Visible = True
            pnlMachines.Location = New Drawing.Point(8, 235)
            pnlMachines.Size = New Drawing.Size(pnlMachines.Size.Width, pnlMachines.Size.Height - 225)
        Else
            grpEnetSettings.Visible = False
            pnlMachines.Location = New Drawing.Point(8, 5)
            pnlMachines.Size = New Drawing.Size(pnlMachines.Size.Width, pnlMachines.Size.Height + 225)
        End If

    End Sub

    Private Sub btnEnetCancelChanges_Click(sender As Object, e As EventArgs) Handles btnEnetCancelChanges.Click

        LoadEnetSettings()

    End Sub


    Private Sub AddGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddGroupToolStripMenuItem.Click

        Dim groupname As String = "Group" & treeviewGroupsOfMachines.Nodes(0).Nodes.Count.ToString()

        treeviewGroupsOfMachines.Nodes(0).Nodes.Add(groupname, groupname)
        treeviewGroupsOfMachines.Nodes(0).Expand()
        AddGroup(groupname)
        ancient_name = groupname
        treeviewGroupsOfMachines.LabelEdit = True
        treeviewGroupsOfMachines.Nodes(0).Nodes(treeviewGroupsOfMachines.Nodes(0).Nodes.Count - 1).BeginEdit()

    End Sub


    Private Sub txtEnetFolder_TextChanged(sender As Object, e As EventArgs) Handles txtEnetFolder.TextChanged

        lstEnetYears.Items.Clear()

        If Not originalEnetFolder = txtEnetFolder.Text Or Not originalEnetHttpIp = txtEnetHttpIp.Text Then
            btnEnetSaveChanges.BackColor = Color.DarkSalmon
            btnEnetSaveChanges.Enabled = True
        Else
            btnEnetSaveChanges.BackColor = Color.Transparent
            btnEnetSaveChanges.Enabled = False
        End If

        If String.IsNullOrWhiteSpace(txtEnetFolder.Text) Then Return

        Try
            If Directory.Exists(txtEnetFolder.Text) Then

                lblEnetFolderStatus.ForeColor = Color.Green
                lblEnetFolderStatus.Text = $"Folder found"

                Dim files As String() = Directory.GetFiles($"{txtEnetFolder.Text}\_REPORTS\")

                For Each file In files

                    Dim fileInfo = New FileInfo(file)

                    If fileInfo.Name.StartsWith("_MACHINE_") And fileInfo.Extension = ".CSV" Then
                        Dim year = fileInfo.Name.Substring(fileInfo.Name.Length - 8, 4)
                        lstEnetYears.Items.Add(year)
                    End If
                Next

            Else
                lblEnetFolderStatus.ForeColor = Color.Red
                lblEnetFolderStatus.Text = $"Folder not found"
            End If
        Catch ex As Exception
            lblEnetFolderStatus.ForeColor = Color.Red
            lblEnetFolderStatus.Text = $"Folder not found"
        End Try

    End Sub


    Private Sub gridviewMachines_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles gridviewMachines.CellEndEdit

        If gridviewMachines.Columns(e.ColumnIndex).HeaderText = "Source" Then

            CheckConnection(e)

        End If

        If gridviewMachines.Columns(e.ColumnIndex).HeaderText = "Machine Name" Then
            ' DataGridView1.Rows(e.RowIndex ).Cells("Machine").Value 
        End If

    End Sub

    Private Sub gridviewMachines_SelectionChanged(sender As Object, e As EventArgs) Handles gridviewMachines.SelectionChanged

        Dim grid As DataGridView = sender

        If grid.SelectedCells.Count > 0 Then

            lblMachineId.Text = gridviewMachines.Rows(grid.SelectedCells(0).RowIndex).Cells("Id").Value
            lblMachineName.Text = gridviewMachines.Rows(grid.SelectedCells(0).RowIndex).Cells("EnetMachineName").Value
            lblEnetPos.Text = ""
            lblDepartment.Text = ""
            lblProtocol.Text = ""
            lblFtpFileName.Text = ""
            lblAlwaysRecCycleOn.Text = ""
            lblCycleOnCommand.Text = ""
            lblCycleOffCommand.Text = ""
            lblPartNumberCommand.Text = ""

            Dim machine = eNetServer.Machines.Find(Function(m) m.MachineName = lblMachineName.Text)

            If machine IsNot Nothing Then
                lblEnetPos.Text = machine.EnetPos
                lblDepartment.Text = machine.Department
                lblProtocol.Text = machine.Protocol
                lblFtpFileName.Text = machine.FTPFileName
                lblAlwaysRecCycleOn.Text = IIf(machine.AlwaysRecordCycleOn, "YES", "NOT")

                lblCycleOnCommand.Text = machine.Cmd_CON
                If Not String.IsNullOrEmpty(machine.Cmd_CON2) Then lblCycleOnCommand.Text = $"{lblCycleOnCommand.Text} / {machine.Cmd_CON2}"

                lblCycleOffCommand.Text = machine.Cmd_COFF
                If Not String.IsNullOrEmpty(machine.Cmd_COFF2) Then lblCycleOffCommand.Text = $"{lblCycleOffCommand.Text} / {machine.Cmd_COFF2}"

                lblPartNumberCommand.Text = machine.Cmd_PARTNO
                If Not String.IsNullOrEmpty(machine.Cmd_PARTNO2) Then lblPartNumberCommand.Text = $"{lblPartNumberCommand.Text} / {machine.Cmd_PARTNO}"

            End If

        End If

    End Sub

    Private Sub gridviewMachines_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles gridviewMachines.CellValueChanged

        If (e.RowIndex < 0) Then
            Return
        End If

        Dim id = gridviewMachines.CurrentRow.Cells("Id").Value
        Dim machineName = gridviewMachines.CurrentRow.Cells("Machines").Value
        Dim machineLabel = gridviewMachines.CurrentRow.Cells("MachineLabel").Value
        Dim dailyTarget = gridviewMachines.CurrentRow.Cells("DailyTarget").Value
        Dim weeklyTarget = gridviewMachines.CurrentRow.Cells("WeeklyTarget").Value
        Dim monthlyTarget = gridviewMachines.CurrentRow.Cells("MonthlyTarget").Value

        Dim sqlCmd As Text.StringBuilder = New Text.StringBuilder()

        sqlCmd.Append($"UPDATE csi_auth.tbl_ehub_conf                     ")
        sqlCmd.Append($"    SET                                           ")
        sqlCmd.Append($"        `machine_name`      = '{ machineName  }', ")
        sqlCmd.Append($"        `machine_label`     = '{ machineLabel }', ")
        sqlCmd.Append($"        `MCH_DailyTarget`   =  { dailyTarget }  , ")
        sqlCmd.Append($"        `MCH_WeeklyTarget`  =  { weeklyTarget } , ")
        sqlCmd.Append($"        `MCH_MonthlyTarget` =  { monthlyTarget}   ")
        sqlCmd.Append($"    WHERE                                         ")
        sqlCmd.Append($"        `Id`                =  { id };            ")
        sqlCmd.Append($"UPDATE csi_auth.tbl_csiconnector                  ")
        sqlCmd.Append($"    SET                                           ")
        sqlCmd.Append($"        `machineName`       = '{ machineName  }'  ")
        sqlCmd.Append($"    WHERE                                         ")
        sqlCmd.Append($"        `MachineId`         =  { id };            ")

        MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Call LoadGridviewMachines()

    End Sub

    Private Sub gridviewMachines_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles gridviewMachines.CellClick

        Try
            If (e.ColumnIndex = gridviewMachines.Columns("Check").Index And e.RowIndex >= 0) Then

                CheckConnection(e)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        '   https://www.youtube.com/watch?v=CHyESZfaxxE
    End Sub

    Private Sub gridviewMachines_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gridviewMachines.MouseUp

        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim hti As DataGridView.HitTestInfo = gridviewMachines.HitTest(e.X, e.Y)

            If gridviewMachines.SelectedCells(0).RowIndex <> hti.RowIndex Then
                gridviewMachines.CurrentCell = gridviewMachines.Rows(hti.RowIndex).Cells(hti.ColumnIndex)
            End If

            If gridviewMachines.SelectedCells.Count > 0 And Boolean.Parse(gridviewMachines.Rows(hti.RowIndex).Cells("IsMonitored").Value) Then
                add_menu.DropDownItems.Clear()

                For Each node As TreeNode In treeviewGroupsOfMachines.Nodes(0).Nodes

                    Dim item_ As New ToolStripMenuItem(node.Text, Nothing, AddressOf AddMachineToGroup, node.Text)

                    item_.Text = node.Text

                    If Not add_menu.DropDownItems.ContainsKey(item_.Text) Then add_menu.DropDownItems.Add(item_)
                Next

                CMS_GridEdit.Show(gridviewMachines, New System.Drawing.Point(e.X, e.Y), ToolStripDropDownDirection.Default)
            End If
        End If

    End Sub


    Private Sub RenameUsingF2Key_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles treeviewGroupsOfMachines.KeyDown

        'If F2 key is pressed
        If e.KeyCode = Keys.F2 Then

            If Not (IsNothing(treeviewGroupsOfMachines.SelectedNode)) And Not treeviewGroupsOfMachines.SelectedNode.Text = "Groups of machines" And treeviewGroupsOfMachines.SelectedNode.Level = 1 Then
                treeviewGroupsOfMachines.LabelEdit = True
                ancient_name = treeviewGroupsOfMachines.SelectedNode.Text
                treeviewGroupsOfMachines.SelectedNode.BeginEdit()
            End If

            RefreshAllDevices()

        ElseIf e.KeyCode = Keys.Delete Then

            Dim tn As TreeNode = treeviewGroupsOfMachines.SelectedNode

            If tn IsNot Nothing And Not tn.Text = "Groups of machines" And tn.Level = 1 Then

                selected_node_1 = Nothing

                Dim iRet = MessageBox.Show("Are you certain you want to delete " & tn.Text & "?", "Delete group", MessageBoxButton.YesNo, MessageBoxImage.Question)

                If iRet = MessageBoxResult.Yes Then

                    treeviewGroupsOfMachines.Nodes.Remove(tn)

                    Try

                        MySqlAccess.ExecuteNonQuery($"DELETE FROM CSI_database.tbl_Groups WHERE `groups` = '{ tn.Text }' and users = '{ CSI_Library.CSI_Library.username }';")

                        Dim RowsAffected = MySqlAccess.ExecuteScalar($"SELECT count(*) from CSI_database.tbl_devices where `groups` LIKE '%{ tn.Text }, %';")

                        If RowsAffected = 0 Then

                            MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_devices SET `groups` = replace(`groups`, ', { tn.Text }', '');")

                        Else

                            MySqlAccess.ExecuteNonQuery($"UPDATE  CSI_database.tbl_devices SET `groups` = replace(`groups`, '{ tn.Text }, ', '');")

                        End If

                    Catch ex As Exception
                        Log.Error(ex)
                        MessageBox.Show("Error in Deleting Group ::" & ex.Message())
                    End Try

                    treeviewGroupsOfMachines.Nodes(0).Nodes.Clear()

                    Load_groupes()

                    Dim dtTable = MySqlAccess.GetDataTable("SELECT IP FROM csi_database.tbl_deviceconfig;")

                    Dim oProducts = New List(Of String)
                    For iIndex As Integer = 0 To dtTable.Rows.Count - 1
                        oProducts.Add(dtTable.Rows(iIndex)("IP"))
                    Next

                    Dim LISTSize As Integer
                    LISTSize = oProducts.Count

                    For Count As Integer = 0 To (LISTSize - 1)
                        RefreshDevice(oProducts(Count).ToString())
                    Next

                    CSIFlexServiceLib.read_dashboard_config_fromdb()
                    RefreshAllDevices()
                    ComputePerfReq()

                    Comput_perf_required = True

                End If
            End If
        End If
    End Sub

    Private Sub treeviewGroupsOfMachines_MouseUp(sender As Object, e As MouseEventArgs) Handles treeviewGroupsOfMachines.MouseUp

        If e.Button = Windows.Forms.MouseButtons.Right Then

            ' Point where the mouse is clicked.
            Dim p As New Drawing.Point(e.X, e.Y)

            ' Get the node that the user has clicked.
            Dim node As TreeNode = treeviewGroupsOfMachines.GetNodeAt(p)
            If node IsNot Nothing Then

                ' Select the node the user has clicked.
                ' The node appears selected until the menu is displayed on the screen.
                Dim m_OldSelectNode = treeviewGroupsOfMachines.SelectedNode
                treeviewGroupsOfMachines.SelectedNode = node

                ' Find the appropriate ContextMenu depending on the selected node.
                Dim nodename As String = Convert.ToString(node.Name)

                If (node.Level = 0) Then
                    CMS_GroupEdit_NEW.Items(0).Visible = True
                    CMS_GroupEdit_NEW.Items(1).Visible = False
                    CMS_GroupEdit_NEW.Items(2).Visible = False
                    CMS_GroupEdit_NEW.Items(3).Visible = False
                    CMS_GroupEdit_NEW.Show(treeviewGroupsOfMachines, p)
                ElseIf (node.Level = 1) Then
                    CMS_GroupEdit_NEW.Items(0).Visible = False
                    CMS_GroupEdit_NEW.Items(1).Visible = True
                    CMS_GroupEdit_NEW.Items(2).Visible = True
                    CMS_GroupEdit_NEW.Items(3).Visible = False
                    CMS_GroupEdit_NEW.Show(treeviewGroupsOfMachines, p)
                ElseIf (node.Level = 2) Then
                    CMS_GroupEdit_NEW.Items(0).Visible = False
                    CMS_GroupEdit_NEW.Items(1).Visible = False
                    CMS_GroupEdit_NEW.Items(2).Visible = False
                    CMS_GroupEdit_NEW.Items(3).Visible = True
                    CMS_GroupEdit_NEW.Show(treeviewGroupsOfMachines, p)
                End If
            End If
        End If
    End Sub

    Private Sub treeviewGroupsOfMachines_AfterLabelEdit(sender As Object, e As NodeLabelEditEventArgs) Handles treeviewGroupsOfMachines.AfterLabelEdit

        Dim groupname As String = e.Label

        If groupname = "" Then
            groupname = "Group" & (treeviewGroupsOfMachines.Nodes(0).Nodes.Count - 1).ToString()
        End If

        treeviewGroupsOfMachines.SelectedNode.Name = e.Label
        treeviewGroupsOfMachines.LabelEdit = False

        Try

            Dim sqlCmd As StringBuilder = New StringBuilder()
            sqlCmd.Append($"UPDATE CSI_database.tbl_groups  SET `groups` = '{ groupname }' WHERE `groups` = '{ ancient_name }' AND users = '{ CSI_Library.CSI_Library.username }'; ")
            sqlCmd.Append($"UPDATE CSI_database.tbl_devices SET `groups` = replace(`groups`, '{ ancient_name }','{ groupname }'); ")
            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception

        End Try

        treeviewGroupsOfMachines.Nodes(0).Nodes.Clear()
        Load_groupes()

        Dim dtTable = MySqlAccess.GetDataTable("SELECT DeviceId FROM csi_database.tbl_deviceconfig;")

        Dim oProducts = New List(Of String)
        For iIndex As Integer = 0 To dtTable.Rows.Count - 1
            oProducts.Add(dtTable.Rows(iIndex)("DeviceId"))
        Next

        Dim LISTSize As Integer
        LISTSize = oProducts.Count
        For Count As Integer = 0 To (LISTSize - 1)
            RefreshDevice(oProducts(Count).ToString())
            Count = Count + 1
        Next

        CSIFlexServiceLib.read_dashboard_config_fromdb()
        RefreshAllDevices()
        ComputePerfReq()

        Comput_perf_required = True

    End Sub


    Private Sub DeleteNodeTreeview(sender As Object, e As EventArgs) Handles DeleteGroupToolStripMenuItem.Click

        Dim tn As TreeNode = treeviewGroupsOfMachines.SelectedNode

        If tn IsNot Nothing And Not tn.Text = "Groups of machines" Then

            selected_node_1 = Nothing
            Dim iRet = MessageBox.Show("Are you certain you want to delete " & tn.Text & "?", "Delete group", MessageBoxButton.YesNo, MessageBoxImage.Question)

            If iRet = MessageBoxResult.Yes Then

                treeviewGroupsOfMachines.Nodes.Remove(tn)

                Try

                    MySqlAccess.ExecuteNonQuery($"DELETE FROM CSI_database.tbl_Groups WHERE `groups` = '{ tn.Text }' and users = '{ CSI_Library.CSI_Library.username }';")

                    Dim RowsAffected = MySqlAccess.ExecuteScalar($"SELECT count(*) FROM CSI_database.tbl_devices WHERE `groups` LIKE '%{ tn.Text }, %';")

                    If RowsAffected = 0 Then

                        MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_devices SET `groups` = replace(`groups`, ', { tn.Text }', '');")

                    Else

                        MySqlAccess.ExecuteNonQuery($"UPDATE  CSI_database.tbl_devices SET `groups` = replace(`groups`, '{ tn.Text }, ', '');")

                    End If

                Catch ex As Exception
                    Log.Error(ex)
                    MessageBox.Show("Error in Deleting Group ::" & ex.Message())
                End Try

                treeviewGroupsOfMachines.Nodes(0).Nodes.Clear()
                Load_groupes()

                Dim dtTable = MySqlAccess.GetDataTable("SELECT IP FROM csi_database.tbl_deviceconfig;")

                Dim oProducts = New List(Of String)
                For iIndex As Integer = 0 To dtTable.Rows.Count - 1
                    oProducts.Add(dtTable.Rows(iIndex)("IP"))
                Next

                Dim LISTSize As Integer
                LISTSize = oProducts.Count

                For Count As Integer = 0 To (LISTSize - 1)
                    RefreshDevice(oProducts(Count).ToString())
                    '    Count = Count + 1
                Next

                CSIFlexServiceLib.read_dashboard_config_fromdb()
                RefreshAllDevices()
                ComputePerfReq()

                Comput_perf_required = True
            End If

        End If

    End Sub

    Private Sub DeleteMachineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteMachineToolStripMenuItem.Click

        Dim tn As TreeNode = treeviewGroupsOfMachines.SelectedNode

        If tn IsNot Nothing And Not tn.Text = "Groups of machines" Then

            selected_node_1 = Nothing

            Try
                Dim groupName = treeviewGroupsOfMachines.SelectedNode.Parent.FullPath.Split("\").Last()
                Dim machineId = treeviewGroupsOfMachines.SelectedNode.Tag

                Dim cmd As Text.StringBuilder = New Text.StringBuilder()
                cmd.Append($"DELETE FROM                          ")
                cmd.Append($"   CSI_database.tbl_Groups           ")
                cmd.Append($"WHERE                                ")
                cmd.Append($"      `groups`    = '{ groupName }'  ")
                cmd.Append($"  AND `machineId` =  { machineId }   ")

                MySqlAccess.ExecuteNonQuery(cmd.ToString())

            Catch ex As Exception

                Log.Error(ex)
            End Try

            treeviewGroupsOfMachines.Nodes(0).Nodes.Clear()

            Load_groupes()

            Comput_perf_required = True

        End If

        CSIFlexServiceLib.read_dashboard_config_fromdb()

        RefreshAllDevices()

        ComputePerfReq()

    End Sub

    Private Sub RenameGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameGroupToolStripMenuItem.Click

        Dim tn As TreeNode = treeviewGroupsOfMachines.SelectedNode

        ancient_name = tn.Text

        treeviewGroupsOfMachines.LabelEdit = True

        tn.BeginEdit()

    End Sub



    Private Sub chkEnetAllPositions_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnetAllPositions.CheckedChanged

        If chkEnetAllPositions.Checked And gridviewMachines.Rows.Count < 128 Then

            Dim id As Integer = 0
            Dim Label As String = ""
            Dim enetMach As String = ""

            Dim targetTable As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf")

            Dim noMonitoredMachines As List(Of eNetMachineConfig) = eNetServer.Machines.FindAll(Function(m) eNetServer.MonitoredMachines.Find(Function(m2) m2.MachineName = m.MachineName) Is Nothing)

            For Each machine As eNetMachineConfig In noMonitoredMachines

                Dim targetMachine As DataRow = targetTable.Select().Where(Function(m) m.Item("EnetMachineName") = machine.MachineName).FirstOrDefault()

                If targetMachine IsNot Nothing Then
                    id = Integer.Parse(targetMachine.Item("id").ToString())
                    Label = targetMachine.Item("machine_label").ToString()
                    enetMach = targetMachine.Item("EnetMachineName").ToString()
                    targetTable.Rows.Remove(targetMachine)
                End If

                Dim newRow As DataGridViewRow = New DataGridViewRow()
                newRow.CreateCells(gridviewMachines, id, False, machine.MachineName, enetMach, Label, "eNET", "check", "", 0, 0, 0, "False")
                newRow.DefaultCellStyle.ForeColor = Color.Gray
                newRow.DefaultCellStyle.BackColor = Color.LightGray
                newRow.DefaultCellStyle.SelectionBackColor = Color.LightCoral
                newRow.DefaultCellStyle.SelectionForeColor = Color.Black

                gridviewMachines.Rows.Add(newRow)
            Next
        Else

            Dim row As DataGridViewRow

            Do
                row = gridviewMachines.Rows.Cast(Of DataGridViewRow).Where(Function(r) Not Boolean.Parse(r.Cells("isMonitored").Value)).FirstOrDefault()

                If row IsNot Nothing Then
                    gridviewMachines.Rows.Remove(row)
                End If
            Loop While row IsNot Nothing

        End If

    End Sub

    Public Sub LoadGridviewMachines()

        'Dim currentrow As String()
        Dim id As Integer = 0
        Dim machineName As String = ""
        Dim readed As String = ""
        Dim label As String = ""
        Dim enetMach As String = ""
        Dim chked As New DataGridViewCheckBoxCell

        chked.Value = False
        gridviewMachines.Rows.Clear()

        If Not eNetServer.eNetLoaded Then Return

        Dim targetTable As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf")

        Dim monitoredMachines As List(Of eNetMachineConfig) = eNetServer.MonitoredMachines

        For Each machine As eNetMachineConfig In monitoredMachines

            Dim DailyTarget As Integer = 0
            Dim Weeklytarget As Integer = 0
            Dim MonthlyTarget As Integer = 0

            Dim targetMachine As DataRow = targetTable.Select().Where(Function(m) m.Item("EnetMachineName") = machine.MachineName).First()

            If targetMachine IsNot Nothing Then
                machineName = targetMachine.Item("Machine_Name").ToString()
                id = Integer.Parse(targetMachine.Item("id").ToString())
                label = targetMachine.Item("machine_label").ToString()
                enetMach = targetMachine.Item("EnetMachineName").ToString()
                DailyTarget = Convert.ToInt32(targetMachine.Item("MCH_DailyTarget").ToString())
                Weeklytarget = Convert.ToInt32(targetMachine.Item("MCH_WeeklyTarget").ToString())
                MonthlyTarget = Convert.ToInt32(targetMachine.Item("MCH_MonthlyTarget").ToString())
            End If

            gridviewMachines.Rows.Add(id, False, machineName, enetMach, label, "eNET", "check", "", DailyTarget, Weeklytarget, MonthlyTarget, "True")
            gridviewMachines.Sort(gridviewMachines.Columns(1), ListSortDirection.Ascending)
        Next

        Dim qtt = gridviewMachines.Rows.Count
        lblQttMachines.Text = $"{qtt} Machine{IIf(qtt > 1, "s", "")}"

        chkEnetAllPositions_CheckedChanged(Me, EventArgs.Empty)

        Return
    End Sub

    Private Sub AddGroup(groupname As String)

        Dim sqlCmd As String = $"INSERT IGNORE INTO CSI_Database.tbl_Groups (users, `groups`) VALUES('{ CSI_Library.CSI_Library.username }', '{ groupname }' )"
        MySqlAccess.ExecuteNonQuery(sqlCmd)

        Comput_perf_required = True
        CSIFlexServiceLib.read_dashboard_config_fromdb()
        UpdateTargetsTitle()
        RefreshAllDevices()
        ComputePerfReq()
    End Sub

    Public Sub AddMachineToGroup(ByVal sender As Object, ByVal e As EventArgs)

        Dim rowIndex = gridviewMachines.SelectedCells(0).RowIndex

        If gridviewMachines.SelectedCells.Count > 0 And Boolean.Parse(gridviewMachines.Rows(rowIndex).Cells("IsMonitored").Value) Then
            Dim alist As New List(Of Integer)
            For Each CELL_ As DataGridViewCell In gridviewMachines.SelectedCells
                If Not alist.Contains(CELL_.RowIndex) Then
                    alist.Add(CELL_.RowIndex)


                    Dim row_ As DataGridViewRow = gridviewMachines.Rows.Item(CELL_.RowIndex)

                    Dim node_ As New TreeNode, node_0 As New TreeNode
                    If Not IsNothing(row_.Cells.Item("Machines").Value) Then

                        node_.Name = row_.Cells.Item("Machines").Value.ToString()
                        node_.Text = row_.Cells.Item("Machines").Value.ToString()
                        node_.Tag = row_.Cells.Item("Id").Value.ToString()

                        Dim g As String = CType(sender, ToolStripMenuItem).Text

                        For Each n As TreeNode In treeviewGroupsOfMachines.Nodes(0).Nodes
                            For Each n2 As TreeNode In n.Nodes
                                If n.Text = g Then
                                    If (n2.Text = node_.Text) Then
                                        GoTo end_
                                    End If
                                End If
                            Next
                        Next

                        Dim treeNode As New TreeNode
                        For Each n As TreeNode In treeviewGroupsOfMachines.Nodes(0).Nodes
                            If (n.Text = CType(sender, ToolStripMenuItem).Text) Then
                                treeNode = n
                            End If
                        Next

                        If (treeNode.Text.Length > 0) Then
                            treeviewGroupsOfMachines.SelectedNode = treeNode
                            If Not (treeviewGroupsOfMachines.Nodes(0).Nodes(treeNode.Index).Nodes.ContainsKey(node_.Name)) Then
                                treeviewGroupsOfMachines.Nodes(0).Nodes(treeNode.Index).Nodes.Add(node_)
                            End If

                            Dim cmd As Text.StringBuilder = New Text.StringBuilder()
                            cmd.Append($"INSERT IGNORE INTO CSI_Database.tbl_Groups ")
                            cmd.Append($"(                                          ")
                            cmd.Append($"   `users`                               , ")
                            cmd.Append($"   `groups`                              , ")
                            cmd.Append($"   `machines`                            , ")
                            cmd.Append($"   `MachineId`                             ")
                            cmd.Append($")                                          ")
                            cmd.Append($"VALUES                                     ")
                            cmd.Append($"(                                          ")
                            cmd.Append($"   '{ CSI_Library.CSI_Library.username }', ")
                            cmd.Append($"   '{ treeNode.Text }'                   , ")
                            cmd.Append($"   '{ node_.Text }'                      , ")
                            cmd.Append($"    { node_.Tag }                          ")
                            cmd.Append($");                                         ")

                            MySqlAccess.ExecuteNonQuery(cmd.ToString())

                            Comput_perf_required = True
                        Else
                            MessageBox.Show("Unable to find group, please retry.")
                        End If
                    End If
                End If
end_:

            Next

        End If

        CSIFlexServiceLib.read_dashboard_config_fromdb()

        UpdateTargetsTitle()

        RefreshAllDevices()

        ComputePerfReq()

    End Sub

    Public Sub LoadHistoryData()
        Try
            Dim years_(-1) As String

            If (File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\years_.csys")) Then
                Using streader As New StreamReader(CSI_Library.CSI_Library.serverRootPath + "\sys\years_.csys")
                    Dim tmp_str As String = streader.ReadLine()
                    If tmp_str IsNot Nothing Then years_ = tmp_str.Split(",")
                End Using
            End If
            If years_ Is Nothing Or years_.Length = 0 Then
                CSI_Lib.Log_server_event("Years_ (in setupform) is nothing, FirstupdateDB executed")
                CSI_Lib.FirstUpdateDB_Mysql(Now.Year)
            Else
                For Each year As String In years_
                    CSI_Lib.Log_server_event("FirstupdateDB executed for year : " & year)
                    If year <> "" Then CSI_Lib.FirstUpdateDB_Mysql(year)
                Next
            End If

        Catch ex As Exception
            Log.Error(ex)
        End Try

        thread_AutoLoadMachinePerf() 'Function that loads todays machine status history from .MON(_MONITORING Folder) and .SYS_ (_TMP Folder)

    End Sub

    Public Sub thread_AutoLoadMachinePerf()

        GetAllENETMachines()

        Dim Today = DateTime.Now()
        Dim SearchMonthFolder = Today.ToString("yyyy-MM")
        Dim SearchDate = Today.ToString("MMMdd")
        Dim allconst As New AllStringConstants.StringConstant
        Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)

        cntsql.Open()

        Try
            AllMachines.Clear()

            'Read all data from EhubConf File where monitoring is ON
            Dim dtEhub As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf WHERE Monstate='1';")

            If dtEhub.Rows.Count > 0 Then
                For Each row As DataRow In dtEhub.Rows

                    If Not AllMachines.ContainsKey(row("monitoring_id").ToString()) Then
                        AllMachines.Add(row("monitoring_id"), New MachineData)
                    End If

                    AllMachines.Item(row("monitoring_id").ToString()).Machinename_ = row("machine_name").ToString()
                    AllMachines.Item(row("monitoring_id").ToString()).MonitoringFileName_ = row("monitoring_filename").ToString()
                    AllMachines.Item(row("monitoring_id").ToString()).MonitoringState_ = Integer.Parse(row("Monstate"))
                    AllMachines.Item(row("monitoring_id").ToString()).MonitoringID_ = row("monitoring_id").ToString()
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("MysqlConnectionstring Error : " & ex.Message())
        End Try

        Dim displaylistpath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\CSI Flex Server\sys\machine_list_.csys"

        If File.Exists(displaylistpath) Then

            Dim fileData() As String = File.ReadAllLines(displaylistpath) 'Read All Lines from machine_list_.csys file

            For Each line1 As String In fileData
                If Not line1 = String.Empty Then

                    Dim machinename As String = line1.Split(",")(0)

                    If Not DisplayMachineList.ContainsKey(machinename) Then
                        DisplayMachineList.Add(machinename, String.Empty)
                    End If

                    DisplayMachineList.Item(machinename) = RenameMachine(machinename)
                End If
            Next
            'Dim listsplit As String() = fileData
        End If

        ' If Directrory Exists 
        Dim folderPath As String = allconst.SERVER_ENET_PATH & allconst.MONITORING_FOLDER_PATH & SearchMonthFolder

        If Directory.Exists(folderPath) Then

            MySqlAccess.Validate_MachinePerf_Database()

            'Delete All the Tables From Database 
            MySqlAccess.Delete_All_MachinePerf_Tables()

            'Folder with YEAR-CURRENTMONTH exist
            'Now check if Today's File exists file Format is :: MMMdd_MachineName_SHIFT1.MON
            For Each kvp As KeyValuePair(Of String, MachineData) In AllMachines

                'Create Tables Fro Machine
                For Each kvp2 As KeyValuePair(Of String, String) In DisplayMachineList

                    Dim match As Boolean = True

                    If kvp.Value.Machinename_ = kvp2.Key Then 'When machine display list and All Machine name matches only create those tables and ignore others 
                        MySqlAccess.Validate_Perf_Machine_Table(kvp2.Key)
                        Exit For
                    End If
                Next
            Next

            Dim Monnewdate As New DateTime
            Dim newDate As New DateTime
            Dim Monlaststatustimestamp As New DateTime
            Dim laststatustimestamp As New DateTime

            For Each keyvalAllMachine As KeyValuePair(Of String, MachineData) In AllMachines

                If DisplayMachineList.ContainsKey(keyvalAllMachine.Value.Machinename_) Then
                    Dim files As String() = IO.Directory.GetFiles(folderPath, SearchDate & "_" & keyvalAllMachine.Value.Machinename_ & "*")
                    Dim count As Integer = files.Count ' no of files 
                    Dim tempshift As Integer = 0

                    If count = 1 Then
                        'tempshift = 2
                        Dim onefile As String = files(0)
                        Dim splitMonfileForShift As Char = onefile(onefile.Length - 5)

                        tempshift = Convert.ToInt32(splitMonfileForShift.ToString())
                        'One Monfile Curret Shift is 2 

                        For Each monfiles As String In files

                            If File.Exists(monfiles) Then

                                Dim MonFileData As String() = File.ReadAllLines(monfiles)

                                For Each Monlines As String In MonFileData

                                    If Monlines.Contains("_PARTNO") Or Monlines.Contains("_DPRINT_") Or Monlines.Contains("_OPERATOR") Or Monlines.Contains("_SH_START") Or Monlines.Contains("_SH_END") Or Monlines.Contains("_SH_") Or (Monlines = String.Empty) Or (Monlines = "") Then 'Or lines1.Contains("NO eMONITOR")
                                        'If above patterns found don't add them to the database table
                                    Else
                                        'Add these entries to database table 
                                        If Monlines.Contains(",") Then 'This handles Empty String So we don't have any , in a string

                                            Dim MonSplitLines As String() = Monlines.Split(",")
                                            Dim Monjoindate As String = MonSplitLines(0) & " " & MonSplitLines(1)
                                            Dim Mononlytime As String() = (Convert.ToDateTime(MonSplitLines(1)).ToString("HH:mm:ss")).Split(":")

                                            '"10/23/19,06:00:01"

                                            Monnewdate = DateTime.ParseExact(Monjoindate, "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)

                                            Dim Monlatestdate = Monnewdate.ToString("yyyy-MM-dd HH:mm:ss")
                                            Dim MonStatus As String = MonSplitLines(2)

                                            If MonStatus = "_CON" Then
                                                MonStatus = "CYCLE ON"
                                            ElseIf MonStatus = "_COFF" Then
                                                MonStatus = "CYCLE OFF"
                                            ElseIf MonStatus = "_SETUP" Then
                                                MonStatus = "SETUP"
                                            End If

                                            Dim MonTIME_s As Integer

                                            MonTIME_s = Convert.ToInt32(Mononlytime(0)) * 3600 + Convert.ToInt32(Mononlytime(1)) * 60 + Convert.ToInt32(Mononlytime(2))

                                            Dim dTable_SelectRowsMon As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) };")

                                            If dTable_SelectRowsMon.Rows.Count = 0 Then

                                                Dim Montimediff = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())
                                                Dim cmdMon As MySqlCommand = New MySqlCommand("insert ignore into CSI_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & "(status,time,cycletime,shift,date) VALUES ('" & MonStatus & "', " & (MonTIME_s) & " , '" & Montimediff & "','" & tempshift & "', '" & Monlatestdate & "')", cntsql)

                                                cmdMon.ExecuteNonQuery()
                                                Monlaststatustimestamp = Monnewdate
                                                laststatustimestamp = Monnewdate

                                            ElseIf dTable_SelectRowsMon.Rows.Count > 0 Then

                                                Dim Monlastcycletime = DateDiff(DateInterval.Second, Monlaststatustimestamp, Monnewdate)
                                                Dim mysqlUpdateMon As String = "SET @select := (SELECT date FROM csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " order by date desc limit 1);SET SQL_SAFE_UPDATES = 0;update  csi_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & " SET `cycletime` = '" & Monlastcycletime & "' Where date =@select;"
                                                Dim cmdmysqlUpdateMon As New MySqlCommand(mysqlUpdateMon, cntsql)

                                                cmdmysqlUpdateMon.ExecuteNonQuery()

                                                Dim Montimediff1 = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())
                                                Dim cmd As MySqlCommand = New MySqlCommand("insert ignore into CSI_machineperf.tbl_" & DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) & "(status,time,cycletime,shift,date) VALUES ('" & MonStatus & "', " & (MonTIME_s) & " ,'" & Montimediff1 & "' ,'" & tempshift & "', '" & Monlatestdate & "')", cntsql)

                                                cmd.ExecuteNonQuery()
                                                Monlaststatustimestamp = Monnewdate
                                                laststatustimestamp = Monnewdate
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        Next
                    ElseIf count = 2 Then

                        'Two Monfiles Curret Shift is 3
                        Dim fileno As Integer = 0

                        For Each monfiles As String In files

                            Dim splitMonfileForShift As Char = monfiles(monfiles.Length - 5)

                            tempshift = Convert.ToInt32(splitMonfileForShift.ToString())

                            If File.Exists(monfiles) Then

                                Dim MonFileData As String() = File.ReadAllLines(monfiles)

                                For Each Monlines As String In MonFileData

                                    If Monlines.Contains("_PARTNO") Or Monlines.Contains("_DPRINT_") Or Monlines.Contains("_OPERATOR") Or Monlines.Contains("_SH_START") Or Monlines.Contains("_SH_END") Or Monlines.Contains("_SH_") Or (Monlines = String.Empty) Or (Monlines = "") Then
                                        'If above patterns found don't add them to the database table
                                    Else
                                        'Add these entries to database tables
                                        If Monlines.Contains(",") Then 'This if handles if the string we have is empty 

                                            Dim MonSplitLines As String() = Monlines.Split(",")
                                            Dim Monjoindate As String = $"{ MonSplitLines(0) } { MonSplitLines(1) }"
                                            Dim Mononlytime As String() = (Convert.ToDateTime(MonSplitLines(1)).ToString("HH:mm:ss")).Split(":")

                                            Monnewdate = DateTime.ParseExact(Monjoindate, "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                                            'Monnewdate = Convert.ToDateTime(Monjoindate)

                                            Dim Monlatestdate = Monnewdate.ToString("yyyy-MM-dd HH:mm:ss")
                                            Dim MonStatus As String = MonSplitLines(2)

                                            If MonStatus = "_CON" Then
                                                MonStatus = "CYCLE ON"
                                            ElseIf MonStatus = "_COFF" Then
                                                MonStatus = "CYCLE OFF"
                                            ElseIf MonStatus = "_SETUP" Then
                                                MonStatus = "SETUP"
                                            End If

                                            Dim MonTIME_s As Integer

                                            MonTIME_s = Convert.ToInt32(Mononlytime(0)) * 3600 + Convert.ToInt32(Mononlytime(1)) * 60 + Convert.ToInt32(Mononlytime(2))

                                            Dim dTable_SelectRowsMon As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) };")

                                            If dTable_SelectRowsMon.Rows.Count = 0 Then

                                                Dim Montimediff = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())

                                                MySqlAccess.ExecuteNonQuery($"INSERT IGNORE INTO CSI_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } (status, time, cycletime, shift, date) VALUES ('{ MonStatus }', { (MonTIME_s) } , '{ Montimediff }','{ tempshift }', '{ Monlatestdate }')")

                                                Monlaststatustimestamp = Monnewdate
                                                laststatustimestamp = Monnewdate

                                            ElseIf dTable_SelectRowsMon.Rows.Count > 0 Then

                                                Dim Monlastcycletime = DateDiff(DateInterval.Second, Monlaststatustimestamp, Monnewdate)

                                                Dim sqlCmd As Text.StringBuilder = New Text.StringBuilder()
                                                sqlCmd.Append($"SET @select := (SELECT date FROM csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } order by date desc limit 1);")
                                                sqlCmd.Append($"SET SQL_SAFE_UPDATES = 0;")
                                                sqlCmd.Append($"update csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } SET `cycletime` = '{ Monlastcycletime }' Where date = @select;")

                                                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                                'Update the last line for cycletime and insert current like now() - dateTimestamp
                                                Dim Montimediff1 = DateDiff(DateInterval.Second, Monnewdate, DateTime.Now())

                                                MySqlAccess.ExecuteNonQuery($"INSERT IGNORE INTO CSI_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } (status, time, cycletime, shift, date ) VALUES ('{ MonStatus }', { (MonTIME_s) }, '{ Montimediff1 }', '{ tempshift }', '{ Monlatestdate }')")

                                                Monlaststatustimestamp = Monnewdate
                                                laststatustimestamp = Monnewdate
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                            fileno = 1
                        Next
                    End If

                    'Code Below load Current SHIFT to MachinePerf Database from TMP Folder
                    Dim filePath As String = allconst.SERVER_ENET_PATH & allconst.TMP_FOLDER_PATH

                    If Directory.Exists(filePath) Then

                        Dim tmpfiles As String() = IO.Directory.GetFiles(filePath, keyvalAllMachine.Value.MonitoringFileName_)

                        If tmpfiles.Length > 0 Then

                            If File.Exists(tmpfiles(0)) Then

                                If File.GetLastWriteTime(tmpfiles(0)).Date.ToString("dd-MM-yyyy") = DateTime.Now.Date.ToString("dd-MM-yyyy") Then

                                    Dim fileData1() As String = File.ReadAllLines(tmpfiles(0))

                                    For Each lines1 As String In fileData1
                                        If lines1.Contains("_PARTNO") Or lines1.Contains("_DPRINT_") Or lines1.Contains("_OPERATOR") Or (lines1 = String.Empty) Or (lines1 = "") Then 'Or lines1.Contains("NO eMONITOR")
                                            'IF Line Contains PARTNo, DPRINT,OPerator or Empty then don't add that line to database
                                        Else
                                            'Add this line to database table
                                            If lines1.Contains(",") Then 'This if handles if the string we have is empty 

                                                Dim Splitlines1 As String() = lines1.Split(",")
                                                Dim joindate As String = Splitlines1(0) & " " & Splitlines1(1)
                                                Dim onlytime As String() = (Convert.ToDateTime(Splitlines1(1)).ToString("HH:mm:ss")).Split(":")

                                                'newDate = Convert.ToDateTime(joindate)
                                                newDate = DateTime.ParseExact(joindate, "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)

                                                Dim latestdate = newDate.ToString("yyyy-MM-dd HH:mm:ss")
                                                Dim Status As String = Splitlines1(2)

                                                If Status = "_CON" Then
                                                    Status = "CYCLE ON"
                                                ElseIf Status = "_COFF" Then
                                                    Status = "CYCLE OFF"
                                                ElseIf Status = "_SETUP" Then
                                                    Status = "SETUP"
                                                End If

                                                Dim TIME_s As Integer = Convert.ToInt32(onlytime(0)) * 3600 + Convert.ToInt32(onlytime(1)) * 60 + Convert.ToInt32(onlytime(2))

                                                'Select table and check for no of rows
                                                Dim dTable_SelectRows As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) };")

                                                If dTable_SelectRows.Rows.Count = 0 Then

                                                    Dim timediff = DateDiff(DateInterval.Second, newDate, DateTime.Now())

                                                    MySqlAccess.ExecuteNonQuery($"INSERT IGNORE INTO CSI_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } (status,time,cycletime,shift,date) VALUES ('{ Status }', { (TIME_s) }, '{ timediff }', '{ (tempshift + 1) }', '{ latestdate }')")

                                                    laststatustimestamp = newDate

                                                ElseIf dTable_SelectRows.Rows.Count > 0 Then

                                                    Dim lastcycletime = DateDiff(DateInterval.Second, laststatustimestamp, newDate)

                                                    Dim sqlCmd As Text.StringBuilder = New Text.StringBuilder()
                                                    sqlCmd.Append($"SET @select := (SELECT date FROM csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } order by date desc limit 1);")
                                                    sqlCmd.Append($"SET SQL_SAFE_UPDATES = 0;")
                                                    sqlCmd.Append($"update csi_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } SET `cycletime` = '{ lastcycletime }' Where date = @select;")

                                                    MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                                                    'Update the last line for cycletime and insert current like now() - dateTimestamp
                                                    Dim timediff1 = 0
                                                    timediff1 = DateDiff(DateInterval.Second, newDate, DateTime.Now())

                                                    MySqlAccess.ExecuteNonQuery($"INSERT IGNORE INTO CSI_machineperf.tbl_{ DisplayMachineList.Item(keyvalAllMachine.Value.Machinename_) } (status, time, cycletime, shift, date ) VALUES ('{ Status }', { (TIME_s) }, '{ timediff1 }', '{ (tempshift + 1) }', '{ latestdate }')")

                                                    laststatustimestamp = newDate
                                                End If
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If
                    'Delete last row of table andadd last row from fct_enet_livestatus() thread 
                End If
            Next
        Else
            'MessageBox.Show("Folder : " & SearchMonthFolder & " not exists")
        End If

        RefreshAllDevices()

    End Sub

    Public Function RenameMachine(machine As String) As String
        Dim res As String = machine

        For i = 32 To 47
            res = res.Replace(Chr(i), "_c" & i & "_")
        Next

        For i = 58 To 64
            res = res.Replace(Chr(i), "_c" & i & "_")
        Next

        For i = 91 To 96
            If i <> 95 Then
                res = res.Replace(Chr(i), "_c" & i & "_")
            End If
        Next

        For i = 123 To 126
            res = res.Replace(Chr(i), "_c" & i & "_")
        Next

        Return res
    End Function

    Private Sub CheckConnection(e As DataGridViewCellEventArgs)

        If (Not IsNothing(gridviewMachines.Rows(e.RowIndex).Cells(3).Value)) Then

            Dim IPENET As String = ""

            If File.Exists(path & "\sys\Networkenet_.csys") Then
                Using reader As StreamReader = New StreamReader(path & "\sys\Networkenet_.csys")
                    IPENET = reader.ReadLine
                    reader.Close()
                End Using
            End If

            Dim dt As DataTable = clt.Run(IPENET)

            gridviewMachines.Rows(e.RowIndex).Cells(5).Value = "Check IP"
            gridviewMachines.Rows(e.RowIndex).Cells(5).Style.BackColor = Color.Red

            If dt IsNot Nothing Then
                For Each machine As DataRow In dt.Rows

                    gridviewMachines.Rows(e.RowIndex).Cells(5).Value = "offLine"
                    gridviewMachines.Rows(e.RowIndex).Cells(5).Style.BackColor = Color.Red

                    If (machine.Item("machine")) = gridviewMachines.Rows(e.RowIndex).Cells(1).Value Then

                        If machine.Item("status") <> "NO EMONITOR" Then

                            gridviewMachines.Rows(e.RowIndex).Cells(5).Style.BackColor = Color.GreenYellow
                            gridviewMachines.Rows(e.RowIndex).Cells(5).Value = "Passed : " & machine.Item("status").ToString()
                            Exit For
                        End If
                    End If
                Next
            End If
        End If

    End Sub

End Class

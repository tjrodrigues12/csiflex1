Imports System.IO
Imports System.Net.NetworkInformation
Imports System.Text.RegularExpressions
Imports System.Windows
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities
Imports MySql.Data.MySqlClient

Partial Public Class SetupForm2

    Private treeViewClick As Boolean = False
    Private configIsLoading As Boolean = False
    Private isFilling As Boolean = False ' =true whene user select a row , =false after tree3 been filled

    Public deviceId As Integer
    Public DeviceType As String = String.Empty
    Public copy As String = ""

    Public Shared checked__treeview3 As New List(Of String)
    Public Shared checked__treeviewDevice As New List(Of String)
    Public Shared checked__treeviewUser As New List(Of String)
    Public Shared checked__treeviewCMP As New List(Of String)
    Public Shared checked__treeviewGroup As New List(Of String)
    Public Shared checked__treeviewGroup2 As New List(Of String)

    Private Sub DashboardTabSelected()

        RemoveHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck

        load_DeviceName()

        Load_groupes()

        Try
            If (treeviewDevices.SelectedNode.Text = treeviewDevices.Nodes(0).Text Or treeviewDevices.SelectedNode.Text = Nothing) Then
                Panel_dashConfig.Visible = False
                TV_LivestatusMachine.Visible = False
            End If
        Catch ex As Exception
            Panel_dashConfig.Visible = False
            TV_LivestatusMachine.Visible = False
        End Try

        TV_LivestatusMachine.Nodes.Clear()

        'add All Machines node
        TV_LivestatusMachine.Nodes.Add("All machines")

        For Each row As DataGridViewRow In gridviewMachines.Rows
            'add machine
            Dim node_ As New TreeNode, node_0 As New TreeNode
            If Not IsNothing(row.Cells("machines").Value) And Boolean.Parse(row.Cells("isMonitored").Value) Then
                node_.Name = row.Cells("machines").Value.ToString()
                node_.Text = row.Cells("machines").Value.ToString()
                node_.Tag = row.Cells("Id").Value.ToString()
                TV_LivestatusMachine.Nodes(0).Nodes.Add(node_)
            End If
        Next

        'add other groups
        For Each node In treeviewGroupsOfMachines.Nodes
            TV_LivestatusMachine.Nodes.Add(node.clone)
        Next

        AddHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck

        Dim nodes As TreeNodeCollection = treeviewDevices.Nodes(0).Nodes

        If nodes.Count > 0 Then
            ' Select the root node
            treeviewDevices.SelectedNode = treeviewDevices.Nodes(0).Nodes(0)
            configIsLoading = True

            load_userConfig(CSI_Library.CSI_Library.MySqlConnectionString)
            configIsLoading = False
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AddDevice.Show()
        send_http_req()
    End Sub

    Private Sub DeleteFuctionsForDashboards(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles treeviewDevices.KeyDown

        If e.KeyCode = Keys.Delete Then

            Dim tn As TreeNode = treeviewDevices.SelectedNode

            Dim iRet = MessageBox.Show("Are you certain you want to delete " & tn.Text & "?", "Delete Device", MessageBoxButton.YesNo, MessageBoxImage.Question)

            If iRet = MessageBoxResult.Yes Then

                Dim sqlCmd As New Text.StringBuilder()
                sqlCmd.Append($"delete from CSI_database.tbl_devices       WHERE Id       = { deviceId }; ")
                sqlCmd.Append($"delete from CSI_database.tbl_deviceconfig  WHERE DeviceId = { deviceId }; ")
                sqlCmd.Append($"delete from CSI_database.tbl_deviceconfig2 WHERE DeviceId = { deviceId }; ")
                sqlCmd.Append($"delete from CSI_database.tbl_messages      WHERE DeviceId = { deviceId }; ")
                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                treeviewDevices.SelectedNode.Remove()
                send_http_req()
            End If
        End If

    End Sub

    Private Sub Devices_TV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles treeviewDevices.NodeMouseClick

        If e.Button = Windows.Forms.MouseButtons.Right Then

            Try
                If e.Node.Text <> treeviewDevices.SelectedNode.Text Then
                    treeviewDevices.SelectedNode = e.Node
                End If

                ' Point where the mouse is clicked.
                Dim p As New Drawing.Point(e.X, e.Y)

                ' Get the node that the user has clicked.
                Dim node As TreeNode = treeviewDevices.GetNodeAt(p)
                If node IsNot Nothing Then

                    ' Select the node the user has clicked.
                    ' The node appears selected until the menu is displayed on the screen.
                    Dim m_OldSelectNode = treeviewDevices.SelectedNode
                    treeviewDevices.SelectedNode = node

                    ' Find the appropriate ContextMenu depending on the selected node.
                    If (node.Level = 0) Then
                        Dim cms = New ContextMenuStrip
                        Dim item1 = cms.Items.Add("Add Device")
                        item1.Tag = 1

                        AddHandler item1.Click, AddressOf menuChoice

                        cms.Show(treeviewDevices, p)

                    ElseIf (node.Level = 1) Then
                        Dim cms = New ContextMenuStrip

                        If CB__DeviceType.Text = "LR1" Then
                            Dim item5 = cms.Items.Add("Change Target Server")
                            Dim item8 = cms.Items.Add("Change Date and Time")
                            Dim item6 = cms.Items.Add("Duplicate Device")
                            Dim item7 = cms.Items.Add("Reboot Device")
                            Dim item10 = cms.Items.Add("Refresh Device")
                            Dim item11 = cms.Items.Add("Update Device")
                            Dim item12 = cms.Items.Add("Change network setting")
                            Dim item2 = cms.Items.Add("Remove Device")
                            Dim item14 = cms.Items.Add("See Preview")
                            item2.Tag = 2
                            item5.Tag = 5
                            item6.Tag = 6
                            item7.Tag = 7
                            item8.Tag = 8
                            item10.Tag = 10
                            item11.Tag = 11
                            item12.Tag = 12
                            item14.Tag = 14
                            AddHandler item5.Click, AddressOf menuChoice
                            AddHandler item6.Click, AddressOf menuChoice
                            AddHandler item7.Click, AddressOf menuChoice
                            AddHandler item2.Click, AddressOf menuChoice
                            AddHandler item8.Click, AddressOf menuChoice
                            AddHandler item10.Click, AddressOf menuChoice
                            AddHandler item11.Click, AddressOf menuChoice
                            AddHandler item12.Click, AddressOf menuChoice
                            AddHandler item14.Click, AddressOf menuChoice

                        Else 'CB__DeviceType.Text = "" Then
                            Dim item6 = cms.Items.Add("Duplicate Device")
                            item6.Tag = 6
                            AddHandler item6.Click, AddressOf menuChoice

                            If treeviewDevices.SelectedNode.Text <> "Local Host" Then
                                Dim item2 = cms.Items.Add("Remove Device")
                                item2.Tag = 2
                                AddHandler item2.Click, AddressOf menuChoice
                            End If

                        End If
                        cms.Show(treeviewDevices, p)
                    End If

                End If
            Catch ex As Exception
                MessageBox.Show("Error while adding device, see log")
                CSI_Lib.LogServerError("Error adding device to dashboard:" + ex.Message, 1)
            End Try
        End If
    End Sub

    Private Sub Devices_TV_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treeviewDevices.AfterSelect

        If (treeviewDevices.SelectedNode.Text = treeviewDevices.Nodes(0).Text) Then
            Panel_dashConfig.Visible = False
            TV_LivestatusMachine.Visible = False
        Else
            Panel_dashConfig.Visible = True
            TV_LivestatusMachine.Visible = True
            btnRSSDeleteMessage.Enabled = False
            txtRSSUserMessageText.Text = ""
            txtRSSUserMessageText.Visible = False
            grpRSSMessageType.Enabled = False
            configIsLoading = True

            treeViewClick = False

            load_userConfig(CSI_Library.CSI_Library.MySqlConnectionString)

            configIsLoading = False

            If Not (dgvRSSMessages.Rows.Count = 0) Then
                btnRSSDeleteMessage.Enabled = True
            End If
        End If

    End Sub

    Private Sub BTN_ping_Click(sender As Object, e As EventArgs) Handles BTN_ping.Click

        Dim DeviceIP As String = IP_TB.Text

        Dim macRegex As Regex = New Regex("^([0-9a-fA-F]{2}(?:(?:-[0-9a-fA-F]{2}){5}|(?::[0-9a-fA-F]{2}){5}|[0-9a-fA-F]{10}))$")

        Dim match As Match = macRegex.Match(DeviceIP)

        If match.Success Then
            DeviceIP = convert_mac_to_ip(DeviceIP.Replace(":", "-"))
        End If

        If DeviceIP <> "0.0.0.0" And DeviceIP <> "" Then
            Dim result As Boolean = pingHost(DeviceIP)
            If result = True Then
                BTN_ping.Text = "Device responded"
                BTN_ping.BackColor = Color.LightGreen
            Else
                BTN_ping.Text = "No response"
                BTN_ping.BackColor = Color.Red
            End If
        Else
            BTN_ping.Text = "Bad IP/MAC"
            BTN_ping.BackColor = Color.Red
        End If

    End Sub

    Private Sub TreeView4_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TV_LivestatusMachine.AfterCheck

        If e.Node.ForeColor = Color.Red And treeViewClick Then
            RemoveHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck
            e.Node.Checked = Not e.Node.Checked
            AddHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck
            treeViewClick = False
            Return
        End If

        LiveStatusMachineAfterCheck(e.Node)

    End Sub

    Private Sub TreeView4_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TV_LivestatusMachine.NodeMouseDoubleClick, TV_LivestatusMachine.NodeMouseClick

        treeViewClick = True

        If e.Button = Windows.Forms.MouseButtons.Right Then
            TV_LivestatusMachine.SelectedNode = e.Node
        End If

    End Sub

    Private Sub btnAdvSettings_Click(sender As Object, e As EventArgs) Handles btnAdvSettings.Click
        config_.ShowDialog()
        dashboardDevice.ReloadDevice()
    End Sub


    Private Sub btnRSSAddMessage_Click(sender As Object, e As EventArgs) Handles btnRSSAddMessage.Click

        rdbRSSUserMessage.Checked = False
        rdbRSSSystemMessage.Checked = False

        btnRSSDeleteMessage.Enabled = True

        dgvRSSMessages.Rows.Add()
        dgvRSSMessages.SelectedCells.Item(0).Selected = False
        dgvRSSMessages.Rows(dgvRSSMessages.Rows.Count - 1).Cells(0).Selected = True
        If Not (dgvRSSMessages.Rows.Count - 1 = 0) Then
            dgvRSSMessages.Rows(dgvRSSMessages.Rows.Count - 2).Cells(0).Selected = False
        End If
        dgvRSSMessages.SelectedCells.Item(0).Value = "Select a message type"
        dgvRSSMessages.Rows(dgvRSSMessages.Rows.Count - 1).Cells(1).Value = dgvRSSMessages.Rows.Count.ToString()
        dgvRSSMessages.Enabled = False

        grpRSSMessageType.Enabled = True
        rdbRSSUserMessage.Checked = False
        rdbRSSSystemMessage.Checked = False

        txtRSSUserMessageText.Text = ""
        txtRSSUserMessageText.Visible = False

        chkRSSBestCycleOn.Checked = True

        rdbRSSDay.Checked = False
        rdbRSSWeek.Checked = False
        rdbRSSMonth.Checked = False

        btnRSSMessageUp.Enabled = False
        btnRSSMessageDown.Enabled = False

        btnRSSAddMessage.Enabled = False

    End Sub

    Private Sub btnRSSDeleteMessage_Click(sender As Object, e As EventArgs) Handles btnRSSDeleteMessage.Click

        Dim temp As String = ""
        Dim priority As String = dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString()
        Dim message As String = dgvRSSMessages.SelectedCells.Item(0).Value.ToString()

        If (dgvRSSMessages.SelectedCells.Item(0).Value.ToString() = "Daily Best CYCLE ON Machine") Then
            message = "_COND" + message
        End If
        If (dgvRSSMessages.SelectedCells.Item(0).Value.ToString() = "Weekly Best CYCLE ON Machine") Then
            message = "_CONW" + message
        End If
        If (dgvRSSMessages.SelectedCells.Item(0).Value.ToString() = "Monthly Best CYCLE ON Machine") Then
            message = "_CONM" + message
        End If


        MySqlAccess.ExecuteNonQuery($"DELETE FROM CSI_Database.tbl_messages WHERE DeviceId = { deviceId } AND message = '{ message }'")
        'Try
        '    Dim cmdsql As New MySqlCommand($"DELETE FROM CSI_Database.tbl_messages WHERE messages = '{ message }' AND IP_adress = '{ IP_TB.Text }' AND Priority = '{ priority }'", cntsql)
        '    cmdsql.ExecuteNonQuery()
        'Catch
        'End Try

        dgvRSSMessages.Rows.Remove(dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex))

        If dgvRSSMessages.Rows.Count = 0 Then

            btnRSSDeleteMessage.Enabled = False
            rdbRSSUserMessage.Checked = False

            txtRSSUserMessageText.Visible = False
            txtRSSUserMessageText.Text = ""
            grpRSSMessageType.Enabled = False
            lblMessageText.Visible = False
        End If

        Try
            If dgvRSSMessages.SelectedCells.Item(0).Value = Nothing Then
            End If
        Catch ex As Exception
            Try
                dgvRSSMessages.Rows(dgvRSSMessages.Rows.Count - 1).Cells(0).Selected = True
            Catch ex2 As Exception

            End Try

        End Try

        'For Each cell As DataGridViewCell In dgvRSSMessages.Rows.Item(0).Cells
        '    MessageBox.Show(cell()
        'Next
        If Not (dgvRSSMessages.Rows.Count = 0) Then
            If Not (dgvRSSMessages.SelectedCells.Item(0).RowIndex = 0) Then
                For i = dgvRSSMessages.SelectedCells.Item(0).RowIndex To dgvRSSMessages.Rows.Count - dgvRSSMessages.SelectedCells.Item(0).RowIndex
                    dgvRSSMessages.Rows(i).Cells(1).Value = i + 1
                Next
            Else
                'dgvRSSMessages.Rows(0).Cells(1).Value = 1
                For i = 0 To dgvRSSMessages.Rows.Count - 1
                    dgvRSSMessages.Rows(i).Cells(1).Value = i + 1
                Next

            End If

        End If

        For i = 0 To dgvRSSMessages.Rows.Count - 1
            Try
                MySqlAccess.ExecuteNonQuery($"UPDATE CSI_Database.tbl_messages SET Priority = '{ dgvRSSMessages.Rows(i).Cells(1).Value.ToString() }' WHERE message = '{ dgvRSSMessages.Rows(i).Cells(0).Value.ToString() }'")

                'Dim cmdsql As New MySqlCommand("update CSI_Database.tbl_messages set Priority = '" + dgvRSSMessages.Rows(i).Cells(1).Value.ToString() + "' where messages = '" + dgvRSSMessages.Rows(i).Cells(0).Value.ToString() + "'", cntsql)
                'cmdsql.ExecuteNonQuery()
            Catch
            End Try

        Next
        btnRSSAddMessage.Enabled = True
        send_http_req()
    End Sub


    Private Sub rdbRSSTypeMessage_CheckedChanged(sender As Object, e As EventArgs) Handles rdbRSSUserMessage.CheckedChanged, rdbRSSSystemMessage.CheckedChanged

        If rdbRSSUserMessage.Checked = True Then

            txtRSSUserMessageText.Visible = True
            lblMessageText.Visible = True
            chkRSSBestCycleOn.Enabled = False
            chkRSSBestCycleOn.Visible = False
            lblBestCycleOnMachine.Visible = False

            grpRSSBestCycleOn.Visible = False
            grpRSSBestCycleOn.Enabled = False

            If Not (dgvRSSMessages.Rows.Count = 0) Then

                If dgvRSSMessages.SelectedCells.Item(0).ColumnIndex = 0 Then
                    If dgvRSSMessages.SelectedCells.Item(0).Value = "Select a period" Then
                        rdbRSSDay.Checked = False
                        rdbRSSWeek.Checked = False
                        rdbRSSMonth.Checked = False
                        chkRSSBestCycleOn.Checked = False
                        txtRSSUserMessageText.Text = ""
                        dgvRSSMessages.SelectedCells.Item(0).Value = "Enter a message text"
                    End If
                    If dgvRSSMessages.SelectedCells.Item(0).Value = "Select a message type" Then
                        dgvRSSMessages.SelectedCells.Item(0).Value = "Enter a message text"
                    End If
                Else
                    If dgvRSSMessages.SelectedCells.Item(0).Value = "" Then
                        dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex).Cells(0).Value = "Enter a message text"
                    End If
                End If

            End If

        ElseIf rdbRSSSystemMessage.Checked = True Then

            chkRSSBestCycleOn.Enabled = True
            chkRSSBestCycleOn.Visible = True
            lblBestCycleOnMachine.Visible = True

            grpRSSBestCycleOn.Visible = True
            grpRSSBestCycleOn.Enabled = True

            txtRSSUserMessageText.Visible = False
            lblMessageText.Visible = False
            If dgvRSSMessages.SelectedCells.Item(0).ColumnIndex = 0 Then
                If dgvRSSMessages.SelectedCells.Item(0).Value = "Select a message type" Then
                    dgvRSSMessages.SelectedCells.Item(0).Value = "Select a system message "
                End If
            Else
                If dgvRSSMessages.SelectedCells.Item(0).Value = "" Then
                    dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex).Cells(0).Value = "Select a system message "
                End If
            End If
        End If

        btnRSSValidate.Enabled = True

    End Sub

    Private Sub txtRSSUserMessageText_TextChanged(sender As Object, e As EventArgs) Handles txtRSSUserMessageText.TextChanged

        If txtRSSUserMessageText.Text = "" Then
            btnRSSValidate.Enabled = False
        Else
            btnRSSValidate.Enabled = True
        End If

    End Sub

    Private Sub rdbRSSPeriod_Selected(sender As Object, e As EventArgs) Handles rdbRSSDay.CheckedChanged, rdbRSSMonth.CheckedChanged, rdbRSSWeek.CheckedChanged

        Dim radioButton As RadioButton = sender

        Select Case radioButton.Text
            Case "Day"
                txtRSSUserMessageText.Text = "Daily Best CYCLE ON Machine"
            Case "Week"
                txtRSSUserMessageText.Text = "Weekly Best CYCLE ON Machine"
            Case "Month"
                txtRSSUserMessageText.Text = "Monthly Best CYCLE ON Machine"
        End Select

    End Sub

    Private Sub btnRSSMessageUp_Click(sender As Object, e As EventArgs) Handles btnRSSMessageUp.Click

        Dim temptext As String = ""

        Try
            If Not dgvRSSMessages.SelectedCells.Item(0).Value = Nothing Then

                If dgvRSSMessages.SelectedCells.Item(0).ColumnIndex = 0 Then

                    temptext = dgvRSSMessages.SelectedCells.Item(0).Value.ToString()
                    dgvRSSMessages.SelectedCells.Item(0).Value = dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex - 1).Cells.Item(0).Value
                    dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex - 1).Cells.Item(0).Value = temptext
                    RemoveHandler dgvRSSMessages.SelectionChanged, AddressOf dgvRSSMessages_SelectionChanged
                    Dim temp As Integer = dgvRSSMessages.SelectedCells.Item(0).RowIndex
                    dgvRSSMessages.Rows(temp).Cells.Item(0).Selected = False
                    dgvRSSMessages.Rows(temp - 1).Cells.Item(0).Selected = True
                    AddHandler dgvRSSMessages.SelectionChanged, AddressOf dgvRSSMessages_SelectionChanged
                End If

                Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                cntsql.Open()
                Try
                    Dim cmdsql As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = 'temp' where Message = '" + dgvRSSMessages.SelectedCells.Item(0).Value.ToString() + "'", cntsql)
                    cmdsql.Parameters.Add(New MySqlParameter("@message", txtRSSUserMessageText.Text))
                    cmdsql.ExecuteNonQuery()
                    Dim cmdsql2 As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = '" + dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex + 1).Cells(1).Value.ToString() + "' where Priority = '" + dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "'", cntsql)
                    cmdsql2.Parameters.Add(New MySqlParameter("@message", txtRSSUserMessageText.Text))
                    cmdsql2.ExecuteNonQuery()
                    Dim cmdsql3 As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = '" + dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "' where Priority = 'temp'", cntsql)
                    cmdsql3.Parameters.Add(New MySqlParameter("@message", txtRSSUserMessageText.Text))
                    cmdsql3.ExecuteNonQuery()
                Catch ex As Exception

                Finally
                    cntsql.Close()
                End Try



            End If
        Catch
        End Try
    End Sub

    Private Sub btnRSSMessageDown_Click(sender As Object, e As EventArgs) Handles btnRSSMessageDown.Click

        'Dim indexNow = dgvRSSMessages.SelectedCells.Item(0).RowIndex

        'If indexNow = dgvRSSMessages.Rows.Count Then Return

        'Dim message = dgvRSSMessages.SelectedCells.Item(0).Value.ToString()


        Dim temptext As String = ""
        Try
            If Not dgvRSSMessages.SelectedCells.Item(0).Value = Nothing Then

                If dgvRSSMessages.SelectedCells.Item(0).ColumnIndex = 0 Then

                    temptext = dgvRSSMessages.SelectedCells.Item(0).Value.ToString()
                    dgvRSSMessages.SelectedCells.Item(0).Value = dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex + 1).Cells.Item(0).Value
                    dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex + 1).Cells.Item(0).Value = temptext
                    RemoveHandler dgvRSSMessages.SelectionChanged, AddressOf dgvRSSMessages_SelectionChanged
                    Dim temp As Integer = dgvRSSMessages.SelectedCells.Item(0).RowIndex
                    dgvRSSMessages.Rows(temp).Cells.Item(0).Selected = False
                    dgvRSSMessages.Rows(temp + 1).Cells.Item(0).Selected = True
                    AddHandler dgvRSSMessages.SelectionChanged, AddressOf dgvRSSMessages_SelectionChanged
                End If

                Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                cntsql.Open()
                Try
                    Dim cmdsql As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = 'temp' where Message = '" + dgvRSSMessages.SelectedCells.Item(0).Value.ToString() + "'", cntsql)
                    cmdsql.Parameters.Add(New MySqlParameter("@message", txtRSSUserMessageText.Text))
                    cmdsql.ExecuteNonQuery()
                    Dim cmdsql2 As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = '" + dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex - 1).Cells(1).Value.ToString() + "' where Priority = '" + dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "'", cntsql)
                    cmdsql2.Parameters.Add(New MySqlParameter("@message", txtRSSUserMessageText.Text))
                    cmdsql2.ExecuteNonQuery()
                    Dim cmdsql3 As New MySqlCommand("update CSI_Database.tbl_messages SET Priority = '" + dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString() + "' where Priority = 'temp'", cntsql)
                    cmdsql3.Parameters.Add(New MySqlParameter("@message", txtRSSUserMessageText.Text))
                    cmdsql3.ExecuteNonQuery()
                Catch ex As Exception

                Finally
                    cntsql.Close()
                End Try



            End If
        Catch
        End Try
    End Sub

    Private Sub btnRSSValidate_Click(sender As Object, e As EventArgs) Handles btnRSSValidate.Click

        Dim sqlCmd As Text.StringBuilder = New System.Text.StringBuilder()

        Dim priority As String = dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex).Cells(1).Value.ToString()
        Dim oldMessage As String = dgvRSSMessages.SelectedCells.Item(0).Value.ToString()
        Dim newMessage As String = txtRSSUserMessageText.Text

        If rdbRSSSystemMessage.Checked Then

            If rdbRSSDay.Checked Then
                oldMessage = "_COND" + oldMessage
                newMessage = "_COND" + newMessage
            ElseIf rdbRSSWeek.Checked Then
                oldMessage = "_CONW" + oldMessage
                newMessage = "_CONW" + newMessage
            ElseIf rdbRSSMonth.Checked Then
                oldMessage = "_CONM" + oldMessage
                newMessage = "_CONM" + newMessage
            End If

        End If

        If Not String.IsNullOrEmpty(newMessage) Then 'dgvRSSMessages.SelectedCells.Item(0).Value = "Enter a message text" Or dgvRSSMessages.SelectedCells.Item(0).Value = "Select a period" Then

            sqlCmd.Append($"INSERT INTO CSI_Database.tbl_messages ")
            sqlCmd.Append($" (                                    ")
            sqlCmd.Append($"   DeviceId   ,                       ")
            sqlCmd.Append($"   IP_Adress  ,                       ")
            sqlCmd.Append($"   Message    ,                       ")
            sqlCmd.Append($"   Priority                           ")
            sqlCmd.Append($" ) VALUES (                           ")
            sqlCmd.Append($"    { deviceId }     ,                ")
            sqlCmd.Append($"   '{ IP_TB.Text }'  ,                ")
            sqlCmd.Append($"   @message          ,                ")
            sqlCmd.Append($"   '{ priority }'                     ")
            sqlCmd.Append($" )                                    ")
            sqlCmd.Append($" ON DUPLICATE KEY                     ")
            sqlCmd.Append($"      UPDATE Message = @message       ")

        Else
            sqlCmd.Append($"UPDATE CSI_Database.tbl_messages ")
            sqlCmd.Append($" SET                             ")
            sqlCmd.Append($"     Message  = @message,        ")
            sqlCmd.Append($"     Priority = '{ priority }'   ")
            sqlCmd.Append($" WHERE                           ")
            sqlCmd.Append($"     DeviceId =  { deviceId }    ")
            sqlCmd.Append($" AND Message  = '{ oldMessage }' ")
        End If

        Dim command As New MySqlCommand(sqlCmd.ToString())
        command.Parameters.Add(New MySqlParameter("@message", newMessage))

        MySqlAccess.ExecuteNonQuery(command)

        If dgvRSSMessages.SelectedCells.Item(0).ColumnIndex = 0 Then
            dgvRSSMessages.SelectedCells.Item(0).Value = txtRSSUserMessageText.Text
        Else
            dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex).Cells(0).Value = txtRSSUserMessageText.Text
        End If
        If txtRSSUserMessageText.Text = "" Then
            btnRSSValidate.Enabled = False
        Else
            btnRSSValidate.Enabled = True
        End If
        txtRSSUserMessageText.Text = ""
        grpRSSBestCycleOn.Visible = False
        txtRSSUserMessageText.Visible = False
        lblBestCycleOnMachine.Visible = False
        chkRSSBestCycleOn.Visible = False
        grpRSSMessageType.Enabled = False
        lblMessageText.Visible = False
        dgvRSSMessages.Enabled = True
        btnRSSMessageUp.Enabled = True
        btnRSSMessageDown.Enabled = True
        btnRSSAddMessage.Enabled = True

        RefreshDevice(deviceId)

    End Sub




    Private Sub Browse_Logo_Click(sender As Object, e As EventArgs) Handles Browse_Logo.Click

        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.ShowDialog()

        Dim newLogoFullPath As String = ""

        txtLogoPath.Text = openFileDialog1.SafeFileName.ToString()

        newLogoFullPath = openFileDialog1.FileName.ToString()

        Try
            Dim logoFolder = IO.Path.Combine(CSI_Library.CSI_Library.serverRootPath, "html", "html", "img")
            Dim logoFullPath = IO.Path.Combine(logoFolder, txtLogoPath.Text)

            'If Not Directory.Exists(logoFolder) Then
            '    Directory.CreateDirectory(logoFolder)
            'End If

            If (File.Exists(logoFullPath)) Then
                File.Delete(logoFullPath)
            End If

            FileCopy(newLogoFullPath, logoFullPath)

        Catch ex As Exception
            Log.Error(ex)
        End Try

        Try

            MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_deviceConfig SET detail_customlogo = '{ txtLogoPath.Text }' WHERE deviceId = { deviceId }")
            UpdateDevice(Me, New EventArgs())

        Catch ex As Exception
            Log.Error(ex)
        End Try

    End Sub



    Private Sub UpdateDevice(sender As Object, e As EventArgs) Handles Temp_CB.CheckedChanged,
                                                                       txtDeviceName.Validated,
                                                                       LSFullscreen_CB.CheckedChanged,
                                                                       Logo_TB.CheckedChanged,
                                                                       IP_TB.Validated,
                                                                       IF_Rot_CB.CheckedChanged,
                                                                       Degree_CB.SelectedIndexChanged,
                                                                       DateTime_TB.CheckedChanged,
                                                                       chkLogoBarHidden.CheckedChanged,
                                                                       chkDarkTheme.CheckedChanged,
                                                                       IF_ON.CheckedChanged,
                                                                       IF_OFF.CheckedChanged,
                                                                       chkRSSMessageOnOff.CheckedChanged,
                                                                       CB__DeviceType.SelectedIndexChanged,
                                                                       Url_TB.Validated,
                                                                       TB_format.Validated,
                                                                       Sec_TB.Validated,
                                                                       txtLogoPath.Validated,
                                                                       City_TB.Validated

        If dashboardDevice IsNot Nothing And Not inLoadingDeviceMode Then

            If Not dashboardDevice.IpAddress = "" Then

                If dashboardDevice.IpAddress <> IP_TB.Text Then
                    dashboardDevice.UpdateIpAddress(IP_TB.Text)
                    RefreshDevice(deviceId)
                    Return
                End If

                dashboardDevice.DeviceName = txtDeviceName.Text

                dashboardDevice.LiveStatusDelay = True
                dashboardDevice.DetailLiveStatusDelay = Sec_TB.Text
                If String.IsNullOrEmpty(Sec_TB.Text) Then
                    dashboardDevice.DetailLiveStatusDelay = "0"
                End If

                dashboardDevice.DateTime = DateTime_TB.Checked
                dashboardDevice.DateFormat = TB_format.Text

                dashboardDevice.Temperature = Temp_CB.Checked
                dashboardDevice.Degree = Degree_CB.SelectedItem.ToString()

                lblTemperature.Text = ""

                If dashboardDevice.Temperature And City_TB.Text <> "" Then
                    Dim ret = CSIFLEX.Utilities.OpenWeather.GetTemperature(City_TB.Text, dashboardDevice.Degree)

                    If ret.Contains("(404)") Then
                        lblTemperature.Text = "City not found"
                    Else
                        lblTemperature.Text = ret
                        dashboardDevice.DetailTemperature = City_TB.Text.Replace(" ", "")
                    End If
                Else
                    dashboardDevice.DetailTemperature = City_TB.Text
                End If

                dashboardDevice.CustomLogo = Logo_TB.Checked
                dashboardDevice.DetailCustomLogo = txtLogoPath.Text

                dashboardDevice.Messages = chkRSSMessageOnOff.Checked

                dashboardDevice.LogoBarHidden = chkLogoBarHidden.Checked
                dashboardDevice.DarkTheme = chkDarkTheme.Checked

                dashboardDevice.IFrame = IF_ON.Checked
                dashboardDevice.DetailIFrame = Url_TB.Text

                If dashboardDevice.IFrame Then
                    IF_Rot_CB.Enabled = True
                Else
                    IF_Rot_CB.Checked = False
                    IF_Rot_CB.Enabled = False
                End If

                dashboardDevice.FullScreen = LSFullscreen_CB.Checked
                dashboardDevice.Rotation = IF_Rot_CB.Checked

                dashboardDevice.DeviceType = "Computer"
                If CB__DeviceType.SelectedIndex = 1 Then
                    dashboardDevice.DeviceType = "LR1"
                End If

                Try
                    dashboardDevice.SaveDevice()

                    treeviewDevices.SelectedNode.Text = txtDeviceName.Text

                    'Dim sqlCd = $"UPDATE CSI_database.tbl_messages SET name = '{ txtDeviceName.Text }' WHERE name = '{ treeviewDevices.SelectedNode.Text }'"

                Catch ex As Exception

                    MsgBox(" Could not update the settings : " & ex.Message)
                    CSI_Lib.LogServerError(" Could not update the settings : " & ex.Message, 1)
                End Try

                RefreshDevice(deviceId)
            End If
        End If

    End Sub


    Public Sub load_DeviceName()

        Dim dTable_name = MySqlAccess.GetDataTable("SELECT deviceId, name FROM csi_database.tbl_deviceconfig")

        treeviewDevices.Nodes.Item(0).Nodes.Clear()
        For Each row As DataRow In dTable_name.Rows
            treeviewDevices.Nodes.Item(0).Nodes.Add(row.Item(0).ToString(), row.Item(1).ToString())
        Next

    End Sub

    Private Sub menuChoice(ByVal sender As Object, ByVal e As EventArgs)

        Dim item = CType(sender, ToolStripMenuItem)
        Dim selection = CInt(item.Tag)

        If (selection = 1) Then
            AddDevice.Show()
            send_http_req()
        End If

        If (selection = 2) Then

            Dim tn As TreeNode = treeviewDevices.SelectedNode
            Dim iRet = MessageBox.Show("Are you certain you want to delete " & tn.Text & "?", "Delete Device", MessageBoxButton.YesNo, MessageBoxImage.Question)

            If iRet = MessageBoxResult.Yes Then

                'Dim device As New DashboardDevice(IP_TB.Text)
                Dim device As New DashboardDevice(CInt(tn.Name))
                device.DeleteDevice()

                treeviewDevices.SelectedNode.Remove()
                send_http_req()

            End If
        End If

        If (selection = 3) Then
            treeviewDevices.Nodes.Add("Group " + treeviewDevices.Nodes.Count.ToString())
        End If

        If (selection = 4) Then
            'Devices_TV.LabelEdit = True
            'Devices_TV.SelectedNode.BeginEdit()
            'While Devices_TV.SelectedNode.IsEditing
            'txtDeviceName.
            'End While
            'txtDeviceName.Text = Devices_TV.SelectedNode.Text
            'Devices_TV.LabelEdit = False
        End If

        If (selection = 5) Then
            targetServer.Show()
        End If

        If (selection = 6) Then 'Duplicate a Device Code Here
            copy = treeviewDevices.SelectedNode.Text   'This saves name of the Device in a variable
            DeviceType = CB__DeviceType.SelectedItem.ToString()
            CSI_Library.CSI_Library.DuplicateDevice = True

            AddDevice.Show()
        End If

        If (selection = 7) Then
            targetServer.reboot()
        End If

        If (selection = 11) Then
            targetServer.updateSoft()
        End If

        If (selection = 12) Then
            targetServer.updateNetwork()
        End If

        If (selection = 8) Then
            TimeDate.Show()
        End If

        If (selection = 9) Then
            SockConn.Show()
        End If

        If (selection = 10) Then
            RefreshDevice(deviceId)
        End If
        If (selection = 14) Then
            'MessageBox.Show("Preview of Raspberry Pie is Selected")
            Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            Try
                cntsql.Open()
                Dim mysql_str As String = "SELECT devicetype FROM CSI_database.tbl_deviceconfig2 where IP_adress = '" & Me.IP_TB.Text & "' and name = '" + Me.txtDeviceName.Text + "'"
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
        End If
    End Sub


    Private Sub LiveStatusMachineAfterCheck(treeNode As TreeNode)

        If isFilling = False Then
            Dim added_machine As New List(Of String)

            RemoveHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck

            If configIsLoading = False Then
                If treeNode.Text = "All machines" Then
                    If treeNode.Checked = False Then

                        For Each n As TreeNode In TV_LivestatusMachine.Nodes            '(0).Nodes
                            n.Checked = False
                        Next

                        dashboardDevice.Machines = ""
                        dashboardDevice.Groups = ""
                        dashboardDevice.SaveDevice()

                    End If
                End If
            End If

            Call CheckAllChildNodes(treeNode)

            For Each node As TreeNode In TV_LivestatusMachine.Nodes
                CheckAllNodes(node, treeNode)
            Next

            If treeNode.Checked Then
                If treeNode.Parent Is Nothing = False Then
                    Dim allChecked As Boolean = True

                    Call IsEveryChildChecked(treeNode.Parent, allChecked)

                    If allChecked Then
                        treeNode.Parent.Checked = True
                        Call ShouldParentsBeChecked(treeNode.Parent)
                    End If
                End If
            Else
                Dim parentNode As TreeNode = treeNode.Parent
                While parentNode Is Nothing = False
                    parentNode.Checked = False
                    parentNode = parentNode.Parent
                End While
            End If

            'Partially Selected Checkbox Logic is here 
            Dim SelectedMachineCount As Integer
            Dim NoofGrps As Integer
            Dim AllMachineCount As Integer
            Dim totalgrp As Integer
            Dim groups_ As String = ""

            totalgrp = 0
            SelectedMachineCount = 0
            NoofGrps = 0
            AllMachineCount = 0

            'All Machines
            For Each SNode As TreeNode In TV_LivestatusMachine.Nodes(0).Nodes
                If SNode.Checked Then
                    SelectedMachineCount += 1
                End If
                AllMachineCount += 1
            Next

            If SelectedMachineCount < AllMachineCount And SelectedMachineCount > 0 Then
                TV_LivestatusMachine.Nodes(0).ForeColor = Color.DarkGray
                TV_LivestatusMachine.Nodes(0).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                TV_LivestatusMachine.Nodes(0).Text += String.Empty
            ElseIf SelectedMachineCount = AllMachineCount And SelectedMachineCount > 0 Then
                groups_ += TV_LivestatusMachine.Nodes(0).Text & ", "
                TV_LivestatusMachine.Nodes(0).ForeColor = Color.Black
                TV_LivestatusMachine.Nodes(0).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                TV_LivestatusMachine.Nodes(0).Text += String.Empty
            Else
                TV_LivestatusMachine.Nodes(0).ForeColor = Color.Black
                TV_LivestatusMachine.Nodes(0).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Regular)
            End If

            'Group of Machines
            NoofGrps = TV_LivestatusMachine.Nodes(1).Nodes.Count
            For Each TotalGroups As TreeNode In TV_LivestatusMachine.Nodes(1).Nodes
                If TotalGroups.Checked Then
                    totalgrp += 1
                End If
            Next

            If totalgrp < NoofGrps And totalgrp > 0 Then
                TV_LivestatusMachine.Nodes(1).ForeColor = Color.DarkGray
                TV_LivestatusMachine.Nodes(1).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                TV_LivestatusMachine.Nodes(1).Text += String.Empty
            ElseIf totalgrp = NoofGrps And totalgrp > 0 Then
                TV_LivestatusMachine.Nodes(1).ForeColor = Color.Black
                TV_LivestatusMachine.Nodes(1).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                TV_LivestatusMachine.Nodes(1).Text += String.Empty
            Else
                TV_LivestatusMachine.Nodes(1).ForeColor = Color.Black
                TV_LivestatusMachine.Nodes(1).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Regular)
            End If

            'Particular Group Machines
            Dim Groups(NoofGrps - 1) As Integer

            For grp As Integer = 0 To (Groups.Length - 1)
                Dim SelectedGroupCount As Integer
                SelectedGroupCount = 0
                Groups.SetValue(TV_LivestatusMachine.Nodes(1).Nodes(grp).Nodes.Count, grp)
                For Each GNode As TreeNode In TV_LivestatusMachine.Nodes(1).Nodes(grp).Nodes
                    If GNode.Checked Then
                        SelectedGroupCount += 1
                    End If
                Next
                If SelectedGroupCount < Groups(grp) And SelectedGroupCount > 0 Then
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).Checked = False
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).ForeColor = Color.DarkGray
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).Text += String.Empty
                    TV_LivestatusMachine.Nodes(1).ForeColor = Color.DarkGray
                    TV_LivestatusMachine.Nodes(1).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                    TV_LivestatusMachine.Nodes(1).Text += String.Empty
                ElseIf SelectedGroupCount = Groups(grp) And SelectedGroupCount > 0 Then
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).Checked = True
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).ForeColor = Color.Black
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Bold)
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).Text += String.Empty
                    groups_ += TV_LivestatusMachine.Nodes(1).Nodes(grp).Text & ","
                Else
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).Checked = False
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).ForeColor = Color.Black
                    TV_LivestatusMachine.Nodes(1).Nodes(grp).NodeFont = New Font(TV_LivestatusMachine.Font, System.Drawing.FontStyle.Regular)
                End If
            Next

            groups_ = groups_.TrimEnd(",")

            Dim value_ As String = "", machines_readed As List(Of String) = readTreeRecurciveDevice(TV_LivestatusMachine), groups_readed As List(Of String) = readTreeRecurcivegroup(TV_LivestatusMachine)

            If Not IsNothing(machines_readed) Then
                For Each machine As String In machines_readed
                    If value_ = "" Then
                        value_ = machine
                        added_machine.Add(machine)
                    Else
                        If Not added_machine.Contains(machine) Then
                            value_ = value_ & ", " & machine
                            added_machine.Add(machine)
                        End If
                    End If
                Next
                If Not IsNothing(groups_readed) Then
                    For Each machine As String In groups_readed
                        If value_ = "" Then
                            value_ = machine
                            added_machine.Add(machine)
                        Else
                            If Not added_machine.Contains(machine) Then
                                value_ = value_ & ", " & machine
                                added_machine.Add(machine)
                            End If
                        End If
                    Next
                End If

                If configIsLoading = False Then

                    dashboardDevice.Machines = value_
                    dashboardDevice.Groups = groups_
                    dashboardDevice.SaveDevice()

                End If
            End If

            AddHandler TV_LivestatusMachine.AfterCheck, AddressOf TreeView4_AfterCheck

        End If

        RefreshDevice(deviceId)

    End Sub

    Private Function load_userConfig(mysqlcntstr As String)

        Try
            inLoadingDeviceMode = True

            deviceId = CInt(treeviewDevices.SelectedNode.Name)

            dashboardDevice = New DashboardDevice()
            dashboardDevice.LoadDevice(deviceId)

            'Dim dtDeviceConfig As DataTable = MySqlAccess.GetDataTable($"SELECT * from CSI_database.tbl_deviceConfig WHERE name = '{ Devices_TV.SelectedNode.Text }'")

            txtDeviceName.Text = dashboardDevice.DeviceName

            IP_TB.Text = dashboardDevice.IpAddress

            LSD_TB.Checked = dashboardDevice.LiveStatusDelay

            DateTime_TB.Checked = dashboardDevice.DateTime

            TB_format.Enabled = dashboardDevice.DateTime

            TB_format.Text = dashboardDevice.DateFormat

            Temp_CB.Checked = dashboardDevice.Temperature

            City_TB.Text = dashboardDevice.DetailTemperature

            Degree_CB.SelectedIndex = Degree_CB.FindStringExact(dashboardDevice.Degree)

            Url_TB.Text = dashboardDevice.DetailIFrame

            Sec_TB.Text = dashboardDevice.DetailLiveStatusDelay

            Logo_TB.Checked = dashboardDevice.CustomLogo

            txtLogoPath.Text = dashboardDevice.DetailCustomLogo

            chkLogoBarHidden.Checked = dashboardDevice.LogoBarHidden

            chkDarkTheme.Checked = dashboardDevice.DarkTheme

            chkRSSMessageOnOff.Checked = dashboardDevice.Messages

            LSFullscreen_CB.Checked = dashboardDevice.FullScreen

            If dashboardDevice.DeviceType = "Computer" Then
                CB__DeviceType.SelectedIndex = 0
            Else
                CB__DeviceType.SelectedIndex = 1
            End If

            IF_ON.Checked = dashboardDevice.IFrame
            IF_OFF.Checked = Not dashboardDevice.IFrame

            IFFullscreen_CB.Enabled = True
            IF_Rot_CB.Enabled = True

            If Not dashboardDevice.IFrame Then
                IF_Rot_CB.Checked = False
                IF_Rot_CB.Enabled = False
            Else
                IF_Rot_CB.Checked = dashboardDevice.Rotation
            End If

            txtDeviceName.Enabled = True
            IP_TB.Enabled = True
            CB__DeviceType.Enabled = True

            If dashboardDevice.DeviceName = "Local Host" Then
                txtDeviceName.Enabled = False
                CB__DeviceType.Enabled = False
            End If

            BTN_ping.Text = "Ping"
            BTN_ping.BackColor = Color.White


            Dim dtab As List(Of String) = New List(Of String)
            Dim ind As Integer = 0
            Dim cpt As Integer = 1
            Dim temps As String = ""

            Dim dtMessages As DataTable = MySqlAccess.GetDataTable($"SELECT message from CSI_database.tbl_messages WHERE deviceId = { deviceId } order by Priority")

            dgvRSSMessages.Rows.Clear()

            For Each r As DataRow In dtMessages.Rows
                temps = r.Item(0).ToString()
                If (r.Item(0).ToString().Contains("_CON")) Then
                    temps = r.Item(0).ToString().Substring(5, r.Item(0).ToString().Length - 5)
                End If
                dgvRSSMessages.Rows.Add(temps, cpt.ToString())
                cpt += 1
            Next

            Dim phrase As String = ","
            Dim Occurrences As Integer = 0

            Dim intCursor As Integer = 0

            Dim input As String = dashboardDevice.Machines

            Do Until intCursor >= input.Length

                Dim strCheckThisString As String = Mid(LCase(input), intCursor + 1, (Len(input) - intCursor))

                Dim intPlaceOfPhrase As Integer = InStr(strCheckThisString, phrase)
                If intPlaceOfPhrase > 0 Then
                    Occurrences += 1
                    intCursor += (intPlaceOfPhrase + Len(phrase) - 1)
                Else
                    intCursor = input.Length
                End If
            Loop

            Dim dt As List(Of String) = input.Split(",").Select(Function(s) s.Trim()).ToList()

            'input = input + ",."
            'For i = 0 To Occurrences
            '    Dim temp As String = input.Substring(0, input.IndexOf(","))
            '    dt.Add(temp)
            '    input = input.Substring(input.IndexOf(",") + 1, input.Count - 1 - input.IndexOf(","))
            'Next

            For Each p As TreeNode In TV_LivestatusMachine.Nodes
                p.Checked = False
                For Each n As TreeNode In p.Nodes
                    n.Checked = False
                    For Each m As TreeNode In n.Nodes
                        m.Checked = False
                    Next
                Next
            Next

            Dim dtMachines As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf;")
            Dim rowMachine As DataRow
            Dim machineId As Integer = 0

            For Each machine As String In dt

                If Integer.TryParse(machine, machineId) Then
                    rowMachine = dtMachines.Rows.Cast(Of DataRow).Where(Function(r) r.Item("Id").ToString() = machine).FirstOrDefault()
                Else
                    rowMachine = dtMachines.Rows.Cast(Of DataRow).Where(Function(r) r.Item("Machine_name").ToString() = machine Or r.Item("EnetMachineName").ToString() = machine).FirstOrDefault()
                End If

                If Not IsNothing(rowMachine) Then

                    machineId = rowMachine.Item("Id")

                    'machine = machine.Trim()

                    For Each p As TreeNode In TV_LivestatusMachine.Nodes

                        If p.Tag = machineId Then
                            p.Checked = True
                        Else

                            For Each n As TreeNode In p.Nodes

                                If n.Tag = machineId Then
                                    n.Checked = True
                                Else
                                    For Each m As TreeNode In n.Nodes
                                        If m.Tag = machineId Then
                                            m.Checked = True
                                        End If
                                    Next
                                End If

                            Next

                        End If

                    Next

                End If

            Next

            inLoadingDeviceMode = False

        Catch ex As Exception

            MessageBox.Show("Unable to load device configuration, there might be a problem with your database. See log for details")

            Log.Error("Unable to load device config.", ex)

        End Try

    End Function

    Private Sub CheckAllChildNodes(ByVal parentNode As TreeNode)

        For Each childNode As TreeNode In parentNode.Nodes

            If Not (childNode.Checked = parentNode.Checked) Then
                childNode.Checked = parentNode.Checked
                For Each node As TreeNode In TV_LivestatusMachine.Nodes
                    CheckAllNodes(node, childNode)
                Next
            End If

            CheckAllChildNodes(childNode)
        Next
    End Sub

    Private Sub CheckAllNodes(ByVal Node As TreeNode, modified_node_ As TreeNode)
        For Each childNode As TreeNode In Node.Nodes
            'childNode.Checked = Node.Checked
            If childNode.Text = modified_node_.Text Then
                childNode.Checked = modified_node_.Checked
            End If
            CheckAllNodes(childNode, modified_node_)
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

    Function readTreeRecurciveDevice(treeViewMachines As TreeView) As List(Of String)

        checked__treeviewDevice.Clear()

        Dim aNode As TreeNode

        For Each aNode In treeViewMachines.Nodes(0).Nodes
            PrintRecursiveDevice(aNode)
        Next
        Return checked__treeviewDevice

    End Function

    Function readTreeRecurcivegroup(treeViewMachines As TreeView) As List(Of String)

        checked__treeviewGroup.Clear()

        Dim aNode As TreeNode

        For Each aNode In treeViewMachines.Nodes(1).Nodes
            PrintRecursiveGroup(aNode)
        Next
        Return checked__treeviewGroup
    End Function

    Private Sub PrintRecursiveDevice(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            checked__treeviewDevice.Add(n.Tag)
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursiveDevice(aNode)
        Next
    End Sub

    Private Sub PrintRecursiveGroup(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            checked__treeviewGroup.Add(n.Tag)
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursiveGroup(aNode)
        Next
    End Sub

    Private Function convert_mac_to_ip(mac As String) As String

        Dim startInfo As New ProcessStartInfo()
        startInfo.CreateNoWindow = True
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardOutput = True
        startInfo.FileName = "arp"
        startInfo.Arguments = "-a"

        Try
            Dim process__1 As Process = Process.Start(startInfo)
            Dim out As String = ""
            Dim splitted_out As String()

            While Not process__1.StandardOutput.EndOfStream
                out = process__1.StandardOutput.ReadLine()
                out = Regex.Replace(out, "\s+", " ")
                splitted_out = out.Split(" ")
                If splitted_out.Count > 1 Then
                    If splitted_out(2) = mac.ToLower Then Return splitted_out(1)
                End If
            End While
        Catch ex As Exception
            MessageBox.Show("Could not find the ip address associated with the device MAC address: " & ex.Message)
            Return "0.0.0.0" 'error
        Finally

        End Try
        Return "0.0.0.0" 'error
    End Function

    Public Function pingHost(IpOrName As String) As Boolean
        Dim pingable As Boolean = False

        Dim pinger As Ping = New Ping()

        Try
            Dim reply As PingReply = pinger.Send(IpOrName)
            If (reply.Status = IPStatus.Success) Then

                pingable = True

            End If
        Catch ex As Exception
            pingable = False
        End Try

        Return pingable
    End Function

    Private Sub dgvRSSMessages_SelectionChanged(sender As Object, e As EventArgs)

        Try
            If dgvRSSMessages.SelectedCells.Item(0).ColumnIndex = 1 Then
                RemoveHandler dgvRSSMessages.SelectionChanged, AddressOf dgvRSSMessages_SelectionChanged
                dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex).Cells(0).Selected = True
                dgvRSSMessages.Rows(dgvRSSMessages.SelectedCells.Item(0).RowIndex).Cells(1).Selected = False
                AddHandler dgvRSSMessages.SelectionChanged, AddressOf dgvRSSMessages_SelectionChanged
            End If
        Catch ex As Exception
        End Try

    End Sub


End Class

Imports System.Xml
Imports System.IO
Imports System.Xml.XPath
Imports System.Xml.Schema
Imports OpenNETCF.MTConnect

Imports OpenNETCF.MTConnect.Client
Imports OpenNETCF.MTConnect.EntityClient
Imports OpenNETCF.Web

Imports System.Text
Imports System.Net
Imports CSI_Library
'Imports System.Data.OleDb
Imports MySql.Data.MySqlClient

Public Class Edit_mtconnect

    Public MachineName As String
    Public MachineIP As String '= SetupForm2.datagridview4.Rows(SetupForm2.datagridview4.SelectedCells(0).RowIndex).Cells("IP").Value
    Public ConnectorType As String
    'Public machine As String
    Private m_client As EntityClient
    'Public Devices As String
    'Public cycleOnId As String

    Private statusdt As New List(Of DataTable)
    'Private statustype As New List(Of String)

    'Public devicename As String = ""

    'Public enettomtc As Boolean = False '1=enet  to mtc
    'Public monitoringfilenumber As Integer
    'Public path As String = CSI_Lib.getRootPath()

    Private StatusAssociationDT As New DataTable

    Private Sub CreateConditionRow()
        Dim dt As New DataTable
        Using sqlConn As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            Try
                sqlConn.Open()
                Dim adap As MySqlDataAdapter = New MySqlDataAdapter("SELECT * FROM CSI_auth.tbl_CSIConnector  where machineName = '" & MachineName & "' and machineIP = '" & MachineIP & "' ", sqlConn)
                Dim returnvalue As Integer = adap.Fill(dt)
                If (returnvalue = 0) Then
                    'For Each status As String In statustype_temp
                    Dim defaultcond As String = "cn3 ==""AUTOMATIC"" && cn6 ==""ACTIVE"""
                    Dim cmd4 As New MySqlCommand("INSERT INTO CSI_auth.tbl_CSIConnector VALUES ('" & MachineName & "', '" & MachineIP & "', '" & ConnectorType & "', 'CYCLE ON','" & defaultcond & "','msg','cn5','Fovr','c3')", sqlConn)
                    cmd4.ExecuteNonQuery()
                    'Next
                    'sqlConn.Close()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message())
            Finally
                sqlConn.Close()
            End Try
        End Using

    End Sub

    '===================================================================================================
    ' Load and populate the treeview
    '===================================================================================================
    Public From_mtc As Boolean = False
    Private Sub Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            statusdt = New List(Of DataTable)

            LB_MachineAddress.Text = MachineName & " : " & MachineIP
            m_client = New EntityClient(MachineIP)

            Dim Devices = m_client.Probe()


            Dim dev As DeviceCollection = Devices
            If (dev.Count > 0) Then
                'if machinename exist in devicecollection
                '.Where(Function(r) r.Name = nodename).ToArray()
                Dim device = dev.Where(Function(f) f.Name = MachineName).ToList
                If (device.Count = 1) Then
                    PopulateTree(Devices, MachineName, TV_MTC)

                    LoadCondFromDB()
                ElseIf (device.Count = 0) Then
                    MessageBox.Show("Machine name not found in device collection")
                    LoadCondFromDB()
                Else
                    MessageBox.Show("More than 1 machine with the same name were found in device collection")
                    LoadCondFromDB()
                End If
            Else
                MessageBox.Show("No device found from this IP")
                LoadCondFromDB()
            End If

            StatusAssociationDT = New DataTable()
            StatusAssociationDT.Columns.Add("status")
            StatusAssociationDT.Columns.Add("mtcid")
            LoadStatusAssociation()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Sub LoadStatusAssociation()
        Try
            Dim connString As String = CSI_Library.CSI_Library.MySqlConnectionString

            Dim results As New DataTable()

            Using conn As New MySqlConnection(connString)
                Try
                    conn.Open()
                    Dim cmd As New MySqlCommand("SELECT PartnoID,ProgramID,FeedOverrideID,SpindleOverrideID FROM CSI_auth.tbl_CSIConnector where machinename='" & MachineName & "' and machineIP='" & MachineIP & "' group by CurrentStatus", conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(results)
                    'conn.Close()
                Catch ex As Exception
                    MessageBox.Show("Database Error in LoadStatusAssociation : " & ex.Message())
                Finally
                    conn.Close()
                End Try
            End Using

            'Dim partnoid As String, programid As String, feedoverrideid As String, spindleoverrideid As String
            'For Each row In results.Rows
            StatusAssociationDT = New DataTable
            StatusAssociationDT.Columns.Add("status", GetType(String))
            StatusAssociationDT.Columns.Add("mtcid", GetType(String))

            If (results.Rows.Count = 1) Then
                'Dim row As DataRow = StatusAssociationDT.NewRow
                'row("status") = "PartnoID"
                'Dim strtemp As String = results.Rows(0)("PartnoID")
                'row("mtcid") = strtemp
                'StatusAssociationDT.Rows.Add(row)

                'row = StatusAssociationDT.NewRow
                'row("status") = "ProgramID"
                'strtemp = results.Rows(0)("ProgramID")
                'row("mtcid") = strtemp
                'StatusAssociationDT.Rows.Add(row)

                'row = StatusAssociationDT.NewRow
                'row("status") = "FeedOverrideID"
                'strtemp = results.Rows(0)("FeedOverrideID")
                'row("mtcid") = "2" 'strtemp
                'StatusAssociationDT.Rows.Add(row)

                'row = StatusAssociationDT.NewRow
                'row("status") = "SpindleOverrideID"
                'strtemp = results.Rows(0)("SpindleOverrideID")
                'row("mtcid") = strtemp
                'StatusAssociationDT.Rows.Add(row)

                StatusAssociationDT.Rows.Add(New String() {"PartnoID", results.Rows(0)("PartnoID").ToString()})
                StatusAssociationDT.Rows.Add(New String() {"ProgramID", results.Rows(0)("ProgramID").ToString()})
                StatusAssociationDT.Rows.Add(New String() {"FeedOverrideID", results.Rows(0)("FeedOverrideID").ToString()})
                StatusAssociationDT.Rows.Add(New String() {"SpindleOverrideID", results.Rows(0)("SpindleOverrideID").ToString()})
            End If

            CB_StatusAssociation.DataSource = StatusAssociationDT
            CB_StatusAssociation.DisplayMember = "status"
            'CB_StatusAssociation.ValueMember = "mtcid"
            'CB_StatusAssociation.BindingContext = Me.BindingContext

        Catch ex As Exception
            CSI_Lib.LogServerError("Error loading status association for MTConnect:" + ex.Message, 1)
        End Try

    End Sub

    '===================================================================================================
    ' populate the treeview
    '===================================================================================================


    Private Function checkIfMachineExists() As Boolean
        Dim Add As Boolean = False

        Using conn As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT Machinename FROM CSI_auth.tbl_CSIConnector where MachineName='" & MachineName & "'", conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim results As New DataTable()
                adapter.Fill(results)
            Catch ex As Exception
                MessageBox.Show("Database Error in checkIfMachineExists : " & ex.Message())
            Finally
                conn.Close()
            End Try
        End Using
        Return Add
    End Function
    Private Sub PopulateTree(devices As DeviceCollection, machinename As String, ByRef treeview As Windows.Forms.TreeView)

        treeview.Nodes.Clear()

        ' create a root node
        Dim root As New TreeNode(devices.AgentInformation.Name)
        root.Tag = devices.AgentInformation

        ' fill the node with devices

        For Each device In devices
            If (machinename = device.Name) Then

                Dim deviceNode As New TreeNode(device.Name)
                deviceNode.Tag = device

                For Each component In device.Components
                    FillComponents(component, deviceNode)
                Next

                For Each dataItem In device.DataItems
                    Dim displayName As String = dataItem.ID

                    If Not (String.IsNullOrEmpty(dataItem.Name)) Then displayName += String.Format(" ({0})", dataItem.Name)

                    Dim dataNode As New TreeNode(displayName)
                    dataNode.Tag = dataItem
                    deviceNode.Nodes.Add(dataNode)
                Next

                root.Nodes.Add(deviceNode)
            End If
        Next

        root.ExpandAll()

        'put the root node into the tree
        treeview.Nodes.Add(root)

        ' cache the device collection
        treeview.Tag = devices
    End Sub

    Private Sub FillComponents(component As OpenNETCF.MTConnect.Component, ByRef parent As TreeNode)

        Dim componentNode As New TreeNode(component.Name)
        componentNode.Tag = component

        For Each subcomponent In component.Components
            FillComponents(subcomponent, componentNode)
        Next

        For Each dataItem In component.DataItems

            Dim displayName As String = dataItem.ID

            If Not (String.IsNullOrEmpty(dataItem.Name)) Then displayName += String.Format(" ({0})", dataItem.Name)

            Dim dataNode As New TreeNode(displayName)
            dataNode.Tag = dataItem
            componentNode.Nodes.Add(dataNode)
        Next

        parent.Nodes.Add(componentNode)
    End Sub


    '===================================================================================================
    ' Fill the property list view
    '===================================================================================================
    Public Sub PopulatePropertyList(source As Object)

        LV_Property.Items.Clear()
        LV_Property.GridLines = True
        LV_Property.FullRowSelect = True


        If (source.ToString() = "OpenNETCF.MTConnect.AgentInformation") Then
            Dim info As AgentInformation = source

            Dim itm As New ListViewItem("Name")
            itm.SubItems.Add(info.Name())
            Dim itm2 As New ListViewItem("Instance ID")
            itm2.SubItems.Add(info.InstanceID())
            Dim itm3 As New ListViewItem("Version")
            itm3.SubItems.Add(info.Version())
            LV_Property.Items.Add(itm)
            LV_Property.Items.Add(itm2)
            LV_Property.Items.Add(itm3)
        End If

        If (source.ToString() = "OpenNETCF.MTConnect.DataItem") Then
            Dim lvi As New ListViewItem("Current value", "")
            Dim dataItem As DataItem = source
            lvi.Tag = dataItem
            LV_Property.Items.Add(lvi)

            For Each prop In dataItem.Properties
                Dim itm As New ListViewItem(prop.Key)
                itm.SubItems.Add(prop.Value)
                LV_Property.Items.Add(itm)
            Next
        End If

        If (source.ToString() = "OpenNETCF.MTConnect.ComponentBase") Then
            Dim item As ComponentBase = source
            For Each prop In item.Properties
                Dim itm As New ListViewItem(prop.Key)
                itm.SubItems.Add(prop.Value)
                LV_Property.Items.Add(itm)

            Next
        End If

    End Sub

    '===================================================================================================
    ' Update the listview
    '===================================================================================================
    Private Sub TV_MTC_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TV_MTC.AfterSelect
        PopulatePropertyList(e.Node.Tag)
        '  Dim currents = m_client.Current()
        '  If e.Node.Tag.ToString() <> "OpenNETCF.MTConnect.AgentInformation" Then MessageBox.Show(m_client.GetDataItemById((e.Node.Tag).ID).Value.ToString())
        If e.Node.Tag.ToString() = "OpenNETCF.MTConnect.DataItem" Then
            Dim lvi As New ListViewItem("Current value", "")

            If Not IsNothing(m_client.GetDataItemById((e.Node.Tag).ID).Value) Then
                lvi.SubItems.Add(m_client.GetDataItemById((e.Node.Tag).ID).Value.ToString())
            End If

            LV_Property.Items.Item(0) = lvi
        End If
        If TV_MTC.SelectedNode.Nodes.Count = 0 Then
            BTN_AddCond.Text = "Add condition with: " & TV_MTC.SelectedNode.Text
        Else
            BTN_AddCond.Text = "Add condition"
        End If



    End Sub


    Private Sub UpdateDataList(dataList As Object)
        Dim i As Integer = 0

        Dim currentValue As String

        For i = 0 To dataList.Items.Count

            Dim lvi = dataList.Items(i)
            currentValue = m_client.GetDataItemById((lvi.Tag).ID).Value.ToString()
            lvi.SubItems(4).Text = currentValue.ToString()

        Next

    End Sub


    Private Sub BTN_AddCond_Click_1(sender As Object, e As EventArgs) Handles BTN_AddCond.Click
        'Dim currents = m_client.Current
        From_mtc = True
        If TV_MTC.SelectedNode.Nodes.Count <> 0 Then
            MsgBox("Select a valid parameter on the list")
        Else


            Dim id As String = TV_MTC.SelectedNode.Text
            If (id.IndexOf("(") >= 0) Then
                id = id.Substring(0, id.IndexOf("(")).Trim
            End If

            Dim condition As New MTCcondition()
            If (condition.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                If (CBX_TypeCond.SelectedIndex >= 0) Then
                    statusdt.Find(Function(x) x.TableName = CBX_TypeCond.SelectedItem).Rows.Add(New String() {id, condition.condition_result})
                    'DGV_Conditions.Rows.Add(New String() {id, condition.condition_result})
                End If

            End If

            SaveConditionsToDB()
        End If
        From_mtc = False
    End Sub

    Private Sub BTN_RemoveCond_Click(sender As Object, e As EventArgs) Handles BTN_RemoveCond.Click
        'check to remove row from dt
        If (DGV_Conditions.Rows.Count > 0) Then
            'if (DGV_Conditions.SelectedRows(0) >= 0) Then
            'DGV_Conditions.Rows.Remove(DGV_Conditions.SelectedRows(0))
            For Each row As DataGridViewRow In DGV_Conditions.SelectedRows
                DGV_Conditions.Rows.Remove(row)
                'statusdt.Find(Function(x) x.TableName = CBX_TypeCond.SelectedItem).Rows.Remove(row)
            Next
            SaveConditionsToDB()
            'End If
        End If
    End Sub


    Private Sub SaveConditionsToDB()
        Dim ds As New DataSet
        Dim mysqlConn As New MySqlConnection
        Dim mysqlCommand As New MySqlCommand
        mysqlConn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString
        Try
            mysqlConn.Open()
            For Each dt As DataTable In statusdt
                Dim strBuilded As String = ""
                For Each row As DataRow In dt.Rows
                    strBuilded = strBuilded + row(0).ToString() + " " + row(1).ToString() + " && "
                Next
                If (strBuilded.Length > 0) Then
                    strBuilded = strBuilded.Substring(0, strBuilded.Length - 3)
                End If
                mysqlCommand = New MySqlCommand(
                "UPDATE CSI_auth.tbl_CSIConnector SET ConditionStr = '" + strBuilded + "'  " +
                "WHERE MachineName = '" & MachineName & "' and MachineIP = '" & MachineIP & "' and CurrentStatus='" & dt.TableName & "' ", mysqlConn)
                mysqlCommand.ExecuteNonQuery()
            Next
            'mysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show("Database Error in SaveConditionsToDB " & ex.Message())
        Finally
            mysqlConn.Close()
        End Try
    End Sub


    Private Sub CBX_TypeCond_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBX_TypeCond.SelectedIndexChanged
        If (CBX_TypeCond.SelectedIndex >= 0) Then
            DGV_Conditions.DataSource = statusdt.Find(Function(x) x.TableName = CBX_TypeCond.SelectedItem)


            Dim column As New DataGridViewCheckBoxColumn()
            With column

                .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                .FlatStyle = FlatStyle.Standard
                .CellTemplate = New DataGridViewCheckBoxCell()
                .CellTemplate.Style.BackColor = Color.Beige
            End With

            DGV_Conditions.Columns.Insert(0, column)

        End If
    End Sub


    Private Sub LoadCondFromDB()

        'load from db, create data table for each status
        CreateConditionRow()


        'Dim path As String = System.Windows.Forms.Application.StartupPath
        'Dim cnt As OleDb.OleDbConnection

        Dim connString As String = CSI_Library.CSI_Library.MySqlConnectionString

        Dim results As New DataTable()

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT CurrentStatus FROM CSI_auth.tbl_CSIConnector where machinename='" & MachineName & "' and machineIP='" & MachineIP & "' group by CurrentStatus", conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                adapter.Fill(results)
                'conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
        Dim reslist As List(Of String) = results.AsEnumerable().[Select](Function(r) r.Field(Of String)("CurrentStatus")).ToList()

        CBX_TypeCond.DataSource = reslist
        Dim CurrentStatus_str As String, conditionstr As String, id As String = "", condition_result As String = ""
        Dim conditiontable As New DataTable, conditionstr_table As New DataTable
        For Each row As DataRow In results.Rows

            conditiontable = New DataTable()
            id = ""
            condition_result = ""
            CurrentStatus_str = row("CurrentStatus")

            Using conn As New MySqlConnection(connString)
                Try
                    conn.Open()
                    Dim cmd As New MySqlCommand("SELECT ConditionStr FROM CSI_auth.tbl_CSIConnector where machinename='" & MachineName & "' and  machineIP='" & MachineIP & "' and CurrentStatus='" & CurrentStatus_str & "'", conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(conditionstr_table)
                    'conn.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                Finally
                    conn.Close()
                End Try
            End Using

            If (conditionstr_table.Rows.Count > 0) Then
                conditionstr = conditionstr_table.Rows(conditionstr_table.Rows.Count - 1)("ConditionStr").ToString()


                conditiontable = ParseConditionStr(conditionstr)
                conditiontable.TableName = CurrentStatus_str
                'tempdt.Rows.Add(New String() {id, condition_result})
                statusdt.Add(conditiontable)
            End If
        Next

        DGV_Conditions.DataSource = statusdt.Find(Function(x) x.TableName = CBX_TypeCond.SelectedItem)


    End Sub

    Private Function ParseConditionStr(conditionstr As String) As DataTable
        Dim dt As New DataTable()
        Dim id As String, condition_result As String
        Dim conditions = conditionstr.Split("&&")

        dt.Columns.Add("id")
        dt.Columns.Add("condition")

        For Each s As String In conditions
            s = s.Trim
            If (s.Length > 0) Then
                id = s.Substring(0, s.IndexOf(" ")).Trim
                condition_result = s.Substring(s.IndexOf(" ")).Trim
                dt.Rows.Add(New String() {id, condition_result})
            End If
        Next

        Return dt
    End Function

    Private Sub BTN_Done_Click(sender As Object, e As EventArgs) Handles BTN_Done.Click

        'Save to db??
        UpdateNameAndIp()

        SetupForm2.Load_DGV_CSIConnector()

        Me.Close()

    End Sub

    Private Sub UpdateNameAndIp()
        Dim info As String = LB_MachineAddress.Text
        Dim colonindex As Integer = info.IndexOf(":", info.IndexOf(":") + 1)
        If (colonindex > info.IndexOf(":")) Then
            colonindex = info.IndexOf(":")
            Dim newname As String = info.Substring(0, colonindex).Trim
            Dim newip As String = info.Substring(colonindex + 1, info.Length - colonindex - 1).Trim


            Dim mysqlConn As New MySqlConnection
            Dim mysqlCommand As New MySqlCommand
            Try
                mysqlConn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString
                mysqlConn.Open()
                mysqlCommand = New MySqlCommand("UPDATE CSI_auth.tbl_CSIConnector SET MachineName = '" + newname + "', MachineIP='" + newip + "' " +
                "WHERE MachineName = '" & MachineName & "' and MachineIP = '" & MachineIP & "' ", mysqlConn)
                mysqlCommand.ExecuteNonQuery()
                'mysqlConn.Close()
            Catch ex As Exception
                MessageBox.Show("Database error in UpdateNameAndIp Function : " & ex.Message())
            Finally
                mysqlConn.Close()
            End Try
        Else
            MessageBox.Show("Colon not found, unable to save name and IP")
        End If
    End Sub


    Private Sub CB_StatusAssociation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_StatusAssociation.SelectedIndexChanged
        If (CB_StatusAssociation.SelectedIndex >= 0) Then
            TB_StatusAssoID.Text = StatusAssociationDT.Rows(CB_StatusAssociation.SelectedIndex)("mtcid").ToString()
        Else
            TB_StatusAssoID.Text = ""
        End If
    End Sub

    Private Sub BTN_Define_Click(sender As Object, e As EventArgs) Handles BTN_Define.Click
        If (CB_StatusAssociation.SelectedIndex >= 0) Then
            Dim id As String = TV_MTC.SelectedNode.Text
            If (id.IndexOf("(") >= 0) Then
                id = id.Substring(0, id.IndexOf("(")).Trim
            End If

            StatusAssociationDT.Rows(CB_StatusAssociation.SelectedIndex)("mtcid") = id

            SaveAssociationsToDB()
        End If
    End Sub

    Private Sub SaveAssociationsToDB()

        'Dim dt As New DataTable

        Dim mysqlConn As New MySqlConnection
        Dim mysqlCommand As New MySqlCommand

        mysqlConn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString
        Try
            mysqlConn.Open()
            Dim partnoid As String = StatusAssociationDT.Select("status like 'PartnoID'")(0).Item(1)
            Dim programid As String = StatusAssociationDT.Select("status like 'ProgramID'")(0).Item(1)
            Dim feedoverrideid As String = StatusAssociationDT.Select("status like 'FeedoverrideID'")(0).Item(1)
            Dim spindleoverride As String = StatusAssociationDT.Select("status like 'SpindleoverrideID'")(0).Item(1)
            mysqlCommand = New MySqlCommand("UPDATE CSI_auth.tbl_CSIConnector SET " +
                                        " PartnoID = '" + partnoid + "', " +
                                        " ProgramID = '" + programid + "', " +
                                        " FeedOverrideID = '" + feedoverrideid + "', " +
                                        " SpindleOverrideID = '" + spindleoverride + "' " +
        " WHERE MachineName = '" & MachineName & "' and MachineIP = '" & MachineIP & "';", mysqlConn)
            mysqlCommand.ExecuteNonQuery()
            'Next
        Catch ex As Exception
            MessageBox.Show(ex.Message())
        Finally
            mysqlConn.Close()
        End Try
    End Sub

    Private Sub Advencedcond_mtc_Click(sender As Object, e As EventArgs) Handles Advencedcond_mtc.Click
        Adv_MTC_cond_edit.Show()
    End Sub

    Private Sub LV_Property_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LV_Property.SelectedIndexChanged

    End Sub
End Class
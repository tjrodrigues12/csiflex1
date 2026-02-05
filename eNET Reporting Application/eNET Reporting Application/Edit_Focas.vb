Imports System.Xml
Imports System.IO
Imports System.Xml.XPath
Imports System.Xml.Schema
Imports OpenNETCF.MTConnect

Imports OpenNETCF.MTConnect.Client
Imports OpenNETCF.MTConnect.EntityClient
'Imports OpenNETCF.Web

Imports System.Text
Imports System.Net
Imports CSI_Library
'Imports System.Data.OleDb
Imports MySql.Data.MySqlClient

Public Class Edit_Focas
    Dim connString As String = CSI_Library.CSI_Library.MySqlConnectionString '"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\wamp\www\CSI_auth.mdb;"

    Public sqlConn As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
    Dim conn As New OleDb.OleDbConnection
    Public IP As String '= SetupForm2.datagridview4.Rows(SetupForm2.datagridview4.SelectedCells(0).RowIndex).Cells("IP").Value
    Public machine As String
    Public m_client As EntityClient
    Public Devices As String
    Public cycleOnId As String

    Private statusdt As New List(Of DataTable)
    Private statustype As New List(Of String)
    Private CurrentStatus As New List(Of String)

    Public devicename As String = ""

    Public sourceName As String



    Public enettomtc As Boolean = False '1=enet  to mtc
    Public monitoringfilenumber As Integer
    Public path As String = CSI_Lib.getRootPath()

    Private Sub createRow()
        Try
            Dim ds As New DataTable
            Dim adap As MySqlDataAdapter = New MySqlDataAdapter("SELECT * FROM CSI_auth.tbl_CSIConnector  where machineName = '" & devicename & "' ", sqlConn)
            Dim returnvalue As Integer = adap.Fill(ds)
            If (returnvalue = 0) Then
                If sqlConn.State = ConnectionState.Open Then
                    sqlConn.Close()
                End If
                Dim CurrentStatus_temp As New List(Of String)
                CurrentStatus_temp.Add("CYCLE ON")
                CurrentStatus_temp.Add("CYCLE OFF")
                CurrentStatus_temp.Add("SETUP")
                sqlConn.Open()
                For Each statut As String In CurrentStatus_temp
                    'sqlConn.Open()
                    Dim cmd4 As New MySqlCommand("INSERT INTO [CSI_auth.tbl_CSIConnector] VALUES ('" & devicename & "', '" & statut & "','')", sqlConn)
                    cmd4.ExecuteNonQuery()
                    'sqlConn.Close()
                Next
            End If
        Catch ex As Exception
        Finally
            sqlConn.Close()
        End Try


    End Sub


    Private Sub Edit_mtconnect_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        If (checkIfMachineExists(IP) = False) Then
            Using writer As New StreamWriter((path & "\sys\machine_list_.csys"), True)

                Dim temp_str As String = devicename + "," + IP.ToString() + ",0"
                If (enettomtc And monitoringfilenumber >= 0) Then
                    temp_str = temp_str + ",1," & monitoringfilenumber
                End If
                writer.WriteLine(temp_str)
                writer.Close()

            End Using
        End If

        SetupForm2.LoadGridviewMachines()

        cleanpublic()

    End Sub

    Private Sub cleanpublic()

        IP = "" ' As String '= SetupForm2.datagridview4.Rows(SetupForm2.datagridview4.SelectedCells(0).RowIndex).Cells("IP").Value
        machine = ""
        m_client = Nothing
        Devices = ""
        cycleOnId = ""

        statusdt = Nothing
        CurrentStatus = Nothing

        devicename = ""

        enettomtc = False
        monitoringfilenumber = -1

    End Sub

    '===================================================================================================
    ' Load and populate the treeview
    '===================================================================================================
    Private Sub Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'mtcADD.ShowDialog()

            'Me.Location = New Point(SetupForm2.Location.X + SetupForm2.Width, SetupForm2.Location.Y)
            'propertyList.Columns.Add("Name", 100)
            'propertyList.Columns.Add("Value", 100)
            TextBox1.Text = machine & " : " & IP
            m_client = New EntityClient(IP)

            Dim Devices
            Devices = m_client.Probe()
            'Dim dev As DeviceCollection = Devices
            PopulateTree(Devices, TreeView1)
            For Each item__ In Devices
                devicename = item__.Name
            Next

            ' SetupForm2.DataGridView2.Rows.Add("", devicename.ToString(), "MTConnect", "Check", "")

            LoadCondFromDB()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    '===================================================================================================
    ' populate the treeview
    '===================================================================================================


    Private Function checkIfMachineExists(IP As String) As Boolean

        Dim currentrow As String()
        Dim readed As String

        Dim AddOrNot As Boolean = False

        If File.Exists(path & "\sys\machine_list_.csys") Then
            Using reader As StreamReader = New StreamReader(path & "\sys\machine_list_.csys")
                While Not reader.EndOfStream
                    readed = reader.ReadLine()
                    If readed <> "" Then
                        currentrow = readed.Split(",")
                        If currentrow(0) <> "" Then
                            If currentrow(1) = IP.ToString() Then
                                AddOrNot = True
                            End If
                        End If
                    End If
                End While
            End Using
        End If


        Return AddOrNot
    End Function
    Private Sub PopulateTree(devices As DeviceCollection, ByRef treeview As Windows.Forms.TreeView)

        treeview.Nodes.Clear()

        ' create a root node
        Dim root As New TreeNode(devices.AgentInformation.Name)
        root.Tag = devices.AgentInformation

        ' fill the node with devices
        For Each device In devices

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

        propertyList.Items.Clear()
        propertyList.GridLines = True
        propertyList.FullRowSelect = True


        If (source.ToString() = "OpenNETCF.MTConnect.AgentInformation") Then
            Dim info As AgentInformation = source

            Dim itm As New ListViewItem("Name")
            itm.SubItems.Add(info.Name())
            Dim itm2 As New ListViewItem("Instance ID")
            itm2.SubItems.Add(info.InstanceID())
            Dim itm3 As New ListViewItem("Version")
            itm3.SubItems.Add(info.Version())
            propertyList.Items.Add(itm)
            propertyList.Items.Add(itm2)
            propertyList.Items.Add(itm3)
        End If

        If (source.ToString() = "OpenNETCF.MTConnect.DataItem") Then
            Dim lvi As New ListViewItem("Current value", "")
            Dim dataItem As DataItem = source
            lvi.Tag = dataItem
            propertyList.Items.Add(lvi)

            For Each prop In dataItem.Properties
                Dim itm As New ListViewItem(prop.Key)
                itm.SubItems.Add(prop.Value)
                propertyList.Items.Add(itm)
            Next
        End If

        If (source.ToString() = "OpenNETCF.MTConnect.ComponentBase") Then
            Dim item As ComponentBase = source
            For Each prop In item.Properties
                Dim itm As New ListViewItem(prop.Key)
                itm.SubItems.Add(prop.Value)
                propertyList.Items.Add(itm)

            Next
        End If

    End Sub

    '===================================================================================================
    ' Update the listview
    '===================================================================================================
    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        PopulatePropertyList(e.Node.Tag)
        '  Dim currents = m_client.Current()
        '  If e.Node.Tag.ToString() <> "OpenNETCF.MTConnect.AgentInformation" Then MessageBox.Show(m_client.GetDataItemById((e.Node.Tag).ID).Value.ToString())
        If e.Node.Tag.ToString() = "OpenNETCF.MTConnect.DataItem" Then
            Dim lvi As New ListViewItem("Current value", "")

            If Not IsNothing(m_client.GetDataItemById((e.Node.Tag).ID).Value) Then lvi.SubItems.Add(m_client.GetDataItemById((e.Node.Tag).ID).Value.ToString())

            propertyList.Items.Item(0) = lvi
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


    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim currents = m_client.Current
        Dim id As String = TreeView1.SelectedNode.Text
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

        SaveToDB()

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
            SaveToDB()
            'End If
        End If
    End Sub


    Private Sub SaveToDB()
        Try
            Dim ds As New DataSet

            Dim command As OleDb.OleDbCommand

            conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\wamp\www\CSI_auth.mdb;"

            conn.Open()

            For Each dt As DataTable In statusdt

                Dim strBuilded As String = ""

                For Each row As DataRow In dt.Rows

                    strBuilded = strBuilded + row(0).ToString() + " " + row(1).ToString() + " && "

                Next
                If (strBuilded.Length > 0) Then
                    strBuilded = strBuilded.Substring(0, strBuilded.Length - 3)
                End If

                command = New OleDb.OleDbCommand(
                "UPDATE CSI_auth.tbl_CSIConnector SET ConditionStr = '" + strBuilded + "'  " +
                "WHERE MachineName = '" & devicename & "' and CurrentStatus='" & dt.TableName & "' ", conn)

                command.ExecuteNonQuery()

            Next
        Catch ex As Exception
        Finally
            conn.Close()
        End Try


    End Sub


    Private Sub CBX_TypeCond_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBX_TypeCond.SelectedIndexChanged
        If (CBX_TypeCond.SelectedIndex >= 0) Then
            DGV_Conditions.DataSource = statusdt.Find(Function(x) x.TableName = CBX_TypeCond.SelectedItem)
        End If
    End Sub


    Private Sub LoadCondFromDB()

        'load from db, create data table for each status
        createRow()


        'Dim path As String = System.Windows.Forms.Application.StartupPath
        'Dim cnt As OleDb.OleDbConnection


        Dim results As New DataTable()

        Using conn As New MySqlConnection(connString)
            Try
                Dim cmd As New MySqlCommand("SELECT CurrentStatus FROM CSI_auth.tbl_CSIConnector where machinename='" & devicename & "' group by CurrentStatus", conn)

                conn.Open()

                Dim adapter As New MySqlDataAdapter(cmd)

                adapter.Fill(results)

            Catch ex As Exception
                MessageBox.Show("Database Connection Error : " & ex.Message())
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
                    Dim cmd As New MySqlCommand("SELECT ConditionStr FROM CSI_auth.tbl_CSIConnector where machinename='" & devicename & "' and CurrentStatus='" & CurrentStatus_str & "'", conn)

                    conn.Open()

                    Dim adapter As New MySqlDataAdapter(cmd)

                    adapter.Fill(conditionstr_table)
                Catch ex As Exception
                    MessageBox.Show("Database Connection Error : " & ex.Message())
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

        SetupForm2.listOfSourceToAdd.Find(Function(x) x.name = Me.sourceName).listOfMachine.Add(devicename)

        Me.Close()



    End Sub
End Class
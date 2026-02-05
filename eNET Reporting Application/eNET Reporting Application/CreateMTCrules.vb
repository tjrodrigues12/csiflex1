Imports System.Xml
Imports System.IO
Imports System.Xml.XPath
Imports System.Xml.Schema
Imports OpenNETCF.MTConnect

Imports OpenNETCF.MTConnect.Client
Imports OpenNETCF.MTConnect.EntityClient
Imports OpenNETCF.Web
Imports System.Text
Imports CSI_Library



Public Class CreateMTCrules

    Public IP As String '= SetupForm.DataGridView1.Rows(SetupForm.DataGridView1.SelectedCells(0).RowIndex).Cells("IP").Value
    Public machine As String '= SetupForm.DataGridView1.Rows(SetupForm.DataGridView1.SelectedCells(0).RowIndex).Cells("machines").Value
    Public m_client As EntityClient '(IP)

    Private statustype As New List(Of String)
    Private statusdt As New List(Of DataTable)

    Public Devices As String
    Public cycleOnId As String
    Public devicename As String = ""

    '===================================================================================================
    ' Load and populate the treeview
    '===================================================================================================
    Private Sub Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            

            'propertyList.Columns.Add("Name", 100)
            'propertyList.Columns.Add("Value", 100)
            TextBox1.Text = machine & " : " & IP

            Dim ip_tmp As String
            If (IsNothing(m_client)) Then
                ip_tmp = IP
            Else
                ip_tmp = m_client.AgentAddress
            End If


            If SetupForm.mtconnect_ping(ip_tmp) = True Then
                'Dim devices = m_client.Probe()
                'PopulateTree(devices, TreeView1)

                m_client = New EntityClient(IP)

                Dim Devices = m_client.Probe()
                Dim dev As DeviceCollection = Devices
                PopulateTree(Devices, TreeView1)
                For Each item__ In dev
                    devicename = item__.Name
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    '===================================================================================================
    ' populate the treeview
    '===================================================================================================
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


        If (source.ToString = "OpenNETCF.MTConnect.AgentInformation") Then
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

        If (source.ToString = "OpenNETCF.MTConnect.DataItem") Then
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

        If (source.ToString = "OpenNETCF.MTConnect.ComponentBase") Then
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
        '  If e.Node.Tag.ToString <> "OpenNETCF.MTConnect.AgentInformation" Then MessageBox.Show(m_client.GetDataItemById((e.Node.Tag).ID).Value.ToString)
        If e.Node.Tag.ToString = "OpenNETCF.MTConnect.DataItem" Then
            Dim lvi As New ListViewItem("Current value", "")
            If Not IsNothing(m_client.GetDataItemById((e.Node.Tag).ID).Value) Then
                lvi.SubItems.Add(m_client.GetDataItemById((e.Node.Tag).ID).Value.ToString)
            End If
            propertyList.Items.Item(0) = lvi
        End If



    End Sub



    Private Sub UpdateDataList(dataList As Object)

        Dim i As Integer = 0


        Dim currentValue As String

        For i = 0 To dataList.Items.Count

            Dim lvi = dataList.Items(i)
            currentValue = m_client.GetDataItemById((lvi.Tag).ID).Value.ToString
            lvi.SubItems(4).Text = currentValue.ToString()

        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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

    'Public Class StatusCond
    '    Public type As String
    '    Public cond_dt As DataTable

    '    Public Sub New(t As String, dt As DataTable)
    '        type = t
    '        cond_dt = dt
    '    End Sub


    'End Class

    Public Sub New(lip As String)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IP = lip

        'm_client = client

        'CBX_TypeCond.Items.Add("CycleOn")
        'CBX_TypeCond.Items.Add("CycleOff")

        LoadCondFromDB()

    End Sub
    Public Sub New(client As EntityClient)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        'IP = lip

        m_client = client

        'CBX_TypeCond.Items.Add("CycleOn")
        'CBX_TypeCond.Items.Add("CycleOff")

        LoadCondFromDB()

    End Sub
    Public Sub New(machinename As String, clients As List(Of cMTConnect_Table))

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        'IP = lip

        Dim b = clients.Find(Function(x) x.machine_Name = machinename.Replace(" ", "").Replace("-", "").Replace(".", "_"))

        m_client = b.m_client

        'CBX_TypeCond.Items.Add("CycleOn")
        'CBX_TypeCond.Items.Add("CycleOff")

        LoadCondFromDB()

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
        'Save all dataset to db
        '(update)
    End Sub

    Private Sub LoadCondFromDB()
        '''''''''TEMP TEST

        statustype.Add("CycleOn")
        statustype.Add("CycleOff")

        Dim tempdt As New DataTable("CycleOn")
        tempdt.Columns.Add("id")
        tempdt.Columns.Add("condition")
        statusdt.Add(tempdt)

        tempdt = New DataTable("CycleOff")
        tempdt.Columns.Add("id")
        tempdt.Columns.Add("condition")
        statusdt.Add(tempdt)

        CBX_TypeCond.DataSource = statustype
        DGV_Conditions.DataSource = statusdt.Find(Function(x) x.TableName = CBX_TypeCond.SelectedItem)
        'on CBX index change, load corresponding DT
        ''''''''''''

        'load from db, create data table for each status
    End Sub

    Private Sub CBX_TypeCond_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBX_TypeCond.SelectedIndexChanged
        If (CBX_TypeCond.SelectedIndex >= 0) Then
            DGV_Conditions.DataSource = statusdt.Find(Function(x) x.TableName = CBX_TypeCond.SelectedItem)
        End If
    End Sub




    Private Function checkIfMachineExists(IP As String) As Boolean

        Dim currentrow As String()
        Dim readed As String

        Dim AddOrNot As Boolean = True

        If File.Exists(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys") Then
            Using reader As StreamReader = New StreamReader(System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys")
                While Not reader.EndOfStream
                    readed = reader.ReadLine()
                    If readed <> "" Then
                        currentrow = readed.Split(",")
                        If currentrow(0) <> "" Then
                            If currentrow(1) = IP.ToString Then
                                AddOrNot = False
                            End If
                        End If
                    End If
                End While
            End Using
        End If


        Return AddOrNot
    End Function

    Private Sub CreateMTCrules_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If (checkIfMachineExists(IP.ToString) = True) Then
            Using writer As New StreamWriter((System.Windows.Forms.Application.StartupPath & "\sys\machine_list_.sys"), True)

                Dim temp_str As String = devicename + "," + IP.ToString + ",0," + cycleOnId
                writer.WriteLine(temp_str)
                writer.Close()

            End Using
        End If

        SetupForm.Load_datagridview()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub DGV_Conditions_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Conditions.CellContentClick

    End Sub
End Class
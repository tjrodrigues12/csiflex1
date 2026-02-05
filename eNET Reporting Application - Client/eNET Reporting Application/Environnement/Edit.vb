Imports System.Xml
Imports System.IO
Imports System.Xml.XPath
Imports System.Xml.Schema
Imports OpenNETCF.MTConnect

Imports OpenNETCF.MTConnect.Client
Imports OpenNETCF.MTConnect.EntityClient
Imports OpenNETCF.Web
Imports System.Text



Public Class Edit

    Public IP As String '= SetupForm.DGV_Source.Rows(SetupForm.DGV_Source.SelectedCells(0).RowIndex).Cells("IP").Value
    Public machine As String '= SetupForm.DGV_Source.Rows(SetupForm.DGV_Source.SelectedCells(0).RowIndex).Cells("machines").Value
    Public m_client As New EntityClient(IP)

    '===================================================================================================
    ' Load and populate the treeview
    '===================================================================================================
    Private Sub Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Me.Location = New Point(SetupForm.Location.X + SetupForm.Width, SetupForm.Location.Y)
            propertyList.Columns.Add("Name", 100)
            propertyList.Columns.Add("Value", 100)
            TextBox1.Text = machine & " : " & IP

            If SetupForm.mtconnect_ping(IP) = True Then
                Dim devices
                devices = m_client.Probe()
                PopulateTree(devices, TreeView1)
            End If

        Catch ex As Exception
            MessageBox.Show("There was an error with the mtconnect client")
            Console.WriteLine("Error in Edit:" + ex.Message)
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
            lvi.SubItems.Add(m_client.GetDataItemById((e.Node.Tag).ID).Value.ToString())
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim currents = m_client.Current
        Dim d As String = ""
        For Each cur In currents.AllEvents
            d = d & " " & cur.DataItemID & ":" & cur.Value & vbCrLf

        Next
        MessageBox.Show(d)

    End Sub


End Class
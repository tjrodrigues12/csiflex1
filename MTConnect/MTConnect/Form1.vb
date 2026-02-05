Imports System.Xml
Imports System.IO
Imports System.Xml.XPath
Imports System.Xml.Schema
Imports OpenNETCF.MTConnect

Imports OpenNETCF.MTConnect.Client
Imports OpenNETCF.MTConnect.EntityClient
Imports OpenNETCF.Web
Imports System.Text







Public Class Form1


    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim m_client As New EntityClient(TextBox1.Text)

            Button1.Enabled = False

            Dim devices = m_client.Probe()
            PopulateTree(devices, TreeView1)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        TextBox1.Text = "http://agent.mtconnect.org/"

        '   InitializeAgentTree();

        ' InitializeDataList();

        ' InitializePlot();

        '  InitializeDataItemWatcher();
    End Sub


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

    Private Sub UpdateDataList()

        '    Dim i As Integer = 0
        '    Dim fileLine = New StringBuilder()
        '    Dim currentValue As String
        '    For i = 0 To dataList.Items.Count

        '        Dim lvi = dataList.Items(i)
        '        currentValue  = m_client.GetDataItemById(((DataItem)lvi.Tag).ID).Value.tostring
        '        lvi.SubItems(4).Text = currentValue.ToString()

        '        '  fileLine.AppendFormat("{0}{1}", currentValue, i < (dataList.Items.Count - 1) ? "," : Environment.NewLine);
        '    Next

        '    ' WriteLineToFile(fileLine.ToString())
    End Sub

    Public Sub PopulatePropertyList(source As Object)

        propertyList.Items.Clear()

        Dim item As ComponentBase = source
        Dim dataItem As DataItem = source
        Dim info As AgentInformation = source

        If (item IsNot DBNull.Value) Then

            For Each prop In item.Properties
                propertyList.Items.Add(New ListViewItem(prop.Key, prop.Value))
            Next
        ElseIf (dataItem IsNot DBNull.Value) Then

            Dim lvi As New ListViewItem("value", "")

            lvi.Tag = dataItem
            propertyList.Items.Add(lvi)

            For Each prop In dataItem.Properties
                propertyList.Items.Add(New ListViewItem(prop.Key, prop.Value))
            Next
        ElseIf (info IsNot DBNull.Value) Then

            propertyList.Items.Add(New ListViewItem("Name", info.Name()))
            propertyList.Items.Add(New ListViewItem("Instance ID", info.InstanceID()))
            propertyList.Items.Add(New ListViewItem("Version", info.Version()))
        End If
    End Sub


    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        PopulatePropertyList(e.Node.Tag)

    End Sub

End Class





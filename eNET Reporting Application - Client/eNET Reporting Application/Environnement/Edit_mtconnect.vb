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



Public Class Edit_mtconnect


    Public IP As String '= SetupForm.datagridview4.Rows(SetupForm.datagridview4.SelectedCells(0).RowIndex).Cells("IP").Value

    Public machine As String
    Public m_client As EntityClient

    '===================================================================================================
    ' Load and populate the treeview
    '===================================================================================================
    Private Sub Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' mtcADD.ShowDialog()
            Me.Location = New Drawing.Point(SetupForm.Location.X + (SetupForm.Width - Me.Width) / 2, (SetupForm.Height - Me.Height) / 2 + SetupForm.Location.Y)

            '  Me.Location = New Point(SetupForm.Location.X + SetupForm.Width, SetupForm.Location.Y)
            propertyList.Columns.Add("Name", 100)
            propertyList.Columns.Add("Value", 100)

            m_client = New EntityClient(IP)

            Dim devices
            devices = m_client.Probe()
            Dim dev As DeviceCollection = devices
            PopulateTree(devices, TreeView1)
            For Each item__ In dev
                devicename = item__.Name
            Next

            'SetupForm.DGV_Source.Rows(SetupForm.DGV_Source.Rows.Count - 1).Cells(1).Value = devicename

            ' SetupForm.DataGridView2.Rows.Add("", devicename.ToString(), "MTConnect", "Check", "")
            TextBox1.Text = devicename & " : " & IP
        Catch ex As Exception
            MessageBox.Show("There was an error with the mtconnect client")
            Console.WriteLine("mtconnect error:" + ex.Message)
        End Try
    End Sub

    Public devicename As String = ""

    '===================================================================================================
    ' populate the treeview
    '===================================================================================================
    Private Sub PopulateTree(devices As DeviceCollection, ByRef treeview As Windows.Forms.TreeView)
        Try


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
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            Console.WriteLine("error populating tree:" + ex.Message)
        End Try
    End Sub

    Private Sub FillComponents(component As OpenNETCF.MTConnect.Component, ByRef parent As TreeNode)
        Try
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
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            Console.WriteLine("error filling components:" + ex.Message)
        End Try

    End Sub


    '===================================================================================================
    ' Fill the property list view
    '===================================================================================================
    Public Sub PopulatePropertyList(source As Object)
        Try
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
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    '===================================================================================================
    ' Update the listview
    '===================================================================================================
    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect

        Try
            PopulatePropertyList(e.Node.Tag)
            '  Dim currents = m_client.Current()
            '  If e.Node.Tag.ToString() <> "OpenNETCF.MTConnect.AgentInformation" Then MessageBox.Show(m_client.GetDataItemById((e.Node.Tag).ID).Value.ToString())
            If e.Node.Tag.ToString() = "OpenNETCF.MTConnect.DataItem" Then
                Dim lvi As New ListViewItem("Current value", "")

                If Not IsNothing(m_client.GetDataItemById((e.Node.Tag).ID).Value) Then lvi.SubItems.Add(m_client.GetDataItemById((e.Node.Tag).ID).Value.ToString())

                propertyList.Items.Item(0) = lvi
            End If


        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub



    Private Sub UpdateDataList(dataList As Object)

        Dim i As Integer = 0


        Dim currentValue As String
        Try
            For i = 0 To dataList.Items.Count

                Dim lvi = dataList.Items(i)
                currentValue = m_client.GetDataItemById((lvi.Tag).ID).Value.ToString()
                lvi.SubItems(4).Text = currentValue.ToString()

            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim currents = m_client.Current
        Dim d As String = ""
        ListView1.Items.Add(TreeView1.SelectedNode.Text)

    End Sub


End Class
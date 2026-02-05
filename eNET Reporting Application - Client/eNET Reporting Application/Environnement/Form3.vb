
Imports System
Imports System.Windows
Imports System.Reflection
Imports System.IO
Imports System.Net
Imports System.IO.File
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Xml
Imports System.Management

Imports System.Environment

Imports System.ApplicationIdentity
Imports System.Threading
Imports CSI_Library
Imports CSI_Library.CSI_DATA
Imports System.Globalization
Imports System.Runtime.Serialization.Formatters.Binary
Imports TreeViewSerialization




Public Class Form3

#Region "VAR"
    Public globalVariable As New GlobalVariables
    Public filtered_table As System.Data.DataTable
    Public readed As Integer = 0, toread As Integer = 0
    Public periode_returned As periode()
    'Public big_periode_returned As periode(,)
    Public CSI_LIB As New CSI_Library.CSI_Library
    Public days As Integer
    Public machine_form9 As Integer
    Public dbConnectStr As String
    Dim cnt As System.Data.OleDb.OleDbConnection
    Dim Catalog As Object
    Public savepath_xlsx As String
    Public savepath_pdf As String
    Public general_array(26, 3, 1) As String
    Public from_form3 As Boolean = False
    Public list As New List(Of String)
    Public list2 As New List(Of String)
    Public tree_j As Integer
    Public tree_k As Integer
    Public Super_array As New List(Of String(,))
    Public for_form9 As Boolean = False
    Public Date_ As String
    Public stat(3) As String
    Public form9_loaded As Boolean = False
    Public toExport As String()
    Dim mt_present As Boolean = False
    Dim st_present As Boolean = False


    Dim CheckedNodes As New List(Of TreeNode)
    Public big_periode_returned As periode(,)

#End Region

    Private Sub Form3_0(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.ResumeLayout()

    End Sub

    Private Sub form3_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        saveLastTreeV()
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------
    ' Form3 Load
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SuspendLayout()

        Select Case Welcome.CSIF_version
            Case 1
                Button2.Enabled = False

        End Select

        Me.MdiParent = Form1
        Me.Left = 0
        Me.Top = 0
        Me.Dock = DockStyle.Left
        Me.Height = Form1.Height - 90

        SplitContainer1.Panel2Collapsed = True

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.BackColor = Color.Transparent
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd MMMM yyyy-ddd"
        DateTimePicker2.CustomFormat = "dd MMMM yyyy-ddd"


        Dim i As Integer
        Dim j As Integer
        Dim k As Integer

        Dim prefixTemp As String ' temp string, multiple use

        Dim machine() As String ' File -> read line -> machine()

        TreeView1.Nodes.Clear()

        If GlobalVariables.reloadByForm1 = False And Exists(System.Windows.Forms.Application.StartupPath + "\sys\StartupConfig_TreeView.xml") And Welcome.CSIF_version <> 3 Then

            loadStartupConfigTreeV()
        ElseIf GlobalVariables.reloadByForm1 = False And Exists(System.Windows.Forms.Application.StartupPath + "\sys\" & Form1.username_ & "_TreeView.xml") And Welcome.CSIF_version <> 3 Then

            loadLastTreeV()
        Else

            GlobalVariables.reloadByForm1 = False
            '*************************************************************************************************************************************************'
            '**** Load machines names from _SETUP\MonList.sys
            '*************************************************************************************************************************************************'
            Dim file As System.IO.StreamReader
            Dim imglst As New ImageList


            If Not My.Computer.FileSystem.FileExists(CSI_LIB.eNET_path + "\_SETUP\MonList.sys") Then
                MessageBox.Show("The file 'Monlist.sys' is missing")
                GoTo fin
            End If

            file = My.Computer.FileSystem.OpenTextFileReader(CSI_LIB.eNET_path + "\_SETUP\MonList.sys")
            i = 0
            j = -1
            k = -1
            Dim cpt As Integer = -1
            Dim cpt2 As Integer = 0
            Dim machine_tmp As String

            While Not file.EndOfStream

                ReDim Preserve machine(i + 1)
                machine(i) = file.ReadLine()

                prefixTemp = Strings.Left(machine(i), 3)

                Dim node As New TreeNode

                If Strings.Len(machine(i)) > 4 Then
                    node.ImageKey = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
                    node.Text = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
                Else
                    node.ImageKey = machine(i)
                    node.Text = machine(i)
                End If

                Select Case prefixTemp
                    Case "_MT"
                        mt_present = True
                        st_present = False

                        list2.Add(machine(i))
                        TreeView1.Nodes.Add(node)
                        cpt = -1
                        j = j + 1
                    Case "_ST"
                        st_present = True
                        cpt += 1
                        If mt_present = False Then
                            TreeView1.Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
                            'TreeView1.Nodes.Add(node)
                        Else
                            TreeView1.Nodes(j).Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
                            'TreeView1.Nodes(j).Nodes.Add(node)
                        End If

                        k += 1

                    Case Else
                        If (prefixTemp <> "_ST") And (prefixTemp <> "_MT") Then
                            If st_present = False And mt_present = True Then
                                machine_tmp = machine(i)
                                machine_tmp = machine_tmp.Replace(" ", "").Replace("-", "").Replace(".", "_")
                                TreeView1.Nodes(j).Nodes.Add(machine_tmp, machine_tmp)
                                TreeView1.ImageList.Images.Add(machine_tmp.ToString, My.Resources.thumb_SMART_430A)
                                cpt += 1

                            ElseIf mt_present = False And st_present = True Then
                                machine_tmp = machine(i)
                                machine_tmp = machine_tmp.Replace(" ", "").Replace("-", "").Replace(".", "_")
                                TreeView1.Nodes(k).Nodes.Add(machine_tmp, machine_tmp)
                                TreeView1.ImageList.Images.Add(machine_tmp.ToString, My.Resources.thumb_SMART_430A)
                                cpt2 += 1

                            ElseIf mt_present = False And st_present = False Then
                                machine_tmp = machine(i)
                                machine_tmp = machine_tmp.Replace(" ", "").Replace("-", "").Replace(".", "_")
                                TreeView1.Nodes.Add(machine_tmp, machine_tmp)
                                TreeView1.ImageList.Images.Add(machine_tmp.ToString, My.Resources.thumb_SMART_430A)

                            Else
                                machine_tmp = machine(i)
                                machine_tmp = machine_tmp.Replace(" ", "").Replace("-", "").Replace(".", "_")
                                TreeView1.Nodes(j).Nodes(cpt).Nodes.Add(machine_tmp, machine_tmp)
                                TreeView1.ImageList.Images.Add(machine_tmp.ToString, My.Resources.thumb_SMART_430A)

                            End If

                        End If
                End Select

                i = i + 1
            End While


            'While Not file.EndOfStream
            '    ReDim Preserve machine(i + 1)

            '    machine(i) = file.ReadLine()

            '    strtmp = Strings.Left(machine(i), 3)
            '    If (strtmp = "_MT") Then
            '        list2.Add(machine(i))
            '        j = j + 1
            '        mt_present = True
            '        st_present = False

            '        Dim node As New TreeNode

            '        node.ImageKey = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
            '        node.Text = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
            '        TreeView1.Nodes.Add(node)
            '        TreeView1.ImageList.Images.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), My.Resources.MC900442150_1_)

            '        'TreeView1.Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))

            '    End If


            '    If (strtmp = "_ST") Then
            '        st_present = True
            '        list2.Add(machine(i))
            '        Dim node As New TreeNode
            '        node.ImageKey = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
            '        node.Text = Strings.Right(machine(i), Strings.Len(machine(i)) - 4).ToString

            '        If mt_present = False Then
            '            TreeView1.Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
            '            TreeView1.Nodes.Add(node)
            '        Else
            '            TreeView1.Nodes(j).Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
            '            TreeView1.Nodes(j).Nodes.Add(node)
            '        End If




            '        TreeView1.ImageList.Images.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4).ToString, My.Resources.home_web_icon_300x300)
            '        k = k + 1
            '    End If

            '    If (strtmp <> "_ST") And (strtmp <> "_MT") Then

            '        If st_present = False And mt_present = True Then
            '            TreeView1.Nodes(j).Nodes.Add(machine(i), machine(i))
            '            TreeView1.ImageList.Images.Add(machine(i).ToString, My.Resources.thumb_SMART_430A)
            '        Else
            '            If mt_present = False And st_present = True Then
            '                TreeView1.Nodes(k).Nodes.Add(machine(i), machine(i))
            '                TreeView1.ImageList.Images.Add(machine(i).ToString, My.Resources.thumb_SMART_430A)
            '            Else
            '                If mt_present = False And st_present = False Then
            '                    TreeView1.Nodes.Add(machine(i), machine(i))
            '                    TreeView1.ImageList.Images.Add(machine(i).ToString, My.Resources.thumb_SMART_430A)
            '                Else
            '                    TreeView1.Nodes(j).Nodes(k).Nodes.Add(machine(i), machine(i))
            '                    TreeView1.ImageList.Images.Add(machine(i).ToString, My.Resources.thumb_SMART_430A)
            '                End If
            '            End If
            '        End If
            '    End If
            '    i = i + 1
            'End While

            file.Close()
        End If
        '*************************************************************************************************************************************************'
        '**** Load machines names from _SETUP\MonList.sys - END
        '*************************************************************************************************************************************************'
Fin:

        'For Each node As TreeNode In TreeView1.Nodes

        '    If node.Nodes.Count > 0 Then node.NodeFont = New Font("Segoe UI", 10, System.Drawing.FontStyle.Underline)

        'Next

        font_tree()
        ' Me.Refresh()

        'follow startup config
        loadStartupParams()

    End Sub


    Private Sub font_tree()
        ' checked_in_treeview1.Clear()

        Dim aNode As TreeNode

        For Each aNode In TreeView1.Nodes
            fontRecursive(aNode)
        Next
        ' Return checked_in_treeview1
    End Sub
    Private Sub fontRecursive(ByVal n As TreeNode)

        Dim aNode As TreeNode
        n.SelectedImageIndex = n.ImageIndex
        If n.Nodes.Count > 0 Then n.NodeFont = New Font("Segoe UI", 10, System.Drawing.FontStyle.Underline)
        If Welcome.CSIF_version = 1 Then
            If n.Nodes.Count > 0 Then
                n.ImageIndex = 2
            End If
            n.ToolTipText = "No monitoring data available"
        Else
            If n.Nodes.Count > 0 Then
                n.ImageIndex = 2
                'If GlobalVariables.mut.WaitOne(1000) Then
            Else
                Try
                    Select Case GlobalVariables.ListOfMachine.Item(n.Text).Statut
                        Case "NO eMONITOR"
                            n.ImageIndex = 0
                        Case "CYCLE ON"
                            n.ImageIndex = 1
                        Case "CYCLE OFF"
                            n.ImageIndex = 3
                        Case "ACTIVE"
                            n.ImageIndex = 1
                        Case "SETUP"
                            n.ImageIndex = 5
                        Case Else
                            n.ImageIndex = 4
                    End Select
                Catch ex As Exception

                End Try
            End If
            'GlobalVariables.mut.ReleaseMutex()
            'End If




        End If
        TreeView1.ShowNodeToolTips = True

        For Each aNode In n.Nodes
            fontRecursive(aNode)
        Next

    End Sub


    Public Sub Fill_combo_Form7(general_array As String(,,))

        Dim i As Integer
        If Form7.Visible = True Then
            Form7.Close()
        End If

        For i = 0 To (UBound(general_array, 3)) - 1
            Form7.ComboBox1.Items.Add(general_array(0, 0, i) & " : " & general_array(7, 0, i))
        Next

        Form7.ComboBox1.SelectedIndex = i - 1

        Form7.Show()
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' secondes to DHMS
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Function uptimeToDHMS(ByVal inSeconds As Double) As String
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
                uptimeToDHMS = "0" & hours & ":" & "0" & minutes
            Else
                uptimeToDHMS = hours & ":" & "0" & minutes
            End If
        Else
            If hours < 10 Then
                uptimeToDHMS = "0" & hours & ":" & minutes
            Else
                uptimeToDHMS = hours & ":" & minutes
            End If
        End If

    End Function


    '-----------------------------------------------------------------------------------------------------------------------
    ' MACHINE CHOICE FROM THE TREE VIEW
    '  
    '-----------------------------------------------------------------------------------------------------------------------
#Region "TREEVIEW"
    Private Sub TreeView1_AfterCheck(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeView1.AfterCheck
        RemoveHandler TreeView1.AfterCheck, AddressOf TreeView1_AfterCheck

        Call CheckAllChildNodes(e.Node)

        Dim machines As String() = read_tree()
        If UBound(machines) > -1 Then

            Button6.Enabled = True
            If Welcome.CSIF_version <> 1 Then Button2.Enabled = True
            If Welcome.CSIF_version <> 1 Then BTN_Dashboard.Enabled = True
            '  TextBox1.Enabled = True
            '   DataGridView1.Enabled = True
        Else
            BTN_Dashboard.Enabled = False
            Button6.Enabled = False
            Button2.Enabled = False
            TextBox1.Clear()

        End If

        If e.Node.Checked Then
            If e.Node.Parent Is Nothing = False Then
                Dim allChecked As Boolean = True
                Call IsEveryChildChecked(e.Node.Parent, allChecked)
                If allChecked Then
                    e.Node.Parent.Checked = True
                    Call ShouldParentsBeChecked(e.Node.Parent)
                End If
            End If
        Else
            Dim parentNode As TreeNode = e.Node.Parent
            While parentNode Is Nothing = False
                parentNode.Checked = False
                parentNode = parentNode.Parent
            End While
        End If

        AddHandler TreeView1.AfterCheck, AddressOf TreeView1_AfterCheck
    End Sub

    Private Sub CheckAllChildNodes(ByVal parentNode As TreeNode)
        For Each childNode As TreeNode In parentNode.Nodes
            childNode.Checked = parentNode.Checked
            CheckAllChildNodes(childNode)
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

    ''------------------------------------------------------------------------------------------------------------------------

    '-----------------------------------------------------------------------------------------------------------------------
    ' Read the treeview and gives the checked machines in an array of strings
    '-----------------------------------------------------------------------------------------------------------------------
    Function read_tree2() As String()


        Dim active_machines(1) As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0

        For Each treeviewnode As TreeNode In Me.TreeView1.Nodes
            If treeviewnode.Checked = True Then
                active_machines(i) = treeviewnode.Name
                i = i + 1
                ReDim Preserve active_machines(i)
            End If
            For Each treeviewnode2 As TreeNode In treeviewnode.Nodes
                For Each treeviewnode3 As TreeNode In treeviewnode2.Nodes
                    If treeviewnode3.Checked = True Then
                        active_machines(i) = treeviewnode3.Name
                        i = i + 1
                        ReDim Preserve active_machines(i)
                    End If
                    k = k + 1
                Next
                If treeviewnode2.Checked = True Then
                    active_machines(i) = treeviewnode2.Name
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
            For Each item2 In list2
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


        Return active_machines
        '*************************************************************************************************************************************************'
        '**** Active machines from the treenode -END
        '*************************************************************************************************************************************************'

    End Function


    ' Dim checked_in_treeview1 As New List(Of String)
    Dim active_machines(1) As String

    Public Function read_tree() As String()
        ' checked_in_treeview1.Clear()
        ReDim active_machines(0)
        Dim aNode As TreeNode

        For Each aNode In TreeView1.Nodes
            PrintRecursive(aNode)
        Next
        ' Return checked_in_treeview1
        ReDim Preserve active_machines(UBound(active_machines) - 1)
        Return active_machines

    End Function
    Private Sub PrintRecursive(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            '   checked_in_treeview1.Add(n.Text)
            If n.Name <> "" Then
                active_machines(UBound(active_machines)) = n.Name
                ReDim Preserve active_machines(UBound(active_machines) + 1)
            End If
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursive(aNode)
        Next
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' Drag and drop
    '-----------------------------------------------------------------------------------------------------------------------
    Public Sub TreeView1_ItemDrag(ByVal sender As System.Object, _
        ByVal e As System.Windows.Forms.ItemDragEventArgs) _
        Handles TreeView1.ItemDrag

        ''Set the drag node and initiate the DragDrop 
        'DoDragDrop(e.Item, DragDropEffects.Move)

    End Sub
    Public Sub TreeView1_DragEnter(ByVal sender As System.Object, _
        ByVal e As System.Windows.Forms.DragEventArgs) _
        Handles TreeView1.DragEnter

        ''See if there is a TreeNode being dragged
        'If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", _
        '    True) Then
        '    'TreeNode found allow move effect
        '    e.Effect = DragDropEffects.Move
        'Else
        '    'No TreeNode found, prevent move
        '    e.Effect = DragDropEffects.None
        'End If

    End Sub
    Public Sub TreeView1_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TreeView1.DragOver

        'Check that there is a TreeNode being dragged 
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedTreeview As TreeView = CType(sender, TreeView)

        'As the mouse moves over nodes, provide feedback to 
        'the user by highlighting the node that is the 
        'current drop target
        Dim pt As System.Drawing.Point = CType(sender, TreeView).PointToClient(New System.Drawing.Point(e.X, e.Y))
        Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)

        'See if the targetNode is currently selected, 
        'if so no need to validate again
        If Not (selectedTreeview.SelectedNode Is targetNode) Then

            'Select the    node currently under the cursor
            selectedTreeview.SelectedNode = targetNode

            'Check that the selected node is not the dropNode and
            'also that it is not a child of the dropNode and 
            'therefore an invalid target
            Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

            Do Until targetNode Is Nothing
                If targetNode Is dropNode Then
                    e.Effect = DragDropEffects.None
                    Exit Sub
                End If
                targetNode = targetNode.Parent
            Loop
        End If

        'Currently selected node is a suitable target
        e.Effect = DragDropEffects.Move


    End Sub
    Public Sub TreeView1_DragDrop(ByVal sender As System.Object, _
       ByVal e As System.Windows.Forms.DragEventArgs) _
       Handles TreeView1.DragDrop

        ''Check that there is a TreeNode being dragged
        'If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", _
        '      True) = False Then Exit Sub

        ''Get the TreeView raising the event (incase multiple on form)
        'Dim selectedTreeview As TreeView = CType(sender, TreeView)

        ''Get the TreeNode being dragged
        'Dim dropNode As TreeNode = _
        '      CType(e.Data.GetData("System.Windows.Forms.TreeNode"),  _
        '      TreeNode)

        ''The target node should be selected from the DragOver event
        'Dim targetNode As TreeNode = selectedTreeview.SelectedNode

        ''Remove the drop node from its current location
        'dropNode.Remove()

        ''If there is no targetNode add dropNode to the bottom of
        ''the TreeView root nodes, otherwise add it to the end of
        ''the dropNode child nodes
        'If targetNode Is Nothing Then
        '    selectedTreeview.Nodes.Add(dropNode)
        'Else
        '    targetNode.Nodes.Add(dropNode)
        'End If

        ''Ensure the newley created node is visible to
        ''the user and select it
        'dropNode.EnsureVisible()
        'selectedTreeview.SelectedNode = dropNode

        'saveTreeV()
    End Sub

#End Region


    '-----------------------------------------------------------------------------------------------------------------------
    ' FILL shifts in hh:mm in form9 with periode_returned
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Public Sub fill_form9_shift_hours(periode_returned As periode(), i As Integer)

        Dim calc As Double = 0, total As Double = 0, verif As Double = 0

        ''Shift1 percentage :
        'For Each key In periode_returned(i).shift1.Keys
        '    If (Not (key.Contains("_PARTNO"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift1.Item(key)
        'Next

        'If periode_returned(i).shift1.ContainsKey("CYCLE ON") Then
        '    Form9.Label41.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE ON"))
        '    verif = periode_returned(i).shift1.Item("CYCLE ON")
        'Else
        '    Form9.Label41.Text = ("00:00")
        'End If

        ''If periode_returned(i).shift1.ContainsKey("CYCLE OFF") Then
        ''    Form9.Label40.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE OFF"))
        ''    verif = verif + periode_returned(i).shift1.Item("CYCLE OFF")
        ''Else
        ''    Form9.Label40.Text = ("00:00")
        ''End If

        'If periode_returned(i).shift1.ContainsKey("CYCLE OFF") Then
        '    If (Welcome.CSIF_version = 1) Then
        '        Dim other_time As Double
        '        For Each key In (periode_returned(i).shift1.Keys)
        '            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then other_time = other_time + periode_returned(i).shift1.Item(key)
        '        Next

        '        Form9.Label40.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE OFF") + periode_returned(i).shift1.Item("SETUP") + other_time)
        '    Else
        '        Form9.Label40.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE OFF"))
        '    End If

        '    verif = verif + periode_returned(i).shift1.Item("CYCLE OFF")
        'Else
        '    Form9.Label40.Text = ("00.00")
        'End If

        'If (Welcome.CSIF_version <> 1) Then
        '    If periode_returned(i).shift1.ContainsKey("SETUP") Then
        '        Form9.Label39.Text = uptimeToDHMS(periode_returned(i).shift1.Item("SETUP"))
        '        verif = verif + periode_returned(i).shift1.Item("SETUP")
        '    Else
        '        Form9.Label39.Text = "00:00"
        '    End If

        '    If Welcome.CSIF_version = 1 Then Form9.Label39.Text = ""

        '    For Each key In (periode_returned(i).shift1.Keys)
        '        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then calc = calc + periode_returned(i).shift1.Item(key)

        '    Next
        '    verif = verif + calc
        '    Form9.Label32.Text = uptimeToDHMS(calc) '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
        'End If
        'Form9.Label31.Text = uptimeToDHMS(verif)

        'total = 0
        'verif = 0
        'calc = 0
        'Shift2 percentage :
        For Each key In periode_returned(i).shift1.Keys
            If (Not (key.Contains("_PARTNO"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift1.Item(key)
        Next
        If (total <> 0) Then
            If periode_returned(i).shift1.ContainsKey("CYCLE ON") Then
                Form9.Label41.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE ON")) 'Math.Round(periode_returned(i).shift1.Item("CYCLE ON") * 100 / total).ToString("00.00")
                verif = periode_returned(i).shift1.Item("CYCLE ON")
            Else
                Form9.Label41.Text = ("00:00")
            End If

            If periode_returned(i).shift1.ContainsKey("CYCLE OFF") Then
                Form9.Label40.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE OFF")) 'Math.Round(periode_returned(i).shift1.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift1.Item("CYCLE OFF")
            Else
                Form9.Label40.Text = ("00:00")
            End If

            If periode_returned(i).shift1.ContainsKey("SETUP") Then
                Form9.Label39.Text = uptimeToDHMS(periode_returned(i).shift1.Item("SETUP")) 'Math.Round(periode_returned(i).shift1.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift1.Item("SETUP")
            Else
                Form9.Label39.Text = "00:00"
            End If

            If Welcome.CSIF_version = 1 Then
                Form9.Label39.Text = ""
            End If

            For Each key In (periode_returned(i).shift1.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                    calc = calc + periode_returned(i).shift1.Item(key)

                    'If top4.ContainsKey(key) Then
                    '    top4.Item(key) = top4.Item(key) + periode_returned(i).shift1.Item(key)
                    'Else
                    '    top4.Add(key, periode_returned(i).shift1.Item(key))
                    'End If
                End If
                ' calc = calc + periode_returned(i).shift1.Item(key)
                '  If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 2 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc
            Form9.Label32.Text = uptimeToDHMS(calc) 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            Form9.Label31.Text = uptimeToDHMS(verif) 'Math.Round(verif * 100 / total).ToString("00.00")
        Else
            Form9.Label41.Text = "00:00"
            Form9.Label40.Text = "00:00"
            Form9.Label39.Text = "00:00"
            Form9.Label32.Text = "00:00"
            Form9.Label31.Text = "00:00"
        End If
        If Welcome.CSIF_version = 1 Then
            Form9.Label39.Text = ""
            Form9.Label32.Text = ""
            If total = 0 Then
                Form9.Label40.Text = ("00:00")
            Else
                '  Form9.Label32.Text = Math.Round((periode_returned(i).shift1.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift1.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift1.Item("CYCLE OFF")
                    If (periode_returned(i).shift1.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift1.Item("SETUP")
                    End If
                    totaloff += calc

                    Form9.Label40.Text = uptimeToDHMS(totaloff)
                    'Form9.Label40.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE OFF") + periode_returned(i).shift1.Item("SETUP") + calc) 'Math.Round((periode_returned(i).shift1.Item("CYCLE OFF") + periode_returned(i).shift1.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Form9.Label40.Text = uptimeToDHMS(calc) 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If

        total = 0
        verif = 0
        calc = 0

        ''''''''''''''From regular fill_form9 function
        'Shift2 percentage :
        For Each key In periode_returned(i).shift2.Keys
            If (Not (key.Contains("_PARTNO"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift2.Item(key)
        Next
        If (total <> 0) Then
            If periode_returned(i).shift2.ContainsKey("CYCLE ON") Then
                Form9.Label51.Text = uptimeToDHMS(periode_returned(i).shift2.Item("CYCLE ON")) 'Math.Round(periode_returned(i).shift2.Item("CYCLE ON") * 100 / total).ToString("00.00")
                verif = periode_returned(i).shift2.Item("CYCLE ON")
            Else
                Form9.Label51.Text = ("00:00")
            End If

            If periode_returned(i).shift2.ContainsKey("CYCLE OFF") Then
                Form9.Label50.Text = uptimeToDHMS(periode_returned(i).shift2.Item("CYCLE OFF")) 'Math.Round(periode_returned(i).shift2.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift2.Item("CYCLE OFF")
            Else
                Form9.Label50.Text = ("00:00")
            End If

            If periode_returned(i).shift2.ContainsKey("SETUP") Then
                Form9.Label49.Text = uptimeToDHMS(periode_returned(i).shift2.Item("SETUP")) 'Math.Round(periode_returned(i).shift2.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift2.Item("SETUP")
            Else
                Form9.Label49.Text = "00:00"
            End If

            If Welcome.CSIF_version = 1 Then
                Form9.Label49.Text = ""
            End If

            For Each key In (periode_returned(i).shift2.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                    calc = calc + periode_returned(i).shift2.Item(key)

                    'If top4.ContainsKey(key) Then
                    '    top4.Item(key) = top4.Item(key) + periode_returned(i).shift2.Item(key)
                    'Else
                    '    top4.Add(key, periode_returned(i).shift2.Item(key))
                    'End If
                End If
                ' calc = calc + periode_returned(i).shift2.Item(key)
                '  If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 2 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc
            Form9.Label45.Text = uptimeToDHMS(calc) 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            Form9.Label44.Text = uptimeToDHMS(verif) 'Math.Round(verif * 100 / total).ToString("00.00")
        Else
            Form9.Label51.Text = "00:00"
            Form9.Label50.Text = "00:00"
            Form9.Label49.Text = "00:00"
            Form9.Label45.Text = "00:00"
            Form9.Label44.Text = "00:00"
        End If
        If Welcome.CSIF_version = 1 Then
            Form9.Label49.Text = ""
            Form9.Label45.Text = ""
            If total = 0 Then
                Form9.Label50.Text = ("00:00")
            Else
                '  Form9.Label45.Text = Math.Round((periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift2.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift2.Item("CYCLE OFF")
                    If (periode_returned(i).shift2.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift2.Item("SETUP")
                    End If
                    totaloff += calc

                    Form9.Label50.Text = uptimeToDHMS(totaloff)

                    'Form9.Label50.Text = uptimeToDHMS(periode_returned(i).shift2.Item("CYCLE OFF") + periode_returned(i).shift2.Item("SETUP") + calc) 'Math.Round((periode_returned(i).shift2.Item("CYCLE OFF") + periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Form9.Label50.Text = uptimeToDHMS(calc) 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If

        total = 0
        verif = 0
        calc = 0


        'Shift3 percentage :
        For Each key In periode_returned(i).shift3.Keys
            If (Not (key.Contains("_PARTNO"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift3.Item(key)
        Next
        If (total <> 0) Then
            If periode_returned(i).shift3.ContainsKey("CYCLE ON") Then
                Form9.Label63.Text = uptimeToDHMS(periode_returned(i).shift3.Item("CYCLE ON")) 'Math.Round(periode_returned(i).shift3.Item("CYCLE ON") * 100 / total).ToString("00.00")
                verif = periode_returned(i).shift3.Item("CYCLE ON")
            Else
                Form9.Label63.Text = ("00:00")
            End If

            If periode_returned(i).shift3.ContainsKey("CYCLE OFF") Then
                Form9.Label62.Text = uptimeToDHMS(periode_returned(i).shift3.Item("CYCLE OFF")) 'Math.Round(periode_returned(i).shift3.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift3.Item("CYCLE OFF")
            Else
                Form9.Label62.Text = ("00:00")
            End If

            If periode_returned(i).shift3.ContainsKey("SETUP") Then
                Form9.Label61.Text = uptimeToDHMS(periode_returned(i).shift3.Item("SETUP")) 'Math.Round(periode_returned(i).shift3.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift3.Item("SETUP")
            Else
                Form9.Label61.Text = "00:00"
            End If

            If Welcome.CSIF_version = 1 Then
                Form9.Label61.Text = ""
            End If

            For Each key In (periode_returned(i).shift3.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                    calc = calc + periode_returned(i).shift3.Item(key)

                    'If top4.ContainsKey(key) Then
                    '    top4.Item(key) = top4.Item(key) + periode_returned(i).shift3.Item(key)
                    'Else
                    '    top4.Add(key, periode_returned(i).shift3.Item(key))
                    'End If
                End If
                ' calc = calc + periode_returned(i).shift3.Item(key)
                '  If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 2 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc
            Form9.Label55.Text = uptimeToDHMS(calc) 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            Form9.Label54.Text = uptimeToDHMS(verif) 'Math.Round(verif * 100 / total).ToString("00.00")
        Else
            Form9.Label63.Text = "00:00"
            Form9.Label62.Text = "00:00"
            Form9.Label61.Text = "00:00"
            Form9.Label55.Text = "00:00"
            Form9.Label54.Text = "00:00"
        End If
        If Welcome.CSIF_version = 1 Then
            Form9.Label61.Text = ""
            Form9.Label55.Text = ""
            If total = 0 Then
                Form9.Label62.Text = ("00:00")
            Else
                '  Form9.Label55.Text = Math.Round((periode_returned(i).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift3.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift3.Item("CYCLE OFF")
                    If (periode_returned(i).shift3.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift3.Item("SETUP")
                    End If
                    totaloff += calc

                    Form9.Label62.Text = uptimeToDHMS(totaloff)

                    'Form9.Label62.Text = uptimeToDHMS(periode_returned(i).shift3.Item("CYCLE OFF") + periode_returned(i).shift3.Item("SETUP") + calc) 'Math.Round((periode_returned(i).shift3.Item("CYCLE OFF") + periode_returned(i).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Form9.Label62.Text = uptimeToDHMS(calc) 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If



    End Sub





    '=======================================================================================================================
    '' '' '' '' '' ''  ____   ____    _____      _      _____   _____ 
    '' '' '' '' '' '' / ___| |  _ \  | ____|    / \    |_   _| | ____|
    '' '' '' '' '' ''| |     | |_) | |  _|     / _ \     | |   |  _|  
    '' '' '' '' '' ''| |___  |  _ <  | |___   / ___ \    | |   | |___ 
    '' '' '' '' '' '' \____| |_| \_\ |_____| /_/   \_\   |_|   |_____|Butt
    '-----------------------------------------------------------------------------------------------------------------------
    ' FORME3 CREATE Button
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click

        'Hide live status
        DashWeb.Hide()
        DashWeb.Close()
        BTN_Dashboard.Text = "Show Live Status"


        'If (DateTimePicker1.Value.Date = DateTimePicker2.Value.Date) Then
        '    MessageBox.Show("Please select two different date")
        '    Exit Sub
        'End If


        Form9.ComboBox1.Items.Clear()
        Form9.Label5.Text = ""

        If Welcome.CSIF_version = 1 Then

            'Chart1.Palette 
            Form9.Chart2.Series("Series3").Color = Color.Transparent
            Form9.Chart2.Series("Series4").Color = Color.Transparent


        End If
        Form9.Location = New System.Drawing.Point(268, 25)

        If Form9.Button23.Text = "%" Then Form9.Button23.Text = "h"
        If Form9.CheckBox1.Checked = True Then Form9.CheckBox1.Checked = False

        If Dashboard.Visible = True Then Dashboard.Close()
        If Form7.Visible = True Then Form7.Close()
        If Form9.Visible = True Then Button6.Text = "Update"
        If Form_Shift_1.Visible = True Then Form_Shift_1.Close()
        If Form_shift_2.Visible = True Then Form_shift_2.Close()
        If Form_Shift_3.Visible = True Then Form_Shift_3.Close()
        If Form_Shift.Visible = True Then Form_Shift.Close()
        Dim periode_ As New periode
        periode_.shift1 = New Dictionary(Of String, Double)
        periode_.shift2 = New Dictionary(Of String, Double)
        periode_.shift3 = New Dictionary(Of String, Double)

        Form9.consolidated = False

        'If CheckBox1.Checked = True Then ' by shifts

        '    ' DETAILLED REPORTS :----------------------------------------------------------------------------------------
        '    periode_returned = CSI_LIB.Detailled(DateTimePicker1.Value, DateTimePicker2.Value, read_tree(), True)
        '    Fill_Combobox_detailled(periode_returned)
        'Else

        ' NON DETAILLED REPORTS :------------------------------------------------------------------------------------
        If DateDiff(DateInterval.Day, DateTimePicker1.Value.Date, DateTimePicker2.Value.Date) > 28 Then
            days = 6

            periode_returned = CSI_LIB.Detailled(DateTimePicker1.Value, DateTimePicker2.Value, read_tree(), True)
            big_periode_returned = CSI_LIB.Evolution(DateTimePicker1.Value, DateTimePicker2.Value, read_tree(), days, True)
            Call Fill_Combo_Form9(periode_returned)
            Form9.Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - Math.Round(DateDiff(DateInterval.Day, DateTimePicker1.Value.Date, DateTimePicker2.Value.Date) / 7) - 1) + 0.5, 88)
        End If

        If (DateDiff(DateInterval.Day, DateTimePicker1.Value.Date, DateTimePicker2.Value.Date) < 30) Then
            days = 1

            periode_returned = CSI_LIB.Detailled(DateTimePicker1.Value, DateTimePicker2.Value, read_tree(), True)
            big_periode_returned = CSI_LIB.Evolution(DateTimePicker1.Value, DateTimePicker2.Value, read_tree(), days, True)
            Call Fill_Combo_Form9(periode_returned)

        End If
        'CHART ZOOM :----------------------------------------------------------------------------------------------

        ' 1Day
        If DateDiff(DateInterval.Day, DateTimePicker1.Value.Date, DateTimePicker2.Value.Date) = 0 Then
            If DateTimePicker1.Value.DayOfWeek > 1 Then
                Form9.Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Form1.week_(0)) - DateTimePicker1.Value.DayOfWeek) + 0.5, 89.5)
                'Form9.Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(0, 89.5)
            Else
                Form9.Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Form1.week_(0)) - DateTimePicker1.Value.DayOfWeek) - 0.5 - 7 + Val(Form1.week_(0)), 89.5)
            End If
        End If

        ' +1Day AND < 28 DAYS
        If DateDiff(DateInterval.Day, DateTimePicker1.Value.Date, DateTimePicker2.Value.Date) < 30 And DateDiff(DateInterval.Day, DateTimePicker1.Value.Date, DateTimePicker2.Value.Date) <> 0 Then
            Form9.Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - DateDiff(DateInterval.Day, DateTimePicker1.Value.Date, DateTimePicker2.Value.Date) - 0.5), 89.5)
        End If

        ' +28 DAYS
        If DateDiff(DateInterval.Day, DateTimePicker1.Value.Date, DateTimePicker2.Value.Date) > 29 Then
            Form9.Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - Math.Round(DateDiff(DateInterval.Day, DateTimePicker1.Value.Date, DateTimePicker2.Value.Date) / 7) - 1) + 0.5, 89.5)
        End If

        '   End If
        Form9.ComboBox1.SelectedIndex = Form9.ComboBox1.Items.Count - 1

        If Welcome.CSIF_version = 1 Then

            'Chart1.Palette 
            Form9.Chart2.Series("Series3").Color = Color.Transparent
            Form9.Chart2.Series("Series4").Color = Color.Transparent


        End If

        Form9.SuspendLayout()
    End Sub
    '=======================================================================================================================


    '-----------------------------------------------------------------------------------------------------------------------
    ' Dates select
    '  
    '-----------------------------------------------------------------------------------------------------------------------
#Region "DATETIMEPICKER"
    '-----------------------------------------------------------------------------------------------------------------------
    ' Set a value to DateTimePicker2 after changing a value to dtp1    
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker2.MinDate = DateTimePicker1.Value
        DateTimePicker2.Value = DateTimePicker1.Value

        If SplitContainer1.Panel2Collapsed = False Then Call TextBox1_Click(Nothing, Nothing)
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' DateTimePicker2    
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        'If DateDiff(DateInterval.Day, DateTimePicker1.Value, DateTimePicker2.Value) > 31 And RadioButton2.Checked = True Then
        '    CheckBox1.Enabled = False
        'Else
        '    CheckBox1.Enabled = True
        'End If


        'If DateDiff(DateInterval.Day, DateTimePicker1.Value, DateTimePicker2.Value) > (Val(Form1.week_(1)) - Val(Form1.week_(0))) = True Then
        '    RadioButton2.Enabled = False
        '    RadioButton1.Checked = True
        'Else
        '    RadioButton2.Enabled = True
        'End If


        'If DateDiff(DateInterval.Day, DateTimePicker1.Value, DateTimePicker2.Value) = 0 Then
        '    RadioButton2.Enabled = False
        '    RadioButton1.Checked = True
        'Else
        '    RadioButton2.Enabled = True
        'End If

        If SplitContainer1.Panel2Collapsed = False Then Call TextBox1_Click(Nothing, Nothing)
    End Sub
#End Region


    '-----------------------------------------------------------------------------------------------------------------------
    ' D W M Y 
    '-----------------------------------------------------------------------------------------------------------------------
#Region "D W M Y "
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DateTimePicker2.Value = DateTimePicker1.Value
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DateTimePicker1.Value = DateTimePicker1.Value.AddDays(Val(Form1.week_(0)) - DateTimePicker1.Value.DayOfWeek)
        DateTimePicker2.Value = DateTimePicker1.Value.AddDays(Val(Form1.week_(1)) - DateTimePicker1.Value.DayOfWeek)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        DateTimePicker1.Value = New Date(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, 1)
        DateTimePicker2.Value = New Date(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, System.DateTime.DaysInMonth(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month))
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        DateTimePicker1.Value = New Date(DateTimePicker1.Value.Year, 1, 1)
        DateTimePicker2.Value = New Date(DateTimePicker1.Value.Year, 12, 31)
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------

#End Region


    '-----------------------------------------------------------------------------------------------------------------------
    ' by shift chekbox 
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
        'If CheckBox1.Checked = False Then
        '    'RadioButton2.Visible = False
        '    'RadioButton1.Visible = False
        '    Form7.Button3.Visible = True
        'Else
        'RadioButton2.Visible = True
        'RadioButton1.Visible = True
        Form7.Button3.Visible = False
        ' End If
    End Sub




#Region "FORM 7 & AND 9"


    '-----------------------------------------------------------------------------------------------------------------------
    ' FILL shifts in % in form9 with periode_returned
    '  
    '-----------------------------------------------------------------------------------------------------------------------

    '-----------------------------------------------------------------------------------------------------------------------
    ' fill_form9_shift
    '  
    '-----------------------------------------------------------------------------------------------------------------------

    Public Sub fill_form9_shift(ByRef periode_returned As periode(), i As Integer)
        Dim calc As Double = 0, total As Double = 0, verif As Double = 0
        Dim top4 As New Dictionary(Of String, Integer)

        'Shift1 percentage :
        For Each key In periode_returned(i).shift1.Keys
            If (Not (key.Contains("_PARTNO"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift1.Item(key)
        Next

        If (total <> 0) Then
            If periode_returned(i).shift1.ContainsKey("CYCLE ON") Then
                Form9.Label41.Text = (periode_returned(i).shift1.Item("CYCLE ON") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift1.Item("CYCLE ON") * 100 / total).ToString("00.00")
                verif = periode_returned(i).shift1.Item("CYCLE ON")
            Else
                Form9.Label41.Text = ("00.00")
            End If

            If periode_returned(i).shift1.ContainsKey("CYCLE OFF") Then
                Form9.Label40.Text = (periode_returned(i).shift1.Item("CYCLE OFF") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift1.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift1.Item("CYCLE OFF")
            Else
                Form9.Label40.Text = ("00.00")
            End If

            If periode_returned(i).shift1.ContainsKey("SETUP") Then
                Form9.Label39.Text = (periode_returned(i).shift1.Item("SETUP") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift1.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift1.Item("SETUP")
            Else
                Form9.Label39.Text = "00.00"
            End If
            If Welcome.CSIF_version = 1 Then Form9.Label39.Text = ""
            For Each key In (periode_returned(i).shift1.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                    calc = calc + periode_returned(i).shift1.Item(key)

                    If top4.ContainsKey(key) Then
                        top4.Item(key) = top4.Item(key) + periode_returned(i).shift1.Item(key)
                    Else
                        top4.Add(key, periode_returned(i).shift1.Item(key))
                    End If
                End If
                ' calc = calc + periode_returned(i).shift2.Item(key)
                '  If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 2 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc
            Form9.Label32.Text = (calc * 100 / total).ToString("00.00") 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            Form9.Label31.Text = (verif * 100 / total).ToString("00.00") 'Math.Round(verif * 100 / total).ToString("00.00")
        Else
            Form9.Label41.Text = ("00.00")
            Form9.Label40.Text = ("00.00")
            Form9.Label39.Text = "00.00"
            Form9.Label32.Text = "00.00"
            Form9.Label31.Text = "00.00"
        End If
        If Welcome.CSIF_version = 1 Then
            Form9.Label39.Text = ""
            Form9.Label32.Text = ""
            If total = 0 Then
                Form9.Label40.Text = ("00.00")
            Else
                '  Form9.Label45.Text = Math.Round((periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift1.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift1.Item("CYCLE OFF")
                    If (periode_returned(i).shift1.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift1.Item("SETUP")
                    End If
                    totaloff += calc

                    Form9.Label40.Text = ((totaloff) * 100 / total).ToString("00.00") 'Math.Round((periode_returned(i).shift1.Item("CYCLE OFF") + periode_returned(i).shift1.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Form9.Label40.Text = ((calc) * 100 / total).ToString("00.00") 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If


        total = 0
        verif = 0
        calc = 0

        'Shift2 percentage :
        For Each key In periode_returned(i).shift2.Keys
            If (Not (key.Contains("_PARTNO"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift2.Item(key)
        Next
        If (total <> 0) Then
            If periode_returned(i).shift2.ContainsKey("CYCLE ON") Then
                Form9.Label51.Text = (periode_returned(i).shift2.Item("CYCLE ON") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift2.Item("CYCLE ON") * 100 / total).ToString("00.00")
                verif = periode_returned(i).shift2.Item("CYCLE ON")
            Else
                Form9.Label51.Text = ("00.00")
            End If

            If periode_returned(i).shift2.ContainsKey("CYCLE OFF") Then
                Form9.Label50.Text = (periode_returned(i).shift2.Item("CYCLE OFF") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift2.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift2.Item("CYCLE OFF")
            Else
                Form9.Label50.Text = ("00.00")
            End If

            If periode_returned(i).shift2.ContainsKey("SETUP") Then
                Form9.Label49.Text = (periode_returned(i).shift2.Item("SETUP") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift2.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift2.Item("SETUP")
            Else
                Form9.Label49.Text = "00.00"
            End If

            If Welcome.CSIF_version = 1 Then
                Form9.Label49.Text = ""
            End If

            For Each key In (periode_returned(i).shift2.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                    calc = calc + periode_returned(i).shift2.Item(key)

                    If top4.ContainsKey(key) Then
                        top4.Item(key) = top4.Item(key) + periode_returned(i).shift2.Item(key)
                    Else
                        top4.Add(key, periode_returned(i).shift2.Item(key))
                    End If
                End If
                ' calc = calc + periode_returned(i).shift2.Item(key)
                '  If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 2 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc
            Form9.Label45.Text = (calc * 100 / total).ToString("00.00") 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            Form9.Label44.Text = (verif * 100 / total).ToString("00.00") 'Math.Round(verif * 100 / total).ToString("00.00")
        Else
            Form9.Label51.Text = ("00.00")
            Form9.Label50.Text = ("00.00")
            Form9.Label49.Text = "00.00"
            Form9.Label45.Text = "00.00"
            Form9.Label44.Text = "00.00"
        End If
        If Welcome.CSIF_version = 1 Then
            Form9.Label49.Text = ""
            Form9.Label45.Text = ""
            If total = 0 Then
                Form9.Label50.Text = ("00.00")
            Else
                '  Form9.Label45.Text = Math.Round((periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift2.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift2.Item("CYCLE OFF")
                    If (periode_returned(i).shift2.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift2.Item("SETUP")
                    End If
                    totaloff += calc

                    Form9.Label50.Text = ((totaloff) * 100 / total).ToString("00.00")
                    '= ((periode_returned(i).shift2.Item("CYCLE OFF") + periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00") 'Math.Round((periode_returned(i).shift2.Item("CYCLE OFF") + periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Form9.Label50.Text = ((calc) * 100 / total).ToString("00.00") 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If

        total = 0
        verif = 0
        calc = 0


        'Shift3 percentage :
        For Each key In periode_returned(i).shift3.Keys
            If (Not (key.Contains("_PARTNO"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift3.Item(key)
        Next
        If (total <> 0) Then
            If periode_returned(i).shift3.ContainsKey("CYCLE ON") Then
                Form9.Label63.Text = (periode_returned(i).shift3.Item("CYCLE ON") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift3.Item("CYCLE ON") * 100 / total).ToString("00.00")
                verif = periode_returned(i).shift3.Item("CYCLE ON")
            Else
                Form9.Label63.Text = ("00.00")
            End If

            If periode_returned(i).shift3.ContainsKey("CYCLE OFF") Then
                Form9.Label62.Text = (periode_returned(i).shift3.Item("CYCLE OFF") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift3.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift3.Item("CYCLE OFF")
            Else
                Form9.Label62.Text = ("00.00")
            End If

            If periode_returned(i).shift3.ContainsKey("SETUP") Then
                Form9.Label61.Text = (periode_returned(i).shift3.Item("SETUP") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift3.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift3.Item("SETUP")
            Else
                Form9.Label61.Text = "00.00"
            End If
            If Welcome.CSIF_version = 1 Then Form9.Label61.Text = ""
            For Each key In (periode_returned(i).shift3.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                    '    calc = calc + periode_returned(i).shift3.Item(key)
                    'End If
                    calc = calc + periode_returned(i).shift3.Item(key)
                    If top4.ContainsKey(key) Then
                        top4.Item(key) = top4.Item(key) + periode_returned(i).shift3.Item(key)
                    Else
                        top4.Add(key, periode_returned(i).shift3.Item(key))
                    End If
                End If
                ' If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 3 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc
            Form9.Label55.Text = (calc * 100 / total).ToString("00.00") 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            Form9.Label54.Text = (verif * 100 / total).ToString("00.00") 'Math.Round(verif * 100 / total).ToString("00.00")
        Else

            Form9.Label63.Text = ("00.00")
            Form9.Label62.Text = ("00.00")
            Form9.Label61.Text = "00.00"
            Form9.Label55.Text = "00.00"
            Form9.Label54.Text = "00.00"
        End If
        Dim k As Integer = 0


        If Welcome.CSIF_version = 1 Then
            Form9.Label61.Text = ""
            Form9.Label55.Text = ""
            If total = 0 Then
                Form9.Label55.Text = ("00.00")
            Else
                If (periode_returned(i).shift3.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift3.Item("CYCLE OFF")
                    If (periode_returned(i).shift3.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift3.Item("SETUP")
                    End If
                    totaloff += calc

                    Form9.Label62.Text = ((totaloff) * 100 / total).ToString("00.00")
                    'Form9.Label62.Text = ((periode_returned(i).shift3.Item("CYCLE OFF") + periode_returned(i).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00") 'Math.Round((periode_returned(i).shift3.Item("CYCLE OFF") + periode_returned(i).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Form9.Label62.Text = ((calc) * 100 / total).ToString("00.00") 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If


            End If

        End If

        Dim sorted = From pair In top4
             Order By pair.Value Descending
        Dim sortedDictionary = sorted.ToDictionary(Function(p) p.Key, Function(p) p.Value)
        Dim subtotal As Double = 0

        For Each value In sortedDictionary
            subtotal = value.Value + subtotal
        Next

        Dim status As String = "", percent As String = "", keyname As String = ""
        Dim tempdouble As Double


        For Each key In sortedDictionary
            If key.Key.Length > 10 Then
                keyname = key.Key.Substring(0, 10)
            Else
                keyname = key.Key
            End If
            status = status & vbCrLf & keyname
            tempdouble = key.Value / subtotal
            tempdouble = tempdouble * 100
            percent = percent & vbCrLf & ": " & uptimeToDHMS(key.Value) & " , " & tempdouble.ToString("00.00") & " %" '(Math.Round(key.Value * 100 / subtotal)).ToString & " %"
            k = k + 1
            If k = 4 Then Exit For
        Next

        Form9.Label5.Text = status
        Form9.Label6.Text = percent
        If percent = "" Then
            Form9.PictureBox3.Visible = False
        Else
            Form9.PictureBox3.Visible = True
        End If


    End Sub


    Public Sub fill_form9(ByRef periode_returned As periode(), ThirdDim As Integer)


        '1period
        Form9.Label28.Text = (Val(periode_returned(ThirdDim))).ToString("00.00")
        Form9.Label27.Text = (Val(periode_returned(ThirdDim))).ToString("00.00")
        Form9.Label26.Text = (Val(periode_returned(ThirdDim))).ToString("00.00")
        Form9.Label22.Text = (Val(periode_returned(ThirdDim))).ToString("00.00")
        Form9.Label21.Text = (Val(periode_returned(ThirdDim))).ToString("00.00")

    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' rb1  
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs)
        '  CheckBox1.Enabled = True
    End Sub



    '-----------------------------------------------------------------------------------------------------------------------
    ' Call fill_form7 + fill form7.combobox1
    '  Use general_array , and fill the form7 labels
    '-----------------------------------------------------------------------------------------------------------------------
    Public Sub Fill_Combobox_detailled(ByRef periode_ As periode())

        Dim i As Integer

        For i = 0 To (UBound(periode_) - 1)
            Form7.ComboBox1.Items.Add(periode_(i).machine_name & " : " & periode_(i).date_)
        Next

        Form7.ComboBox1.SelectedIndex = i - 1
        Call fill_form_7(periode_(i - 1))

        Form7.SuspendLayout()
        Form7.Show()
    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' Call fill_form7  
    '  Use general_array , and fill the form7 labels
    '-----------------------------------------------------------------------------------------------------------------------
    Public Sub fill_form_7(ByRef periode_ As periode)
        Dim cycleTime As Double = 0

        'Clear the GridView
        Form7.DataGridView1.Rows.Clear()
        Form7.DataGridView2.Rows.Clear()
        Form7.DataGridView3.Rows.Clear()
        Form7.ListBox1.Items.Clear()
        Form7.ListBox2.Items.Clear()
        Form7.ListBox3.Items.Clear()

        'For Each row In Form7.DataGridView1.Rows
        '    Form7.DataGridView1.Rows.Clear()
        'Next
        'For Each row In Form7.DataGridView2.Rows
        '    Form7.DataGridView2.Rows.Clear()
        'Next
        'For Each row In Form7.DataGridView3.Rows
        '    Form7.DataGridViews3.Rows.Clear()
        'Next

        Dim j As Integer = 0
        Dim total1 As Double = 0, total2 As Double = 0, total As Double = 0
        Dim verif As Integer = 0

        'Shift1 percentage :
        For Each key In periode_.shift1.Keys
            If (Not (key.Contains("_PARTNO"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_.shift1.Item(key)
        Next


        '' Fill Form7 , shift 1   
        If periode_.shift1.ContainsKey("CYCLE ON") And (total <> 0) Then
            cycleTime = periode_.shift1.Item("CYCLE ON")
            Form7.Label28.Text = cycleTime * 100 / total
            verif = cycleTime + verif
            Form7.DataGridView1.Rows.Add("CYCLE ON", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            Form7.DataGridView1.Rows.Add("CYCLE ON", "00:00", "00")
        End If

        If periode_.shift1.ContainsKey("CYCLE OFF") And (total <> 0) Then
            cycleTime = periode_.shift1.Item("CYCLE OFF")
            verif = cycleTime + verif
            Form7.Label27.Text = cycleTime * 100 / total
            Form7.DataGridView1.Rows.Add("CYCLE OFF", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            Form7.DataGridView1.Rows.Add("CYCLE OFF", "00:00", "00")
        End If

        If periode_.shift1.ContainsKey("SETUP") And (total <> 0) Then
            cycleTime = periode_.shift1.Item("SETUP")
            verif = cycleTime + verif
            Form7.Label26.Text = cycleTime * 100 / total
            Form7.DataGridView1.Rows.Add("SETUP", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            Form7.DataGridView1.Rows.Add("SETUP", "00:00", "00")
        End If

        'Other status in shift1
        For Each key In periode_.shift1.Keys
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" Then
                If key.Contains("_PARTNO") Then
                    Form7.ListBox1.Items.Add(key.Remove(0, 8))
                Else
                    cycleTime = periode_.shift1.Item(key)
                    verif = cycleTime + verif
                    If total <> 0 Then Form7.DataGridView1.Rows.Add(key, uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
                    Form7.Label25.Text = cycleTime + Val(Form7.Label25.Text)
                End If
            End If
            '   If Not (key.Contains("_PARTNO")) Then total = total + cycleTime
        Next
        Form7.Label25.Text = Val(Form7.Label25.Text) * 100 / total
        'TOTAL
        If total <> 0 Then

            Form7.DataGridView1.Rows.Add("TOTAL", uptimeToDHMS(total), ((verif / total) * 100)) 'Math.Round((verif / total) * 100))
        Else
            Form7.DataGridView1.Rows.Add("TOTAL", "00:00", "00")
        End If


        'CON
        Form7.DataGridView1.Rows(0).DefaultCellStyle.BackColor = Color.Green
        Form7.DataGridView1.Rows(0).DefaultCellStyle.ForeColor = Color.White
        '_COFF
        Form7.DataGridView1.Rows(1).DefaultCellStyle.BackColor = Color.Red
        Form7.DataGridView1.Rows(1).DefaultCellStyle.ForeColor = Color.White
        'SETUP
        Form7.DataGridView1.Rows(2).DefaultCellStyle.BackColor = Color.Blue
        Form7.DataGridView1.Rows(2).DefaultCellStyle.ForeColor = Color.White

        'OTHER
        For j = 0 To Form7.DataGridView1.Rows.Count - 4

            Form7.DataGridView1.Rows(j + 3).DefaultCellStyle.BackColor = Color.Yellow
            Form7.DataGridView1.Rows(j + 3).DefaultCellStyle.ForeColor = Color.Black
        Next j

        Form7.DataGridView1.Rows(Form7.DataGridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.White
        Form7.DataGridView1.Rows(Form7.DataGridView1.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Black




        'Shift2 percentage :
        total = 0

        For Each key In periode_.shift2.Keys
            If (Not (key.Contains("_PARTNO"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_.shift2.Item(key)
        Next



        verif = 0

        '' Form7 , shift 2  
        If periode_.shift2.ContainsKey("CYCLE ON") And (total <> 0) Then
            cycleTime = periode_.shift2.Item("CYCLE ON")
            Form7.Label39.Text = cycleTime * 100 / total
            verif = cycleTime + verif
            Form7.DataGridView2.Rows.Add("CYCLE ON", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            Form7.DataGridView2.Rows.Add("CYCLE ON", "00:00", "00")
        End If

        If periode_.shift2.ContainsKey("CYCLE OFF") And (total <> 0) Then
            cycleTime = periode_.shift2.Item("CYCLE OFF")
            verif = cycleTime + verif
            Form7.Label38.Text = cycleTime * 100 / total
            Form7.DataGridView2.Rows.Add("CYCLE OFF", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            Form7.DataGridView2.Rows.Add("CYCLE OFF", "00:00", "00")
        End If

        If periode_.shift2.ContainsKey("SETUP") And (total <> 0) Then
            cycleTime = periode_.shift2.Item("SETUP")
            verif = cycleTime + verif
            Form7.Label37.Text = cycleTime * 100 / total
            Form7.DataGridView2.Rows.Add("SETUP", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            Form7.DataGridView2.Rows.Add("SETUP", "00:00", "00")
        End If

        For Each key In periode_.shift2.Keys
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" Then
                If key.Contains("_PARTNO") Then
                    Form7.ListBox2.Items.Add(key.Remove(0, 8))
                Else
                    cycleTime = periode_.shift2.Item(key)
                    verif = cycleTime + verif
                    If total <> 0 Then Form7.DataGridView2.Rows.Add(key, uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
                    Form7.Label36.Text = cycleTime + Val(Form7.Label36.Text)
                End If
            End If
            '   If Not (key.Contains("_PARTNO")) Then total2 = total2 + cycleTime
        Next
        Form7.Label36.Text = Val(Form7.Label36.Text) * 100 / total
        'TOTAL
        If total <> 0 Then
            Form7.DataGridView2.Rows.Add("TOTAL", uptimeToDHMS(total), ((verif / total) * 100)) 'Math.Round((verif / total) * 100))
        Else
            Form7.DataGridView2.Rows.Add("TOTAL", "00:00", "00")
        End If


        'CON
        Form7.DataGridView2.Rows(0).DefaultCellStyle.BackColor = Color.Green
        Form7.DataGridView2.Rows(0).DefaultCellStyle.ForeColor = Color.White
        '_COFF
        Form7.DataGridView2.Rows(1).DefaultCellStyle.BackColor = Color.Red
        Form7.DataGridView2.Rows(1).DefaultCellStyle.ForeColor = Color.White
        'SETUP
        Form7.DataGridView2.Rows(2).DefaultCellStyle.BackColor = Color.Blue
        Form7.DataGridView2.Rows(2).DefaultCellStyle.ForeColor = Color.White

        'OTHER
        For j = 0 To Form7.DataGridView2.Rows.Count - 4

            Form7.DataGridView2.Rows(j + 3).DefaultCellStyle.BackColor = Color.Yellow
            Form7.DataGridView2.Rows(j + 3).DefaultCellStyle.ForeColor = Color.Black
        Next j

        Form7.DataGridView2.Rows(Form7.DataGridView2.Rows.Count - 1).DefaultCellStyle.BackColor = Color.White
        Form7.DataGridView2.Rows(Form7.DataGridView2.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Black





        total = 0
        'Shift3 percentage :
        For Each key In periode_.shift3.Keys
            If (Not (key.Contains("_PARTNO"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_.shift3.Item(key)
        Next

        verif = 0

        '' Form7 , shift 3   
        If periode_.shift3.ContainsKey("CYCLE ON") And (total <> 0) Then
            cycleTime = periode_.shift3.Item("CYCLE ON")
            verif = cycleTime + verif
            Form7.Label67.Text = cycleTime * 100 / total
            Form7.DataGridView3.Rows.Add("CYCLE ON", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            Form7.DataGridView3.Rows.Add("CYCLE ON", "00:00", "00")
        End If

        If periode_.shift3.ContainsKey("CYCLE OFF") And (total <> 0) Then
            cycleTime = periode_.shift3.Item("CYCLE OFF")
            verif = cycleTime + verif
            Form7.Label66.Text = cycleTime * 100 / total
            Form7.DataGridView3.Rows.Add("CYCLE OFF", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            Form7.DataGridView3.Rows.Add("CYCLE OFF", "00:00", "00")
        End If

        If periode_.shift3.ContainsKey("SETUP") And (total <> 0) Then
            cycleTime = periode_.shift3.Item("SETUP")
            verif = cycleTime + verif
            Form7.Label65.Text = cycleTime * 100 / total
            Form7.DataGridView3.Rows.Add("SETUP", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            Form7.DataGridView3.Rows.Add("SETUP", "00:00", "00")
        End If

        For Each key In periode_.shift3.Keys
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" Then
                If key.Contains("_PARTNO") Then
                    Form7.ListBox3.Items.Add(key.Remove(0, 8))
                Else
                    cycleTime = periode_.shift3.Item(key)

                    verif = cycleTime + verif
                    If total <> 0 Then Form7.DataGridView3.Rows.Add(key, uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
                    Form7.Label64.Text = cycleTime + Val(Form7.Label64.Text)

                End If
            End If
            '    If Not (key.Contains("_PARTNO")) Then total = total + cycleTime
        Next
        Form7.Label64.Text = Val(Form7.Label64.Text) * 100 / total
        'TOTAL
        If total <> 0 Then
            Form7.DataGridView3.Rows.Add("TOTAL", uptimeToDHMS(total), (verif / total * 100)) 'Math.Round(verif * 100 / total))
        Else
            Form7.DataGridView3.Rows.Add("TOTAL", "00:00", "00")
        End If


        'CON
        Form7.DataGridView3.Rows(0).DefaultCellStyle.BackColor = Color.Green
        Form7.DataGridView3.Rows(0).DefaultCellStyle.ForeColor = Color.White
        '_COFF
        Form7.DataGridView3.Rows(1).DefaultCellStyle.BackColor = Color.Red
        Form7.DataGridView3.Rows(1).DefaultCellStyle.ForeColor = Color.White
        'SETUP
        Form7.DataGridView3.Rows(2).DefaultCellStyle.BackColor = Color.Blue
        Form7.DataGridView3.Rows(2).DefaultCellStyle.ForeColor = Color.White

        'OTHER
        For j = 0 To Form7.DataGridView3.Rows.Count - 4

            Form7.DataGridView3.Rows(j + 3).DefaultCellStyle.BackColor = Color.Yellow
            Form7.DataGridView3.Rows(j + 3).DefaultCellStyle.ForeColor = Color.Black
        Next j

        Form7.DataGridView3.Rows(Form7.DataGridView3.Rows.Count - 1).DefaultCellStyle.BackColor = Color.White
        Form7.DataGridView3.Rows(Form7.DataGridView3.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Black


        'Form7.DataGridView1.Rows(Form7.DataGridView1.RowCount - 1).Selected = True
        'Form7.DataGridView2.Rows(Form7.DataGridView2.RowCount - 1).Selected = True
        'Form7.DataGridView3.Rows(Form7.DataGridView3.RowCount - 1).Selected = True

        Form7.DataGridView1.CurrentCell = Nothing
        Form7.DataGridView1.Columns.Item(2).SortMode = DataGridViewColumnSortMode.NotSortable

        Form7.DataGridView2.CurrentCell = Nothing
        Form7.DataGridView2.Columns.Item(2).SortMode = DataGridViewColumnSortMode.NotSortable
        Form7.DataGridView3.CurrentCell = Nothing
        Form7.DataGridView3.Columns.Item(2).SortMode = DataGridViewColumnSortMode.NotSortable

    End Sub



    '-----------------------------------------------------------------------------------------------------------------------
    ' fill combo form 9
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Public Sub Fill_Combo_Form9(ByRef period_return As periode())
        Dim i As Integer
        Try
            If Not IsNothing(period_return) Then
                For i = 0 To (UBound(period_return) - 1)
                    Form9.ComboBox1.Items.Add(period_return(i).machine_name & " : " & period_return(i).date_)
                Next
                Form9.Show()
            End If
        Catch ex As Exception
            MessageBox.Show("CSIFLEX Failed to display the reporting datas : " & ex.Message)
        End Try

    End Sub

#End Region


    '-----------------------------------------------------------------------------------------------------------------------
    ' MOVE FORME 3
    '  
    '-----------------------------------------------------------------------------------------------------------------------
#Region "form3 move"
    Dim _MouseDown As Boolean, _MouseX As Integer, _MouseY As Integer
    Private Sub form3_mouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        _MouseDown = True
        _MouseX = e.X
        _MouseY = e.Y
    End Sub

    Private Sub Form3_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        _MouseDown = False
    End Sub


    Private Sub Form3_Mousemove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove


        If _MouseDown = True Then
            Form1.SuspendLayout()
            Me.Left = Me.Left + (e.X - _MouseX)
            Me.Top = Me.Top + (e.Y - _MouseY)
            If Me.Top < 20 Then Me.Top = 0
            If Me.Left < 20 Then Me.Left = 0
            Form1.ResumeLayout(True)

        End If

    End Sub
#End Region

    '-----------------------------------------------------------------------------------------------------------------------
    ' PARTNO
    '  
    '-----------------------------------------------------------------------------------------------------------------------
#Region "PARTNO"
    '-----------------------------------------------------------------------------------------------------------------------
    ' VIEW PART NUMBERS butt
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click

        ' 334, 477'Form14.Show()
        If Button2.Text = "View Part Numbers" Then 'Panel closed

            SplitContainer1.Panel2Collapsed = False
            Button2.Text = "Hide Part Numbers"
            filtered_table = Nothing
            If SplitContainer1.Panel2Collapsed = False Then Call LoadPartNumbers(read_tree())
        Else

            SplitContainer1.Panel2Collapsed = True
            Button2.Text = "View Part Numbers"
            filtered_table = Nothing
            If SplitContainer1.Panel2Collapsed = False Then Call LoadPartNumbers(read_tree())
        End If

    End Sub



    '-----------------------------------------------------------------------------------------------------------------------
    ' LOAD ALL THE PART NUMBERS for the selected machines (w/o filtering the dates)
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub LoadPartNumbers(machine As String())





        Dim CSILIB As New CSI_Library.CSI_Library

        If UBound(machine) < 0 Then
            DGV_PartsNumber_Form3.DataSource = Nothing
            DGV_PartsNumber_Form3.Refresh()
            GoTo End_
        End If

        '*************************************************************************************************************************************************'
        '**** DB Connection
        '*************************************************************************************************************************************************'
        Dim cnt As OleDb.OleDbConnection
        Try
            Dim dbConnectStr As String
            dbConnectStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CSILIB.db_path(True) & "\CSI_Database.mdb;"
            cnt = New System.Data.OleDb.OleDbConnection(dbConnectStr)
            cnt.Open()

            If cnt.State = 1 Then
            Else
                MessageBox.Show("Connection to the database failed")
                GoTo end_
            End If
        Catch ex As Exception
            MessageBox.Show(" Enable to establish a connection to the database : " & ex.Message, vbCritical + vbSystemModal)
            GoTo end_
        End Try
        '*************************************************************************************************************************************************'
        '**** DB Connection -END
        '*************************************************************************************************************************************************'


        Dim adapter As New OleDbDataAdapter
        Dim reader As OleDbDataReader
        Dim table_ As New System.Data.DataTable("tmp_table")
        Dim table_all As New System.Data.DataTable("tmp_table")
        Dim tbl_temp As New DataTable("tbl_temp")
        Dim tbl_temp2 As New DataTable("tbl_temp2")
        Dim tmp_table_cmd As New OleDbCommand
        Dim command As String = ""











        '   '_PARTNO' for each selected machine
        If UBound(machine) > 0 Then
            For i = 0 To UBound(machine)
                tbl_temp2.Reset()
                machine(i) = Strings.Replace(machine(i), " ", "")
                machine(i) = Strings.Replace(machine(i), "-", "")
                command = "SELECT   status, year_, month_, day_ ,'" + machine(i) + "' as machineName FROM  [SHIFT_tbl_" & machine(i) & "] WHERE [Status] LIKE '_PARTNO:%'   "

                tmp_table_cmd.CommandText = command
                tmp_table_cmd.Connection = cnt

                reader = tmp_table_cmd.ExecuteReader
                tbl_temp2.Load(reader)
                table_all.Merge(tbl_temp2)
            Next i 'machine 

        End If


        cnt.Close()



        table_ = table_all

        table_.Columns.Add("Date", System.Type.GetType("System.DateTime"))
        table_.Columns(0).ColumnName = "Parts"
        Dim cultures As CultureInfo = New CultureInfo("fr-FR")
        For Each row As DataRow In table_.Rows
            row.Item(0) = row.Item(0).ToString.Remove(0, 8)
            If row.Item(3) >= System.DateTime.DaysInMonth((2000 + row.Item(1)), (row.Item(2))) Then
            Else
                row.Item(5) = Convert.ToDateTime(row.Item(3).ToString & "-" & row.Item(2) & "-" & (2000 + row.Item(1)), cultures) 'row.Item(3).ToString & " " & CSILIB.mois_(Val(row.Item(2))) & " " & row.Item(1).ToString
            End If
        Next

        table_.Columns.Remove("Day_")
        table_.Columns.Remove("Month_")
        table_.Columns.Remove("Year_")
        table_.Columns("machineName").SetOrdinal(0)
        table_.Columns("parts").SetOrdinal(1)
        table_.Columns("Date").SetOrdinal(2)
        table_.Columns(0).ColumnName = "machine"
        filtered_table = table_
        filtered_table.Locale = cultures
        DGV_PartsNumber_Form3.DataSource = table_

        DGV_PartsNumber_Form3.Columns(1).DefaultCellStyle.Format = ("dd MMMM yyyy")
        Call TextBox1_Click(Nothing, Nothing)
End_:

    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.KeyUp
        If SplitContainer1.Panel2Collapsed = False Then

            Dim date1 As Date = DateTimePicker1.Value.Date
            Dim date2 As Date = DateTimePicker2.Value.Date

            Dim filter As String = "Date >= #" & date1.ToString("dd MMMM yyyy") & "# and Date <= #" & date2.ToString("dd MMMM yyyy") & "#"
            If Not (filtered_table Is Nothing) Then
                Dim tmp_table_rows As System.Data.DataRow() = filtered_table.Select(filter)

                If UBound(tmp_table_rows) > 0 Then
                    Dim tmp_table_ As System.Data.DataTable = tmp_table_rows.CopyToDataTable

                    Dim view As New DataView(tmp_table_)
                    view.RowFilter = ("Parts like '" + TextBox1.Text & "*'")
                    DGV_PartsNumber_Form3.DataSource = view
                Else
                    Dim view As New DataView(filtered_table)
                    view.RowFilter = ("Parts like '" + TextBox1.Text & "*'")


                    DGV_PartsNumber_Form3.DataSource = Nothing


                End If
            End If
            DGV_PartsNumber_Form3.Refresh()
        End If
    End Sub

#End Region


    '===================================================================================================
    ' ADD a groupe IN TREEVIEW
    '===================================================================================================
    Public groupename___ As String = ""
    Private Sub addgroupe(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        'groupename.ShowDialog()
        'If IsNothing(TreeView1.SelectedNode.Nodes.Count) Then
        '    groupename.Show()
        '    If groupename___ = "" Then groupename___ = "Groupe" & TreeView1.SelectedNode.Nodes.Count.ToString
        '    TreeView1.Nodes.Add(groupename___, groupename___)
        'Else
        '    TreeView1.SelectedNode.Nodes.Add(groupename___, groupename___)

        'End If

        'saveTreeV()
    End Sub

    Public selected_node_1 As TreeNode

    '============================================
    ' TREEVIEW 1 select a node with Right clic to rename/delete 
    '============================================
    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        'If e.Button = Windows.Forms.MouseButtons.Right Then
        '    TreeView1.SelectedNode = e.Node
        '    selected_node_1 = e.Node
        '    ContextMenuADD.Items.Item(1).Visible = True
        '    ContextMenuADD.Items.Item(2).Visible = True
        'End If
    End Sub



    Private Sub Rename_treenode_treeview1(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Dim oldlabel As String = TreeView1.SelectedNode.Text

        If Not (IsNothing(TreeView1.SelectedNode)) Then TreeView1.SelectedNode.BeginEdit()
        TreeView1.SelectedNode.Name = oldlabel
        saveLastTreeV()
    End Sub

    Private Sub Delete_treenode_treeview1(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click

        Dim tn As TreeNode = selected_node_1
        If tn IsNot Nothing Then
            Dim iRet As Integer
            iRet = MessageBox.Show("Are you certain you want to delete " & tn.Text & "?", _
                MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2)
            If iRet = vbYes Then
                TreeView1.Nodes.Remove(selected_node_1)
            End If
        End If
        saveLastTreeV()
    End Sub


    Private Sub saveLastTreeV()

        Dim serializer As TreeViewSerializer = New TreeViewSerializer()


        serializer.SerializeTreeView(Me.TreeView1, System.Windows.Forms.Application.StartupPath + "\sys\" & Form1.username_ & "_TreeView.xml")


    End Sub
    Private Sub loadLastTreeV()
        Me.TreeView1.Nodes.Clear()
        Dim serializer As TreeViewSerializer = New TreeViewSerializer()

        serializer.DeserializeTreeView(Me.TreeView1, System.Windows.Forms.Application.StartupPath & "\sys\" & Form1.username_ & "_TreeView.xml")

        checkForMachineSelected()
    End Sub

    Private Sub loadStartupConfigTreeV()
        'treeview
        Me.TreeView1.Nodes.Clear()
        Dim serializer As TreeViewSerializer = New TreeViewSerializer()
        serializer.DeserializeTreeView(Me.TreeView1, System.Windows.Forms.Application.StartupPath & "\sys\StartupConfig_TreeView.xml")

        checkForMachineSelected()
    End Sub

    Private Sub checkForMachineSelected()
        Dim machines As String() = read_tree()
        If UBound(machines) > -1 Then

            Button6.Enabled = True
            If Welcome.CSIF_version <> 1 Then Button2.Enabled = True
            If Welcome.CSIF_version <> 1 Then BTN_Dashboard.Enabled = True
            '  TextBox1.Enabled = True
            '   DataGridView1.Enabled = True
        Else
            BTN_Dashboard.Enabled = False
            Button6.Enabled = False
            Button2.Enabled = False
            TextBox1.Clear()

        End If
    End Sub


    Private Sub loadStartupParams()
        'report dates
        If My.Computer.FileSystem.FileExists(System.Windows.Forms.Application.StartupPath & "\sys\StartupConfig_.sys") Then
            Using r As StreamReader = New StreamReader(Windows.Forms.Application.StartupPath & "\sys\StartupConfig_.sys", False)
                'Dim lib___ As New CSI_Library.CSI_Library
                Dim tmp As String() = Split(r.ReadLine, ";")
                r.Close()
                If (tmp(0).Contains("Report")) Then
                    Dim nudval As String = tmp(1).Substring(tmp(1).IndexOf("=") + 1)
                    Dim daystoreport As Integer = Int32.Parse(nudval)
                    Dim enddate As Date = DateTimePicker2.Value
                    DateTimePicker1.Value = DateTimePicker1.Value.AddDays(-daystoreport)
                    DateTimePicker2.Value = enddate

                    Button6.PerformClick()
                Else
                    BTN_Dashboard.PerformClick()
                End If
            End Using
        End If
    End Sub


    Private Sub TimerDrawTree_Tick(sender As Object, e As EventArgs) Handles TimerDrawTree.Tick

        If Not (TimerDrawTree.Interval = GlobalVariables.refresh_Interval) Then
            TimerDrawTree.Interval = GlobalVariables.refresh_Interval
        End If

        If (GlobalVariables.ServerIsListening = True) Then
            font_tree()
        End If

    End Sub

    Private Sub BTN_Dashboard_Click(sender As Object, e As EventArgs) Handles BTN_Dashboard.Click

        If (BTN_Dashboard.Text.Contains("Show")) Then
            'Replaced Dashboard by DashGrid, then replaced by DashWeb
            DashWeb.Close()
            DashWeb.Location = New System.Drawing.Point(268, 25)

            'If Dashboard.Visible = True Then Dashboard.Close()
            If Form7.Visible = True Then Form7.Close()
            If Form9.Visible = True Then Form9.Close()
            If Form_Shift_1.Visible = True Then Form_Shift_1.Close()
            If Form_shift_2.Visible = True Then Form_shift_2.Close()
            If Form_Shift_3.Visible = True Then Form_Shift_3.Close()
            If Form_Shift.Visible = True Then Form_Shift.Close()

            DashWeb.Show()
            DashWeb.SuspendLayout()
            BTN_Dashboard.Text = "Hide Live Status"
        Else
            DashWeb.Hide()
            DashWeb.Close()
            BTN_Dashboard.Text = "Show Live Status"
        End If

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class

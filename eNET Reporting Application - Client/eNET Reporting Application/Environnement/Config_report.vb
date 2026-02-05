Imports System.Data.SQLite
Imports System.Globalization
Imports System.IO
Imports System.IO.File
Imports System.Threading
Imports System.Windows
Imports System.Xml
Imports CSI_Library
Imports CSI_Library.CSI_DATA
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities
Imports MySql.Data.MySqlClient
Imports TreeViewSerialization

Public Class Config_report

#Region "VAR"
    Public globalVariable As New GlobalVariables
    Public filtered_table As System.Data.DataTable
    Public readed As Integer = 0, toread As Integer = 0
    Public periode_returned As periode()
    'Public big_periode_returned As periode(,)
    Public CSI_LIB As New CSI_Library.CSI_Library(False)
    Public days As Integer
    Public machine_Report_BarChart As Integer
    Public dbConnectStr As String
    Dim cnt As System.Data.OleDb.OleDbConnection
    Dim Catalog As Object
    Public savepath_xlsx As String
    Public savepath_pdf As String
    Public general_array(26, 3, 1) As String
    Public from_Config_report As Boolean = False
    Public list As New List(Of String)
    Public list2 As New List(Of String)
    Public tree_j As Integer
    Public tree_k As Integer
    Public Super_array As New List(Of String(,))
    Public for_Report_BarChart As Boolean = False
    Public Date_ As String
    'Public chemin_eNET As String
    Public stat(3) As String
    Public Report_BarChart_loaded As Boolean = False
    Public toExport As String()
    Dim mt_present As Boolean = False
    Dim st_present As Boolean = False
    Public MyImageList As ImageList
    Public ID As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
    Dim CheckedNodes As New List(Of TreeNode)
    Public big_periode_returned As periode(,)
    Public machineList As String()
    Public dateStart As Date
    Public dateEnd As Date
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath

#End Region

    Private Sub Config_report_0(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.ResumeLayout()

        'Thread.Sleep(1500)
        'loadStartupParams()
    End Sub

    Private Sub Config_report_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        'saveLastTreeV()
    End Sub

    Private Sub Config_report_sizechanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        DGV_PartsNumber.Refresh()


    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' Config_report Load
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Config_report_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.TransparencyKey = Nothing
        Me.ResizeRedraw = True
        SuspendLayout()

        TreeView_machine.ImageList = New ImageList()

        Select Case Welcome.CSIF_version
            Case 1
                BTN_PartNb.Enabled = False
        End Select

        CSI_LIB.connectionString = CSIFLEXSettings.Instance.ConnectionString

        Me.Left = 0
        Me.Top = 0

        Me.Dock = DockStyle.None
        TreeView_machine.BackColor = Color.FromArgb(237, 239, 240)

        SplitContainer1.Panel2Collapsed = True

        Me.BackColor = Color.FromArgb(48, 63, 78)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        DTP_StartDate.Format = DateTimePickerFormat.Custom
        DTP_EndDate.Format = DateTimePickerFormat.Custom
        DTP_StartDate.CustomFormat = "dd MMMM yyyy-ddd"
        DTP_EndDate.CustomFormat = "dd MMMM yyyy-ddd"
        DTP_StartDate.Value = Now.Date.AddDays(-1)

        Dim i As Integer
        Dim j As Integer
        Dim k As Integer

        Dim prefixTemp As String ' temp string, multiple use


        TreeView_machine.Nodes.Clear()

        i = 0
        j = -1
        k = -1
        Dim cpt As Integer = -1
        Dim cpt2 As Integer = 0
        Dim machine_tmp As String
        Dim machName As String

        For Each machine As String In CSIFLEXSettings.GroupMachines

            machName = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.EnetName = machine Or m.Id.ToString() = machine).MachineName
            If machName Is Nothing Then machName = machine

            prefixTemp = Strings.Left(machName, 3)

            Dim node As New TreeNode
            Dim newNode As New TreeNode

            If Strings.Len(machName) > 4 Then
                node.ImageKey = Strings.Right(machName, Strings.Len(machName) - 4)
                node.Text = Strings.Right(machName, Strings.Len(machName) - 4)
            Else
                node.ImageKey = machName
                node.Text = machName
            End If

            Select Case prefixTemp
                Case "_MT"
                    mt_present = True
                    st_present = False

                    list2.Add(machName)
                    TreeView_machine.Nodes.Add(node)
                    cpt = -1
                    j = j + 1
                Case "_ST"
                    st_present = True
                    cpt += 1
                    If mt_present = False Then
                        TreeView_machine.Nodes.Add(Strings.Right(machName, Strings.Len(machName) - 4) & "  ", Strings.Right(machName, Strings.Len(machName) - 4) & "  ")
                    Else
                        TreeView_machine.Nodes(j).Nodes.Add(Strings.Right(machName, Strings.Len(machName) - 4), Strings.Right(machName, Strings.Len(machName) - 4))
                    End If

                    k += 1

                Case Else

                    If (prefixTemp <> "_ST") And (prefixTemp <> "_MT") Then

                        If st_present = False And mt_present = True Then
                            machine_tmp = machName
                            newNode = TreeView_machine.Nodes(j).Nodes.Add(machine_tmp, machine_tmp)
                            If CSIFLEXSettings.Instance.StartupDisplayType = 2 Then newNode.Checked = CSIFLEXSettings.Instance.StartupMachines.Contains(machine_tmp)
                            cpt += 1

                        ElseIf mt_present = False And st_present = True Then
                            machine_tmp = machName
                            newNode = TreeView_machine.Nodes(k).Nodes.Add(machine_tmp, machine_tmp)
                            If CSIFLEXSettings.Instance.StartupDisplayType = 2 Then newNode.Checked = CSIFLEXSettings.Instance.StartupMachines.Contains(machine_tmp)
                            cpt2 += 1

                        ElseIf mt_present = False And st_present = False Then

                            If i = 0 Then 'create MT named "MACHINES"
                                mt_present = True
                                st_present = False

                                list2.Add(machName)
                                TreeView_machine.Nodes.Add("MACHINES")
                                cpt = -1
                                j = j + 1

                                machine_tmp = machName
                                newNode = TreeView_machine.Nodes(j).Nodes.Add(machine_tmp, machine_tmp)
                                If CSIFLEXSettings.Instance.StartupDisplayType = 2 Then newNode.Checked = CSIFLEXSettings.Instance.StartupMachines.Contains(machine_tmp)
                                cpt += 1
                            Else
                                machine_tmp = machName
                                newNode = TreeView_machine.Nodes.Add(machine_tmp, machine_tmp)
                                If CSIFLEXSettings.Instance.StartupDisplayType = 2 Then newNode.Checked = CSIFLEXSettings.Instance.StartupMachines.Contains(machine_tmp)
                            End If

                        Else
                            machine_tmp = machName
                            newNode = TreeView_machine.Nodes(j).Nodes(cpt).Nodes.Add(machine_tmp, machine_tmp)
                            If CSIFLEXSettings.Instance.StartupDisplayType = 2 Then newNode.Checked = CSIFLEXSettings.Instance.StartupMachines.Contains(machine_tmp)
                        End If

                    End If
            End Select
        Next


        Dim int As Integer = 0
        Dim s As String = ""

        Try

            Dim alpha As Integer = 255
            Dim backcolor As Color
            Dim MyImageList As New ImageList()
            Dim colors As Dictionary(Of String, Integer)

            'colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)
            colors = CSIFLEXSettings.StatusColors

            Dim pathResources = $"{ Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) }\Resources\"

            If Not System.IO.Directory.Exists(pathResources) Then
                My.Computer.FileSystem.CreateDirectory(pathResources)
            End If

            For Each status As String In colors.Keys
                s = status
                backcolor = System.Drawing.ColorTranslator.FromWin32(colors(status))
                backcolor = Color.FromArgb(alpha, backcolor.R, backcolor.G, backcolor.B)
                Dim b As New Bitmap(35, 35)
                Dim g As Graphics = Graphics.FromImage(b)
                Dim brush As New SolidBrush(backcolor)
                g.FillEllipse(brush, New RectangleF(0, 0, b.Width, b.Height))
                If Not status.IndexOf("/") = 0 Then
                    status = status.Replace("/", " ")
                End If

                If Not status.IndexOf("\") = 0 Then
                    status = status.Replace("\", " ")
                End If
                b.Save(pathResources & status & ".bmp")
                MyImageList.Images.Add(Image.FromFile(pathResources & status & ".bmp"))

                g.Dispose()
                ID.Add(status, int)
                int = int + 1
            Next

            MyImageList.Images.Add(Image.FromFile(Windows.Forms.Application.StartupPath & "\Resources\d.png"))
            MyImageList.Images.Add(Image.FromFile(Windows.Forms.Application.StartupPath & "\Resources\NC.png"))
            TreeView_machine.ImageList = MyImageList
            ID.Add("trans", int)
            int = int + 1
            ID.Add("NC", int)
            int = int + 1
            backcolor = Color.FromArgb(alpha, 0, 0, 0)
            Dim bb As New Bitmap(35, 35)
            Dim gg As Graphics = Graphics.FromImage(bb)
            Dim brush2 As New SolidBrush(backcolor)
            gg.FillEllipse(brush2, New RectangleF(0, 0, bb.Width, bb.Height))
            bb.Save(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Resources\OT.bmp")
            MyImageList.Images.Add(Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Resources\OT.bmp"))
            ID.Add("OT", int)
            TreeView_machine.Nodes(0).ImageIndex = ID("trans")
            gg.Dispose()

            If Welcome.CSIF_version <> 3 Then
                For i = 0 To TreeView_machine.Nodes(0).Nodes.Count() - 1
                    TreeView_machine.Nodes(0).Nodes(i).ImageIndex = ID("trans")
                Next
            End If

        Catch ex As Exception
            MessageBox.Show("Error while loading live status colors. Please go in setup, select a valid color file (GraphColor.sys) and reboot CSIFLEX.")
            MyImageList = New ImageList()
            MyImageList.Images.Add(Image.FromFile(Windows.Forms.Application.StartupPath & "\Resources\d.png"))
            MyImageList.Images.Add(Image.FromFile(Windows.Forms.Application.StartupPath & "\Resources\NC.png"))
            TreeView_machine.ImageList = MyImageList
            ID.Add("trans", int)
            int = int + 1
            ID.Add("NC", int)
            TreeView_machine.Nodes(0).ImageIndex = ID("trans")
        End Try

        font_tree()
        'follow startup config
        'Moved to form_shown
        'loadStartupParams()
        DGV_PartsNumber.DefaultCellStyle.Font = New Font("segoe", 9)

        check_startupconfig()
        ResumeLayout()

        TreeView_machine.Nodes(0).Expand()
        TreeView_machine.Nodes(0).Nodes(0).Expand()

    End Sub


    Private Sub check_startupconfig()


        If CSIFLEXSettings.Instance.StartupDisplayType = 2 Then
            DTP_StartDate.Value = Today.Date.AddDays((-1) * CSIFLEXSettings.Instance.StartupReportDays)
            DTP_EndDate.Value = Today.Date.AddDays((-1))
            BTN_Create.PerformClick()
        End If

        Return

        If My.Computer.FileSystem.FileExists(rootPath & "\sys\StartupConfig_.csys") Then

            Using r As StreamReader = New StreamReader(rootPath & "\sys\StartupConfig_.csys", False)

                'Dim lib___ As New CSI_Library.CSI_Library
                Dim tmp As String() = Split(r.ReadLine, ";")
                r.Close()
                If (tmp(0).Contains("Report")) Then
                    Dim nudval As String = tmp(1).Substring(tmp(1).IndexOf("=") + 1)

                    DTP_StartDate.Value = Today.Date.AddDays((-1) * nudval)

                    DTP_EndDate.Value = Today.Date.AddDays((-1))

                    Dim tmp_tv As New TreeView


                    If My.Computer.FileSystem.FileExists(rootPath & "\sys\StartupConfig_TreeView.xml") Then
                        '  Me.TreeView_machine.Nodes.Clear()
                        Dim serializer As TreeViewSerializer = New TreeViewSerializer()
                        serializer.DeserializeTreeView(tmp_tv, rootPath & "\sys\StartupConfig_TreeView.xml")
                    End If

                    Dim mch As String()
                    ReDim mch(0)
                    Dim aNode As TreeNode
                    For Each aNode In tmp_tv.Nodes
                        RecursiveRead(aNode, mch)
                    Next
                    ' Return checked_in_treeview1
                    ReDim Preserve mch(UBound(mch) - 1)

                    For Each aNode In TreeView_machine.Nodes
                        writeRecursive(aNode, mch)
                    Next
                    BTN_Create.PerformClick()
                Else
                    BTN_Dashboard.PerformClick()

                End If
            End Using
        End If
    End Sub

    Private Sub writeRecursive(n As TreeNode, mch As String())
        For Each item In mch
            If n.Text = item Then n.Checked = True
        Next
        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            writeRecursive(aNode, mch)
        Next
    End Sub

    Private Sub RecursiveRead(ByVal n As TreeNode, ByRef mch As String())
        If n.Checked = True And n.Nodes.Count = 0 Then
            '   checked_in_treeview1.Add(n.Text)
            If n.Name <> "" Then
                mch(UBound(mch)) = n.Name
                Dim i As Integer = UBound(mch)
                ReDim Preserve mch(UBound(mch) + 1)
            End If
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            RecursiveRead(aNode, mch)
        Next
    End Sub


    Private Sub font_tree()
        ' checked_in_treeview1.Clear()

        Dim aNode As TreeNode

        For Each aNode In TreeView_machine.Nodes
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
            End If
            n.ToolTipText = "No monitoring data available"
        Else
            If n.Nodes.Count > 0 Then
                'If GlobalVariables.mut.WaitOne(1000) Then
            Else
                Dim state As String = ""
                Try

                    If (GlobalVariables.ListOfMachine.Count() > 0) Then
                        Dim name = n.Text
                        Dim enetName = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.MachineName = n.Text).EnetName
                        If enetName Is Nothing Then enetName = n.Text

                        state = GlobalVariables.ListOfMachine.Item(enetName).Statut.Replace("/", " ")
                        n.ImageIndex = ID(state)
                    End If

                Catch ex As Exception
                    If (state = "") Then
                        n.ImageIndex = ID("NC")
                    Else
                        n.ImageIndex = ID("OT")
                    End If
                    System.Console.WriteLine(ex.Message)
                End Try
            End If
            'GlobalVariables.mut.ReleaseMutex()
            'End If




        End If
        TreeView_machine.ShowNodeToolTips = True

        For Each aNode In n.Nodes
            fontRecursive(aNode)
        Next

    End Sub


    Public Sub Fill_combo_Form7(general_array As String(,,))

        Dim i As Integer
        If Report_PieChart.Visible = True Then
            Report_PieChart.Close()
        End If


        For i = 0 To (UBound(general_array, 3)) - 1
            Report_PieChart.CB_Report.Items.Add(general_array(0, 0, i) & " : " & general_array(7, 0, i))
        Next

        Report_PieChart.CB_Report.SelectedIndex = i - 1

        Report_PieChart.Show()
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
    Private Sub TreeView1_AfterCheck(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeView_machine.AfterCheck

        RemoveHandler TreeView_machine.AfterCheck, AddressOf TreeView1_AfterCheck

        Call CheckAllChildNodes(e.Node)

        Dim machines As String() = read_tree()

        If UBound(machines) > -1 Then

            BTN_Create.Enabled = True
            If Welcome.CSIF_version <> 1 Then BTN_PartNb.Enabled = True

        Else

            BTN_Create.Enabled = False
            BTN_PartNb.Enabled = False
            TB_PartsNumber.Clear()

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

        AddHandler TreeView_machine.AfterCheck, AddressOf TreeView1_AfterCheck

        If SplitContainer1.Panel2Collapsed = False Then Call LoadPartNumbers(read_tree())

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

        For Each treeviewnode As TreeNode In Me.TreeView_machine.Nodes
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

        ReDim active_machines(0)

        Dim aNode As TreeNode

        For Each aNode In TreeView_machine.Nodes
            PrintRecursive(aNode)
        Next

        ReDim Preserve active_machines(UBound(active_machines) - 1)

        Array.Sort(active_machines)

        Return active_machines

    End Function

    Private Sub PrintRecursive(ByVal n As TreeNode)

        If n.Checked = True And n.Nodes.Count = 0 Then

            If n.Name <> "" And active_machines.FirstOrDefault(Function(mch) mch = n.Name) Is Nothing Then

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
    Public Sub TreeView1_ItemDrag(ByVal sender As System.Object,
        ByVal e As System.Windows.Forms.ItemDragEventArgs) _


        ''Set the drag node and initiate the DragDrop 
        'DoDragDrop(e.Item, DragDropEffects.Move)

    End Sub

    Public Sub TreeView1_DragEnter(ByVal sender As System.Object,
        ByVal e As System.Windows.Forms.DragEventArgs) _


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

    Public Sub TreeView1_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs)

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

    Public Sub TreeView1_DragDrop(ByVal sender As System.Object,
       ByVal e As System.Windows.Forms.DragEventArgs)


    End Sub

#End Region

    '-----------------------------------------------------------------------------------------------------------------------
    ' FILL shifts in hh:mm in Report_BarChart with periode_returned
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Public Sub fill_Report_BarChart_shift_hours(periode_returned As periode(), i As Integer)

        Dim calc As Double = 0, total As Double = 0, verif As Double = 0


        'Shift2 percentage :
        For Each key In periode_returned(i).shift1.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift1.Item(key)
        Next
        If (total <> 0) Then
            If periode_returned(i).shift1.ContainsKey("CYCLE ON") Then
                Report_BarChart.LBL_CycleOn_Shift1.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE ON"))
                verif = periode_returned(i).shift1.Item("CYCLE ON")
            Else
                Report_BarChart.LBL_CycleOn_Shift1.Text = ("00:00")
            End If

            If periode_returned(i).shift1.ContainsKey("CYCLE OFF") Then
                Report_BarChart.LBL_CycleOff_Shift1.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE OFF")) 'Math.Round(periode_returned(i).shift1.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift1.Item("CYCLE OFF")
            Else
                Report_BarChart.LBL_CycleOff_Shift1.Text = ("00:00")
            End If

            If periode_returned(i).shift1.ContainsKey("SETUP") Then
                Report_BarChart.LBL_Setup_Shift1.Text = uptimeToDHMS(periode_returned(i).shift1.Item("SETUP")) 'Math.Round(periode_returned(i).shift1.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift1.Item("SETUP")
            Else
                Report_BarChart.LBL_Setup_Shift1.Text = "00:00"
            End If

            If Welcome.CSIF_version = 1 Then
                Report_BarChart.LBL_Setup_Shift1.Text = ""
            End If

            For Each key In (periode_returned(i).shift1.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
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
            Report_BarChart.LBL_Other_Shift1.Text = uptimeToDHMS(calc) 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            Report_BarChart.LBL_Total_Shift1.Text = uptimeToDHMS(verif) 'Math.Round(verif * 100 / total).ToString("00.00")
        Else
            Report_BarChart.LBL_CycleOn_Shift1.Text = "00:00"
            Report_BarChart.LBL_CycleOff_Shift1.Text = "00:00"
            Report_BarChart.LBL_Setup_Shift1.Text = "00:00"
            Report_BarChart.LBL_Other_Shift1.Text = "00:00"
            Report_BarChart.LBL_Total_Shift1.Text = "00:00"
        End If
        If Welcome.CSIF_version = 1 Then
            Report_BarChart.LBL_Setup_Shift1.Text = ""
            Report_BarChart.LBL_Other_Shift1.Text = ""
            If total = 0 Then
                Report_BarChart.LBL_CycleOff_Shift1.Text = ("00:00")
            Else
                '  Form9.Label32.Text = Math.Round((periode_returned(i).shift1.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift1.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift1.Item("CYCLE OFF")
                    If (periode_returned(i).shift1.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift1.Item("SETUP")
                    End If
                    totaloff += calc

                    Report_BarChart.LBL_CycleOff_Shift1.Text = uptimeToDHMS(totaloff)
                    'Form9.Label40.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE OFF") + periode_returned(i).shift1.Item("SETUP") + calc) 'Math.Round((periode_returned(i).shift1.Item("CYCLE OFF") + periode_returned(i).shift1.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Report_BarChart.LBL_CycleOff_Shift1.Text = uptimeToDHMS(calc) 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If

        total = 0
        verif = 0
        calc = 0

        ''''''''''''''From regular fill_form9 function
        'Shift2 percentage :
        For Each key In periode_returned(i).shift2.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift2.Item(key)
        Next
        If (total <> 0) Then
            If periode_returned(i).shift2.ContainsKey("CYCLE ON") Then
                Report_BarChart.LBL_CycleOn_Shift2.Text = uptimeToDHMS(periode_returned(i).shift2.Item("CYCLE ON"))
                verif = periode_returned(i).shift2.Item("CYCLE ON")
            Else
                Report_BarChart.LBL_CycleOn_Shift2.Text = ("00:00")
            End If

            If periode_returned(i).shift2.ContainsKey("CYCLE OFF") Then
                Report_BarChart.LBL_CycleOff_Shift2.Text = uptimeToDHMS(periode_returned(i).shift2.Item("CYCLE OFF")) 'Math.Round(periode_returned(i).shift2.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift2.Item("CYCLE OFF")
            Else
                Report_BarChart.LBL_CycleOff_Shift2.Text = ("00:00")
            End If

            If periode_returned(i).shift2.ContainsKey("SETUP") Then
                Report_BarChart.LBL_Setup_Shift2.Text = uptimeToDHMS(periode_returned(i).shift2.Item("SETUP")) 'Math.Round(periode_returned(i).shift2.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift2.Item("SETUP")
            Else
                Report_BarChart.LBL_Setup_Shift2.Text = "00:00"
            End If

            If Welcome.CSIF_version = 1 Then
                Report_BarChart.LBL_Setup_Shift2.Text = ""
            End If

            For Each key In (periode_returned(i).shift2.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
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
            Report_BarChart.LBL_Other_Shift2.Text = uptimeToDHMS(calc) 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            Report_BarChart.LBL_Total_Shift2.Text = uptimeToDHMS(verif) 'Math.Round(verif * 100 / total).ToString("00.00")
        Else
            Report_BarChart.LBL_CycleOn_Shift2.Text = "00:00"
            Report_BarChart.LBL_CycleOff_Shift2.Text = "00:00"
            Report_BarChart.LBL_Setup_Shift2.Text = "00:00"
            Report_BarChart.LBL_Other_Shift2.Text = "00:00"
            Report_BarChart.LBL_Total_Shift2.Text = "00:00"
        End If
        If Welcome.CSIF_version = 1 Then
            Report_BarChart.LBL_Setup_Shift2.Text = ""
            Report_BarChart.LBL_Other_Shift2.Text = ""
            If total = 0 Then
                Report_BarChart.LBL_CycleOff_Shift2.Text = ("00:00")
            Else
                '  Form9.Label45.Text = Math.Round((periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift2.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift2.Item("CYCLE OFF")
                    If (periode_returned(i).shift2.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift2.Item("SETUP")
                    End If
                    totaloff += calc

                    Report_BarChart.LBL_CycleOff_Shift2.Text = uptimeToDHMS(totaloff)

                    'Form9.Label50.Text = uptimeToDHMS(periode_returned(i).shift2.Item("CYCLE OFF") + periode_returned(i).shift2.Item("SETUP") + calc) 'Math.Round((periode_returned(i).shift2.Item("CYCLE OFF") + periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Report_BarChart.LBL_CycleOff_Shift2.Text = uptimeToDHMS(calc) 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If

        total = 0
        verif = 0
        calc = 0


        'Shift3 percentage :
        For Each key In periode_returned(i).shift3.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift3.Item(key)
        Next
        If (total <> 0) Then
            If periode_returned(i).shift3.ContainsKey("CYCLE ON") Then
                Report_BarChart.LBL_CycleOn_Shift3.Text = uptimeToDHMS(periode_returned(i).shift3.Item("CYCLE ON"))
                verif = periode_returned(i).shift3.Item("CYCLE ON")
            Else
                Report_BarChart.LBL_CycleOn_Shift3.Text = ("00:00")
            End If

            If periode_returned(i).shift3.ContainsKey("CYCLE OFF") Then
                Report_BarChart.LBL_CycleOff_Shift3.Text = uptimeToDHMS(periode_returned(i).shift3.Item("CYCLE OFF")) 'Math.Round(periode_returned(i).shift3.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift3.Item("CYCLE OFF")
            Else
                Report_BarChart.LBL_CycleOff_Shift3.Text = ("00:00")
            End If

            If periode_returned(i).shift3.ContainsKey("SETUP") Then
                Report_BarChart.LBL_Setup_Shift3.Text = uptimeToDHMS(periode_returned(i).shift3.Item("SETUP")) 'Math.Round(periode_returned(i).shift3.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift3.Item("SETUP")
            Else
                Report_BarChart.LBL_Setup_Shift3.Text = "00:00"
            End If

            If Welcome.CSIF_version = 1 Then
                Report_BarChart.LBL_Setup_Shift3.Text = ""
            End If

            For Each key In (periode_returned(i).shift3.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
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
            Report_BarChart.LBL_Other_Shift3.Text = uptimeToDHMS(calc) 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            Report_BarChart.LBL_Total_Shift3.Text = uptimeToDHMS(verif) 'Math.Round(verif * 100 / total).ToString("00.00")
        Else
            Report_BarChart.LBL_CycleOn_Shift3.Text = "00:00"
            Report_BarChart.LBL_CycleOff_Shift3.Text = "00:00"
            Report_BarChart.LBL_Setup_Shift3.Text = "00:00"
            Report_BarChart.LBL_Other_Shift3.Text = "00:00"
            Report_BarChart.LBL_Total_Shift3.Text = "00:00"
        End If
        If Welcome.CSIF_version = 1 Then
            Report_BarChart.LBL_Setup_Shift3.Text = ""
            Report_BarChart.LBL_Other_Shift3.Text = ""
            If total = 0 Then
                Report_BarChart.LBL_CycleOff_Shift3.Text = ("00:00")
            Else
                '  Form9.Label55.Text = Math.Round((periode_returned(i).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift3.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift3.Item("CYCLE OFF")
                    If (periode_returned(i).shift3.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift3.Item("SETUP")
                    End If
                    totaloff += calc

                    Report_BarChart.LBL_CycleOff_Shift3.Text = uptimeToDHMS(totaloff)

                    'Form9.Label62.Text = uptimeToDHMS(periode_returned(i).shift3.Item("CYCLE OFF") + periode_returned(i).shift3.Item("SETUP") + calc) 'Math.Round((periode_returned(i).shift3.Item("CYCLE OFF") + periode_returned(i).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Report_BarChart.LBL_CycleOff_Shift3.Text = uptimeToDHMS(calc) 'Math.Round((calc) * 100 / total).ToString("00.00")
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

    Private Sub BTN_Create_Click(sender As Object, e As EventArgs) Handles BTN_Create.Click

        Report_BarChart.SuspendLayout()

        If Machine_util_det.Visible = True Then
            Machine_util_det.Close()
        End If

        Try
            dateStart = New DateTime(DTP_StartDate.Value.Year, DTP_StartDate.Value.Month, DTP_StartDate.Value.Day, 0, 0, 0)
            dateEnd = New DateTime(DTP_EndDate.Value.Year, DTP_EndDate.Value.Month, DTP_EndDate.Value.Day, 23, 59, 59)
            DashWeb.Hide()
            DashWeb.Close()
            BTN_Dashboard.Text = "Show Live Status"

            Report_BarChart.cBoxReports.Items.Clear()
            Report_BarChart.Label5.Text = ""

            If Welcome.CSIF_version = 1 Then

                'Chart1.Palette 
                Report_BarChart.chartBarHistory.Series("Series3").Color = Color.Transparent
                Report_BarChart.chartBarHistory.Series("Series4").Color = Color.Transparent

            End If

            Report_BarChart.Location = New System.Drawing.Point(Me.Location.X + Me.Width, 0)

            If Report_BarChart.btnUnit.Text = "%" Then Report_BarChart.btnUnit.Text = "h"

            If Report_BarChart.chkConsolidate.Checked = True Then Report_BarChart.chkConsolidate.Checked = False

            If Dashboard.Visible = True Then Dashboard.Close()
            If Report_PieChart.Visible = True Then Report_PieChart.Close()
            If Report_BarChart.Visible = True Or Report_PieChart.Visible = True Then BTN_Create.Text = "Update"

            If Form_Shift.Visible = True Then Form_Shift.Close()

            Dim periode_ As New periode

            periode_.shift1 = New Dictionary(Of String, Double)
            periode_.shift2 = New Dictionary(Of String, Double)
            periode_.shift3 = New Dictionary(Of String, Double)

            Report_BarChart.consolidated = False

            If DateDiff(DateInterval.Day, dateStart, dateEnd) > 28 Then

                days = 6

                periode_returned = CSI_LIB.Detailled(dateStart, dateEnd, read_tree(), True)
                big_periode_returned = CSI_LIB.Evolution(dateStart, dateEnd, read_tree(), days, True)

                Call Fill_Combo_Report_BarChart(periode_returned)

                Report_BarChart.chartBarHistory.ChartAreas(0).AxisX.ScaleView.Zoom((89 - Math.Round(DateDiff(DateInterval.Day, dateStart, dateEnd) / 7) - 1) + 0.5, 88)
            End If

            If (DateDiff(DateInterval.Day, dateStart, dateEnd) < 30) Then

                days = 1

                periode_returned = CSI_LIB.Detailled(dateStart, dateEnd, read_tree(), True)
                big_periode_returned = CSI_LIB.Evolution(dateStart, dateEnd, read_tree(), days, True)

                Call Fill_Combo_Report_BarChart(periode_returned)

            End If


            'CHART ZOOM :----------------------------------------------------------------------------------------------

            ' 1Day
            If DateDiff(DateInterval.Day, dateStart, dateEnd) = 0 Then

                Report_BarChart.chartBarHistory.ChartAreas(0).AxisX.ScaleView.Zoom(82.5, 89.5)

            End If

            ' +1Day AND < 28 DAYS
            If DateDiff(DateInterval.Day, dateStart, dateEnd) < 30 And DateDiff(DateInterval.Day, dateStart, dateEnd) <> 0 Then
                Report_BarChart.chartBarHistory.ChartAreas(0).AxisX.ScaleView.Zoom((89 - DateDiff(DateInterval.Day, dateStart, dateEnd) - 0.5), 89.5)
            End If

            ' +28 DAYS
            If DateDiff(DateInterval.Day, dateStart, dateEnd) > 29 Then
                Report_BarChart.chartBarHistory.ChartAreas(0).AxisX.ScaleView.Zoom((89 - Math.Round(DateDiff(DateInterval.Day, dateStart, dateEnd) / 7) - 1) + 0.5, 89.5)
            End If

            '   End If
            Report_BarChart.cBoxReports.SelectedIndex = Report_BarChart.cBoxReports.Items.Count - 1

            If Welcome.CSIF_version = 1 Then

                'Chart1.Palette 
                Report_BarChart.chartBarHistory.Series("Series3").Color = Color.Transparent
                Report_BarChart.chartBarHistory.Series("Series4").Color = Color.Transparent

            End If

        Catch ex As Exception
            Log.Error(ex)
        End Try

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
    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DTP_StartDate.ValueChanged
        DTP_EndDate.MinDate = DTP_StartDate.Value
        DTP_EndDate.Value = DTP_StartDate.Value

        If SplitContainer1.Panel2Collapsed = False Then Call TextBox1_Click(Nothing, Nothing)
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' DateTimePicker2    
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DTP_EndDate.ValueChanged
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

    Private Sub BTN_Day_Click(sender As Object, e As EventArgs) Handles BTN_Dayly.Click
        DTP_EndDate.Value = DTP_StartDate.Value
        BTN_Dayly.BackColor = Color.DeepSkyBlue
        BTN_Weekly.BackColor = Color.Transparent
        BTN_Monthly.BackColor = Color.Transparent
        BTN_Yearly.BackColor = Color.Transparent
    End Sub

    Private Sub BTN_Day_paint(sender As Object, e As PaintEventArgs) Handles BTN_Dayly.Paint
        Using p As New Pen(Color.Gray, 2.0)
            e.Graphics.DrawLine(p, New System.Drawing.Point(BTN_Dayly.Location.X, BTN_Dayly.Location.Y), New System.Drawing.Point(BTN_Dayly.Location.X, BTN_Dayly.Location.Y + BTN_Dayly.Height))
        End Using
    End Sub

    Private Sub BTN_Week_Click(sender As Object, e As EventArgs) Handles BTN_Weekly.Click
        DTP_StartDate.Value = DTP_StartDate.Value.AddDays(Val(Reporting_application.week_(0)) - DTP_StartDate.Value.DayOfWeek)
        If Reporting_application.week_(1) < Reporting_application.week_(0) Then ' in case of the index of the end of the week is smaller than the start of the week
            DTP_EndDate.Value = DTP_StartDate.Value.AddDays(7 + Val(Reporting_application.week_(1)) - DTP_StartDate.Value.DayOfWeek)
        Else
            DTP_EndDate.Value = DTP_StartDate.Value.AddDays(Val(Reporting_application.week_(1)) - DTP_StartDate.Value.DayOfWeek)
        End If

        BTN_Dayly.BackColor = Color.Transparent
        BTN_Weekly.BackColor = Color.DeepSkyBlue
        BTN_Monthly.BackColor = Color.Transparent
        BTN_Yearly.BackColor = Color.Transparent
    End Sub

    Private Sub BTN_Month_Click(sender As Object, e As EventArgs) Handles BTN_Monthly.Click
        DTP_StartDate.Value = New Date(DTP_StartDate.Value.Year, DTP_StartDate.Value.Month, 1)
        DTP_EndDate.Value = New Date(DTP_StartDate.Value.Year, DTP_StartDate.Value.Month, System.DateTime.DaysInMonth(DTP_StartDate.Value.Year, DTP_StartDate.Value.Month))

        BTN_Dayly.BackColor = Color.Transparent
        BTN_Weekly.BackColor = Color.Transparent
        BTN_Monthly.BackColor = Color.DeepSkyBlue
        BTN_Yearly.BackColor = Color.Transparent
    End Sub

    Private Sub BTN_Year_Click(sender As Object, e As EventArgs) Handles BTN_Yearly.Click

        If Reporting_application.year_ = -1 Then
            Reporting_application.year_ = 1
        End If

        DTP_StartDate.Value = New Date(DTP_StartDate.Value.Year, Val(Reporting_application.year_) + 1, 1)
        DTP_EndDate.Value = DTP_StartDate.Value.AddYears(1)
        DTP_EndDate.Value = DTP_EndDate.Value.AddDays(-1)

        BTN_Dayly.BackColor = Color.Transparent
        BTN_Weekly.BackColor = Color.Transparent
        BTN_Monthly.BackColor = Color.Transparent
        BTN_Yearly.BackColor = Color.DeepSkyBlue
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
        Report_PieChart.BTN_Return.Visible = False
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

    Public Sub fill_Report_BarChart_shift(ByRef periode_returned As periode(), i As Integer)
        Dim calc As Double = 0, total As Double = 0, verif As Double = 0
        Dim top4 As New Dictionary(Of String, Integer)

        'Shift1 percentage :
        For Each key In periode_returned(i).shift1.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift1.Item(key)
        Next

        If (total <> 0) Then

            If Report_BarChart.btnUnit.Text = "h" Then

                If periode_returned(i).shift1.ContainsKey("CYCLE ON") Then
                    Report_BarChart.LBL_CycleOn_Shift1.Text = (periode_returned(i).shift1.Item("CYCLE ON") * 100 / total).ToString("00.00")
                    verif = periode_returned(i).shift1.Item("CYCLE ON")
                Else
                    Report_BarChart.LBL_CycleOn_Shift1.Text = ("00.00")
                End If

                If periode_returned(i).shift1.ContainsKey("CYCLE OFF") Then

                    Report_BarChart.LBL_CycleOff_Shift1.Text = (periode_returned(i).shift1.Item("CYCLE OFF") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift1.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                    verif = verif + periode_returned(i).shift1.Item("CYCLE OFF")
                Else
                    Report_BarChart.LBL_CycleOff_Shift1.Text = ("00.00")
                End If

                If periode_returned(i).shift1.ContainsKey("SETUP") Then
                    Report_BarChart.LBL_Setup_Shift1.Text = (periode_returned(i).shift1.Item("SETUP") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift1.Item("SETUP") * 100 / total).ToString("00.00")
                    verif = verif + periode_returned(i).shift1.Item("SETUP")
                Else
                    Report_BarChart.LBL_Setup_Shift1.Text = "00.00"
                End If

            Else

                If periode_returned(i).shift1.ContainsKey("CYCLE ON") Then

                    Report_BarChart.LBL_CycleOn_Shift1.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE ON"))
                    verif = periode_returned(i).shift1.Item("CYCLE ON")
                Else
                    Report_BarChart.LBL_CycleOn_Shift1.Text = ("00:00")
                End If

                If periode_returned(i).shift1.ContainsKey("CYCLE OFF") Then

                    Report_BarChart.LBL_CycleOff_Shift1.Text = uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE OFF"))
                    verif = verif + periode_returned(i).shift1.Item("CYCLE OFF")
                Else
                    Report_BarChart.LBL_CycleOff_Shift1.Text = ("00:00")
                End If

                If periode_returned(i).shift1.ContainsKey("SETUP") Then
                    Report_BarChart.LBL_Setup_Shift1.Text = uptimeToDHMS(periode_returned(i).shift1.Item("SETUP"))
                    verif = verif + periode_returned(i).shift1.Item("SETUP")
                Else
                    Report_BarChart.LBL_Setup_Shift1.Text = "00:00"
                End If

            End If
            If Welcome.CSIF_version = 1 Then Report_BarChart.LBL_Setup_Shift1.Text = ""
            For Each key In (periode_returned(i).shift1.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
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

            If Report_BarChart.btnUnit.Text = "h" Then
                Report_BarChart.LBL_Other_Shift1.Text = (calc * 100 / total).ToString("00.00") 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
                Report_BarChart.LBL_Total_Shift1.Text = (verif * 100 / total).ToString("00.00") 'Math.Round(verif * 100 / total).ToString("00.00")
            Else
                Report_BarChart.LBL_Other_Shift1.Text = uptimeToDHMS(calc)
                Report_BarChart.LBL_Total_Shift1.Text = uptimeToDHMS(verif)
            End If

        Else
            Report_BarChart.LBL_CycleOn_Shift1.Text = ("00.00")
            Report_BarChart.LBL_CycleOff_Shift1.Text = ("00.00")
            Report_BarChart.LBL_Setup_Shift1.Text = "00.00"
            Report_BarChart.LBL_Other_Shift1.Text = "00.00"
            Report_BarChart.LBL_Total_Shift1.Text = "00.00"
        End If
        If Welcome.CSIF_version = 1 Then
            Report_BarChart.LBL_Setup_Shift1.Text = ""
            Report_BarChart.LBL_Other_Shift1.Text = ""
            If total = 0 Then
                Report_BarChart.LBL_CycleOff_Shift1.Text = ("00.00")
            Else
                '  Form9.Label45.Text = Math.Round((periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift1.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift1.Item("CYCLE OFF")
                    If (periode_returned(i).shift1.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift1.Item("SETUP")
                    End If
                    totaloff += calc

                    Report_BarChart.LBL_CycleOff_Shift1.Text = ((totaloff) * 100 / total).ToString("00.00") 'Math.Round((periode_returned(i).shift1.Item("CYCLE OFF") + periode_returned(i).shift1.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Report_BarChart.LBL_CycleOff_Shift1.Text = ((calc) * 100 / total).ToString("00.00") 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If


        total = 0
        verif = 0
        calc = 0

        'Shift2 percentage :
        For Each key In periode_returned(i).shift2.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift2.Item(key)
        Next
        If (total <> 0) Then
            If Report_BarChart.btnUnit.Text = "h" Then

                If periode_returned(i).shift2.ContainsKey("CYCLE ON") Then
                    Report_BarChart.LBL_CycleOn_Shift2.Text = (periode_returned(i).shift2.Item("CYCLE ON") * 100 / total).ToString("00.00")
                    verif = periode_returned(i).shift2.Item("CYCLE ON")
                Else
                    Report_BarChart.LBL_CycleOn_Shift2.Text = ("00.00")
                End If

                If periode_returned(i).shift2.ContainsKey("CYCLE OFF") Then
                    Report_BarChart.LBL_CycleOff_Shift2.Text = (periode_returned(i).shift2.Item("CYCLE OFF") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift2.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                    verif = verif + periode_returned(i).shift2.Item("CYCLE OFF")
                Else
                    Report_BarChart.LBL_CycleOff_Shift2.Text = ("00.00")
                End If

                If periode_returned(i).shift2.ContainsKey("SETUP") Then
                    Report_BarChart.LBL_Setup_Shift2.Text = (periode_returned(i).shift2.Item("SETUP") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift2.Item("SETUP") * 100 / total).ToString("00.00")
                    verif = verif + periode_returned(i).shift2.Item("SETUP")
                Else
                    Report_BarChart.LBL_Setup_Shift2.Text = "00.00"
                End If
            Else
                If periode_returned(i).shift2.ContainsKey("CYCLE ON") Then

                    Report_BarChart.LBL_CycleOn_Shift2.Text = uptimeToDHMS(periode_returned(i).shift2.Item("CYCLE ON"))
                    verif = periode_returned(i).shift2.Item("CYCLE ON")
                Else
                    Report_BarChart.LBL_CycleOn_Shift2.Text = ("00:00")
                End If

                If periode_returned(i).shift2.ContainsKey("CYCLE OFF") Then

                    Report_BarChart.LBL_CycleOff_Shift2.Text = uptimeToDHMS(periode_returned(i).shift2.Item("CYCLE OFF"))
                    verif = verif + periode_returned(i).shift2.Item("CYCLE OFF")
                Else
                    Report_BarChart.LBL_CycleOff_Shift2.Text = ("00:00")
                End If

                If periode_returned(i).shift2.ContainsKey("SETUP") Then
                    Report_BarChart.LBL_Setup_Shift2.Text = uptimeToDHMS(periode_returned(i).shift2.Item("SETUP"))
                    verif = verif + periode_returned(i).shift2.Item("SETUP")
                Else
                    Report_BarChart.LBL_Setup_Shift2.Text = "00:00"
                End If

            End If


            If Welcome.CSIF_version = 1 Then
                Report_BarChart.LBL_Setup_Shift2.Text = ""
            End If

            For Each key In (periode_returned(i).shift2.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
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

            If Report_BarChart.btnUnit.Text = "h" Then
                Report_BarChart.LBL_Other_Shift2.Text = (calc * 100 / total).ToString("00.00") 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
                Report_BarChart.LBL_Total_Shift2.Text = (verif * 100 / total).ToString("00.00") 'Math.Round(verif * 100 / total).ToString("00.00")
            Else
                Report_BarChart.LBL_Other_Shift2.Text = uptimeToDHMS(calc)
                Report_BarChart.LBL_Total_Shift2.Text = uptimeToDHMS(verif)
            End If

        Else
            Report_BarChart.LBL_CycleOn_Shift2.Text = ("00.00")
            Report_BarChart.LBL_CycleOff_Shift2.Text = ("00.00")
            Report_BarChart.LBL_Setup_Shift2.Text = "00.00"
            Report_BarChart.LBL_Other_Shift2.Text = "00.00"
            Report_BarChart.LBL_Total_Shift2.Text = "00.00"
        End If
        If Welcome.CSIF_version = 1 Then
            Report_BarChart.LBL_Setup_Shift2.Text = ""
            Report_BarChart.LBL_Other_Shift2.Text = ""
            If total = 0 Then
                Report_BarChart.LBL_CycleOff_Shift2.Text = ("00.00")
            Else
                '  Form9.Label45.Text = Math.Round((periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift2.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift2.Item("CYCLE OFF")
                    If (periode_returned(i).shift2.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift2.Item("SETUP")
                    End If
                    totaloff += calc

                    Report_BarChart.LBL_CycleOff_Shift2.Text = ((totaloff) * 100 / total).ToString("00.00")
                    '= ((periode_returned(i).shift2.Item("CYCLE OFF") + periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00") 'Math.Round((periode_returned(i).shift2.Item("CYCLE OFF") + periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Report_BarChart.LBL_CycleOff_Shift2.Text = ((calc) * 100 / total).ToString("00.00") 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If

        total = 0
        verif = 0
        calc = 0


        'Shift3 percentage :
        For Each key In periode_returned(i).shift3.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift3.Item(key)
        Next
        If (total <> 0) Then
            If Report_BarChart.btnUnit.Text = "h" Then

                If periode_returned(i).shift3.ContainsKey("CYCLE ON") Then
                    Report_BarChart.LBL_CycleOn_Shift3.Text = (periode_returned(i).shift3.Item("CYCLE ON") * 100 / total).ToString("00.00")
                    verif = periode_returned(i).shift3.Item("CYCLE ON")
                Else
                    Report_BarChart.LBL_CycleOn_Shift3.Text = ("00.00")
                End If

                If periode_returned(i).shift3.ContainsKey("CYCLE OFF") Then
                    Report_BarChart.LBL_CycleOff_Shift3.Text = (periode_returned(i).shift3.Item("CYCLE OFF") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift3.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                    verif = verif + periode_returned(i).shift3.Item("CYCLE OFF")
                Else
                    Report_BarChart.LBL_CycleOff_Shift3.Text = ("00.00")
                End If

                If periode_returned(i).shift3.ContainsKey("SETUP") Then
                    Report_BarChart.LBL_Setup_Shift3.Text = (periode_returned(i).shift3.Item("SETUP") * 100 / total).ToString("00.00") 'Math.Round(periode_returned(i).shift3.Item("SETUP") * 100 / total).ToString("00.00")
                    verif = verif + periode_returned(i).shift3.Item("SETUP")
                Else
                    Report_BarChart.LBL_Setup_Shift3.Text = "00.00"
                End If
            Else
                If periode_returned(i).shift3.ContainsKey("CYCLE ON") Then

                    Report_BarChart.LBL_CycleOn_Shift3.Text = uptimeToDHMS(periode_returned(i).shift3.Item("CYCLE ON"))
                    verif = periode_returned(i).shift3.Item("CYCLE ON")
                Else
                    Report_BarChart.LBL_CycleOn_Shift3.Text = ("00:00")
                End If

                If periode_returned(i).shift3.ContainsKey("CYCLE OFF") Then

                    Report_BarChart.LBL_CycleOff_Shift3.Text = uptimeToDHMS(periode_returned(i).shift3.Item("CYCLE OFF"))
                    verif = verif + periode_returned(i).shift3.Item("CYCLE OFF")
                Else
                    Report_BarChart.LBL_CycleOff_Shift3.Text = ("00:00")
                End If

                If periode_returned(i).shift3.ContainsKey("SETUP") Then
                    Report_BarChart.LBL_Setup_Shift3.Text = uptimeToDHMS(periode_returned(i).shift3.Item("SETUP"))
                    verif = verif + periode_returned(i).shift3.Item("SETUP")
                Else
                    Report_BarChart.LBL_Setup_Shift3.Text = "00:00"
                End If
            End If

            If Welcome.CSIF_version = 1 Then Report_BarChart.LBL_Setup_Shift3.Text = ""
            For Each key In (periode_returned(i).shift3.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
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

            If Report_BarChart.btnUnit.Text = "h" Then
                Report_BarChart.LBL_Other_Shift3.Text = (calc * 100 / total).ToString("00.00") 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
                Report_BarChart.LBL_Total_Shift3.Text = (verif * 100 / total).ToString("00.00") 'Math.Round(verif * 100 / total).ToString("00.00")
            Else
                Report_BarChart.LBL_Other_Shift3.Text = uptimeToDHMS(calc)
                Report_BarChart.LBL_Total_Shift3.Text = uptimeToDHMS(verif)
            End If

        Else

            Report_BarChart.LBL_CycleOn_Shift3.Text = ("00.00")
            Report_BarChart.LBL_CycleOff_Shift3.Text = ("00.00")
            Report_BarChart.LBL_Setup_Shift3.Text = "00.00"
            Report_BarChart.LBL_Other_Shift3.Text = "00.00"
            Report_BarChart.LBL_Total_Shift3.Text = "00.00"
        End If
        Dim k As Integer = 0


        If Welcome.CSIF_version = 1 Then
            Report_BarChart.LBL_Setup_Shift3.Text = ""
            Report_BarChart.LBL_Other_Shift3.Text = ""
            If total = 0 Then
                Report_BarChart.LBL_Other_Shift3.Text = ("00.00")
            Else
                If (periode_returned(i).shift3.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift3.Item("CYCLE OFF")
                    If (periode_returned(i).shift3.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift3.Item("SETUP")
                    End If
                    totaloff += calc

                    Report_BarChart.LBL_CycleOff_Shift3.Text = ((totaloff) * 100 / total).ToString("00.00")
                    'Form9.Label62.Text = ((periode_returned(i).shift3.Item("CYCLE OFF") + periode_returned(i).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00") 'Math.Round((periode_returned(i).shift3.Item("CYCLE OFF") + periode_returned(i).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    Report_BarChart.LBL_CycleOff_Shift3.Text = ((calc) * 100 / total).ToString("00.00") 'Math.Round((calc) * 100 / total).ToString("00.00")
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
        Dim wordLenght As Integer = 20

        For Each key In sortedDictionary
            If key.Key.Length > wordLenght Then
                keyname = key.Key.Substring(0, wordLenght)
            Else
                keyname = key.Key
            End If
            status = status & vbCrLf & keyname
            tempdouble = key.Value / subtotal
            tempdouble = tempdouble * 100
            percent = percent & vbCrLf & ": " & uptimeToDHMS(key.Value) & " , " & tempdouble.ToString("00.00") & " %" '(Math.Round(key.Value * 100 / subtotal)).ToString() & " %"
            k = k + 1
            If k = 4 Then Exit For
        Next

        Report_BarChart.Label5.Text = status
        Report_BarChart.Label5.TextAlign = ContentAlignment.TopCenter

        Report_BarChart.Label6.Text = percent

    End Sub


    Public Sub fill_Report_BarChart(ByRef periode_returned As periode(), ThirdDim As Integer)


        '1period
        Report_BarChart.LBL_CycleOn_Period.Text = (Val(periode_returned(ThirdDim))).ToString("00.00")
        Report_BarChart.LBL_CycleOff_Period.Text = (Val(periode_returned(ThirdDim))).ToString("00.00")
        Report_BarChart.LBL_Setup_Period.Text = (Val(periode_returned(ThirdDim))).ToString("00.00")
        Report_BarChart.LBL_Other_Period.Text = (Val(periode_returned(ThirdDim))).ToString("00.00")
        Report_BarChart.LBL_Total_Period.Text = (Val(periode_returned(ThirdDim))).ToString("00.00")

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
        'Try
        Dim index As Integer

        For i = 0 To (UBound(periode_) - 1)
            If periode_.Length <= machineList.Length Then
                Report_PieChart.CB_Report.Items.Add(CSI_LIB.RealNameMachine(periode_(i).machine_name) & " : " & periode_(i).date_)
            Else
                Report_PieChart.CB_Report.Items.Add(CSI_LIB.RealNameMachine(machineList(i)) & " : " & periode_(i).date_)
            End If
        Next

        If ((UBound(periode_) - 1)) = 0 Then
            index = 0
        Else
            index = Report_BarChart.cBoxReports.SelectedIndex
        End If
        Report_PieChart.CB_Report.SelectedIndex = index
        ' Call fill_form_7(periode_(index))

        Report_PieChart.SuspendLayout()
        Report_PieChart.Show()
    End Sub
    Public barchart_washere As Boolean = False
    Public piechart_washere As Boolean = False
    Public Sub Fill_Combobox_partnumber(ByRef periode_ As periode())
        Dim i As Integer
        'Try

        For i = 0 To (UBound(periode_) - 1)
            If periode_.Length <= machineList.Length Then
                Report_PartNumber.CB_Report.Items.Add(CSI_LIB.RealNameMachine(periode_(i).machine_name) & " : " & periode_(i).date_)
            Else
                Report_PartNumber.CB_Report.Items.Add(CSI_LIB.RealNameMachine(machineList(i)) & " : " & periode_(i).date_)
            End If
        Next
        Dim index As Integer = Report_BarChart.cBoxReports.SelectedIndex
        Report_PartNumber.CB_Report.SelectedIndex = i - 1
        Call fill_form_7(periode_(i - 1))

        Report_PartNumber.SuspendLayout()


        Report_PartNumber.Show()
    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' Call fill_form7  
    '  Use general_array , and fill the form7 labels
    '-----------------------------------------------------------------------------------------------------------------------
    Public Piechart_data1() As Integer
    Public Piechart_data2() As Integer
    Public Piechart_data3() As Integer
    Private Sub Superarray_add(which As Integer, data As Integer)
        If which = 1 Then
            If Piechart_data1 Is Nothing Then
                ReDim Piechart_data1(0)
            Else
                ReDim Preserve Piechart_data1(Piechart_data1.Count)
            End If
            Piechart_data1(Piechart_data1.Count - 1) = data
        ElseIf which = 2 Then
            If Piechart_data2 Is Nothing Then
                ReDim Piechart_data2(0)
            Else
                ReDim Preserve Piechart_data2(Piechart_data2.Count)
            End If
            Piechart_data2(Piechart_data2.Count - 1) = data
        Else
            If Piechart_data3 Is Nothing Then
                ReDim Piechart_data3(0)
            Else
                ReDim Preserve Piechart_data3(Piechart_data3.Count)
            End If
            Piechart_data3(Piechart_data3.Count - 1) = data
        End If
    End Sub

    Private Sub pie_chart_add_point(shift As Integer, value As Integer, name As String, ByRef colors As Dictionary(Of String, Integer), cycletime As String)
        Dim backcolor As Color

        If colors.ContainsKey(name) Then
            backcolor = System.Drawing.ColorTranslator.FromWin32(colors(name))
        Else
            backcolor = rnd_color()
            colors.Add(name, System.Drawing.ColorTranslator.ToWin32(backcolor))
        End If
        backcolor = Color.FromArgb(255, backcolor.R, backcolor.G, backcolor.B)

        If shift = 1 Then
            Report_PieChart.Chart1.Series("Series1").Points.AddY(value)
            Report_PieChart.Chart1.Series("Series1").Points.Item(Report_PieChart.Chart1.Series("Series1").Points.Count - 1).Color = backcolor
            Report_PieChart.Chart1.Series("Series1").Points.Item(Report_PieChart.Chart1.Series("Series1").Points.Count - 1).ToolTip = name + " : " + cycletime



        ElseIf shift = 2 Then
            Report_PieChart.Chart2.Series("Series1").Points.AddY(value)
            Report_PieChart.Chart2.Series("Series1").Points.Item(Report_PieChart.Chart2.Series("Series1").Points.Count - 1).Color = backcolor
            Report_PieChart.Chart2.Series("Series1").Points.Item(Report_PieChart.Chart2.Series("Series1").Points.Count - 1).ToolTip = name + " : " + cycletime

        Else
            Report_PieChart.Chart3.Series("Series1").Points.AddY(value)
            Report_PieChart.Chart3.Series("Series1").Points.Item(Report_PieChart.Chart3.Series("Series1").Points.Count - 1).Color = backcolor
            Report_PieChart.Chart3.Series("Series1").Points.Item(Report_PieChart.Chart3.Series("Series1").Points.Count - 1).ToolTip = name + " : " + cycletime

        End If

    End Sub

    Private Function rnd_color() As System.Drawing.Color


        Dim MyAlpha As Integer
        Dim MyRed As Integer
        Dim MyGreen As Integer
        Dim MyBlue As Integer
        ' Initialize the random-number generator.
        Randomize()
        ' Generate random value between 1 and 6.
        MyAlpha = CInt(Int((254 * Rnd()) + 0))
        ' Initialize the random-number generator.
        Randomize()
        ' Generate random value between 1 and 6.
        MyRed = CInt(Int((254 * Rnd()) + 0))
        ' Initialize the random-number generator.
        Randomize()
        ' Generate random value between 1 and 6.
        MyGreen = CInt(Int((254 * Rnd()) + 0))
        ' Initialize the random-number generator.
        Randomize()
        ' Generate random value between 1 and 6.
        MyBlue = CInt(Int((254 * Rnd()) + 0))

        Return Color.FromArgb(MyAlpha, MyRed, MyGreen, MyBlue)
    End Function




    Public Sub fill_form_7(ByRef periode_ As periode)
        Dim cycleTime As Double = 0
        Piechart_data1 = Nothing
        Piechart_data2 = Nothing
        Piechart_data3 = Nothing
        Report_PieChart.Chart1.Series("Series1").Points.Clear()
        Report_PieChart.Chart2.Series("Series1").Points.Clear()
        Report_PieChart.Chart3.Series("Series1").Points.Clear()

        Dim colors As Dictionary(Of String, Integer)
        'colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)
        colors = CSIFLEXSettings.StatusColors

        'Clear the GridView
        Report_PieChart.DataGridView1.Rows.Clear()
        Report_PieChart.DataGridView2.Rows.Clear()
        Report_PieChart.DataGridView3.Rows.Clear()
        Report_PieChart.ListBox1.Items.Clear()
        Report_PieChart.ListBox2.Items.Clear()
        Report_PieChart.ListBox3.Items.Clear()



        Dim j As Integer = 0
        Dim total1 As Double = 0, total2 As Double = 0, total As Double = 0
        Dim verif As Integer = 0

        'Shift1 percentage :
        For Each key In periode_.shift1.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_.shift1.Item(key)
        Next


        '' Fill Form7 , shift 1   
        If periode_.shift1.ContainsKey("CYCLE ON") And (total <> 0) Then
            cycleTime = periode_.shift1.Item("CYCLE ON")

            verif = cycleTime + verif
            pie_chart_add_point(1, cycleTime * 100 / total, "CYCLE ON", colors, uptimeToDHMS(cycleTime))
            Report_PieChart.DataGridView1.Rows.Add("CYCLE ON", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            Superarray_add(1, 0)

            pie_chart_add_point(1, 0, "CYCLE ON", colors, "00:00")
            Report_PieChart.DataGridView1.Rows.Add("CYCLE ON", "00:00", "00")
        End If

        If periode_.shift1.ContainsKey("CYCLE OFF") And (total <> 0) Then
            cycleTime = periode_.shift1.Item("CYCLE OFF")

            verif = cycleTime + verif

            pie_chart_add_point(1, cycleTime * 100 / total, "CYCLE OFF", colors, uptimeToDHMS(cycleTime))
            Report_PieChart.DataGridView1.Rows.Add("CYCLE OFF", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else


            pie_chart_add_point(1, 0, "CYCLE OFF", colors, "00:00")
            Report_PieChart.DataGridView1.Rows.Add("CYCLE OFF", "00:00", "00")
        End If

        If periode_.shift1.ContainsKey("SETUP") And (total <> 0) Then
            cycleTime = periode_.shift1.Item("SETUP")
            Superarray_add(1, cycleTime * 100 / total)
            verif = cycleTime + verif
            pie_chart_add_point(1, cycleTime * 100 / total, "SETUP", colors, uptimeToDHMS(cycleTime))

            Report_PieChart.DataGridView1.Rows.Add("SETUP", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            Superarray_add(1, 0)

            pie_chart_add_point(1, 0, "SETUP", colors, "00:00")
            Report_PieChart.DataGridView1.Rows.Add("SETUP", "00:00", "00")
        End If

        Dim backcolor_others As Color


        'Other status in shift1
        For Each key In periode_.shift1.Keys
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" Then
                If key.Contains("_PARTN") Then
                    Report_PieChart.ListBox1.Items.Add(key.Remove(0, 8))
                Else
                    cycleTime = periode_.shift1.Item(key)

                    pie_chart_add_point(1, cycleTime * 100 / total, key, colors, uptimeToDHMS(cycleTime))
                    verif = cycleTime + verif
                    If total <> 0 Then
                        Report_PieChart.DataGridView1.Rows.Add(key, uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))

                        backcolor_others = System.Drawing.ColorTranslator.FromWin32(colors(key))
                        backcolor_others = Color.FromArgb(255, backcolor_others.R, backcolor_others.G, backcolor_others.B)

                        Report_PieChart.DataGridView1.Rows(Report_PieChart.DataGridView1.Rows.Count - 1).DefaultCellStyle.BackColor = backcolor_others
                        Report_PieChart.DataGridView1.Rows(Report_PieChart.DataGridView1.Rows.Count - 1).DefaultCellStyle.ForeColor = Contraste_colors(backcolor_others)

                    End If

                End If
            End If
        Next

        'TOTAL
        If total <> 0 Then

            Report_PieChart.DataGridView1.Rows.Add("TOTAL", uptimeToDHMS(total), ((verif / total) * 100)) 'Math.Round((verif / total) * 100))
        Else
            Report_PieChart.DataGridView1.Rows.Add("TOTAL", "00:00", "00")
        End If

        Dim alpha As Integer = 255

        Try


            Dim backcolor As Color
            Dim backcolor2 As Color
            Dim backcolor3 As Color




            backcolor = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE ON"))
            backcolor = Color.FromArgb(alpha, backcolor.R, backcolor.G, backcolor.B)

            backcolor2 = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE OFF"))
            backcolor2 = Color.FromArgb(alpha, backcolor2.R, backcolor2.G, backcolor2.B)

            backcolor3 = System.Drawing.ColorTranslator.FromWin32(colors("SETUP"))
            backcolor3 = Color.FromArgb(alpha, backcolor3.R, backcolor3.G, backcolor3.B)

            'CON
            Report_PieChart.DataGridView1.Rows(0).DefaultCellStyle.BackColor = backcolor
            Report_PieChart.DataGridView1.Rows(0).DefaultCellStyle.ForeColor = Report_BarChart.getContrastColor(backcolor)
            '_COFF
            Report_PieChart.DataGridView1.Rows(1).DefaultCellStyle.BackColor = backcolor2
            Report_PieChart.DataGridView1.Rows(1).DefaultCellStyle.ForeColor = Report_BarChart.getContrastColor(backcolor2)
            'SETUP
            Report_PieChart.DataGridView1.Rows(2).DefaultCellStyle.BackColor = backcolor3
            Report_PieChart.DataGridView1.Rows(2).DefaultCellStyle.ForeColor = Report_BarChart.getContrastColor(backcolor3)
        Catch ex As Exception
            Report_PieChart.DataGridView1.Rows(0).DefaultCellStyle.BackColor = Color.Green
            Report_PieChart.DataGridView1.Rows(0).DefaultCellStyle.ForeColor = Color.White
            '_COFF
            Report_PieChart.DataGridView1.Rows(1).DefaultCellStyle.BackColor = Color.Red
            Report_PieChart.DataGridView1.Rows(1).DefaultCellStyle.ForeColor = Color.White
            'SETUP
            Report_PieChart.DataGridView1.Rows(2).DefaultCellStyle.BackColor = Color.Blue
            Report_PieChart.DataGridView1.Rows(2).DefaultCellStyle.ForeColor = Color.White
        End Try

        'OTHER



        'Report_PieChart.DataGridView1.Rows(Report_PieChart.DataGridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.White
        'Report_PieChart.DataGridView1.Rows(Report_PieChart.DataGridView1.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Black




        'Shift2 percentage :
        total = 0

        For Each key In periode_.shift2.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_.shift2.Item(key)
        Next



        verif = 0

        '' Form7 , shift 2  
        If periode_.shift2.ContainsKey("CYCLE ON") And (total <> 0) Then
            cycleTime = periode_.shift2.Item("CYCLE ON")
            pie_chart_add_point(2, cycleTime * 100 / total, "CYCLE ON", colors, uptimeToDHMS(cycleTime))

            verif = cycleTime + verif
            Report_PieChart.DataGridView2.Rows.Add("CYCLE ON", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            pie_chart_add_point(2, 0, "CYCLE ON", colors, "00:00")
            Report_PieChart.DataGridView2.Rows.Add("CYCLE ON", "00:00", "00")
        End If

        If periode_.shift2.ContainsKey("CYCLE OFF") And (total <> 0) Then
            cycleTime = periode_.shift2.Item("CYCLE OFF")
            pie_chart_add_point(2, cycleTime * 100 / total, "CYCLE OFF", colors, uptimeToDHMS(cycleTime))
            verif = cycleTime + verif

            Report_PieChart.DataGridView2.Rows.Add("CYCLE OFF", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else

            pie_chart_add_point(2, 0, "CYCLE OFF", colors, "00:00")
            Report_PieChart.DataGridView2.Rows.Add("CYCLE OFF", "00:00", "00")
        End If

        If periode_.shift2.ContainsKey("SETUP") And (total <> 0) Then
            cycleTime = periode_.shift2.Item("SETUP")
            pie_chart_add_point(2, cycleTime * 100 / total, "SETUP", colors, uptimeToDHMS(cycleTime))
            verif = cycleTime + verif
            'Report_PieChart.Label37.Text = cycleTime * 100 / total
            Report_PieChart.DataGridView2.Rows.Add("SETUP", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else

            pie_chart_add_point(2, 0, "SETUP", colors, "00:00")
            Report_PieChart.DataGridView2.Rows.Add("SETUP", "00:00", "00")
        End If

        For Each key In periode_.shift2.Keys
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" Then
                If key.Contains("_PARTN") Then
                    Report_PieChart.ListBox2.Items.Add(key.Remove(0, 8))
                Else
                    cycleTime = periode_.shift2.Item(key)
                    verif = cycleTime + verif
                    pie_chart_add_point(2, cycleTime * 100 / total, key, colors, uptimeToDHMS(cycleTime))
                    If total <> 0 Then
                        Report_PieChart.DataGridView2.Rows.Add(key, uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))

                        backcolor_others = System.Drawing.ColorTranslator.FromWin32(colors(key))
                        backcolor_others = Color.FromArgb(255, backcolor_others.R, backcolor_others.G, backcolor_others.B)

                        Report_PieChart.DataGridView2.Rows(Report_PieChart.DataGridView2.Rows.Count - 1).DefaultCellStyle.BackColor = backcolor_others
                        Report_PieChart.DataGridView2.Rows(Report_PieChart.DataGridView2.Rows.Count - 1).DefaultCellStyle.ForeColor = Contraste_colors(backcolor_others)
                    End If
                    ' Report_PieChart.Label36.Text = cycleTime + Val(Report_PieChart.Label36.Text)
                End If
            End If

        Next

        'TOTAL
        If total <> 0 Then
            Report_PieChart.DataGridView2.Rows.Add("TOTAL", uptimeToDHMS(total), ((verif / total) * 100)) 'Math.Round((verif / total) * 100))
        Else
            Report_PieChart.DataGridView2.Rows.Add("TOTAL", "00:00", "00")
        End If

        Try


            Dim backcolor As Color
            Dim backcolor2 As Color
            Dim backcolor3 As Color

            ' Dim colors As Dictionary(Of String, Integer)
            'colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)
            colors = CSIFLEXSettings.StatusColors


            backcolor = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE ON"))
            backcolor = Color.FromArgb(alpha, backcolor.R, backcolor.G, backcolor.B)

            backcolor2 = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE OFF"))
            backcolor2 = Color.FromArgb(alpha, backcolor2.R, backcolor2.G, backcolor2.B)

            backcolor3 = System.Drawing.ColorTranslator.FromWin32(colors("SETUP"))
            backcolor3 = Color.FromArgb(alpha, backcolor3.R, backcolor3.G, backcolor3.B)
            'CON
            Report_PieChart.DataGridView2.Rows(0).DefaultCellStyle.BackColor = backcolor
            Report_PieChart.DataGridView2.Rows(0).DefaultCellStyle.ForeColor = Report_BarChart.getContrastColor(backcolor)
            '_COFF
            Report_PieChart.DataGridView2.Rows(1).DefaultCellStyle.BackColor = backcolor2
            Report_PieChart.DataGridView2.Rows(1).DefaultCellStyle.ForeColor = Report_BarChart.getContrastColor(backcolor2)
            'SETUP
            Report_PieChart.DataGridView2.Rows(2).DefaultCellStyle.BackColor = backcolor3
            Report_PieChart.DataGridView2.Rows(2).DefaultCellStyle.ForeColor = Report_BarChart.getContrastColor(backcolor3)

        Catch
            'CON
            Report_PieChart.DataGridView2.Rows(0).DefaultCellStyle.BackColor = Color.Green
            Report_PieChart.DataGridView2.Rows(0).DefaultCellStyle.ForeColor = Color.White
            '_COFF
            Report_PieChart.DataGridView2.Rows(1).DefaultCellStyle.BackColor = Color.Red
            Report_PieChart.DataGridView2.Rows(1).DefaultCellStyle.ForeColor = Color.White
            'SETUP
            Report_PieChart.DataGridView2.Rows(2).DefaultCellStyle.BackColor = Color.Blue
            Report_PieChart.DataGridView2.Rows(2).DefaultCellStyle.ForeColor = Color.White
        End Try



        'Report_PieChart.DataGridView2.Rows(Report_PieChart.DataGridView2.Rows.Count - 1).DefaultCellStyle.BackColor = Color.White
        'Report_PieChart.DataGridView2.Rows(Report_PieChart.DataGridView2.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Black





        total = 0
        'Shift3 percentage :
        For Each key In periode_.shift3.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_.shift3.Item(key)
        Next

        verif = 0

        '' Form7 , shift 3   
        If periode_.shift3.ContainsKey("CYCLE ON") And (total <> 0) Then
            cycleTime = periode_.shift3.Item("CYCLE ON")
            verif = cycleTime + verif
            pie_chart_add_point(3, cycleTime * 100 / total, "CYCLE ON", colors, uptimeToDHMS(cycleTime))
            Report_PieChart.DataGridView3.Rows.Add("CYCLE ON", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            pie_chart_add_point(3, 0, "CYCLE ON", colors, "00:00")
            Report_PieChart.DataGridView3.Rows.Add("CYCLE ON", "00:00", "00")
        End If

        If periode_.shift3.ContainsKey("CYCLE OFF") And (total <> 0) Then
            cycleTime = periode_.shift3.Item("CYCLE OFF")

            verif = cycleTime + verif
            pie_chart_add_point(3, cycleTime * 100 / total, "CYCLE OFF", colors, uptimeToDHMS(cycleTime))
            Report_PieChart.DataGridView3.Rows.Add("CYCLE OFF", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            pie_chart_add_point(3, 0, "CYCLE OFF", colors, "00:00")
            Report_PieChart.DataGridView3.Rows.Add("CYCLE OFF", "00:00", "00")
        End If

        If periode_.shift3.ContainsKey("SETUP") And (total <> 0) Then
            cycleTime = periode_.shift3.Item("SETUP")
            pie_chart_add_point(3, cycleTime * 100 / total, "SETUP", colors, uptimeToDHMS(cycleTime))
            verif = cycleTime + verif

            Report_PieChart.DataGridView3.Rows.Add("SETUP", uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
        Else
            pie_chart_add_point(3, 0, "SETUP", colors, "00:00")
            Report_PieChart.DataGridView3.Rows.Add("SETUP", "00:00", "00")
        End If

        For Each key In periode_.shift3.Keys
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" Then
                If key.Contains("_PARTN") Then
                    Report_PieChart.ListBox3.Items.Add(key.Remove(0, 8))
                Else
                    cycleTime = periode_.shift3.Item(key)
                    pie_chart_add_point(3, cycleTime * 100 / total, key, colors, uptimeToDHMS(cycleTime))
                    verif = cycleTime + verif
                    If total <> 0 Then
                        Report_PieChart.DataGridView3.Rows.Add(key, uptimeToDHMS(cycleTime), Math.Round((cycleTime * 100 / total), 2)) 'Math.Round(cycleTime * 100 / total))
                        backcolor_others = System.Drawing.ColorTranslator.FromWin32(colors(key))
                        backcolor_others = Color.FromArgb(255, backcolor_others.R, backcolor_others.G, backcolor_others.B)

                        Report_PieChart.DataGridView3.Rows(Report_PieChart.DataGridView3.Rows.Count - 1).DefaultCellStyle.BackColor = backcolor_others
                        Report_PieChart.DataGridView3.Rows(Report_PieChart.DataGridView3.Rows.Count - 1).DefaultCellStyle.ForeColor = Contraste_colors(backcolor_others)

                    End If


                End If
            End If

        Next


        'TOTAL
        If total <> 0 Then
            Report_PieChart.DataGridView3.Rows.Add("TOTAL", uptimeToDHMS(total), (verif / total * 100)) 'Math.Round(verif * 100 / total))
        Else
            Report_PieChart.DataGridView3.Rows.Add("TOTAL", "00:00", "00")
        End If



        Try


            Dim backcolor As Color
            Dim backcolor2 As Color
            Dim backcolor3 As Color

            '  Dim colors As Dictionary(Of String, Integer)
            'colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)
            colors = CSIFLEXSettings.StatusColors


            backcolor = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE ON"))
            backcolor = Color.FromArgb(alpha, backcolor.R, backcolor.G, backcolor.B)

            backcolor2 = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE OFF"))
            backcolor2 = Color.FromArgb(alpha, backcolor2.R, backcolor2.G, backcolor2.B)

            backcolor3 = System.Drawing.ColorTranslator.FromWin32(colors("SETUP"))
            backcolor3 = Color.FromArgb(alpha, backcolor3.R, backcolor3.G, backcolor3.B)

            'CON
            Report_PieChart.DataGridView3.Rows(0).DefaultCellStyle.BackColor = backcolor
            Report_PieChart.DataGridView3.Rows(0).DefaultCellStyle.ForeColor = Report_BarChart.getContrastColor(backcolor)
            '_COFF
            Report_PieChart.DataGridView3.Rows(1).DefaultCellStyle.BackColor = backcolor2
            Report_PieChart.DataGridView3.Rows(1).DefaultCellStyle.ForeColor = Report_BarChart.getContrastColor(backcolor2)
            'SETUP
            Report_PieChart.DataGridView3.Rows(2).DefaultCellStyle.BackColor = backcolor3
            Report_PieChart.DataGridView3.Rows(2).DefaultCellStyle.ForeColor = Report_BarChart.getContrastColor(backcolor3)

        Catch
            'CON
            Report_PieChart.DataGridView3.Rows(0).DefaultCellStyle.BackColor = Color.Green
            Report_PieChart.DataGridView3.Rows(0).DefaultCellStyle.ForeColor = Color.White
            '_COFF
            Report_PieChart.DataGridView3.Rows(1).DefaultCellStyle.BackColor = Color.Red
            Report_PieChart.DataGridView3.Rows(1).DefaultCellStyle.ForeColor = Color.White
            'SETUP
            Report_PieChart.DataGridView3.Rows(2).DefaultCellStyle.BackColor = Color.Blue
            Report_PieChart.DataGridView3.Rows(2).DefaultCellStyle.ForeColor = Color.White

        End Try


        'OTHER
        'For j = 0 To Report_PieChart.DataGridView3.Rows.Count - 4

        '    Report_PieChart.DataGridView3.Rows(j + 3).DefaultCellStyle.BackColor = Color.Yellow
        '    Report_PieChart.DataGridView3.Rows(j + 3).DefaultCellStyle.ForeColor = Color.Black
        'Next j

        'Report_PieChart.DataGridView3.Rows(Report_PieChart.DataGridView3.Rows.Count - 1).DefaultCellStyle.BackColor = Color.White
        'Report_PieChart.DataGridView3.Rows(Report_PieChart.DataGridView3.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Black


        'Form7.DataGridView1.Rows(Form7.DataGridView1.RowCount - 1).Selected = True
        'Form7.DataGridView2.Rows(Form7.DataGridView2.RowCount - 1).Selected = True
        'Form7.DataGridView3.Rows(Form7.DataGridView3.RowCount - 1).Selected = True

        Report_PieChart.DataGridView1.CurrentCell = Nothing
        Report_PieChart.DataGridView1.Columns.Item(2).SortMode = DataGridViewColumnSortMode.NotSortable

        Report_PieChart.DataGridView2.CurrentCell = Nothing
        Report_PieChart.DataGridView2.Columns.Item(2).SortMode = DataGridViewColumnSortMode.NotSortable
        Report_PieChart.DataGridView3.CurrentCell = Nothing
        Report_PieChart.DataGridView3.Columns.Item(2).SortMode = DataGridViewColumnSortMode.NotSortable

    End Sub

    Public Function Contraste_colors(back_color As Color) As Color

        Dim rdif As Integer, gdif As Integer, bdif As Integer

        If (back_color.R > 100 And back_color.R < 150) Then
            rdif = 150
        Else
            rdif = 255
        End If

        If (back_color.G > 100 And back_color.G < 150) Then
            gdif = 150
        Else
            gdif = 255
        End If

        If (back_color.B > 100 And back_color.B < 150) Then
            bdif = 150
        Else
            bdif = 255
        End If

        Return Color.FromArgb(rdif - back_color.R, gdif - back_color.G, bdif - back_color.B)
    End Function


    '-----------------------------------------------------------------------------------------------------------------------
    ' fill combo Report_BarChart
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Public Sub Fill_Combo_Report_BarChart(ByRef period_return As periode())
        Dim i As Integer
        'Try
        If Not IsNothing(period_return) Then
            '  Dim machine As String()
            machineList = read_tree()
            Report_BarChart.cBoxReports.Items.Clear()

            For i = 0 To (UBound(period_return) - 1)
                If period_return.Length <= machineList.Length Then
                    Report_BarChart.cBoxReports.Items.Add(period_return(i).machine_name & " : " & period_return(i).date_)
                Else
                    Report_BarChart.cBoxReports.Items.Add(machineList(i) & " : " & period_return(i).date_)
                End If
            Next
            Report_BarChart.Show()
        End If


    End Sub

#End Region


    '-----------------------------------------------------------------------------------------------------------------------
    ' MOVE FORME 3
    '  
    '-----------------------------------------------------------------------------------------------------------------------
#Region "Config_report move"
    Dim _MouseDown As Boolean, _MouseX As Integer, _MouseY As Integer
    Private Sub Config_report_mouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        _MouseDown = True
        _MouseX = e.X
        _MouseY = e.Y
    End Sub

    Private Sub Config_report_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        _MouseDown = False
    End Sub


    Private Sub Config_report_Mousemove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove


        If _MouseDown = True Then
            Reporting_application.SuspendLayout()
            Me.Left = Me.Left + (e.X - _MouseX)
            Me.Top = Me.Top + (e.Y - _MouseY)
            If Me.Top < 20 Then Me.Top = 0
            If Me.Left < 20 Then Me.Left = 0
            Reporting_application.ResumeLayout(True)

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
    Private Sub BTN_PartNb_Click(sender As Object, e As EventArgs) Handles BTN_PartNb.Click

        ' 334, 477'Form14.Show()
        If BTN_PartNb.Text = "View Part Numbers" Then 'Panel closed
            SplitContainer1.Panel2Collapsed = False
            BTN_PartNb.Text = "Hide Part Numbers"
            filtered_table = Nothing
            If SplitContainer1.Panel2Collapsed = False Then Call LoadPartNumbers(read_tree())
        Else
            SplitContainer1.Panel2Collapsed = True
            BTN_PartNb.Text = "View Part Numbers"
            filtered_table = Nothing
            '    If SplitContainer1.Panel2Collapsed = False Then Call LoadPartNumbers(read_tree())
        End If

    End Sub



    '-----------------------------------------------------------------------------------------------------------------------
    ' LOAD ALL THE PART NUMBERS for the selected machines (w/o filtering the dates)
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub LoadPartNumbers(machine As String())

        Dim CSILIB As New CSI_Library.CSI_Library(False)

        CSI_LIB.connectionString = CSIFLEXSettings.Instance.ConnectionString

        If UBound(machine) < 0 Then
            DGV_PartsNumber.DataSource = Nothing
            DGV_PartsNumber.Refresh()

            GoTo End_
        End If

        '*************************************************************************************************************************************************'
        '**** DB Connection
        '*************************************************************************************************************************************************'
        'Dim db_authPath As String = Nothing
        'If (File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
        '    Using streader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")
        '        db_authPath = streader.ReadLine()
        '    End Using
        'End If

        'Dim cnt_SQL As MySqlConnection = New MySqlConnection("SERVER=" + db_authPath + ";DATABASE=csi_database;user=client;password=csiflex123;port=3306;")
        'Dim cnt_sqlite As SQLiteConnection = New SQLiteConnection(CSI_Library.CSI_Library.sqlitedbpath)

        'If Welcome.CSIF_version = 3 Then

        '    cnt_SQL.Open()
        '    Try
        '        If cnt_SQL.State = 1 Then
        '        Else
        '            MessageBox.Show("Connection to the database failed")
        '            GoTo End_
        '        End If
        '    Catch ex As Exception
        '        MessageBox.Show(" Unable to establish the connection to the database : " & ex.Message, vbCritical + vbSystemModal)
        '        GoTo End_
        '    End Try
        'Else

        '    cnt_sqlite.Open()
        '    Try

        '        If cnt_sqlite.State = 1 Then
        '        Else
        '            MessageBox.Show("Connection to the database failed")
        '            GoTo End_
        '        End If
        '    Catch ex As Exception
        '        MessageBox.Show(" Unable to establish the connection to the database : " & ex.Message, vbCritical + vbSystemModal)
        '        GoTo End_
        '    End Try
        'End If




        '*************************************************************************************************************************************************'
        '**** DB Connection -END
        '*************************************************************************************************************************************************'


        Dim adapter As New SQLiteDataAdapter
        Dim reader As SQLiteDataReader


        'Dim table_ As New DataTable("tmp_table")
        Dim table_all As New DataTable("tmp_table")
        Dim tbl_temp As New DataTable("tbl_temp")
        Dim tbl_temp2 As New DataTable("tbl_temp2")

        Dim tmp_table_cmd As New SQLiteCommand
        Dim command As String = ""
        Dim machinename As String = ""

        Try
            If (machine.Count) > 0 Then
                Dim i As Integer
                For i = 0 To UBound(machine)
                    tbl_temp2.Reset()
                    machine(i) = CSILIB.RenameMachine(machine(i))
                    machinename = machine(i)

                    If Welcome.CSIF_version = 3 Then

                        command = "SELECT  DISTINCT  substring(status,9), date(Date_),'" + CSILIB.RealNameMachine(machine(i)) + "' as machineName FROM  csi_database.tbl_" & machine(i) & " WHERE status LIKE '_PARTNO:%'"

                        tbl_temp2 = MySqlAccess.GetDataTable(command, CSIFLEXSettings.Instance.ConnectionString)

                    Else

                        'command = "SELECT DISTINCT  substr( status,9), Date_ ,'" + CSILIB.RealNameMachine(machine(i)) + "' as machineName FROM  tbl_" & machine(i) & " WHERE status LIKE '_PARTNO:%'   "

                        'tmp_table_cmd.CommandText = command
                        'tmp_table_cmd.Connection = cnt_sqlite

                        'If cnt_sqlite.State = 1 Then
                        '    reader = tmp_table_cmd.ExecuteReader
                        '    tbl_temp2.Load(reader)
                        '    reader.Close()

                        'Else
                        '    MessageBox.Show("Connection to the database failed")
                        '    GoTo End_
                        'End If

                    End If



                    If Not (tbl_temp2.Rows.Count = 0) Then
                        table_all.Merge(tbl_temp2)
                    End If

                Next i 'machine 
            End If

            'If Welcome.CSIF_version = 3 Then
            '    'cnt_SQL.Close()
            'Else
            '    cnt_sqlite.Close()
            'End If


            Try

                'table_ = table_all

                'table_all.Columns.Add("Date", System.Type.GetType("System.DateTime"))
                table_all.Columns(0).ColumnName = "Parts"
                table_all.Columns(1).ColumnName = "Date"
                table_all.Columns(2).ColumnName = "Machine"
                Dim cultures As CultureInfo = New CultureInfo("us-US")
                'For Each row As DataRow In table_all.Rows
                '    row.Item(0) = row.Item(0).ToString().Remove(0, 8)
                '    If row.Item(3) >= System.DateTime.DaysInMonth((row.Item(1)), (row.Item(2))) Then
                '    Else
                '        row.Item(5) = Convert.ToDateTime(row.Item(3).ToString() & "-" & row.Item(2) & "-" & (row.Item(1)), cultures) 'row.Item(3).ToString() & " " & CSILIB.mois_(Val(row.Item(2))) & " " & row.Item(1).ToString()
                '    End If
                'Next

                'table_all.Columns.Remove("Day_")
                'table_all.Columns.Remove("Month_")
                'table_all.Columns.Remove("Year_")
                table_all.Columns("Machine").SetOrdinal(2)
                table_all.Columns("Parts").SetOrdinal(0)
                table_all.Columns("Date").SetOrdinal(1)
                'table_all.Columns(0).ColumnName = "machine"

                '   table_all = table_all.AsEnumerable().Distinct(DataRowComparer.Default)


                filtered_table = table_all
                filtered_table.Locale = cultures

                DGV_PartsNumber.DataSource = table_all
                DGV_PartsNumber.Columns("Date").ValueType = GetType(Date)
                DGV_PartsNumber.Columns("Date").DefaultCellStyle.Format = "dd MMMM yyyy"


                Call TextBox1_Click(Nothing, Nothing)
            Catch ex As Exception
                DGV_PartsNumber.DataSource = Nothing
                DGV_PartsNumber.Refresh()


            End Try
        Catch ex As SQLiteException
            'Dim cmd As New SQLiteCommand("CREATE TABLE if not exists tbl_" & machinename & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, UNIQUE (time_,status))", cnt_sqlite)
            'cmd.ExecuteNonQuery()
        End Try
End_:

    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TB_PartsNumber.KeyUp
        If SplitContainer1.Panel2Collapsed = False Then

            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            ' Change culture to en-US.
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

            Dim date1 As Date = DTP_StartDate.Value.Date
            Dim date2 As Date = DTP_EndDate.Value.Date

            ' Dim filter As String = "Date >= #" & date1.ToString("dd MMMM yyyy") & "# and Date <= #" & date2.ToString("dd MMMM yyyy") & "#"
            Dim filter As String = "Date >= #" & date1 & "# and Date <= #" & date2.AddMinutes(1439) & "#"

            If Not (filtered_table Is Nothing) Then

                Dim tmp_table_rows As System.Data.DataRow() = filtered_table.Select(filter)


                If (tmp_table_rows.Count) > 0 Then
                    Dim tmp_table_ As System.Data.DataTable = tmp_table_rows.CopyToDataTable

                    Dim view As New DataView(tmp_table_)
                    view.RowFilter = ("Parts like '" + TB_PartsNumber.Text & "*'")
                    DGV_PartsNumber.DataSource = view
                    DGV_PartsNumber.Columns("Date").DefaultCellStyle.Format = "dd MMMM yyyy"
                Else
                    Dim view As New DataView(filtered_table)
                    view.RowFilter = ("Parts like '" + TB_PartsNumber.Text & "*'")


                    DGV_PartsNumber.DataSource = Nothing



                End If

            End If

            DGV_PartsNumber.Refresh()

            Thread.CurrentThread.CurrentCulture = originalCulture

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
        '    If groupename___ = "" Then groupename___ = "Groupe" & TreeView1.SelectedNode.Nodes.Count.ToString()
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
    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs)
        'If e.Button = Windows.Forms.MouseButtons.Right Then
        '    TreeView1.SelectedNode = e.Node
        '    selected_node_1 = e.Node
        '    ContextMenuADD.Items.Item(1).Visible = True
        '    ContextMenuADD.Items.Item(2).Visible = True
        'End If
    End Sub



    Private Sub Rename_treenode_treeview1(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Dim oldlabel As String = TreeView_machine.SelectedNode.Text

        If Not (IsNothing(TreeView_machine.SelectedNode)) Then TreeView_machine.SelectedNode.BeginEdit()
        TreeView_machine.SelectedNode.Name = oldlabel
        'saveLastTreeV()
    End Sub

    Private Sub Delete_treenode_treeview1(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click

        Dim tn As TreeNode = selected_node_1
        If tn IsNot Nothing Then
            Dim iRet As Integer
            iRet = MessageBox.Show("Are you certain you want to delete " & tn.Text & "?",
                MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2)
            If iRet = vbYes Then
                TreeView_machine.Nodes.Remove(selected_node_1)
            End If
        End If
        saveLastTreeV()
    End Sub


    Private Sub saveLastTreeV()

        Dim serializer As TreeViewSerializer = New TreeViewSerializer()


        serializer.SerializeTreeView(Me.TreeView_machine, rootPath & "\sys\" & Reporting_application.username_ & "_TreeView.xml")


    End Sub

    Private Sub loadLastTreeV()
        Me.TreeView_machine.Nodes.Clear()
        Dim serializer As TreeViewSerializer = New TreeViewSerializer()

        If Not My.Computer.FileSystem.FileExists(CSI_LIB.eNET_path + "\_SETUP\MonList.sys") Then
            MessageBox.Show("The file 'Monlist.sys' is missing")
        Else
            Dim i As Integer
            Dim file As System.IO.StreamReader
            Dim machine() As String ' File -> read line -> machine()

            i = 0
            file = My.Computer.FileSystem.OpenTextFileReader(CSI_LIB.eNET_path + "\_SETUP\MonList.sys")

            While Not file.EndOfStream
                ReDim Preserve machine(i + 1)
                machine(i) = file.ReadLine()
                i = i + 1
            End While

            serializer.DeserializeTreeView(Me.TreeView_machine, rootPath & "\sys\" & Reporting_application.username_ & "_TreeView.xml")
            checkForMachineSelected()
        End If




    End Sub

    Private Sub loadStartupConfigTreeV()
        'treeview
        Me.TreeView_machine.Nodes.Clear()
        Dim serializer As TreeViewSerializer = New TreeViewSerializer()

        If Not My.Computer.FileSystem.FileExists(CSI_LIB.eNET_path + "\_SETUP\MonList.sys") Then
            MessageBox.Show("The file 'Monlist.sys' is missing")
        Else
            Dim i As Integer
            Dim file As System.IO.StreamReader
            Dim machine() As String ' File -> read line -> machine()

            i = 0
            file = My.Computer.FileSystem.OpenTextFileReader(CSI_LIB.eNET_path + "\_SETUP\MonList.sys")

            While Not file.EndOfStream
                ReDim Preserve machine(i + 1)
                machine(i) = file.ReadLine()
                i = i + 1
            End While

            serializer.DeserializeTreeView(Me.TreeView_machine, rootPath & "\sys\StartupConfig_TreeView.xml")
            checkForMachineSelected()
        End If
    End Sub

    Private Sub CompareTreeView(MonList As String(), startupFileName As String)
        Dim reader As XmlTextReader
        Dim parentNode As TreeNode
        Dim XmlNodeTag As String

        XmlNodeTag = "node"
        reader = New XmlTextReader(startupFileName)

        While reader.Read()
            If reader.Name = XmlNodeTag Then

                Dim attributeCount As Integer = reader.AttributeCount

                If attributeCount > 0 Then

                    '  reader.MoveToAttribute(0)
                    ' SetAttributeValue(newNode, reader.Name, reader.Value)

                End If

            End If
        End While

    End Sub

    Private Sub checkForMachineSelected()
        Dim machines As String() = read_tree()
        If UBound(machines) > -1 Then

            BTN_Create.Enabled = True
            If Welcome.CSIF_version <> 1 Then BTN_PartNb.Enabled = True

            '  TextBox1.Enabled = True
            '   DataGridView1.Enabled = True

            'Commented by Drausio
            'If Welcome.CSIF_version <> 1 Then BTN_Dashboard.Enabled = True
        Else
            'BTN_Dashboard.Enabled = False

            'Commented by Drausio
            'If Welcome.CSIF_version <> 1 Then BTN_Dashboard.Enabled = True
            BTN_Create.Enabled = False
            BTN_PartNb.Enabled = False
            TB_PartsNumber.Clear()

        End If
    End Sub

    'Moved to Reporting_application form
    'Private Sub loadStartupParams()
    '    'report dates
    '    Try
    '        If My.Computer.FileSystem.FileExists(rootPath & "\sys\StartupConfig_.csys") Then
    '            Using r As StreamReader = New StreamReader(rootPath & "\sys\StartupConfig_.csys", False)
    '                'Dim lib___ As New CSI_Library.CSI_Library
    '                Dim tmp As String() = Split(r.ReadLine, ";")
    '                r.Close()
    '                If (tmp(0).Contains("Report")) Then
    '                    Dim nudval As String = tmp(1).Substring(tmp(1).IndexOf("=") + 1)
    '                    Dim daystoreport As Integer = Int32.Parse(nudval)
    '                    Dim enddate As Date = DTP_EndDate.Value
    '                    DTP_StartDate.Value = DTP_StartDate.Value.AddDays(-daystoreport)
    '                    DTP_EndDate.Value = enddate

    '                    BTN_Create.PerformClick()
    '                Else
    '                    BTN_Dashboard.PerformClick()
    '                End If
    '            End Using
    '        End If
    '    Catch ex As Exception
    '        CSI_LIB.LogClientError("startup load:" & ex.Message)
    '    End Try
    'End Sub





    Private Sub TimerDrawTree_Tick(sender As Object, e As EventArgs) Handles TimerDrawTree.Tick


        If Not (TimerDrawTree.Interval = GlobalVariables.refresh_Interval) Then
            TimerDrawTree.Interval = GlobalVariables.refresh_Interval
        End If

        If (GlobalVariables.ServerIsListening = True) Then
            font_tree()
        End If

    End Sub

    Dim piechart_was_active As Boolean = False

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles BTN_ALL_partno.Click
        RemoveHandler TreeView_machine.AfterCheck, AddressOf TreeView1_AfterCheck
        RemoveHandler TB_PartsNumber.KeyUp, AddressOf TextBox1_Click

        For Each child As TreeNode In TreeView_machine.Nodes
            CheckChildNodes(child, True)
        Next

        Try

            DTP_StartDate.Value = Reporting_application.First_date
            DTP_EndDate.Value = Now.Date

            AddHandler TB_PartsNumber.KeyUp, AddressOf TextBox1_Click
            AddHandler TreeView_machine.AfterCheck, AddressOf TreeView1_AfterCheck

            Call TextBox1_Click(Nothing, Nothing)

        Catch ex As Exception
            MsgBox("Can't Go More Further because of : " & vbCrLf & ex.Message)
        End Try

    End Sub

    Sub CheckChildNodes(ByVal parent As TreeNode, checked As Boolean)
        For Each child As TreeNode In parent.Nodes
            child.Checked = checked
            If child.Nodes.Count > 0 Then CheckChildNodes(child, checked)
        Next
    End Sub

    Public cell_ As DataGridViewCell
    Public row_index As Integer
    Public column_index As Integer

    Private Sub dgvpartno_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DGV_PartsNumber.MouseUp
        Try

            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim hti As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
                If hti.Type = DataGridViewHitTestType.Cell Then
                    If Not DGV_PartsNumber.Rows(hti.RowIndex).Selected Then
                        ' User right clicked a row that is not selected, so throw away all other selections and select this row
                        DGV_PartsNumber.ClearSelection()
                        DGV_PartsNumber.CurrentCell = DGV_PartsNumber(hti.ColumnIndex, hti.RowIndex)
                        'DGV_PartsNumber.Rows(hti.RowIndex).Selected = True
                    End If

                    If hti.ColumnIndex = 0 Then
                        CMS_partsdetails.Items(0).Text = "Part details : " + DGV_PartsNumber.Rows(hti.RowIndex).Cells(hti.ColumnIndex).Value.ToString()
                        row_index = hti.RowIndex
                        column_index = hti.ColumnIndex

                    ElseIf hti.ColumnIndex = 1 Then
                        CMS_partsdetails.Items(0).Text = "View parts made on : " + DateTime.Parse(DGV_PartsNumber.Rows(hti.RowIndex).Cells(hti.ColumnIndex).Value.ToString()).Date
                        row_index = hti.RowIndex
                        column_index = hti.ColumnIndex
                    Else
                        CMS_partsdetails.Items(0).Text = "Parts made on the machine : " + DGV_PartsNumber.Rows(hti.RowIndex).Cells(hti.ColumnIndex).Value.ToString()
                        row_index = hti.RowIndex
                        column_index = hti.ColumnIndex
                    End If
                End If

            Else


            End If
        Catch ex As Exception
            CSI_LIB.LogClientError("part filter err: " & ex.Message)
        End Try
    End Sub


    Public Part_details_ds As New DataSet
    Public Selected_parts As String
    Public Selected_parts_date As Date
    Public Selected_machine_partrep As String

    Private Sub partdetail_Click(sender As Object, e As EventArgs) Handles partdetail.Click
        Report_parts.SuspendLayout()

        Try
            If CMS_partsdetails.Items(0).Text.StartsWith("View parts made on") Then
                filter_by_date()
            ElseIf CMS_partsdetails.Items(0).Text.StartsWith("Parts made on") Then
                filter_by_machine()
            Else
                Selected_machine_partrep = DGV_PartsNumber.Rows(row_index).Cells(DGV_PartsNumber.Columns("Machine").Index).Value
                Selected_parts = DGV_PartsNumber.Rows(row_index).Cells(DGV_PartsNumber.Columns("Parts").Index).Value
                Selected_parts_date = DGV_PartsNumber.Rows(row_index).Cells(DGV_PartsNumber.Columns("Date").Index).Value

                'If Report_PieChart.Visible = True Then
                '    Report_PieChart.Visible = False
                '    piechart_washere = True
                'End If
                'If Report_BarChart.Visible = True Then
                '    Report_BarChart.Visible = False
                '    barchart_washere = True
                'End If

                If Reporting_application.SRV_Version = 0 And Welcome.CSIF_version = 3 Then
                    MsgBox("Yo have to update the CSIFlex server to use this feature", Modal)

                Else
                    If Welcome.CSIF_version = 1 Then
                        MsgBox("This feature is not available in CSIFlex lite")
                    Else
                        'Report_parts.Visible = False
                        'Report_parts.Show()
                        Not_available.ShowDialog()
                    End If
                End If
            End If

        Catch ex As Exception
            CSI_LIB.LogClientError("part filter err:" & ex.Message)
        End Try

    End Sub

    Private Sub filter_by_date()

        'Dim date_ As Date = DateTime.Parse(DGV_PartsNumber.Rows(row_index).Cells(column_index).Value.ToString()).Date

        Dim date_ As Date = DGV_PartsNumber.Rows(row_index).Cells(column_index).Value

        Dim filter As String = "Date >= #" & date_ & "# and Date <= #" & date_ & "#"
        If Not (filtered_table Is Nothing) Then
            filtered_table.Locale = New CultureInfo("en-US")
            Dim tmp_table_rows As System.Data.DataRow() = filtered_table.Select(filter)

            If tmp_table_rows.Count <> 0 Then DGV_PartsNumber.DataSource = tmp_table_rows.CopyToDataTable
        End If

    End Sub

    Private Sub filter_by_machine()

        Dim machine As String = DGV_PartsNumber.Rows(row_index).Cells(column_index).Value.ToString()

        Dim filter As String = "Machine like '" & machine & "'"
        If Not (filtered_table Is Nothing) Then
            Dim tmp_table_rows As System.Data.DataRow() = filtered_table.Select(filter)
            If tmp_table_rows.Count <> 0 Then DGV_PartsNumber.DataSource = tmp_table_rows.CopyToDataTable
        End If

    End Sub

    Private Sub BTN_next_date_click(sender As Object, e As EventArgs) Handles BTN_next_date.Click
        Try
            If DTP_StartDate.Value.Date = DTP_EndDate.Value.Date Then
                DTP_StartDate.Value = DTP_StartDate.Value.AddDays(1)
            ElseIf DTP_StartDate.Value.Date.Day = 1 And DTP_EndDate.Value.Date.Day = Date.DaysInMonth(DTP_StartDate.Value.Date.Year, DTP_StartDate.Value.Date.Month) Then
                DTP_StartDate.Value = DTP_StartDate.Value.AddMonths(1)
                BTN_Monthly.PerformClick()
            ElseIf DTP_StartDate.Value.Day = 1 And DTP_StartDate.Value.Month = 1 And DTP_EndDate.Value.Month = 12 And DTP_EndDate.Value.Day = Date.DaysInMonth(DTP_EndDate.Value.Year, 12) Then
                DTP_StartDate.Value.AddYears(1)
                DTP_EndDate.Value.AddYears(1)
            Else
                Dim diff As Integer = DateDiff(DateInterval.Day, DTP_StartDate.Value.Date, DTP_EndDate.Value.Date)
                DTP_StartDate.Value = DTP_EndDate.Value.Date.AddDays(1)
                DTP_EndDate.Value = DTP_StartDate.Value.AddDays(diff)
            End If
        Catch ex As Exception
            CSI_LIB.LogClientError("next per err:" & ex.Message)
        End Try
    End Sub

    Private Sub Previous_date_Click(sender As Object, e As EventArgs) Handles BTN_Previous_date.Click
        Try
            If DTP_StartDate.Value.Date = DTP_EndDate.Value.Date Then
                DTP_StartDate.Value = DTP_StartDate.Value.AddDays(-1)
            ElseIf DTP_StartDate.Value.Date.Day = 1 And DTP_EndDate.Value.Date.Day = Date.DaysInMonth(DTP_StartDate.Value.Date.Year, DTP_StartDate.Value.Date.Month) Then
                DTP_StartDate.Value = DTP_StartDate.Value.AddMonths(-1)
                BTN_Monthly.PerformClick()
            ElseIf DTP_StartDate.Value.Day = 1 And DTP_StartDate.Value.Month = 1 And DTP_EndDate.Value.Month = 12 And DTP_EndDate.Value.Day = Date.DaysInMonth(DTP_EndDate.Value.Year, 12) Then
                DTP_StartDate.Value.AddYears(-1)
                DTP_EndDate.Value.AddYears(-1)
            Else
                Dim diff As Integer = DateDiff(DateInterval.Day, DTP_StartDate.Value.Date, DTP_EndDate.Value.Date)
                DTP_StartDate.Value = DTP_EndDate.Value.AddDays(-2 * diff)
                DTP_EndDate.Value = DTP_StartDate.Value.Date.AddDays(diff)

            End If
        Catch ex As Exception
            CSI_LIB.LogClientError("prev per err:" & ex.Message)
        End Try
    End Sub

    Private Sub DGV_PartsNumber_doubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_PartsNumber.CellContentDoubleClick
        'Dim hti As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
        Try

            If Not DGV_PartsNumber.Rows(e.RowIndex).Selected Then

                DGV_PartsNumber.ClearSelection()
                DGV_PartsNumber.CurrentCell = DGV_PartsNumber(e.ColumnIndex, e.RowIndex)

            End If

            If e.ColumnIndex = 2 Then
                'CMS_partsdetails.Items(0).Text = "Part details : " + DGV_PartsNumber.Rows(hti.RowIndex).Cells(hti.ColumnIndex).Value.ToString()
                row_index = e.RowIndex
                column_index = e.ColumnIndex
                filter_by_machine()

            ElseIf e.ColumnIndex = 1 Then
                '  CMS_partsdetails.Items(0).Text = "View parts made on : " + DateTime.Parse(DGV_PartsNumber.Rows(hti.RowIndex).Cells(hti.ColumnIndex).Value.ToString()).Date
                row_index = e.RowIndex
                column_index = e.ColumnIndex
                filter_by_date()
            ElseIf e.ColumnIndex = 0 Then
                '  CMS_partsdetails.Items(0).Text = "Parts made on the machine : " + DGV_PartsNumber.Rows(hti.RowIndex).Cells(hti.ColumnIndex).Value.ToString()
                row_index = e.RowIndex
                column_index = e.ColumnIndex

                ' Report_parts.Show()
                Not_available.ShowDialog()
            End If
        Catch ex As Exception
            CSI_LIB.LogClientError(ex.Message)
        End Try

    End Sub


    Dim barchart_was_active As Boolean = False

    Private Sub BTN_Dashboard_Click(sender As Object, e As EventArgs) Handles BTN_Dashboard.Click

        If (BTN_Dashboard.Text.Contains("Show")) Then
            'Replaced Dashboard by DashGrid, then replaced by DashWeb
            DashWeb.Close()
            DashWeb.Location = New System.Drawing.Point()

            'If Dashboard.Visible = True Then Dashboard.Close()
            If Report_PieChart.Visible = True Then
                piechart_was_active = True
                barchart_was_active = False
                '  Report_PieChart.Close()
                Report_PieChart.Hide()
            End If
            If Report_BarChart.Visible = True Then
                barchart_was_active = True
                piechart_was_active = False
                Report_BarChart.Hide()
                ' Report_BarChart.Close()
            End If

            'If Form_Shift_1.Visible = True Then Form_Shift_1.Close()
            'If Form_shift_2.Visible = True Then Form_shift_2.Close()
            'If Form_Shift_3.Visible = True Then Form_Shift_3.Close()
            If Form_Shift.Visible = True Then Form_Shift.Close()

            DashWeb.Show()
            DashWeb.SuspendLayout()
            BTN_Dashboard.Text = "Hide Live Status"
        Else

            If piechart_was_active = True Then
                Report_PieChart.Show()
            End If
            If barchart_was_active = True Then
                Report_BarChart.Show()
            End If

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

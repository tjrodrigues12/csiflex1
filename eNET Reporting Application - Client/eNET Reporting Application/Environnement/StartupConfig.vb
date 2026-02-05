Imports System.IO
Imports System.IO.Compression
Imports System.Windows
Imports System.Data.OleDb
Imports Microsoft.Reporting.WinForms
Imports System.Globalization
Imports CSI_Library
Imports System.Runtime.CompilerServices

Imports TreeViewSerialization

Public Class StartupConfig

    Private active_machines() As String
    Private TypeDePeriode As String
    Private Qry_Tbl_MachineName As String
    Private Qry_Tbl_DataMachine As String
    Public CSI_Lib As CSI_Library.CSI_Library
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath


    Private Sub SetupForm_Reporting_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim imglst As New ImageList
        CSI_Lib = New CSI_Library.CSI_Library(False)

        Dim list2 As New List(Of String)

        Dim mt_present As Boolean = False
        Dim st_present As Boolean = False

        Dim prefixTemp As String ' temp string, multiple use
        Dim i As Integer = 0
        Dim j As Integer = -1
        Dim k As Integer = -1
        Dim cpt As Integer = 0

        TreeView_machine.Nodes.Clear()

        CSI_Lib.connectionString = CSIFLEXSettings.Instance.ConnectionString

        For Each mach As String In CSIFLEXSettings.GroupMachines

            prefixTemp = Strings.Left(mach, 3)

            Dim node As New TreeNode
            Dim newNode As New TreeNode
            Dim machName As String

            If Strings.Len(mach) > 4 Then
                node.ImageKey = Strings.Right(mach, Strings.Len(mach) - 4)
                node.Text = Strings.Right(mach, Strings.Len(mach) - 4)
            Else
                node.ImageKey = mach
                node.Text = mach
            End If

            Select Case prefixTemp
                Case "_MT"
                    mt_present = True
                    st_present = False

                    list2.Add(mach)
                    TreeView_machine.Nodes.Add(node)
                    cpt = -1
                    j = j + 1
                Case "_ST"
                    st_present = True
                    cpt += 1
                    If mt_present = False Then
                        TreeView_machine.Nodes.Add(Strings.Right(mach, Strings.Len(mach) - 4), Strings.Right(mach, Strings.Len(mach) - 4))
                    Else
                        TreeView_machine.Nodes(j).Nodes.Add(Strings.Right(mach, Strings.Len(mach) - 4), Strings.Right(mach, Strings.Len(mach) - 4))
                    End If

                    k += 1

                Case Else
                    If (prefixTemp <> "_ST") And (prefixTemp <> "_MT") Then

                        'machName = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.EnetName = mach Or m.Id.ToString() = mach).MachineName
                        machName = mach

                        If st_present = False And mt_present = True Then
                            newNode = TreeView_machine.Nodes(j).Nodes.Add(machName, machName)
                            newNode.Checked = CSIFLEXSettings.Instance.StartupMachines.Contains(machName)
                            cpt += 1

                        ElseIf mt_present = False And st_present = True Then

                            newNode = TreeView_machine.Nodes(k).Nodes.Add(machName, machName)
                            newNode.Checked = CSIFLEXSettings.Instance.StartupMachines.Contains(machName)

                        ElseIf mt_present = False And st_present = False Then
                            If i = 0 Then 'create MT named "MACHINES"
                                mt_present = True
                                st_present = False

                                list2.Add(machName)
                                TreeView_machine.Nodes.Add("MACHINES")
                                cpt = -1
                                j = j + 1

                                newNode = TreeView_machine.Nodes(j).Nodes.Add(machName, machName)
                                newNode.Checked = CSIFLEXSettings.Instance.StartupMachines.Contains(machName)
                                cpt += 1
                            Else
                                TreeView_machine.Nodes.Add(machName, machName)
                            End If
                        Else

                            newNode = TreeView_machine.Nodes(j).Nodes(cpt).Nodes.Add(machName, machName)
                            newNode.Checked = CSIFLEXSettings.Instance.StartupMachines.Contains(machName)

                        End If
                    End If
            End Select
        Next

        If TreeView_machine.Nodes.Count > 0 Then TreeView_machine.Nodes(0).Expand()

        rdbMonitoring.Checked = CSIFLEXSettings.Instance.StartupDisplayType = 1
        rdbDashboard.Checked = CSIFLEXSettings.Instance.StartupDisplayType = 2
        numDaysToReport.Value = CSIFLEXSettings.Instance.StartupReportDays

    End Sub


    Private Function read_treeV() As String()

        ReDim active_machines(0)

        Dim aNode As TreeNode

        For Each aNode In TreeView_machine.Nodes
            PrintRecursive(aNode)
        Next
        ' Return checked_in_TreeView_machine
        ReDim Preserve active_machines(UBound(active_machines) - 1)
        Return active_machines

    End Function


    Private Sub PrintRecursive(ByVal n As TreeNode)
        'If n.Checked = True And n.Nodes.Count = 0 Then
        '    '   checked_in_TreeView_machine.Add(n.Text)
        '    If n.Name <> "" Then
        '        machines(UBound(machines)) = n.Name
        '        ReDim Preserve machines(UBound(machines) + 1)
        '    End If
        'End If

        'Dim aNode As TreeNode
        'For Each aNode In n.Nodes
        '    PrintRecursive(aNode)
        'Next
    End Sub


    Public Function toMonday(datee As Date) As DateTime
        Dim today As Date = datee
        Dim dayIndex As Integer = Today.DayOfWeek
        If dayIndex < DayOfWeek.Monday Then
            dayIndex += 7
        End If
        Dim dayDiff As Integer = dayIndex - DayOfWeek.Monday
        Dim monday As Date = Today.AddDays(-dayDiff)
        Return monday
    End Function

    Public Function toFirst(datee As Date) As DateTime
        Return DateAdd("m", datepart("m", datee), DateSerial(Year(datee), 1, 1))
    End Function

    Public Function IntToMonday(integ As Integer, param As DateTime) As DateTime

        Dim ret As DateTime

        ret = DateAdd("ww", integ - 1, DateSerial(Year(param), 1, 1))

        Return toMonday(ret)

    End Function

    Public Function IntToFirst(integ As Integer, param As DateTime) As DateTime

        Dim ret As DateTime = New DateTime(Year(param), integ, 1)
        Return ret

    End Function



    'Private Sub frm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    '    Me.Hide()
    '    e.Cancel = True
    'End Sub

    Private Sub TreeView_machine_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeView_machine.AfterCheck

        RemoveHandler TreeView_machine.AfterCheck, AddressOf TreeView_machine_AfterCheck

        Call CheckAllChildNodes(e.Node)

        'Dim machines As String() = read_treeV()

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

        AddHandler TreeView_machine.AfterCheck, AddressOf TreeView_machine_AfterCheck

    End Sub

    Private Sub IsEveryChildChecked(ByVal parentNode As TreeNode, ByRef checkValue As Boolean)
        For Each node As TreeNode In parentNode.Nodes
            Call IsEveryChildChecked(node, checkValue)
            If Not node.Checked Then
                checkValue = False
            End If
        Next
    End Sub

    Private Sub CheckAllChildNodes(ByVal parentNode As TreeNode)
        For Each childNode As TreeNode In parentNode.Nodes
            childNode.Checked = parentNode.Checked
            CheckAllChildNodes(childNode)
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




    Private Sub RdbDashboard_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDashboard.CheckedChanged

        If (rdbDashboard.Checked) Then
            P_ReportSettings.Enabled = True
            If TreeView_machine.Nodes.Count > 0 Then TreeView_machine.Nodes(0).Expand()
        Else
            P_ReportSettings.Enabled = False
        End If

    End Sub

    Private Sub BtnSaveConfig_Click(sender As Object, e As EventArgs) Handles btnSaveConfig.Click

        CSIFLEXSettings.Instance.StartupDisplayType = IIf(rdbDashboard.Checked, 2, 1)
        CSIFLEXSettings.Instance.StartupReportDays = numDaysToReport.Value
        CSIFLEXSettings.Instance.StartupMachines = GetSelectedMachines(TreeView_machine.Nodes(0))
        CSIFLEXSettings.Instance.SaveSettings()

        'If (rdbDashboard.Checked) Then
        '    If (numDaysToReport.Value.ToString().Length > 0) Then
        '        'saveconfig
        '        Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\StartupConfig_.csys", False)
        '            'Dim lib__ As New CSI_Library.CSI_Library

        '            writer.Write("DispType=Report;NOD=" + numDaysToReport.Value.ToString() + ";")
        '            writer.Close()
        '        End Using

        '        saveTreeV()

        '        Me.DialogResult = Forms.DialogResult.OK
        '    Else
        '        MessageBox.Show("Please specify to number of days to report.", "Configuration", MessageBoxButton.OK, MessageBoxImage.Exclamation)
        '    End If
        'Else
        '    'save config
        '    Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\StartupConfig_.csys", False)
        '        'Dim lib__ As New CSI_Library.CSI_Library

        '        writer.Write("DispType=Monitoring;NOD=" + numDaysToReport.Value.ToString() + ";")
        '        writer.Close()
        '    End Using

        '    Me.DialogResult = Forms.DialogResult.OK
        'End If

    End Sub


    Private Function GetSelectedMachines(node As TreeNode) As List(Of String)

        Dim selectedMachines = New List(Of String)()

        For Each child As TreeNode In node.Nodes

            If child.Nodes.Count > 0 Then
                Dim mchs = GetSelectedMachines(child)

                For Each mch In mchs
                    If Not selectedMachines.Contains(mch) Then selectedMachines.Add(mch)
                Next
            Else
                If child.Checked And Not selectedMachines.Contains(child.Text) Then selectedMachines.Add(child.Text)
            End If
        Next

        Return selectedMachines

    End Function


    Private Sub loadSavedParams()


    End Sub
End Class
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


        Dim file As System.IO.StreamReader
        Dim imglst As New ImageList
        CSI_Lib = New CSI_Library.CSI_Library

        Dim list2 As New List(Of String)

        Dim mt_present As Boolean = False
        Dim st_present As Boolean = False

        Dim prefixTemp As String ' temp string, multiple use

        Dim machine() As String ' File -> read line -> machine()

        TreeView_machine.Nodes.Clear()
        ' TreeView_machine = Config_report.TreeView_machine



        'If My.Computer.FileSystem.FileExists(rootPath & "\sys\StartupConfig_TreeView.xml") Then

        '    Me.TreeView_machine.Nodes.Clear()
        '    Dim serializer As TreeViewSerializer = New TreeViewSerializer()
        '    Dim tmptv As New TreeView

        '    serializer.DeserializeTreeView(tmptv, rootPath & "\sys\StartupConfig_TreeView.xml")


        '    ReDim machines(0)
        '    Dim aNode As TreeNode

        '    For Each aNode In tmptv.Nodes
        '        PrintRecursive(aNode)
        '    Next
        '    ' Return checked_in_treeview1
        '    ReDim Preserve machines(UBound(machines) - 1)




        'Else
        '    ' ???
        'End If



        If Not My.Computer.FileSystem.FileExists(rootPath & "\sys\MonList.csys") Then
            MessageBox.Show("The file 'Monlist.sys' is missing")
        Else

            file = My.Computer.FileSystem.OpenTextFileReader(rootPath & "\sys\MonList.csys")
            Dim i As Integer = 0
            Dim j As Integer = -1
            Dim k As Integer = -1
            Dim cpt As Integer = 0
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
                        TreeView_machine.Nodes.Add(node)
                        cpt = -1
                        j = j + 1
                    Case "_ST"
                        st_present = True
                        cpt += 1
                        If mt_present = False Then
                            TreeView_machine.Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
                            'TreeView_machine.Nodes.Add(node)
                        Else
                            TreeView_machine.Nodes(j).Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
                            'TreeView_machine.Nodes(j).Nodes.Add(node)
                        End If

                        k += 1

                    Case Else
                        If (prefixTemp <> "_ST") And (prefixTemp <> "_MT") Then
                            If st_present = False And mt_present = True Then
                                TreeView_machine.Nodes(j).Nodes.Add(machine(i), machine(i))

                                cpt += 1

                            ElseIf mt_present = False And st_present = True Then

                                TreeView_machine.Nodes(k).Nodes.Add(machine(i), machine(i))



                            ElseIf mt_present = False And st_present = False Then
                                If i = 0 Then 'create MT named "MACHINES"
                                    mt_present = True
                                    st_present = False

                                    list2.Add(machine(i))
                                    TreeView_machine.Nodes.Add("MACHINES")
                                    cpt = -1
                                    j = j + 1

                                    ' then add first machine as mt_present

                                    TreeView_machine.Nodes(j).Nodes.Add(machine(i), machine(i))
                                    cpt += 1
                                Else
                                    TreeView_machine.Nodes.Add(machine(i), machine(i))
                                End If
                            Else

                                TreeView_machine.Nodes(j).Nodes(cpt).Nodes.Add(machine(i), machine(i))


                            End If

                        End If
                End Select

                i = i + 1
            End While
        End If

        loadSavedParams()
        loadTreeV()




    End Sub

    Dim machines(1) As String



    Private Function read_treeV() As String()
        ' checked_in_TreeView_machine.Clear()


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
        If n.Checked = True And n.Nodes.Count = 0 Then
            '   checked_in_TreeView_machine.Add(n.Text)
            If n.Name <> "" Then
                machines(UBound(machines)) = n.Name
                ReDim Preserve machines(UBound(machines) + 1)
            End If
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursive(aNode)
        Next
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



    Private Sub frm_Closing(ByVal sender As Object, _
  ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub TreeView_machine_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeView_machine.AfterCheck
        RemoveHandler TreeView_machine.AfterCheck, AddressOf TreeView_machine_AfterCheck

        Call CheckAllChildNodes(e.Node)

        Dim machines As String() = read_treeV()


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




    Private Sub RB_Report_CheckedChanged(sender As Object, e As EventArgs) Handles RB_Report.CheckedChanged
        If (RB_Report.Checked) Then
            P_ReportSettings.Enabled = True
        Else
            P_ReportSettings.Enabled = False
        End If
    End Sub

    Private Sub BTN_SaveConfig_Click(sender As Object, e As EventArgs) Handles BTN_SaveConfig.Click
        If (RB_Report.Checked) Then
            If (NUD_ReportDays.Value.ToString().Length > 0) Then
                'saveconfig
                Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\StartupConfig_.csys", False)
                    'Dim lib__ As New CSI_Library.CSI_Library

                    writer.Write("DispType=Report;NOD=" + NUD_ReportDays.Value.ToString() + ";")
                    writer.Close()
                End Using

                saveTreeV()

                Me.DialogResult = Forms.DialogResult.OK
            Else
                MessageBox.Show("Please specify to number of days to report.", "Configuration", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            End If
        Else
            'save config
            Using writer As StreamWriter = New StreamWriter(rootPath & "\sys\StartupConfig_.csys", False)
                'Dim lib__ As New CSI_Library.CSI_Library

                writer.Write("DispType=Monitoring;NOD=" + NUD_ReportDays.Value.ToString() + ";")
                writer.Close()
            End Using

            Me.DialogResult = Forms.DialogResult.OK
        End If


    End Sub


    Private Sub saveTreeV()
        Dim serializer As TreeViewSerializer = New TreeViewSerializer()
        serializer.SerializeTreeView(Me.TreeView_machine, rootPath & "\sys\StartupConfig_TreeView.xml")
    End Sub

    Private Sub loadTreeV()
        If My.Computer.FileSystem.FileExists(rootPath & "\sys\StartupConfig_TreeView.xml") Then
            Me.TreeView_machine.Nodes.Clear()
            Dim serializer As TreeViewSerializer = New TreeViewSerializer()
            serializer.DeserializeTreeView(Me.TreeView_machine, rootPath & "\sys\StartupConfig_TreeView.xml")
        End If
    End Sub

    Private Sub loadSavedParams()

        If My.Computer.FileSystem.FileExists(rootPath & "\sys\StartupConfig_.csys") Then
            Using r As StreamReader = New StreamReader(rootPath & "\sys\StartupConfig_.csys", False)
                'Dim lib___ As New CSI_Library.CSI_Library
                Dim tmp As String() = Split(r.ReadLine, ";")
                r.Close()
                If (tmp(0).Contains("Report")) Then
                    RB_Report.Checked = True
                    Dim nudval As String = tmp(1).Substring(tmp(1).IndexOf("=") + 1)
                    NUD_ReportDays.Value = Int32.Parse(nudval)
                Else
                    RB_Monit.Checked = True
                    Dim nudval As String = tmp(1).Substring(tmp(1).IndexOf("=") + 1)
                    NUD_ReportDays.Value = Int32.Parse(nudval)
                End If
            End Using
        End If
    End Sub
End Class
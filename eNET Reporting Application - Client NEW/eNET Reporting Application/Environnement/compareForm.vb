Public Class CF



    Public form_(0) As littleform
    Public numberoflittleforms As Integer
    Public TimeLine As DataTable()
    Public shiftofeachform As Integer()
    Public CSI_LIB As New CSI_Library.CSI_Library

    Private Sub compareForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Me.MdiParent = Reporting_application
        Me.Left = 0
        Me.Top = 0
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.BackColor = Color.Transparent
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        TreeView_machine.Nodes.Clear()






        '*************************************************************************************************************************************************'
        '**** Load machines names from _SETUP\MonList.sys
        '*************************************************************************************************************************************************'
        Dim file As System.IO.StreamReader
        Dim imglst As New ImageList
        Dim i As Integer, j As Integer, k As Integer
        Dim machine As String(), strtmp As String, mt_present As Boolean, st_present As Boolean
        Dim list2 As New List(Of String)
        list2 = Config_report.list2



        If Not My.Computer.FileSystem.FileExists(Reporting_application.chemin_eNET + "\_SETUP\MonList.sys") Then
            MessageBox.Show("The file 'Monlist.sys' present in the eNET _SETUP directory is missing")
            GoTo fin
        End If

        file = My.Computer.FileSystem.OpenTextFileReader(Reporting_application.chemin_eNET + "\_SETUP\MonList.sys")
        i = 0
        j = -1
        k = -1
        While Not file.EndOfStream
            ReDim Preserve machine(i + 1)

            machine(i) = file.ReadLine()

            strtmp = Strings.Left(machine(i), 3)
            If (strtmp = "_MT") Then
                list2.Add(machine(i))
                j = j + 1
                mt_present = True
                st_present = False

                Dim node As New TreeNode

                node.ImageKey = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
                node.Text = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
                TreeView_machine.Nodes.Add(node)
                TreeView_machine.ImageList.Images.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), My.Resources.MC900442150_1_)

                'TreeView1.Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))

            End If


            If (strtmp = "_ST") Then
                st_present = True
                list2.Add(machine(i))
                Dim node As New TreeNode
                node.ImageKey = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
                node.Text = Strings.Right(machine(i), Strings.Len(machine(i)) - 4).ToString

                If mt_present = False Then
                    TreeView_machine.Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
                    TreeView_machine.Nodes.Add(node)
                Else
                    TreeView_machine.Nodes(j).Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
                    TreeView_machine.Nodes(j).Nodes.Add(node)
                End If




                TreeView_machine.ImageList.Images.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4).ToString, My.Resources.home_web_icon_300x300)
                k = k + 1
            End If

            If (strtmp <> "_ST") And (strtmp <> "_MT") Then

                If st_present = False And mt_present = True Then
                    TreeView_machine.Nodes(j).Nodes.Add(machine(i), machine(i))
                    TreeView_machine.ImageList.Images.Add(machine(i).ToString, My.Resources.thumb_SMART_430A)
                Else
                    If mt_present = False And st_present = True Then
                        TreeView_machine.Nodes(k).Nodes.Add(machine(i), machine(i))
                        TreeView_machine.ImageList.Images.Add(machine(i).ToString, My.Resources.thumb_SMART_430A)
                    Else
                        If mt_present = False And st_present = False Then
                            TreeView_machine.Nodes.Add(machine(i), machine(i))
                            TreeView_machine.ImageList.Images.Add(machine(i).ToString, My.Resources.thumb_SMART_430A)
                        Else
                            TreeView_machine.Nodes(j).Nodes(k).Nodes.Add(machine(i), machine(i))
                            TreeView_machine.ImageList.Images.Add(machine(i).ToString, My.Resources.thumb_SMART_430A)
                        End If
                    End If
                End If
            End If
            i = i + 1
        End While

        file.Close()
        '*************************************************************************************************************************************************'
        '**** Load machines names from _SETUP\MonList.sys - END
        '*************************************************************************************************************************************************'
Fin:
        Me.Refresh()


    End Sub




    Private Sub BTN_Add_Click(sender As Object, e As EventArgs) Handles BTN_Add.Click


        showlittleform(DTP_StartDate.Value.Date, DTP_EndDate.Value.Date, read_tree(), readshift())

    End Sub
    Sub showlittleform(date1 As Date, date2 As Date, machine As String, shft As Integer)
        ReDim Preserve TimeLine(UBound(form_) + 1)
        numberoflittleforms = UBound(form_)
        ReDim Preserve shiftofeachform(UBound(form_) + 1)
        shiftofeachform(UBound(form_)) = shft

        TimeLine(UBound(form_)) = CSI_LIB.TimeLine(date1, machine, 1)

        ReDim Preserve form_(UBound(form_) + 1)
        form_(UBound(form_)) = New littleform
        form_(UBound(form_)).TopMost = True
        form_(UBound(form_)).Show()

    End Sub
    Function readshift() As Integer
        If RB_Shift1.Checked = True Then Return 1
        If RB_Shift2.Checked = True Then Return 2
        If RB_Shift3.Checked = True Then Return 3
        Return 0
    End Function

    '-----------------------------------------------------------------------------------------------------------------------
    ' Read the treeview and gives the checked machine in string
    '-----------------------------------------------------------------------------------------------------------------------
    Function read_tree() As String


        Dim active_machines(1) As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0

        For Each treeviewnode As TreeNode In Me.TreeView_machine.Nodes
            If treeviewnode.Checked = True Then
                active_machines(i) = treeviewnode.Text
                i = i + 1
                ReDim Preserve active_machines(i)
            End If
            For Each treeviewnode2 As TreeNode In treeviewnode.Nodes
                For Each treeviewnode3 As TreeNode In treeviewnode2.Nodes
                    If treeviewnode3.Checked = True Then
                        active_machines(i) = treeviewnode3.Text
                        i = i + 1
                        ReDim Preserve active_machines(i)
                    End If
                    k = k + 1
                Next
                If treeviewnode2.Checked = True Then
                    active_machines(i) = treeviewnode2.Text
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
            For Each item2 In Config_report.list2
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


        Return active_machines(0)
        '*************************************************************************************************************************************************'
        '**** Active machines from the treenode -END
        '*************************************************************************************************************************************************'

    End Function




    '===============================================================================
    ' TreeView One check
    '===============================================================================
#Region "One check TreeView"
    Private Sub treeview1_AfterCheck(sender As Object, e As TreeViewEventArgs)
        If e.Node.Checked Then
            DiselectParentNodes(e.Node.Parent)
            DiselectChildNodes(e.Node.Nodes)
        End If
    End Sub

    Private Sub DiselectParentNodes(parent As TreeNode)
        While parent IsNot Nothing
            If parent.Checked Then
                parent.Checked = False
            End If
            parent = parent.Parent
        End While
    End Sub

    Private Sub DiselectChildNodes(childes As TreeNodeCollection)
        For Each oneChild As TreeNode In childes
            If oneChild.Checked Then
                oneChild.Checked = False
            End If
            DiselectChildNodes(oneChild.Nodes)
        Next
    End Sub
#End Region

End Class
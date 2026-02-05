Public Class groupename

    Private Sub BTN_Ok_Click(sender As Object, e As EventArgs) Handles BTN_Ok.Click

        Try
            If TB_Name.Text <> "" Then
                If findnamesTreeRecurcive() Then GoTo End___
                Config_report.groupename___ = TB_Name.Text
                Me.Close()
            End If
End___:
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub



    Private Function findnamesTreeRecurcive() As Boolean

        Dim aNode As TreeNode
        For Each aNode In Config_report.TreeView_machine.Nodes()
            If namesRecursive(aNode) Then Return True
        Next
        Return False
    End Function


    Private Function namesRecursive(ByVal n As TreeNode) As Boolean
        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            If TB_Name.Text = aNode.Text Then
                MessageBox.Show("You already have a groupe or a machine with this name, in " & n.Text)
                Return True
            End If
            namesRecursive(aNode)
        Next
        Return False
    End Function

    Private Sub BTN_Cancel_Click(sender As Object, e As EventArgs) Handles BTN_Cancel.Click
        Me.Close()
    End Sub
End Class
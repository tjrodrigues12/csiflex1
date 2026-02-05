Public Class Select_years
    Private Sub Select_years_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        CLB_years.Items.Clear()

        For Each item In Welcome.years__
            CLB_years.Items.Add(item)
            If item = Now.Year Then
                CLB_years.SetItemChecked(CLB_years.Items.Count - 1, True)
            End If
        Next

    End Sub

    Private Sub BTN_ok_Click(sender As Object, e As EventArgs) Handles BTN_ok.Click

        Dim i As Integer
        For i = 0 To CLB_years.Items.Count - 1
            If CLB_years.GetItemCheckState(i) = False Then
                Welcome.years__.Remove(CLB_years.Items.Item(i))
            End If
        Next

        'For Each item In CLB_years.Items

        '    If item.checkeditem = False Then
        '        Welcome.years__.Remove(item)
        '    End If

        'Next

        Close()
    End Sub
End Class
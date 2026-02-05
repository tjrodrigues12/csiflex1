Public Class SelectUserType

    Private Sub BTN_Ok_Click(sender As Object, e As EventArgs) Handles BTN_Ok.Click
        Me.Location = New Point(SetupForm2.Location.X - 150, SetupForm2.Location.Y + (SetupForm2.Width - 417) / 2)
        If RB_User.Checked Then SetupForm2.selectedtype = "user"
        If RB_Admin.Checked Then SetupForm2.selectedtype = "admin"
        If RB_Programmer.Checked Then SetupForm2.selectedtype = "Programer"
        Me.Close()
    End Sub

End Class
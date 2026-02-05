Public Class NewLicenseForm

    Dim form2 As RegisterLicenceForm

    Private Sub NewLic_Click(sender As Object, e As EventArgs) Handles NewLic.Click
        Dim regform As RegistrationForm = New RegistrationForm()
        regform.ShowDialog()
    End Sub

    Private Sub ExistingLic_Click(sender As Object, e As EventArgs) Handles ExistingLic.Click
        Dim licform As RegisterLicenceForm = New RegisterLicenceForm()
        Dim dlgres As DialogResult = New DialogResult
        dlgres = licform.ShowDialog()
        If (dlgres = Windows.Forms.DialogResult.OK) Then
            Dispose()
        End If
    End Sub

End Class
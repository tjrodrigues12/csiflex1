Public Class RenewLicense

    Private Sub BTN_NewLicense_Click(sender As Object, e As EventArgs) Handles BTN_NewLicense.Click
        Dim register As New RegisterLicenceForm
        If (register.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Me.Dispose()
        End If
    End Sub

    Private Sub BTN_Continue_Click(sender As Object, e As EventArgs) Handles BTN_Continue.Click
        Me.Dispose()
    End Sub
End Class
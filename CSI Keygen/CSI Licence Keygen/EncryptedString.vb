Public Class EncrytpedString
    'cpuid:GenuineIntel BFEBFBFF000306C3;hdd:BC42F688;
    Public encryptedString As String
    Private Sub BTN_Accept_Click(sender As Object, e As EventArgs) Handles BTN_Accept.Click
        encryptedString = RTB_EncryptedString.Text
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class
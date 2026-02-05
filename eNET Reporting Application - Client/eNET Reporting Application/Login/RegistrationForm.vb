Imports System.Net.Mail

Public Class RegistrationForm

    Private Sub BTN_Register_Click(sender As Object, e As EventArgs) Handles BTN_Register.Click
        If (CheckFields()) Then
            SendEmail()
            Dispose()
        End If
    End Sub

    Private Function CheckFields() As Boolean
        CheckFields = True
        LBL_Company.ForeColor = Color.Black
        LBL_Email.ForeColor = Color.Black
        LBL_Ext.ForeColor = Color.Black
        LBL_FirstName.ForeColor = Color.Black
        LBL_LastName.ForeColor = Color.Black
        LBL_Phone.ForeColor = Color.Black

        'faire une boucle for each des controle dans le groupbox

        If (TB_FirstName.TextLength <= 0) Then
            CheckFields = False
            LBL_FirstName.ForeColor = Color.Red
        End If

        If (TB_LastName.TextLength <= 0) Then
            CheckFields = False
            LBL_LastName.ForeColor = Color.Red
        End If

        If (TB_Company.TextLength <= 0) Then
            CheckFields = False
            LBL_Company.ForeColor = Color.Red
        End If

        If (TB_Phone.TextLength <= 0) Then
            CheckFields = False
            LBL_Phone.ForeColor = Color.Red
        End If

        If (TB_Email.Text.IndexOf("@") <= 0) Then
            CheckFields = False
            LBL_Email.ForeColor = Color.Red
        End If

    End Function
    'source:http://stackoverflow.com/questions/9201239/send-e-mail-via-smtp-using-c-sharp
    Private Sub SendEmail()

        Dim mail As MailMessage = New MailMessage("license@cam-solutions.ca", TB_Email.Text.Trim())
        Dim client As SmtpClient = New SmtpClient("mail.cam-solutions.ca", 2525)

        client.EnableSsl = False
        client.Timeout = 10000
        client.DeliveryMethod = SmtpDeliveryMethod.Network
        client.UseDefaultCredentials = False
        client.Credentials = New System.Net.NetworkCredential("license@cam-solutions.ca", "2n3pb6yznx")

        mail.Subject = "CSI Flex License key"
        mail.Body = "Hi " + TB_FirstName.Text + " " + TB_LastName.Text + "," & vbCrLf &
            "This is your CSI Flex License Key :" & vbCrLf & GenerateLicense()

        Dim bcc As MailAddress = New MailAddress("license@cam-solutions.ca")
        mail.Bcc.Add(bcc)

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

        Try
            client.Send(mail)
            MessageBox.Show("You should receive an email shortly with your license key")
        Catch ex As Exception
            MessageBox.Show("email error:" + ex.Message, "EMAIL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        
    End Sub

    Private Function GenerateLicense() As String

        Dim infos As String
        infos = "cpuid:" + Login.GetCPUSerialNumber() + ";hdd:" + Login.GetDriveSerialNumber("C:") + ";exp:" + DateTime.Now.AddDays(14).ToShortDateString() + ";"

        GenerateLicense = Login.AES_Encrypt(infos, "license")


    End Function
End Class
Imports System.IO
Imports System.Net.Mail
Imports CSIFLEX.Database.Access
Imports MySql.Data.MySqlClient

Public Class EmailServer

    Private default_email As CSI_Library.EmailSettings = New CSI_Library.EmailSettings
    Private custom_email As CSI_Library.EmailSettings = Nothing

    Private Sub EmailServer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CSI_Lib = New CSI_Library.CSI_Library(True)
        LoadEmailConfig()
    End Sub

    Private Sub BTN_Save_Click(sender As Object, e As EventArgs) Handles BTN_Save.Click
        If SaveEmailConfig() Then
            Dispose()
        End If
    End Sub

    Public Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            AES.Padding = System.Security.Cryptography.PaddingMode.Zeros
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    Private Sub LoadEmailConfig()

        Try

            Dim results = MySqlAccess.GetDataTable("SELECT * FROM CSI_auth.tbl_emailreports")

            For Each email As DataRow In results.Rows

                If Boolean.Parse(email("isdefault").ToString()) Then
                    rbDefaultEmail.Checked = Boolean.Parse(email("isused").ToString())
                    rbCustomEmail.Checked = Not Boolean.Parse(email("isused").ToString())
                    default_email = CSI_Library.CSI_Library.BuildEmailSetting(email)
                Else

                    TB_Sender.Text = email("senderemail").ToString()
                    TB_Host.Text = email("smtphost").ToString()
                    TB_Port.Text = email("smtpport").ToString()
                    CHKB_ReqCred.Checked = Boolean.Parse(email("requirecred").ToString())
                    chkUseSSL.Checked = Boolean.Parse(email("usessl").ToString())
                    custom_email = CSI_Library.CSI_Library.BuildEmailSetting(email)
                End If
            Next


            'Dim defaultIndex As Integer = 0
            'If (results.Rows.Count > 0) Then
            '    If results.Rows.Count > 1 Then
            '        If Convert.ToBoolean(results.Rows(0)("isdefault")) Then
            '            default_email = CSI_Library.CSI_Library.BuildEmailSetting(results.Rows(0))
            '            custom_email = CSI_Library.CSI_Library.BuildEmailSetting(results.Rows(1))
            '        Else
            '            default_email = CSI_Library.CSI_Library.BuildEmailSetting(results.Rows(1))
            '            custom_email = CSI_Library.CSI_Library.BuildEmailSetting(results.Rows(0))
            '        End If
            '    Else
            '        default_email = CSI_Library.CSI_Library.BuildEmailSetting(results.Rows(0))
            '        custom_email = Nothing
            '    End If

            '    If custom_email IsNot Nothing Then
            '        TB_Sender.Text = custom_email.SenderEmail
            '        If Not String.IsNullOrEmpty(custom_email.EncryptedPassword) Then
            '            TB_Pwd.Text = AES_Decrypt(custom_email.EncryptedPassword, "pass")
            '        End If
            '        TB_Host.Text = custom_email.SmtpHost
            '        TB_Port.Text = custom_email.SmtpPort
            '        CHKB_ReqCred.Checked = custom_email.RequireCred
            '        chkUseSSL.Checked = custom_email.UseSSL
            '    End If

            '    rbDefaultEmail.Checked = default_email.IsUsed
            '    gbCustomEmail.Enabled = Not default_email.IsUsed
            '    rbCustomEmail.Checked = Not default_email.IsUsed
            'End If

        Catch ex As Exception
            CSI_Lib.LogServerError("Unable to load reports email configuration:" + ex.Message, 1)
        End Try

    End Sub



    Private Function SaveEmailConfig() As Boolean

        Dim error_msg As String = ""

        Dim sqlCmd = New Text.StringBuilder()


        sqlCmd.Append("UPDATE CSI_auth.tbl_emailreports SET isused = 0; ")

        If rbDefaultEmail.Checked Then

            sqlCmd.Append("UPDATE CSI_auth.tbl_emailreports SET isused = 1 WHERE isdefault = 1;")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Else

            Dim emails = MySqlAccess.GetDataTable("SELECT * FROM CSI_auth.tbl_emailreports")

            Dim mySqlCmd As New MySqlCommand()
            mySqlCmd.Parameters.AddWithValue("@senderemail", TB_Sender.Text)
            mySqlCmd.Parameters.AddWithValue("@smtphost", TB_Host.Text)
            mySqlCmd.Parameters.AddWithValue("@smtpport", TB_Port.Text)
            mySqlCmd.Parameters.AddWithValue("@requirecred", CHKB_ReqCred.Checked)
            mySqlCmd.Parameters.AddWithValue("@usessl", chkUseSSL.Checked)

            If emails.Rows.Count = 1 Then
                sqlCmd.Append("INSERT INTO                  ")
                sqlCmd.Append("   CSI_auth.tbl_emailreports ")
                sqlCmd.Append("   (                         ")
                sqlCmd.Append("       senderemail ,         ")
                sqlCmd.Append("       smtphost    ,         ")
                sqlCmd.Append("       smtpport    ,         ")
                sqlCmd.Append("       requirecred ,         ")
                sqlCmd.Append("       usessl      ,         ")
                sqlCmd.Append("       isdefault   ,         ")
                sqlCmd.Append("       isused                ")
                sqlCmd.Append("   )                         ")
                sqlCmd.Append("   VALUES                    ")
                sqlCmd.Append("   (                         ")
                sqlCmd.Append("       @senderemail,         ")
                sqlCmd.Append("       @smtphost   ,         ")
                sqlCmd.Append("       @smtpport   ,         ")
                sqlCmd.Append("       @requirecred,         ")
                sqlCmd.Append("       @usessl     ,         ")
                sqlCmd.Append("       0           ,         ")
                sqlCmd.Append("       1                     ")
                sqlCmd.Append("   );                        ")
            Else
                sqlCmd.Append("UPDATE                             ")
                sqlCmd.Append("   CSI_auth.tbl_emailreports       ")
                sqlCmd.Append(" SET                               ")
                sqlCmd.Append("       senderemail = @senderemail, ")
                sqlCmd.Append("       smtphost    = @smtphost   , ")
                sqlCmd.Append("       smtpport    = @smtpport   , ")
                sqlCmd.Append("       requirecred = @requirecred, ")
                sqlCmd.Append("       usessl      = @usessl     , ")
                sqlCmd.Append("       isused      = 1             ")
                sqlCmd.Append(" WHERE                             ")
                sqlCmd.Append("       isdefault   = 0           ; ")
            End If

            If Not String.IsNullOrEmpty(TB_Pwd.Text) Then
                sqlCmd.Append("UPDATE                             ")
                sqlCmd.Append("   CSI_auth.tbl_emailreports       ")
                sqlCmd.Append(" SET                               ")
                sqlCmd.Append("       senderpwd   = @senderpwd    ")
                sqlCmd.Append(" WHERE                             ")
                sqlCmd.Append("       isdefault   = 0           ; ")

                Dim encryptedPass = AES_Encrypt(TB_Pwd.Text, "pass")
                mySqlCmd.Parameters.AddWithValue("@senderpwd", encryptedPass)
            End If

            mySqlCmd.CommandText = sqlCmd.ToString()

            MySqlAccess.ExecuteNonQuery(mySqlCmd)

        End If

        Return True


        'cnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        'Try
        '    cnt.Open()
        '    Dim query As String = "UPDATE CSI_auth.tbl_emailreports  SET isused = 1 Where isdefault = " & If(rbDefaultEmail.Checked, 1, 0) & ";"
        '    query += "UPDATE CSI_auth.tbl_emailreports  SET isused = 0 Where isdefault = " & If(rbDefaultEmail.Checked, 0, 1) & ";"
        '    Dim cmd2 As New MySqlCommand(query, cnt)
        '    cmd2.ExecuteNonQuery()
        '    default_email.IsUsed = rbDefaultEmail.Checked
        'Catch ex As Exception
        '    CSI_Lib.LogServerError("Unable to update isused reports email:" + ex.Message, 1)
        'Finally
        '    cnt.Close()
        'End Try

        ''End If
        ''update custom email
        'If Not rbDefaultEmail.Checked Then
        '    If ValidateEntries(error_msg) Then
        '        cnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        '        Try
        '            cnt.Open()
        '            Dim query As String = "UPDATE CSI_auth.tbl_emailreports  SET " +
        '                                          " senderemail = @senderemail," +
        '                                          " senderpwd =  @senderpwd," +
        '                                          " smtphost =  @smtphost," +
        '                                          " smtpport =  @smtpport," +
        '                                          " requirecred = @reqcred," +
        '                                          " usessl = @usessl" +
        '                                          " Where isdefault = 0"

        '            Dim cmd2 As New MySqlCommand(query, cnt)
        '            Dim encryptedPass = AES_Encrypt(TB_Pwd.Text, "pass")
        '            cmd2.Parameters.AddWithValue("@senderemail", TB_Sender.Text)
        '            cmd2.Parameters.AddWithValue("@senderpwd", encryptedPass)
        '            cmd2.Parameters.AddWithValue("@smtphost", TB_Host.Text)
        '            cmd2.Parameters.AddWithValue("@smtpport", TB_Port.Text)
        '            cmd2.Parameters.AddWithValue("@reqcred", CHKB_ReqCred.Checked)
        '            cmd2.Parameters.AddWithValue("@usessl", chkUseSSL.Checked)

        '            If (cmd2.ExecuteNonQuery() = 0) Then
        '                cmd2.CommandText = "INSERT INTO CSI_auth.tbl_emailreports " +
        '                "(senderemail, senderpwd, smtphost, smtpport, requirecred, isdefault, usessl) " +
        '                 "VALUES (@senderemail, @senderpwd, @smtphost, @smtpport, @reqcred, 0, @usessl)"
        '                cmd2.ExecuteNonQuery()
        '            End If
        '            custom_email = New CSI_Library.EmailSettings
        '            custom_email.SenderEmail = TB_Sender.Text
        '            custom_email.EncryptedPassword = encryptedPass
        '            custom_email.SmtpHost = TB_Host.Text
        '            custom_email.SmtpPort = Convert.ToInt32(TB_Port.Text)
        '            custom_email.RequireCred = CHKB_ReqCred.Checked
        '            custom_email.UseSSL = chkUseSSL.Checked
        '            custom_email.IsUsed = True
        '            MsgBox("reports email successfully saved")
        '            Return True
        '        Catch ex As Exception
        '            CSI_Lib.LogServerError("Unable to update reports email configuration:" + ex.Message, 1)
        '        Finally
        '            cnt.Close()
        '        End Try
        '    Else
        '        MsgBox(error_msg)
        '    End If
        '    Return False
        'Else
        '    ' default email is used 
        '    'nothing to do
        '    Return True
        'End If


    End Function

    Private Function ValidateEntries(ByRef error_msg) As Boolean
        error_msg = ""
        If String.IsNullOrEmpty(TB_Host.Text) Then
            error_msg = "SMTP Host is required"
            Return False
        End If
        If String.IsNullOrEmpty(TB_Port.Text) Then
            error_msg = "SMTP Port is required"
            Return False
        Else
            Dim port = 0
            If Not Integer.TryParse(TB_Port.Text, port) Then
                error_msg = "SMTP Port is a number"
            End If
        End If
        If String.IsNullOrEmpty(TB_Sender.Text) Then
            error_msg = "Sender Email is required"
            Return False
        Else
            If TB_Sender.Text.ToLower() = default_email.SenderEmail.ToLower Then
                error_msg = "Sender Email must be different than the default email"
                Return False
            End If
        End If
        If CHKB_ReqCred.Checked Then
            If String.IsNullOrEmpty(TB_Pwd.Text) Then
                error_msg = "Password is required when Credential is checked"
                Return False
            End If
        End If
        Return True
    End Function

    Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            AES.Padding = Security.Cryptography.PaddingMode.Zeros
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub CHKB_ReqCred_CheckedChanged(sender As Object, e As EventArgs) Handles CHKB_ReqCred.CheckedChanged
        TB_Pwd.Text = ""
        TB_Pwd.Enabled = CHKB_ReqCred.Checked
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim host As String = ""
        Dim smtpport As Integer = 0
        Dim sender_ As String = ""
        Dim auth As Boolean
        Dim pass As String = ""
        Dim enableSSl As Boolean
        Dim err_msg = ""
        If String.IsNullOrEmpty(TB_test.Text) Then
            MsgBox("Test Email Field Is required" + TB_test.Text)
        Else
            If rbDefaultEmail.Checked Then
                host = default_email.SmtpHost
                smtpport = default_email.SmtpPort
                sender_ = default_email.SenderEmail
                auth = default_email.RequireCred
                pass = AES_Decrypt(default_email.EncryptedPassword, "pass")
                enableSSl = default_email.UseSSL
            Else
                If ValidateEntries(err_msg) Then
                    host = TB_Host.Text
                    smtpport = Convert.ToInt32(TB_Port.Text)
                    sender_ = TB_Sender.Text
                    auth = CHKB_ReqCred.Checked
                    pass = TB_Pwd.Text
                    enableSSl = chkUseSSL.Checked
                End If
            End If
            If String.IsNullOrEmpty(err_msg) Then
                Try

                    Dim mail As MailMessage = New MailMessage()
                    Dim client As SmtpClient = New SmtpClient(host, smtpport)
                    If Not sender_.Contains("@") Then
                        mail.From = New MailAddress("csiflexreports@gmail.com")
                    Else
                        mail.From = New MailAddress(sender_)
                    End If
                    client.EnableSsl = enableSSl
                    'client.Timeout = 10000
                    client.DeliveryMethod = SmtpDeliveryMethod.Network
                    client.UseDefaultCredentials = False
                    If (auth) Then
                        client.Credentials = New System.Net.NetworkCredential(sender_, pass)
                    End If
                    mail.Subject = "CSIFlex email Notificatinon test "
                    mail.Body = vbCrLf + vbCrLf + "This is a test email sent from CSIFlex" + vbCrLf + vbCrLf
                    mail.To.Add(New MailAddress(TB_test.Text))
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                    client.Send(mail)
                    MsgBox("The test message has been sent to " + TB_test.Text)
                    'End If
                Catch ex As Exception
                    If ex.Message = "The operation has timed out." Then

                    End If
                    MsgBox("Could not send a test email : " + ex.Message)

                    '  CSI_LIB.LogServiceError("email error,CONFIG:MailAdresses:" + mailadresses.ToString() + ",port:" + emailconfig.Rows(0)("smtpport").ToString() + ",server:" + emailconfig.Rows(0)("smtphost").ToString() + ",requirecred:" + emailconfig.Rows(0)("requirecred").ToString() + ",email:" + emailconfig.Rows(0)("senderemail").ToString() + ",pwd:" + emailconfig.Rows(0)("senderpwd").ToString() + ",MSG:" + ex.Message, 1)
                End Try
            Else
                MsgBox(err_msg)
            End If

        End If


    End Sub

    Private Sub rbDefaultEmail_CheckedChanged(sender As Object, e As EventArgs) Handles rbDefaultEmail.CheckedChanged
        gbCustomEmail.Enabled = Not rbDefaultEmail.Checked
    End Sub
End Class
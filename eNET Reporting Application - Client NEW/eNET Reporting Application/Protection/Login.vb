Imports System.Security.Cryptography
Imports System.Management
Imports System.IO
Imports Microsoft.Win32
Imports System.Runtime.InteropServices
Imports CSI_Library.CSI_Library

'mail
Imports System.Net.Mail

'http://msdn.microsoft.com/en-us/library/windows/desktop/aa389273(v=vs.85).aspx



Public Class Login
    Private TripleDes As New TripleDESCryptoServiceProvider
    Private loginOK As Boolean = False
    Private myCode As Int64
    Public CSIFLEX As New CSI_Library.CSI_Library
    Private csiversion As Integer
    Private Const FREETRIAL As Int32 = 30

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.Height = 587
        'Me.Width = 404
        'Me.Width = 789
        csiversion = Welcome.CSIF_version
        If (csiversion <= 0) Then
            MessageBox.Show("Invalid version")
            Environment.Exit(0)
        End If
        '   Dim computername As String = My.Computer.Name
        '   Dim user As String = My.User.Name

        'analyse data from system:
        'Dim Serial As String = GetDriveSerialNumber("C:") & GetCPUSerialNumber()

        'Customer Licence code, wich will be sent to CamSolutions
        'Label1.Text = AES_Encrypt(Serial, "pass")




    End Sub



#Region "ulitities"

    ''' <summary>
    ''' generate login code from computer data
    ''' </summary>
    ''' <param name="_coding">encryted string</param>
    ''' <returns>login code dez / string</returns>
    Private Function generateCode(ByVal _coding As String) As Int64
        Dim _code As Int64
        For I As Integer = 1 To Len(_coding)
            _code += (Asc(Mid(_coding, I, 1)) * 915734)
        Next
        Return _code
    End Function


    ''' <summary>
    ''' Save the exp date
    ''' </summary>
    ''' <param name="date_">exp date</param>
    Private Sub saveexp(date_ As Date)
        'Save the encrypted date
        'Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services", True)

        ''  key.SetValue("ObjectName", "Localsystem")
        'If My.Computer.Registry.GetValue("SYSTEM\CurrentControlSet\Services\LocalControl", "LocalControl", Nothing) Is Nothing Then
        '    key = key.CreateSubKey("LocalControl")
        'End If
        'key.SetValue("LocalControl", EncryptData(Now.Day & Now.Month & Now.Year))
        'key.Close()
    End Sub


    ''' <summary>
    ''' get dirve serial number
    ''' </summary>
    ''' <param name="drive">C:</param>
    ''' <returns>drive serial</returns>
    Public Function GetDriveSerialNumber(ByVal drive As String) As String

        Dim driveSerial As String = String.Empty
        Dim driveFixed As String = System.IO.Path.GetPathRoot(drive)
        driveFixed = Replace(driveFixed, "\", String.Empty)

        Using querySearch As New System.Management.ManagementObjectSearcher("SELECT VolumeSerialNumber FROM Win32_LogicalDisk Where Name = '" & driveFixed & "'")

            Using queryCollection As ManagementObjectCollection = querySearch.Get()

                Dim moItem As ManagementObject

                For Each moItem In queryCollection

                    driveSerial = CStr(moItem.Item("VolumeSerialNumber"))

                    Exit For
                Next
            End Using
        End Using
        Return driveSerial
    End Function




    ''' <summary>
    ''' get CPU serial number
    ''' </summary>
    ''' <returns>drive serial</returns>
    Public Function GetCPUSerialNumber() As String

        'http://msdn.microsoft.com/en-us/library/windows/desktop/aa394373(v=vs.85).aspx

        Dim cpuSerial As String = String.Empty

        Using querySearch As New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor")

            Using queryCollection As ManagementObjectCollection = querySearch.Get()

                For Each moItem As ManagementObject In queryCollection

                    cpuSerial = CStr(moItem.Item("Manufacturer"))
                    cpuSerial = cpuSerial & " " & CStr(moItem.Item("ProcessorID"))

                    Exit For
                Next
            End Using
        End Using
        Return cpuSerial
    End Function

    ''' <summary>
    ''' AES_ENC
    ''' </summary>
    ''' <returns>Crypted </returns>
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
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' AES_DEC
    ''' </summary>
    ''' <returns>decrypted </returns>
    Public Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
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
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' save_to_file, save expiration date + options
    ''' </summary>
    ''' <returns>decrypted </returns>
    'Function save_to_file(data As String) As Boolean
    '    Dim path As String = System.Windows.Forms.Application.StartupPath & "\wincsi.dl1"

    '    data = AES_Encrypt(data, "pass")

    '    Try

    '        Dim textToAdd As String = AES_Encrypt(data, "pass")

    '        If File.Exists(path) Then File.Delete(path)

    '        Using fs As FileStream = New FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite)
    '            Using writer As StreamWriter = New StreamWriter(fs)
    '                writer.Write(textToAdd)
    '                writer.Close()
    '            End Using
    '        End Using

    '        ' Get file info
    '        Dim myFile As FileInfo = New FileInfo(path)
    '        myFile.Attributes = FileAttributes.Hidden

    '        Return True

    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    'Function save_to_regedit(data As String) As Boolean

    '    data = AES_Encrypt(data, "pass")

    '    Try
    '        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\", True)
    '        If key.GetValue("value") Is Nothing Then
    '            Registry.LocalMachine.CreateSubKey("SYSTEM\CurrentControlSet\Services\").SetValue("value", data)
    '        Else
    '            key.SetValue("value", (data))
    '        End If
    '        key.Close()

    '        Return True

    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    ''' <summary>
    ''' send_e_mail 
    ''' </summary>
    ''' <returns>bool </returns>
    Function send_e_mail(data As String, subject As String)
        Try
            Dim SmtpServer As New Net.Mail.SmtpClient("smtp.gmail.com", 587)
            SmtpServer.EnableSsl = True

            SmtpServer.Timeout = 1000

            SmtpServer.Credentials = New System.Net.NetworkCredential(" ", " ")

            Dim mail As New Net.Mail.MailMessage("salmi.hmd@gmail.com", "salmi.hmd@gmail.com", subject, data)

            SmtpServer.Send(mail)

            Return True
        Catch ex As Exception
            MessageBox.Show("Unable to contact the CSI Flex server ") ' & ex.Message)
            Return False
        End Try
    End Function

    ''''''''''''''''''''''''''''''''''FROM REGISTRATION FORM - by Remi Bedard-Couture


    Private Function CheckFields() As Boolean
        CheckFields = True
        LBL_Company.ForeColor = Color.Black
        LBL_Email.ForeColor = Color.Black
        'LBL_Ext.ForeColor = Color.Black
        LBL_FirstName.ForeColor = Color.Black
        LBL_LastName.ForeColor = Color.Black
        LBL_Phone.ForeColor = Color.Black
        LBL_Address.ForeColor = Color.Black
        LBL_City.ForeColor = Color.Black
        LBL_State.ForeColor = Color.Black

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

        If (TB_Address.TextLength <= 0) Then
            CheckFields = False
            LBL_Address.ForeColor = Color.Red
        End If

        If (TB_City.TextLength <= 0) Then
            CheckFields = False
            LBL_City.ForeColor = Color.Red
        End If

        If (TB_State.TextLength <= 0) Then
            CheckFields = False
            LBL_State.ForeColor = Color.Red
        End If

        If (TB_Email.Text.IndexOf("@") <= 0) Then
            CheckFields = False
            LBL_Email.ForeColor = Color.Red
        End If

    End Function
    'source:http://stackoverflow.com/questions/9201239/send-e-mail-via-smtp-using-c-sharp
    Private Sub SendEmail()

        Dim client As SmtpClient = New SmtpClient("mail.cam-solutions.ca", 2525)

        client.EnableSsl = False
        client.Timeout = 10000
        client.DeliveryMethod = SmtpDeliveryMethod.Network
        client.UseDefaultCredentials = False
        client.Credentials = New System.Net.NetworkCredential("license@cam-solutions.ca", "2n3pb6yznx")

        Dim mail As MailMessage = New MailMessage("license@cam-solutions.ca", "license@cam-solutions.ca") 'TB_Email.Text.Trim())

        'If (csiversion = 1) Then
        '    'no need for copy since if we are sending it to ourself
        '    mail = New MailMessage("license@cam-solutions.ca", TB_Email.Text.Trim())
        '    Dim bcc As MailAddress = New MailAddress("license@cam-solutions.ca")
        '    mail.Bcc.Add(bcc)
        'End If

        Dim subject As String
        subject = "CSI Flex License key registration"
        If (csiversion = 1) Then
            subject = "CSI Flex Lite License key registration"
        ElseIf (csiversion = 2) Then
            subject = "CSI Flex Standard License key registration"
        ElseIf (csiversion = 3) Then
            subject = "CSI Flex Client with Server License key registration"
        End If

        mail.Subject = subject
        mail.Body = "Hi " + TB_FirstName.Text + " " + TB_LastName.Text + "," & vbCrLf &
            "This is your CSI Flex License Key :" & vbCrLf & GenerateLicense() & vbCrLf & vbCrLf &
            "Other info:" & vbCrLf &
            "Company:" & TB_Company.Text & vbCrLf &
            "Email:" & TB_Email.Text & vbCrLf &
            "Phone:" & TB_Phone.Text & vbCrLf &
            "Address:" & TB_Address.Text & vbCrLf &
            "City:" & TB_City.Text & vbCrLf &
            "State:" & TB_State.Text


        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

        Try
            'LICENSE PATCH
            ' client.Send(mail)
            'MessageBox.Show("You should receive an email shortly with your license key", "Registration complete")
        Catch ex As Exception
            'MessageBox.Show("email error:" + ex.Message, "e-mail error")
            CSIFLEX.LogServerError("email error:" + ex.Message, 1)
        End Try

    End Sub

    Private Function GenerateLicense() As String
        'NEED TO BE CHANGED'
        Dim infos As String

        infos = "cpuid:" + GetCPUSerialNumber() + ";hdd:" + GetDriveSerialNumber("C:") + ";exp:" + DateTime.Now.AddDays(FREETRIAL).ToShortDateString() + ";version:" + csiversion.ToString() + ";"

        GenerateLicense = AES_Encrypt(infos, "license")

        'Create lite license while waiting for trial or official
        Dim triallicense As String
        triallicense = "cpuid:" + GetCPUSerialNumber() + ";hdd:" + GetDriveSerialNumber("C:") + ";exp:" + DateTime.Now.AddDays(FREETRIAL).ToShortDateString() + ";version:" + csiversion.ToString() + ";"

        triallicense = AES_Encrypt(triallicense, "license")

        Dim path As String = CSI_Library.CSI_Library.ClientRootPath

        If (File.Exists(path & "\wincsi.dl1")) Then
            File.Delete(path & "\wincsi.dl1")
        End If

        Using writer As StreamWriter = New StreamWriter(path & "\wincsi.dl1", True)
            writer.Write(triallicense)
            writer.Close()
        End Using


        If Not Directory.Exists(path & "\sys\") Then
            Directory.CreateDirectory(path & "\sys\")
        End If

        Try
            Using writer As StreamWriter = New StreamWriter(path & "\sys\CSIFv_.csys", False)
                Dim lib__ As New CSI_Library.CSI_Library

                writer.Write(AES_Encrypt(csiversion.ToString() + ":-----", "4Solutions"))
                writer.Close()
                Welcome.CSIF_version = csiversion
            End Using
        Catch ex As Exception
            MessageBox.Show("Unable to save the database path ") ' & ex.Message)
            'everythingIsOkToStart = False
        End Try


    End Function


    ''''''''''''''''''''''''''''''''''''''''''


#End Region

#Region "ok and next button"

    Private Sub Accept()
        Try

            DialogResult = Windows.Forms.DialogResult.OK
            Dispose()

            Reporting_application.activated_ = True
            Me.Close()
end_:

        Catch ex As Exception
            MessageBox.Show("The registration procedure has failed, Contact CSI Flex for more informations.") 'Error : " & ex.Message)
        End Try

    End Sub


    Private Sub BTN_RequestLicence_Click(sender As Object, e As EventArgs) Handles BTN_RequestLicence.Click
        'CHECK DATA!!
        If (CheckFields()) Then
            SendEmail()
            Accept()
        Else
            MessageBox.Show("The contact informations you provided are incorrect", "Contact")
        End If
    End Sub

#End Region



    Private Sub BTN_ContactInfo_Click(sender As Object, e As EventArgs) Handles BTN_ContactInfo.Click
        Contact_info.ShowDialog()

        'MessageBox.Show("if you need help or information, please contact info@cam-solutions.ca", "Contact", MessageBoxButtons.OK, MessageBoxIcon.Question)
    End Sub


    Private Sub BTN_AlreadyHaveLicense_Click(sender As Object, e As EventArgs) Handles BTN_AlreadyHaveLicense.Click

        Dim ofd_license As New OpenFileDialog

        'ofd_license.InitialDirectory = "c:\"
        ofd_license.Filter = "dl1 files (*.dl1)|*.dl1"
        ofd_license.Title = "Select your CSI Flex license file"
        'ofd_license.FilterIndex = 2
        ofd_license.RestoreDirectory = True

        ofd_license.ShowDialog()
        If (ofd_license.FileName.Length > 0) Then
            CopyLicense(ofd_license.FileName)
            MessageBox.Show("Please reboot CSIFlex to apply the changes")
        End If


    End Sub

    Private Sub CopyLicense(filename As String)
        Dim path As String = ClientRootPath

        File.Copy(filename, path & "\Twincsi.dl1", True)
        File.Delete(filename)
        File.Copy(path & "\Twincsi.dl1", path & "\wincsi.dl1", True)
        File.Delete(path & "\Twincsi.dl1")

        'FileCopy(filename, rootpath.DirectoryName & "\wincsi.dl1")

        If (File.Exists(ClientRootPath & "\sys\CSIFv_.csys")) Then
            File.Delete(ClientRootPath & "\sys\CSIFv_.csys")
        End If

        'DialogResult = Windows.Forms.DialogResult.OK
        'Me.Close()

    End Sub

End Class


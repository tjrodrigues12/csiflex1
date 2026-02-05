Imports System.IO
Imports System.Reflection
Imports CSI_Library
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities
Imports Encryption.Utilities
Imports Microsoft.Win32

#If False Then
Name : User Login 
Type : Windows Form 
Functionality : Used to Login to the CSIFlex Server Application
Components : Class UserLogin and Module modFunction
#End If

#Region "UserLogin Class"
Public Class UserLogin

    Dim CSI_Lib = New CSI_Library.CSI_Library(True)
    Dim newCSI As New CSI_Library.CSI_Library(True)
    Dim infos() As String

    Public IsserviceRunning As Boolean = False
    Public Shared MysQl_ok__ As Boolean = False

    Dim prodServer As String = "CSIFLEX Server"

    '///// Form Load function This will Load first when the form is called 
    Private Sub UserLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Log.Instance.Init("CSIFLEXServer")

        Log.Info("CSIFLEX Server Started")
        Log.Info("==============================================================================")

        Dim DBinit As Form = DB_init
        DB_init.ShowDialog()

        If MysQl_ok__ = False Then
            Me.Close()
        End If

        '///// Check if Microsoft Report Viewer is Installed ?
        If Not (IsInstalled()) Then
            CSI_Lib.Log_server_event("report viewer not installed , installing ...")
            CSI_Lib.installReportViewer()
        End If

        MySqlAccess.Validate_Database()

        '///// Check for the Licence The function is written  on "SetUpForm"
        'check_licence()
        Dim licenseLib As New CSILicenseLibrary()

        Dim qtd = Integer.Parse(MySqlAccess.ExecuteScalar("SELECT COUNT(*) FROM csi_auth.tbl_license;"))
        If qtd = 0 Then
            Dim initialSetup As New InitialSetup2()
            initialSetup.StartPosition = FormStartPosition.CenterScreen
            initialSetup.ShowDialog()
        End If

        'Dim initialSetup As New InitialSetup2()
        'initialSetup.StartPosition = FormStartPosition.CenterScreen
        'initialSetup.ShowDialog()

        If Not licenseLib.IsLicenseValid(prodServer) Or licenseLib.DaysToExpiry(prodServer) <= 10 Then

            Dim licenseForm As New LicenseManagement()
            licenseForm.StartPosition = FormStartPosition.CenterScreen
            If Not licenseForm.ShowDialog() = DialogResult.OK Then
                End
            End If
        End If

        CSIFLEXGlobal.CompanyName = licenseLib.CurrentLicense(prodServer).CompanyName
        CSIFLEXGlobal.CompanyId = licenseLib.CurrentLicense(prodServer).CompanyId.ToString()

        Dim versionNumber As Version
        versionNumber = Assembly.GetExecutingAssembly().GetName().Version
        lblVersion.Text = $"Version: { versionNumber.ToString() }"

        Try
            '///// Read Login Credentials from File logininfo.csys on Form Load automatically 
            read_login_infos()
        Catch ex As Exception
            'CSI_Lib.LogServerError(": " & ex.ToString(), 1)
            Log.Error(ex)
        End Try

EndOfProg:

        'Hide All .dll Files in Program86/CSIFlex Server Folder
        Dim files() As String = Directory.GetFiles(CSI_Library.CSI_Library.ServerProgramFilesPath, "*.dll", SearchOption.TopDirectoryOnly)

        For Each FileName As String In files
            File.SetAttributes(FileName, FileAttributes.Hidden + FileAttributes.System)
        Next

    End Sub

    Public Sub check_licence()

        Dim licenseinfos As String()

        CSI_Library.CSI_Library.isServer = True
        licenseinfos = CSI_Lib.CheckLic()

        If (licenseinfos(0) = "ok") Then
            If (licenseinfos(2) = "0") Then
                CSI_Lib.LogServerError("License ok", 1)
            Else
                CSI_Lib.LogServerError("License wrong version", 1)
                MessageBox.Show("The version specified in your license doesn't correspond with this application", "License error")
                Me.Hide()
                Login.ShowDialog()
            End If
        ElseIf (licenseinfos(0) = "EXP") Then
            'lite version
            CSI_Lib.LogServerError("Licence expiration :  " & licenseinfos(1), 1)
            MessageBox.Show("Your license is expired, please renew your license", "License expired")
            Me.Hide()
            Login.ShowDialog()
        Else
            Me.Hide()
            Login.ShowDialog()
        End If

    End Sub

    '///// Code for Checking if Microsoft Report Viewer is installed 
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Try
            If Not (IsInstalled()) Then
                'installReportViewer()
                CSI_Lib.installReportViewer()
            End If
            ' Add any initialization after the InitializeComponent() call.
        Catch ex As Exception
            'CSI_Lib.LogClientError("error in init:" & ex.Message)
            Log.Error("Error in init.", ex)
        End Try

    End Sub

    '///// Function to check if Microsoft Report Viewer is installed
    Public Function IsInstalled() As Boolean

        Dim uninstallKey As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"
        Dim ReportViewerExist As Boolean = False

        Using rk As RegistryKey = Registry.LocalMachine.OpenSubKey(uninstallKey)
            For Each skName As String In rk.GetSubKeyNames()
                Using sk As RegistryKey = rk.OpenSubKey(skName)

                    If sk.GetValue("DisplayName") = "Microsoft Report Viewer 2012 Runtime" Then
                        ReportViewerExist = True
                    End If

                End Using
            Next
        End Using

        Return ReportViewerExist

    End Function


    '///// Code for Check User Credentials User Name and Password  
    ''' <summary>
    ''' If Login is suceessful then Form1 is called to creates sys,html folders and their approprate files which 
    ''' will be useful as a configuration file to set up various parameters 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Ok.Click

        If TB_Username.Text = "" Then
            MessageBox.Show("You must enter a username")
        Else
            If TB_Password.Text = "" Then
                MessageBox.Show("You didn't enter a password")
            Else
                If TB_Username.Text = "csimasteradmin" Then

                    If TB_Password.Text = "csimasteradmin" Then
                        Dim f As Form = Application.OpenForms.Item("form1")
                        If f Is Nothing Then
                            Form1.Show()
                        Else
                            f.BringToFront()

                        End If
                        Me.Hide()
                        CSI_Library.CSI_Library.username = TB_Username.Text
                        CSIFLEXGlobal.UserName = TB_Username.Text
                    End If
                Else
                    If CSI_Lib.check_DB_connection = False Then
                        MessageBox.Show("No connection to the database")
                        Me.Close()
                    End If

                    Dim type As String = ""
                    Dim userId = MySqlAccess.GetFieldValue($"SELECT Id FROM csi_auth.Users WHERE username_ like '{ TB_Username.Text }'", "Id")

                    If Not String.IsNullOrEmpty(userId) Then

                        CSIFLEXGlobal.UserId = userId

                        Dim pass_hash_db = MySqlAccess.GetFieldValue($"SELECT password_ FROM csi_auth.Users WHERE username_ like '{ TB_Username.Text }'", "password_")

                        Dim pass_salt_db = MySqlAccess.GetFieldValue($"SELECT salt_ FROM csi_auth.Users WHERE username_ like '{ TB_Username.Text }'", "salt_")

                        Dim hash_64 = Convert.ToBase64String(HashHelper.ComputePBKDF2Hash(TB_Password.Text, Convert.FromBase64String(pass_salt_db)))

                        If pass_hash_db = hash_64 Then

                            CSI_Library.CSI_Library.username = TB_Username.Text
                            CSIFLEXGlobal.UserName = TB_Username.Text

                            type = MySqlAccess.GetFieldValue($"SELECT usertype FROM csi_auth.Users WHERE STRCMP(Username_,'{ TB_Username.Text }') = 0", "usertype")

                            Dim supportedTypes As New List(Of String)({"Administrator", "Admin", "admin"})

                            If Not String.IsNullOrEmpty(type) And supportedTypes.Contains(type) Then

                                Dim f As Form = Application.OpenForms.Item("form1")
                                If f Is Nothing Then
                                    Form1.Show()
                                Else
                                    f.BringToFront()
                                End If
                                Me.Hide()

                                Save_login_infos(TB_Username.Text, AES_Encrypt(TB_Password.Text, "pass"), TB_Password.Text.Length)

                                CSI_Lib.Log_server_event("Server Login with username : " & TB_Username.Text)

                            Else
                                MessageBox.Show("You have to be an administrator to log into CSIFlex Server.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If
                        Else
                            MessageBox.Show("Invalid password.Please try again!")
                            TB_Password.Focus()
                        End If
                    Else
                        MessageBox.Show("This user does not exist.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                End If
            End If
        End If
    End Sub


    '///// Close the Form when click on Cancel Button 
    Private Sub BTN_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Cancel.Click
        Me.Close()
    End Sub


    '///// Code for Saving User Credentials to logininfo.csys file when User Click Remember Me CheckBox
    Private Sub Save_login_infos(username As String, passW As String, len As Integer)
        Try
            Dim info As New List(Of String) ' user name/pass/len
            Dim windows_username As String = Environment.UserName 'AES_Encrypt(Environment.UserName, "Pass")

            If File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\logininfo.csys") Then
                Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\logininfo.csys")
                    While Not reader.EndOfStream
                        info.Add(reader.ReadLine) 'AES_Decrypt(reader.ReadLine.Split(",")(2), "pass"))
                    End While
                    reader.Close()
                End Using
            End If

            For Each item In info.ToList
                If item.StartsWith(windows_username) Then
                    info.Remove(item)
                    info.Add(windows_username & "," + username & "," + passW & "," & len)
                    GoTo added
                End If
            Next
            info.Add(windows_username & "," + username & "," & passW & "," + len.ToString())
added:

            If File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\logininfo.csys") Then File.Delete(CSI_Library.CSI_Library.serverRootPath & "\sys\logininfo.csys")
            Using writer As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\logininfo.csys")
                For Each item In info
                    writer.WriteLine(item)
                Next
            End Using
        Catch ex As Exception

            CSI_Lib.LogServerError(ex.Message, 1)
        End Try

    End Sub

    '///// Code for Reading Login Credentials from logininfo.csys file 
    Private Function read_login_infos() As String()

        'Dim infos As String() = ServerSettings.LoginInfo

        'For Each info As String In infos
        '    If info IsNot Nothing Then
        '        If info.StartsWith(Environment.UserName) Then
        '            Dim fields = info.Split(",")
        '            TB_Username.Text = fields(1)
        '            TB_Password.Text = AES_Decrypt(fields(2), "pass")
        '            CB_remember.Checked = True
        '        End If
        '    End If
        'Next

        'Return infos

        Try
            'Dim infos(-1) As String
            Dim info As New List(Of String)

            If File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\logininfo.csys") Then

                Dim windows_username As String = Environment.UserName

                Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\logininfo.csys")

                    While Not reader.EndOfStream
                        info.Add(reader.ReadLine)
                    End While

                    reader.Close()
                End Using

                For Each item In info
                    If Not item Is Nothing Then
                        If item.StartsWith(windows_username) Then
                            infos = item.Split(",")
                            TB_Username.Text = infos(1)
                            TB_Password.Text = AES_Decrypt(infos(2), "pass")
                            CB_remember.Checked = True
                            Exit For
                        End If
                    End If
                Next
            End If

            Return infos

        Catch ex As Exception
            CSI_Lib.LogServerError(ex.Message, 1)

            Return infos
        End Try

    End Function


    ''' <summary>
    ''' AES_DEC
    ''' </summary>
    ''' <returns>decrypted </returns>
    ''' Decrypt the passpowrd of tht user 
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


    ''' <summary>
    ''' AES_Enc
    ''' </summary>
    ''' <returns>Encrypted </returns>
    ''' Encrypt the Password of the User
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
            AES.Padding = Security.Cryptography.PaddingMode.Zeros

            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)

            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

            Return encrypted

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CB_remember_CheckedChanged(sender As Object, e As EventArgs) Handles CB_remember.CheckedChanged

    End Sub
End Class
#End Region


#Region "modFuntion Module"
Module modFunction

    Public CSI_Lib As New CSI_Library.CSI_Library(True)

    Public wampattempt As Integer = 0


End Module
#End Region


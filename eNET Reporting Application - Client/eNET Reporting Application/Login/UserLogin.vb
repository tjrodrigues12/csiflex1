Imports System.IO
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities
Imports Encryption.Utilities
Imports MySql.Data.MySqlClient

Public Class UserLogin

    Public synchro As Boolean = False
    Public toBeClosed As Boolean = True
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath

    Private strConnection As String = CSIFLEXSettings.Instance.ConnectionString


    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click

        If String.IsNullOrEmpty(txtUsername.Text) Then
            MessageBox.Show("You must enter a username")
            Return
        End If

        If String.IsNullOrEmpty(txtPassword.Text) Then
            MessageBox.Show("You didn't enter a password")
            Return
        End If

        'Log.Info($"Connection - {strConnection}")
        Dim userDB = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.Users WHERE username_ like '{ txtUsername.Text }'", strConnection)

        If userDB.Rows.Count = 0 Then
            MessageBox.Show("This user does not exist.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim pass_salt_db = userDB.Rows(0)("salt_").ToString()
        Dim hash_64 = Convert.ToBase64String(HashHelper.ComputePBKDF2Hash(txtPassword.Text, Convert.FromBase64String(pass_salt_db)))

        If Not userDB.Rows(0)("password_").ToString() = hash_64 Then
            MessageBox.Show("Invalid password. Please try again!", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPassword.Focus()
            Return
        End If

        CSIFLEXSettings.Instance.UserName = txtUsername.Text
        CSIFLEXSettings.Instance.Password = ""

        If chkRemember.Checked Then
            CSIFLEXSettings.Instance.Password = AES_Encrypt(txtPassword.Text, "pass")
        End If
        CSIFLEXSettings.Instance.SaveSettings()

        CSI_Library.CSI_Library.userid = userDB.Rows(0)("id")
        CSI_Library.CSI_Library.username = userDB.Rows(0)("username_")
        CSI_Library.CSI_Library.userEditTimeline = userDB.Rows(0)("EditTimeline")
        CSI_Library.CSI_Library.userEditPartNumbers = userDB.Rows(0)("EditMasterPartData")
        CSI_Library.CSI_Library.userMachines = New List(Of String)()

        MySqlAccess.SetConnectingString(CSIFLEXSettings.Instance.ConnectionString)

        Dim machines = userDB.Rows(0)("machines").ToString().ToUpper()
        Dim userMachines As List(Of String) = New List(Of String)()

        If machines = "ALL" Then

            Dim dbMachines = MySqlAccess.GetDataTable("SELECT id FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1;", strConnection)

            For Each mch As DataRow In dbMachines.Rows
                userMachines.Add(mch("Id"))
            Next
        Else
            userMachines.AddRange(machines.Split(",", 256, StringSplitOptions.RemoveEmptyEntries).Select(Function(s) s.Trim()))
        End If

        'Dim userMachines = userDB.Rows(0)("machines").ToString().Split(",", 256, StringSplitOptions.RemoveEmptyEntries).Select(Function(m) m.Trim()).ToArray()
        CSIFLEXSettings.Machines = New List(Of String)()

        For Each mchId In userMachines
            If MySqlAccess.ExecuteScalar($"SELECT Monstate FROM csi_auth.tbl_ehub_conf WHERE Id = {mchId};").ToString() = "1" Then
                CSI_Library.CSI_Library.userMachines.Add(mchId)
                CSIFLEXSettings.Machines.Add(mchId)
                Log.Info($"--Machine: {mchId}")
            End If
        Next

        'CSI_Library.CSI_Library.userMachines.AddRange(userMachines)

        toBeClosed = False
        DialogResult = DialogResult.OK

        Me.Close()

    End Sub

    Private Sub Save_login_infos(username As String, passW As String, len As Integer)

        Try
            Dim info As New List(Of String)
            Dim windows_username As String = Environment.UserName

            If File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\logininfo.csys") Then

                Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\logininfo.csys")

                    While Not reader.EndOfStream
                        info.Add(AES_Decrypt(reader.ReadLine, "pass"))
                    End While

                    reader.Close()

                End Using

            End If

            For Each item In info.ToList
                If item.StartsWith(windows_username) Then
                    info.Remove(item)
                    info.Add(windows_username + "," + username + "," + passW + "," + len.ToString())
                    GoTo added
                End If
            Next
            If chkRemember.Checked = True Then
                info.Add((windows_username + "," + username + "," + passW + "," + len.ToString()))
            End If

added:

            If chkRemember.Checked = False Then
                For Each item In info.ToList
                    If item.StartsWith(windows_username) And item.Contains(username) Then
                        info.Remove(item)
                    End If
                Next
            End If

            If File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\logininfo.csys") Then File.Delete(CSI_Library.CSI_Library.ClientRootPath & "\sys\logininfo.csys")

            Using writer As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.ClientRootPath & "\sys\logininfo.csys")

                For Each item In info
                    writer.WriteLine(AES_Encrypt(item, "pass"))
                Next

            End Using

        Catch ex As Exception


        End Try

    End Sub

    Private Function read_login_infos() As String

        txtUsername.Text = CSIFLEXSettings.Instance.UserName
        txtPassword.Text = AES_Decrypt(CSIFLEXSettings.Instance.Password, "pass")

        chkRemember.Checked = Not String.IsNullOrEmpty(txtPassword.Text)

        Return txtUsername.Text

    End Function

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        DialogResult = DialogResult.Cancel
        Me.Close()

    End Sub

    ''' <summary>
    ''' AES_DEC
    ''' </summary>
    ''' <returns>decrypted </returns>
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


    Private Sub UserLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Visible = True

        If File.Exists(rootPath & "\sys\CSIServerIP_.csys") Then
            Using reader As StreamReader = New StreamReader(rootPath & "\sys\CSIServerIP_.csys")
                TextBox1.Text = reader.ReadLine()
                reader.Close()
            End Using
        End If

        check_db(sender, e)
        read_login_infos()

    End Sub

    Private Sub check_db(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            MessageBox.Show("CSIFLEX was not able to create the users database") ' : " & ex.Message)
        End Try

    End Sub

    Private Sub SynchDB()

        SynchroDB.ShowDialog()

        'Dim db As String = Environment.CurrentDirectory
        'TwoWayIndirectSync(db & "\MDBSyn.odc", db & "\MDBSyn2.odc")

    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' MOVE FORME 
    '  
    '-----------------------------------------------------------------------------------------------------------------------
#Region "form move"
    Dim _MouseDown As Boolean, _MouseX As Integer, _MouseY As Integer
    Private Sub form3_mouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        _MouseDown = True
        _MouseX = e.X
        _MouseY = e.Y
    End Sub

    Private Sub Form3_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        _MouseDown = False
    End Sub


    Private Sub Form3_Mousemove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove


        If _MouseDown = True Then
            Reporting_application.SuspendLayout()
            Me.Left = Me.Left + (e.X - _MouseX)
            Me.Top = Me.Top + (e.Y - _MouseY)
            If Me.Top < 20 Then Me.Top = 0
            If Me.Left < 20 Then Me.Left = 0
            Reporting_application.ResumeLayout(True)

        End If

    End Sub
#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub UserLogin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If (toBeClosed = True) Then
            Environment.Exit(0)
        End If
    End Sub

End Class












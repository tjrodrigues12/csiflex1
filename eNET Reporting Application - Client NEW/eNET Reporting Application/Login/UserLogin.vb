Imports System.IO
Imports Encryption.Utilities
Imports MySql.Data.MySqlClient

Public Class UserLogin
    Public synchro As Boolean = False
    Public tobeclosed As Boolean = True
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_ok.Click

        If UsernameTextBox.Text = "" Then
            MessageBox.Show("You must enter a username")
        Else
            If PasswordTextBox.Text = "" Then
                MessageBox.Show("You didn't enter a password")
            Else
                Dim type As String = ""
                Dim pass_hash_db = GetFieldValue("SELECT password_ FROM csi_auth.Users WHERE username_ like'" & UsernameTextBox.Text & "'", "password_")
                If Not String.IsNullOrEmpty(pass_hash_db) Then
                    Dim pass_salt_db = GetFieldValue("SELECT salt_ FROM csi_auth.Users WHERE username_ like'" & UsernameTextBox.Text & "'", "salt_")
                    Dim hash_64 = Convert.ToBase64String(HashHelper.ComputePBKDF2Hash(PasswordTextBox.Text, Convert.FromBase64String(pass_salt_db)))
                    If pass_hash_db = hash_64 Then
                        type = GetFieldValue("SELECT usertype FROM csi_auth.Users WHERE Username_='" & UsernameTextBox.Text & "'", "usertype")
                        Dim supportedTypes As New List(Of String)({"admin", "user", "Programer", "operator", "supervisor"})
                        If Not String.IsNullOrEmpty(type) And supportedTypes.Contains(type) Then
                            tobeclosed = False
                            'createMonListFromServer(UsernameTextBox.Text)
                            Dim f As Form = Application.OpenForms.Item("form1")
                            Reporting_application.username_ = UsernameTextBox.Text
                            If f Is Nothing Then
                                Me.Visible = True
                                Reporting_application.Show()
                            Else
                                f.BringToFront()
                            End If
                            Me.Hide()
                            Save_login_infos(UsernameTextBox.Text, AES_Encrypt(PasswordTextBox.Text, "pass"), PasswordTextBox.Text.Length)
                        Else
                            MessageBox.Show("You are not allowed to log into CSIFlex Client", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tobeclosed = True
                        End If
                    Else
                        MessageBox.Show("Invalid password. Please try again!", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        PasswordTextBox.Focus()
                    End If
                Else
                    MessageBox.Show("This user does not exist.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        End If

    End Sub

    Private Sub Save_login_infos(username As String, passW As String, len As Integer)
        Try
            Dim info As New List(Of String) ' user name/pass/len
            Dim windows_username As String = Environment.UserName 'AES_Encrypt(Environment.UserName, "Pass")

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
            If CB_remember.Checked = True Then
                info.Add((windows_username + "," + username + "," + passW + "," + len.ToString()))
            End If

added:

            If CB_remember.Checked = False Then
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
                'writer.WriteLine(username + "," + passW + "," + len)
                'writer.Close()
            End Using
        Catch ex As Exception

            ' CSI_Library.LogServerError(ex.Message, 1)
        End Try

    End Sub
    Private Function read_login_infos() As String()
        Dim infos() As String
        Try

            Dim info As New List(Of String)

            If File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\logininfo.csys") Then

                Dim windows_username As String = Environment.UserName

                Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\logininfo.csys")
                    info.Add(AES_Decrypt(reader.ReadLine, "pass"))
                    'infos = reader.ReadLine.Split(",")
                    reader.Close()
                End Using

                For Each item In info
                    If item.StartsWith(windows_username) Then
                        infos = item.Split(",")
                        UsernameTextBox.Text = infos(1)
                        PasswordTextBox.Text = AES_Decrypt(infos(2), "pass")
                        CB_remember.Checked = True
                        Exit For
                    End If
                Next

            End If
            Return infos
        Catch ex As Exception
            '  CSI_Library.CSI_Library.cli.(ex.Message, 1)

            Return infos
        End Try
    End Function
    Private Sub createMonListFromServer(userName As String)

        Dim machines As String = GetFieldValue("SELECT Username_, machines FROM Users WHERE Username_='" & UsernameTextBox.Text & "'", "machines")
        Dim listOfMachines() As String = machines.Split(",")

        If (File.Exists(Application.StartupPath.ToString() + "/sys/Monlist.csys")) Then
            'MsgBox("avant kill Monlist.csys")
            Kill(Application.StartupPath.ToString() + "/sys/Monlist.csys")
            'MsgBox("apres kill Monlist.csys")
        End If

        Using writer As New StreamWriter(Application.StartupPath.ToString() + "/sys/Monlist.csys")
            For Each mach As String In listOfMachines
                writer.WriteLine(mach)
            Next
        End Using


    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        '  MessageBox.Show("we are in cancel")
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
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    '9 octobre a 8h
    '6r
    'attestation ecole
    '27$
    'moto + equipement
    '14S


    Private Sub UserLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = True
        'Me.Height = 209
        If File.Exists(rootPath & "\sys\CSIServerIP_.csys") Then
            Using reader As StreamReader = New StreamReader(rootPath & "\sys\CSIServerIP_.csys")
                TextBox1.Text = reader.ReadLine()
                reader.Close()
            End Using
        End If

        check_db(sender, e)
        read_login_infos()

        '   If tobeclosed = True Then Me.Close()
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
        If (tobeclosed = True) Then
            Environment.Exit(0)
        End If
    End Sub

    Public Function GetFieldValue(ByVal srcSQL As String, ByVal strField As String) As String
        'create connection
        GetFieldValue = Nothing
        Try

            Dim db_authPath As String = Nothing

            If (File.Exists(rootPath + "\sys\SrvDBpath.csys")) Then
                Using reader As New StreamReader(rootPath + "\sys\SrvDBpath.csys")
                    db_authPath = reader.ReadLine()
                End Using
            End If
            If Not (db_authPath = Nothing) Then
                Dim server = db_authPath
                'Dim database = "csi_auth"
                'Dim uid = "client"
                'Dim password = "csiflex123"
                'Dim port = "3306"
                Dim connectionString As String
                connectionString = "SERVER=" + server + ";" + "DATABASE=csi_auth;" + CSI_Library.CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"


                Dim mysqlcnt As MySqlConnection = New MySqlConnection(connectionString)


                With mysqlcnt
                    .Open()
                    If .State = ConnectionState.Open Then
                        Dim cmd As MySqlCommand = New MySqlCommand(srcSQL, mysqlcnt)
                        'create data reader
                        Dim rdr As MySqlDataReader = cmd.ExecuteReader
                        'loop through result set
                        While (rdr.Read)
                            GetFieldValue = rdr(strField).ToString()
                        End While
                        'close data reader
                        rdr.Close()
                        ' Close connection
                        .Close()
                    End If
                End With
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to connect. Maybe the port 3306 is blocked")
            GetFieldValue = Nothing
            Return GetFieldValue
        End Try
        ' GetFieldValue = Nothing

        Return GetFieldValue
    End Function
End Class












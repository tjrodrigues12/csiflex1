Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities
Imports MySql.Data.MySqlClient

Public Class InitialSetup2

    Public Sub New()
        InitializeComponent()
    End Sub

    Dim currentUsername As String = ""
    Dim currentUser As WelcomeUser
    Dim users As List(Of WelcomeUser)
    Dim source As BindingSource
    Dim editMode As EditAction = EditAction.NoAction

    Private Sub InitialSetup2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        users = New List(Of WelcomeUser)
        source = New BindingSource()

        Dim dtUsers = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.users WHERE USERTYPE = 'User';")

        For Each row As DataRow In dtUsers.Rows
            users.Add(New WelcomeUser(row("username_"), row("firstname_"), row("name_"), row("email_")))
        Next

        source.DataSource = users
        dgridUsers.DataSource = source

        rdbDefault.Checked = True
        rdbCustom.Checked = False

    End Sub

    Private Sub InitialSetup2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If dgridUsers.Rows.Count > 0 Then

            Dim emails As String = ""

            For Each row As DataGridViewRow In dgridUsers.Rows
                emails = $"{emails};{row.Cells("Email").Value}"
            Next

            If emails.StartsWith(";") Then emails = emails.Substring(1)

            Dim sqlCommand As New MySqlCommand()

            Try
                sqlCommand.Parameters.AddWithValue("@Task_name", "AvailabilityReport")
                sqlCommand.Parameters.AddWithValue("@Day_", "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday")
                sqlCommand.Parameters.AddWithValue("@Time_", "08:00")
                sqlCommand.Parameters.AddWithValue("@ReportType", "Availability")
                sqlCommand.Parameters.AddWithValue("@ReportTitle", "")
                sqlCommand.Parameters.AddWithValue("@ReportPeriod", "Yesterday")
                sqlCommand.Parameters.AddWithValue("@Output_Folder", "C:\CSIFLEX\Reports")
                sqlCommand.Parameters.AddWithValue("@MachineToReport", "ALL")
                sqlCommand.Parameters.AddWithValue("@MailTo", emails)
                sqlCommand.Parameters.AddWithValue("@done", "")
                sqlCommand.Parameters.AddWithValue("@dayback", 7)
                sqlCommand.Parameters.AddWithValue("@timeback", "0-6")
                sqlCommand.Parameters.AddWithValue("@CustomMsg", "")
                sqlCommand.Parameters.AddWithValue("@Scale", "Hours")
                sqlCommand.Parameters.AddWithValue("@Production", 1)
                sqlCommand.Parameters.AddWithValue("@Setup", 1)
                sqlCommand.Parameters.AddWithValue("@OnlySummary", 0)
                sqlCommand.Parameters.AddWithValue("@Sorting", "Value")
                sqlCommand.Parameters.AddWithValue("@EventMinMinutes", 0)
                sqlCommand.Parameters.AddWithValue("@Enabled", 1)
                sqlCommand.Parameters.AddWithValue("@shift_number", "1,2,3")
                sqlCommand.Parameters.AddWithValue("@shift_starttime", "00:01")
                sqlCommand.Parameters.AddWithValue("@shift_endtime", "24:00")
                sqlCommand.Parameters.AddWithValue("@short_filename", "True")

                generateReports(sqlCommand)

            Catch ex As Exception
                Log.Error("Availability Report", ex)
            End Try

            sqlCommand = New MySqlCommand()

            Try
                sqlCommand.Parameters.AddWithValue("@Task_name", "DowntimeReport")
                sqlCommand.Parameters.AddWithValue("@Day_", "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday")
                sqlCommand.Parameters.AddWithValue("@Time_", "08:00")
                sqlCommand.Parameters.AddWithValue("@ReportType", "Downtime")
                sqlCommand.Parameters.AddWithValue("@ReportTitle", "")
                sqlCommand.Parameters.AddWithValue("@ReportPeriod", "Yesterday")
                sqlCommand.Parameters.AddWithValue("@Output_Folder", "C:\CSIFLEX\Reports")
                sqlCommand.Parameters.AddWithValue("@MachineToReport", "ALL")
                sqlCommand.Parameters.AddWithValue("@MailTo", emails)
                sqlCommand.Parameters.AddWithValue("@done", "")
                sqlCommand.Parameters.AddWithValue("@dayback", 7)
                sqlCommand.Parameters.AddWithValue("@timeback", "0-6")
                sqlCommand.Parameters.AddWithValue("@CustomMsg", "")
                sqlCommand.Parameters.AddWithValue("@Scale", "Hours")
                sqlCommand.Parameters.AddWithValue("@Production", 1)
                sqlCommand.Parameters.AddWithValue("@Setup", 1)
                sqlCommand.Parameters.AddWithValue("@OnlySummary", 0)
                sqlCommand.Parameters.AddWithValue("@Sorting", "Value")
                sqlCommand.Parameters.AddWithValue("@EventMinMinutes", 0)
                sqlCommand.Parameters.AddWithValue("@Enabled", 1)
                sqlCommand.Parameters.AddWithValue("@shift_number", "1,2,3")
                sqlCommand.Parameters.AddWithValue("@shift_starttime", "00:01")
                sqlCommand.Parameters.AddWithValue("@shift_endtime", "24:00")
                sqlCommand.Parameters.AddWithValue("@short_filename", "True")

                generateReports(sqlCommand)

            Catch ex As Exception
                Log.Error("Downtime Report", ex)
            End Try

        End If

        DialogResult = DialogResult.OK

    End Sub

    Private Sub dgridUsers_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgridUsers.CellDoubleClick

        If e.RowIndex < 0 Or editMode <> EditAction.NoAction Then Return

        editMode = EditAction.Update

        Dim row As DataGridViewRow = dgridUsers.Rows(e.RowIndex)

        currentUser = New WelcomeUser(
            row.Cells("Username").Value,
            row.Cells("FirstName").Value,
            row.Cells("LastName").Value,
            row.Cells("Email").Value)

        currentUsername = currentUser.Username

        frmUserReadOnly(False)
        txtFirstName.Text = currentUser.FirstName
        txtLastName.Text = currentUser.LastName
        txtEmail.Text = currentUser.Email

        btnAddUser.Enabled = False
        btnAddUser.Image = My.Resources.Resources.icons8_add_256

        btnConfirmUser.Enabled = True
        btnConfirmUser.Image = My.Resources.Resources.icons8_check_circle_40_2
        Me.ToolTip1.SetToolTip(Me.btnConfirmUser, "Confirm update of user")

        btnCancelUser.Enabled = True
        btnCancelUser.Image = My.Resources.Resources.icons8_cancel_40_2
        Me.ToolTip1.SetToolTip(Me.btnCancelUser, "Cancel update of user")

        txtFirstName.Select()

    End Sub

    Private Sub dgridUsers_SelectionChanged(sender As Object, e As EventArgs) Handles dgridUsers.SelectionChanged

        If dgridUsers.SelectedCells.Count <= 0 Or editMode <> EditAction.NoAction Then
            Return
        End If

        Dim row As DataGridViewRow = dgridUsers.Rows(dgridUsers.SelectedCells(0).RowIndex)

        currentUser = New WelcomeUser(
            row.Cells("Username").Value,
            row.Cells("FirstName").Value,
            row.Cells("LastName").Value,
            row.Cells("Email").Value)

        currentUsername = currentUser.Username

        txtFirstName.Text = currentUser.FirstName
        txtLastName.Text = currentUser.LastName
        txtEmail.Text = currentUser.Email

        btnCancelUser.Enabled = True
        btnCancelUser.Image = My.Resources.Resources.icons8_cancel_40_2
        Me.ToolTip1.SetToolTip(Me.btnCancelUser, "Delete user")

    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click

        editMode = EditAction.Insert

        currentUsername = ""
        txtFirstName.Clear()
        txtLastName.Clear()
        txtEmail.Clear()

        frmUserReadOnly(False)

        btnAddUser.Enabled = False
        btnAddUser.Image = My.Resources.Resources.icons8_add_256

        btnConfirmUser.Enabled = True
        btnConfirmUser.Image = My.Resources.Resources.icons8_check_circle_40_2
        Me.ToolTip1.SetToolTip(Me.btnConfirmUser, "Confirme New User")

        btnCancelUser.Enabled = True
        btnCancelUser.Image = My.Resources.Resources.icons8_cancel_40_2
        Me.ToolTip1.SetToolTip(Me.btnCancelUser, "Cancel New User")

        txtFirstName.Select()

    End Sub

    Private Sub btnConfirmUser_Click(sender As Object, e As EventArgs) Handles btnConfirmUser.Click

        If String.IsNullOrEmpty(txtFirstName.Text) Or String.IsNullOrEmpty(txtLastName.Text) Or String.IsNullOrEmpty(txtEmail.Text) Then
            MessageBox.Show("You must complete all fields!", "Missing data")
            Return
        End If

        If Not Util.IsValidEmail(txtEmail.Text) Then
            MessageBox.Show("Email is not valid!", "Missing data")
            Return
        End If

        If String.IsNullOrEmpty(currentUsername) Then
            currentUsername = txtEmail.Text.Substring(0, txtEmail.Text.IndexOf("@"))
            currentUser = New WelcomeUser(currentUsername, txtFirstName.Text, txtLastName.Text, txtEmail.Text)
            users.Add(currentUser)
        Else
            currentUser = users.FirstOrDefault(Function(u) u.Username = currentUsername)
            currentUser.FirstName = txtFirstName.Text
            currentUser.LastName = txtLastName.Text
            currentUser.Email = txtEmail.Text
        End If

        Dim password = ""
        Dim salt = ""

        Dim sqlCmd As New Text.StringBuilder()

        sqlCmd.Append($"INSERT INTO csi_auth.users ")
        sqlCmd.Append($"  (                        ")
        sqlCmd.Append($"    username_       ,      ")
        sqlCmd.Append($"    name_           ,      ")
        sqlCmd.Append($"    firstname_      ,      ")
        sqlCmd.Append($"    displayname     ,      ")
        sqlCmd.Append($"    password_       ,      ")
        sqlCmd.Append($"    salt_           ,      ")
        sqlCmd.Append($"    email_          ,      ")
        sqlCmd.Append($"    usertype        ,      ")
        sqlCmd.Append($"    machines               ")
        sqlCmd.Append($"  )                        ")
        sqlCmd.Append($"  VALUES                   ")
        sqlCmd.Append($"  (                        ")
        sqlCmd.Append($"    '{currentUsername}'                              , ")
        sqlCmd.Append($"    '{currentUser.LastName}'                         , ")
        sqlCmd.Append($"    '{currentUser.FirstName}'                        , ")
        sqlCmd.Append($"    '{currentUser.LastName}, {currentUser.FirstName}', ")
        sqlCmd.Append($"    '{password}'                                     , ")
        sqlCmd.Append($"    '{salt}'                                         , ")
        sqlCmd.Append($"    '{currentUser.Email}'                            , ")
        sqlCmd.Append($"    'User'                                           , ")
        sqlCmd.Append($"    'ALL'                                              ")
        sqlCmd.Append($"  )                                                    ")
        sqlCmd.Append($"  ON DUPLICATE KEY UPDATE                              ")
        sqlCmd.Append($"    name_      = '{currentUser.LastName}'            , ")
        sqlCmd.Append($"    firstname_ = '{currentUser.FirstName}'           , ")
        sqlCmd.Append($"    email_     = '{currentUser.Email}'                 ")
        MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        source.ResetBindings(False)

        btnAddUser.Enabled = (users.Count < 5)

        frmUserClear()

    End Sub

    Private Sub btnCancelUser_Click(sender As Object, e As EventArgs) Handles btnCancelUser.Click

        If editMode = EditAction.NoAction Then

            If Not MessageBox.Show("Do you confirme the exclusion of this user?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) = DialogResult.Yes Then
                Return
            End If

            Dim row As DataGridViewRow = dgridUsers.Rows(dgridUsers.SelectedCells(0).RowIndex)
            Dim username = row.Cells("Username").Value

            MySqlAccess.ExecuteNonQuery($"DELETE FROM csi_auth.users WHERE username_ = '{username}'")

            currentUser = users.FirstOrDefault(Function(u) u.Username = currentUsername)
            users.Remove(currentUser)

            source.ResetBindings(False)

        End If
        frmUserClear()

    End Sub

    Private Sub frmUserClear()

        editMode = EditAction.NoAction

        currentUsername = ""
        txtFirstName.Clear()
        txtLastName.Clear()
        txtEmail.Clear()

        frmUserReadOnly(True)

        btnAddUser.Enabled = True
        btnAddUser.Image = My.Resources.Resources.icons8_add_256_2

        btnConfirmUser.Enabled = False
        btnConfirmUser.Image = My.Resources.Resources.icons8_check_circle_40
        Me.ToolTip1.SetToolTip(Me.btnConfirmUser, "")

        btnCancelUser.Enabled = False
        btnCancelUser.Image = My.Resources.Resources.icons8_cancel_40
        Me.ToolTip1.SetToolTip(Me.btnCancelUser, "")

        dgridUsers.Select()

    End Sub

    Private Sub frmUserReadOnly(isReadOnly As Boolean)
        txtFirstName.ReadOnly = isReadOnly
        txtLastName.ReadOnly = isReadOnly
        txtEmail.ReadOnly = isReadOnly
    End Sub


    Dim customSmtpHost = ""
    Dim customSmtpPort = 587
    Dim customSender = ""
    Dim customPwd = ""
    Dim customSSL = True
    Dim customAuth = False

    Private Sub rdbEmailServer_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDefault.CheckedChanged

        If rdbDefault.Checked Then
            customSmtpHost = txtSMTPHost.Text
            customSmtpPort = txtSMTPPort.Text
            customSender = txtSenderEmail.Text
            customPwd = txtSenderPwd.Text
            customSSL = ckbUseSSL.Checked
            customAuth = ckbAuthentication.Checked

            txtSMTPHost.ReadOnly = True
            txtSMTPPort.ReadOnly = True
            txtSenderEmail.ReadOnly = True
            txtSenderPwd.ReadOnly = True
            ckbUseSSL.Enabled = False
            ckbAuthentication.Enabled = False

            txtSMTPHost.Text = "smtp.gmail.com"
            txtSMTPPort.Text = "587"
            txtSenderEmail.Text = "reports@csiflex.com"
            ckbUseSSL.Checked = True
            ckbAuthentication.Checked = True
        Else
            txtSMTPHost.ReadOnly = False
            txtSMTPPort.ReadOnly = False
            txtSenderEmail.ReadOnly = False
            txtSenderPwd.ReadOnly = True
            ckbUseSSL.Enabled = True
            ckbAuthentication.Enabled = True

            txtSMTPHost.Text = customSmtpHost
            txtSMTPPort.Text = customSmtpPort
            txtSenderEmail.Text = customSender
            txtSenderPwd.ReadOnly = Not customAuth
            txtSenderPwd.Text = customPwd
            ckbUseSSL.Checked = customSSL
            ckbAuthentication.Checked = customAuth
        End If

    End Sub

    Private Sub ckbAuthentication_CheckedChanged(sender As Object, e As EventArgs) Handles ckbAuthentication.CheckedChanged
        txtSenderPwd.Clear()

        txtSenderPwd.ReadOnly = Not (rdbCustom.Checked And ckbAuthentication.Checked)

    End Sub

    Private Sub btnSaveEmail_Click(sender As Object, e As EventArgs) Handles btnSaveEmail.Click

        Dim sqlCmd = New Text.StringBuilder()

        sqlCmd.Append("UPDATE CSI_auth.tbl_emailreports SET isused = 0; ")

        If rdbDefault.Checked Then
            sqlCmd.Append("UPDATE CSI_auth.tbl_emailreports SET isused = 1 WHERE id = 1; ")
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
            sqlCmd.Append("       id          = 2           ; ")

            Dim mySqlCmd As New MySqlCommand()
            mySqlCmd.Parameters.AddWithValue("@senderemail", txtSenderEmail.Text)
            mySqlCmd.Parameters.AddWithValue("@smtphost", txtSMTPHost.Text)
            mySqlCmd.Parameters.AddWithValue("@smtpport", txtSMTPPort.Text)
            mySqlCmd.Parameters.AddWithValue("@requirecred", ckbAuthentication.Checked)
            mySqlCmd.Parameters.AddWithValue("@usessl", ckbUseSSL.Checked)

            If ckbAuthentication.Checked And Not String.IsNullOrEmpty(txtSenderPwd.Text) Then

                sqlCmd.Append("UPDATE                        ")
                sqlCmd.Append("   CSI_auth.tbl_emailreports  ")
                sqlCmd.Append(" SET                          ")
                sqlCmd.Append("       senderpwd = @senderpwd ")
                sqlCmd.Append(" WHERE                        ")
                sqlCmd.Append("       id        = 2        ; ")

                Dim encryptedPass = Cryptography.AES_Encrypt(txtSenderPwd.Text, "pass")
                mySqlCmd.Parameters.AddWithValue("@senderpwd", encryptedPass)

            End If

            mySqlCmd.CommandText = sqlCmd.ToString()
            MySqlAccess.ExecuteNonQuery(mySqlCmd)

        End If

    End Sub

    Private Sub btnSendEmail_Click(sender As Object, e As EventArgs) Handles btnSendEmail.Click

        If (String.IsNullOrEmpty(txtToEmailTest.Text)) Then
            MessageBox.Show("You must inform the destination of the email test.")
            Return
        End If

        If (rdbCustom.Checked And ckbAuthentication.Checked And String.IsNullOrEmpty(txtSenderPwd.Text)) Then
            MessageBox.Show("You must inform the password of SMTP account.")
            Return
        End If

        Dim pwd = "t4Solutions"
        If (rdbCustom.Checked) Then pwd = txtSenderPwd.Text

        Dim emailSetting = New EmailSettings()
        emailSetting.From = txtSenderEmail.Text
        emailSetting.To = txtToEmailTest.Text
        emailSetting.Subject = "CSIFLEX Email Notificatinon Test"
        emailSetting.Message.Add("This is a test email sent from CSIFLEX")
        emailSetting.Message.Add("This is a test email sent from CSIFLEX")
        emailSetting.Message.Add("This is a test email sent from CSIFLEX")
        emailSetting.Host = txtSMTPHost.Text
        emailSetting.Port = Integer.Parse(txtSMTPPort.Text)
        emailSetting.SSL = ckbUseSSL.Checked
        emailSetting.Authentication = ckbAuthentication.Checked
        emailSetting.Password = pwd

        Try
            EmailTools.SendEmail(emailSetting)
            MessageBox.Show($"There message was sent.")
        Catch ex As Exception
            MessageBox.Show($"Could not send a test mail." + vbCrLf + vbCrLf + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        Me.Close()

    End Sub

    Private Sub generateReports(sqlCommand As MySqlCommand)

        Dim sqlCmd As New Text.StringBuilder()
        sqlCmd.Append($"INSERT INTO csi_auth.auto_report_config  ")
        sqlCmd.Append($" (                                       ")
        sqlCmd.Append($"    Task_name        ,                   ")
        sqlCmd.Append($"    Day_             ,                   ")
        sqlCmd.Append($"    Time_            ,                   ")
        sqlCmd.Append($"    ReportType       ,                   ")
        sqlCmd.Append($"    ReportTitle      ,                   ")
        sqlCmd.Append($"    ReportPeriod     ,                   ")
        sqlCmd.Append($"    Output_Folder    ,                   ")
        sqlCmd.Append($"    MachineToReport  ,                   ")
        sqlCmd.Append($"    MailTo           ,                   ")
        sqlCmd.Append($"    done             ,                   ")
        sqlCmd.Append($"    dayback          ,                   ")
        sqlCmd.Append($"    timeback         ,                   ")
        sqlCmd.Append($"    CustomMsg        ,                   ")
        sqlCmd.Append($"    Scale            ,                   ")
        sqlCmd.Append($"    Production       ,                   ")
        sqlCmd.Append($"    Setup            ,                   ")
        sqlCmd.Append($"    OnlySummary      ,                   ")
        sqlCmd.Append($"    Enabled          ,                   ")
        sqlCmd.Append($"    Sorting          ,                   ")
        sqlCmd.Append($"    EventMinMinutes  ,                   ")
        sqlCmd.Append($"    shift_number     ,                   ")
        sqlCmd.Append($"    shift_starttime  ,                   ")
        sqlCmd.Append($"    shift_endtime    ,                   ")
        sqlCmd.Append($"    short_filename                       ")
        sqlCmd.Append($" )                                       ")
        sqlCmd.Append($" VALUES                                  ")
        sqlCmd.Append($" (                                       ")
        sqlCmd.Append($"    @Task_name       ,                   ")
        sqlCmd.Append($"    @Day_            ,                   ")
        sqlCmd.Append($"    @Time_           ,                   ")
        sqlCmd.Append($"    @ReportType      ,                   ")
        sqlCmd.Append($"    @ReportTitle     ,                   ")
        sqlCmd.Append($"    @ReportPeriod    ,                   ")
        sqlCmd.Append($"    @Output_Folder   ,                   ")
        sqlCmd.Append($"    @MachineToReport ,                   ")
        sqlCmd.Append($"    @MailTo          ,                   ")
        sqlCmd.Append($"    @done            ,                   ")
        sqlCmd.Append($"    @dayback         ,                   ")
        sqlCmd.Append($"    @timeback        ,                   ")
        sqlCmd.Append($"    @CustomMsg       ,                   ")
        sqlCmd.Append($"    @Scale           ,                   ")
        sqlCmd.Append($"    @Production      ,                   ")
        sqlCmd.Append($"    @Setup           ,                   ")
        sqlCmd.Append($"    @OnlySummary     ,                   ")
        sqlCmd.Append($"    @Enabled         ,                   ")
        sqlCmd.Append($"    @Sorting         ,                   ")
        sqlCmd.Append($"    @EventMinMinutes ,                   ")
        sqlCmd.Append($"    @shift_number    ,                   ")
        sqlCmd.Append($"    @shift_starttime ,                   ")
        sqlCmd.Append($"    @shift_endtime   ,                   ")
        sqlCmd.Append($"    @short_filename                      ")
        sqlCmd.Append($" )                                       ")
        sqlCmd.Append($" ON DUPLICATE KEY UPDATE                 ")
        sqlCmd.Append($"    Mailto        = @MailTo ;            ")

        sqlCmd.Append($"INSERT IGNORE INTO                       ")
        sqlCmd.Append($"        csi_auth.auto_report_status      ")
        sqlCmd.Append($" (                                       ")
        sqlCmd.Append($"    ReportId         ,                   ")
        sqlCmd.Append($"    Status                               ")
        sqlCmd.Append($" )                                       ")
        sqlCmd.Append($" VALUES                                  ")
        sqlCmd.Append($" (                                       ")
        sqlCmd.Append($"    LAST_INSERT_ID(),                    ")
        sqlCmd.Append($"    'Pending'                            ")
        sqlCmd.Append($" );                                      ")

        sqlCommand.CommandText = sqlCmd.ToString()

        Try
            MySqlAccess.ExecuteNonQuery(sqlCommand)
        Catch ex As Exception
            Log.Error(ex)
        End Try

    End Sub


    Enum EditAction
        NoAction
        Insert
        Update
        Delete
    End Enum

End Class


Public Class WelcomeUser

    Public Sub New()

    End Sub

    Public Sub New(username As String, firstName As String, lastName As String, email As String)
        Me.Username = username
        Me.FirstName = firstName
        Me.LastName = lastName
        Me.Email = email
    End Sub

    Private _username As String
    Public Property Username() As String
        Get
            Return _username
        End Get
        Set(ByVal value As String)
            _username = value
        End Set
    End Property

    Private _firstName As String
    Public Property FirstName() As String
        Get
            Return _firstName
        End Get
        Set(ByVal value As String)
            _firstName = value
        End Set
    End Property

    Private _lastName As String
    Public Property LastName() As String
        Get
            Return _lastName
        End Get
        Set(ByVal value As String)
            _lastName = value
        End Set
    End Property

    Private _email As String
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property
End Class


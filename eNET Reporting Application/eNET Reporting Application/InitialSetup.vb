Imports System.ComponentModel
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities

Public Class frmInitialSetup

    Public Sub New()
        InitializeComponent()
    End Sub


    Dim currentUsername As String = ""
    Dim currentUser As WelcomeUser
    Dim users As List(Of WelcomeUser)
    Dim source As BindingSource


    Private Sub frmInitialSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        users = New List(Of WelcomeUser)
        source = New BindingSource()

        Dim dtUsers = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.users WHERE USERTYPE = 'User';")

        For Each row As DataRow In dtUsers.Rows
            users.Add(New WelcomeUser(row("username_"), row("firstname_"), row("name_"), row("email_")))
        Next

        source.DataSource = users
        dgridUsers.DataSource = source
        txtFirstName.Select()
    End Sub


    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click

        currentUsername = ""

        txtFirstName.Clear()
        txtLastName.Clear()
        txtEmail.Clear()

        txtFirstName.Select()

    End Sub


    Private Sub frmInitialSetup_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If dgridUsers.Rows.Count > 0 Then
            generateReports()
        End If

        DialogResult = DialogResult.OK

    End Sub


    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click

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

    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If dgridUsers.Rows.Count = 0 Then
            txtFirstName.Clear()
            txtLastName.Clear()
            txtEmail.Clear()
            txtFirstName.Select()
            Return
        End If

        dgridUsers_SelectionChanged(dgridUsers, New EventArgs())
        txtFirstName.Select()
    End Sub


    Private Sub dgridUsers_SelectionChanged(sender As Object, e As EventArgs) Handles dgridUsers.SelectionChanged

        Dim grid As DataGridView = sender

        If grid.SelectedCells.Count <= 0 Then
            Return
        End If

        Dim row As DataGridViewRow = grid.Rows(grid.SelectedCells(0).RowIndex)

        currentUser = New WelcomeUser(
            row.Cells("Username").Value,
            row.Cells("FirstName").Value,
            row.Cells("LastName").Value,
            row.Cells("Email").Value)

        currentUsername = currentUser.Username

        txtFirstName.Text = currentUser.FirstName
        txtLastName.Text = currentUser.LastName
        txtEmail.Text = currentUser.Email
        txtFirstName.Select()
    End Sub


    Private Sub generateReports()

        Dim emails As String = ""

        For Each row As DataGridViewRow In dgridUsers.Rows
            emails = $"{emails};{row.Cells("Email").Value}"
        Next

        If emails.StartsWith(";") Then emails = emails.Substring(1)

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
        sqlCmd.Append($" );                                      ")

        sqlCmd.Append($"INSERT INTO csi_auth.auto_report_status  ")
        sqlCmd.Append($" (                                       ")
        sqlCmd.Append($"    ReportId         ,                   ")
        sqlCmd.Append($"    Status                               ")
        sqlCmd.Append($" )                                       ")
        sqlCmd.Append($" VALUES                                  ")
        sqlCmd.Append($" (                                       ")
        sqlCmd.Append($"    LAST_INSERT_ID(),                    ")
        sqlCmd.Append($"    'Pending'                            ")
        sqlCmd.Append($" );                                      ")


    End Sub

End Class


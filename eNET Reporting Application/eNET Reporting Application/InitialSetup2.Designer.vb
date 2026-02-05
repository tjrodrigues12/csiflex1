<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitialSetup2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InitialSetup2))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnAddUser = New System.Windows.Forms.PictureBox()
        Me.btnCancelUser = New System.Windows.Forms.PictureBox()
        Me.btnConfirmUser = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.dgridUsers = New System.Windows.Forms.DataGridView()
        Me.username = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.firstname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lastname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.email = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnSaveEmail = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnSendEmail = New System.Windows.Forms.Button()
        Me.txtToEmailTest = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ckbUseSSL = New System.Windows.Forms.CheckBox()
        Me.ckbAuthentication = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSenderPwd = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtSenderEmail = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSMTPPort = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSMTPHost = New System.Windows.Forms.TextBox()
        Me.rdbCustom = New System.Windows.Forms.RadioButton()
        Me.rdbDefault = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.btnAddUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancelUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnConfirmUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgridUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.csiflex_logo_blue
        Me.PictureBox1.Location = New System.Drawing.Point(4, 12)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(300, 81)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(311, 10)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(761, 46)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Welcome"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnAddUser)
        Me.GroupBox1.Controls.Add(Me.btnCancelUser)
        Me.GroupBox1.Controls.Add(Me.btnConfirmUser)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.txtLastName)
        Me.GroupBox1.Controls.Add(Me.txtFirstName)
        Me.GroupBox1.Controls.Add(Me.dgridUsers)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(311, 45)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(761, 264)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'btnAddUser
        '
        Me.btnAddUser.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddUser.Image = Global.CSI_Reporting_Application.My.Resources.Resources.icons8_add_256_2
        Me.btnAddUser.Location = New System.Drawing.Point(725, 16)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(30, 30)
        Me.btnAddUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnAddUser.TabIndex = 12
        Me.btnAddUser.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnAddUser, "Add user")
        '
        'btnCancelUser
        '
        Me.btnCancelUser.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelUser.Enabled = False
        Me.btnCancelUser.Image = Global.CSI_Reporting_Application.My.Resources.Resources.icons8_cancel_40
        Me.btnCancelUser.Location = New System.Drawing.Point(725, 222)
        Me.btnCancelUser.Name = "btnCancelUser"
        Me.btnCancelUser.Size = New System.Drawing.Size(30, 30)
        Me.btnCancelUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnCancelUser.TabIndex = 11
        Me.btnCancelUser.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnCancelUser, "Delete user")
        '
        'btnConfirmUser
        '
        Me.btnConfirmUser.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConfirmUser.Enabled = False
        Me.btnConfirmUser.Image = Global.CSI_Reporting_Application.My.Resources.Resources.icons8_check_circle_40
        Me.btnConfirmUser.Location = New System.Drawing.Point(689, 222)
        Me.btnConfirmUser.Name = "btnConfirmUser"
        Me.btnConfirmUser.Size = New System.Drawing.Size(30, 30)
        Me.btnConfirmUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnConfirmUser.TabIndex = 10
        Me.btnConfirmUser.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnConfirmUser, "Confirm new user or update")
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(417, 199)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(204, 21)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Email"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(211, 199)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(204, 21)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Last Name"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(5, 199)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(204, 21)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "First Name"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(421, 223)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReadOnly = True
        Me.txtEmail.Size = New System.Drawing.Size(262, 29)
        Me.txtEmail.TabIndex = 6
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(215, 223)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.ReadOnly = True
        Me.txtLastName.Size = New System.Drawing.Size(200, 29)
        Me.txtLastName.TabIndex = 5
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(9, 223)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.ReadOnly = True
        Me.txtFirstName.Size = New System.Drawing.Size(200, 29)
        Me.txtFirstName.TabIndex = 4
        '
        'dgridUsers
        '
        Me.dgridUsers.AllowUserToAddRows = False
        Me.dgridUsers.AllowUserToDeleteRows = False
        Me.dgridUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgridUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgridUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgridUsers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.username, Me.firstname, Me.lastname, Me.email})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgridUsers.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgridUsers.Location = New System.Drawing.Point(9, 49)
        Me.dgridUsers.MultiSelect = False
        Me.dgridUsers.Name = "dgridUsers"
        Me.dgridUsers.Size = New System.Drawing.Size(746, 140)
        Me.dgridUsers.TabIndex = 3
        '
        'username
        '
        Me.username.DataPropertyName = "username"
        Me.username.HeaderText = "username"
        Me.username.Name = "username"
        Me.username.Visible = False
        '
        'firstname
        '
        Me.firstname.DataPropertyName = "firstname"
        Me.firstname.HeaderText = "First Name"
        Me.firstname.Name = "firstname"
        Me.firstname.ReadOnly = True
        '
        'lastname
        '
        Me.lastname.DataPropertyName = "lastname"
        Me.lastname.HeaderText = "Last Name"
        Me.lastname.Name = "lastname"
        Me.lastname.ReadOnly = True
        '
        'email
        '
        Me.email.DataPropertyName = "email"
        Me.email.HeaderText = "Email"
        Me.email.Name = "email"
        Me.email.ReadOnly = True
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 16)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(196, 30)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Users"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.CSI_Reporting_Application.My.Resources.Resources.csiflex_panelv6
        Me.PictureBox2.Location = New System.Drawing.Point(4, 12)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(300, 549)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 3
        Me.PictureBox2.TabStop = False
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(940, 522)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(132, 34)
        Me.btnNext.TabIndex = 4
        Me.btnNext.Text = "Next >>"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.btnSaveEmail)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.ckbUseSSL)
        Me.GroupBox2.Controls.Add(Me.ckbAuthentication)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtSenderPwd)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtSenderEmail)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtSMTPPort)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtSMTPHost)
        Me.GroupBox2.Controls.Add(Me.rdbCustom)
        Me.GroupBox2.Controls.Add(Me.rdbDefault)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(311, 315)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(761, 201)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        '
        'btnSaveEmail
        '
        Me.btnSaveEmail.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveEmail.Location = New System.Drawing.Point(431, 133)
        Me.btnSaveEmail.Name = "btnSaveEmail"
        Me.btnSaveEmail.Size = New System.Drawing.Size(96, 25)
        Me.btnSaveEmail.TabIndex = 19
        Me.btnSaveEmail.Text = "Save"
        Me.btnSaveEmail.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnSendEmail)
        Me.GroupBox3.Controls.Add(Me.txtToEmailTest)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Location = New System.Drawing.Point(533, 51)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(222, 119)
        Me.GroupBox3.TabIndex = 18
        Me.GroupBox3.TabStop = False
        '
        'Button2
        '
        Me.btnSendEmail.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSendEmail.Location = New System.Drawing.Point(11, 82)
        Me.btnSendEmail.Name = "btnSendEmail"
        Me.btnSendEmail.Size = New System.Drawing.Size(200, 25)
        Me.btnSendEmail.TabIndex = 20
        Me.btnSendEmail.Text = "Send"
        Me.btnSendEmail.UseVisualStyleBackColor = True
        '
        'txtToEmailTest
        '
        Me.txtToEmailTest.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToEmailTest.Location = New System.Drawing.Point(11, 46)
        Me.txtToEmailTest.Name = "txtToEmailTest"
        Me.txtToEmailTest.Size = New System.Drawing.Size(200, 25)
        Me.txtToEmailTest.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(6, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(210, 21)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Send test email to"
        '
        'CheckBox2
        '
        Me.ckbUseSSL.AutoSize = True
        Me.ckbUseSSL.Enabled = False
        Me.ckbUseSSL.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbUseSSL.Location = New System.Drawing.Point(9, 164)
        Me.ckbUseSSL.Name = "ckbUseSSL"
        Me.ckbUseSSL.Size = New System.Drawing.Size(73, 21)
        Me.ckbUseSSL.TabIndex = 17
        Me.ckbUseSSL.Text = "Use SSL"
        Me.ckbUseSSL.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.ckbAuthentication.AutoSize = True
        Me.ckbAuthentication.Enabled = False
        Me.ckbAuthentication.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbAuthentication.Location = New System.Drawing.Point(225, 164)
        Me.ckbAuthentication.Name = "ckbAuthentication"
        Me.ckbAuthentication.Size = New System.Drawing.Size(158, 21)
        Me.ckbAuthentication.TabIndex = 16
        Me.ckbAuthentication.Text = "Require Authentication"
        Me.ckbAuthentication.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(222, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(204, 21)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Sender Password"
        '
        'txtSenderPwd
        '
        Me.txtSenderPwd.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSenderPwd.Location = New System.Drawing.Point(225, 133)
        Me.txtSenderPwd.Name = "txtSenderPwd"
        Me.txtSenderPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSenderPwd.ReadOnly = True
        Me.txtSenderPwd.Size = New System.Drawing.Size(200, 25)
        Me.txtSenderPwd.TabIndex = 14
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(222, 51)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(204, 21)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "Sender Email"
        '
        'txtSenderEmail
        '
        Me.txtSenderEmail.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSenderEmail.Location = New System.Drawing.Point(225, 73)
        Me.txtSenderEmail.Name = "txtSenderEmail"
        Me.txtSenderEmail.ReadOnly = True
        Me.txtSenderEmail.Size = New System.Drawing.Size(200, 25)
        Me.txtSenderEmail.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 109)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(204, 21)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "SMTP Port"
        '
        'txtSMTPPort
        '
        Me.txtSMTPPort.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPPort.Location = New System.Drawing.Point(9, 133)
        Me.txtSMTPPort.Name = "txtSMTPPort"
        Me.txtSMTPPort.ReadOnly = True
        Me.txtSMTPPort.Size = New System.Drawing.Size(200, 25)
        Me.txtSMTPPort.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 51)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(204, 21)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "SMTP Host"
        '
        'txtSMTPHost
        '
        Me.txtSMTPHost.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPHost.Location = New System.Drawing.Point(9, 73)
        Me.txtSMTPHost.Name = "txtSMTPHost"
        Me.txtSMTPHost.ReadOnly = True
        Me.txtSMTPHost.Size = New System.Drawing.Size(200, 25)
        Me.txtSMTPHost.TabIndex = 8
        '
        'rdbCustom
        '
        Me.rdbCustom.AutoSize = True
        Me.rdbCustom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbCustom.Location = New System.Drawing.Point(427, 16)
        Me.rdbCustom.Name = "rdbCustom"
        Me.rdbCustom.Size = New System.Drawing.Size(111, 21)
        Me.rdbCustom.TabIndex = 5
        Me.rdbCustom.TabStop = True
        Me.rdbCustom.Text = "Custom Server"
        Me.ToolTip1.SetToolTip(Me.rdbCustom, "Configure your company's email server settings")
        Me.rdbCustom.UseVisualStyleBackColor = True
        '
        'rdbDefault
        '
        Me.rdbDefault.AutoSize = True
        Me.rdbDefault.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbDefault.Location = New System.Drawing.Point(313, 16)
        Me.rdbDefault.Name = "rdbDefault"
        Me.rdbDefault.Size = New System.Drawing.Size(108, 21)
        Me.rdbDefault.TabIndex = 4
        Me.rdbDefault.TabStop = True
        Me.rdbDefault.Text = "Default Server"
        Me.ToolTip1.SetToolTip(Me.rdbDefault, "Use CSIFLEX default email server")
        Me.rdbDefault.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 15)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(252, 30)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Email Server Configuration"
        '
        'InitialSetup2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1084, 564)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InitialSetup2"
        Me.Text = "CSIFLEX Initial Setup"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.btnAddUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancelUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnConfirmUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgridUsers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dgridUsers As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents btnNext As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents username As DataGridViewTextBoxColumn
    Friend WithEvents firstname As DataGridViewTextBoxColumn
    Friend WithEvents lastname As DataGridViewTextBoxColumn
    Friend WithEvents email As DataGridViewTextBoxColumn
    Friend WithEvents btnCancelUser As PictureBox
    Friend WithEvents btnConfirmUser As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents btnAddUser As PictureBox
    Friend WithEvents rdbDefault As RadioButton
    Friend WithEvents rdbCustom As RadioButton
    Friend WithEvents Label8 As Label
    Friend WithEvents txtSMTPPort As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtSMTPHost As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtSenderPwd As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtSenderEmail As TextBox
    Friend WithEvents ckbAuthentication As CheckBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents ckbUseSSL As CheckBox
    Friend WithEvents btnSaveEmail As Button
    Friend WithEvents btnSendEmail As Button
    Friend WithEvents txtToEmailTest As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents ToolTip1 As ToolTip
End Class

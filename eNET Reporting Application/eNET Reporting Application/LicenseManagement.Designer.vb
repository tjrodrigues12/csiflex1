<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LicenseManagement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LicenseManagement))
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.btnTempLicense = New System.Windows.Forms.Button()
        Me.btnRequestLicense = New System.Windows.Forms.Button()
        Me.btnImportLicense = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblCompanyId = New System.Windows.Forms.Label()
        Me.lblComputerId = New System.Windows.Forms.Label()
        Me.lblComputerName = New System.Windows.Forms.Label()
        Me.lblContactPhone = New System.Windows.Forms.Label()
        Me.lblContactEmail = New System.Windows.Forms.Label()
        Me.lblContactName = New System.Windows.Forms.Label()
        Me.lblCompanyName = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.lblServerExpiry = New System.Windows.Forms.Label()
        Me.lblServerLicType = New System.Windows.Forms.Label()
        Me.lblServerStatus = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.chkServer = New System.Windows.Forms.CheckBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.lblFocas = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNumberFocas = New System.Windows.Forms.TextBox()
        Me.chkFocas = New System.Windows.Forms.CheckBox()
        Me.lblFocasExpiry = New System.Windows.Forms.Label()
        Me.lblFocasLicType = New System.Windows.Forms.Label()
        Me.lblFocasStatus = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.grpMobile = New System.Windows.Forms.GroupBox()
        Me.lblMobile = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNumberMobile = New System.Windows.Forms.TextBox()
        Me.chkMobile = New System.Windows.Forms.CheckBox()
        Me.lblMobileExpiry = New System.Windows.Forms.Label()
        Me.lblMobileLicType = New System.Windows.Forms.Label()
        Me.lblMobileStatus = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtContactPhone = New System.Windows.Forms.TextBox()
        Me.txtContactEmail = New System.Windows.Forms.TextBox()
        Me.txtContactPerson = New System.Windows.Forms.TextBox()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.grpMobile.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'pictureBox1
        '
        Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
        Me.pictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(257, 71)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBox1.TabIndex = 8
        Me.pictureBox1.TabStop = False
        '
        'label2
        '
        Me.label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label2.Font = New System.Drawing.Font("Verdana", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(275, 12)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(417, 85)
        Me.label2.TabIndex = 9
        Me.label2.Text = "License Management"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnTempLicense
        '
        Me.btnTempLicense.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTempLicense.Location = New System.Drawing.Point(9, 365)
        Me.btnTempLicense.Name = "btnTempLicense"
        Me.btnTempLicense.Size = New System.Drawing.Size(160, 50)
        Me.btnTempLicense.TabIndex = 0
        Me.btnTempLicense.Text = "Generate a temporary license"
        Me.btnTempLicense.UseVisualStyleBackColor = True
        '
        'btnRequestLicense
        '
        Me.btnRequestLicense.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRequestLicense.Location = New System.Drawing.Point(175, 365)
        Me.btnRequestLicense.Name = "btnRequestLicense"
        Me.btnRequestLicense.Size = New System.Drawing.Size(160, 50)
        Me.btnRequestLicense.TabIndex = 5
        Me.btnRequestLicense.Text = "Request a new license"
        Me.btnRequestLicense.UseVisualStyleBackColor = True
        '
        'btnImportLicense
        '
        Me.btnImportLicense.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportLicense.Location = New System.Drawing.Point(341, 365)
        Me.btnImportLicense.Name = "btnImportLicense"
        Me.btnImportLicense.Size = New System.Drawing.Size(160, 50)
        Me.btnImportLicense.TabIndex = 6
        Me.btnImportLicense.Text = "Import new license file"
        Me.btnImportLicense.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Black
        Me.btnExit.Location = New System.Drawing.Point(507, 365)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 50)
        Me.btnExit.TabIndex = 7
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'lblCompanyId
        '
        Me.lblCompanyId.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyId.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblCompanyId.Location = New System.Drawing.Point(149, 27)
        Me.lblCompanyId.Name = "lblCompanyId"
        Me.lblCompanyId.Size = New System.Drawing.Size(464, 22)
        Me.lblCompanyId.TabIndex = 22
        Me.lblCompanyId.Text = " "
        '
        'lblComputerId
        '
        Me.lblComputerId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComputerId.Location = New System.Drawing.Point(149, 190)
        Me.lblComputerId.Name = "lblComputerId"
        Me.lblComputerId.Size = New System.Drawing.Size(464, 27)
        Me.lblComputerId.TabIndex = 21
        Me.lblComputerId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblComputerName
        '
        Me.lblComputerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComputerName.Location = New System.Drawing.Point(149, 155)
        Me.lblComputerName.Name = "lblComputerName"
        Me.lblComputerName.Size = New System.Drawing.Size(464, 27)
        Me.lblComputerName.TabIndex = 20
        Me.lblComputerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContactPhone
        '
        Me.lblContactPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactPhone.Location = New System.Drawing.Point(149, 120)
        Me.lblContactPhone.Name = "lblContactPhone"
        Me.lblContactPhone.Size = New System.Drawing.Size(464, 27)
        Me.lblContactPhone.TabIndex = 19
        Me.lblContactPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContactEmail
        '
        Me.lblContactEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactEmail.Location = New System.Drawing.Point(149, 85)
        Me.lblContactEmail.Name = "lblContactEmail"
        Me.lblContactEmail.Size = New System.Drawing.Size(464, 27)
        Me.lblContactEmail.TabIndex = 18
        Me.lblContactEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContactName
        '
        Me.lblContactName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactName.Location = New System.Drawing.Point(149, 50)
        Me.lblContactName.Name = "lblContactName"
        Me.lblContactName.Size = New System.Drawing.Size(464, 27)
        Me.lblContactName.TabIndex = 17
        Me.lblContactName.Text = " "
        Me.lblContactName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCompanyName
        '
        Me.lblCompanyName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.Location = New System.Drawing.Point(149, 0)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(464, 27)
        Me.lblCompanyName.TabIndex = 16
        Me.lblCompanyName.Text = " "
        Me.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.grpMobile)
        Me.GroupBox3.Controls.Add(Me.GroupBox9)
        Me.GroupBox3.Controls.Add(Me.btnExit)
        Me.GroupBox3.Controls.Add(Me.GroupBox7)
        Me.GroupBox3.Controls.Add(Me.btnImportLicense)
        Me.GroupBox3.Controls.Add(Me.btnRequestLicense)
        Me.GroupBox3.Controls.Add(Me.btnTempLicense)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(6, 233)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(622, 428)
        Me.GroupBox3.TabIndex = 15
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "  Products  "
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.lblServer)
        Me.GroupBox7.Controls.Add(Me.lblServerExpiry)
        Me.GroupBox7.Controls.Add(Me.lblServerLicType)
        Me.GroupBox7.Controls.Add(Me.lblServerStatus)
        Me.GroupBox7.Controls.Add(Me.Label14)
        Me.GroupBox7.Controls.Add(Me.Label12)
        Me.GroupBox7.Controls.Add(Me.chkServer)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(9, 23)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(598, 110)
        Me.GroupBox7.TabIndex = 10
        Me.GroupBox7.TabStop = False
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Location = New System.Drawing.Point(25, 21)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(108, 15)
        Me.lblServer.TabIndex = 9
        Me.lblServer.Text = "CSIFLEX Server"
        '
        'lblServerExpiry
        '
        Me.lblServerExpiry.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerExpiry.ForeColor = System.Drawing.Color.Blue
        Me.lblServerExpiry.Location = New System.Drawing.Point(419, 43)
        Me.lblServerExpiry.Name = "lblServerExpiry"
        Me.lblServerExpiry.Size = New System.Drawing.Size(120, 20)
        Me.lblServerExpiry.TabIndex = 7
        Me.lblServerExpiry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblServerLicType
        '
        Me.lblServerLicType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerLicType.ForeColor = System.Drawing.Color.Red
        Me.lblServerLicType.Location = New System.Drawing.Point(126, 43)
        Me.lblServerLicType.Name = "lblServerLicType"
        Me.lblServerLicType.Size = New System.Drawing.Size(162, 20)
        Me.lblServerLicType.TabIndex = 4
        Me.lblServerLicType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblServerStatus
        '
        Me.lblServerStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblServerStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerStatus.Location = New System.Drawing.Point(25, 70)
        Me.lblServerStatus.Name = "lblServerStatus"
        Me.lblServerStatus.Size = New System.Drawing.Size(567, 37)
        Me.lblServerStatus.TabIndex = 3
        Me.lblServerStatus.Text = "Label15"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(325, 43)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(88, 20)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Expiry date:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(25, 43)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(95, 20)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "License Type:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkServer
        '
        Me.chkServer.AutoSize = True
        Me.chkServer.Checked = True
        Me.chkServer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkServer.Enabled = False
        Me.chkServer.Location = New System.Drawing.Point(7, 20)
        Me.chkServer.Name = "chkServer"
        Me.chkServer.Size = New System.Drawing.Size(127, 19)
        Me.chkServer.TabIndex = 8
        Me.chkServer.Text = "CSIFLEX Server"
        Me.chkServer.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.lblFocas)
        Me.GroupBox9.Controls.Add(Me.Label3)
        Me.GroupBox9.Controls.Add(Me.txtNumberFocas)
        Me.GroupBox9.Controls.Add(Me.chkFocas)
        Me.GroupBox9.Controls.Add(Me.lblFocasExpiry)
        Me.GroupBox9.Controls.Add(Me.lblFocasLicType)
        Me.GroupBox9.Controls.Add(Me.lblFocasStatus)
        Me.GroupBox9.Controls.Add(Me.Label27)
        Me.GroupBox9.Controls.Add(Me.Label28)
        Me.GroupBox9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox9.Location = New System.Drawing.Point(9, 136)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(598, 110)
        Me.GroupBox9.TabIndex = 12
        Me.GroupBox9.TabStop = False
        '
        'lblFocas
        '
        Me.lblFocas.AutoSize = True
        Me.lblFocas.Location = New System.Drawing.Point(25, 21)
        Me.lblFocas.Name = "lblFocas"
        Me.lblFocas.Size = New System.Drawing.Size(129, 15)
        Me.lblFocas.TabIndex = 13
        Me.lblFocas.Text = "Focas / MTConnect"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(264, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(149, 20)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Number of connections:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNumberFocas
        '
        Me.txtNumberFocas.Location = New System.Drawing.Point(422, 18)
        Me.txtNumberFocas.MaxLength = 3
        Me.txtNumberFocas.Name = "txtNumberFocas"
        Me.txtNumberFocas.Size = New System.Drawing.Size(38, 21)
        Me.txtNumberFocas.TabIndex = 11
        Me.txtNumberFocas.Text = "5"
        '
        'chkFocas
        '
        Me.chkFocas.AutoSize = True
        Me.chkFocas.Location = New System.Drawing.Point(7, 20)
        Me.chkFocas.Name = "chkFocas"
        Me.chkFocas.Size = New System.Drawing.Size(148, 19)
        Me.chkFocas.TabIndex = 10
        Me.chkFocas.Text = "Focas / MTConnect"
        Me.chkFocas.UseVisualStyleBackColor = True
        '
        'lblFocasExpiry
        '
        Me.lblFocasExpiry.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFocasExpiry.ForeColor = System.Drawing.Color.Blue
        Me.lblFocasExpiry.Location = New System.Drawing.Point(419, 42)
        Me.lblFocasExpiry.Name = "lblFocasExpiry"
        Me.lblFocasExpiry.Size = New System.Drawing.Size(120, 20)
        Me.lblFocasExpiry.TabIndex = 6
        Me.lblFocasExpiry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFocasLicType
        '
        Me.lblFocasLicType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFocasLicType.ForeColor = System.Drawing.Color.Red
        Me.lblFocasLicType.Location = New System.Drawing.Point(126, 42)
        Me.lblFocasLicType.Name = "lblFocasLicType"
        Me.lblFocasLicType.Size = New System.Drawing.Size(162, 20)
        Me.lblFocasLicType.TabIndex = 5
        Me.lblFocasLicType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFocasStatus
        '
        Me.lblFocasStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFocasStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFocasStatus.Location = New System.Drawing.Point(25, 70)
        Me.lblFocasStatus.Name = "lblFocasStatus"
        Me.lblFocasStatus.Size = New System.Drawing.Size(567, 37)
        Me.lblFocasStatus.TabIndex = 3
        Me.lblFocasStatus.Text = "Label26"
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(325, 42)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(88, 20)
        Me.Label27.TabIndex = 2
        Me.Label27.Text = "Expiry date:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(25, 42)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(95, 20)
        Me.Label28.TabIndex = 1
        Me.Label28.Text = "License Type:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpMobile
        '
        Me.grpMobile.Controls.Add(Me.lblMobile)
        Me.grpMobile.Controls.Add(Me.Label1)
        Me.grpMobile.Controls.Add(Me.txtNumberMobile)
        Me.grpMobile.Controls.Add(Me.chkMobile)
        Me.grpMobile.Controls.Add(Me.lblMobileExpiry)
        Me.grpMobile.Controls.Add(Me.lblMobileLicType)
        Me.grpMobile.Controls.Add(Me.lblMobileStatus)
        Me.grpMobile.Controls.Add(Me.Label18)
        Me.grpMobile.Controls.Add(Me.Label19)
        Me.grpMobile.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMobile.Location = New System.Drawing.Point(9, 249)
        Me.grpMobile.Name = "grpMobile"
        Me.grpMobile.Size = New System.Drawing.Size(598, 110)
        Me.grpMobile.TabIndex = 11
        Me.grpMobile.TabStop = False
        '
        'lblMobile
        '
        Me.lblMobile.AutoSize = True
        Me.lblMobile.Location = New System.Drawing.Point(25, 21)
        Me.lblMobile.Name = "lblMobile"
        Me.lblMobile.Size = New System.Drawing.Size(123, 15)
        Me.lblMobile.TabIndex = 11
        Me.lblMobile.Text = "CSIFLEX Web App"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(264, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 20)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Number of devices:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label1.Visible = False
        '
        'txtNumberMobile
        '
        Me.txtNumberMobile.Location = New System.Drawing.Point(422, 18)
        Me.txtNumberMobile.MaxLength = 3
        Me.txtNumberMobile.Name = "txtNumberMobile"
        Me.txtNumberMobile.Size = New System.Drawing.Size(38, 21)
        Me.txtNumberMobile.TabIndex = 9
        Me.txtNumberMobile.Text = "5"
        Me.txtNumberMobile.Visible = False
        '
        'chkMobile
        '
        Me.chkMobile.AutoSize = True
        Me.chkMobile.Location = New System.Drawing.Point(7, 20)
        Me.chkMobile.Name = "chkMobile"
        Me.chkMobile.Size = New System.Drawing.Size(130, 19)
        Me.chkMobile.TabIndex = 9
        Me.chkMobile.Text = "CSIFLEX Mobile"
        Me.chkMobile.UseVisualStyleBackColor = True
        '
        'lblMobileExpiry
        '
        Me.lblMobileExpiry.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMobileExpiry.ForeColor = System.Drawing.Color.Blue
        Me.lblMobileExpiry.Location = New System.Drawing.Point(419, 43)
        Me.lblMobileExpiry.Name = "lblMobileExpiry"
        Me.lblMobileExpiry.Size = New System.Drawing.Size(120, 20)
        Me.lblMobileExpiry.TabIndex = 6
        Me.lblMobileExpiry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMobileLicType
        '
        Me.lblMobileLicType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMobileLicType.ForeColor = System.Drawing.Color.Red
        Me.lblMobileLicType.Location = New System.Drawing.Point(126, 43)
        Me.lblMobileLicType.Name = "lblMobileLicType"
        Me.lblMobileLicType.Size = New System.Drawing.Size(162, 20)
        Me.lblMobileLicType.TabIndex = 5
        Me.lblMobileLicType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMobileStatus
        '
        Me.lblMobileStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMobileStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMobileStatus.Location = New System.Drawing.Point(25, 70)
        Me.lblMobileStatus.Name = "lblMobileStatus"
        Me.lblMobileStatus.Size = New System.Drawing.Size(567, 37)
        Me.lblMobileStatus.TabIndex = 3
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(325, 43)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(88, 20)
        Me.Label18.TabIndex = 2
        Me.Label18.Text = "Expiry date:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(25, 43)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(95, 20)
        Me.Label19.TabIndex = 1
        Me.Label19.Text = "License Type:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtContactPhone
        '
        Me.txtContactPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactPhone.Location = New System.Drawing.Point(152, 122)
        Me.txtContactPhone.Name = "txtContactPhone"
        Me.txtContactPhone.Size = New System.Drawing.Size(476, 22)
        Me.txtContactPhone.TabIndex = 4
        Me.txtContactPhone.Visible = False
        '
        'txtContactEmail
        '
        Me.txtContactEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactEmail.Location = New System.Drawing.Point(152, 87)
        Me.txtContactEmail.Name = "txtContactEmail"
        Me.txtContactEmail.Size = New System.Drawing.Size(476, 22)
        Me.txtContactEmail.TabIndex = 3
        Me.txtContactEmail.Visible = False
        '
        'txtContactPerson
        '
        Me.txtContactPerson.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactPerson.Location = New System.Drawing.Point(152, 52)
        Me.txtContactPerson.Name = "txtContactPerson"
        Me.txtContactPerson.Size = New System.Drawing.Size(476, 22)
        Me.txtContactPerson.TabIndex = 2
        Me.txtContactPerson.Visible = False
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.Location = New System.Drawing.Point(152, 2)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(476, 22)
        Me.txtCompanyName.TabIndex = 1
        Me.txtCompanyName.Visible = False
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 190)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(140, 27)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Computer ID:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 155)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(140, 27)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Computer Name:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 120)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(140, 27)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Contact Phone #:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 85)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(140, 27)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Contact email:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(140, 27)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Contact person:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(140, 27)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Company Name:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 22)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(672, 709)
        Me.Panel2.TabIndex = 16
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 95)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(678, 734)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "  License Information  "
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.GroupBox3)
        Me.Panel3.Controls.Add(Me.lblCompanyId)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.lblComputerId)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.lblComputerName)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.lblContactPhone)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.lblContactEmail)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.lblContactName)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.lblCompanyName)
        Me.Panel3.Controls.Add(Me.txtCompanyName)
        Me.Panel3.Controls.Add(Me.txtContactPerson)
        Me.Panel3.Controls.Add(Me.txtContactPhone)
        Me.Panel3.Controls.Add(Me.txtContactEmail)
        Me.Panel3.Location = New System.Drawing.Point(3, 17)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(645, 666)
        Me.Panel3.TabIndex = 0
        '
        'LicenseManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(704, 834)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.pictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(9000, 1200)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(720, 625)
        Me.Name = "LicenseManagement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CSIFLEX License Management"
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.grpMobile.ResumeLayout(False)
        Me.grpMobile.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents pictureBox1 As PictureBox
    Private WithEvents label2 As Label
    Friend WithEvents btnTempLicense As Button
    Friend WithEvents btnRequestLicense As Button
    Friend WithEvents btnImportLicense As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents txtContactPhone As TextBox
    Friend WithEvents txtContactEmail As TextBox
    Friend WithEvents txtContactPerson As TextBox
    Friend WithEvents txtCompanyName As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents lblFocasExpiry As Label
    Friend WithEvents lblFocasLicType As Label
    Friend WithEvents lblFocasStatus As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents grpMobile As GroupBox
    Friend WithEvents lblMobileExpiry As Label
    Friend WithEvents lblMobileLicType As Label
    Friend WithEvents lblMobileStatus As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents lblServerExpiry As Label
    Friend WithEvents lblServerLicType As Label
    Friend WithEvents lblServerStatus As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents chkFocas As CheckBox
    Friend WithEvents chkServer As CheckBox
    Friend WithEvents chkMobile As CheckBox
    Friend WithEvents lblComputerId As Label
    Friend WithEvents lblComputerName As Label
    Friend WithEvents lblContactPhone As Label
    Friend WithEvents lblContactEmail As Label
    Friend WithEvents lblContactName As Label
    Friend WithEvents lblCompanyName As Label
    Friend WithEvents txtNumberMobile As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtNumberFocas As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblFocas As Label
    Friend WithEvents lblServer As Label
    Friend WithEvents lblMobile As Label
    Friend WithEvents lblCompanyId As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents GroupBox1 As GroupBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LicenseManagement
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.btnTempLicense = New System.Windows.Forms.Button()
        Me.btnRequestLicense = New System.Windows.Forms.Button()
        Me.btnImportLicense = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
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
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'pictureBox1
        '
        Me.pictureBox1.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.csi_lt_bgArtboard_1
        Me.pictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(257, 75)
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
        Me.label2.Size = New System.Drawing.Size(468, 85)
        Me.label2.TabIndex = 9
        Me.label2.Text = "License Management"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnTempLicense
        '
        Me.btnTempLicense.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnTempLicense.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTempLicense.Location = New System.Drawing.Point(12, 513)
        Me.btnTempLicense.Name = "btnTempLicense"
        Me.btnTempLicense.Size = New System.Drawing.Size(160, 50)
        Me.btnTempLicense.TabIndex = 0
        Me.btnTempLicense.Text = "Generate a temporary license"
        Me.btnTempLicense.UseVisualStyleBackColor = True
        '
        'btnRequestLicense
        '
        Me.btnRequestLicense.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRequestLicense.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRequestLicense.Location = New System.Drawing.Point(178, 513)
        Me.btnRequestLicense.Name = "btnRequestLicense"
        Me.btnRequestLicense.Size = New System.Drawing.Size(160, 50)
        Me.btnRequestLicense.TabIndex = 5
        Me.btnRequestLicense.Text = "Request a new license"
        Me.btnRequestLicense.UseVisualStyleBackColor = True
        '
        'btnImportLicense
        '
        Me.btnImportLicense.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnImportLicense.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportLicense.Location = New System.Drawing.Point(344, 513)
        Me.btnImportLicense.Name = "btnImportLicense"
        Me.btnImportLicense.Size = New System.Drawing.Size(160, 50)
        Me.btnImportLicense.TabIndex = 6
        Me.btnImportLicense.Text = "Import new license file"
        Me.btnImportLicense.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Black
        Me.btnExit.Location = New System.Drawing.Point(643, 513)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 50)
        Me.btnExit.TabIndex = 7
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblCompanyId)
        Me.GroupBox2.Controls.Add(Me.lblComputerId)
        Me.GroupBox2.Controls.Add(Me.lblComputerName)
        Me.GroupBox2.Controls.Add(Me.lblContactPhone)
        Me.GroupBox2.Controls.Add(Me.lblContactEmail)
        Me.GroupBox2.Controls.Add(Me.lblContactName)
        Me.GroupBox2.Controls.Add(Me.lblCompanyName)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.txtContactPhone)
        Me.GroupBox2.Controls.Add(Me.txtContactEmail)
        Me.GroupBox2.Controls.Add(Me.txtContactPerson)
        Me.GroupBox2.Controls.Add(Me.txtCompanyName)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 106)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(730, 399)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "  License Information  "
        '
        'lblCompanyId
        '
        Me.lblCompanyId.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyId.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblCompanyId.Location = New System.Drawing.Point(152, 59)
        Me.lblCompanyId.Name = "lblCompanyId"
        Me.lblCompanyId.Size = New System.Drawing.Size(354, 17)
        Me.lblCompanyId.TabIndex = 22
        '
        'lblComputerId
        '
        Me.lblComputerId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComputerId.Location = New System.Drawing.Point(152, 216)
        Me.lblComputerId.Name = "lblComputerId"
        Me.lblComputerId.Size = New System.Drawing.Size(550, 27)
        Me.lblComputerId.TabIndex = 21
        Me.lblComputerId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblComputerName
        '
        Me.lblComputerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComputerName.Location = New System.Drawing.Point(152, 181)
        Me.lblComputerName.Name = "lblComputerName"
        Me.lblComputerName.Size = New System.Drawing.Size(550, 27)
        Me.lblComputerName.TabIndex = 20
        Me.lblComputerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContactPhone
        '
        Me.lblContactPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactPhone.Location = New System.Drawing.Point(152, 146)
        Me.lblContactPhone.Name = "lblContactPhone"
        Me.lblContactPhone.Size = New System.Drawing.Size(550, 27)
        Me.lblContactPhone.TabIndex = 19
        Me.lblContactPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContactEmail
        '
        Me.lblContactEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactEmail.Location = New System.Drawing.Point(152, 111)
        Me.lblContactEmail.Name = "lblContactEmail"
        Me.lblContactEmail.Size = New System.Drawing.Size(550, 27)
        Me.lblContactEmail.TabIndex = 18
        Me.lblContactEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblContactName
        '
        Me.lblContactName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactName.Location = New System.Drawing.Point(152, 76)
        Me.lblContactName.Name = "lblContactName"
        Me.lblContactName.Size = New System.Drawing.Size(550, 27)
        Me.lblContactName.TabIndex = 17
        Me.lblContactName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCompanyName
        '
        Me.lblCompanyName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.Location = New System.Drawing.Point(152, 31)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(550, 27)
        Me.lblCompanyName.TabIndex = 16
        Me.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.GroupBox7)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(6, 248)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(717, 143)
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
        Me.GroupBox7.Location = New System.Drawing.Point(6, 23)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(705, 110)
        Me.GroupBox7.TabIndex = 10
        Me.GroupBox7.TabStop = False
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Location = New System.Drawing.Point(25, 21)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(104, 15)
        Me.lblServer.TabIndex = 9
        Me.lblServer.Text = "CSIFLEX Client"
        '
        'lblServerExpiry
        '
        Me.lblServerExpiry.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerExpiry.ForeColor = System.Drawing.Color.Blue
        Me.lblServerExpiry.Location = New System.Drawing.Point(419, 43)
        Me.lblServerExpiry.Name = "lblServerExpiry"
        Me.lblServerExpiry.Size = New System.Drawing.Size(88, 20)
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
        Me.lblServerStatus.Size = New System.Drawing.Size(674, 37)
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
        'txtContactPhone
        '
        Me.txtContactPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactPhone.Location = New System.Drawing.Point(155, 148)
        Me.txtContactPhone.Name = "txtContactPhone"
        Me.txtContactPhone.Size = New System.Drawing.Size(560, 22)
        Me.txtContactPhone.TabIndex = 4
        Me.txtContactPhone.Visible = False
        '
        'txtContactEmail
        '
        Me.txtContactEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactEmail.Location = New System.Drawing.Point(155, 113)
        Me.txtContactEmail.Name = "txtContactEmail"
        Me.txtContactEmail.Size = New System.Drawing.Size(560, 22)
        Me.txtContactEmail.TabIndex = 3
        Me.txtContactEmail.Visible = False
        '
        'txtContactPerson
        '
        Me.txtContactPerson.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactPerson.Location = New System.Drawing.Point(155, 78)
        Me.txtContactPerson.Name = "txtContactPerson"
        Me.txtContactPerson.Size = New System.Drawing.Size(560, 22)
        Me.txtContactPerson.TabIndex = 2
        Me.txtContactPerson.Visible = False
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.Location = New System.Drawing.Point(155, 33)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(560, 22)
        Me.txtCompanyName.TabIndex = 1
        Me.txtCompanyName.Visible = False
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 216)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(140, 27)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Computer ID:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 181)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(140, 27)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Computer Name:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 146)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(140, 27)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Contact Phone #:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 111)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(140, 27)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Contact email:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 76)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(140, 27)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Contact person:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(140, 27)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Company Name:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LicenseManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(755, 575)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnImportLicense)
        Me.Controls.Add(Me.btnRequestLicense)
        Me.Controls.Add(Me.btnTempLicense)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.pictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LicenseManagement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CSIFLEX License Management"
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents pictureBox1 As PictureBox
    Private WithEvents label2 As Label
    Friend WithEvents btnTempLicense As Button
    Friend WithEvents btnRequestLicense As Button
    Friend WithEvents btnImportLicense As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents GroupBox2 As GroupBox
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
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents lblServerExpiry As Label
    Friend WithEvents lblServerLicType As Label
    Friend WithEvents lblServerStatus As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents chkServer As CheckBox
    Friend WithEvents lblComputerId As Label
    Friend WithEvents lblComputerName As Label
    Friend WithEvents lblContactPhone As Label
    Friend WithEvents lblContactEmail As Label
    Friend WithEvents lblContactName As Label
    Friend WithEvents lblCompanyName As Label
    Friend WithEvents lblServer As Label
    Friend WithEvents lblCompanyId As Label
End Class

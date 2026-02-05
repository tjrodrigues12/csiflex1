<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Welcome
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Welcome))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BTN_PathDB = New System.Windows.Forms.Button()
        Me.txtEnetPort = New System.Windows.Forms.Button()
        Me.BTN_DefaultFolder = New System.Windows.Forms.Button()
        Me.txtEnetIPAddress = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CB_LocalDatabase = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDatabaseIP = New System.Windows.Forms.TextBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.btnConfirmOk = New System.Windows.Forms.Button()
        Me.RD_Version_Lite = New System.Windows.Forms.RadioButton()
        Me.RB_Version_Standard = New System.Windows.Forms.RadioButton()
        Me.RB_Version_ServerBased = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.save_server_animation
        Me.PictureBox1.Location = New System.Drawing.Point(33, 39)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(216, 138)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.BTN_PathDB)
        Me.GroupBox1.Controls.Add(Me.txtEnetPort)
        Me.GroupBox1.Controls.Add(Me.BTN_DefaultFolder)
        Me.GroupBox1.Controls.Add(Me.txtEnetIPAddress)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.CB_LocalDatabase)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtDatabaseIP)
        Me.GroupBox1.Controls.Add(Me.PictureBox3)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(12, 121)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(289, 373)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "CSIFLEX Server"
        '
        'BTN_PathDB
        '
        Me.BTN_PathDB.BackColor = System.Drawing.Color.Transparent
        Me.BTN_PathDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_PathDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_PathDB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BTN_PathDB.Location = New System.Drawing.Point(246, 300)
        Me.BTN_PathDB.Name = "BTN_PathDB"
        Me.BTN_PathDB.Size = New System.Drawing.Size(37, 22)
        Me.BTN_PathDB.TabIndex = 1001
        Me.BTN_PathDB.Text = "..."
        Me.BTN_PathDB.UseVisualStyleBackColor = False
        '
        'txtEnetPort
        '
        Me.txtEnetPort.BackColor = System.Drawing.Color.Transparent
        Me.txtEnetPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.txtEnetPort.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEnetPort.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtEnetPort.Location = New System.Drawing.Point(178, 239)
        Me.txtEnetPort.Name = "txtEnetPort"
        Me.txtEnetPort.Size = New System.Drawing.Size(37, 22)
        Me.txtEnetPort.TabIndex = 21
        Me.txtEnetPort.Text = "..."
        Me.txtEnetPort.UseVisualStyleBackColor = False
        '
        'BTN_DefaultFolder
        '
        Me.BTN_DefaultFolder.BackColor = System.Drawing.Color.Transparent
        Me.BTN_DefaultFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_DefaultFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_DefaultFolder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BTN_DefaultFolder.Location = New System.Drawing.Point(221, 239)
        Me.BTN_DefaultFolder.Name = "BTN_DefaultFolder"
        Me.BTN_DefaultFolder.Size = New System.Drawing.Size(62, 22)
        Me.BTN_DefaultFolder.TabIndex = 20
        Me.BTN_DefaultFolder.Text = "Default"
        Me.BTN_DefaultFolder.UseVisualStyleBackColor = False
        '
        'txtEnetIPAddress
        '
        Me.txtEnetIPAddress.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEnetIPAddress.Location = New System.Drawing.Point(6, 239)
        Me.txtEnetIPAddress.Name = "txtEnetIPAddress"
        Me.txtEnetIPAddress.Size = New System.Drawing.Size(170, 22)
        Me.txtEnetIPAddress.TabIndex = 1
        Me.txtEnetIPAddress.Text = "C:\_eNETDNC"
        Me.txtEnetIPAddress.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(174, 216)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 19)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Port : "
        '
        'CB_LocalDatabase
        '
        Me.CB_LocalDatabase.AutoSize = True
        Me.CB_LocalDatabase.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_LocalDatabase.Location = New System.Drawing.Point(6, 328)
        Me.CB_LocalDatabase.Name = "CB_LocalDatabase"
        Me.CB_LocalDatabase.Size = New System.Drawing.Size(152, 23)
        Me.CB_LocalDatabase.TabIndex = 14
        Me.CB_LocalDatabase.Text = "Use a local database"
        Me.CB_LocalDatabase.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 180)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(0, 21)
        Me.Label6.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(3, 278)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(132, 19)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Server DB location : "
        '
        'txtDatabaseIP
        '
        Me.txtDatabaseIP.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatabaseIP.Location = New System.Drawing.Point(6, 300)
        Me.txtDatabaseIP.Name = "txtDatabaseIP"
        Me.txtDatabaseIP.Size = New System.Drawing.Size(234, 22)
        Me.txtDatabaseIP.TabIndex = 2
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.google_img
        Me.PictureBox3.Location = New System.Drawing.Point(33, 28)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(216, 180)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 3
        Me.PictureBox3.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(3, 216)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(127, 19)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "CSIFLEX Server IP : "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(95, 180)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 25)
        Me.Label1.TabIndex = 1000
        Me.Label1.Text = "CSIFLEX Server"
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox4.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.csi_lt_bgArtboard_1
        Me.PictureBox4.Location = New System.Drawing.Point(12, 21)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(353, 94)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 4
        Me.PictureBox4.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox5.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.cable_electrique
        Me.PictureBox5.Location = New System.Drawing.Point(376, 53)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(226, 210)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 5
        Me.PictureBox5.TabStop = False
        '
        'btnConfirmOk
        '
        Me.btnConfirmOk.BackColor = System.Drawing.Color.Transparent
        Me.btnConfirmOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfirmOk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirmOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnConfirmOk.Location = New System.Drawing.Point(376, 455)
        Me.btnConfirmOk.Name = "btnConfirmOk"
        Me.btnConfirmOk.Size = New System.Drawing.Size(162, 38)
        Me.btnConfirmOk.TabIndex = 12
        Me.btnConfirmOk.Text = "OK"
        Me.btnConfirmOk.UseVisualStyleBackColor = False
        '
        'RD_Version_Lite
        '
        Me.RD_Version_Lite.AutoSize = True
        Me.RD_Version_Lite.Checked = True
        Me.RD_Version_Lite.Enabled = False
        Me.RD_Version_Lite.Location = New System.Drawing.Point(26, 32)
        Me.RD_Version_Lite.Name = "RD_Version_Lite"
        Me.RD_Version_Lite.Size = New System.Drawing.Size(42, 17)
        Me.RD_Version_Lite.TabIndex = 13
        Me.RD_Version_Lite.TabStop = True
        Me.RD_Version_Lite.Text = "Lite"
        Me.RD_Version_Lite.UseVisualStyleBackColor = True
        '
        'RB_Version_Standard
        '
        Me.RB_Version_Standard.AutoSize = True
        Me.RB_Version_Standard.Enabled = False
        Me.RB_Version_Standard.Location = New System.Drawing.Point(26, 55)
        Me.RB_Version_Standard.Name = "RB_Version_Standard"
        Me.RB_Version_Standard.Size = New System.Drawing.Size(68, 17)
        Me.RB_Version_Standard.TabIndex = 14
        Me.RB_Version_Standard.Text = "Standard"
        Me.RB_Version_Standard.UseVisualStyleBackColor = True
        '
        'RB_Version_ServerBased
        '
        Me.RB_Version_ServerBased.AutoSize = True
        Me.RB_Version_ServerBased.Location = New System.Drawing.Point(26, 151)
        Me.RB_Version_ServerBased.Name = "RB_Version_ServerBased"
        Me.RB_Version_ServerBased.Size = New System.Drawing.Size(88, 17)
        Me.RB_Version_ServerBased.TabIndex = 15
        Me.RB_Version_ServerBased.Text = "Server based"
        Me.RB_Version_ServerBased.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RB_Version_Standard)
        Me.GroupBox3.Controls.Add(Me.RB_Version_ServerBased)
        Me.GroupBox3.Controls.Add(Me.RD_Version_Lite)
        Me.GroupBox3.Location = New System.Drawing.Point(376, 269)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(162, 174)
        Me.GroupBox3.TabIndex = 16
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "CSIFlex Version :"
        '
        'Welcome
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(612, 510)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.btnConfirmOk)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(753, 549)
        Me.MinimumSize = New System.Drawing.Size(627, 549)
        Me.Name = "Welcome"
        Me.Text = "Welcome"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents btnConfirmOk As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CB_LocalDatabase As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDatabaseIP As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents RD_Version_Lite As System.Windows.Forms.RadioButton
    Friend WithEvents RB_Version_Standard As System.Windows.Forms.RadioButton
    Friend WithEvents RB_Version_ServerBased As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEnetIPAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtEnetPort As System.Windows.Forms.Button
    Friend WithEvents BTN_DefaultFolder As System.Windows.Forms.Button
    Friend WithEvents BTN_PathDB As System.Windows.Forms.Button
End Class

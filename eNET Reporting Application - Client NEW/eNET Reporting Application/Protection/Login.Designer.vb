<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.BTN_AlreadyHaveLicense = New System.Windows.Forms.Button()
        Me.BTN_ContactInfo = New System.Windows.Forms.Button()
        Me.BTN_RequestLicence = New System.Windows.Forms.Button()
        Me.TB_State = New System.Windows.Forms.TextBox()
        Me.LBL_State = New System.Windows.Forms.Label()
        Me.TB_City = New System.Windows.Forms.TextBox()
        Me.LBL_City = New System.Windows.Forms.Label()
        Me.TB_Phone = New System.Windows.Forms.TextBox()
        Me.LBL_Phone = New System.Windows.Forms.Label()
        Me.TB_Address = New System.Windows.Forms.TextBox()
        Me.LBL_Address = New System.Windows.Forms.Label()
        Me.TB_Company = New System.Windows.Forms.TextBox()
        Me.LBL_Company = New System.Windows.Forms.Label()
        Me.TB_Email = New System.Windows.Forms.TextBox()
        Me.LBL_Email = New System.Windows.Forms.Label()
        Me.TB_FirstName = New System.Windows.Forms.TextBox()
        Me.LBL_FirstName = New System.Windows.Forms.Label()
        Me.TB_LastName = New System.Windows.Forms.TextBox()
        Me.LBL_LastName = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox6.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.BTN_AlreadyHaveLicense)
        Me.GroupBox6.Controls.Add(Me.BTN_ContactInfo)
        Me.GroupBox6.Controls.Add(Me.BTN_RequestLicence)
        Me.GroupBox6.Controls.Add(Me.TB_State)
        Me.GroupBox6.Controls.Add(Me.LBL_State)
        Me.GroupBox6.Controls.Add(Me.TB_City)
        Me.GroupBox6.Controls.Add(Me.LBL_City)
        Me.GroupBox6.Controls.Add(Me.TB_Phone)
        Me.GroupBox6.Controls.Add(Me.LBL_Phone)
        Me.GroupBox6.Controls.Add(Me.TB_Address)
        Me.GroupBox6.Controls.Add(Me.LBL_Address)
        Me.GroupBox6.Controls.Add(Me.TB_Company)
        Me.GroupBox6.Controls.Add(Me.LBL_Company)
        Me.GroupBox6.Controls.Add(Me.TB_Email)
        Me.GroupBox6.Controls.Add(Me.LBL_Email)
        Me.GroupBox6.Controls.Add(Me.TB_FirstName)
        Me.GroupBox6.Controls.Add(Me.LBL_FirstName)
        Me.GroupBox6.Controls.Add(Me.TB_LastName)
        Me.GroupBox6.Controls.Add(Me.LBL_LastName)
        Me.GroupBox6.Location = New System.Drawing.Point(9, 128)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(373, 380)
        Me.GroupBox6.TabIndex = 18
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "User Informations"
        '
        'BTN_AlreadyHaveLicense
        '
        Me.BTN_AlreadyHaveLicense.Location = New System.Drawing.Point(125, 300)
        Me.BTN_AlreadyHaveLicense.Name = "BTN_AlreadyHaveLicense"
        Me.BTN_AlreadyHaveLicense.Size = New System.Drawing.Size(106, 50)
        Me.BTN_AlreadyHaveLicense.TabIndex = 31
        Me.BTN_AlreadyHaveLicense.Text = "Click here if you already have your license"
        Me.BTN_AlreadyHaveLicense.UseVisualStyleBackColor = True
        '
        'BTN_ContactInfo
        '
        Me.BTN_ContactInfo.Location = New System.Drawing.Point(18, 300)
        Me.BTN_ContactInfo.Name = "BTN_ContactInfo"
        Me.BTN_ContactInfo.Size = New System.Drawing.Size(101, 50)
        Me.BTN_ContactInfo.TabIndex = 30
        Me.BTN_ContactInfo.Text = "Contact Informations"
        Me.BTN_ContactInfo.UseVisualStyleBackColor = True
        '
        'BTN_RequestLicence
        '
        Me.BTN_RequestLicence.Location = New System.Drawing.Point(237, 300)
        Me.BTN_RequestLicence.Name = "BTN_RequestLicence"
        Me.BTN_RequestLicence.Size = New System.Drawing.Size(110, 50)
        Me.BTN_RequestLicence.TabIndex = 32
        Me.BTN_RequestLicence.Text = "Request a licence"
        Me.BTN_RequestLicence.UseVisualStyleBackColor = True
        '
        'TB_State
        '
        Me.TB_State.Location = New System.Drawing.Point(125, 255)
        Me.TB_State.Name = "TB_State"
        Me.TB_State.Size = New System.Drawing.Size(222, 20)
        Me.TB_State.TabIndex = 8
        '
        'LBL_State
        '
        Me.LBL_State.AutoSize = True
        Me.LBL_State.Location = New System.Drawing.Point(25, 258)
        Me.LBL_State.Name = "LBL_State"
        Me.LBL_State.Size = New System.Drawing.Size(94, 13)
        Me.LBL_State.TabIndex = 140
        Me.LBL_State.Text = "State / Province  :"
        '
        'TB_City
        '
        Me.TB_City.Location = New System.Drawing.Point(101, 229)
        Me.TB_City.Name = "TB_City"
        Me.TB_City.Size = New System.Drawing.Size(246, 20)
        Me.TB_City.TabIndex = 7
        '
        'LBL_City
        '
        Me.LBL_City.AutoSize = True
        Me.LBL_City.Location = New System.Drawing.Point(25, 232)
        Me.LBL_City.Name = "LBL_City"
        Me.LBL_City.Size = New System.Drawing.Size(30, 13)
        Me.LBL_City.TabIndex = 120
        Me.LBL_City.Text = "City :"
        '
        'TB_Phone
        '
        Me.TB_Phone.Location = New System.Drawing.Point(101, 156)
        Me.TB_Phone.Name = "TB_Phone"
        Me.TB_Phone.Size = New System.Drawing.Size(246, 20)
        Me.TB_Phone.TabIndex = 5
        '
        'LBL_Phone
        '
        Me.LBL_Phone.AutoSize = True
        Me.LBL_Phone.Location = New System.Drawing.Point(25, 159)
        Me.LBL_Phone.Name = "LBL_Phone"
        Me.LBL_Phone.Size = New System.Drawing.Size(44, 13)
        Me.LBL_Phone.TabIndex = 100
        Me.LBL_Phone.Text = "Phone :"
        '
        'TB_Address
        '
        Me.TB_Address.Location = New System.Drawing.Point(101, 203)
        Me.TB_Address.Name = "TB_Address"
        Me.TB_Address.Size = New System.Drawing.Size(246, 20)
        Me.TB_Address.TabIndex = 6
        '
        'LBL_Address
        '
        Me.LBL_Address.AutoSize = True
        Me.LBL_Address.Location = New System.Drawing.Point(25, 206)
        Me.LBL_Address.Name = "LBL_Address"
        Me.LBL_Address.Size = New System.Drawing.Size(51, 13)
        Me.LBL_Address.TabIndex = 800
        Me.LBL_Address.Text = "Address :"
        '
        'TB_Company
        '
        Me.TB_Company.Location = New System.Drawing.Point(101, 87)
        Me.TB_Company.Name = "TB_Company"
        Me.TB_Company.Size = New System.Drawing.Size(246, 20)
        Me.TB_Company.TabIndex = 3
        '
        'LBL_Company
        '
        Me.LBL_Company.AutoSize = True
        Me.LBL_Company.Location = New System.Drawing.Point(25, 90)
        Me.LBL_Company.Name = "LBL_Company"
        Me.LBL_Company.Size = New System.Drawing.Size(57, 13)
        Me.LBL_Company.TabIndex = 600
        Me.LBL_Company.Text = "Company :"
        '
        'TB_Email
        '
        Me.TB_Email.Location = New System.Drawing.Point(101, 130)
        Me.TB_Email.Name = "TB_Email"
        Me.TB_Email.Size = New System.Drawing.Size(246, 20)
        Me.TB_Email.TabIndex = 4
        '
        'LBL_Email
        '
        Me.LBL_Email.AutoSize = True
        Me.LBL_Email.Location = New System.Drawing.Point(25, 133)
        Me.LBL_Email.Name = "LBL_Email"
        Me.LBL_Email.Size = New System.Drawing.Size(38, 13)
        Me.LBL_Email.TabIndex = 400
        Me.LBL_Email.Text = "Email :"
        '
        'TB_FirstName
        '
        Me.TB_FirstName.Location = New System.Drawing.Point(101, 35)
        Me.TB_FirstName.Name = "TB_FirstName"
        Me.TB_FirstName.Size = New System.Drawing.Size(246, 20)
        Me.TB_FirstName.TabIndex = 1
        '
        'LBL_FirstName
        '
        Me.LBL_FirstName.AutoSize = True
        Me.LBL_FirstName.Location = New System.Drawing.Point(25, 38)
        Me.LBL_FirstName.Name = "LBL_FirstName"
        Me.LBL_FirstName.Size = New System.Drawing.Size(61, 13)
        Me.LBL_FirstName.TabIndex = 200
        Me.LBL_FirstName.Text = "First name :"
        '
        'TB_LastName
        '
        Me.TB_LastName.Location = New System.Drawing.Point(101, 61)
        Me.TB_LastName.Name = "TB_LastName"
        Me.TB_LastName.Size = New System.Drawing.Size(246, 20)
        Me.TB_LastName.TabIndex = 2
        '
        'LBL_LastName
        '
        Me.LBL_LastName.AutoSize = True
        Me.LBL_LastName.Location = New System.Drawing.Point(25, 64)
        Me.LBL_LastName.Name = "LBL_LastName"
        Me.LBL_LastName.Size = New System.Drawing.Size(64, 13)
        Me.LBL_LastName.TabIndex = 555
        Me.LBL_LastName.Text = "Last Name :"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.CSIFLEXLOGOTR2
        Me.PictureBox1.Location = New System.Drawing.Point(9, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(373, 110)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 17
        Me.PictureBox1.TabStop = False
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(393, 522)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.PictureBox1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Login"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Registration"
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents TB_State As System.Windows.Forms.TextBox
    Friend WithEvents LBL_State As System.Windows.Forms.Label
    Friend WithEvents TB_City As System.Windows.Forms.TextBox
    Friend WithEvents LBL_City As System.Windows.Forms.Label
    Friend WithEvents TB_Phone As System.Windows.Forms.TextBox
    Friend WithEvents LBL_Phone As System.Windows.Forms.Label
    Friend WithEvents TB_Address As System.Windows.Forms.TextBox
    Friend WithEvents LBL_Address As System.Windows.Forms.Label
    Friend WithEvents TB_Company As System.Windows.Forms.TextBox
    Friend WithEvents LBL_Company As System.Windows.Forms.Label
    Friend WithEvents TB_Email As System.Windows.Forms.TextBox
    Friend WithEvents LBL_Email As System.Windows.Forms.Label
    Friend WithEvents TB_FirstName As System.Windows.Forms.TextBox
    Friend WithEvents LBL_FirstName As System.Windows.Forms.Label
    Friend WithEvents TB_LastName As System.Windows.Forms.TextBox
    Friend WithEvents LBL_LastName As System.Windows.Forms.Label
    Friend WithEvents BTN_ContactInfo As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents BTN_RequestLicence As System.Windows.Forms.Button
    Friend WithEvents BTN_AlreadyHaveLicense As System.Windows.Forms.Button
End Class

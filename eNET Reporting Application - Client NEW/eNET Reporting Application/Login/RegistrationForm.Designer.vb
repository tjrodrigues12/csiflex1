<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RegistrationForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RegistrationForm))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TB_Ext = New System.Windows.Forms.TextBox()
        Me.TB_Phone = New System.Windows.Forms.TextBox()
        Me.TB_Email = New System.Windows.Forms.TextBox()
        Me.TB_Company = New System.Windows.Forms.TextBox()
        Me.TB_LastName = New System.Windows.Forms.TextBox()
        Me.TB_FirstName = New System.Windows.Forms.TextBox()
        Me.LBL_Email = New System.Windows.Forms.Label()
        Me.LBL_Company = New System.Windows.Forms.Label()
        Me.LBL_Ext = New System.Windows.Forms.Label()
        Me.LBL_Phone = New System.Windows.Forms.Label()
        Me.LBL_LastName = New System.Windows.Forms.Label()
        Me.LBL_FirstName = New System.Windows.Forms.Label()
        Me.BTN_Register = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(83, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(277, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Please fill the information in the form below"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TB_Ext)
        Me.GroupBox1.Controls.Add(Me.TB_Phone)
        Me.GroupBox1.Controls.Add(Me.TB_Email)
        Me.GroupBox1.Controls.Add(Me.TB_Company)
        Me.GroupBox1.Controls.Add(Me.TB_LastName)
        Me.GroupBox1.Controls.Add(Me.TB_FirstName)
        Me.GroupBox1.Controls.Add(Me.LBL_Email)
        Me.GroupBox1.Controls.Add(Me.LBL_Company)
        Me.GroupBox1.Controls.Add(Me.LBL_Ext)
        Me.GroupBox1.Controls.Add(Me.LBL_Phone)
        Me.GroupBox1.Controls.Add(Me.LBL_LastName)
        Me.GroupBox1.Controls.Add(Me.LBL_FirstName)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 31)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(436, 186)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Register"
        '
        'TB_Ext
        '
        Me.TB_Ext.Location = New System.Drawing.Point(315, 148)
        Me.TB_Ext.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_Ext.Name = "TB_Ext"
        Me.TB_Ext.Size = New System.Drawing.Size(56, 22)
        Me.TB_Ext.TabIndex = 11
        '
        'TB_Phone
        '
        Me.TB_Phone.Location = New System.Drawing.Point(93, 148)
        Me.TB_Phone.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_Phone.Name = "TB_Phone"
        Me.TB_Phone.Size = New System.Drawing.Size(132, 22)
        Me.TB_Phone.TabIndex = 10
        '
        'TB_Email
        '
        Me.TB_Email.Location = New System.Drawing.Point(93, 116)
        Me.TB_Email.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_Email.Name = "TB_Email"
        Me.TB_Email.Size = New System.Drawing.Size(132, 22)
        Me.TB_Email.TabIndex = 9
        '
        'TB_Company
        '
        Me.TB_Company.Location = New System.Drawing.Point(93, 86)
        Me.TB_Company.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_Company.Name = "TB_Company"
        Me.TB_Company.Size = New System.Drawing.Size(132, 22)
        Me.TB_Company.TabIndex = 8
        '
        'TB_LastName
        '
        Me.TB_LastName.Location = New System.Drawing.Point(93, 57)
        Me.TB_LastName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_LastName.Name = "TB_LastName"
        Me.TB_LastName.Size = New System.Drawing.Size(132, 22)
        Me.TB_LastName.TabIndex = 7
        '
        'TB_FirstName
        '
        Me.TB_FirstName.Location = New System.Drawing.Point(93, 28)
        Me.TB_FirstName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_FirstName.Name = "TB_FirstName"
        Me.TB_FirstName.Size = New System.Drawing.Size(132, 22)
        Me.TB_FirstName.TabIndex = 6
        '
        'LBL_Email
        '
        Me.LBL_Email.AutoSize = True
        Me.LBL_Email.Location = New System.Drawing.Point(44, 119)
        Me.LBL_Email.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBL_Email.Name = "LBL_Email"
        Me.LBL_Email.Size = New System.Drawing.Size(41, 17)
        Me.LBL_Email.TabIndex = 5
        Me.LBL_Email.Text = "email"
        '
        'LBL_Company
        '
        Me.LBL_Company.AutoSize = True
        Me.LBL_Company.Location = New System.Drawing.Point(17, 90)
        Me.LBL_Company.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBL_Company.Name = "LBL_Company"
        Me.LBL_Company.Size = New System.Drawing.Size(67, 17)
        Me.LBL_Company.TabIndex = 4
        Me.LBL_Company.Text = "Company"
        '
        'LBL_Ext
        '
        Me.LBL_Ext.AutoSize = True
        Me.LBL_Ext.Location = New System.Drawing.Point(237, 151)
        Me.LBL_Ext.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBL_Ext.Name = "LBL_Ext"
        Me.LBL_Ext.Size = New System.Drawing.Size(68, 17)
        Me.LBL_Ext.TabIndex = 3
        Me.LBL_Ext.Text = "extension"
        '
        'LBL_Phone
        '
        Me.LBL_Phone.AutoSize = True
        Me.LBL_Phone.Location = New System.Drawing.Point(36, 151)
        Me.LBL_Phone.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBL_Phone.Name = "LBL_Phone"
        Me.LBL_Phone.Size = New System.Drawing.Size(48, 17)
        Me.LBL_Phone.TabIndex = 2
        Me.LBL_Phone.Text = "phone"
        '
        'LBL_LastName
        '
        Me.LBL_LastName.AutoSize = True
        Me.LBL_LastName.Location = New System.Drawing.Point(8, 60)
        Me.LBL_LastName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBL_LastName.Name = "LBL_LastName"
        Me.LBL_LastName.Size = New System.Drawing.Size(76, 17)
        Me.LBL_LastName.TabIndex = 1
        Me.LBL_LastName.Text = "Last Name"
        '
        'LBL_FirstName
        '
        Me.LBL_FirstName.AutoSize = True
        Me.LBL_FirstName.Location = New System.Drawing.Point(9, 32)
        Me.LBL_FirstName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBL_FirstName.Name = "LBL_FirstName"
        Me.LBL_FirstName.Size = New System.Drawing.Size(76, 17)
        Me.LBL_FirstName.TabIndex = 0
        Me.LBL_FirstName.Text = "First Name"
        '
        'BTN_Register
        '
        Me.BTN_Register.Location = New System.Drawing.Point(308, 224)
        Me.BTN_Register.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_Register.Name = "BTN_Register"
        Me.BTN_Register.Size = New System.Drawing.Size(140, 37)
        Me.BTN_Register.TabIndex = 2
        Me.BTN_Register.Text = "Register"
        Me.BTN_Register.UseVisualStyleBackColor = True
        '
        'RegistrationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 274)
        Me.Controls.Add(Me.BTN_Register)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "RegistrationForm"
        Me.Text = "Registration"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents LBL_Phone As System.Windows.Forms.Label
    Friend WithEvents LBL_LastName As System.Windows.Forms.Label
    Friend WithEvents LBL_FirstName As System.Windows.Forms.Label
    Friend WithEvents TB_Ext As System.Windows.Forms.TextBox
    Friend WithEvents TB_Phone As System.Windows.Forms.TextBox
    Friend WithEvents TB_Email As System.Windows.Forms.TextBox
    Friend WithEvents TB_Company As System.Windows.Forms.TextBox
    Friend WithEvents TB_LastName As System.Windows.Forms.TextBox
    Friend WithEvents TB_FirstName As System.Windows.Forms.TextBox
    Friend WithEvents LBL_Email As System.Windows.Forms.Label
    Friend WithEvents LBL_Company As System.Windows.Forms.Label
    Friend WithEvents LBL_Ext As System.Windows.Forms.Label
    Friend WithEvents BTN_Register As System.Windows.Forms.Button
End Class

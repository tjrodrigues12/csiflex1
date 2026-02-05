<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmailServer
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_Sender = New System.Windows.Forms.TextBox()
        Me.TB_Host = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TB_Port = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TB_Pwd = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.BTN_Save = New System.Windows.Forms.Button()
        Me.CHKB_ReqCred = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TB_test = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gbCustomEmail = New System.Windows.Forms.GroupBox()
        Me.chkUseSSL = New System.Windows.Forms.CheckBox()
        Me.rbDefaultEmail = New System.Windows.Forms.RadioButton()
        Me.rbCustomEmail = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        Me.gbCustomEmail.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sender email:"
        '
        'TB_Sender
        '
        Me.TB_Sender.Location = New System.Drawing.Point(7, 129)
        Me.TB_Sender.Name = "TB_Sender"
        Me.TB_Sender.Size = New System.Drawing.Size(170, 20)
        Me.TB_Sender.TabIndex = 3
        '
        'TB_Host
        '
        Me.TB_Host.Location = New System.Drawing.Point(7, 41)
        Me.TB_Host.Name = "TB_Host"
        Me.TB_Host.Size = New System.Drawing.Size(170, 20)
        Me.TB_Host.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "SMTP host* :"
        '
        'TB_Port
        '
        Me.TB_Port.Location = New System.Drawing.Point(7, 84)
        Me.TB_Port.Name = "TB_Port"
        Me.TB_Port.Size = New System.Drawing.Size(170, 20)
        Me.TB_Port.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "SMTP port:"
        '
        'TB_Pwd
        '
        Me.TB_Pwd.Location = New System.Drawing.Point(7, 190)
        Me.TB_Pwd.Name = "TB_Pwd"
        Me.TB_Pwd.Size = New System.Drawing.Size(170, 20)
        Me.TB_Pwd.TabIndex = 5
        Me.TB_Pwd.UseSystemPasswordChar = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 173)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Sender password:"
        '
        'BTN_Save
        '
        Me.BTN_Save.Location = New System.Drawing.Point(103, 313)
        Me.BTN_Save.Name = "BTN_Save"
        Me.BTN_Save.Size = New System.Drawing.Size(90, 23)
        Me.BTN_Save.TabIndex = 6
        Me.BTN_Save.Text = "Save and Quit"
        Me.BTN_Save.UseVisualStyleBackColor = True
        '
        'CHKB_ReqCred
        '
        Me.CHKB_ReqCred.AutoSize = True
        Me.CHKB_ReqCred.Checked = True
        Me.CHKB_ReqCred.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHKB_ReqCred.Location = New System.Drawing.Point(7, 155)
        Me.CHKB_ReqCred.Name = "CHKB_ReqCred"
        Me.CHKB_ReqCred.Size = New System.Drawing.Size(133, 17)
        Me.CHKB_ReqCred.TabIndex = 4
        Me.CHKB_ReqCred.Text = "Require authentication"
        Me.CHKB_ReqCred.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(7, 17)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(170, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Send test email to :"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TB_test
        '
        Me.TB_test.Location = New System.Drawing.Point(7, 45)
        Me.TB_test.Margin = New System.Windows.Forms.Padding(2)
        Me.TB_test.Name = "TB_test"
        Me.TB_test.Size = New System.Drawing.Size(170, 20)
        Me.TB_test.TabIndex = 8
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.TB_test)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 341)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(188, 72)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Optional"
        '
        'gbCustomEmail
        '
        Me.gbCustomEmail.Controls.Add(Me.chkUseSSL)
        Me.gbCustomEmail.Controls.Add(Me.TB_Host)
        Me.gbCustomEmail.Controls.Add(Me.Label2)
        Me.gbCustomEmail.Controls.Add(Me.TB_Port)
        Me.gbCustomEmail.Controls.Add(Me.CHKB_ReqCred)
        Me.gbCustomEmail.Controls.Add(Me.TB_Pwd)
        Me.gbCustomEmail.Controls.Add(Me.Label4)
        Me.gbCustomEmail.Controls.Add(Me.Label3)
        Me.gbCustomEmail.Controls.Add(Me.TB_Sender)
        Me.gbCustomEmail.Controls.Add(Me.Label1)
        Me.gbCustomEmail.Location = New System.Drawing.Point(5, 60)
        Me.gbCustomEmail.Name = "gbCustomEmail"
        Me.gbCustomEmail.Size = New System.Drawing.Size(188, 247)
        Me.gbCustomEmail.TabIndex = 11
        Me.gbCustomEmail.TabStop = False
        Me.gbCustomEmail.Text = "Custom Email"
        '
        'chkUseSSL
        '
        Me.chkUseSSL.AutoSize = True
        Me.chkUseSSL.Checked = True
        Me.chkUseSSL.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUseSSL.Location = New System.Drawing.Point(7, 216)
        Me.chkUseSSL.Name = "chkUseSSL"
        Me.chkUseSSL.Size = New System.Drawing.Size(68, 17)
        Me.chkUseSSL.TabIndex = 11
        Me.chkUseSSL.Text = "Use SSL"
        Me.chkUseSSL.UseVisualStyleBackColor = True
        '
        'rbDefaultEmail
        '
        Me.rbDefaultEmail.AutoSize = True
        Me.rbDefaultEmail.Checked = True
        Me.rbDefaultEmail.Location = New System.Drawing.Point(3, 3)
        Me.rbDefaultEmail.Name = "rbDefaultEmail"
        Me.rbDefaultEmail.Size = New System.Drawing.Size(106, 17)
        Me.rbDefaultEmail.TabIndex = 12
        Me.rbDefaultEmail.TabStop = True
        Me.rbDefaultEmail.Text = "Use default email"
        Me.rbDefaultEmail.UseVisualStyleBackColor = True
        '
        'rbCustomEmail
        '
        Me.rbCustomEmail.AutoSize = True
        Me.rbCustomEmail.Location = New System.Drawing.Point(3, 25)
        Me.rbCustomEmail.Name = "rbCustomEmail"
        Me.rbCustomEmail.Size = New System.Drawing.Size(108, 17)
        Me.rbCustomEmail.TabIndex = 13
        Me.rbCustomEmail.Text = "Use custom email"
        Me.rbCustomEmail.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbCustomEmail)
        Me.Panel1.Controls.Add(Me.rbDefaultEmail)
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(188, 53)
        Me.Panel1.TabIndex = 14
        '
        'EmailServer
        '
        Me.AcceptButton = Me.BTN_Save
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(204, 422)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.gbCustomEmail)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BTN_Save)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "EmailServer"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Email Server Configuration"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbCustomEmail.ResumeLayout(False)
        Me.gbCustomEmail.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TB_Sender As System.Windows.Forms.TextBox
    Friend WithEvents TB_Host As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TB_Port As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TB_Pwd As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents BTN_Save As System.Windows.Forms.Button
    Friend WithEvents CHKB_ReqCred As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As Button
    Friend WithEvents TB_test As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents gbCustomEmail As GroupBox
    Friend WithEvents rbDefaultEmail As RadioButton
    Friend WithEvents rbCustomEmail As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents chkUseSSL As CheckBox
End Class

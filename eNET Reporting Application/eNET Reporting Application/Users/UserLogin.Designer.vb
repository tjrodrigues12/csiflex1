<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726")>
Partial Class UserLogin
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents TB_Username As System.Windows.Forms.TextBox
    Friend WithEvents TB_Password As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Cancel As System.Windows.Forms.Button

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.TB_Username = New System.Windows.Forms.TextBox()
        Me.TB_Password = New System.Windows.Forms.TextBox()
        Me.BTN_Cancel = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BTN_Ok = New System.Windows.Forms.Button()
        Me.CB_remember = New System.Windows.Forms.CheckBox()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'UsernameLabel
        '
        Me.UsernameLabel.BackColor = System.Drawing.Color.Transparent
        Me.UsernameLabel.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameLabel.ForeColor = System.Drawing.Color.Black
        Me.UsernameLabel.Location = New System.Drawing.Point(81, 260)
        Me.UsernameLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(292, 28)
        Me.UsernameLabel.TabIndex = 0
        Me.UsernameLabel.Text = "&User Name :"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PasswordLabel
        '
        Me.PasswordLabel.BackColor = System.Drawing.Color.Transparent
        Me.PasswordLabel.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasswordLabel.ForeColor = System.Drawing.Color.Black
        Me.PasswordLabel.Location = New System.Drawing.Point(81, 341)
        Me.PasswordLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(292, 28)
        Me.PasswordLabel.TabIndex = 2
        Me.PasswordLabel.Text = "&Password :"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TB_Username
        '
        Me.TB_Username.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_Username.Location = New System.Drawing.Point(81, 292)
        Me.TB_Username.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_Username.Name = "TB_Username"
        Me.TB_Username.Size = New System.Drawing.Size(292, 33)
        Me.TB_Username.TabIndex = 1
        '
        'TB_Password
        '
        Me.TB_Password.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_Password.Location = New System.Drawing.Point(81, 373)
        Me.TB_Password.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_Password.Name = "TB_Password"
        Me.TB_Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TB_Password.Size = New System.Drawing.Size(292, 33)
        Me.TB_Password.TabIndex = 3
        '
        'BTN_Cancel
        '
        Me.BTN_Cancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BTN_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Cancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Cancel.Location = New System.Drawing.Point(236, 457)
        Me.BTN_Cancel.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Cancel.Name = "BTN_Cancel"
        Me.BTN_Cancel.Size = New System.Drawing.Size(137, 41)
        Me.BTN_Cancel.TabIndex = 6
        Me.BTN_Cancel.Text = "&Cancel"
        Me.BTN_Cancel.UseVisualStyleBackColor = False
        '
        'BTN_Ok
        '
        Me.BTN_Ok.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Ok.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BTN_Ok.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Ok.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Ok.Location = New System.Drawing.Point(81, 457)
        Me.BTN_Ok.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Ok.Name = "BTN_Ok"
        Me.BTN_Ok.Size = New System.Drawing.Size(137, 41)
        Me.BTN_Ok.TabIndex = 5
        Me.BTN_Ok.Text = "&Ok"
        Me.BTN_Ok.UseVisualStyleBackColor = False
        '
        'CB_remember
        '
        Me.CB_remember.AutoSize = True
        Me.CB_remember.BackColor = System.Drawing.Color.Transparent
        Me.CB_remember.ForeColor = System.Drawing.Color.Black
        Me.CB_remember.Location = New System.Drawing.Point(81, 413)
        Me.CB_remember.Name = "CB_remember"
        Me.CB_remember.Size = New System.Drawing.Size(77, 17)
        Me.CB_remember.TabIndex = 4
        Me.CB_remember.Text = "Remember"
        Me.CB_remember.UseVisualStyleBackColor = False
        '
        'lblVersion
        '
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(287, 518)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(151, 23)
        Me.lblVersion.TabIndex = 8
        Me.lblVersion.Text = "Version: 1.1.1"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'UserLogin
        '
        Me.AcceptButton = Me.BTN_Ok
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BackgroundImage = Global.CSI_Reporting_Application.My.Resources.Resources.csiflex_panelv7
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.BTN_Cancel
        Me.ClientSize = New System.Drawing.Size(450, 550)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.CB_remember)
        Me.Controls.Add(Me.BTN_Ok)
        Me.Controls.Add(Me.BTN_Cancel)
        Me.Controls.Add(Me.TB_Password)
        Me.Controls.Add(Me.TB_Username)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.UsernameLabel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UserLogin"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CSIFLEX Server Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BTN_Ok As Button
    Friend WithEvents CB_remember As CheckBox
    Friend WithEvents lblVersion As Label
End Class

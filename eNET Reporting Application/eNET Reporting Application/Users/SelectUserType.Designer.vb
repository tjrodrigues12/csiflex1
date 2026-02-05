<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectUserType
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RB_Programmer = New System.Windows.Forms.RadioButton()
        Me.RB_Admin = New System.Windows.Forms.RadioButton()
        Me.RB_User = New System.Windows.Forms.RadioButton()
        Me.BTN_Ok = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RB_Programmer)
        Me.GroupBox1.Controls.Add(Me.RB_Admin)
        Me.GroupBox1.Controls.Add(Me.RB_User)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(370, 95)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " "
        '
        'RB_Programmer
        '
        Me.RB_Programmer.AutoSize = True
        Me.RB_Programmer.Location = New System.Drawing.Point(6, 65)
        Me.RB_Programmer.Name = "RB_Programmer"
        Me.RB_Programmer.Size = New System.Drawing.Size(81, 17)
        Me.RB_Programmer.TabIndex = 2
        Me.RB_Programmer.TabStop = True
        Me.RB_Programmer.Text = "Programmer"
        Me.RB_Programmer.UseVisualStyleBackColor = True
        '
        'RB_Admin
        '
        Me.RB_Admin.AutoSize = True
        Me.RB_Admin.Location = New System.Drawing.Point(6, 42)
        Me.RB_Admin.Name = "RB_Admin"
        Me.RB_Admin.Size = New System.Drawing.Size(343, 17)
        Me.RB_Admin.TabIndex = 1
        Me.RB_Admin.TabStop = True
        Me.RB_Admin.Text = "Admin : ( Admins are allowed to acces and modify the server setup )"
        Me.RB_Admin.UseVisualStyleBackColor = True
        '
        'RB_User
        '
        Me.RB_User.AutoSize = True
        Me.RB_User.Location = New System.Drawing.Point(6, 19)
        Me.RB_User.Name = "RB_User"
        Me.RB_User.Size = New System.Drawing.Size(47, 17)
        Me.RB_User.TabIndex = 0
        Me.RB_User.TabStop = True
        Me.RB_User.Text = "User"
        Me.RB_User.UseVisualStyleBackColor = True
        '
        'BTN_Ok
        '
        Me.BTN_Ok.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Ok.Location = New System.Drawing.Point(152, 103)
        Me.BTN_Ok.Name = "BTN_Ok"
        Me.BTN_Ok.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Ok.TabIndex = 1
        Me.BTN_Ok.Text = "OK"
        Me.BTN_Ok.UseVisualStyleBackColor = False
        '
        'SelectUserType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(383, 133)
        Me.Controls.Add(Me.BTN_Ok)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SelectUserType"
        Me.Opacity = 0.8R
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Select User Type"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RB_Admin As System.Windows.Forms.RadioButton
    Friend WithEvents RB_User As System.Windows.Forms.RadioButton
    Friend WithEvents BTN_Ok As System.Windows.Forms.Button
    Friend WithEvents RB_Programmer As System.Windows.Forms.RadioButton
End Class

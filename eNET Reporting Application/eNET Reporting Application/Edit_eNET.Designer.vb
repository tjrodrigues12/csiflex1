<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Edit_eNET
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Edit_eNET))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.BTN_Default = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_EnetPath = New System.Windows.Forms.TextBox()
        Me.BTN_Browse = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TB_EnetIp = New System.Windows.Forms.TextBox()
        Me.BTN_Ok = New System.Windows.Forms.Button()
        Me.BTN_Cancel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.BTN_Check = New System.Windows.Forms.Button()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.logo
        Me.PictureBox1.Location = New System.Drawing.Point(12, 42)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(408, 219)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'BTN_Default
        '
        Me.BTN_Default.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Default.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Default.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Default.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Default.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Default.Location = New System.Drawing.Point(16, 330)
        Me.BTN_Default.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_Default.Name = "BTN_Default"
        Me.BTN_Default.Size = New System.Drawing.Size(100, 28)
        Me.BTN_Default.TabIndex = 6
        Me.BTN_Default.Text = "Default"
        Me.BTN_Default.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 278)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 17)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Specify eNET folder :"
        '
        'TB_EnetPath
        '
        Me.TB_EnetPath.Location = New System.Drawing.Point(12, 298)
        Me.TB_EnetPath.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_EnetPath.Name = "TB_EnetPath"
        Me.TB_EnetPath.Size = New System.Drawing.Size(357, 22)
        Me.TB_EnetPath.TabIndex = 5
        '
        'BTN_Browse
        '
        Me.BTN_Browse.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Browse.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Browse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Browse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Browse.Location = New System.Drawing.Point(320, 330)
        Me.BTN_Browse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_Browse.Name = "BTN_Browse"
        Me.BTN_Browse.Size = New System.Drawing.Size(100, 28)
        Me.BTN_Browse.TabIndex = 4
        Me.BTN_Browse.Text = "Browse"
        Me.BTN_Browse.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 450)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(195, 17)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "eNET http Server IP and port:"
        '
        'TB_EnetIp
        '
        Me.TB_EnetIp.Location = New System.Drawing.Point(8, 476)
        Me.TB_EnetIp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_EnetIp.Name = "TB_EnetIp"
        Me.TB_EnetIp.Size = New System.Drawing.Size(176, 22)
        Me.TB_EnetIp.TabIndex = 8
        '
        'BTN_Ok
        '
        Me.BTN_Ok.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Ok.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Ok.Location = New System.Drawing.Point(8, 529)
        Me.BTN_Ok.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_Ok.Name = "BTN_Ok"
        Me.BTN_Ok.Size = New System.Drawing.Size(100, 28)
        Me.BTN_Ok.TabIndex = 10
        Me.BTN_Ok.Text = "Ok"
        Me.BTN_Ok.UseVisualStyleBackColor = False
        '
        'BTN_Cancel
        '
        Me.BTN_Cancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BTN_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Cancel.Location = New System.Drawing.Point(316, 529)
        Me.BTN_Cancel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_Cancel.Name = "BTN_Cancel"
        Me.BTN_Cancel.Size = New System.Drawing.Size(100, 28)
        Me.BTN_Cancel.TabIndex = 11
        Me.BTN_Cancel.Text = "Cancel"
        Me.BTN_Cancel.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PictureBox5)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ComboBox4)
        Me.GroupBox1.Controls.Add(Me.BTN_Check)
        Me.GroupBox1.Controls.Add(Me.BTN_Cancel)
        Me.GroupBox1.Controls.Add(Me.PictureBox3)
        Me.GroupBox1.Controls.Add(Me.PictureBox2)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.BTN_Ok)
        Me.GroupBox1.Controls.Add(Me.TB_EnetIp)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(4, 26)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(421, 565)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "eNET Setup"
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Kcjx7zKcq
        Me.PictureBox5.Location = New System.Drawing.Point(372, 244)
        Me.PictureBox5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(41, 53)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 32
        Me.PictureBox5.TabStop = False
        Me.PictureBox5.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 505)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 17)
        Me.Label3.TabIndex = 19
        '
        'ComboBox4
        '
        Me.ComboBox4.BackColor = System.Drawing.SystemColors.MenuBar
        Me.ComboBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(116, 529)
        Me.ComboBox4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(191, 27)
        Me.ComboBox4.TabIndex = 18
        Me.ComboBox4.Visible = False
        '
        'BTN_Check
        '
        Me.BTN_Check.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Check.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Check.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Check.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Check.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Check.Location = New System.Drawing.Point(193, 474)
        Me.BTN_Check.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_Check.Name = "BTN_Check"
        Me.BTN_Check.Size = New System.Drawing.Size(75, 28)
        Me.BTN_Check.TabIndex = 17
        Me.BTN_Check.Text = "Check"
        Me.BTN_Check.UseVisualStyleBackColor = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.CSI_Reporting_Application.My.Resources.Resources.alert_animated
        Me.PictureBox3.Location = New System.Drawing.Point(276, 431)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(105, 91)
        Me.PictureBox3.TabIndex = 16
        Me.PictureBox3.TabStop = False
        Me.PictureBox3.Visible = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.CSI_Reporting_Application.My.Resources.Resources._339927
        Me.PictureBox2.Location = New System.Drawing.Point(231, 353)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(183, 167)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 15
        Me.PictureBox2.TabStop = False
        Me.PictureBox2.Visible = False
        '
        'Edit_eNET
        '
        Me.AcceptButton = Me.BTN_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.BTN_Cancel
        Me.ClientSize = New System.Drawing.Size(433, 606)
        Me.Controls.Add(Me.BTN_Default)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TB_EnetPath)
        Me.Controls.Add(Me.BTN_Browse)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Edit_eNET"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Add eNET source"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents BTN_Default As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TB_EnetPath As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Browse As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TB_EnetIp As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Ok As System.Windows.Forms.Button
    Friend WithEvents BTN_Cancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents BTN_Check As System.Windows.Forms.Button
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
End Class

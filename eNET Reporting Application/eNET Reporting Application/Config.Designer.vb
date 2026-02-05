<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Config
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Config))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TB_Pwd = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TB_IP = New System.Windows.Forms.TextBox()
        Me.BTN_Save = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolTip_ShowPassword = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(223, 25)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(193, 17)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Enter the Enet FTP password"
        '
        'TB_Pwd
        '
        Me.TB_Pwd.Location = New System.Drawing.Point(224, 47)
        Me.TB_Pwd.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_Pwd.Name = "TB_Pwd"
        Me.TB_Pwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TB_Pwd.Size = New System.Drawing.Size(189, 22)
        Me.TB_Pwd.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 25)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(145, 17)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Enter the Enet FTP IP"
        '
        'TB_IP
        '
        Me.TB_IP.Location = New System.Drawing.Point(21, 47)
        Me.TB_IP.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_IP.Name = "TB_IP"
        Me.TB_IP.Size = New System.Drawing.Size(175, 22)
        Me.TB_IP.TabIndex = 12
        '
        'BTN_Save
        '
        Me.BTN_Save.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Save.Location = New System.Drawing.Point(188, 95)
        Me.BTN_Save.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Save.Name = "BTN_Save"
        Me.BTN_Save.Size = New System.Drawing.Size(120, 28)
        Me.BTN_Save.TabIndex = 19
        Me.BTN_Save.Text = "Save"
        Me.BTN_Save.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(423, 40)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 20
        Me.PictureBox1.TabStop = False
        '
        'ToolTip_ShowPassword
        '
        Me.ToolTip_ShowPassword.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip_ShowPassword.ToolTipTitle = "Show Password"
        '
        'Config
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(514, 146)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.BTN_Save)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TB_Pwd)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TB_IP)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Config"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Enet Configuration"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TB_Pwd As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TB_IP As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Save As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ToolTip_ShowPassword As ToolTip
End Class

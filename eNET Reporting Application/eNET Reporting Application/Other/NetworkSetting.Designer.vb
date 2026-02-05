<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NetworkSetting
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
        Me.TB_SSID = New System.Windows.Forms.TextBox()
        Me.SSID = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_PWD = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TB_IP = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TB_mask = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TB_gatway = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CB_sec = New System.Windows.Forms.ComboBox()
        Me.BTN_ok = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TB_SSID
        '
        Me.TB_SSID.Location = New System.Drawing.Point(68, 84)
        Me.TB_SSID.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_SSID.Name = "TB_SSID"
        Me.TB_SSID.Size = New System.Drawing.Size(135, 20)
        Me.TB_SSID.TabIndex = 0
        '
        'SSID
        '
        Me.SSID.AutoSize = True
        Me.SSID.Location = New System.Drawing.Point(9, 86)
        Me.SSID.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SSID.Name = "SSID"
        Me.SSID.Size = New System.Drawing.Size(35, 13)
        Me.SSID.TabIndex = 1
        Me.SSID.Text = "SSID:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 111)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Password:"
        '
        'TB_PWD
        '
        Me.TB_PWD.Location = New System.Drawing.Point(68, 109)
        Me.TB_PWD.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_PWD.Name = "TB_PWD"
        Me.TB_PWD.Size = New System.Drawing.Size(135, 20)
        Me.TB_PWD.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 174)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "IP:"
        '
        'TB_IP
        '
        Me.TB_IP.Location = New System.Drawing.Point(68, 171)
        Me.TB_IP.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_IP.Name = "TB_IP"
        Me.TB_IP.Size = New System.Drawing.Size(135, 20)
        Me.TB_IP.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 197)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Mask:"
        '
        'TB_mask
        '
        Me.TB_mask.Location = New System.Drawing.Point(68, 194)
        Me.TB_mask.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_mask.Name = "TB_mask"
        Me.TB_mask.Size = New System.Drawing.Size(135, 20)
        Me.TB_mask.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 219)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Gatway:"
        '
        'TB_gatway
        '
        Me.TB_gatway.Location = New System.Drawing.Point(68, 217)
        Me.TB_gatway.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_gatway.Name = "TB_gatway"
        Me.TB_gatway.Size = New System.Drawing.Size(135, 20)
        Me.TB_gatway.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 134)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Security:"
        '
        'CB_sec
        '
        Me.CB_sec.FormattingEnabled = True
        Me.CB_sec.Items.AddRange(New Object() {"nwpa-ssid"})
        Me.CB_sec.Location = New System.Drawing.Point(89, 132)
        Me.CB_sec.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CB_sec.Name = "CB_sec"
        Me.CB_sec.Size = New System.Drawing.Size(92, 21)
        Me.CB_sec.TabIndex = 11
        '
        'BTN_ok
        '
        Me.BTN_ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_ok.Location = New System.Drawing.Point(75, 254)
        Me.BTN_ok.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.BTN_ok.Name = "BTN_ok"
        Me.BTN_ok.Size = New System.Drawing.Size(64, 25)
        Me.BTN_ok.TabIndex = 12
        Me.BTN_ok.Text = "ok"
        Me.BTN_ok.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.appbar_cogs
        Me.PictureBox1.Location = New System.Drawing.Point(-1, -1)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(68, 69)
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'NetworkSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(212, 291)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.BTN_ok)
        Me.Controls.Add(Me.CB_sec)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TB_gatway)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TB_mask)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TB_IP)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TB_PWD)
        Me.Controls.Add(Me.SSID)
        Me.Controls.Add(Me.TB_SSID)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "NetworkSetting"
        Me.Text = "NetworkSetting"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TB_SSID As TextBox
    Friend WithEvents SSID As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TB_PWD As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TB_IP As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TB_mask As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TB_gatway As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents CB_sec As ComboBox
    Friend WithEvents BTN_ok As Button
    Friend WithEvents PictureBox1 As PictureBox
End Class

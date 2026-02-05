<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Database_performance_tuning
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
        Me.pool_buffer = New System.Windows.Forms.TrackBar()
        Me.poolbuffer = New System.Windows.Forms.Label()
        Me.TB_pool_buffer = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.max_conn = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.ok = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TP_help = New System.Windows.Forms.ToolTip(Me.components)
        Me.LBL_ram = New System.Windows.Forms.Label()
        Me.LBL_ram_avail = New System.Windows.Forms.Label()
        Me.def = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        CType(Me.pool_buffer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pool_buffer
        '
        Me.pool_buffer.Location = New System.Drawing.Point(249, 225)
        Me.pool_buffer.Name = "pool_buffer"
        Me.pool_buffer.Size = New System.Drawing.Size(196, 45)
        Me.pool_buffer.TabIndex = 0
        Me.pool_buffer.Value = 5
        '
        'poolbuffer
        '
        Me.poolbuffer.AutoSize = True
        Me.poolbuffer.Location = New System.Drawing.Point(26, 234)
        Me.poolbuffer.Name = "poolbuffer"
        Me.poolbuffer.Size = New System.Drawing.Size(180, 13)
        Me.poolbuffer.TabIndex = 1
        Me.poolbuffer.Text = "innodb buffer pool size (% RAM) :"
        '
        'TB_pool_buffer
        '
        Me.TB_pool_buffer.Location = New System.Drawing.Point(451, 231)
        Me.TB_pool_buffer.Name = "TB_pool_buffer"
        Me.TB_pool_buffer.Size = New System.Drawing.Size(41, 21)
        Me.TB_pool_buffer.TabIndex = 3
        '
        'TextBox2
        '
        Me.TextBox2.Enabled = False
        Me.TextBox2.Location = New System.Drawing.Point(249, 291)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(58, 21)
        Me.TextBox2.TabIndex = 7
        Me.TextBox2.Text = "150"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Enabled = False
        Me.Label1.Location = New System.Drawing.Point(26, 353)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "innodb log file size :"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(249, 353)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(58, 21)
        Me.TextBox3.TabIndex = 12
        '
        'max_conn
        '
        Me.max_conn.AutoSize = True
        Me.max_conn.Location = New System.Drawing.Point(26, 294)
        Me.max_conn.Name = "max_conn"
        Me.max_conn.Size = New System.Drawing.Size(96, 13)
        Me.max_conn.TabIndex = 10
        Me.max_conn.Text = "max connections:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(26, 411)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(166, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "innodb flush log at trx commit:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(26, 472)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(125, 13)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "innodb log buffer size:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(26, 536)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 13)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "query cache size"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(249, 536)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(58, 21)
        Me.TextBox4.TabIndex = 25
        '
        'ok
        '
        Me.ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ok.Location = New System.Drawing.Point(188, 616)
        Me.ok.Name = "ok"
        Me.ok.Size = New System.Drawing.Size(121, 33)
        Me.ok.TabIndex = 34
        Me.ok.Text = "Apply"
        Me.ok.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.free_vector_chart_icons_main
        Me.PictureBox1.Location = New System.Drawing.Point(591, 35)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(129, 129)
        Me.PictureBox1.TabIndex = 36
        Me.PictureBox1.TabStop = False
        '
        'TP_help
        '
        Me.TP_help.IsBalloon = True
        Me.TP_help.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.TP_help.ToolTipTitle = "What is it ?"
        '
        'LBL_ram
        '
        Me.LBL_ram.AutoSize = True
        Me.LBL_ram.Font = New System.Drawing.Font("Segoe UI", 7.0!)
        Me.LBL_ram.Location = New System.Drawing.Point(12, 13)
        Me.LBL_ram.Name = "LBL_ram"
        Me.LBL_ram.Size = New System.Drawing.Size(31, 12)
        Me.LBL_ram.TabIndex = 37
        Me.LBL_ram.Text = "RAM :"
        '
        'LBL_ram_avail
        '
        Me.LBL_ram_avail.AutoSize = True
        Me.LBL_ram_avail.Font = New System.Drawing.Font("Segoe UI", 7.0!)
        Me.LBL_ram_avail.Location = New System.Drawing.Point(12, 32)
        Me.LBL_ram_avail.Name = "LBL_ram_avail"
        Me.LBL_ram_avail.Size = New System.Drawing.Size(31, 12)
        Me.LBL_ram_avail.TabIndex = 38
        Me.LBL_ram_avail.Text = "RAM :"
        '
        'def
        '
        Me.def.AutoSize = True
        Me.def.Font = New System.Drawing.Font("Segoe UI", 7.0!)
        Me.def.Location = New System.Drawing.Point(498, 234)
        Me.def.Name = "def"
        Me.def.Size = New System.Drawing.Size(62, 12)
        Me.def.TabIndex = 39
        Me.def.Text = "Default : 50%"
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(315, 616)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(121, 33)
        Me.Button1.TabIndex = 40
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(442, 616)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(121, 33)
        Me.Button2.TabIndex = 41
        Me.Button2.Text = "Restor to default"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 7.0!)
        Me.Label10.Location = New System.Drawing.Point(317, 296)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(67, 12)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "Default : 150%"
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(249, 399)
        Me.TrackBar1.Maximum = 2
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(196, 45)
        Me.TrackBar1.TabIndex = 43
        Me.TrackBar1.Value = 2
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(451, 399)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(41, 21)
        Me.TextBox1.TabIndex = 44
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 7.0!)
        Me.Label2.Location = New System.Drawing.Point(498, 404)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 12)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Default : 2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 7.0!)
        Me.Label3.Location = New System.Drawing.Point(313, 479)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 12)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Default : 5MB"
        '
        'TextBox5
        '
        Me.TextBox5.Enabled = False
        Me.TextBox5.Location = New System.Drawing.Point(249, 474)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(58, 21)
        Me.TextBox5.TabIndex = 47
        Me.TextBox5.Text = "5"
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Database_performance_tuning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(745, 661)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.def)
        Me.Controls.Add(Me.LBL_ram_avail)
        Me.Controls.Add(Me.LBL_ram)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ok)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.max_conn)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TB_pool_buffer)
        Me.Controls.Add(Me.poolbuffer)
        Me.Controls.Add(Me.pool_buffer)
        Me.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Database_performance_tuning"
        Me.Text = "Database_performance_tuning"
        CType(Me.pool_buffer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pool_buffer As TrackBar
    Friend WithEvents poolbuffer As Label
    Friend WithEvents TB_pool_buffer As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents max_conn As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents ok As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TP_help As ToolTip
    Friend WithEvents LBL_ram As Label
    Friend WithEvents LBL_ram_avail As Label
    Friend WithEvents def As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents TrackBar1 As TrackBar
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox5 As TextBox
End Class

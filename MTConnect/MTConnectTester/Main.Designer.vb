<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.TB_ip = New System.Windows.Forms.TextBox()
        Me.LBL_ip = New System.Windows.Forms.Label()
        Me.LBL_AgentPort = New System.Windows.Forms.Label()
        Me.TB_AgentPort = New System.Windows.Forms.TextBox()
        Me.BTN_Test = New System.Windows.Forms.Button()
        Me.CB_MachineName = New System.Windows.Forms.ComboBox()
        Me.LBL_Devices = New System.Windows.Forms.Label()
        Me.LBL_Mode = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TB_ip
        '
        Me.TB_ip.Location = New System.Drawing.Point(12, 28)
        Me.TB_ip.Name = "TB_ip"
        Me.TB_ip.Size = New System.Drawing.Size(100, 20)
        Me.TB_ip.TabIndex = 0
        '
        'LBL_ip
        '
        Me.LBL_ip.AutoSize = True
        Me.LBL_ip.Location = New System.Drawing.Point(9, 12)
        Me.LBL_ip.Name = "LBL_ip"
        Me.LBL_ip.Size = New System.Drawing.Size(61, 13)
        Me.LBL_ip.TabIndex = 1
        Me.LBL_ip.Text = "Machine IP"
        '
        'LBL_AgentPort
        '
        Me.LBL_AgentPort.AutoSize = True
        Me.LBL_AgentPort.Location = New System.Drawing.Point(133, 12)
        Me.LBL_AgentPort.Name = "LBL_AgentPort"
        Me.LBL_AgentPort.Size = New System.Drawing.Size(55, 13)
        Me.LBL_AgentPort.TabIndex = 5
        Me.LBL_AgentPort.Text = "agent port"
        '
        'TB_AgentPort
        '
        Me.TB_AgentPort.Location = New System.Drawing.Point(136, 28)
        Me.TB_AgentPort.Name = "TB_AgentPort"
        Me.TB_AgentPort.Size = New System.Drawing.Size(100, 20)
        Me.TB_AgentPort.TabIndex = 4
        Me.TB_AgentPort.Text = "5000"
        '
        'BTN_Test
        '
        Me.BTN_Test.Location = New System.Drawing.Point(161, 67)
        Me.BTN_Test.Name = "BTN_Test"
        Me.BTN_Test.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Test.TabIndex = 6
        Me.BTN_Test.Text = "Test"
        Me.BTN_Test.UseVisualStyleBackColor = True
        '
        'CB_MachineName
        '
        Me.CB_MachineName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_MachineName.Enabled = False
        Me.CB_MachineName.FormattingEnabled = True
        Me.CB_MachineName.Items.AddRange(New Object() {"MTConnect", "Focas"})
        Me.CB_MachineName.Location = New System.Drawing.Point(47, 132)
        Me.CB_MachineName.Name = "CB_MachineName"
        Me.CB_MachineName.Size = New System.Drawing.Size(169, 21)
        Me.CB_MachineName.TabIndex = 34
        '
        'LBL_Devices
        '
        Me.LBL_Devices.AutoSize = True
        Me.LBL_Devices.Location = New System.Drawing.Point(44, 116)
        Me.LBL_Devices.Name = "LBL_Devices"
        Me.LBL_Devices.Size = New System.Drawing.Size(82, 13)
        Me.LBL_Devices.TabIndex = 35
        Me.LBL_Devices.Text = "Devices found :"
        '
        'LBL_Mode
        '
        Me.LBL_Mode.AutoSize = True
        Me.LBL_Mode.Location = New System.Drawing.Point(44, 156)
        Me.LBL_Mode.Name = "LBL_Mode"
        Me.LBL_Mode.Size = New System.Drawing.Size(47, 13)
        Me.LBL_Mode.TabIndex = 36
        Me.LBL_Mode.Text = "Adapter:"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(263, 226)
        Me.Controls.Add(Me.LBL_Mode)
        Me.Controls.Add(Me.LBL_Devices)
        Me.Controls.Add(Me.CB_MachineName)
        Me.Controls.Add(Me.BTN_Test)
        Me.Controls.Add(Me.LBL_AgentPort)
        Me.Controls.Add(Me.TB_AgentPort)
        Me.Controls.Add(Me.LBL_ip)
        Me.Controls.Add(Me.TB_ip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.Text = "MTConnect Tester"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TB_ip As System.Windows.Forms.TextBox
    Friend WithEvents LBL_ip As System.Windows.Forms.Label
    Friend WithEvents LBL_AgentPort As System.Windows.Forms.Label
    Friend WithEvents TB_AgentPort As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Test As System.Windows.Forms.Button
    Friend WithEvents CB_MachineName As System.Windows.Forms.ComboBox
    Friend WithEvents LBL_Devices As System.Windows.Forms.Label
    Friend WithEvents LBL_Mode As System.Windows.Forms.Label

End Class

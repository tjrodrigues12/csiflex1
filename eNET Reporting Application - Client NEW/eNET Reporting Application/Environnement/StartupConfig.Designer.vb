<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StartupConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StartupConfig))
        Me.TreeView_machine = New System.Windows.Forms.TreeView()
        Me.BTN_SaveConfig = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RB_Report = New System.Windows.Forms.RadioButton()
        Me.RB_Monit = New System.Windows.Forms.RadioButton()
        Me.P_ReportSettings = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NUD_ReportDays = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox1.SuspendLayout()
        Me.P_ReportSettings.SuspendLayout()
        CType(Me.NUD_ReportDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TreeView_machine
        '
        Me.TreeView_machine.AllowDrop = True
        Me.TreeView_machine.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView_machine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TreeView_machine.CheckBoxes = True
        Me.TreeView_machine.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView_machine.HotTracking = True
        Me.TreeView_machine.LabelEdit = True
        Me.TreeView_machine.LineColor = System.Drawing.Color.Blue
        Me.TreeView_machine.Location = New System.Drawing.Point(8, 82)
        Me.TreeView_machine.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TreeView_machine.Name = "TreeView_machine"
        Me.TreeView_machine.Size = New System.Drawing.Size(381, 356)
        Me.TreeView_machine.TabIndex = 5
        '
        'BTN_SaveConfig
        '
        Me.BTN_SaveConfig.Location = New System.Drawing.Point(244, 32)
        Me.BTN_SaveConfig.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_SaveConfig.Name = "BTN_SaveConfig"
        Me.BTN_SaveConfig.Size = New System.Drawing.Size(165, 57)
        Me.BTN_SaveConfig.TabIndex = 37
        Me.BTN_SaveConfig.Text = "Save configuration"
        Me.BTN_SaveConfig.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RB_Report)
        Me.GroupBox1.Controls.Add(Me.RB_Monit)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 15)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(203, 80)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Display type"
        '
        'RB_Report
        '
        Me.RB_Report.AutoSize = True
        Me.RB_Report.Checked = True
        Me.RB_Report.Location = New System.Drawing.Point(8, 53)
        Me.RB_Report.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RB_Report.Name = "RB_Report"
        Me.RB_Report.Size = New System.Drawing.Size(99, 21)
        Me.RB_Report.TabIndex = 1
        Me.RB_Report.TabStop = True
        Me.RB_Report.Text = "Dashboard"
        Me.RB_Report.UseVisualStyleBackColor = True
        '
        'RB_Monit
        '
        Me.RB_Monit.AutoSize = True
        Me.RB_Monit.Location = New System.Drawing.Point(8, 23)
        Me.RB_Monit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RB_Monit.Name = "RB_Monit"
        Me.RB_Monit.Size = New System.Drawing.Size(169, 21)
        Me.RB_Monit.TabIndex = 0
        Me.RB_Monit.Text = "Live Status Monitoring"
        Me.RB_Monit.UseVisualStyleBackColor = True
        '
        'P_ReportSettings
        '
        Me.P_ReportSettings.Controls.Add(Me.Label2)
        Me.P_ReportSettings.Controls.Add(Me.Label1)
        Me.P_ReportSettings.Controls.Add(Me.NUD_ReportDays)
        Me.P_ReportSettings.Controls.Add(Me.TreeView_machine)
        Me.P_ReportSettings.Location = New System.Drawing.Point(16, 114)
        Me.P_ReportSettings.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.P_ReportSettings.Name = "P_ReportSettings"
        Me.P_ReportSettings.Size = New System.Drawing.Size(393, 443)
        Me.P_ReportSettings.TabIndex = 39
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 59)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(190, 17)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Machines to include in report"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(166, 17)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Number of days to report"
        '
        'NUD_ReportDays
        '
        Me.NUD_ReportDays.Location = New System.Drawing.Point(8, 31)
        Me.NUD_ReportDays.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.NUD_ReportDays.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.NUD_ReportDays.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NUD_ReportDays.Name = "NUD_ReportDays"
        Me.NUD_ReportDays.Size = New System.Drawing.Size(160, 22)
        Me.NUD_ReportDays.TabIndex = 6
        Me.NUD_ReportDays.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'StartupConfig
        '
        Me.AcceptButton = Me.BTN_SaveConfig
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(423, 572)
        Me.Controls.Add(Me.P_ReportSettings)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BTN_SaveConfig)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "StartupConfig"
        Me.Text = "Startup Configuration"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.P_ReportSettings.ResumeLayout(False)
        Me.P_ReportSettings.PerformLayout()
        CType(Me.NUD_ReportDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TreeView_machine As System.Windows.Forms.TreeView
    Friend WithEvents BTN_SaveConfig As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RB_Report As System.Windows.Forms.RadioButton
    Friend WithEvents RB_Monit As System.Windows.Forms.RadioButton
    Friend WithEvents P_ReportSettings As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NUD_ReportDays As System.Windows.Forms.NumericUpDown
End Class

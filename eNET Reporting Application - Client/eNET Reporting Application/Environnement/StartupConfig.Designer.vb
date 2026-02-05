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
        Me.btnSaveConfig = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbDashboard = New System.Windows.Forms.RadioButton()
        Me.rdbMonitoring = New System.Windows.Forms.RadioButton()
        Me.P_ReportSettings = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.numDaysToReport = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox1.SuspendLayout()
        Me.P_ReportSettings.SuspendLayout()
        CType(Me.numDaysToReport, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TreeView_machine.Location = New System.Drawing.Point(6, 67)
        Me.TreeView_machine.Name = "TreeView_machine"
        Me.TreeView_machine.Size = New System.Drawing.Size(286, 290)
        Me.TreeView_machine.TabIndex = 5
        '
        'btnSaveConfig
        '
        Me.btnSaveConfig.Location = New System.Drawing.Point(183, 26)
        Me.btnSaveConfig.Name = "btnSaveConfig"
        Me.btnSaveConfig.Size = New System.Drawing.Size(124, 46)
        Me.btnSaveConfig.TabIndex = 37
        Me.btnSaveConfig.Text = "Save configuration"
        Me.btnSaveConfig.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbDashboard)
        Me.GroupBox1.Controls.Add(Me.rdbMonitoring)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(152, 65)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Display type"
        '
        'rdbDashboard
        '
        Me.rdbDashboard.AutoSize = True
        Me.rdbDashboard.Checked = True
        Me.rdbDashboard.Location = New System.Drawing.Point(6, 43)
        Me.rdbDashboard.Name = "rdbDashboard"
        Me.rdbDashboard.Size = New System.Drawing.Size(77, 17)
        Me.rdbDashboard.TabIndex = 1
        Me.rdbDashboard.TabStop = True
        Me.rdbDashboard.Text = "Dashboard"
        Me.rdbDashboard.UseVisualStyleBackColor = True
        '
        'rdbMonitoring
        '
        Me.rdbMonitoring.AutoSize = True
        Me.rdbMonitoring.Location = New System.Drawing.Point(6, 19)
        Me.rdbMonitoring.Name = "rdbMonitoring"
        Me.rdbMonitoring.Size = New System.Drawing.Size(130, 17)
        Me.rdbMonitoring.TabIndex = 0
        Me.rdbMonitoring.Text = "Live Status Monitoring"
        Me.rdbMonitoring.UseVisualStyleBackColor = True
        '
        'P_ReportSettings
        '
        Me.P_ReportSettings.Controls.Add(Me.Label2)
        Me.P_ReportSettings.Controls.Add(Me.Label1)
        Me.P_ReportSettings.Controls.Add(Me.numDaysToReport)
        Me.P_ReportSettings.Controls.Add(Me.TreeView_machine)
        Me.P_ReportSettings.Location = New System.Drawing.Point(12, 93)
        Me.P_ReportSettings.Name = "P_ReportSettings"
        Me.P_ReportSettings.Size = New System.Drawing.Size(295, 360)
        Me.P_ReportSettings.TabIndex = 39
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(143, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Machines to include in report"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(123, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Number of days to report"
        '
        'numDaysToReport
        '
        Me.numDaysToReport.Location = New System.Drawing.Point(6, 25)
        Me.numDaysToReport.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.numDaysToReport.Name = "numDaysToReport"
        Me.numDaysToReport.Size = New System.Drawing.Size(120, 20)
        Me.numDaysToReport.TabIndex = 6
        Me.numDaysToReport.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'StartupConfig
        '
        Me.AcceptButton = Me.btnSaveConfig
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(317, 465)
        Me.Controls.Add(Me.P_ReportSettings)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnSaveConfig)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "StartupConfig"
        Me.Text = "Startup Configuration"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.P_ReportSettings.ResumeLayout(False)
        Me.P_ReportSettings.PerformLayout()
        CType(Me.numDaysToReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TreeView_machine As System.Windows.Forms.TreeView
    Friend WithEvents btnSaveConfig As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbDashboard As System.Windows.Forms.RadioButton
    Friend WithEvents rdbMonitoring As System.Windows.Forms.RadioButton
    Friend WithEvents P_ReportSettings As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents numDaysToReport As System.Windows.Forms.NumericUpDown
End Class

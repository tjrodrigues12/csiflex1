<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetupForm_Reporting
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
        Me.BTN_GenerateReport = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CB_Weekly = New System.Windows.Forms.CheckBox()
        Me.CB_Monthly = New System.Windows.Forms.CheckBox()
        Me.CB_Daily = New System.Windows.Forms.CheckBox()
        Me.BTN_SetDefaultPath = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BTN_BrowseFolder = New System.Windows.Forms.Button()
        Me.tb_pathReport = New System.Windows.Forms.TextBox()
        Me.DTPicker = New System.Windows.Forms.DateTimePicker()
        Me.TreeView_machine = New System.Windows.Forms.TreeView()
        Me.BackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.PB_loading = New System.Windows.Forms.PictureBox()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PB_loading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BTN_GenerateReport
        '
        Me.BTN_GenerateReport.AutoEllipsis = True
        Me.BTN_GenerateReport.Location = New System.Drawing.Point(146, 405)
        Me.BTN_GenerateReport.Name = "BTN_GenerateReport"
        Me.BTN_GenerateReport.Size = New System.Drawing.Size(153, 38)
        Me.BTN_GenerateReport.TabIndex = 12
        Me.BTN_GenerateReport.Text = "Generate Report"
        Me.BTN_GenerateReport.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CB_Weekly)
        Me.GroupBox2.Controls.Add(Me.CB_Monthly)
        Me.GroupBox2.Controls.Add(Me.CB_Daily)
        Me.GroupBox2.Location = New System.Drawing.Point(24, 393)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(68, 81)
        Me.GroupBox2.TabIndex = 31
        Me.GroupBox2.TabStop = False
        '
        'CB_Weekly
        '
        Me.CB_Weekly.AutoSize = True
        Me.CB_Weekly.Location = New System.Drawing.Point(6, 35)
        Me.CB_Weekly.Name = "CB_Weekly"
        Me.CB_Weekly.Size = New System.Drawing.Size(62, 17)
        Me.CB_Weekly.TabIndex = 10
        Me.CB_Weekly.Text = "Weekly"
        Me.CB_Weekly.UseVisualStyleBackColor = True
        '
        'CB_Monthly
        '
        Me.CB_Monthly.AutoSize = True
        Me.CB_Monthly.Location = New System.Drawing.Point(6, 58)
        Me.CB_Monthly.Name = "CB_Monthly"
        Me.CB_Monthly.Size = New System.Drawing.Size(63, 17)
        Me.CB_Monthly.TabIndex = 11
        Me.CB_Monthly.Text = "Monthly"
        Me.CB_Monthly.UseVisualStyleBackColor = True
        '
        'CB_Daily
        '
        Me.CB_Daily.AutoSize = True
        Me.CB_Daily.Location = New System.Drawing.Point(6, 12)
        Me.CB_Daily.Name = "CB_Daily"
        Me.CB_Daily.Size = New System.Drawing.Size(49, 17)
        Me.CB_Daily.TabIndex = 9
        Me.CB_Daily.Text = "Daily"
        Me.CB_Daily.UseVisualStyleBackColor = True
        '
        'BTN_SetDefaultPath
        '
        Me.BTN_SetDefaultPath.Location = New System.Drawing.Point(246, 28)
        Me.BTN_SetDefaultPath.Name = "BTN_SetDefaultPath"
        Me.BTN_SetDefaultPath.Size = New System.Drawing.Size(53, 23)
        Me.BTN_SetDefaultPath.TabIndex = 3
        Me.BTN_SetDefaultPath.Text = "Default"
        Me.BTN_SetDefaultPath.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Export folder"
        '
        'BTN_BrowseFolder
        '
        Me.BTN_BrowseFolder.Location = New System.Drawing.Point(214, 28)
        Me.BTN_BrowseFolder.Name = "BTN_BrowseFolder"
        Me.BTN_BrowseFolder.Size = New System.Drawing.Size(26, 23)
        Me.BTN_BrowseFolder.TabIndex = 2
        Me.BTN_BrowseFolder.Text = "..."
        Me.BTN_BrowseFolder.UseVisualStyleBackColor = True
        '
        'tb_pathReport
        '
        Me.tb_pathReport.Location = New System.Drawing.Point(24, 28)
        Me.tb_pathReport.Name = "tb_pathReport"
        Me.tb_pathReport.Size = New System.Drawing.Size(184, 20)
        Me.tb_pathReport.TabIndex = 1
        '
        'DTPicker
        '
        Me.DTPicker.Location = New System.Drawing.Point(24, 57)
        Me.DTPicker.Name = "DTPicker"
        Me.DTPicker.Size = New System.Drawing.Size(275, 20)
        Me.DTPicker.TabIndex = 4
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
        Me.TreeView_machine.Location = New System.Drawing.Point(24, 94)
        Me.TreeView_machine.Name = "TreeView_machine"
        Me.TreeView_machine.Size = New System.Drawing.Size(275, 293)
        Me.TreeView_machine.TabIndex = 5
        '
        'BackgroundWorker
        '
        '
        'PB_loading
        '
        Me.PB_loading.Image = Global.CSI_Reporting_Application.My.Resources.Resources.loading_aero
        Me.PB_loading.Location = New System.Drawing.Point(99, 450)
        Me.PB_loading.Name = "PB_loading"
        Me.PB_loading.Size = New System.Drawing.Size(200, 27)
        Me.PB_loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_loading.TabIndex = 36
        Me.PB_loading.TabStop = False
        Me.PB_loading.Visible = False
        '
        'ReportViewer1
        '
        Me.ReportViewer1.DocumentMapWidth = 41
        Me.ReportViewer1.Location = New System.Drawing.Point(475, 136)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(114, 246)
        Me.ReportViewer1.TabIndex = 37
        Me.ReportViewer1.Visible = False
        '
        'SetupForm_Reporting
        '
        Me.AcceptButton = Me.BTN_GenerateReport
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(318, 489)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.PB_loading)
        Me.Controls.Add(Me.TreeView_machine)
        Me.Controls.Add(Me.DTPicker)
        Me.Controls.Add(Me.BTN_GenerateReport)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.BTN_SetDefaultPath)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.BTN_BrowseFolder)
        Me.Controls.Add(Me.tb_pathReport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "SetupForm_Reporting"
        Me.Text = "Reporting"
        Me.TopMost = True
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PB_loading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BTN_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CB_Weekly As System.Windows.Forms.CheckBox
    Friend WithEvents CB_Monthly As System.Windows.Forms.CheckBox
    Friend WithEvents CB_Daily As System.Windows.Forms.CheckBox
    Friend WithEvents BTN_SetDefaultPath As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BTN_BrowseFolder As System.Windows.Forms.Button
    Friend WithEvents tb_pathReport As System.Windows.Forms.TextBox
    Friend WithEvents DTPicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents TreeView_machine As System.Windows.Forms.TreeView
    Friend WithEvents BackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents PB_loading As System.Windows.Forms.PictureBox
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
End Class

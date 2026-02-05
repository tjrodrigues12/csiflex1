<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reporting_availability
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Reporting_availability))
        Me.BTN_GenerateReport = New System.Windows.Forms.Button()
        Me.BTN_SetDefaultPath = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BTN_BrowseFolder = New System.Windows.Forms.Button()
        Me.tb_pathReport = New System.Windows.Forms.TextBox()
        Me.TreeView_machine = New System.Windows.Forms.TreeView()
        Me.BackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.PB_loading = New System.Windows.Forms.PictureBox()
        Me.GB_ReportType = New System.Windows.Forms.GroupBox()
        Me.RB_Monthly = New System.Windows.Forms.RadioButton()
        Me.RB_Weekly = New System.Windows.Forms.RadioButton()
        Me.RB_Daily = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DTP_ReportDate = New System.Windows.Forms.DateTimePicker()
        CType(Me.PB_loading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_ReportType.SuspendLayout()
        Me.SuspendLayout()
        '
        'BTN_GenerateReport
        '
        Me.BTN_GenerateReport.AutoEllipsis = True
        Me.BTN_GenerateReport.Location = New System.Drawing.Point(223, 587)
        Me.BTN_GenerateReport.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_GenerateReport.Name = "BTN_GenerateReport"
        Me.BTN_GenerateReport.Size = New System.Drawing.Size(152, 33)
        Me.BTN_GenerateReport.TabIndex = 20
        Me.BTN_GenerateReport.Text = "Generate Report"
        Me.BTN_GenerateReport.UseVisualStyleBackColor = True
        '
        'BTN_SetDefaultPath
        '
        Me.BTN_SetDefaultPath.Location = New System.Drawing.Point(300, 551)
        Me.BTN_SetDefaultPath.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_SetDefaultPath.Name = "BTN_SetDefaultPath"
        Me.BTN_SetDefaultPath.Size = New System.Drawing.Size(71, 25)
        Me.BTN_SetDefaultPath.TabIndex = 3
        Me.BTN_SetDefaultPath.Text = "Default"
        Me.BTN_SetDefaultPath.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 532)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Export folder"
        '
        'BTN_BrowseFolder
        '
        Me.BTN_BrowseFolder.Location = New System.Drawing.Point(261, 551)
        Me.BTN_BrowseFolder.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_BrowseFolder.Name = "BTN_BrowseFolder"
        Me.BTN_BrowseFolder.Size = New System.Drawing.Size(35, 25)
        Me.BTN_BrowseFolder.TabIndex = 2
        Me.BTN_BrowseFolder.Text = "..."
        Me.BTN_BrowseFolder.UseVisualStyleBackColor = True
        '
        'tb_pathReport
        '
        Me.tb_pathReport.Location = New System.Drawing.Point(8, 551)
        Me.tb_pathReport.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_pathReport.Name = "tb_pathReport"
        Me.tb_pathReport.Size = New System.Drawing.Size(244, 20)
        Me.tb_pathReport.TabIndex = 15
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
        Me.TreeView_machine.Location = New System.Drawing.Point(8, 124)
        Me.TreeView_machine.Margin = New System.Windows.Forms.Padding(4)
        Me.TreeView_machine.Name = "TreeView_machine"
        Me.TreeView_machine.Size = New System.Drawing.Size(362, 398)
        Me.TreeView_machine.TabIndex = 10
        '
        'BackgroundWorker
        '
        '
        'PB_loading
        '
        Me.PB_loading.Image = Global.CSI_Reporting_Application.My.Resources.Resources.loading_aero
        Me.PB_loading.Location = New System.Drawing.Point(8, 587)
        Me.PB_loading.Margin = New System.Windows.Forms.Padding(4)
        Me.PB_loading.Name = "PB_loading"
        Me.PB_loading.Size = New System.Drawing.Size(205, 33)
        Me.PB_loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_loading.TabIndex = 36
        Me.PB_loading.TabStop = False
        Me.PB_loading.Visible = False
        '
        'GB_ReportType
        '
        Me.GB_ReportType.Controls.Add(Me.RB_Monthly)
        Me.GB_ReportType.Controls.Add(Me.RB_Weekly)
        Me.GB_ReportType.Controls.Add(Me.RB_Daily)
        Me.GB_ReportType.Location = New System.Drawing.Point(7, 70)
        Me.GB_ReportType.Margin = New System.Windows.Forms.Padding(4)
        Me.GB_ReportType.Name = "GB_ReportType"
        Me.GB_ReportType.Padding = New System.Windows.Forms.Padding(4)
        Me.GB_ReportType.Size = New System.Drawing.Size(361, 47)
        Me.GB_ReportType.TabIndex = 6
        Me.GB_ReportType.TabStop = False
        Me.GB_ReportType.Text = "Report type"
        '
        'RB_Monthly
        '
        Me.RB_Monthly.AutoSize = True
        Me.RB_Monthly.Location = New System.Drawing.Point(260, 18)
        Me.RB_Monthly.Margin = New System.Windows.Forms.Padding(4)
        Me.RB_Monthly.Name = "RB_Monthly"
        Me.RB_Monthly.Size = New System.Drawing.Size(62, 17)
        Me.RB_Monthly.TabIndex = 2
        Me.RB_Monthly.Text = "Monthly"
        Me.RB_Monthly.UseVisualStyleBackColor = True
        '
        'RB_Weekly
        '
        Me.RB_Weekly.AutoSize = True
        Me.RB_Weekly.Location = New System.Drawing.Point(131, 18)
        Me.RB_Weekly.Margin = New System.Windows.Forms.Padding(4)
        Me.RB_Weekly.Name = "RB_Weekly"
        Me.RB_Weekly.Size = New System.Drawing.Size(61, 17)
        Me.RB_Weekly.TabIndex = 1
        Me.RB_Weekly.Text = "Weekly"
        Me.RB_Weekly.UseVisualStyleBackColor = True
        '
        'RB_Daily
        '
        Me.RB_Daily.AutoSize = True
        Me.RB_Daily.Checked = True
        Me.RB_Daily.Location = New System.Drawing.Point(9, 18)
        Me.RB_Daily.Margin = New System.Windows.Forms.Padding(4)
        Me.RB_Daily.Name = "RB_Daily"
        Me.RB_Daily.Size = New System.Drawing.Size(48, 17)
        Me.RB_Daily.TabIndex = 0
        Me.RB_Daily.TabStop = True
        Me.RB_Daily.Text = "Daily"
        Me.RB_Daily.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 11)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Report date"
        '
        'DTP_ReportDate
        '
        Me.DTP_ReportDate.CustomFormat = "dd MMMM yyyy-ddd"
        Me.DTP_ReportDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_ReportDate.Location = New System.Drawing.Point(7, 38)
        Me.DTP_ReportDate.Margin = New System.Windows.Forms.Padding(4)
        Me.DTP_ReportDate.Name = "DTP_ReportDate"
        Me.DTP_ReportDate.Size = New System.Drawing.Size(360, 20)
        Me.DTP_ReportDate.TabIndex = 5
        '
        'Reporting_availability
        '
        Me.AcceptButton = Me.BTN_GenerateReport
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(381, 630)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DTP_ReportDate)
        Me.Controls.Add(Me.GB_ReportType)
        Me.Controls.Add(Me.PB_loading)
        Me.Controls.Add(Me.TreeView_machine)
        Me.Controls.Add(Me.BTN_GenerateReport)
        Me.Controls.Add(Me.BTN_SetDefaultPath)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.BTN_BrowseFolder)
        Me.Controls.Add(Me.tb_pathReport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "Reporting_availability"
        Me.Text = "Reporting: Availability"
        Me.TopMost = True
        CType(Me.PB_loading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GB_ReportType.ResumeLayout(False)
        Me.GB_ReportType.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BTN_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents BTN_SetDefaultPath As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BTN_BrowseFolder As System.Windows.Forms.Button
    Friend WithEvents tb_pathReport As System.Windows.Forms.TextBox
    Friend WithEvents TreeView_machine As System.Windows.Forms.TreeView
    Friend WithEvents BackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents PB_loading As System.Windows.Forms.PictureBox
    Friend WithEvents GB_ReportType As System.Windows.Forms.GroupBox
    Friend WithEvents RB_Monthly As System.Windows.Forms.RadioButton
    Friend WithEvents RB_Weekly As System.Windows.Forms.RadioButton
    Friend WithEvents RB_Daily As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DTP_ReportDate As System.Windows.Forms.DateTimePicker
End Class

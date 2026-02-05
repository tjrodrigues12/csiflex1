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
        Me.btnGenerateReport = New System.Windows.Forms.Button()
        Me.btnSetDefaultPath = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnBrowseFolder = New System.Windows.Forms.Button()
        Me.txtPathReport = New System.Windows.Forms.TextBox()
        Me.tvMachine = New System.Windows.Forms.TreeView()
        Me.BackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.PB_loading = New System.Windows.Forms.PictureBox()
        Me.GB_ReportType = New System.Windows.Forms.GroupBox()
        Me.rbMonthly = New System.Windows.Forms.RadioButton()
        Me.rbWeekly = New System.Windows.Forms.RadioButton()
        Me.rbDaily = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpReportDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbReportType = New System.Windows.Forms.ComboBox()
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grpDowntime = New System.Windows.Forms.GroupBox()
        Me.chkSetup = New System.Windows.Forms.CheckBox()
        Me.chkProduction = New System.Windows.Forms.CheckBox()
        Me.chkOnlySumary = New System.Windows.Forms.CheckBox()
        Me.cmbScale = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbSorting = New System.Windows.Forms.ComboBox()
        Me.cmbReports = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.PB_loading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_ReportType.SuspendLayout()
        Me.grpDowntime.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnGenerateReport
        '
        Me.btnGenerateReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGenerateReport.AutoEllipsis = True
        Me.btnGenerateReport.Location = New System.Drawing.Point(223, 681)
        Me.btnGenerateReport.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGenerateReport.Name = "btnGenerateReport"
        Me.btnGenerateReport.Size = New System.Drawing.Size(147, 33)
        Me.btnGenerateReport.TabIndex = 20
        Me.btnGenerateReport.Text = "Generate Report"
        Me.btnGenerateReport.UseVisualStyleBackColor = True
        '
        'btnSetDefaultPath
        '
        Me.btnSetDefaultPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSetDefaultPath.Location = New System.Drawing.Point(300, 645)
        Me.btnSetDefaultPath.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSetDefaultPath.Name = "btnSetDefaultPath"
        Me.btnSetDefaultPath.Size = New System.Drawing.Size(70, 25)
        Me.btnSetDefaultPath.TabIndex = 3
        Me.btnSetDefaultPath.Text = "Default"
        Me.btnSetDefaultPath.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 626)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Export folder"
        '
        'btnBrowseFolder
        '
        Me.btnBrowseFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseFolder.Location = New System.Drawing.Point(261, 645)
        Me.btnBrowseFolder.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBrowseFolder.Name = "btnBrowseFolder"
        Me.btnBrowseFolder.Size = New System.Drawing.Size(35, 25)
        Me.btnBrowseFolder.TabIndex = 2
        Me.btnBrowseFolder.Text = "..."
        Me.btnBrowseFolder.UseVisualStyleBackColor = True
        '
        'txtPathReport
        '
        Me.txtPathReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtPathReport.Location = New System.Drawing.Point(8, 645)
        Me.txtPathReport.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPathReport.Name = "txtPathReport"
        Me.txtPathReport.Size = New System.Drawing.Size(244, 20)
        Me.txtPathReport.TabIndex = 15
        '
        'tvMachine
        '
        Me.tvMachine.AllowDrop = True
        Me.tvMachine.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvMachine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tvMachine.CheckBoxes = True
        Me.tvMachine.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvMachine.HotTracking = True
        Me.tvMachine.LabelEdit = True
        Me.tvMachine.LineColor = System.Drawing.Color.Blue
        Me.tvMachine.Location = New System.Drawing.Point(8, 280)
        Me.tvMachine.Margin = New System.Windows.Forms.Padding(4)
        Me.tvMachine.Name = "tvMachine"
        Me.tvMachine.Size = New System.Drawing.Size(362, 337)
        Me.tvMachine.TabIndex = 10
        '
        'BackgroundWorker
        '
        '
        'PB_loading
        '
        Me.PB_loading.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_loading.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.loading_aero
        Me.PB_loading.Location = New System.Drawing.Point(8, 681)
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
        Me.GB_ReportType.Controls.Add(Me.rbMonthly)
        Me.GB_ReportType.Controls.Add(Me.rbWeekly)
        Me.GB_ReportType.Controls.Add(Me.rbDaily)
        Me.GB_ReportType.Location = New System.Drawing.Point(8, 198)
        Me.GB_ReportType.Margin = New System.Windows.Forms.Padding(4)
        Me.GB_ReportType.Name = "GB_ReportType"
        Me.GB_ReportType.Padding = New System.Windows.Forms.Padding(4)
        Me.GB_ReportType.Size = New System.Drawing.Size(362, 47)
        Me.GB_ReportType.TabIndex = 6
        Me.GB_ReportType.TabStop = False
        Me.GB_ReportType.Text = "Interval time"
        '
        'rbMonthly
        '
        Me.rbMonthly.AutoSize = True
        Me.rbMonthly.Location = New System.Drawing.Point(260, 18)
        Me.rbMonthly.Margin = New System.Windows.Forms.Padding(4)
        Me.rbMonthly.Name = "rbMonthly"
        Me.rbMonthly.Size = New System.Drawing.Size(62, 17)
        Me.rbMonthly.TabIndex = 2
        Me.rbMonthly.Text = "Monthly"
        Me.rbMonthly.UseVisualStyleBackColor = True
        '
        'rbWeekly
        '
        Me.rbWeekly.AutoSize = True
        Me.rbWeekly.Location = New System.Drawing.Point(131, 18)
        Me.rbWeekly.Margin = New System.Windows.Forms.Padding(4)
        Me.rbWeekly.Name = "rbWeekly"
        Me.rbWeekly.Size = New System.Drawing.Size(61, 17)
        Me.rbWeekly.TabIndex = 1
        Me.rbWeekly.Text = "Weekly"
        Me.rbWeekly.UseVisualStyleBackColor = True
        '
        'rbDaily
        '
        Me.rbDaily.AutoSize = True
        Me.rbDaily.Checked = True
        Me.rbDaily.Location = New System.Drawing.Point(9, 18)
        Me.rbDaily.Margin = New System.Windows.Forms.Padding(4)
        Me.rbDaily.Name = "rbDaily"
        Me.rbDaily.Size = New System.Drawing.Size(48, 17)
        Me.rbDaily.TabIndex = 0
        Me.rbDaily.TabStop = True
        Me.rbDaily.Text = "Daily"
        Me.rbDaily.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 172)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Report date:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpReportDate
        '
        Me.dtpReportDate.CustomFormat = "dd MMMM yyyy-ddd"
        Me.dtpReportDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReportDate.Location = New System.Drawing.Point(85, 166)
        Me.dtpReportDate.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpReportDate.Name = "dtpReportDate"
        Me.dtpReportDate.Size = New System.Drawing.Size(279, 20)
        Me.dtpReportDate.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(5, 49)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Report type:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbReportType
        '
        Me.cmbReportType.FormattingEnabled = True
        Me.cmbReportType.Items.AddRange(New Object() {"Availability", "Downtime"})
        Me.cmbReportType.Location = New System.Drawing.Point(85, 46)
        Me.cmbReportType.Name = "cmbReportType"
        Me.cmbReportType.Size = New System.Drawing.Size(279, 21)
        Me.cmbReportType.TabIndex = 41
        '
        'lblPeriod
        '
        Me.lblPeriod.Location = New System.Drawing.Point(5, 249)
        Me.lblPeriod.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(365, 27)
        Me.lblPeriod.TabIndex = 42
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(7, 16)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "Sorting:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpDowntime
        '
        Me.grpDowntime.Controls.Add(Me.chkSetup)
        Me.grpDowntime.Controls.Add(Me.chkProduction)
        Me.grpDowntime.Controls.Add(Me.chkOnlySumary)
        Me.grpDowntime.Controls.Add(Me.cmbScale)
        Me.grpDowntime.Controls.Add(Me.Label4)
        Me.grpDowntime.Controls.Add(Me.cmbSorting)
        Me.grpDowntime.Controls.Add(Me.Label3)
        Me.grpDowntime.Location = New System.Drawing.Point(8, 73)
        Me.grpDowntime.Name = "grpDowntime"
        Me.grpDowntime.Size = New System.Drawing.Size(362, 85)
        Me.grpDowntime.TabIndex = 44
        Me.grpDowntime.TabStop = False
        '
        'chkSetup
        '
        Me.chkSetup.AutoSize = True
        Me.chkSetup.Checked = True
        Me.chkSetup.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSetup.Location = New System.Drawing.Point(270, 63)
        Me.chkSetup.Name = "chkSetup"
        Me.chkSetup.Size = New System.Drawing.Size(54, 17)
        Me.chkSetup.TabIndex = 49
        Me.chkSetup.Text = "Setup"
        Me.chkSetup.UseVisualStyleBackColor = True
        '
        'chkProduction
        '
        Me.chkProduction.AutoSize = True
        Me.chkProduction.Checked = True
        Me.chkProduction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkProduction.Location = New System.Drawing.Point(270, 40)
        Me.chkProduction.Name = "chkProduction"
        Me.chkProduction.Size = New System.Drawing.Size(77, 17)
        Me.chkProduction.TabIndex = 48
        Me.chkProduction.Text = "Production"
        Me.chkProduction.UseVisualStyleBackColor = True
        '
        'chkOnlySumary
        '
        Me.chkOnlySumary.AutoSize = True
        Me.chkOnlySumary.Location = New System.Drawing.Point(77, 40)
        Me.chkOnlySumary.Name = "chkOnlySumary"
        Me.chkOnlySumary.Size = New System.Drawing.Size(136, 17)
        Me.chkOnlySumary.TabIndex = 47
        Me.chkOnlySumary.Text = "Consolidated page only"
        Me.chkOnlySumary.UseVisualStyleBackColor = True
        '
        'cmbScale
        '
        Me.cmbScale.FormattingEnabled = True
        Me.cmbScale.Items.AddRange(New Object() {"Hours", "Minutes"})
        Me.cmbScale.Location = New System.Drawing.Point(270, 13)
        Me.cmbScale.Name = "cmbScale"
        Me.cmbScale.Size = New System.Drawing.Size(86, 21)
        Me.cmbScale.TabIndex = 46
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(200, 16)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Scale:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbSorting
        '
        Me.cmbSorting.FormattingEnabled = True
        Me.cmbSorting.Items.AddRange(New Object() {"Value", "Name"})
        Me.cmbSorting.Location = New System.Drawing.Point(77, 13)
        Me.cmbSorting.Name = "cmbSorting"
        Me.cmbSorting.Size = New System.Drawing.Size(86, 21)
        Me.cmbSorting.TabIndex = 44
        '
        'cmbReports
        '
        Me.cmbReports.FormattingEnabled = True
        Me.cmbReports.Items.AddRange(New Object() {"Availability", "Downtime"})
        Me.cmbReports.Location = New System.Drawing.Point(85, 12)
        Me.cmbReports.Name = "cmbReports"
        Me.cmbReports.Size = New System.Drawing.Size(279, 21)
        Me.cmbReports.TabIndex = 46
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(5, 15)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Reports:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Reporting_availability
        '
        Me.AcceptButton = Me.btnGenerateReport
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(381, 725)
        Me.Controls.Add(Me.cmbReports)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.grpDowntime)
        Me.Controls.Add(Me.lblPeriod)
        Me.Controls.Add(Me.cmbReportType)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpReportDate)
        Me.Controls.Add(Me.GB_ReportType)
        Me.Controls.Add(Me.PB_loading)
        Me.Controls.Add(Me.tvMachine)
        Me.Controls.Add(Me.btnGenerateReport)
        Me.Controls.Add(Me.btnSetDefaultPath)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnBrowseFolder)
        Me.Controls.Add(Me.txtPathReport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "Reporting_availability"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reporting"
        CType(Me.PB_loading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GB_ReportType.ResumeLayout(False)
        Me.GB_ReportType.PerformLayout()
        Me.grpDowntime.ResumeLayout(False)
        Me.grpDowntime.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGenerateReport As System.Windows.Forms.Button
    Friend WithEvents btnSetDefaultPath As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnBrowseFolder As System.Windows.Forms.Button
    Friend WithEvents txtPathReport As System.Windows.Forms.TextBox
    Friend WithEvents tvMachine As System.Windows.Forms.TreeView
    Friend WithEvents BackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents PB_loading As System.Windows.Forms.PictureBox
    Friend WithEvents GB_ReportType As System.Windows.Forms.GroupBox
    Friend WithEvents rbMonthly As System.Windows.Forms.RadioButton
    Friend WithEvents rbWeekly As System.Windows.Forms.RadioButton
    Friend WithEvents rbDaily As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpReportDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbReportType As ComboBox
    Friend WithEvents lblPeriod As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents grpDowntime As GroupBox
    Friend WithEvents cmbSorting As ComboBox
    Friend WithEvents cmbScale As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents chkOnlySumary As CheckBox
    Friend WithEvents chkProduction As CheckBox
    Friend WithEvents chkSetup As CheckBox
    Friend WithEvents cmbReports As ComboBox
    Friend WithEvents Label5 As Label
End Class

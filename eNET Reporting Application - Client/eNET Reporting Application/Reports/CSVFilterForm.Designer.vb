<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CSVFilterForm
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tViewMachines = New System.Windows.Forms.TreeView()
        Me.gboxGrid = New System.Windows.Forms.GroupBox()
        Me.btnExportCsv = New System.Windows.Forms.Button()
        Me.dgvReport = New System.Windows.Forms.DataGridView()
        Me.colMachineId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMachineName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEventDatetime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colShift = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPartNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOperation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPartCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFeedrateOvr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRapidOvr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSpindleOvr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gboxFilter = New System.Windows.Forms.GroupBox()
        Me.btnGenerateReport = New System.Windows.Forms.Button()
        Me.gboxCustom = New System.Windows.Forms.GroupBox()
        Me.dtpCustomEnd = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dtpCustomStart = New System.Windows.Forms.DateTimePicker()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.gboxMonth = New System.Windows.Forms.GroupBox()
        Me.lblMonthPeriod = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtpMonth = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.gboxWeek = New System.Windows.Forms.GroupBox()
        Me.lblWeekPeriod = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbWeekStart = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpWeek = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.gboxDay = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkShift3 = New System.Windows.Forms.CheckBox()
        Me.chkShift2 = New System.Windows.Forms.CheckBox()
        Me.chkShift1 = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpDay = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rBtnCustom = New System.Windows.Forms.RadioButton()
        Me.rBtnMonth = New System.Windows.Forms.RadioButton()
        Me.rBtnWeek = New System.Windows.Forms.RadioButton()
        Me.rBtnDay = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lboxPartNumber = New System.Windows.Forms.CheckedListBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.gboxGrid.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboxFilter.SuspendLayout()
        Me.gboxCustom.SuspendLayout()
        Me.gboxMonth.SuspendLayout()
        Me.gboxWeek.SuspendLayout()
        Me.gboxDay.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.tViewMachines)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(288, 330)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(273, 31)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Machines"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tViewMachines
        '
        Me.tViewMachines.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tViewMachines.CheckBoxes = True
        Me.tViewMachines.Location = New System.Drawing.Point(7, 53)
        Me.tViewMachines.Name = "tViewMachines"
        Me.tViewMachines.Size = New System.Drawing.Size(270, 260)
        Me.tViewMachines.TabIndex = 0
        '
        'gboxGrid
        '
        Me.gboxGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxGrid.Controls.Add(Me.btnExportCsv)
        Me.gboxGrid.Controls.Add(Me.dgvReport)
        Me.gboxGrid.Location = New System.Drawing.Point(14, 807)
        Me.gboxGrid.Name = "gboxGrid"
        Me.gboxGrid.Size = New System.Drawing.Size(960, 346)
        Me.gboxGrid.TabIndex = 1
        Me.gboxGrid.TabStop = False
        '
        'btnExportCsv
        '
        Me.btnExportCsv.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExportCsv.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportCsv.Location = New System.Drawing.Point(780, 14)
        Me.btnExportCsv.Name = "btnExportCsv"
        Me.btnExportCsv.Size = New System.Drawing.Size(174, 37)
        Me.btnExportCsv.TabIndex = 13
        Me.btnExportCsv.Text = "Export CSV"
        Me.btnExportCsv.UseVisualStyleBackColor = True
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colMachineId, Me.colMachineName, Me.colEventDatetime, Me.colShift, Me.colStatus, Me.colPartNumber, Me.colOperation, Me.colPartCount, Me.colFeedrateOvr, Me.colRapidOvr, Me.colSpindleOvr})
        Me.dgvReport.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvReport.Location = New System.Drawing.Point(7, 57)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.Size = New System.Drawing.Size(947, 283)
        Me.dgvReport.TabIndex = 0
        '
        'colMachineId
        '
        Me.colMachineId.DataPropertyName = "MachineId"
        Me.colMachineId.HeaderText = "ID"
        Me.colMachineId.Name = "colMachineId"
        Me.colMachineId.ReadOnly = True
        Me.colMachineId.Width = 43
        '
        'colMachineName
        '
        Me.colMachineName.DataPropertyName = "MachineName"
        Me.colMachineName.HeaderText = "Machine Name"
        Me.colMachineName.Name = "colMachineName"
        Me.colMachineName.ReadOnly = True
        Me.colMachineName.Width = 113
        '
        'colEventDatetime
        '
        Me.colEventDatetime.DataPropertyName = "EventDateTime"
        DataGridViewCellStyle3.Format = "yyyy-MM-dd HH:mm:ss"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.colEventDatetime.DefaultCellStyle = DataGridViewCellStyle3
        Me.colEventDatetime.HeaderText = "Date Time"
        Me.colEventDatetime.Name = "colEventDatetime"
        Me.colEventDatetime.ReadOnly = True
        Me.colEventDatetime.Width = 85
        '
        'colShift
        '
        Me.colShift.DataPropertyName = "Shift"
        Me.colShift.HeaderText = "Shift"
        Me.colShift.Name = "colShift"
        Me.colShift.ReadOnly = True
        Me.colShift.Width = 56
        '
        'colStatus
        '
        Me.colStatus.DataPropertyName = "Status"
        Me.colStatus.HeaderText = "Status"
        Me.colStatus.Name = "colStatus"
        Me.colStatus.ReadOnly = True
        Me.colStatus.Width = 64
        '
        'colPartNumber
        '
        Me.colPartNumber.DataPropertyName = "PartNumber"
        Me.colPartNumber.HeaderText = "Part Number"
        Me.colPartNumber.Name = "colPartNumber"
        Me.colPartNumber.ReadOnly = True
        '
        'colOperation
        '
        Me.colOperation.DataPropertyName = "Operation"
        Me.colOperation.HeaderText = "Operation"
        Me.colOperation.Name = "colOperation"
        Me.colOperation.ReadOnly = True
        Me.colOperation.Width = 85
        '
        'colPartCount
        '
        Me.colPartCount.DataPropertyName = "PartCount"
        Me.colPartCount.HeaderText = "Part Count"
        Me.colPartCount.Name = "colPartCount"
        Me.colPartCount.ReadOnly = True
        Me.colPartCount.Width = 89
        '
        'colFeedrateOvr
        '
        Me.colFeedrateOvr.DataPropertyName = "Feedrate"
        Me.colFeedrateOvr.HeaderText = "Feedrate Ovr"
        Me.colFeedrateOvr.Name = "colFeedrateOvr"
        Me.colFeedrateOvr.ReadOnly = True
        Me.colFeedrateOvr.Width = 99
        '
        'colRapidOvr
        '
        Me.colRapidOvr.DataPropertyName = "Rapid"
        Me.colRapidOvr.HeaderText = "Rapid Ovr"
        Me.colRapidOvr.Name = "colRapidOvr"
        Me.colRapidOvr.ReadOnly = True
        Me.colRapidOvr.Width = 84
        '
        'colSpindleOvr
        '
        Me.colSpindleOvr.DataPropertyName = "Spindle"
        Me.colSpindleOvr.HeaderText = "Spindle Ovr"
        Me.colSpindleOvr.Name = "colSpindleOvr"
        Me.colSpindleOvr.ReadOnly = True
        Me.colSpindleOvr.Width = 93
        '
        'gboxFilter
        '
        Me.gboxFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxFilter.Controls.Add(Me.Label11)
        Me.gboxFilter.Controls.Add(Me.lboxPartNumber)
        Me.gboxFilter.Controls.Add(Me.btnGenerateReport)
        Me.gboxFilter.Controls.Add(Me.gboxCustom)
        Me.gboxFilter.Controls.Add(Me.gboxMonth)
        Me.gboxFilter.Controls.Add(Me.gboxWeek)
        Me.gboxFilter.Controls.Add(Me.gboxDay)
        Me.gboxFilter.Controls.Add(Me.rBtnCustom)
        Me.gboxFilter.Controls.Add(Me.rBtnMonth)
        Me.gboxFilter.Controls.Add(Me.rBtnWeek)
        Me.gboxFilter.Controls.Add(Me.rBtnDay)
        Me.gboxFilter.Controls.Add(Me.Label3)
        Me.gboxFilter.Controls.Add(Me.Label2)
        Me.gboxFilter.Location = New System.Drawing.Point(308, 14)
        Me.gboxFilter.Name = "gboxFilter"
        Me.gboxFilter.Size = New System.Drawing.Size(666, 787)
        Me.gboxFilter.TabIndex = 2
        Me.gboxFilter.TabStop = False
        '
        'btnGenerateReport
        '
        Me.btnGenerateReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerateReport.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerateReport.Location = New System.Drawing.Point(486, 744)
        Me.btnGenerateReport.Name = "btnGenerateReport"
        Me.btnGenerateReport.Size = New System.Drawing.Size(174, 37)
        Me.btnGenerateReport.TabIndex = 12
        Me.btnGenerateReport.Text = "Generate"
        Me.btnGenerateReport.UseVisualStyleBackColor = True
        '
        'gboxCustom
        '
        Me.gboxCustom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxCustom.Controls.Add(Me.dtpCustomEnd)
        Me.gboxCustom.Controls.Add(Me.Label14)
        Me.gboxCustom.Controls.Add(Me.Label18)
        Me.gboxCustom.Controls.Add(Me.dtpCustomStart)
        Me.gboxCustom.Controls.Add(Me.Label19)
        Me.gboxCustom.Location = New System.Drawing.Point(10, 578)
        Me.gboxCustom.Name = "gboxCustom"
        Me.gboxCustom.Size = New System.Drawing.Size(391, 160)
        Me.gboxCustom.TabIndex = 11
        Me.gboxCustom.TabStop = False
        '
        'dtpCustomEnd
        '
        Me.dtpCustomEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCustomEnd.Location = New System.Drawing.Point(114, 91)
        Me.dtpCustomEnd.Name = "dtpCustomEnd"
        Me.dtpCustomEnd.Size = New System.Drawing.Size(140, 23)
        Me.dtpCustomEnd.TabIndex = 13
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(8, 91)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(100, 20)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "End Date :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(6, 19)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(362, 20)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "Select the period to generate the report"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpCustomStart
        '
        Me.dtpCustomStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCustomStart.Location = New System.Drawing.Point(114, 55)
        Me.dtpCustomStart.Name = "dtpCustomStart"
        Me.dtpCustomStart.Size = New System.Drawing.Size(140, 23)
        Me.dtpCustomStart.TabIndex = 5
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(8, 55)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(100, 20)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "Start Date :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gboxMonth
        '
        Me.gboxMonth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxMonth.Controls.Add(Me.lblMonthPeriod)
        Me.gboxMonth.Controls.Add(Me.Label13)
        Me.gboxMonth.Controls.Add(Me.Label15)
        Me.gboxMonth.Controls.Add(Me.dtpMonth)
        Me.gboxMonth.Controls.Add(Me.Label16)
        Me.gboxMonth.Location = New System.Drawing.Point(10, 412)
        Me.gboxMonth.Name = "gboxMonth"
        Me.gboxMonth.Size = New System.Drawing.Size(391, 160)
        Me.gboxMonth.TabIndex = 10
        Me.gboxMonth.TabStop = False
        '
        'lblMonthPeriod
        '
        Me.lblMonthPeriod.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthPeriod.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblMonthPeriod.Location = New System.Drawing.Point(111, 91)
        Me.lblMonthPeriod.Name = "lblMonthPeriod"
        Me.lblMonthPeriod.Size = New System.Drawing.Size(199, 20)
        Me.lblMonthPeriod.TabIndex = 15
        Me.lblMonthPeriod.Text = "Report period :"
        Me.lblMonthPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(8, 91)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(100, 20)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "Report period :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(6, 19)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(362, 20)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "Select a day of the month to generate the report"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpMonth
        '
        Me.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMonth.Location = New System.Drawing.Point(114, 55)
        Me.dtpMonth.Name = "dtpMonth"
        Me.dtpMonth.Size = New System.Drawing.Size(140, 23)
        Me.dtpMonth.TabIndex = 5
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(8, 55)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(100, 20)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "Date :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gboxWeek
        '
        Me.gboxWeek.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxWeek.Controls.Add(Me.lblWeekPeriod)
        Me.gboxWeek.Controls.Add(Me.Label10)
        Me.gboxWeek.Controls.Add(Me.cmbWeekStart)
        Me.gboxWeek.Controls.Add(Me.Label9)
        Me.gboxWeek.Controls.Add(Me.Label8)
        Me.gboxWeek.Controls.Add(Me.dtpWeek)
        Me.gboxWeek.Controls.Add(Me.Label7)
        Me.gboxWeek.Location = New System.Drawing.Point(10, 246)
        Me.gboxWeek.Name = "gboxWeek"
        Me.gboxWeek.Size = New System.Drawing.Size(391, 160)
        Me.gboxWeek.TabIndex = 9
        Me.gboxWeek.TabStop = False
        '
        'lblWeekPeriod
        '
        Me.lblWeekPeriod.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeekPeriod.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblWeekPeriod.Location = New System.Drawing.Point(111, 127)
        Me.lblWeekPeriod.Name = "lblWeekPeriod"
        Me.lblWeekPeriod.Size = New System.Drawing.Size(199, 20)
        Me.lblWeekPeriod.TabIndex = 15
        Me.lblWeekPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(8, 127)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 20)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "Report period :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbWeekStart
        '
        Me.cmbWeekStart.FormattingEnabled = True
        Me.cmbWeekStart.Items.AddRange(New Object() {"Sunday", "Monday"})
        Me.cmbWeekStart.Location = New System.Drawing.Point(114, 91)
        Me.cmbWeekStart.Name = "cmbWeekStart"
        Me.cmbWeekStart.Size = New System.Drawing.Size(140, 23)
        Me.cmbWeekStart.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(8, 91)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 20)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Week Start :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(362, 20)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Select a day of the week to generate the report"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpWeek
        '
        Me.dtpWeek.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWeek.Location = New System.Drawing.Point(114, 55)
        Me.dtpWeek.Name = "dtpWeek"
        Me.dtpWeek.Size = New System.Drawing.Size(140, 23)
        Me.dtpWeek.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(8, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 20)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gboxDay
        '
        Me.gboxDay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxDay.Controls.Add(Me.Label6)
        Me.gboxDay.Controls.Add(Me.chkShift3)
        Me.gboxDay.Controls.Add(Me.chkShift2)
        Me.gboxDay.Controls.Add(Me.chkShift1)
        Me.gboxDay.Controls.Add(Me.Label5)
        Me.gboxDay.Controls.Add(Me.dtpDay)
        Me.gboxDay.Controls.Add(Me.Label4)
        Me.gboxDay.Location = New System.Drawing.Point(10, 80)
        Me.gboxDay.Name = "gboxDay"
        Me.gboxDay.Size = New System.Drawing.Size(391, 160)
        Me.gboxDay.TabIndex = 8
        Me.gboxDay.TabStop = False
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(362, 20)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Select a date and shift to generate the report"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkShift3
        '
        Me.chkShift3.AutoSize = True
        Me.chkShift3.Location = New System.Drawing.Point(244, 93)
        Me.chkShift3.Name = "chkShift3"
        Me.chkShift3.Size = New System.Drawing.Size(59, 19)
        Me.chkShift3.TabIndex = 9
        Me.chkShift3.Text = "Shift 3"
        Me.chkShift3.UseVisualStyleBackColor = True
        '
        'chkShift2
        '
        Me.chkShift2.AutoSize = True
        Me.chkShift2.Location = New System.Drawing.Point(179, 93)
        Me.chkShift2.Name = "chkShift2"
        Me.chkShift2.Size = New System.Drawing.Size(59, 19)
        Me.chkShift2.TabIndex = 8
        Me.chkShift2.Text = "Shift 2"
        Me.chkShift2.UseVisualStyleBackColor = True
        '
        'chkShift1
        '
        Me.chkShift1.AutoSize = True
        Me.chkShift1.Location = New System.Drawing.Point(114, 93)
        Me.chkShift1.Name = "chkShift1"
        Me.chkShift1.Size = New System.Drawing.Size(59, 19)
        Me.chkShift1.TabIndex = 7
        Me.chkShift1.Text = "Shift 1"
        Me.chkShift1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 20)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Shifts :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpDay
        '
        Me.dtpDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDay.Location = New System.Drawing.Point(114, 55)
        Me.dtpDay.Name = "dtpDay"
        Me.dtpDay.Size = New System.Drawing.Size(140, 23)
        Me.dtpDay.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 20)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rBtnCustom
        '
        Me.rBtnCustom.AutoSize = True
        Me.rBtnCustom.Location = New System.Drawing.Point(251, 54)
        Me.rBtnCustom.Name = "rBtnCustom"
        Me.rBtnCustom.Size = New System.Drawing.Size(67, 19)
        Me.rBtnCustom.TabIndex = 7
        Me.rBtnCustom.TabStop = True
        Me.rBtnCustom.Text = "Custom"
        Me.rBtnCustom.UseVisualStyleBackColor = True
        '
        'rBtnMonth
        '
        Me.rBtnMonth.AutoSize = True
        Me.rBtnMonth.Location = New System.Drawing.Point(184, 54)
        Me.rBtnMonth.Name = "rBtnMonth"
        Me.rBtnMonth.Size = New System.Drawing.Size(61, 19)
        Me.rBtnMonth.TabIndex = 6
        Me.rBtnMonth.TabStop = True
        Me.rBtnMonth.Text = "Month"
        Me.rBtnMonth.UseVisualStyleBackColor = True
        '
        'rBtnWeek
        '
        Me.rBtnWeek.AutoSize = True
        Me.rBtnWeek.Location = New System.Drawing.Point(124, 54)
        Me.rBtnWeek.Name = "rBtnWeek"
        Me.rBtnWeek.Size = New System.Drawing.Size(54, 19)
        Me.rBtnWeek.TabIndex = 5
        Me.rBtnWeek.TabStop = True
        Me.rBtnWeek.Text = "Week"
        Me.rBtnWeek.UseVisualStyleBackColor = True
        '
        'rBtnDay
        '
        Me.rBtnDay.AutoSize = True
        Me.rBtnDay.Location = New System.Drawing.Point(73, 54)
        Me.rBtnDay.Name = "rBtnDay"
        Me.rBtnDay.Size = New System.Drawing.Size(45, 19)
        Me.rBtnDay.TabIndex = 4
        Me.rBtnDay.TabStop = True
        Me.rBtnDay.Text = "Day"
        Me.rBtnDay.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(7, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 20)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Period:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(372, 31)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Filters"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lboxPartNumber
        '
        Me.lboxPartNumber.FormattingEnabled = True
        Me.lboxPartNumber.Location = New System.Drawing.Point(407, 53)
        Me.lboxPartNumber.Name = "lboxPartNumber"
        Me.lboxPartNumber.Size = New System.Drawing.Size(253, 184)
        Me.lboxPartNumber.TabIndex = 13
        Me.lboxPartNumber.Visible = False
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(403, 19)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(257, 31)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "Part Number"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label11.Visible = False
        '
        'CSVFilterForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 1165)
        Me.Controls.Add(Me.gboxFilter)
        Me.Controls.Add(Me.gboxGrid)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "CSVFilterForm"
        Me.Text = "CSV Filter"
        Me.GroupBox1.ResumeLayout(False)
        Me.gboxGrid.ResumeLayout(False)
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboxFilter.ResumeLayout(False)
        Me.gboxFilter.PerformLayout()
        Me.gboxCustom.ResumeLayout(False)
        Me.gboxMonth.ResumeLayout(False)
        Me.gboxWeek.ResumeLayout(False)
        Me.gboxDay.ResumeLayout(False)
        Me.gboxDay.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents tViewMachines As TreeView
    Friend WithEvents gboxGrid As GroupBox
    Friend WithEvents dgvReport As DataGridView
    Friend WithEvents gboxFilter As GroupBox
    Friend WithEvents rBtnMonth As RadioButton
    Friend WithEvents rBtnWeek As RadioButton
    Friend WithEvents rBtnDay As RadioButton
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents gboxDay As GroupBox
    Friend WithEvents chkShift3 As CheckBox
    Friend WithEvents chkShift2 As CheckBox
    Friend WithEvents chkShift1 As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpDay As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents rBtnCustom As RadioButton
    Friend WithEvents gboxWeek As GroupBox
    Friend WithEvents dtpWeek As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblWeekPeriod As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents cmbWeekStart As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents gboxCustom As GroupBox
    Friend WithEvents dtpCustomEnd As DateTimePicker
    Friend WithEvents Label14 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents dtpCustomStart As DateTimePicker
    Friend WithEvents Label19 As Label
    Friend WithEvents gboxMonth As GroupBox
    Friend WithEvents lblMonthPeriod As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents dtpMonth As DateTimePicker
    Friend WithEvents Label16 As Label
    Friend WithEvents btnGenerateReport As Button
    Friend WithEvents colMachineId As DataGridViewTextBoxColumn
    Friend WithEvents colMachineName As DataGridViewTextBoxColumn
    Friend WithEvents colEventDatetime As DataGridViewTextBoxColumn
    Friend WithEvents colShift As DataGridViewTextBoxColumn
    Friend WithEvents colStatus As DataGridViewTextBoxColumn
    Friend WithEvents colPartNumber As DataGridViewTextBoxColumn
    Friend WithEvents colOperation As DataGridViewTextBoxColumn
    Friend WithEvents colPartCount As DataGridViewTextBoxColumn
    Friend WithEvents colFeedrateOvr As DataGridViewTextBoxColumn
    Friend WithEvents colRapidOvr As DataGridViewTextBoxColumn
    Friend WithEvents colSpindleOvr As DataGridViewTextBoxColumn
    Friend WithEvents btnExportCsv As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents lboxPartNumber As CheckedListBox
End Class

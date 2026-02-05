<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AutoReporting
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AutoReporting))
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BG_generate = New System.ComponentModel.BackgroundWorker()
        Me.btnModify = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.dgvMachineList = New System.Windows.Forms.DataGridView()
        Me.ReportOrNot = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.MachId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MachName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EnetMachName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvReports = New System.Windows.Forms.DataGridView()
        Me.Generate = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Task_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Report_Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Periodicity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Day = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Output_folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MachineName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MailTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Enabled = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReportId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvEmail = New System.Windows.Forms.DataGridView()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.btnAddNew = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.GrBx_Parameter = New System.Windows.Forms.GroupBox()
        Me.panelToday = New System.Windows.Forms.Panel()
        Me.chkAllShifts = New System.Windows.Forms.CheckBox()
        Me.chkShift3 = New System.Windows.Forms.CheckBox()
        Me.chkShift2 = New System.Windows.Forms.CheckBox()
        Me.chkShift1 = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnGenerateSelectedReports = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkShowOnTimeline = New System.Windows.Forms.CheckBox()
        Me.chkShowSetupCycleOnTime = New System.Windows.Forms.CheckBox()
        Me.panelDowntime = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtEventMinutes = New System.Windows.Forms.TextBox()
        Me.chkProduction = New System.Windows.Forms.CheckBox()
        Me.cbScale = New System.Windows.Forms.ComboBox()
        Me.chkHideEvents = New System.Windows.Forms.CheckBox()
        Me.lblScale = New System.Windows.Forms.Label()
        Me.chkOnlySumary = New System.Windows.Forms.CheckBox()
        Me.chkSetup = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbChartSort = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbSelectReports = New System.Windows.Forms.ComboBox()
        Me.txtTaskName = New System.Windows.Forms.TextBox()
        Me.cbReportType = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblReportDisabled = New System.Windows.Forms.Label()
        Me.cbPeriod = New System.Windows.Forms.ComboBox()
        Me.txtReportTitle = New System.Windows.Forms.TextBox()
        Me.grpGeneration = New System.Windows.Forms.GroupBox()
        Me.chkDisableReport = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpGenerationTime = New System.Windows.Forms.DateTimePicker()
        Me.chkShortFileName = New System.Windows.Forms.CheckBox()
        Me.chkSunday = New System.Windows.Forms.CheckBox()
        Me.btnCustomEmail = New System.Windows.Forms.Button()
        Me.BTN_Output = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtOutputFolder = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.panelWeekly = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbWeekEnd = New System.Windows.Forms.ComboBox()
        Me.cbDayOfWeek = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbWeekStart = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.grpDowntime = New System.Windows.Forms.GroupBox()
        Me.grpWeekly = New System.Windows.Forms.GroupBox()
        Me.grpToday = New System.Windows.Forms.GroupBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkMonday = New System.Windows.Forms.CheckBox()
        Me.chkTuesday = New System.Windows.Forms.CheckBox()
        Me.chkWednesday = New System.Windows.Forms.CheckBox()
        Me.chkThursday = New System.Windows.Forms.CheckBox()
        Me.chkFriday = New System.Windows.Forms.CheckBox()
        Me.chkSaturday = New System.Windows.Forms.CheckBox()
        CType(Me.dgvMachineList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.dgvReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.dgvEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GrBx_Parameter.SuspendLayout()
        Me.panelToday.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.panelDowntime.SuspendLayout()
        Me.grpGeneration.SuspendLayout()
        Me.panelWeekly.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(46, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(90, 15)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Shifts :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BG_generate
        '
        Me.BG_generate.WorkerReportsProgress = True
        '
        'btnModify
        '
        Me.btnModify.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnModify.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModify.Location = New System.Drawing.Point(158, 24)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(70, 23)
        Me.btnModify.TabIndex = 2
        Me.btnModify.Text = "Modify"
        Me.btnModify.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(324, 24)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(70, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'dgvMachineList
        '
        Me.dgvMachineList.AllowUserToAddRows = False
        Me.dgvMachineList.AllowUserToResizeRows = False
        Me.dgvMachineList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMachineList.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.dgvMachineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMachineList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ReportOrNot, Me.MachId, Me.MachName, Me.EnetMachName})
        Me.dgvMachineList.Location = New System.Drawing.Point(761, 3)
        Me.dgvMachineList.Name = "dgvMachineList"
        Me.dgvMachineList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgvMachineList.RowHeadersVisible = False
        Me.dgvMachineList.Size = New System.Drawing.Size(195, 802)
        Me.dgvMachineList.TabIndex = 9
        '
        'ReportOrNot
        '
        Me.ReportOrNot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ReportOrNot.FillWeight = 20.0!
        Me.ReportOrNot.HeaderText = ""
        Me.ReportOrNot.Name = "ReportOrNot"
        Me.ReportOrNot.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ReportOrNot.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'MachId
        '
        Me.MachId.HeaderText = "Id"
        Me.MachId.Name = "MachId"
        Me.MachId.ReadOnly = True
        Me.MachId.Visible = False
        '
        'MachName
        '
        Me.MachName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.MachName.FillWeight = 80.0!
        Me.MachName.HeaderText = "Machine"
        Me.MachName.Name = "MachName"
        '
        'EnetMachName
        '
        Me.EnetMachName.HeaderText = "EnetMachine"
        Me.EnetMachName.Name = "EnetMachName"
        Me.EnetMachName.ReadOnly = True
        Me.EnetMachName.Visible = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.14214!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.85786!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.dgvMachineList, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(959, 808)
        Me.TableLayoutPanel1.TabIndex = 13
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.dgvReports, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.7811!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.21891!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(754, 804)
        Me.TableLayoutPanel2.TabIndex = 10
        '
        'dgvReports
        '
        Me.dgvReports.AllowUserToAddRows = False
        Me.dgvReports.AllowUserToResizeRows = False
        Me.dgvReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvReports.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.dgvReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReports.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Generate, Me.Task_name, Me.Report_Type, Me.Periodicity, Me.Day, Me.Time, Me.Output_folder, Me.MachineName, Me.MailTo, Me.Enabled, Me.ReportId})
        Me.dgvReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvReports.Location = New System.Drawing.Point(3, 556)
        Me.dgvReports.Name = "dgvReports"
        Me.dgvReports.RowHeadersVisible = False
        Me.dgvReports.Size = New System.Drawing.Size(748, 245)
        Me.dgvReports.TabIndex = 10
        '
        'Generate
        '
        Me.Generate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Generate.FillWeight = 152.2843!
        Me.Generate.HeaderText = ""
        Me.Generate.Name = "Generate"
        Me.Generate.Width = 30
        '
        'Task_name
        '
        Me.Task_name.DataPropertyName = "Task_name"
        Me.Task_name.FillWeight = 89.54314!
        Me.Task_name.HeaderText = "Task Name"
        Me.Task_name.Name = "Task_name"
        Me.Task_name.ReadOnly = True
        '
        'Report_Type
        '
        Me.Report_Type.DataPropertyName = "ReportType"
        Me.Report_Type.FillWeight = 89.54314!
        Me.Report_Type.HeaderText = "Report Type"
        Me.Report_Type.Name = "Report_Type"
        '
        'Periodicity
        '
        Me.Periodicity.DataPropertyName = "ReportPeriod"
        Me.Periodicity.FillWeight = 89.54314!
        Me.Periodicity.HeaderText = "Periodicity"
        Me.Periodicity.Name = "Periodicity"
        Me.Periodicity.ReadOnly = True
        '
        'Day
        '
        Me.Day.DataPropertyName = "Day_"
        Me.Day.HeaderText = "Day"
        Me.Day.Name = "Day"
        Me.Day.ReadOnly = True
        Me.Day.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Day.Visible = False
        '
        'Time
        '
        Me.Time.DataPropertyName = "Time_"
        Me.Time.FillWeight = 89.54314!
        Me.Time.HeaderText = "Time"
        Me.Time.Name = "Time"
        Me.Time.ReadOnly = True
        Me.Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Output_folder
        '
        Me.Output_folder.DataPropertyName = "Output_Folder"
        Me.Output_folder.FillWeight = 89.54314!
        Me.Output_folder.HeaderText = "Output Folder"
        Me.Output_folder.Name = "Output_folder"
        Me.Output_folder.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Output_folder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'MachineName
        '
        Me.MachineName.DataPropertyName = "MachineToReport"
        Me.MachineName.HeaderText = "MachineName"
        Me.MachineName.Name = "MachineName"
        Me.MachineName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MachineName.Visible = False
        '
        'MailTo
        '
        Me.MailTo.HeaderText = "MailTo"
        Me.MailTo.Name = "MailTo"
        Me.MailTo.ReadOnly = True
        Me.MailTo.Visible = False
        '
        'Enabled
        '
        Me.Enabled.DataPropertyName = "Enabled"
        Me.Enabled.HeaderText = "Enabled"
        Me.Enabled.Name = "Enabled"
        Me.Enabled.ReadOnly = True
        Me.Enabled.Visible = False
        '
        'ReportId
        '
        Me.ReportId.DataPropertyName = "Id"
        Me.ReportId.HeaderText = "ReportId"
        Me.ReportId.Name = "ReportId"
        Me.ReportId.ReadOnly = True
        Me.ReportId.Visible = False
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.74678!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.25322!))
        Me.TableLayoutPanel3.Controls.Add(Me.dgvEmail, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(750, 549)
        Me.TableLayoutPanel3.TabIndex = 11
        '
        'dgvEmail
        '
        Me.dgvEmail.AllowUserToAddRows = False
        Me.dgvEmail.AllowUserToResizeRows = False
        Me.dgvEmail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvEmail.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.dgvEmail.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.dgvEmail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumn1, Me.DataGridViewTextBoxColumn1})
        Me.dgvEmail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvEmail.Location = New System.Drawing.Point(548, 3)
        Me.dgvEmail.Name = "dgvEmail"
        Me.dgvEmail.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgvEmail.RowHeadersVisible = False
        Me.dgvEmail.Size = New System.Drawing.Size(199, 543)
        Me.dgvEmail.TabIndex = 8
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewCheckBoxColumn1.FillWeight = 20.0!
        Me.DataGridViewCheckBoxColumn1.HeaderText = ""
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Mail"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.btnGenerate)
        Me.Panel1.Controls.Add(Me.btnAddNew)
        Me.Panel1.Controls.Add(Me.btnRemove)
        Me.Panel1.Controls.Add(Me.btnModify)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.GrBx_Parameter)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(2, 2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(541, 545)
        Me.Panel1.TabIndex = 9
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGenerate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnGenerate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerate.Location = New System.Drawing.Point(234, 24)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(84, 23)
        Me.btnGenerate.TabIndex = 3
        Me.btnGenerate.Text = "Generate Now"
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'btnAddNew
        '
        Me.btnAddNew.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnAddNew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAddNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNew.Location = New System.Drawing.Point(7, 24)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(70, 23)
        Me.btnAddNew.TabIndex = 0
        Me.btnAddNew.Text = "Add New"
        Me.btnAddNew.UseVisualStyleBackColor = False
        '
        'btnRemove
        '
        Me.btnRemove.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.Location = New System.Drawing.Point(82, 24)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(70, 23)
        Me.btnRemove.TabIndex = 1
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = False
        '
        'GrBx_Parameter
        '
        Me.GrBx_Parameter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrBx_Parameter.Controls.Add(Me.panelToday)
        Me.GrBx_Parameter.Controls.Add(Me.Panel2)
        Me.GrBx_Parameter.Controls.Add(Me.panelWeekly)
        Me.GrBx_Parameter.Controls.Add(Me.grpDowntime)
        Me.GrBx_Parameter.Controls.Add(Me.grpWeekly)
        Me.GrBx_Parameter.Controls.Add(Me.grpToday)
        Me.GrBx_Parameter.Location = New System.Drawing.Point(7, 54)
        Me.GrBx_Parameter.MinimumSize = New System.Drawing.Size(430, 275)
        Me.GrBx_Parameter.Name = "GrBx_Parameter"
        Me.GrBx_Parameter.Size = New System.Drawing.Size(530, 488)
        Me.GrBx_Parameter.TabIndex = 8
        Me.GrBx_Parameter.TabStop = False
        Me.GrBx_Parameter.Text = "Parameters"
        '
        'panelToday
        '
        Me.panelToday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelToday.Controls.Add(Me.chkAllShifts)
        Me.panelToday.Controls.Add(Me.chkShift3)
        Me.panelToday.Controls.Add(Me.Label8)
        Me.panelToday.Controls.Add(Me.chkShift2)
        Me.panelToday.Controls.Add(Me.chkShift1)
        Me.panelToday.Location = New System.Drawing.Point(8, 507)
        Me.panelToday.Name = "panelToday"
        Me.panelToday.Size = New System.Drawing.Size(470, 40)
        Me.panelToday.TabIndex = 40
        '
        'chkAllShifts
        '
        Me.chkAllShifts.AutoSize = True
        Me.chkAllShifts.Location = New System.Drawing.Point(144, 12)
        Me.chkAllShifts.Name = "chkAllShifts"
        Me.chkAllShifts.Size = New System.Drawing.Size(66, 17)
        Me.chkAllShifts.TabIndex = 1
        Me.chkAllShifts.Text = "All Shifts"
        Me.chkAllShifts.UseVisualStyleBackColor = True
        '
        'chkShift3
        '
        Me.chkShift3.AutoSize = True
        Me.chkShift3.Location = New System.Drawing.Point(375, 12)
        Me.chkShift3.Name = "chkShift3"
        Me.chkShift3.Size = New System.Drawing.Size(56, 17)
        Me.chkShift3.TabIndex = 4
        Me.chkShift3.Text = "Shift 3"
        Me.chkShift3.UseVisualStyleBackColor = True
        '
        'chkShift2
        '
        Me.chkShift2.AutoSize = True
        Me.chkShift2.Location = New System.Drawing.Point(313, 12)
        Me.chkShift2.Name = "chkShift2"
        Me.chkShift2.Size = New System.Drawing.Size(56, 17)
        Me.chkShift2.TabIndex = 3
        Me.chkShift2.Text = "Shift 2"
        Me.chkShift2.UseVisualStyleBackColor = True
        '
        'chkShift1
        '
        Me.chkShift1.AutoSize = True
        Me.chkShift1.Location = New System.Drawing.Point(251, 12)
        Me.chkShift1.Name = "chkShift1"
        Me.chkShift1.Size = New System.Drawing.Size(56, 17)
        Me.chkShift1.TabIndex = 2
        Me.chkShift1.Text = "Shift 1"
        Me.chkShift1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.AutoScroll = True
        Me.Panel2.Controls.Add(Me.btnGenerateSelectedReports)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.cmbSelectReports)
        Me.Panel2.Controls.Add(Me.txtTaskName)
        Me.Panel2.Controls.Add(Me.cbReportType)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.lblReportDisabled)
        Me.Panel2.Controls.Add(Me.cbPeriod)
        Me.Panel2.Controls.Add(Me.txtReportTitle)
        Me.Panel2.Controls.Add(Me.grpGeneration)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Location = New System.Drawing.Point(6, 19)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(5)
        Me.Panel2.Size = New System.Drawing.Size(518, 465)
        Me.Panel2.TabIndex = 41
        '
        'btnGenerateSelectedReports
        '
        Me.btnGenerateSelectedReports.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnGenerateSelectedReports.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGenerateSelectedReports.Enabled = False
        Me.btnGenerateSelectedReports.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnGenerateSelectedReports.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnGenerateSelectedReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerateSelectedReports.Location = New System.Drawing.Point(316, 434)
        Me.btnGenerateSelectedReports.Name = "btnGenerateSelectedReports"
        Me.btnGenerateSelectedReports.Size = New System.Drawing.Size(164, 23)
        Me.btnGenerateSelectedReports.TabIndex = 37
        Me.btnGenerateSelectedReports.Text = "Regenerate Selected Reports"
        Me.btnGenerateSelectedReports.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkShowOnTimeline)
        Me.GroupBox2.Controls.Add(Me.chkShowSetupCycleOnTime)
        Me.GroupBox2.Controls.Add(Me.panelDowntime)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 107)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(505, 190)
        Me.GroupBox2.TabIndex = 31
        Me.GroupBox2.TabStop = False
        '
        'chkShowOnTimeline
        '
        Me.chkShowOnTimeline.AutoSize = True
        Me.chkShowOnTimeline.Location = New System.Drawing.Point(274, 14)
        Me.chkShowOnTimeline.Name = "chkShowOnTimeline"
        Me.chkShowOnTimeline.Size = New System.Drawing.Size(113, 17)
        Me.chkShowOnTimeline.TabIndex = 40
        Me.chkShowOnTimeline.Text = "Show on Timeline."
        Me.chkShowOnTimeline.UseVisualStyleBackColor = True
        Me.chkShowOnTimeline.Visible = False
        '
        'chkShowSetupCycleOnTime
        '
        Me.chkShowSetupCycleOnTime.AutoSize = True
        Me.chkShowSetupCycleOnTime.Location = New System.Drawing.Point(101, 14)
        Me.chkShowSetupCycleOnTime.Name = "chkShowSetupCycleOnTime"
        Me.chkShowSetupCycleOnTime.Size = New System.Drawing.Size(167, 17)
        Me.chkShowSetupCycleOnTime.TabIndex = 39
        Me.chkShowSetupCycleOnTime.Text = "Show Cycle ON during Setup."
        Me.chkShowSetupCycleOnTime.UseVisualStyleBackColor = True
        '
        'panelDowntime
        '
        Me.panelDowntime.Controls.Add(Me.Label9)
        Me.panelDowntime.Controls.Add(Me.txtEventMinutes)
        Me.panelDowntime.Controls.Add(Me.chkProduction)
        Me.panelDowntime.Controls.Add(Me.cbScale)
        Me.panelDowntime.Controls.Add(Me.chkHideEvents)
        Me.panelDowntime.Controls.Add(Me.lblScale)
        Me.panelDowntime.Controls.Add(Me.chkOnlySumary)
        Me.panelDowntime.Controls.Add(Me.chkSetup)
        Me.panelDowntime.Controls.Add(Me.Label11)
        Me.panelDowntime.Controls.Add(Me.cbChartSort)
        Me.panelDowntime.Location = New System.Drawing.Point(2, 40)
        Me.panelDowntime.Name = "panelDowntime"
        Me.panelDowntime.Size = New System.Drawing.Size(470, 95)
        Me.panelDowntime.TabIndex = 38
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(328, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 15)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "minutes"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEventMinutes
        '
        Me.txtEventMinutes.Enabled = False
        Me.txtEventMinutes.Location = New System.Drawing.Point(292, 70)
        Me.txtEventMinutes.MaxLength = 3
        Me.txtEventMinutes.Name = "txtEventMinutes"
        Me.txtEventMinutes.Size = New System.Drawing.Size(30, 20)
        Me.txtEventMinutes.TabIndex = 6
        '
        'chkProduction
        '
        Me.chkProduction.AutoSize = True
        Me.chkProduction.Checked = True
        Me.chkProduction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkProduction.Location = New System.Drawing.Point(292, 39)
        Me.chkProduction.Name = "chkProduction"
        Me.chkProduction.Size = New System.Drawing.Size(77, 17)
        Me.chkProduction.TabIndex = 3
        Me.chkProduction.Text = "Production"
        Me.chkProduction.UseVisualStyleBackColor = True
        '
        'cbScale
        '
        Me.cbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbScale.FormattingEnabled = True
        Me.cbScale.Items.AddRange(New Object() {"Hours", "Minutes"})
        Me.cbScale.Location = New System.Drawing.Point(142, 37)
        Me.cbScale.Name = "cbScale"
        Me.cbScale.Size = New System.Drawing.Size(101, 21)
        Me.cbScale.TabIndex = 2
        '
        'chkHideEvents
        '
        Me.chkHideEvents.AutoSize = True
        Me.chkHideEvents.Location = New System.Drawing.Point(142, 72)
        Me.chkHideEvents.Name = "chkHideEvents"
        Me.chkHideEvents.Size = New System.Drawing.Size(153, 17)
        Me.chkHideEvents.TabIndex = 5
        Me.chkHideEvents.Text = "Hide events with less then "
        Me.chkHideEvents.UseVisualStyleBackColor = True
        '
        'lblScale
        '
        Me.lblScale.Location = New System.Drawing.Point(46, 39)
        Me.lblScale.Name = "lblScale"
        Me.lblScale.Size = New System.Drawing.Size(90, 15)
        Me.lblScale.TabIndex = 27
        Me.lblScale.Text = "Scale:"
        Me.lblScale.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkOnlySumary
        '
        Me.chkOnlySumary.AutoSize = True
        Me.chkOnlySumary.Location = New System.Drawing.Point(292, 7)
        Me.chkOnlySumary.Name = "chkOnlySumary"
        Me.chkOnlySumary.Size = New System.Drawing.Size(136, 17)
        Me.chkOnlySumary.TabIndex = 1
        Me.chkOnlySumary.Text = "Consolidated page only"
        Me.chkOnlySumary.UseVisualStyleBackColor = True
        '
        'chkSetup
        '
        Me.chkSetup.AutoSize = True
        Me.chkSetup.Checked = True
        Me.chkSetup.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSetup.Location = New System.Drawing.Point(375, 39)
        Me.chkSetup.Name = "chkSetup"
        Me.chkSetup.Size = New System.Drawing.Size(54, 17)
        Me.chkSetup.TabIndex = 4
        Me.chkSetup.Text = "Setup"
        Me.chkSetup.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(12, 7)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(124, 15)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Sort chart's columns by"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbChartSort
        '
        Me.cbChartSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbChartSort.FormattingEnabled = True
        Me.cbChartSort.Items.AddRange(New Object() {"Value", "Name"})
        Me.cbChartSort.Location = New System.Drawing.Point(142, 5)
        Me.cbChartSort.Name = "cbChartSort"
        Me.cbChartSort.Size = New System.Drawing.Size(101, 21)
        Me.cbChartSort.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(11, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Task Name:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(11, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 15)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Report Type:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbSelectReports
        '
        Me.cmbSelectReports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSelectReports.DropDownWidth = 180
        Me.cmbSelectReports.FormattingEnabled = True
        Me.cmbSelectReports.IntegralHeight = False
        Me.cmbSelectReports.Items.AddRange(New Object() {"Select reports", "All reports", "All daily reports", "All today reports", "All yesterday reports", "All weekly reports", "All monthly reports", "None"})
        Me.cmbSelectReports.Location = New System.Drawing.Point(5, 436)
        Me.cmbSelectReports.Name = "cmbSelectReports"
        Me.cmbSelectReports.Size = New System.Drawing.Size(136, 21)
        Me.cmbSelectReports.TabIndex = 36
        '
        'txtTaskName
        '
        Me.txtTaskName.Location = New System.Drawing.Point(106, 12)
        Me.txtTaskName.Name = "txtTaskName"
        Me.txtTaskName.Size = New System.Drawing.Size(136, 20)
        Me.txtTaskName.TabIndex = 0
        '
        'cbReportType
        '
        Me.cbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbReportType.DropDownWidth = 180
        Me.cbReportType.FormattingEnabled = True
        Me.cbReportType.IntegralHeight = False
        Me.cbReportType.Items.AddRange(New Object() {"Availability", "Downtime"})
        Me.cbReportType.Location = New System.Drawing.Point(106, 45)
        Me.cbReportType.Name = "cbReportType"
        Me.cbReportType.Size = New System.Drawing.Size(136, 21)
        Me.cbReportType.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(248, 47)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(90, 15)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "Periodicity:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblReportDisabled
        '
        Me.lblReportDisabled.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportDisabled.ForeColor = System.Drawing.Color.Red
        Me.lblReportDisabled.Location = New System.Drawing.Point(281, 12)
        Me.lblReportDisabled.Name = "lblReportDisabled"
        Me.lblReportDisabled.Size = New System.Drawing.Size(185, 20)
        Me.lblReportDisabled.TabIndex = 34
        Me.lblReportDisabled.Text = "Report Disabled"
        Me.lblReportDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblReportDisabled.Visible = False
        '
        'cbPeriod
        '
        Me.cbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPeriod.Enabled = False
        Me.cbPeriod.FormattingEnabled = True
        Me.cbPeriod.Items.AddRange(New Object() {"Daily - Today", "Daily - Yesterday", "Weekly", "Monthly"})
        Me.cbPeriod.Location = New System.Drawing.Point(344, 45)
        Me.cbPeriod.Name = "cbPeriod"
        Me.cbPeriod.Size = New System.Drawing.Size(122, 21)
        Me.cbPeriod.TabIndex = 2
        '
        'txtReportTitle
        '
        Me.txtReportTitle.Location = New System.Drawing.Point(106, 80)
        Me.txtReportTitle.MaxLength = 120
        Me.txtReportTitle.Name = "txtReportTitle"
        Me.txtReportTitle.Size = New System.Drawing.Size(360, 20)
        Me.txtReportTitle.TabIndex = 3
        '
        'grpGeneration
        '
        Me.grpGeneration.Controls.Add(Me.chkSaturday)
        Me.grpGeneration.Controls.Add(Me.chkFriday)
        Me.grpGeneration.Controls.Add(Me.chkThursday)
        Me.grpGeneration.Controls.Add(Me.chkWednesday)
        Me.grpGeneration.Controls.Add(Me.chkTuesday)
        Me.grpGeneration.Controls.Add(Me.chkMonday)
        Me.grpGeneration.Controls.Add(Me.chkDisableReport)
        Me.grpGeneration.Controls.Add(Me.Label3)
        Me.grpGeneration.Controls.Add(Me.dtpGenerationTime)
        Me.grpGeneration.Controls.Add(Me.chkShortFileName)
        Me.grpGeneration.Controls.Add(Me.chkSunday)
        Me.grpGeneration.Controls.Add(Me.btnCustomEmail)
        Me.grpGeneration.Controls.Add(Me.BTN_Output)
        Me.grpGeneration.Controls.Add(Me.Label5)
        Me.grpGeneration.Controls.Add(Me.txtOutputFolder)
        Me.grpGeneration.Location = New System.Drawing.Point(5, 300)
        Me.grpGeneration.Name = "grpGeneration"
        Me.grpGeneration.Size = New System.Drawing.Size(505, 125)
        Me.grpGeneration.TabIndex = 28
        Me.grpGeneration.TabStop = False
        '
        'chkDisableReport
        '
        Me.chkDisableReport.AutoSize = True
        Me.chkDisableReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDisableReport.Location = New System.Drawing.Point(339, 102)
        Me.chkDisableReport.Name = "chkDisableReport"
        Me.chkDisableReport.Size = New System.Drawing.Size(110, 17)
        Me.chkDisableReport.TabIndex = 33
        Me.chkDisableReport.Text = "Disable Report"
        Me.chkDisableReport.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(6, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 15)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Generation Time:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpGenerationTime
        '
        Me.dtpGenerationTime.CustomFormat = "HH:mm"
        Me.dtpGenerationTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGenerationTime.Location = New System.Drawing.Point(102, 15)
        Me.dtpGenerationTime.Name = "dtpGenerationTime"
        Me.dtpGenerationTime.ShowUpDown = True
        Me.dtpGenerationTime.Size = New System.Drawing.Size(54, 20)
        Me.dtpGenerationTime.TabIndex = 5
        '
        'chkShortFileName
        '
        Me.chkShortFileName.AutoSize = True
        Me.chkShortFileName.Location = New System.Drawing.Point(339, 74)
        Me.chkShortFileName.Margin = New System.Windows.Forms.Padding(2)
        Me.chkShortFileName.Name = "chkShortFileName"
        Me.chkShortFileName.Size = New System.Drawing.Size(126, 17)
        Me.chkShortFileName.TabIndex = 25
        Me.chkShortFileName.Text = "Use Short File Name "
        Me.chkShortFileName.UseVisualStyleBackColor = True
        '
        'chkSunday
        '
        Me.chkSunday.AutoSize = True
        Me.chkSunday.Checked = True
        Me.chkSunday.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSunday.Location = New System.Drawing.Point(172, 18)
        Me.chkSunday.Name = "chkSunday"
        Me.chkSunday.Size = New System.Drawing.Size(45, 17)
        Me.chkSunday.TabIndex = 3
        Me.chkSunday.Text = "Sun"
        Me.chkSunday.UseVisualStyleBackColor = True
        '
        'btnCustomEmail
        '
        Me.btnCustomEmail.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCustomEmail.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCustomEmail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCustomEmail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnCustomEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomEmail.Location = New System.Drawing.Point(102, 92)
        Me.btnCustomEmail.Name = "btnCustomEmail"
        Me.btnCustomEmail.Size = New System.Drawing.Size(163, 23)
        Me.btnCustomEmail.TabIndex = 15
        Me.btnCustomEmail.Text = "Custom email message"
        Me.btnCustomEmail.UseVisualStyleBackColor = False
        '
        'BTN_Output
        '
        Me.BTN_Output.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Output.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Output.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Output.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Output.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Output.Location = New System.Drawing.Point(339, 43)
        Me.BTN_Output.Name = "BTN_Output"
        Me.BTN_Output.Size = New System.Drawing.Size(70, 23)
        Me.BTN_Output.TabIndex = 7
        Me.BTN_Output.Text = "Browse"
        Me.BTN_Output.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BTN_Output.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(6, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 15)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Output folder:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOutputFolder
        '
        Me.txtOutputFolder.Location = New System.Drawing.Point(102, 45)
        Me.txtOutputFolder.Name = "txtOutputFolder"
        Me.txtOutputFolder.Size = New System.Drawing.Size(231, 20)
        Me.txtOutputFolder.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(11, 82)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 15)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "Report Title:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'panelWeekly
        '
        Me.panelWeekly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelWeekly.Controls.Add(Me.Label13)
        Me.panelWeekly.Controls.Add(Me.cmbWeekEnd)
        Me.panelWeekly.Controls.Add(Me.cbDayOfWeek)
        Me.panelWeekly.Controls.Add(Me.Label14)
        Me.panelWeekly.Controls.Add(Me.cmbWeekStart)
        Me.panelWeekly.Controls.Add(Me.Label12)
        Me.panelWeekly.Location = New System.Drawing.Point(8, 540)
        Me.panelWeekly.Name = "panelWeekly"
        Me.panelWeekly.Size = New System.Drawing.Size(470, 40)
        Me.panelWeekly.TabIndex = 39
        Me.panelWeekly.Visible = False
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(251, 12)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(118, 15)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "Day of generation:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbWeekEnd
        '
        Me.cmbWeekEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbWeekEnd.DropDownWidth = 180
        Me.cmbWeekEnd.FormattingEnabled = True
        Me.cmbWeekEnd.IntegralHeight = False
        Me.cmbWeekEnd.Items.AddRange(New Object() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})
        Me.cmbWeekEnd.Location = New System.Drawing.Point(144, 10)
        Me.cmbWeekEnd.Name = "cmbWeekEnd"
        Me.cmbWeekEnd.Size = New System.Drawing.Size(82, 21)
        Me.cmbWeekEnd.TabIndex = 14
        '
        'cbDayOfWeek
        '
        Me.cbDayOfWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDayOfWeek.DropDownWidth = 180
        Me.cbDayOfWeek.FormattingEnabled = True
        Me.cbDayOfWeek.IntegralHeight = False
        Me.cbDayOfWeek.Items.AddRange(New Object() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})
        Me.cbDayOfWeek.Location = New System.Drawing.Point(377, 10)
        Me.cbDayOfWeek.Name = "cbDayOfWeek"
        Me.cbDayOfWeek.Size = New System.Drawing.Size(82, 21)
        Me.cbDayOfWeek.TabIndex = 4
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(129, 12)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(16, 13)
        Me.Label14.TabIndex = 13
        Me.Label14.Text = "to"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbWeekStart
        '
        Me.cmbWeekStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbWeekStart.DropDownWidth = 180
        Me.cmbWeekStart.FormattingEnabled = True
        Me.cmbWeekStart.IntegralHeight = False
        Me.cmbWeekStart.Items.AddRange(New Object() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})
        Me.cmbWeekStart.Location = New System.Drawing.Point(44, 10)
        Me.cmbWeekStart.Name = "cmbWeekStart"
        Me.cmbWeekStart.Size = New System.Drawing.Size(82, 21)
        Me.cmbWeekStart.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(3, 12)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(39, 15)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "From"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpDowntime
        '
        Me.grpDowntime.Location = New System.Drawing.Point(559, 125)
        Me.grpDowntime.Name = "grpDowntime"
        Me.grpDowntime.Size = New System.Drawing.Size(475, 110)
        Me.grpDowntime.TabIndex = 30
        Me.grpDowntime.TabStop = False
        Me.grpDowntime.Text = "Downtime"
        Me.grpDowntime.Visible = False
        '
        'grpWeekly
        '
        Me.grpWeekly.Enabled = False
        Me.grpWeekly.Location = New System.Drawing.Point(6, 642)
        Me.grpWeekly.Name = "grpWeekly"
        Me.grpWeekly.Size = New System.Drawing.Size(475, 110)
        Me.grpWeekly.TabIndex = 35
        Me.grpWeekly.TabStop = False
        Me.grpWeekly.Text = "Weekly"
        Me.grpWeekly.Visible = False
        '
        'grpToday
        '
        Me.grpToday.Location = New System.Drawing.Point(559, 241)
        Me.grpToday.Name = "grpToday"
        Me.grpToday.Size = New System.Drawing.Size(475, 110)
        Me.grpToday.TabIndex = 26
        Me.grpToday.TabStop = False
        Me.grpToday.Text = "Daily - Today"
        '
        'chkMonday
        '
        Me.chkMonday.AutoSize = True
        Me.chkMonday.Checked = True
        Me.chkMonday.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMonday.Location = New System.Drawing.Point(219, 18)
        Me.chkMonday.Name = "chkMonday"
        Me.chkMonday.Size = New System.Drawing.Size(47, 17)
        Me.chkMonday.TabIndex = 34
        Me.chkMonday.Text = "Mon"
        Me.chkMonday.UseVisualStyleBackColor = True
        '
        'chkTuesday
        '
        Me.chkTuesday.AutoSize = True
        Me.chkTuesday.Checked = True
        Me.chkTuesday.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTuesday.Location = New System.Drawing.Point(266, 18)
        Me.chkTuesday.Name = "chkTuesday"
        Me.chkTuesday.Size = New System.Drawing.Size(45, 17)
        Me.chkTuesday.TabIndex = 35
        Me.chkTuesday.Text = "Tue"
        Me.chkTuesday.UseVisualStyleBackColor = True
        '
        'chkWednesday
        '
        Me.chkWednesday.AutoSize = True
        Me.chkWednesday.Checked = True
        Me.chkWednesday.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWednesday.Location = New System.Drawing.Point(313, 18)
        Me.chkWednesday.Name = "chkWednesday"
        Me.chkWednesday.Size = New System.Drawing.Size(49, 17)
        Me.chkWednesday.TabIndex = 36
        Me.chkWednesday.Text = "Wed"
        Me.chkWednesday.UseVisualStyleBackColor = True
        '
        'chkThursday
        '
        Me.chkThursday.AutoSize = True
        Me.chkThursday.Checked = True
        Me.chkThursday.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkThursday.Location = New System.Drawing.Point(362, 18)
        Me.chkThursday.Name = "chkThursday"
        Me.chkThursday.Size = New System.Drawing.Size(45, 17)
        Me.chkThursday.TabIndex = 37
        Me.chkThursday.Text = "Thu"
        Me.chkThursday.UseVisualStyleBackColor = True
        '
        'chkFriday
        '
        Me.chkFriday.AutoSize = True
        Me.chkFriday.Checked = True
        Me.chkFriday.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFriday.Location = New System.Drawing.Point(408, 18)
        Me.chkFriday.Name = "chkFriday"
        Me.chkFriday.Size = New System.Drawing.Size(37, 17)
        Me.chkFriday.TabIndex = 38
        Me.chkFriday.Text = "Fri"
        Me.chkFriday.UseVisualStyleBackColor = True
        '
        'chkSaturday
        '
        Me.chkSaturday.AutoSize = True
        Me.chkSaturday.Checked = True
        Me.chkSaturday.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSaturday.Location = New System.Drawing.Point(449, 18)
        Me.chkSaturday.Name = "chkSaturday"
        Me.chkSaturday.Size = New System.Drawing.Size(42, 17)
        Me.chkSaturday.TabIndex = 39
        Me.chkSaturday.Text = "Sat"
        Me.chkSaturday.UseVisualStyleBackColor = True
        '
        'AutoReporting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(959, 808)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(900, 600)
        Me.Name = "AutoReporting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Report Manager"
        CType(Me.dgvMachineList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.dgvReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.dgvEmail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GrBx_Parameter.ResumeLayout(False)
        Me.panelToday.ResumeLayout(False)
        Me.panelToday.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.panelDowntime.ResumeLayout(False)
        Me.panelDowntime.PerformLayout()
        Me.grpGeneration.ResumeLayout(False)
        Me.grpGeneration.PerformLayout()
        Me.panelWeekly.ResumeLayout(False)
        Me.panelWeekly.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label8 As Label
    Friend WithEvents BG_generate As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnModify As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents dgvMachineList As DataGridView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents dgvReports As DataGridView
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents dgvEmail As DataGridView
    Friend WithEvents DataGridViewCheckBoxColumn1 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnGenerate As Button
    Friend WithEvents btnAddNew As Button
    Friend WithEvents GrBx_Parameter As GroupBox
    Friend WithEvents btnCustomEmail As Button
    Friend WithEvents chkSunday As CheckBox
    Friend WithEvents BTN_Output As Button
    Friend WithEvents txtOutputFolder As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cbReportType As ComboBox
    Friend WithEvents dtpGenerationTime As DateTimePicker
    Friend WithEvents txtTaskName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnRemove As Button
    Friend WithEvents chkShortFileName As CheckBox
    Friend WithEvents grpToday As GroupBox
    Friend WithEvents cbPeriod As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents grpGeneration As GroupBox
    Friend WithEvents lblScale As Label
    Friend WithEvents cbScale As ComboBox
    Friend WithEvents grpDowntime As GroupBox
    Friend WithEvents chkSetup As CheckBox
    Friend WithEvents chkProduction As CheckBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents chkOnlySumary As CheckBox
    Friend WithEvents Label11 As Label
    Friend WithEvents cbChartSort As ComboBox
    Friend WithEvents chkShift3 As CheckBox
    Friend WithEvents chkShift2 As CheckBox
    Friend WithEvents chkShift1 As CheckBox
    Friend WithEvents txtReportTitle As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents chkAllShifts As CheckBox
    Friend WithEvents lblReportDisabled As Label
    Friend WithEvents chkDisableReport As CheckBox
    Friend WithEvents txtEventMinutes As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents chkHideEvents As CheckBox
    Friend WithEvents grpWeekly As GroupBox
    Friend WithEvents cmbWeekEnd As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents cmbWeekStart As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents cbDayOfWeek As ComboBox
    Friend WithEvents btnGenerateSelectedReports As Button
    Friend WithEvents cmbSelectReports As ComboBox
    Friend WithEvents panelToday As Panel
    Friend WithEvents panelDowntime As Panel
    Friend WithEvents panelWeekly As Panel
    Friend WithEvents ReportOrNot As DataGridViewCheckBoxColumn
    Friend WithEvents MachId As DataGridViewTextBoxColumn
    Friend WithEvents MachName As DataGridViewTextBoxColumn
    Friend WithEvents EnetMachName As DataGridViewTextBoxColumn
    Friend WithEvents Generate As DataGridViewCheckBoxColumn
    Friend WithEvents Task_name As DataGridViewTextBoxColumn
    Friend WithEvents Report_Type As DataGridViewTextBoxColumn
    Friend WithEvents Periodicity As DataGridViewTextBoxColumn
    Friend WithEvents Day As DataGridViewTextBoxColumn
    Friend WithEvents Time As DataGridViewTextBoxColumn
    Friend WithEvents Output_folder As DataGridViewTextBoxColumn
    Friend WithEvents MachineName As DataGridViewTextBoxColumn
    Friend WithEvents MailTo As DataGridViewTextBoxColumn
    Friend WithEvents Enabled As DataGridViewTextBoxColumn
    Friend WithEvents ReportId As DataGridViewTextBoxColumn
    Friend WithEvents chkShowSetupCycleOnTime As CheckBox
    Friend WithEvents chkShowOnTimeline As CheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents chkSaturday As CheckBox
    Friend WithEvents chkFriday As CheckBox
    Friend WithEvents chkThursday As CheckBox
    Friend WithEvents chkWednesday As CheckBox
    Friend WithEvents chkTuesday As CheckBox
    Friend WithEvents chkMonday As CheckBox
End Class

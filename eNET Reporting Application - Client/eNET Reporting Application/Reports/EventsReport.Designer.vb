<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EventsReport
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
        Dim DataGridViewCellStyle34 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle44 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EventsReport))
        Dim DataGridViewCellStyle35 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle36 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle37 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle38 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle39 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle40 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle41 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle42 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle43 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle33 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnReloadMachines = New System.Windows.Forms.Button()
        Me.dtpDayFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpDayTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rBtnDay = New System.Windows.Forms.RadioButton()
        Me.chkShift3 = New System.Windows.Forms.CheckBox()
        Me.chkShift2 = New System.Windows.Forms.CheckBox()
        Me.chkShift1 = New System.Windows.Forms.CheckBox()
        Me.rBtnCustom = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkAllPartNumbers = New System.Windows.Forms.CheckBox()
        Me.chkAllMachines = New System.Windows.Forms.CheckBox()
        Me.lBoxMachines = New System.Windows.Forms.CheckedListBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblPartNumbers = New System.Windows.Forms.Label()
        Me.lboxPartNumber = New System.Windows.Forms.CheckedListBox()
        Me.lblMachines = New System.Windows.Forms.Label()
        Me.tViewMachines = New System.Windows.Forms.TreeView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rBtnSearchForPartNumbers = New System.Windows.Forms.RadioButton()
        Me.rBtnSearchForMachines = New System.Windows.Forms.RadioButton()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgvSummary = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgvDetails = New System.Windows.Forms.DataGridView()
        Me.btnExportCsv = New System.Windows.Forms.Button()
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.colMachineId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDateTimeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colShift = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMachineName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colState = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPartNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOperation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOperator = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFeedrateOvr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRapidOvr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSpindleOvr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2MachineId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2EventDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2DateTimeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2Shift = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2MachineName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2State = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2PartNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2Operation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2Operator = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CycleTimeSec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CycleTimeHrs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2CycleAvg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2Graphic = New System.Windows.Forms.DataGridViewImageColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.SplitContainer1)
        Me.GroupBox1.Controls.Add(Me.btnExportCsv)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 15)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1381, 957)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(8, 16)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControl)
        Me.SplitContainer1.Size = New System.Drawing.Size(1365, 891)
        Me.SplitContainer1.SplitterDistance = 210
        Me.SplitContainer1.TabIndex = 25
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.btnReloadMachines)
        Me.GroupBox2.Controls.Add(Me.dtpDayFrom)
        Me.GroupBox2.Controls.Add(Me.dtpDayTo)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.rBtnDay)
        Me.GroupBox2.Controls.Add(Me.chkShift3)
        Me.GroupBox2.Controls.Add(Me.chkShift2)
        Me.GroupBox2.Controls.Add(Me.chkShift1)
        Me.GroupBox2.Controls.Add(Me.rBtnCustom)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(451, 204)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        '
        'btnReloadMachines
        '
        Me.btnReloadMachines.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReloadMachines.BackgroundImage = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.baseline_sync_black_24dp
        Me.btnReloadMachines.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReloadMachines.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnReloadMachines.Location = New System.Drawing.Point(420, 173)
        Me.btnReloadMachines.Name = "btnReloadMachines"
        Me.btnReloadMachines.Size = New System.Drawing.Size(25, 25)
        Me.btnReloadMachines.TabIndex = 22
        Me.btnReloadMachines.UseVisualStyleBackColor = True
        '
        'dtpDayFrom
        '
        Me.dtpDayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDayFrom.Location = New System.Drawing.Point(95, 69)
        Me.dtpDayFrom.Name = "dtpDayFrom"
        Me.dtpDayFrom.Size = New System.Drawing.Size(140, 23)
        Me.dtpDayFrom.TabIndex = 15
        '
        'dtpDayTo
        '
        Me.dtpDayTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDayTo.Location = New System.Drawing.Point(286, 69)
        Me.dtpDayTo.Name = "dtpDayTo"
        Me.dtpDayTo.Size = New System.Drawing.Size(140, 23)
        Me.dtpDayTo.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 22)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Period :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(241, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 20)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "To"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rBtnDay
        '
        Me.rBtnDay.AutoSize = True
        Me.rBtnDay.Checked = True
        Me.rBtnDay.Location = New System.Drawing.Point(95, 25)
        Me.rBtnDay.Name = "rBtnDay"
        Me.rBtnDay.Size = New System.Drawing.Size(51, 21)
        Me.rBtnDay.TabIndex = 10
        Me.rBtnDay.TabStop = True
        Me.rBtnDay.Text = "Day"
        Me.rBtnDay.UseVisualStyleBackColor = True
        '
        'chkShift3
        '
        Me.chkShift3.AutoSize = True
        Me.chkShift3.Checked = True
        Me.chkShift3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShift3.Location = New System.Drawing.Point(241, 105)
        Me.chkShift3.Name = "chkShift3"
        Me.chkShift3.Size = New System.Drawing.Size(67, 21)
        Me.chkShift3.TabIndex = 19
        Me.chkShift3.Text = "Shift 3"
        Me.chkShift3.UseVisualStyleBackColor = True
        '
        'chkShift2
        '
        Me.chkShift2.AutoSize = True
        Me.chkShift2.Checked = True
        Me.chkShift2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShift2.Location = New System.Drawing.Point(168, 105)
        Me.chkShift2.Name = "chkShift2"
        Me.chkShift2.Size = New System.Drawing.Size(67, 21)
        Me.chkShift2.TabIndex = 18
        Me.chkShift2.Text = "Shift 2"
        Me.chkShift2.UseVisualStyleBackColor = True
        '
        'chkShift1
        '
        Me.chkShift1.AutoSize = True
        Me.chkShift1.Checked = True
        Me.chkShift1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShift1.Location = New System.Drawing.Point(95, 105)
        Me.chkShift1.Name = "chkShift1"
        Me.chkShift1.Size = New System.Drawing.Size(67, 21)
        Me.chkShift1.TabIndex = 17
        Me.chkShift1.Text = "Shift 1"
        Me.chkShift1.UseVisualStyleBackColor = True
        '
        'rBtnCustom
        '
        Me.rBtnCustom.AutoSize = True
        Me.rBtnCustom.Location = New System.Drawing.Point(152, 25)
        Me.rBtnCustom.Name = "rBtnCustom"
        Me.rBtnCustom.Size = New System.Drawing.Size(68, 21)
        Me.rBtnCustom.TabIndex = 13
        Me.rBtnCustom.Text = "Range"
        Me.rBtnCustom.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(9, 103)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 20)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Shifts :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(9, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 20)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.chkAllPartNumbers)
        Me.GroupBox3.Controls.Add(Me.chkAllMachines)
        Me.GroupBox3.Controls.Add(Me.lBoxMachines)
        Me.GroupBox3.Controls.Add(Me.btnRefresh)
        Me.GroupBox3.Controls.Add(Me.lblPartNumbers)
        Me.GroupBox3.Controls.Add(Me.lboxPartNumber)
        Me.GroupBox3.Controls.Add(Me.lblMachines)
        Me.GroupBox3.Controls.Add(Me.tViewMachines)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.rBtnSearchForPartNumbers)
        Me.GroupBox3.Controls.Add(Me.rBtnSearchForMachines)
        Me.GroupBox3.Location = New System.Drawing.Point(460, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(684, 204)
        Me.GroupBox3.TabIndex = 23
        Me.GroupBox3.TabStop = False
        '
        'chkAllPartNumbers
        '
        Me.chkAllPartNumbers.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkAllPartNumbers.AutoSize = True
        Me.chkAllPartNumbers.Checked = True
        Me.chkAllPartNumbers.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllPartNumbers.Location = New System.Drawing.Point(406, 176)
        Me.chkAllPartNumbers.Name = "chkAllPartNumbers"
        Me.chkAllPartNumbers.Size = New System.Drawing.Size(130, 21)
        Me.chkAllPartNumbers.TabIndex = 20
        Me.chkAllPartNumbers.Text = "All part numbers"
        Me.chkAllPartNumbers.UseVisualStyleBackColor = True
        '
        'chkAllMachines
        '
        Me.chkAllMachines.AutoSize = True
        Me.chkAllMachines.Checked = True
        Me.chkAllMachines.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllMachines.Location = New System.Drawing.Point(406, 176)
        Me.chkAllMachines.Name = "chkAllMachines"
        Me.chkAllMachines.Size = New System.Drawing.Size(106, 21)
        Me.chkAllMachines.TabIndex = 19
        Me.chkAllMachines.Text = "All machines"
        Me.chkAllMachines.UseVisualStyleBackColor = True
        Me.chkAllMachines.Visible = False
        '
        'lBoxMachines
        '
        Me.lBoxMachines.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lBoxMachines.FormattingEnabled = True
        Me.lBoxMachines.Location = New System.Drawing.Point(406, 42)
        Me.lBoxMachines.Name = "lBoxMachines"
        Me.lBoxMachines.Size = New System.Drawing.Size(230, 130)
        Me.lBoxMachines.TabIndex = 18
        Me.lBoxMachines.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackgroundImage = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.baseline_sync_black_24dp
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnRefresh.Location = New System.Drawing.Point(653, 173)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(25, 25)
        Me.btnRefresh.TabIndex = 17
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblPartNumbers
        '
        Me.lblPartNumbers.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPartNumbers.Location = New System.Drawing.Point(404, 14)
        Me.lblPartNumbers.Name = "lblPartNumbers"
        Me.lblPartNumbers.Size = New System.Drawing.Size(231, 25)
        Me.lblPartNumbers.TabIndex = 16
        Me.lblPartNumbers.Text = "Part Numbers"
        Me.lblPartNumbers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lboxPartNumber
        '
        Me.lboxPartNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lboxPartNumber.FormattingEnabled = True
        Me.lboxPartNumber.Location = New System.Drawing.Point(406, 42)
        Me.lboxPartNumber.Name = "lboxPartNumber"
        Me.lboxPartNumber.Size = New System.Drawing.Size(230, 130)
        Me.lboxPartNumber.TabIndex = 15
        '
        'lblMachines
        '
        Me.lblMachines.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachines.Location = New System.Drawing.Point(164, 14)
        Me.lblMachines.Name = "lblMachines"
        Me.lblMachines.Size = New System.Drawing.Size(234, 25)
        Me.lblMachines.TabIndex = 11
        Me.lblMachines.Text = "Machines"
        Me.lblMachines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tViewMachines
        '
        Me.tViewMachines.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tViewMachines.CheckBoxes = True
        Me.tViewMachines.Location = New System.Drawing.Point(164, 42)
        Me.tViewMachines.Name = "tViewMachines"
        Me.tViewMachines.Size = New System.Drawing.Size(230, 150)
        Me.tViewMachines.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 31)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Search by"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rBtnSearchForPartNumbers
        '
        Me.rBtnSearchForPartNumbers.AutoSize = True
        Me.rBtnSearchForPartNumbers.Location = New System.Drawing.Point(10, 80)
        Me.rBtnSearchForPartNumbers.Name = "rBtnSearchForPartNumbers"
        Me.rBtnSearchForPartNumbers.Size = New System.Drawing.Size(113, 21)
        Me.rBtnSearchForPartNumbers.TabIndex = 9
        Me.rBtnSearchForPartNumbers.TabStop = True
        Me.rBtnSearchForPartNumbers.Text = "Part Numbers"
        Me.rBtnSearchForPartNumbers.UseVisualStyleBackColor = True
        '
        'rBtnSearchForMachines
        '
        Me.rBtnSearchForMachines.AutoSize = True
        Me.rBtnSearchForMachines.Location = New System.Drawing.Point(10, 53)
        Me.rBtnSearchForMachines.Name = "rBtnSearchForMachines"
        Me.rBtnSearchForMachines.Size = New System.Drawing.Size(86, 21)
        Me.rBtnSearchForMachines.TabIndex = 8
        Me.rBtnSearchForMachines.TabStop = True
        Me.rBtnSearchForMachines.Text = "Machines"
        Me.rBtnSearchForMachines.UseVisualStyleBackColor = True
        '
        'TabControl
        '
        Me.TabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl.Controls.Add(Me.TabPage1)
        Me.TabControl.Controls.Add(Me.TabPage2)
        Me.TabControl.ItemSize = New System.Drawing.Size(150, 30)
        Me.TabControl.Location = New System.Drawing.Point(4, 3)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(1358, 671)
        Me.TabControl.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvSummary)
        Me.TabPage1.Location = New System.Drawing.Point(4, 34)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1350, 633)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Status"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgvSummary
        '
        Me.dgvSummary.AllowUserToAddRows = False
        Me.dgvSummary.AllowUserToDeleteRows = False
        Me.dgvSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSummary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col2Id, Me.col2MachineId, Me.col2EventDateTime, Me.col2DateTimeId, Me.col2Shift, Me.col2MachineName, Me.col2State, Me.col2PartNumber, Me.col2Operation, Me.col2Operator, Me.col2CycleTimeSec, Me.col2CycleTimeHrs, Me.col2CycleAvg, Me.col2Graphic})
        DataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle34.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle34.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle34.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle34.NullValue = """"""
        DataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSummary.DefaultCellStyle = DataGridViewCellStyle34
        Me.dgvSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSummary.Location = New System.Drawing.Point(3, 3)
        Me.dgvSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvSummary.MultiSelect = False
        Me.dgvSummary.Name = "dgvSummary"
        Me.dgvSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSummary.Size = New System.Drawing.Size(1344, 627)
        Me.dgvSummary.TabIndex = 3
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgvDetails)
        Me.TabPage2.Location = New System.Drawing.Point(4, 34)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1350, 633)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "OVR's"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgvDetails
        '
        Me.dgvDetails.AllowUserToAddRows = False
        Me.dgvDetails.AllowUserToDeleteRows = False
        Me.dgvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colMachineId, Me.colDateTime, Me.colDateTimeId, Me.colShift, Me.colMachineName, Me.colState, Me.colPartNumber, Me.colOperation, Me.colOperator, Me.colFeedrateOvr, Me.colRapidOvr, Me.colSpindleOvr})
        DataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle44.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle44.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle44.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle44.NullValue = """"""
        DataGridViewCellStyle44.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle44.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle44.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetails.DefaultCellStyle = DataGridViewCellStyle44
        Me.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetails.Location = New System.Drawing.Point(3, 3)
        Me.dgvDetails.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvDetails.MultiSelect = False
        Me.dgvDetails.Name = "dgvDetails"
        Me.dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetails.Size = New System.Drawing.Size(1344, 627)
        Me.dgvDetails.TabIndex = 2
        '
        'btnExportCsv
        '
        Me.btnExportCsv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExportCsv.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportCsv.Location = New System.Drawing.Point(1199, 913)
        Me.btnExportCsv.Name = "btnExportCsv"
        Me.btnExportCsv.Size = New System.Drawing.Size(174, 37)
        Me.btnExportCsv.TabIndex = 24
        Me.btnExportCsv.Text = "Export CSV"
        Me.btnExportCsv.UseVisualStyleBackColor = True
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "BlueBar")
        Me.ImageList.Images.SetKeyName(1, "GreenBar")
        Me.ImageList.Images.SetKeyName(2, "RedBar")
        Me.ImageList.Images.SetKeyName(3, "YellowBar")
        Me.ImageList.Images.SetKeyName(4, "WhiteBar")
        '
        'colMachineId
        '
        Me.colMachineId.DataPropertyName = "MachineId"
        Me.colMachineId.HeaderText = "MachineId"
        Me.colMachineId.Name = "colMachineId"
        Me.colMachineId.Visible = False
        '
        'colDateTime
        '
        Me.colDateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colDateTime.DataPropertyName = "DateTime"
        DataGridViewCellStyle35.NullValue = """"""
        Me.colDateTime.DefaultCellStyle = DataGridViewCellStyle35
        Me.colDateTime.HeaderText = "Date Time"
        Me.colDateTime.Name = "colDateTime"
        Me.colDateTime.ReadOnly = True
        Me.colDateTime.Width = 98
        '
        'colDateTimeId
        '
        Me.colDateTimeId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colDateTimeId.DataPropertyName = "DateTimeId"
        Me.colDateTimeId.HeaderText = ""
        Me.colDateTimeId.MinimumWidth = 50
        Me.colDateTimeId.Name = "colDateTimeId"
        Me.colDateTimeId.ReadOnly = True
        Me.colDateTimeId.Visible = False
        Me.colDateTimeId.Width = 50
        '
        'colShift
        '
        Me.colShift.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colShift.DataPropertyName = "Shift"
        DataGridViewCellStyle36.NullValue = """"""
        Me.colShift.DefaultCellStyle = DataGridViewCellStyle36
        Me.colShift.HeaderText = "Shift"
        Me.colShift.Name = "colShift"
        Me.colShift.ReadOnly = True
        Me.colShift.Width = 61
        '
        'colMachineName
        '
        Me.colMachineName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colMachineName.DataPropertyName = "MachineName"
        DataGridViewCellStyle37.NullValue = """"""
        Me.colMachineName.DefaultCellStyle = DataGridViewCellStyle37
        Me.colMachineName.HeaderText = "Machine Name"
        Me.colMachineName.MinimumWidth = 100
        Me.colMachineName.Name = "colMachineName"
        Me.colMachineName.ReadOnly = True
        Me.colMachineName.Width = 116
        '
        'colState
        '
        Me.colState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colState.DataPropertyName = "State"
        DataGridViewCellStyle38.NullValue = """"""
        Me.colState.DefaultCellStyle = DataGridViewCellStyle38
        Me.colState.HeaderText = "State"
        Me.colState.MinimumWidth = 100
        Me.colState.Name = "colState"
        Me.colState.ReadOnly = True
        '
        'colPartNumber
        '
        Me.colPartNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colPartNumber.DataPropertyName = "PartNumber"
        DataGridViewCellStyle39.NullValue = """"""
        Me.colPartNumber.DefaultCellStyle = DataGridViewCellStyle39
        Me.colPartNumber.HeaderText = "Part Number"
        Me.colPartNumber.MinimumWidth = 100
        Me.colPartNumber.Name = "colPartNumber"
        Me.colPartNumber.ReadOnly = True
        Me.colPartNumber.Width = 104
        '
        'colOperation
        '
        Me.colOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colOperation.DataPropertyName = "Operation"
        DataGridViewCellStyle40.NullValue = """"""
        Me.colOperation.DefaultCellStyle = DataGridViewCellStyle40
        Me.colOperation.HeaderText = "Operation"
        Me.colOperation.MinimumWidth = 100
        Me.colOperation.Name = "colOperation"
        Me.colOperation.ReadOnly = True
        '
        'colOperator
        '
        Me.colOperator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colOperator.DataPropertyName = "Operator"
        Me.colOperator.HeaderText = "Operator"
        Me.colOperator.MinimumWidth = 100
        Me.colOperator.Name = "colOperator"
        Me.colOperator.ReadOnly = True
        '
        'colFeedrateOvr
        '
        Me.colFeedrateOvr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colFeedrateOvr.DataPropertyName = "FeedrateOvr"
        DataGridViewCellStyle41.NullValue = """"""
        Me.colFeedrateOvr.DefaultCellStyle = DataGridViewCellStyle41
        Me.colFeedrateOvr.HeaderText = "Feedrate Ovr"
        Me.colFeedrateOvr.MinimumWidth = 100
        Me.colFeedrateOvr.Name = "colFeedrateOvr"
        Me.colFeedrateOvr.ReadOnly = True
        Me.colFeedrateOvr.Width = 107
        '
        'colRapidOvr
        '
        Me.colRapidOvr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colRapidOvr.DataPropertyName = "RapidOvr"
        DataGridViewCellStyle42.NullValue = """"""
        Me.colRapidOvr.DefaultCellStyle = DataGridViewCellStyle42
        Me.colRapidOvr.HeaderText = "Rapid Ovr"
        Me.colRapidOvr.MinimumWidth = 100
        Me.colRapidOvr.Name = "colRapidOvr"
        Me.colRapidOvr.ReadOnly = True
        '
        'colSpindleOvr
        '
        Me.colSpindleOvr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colSpindleOvr.DataPropertyName = "SpindleOvr"
        DataGridViewCellStyle43.NullValue = """"""
        Me.colSpindleOvr.DefaultCellStyle = DataGridViewCellStyle43
        Me.colSpindleOvr.HeaderText = "Spindle Ovr"
        Me.colSpindleOvr.MinimumWidth = 100
        Me.colSpindleOvr.Name = "colSpindleOvr"
        Me.colSpindleOvr.ReadOnly = True
        '
        'col2Id
        '
        Me.col2Id.DataPropertyName = "Id"
        DataGridViewCellStyle23.NullValue = """"""
        Me.col2Id.DefaultCellStyle = DataGridViewCellStyle23
        Me.col2Id.HeaderText = "ID"
        Me.col2Id.Name = "col2Id"
        Me.col2Id.ReadOnly = True
        Me.col2Id.Visible = False
        '
        'col2MachineId
        '
        Me.col2MachineId.DataPropertyName = "MachineId"
        DataGridViewCellStyle24.NullValue = """"""
        Me.col2MachineId.DefaultCellStyle = DataGridViewCellStyle24
        Me.col2MachineId.HeaderText = "MachineId"
        Me.col2MachineId.Name = "col2MachineId"
        Me.col2MachineId.ReadOnly = True
        Me.col2MachineId.Visible = False
        '
        'col2EventDateTime
        '
        Me.col2EventDateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.col2EventDateTime.DataPropertyName = "DateTime"
        DataGridViewCellStyle25.NullValue = """"""
        Me.col2EventDateTime.DefaultCellStyle = DataGridViewCellStyle25
        Me.col2EventDateTime.HeaderText = "Date Time"
        Me.col2EventDateTime.Name = "col2EventDateTime"
        Me.col2EventDateTime.ReadOnly = True
        Me.col2EventDateTime.Width = 98
        '
        'col2DateTimeId
        '
        Me.col2DateTimeId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.col2DateTimeId.DataPropertyName = "DateTimeId"
        Me.col2DateTimeId.HeaderText = ""
        Me.col2DateTimeId.MinimumWidth = 50
        Me.col2DateTimeId.Name = "col2DateTimeId"
        Me.col2DateTimeId.ReadOnly = True
        Me.col2DateTimeId.Visible = False
        Me.col2DateTimeId.Width = 50
        '
        'col2Shift
        '
        Me.col2Shift.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.col2Shift.DataPropertyName = "Shift"
        DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle26.NullValue = """"""
        Me.col2Shift.DefaultCellStyle = DataGridViewCellStyle26
        Me.col2Shift.HeaderText = "Shift"
        Me.col2Shift.Name = "col2Shift"
        Me.col2Shift.ReadOnly = True
        Me.col2Shift.Width = 61
        '
        'col2MachineName
        '
        Me.col2MachineName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.col2MachineName.DataPropertyName = "MachineName"
        DataGridViewCellStyle27.NullValue = """"""
        Me.col2MachineName.DefaultCellStyle = DataGridViewCellStyle27
        Me.col2MachineName.HeaderText = "Machine Name"
        Me.col2MachineName.MinimumWidth = 100
        Me.col2MachineName.Name = "col2MachineName"
        Me.col2MachineName.ReadOnly = True
        Me.col2MachineName.Width = 116
        '
        'col2State
        '
        Me.col2State.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.col2State.DataPropertyName = "State"
        DataGridViewCellStyle28.NullValue = """"""
        Me.col2State.DefaultCellStyle = DataGridViewCellStyle28
        Me.col2State.HeaderText = "State"
        Me.col2State.MinimumWidth = 100
        Me.col2State.Name = "col2State"
        Me.col2State.ReadOnly = True
        '
        'col2PartNumber
        '
        Me.col2PartNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.col2PartNumber.DataPropertyName = "PartNumber"
        DataGridViewCellStyle29.NullValue = """"""
        Me.col2PartNumber.DefaultCellStyle = DataGridViewCellStyle29
        Me.col2PartNumber.HeaderText = "Part Number"
        Me.col2PartNumber.MinimumWidth = 100
        Me.col2PartNumber.Name = "col2PartNumber"
        Me.col2PartNumber.ReadOnly = True
        Me.col2PartNumber.Width = 104
        '
        'col2Operation
        '
        Me.col2Operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.col2Operation.DataPropertyName = "Operation"
        DataGridViewCellStyle30.NullValue = """"""
        Me.col2Operation.DefaultCellStyle = DataGridViewCellStyle30
        Me.col2Operation.HeaderText = "Operation"
        Me.col2Operation.MinimumWidth = 100
        Me.col2Operation.Name = "col2Operation"
        Me.col2Operation.ReadOnly = True
        '
        'col2Operator
        '
        Me.col2Operator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.col2Operator.DataPropertyName = "Operator"
        Me.col2Operator.HeaderText = "Operator"
        Me.col2Operator.MinimumWidth = 100
        Me.col2Operator.Name = "col2Operator"
        Me.col2Operator.ReadOnly = True
        '
        'col2CycleTimeSec
        '
        Me.col2CycleTimeSec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.col2CycleTimeSec.DataPropertyName = "CycleTimeSec"
        DataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle31.NullValue = """"""
        Me.col2CycleTimeSec.DefaultCellStyle = DataGridViewCellStyle31
        Me.col2CycleTimeSec.HeaderText = "Cycle Time (sec)"
        Me.col2CycleTimeSec.MinimumWidth = 100
        Me.col2CycleTimeSec.Name = "col2CycleTimeSec"
        Me.col2CycleTimeSec.ReadOnly = True
        Me.col2CycleTimeSec.Width = 126
        '
        'col2CycleTimeHrs
        '
        Me.col2CycleTimeHrs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.col2CycleTimeHrs.DataPropertyName = "CycleTimeHrs"
        DataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle32.NullValue = """"""
        Me.col2CycleTimeHrs.DefaultCellStyle = DataGridViewCellStyle32
        Me.col2CycleTimeHrs.HeaderText = "Cycle Time (hrs)"
        Me.col2CycleTimeHrs.MinimumWidth = 100
        Me.col2CycleTimeHrs.Name = "col2CycleTimeHrs"
        Me.col2CycleTimeHrs.ReadOnly = True
        Me.col2CycleTimeHrs.Width = 124
        '
        'col2CycleAvg
        '
        Me.col2CycleAvg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.col2CycleAvg.DataPropertyName = "AverageCycle"
        DataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.col2CycleAvg.DefaultCellStyle = DataGridViewCellStyle33
        Me.col2CycleAvg.HeaderText = "Average Cycle"
        Me.col2CycleAvg.MinimumWidth = 100
        Me.col2CycleAvg.Name = "col2CycleAvg"
        Me.col2CycleAvg.Visible = False
        Me.col2CycleAvg.Width = 114
        '
        'col2Graphic
        '
        Me.col2Graphic.HeaderText = ""
        Me.col2Graphic.MinimumWidth = 150
        Me.col2Graphic.Name = "col2Graphic"
        Me.col2Graphic.Visible = False
        '
        'EventsReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1413, 986)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EventsReport"
        Me.Text = "Event Status Export "
        Me.GroupBox1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dgvSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dgvDetails As DataGridView
    Friend WithEvents rBtnCustom As RadioButton
    Friend WithEvents rBtnDay As RadioButton
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpDayTo As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents chkShift3 As CheckBox
    Friend WithEvents chkShift2 As CheckBox
    Friend WithEvents chkShift1 As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpDayFrom As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents rBtnSearchForPartNumbers As RadioButton
    Friend WithEvents rBtnSearchForMachines As RadioButton
    Friend WithEvents lblMachines As Label
    Friend WithEvents tViewMachines As TreeView
    Friend WithEvents btnRefresh As Button
    Friend WithEvents lblPartNumbers As Label
    Friend WithEvents lboxPartNumber As CheckedListBox
    Friend WithEvents btnReloadMachines As Button
    Friend WithEvents btnExportCsv As Button
    Friend WithEvents lBoxMachines As CheckedListBox
    Friend WithEvents chkAllPartNumbers As CheckBox
    Friend WithEvents chkAllMachines As CheckBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents dgvSummary As DataGridView
    Friend WithEvents TabControl As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents ImageList As ImageList
    Friend WithEvents colMachineId As DataGridViewTextBoxColumn
    Friend WithEvents colDateTime As DataGridViewTextBoxColumn
    Friend WithEvents colDateTimeId As DataGridViewTextBoxColumn
    Friend WithEvents colShift As DataGridViewTextBoxColumn
    Friend WithEvents colMachineName As DataGridViewTextBoxColumn
    Friend WithEvents colState As DataGridViewTextBoxColumn
    Friend WithEvents colPartNumber As DataGridViewTextBoxColumn
    Friend WithEvents colOperation As DataGridViewTextBoxColumn
    Friend WithEvents colOperator As DataGridViewTextBoxColumn
    Friend WithEvents colFeedrateOvr As DataGridViewTextBoxColumn
    Friend WithEvents colRapidOvr As DataGridViewTextBoxColumn
    Friend WithEvents colSpindleOvr As DataGridViewTextBoxColumn
    Friend WithEvents col2Id As DataGridViewTextBoxColumn
    Friend WithEvents col2MachineId As DataGridViewTextBoxColumn
    Friend WithEvents col2EventDateTime As DataGridViewTextBoxColumn
    Friend WithEvents col2DateTimeId As DataGridViewTextBoxColumn
    Friend WithEvents col2Shift As DataGridViewTextBoxColumn
    Friend WithEvents col2MachineName As DataGridViewTextBoxColumn
    Friend WithEvents col2State As DataGridViewTextBoxColumn
    Friend WithEvents col2PartNumber As DataGridViewTextBoxColumn
    Friend WithEvents col2Operation As DataGridViewTextBoxColumn
    Friend WithEvents col2Operator As DataGridViewTextBoxColumn
    Friend WithEvents col2CycleTimeSec As DataGridViewTextBoxColumn
    Friend WithEvents col2CycleTimeHrs As DataGridViewTextBoxColumn
    Friend WithEvents col2CycleAvg As DataGridViewTextBoxColumn
    Friend WithEvents col2Graphic As DataGridViewImageColumn
End Class

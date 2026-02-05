<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_raw_values
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_raw_values))
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvTimeline = New System.Windows.Forms.DataGridView()
        Me.TimeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Selected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Shift = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeEnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cycletime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Image = New System.Windows.Forms.DataGridViewImageColumn()
        Me.PartNr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Oper = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Comments = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.gboxDetails = New System.Windows.Forms.GroupBox()
        Me.grpEditForm = New System.Windows.Forms.GroupBox()
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.lblLimitStartMax = New System.Windows.Forms.Label()
        Me.lblLimitEndMin = New System.Windows.Forms.Label()
        Me.timePickerEnd = New System.Windows.Forms.DateTimePicker()
        Me.lblLimitEndMax = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.timePickerStart = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblLimitStartMin = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.rdbOperator = New System.Windows.Forms.RadioButton()
        Me.rdbPartNumber = New System.Windows.Forms.RadioButton()
        Me.rdbStatus = New System.Windows.Forms.RadioButton()
        Me.pnlOperator = New System.Windows.Forms.Panel()
        Me.rdbOPCurrentLine = New System.Windows.Forms.RadioButton()
        Me.rdbOPSelectedLines = New System.Windows.Forms.RadioButton()
        Me.txtOperator = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.pnlPartNumber = New System.Windows.Forms.Panel()
        Me.rbtPNCurrentLine = New System.Windows.Forms.RadioButton()
        Me.rbtPNSelectedLines = New System.Windows.Forms.RadioButton()
        Me.txtPartNumber = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnDeleteLines = New System.Windows.Forms.Button()
        Me.btnCancelChange = New System.Windows.Forms.Button()
        Me.btnSaveChange = New System.Windows.Forms.Button()
        Me.btnStartChange = New System.Windows.Forms.Button()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnMarkSelected = New System.Windows.Forms.Button()
        Me.rdbEdits = New System.Windows.Forms.RadioButton()
        Me.rdbTimeline = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.dgvEdits = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Action = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OriginalStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OriginalTimeStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewTimeStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OriginalCycletime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewCycletime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EditImage = New System.Windows.Forms.DataGridViewImageColumn()
        Me.OriginalPartNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewPartNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OriginalOperator = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewOperator = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OriginalComments = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewComments = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EditedBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EditedWhen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EditDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnExportRawData = New System.Windows.Forms.Button()
        Me.btnClearSelection = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnOVRExportCsv = New System.Windows.Forms.Button()
        Me.chkSpindle = New System.Windows.Forms.CheckBox()
        Me.chkRapid = New System.Windows.Forms.CheckBox()
        Me.chkFeedrate = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chartOverride = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.dgvOverride = New System.Windows.Forms.DataGridView()
        Me.ovrEventDateTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ovrStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ovrPartNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ovrOperation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ovrFeedrate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ovrRapid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ovrSpindle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OvrCycleTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.dgvTimeline, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboxDetails.SuspendLayout()
        Me.grpEditForm.SuspendLayout()
        Me.pnlStatus.SuspendLayout()
        Me.pnlOperator.SuspendLayout()
        Me.pnlPartNumber.SuspendLayout()
        CType(Me.dgvEdits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.chartOverride, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOverride, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvTimeline
        '
        Me.dgvTimeline.AllowUserToAddRows = False
        Me.dgvTimeline.AllowUserToDeleteRows = False
        Me.dgvTimeline.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTimeline.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvTimeline.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTimeline.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTimeline.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TimeId, Me.Selected, Me.Status, Me.Shift, Me.TimeStart, Me.TimeEnd, Me.Cycletime, Me.Image, Me.PartNr, Me.Oper, Me.Comments})
        Me.dgvTimeline.EnableHeadersVisualStyles = False
        Me.dgvTimeline.Location = New System.Drawing.Point(3, 52)
        Me.dgvTimeline.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgvTimeline.Name = "dgvTimeline"
        Me.dgvTimeline.RowTemplate.Height = 24
        Me.dgvTimeline.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTimeline.Size = New System.Drawing.Size(1094, 200)
        Me.dgvTimeline.TabIndex = 0
        '
        'TimeId
        '
        Me.TimeId.DataPropertyName = "TimeId"
        Me.TimeId.HeaderText = "TimeId"
        Me.TimeId.Name = "TimeId"
        Me.TimeId.ReadOnly = True
        Me.TimeId.Visible = False
        '
        'Selected
        '
        Me.Selected.DataPropertyName = "Selected"
        Me.Selected.HeaderText = ""
        Me.Selected.Name = "Selected"
        Me.Selected.Width = 50
        '
        'Status
        '
        Me.Status.DataPropertyName = "Status"
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Width = 120
        '
        'Shift
        '
        Me.Shift.DataPropertyName = "Shift"
        Me.Shift.HeaderText = "Shift"
        Me.Shift.Name = "Shift"
        Me.Shift.ReadOnly = True
        Me.Shift.Width = 45
        '
        'TimeStart
        '
        Me.TimeStart.DataPropertyName = "TimeStart"
        DataGridViewCellStyle9.Format = "MM-dd-yyyy HH:mm:ss"
        Me.TimeStart.DefaultCellStyle = DataGridViewCellStyle9
        Me.TimeStart.HeaderText = "Time Start"
        Me.TimeStart.Name = "TimeStart"
        Me.TimeStart.ReadOnly = True
        Me.TimeStart.Width = 150
        '
        'TimeEnd
        '
        Me.TimeEnd.DataPropertyName = "TimeEnd"
        DataGridViewCellStyle10.Format = "MM-dd-yyyy HH:mm:ss"
        Me.TimeEnd.DefaultCellStyle = DataGridViewCellStyle10
        Me.TimeEnd.HeaderText = "Time End"
        Me.TimeEnd.Name = "TimeEnd"
        Me.TimeEnd.ReadOnly = True
        Me.TimeEnd.Width = 150
        '
        'Cycletime
        '
        Me.Cycletime.DataPropertyName = "Cycletime"
        Me.Cycletime.HeaderText = "Cycletime"
        Me.Cycletime.Name = "Cycletime"
        Me.Cycletime.ReadOnly = True
        Me.Cycletime.Width = 65
        '
        'Image
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.NullValue = CType(resources.GetObject("DataGridViewCellStyle11.NullValue"), Object)
        Me.Image.DefaultCellStyle = DataGridViewCellStyle11
        Me.Image.HeaderText = ""
        Me.Image.Name = "Image"
        Me.Image.ReadOnly = True
        Me.Image.Width = 150
        '
        'PartNr
        '
        Me.PartNr.DataPropertyName = "PartNumber"
        Me.PartNr.HeaderText = "Part Number"
        Me.PartNr.Name = "PartNr"
        Me.PartNr.ReadOnly = True
        '
        'Oper
        '
        Me.Oper.DataPropertyName = "OperatorName"
        Me.Oper.HeaderText = "Operator"
        Me.Oper.Name = "Oper"
        Me.Oper.ReadOnly = True
        '
        'Comments
        '
        Me.Comments.DataPropertyName = "Comments"
        Me.Comments.HeaderText = "Comments"
        Me.Comments.Name = "Comments"
        Me.Comments.ReadOnly = True
        Me.Comments.Width = 300
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
        'gboxDetails
        '
        Me.gboxDetails.Controls.Add(Me.grpEditForm)
        Me.gboxDetails.Controls.Add(Me.btnCancelChange)
        Me.gboxDetails.Controls.Add(Me.btnSaveChange)
        Me.gboxDetails.Controls.Add(Me.btnStartChange)
        Me.gboxDetails.Controls.Add(Me.txtDescription)
        Me.gboxDetails.Controls.Add(Me.Label5)
        Me.gboxDetails.Location = New System.Drawing.Point(3, 3)
        Me.gboxDetails.Name = "gboxDetails"
        Me.gboxDetails.Size = New System.Drawing.Size(1083, 486)
        Me.gboxDetails.TabIndex = 10
        Me.gboxDetails.TabStop = False
        '
        'grpEditForm
        '
        Me.grpEditForm.Controls.Add(Me.pnlStatus)
        Me.grpEditForm.Controls.Add(Me.rdbOperator)
        Me.grpEditForm.Controls.Add(Me.rdbPartNumber)
        Me.grpEditForm.Controls.Add(Me.rdbStatus)
        Me.grpEditForm.Controls.Add(Me.pnlOperator)
        Me.grpEditForm.Controls.Add(Me.btnCancel)
        Me.grpEditForm.Controls.Add(Me.btnApply)
        Me.grpEditForm.Controls.Add(Me.pnlPartNumber)
        Me.grpEditForm.Controls.Add(Me.btnNew)
        Me.grpEditForm.Controls.Add(Me.btnEdit)
        Me.grpEditForm.Controls.Add(Me.btnDeleteLines)
        Me.grpEditForm.Location = New System.Drawing.Point(6, 64)
        Me.grpEditForm.Name = "grpEditForm"
        Me.grpEditForm.Size = New System.Drawing.Size(1069, 322)
        Me.grpEditForm.TabIndex = 29
        Me.grpEditForm.TabStop = False
        '
        'pnlStatus
        '
        Me.pnlStatus.Controls.Add(Me.cmbStatus)
        Me.pnlStatus.Controls.Add(Me.lblLimitStartMax)
        Me.pnlStatus.Controls.Add(Me.lblLimitEndMin)
        Me.pnlStatus.Controls.Add(Me.timePickerEnd)
        Me.pnlStatus.Controls.Add(Me.lblLimitEndMax)
        Me.pnlStatus.Controls.Add(Me.Label3)
        Me.pnlStatus.Controls.Add(Me.timePickerStart)
        Me.pnlStatus.Controls.Add(Me.Label2)
        Me.pnlStatus.Controls.Add(Me.Label1)
        Me.pnlStatus.Controls.Add(Me.lblLimitStartMin)
        Me.pnlStatus.Controls.Add(Me.Label7)
        Me.pnlStatus.Controls.Add(Me.txtComments)
        Me.pnlStatus.Location = New System.Drawing.Point(44, 14)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(1000, 100)
        Me.pnlStatus.TabIndex = 38
        '
        'cmbStatus
        '
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(11, 28)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(218, 25)
        Me.cmbStatus.TabIndex = 10
        '
        'lblLimitStartMax
        '
        Me.lblLimitStartMax.ForeColor = System.Drawing.Color.Blue
        Me.lblLimitStartMax.Location = New System.Drawing.Point(253, 77)
        Me.lblLimitStartMax.Name = "lblLimitStartMax"
        Me.lblLimitStartMax.Size = New System.Drawing.Size(123, 18)
        Me.lblLimitStartMax.TabIndex = 22
        Me.lblLimitStartMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLimitEndMin
        '
        Me.lblLimitEndMin.ForeColor = System.Drawing.Color.Blue
        Me.lblLimitEndMin.Location = New System.Drawing.Point(403, 56)
        Me.lblLimitEndMin.Name = "lblLimitEndMin"
        Me.lblLimitEndMin.Size = New System.Drawing.Size(123, 18)
        Me.lblLimitEndMin.TabIndex = 21
        Me.lblLimitEndMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'timePickerEnd
        '
        Me.timePickerEnd.CustomFormat = "HH:mm:ss"
        Me.timePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timePickerEnd.Location = New System.Drawing.Point(406, 28)
        Me.timePickerEnd.Name = "timePickerEnd"
        Me.timePickerEnd.ShowUpDown = True
        Me.timePickerEnd.Size = New System.Drawing.Size(120, 25)
        Me.timePickerEnd.TabIndex = 14
        '
        'lblLimitEndMax
        '
        Me.lblLimitEndMax.ForeColor = System.Drawing.Color.Blue
        Me.lblLimitEndMax.Location = New System.Drawing.Point(403, 77)
        Me.lblLimitEndMax.Name = "lblLimitEndMax"
        Me.lblLimitEndMax.Size = New System.Drawing.Size(123, 18)
        Me.lblLimitEndMax.TabIndex = 23
        Me.lblLimitEndMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(403, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 17)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "End:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'timePickerStart
        '
        Me.timePickerStart.CustomFormat = "HH:mm:ss"
        Me.timePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.timePickerStart.Location = New System.Drawing.Point(256, 28)
        Me.timePickerStart.Name = "timePickerStart"
        Me.timePickerStart.ShowUpDown = True
        Me.timePickerStart.Size = New System.Drawing.Size(120, 25)
        Me.timePickerStart.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(253, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 17)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Start:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 17)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Status:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLimitStartMin
        '
        Me.lblLimitStartMin.ForeColor = System.Drawing.Color.Blue
        Me.lblLimitStartMin.Location = New System.Drawing.Point(253, 56)
        Me.lblLimitStartMin.Name = "lblLimitStartMin"
        Me.lblLimitStartMin.Size = New System.Drawing.Size(123, 18)
        Me.lblLimitStartMin.TabIndex = 20
        Me.lblLimitStartMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(553, 7)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 17)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Comments:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComments
        '
        Me.txtComments.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtComments.Location = New System.Drawing.Point(556, 28)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(433, 67)
        Me.txtComments.TabIndex = 25
        '
        'rdbOperator
        '
        Me.rdbOperator.AutoSize = True
        Me.rdbOperator.Location = New System.Drawing.Point(19, 193)
        Me.rdbOperator.Name = "rdbOperator"
        Me.rdbOperator.Size = New System.Drawing.Size(14, 13)
        Me.rdbOperator.TabIndex = 37
        Me.rdbOperator.TabStop = True
        Me.rdbOperator.UseVisualStyleBackColor = True
        '
        'rdbPartNumber
        '
        Me.rdbPartNumber.AutoSize = True
        Me.rdbPartNumber.Location = New System.Drawing.Point(19, 127)
        Me.rdbPartNumber.Name = "rdbPartNumber"
        Me.rdbPartNumber.Size = New System.Drawing.Size(14, 13)
        Me.rdbPartNumber.TabIndex = 36
        Me.rdbPartNumber.TabStop = True
        Me.rdbPartNumber.UseVisualStyleBackColor = True
        '
        'rdbStatus
        '
        Me.rdbStatus.AutoSize = True
        Me.rdbStatus.Location = New System.Drawing.Point(19, 23)
        Me.rdbStatus.Name = "rdbStatus"
        Me.rdbStatus.Size = New System.Drawing.Size(14, 13)
        Me.rdbStatus.TabIndex = 35
        Me.rdbStatus.TabStop = True
        Me.rdbStatus.UseVisualStyleBackColor = True
        '
        'pnlOperator
        '
        Me.pnlOperator.Controls.Add(Me.rdbOPCurrentLine)
        Me.pnlOperator.Controls.Add(Me.rdbOPSelectedLines)
        Me.pnlOperator.Controls.Add(Me.txtOperator)
        Me.pnlOperator.Controls.Add(Me.Label8)
        Me.pnlOperator.Location = New System.Drawing.Point(44, 186)
        Me.pnlOperator.Name = "pnlOperator"
        Me.pnlOperator.Size = New System.Drawing.Size(1000, 60)
        Me.pnlOperator.TabIndex = 32
        '
        'rdbOPCurrentLine
        '
        Me.rdbOPCurrentLine.AutoSize = True
        Me.rdbOPCurrentLine.Location = New System.Drawing.Point(259, 26)
        Me.rdbOPCurrentLine.Name = "rdbOPCurrentLine"
        Me.rdbOPCurrentLine.Size = New System.Drawing.Size(102, 21)
        Me.rdbOPCurrentLine.TabIndex = 28
        Me.rdbOPCurrentLine.TabStop = True
        Me.rdbOPCurrentLine.Text = "Current Entry"
        Me.rdbOPCurrentLine.UseVisualStyleBackColor = True
        Me.rdbOPCurrentLine.Visible = False
        '
        'rdbOPSelectedLines
        '
        Me.rdbOPSelectedLines.AutoSize = True
        Me.rdbOPSelectedLines.Location = New System.Drawing.Point(377, 26)
        Me.rdbOPSelectedLines.Name = "rdbOPSelectedLines"
        Me.rdbOPSelectedLines.Size = New System.Drawing.Size(118, 21)
        Me.rdbOPSelectedLines.TabIndex = 29
        Me.rdbOPSelectedLines.TabStop = True
        Me.rdbOPSelectedLines.Text = "Selected Entries"
        Me.rdbOPSelectedLines.UseVisualStyleBackColor = True
        Me.rdbOPSelectedLines.Visible = False
        '
        'txtOperator
        '
        Me.txtOperator.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOperator.Location = New System.Drawing.Point(11, 25)
        Me.txtOperator.Name = "txtOperator"
        Me.txtOperator.Size = New System.Drawing.Size(218, 25)
        Me.txtOperator.TabIndex = 34
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 5)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 17)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "Operator"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Enabled = False
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(559, 270)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 34)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnApply.Enabled = False
        Me.btnApply.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApply.Location = New System.Drawing.Point(433, 270)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(120, 34)
        Me.btnApply.TabIndex = 17
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'pnlPartNumber
        '
        Me.pnlPartNumber.Controls.Add(Me.rbtPNCurrentLine)
        Me.pnlPartNumber.Controls.Add(Me.rbtPNSelectedLines)
        Me.pnlPartNumber.Controls.Add(Me.txtPartNumber)
        Me.pnlPartNumber.Controls.Add(Me.Label4)
        Me.pnlPartNumber.Location = New System.Drawing.Point(44, 120)
        Me.pnlPartNumber.Name = "pnlPartNumber"
        Me.pnlPartNumber.Size = New System.Drawing.Size(1000, 60)
        Me.pnlPartNumber.TabIndex = 31
        '
        'rbtPNCurrentLine
        '
        Me.rbtPNCurrentLine.AutoSize = True
        Me.rbtPNCurrentLine.Location = New System.Drawing.Point(259, 26)
        Me.rbtPNCurrentLine.Name = "rbtPNCurrentLine"
        Me.rbtPNCurrentLine.Size = New System.Drawing.Size(102, 21)
        Me.rbtPNCurrentLine.TabIndex = 28
        Me.rbtPNCurrentLine.TabStop = True
        Me.rbtPNCurrentLine.Text = "Current Entry"
        Me.rbtPNCurrentLine.UseVisualStyleBackColor = True
        Me.rbtPNCurrentLine.Visible = False
        '
        'rbtPNSelectedLines
        '
        Me.rbtPNSelectedLines.AutoSize = True
        Me.rbtPNSelectedLines.Location = New System.Drawing.Point(377, 26)
        Me.rbtPNSelectedLines.Name = "rbtPNSelectedLines"
        Me.rbtPNSelectedLines.Size = New System.Drawing.Size(118, 21)
        Me.rbtPNSelectedLines.TabIndex = 29
        Me.rbtPNSelectedLines.TabStop = True
        Me.rbtPNSelectedLines.Text = "Selected Entries"
        Me.rbtPNSelectedLines.UseVisualStyleBackColor = True
        Me.rbtPNSelectedLines.Visible = False
        '
        'txtPartNumber
        '
        Me.txtPartNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPartNumber.Location = New System.Drawing.Point(11, 25)
        Me.txtPartNumber.Name = "txtPartNumber"
        Me.txtPartNumber.Size = New System.Drawing.Size(218, 25)
        Me.txtPartNumber.TabIndex = 27
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 17)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Part Number:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNew.Enabled = False
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(181, 270)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(120, 34)
        Me.btnNew.TabIndex = 16
        Me.btnNew.Text = "New Entry"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.Enabled = False
        Me.btnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.Location = New System.Drawing.Point(55, 270)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(120, 34)
        Me.btnEdit.TabIndex = 15
        Me.btnEdit.Text = "Edit Entry"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnDeleteLines
        '
        Me.btnDeleteLines.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteLines.Enabled = False
        Me.btnDeleteLines.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteLines.Location = New System.Drawing.Point(307, 270)
        Me.btnDeleteLines.Name = "btnDeleteLines"
        Me.btnDeleteLines.Size = New System.Drawing.Size(120, 34)
        Me.btnDeleteLines.TabIndex = 8
        Me.btnDeleteLines.Text = "Delete Entries"
        Me.btnDeleteLines.UseVisualStyleBackColor = True
        '
        'btnCancelChange
        '
        Me.btnCancelChange.Enabled = False
        Me.btnCancelChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelChange.Location = New System.Drawing.Point(187, 443)
        Me.btnCancelChange.Name = "btnCancelChange"
        Me.btnCancelChange.Size = New System.Drawing.Size(120, 34)
        Me.btnCancelChange.TabIndex = 20
        Me.btnCancelChange.Text = "Cancel"
        Me.btnCancelChange.UseVisualStyleBackColor = True
        '
        'btnSaveChange
        '
        Me.btnSaveChange.Enabled = False
        Me.btnSaveChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveChange.Location = New System.Drawing.Point(61, 443)
        Me.btnSaveChange.Name = "btnSaveChange"
        Me.btnSaveChange.Size = New System.Drawing.Size(120, 34)
        Me.btnSaveChange.TabIndex = 19
        Me.btnSaveChange.Text = "Save"
        Me.btnSaveChange.UseVisualStyleBackColor = True
        '
        'btnStartChange
        '
        Me.btnStartChange.Enabled = False
        Me.btnStartChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartChange.Location = New System.Drawing.Point(61, 24)
        Me.btnStartChange.Name = "btnStartChange"
        Me.btnStartChange.Size = New System.Drawing.Size(210, 34)
        Me.btnStartChange.TabIndex = 18
        Me.btnStartChange.Text = "Edit Timeline"
        Me.btnStartChange.UseVisualStyleBackColor = True
        '
        'txtDescription
        '
        Me.txtDescription.Enabled = False
        Me.txtDescription.Location = New System.Drawing.Point(187, 401)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(849, 25)
        Me.txtDescription.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(58, 404)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(127, 17)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Changes Comments:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnMarkSelected
        '
        Me.btnMarkSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMarkSelected.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMarkSelected.Location = New System.Drawing.Point(64, 259)
        Me.btnMarkSelected.Name = "btnMarkSelected"
        Me.btnMarkSelected.Size = New System.Drawing.Size(132, 25)
        Me.btnMarkSelected.TabIndex = 39
        Me.btnMarkSelected.Text = "Mark selected lines"
        Me.btnMarkSelected.UseVisualStyleBackColor = True
        '
        'rdbEdits
        '
        Me.rdbEdits.AutoSize = True
        Me.rdbEdits.Location = New System.Drawing.Point(978, 13)
        Me.rdbEdits.Name = "rdbEdits"
        Me.rdbEdits.Size = New System.Drawing.Size(54, 21)
        Me.rdbEdits.TabIndex = 28
        Me.rdbEdits.TabStop = True
        Me.rdbEdits.Text = "Edits"
        Me.rdbEdits.UseVisualStyleBackColor = True
        '
        'rdbTimeline
        '
        Me.rdbTimeline.AutoSize = True
        Me.rdbTimeline.Location = New System.Drawing.Point(898, 13)
        Me.rdbTimeline.Name = "rdbTimeline"
        Me.rdbTimeline.Size = New System.Drawing.Size(74, 21)
        Me.rdbTimeline.TabIndex = 27
        Me.rdbTimeline.TabStop = True
        Me.rdbTimeline.Text = "Timeline"
        Me.rdbTimeline.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(816, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 28)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Display:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(3, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(306, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Timeline - Raw Data"
        '
        'dgvEdits
        '
        Me.dgvEdits.AllowUserToAddRows = False
        Me.dgvEdits.AllowUserToDeleteRows = False
        Me.dgvEdits.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvEdits.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvEdits.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvEdits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEdits.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.Action, Me.OriginalStatus, Me.NewStatus, Me.OriginalTimeStart, Me.NewTimeStart, Me.OriginalCycletime, Me.NewCycletime, Me.EditImage, Me.OriginalPartNumber, Me.NewPartNumber, Me.OriginalOperator, Me.NewOperator, Me.OriginalComments, Me.NewComments, Me.EditedBy, Me.EditedWhen, Me.EditDescription})
        Me.dgvEdits.EnableHeadersVisualStyles = False
        Me.dgvEdits.Location = New System.Drawing.Point(3, 52)
        Me.dgvEdits.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgvEdits.Name = "dgvEdits"
        Me.dgvEdits.RowTemplate.Height = 24
        Me.dgvEdits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEdits.Size = New System.Drawing.Size(1094, 202)
        Me.dgvEdits.TabIndex = 11
        '
        'Id
        '
        Me.Id.DataPropertyName = "EditId"
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.Visible = False
        '
        'Action
        '
        Me.Action.DataPropertyName = "Action"
        Me.Action.HeaderText = "Action"
        Me.Action.Name = "Action"
        Me.Action.ReadOnly = True
        Me.Action.Width = 60
        '
        'OriginalStatus
        '
        Me.OriginalStatus.DataPropertyName = "OriginalStatus"
        Me.OriginalStatus.HeaderText = "Original Status"
        Me.OriginalStatus.Name = "OriginalStatus"
        Me.OriginalStatus.ReadOnly = True
        Me.OriginalStatus.Width = 120
        '
        'NewStatus
        '
        Me.NewStatus.DataPropertyName = "NewStatus"
        Me.NewStatus.HeaderText = "New Status"
        Me.NewStatus.Name = "NewStatus"
        Me.NewStatus.ReadOnly = True
        Me.NewStatus.Width = 120
        '
        'OriginalTimeStart
        '
        Me.OriginalTimeStart.DataPropertyName = "OriginalTimeStart"
        DataGridViewCellStyle12.Format = "yyyy-MM-dd HH:mm:ss"
        Me.OriginalTimeStart.DefaultCellStyle = DataGridViewCellStyle12
        Me.OriginalTimeStart.HeaderText = "Original Time Start"
        Me.OriginalTimeStart.Name = "OriginalTimeStart"
        Me.OriginalTimeStart.ReadOnly = True
        Me.OriginalTimeStart.Width = 150
        '
        'NewTimeStart
        '
        Me.NewTimeStart.DataPropertyName = "NewTimeStart"
        DataGridViewCellStyle13.Format = "yyyy-MM-dd HH:mm:ss"
        Me.NewTimeStart.DefaultCellStyle = DataGridViewCellStyle13
        Me.NewTimeStart.HeaderText = "New Time Start"
        Me.NewTimeStart.Name = "NewTimeStart"
        Me.NewTimeStart.ReadOnly = True
        Me.NewTimeStart.Width = 150
        '
        'OriginalCycletime
        '
        Me.OriginalCycletime.DataPropertyName = "OriginalCycletime"
        Me.OriginalCycletime.HeaderText = "Original Cycletime"
        Me.OriginalCycletime.Name = "OriginalCycletime"
        Me.OriginalCycletime.ReadOnly = True
        '
        'NewCycletime
        '
        Me.NewCycletime.DataPropertyName = "NewCycletime"
        Me.NewCycletime.HeaderText = "New Cycletime"
        Me.NewCycletime.Name = "NewCycletime"
        Me.NewCycletime.ReadOnly = True
        '
        'EditImage
        '
        Me.EditImage.DataPropertyName = "Image"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.NullValue = CType(resources.GetObject("DataGridViewCellStyle14.NullValue"), Object)
        Me.EditImage.DefaultCellStyle = DataGridViewCellStyle14
        Me.EditImage.HeaderText = ""
        Me.EditImage.Name = "EditImage"
        Me.EditImage.ReadOnly = True
        Me.EditImage.Width = 150
        '
        'OriginalPartNumber
        '
        Me.OriginalPartNumber.DataPropertyName = "OriginalPartNumber"
        Me.OriginalPartNumber.HeaderText = "Original Part Number"
        Me.OriginalPartNumber.Name = "OriginalPartNumber"
        Me.OriginalPartNumber.ReadOnly = True
        Me.OriginalPartNumber.Width = 150
        '
        'NewPartNumber
        '
        Me.NewPartNumber.DataPropertyName = "NewPartNumber"
        Me.NewPartNumber.HeaderText = "New Part Number"
        Me.NewPartNumber.Name = "NewPartNumber"
        Me.NewPartNumber.ReadOnly = True
        Me.NewPartNumber.Width = 150
        '
        'OriginalOperator
        '
        Me.OriginalOperator.DataPropertyName = "OriginalOperatorName"
        Me.OriginalOperator.HeaderText = "Original Operator"
        Me.OriginalOperator.Name = "OriginalOperator"
        Me.OriginalOperator.ReadOnly = True
        '
        'NewOperator
        '
        Me.NewOperator.DataPropertyName = "NewOperatorName"
        Me.NewOperator.HeaderText = "New Operator"
        Me.NewOperator.Name = "NewOperator"
        Me.NewOperator.ReadOnly = True
        '
        'OriginalComments
        '
        Me.OriginalComments.DataPropertyName = "OriginalComments"
        Me.OriginalComments.HeaderText = "Original Comments"
        Me.OriginalComments.Name = "OriginalComments"
        Me.OriginalComments.ReadOnly = True
        Me.OriginalComments.Width = 200
        '
        'NewComments
        '
        Me.NewComments.DataPropertyName = "NewComments"
        Me.NewComments.HeaderText = "New Comments"
        Me.NewComments.Name = "NewComments"
        Me.NewComments.ReadOnly = True
        Me.NewComments.Width = 200
        '
        'EditedBy
        '
        Me.EditedBy.DataPropertyName = "EditBy"
        Me.EditedBy.HeaderText = "Edited By"
        Me.EditedBy.Name = "EditedBy"
        Me.EditedBy.ReadOnly = True
        '
        'EditedWhen
        '
        Me.EditedWhen.DataPropertyName = "EditWhen"
        Me.EditedWhen.HeaderText = "Edited When"
        Me.EditedWhen.Name = "EditedWhen"
        Me.EditedWhen.ReadOnly = True
        Me.EditedWhen.Width = 150
        '
        'EditDescription
        '
        Me.EditDescription.DataPropertyName = "EditDescription"
        Me.EditDescription.HeaderText = "Edit Description"
        Me.EditDescription.Name = "EditDescription"
        Me.EditDescription.ReadOnly = True
        Me.EditDescription.Width = 300
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BackColor = System.Drawing.Color.Gainsboro
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnExportRawData)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnClearSelection)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnMarkSelected)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbEdits)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbTimeline)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTitle)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvEdits)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvTimeline)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1106, 775)
        Me.SplitContainer1.SplitterDistance = 289
        Me.SplitContainer1.TabIndex = 0
        '
        'btnExportRawData
        '
        Me.btnExportRawData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExportRawData.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportRawData.Location = New System.Drawing.Point(965, 259)
        Me.btnExportRawData.Name = "btnExportRawData"
        Me.btnExportRawData.Size = New System.Drawing.Size(132, 25)
        Me.btnExportRawData.TabIndex = 41
        Me.btnExportRawData.Text = "Export CSV"
        Me.btnExportRawData.UseVisualStyleBackColor = True
        '
        'btnClearSelection
        '
        Me.btnClearSelection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClearSelection.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearSelection.Location = New System.Drawing.Point(202, 259)
        Me.btnClearSelection.Name = "btnClearSelection"
        Me.btnClearSelection.Size = New System.Drawing.Size(132, 25)
        Me.btnClearSelection.TabIndex = 40
        Me.btnClearSelection.Text = "Clear selection"
        Me.btnClearSelection.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.gboxDetails)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1094, 476)
        Me.Panel1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1125, 814)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1117, 784)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Timeline - Raw Data"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.SplitContainer2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1117, 784)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "OVRs Details"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BackColor = System.Drawing.Color.Gainsboro
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnOVRExportCsv)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkSpindle)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkRapid)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkFeedrate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label9)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chartOverride)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvOverride)
        Me.SplitContainer2.Size = New System.Drawing.Size(1111, 778)
        Me.SplitContainer2.SplitterDistance = 281
        Me.SplitContainer2.TabIndex = 0
        '
        'btnOVRExportCsv
        '
        Me.btnOVRExportCsv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOVRExportCsv.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOVRExportCsv.Location = New System.Drawing.Point(969, 242)
        Me.btnOVRExportCsv.Name = "btnOVRExportCsv"
        Me.btnOVRExportCsv.Size = New System.Drawing.Size(132, 25)
        Me.btnOVRExportCsv.TabIndex = 42
        Me.btnOVRExportCsv.Text = "Export CSV"
        Me.btnOVRExportCsv.UseVisualStyleBackColor = True
        '
        'chkSpindle
        '
        Me.chkSpindle.AutoSize = True
        Me.chkSpindle.Enabled = False
        Me.chkSpindle.Location = New System.Drawing.Point(159, 255)
        Me.chkSpindle.Name = "chkSpindle"
        Me.chkSpindle.Size = New System.Drawing.Size(70, 21)
        Me.chkSpindle.TabIndex = 4
        Me.chkSpindle.Text = "Spindle"
        Me.chkSpindle.UseVisualStyleBackColor = True
        '
        'chkRapid
        '
        Me.chkRapid.AutoSize = True
        Me.chkRapid.Enabled = False
        Me.chkRapid.Location = New System.Drawing.Point(92, 255)
        Me.chkRapid.Name = "chkRapid"
        Me.chkRapid.Size = New System.Drawing.Size(61, 21)
        Me.chkRapid.TabIndex = 3
        Me.chkRapid.Text = "Rapid"
        Me.chkRapid.UseVisualStyleBackColor = True
        '
        'chkFeedrate
        '
        Me.chkFeedrate.AutoSize = True
        Me.chkFeedrate.Enabled = False
        Me.chkFeedrate.Location = New System.Drawing.Point(8, 255)
        Me.chkFeedrate.Name = "chkFeedrate"
        Me.chkFeedrate.Size = New System.Drawing.Size(78, 21)
        Me.chkFeedrate.TabIndex = 2
        Me.chkFeedrate.Text = "Feedrate"
        Me.chkFeedrate.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(306, 25)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "OVRs Details"
        '
        'chartOverride
        '
        Me.chartOverride.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea2.Name = "ChartArea1"
        Me.chartOverride.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.chartOverride.Legends.Add(Legend2)
        Me.chartOverride.Location = New System.Drawing.Point(5, 48)
        Me.chartOverride.Name = "chartOverride"
        Me.chartOverride.Size = New System.Drawing.Size(1101, 190)
        Me.chartOverride.TabIndex = 0
        Me.chartOverride.Text = "Chart1"
        '
        'dgvOverride
        '
        Me.dgvOverride.AllowUserToAddRows = False
        Me.dgvOverride.AllowUserToDeleteRows = False
        Me.dgvOverride.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvOverride.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvOverride.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOverride.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ovrEventDateTime, Me.ovrStatus, Me.ovrPartNumber, Me.ovrOperation, Me.ovrFeedrate, Me.ovrRapid, Me.ovrSpindle, Me.OvrCycleTime})
        Me.dgvOverride.Location = New System.Drawing.Point(5, 3)
        Me.dgvOverride.Name = "dgvOverride"
        Me.dgvOverride.Size = New System.Drawing.Size(1101, 487)
        Me.dgvOverride.TabIndex = 0
        '
        'ovrEventDateTime
        '
        Me.ovrEventDateTime.DataPropertyName = "EventDateTime"
        DataGridViewCellStyle15.Format = "MM-dd-yyyy HH:mm:ss"
        DataGridViewCellStyle15.NullValue = Nothing
        Me.ovrEventDateTime.DefaultCellStyle = DataGridViewCellStyle15
        Me.ovrEventDateTime.HeaderText = "Date"
        Me.ovrEventDateTime.Name = "ovrEventDateTime"
        Me.ovrEventDateTime.ReadOnly = True
        '
        'ovrStatus
        '
        Me.ovrStatus.DataPropertyName = "Status"
        Me.ovrStatus.HeaderText = "Status"
        Me.ovrStatus.Name = "ovrStatus"
        Me.ovrStatus.ReadOnly = True
        '
        'ovrPartNumber
        '
        Me.ovrPartNumber.DataPropertyName = "PartNumber"
        Me.ovrPartNumber.HeaderText = "Part Number"
        Me.ovrPartNumber.Name = "ovrPartNumber"
        Me.ovrPartNumber.ReadOnly = True
        '
        'ovrOperation
        '
        Me.ovrOperation.DataPropertyName = "Operation"
        Me.ovrOperation.HeaderText = "Operation"
        Me.ovrOperation.Name = "ovrOperation"
        Me.ovrOperation.ReadOnly = True
        '
        'ovrFeedrate
        '
        Me.ovrFeedrate.DataPropertyName = "Feedrate"
        DataGridViewCellStyle16.NullValue = Nothing
        Me.ovrFeedrate.DefaultCellStyle = DataGridViewCellStyle16
        Me.ovrFeedrate.HeaderText = "Feedrate %"
        Me.ovrFeedrate.Name = "ovrFeedrate"
        Me.ovrFeedrate.ReadOnly = True
        '
        'ovrRapid
        '
        Me.ovrRapid.DataPropertyName = "Rapid"
        Me.ovrRapid.HeaderText = "Rapid %"
        Me.ovrRapid.Name = "ovrRapid"
        Me.ovrRapid.ReadOnly = True
        '
        'ovrSpindle
        '
        Me.ovrSpindle.DataPropertyName = "Spindle"
        Me.ovrSpindle.HeaderText = "Spindle %"
        Me.ovrSpindle.Name = "ovrSpindle"
        Me.ovrSpindle.ReadOnly = True
        '
        'OvrCycleTime
        '
        Me.OvrCycleTime.DataPropertyName = "Cycletime"
        Me.OvrCycleTime.HeaderText = "Cycle Time"
        Me.OvrCycleTime.Name = "OvrCycleTime"
        Me.OvrCycleTime.ReadOnly = True
        '
        'form_raw_values
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1125, 821)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MinimumSize = New System.Drawing.Size(1110, 39)
        Me.Name = "form_raw_values"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Raw data without processing"
        CType(Me.dgvTimeline, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboxDetails.ResumeLayout(False)
        Me.gboxDetails.PerformLayout()
        Me.grpEditForm.ResumeLayout(False)
        Me.grpEditForm.PerformLayout()
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        Me.pnlOperator.ResumeLayout(False)
        Me.pnlOperator.PerformLayout()
        Me.pnlPartNumber.ResumeLayout(False)
        Me.pnlPartNumber.PerformLayout()
        CType(Me.dgvEdits, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.chartOverride, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOverride, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvTimeline As DataGridView
    Friend WithEvents ImageList As ImageList
    Friend WithEvents gboxDetails As GroupBox
    Friend WithEvents btnCancelChange As Button
    Friend WithEvents btnSaveChange As Button
    Friend WithEvents btnStartChange As Button
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblLimitEndMax As Label
    Friend WithEvents lblLimitStartMax As Label
    Friend WithEvents lblLimitEndMin As Label
    Friend WithEvents lblLimitStartMin As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnApply As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents timePickerEnd As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents btnEdit As Button
    Friend WithEvents timePickerStart As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbStatus As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnDeleteLines As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents rdbEdits As RadioButton
    Friend WithEvents rdbTimeline As RadioButton
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents dgvEdits As DataGridView
    Friend WithEvents grpEditForm As GroupBox
    Friend WithEvents txtComments As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents rdbOperator As RadioButton
    Friend WithEvents rdbPartNumber As RadioButton
    Friend WithEvents rdbStatus As RadioButton
    Friend WithEvents txtOperator As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents pnlOperator As Panel
    Friend WithEvents rdbOPCurrentLine As RadioButton
    Friend WithEvents rdbOPSelectedLines As RadioButton
    Friend WithEvents pnlPartNumber As Panel
    Friend WithEvents rbtPNCurrentLine As RadioButton
    Friend WithEvents rbtPNSelectedLines As RadioButton
    Friend WithEvents txtPartNumber As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents pnlStatus As Panel
    Friend WithEvents TimeId As DataGridViewTextBoxColumn
    Friend WithEvents Selected As DataGridViewCheckBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents Shift As DataGridViewTextBoxColumn
    Friend WithEvents TimeStart As DataGridViewTextBoxColumn
    Friend WithEvents TimeEnd As DataGridViewTextBoxColumn
    Friend WithEvents Cycletime As DataGridViewTextBoxColumn
    Friend WithEvents Image As DataGridViewImageColumn
    Friend WithEvents PartNr As DataGridViewTextBoxColumn
    Friend WithEvents Oper As DataGridViewTextBoxColumn
    Friend WithEvents Comments As DataGridViewTextBoxColumn
    Friend WithEvents btnMarkSelected As Button
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnClearSelection As Button
    Friend WithEvents Id As DataGridViewTextBoxColumn
    Friend WithEvents Action As DataGridViewTextBoxColumn
    Friend WithEvents OriginalStatus As DataGridViewTextBoxColumn
    Friend WithEvents NewStatus As DataGridViewTextBoxColumn
    Friend WithEvents OriginalTimeStart As DataGridViewTextBoxColumn
    Friend WithEvents NewTimeStart As DataGridViewTextBoxColumn
    Friend WithEvents OriginalCycletime As DataGridViewTextBoxColumn
    Friend WithEvents NewCycletime As DataGridViewTextBoxColumn
    Friend WithEvents EditImage As DataGridViewImageColumn
    Friend WithEvents OriginalPartNumber As DataGridViewTextBoxColumn
    Friend WithEvents NewPartNumber As DataGridViewTextBoxColumn
    Friend WithEvents OriginalOperator As DataGridViewTextBoxColumn
    Friend WithEvents NewOperator As DataGridViewTextBoxColumn
    Friend WithEvents OriginalComments As DataGridViewTextBoxColumn
    Friend WithEvents NewComments As DataGridViewTextBoxColumn
    Friend WithEvents EditedBy As DataGridViewTextBoxColumn
    Friend WithEvents EditedWhen As DataGridViewTextBoxColumn
    Friend WithEvents EditDescription As DataGridViewTextBoxColumn
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents chartOverride As DataVisualization.Charting.Chart
    Friend WithEvents dgvOverride As DataGridView
    Friend WithEvents Label9 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents chkSpindle As CheckBox
    Friend WithEvents chkRapid As CheckBox
    Friend WithEvents chkFeedrate As CheckBox
    Friend WithEvents ovrEventDateTime As DataGridViewTextBoxColumn
    Friend WithEvents ovrStatus As DataGridViewTextBoxColumn
    Friend WithEvents ovrPartNumber As DataGridViewTextBoxColumn
    Friend WithEvents ovrOperation As DataGridViewTextBoxColumn
    Friend WithEvents ovrFeedrate As DataGridViewTextBoxColumn
    Friend WithEvents ovrRapid As DataGridViewTextBoxColumn
    Friend WithEvents ovrSpindle As DataGridViewTextBoxColumn
    Friend WithEvents OvrCycleTime As DataGridViewTextBoxColumn
    Friend WithEvents btnExportRawData As Button
    Friend WithEvents btnOVRExportCsv As Button
End Class

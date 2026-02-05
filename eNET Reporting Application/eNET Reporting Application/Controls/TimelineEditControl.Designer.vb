<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TimelineEditControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TimelineEditControl))
        Me.GroupBox = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbChanges = New System.Windows.Forms.ComboBox()
        Me.gboxDetails = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.btnCancelChange = New System.Windows.Forms.Button()
        Me.btnSaveChange = New System.Windows.Forms.Button()
        Me.btnStartChange = New System.Windows.Forms.Button()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblLimitEndMax = New System.Windows.Forms.Label()
        Me.lblLimitStartMax = New System.Windows.Forms.Label()
        Me.lblLimitEndMin = New System.Windows.Forms.Label()
        Me.lblLimitStartMin = New System.Windows.Forms.Label()
        Me.lblInformation = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.timePickerEnd = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.timePickerStart = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnDeleteLines = New System.Windows.Forms.Button()
        Me.lblShift = New System.Windows.Forms.Label()
        Me.cmbShift = New System.Windows.Forms.ComboBox()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.dgStatus = New System.Windows.Forms.DataGridView()
        Me.Selected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.OriginalDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Edited = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Shift = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeEnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cycletime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Image = New System.Windows.Forms.DataGridViewImageColumn()
        Me.HasBefore = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.HasAfter = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.DateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.lblMachine = New System.Windows.Forms.Label()
        Me.cmbMachines = New System.Windows.Forms.ComboBox()
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox.SuspendLayout()
        Me.gboxDetails.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox
        '
        Me.GroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox.Controls.Add(Me.Label6)
        Me.GroupBox.Controls.Add(Me.cmbChanges)
        Me.GroupBox.Controls.Add(Me.gboxDetails)
        Me.GroupBox.Controls.Add(Me.lblShift)
        Me.GroupBox.Controls.Add(Me.cmbShift)
        Me.GroupBox.Controls.Add(Me.btnLoad)
        Me.GroupBox.Controls.Add(Me.dgStatus)
        Me.GroupBox.Controls.Add(Me.lblDate)
        Me.GroupBox.Controls.Add(Me.DateTimePicker)
        Me.GroupBox.Controls.Add(Me.lblMachine)
        Me.GroupBox.Controls.Add(Me.cmbMachines)
        Me.GroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox.Name = "GroupBox"
        Me.GroupBox.Size = New System.Drawing.Size(1000, 800)
        Me.GroupBox.TabIndex = 0
        Me.GroupBox.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 75)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 16)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Changes"
        '
        'cmbChanges
        '
        Me.cmbChanges.FormattingEnabled = True
        Me.cmbChanges.Location = New System.Drawing.Point(20, 94)
        Me.cmbChanges.Name = "cmbChanges"
        Me.cmbChanges.Size = New System.Drawing.Size(262, 24)
        Me.cmbChanges.TabIndex = 10
        '
        'gboxDetails
        '
        Me.gboxDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxDetails.Controls.Add(Me.GroupBox1)
        Me.gboxDetails.Controls.Add(Me.lblLimitEndMax)
        Me.gboxDetails.Controls.Add(Me.lblLimitStartMax)
        Me.gboxDetails.Controls.Add(Me.lblLimitEndMin)
        Me.gboxDetails.Controls.Add(Me.lblLimitStartMin)
        Me.gboxDetails.Controls.Add(Me.lblInformation)
        Me.gboxDetails.Controls.Add(Me.btnCancel)
        Me.gboxDetails.Controls.Add(Me.btnApply)
        Me.gboxDetails.Controls.Add(Me.btnNew)
        Me.gboxDetails.Controls.Add(Me.timePickerEnd)
        Me.gboxDetails.Controls.Add(Me.Label3)
        Me.gboxDetails.Controls.Add(Me.btnEdit)
        Me.gboxDetails.Controls.Add(Me.timePickerStart)
        Me.gboxDetails.Controls.Add(Me.Label2)
        Me.gboxDetails.Controls.Add(Me.cmbStatus)
        Me.gboxDetails.Controls.Add(Me.Label1)
        Me.gboxDetails.Controls.Add(Me.btnDeleteLines)
        Me.gboxDetails.Location = New System.Drawing.Point(20, 437)
        Me.gboxDetails.Name = "gboxDetails"
        Me.gboxDetails.Size = New System.Drawing.Size(960, 357)
        Me.gboxDetails.TabIndex = 9
        Me.gboxDetails.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblInfo)
        Me.GroupBox1.Controls.Add(Me.btnCancelChange)
        Me.GroupBox1.Controls.Add(Me.btnSaveChange)
        Me.GroupBox1.Controls.Add(Me.btnStartChange)
        Me.GroupBox1.Controls.Add(Me.txtDescription)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(948, 165)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        '
        'lblInfo
        '
        Me.lblInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInfo.Location = New System.Drawing.Point(424, 129)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(518, 30)
        Me.lblInfo.TabIndex = 21
        Me.lblInfo.Text = "Description:"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancelChange
        '
        Me.btnCancelChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancelChange.Enabled = False
        Me.btnCancelChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelChange.Location = New System.Drawing.Point(318, 129)
        Me.btnCancelChange.Name = "btnCancelChange"
        Me.btnCancelChange.Size = New System.Drawing.Size(100, 30)
        Me.btnCancelChange.TabIndex = 20
        Me.btnCancelChange.Text = "Cancel"
        Me.btnCancelChange.UseVisualStyleBackColor = True
        '
        'btnSaveChange
        '
        Me.btnSaveChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveChange.Enabled = False
        Me.btnSaveChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveChange.Location = New System.Drawing.Point(212, 129)
        Me.btnSaveChange.Name = "btnSaveChange"
        Me.btnSaveChange.Size = New System.Drawing.Size(100, 30)
        Me.btnSaveChange.TabIndex = 19
        Me.btnSaveChange.Text = "Save"
        Me.btnSaveChange.UseVisualStyleBackColor = True
        '
        'btnStartChange
        '
        Me.btnStartChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStartChange.Enabled = False
        Me.btnStartChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartChange.Location = New System.Drawing.Point(92, 129)
        Me.btnStartChange.Name = "btnStartChange"
        Me.btnStartChange.Size = New System.Drawing.Size(114, 30)
        Me.btnStartChange.TabIndex = 18
        Me.btnStartChange.Text = "Start Change"
        Me.btnStartChange.UseVisualStyleBackColor = True
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.Enabled = False
        Me.txtDescription.Location = New System.Drawing.Point(92, 58)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.Size = New System.Drawing.Size(850, 65)
        Me.txtDescription.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 16)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Description:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(203, 20)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Change Timeline History"
        '
        'lblLimitEndMax
        '
        Me.lblLimitEndMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLimitEndMax.ForeColor = System.Drawing.Color.Blue
        Me.lblLimitEndMax.Location = New System.Drawing.Point(401, 276)
        Me.lblLimitEndMax.Name = "lblLimitEndMax"
        Me.lblLimitEndMax.Size = New System.Drawing.Size(123, 16)
        Me.lblLimitEndMax.TabIndex = 23
        Me.lblLimitEndMax.Text = "lblLimitEndMax"
        Me.lblLimitEndMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLimitStartMax
        '
        Me.lblLimitStartMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLimitStartMax.ForeColor = System.Drawing.Color.Blue
        Me.lblLimitStartMax.Location = New System.Drawing.Point(251, 276)
        Me.lblLimitStartMax.Name = "lblLimitStartMax"
        Me.lblLimitStartMax.Size = New System.Drawing.Size(123, 16)
        Me.lblLimitStartMax.TabIndex = 22
        Me.lblLimitStartMax.Text = "lblLimitStartMax"
        Me.lblLimitStartMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLimitEndMin
        '
        Me.lblLimitEndMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLimitEndMin.ForeColor = System.Drawing.Color.Blue
        Me.lblLimitEndMin.Location = New System.Drawing.Point(401, 258)
        Me.lblLimitEndMin.Name = "lblLimitEndMin"
        Me.lblLimitEndMin.Size = New System.Drawing.Size(123, 16)
        Me.lblLimitEndMin.TabIndex = 21
        Me.lblLimitEndMin.Text = "lblLimitEndMin"
        Me.lblLimitEndMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLimitStartMin
        '
        Me.lblLimitStartMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLimitStartMin.ForeColor = System.Drawing.Color.Blue
        Me.lblLimitStartMin.Location = New System.Drawing.Point(251, 258)
        Me.lblLimitStartMin.Name = "lblLimitStartMin"
        Me.lblLimitStartMin.Size = New System.Drawing.Size(123, 16)
        Me.lblLimitStartMin.TabIndex = 20
        Me.lblLimitStartMin.Text = "lblLimitStartMin"
        Me.lblLimitStartMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblInformation
        '
        Me.lblInformation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblInformation.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInformation.Location = New System.Drawing.Point(6, 181)
        Me.lblInformation.Name = "lblInformation"
        Me.lblInformation.Size = New System.Drawing.Size(638, 27)
        Me.lblInformation.TabIndex = 19
        Me.lblInformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Enabled = False
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(324, 310)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 30)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnApply.Enabled = False
        Me.btnApply.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApply.Location = New System.Drawing.Point(218, 310)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(100, 30)
        Me.btnApply.TabIndex = 17
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNew.Enabled = False
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(112, 310)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 30)
        Me.btnNew.TabIndex = 16
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'timePickerEnd
        '
        Me.timePickerEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.timePickerEnd.Enabled = False
        Me.timePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.timePickerEnd.Location = New System.Drawing.Point(404, 233)
        Me.timePickerEnd.Name = "timePickerEnd"
        Me.timePickerEnd.ShowUpDown = True
        Me.timePickerEnd.Size = New System.Drawing.Size(120, 22)
        Me.timePickerEnd.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(401, 214)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 16)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "End:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.Enabled = False
        Me.btnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.Location = New System.Drawing.Point(6, 310)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(100, 30)
        Me.btnEdit.TabIndex = 15
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'timePickerStart
        '
        Me.timePickerStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.timePickerStart.Enabled = False
        Me.timePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.timePickerStart.Location = New System.Drawing.Point(254, 233)
        Me.timePickerStart.Name = "timePickerStart"
        Me.timePickerStart.ShowUpDown = True
        Me.timePickerStart.Size = New System.Drawing.Size(120, 22)
        Me.timePickerStart.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(251, 214)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 16)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Start:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbStatus
        '
        Me.cmbStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbStatus.Enabled = False
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(6, 233)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(218, 24)
        Me.cmbStatus.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 214)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Status:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnDeleteLines
        '
        Me.btnDeleteLines.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteLines.Enabled = False
        Me.btnDeleteLines.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteLines.Location = New System.Drawing.Point(754, 310)
        Me.btnDeleteLines.Name = "btnDeleteLines"
        Me.btnDeleteLines.Size = New System.Drawing.Size(200, 30)
        Me.btnDeleteLines.TabIndex = 8
        Me.btnDeleteLines.Text = "Delete Selected Lines"
        Me.btnDeleteLines.UseVisualStyleBackColor = True
        '
        'lblShift
        '
        Me.lblShift.AutoSize = True
        Me.lblShift.Location = New System.Drawing.Point(525, 20)
        Me.lblShift.Name = "lblShift"
        Me.lblShift.Size = New System.Drawing.Size(33, 16)
        Me.lblShift.TabIndex = 7
        Me.lblShift.Text = "Shift"
        '
        'cmbShift
        '
        Me.cmbShift.FormattingEnabled = True
        Me.cmbShift.Location = New System.Drawing.Point(528, 40)
        Me.cmbShift.Name = "cmbShift"
        Me.cmbShift.Size = New System.Drawing.Size(130, 24)
        Me.cmbShift.TabIndex = 6
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(688, 29)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(130, 35)
        Me.btnLoad.TabIndex = 5
        Me.btnLoad.Text = "Load Timeline"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'dgStatus
        '
        Me.dgStatus.AllowUserToAddRows = False
        Me.dgStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgStatus.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Selected, Me.OriginalDate, Me.Edited, Me.Status, Me.Shift, Me.TimeId, Me.TimeStart, Me.TimeEnd, Me.Cycletime, Me.Image, Me.HasBefore, Me.HasAfter})
        Me.dgStatus.Location = New System.Drawing.Point(20, 124)
        Me.dgStatus.MultiSelect = False
        Me.dgStatus.Name = "dgStatus"
        Me.dgStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgStatus.Size = New System.Drawing.Size(960, 307)
        Me.dgStatus.TabIndex = 4
        '
        'Selected
        '
        Me.Selected.DataPropertyName = "Selected"
        Me.Selected.HeaderText = ""
        Me.Selected.Name = "Selected"
        Me.Selected.Width = 60
        '
        'OriginalDate
        '
        Me.OriginalDate.DataPropertyName = "OriginalDate"
        Me.OriginalDate.HeaderText = "OriginalDate"
        Me.OriginalDate.Name = "OriginalDate"
        Me.OriginalDate.Visible = False
        '
        'Edited
        '
        Me.Edited.HeaderText = "Edited"
        Me.Edited.Name = "Edited"
        Me.Edited.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Edited.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Edited.Visible = False
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
        Me.Shift.Width = 60
        '
        'TimeId
        '
        Me.TimeId.DataPropertyName = "TimeId"
        Me.TimeId.HeaderText = "Time Id"
        Me.TimeId.Name = "TimeId"
        '
        'TimeStart
        '
        Me.TimeStart.DataPropertyName = "TimeStart"
        Me.TimeStart.HeaderText = "Start"
        Me.TimeStart.Name = "TimeStart"
        Me.TimeStart.ReadOnly = True
        Me.TimeStart.Width = 180
        '
        'TimeEnd
        '
        Me.TimeEnd.DataPropertyName = "TimeEnd"
        Me.TimeEnd.HeaderText = "End"
        Me.TimeEnd.Name = "TimeEnd"
        Me.TimeEnd.ReadOnly = True
        Me.TimeEnd.Width = 180
        '
        'Cycletime
        '
        Me.Cycletime.DataPropertyName = "Cycletime"
        Me.Cycletime.HeaderText = "Cycletime"
        Me.Cycletime.Name = "Cycletime"
        Me.Cycletime.ReadOnly = True
        Me.Cycletime.Width = 80
        '
        'Image
        '
        Me.Image.DataPropertyName = "Image"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.NullValue = Nothing
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.Image.DefaultCellStyle = DataGridViewCellStyle1
        Me.Image.HeaderText = ""
        Me.Image.Name = "Image"
        Me.Image.ReadOnly = True
        Me.Image.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Image.Width = 300
        '
        'HasBefore
        '
        Me.HasBefore.DataPropertyName = "HasBefore"
        Me.HasBefore.HeaderText = "HasBefore"
        Me.HasBefore.Name = "HasBefore"
        Me.HasBefore.Visible = False
        '
        'HasAfter
        '
        Me.HasAfter.DataPropertyName = "HasAfter"
        Me.HasAfter.HeaderText = "HasAfter"
        Me.HasAfter.Name = "HasAfter"
        Me.HasAfter.Visible = False
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(309, 20)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(37, 16)
        Me.lblDate.TabIndex = 3
        Me.lblDate.Text = "Date"
        '
        'DateTimePicker
        '
        Me.DateTimePicker.Location = New System.Drawing.Point(312, 40)
        Me.DateTimePicker.Name = "DateTimePicker"
        Me.DateTimePicker.Size = New System.Drawing.Size(186, 22)
        Me.DateTimePicker.TabIndex = 2
        '
        'lblMachine
        '
        Me.lblMachine.AutoSize = True
        Me.lblMachine.Location = New System.Drawing.Point(20, 20)
        Me.lblMachine.Name = "lblMachine"
        Me.lblMachine.Size = New System.Drawing.Size(59, 16)
        Me.lblMachine.TabIndex = 1
        Me.lblMachine.Text = "Machine"
        '
        'cmbMachines
        '
        Me.cmbMachines.FormattingEnabled = True
        Me.cmbMachines.Location = New System.Drawing.Point(20, 40)
        Me.cmbMachines.Name = "cmbMachines"
        Me.cmbMachines.Size = New System.Drawing.Size(262, 24)
        Me.cmbMachines.TabIndex = 0
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "BlueBar")
        Me.ImageList.Images.SetKeyName(1, "GreenBar")
        Me.ImageList.Images.SetKeyName(2, "RedBar")
        Me.ImageList.Images.SetKeyName(3, "YellowBar")
        '
        'TimelineEditControl
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.GroupBox)
        Me.Name = "TimelineEditControl"
        Me.Size = New System.Drawing.Size(1006, 806)
        Me.GroupBox.ResumeLayout(False)
        Me.GroupBox.PerformLayout()
        Me.gboxDetails.ResumeLayout(False)
        Me.gboxDetails.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox As GroupBox
    Friend WithEvents dgStatus As DataGridView
    Friend WithEvents lblDate As Label
    Friend WithEvents DateTimePicker As DateTimePicker
    Friend WithEvents lblMachine As Label
    Friend WithEvents cmbMachines As ComboBox
    Friend WithEvents btnLoad As Button
    Friend WithEvents ImageList As ImageList
    Friend WithEvents lblShift As Label
    Friend WithEvents cmbShift As ComboBox
    Friend WithEvents btnDeleteLines As Button
    Friend WithEvents gboxDetails As GroupBox
    Friend WithEvents timePickerEnd As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents timePickerStart As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbStatus As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnApply As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblInformation As Label
    Friend WithEvents lblLimitEndMin As Label
    Friend WithEvents lblLimitStartMin As Label
    Friend WithEvents lblLimitEndMax As Label
    Friend WithEvents lblLimitStartMax As Label
    Friend WithEvents Selected As DataGridViewCheckBoxColumn
    Friend WithEvents OriginalDate As DataGridViewTextBoxColumn
    Friend WithEvents Edited As DataGridViewCheckBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents Shift As DataGridViewTextBoxColumn
    Friend WithEvents TimeId As DataGridViewTextBoxColumn
    Friend WithEvents TimeStart As DataGridViewTextBoxColumn
    Friend WithEvents TimeEnd As DataGridViewTextBoxColumn
    Friend WithEvents Cycletime As DataGridViewTextBoxColumn
    Friend WithEvents Image As DataGridViewImageColumn
    Friend WithEvents HasBefore As DataGridViewCheckBoxColumn
    Friend WithEvents HasAfter As DataGridViewCheckBoxColumn
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btnCancelChange As Button
    Friend WithEvents btnSaveChange As Button
    Friend WithEvents btnStartChange As Button
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbChanges As ComboBox
    Friend WithEvents lblInfo As Label

End Class

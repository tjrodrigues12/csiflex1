<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MonitoringUnitsList
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvMonitoringUnits = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IpAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mac = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Manufacturer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Model = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SerialNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Firmware = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreatedAt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Target = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Enabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Machine_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.PictureBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chkShowDeleted = New System.Windows.Forms.CheckBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblDeleted = New System.Windows.Forms.Label()
        Me.btnSaveBoard = New System.Windows.Forms.Button()
        Me.btnNewBoard = New System.Windows.Forms.Button()
        Me.btnEditBoard = New System.Windows.Forms.Button()
        Me.btnDeleteBoard = New System.Windows.Forms.Button()
        Me.lblLinkSettings = New System.Windows.Forms.Label()
        Me.lblMachineName = New System.Windows.Forms.Label()
        Me.lblCreatedAt = New System.Windows.Forms.Label()
        Me.lblFirmware = New System.Windows.Forms.Label()
        Me.lblSerialNumber = New System.Windows.Forms.Label()
        Me.lblModel = New System.Windows.Forms.Label()
        Me.lblManufacturer = New System.Windows.Forms.Label()
        Me.lblMacAddress = New System.Windows.Forms.Label()
        Me.lblIpAddress = New System.Windows.Forms.Label()
        Me.lblUnitLabel = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dgvSensors = New System.Windows.Forms.DataGridView()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        CType(Me.dgvMonitoringUnits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvSensors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvMonitoringUnits
        '
        Me.dgvMonitoringUnits.AllowUserToAddRows = False
        Me.dgvMonitoringUnits.AllowUserToDeleteRows = False
        Me.dgvMonitoringUnits.AllowUserToOrderColumns = True
        Me.dgvMonitoringUnits.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMonitoringUnits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvMonitoringUnits.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvMonitoringUnits.ColumnHeadersHeight = 36
        Me.dgvMonitoringUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvMonitoringUnits.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.Label, Me.IpAddress, Me.Mac, Me.Manufacturer, Me.Model, Me.SerialNumber, Me.Firmware, Me.CreatedAt, Me.Target, Me.Enabled, Me.Machine_Name})
        Me.dgvMonitoringUnits.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvMonitoringUnits.Location = New System.Drawing.Point(3, 3)
        Me.dgvMonitoringUnits.MultiSelect = False
        Me.dgvMonitoringUnits.Name = "dgvMonitoringUnits"
        Me.dgvMonitoringUnits.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvMonitoringUnits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMonitoringUnits.Size = New System.Drawing.Size(1336, 195)
        Me.dgvMonitoringUnits.TabIndex = 2
        '
        'Id
        '
        Me.Id.DataPropertyName = "Id"
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.Visible = False
        '
        'Label
        '
        Me.Label.DataPropertyName = "Label"
        Me.Label.FillWeight = 102.6831!
        Me.Label.HeaderText = "Label"
        Me.Label.Name = "Label"
        Me.Label.ReadOnly = True
        '
        'IpAddress
        '
        Me.IpAddress.DataPropertyName = "IpAddress"
        Me.IpAddress.FillWeight = 102.6831!
        Me.IpAddress.HeaderText = "IP Address"
        Me.IpAddress.Name = "IpAddress"
        Me.IpAddress.ReadOnly = True
        '
        'Mac
        '
        Me.Mac.DataPropertyName = "Mac"
        Me.Mac.HeaderText = "Mac Address"
        Me.Mac.Name = "Mac"
        Me.Mac.ReadOnly = True
        Me.Mac.Visible = False
        '
        'Manufacturer
        '
        Me.Manufacturer.DataPropertyName = "Manufacturer"
        Me.Manufacturer.FillWeight = 102.6831!
        Me.Manufacturer.HeaderText = "Manufacturer"
        Me.Manufacturer.Name = "Manufacturer"
        Me.Manufacturer.ReadOnly = True
        '
        'Model
        '
        Me.Model.DataPropertyName = "Model"
        Me.Model.FillWeight = 102.6831!
        Me.Model.HeaderText = "Model"
        Me.Model.Name = "Model"
        Me.Model.ReadOnly = True
        '
        'SerialNumber
        '
        Me.SerialNumber.DataPropertyName = "SerialNumber"
        Me.SerialNumber.FillWeight = 102.6831!
        Me.SerialNumber.HeaderText = "Serial #"
        Me.SerialNumber.Name = "SerialNumber"
        Me.SerialNumber.ReadOnly = True
        '
        'Firmware
        '
        Me.Firmware.DataPropertyName = "Firmware"
        Me.Firmware.HeaderText = "Firmware"
        Me.Firmware.Name = "Firmware"
        Me.Firmware.ReadOnly = True
        Me.Firmware.Visible = False
        '
        'CreatedAt
        '
        Me.CreatedAt.DataPropertyName = "CreatedAt"
        Me.CreatedAt.FillWeight = 102.6831!
        Me.CreatedAt.HeaderText = "Created At"
        Me.CreatedAt.Name = "CreatedAt"
        Me.CreatedAt.ReadOnly = True
        '
        'Target
        '
        Me.Target.DataPropertyName = "Target"
        Me.Target.HeaderText = "Target"
        Me.Target.Name = "Target"
        Me.Target.ReadOnly = True
        Me.Target.Visible = False
        '
        'Enabled
        '
        Me.Enabled.DataPropertyName = "Enabled"
        Me.Enabled.FillWeight = 81.21828!
        Me.Enabled.HeaderText = "Enabled"
        Me.Enabled.Name = "Enabled"
        Me.Enabled.ReadOnly = True
        '
        'Machine_Name
        '
        Me.Machine_Name.DataPropertyName = "MachineName"
        Me.Machine_Name.FillWeight = 102.6831!
        Me.Machine_Name.HeaderText = "Machine"
        Me.Machine_Name.Name = "Machine_Name"
        Me.Machine_Name.ReadOnly = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(291, 33)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Monitoring Units"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.Image = Global.CSI_Reporting_Application.My.Resources.Resources.appbar_close
        Me.btnClose.Location = New System.Drawing.Point(1345, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(30, 30)
        Me.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.btnClose.TabIndex = 4
        Me.btnClose.TabStop = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer1.Location = New System.Drawing.Point(31, 66)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkShowDeleted)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvMonitoringUnits)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1344, 795)
        Me.SplitContainer1.SplitterDistance = 228
        Me.SplitContainer1.TabIndex = 7
        '
        'chkShowDeleted
        '
        Me.chkShowDeleted.AutoSize = True
        Me.chkShowDeleted.Location = New System.Drawing.Point(3, 206)
        Me.chkShowDeleted.Name = "chkShowDeleted"
        Me.chkShowDeleted.Size = New System.Drawing.Size(93, 17)
        Me.chkShowDeleted.TabIndex = 3
        Me.chkShowDeleted.Text = "Show Deleted"
        Me.chkShowDeleted.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(1336, 555)
        Me.SplitContainer2.SplitterDistance = 488
        Me.SplitContainer2.TabIndex = 17
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.lblDeleted)
        Me.Panel1.Controls.Add(Me.btnSaveBoard)
        Me.Panel1.Controls.Add(Me.btnNewBoard)
        Me.Panel1.Controls.Add(Me.btnEditBoard)
        Me.Panel1.Controls.Add(Me.btnDeleteBoard)
        Me.Panel1.Controls.Add(Me.lblLinkSettings)
        Me.Panel1.Controls.Add(Me.lblMachineName)
        Me.Panel1.Controls.Add(Me.lblCreatedAt)
        Me.Panel1.Controls.Add(Me.lblFirmware)
        Me.Panel1.Controls.Add(Me.lblSerialNumber)
        Me.Panel1.Controls.Add(Me.lblModel)
        Me.Panel1.Controls.Add(Me.lblManufacturer)
        Me.Panel1.Controls.Add(Me.lblMacAddress)
        Me.Panel1.Controls.Add(Me.lblIpAddress)
        Me.Panel1.Controls.Add(Me.lblUnitLabel)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(480, 547)
        Me.Panel1.TabIndex = 16
        '
        'lblDeleted
        '
        Me.lblDeleted.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeleted.ForeColor = System.Drawing.Color.Red
        Me.lblDeleted.Location = New System.Drawing.Point(328, 377)
        Me.lblDeleted.Name = "lblDeleted"
        Me.lblDeleted.Size = New System.Drawing.Size(123, 23)
        Me.lblDeleted.TabIndex = 26
        Me.lblDeleted.Text = "DELETED"
        Me.lblDeleted.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDeleted.Visible = False
        '
        'btnSaveBoard
        '
        Me.btnSaveBoard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveBoard.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveBoard.Location = New System.Drawing.Point(234, 498)
        Me.btnSaveBoard.Name = "btnSaveBoard"
        Me.btnSaveBoard.Size = New System.Drawing.Size(100, 35)
        Me.btnSaveBoard.TabIndex = 13
        Me.btnSaveBoard.Text = "Save"
        Me.btnSaveBoard.UseVisualStyleBackColor = True
        Me.btnSaveBoard.Visible = False
        '
        'btnNewBoard
        '
        Me.btnNewBoard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewBoard.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewBoard.Location = New System.Drawing.Point(22, 498)
        Me.btnNewBoard.Name = "btnNewBoard"
        Me.btnNewBoard.Size = New System.Drawing.Size(100, 35)
        Me.btnNewBoard.TabIndex = 10
        Me.btnNewBoard.Text = "New"
        Me.btnNewBoard.UseVisualStyleBackColor = True
        Me.btnNewBoard.Visible = False
        '
        'btnEditBoard
        '
        Me.btnEditBoard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditBoard.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditBoard.Location = New System.Drawing.Point(128, 498)
        Me.btnEditBoard.Name = "btnEditBoard"
        Me.btnEditBoard.Size = New System.Drawing.Size(100, 35)
        Me.btnEditBoard.TabIndex = 11
        Me.btnEditBoard.Text = "Edit"
        Me.btnEditBoard.UseVisualStyleBackColor = True
        Me.btnEditBoard.Visible = False
        '
        'btnDeleteBoard
        '
        Me.btnDeleteBoard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteBoard.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteBoard.Location = New System.Drawing.Point(340, 498)
        Me.btnDeleteBoard.Name = "btnDeleteBoard"
        Me.btnDeleteBoard.Size = New System.Drawing.Size(100, 35)
        Me.btnDeleteBoard.TabIndex = 12
        Me.btnDeleteBoard.Text = "Delete"
        Me.btnDeleteBoard.UseVisualStyleBackColor = True
        '
        'lblLinkSettings
        '
        Me.lblLinkSettings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLinkSettings.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblLinkSettings.Font = New System.Drawing.Font("Segoe UI", 10.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLinkSettings.ForeColor = System.Drawing.Color.Blue
        Me.lblLinkSettings.Location = New System.Drawing.Point(15, 377)
        Me.lblLinkSettings.Name = "lblLinkSettings"
        Me.lblLinkSettings.Size = New System.Drawing.Size(167, 23)
        Me.lblLinkSettings.TabIndex = 25
        Me.lblLinkSettings.Text = "Open Unit Settings"
        Me.lblLinkSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMachineName
        '
        Me.lblMachineName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMachineName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachineName.Location = New System.Drawing.Point(144, 323)
        Me.lblMachineName.Name = "lblMachineName"
        Me.lblMachineName.Size = New System.Drawing.Size(307, 23)
        Me.lblMachineName.TabIndex = 24
        Me.lblMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCreatedAt
        '
        Me.lblCreatedAt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCreatedAt.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreatedAt.Location = New System.Drawing.Point(144, 284)
        Me.lblCreatedAt.Name = "lblCreatedAt"
        Me.lblCreatedAt.Size = New System.Drawing.Size(307, 23)
        Me.lblCreatedAt.TabIndex = 23
        Me.lblCreatedAt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFirmware
        '
        Me.lblFirmware.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFirmware.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFirmware.Location = New System.Drawing.Point(144, 245)
        Me.lblFirmware.Name = "lblFirmware"
        Me.lblFirmware.Size = New System.Drawing.Size(320, 23)
        Me.lblFirmware.TabIndex = 22
        Me.lblFirmware.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSerialNumber
        '
        Me.lblSerialNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSerialNumber.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerialNumber.Location = New System.Drawing.Point(144, 206)
        Me.lblSerialNumber.Name = "lblSerialNumber"
        Me.lblSerialNumber.Size = New System.Drawing.Size(320, 23)
        Me.lblSerialNumber.TabIndex = 21
        Me.lblSerialNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblModel
        '
        Me.lblModel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblModel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModel.Location = New System.Drawing.Point(144, 167)
        Me.lblModel.Name = "lblModel"
        Me.lblModel.Size = New System.Drawing.Size(320, 23)
        Me.lblModel.TabIndex = 20
        Me.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblManufacturer
        '
        Me.lblManufacturer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblManufacturer.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblManufacturer.Location = New System.Drawing.Point(144, 128)
        Me.lblManufacturer.Name = "lblManufacturer"
        Me.lblManufacturer.Size = New System.Drawing.Size(320, 23)
        Me.lblManufacturer.TabIndex = 19
        Me.lblManufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMacAddress
        '
        Me.lblMacAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMacAddress.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMacAddress.Location = New System.Drawing.Point(144, 89)
        Me.lblMacAddress.Name = "lblMacAddress"
        Me.lblMacAddress.Size = New System.Drawing.Size(320, 23)
        Me.lblMacAddress.TabIndex = 18
        Me.lblMacAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIpAddress
        '
        Me.lblIpAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblIpAddress.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIpAddress.Location = New System.Drawing.Point(144, 50)
        Me.lblIpAddress.Name = "lblIpAddress"
        Me.lblIpAddress.Size = New System.Drawing.Size(320, 23)
        Me.lblIpAddress.TabIndex = 17
        Me.lblIpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUnitLabel
        '
        Me.lblUnitLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUnitLabel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnitLabel.Location = New System.Drawing.Point(144, 11)
        Me.lblUnitLabel.Name = "lblUnitLabel"
        Me.lblUnitLabel.Size = New System.Drawing.Size(320, 23)
        Me.lblUnitLabel.TabIndex = 16
        Me.lblUnitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 167)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 23)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Model :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(15, 323)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(123, 23)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Machine Name :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(123, 23)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Unit Label :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(15, 284)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(123, 23)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Created At :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(123, 23)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "IP Address :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(15, 245)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(123, 23)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Firmware :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(15, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 23)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "MAC Address :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(15, 206)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(123, 23)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Serial Number :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(15, 128)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 23)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Manufacturer :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvSensors
        '
        Me.dgvSensors.AllowUserToAddRows = False
        Me.dgvSensors.AllowUserToDeleteRows = False
        Me.dgvSensors.AllowUserToOrderColumns = True
        Me.dgvSensors.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSensors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSensors.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvSensors.ColumnHeadersHeight = 36
        Me.dgvSensors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvSensors.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvSensors.Location = New System.Drawing.Point(10, 42)
        Me.dgvSensors.MultiSelect = False
        Me.dgvSensors.Name = "dgvSensors"
        Me.dgvSensors.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvSensors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSensors.Size = New System.Drawing.Size(817, 173)
        Me.dgvSensors.TabIndex = 14
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(5, 5)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(291, 33)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Sensors"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer3.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.dgvSensors)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label11)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer3.Size = New System.Drawing.Size(836, 547)
        Me.SplitContainer3.SplitterDistance = 218
        Me.SplitContainer3.TabIndex = 15
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel2.Size = New System.Drawing.Size(836, 325)
        Me.Panel2.TabIndex = 0
        '
        'MonitoringUnitsList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1406, 877)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label1)
        Me.Name = "MonitoringUnitsList"
        Me.Text = "Monitoring Units List"
        CType(Me.dgvMonitoringUnits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvSensors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvMonitoringUnits As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents btnClose As PictureBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblMachineName As Label
    Friend WithEvents lblCreatedAt As Label
    Friend WithEvents lblFirmware As Label
    Friend WithEvents lblSerialNumber As Label
    Friend WithEvents lblModel As Label
    Friend WithEvents lblManufacturer As Label
    Friend WithEvents lblMacAddress As Label
    Friend WithEvents lblIpAddress As Label
    Friend WithEvents lblUnitLabel As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Label11 As Label
    Friend WithEvents Id As DataGridViewTextBoxColumn
    Friend WithEvents Label As DataGridViewTextBoxColumn
    Friend WithEvents IpAddress As DataGridViewTextBoxColumn
    Friend WithEvents Mac As DataGridViewTextBoxColumn
    Friend WithEvents Manufacturer As DataGridViewTextBoxColumn
    Friend WithEvents Model As DataGridViewTextBoxColumn
    Friend WithEvents SerialNumber As DataGridViewTextBoxColumn
    Friend WithEvents Firmware As DataGridViewTextBoxColumn
    Friend WithEvents CreatedAt As DataGridViewTextBoxColumn
    Friend WithEvents Target As DataGridViewTextBoxColumn
    Friend WithEvents Enabled As DataGridViewCheckBoxColumn
    Friend WithEvents Machine_Name As DataGridViewTextBoxColumn
    Friend WithEvents lblLinkSettings As Label
    Friend WithEvents dgvSensors As DataGridView
    Friend WithEvents btnSaveBoard As Button
    Friend WithEvents btnNewBoard As Button
    Friend WithEvents btnEditBoard As Button
    Friend WithEvents btnDeleteBoard As Button
    Friend WithEvents chkShowDeleted As CheckBox
    Friend WithEvents lblDeleted As Label
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents Panel2 As Panel
End Class

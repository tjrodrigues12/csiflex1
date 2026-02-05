<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Adv_MTC_cond_edit
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Adv_MTC_cond_edit))
        Me.TV_MTC = New System.Windows.Forms.TreeView()
        Me.TV_context_menu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Addcond = New System.Windows.Forms.ToolStripMenuItem()
        Me.Associate = New System.Windows.Forms.ToolStripMenuItem()
        Me.Partno = New System.Windows.Forms.ToolStripMenuItem()
        Me.Programno = New System.Windows.Forms.ToolStripMenuItem()
        Me.Feedrate = New System.Windows.Forms.ToolStripMenuItem()
        Me.Spindle = New System.Windows.Forms.ToolStripMenuItem()
        Me.Rapid = New System.Windows.Forms.ToolStripMenuItem()
        Me.RequiredPartsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PartCountMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PalletMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgridConditions = New System.Windows.Forms.DataGridView()
        Me.lgc_opr = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.BG_filltree = New System.ComponentModel.BackgroundWorker()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.LBL_RETR = New System.Windows.Forms.Label()
        Me.PB_processing = New System.Windows.Forms.PictureBox()
        Me.TGV_MTC = New AdvancedDataGridView.TreeGridView()
        Me.Device_response = New AdvancedDataGridView.TreeGridColumn()
        Me.Value__ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BTN_Refresh = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TB_tabs = New System.Windows.Forms.TabControl()
        Me.page_status = New System.Windows.Forms.TabPage()
        Me.cmbDelayForCycleOffScale = New System.Windows.Forms.ComboBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.nudDelayForCycleOff = New System.Windows.Forms.NumericUpDown()
        Me.lblStatusDisabled = New System.Windows.Forms.Label()
        Me.btnDisableStatus = New System.Windows.Forms.Button()
        Me.cmbTimeDelay = New System.Windows.Forms.ComboBox()
        Me.chkCsdOnSetup = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.LBL_title = New System.Windows.Forms.Label()
        Me.LBL_man_edit = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.txtExpression = New System.Windows.Forms.TextBox()
        Me.BTN_del = New System.Windows.Forms.Button()
        Me.LBL_delay = New System.Windows.Forms.Label()
        Me.Add = New System.Windows.Forms.Button()
        Me.nudDelay = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.page_notif = New System.Windows.Forms.TabPage()
        Me.dgviewNotifConditions = New System.Windows.Forms.DataGridView()
        Me.DataGridViewComboBoxColumn2 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.sc_nOTIF = New System.Windows.Forms.SplitContainer()
        Me.dgviewNotificationStatus = New System.Windows.Forms.DataGridView()
        Me.DataGridViewComboBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Delay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LBL_NOTIFONSTATUS = New System.Windows.Forms.Label()
        Me.PB_processing_notif = New System.Windows.Forms.PictureBox()
        Me.DGV_COND_notif = New System.Windows.Forms.DataGridView()
        Me.Condition = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Warning = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Fault = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.page_notif_dest = New System.Windows.Forms.TabPage()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.BTN_Config_email = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TB_Cond_TEXT = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DGV_sendto = New System.Windows.Forms.DataGridView()
        Me.Activate = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.sendto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TB_STATUS_TEXT = New System.Windows.Forms.TextBox()
        Me.page_other = New System.Windows.Forms.TabPage()
        Me.chkSaveRawDuringSetup = New System.Windows.Forms.CheckBox()
        Me.chkSaveRawDuringProd = New System.Windows.Forms.CheckBox()
        Me.chkSaveDataraw = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.panelOther = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkOperation = New System.Windows.Forms.CheckBox()
        Me.txtOperationOutput = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.chkFilter3 = New System.Windows.Forms.CheckBox()
        Me.chkFilter2 = New System.Windows.Forms.CheckBox()
        Me.txtPNrPrefix3 = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtPNrPrefix2 = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtPNrPrefix1 = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtPNrFilter3Start = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtPNrFilter3End = New System.Windows.Forms.TextBox()
        Me.txtPNrFilter2Start = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtPNrFilter2End = New System.Windows.Forms.TextBox()
        Me.txtOperationStart = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtOperationEnd = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtInputPartNumber = New System.Windows.Forms.TextBox()
        Me.lblPartNumber = New System.Windows.Forms.Label()
        Me.txtPNrFilter1Start = New System.Windows.Forms.TextBox()
        Me.LBL_prefix = New System.Windows.Forms.Label()
        Me.txtPNrFilter1End = New System.Windows.Forms.TextBox()
        Me.LBL_suffix = New System.Windows.Forms.Label()
        Me.btnClearPartNumber = New System.Windows.Forms.Button()
        Me.txtPNrOutput = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnRefreshPartNumber = New System.Windows.Forms.PictureBox()
        Me.grpOthers = New System.Windows.Forms.GroupBox()
        Me.txtMinFovr = New System.Windows.Forms.TextBox()
        Me.lblFovr = New System.Windows.Forms.Label()
        Me.lblSovr = New System.Windows.Forms.Label()
        Me.btnClearFeedrate = New System.Windows.Forms.Button()
        Me.btnClearSpindle = New System.Windows.Forms.Button()
        Me.lblPartsCount = New System.Windows.Forms.Label()
        Me.lblRovr = New System.Windows.Forms.Label()
        Me.btnClearRapid = New System.Windows.Forms.Button()
        Me.lblRequiredParts = New System.Windows.Forms.Label()
        Me.BTN_FeedrateEdit = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.BTN_SpindleEdit = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.BTN_RapidEdit = New System.Windows.Forms.Button()
        Me.txtMaxRovr = New System.Windows.Forms.TextBox()
        Me.BTN_FeedrateOK = New System.Windows.Forms.Button()
        Me.txtMinRovr = New System.Windows.Forms.TextBox()
        Me.BTN_SpindleOK = New System.Windows.Forms.Button()
        Me.txtMaxSovr = New System.Windows.Forms.TextBox()
        Me.BTN_RapidOK = New System.Windows.Forms.Button()
        Me.txtMinSovr = New System.Windows.Forms.TextBox()
        Me.txtMaxFovr = New System.Windows.Forms.TextBox()
        Me.btnClearRequiredParts = New System.Windows.Forms.Button()
        Me.btnClearPartsCount = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.grpPallet = New System.Windows.Forms.GroupBox()
        Me.chkEnableCriticalStop = New System.Windows.Forms.CheckBox()
        Me.cmbMCSScale = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.numMCSDelay = New System.Windows.Forms.NumericUpDown()
        Me.txtCriticalTemperature = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtCriticalPressure = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtWarningTemperature = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtWarningPressure = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.chkSendPallet = New System.Windows.Forms.CheckBox()
        Me.txtInputPallet = New System.Windows.Forms.TextBox()
        Me.lblActivePallet = New System.Windows.Forms.Label()
        Me.btnClearPallet = New System.Windows.Forms.Button()
        Me.txtStartPallet = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.btnRefreshPallet = New System.Windows.Forms.PictureBox()
        Me.txtEndPallet = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtOutputPallet = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtInputProgrNumber = New System.Windows.Forms.TextBox()
        Me.lblProgramNumber = New System.Windows.Forms.Label()
        Me.btnClearProgramNumber = New System.Windows.Forms.Button()
        Me.txtStartProgNumber = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnRefreshProgNum = New System.Windows.Forms.PictureBox()
        Me.txtEndProgNumber = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtOutputProgrNumber = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LB_current_selected_values = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LV_Property = New System.Windows.Forms.ListView()
        Me.Parameter = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Value = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbl_Result = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BW_refresh_Status = New System.ComponentModel.BackgroundWorker()
        Me.BW_refresh_tree = New System.ComponentModel.BackgroundWorker()
        Me.Timer_threads = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayout_TOP = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_MTC_logo = New System.Windows.Forms.Panel()
        Me.PB_CSIF_LOGO = New System.Windows.Forms.PictureBox()
        Me.Panel_Status = New System.Windows.Forms.Panel()
        Me.PB_status = New System.Windows.Forms.PictureBox()
        Me.Panel_CSIF_logo = New System.Windows.Forms.Panel()
        Me.lblMonitoringUnit = New System.Windows.Forms.Label()
        Me.pbFocas = New System.Windows.Forms.PictureBox()
        Me.LB_MachineAddress = New System.Windows.Forms.Label()
        Me.PB_MTC_LOGO = New System.Windows.Forms.PictureBox()
        Me.BW_find_cond = New System.ComponentModel.BackgroundWorker()
        Me.chkSaveCOnDuringSetup = New System.Windows.Forms.CheckBox()
        Me.TV_context_menu.SuspendLayout()
        CType(Me.dgridConditions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.PB_processing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TGV_MTC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TB_tabs.SuspendLayout()
        Me.page_status.SuspendLayout()
        CType(Me.nudDelayForCycleOff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudDelay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.page_notif.SuspendLayout()
        CType(Me.dgviewNotifConditions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_nOTIF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sc_nOTIF.Panel1.SuspendLayout()
        Me.sc_nOTIF.Panel2.SuspendLayout()
        Me.sc_nOTIF.SuspendLayout()
        CType(Me.dgviewNotificationStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_processing_notif, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_COND_notif, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.page_notif_dest.SuspendLayout()
        CType(Me.DGV_sendto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.page_other.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.panelOther.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.btnRefreshPartNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOthers.SuspendLayout()
        Me.grpPallet.SuspendLayout()
        CType(Me.numMCSDelay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefreshPallet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.btnRefreshProgNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayout_TOP.SuspendLayout()
        Me.Panel_MTC_logo.SuspendLayout()
        CType(Me.PB_CSIF_LOGO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel_Status.SuspendLayout()
        CType(Me.PB_status, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel_CSIF_logo.SuspendLayout()
        CType(Me.pbFocas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_MTC_LOGO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TV_MTC
        '
        Me.TV_MTC.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TV_MTC.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TV_MTC.ContextMenuStrip = Me.TV_context_menu
        Me.TV_MTC.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TV_MTC.FullRowSelect = True
        Me.TV_MTC.HotTracking = True
        Me.TV_MTC.Location = New System.Drawing.Point(4, 79)
        Me.TV_MTC.Margin = New System.Windows.Forms.Padding(4)
        Me.TV_MTC.Name = "TV_MTC"
        Me.TV_MTC.ShowLines = False
        Me.TV_MTC.ShowRootLines = False
        Me.TV_MTC.Size = New System.Drawing.Size(717, 786)
        Me.TV_MTC.TabIndex = 13
        '
        'TV_context_menu
        '
        Me.TV_context_menu.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.TV_context_menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Addcond, Me.Associate})
        Me.TV_context_menu.Name = "TV_context_menu"
        Me.TV_context_menu.Size = New System.Drawing.Size(153, 48)
        '
        'Addcond
        '
        Me.Addcond.Name = "Addcond"
        Me.Addcond.Size = New System.Drawing.Size(152, 22)
        Me.Addcond.Text = "Add Condition"
        '
        'Associate
        '
        Me.Associate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Partno, Me.Programno, Me.Feedrate, Me.Spindle, Me.Rapid, Me.RequiredPartsMenuItem, Me.PartCountMenuItem, Me.PalletMenuItem})
        Me.Associate.Name = "Associate"
        Me.Associate.Size = New System.Drawing.Size(152, 22)
        Me.Associate.Text = "Associate with"
        '
        'Partno
        '
        Me.Partno.Name = "Partno"
        Me.Partno.Size = New System.Drawing.Size(167, 22)
        Me.Partno.Text = "Part Number"
        '
        'Programno
        '
        Me.Programno.Name = "Programno"
        Me.Programno.Size = New System.Drawing.Size(167, 22)
        Me.Programno.Text = "Program Number"
        '
        'Feedrate
        '
        Me.Feedrate.Name = "Feedrate"
        Me.Feedrate.Size = New System.Drawing.Size(167, 22)
        Me.Feedrate.Text = "Feedrate Override"
        '
        'Spindle
        '
        Me.Spindle.Name = "Spindle"
        Me.Spindle.Size = New System.Drawing.Size(167, 22)
        Me.Spindle.Text = "Spindle Override"
        '
        'Rapid
        '
        Me.Rapid.Name = "Rapid"
        Me.Rapid.Size = New System.Drawing.Size(167, 22)
        Me.Rapid.Text = "Rapid Override"
        '
        'RequiredPartsMenuItem
        '
        Me.RequiredPartsMenuItem.Name = "RequiredPartsMenuItem"
        Me.RequiredPartsMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.RequiredPartsMenuItem.Text = "Required parts"
        '
        'PartCountMenuItem
        '
        Me.PartCountMenuItem.Name = "PartCountMenuItem"
        Me.PartCountMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.PartCountMenuItem.Text = "Part Count"
        '
        'PalletMenuItem
        '
        Me.PalletMenuItem.Name = "PalletMenuItem"
        Me.PalletMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.PalletMenuItem.Text = "Pallet"
        '
        'dgridConditions
        '
        Me.dgridConditions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgridConditions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgridConditions.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgridConditions.BackgroundColor = System.Drawing.Color.White
        Me.dgridConditions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgridConditions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dgridConditions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgridConditions.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgridConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgridConditions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.lgc_opr})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgridConditions.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgridConditions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.dgridConditions.Location = New System.Drawing.Point(5, 75)
        Me.dgridConditions.Margin = New System.Windows.Forms.Padding(5)
        Me.dgridConditions.MultiSelect = False
        Me.dgridConditions.Name = "dgridConditions"
        Me.dgridConditions.RowHeadersWidth = 40
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        Me.dgridConditions.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgridConditions.RowTemplate.Height = 24
        Me.dgridConditions.Size = New System.Drawing.Size(653, 382)
        Me.dgridConditions.TabIndex = 14
        '
        'lgc_opr
        '
        Me.lgc_opr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.lgc_opr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lgc_opr.HeaderText = "Condition"
        Me.lgc_opr.Items.AddRange(New Object() {"AND", "OR"})
        Me.lgc_opr.Name = "lgc_opr"
        Me.lgc_opr.Width = 65
        '
        'BG_filltree
        '
        Me.BG_filltree.WorkerReportsProgress = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 148)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel1.Controls.Add(Me.LBL_RETR)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PB_processing)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TGV_MTC)
        Me.SplitContainer1.Panel1.Controls.Add(Me.BTN_Refresh)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TV_MTC)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1484, 869)
        Me.SplitContainer1.SplitterDistance = 725
        Me.SplitContainer1.SplitterWidth = 10
        Me.SplitContainer1.TabIndex = 15
        '
        'LBL_RETR
        '
        Me.LBL_RETR.AutoSize = True
        Me.LBL_RETR.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LBL_RETR.Location = New System.Drawing.Point(62, 190)
        Me.LBL_RETR.Name = "LBL_RETR"
        Me.LBL_RETR.Size = New System.Drawing.Size(187, 15)
        Me.LBL_RETR.TabIndex = 41
        Me.LBL_RETR.Text = "Retrieving data from the machine "
        '
        'PB_processing
        '
        Me.PB_processing.Image = Global.CSI_Reporting_Application.My.Resources.Resources.processing
        Me.PB_processing.Location = New System.Drawing.Point(112, 79)
        Me.PB_processing.Name = "PB_processing"
        Me.PB_processing.Size = New System.Drawing.Size(121, 115)
        Me.PB_processing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_processing.TabIndex = 40
        Me.PB_processing.TabStop = False
        '
        'TGV_MTC
        '
        Me.TGV_MTC.AllowUserToAddRows = False
        Me.TGV_MTC.AllowUserToDeleteRows = False
        Me.TGV_MTC.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TGV_MTC.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.TGV_MTC.BackgroundColor = System.Drawing.Color.White
        Me.TGV_MTC.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TGV_MTC.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TGV_MTC.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.TGV_MTC.ColumnHeadersHeight = 30
        Me.TGV_MTC.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Device_response, Me.Value__})
        Me.TGV_MTC.ContextMenuStrip = Me.TV_context_menu
        Me.TGV_MTC.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.TGV_MTC.GridColor = System.Drawing.Color.LightGray
        Me.TGV_MTC.ImageList = Nothing
        Me.TGV_MTC.Location = New System.Drawing.Point(5, 3)
        Me.TGV_MTC.MultiSelect = False
        Me.TGV_MTC.Name = "TGV_MTC"
        Me.TGV_MTC.ReadOnly = True
        Me.TGV_MTC.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.TGV_MTC.RowHeadersVisible = False
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TGV_MTC.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.TGV_MTC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.TGV_MTC.ShowEditingIcon = False
        Me.TGV_MTC.ShowLines = False
        Me.TGV_MTC.Size = New System.Drawing.Size(717, 850)
        Me.TGV_MTC.TabIndex = 39
        '
        'Device_response
        '
        Me.Device_response.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Device_response.DefaultCellStyle = DataGridViewCellStyle5
        Me.Device_response.DefaultNodeImage = Nothing
        Me.Device_response.HeaderText = "Device response"
        Me.Device_response.Name = "Device_response"
        Me.Device_response.ReadOnly = True
        Me.Device_response.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Value__
        '
        Me.Value__.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Value__.DefaultCellStyle = DataGridViewCellStyle6
        Me.Value__.HeaderText = "Value"
        Me.Value__.Name = "Value__"
        Me.Value__.ReadOnly = True
        Me.Value__.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'BTN_Refresh
        '
        Me.BTN_Refresh.FlatAppearance.BorderSize = 0
        Me.BTN_Refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue
        Me.BTN_Refresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Refresh.Location = New System.Drawing.Point(190, 20)
        Me.BTN_Refresh.Name = "BTN_Refresh"
        Me.BTN_Refresh.Size = New System.Drawing.Size(63, 27)
        Me.BTN_Refresh.TabIndex = 38
        Me.BTN_Refresh.Text = "refresh"
        Me.BTN_Refresh.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 19)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Device response :"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.Location = New System.Drawing.Point(2, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.SplitContainer2.Panel1.Controls.Add(Me.TB_tabs)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.AutoScroll = True
        Me.SplitContainer2.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer2.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.SplitContainer2.Size = New System.Drawing.Size(680, 861)
        Me.SplitContainer2.SplitterDistance = 638
        Me.SplitContainer2.SplitterWidth = 10
        Me.SplitContainer2.TabIndex = 62
        '
        'TB_tabs
        '
        Me.TB_tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_tabs.Controls.Add(Me.page_status)
        Me.TB_tabs.Controls.Add(Me.page_notif)
        Me.TB_tabs.Controls.Add(Me.page_notif_dest)
        Me.TB_tabs.Controls.Add(Me.page_other)
        Me.TB_tabs.HotTrack = True
        Me.TB_tabs.Location = New System.Drawing.Point(0, 0)
        Me.TB_tabs.Name = "TB_tabs"
        Me.TB_tabs.SelectedIndex = 0
        Me.TB_tabs.Size = New System.Drawing.Size(674, 635)
        Me.TB_tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TB_tabs.TabIndex = 63
        '
        'page_status
        '
        Me.page_status.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.page_status.Controls.Add(Me.chkSaveCOnDuringSetup)
        Me.page_status.Controls.Add(Me.cmbDelayForCycleOffScale)
        Me.page_status.Controls.Add(Me.Label42)
        Me.page_status.Controls.Add(Me.nudDelayForCycleOff)
        Me.page_status.Controls.Add(Me.lblStatusDisabled)
        Me.page_status.Controls.Add(Me.btnDisableStatus)
        Me.page_status.Controls.Add(Me.cmbTimeDelay)
        Me.page_status.Controls.Add(Me.chkCsdOnSetup)
        Me.page_status.Controls.Add(Me.btnSave)
        Me.page_status.Controls.Add(Me.LBL_title)
        Me.page_status.Controls.Add(Me.LBL_man_edit)
        Me.page_status.Controls.Add(Me.cmbStatus)
        Me.page_status.Controls.Add(Me.txtExpression)
        Me.page_status.Controls.Add(Me.BTN_del)
        Me.page_status.Controls.Add(Me.LBL_delay)
        Me.page_status.Controls.Add(Me.Add)
        Me.page_status.Controls.Add(Me.nudDelay)
        Me.page_status.Controls.Add(Me.dgridConditions)
        Me.page_status.Controls.Add(Me.Label5)
        Me.page_status.Location = New System.Drawing.Point(4, 21)
        Me.page_status.Name = "page_status"
        Me.page_status.Padding = New System.Windows.Forms.Padding(3)
        Me.page_status.Size = New System.Drawing.Size(666, 610)
        Me.page_status.TabIndex = 0
        Me.page_status.Text = "Status"
        '
        'cmbDelayForCycleOffScale
        '
        Me.cmbDelayForCycleOffScale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbDelayForCycleOffScale.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbDelayForCycleOffScale.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDelayForCycleOffScale.FormattingEnabled = True
        Me.cmbDelayForCycleOffScale.Items.AddRange(New Object() {"Sec", "Min"})
        Me.cmbDelayForCycleOffScale.Location = New System.Drawing.Point(447, 537)
        Me.cmbDelayForCycleOffScale.Name = "cmbDelayForCycleOffScale"
        Me.cmbDelayForCycleOffScale.Size = New System.Drawing.Size(60, 28)
        Me.cmbDelayForCycleOffScale.TabIndex = 65
        '
        'Label42
        '
        Me.Label42.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label42.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(225, 541)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(150, 19)
        Me.Label42.TabIndex = 63
        Me.Label42.Text = "Cycle OFF Delay :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'nudDelayForCycleOff
        '
        Me.nudDelayForCycleOff.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.nudDelayForCycleOff.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.nudDelayForCycleOff.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudDelayForCycleOff.Location = New System.Drawing.Point(381, 541)
        Me.nudDelayForCycleOff.Maximum = New Decimal(New Integer() {-727379968, 232, 0, 0})
        Me.nudDelayForCycleOff.Name = "nudDelayForCycleOff"
        Me.nudDelayForCycleOff.Size = New System.Drawing.Size(60, 23)
        Me.nudDelayForCycleOff.TabIndex = 64
        Me.nudDelayForCycleOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblStatusDisabled
        '
        Me.lblStatusDisabled.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusDisabled.ForeColor = System.Drawing.Color.Red
        Me.lblStatusDisabled.Location = New System.Drawing.Point(178, 41)
        Me.lblStatusDisabled.Name = "lblStatusDisabled"
        Me.lblStatusDisabled.Size = New System.Drawing.Size(339, 27)
        Me.lblStatusDisabled.TabIndex = 62
        Me.lblStatusDisabled.Text = "Status Disabled"
        Me.lblStatusDisabled.Visible = False
        '
        'btnDisableStatus
        '
        Me.btnDisableStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDisableStatus.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDisableStatus.Location = New System.Drawing.Point(242, 579)
        Me.btnDisableStatus.Name = "btnDisableStatus"
        Me.btnDisableStatus.Size = New System.Drawing.Size(106, 25)
        Me.btnDisableStatus.TabIndex = 61
        Me.btnDisableStatus.Text = "Enable Status"
        Me.btnDisableStatus.UseVisualStyleBackColor = True
        Me.btnDisableStatus.Visible = False
        '
        'cmbTimeDelay
        '
        Me.cmbTimeDelay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbTimeDelay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbTimeDelay.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTimeDelay.FormattingEnabled = True
        Me.cmbTimeDelay.Items.AddRange(New Object() {"Sec", "Min"})
        Me.cmbTimeDelay.Location = New System.Drawing.Point(159, 537)
        Me.cmbTimeDelay.Name = "cmbTimeDelay"
        Me.cmbTimeDelay.Size = New System.Drawing.Size(60, 28)
        Me.cmbTimeDelay.TabIndex = 60
        '
        'chkCsdOnSetup
        '
        Me.chkCsdOnSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkCsdOnSetup.AutoSize = True
        Me.chkCsdOnSetup.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCsdOnSetup.Location = New System.Drawing.Point(93, 581)
        Me.chkCsdOnSetup.Name = "chkCsdOnSetup"
        Me.chkCsdOnSetup.Size = New System.Drawing.Size(143, 23)
        Me.chkCsdOnSetup.TabIndex = 58
        Me.chkCsdOnSetup.Text = "CSD During SETUP"
        Me.chkCsdOnSetup.UseVisualStyleBackColor = True
        Me.chkCsdOnSetup.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(605, 11)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(53, 31)
        Me.btnSave.TabIndex = 57
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'LBL_title
        '
        Me.LBL_title.AutoSize = True
        Me.LBL_title.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_title.Location = New System.Drawing.Point(24, 14)
        Me.LBL_title.Name = "LBL_title"
        Me.LBL_title.Size = New System.Drawing.Size(131, 19)
        Me.LBL_title.TabIndex = 36
        Me.LBL_title.Text = "Specify Criteria for :"
        '
        'LBL_man_edit
        '
        Me.LBL_man_edit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LBL_man_edit.AutoSize = True
        Me.LBL_man_edit.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_man_edit.Location = New System.Drawing.Point(7, 466)
        Me.LBL_man_edit.Name = "LBL_man_edit"
        Me.LBL_man_edit.Size = New System.Drawing.Size(80, 19)
        Me.LBL_man_edit.TabIndex = 16
        Me.LBL_man_edit.Text = "Expression :"
        '
        'cmbStatus
        '
        Me.cmbStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbStatus.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(182, 11)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(299, 27)
        Me.cmbStatus.TabIndex = 37
        '
        'txtExpression
        '
        Me.txtExpression.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtExpression.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtExpression.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtExpression.Location = New System.Drawing.Point(93, 465)
        Me.txtExpression.Multiline = True
        Me.txtExpression.Name = "txtExpression"
        Me.txtExpression.Size = New System.Drawing.Size(565, 62)
        Me.txtExpression.TabIndex = 15
        '
        'BTN_del
        '
        Me.BTN_del.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTN_del.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_del.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_del.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight
        Me.BTN_del.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_del.Location = New System.Drawing.Point(546, 11)
        Me.BTN_del.Name = "BTN_del"
        Me.BTN_del.Size = New System.Drawing.Size(53, 31)
        Me.BTN_del.TabIndex = 56
        Me.BTN_del.Text = "Del"
        Me.BTN_del.UseVisualStyleBackColor = False
        '
        'LBL_delay
        '
        Me.LBL_delay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LBL_delay.AutoSize = True
        Me.LBL_delay.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_delay.Location = New System.Drawing.Point(33, 540)
        Me.LBL_delay.Name = "LBL_delay"
        Me.LBL_delay.Size = New System.Drawing.Size(54, 19)
        Me.LBL_delay.TabIndex = 48
        Me.LBL_delay.Text = "Delay  :"
        '
        'Add
        '
        Me.Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Add.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Add.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight
        Me.Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Add.Location = New System.Drawing.Point(487, 11)
        Me.Add.Name = "Add"
        Me.Add.Size = New System.Drawing.Size(53, 31)
        Me.Add.TabIndex = 45
        Me.Add.Text = "Add"
        Me.Add.UseVisualStyleBackColor = False
        '
        'nudDelay
        '
        Me.nudDelay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.nudDelay.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.nudDelay.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudDelay.Location = New System.Drawing.Point(93, 540)
        Me.nudDelay.Maximum = New Decimal(New Integer() {-727379968, 232, 0, 0})
        Me.nudDelay.Name = "nudDelay"
        Me.nudDelay.Size = New System.Drawing.Size(60, 23)
        Me.nudDelay.TabIndex = 49
        Me.nudDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(42, 408)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 15)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "Seconds"
        '
        'page_notif
        '
        Me.page_notif.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.page_notif.Controls.Add(Me.dgviewNotifConditions)
        Me.page_notif.Controls.Add(Me.sc_nOTIF)
        Me.page_notif.Location = New System.Drawing.Point(4, 22)
        Me.page_notif.Name = "page_notif"
        Me.page_notif.Padding = New System.Windows.Forms.Padding(3)
        Me.page_notif.Size = New System.Drawing.Size(672, 609)
        Me.page_notif.TabIndex = 1
        Me.page_notif.Text = "Notifications"
        '
        'dgviewNotifConditions
        '
        Me.dgviewNotifConditions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgviewNotifConditions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgviewNotifConditions.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgviewNotifConditions.BackgroundColor = System.Drawing.Color.White
        Me.dgviewNotifConditions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgviewNotifConditions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dgviewNotifConditions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.Gray
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgviewNotifConditions.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgviewNotifConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgviewNotifConditions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewComboBoxColumn2})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgviewNotifConditions.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgviewNotifConditions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.dgviewNotifConditions.Location = New System.Drawing.Point(7, 239)
        Me.dgviewNotifConditions.Margin = New System.Windows.Forms.Padding(5)
        Me.dgviewNotifConditions.MultiSelect = False
        Me.dgviewNotifConditions.Name = "dgviewNotifConditions"
        Me.dgviewNotifConditions.RowHeadersWidth = 40
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.White
        Me.dgviewNotifConditions.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.dgviewNotifConditions.RowTemplate.Height = 24
        Me.dgviewNotifConditions.Size = New System.Drawing.Size(659, 130)
        Me.dgviewNotifConditions.TabIndex = 15
        Me.dgviewNotifConditions.Visible = False
        '
        'DataGridViewComboBoxColumn2
        '
        Me.DataGridViewComboBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DataGridViewComboBoxColumn2.HeaderText = "Condition"
        Me.DataGridViewComboBoxColumn2.Items.AddRange(New Object() {"AND", "OR"})
        Me.DataGridViewComboBoxColumn2.Name = "DataGridViewComboBoxColumn2"
        Me.DataGridViewComboBoxColumn2.Width = 65
        '
        'sc_nOTIF
        '
        Me.sc_nOTIF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sc_nOTIF.Location = New System.Drawing.Point(3, 3)
        Me.sc_nOTIF.Name = "sc_nOTIF"
        '
        'sc_nOTIF.Panel1
        '
        Me.sc_nOTIF.Panel1.Controls.Add(Me.dgviewNotificationStatus)
        Me.sc_nOTIF.Panel1.Controls.Add(Me.LBL_NOTIFONSTATUS)
        '
        'sc_nOTIF.Panel2
        '
        Me.sc_nOTIF.Panel2.Controls.Add(Me.PB_processing_notif)
        Me.sc_nOTIF.Panel2.Controls.Add(Me.DGV_COND_notif)
        Me.sc_nOTIF.Panel2.Controls.Add(Me.Label7)
        Me.sc_nOTIF.Size = New System.Drawing.Size(666, 203)
        Me.sc_nOTIF.SplitterDistance = 287
        Me.sc_nOTIF.SplitterWidth = 7
        Me.sc_nOTIF.TabIndex = 0
        '
        'dgviewNotificationStatus
        '
        Me.dgviewNotificationStatus.AllowUserToAddRows = False
        Me.dgviewNotificationStatus.AllowUserToDeleteRows = False
        Me.dgviewNotificationStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgviewNotificationStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgviewNotificationStatus.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgviewNotificationStatus.BackgroundColor = System.Drawing.Color.White
        Me.dgviewNotificationStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgviewNotificationStatus.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dgviewNotificationStatus.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgviewNotificationStatus.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgviewNotificationStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgviewNotificationStatus.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewComboBoxColumn1, Me.Status, Me.Delay})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgviewNotificationStatus.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgviewNotificationStatus.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.dgviewNotificationStatus.Location = New System.Drawing.Point(5, 37)
        Me.dgviewNotificationStatus.Margin = New System.Windows.Forms.Padding(5)
        Me.dgviewNotificationStatus.MultiSelect = False
        Me.dgviewNotificationStatus.Name = "dgviewNotificationStatus"
        Me.dgviewNotificationStatus.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgviewNotificationStatus.RowHeadersVisible = False
        Me.dgviewNotificationStatus.RowHeadersWidth = 40
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgviewNotificationStatus.RowsDefaultCellStyle = DataGridViewCellStyle13
        Me.dgviewNotificationStatus.RowTemplate.Height = 24
        Me.dgviewNotificationStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgviewNotificationStatus.Size = New System.Drawing.Size(277, 161)
        Me.dgviewNotificationStatus.TabIndex = 16
        '
        'DataGridViewComboBoxColumn1
        '
        Me.DataGridViewComboBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn1.HeaderText = "Activate"
        Me.DataGridViewComboBoxColumn1.Name = "DataGridViewComboBoxColumn1"
        Me.DataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn1.Width = 53
        '
        'Status
        '
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        '
        'Delay
        '
        Me.Delay.HeaderText = "Delay"
        Me.Delay.Name = "Delay"
        '
        'LBL_NOTIFONSTATUS
        '
        Me.LBL_NOTIFONSTATUS.AutoSize = True
        Me.LBL_NOTIFONSTATUS.Location = New System.Drawing.Point(20, 9)
        Me.LBL_NOTIFONSTATUS.Name = "LBL_NOTIFONSTATUS"
        Me.LBL_NOTIFONSTATUS.Size = New System.Drawing.Size(165, 13)
        Me.LBL_NOTIFONSTATUS.TabIndex = 5
        Me.LBL_NOTIFONSTATUS.Text = "Notification on defined status:"
        '
        'PB_processing_notif
        '
        Me.PB_processing_notif.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_processing_notif.Image = Global.CSI_Reporting_Application.My.Resources.Resources.CON_
        Me.PB_processing_notif.Location = New System.Drawing.Point(79, 0)
        Me.PB_processing_notif.Name = "PB_processing_notif"
        Me.PB_processing_notif.Size = New System.Drawing.Size(35, 34)
        Me.PB_processing_notif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_processing_notif.TabIndex = 43
        Me.PB_processing_notif.TabStop = False
        Me.PB_processing_notif.Tag = "Retrieving data from the machine "
        Me.PB_processing_notif.UseWaitCursor = True
        '
        'DGV_COND_notif
        '
        Me.DGV_COND_notif.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_COND_notif.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_COND_notif.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DGV_COND_notif.BackgroundColor = System.Drawing.Color.White
        Me.DGV_COND_notif.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DGV_COND_notif.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.DGV_COND_notif.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_COND_notif.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.DGV_COND_notif.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_COND_notif.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Condition, Me.Warning, Me.Fault, Me.DataGridViewTextBoxColumn1})
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV_COND_notif.DefaultCellStyle = DataGridViewCellStyle15
        Me.DGV_COND_notif.Location = New System.Drawing.Point(3, 37)
        Me.DGV_COND_notif.Margin = New System.Windows.Forms.Padding(5)
        Me.DGV_COND_notif.MultiSelect = False
        Me.DGV_COND_notif.Name = "DGV_COND_notif"
        Me.DGV_COND_notif.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGV_COND_notif.RowHeadersVisible = False
        Me.DGV_COND_notif.RowHeadersWidth = 40
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.White
        Me.DGV_COND_notif.RowsDefaultCellStyle = DataGridViewCellStyle16
        Me.DGV_COND_notif.RowTemplate.Height = 24
        Me.DGV_COND_notif.Size = New System.Drawing.Size(310, 161)
        Me.DGV_COND_notif.TabIndex = 16
        '
        'Condition
        '
        Me.Condition.HeaderText = "Condition"
        Me.Condition.Name = "Condition"
        '
        'Warning
        '
        Me.Warning.HeaderText = "Warning"
        Me.Warning.Name = "Warning"
        '
        'Fault
        '
        Me.Fault.HeaderText = "Fault (Alarm)"
        Me.Fault.Name = "Fault"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Delay"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(188, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Notification conditions and alarms:"
        '
        'page_notif_dest
        '
        Me.page_notif_dest.Controls.Add(Me.TextBox4)
        Me.page_notif_dest.Controls.Add(Me.TextBox3)
        Me.page_notif_dest.Controls.Add(Me.TextBox2)
        Me.page_notif_dest.Controls.Add(Me.TextBox1)
        Me.page_notif_dest.Controls.Add(Me.BTN_Config_email)
        Me.page_notif_dest.Controls.Add(Me.Label15)
        Me.page_notif_dest.Controls.Add(Me.TB_Cond_TEXT)
        Me.page_notif_dest.Controls.Add(Me.Label10)
        Me.page_notif_dest.Controls.Add(Me.Label9)
        Me.page_notif_dest.Controls.Add(Me.Label8)
        Me.page_notif_dest.Controls.Add(Me.DGV_sendto)
        Me.page_notif_dest.Controls.Add(Me.TB_STATUS_TEXT)
        Me.page_notif_dest.Location = New System.Drawing.Point(4, 22)
        Me.page_notif_dest.Name = "page_notif_dest"
        Me.page_notif_dest.Padding = New System.Windows.Forms.Padding(3)
        Me.page_notif_dest.Size = New System.Drawing.Size(672, 609)
        Me.page_notif_dest.TabIndex = 3
        Me.page_notif_dest.Text = "Notification setup"
        Me.page_notif_dest.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox4.BackColor = System.Drawing.Color.White
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox4.Location = New System.Drawing.Point(406, 381)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(100, 14)
        Me.TextBox4.TabIndex = 15
        Me.TextBox4.Text = "[TimeEvent]"
        '
        'TextBox3
        '
        Me.TextBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox3.BackColor = System.Drawing.Color.White
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.Location = New System.Drawing.Point(406, 359)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(100, 14)
        Me.TextBox3.TabIndex = 14
        Me.TextBox3.Text = "[StatusName]"
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Location = New System.Drawing.Point(258, 427)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(100, 14)
        Me.TextBox2.TabIndex = 13
        Me.TextBox2.Text = "[ConditionName]"
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Location = New System.Drawing.Point(258, 405)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(100, 14)
        Me.TextBox1.TabIndex = 12
        Me.TextBox1.Text = "[MachineName]"
        '
        'BTN_Config_email
        '
        Me.BTN_Config_email.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_Config_email.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Config_email.Location = New System.Drawing.Point(6, 412)
        Me.BTN_Config_email.Name = "BTN_Config_email"
        Me.BTN_Config_email.Size = New System.Drawing.Size(242, 30)
        Me.BTN_Config_email.TabIndex = 11
        Me.BTN_Config_email.Text = "Configure email server"
        Me.BTN_Config_email.UseVisualStyleBackColor = True
        Me.BTN_Config_email.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(256, 146)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(214, 13)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "Notification text for a condition change:"
        '
        'TB_Cond_TEXT
        '
        Me.TB_Cond_TEXT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_Cond_TEXT.Location = New System.Drawing.Point(254, 173)
        Me.TB_Cond_TEXT.Multiline = True
        Me.TB_Cond_TEXT.Name = "TB_Cond_TEXT"
        Me.TB_Cond_TEXT.Size = New System.Drawing.Size(339, 79)
        Me.TB_Cond_TEXT.TabIndex = 9
        Me.TB_Cond_TEXT.Text = "Condition alert for machine [MachineName] : [ConditionName], since [AwaitedDelay]" &
    " ."
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(273, 329)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 13)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Key words :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(256, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(195, 13)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Notification text for a status change:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Send to :"
        '
        'DGV_sendto
        '
        Me.DGV_sendto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DGV_sendto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_sendto.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DGV_sendto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_sendto.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Activate, Me.sendto})
        Me.DGV_sendto.Location = New System.Drawing.Point(6, 50)
        Me.DGV_sendto.Name = "DGV_sendto"
        Me.DGV_sendto.RowHeadersVisible = False
        Me.DGV_sendto.RowTemplate.Height = 24
        Me.DGV_sendto.Size = New System.Drawing.Size(242, 338)
        Me.DGV_sendto.TabIndex = 1
        '
        'Activate
        '
        Me.Activate.FillWeight = 25.38071!
        Me.Activate.HeaderText = ""
        Me.Activate.Name = "Activate"
        '
        'sendto
        '
        Me.sendto.FillWeight = 174.6193!
        Me.sendto.HeaderText = "Send to"
        Me.sendto.Name = "sendto"
        '
        'TB_STATUS_TEXT
        '
        Me.TB_STATUS_TEXT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_STATUS_TEXT.Location = New System.Drawing.Point(254, 50)
        Me.TB_STATUS_TEXT.Multiline = True
        Me.TB_STATUS_TEXT.Name = "TB_STATUS_TEXT"
        Me.TB_STATUS_TEXT.Size = New System.Drawing.Size(333, 79)
        Me.TB_STATUS_TEXT.TabIndex = 0
        Me.TB_STATUS_TEXT.Text = "Current status for machine [MachineName] : [StatusName], since [AwaitedDelay] ."
        '
        'page_other
        '
        Me.page_other.AutoScroll = True
        Me.page_other.BackColor = System.Drawing.Color.White
        Me.page_other.Controls.Add(Me.chkSaveRawDuringSetup)
        Me.page_other.Controls.Add(Me.chkSaveRawDuringProd)
        Me.page_other.Controls.Add(Me.chkSaveDataraw)
        Me.page_other.Controls.Add(Me.GroupBox3)
        Me.page_other.Location = New System.Drawing.Point(4, 22)
        Me.page_other.Name = "page_other"
        Me.page_other.Size = New System.Drawing.Size(672, 609)
        Me.page_other.TabIndex = 2
        Me.page_other.Text = "Other"
        '
        'chkSaveRawDuringSetup
        '
        Me.chkSaveRawDuringSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSaveRawDuringSetup.AutoSize = True
        Me.chkSaveRawDuringSetup.Location = New System.Drawing.Point(277, 577)
        Me.chkSaveRawDuringSetup.Name = "chkSaveRawDuringSetup"
        Me.chkSaveRawDuringSetup.Size = New System.Drawing.Size(99, 17)
        Me.chkSaveRawDuringSetup.TabIndex = 118
        Me.chkSaveRawDuringSetup.Text = "Include SETUP"
        Me.chkSaveRawDuringSetup.UseVisualStyleBackColor = True
        Me.chkSaveRawDuringSetup.Visible = False
        '
        'chkSaveRawDuringProd
        '
        Me.chkSaveRawDuringProd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSaveRawDuringProd.AutoSize = True
        Me.chkSaveRawDuringProd.Location = New System.Drawing.Point(138, 577)
        Me.chkSaveRawDuringProd.Name = "chkSaveRawDuringProd"
        Me.chkSaveRawDuringProd.Size = New System.Drawing.Size(125, 17)
        Me.chkSaveRawDuringProd.TabIndex = 117
        Me.chkSaveRawDuringProd.Text = "During PROD. Only"
        Me.chkSaveRawDuringProd.UseVisualStyleBackColor = True
        Me.chkSaveRawDuringProd.Visible = False
        '
        'chkSaveDataraw
        '
        Me.chkSaveDataraw.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSaveDataraw.AutoSize = True
        Me.chkSaveDataraw.Location = New System.Drawing.Point(23, 577)
        Me.chkSaveDataraw.Name = "chkSaveDataraw"
        Me.chkSaveDataraw.Size = New System.Drawing.Size(101, 17)
        Me.chkSaveDataraw.TabIndex = 116
        Me.chkSaveDataraw.Text = "Save Raw Data"
        Me.chkSaveDataraw.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox3.Controls.Add(Me.Panel3)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(664, 562)
        Me.GroupBox3.TabIndex = 115
        Me.GroupBox3.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.AutoScroll = True
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.panelOther)
        Me.Panel3.Location = New System.Drawing.Point(6, 11)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel3.Size = New System.Drawing.Size(652, 542)
        Me.Panel3.TabIndex = 114
        '
        'panelOther
        '
        Me.panelOther.Controls.Add(Me.GroupBox2)
        Me.panelOther.Controls.Add(Me.grpOthers)
        Me.panelOther.Controls.Add(Me.Label6)
        Me.panelOther.Controls.Add(Me.grpPallet)
        Me.panelOther.Controls.Add(Me.GroupBox1)
        Me.panelOther.Location = New System.Drawing.Point(3, 3)
        Me.panelOther.Name = "panelOther"
        Me.panelOther.Size = New System.Drawing.Size(664, 1000)
        Me.panelOther.TabIndex = 113
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkOperation)
        Me.GroupBox2.Controls.Add(Me.txtOperationOutput)
        Me.GroupBox2.Controls.Add(Me.Label43)
        Me.GroupBox2.Controls.Add(Me.chkFilter3)
        Me.GroupBox2.Controls.Add(Me.chkFilter2)
        Me.GroupBox2.Controls.Add(Me.txtPNrPrefix3)
        Me.GroupBox2.Controls.Add(Me.Label41)
        Me.GroupBox2.Controls.Add(Me.txtPNrPrefix2)
        Me.GroupBox2.Controls.Add(Me.Label40)
        Me.GroupBox2.Controls.Add(Me.Label38)
        Me.GroupBox2.Controls.Add(Me.Label36)
        Me.GroupBox2.Controls.Add(Me.txtPNrPrefix1)
        Me.GroupBox2.Controls.Add(Me.Label39)
        Me.GroupBox2.Controls.Add(Me.txtPNrFilter3Start)
        Me.GroupBox2.Controls.Add(Me.Label37)
        Me.GroupBox2.Controls.Add(Me.txtPNrFilter3End)
        Me.GroupBox2.Controls.Add(Me.txtPNrFilter2Start)
        Me.GroupBox2.Controls.Add(Me.Label35)
        Me.GroupBox2.Controls.Add(Me.txtPNrFilter2End)
        Me.GroupBox2.Controls.Add(Me.txtOperationStart)
        Me.GroupBox2.Controls.Add(Me.Label33)
        Me.GroupBox2.Controls.Add(Me.txtOperationEnd)
        Me.GroupBox2.Controls.Add(Me.Label34)
        Me.GroupBox2.Controls.Add(Me.Label32)
        Me.GroupBox2.Controls.Add(Me.txtInputPartNumber)
        Me.GroupBox2.Controls.Add(Me.lblPartNumber)
        Me.GroupBox2.Controls.Add(Me.txtPNrFilter1Start)
        Me.GroupBox2.Controls.Add(Me.LBL_prefix)
        Me.GroupBox2.Controls.Add(Me.txtPNrFilter1End)
        Me.GroupBox2.Controls.Add(Me.LBL_suffix)
        Me.GroupBox2.Controls.Add(Me.btnClearPartNumber)
        Me.GroupBox2.Controls.Add(Me.txtPNrOutput)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.btnRefreshPartNumber)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 21)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(637, 285)
        Me.GroupBox2.TabIndex = 110
        Me.GroupBox2.TabStop = False
        '
        'chkOperation
        '
        Me.chkOperation.AutoSize = True
        Me.chkOperation.Location = New System.Drawing.Point(92, 215)
        Me.chkOperation.Name = "chkOperation"
        Me.chkOperation.Size = New System.Drawing.Size(15, 14)
        Me.chkOperation.TabIndex = 104
        Me.chkOperation.UseVisualStyleBackColor = True
        '
        'txtOperationOutput
        '
        Me.txtOperationOutput.Enabled = False
        Me.txtOperationOutput.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtOperationOutput.Location = New System.Drawing.Point(416, 244)
        Me.txtOperationOutput.Name = "txtOperationOutput"
        Me.txtOperationOutput.ReadOnly = True
        Me.txtOperationOutput.Size = New System.Drawing.Size(160, 20)
        Me.txtOperationOutput.TabIndex = 102
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(359, 247)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(51, 13)
        Me.Label43.TabIndex = 103
        Me.Label43.Text = "Output :"
        '
        'chkFilter3
        '
        Me.chkFilter3.AutoSize = True
        Me.chkFilter3.Enabled = False
        Me.chkFilter3.Location = New System.Drawing.Point(22, 142)
        Me.chkFilter3.Name = "chkFilter3"
        Me.chkFilter3.Size = New System.Drawing.Size(15, 14)
        Me.chkFilter3.TabIndex = 101
        Me.chkFilter3.UseVisualStyleBackColor = True
        '
        'chkFilter2
        '
        Me.chkFilter2.AutoSize = True
        Me.chkFilter2.Location = New System.Drawing.Point(22, 115)
        Me.chkFilter2.Name = "chkFilter2"
        Me.chkFilter2.Size = New System.Drawing.Size(15, 14)
        Me.chkFilter2.TabIndex = 100
        Me.chkFilter2.UseVisualStyleBackColor = True
        '
        'txtPNrPrefix3
        '
        Me.txtPNrPrefix3.Enabled = False
        Me.txtPNrPrefix3.Location = New System.Drawing.Point(117, 139)
        Me.txtPNrPrefix3.Name = "txtPNrPrefix3"
        Me.txtPNrPrefix3.Size = New System.Drawing.Size(75, 21)
        Me.txtPNrPrefix3.TabIndex = 98
        '
        'Label41
        '
        Me.Label41.Location = New System.Drawing.Point(36, 139)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(70, 18)
        Me.Label41.TabIndex = 99
        Me.Label41.Text = "Prefix :"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPNrPrefix2
        '
        Me.txtPNrPrefix2.Enabled = False
        Me.txtPNrPrefix2.Location = New System.Drawing.Point(117, 112)
        Me.txtPNrPrefix2.Name = "txtPNrPrefix2"
        Me.txtPNrPrefix2.Size = New System.Drawing.Size(75, 21)
        Me.txtPNrPrefix2.TabIndex = 96
        '
        'Label40
        '
        Me.Label40.Location = New System.Drawing.Point(36, 112)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(70, 18)
        Me.Label40.TabIndex = 97
        Me.Label40.Text = "Prefix :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label38
        '
        Me.Label38.Location = New System.Drawing.Point(390, 139)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(80, 18)
        Me.Label38.TabIndex = 95
        Me.Label38.Text = "Ends before :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label36
        '
        Me.Label36.Location = New System.Drawing.Point(390, 112)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(80, 18)
        Me.Label36.TabIndex = 94
        Me.Label36.Text = "Ends before :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPNrPrefix1
        '
        Me.txtPNrPrefix1.Location = New System.Drawing.Point(117, 85)
        Me.txtPNrPrefix1.Name = "txtPNrPrefix1"
        Me.txtPNrPrefix1.Size = New System.Drawing.Size(75, 21)
        Me.txtPNrPrefix1.TabIndex = 92
        '
        'Label39
        '
        Me.Label39.Location = New System.Drawing.Point(36, 85)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(70, 18)
        Me.Label39.TabIndex = 93
        Me.Label39.Text = "Prefix :"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPNrFilter3Start
        '
        Me.txtPNrFilter3Start.Enabled = False
        Me.txtPNrFilter3Start.Location = New System.Drawing.Point(284, 139)
        Me.txtPNrFilter3Start.Name = "txtPNrFilter3Start"
        Me.txtPNrFilter3Start.Size = New System.Drawing.Size(100, 21)
        Me.txtPNrFilter3Start.TabIndex = 88
        '
        'Label37
        '
        Me.Label37.Location = New System.Drawing.Point(198, 139)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(80, 18)
        Me.Label37.TabIndex = 89
        Me.Label37.Text = "Starts after :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPNrFilter3End
        '
        Me.txtPNrFilter3End.Enabled = False
        Me.txtPNrFilter3End.Location = New System.Drawing.Point(476, 139)
        Me.txtPNrFilter3End.Name = "txtPNrFilter3End"
        Me.txtPNrFilter3End.Size = New System.Drawing.Size(100, 21)
        Me.txtPNrFilter3End.TabIndex = 90
        '
        'txtPNrFilter2Start
        '
        Me.txtPNrFilter2Start.Enabled = False
        Me.txtPNrFilter2Start.Location = New System.Drawing.Point(284, 112)
        Me.txtPNrFilter2Start.Name = "txtPNrFilter2Start"
        Me.txtPNrFilter2Start.Size = New System.Drawing.Size(100, 21)
        Me.txtPNrFilter2Start.TabIndex = 84
        '
        'Label35
        '
        Me.Label35.Location = New System.Drawing.Point(198, 112)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(80, 18)
        Me.Label35.TabIndex = 85
        Me.Label35.Text = "Starts after :"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPNrFilter2End
        '
        Me.txtPNrFilter2End.Enabled = False
        Me.txtPNrFilter2End.Location = New System.Drawing.Point(476, 112)
        Me.txtPNrFilter2End.Name = "txtPNrFilter2End"
        Me.txtPNrFilter2End.Size = New System.Drawing.Size(100, 21)
        Me.txtPNrFilter2End.TabIndex = 86
        '
        'txtOperationStart
        '
        Me.txtOperationStart.Location = New System.Drawing.Point(117, 244)
        Me.txtOperationStart.Name = "txtOperationStart"
        Me.txtOperationStart.ReadOnly = True
        Me.txtOperationStart.Size = New System.Drawing.Size(75, 21)
        Me.txtOperationStart.TabIndex = 78
        '
        'Label33
        '
        Me.Label33.Location = New System.Drawing.Point(16, 244)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(90, 18)
        Me.Label33.TabIndex = 79
        Me.Label33.Text = "Starts after :"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOperationEnd
        '
        Me.txtOperationEnd.Location = New System.Drawing.Point(278, 244)
        Me.txtOperationEnd.Name = "txtOperationEnd"
        Me.txtOperationEnd.ReadOnly = True
        Me.txtOperationEnd.Size = New System.Drawing.Size(75, 21)
        Me.txtOperationEnd.TabIndex = 80
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(198, 247)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(75, 13)
        Me.Label34.TabIndex = 81
        Me.Label34.Text = "Ends before :"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(113, 210)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(78, 19)
        Me.Label32.TabIndex = 77
        Me.Label32.Text = "Operation :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInputPartNumber
        '
        Me.txtInputPartNumber.Location = New System.Drawing.Point(117, 50)
        Me.txtInputPartNumber.Name = "txtInputPartNumber"
        Me.txtInputPartNumber.Size = New System.Drawing.Size(459, 21)
        Me.txtInputPartNumber.TabIndex = 72
        '
        'lblPartNumber
        '
        Me.lblPartNumber.AutoSize = True
        Me.lblPartNumber.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPartNumber.Location = New System.Drawing.Point(113, 20)
        Me.lblPartNumber.Name = "lblPartNumber"
        Me.lblPartNumber.Size = New System.Drawing.Size(95, 19)
        Me.lblPartNumber.TabIndex = 57
        Me.lblPartNumber.Text = "Part Number :"
        Me.lblPartNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPNrFilter1Start
        '
        Me.txtPNrFilter1Start.Location = New System.Drawing.Point(284, 85)
        Me.txtPNrFilter1Start.Name = "txtPNrFilter1Start"
        Me.txtPNrFilter1Start.Size = New System.Drawing.Size(100, 21)
        Me.txtPNrFilter1Start.TabIndex = 64
        '
        'LBL_prefix
        '
        Me.LBL_prefix.Location = New System.Drawing.Point(198, 85)
        Me.LBL_prefix.Name = "LBL_prefix"
        Me.LBL_prefix.Size = New System.Drawing.Size(80, 18)
        Me.LBL_prefix.TabIndex = 65
        Me.LBL_prefix.Text = "Starts after :"
        Me.LBL_prefix.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPNrFilter1End
        '
        Me.txtPNrFilter1End.Location = New System.Drawing.Point(476, 85)
        Me.txtPNrFilter1End.Name = "txtPNrFilter1End"
        Me.txtPNrFilter1End.Size = New System.Drawing.Size(100, 21)
        Me.txtPNrFilter1End.TabIndex = 66
        '
        'LBL_suffix
        '
        Me.LBL_suffix.Location = New System.Drawing.Point(390, 85)
        Me.LBL_suffix.Name = "LBL_suffix"
        Me.LBL_suffix.Size = New System.Drawing.Size(80, 18)
        Me.LBL_suffix.TabIndex = 67
        Me.LBL_suffix.Text = "Ends before :"
        Me.LBL_suffix.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClearPartNumber
        '
        Me.btnClearPartNumber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClearPartNumber.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearPartNumber.FlatAppearance.BorderSize = 0
        Me.btnClearPartNumber.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnClearPartNumber.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnClearPartNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearPartNumber.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnClearPartNumber.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Delete
        Me.btnClearPartNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearPartNumber.Location = New System.Drawing.Point(19, 15)
        Me.btnClearPartNumber.Name = "btnClearPartNumber"
        Me.btnClearPartNumber.Size = New System.Drawing.Size(88, 34)
        Me.btnClearPartNumber.TabIndex = 68
        Me.btnClearPartNumber.Text = "Clear"
        Me.btnClearPartNumber.UseVisualStyleBackColor = True
        '
        'txtPNrOutput
        '
        Me.txtPNrOutput.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPNrOutput.Location = New System.Drawing.Point(117, 174)
        Me.txtPNrOutput.Name = "txtPNrOutput"
        Me.txtPNrOutput.ReadOnly = True
        Me.txtPNrOutput.Size = New System.Drawing.Size(459, 20)
        Me.txtPNrOutput.TabIndex = 73
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(16, 50)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 18)
        Me.Label12.TabIndex = 74
        Me.Label12.Text = "Input :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(16, 174)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 18)
        Me.Label11.TabIndex = 75
        Me.Label11.Text = "Output :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnRefreshPartNumber
        '
        Me.btnRefreshPartNumber.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Refresh
        Me.btnRefreshPartNumber.Location = New System.Drawing.Point(582, 46)
        Me.btnRefreshPartNumber.Name = "btnRefreshPartNumber"
        Me.btnRefreshPartNumber.Size = New System.Drawing.Size(25, 25)
        Me.btnRefreshPartNumber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnRefreshPartNumber.TabIndex = 76
        Me.btnRefreshPartNumber.TabStop = False
        '
        'grpOthers
        '
        Me.grpOthers.Controls.Add(Me.txtMinFovr)
        Me.grpOthers.Controls.Add(Me.lblFovr)
        Me.grpOthers.Controls.Add(Me.lblSovr)
        Me.grpOthers.Controls.Add(Me.btnClearFeedrate)
        Me.grpOthers.Controls.Add(Me.btnClearSpindle)
        Me.grpOthers.Controls.Add(Me.lblPartsCount)
        Me.grpOthers.Controls.Add(Me.lblRovr)
        Me.grpOthers.Controls.Add(Me.btnClearRapid)
        Me.grpOthers.Controls.Add(Me.lblRequiredParts)
        Me.grpOthers.Controls.Add(Me.BTN_FeedrateEdit)
        Me.grpOthers.Controls.Add(Me.Label14)
        Me.grpOthers.Controls.Add(Me.BTN_SpindleEdit)
        Me.grpOthers.Controls.Add(Me.Label13)
        Me.grpOthers.Controls.Add(Me.BTN_RapidEdit)
        Me.grpOthers.Controls.Add(Me.txtMaxRovr)
        Me.grpOthers.Controls.Add(Me.BTN_FeedrateOK)
        Me.grpOthers.Controls.Add(Me.txtMinRovr)
        Me.grpOthers.Controls.Add(Me.BTN_SpindleOK)
        Me.grpOthers.Controls.Add(Me.txtMaxSovr)
        Me.grpOthers.Controls.Add(Me.BTN_RapidOK)
        Me.grpOthers.Controls.Add(Me.txtMinSovr)
        Me.grpOthers.Controls.Add(Me.txtMaxFovr)
        Me.grpOthers.Controls.Add(Me.btnClearRequiredParts)
        Me.grpOthers.Controls.Add(Me.btnClearPartsCount)
        Me.grpOthers.Location = New System.Drawing.Point(9, 458)
        Me.grpOthers.Name = "grpOthers"
        Me.grpOthers.Size = New System.Drawing.Size(637, 213)
        Me.grpOthers.TabIndex = 112
        Me.grpOthers.TabStop = False
        '
        'txtMinFovr
        '
        Me.txtMinFovr.Enabled = False
        Me.txtMinFovr.Location = New System.Drawing.Point(383, 43)
        Me.txtMinFovr.Name = "txtMinFovr"
        Me.txtMinFovr.ReadOnly = True
        Me.txtMinFovr.Size = New System.Drawing.Size(50, 21)
        Me.txtMinFovr.TabIndex = 88
        Me.txtMinFovr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblFovr
        '
        Me.lblFovr.AutoEllipsis = True
        Me.lblFovr.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFovr.Location = New System.Drawing.Point(70, 43)
        Me.lblFovr.Name = "lblFovr"
        Me.lblFovr.Size = New System.Drawing.Size(307, 21)
        Me.lblFovr.TabIndex = 58
        Me.lblFovr.Text = "Feedrate Override :"
        Me.lblFovr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSovr
        '
        Me.lblSovr.AutoEllipsis = True
        Me.lblSovr.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSovr.Location = New System.Drawing.Point(70, 76)
        Me.lblSovr.Name = "lblSovr"
        Me.lblSovr.Size = New System.Drawing.Size(307, 21)
        Me.lblSovr.TabIndex = 59
        Me.lblSovr.Text = "Spindle Override :"
        Me.lblSovr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClearFeedrate
        '
        Me.btnClearFeedrate.AutoEllipsis = True
        Me.btnClearFeedrate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClearFeedrate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearFeedrate.FlatAppearance.BorderSize = 0
        Me.btnClearFeedrate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnClearFeedrate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnClearFeedrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearFeedrate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnClearFeedrate.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Delete
        Me.btnClearFeedrate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearFeedrate.Location = New System.Drawing.Point(8, 41)
        Me.btnClearFeedrate.Name = "btnClearFeedrate"
        Me.btnClearFeedrate.Size = New System.Drawing.Size(70, 21)
        Me.btnClearFeedrate.TabIndex = 70
        Me.btnClearFeedrate.Text = "Clear"
        Me.btnClearFeedrate.UseVisualStyleBackColor = True
        '
        'btnClearSpindle
        '
        Me.btnClearSpindle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClearSpindle.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSpindle.FlatAppearance.BorderSize = 0
        Me.btnClearSpindle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnClearSpindle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnClearSpindle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSpindle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnClearSpindle.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Delete
        Me.btnClearSpindle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearSpindle.Location = New System.Drawing.Point(8, 77)
        Me.btnClearSpindle.Name = "btnClearSpindle"
        Me.btnClearSpindle.Size = New System.Drawing.Size(70, 21)
        Me.btnClearSpindle.TabIndex = 71
        Me.btnClearSpindle.Text = "Clear"
        Me.btnClearSpindle.UseVisualStyleBackColor = True
        '
        'lblPartsCount
        '
        Me.lblPartsCount.AutoEllipsis = True
        Me.lblPartsCount.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPartsCount.Location = New System.Drawing.Point(70, 180)
        Me.lblPartsCount.Name = "lblPartsCount"
        Me.lblPartsCount.Size = New System.Drawing.Size(419, 21)
        Me.lblPartsCount.TabIndex = 107
        Me.lblPartsCount.Text = "Part count :"
        Me.lblPartsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRovr
        '
        Me.lblRovr.AutoEllipsis = True
        Me.lblRovr.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRovr.Location = New System.Drawing.Point(70, 110)
        Me.lblRovr.Name = "lblRovr"
        Me.lblRovr.Size = New System.Drawing.Size(307, 21)
        Me.lblRovr.TabIndex = 77
        Me.lblRovr.Text = "Rapid Override :"
        Me.lblRovr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClearRapid
        '
        Me.btnClearRapid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClearRapid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearRapid.FlatAppearance.BorderSize = 0
        Me.btnClearRapid.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnClearRapid.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnClearRapid.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearRapid.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnClearRapid.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Delete
        Me.btnClearRapid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearRapid.Location = New System.Drawing.Point(8, 111)
        Me.btnClearRapid.Name = "btnClearRapid"
        Me.btnClearRapid.Size = New System.Drawing.Size(70, 21)
        Me.btnClearRapid.TabIndex = 78
        Me.btnClearRapid.Text = "Clear"
        Me.btnClearRapid.UseVisualStyleBackColor = True
        '
        'lblRequiredParts
        '
        Me.lblRequiredParts.AutoEllipsis = True
        Me.lblRequiredParts.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRequiredParts.Location = New System.Drawing.Point(70, 145)
        Me.lblRequiredParts.Name = "lblRequiredParts"
        Me.lblRequiredParts.Size = New System.Drawing.Size(419, 21)
        Me.lblRequiredParts.TabIndex = 105
        Me.lblRequiredParts.Text = "Required parts :"
        Me.lblRequiredParts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BTN_FeedrateEdit
        '
        Me.BTN_FeedrateEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BTN_FeedrateEdit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_FeedrateEdit.FlatAppearance.BorderSize = 0
        Me.BTN_FeedrateEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BTN_FeedrateEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_FeedrateEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_FeedrateEdit.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BTN_FeedrateEdit.Location = New System.Drawing.Point(497, 42)
        Me.BTN_FeedrateEdit.Name = "BTN_FeedrateEdit"
        Me.BTN_FeedrateEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BTN_FeedrateEdit.Size = New System.Drawing.Size(25, 25)
        Me.BTN_FeedrateEdit.TabIndex = 82
        Me.BTN_FeedrateEdit.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(443, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(39, 19)
        Me.Label14.TabIndex = 95
        Me.Label14.Text = "MAX"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTN_SpindleEdit
        '
        Me.BTN_SpindleEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BTN_SpindleEdit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_SpindleEdit.FlatAppearance.BorderSize = 0
        Me.BTN_SpindleEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BTN_SpindleEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_SpindleEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_SpindleEdit.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BTN_SpindleEdit.Location = New System.Drawing.Point(497, 75)
        Me.BTN_SpindleEdit.Name = "BTN_SpindleEdit"
        Me.BTN_SpindleEdit.Size = New System.Drawing.Size(25, 25)
        Me.BTN_SpindleEdit.TabIndex = 83
        Me.BTN_SpindleEdit.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(388, 14)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 19)
        Me.Label13.TabIndex = 94
        Me.Label13.Text = "MIN "
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BTN_RapidEdit
        '
        Me.BTN_RapidEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BTN_RapidEdit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_RapidEdit.FlatAppearance.BorderSize = 0
        Me.BTN_RapidEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BTN_RapidEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_RapidEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_RapidEdit.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BTN_RapidEdit.Location = New System.Drawing.Point(497, 109)
        Me.BTN_RapidEdit.Name = "BTN_RapidEdit"
        Me.BTN_RapidEdit.Size = New System.Drawing.Size(25, 25)
        Me.BTN_RapidEdit.TabIndex = 84
        Me.BTN_RapidEdit.UseVisualStyleBackColor = True
        '
        'txtMaxRovr
        '
        Me.txtMaxRovr.Enabled = False
        Me.txtMaxRovr.Location = New System.Drawing.Point(439, 113)
        Me.txtMaxRovr.Name = "txtMaxRovr"
        Me.txtMaxRovr.ReadOnly = True
        Me.txtMaxRovr.Size = New System.Drawing.Size(50, 21)
        Me.txtMaxRovr.TabIndex = 93
        Me.txtMaxRovr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BTN_FeedrateOK
        '
        Me.BTN_FeedrateOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BTN_FeedrateOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_FeedrateOK.Enabled = False
        Me.BTN_FeedrateOK.FlatAppearance.BorderSize = 0
        Me.BTN_FeedrateOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BTN_FeedrateOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_FeedrateOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_FeedrateOK.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BTN_FeedrateOK.Location = New System.Drawing.Point(533, 41)
        Me.BTN_FeedrateOK.Name = "BTN_FeedrateOK"
        Me.BTN_FeedrateOK.Size = New System.Drawing.Size(25, 25)
        Me.BTN_FeedrateOK.TabIndex = 85
        Me.BTN_FeedrateOK.UseVisualStyleBackColor = True
        '
        'txtMinRovr
        '
        Me.txtMinRovr.Enabled = False
        Me.txtMinRovr.Location = New System.Drawing.Point(383, 113)
        Me.txtMinRovr.Name = "txtMinRovr"
        Me.txtMinRovr.ReadOnly = True
        Me.txtMinRovr.Size = New System.Drawing.Size(50, 21)
        Me.txtMinRovr.TabIndex = 92
        Me.txtMinRovr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BTN_SpindleOK
        '
        Me.BTN_SpindleOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BTN_SpindleOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_SpindleOK.Enabled = False
        Me.BTN_SpindleOK.FlatAppearance.BorderSize = 0
        Me.BTN_SpindleOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BTN_SpindleOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_SpindleOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_SpindleOK.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BTN_SpindleOK.Location = New System.Drawing.Point(533, 74)
        Me.BTN_SpindleOK.Name = "BTN_SpindleOK"
        Me.BTN_SpindleOK.Size = New System.Drawing.Size(25, 25)
        Me.BTN_SpindleOK.TabIndex = 86
        Me.BTN_SpindleOK.UseVisualStyleBackColor = True
        '
        'txtMaxSovr
        '
        Me.txtMaxSovr.Enabled = False
        Me.txtMaxSovr.Location = New System.Drawing.Point(439, 78)
        Me.txtMaxSovr.Name = "txtMaxSovr"
        Me.txtMaxSovr.ReadOnly = True
        Me.txtMaxSovr.Size = New System.Drawing.Size(50, 21)
        Me.txtMaxSovr.TabIndex = 91
        Me.txtMaxSovr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BTN_RapidOK
        '
        Me.BTN_RapidOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BTN_RapidOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_RapidOK.Enabled = False
        Me.BTN_RapidOK.FlatAppearance.BorderSize = 0
        Me.BTN_RapidOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BTN_RapidOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_RapidOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_RapidOK.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BTN_RapidOK.Location = New System.Drawing.Point(533, 108)
        Me.BTN_RapidOK.Name = "BTN_RapidOK"
        Me.BTN_RapidOK.Size = New System.Drawing.Size(25, 25)
        Me.BTN_RapidOK.TabIndex = 87
        Me.BTN_RapidOK.UseVisualStyleBackColor = True
        '
        'txtMinSovr
        '
        Me.txtMinSovr.Enabled = False
        Me.txtMinSovr.Location = New System.Drawing.Point(383, 78)
        Me.txtMinSovr.Name = "txtMinSovr"
        Me.txtMinSovr.ReadOnly = True
        Me.txtMinSovr.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtMinSovr.Size = New System.Drawing.Size(50, 21)
        Me.txtMinSovr.TabIndex = 90
        Me.txtMinSovr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMaxFovr
        '
        Me.txtMaxFovr.Enabled = False
        Me.txtMaxFovr.Location = New System.Drawing.Point(439, 43)
        Me.txtMaxFovr.Name = "txtMaxFovr"
        Me.txtMaxFovr.ReadOnly = True
        Me.txtMaxFovr.Size = New System.Drawing.Size(50, 21)
        Me.txtMaxFovr.TabIndex = 89
        Me.txtMaxFovr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnClearRequiredParts
        '
        Me.btnClearRequiredParts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClearRequiredParts.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearRequiredParts.FlatAppearance.BorderSize = 0
        Me.btnClearRequiredParts.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnClearRequiredParts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnClearRequiredParts.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearRequiredParts.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnClearRequiredParts.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Delete
        Me.btnClearRequiredParts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearRequiredParts.Location = New System.Drawing.Point(8, 146)
        Me.btnClearRequiredParts.Name = "btnClearRequiredParts"
        Me.btnClearRequiredParts.Size = New System.Drawing.Size(70, 21)
        Me.btnClearRequiredParts.TabIndex = 106
        Me.btnClearRequiredParts.Text = "Clear"
        Me.btnClearRequiredParts.UseVisualStyleBackColor = True
        '
        'btnClearPartsCount
        '
        Me.btnClearPartsCount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClearPartsCount.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearPartsCount.FlatAppearance.BorderSize = 0
        Me.btnClearPartsCount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnClearPartsCount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnClearPartsCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearPartsCount.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnClearPartsCount.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Delete
        Me.btnClearPartsCount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearPartsCount.Location = New System.Drawing.Point(8, 181)
        Me.btnClearPartsCount.Name = "btnClearPartsCount"
        Me.btnClearPartsCount.Size = New System.Drawing.Size(70, 21)
        Me.btnClearPartsCount.TabIndex = 108
        Me.btnClearPartsCount.Text = "Clear"
        Me.btnClearPartsCount.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 7.0!)
        Me.Label6.Location = New System.Drawing.Point(17, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(460, 24)
        Me.Label6.TabIndex = 63
        Me.Label6.Text = "* CSIFLEX will take the programme number as a part number if no part number avail" &
    "able"
        '
        'grpPallet
        '
        Me.grpPallet.Controls.Add(Me.chkEnableCriticalStop)
        Me.grpPallet.Controls.Add(Me.cmbMCSScale)
        Me.grpPallet.Controls.Add(Me.Label31)
        Me.grpPallet.Controls.Add(Me.numMCSDelay)
        Me.grpPallet.Controls.Add(Me.txtCriticalTemperature)
        Me.grpPallet.Controls.Add(Me.Label29)
        Me.grpPallet.Controls.Add(Me.txtCriticalPressure)
        Me.grpPallet.Controls.Add(Me.Label30)
        Me.grpPallet.Controls.Add(Me.txtWarningTemperature)
        Me.grpPallet.Controls.Add(Me.Label28)
        Me.grpPallet.Controls.Add(Me.txtWarningPressure)
        Me.grpPallet.Controls.Add(Me.Label27)
        Me.grpPallet.Controls.Add(Me.Label26)
        Me.grpPallet.Controls.Add(Me.Label25)
        Me.grpPallet.Controls.Add(Me.Label20)
        Me.grpPallet.Controls.Add(Me.chkSendPallet)
        Me.grpPallet.Controls.Add(Me.txtInputPallet)
        Me.grpPallet.Controls.Add(Me.lblActivePallet)
        Me.grpPallet.Controls.Add(Me.btnClearPallet)
        Me.grpPallet.Controls.Add(Me.txtStartPallet)
        Me.grpPallet.Controls.Add(Me.Label21)
        Me.grpPallet.Controls.Add(Me.btnRefreshPallet)
        Me.grpPallet.Controls.Add(Me.txtEndPallet)
        Me.grpPallet.Controls.Add(Me.Label22)
        Me.grpPallet.Controls.Add(Me.Label23)
        Me.grpPallet.Controls.Add(Me.Label24)
        Me.grpPallet.Controls.Add(Me.txtOutputPallet)
        Me.grpPallet.Location = New System.Drawing.Point(9, 677)
        Me.grpPallet.Name = "grpPallet"
        Me.grpPallet.Size = New System.Drawing.Size(637, 310)
        Me.grpPallet.TabIndex = 111
        Me.grpPallet.TabStop = False
        '
        'chkEnableCriticalStop
        '
        Me.chkEnableCriticalStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkEnableCriticalStop.AutoSize = True
        Me.chkEnableCriticalStop.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnableCriticalStop.Location = New System.Drawing.Point(390, 272)
        Me.chkEnableCriticalStop.Name = "chkEnableCriticalStop"
        Me.chkEnableCriticalStop.Size = New System.Drawing.Size(126, 17)
        Me.chkEnableCriticalStop.TabIndex = 120
        Me.chkEnableCriticalStop.Text = "Enable Critical Stop"
        Me.chkEnableCriticalStop.UseVisualStyleBackColor = True
        '
        'cmbMCSScale
        '
        Me.cmbMCSScale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbMCSScale.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbMCSScale.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMCSScale.FormattingEnabled = True
        Me.cmbMCSScale.Items.AddRange(New Object() {"Seconds", "Minutes"})
        Me.cmbMCSScale.Location = New System.Drawing.Point(185, 268)
        Me.cmbMCSScale.Name = "cmbMCSScale"
        Me.cmbMCSScale.Size = New System.Drawing.Size(100, 21)
        Me.cmbMCSScale.TabIndex = 119
        '
        'Label31
        '
        Me.Label31.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label31.Font = New System.Drawing.Font("Segoe UI", 7.8!)
        Me.Label31.Location = New System.Drawing.Point(19, 269)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(88, 19)
        Me.Label31.TabIndex = 117
        Me.Label31.Text = "Delay  :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'numMCSDelay
        '
        Me.numMCSDelay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.numMCSDelay.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.numMCSDelay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numMCSDelay.Location = New System.Drawing.Point(117, 272)
        Me.numMCSDelay.Maximum = New Decimal(New Integer() {-727379968, 232, 0, 0})
        Me.numMCSDelay.Name = "numMCSDelay"
        Me.numMCSDelay.Size = New System.Drawing.Size(61, 18)
        Me.numMCSDelay.TabIndex = 118
        Me.numMCSDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCriticalTemperature
        '
        Me.txtCriticalTemperature.Location = New System.Drawing.Point(390, 235)
        Me.txtCriticalTemperature.Name = "txtCriticalTemperature"
        Me.txtCriticalTemperature.Size = New System.Drawing.Size(100, 21)
        Me.txtCriticalTemperature.TabIndex = 116
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(294, 235)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(90, 18)
        Me.Label29.TabIndex = 115
        Me.Label29.Text = "Temperature:"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCriticalPressure
        '
        Me.txtCriticalPressure.Location = New System.Drawing.Point(185, 235)
        Me.txtCriticalPressure.Name = "txtCriticalPressure"
        Me.txtCriticalPressure.Size = New System.Drawing.Size(100, 21)
        Me.txtCriticalPressure.TabIndex = 114
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(109, 235)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(70, 18)
        Me.Label30.TabIndex = 113
        Me.Label30.Text = "Pressure:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtWarningTemperature
        '
        Me.txtWarningTemperature.Location = New System.Drawing.Point(390, 200)
        Me.txtWarningTemperature.Name = "txtWarningTemperature"
        Me.txtWarningTemperature.Size = New System.Drawing.Size(100, 21)
        Me.txtWarningTemperature.TabIndex = 112
        '
        'Label28
        '
        Me.Label28.Location = New System.Drawing.Point(294, 200)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(90, 18)
        Me.Label28.TabIndex = 111
        Me.Label28.Text = "Temperature:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtWarningPressure
        '
        Me.txtWarningPressure.Location = New System.Drawing.Point(185, 200)
        Me.txtWarningPressure.Name = "txtWarningPressure"
        Me.txtWarningPressure.Size = New System.Drawing.Size(100, 21)
        Me.txtWarningPressure.TabIndex = 110
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(109, 200)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(70, 18)
        Me.Label27.TabIndex = 109
        Me.Label27.Text = "Pressure:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(16, 233)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(90, 18)
        Me.Label26.TabIndex = 108
        Me.Label26.Text = "Critical"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(16, 198)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(90, 18)
        Me.Label25.TabIndex = 107
        Me.Label25.Text = "Warning"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(15, 17)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(200, 21)
        Me.Label20.TabIndex = 106
        Me.Label20.Text = "Monitoring Unit Settings"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkSendPallet
        '
        Me.chkSendPallet.AutoSize = True
        Me.chkSendPallet.Location = New System.Drawing.Point(348, 146)
        Me.chkSendPallet.Name = "chkSendPallet"
        Me.chkSendPallet.Size = New System.Drawing.Size(189, 17)
        Me.chkSendPallet.TabIndex = 105
        Me.chkSendPallet.Text = "Send Pallet to Monitoring Units"
        Me.chkSendPallet.UseVisualStyleBackColor = True
        '
        'txtInputPallet
        '
        Me.txtInputPallet.Location = New System.Drawing.Point(117, 84)
        Me.txtInputPallet.Name = "txtInputPallet"
        Me.txtInputPallet.Size = New System.Drawing.Size(164, 21)
        Me.txtInputPallet.TabIndex = 100
        '
        'lblActivePallet
        '
        Me.lblActivePallet.AutoSize = True
        Me.lblActivePallet.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActivePallet.Location = New System.Drawing.Point(113, 52)
        Me.lblActivePallet.Name = "lblActivePallet"
        Me.lblActivePallet.Size = New System.Drawing.Size(86, 19)
        Me.lblActivePallet.TabIndex = 60
        Me.lblActivePallet.Text = "Active Pallet:"
        Me.lblActivePallet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClearPallet
        '
        Me.btnClearPallet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClearPallet.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearPallet.FlatAppearance.BorderSize = 0
        Me.btnClearPallet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnClearPallet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnClearPallet.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearPallet.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnClearPallet.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Delete
        Me.btnClearPallet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearPallet.Location = New System.Drawing.Point(19, 45)
        Me.btnClearPallet.Name = "btnClearPallet"
        Me.btnClearPallet.Size = New System.Drawing.Size(87, 34)
        Me.btnClearPallet.TabIndex = 69
        Me.btnClearPallet.Text = "Clear"
        Me.btnClearPallet.UseVisualStyleBackColor = True
        '
        'txtStartPallet
        '
        Me.txtStartPallet.Location = New System.Drawing.Point(117, 114)
        Me.txtStartPallet.Name = "txtStartPallet"
        Me.txtStartPallet.Size = New System.Drawing.Size(100, 21)
        Me.txtStartPallet.TabIndex = 96
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(16, 114)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(90, 18)
        Me.Label21.TabIndex = 97
        Me.Label21.Text = "Starts with :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnRefreshPallet
        '
        Me.btnRefreshPallet.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Refresh
        Me.btnRefreshPallet.Location = New System.Drawing.Point(289, 80)
        Me.btnRefreshPallet.Name = "btnRefreshPallet"
        Me.btnRefreshPallet.Size = New System.Drawing.Size(25, 25)
        Me.btnRefreshPallet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnRefreshPallet.TabIndex = 104
        Me.btnRefreshPallet.TabStop = False
        '
        'txtEndPallet
        '
        Me.txtEndPallet.Location = New System.Drawing.Point(348, 114)
        Me.txtEndPallet.Name = "txtEndPallet"
        Me.txtEndPallet.Size = New System.Drawing.Size(100, 21)
        Me.txtEndPallet.TabIndex = 98
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(16, 144)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(90, 18)
        Me.Label22.TabIndex = 103
        Me.Label22.Text = "Output :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(270, 117)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(64, 13)
        Me.Label23.TabIndex = 99
        Me.Label23.Text = "Ends with :"
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(16, 84)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(90, 18)
        Me.Label24.TabIndex = 102
        Me.Label24.Text = "Input :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOutputPallet
        '
        Me.txtOutputPallet.Enabled = False
        Me.txtOutputPallet.Location = New System.Drawing.Point(117, 143)
        Me.txtOutputPallet.Name = "txtOutputPallet"
        Me.txtOutputPallet.Size = New System.Drawing.Size(164, 21)
        Me.txtOutputPallet.TabIndex = 101
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtInputProgrNumber)
        Me.GroupBox1.Controls.Add(Me.lblProgramNumber)
        Me.GroupBox1.Controls.Add(Me.btnClearProgramNumber)
        Me.GroupBox1.Controls.Add(Me.txtStartProgNumber)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.btnRefreshProgNum)
        Me.GroupBox1.Controls.Add(Me.txtEndProgNumber)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtOutputProgrNumber)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 312)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(637, 140)
        Me.GroupBox1.TabIndex = 109
        Me.GroupBox1.TabStop = False
        '
        'txtInputProgrNumber
        '
        Me.txtInputProgrNumber.Location = New System.Drawing.Point(117, 48)
        Me.txtInputProgrNumber.Name = "txtInputProgrNumber"
        Me.txtInputProgrNumber.Size = New System.Drawing.Size(164, 21)
        Me.txtInputProgrNumber.TabIndex = 100
        '
        'lblProgramNumber
        '
        Me.lblProgramNumber.AutoSize = True
        Me.lblProgramNumber.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgramNumber.Location = New System.Drawing.Point(113, 16)
        Me.lblProgramNumber.Name = "lblProgramNumber"
        Me.lblProgramNumber.Size = New System.Drawing.Size(91, 19)
        Me.lblProgramNumber.TabIndex = 60
        Me.lblProgramNumber.Text = "Program No :"
        Me.lblProgramNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClearProgramNumber
        '
        Me.btnClearProgramNumber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnClearProgramNumber.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearProgramNumber.FlatAppearance.BorderSize = 0
        Me.btnClearProgramNumber.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnClearProgramNumber.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnClearProgramNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearProgramNumber.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnClearProgramNumber.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Delete
        Me.btnClearProgramNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearProgramNumber.Location = New System.Drawing.Point(19, 14)
        Me.btnClearProgramNumber.Name = "btnClearProgramNumber"
        Me.btnClearProgramNumber.Size = New System.Drawing.Size(88, 34)
        Me.btnClearProgramNumber.TabIndex = 69
        Me.btnClearProgramNumber.Text = "Clear"
        Me.btnClearProgramNumber.UseVisualStyleBackColor = True
        '
        'txtStartProgNumber
        '
        Me.txtStartProgNumber.Location = New System.Drawing.Point(117, 78)
        Me.txtStartProgNumber.Name = "txtStartProgNumber"
        Me.txtStartProgNumber.Size = New System.Drawing.Size(100, 21)
        Me.txtStartProgNumber.TabIndex = 96
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(16, 78)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(90, 18)
        Me.Label19.TabIndex = 97
        Me.Label19.Text = "Starts with :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnRefreshProgNum
        '
        Me.btnRefreshProgNum.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Refresh
        Me.btnRefreshProgNum.Location = New System.Drawing.Point(289, 44)
        Me.btnRefreshProgNum.Name = "btnRefreshProgNum"
        Me.btnRefreshProgNum.Size = New System.Drawing.Size(25, 25)
        Me.btnRefreshProgNum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnRefreshProgNum.TabIndex = 104
        Me.btnRefreshProgNum.TabStop = False
        '
        'txtEndProgNumber
        '
        Me.txtEndProgNumber.Location = New System.Drawing.Point(348, 78)
        Me.txtEndProgNumber.Name = "txtEndProgNumber"
        Me.txtEndProgNumber.Size = New System.Drawing.Size(100, 21)
        Me.txtEndProgNumber.TabIndex = 98
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(16, 107)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(90, 18)
        Me.Label16.TabIndex = 103
        Me.Label16.Text = "Output :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(270, 81)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(64, 13)
        Me.Label18.TabIndex = 99
        Me.Label18.Text = "Ends with :"
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(16, 48)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(90, 18)
        Me.Label17.TabIndex = 102
        Me.Label17.Text = "Input :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOutputProgrNumber
        '
        Me.txtOutputProgrNumber.Enabled = False
        Me.txtOutputProgrNumber.Location = New System.Drawing.Point(117, 107)
        Me.txtOutputProgrNumber.Name = "txtOutputProgrNumber"
        Me.txtOutputProgrNumber.Size = New System.Drawing.Size(164, 21)
        Me.txtOutputProgrNumber.TabIndex = 101
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.74436!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.25564!))
        Me.TableLayoutPanel2.Controls.Add(Me.Panel2, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(674, 183)
        Me.TableLayoutPanel2.TabIndex = 45
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.AutoScroll = True
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.LB_current_selected_values)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(324, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(347, 177)
        Me.Panel2.TabIndex = 1
        '
        'LB_current_selected_values
        '
        Me.LB_current_selected_values.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_current_selected_values.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LB_current_selected_values.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.LB_current_selected_values.FormattingEnabled = True
        Me.LB_current_selected_values.ItemHeight = 20
        Me.LB_current_selected_values.Location = New System.Drawing.Point(4, 33)
        Me.LB_current_selected_values.Margin = New System.Windows.Forms.Padding(7)
        Me.LB_current_selected_values.Name = "LB_current_selected_values"
        Me.LB_current_selected_values.Size = New System.Drawing.Size(336, 20)
        Me.LB_current_selected_values.TabIndex = 41
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(218, 19)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Current value for expression items"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.LV_Property)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(315, 177)
        Me.Panel1.TabIndex = 0
        '
        'LV_Property
        '
        Me.LV_Property.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.LV_Property.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LV_Property.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LV_Property.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Parameter, Me.Value})
        Me.LV_Property.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LV_Property.FullRowSelect = True
        Me.LV_Property.HideSelection = False
        Me.LV_Property.Location = New System.Drawing.Point(3, 35)
        Me.LV_Property.Margin = New System.Windows.Forms.Padding(7)
        Me.LV_Property.Name = "LV_Property"
        Me.LV_Property.Size = New System.Drawing.Size(305, 135)
        Me.LV_Property.TabIndex = 43
        Me.LV_Property.UseCompatibleStateImageBehavior = False
        Me.LV_Property.View = System.Windows.Forms.View.Details
        '
        'Parameter
        '
        Me.Parameter.Text = "Parameter"
        Me.Parameter.Width = 122
        '
        'Value
        '
        Me.Value.Text = "Value"
        Me.Value.Width = 115
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 19)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Selected items details"
        '
        'lbl_Result
        '
        Me.lbl_Result.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lbl_Result.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Result.Location = New System.Drawing.Point(41, 60)
        Me.lbl_Result.Name = "lbl_Result"
        Me.lbl_Result.Size = New System.Drawing.Size(113, 22)
        Me.lbl_Result.TabIndex = 35
        Me.lbl_Result.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.lbl_Result.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(41, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 19)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = " Status result "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label1.Visible = False
        '
        'BW_refresh_Status
        '
        Me.BW_refresh_Status.WorkerSupportsCancellation = True
        '
        'BW_refresh_tree
        '
        Me.BW_refresh_tree.WorkerSupportsCancellation = True
        '
        'Timer_threads
        '
        Me.Timer_threads.Interval = 1000
        '
        'TableLayout_TOP
        '
        Me.TableLayout_TOP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayout_TOP.BackColor = System.Drawing.Color.White
        Me.TableLayout_TOP.ColumnCount = 3
        Me.TableLayout_TOP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayout_TOP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayout_TOP.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayout_TOP.Controls.Add(Me.Panel_MTC_logo, 0, 0)
        Me.TableLayout_TOP.Controls.Add(Me.Panel_Status, 1, 0)
        Me.TableLayout_TOP.Controls.Add(Me.Panel_CSIF_logo, 2, 0)
        Me.TableLayout_TOP.Location = New System.Drawing.Point(10, 0)
        Me.TableLayout_TOP.Name = "TableLayout_TOP"
        Me.TableLayout_TOP.RowCount = 1
        Me.TableLayout_TOP.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayout_TOP.Size = New System.Drawing.Size(1477, 138)
        Me.TableLayout_TOP.TabIndex = 38
        '
        'Panel_MTC_logo
        '
        Me.Panel_MTC_logo.Controls.Add(Me.PB_CSIF_LOGO)
        Me.Panel_MTC_logo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_MTC_logo.Location = New System.Drawing.Point(3, 3)
        Me.Panel_MTC_logo.Name = "Panel_MTC_logo"
        Me.Panel_MTC_logo.Size = New System.Drawing.Size(632, 132)
        Me.Panel_MTC_logo.TabIndex = 0
        '
        'PB_CSIF_LOGO
        '
        Me.PB_CSIF_LOGO.BackColor = System.Drawing.Color.Transparent
        Me.PB_CSIF_LOGO.Image = Global.CSI_Reporting_Application.My.Resources.Resources.csiflex_logo_blue
        Me.PB_CSIF_LOGO.Location = New System.Drawing.Point(12, 23)
        Me.PB_CSIF_LOGO.Name = "PB_CSIF_LOGO"
        Me.PB_CSIF_LOGO.Size = New System.Drawing.Size(320, 89)
        Me.PB_CSIF_LOGO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_CSIF_LOGO.TabIndex = 1
        Me.PB_CSIF_LOGO.TabStop = False
        '
        'Panel_Status
        '
        Me.Panel_Status.Controls.Add(Me.PB_status)
        Me.Panel_Status.Controls.Add(Me.lbl_Result)
        Me.Panel_Status.Controls.Add(Me.Label1)
        Me.Panel_Status.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Status.Location = New System.Drawing.Point(641, 3)
        Me.Panel_Status.Name = "Panel_Status"
        Me.Panel_Status.Size = New System.Drawing.Size(194, 132)
        Me.Panel_Status.TabIndex = 1
        '
        'PB_status
        '
        Me.PB_status.Image = Global.CSI_Reporting_Application.My.Resources.Resources.green_ring
        Me.PB_status.Location = New System.Drawing.Point(29, 2)
        Me.PB_status.Name = "PB_status"
        Me.PB_status.Size = New System.Drawing.Size(137, 129)
        Me.PB_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_status.TabIndex = 37
        Me.PB_status.TabStop = False
        '
        'Panel_CSIF_logo
        '
        Me.Panel_CSIF_logo.Controls.Add(Me.lblMonitoringUnit)
        Me.Panel_CSIF_logo.Controls.Add(Me.pbFocas)
        Me.Panel_CSIF_logo.Controls.Add(Me.LB_MachineAddress)
        Me.Panel_CSIF_logo.Controls.Add(Me.PB_MTC_LOGO)
        Me.Panel_CSIF_logo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_CSIF_logo.Location = New System.Drawing.Point(841, 3)
        Me.Panel_CSIF_logo.Name = "Panel_CSIF_logo"
        Me.Panel_CSIF_logo.Size = New System.Drawing.Size(633, 132)
        Me.Panel_CSIF_logo.TabIndex = 2
        '
        'lblMonitoringUnit
        '
        Me.lblMonitoringUnit.AutoSize = True
        Me.lblMonitoringUnit.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonitoringUnit.Location = New System.Drawing.Point(52, 110)
        Me.lblMonitoringUnit.Name = "lblMonitoringUnit"
        Me.lblMonitoringUnit.Size = New System.Drawing.Size(47, 13)
        Me.lblMonitoringUnit.TabIndex = 3
        Me.lblMonitoringUnit.Text = "Label20"
        '
        'pbFocas
        '
        Me.pbFocas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbFocas.Image = Global.CSI_Reporting_Application.My.Resources.Resources.FANUC
        Me.pbFocas.Location = New System.Drawing.Point(363, 40)
        Me.pbFocas.Name = "pbFocas"
        Me.pbFocas.Size = New System.Drawing.Size(214, 47)
        Me.pbFocas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbFocas.TabIndex = 2
        Me.pbFocas.TabStop = False
        '
        'LB_MachineAddress
        '
        Me.LB_MachineAddress.AutoSize = True
        Me.LB_MachineAddress.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_MachineAddress.Location = New System.Drawing.Point(51, 93)
        Me.LB_MachineAddress.Name = "LB_MachineAddress"
        Me.LB_MachineAddress.Size = New System.Drawing.Size(47, 13)
        Me.LB_MachineAddress.TabIndex = 1
        Me.LB_MachineAddress.Text = "Label16"
        '
        'PB_MTC_LOGO
        '
        Me.PB_MTC_LOGO.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_MTC_LOGO.Image = Global.CSI_Reporting_Application.My.Resources.Resources.MTConnectLogo
        Me.PB_MTC_LOGO.Location = New System.Drawing.Point(363, 40)
        Me.PB_MTC_LOGO.Name = "PB_MTC_LOGO"
        Me.PB_MTC_LOGO.Size = New System.Drawing.Size(214, 47)
        Me.PB_MTC_LOGO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_MTC_LOGO.TabIndex = 0
        Me.PB_MTC_LOGO.TabStop = False
        '
        'BW_find_cond
        '
        '
        'chkSaveCOnDuringSetup
        '
        Me.chkSaveCOnDuringSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkSaveCOnDuringSetup.AutoSize = True
        Me.chkSaveCOnDuringSetup.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSaveCOnDuringSetup.Location = New System.Drawing.Point(459, 581)
        Me.chkSaveCOnDuringSetup.Name = "chkSaveCOnDuringSetup"
        Me.chkSaveCOnDuringSetup.Size = New System.Drawing.Size(204, 23)
        Me.chkSaveCOnDuringSetup.TabIndex = 66
        Me.chkSaveCOnDuringSetup.Text = "Save Cycle ON during SETUP"
        Me.chkSaveCOnDuringSetup.UseVisualStyleBackColor = True
        '
        'Adv_MTC_cond_edit
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.ClientSize = New System.Drawing.Size(1499, 1024)
        Me.Controls.Add(Me.TableLayout_TOP)
        Me.Controls.Add(Me.SplitContainer1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(936, 47)
        Me.Name = "Adv_MTC_cond_edit"
        Me.Text = "MTConnect Condition Editor"
        Me.TV_context_menu.ResumeLayout(False)
        CType(Me.dgridConditions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.PB_processing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TGV_MTC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TB_tabs.ResumeLayout(False)
        Me.page_status.ResumeLayout(False)
        Me.page_status.PerformLayout()
        CType(Me.nudDelayForCycleOff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudDelay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.page_notif.ResumeLayout(False)
        CType(Me.dgviewNotifConditions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc_nOTIF.Panel1.ResumeLayout(False)
        Me.sc_nOTIF.Panel1.PerformLayout()
        Me.sc_nOTIF.Panel2.ResumeLayout(False)
        Me.sc_nOTIF.Panel2.PerformLayout()
        CType(Me.sc_nOTIF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sc_nOTIF.ResumeLayout(False)
        CType(Me.dgviewNotificationStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_processing_notif, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_COND_notif, System.ComponentModel.ISupportInitialize).EndInit()
        Me.page_notif_dest.ResumeLayout(False)
        Me.page_notif_dest.PerformLayout()
        CType(Me.DGV_sendto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.page_other.ResumeLayout(False)
        Me.page_other.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.panelOther.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.btnRefreshPartNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOthers.ResumeLayout(False)
        Me.grpOthers.PerformLayout()
        Me.grpPallet.ResumeLayout(False)
        Me.grpPallet.PerformLayout()
        CType(Me.numMCSDelay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefreshPallet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.btnRefreshProgNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayout_TOP.ResumeLayout(False)
        Me.Panel_MTC_logo.ResumeLayout(False)
        CType(Me.PB_CSIF_LOGO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel_Status.ResumeLayout(False)
        Me.Panel_Status.PerformLayout()
        CType(Me.PB_status, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel_CSIF_logo.ResumeLayout(False)
        Me.Panel_CSIF_logo.PerformLayout()
        CType(Me.pbFocas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_MTC_LOGO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PB_MTC_LOGO As PictureBox
    Friend WithEvents PB_CSIF_LOGO As PictureBox
    Friend WithEvents TV_MTC As TreeView
    Friend WithEvents dgridConditions As DataGridView
    Friend WithEvents BG_filltree As System.ComponentModel.BackgroundWorker
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents LBL_man_edit As Label
    Friend WithEvents txtExpression As TextBox
    Friend WithEvents lbl_Result As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LBL_title As Label
    Friend WithEvents cmbStatus As ComboBox
    Friend WithEvents BTN_Refresh As Button
    Friend WithEvents LB_current_selected_values As ListBox
    Friend WithEvents Label3 As Label
    Friend WithEvents BW_refresh_Status As System.ComponentModel.BackgroundWorker
    Friend WithEvents BW_refresh_tree As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer_threads As Timer
    Friend WithEvents Label4 As Label
    Friend WithEvents LV_Property As ListView
    Friend WithEvents Value As ColumnHeader
    Friend WithEvents TV_context_menu As ContextMenuStrip
    Friend WithEvents Associate As ToolStripMenuItem
    Friend WithEvents Addcond As ToolStripMenuItem
    Friend WithEvents Partno As ToolStripMenuItem
    Friend WithEvents Programno As ToolStripMenuItem
    Friend WithEvents Feedrate As ToolStripMenuItem
    Friend WithEvents Spindle As ToolStripMenuItem
    Friend WithEvents Add As Button
    Friend WithEvents LBL_delay As Label
    Friend WithEvents nudDelay As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents TableLayout_TOP As TableLayoutPanel
    Friend WithEvents Panel_MTC_logo As Panel
    Friend WithEvents Panel_Status As Panel
    Friend WithEvents Panel_CSIF_logo As Panel
    Friend WithEvents TGV_MTC As AdvancedDataGridView.TreeGridView
    Friend WithEvents Value__ As DataGridViewTextBoxColumn
    Friend WithEvents Device_response As AdvancedDataGridView.TreeGridColumn
    Friend WithEvents BTN_del As Button
    Friend WithEvents PB_processing As PictureBox
    Private WithEvents Parameter As ColumnHeader
    Friend WithEvents PB_status As PictureBox
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TB_tabs As TabControl
    Friend WithEvents Label6 As Label
    Friend WithEvents lblProgramNumber As Label
    Friend WithEvents lblPartNumber As Label
    Friend WithEvents lblFovr As Label
    Friend WithEvents lblSovr As Label
    Friend WithEvents LBL_RETR As Label
    Friend WithEvents sc_nOTIF As SplitContainer
    Friend WithEvents LBL_NOTIFONSTATUS As Label
    Friend WithEvents dgviewNotificationStatus As DataGridView
    Friend WithEvents DataGridViewComboBoxColumn1 As DataGridViewCheckBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents Delay As DataGridViewTextBoxColumn
    Friend WithEvents Label7 As Label
    Friend WithEvents DGV_COND_notif As DataGridView
    Friend WithEvents Condition As DataGridViewTextBoxColumn
    Friend WithEvents Warning As DataGridViewCheckBoxColumn
    Friend WithEvents Fault As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents PB_processing_notif As PictureBox
    Friend WithEvents BW_find_cond As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents DGV_sendto As DataGridView
    Friend WithEvents TB_STATUS_TEXT As TextBox
    Friend WithEvents Activate As DataGridViewCheckBoxColumn
    Friend WithEvents sendto As DataGridViewTextBoxColumn
    Friend WithEvents Label15 As Label
    Friend WithEvents TB_Cond_TEXT As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents BTN_Config_email As Button
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents btnClearPartNumber As Button
    Friend WithEvents LBL_suffix As Label
    Friend WithEvents txtPNrFilter1End As TextBox
    Friend WithEvents LBL_prefix As Label
    Friend WithEvents txtPNrFilter1Start As TextBox
    Friend WithEvents btnClearSpindle As Button
    Friend WithEvents btnClearFeedrate As Button
    Friend WithEvents btnClearProgramNumber As Button
    Friend WithEvents lgc_opr As DataGridViewComboBoxColumn
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents txtPNrOutput As TextBox
    Friend WithEvents txtInputPartNumber As TextBox
    Friend WithEvents btnRefreshPartNumber As PictureBox
    Friend WithEvents btnClearRapid As Button
    Friend WithEvents lblRovr As Label
    Friend WithEvents Rapid As ToolStripMenuItem
    Friend WithEvents BTN_RapidOK As Button
    Friend WithEvents BTN_SpindleOK As Button
    Friend WithEvents BTN_FeedrateOK As Button
    Friend WithEvents BTN_FeedrateEdit As Button
    Friend WithEvents BTN_RapidEdit As Button
    Friend WithEvents BTN_SpindleEdit As Button
    Friend WithEvents txtMaxRovr As TextBox
    Friend WithEvents txtMinRovr As TextBox
    Friend WithEvents txtMaxSovr As TextBox
    Friend WithEvents txtMinSovr As TextBox
    Friend WithEvents txtMaxFovr As TextBox
    Friend WithEvents txtMinFovr As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Public WithEvents page_notif As TabPage
    Public WithEvents page_notif_dest As TabPage
    Friend WithEvents LB_MachineAddress As Label
    Private WithEvents page_status As TabPage
    Friend WithEvents btnRefreshProgNum As PictureBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents txtOutputProgrNumber As TextBox
    Friend WithEvents txtInputProgrNumber As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents txtEndProgNumber As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txtStartProgNumber As TextBox
    Friend WithEvents pbFocas As PictureBox
    Friend WithEvents btnClearPartsCount As Button
    Friend WithEvents lblPartsCount As Label
    Friend WithEvents btnClearRequiredParts As Button
    Friend WithEvents lblRequiredParts As Label
    Friend WithEvents RequiredPartsMenuItem As ToolStripMenuItem
    Friend WithEvents PartCountMenuItem As ToolStripMenuItem
    Friend WithEvents btnSave As Button
    Friend WithEvents PalletMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents grpPallet As GroupBox
    Friend WithEvents txtInputPallet As TextBox
    Friend WithEvents lblActivePallet As Label
    Friend WithEvents btnClearPallet As Button
    Friend WithEvents txtStartPallet As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents btnRefreshPallet As PictureBox
    Friend WithEvents txtEndPallet As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents txtOutputPallet As TextBox
    Friend WithEvents lblMonitoringUnit As Label
    Friend WithEvents grpOthers As GroupBox
    Friend WithEvents chkSendPallet As CheckBox
    Friend WithEvents chkCsdOnSetup As CheckBox
    Friend WithEvents btnDisableStatus As Button
    Friend WithEvents cmbTimeDelay As ComboBox
    Friend WithEvents lblStatusDisabled As Label
    Friend WithEvents page_other As TabPage
    Friend WithEvents panelOther As Panel
    Friend WithEvents txtCriticalTemperature As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents txtCriticalPressure As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents txtWarningTemperature As TextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents txtWarningPressure As TextBox
    Friend WithEvents Label27 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents chkEnableCriticalStop As CheckBox
    Friend WithEvents cmbMCSScale As ComboBox
    Friend WithEvents Label31 As Label
    Friend WithEvents numMCSDelay As NumericUpDown
    Friend WithEvents txtOperationStart As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents txtOperationEnd As TextBox
    Friend WithEvents Label34 As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents chkSaveDataraw As CheckBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents chkSaveRawDuringSetup As CheckBox
    Friend WithEvents chkSaveRawDuringProd As CheckBox
    Friend WithEvents txtPNrPrefix1 As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents txtPNrFilter3Start As TextBox
    Friend WithEvents Label37 As Label
    Friend WithEvents txtPNrFilter3End As TextBox
    Friend WithEvents txtPNrFilter2Start As TextBox
    Friend WithEvents Label35 As Label
    Friend WithEvents txtPNrFilter2End As TextBox
    Friend WithEvents txtPNrPrefix2 As TextBox
    Friend WithEvents Label40 As Label
    Friend WithEvents Label38 As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents chkFilter3 As CheckBox
    Friend WithEvents chkFilter2 As CheckBox
    Friend WithEvents txtPNrPrefix3 As TextBox
    Friend WithEvents Label41 As Label
    Friend WithEvents cmbDelayForCycleOffScale As ComboBox
    Friend WithEvents Label42 As Label
    Friend WithEvents nudDelayForCycleOff As NumericUpDown
    Friend WithEvents txtOperationOutput As TextBox
    Friend WithEvents Label43 As Label
    Friend WithEvents chkOperation As CheckBox
    Friend WithEvents dgviewNotifConditions As DataGridView
    Friend WithEvents DataGridViewComboBoxColumn2 As DataGridViewComboBoxColumn
    Friend WithEvents chkSaveCOnDuringSetup As CheckBox
End Class

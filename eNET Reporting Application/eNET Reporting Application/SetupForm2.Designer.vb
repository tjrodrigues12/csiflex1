<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SetupForm2
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All Machines")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("User Types")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Machines Groups")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Devices")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Groups of machines")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetupForm2))
        Me.CMS_GridEdit = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.add_menu = New System.Windows.Forms.ToolStripMenuItem()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.BgWorker_CreateDB = New System.ComponentModel.BackgroundWorker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.TSSL_DBCreation = New System.Windows.Forms.ToolStripStatusLabel()
        Me.timerServiceState = New System.Windows.Forms.Timer(Me.components)
        Me.BgWorker_importDB = New System.ComponentModel.BackgroundWorker()
        Me.CMS_GroupEdit_NEW = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteMachineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tablePageGeneral = New System.Windows.Forms.TabPage()
        Me.tablePageEnet = New System.Windows.Forms.TabPage()
        Me.tablePageUsers = New System.Windows.Forms.TabPage()
        Me.tablePageDashboards = New System.Windows.Forms.TabPage()
        Me.tablePageCSICon = New System.Windows.Forms.TabPage()
        Me.tablePageCSIConn = New System.Windows.Forms.TabPage()
        Me.tablePagePartNumber = New System.Windows.Forms.TabPage()
        Me.tablePageLicense = New System.Windows.Forms.TabPage()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.treeviewMachines = New System.Windows.Forms.TreeView()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.btnAddUser = New System.Windows.Forms.Button()
        Me.treeViewUsers = New System.Windows.Forms.TreeView()
        Me.GB_Users = New System.Windows.Forms.GroupBox()
        Me.gboxUserPermitions = New System.Windows.Forms.GroupBox()
        Me.chkEditTimeline = New System.Windows.Forms.CheckBox()
        Me.chkEditPartNumber = New System.Windows.Forms.CheckBox()
        Me.txtDisplayName = New System.Windows.Forms.TextBox()
        Me.lblUserPermitions = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.btnEditPassword = New System.Windows.Forms.PictureBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.btnShowPassword = New System.Windows.Forms.PictureBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtUserExtention = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtUserDept = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUserTitle = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtUserRefID = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtUserEmail = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.btnSaveUser = New System.Windows.Forms.Button()
        Me.cmbUserType = New System.Windows.Forms.ComboBox()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.RTB_liste = New System.Windows.Forms.RichTextBox()
        Me.aboutLogoPictBox = New System.Windows.Forms.PictureBox()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnLicenseRequest = New System.Windows.Forms.Button()
        Me.btnMonitoringUnits = New System.Windows.Forms.Button()
        Me.btnDeleteConnector = New System.Windows.Forms.Button()
        Me.btnEditConnector = New System.Windows.Forms.Button()
        Me.grvConnectors = New System.Windows.Forms.DataGridView()
        Me.btnAddConnector = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TV_LivestatusMachine = New System.Windows.Forms.TreeView()
        Me.BTN_gensett = New System.Windows.Forms.Button()
        Me.Panel_dashConfig = New System.Windows.Forms.Panel()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.RSS_GB = New System.Windows.Forms.GroupBox()
        Me.chkRSSMessageOnOff = New System.Windows.Forms.CheckBox()
        Me.lblBestCycleOnMachine = New System.Windows.Forms.Label()
        Me.btnRSSValidate = New System.Windows.Forms.Button()
        Me.txtRSSUserMessageText = New System.Windows.Forms.TextBox()
        Me.lblMessageText = New System.Windows.Forms.Label()
        Me.grpRSSMessageType = New System.Windows.Forms.GroupBox()
        Me.rdbRSSSystemMessage = New System.Windows.Forms.RadioButton()
        Me.rdbRSSUserMessage = New System.Windows.Forms.RadioButton()
        Me.btnRSSDeleteMessage = New System.Windows.Forms.Button()
        Me.btnRSSAddMessage = New System.Windows.Forms.Button()
        Me.btnRSSMessageDown = New System.Windows.Forms.Button()
        Me.btnRSSMessageUp = New System.Windows.Forms.Button()
        Me.dgvRSSMessages = New System.Windows.Forms.DataGridView()
        Me.Messages = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Priority = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpRSSBestCycleOn = New System.Windows.Forms.GroupBox()
        Me.rdbRSSMonth = New System.Windows.Forms.RadioButton()
        Me.rdbRSSWeek = New System.Windows.Forms.RadioButton()
        Me.rdbRSSDay = New System.Windows.Forms.RadioButton()
        Me.chkRSSBestCycleOn = New System.Windows.Forms.CheckBox()
        Me.btnAdvSettings = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.IF_OFF = New System.Windows.Forms.RadioButton()
        Me.IF_Rot_CB = New System.Windows.Forms.CheckBox()
        Me.IF_ON = New System.Windows.Forms.RadioButton()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Url_TB = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.IFFullscreen_CB = New System.Windows.Forms.CheckBox()
        Me.BTN_ping = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CB__DeviceType = New System.Windows.Forms.ComboBox()
        Me.CommonControls_GB = New System.Windows.Forms.GroupBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.chkDarkTheme = New System.Windows.Forms.CheckBox()
        Me.LSFullscreen_CB = New System.Windows.Forms.CheckBox()
        Me.Sec_TB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LSD_TB = New System.Windows.Forms.CheckBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.chkLogoBarHidden = New System.Windows.Forms.CheckBox()
        Me.Logo_TB = New System.Windows.Forms.CheckBox()
        Me.txtLogoPath = New System.Windows.Forms.TextBox()
        Me.Browse_Logo = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblTemperature = New System.Windows.Forms.Label()
        Me.City_TB = New System.Windows.Forms.TextBox()
        Me.Temp_CB = New System.Windows.Forms.CheckBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Degree_CB = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TB_format = New System.Windows.Forms.TextBox()
        Me.DateTime_TB = New System.Windows.Forms.CheckBox()
        Me.Label_format = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.IP_TB = New System.Windows.Forms.TextBox()
        Me.txtDeviceName = New System.Windows.Forms.TextBox()
        Me.treeviewDevices = New System.Windows.Forms.TreeView()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.btnPerformanceTuning = New System.Windows.Forms.Button()
        Me.btnPerformanceClear = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.LBL_IPChange = New System.Windows.Forms.Label()
        Me.LBL_srvResult = New System.Windows.Forms.Label()
        Me.btnCheckWebServer = New System.Windows.Forms.Button()
        Me.btnSaveWebServerPort = New System.Windows.Forms.Button()
        Me.btnChangeIpAddress = New System.Windows.Forms.Button()
        Me.LBL_IP = New System.Windows.Forms.Label()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.btnStartMBServer = New System.Windows.Forms.PictureBox()
        Me.lblServiceState = New System.Windows.Forms.Label()
        Me.lblStartService = New System.Windows.Forms.Label()
        Me.btnStartServ = New System.Windows.Forms.PictureBox()
        Me.btnStopServ = New System.Windows.Forms.PictureBox()
        Me.lblReportServiceState = New System.Windows.Forms.Label()
        Me.btnStartRepService = New System.Windows.Forms.PictureBox()
        Me.btnStopRepService = New System.Windows.Forms.PictureBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.lblMBServer = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.cmbStartWeek = New System.Windows.Forms.ComboBox()
        Me.btnImportSettings = New System.Windows.Forms.Button()
        Me.btnConfigureReinmeter = New System.Windows.Forms.Button()
        Me.btnLoadingCycleOn = New System.Windows.Forms.Button()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.btnConfigureEmail = New System.Windows.Forms.Button()
        Me.btnConfigureReports = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnDefaultEnetFolder = New System.Windows.Forms.Button()
        Me.btnBrowserEnetFolder = New System.Windows.Forms.Button()
        Me.btnReloadEnetSettings = New System.Windows.Forms.Button()
        Me.txtGeneralEnetFolder = New System.Windows.Forms.TextBox()
        Me.TabControl_DashBoard = New System.Windows.Forms.TabControl()
        Me.pnlMachines = New System.Windows.Forms.Panel()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.cmbImportGroups = New System.Windows.Forms.ComboBox()
        Me.lblQttMachines = New System.Windows.Forms.Label()
        Me.chkEnetAllPositions = New System.Windows.Forms.CheckBox()
        Me.gboxMachineSettings = New System.Windows.Forms.GroupBox()
        Me.pnlMachineSettings = New System.Windows.Forms.Panel()

        Me.labelMachineId = New System.Windows.Forms.Label()
        Me.lblMachineId = New System.Windows.Forms.Label()
        Me.labelMachineName = New System.Windows.Forms.Label()
        Me.lblMachineName = New System.Windows.Forms.Label()
        Me.labelEnetPos = New System.Windows.Forms.Label()
        Me.lblEnetPos = New System.Windows.Forms.Label()
        Me.labelDepartment = New System.Windows.Forms.Label()
        Me.lblDepartment = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.lblProtocol = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.lblFtpFileName = New System.Windows.Forms.Label()
        Me.labelAlwaysRecCycleOn = New System.Windows.Forms.Label()
        Me.lblAlwaysRecCycleOn = New System.Windows.Forms.Label()

        Me.labelCycleOnCommand = New System.Windows.Forms.Label()
        Me.lblCycleOnCommand = New System.Windows.Forms.Label()
        Me.labelCycleOffCommand = New System.Windows.Forms.Label()
        Me.lblCycleOffCommand = New System.Windows.Forms.Label()
        Me.labelPartNumberCommand = New System.Windows.Forms.Label()
        Me.lblPartNumberCommand = New System.Windows.Forms.Label()

        Me.btnEnetSettings = New System.Windows.Forms.Button()
        Me.gridviewMachines = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Display = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Machines = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.enetMachineName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MachineLabel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Source = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Check = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Connection = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dailytarget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Weeklytarget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Monthlytarget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IsMonitored = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.treeviewGroupsOfMachines = New System.Windows.Forms.TreeView()
        Me.grpEnetSettings = New System.Windows.Forms.GroupBox()
        Me.btnEnetCancelChanges = New System.Windows.Forms.Button()
        Me.lblEnetFolderStatus = New System.Windows.Forms.Label()
        Me.btnEnetSaveChanges = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnLoadEnetHistory = New System.Windows.Forms.Button()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.lstEnetYears = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblEnetCheckResult = New System.Windows.Forms.Label()
        Me.btnCheckEnetHttpIp = New System.Windows.Forms.Button()
        Me.lstEnetMachines = New System.Windows.Forms.ListView()
        Me.Column1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtEnetHttpIp = New System.Windows.Forms.TextBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtEnetFolder = New System.Windows.Forms.TextBox()
        Me.btnEnetFolderBrowser = New System.Windows.Forms.Button()
        Me.btnEnetFolderDefault = New System.Windows.Forms.Button()
        Me.IM_tab = New System.Windows.Forms.ImageList(Me.components)
        Me.BG_updates = New System.ComponentModel.BackgroundWorker()
        Me.BG_DL_update = New System.ComponentModel.BackgroundWorker()
        Me.BG_installUdate = New System.ComponentModel.BackgroundWorker()
        Me.BG_checkfilesize = New System.ComponentModel.BackgroundWorker()
        Me.btnStopMBServer = New System.Windows.Forms.PictureBox()
        Me.CMS_GridEdit.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.CMS_GroupEdit_NEW.SuspendLayout()
        Me.tablePageUsers.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GB_Users.SuspendLayout()
        CType(Me.btnEditPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tablePageLicense.SuspendLayout()
        CType(Me.aboutLogoPictBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tablePageCSICon.SuspendLayout()
        CType(Me.grvConnectors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tablePageDashboards.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel_dashConfig.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RSS_GB.SuspendLayout()
        Me.grpRSSMessageType.SuspendLayout()
        CType(Me.dgvRSSMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRSSBestCycleOn.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.CommonControls_GB.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.tablePageGeneral.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox11.SuspendLayout()
        CType(Me.btnStartMBServer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnStartServ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnStopServ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnStartRepService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnStopRepService, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl_DashBoard.SuspendLayout()
        Me.tablePageEnet.SuspendLayout()
        Me.pnlMachines.SuspendLayout()
        Me.gboxMachineSettings.SuspendLayout()
        Me.pnlMachineSettings.SuspendLayout()
        CType(Me.gridviewMachines, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpEnetSettings.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnStopMBServer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CMS_GridEdit
        '
        Me.CMS_GridEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem
        Me.CMS_GridEdit.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.CMS_GridEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.add_menu})
        Me.CMS_GridEdit.Name = "ContextMenuStrip1"
        Me.CMS_GridEdit.Size = New System.Drawing.Size(200, 144)
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Image = Global.CSI_Reporting_Application.My.Resources.Resources.setup
        Me.EditToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(199, 70)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'add_menu
        '
        Me.add_menu.Image = Global.CSI_Reporting_Application.My.Resources.Resources.MergeJoin_64x64
        Me.add_menu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.add_menu.Name = "add_menu"
        Me.add_menu.Size = New System.Drawing.Size(199, 70)
        Me.add_menu.Text = "Add to group :"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'BgWorker_CreateDB
        '
        Me.BgWorker_CreateDB.WorkerSupportsCancellation = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSSL_DBCreation})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 859)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1233, 22)
        Me.StatusStrip1.TabIndex = 16
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'TSSL_DBCreation
        '
        Me.TSSL_DBCreation.Name = "TSSL_DBCreation"
        Me.TSSL_DBCreation.Size = New System.Drawing.Size(0, 17)
        '
        'timerServiceState
        '
        Me.timerServiceState.Enabled = True
        Me.timerServiceState.Interval = 800
        '
        'BgWorker_importDB
        '
        Me.BgWorker_importDB.WorkerSupportsCancellation = True
        '
        'CMS_GroupEdit_NEW
        '
        Me.CMS_GroupEdit_NEW.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.CMS_GroupEdit_NEW.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddGroupToolStripMenuItem, Me.RenameGroupToolStripMenuItem, Me.DeleteGroupToolStripMenuItem, Me.DeleteMachineToolStripMenuItem})
        Me.CMS_GroupEdit_NEW.Name = "CMS_GroupEdit_NEW"
        Me.CMS_GroupEdit_NEW.Size = New System.Drawing.Size(167, 92)
        '
        'AddGroupToolStripMenuItem
        '
        Me.AddGroupToolStripMenuItem.Name = "AddGroupToolStripMenuItem"
        Me.AddGroupToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.AddGroupToolStripMenuItem.Text = "Add group"
        '
        'RenameGroupToolStripMenuItem
        '
        Me.RenameGroupToolStripMenuItem.Name = "RenameGroupToolStripMenuItem"
        Me.RenameGroupToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.RenameGroupToolStripMenuItem.Text = "Rename group"
        '
        'DeleteGroupToolStripMenuItem
        '
        Me.DeleteGroupToolStripMenuItem.Name = "DeleteGroupToolStripMenuItem"
        Me.DeleteGroupToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.DeleteGroupToolStripMenuItem.Text = "Delete group"
        '
        'DeleteMachineToolStripMenuItem
        '
        Me.DeleteMachineToolStripMenuItem.Name = "DeleteMachineToolStripMenuItem"
        Me.DeleteMachineToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.DeleteMachineToolStripMenuItem.Text = "Remove machine"
        '
        'tablePageCSIConn
        '
        Me.tablePageCSIConn.Location = New System.Drawing.Point(4, 30)
        Me.tablePageCSIConn.Name = "tablePageCSIConn"
        Me.tablePageCSIConn.Size = New System.Drawing.Size(1021, 586)
        Me.tablePageCSIConn.TabIndex = 13
        Me.tablePageCSIConn.Text = "CSI Connector"
        Me.tablePageCSIConn.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button3.Enabled = False
        Me.Button3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(950, 826)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 30)
        Me.Button3.TabIndex = 15
        Me.Button3.Text = "OK"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'tablePageUsers
        '
        Me.tablePageUsers.AutoScroll = True
        Me.tablePageUsers.Controls.Add(Me.GroupBox10)
        Me.tablePageUsers.Controls.Add(Me.GroupBox7)
        Me.tablePageUsers.Controls.Add(Me.GB_Users)
        Me.tablePageUsers.Location = New System.Drawing.Point(4, 59)
        Me.tablePageUsers.Name = "tablePageUsers"
        Me.tablePageUsers.Padding = New System.Windows.Forms.Padding(3)
        Me.tablePageUsers.Size = New System.Drawing.Size(1225, 793)
        Me.tablePageUsers.TabIndex = 14
        Me.tablePageUsers.Text = "Users"
        Me.tablePageUsers.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox10.Controls.Add(Me.treeviewMachines)
        Me.GroupBox10.Location = New System.Drawing.Point(1003, 6)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(216, 781)
        Me.GroupBox10.TabIndex = 20
        Me.GroupBox10.TabStop = False
        '
        'treeviewMachines
        '
        Me.treeviewMachines.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.treeviewMachines.CheckBoxes = True
        Me.treeviewMachines.Location = New System.Drawing.Point(6, 18)
        Me.treeviewMachines.MinimumSize = New System.Drawing.Size(151, 4)
        Me.treeviewMachines.Name = "treeviewMachines"
        TreeNode1.Name = "Machines Groupes"
        TreeNode1.NodeFont = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        TreeNode1.Text = "All Machines"
        Me.treeviewMachines.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1})
        Me.treeviewMachines.Size = New System.Drawing.Size(204, 757)
        Me.treeviewMachines.TabIndex = 16
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox7.Controls.Add(Me.btnAddUser)
        Me.GroupBox7.Controls.Add(Me.treeViewUsers)
        Me.GroupBox7.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(200, 781)
        Me.GroupBox7.TabIndex = 19
        Me.GroupBox7.TabStop = False
        '
        'btnAddUser
        '
        Me.btnAddUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddUser.BackColor = System.Drawing.Color.Transparent
        Me.btnAddUser.BackgroundImage = Global.CSI_Reporting_Application.My.Resources.Resources.icon_plus_circle_64
        Me.btnAddUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddUser.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddUser.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnAddUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAddUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddUser.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.btnAddUser.Location = New System.Drawing.Point(153, 19)
        Me.btnAddUser.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(40, 40)
        Me.btnAddUser.TabIndex = 18
        Me.btnAddUser.UseVisualStyleBackColor = False
        '
        'treeViewUsers
        '
        Me.treeViewUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.treeViewUsers.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.treeViewUsers.HideSelection = False
        Me.treeViewUsers.Location = New System.Drawing.Point(6, 18)
        Me.treeViewUsers.Name = "treeViewUsers"
        TreeNode2.Name = "Users"
        TreeNode2.NodeFont = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        TreeNode2.Text = "User Types"
        Me.treeViewUsers.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode2})
        Me.treeViewUsers.Size = New System.Drawing.Size(188, 757)
        Me.treeViewUsers.TabIndex = 11
        '
        'GB_Users
        '
        Me.GB_Users.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GB_Users.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.GB_Users.Controls.Add(Me.txtDisplayName)
        Me.GB_Users.Controls.Add(Me.Label42)
        Me.GB_Users.Controls.Add(Me.btnEditPassword)
        Me.GB_Users.Controls.Add(Me.Label21)
        Me.GB_Users.Controls.Add(Me.btnShowPassword)
        Me.GB_Users.Controls.Add(Me.txtPassword)
        Me.GB_Users.Controls.Add(Me.Label25)
        Me.GB_Users.Controls.Add(Me.Label18)
        Me.GB_Users.Controls.Add(Me.Label16)
        Me.GB_Users.Controls.Add(Me.Label13)
        Me.GB_Users.Controls.Add(Me.Label11)
        Me.GB_Users.Controls.Add(Me.Label10)
        Me.GB_Users.Controls.Add(Me.txtUserExtention)
        Me.GB_Users.Controls.Add(Me.Label5)
        Me.GB_Users.Controls.Add(Me.txtUserDept)
        Me.GB_Users.Controls.Add(Me.Label1)
        Me.GB_Users.Controls.Add(Me.txtUserTitle)
        Me.GB_Users.Controls.Add(Me.Label4)
        Me.GB_Users.Controls.Add(Me.txtUserRefID)
        Me.GB_Users.Controls.Add(Me.Label32)
        Me.GB_Users.Controls.Add(Me.txtUserEmail)
        Me.GB_Users.Controls.Add(Me.Label31)
        Me.GB_Users.Controls.Add(Me.btnSaveUser)
        Me.GB_Users.Controls.Add(Me.cmbUserType)
        Me.GB_Users.Controls.Add(Me.txtLastName)
        Me.GB_Users.Controls.Add(Me.txtFirstName)
        Me.GB_Users.Controls.Add(Me.txtUserName)
        Me.GB_Users.Controls.Add(Me.Label30)
        Me.GB_Users.Controls.Add(Me.Label29)
        Me.GB_Users.Controls.Add(Me.Label26)
        Me.GB_Users.Controls.Add(Me.Label9)
        Me.GB_Users.Controls.Add(Me.gboxUserPermitions)
        Me.GB_Users.Location = New System.Drawing.Point(214, 6)
        Me.GB_Users.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.GB_Users.Name = "GB_Users"
        Me.GB_Users.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.GB_Users.Size = New System.Drawing.Size(783, 781)
        Me.GB_Users.TabIndex = 17
        Me.GB_Users.TabStop = False
        Me.GB_Users.Visible = False
        '
        'gboxUserPermitions
        '
        Me.gboxUserPermitions.Controls.Add(Me.lblUserPermitions)
        Me.gboxUserPermitions.Controls.Add(Me.chkEditTimeline)
        Me.gboxUserPermitions.Controls.Add(Me.chkEditPartNumber)
        Me.gboxUserPermitions.Location = New Point(10, 360)
        Me.gboxUserPermitions.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.gboxUserPermitions.Name = "gboxUserPermitions"
        Me.gboxUserPermitions.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.gboxUserPermitions.Size = New System.Drawing.Size(760, 120)
        '
        'lblUserPermitions
        '
        Me.lblUserPermitions.Font = New System.Drawing.Font("Segoe UI", 12.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserPermitions.Location = New System.Drawing.Point(15, 15)
        Me.lblUserPermitions.Name = "lblUserPermitions"
        Me.lblUserPermitions.Size = New System.Drawing.Size(115, 30)
        Me.lblUserPermitions.TabIndex = 24
        Me.lblUserPermitions.Text = "Permissions"
        Me.lblUserPermitions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkEditTimeline
        '
        Me.chkEditTimeline.AutoSize = True
        Me.chkEditTimeline.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.chkEditTimeline.Location = New System.Drawing.Point(145, 50)
        Me.chkEditTimeline.Name = "chkEditTimeline"
        Me.chkEditTimeline.Size = New System.Drawing.Size(154, 19)
        Me.chkEditTimeline.TabIndex = 57
        Me.chkEditTimeline.Text = "Allowed to edit Timeline"
        Me.chkEditTimeline.UseVisualStyleBackColor = True
        '
        'chkEditPartNumber
        '
        Me.chkEditPartNumber.AutoSize = True
        Me.chkEditPartNumber.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.chkEditPartNumber.Location = New System.Drawing.Point(145, 80)
        Me.chkEditPartNumber.Name = "chkEditPartNumber"
        Me.chkEditPartNumber.Size = New System.Drawing.Size(154, 19)
        Me.chkEditPartNumber.TabIndex = 57
        Me.chkEditPartNumber.Text = "Allowed to edit Master Part Number"
        Me.chkEditPartNumber.UseVisualStyleBackColor = True
        '
        'txtDisplayName
        '
        Me.txtDisplayName.Location = New System.Drawing.Point(516, 206)
        Me.txtDisplayName.Name = "txtDisplayName"
        Me.txtDisplayName.Size = New System.Drawing.Size(200, 29)
        Me.txtDisplayName.TabIndex = 6
        '
        'Label42
        '
        Me.Label42.Location = New System.Drawing.Point(399, 209)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(115, 21)
        Me.Label42.TabIndex = 24
        Me.Label42.Text = "Display Name :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnEditPassword
        '
        Me.btnEditPassword.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEditPassword.Image = Global.CSI_Reporting_Application.My.Resources.Resources.pencil_draw_edit_sketch_tool_3d459782aa34d16b_512x512
        Me.btnEditPassword.Location = New System.Drawing.Point(360, 88)
        Me.btnEditPassword.Name = "btnEditPassword"
        Me.btnEditPassword.Size = New System.Drawing.Size(29, 29)
        Me.btnEditPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnEditPassword.TabIndex = 23
        Me.btnEditPassword.TabStop = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.ForeColor = System.Drawing.Color.Red
        Me.Label21.Location = New System.Drawing.Point(14, 91)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(17, 21)
        Me.Label21.TabIndex = 21
        Me.Label21.Text = "*"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnShowPassword
        '
        Me.btnShowPassword.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnShowPassword.Location = New System.Drawing.Point(395, 98)
        Me.btnShowPassword.Name = "btnShowPassword"
        Me.btnShowPassword.Size = New System.Drawing.Size(29, 29)
        Me.btnShowPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnShowPassword.TabIndex = 22
        Me.btnShowPassword.TabStop = False
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(154, 88)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(200, 29)
        Me.txtPassword.TabIndex = 2
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(30, 91)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(120, 21)
        Me.Label25.TabIndex = 1
        Me.Label25.Text = "Password :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(14, 32)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(17, 21)
        Me.Label18.TabIndex = 20
        Me.Label18.Text = "*"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ForeColor = System.Drawing.Color.Red
        Me.Label16.Location = New System.Drawing.Point(372, 32)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(17, 21)
        Me.Label16.TabIndex = 19
        Me.Label16.Text = "*"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.Red
        Me.Label13.Location = New System.Drawing.Point(14, 151)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(17, 21)
        Me.Label13.TabIndex = 18
        Me.Label13.Text = "*"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(376, 151)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(17, 21)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "*"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(14, 327)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(17, 21)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "*"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label10.Visible = False
        '
        'txtUserExtention
        '
        Me.txtUserExtention.Location = New System.Drawing.Point(516, 324)
        Me.txtUserExtention.Name = "txtUserExtention"
        Me.txtUserExtention.Size = New System.Drawing.Size(200, 29)
        Me.txtUserExtention.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(395, 327)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(115, 21)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Phone Ext. :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUserDept
        '
        Me.txtUserDept.Location = New System.Drawing.Point(516, 265)
        Me.txtUserDept.Name = "txtUserDept"
        Me.txtUserDept.Size = New System.Drawing.Size(200, 29)
        Me.txtUserDept.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(395, 268)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 21)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Dept :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUserTitle
        '
        Me.txtUserTitle.Location = New System.Drawing.Point(154, 265)
        Me.txtUserTitle.Name = "txtUserTitle"
        Me.txtUserTitle.Size = New System.Drawing.Size(200, 29)
        Me.txtUserTitle.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(30, 268)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 21)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Title :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUserRefID
        '
        Me.txtUserRefID.Location = New System.Drawing.Point(154, 206)
        Me.txtUserRefID.Name = "txtUserRefID"
        Me.txtUserRefID.Size = New System.Drawing.Size(200, 29)
        Me.txtUserRefID.TabIndex = 5
        '
        'Label32
        '
        Me.Label32.Location = New System.Drawing.Point(30, 209)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(120, 21)
        Me.Label32.TabIndex = 9
        Me.Label32.Text = "Ref. ID :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUserEmail
        '
        Me.txtUserEmail.Location = New System.Drawing.Point(154, 324)
        Me.txtUserEmail.Name = "txtUserEmail"
        Me.txtUserEmail.Size = New System.Drawing.Size(200, 29)
        Me.txtUserEmail.TabIndex = 9
        '
        'Label31
        '
        Me.Label31.Location = New System.Drawing.Point(30, 327)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(120, 21)
        Me.Label31.TabIndex = 7
        Me.Label31.Text = "Email :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSaveUser
        '
        Me.btnSaveUser.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveUser.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnSaveUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSaveUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSaveUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveUser.Location = New System.Drawing.Point(323, 500)
        Me.btnSaveUser.Name = "btnSaveUser"
        Me.btnSaveUser.Size = New System.Drawing.Size(98, 78)
        Me.btnSaveUser.TabIndex = 11
        Me.btnSaveUser.Text = "Save"
        Me.btnSaveUser.UseVisualStyleBackColor = False
        '
        'cmbUserType
        '
        Me.cmbUserType.AutoCompleteCustomSource.AddRange(New String() {"admin", "supervisor", "operator", "user"})
        Me.cmbUserType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cmbUserType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUserType.FormattingEnabled = True
        Me.cmbUserType.Items.AddRange(New Object() {"Administrator", "Supervisor", "Operator", "User", "Machine"})
        Me.cmbUserType.Location = New System.Drawing.Point(516, 29)
        Me.cmbUserType.Name = "cmbUserType"
        Me.cmbUserType.Size = New System.Drawing.Size(200, 29)
        Me.cmbUserType.TabIndex = 1
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(516, 148)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(200, 29)
        Me.txtLastName.TabIndex = 4
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(154, 147)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(200, 29)
        Me.txtFirstName.TabIndex = 3
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(154, 29)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(200, 29)
        Me.txtUserName.TabIndex = 0
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(395, 32)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(115, 21)
        Me.Label30.TabIndex = 4
        Me.Label30.Text = "User Type :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(399, 151)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(115, 21)
        Me.Label29.TabIndex = 3
        Me.Label29.Text = "Last Name :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.Location = New System.Drawing.Point(30, 150)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(120, 21)
        Me.Label26.TabIndex = 2
        Me.Label26.Text = "First Name :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(30, 32)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(120, 21)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Username :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tablePageLicense
        '
        Me.tablePageLicense.BackColor = System.Drawing.Color.White
        Me.tablePageLicense.Controls.Add(Me.RTB_liste)
        Me.tablePageLicense.Controls.Add(Me.aboutLogoPictBox)
        Me.tablePageLicense.Controls.Add(Me.btnAbout)
        Me.tablePageLicense.Controls.Add(Me.Label8)
        Me.tablePageLicense.Controls.Add(Me.btnLicenseRequest)
        Me.tablePageLicense.Location = New System.Drawing.Point(4, 59)
        Me.tablePageLicense.Name = "tablePageLicense"
        Me.tablePageLicense.Padding = New System.Windows.Forms.Padding(3)
        Me.tablePageLicense.Size = New System.Drawing.Size(1225, 793)
        Me.tablePageLicense.TabIndex = 7
        Me.tablePageLicense.Text = "About/License"
        '
        'RTB_liste
        '
        Me.RTB_liste.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RTB_liste.BackColor = System.Drawing.Color.White
        Me.RTB_liste.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RTB_liste.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RTB_liste.Location = New System.Drawing.Point(341, 38)
        Me.RTB_liste.Name = "RTB_liste"
        Me.RTB_liste.ReadOnly = True
        Me.RTB_liste.Size = New System.Drawing.Size(876, 749)
        Me.RTB_liste.TabIndex = 34
        Me.RTB_liste.Text = ""
        '
        'aboutLogoPictBox
        '
        Me.aboutLogoPictBox.Image = Global.CSI_Reporting_Application.My.Resources.Resources.CSIFLEX_New_Logo_167
        Me.aboutLogoPictBox.Location = New System.Drawing.Point(44, 38)
        Me.aboutLogoPictBox.Name = "aboutLogoPictBox"
        Me.aboutLogoPictBox.Size = New System.Drawing.Size(276, 251)
        Me.aboutLogoPictBox.SizeMode = PictureBoxSizeMode.StretchImage
        Me.aboutLogoPictBox.TabIndex = 33
        Me.aboutLogoPictBox.TabStop = False
        '
        'btnAbout
        '
        Me.btnAbout.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnAbout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAbout.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAbout.Location = New System.Drawing.Point(44, 509)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(276, 75)
        Me.btnAbout.TabIndex = 31
        Me.btnAbout.Text = "About CSIFLEX"
        Me.btnAbout.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(349, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(0, 21)
        Me.Label8.TabIndex = 30
        '
        'btnLicenseRequest
        '
        Me.btnLicenseRequest.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnLicenseRequest.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLicenseRequest.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnLicenseRequest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnLicenseRequest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnLicenseRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLicenseRequest.Location = New System.Drawing.Point(44, 416)
        Me.btnLicenseRequest.Name = "btnLicenseRequest"
        Me.btnLicenseRequest.Size = New System.Drawing.Size(276, 75)
        Me.btnLicenseRequest.TabIndex = 27
        Me.btnLicenseRequest.Text = "License"
        Me.btnLicenseRequest.UseVisualStyleBackColor = False
        '
        'tablePageCSICon
        '
        Me.tablePageCSICon.Controls.Add(Me.btnMonitoringUnits)
        Me.tablePageCSICon.Controls.Add(Me.btnDeleteConnector)
        Me.tablePageCSICon.Controls.Add(Me.btnEditConnector)
        Me.tablePageCSICon.Controls.Add(Me.grvConnectors)
        Me.tablePageCSICon.Controls.Add(Me.btnAddConnector)
        Me.tablePageCSICon.Location = New System.Drawing.Point(4, 59)
        Me.tablePageCSICon.Name = "tablePageCSICon"
        Me.tablePageCSICon.Size = New System.Drawing.Size(1225, 793)
        Me.tablePageCSICon.TabIndex = 13
        Me.tablePageCSICon.Text = "CSI Connector"
        Me.tablePageCSICon.UseVisualStyleBackColor = True
        '
        'btnMonitoringUnits
        '
        Me.btnMonitoringUnits.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMonitoringUnits.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnMonitoringUnits.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnMonitoringUnits.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMonitoringUnits.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnMonitoringUnits.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMonitoringUnits.Location = New System.Drawing.Point(991, 19)
        Me.btnMonitoringUnits.Name = "btnMonitoringUnits"
        Me.btnMonitoringUnits.Size = New System.Drawing.Size(213, 62)
        Me.btnMonitoringUnits.TabIndex = 4
        Me.btnMonitoringUnits.Text = "Monitoring Units"
        Me.btnMonitoringUnits.UseVisualStyleBackColor = False
        '
        'btnDeleteConnector
        '
        Me.btnDeleteConnector.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnDeleteConnector.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnDeleteConnector.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDeleteConnector.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnDeleteConnector.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteConnector.Location = New System.Drawing.Point(348, 19)
        Me.btnDeleteConnector.Name = "btnDeleteConnector"
        Me.btnDeleteConnector.Size = New System.Drawing.Size(158, 62)
        Me.btnDeleteConnector.TabIndex = 3
        Me.btnDeleteConnector.Text = "Delete machine"
        Me.btnDeleteConnector.UseVisualStyleBackColor = False
        '
        'btnEditConnector
        '
        Me.btnEditConnector.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnEditConnector.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnEditConnector.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnEditConnector.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnEditConnector.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditConnector.Location = New System.Drawing.Point(189, 19)
        Me.btnEditConnector.Name = "btnEditConnector"
        Me.btnEditConnector.Size = New System.Drawing.Size(139, 62)
        Me.btnEditConnector.TabIndex = 2
        Me.btnEditConnector.Text = "Edit machine"
        Me.btnEditConnector.UseVisualStyleBackColor = False
        '
        'grvConnectors
        '
        Me.grvConnectors.AllowUserToAddRows = False
        Me.grvConnectors.AllowUserToDeleteRows = False
        Me.grvConnectors.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grvConnectors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grvConnectors.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.grvConnectors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grvConnectors.Location = New System.Drawing.Point(20, 102)
        Me.grvConnectors.MultiSelect = False
        Me.grvConnectors.Name = "grvConnectors"
        Me.grvConnectors.ReadOnly = True
        Me.grvConnectors.Size = New System.Drawing.Size(1184, 675)
        Me.grvConnectors.TabIndex = 1
        '
        'btnAddConnector
        '
        Me.btnAddConnector.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnAddConnector.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnAddConnector.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAddConnector.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnAddConnector.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddConnector.Location = New System.Drawing.Point(20, 19)
        Me.btnAddConnector.Name = "btnAddConnector"
        Me.btnAddConnector.Size = New System.Drawing.Size(147, 62)
        Me.btnAddConnector.TabIndex = 0
        Me.btnAddConnector.Text = "Add machine"
        Me.btnAddConnector.UseVisualStyleBackColor = False
        '
        'tablePageDashboards
        '
        Me.tablePageDashboards.Controls.Add(Me.Panel1)
        Me.tablePageDashboards.Location = New System.Drawing.Point(4, 59)
        Me.tablePageDashboards.Name = "tablePageDashboards"
        Me.tablePageDashboards.Padding = New System.Windows.Forms.Padding(3)
        Me.tablePageDashboards.Size = New System.Drawing.Size(1225, 793)
        Me.tablePageDashboards.TabIndex = 12
        Me.tablePageDashboards.Text = "Dashboards"
        Me.tablePageDashboards.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.AutoScrollMinSize = New System.Drawing.Size(1200, 500)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.TV_LivestatusMachine)
        Me.Panel1.Controls.Add(Me.BTN_gensett)
        Me.Panel1.Controls.Add(Me.Panel_dashConfig)
        Me.Panel1.Controls.Add(Me.treeviewDevices)
        Me.Panel1.Location = New System.Drawing.Point(3, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1218, 781)
        Me.Panel1.TabIndex = 18
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Transparent
        Me.Button2.BackgroundImage = Global.CSI_Reporting_Application.My.Resources.Resources.icon_plus_circle_64
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.Button2.Location = New System.Drawing.Point(120, 6)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(40, 40)
        Me.Button2.TabIndex = 17
        Me.Button2.UseVisualStyleBackColor = False
        '
        'TV_LivestatusMachine
        '
        Me.TV_LivestatusMachine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TV_LivestatusMachine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TV_LivestatusMachine.CheckBoxes = True
        Me.TV_LivestatusMachine.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TV_LivestatusMachine.Location = New System.Drawing.Point(995, 3)
        Me.TV_LivestatusMachine.MinimumSize = New System.Drawing.Size(151, 4)
        Me.TV_LivestatusMachine.Name = "TV_LivestatusMachine"
        TreeNode3.Name = "Machines Groupes"
        TreeNode3.NodeFont = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        TreeNode3.Text = "Machines Groups"
        Me.TV_LivestatusMachine.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode3})
        Me.TV_LivestatusMachine.Size = New System.Drawing.Size(218, 775)
        Me.TV_LivestatusMachine.TabIndex = 12
        '
        'BTN_gensett
        '
        Me.BTN_gensett.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_gensett.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_gensett.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_gensett.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_gensett.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_gensett.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_gensett.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.BTN_gensett.Location = New System.Drawing.Point(3, 5)
        Me.BTN_gensett.Name = "BTN_gensett"
        Me.BTN_gensett.Size = New System.Drawing.Size(158, 40)
        Me.BTN_gensett.TabIndex = 15
        Me.BTN_gensett.Text = "General settings"
        Me.BTN_gensett.UseVisualStyleBackColor = False
        Me.BTN_gensett.Visible = False
        '
        'Panel_dashConfig
        '
        Me.Panel_dashConfig.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_dashConfig.AutoScroll = True
        Me.Panel_dashConfig.Controls.Add(Me.PictureBox6)
        Me.Panel_dashConfig.Controls.Add(Me.RSS_GB)
        Me.Panel_dashConfig.Controls.Add(Me.btnAdvSettings)
        Me.Panel_dashConfig.Controls.Add(Me.GroupBox6)
        Me.Panel_dashConfig.Controls.Add(Me.BTN_ping)
        Me.Panel_dashConfig.Controls.Add(Me.Label2)
        Me.Panel_dashConfig.Controls.Add(Me.CB__DeviceType)
        Me.Panel_dashConfig.Controls.Add(Me.CommonControls_GB)
        Me.Panel_dashConfig.Controls.Add(Me.Label17)
        Me.Panel_dashConfig.Controls.Add(Me.Label12)
        Me.Panel_dashConfig.Controls.Add(Me.IP_TB)
        Me.Panel_dashConfig.Controls.Add(Me.txtDeviceName)
        Me.Panel_dashConfig.Location = New System.Drawing.Point(167, 3)
        Me.Panel_dashConfig.Name = "Panel_dashConfig"
        Me.Panel_dashConfig.Size = New System.Drawing.Size(822, 775)
        Me.Panel_dashConfig.TabIndex = 16
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = Global.CSI_Reporting_Application.My.Resources.Resources.appbar_browser_wire
        Me.PictureBox6.Location = New System.Drawing.Point(577, 98)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(29, 29)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox6.TabIndex = 58
        Me.PictureBox6.TabStop = False
        '
        'RSS_GB
        '
        Me.RSS_GB.BackColor = System.Drawing.Color.White
        Me.RSS_GB.Controls.Add(Me.chkRSSMessageOnOff)
        Me.RSS_GB.Controls.Add(Me.lblBestCycleOnMachine)
        Me.RSS_GB.Controls.Add(Me.btnRSSValidate)
        Me.RSS_GB.Controls.Add(Me.txtRSSUserMessageText)
        Me.RSS_GB.Controls.Add(Me.lblMessageText)
        Me.RSS_GB.Controls.Add(Me.grpRSSMessageType)
        Me.RSS_GB.Controls.Add(Me.btnRSSDeleteMessage)
        Me.RSS_GB.Controls.Add(Me.btnRSSAddMessage)
        Me.RSS_GB.Controls.Add(Me.btnRSSMessageDown)
        Me.RSS_GB.Controls.Add(Me.btnRSSMessageUp)
        Me.RSS_GB.Controls.Add(Me.dgvRSSMessages)
        Me.RSS_GB.Controls.Add(Me.grpRSSBestCycleOn)
        Me.RSS_GB.Controls.Add(Me.chkRSSBestCycleOn)
        Me.RSS_GB.Location = New System.Drawing.Point(23, 549)
        Me.RSS_GB.Name = "RSS_GB"
        Me.RSS_GB.Size = New System.Drawing.Size(789, 289)
        Me.RSS_GB.TabIndex = 45
        Me.RSS_GB.TabStop = False
        Me.RSS_GB.Text = "RSS Feed"
        '
        'chkRSSMessageOnOff
        '
        Me.chkRSSMessageOnOff.AutoSize = True
        Me.chkRSSMessageOnOff.Location = New System.Drawing.Point(668, 240)
        Me.chkRSSMessageOnOff.Name = "chkRSSMessageOnOff"
        Me.chkRSSMessageOnOff.Size = New System.Drawing.Size(87, 25)
        Me.chkRSSMessageOnOff.TabIndex = 50
        Me.chkRSSMessageOnOff.Text = "ON/OFF"
        Me.chkRSSMessageOnOff.UseVisualStyleBackColor = True
        '
        'lblBestCycleOnMachine
        '
        Me.lblBestCycleOnMachine.AutoSize = True
        Me.lblBestCycleOnMachine.Location = New System.Drawing.Point(36, 175)
        Me.lblBestCycleOnMachine.Name = "lblBestCycleOnMachine"
        Me.lblBestCycleOnMachine.Size = New System.Drawing.Size(178, 21)
        Me.lblBestCycleOnMachine.TabIndex = 49
        Me.lblBestCycleOnMachine.Text = "Best CYCLE ON Machine1"
        Me.lblBestCycleOnMachine.Visible = False
        '
        'btnRSSValidate
        '
        Me.btnRSSValidate.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRSSValidate.Enabled = False
        Me.btnRSSValidate.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnRSSValidate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRSSValidate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnRSSValidate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRSSValidate.Location = New System.Drawing.Point(668, 203)
        Me.btnRSSValidate.Name = "btnRSSValidate"
        Me.btnRSSValidate.Size = New System.Drawing.Size(98, 32)
        Me.btnRSSValidate.TabIndex = 48
        Me.btnRSSValidate.Text = "Validate"
        Me.btnRSSValidate.UseVisualStyleBackColor = False
        '
        'txtRSSUserMessageText
        '
        Me.txtRSSUserMessageText.Location = New System.Drawing.Point(12, 203)
        Me.txtRSSUserMessageText.Multiline = True
        Me.txtRSSUserMessageText.Name = "txtRSSUserMessageText"
        Me.txtRSSUserMessageText.Size = New System.Drawing.Size(650, 77)
        Me.txtRSSUserMessageText.TabIndex = 47
        Me.txtRSSUserMessageText.Visible = False
        '
        'lblMessageText
        '
        Me.lblMessageText.AutoSize = True
        Me.lblMessageText.Location = New System.Drawing.Point(8, 176)
        Me.lblMessageText.Name = "lblMessageText"
        Me.lblMessageText.Size = New System.Drawing.Size(104, 21)
        Me.lblMessageText.TabIndex = 46
        Me.lblMessageText.Text = "Message Text:"
        Me.lblMessageText.Visible = False
        '
        'grpRSSMessageType
        '
        Me.grpRSSMessageType.Controls.Add(Me.rdbRSSSystemMessage)
        Me.grpRSSMessageType.Controls.Add(Me.rdbRSSUserMessage)
        Me.grpRSSMessageType.Enabled = False
        Me.grpRSSMessageType.Location = New System.Drawing.Point(12, 70)
        Me.grpRSSMessageType.Name = "grpRSSMessageType"
        Me.grpRSSMessageType.Size = New System.Drawing.Size(200, 102)
        Me.grpRSSMessageType.TabIndex = 45
        Me.grpRSSMessageType.TabStop = False
        Me.grpRSSMessageType.Text = "Message Type"
        '
        'rdbRSSSystemMessage
        '
        Me.rdbRSSSystemMessage.AutoSize = True
        Me.rdbRSSSystemMessage.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.rdbRSSSystemMessage.Location = New System.Drawing.Point(21, 67)
        Me.rdbRSSSystemMessage.Name = "rdbRSSSystemMessage"
        Me.rdbRSSSystemMessage.Size = New System.Drawing.Size(129, 23)
        Me.rdbRSSSystemMessage.TabIndex = 46
        Me.rdbRSSSystemMessage.TabStop = True
        Me.rdbRSSSystemMessage.Text = "System Message"
        Me.rdbRSSSystemMessage.UseVisualStyleBackColor = True
        '
        'rdbRSSUserMessage
        '
        Me.rdbRSSUserMessage.AutoSize = True
        Me.rdbRSSUserMessage.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.rdbRSSUserMessage.Location = New System.Drawing.Point(20, 35)
        Me.rdbRSSUserMessage.Name = "rdbRSSUserMessage"
        Me.rdbRSSUserMessage.Size = New System.Drawing.Size(113, 23)
        Me.rdbRSSUserMessage.TabIndex = 45
        Me.rdbRSSUserMessage.TabStop = True
        Me.rdbRSSUserMessage.Text = "User Message"
        Me.rdbRSSUserMessage.UseVisualStyleBackColor = True
        '
        'btnRSSDeleteMessage
        '
        Me.btnRSSDeleteMessage.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRSSDeleteMessage.Enabled = False
        Me.btnRSSDeleteMessage.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnRSSDeleteMessage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRSSDeleteMessage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnRSSDeleteMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRSSDeleteMessage.Location = New System.Drawing.Point(124, 29)
        Me.btnRSSDeleteMessage.Name = "btnRSSDeleteMessage"
        Me.btnRSSDeleteMessage.Size = New System.Drawing.Size(75, 34)
        Me.btnRSSDeleteMessage.TabIndex = 42
        Me.btnRSSDeleteMessage.Text = "Delete"
        Me.btnRSSDeleteMessage.UseVisualStyleBackColor = False
        '
        'btnRSSAddMessage
        '
        Me.btnRSSAddMessage.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRSSAddMessage.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnRSSAddMessage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRSSAddMessage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnRSSAddMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRSSAddMessage.Location = New System.Drawing.Point(29, 29)
        Me.btnRSSAddMessage.Name = "btnRSSAddMessage"
        Me.btnRSSAddMessage.Size = New System.Drawing.Size(75, 34)
        Me.btnRSSAddMessage.TabIndex = 41
        Me.btnRSSAddMessage.Text = "Add"
        Me.btnRSSAddMessage.UseVisualStyleBackColor = False
        '
        'btnRSSMessageDown
        '
        Me.btnRSSMessageDown.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRSSMessageDown.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnRSSMessageDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRSSMessageDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnRSSMessageDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRSSMessageDown.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRSSMessageDown.Location = New System.Drawing.Point(290, 105)
        Me.btnRSSMessageDown.Name = "btnRSSMessageDown"
        Me.btnRSSMessageDown.Size = New System.Drawing.Size(28, 44)
        Me.btnRSSMessageDown.TabIndex = 39
        Me.btnRSSMessageDown.Text = "↓"
        Me.btnRSSMessageDown.UseVisualStyleBackColor = False
        '
        'btnRSSMessageUp
        '
        Me.btnRSSMessageUp.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRSSMessageUp.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnRSSMessageUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRSSMessageUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnRSSMessageUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRSSMessageUp.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRSSMessageUp.Location = New System.Drawing.Point(290, 43)
        Me.btnRSSMessageUp.Name = "btnRSSMessageUp"
        Me.btnRSSMessageUp.Size = New System.Drawing.Size(28, 44)
        Me.btnRSSMessageUp.TabIndex = 38
        Me.btnRSSMessageUp.Text = "↑"
        Me.btnRSSMessageUp.UseVisualStyleBackColor = False
        '
        'dgvRSSMessages
        '
        Me.dgvRSSMessages.AllowUserToAddRows = False
        Me.dgvRSSMessages.AllowUserToDeleteRows = False
        Me.dgvRSSMessages.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvRSSMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRSSMessages.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Messages, Me.Priority})
        Me.dgvRSSMessages.Location = New System.Drawing.Point(324, 22)
        Me.dgvRSSMessages.Name = "dgvRSSMessages"
        Me.dgvRSSMessages.ReadOnly = True
        Me.dgvRSSMessages.RowHeadersVisible = False
        Me.dgvRSSMessages.Size = New System.Drawing.Size(459, 150)
        Me.dgvRSSMessages.TabIndex = 37
        '
        'Messages
        '
        Me.Messages.HeaderText = "Messages"
        Me.Messages.Name = "Messages"
        Me.Messages.ReadOnly = True
        Me.Messages.Width = 270
        '
        'Priority
        '
        Me.Priority.HeaderText = "Order"
        Me.Priority.Name = "Priority"
        Me.Priority.ReadOnly = True
        Me.Priority.Width = 55
        '
        'grpRSSBestCycleOn
        '
        Me.grpRSSBestCycleOn.Controls.Add(Me.rdbRSSMonth)
        Me.grpRSSBestCycleOn.Controls.Add(Me.rdbRSSWeek)
        Me.grpRSSBestCycleOn.Controls.Add(Me.rdbRSSDay)
        Me.grpRSSBestCycleOn.Enabled = False
        Me.grpRSSBestCycleOn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpRSSBestCycleOn.Location = New System.Drawing.Point(262, 179)
        Me.grpRSSBestCycleOn.Name = "grpRSSBestCycleOn"
        Me.grpRSSBestCycleOn.Size = New System.Drawing.Size(217, 101)
        Me.grpRSSBestCycleOn.TabIndex = 36
        Me.grpRSSBestCycleOn.TabStop = False
        Me.grpRSSBestCycleOn.Text = "Best CYCLE ON Machine2"
        Me.grpRSSBestCycleOn.Visible = False
        '
        'rdbRSSMonth
        '
        Me.rdbRSSMonth.AutoSize = True
        Me.rdbRSSMonth.Location = New System.Drawing.Point(11, 67)
        Me.rdbRSSMonth.Name = "rdbRSSMonth"
        Me.rdbRSSMonth.Size = New System.Drawing.Size(69, 23)
        Me.rdbRSSMonth.TabIndex = 7
        Me.rdbRSSMonth.TabStop = True
        Me.rdbRSSMonth.Text = "Month"
        Me.rdbRSSMonth.UseVisualStyleBackColor = True
        '
        'rdbRSSWeek
        '
        Me.rdbRSSWeek.AutoSize = True
        Me.rdbRSSWeek.Location = New System.Drawing.Point(11, 44)
        Me.rdbRSSWeek.Name = "rdbRSSWeek"
        Me.rdbRSSWeek.Size = New System.Drawing.Size(60, 23)
        Me.rdbRSSWeek.TabIndex = 6
        Me.rdbRSSWeek.TabStop = True
        Me.rdbRSSWeek.Text = "Week"
        Me.rdbRSSWeek.UseVisualStyleBackColor = True
        '
        'rdbRSSDay
        '
        Me.rdbRSSDay.AutoSize = True
        Me.rdbRSSDay.Location = New System.Drawing.Point(11, 21)
        Me.rdbRSSDay.Name = "rdbRSSDay"
        Me.rdbRSSDay.Size = New System.Drawing.Size(51, 23)
        Me.rdbRSSDay.TabIndex = 5
        Me.rdbRSSDay.TabStop = True
        Me.rdbRSSDay.Text = "Day"
        Me.rdbRSSDay.UseVisualStyleBackColor = True
        '
        'chkRSSBestCycleOn
        '
        Me.chkRSSBestCycleOn.AutoSize = True
        Me.chkRSSBestCycleOn.Enabled = False
        Me.chkRSSBestCycleOn.Location = New System.Drawing.Point(12, 180)
        Me.chkRSSBestCycleOn.Name = "chkRSSBestCycleOn"
        Me.chkRSSBestCycleOn.Size = New System.Drawing.Size(15, 14)
        Me.chkRSSBestCycleOn.TabIndex = 4
        Me.chkRSSBestCycleOn.UseVisualStyleBackColor = True
        Me.chkRSSBestCycleOn.Visible = False
        '
        'btnAdvSettings
        '
        Me.btnAdvSettings.Location = New System.Drawing.Point(612, 12)
        Me.btnAdvSettings.Name = "btnAdvSettings"
        Me.btnAdvSettings.Size = New System.Drawing.Size(194, 53)
        Me.btnAdvSettings.TabIndex = 54
        Me.btnAdvSettings.Text = "Advanced Settings"
        Me.btnAdvSettings.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.White
        Me.GroupBox6.Controls.Add(Me.IF_OFF)
        Me.GroupBox6.Controls.Add(Me.IF_Rot_CB)
        Me.GroupBox6.Controls.Add(Me.IF_ON)
        Me.GroupBox6.Controls.Add(Me.Label19)
        Me.GroupBox6.Controls.Add(Me.Url_TB)
        Me.GroupBox6.Controls.Add(Me.Label14)
        Me.GroupBox6.Controls.Add(Me.IFFullscreen_CB)
        Me.GroupBox6.Location = New System.Drawing.Point(23, 429)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(789, 114)
        Me.GroupBox6.TabIndex = 46
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "IFrame Settings"
        '
        'IF_OFF
        '
        Me.IF_OFF.AutoSize = True
        Me.IF_OFF.Checked = True
        Me.IF_OFF.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.IF_OFF.Location = New System.Drawing.Point(218, 38)
        Me.IF_OFF.Name = "IF_OFF"
        Me.IF_OFF.Size = New System.Drawing.Size(46, 23)
        Me.IF_OFF.TabIndex = 1
        Me.IF_OFF.TabStop = True
        Me.IF_OFF.Text = "Off"
        Me.IF_OFF.UseVisualStyleBackColor = True
        '
        'IF_Rot_CB
        '
        Me.IF_Rot_CB.AutoSize = True
        Me.IF_Rot_CB.Enabled = False
        Me.IF_Rot_CB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.IF_Rot_CB.Location = New System.Drawing.Point(35, 77)
        Me.IF_Rot_CB.Name = "IF_Rot_CB"
        Me.IF_Rot_CB.Size = New System.Drawing.Size(171, 19)
        Me.IF_Rot_CB.TabIndex = 55
        Me.IF_Rot_CB.Text = "IFrame/Live Status Rotation"
        Me.IF_Rot_CB.UseVisualStyleBackColor = True
        '
        'IF_ON
        '
        Me.IF_ON.AutoSize = True
        Me.IF_ON.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.IF_ON.Location = New System.Drawing.Point(164, 38)
        Me.IF_ON.Name = "IF_ON"
        Me.IF_ON.Size = New System.Drawing.Size(46, 23)
        Me.IF_ON.TabIndex = 0
        Me.IF_ON.Text = "On"
        Me.IF_ON.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label19.Location = New System.Drawing.Point(30, 38)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(102, 19)
        Me.Label19.TabIndex = 53
        Me.Label19.Text = "IFrame Display:"
        '
        'Url_TB
        '
        Me.Url_TB.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Url_TB.Location = New System.Drawing.Point(355, 33)
        Me.Url_TB.Name = "Url_TB"
        Me.Url_TB.Size = New System.Drawing.Size(410, 25)
        Me.Url_TB.TabIndex = 51
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label14.Location = New System.Drawing.Point(305, 38)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(37, 19)
        Me.Label14.TabIndex = 50
        Me.Label14.Text = "URL:"
        '
        'IFFullscreen_CB
        '
        Me.IFFullscreen_CB.AutoSize = True
        Me.IFFullscreen_CB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.IFFullscreen_CB.Location = New System.Drawing.Point(683, 77)
        Me.IFFullscreen_CB.Name = "IFFullscreen_CB"
        Me.IFFullscreen_CB.Size = New System.Drawing.Size(82, 19)
        Me.IFFullscreen_CB.TabIndex = 12
        Me.IFFullscreen_CB.Text = "Full screen"
        Me.IFFullscreen_CB.UseVisualStyleBackColor = True
        Me.IFFullscreen_CB.Visible = False
        '
        'BTN_ping
        '
        Me.BTN_ping.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_ping.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_ping.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_ping.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_ping.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_ping.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_ping.Location = New System.Drawing.Point(441, 98)
        Me.BTN_ping.Name = "BTN_ping"
        Me.BTN_ping.Size = New System.Drawing.Size(130, 29)
        Me.BTN_ping.TabIndex = 56
        Me.BTN_ping.Text = "ping"
        Me.BTN_ping.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(19, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(150, 21)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "Device Type :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CB__DeviceType
        '
        Me.CB__DeviceType.BackColor = System.Drawing.Color.White
        Me.CB__DeviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB__DeviceType.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB__DeviceType.FormattingEnabled = True
        Me.CB__DeviceType.Items.AddRange(New Object() {"Computer/tablet/phone", "LR1"})
        Me.CB__DeviceType.Location = New System.Drawing.Point(175, 55)
        Me.CB__DeviceType.Name = "CB__DeviceType"
        Me.CB__DeviceType.Size = New System.Drawing.Size(259, 29)
        Me.CB__DeviceType.TabIndex = 54
        '
        'CommonControls_GB
        '
        Me.CommonControls_GB.BackColor = System.Drawing.Color.White
        Me.CommonControls_GB.Controls.Add(Me.Panel5)
        Me.CommonControls_GB.Controls.Add(Me.Panel4)
        Me.CommonControls_GB.Controls.Add(Me.Panel3)
        Me.CommonControls_GB.Controls.Add(Me.Panel2)
        Me.CommonControls_GB.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.CommonControls_GB.Location = New System.Drawing.Point(23, 134)
        Me.CommonControls_GB.Name = "CommonControls_GB"
        Me.CommonControls_GB.Size = New System.Drawing.Size(789, 289)
        Me.CommonControls_GB.TabIndex = 22
        Me.CommonControls_GB.TabStop = False
        Me.CommonControls_GB.Text = "Common controls"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkDarkTheme)
        Me.Panel5.Controls.Add(Me.LSFullscreen_CB)
        Me.Panel5.Controls.Add(Me.Sec_TB)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.LSD_TB)
        Me.Panel5.Location = New System.Drawing.Point(4, 234)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(780, 50)
        Me.Panel5.TabIndex = 60
        '
        'chkDarkTheme
        '
        Me.chkDarkTheme.AutoSize = True
        Me.chkDarkTheme.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.chkDarkTheme.Location = New System.Drawing.Point(513, 17)
        Me.chkDarkTheme.Name = "chkDarkTheme"
        Me.chkDarkTheme.Size = New System.Drawing.Size(89, 19)
        Me.chkDarkTheme.TabIndex = 55
        Me.chkDarkTheme.Text = "Dark Theme"
        Me.chkDarkTheme.UseVisualStyleBackColor = True
        '
        'LSFullscreen_CB
        '
        Me.LSFullscreen_CB.AutoSize = True
        Me.LSFullscreen_CB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LSFullscreen_CB.Location = New System.Drawing.Point(29, 17)
        Me.LSFullscreen_CB.Name = "LSFullscreen_CB"
        Me.LSFullscreen_CB.Size = New System.Drawing.Size(82, 19)
        Me.LSFullscreen_CB.TabIndex = 12
        Me.LSFullscreen_CB.Text = "Full screen"
        Me.LSFullscreen_CB.UseVisualStyleBackColor = True
        '
        'Sec_TB
        '
        Me.Sec_TB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Sec_TB.Location = New System.Drawing.Point(244, 13)
        Me.Sec_TB.Name = "Sec_TB"
        Me.Sec_TB.Size = New System.Drawing.Size(32, 25)
        Me.Sec_TB.TabIndex = 34
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(282, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 15)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "Time by page (seconds)"
        '
        'LSD_TB
        '
        Me.LSD_TB.AutoSize = True
        Me.LSD_TB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LSD_TB.Location = New System.Drawing.Point(624, 17)
        Me.LSD_TB.Name = "LSD_TB"
        Me.LSD_TB.Size = New System.Drawing.Size(154, 19)
        Me.LSD_TB.TabIndex = 33
        Me.LSD_TB.Text = "New Page change after :"
        Me.LSD_TB.UseVisualStyleBackColor = True
        Me.LSD_TB.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Label27)
        Me.Panel4.Controls.Add(Me.chkLogoBarHidden)
        Me.Panel4.Controls.Add(Me.Logo_TB)
        Me.Panel4.Controls.Add(Me.txtLogoPath)
        Me.Panel4.Controls.Add(Me.Browse_Logo)
        Me.Panel4.Location = New System.Drawing.Point(4, 149)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(780, 84)
        Me.Panel4.TabIndex = 59
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label27.Location = New System.Drawing.Point(178, 48)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(60, 19)
        Me.Label27.TabIndex = 57
        Me.Label27.Text = "File:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkLogoBarHidden
        '
        Me.chkLogoBarHidden.AutoSize = True
        Me.chkLogoBarHidden.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.chkLogoBarHidden.Location = New System.Drawing.Point(29, 17)
        Me.chkLogoBarHidden.Name = "chkLogoBarHidden"
        Me.chkLogoBarHidden.Size = New System.Drawing.Size(152, 19)
        Me.chkLogoBarHidden.TabIndex = 56
        Me.chkLogoBarHidden.Text = "Hide dashboard header "
        Me.chkLogoBarHidden.UseVisualStyleBackColor = True
        '
        'Logo_TB
        '
        Me.Logo_TB.AutoSize = True
        Me.Logo_TB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Logo_TB.Location = New System.Drawing.Point(29, 50)
        Me.Logo_TB.Name = "Logo_TB"
        Me.Logo_TB.Size = New System.Drawing.Size(101, 19)
        Me.Logo_TB.TabIndex = 11
        Me.Logo_TB.Text = "Custom Logo:"
        Me.Logo_TB.UseVisualStyleBackColor = True
        '
        'txtLogoPath
        '
        Me.txtLogoPath.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLogoPath.Location = New System.Drawing.Point(244, 46)
        Me.txtLogoPath.Name = "txtLogoPath"
        Me.txtLogoPath.Size = New System.Drawing.Size(263, 25)
        Me.txtLogoPath.TabIndex = 12
        '
        'Browse_Logo
        '
        Me.Browse_Logo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Browse_Logo.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Browse_Logo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Browse_Logo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Browse_Logo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Browse_Logo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Browse_Logo.Location = New System.Drawing.Point(513, 46)
        Me.Browse_Logo.Name = "Browse_Logo"
        Me.Browse_Logo.Size = New System.Drawing.Size(106, 25)
        Me.Browse_Logo.TabIndex = 13
        Me.Browse_Logo.Text = "Browse"
        Me.Browse_Logo.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblTemperature)
        Me.Panel3.Controls.Add(Me.City_TB)
        Me.Panel3.Controls.Add(Me.Temp_CB)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Controls.Add(Me.Degree_CB)
        Me.Panel3.Controls.Add(Me.Label23)
        Me.Panel3.Location = New System.Drawing.Point(4, 79)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(780, 69)
        Me.Panel3.TabIndex = 58
        '
        'lblTemperature
        '
        Me.lblTemperature.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTemperature.Location = New System.Drawing.Point(244, 43)
        Me.lblTemperature.Name = "lblTemperature"
        Me.lblTemperature.Size = New System.Drawing.Size(517, 21)
        Me.lblTemperature.TabIndex = 51
        Me.lblTemperature.Text = "Weather"
        '
        'City_TB
        '
        Me.City_TB.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.City_TB.Location = New System.Drawing.Point(244, 12)
        Me.City_TB.Name = "City_TB"
        Me.City_TB.Size = New System.Drawing.Size(263, 25)
        Me.City_TB.TabIndex = 43
        '
        'Temp_CB
        '
        Me.Temp_CB.AutoSize = True
        Me.Temp_CB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Temp_CB.Location = New System.Drawing.Point(29, 16)
        Me.Temp_CB.Name = "Temp_CB"
        Me.Temp_CB.Size = New System.Drawing.Size(92, 19)
        Me.Temp_CB.TabIndex = 40
        Me.Temp_CB.Text = "Temperature"
        Me.Temp_CB.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label24.Location = New System.Drawing.Point(178, 15)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(60, 19)
        Me.Label24.TabIndex = 42
        Me.Label24.Text = "City:"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Degree_CB
        '
        Me.Degree_CB.BackColor = System.Drawing.Color.White
        Me.Degree_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Degree_CB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Degree_CB.FormattingEnabled = True
        Me.Degree_CB.Items.AddRange(New Object() {"Fahrenheit", "Celsius"})
        Me.Degree_CB.Location = New System.Drawing.Point(579, 12)
        Me.Degree_CB.Name = "Degree_CB"
        Me.Degree_CB.Size = New System.Drawing.Size(183, 25)
        Me.Degree_CB.TabIndex = 48
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label23.Location = New System.Drawing.Point(513, 15)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(60, 19)
        Me.Label23.TabIndex = 49
        Me.Label23.Text = "Units:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel2.Controls.Add(Me.TB_format)
        Me.Panel2.Controls.Add(Me.DateTime_TB)
        Me.Panel2.Controls.Add(Me.Label_format)
        Me.Panel2.Location = New System.Drawing.Point(4, 28)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(780, 50)
        Me.Panel2.TabIndex = 57
        '
        'TB_format
        '
        Me.TB_format.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.TB_format.Location = New System.Drawing.Point(244, 13)
        Me.TB_format.Name = "TB_format"
        Me.TB_format.Size = New System.Drawing.Size(263, 25)
        Me.TB_format.TabIndex = 52
        Me.TB_format.Text = "dd-MM-yyyy HH:mm:ss"
        Me.TB_format.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateTime_TB
        '
        Me.DateTime_TB.AutoSize = True
        Me.DateTime_TB.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.DateTime_TB.Location = New System.Drawing.Point(29, 17)
        Me.DateTime_TB.Name = "DateTime_TB"
        Me.DateTime_TB.Size = New System.Drawing.Size(102, 19)
        Me.DateTime_TB.TabIndex = 8
        Me.DateTime_TB.Text = "Date and Time"
        Me.DateTime_TB.UseVisualStyleBackColor = True
        '
        'Label_format
        '
        Me.Label_format.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label_format.Location = New System.Drawing.Point(178, 15)
        Me.Label_format.Name = "Label_format"
        Me.Label_format.Size = New System.Drawing.Size(60, 19)
        Me.Label_format.TabIndex = 51
        Me.Label_format.Text = "Format:"
        Me.Label_format.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(19, 101)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(150, 21)
        Me.Label17.TabIndex = 23
        Me.Label17.Text = "Device IP/MAC:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(19, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(150, 21)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Device Name:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'IP_TB
        '
        Me.IP_TB.Location = New System.Drawing.Point(175, 98)
        Me.IP_TB.Name = "IP_TB"
        Me.IP_TB.Size = New System.Drawing.Size(260, 29)
        Me.IP_TB.TabIndex = 2
        '
        'txtDeviceName
        '
        Me.txtDeviceName.Location = New System.Drawing.Point(175, 12)
        Me.txtDeviceName.Name = "txtDeviceName"
        Me.txtDeviceName.Size = New System.Drawing.Size(259, 29)
        Me.txtDeviceName.TabIndex = 1
        '
        'treeviewDevices
        '
        Me.treeviewDevices.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.treeviewDevices.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.treeviewDevices.HideSelection = False
        Me.treeviewDevices.Location = New System.Drawing.Point(3, 5)
        Me.treeviewDevices.Name = "treeviewDevices"
        TreeNode4.Name = "Devices"
        TreeNode4.NodeFont = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        TreeNode4.Text = "Devices"
        Me.treeviewDevices.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode4})
        Me.treeviewDevices.Size = New System.Drawing.Size(158, 773)
        Me.treeviewDevices.TabIndex = 10
        '
        'tablePageGeneral
        '
        Me.tablePageGeneral.BackColor = System.Drawing.Color.White
        Me.tablePageGeneral.Controls.Add(Me.GroupBox12)
        Me.tablePageGeneral.Controls.Add(Me.GroupBox3)
        Me.tablePageGeneral.Controls.Add(Me.PictureBox3)
        Me.tablePageGeneral.Controls.Add(Me.PictureBox2)
        Me.tablePageGeneral.Controls.Add(Me.GroupBox11)
        Me.tablePageGeneral.Controls.Add(Me.GroupBox5)
        Me.tablePageGeneral.Controls.Add(Me.GroupBox9)
        Me.tablePageGeneral.Controls.Add(Me.GroupBox1)
        Me.tablePageGeneral.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tablePageGeneral.Location = New System.Drawing.Point(4, 59)
        Me.tablePageGeneral.Name = "tablePageGeneral"
        Me.tablePageGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.tablePageGeneral.Size = New System.Drawing.Size(1225, 793)
        Me.tablePageGeneral.TabIndex = 0
        Me.tablePageGeneral.Text = "General"
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.btnPerformanceTuning)
        Me.GroupBox12.Controls.Add(Me.btnPerformanceClear)
        Me.GroupBox12.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox12.Location = New System.Drawing.Point(732, 512)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(283, 144)
        Me.GroupBox12.TabIndex = 48
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "Database Performance"
        '
        'btnPerformanceTuning
        '
        Me.btnPerformanceTuning.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnPerformanceTuning.Enabled = False
        Me.btnPerformanceTuning.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnPerformanceTuning.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPerformanceTuning.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnPerformanceTuning.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPerformanceTuning.Location = New System.Drawing.Point(12, 24)
        Me.btnPerformanceTuning.Name = "btnPerformanceTuning"
        Me.btnPerformanceTuning.Size = New System.Drawing.Size(160, 40)
        Me.btnPerformanceTuning.TabIndex = 42
        Me.btnPerformanceTuning.Text = "Tuning"
        Me.btnPerformanceTuning.UseVisualStyleBackColor = False
        '
        'btnPerformanceClear
        '
        Me.btnPerformanceClear.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnPerformanceClear.Enabled = False
        Me.btnPerformanceClear.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnPerformanceClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPerformanceClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnPerformanceClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPerformanceClear.Location = New System.Drawing.Point(12, 86)
        Me.btnPerformanceClear.Name = "btnPerformanceClear"
        Me.btnPerformanceClear.Size = New System.Drawing.Size(160, 40)
        Me.btnPerformanceClear.TabIndex = 44
        Me.btnPerformanceClear.Text = "Clear"
        Me.btnPerformanceClear.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label33)
        Me.GroupBox3.Controls.Add(Me.Label28)
        Me.GroupBox3.Controls.Add(Me.LBL_IPChange)
        Me.GroupBox3.Controls.Add(Me.LBL_srvResult)
        Me.GroupBox3.Controls.Add(Me.btnCheckWebServer)
        Me.GroupBox3.Controls.Add(Me.btnSaveWebServerPort)
        Me.GroupBox3.Controls.Add(Me.btnChangeIpAddress)
        Me.GroupBox3.Controls.Add(Me.LBL_IP)
        Me.GroupBox3.Controls.Add(Me.txtPort)
        Me.GroupBox3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(355, 108)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(660, 146)
        Me.GroupBox3.TabIndex = 43
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Network"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(274, 32)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(46, 22)
        Me.Label33.TabIndex = 11
        Me.Label33.Text = "Port:"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(9, 32)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(133, 22)
        Me.Label28.TabIndex = 10
        Me.Label28.Text = "Web Server IP Address:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LBL_IPChange
        '
        Me.LBL_IPChange.Location = New System.Drawing.Point(12, 111)
        Me.LBL_IPChange.Name = "LBL_IPChange"
        Me.LBL_IPChange.Size = New System.Drawing.Size(221, 26)
        Me.LBL_IPChange.TabIndex = 8
        Me.LBL_IPChange.Text = "IP_change"
        Me.LBL_IPChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LBL_IPChange.Visible = False
        '
        'LBL_srvResult
        '
        Me.LBL_srvResult.Location = New System.Drawing.Point(239, 111)
        Me.LBL_srvResult.Name = "LBL_srvResult"
        Me.LBL_srvResult.Size = New System.Drawing.Size(219, 26)
        Me.LBL_srvResult.TabIndex = 7
        Me.LBL_srvResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCheckWebServer
        '
        Me.btnCheckWebServer.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCheckWebServer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCheckWebServer.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnCheckWebServer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCheckWebServer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnCheckWebServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCheckWebServer.Location = New System.Drawing.Point(239, 68)
        Me.btnCheckWebServer.Name = "btnCheckWebServer"
        Me.btnCheckWebServer.Size = New System.Drawing.Size(220, 40)
        Me.btnCheckWebServer.TabIndex = 6
        Me.btnCheckWebServer.Text = "Check the CSIFLEX Webserver"
        Me.btnCheckWebServer.UseVisualStyleBackColor = False
        '
        'btnSaveWebServerPort
        '
        Me.btnSaveWebServerPort.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveWebServerPort.Enabled = False
        Me.btnSaveWebServerPort.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnSaveWebServerPort.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSaveWebServerPort.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSaveWebServerPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveWebServerPort.Location = New System.Drawing.Point(399, 30)
        Me.btnSaveWebServerPort.Name = "btnSaveWebServerPort"
        Me.btnSaveWebServerPort.Size = New System.Drawing.Size(60, 25)
        Me.btnSaveWebServerPort.TabIndex = 5
        Me.btnSaveWebServerPort.Text = "save"
        Me.btnSaveWebServerPort.UseVisualStyleBackColor = False
        '
        'btnChangeIpAddress
        '
        Me.btnChangeIpAddress.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnChangeIpAddress.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnChangeIpAddress.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnChangeIpAddress.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnChangeIpAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnChangeIpAddress.Location = New System.Drawing.Point(12, 68)
        Me.btnChangeIpAddress.Name = "btnChangeIpAddress"
        Me.btnChangeIpAddress.Size = New System.Drawing.Size(221, 40)
        Me.btnChangeIpAddress.TabIndex = 4
        Me.btnChangeIpAddress.Text = "Change the IP Address"
        Me.btnChangeIpAddress.UseVisualStyleBackColor = False
        '
        'LBL_IP
        '
        Me.LBL_IP.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_IP.Location = New System.Drawing.Point(144, 31)
        Me.LBL_IP.Name = "LBL_IP"
        Me.LBL_IP.Size = New System.Drawing.Size(131, 22)
        Me.LBL_IP.TabIndex = 1
        Me.LBL_IP.Text = "000.000.000.000"
        Me.LBL_IP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(326, 31)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(60, 25)
        Me.txtPort.TabIndex = 0
        Me.txtPort.Text = "8008"
        Me.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.CSI_Reporting_Application.My.Resources.Resources.csiflex_panelv4
        Me.PictureBox2.Location = New System.Drawing.Point(7, 33)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(339, 620)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 42
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.Image = Global.CSI_Reporting_Application.My.Resources.Resources.csiflex_logo_blue
        Me.PictureBox3.Location = New System.Drawing.Point(7, 30)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(339, 92)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 41
        Me.PictureBox3.TabStop = False
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.btnStopMBServer)
        Me.GroupBox11.Controls.Add(Me.btnStartMBServer)
        Me.GroupBox11.Controls.Add(Me.lblServiceState)
        Me.GroupBox11.Controls.Add(Me.lblStartService)
        Me.GroupBox11.Controls.Add(Me.btnStartServ)
        Me.GroupBox11.Controls.Add(Me.btnStopServ)
        Me.GroupBox11.Controls.Add(Me.lblReportServiceState)
        Me.GroupBox11.Controls.Add(Me.btnStartRepService)
        Me.GroupBox11.Controls.Add(Me.btnStopRepService)
        Me.GroupBox11.Controls.Add(Me.Label47)
        Me.GroupBox11.Controls.Add(Me.lblMBServer)
        Me.GroupBox11.Controls.Add(Me.Label37)
        Me.GroupBox11.Controls.Add(Me.Label7)
        Me.GroupBox11.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox11.Location = New System.Drawing.Point(355, 260)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(660, 143)
        Me.GroupBox11.TabIndex = 40
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Services"
        '
        'btnStartMBServer
        '
        Me.btnStartMBServer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStartMBServer.Image = Global.CSI_Reporting_Application.My.Resources.Resources.player_play
        Me.btnStartMBServer.Location = New System.Drawing.Point(325, 107)
        Me.btnStartMBServer.Name = "btnStartMBServer"
        Me.btnStartMBServer.Size = New System.Drawing.Size(40, 30)
        Me.btnStartMBServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnStartMBServer.TabIndex = 25
        Me.btnStartMBServer.TabStop = False
        '
        'lblServiceState
        '
        Me.lblServiceState.BackColor = System.Drawing.Color.LightGray
        Me.lblServiceState.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServiceState.Location = New System.Drawing.Point(199, 29)
        Me.lblServiceState.Name = "lblServiceState"
        Me.lblServiceState.Size = New System.Drawing.Size(120, 24)
        Me.lblServiceState.TabIndex = 15
        Me.lblServiceState.Text = "serviceState"
        Me.lblServiceState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnStartServ
        '
        Me.btnStartServ.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStartServ.Image = Global.CSI_Reporting_Application.My.Resources.Resources.player_play
        Me.btnStartServ.Location = New System.Drawing.Point(325, 26)
        Me.btnStartServ.Name = "btnStartServ"
        Me.btnStartServ.Size = New System.Drawing.Size(40, 30)
        Me.btnStartServ.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnStartServ.TabIndex = 18
        Me.btnStartServ.TabStop = False
        '
        'btnStopServ
        '
        Me.btnStopServ.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStopServ.Image = Global.CSI_Reporting_Application.My.Resources.Resources.player_stop
        Me.btnStopServ.Location = New System.Drawing.Point(325, 26)
        Me.btnStopServ.Name = "btnStopServ"
        Me.btnStopServ.Size = New System.Drawing.Size(40, 30)
        Me.btnStopServ.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnStopServ.TabIndex = 17
        Me.btnStopServ.TabStop = False
        '
        'lblStartService
        '
        Me.lblStartService.Font = New System.Drawing.Font("Segoe UI Semibold", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartService.ForeColor = System.Drawing.Color.Red
        Me.lblStartService.Location = New System.Drawing.Point(199, 54)
        Me.lblStartService.Name = "lblStartService"
        Me.lblStartService.Size = New System.Drawing.Size(300, 15)
        Me.lblStartService.TabIndex = 15
        Me.lblStartService.Text = "The service is starting. This might take several minutes"
        Me.lblStartService.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'lblReportServiceState
        '
        Me.lblReportServiceState.BackColor = System.Drawing.Color.LightGray
        Me.lblReportServiceState.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportServiceState.Location = New System.Drawing.Point(199, 69)
        Me.lblReportServiceState.Name = "lblReportServiceState"
        Me.lblReportServiceState.Size = New System.Drawing.Size(120, 24)
        Me.lblReportServiceState.TabIndex = 19
        Me.lblReportServiceState.Text = "serviceState"
        Me.lblReportServiceState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnStartRepService
        '
        Me.btnStartRepService.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStartRepService.Image = Global.CSI_Reporting_Application.My.Resources.Resources.player_play
        Me.btnStartRepService.Location = New System.Drawing.Point(325, 66)
        Me.btnStartRepService.Name = "btnStartRepService"
        Me.btnStartRepService.Size = New System.Drawing.Size(40, 30)
        Me.btnStartRepService.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnStartRepService.TabIndex = 21
        Me.btnStartRepService.TabStop = False
        '
        'btnStopRepService
        '
        Me.btnStopRepService.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStopRepService.Image = Global.CSI_Reporting_Application.My.Resources.Resources.player_stop
        Me.btnStopRepService.Location = New System.Drawing.Point(325, 66)
        Me.btnStopRepService.Name = "btnStopRepService"
        Me.btnStopRepService.Size = New System.Drawing.Size(40, 30)
        Me.btnStopRepService.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnStopRepService.TabIndex = 22
        Me.btnStopRepService.TabStop = False
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(13, 110)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(180, 22)
        Me.Label47.TabIndex = 24
        Me.Label47.Text = "Web Application Service"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMBServer
        '
        Me.lblMBServer.BackColor = System.Drawing.Color.LightGray
        Me.lblMBServer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMBServer.Location = New System.Drawing.Point(199, 109)
        Me.lblMBServer.Name = "lblMBServer"
        Me.lblMBServer.Size = New System.Drawing.Size(120, 24)
        Me.lblMBServer.TabIndex = 23
        Me.lblMBServer.Text = "serviceState"
        Me.lblMBServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(13, 70)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(180, 22)
        Me.Label37.TabIndex = 20
        Me.Label37.Text = "Reporting Service"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(13, 30)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(180, 22)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "CSIFLEX Server Service"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label43)
        Me.GroupBox5.Controls.Add(Me.cmbStartWeek)
        Me.GroupBox5.Controls.Add(Me.btnImportSettings)
        Me.GroupBox5.Controls.Add(Me.btnConfigureReinmeter)
        Me.GroupBox5.Controls.Add(Me.btnLoadingCycleOn)
        Me.GroupBox5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(355, 512)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(371, 144)
        Me.GroupBox5.TabIndex = 39
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Other options"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(185, 81)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(128, 17)
        Me.Label43.TabIndex = 45
        Me.Label43.Text = "First day of the week"
        '
        'cmbStartWeek
        '
        Me.cmbStartWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStartWeek.FormattingEnabled = True
        Me.cmbStartWeek.Items.AddRange(New Object() {"Sunday", "Monday"})
        Me.cmbStartWeek.Location = New System.Drawing.Point(188, 101)
        Me.cmbStartWeek.Name = "cmbStartWeek"
        Me.cmbStartWeek.Size = New System.Drawing.Size(160, 25)
        Me.cmbStartWeek.TabIndex = 44
        '
        'btnImportSettings
        '
        Me.btnImportSettings.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnImportSettings.Enabled = True
        Me.btnImportSettings.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnImportSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnImportSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnImportSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImportSettings.Location = New System.Drawing.Point(12, 86)
        Me.btnImportSettings.Name = "btnImportSettings"
        Me.btnImportSettings.Size = New System.Drawing.Size(160, 40)
        Me.btnImportSettings.TabIndex = 43
        Me.btnImportSettings.Text = "Import settings"
        Me.btnImportSettings.UseVisualStyleBackColor = False
        '
        'btnConfigureReinmeter
        '
        Me.btnConfigureReinmeter.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnConfigureReinmeter.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnConfigureReinmeter.FlatAppearance.CheckedBackColor = System.Drawing.Color.White
        Me.btnConfigureReinmeter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnConfigureReinmeter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnConfigureReinmeter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfigureReinmeter.Location = New System.Drawing.Point(12, 24)
        Me.btnConfigureReinmeter.Name = "btnConfigureReinmeter"
        Me.btnConfigureReinmeter.Size = New System.Drawing.Size(160, 40)
        Me.btnConfigureReinmeter.TabIndex = 32
        Me.btnConfigureReinmeter.Text = "Configure Rainmeter"
        Me.btnConfigureReinmeter.UseVisualStyleBackColor = False
        '
        'btnLoadingCycleOn
        '
        Me.btnLoadingCycleOn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnLoadingCycleOn.Enabled = False
        Me.btnLoadingCycleOn.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnLoadingCycleOn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnLoadingCycleOn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnLoadingCycleOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadingCycleOn.Location = New System.Drawing.Point(188, 24)
        Me.btnLoadingCycleOn.Name = "btnLoadingCycleOn"
        Me.btnLoadingCycleOn.Size = New System.Drawing.Size(160, 40)
        Me.btnLoadingCycleOn.TabIndex = 6
        Me.btnLoadingCycleOn.Text = "Loading as Cycle On"
        Me.btnLoadingCycleOn.UseVisualStyleBackColor = False
        '
        'GroupBox9
        '
        Me.GroupBox9.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox9.Controls.Add(Me.PictureBox4)
        Me.GroupBox9.Controls.Add(Me.btnConfigureEmail)
        Me.GroupBox9.Controls.Add(Me.btnConfigureReports)
        Me.GroupBox9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox9.Location = New System.Drawing.Point(355, 409)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(660, 97)
        Me.GroupBox9.TabIndex = 32
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Auto Reporting"
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.CSI_Reporting_Application.My.Resources.Resources.marketing_analysis_icon_32
        Me.PictureBox4.Location = New System.Drawing.Point(484, 10)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(97, 76)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 38
        Me.PictureBox4.TabStop = False
        '
        'btnConfigureEmail
        '
        Me.btnConfigureEmail.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnConfigureEmail.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnConfigureEmail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnConfigureEmail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnConfigureEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfigureEmail.Location = New System.Drawing.Point(239, 33)
        Me.btnConfigureEmail.Name = "btnConfigureEmail"
        Me.btnConfigureEmail.Size = New System.Drawing.Size(220, 40)
        Me.btnConfigureEmail.TabIndex = 37
        Me.btnConfigureEmail.Text = "Configure email server"
        Me.btnConfigureEmail.UseVisualStyleBackColor = False
        '
        'btnConfigureReports
        '
        Me.btnConfigureReports.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnConfigureReports.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnConfigureReports.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnConfigureReports.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnConfigureReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfigureReports.Location = New System.Drawing.Point(13, 33)
        Me.btnConfigureReports.Name = "btnConfigureReports"
        Me.btnConfigureReports.Size = New System.Drawing.Size(220, 40)
        Me.btnConfigureReports.TabIndex = 32
        Me.btnConfigureReports.Text = "Configure reports"
        Me.btnConfigureReports.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnDefaultEnetFolder)
        Me.GroupBox1.Controls.Add(Me.btnBrowserEnetFolder)
        Me.GroupBox1.Controls.Add(Me.btnReloadEnetSettings)
        Me.GroupBox1.Controls.Add(Me.txtGeneralEnetFolder)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(355, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(660, 96)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Folders"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(15, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(356, 22)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Specify the eNETDNC folder"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnBrowserEnetFolder
        '
        Me.btnBrowserEnetFolder.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnBrowserEnetFolder.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnBrowserEnetFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBrowserEnetFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnBrowserEnetFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowserEnetFolder.Location = New System.Drawing.Point(377, 53)
        Me.btnBrowserEnetFolder.Name = "btnBrowserEnetFolder"
        Me.btnBrowserEnetFolder.Size = New System.Drawing.Size(80, 27)
        Me.btnBrowserEnetFolder.TabIndex = 4
        Me.btnBrowserEnetFolder.Text = "Browse"
        Me.btnBrowserEnetFolder.UseVisualStyleBackColor = False
        '
        'btnDefaultEnetFolder
        '
        Me.btnDefaultEnetFolder.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnDefaultEnetFolder.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnDefaultEnetFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDefaultEnetFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnDefaultEnetFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDefaultEnetFolder.Location = New System.Drawing.Point(463, 53)
        Me.btnDefaultEnetFolder.Name = "btnDefaultEnetFolder"
        Me.btnDefaultEnetFolder.Size = New System.Drawing.Size(80, 27)
        Me.btnDefaultEnetFolder.TabIndex = 8
        Me.btnDefaultEnetFolder.Text = "Default"
        Me.btnDefaultEnetFolder.UseVisualStyleBackColor = False
        '
        'btnReloadEnetSettings
        '
        Me.btnReloadEnetSettings.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnReloadEnetSettings.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnReloadEnetSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnReloadEnetSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnReloadEnetSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReloadEnetSettings.Location = New System.Drawing.Point(549, 53)
        Me.btnReloadEnetSettings.Name = "btnReloadEnetSettings"
        Me.btnReloadEnetSettings.Size = New System.Drawing.Size(80, 27)
        Me.btnReloadEnetSettings.TabIndex = 4
        Me.btnReloadEnetSettings.Text = "Reload"
        Me.btnReloadEnetSettings.UseVisualStyleBackColor = False
        '
        'txtGeneralEnetFolder
        '
        Me.txtGeneralEnetFolder.BackColor = System.Drawing.Color.White
        Me.txtGeneralEnetFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGeneralEnetFolder.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGeneralEnetFolder.Location = New System.Drawing.Point(13, 53)
        Me.txtGeneralEnetFolder.Name = "txtGeneralEnetFolder"
        Me.txtGeneralEnetFolder.Size = New System.Drawing.Size(358, 27)
        Me.txtGeneralEnetFolder.TabIndex = 5
        '
        'TabControl_DashBoard
        '
        Me.TabControl_DashBoard.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl_DashBoard.Controls.Add(Me.tablePageGeneral)
        Me.TabControl_DashBoard.Controls.Add(Me.tablePageEnet)
        Me.TabControl_DashBoard.Controls.Add(Me.tablePageUsers)
        Me.TabControl_DashBoard.Controls.Add(Me.tablePageDashboards)
        Me.TabControl_DashBoard.Controls.Add(Me.tablePageCSICon)
        Me.TabControl_DashBoard.Controls.Add(Me.tablePagePartNumber)
        Me.TabControl_DashBoard.Controls.Add(Me.tablePageLicense)
        Me.TabControl_DashBoard.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl_DashBoard.HotTrack = True
        Me.TabControl_DashBoard.ImageList = Me.IM_tab
        Me.TabControl_DashBoard.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TabControl_DashBoard.ItemSize = New System.Drawing.Size(125, 55)
        Me.TabControl_DashBoard.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_DashBoard.Multiline = True
        Me.TabControl_DashBoard.Name = "TabControl_DashBoard"
        Me.TabControl_DashBoard.SelectedIndex = 0
        Me.TabControl_DashBoard.Size = New System.Drawing.Size(1233, 856)
        Me.TabControl_DashBoard.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControl_DashBoard.TabIndex = 13
        '
        'tablePageEnet
        '
        Me.tablePageEnet.Controls.Add(Me.pnlMachines)
        Me.tablePageEnet.Controls.Add(Me.grpEnetSettings)
        Me.tablePageEnet.Location = New System.Drawing.Point(4, 59)
        Me.tablePageEnet.Name = "tablePageEnet"
        Me.tablePageEnet.Size = New System.Drawing.Size(1225, 793)
        Me.tablePageEnet.TabIndex = 15
        Me.tablePageEnet.Text = "eNETDNC"
        Me.tablePageEnet.UseVisualStyleBackColor = True
        '
        'pnlMachines
        '
        Me.pnlMachines.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMachines.Controls.Add(Me.Label45)
        Me.pnlMachines.Controls.Add(Me.cmbImportGroups)
        Me.pnlMachines.Controls.Add(Me.lblQttMachines)
        Me.pnlMachines.Controls.Add(Me.chkEnetAllPositions)
        Me.pnlMachines.Controls.Add(Me.gboxMachineSettings)
        Me.pnlMachines.Controls.Add(Me.btnEnetSettings)
        Me.pnlMachines.Controls.Add(Me.gridviewMachines)
        Me.pnlMachines.Controls.Add(Me.Button7)
        Me.pnlMachines.Controls.Add(Me.treeviewGroupsOfMachines)
        Me.pnlMachines.Location = New System.Drawing.Point(8, 235)
        Me.pnlMachines.Name = "pnlMachines"
        Me.pnlMachines.Size = New System.Drawing.Size(1209, 554)
        Me.pnlMachines.TabIndex = 24
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(3, 19)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(57, 21)
        Me.Label45.TabIndex = 47
        Me.Label45.Text = "Import"
        '
        'cmbImportGroups
        '
        Me.cmbImportGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbImportGroups.FormattingEnabled = True
        Me.cmbImportGroups.Items.AddRange(New Object() {"eNET Departments", "eNET Groups"})
        Me.cmbImportGroups.Location = New System.Drawing.Point(66, 16)
        Me.cmbImportGroups.Name = "cmbImportGroups"
        Me.cmbImportGroups.Size = New System.Drawing.Size(196, 29)
        Me.cmbImportGroups.TabIndex = 46
        '
        'lblQttMachines
        '
        Me.lblQttMachines.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblQttMachines.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQttMachines.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.lblQttMachines.Location = New System.Drawing.Point(748, 513)
        Me.lblQttMachines.Name = "lblQttMachines"
        Me.lblQttMachines.Size = New System.Drawing.Size(220, 22)
        Me.lblQttMachines.TabIndex = 26
        Me.lblQttMachines.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkEnetAllPositions
        '
        Me.chkEnetAllPositions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkEnetAllPositions.AutoSize = True
        Me.chkEnetAllPositions.Location = New System.Drawing.Point(269, 509)
        Me.chkEnetAllPositions.Name = "chkEnetAllPositions"
        Me.chkEnetAllPositions.Size = New System.Drawing.Size(228, 25)
        Me.chkEnetAllPositions.TabIndex = 25
        Me.chkEnetAllPositions.Text = "Show all eNETDNC positions"
        Me.chkEnetAllPositions.UseVisualStyleBackColor = True
        '
        'gboxMachineSettings
        '
        Me.gboxMachineSettings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboxMachineSettings.Controls.Add(Me.pnlMachineSettings)
        Me.gboxMachineSettings.Location = New System.Drawing.Point(974, 3)
        Me.gboxMachineSettings.Name = "gboxMachineSettings"
        Me.gboxMachineSettings.Size = New System.Drawing.Size(232, 548)
        Me.gboxMachineSettings.TabIndex = 24
        Me.gboxMachineSettings.TabStop = False
        Me.gboxMachineSettings.Padding = New Padding(2, 5, 2, 7)
        '
        'pnlMachineSettings
        '
        Me.pnlMachineSettings.Name = "pnlMachineSettings"
        Me.pnlMachineSettings.Dock = DockStyle.Fill
        'Me.pnlMachineSettings.Location = New Point(3, 3)
        'Me.pnlMachineSettings.Size = New Size(Me.gboxMachineSettings.Size.Width - 6, Me.gboxMachineSettings.Size.Height - 6)
        'Me.pnlMachineSettings.BorderStyle = BorderStyle.FixedSingle
        Me.pnlMachineSettings.AutoScroll = True
        Me.pnlMachineSettings.Controls.Add(Me.labelMachineId)
        Me.pnlMachineSettings.Controls.Add(Me.lblMachineId)
        Me.pnlMachineSettings.Controls.Add(Me.labelMachineName)
        Me.pnlMachineSettings.Controls.Add(Me.lblMachineName)
        Me.pnlMachineSettings.Controls.Add(Me.labelEnetPos)
        Me.pnlMachineSettings.Controls.Add(Me.lblEnetPos)
        Me.pnlMachineSettings.Controls.Add(Me.labelDepartment)
        Me.pnlMachineSettings.Controls.Add(Me.lblDepartment)
        Me.pnlMachineSettings.Controls.Add(Me.Label40)
        Me.pnlMachineSettings.Controls.Add(Me.lblProtocol)
        Me.pnlMachineSettings.Controls.Add(Me.Label41)
        Me.pnlMachineSettings.Controls.Add(Me.lblFtpFileName)
        Me.pnlMachineSettings.Controls.Add(Me.labelAlwaysRecCycleOn)
        Me.pnlMachineSettings.Controls.Add(Me.lblAlwaysRecCycleOn)

        Me.pnlMachineSettings.Controls.Add(Me.labelCycleOnCommand)
        Me.pnlMachineSettings.Controls.Add(Me.lblCycleOnCommand)
        Me.pnlMachineSettings.Controls.Add(Me.labelCycleOffCommand)
        Me.pnlMachineSettings.Controls.Add(Me.lblCycleOffCommand)
        Me.pnlMachineSettings.Controls.Add(Me.labelPartNumberCommand)
        Me.pnlMachineSettings.Controls.Add(Me.lblPartNumberCommand)
        '
        'labelMachineId
        '
        Me.labelMachineId.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelMachineId.Location = New System.Drawing.Point(6, 25)
        Me.labelMachineId.Name = "labelMachineId"
        Me.labelMachineId.Size = New System.Drawing.Size(220, 22)
        Me.labelMachineId.TabIndex = 19
        Me.labelMachineId.Text = "Machine Id"
        '
        'lblMachineId
        '
        Me.lblMachineId.Font = New System.Drawing.Font("Segoe UI", 11.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachineId.Location = New System.Drawing.Point(6, 47)
        Me.lblMachineId.Name = "lblMachineId"
        Me.lblMachineId.Size = New System.Drawing.Size(220, 22)
        Me.lblMachineId.TabIndex = 20
        Me.lblMachineId.Text = ""
        '
        'labelMachineName
        '
        Me.labelMachineName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelMachineName.Location = New System.Drawing.Point(6, 80)
        Me.labelMachineName.Name = "labelMachineName"
        Me.labelMachineName.Size = New System.Drawing.Size(220, 22)
        Me.labelMachineName.TabIndex = 3
        Me.labelMachineName.Text = "Machine Name"
        '
        'lblMachineName
        '
        Me.lblMachineName.Font = New System.Drawing.Font("Segoe UI", 11.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachineName.Location = New System.Drawing.Point(6, 102)
        Me.lblMachineName.Name = "lblMachineName"
        Me.lblMachineName.Size = New System.Drawing.Size(220, 22)
        Me.lblMachineName.TabIndex = 4
        Me.lblMachineName.Text = ""
        '
        'labelEnetPos
        '
        Me.labelEnetPos.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelEnetPos.Location = New System.Drawing.Point(6, 135)
        Me.labelEnetPos.Name = "labelEnetPos"
        Me.labelEnetPos.Size = New System.Drawing.Size(220, 22)
        Me.labelEnetPos.TabIndex = 5
        Me.labelEnetPos.Text = "eNETDNC Position"
        '
        'lblEnetPos
        '
        Me.lblEnetPos.Font = New System.Drawing.Font("Segoe UI", 11.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnetPos.Location = New System.Drawing.Point(6, 157)
        Me.lblEnetPos.Name = "lblEnetPos"
        Me.lblEnetPos.Size = New System.Drawing.Size(220, 22)
        Me.lblEnetPos.TabIndex = 6
        Me.lblEnetPos.Text = ""
        '
        'labelDepartment
        '
        Me.labelDepartment.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelDepartment.Location = New System.Drawing.Point(6, 190)
        Me.labelDepartment.Name = "labelDepartment"
        Me.labelDepartment.Size = New System.Drawing.Size(220, 22)
        Me.labelDepartment.TabIndex = 7
        Me.labelDepartment.Text = "Department"
        '
        'lblDepartment
        '
        Me.lblDepartment.Font = New System.Drawing.Font("Segoe UI", 11.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(6, 212)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(220, 22)
        Me.lblDepartment.TabIndex = 8
        Me.lblDepartment.Text = ""
        '
        'Label40
        '
        Me.Label40.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(6, 245)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(220, 22)
        Me.Label40.TabIndex = 9
        Me.Label40.Text = "Protocol"
        '
        'lblProtocol
        '
        Me.lblProtocol.Font = New System.Drawing.Font("Segoe UI", 11.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProtocol.Location = New System.Drawing.Point(6, 267)
        Me.lblProtocol.Name = "lblProtocol"
        Me.lblProtocol.Size = New System.Drawing.Size(220, 22)
        Me.lblProtocol.TabIndex = 10
        Me.lblProtocol.Text = ""
        '
        'Label41
        '
        Me.Label41.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(6, 300)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(220, 22)
        Me.Label41.TabIndex = 11
        Me.Label41.Text = "FTP File Name"
        '
        'lblFtpFileName
        '
        Me.lblFtpFileName.Font = New System.Drawing.Font("Segoe UI", 11.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFtpFileName.Location = New System.Drawing.Point(6, 322)
        Me.lblFtpFileName.Name = "lblFtpFileName"
        Me.lblFtpFileName.Size = New System.Drawing.Size(220, 22)
        Me.lblFtpFileName.TabIndex = 12
        Me.lblFtpFileName.Text = ""
        '
        'labelAlwaysRecCycleOn
        '
        Me.labelAlwaysRecCycleOn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelAlwaysRecCycleOn.Location = New System.Drawing.Point(6, 355)
        Me.labelAlwaysRecCycleOn.Name = "labelAlwaysRecCycleOn"
        Me.labelAlwaysRecCycleOn.Size = New System.Drawing.Size(220, 22)
        Me.labelAlwaysRecCycleOn.TabIndex = 17
        Me.labelAlwaysRecCycleOn.Text = "Always Record Cycle ON"
        '
        'lblAlwaysRecCycleOn
        '
        Me.lblAlwaysRecCycleOn.Font = New System.Drawing.Font("Segoe UI", 11.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlwaysRecCycleOn.Location = New System.Drawing.Point(6, 377)
        Me.lblAlwaysRecCycleOn.Name = "lblAlwaysRecCycleOn"
        Me.lblAlwaysRecCycleOn.Size = New System.Drawing.Size(220, 22)
        Me.lblAlwaysRecCycleOn.TabIndex = 18
        Me.lblAlwaysRecCycleOn.Text = ""

        '
        'labelCycleOnCommand
        '
        Me.labelCycleOnCommand.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCycleOnCommand.Location = New System.Drawing.Point(6, 410)
        Me.labelCycleOnCommand.Name = "labelCycleOnCommand"
        Me.labelCycleOnCommand.Size = New System.Drawing.Size(220, 22)
        'Me.labelCycleOnCommand.TabIndex = 17
        Me.labelCycleOnCommand.Text = "CYCLE ON Command"
        '
        'lblCycleOnCommand
        '
        Me.lblCycleOnCommand.Font = New System.Drawing.Font("Segoe UI", 11.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCycleOnCommand.Location = New System.Drawing.Point(6, 432)
        Me.lblCycleOnCommand.Name = "lblCycleOnCommand"
        Me.lblCycleOnCommand.Size = New System.Drawing.Size(220, 22)
        'Me.lblCycleOnCommand.TabIndex = 18
        Me.lblCycleOnCommand.Text = ""
        '
        'labelCycleOffCommand
        '
        Me.labelCycleOffCommand.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCycleOffCommand.Location = New System.Drawing.Point(6, 465)
        Me.labelCycleOffCommand.Name = "labelCycleOffCommand"
        Me.labelCycleOffCommand.Size = New System.Drawing.Size(220, 22)
        'Me.labelCycleOffCommand.TabIndex = 17
        Me.labelCycleOffCommand.Text = "CYCLE OFF Command"
        '
        'lblCycleOffCommand
        '
        Me.lblCycleOffCommand.Font = New System.Drawing.Font("Segoe UI", 11.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCycleOffCommand.Location = New System.Drawing.Point(6, 487)
        Me.lblCycleOffCommand.Name = "lblCycleOffCommand"
        Me.lblCycleOffCommand.Size = New System.Drawing.Size(220, 22)
        'Me.lblCycleOffCommand.TabIndex = 18
        Me.lblCycleOffCommand.Text = ""
        '
        'labelPartNumberCommand
        '
        Me.labelPartNumberCommand.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelPartNumberCommand.Location = New System.Drawing.Point(6, 520)
        Me.labelPartNumberCommand.Name = "labelPartNumberCommand"
        Me.labelPartNumberCommand.Size = New System.Drawing.Size(220, 22)
        'Me.labelPartNumberCommand.TabIndex = 17
        Me.labelPartNumberCommand.Text = "Part Number Command"
        '
        'lblPartNumberCommand
        '
        Me.lblPartNumberCommand.Font = New System.Drawing.Font("Segoe UI", 11.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPartNumberCommand.Location = New System.Drawing.Point(6, 542)
        Me.lblPartNumberCommand.Name = "lblPartNumberCommand"
        Me.lblPartNumberCommand.Size = New System.Drawing.Size(220, 22)
        'Me.lblPartNumberCommand.TabIndex = 18
        Me.lblPartNumberCommand.Text = ""
        '
        'btnEnetSettings
        '
        Me.btnEnetSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEnetSettings.BackColor = System.Drawing.Color.Transparent
        Me.btnEnetSettings.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnetSettings.Location = New System.Drawing.Point(0, 508)
        Me.btnEnetSettings.Name = "btnEnetSettings"
        Me.btnEnetSettings.Size = New System.Drawing.Size(262, 43)
        Me.btnEnetSettings.TabIndex = 23
        Me.btnEnetSettings.Text = "eNETDNC Settings"
        Me.btnEnetSettings.UseVisualStyleBackColor = False
        '
        'gridviewMachines
        '
        Me.gridviewMachines.AllowUserToAddRows = False
        Me.gridviewMachines.AllowUserToDeleteRows = False
        Me.gridviewMachines.AllowUserToResizeRows = False
        Me.gridviewMachines.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gridviewMachines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.gridviewMachines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridviewMachines.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.Display, Me.Machines, Me.enetMachineName, Me.MachineLabel, Me.Source, Me.Check, Me.Connection, Me.Dailytarget, Me.Weeklytarget, Me.Monthlytarget, Me.IsMonitored})
        Me.gridviewMachines.ContextMenuStrip = Me.CMS_GridEdit
        Me.gridviewMachines.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.gridviewMachines.Location = New System.Drawing.Point(268, 16)
        Me.gridviewMachines.MultiSelect = True
        Me.gridviewMachines.Name = "gridviewMachines"
        Me.gridviewMachines.Size = New System.Drawing.Size(700, 486)
        Me.gridviewMachines.TabIndex = 21
        '
        'Id
        '
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.Visible = False
        Me.Id.ReadOnly = True
        Me.Id.Width = 25
        '
        'Display
        '
        Me.Display.HeaderText = "Display"
        Me.Display.Name = "Display"
        Me.Display.Visible = False
        '
        'Machines
        '
        Me.Machines.HeaderText = "Machine Name"
        Me.Machines.Name = "Machines"
        '
        'enetMachineName
        '
        Me.enetMachineName.HeaderText = "eNET Machine"
        Me.enetMachineName.Name = "enetMachineName"
        Me.enetMachineName.ReadOnly = True
        '
        'MachineLabel
        '
        Me.MachineLabel.HeaderText = "Machine Label"
        Me.MachineLabel.Name = "MachineLabel"
        '
        'Source
        '
        Me.Source.HeaderText = "Source"
        Me.Source.Name = "Source"
        Me.Source.Visible = False
        '
        'Check
        '
        Me.Check.HeaderText = "Check"
        Me.Check.Name = "Check"
        Me.Check.Visible = False
        '
        'Connection
        '
        Me.Connection.HeaderText = "Connection"
        Me.Connection.Name = "Connection"
        Me.Connection.Visible = False
        '
        'Dailytarget
        '
        Me.Dailytarget.HeaderText = "Daily target (h)"
        Me.Dailytarget.Name = "Dailytarget"
        Me.Dailytarget.Width = 60
        '
        'Weeklytarget
        '
        Me.Weeklytarget.HeaderText = "Weekly target (h)"
        Me.Weeklytarget.Name = "Weeklytarget"
        Me.Weeklytarget.Width = 60
        '
        'Monthlytarget
        '
        Me.Monthlytarget.HeaderText = "Monthly target (h)"
        Me.Monthlytarget.Name = "Monthlytarget"
        Me.Monthlytarget.Width = 60
        '
        'IsMonitored
        '
        Me.IsMonitored.DataPropertyName = "IsMonitored"
        Me.IsMonitored.HeaderText = "IsMonitored"
        Me.IsMonitored.Name = "IsMonitored"
        Me.IsMonitored.ReadOnly = True
        Me.IsMonitored.Visible = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.Transparent
        Me.Button7.BackgroundImage = Global.CSI_Reporting_Application.My.Resources.Resources.icon_plus_circle_64
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button7.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.Button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.Button7.Location = New System.Drawing.Point(221, 51)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(40, 40)
        Me.Button7.TabIndex = 22
        Me.Button7.UseVisualStyleBackColor = False
        '
        'treeviewGroupsOfMachines
        '
        Me.treeviewGroupsOfMachines.AllowDrop = True
        Me.treeviewGroupsOfMachines.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.treeviewGroupsOfMachines.LabelEdit = True
        Me.treeviewGroupsOfMachines.Location = New System.Drawing.Point(0, 50)
        Me.treeviewGroupsOfMachines.MinimumSize = New System.Drawing.Size(226, 4)
        Me.treeviewGroupsOfMachines.Name = "treeviewGroupsOfMachines"
        TreeNode5.Name = "Groups of machines"
        TreeNode5.NodeFont = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        TreeNode5.Text = "Groups of machines"
        Me.treeviewGroupsOfMachines.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode5})
        Me.treeviewGroupsOfMachines.Size = New System.Drawing.Size(262, 452)
        Me.treeviewGroupsOfMachines.TabIndex = 9
        '
        'grpEnetSettings
        '
        Me.grpEnetSettings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpEnetSettings.Controls.Add(Me.btnEnetCancelChanges)
        Me.grpEnetSettings.Controls.Add(Me.lblEnetFolderStatus)
        Me.grpEnetSettings.Controls.Add(Me.btnEnetSaveChanges)
        Me.grpEnetSettings.Controls.Add(Me.GroupBox4)
        Me.grpEnetSettings.Controls.Add(Me.lblEnetCheckResult)
        Me.grpEnetSettings.Controls.Add(Me.btnCheckEnetHttpIp)
        Me.grpEnetSettings.Controls.Add(Me.lstEnetMachines)
        Me.grpEnetSettings.Controls.Add(Me.txtEnetHttpIp)
        Me.grpEnetSettings.Controls.Add(Me.PictureBox7)
        Me.grpEnetSettings.Controls.Add(Me.Label34)
        Me.grpEnetSettings.Controls.Add(Me.Label22)
        Me.grpEnetSettings.Controls.Add(Me.txtEnetFolder)
        Me.grpEnetSettings.Controls.Add(Me.btnEnetFolderBrowser)
        Me.grpEnetSettings.Controls.Add(Me.btnEnetFolderDefault)
        Me.grpEnetSettings.Location = New System.Drawing.Point(8, 5)
        Me.grpEnetSettings.Name = "grpEnetSettings"
        Me.grpEnetSettings.Size = New System.Drawing.Size(1209, 225)
        Me.grpEnetSettings.TabIndex = 23
        Me.grpEnetSettings.TabStop = False
        Me.grpEnetSettings.Visible = False
        '
        'btnEnetCancelChanges
        '
        Me.btnEnetCancelChanges.BackColor = System.Drawing.Color.Transparent
        Me.btnEnetCancelChanges.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnetCancelChanges.Location = New System.Drawing.Point(541, 184)
        Me.btnEnetCancelChanges.Name = "btnEnetCancelChanges"
        Me.btnEnetCancelChanges.Size = New System.Drawing.Size(153, 31)
        Me.btnEnetCancelChanges.TabIndex = 14
        Me.btnEnetCancelChanges.Text = "Cancel"
        Me.btnEnetCancelChanges.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnEnetCancelChanges.UseVisualStyleBackColor = False
        '
        'lblEnetFolderStatus
        '
        Me.lblEnetFolderStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnetFolderStatus.Location = New System.Drawing.Point(322, 67)
        Me.lblEnetFolderStatus.Name = "lblEnetFolderStatus"
        Me.lblEnetFolderStatus.Size = New System.Drawing.Size(286, 22)
        Me.lblEnetFolderStatus.TabIndex = 13
        Me.lblEnetFolderStatus.Text = "eNETDNC Folder Result"
        '
        'btnEnetSaveChanges
        '
        Me.btnEnetSaveChanges.BackColor = System.Drawing.Color.Transparent
        Me.btnEnetSaveChanges.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnetSaveChanges.Location = New System.Drawing.Point(322, 184)
        Me.btnEnetSaveChanges.Name = "btnEnetSaveChanges"
        Me.btnEnetSaveChanges.Size = New System.Drawing.Size(153, 31)
        Me.btnEnetSaveChanges.TabIndex = 12
        Me.btnEnetSaveChanges.Text = "Save Changes"
        Me.btnEnetSaveChanges.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnEnetSaveChanges.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnLoadEnetHistory)
        Me.GroupBox4.Controls.Add(Me.Label35)
        Me.GroupBox4.Controls.Add(Me.lstEnetYears)
        Me.GroupBox4.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(878, 14)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(167, 203)
        Me.GroupBox4.TabIndex = 11
        Me.GroupBox4.TabStop = False
        '
        'btnLoadEnetHistory
        '
        Me.btnLoadEnetHistory.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadEnetHistory.Location = New System.Drawing.Point(6, 148)
        Me.btnLoadEnetHistory.Name = "btnLoadEnetHistory"
        Me.btnLoadEnetHistory.Size = New System.Drawing.Size(155, 49)
        Me.btnLoadEnetHistory.TabIndex = 15
        Me.btnLoadEnetHistory.Text = "Load eNETDNC Data"
        Me.btnLoadEnetHistory.UseVisualStyleBackColor = True
        '
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(6, 11)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(155, 22)
        Me.Label35.TabIndex = 14
        Me.Label35.Text = "eNETDNC Years"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lstEnetYears
        '
        Me.lstEnetYears.CheckBoxes = True
        Me.lstEnetYears.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lstEnetYears.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstEnetYears.FullRowSelect = True
        Me.lstEnetYears.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstEnetYears.HideSelection = False
        Me.lstEnetYears.Location = New System.Drawing.Point(6, 36)
        Me.lstEnetYears.MultiSelect = False
        Me.lstEnetYears.Name = "lstEnetYears"
        Me.lstEnetYears.Size = New System.Drawing.Size(155, 106)
        Me.lstEnetYears.Sorting = System.Windows.Forms.SortOrder.Descending
        Me.lstEnetYears.TabIndex = 13
        Me.lstEnetYears.UseCompatibleStateImageBehavior = False
        Me.lstEnetYears.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = ""
        Me.ColumnHeader1.Width = 100
        '
        'lblEnetCheckResult
        '
        Me.lblEnetCheckResult.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnetCheckResult.Location = New System.Drawing.Point(322, 159)
        Me.lblEnetCheckResult.Name = "lblEnetCheckResult"
        Me.lblEnetCheckResult.Size = New System.Drawing.Size(286, 22)
        Me.lblEnetCheckResult.TabIndex = 10
        Me.lblEnetCheckResult.Text = "eNETDNC Http Server IP Result"
        '
        'btnCheckEnetHttpIp
        '
        Me.btnCheckEnetHttpIp.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCheckEnetHttpIp.Location = New System.Drawing.Point(614, 131)
        Me.btnCheckEnetHttpIp.Name = "btnCheckEnetHttpIp"
        Me.btnCheckEnetHttpIp.Size = New System.Drawing.Size(80, 25)
        Me.btnCheckEnetHttpIp.TabIndex = 9
        Me.btnCheckEnetHttpIp.Text = "Check"
        Me.btnCheckEnetHttpIp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCheckEnetHttpIp.UseVisualStyleBackColor = True
        '
        'lstEnetMachines
        '
        Me.lstEnetMachines.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Column1})
        Me.lstEnetMachines.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstEnetMachines.FullRowSelect = True
        Me.lstEnetMachines.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstEnetMachines.HideSelection = False
        Me.lstEnetMachines.Location = New System.Drawing.Point(707, 24)
        Me.lstEnetMachines.MultiSelect = False
        Me.lstEnetMachines.Name = "lstEnetMachines"
        Me.lstEnetMachines.Size = New System.Drawing.Size(165, 193)
        Me.lstEnetMachines.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstEnetMachines.TabIndex = 8
        Me.lstEnetMachines.UseCompatibleStateImageBehavior = False
        Me.lstEnetMachines.View = System.Windows.Forms.View.Details
        '
        'Column1
        '
        Me.Column1.Text = ""
        Me.Column1.Width = 150
        '
        'txtEnetHttpIp
        '
        Me.txtEnetHttpIp.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEnetHttpIp.Location = New System.Drawing.Point(322, 131)
        Me.txtEnetHttpIp.Name = "txtEnetHttpIp"
        Me.txtEnetHttpIp.Size = New System.Drawing.Size(286, 25)
        Me.txtEnetHttpIp.TabIndex = 7
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = Global.CSI_Reporting_Application.My.Resources.Resources.logo
        Me.PictureBox7.Location = New System.Drawing.Point(6, 33)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(306, 178)
        Me.PictureBox7.TabIndex = 1
        Me.PictureBox7.TabStop = False
        '
        'Label34
        '
        Me.Label34.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(322, 106)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(286, 22)
        Me.Label34.TabIndex = 6
        Me.Label34.Text = "eNETDNC Http Server IP"
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(318, 14)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(290, 22)
        Me.Label22.TabIndex = 2
        Me.Label22.Text = "eNETDNC Folder"
        '
        'txtEnetFolder
        '
        Me.txtEnetFolder.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEnetFolder.Location = New System.Drawing.Point(322, 39)
        Me.txtEnetFolder.Name = "txtEnetFolder"
        Me.txtEnetFolder.Size = New System.Drawing.Size(286, 25)
        Me.txtEnetFolder.TabIndex = 3
        '
        'btnEnetFolderBrowser
        '
        Me.btnEnetFolderBrowser.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnetFolderBrowser.Location = New System.Drawing.Point(614, 39)
        Me.btnEnetFolderBrowser.Name = "btnEnetFolderBrowser"
        Me.btnEnetFolderBrowser.Size = New System.Drawing.Size(30, 25)
        Me.btnEnetFolderBrowser.TabIndex = 4
        Me.btnEnetFolderBrowser.Text = "..."
        Me.btnEnetFolderBrowser.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnEnetFolderBrowser.UseVisualStyleBackColor = True
        '
        'btnEnetFolderDefault
        '
        Me.btnEnetFolderDefault.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnetFolderDefault.Location = New System.Drawing.Point(614, 70)
        Me.btnEnetFolderDefault.Name = "btnEnetFolderDefault"
        Me.btnEnetFolderDefault.Size = New System.Drawing.Size(80, 25)
        Me.btnEnetFolderDefault.TabIndex = 5
        Me.btnEnetFolderDefault.Text = "Default"
        Me.btnEnetFolderDefault.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnEnetFolderDefault.UseVisualStyleBackColor = True
        '
        'tablePagePartNumber
        '
        Me.tablePagePartNumber.Location = New System.Drawing.Point(4, 59)
        Me.tablePagePartNumber.Name = "tablePagePartNumber"
        Me.tablePagePartNumber.Size = New System.Drawing.Size(1225, 793)
        Me.tablePagePartNumber.TabIndex = 16
        Me.tablePagePartNumber.Tag = "PartNumber"
        Me.tablePagePartNumber.Text = "Part Number"
        Me.tablePagePartNumber.UseVisualStyleBackColor = True
        '
        'IM_tab
        '
        Me.IM_tab.ImageStream = CType(resources.GetObject("IM_tab.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.IM_tab.TransparentColor = System.Drawing.Color.Transparent
        Me.IM_tab.Images.SetKeyName(0, "cogs.png")
        '
        'BG_updates
        '
        Me.BG_updates.WorkerReportsProgress = True
        '
        'BG_DL_update
        '
        Me.BG_DL_update.WorkerReportsProgress = True
        '
        'BG_checkfilesize
        '
        Me.BG_checkfilesize.WorkerReportsProgress = True
        '
        'btnStopMBServer
        '
        Me.btnStopMBServer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStopMBServer.Image = Global.CSI_Reporting_Application.My.Resources.Resources.player_stop
        Me.btnStopMBServer.Location = New System.Drawing.Point(325, 107)
        Me.btnStopMBServer.Name = "btnStopMBServer"
        Me.btnStopMBServer.Size = New System.Drawing.Size(40, 30)
        Me.btnStopMBServer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnStopMBServer.TabIndex = 26
        Me.btnStopMBServer.TabStop = False
        '
        'SetupForm2
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1233, 881)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TabControl_DashBoard)
        Me.Controls.Add(Me.Button3)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SetupForm2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CSIFLEX Server"
        Me.CMS_GridEdit.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.CMS_GroupEdit_NEW.ResumeLayout(False)
        Me.tablePageUsers.ResumeLayout(False)
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GB_Users.ResumeLayout(False)
        Me.GB_Users.PerformLayout()
        CType(Me.btnEditPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowPassword, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tablePageLicense.ResumeLayout(False)
        Me.tablePageLicense.PerformLayout()
        CType(Me.aboutLogoPictBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tablePageCSICon.ResumeLayout(False)
        CType(Me.grvConnectors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tablePageDashboards.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel_dashConfig.ResumeLayout(False)
        Me.Panel_dashConfig.PerformLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RSS_GB.ResumeLayout(False)
        Me.RSS_GB.PerformLayout()
        Me.grpRSSMessageType.ResumeLayout(False)
        Me.grpRSSMessageType.PerformLayout()
        CType(Me.dgvRSSMessages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRSSBestCycleOn.ResumeLayout(False)
        Me.grpRSSBestCycleOn.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.CommonControls_GB.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.tablePageGeneral.ResumeLayout(False)
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox11.ResumeLayout(False)
        CType(Me.btnStartMBServer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnStartServ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnStopServ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnStartRepService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnStopRepService, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl_DashBoard.ResumeLayout(False)
        Me.tablePageEnet.ResumeLayout(False)
        Me.pnlMachines.ResumeLayout(False)
        Me.pnlMachines.PerformLayout()
        Me.gboxMachineSettings.ResumeLayout(False)
        CType(Me.gridviewMachines, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpEnetSettings.ResumeLayout(False)
        Me.grpEnetSettings.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnStopMBServer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CMS_GridEdit As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents add_menu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    'Friend WithEvents TableAdapterManager1 As CSI_Reporting_Application.CSI_authDataSetTableAdapters.TableAdapterManager
    Friend WithEvents BgWorker_CreateDB As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents timerServiceState As System.Windows.Forms.Timer
    Friend WithEvents BgWorker_importDB As System.ComponentModel.BackgroundWorker
    Friend WithEvents CMS_GroupEdit_NEW As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteMachineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tablePageGeneral As System.Windows.Forms.TabPage
    Friend WithEvents tablePageEnet As System.Windows.Forms.TabPage
    Friend WithEvents tablePageUsers As System.Windows.Forms.TabPage
    Friend WithEvents tablePageDashboards As System.Windows.Forms.TabPage
    Friend WithEvents tablePageCSIConn As System.Windows.Forms.TabPage
    Friend WithEvents tablePageCSICon As System.Windows.Forms.TabPage
    Friend WithEvents tablePagePartNumber As System.Windows.Forms.TabPage
    Friend WithEvents tablePageLicense As System.Windows.Forms.TabPage
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents treeviewMachines As System.Windows.Forms.TreeView
    Friend WithEvents treeViewUsers As System.Windows.Forms.TreeView
    Friend WithEvents btnAbout As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnLicenseRequest As System.Windows.Forms.Button
    Friend WithEvents btnDeleteConnector As System.Windows.Forms.Button
    Friend WithEvents btnEditConnector As System.Windows.Forms.Button
    Friend WithEvents grvConnectors As System.Windows.Forms.DataGridView
    Friend WithEvents btnAddConnector As System.Windows.Forms.Button
    Friend WithEvents TV_LivestatusMachine As System.Windows.Forms.TreeView
    Friend WithEvents treeviewDevices As System.Windows.Forms.TreeView
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents btnConfigureReports As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnDefaultEnetFolder As System.Windows.Forms.Button
    Friend WithEvents btnBrowserEnetFolder As System.Windows.Forms.Button
    Friend WithEvents btnReloadEnetSettings As System.Windows.Forms.Button
    Friend WithEvents txtGeneralEnetFolder As System.Windows.Forms.TextBox
    Friend WithEvents TabControl_DashBoard As System.Windows.Forms.TabControl
    Friend WithEvents gridviewMachines As System.Windows.Forms.DataGridView
    Friend WithEvents treeviewGroupsOfMachines As System.Windows.Forms.TreeView
    Friend WithEvents btnConfigureEmail As System.Windows.Forms.Button
    Friend WithEvents TSSL_DBCreation As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnConfigureReinmeter As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnLoadingCycleOn As System.Windows.Forms.Button
    Friend WithEvents btnMonitoringUnits As System.Windows.Forms.Button
    Friend WithEvents GroupBox11 As GroupBox
    Friend WithEvents lblServiceState As Label
    Friend WithEvents lblStartService As Label
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents btnPerformanceTuning As Button
    Friend WithEvents RTB_liste As RichTextBox
    Friend WithEvents aboutLogoPictBox As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents BTN_gensett As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents LBL_IP As Label
    Friend WithEvents txtPort As TextBox
    Friend WithEvents btnChangeIpAddress As Button
    Friend WithEvents btnSaveWebServerPort As Button
    Friend WithEvents btnImportSettings As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents CB__DeviceType As ComboBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents IF_OFF As RadioButton
    Friend WithEvents IF_Rot_CB As CheckBox
    Friend WithEvents IF_ON As RadioButton
    Friend WithEvents Label19 As Label
    Friend WithEvents Url_TB As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents IFFullscreen_CB As CheckBox
    Friend WithEvents RSS_GB As GroupBox
    Friend WithEvents chkRSSMessageOnOff As CheckBox
    Friend WithEvents lblBestCycleOnMachine As Label
    Friend WithEvents btnRSSValidate As Button
    Friend WithEvents txtRSSUserMessageText As TextBox
    Friend WithEvents lblMessageText As Label
    Friend WithEvents grpRSSMessageType As GroupBox
    Friend WithEvents rdbRSSSystemMessage As RadioButton
    Friend WithEvents rdbRSSUserMessage As RadioButton
    Friend WithEvents btnRSSDeleteMessage As Button
    Friend WithEvents btnRSSAddMessage As Button
    Friend WithEvents btnRSSMessageDown As Button
    Friend WithEvents btnRSSMessageUp As Button
    Friend WithEvents dgvRSSMessages As DataGridView
    Friend WithEvents Messages As DataGridViewTextBoxColumn
    Friend WithEvents Priority As DataGridViewTextBoxColumn
    Friend WithEvents grpRSSBestCycleOn As GroupBox
    Friend WithEvents rdbRSSMonth As RadioButton
    Friend WithEvents rdbRSSWeek As RadioButton
    Friend WithEvents rdbRSSDay As RadioButton
    Friend WithEvents chkRSSBestCycleOn As CheckBox
    Friend WithEvents txtDeviceName As TextBox
    Friend WithEvents CommonControls_GB As GroupBox
    Friend WithEvents TB_format As TextBox
    Friend WithEvents Label_format As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents LSD_TB As CheckBox
    Friend WithEvents Degree_CB As ComboBox
    Friend WithEvents Sec_TB As TextBox
    Friend WithEvents City_TB As TextBox
    Friend WithEvents LSFullscreen_CB As CheckBox
    Friend WithEvents Label24 As Label
    Friend WithEvents Temp_CB As CheckBox
    Friend WithEvents txtLogoPath As TextBox
    Friend WithEvents Browse_Logo As Button
    Friend WithEvents Logo_TB As CheckBox
    Private WithEvents DateTime_TB As CheckBox
    Friend WithEvents IP_TB As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents GB_Users As GroupBox
    Friend WithEvents gboxUserPermitions As GroupBox
    Friend WithEvents txtUserExtention As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtUserDept As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtUserTitle As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtUserRefID As TextBox
    Friend WithEvents Label32 As Label
    Friend WithEvents txtUserEmail As TextBox
    Friend WithEvents Label31 As Label
    Friend WithEvents btnSaveUser As Button
    Friend WithEvents cmbUserType As ComboBox
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtUserName As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents BTN_ping As Button
    Friend WithEvents IM_tab As ImageList
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents Panel_dashConfig As Panel
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents LBL_srvResult As Label
    Friend WithEvents btnCheckWebServer As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents BG_updates As System.ComponentModel.BackgroundWorker
    Friend WithEvents BG_DL_update As System.ComponentModel.BackgroundWorker
    Friend WithEvents BG_installUdate As System.ComponentModel.BackgroundWorker
    Friend WithEvents BG_checkfilesize As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label21 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents btnPerformanceClear As Button
    Friend WithEvents LBL_IPChange As Label
    Friend WithEvents btnShowPassword As PictureBox
    Friend WithEvents btnAddUser As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents chkLogoBarHidden As CheckBox
    Friend WithEvents chkDarkTheme As CheckBox
    Friend WithEvents btnAdvSettings As Button
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents GroupBox10 As GroupBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label27 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents GroupBox12 As GroupBox
    Friend WithEvents btnEditPassword As PictureBox
    Friend WithEvents btnStopServ As PictureBox
    Friend WithEvents btnStartServ As PictureBox
    Friend WithEvents lblTemperature As Label
    Friend WithEvents grpEnetSettings As GroupBox
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents txtEnetFolder As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents btnEnetFolderDefault As Button
    Friend WithEvents btnEnetFolderBrowser As Button
    Friend WithEvents lstEnetMachines As ListView
    Friend WithEvents txtEnetHttpIp As TextBox
    Friend WithEvents Label34 As Label
    Friend WithEvents btnCheckEnetHttpIp As Button
    Friend WithEvents lblEnetCheckResult As Label
    Friend WithEvents Column1 As ColumnHeader
    Friend WithEvents btnEnetSaveChanges As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents lstEnetYears As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents lblEnetFolderStatus As Label
    Friend WithEvents Label35 As Label
    Friend WithEvents btnLoadEnetHistory As Button
    Friend WithEvents pnlMachines As Panel
    Friend WithEvents btnEnetSettings As Button
    Friend WithEvents btnEnetCancelChanges As Button
    Friend WithEvents gboxMachineSettings As GroupBox
    Friend WithEvents pnlMachineSettings As Panel

    Friend WithEvents labelMachineId As Label
    Friend WithEvents lblMachineId As Label
    Friend WithEvents labelMachineName As Label
    Friend WithEvents lblMachineName As Label
    Friend WithEvents labelEnetPos As Label
    Friend WithEvents lblEnetPos As Label
    Friend WithEvents labelDepartment As Label
    Friend WithEvents lblDepartment As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents lblProtocol As Label
    Friend WithEvents Label41 As Label
    Friend WithEvents lblFtpFileName As Label
    Friend WithEvents labelAlwaysRecCycleOn As Label
    Friend WithEvents lblAlwaysRecCycleOn As Label
    Friend WithEvents labelCycleOnCommand As Label
    Friend WithEvents lblCycleOnCommand As Label
    Friend WithEvents labelCycleOffCommand As Label
    Friend WithEvents lblCycleOffCommand As Label
    Friend WithEvents labelPartNumberCommand As Label
    Friend WithEvents lblPartNumberCommand As Label

    Friend WithEvents chkEnetAllPositions As CheckBox
    Friend WithEvents btnStartRepService As PictureBox
    Friend WithEvents btnStopRepService As PictureBox
    Friend WithEvents Label37 As Label
    Friend WithEvents lblReportServiceState As Label
    Friend WithEvents txtDisplayName As TextBox
    Friend WithEvents lblUserPermitions As Label
    Friend WithEvents Label42 As Label
    Friend WithEvents Label43 As Label
    Friend WithEvents cmbStartWeek As ComboBox
    Friend WithEvents lblQttMachines As Label
    Friend WithEvents Label45 As Label
    Friend WithEvents cmbImportGroups As ComboBox
    Friend WithEvents Id As DataGridViewTextBoxColumn
    Friend WithEvents Display As DataGridViewTextBoxColumn
    Friend WithEvents Machines As DataGridViewTextBoxColumn
    Friend WithEvents enetMachineName As DataGridViewTextBoxColumn
    Friend WithEvents MachineLabel As DataGridViewTextBoxColumn
    Friend WithEvents Source As DataGridViewTextBoxColumn
    Friend WithEvents Check As DataGridViewButtonColumn
    Friend WithEvents Connection As DataGridViewTextBoxColumn
    Friend WithEvents Dailytarget As DataGridViewTextBoxColumn
    Friend WithEvents Weeklytarget As DataGridViewTextBoxColumn
    Friend WithEvents Monthlytarget As DataGridViewTextBoxColumn
    Friend WithEvents IsMonitored As DataGridViewTextBoxColumn
    Friend WithEvents chkEditTimeline As CheckBox
    Friend WithEvents chkEditPartNumber As CheckBox
    Friend WithEvents btnStartMBServer As PictureBox
    Friend WithEvents Label47 As Label
    Friend WithEvents lblMBServer As Label
    Friend WithEvents btnStopMBServer As PictureBox
End Class

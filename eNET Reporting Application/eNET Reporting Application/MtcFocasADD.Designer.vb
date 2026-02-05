<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MtcFocasADD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MtcFocasADD))
        Me.tabControlConnector = New System.Windows.Forms.TabControl()
        Me.tabPageMtc = New System.Windows.Forms.TabPage()
        Me.txtMtcMachineName = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnMtcFindMachine = New System.Windows.Forms.Button()
        Me.cbMtcEnetMachineName = New System.Windows.Forms.ComboBox()
        Me.cbMtcMachineName = New System.Windows.Forms.ComboBox()
        Me.txtMtcConnectorType = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMtcIpAddress = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnMtcClose = New System.Windows.Forms.Button()
        Me.btnMtcMore = New System.Windows.Forms.Button()
        Me.btnMtcApply = New System.Windows.Forms.Button()
        Me.tabPageFocas = New System.Windows.Forms.TabPage()
        Me.grpServices = New System.Windows.Forms.GroupBox()
        Me.btnStopAgent = New System.Windows.Forms.PictureBox()
        Me.lblAdapterServState = New System.Windows.Forms.Label()
        Me.btnStopAdapter = New System.Windows.Forms.PictureBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnStartAgent = New System.Windows.Forms.PictureBox()
        Me.btnRefreshAdapter = New System.Windows.Forms.PictureBox()
        Me.btnStartAdapter = New System.Windows.Forms.PictureBox()
        Me.lblAgentServState = New System.Windows.Forms.Label()
        Me.btnRefreshAgent = New System.Windows.Forms.PictureBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.btnFocasMore = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BTN_Save = New System.Windows.Forms.Button()
        Me.TB_AgentPort = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TB_AgentIPAdd = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.BTN_MoreSettings = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtFocasManufacturer = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbFocasControllerType = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtFocasPort = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnFocasPing = New System.Windows.Forms.Button()
        Me.btnFocasClose = New System.Windows.Forms.Button()
        Me.btnFocasEdit = New System.Windows.Forms.Button()
        Me.btnFocasOk = New System.Windows.Forms.Button()
        Me.cbFocasEnetMachineName = New System.Windows.Forms.ComboBox()
        Me.txtFocasMachineName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtFocasIpAddress = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtFocasConnectionType = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PictureBox_MTConnect = New System.Windows.Forms.PictureBox()
        Me.PictureBox_Focas = New System.Windows.Forms.PictureBox()
        Me.cmbFocasMonitoringUnit = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cmbMtcMonitoringUnit = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.tabControlConnector.SuspendLayout()
        Me.tabPageMtc.SuspendLayout()
        Me.tabPageFocas.SuspendLayout()
        Me.grpServices.SuspendLayout()
        CType(Me.btnStopAgent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnStopAdapter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnStartAgent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefreshAdapter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnStartAdapter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefreshAgent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox_MTConnect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox_Focas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabControlConnector
        '
        Me.tabControlConnector.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabControlConnector.Controls.Add(Me.tabPageMtc)
        Me.tabControlConnector.Controls.Add(Me.tabPageFocas)
        Me.tabControlConnector.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabControlConnector.HotTrack = True
        Me.tabControlConnector.ImageList = Me.ImageList1
        Me.tabControlConnector.ItemSize = New System.Drawing.Size(150, 50)
        Me.tabControlConnector.Location = New System.Drawing.Point(-1, 3)
        Me.tabControlConnector.Multiline = True
        Me.tabControlConnector.Name = "tabControlConnector"
        Me.tabControlConnector.SelectedIndex = 0
        Me.tabControlConnector.Size = New System.Drawing.Size(659, 663)
        Me.tabControlConnector.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tabControlConnector.TabIndex = 0
        '
        'tabPageMtc
        '
        Me.tabPageMtc.BackColor = System.Drawing.Color.White
        Me.tabPageMtc.Controls.Add(Me.cmbMtcMonitoringUnit)
        Me.tabPageMtc.Controls.Add(Me.Label25)
        Me.tabPageMtc.Controls.Add(Me.txtMtcMachineName)
        Me.tabPageMtc.Controls.Add(Me.Label23)
        Me.tabPageMtc.Controls.Add(Me.Label19)
        Me.tabPageMtc.Controls.Add(Me.Label18)
        Me.tabPageMtc.Controls.Add(Me.Label17)
        Me.tabPageMtc.Controls.Add(Me.btnMtcFindMachine)
        Me.tabPageMtc.Controls.Add(Me.cbMtcEnetMachineName)
        Me.tabPageMtc.Controls.Add(Me.cbMtcMachineName)
        Me.tabPageMtc.Controls.Add(Me.txtMtcConnectorType)
        Me.tabPageMtc.Controls.Add(Me.Label4)
        Me.tabPageMtc.Controls.Add(Me.Label3)
        Me.tabPageMtc.Controls.Add(Me.Label2)
        Me.tabPageMtc.Controls.Add(Me.txtMtcIpAddress)
        Me.tabPageMtc.Controls.Add(Me.Label1)
        Me.tabPageMtc.Controls.Add(Me.btnMtcClose)
        Me.tabPageMtc.Controls.Add(Me.btnMtcMore)
        Me.tabPageMtc.Controls.Add(Me.btnMtcApply)
        Me.tabPageMtc.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabPageMtc.Location = New System.Drawing.Point(4, 54)
        Me.tabPageMtc.Name = "tabPageMtc"
        Me.tabPageMtc.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageMtc.Size = New System.Drawing.Size(651, 605)
        Me.tabPageMtc.TabIndex = 0
        Me.tabPageMtc.Text = "MTConnect"
        '
        'txtMtcMachineName
        '
        Me.txtMtcMachineName.Enabled = False
        Me.txtMtcMachineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMtcMachineName.Location = New System.Drawing.Point(62, 294)
        Me.txtMtcMachineName.Name = "txtMtcMachineName"
        Me.txtMtcMachineName.Size = New System.Drawing.Size(550, 23)
        Me.txtMtcMachineName.TabIndex = 45
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(60, 270)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(122, 21)
        Me.Label23.TabIndex = 44
        Me.Label23.Text = "Machine Name :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(170, 153)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(15, 19)
        Me.Label19.TabIndex = 43
        Me.Label19.Text = "*"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(247, 213)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(15, 19)
        Me.Label18.TabIndex = 42
        Me.Label18.Text = "*"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(146, 93)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(15, 19)
        Me.Label17.TabIndex = 41
        Me.Label17.Text = "*"
        '
        'btnMtcFindMachine
        '
        Me.btnMtcFindMachine.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnMtcFindMachine.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnMtcFindMachine.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMtcFindMachine.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnMtcFindMachine.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMtcFindMachine.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMtcFindMachine.Location = New System.Drawing.Point(485, 112)
        Me.btnMtcFindMachine.Name = "btnMtcFindMachine"
        Me.btnMtcFindMachine.Size = New System.Drawing.Size(127, 29)
        Me.btnMtcFindMachine.TabIndex = 8
        Me.btnMtcFindMachine.Text = "Find Machines"
        Me.btnMtcFindMachine.UseVisualStyleBackColor = False
        '
        'cbMtcEnetMachineName
        '
        Me.cbMtcEnetMachineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMtcEnetMachineName.FormattingEnabled = True
        Me.cbMtcEnetMachineName.ItemHeight = 16
        Me.cbMtcEnetMachineName.Location = New System.Drawing.Point(62, 235)
        Me.cbMtcEnetMachineName.Name = "cbMtcEnetMachineName"
        Me.cbMtcEnetMachineName.Size = New System.Drawing.Size(550, 24)
        Me.cbMtcEnetMachineName.TabIndex = 7
        '
        'cbMtcMachineName
        '
        Me.cbMtcMachineName.Enabled = False
        Me.cbMtcMachineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMtcMachineName.FormattingEnabled = True
        Me.cbMtcMachineName.Items.AddRange(New Object() {"Focas", "MTConnect"})
        Me.cbMtcMachineName.Location = New System.Drawing.Point(62, 175)
        Me.cbMtcMachineName.Name = "cbMtcMachineName"
        Me.cbMtcMachineName.Size = New System.Drawing.Size(550, 24)
        Me.cbMtcMachineName.TabIndex = 5
        '
        'txtMtcConnectorType
        '
        Me.txtMtcConnectorType.Enabled = False
        Me.txtMtcConnectorType.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMtcConnectorType.Location = New System.Drawing.Point(62, 54)
        Me.txtMtcConnectorType.Name = "txtMtcConnectorType"
        Me.txtMtcConnectorType.ReadOnly = True
        Me.txtMtcConnectorType.Size = New System.Drawing.Size(550, 23)
        Me.txtMtcConnectorType.TabIndex = 1
        Me.txtMtcConnectorType.Text = "MTConnect"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(60, 210)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(188, 21)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "eNETDNC Machine Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(60, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 21)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Connector Type :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(60, 150)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 21)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "MTC Machine"
        '
        'txtMtcIpAddress
        '
        Me.txtMtcIpAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMtcIpAddress.Location = New System.Drawing.Point(62, 115)
        Me.txtMtcIpAddress.Name = "txtMtcIpAddress"
        Me.txtMtcIpAddress.Size = New System.Drawing.Size(417, 23)
        Me.txtMtcIpAddress.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(60, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 21)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Machine IP :"
        '
        'btnMtcClose
        '
        Me.btnMtcClose.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnMtcClose.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnMtcClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMtcClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnMtcClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMtcClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMtcClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnMtcClose.Location = New System.Drawing.Point(485, 450)
        Me.btnMtcClose.Name = "btnMtcClose"
        Me.btnMtcClose.Size = New System.Drawing.Size(127, 29)
        Me.btnMtcClose.TabIndex = 11
        Me.btnMtcClose.Text = "Close"
        Me.btnMtcClose.UseVisualStyleBackColor = False
        '
        'btnMtcMore
        '
        Me.btnMtcMore.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnMtcMore.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnMtcMore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMtcMore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnMtcMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMtcMore.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMtcMore.Location = New System.Drawing.Point(219, 450)
        Me.btnMtcMore.Name = "btnMtcMore"
        Me.btnMtcMore.Size = New System.Drawing.Size(127, 29)
        Me.btnMtcMore.TabIndex = 9
        Me.btnMtcMore.Text = "More >"
        Me.btnMtcMore.UseVisualStyleBackColor = False
        '
        'btnMtcApply
        '
        Me.btnMtcApply.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnMtcApply.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnMtcApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMtcApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnMtcApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMtcApply.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMtcApply.Location = New System.Drawing.Point(352, 450)
        Me.btnMtcApply.Name = "btnMtcApply"
        Me.btnMtcApply.Size = New System.Drawing.Size(127, 29)
        Me.btnMtcApply.TabIndex = 10
        Me.btnMtcApply.Text = "Apply"
        Me.btnMtcApply.UseVisualStyleBackColor = False
        '
        'tabPageFocas
        '
        Me.tabPageFocas.BackColor = System.Drawing.Color.White
        Me.tabPageFocas.Controls.Add(Me.cmbFocasMonitoringUnit)
        Me.tabPageFocas.Controls.Add(Me.Label24)
        Me.tabPageFocas.Controls.Add(Me.grpServices)
        Me.tabPageFocas.Controls.Add(Me.btnFocasMore)
        Me.tabPageFocas.Controls.Add(Me.GroupBox1)
        Me.tabPageFocas.Controls.Add(Me.BTN_MoreSettings)
        Me.tabPageFocas.Controls.Add(Me.Label16)
        Me.tabPageFocas.Controls.Add(Me.Label15)
        Me.tabPageFocas.Controls.Add(Me.Label14)
        Me.tabPageFocas.Controls.Add(Me.Label12)
        Me.tabPageFocas.Controls.Add(Me.Label11)
        Me.tabPageFocas.Controls.Add(Me.txtFocasManufacturer)
        Me.tabPageFocas.Controls.Add(Me.Label13)
        Me.tabPageFocas.Controls.Add(Me.cbFocasControllerType)
        Me.tabPageFocas.Controls.Add(Me.Label10)
        Me.tabPageFocas.Controls.Add(Me.txtFocasPort)
        Me.tabPageFocas.Controls.Add(Me.Label9)
        Me.tabPageFocas.Controls.Add(Me.btnFocasPing)
        Me.tabPageFocas.Controls.Add(Me.btnFocasClose)
        Me.tabPageFocas.Controls.Add(Me.btnFocasEdit)
        Me.tabPageFocas.Controls.Add(Me.btnFocasOk)
        Me.tabPageFocas.Controls.Add(Me.cbFocasEnetMachineName)
        Me.tabPageFocas.Controls.Add(Me.txtFocasMachineName)
        Me.tabPageFocas.Controls.Add(Me.Label6)
        Me.tabPageFocas.Controls.Add(Me.Label7)
        Me.tabPageFocas.Controls.Add(Me.txtFocasIpAddress)
        Me.tabPageFocas.Controls.Add(Me.Label8)
        Me.tabPageFocas.Controls.Add(Me.txtFocasConnectionType)
        Me.tabPageFocas.Controls.Add(Me.Label5)
        Me.tabPageFocas.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabPageFocas.Location = New System.Drawing.Point(4, 54)
        Me.tabPageFocas.Name = "tabPageFocas"
        Me.tabPageFocas.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPageFocas.Size = New System.Drawing.Size(651, 605)
        Me.tabPageFocas.TabIndex = 1
        Me.tabPageFocas.Text = "Focas"
        '
        'grpServices
        '
        Me.grpServices.Controls.Add(Me.btnStopAgent)
        Me.grpServices.Controls.Add(Me.lblAdapterServState)
        Me.grpServices.Controls.Add(Me.btnStopAdapter)
        Me.grpServices.Controls.Add(Me.Label22)
        Me.grpServices.Controls.Add(Me.btnStartAgent)
        Me.grpServices.Controls.Add(Me.btnRefreshAdapter)
        Me.grpServices.Controls.Add(Me.btnStartAdapter)
        Me.grpServices.Controls.Add(Me.lblAgentServState)
        Me.grpServices.Controls.Add(Me.btnRefreshAgent)
        Me.grpServices.Controls.Add(Me.Label37)
        Me.grpServices.Location = New System.Drawing.Point(63, 495)
        Me.grpServices.Name = "grpServices"
        Me.grpServices.Size = New System.Drawing.Size(552, 104)
        Me.grpServices.TabIndex = 60
        Me.grpServices.TabStop = False
        Me.grpServices.Visible = False
        '
        'btnStopAgent
        '
        Me.btnStopAgent.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStopAgent.Image = Global.CSI_Reporting_Application.My.Resources.Resources.player_stop
        Me.btnStopAgent.Location = New System.Drawing.Point(466, 64)
        Me.btnStopAgent.Name = "btnStopAgent"
        Me.btnStopAgent.Size = New System.Drawing.Size(40, 30)
        Me.btnStopAgent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnStopAgent.TabIndex = 59
        Me.btnStopAgent.TabStop = False
        '
        'lblAdapterServState
        '
        Me.lblAdapterServState.BackColor = System.Drawing.Color.LightGray
        Me.lblAdapterServState.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdapterServState.Location = New System.Drawing.Point(335, 27)
        Me.lblAdapterServState.Name = "lblAdapterServState"
        Me.lblAdapterServState.Size = New System.Drawing.Size(120, 24)
        Me.lblAdapterServState.TabIndex = 50
        Me.lblAdapterServState.Text = "serviceState"
        Me.lblAdapterServState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnStopAdapter
        '
        Me.btnStopAdapter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStopAdapter.Image = Global.CSI_Reporting_Application.My.Resources.Resources.player_stop
        Me.btnStopAdapter.Location = New System.Drawing.Point(466, 24)
        Me.btnStopAdapter.Name = "btnStopAdapter"
        Me.btnStopAdapter.Size = New System.Drawing.Size(40, 30)
        Me.btnStopAdapter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnStopAdapter.TabIndex = 58
        Me.btnStopAdapter.TabStop = False
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(149, 28)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(180, 22)
        Me.Label22.TabIndex = 51
        Me.Label22.Text = "Adapter"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnStartAgent
        '
        Me.btnStartAgent.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStartAgent.Image = Global.CSI_Reporting_Application.My.Resources.Resources.player_play
        Me.btnStartAgent.Location = New System.Drawing.Point(466, 64)
        Me.btnStartAgent.Name = "btnStartAgent"
        Me.btnStartAgent.Size = New System.Drawing.Size(40, 30)
        Me.btnStartAgent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnStartAgent.TabIndex = 57
        Me.btnStartAgent.TabStop = False
        '
        'btnRefreshAdapter
        '
        Me.btnRefreshAdapter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefreshAdapter.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Refresh
        Me.btnRefreshAdapter.Location = New System.Drawing.Point(512, 24)
        Me.btnRefreshAdapter.Name = "btnRefreshAdapter"
        Me.btnRefreshAdapter.Size = New System.Drawing.Size(30, 30)
        Me.btnRefreshAdapter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnRefreshAdapter.TabIndex = 52
        Me.btnRefreshAdapter.TabStop = False
        '
        'btnStartAdapter
        '
        Me.btnStartAdapter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStartAdapter.Image = Global.CSI_Reporting_Application.My.Resources.Resources.player_play
        Me.btnStartAdapter.Location = New System.Drawing.Point(466, 24)
        Me.btnStartAdapter.Name = "btnStartAdapter"
        Me.btnStartAdapter.Size = New System.Drawing.Size(40, 30)
        Me.btnStartAdapter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnStartAdapter.TabIndex = 56
        Me.btnStartAdapter.TabStop = False
        '
        'lblAgentServState
        '
        Me.lblAgentServState.BackColor = System.Drawing.Color.LightGray
        Me.lblAgentServState.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAgentServState.Location = New System.Drawing.Point(335, 67)
        Me.lblAgentServState.Name = "lblAgentServState"
        Me.lblAgentServState.Size = New System.Drawing.Size(120, 24)
        Me.lblAgentServState.TabIndex = 53
        Me.lblAgentServState.Text = "serviceState"
        Me.lblAgentServState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRefreshAgent
        '
        Me.btnRefreshAgent.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefreshAgent.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Refresh
        Me.btnRefreshAgent.Location = New System.Drawing.Point(512, 64)
        Me.btnRefreshAgent.Name = "btnRefreshAgent"
        Me.btnRefreshAgent.Size = New System.Drawing.Size(30, 30)
        Me.btnRefreshAgent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnRefreshAgent.TabIndex = 55
        Me.btnRefreshAgent.TabStop = False
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(149, 68)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(180, 22)
        Me.Label37.TabIndex = 54
        Me.Label37.Text = "Agent"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnFocasMore
        '
        Me.btnFocasMore.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnFocasMore.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnFocasMore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFocasMore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnFocasMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFocasMore.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFocasMore.Location = New System.Drawing.Point(348, 450)
        Me.btnFocasMore.Name = "btnFocasMore"
        Me.btnFocasMore.Size = New System.Drawing.Size(127, 29)
        Me.btnFocasMore.TabIndex = 49
        Me.btnFocasMore.Text = "More >"
        Me.btnFocasMore.UseVisualStyleBackColor = False
        Me.btnFocasMore.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BTN_Save)
        Me.GroupBox1.Controls.Add(Me.TB_AgentPort)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.TB_AgentIPAdd)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 671)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(618, 229)
        Me.GroupBox1.TabIndex = 48
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Agent Settings"
        Me.GroupBox1.Visible = False
        '
        'BTN_Save
        '
        Me.BTN_Save.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Save.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Save.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Save.Location = New System.Drawing.Point(480, 178)
        Me.BTN_Save.Name = "BTN_Save"
        Me.BTN_Save.Size = New System.Drawing.Size(127, 29)
        Me.BTN_Save.TabIndex = 49
        Me.BTN_Save.Text = "Save"
        Me.BTN_Save.UseVisualStyleBackColor = False
        '
        'TB_AgentPort
        '
        Me.TB_AgentPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_AgentPort.Location = New System.Drawing.Point(54, 133)
        Me.TB_AgentPort.Name = "TB_AgentPort"
        Me.TB_AgentPort.Size = New System.Drawing.Size(553, 23)
        Me.TB_AgentPort.TabIndex = 52
        Me.TB_AgentPort.Text = "5000"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(50, 109)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(94, 21)
        Me.Label21.TabIndex = 51
        Me.Label21.Text = "Agent Port : "
        '
        'TB_AgentIPAdd
        '
        Me.TB_AgentIPAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_AgentIPAdd.Location = New System.Drawing.Point(52, 66)
        Me.TB_AgentIPAdd.Name = "TB_AgentIPAdd"
        Me.TB_AgentIPAdd.Size = New System.Drawing.Size(553, 23)
        Me.TB_AgentIPAdd.TabIndex = 50
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(48, 42)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(135, 21)
        Me.Label20.TabIndex = 49
        Me.Label20.Text = "Agent IP Address :"
        '
        'BTN_MoreSettings
        '
        Me.BTN_MoreSettings.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_MoreSettings.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_MoreSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_MoreSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_MoreSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_MoreSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_MoreSettings.Location = New System.Drawing.Point(63, 450)
        Me.BTN_MoreSettings.Name = "BTN_MoreSettings"
        Me.BTN_MoreSettings.Size = New System.Drawing.Size(127, 29)
        Me.BTN_MoreSettings.TabIndex = 45
        Me.BTN_MoreSettings.Text = "More Settings"
        Me.BTN_MoreSettings.UseVisualStyleBackColor = False
        Me.BTN_MoreSettings.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(60, 90)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(136, 21)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Focas Machine IP :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(193, 93)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(15, 19)
        Me.Label11.TabIndex = 40
        Me.Label11.Text = "*"
        '
        'txtFocasIpAddress
        '
        Me.txtFocasIpAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFocasIpAddress.Location = New System.Drawing.Point(60, 115)
        Me.txtFocasIpAddress.Name = "txtFocasIpAddress"
        Me.txtFocasIpAddress.Size = New System.Drawing.Size(245, 23)
        Me.txtFocasIpAddress.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(314, 90)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 21)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Focas Port :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(397, 93)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(15, 19)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "*"
        '
        'txtFocasPort
        '
        Me.txtFocasPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFocasPort.Location = New System.Drawing.Point(318, 115)
        Me.txtFocasPort.Name = "txtFocasPort"
        Me.txtFocasPort.Size = New System.Drawing.Size(115, 23)
        Me.txtFocasPort.TabIndex = 5
        Me.txtFocasPort.Text = "8193"
        '
        'btnFocasPing
        '
        Me.btnFocasPing.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnFocasPing.Enabled = False
        Me.btnFocasPing.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnFocasPing.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFocasPing.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnFocasPing.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFocasPing.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFocasPing.Location = New System.Drawing.Point(484, 112)
        Me.btnFocasPing.Name = "btnFocasPing"
        Me.btnFocasPing.Size = New System.Drawing.Size(126, 29)
        Me.btnFocasPing.TabIndex = 6
        Me.btnFocasPing.Text = "Ping"
        Me.btnFocasPing.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(60, 150)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(162, 21)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "eNET Machine Name :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(220, 153)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(15, 19)
        Me.Label14.TabIndex = 42
        Me.Label14.Text = "*"
        '
        'cbFocasEnetMachineName
        '
        Me.cbFocasEnetMachineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFocasEnetMachineName.FormattingEnabled = True
        Me.cbFocasEnetMachineName.ItemHeight = 16
        Me.cbFocasEnetMachineName.Location = New System.Drawing.Point(60, 174)
        Me.cbFocasEnetMachineName.Name = "cbFocasEnetMachineName"
        Me.cbFocasEnetMachineName.Size = New System.Drawing.Size(550, 24)
        Me.cbFocasEnetMachineName.TabIndex = 39
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(61, 210)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(122, 21)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Machine Name :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(184, 213)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(15, 19)
        Me.Label15.TabIndex = 43
        Me.Label15.Text = "*"
        '
        'txtFocasMachineName
        '
        Me.txtFocasMachineName.Enabled = False
        Me.txtFocasMachineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFocasMachineName.Location = New System.Drawing.Point(61, 235)
        Me.txtFocasMachineName.Name = "txtFocasMachineName"
        Me.txtFocasMachineName.Size = New System.Drawing.Size(550, 23)
        Me.txtFocasMachineName.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AccessibleDescription = " "
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(61, 270)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(127, 21)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Controller Type :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(187, 273)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(15, 19)
        Me.Label16.TabIndex = 44
        Me.Label16.Text = "*"
        '
        'txtFocasManufacturer
        '
        Me.txtFocasManufacturer.Enabled = False
        Me.txtFocasManufacturer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFocasManufacturer.Location = New System.Drawing.Point(60, 355)
        Me.txtFocasManufacturer.Name = "txtFocasManufacturer"
        Me.txtFocasManufacturer.Size = New System.Drawing.Size(550, 23)
        Me.txtFocasManufacturer.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(60, 330)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(111, 21)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Manufacturer :"
        '
        'cbFocasControllerType
        '
        Me.cbFocasControllerType.Enabled = False
        Me.cbFocasControllerType.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFocasControllerType.FormattingEnabled = True
        Me.cbFocasControllerType.Items.AddRange(New Object() {"0i", "0id", "15i", "16i", "18i", "30i", "31i"})
        Me.cbFocasControllerType.Location = New System.Drawing.Point(61, 294)
        Me.cbFocasControllerType.Name = "cbFocasControllerType"
        Me.cbFocasControllerType.Size = New System.Drawing.Size(550, 24)
        Me.cbFocasControllerType.TabIndex = 10
        '
        'btnFocasClose
        '
        Me.btnFocasClose.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnFocasClose.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnFocasClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFocasClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnFocasClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFocasClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFocasClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnFocasClose.Location = New System.Drawing.Point(484, 450)
        Me.btnFocasClose.Name = "btnFocasClose"
        Me.btnFocasClose.Size = New System.Drawing.Size(127, 29)
        Me.btnFocasClose.TabIndex = 17
        Me.btnFocasClose.Text = "Close"
        Me.btnFocasClose.UseVisualStyleBackColor = False
        '
        'btnFocasEdit
        '
        Me.btnFocasEdit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnFocasEdit.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnFocasEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFocasEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnFocasEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFocasEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFocasEdit.Location = New System.Drawing.Point(203, 450)
        Me.btnFocasEdit.Name = "btnFocasEdit"
        Me.btnFocasEdit.Size = New System.Drawing.Size(127, 29)
        Me.btnFocasEdit.TabIndex = 16
        Me.btnFocasEdit.Text = "Edit"
        Me.btnFocasEdit.UseVisualStyleBackColor = False
        '
        'btnFocasOk
        '
        Me.btnFocasOk.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnFocasOk.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnFocasOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFocasOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnFocasOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFocasOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFocasOk.Location = New System.Drawing.Point(348, 450)
        Me.btnFocasOk.Name = "btnFocasOk"
        Me.btnFocasOk.Size = New System.Drawing.Size(127, 29)
        Me.btnFocasOk.TabIndex = 15
        Me.btnFocasOk.Text = "Apply"
        Me.btnFocasOk.UseVisualStyleBackColor = False
        '
        'txtFocasConnectionType
        '
        Me.txtFocasConnectionType.Enabled = False
        Me.txtFocasConnectionType.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFocasConnectionType.Location = New System.Drawing.Point(60, 54)
        Me.txtFocasConnectionType.Name = "txtFocasConnectionType"
        Me.txtFocasConnectionType.ReadOnly = True
        Me.txtFocasConnectionType.Size = New System.Drawing.Size(550, 23)
        Me.txtFocasConnectionType.TabIndex = 1
        Me.txtFocasConnectionType.Text = "Focas"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(60, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(125, 21)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Connector Type :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "FANUC.jpg")
        Me.ImageList1.Images.SetKeyName(1, "MTConnectLogo.jpg")
        '
        'PictureBox_MTConnect
        '
        Me.PictureBox_MTConnect.Image = Global.CSI_Reporting_Application.My.Resources.Resources.MTConnectLogo
        Me.PictureBox_MTConnect.Location = New System.Drawing.Point(454, 5)
        Me.PictureBox_MTConnect.Name = "PictureBox_MTConnect"
        Me.PictureBox_MTConnect.Size = New System.Drawing.Size(200, 50)
        Me.PictureBox_MTConnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox_MTConnect.TabIndex = 3
        Me.PictureBox_MTConnect.TabStop = False
        '
        'PictureBox_Focas
        '
        Me.PictureBox_Focas.Image = Global.CSI_Reporting_Application.My.Resources.Resources.FANUC
        Me.PictureBox_Focas.Location = New System.Drawing.Point(454, 5)
        Me.PictureBox_Focas.Name = "PictureBox_Focas"
        Me.PictureBox_Focas.Size = New System.Drawing.Size(200, 50)
        Me.PictureBox_Focas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox_Focas.TabIndex = 4
        Me.PictureBox_Focas.TabStop = False
        Me.PictureBox_Focas.Visible = False
        '
        'cmbFocasMonitoringUnit
        '
        Me.cmbFocasMonitoringUnit.Enabled = False
        Me.cmbFocasMonitoringUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFocasMonitoringUnit.FormattingEnabled = True
        Me.cmbFocasMonitoringUnit.Location = New System.Drawing.Point(60, 414)
        Me.cmbFocasMonitoringUnit.Name = "cmbFocasMonitoringUnit"
        Me.cmbFocasMonitoringUnit.Size = New System.Drawing.Size(550, 24)
        Me.cmbFocasMonitoringUnit.TabIndex = 62
        '
        'Label24
        '
        Me.Label24.AccessibleDescription = " "
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(60, 390)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(128, 21)
        Me.Label24.TabIndex = 61
        Me.Label24.Text = "Monitoring Unit :"
        '
        'cmbMtcMonitoringUnit
        '
        Me.cmbMtcMonitoringUnit.Enabled = False
        Me.cmbMtcMonitoringUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMtcMonitoringUnit.FormattingEnabled = True
        Me.cmbMtcMonitoringUnit.Location = New System.Drawing.Point(62, 354)
        Me.cmbMtcMonitoringUnit.Name = "cmbMtcMonitoringUnit"
        Me.cmbMtcMonitoringUnit.Size = New System.Drawing.Size(550, 24)
        Me.cmbMtcMonitoringUnit.TabIndex = 64
        '
        'Label25
        '
        Me.Label25.AccessibleDescription = " "
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(60, 330)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(128, 21)
        Me.Label25.TabIndex = 63
        Me.Label25.Text = "Monitoring Unit :"
        '
        'MtcFocasADD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 673)
        Me.Controls.Add(Me.PictureBox_MTConnect)
        Me.Controls.Add(Me.PictureBox_Focas)
        Me.Controls.Add(Me.tabControlConnector)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MtcFocasADD"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CSI Connector "
        Me.tabControlConnector.ResumeLayout(False)
        Me.tabPageMtc.ResumeLayout(False)
        Me.tabPageMtc.PerformLayout()
        Me.tabPageFocas.ResumeLayout(False)
        Me.tabPageFocas.PerformLayout()
        Me.grpServices.ResumeLayout(False)
        CType(Me.btnStopAgent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnStopAdapter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnStartAgent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefreshAdapter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnStartAdapter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefreshAgent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox_MTConnect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox_Focas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabPageMtc As TabPage
    Friend WithEvents tabPageFocas As TabPage
    Friend WithEvents PictureBox_MTConnect As PictureBox
    Friend WithEvents PictureBox_Focas As PictureBox
    Friend WithEvents btnMtcClose As Button
    Friend WithEvents btnMtcMore As Button
    Friend WithEvents btnMtcApply As Button
    Friend WithEvents txtMtcConnectorType As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtMtcIpAddress As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cbMtcMachineName As ComboBox
    Friend WithEvents cbMtcEnetMachineName As ComboBox
    Friend WithEvents btnMtcFindMachine As Button
    Friend WithEvents txtFocasConnectionType As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cbFocasEnetMachineName As ComboBox
    Friend WithEvents txtFocasMachineName As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents btnFocasPing As Button
    Friend WithEvents btnFocasClose As Button
    Friend WithEvents btnFocasEdit As Button
    Friend WithEvents btnFocasOk As Button
    Friend WithEvents txtFocasIpAddress As TextBox
    Friend WithEvents txtFocasPort As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents cbFocasControllerType As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents tabControlConnector As TabControl
    Friend WithEvents txtFocasManufacturer As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents BTN_MoreSettings As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BTN_Save As Button
    Friend WithEvents TB_AgentPort As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents TB_AgentIPAdd As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents btnFocasMore As Button
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents btnRefreshAgent As PictureBox
    Friend WithEvents Label37 As Label
    Friend WithEvents lblAgentServState As Label
    Friend WithEvents btnRefreshAdapter As PictureBox
    Friend WithEvents Label22 As Label
    Friend WithEvents lblAdapterServState As Label
    Friend WithEvents btnStartAgent As PictureBox
    Friend WithEvents btnStartAdapter As PictureBox
    Friend WithEvents btnStopAgent As PictureBox
    Friend WithEvents btnStopAdapter As PictureBox
    Friend WithEvents grpServices As GroupBox
    Friend WithEvents txtMtcMachineName As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents cmbMtcMonitoringUnit As ComboBox
    Friend WithEvents Label25 As Label
    Friend WithEvents cmbFocasMonitoringUnit As ComboBox
    Friend WithEvents Label24 As Label
End Class

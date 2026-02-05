<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SetupForm
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetupForm))
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ContextMenuADD = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripadd = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.BTN_Cancel = New System.Windows.Forms.Button()
        Me.BTN_Ok = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TP_GeneralSetting = New System.Windows.Forms.TabPage()
        Me.BTN_Rainmeter = New System.Windows.Forms.Button()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Other_color = New System.Windows.Forms.Button()
        Me.UseEnetColors_checkbox = New System.Windows.Forms.CheckBox()
        Me.BTN_BrowseColor = New System.Windows.Forms.Button()
        Me.Color_Path = New System.Windows.Forms.TextBox()
        Me.BTN_StartupConfig = New System.Windows.Forms.Button()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.BTN_BrowseFolder = New System.Windows.Forms.Button()
        Me.TB_DefaultReportFolder = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.NUD_Refresh = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.NUD_TargetLine = New System.Windows.Forms.NumericUpDown()
        Me.BTN_Remove = New System.Windows.Forms.Button()
        Me.BTN_Color = New System.Windows.Forms.Button()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.TP_Folders = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BTN_BData = New System.Windows.Forms.Button()
        Me.BTN_BEnet = New System.Windows.Forms.Button()
        Me.BTN_eNETDNC_DefaultPath = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_eNETDNC = New System.Windows.Forms.TextBox()
        Me.BTN_Database_DefautlPath = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TB_Database = New System.Windows.Forms.TextBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.TP_Dates = New System.Windows.Forms.TabPage()
        Me.DTP_firstdate = New System.Windows.Forms.DateTimePicker()
        Me.First_date = New System.Windows.Forms.Label()
        Me.CB_BeginOfYear = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CB_LastDayOfWeek = New System.Windows.Forms.ComboBox()
        Me.CB_FirstDayOfWeek = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.TP_Sources = New System.Windows.Forms.TabPage()
        Me.DGV_Source = New System.Windows.Forms.DataGridView()
        Me.Display = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Machines = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Check = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.State = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TP_Dashboard = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.CHKB_UseCSIDahboard = New System.Windows.Forms.CheckBox()
        Me.CHKB_AutoScroll = New System.Windows.Forms.CheckBox()
        Me.TB_Port = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.BTN_Check = New System.Windows.Forms.Button()
        Me.TB_IpAdress = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TB_LocalIpAddress = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.TP_Version = New System.Windows.Forms.TabPage()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.PictureBox10 = New System.Windows.Forms.PictureBox()
        Me.BTN_LoadLicense = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ReportingDataset = New CSI_Reporting_Application.ReportingDataset()
        Me.Tbl_MachineNameBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BW_import_csv = New System.ComponentModel.BackgroundWorker()
        Me.ContextMenuADD.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP_GeneralSetting.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.NUD_Refresh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.NUD_TargetLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Folders.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Dates.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Sources.SuspendLayout()
        CType(Me.DGV_Source, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Dashboard.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Version.SuspendLayout()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportingDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tbl_MachineNameBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "GraphColors.sys"
        Me.OpenFileDialog1.Title = "Select The eNET color file"
        '
        'ContextMenuADD
        '
        Me.ContextMenuADD.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem
        Me.ContextMenuADD.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuADD.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripadd})
        Me.ContextMenuADD.Name = "ContextMenuStrip1"
        Me.ContextMenuADD.Size = New System.Drawing.Size(166, 28)
        '
        'ToolStripadd
        '
        Me.ToolStripadd.Name = "ToolStripadd"
        Me.ToolStripadd.Size = New System.Drawing.Size(165, 24)
        Me.ToolStripadd.Text = "Add a source"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(105, 28)
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(104, 24)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(531, 445)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(168, 29)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Cancel (without saving )"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button14
        '
        Me.Button14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button14.Location = New System.Drawing.Point(324, 445)
        Me.Button14.Margin = New System.Windows.Forms.Padding(4)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(184, 29)
        Me.Button14.TabIndex = 9
        Me.Button14.Text = "Search MTConnect Agent"
        Me.Button14.UseVisualStyleBackColor = True
        Me.Button14.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.BTN_Cancel)
        Me.Panel3.Controls.Add(Me.BTN_Ok)
        Me.Panel3.Location = New System.Drawing.Point(356, 439)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(850, 46)
        Me.Panel3.TabIndex = 8
        '
        'BTN_Cancel
        '
        Me.BTN_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Cancel.Location = New System.Drawing.Point(755, 0)
        Me.BTN_Cancel.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Cancel.Name = "BTN_Cancel"
        Me.BTN_Cancel.Size = New System.Drawing.Size(85, 46)
        Me.BTN_Cancel.TabIndex = 8
        Me.BTN_Cancel.Text = "Cancel"
        Me.BTN_Cancel.UseVisualStyleBackColor = True
        '
        'BTN_Ok
        '
        Me.BTN_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Ok.Location = New System.Drawing.Point(670, 0)
        Me.BTN_Ok.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Ok.Name = "BTN_Ok"
        Me.BTN_Ok.Size = New System.Drawing.Size(78, 46)
        Me.BTN_Ok.TabIndex = 7
        Me.BTN_Ok.Text = "OK"
        Me.BTN_Ok.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureBox4)
        Me.Panel1.Controls.Add(Me.PictureBox3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(4, 4)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(344, 427)
        Me.Panel1.TabIndex = 6
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.CSI_Reporting_Application.My.Resources.Resources.Repaire
        Me.PictureBox4.Location = New System.Drawing.Point(221, 309)
        Me.PictureBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(122, 112)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 8
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.CSI_Reporting_Application.My.Resources.Resources.CSIFLEX_logo
        Me.PictureBox3.Location = New System.Drawing.Point(-4, 5)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(344, 420)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 7
        Me.PictureBox3.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.13223!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.86777!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel3, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TabControl1, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1210, 489)
        Me.TableLayoutPanel1.TabIndex = 10
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TP_GeneralSetting)
        Me.TabControl1.Controls.Add(Me.TP_Folders)
        Me.TabControl1.Controls.Add(Me.TP_Dates)
        Me.TabControl1.Controls.Add(Me.TP_Sources)
        Me.TabControl1.Controls.Add(Me.TP_Dashboard)
        Me.TabControl1.Controls.Add(Me.TP_Version)
        Me.TabControl1.Location = New System.Drawing.Point(356, 4)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(850, 419)
        Me.TabControl1.TabIndex = 5
        '
        'TP_GeneralSetting
        '
        Me.TP_GeneralSetting.Controls.Add(Me.BTN_Rainmeter)
        Me.TP_GeneralSetting.Controls.Add(Me.GroupBox9)
        Me.TP_GeneralSetting.Controls.Add(Me.BTN_StartupConfig)
        Me.TP_GeneralSetting.Controls.Add(Me.GroupBox8)
        Me.TP_GeneralSetting.Controls.Add(Me.GroupBox7)
        Me.TP_GeneralSetting.Controls.Add(Me.GroupBox5)
        Me.TP_GeneralSetting.Controls.Add(Me.PictureBox8)
        Me.TP_GeneralSetting.Location = New System.Drawing.Point(4, 25)
        Me.TP_GeneralSetting.Margin = New System.Windows.Forms.Padding(4)
        Me.TP_GeneralSetting.Name = "TP_GeneralSetting"
        Me.TP_GeneralSetting.Padding = New System.Windows.Forms.Padding(4)
        Me.TP_GeneralSetting.Size = New System.Drawing.Size(842, 390)
        Me.TP_GeneralSetting.TabIndex = 6
        Me.TP_GeneralSetting.Text = "General Settings"
        Me.TP_GeneralSetting.UseVisualStyleBackColor = True
        '
        'BTN_Rainmeter
        '
        Me.BTN_Rainmeter.Location = New System.Drawing.Point(631, 18)
        Me.BTN_Rainmeter.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Rainmeter.Name = "BTN_Rainmeter"
        Me.BTN_Rainmeter.Size = New System.Drawing.Size(204, 84)
        Me.BTN_Rainmeter.TabIndex = 262
        Me.BTN_Rainmeter.Text = "Rainmeter"
        Me.BTN_Rainmeter.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Other_color)
        Me.GroupBox9.Controls.Add(Me.UseEnetColors_checkbox)
        Me.GroupBox9.Controls.Add(Me.BTN_BrowseColor)
        Me.GroupBox9.Controls.Add(Me.Color_Path)
        Me.GroupBox9.Location = New System.Drawing.Point(8, 109)
        Me.GroupBox9.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox9.Size = New System.Drawing.Size(301, 97)
        Me.GroupBox9.TabIndex = 261
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Status Colors"
        '
        'Other_color
        '
        Me.Other_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Other_color.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Other_color.Location = New System.Drawing.Point(136, 12)
        Me.Other_color.Name = "Other_color"
        Me.Other_color.Size = New System.Drawing.Size(157, 48)
        Me.Other_color.TabIndex = 43
        Me.Other_color.Text = "Aggregate events color ('Other' status)"
        Me.Other_color.UseVisualStyleBackColor = True
        '
        'UseEnetColors_checkbox
        '
        Me.UseEnetColors_checkbox.Location = New System.Drawing.Point(8, 26)
        Me.UseEnetColors_checkbox.Name = "UseEnetColors_checkbox"
        Me.UseEnetColors_checkbox.Size = New System.Drawing.Size(230, 24)
        Me.UseEnetColors_checkbox.TabIndex = 42
        Me.UseEnetColors_checkbox.Text = "Use Enet Colors"
        Me.UseEnetColors_checkbox.UseVisualStyleBackColor = True
        '
        'BTN_BrowseColor
        '
        Me.BTN_BrowseColor.Location = New System.Drawing.Point(245, 64)
        Me.BTN_BrowseColor.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_BrowseColor.Name = "BTN_BrowseColor"
        Me.BTN_BrowseColor.Size = New System.Drawing.Size(48, 29)
        Me.BTN_BrowseColor.TabIndex = 41
        Me.BTN_BrowseColor.Text = "..."
        Me.BTN_BrowseColor.UseVisualStyleBackColor = True
        '
        'Color_Path
        '
        Me.Color_Path.Location = New System.Drawing.Point(8, 67)
        Me.Color_Path.Margin = New System.Windows.Forms.Padding(4)
        Me.Color_Path.Name = "Color_Path"
        Me.Color_Path.Size = New System.Drawing.Size(229, 22)
        Me.Color_Path.TabIndex = 0
        '
        'BTN_StartupConfig
        '
        Me.BTN_StartupConfig.Location = New System.Drawing.Point(631, 118)
        Me.BTN_StartupConfig.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_StartupConfig.Name = "BTN_StartupConfig"
        Me.BTN_StartupConfig.Size = New System.Drawing.Size(204, 84)
        Me.BTN_StartupConfig.TabIndex = 257
        Me.BTN_StartupConfig.Text = "Startup configuration"
        Me.BTN_StartupConfig.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.BTN_BrowseFolder)
        Me.GroupBox8.Controls.Add(Me.TB_DefaultReportFolder)
        Me.GroupBox8.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox8.Size = New System.Drawing.Size(301, 94)
        Me.GroupBox8.TabIndex = 260
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Default reporting folder"
        '
        'BTN_BrowseFolder
        '
        Me.BTN_BrowseFolder.Location = New System.Drawing.Point(246, 38)
        Me.BTN_BrowseFolder.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_BrowseFolder.Name = "BTN_BrowseFolder"
        Me.BTN_BrowseFolder.Size = New System.Drawing.Size(48, 29)
        Me.BTN_BrowseFolder.TabIndex = 41
        Me.BTN_BrowseFolder.Text = "..."
        Me.BTN_BrowseFolder.UseVisualStyleBackColor = True
        '
        'TB_DefaultReportFolder
        '
        Me.TB_DefaultReportFolder.Location = New System.Drawing.Point(9, 41)
        Me.TB_DefaultReportFolder.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_DefaultReportFolder.Name = "TB_DefaultReportFolder"
        Me.TB_DefaultReportFolder.Size = New System.Drawing.Size(229, 22)
        Me.TB_DefaultReportFolder.TabIndex = 0
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.NUD_Refresh)
        Me.GroupBox7.Location = New System.Drawing.Point(316, 8)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Size = New System.Drawing.Size(310, 94)
        Me.GroupBox7.TabIndex = 260
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Tree view refresh rate (sec)"
        '
        'NUD_Refresh
        '
        Me.NUD_Refresh.Location = New System.Drawing.Point(73, 42)
        Me.NUD_Refresh.Margin = New System.Windows.Forms.Padding(4)
        Me.NUD_Refresh.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.NUD_Refresh.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NUD_Refresh.Name = "NUD_Refresh"
        Me.NUD_Refresh.Size = New System.Drawing.Size(150, 22)
        Me.NUD_Refresh.TabIndex = 1
        Me.NUD_Refresh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NUD_Refresh.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.NUD_TargetLine)
        Me.GroupBox5.Controls.Add(Me.BTN_Remove)
        Me.GroupBox5.Controls.Add(Me.BTN_Color)
        Me.GroupBox5.Location = New System.Drawing.Point(316, 109)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Size = New System.Drawing.Size(310, 97)
        Me.GroupBox5.TabIndex = 258
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Target line"
        '
        'NUD_TargetLine
        '
        Me.NUD_TargetLine.Location = New System.Drawing.Point(8, 38)
        Me.NUD_TargetLine.Margin = New System.Windows.Forms.Padding(4)
        Me.NUD_TargetLine.Name = "NUD_TargetLine"
        Me.NUD_TargetLine.Size = New System.Drawing.Size(62, 22)
        Me.NUD_TargetLine.TabIndex = 253
        Me.NUD_TargetLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BTN_Remove
        '
        Me.BTN_Remove.Location = New System.Drawing.Point(179, 34)
        Me.BTN_Remove.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Remove.Name = "BTN_Remove"
        Me.BTN_Remove.Size = New System.Drawing.Size(105, 29)
        Me.BTN_Remove.TabIndex = 255
        Me.BTN_Remove.Text = "Remove"
        Me.BTN_Remove.UseVisualStyleBackColor = True
        '
        'BTN_Color
        '
        Me.BTN_Color.Location = New System.Drawing.Point(106, 34)
        Me.BTN_Color.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Color.Name = "BTN_Color"
        Me.BTN_Color.Size = New System.Drawing.Size(46, 29)
        Me.BTN_Color.TabIndex = 256
        Me.BTN_Color.UseVisualStyleBackColor = True
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = Global.CSI_Reporting_Application.My.Resources.Resources.flow
        Me.PictureBox8.Location = New System.Drawing.Point(-65, 200)
        Me.PictureBox8.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(738, 221)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox8.TabIndex = 257
        Me.PictureBox8.TabStop = False
        '
        'TP_Folders
        '
        Me.TP_Folders.BackColor = System.Drawing.Color.White
        Me.TP_Folders.Controls.Add(Me.Panel2)
        Me.TP_Folders.Controls.Add(Me.GroupBox1)
        Me.TP_Folders.Controls.Add(Me.PictureBox5)
        Me.TP_Folders.Location = New System.Drawing.Point(4, 25)
        Me.TP_Folders.Margin = New System.Windows.Forms.Padding(4)
        Me.TP_Folders.Name = "TP_Folders"
        Me.TP_Folders.Padding = New System.Windows.Forms.Padding(4)
        Me.TP_Folders.Size = New System.Drawing.Size(842, 390)
        Me.TP_Folders.TabIndex = 0
        Me.TP_Folders.Text = "Folders"
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(0, 401)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(339, 84)
        Me.Panel2.TabIndex = 11
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BTN_BData)
        Me.GroupBox1.Controls.Add(Me.BTN_BEnet)
        Me.GroupBox1.Controls.Add(Me.BTN_eNETDNC_DefaultPath)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TB_eNETDNC)
        Me.GroupBox1.Controls.Add(Me.BTN_Database_DefautlPath)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TB_Database)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(58, 24)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(715, 214)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BTN_BData
        '
        Me.BTN_BData.Location = New System.Drawing.Point(529, 150)
        Me.BTN_BData.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_BData.Name = "BTN_BData"
        Me.BTN_BData.Size = New System.Drawing.Size(86, 36)
        Me.BTN_BData.TabIndex = 122
        Me.BTN_BData.Text = "Browse"
        Me.BTN_BData.UseVisualStyleBackColor = True
        '
        'BTN_BEnet
        '
        Me.BTN_BEnet.Location = New System.Drawing.Point(529, 68)
        Me.BTN_BEnet.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_BEnet.Name = "BTN_BEnet"
        Me.BTN_BEnet.Size = New System.Drawing.Size(86, 36)
        Me.BTN_BEnet.TabIndex = 121
        Me.BTN_BEnet.Text = "Browse"
        Me.BTN_BEnet.UseVisualStyleBackColor = True
        '
        'BTN_eNETDNC_DefaultPath
        '
        Me.BTN_eNETDNC_DefaultPath.Location = New System.Drawing.Point(621, 68)
        Me.BTN_eNETDNC_DefaultPath.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_eNETDNC_DefaultPath.Name = "BTN_eNETDNC_DefaultPath"
        Me.BTN_eNETDNC_DefaultPath.Size = New System.Drawing.Size(86, 36)
        Me.BTN_eNETDNC_DefaultPath.TabIndex = 120
        Me.BTN_eNETDNC_DefaultPath.Text = "Default"
        Me.BTN_eNETDNC_DefaultPath.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 39)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(256, 28)
        Me.Label1.TabIndex = 110
        Me.Label1.Text = "Specify the eNETDNC folder"
        '
        'TB_eNETDNC
        '
        Me.TB_eNETDNC.Location = New System.Drawing.Point(20, 69)
        Me.TB_eNETDNC.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_eNETDNC.Name = "TB_eNETDNC"
        Me.TB_eNETDNC.Size = New System.Drawing.Size(500, 34)
        Me.TB_eNETDNC.TabIndex = 1
        '
        'BTN_Database_DefautlPath
        '
        Me.BTN_Database_DefautlPath.Location = New System.Drawing.Point(621, 150)
        Me.BTN_Database_DefautlPath.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Database_DefautlPath.Name = "BTN_Database_DefautlPath"
        Me.BTN_Database_DefautlPath.Size = New System.Drawing.Size(86, 36)
        Me.BTN_Database_DefautlPath.TabIndex = 80
        Me.BTN_Database_DefautlPath.Text = "Default"
        Me.BTN_Database_DefautlPath.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 121)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(249, 28)
        Me.Label2.TabIndex = 70
        Me.Label2.Text = "Specify the database folder"
        '
        'TB_Database
        '
        Me.TB_Database.Location = New System.Drawing.Point(20, 151)
        Me.TB_Database.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_Database.Name = "TB_Database"
        Me.TB_Database.Size = New System.Drawing.Size(500, 34)
        Me.TB_Database.TabIndex = 5
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = Global.CSI_Reporting_Application.My.Resources.Resources.flow
        Me.PictureBox5.Location = New System.Drawing.Point(-65, 200)
        Me.PictureBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(738, 221)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 9
        Me.PictureBox5.TabStop = False
        '
        'TP_Dates
        '
        Me.TP_Dates.BackColor = System.Drawing.Color.White
        Me.TP_Dates.Controls.Add(Me.DTP_firstdate)
        Me.TP_Dates.Controls.Add(Me.First_date)
        Me.TP_Dates.Controls.Add(Me.CB_BeginOfYear)
        Me.TP_Dates.Controls.Add(Me.Label5)
        Me.TP_Dates.Controls.Add(Me.CB_LastDayOfWeek)
        Me.TP_Dates.Controls.Add(Me.CB_FirstDayOfWeek)
        Me.TP_Dates.Controls.Add(Me.Label4)
        Me.TP_Dates.Controls.Add(Me.Label3)
        Me.TP_Dates.Controls.Add(Me.PictureBox6)
        Me.TP_Dates.Location = New System.Drawing.Point(4, 25)
        Me.TP_Dates.Margin = New System.Windows.Forms.Padding(4)
        Me.TP_Dates.Name = "TP_Dates"
        Me.TP_Dates.Padding = New System.Windows.Forms.Padding(4)
        Me.TP_Dates.Size = New System.Drawing.Size(842, 390)
        Me.TP_Dates.TabIndex = 4
        Me.TP_Dates.Text = "Dates"
        '
        'DTP_firstdate
        '
        Me.DTP_firstdate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DTP_firstdate.CalendarFont = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_firstdate.CustomFormat = "dd MMMM yyyy-ddd"
        Me.DTP_firstdate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_firstdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_firstdate.Location = New System.Drawing.Point(236, 133)
        Me.DTP_firstdate.Margin = New System.Windows.Forms.Padding(4)
        Me.DTP_firstdate.Name = "DTP_firstdate"
        Me.DTP_firstdate.Size = New System.Drawing.Size(212, 27)
        Me.DTP_firstdate.TabIndex = 42
        '
        'First_date
        '
        Me.First_date.AutoSize = True
        Me.First_date.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.First_date.Location = New System.Drawing.Point(232, 96)
        Me.First_date.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.First_date.Name = "First_date"
        Me.First_date.Size = New System.Drawing.Size(396, 28)
        Me.First_date.TabIndex = 41
        Me.First_date.Text = "First date to consider for the parts reporting"
        '
        'CB_BeginOfYear
        '
        Me.CB_BeginOfYear.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_BeginOfYear.FormattingEnabled = True
        Me.CB_BeginOfYear.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        Me.CB_BeginOfYear.Location = New System.Drawing.Point(13, 128)
        Me.CB_BeginOfYear.Margin = New System.Windows.Forms.Padding(4)
        Me.CB_BeginOfYear.Name = "CB_BeginOfYear"
        Me.CB_BeginOfYear.Size = New System.Drawing.Size(150, 36)
        Me.CB_BeginOfYear.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 96)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(198, 28)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "Beginning of the year"
        '
        'CB_LastDayOfWeek
        '
        Me.CB_LastDayOfWeek.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_LastDayOfWeek.FormattingEnabled = True
        Me.CB_LastDayOfWeek.Items.AddRange(New Object() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})
        Me.CB_LastDayOfWeek.Location = New System.Drawing.Point(236, 47)
        Me.CB_LastDayOfWeek.Margin = New System.Windows.Forms.Padding(4)
        Me.CB_LastDayOfWeek.Name = "CB_LastDayOfWeek"
        Me.CB_LastDayOfWeek.Size = New System.Drawing.Size(150, 36)
        Me.CB_LastDayOfWeek.TabIndex = 3
        '
        'CB_FirstDayOfWeek
        '
        Me.CB_FirstDayOfWeek.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_FirstDayOfWeek.FormattingEnabled = True
        Me.CB_FirstDayOfWeek.Items.AddRange(New Object() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})
        Me.CB_FirstDayOfWeek.Location = New System.Drawing.Point(14, 47)
        Me.CB_FirstDayOfWeek.Margin = New System.Windows.Forms.Padding(4)
        Me.CB_FirstDayOfWeek.Name = "CB_FirstDayOfWeek"
        Me.CB_FirstDayOfWeek.Size = New System.Drawing.Size(150, 36)
        Me.CB_FirstDayOfWeek.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(232, 17)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(188, 28)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Last day of the week"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 17)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(191, 28)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "First day of the week"
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = Global.CSI_Reporting_Application.My.Resources.Resources.flow
        Me.PictureBox6.Location = New System.Drawing.Point(-65, 200)
        Me.PictureBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(738, 221)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox6.TabIndex = 10
        Me.PictureBox6.TabStop = False
        '
        'TP_Sources
        '
        Me.TP_Sources.Controls.Add(Me.DGV_Source)
        Me.TP_Sources.Location = New System.Drawing.Point(4, 25)
        Me.TP_Sources.Margin = New System.Windows.Forms.Padding(4)
        Me.TP_Sources.Name = "TP_Sources"
        Me.TP_Sources.Padding = New System.Windows.Forms.Padding(4)
        Me.TP_Sources.Size = New System.Drawing.Size(842, 390)
        Me.TP_Sources.TabIndex = 8
        Me.TP_Sources.Text = "Sources"
        Me.TP_Sources.UseVisualStyleBackColor = True
        '
        'DGV_Source
        '
        Me.DGV_Source.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_Source.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DGV_Source.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_Source.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DGV_Source.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Display, Me.Machines, Me.IP, Me.Check, Me.State})
        Me.DGV_Source.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DGV_Source.Cursor = System.Windows.Forms.Cursors.Default
        Me.DGV_Source.Location = New System.Drawing.Point(8, 2)
        Me.DGV_Source.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV_Source.Name = "DGV_Source"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Source.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGV_Source.RowHeadersWidth = 24
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DGV_Source.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGV_Source.RowTemplate.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DGV_Source.Size = New System.Drawing.Size(825, 376)
        Me.DGV_Source.TabIndex = 9
        '
        'Display
        '
        Me.Display.HeaderText = "Display"
        Me.Display.Name = "Display"
        '
        'Machines
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Machines.DefaultCellStyle = DataGridViewCellStyle1
        Me.Machines.FillWeight = 93.27411!
        Me.Machines.HeaderText = "Machine name"
        Me.Machines.Name = "Machines"
        Me.Machines.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'IP
        '
        Me.IP.FillWeight = 93.27411!
        Me.IP.HeaderText = "Source"
        Me.IP.Name = "IP"
        '
        'Check
        '
        Me.Check.FillWeight = 93.27411!
        Me.Check.HeaderText = "Check"
        Me.Check.Name = "Check"
        '
        'State
        '
        Me.State.FillWeight = 93.27411!
        Me.State.HeaderText = "Connection"
        Me.State.Name = "State"
        '
        'TP_Dashboard
        '
        Me.TP_Dashboard.Controls.Add(Me.GroupBox4)
        Me.TP_Dashboard.Controls.Add(Me.GroupBox3)
        Me.TP_Dashboard.Controls.Add(Me.Label10)
        Me.TP_Dashboard.Controls.Add(Me.ComboBox4)
        Me.TP_Dashboard.Controls.Add(Me.PictureBox2)
        Me.TP_Dashboard.Controls.Add(Me.PictureBox1)
        Me.TP_Dashboard.Controls.Add(Me.PictureBox9)
        Me.TP_Dashboard.Location = New System.Drawing.Point(4, 25)
        Me.TP_Dashboard.Margin = New System.Windows.Forms.Padding(4)
        Me.TP_Dashboard.Name = "TP_Dashboard"
        Me.TP_Dashboard.Padding = New System.Windows.Forms.Padding(4)
        Me.TP_Dashboard.Size = New System.Drawing.Size(842, 390)
        Me.TP_Dashboard.TabIndex = 9
        Me.TP_Dashboard.Text = "Dashboard Servers"
        Me.TP_Dashboard.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.CHKB_UseCSIDahboard)
        Me.GroupBox4.Controls.Add(Me.CHKB_AutoScroll)
        Me.GroupBox4.Controls.Add(Me.TB_Port)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.BTN_Check)
        Me.GroupBox4.Controls.Add(Me.TB_IpAdress)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Location = New System.Drawing.Point(299, 22)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(244, 192)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "eNET http server"
        '
        'CHKB_UseCSIDahboard
        '
        Me.CHKB_UseCSIDahboard.AutoSize = True
        Me.CHKB_UseCSIDahboard.Location = New System.Drawing.Point(8, 161)
        Me.CHKB_UseCSIDahboard.Margin = New System.Windows.Forms.Padding(4)
        Me.CHKB_UseCSIDahboard.Name = "CHKB_UseCSIDahboard"
        Me.CHKB_UseCSIDahboard.Size = New System.Drawing.Size(229, 21)
        Me.CHKB_UseCSIDahboard.TabIndex = 301
        Me.CHKB_UseCSIDahboard.Text = "Use CSI Flex Server Dashboard"
        Me.CHKB_UseCSIDahboard.UseVisualStyleBackColor = True
        '
        'CHKB_AutoScroll
        '
        Me.CHKB_AutoScroll.AutoSize = True
        Me.CHKB_AutoScroll.Location = New System.Drawing.Point(8, 141)
        Me.CHKB_AutoScroll.Margin = New System.Windows.Forms.Padding(4)
        Me.CHKB_AutoScroll.Name = "CHKB_AutoScroll"
        Me.CHKB_AutoScroll.Size = New System.Drawing.Size(183, 21)
        Me.CHKB_AutoScroll.TabIndex = 19
        Me.CHKB_AutoScroll.Text = "AutoScroll in Live Status"
        Me.CHKB_AutoScroll.UseVisualStyleBackColor = True
        '
        'TB_Port
        '
        Me.TB_Port.Location = New System.Drawing.Point(50, 68)
        Me.TB_Port.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_Port.Name = "TB_Port"
        Me.TB_Port.Size = New System.Drawing.Size(178, 22)
        Me.TB_Port.TabIndex = 9
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 71)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(42, 17)
        Me.Label13.TabIndex = 100
        Me.Label13.Text = "Port :"
        '
        'BTN_Check
        '
        Me.BTN_Check.Location = New System.Drawing.Point(25, 100)
        Me.BTN_Check.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Check.Name = "BTN_Check"
        Me.BTN_Check.Size = New System.Drawing.Size(204, 29)
        Me.BTN_Check.TabIndex = 80
        Me.BTN_Check.Text = "Check"
        Me.BTN_Check.UseVisualStyleBackColor = True
        '
        'TB_IpAdress
        '
        Me.TB_IpAdress.Location = New System.Drawing.Point(25, 35)
        Me.TB_IpAdress.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_IpAdress.Name = "TB_IpAdress"
        Me.TB_IpAdress.Size = New System.Drawing.Size(203, 22)
        Me.TB_IpAdress.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(0, 39)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(28, 17)
        Me.Label12.TabIndex = 300
        Me.Label12.Text = "IP :"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TB_LocalIpAddress)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Enabled = False
        Me.GroupBox3.Location = New System.Drawing.Point(30, 22)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(261, 154)
        Me.GroupBox3.TabIndex = 18
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " Local IP"
        Me.GroupBox3.Visible = False
        '
        'TB_LocalIpAddress
        '
        Me.TB_LocalIpAddress.Location = New System.Drawing.Point(25, 35)
        Me.TB_LocalIpAddress.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_LocalIpAddress.Name = "TB_LocalIpAddress"
        Me.TB_LocalIpAddress.Size = New System.Drawing.Size(203, 22)
        Me.TB_LocalIpAddress.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(0, 39)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(28, 17)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "IP :"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(247, 216)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(328, 23)
        Me.Label10.TabIndex = 12
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComboBox4
        '
        Me.ComboBox4.BackColor = System.Drawing.SystemColors.MenuBar
        Me.ComboBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(307, 243)
        Me.ComboBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(224, 27)
        Me.ComboBox4.TabIndex = 16
        Me.ComboBox4.Visible = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.CSI_Reporting_Application.My.Resources.Resources.alert_animated
        Me.PictureBox2.Location = New System.Drawing.Point(570, 41)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(99, 154)
        Me.PictureBox2.TabIndex = 15
        Me.PictureBox2.TabStop = False
        Me.PictureBox2.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources._339927
        Me.PictureBox1.Location = New System.Drawing.Point(570, 61)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(171, 232)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'PictureBox9
        '
        Me.PictureBox9.Image = Global.CSI_Reporting_Application.My.Resources.Resources.flow
        Me.PictureBox9.Location = New System.Drawing.Point(-58, 229)
        Me.PictureBox9.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(738, 221)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox9.TabIndex = 17
        Me.PictureBox9.TabStop = False
        '
        'TP_Version
        '
        Me.TP_Version.Controls.Add(Me.Button11)
        Me.TP_Version.Controls.Add(Me.PictureBox10)
        Me.TP_Version.Controls.Add(Me.BTN_LoadLicense)
        Me.TP_Version.Controls.Add(Me.Label16)
        Me.TP_Version.Location = New System.Drawing.Point(4, 25)
        Me.TP_Version.Margin = New System.Windows.Forms.Padding(4)
        Me.TP_Version.Name = "TP_Version"
        Me.TP_Version.Padding = New System.Windows.Forms.Padding(4)
        Me.TP_Version.Size = New System.Drawing.Size(842, 390)
        Me.TP_Version.TabIndex = 11
        Me.TP_Version.Text = "CSIFlex Version"
        Me.TP_Version.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.Location = New System.Drawing.Point(14, 135)
        Me.Button11.Margin = New System.Windows.Forms.Padding(4)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(215, 51)
        Me.Button11.TabIndex = 28
        Me.Button11.Text = "Request a licence key"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'PictureBox10
        '
        Me.PictureBox10.Image = Global.CSI_Reporting_Application.My.Resources.Resources.flow
        Me.PictureBox10.Location = New System.Drawing.Point(-65, 200)
        Me.PictureBox10.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox10.Name = "PictureBox10"
        Me.PictureBox10.Size = New System.Drawing.Size(738, 221)
        Me.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox10.TabIndex = 10
        Me.PictureBox10.TabStop = False
        '
        'BTN_LoadLicense
        '
        Me.BTN_LoadLicense.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_LoadLicense.Location = New System.Drawing.Point(236, 135)
        Me.BTN_LoadLicense.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_LoadLicense.Name = "BTN_LoadLicense"
        Me.BTN_LoadLicense.Size = New System.Drawing.Size(199, 51)
        Me.BTN_LoadLicense.TabIndex = 1
        Me.BTN_LoadLicense.Text = "Change your license"
        Me.BTN_LoadLicense.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(24, 98)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(124, 17)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "You are using the "
        '
        'ReportingDataset
        '
        Me.ReportingDataset.DataSetName = "ReportingDataset"
        Me.ReportingDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Tbl_MachineNameBindingSource
        '
        Me.Tbl_MachineNameBindingSource.DataMember = "Tbl_MachineName"
        Me.Tbl_MachineNameBindingSource.DataSource = Me.ReportingDataset
        '
        'BW_import_csv
        '
        '
        'SetupForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1210, 489)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Button14)
        Me.Controls.Add(Me.Button2)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SetupForm"
        Me.Text = "Configuration"
        Me.ContextMenuADD.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TP_GeneralSetting.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        CType(Me.NUD_Refresh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.NUD_TargetLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TP_Folders.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TP_Dates.ResumeLayout(False)
        Me.TP_Dates.PerformLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TP_Sources.ResumeLayout(False)
        CType(Me.DGV_Source, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TP_Dashboard.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TP_Version.ResumeLayout(False)
        Me.TP_Version.PerformLayout()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportingDataset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tbl_MachineNameBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents ContextMenuADD As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripadd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Tbl_MachineNameBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ReportingDataset As CSI_Reporting_Application.ReportingDataset
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents BTN_Cancel As System.Windows.Forms.Button
    Friend WithEvents BTN_Ok As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TP_GeneralSetting As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents BTN_BrowseColor As System.Windows.Forms.Button
    Friend WithEvents Color_Path As System.Windows.Forms.TextBox
    Friend WithEvents BTN_StartupConfig As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents BTN_BrowseFolder As System.Windows.Forms.Button
    Friend WithEvents TB_DefaultReportFolder As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents NUD_Refresh As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents NUD_TargetLine As System.Windows.Forms.NumericUpDown
    Friend WithEvents BTN_Remove As System.Windows.Forms.Button
    Friend WithEvents BTN_Color As System.Windows.Forms.Button
    Friend WithEvents PictureBox8 As System.Windows.Forms.PictureBox
    Friend WithEvents TP_Folders As System.Windows.Forms.TabPage
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BTN_BData As System.Windows.Forms.Button
    Friend WithEvents BTN_BEnet As System.Windows.Forms.Button
    Friend WithEvents BTN_eNETDNC_DefaultPath As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TB_eNETDNC As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Database_DefautlPath As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TB_Database As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents TP_Dates As System.Windows.Forms.TabPage
    Friend WithEvents CB_BeginOfYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CB_LastDayOfWeek As System.Windows.Forms.ComboBox
    Friend WithEvents CB_FirstDayOfWeek As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents TP_Sources As System.Windows.Forms.TabPage
    Friend WithEvents DGV_Source As System.Windows.Forms.DataGridView
    Friend WithEvents Display As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Machines As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Check As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents State As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TP_Dashboard As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents CHKB_UseCSIDahboard As System.Windows.Forms.CheckBox
    Friend WithEvents CHKB_AutoScroll As System.Windows.Forms.CheckBox
    Friend WithEvents TB_Port As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents BTN_Check As System.Windows.Forms.Button
    Friend WithEvents TB_IpAdress As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TB_LocalIpAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox9 As System.Windows.Forms.PictureBox
    Friend WithEvents TP_Version As System.Windows.Forms.TabPage
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents PictureBox10 As System.Windows.Forms.PictureBox
    Friend WithEvents BTN_LoadLicense As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents BTN_Rainmeter As System.Windows.Forms.Button
    Friend WithEvents UseEnetColors_checkbox As CheckBox
    Friend WithEvents BW_import_csv As System.ComponentModel.BackgroundWorker
    Friend WithEvents Other_color As Button
    Friend WithEvents First_date As Label
    Friend WithEvents DTP_firstdate As DateTimePicker
End Class

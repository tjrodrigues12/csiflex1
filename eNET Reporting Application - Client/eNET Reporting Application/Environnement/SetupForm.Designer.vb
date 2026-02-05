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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TP_GeneralSetting = New System.Windows.Forms.TabPage()
        Me.BTN_Rainmeter = New System.Windows.Forms.Button()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.btnOthersColor = New System.Windows.Forms.Button()
        Me.cboxUseEnetColors = New System.Windows.Forms.CheckBox()
        Me.BTN_BrowseColor = New System.Windows.Forms.Button()
        Me.Color_Path = New System.Windows.Forms.TextBox()
        Me.BTN_StartupConfig = New System.Windows.Forms.Button()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.BTN_BrowseFolder = New System.Windows.Forms.Button()
        Me.txtDefaultReportFolder = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.numRefreshRate = New System.Windows.Forms.NumericUpDown()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.numTargetLine = New System.Windows.Forms.NumericUpDown()
        Me.btnRemoveTarget = New System.Windows.Forms.Button()
        Me.btnTargetColor = New System.Windows.Forms.Button()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.TP_Dates = New System.Windows.Forms.TabPage()
        Me.dtpFirstDateParts = New System.Windows.Forms.DateTimePicker()
        Me.First_date = New System.Windows.Forms.Label()
        Me.cmbBeginOfYear = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbLastDayWeek = New System.Windows.Forms.ComboBox()
        Me.cmbFirstDayWeek = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
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
        Me.txtIPDatabase = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.TP_Version = New System.Windows.Forms.TabPage()
        Me.btnLicense = New System.Windows.Forms.Button()
        Me.PictureBox10 = New System.Windows.Forms.PictureBox()
        Me.BTN_LoadLicense = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.BW_import_csv = New System.ComponentModel.BackgroundWorker()
        Me.Tbl_MachineNameBindingSource = New System.Windows.Forms.BindingSource(Me.components)
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
        CType(Me.numRefreshRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.numTargetLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Dates.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Dashboard.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Version.SuspendLayout()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ContextMenuADD.Size = New System.Drawing.Size(144, 26)
        '
        'ToolStripadd
        '
        Me.ToolStripadd.Name = "ToolStripadd"
        Me.ToolStripadd.Size = New System.Drawing.Size(143, 22)
        Me.ToolStripadd.Text = "Add a source"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(95, 26)
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(94, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(425, 356)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(134, 23)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Cancel (without saving )"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button14
        '
        Me.Button14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button14.Location = New System.Drawing.Point(259, 356)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(147, 23)
        Me.Button14.TabIndex = 9
        Me.Button14.Text = "Search MTConnect Agent"
        Me.Button14.UseVisualStyleBackColor = True
        Me.Button14.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Controls.Add(Me.btnOk)
        Me.Panel3.Location = New System.Drawing.Point(284, 351)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(680, 37)
        Me.Panel3.TabIndex = 8
        '
        'btnCancel
        '
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(604, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 37)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOk.Location = New System.Drawing.Point(536, 0)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(62, 37)
        Me.btnOk.TabIndex = 7
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureBox4)
        Me.Panel1.Controls.Add(Me.PictureBox3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(275, 342)
        Me.Panel1.TabIndex = 6
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.Repaire
        Me.PictureBox4.Location = New System.Drawing.Point(177, 247)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(98, 90)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 8
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.CSIFLEX_New_Logo_167
        Me.PictureBox3.Location = New System.Drawing.Point(-3, 36)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(275, 275)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
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
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(968, 391)
        Me.TableLayoutPanel1.TabIndex = 10
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TP_GeneralSetting)
        Me.TabControl1.Controls.Add(Me.TP_Dates)
        Me.TabControl1.Controls.Add(Me.TP_Dashboard)
        Me.TabControl1.Controls.Add(Me.TP_Version)
        Me.TabControl1.Location = New System.Drawing.Point(284, 3)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(680, 335)
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
        Me.TP_GeneralSetting.Location = New System.Drawing.Point(4, 22)
        Me.TP_GeneralSetting.Name = "TP_GeneralSetting"
        Me.TP_GeneralSetting.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_GeneralSetting.Size = New System.Drawing.Size(672, 309)
        Me.TP_GeneralSetting.TabIndex = 6
        Me.TP_GeneralSetting.Text = "General Settings"
        Me.TP_GeneralSetting.UseVisualStyleBackColor = True
        '
        'BTN_Rainmeter
        '
        Me.BTN_Rainmeter.Location = New System.Drawing.Point(505, 14)
        Me.BTN_Rainmeter.Name = "BTN_Rainmeter"
        Me.BTN_Rainmeter.Size = New System.Drawing.Size(163, 67)
        Me.BTN_Rainmeter.TabIndex = 262
        Me.BTN_Rainmeter.Text = "Rainmeter"
        Me.BTN_Rainmeter.UseVisualStyleBackColor = True
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.btnOthersColor)
        Me.GroupBox9.Controls.Add(Me.cboxUseEnetColors)
        Me.GroupBox9.Controls.Add(Me.BTN_BrowseColor)
        Me.GroupBox9.Controls.Add(Me.Color_Path)
        Me.GroupBox9.Location = New System.Drawing.Point(6, 87)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(241, 78)
        Me.GroupBox9.TabIndex = 261
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Status Colors"
        '
        'btnOthersColor
        '
        Me.btnOthersColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOthersColor.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOthersColor.Location = New System.Drawing.Point(109, 10)
        Me.btnOthersColor.Margin = New System.Windows.Forms.Padding(2)
        Me.btnOthersColor.Name = "btnOthersColor"
        Me.btnOthersColor.Size = New System.Drawing.Size(126, 38)
        Me.btnOthersColor.TabIndex = 43
        Me.btnOthersColor.Text = "Aggregate events color ('Other' status)"
        Me.btnOthersColor.UseVisualStyleBackColor = True
        '
        'cboxUseEnetColors
        '
        Me.cboxUseEnetColors.Location = New System.Drawing.Point(6, 21)
        Me.cboxUseEnetColors.Margin = New System.Windows.Forms.Padding(2)
        Me.cboxUseEnetColors.Name = "cboxUseEnetColors"
        Me.cboxUseEnetColors.Size = New System.Drawing.Size(184, 19)
        Me.cboxUseEnetColors.TabIndex = 42
        Me.cboxUseEnetColors.Text = "Use Enet Colors"
        Me.cboxUseEnetColors.UseVisualStyleBackColor = True
        '
        'BTN_BrowseColor
        '
        Me.BTN_BrowseColor.Location = New System.Drawing.Point(196, 51)
        Me.BTN_BrowseColor.Name = "BTN_BrowseColor"
        Me.BTN_BrowseColor.Size = New System.Drawing.Size(38, 23)
        Me.BTN_BrowseColor.TabIndex = 41
        Me.BTN_BrowseColor.Text = "..."
        Me.BTN_BrowseColor.UseVisualStyleBackColor = True
        '
        'Color_Path
        '
        Me.Color_Path.Location = New System.Drawing.Point(6, 54)
        Me.Color_Path.Name = "Color_Path"
        Me.Color_Path.Size = New System.Drawing.Size(184, 20)
        Me.Color_Path.TabIndex = 0
        '
        'BTN_StartupConfig
        '
        Me.BTN_StartupConfig.Location = New System.Drawing.Point(505, 94)
        Me.BTN_StartupConfig.Name = "BTN_StartupConfig"
        Me.BTN_StartupConfig.Size = New System.Drawing.Size(163, 67)
        Me.BTN_StartupConfig.TabIndex = 257
        Me.BTN_StartupConfig.Text = "Startup configuration"
        Me.BTN_StartupConfig.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.BTN_BrowseFolder)
        Me.GroupBox8.Controls.Add(Me.txtDefaultReportFolder)
        Me.GroupBox8.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(241, 75)
        Me.GroupBox8.TabIndex = 260
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Default reporting folder"
        '
        'BTN_BrowseFolder
        '
        Me.BTN_BrowseFolder.Location = New System.Drawing.Point(197, 30)
        Me.BTN_BrowseFolder.Name = "BTN_BrowseFolder"
        Me.BTN_BrowseFolder.Size = New System.Drawing.Size(38, 23)
        Me.BTN_BrowseFolder.TabIndex = 41
        Me.BTN_BrowseFolder.Text = "..."
        Me.BTN_BrowseFolder.UseVisualStyleBackColor = True
        '
        'txtDefaultReportFolder
        '
        Me.txtDefaultReportFolder.Location = New System.Drawing.Point(7, 33)
        Me.txtDefaultReportFolder.Name = "txtDefaultReportFolder"
        Me.txtDefaultReportFolder.Size = New System.Drawing.Size(184, 20)
        Me.txtDefaultReportFolder.TabIndex = 0
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.numRefreshRate)
        Me.GroupBox7.Location = New System.Drawing.Point(253, 6)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(248, 75)
        Me.GroupBox7.TabIndex = 260
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Tree view refresh rate (sec)"
        '
        'numRefreshRate
        '
        Me.numRefreshRate.Location = New System.Drawing.Point(58, 34)
        Me.numRefreshRate.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.numRefreshRate.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numRefreshRate.Name = "numRefreshRate"
        Me.numRefreshRate.Size = New System.Drawing.Size(120, 20)
        Me.numRefreshRate.TabIndex = 1
        Me.numRefreshRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numRefreshRate.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.numTargetLine)
        Me.GroupBox5.Controls.Add(Me.btnRemoveTarget)
        Me.GroupBox5.Controls.Add(Me.btnTargetColor)
        Me.GroupBox5.Location = New System.Drawing.Point(253, 87)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(248, 78)
        Me.GroupBox5.TabIndex = 258
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Target line"
        '
        'numTargetLine
        '
        Me.numTargetLine.Location = New System.Drawing.Point(6, 30)
        Me.numTargetLine.Name = "numTargetLine"
        Me.numTargetLine.Size = New System.Drawing.Size(50, 20)
        Me.numTargetLine.TabIndex = 253
        Me.numTargetLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnRemoveTarget
        '
        Me.btnRemoveTarget.Location = New System.Drawing.Point(143, 27)
        Me.btnRemoveTarget.Name = "btnRemoveTarget"
        Me.btnRemoveTarget.Size = New System.Drawing.Size(84, 23)
        Me.btnRemoveTarget.TabIndex = 255
        Me.btnRemoveTarget.Text = "Remove"
        Me.btnRemoveTarget.UseVisualStyleBackColor = True
        '
        'btnTargetColor
        '
        Me.btnTargetColor.Location = New System.Drawing.Point(85, 27)
        Me.btnTargetColor.Name = "btnTargetColor"
        Me.btnTargetColor.Size = New System.Drawing.Size(37, 23)
        Me.btnTargetColor.TabIndex = 256
        Me.btnTargetColor.UseVisualStyleBackColor = True
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.flow
        Me.PictureBox8.Location = New System.Drawing.Point(-42, 171)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(580, 166)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox8.TabIndex = 257
        Me.PictureBox8.TabStop = False
        '
        'TP_Dates
        '
        Me.TP_Dates.BackColor = System.Drawing.Color.White
        Me.TP_Dates.Controls.Add(Me.dtpFirstDateParts)
        Me.TP_Dates.Controls.Add(Me.First_date)
        Me.TP_Dates.Controls.Add(Me.cmbBeginOfYear)
        Me.TP_Dates.Controls.Add(Me.Label5)
        Me.TP_Dates.Controls.Add(Me.cmbLastDayWeek)
        Me.TP_Dates.Controls.Add(Me.cmbFirstDayWeek)
        Me.TP_Dates.Controls.Add(Me.Label4)
        Me.TP_Dates.Controls.Add(Me.Label3)
        Me.TP_Dates.Controls.Add(Me.PictureBox6)
        Me.TP_Dates.Location = New System.Drawing.Point(4, 22)
        Me.TP_Dates.Name = "TP_Dates"
        Me.TP_Dates.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_Dates.Size = New System.Drawing.Size(672, 309)
        Me.TP_Dates.TabIndex = 4
        Me.TP_Dates.Text = "Dates"
        '
        'dtpFirstDateParts
        '
        Me.dtpFirstDateParts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFirstDateParts.CalendarFont = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFirstDateParts.CustomFormat = "dd MMMM yyyy-ddd"
        Me.dtpFirstDateParts.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFirstDateParts.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFirstDateParts.Location = New System.Drawing.Point(189, 106)
        Me.dtpFirstDateParts.Name = "dtpFirstDateParts"
        Me.dtpFirstDateParts.Size = New System.Drawing.Size(170, 23)
        Me.dtpFirstDateParts.TabIndex = 42
        '
        'First_date
        '
        Me.First_date.AutoSize = True
        Me.First_date.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.First_date.Location = New System.Drawing.Point(186, 77)
        Me.First_date.Name = "First_date"
        Me.First_date.Size = New System.Drawing.Size(313, 21)
        Me.First_date.TabIndex = 41
        Me.First_date.Text = "First date to consider for the parts reporting"
        '
        'cmbBeginOfYear
        '
        Me.cmbBeginOfYear.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBeginOfYear.FormattingEnabled = True
        Me.cmbBeginOfYear.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        Me.cmbBeginOfYear.Location = New System.Drawing.Point(10, 102)
        Me.cmbBeginOfYear.Name = "cmbBeginOfYear"
        Me.cmbBeginOfYear.Size = New System.Drawing.Size(121, 29)
        Me.cmbBeginOfYear.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(158, 21)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "Beginning of the year"
        '
        'cmbLastDayWeek
        '
        Me.cmbLastDayWeek.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLastDayWeek.FormattingEnabled = True
        Me.cmbLastDayWeek.Items.AddRange(New Object() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})
        Me.cmbLastDayWeek.Location = New System.Drawing.Point(189, 38)
        Me.cmbLastDayWeek.Name = "cmbLastDayWeek"
        Me.cmbLastDayWeek.Size = New System.Drawing.Size(121, 29)
        Me.cmbLastDayWeek.TabIndex = 3
        '
        'cmbFirstDayWeek
        '
        Me.cmbFirstDayWeek.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFirstDayWeek.FormattingEnabled = True
        Me.cmbFirstDayWeek.Items.AddRange(New Object() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})
        Me.cmbFirstDayWeek.Location = New System.Drawing.Point(11, 38)
        Me.cmbFirstDayWeek.Name = "cmbFirstDayWeek"
        Me.cmbFirstDayWeek.Size = New System.Drawing.Size(121, 29)
        Me.cmbFirstDayWeek.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(186, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(151, 21)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Last day of the week"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(153, 21)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "First day of the week"
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.flow
        Me.PictureBox6.Location = New System.Drawing.Point(-52, 160)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(590, 177)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox6.TabIndex = 10
        Me.PictureBox6.TabStop = False
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
        Me.TP_Dashboard.Location = New System.Drawing.Point(4, 22)
        Me.TP_Dashboard.Name = "TP_Dashboard"
        Me.TP_Dashboard.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_Dashboard.Size = New System.Drawing.Size(672, 309)
        Me.TP_Dashboard.TabIndex = 9
        Me.TP_Dashboard.Text = "Server"
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
        Me.GroupBox4.Location = New System.Drawing.Point(239, 18)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(238, 154)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "eNET http server"
        Me.GroupBox4.Visible = False
        '
        'CHKB_UseCSIDahboard
        '
        Me.CHKB_UseCSIDahboard.AutoSize = True
        Me.CHKB_UseCSIDahboard.Location = New System.Drawing.Point(6, 129)
        Me.CHKB_UseCSIDahboard.Name = "CHKB_UseCSIDahboard"
        Me.CHKB_UseCSIDahboard.Size = New System.Drawing.Size(176, 17)
        Me.CHKB_UseCSIDahboard.TabIndex = 301
        Me.CHKB_UseCSIDahboard.Text = "Use CSI Flex Server Dashboard"
        Me.CHKB_UseCSIDahboard.UseVisualStyleBackColor = True
        Me.CHKB_UseCSIDahboard.Visible = False
        '
        'CHKB_AutoScroll
        '
        Me.CHKB_AutoScroll.AutoSize = True
        Me.CHKB_AutoScroll.Location = New System.Drawing.Point(56, 113)
        Me.CHKB_AutoScroll.Name = "CHKB_AutoScroll"
        Me.CHKB_AutoScroll.Size = New System.Drawing.Size(141, 17)
        Me.CHKB_AutoScroll.TabIndex = 19
        Me.CHKB_AutoScroll.Text = "AutoScroll in Live Status"
        Me.CHKB_AutoScroll.UseVisualStyleBackColor = True
        '
        'TB_Port
        '
        Me.TB_Port.Location = New System.Drawing.Point(40, 54)
        Me.TB_Port.Name = "TB_Port"
        Me.TB_Port.Size = New System.Drawing.Size(143, 20)
        Me.TB_Port.TabIndex = 9
        Me.TB_Port.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 57)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(32, 13)
        Me.Label13.TabIndex = 100
        Me.Label13.Text = "Port :"
        Me.Label13.Visible = False
        '
        'BTN_Check
        '
        Me.BTN_Check.Location = New System.Drawing.Point(56, 80)
        Me.BTN_Check.Name = "BTN_Check"
        Me.BTN_Check.Size = New System.Drawing.Size(176, 23)
        Me.BTN_Check.TabIndex = 80
        Me.BTN_Check.Text = "Check"
        Me.BTN_Check.UseVisualStyleBackColor = True
        '
        'TB_IpAdress
        '
        Me.TB_IpAdress.Location = New System.Drawing.Point(56, 28)
        Me.TB_IpAdress.Name = "TB_IpAdress"
        Me.TB_IpAdress.Size = New System.Drawing.Size(176, 20)
        Me.TB_IpAdress.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(0, 31)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 13)
        Me.Label12.TabIndex = 300
        Me.Label12.Text = "IP / Port:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtIPDatabase)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Location = New System.Drawing.Point(24, 18)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(209, 123)
        Me.GroupBox3.TabIndex = 18
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " CSIFLEX Database "
        '
        'txtIPDatabase
        '
        Me.txtIPDatabase.Location = New System.Drawing.Point(38, 54)
        Me.txtIPDatabase.Name = "txtIPDatabase"
        Me.txtIPDatabase.Size = New System.Drawing.Size(150, 20)
        Me.txtIPDatabase.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 57)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(23, 13)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "IP :"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(198, 173)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(262, 18)
        Me.Label10.TabIndex = 12
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComboBox4
        '
        Me.ComboBox4.BackColor = System.Drawing.SystemColors.MenuBar
        Me.ComboBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(246, 194)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(180, 21)
        Me.ComboBox4.TabIndex = 16
        Me.ComboBox4.Visible = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.alert_animated
        Me.PictureBox2.Location = New System.Drawing.Point(587, 33)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(79, 123)
        Me.PictureBox2.TabIndex = 15
        Me.PictureBox2.TabStop = False
        Me.PictureBox2.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources._339927
        Me.PictureBox1.Location = New System.Drawing.Point(529, 49)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(137, 186)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'PictureBox9
        '
        Me.PictureBox9.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.flow
        Me.PictureBox9.Location = New System.Drawing.Point(-46, 183)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(590, 177)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox9.TabIndex = 17
        Me.PictureBox9.TabStop = False
        '
        'TP_Version
        '
        Me.TP_Version.Controls.Add(Me.btnLicense)
        Me.TP_Version.Controls.Add(Me.PictureBox10)
        Me.TP_Version.Controls.Add(Me.BTN_LoadLicense)
        Me.TP_Version.Controls.Add(Me.Label16)
        Me.TP_Version.Location = New System.Drawing.Point(4, 22)
        Me.TP_Version.Name = "TP_Version"
        Me.TP_Version.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_Version.Size = New System.Drawing.Size(672, 309)
        Me.TP_Version.TabIndex = 11
        Me.TP_Version.Text = "License"
        Me.TP_Version.UseVisualStyleBackColor = True
        '
        'btnLicense
        '
        Me.btnLicense.Location = New System.Drawing.Point(11, 108)
        Me.btnLicense.Name = "btnLicense"
        Me.btnLicense.Size = New System.Drawing.Size(172, 41)
        Me.btnLicense.TabIndex = 28
        Me.btnLicense.Text = "License Management"
        Me.btnLicense.UseVisualStyleBackColor = True
        '
        'PictureBox10
        '
        Me.PictureBox10.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.flow
        Me.PictureBox10.Location = New System.Drawing.Point(-52, 160)
        Me.PictureBox10.Name = "PictureBox10"
        Me.PictureBox10.Size = New System.Drawing.Size(590, 177)
        Me.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox10.TabIndex = 10
        Me.PictureBox10.TabStop = False
        '
        'BTN_LoadLicense
        '
        Me.BTN_LoadLicense.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_LoadLicense.Location = New System.Drawing.Point(189, 108)
        Me.BTN_LoadLicense.Name = "BTN_LoadLicense"
        Me.BTN_LoadLicense.Size = New System.Drawing.Size(159, 41)
        Me.BTN_LoadLicense.TabIndex = 1
        Me.BTN_LoadLicense.Text = "Change your license"
        Me.BTN_LoadLicense.UseVisualStyleBackColor = True
        Me.BTN_LoadLicense.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(19, 78)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(93, 13)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "You are using the "
        '
        'BW_import_csv
        '
        '
        'SetupForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(968, 391)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Button14)
        Me.Controls.Add(Me.Button2)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
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
        CType(Me.numRefreshRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.numTargetLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TP_Dates.ResumeLayout(False)
        Me.TP_Dates.PerformLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
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
    Friend WithEvents txtDefaultReportFolder As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents numRefreshRate As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents numTargetLine As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnRemoveTarget As System.Windows.Forms.Button
    Friend WithEvents btnTargetColor As System.Windows.Forms.Button
    Friend WithEvents PictureBox8 As System.Windows.Forms.PictureBox
    Friend WithEvents TP_Dates As System.Windows.Forms.TabPage
    Friend WithEvents cmbBeginOfYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbLastDayWeek As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFirstDayWeek As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
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
    Friend WithEvents txtIPDatabase As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox9 As System.Windows.Forms.PictureBox
    Friend WithEvents TP_Version As System.Windows.Forms.TabPage
    Friend WithEvents btnLicense As System.Windows.Forms.Button
    Friend WithEvents PictureBox10 As System.Windows.Forms.PictureBox
    Friend WithEvents BTN_LoadLicense As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents BTN_Rainmeter As System.Windows.Forms.Button
    Friend WithEvents cboxUseEnetColors As CheckBox
    Friend WithEvents BW_import_csv As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnOthersColor As Button
    Friend WithEvents First_date As Label
    Friend WithEvents dtpFirstDateParts As DateTimePicker
End Class

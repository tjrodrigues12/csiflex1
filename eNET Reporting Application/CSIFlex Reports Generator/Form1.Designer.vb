<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CSIFlex_Reporting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CSIFlex_Reporting))
        Me.Btn_gen = New System.Windows.Forms.Button()
        Me.DGV_tasks = New System.Windows.Forms.DataGridView()
        Me.Task_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Day = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Report_Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Output_folder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MachineName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MailTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgv_mail = New System.Windows.Forms.DataGridView()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BTN_Add = New System.Windows.Forms.Button()
        Me.GrBx_Parameter = New System.Windows.Forms.GroupBox()
        Me.PB_Unistall = New System.Windows.Forms.PictureBox()
        Me.PB_Status = New System.Windows.Forms.PictureBox()
        Me.CB_endtime = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.RB_Shift3 = New System.Windows.Forms.RadioButton()
        Me.RB_Shift2 = New System.Windows.Forms.RadioButton()
        Me.RB_Shift1 = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BTN_CustomEmail = New System.Windows.Forms.Button()
        Me.cb_t = New System.Windows.Forms.DateTimePicker()
        Me.cb_d = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CB_WKND = New System.Windows.Forms.CheckBox()
        Me.CB_DOW = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BTN_Output = New System.Windows.Forms.Button()
        Me.TB_OutPutFolder = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CBX_ReportType = New System.Windows.Forms.ComboBox()
        Me.DTP_Time = New System.Windows.Forms.DateTimePicker()
        Me.TB_TaskName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BTN_Remove = New System.Windows.Forms.Button()
        Me.BTN_Modify = New System.Windows.Forms.Button()
        Me.BTN_Cancel = New System.Windows.Forms.Button()
        Me.DGV_MachineList = New System.Windows.Forms.DataGridView()
        Me.ReportOrNot = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.MachineName2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.BG_generate = New System.ComponentModel.BackgroundWorker()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.DGV_tasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.dgv_mail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GrBx_Parameter.SuspendLayout()
        CType(Me.PB_Unistall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_Status, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_MachineList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Btn_gen
        '
        Me.Btn_gen.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Btn_gen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_gen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Btn_gen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Btn_gen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_gen.Location = New System.Drawing.Point(312, 30)
        Me.Btn_gen.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_gen.Name = "Btn_gen"
        Me.Btn_gen.Size = New System.Drawing.Size(112, 28)
        Me.Btn_gen.TabIndex = 9
        Me.Btn_gen.Text = "Generate Now"
        Me.Btn_gen.UseVisualStyleBackColor = False
        '
        'DGV_tasks
        '
        Me.DGV_tasks.AllowUserToAddRows = False
        Me.DGV_tasks.AllowUserToResizeRows = False
        Me.DGV_tasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_tasks.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DGV_tasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_tasks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Task_name, Me.Day, Me.Time, Me.Report_Type, Me.Output_folder, Me.MachineName, Me.MailTo})
        Me.DGV_tasks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_tasks.Location = New System.Drawing.Point(4, 424)
        Me.DGV_tasks.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV_tasks.Name = "DGV_tasks"
        Me.DGV_tasks.RowHeadersVisible = False
        Me.DGV_tasks.Size = New System.Drawing.Size(850, 211)
        Me.DGV_tasks.TabIndex = 10
        '
        'Task_name
        '
        Me.Task_name.DataPropertyName = "Task_name"
        Me.Task_name.HeaderText = "Task Name"
        Me.Task_name.Name = "Task_name"
        Me.Task_name.ReadOnly = True
        Me.Task_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
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
        Me.Time.HeaderText = "Time"
        Me.Time.Name = "Time"
        Me.Time.ReadOnly = True
        Me.Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Report_Type
        '
        Me.Report_Type.DataPropertyName = "Report_Type"
        Me.Report_Type.HeaderText = "Report Type"
        Me.Report_Type.Name = "Report_Type"
        Me.Report_Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Output_folder
        '
        Me.Output_folder.DataPropertyName = "Output_Folder"
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
        Me.MailTo.HeaderText = "mailTo"
        Me.MailTo.Name = "MailTo"
        Me.MailTo.ReadOnly = True
        Me.MailTo.Visible = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.DGV_tasks, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 2)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.84606!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.15394!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(858, 639)
        Me.TableLayoutPanel2.TabIndex = 10
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.dgv_mail, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 2)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(852, 416)
        Me.TableLayoutPanel3.TabIndex = 11
        '
        'dgv_mail
        '
        Me.dgv_mail.AllowUserToAddRows = False
        Me.dgv_mail.AllowUserToResizeRows = False
        Me.dgv_mail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv_mail.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.dgv_mail.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.dgv_mail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumn1, Me.DataGridViewTextBoxColumn1})
        Me.dgv_mail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_mail.Location = New System.Drawing.Point(600, 4)
        Me.dgv_mail.Margin = New System.Windows.Forms.Padding(4)
        Me.dgv_mail.Name = "dgv_mail"
        Me.dgv_mail.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgv_mail.RowHeadersVisible = False
        Me.dgv_mail.Size = New System.Drawing.Size(248, 408)
        Me.dgv_mail.TabIndex = 8
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
        Me.Panel1.Controls.Add(Me.Btn_gen)
        Me.Panel1.Controls.Add(Me.BTN_Add)
        Me.Panel1.Controls.Add(Me.GrBx_Parameter)
        Me.Panel1.Controls.Add(Me.BTN_Remove)
        Me.Panel1.Controls.Add(Me.BTN_Modify)
        Me.Panel1.Controls.Add(Me.BTN_Cancel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(590, 412)
        Me.Panel1.TabIndex = 9
        '
        'BTN_Add
        '
        Me.BTN_Add.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Add.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Add.Location = New System.Drawing.Point(9, 30)
        Me.BTN_Add.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Add.Name = "BTN_Add"
        Me.BTN_Add.Size = New System.Drawing.Size(93, 28)
        Me.BTN_Add.TabIndex = 1
        Me.BTN_Add.Text = "Add New"
        Me.BTN_Add.UseVisualStyleBackColor = False
        '
        'GrBx_Parameter
        '
        Me.GrBx_Parameter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrBx_Parameter.Controls.Add(Me.PB_Unistall)
        Me.GrBx_Parameter.Controls.Add(Me.PB_Status)
        Me.GrBx_Parameter.Controls.Add(Me.CB_endtime)
        Me.GrBx_Parameter.Controls.Add(Me.Label9)
        Me.GrBx_Parameter.Controls.Add(Me.RB_Shift3)
        Me.GrBx_Parameter.Controls.Add(Me.RB_Shift2)
        Me.GrBx_Parameter.Controls.Add(Me.RB_Shift1)
        Me.GrBx_Parameter.Controls.Add(Me.Label8)
        Me.GrBx_Parameter.Controls.Add(Me.BTN_CustomEmail)
        Me.GrBx_Parameter.Controls.Add(Me.cb_t)
        Me.GrBx_Parameter.Controls.Add(Me.cb_d)
        Me.GrBx_Parameter.Controls.Add(Me.Label7)
        Me.GrBx_Parameter.Controls.Add(Me.Label6)
        Me.GrBx_Parameter.Controls.Add(Me.CB_WKND)
        Me.GrBx_Parameter.Controls.Add(Me.CB_DOW)
        Me.GrBx_Parameter.Controls.Add(Me.Label2)
        Me.GrBx_Parameter.Controls.Add(Me.BTN_Output)
        Me.GrBx_Parameter.Controls.Add(Me.TB_OutPutFolder)
        Me.GrBx_Parameter.Controls.Add(Me.Label5)
        Me.GrBx_Parameter.Controls.Add(Me.CBX_ReportType)
        Me.GrBx_Parameter.Controls.Add(Me.DTP_Time)
        Me.GrBx_Parameter.Controls.Add(Me.TB_TaskName)
        Me.GrBx_Parameter.Controls.Add(Me.Label4)
        Me.GrBx_Parameter.Controls.Add(Me.Label1)
        Me.GrBx_Parameter.Controls.Add(Me.Label3)
        Me.GrBx_Parameter.Location = New System.Drawing.Point(9, 66)
        Me.GrBx_Parameter.Margin = New System.Windows.Forms.Padding(4)
        Me.GrBx_Parameter.Name = "GrBx_Parameter"
        Me.GrBx_Parameter.Padding = New System.Windows.Forms.Padding(4)
        Me.GrBx_Parameter.Size = New System.Drawing.Size(574, 346)
        Me.GrBx_Parameter.TabIndex = 8
        Me.GrBx_Parameter.TabStop = False
        Me.GrBx_Parameter.Text = "Parameters"
        '
        'PB_Unistall
        '
        Me.PB_Unistall.Image = CType(resources.GetObject("PB_Unistall.Image"), System.Drawing.Image)
        Me.PB_Unistall.Location = New System.Drawing.Point(487, 78)
        Me.PB_Unistall.Name = "PB_Unistall"
        Me.PB_Unistall.Size = New System.Drawing.Size(80, 65)
        Me.PB_Unistall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_Unistall.TabIndex = 25
        Me.PB_Unistall.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PB_Unistall, "Press to Uninstall Service ")
        '
        'PB_Status
        '
        Me.PB_Status.Image = CType(resources.GetObject("PB_Status.Image"), System.Drawing.Image)
        Me.PB_Status.Location = New System.Drawing.Point(349, 78)
        Me.PB_Status.Name = "PB_Status"
        Me.PB_Status.Size = New System.Drawing.Size(80, 65)
        Me.PB_Status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_Status.TabIndex = 10
        Me.PB_Status.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PB_Status, "Service Status  : Running")
        Me.PB_Status.Visible = False
        '
        'CB_endtime
        '
        Me.CB_endtime.CustomFormat = "HH:mm"
        Me.CB_endtime.Enabled = False
        Me.CB_endtime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.CB_endtime.Location = New System.Drawing.Point(376, 221)
        Me.CB_endtime.Margin = New System.Windows.Forms.Padding(4)
        Me.CB_endtime.Name = "CB_endtime"
        Me.CB_endtime.ShowUpDown = True
        Me.CB_endtime.Size = New System.Drawing.Size(162, 22)
        Me.CB_endtime.TabIndex = 24
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(373, 200)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 17)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "End time:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'RB_Shift3
        '
        Me.RB_Shift3.AutoSize = True
        Me.RB_Shift3.Location = New System.Drawing.Point(282, 126)
        Me.RB_Shift3.Name = "RB_Shift3"
        Me.RB_Shift3.Size = New System.Drawing.Size(37, 21)
        Me.RB_Shift3.TabIndex = 22
        Me.RB_Shift3.TabStop = True
        Me.RB_Shift3.Text = "3"
        Me.RB_Shift3.UseVisualStyleBackColor = True
        '
        'RB_Shift2
        '
        Me.RB_Shift2.AutoSize = True
        Me.RB_Shift2.Location = New System.Drawing.Point(213, 126)
        Me.RB_Shift2.Name = "RB_Shift2"
        Me.RB_Shift2.Size = New System.Drawing.Size(37, 21)
        Me.RB_Shift2.TabIndex = 21
        Me.RB_Shift2.TabStop = True
        Me.RB_Shift2.Text = "2"
        Me.RB_Shift2.UseVisualStyleBackColor = True
        '
        'RB_Shift1
        '
        Me.RB_Shift1.AutoSize = True
        Me.RB_Shift1.Location = New System.Drawing.Point(139, 126)
        Me.RB_Shift1.Name = "RB_Shift1"
        Me.RB_Shift1.Size = New System.Drawing.Size(37, 21)
        Me.RB_Shift1.TabIndex = 20
        Me.RB_Shift1.TabStop = True
        Me.RB_Shift1.Text = "1"
        Me.RB_Shift1.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(87, 128)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 17)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Shift :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BTN_CustomEmail
        '
        Me.BTN_CustomEmail.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_CustomEmail.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_CustomEmail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_CustomEmail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_CustomEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_CustomEmail.Location = New System.Drawing.Point(349, 35)
        Me.BTN_CustomEmail.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_CustomEmail.Name = "BTN_CustomEmail"
        Me.BTN_CustomEmail.Size = New System.Drawing.Size(217, 26)
        Me.BTN_CustomEmail.TabIndex = 15
        Me.BTN_CustomEmail.Text = "Custom email message"
        Me.BTN_CustomEmail.UseVisualStyleBackColor = False
        '
        'cb_t
        '
        Me.cb_t.CustomFormat = "HH:mm"
        Me.cb_t.Enabled = False
        Me.cb_t.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cb_t.Location = New System.Drawing.Point(376, 171)
        Me.cb_t.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_t.Name = "cb_t"
        Me.cb_t.ShowUpDown = True
        Me.cb_t.Size = New System.Drawing.Size(161, 22)
        Me.cb_t.TabIndex = 14
        '
        'cb_d
        '
        Me.cb_d.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_d.Enabled = False
        Me.cb_d.FormattingEnabled = True
        Me.cb_d.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7"})
        Me.cb_d.Location = New System.Drawing.Point(139, 169)
        Me.cb_d.Margin = New System.Windows.Forms.Padding(4)
        Me.cb_d.Name = "cb_d"
        Me.cb_d.Size = New System.Drawing.Size(111, 24)
        Me.cb_d.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(373, 146)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 17)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Start time:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(87, 177)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Days:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'CB_WKND
        '
        Me.CB_WKND.AutoSize = True
        Me.CB_WKND.Checked = True
        Me.CB_WKND.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CB_WKND.Enabled = False
        Me.CB_WKND.Location = New System.Drawing.Point(376, 252)
        Me.CB_WKND.Margin = New System.Windows.Forms.Padding(4)
        Me.CB_WKND.Name = "CB_WKND"
        Me.CB_WKND.Size = New System.Drawing.Size(90, 21)
        Me.CB_WKND.TabIndex = 3
        Me.CB_WKND.Text = "Weekend"
        Me.CB_WKND.UseVisualStyleBackColor = True
        '
        'CB_DOW
        '
        Me.CB_DOW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_DOW.DropDownWidth = 180
        Me.CB_DOW.FormattingEnabled = True
        Me.CB_DOW.IntegralHeight = False
        Me.CB_DOW.Items.AddRange(New Object() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})
        Me.CB_DOW.Location = New System.Drawing.Point(139, 210)
        Me.CB_DOW.Margin = New System.Windows.Forms.Padding(4)
        Me.CB_DOW.Name = "CB_DOW"
        Me.CB_DOW.Size = New System.Drawing.Size(180, 24)
        Me.CB_DOW.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(38, 217)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 17)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Day of Week:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BTN_Output
        '
        Me.BTN_Output.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Output.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Output.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Output.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Output.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Output.Location = New System.Drawing.Point(444, 285)
        Me.BTN_Output.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Output.Name = "BTN_Output"
        Me.BTN_Output.Size = New System.Drawing.Size(93, 28)
        Me.BTN_Output.TabIndex = 7
        Me.BTN_Output.Text = "Browse"
        Me.BTN_Output.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BTN_Output.UseVisualStyleBackColor = False
        '
        'TB_OutPutFolder
        '
        Me.TB_OutPutFolder.Location = New System.Drawing.Point(139, 295)
        Me.TB_OutPutFolder.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_OutPutFolder.Name = "TB_OutPutFolder"
        Me.TB_OutPutFolder.Size = New System.Drawing.Size(264, 22)
        Me.TB_OutPutFolder.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(36, 295)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 17)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Output folder:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'CBX_ReportType
        '
        Me.CBX_ReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBX_ReportType.DropDownWidth = 180
        Me.CBX_ReportType.FormattingEnabled = True
        Me.CBX_ReportType.IntegralHeight = False
        Me.CBX_ReportType.Items.AddRange(New Object() {"Daily ( Today ) Availability - PDF", "Daily ( Yesterday ) Availability - PDF", "Weekly Availability - PDF", "Monthly Availability - PDF"})
        Me.CBX_ReportType.Location = New System.Drawing.Point(139, 78)
        Me.CBX_ReportType.Margin = New System.Windows.Forms.Padding(4)
        Me.CBX_ReportType.Name = "CBX_ReportType"
        Me.CBX_ReportType.Size = New System.Drawing.Size(180, 24)
        Me.CBX_ReportType.TabIndex = 2
        '
        'DTP_Time
        '
        Me.DTP_Time.CustomFormat = "HH:mm"
        Me.DTP_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_Time.Location = New System.Drawing.Point(139, 252)
        Me.DTP_Time.Margin = New System.Windows.Forms.Padding(4)
        Me.DTP_Time.Name = "DTP_Time"
        Me.DTP_Time.ShowUpDown = True
        Me.DTP_Time.Size = New System.Drawing.Size(71, 22)
        Me.DTP_Time.TabIndex = 5
        '
        'TB_TaskName
        '
        Me.TB_TaskName.Location = New System.Drawing.Point(139, 37)
        Me.TB_TaskName.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_TaskName.Name = "TB_TaskName"
        Me.TB_TaskName.Size = New System.Drawing.Size(180, 22)
        Me.TB_TaskName.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(40, 85)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 17)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Report Type:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(47, 37)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Task Name:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 256)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Generation Time:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BTN_Remove
        '
        Me.BTN_Remove.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Remove.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Remove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Remove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Remove.Location = New System.Drawing.Point(109, 30)
        Me.BTN_Remove.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Remove.Name = "BTN_Remove"
        Me.BTN_Remove.Size = New System.Drawing.Size(93, 28)
        Me.BTN_Remove.TabIndex = 2
        Me.BTN_Remove.Text = "Remove"
        Me.BTN_Remove.UseVisualStyleBackColor = False
        '
        'BTN_Modify
        '
        Me.BTN_Modify.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Modify.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Modify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Modify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Modify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Modify.Location = New System.Drawing.Point(211, 30)
        Me.BTN_Modify.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Modify.Name = "BTN_Modify"
        Me.BTN_Modify.Size = New System.Drawing.Size(93, 28)
        Me.BTN_Modify.TabIndex = 3
        Me.BTN_Modify.Text = "Modify"
        Me.BTN_Modify.UseVisualStyleBackColor = False
        '
        'BTN_Cancel
        '
        Me.BTN_Cancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Cancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Cancel.Location = New System.Drawing.Point(432, 30)
        Me.BTN_Cancel.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Cancel.Name = "BTN_Cancel"
        Me.BTN_Cancel.Size = New System.Drawing.Size(93, 28)
        Me.BTN_Cancel.TabIndex = 4
        Me.BTN_Cancel.Text = "Cancel"
        Me.BTN_Cancel.UseVisualStyleBackColor = False
        '
        'DGV_MachineList
        '
        Me.DGV_MachineList.AllowUserToAddRows = False
        Me.DGV_MachineList.AllowUserToResizeRows = False
        Me.DGV_MachineList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_MachineList.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DGV_MachineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_MachineList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ReportOrNot, Me.MachineName2})
        Me.DGV_MachineList.Location = New System.Drawing.Point(868, 4)
        Me.DGV_MachineList.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV_MachineList.Name = "DGV_MachineList"
        Me.DGV_MachineList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.DGV_MachineList.RowHeadersVisible = False
        Me.DGV_MachineList.Size = New System.Drawing.Size(280, 635)
        Me.DGV_MachineList.TabIndex = 9
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
        'MachineName2
        '
        Me.MachineName2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.MachineName2.FillWeight = 80.0!
        Me.MachineName2.HeaderText = "Machine"
        Me.MachineName2.Name = "MachineName2"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DGV_MachineList, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1152, 643)
        Me.TableLayoutPanel1.TabIndex = 12
        '
        'BG_generate
        '
        Me.BG_generate.WorkerReportsProgress = True
        '
        'CSIFlex_Reporting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(1152, 643)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "CSIFlex_Reporting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CSIFlex Report Generator"
        CType(Me.DGV_tasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.dgv_mail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GrBx_Parameter.ResumeLayout(False)
        Me.GrBx_Parameter.PerformLayout()
        CType(Me.PB_Unistall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_Status, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_MachineList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Friend WithEvents Btn_gen As Button
    Friend WithEvents DGV_tasks As DataGridView
    Friend WithEvents Task_name As DataGridViewTextBoxColumn
    Friend WithEvents Day As DataGridViewTextBoxColumn
    Friend WithEvents Time As DataGridViewTextBoxColumn
    Friend WithEvents Report_Type As DataGridViewTextBoxColumn
    Friend WithEvents Output_folder As DataGridViewTextBoxColumn
    Friend WithEvents MachineName As DataGridViewTextBoxColumn
    Friend WithEvents MailTo As DataGridViewTextBoxColumn
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents dgv_mail As DataGridView
    Friend WithEvents DataGridViewCheckBoxColumn1 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As Panel
    Friend WithEvents BTN_Add As Button
    Friend WithEvents GrBx_Parameter As GroupBox
    Friend WithEvents BTN_CustomEmail As Button
    Friend WithEvents cb_t As DateTimePicker
    Friend WithEvents cb_d As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents CB_WKND As CheckBox
    Friend WithEvents CB_DOW As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents BTN_Output As Button
    Friend WithEvents TB_OutPutFolder As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents CBX_ReportType As ComboBox
    Friend WithEvents DTP_Time As DateTimePicker
    Friend WithEvents TB_TaskName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents BTN_Remove As Button
    Friend WithEvents BTN_Modify As Button
    Friend WithEvents BTN_Cancel As Button
    Friend WithEvents DGV_MachineList As DataGridView
    Friend WithEvents ReportOrNot As DataGridViewCheckBoxColumn
    Friend WithEvents MachineName2 As DataGridViewTextBoxColumn
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents BG_generate As System.ComponentModel.BackgroundWorker

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Friend WithEvents Label8 As Label
    Friend WithEvents RB_Shift3 As RadioButton
    Friend WithEvents RB_Shift2 As RadioButton
    Friend WithEvents RB_Shift1 As RadioButton
    Friend WithEvents CB_endtime As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents PB_Status As PictureBox
    Friend WithEvents PB_Unistall As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
End Class

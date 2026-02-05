<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Config_report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Config_report))
        Me.ContextMenuADD = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BTN_Dashboard = New System.Windows.Forms.Button()
        Me.BTN_Create = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BTN_Previous_date = New System.Windows.Forms.Button()
        Me.BTN_next_date = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BTN_Yearly = New System.Windows.Forms.Button()
        Me.BTN_Monthly = New System.Windows.Forms.Button()
        Me.BTN_Weekly = New System.Windows.Forms.Button()
        Me.BTN_Dayly = New System.Windows.Forms.Button()
        Me.DTP_StartDate = New System.Windows.Forms.DateTimePicker()
        Me.DTP_EndDate = New System.Windows.Forms.DateTimePicker()
        Me.BTN_PartNb = New System.Windows.Forms.Button()
        Me.TreeView_machine = New System.Windows.Forms.TreeView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.BTN_ALL_partno = New System.Windows.Forms.Button()
        Me.DGV_PartsNumber = New System.Windows.Forms.DataGridView()
        Me.CMS_partsdetails = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.partdetail = New System.Windows.Forms.ToolStripMenuItem()
        Me.TB_PartsNumber = New System.Windows.Forms.TextBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TimerDrawTree = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuADD.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DGV_PartsNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CMS_partsdetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuADD
        '
        Me.ContextMenuADD.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem
        Me.ContextMenuADD.BackgroundImage = Global.CSI_Reporting_Application.My.Resources.Resources.set_of_light_blue_background
        Me.ContextMenuADD.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuADD.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem3, Me.ToolStripMenuItem2})
        Me.ContextMenuADD.Name = "ContextMenuStrip1"
        Me.ContextMenuADD.Size = New System.Drawing.Size(225, 82)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripMenuItem1.Enabled = False
        Me.ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(224, 26)
        Me.ToolStripMenuItem1.Text = "Add a groupe"
        Me.ToolStripMenuItem1.Visible = False
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(224, 26)
        Me.ToolStripMenuItem3.Text = "Rename"
        Me.ToolStripMenuItem3.Visible = False
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(224, 26)
        Me.ToolStripMenuItem2.Text = "Delete"
        Me.ToolStripMenuItem2.Visible = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(67, 4)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Cursor = System.Windows.Forms.Cursors.SizeNS
        Me.SplitContainer1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.SplitContainer1.ForeColor = System.Drawing.Color.Silver
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.SplitContainer1.Panel1.ForeColor = System.Drawing.Color.SlateGray
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Default
        Me.SplitContainer1.Size = New System.Drawing.Size(346, 925)
        Me.SplitContainer1.SplitterDistance = 603
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 31
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.BTN_Dashboard)
        Me.Panel2.Controls.Add(Me.BTN_Create)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.BTN_PartNb)
        Me.Panel2.Controls.Add(Me.TreeView_machine)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(346, 603)
        Me.Panel2.TabIndex = 27
        '
        'BTN_Dashboard
        '
        Me.BTN_Dashboard.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTN_Dashboard.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Dashboard.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Dashboard.FlatAppearance.BorderSize = 0
        Me.BTN_Dashboard.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_Dashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_Dashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Dashboard.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Dashboard.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Dashboard.Location = New System.Drawing.Point(9, 321)
        Me.BTN_Dashboard.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Dashboard.Name = "BTN_Dashboard"
        Me.BTN_Dashboard.Size = New System.Drawing.Size(326, 49)
        Me.BTN_Dashboard.TabIndex = 36
        Me.BTN_Dashboard.Text = "Show Live Status"
        Me.BTN_Dashboard.UseVisualStyleBackColor = True
        '
        'BTN_Create
        '
        Me.BTN_Create.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_Create.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Create.Enabled = False
        Me.BTN_Create.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Create.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_Create.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_Create.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Create.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Create.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Create.Location = New System.Drawing.Point(179, 541)
        Me.BTN_Create.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Create.Name = "BTN_Create"
        Me.BTN_Create.Size = New System.Drawing.Size(156, 49)
        Me.BTN_Create.TabIndex = 34
        Me.BTN_Create.Text = "Create"
        Me.BTN_Create.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.BTN_Previous_date)
        Me.GroupBox1.Controls.Add(Me.BTN_next_date)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.BTN_Yearly)
        Me.GroupBox1.Controls.Add(Me.BTN_Monthly)
        Me.GroupBox1.Controls.Add(Me.BTN_Weekly)
        Me.GroupBox1.Controls.Add(Me.BTN_Dayly)
        Me.GroupBox1.Controls.Add(Me.DTP_StartDate)
        Me.GroupBox1.Controls.Add(Me.DTP_EndDate)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.GroupBox1.Location = New System.Drawing.Point(9, 369)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(326, 158)
        Me.GroupBox1.TabIndex = 32
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Report dates"
        '
        'BTN_Previous_date
        '
        Me.BTN_Previous_date.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_Previous_date.BackColor = System.Drawing.Color.Transparent
        Me.BTN_Previous_date.BackgroundImage = Global.CSI_Reporting_Application.My.Resources.Resources.left_arrow
        Me.BTN_Previous_date.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BTN_Previous_date.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Previous_date.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Previous_date.FlatAppearance.BorderSize = 0
        Me.BTN_Previous_date.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_Previous_date.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Previous_date.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_Previous_date.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Previous_date.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Previous_date.ForeColor = System.Drawing.Color.Transparent
        Me.BTN_Previous_date.Location = New System.Drawing.Point(74, 73)
        Me.BTN_Previous_date.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Previous_date.Name = "BTN_Previous_date"
        Me.BTN_Previous_date.Size = New System.Drawing.Size(21, 29)
        Me.BTN_Previous_date.TabIndex = 11
        Me.BTN_Previous_date.UseVisualStyleBackColor = False
        '
        'BTN_next_date
        '
        Me.BTN_next_date.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_next_date.BackgroundImage = Global.CSI_Reporting_Application.My.Resources.Resources.right_arrow
        Me.BTN_next_date.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BTN_next_date.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_next_date.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_next_date.FlatAppearance.BorderSize = 0
        Me.BTN_next_date.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_next_date.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_next_date.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_next_date.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_next_date.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_next_date.ForeColor = System.Drawing.Color.Transparent
        Me.BTN_next_date.Location = New System.Drawing.Point(95, 73)
        Me.BTN_next_date.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_next_date.Name = "BTN_next_date"
        Me.BTN_next_date.Size = New System.Drawing.Size(21, 29)
        Me.BTN_next_date.TabIndex = 10
        Me.BTN_next_date.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Light", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.Label3.Location = New System.Drawing.Point(5, 73)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 25)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Period : "
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Light", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.Label2.Location = New System.Drawing.Point(5, 111)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 25)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "End : "
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Light", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.Label1.Location = New System.Drawing.Point(5, 38)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 25)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Start : "
        '
        'BTN_Yearly
        '
        Me.BTN_Yearly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_Yearly.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Yearly.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Yearly.FlatAppearance.BorderSize = 0
        Me.BTN_Yearly.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_Yearly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Yearly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_Yearly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Yearly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Yearly.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Yearly.Location = New System.Drawing.Point(264, 74)
        Me.BTN_Yearly.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Yearly.Name = "BTN_Yearly"
        Me.BTN_Yearly.Size = New System.Drawing.Size(39, 29)
        Me.BTN_Yearly.TabIndex = 8
        Me.BTN_Yearly.Text = "Y"
        Me.BTN_Yearly.UseVisualStyleBackColor = True
        '
        'BTN_Monthly
        '
        Me.BTN_Monthly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_Monthly.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Monthly.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Monthly.FlatAppearance.BorderSize = 0
        Me.BTN_Monthly.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_Monthly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Monthly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_Monthly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Monthly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Monthly.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Monthly.Location = New System.Drawing.Point(218, 74)
        Me.BTN_Monthly.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Monthly.Name = "BTN_Monthly"
        Me.BTN_Monthly.Size = New System.Drawing.Size(39, 29)
        Me.BTN_Monthly.TabIndex = 7
        Me.BTN_Monthly.Text = "M"
        Me.BTN_Monthly.UseVisualStyleBackColor = True
        '
        'BTN_Weekly
        '
        Me.BTN_Weekly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_Weekly.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Weekly.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Weekly.FlatAppearance.BorderSize = 0
        Me.BTN_Weekly.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_Weekly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Weekly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_Weekly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Weekly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Weekly.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Weekly.Location = New System.Drawing.Point(171, 73)
        Me.BTN_Weekly.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Weekly.Name = "BTN_Weekly"
        Me.BTN_Weekly.Size = New System.Drawing.Size(39, 29)
        Me.BTN_Weekly.TabIndex = 6
        Me.BTN_Weekly.Text = "W"
        Me.BTN_Weekly.UseVisualStyleBackColor = True
        '
        'BTN_Dayly
        '
        Me.BTN_Dayly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_Dayly.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_Dayly.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Dayly.FlatAppearance.BorderSize = 0
        Me.BTN_Dayly.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_Dayly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Dayly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_Dayly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Dayly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Dayly.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_Dayly.Location = New System.Drawing.Point(124, 73)
        Me.BTN_Dayly.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Dayly.Name = "BTN_Dayly"
        Me.BTN_Dayly.Size = New System.Drawing.Size(39, 29)
        Me.BTN_Dayly.TabIndex = 5
        Me.BTN_Dayly.Text = "D"
        Me.BTN_Dayly.UseVisualStyleBackColor = True
        '
        'DTP_StartDate
        '
        Me.DTP_StartDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DTP_StartDate.CalendarFont = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_StartDate.CustomFormat = "dd MMMM yyyy-ddd"
        Me.DTP_StartDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_StartDate.Location = New System.Drawing.Point(85, 38)
        Me.DTP_StartDate.Margin = New System.Windows.Forms.Padding(4)
        Me.DTP_StartDate.Name = "DTP_StartDate"
        Me.DTP_StartDate.Size = New System.Drawing.Size(231, 27)
        Me.DTP_StartDate.TabIndex = 0
        '
        'DTP_EndDate
        '
        Me.DTP_EndDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DTP_EndDate.CalendarFont = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_EndDate.CustomFormat = "dd MMMM yyyy-ddd"
        Me.DTP_EndDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_EndDate.Location = New System.Drawing.Point(85, 111)
        Me.DTP_EndDate.Margin = New System.Windows.Forms.Padding(4)
        Me.DTP_EndDate.Name = "DTP_EndDate"
        Me.DTP_EndDate.Size = New System.Drawing.Size(231, 27)
        Me.DTP_EndDate.TabIndex = 1
        '
        'BTN_PartNb
        '
        Me.BTN_PartNb.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_PartNb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_PartNb.Enabled = False
        Me.BTN_PartNb.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_PartNb.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_PartNb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_PartNb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_PartNb.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_PartNb.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_PartNb.Location = New System.Drawing.Point(9, 541)
        Me.BTN_PartNb.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_PartNb.Name = "BTN_PartNb"
        Me.BTN_PartNb.Size = New System.Drawing.Size(158, 49)
        Me.BTN_PartNb.TabIndex = 35
        Me.BTN_PartNb.Text = "View Part Numbers"
        Me.BTN_PartNb.UseVisualStyleBackColor = True
        '
        'TreeView_machine
        '
        Me.TreeView_machine.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView_machine.BackColor = System.Drawing.Color.Gainsboro
        Me.TreeView_machine.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView_machine.CheckBoxes = True
        Me.TreeView_machine.ContextMenuStrip = Me.ContextMenuADD
        Me.TreeView_machine.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView_machine.HotTracking = True
        Me.TreeView_machine.LabelEdit = True
        Me.TreeView_machine.LineColor = System.Drawing.Color.Blue
        Me.TreeView_machine.Location = New System.Drawing.Point(9, 14)
        Me.TreeView_machine.Margin = New System.Windows.Forms.Padding(4)
        Me.TreeView_machine.MinimumSize = New System.Drawing.Size(0, 15)
        Me.TreeView_machine.Name = "TreeView_machine"
        Me.TreeView_machine.SelectedImageIndex = Me.TreeView_machine.ImageIndex
        Me.TreeView_machine.ShowLines = False
        Me.TreeView_machine.Size = New System.Drawing.Size(326, 300)
        Me.TreeView_machine.TabIndex = 33
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(346, 317)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.BTN_ALL_partno)
        Me.GroupBox3.Controls.Add(Me.DGV_PartsNumber)
        Me.GroupBox3.Controls.Add(Me.TB_PartsNumber)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.GroupBox3.Location = New System.Drawing.Point(9, 3)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(326, 301)
        Me.GroupBox3.TabIndex = 30
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Part Numbers Filter"
        '
        'BTN_ALL_partno
        '
        Me.BTN_ALL_partno.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_ALL_partno.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_ALL_partno.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_ALL_partno.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_ALL_partno.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_ALL_partno.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.BTN_ALL_partno.Location = New System.Drawing.Point(10, 29)
        Me.BTN_ALL_partno.Name = "BTN_ALL_partno"
        Me.BTN_ALL_partno.Size = New System.Drawing.Size(62, 34)
        Me.BTN_ALL_partno.TabIndex = 32
        Me.BTN_ALL_partno.Text = "All"
        Me.BTN_ALL_partno.UseVisualStyleBackColor = True
        '
        'DGV_PartsNumber
        '
        Me.DGV_PartsNumber.AllowUserToAddRows = False
        Me.DGV_PartsNumber.AllowUserToDeleteRows = False
        Me.DGV_PartsNumber.AllowUserToOrderColumns = True
        Me.DGV_PartsNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DGV_PartsNumber.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_PartsNumber.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.DGV_PartsNumber.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_PartsNumber.ColumnHeadersVisible = False
        Me.DGV_PartsNumber.ContextMenuStrip = Me.CMS_partsdetails
        Me.DGV_PartsNumber.GridColor = System.Drawing.SystemColors.Highlight
        Me.DGV_PartsNumber.Location = New System.Drawing.Point(10, 68)
        Me.DGV_PartsNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV_PartsNumber.Name = "DGV_PartsNumber"
        Me.DGV_PartsNumber.ReadOnly = True
        Me.DGV_PartsNumber.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGV_PartsNumber.RowHeadersVisible = False
        Me.DGV_PartsNumber.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.DGV_PartsNumber.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DGV_PartsNumber.Size = New System.Drawing.Size(303, 225)
        Me.DGV_PartsNumber.StandardTab = True
        Me.DGV_PartsNumber.TabIndex = 31
        '
        'CMS_partsdetails
        '
        Me.CMS_partsdetails.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.CMS_partsdetails.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.partdetail})
        Me.CMS_partsdetails.Name = "CMS_partsdetails"
        Me.CMS_partsdetails.Size = New System.Drawing.Size(158, 30)
        '
        'partdetail
        '
        Me.partdetail.Name = "partdetail"
        Me.partdetail.Size = New System.Drawing.Size(157, 26)
        Me.partdetail.Text = "Part details"
        '
        'TB_PartsNumber
        '
        Me.TB_PartsNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TB_PartsNumber.Location = New System.Drawing.Point(79, 29)
        Me.TB_PartsNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_PartsNumber.Name = "TB_PartsNumber"
        Me.TB_PartsNumber.Size = New System.Drawing.Size(234, 34)
        Me.TB_PartsNumber.TabIndex = 30
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(67, 4)
        '
        'TimerDrawTree
        '
        Me.TimerDrawTree.Enabled = True
        Me.TimerDrawTree.Interval = 10000
        '
        'Config_report
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.DimGray
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(344, 925)
        Me.Controls.Add(Me.SplitContainer1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Config_report"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TransparencyKey = System.Drawing.Color.Transparent
        Me.ContextMenuADD.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DGV_PartsNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CMS_partsdetails.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ContextMenuADD As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerDrawTree As System.Windows.Forms.Timer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TB_PartsNumber As TextBox
    Friend WithEvents DGV_PartsNumber As DataGridView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents BTN_ALL_partno As Button
    Friend WithEvents CMS_partsdetails As ContextMenuStrip
    Friend WithEvents partdetail As ToolStripMenuItem
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TreeView_machine As TreeView
    Friend WithEvents BTN_PartNb As Button
    Friend WithEvents DTP_EndDate As DateTimePicker
    Friend WithEvents DTP_StartDate As DateTimePicker
    Friend WithEvents BTN_Dayly As Button
    Friend WithEvents BTN_Weekly As Button
    Friend WithEvents BTN_Monthly As Button
    Friend WithEvents BTN_Yearly As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents BTN_next_date As Button
    Friend WithEvents BTN_Previous_date As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BTN_Create As Button
    Friend WithEvents BTN_Dashboard As Button
End Class

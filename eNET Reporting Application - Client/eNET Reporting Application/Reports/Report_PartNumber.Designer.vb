<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Report_PartNumber
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Report_PartNumber))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim DataPoint1 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 0R)
        Dim DataPoint2 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 0R)
        Dim DataPoint3 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 0R)
        Dim DataPoint4 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 0R)
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TB_PartsNumber = New System.Windows.Forms.TextBox()
        Me.CB_partNumber = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BTN_Prev = New System.Windows.Forms.Button()
        Me.BTN_Next = New System.Windows.Forms.Button()
        Me.CB_Report = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PrintForm1 = New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(Me.components)
        Me.LBL_MachineName = New System.Windows.Forms.Label()
        Me.DGV_partnumber = New System.Windows.Forms.DataGridView()
        Me.DGV_percentage = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.DGV_top = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.BTN_Return = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_partnumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_percentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_top, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.TB_PartsNumber)
        Me.GroupBox1.Controls.Add(Me.CB_partNumber)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.BTN_Prev)
        Me.GroupBox1.Controls.Add(Me.BTN_Next)
        Me.GroupBox1.Controls.Add(Me.CB_Report)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(416, 25)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(755, 159)
        Me.GroupBox1.TabIndex = 151
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Part Number :"
        '
        'TB_PartsNumber
        '
        Me.TB_PartsNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_PartsNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TB_PartsNumber.Enabled = False
        Me.TB_PartsNumber.Location = New System.Drawing.Point(198, 115)
        Me.TB_PartsNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_PartsNumber.Name = "TB_PartsNumber"
        Me.TB_PartsNumber.Size = New System.Drawing.Size(424, 34)
        Me.TB_PartsNumber.TabIndex = 127
        '
        'CB_partNumber
        '
        Me.CB_partNumber.BackColor = System.Drawing.Color.White
        Me.CB_partNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_partNumber.Enabled = False
        Me.CB_partNumber.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_partNumber.FormattingEnabled = True
        Me.CB_partNumber.IntegralHeight = False
        Me.CB_partNumber.Location = New System.Drawing.Point(198, 88)
        Me.CB_partNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.CB_partNumber.Name = "CB_partNumber"
        Me.CB_partNumber.Size = New System.Drawing.Size(424, 27)
        Me.CB_partNumber.TabIndex = 126
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Enabled = False
        Me.Label2.Location = New System.Drawing.Point(8, 88)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(180, 28)
        Me.Label2.TabIndex = 125
        Me.Label2.Text = "Part Numbers Filter"
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Button2.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Button2.Location = New System.Drawing.Point(630, 61)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(109, 29)
        Me.Button2.TabIndex = 124
        Me.Button2.Text = "by part"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Button1.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Button1.Location = New System.Drawing.Point(630, 98)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(109, 29)
        Me.Button1.TabIndex = 122
        Me.Button1.Text = "Return"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BTN_Prev
        '
        Me.BTN_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Prev.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Prev.ForeColor = System.Drawing.Color.Black
        Me.BTN_Prev.Location = New System.Drawing.Point(630, 25)
        Me.BTN_Prev.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Prev.Name = "BTN_Prev"
        Me.BTN_Prev.Size = New System.Drawing.Size(55, 29)
        Me.BTN_Prev.TabIndex = 121
        Me.BTN_Prev.Text = "Prev"
        Me.BTN_Prev.UseVisualStyleBackColor = True
        '
        'BTN_Next
        '
        Me.BTN_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Next.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Next.ForeColor = System.Drawing.Color.Black
        Me.BTN_Next.Location = New System.Drawing.Point(684, 25)
        Me.BTN_Next.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Next.Name = "BTN_Next"
        Me.BTN_Next.Size = New System.Drawing.Size(55, 29)
        Me.BTN_Next.TabIndex = 120
        Me.BTN_Next.Text = "Next"
        Me.BTN_Next.UseVisualStyleBackColor = True
        '
        'CB_Report
        '
        Me.CB_Report.BackColor = System.Drawing.Color.White
        Me.CB_Report.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_Report.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_Report.FormattingEnabled = True
        Me.CB_Report.Location = New System.Drawing.Point(8, 44)
        Me.CB_Report.Margin = New System.Windows.Forms.Padding(4)
        Me.CB_Report.Name = "CB_Report"
        Me.CB_Report.Size = New System.Drawing.Size(614, 27)
        Me.CB_Report.TabIndex = 118
        '
        'PrintDocument1
        '
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.print_icon
        Me.PictureBox4.Location = New System.Drawing.Point(1118, 5)
        Me.PictureBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(38, 31)
        Me.PictureBox4.TabIndex = 252
        Me.PictureBox4.TabStop = False
        '
        'PrintForm1
        '
        Me.PrintForm1.DocumentName = "document"
        Me.PrintForm1.Form = Me
        Me.PrintForm1.PrintAction = System.Drawing.Printing.PrintAction.PrintToPreview
        Me.PrintForm1.PrinterSettings = CType(resources.GetObject("PrintForm1.PrinterSettings"), System.Drawing.Printing.PrinterSettings)
        Me.PrintForm1.PrintFileName = Nothing
        '
        'LBL_MachineName
        '
        Me.LBL_MachineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 50.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_MachineName.Location = New System.Drawing.Point(15, 16)
        Me.LBL_MachineName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBL_MachineName.Name = "LBL_MachineName"
        Me.LBL_MachineName.Size = New System.Drawing.Size(394, 109)
        Me.LBL_MachineName.TabIndex = 253
        Me.LBL_MachineName.Text = "Mach1"
        Me.LBL_MachineName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DGV_partnumber
        '
        Me.DGV_partnumber.AllowUserToAddRows = False
        Me.DGV_partnumber.AllowUserToDeleteRows = False
        Me.DGV_partnumber.AllowUserToOrderColumns = True
        Me.DGV_partnumber.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.DGV_partnumber.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_partnumber.Location = New System.Drawing.Point(61, 504)
        Me.DGV_partnumber.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV_partnumber.Name = "DGV_partnumber"
        Me.DGV_partnumber.ReadOnly = True
        Me.DGV_partnumber.Size = New System.Drawing.Size(1110, 349)
        Me.DGV_partnumber.TabIndex = 254
        '
        'DGV_percentage
        '
        Me.DGV_percentage.AllowUserToAddRows = False
        Me.DGV_percentage.AllowUserToDeleteRows = False
        Me.DGV_percentage.AllowUserToOrderColumns = True
        Me.DGV_percentage.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.DGV_percentage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_percentage.Location = New System.Drawing.Point(851, 191)
        Me.DGV_percentage.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV_percentage.Name = "DGV_percentage"
        Me.DGV_percentage.ReadOnly = True
        Me.DGV_percentage.Size = New System.Drawing.Size(320, 258)
        Me.DGV_percentage.TabIndex = 255
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(55, 464)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(260, 36)
        Me.Label1.TabIndex = 256
        Me.Label1.Text = "Part Numbers List:"
        '
        'Chart1
        '
        Me.Chart1.BackColor = System.Drawing.Color.Transparent
        Me.Chart1.BorderlineColor = System.Drawing.Color.Transparent
        Me.Chart1.BorderSkin.BackColor = System.Drawing.Color.DimGray
        Me.Chart1.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft
        ChartArea1.BackColor = System.Drawing.Color.Transparent
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Me.Chart1.Cursor = System.Windows.Forms.Cursors.Arrow
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(61, 222)
        Me.Chart1.Margin = New System.Windows.Forms.Padding(4)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None
        Me.Chart1.PaletteCustomColors = New System.Drawing.Color() {System.Drawing.Color.Green, System.Drawing.Color.Red, System.Drawing.Color.Blue, System.Drawing.Color.Yellow}
        Series1.BackImageTransparentColor = System.Drawing.Color.Transparent
        Series1.BorderColor = System.Drawing.Color.Transparent
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
        Series1.CustomProperties = "PieLineColor=Black, PieDrawingStyle=Concave"
        Series1.IsVisibleInLegend = False
        Series1.IsXValueIndexed = True
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        DataPoint1.IsVisibleInLegend = True
        DataPoint1.Label = "CYCLE ON"
        DataPoint2.LegendText = "CYCLE OFF"
        DataPoint3.LegendText = "SETUP"
        DataPoint4.LegendText = "OTHER"
        Series1.Points.Add(DataPoint1)
        Series1.Points.Add(DataPoint2)
        Series1.Points.Add(DataPoint3)
        Series1.Points.Add(DataPoint4)
        Series1.ShadowOffset = 8
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(262, 226)
        Me.Chart1.TabIndex = 258
        Me.Chart1.Text = "Chart1"
        '
        'DGV_top
        '
        Me.DGV_top.AllowUserToAddRows = False
        Me.DGV_top.AllowUserToDeleteRows = False
        Me.DGV_top.BackgroundColor = System.Drawing.Color.White
        Me.DGV_top.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DGV_top.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Symbol", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlDark
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_top.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGV_top.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DGV_top.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9})
        Me.DGV_top.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.DGV_top.Location = New System.Drawing.Point(331, 191)
        Me.DGV_top.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV_top.MultiSelect = False
        Me.DGV_top.Name = "DGV_top"
        Me.DGV_top.ReadOnly = True
        Me.DGV_top.RowHeadersVisible = False
        Me.DGV_top.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV_top.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DGV_top.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_top.ShowCellErrors = False
        Me.DGV_top.ShowCellToolTips = False
        Me.DGV_top.ShowEditingIcon = False
        Me.DGV_top.ShowRowErrors = False
        Me.DGV_top.Size = New System.Drawing.Size(512, 258)
        Me.DGV_top.TabIndex = 259
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlDark
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn7.HeaderText = "Part Numbers"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn7.Width = 250
        '
        'DataGridViewTextBoxColumn8
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlDark
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn8.HeaderText = "Occurrence"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn8.Width = 95
        '
        'DataGridViewTextBoxColumn9
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlDark
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn9.HeaderText = "%"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 65
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(61, 190)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(262, 28)
        Me.Label19.TabIndex = 260
        Me.Label19.Text = "Top Part Numbers"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'BTN_Return
        '
        Me.BTN_Return.BackgroundImage = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.home_web_icon_300x300
        Me.BTN_Return.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BTN_Return.Location = New System.Drawing.Point(6, 7)
        Me.BTN_Return.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Return.Name = "BTN_Return"
        Me.BTN_Return.Size = New System.Drawing.Size(37, 37)
        Me.BTN_Return.TabIndex = 261
        Me.BTN_Return.UseVisualStyleBackColor = True
        '
        'Report_PartNumber
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1176, 881)
        Me.Controls.Add(Me.BTN_Return)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.DGV_top)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DGV_percentage)
        Me.Controls.Add(Me.DGV_partnumber)
        Me.Controls.Add(Me.LBL_MachineName)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximumSize = New System.Drawing.Size(1240, 924)
        Me.Name = "Report_PartNumber"
        Me.Opacity = 0.3R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Weekly Report"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_partnumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_percentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_top, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BTN_Prev As System.Windows.Forms.Button
    Friend WithEvents BTN_Next As System.Windows.Forms.Button
    Friend WithEvents CB_Report As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PrintForm1 As Microsoft.VisualBasic.PowerPacks.Printing.PrintForm
    Friend WithEvents LBL_MachineName As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DGV_partnumber As System.Windows.Forms.DataGridView
    Friend WithEvents DGV_percentage As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents DGV_top As System.Windows.Forms.DataGridView
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CB_partNumber As System.Windows.Forms.ComboBox
    Friend WithEvents TB_PartsNumber As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Return As Button
End Class

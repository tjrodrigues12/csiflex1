<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_Shift
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Shift))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.chartPartNumbers = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.chartTimeline = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.chkSpindle = New System.Windows.Forms.CheckBox()
        Me.chkRapid = New System.Windows.Forms.CheckBox()
        Me.chkFeedrate = New System.Windows.Forms.CheckBox()
        Me.chartOverride = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnExpande = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.chartHistory = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BTN_RAW_VALUES = New System.Windows.Forms.Button()
        Me.Disp_labels = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.chartPartNumbers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.chartTimeline, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartOverride, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.SplitContainer1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1069, 750)
        Me.TableLayoutPanel1.TabIndex = 254
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(4, 72)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chartHistory)
        Me.SplitContainer1.Size = New System.Drawing.Size(1061, 674)
        Me.SplitContainer1.SplitterDistance = 270
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(6, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chartPartNumbers)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnExpande)
        Me.SplitContainer2.Size = New System.Drawing.Size(1045, 260)
        Me.SplitContainer2.SplitterDistance = 45
        Me.SplitContainer2.SplitterWidth = 2
        Me.SplitContainer2.TabIndex = 257
        '
        'chartPartNumbers
        '
        Me.chartPartNumbers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chartPartNumbers.BackColor = System.Drawing.Color.Transparent
        Me.chartPartNumbers.BorderlineColor = System.Drawing.Color.Transparent
        ChartArea1.Area3DStyle.Inclination = 0
        ChartArea1.Area3DStyle.IsRightAngleAxes = False
        ChartArea1.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic
        ChartArea1.Area3DStyle.Rotation = 50
        ChartArea1.Area3DStyle.WallWidth = 0
        ChartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
        ChartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds
        ChartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds
        ChartArea1.AxisX.LabelStyle.Enabled = False
        ChartArea1.AxisX.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ChartArea1.AxisX.LabelStyle.Format = "hh:mm:ss"
        ChartArea1.AxisX.MajorGrid.Enabled = False
        ChartArea1.AxisX.ScaleBreakStyle.Enabled = True
        ChartArea1.AxisX.ScaleView.MinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds
        ChartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea1.AxisY.LabelStyle.Enabled = False
        ChartArea1.AxisY.LineColor = System.Drawing.Color.Transparent
        ChartArea1.AxisY.MajorGrid.Enabled = False
        ChartArea1.AxisY.MajorTickMark.Enabled = False
        ChartArea1.AxisY.Maximum = 100.0R
        ChartArea1.BackColor = System.Drawing.Color.Transparent
        ChartArea1.BorderColor = System.Drawing.Color.Transparent
        ChartArea1.CursorX.IntervalOffset = 1.0R
        ChartArea1.CursorX.IsUserEnabled = True
        ChartArea1.CursorX.IsUserSelectionEnabled = True
        ChartArea1.InnerPlotPosition.Auto = False
        ChartArea1.InnerPlotPosition.Height = 98.01672!
        ChartArea1.InnerPlotPosition.Width = 95.92!
        ChartArea1.InnerPlotPosition.X = 0.08000183!
        ChartArea1.Name = "ChartArea1"
        Me.chartPartNumbers.ChartAreas.Add(ChartArea1)
        Me.chartPartNumbers.Cursor = System.Windows.Forms.Cursors.Default
        Me.chartPartNumbers.Location = New System.Drawing.Point(4, 0)
        Me.chartPartNumbers.Margin = New System.Windows.Forms.Padding(4)
        Me.chartPartNumbers.Name = "chartPartNumbers"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area
        Series1.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Series1.Name = "Cycle"
        Series1.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes
        Series1.SmartLabelStyle.CalloutBackColor = System.Drawing.Color.White
        Series1.SmartLabelStyle.MovingDirection = System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Center
        Series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
        Me.chartPartNumbers.Series.Add(Series1)
        Me.chartPartNumbers.Size = New System.Drawing.Size(1008, 43)
        Me.chartPartNumbers.TabIndex = 256
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
        Me.SplitContainer3.Panel1.Controls.Add(Me.chartTimeline)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.chkSpindle)
        Me.SplitContainer3.Panel2.Controls.Add(Me.chkRapid)
        Me.SplitContainer3.Panel2.Controls.Add(Me.chkFeedrate)
        Me.SplitContainer3.Panel2.Controls.Add(Me.chartOverride)
        Me.SplitContainer3.Size = New System.Drawing.Size(1013, 207)
        Me.SplitContainer3.SplitterDistance = 59
        Me.SplitContainer3.TabIndex = 261
        '
        'chartTimeline
        '
        Me.chartTimeline.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chartTimeline.BackColor = System.Drawing.Color.Transparent
        Me.chartTimeline.BorderlineColor = System.Drawing.Color.Transparent
        ChartArea2.Area3DStyle.Inclination = 0
        ChartArea2.Area3DStyle.IsRightAngleAxes = False
        ChartArea2.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic
        ChartArea2.Area3DStyle.Rotation = 50
        ChartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
        ChartArea2.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds
        ChartArea2.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds
        ChartArea2.AxisX.LabelStyle.Enabled = False
        ChartArea2.AxisX.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ChartArea2.AxisX.LabelStyle.Format = "hh:mm:ss"
        ChartArea2.AxisX.MajorGrid.Enabled = False
        ChartArea2.AxisX.ScaleBreakStyle.Enabled = True
        ChartArea2.AxisX.ScaleView.MinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds
        ChartArea2.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea2.AxisY.LabelStyle.Enabled = False
        ChartArea2.AxisY.LineColor = System.Drawing.Color.Transparent
        ChartArea2.AxisY.MajorGrid.Enabled = False
        ChartArea2.AxisY.MajorTickMark.Enabled = False
        ChartArea2.AxisY.Maximum = 100.0R
        ChartArea2.BackColor = System.Drawing.Color.Transparent
        ChartArea2.BorderColor = System.Drawing.Color.Transparent
        ChartArea2.CursorX.IntervalOffset = 1.0R
        ChartArea2.CursorX.IsUserEnabled = True
        ChartArea2.CursorX.IsUserSelectionEnabled = True
        ChartArea2.InnerPlotPosition.Auto = False
        ChartArea2.InnerPlotPosition.Height = 98.01672!
        ChartArea2.InnerPlotPosition.Width = 95.92!
        ChartArea2.InnerPlotPosition.X = 0.08000183!
        ChartArea2.Name = "ChartArea1"
        Me.chartTimeline.ChartAreas.Add(ChartArea2)
        Me.chartTimeline.Cursor = System.Windows.Forms.Cursors.Default
        Me.chartTimeline.Location = New System.Drawing.Point(1, 4)
        Me.chartTimeline.Margin = New System.Windows.Forms.Padding(4)
        Me.chartTimeline.Name = "chartTimeline"
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area
        Series2.IsVisibleInLegend = False
        Series2.Name = "Cycle"
        Series2.SmartLabelStyle.CalloutStyle = System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.Box
        Series2.SmartLabelStyle.MovingDirection = CType((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Right Or System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomRight), System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles)
        Series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime
        Me.chartTimeline.Series.Add(Series2)
        Me.chartTimeline.Size = New System.Drawing.Size(1008, 51)
        Me.chartTimeline.TabIndex = 255
        '
        'chkSpindle
        '
        Me.chkSpindle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSpindle.AutoSize = True
        Me.chkSpindle.Enabled = False
        Me.chkSpindle.Location = New System.Drawing.Point(425, 114)
        Me.chkSpindle.Name = "chkSpindle"
        Me.chkSpindle.Size = New System.Drawing.Size(61, 17)
        Me.chkSpindle.TabIndex = 263
        Me.chkSpindle.Text = "Spindle"
        Me.chkSpindle.UseVisualStyleBackColor = True
        '
        'chkRapid
        '
        Me.chkRapid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkRapid.AutoSize = True
        Me.chkRapid.Enabled = False
        Me.chkRapid.Location = New System.Drawing.Point(365, 114)
        Me.chkRapid.Name = "chkRapid"
        Me.chkRapid.Size = New System.Drawing.Size(54, 17)
        Me.chkRapid.TabIndex = 262
        Me.chkRapid.Text = "Rapid"
        Me.chkRapid.UseVisualStyleBackColor = True
        '
        'chkFeedrate
        '
        Me.chkFeedrate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkFeedrate.AutoSize = True
        Me.chkFeedrate.Enabled = False
        Me.chkFeedrate.Location = New System.Drawing.Point(291, 114)
        Me.chkFeedrate.Name = "chkFeedrate"
        Me.chkFeedrate.Size = New System.Drawing.Size(68, 17)
        Me.chkFeedrate.TabIndex = 261
        Me.chkFeedrate.Text = "Feedrate"
        Me.chkFeedrate.UseVisualStyleBackColor = True
        '
        'chartOverride
        '
        Me.chartOverride.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chartOverride.BackColor = System.Drawing.Color.Transparent
        Me.chartOverride.BorderlineColor = System.Drawing.Color.Transparent
        ChartArea3.Area3DStyle.Inclination = 0
        ChartArea3.Area3DStyle.IsRightAngleAxes = False
        ChartArea3.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic
        ChartArea3.Area3DStyle.Rotation = 50
        ChartArea3.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
        ChartArea3.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds
        ChartArea3.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds
        ChartArea3.AxisX.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ChartArea3.AxisX.LabelStyle.Format = "hh:mm:ss"
        ChartArea3.AxisX.MajorGrid.Enabled = False
        ChartArea3.AxisX.ScaleBreakStyle.Enabled = True
        ChartArea3.AxisX.ScaleView.MinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds
        ChartArea3.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        ChartArea3.AxisY.LineColor = System.Drawing.Color.Transparent
        ChartArea3.AxisY.MajorGrid.Enabled = False
        ChartArea3.AxisY.MajorTickMark.Enabled = False
        ChartArea3.AxisY.Maximum = 100.0R
        ChartArea3.BackColor = System.Drawing.Color.Transparent
        ChartArea3.BorderColor = System.Drawing.Color.Transparent
        ChartArea3.CursorX.IntervalOffset = 1.0R
        ChartArea3.CursorX.IsUserEnabled = True
        ChartArea3.CursorX.IsUserSelectionEnabled = True
        ChartArea3.InnerPlotPosition.Auto = False
        ChartArea3.InnerPlotPosition.Height = 78.01672!
        ChartArea3.InnerPlotPosition.Width = 95.92!
        ChartArea3.InnerPlotPosition.X = 0.08000183!
        ChartArea3.Name = "ChartArea1"
        Me.chartOverride.ChartAreas.Add(ChartArea3)
        Me.chartOverride.Cursor = System.Windows.Forms.Cursors.Default
        Legend1.Name = "Legend1"
        Me.chartOverride.Legends.Add(Legend1)
        Me.chartOverride.Location = New System.Drawing.Point(1, 4)
        Me.chartOverride.Margin = New System.Windows.Forms.Padding(4)
        Me.chartOverride.Name = "chartOverride"
        Me.chartOverride.Size = New System.Drawing.Size(1008, 139)
        Me.chartOverride.TabIndex = 260
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label3.Location = New System.Drawing.Point(1018, 176)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(15, 13)
        Me.Label3.TabIndex = 259
        Me.Label3.Text = "H"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnExpande
        '
        Me.btnExpande.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExpande.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExpande.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnExpande.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.btnExpande.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnExpande.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExpande.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.d
        Me.btnExpande.Location = New System.Drawing.Point(1012, 184)
        Me.btnExpande.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExpande.Name = "btnExpande"
        Me.btnExpande.Size = New System.Drawing.Size(24, 25)
        Me.btnExpande.TabIndex = 257
        Me.btnExpande.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1021, 4)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 258
        Me.Label2.Text = "TM"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.d
        Me.Button1.Location = New System.Drawing.Point(1019, 14)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(24, 25)
        Me.Button1.TabIndex = 260
        Me.Button1.UseVisualStyleBackColor = True
        '
        'chartHistory
        '
        Me.chartHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chartHistory.BackColor = System.Drawing.Color.Transparent
        Me.chartHistory.BorderlineColor = System.Drawing.Color.Transparent
        ChartArea4.AxisX.IsLabelAutoFit = False
        ChartArea4.AxisX.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ChartArea4.AxisX.LabelStyle.Format = "hh:mm:ss"
        ChartArea4.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours
        ChartArea4.AxisX.MajorGrid.Enabled = False
        ChartArea4.AxisX.MajorTickMark.Enabled = False
        ChartArea4.AxisX.MinorTickMark.Enabled = True
        ChartArea4.AxisY.IsLabelAutoFit = False
        ChartArea4.AxisY.LabelStyle.Font = New System.Drawing.Font("Segoe UI Symbol", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ChartArea4.AxisY.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea4.AxisY.LineColor = System.Drawing.Color.Transparent
        ChartArea4.AxisY.MajorGrid.LineColor = System.Drawing.Color.DimGray
        ChartArea4.AxisY.MajorTickMark.Enabled = False
        ChartArea4.AxisY.Maximum = 100.0R
        ChartArea4.BackColor = System.Drawing.Color.Transparent
        ChartArea4.BorderColor = System.Drawing.Color.Transparent
        ChartArea4.CursorX.IntervalOffset = 1.0R
        ChartArea4.CursorX.IsUserEnabled = True
        ChartArea4.CursorX.IsUserSelectionEnabled = True
        ChartArea4.Name = "ChartArea1"
        Me.chartHistory.ChartAreas.Add(ChartArea4)
        Me.chartHistory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chartHistory.Location = New System.Drawing.Point(10, 12)
        Me.chartHistory.Margin = New System.Windows.Forms.Padding(4)
        Me.chartHistory.Name = "chartHistory"
        Me.chartHistory.Size = New System.Drawing.Size(1009, 361)
        Me.chartHistory.TabIndex = 254
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(4, 4)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1061, 32)
        Me.FlowLayoutPanel1.TabIndex = 254
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.BTN_RAW_VALUES)
        Me.Panel1.Controls.Add(Me.Disp_labels)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 43)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1063, 22)
        Me.Panel1.TabIndex = 258
        '
        'BTN_RAW_VALUES
        '
        Me.BTN_RAW_VALUES.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTN_RAW_VALUES.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_RAW_VALUES.FlatAppearance.BorderSize = 0
        Me.BTN_RAW_VALUES.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_RAW_VALUES.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.BTN_RAW_VALUES.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_RAW_VALUES.Location = New System.Drawing.Point(914, -1)
        Me.BTN_RAW_VALUES.Name = "BTN_RAW_VALUES"
        Me.BTN_RAW_VALUES.Size = New System.Drawing.Size(143, 26)
        Me.BTN_RAW_VALUES.TabIndex = 261
        Me.BTN_RAW_VALUES.Text = "Display Raw data"
        Me.BTN_RAW_VALUES.UseVisualStyleBackColor = True
        '
        'Disp_labels
        '
        Me.Disp_labels.AutoSize = True
        Me.Disp_labels.Location = New System.Drawing.Point(3, 0)
        Me.Disp_labels.Name = "Disp_labels"
        Me.Disp_labels.Size = New System.Drawing.Size(120, 17)
        Me.Disp_labels.TabIndex = 260
        Me.Disp_labels.Text = "Display Cycle Times"
        Me.Disp_labels.UseVisualStyleBackColor = True
        '
        'Form_Shift
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1069, 750)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_Shift"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "TimeLinex"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.chartPartNumbers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.chartTimeline, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartOverride, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnExpande As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents chartHistory As DataVisualization.Charting.Chart
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents chartTimeline As DataVisualization.Charting.Chart
    Friend WithEvents chartPartNumbers As DataVisualization.Charting.Chart
    Friend WithEvents Disp_labels As CheckBox
    Friend WithEvents BTN_RAW_VALUES As Button
    Friend WithEvents chartOverride As DataVisualization.Charting.Chart
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents chkFeedrate As CheckBox
    Friend WithEvents chkRapid As CheckBox
    Friend WithEvents chkSpindle As CheckBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reporting_partsNumber
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Reporting_partsNumber))
        Me.TreeView_machine = New System.Windows.Forms.TreeView()
        Me.BTN_GenerateReport = New System.Windows.Forms.Button()
        Me.BTN_SetDefaultPath = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BTN_BrowseFolder = New System.Windows.Forms.Button()
        Me.tb_pathReport = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BTN_Yearly = New System.Windows.Forms.Button()
        Me.BTN_Monthly = New System.Windows.Forms.Button()
        Me.BTN_Weekly = New System.Windows.Forms.Button()
        Me.BTN_Daily = New System.Windows.Forms.Button()
        Me.DTP_StartDate = New System.Windows.Forms.DateTimePicker()
        Me.DTP_EndDate = New System.Windows.Forms.DateTimePicker()
        Me.CBX_Format = New System.Windows.Forms.ComboBox()
        Me.TB_OutputName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TreeView_machine
        '
        Me.TreeView_machine.BackColor = System.Drawing.SystemColors.Window
        Me.TreeView_machine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TreeView_machine.CheckBoxes = True
        Me.TreeView_machine.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView_machine.HotTracking = True
        Me.TreeView_machine.LabelEdit = True
        Me.TreeView_machine.LineColor = System.Drawing.Color.Blue
        Me.TreeView_machine.Location = New System.Drawing.Point(15, 223)
        Me.TreeView_machine.Margin = New System.Windows.Forms.Padding(4)
        Me.TreeView_machine.Name = "TreeView_machine"
        Me.TreeView_machine.Size = New System.Drawing.Size(366, 381)
        Me.TreeView_machine.TabIndex = 43
        '
        'BTN_GenerateReport
        '
        Me.BTN_GenerateReport.AutoEllipsis = True
        Me.BTN_GenerateReport.Location = New System.Drawing.Point(231, 657)
        Me.BTN_GenerateReport.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_GenerateReport.Name = "BTN_GenerateReport"
        Me.BTN_GenerateReport.Size = New System.Drawing.Size(151, 47)
        Me.BTN_GenerateReport.TabIndex = 44
        Me.BTN_GenerateReport.Text = "Generate Report"
        Me.BTN_GenerateReport.UseVisualStyleBackColor = True
        '
        'BTN_SetDefaultPath
        '
        Me.BTN_SetDefaultPath.Location = New System.Drawing.Point(310, 25)
        Me.BTN_SetDefaultPath.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_SetDefaultPath.Name = "BTN_SetDefaultPath"
        Me.BTN_SetDefaultPath.Size = New System.Drawing.Size(71, 28)
        Me.BTN_SetDefaultPath.TabIndex = 41
        Me.BTN_SetDefaultPath.Text = "Default"
        Me.BTN_SetDefaultPath.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 9)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 45
        Me.Label6.Text = "Export folder"
        '
        'BTN_BrowseFolder
        '
        Me.BTN_BrowseFolder.Location = New System.Drawing.Point(270, 25)
        Me.BTN_BrowseFolder.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_BrowseFolder.Name = "BTN_BrowseFolder"
        Me.BTN_BrowseFolder.Size = New System.Drawing.Size(35, 28)
        Me.BTN_BrowseFolder.TabIndex = 40
        Me.BTN_BrowseFolder.Text = "..."
        Me.BTN_BrowseFolder.UseVisualStyleBackColor = True
        '
        'tb_pathReport
        '
        Me.tb_pathReport.Location = New System.Drawing.Point(15, 28)
        Me.tb_pathReport.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_pathReport.Name = "tb_pathReport"
        Me.tb_pathReport.Size = New System.Drawing.Size(244, 20)
        Me.tb_pathReport.TabIndex = 39
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.BTN_Yearly)
        Me.GroupBox1.Controls.Add(Me.BTN_Monthly)
        Me.GroupBox1.Controls.Add(Me.BTN_Weekly)
        Me.GroupBox1.Controls.Add(Me.BTN_Daily)
        Me.GroupBox1.Controls.Add(Me.DTP_StartDate)
        Me.GroupBox1.Controls.Add(Me.DTP_EndDate)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.GroupBox1.Location = New System.Drawing.Point(15, 66)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(367, 149)
        Me.GroupBox1.TabIndex = 49
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Report dates"
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Light", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(1, 70)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 20)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Period : "
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Light", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(1, 107)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "End date : "
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Light", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(1, 35)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Start date : "
        '
        'BTN_Yearly
        '
        Me.BTN_Yearly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTN_Yearly.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.BTN_Yearly.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_Yearly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Yearly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_Yearly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Yearly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Yearly.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.BTN_Yearly.Location = New System.Drawing.Point(295, 70)
        Me.BTN_Yearly.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Yearly.Name = "BTN_Yearly"
        Me.BTN_Yearly.Size = New System.Drawing.Size(53, 28)
        Me.BTN_Yearly.TabIndex = 8
        Me.BTN_Yearly.Text = "Y"
        Me.BTN_Yearly.UseVisualStyleBackColor = True
        '
        'BTN_Monthly
        '
        Me.BTN_Monthly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTN_Monthly.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.BTN_Monthly.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_Monthly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Monthly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_Monthly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Monthly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Monthly.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.BTN_Monthly.Location = New System.Drawing.Point(237, 70)
        Me.BTN_Monthly.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Monthly.Name = "BTN_Monthly"
        Me.BTN_Monthly.Size = New System.Drawing.Size(53, 28)
        Me.BTN_Monthly.TabIndex = 7
        Me.BTN_Monthly.Text = "M"
        Me.BTN_Monthly.UseVisualStyleBackColor = True
        '
        'BTN_Weekly
        '
        Me.BTN_Weekly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTN_Weekly.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.BTN_Weekly.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_Weekly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Weekly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_Weekly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Weekly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Weekly.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.BTN_Weekly.Location = New System.Drawing.Point(179, 70)
        Me.BTN_Weekly.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Weekly.Name = "BTN_Weekly"
        Me.BTN_Weekly.Size = New System.Drawing.Size(52, 28)
        Me.BTN_Weekly.TabIndex = 6
        Me.BTN_Weekly.Text = "W"
        Me.BTN_Weekly.UseVisualStyleBackColor = True
        '
        'BTN_Daily
        '
        Me.BTN_Daily.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTN_Daily.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.BTN_Daily.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightSteelBlue
        Me.BTN_Daily.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Daily.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.BTN_Daily.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Daily.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Daily.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.BTN_Daily.Location = New System.Drawing.Point(126, 70)
        Me.BTN_Daily.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Daily.Name = "BTN_Daily"
        Me.BTN_Daily.Size = New System.Drawing.Size(48, 28)
        Me.BTN_Daily.TabIndex = 5
        Me.BTN_Daily.Text = "D"
        Me.BTN_Daily.UseVisualStyleBackColor = True
        '
        'DTP_StartDate
        '
        Me.DTP_StartDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DTP_StartDate.CalendarFont = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_StartDate.CustomFormat = "dd MMMM yyyy-ddd"
        Me.DTP_StartDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_StartDate.Location = New System.Drawing.Point(111, 34)
        Me.DTP_StartDate.Margin = New System.Windows.Forms.Padding(4)
        Me.DTP_StartDate.Name = "DTP_StartDate"
        Me.DTP_StartDate.Size = New System.Drawing.Size(247, 23)
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
        Me.DTP_EndDate.Location = New System.Drawing.Point(111, 106)
        Me.DTP_EndDate.Margin = New System.Windows.Forms.Padding(4)
        Me.DTP_EndDate.Name = "DTP_EndDate"
        Me.DTP_EndDate.Size = New System.Drawing.Size(248, 23)
        Me.DTP_EndDate.TabIndex = 1
        '
        'CBX_Format
        '
        Me.CBX_Format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBX_Format.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBX_Format.FormattingEnabled = True
        Me.CBX_Format.Items.AddRange(New Object() {"xlsx", "csv"})
        Me.CBX_Format.Location = New System.Drawing.Point(15, 624)
        Me.CBX_Format.Margin = New System.Windows.Forms.Padding(4)
        Me.CBX_Format.Name = "CBX_Format"
        Me.CBX_Format.Size = New System.Drawing.Size(79, 21)
        Me.CBX_Format.TabIndex = 52
        '
        'TB_OutputName
        '
        Me.TB_OutputName.Location = New System.Drawing.Point(103, 625)
        Me.TB_OutputName.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_OutputName.Name = "TB_OutputName"
        Me.TB_OutputName.Size = New System.Drawing.Size(277, 20)
        Me.TB_OutputName.TabIndex = 53
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.Label4.Location = New System.Drawing.Point(264, 608)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "Output file name: "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.Label5.Location = New System.Drawing.Point(11, 608)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "format:"
        '
        'Reporting_partsNumber
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(395, 722)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TB_OutputName)
        Me.Controls.Add(Me.CBX_Format)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TreeView_machine)
        Me.Controls.Add(Me.BTN_GenerateReport)
        Me.Controls.Add(Me.BTN_SetDefaultPath)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.BTN_BrowseFolder)
        Me.Controls.Add(Me.tb_pathReport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Reporting_partsNumber"
        Me.Text = "Reporting: parts Numbers"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TreeView_machine As System.Windows.Forms.TreeView
    Friend WithEvents BTN_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents BTN_SetDefaultPath As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BTN_BrowseFolder As System.Windows.Forms.Button
    Friend WithEvents tb_pathReport As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BTN_Yearly As System.Windows.Forms.Button
    Friend WithEvents BTN_Monthly As System.Windows.Forms.Button
    Friend WithEvents BTN_Weekly As System.Windows.Forms.Button
    Friend WithEvents BTN_Daily As System.Windows.Forms.Button
    Friend WithEvents DTP_StartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTP_EndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents CBX_Format As System.Windows.Forms.ComboBox
    Friend WithEvents TB_OutputName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class

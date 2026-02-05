<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Event_report
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
        Me.PB_loading = New System.Windows.Forms.PictureBox()
        Me.TreeView_machine = New System.Windows.Forms.TreeView()
        Me.BTN_GenerateReport = New System.Windows.Forms.Button()
        Me.BTN_SetDefaultPath = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BTN_BrowseFolder = New System.Windows.Forms.Button()
        Me.tb_pathReport = New System.Windows.Forms.TextBox()
        Me.CB_Events = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.CHB_event = New System.Windows.Forms.CheckBox()
        Me.GB_ReportType = New System.Windows.Forms.GroupBox()
        Me.RB_Month = New System.Windows.Forms.RadioButton()
        Me.RB_Week = New System.Windows.Forms.RadioButton()
        Me.RB_Day = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DTP_End = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DTP_Start = New System.Windows.Forms.DateTimePicker()
        CType(Me.PB_loading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_ReportType.SuspendLayout()
        Me.SuspendLayout()
        '
        'PB_loading
        '
        Me.PB_loading.Image = Global.CSI_Reporting_Application.My.Resources.Resources.loading_aero
        Me.PB_loading.Location = New System.Drawing.Point(18, 487)
        Me.PB_loading.Name = "PB_loading"
        Me.PB_loading.Size = New System.Drawing.Size(154, 27)
        Me.PB_loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_loading.TabIndex = 43
        Me.PB_loading.TabStop = False
        Me.PB_loading.Visible = False
        '
        'TreeView_machine
        '
        Me.TreeView_machine.AllowDrop = True
        Me.TreeView_machine.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView_machine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TreeView_machine.CheckBoxes = True
        Me.TreeView_machine.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView_machine.HotTracking = True
        Me.TreeView_machine.LabelEdit = True
        Me.TreeView_machine.LineColor = System.Drawing.Color.Blue
        Me.TreeView_machine.Location = New System.Drawing.Point(17, 151)
        Me.TreeView_machine.Name = "TreeView_machine"
        Me.TreeView_machine.Size = New System.Drawing.Size(275, 227)
        Me.TreeView_machine.TabIndex = 40
        '
        'BTN_GenerateReport
        '
        Me.BTN_GenerateReport.AutoEllipsis = True
        Me.BTN_GenerateReport.Location = New System.Drawing.Point(178, 487)
        Me.BTN_GenerateReport.Name = "BTN_GenerateReport"
        Me.BTN_GenerateReport.Size = New System.Drawing.Size(114, 30)
        Me.BTN_GenerateReport.TabIndex = 41
        Me.BTN_GenerateReport.Text = "Generate Report"
        Me.BTN_GenerateReport.UseVisualStyleBackColor = True
        '
        'BTN_SetDefaultPath
        '
        Me.BTN_SetDefaultPath.Location = New System.Drawing.Point(239, 461)
        Me.BTN_SetDefaultPath.Name = "BTN_SetDefaultPath"
        Me.BTN_SetDefaultPath.Size = New System.Drawing.Size(53, 23)
        Me.BTN_SetDefaultPath.TabIndex = 39
        Me.BTN_SetDefaultPath.Text = "Default"
        Me.BTN_SetDefaultPath.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 445)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Export folder"
        '
        'BTN_BrowseFolder
        '
        Me.BTN_BrowseFolder.Location = New System.Drawing.Point(207, 461)
        Me.BTN_BrowseFolder.Name = "BTN_BrowseFolder"
        Me.BTN_BrowseFolder.Size = New System.Drawing.Size(26, 23)
        Me.BTN_BrowseFolder.TabIndex = 38
        Me.BTN_BrowseFolder.Text = "..."
        Me.BTN_BrowseFolder.UseVisualStyleBackColor = True
        '
        'tb_pathReport
        '
        Me.tb_pathReport.Location = New System.Drawing.Point(17, 461)
        Me.tb_pathReport.Name = "tb_pathReport"
        Me.tb_pathReport.Size = New System.Drawing.Size(184, 20)
        Me.tb_pathReport.TabIndex = 37
        '
        'CB_Events
        '
        Me.CB_Events.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_Events.FormattingEnabled = True
        Me.CB_Events.Location = New System.Drawing.Point(56, 396)
        Me.CB_Events.Name = "CB_Events"
        Me.CB_Events.Size = New System.Drawing.Size(236, 21)
        Me.CB_Events.TabIndex = 46
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 399)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 47
        Me.Label1.Text = "Event"
        '
        'BackgroundWorker
        '
        '
        'CHB_event
        '
        Me.CHB_event.AutoSize = True
        Me.CHB_event.Location = New System.Drawing.Point(150, 423)
        Me.CHB_event.Name = "CHB_event"
        Me.CHB_event.Size = New System.Drawing.Size(142, 17)
        Me.CHB_event.TabIndex = 48
        Me.CHB_event.Text = "Most Predominant Event"
        Me.CHB_event.UseVisualStyleBackColor = True
        '
        'GB_ReportType
        '
        Me.GB_ReportType.Controls.Add(Me.RB_Month)
        Me.GB_ReportType.Controls.Add(Me.RB_Week)
        Me.GB_ReportType.Controls.Add(Me.RB_Day)
        Me.GB_ReportType.Location = New System.Drawing.Point(17, 107)
        Me.GB_ReportType.Name = "GB_ReportType"
        Me.GB_ReportType.Size = New System.Drawing.Size(275, 38)
        Me.GB_ReportType.TabIndex = 46
        Me.GB_ReportType.TabStop = False
        Me.GB_ReportType.Text = "Report type"
        '
        'RB_Month
        '
        Me.RB_Month.AutoSize = True
        Me.RB_Month.Location = New System.Drawing.Point(194, 15)
        Me.RB_Month.Name = "RB_Month"
        Me.RB_Month.Size = New System.Drawing.Size(62, 17)
        Me.RB_Month.TabIndex = 2
        Me.RB_Month.TabStop = True
        Me.RB_Month.Text = "Monthly"
        Me.RB_Month.UseVisualStyleBackColor = True
        '
        'RB_Week
        '
        Me.RB_Week.AutoSize = True
        Me.RB_Week.Location = New System.Drawing.Point(97, 15)
        Me.RB_Week.Name = "RB_Week"
        Me.RB_Week.Size = New System.Drawing.Size(61, 17)
        Me.RB_Week.TabIndex = 1
        Me.RB_Week.TabStop = True
        Me.RB_Week.Text = "Weekly"
        Me.RB_Week.UseVisualStyleBackColor = True
        '
        'RB_Day
        '
        Me.RB_Day.AutoSize = True
        Me.RB_Day.Checked = True
        Me.RB_Day.Location = New System.Drawing.Point(5, 15)
        Me.RB_Day.Name = "RB_Day"
        Me.RB_Day.Size = New System.Drawing.Size(48, 17)
        Me.RB_Day.TabIndex = 0
        Me.RB_Day.TabStop = True
        Me.RB_Day.Text = "Daily"
        Me.RB_Day.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 13)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "End date and time"
        '
        'DTP_End
        '
        Me.DTP_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_End.Location = New System.Drawing.Point(18, 80)
        Me.DTP_End.Name = "DTP_End"
        Me.DTP_End.Size = New System.Drawing.Size(271, 20)
        Me.DTP_End.TabIndex = 42
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 13)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Start date and time"
        '
        'DTP_Start
        '
        Me.DTP_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_Start.Location = New System.Drawing.Point(18, 30)
        Me.DTP_Start.Name = "DTP_Start"
        Me.DTP_Start.Size = New System.Drawing.Size(271, 20)
        Me.DTP_Start.TabIndex = 40
        '
        'Event_report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(310, 527)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GB_ReportType)
        Me.Controls.Add(Me.DTP_End)
        Me.Controls.Add(Me.CHB_event)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DTP_Start)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CB_Events)
        Me.Controls.Add(Me.PB_loading)
        Me.Controls.Add(Me.TreeView_machine)
        Me.Controls.Add(Me.BTN_GenerateReport)
        Me.Controls.Add(Me.BTN_SetDefaultPath)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.BTN_BrowseFolder)
        Me.Controls.Add(Me.tb_pathReport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Event_report"
        Me.Text = "Event report"
        CType(Me.PB_loading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GB_ReportType.ResumeLayout(False)
        Me.GB_ReportType.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PB_loading As System.Windows.Forms.PictureBox
    Friend WithEvents TreeView_machine As System.Windows.Forms.TreeView
    Friend WithEvents BTN_GenerateReport As System.Windows.Forms.Button
    Friend WithEvents BTN_SetDefaultPath As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BTN_BrowseFolder As System.Windows.Forms.Button
    Friend WithEvents tb_pathReport As System.Windows.Forms.TextBox
    Friend WithEvents CB_Events As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents CHB_event As System.Windows.Forms.CheckBox
    Friend WithEvents GB_ReportType As System.Windows.Forms.GroupBox
    Friend WithEvents RB_Month As System.Windows.Forms.RadioButton
    Friend WithEvents RB_Week As System.Windows.Forms.RadioButton
    Friend WithEvents RB_Day As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DTP_End As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DTP_Start As System.Windows.Forms.DateTimePicker
End Class

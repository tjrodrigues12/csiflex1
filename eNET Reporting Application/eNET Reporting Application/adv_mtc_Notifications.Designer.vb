<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class adv_mtc_Notifications
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DGV_stat = New System.Windows.Forms.DataGridView()
        Me.DataGridViewComboBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Delay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LBL_NOTIFONSTATUS = New System.Windows.Forms.Label()
        Me.LBL_RETR = New System.Windows.Forms.Label()
        Me.PB_processing = New System.Windows.Forms.PictureBox()
        Me.DGV_Cond = New System.Windows.Forms.DataGridView()
        Me.Condition = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Warning = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Fault = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BW_conditions = New System.ComponentModel.BackgroundWorker()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DGV_stat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_processing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_Cond, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DGV_stat)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LBL_NOTIFONSTATUS)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.LBL_RETR)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PB_processing)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DGV_Cond)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Size = New System.Drawing.Size(650, 442)
        Me.SplitContainer1.SplitterDistance = 299
        Me.SplitContainer1.TabIndex = 2
        '
        'DGV_stat
        '
        Me.DGV_stat.AllowUserToAddRows = False
        Me.DGV_stat.AllowUserToDeleteRows = False
        Me.DGV_stat.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_stat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_stat.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DGV_stat.BackgroundColor = System.Drawing.Color.White
        Me.DGV_stat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DGV_stat.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.DGV_stat.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_stat.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGV_stat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_stat.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewComboBoxColumn1, Me.Status, Me.Delay})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV_stat.DefaultCellStyle = DataGridViewCellStyle2
        Me.DGV_stat.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.DGV_stat.Location = New System.Drawing.Point(8, 40)
        Me.DGV_stat.MultiSelect = False
        Me.DGV_stat.Name = "DGV_stat"
        Me.DGV_stat.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGV_stat.RowHeadersVisible = False
        Me.DGV_stat.RowHeadersWidth = 40
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        Me.DGV_stat.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGV_stat.RowTemplate.Height = 24
        Me.DGV_stat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DGV_stat.Size = New System.Drawing.Size(288, 392)
        Me.DGV_stat.TabIndex = 15
        '
        'DataGridViewComboBoxColumn1
        '
        Me.DataGridViewComboBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.DataGridViewComboBoxColumn1.HeaderText = "Activate"
        Me.DataGridViewComboBoxColumn1.Name = "DataGridViewComboBoxColumn1"
        Me.DataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn1.Width = 53
        '
        'Status
        '
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        '
        'Delay
        '
        Me.Delay.HeaderText = "Delay"
        Me.Delay.Name = "Delay"
        '
        'LBL_NOTIFONSTATUS
        '
        Me.LBL_NOTIFONSTATUS.AutoSize = True
        Me.LBL_NOTIFONSTATUS.Location = New System.Drawing.Point(8, 9)
        Me.LBL_NOTIFONSTATUS.Name = "LBL_NOTIFONSTATUS"
        Me.LBL_NOTIFONSTATUS.Size = New System.Drawing.Size(165, 13)
        Me.LBL_NOTIFONSTATUS.TabIndex = 4
        Me.LBL_NOTIFONSTATUS.Text = "Notification on defined status:"
        '
        'LBL_RETR
        '
        Me.LBL_RETR.AutoSize = True
        Me.LBL_RETR.BackColor = System.Drawing.Color.White
        Me.LBL_RETR.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.LBL_RETR.Location = New System.Drawing.Point(54, 266)
        Me.LBL_RETR.Name = "LBL_RETR"
        Me.LBL_RETR.Size = New System.Drawing.Size(187, 15)
        Me.LBL_RETR.TabIndex = 42
        Me.LBL_RETR.Text = "Retrieving data from the machine "
        '
        'PB_processing
        '
        Me.PB_processing.Image = Global.CSI_Reporting_Application.My.Resources.Resources.processing
        Me.PB_processing.Location = New System.Drawing.Point(38, 315)
        Me.PB_processing.Name = "PB_processing"
        Me.PB_processing.Size = New System.Drawing.Size(121, 115)
        Me.PB_processing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_processing.TabIndex = 41
        Me.PB_processing.TabStop = False
        '
        'DGV_Cond
        '
        Me.DGV_Cond.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_Cond.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_Cond.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DGV_Cond.BackgroundColor = System.Drawing.Color.White
        Me.DGV_Cond.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DGV_Cond.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.DGV_Cond.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV_Cond.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DGV_Cond.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Cond.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Condition, Me.Warning, Me.Fault, Me.DataGridViewTextBoxColumn1})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV_Cond.DefaultCellStyle = DataGridViewCellStyle5
        Me.DGV_Cond.Location = New System.Drawing.Point(3, 40)
        Me.DGV_Cond.MultiSelect = False
        Me.DGV_Cond.Name = "DGV_Cond"
        Me.DGV_Cond.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGV_Cond.RowHeadersVisible = False
        Me.DGV_Cond.RowHeadersWidth = 40
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        Me.DGV_Cond.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.DGV_Cond.RowTemplate.Height = 24
        Me.DGV_Cond.Size = New System.Drawing.Size(337, 392)
        Me.DGV_Cond.TabIndex = 15
        '
        'Condition
        '
        Me.Condition.HeaderText = "Condition"
        Me.Condition.Name = "Condition"
        '
        'Warning
        '
        Me.Warning.HeaderText = "Warning"
        Me.Warning.Name = "Warning"
        '
        'Fault
        '
        Me.Fault.HeaderText = "Fault (Alarm)"
        Me.Fault.Name = "Fault"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Delay"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(188, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Notification conditions and alarms:"
        '
        'BW_conditions
        '
        Me.BW_conditions.WorkerReportsProgress = True
        Me.BW_conditions.WorkerSupportsCancellation = True
        '
        'adv_mtc_Notifications
        '
        Me.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.ClientSize = New System.Drawing.Size(650, 442)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "adv_mtc_Notifications"
        Me.Text = "Notifications manager"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DGV_stat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_processing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_Cond, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents LBL_NOTIFONSTATUS As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents DGV_stat As DataGridView
    Friend WithEvents DataGridViewComboBoxColumn1 As DataGridViewCheckBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents Delay As DataGridViewTextBoxColumn
    Friend WithEvents DGV_Cond As DataGridView
    Friend WithEvents BW_conditions As System.ComponentModel.BackgroundWorker
    Friend WithEvents PB_processing As PictureBox
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents Fault As DataGridViewCheckBoxColumn
    Friend WithEvents Warning As DataGridViewCheckBoxColumn
    Friend WithEvents Condition As DataGridViewTextBoxColumn
    Friend WithEvents LBL_RETR As Label
End Class

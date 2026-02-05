<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GeneralSettings
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RB2 = New System.Windows.Forms.RadioButton()
        Me.RB_1 = New System.Windows.Forms.RadioButton()
        Me.CB_shiftcompare = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TB_percent = New System.Windows.Forms.TextBox()
        Me.GB_PROD = New System.Windows.Forms.GroupBox()
        Me.CB_LOADING = New System.Windows.Forms.CheckBox()
        Me.ElapsedTime_CB = New System.Windows.Forms.CheckBox()
        Me.CurrentCycle_CB = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.BTN_DashboardReset = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbStartWeek = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GB_PROD.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.GroupBox1.Controls.Add(Me.RB2)
        Me.GroupBox1.Controls.Add(Me.RB_1)
        Me.GroupBox1.Controls.Add(Me.CB_shiftcompare)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TB_percent)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 191)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(639, 84)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Trends :"
        '
        'RB2
        '
        Me.RB2.AutoSize = True
        Me.RB2.Location = New System.Drawing.Point(16, 53)
        Me.RB2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RB2.Name = "RB2"
        Me.RB2.Size = New System.Drawing.Size(178, 17)
        Me.RB2.TabIndex = 49
        Me.RB2.TabStop = True
        Me.RB2.Text = "Production % for the current shift"
        Me.RB2.UseVisualStyleBackColor = True
        '
        'RB_1
        '
        Me.RB_1.AutoSize = True
        Me.RB_1.Location = New System.Drawing.Point(16, 24)
        Me.RB_1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RB_1.Name = "RB_1"
        Me.RB_1.Size = New System.Drawing.Size(209, 17)
        Me.RB_1.TabIndex = 48
        Me.RB_1.TabStop = True
        Me.RB_1.Text = "Compare the production time of the last"
        Me.RB_1.UseVisualStyleBackColor = True
        '
        'CB_shiftcompare
        '
        Me.CB_shiftcompare.Enabled = False
        Me.CB_shiftcompare.FormattingEnabled = True
        Me.CB_shiftcompare.Items.AddRange(New Object() {"The actual shift", "The current week", "The current month"})
        Me.CB_shiftcompare.Location = New System.Drawing.Point(386, 23)
        Me.CB_shiftcompare.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CB_shiftcompare.Name = "CB_shiftcompare"
        Me.CB_shiftcompare.Size = New System.Drawing.Size(242, 21)
        Me.CB_shiftcompare.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Enabled = False
        Me.Label2.Location = New System.Drawing.Point(255, 26)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "% of the current shift with "
        '
        'TB_percent
        '
        Me.TB_percent.Enabled = False
        Me.TB_percent.Location = New System.Drawing.Point(228, 23)
        Me.TB_percent.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_percent.Name = "TB_percent"
        Me.TB_percent.Size = New System.Drawing.Size(25, 20)
        Me.TB_percent.TabIndex = 0
        Me.TB_percent.Text = "20"
        Me.TB_percent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GB_PROD
        '
        Me.GB_PROD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GB_PROD.Controls.Add(Me.CB_LOADING)
        Me.GB_PROD.Controls.Add(Me.ElapsedTime_CB)
        Me.GB_PROD.Controls.Add(Me.CurrentCycle_CB)
        Me.GB_PROD.Location = New System.Drawing.Point(290, 11)
        Me.GB_PROD.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GB_PROD.Name = "GB_PROD"
        Me.GB_PROD.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GB_PROD.Size = New System.Drawing.Size(140, 176)
        Me.GB_PROD.TabIndex = 7
        Me.GB_PROD.TabStop = False
        Me.GB_PROD.Text = "Production is:"
        '
        'CB_LOADING
        '
        Me.CB_LOADING.AutoSize = True
        Me.CB_LOADING.Enabled = False
        Me.CB_LOADING.Location = New System.Drawing.Point(17, 78)
        Me.CB_LOADING.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CB_LOADING.Name = "CB_LOADING"
        Me.CB_LOADING.Size = New System.Drawing.Size(64, 17)
        Me.CB_LOADING.TabIndex = 43
        Me.CB_LOADING.Text = "Loading"
        Me.CB_LOADING.UseVisualStyleBackColor = True
        '
        'ElapsedTime_CB
        '
        Me.ElapsedTime_CB.AutoSize = True
        Me.ElapsedTime_CB.Enabled = False
        Me.ElapsedTime_CB.Location = New System.Drawing.Point(17, 53)
        Me.ElapsedTime_CB.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ElapsedTime_CB.Name = "ElapsedTime_CB"
        Me.ElapsedTime_CB.Size = New System.Drawing.Size(54, 17)
        Me.ElapsedTime_CB.TabIndex = 42
        Me.ElapsedTime_CB.Text = "Setup"
        Me.ElapsedTime_CB.UseVisualStyleBackColor = True
        '
        'CurrentCycle_CB
        '
        Me.CurrentCycle_CB.AutoSize = True
        Me.CurrentCycle_CB.Checked = True
        Me.CurrentCycle_CB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CurrentCycle_CB.Enabled = False
        Me.CurrentCycle_CB.Location = New System.Drawing.Point(17, 28)
        Me.CurrentCycle_CB.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CurrentCycle_CB.Name = "CurrentCycle_CB"
        Me.CurrentCycle_CB.Size = New System.Drawing.Size(67, 17)
        Me.CurrentCycle_CB.TabIndex = 41
        Me.CurrentCycle_CB.Text = "Cycle on"
        Me.CurrentCycle_CB.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.your_success_
        Me.PictureBox1.Location = New System.Drawing.Point(9, 11)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(268, 176)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'BTN_DashboardReset
        '
        Me.BTN_DashboardReset.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_DashboardReset.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_DashboardReset.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_DashboardReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_DashboardReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_DashboardReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_DashboardReset.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.BTN_DashboardReset.Location = New System.Drawing.Point(435, 155)
        Me.BTN_DashboardReset.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.BTN_DashboardReset.Name = "BTN_DashboardReset"
        Me.BTN_DashboardReset.Size = New System.Drawing.Size(213, 32)
        Me.BTN_DashboardReset.TabIndex = 16
        Me.BTN_DashboardReset.Text = "Reset Dashboard Timeline"
        Me.BTN_DashboardReset.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cmbStartWeek)
        Me.GroupBox2.Location = New System.Drawing.Point(435, 11)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(213, 139)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        '
        'cmbStartWeek
        '
        Me.cmbStartWeek.FormattingEnabled = True
        Me.cmbStartWeek.Items.AddRange(New Object() {"Sunday", "Monday"})
        Me.cmbStartWeek.Location = New System.Drawing.Point(6, 49)
        Me.cmbStartWeek.Name = "cmbStartWeek"
        Me.cmbStartWeek.Size = New System.Drawing.Size(196, 21)
        Me.cmbStartWeek.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "First day of the week"
        '
        'GeneralSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(658, 287)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.BTN_DashboardReset)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GB_PROD)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GeneralSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "GeneralSettings (Applied for all dashboards)"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GB_PROD.ResumeLayout(False)
        Me.GB_PROD.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RB2 As RadioButton
    Friend WithEvents RB_1 As RadioButton
    Friend WithEvents CB_shiftcompare As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TB_percent As TextBox
    Friend WithEvents GB_PROD As GroupBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents CB_LOADING As CheckBox
    Friend WithEvents ElapsedTime_CB As CheckBox
    Friend WithEvents CurrentCycle_CB As CheckBox
    Friend WithEvents BTN_DashboardReset As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbStartWeek As ComboBox
End Class

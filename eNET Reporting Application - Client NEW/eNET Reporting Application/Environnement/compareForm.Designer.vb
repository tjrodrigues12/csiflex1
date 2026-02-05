<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CF
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
        Me.components = New System.ComponentModel.Container()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TreeView_machine = New System.Windows.Forms.TreeView()
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BTN_Add = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RB_Shift3 = New System.Windows.Forms.RadioButton()
        Me.RB_Shift2 = New System.Windows.Forms.RadioButton()
        Me.RB_Shift1 = New System.Windows.Forms.RadioButton()
        Me.BTN_Yearly = New System.Windows.Forms.Button()
        Me.BTN_Monthly = New System.Windows.Forms.Button()
        Me.BTN_Weekly = New System.Windows.Forms.Button()
        Me.BTN_Dayly = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DTP_StartDate = New System.Windows.Forms.DateTimePicker()
        Me.DTP_EndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button6
        '
        Me.Button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button6.Enabled = False
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Location = New System.Drawing.Point(157, 1625)
        Me.Button6.Margin = New System.Windows.Forms.Padding(4)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(139, 29)
        Me.Button6.TabIndex = 32
        Me.Button6.Text = "Create"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Size = New System.Drawing.Size(324, 717)
        Me.SplitContainer1.SplitterDistance = 500
        Me.SplitContainer1.TabIndex = 34
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.TreeView_machine)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(324, 500)
        Me.GroupBox2.TabIndex = 34
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Machines : "
        '
        'TreeView_machine
        '
        Me.TreeView_machine.CheckBoxes = True
        Me.TreeView_machine.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView_machine.HotTracking = True
        Me.TreeView_machine.ImageIndex = 0
        Me.TreeView_machine.ImageList = Me.ImageList
        Me.TreeView_machine.Location = New System.Drawing.Point(4, 23)
        Me.TreeView_machine.Margin = New System.Windows.Forms.Padding(4)
        Me.TreeView_machine.Name = "TreeView_machine"
        Me.TreeView_machine.SelectedImageIndex = Me.TreeView_machine.ImageIndex
        Me.TreeView_machine.Size = New System.Drawing.Size(316, 473)
        Me.TreeView_machine.TabIndex = 19
        '
        'ImageList
        '
        Me.ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.BTN_Add)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.BTN_Yearly)
        Me.GroupBox1.Controls.Add(Me.BTN_Monthly)
        Me.GroupBox1.Controls.Add(Me.BTN_Weekly)
        Me.GroupBox1.Controls.Add(Me.BTN_Dayly)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.DTP_StartDate)
        Me.GroupBox1.Controls.Add(Me.DTP_EndDate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.DarkBlue
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(324, 213)
        Me.GroupBox1.TabIndex = 32
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select dates"
        '
        'BTN_Add
        '
        Me.BTN_Add.Location = New System.Drawing.Point(211, 140)
        Me.BTN_Add.Name = "BTN_Add"
        Me.BTN_Add.Size = New System.Drawing.Size(75, 50)
        Me.BTN_Add.TabIndex = 10
        Me.BTN_Add.Text = "Add"
        Me.BTN_Add.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RB_Shift3)
        Me.GroupBox3.Controls.Add(Me.RB_Shift2)
        Me.GroupBox3.Controls.Add(Me.RB_Shift1)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 128)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(176, 62)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Shift"
        '
        'RB_Shift3
        '
        Me.RB_Shift3.Appearance = System.Windows.Forms.Appearance.Button
        Me.RB_Shift3.AutoSize = True
        Me.RB_Shift3.Location = New System.Drawing.Point(107, 25)
        Me.RB_Shift3.Name = "RB_Shift3"
        Me.RB_Shift3.Size = New System.Drawing.Size(27, 29)
        Me.RB_Shift3.TabIndex = 5
        Me.RB_Shift3.TabStop = True
        Me.RB_Shift3.Text = "3"
        Me.RB_Shift3.UseVisualStyleBackColor = True
        '
        'RB_Shift2
        '
        Me.RB_Shift2.Appearance = System.Windows.Forms.Appearance.Button
        Me.RB_Shift2.AutoSize = True
        Me.RB_Shift2.Location = New System.Drawing.Point(74, 25)
        Me.RB_Shift2.Name = "RB_Shift2"
        Me.RB_Shift2.Size = New System.Drawing.Size(27, 29)
        Me.RB_Shift2.TabIndex = 4
        Me.RB_Shift2.TabStop = True
        Me.RB_Shift2.Text = "2"
        Me.RB_Shift2.UseVisualStyleBackColor = True
        '
        'RB_Shift1
        '
        Me.RB_Shift1.Appearance = System.Windows.Forms.Appearance.Button
        Me.RB_Shift1.AutoSize = True
        Me.RB_Shift1.Checked = True
        Me.RB_Shift1.Location = New System.Drawing.Point(41, 25)
        Me.RB_Shift1.Name = "RB_Shift1"
        Me.RB_Shift1.Size = New System.Drawing.Size(27, 29)
        Me.RB_Shift1.TabIndex = 3
        Me.RB_Shift1.TabStop = True
        Me.RB_Shift1.Text = "1"
        Me.RB_Shift1.UseVisualStyleBackColor = True
        '
        'BTN_Yearly
        '
        Me.BTN_Yearly.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray
        Me.BTN_Yearly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Yearly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BTN_Yearly.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BTN_Yearly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Yearly.Location = New System.Drawing.Point(221, 58)
        Me.BTN_Yearly.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Yearly.Name = "BTN_Yearly"
        Me.BTN_Yearly.Size = New System.Drawing.Size(34, 29)
        Me.BTN_Yearly.TabIndex = 8
        Me.BTN_Yearly.Text = "Y"
        Me.BTN_Yearly.UseVisualStyleBackColor = True
        '
        'BTN_Monthly
        '
        Me.BTN_Monthly.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray
        Me.BTN_Monthly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Monthly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BTN_Monthly.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BTN_Monthly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Monthly.Location = New System.Drawing.Point(180, 58)
        Me.BTN_Monthly.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Monthly.Name = "BTN_Monthly"
        Me.BTN_Monthly.Size = New System.Drawing.Size(34, 29)
        Me.BTN_Monthly.TabIndex = 7
        Me.BTN_Monthly.Text = "M"
        Me.BTN_Monthly.UseVisualStyleBackColor = True
        '
        'BTN_Weekly
        '
        Me.BTN_Weekly.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray
        Me.BTN_Weekly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Weekly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BTN_Weekly.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BTN_Weekly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Weekly.Location = New System.Drawing.Point(139, 58)
        Me.BTN_Weekly.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Weekly.Name = "BTN_Weekly"
        Me.BTN_Weekly.Size = New System.Drawing.Size(34, 29)
        Me.BTN_Weekly.TabIndex = 6
        Me.BTN_Weekly.Text = "W"
        Me.BTN_Weekly.UseVisualStyleBackColor = True
        '
        'BTN_Dayly
        '
        Me.BTN_Dayly.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray
        Me.BTN_Dayly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BTN_Dayly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BTN_Dayly.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BTN_Dayly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Dayly.Location = New System.Drawing.Point(98, 58)
        Me.BTN_Dayly.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Dayly.Name = "BTN_Dayly"
        Me.BTN_Dayly.Size = New System.Drawing.Size(34, 29)
        Me.BTN_Dayly.TabIndex = 5
        Me.BTN_Dayly.Text = "D"
        Me.BTN_Dayly.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 28)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 19)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Start date : "
        '
        'DTP_StartDate
        '
        Me.DTP_StartDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DTP_StartDate.Location = New System.Drawing.Point(92, 22)
        Me.DTP_StartDate.Margin = New System.Windows.Forms.Padding(4)
        Me.DTP_StartDate.Name = "DTP_StartDate"
        Me.DTP_StartDate.Size = New System.Drawing.Size(176, 26)
        Me.DTP_StartDate.TabIndex = 0
        '
        'DTP_EndDate
        '
        Me.DTP_EndDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DTP_EndDate.Location = New System.Drawing.Point(92, 95)
        Me.DTP_EndDate.Margin = New System.Windows.Forms.Padding(4)
        Me.DTP_EndDate.Name = "DTP_EndDate"
        Me.DTP_EndDate.Size = New System.Drawing.Size(178, 26)
        Me.DTP_EndDate.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 101)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 19)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "End date : "
        '
        'CF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(324, 717)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Button6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "CF"
        Me.Text = "compareForm"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TreeView_machine As System.Windows.Forms.TreeView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents BTN_Yearly As System.Windows.Forms.Button
    Friend WithEvents BTN_Monthly As System.Windows.Forms.Button
    Friend WithEvents BTN_Weekly As System.Windows.Forms.Button
    Friend WithEvents BTN_Dayly As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DTP_StartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTP_EndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents RB_Shift3 As System.Windows.Forms.RadioButton
    Friend WithEvents RB_Shift2 As System.Windows.Forms.RadioButton
    Friend WithEvents RB_Shift1 As System.Windows.Forms.RadioButton
    Friend WithEvents BTN_Add As System.Windows.Forms.Button
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
End Class

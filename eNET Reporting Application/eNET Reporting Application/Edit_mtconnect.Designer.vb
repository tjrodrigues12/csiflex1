<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Edit_mtconnect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Edit_mtconnect))
        Me.TV_MTC = New System.Windows.Forms.TreeView()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LB_MachineAddress = New System.Windows.Forms.TextBox()
        Me.LV_Property = New System.Windows.Forms.ListView()
        Me.Parameter = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Value = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CBX_TypeCond = New System.Windows.Forms.ComboBox()
        Me.DGV_Conditions = New System.Windows.Forms.DataGridView()
        Me.BTN_RemoveCond = New System.Windows.Forms.Button()
        Me.BTN_AddCond = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.BTN_Done = New System.Windows.Forms.Button()
        Me.CB_StatusAssociation = New System.Windows.Forms.ComboBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TB_StatusAssoID = New System.Windows.Forms.TextBox()
        Me.BTN_Define = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Advencedcond_mtc = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_Conditions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TV_MTC
        '
        Me.TV_MTC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TV_MTC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TV_MTC.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TV_MTC.Location = New System.Drawing.Point(13, 178)
        Me.TV_MTC.Margin = New System.Windows.Forms.Padding(4)
        Me.TV_MTC.Name = "TV_MTC"
        Me.TV_MTC.Size = New System.Drawing.Size(262, 580)
        Me.TV_MTC.TabIndex = 8
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.CSIFLEXLOGOTR2
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(718, 140)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'LB_MachineAddress
        '
        Me.LB_MachineAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LB_MachineAddress.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_MachineAddress.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.LB_MachineAddress.Location = New System.Drawing.Point(199, 133)
        Me.LB_MachineAddress.Margin = New System.Windows.Forms.Padding(4)
        Me.LB_MachineAddress.Name = "LB_MachineAddress"
        Me.LB_MachineAddress.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.LB_MachineAddress.Size = New System.Drawing.Size(418, 25)
        Me.LB_MachineAddress.TabIndex = 11
        Me.LB_MachineAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LV_Property
        '
        Me.LV_Property.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LV_Property.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Parameter, Me.Value})
        Me.LV_Property.Location = New System.Drawing.Point(392, 520)
        Me.LV_Property.Margin = New System.Windows.Forms.Padding(4)
        Me.LV_Property.Name = "LV_Property"
        Me.LV_Property.Size = New System.Drawing.Size(249, 180)
        Me.LV_Property.TabIndex = 12
        Me.LV_Property.UseCompatibleStateImageBehavior = False
        Me.LV_Property.View = System.Windows.Forms.View.Details
        '
        'Parameter
        '
        Me.Parameter.Text = "Parameter"
        Me.Parameter.Width = 130
        '
        'Value
        '
        Me.Value.Text = "Value"
        Me.Value.Width = 115
        '
        'CBX_TypeCond
        '
        Me.CBX_TypeCond.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBX_TypeCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBX_TypeCond.FormattingEnabled = True
        Me.CBX_TypeCond.Location = New System.Drawing.Point(434, 178)
        Me.CBX_TypeCond.Margin = New System.Windows.Forms.Padding(4)
        Me.CBX_TypeCond.Name = "CBX_TypeCond"
        Me.CBX_TypeCond.Size = New System.Drawing.Size(277, 21)
        Me.CBX_TypeCond.TabIndex = 23
        '
        'DGV_Conditions
        '
        Me.DGV_Conditions.AllowUserToAddRows = False
        Me.DGV_Conditions.AllowUserToDeleteRows = False
        Me.DGV_Conditions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_Conditions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_Conditions.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DGV_Conditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Conditions.Location = New System.Drawing.Point(434, 206)
        Me.DGV_Conditions.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV_Conditions.MultiSelect = False
        Me.DGV_Conditions.Name = "DGV_Conditions"
        Me.DGV_Conditions.ReadOnly = True
        Me.DGV_Conditions.RowHeadersVisible = False
        Me.DGV_Conditions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.DGV_Conditions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_Conditions.Size = New System.Drawing.Size(277, 133)
        Me.DGV_Conditions.TabIndex = 22
        '
        'BTN_RemoveCond
        '
        Me.BTN_RemoveCond.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTN_RemoveCond.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_RemoveCond.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_RemoveCond.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_RemoveCond.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_RemoveCond.Location = New System.Drawing.Point(573, 342)
        Me.BTN_RemoveCond.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_RemoveCond.Name = "BTN_RemoveCond"
        Me.BTN_RemoveCond.Size = New System.Drawing.Size(138, 28)
        Me.BTN_RemoveCond.TabIndex = 21
        Me.BTN_RemoveCond.Text = "Remove Condition"
        Me.BTN_RemoveCond.UseVisualStyleBackColor = False
        '
        'BTN_AddCond
        '
        Me.BTN_AddCond.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_AddCond.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_AddCond.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_AddCond.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_AddCond.Location = New System.Drawing.Point(281, 206)
        Me.BTN_AddCond.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_AddCond.Name = "BTN_AddCond"
        Me.BTN_AddCond.Size = New System.Drawing.Size(145, 105)
        Me.BTN_AddCond.TabIndex = 20
        Me.BTN_AddCond.Text = "Add Condition"
        Me.BTN_AddCond.UseVisualStyleBackColor = False
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox2.Location = New System.Drawing.Point(301, 179)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.TextBox2.Size = New System.Drawing.Size(107, 18)
        Me.TextBox2.TabIndex = 19
        Me.TextBox2.Text = "Condition for"
        '
        'BTN_Done
        '
        Me.BTN_Done.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_Done.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Done.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Done.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Done.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Done.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Done.Location = New System.Drawing.Point(319, 708)
        Me.BTN_Done.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Done.Name = "BTN_Done"
        Me.BTN_Done.Size = New System.Drawing.Size(356, 50)
        Me.BTN_Done.TabIndex = 24
        Me.BTN_Done.Text = "Done"
        Me.BTN_Done.UseVisualStyleBackColor = False
        '
        'CB_StatusAssociation
        '
        Me.CB_StatusAssociation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CB_StatusAssociation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_StatusAssociation.FormattingEnabled = True
        Me.CB_StatusAssociation.Location = New System.Drawing.Point(488, 433)
        Me.CB_StatusAssociation.Margin = New System.Windows.Forms.Padding(4)
        Me.CB_StatusAssociation.Name = "CB_StatusAssociation"
        Me.CB_StatusAssociation.Size = New System.Drawing.Size(222, 21)
        Me.CB_StatusAssociation.TabIndex = 26
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox1.Location = New System.Drawing.Point(302, 434)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.TextBox1.Size = New System.Drawing.Size(178, 18)
        Me.TextBox1.TabIndex = 25
        Me.TextBox1.Text = "Associatiat with :"
        '
        'TB_StatusAssoID
        '
        Me.TB_StatusAssoID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TB_StatusAssoID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TB_StatusAssoID.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_StatusAssoID.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TB_StatusAssoID.Location = New System.Drawing.Point(487, 480)
        Me.TB_StatusAssoID.Margin = New System.Windows.Forms.Padding(4)
        Me.TB_StatusAssoID.Name = "TB_StatusAssoID"
        Me.TB_StatusAssoID.ReadOnly = True
        Me.TB_StatusAssoID.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.TB_StatusAssoID.Size = New System.Drawing.Size(223, 25)
        Me.TB_StatusAssoID.TabIndex = 27
        Me.TB_StatusAssoID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BTN_Define
        '
        Me.BTN_Define.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTN_Define.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Define.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Define.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Define.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Define.Location = New System.Drawing.Point(301, 480)
        Me.BTN_Define.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Define.Name = "BTN_Define"
        Me.BTN_Define.Size = New System.Drawing.Size(173, 30)
        Me.BTN_Define.TabIndex = 28
        Me.BTN_Define.Text = "Define ID"
        Me.BTN_Define.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(435, 342)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(138, 28)
        Me.Button1.TabIndex = 29
        Me.Button1.Text = "Modify Condition"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Advencedcond_mtc
        '
        Me.Advencedcond_mtc.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Advencedcond_mtc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Advencedcond_mtc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Advencedcond_mtc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Advencedcond_mtc.Location = New System.Drawing.Point(281, 319)
        Me.Advencedcond_mtc.Margin = New System.Windows.Forms.Padding(4)
        Me.Advencedcond_mtc.Name = "Advencedcond_mtc"
        Me.Advencedcond_mtc.Size = New System.Drawing.Size(145, 51)
        Me.Advencedcond_mtc.TabIndex = 30
        Me.Advencedcond_mtc.Text = "Advanced condition edit"
        Me.Advencedcond_mtc.UseVisualStyleBackColor = False
        '
        'Edit_mtconnect
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(718, 764)
        Me.Controls.Add(Me.Advencedcond_mtc)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BTN_Define)
        Me.Controls.Add(Me.TB_StatusAssoID)
        Me.Controls.Add(Me.CB_StatusAssociation)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.BTN_Done)
        Me.Controls.Add(Me.CBX_TypeCond)
        Me.Controls.Add(Me.DGV_Conditions)
        Me.Controls.Add(Me.BTN_RemoveCond)
        Me.Controls.Add(Me.BTN_AddCond)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.LV_Property)
        Me.Controls.Add(Me.LB_MachineAddress)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TV_MTC)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Edit_mtconnect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit Machine Property"
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_Conditions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TV_MTC As System.Windows.Forms.TreeView
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents LB_MachineAddress As System.Windows.Forms.TextBox
    Friend WithEvents LV_Property As System.Windows.Forms.ListView
    Friend WithEvents Parameter As System.Windows.Forms.ColumnHeader
    Friend WithEvents Value As System.Windows.Forms.ColumnHeader
    Friend WithEvents CBX_TypeCond As System.Windows.Forms.ComboBox
    Friend WithEvents DGV_Conditions As System.Windows.Forms.DataGridView
    Friend WithEvents BTN_RemoveCond As System.Windows.Forms.Button
    Friend WithEvents BTN_AddCond As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Done As System.Windows.Forms.Button
    Friend WithEvents CB_StatusAssociation As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TB_StatusAssoID As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Define As System.Windows.Forms.Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Advencedcond_mtc As Button
End Class

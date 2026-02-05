<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Edit_Focas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Edit_Focas))
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.propertyList = New System.Windows.Forms.ListView()
        Me.Parameter = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Value = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CBX_TypeCond = New System.Windows.Forms.ComboBox()
        Me.DGV_Conditions = New System.Windows.Forms.DataGridView()
        Me.BTN_RemoveCond = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.BTN_Done = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_Conditions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.Location = New System.Drawing.Point(1, 112)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(241, 435)
        Me.TreeView1.TabIndex = 8
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.FANUC
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(502, 109)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox1.Location = New System.Drawing.Point(192, 5)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.TextBox1.Size = New System.Drawing.Size(306, 25)
        Me.TextBox1.TabIndex = 11
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'propertyList
        '
        Me.propertyList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.propertyList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Parameter, Me.Value})
        Me.propertyList.Location = New System.Drawing.Point(246, 346)
        Me.propertyList.Name = "propertyList"
        Me.propertyList.Size = New System.Drawing.Size(252, 201)
        Me.propertyList.TabIndex = 12
        Me.propertyList.UseCompatibleStateImageBehavior = False
        Me.propertyList.View = System.Windows.Forms.View.Details
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
        Me.CBX_TypeCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBX_TypeCond.FormattingEnabled = True
        Me.CBX_TypeCond.Location = New System.Drawing.Point(340, 144)
        Me.CBX_TypeCond.Name = "CBX_TypeCond"
        Me.CBX_TypeCond.Size = New System.Drawing.Size(109, 21)
        Me.CBX_TypeCond.TabIndex = 23
        '
        'DGV_Conditions
        '
        Me.DGV_Conditions.AllowUserToAddRows = False
        Me.DGV_Conditions.AllowUserToDeleteRows = False
        Me.DGV_Conditions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_Conditions.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DGV_Conditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Conditions.Location = New System.Drawing.Point(245, 167)
        Me.DGV_Conditions.MultiSelect = False
        Me.DGV_Conditions.Name = "DGV_Conditions"
        Me.DGV_Conditions.ReadOnly = True
        Me.DGV_Conditions.RowHeadersVisible = False
        Me.DGV_Conditions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.DGV_Conditions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_Conditions.Size = New System.Drawing.Size(252, 172)
        Me.DGV_Conditions.TabIndex = 22
        '
        'BTN_RemoveCond
        '
        Me.BTN_RemoveCond.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_RemoveCond.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_RemoveCond.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_RemoveCond.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_RemoveCond.Location = New System.Drawing.Point(340, 115)
        Me.BTN_RemoveCond.Name = "BTN_RemoveCond"
        Me.BTN_RemoveCond.Size = New System.Drawing.Size(109, 23)
        Me.BTN_RemoveCond.TabIndex = 21
        Me.BTN_RemoveCond.Text = "Remove Condition"
        Me.BTN_RemoveCond.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(248, 115)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(86, 23)
        Me.Button1.TabIndex = 20
        Me.Button1.Text = "Add Condition"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox2.Location = New System.Drawing.Point(254, 147)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.TextBox2.Size = New System.Drawing.Size(80, 18)
        Me.TextBox2.TabIndex = 19
        Me.TextBox2.Text = "Condition for"
        '
        'BTN_Done
        '
        Me.BTN_Done.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Done.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Done.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Done.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Done.Location = New System.Drawing.Point(456, 114)
        Me.BTN_Done.Name = "BTN_Done"
        Me.BTN_Done.Size = New System.Drawing.Size(42, 51)
        Me.BTN_Done.TabIndex = 24
        Me.BTN_Done.Text = "Done"
        Me.BTN_Done.UseVisualStyleBackColor = False
        '
        'Edit_Focas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(502, 549)
        Me.Controls.Add(Me.BTN_Done)
        Me.Controls.Add(Me.CBX_TypeCond)
        Me.Controls.Add(Me.DGV_Conditions)
        Me.Controls.Add(Me.BTN_RemoveCond)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.propertyList)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TreeView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Edit_Focas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit Machine Property"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_Conditions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents propertyList As System.Windows.Forms.ListView
    Friend WithEvents Parameter As System.Windows.Forms.ColumnHeader
    Friend WithEvents Value As System.Windows.Forms.ColumnHeader
    Friend WithEvents CBX_TypeCond As System.Windows.Forms.ComboBox
    Friend WithEvents DGV_Conditions As System.Windows.Forms.DataGridView
    Friend WithEvents BTN_RemoveCond As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Done As System.Windows.Forms.Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreateMTCrules
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
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.propertyList = New System.Windows.Forms.ListView()
        Me.Parameter = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Value = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BTN_RemoveCond = New System.Windows.Forms.Button()
        Me.DGV_Conditions = New System.Windows.Forms.DataGridView()
        Me.CBX_TypeCond = New System.Windows.Forms.ComboBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_Conditions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.Location = New System.Drawing.Point(1, 138)
        Me.TreeView1.Margin = New System.Windows.Forms.Padding(4)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(320, 534)
        Me.TreeView1.TabIndex = 8
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.MTConnectLogo
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(669, 134)
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox1.Location = New System.Drawing.Point(256, 6)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.TextBox1.Size = New System.Drawing.Size(407, 25)
        Me.TextBox1.TabIndex = 11
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'propertyList
        '
        Me.propertyList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.propertyList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Parameter, Me.Value})
        Me.propertyList.Location = New System.Drawing.Point(328, 426)
        Me.propertyList.Margin = New System.Windows.Forms.Padding(4)
        Me.propertyList.Name = "propertyList"
        Me.propertyList.Size = New System.Drawing.Size(335, 246)
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
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.TextBox2.Location = New System.Drawing.Point(501, 177)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.TextBox2.Size = New System.Drawing.Size(107, 18)
        Me.TextBox2.TabIndex = 13
        Me.TextBox2.Text = "CYCLE ON is : "
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(331, 142)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(147, 28)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Add Condition"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BTN_RemoveCond
        '
        Me.BTN_RemoveCond.Location = New System.Drawing.Point(485, 142)
        Me.BTN_RemoveCond.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_RemoveCond.Name = "BTN_RemoveCond"
        Me.BTN_RemoveCond.Size = New System.Drawing.Size(147, 28)
        Me.BTN_RemoveCond.TabIndex = 16
        Me.BTN_RemoveCond.Text = "Remove Condition"
        Me.BTN_RemoveCond.UseVisualStyleBackColor = True
        '
        'DGV_Conditions
        '
        Me.DGV_Conditions.AllowUserToAddRows = False
        Me.DGV_Conditions.AllowUserToDeleteRows = False
        Me.DGV_Conditions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_Conditions.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DGV_Conditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Conditions.Location = New System.Drawing.Point(328, 207)
        Me.DGV_Conditions.Margin = New System.Windows.Forms.Padding(4)
        Me.DGV_Conditions.MultiSelect = False
        Me.DGV_Conditions.Name = "DGV_Conditions"
        Me.DGV_Conditions.ReadOnly = True
        Me.DGV_Conditions.RowHeadersVisible = False
        Me.DGV_Conditions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.DGV_Conditions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_Conditions.Size = New System.Drawing.Size(336, 212)
        Me.DGV_Conditions.TabIndex = 17
        '
        'CBX_TypeCond
        '
        Me.CBX_TypeCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBX_TypeCond.FormattingEnabled = True
        Me.CBX_TypeCond.Location = New System.Drawing.Point(332, 177)
        Me.CBX_TypeCond.Margin = New System.Windows.Forms.Padding(4)
        Me.CBX_TypeCond.Name = "CBX_TypeCond"
        Me.CBX_TypeCond.Size = New System.Drawing.Size(144, 24)
        Me.CBX_TypeCond.TabIndex = 18
        '
        'CreateMTCrules
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(669, 676)
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
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "CreateMTCrules"
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
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents BTN_RemoveCond As System.Windows.Forms.Button
    Friend WithEvents DGV_Conditions As System.Windows.Forms.DataGridView
    Friend WithEvents CBX_TypeCond As System.Windows.Forms.ComboBox
End Class

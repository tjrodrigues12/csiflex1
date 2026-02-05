<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetMinMaxValues
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetMinMaxValues))
        Me.LBL_AttributeName = New System.Windows.Forms.Label()
        Me.TB_MIN = New System.Windows.Forms.TextBox()
        Me.LBL_MIN = New System.Windows.Forms.Label()
        Me.TB_MAX = New System.Windows.Forms.TextBox()
        Me.LBL_MAX = New System.Windows.Forms.Label()
        Me.BTN_Save = New System.Windows.Forms.Button()
        Me.BTN_Cancel = New System.Windows.Forms.Button()
        Me.LBL_MCHName = New System.Windows.Forms.Label()
        Me.LBL_MCHIP = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LBL_AttributeName
        '
        Me.LBL_AttributeName.AutoSize = True
        Me.LBL_AttributeName.Location = New System.Drawing.Point(31, 29)
        Me.LBL_AttributeName.Name = "LBL_AttributeName"
        Me.LBL_AttributeName.Size = New System.Drawing.Size(110, 17)
        Me.LBL_AttributeName.TabIndex = 0
        Me.LBL_AttributeName.Text = "Attribute Name :"
        '
        'TB_MIN
        '
        Me.TB_MIN.Location = New System.Drawing.Point(74, 65)
        Me.TB_MIN.Name = "TB_MIN"
        Me.TB_MIN.Size = New System.Drawing.Size(94, 22)
        Me.TB_MIN.TabIndex = 2
        '
        'LBL_MIN
        '
        Me.LBL_MIN.AutoSize = True
        Me.LBL_MIN.Location = New System.Drawing.Point(28, 68)
        Me.LBL_MIN.Name = "LBL_MIN"
        Me.LBL_MIN.Size = New System.Drawing.Size(40, 17)
        Me.LBL_MIN.TabIndex = 1
        Me.LBL_MIN.Text = "MIN :"
        '
        'TB_MAX
        '
        Me.TB_MAX.Location = New System.Drawing.Point(237, 65)
        Me.TB_MAX.Name = "TB_MAX"
        Me.TB_MAX.Size = New System.Drawing.Size(94, 22)
        Me.TB_MAX.TabIndex = 4
        '
        'LBL_MAX
        '
        Me.LBL_MAX.AutoSize = True
        Me.LBL_MAX.Location = New System.Drawing.Point(186, 68)
        Me.LBL_MAX.Name = "LBL_MAX"
        Me.LBL_MAX.Size = New System.Drawing.Size(45, 17)
        Me.LBL_MAX.TabIndex = 3
        Me.LBL_MAX.Text = "MAX :"
        '
        'BTN_Save
        '
        Me.BTN_Save.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Save.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BTN_Save.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Save.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Save.Location = New System.Drawing.Point(19, 114)
        Me.BTN_Save.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Save.Name = "BTN_Save"
        Me.BTN_Save.Size = New System.Drawing.Size(149, 33)
        Me.BTN_Save.TabIndex = 5
        Me.BTN_Save.Text = "Save"
        Me.BTN_Save.UseVisualStyleBackColor = False
        '
        'BTN_Cancel
        '
        Me.BTN_Cancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BTN_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Cancel.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Cancel.Location = New System.Drawing.Point(183, 114)
        Me.BTN_Cancel.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Cancel.Name = "BTN_Cancel"
        Me.BTN_Cancel.Size = New System.Drawing.Size(149, 33)
        Me.BTN_Cancel.TabIndex = 6
        Me.BTN_Cancel.Text = "Cancel"
        Me.BTN_Cancel.UseVisualStyleBackColor = False
        '
        'LBL_MCHName
        '
        Me.LBL_MCHName.AutoSize = True
        Me.LBL_MCHName.Location = New System.Drawing.Point(44, 9)
        Me.LBL_MCHName.Name = "LBL_MCHName"
        Me.LBL_MCHName.Size = New System.Drawing.Size(0, 17)
        Me.LBL_MCHName.TabIndex = 10
        Me.LBL_MCHName.Visible = False
        '
        'LBL_MCHIP
        '
        Me.LBL_MCHIP.AutoSize = True
        Me.LBL_MCHIP.Location = New System.Drawing.Point(116, 12)
        Me.LBL_MCHIP.Name = "LBL_MCHIP"
        Me.LBL_MCHIP.Size = New System.Drawing.Size(0, 17)
        Me.LBL_MCHIP.TabIndex = 11
        Me.LBL_MCHIP.Visible = False
        '
        'SetMinMaxValues
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(359, 161)
        Me.Controls.Add(Me.LBL_MCHIP)
        Me.Controls.Add(Me.LBL_MCHName)
        Me.Controls.Add(Me.BTN_Cancel)
        Me.Controls.Add(Me.BTN_Save)
        Me.Controls.Add(Me.TB_MAX)
        Me.Controls.Add(Me.LBL_MAX)
        Me.Controls.Add(Me.TB_MIN)
        Me.Controls.Add(Me.LBL_MIN)
        Me.Controls.Add(Me.LBL_AttributeName)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SetMinMaxValues"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Set MIN/MAX Values"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LBL_AttributeName As Label
    Friend WithEvents TB_MIN As TextBox
    Friend WithEvents LBL_MIN As Label
    Friend WithEvents TB_MAX As TextBox
    Friend WithEvents LBL_MAX As Label
    Friend WithEvents BTN_Save As Button
    Friend WithEvents BTN_Cancel As Button
    Friend WithEvents LBL_MCHName As Label
    Friend WithEvents LBL_MCHIP As Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UninstallAndBackUp
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BTN_Yes = New System.Windows.Forms.Button()
        Me.BTN_No = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RBTN_ConfigOnly = New System.Windows.Forms.RadioButton()
        Me.RBTN_DataPlus = New System.Windows.Forms.RadioButton()
        Me.BTN_Cancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(73, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(426, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "This will uninstall the service.  Do you want to take backup before?"
        '
        'BTN_Yes
        '
        Me.BTN_Yes.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Yes.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Yes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Yes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Yes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Yes.Location = New System.Drawing.Point(150, 148)
        Me.BTN_Yes.Name = "BTN_Yes"
        Me.BTN_Yes.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Yes.TabIndex = 4
        Me.BTN_Yes.Text = "Yes"
        Me.BTN_Yes.UseVisualStyleBackColor = False
        '
        'BTN_No
        '
        Me.BTN_No.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_No.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_No.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_No.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_No.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_No.Location = New System.Drawing.Point(250, 148)
        Me.BTN_No.Name = "BTN_No"
        Me.BTN_No.Size = New System.Drawing.Size(75, 23)
        Me.BTN_No.TabIndex = 5
        Me.BTN_No.Text = "No"
        Me.BTN_No.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(239, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "BackUp Type "
        '
        'RBTN_ConfigOnly
        '
        Me.RBTN_ConfigOnly.AutoSize = True
        Me.RBTN_ConfigOnly.Checked = True
        Me.RBTN_ConfigOnly.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBTN_ConfigOnly.Location = New System.Drawing.Point(214, 63)
        Me.RBTN_ConfigOnly.Name = "RBTN_ConfigOnly"
        Me.RBTN_ConfigOnly.Size = New System.Drawing.Size(150, 21)
        Me.RBTN_ConfigOnly.TabIndex = 2
        Me.RBTN_ConfigOnly.TabStop = True
        Me.RBTN_ConfigOnly.Text = "Configurations Only"
        Me.RBTN_ConfigOnly.UseVisualStyleBackColor = True
        '
        'RBTN_DataPlus
        '
        Me.RBTN_DataPlus.AutoSize = True
        Me.RBTN_DataPlus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBTN_DataPlus.Location = New System.Drawing.Point(214, 92)
        Me.RBTN_DataPlus.Name = "RBTN_DataPlus"
        Me.RBTN_DataPlus.Size = New System.Drawing.Size(163, 21)
        Me.RBTN_DataPlus.TabIndex = 3
        Me.RBTN_DataPlus.Text = "Data + Configurations"
        Me.RBTN_DataPlus.UseVisualStyleBackColor = True
        '
        'BTN_Cancel
        '
        Me.BTN_Cancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Cancel.Location = New System.Drawing.Point(351, 148)
        Me.BTN_Cancel.Name = "BTN_Cancel"
        Me.BTN_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Cancel.TabIndex = 6
        Me.BTN_Cancel.Text = "Cancel"
        Me.BTN_Cancel.UseVisualStyleBackColor = False
        '
        'UninstallAndBackUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(610, 213)
        Me.Controls.Add(Me.BTN_Cancel)
        Me.Controls.Add(Me.RBTN_DataPlus)
        Me.Controls.Add(Me.RBTN_ConfigOnly)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BTN_No)
        Me.Controls.Add(Me.BTN_Yes)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UninstallAndBackUp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BackUp Options"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents BTN_Yes As Button
    Friend WithEvents BTN_No As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents RBTN_ConfigOnly As RadioButton
    Friend WithEvents RBTN_DataPlus As RadioButton
    Friend WithEvents BTN_Cancel As Button
End Class

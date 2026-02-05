<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RMConfig
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RMConfig))
        Me.clbMachines = New System.Windows.Forms.CheckedListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Done = New System.Windows.Forms.Button()
        Me.TB_Port = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'clbMachines
        '
        Me.clbMachines.FormattingEnabled = True
        Me.clbMachines.Location = New System.Drawing.Point(12, 52)
        Me.clbMachines.Name = "clbMachines"
        Me.clbMachines.Size = New System.Drawing.Size(230, 184)
        Me.clbMachines.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(229, 36)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select the machines that you would like to monitor on your desktop"
        '
        'Done
        '
        Me.Done.FlatAppearance.BorderSize = 0
        Me.Done.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Done.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Done.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Done.Location = New System.Drawing.Point(166, 243)
        Me.Done.Name = "Done"
        Me.Done.Size = New System.Drawing.Size(75, 23)
        Me.Done.TabIndex = 2
        Me.Done.Text = "Done"
        Me.Done.UseVisualStyleBackColor = True
        '
        'TB_Port
        '
        Me.TB_Port.Enabled = False
        Me.TB_Port.Location = New System.Drawing.Point(46, 248)
        Me.TB_Port.Margin = New System.Windows.Forms.Padding(2)
        Me.TB_Port.Name = "TB_Port"
        Me.TB_Port.Size = New System.Drawing.Size(76, 20)
        Me.TB_Port.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 250)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Port :"
        '
        'RMConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(254, 279)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TB_Port)
        Me.Controls.Add(Me.Done)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.clbMachines)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "RMConfig"
        Me.Text = "RainMeter configuration"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents clbMachines As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Done As System.Windows.Forms.Button
    Friend WithEvents TB_Port As TextBox
    Friend WithEvents Label2 As Label
End Class

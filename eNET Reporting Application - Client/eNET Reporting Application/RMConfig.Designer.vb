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
        Me.Done = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CLB_RMMachines = New System.Windows.Forms.CheckedListBox()
        Me.SuspendLayout()
        '
        'Done
        '
        Me.Done.Location = New System.Drawing.Point(223, 297)
        Me.Done.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Done.Name = "Done"
        Me.Done.Size = New System.Drawing.Size(100, 28)
        Me.Done.TabIndex = 5
        Me.Done.Text = "Done"
        Me.Done.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(17, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(305, 44)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Select the machines that you would like to monitor on your desktop"
        '
        'CLB_RMMachines
        '
        Me.CLB_RMMachines.FormattingEnabled = True
        Me.CLB_RMMachines.Location = New System.Drawing.Point(16, 55)
        Me.CLB_RMMachines.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CLB_RMMachines.Name = "CLB_RMMachines"
        Me.CLB_RMMachines.Size = New System.Drawing.Size(305, 225)
        Me.CLB_RMMachines.TabIndex = 3
        '
        'RMConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(336, 334)
        Me.Controls.Add(Me.Done)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CLB_RMMachines)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "RMConfig"
        Me.Text = "Rainmeter configuration"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Done As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CLB_RMMachines As System.Windows.Forms.CheckedListBox
End Class

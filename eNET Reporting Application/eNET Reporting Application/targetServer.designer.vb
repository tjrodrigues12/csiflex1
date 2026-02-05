<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class targetServer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(targetServer))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.IP_TB = New System.Windows.Forms.TextBox()
        Me.OK_BTN = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "New IP"
        '
        'IP_TB
        '
        Me.IP_TB.Location = New System.Drawing.Point(60, 21)
        Me.IP_TB.Name = "IP_TB"
        Me.IP_TB.Size = New System.Drawing.Size(100, 20)
        Me.IP_TB.TabIndex = 1
        '
        'OK_BTN
        '
        Me.OK_BTN.Location = New System.Drawing.Point(184, 19)
        Me.OK_BTN.Name = "OK_BTN"
        Me.OK_BTN.Size = New System.Drawing.Size(75, 23)
        Me.OK_BTN.TabIndex = 4
        Me.OK_BTN.Text = "OK"
        Me.OK_BTN.UseVisualStyleBackColor = True
        '
        'targetServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 57)
        Me.Controls.Add(Me.OK_BTN)
        Me.Controls.Add(Me.IP_TB)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "targetServer"
        Me.Text = "Target server"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents IP_TB As System.Windows.Forms.TextBox
    Friend WithEvents OK_BTN As System.Windows.Forms.Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RenewLicense
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RenewLicense))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BTN_NewLicense = New System.Windows.Forms.Button()
        Me.BTN_Continue = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(89, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(248, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Your license has expired."
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(49, 41)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(320, 28)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Do you have a new license key?"
        '
        'BTN_NewLicense
        '
        Me.BTN_NewLicense.Location = New System.Drawing.Point(217, 84)
        Me.BTN_NewLicense.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_NewLicense.Name = "BTN_NewLicense"
        Me.BTN_NewLicense.Size = New System.Drawing.Size(183, 48)
        Me.BTN_NewLicense.TabIndex = 2
        Me.BTN_NewLicense.Text = "Enter new license"
        Me.BTN_NewLicense.UseVisualStyleBackColor = True
        '
        'BTN_Continue
        '
        Me.BTN_Continue.Location = New System.Drawing.Point(16, 84)
        Me.BTN_Continue.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_Continue.Name = "BTN_Continue"
        Me.BTN_Continue.Size = New System.Drawing.Size(183, 48)
        Me.BTN_Continue.TabIndex = 3
        Me.BTN_Continue.Text = "continue without license"
        Me.BTN_Continue.UseVisualStyleBackColor = True
        '
        'RenewLicense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 150)
        Me.Controls.Add(Me.BTN_Continue)
        Me.Controls.Add(Me.BTN_NewLicense)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "RenewLicense"
        Me.Text = "RenewLicense"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BTN_NewLicense As System.Windows.Forms.Button
    Friend WithEvents BTN_Continue As System.Windows.Forms.Button
End Class

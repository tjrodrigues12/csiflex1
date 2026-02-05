<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EncrytpedString
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncrytpedString))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BTN_Accept = New System.Windows.Forms.Button()
        Me.RTB_EncryptedString = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(190, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Paste the offline information in this box."
        '
        'BTN_Accept
        '
        Me.BTN_Accept.Location = New System.Drawing.Point(239, 123)
        Me.BTN_Accept.Name = "BTN_Accept"
        Me.BTN_Accept.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Accept.TabIndex = 4
        Me.BTN_Accept.Text = "Accept"
        Me.BTN_Accept.UseVisualStyleBackColor = True
        '
        'RTB_EncryptedString
        '
        Me.RTB_EncryptedString.Location = New System.Drawing.Point(23, 46)
        Me.RTB_EncryptedString.Name = "RTB_EncryptedString"
        Me.RTB_EncryptedString.Size = New System.Drawing.Size(291, 71)
        Me.RTB_EncryptedString.TabIndex = 5
        Me.RTB_EncryptedString.Text = ""
        '
        'EncrytpedString
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(335, 162)
        Me.Controls.Add(Me.RTB_EncryptedString)
        Me.Controls.Add(Me.BTN_Accept)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EncrytpedString"
        Me.Text = "Encrypted String"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BTN_Accept As System.Windows.Forms.Button
    Friend WithEvents RTB_EncryptedString As System.Windows.Forms.RichTextBox
End Class

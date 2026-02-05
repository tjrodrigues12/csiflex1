<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomEmailMsg
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
        Me.BTN_Done = New System.Windows.Forms.Button()
        Me.RTB_Msg = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BTN_Done
        '
        Me.BTN_Done.Location = New System.Drawing.Point(658, 372)
        Me.BTN_Done.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Done.Name = "BTN_Done"
        Me.BTN_Done.Size = New System.Drawing.Size(100, 28)
        Me.BTN_Done.TabIndex = 5
        Me.BTN_Done.Text = "Done"
        Me.BTN_Done.UseVisualStyleBackColor = True
        '
        'RTB_Msg
        '
        Me.RTB_Msg.Location = New System.Drawing.Point(47, 83)
        Me.RTB_Msg.Margin = New System.Windows.Forms.Padding(4)
        Me.RTB_Msg.Name = "RTB_Msg"
        Me.RTB_Msg.Size = New System.Drawing.Size(709, 281)
        Me.RTB_Msg.TabIndex = 4
        Me.RTB_Msg.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(43, 51)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(217, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Enter your custom message here"
        '
        'CustomEmailMsg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.BTN_Done)
        Me.Controls.Add(Me.RTB_Msg)
        Me.Controls.Add(Me.Label1)
        Me.Name = "CustomEmailMsg"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CustomEmailMsg"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BTN_Done As Button
    Friend WithEvents RTB_Msg As RichTextBox
    Friend WithEvents Label1 As Label
End Class

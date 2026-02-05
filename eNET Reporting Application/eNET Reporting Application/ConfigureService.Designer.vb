<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigureService
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
        Me.CHKB_LoadingAsCON = New System.Windows.Forms.CheckBox()
        Me.BTN_Accept = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CHKB_LoadingAsCON
        '
        Me.CHKB_LoadingAsCON.AutoSize = True
        Me.CHKB_LoadingAsCON.Location = New System.Drawing.Point(12, 28)
        Me.CHKB_LoadingAsCON.Name = "CHKB_LoadingAsCON"
        Me.CHKB_LoadingAsCON.Size = New System.Drawing.Size(199, 17)
        Me.CHKB_LoadingAsCON.TabIndex = 0
        Me.CHKB_LoadingAsCON.Text = "Consider Loading status as CYCLE ON"
        Me.CHKB_LoadingAsCON.UseVisualStyleBackColor = True
        '
        'BTN_Accept
        '
        Me.BTN_Accept.Location = New System.Drawing.Point(229, 77)
        Me.BTN_Accept.Name = "BTN_Accept"
        Me.BTN_Accept.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Accept.TabIndex = 1
        Me.BTN_Accept.Text = "Accept"
        Me.BTN_Accept.UseVisualStyleBackColor = True
        '
        'ConfigureService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(316, 112)
        Me.Controls.Add(Me.BTN_Accept)
        Me.Controls.Add(Me.CHKB_LoadingAsCON)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ConfigureService"
        Me.Text = "Configure Service"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CHKB_LoadingAsCON As System.Windows.Forms.CheckBox
    Friend WithEvents BTN_Accept As System.Windows.Forms.Button
End Class

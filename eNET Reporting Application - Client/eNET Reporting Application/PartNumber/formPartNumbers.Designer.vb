<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formPartNumbers
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
        Me.PartNumberControl1 = New CSIFLEX_Reporting_Client.PartNumberControl()
        Me.SuspendLayout()
        '
        'PartNumberControl1
        '
        Me.PartNumberControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PartNumberControl1.Location = New System.Drawing.Point(13, 13)
        Me.PartNumberControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.PartNumberControl1.Name = "PartNumberControl1"
        Me.PartNumberControl1.Size = New System.Drawing.Size(1241, 798)
        Me.PartNumberControl1.TabIndex = 0
        '
        'formPartNumbers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1264, 822)
        Me.Controls.Add(Me.PartNumberControl1)
        Me.Name = "formPartNumbers"
        Me.Text = "Part Numbers"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PartNumberControl1 As PartNumberControl
End Class

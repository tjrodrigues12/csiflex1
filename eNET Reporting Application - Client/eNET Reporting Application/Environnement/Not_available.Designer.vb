<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Not_available
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
        Me.ok = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lbl_message = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ok
        '
        Me.ok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ok.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ok.FlatAppearance.BorderSize = 0
        Me.ok.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ok.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ok.Location = New System.Drawing.Point(161, 208)
        Me.ok.Name = "ok"
        Me.ok.Size = New System.Drawing.Size(75, 33)
        Me.ok.TabIndex = 0
        Me.ok.Text = "ok"
        Me.ok.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSIFLEX_Reporting_Client.My.Resources.Resources.CSIFLEX_logo
        Me.PictureBox1.Location = New System.Drawing.Point(12, 50)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 114)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'lbl_message
        '
        Me.lbl_message.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_message.Location = New System.Drawing.Point(118, 60)
        Me.lbl_message.Name = "lbl_message"
        Me.lbl_message.Size = New System.Drawing.Size(247, 89)
        Me.lbl_message.TabIndex = 2
        Me.lbl_message.Text = "This functionality will be available on a future version"
        Me.lbl_message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Not_available
        '
        Me.AcceptButton = Me.ok
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.ok
        Me.ClientSize = New System.Drawing.Size(377, 253)
        Me.Controls.Add(Me.lbl_message)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ok)
        Me.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "Not_available"
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "CSIFlex Information System"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ok As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lbl_message As Label
End Class

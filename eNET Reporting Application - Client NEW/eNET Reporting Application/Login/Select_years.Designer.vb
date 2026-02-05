<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Select_years
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
        Me.BTN_ok = New System.Windows.Forms.Button()
        Me.CLB_years = New System.Windows.Forms.CheckedListBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BTN_ok
        '
        Me.BTN_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_ok.Location = New System.Drawing.Point(131, 253)
        Me.BTN_ok.Name = "BTN_ok"
        Me.BTN_ok.Size = New System.Drawing.Size(75, 29)
        Me.BTN_ok.TabIndex = 3
        Me.BTN_ok.Text = "ok"
        Me.BTN_ok.UseVisualStyleBackColor = True
        '
        'CLB_years
        '
        Me.CLB_years.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CLB_years.FormattingEnabled = True
        Me.CLB_years.Location = New System.Drawing.Point(183, 102)
        Me.CLB_years.Name = "CLB_years"
        Me.CLB_years.Size = New System.Drawing.Size(133, 136)
        Me.CLB_years.TabIndex = 2
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(12, 12)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(304, 56)
        Me.TextBox1.TabIndex = 5
        Me.TextBox1.Text = "CSIFlex has found many years of data, select what do you want to use:"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.data_integration_import
        Me.PictureBox1.Location = New System.Drawing.Point(2, 85)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(158, 153)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'Select_years
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(328, 294)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.BTN_ok)
        Me.Controls.Add(Me.CLB_years)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Select_years"
        Me.Text = "Available data"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents CLB_years As CheckedListBox
    Friend WithEvents BTN_ok As Button
    Friend WithEvents PictureBox1 As PictureBox
End Class

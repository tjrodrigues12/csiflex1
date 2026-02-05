<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IP_choice
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IP_choice))
        Me.LB_list = New System.Windows.Forms.ListBox()
        Me.ok = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LB_list
        '
        Me.LB_list.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LB_list.FormattingEnabled = True
        Me.LB_list.ItemHeight = 16
        Me.LB_list.Location = New System.Drawing.Point(69, 57)
        Me.LB_list.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LB_list.Name = "LB_list"
        Me.LB_list.Size = New System.Drawing.Size(295, 178)
        Me.LB_list.TabIndex = 0
        '
        'ok
        '
        Me.ok.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ok.Location = New System.Drawing.Point(180, 241)
        Me.ok.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ok.Name = "ok"
        Me.ok.Size = New System.Drawing.Size(75, 32)
        Me.ok.TabIndex = 1
        Me.ok.Text = "ok"
        Me.ok.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(413, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Multiple Ip Address has been found, please select the right one :"
        '
        'IP_choice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(432, 279)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ok)
        Me.Controls.Add(Me.LB_list)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "IP_choice"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select your IP address"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LB_list As ListBox
    Friend WithEvents ok As Button
    Friend WithEvents Label1 As Label
End Class

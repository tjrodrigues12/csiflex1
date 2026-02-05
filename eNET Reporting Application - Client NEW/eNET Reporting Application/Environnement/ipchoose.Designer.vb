<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ipchoose
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ipchoose))
        Me.LB_IpAddress = New System.Windows.Forms.ListBox()
        Me.BTN_Ok = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LB_IpAddress
        '
        Me.LB_IpAddress.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_IpAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LB_IpAddress.FormattingEnabled = True
        Me.LB_IpAddress.ItemHeight = 16
        Me.LB_IpAddress.Location = New System.Drawing.Point(0, 12)
        Me.LB_IpAddress.Margin = New System.Windows.Forms.Padding(4)
        Me.LB_IpAddress.Name = "LB_IpAddress"
        Me.LB_IpAddress.Size = New System.Drawing.Size(660, 176)
        Me.LB_IpAddress.TabIndex = 0
        '
        'BTN_Ok
        '
        Me.BTN_Ok.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BTN_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Ok.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Ok.Location = New System.Drawing.Point(0, 191)
        Me.BTN_Ok.Margin = New System.Windows.Forms.Padding(4)
        Me.BTN_Ok.Name = "BTN_Ok"
        Me.BTN_Ok.Size = New System.Drawing.Size(660, 38)
        Me.BTN_Ok.TabIndex = 1
        Me.BTN_Ok.Text = "ok"
        Me.BTN_Ok.UseVisualStyleBackColor = True
        '
        'ipchoose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(660, 229)
        Me.Controls.Add(Me.BTN_Ok)
        Me.Controls.Add(Me.LB_IpAddress)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ipchoose"
        Me.Text = "CSIFLEX has found many ip address, please chose one: "
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LB_IpAddress As System.Windows.Forms.ListBox
    Friend WithEvents BTN_Ok As System.Windows.Forms.Button
End Class

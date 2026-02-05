<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RegisterLicenceForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RegisterLicenceForm))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_LicenseKey = New System.Windows.Forms.TextBox()
        Me.BTN_Accept = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 16)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(148, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Enter your license key"
        '
        'TB_LicenseKey
        '
        Me.TB_LicenseKey.Location = New System.Drawing.Point(17, 37)
        Me.TB_LicenseKey.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_LicenseKey.Multiline = True
        Me.TB_LicenseKey.Name = "TB_LicenseKey"
        Me.TB_LicenseKey.Size = New System.Drawing.Size(263, 86)
        Me.TB_LicenseKey.TabIndex = 1
        '
        'BTN_Accept
        '
        Me.BTN_Accept.Location = New System.Drawing.Point(181, 132)
        Me.BTN_Accept.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_Accept.Name = "BTN_Accept"
        Me.BTN_Accept.Size = New System.Drawing.Size(100, 28)
        Me.BTN_Accept.TabIndex = 2
        Me.BTN_Accept.Text = "Accept"
        Me.BTN_Accept.UseVisualStyleBackColor = True
        '
        'RegisterLicenceForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(301, 175)
        Me.Controls.Add(Me.BTN_Accept)
        Me.Controls.Add(Me.TB_LicenseKey)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "RegisterLicenceForm"
        Me.Text = "License"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TB_LicenseKey As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Accept As System.Windows.Forms.Button
End Class

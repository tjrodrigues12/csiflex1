<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewLicenseForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewLicenseForm))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ExistingLic = New System.Windows.Forms.Button()
        Me.NewLic = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(124, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(191, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Please register your product."
        '
        'ExistingLic
        '
        Me.ExistingLic.Location = New System.Drawing.Point(208, 58)
        Me.ExistingLic.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ExistingLic.Name = "ExistingLic"
        Me.ExistingLic.Size = New System.Drawing.Size(184, 37)
        Me.ExistingLic.TabIndex = 1
        Me.ExistingLic.Text = "I have a license key"
        Me.ExistingLic.UseVisualStyleBackColor = True
        '
        'NewLic
        '
        Me.NewLic.Location = New System.Drawing.Point(16, 58)
        Me.NewLic.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.NewLic.Name = "NewLic"
        Me.NewLic.Size = New System.Drawing.Size(184, 37)
        Me.NewLic.TabIndex = 2
        Me.NewLic.Text = "I don't have a license key"
        Me.NewLic.UseVisualStyleBackColor = True
        '
        'NewLicenseForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 116)
        Me.Controls.Add(Me.NewLic)
        Me.Controls.Add(Me.ExistingLic)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "NewLicenseForm"
        Me.Text = "Register your product"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ExistingLic As System.Windows.Forms.Button
    Friend WithEvents NewLic As System.Windows.Forms.Button
End Class

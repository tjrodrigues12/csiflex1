<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DashWeb
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DashWeb))
        Me.WB_EnetLive = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'WB_EnetLive
        '
        Me.WB_EnetLive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WB_EnetLive.Location = New System.Drawing.Point(0, 0)
        Me.WB_EnetLive.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WB_EnetLive.Name = "WB_EnetLive"
        Me.WB_EnetLive.Size = New System.Drawing.Size(810, 424)
        Me.WB_EnetLive.TabIndex = 0
        Me.WB_EnetLive.TabStop = False
        '
        'DashWeb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(810, 424)
        Me.Controls.Add(Me.WB_EnetLive)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximumSize = New System.Drawing.Size(2000, 739)
        Me.Name = "DashWeb"
        Me.Opacity = 0.3R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Dashboard"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WB_EnetLive As System.Windows.Forms.WebBrowser
End Class

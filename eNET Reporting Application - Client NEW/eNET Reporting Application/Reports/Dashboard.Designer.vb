<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dashboard))
        Me.P_Dash = New System.Windows.Forms.Panel()
        Me.P_Headers = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'P_Dash
        '
        Me.P_Dash.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.P_Dash.AutoScroll = True
        Me.P_Dash.Location = New System.Drawing.Point(12, 68)
        Me.P_Dash.Name = "P_Dash"
        Me.P_Dash.Size = New System.Drawing.Size(967, 625)
        Me.P_Dash.TabIndex = 254
        '
        'P_Headers
        '
        Me.P_Headers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.P_Headers.AutoScroll = True
        Me.P_Headers.Location = New System.Drawing.Point(12, 12)
        Me.P_Headers.Name = "P_Headers"
        Me.P_Headers.Size = New System.Drawing.Size(967, 50)
        Me.P_Headers.TabIndex = 255
        '
        'Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(991, 705)
        Me.Controls.Add(Me.P_Headers)
        Me.Controls.Add(Me.P_Dash)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximumSize = New System.Drawing.Size(2000, 739)
        Me.Name = "Dashboard"
        Me.Opacity = 0.3R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Dashboard"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents P_Dash As System.Windows.Forms.Panel
    Friend WithEvents P_Headers As System.Windows.Forms.Panel
End Class

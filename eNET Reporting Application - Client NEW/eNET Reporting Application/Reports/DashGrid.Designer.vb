<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DashGrid
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DashGrid))
        Me.DGV_LiveStatus = New System.Windows.Forms.DataGridView()
        CType(Me.DGV_LiveStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_LiveStatus
        '
        Me.DGV_LiveStatus.AllowUserToAddRows = False
        Me.DGV_LiveStatus.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DGV_LiveStatus.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGV_LiveStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_LiveStatus.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DGV_LiveStatus.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_LiveStatus.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DGV_LiveStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_LiveStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_LiveStatus.Location = New System.Drawing.Point(0, 0)
        Me.DGV_LiveStatus.Name = "DGV_LiveStatus"
        Me.DGV_LiveStatus.ReadOnly = True
        Me.DGV_LiveStatus.RowHeadersVisible = False
        Me.DGV_LiveStatus.Size = New System.Drawing.Size(991, 705)
        Me.DGV_LiveStatus.TabIndex = 0
        '
        'DashGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(991, 705)
        Me.Controls.Add(Me.DGV_LiveStatus)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximumSize = New System.Drawing.Size(2000, 739)
        Me.Name = "DashGrid"
        Me.Opacity = 0.3R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Dashboard"
        CType(Me.DGV_LiveStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGV_LiveStatus As System.Windows.Forms.DataGridView
End Class

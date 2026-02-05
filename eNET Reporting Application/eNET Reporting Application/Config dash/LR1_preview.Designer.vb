<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LR1_preview
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
        Me.PB_prev = New System.Windows.Forms.PictureBox()
        Me.PB_wait = New System.Windows.Forms.PictureBox()
        Me.BG_capture = New System.ComponentModel.BackgroundWorker()
        Me.LBL_stat = New System.Windows.Forms.Label()
        CType(Me.PB_prev, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_wait, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PB_prev
        '
        Me.PB_prev.BackColor = System.Drawing.Color.White
        Me.PB_prev.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PB_prev.Location = New System.Drawing.Point(0, 0)
        Me.PB_prev.Name = "PB_prev"
        Me.PB_prev.Size = New System.Drawing.Size(568, 426)
        Me.PB_prev.TabIndex = 0
        Me.PB_prev.TabStop = False
        '
        'PB_wait
        '
        Me.PB_wait.Image = Global.CSI_Reporting_Application.My.Resources.Resources.processing
        Me.PB_wait.Location = New System.Drawing.Point(180, 119)
        Me.PB_wait.Name = "PB_wait"
        Me.PB_wait.Size = New System.Drawing.Size(200, 200)
        Me.PB_wait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_wait.TabIndex = 1
        Me.PB_wait.TabStop = False
        '
        'BG_capture
        '
        Me.BG_capture.WorkerReportsProgress = True
        Me.BG_capture.WorkerSupportsCancellation = True
        '
        'LBL_stat
        '
        Me.LBL_stat.AutoSize = True
        Me.LBL_stat.BackColor = System.Drawing.Color.White
        Me.LBL_stat.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_stat.Location = New System.Drawing.Point(260, 9)
        Me.LBL_stat.Name = "LBL_stat"
        Me.LBL_stat.Size = New System.Drawing.Size(0, 20)
        Me.LBL_stat.TabIndex = 2
        '
        'LR1_preview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 426)
        Me.Controls.Add(Me.LBL_stat)
        Me.Controls.Add(Me.PB_wait)
        Me.Controls.Add(Me.PB_prev)
        Me.Name = "LR1_preview"
        Me.Text = "LR1 preview"
        CType(Me.PB_prev, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_wait, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PB_prev As PictureBox
    Friend WithEvents PB_wait As PictureBox
    Friend WithEvents BG_capture As System.ComponentModel.BackgroundWorker
    Friend WithEvents LBL_stat As Label
End Class

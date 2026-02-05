<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ActivatedMaxTime
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BT_activate = New System.Windows.Forms.Button()
        Me.PB_Lock = New System.Windows.Forms.PictureBox()
        CType(Me.PB_Lock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(341, 40)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "The max cycle time has been disabled for this part" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "in the eNET setting." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BT_activate
        '
        Me.BT_activate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BT_activate.FlatAppearance.BorderSize = 0
        Me.BT_activate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BT_activate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.BT_activate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BT_activate.Location = New System.Drawing.Point(60, 189)
        Me.BT_activate.Name = "BT_activate"
        Me.BT_activate.Size = New System.Drawing.Size(247, 33)
        Me.BT_activate.TabIndex = 3
        Me.BT_activate.Text = "Activate the max cycle time"
        Me.BT_activate.UseVisualStyleBackColor = True
        '
        'PB_Lock
        '
        Me.PB_Lock.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.PB_Lock.Image = Global.CSI_Reporting_Application.My.Resources.Resources.alert_animated
        Me.PB_Lock.Location = New System.Drawing.Point(138, 12)
        Me.PB_Lock.Name = "PB_Lock"
        Me.PB_Lock.Size = New System.Drawing.Size(85, 71)
        Me.PB_Lock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_Lock.TabIndex = 0
        Me.PB_Lock.TabStop = False
        '
        'ActivatedMaxTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(367, 234)
        Me.Controls.Add(Me.BT_activate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PB_Lock)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "ActivatedMaxTime"
        Me.ShowIcon = False
        Me.Text = "Max cycle time not activated"
        Me.TopMost = True
        CType(Me.PB_Lock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents BT_activate As Button
    Friend WithEvents PB_Lock As PictureBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class importDB
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.OFD_ = New System.Windows.Forms.OpenFileDialog()
        Me.BTN_import = New System.Windows.Forms.Button()
        Me.LB_selected = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LBL_ImpRes = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(85, 124)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Browse"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'OFD_
        '
        Me.OFD_.Filter = "Sql Files (*.sql)|*.sql"
        Me.OFD_.Multiselect = True
        Me.OFD_.Title = "Select your sql files"
        '
        'BTN_import
        '
        Me.BTN_import.Location = New System.Drawing.Point(85, 290)
        Me.BTN_import.Name = "BTN_import"
        Me.BTN_import.Size = New System.Drawing.Size(155, 28)
        Me.BTN_import.TabIndex = 2
        Me.BTN_import.Text = "Confirm and Import"
        Me.BTN_import.UseVisualStyleBackColor = True
        '
        'LB_selected
        '
        Me.LB_selected.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_selected.FormattingEnabled = True
        Me.LB_selected.ItemHeight = 16
        Me.LB_selected.Location = New System.Drawing.Point(177, 180)
        Me.LB_selected.Name = "LB_selected"
        Me.LB_selected.Size = New System.Drawing.Size(178, 84)
        Me.LB_selected.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(54, 127)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "1 - "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(54, 180)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(121, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "2 - Selected files :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(54, 296)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "3 - "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(95, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(260, 17)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Import settings from a previous CSIFlex :"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.MergeJoin_64x64
        Me.PictureBox1.Location = New System.Drawing.Point(19, 23)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(70, 63)
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'LBL_ImpRes
        '
        Me.LBL_ImpRes.AutoSize = True
        Me.LBL_ImpRes.Location = New System.Drawing.Point(246, 296)
        Me.LBL_ImpRes.Name = "LBL_ImpRes"
        Me.LBL_ImpRes.Size = New System.Drawing.Size(0, 17)
        Me.LBL_ImpRes.TabIndex = 9
        '
        'importDB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(389, 358)
        Me.Controls.Add(Me.LBL_ImpRes)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LB_selected)
        Me.Controls.Add(Me.BTN_import)
        Me.Controls.Add(Me.Button1)
        Me.Name = "importDB"
        Me.Text = "Import Settings"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents OFD_ As OpenFileDialog
    Friend WithEvents BTN_import As Button
    Friend WithEvents LB_selected As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents LBL_ImpRes As Label
End Class

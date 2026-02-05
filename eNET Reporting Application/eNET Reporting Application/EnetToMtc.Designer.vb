<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EnetToMtc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EnetToMtc))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_Address = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.BTN_Ok = New System.Windows.Forms.Button()
        Me.CBX_EnetGroup = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CBX_EnetGroupNum = New System.Windows.Forms.ComboBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(54, 133)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(195, 21)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Specify the agent IP adress"
        '
        'TB_Address
        '
        Me.TB_Address.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_Address.Location = New System.Drawing.Point(22, 157)
        Me.TB_Address.Name = "TB_Address"
        Me.TB_Address.Size = New System.Drawing.Size(260, 29)
        Me.TB_Address.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.CSI_Reporting_Application.My.Resources.Resources.MTConnectLogo
        Me.PictureBox1.Location = New System.Drawing.Point(2, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(500, 106)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'BTN_Ok
        '
        Me.BTN_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Ok.Location = New System.Drawing.Point(105, 192)
        Me.BTN_Ok.Name = "BTN_Ok"
        Me.BTN_Ok.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Ok.TabIndex = 3
        Me.BTN_Ok.Text = "OK"
        Me.BTN_Ok.UseVisualStyleBackColor = True
        '
        'CBX_EnetGroup
        '
        Me.CBX_EnetGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBX_EnetGroup.FormattingEnabled = True
        Me.CBX_EnetGroup.Location = New System.Drawing.Point(56, 34)
        Me.CBX_EnetGroup.Name = "CBX_EnetGroup"
        Me.CBX_EnetGroup.Size = New System.Drawing.Size(121, 21)
        Me.CBX_EnetGroup.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.CBX_EnetGroupNum)
        Me.GroupBox1.Controls.Add(Me.CBX_EnetGroup)
        Me.GroupBox1.Location = New System.Drawing.Point(299, 114)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(193, 110)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Enet eHUB config"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(113, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Corresponding number"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Corresponding group"
        '
        'CBX_EnetGroupNum
        '
        Me.CBX_EnetGroupNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBX_EnetGroupNum.FormattingEnabled = True
        Me.CBX_EnetGroupNum.Location = New System.Drawing.Point(56, 78)
        Me.CBX_EnetGroupNum.Name = "CBX_EnetGroupNum"
        Me.CBX_EnetGroupNum.Size = New System.Drawing.Size(121, 21)
        Me.CBX_EnetGroupNum.TabIndex = 5
        '
        'EnetToMtc
        '
        Me.AcceptButton = Me.BTN_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(504, 236)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BTN_Ok)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TB_Address)
        Me.Controls.Add(Me.Label1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EnetToMtc"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "MTConnect Agent address"
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TB_Address As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents BTN_Ok As System.Windows.Forms.Button
    Friend WithEvents CBX_EnetGroup As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CBX_EnetGroupNum As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class

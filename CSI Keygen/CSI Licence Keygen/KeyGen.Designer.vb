<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KeyGen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KeyGen))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.OpenFileDialog_KeyGen = New System.Windows.Forms.OpenFileDialog()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LicenseFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OfflineInformationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EncryptedStringToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CB_Version = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DTP_ExpirationDate = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TB_Key = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GB_LicenseInfos = New System.Windows.Forms.GroupBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.GB_LicenseInfos.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 27)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(265, 69)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(294, 24)
        Me.MenuStrip1.TabIndex = 17
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LicenseFileToolStripMenuItem, Me.OfflineInformationToolStripMenuItem, Me.EncryptedStringToolStripMenuItem})
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'LicenseFileToolStripMenuItem
        '
        Me.LicenseFileToolStripMenuItem.Name = "LicenseFileToolStripMenuItem"
        Me.LicenseFileToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.LicenseFileToolStripMenuItem.Text = "License file"
        '
        'OfflineInformationToolStripMenuItem
        '
        Me.OfflineInformationToolStripMenuItem.Name = "OfflineInformationToolStripMenuItem"
        Me.OfflineInformationToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.OfflineInformationToolStripMenuItem.Text = "Offline information"
        '
        'EncryptedStringToolStripMenuItem
        '
        Me.EncryptedStringToolStripMenuItem.Name = "EncryptedStringToolStripMenuItem"
        Me.EncryptedStringToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.EncryptedStringToolStripMenuItem.Text = "Encrypted string"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Enabled = False
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'CB_Version
        '
        Me.CB_Version.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_Version.FormattingEnabled = True
        Me.CB_Version.Items.AddRange(New Object() {"0-Server", "1-Lite", "2-Standard", "3-ClientServer"})
        Me.CB_Version.Location = New System.Drawing.Point(16, 45)
        Me.CB_Version.Name = "CB_Version"
        Me.CB_Version.Size = New System.Drawing.Size(237, 21)
        Me.CB_Version.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Version : "
        '
        'DTP_ExpirationDate
        '
        Me.DTP_ExpirationDate.Location = New System.Drawing.Point(16, 100)
        Me.DTP_ExpirationDate.Name = "DTP_ExpirationDate"
        Me.DTP_ExpirationDate.Size = New System.Drawing.Size(237, 20)
        Me.DTP_ExpirationDate.TabIndex = 26
        '
        'Label9
        '
        Me.Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(16, 79)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 13)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Expiration :"
        '
        'TB_Key
        '
        Me.TB_Key.Location = New System.Drawing.Point(16, 155)
        Me.TB_Key.Name = "TB_Key"
        Me.TB_Key.ReadOnly = True
        Me.TB_Key.Size = New System.Drawing.Size(237, 20)
        Me.TB_Key.TabIndex = 27
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(16, 134)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 13)
        Me.Label10.TabIndex = 25
        Me.Label10.Text = "Key :"
        '
        'GB_LicenseInfos
        '
        Me.GB_LicenseInfos.Controls.Add(Me.CB_Version)
        Me.GB_LicenseInfos.Controls.Add(Me.Label10)
        Me.GB_LicenseInfos.Controls.Add(Me.Label9)
        Me.GB_LicenseInfos.Controls.Add(Me.DTP_ExpirationDate)
        Me.GB_LicenseInfos.Controls.Add(Me.Label8)
        Me.GB_LicenseInfos.Controls.Add(Me.TB_Key)
        Me.GB_LicenseInfos.Enabled = False
        Me.GB_LicenseInfos.Location = New System.Drawing.Point(12, 113)
        Me.GB_LicenseInfos.Name = "GB_LicenseInfos"
        Me.GB_LicenseInfos.Size = New System.Drawing.Size(265, 197)
        Me.GB_LicenseInfos.TabIndex = 28
        Me.GB_LicenseInfos.TabStop = False
        Me.GB_LicenseInfos.Text = "License key informations"
        '
        'KeyGen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(294, 321)
        Me.Controls.Add(Me.GB_LicenseInfos)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "KeyGen"
        Me.Text = "CSIFLEX KeyGen"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GB_LicenseInfos.ResumeLayout(False)
        Me.GB_LicenseInfos.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents OpenFileDialog_KeyGen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LicenseFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OfflineInformationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CB_Version As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DTP_ExpirationDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TB_Key As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GB_LicenseInfos As System.Windows.Forms.GroupBox
    Friend WithEvents EncryptedStringToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class

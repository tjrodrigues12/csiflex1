<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Edit_logfile
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
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.CB_CYCLEON = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.BT_CANCEL = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BT_BROWSE = New System.Windows.Forms.Button()
        Me.TB_PATH = New System.Windows.Forms.TextBox()
        Me.BT_Preview = New System.Windows.Forms.Button()
        Me.TB_OTHER = New System.Windows.Forms.TextBox()
        Me.CHK_OTHER = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RB_FIXED = New System.Windows.Forms.RadioButton()
        Me.CHK_MERGEDEL = New System.Windows.Forms.CheckBox()
        Me.CHK_SPACE = New System.Windows.Forms.CheckBox()
        Me.RB_SEPARATED = New System.Windows.Forms.RadioButton()
        Me.CHK_COMA = New System.Windows.Forms.CheckBox()
        Me.CHK_TAB = New System.Windows.Forms.CheckBox()
        Me.CHK_SEMICOL = New System.Windows.Forms.CheckBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.BT_SCAN_CYCLEON = New System.Windows.Forms.Button()
        Me.BT_VALIDATE = New System.Windows.Forms.Button()
        Me.CB_CYCLEOFF = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BT_SCAN_CYCLEOFF = New System.Windows.Forms.Button()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView2
        '
        Me.DataGridView2.BackgroundColor = System.Drawing.SystemColors.WindowFrame
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(15, 348)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowTemplate.Height = 24
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView2.Size = New System.Drawing.Size(985, 262)
        Me.DataGridView2.TabIndex = 27
        '
        'CB_CYCLEON
        '
        Me.CB_CYCLEON.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_CYCLEON.FormattingEnabled = True
        Me.CB_CYCLEON.Location = New System.Drawing.Point(145, 727)
        Me.CB_CYCLEON.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CB_CYCLEON.Name = "CB_CYCLEON"
        Me.CB_CYCLEON.Size = New System.Drawing.Size(247, 36)
        Me.CB_CYCLEON.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label1.Location = New System.Drawing.Point(9, 731)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 28)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "CYCLE ON is"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.ColumnHeadersVisible = False
        Me.DataGridView1.Location = New System.Drawing.Point(15, 615)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(985, 100)
        Me.DataGridView1.TabIndex = 23
        '
        'BT_CANCEL
        '
        Me.BT_CANCEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BT_CANCEL.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BT_CANCEL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BT_CANCEL.Location = New System.Drawing.Point(509, 789)
        Me.BT_CANCEL.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BT_CANCEL.Name = "BT_CANCEL"
        Me.BT_CANCEL.Size = New System.Drawing.Size(119, 38)
        Me.BT_CANCEL.TabIndex = 22
        Me.BT_CANCEL.Text = "Cancel"
        Me.BT_CANCEL.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.BT_BROWSE)
        Me.GroupBox2.Controls.Add(Me.TB_PATH)
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.GroupBox2.Location = New System.Drawing.Point(15, 14)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(985, 89)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "File"
        '
        'BT_BROWSE
        '
        Me.BT_BROWSE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BT_BROWSE.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BT_BROWSE.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.BT_BROWSE.Location = New System.Drawing.Point(841, 41)
        Me.BT_BROWSE.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BT_BROWSE.Name = "BT_BROWSE"
        Me.BT_BROWSE.Size = New System.Drawing.Size(128, 36)
        Me.BT_BROWSE.TabIndex = 0
        Me.BT_BROWSE.Text = "Browse"
        Me.BT_BROWSE.UseVisualStyleBackColor = True
        '
        'TB_PATH
        '
        Me.TB_PATH.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_PATH.Location = New System.Drawing.Point(20, 41)
        Me.TB_PATH.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_PATH.Name = "TB_PATH"
        Me.TB_PATH.Size = New System.Drawing.Size(815, 34)
        Me.TB_PATH.TabIndex = 1
        '
        'BT_Preview
        '
        Me.BT_Preview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BT_Preview.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BT_Preview.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BT_Preview.Location = New System.Drawing.Point(432, 306)
        Me.BT_Preview.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BT_Preview.Name = "BT_Preview"
        Me.BT_Preview.Size = New System.Drawing.Size(123, 38)
        Me.BT_Preview.TabIndex = 21
        Me.BT_Preview.Text = "Preview"
        Me.BT_Preview.UseVisualStyleBackColor = True
        '
        'TB_OTHER
        '
        Me.TB_OTHER.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_OTHER.Location = New System.Drawing.Point(511, 129)
        Me.TB_OTHER.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_OTHER.Name = "TB_OTHER"
        Me.TB_OTHER.Size = New System.Drawing.Size(133, 34)
        Me.TB_OTHER.TabIndex = 11
        '
        'CHK_OTHER
        '
        Me.CHK_OTHER.AutoSize = True
        Me.CHK_OTHER.Checked = True
        Me.CHK_OTHER.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CHK_OTHER.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHK_OTHER.Location = New System.Drawing.Point(417, 129)
        Me.CHK_OTHER.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CHK_OTHER.Name = "CHK_OTHER"
        Me.CHK_OTHER.Size = New System.Drawing.Size(84, 32)
        Me.CHK_OTHER.TabIndex = 9
        Me.CHK_OTHER.Text = "Other"
        Me.CHK_OTHER.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.TB_OTHER)
        Me.GroupBox1.Controls.Add(Me.RB_FIXED)
        Me.GroupBox1.Controls.Add(Me.CHK_OTHER)
        Me.GroupBox1.Controls.Add(Me.CHK_MERGEDEL)
        Me.GroupBox1.Controls.Add(Me.CHK_SPACE)
        Me.GroupBox1.Controls.Add(Me.RB_SEPARATED)
        Me.GroupBox1.Controls.Add(Me.CHK_COMA)
        Me.GroupBox1.Controls.Add(Me.CHK_TAB)
        Me.GroupBox1.Controls.Add(Me.CHK_SEMICOL)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.GroupBox1.Location = New System.Drawing.Point(15, 107)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(985, 194)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Separator options"
        '
        'RB_FIXED
        '
        Me.RB_FIXED.AutoSize = True
        Me.RB_FIXED.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RB_FIXED.Location = New System.Drawing.Point(20, 41)
        Me.RB_FIXED.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RB_FIXED.Name = "RB_FIXED"
        Me.RB_FIXED.Size = New System.Drawing.Size(133, 32)
        Me.RB_FIXED.TabIndex = 3
        Me.RB_FIXED.Text = "Fixed width"
        Me.RB_FIXED.UseVisualStyleBackColor = True
        '
        'CHK_MERGEDEL
        '
        Me.CHK_MERGEDEL.AutoSize = True
        Me.CHK_MERGEDEL.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHK_MERGEDEL.Location = New System.Drawing.Point(417, 101)
        Me.CHK_MERGEDEL.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CHK_MERGEDEL.Name = "CHK_MERGEDEL"
        Me.CHK_MERGEDEL.Size = New System.Drawing.Size(182, 32)
        Me.CHK_MERGEDEL.TabIndex = 10
        Me.CHK_MERGEDEL.Text = "Merge delimiters"
        Me.CHK_MERGEDEL.UseVisualStyleBackColor = True
        '
        'CHK_SPACE
        '
        Me.CHK_SPACE.AutoSize = True
        Me.CHK_SPACE.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHK_SPACE.Location = New System.Drawing.Point(689, 129)
        Me.CHK_SPACE.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CHK_SPACE.Name = "CHK_SPACE"
        Me.CHK_SPACE.Size = New System.Drawing.Size(86, 32)
        Me.CHK_SPACE.TabIndex = 8
        Me.CHK_SPACE.Text = "Space"
        Me.CHK_SPACE.UseVisualStyleBackColor = True
        '
        'RB_SEPARATED
        '
        Me.RB_SEPARATED.AutoSize = True
        Me.RB_SEPARATED.Checked = True
        Me.RB_SEPARATED.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RB_SEPARATED.Location = New System.Drawing.Point(233, 41)
        Me.RB_SEPARATED.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RB_SEPARATED.Name = "RB_SEPARATED"
        Me.RB_SEPARATED.Size = New System.Drawing.Size(158, 32)
        Me.RB_SEPARATED.TabIndex = 4
        Me.RB_SEPARATED.TabStop = True
        Me.RB_SEPARATED.Text = "Separated by :"
        Me.RB_SEPARATED.UseVisualStyleBackColor = True
        '
        'CHK_COMA
        '
        Me.CHK_COMA.AutoSize = True
        Me.CHK_COMA.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHK_COMA.Location = New System.Drawing.Point(253, 129)
        Me.CHK_COMA.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CHK_COMA.Name = "CHK_COMA"
        Me.CHK_COMA.Size = New System.Drawing.Size(85, 32)
        Me.CHK_COMA.TabIndex = 6
        Me.CHK_COMA.Text = "Coma"
        Me.CHK_COMA.UseVisualStyleBackColor = True
        '
        'CHK_TAB
        '
        Me.CHK_TAB.AutoSize = True
        Me.CHK_TAB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHK_TAB.Location = New System.Drawing.Point(689, 101)
        Me.CHK_TAB.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CHK_TAB.Name = "CHK_TAB"
        Me.CHK_TAB.Size = New System.Drawing.Size(66, 32)
        Me.CHK_TAB.TabIndex = 5
        Me.CHK_TAB.Text = "Tab"
        Me.CHK_TAB.UseVisualStyleBackColor = True
        '
        'CHK_SEMICOL
        '
        Me.CHK_SEMICOL.AutoSize = True
        Me.CHK_SEMICOL.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHK_SEMICOL.Location = New System.Drawing.Point(253, 101)
        Me.CHK_SEMICOL.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CHK_SEMICOL.Name = "CHK_SEMICOL"
        Me.CHK_SEMICOL.Size = New System.Drawing.Size(126, 32)
        Me.CHK_SEMICOL.TabIndex = 7
        Me.CHK_SEMICOL.Text = "Semicolon"
        Me.CHK_SEMICOL.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'BT_SCAN_CYCLEON
        '
        Me.BT_SCAN_CYCLEON.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BT_SCAN_CYCLEON.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BT_SCAN_CYCLEON.Location = New System.Drawing.Point(399, 725)
        Me.BT_SCAN_CYCLEON.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BT_SCAN_CYCLEON.Name = "BT_SCAN_CYCLEON"
        Me.BT_SCAN_CYCLEON.Size = New System.Drawing.Size(95, 38)
        Me.BT_SCAN_CYCLEON.TabIndex = 26
        Me.BT_SCAN_CYCLEON.Text = "Scan"
        Me.BT_SCAN_CYCLEON.UseVisualStyleBackColor = True
        '
        'BT_VALIDATE
        '
        Me.BT_VALIDATE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BT_VALIDATE.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BT_VALIDATE.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BT_VALIDATE.Location = New System.Drawing.Point(389, 789)
        Me.BT_VALIDATE.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BT_VALIDATE.Name = "BT_VALIDATE"
        Me.BT_VALIDATE.Size = New System.Drawing.Size(115, 38)
        Me.BT_VALIDATE.TabIndex = 28
        Me.BT_VALIDATE.Text = "Validate"
        Me.BT_VALIDATE.UseVisualStyleBackColor = True
        '
        'CB_CYCLEOFF
        '
        Me.CB_CYCLEOFF.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_CYCLEOFF.FormattingEnabled = True
        Me.CB_CYCLEOFF.Location = New System.Drawing.Point(640, 727)
        Me.CB_CYCLEOFF.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CB_CYCLEOFF.Name = "CB_CYCLEOFF"
        Me.CB_CYCLEOFF.Size = New System.Drawing.Size(257, 36)
        Me.CB_CYCLEOFF.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label2.Location = New System.Drawing.Point(499, 731)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 28)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "CYCLE OFF is"
        '
        'BT_SCAN_CYCLEOFF
        '
        Me.BT_SCAN_CYCLEOFF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BT_SCAN_CYCLEOFF.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BT_SCAN_CYCLEOFF.Location = New System.Drawing.Point(904, 725)
        Me.BT_SCAN_CYCLEOFF.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BT_SCAN_CYCLEOFF.Name = "BT_SCAN_CYCLEOFF"
        Me.BT_SCAN_CYCLEOFF.Size = New System.Drawing.Size(95, 38)
        Me.BT_SCAN_CYCLEOFF.TabIndex = 31
        Me.BT_SCAN_CYCLEOFF.Text = "Scan"
        Me.BT_SCAN_CYCLEOFF.UseVisualStyleBackColor = True
        '
        'Edit_logfile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.CSI_Reporting_Application.My.Resources.Resources.bgi
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1027, 841)
        Me.Controls.Add(Me.CB_CYCLEOFF)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BT_SCAN_CYCLEOFF)
        Me.Controls.Add(Me.BT_VALIDATE)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.CB_CYCLEON)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.BT_CANCEL)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.BT_Preview)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BT_SCAN_CYCLEON)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Edit_logfile"
        Me.Text = "CSIFLEX Log File Monitoring Setup"
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents CB_CYCLEON As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents BT_CANCEL As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BT_BROWSE As System.Windows.Forms.Button
    Friend WithEvents TB_PATH As System.Windows.Forms.TextBox
    Friend WithEvents BT_Preview As System.Windows.Forms.Button
    Friend WithEvents TB_OTHER As System.Windows.Forms.TextBox
    Friend WithEvents CHK_OTHER As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CHK_MERGEDEL As System.Windows.Forms.CheckBox
    Friend WithEvents CHK_SPACE As System.Windows.Forms.CheckBox
    Friend WithEvents CHK_COMA As System.Windows.Forms.CheckBox
    Friend WithEvents CHK_TAB As System.Windows.Forms.CheckBox
    Friend WithEvents CHK_SEMICOL As System.Windows.Forms.CheckBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BT_SCAN_CYCLEON As System.Windows.Forms.Button
    Friend WithEvents RB_FIXED As System.Windows.Forms.RadioButton
    Friend WithEvents RB_SEPARATED As System.Windows.Forms.RadioButton
    Friend WithEvents BT_VALIDATE As System.Windows.Forms.Button
    Friend WithEvents CB_CYCLEOFF As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BT_SCAN_CYCLEOFF As System.Windows.Forms.Button
End Class

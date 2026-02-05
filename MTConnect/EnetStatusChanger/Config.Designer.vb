<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Config
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Config))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TB_Pwd = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TB_IP = New System.Windows.Forms.TextBox()
        Me.TB_Path = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BTN_EnetPathBrowse = New System.Windows.Forms.Button()
        Me.BTN_Save = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CB_MachList = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(235, 37)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(193, 17)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Enter the Enet FTP password"
        '
        'TB_Pwd
        '
        Me.TB_Pwd.Location = New System.Drawing.Point(239, 57)
        Me.TB_Pwd.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_Pwd.Name = "TB_Pwd"
        Me.TB_Pwd.Size = New System.Drawing.Size(189, 22)
        Me.TB_Pwd.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(32, 37)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(145, 17)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Enter the Enet FTP IP"
        '
        'TB_IP
        '
        Me.TB_IP.Location = New System.Drawing.Point(36, 57)
        Me.TB_IP.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_IP.Name = "TB_IP"
        Me.TB_IP.Size = New System.Drawing.Size(175, 22)
        Me.TB_IP.TabIndex = 12
        '
        'TB_Path
        '
        Me.TB_Path.Location = New System.Drawing.Point(36, 132)
        Me.TB_Path.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_Path.Name = "TB_Path"
        Me.TB_Path.Size = New System.Drawing.Size(284, 22)
        Me.TB_Path.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 108)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(175, 17)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Select the enet folder path"
        '
        'BTN_EnetPathBrowse
        '
        Me.BTN_EnetPathBrowse.Location = New System.Drawing.Point(329, 128)
        Me.BTN_EnetPathBrowse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_EnetPathBrowse.Name = "BTN_EnetPathBrowse"
        Me.BTN_EnetPathBrowse.Size = New System.Drawing.Size(100, 28)
        Me.BTN_EnetPathBrowse.TabIndex = 18
        Me.BTN_EnetPathBrowse.Text = "Browse"
        Me.BTN_EnetPathBrowse.UseVisualStyleBackColor = True
        '
        'BTN_Save
        '
        Me.BTN_Save.Enabled = False
        Me.BTN_Save.Location = New System.Drawing.Point(309, 199)
        Me.BTN_Save.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_Save.Name = "BTN_Save"
        Me.BTN_Save.Size = New System.Drawing.Size(120, 28)
        Me.BTN_Save.TabIndex = 19
        Me.BTN_Save.Text = "Save"
        Me.BTN_Save.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 182)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(163, 17)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Restrict to this machine :"
        '
        'CB_MachList
        '
        Me.CB_MachList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_MachList.FormattingEnabled = True
        Me.CB_MachList.Location = New System.Drawing.Point(36, 202)
        Me.CB_MachList.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CB_MachList.Name = "CB_MachList"
        Me.CB_MachList.Size = New System.Drawing.Size(244, 24)
        Me.CB_MachList.TabIndex = 20
        '
        'Config
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(448, 242)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CB_MachList)
        Me.Controls.Add(Me.BTN_Save)
        Me.Controls.Add(Me.BTN_EnetPathBrowse)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TB_Path)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TB_Pwd)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TB_IP)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "Config"
        Me.Text = "Enet Configuration"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TB_Pwd As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TB_IP As System.Windows.Forms.TextBox
    Friend WithEvents TB_Path As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BTN_EnetPathBrowse As System.Windows.Forms.Button
    Friend WithEvents BTN_Save As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CB_MachList As System.Windows.Forms.ComboBox
End Class

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
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(176, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(146, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Enter the Enet FTP password"
        '
        'TB_Pwd
        '
        Me.TB_Pwd.Location = New System.Drawing.Point(179, 46)
        Me.TB_Pwd.Name = "TB_Pwd"
        Me.TB_Pwd.Size = New System.Drawing.Size(143, 20)
        Me.TB_Pwd.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Enter the Enet FTP IP"
        '
        'TB_IP
        '
        Me.TB_IP.Location = New System.Drawing.Point(27, 46)
        Me.TB_IP.Name = "TB_IP"
        Me.TB_IP.Size = New System.Drawing.Size(132, 20)
        Me.TB_IP.TabIndex = 12
        '
        'TB_Path
        '
        Me.TB_Path.Location = New System.Drawing.Point(27, 107)
        Me.TB_Path.Name = "TB_Path"
        Me.TB_Path.Size = New System.Drawing.Size(214, 20)
        Me.TB_Path.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Select the enet folder path"
        '
        'BTN_EnetPathBrowse
        '
        Me.BTN_EnetPathBrowse.Location = New System.Drawing.Point(247, 104)
        Me.BTN_EnetPathBrowse.Name = "BTN_EnetPathBrowse"
        Me.BTN_EnetPathBrowse.Size = New System.Drawing.Size(75, 23)
        Me.BTN_EnetPathBrowse.TabIndex = 18
        Me.BTN_EnetPathBrowse.Text = "Browse"
        Me.BTN_EnetPathBrowse.UseVisualStyleBackColor = True
        '
        'BTN_Save
        '
        Me.BTN_Save.Location = New System.Drawing.Point(232, 162)
        Me.BTN_Save.Name = "BTN_Save"
        Me.BTN_Save.Size = New System.Drawing.Size(90, 23)
        Me.BTN_Save.TabIndex = 19
        Me.BTN_Save.Text = "Save"
        Me.BTN_Save.UseVisualStyleBackColor = True
        '
        'Config
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(336, 197)
        Me.Controls.Add(Me.BTN_Save)
        Me.Controls.Add(Me.BTN_EnetPathBrowse)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TB_Path)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TB_Pwd)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TB_IP)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
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
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserCredentials
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserCredentials))
        Me.BTN_Save = New System.Windows.Forms.Button()
        Me.TB_EnetPCPwd = New System.Windows.Forms.TextBox()
        Me.LBL_EnetPCPwd = New System.Windows.Forms.Label()
        Me.TB_EnetPCName = New System.Windows.Forms.TextBox()
        Me.LBL_EnetPCName = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BTN_Save
        '
        Me.BTN_Save.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Save.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BTN_Save.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Save.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Save.Location = New System.Drawing.Point(158, 149)
        Me.BTN_Save.Margin = New System.Windows.Forms.Padding(0)
        Me.BTN_Save.Name = "BTN_Save"
        Me.BTN_Save.Size = New System.Drawing.Size(127, 42)
        Me.BTN_Save.TabIndex = 9
        Me.BTN_Save.Text = "Save"
        Me.BTN_Save.UseVisualStyleBackColor = False
        '
        'TB_EnetPCPwd
        '
        Me.TB_EnetPCPwd.Location = New System.Drawing.Point(192, 101)
        Me.TB_EnetPCPwd.Name = "TB_EnetPCPwd"
        Me.TB_EnetPCPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TB_EnetPCPwd.Size = New System.Drawing.Size(192, 22)
        Me.TB_EnetPCPwd.TabIndex = 8
        '
        'LBL_EnetPCPwd
        '
        Me.LBL_EnetPCPwd.AutoSize = True
        Me.LBL_EnetPCPwd.Location = New System.Drawing.Point(42, 101)
        Me.LBL_EnetPCPwd.Name = "LBL_EnetPCPwd"
        Me.LBL_EnetPCPwd.Size = New System.Drawing.Size(132, 17)
        Me.LBL_EnetPCPwd.TabIndex = 7
        Me.LBL_EnetPCPwd.Text = "Enet PC Password :"
        '
        'TB_EnetPCName
        '
        Me.TB_EnetPCName.Location = New System.Drawing.Point(192, 50)
        Me.TB_EnetPCName.Name = "TB_EnetPCName"
        Me.TB_EnetPCName.Size = New System.Drawing.Size(192, 22)
        Me.TB_EnetPCName.TabIndex = 6
        '
        'LBL_EnetPCName
        '
        Me.LBL_EnetPCName.AutoSize = True
        Me.LBL_EnetPCName.Location = New System.Drawing.Point(66, 50)
        Me.LBL_EnetPCName.Name = "LBL_EnetPCName"
        Me.LBL_EnetPCName.Size = New System.Drawing.Size(108, 17)
        Me.LBL_EnetPCName.TabIndex = 5
        Me.LBL_EnetPCName.Text = "Enet PC Name :"
        '
        'UserCredentials
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 222)
        Me.Controls.Add(Me.BTN_Save)
        Me.Controls.Add(Me.TB_EnetPCPwd)
        Me.Controls.Add(Me.LBL_EnetPCPwd)
        Me.Controls.Add(Me.TB_EnetPCName)
        Me.Controls.Add(Me.LBL_EnetPCName)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UserCredentials"
        Me.Text = "UserCredentials"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BTN_Save As Windows.Forms.Button
    Friend WithEvents TB_EnetPCPwd As Windows.Forms.TextBox
    Friend WithEvents LBL_EnetPCPwd As Windows.Forms.Label
    Friend WithEvents TB_EnetPCName As Windows.Forms.TextBox
    Friend WithEvents LBL_EnetPCName As Windows.Forms.Label
End Class

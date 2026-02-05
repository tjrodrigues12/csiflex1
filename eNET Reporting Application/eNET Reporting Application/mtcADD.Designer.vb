<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MtcADD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MtcADD))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_AddressIP = New System.Windows.Forms.TextBox()
        Me.PB_Connector = New System.Windows.Forms.PictureBox()
        Me.BTN_Ok = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CB_Connector = New System.Windows.Forms.ComboBox()
        Me.CB_MachineName = New System.Windows.Forms.ComboBox()
        Me.BTN_GetMachineNames = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CB_eNETMachineName = New System.Windows.Forms.ComboBox()
        Me.BTN_More = New System.Windows.Forms.Button()
        Me.BTN_Cancel = New System.Windows.Forms.Button()
        Me.LB_FieldOPENER = New System.Windows.Forms.Label()
        Me.TB_ConnectorType = New System.Windows.Forms.TextBox()
        Me.TB_MachineName = New System.Windows.Forms.TextBox()
        Me.TB_eNETMachineName = New System.Windows.Forms.TextBox()
        Me.BTN_ChangeENET = New System.Windows.Forms.Button()
        CType(Me.PB_Connector, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 21)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Machine IP"
        '
        'TB_AddressIP
        '
        Me.TB_AddressIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_AddressIP.Location = New System.Drawing.Point(16, 92)
        Me.TB_AddressIP.Name = "TB_AddressIP"
        Me.TB_AddressIP.Size = New System.Drawing.Size(328, 23)
        Me.TB_AddressIP.TabIndex = 3
        '
        'PB_Connector
        '
        Me.PB_Connector.Image = Global.CSI_Reporting_Application.My.Resources.Resources.MTConnectLogo
        Me.PB_Connector.Location = New System.Drawing.Point(218, 1)
        Me.PB_Connector.Name = "PB_Connector"
        Me.PB_Connector.Size = New System.Drawing.Size(284, 65)
        Me.PB_Connector.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PB_Connector.TabIndex = 2
        Me.PB_Connector.TabStop = False
        '
        'BTN_Ok
        '
        Me.BTN_Ok.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Ok.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Ok.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Ok.Location = New System.Drawing.Point(365, 180)
        Me.BTN_Ok.Name = "BTN_Ok"
        Me.BTN_Ok.Size = New System.Drawing.Size(127, 29)
        Me.BTN_Ok.TabIndex = 10
        Me.BTN_Ok.Text = "Apply"
        Me.BTN_Ok.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 21)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Machine name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(116, 21)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Connector type"
        '
        'CB_Connector
        '
        Me.CB_Connector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_Connector.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_Connector.FormattingEnabled = True
        Me.CB_Connector.Items.AddRange(New Object() {"MTConnect", "Focas"})
        Me.CB_Connector.Location = New System.Drawing.Point(16, 34)
        Me.CB_Connector.Name = "CB_Connector"
        Me.CB_Connector.Size = New System.Drawing.Size(186, 24)
        Me.CB_Connector.TabIndex = 1
        '
        'CB_MachineName
        '
        Me.CB_MachineName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_MachineName.Enabled = False
        Me.CB_MachineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_MachineName.FormattingEnabled = True
        Me.CB_MachineName.Items.AddRange(New Object() {"MTConnect", "Focas"})
        Me.CB_MachineName.Location = New System.Drawing.Point(16, 160)
        Me.CB_MachineName.Name = "CB_MachineName"
        Me.CB_MachineName.Size = New System.Drawing.Size(328, 24)
        Me.CB_MachineName.TabIndex = 6
        '
        'BTN_GetMachineNames
        '
        Me.BTN_GetMachineNames.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_GetMachineNames.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_GetMachineNames.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_GetMachineNames.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_GetMachineNames.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_GetMachineNames.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_GetMachineNames.Location = New System.Drawing.Point(365, 92)
        Me.BTN_GetMachineNames.Name = "BTN_GetMachineNames"
        Me.BTN_GetMachineNames.Size = New System.Drawing.Size(127, 29)
        Me.BTN_GetMachineNames.TabIndex = 4
        Me.BTN_GetMachineNames.Text = "Find Machines"
        Me.BTN_GetMachineNames.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 206)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(155, 21)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "eNET Machine Name"
        '
        'CB_eNETMachineName
        '
        Me.CB_eNETMachineName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_eNETMachineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_eNETMachineName.FormattingEnabled = True
        Me.CB_eNETMachineName.ItemHeight = 16
        Me.CB_eNETMachineName.Location = New System.Drawing.Point(16, 230)
        Me.CB_eNETMachineName.Name = "CB_eNETMachineName"
        Me.CB_eNETMachineName.Size = New System.Drawing.Size(328, 24)
        Me.CB_eNETMachineName.TabIndex = 8
        '
        'BTN_More
        '
        Me.BTN_More.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_More.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_More.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_More.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_More.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_More.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_More.Location = New System.Drawing.Point(365, 136)
        Me.BTN_More.Name = "BTN_More"
        Me.BTN_More.Size = New System.Drawing.Size(127, 29)
        Me.BTN_More.TabIndex = 9
        Me.BTN_More.Text = "More >"
        Me.BTN_More.UseVisualStyleBackColor = False
        '
        'BTN_Cancel
        '
        Me.BTN_Cancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Cancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BTN_Cancel.Location = New System.Drawing.Point(365, 230)
        Me.BTN_Cancel.Name = "BTN_Cancel"
        Me.BTN_Cancel.Size = New System.Drawing.Size(127, 29)
        Me.BTN_Cancel.TabIndex = 11
        Me.BTN_Cancel.Text = "Close"
        Me.BTN_Cancel.UseVisualStyleBackColor = False
        '
        'LB_FieldOPENER
        '
        Me.LB_FieldOPENER.AutoSize = True
        Me.LB_FieldOPENER.Location = New System.Drawing.Point(254, 127)
        Me.LB_FieldOPENER.Name = "LB_FieldOPENER"
        Me.LB_FieldOPENER.Size = New System.Drawing.Size(39, 13)
        Me.LB_FieldOPENER.TabIndex = 12
        Me.LB_FieldOPENER.Text = "Label5"
        Me.LB_FieldOPENER.Visible = False
        '
        'TB_ConnectorType
        '
        Me.TB_ConnectorType.Enabled = False
        Me.TB_ConnectorType.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_ConnectorType.Location = New System.Drawing.Point(16, 35)
        Me.TB_ConnectorType.Name = "TB_ConnectorType"
        Me.TB_ConnectorType.Size = New System.Drawing.Size(186, 23)
        Me.TB_ConnectorType.TabIndex = 13
        Me.TB_ConnectorType.Visible = False
        '
        'TB_MachineName
        '
        Me.TB_MachineName.Enabled = False
        Me.TB_MachineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_MachineName.Location = New System.Drawing.Point(16, 161)
        Me.TB_MachineName.Name = "TB_MachineName"
        Me.TB_MachineName.Size = New System.Drawing.Size(328, 23)
        Me.TB_MachineName.TabIndex = 14
        Me.TB_MachineName.Visible = False
        '
        'TB_eNETMachineName
        '
        Me.TB_eNETMachineName.Enabled = False
        Me.TB_eNETMachineName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_eNETMachineName.Location = New System.Drawing.Point(16, 230)
        Me.TB_eNETMachineName.Name = "TB_eNETMachineName"
        Me.TB_eNETMachineName.Size = New System.Drawing.Size(328, 23)
        Me.TB_eNETMachineName.TabIndex = 15
        Me.TB_eNETMachineName.Visible = False
        '
        'BTN_ChangeENET
        '
        Me.BTN_ChangeENET.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_ChangeENET.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_ChangeENET.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_ChangeENET.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_ChangeENET.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_ChangeENET.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_ChangeENET.Location = New System.Drawing.Point(365, 92)
        Me.BTN_ChangeENET.Name = "BTN_ChangeENET"
        Me.BTN_ChangeENET.Size = New System.Drawing.Size(127, 29)
        Me.BTN_ChangeENET.TabIndex = 16
        Me.BTN_ChangeENET.Text = "Change"
        Me.BTN_ChangeENET.UseVisualStyleBackColor = False
        Me.BTN_ChangeENET.Visible = False
        '
        'MtcADD
        '
        Me.AcceptButton = Me.BTN_Ok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(504, 288)
        Me.Controls.Add(Me.BTN_ChangeENET)
        Me.Controls.Add(Me.TB_eNETMachineName)
        Me.Controls.Add(Me.TB_MachineName)
        Me.Controls.Add(Me.TB_ConnectorType)
        Me.Controls.Add(Me.LB_FieldOPENER)
        Me.Controls.Add(Me.BTN_Cancel)
        Me.Controls.Add(Me.BTN_More)
        Me.Controls.Add(Me.CB_eNETMachineName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.BTN_GetMachineNames)
        Me.Controls.Add(Me.CB_MachineName)
        Me.Controls.Add(Me.CB_Connector)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BTN_Ok)
        Me.Controls.Add(Me.PB_Connector)
        Me.Controls.Add(Me.TB_AddressIP)
        Me.Controls.Add(Me.Label1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MtcADD"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "CSI Connector"
        CType(Me.PB_Connector, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TB_AddressIP As System.Windows.Forms.TextBox
    Friend WithEvents PB_Connector As System.Windows.Forms.PictureBox
    Friend WithEvents BTN_Ok As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CB_Connector As System.Windows.Forms.ComboBox
    Friend WithEvents CB_MachineName As System.Windows.Forms.ComboBox
    Friend WithEvents BTN_GetMachineNames As System.Windows.Forms.Button
    Friend WithEvents Label4 As Label
    Friend WithEvents CB_eNETMachineName As ComboBox
    Friend WithEvents BTN_More As Button
    Friend WithEvents BTN_Cancel As Button
    Friend WithEvents LB_FieldOPENER As Label
    Friend WithEvents TB_ConnectorType As TextBox
    Friend WithEvents TB_MachineName As TextBox
    Friend WithEvents TB_eNETMachineName As TextBox
    Friend WithEvents BTN_ChangeENET As Button
End Class

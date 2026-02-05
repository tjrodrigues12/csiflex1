<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MonitoringUnitsControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvMonitoringUnits = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MachineId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MachineName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MacAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IPAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvMonitoringUnits, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(30, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(291, 33)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Monitoring Units"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgvMonitoringUnits
        '
        Me.dgvMonitoringUnits.AllowUserToAddRows = False
        Me.dgvMonitoringUnits.AllowUserToDeleteRows = False
        Me.dgvMonitoringUnits.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMonitoringUnits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvMonitoringUnits.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvMonitoringUnits.ColumnHeadersHeight = 36
        Me.dgvMonitoringUnits.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.MachineId, Me.Name, Me.MachineName, Me.MacAddress, Me.IPAddress})
        Me.dgvMonitoringUnits.Location = New System.Drawing.Point(35, 70)
        Me.dgvMonitoringUnits.Name = "dgvMonitoringUnits"
        Me.dgvMonitoringUnits.Size = New System.Drawing.Size(876, 199)
        Me.dgvMonitoringUnits.TabIndex = 1
        '
        'Id
        '
        Me.Id.DataPropertyName = "Id"
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.Visible = False
        '
        'MachineId
        '
        Me.MachineId.DataPropertyName = "MachineId"
        Me.MachineId.HeaderText = "Machine Id"
        Me.MachineId.Name = "MachineId"
        Me.MachineId.Visible = False
        '
        'Name
        '
        Me.Name.DataPropertyName = "Name"
        Me.Name.HeaderText = "Name"
        Me.Name.Name = "Name"
        Me.Name.ReadOnly = True
        '
        'MachineName
        '
        Me.MachineName.DataPropertyName = "MachineName"
        Me.MachineName.HeaderText = "Machine Name"
        Me.MachineName.Name = "MachineName"
        Me.MachineName.ReadOnly = True
        '
        'MacAddress
        '
        Me.MacAddress.DataPropertyName = "MacAddress"
        Me.MacAddress.HeaderText = "MAC Address"
        Me.MacAddress.Name = "MacAddress"
        Me.MacAddress.ReadOnly = True
        '
        'IPAddress
        '
        Me.IPAddress.DataPropertyName = "IpAddress"
        Me.IPAddress.HeaderText = "IP Address"
        Me.IPAddress.Name = "IPAddress"
        Me.IPAddress.ReadOnly = True
        '
        'MonitoringUnitsControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dgvMonitoringUnits)
        Me.Controls.Add(Me.Label1)
        Me.Name = "MonitoringUnitsControl"
        Me.Size = New System.Drawing.Size(948, 444)
        CType(Me.dgvMonitoringUnits, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents dgvMonitoringUnits As DataGridView
    Friend WithEvents Id As DataGridViewTextBoxColumn
    Friend WithEvents MachineId As DataGridViewTextBoxColumn
    Friend WithEvents Name As DataGridViewTextBoxColumn
    Friend WithEvents MachineName As DataGridViewTextBoxColumn
    Friend WithEvents MacAddress As DataGridViewTextBoxColumn
    Friend WithEvents IPAddress As DataGridViewTextBoxColumn
End Class

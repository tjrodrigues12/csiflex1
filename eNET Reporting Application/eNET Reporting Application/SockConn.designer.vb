<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SockConn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SockConn))
        Me.btn_scan = New System.Windows.Forms.Button()
        Me.dgv_1 = New System.Windows.Forms.DataGridView()
        Me.IpAdd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgv_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_scan
        '
        Me.btn_scan.Location = New System.Drawing.Point(107, 126)
        Me.btn_scan.Name = "btn_scan"
        Me.btn_scan.Size = New System.Drawing.Size(57, 26)
        Me.btn_scan.TabIndex = 0
        Me.btn_scan.Text = "Find"
        Me.btn_scan.UseVisualStyleBackColor = True
        '
        'dgv_1
        '
        Me.dgv_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IpAdd})
        Me.dgv_1.Location = New System.Drawing.Point(13, 12)
        Me.dgv_1.Name = "dgv_1"
        Me.dgv_1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgv_1.Size = New System.Drawing.Size(235, 108)
        Me.dgv_1.TabIndex = 1
        '
        'IpAdd
        '
        Me.IpAdd.HeaderText = "LR01 IP found:"
        Me.IpAdd.Name = "IpAdd"
        Me.IpAdd.Width = 200
        '
        'SockConn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(260, 162)
        Me.Controls.Add(Me.dgv_1)
        Me.Controls.Add(Me.btn_scan)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SockConn"
        Me.Text = "Device URL"
        CType(Me.dgv_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_scan As System.Windows.Forms.Button
    Friend WithEvents dgv_1 As System.Windows.Forms.DataGridView
    Friend WithEvents IpAdd As System.Windows.Forms.DataGridViewTextBoxColumn

End Class

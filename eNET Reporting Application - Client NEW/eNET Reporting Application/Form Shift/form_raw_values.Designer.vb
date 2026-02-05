<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_raw_values
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
        Me.DGV_VALUES = New System.Windows.Forms.DataGridView()
        CType(Me.DGV_VALUES, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_VALUES
        '
        Me.DGV_VALUES.AllowUserToAddRows = False
        Me.DGV_VALUES.AllowUserToDeleteRows = False
        Me.DGV_VALUES.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_VALUES.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DGV_VALUES.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DGV_VALUES.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_VALUES.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_VALUES.Location = New System.Drawing.Point(0, 0)
        Me.DGV_VALUES.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DGV_VALUES.Name = "DGV_VALUES"
        Me.DGV_VALUES.RowTemplate.Height = 24
        Me.DGV_VALUES.Size = New System.Drawing.Size(472, 495)
        Me.DGV_VALUES.TabIndex = 0
        '
        'form_raw_values
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(472, 495)
        Me.Controls.Add(Me.DGV_VALUES)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "form_raw_values"
        Me.ShowIcon = False
        Me.Text = "Raw data without processing"
        CType(Me.DGV_VALUES, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGV_VALUES As DataGridView
End Class

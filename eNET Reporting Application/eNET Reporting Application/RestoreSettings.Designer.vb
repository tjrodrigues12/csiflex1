<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RestoreSettings
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSourceFile = New System.Windows.Forms.TextBox()
        Me.btnSelectFile = New System.Windows.Forms.Button()
        Me.dgvTables = New System.Windows.Forms.DataGridView()
        Me.TableSelected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TableName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ckbSelectAllTables = New System.Windows.Forms.CheckBox()
        Me.btnRestore = New System.Windows.Forms.Button()
        CType(Me.dgvTables, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 31)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select the File :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSourceFile
        '
        Me.txtSourceFile.Location = New System.Drawing.Point(159, 48)
        Me.txtSourceFile.Name = "txtSourceFile"
        Me.txtSourceFile.ReadOnly = True
        Me.txtSourceFile.Size = New System.Drawing.Size(452, 27)
        Me.txtSourceFile.TabIndex = 1
        '
        'btnSelectFile
        '
        Me.btnSelectFile.Location = New System.Drawing.Point(617, 48)
        Me.btnSelectFile.Name = "btnSelectFile"
        Me.btnSelectFile.Size = New System.Drawing.Size(32, 27)
        Me.btnSelectFile.TabIndex = 2
        Me.btnSelectFile.Text = "..."
        Me.btnSelectFile.UseVisualStyleBackColor = True
        '
        'dgvTables
        '
        Me.dgvTables.AllowUserToAddRows = False
        Me.dgvTables.AllowUserToDeleteRows = False
        Me.dgvTables.AllowUserToResizeRows = False
        Me.dgvTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTables.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TableSelected, Me.TableName})
        Me.dgvTables.Location = New System.Drawing.Point(16, 127)
        Me.dgvTables.Name = "dgvTables"
        Me.dgvTables.Size = New System.Drawing.Size(678, 459)
        Me.dgvTables.TabIndex = 3
        '
        'TableSelected
        '
        Me.TableSelected.HeaderText = ""
        Me.TableSelected.Name = "TableSelected"
        '
        'TableName
        '
        Me.TableName.DataPropertyName = "tableName"
        Me.TableName.HeaderText = "Table Name"
        Me.TableName.MinimumWidth = 550
        Me.TableName.Name = "TableName"
        Me.TableName.ReadOnly = True
        '
        'ckbSelectAllTables
        '
        Me.ckbSelectAllTables.AutoSize = True
        Me.ckbSelectAllTables.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbSelectAllTables.Location = New System.Drawing.Point(16, 104)
        Me.ckbSelectAllTables.Name = "ckbSelectAllTables"
        Me.ckbSelectAllTables.Size = New System.Drawing.Size(117, 21)
        Me.ckbSelectAllTables.TabIndex = 4
        Me.ckbSelectAllTables.Text = "Select all tables"
        Me.ckbSelectAllTables.UseVisualStyleBackColor = True
        '
        'btnRestore
        '
        Me.btnRestore.Location = New System.Drawing.Point(16, 592)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(190, 34)
        Me.btnRestore.TabIndex = 5
        Me.btnRestore.Text = "Restore Settings"
        Me.btnRestore.UseVisualStyleBackColor = True
        '
        'RestoreSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(712, 638)
        Me.Controls.Add(Me.btnRestore)
        Me.Controls.Add(Me.ckbSelectAllTables)
        Me.Controls.Add(Me.dgvTables)
        Me.Controls.Add(Me.btnSelectFile)
        Me.Controls.Add(Me.txtSourceFile)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RestoreSettings"
        Me.Text = "Restore Settings"
        CType(Me.dgvTables, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtSourceFile As TextBox
    Friend WithEvents btnSelectFile As Button
    Friend WithEvents dgvTables As DataGridView
    Friend WithEvents TableSelected As DataGridViewCheckBoxColumn
    Friend WithEvents TableName As DataGridViewTextBoxColumn
    Friend WithEvents ckbSelectAllTables As CheckBox
    Friend WithEvents btnRestore As Button
End Class

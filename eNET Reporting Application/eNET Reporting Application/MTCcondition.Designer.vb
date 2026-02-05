<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MTCcondition
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MTCcondition))
        Me.CBX_Operator = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_Value = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BTN_Done = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CBX_Operator
        '
        Me.CBX_Operator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBX_Operator.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBX_Operator.FormattingEnabled = True
        Me.CBX_Operator.Location = New System.Drawing.Point(11, 82)
        Me.CBX_Operator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CBX_Operator.Name = "CBX_Operator"
        Me.CBX_Operator.Size = New System.Drawing.Size(140, 25)
        Me.CBX_Operator.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 57)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 19)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Choose your operator"
        '
        'TB_Value
        '
        Me.TB_Value.Location = New System.Drawing.Point(252, 84)
        Me.TB_Value.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_Value.Name = "TB_Value"
        Me.TB_Value.Size = New System.Drawing.Size(140, 25)
        Me.TB_Value.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(248, 60)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(163, 19)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Enter the threshold value"
        '
        'BTN_Done
        '
        Me.BTN_Done.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Done.Location = New System.Drawing.Point(155, 141)
        Me.BTN_Done.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_Done.Name = "BTN_Done"
        Me.BTN_Done.Size = New System.Drawing.Size(88, 30)
        Me.BTN_Done.TabIndex = 4
        Me.BTN_Done.Text = "Done"
        Me.BTN_Done.UseVisualStyleBackColor = True
        '
        'MTCcondition
        '
        Me.AcceptButton = Me.BTN_Done
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(425, 184)
        Me.Controls.Add(Me.BTN_Done)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TB_Value)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CBX_Operator)
        Me.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "MTCcondition"
        Me.Text = "define your condition"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CBX_Operator As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TB_Value As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BTN_Done As System.Windows.Forms.Button
End Class

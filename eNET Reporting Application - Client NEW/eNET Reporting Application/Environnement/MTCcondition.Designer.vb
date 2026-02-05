<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MTCcondition
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
        Me.CBX_Operator.FormattingEnabled = True
        Me.CBX_Operator.Location = New System.Drawing.Point(12, 28)
        Me.CBX_Operator.Name = "CBX_Operator"
        Me.CBX_Operator.Size = New System.Drawing.Size(121, 21)
        Me.CBX_Operator.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "choose your operator"
        '
        'TB_Value
        '
        Me.TB_Value.Location = New System.Drawing.Point(159, 28)
        Me.TB_Value.Name = "TB_Value"
        Me.TB_Value.Size = New System.Drawing.Size(121, 20)
        Me.TB_Value.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(156, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "enter the threshold value"
        '
        'BTN_Done
        '
        Me.BTN_Done.Location = New System.Drawing.Point(204, 72)
        Me.BTN_Done.Name = "BTN_Done"
        Me.BTN_Done.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Done.TabIndex = 4
        Me.BTN_Done.Text = "Done"
        Me.BTN_Done.UseVisualStyleBackColor = True
        '
        'MTCcondition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(310, 107)
        Me.Controls.Add(Me.BTN_Done)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TB_Value)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CBX_Operator)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "MTCcondition"
        Me.Text = "define your condition"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CBX_Operator As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TB_Value As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BTN_Done As System.Windows.Forms.Button
End Class

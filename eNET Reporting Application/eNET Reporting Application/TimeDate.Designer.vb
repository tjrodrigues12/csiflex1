<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TimeDate
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.OK_BTN = New System.Windows.Forms.Button()
        Me.Day_TB = New System.Windows.Forms.TextBox()
        Me.Time_TB = New System.Windows.Forms.TextBox()
        Me.Year_TB = New System.Windows.Forms.TextBox()
        Me.Month_TB = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Day"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(127, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Month"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(247, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Year"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Time"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.Label5.Location = New System.Drawing.Point(128, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "HH:MM:SS"
        '
        'OK_BTN
        '
        Me.OK_BTN.Enabled = False
        Me.OK_BTN.Location = New System.Drawing.Point(153, 105)
        Me.OK_BTN.Name = "OK_BTN"
        Me.OK_BTN.Size = New System.Drawing.Size(75, 23)
        Me.OK_BTN.TabIndex = 5
        Me.OK_BTN.Text = "OK"
        Me.OK_BTN.UseVisualStyleBackColor = True
        '
        'Day_TB
        '
        Me.Day_TB.Location = New System.Drawing.Point(44, 61)
        Me.Day_TB.Name = "Day_TB"
        Me.Day_TB.Size = New System.Drawing.Size(58, 20)
        Me.Day_TB.TabIndex = 2
        '
        'Time_TB
        '
        Me.Time_TB.Location = New System.Drawing.Point(48, 22)
        Me.Time_TB.Name = "Time_TB"
        Me.Time_TB.Size = New System.Drawing.Size(74, 20)
        Me.Time_TB.TabIndex = 1
        '
        'Year_TB
        '
        Me.Year_TB.Location = New System.Drawing.Point(285, 61)
        Me.Year_TB.Name = "Year_TB"
        Me.Year_TB.Size = New System.Drawing.Size(58, 20)
        Me.Year_TB.TabIndex = 4
        '
        'Month_TB
        '
        Me.Month_TB.Location = New System.Drawing.Point(170, 61)
        Me.Month_TB.Name = "Month_TB"
        Me.Month_TB.Size = New System.Drawing.Size(58, 20)
        Me.Month_TB.TabIndex = 3
        '
        'TimeDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(372, 139)
        Me.Controls.Add(Me.Month_TB)
        Me.Controls.Add(Me.Year_TB)
        Me.Controls.Add(Me.Time_TB)
        Me.Controls.Add(Me.Day_TB)
        Me.Controls.Add(Me.OK_BTN)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "TimeDate"
        Me.Text = "TimeDate"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents OK_BTN As System.Windows.Forms.Button
    Friend WithEvents Day_TB As System.Windows.Forms.TextBox
    Friend WithEvents Time_TB As System.Windows.Forms.TextBox
    Friend WithEvents Year_TB As System.Windows.Forms.TextBox
    Friend WithEvents Month_TB As System.Windows.Forms.TextBox
End Class

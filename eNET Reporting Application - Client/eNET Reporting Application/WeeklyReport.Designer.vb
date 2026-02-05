<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WeeklyReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WeeklyReport))
        Me.MonthCalendar = New System.Windows.Forms.MonthCalendar()
        Me.BTN_Generate = New System.Windows.Forms.Button()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.BTN_Next = New System.Windows.Forms.Button()
        Me.RB_5DaysWeek = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RB_7DaysWeek = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'MonthCalendar
        '
        Me.MonthCalendar.FirstDayOfWeek = System.Windows.Forms.Day.Sunday
        Me.MonthCalendar.Location = New System.Drawing.Point(0, -1)
        Me.MonthCalendar.Name = "MonthCalendar"
        Me.MonthCalendar.ShowWeekNumbers = True
        Me.MonthCalendar.TabIndex = 0
        '
        'BTN_Generate
        '
        Me.BTN_Generate.Location = New System.Drawing.Point(81, 348)
        Me.BTN_Generate.Name = "BTN_Generate"
        Me.BTN_Generate.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Generate.TabIndex = 1
        Me.BTN_Generate.Text = "Gererate"
        Me.BTN_Generate.UseVisualStyleBackColor = True
        '
        'TreeView1
        '
        Me.TreeView1.CheckBoxes = True
        Me.TreeView1.Location = New System.Drawing.Point(0, 191)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(249, 151)
        Me.TreeView1.TabIndex = 19
        '
        'BTN_Next
        '
        Me.BTN_Next.Location = New System.Drawing.Point(248, 138)
        Me.BTN_Next.Name = "BTN_Next"
        Me.BTN_Next.Size = New System.Drawing.Size(44, 23)
        Me.BTN_Next.TabIndex = 20
        Me.BTN_Next.Text = "Next"
        Me.BTN_Next.UseVisualStyleBackColor = True
        '
        'RB_5DaysWeek
        '
        Me.RB_5DaysWeek.AutoSize = True
        Me.RB_5DaysWeek.Location = New System.Drawing.Point(81, 166)
        Me.RB_5DaysWeek.Name = "RB_5DaysWeek"
        Me.RB_5DaysWeek.Size = New System.Drawing.Size(58, 17)
        Me.RB_5DaysWeek.TabIndex = 21
        Me.RB_5DaysWeek.Text = "5 Days"
        Me.RB_5DaysWeek.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 168)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Weeks of : "
        '
        'RB_7DaysWeek
        '
        Me.RB_7DaysWeek.AutoSize = True
        Me.RB_7DaysWeek.Checked = True
        Me.RB_7DaysWeek.Location = New System.Drawing.Point(145, 166)
        Me.RB_7DaysWeek.Name = "RB_7DaysWeek"
        Me.RB_7DaysWeek.Size = New System.Drawing.Size(58, 17)
        Me.RB_7DaysWeek.TabIndex = 23
        Me.RB_7DaysWeek.TabStop = True
        Me.RB_7DaysWeek.Text = "7 Days"
        Me.RB_7DaysWeek.UseVisualStyleBackColor = True
        '
        'WeeklyReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(294, 383)
        Me.Controls.Add(Me.RB_7DaysWeek)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RB_5DaysWeek)
        Me.Controls.Add(Me.BTN_Next)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.BTN_Generate)
        Me.Controls.Add(Me.MonthCalendar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WeeklyReport"
        Me.Text = "Weekly Report"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MonthCalendar As System.Windows.Forms.MonthCalendar
    Friend WithEvents BTN_Generate As System.Windows.Forms.Button
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents BTN_Next As System.Windows.Forms.Button
    Friend WithEvents RB_5DaysWeek As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RB_7DaysWeek As System.Windows.Forms.RadioButton
End Class

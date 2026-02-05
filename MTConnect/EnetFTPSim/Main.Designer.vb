<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.BTN_Start = New System.Windows.Forms.Button()
        Me.BTN_Stop = New System.Windows.Forms.Button()
        Me.LBL_Status = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CB_MachList = New System.Windows.Forms.ComboBox()
        Me.NUD_MaxCONTime = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BTN_Save = New System.Windows.Forms.Button()
        CType(Me.NUD_MaxCONTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BTN_Start
        '
        Me.BTN_Start.Location = New System.Drawing.Point(75, 98)
        Me.BTN_Start.Name = "BTN_Start"
        Me.BTN_Start.Size = New System.Drawing.Size(174, 23)
        Me.BTN_Start.TabIndex = 0
        Me.BTN_Start.Text = "Start"
        Me.BTN_Start.UseVisualStyleBackColor = True
        '
        'BTN_Stop
        '
        Me.BTN_Stop.Location = New System.Drawing.Point(75, 127)
        Me.BTN_Stop.Name = "BTN_Stop"
        Me.BTN_Stop.Size = New System.Drawing.Size(174, 23)
        Me.BTN_Stop.TabIndex = 1
        Me.BTN_Stop.Text = "Stop"
        Me.BTN_Stop.UseVisualStyleBackColor = True
        '
        'LBL_Status
        '
        Me.LBL_Status.AutoSize = True
        Me.LBL_Status.Location = New System.Drawing.Point(140, 162)
        Me.LBL_Status.Name = "LBL_Status"
        Me.LBL_Status.Size = New System.Drawing.Size(47, 13)
        Me.LBL_Status.TabIndex = 2
        Me.LBL_Status.Text = "Stopped"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Select the machine"
        '
        'CB_MachList
        '
        Me.CB_MachList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_MachList.FormattingEnabled = True
        Me.CB_MachList.Location = New System.Drawing.Point(28, 44)
        Me.CB_MachList.Name = "CB_MachList"
        Me.CB_MachList.Size = New System.Drawing.Size(121, 21)
        Me.CB_MachList.TabIndex = 4
        '
        'NUD_MaxCONTime
        '
        Me.NUD_MaxCONTime.Location = New System.Drawing.Point(176, 45)
        Me.NUD_MaxCONTime.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NUD_MaxCONTime.Name = "NUD_MaxCONTime"
        Me.NUD_MaxCONTime.Size = New System.Drawing.Size(73, 20)
        Me.NUD_MaxCONTime.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(173, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(142, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Max Cycle ON time in minute"
        '
        'BTN_Save
        '
        Me.BTN_Save.Location = New System.Drawing.Point(255, 44)
        Me.BTN_Save.Name = "BTN_Save"
        Me.BTN_Save.Size = New System.Drawing.Size(63, 23)
        Me.BTN_Save.TabIndex = 8
        Me.BTN_Save.Text = "Save"
        Me.BTN_Save.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(327, 215)
        Me.Controls.Add(Me.BTN_Save)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.NUD_MaxCONTime)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CB_MachList)
        Me.Controls.Add(Me.LBL_Status)
        Me.Controls.Add(Me.BTN_Stop)
        Me.Controls.Add(Me.BTN_Start)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.Text = "Enet FTP Simulator"
        CType(Me.NUD_MaxCONTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BTN_Start As System.Windows.Forms.Button
    Friend WithEvents BTN_Stop As System.Windows.Forms.Button
    Friend WithEvents LBL_Status As System.Windows.Forms.Label
    Friend WithEvents Label1 As Label
    Friend WithEvents CB_MachList As ComboBox
    Friend WithEvents NUD_MaxCONTime As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents BTN_Save As Button
End Class

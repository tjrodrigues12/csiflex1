<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EnetStatusChanger
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EnetStatusChanger))
        Me.CB_MachList = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CB_Status = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BTN_ChangeStatus = New System.Windows.Forms.Button()
        Me.TB_PartNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BTN_SetPartNo = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CB_Heads = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'CB_MachList
        '
        Me.CB_MachList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_MachList.FormattingEnabled = True
        Me.CB_MachList.Location = New System.Drawing.Point(20, 42)
        Me.CB_MachList.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CB_MachList.Name = "CB_MachList"
        Me.CB_MachList.Size = New System.Drawing.Size(160, 24)
        Me.CB_MachList.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 22)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select the machine"
        '
        'CB_Status
        '
        Me.CB_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_Status.FormattingEnabled = True
        Me.CB_Status.Location = New System.Drawing.Point(223, 42)
        Me.CB_Status.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CB_Status.Name = "CB_Status"
        Me.CB_Status.Size = New System.Drawing.Size(160, 24)
        Me.CB_Status.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(219, 22)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Select the status"
        '
        'BTN_ChangeStatus
        '
        Me.BTN_ChangeStatus.Location = New System.Drawing.Point(392, 39)
        Me.BTN_ChangeStatus.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_ChangeStatus.Name = "BTN_ChangeStatus"
        Me.BTN_ChangeStatus.Size = New System.Drawing.Size(144, 28)
        Me.BTN_ChangeStatus.TabIndex = 4
        Me.BTN_ChangeStatus.Text = "Change status"
        Me.BTN_ChangeStatus.UseVisualStyleBackColor = True
        '
        'TB_PartNo
        '
        Me.TB_PartNo.Location = New System.Drawing.Point(223, 103)
        Me.TB_PartNo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TB_PartNo.Name = "TB_PartNo"
        Me.TB_PartNo.Size = New System.Drawing.Size(160, 22)
        Me.TB_PartNo.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(219, 84)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(143, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Enter the partnumber"
        '
        'BTN_SetPartNo
        '
        Me.BTN_SetPartNo.Location = New System.Drawing.Point(392, 101)
        Me.BTN_SetPartNo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BTN_SetPartNo.Name = "BTN_SetPartNo"
        Me.BTN_SetPartNo.Size = New System.Drawing.Size(144, 28)
        Me.BTN_SetPartNo.TabIndex = 7
        Me.BTN_SetPartNo.Text = "Change partnumber"
        Me.BTN_SetPartNo.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 81)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(145, 17)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Select the head/pallet"
        '
        'CB_Heads
        '
        Me.CB_Heads.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_Heads.FormattingEnabled = True
        Me.CB_Heads.Items.AddRange(New Object() {"1", "2"})
        Me.CB_Heads.Location = New System.Drawing.Point(20, 101)
        Me.CB_Heads.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CB_Heads.Name = "CB_Heads"
        Me.CB_Heads.Size = New System.Drawing.Size(160, 24)
        Me.CB_Heads.TabIndex = 8
        '
        'EnetStatusChanger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 160)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CB_Heads)
        Me.Controls.Add(Me.BTN_SetPartNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TB_PartNo)
        Me.Controls.Add(Me.BTN_ChangeStatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CB_Status)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CB_MachList)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "EnetStatusChanger"
        Me.Text = "CSI Flex Enet Status Changer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CB_MachList As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CB_Status As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BTN_ChangeStatus As System.Windows.Forms.Button
    Friend WithEvents TB_PartNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents BTN_SetPartNo As System.Windows.Forms.Button
    Friend WithEvents Label4 As Label
    Friend WithEvents CB_Heads As ComboBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddExternalSource
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddExternalSource))
        Me.TB_ExtName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TB_ExtIP = New System.Windows.Forms.TextBox()
        Me.BTN_Save = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TB_ExtName
        '
        Me.TB_ExtName.Location = New System.Drawing.Point(66, 21)
        Me.TB_ExtName.Name = "TB_ExtName"
        Me.TB_ExtName.Size = New System.Drawing.Size(100, 20)
        Me.TB_ExtName.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(43, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "IP"
        '
        'TB_ExtIP
        '
        Me.TB_ExtIP.Location = New System.Drawing.Point(66, 58)
        Me.TB_ExtIP.Name = "TB_ExtIP"
        Me.TB_ExtIP.Size = New System.Drawing.Size(100, 20)
        Me.TB_ExtIP.TabIndex = 3
        '
        'BTN_Save
        '
        Me.BTN_Save.Location = New System.Drawing.Point(91, 99)
        Me.BTN_Save.Name = "BTN_Save"
        Me.BTN_Save.Size = New System.Drawing.Size(75, 23)
        Me.BTN_Save.TabIndex = 4
        Me.BTN_Save.Text = "Add"
        Me.BTN_Save.UseVisualStyleBackColor = True
        '
        'AddExternalSource
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(193, 147)
        Me.Controls.Add(Me.BTN_Save)
        Me.Controls.Add(Me.TB_ExtIP)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TB_ExtName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AddExternalSource"
        Me.Text = "Add External Source"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TB_ExtName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TB_ExtIP As System.Windows.Forms.TextBox
    Friend WithEvents BTN_Save As System.Windows.Forms.Button
End Class

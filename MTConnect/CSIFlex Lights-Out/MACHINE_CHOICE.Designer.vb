<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MACHINE_CHOICE
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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"text1", "", ""}, -1)
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("text2")
        Me.LBL_Equipements = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.SuspendLayout()
        '
        'LBL_Equipements
        '
        Me.LBL_Equipements.AutoSize = True
        Me.LBL_Equipements.BackColor = System.Drawing.SystemColors.HotTrack
        Me.LBL_Equipements.Dock = System.Windows.Forms.DockStyle.Top
        Me.LBL_Equipements.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_Equipements.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.LBL_Equipements.Location = New System.Drawing.Point(0, 0)
        Me.LBL_Equipements.Name = "LBL_Equipements"
        Me.LBL_Equipements.Padding = New System.Windows.Forms.Padding(20)
        Me.LBL_Equipements.Size = New System.Drawing.Size(211, 77)
        Me.LBL_Equipements.TabIndex = 0
        Me.LBL_Equipements.Text = "Equipements"
        '
        'ListView1
        '
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        ListViewItem1.UseItemStyleForSubItems = False
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2})
        Me.ListView1.Location = New System.Drawing.Point(0, 77)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(209, 437)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Tile
        '
        'MACHINE_CHOICE
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(209, 514)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.LBL_Equipements)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "MACHINE_CHOICE"
        Me.Text = "MACHINE_CHOICE"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LBL_Equipements As Label
    Friend WithEvents ListView1 As ListView
End Class

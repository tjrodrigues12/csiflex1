<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CSIFlexClientSetup
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CSIFlexClientSetup))
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.numPort = New System.Windows.Forms.NumericUpDown()
        Me.CB_IPAddress = New System.Windows.Forms.ComboBox()
        Me.BTN_IPRefresh = New System.Windows.Forms.Button()
        Me.BTN_Start = New System.Windows.Forms.Button()
        Me.BTN_Stop = New System.Windows.Forms.Button()
        Me.BTN_Uninstall = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LBL_ServiceResult = New System.Windows.Forms.Label()
        Me.BTN_ViewInBrowser = New System.Windows.Forms.Button()
        Me.trayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        CType(Me.numPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(460, 25)
        Me.label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(50, 20)
        Me.label2.TabIndex = 2
        Me.label2.Text = "Port:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(40, 25)
        Me.label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(105, 20)
        Me.label1.TabIndex = 0
        Me.label1.Text = "IP address:"
        '
        'numPort
        '
        Me.numPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numPort.Location = New System.Drawing.Point(463, 46)
        Me.numPort.Margin = New System.Windows.Forms.Padding(4)
        Me.numPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.numPort.Name = "numPort"
        Me.numPort.Size = New System.Drawing.Size(117, 27)
        Me.numPort.TabIndex = 3
        Me.numPort.Value = New Decimal(New Integer() {8002, 0, 0, 0})
        '
        'CB_IPAddress
        '
        Me.CB_IPAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_IPAddress.FormattingEnabled = True
        Me.CB_IPAddress.Location = New System.Drawing.Point(43, 46)
        Me.CB_IPAddress.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CB_IPAddress.Name = "CB_IPAddress"
        Me.CB_IPAddress.Size = New System.Drawing.Size(375, 28)
        Me.CB_IPAddress.TabIndex = 1
        '
        'BTN_IPRefresh
        '
        Me.BTN_IPRefresh.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_IPRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_IPRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_IPRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_IPRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_IPRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_IPRefresh.Location = New System.Drawing.Point(619, 41)
        Me.BTN_IPRefresh.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BTN_IPRefresh.Name = "BTN_IPRefresh"
        Me.BTN_IPRefresh.Size = New System.Drawing.Size(133, 36)
        Me.BTN_IPRefresh.TabIndex = 4
        Me.BTN_IPRefresh.Text = "Refresh IP"
        Me.BTN_IPRefresh.UseVisualStyleBackColor = False
        '
        'BTN_Start
        '
        Me.BTN_Start.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Start.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Start.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Start.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Start.Location = New System.Drawing.Point(44, 177)
        Me.BTN_Start.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BTN_Start.Name = "BTN_Start"
        Me.BTN_Start.Size = New System.Drawing.Size(144, 62)
        Me.BTN_Start.TabIndex = 7
        Me.BTN_Start.Text = "Start"
        Me.BTN_Start.UseVisualStyleBackColor = False
        '
        'BTN_Stop
        '
        Me.BTN_Stop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Stop.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Stop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Stop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Stop.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Stop.Location = New System.Drawing.Point(249, 177)
        Me.BTN_Stop.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BTN_Stop.Name = "BTN_Stop"
        Me.BTN_Stop.Size = New System.Drawing.Size(144, 62)
        Me.BTN_Stop.TabIndex = 8
        Me.BTN_Stop.Text = "Stop"
        Me.BTN_Stop.UseVisualStyleBackColor = False
        '
        'BTN_Uninstall
        '
        Me.BTN_Uninstall.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_Uninstall.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_Uninstall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_Uninstall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_Uninstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_Uninstall.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Uninstall.Location = New System.Drawing.Point(671, 177)
        Me.BTN_Uninstall.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BTN_Uninstall.Name = "BTN_Uninstall"
        Me.BTN_Uninstall.Size = New System.Drawing.Size(144, 62)
        Me.BTN_Uninstall.TabIndex = 10
        Me.BTN_Uninstall.Text = "Uninstall"
        Me.BTN_Uninstall.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(43, 113)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Status :"
        '
        'LBL_ServiceResult
        '
        Me.LBL_ServiceResult.AutoSize = True
        Me.LBL_ServiceResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_ServiceResult.Location = New System.Drawing.Point(125, 113)
        Me.LBL_ServiceResult.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LBL_ServiceResult.Name = "LBL_ServiceResult"
        Me.LBL_ServiceResult.Size = New System.Drawing.Size(0, 20)
        Me.LBL_ServiceResult.TabIndex = 6
        '
        'BTN_ViewInBrowser
        '
        Me.BTN_ViewInBrowser.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_ViewInBrowser.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.BTN_ViewInBrowser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BTN_ViewInBrowser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BTN_ViewInBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_ViewInBrowser.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_ViewInBrowser.Location = New System.Drawing.Point(463, 177)
        Me.BTN_ViewInBrowser.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BTN_ViewInBrowser.Name = "BTN_ViewInBrowser"
        Me.BTN_ViewInBrowser.Size = New System.Drawing.Size(144, 62)
        Me.BTN_ViewInBrowser.TabIndex = 9
        Me.BTN_ViewInBrowser.Text = "View In Browser"
        Me.BTN_ViewInBrowser.UseVisualStyleBackColor = False
        '
        'trayIcon
        '
        Me.trayIcon.Icon = CType(resources.GetObject("trayIcon.Icon"), System.Drawing.Icon)
        Me.trayIcon.Text = "CSIFlex Client Application"
        Me.trayIcon.Visible = True
        '
        'CSIFlexClientSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(845, 267)
        Me.Controls.Add(Me.BTN_ViewInBrowser)
        Me.Controls.Add(Me.LBL_ServiceResult)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.BTN_Uninstall)
        Me.Controls.Add(Me.BTN_Stop)
        Me.Controls.Add(Me.BTN_Start)
        Me.Controls.Add(Me.BTN_IPRefresh)
        Me.Controls.Add(Me.CB_IPAddress)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.numPort)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(863, 314)
        Me.MinimumSize = New System.Drawing.Size(863, 314)
        Me.Name = "CSIFlexClientSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CSIFlex Client Application"
        CType(Me.numPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents label2 As Label
    Private WithEvents label1 As Label
    Private WithEvents numPort As NumericUpDown
    Friend WithEvents CB_IPAddress As ComboBox
    Friend WithEvents BTN_IPRefresh As Button
    Friend WithEvents BTN_Start As Button
    Friend WithEvents BTN_Stop As Button
    Friend WithEvents BTN_Uninstall As Button
    Private WithEvents Label3 As Label
    Private WithEvents LBL_ServiceResult As Label
    Friend WithEvents BTN_ViewInBrowser As Button
    Private WithEvents trayIcon As NotifyIcon
End Class

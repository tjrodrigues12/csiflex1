<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InternalWebServer
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InternalWebServer))
        Me.folderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.button1 = New System.Windows.Forms.Button()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtContentPath = New System.Windows.Forms.TextBox()
        Me.ipAddresses = New System.Windows.Forms.Label()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.nudIP3 = New System.Windows.Forms.NumericUpDown()
        Me.nudIP2 = New System.Windows.Forms.NumericUpDown()
        Me.nudIP1 = New System.Windows.Forms.NumericUpDown()
        Me.nudPort = New System.Windows.Forms.NumericUpDown()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.trayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.nudIP4 = New System.Windows.Forms.NumericUpDown()
        Me.btnStop = New System.Windows.Forms.Button()
        CType(Me.nudIP3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudIP2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudIP1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudPort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudIP4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'button1
        '
        Me.button1.Location = New System.Drawing.Point(591, 95)
        Me.button1.Margin = New System.Windows.Forms.Padding(4)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(142, 28)
        Me.button1.TabIndex = 58
        Me.button1.Text = "Choose Folder "
        Me.button1.UseVisualStyleBackColor = True
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(29, 79)
        Me.label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(172, 17)
        Me.label3.TabIndex = 57
        Me.label3.Text = "Content path (root folder):"
        '
        'txtContentPath
        '
        Me.txtContentPath.Location = New System.Drawing.Point(33, 98)
        Me.txtContentPath.Margin = New System.Windows.Forms.Padding(4)
        Me.txtContentPath.Name = "txtContentPath"
        Me.txtContentPath.Size = New System.Drawing.Size(550, 22)
        Me.txtContentPath.TabIndex = 56
        Me.txtContentPath.Text = "C:\Users\BDesai\Desktop\RedirectEnetPage\CSIFlex_Server_WebPages"
        '
        'ipAddresses
        '
        Me.ipAddresses.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ipAddresses.Location = New System.Drawing.Point(26, 169)
        Me.ipAddresses.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ipAddresses.Name = "ipAddresses"
        Me.ipAddresses.Size = New System.Drawing.Size(707, 60)
        Me.ipAddresses.TabIndex = 55
        Me.ipAddresses.Text = "Local IP: "
        Me.ipAddresses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnTest
        '
        Me.btnTest.Enabled = False
        Me.btnTest.Location = New System.Drawing.Point(156, 133)
        Me.btnTest.Margin = New System.Windows.Forms.Padding(4)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(100, 28)
        Me.btnTest.TabIndex = 54
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(605, 20)
        Me.label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(38, 17)
        Me.label2.TabIndex = 53
        Me.label2.Text = "Port:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(29, 22)
        Me.label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(79, 17)
        Me.label1.TabIndex = 52
        Me.label1.Text = "IP address:"
        '
        'nudIP3
        '
        Me.nudIP3.Location = New System.Drawing.Point(315, 43)
        Me.nudIP3.Margin = New System.Windows.Forms.Padding(4)
        Me.nudIP3.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudIP3.Name = "nudIP3"
        Me.nudIP3.Size = New System.Drawing.Size(117, 22)
        Me.nudIP3.TabIndex = 50
        '
        'nudIP2
        '
        Me.nudIP2.Location = New System.Drawing.Point(174, 43)
        Me.nudIP2.Margin = New System.Windows.Forms.Padding(4)
        Me.nudIP2.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudIP2.Name = "nudIP2"
        Me.nudIP2.Size = New System.Drawing.Size(117, 22)
        Me.nudIP2.TabIndex = 49
        '
        'nudIP1
        '
        Me.nudIP1.Location = New System.Drawing.Point(33, 43)
        Me.nudIP1.Margin = New System.Windows.Forms.Padding(4)
        Me.nudIP1.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudIP1.Name = "nudIP1"
        Me.nudIP1.Size = New System.Drawing.Size(117, 22)
        Me.nudIP1.TabIndex = 48
        Me.nudIP1.Value = New Decimal(New Integer() {127, 0, 0, 0})
        '
        'nudPort
        '
        Me.nudPort.Location = New System.Drawing.Point(608, 43)
        Me.nudPort.Margin = New System.Windows.Forms.Padding(4)
        Me.nudPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudPort.Name = "nudPort"
        Me.nudPort.Size = New System.Drawing.Size(117, 22)
        Me.nudPort.TabIndex = 47
        Me.nudPort.Value = New Decimal(New Integer() {8000, 0, 0, 0})
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(48, 133)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(100, 28)
        Me.btnStart.TabIndex = 45
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'trayIcon
        '
        Me.trayIcon.Icon = CType(resources.GetObject("trayIcon.Icon"), System.Drawing.Icon)
        Me.trayIcon.Text = "CSIFlex Web Server"
        Me.trayIcon.Visible = True
        '
        'nudIP4
        '
        Me.nudIP4.Location = New System.Drawing.Point(466, 43)
        Me.nudIP4.Margin = New System.Windows.Forms.Padding(4)
        Me.nudIP4.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.nudIP4.Name = "nudIP4"
        Me.nudIP4.Size = New System.Drawing.Size(117, 22)
        Me.nudIP4.TabIndex = 51
        Me.nudIP4.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(264, 133)
        Me.btnStop.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(100, 28)
        Me.btnStop.TabIndex = 46
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'InternalWebServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 249)
        Me.Controls.Add(Me.button1)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.txtContentPath)
        Me.Controls.Add(Me.ipAddresses)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.nudIP3)
        Me.Controls.Add(Me.nudIP2)
        Me.Controls.Add(Me.nudIP1)
        Me.Controls.Add(Me.nudPort)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.nudIP4)
        Me.Controls.Add(Me.btnStop)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "InternalWebServer"
        Me.Text = "InternalWebServer"
        CType(Me.nudIP3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudIP2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudIP1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudPort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudIP4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents folderBrowserDialog1 As FolderBrowserDialog
    Private WithEvents button1 As Button
    Private WithEvents label3 As Label
    Private WithEvents txtContentPath As TextBox
    Private WithEvents ipAddresses As Label
    Private WithEvents btnTest As Button
    Private WithEvents label2 As Label
    Private WithEvents label1 As Label
    Private WithEvents nudIP3 As NumericUpDown
    Private WithEvents nudIP2 As NumericUpDown
    Private WithEvents nudIP1 As NumericUpDown
    Private WithEvents nudPort As NumericUpDown
    Private WithEvents btnStart As Button
    Private WithEvents trayIcon As NotifyIcon
    Private WithEvents nudIP4 As NumericUpDown
    Private WithEvents btnStop As Button
End Class

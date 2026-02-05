Imports System.IO
Imports System.Security.Principal
Imports CSIFLEX.Utilities


Public Class Welcome

    Public CSIF_version As Integer = 3

    Private validIpAddress As Boolean = False
    Public Shared from_ As String

    Private Sub BtnConfirmOk_Click(sender As Object, e As EventArgs) Handles btnConfirmOk.Click

        If Not Utilities.IsValidIpAddress(txtDatabaseIP.Text, True) Then

            MessageBox.Show("Invalid Database IP Address", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return

        End If

        validIpAddress = True
        CSIFLEXSettings.Instance.DatabaseIp = txtDatabaseIP.Text
        CSIFLEXSettings.Instance.SaveSettings()
        Me.Close()

    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs)

        UserLogin.toBeClosed = True

        Me.Close()

    End Sub



    '-----------------------------------------------------------------------------------------------------------------------
    ' MOVE FORME 
    '  
    '-----------------------------------------------------------------------------------------------------------------------
#Region "form move"
    Dim _MouseDown As Boolean, _MouseX As Integer, _MouseY As Integer
    Private Sub Config_report_Form_mouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        _MouseDown = True
        _MouseX = e.X
        _MouseY = e.Y
    End Sub

    Private Sub Config_report_Form_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        _MouseDown = False
    End Sub


    Private Sub Config_report_Form_Mousemove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove


        If _MouseDown = True Then
            Reporting_application.SuspendLayout()
            Me.Left = Me.Left + (e.X - _MouseX)
            Me.Top = Me.Top + (e.Y - _MouseY)
            If Me.Top < 20 Then Me.Top = 0
            If Me.Left < 20 Then Me.Left = 0
            Reporting_application.ResumeLayout(True)

        End If

    End Sub
#End Region

    Private Sub Welcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Width = 500

        txtDatabaseIP.Text = CSIFLEXSettings.Instance.DatabaseIp
        txtEnetIPAddress.Text = CSIFLEXSettings.Instance.EnetIPAddress
        txtEnetPort.Text = CSIFLEXSettings.Instance.EnetIPPort

        RB_Version_ServerBased.Checked = True

        GroupBox1.Text = "CSIFlex Client"
        CB_LocalDatabase.Checked = True
        CB_LocalDatabase.Visible = False
        CB_LocalDatabase.Enabled = False
        Label2.Text = "Server DB Ip : "

        Label3.Visible = False
        Label3.Text = "eNETDNC Dashboard IP address"
        Label4.Visible = False
        Label4.Text = "Port"

        txtEnetIPAddress.Visible = False
        txtEnetPort.Visible = False

        BTN_PathDB.Visible = False
        BTN_PathDB.Enabled = False
        BTN_DefaultFolder.Visible = False
        BTN_DefaultFolder.Enabled = False

    End Sub

    Private Sub Welcome_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing

        If Not validIpAddress Then
            DialogResult = DialogResult.Cancel
        Else
            DialogResult = DialogResult.OK
        End If

    End Sub

    Public years__ As New List(Of String)


    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles BTN_DefaultFolder.Click
        txtEnetIPAddress.Text = "C:\_eNETDNC"
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CB_LocalDatabase.CheckedChanged

        If Me.Width < 800 And CB_LocalDatabase.Checked Then
            Me.Width = 500
        End If

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

End Class
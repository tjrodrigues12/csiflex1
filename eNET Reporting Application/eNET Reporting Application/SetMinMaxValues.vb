Imports MySql.Data.MySqlClient

Public Class SetMinMaxValues
    Public mysqlUpdate As String
    Public cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
    Private Sub BTN_Save_Click(sender As Object, e As EventArgs) Handles BTN_Save.Click
        'save data
        If TB_MAX.Text.Length > 0 And TB_MIN.Text.Length > 0 Then
            Try
                cntsql.Open()
                If LBL_AttributeName.Text.Contains("Feedrate Override") Then
                    mysqlUpdate = "UPDATE ignore `csi_auth`.`tbl_csiothersettings` SET `Feed_MIN` = '" & TB_MIN.Text & "', `Feed_MAX` = '" & TB_MAX.Text & "'WHERE `Machine_Name` = '" & LBL_MCHName.Text & "' AND `IP_Address` = '" & LBL_MCHIP.Text & "';"
                ElseIf LBL_AttributeName.Text.Contains("Spindle Override") Then
                    mysqlUpdate = "UPDATE ignore `csi_auth`.`tbl_csiothersettings `SET `Spindle_MIN` = '" & TB_MIN.Text & "', `Spindle_MAX` = '" & TB_MAX.Text & "' WHERE `Machine_Name` = '" & LBL_MCHName.Text & "' AND `IP_Address` = '" & LBL_MCHIP.Text & "';"
                ElseIf LBL_AttributeName.Text.Contains("Rapid Override") Then
                    mysqlUpdate = "UPDATE ignore `csi_auth`.`tbl_csiothersettings` SET `Rapid_MIN` = '" & TB_MIN.Text & "', `Rapid_MAX` = '" & TB_MAX.Text & "' WHERE `Machine_Name` = '" & LBL_MCHName.Text & "' AND `IP_Address` = '" & LBL_MCHIP.Text & "';"
                End If
                Dim cmdmysqlUpdate As New MySqlCommand(mysqlUpdate, cntsql)
                cmdmysqlUpdate.ExecuteNonQuery()
                'cntsql.Close()
                Me.Close()
            Catch ex As Exception
                MessageBox.Show("Error in Update MIN/MAX values : " & ex.ToString())
            Finally
                If cntsql.State = ConnectionState.Open Then
                    cntsql.Close()
                End If
            End Try
        Else
            MessageBox.Show("Please enter MIN/MAX values !")
        End If
    End Sub
    Private Sub BTN_Cancel_Click(sender As Object, e As EventArgs) Handles BTN_Cancel.Click
        Me.Close()
    End Sub

    Private Sub SetMinMaxValues_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        Me.BringToFront()
    End Sub
    Public Sub New(ByVal AttributeName As String, ByVal ParameterValue As String, ByVal MachineIP As String, ByVal MachineName As String)

        ' This call is required by the designer.
        InitializeComponent()
        If AttributeName = "Feedrate Override" Then
            'Case Feedrate Override
            LBL_AttributeName.Text = "Feedrate Override : " & ParameterValue
            'TB_AttributeValue.Text = ParameterValue
            TB_MIN.Focus()
        ElseIf AttributeName = "Spindle Override" Then
            'Case Spindle Override
            LBL_AttributeName.Text = "Spindle Override : " & ParameterValue
            'TB_AttributeValue.Text = ParameterValue
            TB_MIN.Focus()
        ElseIf AttributeName = "Rapid Override" Then
            'Case Repid Override
            LBL_AttributeName.Text = "Rapid Override : " & ParameterValue
            'TB_AttributeValue.Text = ParameterValue
            TB_MIN.Focus()
        End If
        LBL_MCHIP.Text = MachineIP
        LBL_MCHName.Text = MachineName
        Me.TopMost = True
        Me.BringToFront()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
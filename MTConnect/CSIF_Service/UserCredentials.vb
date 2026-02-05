Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class UserCredentials
    Public Sub New(ByVal RemotePCName As String, ByVal RemotePCPwd As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TB_EnetPCName.Text = RemotePCName
        TB_EnetPCPwd.Text = RemotePCPwd
    End Sub
    Private Sub BTN_Save_Click(sender As Object, e As EventArgs) Handles BTN_Save.Click
        If TB_EnetPCName.Text.Length > 0 And TB_EnetPCPwd.Text.Length > 0 Then
            Dim mySqlCnt As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            Try
                mySqlCnt.Open()
                Dim mysql As String = "update ignore csi_auth.ftpconfig SET EnetPCName = '" & TB_EnetPCName.Text & "',EnetPCPwd = '" & TB_EnetPCPwd.Text & "' LIMIT 1 ;"
                Dim cmd As New MySqlCommand(mysql, mySqlCnt)
                cmd.ExecuteNonQuery()
                mysql = "commit"
                cmd = New MySqlCommand(mysql, mySqlCnt)
                cmd.ExecuteNonQuery()
                mySqlCnt.Close()
                MessageBox.Show("Remote PC Credentials saved successfully ! You  need to give Full Control access to the c:\_eNETDNC folder for the CSIFlex Admin user !")
            Catch ex As Exception
                If mySqlCnt.State = ConnectionState.Open Then mySqlCnt.Close()
                MessageBox.Show("Credential save error : " & ex.Message())
            End Try
        Else
            MessageBox.Show("Please fill Enet PC Name and Enet PC Password !")
        End If
    End Sub
End Class
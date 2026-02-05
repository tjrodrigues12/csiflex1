Imports System.IO
Imports CSIFLEX.Database.Access
Imports MySql.Data.MySqlClient

Public Class Config

    Private Sub BTN_Save_Click(sender As Object, e As EventArgs) Handles BTN_Save.Click

        Try

            Dim dTable_message13 = MySqlAccess.GetDataTable("SELECT * from CSI_auth.ftpconfig;")

            If dTable_message13.Rows.Count > 0 Then
                MySqlAccess.ExecuteNonQuery($"delete from CSI_auth.ftpconfig; insert into CSI_auth.ftpconfig  (ftpip,ftppwd) Values ('{ TB_IP.Text.Trim }','{ TB_Pwd.Text.Trim }') ;")
            Else
                MySqlAccess.ExecuteNonQuery($"insert into CSI_auth.ftpconfig  (ftpip,ftppwd) Values ('{ TB_IP.Text.Trim }','{ TB_Pwd.Text.Trim }') ;")
            End If

            Dispose()

        Catch ex As Exception
            CSI_Lib.LogServerError("MySQL Error in BTN_Save_Click on Config.vb Form :" & ex.Message() & " StackTrace : " & ex.StackTrace(), 1)
        End Try

    End Sub

    Private Function CheckConfig() As Boolean
        Dim config As Boolean = False

        If (TB_IP.Text.Length > 0) Then
            config = True
        End If

        Return config
    End Function

    Private Sub Config_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'RemoveHandler CHKB_LoadingAsCON.CheckedChanged, AddressOf CHKB_LoadingAsCON_CheckedChanged
            Using mysqlcon As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                Try
                    mysqlcon.Open()
                    Using mysqlcmd As New MySqlDataAdapter("select ftpip, ftppwd from csi_auth.ftpconfig;", mysqlcon)
                        Dim dt As New DataTable
                        mysqlcmd.Fill(dt)
                        If (dt.Rows.Count >= 1) Then
                            TB_IP.Text = dt.Rows(0)("ftpip")
                            TB_Pwd.Text = dt.Rows(0)("ftppwd")
                        End If
                    End Using
                Catch ex As Exception
                    MessageBox.Show("MYSQL Error in Config_Load Method : " & ex.Message())
                Finally
                    mysqlcon.Close()
                End Try
            End Using
        Catch ex As Exception
            CSI_Lib.LogServerError("unable to load ftp config:" + ex.Message, 1)
            MessageBox.Show("unable to load ftp config")
            'Finally
            '    AddHandler CHKB_LoadingAsCON.CheckedChanged, AddressOf CHKB_LoadingAsCON_CheckedChanged
        End Try
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As EventArgs) Handles PictureBox1.MouseDown
        TB_Pwd.PasswordChar = ControlChars.NullChar
    End Sub
    Private Sub PictureBox1_MouseUp(sender As Object, e As EventArgs) Handles PictureBox1.MouseUp
        TB_Pwd.PasswordChar = "*"c
    End Sub
End Class
Imports MySql.Data.MySqlClient

Public Class CustomEmailMsg

    Public taskname As String

    Private Sub CustomEmailMsg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load msg from taskname

        Dim sql As String = ("select CustomMsg from CSI_auth.Auto_Report_config where Task_name = '" + taskname + "'")
        Dim dt As New DataTable
        Try
            Using connection As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                Try
                    connection.Open()
                    Using mysqladap As New MySqlDataAdapter(sql, connection)
                        mysqladap.Fill(dt)
                    End Using
                Catch ex As Exception
                    CSI_Lib.LogServerError("MYSQL Error on CustomEmailMsg.vb Page : " & ex.Message() & " StackTrace : " & ex.StackTrace(), 1)
                Finally
                    connection.Close()
                End Try
            End Using

            If (dt.Rows.Count > 0) Then
                RTB_Msg.Text = dt.Rows(0)("CustomMsg").ToString()
            End If
        Catch ex As Exception
            CSI_Lib.LogServerError("Unable to retrieve custom message:" + ex.Message, 1)
        End Try

    End Sub

    Private Sub BTN_Done_Click(sender As Object, e As EventArgs) Handles BTN_Done.Click
        'save RTB_Msg.text to autoreport


        Dim sqlupdate As String = "UPDATE CSI_auth.auto_report_config SET CustomMsg='" + RTB_Msg.Text + "' WHERE Task_name='" + taskname + "'"
        'Dim sql As String = ("update CustomMsg from CSI_auth.Auto_Report_config where Task_name = '" + taskname + "'")
        'Dim dt As New DataTable
        Try
            Using connection As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                Try
                    connection.Open()
                    Using mysqlcmd As New MySqlCommand(sqlupdate, connection)
                        mysqlcmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    CSI_Lib.LogServerError("Mysql Error in BTN_Done_Click on CustomEmailMsg.vb Page : " + ex.Message, 1)
                Finally
                    connection.Close()
                End Try
            End Using
        Catch ex As Exception
            CSI_Lib.LogServerError("Unable to update custom message:" + ex.Message, 1)
        End Try

        Dispose()
    End Sub
End Class
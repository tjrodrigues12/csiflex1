Imports Renci.SshNet
Public Class TimeDate

    Private Sub TimeDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles OK_BTN.Click
        Try

            Using client = New SshClient(SetupForm2.IP_TB.Text, "pi", "raspberry")
                client.Connect()
                Dim d As String = """" + Day_TB.Text + " " + MonthName(Month_TB.Text) + " " + Year_TB.Text + " " + Time_TB.Text + """"
                Using cmd2 = client.CreateCommand("sudo date -s " + d)
                    cmd2.Execute()
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Device can't be found")
        End Try

        Me.Close()
    End Sub

    Private Sub checktimedate()
        If Time_TB.Text <> "" And Day_TB.Text <> "" And Month_TB.Text <> "" And Year_TB.Text <> "" Then
            OK_BTN.Enabled = True
        Else
            OK_BTN.Enabled = False
        End If
    End Sub

    Private Sub Time_TB_TextChanged(sender As Object, e As EventArgs) Handles Time_TB.TextChanged
        checktimedate()
    End Sub

    Private Sub Day_TB_TextChanged(sender As Object, e As EventArgs) Handles Day_TB.TextChanged
        checktimedate()
    End Sub

    Private Sub Month_TB_TextChanged(sender As Object, e As EventArgs) Handles Month_TB.TextChanged
        checktimedate()
    End Sub

    Private Sub Year_TB_TextChanged(sender As Object, e As EventArgs) Handles Year_TB.TextChanged
        checktimedate()
    End Sub
End Class
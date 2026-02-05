Imports System.IO

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click




        While (True)

            Dim reader As StreamReader = New StreamReader(TextBox1.Text)



            Threading.Thread.Sleep(300)
        End While
    End Sub
End Class

Imports System.Reflection
Imports System.IO

Public Class ipchoose

    Private Sub BTN_Ok_Click(sender As Object, e As EventArgs) Handles BTN_Ok.Click

        Dim path As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)

        Try

            If LB_IpAddress.SelectedItem Is Nothing Then MessageBox.Show("??")
            '.Text = ListBox1.SelectedItem.ToString
            SetupForm.TB_IpAdress.Text = LB_IpAddress.SelectedItem.ToString.Split(":")(0)


        Catch ex As Exception
            MessageBox.Show("CSIFLEX has  encountered an error while saving the IP adress : " & ex.Message)
        End Try
        Me.Close()
    End Sub

    Private Sub ipchoose_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New System.Drawing.Point(SetupForm.Location.X, SetupForm.Location.Y + SetupForm.Height)
    End Sub
End Class
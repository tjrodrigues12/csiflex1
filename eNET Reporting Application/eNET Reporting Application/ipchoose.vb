Imports System.Reflection
Imports System.IO

Public Class ipchoose

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim path As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)

        Try

            If ListBox1.SelectedItem Is Nothing Then
                MessageBox.Show("You have to select an IP in the list.", "Bad use", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Else
                SetupForm.TextBox4.Text = ListBox1.SelectedItem.ToString
                '      SetupForm.TextBox6.Text = ListBox1.SelectedItem.ToString.Split(":")(0)
            End If




        Catch ex As Exception
            MessageBox.Show("CSIFLEX has  encountered an error while saving the IP adress : " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Close()
    End Sub

    Private Sub ipchoose_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New System.Drawing.Point(SetupForm.Location.X, SetupForm.Location.Y)
    End Sub
End Class
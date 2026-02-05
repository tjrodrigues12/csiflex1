Imports System.IO
Public Class Config

    'Dim FTPFILENAME As String = "CSI.FTP"
    Dim CONFIGFILENAME As String = "enetsimconfig.ini"

    Private Sub BTN_Save_Click(sender As Object, e As EventArgs) Handles BTN_Save.Click
        Try
            If (CheckConfig()) Then
                Dim filename As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\" & CONFIGFILENAME
                Dim lines(2) As String

                lines(0) = "ENETFTPIP:" & TB_IP.Text
                lines(1) = "ENETFTPPWD:" & TB_Pwd.Text
                lines(2) = "ENETPATH:" & TB_Path.Text

                File.WriteAllLines(filename, lines)

                Dispose()
            Else
                MessageBox.Show("Please input valid IP and path to enet folder", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            System.Console.WriteLine("Error writing configuration file")
        End Try
    End Sub

    Private Function CheckConfig() As Boolean
        Dim config As Boolean = False

        If (TB_IP.Text.Length > 0) And (TB_Path.Text.Length > 0) Then
            config = True
        End If

        Return config
    End Function

    Private Sub BTN_EnetPathBrowse_Click(sender As Object, e As EventArgs) Handles BTN_EnetPathBrowse.Click
        Dim fbd As New FolderBrowserDialog
        fbd.SelectedPath = "C:\_eNETDNC"
        fbd.ShowNewFolderButton = False

        If (fbd.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            TB_Path.Text = fbd.SelectedPath
        End If

    End Sub
End Class
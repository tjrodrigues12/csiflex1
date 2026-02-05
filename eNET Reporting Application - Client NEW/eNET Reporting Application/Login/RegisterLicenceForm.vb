Imports System.IO

Public Class RegisterLicenceForm

    Private Sub BTN_Accept_Click(sender As Object, e As EventArgs) Handles BTN_Accept.Click
        'write code in file, check code after
        If TB_LicenseKey.TextLength > 0 Then

            Dim rootpath As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)

            'If Not (Directory.Exists(rootpath.DirectoryName & "\sys")) Then
            '    Directory.CreateDirectory(rootpath.DirectoryName & "\sys")
            'End If
            Using writer As StreamWriter = New StreamWriter(rootpath.DirectoryName & "\wincsi.dl1", False) '\CSIFLIC.clf
                writer.Write(TB_LicenseKey.Text.Trim())
                writer.Close()
            End Using
            DialogResult = Windows.Forms.DialogResult.OK


            'Dim rootpath As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)

            'If Not (Directory.Exists(rootpath.DirectoryName & "\sys")) Then
            '    Directory.CreateDirectory(rootpath.DirectoryName & "\sys")
            'End If
            'Using writer As StreamWriter = New StreamWriter(rootpath.DirectoryName & "\sys\CSIFLIC.clf", True)
            '    writer.Write(TB_LicenseKey.Text.Trim())
            '    writer.Close()
            'End Using
            'DialogResult = Windows.Forms.DialogResult.OK

            MessageBox.Show("Please restart the software to register your license.")

            Dispose()

        End If
    End Sub
End Class
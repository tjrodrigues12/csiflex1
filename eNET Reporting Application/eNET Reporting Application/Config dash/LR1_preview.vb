Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Renci.SshNet

Public Class LR1_preview
    Dim path As String = CSI_Library.CSI_Library.serverRootPath & "\temp\"

    Private Sub LR1_preview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LBL_stat.Text = "Connecting to the device ..."
        PB_wait.Visible = True
        If prev IsNot Nothing Then prev.Dispose()

        If path.EndsWith("LR1preview.png") Then
            If File.Exists(path) Then
                File.Delete(path)
            End If
        Else
            If File.Exists(path + "LR1preview.png") Then
                File.Delete(path + "LR1preview.png")
            End If
        End If


    End Sub


    Private Sub LR1_preview_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        PB_wait.Location = New Point(Width / 2 - 100, Height / 2 - 100)
        LBL_stat.Location = New Point(Width / 2 - 100, 10)
        If BG_capture.IsBusy Then
        Else
            BG_capture.RunWorkerAsync(SetupForm2.IP_TB.Text)
        End If


    End Sub


    Private Function convert_mac_to_ip(mac As String) As String

        Dim startInfo As New ProcessStartInfo()
        startInfo.CreateNoWindow = True
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardOutput = True
        startInfo.FileName = "arp"
        startInfo.Arguments = "-a"

        Try
            Dim process__1 As Process = Process.Start(startInfo)
            Dim out As String = ""
            Dim splitted_out As String()

            While Not process__1.StandardOutput.EndOfStream
                out = process__1.StandardOutput.ReadLine()
                out = Regex.Replace(out, "\s+", " ")
                splitted_out = out.Split(" ")
                If splitted_out.Count > 1 Then
                    If splitted_out(2) = mac.ToLower Then Return splitted_out(1)
                End If
            End While
        Catch ex As Exception
            MessageBox.Show("Could not find the ip address associated with the device MAC address: " & ex.Message)
            Return "0.0.0.0" 'error
        Finally

        End Try
        Return "0.0.0.0" 'error
    End Function
    Private Sub LR1_preview_cloing(sender As Object, e As EventArgs) Handles MyBase.Closing
        Try
            PB_prev.Image = Nothing
            prev.Dispose()
            If path.EndsWith("LR1preview.png") Then
                If File.Exists(path) Then
                    File.Delete(path)
                End If
            Else
                If File.Exists(path + "LR1preview.png") Then
                    File.Delete(path + "LR1preview.png")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub LR1_preview_close(sender As Object, e As EventArgs) Handles MyBase.Closed
        'Try
        '    If PB_prev.Image IsNot Nothing Then
        '        PB_prev.Image.Dispose()
        '    End If

        '    If File.Exists(path) Then File.Delete(path)
        'Catch ex As Exception

        'End Try
        If BG_capture.IsBusy Then
            BG_capture.CancelAsync()
        Else

        End If
    End Sub
    Private Function connect_and_DL(host As String)
        Dim client As New SshClient(host, "pi", "raspberry")
        Try
            ' Dim host As String = SetupForm2.IP_TB.Text
            ' host = "10.0.10.123" ' debug
            ' BG_capture.ReportProgress(0)

            'client = New SshClient(host, "pi", "raspberry")
            client.Connect()
            'Using cmd = client.CreateCommand("DISPLAY=:0.0 import -window root /tmp/LR1preview.png")
            Using cmd = client.CreateCommand("DISPLAY=:0.0 scrot LR1preview.png")
                cmd.Execute()

                Console.WriteLine("Command>" + cmd.CommandText)
                Console.WriteLine("Return Value = {0}", cmd.ExitStatus)
            End Using
            client.Disconnect()
            client.Dispose()
            BG_capture.ReportProgress(1)
            DownloadFile_viaSFP(host)
            BG_capture.ReportProgress(2)

            Return Nothing
        Catch ex As Exception
            MessageBox.Show("Device can't be found : " & ex.Message)
            If client IsNot Nothing Then
                client.Disconnect()
                client.Dispose()
            End If
            Return Nothing
        End Try
    End Function


    Public Sub DownloadFile_viaSFP(host As String)

        Dim username As String = "pi"
        Dim password As String = "raspberry"
        Dim localFileName As String = "LR1preview.png" 'System.IO.Path.GetFileName(localFile)
        Dim remoteFileName As String = "LR1preview.png"
        Dim remoteDirectory As String = "/tmp/"

        Try
            path = CSI_Library.CSI_Library.serverRootPath & "\temp\"
            If Not Directory.Exists(path) Then Directory.CreateDirectory(path)
            path = path & "LR1preview.png"
            If File.Exists(path) Then File.Delete(path)

            Using sftp = New SftpClient(host, username, password)
                sftp.Connect()

                Using file__1 = File.OpenWrite(path)
                    sftp.DownloadFile(remoteFileName, file__1)
                End Using
                'Delete File on Raspberry
                sftp.DeleteFile(remoteFileName)
                sftp.Disconnect()
            End Using
        Catch ex As Exception
            MessageBox.Show("Could not transfer the preview : " & ex.Message)
        End Try
    End Sub

    Private Sub BG_capture_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BG_capture.DoWork
        Try

            Dim DeviceIP As String '= SetupForm2.IP_TB.Text
            DeviceIP = DirectCast(e.Argument, String)
            If DeviceIP.Split("-").Count = 6 Then
                DeviceIP = convert_mac_to_ip(DeviceIP)
            End If
            If DeviceIP <> "0.0.0.0" And DeviceIP <> "" Then
                connect_and_DL(DeviceIP)
                ' Dim T = Task.Factory.StartNew(Function() connect_and_DL(DeviceIP))
                ' T.Wait()
            Else
                MessageBox.Show("The ip/Mac address is bad.")
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Could not show the preview : " & ex.Message)
        Finally
            '  If T IsNot Nothing Then T.Dispose()
        End Try
    End Sub
    Dim prev As Bitmap
    Private Sub BG_capture_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BG_capture.RunWorkerCompleted
        Try
            PB_wait.Visible = False
            If path.EndsWith("LR1preview.png") Then

                PB_prev.SizeMode = PictureBoxSizeMode.Zoom
                prev = New Bitmap(path)
                PB_prev.Image = CType(prev, Image)
                Me.BackColor = Color.Black
            End If
        Catch ex As Exception
            MessageBox.Show("Could not show the preview : " & ex.Message)
        End Try
    End Sub
    Private Sub BG_capture_progress(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BG_capture.ProgressChanged
        If e.ProgressPercentage = 1 Then
            LBL_stat.Text = "Downloading the preview ... "
        ElseIf e.ProgressPercentage = 2 Then
            LBL_stat.Text = ""
        End If
    End Sub

    Private Sub LR1_preview_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        PB_wait.Location = New Point(Width / 2 - 100, Height / 2 - 100)
        LBL_stat.Location = New Point(Width / 2 - 100, 10)
    End Sub
End Class
Imports System.IO
Imports System.Net
Imports System.Threading

Public Class Form1


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        thread_srv.RunWorkerAsync()
    End Sub

    Dim context As HttpListenerContext
    Dim srv As New HttpListener



    Private Sub Run_srv(port_ As Integer)

        Try
            srv.Prefixes.Add("http://+:" & port_ & "/")
            srv.Start()
            rm_server_listen()

            srv.Stop()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Private Async Sub rm_server_listen()
        While True
            Dim context = srv.GetContext()
            Console.WriteLine("Client connected")
            Task.Factory.StartNew(Function() Process(context))
        End While

        srv.Close()
    End Sub


    Private Function Process(context As HttpListenerContext)

        Dim request As HttpListenerRequest = context.Request
        Dim response As HttpListenerResponse = context.Response

        Dim tmp As String
        Dim output As System.IO.Stream

        Using r As StreamReader = New StreamReader("C:\Users\asalm\Desktop\test.txt", False)
            'Dim lib___ As New CSI_Library.CSI_Library
            tmp = r.ReadToEnd
            r.Close()
        End Using

        Dim responseString As String = tmp

        Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(responseString)
        response.ContentLength64 = buffer.Length

        output = response.OutputStream
        output.Write(buffer, 0, buffer.Length)
        Thread.Sleep(100)
        output.Close()

        Return True
    End Function





    Private Sub thread_srv_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles thread_srv.DoWork
        Run_srv(8008)
    End Sub
    Private Sub thread_srv_complete(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles thread_srv.RunWorkerCompleted
        '  run_thread()
    End Sub

    Private Sub run_thread()
        '    thread_srv.RunWorkerAsync()
    End Sub

End Class

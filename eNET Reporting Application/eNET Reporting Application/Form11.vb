Imports CSI_Library
Imports System.Threading

Public Class Form12

    Public CSI_Lib As CSI_Library.CSI_Library



    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load





        Me.Visible = True
        Me.Location = New Point(100, 100)

        'Dim t As Task
        't = Task.Factory.StartNew(AddressOf wait_)



        'Dim _thread As Thread
        '_thread = New Thread(AddressOf wait_)
        '_thread.Priority = ThreadPriority.AboveNormal
        '_thread.IsBackground = True
        '_thread.Start()

    End Sub



    Private Sub wait_()
        'CSI_Lib = New CSI_Library.CSI_Library

        'While CSI_Library.CSI_Library.stat_ < 100
        '    Thread.Sleep(3000)
        '        ProgressBar1.Value = CSI_Library.CSI_Library.stat_

        '    TextBox2.Text = CSI_Library.CSI_Library.stat_s
        'End While
    End Sub
End Class
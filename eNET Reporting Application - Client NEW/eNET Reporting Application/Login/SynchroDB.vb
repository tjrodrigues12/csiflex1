Imports System.ComponentModel
Imports System.IO
Imports System
Imports System.Windows
Imports System.Data.OleDb

Public Class SynchroDB

    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath


    Private Sub SynchroDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If Welcome.CSIF_version <> 1 And Welcome.CSIF_version <> 2 Then
        '    BackgroundWorker1.WorkerReportsProgress = True
        '    BackgroundWorker1.WorkerSupportsCancellation = True
        '    If BackgroundWorker1.IsBusy <> True Then
        '        ' Start the asynchronous operation.
        '        BackgroundWorker1.RunWorkerAsync()
        '    End If
        'End If

    End Sub

    ' Private Sub cancelAsyncButton_Click(ByVal sender As System.Object, _
    'ByVal e As System.EventArgs) Handles cancelAsyncButton.Click
    '     If BackgroundWorker1.WorkerSupportsCancellation = True Then

    '         ' Cancel the asynchronous operation.
    '         ' BackgroundWorker1.CancelAsync()
    '     End If
    '     'Me.Close()
    ' End Sub

    ' This event handler is where the time-consuming work is done. 
    Private Sub backgroundWorker1_DoWork(ByVal sender As System.Object, _
    ByVal e As DoWorkEventArgs)
        ' Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        'synchroDBuser()
        ' createReplicaOfDB()
        'If File.Exists(rootPath & "\sys\Setupdb_.csys") Then
        '    Using writer As StreamReader = New StreamReader(rootPath & "\sys\Setupdb_.csys")
        '        Welcome.to_ = writer.ReadLine()
        '    End Using
        '    File.Copy(Welcome.from_ & "/CSI_Database.mdb", Welcome.to_ & "/CSI_Database.mdb", True)
        'End If

    End Sub


    ' This event handler updates the progress. 
    Private Sub backgroundWorker1_ProgressChanged(ByVal sender As System.Object, _
    ByVal e As ProgressChangedEventArgs)
        ' Label2.Text = (e.ProgressPercentage.ToString() + "%")
    End Sub

    ' This event handler deals with the results of the background operation. 
    Private Sub backgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, _
    ByVal e As RunWorkerCompletedEventArgs)
        If e.Cancelled = True Then
            Label2.Text = "Canceled!"
            Drop_DB()

        ElseIf e.Error IsNot Nothing Then
            Label2.Text = "Error: " & e.Error.Message
            Drop_DB()
        Else
            Label2.Text = "Done!, The database has been synchronized"
            Reporting_application.First_date = Reporting_application.set_firstdate()
            Me.Close()

        End If
    End Sub

    Private Sub Drop_DB()
        If File.Exists(rootPath & "\sys\csisqlite.db3") Then File.Delete(rootPath & "\sys\csisqlite.db3")
    End Sub

    Private Sub ok_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub SynchroDB_closeed(sender As Object, e As EventArgs) Handles MyBase.FormClosed
        Config_report.Show()
    End Sub

    Private Sub cancelAsyncButton_Click(sender As Object, e As EventArgs) Handles cancelAsyncButton.Click

        Me.Close()

    End Sub

    '#Region "form move"


    '    '-----------------------------------------------------------------------------------------------------------------------
    '    ' MOVE FORME 
    '    '  
    '    '-----------------------------------------------------------------------------------------------------------------------

    '    Dim _MouseDown As Boolean, _MouseX As Integer, _MouseY As Integer
    '    Private Sub form3_mouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
    '        _MouseDown = True
    '        _MouseX = e.X
    '        _MouseY = e.Y
    '    End Sub

    '    Private Sub Form3_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
    '        _MouseDown = False
    '    End Sub


    '    Private Sub Form3_Mousemove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove


    '        If _MouseDown = True Then
    '            Reporting_application.SuspendLayout()
    '            Me.Left = Me.Left + (e.X - _MouseX)
    '            Me.Top = Me.Top + (e.Y - _MouseY)
    '            If Me.Top < 20 Then Me.Top = 0
    '            If Me.Left < 20 Then Me.Left = 0
    '            Reporting_application.ResumeLayout(True)

    '        End If

    '    End Sub
    '#End Region

End Class
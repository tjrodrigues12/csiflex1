Imports System.Threading
Imports System.Windows.Forms

Public Class Service1

    Dim ReportGenerator As New Report_Generator()

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        'System.Diagnostics.Debugger.Launch()

        'Thread.Sleep(30000)

        System.Diagnostics.Debugger.Launch()

        ReportGenerator.StartReportingServiceThread()

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        ReportGenerator.StopReportServiceThread()
    End Sub

End Class

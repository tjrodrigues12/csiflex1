Imports System.Threading
Imports System.Timers

Public Class CSIFlexServerService

    Dim servicelib As New ServiceLibrary()

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        'servicelib.WriteErrorLog("Service started")

        'Debugger.Launch()
        'System.Diagnostics.Debugger.Launch()

        servicelib.StartThreadsAsync()

        'servicelib.WriteErrorLog("thread started")        
    End Sub


    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        'servicelib.WriteErrorLog("Service stopped")

        servicelib.StopThreads()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

End Class

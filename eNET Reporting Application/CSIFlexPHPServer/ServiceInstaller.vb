Imports System.ComponentModel
Imports System.ServiceProcess
Imports System.Configuration.Install
<RunInstallerAttribute(True)>Public Class MyInstaller
    Inherits Installer
    Dim serviceInstaller As ServiceInstaller
    Dim processInstaller As ServiceProcessInstaller
    Public Sub New()
        MyBase.New()

        serviceInstaller = New ServiceInstaller()
        processInstaller = New ServiceProcessInstaller()

        processInstaller.Account = ServiceAccount.LocalSystem
        serviceInstaller.StartType = ServiceStartMode.Automatic
        serviceInstaller.ServiceName = "CSIFlexMobileServer"
        Installers.Add(processInstaller)
        Installers.Add(serviceInstaller)

    End Sub

End Class

Imports System.IO
Imports CSIFLEX.Server.Settings
Imports CSIFLEX.Utilities

Public Class Services

    Dim services As List(Of ServStatus)

    Dim timer As Timer


    Sub New()

        services = New List(Of ServStatus)()
        services.Add(New ServStatus("CSIFlexServerService", ServiceTools.ServiceState.Unknown))
        services.Add(New ServStatus("CSIFlex_Reports_Generator_Service", ServiceTools.ServiceState.Unknown))
        services.Add(New ServStatus("CSIFlexMBServer", ServiceTools.ServiceState.Unknown))
        services.Add(New ServStatus("CSIFLEX.WebApp.Service", ServiceTools.ServiceState.Unknown))

        'For Each pair In services
        '    RaiseEvent StatusChanged(pair.Key, ServiceTools.ServiceInstaller.GetServiceStatus(pair.Key))
        'Next

        timer = New Timer()
        timer.Interval = 800

        AddHandler timer.Tick, AddressOf CheckStatus
        timer.Start()

    End Sub


    Public Event StatusChanged(serviceName As String, newStatus As ServiceTools.ServiceState)


    Private Sub CheckStatus()

        Dim newStatus As ServiceTools.ServiceState

        For Each pair In services
            newStatus = ServiceTools.ServiceInstaller.GetServiceStatus(pair.Key)

            If Not newStatus = pair.Value Then
                pair.Value = newStatus
                RaiseEvent StatusChanged(pair.Key, pair.Value)
            End If
        Next

    End Sub


    Public Shared Function CheckStatus(service As String) As ServiceTools.ServiceState

        Return ServiceTools.ServiceInstaller.GetServiceStatus(service)

    End Function


    Public Function IsServiceInstalled(service As String) As Boolean

        Return ServiceTools.ServiceInstaller.ServiceIsInstalled(service)

    End Function


    Public Function IsServiceRunning(service As String) As Boolean

        If (ServiceTools.ServiceInstaller.ServiceIsInstalled(service)) Then

            If (ServiceTools.ServiceInstaller.GetServiceStatus(service) = ServiceTools.ServiceState.Run) Or
                   (ServiceTools.ServiceInstaller.GetServiceStatus(service) = ServiceTools.ServiceState.Starting) Then

                Return True

            Else

                Return False

            End If

        Else

            Return False

        End If

    End Function


    Public Sub InstallAndStartService(service As String)

        Try
            Dim serviceExecPath As String = String.Empty

            If service = "CSIFlexServerService" Then
                serviceExecPath = AppDomain.CurrentDomain.BaseDirectory + "CSIFlexServerService.exe"

            ElseIf service = "CSIFlex_Reports_Generator_Service" Then
                serviceExecPath = AppDomain.CurrentDomain.BaseDirectory + "CSIFLEX.Reporting.Service.exe"

            ElseIf service = "CSIFlexMBServer" Then
                serviceExecPath = Path.Combine("C:\\CSIFLEX", "MonitoringBoardsServer", "MB.Web.exe")

            ElseIf service = "CSIFLEX.WebApp.Service" Then
                serviceExecPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CSIFLEX", "CSIFLEX.WebApp", "CSIFLEX.WebApp.exe")

                Dim configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CSIFLEX", "CSIFLEX.config.json")

                If File.Exists(configFile) Then

                    Log.Debug($"*** ServerSettings.ServerIPAddress: {ServerSettings.ServerIPAddress}")

                    Dim text = File.ReadAllText(configFile)

                    If text.Contains("<IPADDRESS>") Then

                        Dim ipAddress = ""
                        Dim counter = 0
                        Do
                            ipAddress = ServerSettings.ServerIPAddress

                            If Not String.IsNullOrEmpty(ipAddress) Then
                                text = text.Replace("<IPADDRESS>", ServerSettings.ServerIPAddress)
                                File.WriteAllText(configFile, text)
                                Exit Do
                            End If
                            counter += 1
                            Threading.Thread.Sleep(100)
                        Loop While counter < 20

                    End If
                End If

            Else
                Return

            End If

            'Add your code here for Start Service On StartUp
            InstallAndStartService(service, serviceExecPath)

        Catch ex As Exception

            MessageBox.Show("There was an error trying to start the Main service. See the log for more details :" & ex.Message)

            Log.Error("There was an error trying to start the Main service. See the log for more details", ex)

        End Try

    End Sub


    Public Sub InstallAndStartService(service As String, serviceExecPath As String)

        If (ServiceTools.ServiceInstaller.ServiceIsInstalled(service)) Then

            If (ServiceTools.ServiceInstaller.GetServiceStatus(service) = ServiceTools.ServiceState.Run) Or
                   (ServiceTools.ServiceInstaller.GetServiceStatus(service) = ServiceTools.ServiceState.Starting) Then

                Return

            Else
                ServiceTools.ServiceInstaller.StartService(service)
                ServiceTools.ServiceInstaller.SetDelayedStart(service)
            End If

        Else

            ServiceTools.ServiceInstaller.InstallAndStart(service, service, serviceExecPath)
            ServiceTools.ServiceInstaller.SetDelayedStart(service)

        End If

    End Sub


    Public Sub StopService(service As String)

        Try

            If (ServiceTools.ServiceInstaller.ServiceIsInstalled(service)) Then

                If (ServiceTools.ServiceInstaller.GetServiceStatus(service) = ServiceTools.ServiceState.Run) Then

                    CSI_Lib.KillingAProcess(service)

                End If
            End If

        Catch ex As Exception

            Log.Error(ex)
            MessageBox.Show("There was an error trying to start the Main service. See the log for more details")

        End Try
    End Sub


End Class


Public Class ServStatus

    Private Key_ As String
    Public Property Key() As String
        Get
            Return Key_
        End Get
        Set(ByVal value As String)
            Key_ = value
        End Set
    End Property

    Private Value_ As String
    Public Property Value() As ServiceTools.ServiceState
        Get
            Return Value_
        End Get
        Set(ByVal value As ServiceTools.ServiceState)
            Value_ = value
        End Set
    End Property

    Sub New(key As String, value As ServiceTools.ServiceState)
        Key_ = key
        Value_ = value
    End Sub

End Class

Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.ServiceProcess
Imports System.Xml
Imports MySql.Data.MySqlClient

Public Class FocasInstaller
    Public objMtcFocas As MtcFocasADD()
    Public BothRunnig As Boolean = False
    'Fuction to check if IP is already in use or not 
    Public Function PortInUse(ByVal port As Integer) As Boolean
        Dim inUse As Boolean = False
        Dim ipProperties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
        Dim ipEndPoints As IPEndPoint() = ipProperties.GetActiveTcpListeners()
        For Each endPoint As IPEndPoint In ipEndPoints
            If endPoint.Port = port Then
                inUse = True
                Exit For
            End If
        Next
        Return inUse
    End Function
    'Function to Ping the IP address with port (ex.10.0.10.189:1234)
    Public Function Ping(IP As String, FPort As Integer) As Boolean
        '        Dim res As Boolean = False
        Dim PingisSuccess As Boolean
        PingisSuccess = False
        Try
            'Check ip ping 
            If My.Computer.Network.Ping(IP) Then
                'Success 
                If PortInUse(FPort) Then
                    'Port already in use
                    'Use the next available port ++ to 8193 
                    'Dim OldFocasPort = FPort
                    MessageBox.Show("Port : " & Convert.ToString(FPort) & " is already in use Please Choose another port!")
                Else
                    MessageBox.Show("Ping Successful : " & IP)
                    PingisSuccess = True
                End If
            Else
                'Ping Fail
                MessageBox.Show("Ping Fail to : " & IP)
            End If
        Catch ex As Exception
            MessageBox.Show("Machine IP Address is not a valid IP !")
            CSI_Lib.LogServiceError("Error In IP Ping :" & ex.ToString(), 1)
        End Try
        Return PingisSuccess
    End Function
    Public Sub CopyLibraryFiles(controllerType As String)
        'Old line Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\focasadapters"
        Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\focasadapters"
        'Common Logic will be "*<ControllerType>.dll"
        Dim dest As String = Environment.SystemDirectory
        ' need to copy three .dll files
        Dim files As String() = System.IO.Directory.GetFiles(path, "*" & controllerType & ".dll")
        'MessageBox.Show("Total Files found : " & files.Length.ToString())
        For Each file As String In files
            Dim FileName As String = System.IO.Path.GetFileName(file)
            System.IO.File.Copy(file, dest & "\" & FileName, True)
        Next
        Dim library32bit As String = System.IO.Path.Combine(path, "Fwlib32.dll")
        Dim library64bit As String = System.IO.Path.Combine(path, "Fwlib64.dll")
        System.IO.File.Copy(library32bit, dest & "\Fwlib32.dll", True)
        System.IO.File.Copy(library64bit, dest & "\Fwlib64.dll", True)
    End Sub
    Public Sub AdapterSupportingFiles(MachineName As String, ContrrollerType As String, AdapterPort As String, FocasIPAddress As String, FocasPort As String, AgentIP As String)
        Try
            Dim focasAdapterFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\focasadapters\"
            My.Computer.FileSystem.CopyFile(focasAdapterFolder & "adapter.ini", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Adapter\adapter.ini", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            My.Computer.FileSystem.CopyFile(focasAdapterFolder & "fanuc_" & ContrrollerType & ".exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Adapter\" & MachineName & "_fanuc_" & ContrrollerType & ".exe", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            If System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Adapter\adapter.ini") Then
                Dim adapterlines As String() = IO.File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Adapter\adapter.ini")
                For i As Integer = 0 To adapterlines.Length - 1
                    If adapterlines(i).Contains("port =") Then
                        'this port is 7878 and ++ (Write Logic)
                        adapterlines(i) = "port = " + AdapterPort
                    ElseIf adapterlines(i).Contains("service =") Then
                        adapterlines(i) = "service = MTCFocasAdapter-" + MachineName
                    ElseIf adapterlines(i).Contains("port1 = <FocasPort>") Then
                        adapterlines(i) = "port = " + FocasPort
                    ElseIf adapterlines(i).Contains("host =") Then
                        adapterlines(i) = "host = " + FocasIPAddress 'This host is the Adapter Machine IP Address 
                    End If
                Next
                'Write all new lines in .ini file
                IO.File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Adapter\adapter.ini", adapterlines)
            End If
        Catch ex As Exception
            MessageBox.Show("Adapter Folder Supporting File Error : " & ex.Message())
            CSI_Lib.LogServerError("Adapter Folder Supporting File Error : " & ex.StackTrace(), 1)
        End Try
    End Sub
    Public Sub AgentSuppotrtingFiles(MachineName As String, ControllerType As String, AgentIP As String, AgentPort As String, AdapterIP As String, AdapterPort As String, Manufacturer As String)
        Try
            Dim focasAgentFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\focasagent\"
            'Copy whole Agent Directory
            My.Computer.FileSystem.CopyDirectory(focasAgentFolder, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Agent\", False)
            'Rename .xml File name as per ControllerType
            My.Computer.FileSystem.RenameFile(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Agent\fanuc.xml", "Fanuc" & ControllerType & ".xml")
            Dim XmlFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Agent\Fanuc" & ControllerType & ".xml"
            XmlFileDataChanger(XmlFilePath, MachineName, Manufacturer)
            If System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Agent\agent.cfg") Then
                Dim agentlines As String() = IO.File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Agent\agent.cfg")
                For j As Integer = 0 To agentlines.Length - 1
                    If agentlines(j).Contains("Devices =") Then
                        'Set name of the xml file will be Fanuc0i.xml
                        agentlines(j) = "Devices = Fanuc" + ControllerType + ".xml"
                    ElseIf agentlines(j).Contains("Port =") Then
                        'this port is 5000 and ++
                        agentlines(j) = "Port = " + AgentPort
                    ElseIf agentlines(j).Contains("ServiceName =") Then
                        agentlines(j) = "ServiceName = MTCAgent-" + MachineName
                    ElseIf agentlines(j).Contains("Fanuc {") Then
                        agentlines(j) = Manufacturer + " {"
                    ElseIf agentlines(j).Contains("Device1 =") Then
                        agentlines(j) = "Device = " + MachineName
                    ElseIf agentlines(j).Contains("Host1 =") Then
                        'This should be the localhost or IP Address on which port 8008 is running 
                        agentlines(j) = "Host = " + AgentIP   'This host is the Agent Machine IP Address(By default it is a localhost IP address on which CSIFlex Server App is running) 
                    ElseIf agentlines(j).Contains("Port1 =") Then
                        ' This port is 7878 and ++ (Write Logic)
                        agentlines(j) = "Port = " + AdapterPort
                    End If
                Next
                'Write all new lines to .cfg File
                IO.File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Agent\agent.cfg", agentlines)
            End If
        Catch ex As Exception
            MessageBox.Show("Agent Folder Supporting File Error : " & ex.Message())
            CSI_Lib.LogServerError("Agent Folder Supporting File Error : " & ex.StackTrace(), 1)
        End Try
    End Sub
    Public Sub XmlFileDataChanger(XmlFilePath As String, MachineName As String, Manufacturer As String)
        Dim XmlDoc As New XmlDocument()
        Dim file As New StreamReader(XmlFilePath)
        XmlDoc.Load(file)
        For Each Element As XmlElement In XmlDoc.DocumentElement
            If Element.Name = "Devices" Then
                For Each Element2 As XmlElement In Element
                    If Element2.Name = "Device" Then
                        Element2.Attributes(1).Value = MachineName
                        For Each Element3 As XmlElement In Element2
                            If Element3.Name = "Description" Then
                                Element3.Attributes(0).Value = Manufacturer
                            End If
                        Next
                    End If
                Next
            End If
        Next
        file.Dispose()
        file.Close()
        Dim save As New StreamWriter(XmlFilePath)
        XmlDoc.Save(save)
        save.Dispose()
        save.Close()
        'Dim output As String
        'output = Beautify(XmlDoc)
        'TB_DisplayFileContent.Text = output
    End Sub
    'Public Sub AdapterServiceInstaller(MachineName As String, ContrrollerType As String)
    '    'Adapter Service Install and StartUp Code
    '    Dim AdapterExePath As String = String.Empty
    '    AdapterExePath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Adapter\" & MachineName & "_fanuc_" & ContrrollerType & ".exe"
    '    If (ServiceTools.ServiceInstaller.ServiceIsInstalled("MTCFocasAdapter-" + MachineName)) Then 'AdapterServiceName
    '        While ServiceTools.ServiceInstaller.GetServiceStatus("MTCFocasAdapter-" + MachineName) = ServiceTools.ServiceState.Stop Or ServiceTools.ServiceInstaller.GetServiceStatus("MTCFocasAdapter-" + MachineName) = ServiceTools.ServiceState.Stopping Or ServiceTools.ServiceInstaller.GetServiceStatus("MTCFocasAdapter-" + MachineName) = ServiceTools.ServiceState.Starting
    '            ServiceTools.ServiceInstaller.StartService("MTCFocasAdapter-" + MachineName)
    '        End While
    '    Else
    '        'Service is not installed then install the service First and Then Start It After
    '        ServiceTools.ServiceInstaller.InstallAndStart("MTCFocasAdapter-" + MachineName, "MTCFocasAdapter-" + MachineName, AdapterExePath)
    '        While ServiceTools.ServiceInstaller.GetServiceStatus("mtcfocasadapter-" + MachineName) = ServiceTools.ServiceState.Stop Or ServiceTools.ServiceInstaller.GetServiceStatus("mtcfocasadapter-" + MachineName) = ServiceTools.ServiceState.Stopping Or ServiceTools.ServiceInstaller.GetServiceStatus("mtcfocasadapter-" + MachineName) = ServiceTools.ServiceState.Starting
    '            ServiceTools.ServiceInstaller.StartService("mtcfocasadapter-" + MachineName)
    '        End While
    '    End If
    'End Sub
    'Public Sub AgentServiceInstaller(MachineName As String)
    '    Dim AgentExePath As String = String.Empty
    '    AgentExePath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Agent\agent.exe"
    '    If (ServiceTools.ServiceInstaller.ServiceIsInstalled("MTCAgent-" + MachineName)) Then 'AgentServiceName
    '        While ServiceTools.ServiceInstaller.GetServiceStatus("MTCAgent-" + MachineName) = ServiceTools.ServiceState.Stop Or ServiceTools.ServiceInstaller.GetServiceStatus("MTCAgent-" + MachineName) = ServiceTools.ServiceState.Stopping Or ServiceTools.ServiceInstaller.GetServiceStatus("MTCAgent-" + MachineName) = ServiceTools.ServiceState.Starting
    '            ServiceTools.ServiceInstaller.StartService("MTCAgent-" + MachineName)
    '        End While
    '    Else
    '        'Service is not installed then install the service First and Then Start It After
    '        ServiceTools.ServiceInstaller.InstallAndStart("MTCAgent-" + MachineName, "MTCAgent-" + MachineName, AgentExePath)
    '        While ServiceTools.ServiceInstaller.GetServiceStatus("MTCAgent-" + MachineName) = ServiceTools.ServiceState.Stop Or ServiceTools.ServiceInstaller.GetServiceStatus("MTCAgent-" + MachineName) = ServiceTools.ServiceState.Stopping Or ServiceTools.ServiceInstaller.GetServiceStatus("MTCAgent-" + MachineName) = ServiceTools.ServiceState.Starting
    '            ServiceTools.ServiceInstaller.StartService("MTCAgent-" + MachineName)
    '        End While
    '    End If
    'End Sub

    'Public Function CheckBothServiceRunnig(MachineName As String, ControllerType As String, FocasIPAddress As String) As Boolean
    '    BothRunnig = False
    '    If ServiceTools.ServiceInstaller.GetServiceStatus("MTCAgent-" + MachineName) = ServiceTools.ServiceState.Run And ServiceTools.ServiceInstaller.GetServiceStatus("MTCFocasAdapter-" + MachineName) = ServiceTools.ServiceState.Run Then
    '        BothRunnig = True
    '        'MessageBox.Show("Both Agent And Adapter is running properly !")
    '        Dim AgentExePath As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Agent\agent.exe"
    '        Dim AdapterExePath As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\CSI Flex Server\Connectors\" & MachineName & "\Adapter\" & MachineName & "_fanuc_" & ControllerType & ".exe"
    '        Dim mysqlConn As New MySqlConnection
    '        Dim mysqlCommand As New MySqlCommand
    '        mysqlConn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString
    '        mysqlConn.Open()
    '        mysqlCommand = New MySqlCommand("UPDATE CSI_auth.tbl_CSIConnector SET AgentServiceName = '" + "MTCAgent-" + MachineName + "', AgentExeLocation='" + AgentExePath + "',AdapterServiceName='" + "MTCFocasAdapter-" + MachineName + "',AdapterExeLocation='" + AdapterExePath + "' WHERE MachineName = '" & MachineName & "' and MachineIP = '" & FocasIPAddress & "' ", mysqlConn)
    '        mysqlCommand.ExecuteNonQuery()
    '        mysqlConn.Close()
    '        Return BothRunnig
    '    Else
    '        'MessageBox.Show("Services are not running properly !")
    '        'If services are not running try to start it again 
    '        'The below line is a monkey businness :)
    '        AdapterServiceInstaller(MachineName, ControllerType)
    '        AgentServiceInstaller(MachineName)
    '        Return BothRunnig
    '    End If
    'End Function
End Class

Imports System.IO
Imports System.Runtime.Serialization.Json
Imports CSIFLEX.Database.Access
Imports Newtonsoft.Json
Imports CSIFLEX.Utilities

Public Class CSIFLEXSettings

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Instance As CSIFLEXSettings
        Get
            Static INST As CSIFLEXSettings = New CSIFLEXSettings
            Return INST
        End Get
    End Property

    Private autoScroll_ As Boolean = True
    Public ReadOnly Property AutoScroll() As Boolean
        Get
            Return autoScroll_
        End Get
    End Property

    Private targetLine_ As Integer
    Public Property TargetLine() As Integer
        Set(value As Integer)
            targetLine_ = value
        End Set
        Get
            Return targetLine_
        End Get
    End Property

    Private targetColor_ As Integer
    Public Property TargetColor() As Integer
        Set(value As Integer)
            targetColor_ = value
        End Set
        Get
            Return targetColor_
        End Get
    End Property

    Private otherColor_ As Integer
    Public Property OtherColor() As Integer
        Set(value As Integer)
            otherColor_ = value
        End Set
        Get
            Return otherColor_
        End Get
    End Property

    Private databaseIp_ As String = ""
    Public Property DatabaseIp() As String
        Set(value As String)
            databaseIp_ = value
        End Set
        Get
            Return databaseIp_
        End Get
    End Property

    Private refreshRate_ As Integer = 1000
    Public Property RefreshRate() As Integer
        Set(value As Integer)
            refreshRate_ = value
        End Set
        Get
            Return refreshRate_ / 1000
        End Get
    End Property

    Private firstDayWeek_ As Integer = 1
    Public Property FirstDayOfWeek() As Integer
        Get
            Return firstDayWeek_
        End Get
        Set(ByVal value As Integer)
            firstDayWeek_ = value
        End Set
    End Property

    Private lastDayWeek_ As Integer = 5
    Public Property LastDayOfWeek() As Integer
        Get
            Return lastDayWeek_
        End Get
        Set(ByVal value As Integer)
            lastDayWeek_ = value
        End Set
    End Property

    Private firstMonthYear_ As Integer = 1
    Public Property FirstMonthOfYear() As Integer
        Get
            Return firstMonthYear_
        End Get
        Set(ByVal value As Integer)
            firstMonthYear_ = value
        End Set
    End Property

    Private reportFolder_ As String
    Public Property ReportFolder() As String
        Set(value As String)
            reportFolder_ = value
        End Set
        Get
            Return reportFolder_
        End Get
    End Property

    Private reportTemplatesFolder_ As String = ""
    Public ReadOnly Property ReportTemplatesFolder() As String
        Get
            Return reportTemplatesFolder_
        End Get
    End Property

    Private connectionString_ As String = ""
    Public ReadOnly Property ConnectionString() As String
        Get
            Return $"SERVER={databaseIp_};user=client;password=csiflex123;port=3306;"
        End Get
    End Property

    Private eNetIPAddress_ As String = ""
    Public Property EnetIPAddress() As String
        Set(value As String)
            eNetIPAddress_ = value
        End Set
        Get
            Return eNetIPAddress_
        End Get
    End Property

    Private eNetIPPort_ As Integer = 80
    Public Property EnetIPPort() As Integer
        Set(value As Integer)
            eNetIPPort_ = value
        End Set
        Get
            Return eNetIPPort_
        End Get
    End Property

    Private userName_ As String = ""
    Public Property UserName() As String

        Set(value As String)
            userName_ = value

            Try
                MachinesIdNames = New Dictionary(Of Integer, Tuple(Of String, String, String))()

                'Log.Info($"Connection - {ConnectionString}")
                Dim dtMachines As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1;", ConnectionString)

                For Each row As DataRow In dtMachines.Rows
                    Log.Info($"-Machine: {row("id")}, {row("Machine_Name")}")
                    MachinesIdNames.Add(row("id"), New Tuple(Of String, String, String)(row("Machine_Name"), row("EnetMachineName"), Utilities.MachineDbTableName(row("EnetMachineName"))))
                Next
            Catch ex As Exception
                Log.Error("1", ex)
            End Try

            Try
                UserMachines = New Dictionary(Of Integer, Tuple(Of String, String))()

                Dim machines = MySqlAccess.ExecuteScalar($"SELECT machines FROM csi_auth.users WHERE username_ = '{userName_}';", ConnectionString).ToString().Split(",").Select(Function(m) m.Trim()).ToArray()

                Log.Info($"Machines: {String.Join(",", machines)}")

                If machines.Count() = 1 And machines(0).ToUpper().StartsWith("ALL") Then
                    Dim tbMchs = MySqlAccess.GetDataTable("SELECT Id FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1;")
                    Dim ltMchs = New List(Of String)()

                    For Each mch As DataRow In tbMchs.Rows
                        ltMchs.Add(mch("Id"))
                    Next
                    machines = ltMchs.ToArray()
                End If

                Try
                    Dim nullValue As Dictionary(Of Integer, Tuple(Of String, String, String)) = Nothing

                    For Each machine As String In machines

                        If MachinesIdNames.ContainsKey(machine) Then

                            Log.Info($"Machine: {machine}")

                            Dim machineIdName = MachinesIdNames.FirstOrDefault(Function(m) m.Key.ToString() = machine Or m.Value.Item1 = machine Or m.Value.Item2 = machine)

                            Log.Info($"MachineId.Key: {machineIdName.Key}")
                            Log.Info($"MachineId.Item1: {machineIdName.Value.Item1}")
                            Log.Info($"MachineId.Item2: {machineIdName.Value.Item2}")
                            Log.Info($"MachineId.Item3: {machineIdName.Value.Item3}")

                            If Not machineIdName.Equals(nullValue) Then

                                UserMachines.Add(machineIdName.Key, New Tuple(Of String, String)(machineIdName.Value.Item1, machineIdName.Value.Item2))

                            End If

                        End If
                    Next
                Catch ex As Exception
                    Log.Error("1.a", ex)
                End Try

            Catch ex As Exception
                Log.Error("2", ex)
            End Try


        End Set
        Get
            Return userName_
        End Get
    End Property

    Private password_ As String = ""
    Public Property Password() As String
        Set(value As String)
            password_ = value
        End Set
        Get
            Return password_
        End Get
    End Property

    Private startupDisplayType_ As Integer
    Public Property StartupDisplayType() As Integer
        Get
            Return startupDisplayType_
        End Get
        Set(ByVal value As Integer)
            startupDisplayType_ = value
        End Set
    End Property

    Private startupReportDays_ As Integer
    Public Property StartupReportDays() As Integer
        Get
            Return startupReportDays_
        End Get
        Set(ByVal value As Integer)
            startupReportDays_ = value
        End Set
    End Property

    Private startupMachines_ As List(Of String)
    Public Property StartupMachines() As List(Of String)
        Get
            Return startupMachines_
        End Get
        Set(ByVal value As List(Of String))
            startupMachines_ = value
        End Set
    End Property

    Public Shared Machines As List(Of String)
    Public Shared GroupMachines As List(Of String)
    Public Shared UserMachines As Dictionary(Of Integer, Tuple(Of String, String))
    Public Shared MachinesIdNames As Dictionary(Of Integer, Tuple(Of String, String, String))
    Public Shared StatusColors As Dictionary(Of String, Integer)

    Dim sysFile As String = "C:\CSIFLEX\CSIFLEXClientConfig.json"
    Dim clientSettings As ClientSettings

    Public Sub Init()

        clientSettings = New ClientSettings()
        startupMachines_ = New List(Of String)()

        If Not Directory.Exists("C:\CSIFLEX") Then
            Directory.CreateDirectory("C:\CSIFLEX")
        End If

        If Not File.Exists(sysFile) Then
            SaveSettings()
        End If

        Using Stream = New FileStream(sysFile, FileMode.Open)

            Dim Serializer = New DataContractJsonSerializer(GetType(ClientSettings))
            clientSettings = DirectCast(Serializer.ReadObject(Stream), ClientSettings)

            databaseIp_ = clientSettings.DatabaseIP
            userName_ = clientSettings.LoginUser
            password_ = clientSettings.Password
            eNetIPAddress_ = clientSettings.EnetIPAddress
            eNetIPPort_ = clientSettings.EnetIPPort
            reportFolder_ = clientSettings.ReportsFolder
            firstDayWeek_ = clientSettings.FirstDayOfWeek
            FirstMonthOfYear = clientSettings.FirstMonthOfYear
            lastDayWeek_ = clientSettings.LastDayOfWeek
            refreshRate_ = clientSettings.RefreshRate
            otherColor_ = clientSettings.OtherColor
            targetColor_ = clientSettings.TargetColor
            startupDisplayType_ = clientSettings.StartupDisplayType
            startupReportDays_ = clientSettings.StartupReportDays

            For Each mach In clientSettings.StartupMachines
                startupMachines_.Add(mach)
            Next

        End Using

    End Sub

    Public Sub SaveSettings()

        clientSettings.DatabaseIP = databaseIp_
        clientSettings.LoginUser = userName_
        clientSettings.Password = password_
        clientSettings.ReportsFolder = reportFolder_
        clientSettings.RefreshRate = refreshRate_
        clientSettings.FirstDayOfWeek = firstDayWeek_
        clientSettings.LastDayOfWeek = lastDayWeek_
        clientSettings.FirstMonthOfYear = FirstMonthOfYear
        clientSettings.OtherColor = otherColor_
        clientSettings.TargetColor = targetColor_
        clientSettings.EnetIPAddress = eNetIPAddress_
        clientSettings.EnetIPPort = eNetIPPort_
        clientSettings.StartupDisplayType = startupDisplayType_
        clientSettings.StartupReportDays = startupReportDays_
        clientSettings.StartupMachines = New List(Of String)()
        For Each mach In startupMachines_
            clientSettings.StartupMachines.Add(mach)
        Next
        File.WriteAllText(sysFile, JsonConvert.SerializeObject(clientSettings, Formatting.Indented))

    End Sub

End Class

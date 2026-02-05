Imports CSIFLEX.Database.Access

Public Class DashboardDevice

#Region "Properties"

#Region "tbl_devices"
    Public Property DeviceId As Integer = 0

    Public Property IpAddress As String = ""

    Public Property DeviceName As String = ""

    Public Property Machines As String = ""

    Public Property Groups As String = ""

#End Region

#Region "tbl_deviceConfig"

    Public Property Messages As Boolean = False

    Public Property PieChart As Boolean = True

    Public Property PieChartTime As String = "Weekly"

    Public Property RefreshBrowser As String = "refreshing"

    Public Property LiveStatusDelay As Boolean = True

    Public Property DetailLiveStatusDelay As String = "6"

    Public Property DateTime As Boolean = True

    Public Property CustomLogo As Boolean = False

    Public Property DetailCustomLogo As String = ""

    Public Property Temperature As Boolean = False

    Public Property Degree As String = "Fahrenheit"

    Public Property DetailTemperature As String = ""

    Public Property LastCycle As Boolean = True

    Public Property CurrentCycle As Boolean = True

    Public Property ElapsedTime As Boolean = True

    Public Property FullScreen As Boolean = False

    Public Property LiveStatusPosition As String = "right"

    Public Property IFrame As Boolean = False

    Public Property DetailIFrame As String = ""

    Public Property Rotation As Boolean = False

    Public Property PieChartBy As String = "ByMachines"

    Public Property DisplayWhat As String = "none"

    Public Property LogoBarHidden As Boolean = False

    Public Property DarkTheme As Boolean = False

#End Region

#Region "tbl_deviceconfig2"

    Public Property Timeline As Boolean = True

    Public Property Trends As Boolean = True

    Public Property TrendsPercent As Integer = 20

    Public Property TrendCompare As String = "shift"

    Public Property ProdPercentOn As Boolean = True

    Public Property DateFormat As String = "dd-MM-yyyy HH:mm:ss"

    Public Property DeviceType As String = "Computer"

    Public Property Scale As Integer = 100

    'Public Property ByGroup As Boolean = False

    Public Property DisplayByGroups As Boolean = False

    Public Property BrowserZoom As Integer = 100

    Public Property FeedrateOver As Boolean = True

    Public Property SpindleOver As Boolean = True

    Public Property RapidOver As Boolean = True

    Public Property TimeLineBarHeight As Integer = 100

    Public Property MachineNameText As Integer = 100

    Public Property MachineNameWidth As Integer = 100

    Public Property RemoveLastRow As Boolean = False

    Public Property DeviceExists As Boolean = False

#End Region

#End Region

    Sub New()

    End Sub


    Sub New(deviceId As Integer)

        LoadDevice(deviceId)

    End Sub


    Sub New(deviceIp As String)

        LoadDeviceByIp(deviceIp)

    End Sub


    Public Sub LoadDeviceByName(deviceName As String)

        Dim table = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.tbl_devices WHERE deviceName = '{deviceName}'")

        DeviceExists = False

        If table.Rows.Count > 0 Then
            Dim row = table.Rows(0)
            LoadDevice(CInt(row("id").ToString()))
        End If

    End Sub


    Public Sub LoadDeviceByIp(deviceIp As String)

        Dim table = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.tbl_devices WHERE IP_adress = '{deviceIp}'")

        DeviceExists = False

        If table.Rows.Count > 0 Then
            Dim row = table.Rows(0)
            LoadDevice(CInt(row("id").ToString()))
        End If

    End Sub


    Public Sub LoadDevice(deviceId As Integer)

        Dim table = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.tbl_devices WHERE id = {deviceId}")

        If table.Rows.Count > 0 Then

            DeviceExists = True

            Dim row = table.Rows(0)

            Me.DeviceId = deviceId

            IpAddress = row("IP_adress").ToString()
            DeviceName = row("deviceName").ToString()
            Machines = row("machines").ToString()
            Groups = row("groups").ToString()

            table = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.tbl_deviceConfig WHERE deviceId = {deviceId}")
            row = table.Rows(0)

            Messages = TrueOrFalse(row("messages").ToString())
            PieChart = TrueOrFalse(row("piechart").ToString())
            PieChartTime = row("piecharttime").ToString()
            RefreshBrowser = row("refreshbrowser").ToString()
            DateTime = TrueOrFalse(row("datetime").ToString())
            CustomLogo = TrueOrFalse(row("customlogo").ToString())
            DetailCustomLogo = row("detail_customlogo").ToString()
            Temperature = TrueOrFalse(row("temperature").ToString())
            Degree = row("Degree").ToString()
            DetailTemperature = row("detail_temperature").ToString()
            LastCycle = TrueOrFalse(row("LastCycle").ToString())
            CurrentCycle = TrueOrFalse(row("CurrentCycle").ToString())
            ElapsedTime = TrueOrFalse(row("ElapsedTime").ToString())
            FullScreen = TrueOrFalse(row("FullScreen").ToString())
            LiveStatusPosition = row("LiveStatusPosition").ToString()
            IFrame = TrueOrFalse(row("IFrame").ToString())
            DetailIFrame = row("detail_IFrame").ToString()
            Rotation = TrueOrFalse(row("Rotation").ToString())
            PieChartBy = row("PieChartBy").ToString()
            DisplayWhat = row("DisplayWhat").ToString()
            LogoBarHidden = TrueOrFalse(row("LogoBarHidden").ToString())
            DarkTheme = TrueOrFalse(row("DarkTheme").ToString())

            LiveStatusDelay = TrueOrFalse(row("livestatusdelay").ToString())
            DetailLiveStatusDelay = row("detail_livestatusdelay").ToString()
            If Not LiveStatusDelay Then
                LiveStatusDelay = True
                DetailLiveStatusDelay = "6"
            End If

            table = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.tbl_deviceconfig2 WHERE deviceId = {deviceId}")
            row = table.Rows(0)

            Timeline = TrueOrFalse(row("Timeline").ToString())
            Trends = TrueOrFalse(row("Trends").ToString())
            TrendsPercent = row("TrendsPercent")
            TrendCompare = row("TrendCompare").ToString()
            ProdPercentOn = TrueOrFalse(row("ProdPercentOn").ToString())
            DateFormat = row("DateFormat").ToString()
            DeviceType = row("DeviceType").ToString()
            Scale = row("Scale")
            'ByGroup = TrueOrFalse(row("ByGroup").ToString())
            DisplayByGroups = TrueOrFalse(row("DisplayByGroups").ToString())
            BrowserZoom = row("BrowserZoom")
            FeedrateOver = TrueOrFalse(row("FeedrateOver").ToString())
            SpindleOver = TrueOrFalse(row("SpindleOver").ToString())
            RapidOver = TrueOrFalse(row("RapidOver").ToString())
            TimeLineBarHeight = row("TimeLineBarHeight")
            MachineNameText = row("MachineNameText")
            MachineNameWidth = row("MachineNameWidth")
            RemoveLastRow = TrueOrFalse(row("RemoveLastRow").ToString())
        End If

    End Sub


    Public Sub ReloadDevice()

        DeviceExists = False

        If Me.DeviceId = 0 Then
            Return
        End If
        LoadDevice(Me.DeviceId)
    End Sub


    Public Sub SaveDevice()

        If IpAddress = "" Then
            Return
        End If

        Dim sqlCmd As New Text.StringBuilder()

        Try
            If Me.DeviceId = 0 Then
                sqlCmd.Append($"INSERT INTO                     ")
                sqlCmd.Append($"  csi_database.tbl_devices      ")
                sqlCmd.Append($"  (                             ")
                sqlCmd.Append($"     IP_adress  ,               ")
                sqlCmd.Append($"     deviceName ,               ")
                sqlCmd.Append($"     machines   ,               ")
                sqlCmd.Append($"     `groups`                   ")
                sqlCmd.Append($"  )                             ")
                sqlCmd.Append($"  VALUES                        ")
                sqlCmd.Append($"  (                             ")
                sqlCmd.Append($"     '{IpAddress}'  ,           ")
                sqlCmd.Append($"     '{DeviceName}' ,           ")
                sqlCmd.Append($"     '{Machines}'   ,           ")
                sqlCmd.Append($"     '{Groups}'                 ")
                sqlCmd.Append($"  )                           ; ")
                sqlCmd.Append($"  SELECT LAST_INSERT_ID()     ; ")

                Me.DeviceId = CInt(MySqlAccess.ExecuteScalar(sqlCmd.ToString()).ToString())
            Else

                sqlCmd.Append($"UPDATE                          ")
                sqlCmd.Append($"  csi_database.tbl_devices      ")
                sqlCmd.Append($" SET                            ")
                sqlCmd.Append($"   deviceName = '{DeviceName}', ")
                sqlCmd.Append($"   machines   = '{Machines}'  , ")
                sqlCmd.Append($"   `groups`   = '{Groups}'      ")
                sqlCmd.Append($" WHERE                          ")
                sqlCmd.Append($"   id         = {Me.DeviceId}   ")

                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

            End If


            sqlCmd.Clear()
            sqlCmd.Append($"INSERT INTO                                                 ")
            sqlCmd.Append($"  csi_database.tbl_deviceConfig                             ")
            sqlCmd.Append($"  (                                                         ")
            sqlCmd.Append($"     DeviceId              ,                                ")
            sqlCmd.Append($"     IP                    ,                                ")
            sqlCmd.Append($"     name                  ,                                ")
            sqlCmd.Append($"     messages              ,                                ")
            sqlCmd.Append($"     piechart              ,                                ")
            sqlCmd.Append($"     piecharttime          ,                                ")
            sqlCmd.Append($"     refreshbrowser        ,                                ")
            sqlCmd.Append($"     livestatusdelay       ,                                ")
            sqlCmd.Append($"     detail_livestatusdelay,                                ")
            sqlCmd.Append($"     `datetime`            ,                                ")
            sqlCmd.Append($"     customlogo            ,                                ")
            sqlCmd.Append($"     detail_customlogo     ,                                ")
            sqlCmd.Append($"     temperature           ,                                ")
            sqlCmd.Append($"     Degree                ,                                ")
            sqlCmd.Append($"     detail_temperature    ,                                ")
            sqlCmd.Append($"     LastCycle             ,                                ")
            sqlCmd.Append($"     CurrentCycle          ,                                ")
            sqlCmd.Append($"     ElapsedTime           ,                                ")
            sqlCmd.Append($"     FullScreen            ,                                ")
            sqlCmd.Append($"     LiveStatusPosition    ,                                ")
            sqlCmd.Append($"     IFrame                ,                                ")
            sqlCmd.Append($"     detail_IFrame         ,                                ")
            sqlCmd.Append($"     Rotation              ,                                ")
            sqlCmd.Append($"     PieChartBy            ,                                ")
            sqlCmd.Append($"     DisplayWhat           ,                                ")
            sqlCmd.Append($"     LogoBarHidden         ,                                ")
            sqlCmd.Append($"     DarkTheme                                              ")
            sqlCmd.Append($"  )                                                         ")
            sqlCmd.Append($"  VALUES                                                    ")
            sqlCmd.Append($"  (                                                         ")
            sqlCmd.Append($"      {Me.DeviceId}              ,                          ")
            sqlCmd.Append($"     '{IpAddress}'               ,                          ")
            sqlCmd.Append($"     '{DeviceName}'              ,                          ")
            sqlCmd.Append($"     '{OnOrOff(Messages)}'       ,                          ")
            sqlCmd.Append($"     '{OnOrOff(PieChart)}'       ,                          ")
            sqlCmd.Append($"     '{PieChartTime}'            ,                          ")
            sqlCmd.Append($"     '{RefreshBrowser}'          ,                          ")
            sqlCmd.Append($"     '{OnOrOff(LiveStatusDelay)}',                          ")
            sqlCmd.Append($"     '{DetailLiveStatusDelay}'   ,                          ")
            sqlCmd.Append($"     '{OnOrOff(DateTime)}'       ,                          ")
            sqlCmd.Append($"     '{OnOrOff(CustomLogo)}'     ,                          ")
            sqlCmd.Append($"     '{DetailCustomLogo}'        ,                          ")
            sqlCmd.Append($"     '{OnOrOff(Temperature)}'    ,                          ")
            sqlCmd.Append($"     '{Degree}'                  ,                          ")
            sqlCmd.Append($"     '{DetailTemperature}'       ,                          ")
            sqlCmd.Append($"     '{OnOrOff(LastCycle)}'      ,                          ")
            sqlCmd.Append($"     '{OnOrOff(CurrentCycle)}'   ,                          ")
            sqlCmd.Append($"     '{OnOrOff(ElapsedTime)}'    ,                          ")
            sqlCmd.Append($"     '{OnOrOff(FullScreen)}'     ,                          ")
            sqlCmd.Append($"     '{LiveStatusPosition}'      ,                          ")
            sqlCmd.Append($"     '{OnOrOff(IFrame)}'         ,                          ")
            sqlCmd.Append($"     '{DetailIFrame}'            ,                          ")
            sqlCmd.Append($"     '{OnOrOff(Rotation)}'       ,                          ")
            sqlCmd.Append($"     '{PieChartBy}'              ,                          ")
            sqlCmd.Append($"     '{DisplayWhat}'             ,                          ")
            sqlCmd.Append($"     '{OnOrOff(LogoBarHidden)}'  ,                          ")
            sqlCmd.Append($"     '{OnOrOff(DarkTheme)}'                                 ")
            sqlCmd.Append($"  )                                                         ")
            sqlCmd.Append($"  ON DUPLICATE KEY UPDATE                                   ")
            sqlCmd.Append($"     IP                     = '{IpAddress}'               , ")
            sqlCmd.Append($"     name                   = '{DeviceName}'              , ")
            sqlCmd.Append($"     messages               = '{OnOrOff(Messages)}'       , ")
            sqlCmd.Append($"     piechart               = '{OnOrOff(PieChart)}'       , ")
            sqlCmd.Append($"     piecharttime           = '{PieChartTime}'            , ")
            sqlCmd.Append($"     refreshbrowser         = '{RefreshBrowser}'          , ")
            sqlCmd.Append($"     livestatusdelay        = '{OnOrOff(LiveStatusDelay)}', ")
            sqlCmd.Append($"     detail_livestatusdelay = '{DetailLiveStatusDelay}'   , ")
            sqlCmd.Append($"     `datetime`             = '{OnOrOff(DateTime)}'       , ")
            sqlCmd.Append($"     customlogo             = '{OnOrOff(CustomLogo)}'     , ")
            sqlCmd.Append($"     detail_customlogo      = '{DetailCustomLogo}'        , ")
            sqlCmd.Append($"     temperature            = '{OnOrOff(Temperature)}'    , ")
            sqlCmd.Append($"     Degree                 = '{Degree}'                  , ")
            sqlCmd.Append($"     detail_temperature     = '{DetailTemperature}'       , ")
            sqlCmd.Append($"     LastCycle              = '{OnOrOff(LastCycle)}'      , ")
            sqlCmd.Append($"     CurrentCycle           = '{OnOrOff(CurrentCycle)}'   , ")
            sqlCmd.Append($"     ElapsedTime            = '{OnOrOff(ElapsedTime)}'    , ")
            sqlCmd.Append($"     FullScreen             = '{OnOrOff(FullScreen)}'     , ")
            sqlCmd.Append($"     LiveStatusPosition     = '{LiveStatusPosition}'      , ")
            sqlCmd.Append($"     IFrame                 = '{OnOrOff(IFrame)}'         , ")
            sqlCmd.Append($"     detail_IFrame          = '{DetailIFrame}'            , ")
            sqlCmd.Append($"     Rotation               = '{OnOrOff(Rotation)}'       , ")
            sqlCmd.Append($"     PieChartBy             = '{PieChartBy}'              , ")
            sqlCmd.Append($"     DisplayWhat            = '{DisplayWhat}'             , ")
            sqlCmd.Append($"     LogoBarHidden          = '{OnOrOff(LogoBarHidden)}'  , ")
            sqlCmd.Append($"     DarkTheme              = '{OnOrOff(DarkTheme)}'        ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

            sqlCmd.Clear()
            sqlCmd.Append($"INSERT INTO                                     ")
            sqlCmd.Append($"  csi_database.tbl_deviceConfig2                ")
            sqlCmd.Append($"  (                                             ")
            sqlCmd.Append($"     DeviceId             ,                     ")
            sqlCmd.Append($"     IP_adress            ,                     ")
            sqlCmd.Append($"     name                 ,                     ")
            sqlCmd.Append($"     Timeline             ,                     ")
            sqlCmd.Append($"     Trends               ,                     ")
            sqlCmd.Append($"     TrendsPercent        ,                     ")
            sqlCmd.Append($"     TrendCompare         ,                     ")
            sqlCmd.Append($"     ProdPercentOn        ,                     ")
            sqlCmd.Append($"     DateFormat           ,                     ")
            sqlCmd.Append($"     DeviceType           ,                     ")
            sqlCmd.Append($"     Scale                ,                     ")
            'sqlCmd.Append($"     ByGroup              ,                     ")
            sqlCmd.Append($"     DisplayByGroups      ,                     ")
            sqlCmd.Append($"     BrowserZoom          ,                     ")
            sqlCmd.Append($"     FeedrateOver         ,                     ")
            sqlCmd.Append($"     SpindleOver          ,                     ")
            sqlCmd.Append($"     RapidOver            ,                     ")
            sqlCmd.Append($"     TimeLineBarHeight    ,                     ")
            sqlCmd.Append($"     MachineNameText      ,                     ")
            sqlCmd.Append($"     MachineNameWidth     ,                     ")
            sqlCmd.Append($"     RemoveLastRow                              ")
            sqlCmd.Append($"  )                                             ")
            sqlCmd.Append($"  VALUES                                        ")
            sqlCmd.Append($"  (                                             ")
            sqlCmd.Append($"      {Me.DeviceId}       ,                     ")
            sqlCmd.Append($"     '{IpAddress}'        ,                     ")
            sqlCmd.Append($"     '{DeviceName}'       ,                     ")
            sqlCmd.Append($"      {Timeline}          ,                     ")
            sqlCmd.Append($"      {Trends}            ,                     ")
            sqlCmd.Append($"      {TrendsPercent}     ,                     ")
            sqlCmd.Append($"     '{TrendCompare}'     ,                     ")
            sqlCmd.Append($"     '{OnOrOff(ProdPercentOn)}',                ")
            sqlCmd.Append($"     '{DateFormat}'       ,                     ")
            sqlCmd.Append($"     '{DeviceType}'       ,                     ")
            sqlCmd.Append($"      {Scale}             ,                     ")
            'sqlCmd.Append($"      {ByGroup}           ,                     ")
            sqlCmd.Append($"      {DisplayByGroups}   ,                     ")
            sqlCmd.Append($"      {BrowserZoom}       ,                     ")
            sqlCmd.Append($"     '{OnOrOff(FeedrateOver)}',                 ")
            sqlCmd.Append($"     '{OnOrOff(SpindleOver)}' ,                 ")
            sqlCmd.Append($"     '{OnOrOff(RapidOver)}'   ,                 ")
            sqlCmd.Append($"      {TimeLineBarHeight} ,                     ")
            sqlCmd.Append($"      {MachineNameText}   ,                     ")
            sqlCmd.Append($"      {MachineNameWidth}  ,                     ")
            sqlCmd.Append($"     '{OnOrOff(RemoveLastRow)}'                 ")
            sqlCmd.Append($"  )                                             ")
            sqlCmd.Append($"  ON DUPLICATE KEY UPDATE                       ")
            sqlCmd.Append($"     name              = '{DeviceName}'       , ")
            sqlCmd.Append($"     IP_adress         = '{IpAddress}'        , ")
            sqlCmd.Append($"     Timeline          =  {Timeline}          , ")
            sqlCmd.Append($"     Trends            =  {Trends}            , ")
            sqlCmd.Append($"     TrendsPercent     =  {TrendsPercent}     , ")
            sqlCmd.Append($"     TrendCompare      = '{TrendCompare}'     , ")
            sqlCmd.Append($"     ProdPercentOn     = '{OnOrOff(ProdPercentOn)}', ")
            sqlCmd.Append($"     DateFormat        = '{DateFormat}'       , ")
            sqlCmd.Append($"     DeviceType        = '{DeviceType}'       , ")
            sqlCmd.Append($"     Scale             =  {Scale}             , ")
            'sqlCmd.Append($"     ByGroup           =  {ByGroup}           , ")
            sqlCmd.Append($"     DisplayByGroups   =  {DisplayByGroups}   , ")
            sqlCmd.Append($"     BrowserZoom       =  {BrowserZoom}       , ")
            sqlCmd.Append($"     FeedrateOver      = '{OnOrOff(FeedrateOver)}', ")
            sqlCmd.Append($"     SpindleOver       = '{OnOrOff(SpindleOver)}' , ")
            sqlCmd.Append($"     RapidOver         = '{OnOrOff(RapidOver)}'   , ")
            sqlCmd.Append($"     TimeLineBarHeight =  {TimeLineBarHeight} , ")
            sqlCmd.Append($"     MachineNameText   =  {MachineNameText}   , ")
            sqlCmd.Append($"     MachineNameWidth  =  {MachineNameWidth}  , ")
            sqlCmd.Append($"     RemoveLastRow     = '{OnOrOff(RemoveLastRow)}' ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

            MySqlAccess.ExecuteNonQuery($"UPDATE csi_database.tbl_messages      SET name = '{DeviceName}' WHERE DeviceId = {Me.DeviceId}; ")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Public Sub DeleteDevice()

        MySqlAccess.ExecuteNonQuery($"DELETE FROM CSI_database.tbl_devices WHERE Id = { Me.DeviceId }")

        MySqlAccess.ExecuteNonQuery($"DELETE FROM CSI_database.tbl_deviceconfig WHERE DeviceId = { Me.DeviceId }")

        MySqlAccess.ExecuteNonQuery($"DELETE FROM CSI_database.tbl_deviceconfig2 WHERE DeviceId = { Me.DeviceId }")

        MySqlAccess.ExecuteNonQuery($"DELETE FROM CSI_database.tbl_messages WHERE DeviceId = { Me.DeviceId }")

        IpAddress = String.Empty

    End Sub


    Public Sub UpdateIpAddress(newIpAddress As String)

        If String.IsNullOrEmpty(newIpAddress) Or String.IsNullOrEmpty(IpAddress) Then
            Return
        End If

        Dim sqlCmd As New Text.StringBuilder()

        sqlCmd.Append($"UPDATE csi_database.tbl_devices       SET IP_adress = '{newIpAddress}' WHERE Id       = { Me.DeviceId }; ")
        sqlCmd.Append($"UPDATE csi_database.tbl_deviceconfig  SET IP        = '{newIpAddress}' WHERE DeviceId = { Me.DeviceId }; ")
        sqlCmd.Append($"UPDATE csi_database.tbl_deviceconfig2 SET IP_ADRESS = '{newIpAddress}' WHERE DeviceId = { Me.DeviceId }; ")
        sqlCmd.Append($"UPDATE csi_database.tbl_messages      SET IP_adress = '{newIpAddress}' WHERE DeviceId = { Me.DeviceId }; ")

        Try
            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())
            IpAddress = newIpAddress

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Function TrueOrFalse(value As String) As Boolean
        Return value = "on" Or value = "1"
    End Function

    Private Function OnOrOff(value As Boolean) As String
        Return If(value, "on", "off")
    End Function

End Class

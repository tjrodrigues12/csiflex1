Imports System.ComponentModel
Imports System.IO
Imports System.IO.File
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows
Imports CSIFLEX.Database.Access
Imports CSIFLEX.eNetLibrary
Imports CSIFLEX.eNetLibrary.Data
Imports CSIFLEX.Server.Library
Imports CSIFLEX.Server.Settings
Imports CSIFLEX.Utilities
Imports MySql.Data.MySqlClient

Partial Public Class SetupForm2

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Icon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule.FileName)

    End Sub


    Private lastIp As String
    Private lastIp2 As String
    Private lastDevice As String
    Private lastMessage As String
    Private ServerPort As String = String.Empty
    Private IPAddressNew As String = String.Empty
    Private loadingFORM As Boolean = False
    Private isComingFromChangeIP As Boolean = False
    Private inLoadingDeviceMode = False
    Private Comput_perf_required As Boolean = False

    Private t1 As New System.Windows.Forms.Timer
    Private t2 As New System.Windows.Forms.Timer
    Private t3 As New System.Windows.Forms.Timer
    Private t4 As New System.Windows.Forms.Timer
    Private t5 As New System.Windows.Forms.Timer

    Private DailyTargets As Dictionary(Of String, Integer)
    Private WeeklyTargets As Dictionary(Of String, Integer)
    Private MonthlyTargets As Dictionary(Of String, Integer)

    Private tabColor As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private Groups_dic As Dictionary(Of String, List(Of String))

    Private services As Services
    Private myUserProfile As UserProfile
    Private path As String = CSI_Library.CSI_Library.serverRootPath
    Private dashboardDevice As DashboardDevice

    Public Shared listOfSourceToAdd As List(Of sourceToAdd)

    Public IP_ As String = ""
    Public selectedtype As String = ""

    Private Sub SetupForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CSI_Lib = New CSI_Library.CSI_Library(True)
        CSI_Library.CSI_Library.isServer = True 'Indicated that the this is a server application(not Client)
        listOfSourceToAdd = New List(Of sourceToAdd)
        lastIp = ""
        lastIp2 = ""
        lastDevice = ""
        lastMessage = ""
        loadingFORM = True

        Log.Debug("Start")

        'SetupFormGeneral_Load()

        'Dim tab As TabPage = TabControl_DashBoard.TabPages("tablePageTimeline")
        'TabControl_DashBoard.TabPages.Remove(tab)

        Dim partNumberCtrl As PartNumberControl = New PartNumberControl()
        Dim tab As TabPage = TabControl_DashBoard.TabPages("tablePagePartNumber")
        tab.Controls.Add(partNumberCtrl)
        partNumberCtrl.Location = New Drawing.Point(0, 0)
        partNumberCtrl.Dock = DockStyle.Fill

        Try
            ServerSettings.Instance.Init(IO.Path.Combine(CSI_Library.CSI_Library.serverRootPath, "sys"))
            Log.Debug($"*** ServerSettings.Instance.Init - {ServerSettings.ServerIPAddress}")

            eNetServer.Instance.Init(ServerSettings.EnetFolder)

            GetAllENETMachines()

            Dim machinesId As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)()
            Dim dbMachines As DataTable = MySqlAccess.GetDataTable("SELECT Id, EnetMachineName FROM csi_auth.tbl_ehub_conf;")
            For Each row As DataRow In dbMachines.Rows
                machinesId.Add(row("Id"), row("EnetMachineName"))
            Next
            eNetServer.Instance.ImportMachinesId(machinesId)

            LoadGridviewMachines()
            'Load_DGV_CSIConnector()
            LoadEnetGroups()

            myUserProfile = New UserProfile(CSIFLEXGlobal.UserName)

            grvConnectors.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            load_colors()

            loadEnetPwd()

            eNETMachineStatus.LoadMachinesStatus()

            Dim checkfortempthread As New Thread(AddressOf checkfortemp)
            checkfortempthread.Name = "tempchecking"
            checkfortempthread.Start()
            checkfortempthread.Priority = ThreadPriority.Normal

            loadingFORM = False

            LBL_IP.Text = ServerSettings.ServerIPAddress
            txtPort.Text = ServerSettings.RMPort

            move_htm_files()

            Dim OnlyIP As String = LBL_IP.Text
            IP_ = OnlyIP.Trim().ToString()

            dashboardDevice = New DashboardDevice()
            dashboardDevice.LoadDeviceByName("Local Host")
            If Not dashboardDevice.DeviceExists And IP_ <> "000.000.000.000" Then
                dashboardDevice.DeviceName = "Local Host"
                dashboardDevice.IpAddress = IP_
                dashboardDevice.Machines = "ALL"
                dashboardDevice.FullScreen = True
                dashboardDevice.DarkTheme = True
                dashboardDevice.SaveDevice()
            End If

            Dim endUserConfigUrl As String = """http://" & IP_ & ":" & txtPort.Text & "/enduserconfig"";"
            Dim newestMachinesRecordsUrl As String = """http://" & IP_ & ":" & txtPort.Text & "/refresh"";"
            Dim timelineUrl As String = """http://" & IP_ & ":" & txtPort.Text & "/timeline"";"

            Dim r As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global.js")
            Dim w As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global-.js")
            Dim line As String = ""

            While ((Not r.EndOfStream))
                line = r.ReadLine()
                If (line.StartsWith("var endUserConfigUrl =")) Then
                    w.WriteLine("var endUserConfigUrl =" & endUserConfigUrl)

                ElseIf (line.StartsWith("var newestMachinesRecordsUrl =")) Then
                    w.WriteLine("var newestMachinesRecordsUrl =" & newestMachinesRecordsUrl)

                ElseIf (line.StartsWith("var timelineUrl = ")) Then
                    w.WriteLine("var timelineUrl = " & timelineUrl)

                Else
                    w.WriteLine(line)
                End If

            End While
            r.Close()
            w.Close()

            File.Delete(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global.js")
            FileIO.FileSystem.RenameFile(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\Global-.js", "global.js")

            SetupFormGeneral_Load()

            Dim mySqlFolder = "C:\\CSIFLEX\\MySQL\\mysql-8.0.18-winx64\\bin"

            If Not Directory.Exists(mySqlFolder) Then
                mySqlFolder = "C:\\Program Files (x86)\\CSI Flex Server\\mysql\\mysql-8.0.18-winx64\\bin"
            End If

            If Not Directory.Exists(mySqlFolder) Then
                mySqlFolder = "C:\\Program Files (x86)\\CSI Flex Server\\mysql\\mysql-5.7.21-win32\\bin"
            End If

            BackupSettings.Backup(mySqlFolder)

        Catch ex As Exception
            loadingFORM = False
            MessageBox.Show(ex.Message)
            Me.Close()
        End Try

        Load_rm_port()

        btnSaveWebServerPort.Enabled = False

        If Not IPAddressNew = String.Empty Then
            LBL_IP.Text = IPAddressNew
        End If

    End Sub

    Private Sub TabControl1_select(sender As Object, e As EventArgs) Handles TabControl_DashBoard.SelectedIndexChanged

        If (TabControl_DashBoard.SelectedTab.Text) = "Sources" Then

            Load_groupes()

        End If

        If TabControl_DashBoard.SelectedTab.Text = "Users" Then

            UsersTabSelected()

        End If

        If TabControl_DashBoard.SelectedTab.Text = "eNETDNC" Then

            LoadEnetSettings()

            Call Load_groupes()

            If treeviewGroupsOfMachines.Nodes(0).Nodes.Count > 0 Then
                treeviewGroupsOfMachines.Nodes(0).Expand()
            End If

        End If

        If Not (TabControl_DashBoard.SelectedTab.Text = "eNETDNC") And (TargetsChanged = True) And (Comput_perf_required = False) Then
            TargetsChanged = False
            UpdateTargetsTitle()
            RefreshAllDevices()
        End If

        If Not (TabControl_DashBoard.SelectedTab.Text = "eNETDNC") And (Comput_perf_required = True) Then
            ComputePerfReq()
            Comput_perf_required = False
        End If

        If TabControl_DashBoard.SelectedTab.Text = "CSI Connector" Then

            Load_DGV_CSIConnector()

        End If

        If TabControl_DashBoard.SelectedTab.Text = "Dashboards" Then

            DashboardTabSelected()

        End If

        If TabControl_DashBoard.SelectedTab.Text = "About/License" Then

            Dim sb = New System.Text.StringBuilder()
            sb.Append("{\rtf1\ansi")
            sb.Append("---- \b Version 1.9.3.20\b0  :  2024 February 19 \line \line ")
            sb.Append("\b Fixe : \b0 Fixed issue with mobile app\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.3.19\b0  :  2023 January 9 \line \line ")
            sb.Append("\b Improvement : \b0 Change in the reports generator to select the shift(s) on the Yesterday report\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.3.16\b0  :  2022 September 29 \line \line ")
            sb.Append("\b Improvement : \b0 Change in the information saved on database\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.3.15\b0  :  2022 September 08 \line \line ")
            sb.Append("\b Improvement : \b0 Ajusted for the new version of eNETDNC\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.3.13\b0  :  2022 August 17 \line \line ")
            sb.Append("\b Fixe : \b0 Fixed User's form\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.3.12\b0  :  2022 July 18 \line \line ")
            sb.Append("\b Fixe : \b0 Fixed order of machines in the Availability Report\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.3.11\b0  :  2022 June 29 \line \line ")
            sb.Append("\b Fixe : \b0 Fixed issues with connectors and users\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.3.10\b0  :  2022 June 09 \line \line ")
            sb.Append("\b Improvement : \b0 Part number form\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.3.9\b0  :  2022 May 11 \line \line ")

            sb.Append("---- \b Version 1.9.3.8\b0  :  2022 April 12 \line \line ")

            sb.Append("---- \b Version 1.9.3.7\b0  :  2022 March 23 \line \line ")

            sb.Append("---- \b Version 1.9.3.6\b0  :  2022 March 08 \line \line ")

            sb.Append("---- \b Version 1.9.3.5\b0  :  2022 February 24 \line \line ")
            sb.Append("\b Fixe : \b0 Fixed issues with the connectors\b0 . \line \line ")
            sb.Append("\b Improvement : \b0 Created a new filter for partnumber from the connectors\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.3.4\b0  :  2022 February 03 \line \line ")
            sb.Append("\b Fixe : \b0 Fixed an issue with the reports\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.3.3\b0  :  2022 January 24 \line \line ")
            sb.Append("\b Improvement : \b0 Created the Raw Data Log\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.3.1\b0  :  2021 November 18 \line \line ")
            sb.Append("\b Fixe : \b0 Fixed some issues with import data from FOCAS machines \b0 . \line \line ")
            sb.Append("\b Improvement : \b0 Change the visual identity of the product\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.33\b0  :  2021 November 18 \line \line ")
            sb.Append("\b Fixe :\b0 Fixed some issues with import data from eNETDNC \b0 . \line \line ")
            sb.Append("\b Improvement : \b0 Change the visual identity of the product\b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.29\b0  :  2021 July 09 \line \line ")
            sb.Append("\b Fixe : \b0 Rename Admin group to Administrators \b0 . \line \line ")
            sb.Append("\b Fixe : \b0 Fixed Setup command from eNETDNC \b0 . \line \line ")
            sb.Append("\b Fixe : \b0 Fixed error when deleting Rapid override from connectors \b0 . \line \line ")
            sb.Append("\b Improvement :\b0 Message while the service is starting \b0 . \line \line ")
            sb.Append("\b Improvement :\b0 Import group of machines from eNETDNC \b0 . \line \line ")
            sb.Append("\b Improvement :\b0 Changed the mobile interface to don't need an user \b0 . \line \line ")

            sb.Append("---- \b Version 1.9.2.28\b0  :  2021 May 27 \line \line ")
            sb.Append("\b Fixe :\b0 Fixed some issues with duplicated machine name on eNETDNC \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.27\b0  :  2021 May 06 \line \line ")
            'sb.Append("\b Fixe :\b0 Fixed problem with column of pressure when connected with monitoring unit \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0 Fixed problem with performance tables that was causing problems at dashboard's timeline \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.26\b0  :  2021 May 04 \line \line ")
            'sb.Append("\b New :\b0  Column of pressure when connected with monitoring unit \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.24\b0  :  2021 March 03 \line \line ")
            'sb.Append("\b New :\b0  Backup of CSIFLEX and eNETDNC settings \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.23\b0  :  2021 February 20 \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issues with the support for Monitoring Units \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issues with the User Interface \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.22\b0  :  2021 February 08 \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issues with the support for Monitoring Units \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.21\b0  :  2021 January 29 \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issues with the support for Monitoring Units \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.19\b0  :  2021 January 11 \line \line ")
            'sb.Append("\b Improvement :\b0  Implemented the support for Monitoring Units \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.18\b0  :  2021 January 08 \line \line ")
            'sb.Append("\b Improvement :\b0  Implemented the support for Monitoring Units \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.16\b0  :  2020 October 26 \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issues with connectors \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.15\b0  :  2020 September 22 \line \line ")
            'sb.Append("\b Improvement :\b0  Created the property Machine Name that can be different of the eNETDNC Machine Name \b0 . \line \line ")
            'sb.Append("\b Improvement :\b0  It's possible select if the dashboard will show the Machine Name, Label, or eNETDNC Machine Name \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.14\b0  :  2020 August 23 \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issue with logs files \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.13\b0  :  2020 August 20 \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issue with logs files \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.12\b0  :  2020 July 16 \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issue with installation of MYSQL service \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issue with initialization of the database \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.11\b0  :  2020 July 07 \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issue with dashboard timeline \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issue when add machines connections \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.10\b0  :  2020 May 28 \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issue with Downtime reports \b0 . \line \line ")
            'sb.Append("\b Improvement :\b0  Created views of operator table on database \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.9\b0  :  2020 June 04 \line \line ")
            'sb.Append("\b Improvement :\b0  Created views of machines' tables on database \b0 . \line \line ")
            'sb.Append("\b Improvement :\b0  Created user to access the views \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.8\b0  :  2020 May 28 \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed issues saving Part Number info in the database \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.6\b0  :  2020 April 20 \line \line ")
            'sb.Append("\b Fixe :\b0  Rules do status CYCLE OFF in the connectors \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.5\b0  :  2020 April 10 \line \line ")
            'sb.Append("\b Fixe :\b0  Rules do status changes in the connectors \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.4\b0  :  2020 March 23 \line \line ")
            'sb.Append("\b Improvement :\b0  Health monitoring report \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.3\b0  :  2020 March 19 \line \line ")
            'sb.Append("\b Improvement :\b0  Improvement CSIFLEX to don't use the eNETDNC dashboard \b0 . \line \line ")
            'sb.Append("\b Improvement :\b0  User Mobile for Mobile App \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.2\b0  :  2020 March 11 \line \line ")
            'sb.Append("\b Improvement :\b0  Improvement CSIFLEX to don't use the eNETDNC dashboard \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.1\b0  :  2020 March 06 \line \line ")
            'sb.Append("\b Improvement :\b0  Improvements for CSIFLEX Mobile \b0 . \line \line ")
            'sb.Append("\b Improvement :\b0  Restart the Reporting Service when it breaks \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.2.0\b0  :  2020 February 25 \line \line ")
            'sb.Append("\b Improvement :\b0  Improvement in the UDP communication  \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed problems with MTC/Focas Connectors settings  \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed problems with Users settings  \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Fixed problem with the duplicity of machines in eNET  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.53\b0  :  2020 February 06 \line \line ")
            'sb.Append("\b Fixe :\b0  Problems with 'Configure reports' form  \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Problems with 'eNETDNC' form  \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Problems with Dashboard's pie chart and targets  \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Problems with initialization of the CSIFLEX service  \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Some issues in the Reporting service  \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  New CSIFLEX Logo  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.52\b0  :  2020 January 29 \line \line ")
            'sb.Append("\b New :\b0  CSI Connector - Function to control and update the adapters services. \b0 . \line \line ")
            'sb.Append("\b Fixe :\b0  Duplication of dashboard devices  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.51\b0  :  2020 January 16 \line \line ")
            'sb.Append("\b New :\b0  New adapter that allows recovering 'Parts Required' and 'Part Count' from FOCAS machines to show in the dashboards. \b0 . \line \line ")
            'sb.Append("\b New :\b0  The changes include: \b0 . \line \line ")
            'sb.Append("\b New :\b0   - Mapping the fields from the adapter in the 'Condition Editor' form. \b0 . \line \line ")
            'sb.Append("\b New :\b0   - Setting in the dashboards settings the format with that the information will be showing in the dashboard. \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.50\b0  :  2020 January 09 \line \line ")
            'sb.Append("\b New :\b0  Report feature - Allow selecting the reports to generate automatically at any time. \b0 . \line \line ")
            'sb.Append("\b New :\b0  New mechanism for event and error logging. \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.49\b0  :  2020 January 02 \line \line ")
            'sb.Append("\b Fixe :\b0  Report improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.48\b0  :  2019 December 20 \line \line ")
            'sb.Append("\b Fixe :\b0  Report improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.47\b0  :  2019 December 16 \line \line ")
            'sb.Append("\b Fixe :\b0  Report improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.46\b0  :  2019 December 09 \line \line ")
            'sb.Append("\b Fixe :\b0  Dashboard improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.45\b0  :  2019 December 03 \line \line ")
            'sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.43\b0  :  2019 November 27 \line \line ")
            'sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.42\b0  :  2019 November 21 \line \line ")
            'sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.41\b0  :  2019 November 18 \line \line ")
            'sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            'sb.Append("---- \b Version 1.9.1.40\b0  :  2019 November 12 \line \line ")
            'sb.Append("\b Fixe :\b0  Reports improvements  \b0 . \line \line ")

            sb.Append("}")
            RTB_liste.Rtf = sb.ToString()

        End If

    End Sub


    Private Sub btnLicenseRequest_Click(sender As Object, e As EventArgs) Handles btnLicenseRequest.Click
        LicenseManagement.StartPosition = FormStartPosition.CenterScreen
        LicenseManagement.ShowDialog()
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        Dim aboutform As New AboutBox
        aboutform.ShowDialog()
    End Sub


    Private Sub SetupForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Try
            If Not (TabControl_DashBoard.SelectedTab.Text = "eNETDNC") And (TargetsChanged = True) Then
                TargetsChanged = False
                UpdateTargetsTitle()
                RefreshAllDevices()

            End If

            If Not (TabControl_DashBoard.SelectedTab.Text = "eNETDNC") And (Comput_perf_required = True) Then
                ComputePerfReq()
                Comput_perf_required = False
            End If

            Dim Result

            If BgWorker_CreateDB.IsBusy Then
                Result = MessageBox.Show("Database is being created, Do you want to exit?.", "CAUTION", MessageBoxButton.OKCancel, MessageBoxImage.Information)

            ElseIf BgWorker_importDB.IsBusy Then
                Result = MessageBox.Show("CSV file is being imported, Do you want to exit?.", "CAUTION", MessageBoxButton.OKCancel, MessageBoxImage.Information)

            Else
                Result = MessageBox.Show("Do you want to exit the application ?.", "Exit", MessageBoxButton.OKCancel, MessageBoxImage.Information)

            End If

            If (Result = MessageBoxResult.OK) Then
                UserLogin.Close()
                Form1.Close()
                Environment.Exit(0)
            Else
                e.Cancel = True
            End If

        Catch ex As Exception

        End Try

    End Sub



    Public Sub GetAllENETMachines() 'This Function is used to load Ehub Config Files in Database 

        Dim StringMonId As String = ""

        Try

            Dim dTable_CheckTableisEmpty = MySqlAccess.GetDataTable("SELECT count(*) as Count FROM csi_auth.tbl_ehub_conf;")

            Dim IsInsert As Boolean = False

            IsInsert = (dTable_CheckTableisEmpty.Rows.Item(0).Item("Count") = 0)

            Dim ehubfilepath As String = ""
            Dim Monsetupfilepath As String = ""
            Dim allconst As New AllStringConstants.StringConstant

            ehubfilepath = allconst.SERVER_ENET_PATH & allconst.EHUB_CONF_FILE_NAME
            Monsetupfilepath = allconst.SERVER_ENET_PATH & allconst.MON_SETUP_FILE_NAME

            If File.Exists(ehubfilepath) And File.Exists(Monsetupfilepath) Then

                Dim fileData() As String = File.ReadAllLines(ehubfilepath) 'Read All Lines from EhubConf file
                Dim MonData() As String = File.ReadAllLines(Monsetupfilepath) 'Read All Lines from MonSetUp file

                Dim splitInput As New List(Of String)
                Dim splitMonData As New List(Of String)
                Dim machines As New List(Of MachineEhub)

                Dim machine As New MachineEhub()
                Dim nextLine As Boolean = False

                For Each line As String In fileData
                    If nextLine Then
                        machine.MachinePosition = line.Replace(":", "")
                        machine.MachineLabel = $"MCH_{machine.MachinePosition.Replace(",", "")}"
                        nextLine = False

                    ElseIf line.Contains("NM:") Then
                        splitInput.Add(line.Replace("NM:", "")) 'Name of the ENET Machine
                        machine.MachineName = line.Replace("NM:", "")

                    ElseIf line.Contains("FI:") Then
                        splitInput.Add(line.Replace("FI:", "")) 'Connection Type_ This value doesn't define all parameters but when it's 1 then it indicates FTP Connection
                        machine.IsFtpConnection = (line.Replace("FI:", "").Trim() = 1)
                        machine.ConnectionType = If(machine.IsFtpConnection, 5, 0)

                        machines.Add(machine)
                        machine = New MachineEhub()

                    End If

                    If line.Trim() = "" Then
                        nextLine = True
                    End If
                Next

                nextLine = False
                For Each line As String In MonData

                    If nextLine Then
                        machine = machines.Find(Function(m) m.MachinePosition = line.Replace(":", ""))
                        nextLine = False
                        If Not machine Is Nothing Then StringMonId = $"Mon {machine.MachinePosition}"

                    ElseIf line.Contains("ON:") Then 'Machine Monitoring Status If 1 then Monitoring is ON IF 0 then Monitoring is OFF
                        splitMonData.Add(line.Replace("ON:", ""))
                        machine.IsMonitoring = (line.Replace("ON:", "") = 1)

                    ElseIf line.Contains("TH:") Then
                        splitMonData.Add(line.Split(":")(1)) 'Stores the TH Parameter Values
                        machine.ThParameter = line.Split(":")(1)

                    ElseIf line.Contains("DD:") Then
                        Dim FileFTPName As String = line.Split(",")(1)
                        splitMonData.Add(FileFTPName)
                        machine.FtpFileName = FileFTPName

                    ElseIf line.Contains("DA:") Then
                        splitMonData.Add(line.Split(":")(1))
                        machine.DaParameter = line.Split(":")(1)

                    End If

                    If line.Trim() = "" Then
                        nextLine = True
                    End If
                Next

                Dim fileLength As Integer = splitInput.Count
                Dim MonFilelength As Integer = splitMonData.Count
                Dim k As Integer, l As Integer, p As Integer, q As Integer, querycount As Integer, Counter As Integer, r As Integer
                Dim MonitoringId As String = ""
                Dim MachineName As String = ""
                Dim Machine_Filename As String = ""
                Dim MachineLabel As String = ""
                Dim TH_Value As Integer
                Dim Monstate As Integer
                Dim FTPFileName As String
                Dim EnetDept As String
                'Dim MonSetUpId As Integer, 
                Dim Con_type As Integer
                Dim machineId As Integer = 1

                k = 1
                l = 1
                p = 0
                q = 1
                r = 0
                querycount = 0
                Counter = 0

                While q < fileLength

                    If k <= 16 And l <= 8 Then

                        MonitoringId = Convert.ToString(k) + "," + Convert.ToString(l)
                        MachineLabel = "MCH_" + Convert.ToString(k) + Convert.ToString(l)
                        Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & ".SYS_"
                        StringMonId = Convert.ToString(MonitoringId)
                        MachineName = Convert.ToString(splitInput(p))

                        If MachineName = "" Then
                            MachineName = $"MCH {Format(machineId, "000")}"
                        End If

                        machineId += 1

                        'MonSetUpId = 0
                        If (Convert.ToInt32(splitInput(q))) = 1 Then 'Machines whose (FI:1) For future it will be called as Connection Type 5 where  (FI:1,EH:0 and EO:0) 
                            Con_type = 5
                        Else
                            Con_type = 0
                        End If

                        Monstate = Convert.ToInt32(splitMonData(r))
                        TH_Value = Convert.ToInt32(splitMonData(r + 1)) 'Represent TH value
                        FTPFileName = splitMonData(r + 2).ToString()
                        EnetDept = splitMonData(r + 3).ToString()

                        If TH_Value = 1 Then
                            '2 Head MonitorData010.SYS_,MonitorData011.SYS_
                            Machine_Filename = $"MonitorData{ (k - 1).ToString("x") & Convert.ToString(l - 1) }0.SYS_,MonitorData{ (k - 1).ToString("x") & Convert.ToString(l - 1) }1.SYS_"

                        ElseIf TH_Value = 2 Then
                            '2 Pallet MonitorData010.SYS_,MonitorData011.SYS_
                            Machine_Filename = $"MonitorData{ (k - 1).ToString("x") & Convert.ToString(l - 1) }0.SYS_,MonitorData{ (k - 1).ToString("x") & Convert.ToString(l - 1) }1.SYS_"

                        Else
                            Machine_Filename = $"MonitorData{ (k - 1).ToString("x") & Convert.ToString(l - 1) }.SYS_"

                        End If

                        Dim sqlCmd As New Text.StringBuilder()

                        If IsInsert Then
                            sqlCmd.Append($"INSERT INTO csi_auth.tbl_ehub_conf ")
                            sqlCmd.Append($" (                                 ")
                            sqlCmd.Append($"    monitoring_id      ,           ")
                            sqlCmd.Append($"    machine_name       ,           ")
                            sqlCmd.Append($"    EnetMachineName    ,           ")
                            sqlCmd.Append($"    Con_type           ,           ")
                            sqlCmd.Append($"    monitoring_filename,           ")
                            sqlCmd.Append($"    Monstate           ,           ")
                            sqlCmd.Append($"    machine_label      ,           ")
                            sqlCmd.Append($"    ftpfilename        ,           ")
                            sqlCmd.Append($"    CurrentStatus      ,           ")
                            sqlCmd.Append($"    CurrentPartNumber  ,           ")
                            sqlCmd.Append($"    EnetDept           ,           ")
                            sqlCmd.Append($"    TH_State                       ")
                            sqlCmd.Append($" )                                 ")
                            sqlCmd.Append($" VALUES                            ")
                            sqlCmd.Append($" (                                 ")
                            sqlCmd.Append($"    '{StringMonId}'     ,          ")
                            sqlCmd.Append($"    '{MachineName}'     ,          ")
                            sqlCmd.Append($"    '{MachineName}'     ,          ")
                            sqlCmd.Append($"    '{Con_type}'        ,          ")
                            sqlCmd.Append($"    '{Machine_Filename}',          ")
                            sqlCmd.Append($"    '{Monstate}'        ,          ")
                            sqlCmd.Append($"    '{MachineLabel}'    ,          ")
                            sqlCmd.Append($"    '{FTPFileName}'     ,          ")
                            sqlCmd.Append($"    ''                  ,          ")
                            sqlCmd.Append($"    ''                  ,          ")
                            sqlCmd.Append($"    '{EnetDept}'        ,          ")
                            sqlCmd.Append($"    '{TH_Value}'                   ")
                            sqlCmd.Append($" )                               ; ")

                        Else
                            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_ehub_conf               ")
                            sqlCmd.Append($"  SET                                              ")
                            sqlCmd.Append($"      EnetMachineName     = '{ MachineName }'     ,")
                            sqlCmd.Append($"      Con_type            = '{ Con_type }'        ,")
                            sqlCmd.Append($"      monitoring_filename = '{ Machine_Filename }',")
                            sqlCmd.Append($"      Monstate            = '{ Monstate }'        ,")
                            sqlCmd.Append($"      ftpfilename         = '{ FTPFileName }'     ,")
                            sqlCmd.Append($"      EnetDept            = '{ EnetDept }'        ,")
                            sqlCmd.Append($"      TH_State            = '{ TH_Value }'         ")
                            sqlCmd.Append($"  WHERE                                            ")
                            sqlCmd.Append($"      monitoring_id       = '{ MonitoringId }'   ; ")

                            sqlCmd.Append($"UPDATE IGNORE `csi_auth`.`tbl_ehub_conf`            ")
                            sqlCmd.Append($" SET                                                ")
                            sqlCmd.Append($"    `Machine_Name`        = '{ MachineName }'       ")
                            sqlCmd.Append($" WHERE                                              ")
                            sqlCmd.Append($"     `monitoring_id`      = '{ MonitoringId }'      ")
                            sqlCmd.Append($" AND `Machine_Name`       = ''                    ; ")
                        End If

                        querycount = MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                        If querycount > 1 Then
                            Counter += 1
                        End If

                        l += 1
                        p += 2
                        q += 2
                        r += 4

                        If l > 8 Then
                            l = 1
                            k += 1
                        End If
                    End If

                End While

            Else
                MessageBox.Show("EhubConf.sys Or MonSetUp.sys file is not exists or you don't have authority to access it")
            End If

        Catch ex As Exception
            MessageBox.Show($"EhubConf Table has error while inserting data ({ StringMonId }): " & ex.Message)
        End Try

    End Sub

    Private Sub LoadEnetGroups()

        Dim eNetPath = CSI_Lib.eNET_path() & "\_SETUP\MonList.sys"
        Dim groupName = ""
        Dim sqlCmd = New Text.StringBuilder()
        Dim line As String

        Using fs As New StreamReader(File.OpenRead(eNetPath))

            sqlCmd.Append($"TRUNCATE TABLE csi_database.tbl_EnetGroups; ")

            While Not (fs.EndOfStream)

                line = fs.ReadLine()

                If line.StartsWith("_MT_") Or line.StartsWith("_ST_") Then
                    groupName = line
                Else
                    If line.Trim().Length > 0 Then
                        sqlCmd.Append($"INSERT INTO                   ")
                        sqlCmd.Append($"  csi_database.tbl_EnetGroups ")
                        sqlCmd.Append($"  (                           ")
                        sqlCmd.Append($"     groupName,               ")
                        sqlCmd.Append($"     machine                  ")
                        sqlCmd.Append($"  )                           ")
                        sqlCmd.Append($"  VALUES                      ")
                        sqlCmd.Append($"  (                           ")
                        sqlCmd.Append($"     '{groupName}',           ")
                        sqlCmd.Append($"     '{line}'                 ")
                        sqlCmd.Append($"  );                          ")
                    End If
                End If
            End While

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())
        End Using
    End Sub

    Public Sub load_colors()
        Try
            Dim chemin_Color As String = ""
            Dim scolor As String = ""

            If Exists(path & "\sys\SetupColor_.csys") Then
                Using reader As StreamReader = New StreamReader(path & "\sys\SetupColor_.csys")
                    chemin_Color = reader.ReadLine()
                End Using
            ElseIf Exists(path & "\sys\setup_.csys") Then
                Using reader As StreamReader = New StreamReader(path & "\sys\setup_.csys")
                    chemin_Color = reader.ReadLine()
                End Using
            Else
                MessageBox.Show("Please specify the eNET folder")
            End If

            Dim int As Integer = 0
            Dim alpha As Integer = 255
            Dim backcolor As Color
            Dim MyImageList As New ImageList()
            Dim colors As Dictionary(Of String, Integer)
            colors = GetEnetGraphColor(chemin_Color)

            Dim sql As String

            For Each status As String In colors.Keys

                backcolor = System.Drawing.ColorTranslator.FromWin32(colors(status))
                scolor = System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(backcolor.R, backcolor.G, backcolor.B))

                tabColor.Add(status, scolor)

                sql = $"insert into CSI_database.tbl_colors  ( statut,  color)  values ( '{ tabColor.Keys(int) }', '{ tabColor.Values(int) }')  ON DUPLICATE KEY update statut = ('{ tabColor.Keys(int) }'), color = ('{ tabColor.Values(int) }')"

                int = int + 1

                Try
                    MySqlAccess.ExecuteNonQuery(sql)

                Catch x As Exception
                    Log.Error("LoadColors1", x)
                    MessageBox.Show(x.ToString())
                End Try
            Next

        Catch ex As Exception
            Log.Error("LoadColors1", ex)
            MessageBox.Show(ex.ToString())
        End Try

    End Sub

    Function GetEnetGraphColor(eNETrep As String) As Dictionary(Of String, Integer)

        Dim res As Dictionary(Of String, Integer)

        Dim file As System.IO.StreamReader

        Dim color_list As New Dictionary(Of String, Integer), line As String()

        Try
            Log.Info($"Colors file: {eNETrep}\_SETUP\GraphColor.sys")
            If My.Computer.FileSystem.FileExists(eNETrep + "\_SETUP\GraphColor.sys") Then

                file = My.Computer.FileSystem.OpenTextFileReader(eNETrep + "\_SETUP\GraphColor.sys")
                While Not file.EndOfStream
                    line = file.ReadLine().Split(",")
                    If line(1) <> "" Then
                        If color_list.ContainsKey(line(1)) Then

                        Else
                            color_list.Add(line(1).ToUpperInvariant, line(0))
                        End If

                    End If

                End While
                Log.Info($"Qtt of colors: {color_list.Count}")
                file.Close()
            End If
        Catch ex As Exception
            Log.Error(ex)
        End Try

        res = color_list
        Return res

    End Function

    Public Sub loadEnetPwd()

        Try
            Dim enetPath As String

            If ServerSettings.EnetFolder <> "" Then

                enetPath = IO.Path.Combine(eNetServer.EnetSetupFolder, "Options.sys")

                If File.Exists(enetPath) Then

                    Dim line As String = ""
                    Dim idx As Integer = 0
                    Dim ftpIp As String = ""
                    Dim ftpPwd As String = ""

                    Using reader As StreamReader = New StreamReader(enetPath)
                        While Not reader.EndOfStream
                            idx += 1
                            line = reader.ReadLine()

                            If idx = 25 Then
                                ftpIp = Util.ConvertHexToIpAddress(line)
                            End If
                            If idx = 26 Then
                                ftpPwd = line
                            End If
                        End While
                    End Using

                    Dim dt = MySqlAccess.GetDataTable($"SELECT ftpip FROM csi_auth.ftpconfig WHERE ftpip = '{ftpIp}'")

                    If dt.Rows.Count > 0 Then
                        MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.ftpconfig SET ftppwd = '{ftpPwd}' WHERE ftpip = '{ftpIp}'")
                    Else
                        MySqlAccess.ExecuteNonQuery($"TRUNCATE csi_auth.ftpconfig; INSERT INTO csi_auth.ftpconfig (ftpip, ftppwd) VALUES ('{ftpIp}', '{ftpPwd}')")
                    End If
                End If
            Else
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub

    Private Sub checkfortemp()
        If CheckForInternetConnection() = False Then
            Temp_CB.Enabled = False
            'Label18.Enabled = False
            Label24.Enabled = False
            City_TB.Enabled = False
            Degree_CB.Enabled = False
            Label23.Enabled = False
        Else
            Temp_CB.Enabled = True
            ' Label18.Enabled = True
            Label24.Enabled = True
            City_TB.Enabled = True
            Degree_CB.Enabled = True
            Label23.Enabled = True
        End If
    End Sub

    Public Shared Function CheckForInternetConnection() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    Return True
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Sub move_htm_files()

        isComingFromChangeIP = False

        If Not Directory.Exists(CSI_Library.CSI_Library.serverRootPath & "\html\") Then

            CSI_Lib.Log_server_event("html files not found, copying files ...")

            Dim path As String = System.Reflection.Assembly.GetExecutingAssembly().Location
            path = System.IO.Path.GetDirectoryName(path)

            If Directory.Exists(path) Then

                path = path & "\html\"

                If Directory.Exists(path) Then
                    Directory.Move(path, CSI_Library.CSI_Library.serverRootPath & "\html\")
                    change_IP(True)
                End If
            Else
                CSI_Lib.Log_server_event("html files not found in : " & path)
            End If
        Else
            change_IP(True)
        End If

    End Sub

    Private Sub change_IP(checkfile As Boolean)

        Dim IP_0 As String = ""

        If checkfile = True Then
            Try
                If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")) Then

                    Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")
                        IP_0 = reader.ReadLine
                        IP_choice.LB_IPCheckChanged.Text = String.Empty
                        IP_choice.LB_IPCheckChanged.Text = IP_0
                        LBL_IP.Text = IP_0
                        reader.Close()
                    End Using

                End If
            Catch ex As Exception
                CSI_Lib.LogServerError("At reading srvip  from csys: " & ex.Message, 1)
            End Try
        End If

        Dim IPv4Address As New List(Of String)
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                IPv4Address.Add(ipheal.ToString())
            End If
        Next

        If IPv4Address.Count = 0 Then
            MsgBox("No Ip Address found")
            Me.Close()
        End If


        If IPv4Address.Count > 1 And checkfile = True And IPv4Address.Contains(IP_0) Then Return


        If IPv4Address.Count > 1 Then
            Try
                If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")) Then
                    Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")
                        IP_0 = reader.ReadLine
                        IP_choice.LB_IPCheckChanged.Text = String.Empty
                        IP_choice.LB_IPCheckChanged.Text = IP_0
                        reader.Close()
                    End Using
                End If
            Catch ex As Exception
                CSI_Lib.LogServerError("At reading srvip  from csys: " & ex.Message, 1)
            End Try

            IP_choice.LB_list.DataSource = IPv4Address
            IP_choice.ShowDialog()

            If CSI_Library.CSI_Library.IPChange = 1 Then
                LBL_IPChange.Text = "IP did not change !"
                LBL_IPChange.Visible = True
                LBL_IPChange.BackColor = Color.Yellow
                t1.Interval = 10000
                AddHandler t1.Tick, AddressOf Tickhandle
                t1.Start()

            ElseIf CSI_Library.CSI_Library.IPChange = 2 Then

                Dim serviceexepath As String = String.Empty

                serviceexepath = AppDomain.CurrentDomain.BaseDirectory + "CSIFlexServerServices.exe"

                If (ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlexServerService")) Then

                    'If below code not working uncomment above two lines and comment below Logic upto Else 
                    'While ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Run Or ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Starting
                    '    Do Until ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Stop
                    '        CSI_Lib.KillingAProcess("CSIFlexServerService")
                    '    Loop
                    'End While

                    'Do Until ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Run ' Or ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") = ServiceTools.ServiceState.Starting
                    '    ServiceTools.ServiceInstaller.StartService("CSIFlexServerService")
                    '    ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlexServerService")
                    'Loop

                    'Update all Focas Machine Agent IPAddress to this Server Address
                    Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                    Dim NewUpdate As String = String.Empty

                    Try
                        cntsql.Open()
                        NewUpdate = $"SET SQL_SAFE_UPDATES = 0; UPDATE `csi_auth`.`tbl_csiconnector` SET `AgentIP` = '{ IP_ }' WHERE ConnectorType ='Focas';"
                        Dim cmdOtherUpdate As New MySqlCommand(NewUpdate, cntsql)
                        Dim rdrOtherUpdate As MySqlDataReader = cmdOtherUpdate.ExecuteReader
                        cntsql.Close()
                    Catch ex As Exception
                        MessageBox.Show("Agent IP Update Database Error : " & ex.Message())
                    End Try

                    LBL_IPChange.Text = "IP Changed Sucessfully !"
                    LBL_IPChange.Visible = True
                    LBL_IPChange.BackColor = Color.LimeGreen

                    Me.Load_DGV_CSIConnector()
                Else
                    'In this case Install the Service 
                    ServiceTools.ServiceInstaller.InstallAndStart("CSIFlexServerService", "CSIFlexServerService", serviceexepath)
                    ServiceTools.ServiceInstaller.SetDelayedStart("CSIFlexServerService")
                    Dim ServiceCont_CSIF As System.ServiceProcess.ServiceController
                    ServiceCont_CSIF = New System.ServiceProcess.ServiceController("CSIFlexServerService")
                End If
                t2.Interval = 10000
                AddHandler t2.Tick, AddressOf Tickhandle
                t2.Start()
            Else
                'Do nothing
            End If

        Else
            If isComingFromChangeIP Then
                isComingFromChangeIP = False
                LBL_IPChange.Text = "Your PC has only one IP Address !"
                LBL_IPChange.Visible = True
                LBL_IPChange.BackColor = Color.LimeGreen
                t5.Interval = 10000
                AddHandler t5.Tick, AddressOf Tickhandle
                t5.Start()
            End If
            IP_ = IPv4Address.Item(0)
        End If

        Try
            ' If File.Exists((CSI_Lib.serverRootPath & "\sys\srv_ip_.csys")) Then
            Using w_ As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")
                w_.WriteLine(IP_)
                LBL_IP.Text = IP_
            End Using
            '  End If

        Catch ex As Exception

            Log.Error($"At writing srv_ip to csys: { ex.Message }", ex)

        End Try

        Dim PORT As String = ""

        Try
            If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")) Then
                Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                    PORT = reader.ReadLine
                    reader.Close()
                End Using
            Else
                PORT = "8008"
            End If
        Catch ex As Exception

            Log.Error($"At reading RM Port from csys: { ex.Message }", ex)

        End Try


        Dim endUserConfigUrl As String = """http://" & IP_ & ":" & PORT & "/enduserconfig"";"
        Dim newestMachinesRecordsUrl As String = """http://" & IP_ & ":" & PORT & "/refresh"";"
        Dim timelineUrl As String = """http://" & IP_ & ":" & PORT & "/timeline"";"


        Dim r As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global.js")
        Dim w As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global-.js")
        Dim line As String = ""

        While ((Not r.EndOfStream))
            line = r.ReadLine()
            If (line.StartsWith("var endUserConfigUrl =")) Then
                w.WriteLine("var endUserConfigUrl =" & endUserConfigUrl)

            ElseIf (line.StartsWith("var newestMachinesRecordsUrl =")) Then
                w.WriteLine("var newestMachinesRecordsUrl =" & newestMachinesRecordsUrl)

            ElseIf (line.StartsWith("var timelineUrl = ")) Then
                w.WriteLine("var timelineUrl = " & timelineUrl)

            Else
                w.WriteLine(line)
            End If

        End While
        r.Close()
        w.Close()
        File.Delete(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global.js")
        Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\Global-.js", "global.js")
end_:
    End Sub

    Private Sub Load_rm_port()

        Try
            Dim port_ As String = "8008"
            If File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys") Then

                Using rdr As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                    port_ = rdr.ReadLine
                    ServerPort = port_
                    rdr.Close()
                End Using
            Else
                Using wrt As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                    wrt.WriteLine(port_)
                    wrt.Close()
                End Using
            End If

            If (MySqlAccess.ExecuteNonQuery($"UPDATE CSI_database.tbl_RM_Port SET port = { port_ }") = 0) Then
                MySqlAccess.ExecuteNonQuery($"INSERT INTO CSI_database.tbl_RM_Port ( port ) VALUES ( {port_} )")
            End If

        Catch ex As Exception

            MsgBox("Could not load or open a port")
            Log.Error(ex)

        End Try

    End Sub

    Private Sub Tickhandle()
        LBL_IPChange.Visible = False
        LBL_IPChange.Text = String.Empty
        CSI_Library.CSI_Library.IPChange = 0
        t1.Dispose()
        t2.Dispose()
        t4.Dispose()
        t5.Dispose()
    End Sub

    Private Sub Load_groupes()

        Try
            Dim dtGroups As DataTable = MySqlAccess.GetDataTable("SELECT * from CSI_database.tbl_Groups order by `groups`, machines")

            Dim dtMachines As DataTable = MySqlAccess.GetDataTable("SELECT * from csi_auth.tbl_ehub_conf")

            treeviewGroupsOfMachines.Nodes(0).Nodes.Clear()

            Dim i As Integer = -1
            Dim group As String = ""
            Dim node As TreeNode

            For Each row As DataRow In dtGroups.Rows

                If Not row("groups").ToString() = group Then
                    node = New TreeNode(row("groups").ToString())
                    node.Name = row("groups").ToString()
                    node.Tag = "0"
                    treeviewGroupsOfMachines.Nodes(0).Nodes.Add(node)
                    group = row("groups").ToString()
                    i += 1
                End If

                Dim rowMachine As DataRow = dtMachines.Select().Where(Function(m) m.Item("id").ToString() = row("machineId").ToString()).FirstOrDefault()

                If Not IsNothing(rowMachine) Then
                    node = New TreeNode()
                    node.Name = rowMachine("Machine_Name").ToString()
                    node.Text = rowMachine("Machine_Name").ToString()
                    node.Tag = rowMachine("Id").ToString()
                    node.ForeColor = Color.Gray
                    treeviewGroupsOfMachines.Nodes(0).Nodes(i).Nodes.Add(node)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Unable to Load the machines groups : " & ex.Message)
        End Try

    End Sub

    Public Sub RefreshDevice(deviceId As String)
        Try
            If Not inLoadingDeviceMode Then

                'Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
                'cntsql.Open()
                'Dim cmdsql As New MySqlCommand("update csi_database.tbl_deviceconfig set refreshbrowser='refreshing' where IP='" & deviceip & "'", cntsql)
                'cmdsql.ExecuteNonQuery()
                'cntsql.Close()

                MySqlAccess.ExecuteNonQuery($"UPDATE csi_database.tbl_deviceconfig SET refreshbrowser = 'refreshing' WHERE DeviceId = { deviceId }")
                send_http_req()

            End If

        Catch ex As Exception

            Log.Error("Unable to ask for refresh.", ex)

        End Try
    End Sub

    Private Sub UpdateTargetsTitle()
        Try

            Dim cmmd As String = "SELECT * from csi_machineperf.tbl_perf where machinename_ like 'TitleTarget_Grp_%'"
            Dim dadapter_name As New MySqlDataAdapter(cmmd, CSI_Library.CSI_Library.MySqlConnectionString)
            Dim PERF_TBL As New DataTable
            dadapter_name.Fill(PERF_TBL)
            Dim Groups_perf_w As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
            Dim Groups_perf_m As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
            Groups_dic = LoadGroups()

            Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)

            MonthlyTargets.Clear()
            WeeklyTargets.Clear()

            For Each ro As DataRow In PERF_TBL.Rows

                Dim Group As String = Convert.ToString(ro(0)).Replace("TitleTarget_Grp_", "")
                Dim TMP_str_w As String = Convert.ToString(ro(1)).Replace("Grp_" + Group + " ", "")
                Dim TMP_str_m As String = Convert.ToString(ro(2)).ToString().Replace("Grp_" + Group + " ", "")
                Dim w_H As String = Regex.Split(TMP_str_w, " of ")(0).Replace("h", "")
                Dim w_M As String = Regex.Split(TMP_str_m, " of ")(0).Replace("h", "")

                Groups_perf_w.Add(Group, Convert.ToInt32(Regex.Split(TMP_str_w, " of ")(0).Replace("h", "")))
                Groups_perf_m.Add(Group, Convert.ToInt32(Regex.Split(TMP_str_m, " of ")(0).Replace("h", "")))

            Next

            For Each row In gridviewMachines.Rows
                For Each Group In Groups_dic.Keys
                    Dim machinename As String = gridviewMachines.Item(1, row.index).Value.ToString()
                    If Groups_dic(Group).Contains(machinename) Then
                        If MonthlyTargets.ContainsKey(Group) Then
                            MonthlyTargets(Group) = MonthlyTargets(Group) + Convert.ToInt32(gridviewMachines.Item(8, row.index).Value.ToString())
                            WeeklyTargets(Group) = WeeklyTargets(Group) + Convert.ToInt32(gridviewMachines.Item(7, row.index).Value.ToString())
                        Else
                            MonthlyTargets.Add((Group), Convert.ToInt32(gridviewMachines.Item(8, row.index).Value.ToString()))
                            WeeklyTargets.Add((Group), Convert.ToInt32(gridviewMachines.Item(7, row.index).Value.ToString()))
                        End If

                    End If
                Next
            Next

            cntsql.Open()
            For Each Group In MonthlyTargets.Keys
                Dim TargetTile_Monthly As String = "Grp_" + Group & " " & Math.Round(Groups_perf_m(Group)) & "h of " & Math.Round(MonthlyTargets(Group)) & "h"
                Dim TargetTile_weekly As String = "Grp_" + Group & " " & Math.Round(Groups_perf_w(Group)) & "h of " & Math.Round(WeeklyTargets(Group)) & "h"
                cmmd = "update csi_machineperf.tbl_perf Set monthly_ = '" + TargetTile_Monthly + "', weekly_ = '" + TargetTile_weekly + "' where machinename_ = 'TitleTarget_Grp_" + Group + "'"
                Dim cmdsql2 As New MySqlCommand(cmmd, cntsql)
                cmdsql2.ExecuteNonQuery()

            Next
            cntsql.Close()

        Catch ex As Exception
            CSI_Lib.LogServiceError("Err while trying to read the perf from the database : " & ex.Message, 1)
        Finally

        End Try
    End Sub

    Public Sub RefreshAllDevices()
        Try
            If Not inLoadingDeviceMode Then
                send_http_req()

                Dim portT As New DataTable

                Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select port From csi_database.tbl_rm_port;", CSI_Library.CSI_Library.MySqlConnectionString)
                dadapter_name.Fill(portT)

                Dim request As WebRequest

                If portT.Rows.Count <> 0 Then
                    If IsDBNull(portT.Rows(0)("port")) Then
                        request = WebRequest.Create("http://127.0.0.1:8008/readPerf")
                    Else
                        request = WebRequest.Create("http://127.0.0.1:" & portT.Rows(0)("port") & "/readPerf")
                    End If
                End If

                request.Method = "POST"

                Dim postData As String = ""
                Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
                ' Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded"
                ' Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length
                ' Get the request stream.
                Dim dataStream As Stream = request.GetRequestStream()
                ' Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length)
                ' Close the Stream object.

                dataStream.Close()

            End If

        Catch ex As Exception
            Log.Error(ex)
        End Try
    End Sub

    Sub send_http_req(Optional extension As String = "readconfig")

        Try

            Dim portT As DataTable = MySqlAccess.GetDataTable("SELECT port FROM csi_database.tbl_rm_port;")

            Dim request As WebRequest

            If portT.Rows.Count > 0 Then
                If IsDBNull(portT.Rows(0)("port")) Then
                    request = WebRequest.Create($"http://127.0.0.1:8008/{ extension }")
                Else
                    request = WebRequest.Create($"http://127.0.0.1:{ portT.Rows(0)("port") }/{ extension }")
                End If
            Else
                request = WebRequest.Create("http://127.0.0.1:8008/readconfig")
            End If

            Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes("")

            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length

            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()

        Catch ex As Exception

            Log.Error(ex)

        End Try

    End Sub

    Public Function LoadGroups() As Dictionary(Of String, List(Of String))

        Dim tmp_tbl_ As New DataTable
        Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select * FROM csi_database.tbl_groups", CSI_Library.CSI_Library.MySqlConnectionString)
        dadapter_name.Fill(tmp_tbl_)

        Dim ___ As New Dictionary(Of String, List(Of String))

        For Each ro As DataRow In tmp_tbl_.Rows
            If ___.ContainsKey(ro("groups")) Then
                If Not ro("machines") = "" Then ___(ro("groups")).Add(ro("machines"))
            Else
                ___.Add(ro("groups"), New List(Of String))
                ___(ro("groups")).Add(ro("machines"))
            End If
        Next

        Return ___

    End Function

    Public Sub ComputePerfReq()
        Try
            If Not inLoadingDeviceMode Then
                Srv_read_req()
                Dim portT As New DataTable
                Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select port From csi_database.tbl_rm_port;", CSI_Library.CSI_Library.MySqlConnectionString)
                dadapter_name.Fill(portT)

                Dim request As WebRequest

                If portT.Rows.Count <> 0 Then
                    If IsDBNull(portT.Rows(0)("port")) Then
                        request = WebRequest.Create("http://127.0.0.1:8008/ComputePerf")
                    Else
                        request = WebRequest.Create("http://127.0.0.1:" & portT.Rows(0)("port") & "/ComputePerf")
                    End If
                End If

                request.Method = "POST"

                Dim postData As String = ""
                Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
                ' Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded"
                ' Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length
                ' Get the request stream.
                Dim dataStream As Stream = request.GetRequestStream()
                ' Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length)
                ' Close the Stream object.

                dataStream.Close()
            End If
        Catch ex As Exception
            Log.Error(ex)
        End Try
    End Sub

    Private Function Srv_read_req() As Boolean
        Try
            Dim portT As New DataTable
            Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select port From csi_database.tbl_rm_port;", CSI_Library.CSI_Library.MySqlConnectionString)

            dadapter_name.Fill(portT)

            Dim request As WebRequest

            If portT.Rows.Count <> 0 Then
                If IsDBNull(portT.Rows(0)("port")) Then
                    request = WebRequest.Create("http://127.0.0.1:8008/readconfig")
                Else
                    request = WebRequest.Create("http://127.0.0.1:" & portT.Rows(0)("port") & "/readconfig")
                End If
            End If

            request.Method = "POST"

            Dim postData As String = ""
            Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
            ' Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded"
            ' Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length
            ' Get the request stream.
            Dim dataStream As Stream = request.GetRequestStream()
            ' Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length)
            ' Close the Stream object.

            Dim myWebResponse As WebResponse = request.GetResponse()
            Dim dataStreamR As Stream = myWebResponse.GetResponseStream()
            Dim reader As New StreamReader(dataStreamR)
            Dim responseFromServer As String = reader.ReadToEnd()


            dataStream.Close()
            ' Get the response.
            If responseFromServer = "ok,thanks ;)" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Log.Error(ex)
            Return False

        End Try
    End Function

    Public Function GetIPv4Address() As String

        GetIPv4Address = String.Empty

        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                GetIPv4Address = ipheal.ToString()
            End If
        Next

    End Function

End Class
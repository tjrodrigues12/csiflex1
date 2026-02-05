Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports System.Windows
Imports System.Xml
Imports CSI_Library
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities
Imports Microsoft.Win32


'=========================================================================================================================================
'=========================================================================================================================================
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄ 
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀  ▀▀▀▀█░█▀▀▀▀ 
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░▌          ▐░▌               ▐░▌     
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░▌          ▐░█▄▄▄▄▄▄▄▄▄      ▐░▌     
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░▌          ▐░░░░░░░░░░░▌     ▐░▌     
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░▌           ▀▀▀▀▀▀▀▀▀█░▌     ▐░▌     
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░▌                    ▐░▌     ▐░▌     
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░█▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄█░▌ ▄▄▄▄█░█▄▄▄▄ 
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ''▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌
' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' '' ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀  Flex Reporting Client Side 
'=========================================================================================================================================
'=========================================================================================================================================

Public Class Reporting_application

    Public CSFIFLEX_VERSION As String = "1.8.9.4"

    Public User_Connection_String As String = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={ Forms.Application.StartupPath }\sys\CSI_auth.mdb; Jet OLEDB:Database Password=4Solutions; mode=ReadWrite;"
    Public LocalDB_Connection_String As String = ""
    Public ServerDB_Connection_String As String = ""
    'Public Shared cs As String = CSI_Library.CSI_Library.sqlitedbpath
    Public username_ As String
    Public Shared setupForm_Singleton As Reporting_availability
    Public minyear As Date
    Public maxyear As Date
    Public eHUBConf As New Dictionary(Of String, String) ' Used to get the shift setup
    Public MonSetup As New Dictionary(Of String, String) ' Used to get the shift setup
    Public ShiftSetup As New Dictionary(Of String, String)
    Public before2012 As Boolean = False
    Public CSI_Lib As New CSI_Library.CSI_Library(False)
    Public stat(3) As String
    Public chemin_bd As String
    Public Image1 As Image, bySetup As Boolean = False
    Public machine_list As New Dictionary(Of String, String)

    Public SRV_Version As Integer = 0

    Public First_date As Date

    Public week_ As String = 15
    Public year_ As String = 1
    Public activated_ As Boolean = False
    Public first_creation_of_the_DB As Boolean = False
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath

    Public continue_updating As Boolean = True

    Private Sub Reporting_application_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        UserLogin.Close()
        Environment.Exit(0)

    End Sub


    Private Sub ConfigurationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfigurationToolStripMenuItem.Click
        bySetup = True
        SetupForm.MdiParent = Me
        SetupForm.StartPosition = FormStartPosition.CenterScreen
        SetupForm.Show()
    End Sub


    Private Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        AboutBox1.StartPosition = FormStartPosition.CenterParent
        AboutBox1.Show()
    End Sub


    Private Sub Reporting_application_Resize0(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        Me.SuspendLayout()
    End Sub


    Private Sub Reporting_application_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd

        Me.Update()
        Me.ResumeLayout()
        Me.Refresh()

        Config_report.Location = New System.Drawing.Point(0, 0)
        Report_BarChart.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, 0)
        Report_BarChart.Height = Config_report.Height
        Report_BarChart.Refresh()
        Me.PerformLayout()

    End Sub


    Private Sub reporting_application_size_change(sender As Object, e As EventArgs) Handles MyBase.SizeChanged

        Config_report.Height = Me.Height - 112

        Report_BarChart.Height = Config_report.Height
        Report_BarChart.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Size.Width, 0)


        Report_PieChart.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, 0)
        Report_PieChart.Height = Config_report.Height

        Machine_util_det.Location = New System.Drawing.Point(Report_BarChart.Size.Width + Config_report.Size.Width, Report_BarChart.Location.Y)
        Machine_util_det.Height = Report_BarChart.Height

    End Sub


    Private Sub Reporting_application_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CSI_Library.CSI_Library.isServer = False

        Dim splash As New SplashScreen
        splash.Show()

        Me.Height = Screen.PrimaryScreen.Bounds.Height - 200
        Me.Width = Screen.PrimaryScreen.Bounds.Width
        Me.WindowState = FormWindowState.Maximized

        ToolStripStatusLabel2.Text = ""

        Log.Instance.Init("CSIFLEXClient")
        Log.Info("CSIFLEX Client Started")
        Log.Info("==============================================================================")

        CSI_Lib = New CSI_Library.CSI_Library(False)
        CSIFLEXSettings.Instance.Init()

        For Each ctl As Control In Me.Controls
            If TypeOf ctl Is MdiClient Then
                ctl.BackColor = Color.FromArgb(237, 239, 240)
            End If
        Next ctl

        If Not Utilities.IsValidIpAddress(CSIFLEXSettings.Instance.DatabaseIp, True) Then

            If Not Welcome.ShowDialog() = DialogResult.OK Then
                Return
            End If

        End If

        Dim connectionString As String = CSIFLEXSettings.Instance.ConnectionString
        Dim dbMessage = MySqlAccess.hasConnection2(connectionString)

        If Not dbMessage = "True" Then
            MessageBox.Show($"Database '{ CSIFLEXSettings.Instance.DatabaseIp }' is not available! { dbMessage }")
            Environment.Exit(0)
        End If

        CSI_Library.CSI_Library.MySqlConnectionString = connectionString

        CSI_Library.CSI_Library.SetConnString(connectionString)

        CSI_Lib.connectionString = connectionString

        Dim licenseLib As New CSILicenseLibrary(connectionString)
        Dim license As New license()
        licenseLib.LoadCsiflexClientLicense(license)

        If Not license.Status = "Valid" Or DateDiff(DateInterval.Day, Today, license.ExpiryDate) <= 10 Then
            Dim licenseForm As New LicenseManagement(connectionString)
            If Not licenseForm.ShowDialog() = DialogResult.OK Then
                Environment.Exit(0)
                End
            End If
        End If

        activated_ = True

        If Not UserLogin.ShowDialog() = DialogResult.OK Then
            Environment.Exit(0)
            Return
        End If


        MenuStrip1.ForeColor = Color.Black
        Me.BackColor = Color.FromArgb(237, 239, 240)

        'Check Config Data ================================================================
        Try
            Dim userMachines As String = MySqlAccess.ExecuteScalar($"SELECT machines FROM csi_auth.Users WHERE Username_ = '{ CSIFLEXSettings.Instance.UserName }' LIMIT 1", connectionString)
            Dim tbMchs = MySqlAccess.GetDataTable("SELECT Id FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1;")
            Dim ltMchs = New List(Of String)()

            For Each mch As DataRow In tbMchs.Rows
                ltMchs.Add(mch("Id"))
            Next

            If userMachines.ToUpper().StartsWith("ALL") Then
                userMachines = String.Join(", ", ltMchs.ToArray())
            End If

            CSIFLEXSettings.GroupMachines = New List(Of String)()
            CSIFLEXSettings.GroupMachines.Add("_MT_Assets : ")
            CSIFLEXSettings.GroupMachines.Add("_ST_All Machines : ")

            For Each mach As String In Split(userMachines, ",")

                If ltMchs.Contains(mach) Then
                    If CSIFLEXSettings.UserMachines.ContainsKey(mach) Then
                        CSIFLEXSettings.GroupMachines.Add(CSIFLEXSettings.UserMachines(mach).Item1)
                    Else
                        Dim item = CSIFLEXSettings.UserMachines.FirstOrDefault(Function(i) i.Value.Item1 = mach Or i.Value.Item2 = mach)
                        If Not IsNothing(item) Then
                            CSIFLEXSettings.GroupMachines.Add(item.Value.Item1)
                        End If
                    End If
                    Log.Info($"=>Machine: {mach}")
                End If
            Next

            Dim machines As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.tbl_groups WHERE machines <> '' ORDER BY `groups`, machines", connectionString)
            Dim grp As String = ""

            For Each machine As DataRow In machines.Rows

                If CSIFLEXSettings.UserMachines.ContainsKey(machine("machineId")) Then

                    If Not machine.Item("groups").ToString() = grp Then
                        CSIFLEXSettings.GroupMachines.Add($"_ST_{machine.Item("groups").ToString()}")
                        grp = machine.Item("groups").ToString()
                    End If

                    CSIFLEXSettings.GroupMachines.Add(CSIFLEXSettings.UserMachines(machine("machineId")).Item1)
                End If
            Next

            CSIFLEXSettings.Machines = New List(Of String)()
            For Each mach As String In CSIFLEXSettings.GroupMachines
                If Not mach.StartsWith("_MT") And Not mach.StartsWith("_ST") And Not CSIFLEXSettings.Machines.Contains(mach) Then
                    CSIFLEXSettings.Machines.Add($"{mach},eNET,1")
                End If
            Next

            CSIFLEXSettings.StatusColors = New Dictionary(Of String, Integer)()
            Dim tblColors = CSI_Lib.Read_colors_from_database(CSIFLEXSettings.Instance.ConnectionString)
            For Each row In tblColors.Rows
                Dim s As String = row.Item(0).ToString()
                CSIFLEXSettings.StatusColors.Add(row.item(0).ToString(), ColorTranslator.ToWin32(ColorTranslator.FromHtml(row.Item(1).ToString())))
            Next

            Report_PieChart.MdiParent = Me
            Config_report.MdiParent = Me
            Machine_util_det.MdiParent = Me

            SynchroDB.MdiParent = Me

            Me.LayoutMdi(MdiLayout.Cascade)
            SetStyle(ControlStyles.DoubleBuffer, True)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            SetStyle(ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        Catch ex As Exception
            Log.Error("Unable to load config files.", ex)
            MsgBox($"Unable to load config files. {ex.Message}")
        End Try

        If activated_ = False Then Me.Close()

        Dim lic As Integer = Welcome.CSIF_version
        Dim srv_thread As New Thread(AddressOf fct_get_enet_livestatus)
        srv_thread.Name = "CSIFLEX Real time monitoring thread"
        srv_thread.Start()

        Try
            CSI_Library.CSI_Library.loadingasCON = CSI_Lib.GetLoadingAsCON(connectionString)
        Catch ex As Exception
            Log.Error("pb with the db path", ex)
        End Try

        Me.MinimizeBox = True

        First_date = set_firstdate()

        Config_report.Show()
        check_srv_version.RunWorkerAsync()

    End Sub


    Public SRV_UDT_NEEDED As Boolean = False


    'Private Sub Load_rm_port()
    '    Try
    '        Dim port_ As String = "8008"

    '        Dim db_authPath As String
    '        If (File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
    '            Using streader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath + "\sys\SrvDBpath.csys")
    '                db_authPath = streader.ReadLine()
    '            End Using
    '        End If


    '        Dim cnt As MySqlConnection = New MySqlConnection("SERVER=" + db_authPath + ";" + "DATABASE=csi_auth;" + CSI_Library.CSI_Library.MySqlServerBaseString)
    '        cnt.Open()
    '        If cnt.State = ConnectionState.Open Then


    '            Dim query As String = "select port from  CSI_database.tbl_RM_Port"

    '            Dim cmd As New MySqlCommand(query, cnt)
    '            Dim dtrdr As MySqlDataReader


    '            dtrdr = cmd.ExecuteReader
    '            dtrdr.Read()
    '            port_ = dtrdr.Item(0)
    '            cnt.Close()
    '        End If


    '        If File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys") Then
    '            File.Delete(CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys")
    '        End If

    '        Using wrt As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys")
    '            wrt.WriteLine(port_)
    '            wrt.Close()
    '        End Using


    '    Catch ex As Exception
    '        MsgBox("Could not load the rainmeter port")
    '        CSI_Lib.LogClientError(ex.Message)
    '    End Try

    'End Sub


    Public Function set_firstdate()

        Dim First_date As Date = New Date(Now.Year, 1, 1)
        Return First_date

    End Function


    Private Sub fct_get_enet_livestatus()

        Dim dt As New DataTable
        Dim dic As New Dictionary(Of String, EMachine)(StringComparer.CurrentCultureIgnoreCase)

        While continue_updating

            Try
                dt = GetEnetPage() 'clt.Run(GetEnetIp())
            Catch ex As Exception
                Log.Error("ERROR getting data from http server", ex)
            End Try

            Try
                dic.Clear()
                If Not (dt Is Nothing) Then
                    For Each row As DataRow In dt.Rows
                        Try
                            dic.Add(row.Item("machine"), New EMachine(row, "enet"))
                        Catch ex As Exception
                            Log.Error($"ERROR machine: { row.Item("machine")}{Environment.NewLine}{ row.Item("status")}{Environment.NewLine}{row.Item("PartNumber")}{Environment.NewLine}{row.Item("PartNumber")}{Environment.NewLine}{row.Item("CycleCount")}{ Environment.NewLine }", ex)
                        End Try
                    Next
                End If
                'Return dic
                GlobalVariables.ListOfMachine = dic
            Catch ex As Exception
                Log.Error("Error getting live status from enet", ex)
            End Try

            Thread.Sleep(5300)
        End While
    End Sub


    Private Function GetEnetPage() As DataTable

        Try
            Dim table As New DataTable

            table.Columns.Add("MachineId", GetType(Integer))
            table.Columns.Add("Machine", GetType(String))
            table.Columns.Add("EnetMachine", GetType(String))
            table.Columns.Add("Shift", GetType(String))
            table.Columns.Add("status", GetType(String))
            table.Columns.Add("PartNumber", GetType(String))
            table.Columns.Add("OperatorRefId", GetType(String))
            table.Columns.Add("Condition", GetType(String))
            table.Columns("Condition").AllowDBNull = True
            table.Columns.Add("CycleCount", GetType(String))
            table.Columns.Add("LastCycle", GetType(String))
            table.Columns.Add("CurrentCycle", GetType(String))
            table.Columns.Add("Comment", GetType(String))
            table.Columns.Add("ElapsedTime", GetType(String))
            table.Columns.Add("StatusDatetime", GetType(String))
            table.Columns.Add("FeedOverride", GetType(String))
            table.Columns.Add("SpindleOverride", GetType(String))

            GetCurrentStatus(table)
            Return table

        Catch ex As Exception
            Log.Error("ERROR getting data from http server", ex)
            Return Nothing
        End Try

    End Function


    Private Structure Data
        Public status As String
        Public shift As String
        Public PartNumber As String
        Public CycleCount As String
        Public LastCycle As String
        Public CurrentCycle As String
        Public ElapsedTime As String
        Public feedOverride As String
    End Structure


    Private Sub GetCurrentStatus(ByRef table)

        Dim sqlCmd As New Text.StringBuilder
        Dim machTab As String

        For Each machine As KeyValuePair(Of Integer, Tuple(Of String, String)) In CSIFLEXSettings.UserMachines

            'Log.Info($"->Machine: {machine.Key}")

            machTab = CSI_Lib.RenameMachine(machine.Value.Item2)

            sqlCmd.Clear()
            sqlCmd.Append($"SELECT *                          ")
            sqlCmd.Append($" FROM                             ")
            sqlCmd.Append($"    csi_machineperf.tbl_{machTab} ")
            sqlCmd.Append($" WHERE                            ")
            sqlCmd.Append($"    Status NOT LIKE '\_%'         ")
            sqlCmd.Append($" ORDER BY Id DESC LIMIT 1         ")

            Dim temp_table = MySqlAccess.GetDataTable(sqlCmd.ToString(), CSIFLEXSettings.Instance.ConnectionString)

            If temp_table.Rows.Count > 0 Then

                Dim row As DataRow = table.NewRow

                row("Machine") = machine.Value.Item2
                row("ElapsedTime") = 0
                row("CurrentCycle") = temp_table.Rows(0)("cycletime")
                row("shift") = temp_table.Rows(0)("shift")
                row("status") = temp_table.Rows(0)("status")

                row("feedOverride") = 0

                row("LastCycle") = 0
                row("CycleCount") = 0
                row("PartNumber") = temp_table.Rows(0)("PartNumber")
                row("OperatorRefId") = temp_table.Rows(0)("Operator")

                table.Rows.Add(row)
            End If
        Next

    End Sub


    Function ExcelConnection() As String

        Dim strConnect As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\_MACHINE_2013.xlsx; Extended Properties = Excel 12.0 Xml;HDR=YES ; Format=xlsx;"

        Return strConnect
    End Function


    '-----------------------------------------------------------------------------------------------------------------------
    ' 1st button Reporting_application
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub DailyReportToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Config_report.Show()
    End Sub


    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Report_PieChart.Show()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Config_report.StartPosition = FormStartPosition.CenterScreen
        Config_report.Show()
    End Sub


    Private Sub ConfigurationToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConfigurationToolStripMenuItem1.Click
        ConfigurationToolStripMenuItem_Click(sender, e)
    End Sub


    Public Function IsInstalled() As Boolean
        Dim uninstallKey As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"
        Dim ReportViewerExist As Boolean = False
        Using rk As RegistryKey = Registry.LocalMachine.OpenSubKey(uninstallKey)
            For Each skName As String In rk.GetSubKeyNames()
                Using sk As RegistryKey = rk.OpenSubKey(skName)

                    If sk.GetValue("DisplayName") & "  " & sk.GetValue("DisplayVersion") = "Microsoft Report Viewer 2012 Runtime  11.1.3452.0" Then
                        ReportViewerExist = True
                    End If

                End Using
            Next
        End Using

        Return ReportViewerExist

    End Function


    Public Sub New()

        InitializeComponent()

        If Not (IsInstalled()) Then
            CSI_Lib.installReportViewer()
        End If

    End Sub


    Private Shared Function CurrentDomain_AssemblyResolve(sender As Object, args As ResolveEventArgs) As System.Reflection.Assembly
        Return EmbeddedAssembly.[Get](args.Name)
    End Function


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Environment.Exit(0)
    End Sub


    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim about As New AboutBox1
        about.StartPosition = FormStartPosition.CenterScreen
        about.ShowDialog()
    End Sub


    Private Sub AvailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AvailToolStripMenuItem.Click

        setupForm_Singleton = New Reporting_availability()
        setupForm_Singleton.StartPosition = FormStartPosition.CenterScreen
        setupForm_Singleton.Show()
        setupForm_Singleton.Focus()
    End Sub


    Private Sub PartsNoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PartsNoToolStripMenuItem.Click

        Dim test As New Reporting_partsNumber()
        test.Show()
    End Sub


    Private Sub RawDataToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Dim folderDlg As New SaveFileDialog
        folderDlg.ShowDialog()

        If (folderDlg.FileName <> "") Then
            generateCSVaccordingToConfig_report(folderDlg.FileName())
        End If

    End Sub


    Private Sub generateCSVaccordingToConfig_report(filePath As String)

        setData(filePath)

    End Sub


    Private Sub generateCSV_file(filepath As String)

        setData(filepath)

    End Sub


    Public Function ToCSV(table As DataTable, filePath As String, firstFill As Boolean)

        Using sw As StreamWriter = New StreamWriter(filePath, Not firstFill)

            Dim byeList As New List(Of Byte)

            Dim result As New System.Text.StringBuilder()

            If (firstFill = True) Then
                For i As Integer = 0 To table.Columns.Count - 1

                    result.Append(table.Columns(i).ColumnName)
                    result.Append(If(i = table.Columns.Count - 1, vbLf, ","))

                Next
                sw.Write(result)
            End If

            For Each row As DataRow In table.Rows
                result = New System.Text.StringBuilder()

                For i As Integer = 0 To table.Columns.Count - 1

                    result.Append(row(i).ToString())
                    result.Append(If(i = table.Columns.Count - 1, vbLf, ","))

                Next

                sw.Write(result)
            Next

        End Using

    End Function


    Private Function setData(filepath As String) As DataTable

        Dim machines() As String = Config_report.read_tree()

        Dim command As String = ""

        Dim bigTable, temp_table As New DataTable
        Dim firstFillorNot As Boolean = True
        Dim sqlCmd As New Text.StringBuilder
        Dim machTab As String
        Dim startDate As String = Config_report.DTP_StartDate.Value.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))
        Dim endDate As String = Config_report.DTP_EndDate.Value.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))

        For Each machine As String In machines

            machTab = CSI_Lib.RenameMachine(machine)

            sqlCmd.Clear()
            sqlCmd.Append($"SELECT                         ")
            sqlCmd.Append($"    '{machine}' AS MchName   , ")
            sqlCmd.Append($"    month_                   , ")
            sqlCmd.Append($"    day_                     , ")
            sqlCmd.Append($"    year_                    , ")
            sqlCmd.Append($"    ShiftDate                , ")
            sqlCmd.Append($"    date_                    , ")
            sqlCmd.Append($"    CASE WHEN `status` LIKE '_PARTNO:%' THEN Mid(Status, 9) ELSE status END status, ")
            sqlCmd.Append($"    shift                    , ")
            sqlCmd.Append($"    cycletime                  ")
            sqlCmd.Append($" FROM                          ")
            sqlCmd.Append($"    csi_database.tbl_{machTab} ")
            sqlCmd.Append($" USE INDEX()                   ")
            sqlCmd.Append($" WHERE                         ")
            sqlCmd.Append($"    ShiftDate BETWEEN          ")
            sqlCmd.Append($"       '{ startDate }'         ")
            sqlCmd.Append($"       AND                     ")
            sqlCmd.Append($"       '{ endDate }'           ")

            temp_table = MySqlAccess.GetDataTable(sqlCmd.ToString(), CSIFLEXSettings.Instance.ConnectionString)

            ToCSV(temp_table, filepath, firstFillorNot)
            firstFillorNot = False
        Next

        Return bigTable

    End Function


    Public Shared Sub Deserialise(c As Control, XmlFileName As String)

        If File.Exists(XmlFileName) Then
            Dim xmlSerialisedForm As New XmlDocument()
            xmlSerialisedForm.Load(XmlFileName)
            Dim topLevel As XmlNode = xmlSerialisedForm.ChildNodes(1)
            For Each n As XmlNode In topLevel.ChildNodes
                SetControlProperties(DirectCast(c, Control), n)
            Next
        End If

    End Sub


    Private Shared Sub SetControlProperties(currentCtrl As Control, n As XmlNode)

        ' get the control's name and type
        Dim controlName As String = n.Attributes("Name").Value
        Dim controlType As String = n.Attributes("Type").Value
        ' find the control
        Dim ctrl As Control() = currentCtrl.Controls.Find(controlName, True)
        ' can't find the control
        If ctrl.Length = 0 Then
        Else
            Dim ctrlToSet As Control = GetImmediateChildControl(ctrl, currentCtrl)
            If ctrlToSet IsNot Nothing Then
                If ctrlToSet.[GetType]().ToString() = controlType Then
                    ' the right type too ;-)
                    Select Case controlType
                        Case "System.Windows.Forms.TextBox"
                            DirectCast(ctrlToSet, System.Windows.Forms.TextBox).Text = n("Text").InnerText
                            Exit Select
                        Case "System.Windows.Forms.ComboBox"
                            DirectCast(ctrlToSet, System.Windows.Forms.ComboBox).Text = n("Text").InnerText
                            DirectCast(ctrlToSet, System.Windows.Forms.ComboBox).SelectedIndex = Convert.ToInt32(n("SelectedIndex").InnerText)
                            Exit Select
                        Case "System.Windows.Forms.ListBox"
                            ' need to account for multiply selected items
                            Dim lst As ListBox = DirectCast(ctrlToSet, ListBox)
                            Dim xnlSelectedIndex As XmlNodeList = n.SelectNodes("SelectedIndex")
                            For i As Integer = 0 To xnlSelectedIndex.Count - 1
                                lst.SelectedIndex = Convert.ToInt32(xnlSelectedIndex(i).InnerText)
                            Next
                            Exit Select
                        Case "System.Windows.Forms.CheckBox"
                            DirectCast(ctrlToSet, System.Windows.Forms.CheckBox).Checked = Convert.ToBoolean(n("Checked").InnerText)
                            Exit Select
                    End Select
                    ctrlToSet.Visible = Convert.ToBoolean(n("Visible").InnerText)
                    ' if n has any children that are controls, deserialise them as well
                    If n.HasChildNodes AndAlso ctrlToSet.HasChildren Then
                        Dim xnlControls As XmlNodeList = n.SelectNodes("Control")
                        For Each n2 As XmlNode In xnlControls
                            SetControlProperties(ctrlToSet, n2)
                        Next
                    End If
                    ' not the right type
                Else
                End If
                ' can't find a control whose parent is the current control
            Else
            End If
        End If
    End Sub


    Private Shared Function GetImmediateChildControl(ctrl As Control(), currentCtrl As Control) As Control
        Dim c As Control = Nothing
        For i As Integer = 0 To ctrl.Length - 1
            If (ctrl(i).Parent.Name = currentCtrl.Name) OrElse (TypeOf currentCtrl Is SplitContainer AndAlso ctrl(i).Parent.Parent.Name = currentCtrl.Name) Then
                c = ctrl(i)
                Exit For
            End If
        Next
        Return c
    End Function


    Private Sub CsvFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CsvFileToolStripMenuItem.Click

        Dim folderDlg As New SaveFileDialog
        folderDlg.Filter = "csv Files | *.csv"
        folderDlg.ShowDialog()

        If (folderDlg.FileName <> "") Then
            generateCSVaccordingToConfig_report(folderDlg.FileName())
        End If

    End Sub


    Private Sub TestSqliteToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'CSI_Lib.UpdateDB_Sqlite()
    End Sub


    Private Sub EventReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EventReportToolStripMenuItem.Click
        Event_report.Show()
    End Sub


    Private Sub Button1_Click_2(sender As Object, e As EventArgs)
        MsgBox(Me.Height)
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs)
        MsgBox(Config_report.Size.Height)
    End Sub


    Private Sub Button1_Click_3(sender As Object, e As EventArgs)
        MsgBox(Me.Size.Height)
    End Sub


    Private Sub CsvFilterToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Dim csvFilterForm As CSVFilterForm = New CSVFilterForm()
        csvFilterForm.MdiParent = Me
        csvFilterForm.StartPosition = FormStartPosition.CenterScreen
        csvFilterForm.Show()
    End Sub

    Private Sub CSVFilesGeneratorNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CSVFilesGeneratorNewToolStripMenuItem.Click

        Dim eventsReport As EventsReport = New EventsReport()
        eventsReport.MdiParent = Me
        eventsReport.StartPosition = FormStartPosition.CenterScreen
        eventsReport.Show()
    End Sub

    Private Sub PartNumbersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PartNumbersToolStripMenuItem.Click

        Dim formPartNumbers As formPartNumbers = New formPartNumbers()
        formPartNumbers.MdiParent = Me
        formPartNumbers.StartPosition = FormStartPosition.CenterScreen
        formPartNumbers.Show()

    End Sub

End Class

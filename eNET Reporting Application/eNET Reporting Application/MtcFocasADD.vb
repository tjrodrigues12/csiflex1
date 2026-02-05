Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text.RegularExpressions
Imports CSIFLEX.Database.Access
Imports CSIFLEX.eNetLibrary
Imports CSIFLEX.eNetLibrary.Data
Imports CSIFLEX.Server.Library
Imports CSIFLEX.Server.Library.DataModel
Imports CSIFLEX.Utilities
Imports FocasLibrary.Tools
Imports MySql.Data.MySqlClient
Imports OpenNETCF.MTConnect


Public Class MtcFocasADD

    Public Shared focasinst As New FocasInstaller()
    Public objMainWindow As New FocasLibrary.MainWindow()
    Public objdeviceInfo As New FocasLibrary.Components.DeviceInfo()
    Public objAdapterInfo As New FocasLibrary.Components.AdapterInfo()
    Public IPString As String = String.Empty
    Public minHeight As Integer = 608
    Public storedHeight As Integer = 847
    Public TabHeight As Integer

    Dim focasPingOk As Boolean = False

    Dim connectorId As Integer = 0
    Dim eNetMachine As eNetMachineConfig
    Dim tblMachines As DataTable
    Dim connector As Connector

    Public Sub New(Optional _connectorId As Integer = 0)

        InitializeComponent()
        Dim point As Integer = 1

        Try

            connectorId = _connectorId
            connector = New Connector(connectorId)

            point = 2

            tblMachines = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1")

            Dim tblBoards As List(Of MonitoringBoard) = MonitoringBoardsService.GetBoards().Where(Function(m) String.IsNullOrEmpty(m.Target) Or m.Target = "0" Or m.Target = connector.MachineId).ToList()
            Dim hasMonitoringUnit As Boolean = tblBoards.Count > 0

            point = 3

            cmbFocasMonitoringUnit.Enabled = hasMonitoringUnit
            cmbMtcMonitoringUnit.Enabled = hasMonitoringUnit

            If hasMonitoringUnit Then
                cmbFocasMonitoringUnit.DataSource = tblBoards
                cmbFocasMonitoringUnit.DisplayMember = "Label"
                cmbFocasMonitoringUnit.ValueMember = "Id"
                cmbFocasMonitoringUnit.SelectedIndex = -1

                cmbMtcMonitoringUnit.DataSource = tblBoards
                cmbMtcMonitoringUnit.DisplayMember = "Label"
                cmbMtcMonitoringUnit.ValueMember = "Id"
                cmbMtcMonitoringUnit.SelectedIndex = -1
            End If

            point = 4

            LoadEnetMachinesToCombobox()

            point = 5

            If _connectorId = 0 Then

                btnMtcMore.Visible = False
                btnMtcApply.Enabled = False

                cbMtcMachineName.Enabled = False
                cbMtcEnetMachineName.Enabled = False
                cmbMtcMonitoringUnit.Enabled = False

                btnFocasMore.Visible = False
                btnFocasEdit.Visible = False

            Else

                point = 5

                Dim isMTConnect = (connector.ConnectorType = "MTConnect")
                PictureBox_MTConnect.Visible = isMTConnect
                PictureBox_Focas.Visible = Not isMTConnect

                TB_AgentPort.Text = connector.AgentPort

                If isMTConnect Then

                    point = 6

                    tabControlConnector.SelectedIndex = 0

                    txtMtcIpAddress.Enabled = False
                    cbMtcMachineName.Enabled = False
                    cbMtcEnetMachineName.Enabled = False
                    cmbMtcMonitoringUnit.Enabled = False

                    txtMtcIpAddress.Text = connector.MachineIP
                    cbMtcEnetMachineName.SelectedIndex = cbMtcEnetMachineName.FindStringExact(connector.eNETMachineName)
                    cbMtcMachineName.Items.Clear()
                    cbMtcMachineName.Items.Add(connector.MTCMachine)
                    cbMtcMachineName.SelectedIndex = 0

                    cmbMtcMonitoringUnit.SelectedValue = connector.MonitoringUnitId

                    btnMtcApply.Enabled = False
                    tabControlConnector.TabPages(1).Enabled = False

                    point = 7

                Else

                    point = 8

                    tabControlConnector.SelectedIndex = 1

                    txtFocasIpAddress.Enabled = False
                    txtFocasPort.Enabled = False
                    txtFocasManufacturer.Enabled = False
                    cbFocasControllerType.Enabled = False
                    cbFocasEnetMachineName.Enabled = False
                    cmbFocasMonitoringUnit.Enabled = False

                    btnFocasPing.Enabled = False
                    btnFocasOk.Visible = False
                    btnFocasMore.Visible = True

                    txtFocasIpAddress.Text = connector.MachineIP
                    txtFocasPort.Text = connector.FocasPort
                    txtFocasMachineName.Text = connector.MachineName
                    txtFocasManufacturer.Text = connector.Manufacturer
                    cbFocasEnetMachineName.SelectedIndex = cbFocasEnetMachineName.FindStringExact(connector.eNETMachineName)
                    cbFocasControllerType.SelectedIndex = cbFocasControllerType.FindStringExact(connector.ControllerType)

                    cmbFocasMonitoringUnit.SelectedValue = connector.MonitoringUnitId

                    SetServiceStatus("Adapter", Services.CheckStatus(connector.AdapterServiceName))
                    SetServiceStatus("Agent", Services.CheckStatus(connector.AgentServiceName))

                    grpServices.Visible = True

                    tabControlConnector.TabPages(0).Enabled = False

                    focasPingOk = True

                    point = 9

                End If

            End If

        Catch ex As Exception
            Log.Error($"==> Point {point}", ex)
        End Try

        AddHandler txtFocasIpAddress.TextChanged, AddressOf txtFocasIpAddress_TextChanged
        AddHandler txtFocasPort.TextChanged, AddressOf txtFocasField_TextChanged
        AddHandler txtFocasManufacturer.TextChanged, AddressOf txtFocasField_TextChanged
        AddHandler txtFocasMachineName.TextChanged, AddressOf txtFocasField_TextChanged
        AddHandler txtFocasIpAddress.TextChanged, AddressOf txtFocasField_TextChanged
        AddHandler cbFocasEnetMachineName.TextChanged, AddressOf txtFocasField_TextChanged
        AddHandler cbFocasControllerType.TextChanged, AddressOf txtFocasField_TextChanged

    End Sub


    Private Sub btnMtcFindMachine_Click(sender As Object, e As EventArgs) Handles btnMtcFindMachine.Click

        If (txtMtcIpAddress.Text.Length > 0) Then

            Try
                cbMtcMachineName.Items.Clear()

                Dim m_client As EntityClient = New EntityClient(txtMtcIpAddress.Text)
                m_client.RequestTimeout = 10000

                Dim devices As DeviceCollection = m_client.Probe()

                If (devices.Count > 0) Then

                    For Each machine In devices
                        cbMtcMachineName.Items.Add(machine.Name)
                    Next

                    btnMtcApply.Enabled = True
                    cbMtcMachineName.Enabled = True
                    cbMtcEnetMachineName.Enabled = True
                    cmbMtcMonitoringUnit.Enabled = True
                    cbMtcMachineName.SelectedIndex = 0

                Else

                    MessageBox.Show("No device found from this IP")

                End If

            Catch ex As Exception
                MessageBox.Show("Unable to retrieve any information from this IP")
                Log.Error($"Unable to retrieve any information from the IP { txtMtcIpAddress.Text }", ex)
            End Try

        Else
            MessageBox.Show("Please input an IP address")
        End If

    End Sub


    Private Sub btnMtcMore_Click(sender As Object, e As EventArgs) Handles btnMtcMore.Click

        connector.MachineIP = txtMtcIpAddress.Text
        connector.ConnectorType = "MTConnect"
        connector.MachineName = cbMtcMachineName.Text
        connector.eNETMachineName = cbMtcEnetMachineName.Text

        LoadCondEditForm()

    End Sub


    Private Sub btnMtcApply_Click(sender As Object, e As EventArgs) Handles btnMtcApply.Click

        Try
            If btnMtcApply.Text = "Apply" Then

                If String.IsNullOrEmpty(cbMtcMachineName.Text) Then
                    MessageBox.Show("You must select a machine.")
                    Return
                End If

                If String.IsNullOrEmpty(cbMtcEnetMachineName.Text) Then
                    MessageBox.Show("You must select a eNETDNC machine.")
                    Return
                End If

                Dim GoodName As Boolean = True

                For Each row As DataGridViewRow In SetupForm2.grvConnectors.Rows

                    If cbMtcMachineName.Text = row.Cells(0).Value.ToString() Then
                        MessageBox.Show("This machine is already in CSIFLEX")
                        Return

                    ElseIf cbMtcEnetMachineName.Text = row.Cells(3).Value.ToString() Then
                        MessageBox.Show("This eNETDNC Machine is already in CSIFLEX")
                        Return

                    End If
                Next

                If txtMtcIpAddress.Text.ToUpper().StartsWith("HTTP") Then
                    txtMtcIpAddress.Text = txtMtcIpAddress.Text.Substring(txtMtcIpAddress.Text.IndexOf("//") + 2)
                End If

                Dim agentAddress = txtMtcIpAddress.Text.Split(":")

                connector.MachineIP = agentAddress(0)
                connector.MTCMachine = cbMtcMachineName.Text
                connector.eNETMachineName = cbMtcEnetMachineName.Text
                connector.MachineName = txtMtcMachineName.Text
                connector.AgentIP = agentAddress(0)
                connector.AgentPort = IIf(agentAddress.Length > 1, agentAddress(1), "")
                connector.ConnectorType = "MTConnect"
                connector.MonitoringUnitId = cmbMtcMonitoringUnit.SelectedValue
                connector.SaveConnector()

                Dim reload = False
                If Not eNetMachine.Cmd_Others.ContainsKey("NO eMONITORING") Then
                    eNetServer.Instance.AddStatusMachine(eNetMachine.EnetPos, "NO eMONITORING", "NO eMONITORING")
                    reload = True
                End If
                If connector.MonitoringUnitId > 0 And Not eNetMachine.Cmd_Others.ContainsKey("LOCKED") Then
                    eNetServer.Instance.AddStatusMachine(eNetMachine.EnetPos, "LOCKED", "LOCKED")
                    reload = True
                End If

                If String.IsNullOrEmpty(eNetMachine.Cmd_UDPCON) Then
                    eNetServer.Instance.AddStatusMachine(eNetMachine.EnetPos, "CYCLE ON", "CON")
                    reload = True
                End If

                If reload Then
                    eNetServer.Instance.ReloadMachines()
                    SetupForm2.send_http_req("reloadMachines")
                End If

                LoadCondEditForm()

            ElseIf btnMtcApply.Text = "Update" Then

                If (txtMtcIpAddress.Text.Length > 0) Then
                    Dim GoodName As Boolean = True
                    For Each row As DataGridViewRow In SetupForm2.grvConnectors.Rows

                        If cbMtcEnetMachineName.Text = row.Cells(3).Value.ToString() Then
                            MessageBox.Show("This eNET Machine is already in CSIFlex")
                            GoodName = False
                        End If

                    Next

                    If (GoodName = True) Then

                        If cbMtcEnetMachineName.Text = String.Empty Then
                            MessageBox.Show("eNET Machine Name should not be empty !")
                        Else
                            connector.SaveConnector()
                            If txtMtcConnectorType.Text = "MTConnect" Then

                                btnMtcApply.Visible = False
                                cbMtcEnetMachineName.Visible = False
                                btnMtcMore.Visible = True
                                btnMtcFindMachine.Visible = False
                                Me.btnMtcMore.PerformClick()

                            ElseIf txtMtcConnectorType.Text = "Focas" Then

                                btnFocasOk.Visible = False
                                cbFocasEnetMachineName.Visible = False
                                btnFocasEdit.Visible = True
                                btnFocasPing.Visible = False
                                MessageBox.Show("Focas Machine data updated successfully !")

                            End If

                        End If
                    End If
                End If
            End If

            SetupForm2.Load_DGV_CSIConnector()

        Catch ex As Exception
            MessageBox.Show("Error in Adding/Updating MTconnect Machine : " & ex.Message() & " StackTrace : " & ex.StackTrace())

            Log.Error($"Error in Adding/Updating MTconnect Machine", ex)
        End Try

    End Sub


    Private Sub btnFocasPing_Click(sender As Object, e As EventArgs) Handles btnFocasPing.Click

        If txtFocasIpAddress.Text = String.Empty Or txtFocasPort.Text = String.Empty Then

            MessageBox.Show("Machine IP and Focas Port Should not be empty !")
            txtFocasIpAddress.Focus()

        Else

            Dim IP As String
            Dim FPort As String
            Dim pingSucess As Boolean

            IP = txtFocasIpAddress.Text
            FPort = txtFocasPort.Text
            pingSucess = focasinst.Ping(IP, FPort)

            focasPingOk = pingSucess

            If pingSucess Then

                cbFocasEnetMachineName.Enabled = True
                cbFocasControllerType.Enabled = True
                txtFocasManufacturer.Enabled = True
                cmbFocasMonitoringUnit.Enabled = True

                btnFocasOk.Enabled = True

                btnFocasPing.FlatAppearance.BorderColor = Color.Green
                btnFocasPing.FlatAppearance.BorderSize = 2

            Else

                btnFocasPing.FlatAppearance.BorderColor = Color.Red
                btnFocasPing.FlatAppearance.BorderSize = 2

                txtFocasIpAddress.Focus()
            End If
        End If
    End Sub


    Private Sub btnFocasEdit_Click(sender As Object, e As EventArgs) Handles btnFocasEdit.Click

        btnFocasOk.Visible = True
        btnFocasOk.Text = "Update"
        btnFocasMore.Visible = False
        btnFocasPing.Enabled = True
        btnFocasEdit.Visible = False

        txtFocasIpAddress.Enabled = True
        txtFocasPort.Enabled = True

        cbFocasEnetMachineName.Enabled = True
        txtFocasManufacturer.Enabled = True
        cmbFocasMonitoringUnit.Enabled = True

        TB_AgentIPAdd.Enabled = False
        TB_AgentPort.Enabled = False
        BTN_MoreSettings.Enabled = True

    End Sub


    Private Sub btnFocasMore_Click(sender As Object, e As EventArgs) Handles btnFocasMore.Click

        LoadCondEditForm()

    End Sub


    Private Sub btnFocasOk_Click(sender As Object, e As EventArgs) Handles btnFocasOk.Click

        'If Not String.IsNullOrEmpty(txtFocasMachineName.Text.Length > 0) And txtFocasMachineName.Text.Contains(" ") Then
        '    MessageBox.Show("Space is not allowed in Machine Name.")
        '    Return
        'End If

        If txtFocasIpAddress.Text.Length = 0 Or txtFocasMachineName.Text.Length = 0 Or txtFocasPort.Text.Length = 0 Or cbFocasEnetMachineName.Text.Length = 0 Or cbFocasControllerType.Text.Length = 0 Then
            MessageBox.Show("Please, fill all required feilds !")
            Return
        End If

        If btnFocasOk.Text = "Apply" Then

            For Each row As DataGridViewRow In SetupForm2.grvConnectors.Rows

                If cbFocasEnetMachineName.Text = row.Cells(3).Value.ToString() Then
                    MessageBox.Show("This eNET Machine is already in CSIFlex")
                    Return
                End If
            Next

            Dim deviceName = txtFocasMachineName.Text.Replace(" ", "-")

            connector.MachineIP = txtFocasIpAddress.Text
            connector.MTCMachine = deviceName
            connector.FocasPort = txtFocasPort.Text
            connector.Manufacturer = txtFocasManufacturer.Text
            connector.AgentIP = Util.GetLocalIPAddress()
            connector.AgentPort = GetNewAgentPort()
            connector.AgentExeLocation = Path.Combine(String.Format(Paths.AGENT_FOLDER_FORMAT, objdeviceInfo.DeviceName), Files.AGENT_EXE)
            connector.AdapterPort = GetNewAdapterPort()
            connector.MonitoringUnitId = cmbFocasMonitoringUnit.SelectedValue
            connector.ConnectorType = "Focas"

            objdeviceInfo.AdapterFocasIp = txtFocasIpAddress.Text
            objdeviceInfo.FocasMachinePort = txtFocasPort.Text
            objdeviceInfo.Manufacturer = txtFocasManufacturer.Text
            objdeviceInfo.DeviceName = deviceName
            objdeviceInfo.MachineId = connector.MachineId
            objdeviceInfo.Adapter = cbFocasControllerType.Text
            objdeviceInfo.AgentPort = connector.AgentPort
            objdeviceInfo.AdapterPort = connector.AdapterPort
            objdeviceInfo.AdapterPath = Path.Combine(Paths.ApplicationPath, "Adapters", connector.AdapterServiceName)
            objdeviceInfo.AgentFolderPath = String.Format(Paths.AGENT_FOLDER_FORMAT, connector.AgentServiceName)
            objdeviceInfo.AdapterServiceName = connector.AdapterServiceName
            objdeviceInfo.AgentServiceName = connector.AgentServiceName

            Me.Cursor = Cursors.WaitCursor

            objMainWindow.InstallAdapterForNewAgent(objdeviceInfo)
            objMainWindow.CreateFilesAndInstallAgentService(objdeviceInfo)

            Dim reload = False

            Try
                connector.SaveConnector()

                Try
                    If Not eNetMachine.Cmd_Others.ContainsKey("NO eMONITORING") Then
                        eNetServer.Instance.AddStatusMachine(eNetMachine.EnetPos, "NO eMONITORING", "NO eMONITORING")
                        reload = True
                    End If
                Catch ex As Exception
                    Log.Error($"Error trying to create event NO eMONITORING on eNETDNC for the machine {eNetMachine.MachineName}", ex)
                End Try

                Try
                    If connector.MonitoringUnitId > 0 And Not eNetMachine.Cmd_Others.ContainsKey("LOCKED") Then
                        eNetServer.Instance.AddStatusMachine(eNetMachine.EnetPos, "LOCKED", "LOCKED")
                        reload = True
                    End If
                Catch ex As Exception
                    Log.Error($"Error trying to create event LOCKED on eNETDNC for the machine {eNetMachine.MachineName}", ex)
                End Try

                Try
                    If connector.MonitoringUnitId > 0 And Not eNetMachine.Cmd_Others.ContainsKey("CRITICAL PRESSURE") Then
                        eNetServer.Instance.AddStatusMachine(eNetMachine.EnetPos, "CRITICAL PRESSURE", "CRITICAL PRESSURE")
                        reload = True
                    End If
                Catch ex As Exception
                    Log.Error($"Error trying to create event CRITICAL PRESSURE on eNETDNC for the machine {eNetMachine.MachineName}", ex)
                End Try

                Try
                    If String.IsNullOrEmpty(eNetMachine.Cmd_UDPCON) Then
                        eNetServer.Instance.AddStatusMachine(eNetMachine.EnetPos, "CYCLE ON", "CON")
                        reload = True
                    End If
                Catch ex As Exception
                    Log.Error($"Error trying to create event CYCLE ON on eNETDNC for the machine {eNetMachine.MachineName}", ex)
                End Try

            Catch ex As Exception
                Log.Error(ex)
                MessageBox.Show("Error trying to save the Focas Adapter in the Database. See the log file for more information.")
                Me.Cursor = Cursors.Default
                Return
            End Try

            If reload Then
                eNetServer.Instance.ReloadMachines()
                SetupForm2.send_http_req("reloadMachines")
            End If

            Try
                AdapterManagement.Start(connector.AdapterServiceName)

                AgentManagement.Start(connector.AgentServiceName)

            Catch ex As Exception
                Log.Error(ex)
                MessageBox.Show("Error trying to start the Focas Adapter/Agent. See the log file for more information.")

                LoadCondEditForm()

                Me.Cursor = Cursors.Default
                Return
            End Try


            Me.Cursor = Cursors.Default

            SetupForm2.Load_DGV_CSIConnector()

            LoadCondEditForm()

        ElseIf btnFocasOk.Text = "Update" Then

            Dim IsPingable As Boolean

            If txtFocasIpAddress.Text = connector.MachineIP And cbMtcEnetMachineName.Text = connector.eNETMachineName And Not cmbFocasMonitoringUnit.SelectedValue = connector.MonitoringUnitId Then

                Dim newMonUnitId As Integer = cmbFocasMonitoringUnit.SelectedValue

                MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.tbl_csiconnector SET MonitoringUnitId = {newMonUnitId} WHERE Id = {connector.Id}")

                If connector.MonitoringUnitId > 0 Then
                    MonitoringBoardsService.SetTarget(connector.MonitoringUnitId, 0)
                End If
                If newMonUnitId > 0 Then
                    MonitoringBoardsService.SetTarget(newMonUnitId, connector.MachineId)
                    connector.CreateCSDCondition()
                End If

                connector.MonitoringUnitId = cmbFocasMonitoringUnit.SelectedValue
                Me.Close()
                Return
            End If

            IsPingable = focasinst.Ping(txtFocasIpAddress.Text, Convert.ToInt32(txtFocasPort.Text))

            If Not IsPingable Then
                MessageBox.Show("Ping unsuccessfull or Focas Port is already in use !")
                Return
            End If

            For Each row As DataGridViewRow In SetupForm2.grvConnectors.Rows

                If txtFocasMachineName.Text = row.Cells(0).Value.ToString() Then

                    If cbFocasControllerType.Text = row.Cells(5).Value.ToString() And txtFocasManufacturer.Text = row.Cells(8).Value.ToString() And txtFocasIpAddress.Text = row.Cells(1).Value.ToString() And txtFocasPort.Text = row.Cells(4).Value.ToString() And cbFocasEnetMachineName.Text = row.Cells(3).Value.ToString() Then

                        MessageBox.Show("This Machine is already in CSIFlex !" + Environment.NewLine + Environment.NewLine +
                                        "Machine Configurations are : " + Environment.NewLine +
                                        "Machine Name : " + txtFocasMachineName.Text + Environment.NewLine +
                                        "Focas Machine IP : " + txtFocasIpAddress.Text + Environment.NewLine +
                                        "Focas Port : " + txtFocasPort.Text + Environment.NewLine +
                                        "Controller Type : " + cbFocasControllerType.Text + Environment.NewLine +
                                        "Manufacturer : " + txtFocasManufacturer.Text + Environment.NewLine +
                                        "Agent IPAddress : " + TB_AgentIPAdd.Text + Environment.NewLine +
                                        "Agent Port : " + TB_AgentPort.Text)

                        Return
                        Me.Close()
                    Else

                        Dim agentPort = row.Cells(7).Value.ToString()
                        Dim adapterPort = GetAdapterPort(txtFocasMachineName.Text)

                        objAdapterInfo.Port = adapterPort

                        'Update adapter.ini file
                        objMainWindow.UpdateAdapterIniFile(txtFocasMachineName.Text, txtFocasIpAddress.Text, Convert.ToInt32(txtFocasPort.Text), adapterPort, connector.AdapterServiceName)

                        Dim configFilePath = Path.Combine(String.Format(Paths.AGENT_FOLDER_FORMAT, txtFocasMachineName.Text), Files.AGENT_CONFIG)

                        objMainWindow.UpdateAdapterInAgentConfig(configFilePath, txtFocasMachineName.Text, adapterPort)

                        DeleteOldFocasMachine()

                        connector.Id = 0

                        ReplaceNewFocasMachine(adapterPort, agentPort)

                        Me.Close()

                    End If
                End If
            Next

            Try
                connector.SaveConnector()
                Me.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

    End Sub


    Private Sub btnStopAdapter_Click(sender As Object, e As EventArgs) Handles btnStopAdapter.Click

        AdapterManagement.Stop(connector.AdapterServiceName)

        SetServiceStatus("Adapter", Services.CheckStatus(connector.AdapterServiceName))

    End Sub


    Private Sub btnStartAdapter_Click(sender As Object, e As EventArgs) Handles btnStartAdapter.Click

        AdapterManagement.Start(connector.AdapterServiceName)

        SetServiceStatus("Adapter", Services.CheckStatus(connector.AdapterServiceName))

    End Sub


    Private Sub btnStopAgent_Click(sender As Object, e As EventArgs) Handles btnStopAgent.Click

        AgentManagement.Stop(connector.AgentServiceName)

        SetServiceStatus("Agent", Services.CheckStatus(connector.AgentServiceName))

    End Sub


    Private Sub btnStartAgent_Click(sender As Object, e As EventArgs) Handles btnStartAgent.Click

        AgentManagement.Start(connector.AgentServiceName)

        SetServiceStatus("Agent", Services.CheckStatus(connector.AgentServiceName))

    End Sub


    Private Sub btnRefreshAdapter_Click(sender As Object, e As EventArgs) Handles btnRefreshAdapter.Click

        Try
            Me.Cursor = Cursors.WaitCursor

            connector.AdapterServiceName = ""

            Dim serviceName = connector.AdapterServiceName
            Dim pathAdapter = Path.Combine(Paths.ADAPTERS, $"CSIFLEX.FocasAdapter.Machine-{connector.MachineId.ToString("000")}")
            Dim pathAgent = String.Format(Paths.AGENT_FOLDER_FORMAT, $"CSIFLEX.FocasAgent.Machine-{connector.MachineId.ToString("000")}")

            connector.AdapterExeLocation = pathAdapter
            connector.AgentExeLocation = pathAgent

            objAdapterInfo.ServiceName = serviceName
            objAdapterInfo.Path = pathAdapter
            objAdapterInfo.DeviceName = connector.MachineName

            objdeviceInfo.AdapterFocasIp = connector.MachineIP
            objdeviceInfo.Manufacturer = connector.Manufacturer
            objdeviceInfo.FocasMachinePort = connector.FocasPort
            objdeviceInfo.DeviceName = connector.MachineName
            objdeviceInfo.MachineId = connector.MachineId

            objdeviceInfo.Adapter = connector.ControllerType
            objdeviceInfo.AdapterPath = pathAdapter
            objdeviceInfo.AdapterPort = connector.AdapterPort
            objdeviceInfo.AdapterServiceName = serviceName

            objdeviceInfo.AgentPort = connector.AgentPort
            objdeviceInfo.AgentFolderPath = connector.AgentExeLocation

            objMainWindow.UninstallAdapter(objAdapterInfo)
            SetServiceStatus("Adapter", Services.CheckStatus(serviceName))

            objMainWindow.InstallAdapterForNewAgent(objdeviceInfo)
            SetServiceStatus("Adapter", Services.CheckStatus(serviceName))

            AdapterManagement.Start(serviceName)
            SetServiceStatus("Adapter", Services.CheckStatus(serviceName))

            connector.SaveConnector()

        Catch ex As Exception
            MessageBox.Show("Error while trying to regenerate the Adapter service.")
            Log.Error(ex)
        End Try

        Me.Cursor = Cursors.Default

    End Sub


    Private Sub btnRefreshAgent_Click(sender As Object, e As EventArgs) Handles btnRefreshAgent.Click

        Try
            Me.Cursor = Cursors.WaitCursor

            connector.AgentServiceName = ""
            connector.AdapterServiceName = ""

            Dim agentServiceName = connector.AgentServiceName
            Dim pathAgent = String.Format(Paths.AGENT_FOLDER_FORMAT, agentServiceName)

            connector.AgentExeLocation = pathAgent

            objMainWindow.UninstallAgent(connector.MachineName, connector.AgentServiceName)
            SetServiceStatus("Agent", Services.CheckStatus(agentServiceName))

            objdeviceInfo.AgentServiceName = agentServiceName
            objdeviceInfo.AdapterServiceName = connector.AdapterServiceName
            objdeviceInfo.AdapterFocasIp = connector.AgentIP
            objdeviceInfo.Manufacturer = connector.Manufacturer
            objdeviceInfo.FocasMachinePort = connector.FocasPort
            objdeviceInfo.DeviceName = connector.MachineName.Replace(" ", "-")
            objdeviceInfo.Adapter = connector.ControllerType
            objdeviceInfo.AgentPort = connector.AgentPort
            objdeviceInfo.AdapterPort = connector.AdapterPort
            objdeviceInfo.AgentFolderPath = pathAgent

            objMainWindow.CreateFilesAndInstallAgentService(objdeviceInfo)
            SetServiceStatus("Agent", Services.CheckStatus(agentServiceName))

            AgentManagement.Start(agentServiceName)
            SetServiceStatus("Agent", Services.CheckStatus(agentServiceName))

        Catch ex As Exception
            MessageBox.Show("Error while trying to regenerate the Agent service.")
            Log.Error(ex)
        End Try

        Me.Cursor = Cursors.Default

    End Sub


    Private Sub ReplaceNewFocasMachine(adapterPort As Integer, agentPort As String)

        objdeviceInfo.AdapterFocasIp = txtFocasIpAddress.Text
        objdeviceInfo.Manufacturer = txtFocasManufacturer.Text
        objdeviceInfo.DeviceName = txtFocasMachineName.Text
        objdeviceInfo.Adapter = cbFocasControllerType.Text

        If (String.IsNullOrEmpty(adapterPort)) Then
            adapterPort = "7878"
        End If

        objdeviceInfo.AdapterPort = Convert.ToInt32(adapterPort)

        connector.SaveConnector()

        LoadCondEditForm()

        Return

        If txtFocasIpAddress.Text.StartsWith("http") Then
        Else
            IPString = "http://" & CSI_Library.CSI_Library.LocalHostIP & ":" & agentPort & "/"  '& TB_MachineNameFocas.Text & "/"
        End If

        Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(IPString), HttpWebRequest)

        myHttpWebRequest.Timeout = 10000

        Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)

        If myHttpWebResponse.StatusCode = HttpStatusCode.OK Then

            SetupForm2.Load_DGV_CSIConnector()
            Adv_MTC_cond_edit.MachineName = Convert.ToString(objdeviceInfo.DeviceName) 'TB_MachineNameFocas.Text
            Adv_MTC_cond_edit.MachineIP = Convert.ToString(IPString) 'TB_AddressIP.Text
            Adv_MTC_cond_edit.AdapterIPAddress = Convert.ToString(txtFocasIpAddress.Text)
            Adv_MTC_cond_edit.eNetName = Convert.ToString(cbFocasEnetMachineName.Text)
            Adv_MTC_cond_edit.ConnectorType = "Focas"
            Me.DialogResult = Windows.Forms.DialogResult.OK

            Adv_MTC_cond_edit.ShowDialog()
            Adv_MTC_cond_edit.Close()
        Else
            MessageBox.Show("No response from this address")
        End If

    End Sub


    Private Sub txtFocasIpAddress_TextChanged(sender As Object, e As EventArgs)

        Dim regex = New Regex("^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")
        Dim Match = regex.Match(txtFocasIpAddress.Text)
        Dim success = Match.Success

        If success Then
            btnFocasPing.Enabled = True
            btnFocasPing.FlatAppearance.BorderColor = Color.Blue  'Color.LawnGreen
            btnFocasPing.FlatAppearance.BorderSize = 2
        Else
            btnFocasPing.Enabled = False
            btnFocasPing.FlatAppearance.BorderColor = Color.Silver
            btnFocasPing.FlatAppearance.BorderSize = 1
        End If

        focasPingOk = False

    End Sub



    Private Sub tabControlConnector_Selecting(ByVal sender As Object, ByVal e As TabControlCancelEventArgs) Handles tabControlConnector.Selecting

        If Not connector.Id = 0 Then

            If e.Action = TabControlAction.Selecting AndAlso e.TabPageIndex = 1 Then
                e.Cancel = True
                MessageBox.Show("It is MTConnect Machine, You can't select this Tab ! ")
            End If

            If e.Action = TabControlAction.Selecting AndAlso e.TabPageIndex = 0 Then
                e.Cancel = True
                MessageBox.Show("It is Focas Machine, You can't select this Tab ! ")
            End If

        End If

    End Sub


    Private Sub tabControlConnector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabControlConnector.SelectedIndexChanged

        If tabControlConnector.SelectedTab.Text = "Focas" Then

            PictureBox_Focas.Visible = True
            PictureBox_MTConnect.Visible = False
            connector.ConnectorType = "Focas"

        ElseIf tabControlConnector.SelectedTab.Text = "MTConnect" Then

            PictureBox_Focas.Visible = False
            PictureBox_MTConnect.Visible = True
            connector.ConnectorType = "MTConnect"

        End If

    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnMtcClose.Click, btnFocasClose.Click
        Me.Close()
    End Sub



#Region "Common Functions including Form_Load Functions"


    Private Sub LoadCondEditForm()

        Dim agentAddress = $"http://{ connector.AgentIP }:{ connector.AgentPort }"

        Try
            Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(agentAddress), HttpWebRequest)

            myHttpWebRequest.Timeout = 20000

            Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)

            If myHttpWebResponse.StatusCode = HttpStatusCode.OK Then

                Adv_MTC_cond_edit.ConnectorId = connector.Id
                Adv_MTC_cond_edit.MachineName = connector.MachineName
                Adv_MTC_cond_edit.MTCMachineName = connector.MTCMachine
                Adv_MTC_cond_edit.MachineIP = connector.MachineIP
                Adv_MTC_cond_edit.AdapterIPAddress = agentAddress
                Adv_MTC_cond_edit.eNetName = connector.eNETMachineName
                Adv_MTC_cond_edit.ConnectorType = connector.ConnectorType
                Me.DialogResult = DialogResult.OK

                Adv_MTC_cond_edit.ShowDialog()
                Adv_MTC_cond_edit.Close()
            Else
                MessageBox.Show("No response from this address")
            End If

        Catch ex As Exception

            MessageBox.Show("Machine Setting is not loading !")

            Log.Error($"Machine Setting is not loading ! Timeout Expired for machine : { connector.MachineName } - { agentAddress }", ex)

        End Try

    End Sub


    Private Sub LoadEnetMachinesToCombobox() 'This function loads CB_eNETMachineName with all eNETMachines who have FTP Connections means  Con_type = 5

        Try
            RemoveHandler cbMtcEnetMachineName.SelectedIndexChanged, AddressOf cbMtcEnetMachineName_SelectedIndexChanged
            RemoveHandler cbFocasEnetMachineName.SelectedIndexChanged, AddressOf cbFocasEnetMachineName_SelectedIndexChanged

            cbMtcEnetMachineName.DataSource = tblMachines
            cbMtcEnetMachineName.DisplayMember = "EnetMachineName"
            cbMtcEnetMachineName.ValueMember = "Id"
            cbMtcEnetMachineName.SelectedIndex = -1
            cbMtcEnetMachineName.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbMtcEnetMachineName.AutoCompleteMode = AutoCompleteMode.SuggestAppend

            cbFocasEnetMachineName.DataSource = tblMachines
            cbFocasEnetMachineName.DisplayMember = "EnetMachineName"
            cbFocasEnetMachineName.ValueMember = "Id"
            cbFocasEnetMachineName.SelectedIndex = -1
            cbFocasEnetMachineName.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbFocasEnetMachineName.AutoCompleteMode = AutoCompleteMode.SuggestAppend

            AddHandler cbMtcEnetMachineName.SelectedIndexChanged, AddressOf cbMtcEnetMachineName_SelectedIndexChanged
            AddHandler cbFocasEnetMachineName.SelectedIndexChanged, AddressOf cbFocasEnetMachineName_SelectedIndexChanged

        Catch ex As Exception
            Log.Error(ex)
        End Try

    End Sub


    Private Sub SetServiceStatus(serviceName As String, newStatus As ServiceTools.ServiceState)

        Dim changeLabel As Label
        Dim btnStart As PictureBox
        Dim btnStop As PictureBox

        If serviceName = "Adapter" Then
            changeLabel = lblAdapterServState
            btnStart = btnStartAdapter
            btnStop = btnStopAdapter
        Else
            changeLabel = lblAgentServState
            btnStart = btnStartAgent
            btnStop = btnStopAgent
        End If

        btnStart.Visible = False
        btnStop.Visible = False

        Select Case newStatus

            Case ServiceTools.ServiceState.NotFound
                changeLabel.Text = "Not Installed"
                changeLabel.BackColor = Color.Red

            Case ServiceTools.ServiceState.Run
                changeLabel.Text = "Running"
                changeLabel.BackColor = Color.LimeGreen
                btnStop.Visible = True

            Case ServiceTools.ServiceState.Starting
                changeLabel.Text = "Starting"
                changeLabel.BackColor = Color.Yellow

            Case ServiceTools.ServiceState.Stop
                changeLabel.Text = "Stopped"
                changeLabel.BackColor = Color.Red
                btnStart.Visible = True

            Case ServiceTools.ServiceState.Stopping
                changeLabel.Text = "Stopping"
                changeLabel.BackColor = Color.Orange

            Case ServiceTools.ServiceState.Unknown
                changeLabel.Text = "Unknown"
                changeLabel.BackColor = Color.Red

            Case Else
                changeLabel.Text = "Unknown"
                changeLabel.BackColor = Color.Red

        End Select
    End Sub


    Private Sub DeleteOldFocasMachine()

        Dim selectedrowindex As Integer = SetupForm2.grvConnectors.SelectedCells(0).RowIndex
        Dim selectedRow As DataGridViewRow = SetupForm2.grvConnectors.Rows(selectedrowindex)

        Dim machinename As String = Convert.ToString(selectedRow.Cells("MachineName").Value)
        Dim enetMachinename As String = Convert.ToString(selectedRow.Cells("eNETMachineName").Value)
        Dim machineip As String = Convert.ToString(selectedRow.Cells("MachineIP").Value)
        Dim MachineType As String = Convert.ToString(selectedRow.Cells("ConnectorType").Value)

        Me.Cursor = Cursors.WaitCursor

        objAdapterInfo.ServiceName = "MTCFocasAdapter-" + machinename.Replace(" ", "-")
        objAdapterInfo.Path = System.IO.Path.Combine(Paths.ADAPTERS, machinename)
        objAdapterInfo.DeviceName = machinename

        Try
            Dim cmd As Text.StringBuilder = New Text.StringBuilder()

            cmd.Append($"DELETE FROM CSI_auth.tbl_CSIConnector           WHERE Id          = { connectorId } ; ")
            cmd.Append($"DELETE FROM CSI_auth.tbl_mtcfocasconditions     WHERE ConnectorId = { connectorId } ; ")
            cmd.Append($"DELETE FROM CSI_auth.tbl_csiothersettings       WHERE ConnectorId = { connectorId } ; ")
            cmd.Append($"DELETE FROM CSI_auth.tbl_csiothersettingsvalues WHERE ConnectorId = { connectorId } ; ")
            cmd.Append($"UPDATE IGNORE csi_auth.tbl_ehub_conf SET MTC_Machine_name = '' WHERE MTC_Machine_name = '{ machinename }';")

            MySqlAccess.ExecuteNonQuery(cmd.ToString())

            SetupForm2.Load_DGV_CSIConnector()

        Catch ex As Exception
            Log.Error(ex)
            MessageBox.Show("Error in Database : " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub


#End Region


    Public Shared Function PortInUse(ByVal port As Integer) As Boolean
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


    Public Sub _CleanFocasForm()
        txtFocasIpAddress.Text = String.Empty
        'TB_FocasPort.Text = "8193" 
        txtFocasMachineName.Text = String.Empty
        txtFocasMachineName.Enabled = False
        cbFocasControllerType.Text = String.Empty
        cbFocasControllerType.Enabled = False
        cbFocasEnetMachineName.Text = String.Empty
        cbFocasEnetMachineName.Enabled = False
        cmbFocasMonitoringUnit.SelectedIndex = -1
        cmbFocasMonitoringUnit.Enabled = False
        txtFocasManufacturer.Text = String.Empty
        txtFocasManufacturer.Enabled = False
        txtFocasPort.Text = "8193"
        TB_AgentIPAdd.Text = CSI_Library.CSI_Library.LocalHostIP
        TB_AgentPort.Text = "5000"
        txtFocasPort.Enabled = True
        btnFocasOk.Enabled = False
        BTN_MoreSettings.Enabled = False
        txtFocasIpAddress.Focus()
    End Sub


#Region "Focas Machine Functions"

    Private Sub BTN_CancelFocas_Click(sender As Object, e As EventArgs) Handles btnFocasClose.Click
        Me.Close()
    End Sub


    Private Sub TB_FocasPort_EditClick(sender As Object, e As EventArgs) Handles txtFocasPort.MouseClick, txtFocasPort.GotFocus, txtFocasPort.MouseDown
        txtFocasPort.ReadOnly = False
        txtFocasPort.Text = String.Empty
    End Sub


    Private Sub TB_FocasPort_EditCompleteClick(sender As Object, e As EventArgs) Handles txtFocasPort.LostFocus
        If txtFocasPort.Text = String.Empty Then
            txtFocasPort.Text = "8193"
            txtFocasPort.ReadOnly = True
        Else
            txtFocasPort.ReadOnly = True
        End If
    End Sub


    Private Sub LBL_MoreSettings_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Label13.Visible = True
        txtFocasManufacturer.Visible = True
        txtFocasManufacturer.Enabled = True
    End Sub


    Private Sub LBL_ShowLess_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Label13.Visible = False
        txtFocasManufacturer.Visible = False
        txtFocasManufacturer.Visible = False
    End Sub

#End Region


    Private Sub BTN_MoreSettings_Click(sender As Object, e As EventArgs) Handles BTN_MoreSettings.Click
        If GroupBox1.Visible = False Then
            BTN_MoreSettings.Enabled = False
            TB_AgentIPAdd.Enabled = True
            TB_AgentPort.Enabled = True
            BTN_Save.Enabled = True
            GroupBox1.Visible = True
            Me.Height = storedHeight
            TabHeight = tabControlConnector.Height
            tabControlConnector.Height = 805
            Me.StartPosition = FormStartPosition.CenterScreen
        End If
    End Sub


    Private Sub BTN_Save_Click(sender As Object, e As EventArgs) Handles BTN_Save.Click
        If GroupBox1.Visible = True Then
            BTN_MoreSettings.Enabled = True
            TB_AgentIPAdd.Enabled = False
            TB_AgentPort.Enabled = False
            BTN_Save.Enabled = False
            GroupBox1.Visible = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Height = minHeight
            tabControlConnector.Height = TabHeight
        End If
    End Sub


    Public Function GetNewAgentPort() As Integer
        Dim port_number As String = 5000
        Try
            Dim portT As New DataTable
            Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select MAX(AgentPort) from csi_auth.tbl_csiconnector where ConnectorType='Focas'", CSI_Library.CSI_Library.MySqlConnectionString)
            dadapter_name.Fill(portT)
            If portT.Rows.Count <> 0 And Not IsDBNull(portT.Rows(0)(0)) Then
                port_number = Convert.ToInt32(portT.Rows(0)(0)) + 1

            Else
                port_number = 5000
            End If
        Catch ex As Exception

        End Try
        Return port_number

    End Function


    Public Function GetNewAdapterPort() As Integer
        Dim port_number As Integer = 7878
        Try
            Dim portT As New DataTable
            Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select MAX(AdapterPort) from csi_auth.tbl_csiconnector where ConnectorType='Focas'", CSI_Library.CSI_Library.MySqlConnectionString)
            dadapter_name.Fill(portT)
            If portT.Rows.Count <> 0 And Not IsDBNull(portT.Rows(0)(0)) Then
                port_number = Convert.ToInt32(portT.Rows(0)(0)) + 1

            Else
                port_number = 7878
            End If
        Catch ex As Exception

        End Try
        Return port_number

    End Function


    Public Function GetAdapterPort(machineName As String) As Integer

        Dim port_number As Integer = 0

        Try
            Dim portT As New DataTable
            Dim dadapter_name As MySqlDataAdapter = New MySqlDataAdapter("Select AdapterPort from csi_auth.tbl_csiconnector where MachineName = '" & machineName & "' and ConnectorType ='Focas'", CSI_Library.CSI_Library.MySqlConnectionString)
            dadapter_name.Fill(portT)
            If portT.Rows.Count <> 0 And Not IsDBNull(portT.Rows(0)(0)) Then
                port_number = Convert.ToInt32(portT.Rows(0)(0))

            Else
                port_number = 7878
            End If
        Catch ex As Exception

        End Try

        Return port_number

    End Function


    Private Sub cbFocasEnetMachineName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFocasEnetMachineName.SelectedIndexChanged

        Try
            If cbFocasEnetMachineName.SelectedIndex < 0 Then
                connector.MachineId = 0
                txtFocasMachineName.Clear()
                Return
            End If

            txtFocasMachineName.Text = cbFocasEnetMachineName.SelectedItem("Machine_Name")

            Log.Info($"Machine [ {txtFocasMachineName.Text} ] not found")

            eNetMachine = eNetServer.Machines.FirstOrDefault(Function(m) m.MachineName = cbFocasEnetMachineName.Text)

            If eNetMachine IsNot Nothing Then
                connector.MachineId = cbFocasEnetMachineName.SelectedItem("Id")
                connector.eNETMachineName = eNetMachine.MachineName
            Else
                Log.Error($"Machine [ {cbFocasEnetMachineName.Text} ] not found")
            End If
        Catch ex As Exception
            Log.Error(ex)
        End Try

    End Sub


    Private Sub cbMtcEnetMachineName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMtcEnetMachineName.SelectedIndexChanged

        If cbMtcEnetMachineName.SelectedIndex < 0 Then
            connector.MachineId = 0
            Return
        End If

        txtMtcMachineName.Text = cbMtcEnetMachineName.SelectedItem("Machine_Name")
        eNetMachine = eNetServer.Machines.FirstOrDefault(Function(m) m.MachineName = cbMtcEnetMachineName.Text)

        connector.MachineId = cbMtcEnetMachineName.SelectedItem("Id")
        connector.eNETMachineName = eNetMachine.MachineName

    End Sub


    Private Sub txtMTCField_TextChanged(sender As Object, e As EventArgs) Handles txtMtcIpAddress.TextChanged, cbMtcMachineName.SelectedIndexChanged

        connector.MachineName = cbMtcEnetMachineName.Text
        connector.MachineIP = txtMtcIpAddress.Text

    End Sub


    Private Sub txtFocasField_TextChanged(sender As Object, e As EventArgs)

        connector.MachineName = txtFocasMachineName.Text
        connector.eNETMachineName = cbFocasEnetMachineName.Text
        connector.MachineIP = txtFocasIpAddress.Text
        connector.FocasPort = txtFocasPort.Text
        connector.ControllerType = cbFocasControllerType.Text
        connector.Manufacturer = txtFocasManufacturer.Text
    End Sub

End Class
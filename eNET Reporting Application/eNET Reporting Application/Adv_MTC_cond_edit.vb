Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml
Imports AdvancedDataGridView
Imports CSIFLEX.Database.Access
Imports CSIFLEX.eNetLibrary
Imports CSIFLEX.Server.Library
Imports CSIFLEX.Utilities
Imports OpenNETCF.MTConnect

Public Class Adv_MTC_cond_edit

    Public row As String()
    Dim previous_tab As String = ""
    Public CSILib As CSI_Library.CSI_Library
    Public mysqlUpdate As String

    Private Sub tb_tabs_click(sender As Object, e As EventArgs) Handles TB_tabs.SelectedIndexChanged

        If (TB_tabs.SelectedTab.Name = "page_notif") Then

            ' check the available conditions from the agent
            If Not BW_find_cond.IsBusy Then BW_find_cond.RunWorkerAsync()

            ' Read the status dgv from setup file
            read_setup_notif()

            Dim stat_column As New List(Of String)
            If dgviewNotificationStatus IsNot Nothing Then
                For Each row As DataGridViewRow In dgviewNotificationStatus.Rows
                    If row.Cells(1).Value IsNot Nothing Then stat_column.Add(row.Cells(1).Value)
                Next
            End If
            For Each status As String In list_of_Status
                If Not stat_column.Contains(status) Then
                    If status <> "" Then dgviewNotificationStatus.Rows.Add(False, status, 0)
                End If
            Next
            If Not stat_column.Contains("CYCLE OFF") Then
                dgviewNotificationStatus.Rows.Add(False, "CYCLE OFF", 0)
            End If

        ElseIf previous_tab = "page_notif" Then
            save_setup_notif()
        End If


        If (TB_tabs.SelectedTab.Name = "page_notif_dest") Then

            If File.Exists(home_path & "\sys\Conditions\" & eNetName & "\Sendto.csys") Then

                DGV_sendto.Rows.Clear()
                Using r As StreamReader = New StreamReader(home_path + "\sys\Conditions\" & eNetName & "\Sendto.csys")
                    While Not r.EndOfStream
                        Dim l() As String = r.ReadLine.Split("|")
                        If l.Count = 2 Then
                            DGV_sendto.Rows.Add(l(0), l(1))
                        End If

                    End While

                    r.Close()
                End Using
            End If

            If File.Exists(home_path & "\sys\Conditions\" & eNetName & "\text.csys") Then


                Using r As StreamReader = New StreamReader(home_path + "\sys\Conditions\" & eNetName & "\text.csys")
                    Dim t() As String = r.ReadToEnd.Replace(vbCrLf, "").Split("|")


                    If Not t.Length < 1 Then TB_STATUS_TEXT.Text = t(0)
                    If Not t.Length < 2 Then TB_Cond_TEXT.Text = t(1)

                    r.Close()
                End Using

            End If

        End If

        If (TB_tabs.SelectedTab.Name = "page_other") Then

            ' Drausio
            If Not partNumberField = "" Then
                txtInputPartNumber.Text = Get_Node_Value(TGV_MTC.Nodes, partNumberField)
            End If
            If Not progNumberField = "" Then
                txtInputProgrNumber.Text = Get_Node_Value(TGV_MTC.Nodes, progNumberField)
            End If
            If Not palletField = "" Then
                txtInputPallet.Text = Get_Node_Value(TGV_MTC.Nodes, palletField)
            End If
        End If

        previous_tab = TB_tabs.SelectedTab.Name
    End Sub

    Private Function Get_Node_Value(treeNodes As TreeGridNodeCollection, key As String) As String

        Dim result As String = ""

        For Each node As TreeGridNode In treeNodes
            If node.HasChildren Then
                result = Get_Node_Value(node.Nodes, key)
            ElseIf node.Tag.Id = key Then
                result = node.Cells.Item(1).Value
                Exit For
            End If
            If Not result = "" Then Exit For
        Next

        Return result

    End Function

#Region "some var"

    Private delete_image As Image = Nothing
    Dim TGV_images As New ImageList()

    Public ConnectorId As Integer
    Public MachineId As Integer
    Public MachineName As String
    Public eNetName As String
    Public MTCMachineName As String '= String.Empty
    Public MachineIP As String
    Public AdapterIPAddress As String
    Public Delay_Val As String
    Public ConnectorType As String
    Private m_client As EntityClient
    Private statusdt As New List(Of DataTable)
    Public Devices
    Private StatusAssociationDT As New DataTable
    Public Tree As New TreeNode
    Public version As String = "Lightsout" ' server or Lightsout
    Public home_path As String = CSI_Library.CSI_Library.serverRootPath
    Public Conditions_expression As New Dictionary(Of String, String) ' Status, expression
    Public CONDITIONS As List(Of String)
    Dim mch__ As CSI_Library.MCH_

    Dim selectedStatus As String
    Dim isStatusDisabled As Boolean = False
    Dim isMinuteScale As Boolean

    Dim monitoringUnitId As Integer = 0
    Dim monitoringUnitName As String = ""
    Dim monitoringUnitIpAddress As String = ""
    Dim monitoringUnitMacAddress As String = ""
    Dim monitoringUnitProbeAddress As String = ""
    Dim monitoringUnitCurrentAddress As String = ""

    Dim hasChanges As Boolean = False
    Dim isLoading As Boolean = False
    Dim Current_selected_ As New List(Of String)

#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "main form load/close/paint"

    Private Sub Adv_MTC_cond_edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Log.Info("Mtconnect logic editor opened")

            isLoading = True

            dgridConditions.EnableHeadersVisualStyles = False
            dgridConditions.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray
            dgridConditions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White

            dgviewNotificationStatus.EnableHeadersVisualStyles = False
            dgviewNotificationStatus.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray
            dgviewNotificationStatus.ColumnHeadersDefaultCellStyle.ForeColor = Color.White

            DGViewConditionsFormat(dgviewNotifConditions)

            'dgviewNotifConditions.EnableHeadersVisualStyles = False
            'dgviewNotifConditions.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray
            'dgviewNotifConditions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White

            DGV_COND_notif.EnableHeadersVisualStyles = False
            DGV_COND_notif.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray
            DGV_COND_notif.ColumnHeadersDefaultCellStyle.ForeColor = Color.White

            pbFocas.Visible = (ConnectorType = "Focas")
            PB_MTC_LOGO.Visible = (ConnectorType = "MTConnect")

            delete_image = My.Resources.ResourceManager.GetObject("Delete")
            TGV_images.Images.Add(My.Resources.ResourceManager.GetObject("Robot"))
            TGV_images.Images.Add(My.Resources.ResourceManager.GetObject("Agent"))
            TGV_MTC.ImageList = TGV_images

            statusdt = New List(Of DataTable)
            'MTCMachineName = MachineName 'This is  Machinename Coming from Server 

            Dim eNETMachineName As String = String.Empty
            eNETMachineName = eNetName

            Dim tblConnector = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_csiconnector WHERE Id = { ConnectorId } ;")

            If tblConnector.Rows.Count > 0 Then
                Dim rowConnector = tblConnector.Rows(0)
                MachineId = rowConnector("MachineId")
                MachineName = rowConnector("eNETMachineName").ToString()
                MTCMachineName = rowConnector("MTCMachine").ToString()
                eNETMachineName = rowConnector("eNETMachineName").ToString()
                monitoringUnitId = rowConnector("MonitoringUnitId")
            End If

            mch__ = New CSI_Library.MCH_(ConnectorId, MachineId, MachineName, MTCMachineName, CSI_Library.CSI_Library.serverRootPath, MachineIP) 'CSI_Lib 'MachineIP = agent IP Address

            LB_MachineAddress.Text = $"{ eNETMachineName } ({ MTCMachineName }) : { MachineIP }"
            lblMonitoringUnit.Text = $""

            If monitoringUnitId > 0 Then
                Dim tblMonitoringUnits = MySqlAccess.GetDataTable($"SELECT * FROM monitoring.monitoringboards WHERE Id = {monitoringUnitId}")
                If tblMonitoringUnits.Rows.Count > 0 Then
                    monitoringUnitName = tblMonitoringUnits.Rows(0)("Name").ToString()
                    monitoringUnitIpAddress = tblMonitoringUnits.Rows(0)("IpAddress").ToString()
                    monitoringUnitMacAddress = tblMonitoringUnits.Rows(0)("Mac").ToString()

                    If Not monitoringUnitIpAddress.ToUpper().StartsWith("HTTP://") Then
                        monitoringUnitIpAddress = $"http://{monitoringUnitIpAddress}"
                    End If
                    monitoringUnitProbeAddress = $"{monitoringUnitIpAddress}/probe"
                    monitoringUnitCurrentAddress = $"{monitoringUnitIpAddress}/current"

                    lblMonitoringUnit.Text = $"Monitoring Unit : {monitoringUnitName}"
                End If
            End If

            If String.IsNullOrEmpty(lblMonitoringUnit.Text) Then
                grpPallet.Visible = False
                'grpOthers.Location = New Point(9, 300)
                panelOther.Height = panelOther.Height - 310
            End If

            m_client = New EntityClient(AdapterIPAddress) 'agent IP Address
            m_client.RequestTimeout = 10000

            Devices = m_client.Probe()

            Dim dev As DeviceCollection = Devices

            'Dim muCurrentXml As Task(Of String) = MonitoringBoardsService.GetProbeXml(monitoringUnitId)
            Dim muCurrentXml = MonitoringBoardsService.GetProbeXml(monitoringUnitId)

            'Dim muDevices = m_client.ProbeFromXml(muCurrentXml.Result)
            Dim muDevices = m_client.ProbeFromXml(muCurrentXml)

            For Each device As Device In muDevices
                dev.Add(device)
            Next

            If (dev.Count > 0) Then

                Dim device = dev.Where(Function(f) f.Name = MTCMachineName).ToList

                If (device.Count = 1) Then
                    BG_filltree.RunWorkerAsync()

                    StatusAssociationDT = New DataTable()
                    StatusAssociationDT.Columns.Add("status")
                    StatusAssociationDT.Columns.Add("mtcid")

                    If dgridConditions.ColumnCount = 1 Then

                        If Not dgridConditions.Columns.Contains(ID_Combo_column) Then
                            'Component Combobox That is commented by Bhavik Desai for Testing
                            ID_Combo_column.HeaderText = "Item"
                            ID_Combo_column.DefaultCellStyle.BackColor = Color.White
                            ID_Combo_column.FlatStyle = FlatStyle.Flat
                            dgridConditions.Columns.Add(ID_Combo_column)
                        End If

                        'value condition
                        Dim Condtype_Combo_column As New DataGridViewComboBoxColumn
                        Condtype_Combo_column.HeaderText = "Operator"
                        Condtype_Combo_column.Items.Add("<")
                        Condtype_Combo_column.Items.Add(">")
                        Condtype_Combo_column.Items.Add("=")
                        Condtype_Combo_column.Items.Add("!=")
                        Condtype_Combo_column.Items.Add("On change")
                        Condtype_Combo_column.FlatStyle = FlatStyle.Flat
                        dgridConditions.Columns.Add(Condtype_Combo_column)

                        'Column Value
                        Dim Value_Combo_column As New DataGridViewTextBoxColumn
                        Value_Combo_column.HeaderText = "Value"
                        dgridConditions.Columns.Add(Value_Combo_column)

                    End If

                    'Add First Status 
                    cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
                    Add.Text = "Add"
                    cmbStatus.Items.Clear()
                    cmbStatus.Items.Add("CYCLE ON")
                    cmbStatus.SelectedIndex = 0

                    Dim loadAllStatus = MySqlAccess.GetDataTable($"SELECT * from csi_auth.tbl_mtcfocasconditions WHERE ConnectorId = { ConnectorId } ;")

                    If loadAllStatus.Rows.Count > 0 Then

                        For Each rows As DataRow In loadAllStatus.Rows

                            Dim status = rows("Status").ToString()
                            Dim condition = rows("condition").ToString()

                            If Conditions_expression.ContainsKey(status) Then
                                Conditions_expression.Item(status) = condition
                            Else
                                Conditions_expression.Add(status, condition)
                            End If


                            If status <> "CYCLE ON" Then
                                cmbStatus.Items.Add(status)
                            End If
                        Next

                    End If

                    'Disable the first cell
                    dgridConditions.Rows.Item(0).Cells.Item(0).ReadOnly = True
                    dgridConditions.Rows.Item(0).Cells.Item(0).Style.BackColor = Color.LightGray

                    readsetup()
                    isLoading = True

                    'AddHandler txtExpression.TextChanged, AddressOf txtExpression_TextChanged
                    AddHandler dgridConditions.CellValueChanged, AddressOf dgridConditions_Cellvaluechange

                    dgridConditions_Cellvaluechange(Me, New DataGridViewCellEventArgs(0, 0))

                    Timer_threads.Start()
                    EmergencyClosing = False

                ElseIf (device.Count = 0) Then

                    MessageBox.Show("Machine name not found in device collection")
                    EmergencyClosing = True
                    Me.Close()
                Else

                    MessageBox.Show("More than 1 machine with the same name were found in device collection")
                    EmergencyClosing = True
                    Me.Close()
                End If
            Else
                MessageBox.Show("The device on this IP address is not responding. Please check your connection.")
                EmergencyClosing = True
                Me.Close()
            End If

            Dim imageListLarge As New ImageList()
            imageListLarge.ImageSize = New Size(25, 25)
            imageListLarge.Images.AddRange({My.Resources.pencil_draw_edit_sketch_tool_3d459782aa34d16b_512x512, My.Resources.cancel_button_icon_63471, My.Resources.Check_mark_button_hi})
            BTN_FeedrateEdit.ImageList = imageListLarge
            BTN_FeedrateEdit.ImageIndex = 0
            BTN_RapidEdit.ImageList = imageListLarge
            BTN_RapidEdit.ImageIndex = 0
            BTN_SpindleEdit.ImageList = imageListLarge
            BTN_SpindleEdit.ImageIndex = 0
            BTN_FeedrateOK.ImageList = imageListLarge
            BTN_FeedrateOK.ImageIndex = 2
            BTN_RapidOK.ImageList = imageListLarge
            BTN_RapidOK.ImageIndex = 2
            BTN_SpindleOK.ImageList = imageListLarge
            BTN_SpindleOK.ImageIndex = 2

            'AF! Reload tab
            previous_tab = ""
            previous_index = 0
            tb_tabs_click(Nothing, Nothing)

            isLoading = False

        Catch ex As Exception

            Log.Error(ex)
            MessageBox.Show(ex.Message & "and " & ex.StackTrace())
            EmergencyClosing = True
            Me.Close()
        End Try

    End Sub


    Private Sub DGViewConditionsFormat(dgv As DataGridView)

        Dim idColumn As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
        idColumn.Items.Add("Test")

        dgv.EnableHeadersVisualStyles = False
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White

        If dgv.ColumnCount = 1 Then

            If Not dgv.Columns.Contains(idColumn) Then
                'Component Combobox That is commented by Bhavik Desai for Testing
                idColumn.HeaderText = "Item"
                idColumn.DefaultCellStyle.BackColor = Color.White
                idColumn.FlatStyle = FlatStyle.Flat
                dgv.Columns.Add(idColumn)
            End If

            'value condition
            Dim operatorColumn As New DataGridViewComboBoxColumn
            operatorColumn.HeaderText = "Operator"
            operatorColumn.Items.Add("<")
            operatorColumn.Items.Add(">")
            operatorColumn.Items.Add("=")
            operatorColumn.Items.Add("!=")
            operatorColumn.Items.Add("On change")
            operatorColumn.FlatStyle = FlatStyle.Flat
            dgv.Columns.Add(operatorColumn)

            'Column Value
            Dim valueColumn As New DataGridViewTextBoxColumn
            valueColumn.HeaderText = "Value"
            dgv.Columns.Add(valueColumn)

        End If

    End Sub


    Private Sub Adv_MTC_cond_edit_Lonclose(sender As Object, e As EventArgs) Handles MyBase.Closing

        Try
            If EmergencyClosing = False Then

                'save_setup()

                If TB_tabs.SelectedTab.Name = "page_notif" Then save_setup_notif()

                If BW_refresh_tree.IsBusy Then
                    BW_refresh_tree.CancelAsync()
                    BW_refresh_tree.Dispose()
                End If

                If BW_refresh_Status.IsBusy Then
                    BW_refresh_Status.CancelAsync()
                    BW_refresh_Status.Dispose()
                End If

                Timer_threads.Stop()

                Dim ref As New CSI_Library.CSI_Library(True)

                ref.readsetup_for_adv_mtc()
                SetupForm2.RefreshAllDevices()
                CSI_Library.CSI_Library.Current_selected_.Clear()

                For Each table As DataTable In conditions_array.Tables
                    For Each row As DataRow In table.Rows

                        If Not CSI_Library.CSI_Library.Current_selected_.Contains(row.Item("item") & " = ") Then CSI_Library.CSI_Library.Current_selected_.Add(row.Item("item") & " = ")

                    Next
                Next

                RefreshServiceSettings()

                Log.Debug("Mtconnect logic editor closed")

            End If
        Catch ex As Exception

            MsgBox(ex.Message)
            Log.Error("Err while closing ADV_MTC_EDT", ex)

        End Try

    End Sub



    Dim EmergencyClosing As Boolean

    Private Sub DGV_COND_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgridConditions.EditingControlShowing

        If dgridConditions.EditingControl.[GetType]() = GetType(DataGridViewTextBoxEditingControl) Then

            Dim prodCode As TextBox = TryCast(e.Control, TextBox)

            If dgridConditions.CurrentCell.ColumnIndex = 3 And dgridConditions.CurrentRow.Cells.Item(1).Value IsNot Nothing Then
                Dim source = New AutoCompleteStringCollection()
                Dim stringArray() As String

                stringArray = mch__.Get_choices(dgridConditions.CurrentRow.Cells.Item(1).Value).ToArray
                source.AddRange(stringArray)

                If prodCode IsNot Nothing Then
                    prodCode.AutoCompleteMode = AutoCompleteMode.Suggest
                    prodCode.AutoCompleteCustomSource = source
                    prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource
                End If
            Else
                prodCode.AutoCompleteCustomSource = Nothing
            End If
        End If
    End Sub

    Private Sub Adv_MTC_cond_edit_paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        ResizeAutoSizeColumn(LV_Property, 1)

    End Sub

    Private Sub Adv_MTC_cond_edit_resizeSTART(sender As Object, e As EventArgs) Handles MyBase.ResizeBegin
        Me.SuspendLayout()
    End Sub

    Private Sub Adv_MTC_cond_edit_resizeEND(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd

        Me.ResumeLayout()
        '  Me.Refresh()
    End Sub

#End Region


#Region "read and save setup"
    Dim delays As New Dictionary(Of String, Integer)  ' status - delay

    ''' <summary>
    ''' Read setup files in conditions/machine_name/
    ''' </summary>
    Private Sub readsetup()
        Try
            hasChanges = False
            isLoading = True

            If cmbStatus.Text.Length > 0 Then
                ReadStatusFromDatabase(MachineName, cmbStatus.SelectedItem)
                nudDelay.Value = Delay_Val
            End If

            If ConnectorType = "MTConnect" Then
                GetOtherSettingsFromDB(MachineName, MachineIP)
            ElseIf ConnectorType = "Focas" Then
                GetOtherSettingsFromDB(MachineName, AdapterIPAddress)
            End If

            hasChanges = False
            isLoading = False

        Catch ex As Exception
            MsgBox("Error in reading setup : " & ex.Message)
            Log.Error(ex)
        End Try
    End Sub

    Dim partNumberField As String = ""
    Dim progNumberField As String = ""
    Dim palletField As String = ""

    Private Sub GetOtherSettingsFromDB(Machinename As String, IPAddress As String)

        Dim table = MySqlAccess.GetDataTable($"SELECT * from csi_auth.view_csiothersettings WHERE ConnectorId = { ConnectorId };")

        If table.Rows.Count > 0 Then

            partNumberField = table.Rows(0).Item("PartNumber_Variable").ToString()
            lblPartNumber.Text = "Part Number : " + partNumberField
            txtPNrOutput.Text = table.Rows(0).Item("PartNumber_Value").ToString()

            txtPNrPrefix1.Text = table.Rows(0).Item("PartNumber_Prefix1").ToString()
            txtPNrFilter1Start.Text = table.Rows(0).Item("PartNumber_Filter1Start").ToString()
            txtPNrFilter1End.Text = table.Rows(0).Item("PartNumber_Filter1End").ToString()

            chkFilter2.Checked = table.Rows(0).Item("PartNumber_Filter2Apply")
            txtPNrPrefix2.Text = table.Rows(0).Item("PartNumber_Prefix2").ToString()
            txtPNrFilter2Start.Text = table.Rows(0).Item("PartNumber_Filter2Start").ToString()
            txtPNrFilter2End.Text = table.Rows(0).Item("PartNumber_Filter2End").ToString()

            chkFilter3.Checked = table.Rows(0).Item("PartNumber_Filter3Apply")
            txtPNrPrefix3.Text = table.Rows(0).Item("PartNumber_Prefix3").ToString()
            txtPNrFilter3Start.Text = table.Rows(0).Item("PartNumber_Filter3Start").ToString()
            txtPNrFilter3End.Text = table.Rows(0).Item("PartNumber_Filter3End").ToString()

            progNumberField = table.Rows(0).Item("ProgramNumber_Variable").ToString()
            lblProgramNumber.Text = "Program No : " + progNumberField
            txtOutputProgrNumber.Text = table.Rows(0).Item("ProgramNumber_Value").ToString()
            txtStartProgNumber.Text = table.Rows(0).Item("ProgramNumber_FilterStart").ToString()
            txtEndProgNumber.Text = table.Rows(0).Item("ProgramNumber_FilterEnd").ToString()

            chkOperation.Checked = table.Rows(0).Item("Operation_Available")
            txtOperationStart.Text = table.Rows(0).Item("Operation_FilterStart").ToString()
            txtOperationEnd.Text = table.Rows(0).Item("Operation_FilterEnd").ToString()

            palletField = table.Rows(0).Item("ActivePallet_Var").ToString()
            lblActivePallet.Text = $"Active Pallet: {palletField}"
            txtOutputPallet.Text = table.Rows(0).Item("ActivePallet_Value").ToString()
            txtStartPallet.Text = table.Rows(0).Item("ActivePallet_StartWith").ToString()
            txtEndPallet.Text = table.Rows(0).Item("ActivePallet_EndWith").ToString()
            chkSendPallet.Checked = table.Rows(0).Item("ActivePallet_ToMU")

            chkEnableCriticalStop.Checked = table.Rows(0).Item("EnableMCS")
            chkSaveDataraw.Checked = table.Rows(0).Item("SaveDataRaw")
            chkSaveCOnDuringSetup.Checked = table.Rows(0).Item("COnDuringSetup")

            chkSaveRawDuringProd.Visible = chkSaveDataraw.Checked
            chkSaveRawDuringProd.Checked = (table.Rows(0).Item("SaveProdOnly") > 0)

            chkSaveRawDuringSetup.Visible = chkSaveRawDuringProd.Checked
            chkSaveRawDuringSetup.Checked = (table.Rows(0).Item("SaveProdOnly") > 1)
            'chkEnableCriticalStop.Visible = monitoringUnitId > 0

            txtWarningPressure.Text = table.Rows(0).Item("WarningPressure")
            txtCriticalPressure.Text = table.Rows(0).Item("CriticalPressure")
            txtWarningTemperature.Text = table.Rows(0).Item("WarningTemperature")
            txtCriticalTemperature.Text = table.Rows(0).Item("CriticalTemperature")

            If table.Rows(0).Item("DelayScale") = "sec" Then
                cmbMCSScale.SelectedIndex = 0
                numMCSDelay.Value = table.Rows(0).Item("MCSDelay")
            Else
                cmbMCSScale.SelectedIndex = 1
                numMCSDelay.Value = table.Rows(0).Item("MCSDelay") / 60
            End If

            If table.Rows(0).Item("DelayForCycleOffScale").ToString() = "sec" Then
                cmbDelayForCycleOffScale.SelectedIndex = 0
                nudDelayForCycleOff.Value = table.Rows(0).Item("DelayForCycleOff")
            Else
                cmbDelayForCycleOffScale.SelectedIndex = 0
                nudDelayForCycleOff.Value = table.Rows(0).Item("DelayForCycleOff") / 60
            End If


            If Not txtOutputProgrNumber.Text = "NOT AVAILABLE" Then
                txtInputProgrNumber.Text = txtOutputProgrNumber.Text
            End If

            lblFovr.Text = "Feedrate Override : " + table.Rows(0).Item("FeedRate_Variable").ToString()
            lblSovr.Text = "Spindle Override : " + table.Rows(0).Item("Spindle_Variable").ToString()
            lblRovr.Text = "Rapid Override : " + table.Rows(0).Item("Rapid_Variable").ToString()
            lblRequiredParts.Text = $"Required parts: { table.Rows(0).Item("PartRequired_Variable").ToString() }"
            lblPartsCount.Text = $"Part count: { table.Rows(0).Item("PartCount_Variable").ToString() }"

            txtMinFovr.Text = table.Rows(0).Item("Feedrate_MIN").ToString()
            txtMaxFovr.Text = table.Rows(0).Item("Feedrate_MAX").ToString()
            txtMinRovr.Text = table.Rows(0).Item("Rapid_MIN").ToString()
            txtMaxRovr.Text = table.Rows(0).Item("Rapid_MAX").ToString()
            txtMinSovr.Text = table.Rows(0).Item("Spindle_MIN").ToString()
            txtMaxSovr.Text = table.Rows(0).Item("Spindle_MAX").ToString()

        End If
    End Sub

    Private Sub ReadStatusFromDatabase(Machinename As String, SelectedStatus As String)

        dgridConditions.Rows.Clear()

        Dim delay As Integer

        Try

            Dim loadconditions = MySqlAccess.GetDataTable($"SELECT * from csi_auth.tbl_mtcfocasconditions WHERE ConnectorId = { ConnectorId } And `Status` = '{ SelectedStatus }' ;")

            For Each row As DataRow In loadconditions.Rows

                delay = row("delay")
                isMinuteScale = (row("DelayScale") = "min")

                If isMinuteScale Then delay = delay / 60

                nudDelay.Value = delay
                cmbTimeDelay.SelectedIndex = IIf(isMinuteScale, 1, 0)


                Dim condition = row("Condition").ToString()

                txtExpression.Text = condition
                chkCsdOnSetup.Checked = row("CsdOnSetup")
                'chkSaveCOnDuringSetup.Checked = row("COnDuringSetup")

                Delay_Val = delay

                isStatusDisabled = row("StatusDisabled")

                btnDisableStatus.Text = IIf(isStatusDisabled, "Enable Status", "Disable Status")
                lblStatusDisabled.Visible = isStatusDisabled

                Dim count_paranthesis = condition.Split("(").Length - 1 'Count number of conditions 
                Dim AndORCnt = count_paranthesis - 1 ' No of AND/OR Count
                Dim newcondition = condition.Replace(" ", "").Replace("'", "") 'Remove spaces and ' from the condition

                newcondition = condition.Replace("'", "")

                Dim splitnewcondition As String() = newcondition.Split(New String() {"(", ")"}, StringSplitOptions.RemoveEmptyEntries).Select(Function(item) item.Trim()).ToArray()
                Dim length_splitnewcondition = splitnewcondition.Length

                For index As Integer = 0 To length_splitnewcondition - 1

                    Dim substring = splitnewcondition(index).Trim()

                    If Not (substring.Contains("AND") Or substring.Contains("OR")) Then

                        Dim indexofOperator As Integer
                        Dim found As Boolean = False
                        Dim length_substring = substring.Length
                        Dim strlist = New String() {"Onchange", "!=", ">", "<", "="}

                        For listele As Integer = 0 To strlist.Length - 1

                            If found = False Then

                                If substring.Contains(strlist(listele)) Then

                                    dgridConditions.Rows.Add()
                                    indexofOperator = substring.IndexOf(strlist(listele))

                                    Dim operatorlen = strlist(listele).Length
                                    Dim str1st = substring.Substring(0, indexofOperator)
                                    Dim temp = indexofOperator + operatorlen
                                    Dim str2nd = substring.Substring(temp, substring.Length - temp)

                                    If row("Condition") <> "" Then

                                        If index = 0 Then

                                            'This part always executes at least once 
                                            dgridConditions.Rows.Item(index).Cells.Item(0).Value = ""
                                            dgridConditions.Rows.Item(index).Cells.Item(0).ReadOnly = False
                                            If Not DirectCast(dgridConditions.Columns(1), DataGridViewComboBoxColumn).Items.Contains(str1st) Then DirectCast(dgridConditions.Columns(1), DataGridViewComboBoxColumn).Items.Add(str1st)
                                            dgridConditions.Rows.Item(index).Cells.Item(1).Value = str1st  'item column

                                            If strlist(listele) = "Onchange" Then
                                                'When operator is On change Value column is empty
                                                dgridConditions.Rows.Item(index).Cells.Item(2).Value = "On change"   ' operator column
                                            Else
                                                dgridConditions.Rows.Item(index).Cells.Item(2).Value = strlist(listele)   ' operator column
                                            End If

                                            dgridConditions.Rows.Item(index).Cells.Item(3).Value = str2nd  'value column THIS SHOULD BE EMPTY

                                        ElseIf index > 0 Then

                                            If index Mod 2 = 0 Then

                                                dgridConditions.Rows.Item(index / 2).Cells.Item(0).Value = splitnewcondition(index - 1)
                                                If Not DirectCast(dgridConditions.Columns(1), DataGridViewComboBoxColumn).Items.Contains(str1st) Then
                                                    DirectCast(dgridConditions.Columns(1), DataGridViewComboBoxColumn).Items.Add(str1st)
                                                End If

                                                dgridConditions.Rows.Item(index / 2).Cells.Item(1).Value = str1st  'item column
                                                If strlist(listele) = "Onchange" Then
                                                    'When operator is On change Value column is empty
                                                    dgridConditions.Rows.Item(index / 2).Cells.Item(2).Value = "On change"   ' operator column
                                                Else
                                                    dgridConditions.Rows.Item(index / 2).Cells.Item(2).Value = strlist(listele)   ' operator column
                                                End If

                                                dgridConditions.Rows.Item(index / 2).Cells.Item(3).Value = str2nd

                                            End If
                                        End If
                                    End If
                                    found = True
                                End If
                            End If
                        Next
                    End If
                Next
            Next

        Catch ex As Exception

            MessageBox.Show("Error in loading status from Database !")
            Log.Error("Error in loading status from Database", ex)

        End Try
    End Sub

    Dim conditions_array As New DataSet
    Dim loadconditions As New DataSet

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        save_setup()
        hasChanges = False
    End Sub

    Private Sub save_setup(Optional deleted_status As String = Nothing)

        Try
            If deleted_status = Nothing Then
                SaveCurrentConditionToDB()
            Else
                'Do nothing because code already written on  BTN_del.Click event 
                RefreshServiceSettings()
            End If
        Catch ex As Exception

            MsgBox("Could Not save the conditions :  " & ex.Message)
        End Try

    End Sub

    Private Sub SaveCurrentConditionToDB()

        'If Not String.IsNullOrEmpty(txtExpression.Text) And Not String.IsNullOrEmpty(CB_status.SelectedItem) And dgridConditions.Enabled = True Then
        If Not String.IsNullOrEmpty(cmbStatus.SelectedItem) And dgridConditions.Enabled = True Then

            Try
                Dim delay As String = nudDelay.Value
                Dim store_expression = txtExpression.Text.Replace("'", "''")

                Dim dTable_SelectAll = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_mtcfocasconditions WHERE `ConnectorId` = { ConnectorId } AND `Status` = '{ selectedStatus }';")

                Dim cmd As Text.StringBuilder = New StringBuilder()
                Dim delayScale = IIf(isMinuteScale, "min", "sec")

                If isMinuteScale Then delay = delay * 60

                If dTable_SelectAll.Rows.Count > 0 Then

                    cmd.Append($"UPDATE IGNORE csi_auth.tbl_mtcfocasconditions            ")
                    cmd.Append($" SET                                                     ")
                    cmd.Append($"   `Condition`      = '{ store_expression }'    ,        ")
                    cmd.Append($"   `delay`          = '{ delay }'               ,        ")
                    cmd.Append($"   `delayScale`     = '{ delayScale }'          ,        ")
                    cmd.Append($"   `CsdOnSetup`     =  { chkCsdOnSetup.Checked }         ")
                    cmd.Append($" WHERE                                                   ")
                    cmd.Append($"   `ConnectorId`    =  { ConnectorId }                   ")
                    cmd.Append($"   AND `Status`     = '{ selectedStatus }';              ")

                    MySqlAccess.ExecuteNonQuery(cmd.ToString())

                Else

                    cmd.Append($"INSERT IGNORE INTO                     ")
                    cmd.Append($"       csi_auth.tbl_mtcfocasconditions ")
                    cmd.Append($" (                                     ")
                    cmd.Append($"   `ConnectorId`  ,                    ")
                    cmd.Append($"   `Machine_Name` ,                    ")
                    cmd.Append($"   `IP_Address`   ,                    ")
                    cmd.Append($"   `Status`       ,                    ")
                    cmd.Append($"   `Condition`    ,                    ")
                    cmd.Append($"   `Machine_Type` ,                    ")
                    cmd.Append($"   `delay`                             ")
                    cmd.Append($" )                                     ")
                    cmd.Append($"VALUES                                 ")
                    cmd.Append($" (                                     ")
                    cmd.Append($"    { ConnectorId }            ,       ")
                    cmd.Append($"   '{ MachineName }'           ,       ")
                    cmd.Append($"   '{ AdapterIPAddress }'      ,       ")
                    cmd.Append($"   '{ selectedStatus }'        ,       ")
                    cmd.Append($"   '{ store_expression }'      ,       ")
                    cmd.Append($"   '{ ConnectorType }'         ,       ")
                    cmd.Append($"   '{ delay }'                 ,       ")
                    cmd.Append($"    { chkCsdOnSetup.Checked }          ")
                    cmd.Append($" );                                    ")

                    MySqlAccess.ExecuteNonQuery(cmd.ToString())

                End If

                mch__.delays(selectedStatus) = delay

                If Conditions_expression.ContainsKey(selectedStatus) Then
                    Conditions_expression.Item(selectedStatus) = txtExpression.Text
                Else
                    Conditions_expression.Add(selectedStatus, txtExpression.Text)
                End If

            Catch ex As Exception

                Log.Error(ex)
                MsgBox(ex.Message)

            End Try

        End If

    End Sub

    Private Sub RefreshServiceSettings()

        Try

            Dim portT As DataTable = MySqlAccess.GetDataTable("Select port From csi_database.tbl_rm_port;")

            Dim request As WebRequest

            If portT.Rows.Count <> 0 Then
                If IsDBNull(portT.Rows(0)("port")) Then
                    request = WebRequest.Create("http://127.0.0.1:8008/readconfig")
                Else
                    request = WebRequest.Create("http://127.0.0.1:" & portT.Rows(0)("port") & "/readMTCconfig")
                End If
            End If

            request.Method = "POST"

            Dim postData As String = ""
            Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)

            request.ContentType = "application/x-www-form-urlencoded"

            request.ContentLength = byteArray.Length

            Dim dataStream As Stream = request.GetRequestStream()

            dataStream.Write(byteArray, 0, byteArray.Length)

            dataStream.Close()

        Catch ex As Exception

            MsgBox("Could not apply these settings, please restart the CSIFlex service.")
            Log.Error(ex)
        End Try
    End Sub

#End Region


#Region "TreeGview"

    Public root As New TreeNode
    Public TGV_root As New AdvancedDataGridView.TreeGridNode
    Public combodone As Boolean = False
    Public ID_Combo_column As New DataGridViewComboBoxColumn

    Dim Current_stream As DataStream
    Dim values_ As New List(Of String)

    Private Sub populate_TGV(devices As DeviceCollection, machinename As String)

        Dim device_index As Integer = 0

        Try

            TGV_MTC.Nodes.Clear()

            Dim rootnode As TreeGridNode = TGV_MTC.Nodes.Add(devices.AgentInformation.Name, "")

            rootnode.ImageIndex = 1
            'node = node.Nodes.Add("node2", "value2")
            rootnode.Tag = devices.AgentInformation

            Dim Current_device As DeviceStream
            Dim value As String = ""

            For Each device In devices

                If (device.Name = machinename Or device.Name = monitoringUnitName) Then

                    Current_device = Current_stream.DeviceStreams(device_index)

                    Dim current_component_stream() As ComponentStream = Current_device.ComponentStreams
                    Dim devicenode As TreeGridNode = rootnode.Nodes.Add(device.Name, "")

                    devicenode.ImageIndex = 0
                    devicenode.Tag = device

                    For Each component In device.Components
                        FillComponents_TGV(component, devicenode, Current_device)
                    Next

                    For Each dataItem In device.DataItems

                        Dim displayname As String = dataItem.ID

                        If combodone = False Then ID_Combo_column.Items.Add(displayname)

                        If Not (String.IsNullOrEmpty(dataItem.Name)) Then
                            displayname += String.Format(" ({0})", dataItem.Name)
                        End If

                        For Each Current_comp_stream In current_component_stream
                            If Current_comp_stream.ComponentType = "Device" Then
                                Dim current_events() As IDataElement = Current_comp_stream.Events

                                For Each ev In current_events
                                    If dataItem.ID = ev.DataItemID Then
                                        value = ev.Value
                                    End If
                                Next
                            End If
                        Next

                        devicenode = devicenode.Parent.Nodes.Add(displayname, value)
                        devicenode.Tag = dataItem
                        value = ""

                    Next
                End If
                device_index += 1
            Next

            combodone = True
            TGV_expand(TGV_MTC)
            PB_processing.Visible = False
            LBL_RETR.Visible = False

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub FillComponents_TGV(component As OpenNETCF.MTConnect.Component, ByRef parent As TreeGridNode, Current_device As DeviceStream)

        Dim boldFont As Font = New Font(TGV_MTC.DefaultCellStyle.Font.Name, 10, TGV_MTC.DefaultCellStyle.Font.Bold, TGV_MTC.DefaultCellStyle.Font.Unit)

        Dim componentNode As TreeGridNode = parent.Nodes.Add(component.Name, "")

        componentNode.Tag = component
        componentNode.DefaultCellStyle.BackColor = Color.Gray
        componentNode.DefaultCellStyle.ForeColor = Color.White
        componentNode.DefaultCellStyle.Font = boldFont

        For Each subcomponent In component.Components
            FillComponents_TGV(subcomponent, componentNode, Current_device)
        Next

        ' If component.Components.Count = 0 Then
        Dim found As Boolean = False

        Dim component_index As Integer = -1
        For Each subcomponent In Current_device.ComponentStreams
            component_index += 1
            If subcomponent.Name = component.Name Then
                found = True
                Exit For
            End If
        Next

        If found = True Or Current_device.ComponentStreams.Count = 0 Then
            Dim Current_dataitems() As IDataElement = Current_device.ComponentStreams(component_index).AllDataItems
            Dim Current_conditions() As IDataElement = Current_device.ComponentStreams(component_index).Conditions

            Dim first__ As Boolean = True
            Dim value As String = ""
            For Each dataItem In component.DataItems
                Dim displayName As String = dataItem.ID
                If combodone = False Then ID_Combo_column.Items.Add(displayName)
                value = ""
                If Not (String.IsNullOrEmpty(dataItem.Name)) Then
                    displayName += String.Format(" ({0})", dataItem.Name)

                    For Each CurrentdataItem In Current_dataitems
                        If dataItem.ID = CurrentdataItem.DataItemID Then
                            value = CurrentdataItem.Value
                        End If
                    Next
                    For Each Currentcondition In Current_conditions
                        If dataItem.ID = Currentcondition.DataItemID Then
                            value = Currentcondition.Value
                        End If

                    Next

                End If

                If Not (String.IsNullOrEmpty(dataItem.ID)) Then
                    For Each Currentcondition In Current_conditions
                        If dataItem.ID = Currentcondition.DataItemID Then
                            value = Currentcondition.Value
                        End If
                    Next
                    For Each CurrentdataItem In Current_dataitems
                        If dataItem.ID = CurrentdataItem.DataItemID Then
                            value = CurrentdataItem.Value
                        End If
                    Next
                End If

                Dim notboldFont As Font = New Font(TGV_MTC.DefaultCellStyle.Font.Name, 8, TGV_MTC.DefaultCellStyle.Font.Style, TGV_MTC.DefaultCellStyle.Font.Unit)
                If first__ = True Then
                    first__ = False

                    componentNode = componentNode.Nodes.Add(displayName, value) ' go to level down
                    componentNode.DefaultCellStyle.Font = notboldFont
                    componentNode.Tag = dataItem
                    If value IsNot Nothing Then
                        If value.StartsWith("Warning") Then
                            componentNode.DefaultCellStyle.BackColor = Color.Yellow
                        ElseIf value.StartsWith("Fault") Then
                            componentNode.DefaultCellStyle.BackColor = Color.Red
                        ElseIf value.StartsWith("Normal") Then
                            componentNode.DefaultCellStyle.BackColor = Color.LightGreen
                        End If
                    End If
                Else
                    componentNode = componentNode.Parent.Nodes.Add(displayName, value) ' same level
                    componentNode.DefaultCellStyle.Font = notboldFont
                    componentNode.Tag = dataItem
                    If value IsNot Nothing Then
                        If value.StartsWith("Warning") Then
                            componentNode.DefaultCellStyle.BackColor = Color.Yellow
                        ElseIf value.StartsWith("Fault") Then
                            componentNode.DefaultCellStyle.BackColor = Color.Red
                        ElseIf value.StartsWith("Normal") Then
                            componentNode.DefaultCellStyle.BackColor = Color.LightGreen
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub refresh_TGV(treeGN As TreeGridNode, devices As DeviceCollection, machinename As String)

        Dim crt As ISample

        Try
            For Each item As TreeGridNode In treeGN.Nodes
                refresh_TGV(item, devices, machinename)
            Next

            Dim ID_ As String = ""

            For Each item As TreeGridNode In treeGN.Nodes

                If item.Cells.Item(0).Value.indexof(" (") <> -1 Then
                    ID_ = item.Cells.Item(0).Value.substring(0, item.Cells.Item(0).Value.indexof(" ("))
                Else
                    ID_ = item.Cells.Item(0).Value
                End If

                If Current_stream IsNot Nothing Then
                    crt = Current_stream.GetSample(ID_)

                    If crt Is Nothing Then

                        Dim Conds() As IDataElement = Current_stream.AllConditions

                        For Each cond In Conds
                            If cond.DataItemID = ID_ Then
                                item.Cells.Item(1).Value = cond.Value
                                If cond.Value IsNot Nothing Then
                                    If cond.Value.StartsWith("Warning") Then
                                        item.DefaultCellStyle.BackColor = Color.Yellow
                                    ElseIf cond.Value.StartsWith("Fault") Then
                                        item.DefaultCellStyle.BackColor = Color.Red
                                    ElseIf cond.Value.StartsWith("Normal") Then
                                        item.DefaultCellStyle.BackColor = Color.LightGreen
                                    End If
                                End If
                                If TGV_MTC.CurrentNode IsNot Nothing Then
                                    If TGV_MTC.CurrentNode.Tag.ToString() <> "OpenNETCF.MTConnect.AgentInformation" Then
                                        If TGV_MTC.CurrentNode.Tag.id = item.Tag.id Then
                                            Dim lvi As New ListViewItem("Current value", "")
                                            lvi.SubItems.Add(If(cond.Value Is Nothing, "", cond.Value.ToString()))
                                            If LV_Property.Items.Count > 0 Then LV_Property.Items.Item(0) = lvi
                                        End If
                                    End If
                                End If
                            End If
                        Next
                        Dim Events() As IDataElement = Current_stream.AllEvents
                        For Each event_ In Events
                            If event_.DataItemID = ID_ Then
                                item.Cells.Item(1).Value = event_.Value
                                If TGV_MTC.CurrentNode IsNot Nothing Then
                                    If TGV_MTC.CurrentNode.Tag.ToString() <> "OpenNETCF.MTConnect.AgentInformation" Then
                                        If TGV_MTC.CurrentNode.Tag.id = item.Tag.id Then
                                            Dim lvi As New ListViewItem("Current value", "")
                                            lvi.SubItems.Add(If(event_.Value Is Nothing, "", event_.Value.ToString()))
                                            If LV_Property.Items.Count > 0 Then LV_Property.Items.Item(0) = lvi
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    Else
                        item.Cells.Item(1).Value = crt.Value
                        If TGV_MTC.CurrentNode IsNot Nothing Then
                            If TGV_MTC.CurrentNode.Tag.ToString() <> "OpenNETCF.MTConnect.AgentInformation" Then
                                If TGV_MTC.CurrentNode IsNot Nothing Then
                                    If TGV_MTC.CurrentNode.Tag.id = item.Tag.id Then
                                        Dim lvi As New ListViewItem("Current value", "")
                                        lvi.SubItems.Add(If(crt.Value Is Nothing, "", crt.Value.ToString()))
                                        If LV_Property.Items.Count > 0 Then LV_Property.Items.Item(0) = lvi
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next

        Catch ex As Exception
            CSI_Lib.LogServerError("Err in refresh_TGV: " + ex.Message, 1)
        End Try
    End Sub

    Private Sub TGV_expand(ByRef TGV As TreeGridView)
        For Each node__ As TreeGridNode In TGV.Nodes
            node__.Expand()
            TGV_node_expand(node__)
        Next
    End Sub

    Private Sub TGV_node_expand(ByRef TGV As TreeGridNode)
        For Each node__ As TreeGridNode In TGV.Nodes
            TGV.Expand()
            TGV_node_expand(node__)
        Next

    End Sub

    Private Sub TGV_MTC_CellContentClick(sender As Object, e As MouseEventArgs) Handles TGV_MTC.MouseUp  ', TGV_MTC.CellContentClick
        Try

            If e.Button = MouseButtons.Right Then
                Dim hit As DataGridView.HitTestInfo =
            TGV_MTC.HitTest(e.X, e.Y)
                If hit.Type = DataGridViewHitTestType.Cell Then
                    TGV_MTC.CurrentCell = TGV_MTC.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                End If
            Else
                PopulatePropertyList(TGV_MTC.CurrentNode.Tag)
                Dim lvi As New ListViewItem("Current value", "")
                If Not IsNothing(TGV_MTC.CurrentRow.Cells.Item(1).Value) Then
                    lvi.SubItems.Add(TGV_MTC.CurrentRow.Cells.Item(1).Value)
                End If
                If LV_Property.Items.Count > 0 Then LV_Property.Items.Item(0) = lvi
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function PopulateTree(devices As DeviceCollection, machinename As String)
        Try
            Dim device_index As Integer = 0
            Current_stream = m_client.Current()
            root = New TreeNode(devices.AgentInformation.Name)
            root.Tag = devices.AgentInformation
            Dim Current_device As DeviceStream
            For Each device In devices
                If (machinename = device.Name) Then
                    Current_device = Current_stream.DeviceStreams(device_index)
                    Dim current_component_stream() As ComponentStream = Current_device.ComponentStreams
                    Dim deviceNode As New TreeNode(device.Name)
                    deviceNode.Tag = device
                    For Each component In device.Components
                        FillComponents(component, deviceNode, Current_device)
                    Next

                    For Each dataItem In device.DataItems
                        Dim displayName As String = dataItem.ID
                        If combodone = False Then ID_Combo_column.Items.Add(displayName)
                        If Not (String.IsNullOrEmpty(dataItem.Name)) Then
                            displayName += String.Format(" ({0})", dataItem.Name)
                            displayName += " = [Value]"
                        End If
                        For Each Current_comp_stream In current_component_stream
                            If Current_comp_stream.ComponentType = "Device" Then
                                Dim current_events() As IDataElement = Current_comp_stream.Events

                                For Each ev In current_events
                                    If displayName = ev.DataItemID Then
                                        displayName += " = [" & ev.Value & "]"

                                    End If
                                Next
                            End If
                        Next
                        Dim dataNode As New TreeNode(displayName)
                        Dim TGV_dataNode As New TreeGridNode()
                        dataNode.Tag = dataItem
                        TGV_dataNode.Tag = dataItem
                        deviceNode.Nodes.Add(dataNode)
                    Next
                    root.Nodes.Add(deviceNode)
                End If
                device_index += 1
            Next

            root.ExpandAll()
            root.Tag = devices
            TGV_root.Tag = devices
            root.Nodes(0).NodeFont = New Drawing.Font("Segoe UI", 12, FontStyle.Bold And FontStyle.Underline)
            combodone = True
            Return True
        Catch ex As Exception
            MsgBox("Could Not retreive Mtconnect data")
            CSI_Lib.LogServerError(ex.Message, 1)
            Return False
        End Try

    End Function

    Private Sub FillComponents(component As OpenNETCF.MTConnect.Component, ByRef parent As TreeNode, Current_device As DeviceStream)

        Dim componentNode As New TreeNode(component.Name)
        componentNode.Tag = component

        For Each subcomponent In component.Components
            FillComponents(subcomponent, componentNode, Current_device)
        Next

        Dim component_index As Integer = -1
        For Each subcomponent In Current_device.ComponentStreams
            component_index += 1
            If subcomponent.Name = component.Name Then Exit For
        Next

        Dim Current_dataitems() As IDataElement = Current_device.ComponentStreams(component_index).AllDataItems
        Dim Current_conditions() As IDataElement = Current_device.ComponentStreams(component_index).Conditions
        Dim Current_Samples() As IDataElement = Current_device.ComponentStreams(component_index).Samples

        For Each dataItem In component.DataItems


            Dim displayName As String = dataItem.ID
            If combodone = False Then ID_Combo_column.Items.Add(displayName)
            If Not (String.IsNullOrEmpty(dataItem.Name)) Then
                displayName += String.Format(" ({0})", dataItem.Name)
            End If
            If Not (String.IsNullOrEmpty(dataItem.ID)) Then
                For Each CurrentdataItem In Current_dataitems
                    If dataItem.ID = CurrentdataItem.DataItemID Then
                        displayName += " =[" & CurrentdataItem.Value & "]"
                    End If
                Next
            End If
            Dim dataNode As New TreeNode(displayName)
            dataNode.Tag = dataItem
            componentNode.Nodes.Add(dataNode)
        Next
        parent.Nodes.Add(componentNode)
    End Sub

#End Region


    Private Sub refresh_tree(TV_MTC As TreeNode)
        For Each node As TreeNode In TV_MTC.Nodes
            refresh_tree(node)
        Next

        Dim tag = TV_MTC.Tag

        TV_MTC.Text = tag.ID & " (" & tag.name & ") = " & "[Value]"
    End Sub

    Private Sub TV_MTC_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TV_MTC.AfterSelect
        Try
            PopulatePropertyList(e.Node.Tag)
            If e.Node.Tag.ToString() = "OpenNETCF.MTConnect.DataItem" Then
                Dim value() As String = e.Node.Text.Split("=")

                Dim lvi As New ListViewItem("Current value", "")
                If value.Count > 1 Then
                    If Not IsNothing(value(1)) Then
                        lvi.SubItems.Add(value(1))
                    End If
                End If
                LV_Property.Items.Item(0) = lvi
            End If

            If TV_MTC.SelectedNode.Nodes.Count = 0 Then
                '  BTN_AddCond.Text = "Add condition With: " & TV_MTC.SelectedNode.Text
                If dgridConditions.SelectedRows.Count <> 0 Then dgridConditions.SelectedRows.Item(0).Cells.Item("Item").Value = TV_MTC.SelectedNode.Text
            Else
                '  BTN_AddCond.Text = "Add condition"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            CSI_Lib.LogServerError(ex.Message, 1)
        End Try
    End Sub

    '===================================================================================================
    ' Fill the property list view
    '===================================================================================================
    Public Sub PopulatePropertyList(source As Object)
        Try
            LV_Property.Items.Clear()
            LV_Property.GridLines = True
            LV_Property.FullRowSelect = True


            If (source.ToString() = "OpenNETCF.MTConnect.AgentInformation") Then
                Dim info As AgentInformation = source

                Dim itm As New ListViewItem("Name")
                itm.SubItems.Add(info.Name())
                Dim itm2 As New ListViewItem("Instance ID")
                itm2.SubItems.Add(info.InstanceID())
                Dim itm3 As New ListViewItem("Version")
                itm3.SubItems.Add(info.Version())
                LV_Property.Items.Add(itm)
                LV_Property.Items.Add(itm2)
                LV_Property.Items.Add(itm3)
            End If

            If (source.ToString() = "OpenNETCF.MTConnect.DataItem") Then
                Dim lvi As New ListViewItem("Current value", "")
                Dim dataItem As DataItem = source
                lvi.Tag = dataItem
                LV_Property.Items.Add(lvi)

                For Each prop In dataItem.Properties
                    Dim itm As New ListViewItem(prop.Key)
                    itm.SubItems.Add(prop.Value)
                    LV_Property.Items.Add(itm)
                Next
            End If

            If (source.ToString() = "OpenNETCF.MTConnect.ComponentBase") Then
                Dim item As ComponentBase = source
                For Each prop In item.Properties
                    Dim itm As New ListViewItem(prop.Key)
                    itm.SubItems.Add(prop.Value)
                    LV_Property.Items.Add(itm)

                Next
            End If
        Catch ex As Exception
            CSI_Lib.LogServerError(ex.Message, 1)
        End Try
    End Sub

    Private Sub dgridConditions_Cellvaluechange(sender As Object, e As DataGridViewCellEventArgs)

        If Not isLoading Then hasChanges = True

        Dim point = 0
        Dim vOperator = ""
        Dim vParameter = ""
        Dim vCondition = ""
        Dim vValue = ""

        Try
            If dgridConditions.Rows.Item(0).Cells.Count > 1 Then

                Dim interpreted As String = ""

                LB_current_selected_values.Items.Clear()

                point = 1

                Current_selected_.Clear()

                point = 2

                If txtExpression.Text <> "" Then txtExpression.Clear()

                For Each row As DataGridViewRow In dgridConditions.Rows

                    point = 3

                    If String.IsNullOrEmpty(row.Cells.Item(1).Value) Then Exit For

                    point = 4

                    vOperator = ""
                    vParameter = ""
                    vCondition = ""
                    vValue = ""

                    If row.Cells.Item(0).Value IsNot Nothing Then
                        vOperator = row.Cells.Item(0).Value.ToString().Trim()
                    End If
                    If row.Cells.Item(1).Value IsNot Nothing Then
                        vParameter = row.Cells.Item(1).Value.ToString().Trim()
                    End If
                    If row.Cells.Item(2).Value IsNot Nothing Then
                        vCondition = row.Cells.Item(2).Value.ToString().Trim()
                    End If
                    If row.Cells.Item(3).Value IsNot Nothing Then
                        vValue = row.Cells.Item(3).Value.ToString().Trim()
                    End If

                    point = 5

                    Dim strCondition As String = ""

                    If Not (String.IsNullOrEmpty(vParameter) Or String.IsNullOrEmpty(vCondition) Or String.IsNullOrEmpty(vValue)) Then

                        strCondition = IIf(IsNumeric(vValue), $"({ vParameter } { vCondition } { vValue })", $"({ vParameter } { vCondition } '{ vValue }')")

                        If txtExpression.Text.Length > 0 Then txtExpression.Text &= $" { vOperator } "

                        txtExpression.Text &= strCondition
                    End If

                    If row.Index <> 0 Then
                        If row.Cells.Item(0).Value IsNot Nothing Then
                            row.Cells.Item(0).Style.BackColor = Color.White
                        Else
                            row.Cells.Item(0).Style.BackColor = Color.LightSalmon
                        End If
                    End If

                Next

                current_selected()
            Else
                txtExpression.Text = ""
            End If

            dgridConditions.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub DGV_Cond_newrow(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgridConditions.RowsAdded
        dgridConditions.Rows(dgridConditions.Rows.Count - 1).Height = 40
        If dgridConditions.Rows.Count > 1 Then
            dgridConditions.Rows(dgridConditions.Rows.Count - 1).Cells.Item(0).Style.BackColor = Color.LightSalmon
        End If
    End Sub

    Private Sub DGV_Cond_CellPainting(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgridConditions.RowPostPaint
        'Convert the image to icon, in order to load it in the row header column
        Dim myBitmap As New Bitmap(delete_image)
        Dim myIcon As Icon = Icon.FromHandle(myBitmap.GetHicon())

        Dim graphics As Graphics = e.Graphics

        'Set Image dimension - User's choice
        Dim iconHeight As Integer = 14
        Dim iconWidth As Integer = 14

        'Set x/y position - As the center of the RowHeaderCell
        Dim xPosition As Integer = e.RowBounds.X + (dgridConditions.RowHeadersWidth / 2)
        Dim yPosition As Integer = e.RowBounds.Y + ((dgridConditions.Rows(e.RowIndex).Height - iconHeight) / 2)

        Dim rectangle As New Rectangle(xPosition, yPosition, iconWidth, iconHeight)
        graphics.DrawIcon(myIcon, rectangle)



    End Sub

    Private Sub DGV_COND_RowHeaderMouseClick(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs) Handles dgridConditions.RowHeaderMouseClick

        dgridConditions.SelectionMode =
            DataGridViewSelectionMode.RowHeaderSelect
        dgridConditions.Rows(e.RowIndex).Selected = True

        Dim resp As MsgBoxResult = MsgBox("Delete condition ?", MsgBoxStyle.YesNo)
        If resp = MsgBoxResult.Yes Then
            If Not (dgridConditions.Rows(e.RowIndex).IsNewRow) Then dgridConditions.Rows.RemoveAt((e.RowIndex))
            If dgridConditions.Rows.Count = 1 Then
                txtExpression.Text = String.Empty
            Else
                'DGV_Cond.Rows.Remove(DGV_Cond.Rows(e.RowIndex))
            End If
            dgridConditions_Cellvaluechange(sender, Nothing)
        End If
    End Sub

    Private Sub TBL_LAYOUT_TOP_Paint(sender As Object, e As PaintEventArgs) Handles TableLayout_TOP.Paint

        Using p As New Pen(Color.FromArgb(217, 217, 217), 2.0)
            e.Graphics.DrawRectangle(p, 0, 0, TableLayout_TOP.Width, TableLayout_TOP.Height)
        End Using

        PB_CSIF_LOGO.Location = New Point((Panel_CSIF_logo.Width / 2) - (PB_CSIF_LOGO.Width / 2), PB_CSIF_LOGO.Location.Y)
        PB_MTC_LOGO.Location = New Point((Panel_CSIF_logo.Width - PB_MTC_LOGO.Width) / 2, PB_MTC_LOGO.Location.Y)
        pbFocas.Location = New Point((Panel_CSIF_logo.Width - PB_MTC_LOGO.Width) / 2, PB_MTC_LOGO.Location.Y)

        LB_MachineAddress.Location = New Point((Panel_CSIF_logo.Width - LB_MachineAddress.Width) / 2, LB_MachineAddress.Location.Y)
        lblMonitoringUnit.Location = New Point((Panel_CSIF_logo.Width - lblMonitoringUnit.Width) / 2, lblMonitoringUnit.Location.Y)

    End Sub

    Private Sub splitcontainer1_P1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint
        Using p As New Pen(Color.FromArgb(217, 217, 217), 2.0)
            e.Graphics.DrawRectangle(p, 0, 0, SplitContainer1.Panel1.Width, SplitContainer1.Panel1.Height)
        End Using

    End Sub

    Private Sub splitcontainer2_P1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer2.Panel1.Paint

        Using p As New Pen(Color.FromArgb(217, 217, 217), 1.0)
            e.Graphics.DrawRectangle(p, 0, 24, SplitContainer2.Panel1.Width - 1, SplitContainer2.Panel1.Height - 1 - 24)
        End Using

    End Sub

    Private Sub splitcontainer2_P2_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer2.Panel2.Paint
        Using p As New Pen(Color.FromArgb(217, 217, 217), 1.0)
            e.Graphics.DrawRectangle(p, 0, 0, SplitContainer2.Panel2.Width - 1, SplitContainer2.Panel2.Height - 1)

        End Using
        TB_tabs.Refresh()
    End Sub

    Private Sub tb_tabs_Paint(sender As Object, e As PaintEventArgs) Handles TB_tabs.Paint

        If TB_tabs.SelectedTab.Name = "page_status" Then
            Using p As New Pen(Color.FromArgb(217, 217, 217), 1.0)
                e.Graphics.DrawRectangle(p, LBL_man_edit.Location.X - 1, txtExpression.Location.Y - 4, dgridConditions.Width, 35)
            End Using
        End If
    End Sub

    Private Sub BTN_Refresh_Click(sender As Object, e As EventArgs) Handles BTN_Refresh.Click
        Dim dev As DeviceCollection = Devices
        If (dev.Count > 0) Then

            Dim device = dev.Where(Function(f) f.Name = MachineName).ToList
            If (device.Count = 1) Then
                If Not BG_filltree.IsBusy Then BG_filltree.RunWorkerAsync()
                ' LoadCondFromDB()
            ElseIf (device.Count = 0) Then
                ' MessageBox.Show("Machine name not found in device collection")
                '  LoadCondFromDB()
            Else
                'MessageBox.Show("More than 1 machine with the same name were found in device collection")
                ' LoadCondFromDB()
            End If
        Else
            ' MessageBox.Show("No device found from this IP")
            ' LoadCondFromDB()
        End If
    End Sub


    Private Sub current_selected()
        Try
            LB_current_selected_values.Items.Clear()

            Dim crt As ISample

            If dgridConditions.Rows.Count > 0 Then

                For Each row As DataGridViewRow In dgridConditions.Rows

                    Dim cellValue = dgridConditions.Rows.Item(row.Index).Cells.Item(1).Value

                    'If Not dgridConditions.Rows.Item(row.Index).Cells.Item(1).Value Is Nothing Then
                    If Not cellValue Is Nothing Then

                        'If Not dgridConditions.Rows.Item(row.Index).Cells.Item(1).Value Is DBNull.Value Then
                        If Not cellValue Is DBNull.Value Then

                            'Dim dgvCellValue = dgridConditions.Rows.Item(row.Index).Cells.Item(1).Value.ToString().Trim()
                            Dim dgvCellValue = cellValue.ToString().Trim()

                            If Current_stream IsNot Nothing Then

                                'crt = Current_stream.GetSample(dgridConditions.Rows.Item(row.Index).Cells.Item(1).Value)
                                crt = Current_stream.GetSample(cellValue)

                                If crt Is Nothing Then

                                    Dim Conds() As IDataElement = Current_stream.AllConditions

                                    For Each cond In Conds
                                        If cond.DataItemID = dgvCellValue Then

                                            LB_current_selected_values.Items.Add($"{ dgvCellValue } = { cond.Value.ToString().Trim() }")

                                            CSI_Lib.Update_current_selected(dgvCellValue, cond.Value, mch__)

                                        End If
                                    Next

                                    Dim Events() As IDataElement = Current_stream.AllEvents

                                    For Each event_ In Events
                                        If event_.DataItemID = dgvCellValue Then

                                            LB_current_selected_values.Items.Add($"{ dgvCellValue } = { event_.Value.ToString().Trim() }")

                                            CSI_Lib.Update_current_selected(dgvCellValue, event_.Value, mch__)

                                        End If
                                    Next

                                    Dim Samples() As IDataElement = Current_stream.AllSamples

                                    For Each sample In Samples
                                        If sample.DataItemID = dgvCellValue Then

                                            LB_current_selected_values.Items.Add($"{ dgvCellValue } = { sample.Value.ToString().Trim() }")

                                            CSI_Lib.Update_current_selected(dgvCellValue, sample.Value, mch__)

                                        End If
                                    Next

                                Else
                                    If Not dgvCellValue = Nothing Then

                                        LB_current_selected_values.Items.Add($"{dgvCellValue } = { If(crt Is Nothing, "", crt.Value.ToString().Trim()) }")

                                        CSI_Lib.Update_current_selected(dgvCellValue, crt.Value, mch__)

                                    End If

                                End If
                            End If
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            MsgBox("cannot fill from DGV: " & ex.Message)
        End Try
    End Sub


#Region "display status drawing"

    'https://www.youtube.com/watch?v=Z8z3hXGg2k4
    Private Sub Draw_text(text As String)

        Dim strString As String = text
        Dim newBitmap As Bitmap = PB_status.Image

        Dim g As Graphics = Graphics.FromImage(newBitmap)

        Dim font__ As New Font("Segoe UI", 18, FontStyle.Italic)
        Dim size As SizeF = g.MeasureString(text, font__)

        Dim AdjustedFont As Font = GetAdjustedFont(g, text, font__, PB_status.Width, 18, 10, True)

        size = g.MeasureString(text, AdjustedFont)
        g.DrawString(text, AdjustedFont, New SolidBrush(Color.White), New Point((PB_status.Width - size.Width) / 2 + 12, PB_status.Height / 2))
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

    End Sub

    ''' <summary>
    ''' 'https://msdn.microsoft.com/en-ca/library/bb986765.aspx
    ''' </summary>
    Public Function GetAdjustedFont(GraphicRef As Graphics, GraphicString As String, OriginalFont As Font, ContainerWidth As Integer, MaxFontSize As Integer, MinFontSize As Integer,
    SmallestOnFail As Boolean) As Font
        ' We utilize MeasureString which we get via a control instance           
        For AdjustedSize As Integer = MaxFontSize To MinFontSize Step -1
            Dim TestFont As New Font(OriginalFont.Name, AdjustedSize, OriginalFont.Style)

            ' Test the string with the new size
            Dim AdjustedSizeNew As SizeF = GraphicRef.MeasureString(GraphicString, TestFont)

            If ContainerWidth > Convert.ToInt32(AdjustedSizeNew.Width) Then
                ' Good font, return it
                Return TestFont
            End If
        Next

        ' If you get here there was no fontsize that worked
        ' return MinimumSize or Original?
        If SmallestOnFail Then
            Return New Font(OriginalFont.Name, MinFontSize, OriginalFont.Style)
        Else
            Return OriginalFont
        End If
    End Function

#End Region


    Private Shared Sub ResizeAutoSizeColumn(listView As ListView, autoSizeColumnIndex As Integer)

        If listView.View <> View.Details OrElse listView.Columns.Count <= 0 OrElse autoSizeColumnIndex < 0 Then
            Return
        End If
        If autoSizeColumnIndex >= listView.Columns.Count Then
            Throw New IndexOutOfRangeException("Parameter autoSizeColumnIndex is outside the range of column indices in the ListView.")
        End If

        ' Sum up the width of all columns except the auto-resizing one.
        Dim otherColumnsWidth As Integer = 0
        For Each header As ColumnHeader In listView.Columns
            If header.Index <> autoSizeColumnIndex Then
                otherColumnsWidth += header.Width
            End If
        Next

        ' Calculate the (possibly) new width of the auto-resizable column.
        Dim autoSizeColumnWidth As Integer = listView.ClientRectangle.Width - otherColumnsWidth

        ' Finally set the new width of the auto-resizing column, if it has changed.
        If listView.Columns(autoSizeColumnIndex).Width <> autoSizeColumnWidth Then
            listView.Columns(autoSizeColumnIndex).Width = autoSizeColumnWidth
        End If
    End Sub


#Region "Background worker"

    'runs 1 time a the load
    Private Sub BG_filltree_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BG_filltree.DoWork

        Current_stream = m_client.Current()

        Dim MUCurrentXml = MonitoringBoardsService.GetCurrentXml(monitoringUnitId)

        Current_stream.AddMonitoringUnits(MUCurrentXml)

    End Sub

    Private Sub BG_filltree_Compl(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BG_filltree.RunWorkerCompleted
        'old code ::: populate_TGV(Devices, MachineName)
        populate_TGV(Devices, MTCMachineName)
        'TV_MTC.Nodes.Clear()
        'TV_MTC.Nodes.Add(root)


        'TGV_MTC.Nodes.Clear()
        'TGV_MTC.Nodes.Add(root)

    End Sub

    'runs many times to refresh the tree
    Private Sub BW_refresh_tree_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW_refresh_tree.DoWork

        If Not BG_filltree.IsBusy Then
            Current_stream = m_client.Current()

            Dim MUCurrentXml = MonitoringBoardsService.GetCurrentXml(monitoringUnitId)

            Current_stream.AddMonitoringUnits(MUCurrentXml)
        End If

    End Sub

    Private Sub BW_refresh_tree_workcomplete(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW_refresh_tree.RunWorkerCompleted

        For Each node_ As TreeGridNode In TGV_MTC.Nodes
            refresh_TGV(node_, Devices, MTCMachineName)
        Next

        current_selected()

        If Not BW_refresh_Status.IsBusy Then BW_refresh_Status.RunWorkerAsync()

    End Sub

    '
    Private Sub Timer_threads_tic() Handles Timer_threads.Tick
        If Not BW_refresh_tree.IsBusy Then BW_refresh_tree.RunWorkerAsync()
    End Sub

    Dim status_result As String = ""

    Dim flag As Boolean = False

    Private Sub BW_Status_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW_refresh_Status.DoWork

        status_result = ""

        For Each status__ In Conditions_expression.Keys.ToList

            CSI_Lib.Evaluate_logic(Conditions_expression(status__), mch__)

            Give_result(status__)

        Next

        If status_result = "" Then

            status_result = "CYCLE OFF"
            mch__.current() = status_result

        Else

            mch__.current() = status_result
            status_result = mch__.current()

        End If

    End Sub

    Private Function Give_result(status__ As String) As String

        If mch__.rpn_result_intern_ IsNot Nothing Then

            If mch__.rpn_result_intern_.NumValue = 1 Then

                If status_result = "" Then
                    status_result = status__
                Else
                    status_result = "Conflicting stat"
                End If

            End If

        End If

        Return status_result

    End Function

    Private Sub BW_Status_work_completed(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW_refresh_Status.RunWorkerCompleted

        lbl_Result.Text = status_result

        If status_result = "CYCLE ON" Then
            PB_status.Image = My.Resources.green_ring

        ElseIf status_result = "CYCLE OFF" Then
            PB_status.Image = My.Resources.redring_med

        ElseIf status_result = "Conflicting stat" Then
            PB_status.Image = My.Resources.redring_med

        Else
            PB_status.Image = My.Resources.redring_med

        End If

        Draw_text(status_result)
        Timer_threads.Start()

    End Sub

#End Region


#Region "add/del/save/CB"

    ' Right click on the tree to add a condition
    Private Sub Addcond_Click(sender As Object, e As EventArgs) Handles Addcond.Click

        Try
            ' Code written by Drausio replacing the code commented below
            '
            If Not TB_tabs.SelectedTab.Name = "page_status" Then Return


            Dim selectedRow = dgridConditions.Rows.Count - 1

            If dgridConditions.SelectedCells.Count > 0 Then selectedRow = dgridConditions.SelectedCells(0).RowIndex

            If selectedRow = (dgridConditions.Rows.Count - 1) Then
                Dim values = New String() {"", TGV_MTC.CurrentNode.Tag.id, "=", TGV_MTC.CurrentNode.Cells.Item(1).Value}
                dgridConditions.Rows.Add(values)
            Else
                dgridConditions.Rows.Item(selectedRow).Cells.Item(1).Value = TGV_MTC.CurrentNode.Tag.id
                dgridConditions.Rows.Item(selectedRow).Cells.Item(2).Value = "="
                dgridConditions.Rows.Item(selectedRow).Cells.Item(3).Value = TGV_MTC.CurrentNode.Cells.Item(1).Value
            End If

            dgridConditions_Cellvaluechange(sender, Nothing)

        Catch ex As Exception

            Log.Error(ex)
            MsgBox(ex.Message)

        End Try
    End Sub


    Private Function CutStartEndValue(value As String, startCut As String, endCut As String) As String

        Dim endValue = value
        Dim idxCut As Integer = 0



        If String.IsNullOrEmpty(startCut) And String.IsNullOrEmpty(endCut) Then
            Return value
        End If

        If (Not String.IsNullOrEmpty(startCut) And Not endValue.Contains(startCut)) Or (Not String.IsNullOrEmpty(endCut) And Not endValue.Contains(endCut)) Then
            Return ""
        End If


        If Not String.IsNullOrEmpty(startCut) And endValue.Contains(startCut) Then
            idxCut = endValue.IndexOf(startCut) + startCut.Length
            endValue = endValue.Substring(idxCut)
        End If

        If Not String.IsNullOrEmpty(endCut) And endValue.Contains(endCut) Then
            idxCut = endValue.IndexOf(endCut)
            endValue = endValue.Substring(0, idxCut)
        End If

        Return endValue

    End Function


    Public Sub UpdateOtherSettings()

        Try
            'Dim sqlCmd As New StringBuilder()
            'sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings           ")
            'sqlCmd.Append($"   SET                                                ")
            'sqlCmd.Append($"      RapidOver_Var     = '{ RapidoverVariable }',    ")
            'sqlCmd.Append($"      SpindleOver_Var   = '{ spindleoverVariable }',  ")
            'sqlCmd.Append($"      FeedRateOver_Var  = '{ feedrateoverVariable }', ")
            'sqlCmd.Append($"      Part_Req_Var      = '{ partReqVar }',           ")
            'sqlCmd.Append($"      Part_Count_Var    = '{ partCountVar }',         ")
            'sqlCmd.Append($"      Prog_No_Var       = '{ ProgramVariable }',      ")
            'sqlCmd.Append($"      Prog_No_StartWith = '{ ProgNumStartWith }',     ")
            'sqlCmd.Append($"      Prog_No_EndWith   = '{ ProgNumEndsWith }',      ")
            'sqlCmd.Append($"      Part_No_Var       = '{ PartVariable }',         ")
            'sqlCmd.Append($"      Part_No_StartWith = '{ PartStartWith }',        ")
            'sqlCmd.Append($"      Part_No_EndWith   = '{ PartEndWith }'           ")
            'sqlCmd.Append($"   WHERE                                              ")
            'sqlCmd.Append($"      ConnectorId    =  { ConnectorId }        ;      ")
            'sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues     ")
            'sqlCmd.Append($"   SET                                                ")
            'sqlCmd.Append($"      RapidOver_Value    = '{ RapidoverValue }',      ")
            'sqlCmd.Append($"      SpindleOver_Value  = '{ spindleoverValue }',    ")
            'sqlCmd.Append($"      FeedRateOver_Value = '{ feedrateoverValue }',   ")
            'sqlCmd.Append($"      Part_Req_Value     = '{ partReqValue }',        ")
            'sqlCmd.Append($"      Part_Count_Value   = '{ partCountValue }',      ")
            'sqlCmd.Append($"      Prog_No_Value      = '{ ProgramValue }',        ")
            'sqlCmd.Append($"      Part_No_Value      = '{ PartValue }'            ")
            'sqlCmd.Append($"   WHERE                                              ")
            'sqlCmd.Append($"      ConnectorId    =  { ConnectorId }        ;      ")

            'MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Update Rapid Override Problem : " & ex.ToString())
            Log.Error("Database Update Rapid Override Problem.", ex)
        End Try

    End Sub

    Public Sub UpdateRapidoverDB(Machinename As String, IPaddress As String, RapidoverVariable As String, RapidoverValue As String)

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings      ")
            sqlCmd.Append($"   SET                                           ")
            sqlCmd.Append($"      Rapid_Variable = '{ RapidoverVariable }'   ")
            sqlCmd.Append($"   WHERE                                         ")
            sqlCmd.Append($"      ConnectorId    =  { ConnectorId }        ; ")
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues")
            sqlCmd.Append($"   SET                                           ")
            sqlCmd.Append($"      Rapid_Value    = '{ RapidoverValue }'      ")
            sqlCmd.Append($"   WHERE                                         ")
            sqlCmd.Append($"      ConnectorId    =  { ConnectorId }        ; ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Update Rapid Override Problem : " & ex.ToString())
            Log.Error("Database Update Rapid Override Problem.", ex)
        End Try

    End Sub

    Public Sub UpdateSpindleoverDB(Machinename As String, IPaddress As String, spindleoverVariable As String, spindleoverValue As String)

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings        ")
            sqlCmd.Append($"   SET                                             ")
            sqlCmd.Append($"      Spindle_Variable = '{ spindleoverVariable }' ")
            sqlCmd.Append($"   WHERE                                           ")
            sqlCmd.Append($"      ConnectorId      =  { ConnectorId }        ; ")
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues  ")
            sqlCmd.Append($"   SET                                             ")
            sqlCmd.Append($"      Spindle_Value    = '{ spindleoverValue }'    ")
            sqlCmd.Append($"   WHERE                                           ")
            sqlCmd.Append($"      ConnectorId      =  { ConnectorId }        ; ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Spindle Override Number Problem : " & ex.ToString())
            Log.Error("Database Update Spindle Override Problem.", ex)
        End Try

    End Sub

    Public Sub UpdateFeedrateoverDB(Machinename As String, IPaddress As String, feedrateoverVariable As String, feedrateoverValue As String)
        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings            ")
            sqlCmd.Append($"   SET                                                 ")
            sqlCmd.Append($"      FeedRate_Variable   = '{ feedrateoverVariable }' ")
            sqlCmd.Append($"   WHERE                                               ")
            sqlCmd.Append($"      ConnectorId    =  { ConnectorId }             ;  ")
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues      ")
            sqlCmd.Append($"   SET                                                 ")
            sqlCmd.Append($"      FeedRate_Value = '{ feedrateoverValue }'         ")
            sqlCmd.Append($"   WHERE                                               ")
            sqlCmd.Append($"      ConnectorId    =  { ConnectorId }                ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Update Feedrate Override Problem : " & ex.ToString())
            Log.Error("Database Update Feedrate Override Problem.", ex)
        End Try
    End Sub

    Public Sub UpdateRequiredPartDB(Machinename As String, IPaddress As String, partReqVar As String, partReqValue As Integer)
        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings       ")
            sqlCmd.Append($"   SET                                            ")
            sqlCmd.Append($"      PartRequired_Variable   = '{ partReqVar }'  ")
            sqlCmd.Append($"   WHERE                                          ")
            sqlCmd.Append($"      ConnectorId    =  { ConnectorId }  ;        ")
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues ")
            sqlCmd.Append($"   SET                                            ")
            sqlCmd.Append($"      PartRequired_Value =  { partReqValue }      ")
            sqlCmd.Append($"   WHERE                                          ")
            sqlCmd.Append($"      ConnectorId    =  { ConnectorId }           ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Update Part Required Problem : " & ex.ToString())
            Log.Error("Database Update Part Required Problem.", ex)
        End Try
    End Sub

    Public Sub UpdatePartCountDB(Machinename As String, IPaddress As String, partCountVar As String, partCountValue As Integer)
        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings       ")
            sqlCmd.Append($"   SET                                            ")
            sqlCmd.Append($"      PartCount_Variable   = '{ partCountVar }'   ")
            sqlCmd.Append($"   WHERE                                          ")
            sqlCmd.Append($"      ConnectorId      =  { ConnectorId }    ;    ")
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues ")
            sqlCmd.Append($"   SET                                            ")
            sqlCmd.Append($"      PartCount_Value =  { partCountValue }       ")
            sqlCmd.Append($"   WHERE                                          ")
            sqlCmd.Append($"      ConnectorId      =  { ConnectorId }    ;    ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Update Part Count Problem : " & ex.ToString())
            Log.Error("Database Update Part Count Problem.", ex)
        End Try
    End Sub

    Private Sub UpdateProgramNumberDB(ProgramVariable As String, ProgramValue As String, ProgNumStartWith As String, ProgNumEndsWith As String)
        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings       ")
            sqlCmd.Append($"   SET                                            ")
            sqlCmd.Append($"      ProgramNumber_Variable    = '{ ProgramVariable }',  ")
            sqlCmd.Append($"      ProgramNumber_FilterStart = '{ ProgNumStartWith }', ")
            sqlCmd.Append($"      ProgramNumber_FilterEnd   = '{ ProgNumEndsWith }'   ")
            sqlCmd.Append($"   WHERE                                          ")
            sqlCmd.Append($"      ConnectorId       =  { ConnectorId }      ; ")
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues ")
            sqlCmd.Append($"   SET                                            ")
            sqlCmd.Append($"      ProgramNumber_Value     = '{ ProgramValue }'      ")
            sqlCmd.Append($"   WHERE                                          ")
            sqlCmd.Append($"      ConnectorId       =  { ConnectorId }      ; ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Update Program Number Problem : " & ex.ToString())
            Log.Error("Database Update Program Number Problem.", ex)
        End Try
    End Sub

    Private Sub UpdatePartNumberDB(pnrVariable As String, pnrValue As String)

        Dim pnrPrefix1 = txtPNrPrefix1.Text
        Dim pnrFilter1Start = txtPNrFilter1Start.Text
        Dim pnrFilter1End = txtPNrFilter1End.Text

        Dim pnrPrefix2 = txtPNrPrefix2.Text
        Dim pnrFilter2Start = txtPNrFilter2Start.Text
        Dim pnrFilter2End = txtPNrFilter2End.Text

        Dim pnrPrefix3 = txtPNrPrefix3.Text
        Dim pnrFilter3Start = txtPNrFilter3Start.Text
        Dim pnrFilter3End = txtPNrFilter3End.Text

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings               ")
            sqlCmd.Append($"   SET                                                    ")
            sqlCmd.Append($"      PartNumber_Variable      = '{ pnrVariable }',       ")
            sqlCmd.Append($"      PartNumber_Prefix1       = '{ pnrPrefix1 }',        ")
            sqlCmd.Append($"      PartNumber_Filter1Start  = '{ pnrFilter1Start}',    ")
            sqlCmd.Append($"      PartNumber_Filter1End    = '{ pnrFilter1End }',     ")
            sqlCmd.Append($"      PartNumber_Filter2Apply  =  { chkFilter2.Checked }, ")
            sqlCmd.Append($"      PartNumber_Prefix2       = '{ pnrPrefix2 }',        ")
            sqlCmd.Append($"      PartNumber_Filter2Start  = '{ pnrFilter2Start}',    ")
            sqlCmd.Append($"      PartNumber_Filter2End    = '{ pnrFilter2End }',     ")
            sqlCmd.Append($"      PartNumber_Filter3Apply  =  { chkFilter3.Checked }, ")
            sqlCmd.Append($"      PartNumber_Prefix3       = '{ pnrPrefix3 }',        ")
            sqlCmd.Append($"      PartNumber_Filter3Start  = '{ pnrFilter3Start}',    ")
            sqlCmd.Append($"      PartNumber_Filter3End    = '{ pnrFilter3End }'      ")
            sqlCmd.Append($"   WHERE                                                  ")
            sqlCmd.Append($"      ConnectorId       =  { ConnectorId }   ;            ")
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues         ")
            sqlCmd.Append($"   SET                                                    ")
            sqlCmd.Append($"      PartNumber_Value     = '{ pnrValue }'               ")
            sqlCmd.Append($"   WHERE                                                  ")
            sqlCmd.Append($"      ConnectorId       =  { ConnectorId }   ;            ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Update PartNumber Problem : " & ex.ToString())
            Log.Error("Database Update PartNumber Problem.", ex)
        End Try
    End Sub

    Private Sub UpdateOperationDB()

        Dim operFilterStart = txtOperationStart.Text
        Dim operFilterEnd = txtOperationEnd.Text
        Dim operValue = txtOperationOutput.Text

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings               ")
            sqlCmd.Append($"   SET                                                    ")
            sqlCmd.Append($"      Operation_Available    =  { chkOperation.Checked }, ")
            sqlCmd.Append($"      Operation_FilterStart  = '{ operFilterStart }',     ")
            sqlCmd.Append($"      Operation_FilterEnd    = '{ operFilterEnd }'        ")
            sqlCmd.Append($"   WHERE                                                  ")
            sqlCmd.Append($"      ConnectorId       =  { ConnectorId }   ;            ")
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues         ")
            sqlCmd.Append($"   SET                                                    ")
            sqlCmd.Append($"      PartNumber_Value     = '{ operValue }'              ")
            sqlCmd.Append($"   WHERE                                                  ")
            sqlCmd.Append($"      ConnectorId       =  { ConnectorId }   ;            ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Update Operation Problem : " & ex.ToString())
            Log.Error("Database Update Operation Problem.", ex)
        End Try

    End Sub

    Private Sub UpdatePalletDB()
        Try
            Dim labelItems = lblActivePallet.Text.Split(":").Select(Function(m) m.Trim()).ToArray()
            Dim fieldName = labelItems(1)

            mch__.Pallet = fieldName
            mch__.Pallet_startwith = txtStartPallet.Text
            mch__.Pallet_endwith = txtEndPallet.Text
            mch__.Pallet_val = txtOutputPallet.Text

            Dim palletVariable = fieldName
            Dim palletValue = txtOutputPallet.Text
            Dim palletStartWith = txtStartPallet.Text
            Dim palletEndsWith = txtEndPallet.Text
            Dim palletToMU = chkSendPallet.Checked

            Dim warningPressure As Integer = 0
            Integer.TryParse(txtWarningPressure.Text, warningPressure)

            Dim criticalPressure As Integer = 0
            Integer.TryParse(txtCriticalPressure.Text, criticalPressure)

            Dim warningTemperature As Integer = 0
            Integer.TryParse(txtWarningTemperature.Text, warningTemperature)

            Dim criticalTemperature As Integer = 0
            Integer.TryParse(txtCriticalTemperature.Text, criticalTemperature)

            Dim delayScale = If(cmbMCSScale.SelectedIndex = 1, "min", "sec")

            Dim mcsDelay = If(delayScale = "sec", numMCSDelay.Value, numMCSDelay.Value * 60)

            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings              ")
            sqlCmd.Append($"   SET                                                   ")
            sqlCmd.Append($"      ActivePallet_Var       = '{ palletVariable }',     ")
            sqlCmd.Append($"      ActivePallet_StartWith = '{ palletStartWith }',    ")
            sqlCmd.Append($"      ActivePallet_EndWith   = '{ palletEndsWith }',     ")
            sqlCmd.Append($"      ActivePallet_ToMU      =  { palletToMU },          ")
            sqlCmd.Append($"      WarningPressure        =  { warningPressure },     ")
            sqlCmd.Append($"      CriticalPressure       =  { criticalPressure },    ")
            sqlCmd.Append($"      WarningTemperature     =  { warningTemperature },  ")
            sqlCmd.Append($"      CriticalTemperature    =  { criticalTemperature }, ")
            sqlCmd.Append($"      MCSDelay               =  { mcsDelay },            ")
            sqlCmd.Append($"      DelayScale             = '{ delayScale }'          ")
            sqlCmd.Append($"   WHERE                                                 ")
            sqlCmd.Append($"      ConnectorId            =  { ConnectorId }        ; ")
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettingsvalues        ")
            sqlCmd.Append($"   SET                                                   ")
            sqlCmd.Append($"      ActivePallet_Value     = '{ palletValue }'         ")
            sqlCmd.Append($"   WHERE                                                 ")
            sqlCmd.Append($"      ConnectorId            =  { ConnectorId }        ; ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Update Program Number Problem : " & ex.ToString())
            Log.Error("Database Update Program Number Problem.", ex)
        End Try
    End Sub

    'Associate with partno
    Private Sub Partno_Click(sender As Object, e As EventArgs) Handles Partno.Click

        Try
            'Assign Part Number Variable to Label 
            lblPartNumber.Text = "Partnumber : " + TGV_MTC.CurrentNode.Tag.id

            ' Adds value to the textbox 
            txtInputPartNumber.Text = TGV_MTC.CurrentNode.Cells.Item(1).Value
            txtPNrFilter1Start.Text = String.Empty
            txtPNrFilter1End.Text = String.Empty

            txtPNrOutput.Text = CutStartEndValue(txtInputPartNumber.Text, txtPNrFilter1Start.Text, txtPNrFilter1End.Text)

            UpdatePartNumberDB(TGV_MTC.CurrentNode.Tag.id, txtPNrOutput.Text)

            mch__.PartNumber_Variable = TGV_MTC.CurrentNode.Tag.id

            mch__.PartNumber_Prefix1 = txtPNrPrefix1.Text
            mch__.PartNumber_Filter1Start = txtPNrFilter1Start.Text
            mch__.PartNumber_Filter1End = txtPNrFilter1End.Text

            mch__.PartNumber_Prefix2 = txtPNrPrefix2.Text
            mch__.PartNumber_Filter2Start = txtPNrFilter2Start.Text
            mch__.PartNumber_Filter2End = txtPNrFilter2End.Text

            mch__.PartNumber_Prefix3 = txtPNrPrefix3.Text
            mch__.PartNumber_Filter3Start = txtPNrFilter3Start.Text
            mch__.PartNumber_Filter3End = txtPNrFilter3End.Text

            If txtPNrOutput.Text = "NOT AVAILABLE" Then
                mch__.PartNumber_Value = txtInputPartNumber.Text
            Else
                mch__.PartNumber_Value = txtPNrOutput.Text
            End If

            SetupForm2.RefreshAllDevices()

        Catch ex As Exception
            MsgBox(ex.Message)
            Log.Error(ex)
        End Try

    End Sub

    Private Sub SubInputPartNumberChange(sender As Object, e As EventArgs) Handles txtInputPartNumber.TextChanged

        SubPartNumberChange(sender, e)
        SubOperationChange(sender, e)

    End Sub

    Private Sub SubPartNumberChange(sender As Object, e As EventArgs) Handles txtPNrPrefix1.TextChanged,
                                                                              txtPNrFilter1Start.TextChanged,
                                                                              txtPNrFilter1End.TextChanged,
                                                                              txtPNrPrefix2.TextChanged,
                                                                              txtPNrFilter2Start.TextChanged,
                                                                              txtPNrFilter2End.TextChanged,
                                                                              txtPNrPrefix3.TextChanged,
                                                                              txtPNrFilter3Start.TextChanged,
                                                                              txtPNrFilter3End.TextChanged

        Try
            Dim partNumberFilter1 = ""
            Dim partNumberFilter2 = ""
            Dim partNumberFilter3 = ""

            partNumberFilter1 = $"{txtPNrPrefix1.Text.Trim()}{CutStartEndValue(txtInputPartNumber.Text, txtPNrFilter1Start.Text, txtPNrFilter1End.Text)}"

            If chkFilter2.Checked Then
                partNumberFilter2 = $"{txtPNrPrefix2.Text.Trim()}{CutStartEndValue(txtInputPartNumber.Text, txtPNrFilter2Start.Text, txtPNrFilter2End.Text)}"
            End If

            If chkFilter3.Checked Then
                partNumberFilter3 = $"{txtPNrPrefix3.Text.Trim()}{CutStartEndValue(txtInputPartNumber.Text, txtPNrFilter3Start.Text, txtPNrFilter3End.Text)}"
            End If

            txtPNrOutput.Text = $"{partNumberFilter1}{partNumberFilter2}{partNumberFilter3}"

            Dim labelItems = lblPartNumber.Text.Split(":").Select(Function(m) m.Trim()).ToArray()
            Dim fieldName = labelItems(1)

            UpdatePartNumberDB(fieldName, txtPNrOutput.Text)

            mch__.PartNumber_Variable = fieldName

            mch__.PartNumber_Prefix1 = txtPNrPrefix1.Text
            mch__.PartNumber_Filter1Start = txtPNrFilter1Start.Text
            mch__.PartNumber_Filter1End = txtPNrFilter1End.Text

            mch__.PartNumber_Filter2Apply = chkFilter2.Checked
            mch__.PartNumber_Prefix2 = txtPNrPrefix2.Text
            mch__.PartNumber_Filter2Start = txtPNrFilter2Start.Text
            mch__.PartNumber_Filter2End = txtPNrFilter2End.Text

            mch__.PartNumber_Filter3Apply = chkFilter3.Checked
            mch__.PartNumber_Prefix3 = txtPNrPrefix3.Text
            mch__.PartNumber_Filter3Start = txtPNrFilter3Start.Text
            mch__.PartNumber_Filter3End = txtPNrFilter3End.Text

            mch__.PartNumber_Value = txtPNrOutput.Text

        Catch ex As Exception
            MsgBox(ex.Message)
            Log.Error(ex)
        End Try

    End Sub

    Private Sub SubOperationChange(sender As Object, e As EventArgs) Handles chkOperation.CheckedChanged, txtOperationStart.TextChanged, txtOperationEnd.TextChanged

        Dim operationOutput = CutStartEndValue(txtInputPartNumber.Text, txtOperationStart.Text, txtOperationEnd.Text)

        txtOperationOutput.Text = operationOutput
        mch__.Operation_Available = chkOperation.Checked
        mch__.Operation_FilterStart = txtOperationStart.Text
        mch__.Operation_FilterEnd = txtOperationEnd.Text
        mch__.Operation_Value = operationOutput

        UpdateOperationDB()

    End Sub

    Private Sub SubProgrNumberChange(sender As Object, e As EventArgs) Handles txtStartProgNumber.TextChanged, txtInputProgrNumber.TextChanged, txtEndProgNumber.TextChanged

        Try
            txtOutputProgrNumber.Text = CutStartEndValue(txtInputProgrNumber.Text, txtStartProgNumber.Text, txtEndProgNumber.Text)

            Dim labelItems = lblProgramNumber.Text.Split(":").Select(Function(m) m.Trim()).ToArray()
            Dim fieldName = labelItems(1)

            UpdateProgramNumberDB(fieldName, txtOutputProgrNumber.Text, txtStartProgNumber.Text, txtEndProgNumber.Text)

            mch__.progno = fieldName
            mch__.PRN_startwith = txtStartProgNumber.Text
            mch__.PRN_endwith = txtEndProgNumber.Text
            mch__.progno_val = txtOutputProgrNumber.Text

        Catch ex As Exception
            MsgBox(ex.Message)
            Log.Error(ex)
        End Try

    End Sub

    Private Sub SubPalletChange(sender As Object, e As EventArgs) Handles txtStartPallet.TextChanged, txtInputPallet.TextChanged, txtEndPallet.TextChanged, txtWarningPressure.TextChanged, txtWarningTemperature.TextChanged, txtCriticalTemperature.TextChanged, txtCriticalPressure.TextChanged, numMCSDelay.ValueChanged, cmbMCSScale.SelectedIndexChanged

        Try
            txtOutputPallet.Text = CutStartEndValue(txtInputPallet.Text, txtStartPallet.Text, txtEndPallet.Text)

            UpdatePalletDB()

        Catch ex As Exception
            MsgBox(ex.Message)
            Log.Error(ex)
        End Try

    End Sub

    'Private Sub Partno_TB_endwith(sender As Object, e As EventArgs) Handles txtEndPartNumber.LostFocus

    '    Try
    '        FilterParrtNumber(txtInputPartNumber.Text)
    '        Dim mainstr = (lblPartNumber.Text).Replace(" ", "")
    '        Dim len_mainstr = mainstr.Length
    '        Dim index_var = mainstr.IndexOf(":")
    '        Dim sizechar = index_var + 1
    '        Dim substr = mainstr.Substring(sizechar, len_mainstr - sizechar)
    '        UpdatePartNumberDB(MachineName, If(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress), substr, txtOutputPartNumber.Text, txtStartPartNumber.Text, txtEndPartNumber.Text)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        Log.Error(ex)
    '    End Try

    'End Sub

    'Private Sub Sub_TB_PNumEndswith(sender As Object, e As EventArgs) Handles txtEndProgNumber.LostFocus

    '    Try
    '        FilterProgramNumber(txtInputProgrNumber.Text)
    '        Dim mainstr = (lblProgramNumber.Text).Replace(" ", "")
    '        Dim len_mainstr = mainstr.Length
    '        Dim index_var = mainstr.IndexOf(":")
    '        Dim sizechar = index_var + 1
    '        Dim substr = mainstr.Substring(sizechar, len_mainstr - sizechar)
    '        UpdateProgramNumberDB(MachineName, If(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress), substr, txtOutputProgrNumber.Text, txtStartProgNumber.Text, txtEndProgNumber.Text)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        Log.Error(ex)
    '    End Try

    'End Sub

    Private Sub Feedrate_Click(sender As Object, e As EventArgs) Handles Feedrate.Click
        Try
            lblFovr.Text = "Feedrate Override : " + TGV_MTC.CurrentNode.Tag.id
            UpdateFeedrateoverDB(MachineName, If(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress), TGV_MTC.CurrentNode.Tag.id, TGV_MTC.CurrentNode.Cells.Item(1).Value.ToString())
            mch__.FeedRate_Variable = TGV_MTC.CurrentNode.Tag.id
            SetupForm2.RefreshAllDevices()
        Catch ex As Exception
            MsgBox(ex.Message)
            Log.Error(ex)
        End Try
    End Sub

    Private Sub Rapid_Click(sender As Object, e As EventArgs) Handles Rapid.Click
        Try
            lblRovr.Text = "Rapid Override : " + TGV_MTC.CurrentNode.Tag.id
            UpdateRapidoverDB(MachineName, If(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress), TGV_MTC.CurrentNode.Tag.id, TGV_MTC.CurrentNode.Cells.Item(1).Value.ToString())
            mch__.Rapid_Variable = TGV_MTC.CurrentNode.Tag.id
            'CSI_Lib.MCHS_(MachineName).rapidov = TGV_MTC.CurrentNode.Tag.id
            SetupForm2.RefreshAllDevices()
        Catch ex As Exception
            MsgBox(ex.Message)
            Log.Error(ex)
        End Try
    End Sub

    Private Sub Spindle_Click(sender As Object, e As EventArgs) Handles Spindle.Click
        Try
            lblSovr.Text = "Spindle Override : " + TGV_MTC.CurrentNode.Tag.id
            UpdateSpindleoverDB(MachineName, If(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress), TGV_MTC.CurrentNode.Tag.id, TGV_MTC.CurrentNode.Cells.Item(1).Value.ToString())
            mch__.Spindle_Variable = TGV_MTC.CurrentNode.Tag.id
            SetupForm2.RefreshAllDevices()
        Catch ex As Exception
            MsgBox(ex.Message)
            Log.Error(ex)
        End Try
    End Sub

    Private Sub PartCountMenuItem_Click(sender As Object, e As EventArgs) Handles PartCountMenuItem.Click
        Try
            lblPartsCount.Text = "Part Count : " + TGV_MTC.CurrentNode.Tag.id

            Dim partCount As Integer = 0

            Integer.TryParse(TGV_MTC.CurrentNode.Cells.Item(1).Value.ToString(), partCount)

            UpdatePartCountDB(MachineName, If(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress), TGV_MTC.CurrentNode.Tag.id, partCount)

            mch__.PartCount_Variable = TGV_MTC.CurrentNode.Tag.id
            SetupForm2.RefreshAllDevices()

        Catch ex As Exception
            MsgBox(ex.Message)
            Log.Error(ex)
        End Try
    End Sub

    Private Sub RequiredPartsMenuItem_Click(sender As Object, e As EventArgs) Handles RequiredPartsMenuItem.Click
        Try
            lblRequiredParts.Text = "Required Parts : " + TGV_MTC.CurrentNode.Tag.id

            Dim reqParts As Integer = 0

            Integer.TryParse(TGV_MTC.CurrentNode.Cells.Item(1).Value.ToString(), reqParts)

            UpdateRequiredPartDB(MachineName, If(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress), TGV_MTC.CurrentNode.Tag.id, reqParts)

            mch__.RequiredParts_Variable = TGV_MTC.CurrentNode.Tag.id
            SetupForm2.RefreshAllDevices()

        Catch ex As Exception
            MsgBox(ex.Message)
            Log.Error(ex)
        End Try
    End Sub


    Private Sub Programno_Click(sender As Object, e As EventArgs) Handles Programno.Click
        Try
            lblProgramNumber.Text = "Program No : " + TGV_MTC.CurrentNode.Tag.id
            txtInputProgrNumber.Text = TGV_MTC.CurrentNode.Cells.Item(1).Value
            txtStartProgNumber.Text = String.Empty
            txtEndProgNumber.Text = String.Empty

            txtOutputProgrNumber.Text = CutStartEndValue(txtInputProgrNumber.Text, txtStartProgNumber.Text, txtEndProgNumber.Text)

            UpdateProgramNumberDB(TGV_MTC.CurrentNode.Tag.id, TGV_MTC.CurrentNode.Cells.Item(1).Value.ToString(), txtStartProgNumber.Text, txtEndProgNumber.Text)

            mch__.progno = TGV_MTC.CurrentNode.Tag.id
            mch__.PRN_startwith = txtStartProgNumber.Text
            mch__.PRN_endwith = txtEndProgNumber.Text
            mch__.progno_val = txtOutputProgrNumber.Text

            SetupForm2.RefreshAllDevices()

        Catch ex As Exception
            MsgBox(ex.Message)
            CSI_Lib.LogServerError(ex.Message, 1)
        End Try
    End Sub


    Private Sub PalletMenuItem_Click(sender As Object, e As EventArgs) Handles PalletMenuItem.Click
        Try
            lblActivePallet.Text = $"Active Pallet: { TGV_MTC.CurrentNode.Tag.id }"
            txtInputPallet.Text = TGV_MTC.CurrentNode.Cells.Item(1).Value
            txtStartPallet.Text = String.Empty
            txtEndPallet.Text = String.Empty
            chkSendPallet.Checked = True

            txtOutputPallet.Text = CutStartEndValue(txtInputPallet.Text, txtStartPallet.Text, txtEndPallet.Text)

            'UpdatePalletDB(TGV_MTC.CurrentNode.Tag.id, TGV_MTC.CurrentNode.Cells.Item(1).Value.ToString(), txtStartPallet.Text, txtEndPallet.Text, chkSendPallet.Checked)
            UpdatePalletDB()

            mch__.Pallet = TGV_MTC.CurrentNode.Tag.id
            mch__.Pallet_startwith = txtStartPallet.Text
            mch__.Pallet_endwith = txtEndPallet.Text
            mch__.Pallet_val = txtOutputPallet.Text

            SetupForm2.RefreshAllDevices()

        Catch ex As Exception

        End Try
    End Sub



    Private Sub txtExpression_TextChanged(sender As Object, e As EventArgs)

        If Not String.IsNullOrEmpty(txtExpression.Text) And Conditions_expression IsNot Nothing And cmbStatus.SelectedItem IsNot Nothing Then

            If Conditions_expression.ContainsKey(cmbStatus.SelectedItem) Then
                Conditions_expression.Item(cmbStatus.SelectedItem) = txtExpression.Text
            Else
                Conditions_expression.Add(cmbStatus.SelectedItem, txtExpression.Text)
            End If
            'SaveCurrentConditionToDB()

            If Not isLoading Then hasChanges = True

        End If

    End Sub

    Dim defConditionText = "Insert New Status"

    Private Sub Add_Click(sender As Object, e As EventArgs) Handles Add.Click

        If Not String.IsNullOrEmpty(cmbStatus.Text) And cmbStatus.Text <> defConditionText Then

            If Add.Text = "Save" Then

                Dim newCmd = cmbStatus.Text
                Dim enetMachine = eNetServer.Machines.FirstOrDefault(Function(m) m.MachineId = MachineId)
                If Not enetMachine.Cmd_Others.ContainsKey(newCmd) Then
                    If Not MessageBox.Show($"This event does not exist in eNETDNC for machine {MachineName} ({enetMachine.EnetPos}). It is necessary to create it in eNETDNC and then restart the CSIFLEX Server Service!", "Event doesn't exist", MessageBoxButtons.OKCancel) = DialogResult.OK Then

                        Add.Text = "Add"
                        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
                        dgridConditions.Enabled = True
                        cmbStatus.Items.RemoveAt(cmbStatus.SelectedIndex)
                        cmbStatus.SelectedIndex = 0
                        Return
                    End If
                End If

                cmbStatus.Items.Insert(cmbStatus.Items.Count - 1, cmbStatus.Text)
                cmbStatus.Items.RemoveAt(cmbStatus.Items.Count - 1)
                Add.Text = "Add"
                Conditions_expression.Add(cmbStatus.Text, "") 'Status , Condition 
                cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
                previous_index = cmbStatus.Items.Count - 1
                cmbStatus.SelectedIndex = cmbStatus.Items.Count - 1
                dgridConditions.Enabled = True
                txtExpression.Text = String.Empty

            Else
                'Add Means Adding New Status Name in CB_Status Collection
                'This means Add Button Value = "Add"
                If hasChanges And Not isLoading And Conditions_expression.Count <> 0 Then

                    Dim resu As MsgBoxResult = MsgBox("save ? ", MsgBoxStyle.YesNo, "Modofication have been made")

                    If resu = MsgBoxResult.Yes Then
                        save_setup()
                        hasChanges = False
                    End If

                End If

                cmbStatus.DropDownStyle = ComboBoxStyle.DropDown
                cmbStatus.Items.Add(defConditionText)
                cmbStatus.SelectAll()
                cmbStatus.SelectedIndex = cmbStatus.Items.Count - 1
                cmbStatus.Focus()
                txtExpression.Text = String.Empty
                Add.Text = "Save"
                dgridConditions.Rows.Clear()
                dgridConditions.Enabled = False

                isStatusDisabled = False
                lblStatusDisabled.Visible = False
                btnDisableStatus.Text = "Disable Status"
            End If
        End If

    End Sub

    Private Sub BTN_del_Click(sender As Object, e As EventArgs) Handles BTN_del.Click

        If cmbStatus.SelectedIndex <> -1 Then

            If cmbStatus.SelectedIndex = 0 Then
                MessageBox.Show("You must have one status for this machine !")
            Else

                If Conditions_expression.ContainsKey(cmbStatus.SelectedItem) Then Conditions_expression.Remove(cmbStatus.SelectedItem)

                MySqlAccess.ExecuteNonQuery($"DELETE FROM csi_auth.tbl_mtcfocasconditions WHERE ConnectorId = { ConnectorId } and Status = '{ cmbStatus.SelectedItem }';")

                Dim deleted_status As String = cmbStatus.SelectedItem
                Dim deleted_index As Integer = cmbStatus.SelectedIndex

                cmbStatus.Items.RemoveAt(cmbStatus.SelectedIndex)
                txtExpression.Clear()
                dgridConditions.Rows.Clear()
                save_setup(deleted_status)
                hasChanges = False
                Add.Text = "Add"
                cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
                cmbStatus.SelectedIndex = If(deleted_index = 0, 0, deleted_index - 1)
                readsetup()
            End If

        End If
    End Sub

    Private Sub BTN_save_Click(sender As Object, e As EventArgs)
        save_setup()
        hasChanges = False
    End Sub

    Dim previous_index As Integer = 0


    Private Sub Adv_MTC_cond_edit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If hasChanges And Not isLoading Then
            Dim resu As MsgBoxResult = MsgBox("save ? ", MsgBoxStyle.YesNo, "Modofication have been made")
            If resu = MsgBoxResult.Yes Then
                save_setup()
            End If
        End If

    End Sub


    Private Sub cmbStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatus.SelectedIndexChanged

        selectedStatus = cmbStatus.SelectedItem

    End Sub


    Private Sub cmbStatus_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbStatus.SelectionChangeCommitted

        If hasChanges And Not isLoading And Conditions_expression.Count <> 0 Then
            Dim resu As MsgBoxResult = MsgBox("save ? ", MsgBoxStyle.YesNo, "Modification have been made")
            If resu = MsgBoxResult.Yes Then
                save_setup()
            End If
        End If

        If cmbStatus.SelectedItem <> "" And previous_index <> cmbStatus.SelectedIndex Then
            readsetup()
        End If

        previous_index = cmbStatus.SelectedIndex

        chkCsdOnSetup.Visible = (cmbStatus.Text = "CYCLE START DISABLE")
        btnDisableStatus.Visible = (cmbStatus.Text = "CYCLE START DISABLE")
        lblStatusDisabled.Visible = isStatusDisabled

    End Sub

    Private Sub nudDelay_ValueChanged(sender As Object, e As EventArgs) Handles nudDelay.ValueChanged

        Dim delay = nudDelay.Value
        Dim status = cmbStatus.SelectedItem
        Dim cmd = $"UPDATE IGNORE csi_auth.tbl_mtcfocasconditions SET delay = '{ delay }' WHERE ConnectorId = { ConnectorId } And Status = '{ selectedStatus }';"

        If Not isLoading Then hasChanges = True

        'Try
        '    MySqlAccess.ExecuteNonQuery(cmd)

        '    mch__.delays(status) = delay

        'Catch ex As Exception
        '    Log.Error(ex)
        'End Try

    End Sub

    Private Sub PB_CSIF_LOGO_Click(sender As Object, e As EventArgs) Handles PB_CSIF_LOGO.Click
        'PB_CSIF_LOGO.Image = My.Resources.CSIFLEX_lightout_LOGOTR
        PB_CSIF_LOGO.Image = My.Resources.Resources.CSIFLEXLOGOTR2
    End Sub

    Private Sub BTN_notif_Click(sender As Object, e As EventArgs)
        adv_mtc_Notifications.ShowDialog()
        adv_mtc_Notifications.eNetname = eNetName
        adv_mtc_Notifications.MTCMachinename = MachineName
    End Sub


#Region "Notifications"

    Dim Current_conditions As New List(Of ICondition)
    Dim list_of_Status As New List(Of String)

    Private Sub BW_find_cond_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW_find_cond.DoWork
        Try
            Dim device_index As Integer = 0

            Dim m_client As EntityClient = New EntityClient(MachineIP)
            Dim Current_stream As DataStream = m_client.Current()
            Dim Current_device As DeviceStream

            Dim Devices = m_client.Probe()
            Dim dev As DeviceCollection = Devices
            Current_conditions.Clear()

            For Each device In Devices
                If (MachineName = device.Name) Then
                    Current_device = Current_stream.DeviceStreams(device_index)
                    Dim current_component_stream() As ComponentStream = Current_device.ComponentStreams
                    For Each Component As ComponentStream In current_component_stream
                        For Each condition As ICondition In Component.Conditions
                            Current_conditions.Add(condition)
                        Next
                    Next
                End If
                device_index += 1
            Next
        Catch ex As Exception
            CSI_Lib.LogServerError(ex.Message, 1)
        End Try

    End Sub

    Private Sub BW_find_cond_completed(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW_find_cond.RunWorkerCompleted

        PB_processing_notif.Visible = False
        'LBL_RETR_notif.Visible = False
        Dim LVI As ListViewItem = New ListViewItem
        LVI.Text = "IDs"

        Dim list_of_cond As New List(Of String)

        For Each item In Current_conditions
            list_of_cond.Add(item.DataItemID + " (" + item.Type + ")")
        Next

        'merge
        Dim cond_column As New List(Of String)
        'DGV_COND_notif.Rows.Clear()
        If DGV_COND_notif IsNot Nothing Then
            For Each row As DataGridViewRow In DGV_COND_notif.Rows
                If row.Cells(0).Value IsNot Nothing Then cond_column.Add(row.Cells(0).Value)
            Next

            For Each status As String In list_of_cond
                If Not cond_column.Contains(status) Then
                    If status <> "" Then 'And cond_column.Count <> 0 Then
                        DGV_COND_notif.Rows.Add(status, False, False, 0)
                        'DGV_COND_notif.Rows(DGV_COND_notif.Rows.Count - 1).Tag = get_tag_(status, Current_conditions)
                    End If
                End If
            Next
        End If
    End Sub

    Private Function get_tag_(cond_id As String, cond_list As List(Of ICondition)) As ICondition
        For Each cond_ As ICondition In cond_list
            If cond_.DataItemID = cond_id Then Return cond_
        Next
        Return Nothing
    End Function

    Private tbl_notif_status As New DataTable
    Private tbl_notif_cond As New DataTable

    Private Sub save_setup_notif()

        Return

        Try
            'save the status datagridview   
            Dim table_ As New DataTable("notif_stat")
            For Each col As DataGridViewColumn In dgviewNotificationStatus.Columns
                table_.Columns.Add(col.HeaderText)
            Next

            For Each row As DataGridViewRow In dgviewNotificationStatus.Rows
                'saving only the active satuses notification
                Dim isActive As Boolean = row.Cells(0).Value
                If isActive Then
                    Dim tablerow As DataRow = table_.NewRow
                    For Each cell As DataGridViewCell In row.Cells
                        tablerow(cell.ColumnIndex) = If(cell.Value Is Nothing, "", cell.EditedFormattedValue)
                    Next
                    table_.Rows.Add(tablerow)
                End If
            Next
            ' Create a file name to write to.
            If File.Exists(home_path & "\sys\Conditions\" & eNetName & "\" & "notif_stat.xml") Then
                File.Delete(home_path & "\sys\Conditions\" & eNetName & "\" & "notif_stat.xml")
            End If
            ' Create the FileStream to write with.
            Dim stream As New System.IO.FileStream _
                (home_path & "\sys\Conditions\" & eNetName & "\" & "notif_stat.xml", System.IO.FileMode.CreateNew)
            table_.WriteXml(stream)
            stream.Close()
            'save the cond datagridview   
            table_ = New DataTable("notif_cond")
            For Each col As DataGridViewColumn In DGV_COND_notif.Columns
                table_.Columns.Add(col.HeaderText)
            Next
            For Each row As DataGridViewRow In DGV_COND_notif.Rows
                If Not (row.Cells(1).Value = False And row.Cells((2)).Value = False) Then
                    Dim tablerow As DataRow = table_.NewRow

                    For Each cell As DataGridViewCell In row.Cells
                        tablerow(cell.ColumnIndex) = If(cell.Value Is Nothing, "", cell.EditedFormattedValue)
                    Next
                    table_.Rows.Add(tablerow)
                End If
            Next
            ' Create a file name to write to.
            If File.Exists(home_path & "\sys\Conditions\" & eNetName & "\" & "notif_cond.xml") Then
                File.Delete(home_path & "\sys\Conditions\" & eNetName & "\" & "notif_cond.xml")
            End If
            ' Create the FileStream to write with.
            stream = New System.IO.FileStream _
                (home_path & "\sys\Conditions\" & eNetName & "\" & "notif_cond.xml", System.IO.FileMode.CreateNew)

            table_.WriteXml(stream)
            stream.Close()
        Catch ex As Exception
            MsgBox("Unable to save the notification setup : " + ex.Message)
            CSI_Lib.LogServerError(ex.Message, 1)
        End Try

    End Sub

    Private Sub read_setup_notif()
        ' Check the available status from the setup config files (to chck if new status available)
        list_of_Status = read_status()
        dgviewNotificationStatus.Rows.Clear()
        DGV_COND_notif.Rows.Clear()
        For Each sttus In list_of_Status
            dgviewNotificationStatus.Rows.Add(False, sttus, 0)
            DGV_COND_notif.Rows.Add(sttus, False, False, Nothing)
        Next
        If File.Exists(home_path & "\sys\Conditions\" & eNetName & "\" & "notif_stat.xml") Then
            Dim xmlFile As XmlReader
            xmlFile = XmlReader.Create(home_path & "\sys\Conditions\" & eNetName & "\" & "notif_stat.xml")
            tbl_notif_status.Clear()
            tbl_notif_status = New DataTable("notif_stat")
            For Each col As DataGridViewColumn In dgviewNotificationStatus.Columns
                tbl_notif_status.Columns.Add(col.HeaderText)
            Next
            tbl_notif_status.ReadXml(xmlFile)
            xmlFile.Close()
            For Each row As DataRow In tbl_notif_status.Rows
                If row(1) <> "" And row(0) = True Then
                    For Each stat_row As DataGridViewRow In dgviewNotificationStatus.Rows
                        If row(1) = stat_row.Cells(1).Value Then
                            stat_row.Cells(0).Value = row(0)
                            stat_row.Cells(2).Value = row(2)
                        End If
                    Next
                End If
            Next
        End If

        If File.Exists(home_path & "\sys\Conditions\" & eNetName & "\" & "notif_cond.xml") Then
            Dim xmlFile As XmlReader
            xmlFile = XmlReader.Create(home_path & "\sys\Conditions\" & eNetName & "\" & "notif_cond.xml")

            Dim tbl_notif_COND As New DataTable("notif_cond")
            tbl_notif_COND.Clear()


            For Each col As DataGridViewColumn In DGV_COND_notif.Columns
                tbl_notif_COND.Columns.Add(col.HeaderText)
            Next
            tbl_notif_COND.ReadXml(xmlFile)
            xmlFile.Close()


            For Each row As DataRow In tbl_notif_COND.Rows
                If row(0) <> "" = True Then
                    For Each cond_row As DataGridViewRow In DGV_COND_notif.Rows
                        If row(0) = cond_row.Cells(0).Value Then
                            cond_row.Cells(1).Value = row(1)
                            cond_row.Cells(2).Value = row(2)
                            cond_row.Cells(3).Value = row(3)
                        End If
                    Next
                End If
            Next
        End If

    End Sub

    Private Function read_status() As List(Of String)

        Dim list_of_Status As New List(Of String)

        Try

            Dim tblStatus As DataTable = MySqlAccess.GetDataTable($"SELECT distinct Status from csi_auth.tbl_mtcfocasconditions WHERE ConnectorId = { ConnectorId }")

            For Each row As DataRow In tblStatus.Rows
                list_of_Status.Add(row("Status").ToString())
            Next
        Catch ex As Exception
            MsgBox("Error while retrieving the machine status :  " + ex.Message)
            Log.Error(ex)
        End Try

        Return list_of_Status

    End Function

    Private Sub DGV_COND_notif_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_COND_notif.CellContentClick
        Try
            Dim tag
            Dim value__ As String
            For Each node_ As TreeGridNode In TGV_MTC.Nodes
                If DGV_COND_notif.Rows(e.RowIndex).Cells(0).Value IsNot Nothing Then
                    selectnode(node_, DGV_COND_notif.Rows(e.RowIndex).Cells(0).Value, tag, value__)
                End If
                If Not tag Is Nothing Then Exit For
            Next


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub selectnode(ByRef node_ As TreeGridNode, ID As String, ByRef tag As Object, ByRef value__ As String)

        For Each node__ As TreeGridNode In node_.Nodes
            If node__.Cells.Item(0).Value.ToString().Split("(")(0) = ID.ToString().Split("(")(0) Then
                TGV_MTC.CurrentCell = node__.Cells.Item(0)
                PopulatePropertyList(node__.Tag)
                GoTo Stop__
            End If
            selectnode(node__, ID, tag, value__)
        Next
Stop__:
    End Sub

    Private Sub DGV_sendto_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_sendto.CellEndEdit
        Try
            Dim lines As New List(Of String)
            For Each row As DataGridViewRow In DGV_sendto.Rows
                If row.Cells(1).Value IsNot Nothing Then
                    lines.Add(row.Cells(0).Value.ToString() + "|" + row.Cells(1).Value.ToString())
                End If
            Next
            If lines.Count > 0 Then
                If File.Exists(home_path & "\sys\Conditions\" & eNetName & "\Sendto.csys") Then
                    File.Delete(home_path & "\sys\Conditions\" & eNetName & "\Sendto.csys")
                End If
                Using w As StreamWriter = New StreamWriter(home_path + "\sys\Conditions\" & eNetName & "\Sendto.csys")
                    For Each line In lines
                        w.WriteLine(line)
                    Next
                    w.Close()
                End Using
                If File.Exists(home_path & "\sys\Conditions\" & eNetName & "\text.csys") Then
                    File.Delete(home_path & "\sys\Conditions\" & eNetName & "\text.csys")
                End If
                Using w As StreamWriter = New StreamWriter(home_path + "\sys\Conditions\" & MachineName & "\text.csys")
                    w.WriteLine(TB_STATUS_TEXT.Text)
                    w.WriteLine("|")
                    w.WriteLine(TB_Cond_TEXT.Text)
                    w.Close()
                End Using
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BTN_Config_email_Click(sender As Object, e As EventArgs) Handles BTN_Config_email.Click
        Dim emailfrm As New EmailServer
        emailfrm.ShowDialog()
        emailfrm.BringToFront()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim s As String = ""
        Dim h As String = ""
        Dim hh As String = ""
        Dim xml As DataStream = Current_stream
        Dim device_index As Integer = 0

        Try
            Dim Current_device As DeviceStream
            Dim value As String = ""

            's = s + "<table style=""width:100%"">"
            For Each device In Devices
                If (MachineName = device.Name) Then
                    Current_device = Current_stream.DeviceStreams(device_index)
                    Dim current_component_stream() As ComponentStream = Current_device.ComponentStreams

                    's = add_LI_parent(device.uuid, device.Name, "")

                    h = ""
                    hh = ""

                    For Each component In device.Components

                        hh = fill_component_(component, Current_device)

                        h = h + add_LI_parent(component.ID, component.name, "<ol class=""dd-list"">" + hh + "</ol>")

                    Next

                    s = s + add_LI_parent(device.uuid, device.Name, "<ol class=""dd-list"">" + h + "</ol>")
                    ' s = s + "<table style=""width:100%"">"
                    For Each dataItem In device.DataItems

                        Dim displayName As String = dataItem.ID

                        For Each Current_comp_stream In current_component_stream
                            If Current_comp_stream.ComponentType = "Device" Then
                                Dim current_events() As IDataElement = Current_comp_stream.Events
                                For Each ev In current_events
                                    If displayName = ev.DataItemID Then
                                        s = s + add_LI(ev.DataItemID, "<td><b>" + ev.Type + "  : </b></td> <td> " + ev.Value + "</td>")

                                    End If
                                Next
                            End If
                        Next

                    Next
                    '  s = s + "</table>"
                End If
                device_index += 1
            Next
            ' s = s + "</table>"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim filepath As String = CSI_Library.CSI_Library.serverRootPath + "\html\html\Notification\resps.html"
        Dim resps As String = ""
        If (File.Exists(filepath)) Then
            Using reader As StreamReader = New StreamReader(filepath)
                resps = reader.ReadToEnd
                reader.Close()
            End Using
        End If


        resps = resps.Replace("[BIG_TEXT]", s)

        If (File.Exists((CSI_Library.CSI_Library.serverRootPath + "\html\html\saved_pages\resps2.html"))) Then
            File.Delete(CSI_Library.CSI_Library.serverRootPath + "\html\html\saved_pages\resps2.html")
        End If

        Using reader As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath + "\html\html\saved_pages\resps2.html")
            reader.Write(resps)
            reader.Close()
        End Using


    End Sub

    Private Function fill_component_(component As OpenNETCF.MTConnect.Component, Current_device As DeviceStream) As String


        Dim boldFont As Font = New Font(TGV_MTC.DefaultCellStyle.Font.Name, 10, TGV_MTC.DefaultCellStyle.Font.Bold, TGV_MTC.DefaultCellStyle.Font.Unit)
        Dim hh As String = ""
        Dim sub_h As String = ""

        For Each subcomponent In component.Components
            sub_h = fill_component_(subcomponent, Current_device)
            hh = hh + add_LI_parent(subcomponent.ID, subcomponent.Name, "<ol class=""dd-list"">" + sub_h + "</ol>")
        Next
        sub_h = ""


        Dim found As Boolean = False

        Dim component_index As Integer = -1
        For Each subcomponent In Current_device.ComponentStreams
            component_index += 1
            If subcomponent.Name = component.Name And found = False Then
                found = True
                Exit For
            End If
        Next

        If found = True Or Current_device.ComponentStreams.Count = 0 Then
            Dim Current_dataitems() As IDataElement = Current_device.ComponentStreams(component_index).AllDataItems
            Dim Current_conditions() As IDataElement = Current_device.ComponentStreams(component_index).Conditions


            Dim first__ As Boolean = True
            Dim value As String = ""

            'hh += "<table style=""width:100%"">"

            For Each dataItem In component.DataItems




                Dim displayName As String = dataItem.ID

                value = ""
                If Not (String.IsNullOrEmpty(dataItem.Name)) Then
                    displayName += String.Format(" ({0})", dataItem.Name)

                    For Each CurrentdataItem In Current_dataitems
                        If dataItem.ID = CurrentdataItem.DataItemID Then

                            value = CurrentdataItem.Value
                            Exit For
                        End If
                    Next
                    For Each Currentcondition In Current_conditions
                        If dataItem.ID = Currentcondition.DataItemID Then

                            value = Currentcondition.Value
                        End If

                    Next

                End If

                If Not (String.IsNullOrEmpty(dataItem.ID)) Then
                    For Each Currentcondition In Current_conditions
                        If dataItem.ID = Currentcondition.DataItemID Then

                            value = Currentcondition.Value
                        End If
                    Next

                    For Each CurrentdataItem In Current_dataitems
                        If dataItem.ID = CurrentdataItem.DataItemID Then

                            value = CurrentdataItem.Value
                        End If
                    Next
                End If
                hh += add_LI(dataItem.ID, "<td><b>" + If(dataItem.Name Is Nothing, dataItem.ID, dataItem.Name) + " : </b></td> <td> " + value + "</td>")

            Next
        End If
        'hh += "</table>"
        Return hh
    End Function

    Private Function add_LI_parent(id As String, name As String, inner As String) As String

        Return "<li class=""dd-item"" data-id=" + id + ">" + "<div class=""dd-handle""> " + name + " </div>" + inner + "</li>" + vbCrLf

    End Function

    Private Function add_LI(id As String, inner As String) As String

        Return "<li class=""dd-item"" data-id=" + id + ">" + "<tr>" + inner + "</tr>" + "</li>" + vbCrLf

    End Function

    Private Sub btnClearPartNumber_Click(sender As Object, e As EventArgs) Handles btnClearPartNumber.Click

        lblPartNumber.Text = "Part Number :"
        txtInputPartNumber.Text = ""
        txtPNrPrefix1.Text = ""
        txtPNrFilter1Start.Text = ""
        txtPNrFilter1End.Text = ""
        chkFilter2.Checked = False
        chkFilter3.Checked = False
        txtPNrOutput.Text = ""

        UpdatePartNumberDB(String.Empty, String.Empty)

        mch__.PartNumber_Variable = String.Empty
        mch__.PartNumber_Prefix1 = String.Empty
        mch__.PartNumber_Filter1Start = String.Empty
        mch__.PartNumber_Filter1End = String.Empty
        mch__.PartNumber_Prefix2 = String.Empty
        mch__.PartNumber_Filter2Start = String.Empty
        mch__.PartNumber_Filter2End = String.Empty
        mch__.PartNumber_Prefix3 = String.Empty
        mch__.PartNumber_Filter3Start = String.Empty
        mch__.PartNumber_Filter3End = String.Empty

        If txtPNrOutput.Text = "NOT AVAILABLE" Then
            mch__.PartNumber_Value = txtInputPartNumber.Text
        Else
            mch__.PartNumber_Value = txtPNrOutput.Text
        End If

    End Sub

    Private Sub btnClearProgramNumber_Click(sender As Object, e As EventArgs) Handles btnClearProgramNumber.Click

        lblProgramNumber.Text = "Program No : "
        txtInputProgrNumber.Text = ""
        txtStartProgNumber.Text = ""
        txtEndProgNumber.Text = ""
        txtOutputProgrNumber.Text = ""

        UpdateProgramNumberDB(String.Empty, String.Empty, String.Empty, String.Empty)

        mch__.progno = String.Empty
        mch__.PRN_startwith = String.Empty
        mch__.PRN_endwith = String.Empty

        If txtOutputProgrNumber.Text = "NOT AVAILABLE" Then
            mch__.progno_val = txtInputProgrNumber.Text
        Else
            mch__.progno_val = txtOutputProgrNumber.Text
        End If

    End Sub

    Private Sub btnClearPallet_Click(sender As Object, e As EventArgs) Handles btnClearPallet.Click

        lblActivePallet.Text = "Active Pallet : "
        txtInputPallet.Text = ""
        txtStartPallet.Text = ""
        txtEndPallet.Text = ""
        txtOutputPallet.Text = ""
        chkSendPallet.Checked = False

        'UpdatePalletDB(String.Empty, String.Empty, String.Empty, String.Empty, False)
        UpdatePalletDB()

        mch__.Pallet = String.Empty
        mch__.Pallet_startwith = String.Empty
        mch__.Pallet_endwith = String.Empty

        If txtOutputProgrNumber.Text = "NOT AVAILABLE" Then
            mch__.progno_val = txtInputProgrNumber.Text
        Else
            mch__.progno_val = txtOutputProgrNumber.Text
        End If
        'CSILib.MCHS_(MachineName).progno = String.Empty

    End Sub

    Private Sub btnClearFeedrate_Click(sender As Object, e As EventArgs) Handles btnClearFeedrate.Click

        lblFovr.Text = "Feedrate Override :"

        Dim ip = IIf(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress)

        UpdateFeedrateoverDB(MachineName, ip, String.Empty, String.Empty)
        ClearMinMax("Feedrate Override", MachineName, ip)

        mch__.FeedRate_Variable = String.Empty

        SetupForm2.RefreshAllDevices()

    End Sub

    Private Sub btnClearRapid_Click(sender As Object, e As EventArgs) Handles btnClearRapid.Click

        lblRovr.Text = "Rapid Override : "

        Dim ip = IIf(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress)

        UpdateRapidoverDB(MachineName, ip, String.Empty, String.Empty)
        ClearMinMax("Rapid Override", MachineName, ip)

        mch__.Rapid_Variable = String.Empty

        SetupForm2.RefreshAllDevices()

    End Sub

    Private Sub btnClearSpindle_Click(sender As Object, e As EventArgs) Handles btnClearSpindle.Click

        lblSovr.Text = "Spindle Override :"

        Dim ip = IIf(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress)

        UpdateSpindleoverDB(MachineName, ip, String.Empty, String.Empty)
        ClearMinMax("Spindle Override", MachineName, ip)

        mch__.Spindle_Variable = String.Empty

        SetupForm2.RefreshAllDevices()

    End Sub

    Private Sub btnClearRequiredParts_Click(sender As Object, e As EventArgs) Handles btnClearRequiredParts.Click

        lblRequiredParts.Text = "Required Parts :"

        Dim ip = IIf(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress)

        mch__.RequiredParts_Variable = ""

        UpdateRequiredPartDB(MachineName, ip, mch__.RequiredParts_Variable, 0)

        SetupForm2.RefreshAllDevices()

    End Sub

    Private Sub btnClearPartsCount_Click(sender As Object, e As EventArgs) Handles btnClearPartsCount.Click

        lblPartsCount.Text = "Part Count :"

        Dim ip = IIf(ConnectorType = "MTConnect", MachineIP, AdapterIPAddress)

        mch__.PartCount_Variable = ""

        UpdatePartCountDB(MachineName, ip, mch__.PartCount_Variable, 0)

        SetupForm2.RefreshAllDevices()

    End Sub

    Sub ClearMinMax(ElementName As String, MCHName As String, MCHIP As String)

        Try

            If ElementName = "Feedrate Override" Then
                mysqlUpdate = $"UPDATE ignore `csi_auth`.`tbl_csiothersettings` SET `Feedrate_MIN` = '', `Feedrate_MAX` = '' WHERE ConnectorId = { ConnectorId };"
            ElseIf ElementName = "Spindle Override" Then
                mysqlUpdate = $"UPDATE ignore `csi_auth`.`tbl_csiothersettings` SET `Spindle_MIN` = '', `Spindle_MAX` = '' WHERE ConnectorId = { ConnectorId };"
            ElseIf ElementName = "Rapid Override" Then
                mysqlUpdate = $"UPDATE ignore `csi_auth`.`tbl_csiothersettings` SET `Rapid_MIN` = '', `Rapid_MAX` = '' WHERE ConnectorId = { ConnectorId };"
            End If

            MySqlAccess.ExecuteNonQuery(mysqlUpdate)

        Catch ex As Exception
            MessageBox.Show("Error in Clearing Min/Max Values : " & ex.ToString())
            Log.Error(ex)
        End Try

    End Sub


    Private Sub btnRefreshPartNumber_Click(sender As Object, e As EventArgs) Handles btnRefreshPartNumber.Click

        'Assign Part Number Variable to Label 
        txtPNrFilter1Start.Text = String.Empty
        txtPNrFilter1End.Text = String.Empty

        If Not lblPartNumber.Text = "Part Number :" Then

            Try
                txtInputPartNumber.Text = String.Empty
                txtPNrOutput.Text = "NOT AVAILABLE"

                Dim labelItems = lblPartNumber.Text.Split(":").Select(Function(m) m.Trim()).ToArray()
                Dim fieldName = labelItems(1)

                For Each row As DataGridViewRow In TGV_MTC.Rows

                    Dim rowValue = row.Cells(0).Value.ToString().Split("(").Select(Function(m) m.Trim()).ToArray()

                    If rowValue(0) = fieldName Then
                        txtInputPartNumber.Text = row.Cells(1).Value.ToString() 'patiya besadiya che 
                        Exit For
                    End If

                Next

                txtPNrOutput.Text = CutStartEndValue(txtInputPartNumber.Text, txtPNrFilter1Start.Text, txtPNrFilter1End.Text)

                'Update above values to table tbl_csiothersettings
                UpdatePartNumberDB(fieldName, txtPNrOutput.Text)

                mch__.PartNumber_Variable = String.Empty
                mch__.PartNumber_Prefix1 = String.Empty
                mch__.PartNumber_Filter1Start = String.Empty
                mch__.PartNumber_Filter1End = String.Empty
                mch__.PartNumber_Filter2Apply = False
                mch__.PartNumber_Prefix2 = String.Empty
                mch__.PartNumber_Filter2Start = String.Empty
                mch__.PartNumber_Filter2End = String.Empty
                mch__.PartNumber_Filter3Apply = False
                mch__.PartNumber_Prefix3 = String.Empty
                mch__.PartNumber_Filter3Start = String.Empty
                mch__.PartNumber_Filter3End = String.Empty
                mch__.PartNumber_Value = txtPNrOutput.Text

            Catch ex As Exception

                MessageBox.Show("Error in Part Number Update on Refresh Button : " & ex.ToString())
                Log.Error(ex)

            End Try
        End If
    End Sub

    Private Sub btnRefreshProgNum_Click(sender As Object, e As EventArgs) Handles btnRefreshProgNum.Click

        txtStartProgNumber.Text = String.Empty
        txtEndProgNumber.Text = String.Empty

        If Not lblProgramNumber.Text = "Program No :" Then

            Try
                txtInputProgrNumber.Text = String.Empty
                txtOutputProgrNumber.Text = "NOT AVAILABLE"

                Dim labelItems = lblProgramNumber.Text.Split(":").Select(Function(m) m.Trim()).ToArray()
                Dim fieldName = labelItems(1)

                For Each row As DataGridViewRow In TGV_MTC.Rows

                    If row.Tag.id = fieldName Then 'row.Cells(0).Value.ToString().Contains(substr) And
                        txtInputProgrNumber.Text = row.Cells(1).Value.ToString() 'patiya besadiya che 
                        Exit For
                    End If
                Next

                txtOutputProgrNumber.Text = CutStartEndValue(txtInputProgrNumber.Text, txtStartProgNumber.Text, txtEndProgNumber.Text)

                'Update above values to table tbl_csiothersettings
                UpdateProgramNumberDB(fieldName, txtOutputProgrNumber.Text, txtStartProgNumber.Text, txtEndProgNumber.Text)

                mch__.progno = String.Empty
                mch__.PRN_startwith = String.Empty
                mch__.PRN_endwith = String.Empty
                mch__.progno_val = txtInputProgrNumber.Text

            Catch ex As Exception

                MessageBox.Show("Error in Program Number Update on Refresh Button : " & ex.ToString())
                Log.Error(ex)

            End Try
        End If
    End Sub

    Private Sub btnRefreshPallet_Click(sender As Object, e As EventArgs) Handles btnRefreshPallet.Click

        txtStartPallet.Text = String.Empty
        txtEndPallet.Text = String.Empty

        If Not lblActivePallet.Text = "Active Pallet :" Then

            Try
                txtInputPallet.Text = String.Empty
                txtOutputPallet.Text = "NOT AVAILABLE"

                Dim labelItems = lblActivePallet.Text.Split(":").Select(Function(m) m.Trim()).ToArray()
                Dim fieldName = labelItems(1)

                For Each row As DataGridViewRow In TGV_MTC.Rows

                    If row.Tag.Id = fieldName Then 'row.Cells(0).Value.ToString().Contains(substr) And
                        txtInputPallet.Text = row.Cells(1).Value.ToString() 'patiya besadiya che 
                        Exit For
                    End If
                Next

                txtOutputPallet.Text = CutStartEndValue(txtInputPallet.Text, txtStartPallet.Text, txtEndPallet.Text)

                'Update above values to table tbl_csiothersettings
                'UpdatePalletDB(fieldName, txtOutputPallet.Text, txtStartPallet.Text, txtEndPallet.Text, chkSendPallet.Checked)
                UpdatePalletDB()

                mch__.Pallet = String.Empty
                mch__.Pallet_startwith = String.Empty
                mch__.Pallet_endwith = String.Empty
                mch__.Pallet_val = txtInputProgrNumber.Text
                mch__.Pallet_sendToMU = chkSendPallet.Checked

            Catch ex As Exception

                MessageBox.Show("Error in Active Pallet Update on Refresh Button : " & ex.ToString())
                Log.Error(ex)

            End Try
        End If
    End Sub



    Private Sub FeedRateOverEdit_Click(sender As Object, e As EventArgs) Handles BTN_FeedrateEdit.Click

        If BTN_FeedrateEdit.ImageIndex = 0 Then

            txtMinFovr.Enabled = True
            txtMinFovr.ReadOnly = False
            txtMaxFovr.Enabled = True
            txtMaxFovr.ReadOnly = False
            BTN_FeedrateOK.Enabled = True
            BTN_FeedrateEdit.ImageIndex = 1 'My.Resources.cancel_button_icon_63471 'Image for cancel the operation

        ElseIf BTN_FeedrateEdit.ImageIndex = 1 Then 'Is My.Resources.cancel_button_icon_63471 Then

            Dim table As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.view_csiothersettings Where ConnectorId = { ConnectorId };")

            If table.Rows.Count > 0 Then
                txtMinFovr.Text = table.Rows(0).Item("Feedrate_MIN").ToString()
                txtMaxFovr.Text = table.Rows(0).Item("Feedrate_MAX").ToString()
            End If

            txtMinFovr.Enabled = False
            txtMaxFovr.Enabled = False
            txtMinFovr.ReadOnly = True
            txtMaxFovr.ReadOnly = True
            BTN_FeedrateOK.Enabled = False
            BTN_FeedrateEdit.ImageIndex = 0 'My.Resources.images 'Image for Edit Operation

        End If

        RefreshServiceSettings()

    End Sub

    Private Sub SpindleOverEdit_Click(sender As Object, e As EventArgs) Handles BTN_SpindleEdit.Click

        If BTN_SpindleEdit.ImageIndex = 0 Then ' Is My.Resources.images Then

            txtMinSovr.ReadOnly = False
            txtMaxSovr.ReadOnly = False
            txtMinSovr.Enabled = True
            txtMaxSovr.Enabled = True
            BTN_SpindleOK.Enabled = True
            BTN_SpindleEdit.ImageIndex = 1 'My.Resources.cancel_button_icon_63471 'Image for cancel the operation

        ElseIf BTN_SpindleEdit.ImageIndex = 1 Then ' Is My.Resources.cancel_button_icon_63471 Then

            Dim table As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.view_csiothersettings Where ConnectorId = { ConnectorId };")

            If table.Rows.Count > 0 Then
                txtMinSovr.Text = table.Rows(0).Item("Spindle_MIN").ToString()
                txtMaxSovr.Text = table.Rows(0).Item("Spindle_MAX").ToString()
            End If

            txtMinSovr.ReadOnly = True
            txtMaxSovr.ReadOnly = True
            txtMinSovr.Enabled = False
            txtMaxSovr.Enabled = False
            BTN_SpindleOK.Enabled = False
            BTN_SpindleEdit.ImageIndex = 0 'My.Resources.images 'Image for Edit Operation

        End If

        RefreshServiceSettings()

    End Sub

    Private Sub RapidOverEdit_Click(sender As Object, e As EventArgs) Handles BTN_RapidEdit.Click

        If BTN_RapidEdit.ImageIndex = 0 Then ' Is My.Resources.images Then

            txtMinRovr.ReadOnly = False
            txtMaxRovr.ReadOnly = False
            txtMinRovr.Enabled = True
            txtMaxRovr.Enabled = True
            BTN_RapidOK.Enabled = True
            BTN_RapidEdit.ImageIndex = 1 'My.Resources.cancel_button_icon_63471 'Image for cancel the operation

        ElseIf BTN_RapidEdit.ImageIndex = 1 Then 'Is My.Resources.cancel_button_icon_63471 Then

            Dim table As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.view_csiothersettings Where ConnectorId = { ConnectorId };")

            If table.Rows.Count > 0 Then
                txtMinRovr.Text = table.Rows(0).Item("Rapid_MIN").ToString()
                txtMaxRovr.Text = table.Rows(0).Item("Rapid_MAX").ToString()
            End If

            txtMinRovr.ReadOnly = True
            txtMaxRovr.ReadOnly = True
            txtMinRovr.Enabled = False
            txtMaxRovr.Enabled = False
            BTN_RapidOK.Enabled = False
            BTN_RapidEdit.ImageIndex = 0 'My.Resources.images 'Image for Edit Operation

        End If

        RefreshServiceSettings()

    End Sub

    Private Sub BTN_FeedrateOK_Click(sender As Object, e As EventArgs) Handles BTN_FeedrateOK.Click

        If txtMinFovr.Text.Length > 0 And txtMaxFovr.Text.Length > 0 Then
            Try
                MySqlAccess.ExecuteNonQuery($"UPDATE ignore `csi_auth`.`tbl_csiothersettings` SET `Feedrate_MIN` = '{ txtMinFovr.Text }', `Feedrate_MAX` = '{ txtMaxFovr.Text }' WHERE ConnectorId = { ConnectorId }")

            Catch ex As Exception
                MessageBox.Show("Error in Update MIN/MAX values for Feedrate Override : " & ex.ToString())
                Log.Error(ex)
            End Try
        Else
            MessageBox.Show("Feedrate Override's Min/Max values shouldn't be empty !")
        End If

        mch__.Min_Fover = txtMinFovr.Text
        mch__.Max_Fover = txtMaxFovr.Text
        txtMinFovr.Enabled = False
        txtMaxFovr.Enabled = False
        txtMinFovr.ReadOnly = True
        txtMaxFovr.ReadOnly = True
        BTN_FeedrateOK.Enabled = False
        BTN_FeedrateEdit.ImageIndex = 0
        BTN_FeedrateEdit.Enabled = True

        RefreshServiceSettings()

    End Sub

    Private Sub BTN_SpindleOK_Click(sender As Object, e As EventArgs) Handles BTN_SpindleOK.Click

        If txtMinSovr.Text.Length > 0 And txtMaxSovr.Text.Length > 0 Then
            Try
                MySqlAccess.ExecuteNonQuery($"UPDATE ignore `csi_auth`.`tbl_csiothersettings` SET `Spindle_MIN` = '{ txtMinSovr.Text }', `Spindle_MAX` = '{ txtMaxSovr.Text }' WHERE ConnectorId = { ConnectorId }")

            Catch ex As Exception
                MessageBox.Show("Error in Update MIN/MAX values for Spindle Override : " & ex.ToString())
                Log.Error(ex)
            End Try
        Else
            MessageBox.Show("Spindle Override's Min/Max values shouldn't be empty !")
        End If

        mch__.Min_Sover = txtMinSovr.Text
        mch__.Max_Sover = txtMaxSovr.Text
        txtMinSovr.ReadOnly = True
        txtMaxSovr.ReadOnly = True
        txtMinSovr.Enabled = False
        txtMaxSovr.Enabled = False
        BTN_SpindleOK.Enabled = False
        BTN_SpindleEdit.ImageIndex = 0
        BTN_SpindleEdit.Enabled = True

        RefreshServiceSettings()

    End Sub

    Private Sub BTN_RapidOK_Click(sender As Object, e As EventArgs) Handles BTN_RapidOK.Click

        If txtMinRovr.Text.Length > 0 And txtMaxRovr.Text.Length > 0 Then
            Try
                MySqlAccess.ExecuteNonQuery($"UPDATE ignore `csi_auth`.`tbl_csiothersettings` SET `Rapid_MIN` = '{ txtMinRovr.Text }', `Rapid_MAX` = '{ txtMaxRovr.Text }' WHERE ConnectorId = { ConnectorId }")

            Catch ex As Exception
                MessageBox.Show("Error in Update MIN/MAX values for Rapid Override : " & ex.ToString())
                Log.Error(ex)
            End Try
        Else
            MessageBox.Show("Rapid Override's Min/Max values shouldn't be empty !")
        End If

        mch__.Min_Rover = txtMinRovr.Text
        mch__.Max_Rover = txtMaxRovr.Text
        txtMinRovr.ReadOnly = True
        txtMaxRovr.ReadOnly = True
        txtMinRovr.Enabled = False
        txtMaxRovr.Enabled = False
        BTN_RapidOK.Enabled = False
        BTN_RapidEdit.ImageIndex = 0
        BTN_RapidEdit.Enabled = True

        RefreshServiceSettings()

    End Sub

    Private Sub btnDisableStatus_Click(sender As Object, e As EventArgs) Handles btnDisableStatus.Click

        isStatusDisabled = Not isStatusDisabled

        lblStatusDisabled.Visible = isStatusDisabled

        btnDisableStatus.Text = IIf(isStatusDisabled, "Enable Status", "Disable Status")

        MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.tbl_mtcfocasconditions SET StatusDisabled = {isStatusDisabled} WHERE ConnectorId = { ConnectorId } And `Status` = '{ selectedStatus }' ;")

    End Sub

    Private Sub cmbTimeDelay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTimeDelay.SelectedIndexChanged
        isMinuteScale = (cmbTimeDelay.SelectedIndex = 1)
    End Sub

    Private Sub chkEnableCriticalStop_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnableCriticalStop.CheckedChanged

        If ConnectorId = 0 Then Return

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings           ")
            sqlCmd.Append($"   SET                                                ")
            sqlCmd.Append($"      EnableMCS   = { chkEnableCriticalStop.Checked } ")
            sqlCmd.Append($"   WHERE                                              ")
            sqlCmd.Append($"      ConnectorId = { ConnectorId }                 ; ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Update Critical Stop Problem : " & ex.ToString())
            Log.Error("Database Update Critical Stop Problem.", ex)
        End Try

    End Sub

    Private Sub chkSaveDataraw_CheckedChanged(sender As Object, e As EventArgs) Handles chkSaveDataraw.CheckedChanged

        If ConnectorId = 0 Then Return

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings    ")
            sqlCmd.Append($"   SET                                         ")
            sqlCmd.Append($"      SaveDataRaw = { chkSaveDataraw.Checked } ")
            sqlCmd.Append($"   WHERE                                       ")
            sqlCmd.Append($"      ConnectorId = { ConnectorId }          ; ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

            chkSaveRawDuringProd.Visible = chkSaveDataraw.Checked
            chkSaveRawDuringProd.Checked = chkSaveDataraw.Checked

        Catch ex As Exception
            MessageBox.Show("Database Update Save Data Raw Problem : " & ex.ToString())
            Log.Error("Database Update Save Data Raw Problem.", ex)
        End Try

    End Sub

    Private Sub chkSaveCOnDuringSetup_CheckedChanged(sender As Object, e As EventArgs) Handles chkSaveCOnDuringSetup.CheckedChanged

        If ConnectorId = 0 Then Return

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings    ")
            sqlCmd.Append($"   SET                                         ")
            sqlCmd.Append($"      COnDuringSetup = { chkSaveCOnDuringSetup.Checked } ")
            sqlCmd.Append($"   WHERE                                       ")
            sqlCmd.Append($"      ConnectorId = { ConnectorId }          ; ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

            'chkSaveRawDuringProd.Visible = chkSaveDataraw.Checked
            'chkSaveRawDuringProd.Checked = chkSaveDataraw.Checked

        Catch ex As Exception
            MessageBox.Show("Database Update Save Data Raw Problem : " & ex.ToString())
            Log.Error("Database Update Save Data Raw Problem.", ex)
        End Try

    End Sub

    Private Sub chkSaveRawDuringProd_CheckedChanged(sender As Object, e As EventArgs) Handles chkSaveRawDuringProd.CheckedChanged

        If ConnectorId = 0 Then Return

        Dim saveValue = IIf(chkSaveRawDuringProd.Checked, 1, 0)

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings ")
            sqlCmd.Append($"   SET                                      ")
            sqlCmd.Append($"      SaveProdOnly = { saveValue }          ")
            sqlCmd.Append($"   WHERE                                    ")
            sqlCmd.Append($"      ConnectorId = { ConnectorId }       ; ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

            chkSaveRawDuringSetup.Visible = chkSaveRawDuringProd.Checked
            chkSaveRawDuringSetup.Checked = chkSaveRawDuringProd.Checked

        Catch ex As Exception
            MessageBox.Show("Database Update Save Raw Data During Prod. Problem : " & ex.ToString())
            Log.Error("Database Update Save Raw Data During Prod. Problem.", ex)
        End Try

    End Sub

    Private Sub chkSaveRawDuringSetup_CheckedChanged(sender As Object, e As EventArgs) Handles chkSaveRawDuringSetup.CheckedChanged

        If ConnectorId = 0 Then Return

        If Not chkSaveRawDuringSetup.Visible Then Return

        Dim saveValue = IIf(chkSaveRawDuringSetup.Checked, 2, 1)

        Try
            Dim sqlCmd As New StringBuilder()
            sqlCmd.Append($"UPDATE IGNORE csi_auth.tbl_csiothersettings ")
            sqlCmd.Append($"   SET                                      ")
            sqlCmd.Append($"      SaveProdOnly = { saveValue }          ")
            sqlCmd.Append($"   WHERE                                    ")
            sqlCmd.Append($"      ConnectorId = { ConnectorId }       ; ")

            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        Catch ex As Exception
            MessageBox.Show("Database Update Save Raw Data During Prod. Problem : " & ex.ToString())
            Log.Error("Database Update Save Raw Data During Prod. Problem.", ex)
        End Try

    End Sub


    Private Sub chkFilter2_CheckedChanged(sender As Object, e As EventArgs) Handles chkFilter2.CheckedChanged

        txtPNrPrefix2.Enabled = chkFilter2.Checked
        txtPNrFilter2Start.Enabled = chkFilter2.Checked
        txtPNrFilter2End.Enabled = chkFilter2.Checked

        chkFilter3.Enabled = chkFilter2.Checked

    End Sub

    Private Sub chkFilter3_CheckedChanged(sender As Object, e As EventArgs) Handles chkFilter3.CheckedChanged

        txtPNrPrefix3.Enabled = chkFilter3.Checked
        txtPNrFilter3Start.Enabled = chkFilter3.Checked
        txtPNrFilter3End.Enabled = chkFilter3.Checked

    End Sub

    Private Sub DelayForCycleOff_ValueChanged(sender As Object, e As EventArgs) Handles nudDelayForCycleOff.ValueChanged, cmbDelayForCycleOffScale.SelectedIndexChanged

        If ConnectorId < 1 Or cmbDelayForCycleOffScale.SelectedIndex < 0 Then Return

        Dim scale = IIf(cmbDelayForCycleOffScale.SelectedIndex = 0, "sec", "min")
        Dim time = IIf(scale = "sec", nudDelayForCycleOff.Value, nudDelayForCycleOff.Value * 60)

        Dim sqlCmd As New StringBuilder()
        sqlCmd.Append($"UPDATE IGNORE                           ")
        sqlCmd.Append($"    csi_auth.tbl_csiothersettings       ")
        sqlCmd.Append($" SET                                    ")
        sqlCmd.Append($"    DelayForCycleOff = { time },        ")
        sqlCmd.Append($"    DelayForCycleOffScale = '{ scale }' ")
        sqlCmd.Append($" WHERE                                  ")
        sqlCmd.Append($"    ConnectorId = { ConnectorId }     ; ")

        Try
            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())
        Catch ex As Exception
            Log.Error(ex)
        End Try

    End Sub

    Private Sub chkOperation_CheckedChanged(sender As Object, e As EventArgs) Handles chkOperation.CheckedChanged

        txtOperationStart.ReadOnly = Not chkOperation.Checked
        txtOperationEnd.ReadOnly = Not chkOperation.Checked

        If Not chkOperation.Checked Then
            txtOperationStart.Clear()
            txtOperationEnd.Clear()
            txtOperationOutput.Clear()
        End If
    End Sub

#End Region






#End Region

End Class


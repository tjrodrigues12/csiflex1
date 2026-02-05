Imports System.IO
Imports System.Net
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Server.Library
Imports CSIFLEX.Utilities
Imports OpenNETCF.MTConnect

Public Class MonitoringUnitsList

    Private boards As List(Of MonitoringBoard)
    Private board As MonitoringBoard
    Private selectedBoardId As Integer = 0

    'Private entityClient As EntityClient


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub MonitoringUnitsList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'entityClient = New EntityClient()
        LoadBoardsGrid()

    End Sub

    Private Sub LoadBoardsGrid(Optional showDeleted As Boolean = False)

        dgvMonitoringUnits.DataSource = Nothing
        dgvSensors.DataSource = Nothing
        FillBoardFields(Nothing)

        boards = MonitoringBoardsService.GetBoards()

        If showDeleted Then
            dgvMonitoringUnits.DataSource = boards
        Else
            dgvMonitoringUnits.DataSource = boards.Where(Function(m) Not m.Deleted).ToList()
        End If
        dgvMonitoringUnits.Refresh()

        BoardFieldsVisible(False)

    End Sub

    Private Sub CreateSensorsGrid()

        Dim defaultCellStyle As DataGridViewCellStyle = dgvSensors.DefaultCellStyle()

        dgvSensors.Columns.Clear()
        dgvSensors.DefaultCellStyle = defaultCellStyle

        Dim column As DataGridViewColumn

        column = New DataGridViewTextBoxColumn()
        column.Name = "snsId"
        column.DataPropertyName = "SensorId"
        column.HeaderText = "Id"
        column.Visible = False
        column.ReadOnly = True
        dgvSensors.Columns.Add(column)

        column = New DataGridViewTextBoxColumn()
        column.Name = "snsLabel"
        column.DataPropertyName = "SensorLabel"
        column.HeaderText = "Label"
        column.Visible = True
        column.ReadOnly = True
        dgvSensors.Columns.Add(column)

        column = New DataGridViewTextBoxColumn()
        column.Name = "snsType"
        column.DataPropertyName = "SensorType"
        column.HeaderText = "Type"
        column.Visible = True
        column.ReadOnly = True
        dgvSensors.Columns.Add(column)

        column = New DataGridViewTextBoxColumn()
        column.Name = "snsGroup"
        column.DataPropertyName = "SensorGroup"
        column.HeaderText = "Pallet"
        column.Visible = True
        column.ReadOnly = True
        dgvSensors.Columns.Add(column)

        column = New DataGridViewTextBoxColumn()
        column.Name = "snsMac"
        column.DataPropertyName = "SensorMac"
        column.HeaderText = "MAC"
        column.Visible = True
        column.ReadOnly = True
        dgvSensors.Columns.Add(column)

        column = New DataGridViewTextBoxColumn()
        column.Name = "snsManufacturer"
        column.DataPropertyName = "SensorManufacturer"
        column.HeaderText = "Manufacturer"
        column.Visible = True
        column.ReadOnly = True
        dgvSensors.Columns.Add(column)

        column = New DataGridViewTextBoxColumn()
        column.Name = "snsModel"
        column.DataPropertyName = "SensorModel"
        column.HeaderText = "Model"
        column.Visible = True
        column.ReadOnly = True
        dgvSensors.Columns.Add(column)

        column = New DataGridViewTextBoxColumn()
        column.Name = "snsSerialNumber"
        column.DataPropertyName = "SensorSerialNumber"
        column.HeaderText = "Serial #"
        column.Visible = True
        column.ReadOnly = True
        dgvSensors.Columns.Add(column)

        column = New DataGridViewCheckBoxColumn()
        column.Name = "snsDeleted"
        column.DataPropertyName = "Deleted"
        column.HeaderText = "Deleted"
        column.Visible = True
        column.ReadOnly = True
        dgvSensors.Columns.Add(column)

        dgvSensors.Columns("snsId").Visible = False

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvMonitoringUnits_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMonitoringUnits.SelectionChanged

        board = Nothing
        selectedBoardId = 0

        If dgvMonitoringUnits.SelectedRows.Count = 0 Then Return

        If dgvMonitoringUnits.SelectedRows(0).Index < 0 Then Return

        Dim selectedRow As DataGridViewRow = dgvMonitoringUnits.SelectedRows(0)

        selectedBoardId = selectedRow.Cells("Id").Value

        board = boards.FirstOrDefault(Function(b) b.Id = selectedBoardId)

        FillBoardFields(board)

        BoardFieldsVisible(False)

        dgvSensors.DataSource = Nothing
        dgvSensors.Rows.Clear()

        CreateSensorsGrid()
        dgvSensors.AutoGenerateColumns = False

        dgvSensors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        If chkShowDeleted.Checked Then
            dgvSensors.DataSource = board.Sensors
        Else
            dgvSensors.DataSource = board.Sensors.Where(Function(s) s.Deleted = False).ToList()
        End If

        dgvSensors.Refresh()

        'LoadSensorsState()

    End Sub

    Private Sub lblLinkSettings_Click(sender As Object, e As EventArgs) Handles lblLinkSettings.Click

        If String.IsNullOrEmpty(lblIpAddress.Text) Then
            Return
        End If

        Dim webAddress As String = $"http://{lblIpAddress.Text}/"
        Process.Start(webAddress)

    End Sub

    Private Sub btnNewBoard_Click(sender As Object, e As EventArgs) Handles btnNewBoard.Click

        FillBoardFields(Nothing)

        BoardFieldsVisible(True)

        btnEditBoard.Enabled = False
        btnDeleteBoard.Enabled = False
    End Sub

    Private Sub btnEditBoard_Click(sender As Object, e As EventArgs) Handles btnEditBoard.Click

        If selectedBoardId = 0 Then Return

        BoardFieldsVisible(True)

        btnNewBoard.Enabled = False
        btnDeleteBoard.Enabled = False
    End Sub

    Private Sub btnDeleteBoard_Click(sender As Object, e As EventArgs) Handles btnDeleteBoard.Click

        If selectedBoardId = 0 Then Return

        If Not MessageBox.Show("Do you confirm the exclusion of this board?", "Confirm!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then Return

        Try
            If Not String.IsNullOrEmpty(board.MachineName) Then
                MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.tbl_csiconnector SET MonitoringUnitId = 0 WHERE MonitoringUnitId = { selectedBoardId };")
            End If

            MonitoringBoardsService.DeleteBoard(selectedBoardId)

            RefreshServiceSettings()

        Catch ex As Exception
            Log.Error(ex)
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        LoadBoardsGrid(chkShowDeleted.Checked)

    End Sub

    Private Sub btnSaveBoard_Click(sender As Object, e As EventArgs) Handles btnSaveBoard.Click

        'Dim dataOkay As Boolean = True

        'If String.IsNullOrEmpty(txtUnitLabel.Text) Then
        '    dataOkay = False
        '    txtUnitLabel.BackColor = Color.FromArgb(255, 192, 203)
        'End If

        'If String.IsNullOrEmpty(txtIpAddress.Text) Then
        '    dataOkay = False
        '    txtIpAddress.BackColor = Color.FromArgb(255, 192, 203)
        'End If

        'If String.IsNullOrEmpty(txtMacAddress.Text) Then
        '    dataOkay = False
        '    txtMacAddress.BackColor = Color.FromArgb(255, 192, 203)
        'End If

        'If Not dataOkay Then Return

        'If Not MessageBox.Show("Do you confirm the inclusion of a new board?", "Confirm!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Yes Then Return

        'Dim newBoard As MonitoringBoard = New MonitoringBoard()
        'newBoard.Firmware = txtFirmware.Text
        'newBoard.IpAddress = txtIpAddress.Text
        'newBoard.Label = txtUnitLabel.Text
        'newBoard.Firmware = txtFirmware.Text
        'newBoard.Mac = txtMacAddress.Text
        'newBoard.Manufacturer = txtManufacturer.Text
        'newBoard.Model = txtModel.Text
        'newBoard.SerialNumber = txtSerialNumber.Text

        'Try
        '    MonitoringBoardsService.CreateNewBoard(newBoard)
        'Catch ex As Exception
        '    Log.Error(ex)
        '    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return
        'End Try

        'LoadBoardsGrid()

        'btnEditBoard.Enabled = True
        'btnDeleteBoard.Enabled = True

    End Sub

    Private Sub BoardFieldsVisible(visible As Boolean)

        'txtUnitLabel.Visible = visible
        'txtIpAddress.Visible = visible
        'txtMacAddress.Visible = visible
        'txtManufacturer.Visible = visible
        'txtModel.Visible = visible
        'txtSerialNumber.Visible = visible
        'txtFirmware.Visible = visible

    End Sub

    Private Sub txtBoardField_TextChanged(sender As Object, e As EventArgs)

        Dim textBox As TextBox = CType(sender, TextBox)

        textBox.BackColor = Color.White

    End Sub

    Private Sub RefreshServiceSettings()

        Try

            Dim portT As DataTable = MySqlAccess.GetDataTable("Select port From csi_database.tbl_rm_port;")

            Dim request As WebRequest

            If portT.Rows.Count <> 0 Then
                If IsDBNull(portT.Rows(0)("port")) Then
                    request = WebRequest.Create("http://127.0.0.1:8008/readconfig")
                Else
                    request = WebRequest.Create($"http://127.0.0.1:{ portT.Rows(0)("port") }/readMTCconfig")
                End If
            Else
                request = WebRequest.Create("http://127.0.0.1:8008/readconfig")
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

    Private Sub FillBoardFields(_board As MonitoringBoard)

        If _board Is Nothing Then
            lblUnitLabel.Text = ""
            lblIpAddress.Text = ""
            lblMacAddress.Text = ""
            lblManufacturer.Text = ""
            lblModel.Text = ""
            lblSerialNumber.Text = ""
            lblFirmware.Text = ""
            lblCreatedAt.Text = ""
            lblMachineName.Text = ""
            lblDeleted.Visible = False
            btnDeleteBoard.Visible = False
        Else
            lblUnitLabel.Text = _board.Label
            lblIpAddress.Text = _board.IpAddress
            lblMacAddress.Text = _board.Mac
            lblManufacturer.Text = _board.Manufacturer
            lblModel.Text = _board.Model
            lblSerialNumber.Text = _board.SerialNumber
            lblFirmware.Text = _board.Firmware
            lblCreatedAt.Text = _board.CreatedAt
            lblMachineName.Text = _board.MachineName
            lblDeleted.Visible = _board.Deleted
            btnDeleteBoard.Visible = Not _board.Deleted
        End If

    End Sub

    Private Sub dgvMonitoringUnits_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvMonitoringUnits.RowPostPaint

        Dim rowIdx = e.RowIndex

        If rowIdx < 0 Then Return

        Dim row = dgvMonitoringUnits.Rows(rowIdx)
        Dim isDeleted As Boolean = Boolean.Parse(row.Cells("Deleted").Value)

        If isDeleted Then
            row.DefaultCellStyle.ForeColor = Color.Gray
            row.DefaultCellStyle.SelectionBackColor = Color.LightCoral
            row.DefaultCellStyle.SelectionForeColor = Color.Black
        End If

    End Sub

    Private Sub ckbShowDeleted_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowDeleted.CheckedChanged

        LoadBoardsGrid(chkShowDeleted.Checked)

    End Sub

    Private Sub dgvMonitoringUnits_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvMonitoringUnits.CurrentCellDirtyStateChanged

        If dgvMonitoringUnits.IsCurrentCellDirty Then
            dgvMonitoringUnits.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub

    Private Sub LoadSensorsState()

        If selectedBoardId = 0 Then Return

        Dim muCurrentXml = MonitoringBoardsService.GetCurrentXml(selectedBoardId)

        'Dim currentStream As DataStream = entityClient.CurrentFromXml(muCurrentXml)


    End Sub

End Class


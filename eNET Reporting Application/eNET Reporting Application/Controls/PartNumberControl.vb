Imports System.Text
Imports CSIFLEX.Database.Access
Imports CSIFLEX.eNetLibrary
Imports CSIFLEX.Utilities

Public Class PartNumberControl

    Dim connString As String = CSI_Library.CSI_Library.MySqlConnectionString
    Dim inEditMode As Boolean = False
    Dim partNumberId As Integer = 0

    Dim gridHistory As List(Of PartNumber)
    Dim gridHistorySource As BindingSource

    Dim lstPartNumberItens As List(Of String)
    Dim lstMachinesItens As List(Of String)

    Dim timeScale() As String = {"s", "m", "h"}

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub PartNumberControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        gridHistory = New List(Of PartNumber)()
        gridHistorySource = New BindingSource()
        gridHistorySource.DataSource = gridHistory

        dgvPartNumbers.AutoGenerateColumns = False

        dgvHistory.AutoGenerateColumns = False
        dgvHistory.DataSource = gridHistorySource

        lstPartNumberItens = New List(Of String)()
        lstMachinesItens = New List(Of String)()

        Dim query = $"SELECT * FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1 ORDER BY Machine_Name;"
        Dim tbMachines As DataTable = MySqlAccess.GetDataTable(query, connString)
        cmbMachines.DataSource = tbMachines
        cmbMachines.DisplayMember = "Machine_Name"
        cmbMachines.ValueMember = "Id"

        query = $"SELECT * FROM csi_database.vw_masterpartnumbers;"
        Dim tbPartNumbers As DataTable = MySqlAccess.GetDataTable(query, connString)
        Dim gridItem As PartNumber
        Dim rowId As Integer = 0

        dgvPartNumbers.Rows.Clear()

        For Each row As DataRow In tbPartNumbers.Rows
            gridItem = New PartNumber
            gridItem.Id = row("Id")
            gridItem.MachineId = row("MachineId")
            gridItem.MachineName = row("Machine_Name")
            gridItem.PartNumber = row("PartNumber")
            gridItem.Operation = row("Operation")
            gridItem.CycleTimeSec = row("CycleTime")
            gridItem.CycleMultiplier = row("CycleMultiplier")
            gridItem.CreatedBy = row("UserDisplayName")
            gridItem.CreatedWhen = row("CreatedWhen")
            gridItem.LoadTimeSec = row("PartLoad")
            gridItem.SetupTimeSec = row("SetupTime")
            gridItem.CycleTimeScale = row("DisplayCycleTimeScale")
            gridItem.SetupTimeScale = row("DisplaySetupTimeScale")
            gridItem.LoadTimeScale = row("DisplayPartLoadTimeScale")

            dgvPartNumbers.Rows.Add(gridItem.Id,
                                    gridItem.PartNumber,
                                    gridItem.MachineId,
                                    gridItem.MachineName,
                                    gridItem.Operation,
                                    gridItem.CycleTime,
                                    gridItem.CycleMultiplier,
                                    gridItem.SetupTime,
                                    gridItem.LoadTime,
                                    gridItem.CreatedBy,
                                    gridItem.CreatedWhen,
                                    gridItem.CycleTimeScale,
                                    gridItem.SetupTimeScale,
                                    gridItem.LoadTimeScale,
                                    gridItem.CycleTimeSec,
                                    gridItem.SetupTimeSec,
                                    gridItem.LoadTimeSec)

            If Not lstPartNumberItens.Contains(row("PartNumber")) Then lstPartNumberItens.Add(row("PartNumber"))
            If Not lstMachinesItens.Contains(row("Machine_Name")) Then lstMachinesItens.Add(row("Machine_Name"))
        Next

        lstPartNumberItens.Sort()
        chkListPartNumbers.DataSource = lstPartNumberItens
        lstMachinesItens.Sort()
        chkListMachines.DataSource = lstMachinesItens

    End Sub

    Private Sub btnFiltersShow_Click(sender As Object, e As EventArgs) Handles btnFiltersShow.Click
        gBoxFilters.Visible = True
    End Sub

    Private Sub btnFiltersHidde_Click(sender As Object, e As EventArgs) Handles btnFiltersHidde.Click
        gBoxFilters.Visible = False
    End Sub

    Private Sub chkBoxPartNumber_CheckedChanged(sender As Object, e As EventArgs) Handles chkBoxPartNumber.CheckedChanged
        For i As Integer = 0 To chkListPartNumbers.Items.Count - 1
            chkListPartNumbers.SetItemChecked(i, chkBoxPartNumber.Checked)
        Next
    End Sub

    Private Sub chkBoxMachines_CheckedChanged(sender As Object, e As EventArgs) Handles chkBoxMachines.CheckedChanged
        For i As Integer = 0 To chkListMachines.Items.Count - 1
            chkListMachines.SetItemChecked(i, chkBoxMachines.Checked)
        Next
    End Sub

    Private Sub btnApplyFilter_Click(sender As Object, e As EventArgs) Handles btnApplyFilter.Click

        Dim partNumbers = ""
        For Each item As String In chkListPartNumbers.CheckedItems
            If partNumbers <> "" Then partNumbers += ";"
            partNumbers += item.ToUpper()
        Next

        Dim machines = ""
        For Each item As String In chkListMachines.CheckedItems
            If machines <> "" Then machines += ";"
            machines += item.ToUpper()
        Next

        Dim partNumber = ""
        Dim machine = ""

        For Each row As DataGridViewRow In dgvPartNumbers.Rows
            row.Visible = True
            partNumber = row.Cells("pncPartNumber").Value.ToString().ToUpper()
            machine = row.Cells("pncMachineName").Value.ToString().ToUpper()

            If Not String.IsNullOrEmpty(partNumbers) Then row.Visible = partNumbers.Contains(partNumber)
            If row.Visible And Not String.IsNullOrEmpty(machines) Then row.Visible = machines.Contains(machine)
        Next

        gBoxFilters.Visible = False

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

        Dim search = txtSearch.Text.ToUpper
        Dim partNumber = ""
        Dim machine = ""

        For Each row As DataGridViewRow In dgvPartNumbers.Rows
            row.Visible = True
            partNumber = row.Cells("pncPartNumber").Value.ToString().ToUpper()
            machine = row.Cells("pncMachineName").Value.ToString().ToUpper()

            If txtSearch.TextLength >= 3 Then
                row.Visible = (partNumber.Contains(search)) Or (machine.Contains(search))
            End If
        Next

    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        txtSearch.Clear()
    End Sub

    Private Sub dgvPartNumbers_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPartNumbers.SelectionChanged

        btnEdit.Enabled = False
        btnDelete.Enabled = False

        If dgvPartNumbers.SelectedRows.Count = 0 Then Return

        If inEditMode Then
            CloseFields()
            inEditMode = False
        End If

        Dim dgRow As DataGridViewRow = dgvPartNumbers.SelectedRows(0)
        partNumberId = dgRow.Cells("pncId").Value

        txtPartNumber.Text = dgRow.Cells("pncPartNumber").Value
        txtOperation.Text = dgRow.Cells("pncOperation").Value
        txtCycleMultiplier.Text = dgRow.Cells("pncCycleMultiplier").Value
        cmbMachines.SelectedValue = dgRow.Cells("pncMachineId").Value

        cmbCycleTimeScale.SelectedIndex = Array.IndexOf(timeScale, dgRow.Cells("pncCycleTimeScale").Value)
        cmbSetupTimeScale.SelectedIndex = Array.IndexOf(timeScale, dgRow.Cells("pncSetupTimeScale").Value)
        cmbLoadTimeScale.SelectedIndex = Array.IndexOf(timeScale, dgRow.Cells("pncLoadTimeScale").Value)

        Dim cycleTime As Double = dgRow.Cells("pncCycleTimeSec").Value
        If dgRow.Cells("pncCycleTimeScale").Value = "m" Then cycleTime = cycleTime / 60
        If dgRow.Cells("pncCycleTimeScale").Value = "h" Then cycleTime = cycleTime / 3600
        txtCycleTime.Text = cycleTime

        Dim setupTime As Double = dgRow.Cells("pncSetupTimeSec").Value
        If dgRow.Cells("pncSetupTimeScale").Value = "m" Then setupTime = setupTime / 60
        If dgRow.Cells("pncSetupTimeScale").Value = "h" Then setupTime = setupTime / 3600
        txtSetupTime.Text = setupTime

        Dim loadTime As Double = dgRow.Cells("pncLoadTimeSec").Value
        If dgRow.Cells("pncLoadTimeScale").Value = "m" Then loadTime = loadTime / 60
        If dgRow.Cells("pncLoadTimeScale").Value = "h" Then loadTime = loadTime / 3600
        txtLoadTime.Text = loadTime

        Dim partNumber As String = dgRow.Cells("pncPartNumber").Value
        Dim machineId As Integer = dgRow.Cells("pncMachineId").Value
        Dim operation As String = dgRow.Cells("pncOperation").Value

        gridHistory.Clear()
        Dim query = $"SELECT * FROM csi_database.vw_historypartnumbers WHERE PartNumber = '{partNumber}' AND MachineId = {machineId} AND Operation = '{operation}';"
        Dim tbPartNumbers As DataTable = MySqlAccess.GetDataTable(query, connString)
        Dim gridItem As PartNumber

        For Each row As DataRow In tbPartNumbers.Rows
            gridItem = New PartNumber
            gridItem.Id = row("Id")
            gridItem.MachineId = row("MachineId")
            gridItem.MachineName = row("Machine_Name")
            gridItem.PartNumber = row("PartNumber")
            gridItem.Operation = row("Operation")
            gridItem.CycleTimeSec = row("CycleTime")
            gridItem.CycleMultiplier = row("CycleMultiplier")
            gridItem.CreatedBy = row("UserDisplayName")
            gridItem.CreatedWhen = row("CreatedWhen")
            gridItem.LoadTimeSec = row("PartLoad")
            gridItem.SetupTimeSec = row("SetupTime")
            gridItem.CycleTimeScale = row("DisplayCycleTimeScale")
            gridItem.SetupTimeScale = row("DisplaySetupTimeScale")
            gridItem.LoadTimeScale = row("DisplayPartLoadTimeScale")
            gridHistory.Add(gridItem)
        Next

        gridHistorySource.ResetBindings(False)

        btnEdit.Enabled = True
        btnDelete.Enabled = True

    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        OpenFieldsToEdition()
        partNumberId = 0
        btnClear.Enabled = True
        inEditMode = True
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        If dgvPartNumbers.SelectedRows.Count = 0 Then Return

        OpenFieldsToEdition()
        inEditMode = True
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If dgvPartNumbers.SelectedRows.Count = 0 Or partNumberId = 0 Then Return

        If Not MessageBox.Show($"The Part Number selected will be permanently deleted! {vbNewLine}{vbNewLine}Confirm?", "Delete Part Number", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) = DialogResult.Yes Then Return

        Dim sqlCmd = $"DELETE FROM `csi_database`.`tbl_masterpartnumbers` WHERE (`Id` = {partNumberId});"

        MySqlAccess.ExecuteNonQuery(sqlCmd, connString)

        dgvPartNumbers.Rows.RemoveAt(dgvPartNumbers.SelectedRows(0).Index)

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CloseFields()
        btnClear.Enabled = False
        inEditMode = False

        dgvPartNumbers_SelectionChanged(Me, New EventArgs())

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If String.IsNullOrEmpty(txtPartNumber.Text) Or cmbMachines.SelectedIndex < 0 Or String.IsNullOrEmpty(txtOperation.Text) Then Return

        Dim partNumber = txtPartNumber.Text
        Dim machineId = cmbMachines.SelectedValue
        Dim operation = txtOperation.Text
        Dim cycleTime = IIf(IsNumeric(txtCycleTime.Text), txtCycleTime.Text, 0)
        Dim setupTime = IIf(IsNumeric(txtSetupTime.Text), txtSetupTime.Text, 0)
        Dim loadTime = IIf(IsNumeric(txtLoadTime.Text), txtLoadTime.Text, 0)
        Dim cycleMultiplier = IIf(IsNumeric(txtCycleMultiplier.Text), txtCycleMultiplier.Text, 1)
        Dim cycleTimeScale = cmbCycleTimeScale.Text.Substring(0, 1).ToLower()
        Dim setupTimeScale = cmbSetupTimeScale.Text.Substring(0, 1).ToLower()
        Dim loadTimeScale = cmbLoadTimeScale.Text.Substring(0, 1).ToLower()

        If cycleTimeScale = "m" Then cycleTime = cycleTime * 60
        If cycleTimeScale = "h" Then cycleTime = cycleTime * 3600

        If setupTimeScale = "m" Then setupTime = setupTime * 60
        If setupTimeScale = "h" Then setupTime = setupTime * 3600

        If loadTimeScale = "m" Then loadTime = loadTime * 60
        If loadTimeScale = "h" Then loadTime = loadTime * 3600

        Dim _partNumber = ""
        Dim _machineId = 0
        Dim _operation = ""
        Dim regExists = False
        For Each row As DataGridViewRow In dgvPartNumbers.Rows
            _partNumber = row.Cells("pncPartNumber").Value.ToString().ToUpper()
            _machineId = row.Cells("pncMachineId").Value
            _operation = row.Cells("pncOperation").Value.ToString().ToUpper()
            If _partNumber = partNumber.ToUpper() And _machineId = machineId And _operation = operation Then
                regExists = True
                Exit For
            End If
        Next

        Dim GridItem = New PartNumber
        GridItem.MachineId = machineId
        GridItem.MachineName = cmbMachines.Text
        GridItem.PartNumber = partNumber
        GridItem.Operation = operation
        GridItem.CycleTimeSec = cycleTime
        GridItem.CycleMultiplier = cycleMultiplier
        GridItem.CreatedBy = CSIFLEXGlobal.UserName
        GridItem.CreatedWhen = DateTime.Today().ToString("yyyy-MM-dd")
        GridItem.LoadTimeSec = loadTime
        GridItem.SetupTimeSec = setupTime
        GridItem.CycleTimeScale = cycleTimeScale
        GridItem.SetupTimeScale = setupTimeScale
        GridItem.LoadTimeScale = loadTimeScale

        Dim query As StringBuilder = New StringBuilder()
        Try
            If partNumberId = 0 And Not regExists Then
                query.Append($"INSERT INTO                                      ")
                query.Append($" csi_database.tbl_masterpartnumbers              ")
                query.Append($" (                                               ")
                query.Append($"     PartNumber,                                 ")
                query.Append($"     MachineId,                                  ")
                query.Append($"     Operation,                                  ")
                query.Append($"     CycleTime,                                  ")
                query.Append($"     DisplayCycleTimeScale,                      ")
                query.Append($"     CycleMultiplier,                            ")
                query.Append($"     SetupTime,                                  ")
                query.Append($"     DisplaySetupTimeScale,                      ")
                query.Append($"     PartLoad,                                   ")
                query.Append($"     DisplayPartLoadTimeScale,                   ")
                query.Append($"     CreatedWhen,                                ")
                query.Append($"     CreatedBy                                   ")
                query.Append($" )                                               ")
                query.Append($" VALUES                                          ")
                query.Append($" (                                               ")
                query.Append($"     '{partNumber}',                             ")
                query.Append($"     '{machineId}',                              ")
                query.Append($"     '{operation}',                              ")
                query.Append($"     '{cycleTime}',                              ")
                query.Append($"     '{cycleTimeScale}',                         ")
                query.Append($"     '{cycleMultiplier}',                        ")
                query.Append($"     '{setupTime}',                              ")
                query.Append($"     '{setupTimeScale}',                         ")
                query.Append($"     '{loadTime}',                               ")
                query.Append($"     '{loadTimeScale}',                          ")
                query.Append($"     '{DateTime.Today().ToString("yyyy-MM-dd")}',")
                query.Append($"     '{CSIFLEXGlobal.UserId}'                    ")
                query.Append($" )                                               ")

                Dim id = MySqlAccess.ExecuteNonQuery(query.ToString(), connString)
                GridItem.Id = id

                dgvPartNumbers.Rows.Add(GridItem.Id,
                                    GridItem.PartNumber,
                                    GridItem.MachineId,
                                    GridItem.MachineName,
                                    GridItem.Operation,
                                    GridItem.CycleTime,
                                    GridItem.CycleMultiplier,
                                    GridItem.SetupTime,
                                    GridItem.LoadTime,
                                    GridItem.CreatedBy,
                                    GridItem.CreatedWhen,
                                    GridItem.CycleTimeScale,
                                    GridItem.SetupTimeScale,
                                    GridItem.LoadTimeScale,
                                    GridItem.CycleTimeSec,
                                    GridItem.SetupTimeSec,
                                    GridItem.LoadTimeSec)

            ElseIf partNumberId > 0 And regExists Then
                query.Append($"UPDATE csi_database.tbl_masterpartnumbers                     ")
                query.Append($" SET                                                          ")
                query.Append($"     PartNumber               = '{GridItem.PartNumber}',      ")
                query.Append($"     MachineId                = '{GridItem.MachineId}',       ")
                query.Append($"     Operation                = '{GridItem.Operation}',       ")
                query.Append($"     CycleTime                = '{GridItem.CycleTimeSec}',    ")
                query.Append($"     DisplayCycleTimeScale    = '{GridItem.CycleTimeScale}',  ")
                query.Append($"     CycleMultiplier          = '{GridItem.CycleMultiplier}', ")
                query.Append($"     SetupTime                = '{GridItem.SetupTimeSec}',    ")
                query.Append($"     DisplaySetupTimeScale    = '{GridItem.SetupTimeScale}',  ")
                query.Append($"     PartLoad                 = '{GridItem.LoadTimeSec}',     ")
                query.Append($"     DisplayPartLoadTimeScale = '{GridItem.LoadTimeScale}'    ")
                query.Append($" WHERE                                                        ")
                query.Append($"     Id = {partNumberId}                                      ")

                MySqlAccess.ExecuteNonQuery(query.ToString(), connString)

                Dim _partNumberId = 0
                For Each row As DataGridViewRow In dgvPartNumbers.Rows
                    _partNumberId = row.Cells("pncId").Value
                    If _partNumberId = partNumberId Then
                        row.Cells("pncPartNumber").Value = GridItem.PartNumber
                        row.Cells("pncMachineId").Value = GridItem.MachineId
                        row.Cells("pncMachineName").Value = GridItem.MachineName
                        row.Cells("pncOperation").Value = GridItem.Operation
                        row.Cells("pncCycleTime").Value = GridItem.CycleTime
                        row.Cells("pncCycleMultiplier").Value = GridItem.CycleMultiplier
                        row.Cells("pncSetupTime").Value = GridItem.SetupTime
                        row.Cells("pncLoadTime").Value = GridItem.LoadTime
                        row.Cells("pncCycleTimeScale").Value = GridItem.CycleTimeScale
                        row.Cells("pncSetupTimeScale").Value = GridItem.SetupTimeScale
                        row.Cells("pncLoadTimeScale").Value = GridItem.LoadTimeScale
                        row.Cells("pncCycleTimeSec").Value = GridItem.CycleTimeSec
                        row.Cells("pncSetupTimeSec").Value = GridItem.SetupTimeSec
                        row.Cells("pncLoadTimeSec").Value = GridItem.LoadTimeSec
                        Exit For
                    End If
                Next
            Else
                If partNumberId = 0 Then MessageBox.Show("Part Number, Machine and Operation already exists!")
            End If

        Catch ex As Exception
            Log.Error(ex)
        End Try

        btnClear.Enabled = False
        CloseFields()
        inEditMode = False
    End Sub

    Private Sub OpenFieldsToEdition()
        txtPartNumber.ReadOnly = False
        txtOperation.ReadOnly = False
        txtCycleTime.ReadOnly = False
        txtSetupTime.ReadOnly = False
        txtLoadTime.ReadOnly = False
        txtCycleMultiplier.ReadOnly = False

        cmbMachines.Enabled = True
        cmbCycleTimeScale.Enabled = True
        cmbSetupTimeScale.Enabled = True
        cmbLoadTimeScale.Enabled = True

        btnNew.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = False
        btnCancel.Enabled = True
        btnSave.Enabled = True

        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 2
        btnSave.BackColor = Color.LightGray
    End Sub

    Private Sub CloseFields()
        txtPartNumber.ReadOnly = True
        txtOperation.ReadOnly = True
        txtCycleTime.ReadOnly = True
        txtSetupTime.ReadOnly = True
        txtLoadTime.ReadOnly = True
        txtCycleMultiplier.ReadOnly = True

        cmbMachines.Enabled = False
        cmbCycleTimeScale.Enabled = False
        cmbSetupTimeScale.Enabled = False
        cmbLoadTimeScale.Enabled = False

        btnNew.Enabled = True
        btnEdit.Enabled = True
        btnDelete.Enabled = True
        btnCancel.Enabled = False
        btnSave.Enabled = False

        btnSave.FlatStyle = FlatStyle.Standard
    End Sub

    Private Sub ClearFields(sender As Object, e As EventArgs) Handles btnClear.Click
        txtPartNumber.Clear()
        txtOperation.Clear()
        txtCycleTime.Text = 0
        txtSetupTime.Text = 0
        txtLoadTime.Text = 0
        txtCycleMultiplier.Text = 1
        cmbMachines.SelectedIndex = -1
        cmbCycleTimeScale.SelectedIndex = 0
        cmbSetupTimeScale.SelectedIndex = 0
        cmbLoadTimeScale.SelectedIndex = 0
    End Sub

    Private Sub OnlyNumbers_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtCycleTime.KeyPress, txtSetupTime.KeyPress, txtLoadTime.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub SavePartNumber()

    End Sub

End Class

Public Class PartNumber
    Private _pnId As Integer
    Public Property Id() As Integer
        Get
            Return _pnId
        End Get
        Set(ByVal value As Integer)
            _pnId = value
        End Set
    End Property

    Private _partNumber As String
    Public Property PartNumber() As String
        Get
            Return _partNumber
        End Get
        Set(ByVal value As String)
            _partNumber = value
        End Set
    End Property

    Private _machineId As Integer
    Public Property MachineId() As Integer
        Get
            Return _machineId
        End Get
        Set(ByVal value As Integer)
            _machineId = value
        End Set
    End Property

    Private _machineName As String
    Public Property MachineName() As String
        Get
            Return _machineName
        End Get
        Set(ByVal value As String)
            _machineName = value
        End Set
    End Property

    Private _operation As String
    Public Property Operation() As String
        Get
            Return _operation
        End Get
        Set(ByVal value As String)
            _operation = value
        End Set
    End Property

    Private _cycleTimeSec As Integer = 0
    Public Property CycleTimeSec() As Integer
        Get
            Return _cycleTimeSec
        End Get
        Set(ByVal value As Integer)
            _cycleTimeSec = value
        End Set
    End Property

    Private _cycleTimeScale As String
    Public Property CycleTimeScale() As String
        Get
            Return _cycleTimeScale
        End Get
        Set(ByVal value As String)
            _cycleTimeScale = value
        End Set
    End Property

    Public ReadOnly Property CycleTime() As String
        Get
            Dim time As TimeSpan = New TimeSpan(0, 0, _cycleTimeSec)
            Return time.ToString()
        End Get
    End Property

    Private _cycleMultiplier As Integer = 0
    Public Property CycleMultiplier() As Integer
        Get
            Return _cycleMultiplier
        End Get
        Set(ByVal value As Integer)
            _cycleMultiplier = value
        End Set
    End Property

    Private _setupTimeSec As Integer = 0
    Public Property SetupTimeSec() As Integer
        Get
            Return _setupTimeSec
        End Get
        Set(ByVal value As Integer)
            _setupTimeSec = value
        End Set
    End Property

    Private _setupTimeScale As String
    Public Property SetupTimeScale() As String
        Get
            Return _setupTimeScale
        End Get
        Set(ByVal value As String)
            _setupTimeScale = value
        End Set
    End Property

    Public ReadOnly Property SetupTime() As String
        Get
            Dim time As TimeSpan = New TimeSpan(0, 0, _setupTimeSec)
            Return time.ToString()
        End Get
    End Property

    Private _loadTimeSec As Integer = 0
    Public Property LoadTimeSec() As Integer
        Get
            Return _loadTimeSec
        End Get
        Set(ByVal value As Integer)
            _loadTimeSec = value
        End Set
    End Property

    Private _loadTimeScale As String
    Public Property LoadTimeScale() As String
        Get
            Return _loadTimeScale
        End Get
        Set(ByVal value As String)
            _loadTimeScale = value
        End Set
    End Property

    Public ReadOnly Property LoadTime() As String
        Get
            Dim time As TimeSpan = New TimeSpan(0, 0, _loadTimeSec)
            Return time.ToString()
        End Get
    End Property

    Private _createdBy As String
    Public Property CreatedBy() As String
        Get
            Return _createdBy
        End Get
        Set(ByVal value As String)
            _createdBy = value
        End Set
    End Property

    Private _createdWhen As String
    Public Property CreatedWhen() As String
        Get
            Return _createdWhen
        End Get
        Set(ByVal value As String)
            _createdWhen = value
        End Set
    End Property
End Class
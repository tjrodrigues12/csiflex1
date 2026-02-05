Imports CSIFLEX.Database.Access
Imports CSIFLEX.eNetLibrary
Imports CSIFLEX.eNetLibrary.Data
Imports CSIFLEX.Server.Library
Imports CSIFLEX.Utilities

Public Class TimelineEditControl

    Dim machines As List(Of Tuple(Of Integer, String, String))
    Dim listMachineStatus As List(Of TimelineChange)
    Dim listChanges As List(Of TimelineChange)

    Dim statusList As List(Of String)
    Dim enetMachName As String
    Dim maxValue As Integer
    Dim selectedTimeId As Integer
    Dim machineId As Integer

    Dim regBefore As TimelineChange
    Dim regToEdit As TimelineChange
    Dim regAfter As TimelineChange

    Dim minTimeStart As DateTime
    Dim maxTimeStart As DateTime
    Dim minTimeEnd As DateTime
    Dim maxTimeEnd As DateTime

    Dim isChanging As Boolean = False
    Dim editMode As Boolean = False
    Dim insertMode As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Reset()

    End Sub


    Public Sub Reset()

        Try
            machines = New List(Of Tuple(Of Integer, String, String))()
            listChanges = New List(Of TimelineChange)()

            RemoveHandler cmbMachines.SelectedIndexChanged, AddressOf cmbMachines_SelectedIndexChanged

            Dim tableMachines As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1")

            For Each rowMachine As DataRow In tableMachines.Rows
                machines.Add(Tuple.Create(Integer.Parse(rowMachine("Id").ToString()), rowMachine("machine_name").ToString(), rowMachine("EnetMachineName").ToString()))
            Next

            cmbMachines.DataSource = machines
            cmbMachines.DisplayMember = "Item2"
            cmbMachines.ValueMember = "Item3"
            cmbMachines.SelectedIndex = -1

            DateTimePicker.Value = Today.AddDays(-1)
            dgStatus.DataSource = Nothing

            lblInfo.ResetText()

            lblLimitStartMin.ResetText()
            lblLimitStartMax.ResetText()
            lblLimitEndMin.ResetText()
            lblLimitEndMax.ResetText()

            AddHandler cmbMachines.SelectedIndexChanged, AddressOf cmbMachines_SelectedIndexChanged

        Catch ex As Exception

            Log.Error(ex)

        End Try

    End Sub


    Private Sub cmbMachines_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMachines.SelectedIndexChanged, DateTimePicker.ValueChanged

        enetMachName = ""

        If listMachineStatus IsNot Nothing Then
            listMachineStatus = New List(Of TimelineChange)()
            LoadStatusDatagrid(listMachineStatus)
        End If

        If cmbMachines.SelectedIndex < 0 Then Return

        enetMachName = cmbMachines.SelectedValue

        machineId = DirectCast(cmbMachines.SelectedItem, Tuple(Of Integer, String, String)).Item1

        Dim enetMachine As eNetMachineConfig = eNetServer.Machines.FirstOrDefault(Function(m) m.MachineName = enetMachName)

        Dim department As String = enetMachine.Department

        Dim dayOfWeek As DayOfWeek = DateTimePicker.Value.DayOfWeek

        Dim shifts = eNetServer.Departments.FirstOrDefault(Function(d) d.Name = department).WeekShifts(dayOfWeek).Select(Function(s) s.Number).ToList()

        cmbShift.DataSource = shifts
        cmbShift.SelectedIndex = 0

        statusList = enetMachine.CommandsAvailable().Select(Function(i) i.Key).ToList()
        statusList.Remove("PRODUCTION")
        cmbStatus.DataSource = statusList
        cmbStatus.SelectedIndex = -1

        RemoveHandler cmbChanges.SelectedIndexChanged, AddressOf cmbChanges_SelectedIndexChanged

        Dim changes = New List(Of Tuple(Of Integer, String, String))()
        Dim tblChanges = MySqlAccess.GetDataTable($"SELECT Id, AdjustDate, Shift FROM csi_database.tbl_adjustment WHERE Machine = '{ enetMachName }'")

        For Each rowChange As DataRow In tblChanges.Rows
            Dim adjustDate As DateTime = rowChange("AdjustDate")
            changes.Add(Tuple.Create(Integer.Parse(rowChange("Id").ToString()), $"{adjustDate.ToString("MM-dd-yyyy")} - Shift: {rowChange("Shift").ToString()}", rowChange("Shift").ToString()))
        Next

        cmbChanges.DataSource = changes
        cmbChanges.ValueMember = "Item1"
        cmbChanges.DisplayMember = "Item2"
        cmbChanges.SelectedIndex = -1
        cmbChanges.ResetText()

        AddHandler cmbChanges.SelectedIndexChanged, AddressOf cmbChanges_SelectedIndexChanged

    End Sub


    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click

        If cmbMachines.SelectedIndex < 0 Then Return

        If listChanges.Count > 0 Then

            If Not MessageBox.Show("Attention!! You didn't save the changes in the timeline. If you continue you will lose the changes. \nDo you want to continue?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3) = DialogResult.Yes Then
                Return
            End If

        End If

        maxValue = 0
        Dim tableName As String = Support.GetMachineTableName(enetMachName)
        Dim dateStart As String = DateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss")
        Dim dateEnd As String = DateTimePicker.Value.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
        Dim shift As String = cmbShift.Text
        Dim timeIdAdj As Integer = 0

        listMachineStatus = New List(Of TimelineChange)

        Dim tableStatusChanges As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.{tableName} WHERE Date_ >= '{dateStart}' AND Date_ < '{dateEnd}' AND shift = {shift}")

        If tableStatusChanges.Rows.Count > 0 Then

            Dim startDateTime As DateTime = tableStatusChanges.Rows(0)("Date_")

            For Each rowChange As DataRow In tableStatusChanges.Rows

                Dim change As TimelineChange = New TimelineChange()

                Dim datetimeChange As DateTime = rowChange("Date_")

                If datetimeChange < startDateTime Then
                    datetimeChange = datetimeChange.AddDays(1)
                    timeIdAdj = 86400
                Else
                    timeIdAdj = 0
                End If

                change.Selected = False
                change.TimeId = datetimeChange.TimeOfDay.TotalSeconds + timeIdAdj
                change.Status = rowChange("status")
                change.OriginalStatus = rowChange("status")
                change.Cycletime = Integer.Parse(rowChange("cycletime").ToString())
                change.OriginalCycletime = Integer.Parse(rowChange("cycletime").ToString())
                change.Shift = Integer.Parse(rowChange("shift").ToString())
                change.TimeStart = datetimeChange
                change.OriginalDate = DateTime.Parse(rowChange("Date_").ToString())
                change.TimeEnd = datetimeChange.AddSeconds(change.Cycletime)

                If Not change.Status.StartsWith("_") Then

                    If change.Cycletime > maxValue Then maxValue = change.Cycletime

                    listMachineStatus.Add(change)

                End If
            Next

            LoadStatusDatagrid(listMachineStatus)

            btnStartChange.Enabled = True

        End If

    End Sub


    Private Sub LoadStatusDatagrid(machineStatusList As List(Of TimelineChange))

        dgStatus.DataSource = Nothing
        dgStatus.Rows.Clear()
        dgStatus.DataSource = machineStatusList.OrderBy(Function(x) x.TimeId).ToList()
        dgStatus.Refresh()

        'dgStatus.Columns("OriginalDate").Visible = False
        'dgStatus.Columns("HasBefore").Visible = False
        'dgStatus.Columns("HasAfter").Visible = False
        'dgStatus.Columns("Action").Visible = False
        'dgStatus.Columns("OriginalStatus").Visible = False
        'dgStatus.Columns("OriginalCycletime").Visible = False

        HideAllColumns()

        dgStatus.Columns("Selected").DisplayIndex = 0
        dgStatus.Columns("Selected").Width = 60
        dgStatus.Columns("Selected").HeaderText = ""
        dgStatus.Columns("Selected").Visible = True

        dgStatus.Columns("Status").DisplayIndex = 1
        dgStatus.Columns("Status").Width = 120
        dgStatus.Columns("Status").HeaderText = "Status"
        dgStatus.Columns("Status").Visible = True

        dgStatus.Columns("Shift").DisplayIndex = 2
        dgStatus.Columns("Shift").Width = 60
        dgStatus.Columns("Shift").HeaderText = "Shift"
        dgStatus.Columns("Shift").Visible = True

        dgStatus.Columns("TimeId").DisplayIndex = 3
        dgStatus.Columns("TimeId").Width = 100
        dgStatus.Columns("TimeId").HeaderText = "Time Id"
        dgStatus.Columns("TimeId").Visible = True

        dgStatus.Columns("TimeStart").DisplayIndex = 4
        dgStatus.Columns("TimeStart").Width = 180
        dgStatus.Columns("TimeStart").HeaderText = "Start"
        dgStatus.Columns("TimeStart").DefaultCellStyle.Format = "MM-dd-yyyy HH:mm:ss"
        dgStatus.Columns("TimeStart").Visible = True

        dgStatus.Columns("TimeEnd").DisplayIndex = 5
        dgStatus.Columns("TimeEnd").Width = 180
        dgStatus.Columns("TimeEnd").HeaderText = "End"
        dgStatus.Columns("TimeEnd").DefaultCellStyle.Format = "MM-dd-yyyy HH:mm:ss"
        dgStatus.Columns("TimeEnd").Visible = True

        dgStatus.Columns("Cycletime").DisplayIndex = 6
        dgStatus.Columns("Cycletime").Width = 80
        dgStatus.Columns("Cycletime").HeaderText = "Cycletime"
        dgStatus.Columns("Cycletime").Visible = True

        dgStatus.Columns("Image").DisplayIndex = 7
        dgStatus.Columns("Image").Width = 300
        dgStatus.Columns("Image").HeaderText = ""
        dgStatus.Columns("Image").Visible = True

        dgStatusFillBars()

        btnSaveChange.Enabled = listChanges.Count > 0 And txtDescription.Text.Length > 10
        btnCancelChange.Enabled = listChanges.Count > 0 Or txtDescription.Text.Length > 0

    End Sub


    Private Sub cmbChanges_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChanges.SelectedIndexChanged

        If cmbChanges.SelectedIndex < 0 Then Return

        txtDescription.ResetText()
        lblInfo.ResetText()

        listMachineStatus = New List(Of TimelineChange)()

        Dim changeId As Integer = cmbChanges.SelectedValue

        Dim sqlCmd As Text.StringBuilder = New System.Text.StringBuilder()
        sqlCmd.Append($"SELECT                            ")
        sqlCmd.Append($"	A.Id                        , ")
        sqlCmd.Append($"	A.Machine                   , ")
        sqlCmd.Append($"	A.AdjustDate                , ")
        sqlCmd.Append($"	A.Shift                     , ")
        sqlCmd.Append($"	A.Description               , ")
        sqlCmd.Append($"	A.CreatedWhen               , ")
        sqlCmd.Append($"	A.CreatedBy                 , ")
        sqlCmd.Append($"    R.Id RowId                  , ")
        sqlCmd.Append($"    R.Action                    , ")
        sqlCmd.Append($"    R.Date                      , ")
        sqlCmd.Append($"    R.NewDate                   , ")
        sqlCmd.Append($"    R.Status                    , ")
        sqlCmd.Append($"    R.NewStatus                 , ")
        sqlCmd.Append($"    R.Cycletime                 , ")
        sqlCmd.Append($"    R.NewCycletime                ")
        sqlCmd.Append($" FROM                             ")
        sqlCmd.Append($"	csi_database.tbl_adjustment A ")
        sqlCmd.Append($"	INNER JOIN csi_database.tbl_adjust_rows R ON A.Id = R.AdjustmentId ")
        sqlCmd.Append($" WHERE A.Id = { changeId }        ")
        sqlCmd.Append($" ORDER BY R.Date                  ")

        Dim tblChange = MySqlAccess.GetDataTable(sqlCmd.ToString())
        Dim change As TimelineChange

        If tblChange.Rows.Count > 0 Then
            lblInfo.Text = $"Changed by { tblChange.Rows(0)("CreatedBy") } on { tblChange.Rows(0)("CreatedWhen") }"
            txtDescription.Text = tblChange.Rows(0)("Description")
        End If

        For Each rowChange In tblChange.Rows

            change = New TimelineChange()
            change.Action = IIf(rowChange("Action") = "E", "Edited", IIf(rowChange("Action") = "D", "Deleted", "Inserted"))
            change.Shift = Integer.Parse(rowChange("shift").ToString())

            change.Status = rowChange("NewStatus")
            change.OriginalStatus = rowChange("status")

            change.TimeStart = rowChange("NewDate")
            change.OriginalDate = rowChange("Date")

            change.Cycletime = rowChange("NewCycletime")
            change.OriginalCycletime = rowChange("cycletime")

            listMachineStatus.Add(change)
        Next

        dgStatus.DataSource = Nothing
        dgStatus.Rows.Clear()
        dgStatus.DataSource = listMachineStatus.OrderBy(Function(x) x.TimeStart).ToList()
        dgStatus.Refresh()

        HideAllColumns()

        dgStatus.Columns("Action").DisplayIndex = 0
        dgStatus.Columns("Action").Width = 60
        dgStatus.Columns("Action").HeaderText = "Action"
        dgStatus.Columns("Action").Visible = True

        dgStatus.Columns("OriginalStatus").DisplayIndex = 1
        dgStatus.Columns("OriginalStatus").Width = 120
        dgStatus.Columns("OriginalStatus").HeaderText = "Status"
        dgStatus.Columns("OriginalStatus").Visible = True

        dgStatus.Columns("Status").DisplayIndex = 2
        dgStatus.Columns("Status").Width = 120
        dgStatus.Columns("Status").HeaderText = "New Status"
        dgStatus.Columns("Status").Visible = True

        dgStatus.Columns("OriginalDate").DisplayIndex = 3
        dgStatus.Columns("OriginalDate").Width = 150
        dgStatus.Columns("OriginalDate").HeaderText = "Start"
        dgStatus.Columns("OriginalDate").DefaultCellStyle.Format = "MM-dd-yyyy HH:mm:ss"
        dgStatus.Columns("OriginalDate").Visible = True

        dgStatus.Columns("TimeStart").DisplayIndex = 4
        dgStatus.Columns("TimeStart").Width = 150
        dgStatus.Columns("TimeStart").HeaderText = "New Start"
        dgStatus.Columns("TimeStart").DefaultCellStyle.Format = "MM-dd-yyyy HH:mm:ss"
        dgStatus.Columns("TimeStart").Visible = True

        dgStatus.Columns("OriginalCycletime").DisplayIndex = 5
        dgStatus.Columns("OriginalCycletime").Width = 80
        dgStatus.Columns("OriginalCycletime").HeaderText = "Cycletime"
        dgStatus.Columns("OriginalCycletime").Visible = True

        dgStatus.Columns("Cycletime").DisplayIndex = 6
        dgStatus.Columns("Cycletime").Width = 140
        dgStatus.Columns("Cycletime").HeaderText = "New Cycletime"
        dgStatus.Columns("Cycletime").Visible = True

        dgStatus.Columns("Image").DisplayIndex = 7
        dgStatus.Columns("Image").Width = 300
        dgStatus.Columns("Image").HeaderText = ""
        dgStatus.Columns("Image").Visible = True

        dgStatusFillBars()

    End Sub


    Private Sub HideAllColumns()

        For Each column As DataGridViewColumn In dgStatus.Columns
            column.Visible = False
        Next

    End Sub


    Private Sub dgStatusFillBars()

        Dim action As String
        Dim status As String
        Dim cycletime As Integer
        Dim statusImage As Image
        Dim imageSize As Integer

        If dgStatus.Rows.Count = 0 Then Return

        maxValue = listMachineStatus.Select(Function(m) m.Cycletime).Max()

        For Each row As DataGridViewRow In dgStatus.Rows

            status = row.Cells("Status").Value
            action = row.Cells("Action").Value

            If Not String.IsNullOrEmpty(status) Then

                cycletime = row.Cells("Cycletime").Value

                imageSize = Int((cycletime * 250) / maxValue)

                If imageSize > 0 Then
                    Select Case status
                        Case "CYCLE ON"
                            statusImage = New Bitmap(ImageList.Images("GreenBar"), imageSize, 18)
                        Case "CYCLE OFF"
                            statusImage = New Bitmap(ImageList.Images("RedBar"), imageSize, 18)
                        Case "SETUP"
                            statusImage = New Bitmap(ImageList.Images("BlueBar"), imageSize, 18)
                        Case Else
                            statusImage = New Bitmap(ImageList.Images("YellowBar"), imageSize, 18)
                    End Select

                    row.Cells("Image").Value = statusImage
                Else
                    row.Cells("Image").Value = New Bitmap(ImageList.Images("WhiteBar"), 10, 18)
                End If

                Select Case action
                    Case "Edited"
                        row.Cells("Action").Style.BackColor = Color.Yellow
                    Case "Inserted"
                        row.Cells("Action").Style.BackColor = Color.LightGreen
                    Case "Deleted"
                        row.Cells("Action").Style.BackColor = Color.Red
                End Select
            End If
        Next

    End Sub


    Private Sub dgStatus_SelectionChanged(sender As Object, e As EventArgs) Handles dgStatus.SelectionChanged

        If dgStatus.SelectedRows.Count = 0 Or dgStatus.Columns("Action").Visible Then Return

        Dim row As DataGridViewRow = dgStatus.SelectedRows(0)

        selectedTimeId = row.Cells("TimeId").Value
        timePickerStart.Value = row.Cells("TimeStart").Value
        timePickerEnd.Value = row.Cells("TimeEnd").Value
        cmbStatus.SelectedItem = row.Cells("Status").Value

        If Not isChanging Then
            Return
        End If

        If insertMode Then
            btnNew_Click(Me, New EventArgs())
            Return
        End If

        lblInformation.Text = "Display Mode"
        lblLimitStartMin.ResetText()
        lblLimitStartMax.ResetText()
        lblLimitEndMin.ResetText()
        lblLimitEndMax.ResetText()

        cmbStatus.Enabled = False
        timePickerStart.Enabled = False
        timePickerEnd.Enabled = False

        btnEdit.Enabled = True
        btnNew.Enabled = True
        btnApply.Enabled = False
        btnCancel.Enabled = False

        editMode = False
        insertMode = False

    End Sub


    Private Sub dgStatus_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgStatus.CellBeginEdit

        Dim row = e.RowIndex
        Dim col = e.ColumnIndex

        If row < 0 Or col <> dgStatus.Columns("Selected").Index Then
            Return
        End If

        If Not isChanging Then
            e.Cancel = True
            Return
        End If

        If row = 0 Then
            MessageBox.Show("The first record cannot be deleted!", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
        End If

    End Sub


    Private Sub dgStatus_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgStatus.RowValidating

        If editMode Then
            e.Cancel = True
            Return
        End If

    End Sub


    Private Sub btnStartChange_Click(sender As Object, e As EventArgs) Handles btnStartChange.Click

        isChanging = True

        txtDescription.Enabled = True
        txtDescription.ReadOnly = False

        EnableEditButtons(True)

        listChanges = New List(Of TimelineChange)()

    End Sub


    Private Sub btnSaveChange_Click(sender As Object, e As EventArgs) Handles btnSaveChange.Click


        If Not MessageBox.Show("Do you confirm the changes in the timeline?", "Confirmation!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) = DialogResult.Yes Then
            Return
        End If

        Dim sqlCmd As Text.StringBuilder = New System.Text.StringBuilder()
        Dim adjId As Integer

        Dim tableName As String = Support.GetMachineTableName(enetMachName)

        Dim adjustment As Integer = 0
        If timePickerStart.Value.Date > DateTimePicker.Value.Date Then adjustment = -1

        sqlCmd.Append($"INSERT INTO csi_database.tbl_adjustment")
        sqlCmd.Append($" (                                     ")
        sqlCmd.Append($"    MachineId                        , ")
        sqlCmd.Append($"    Machine                          , ")
        sqlCmd.Append($"    AdjustDate                       , ")
        sqlCmd.Append($"    Shift                            , ")
        sqlCmd.Append($"    Description                      , ")
        sqlCmd.Append($"    CreatedWhen                      , ")
        sqlCmd.Append($"    CreatedBy                          ")
        sqlCmd.Append($" ) VALUES (                            ")
        sqlCmd.Append($"     { machineId }                   , ")
        sqlCmd.Append($"    '{ enetMachName }'               , ")
        sqlCmd.Append($"    '{ DateTimePicker.Value.Date.ToString("yyyy-MM-dd") }', ")
        sqlCmd.Append($"     { cmbShift.Text }               , ")
        sqlCmd.Append($"    '{ txtDescription.Text }'        , ")
        sqlCmd.Append($"    '{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }'    , ")
        sqlCmd.Append($"    '{ CSIFLEXGlobal.UserName }'       ")
        sqlCmd.Append($" );                                    ")
        sqlCmd.Append($" SELECT LAST_INSERT_ID();              ")

        adjId = MySqlAccess.ExecuteScalar(sqlCmd.ToString())

        sqlCmd.Clear()

        For Each change As TimelineChange In listChanges

            If change.Action = "E" Then

                change.TimeStart = change.TimeStart.AddDays(adjustment)

                sqlCmd.Append($"UPDATE csi_database.{ tableName } SET    ")
                sqlCmd.Append($"    time_     = '{ change.TimeStart.ToString("yyyy-MM-dd HH:mm:ss") }', ")
                sqlCmd.Append($"    Date_     = '{ change.TimeStart.ToString("yyyy-MM-dd HH:mm:ss") }', ")
                sqlCmd.Append($"    Status    = '{ change.Status }',     ")
                sqlCmd.Append($"    Cycletime =  { change.Cycletime }    ")
                sqlCmd.Append($" WHERE                                   ")
                sqlCmd.Append($"    Date_ = '{ change.OriginalDate.ToString("yyyy-MM-dd HH:mm:ss") }';  ")

            ElseIf change.Action = "I" Then

                change.OriginalDate = change.TimeStart.AddDays(adjustment)
                change.TimeStart = change.TimeStart.AddDays(adjustment)

                sqlCmd.Append($"INSERT INTO csi_database.{ tableName } ")
                sqlCmd.Append($" (                        ")
                sqlCmd.Append($"    month_              , ")
                sqlCmd.Append($"    day_                , ")
                sqlCmd.Append($"    year_               , ")
                sqlCmd.Append($"    time_               , ")
                sqlCmd.Append($"    Date_               , ")
                sqlCmd.Append($"    status              , ")
                sqlCmd.Append($"    shift               , ")
                sqlCmd.Append($"    cycletime           , ")
                sqlCmd.Append($"    partNumber            ")
                sqlCmd.Append($" ) VALUES (               ")
                sqlCmd.Append($"     { change.TimeStart.Month }, ")
                sqlCmd.Append($"     { change.TimeStart.Day }  , ")
                sqlCmd.Append($"     { change.TimeStart.Year } , ")
                sqlCmd.Append($"    '{ change.TimeStart.ToString("yyyy-MM-dd HH:mm:ss") }', ")
                sqlCmd.Append($"    '{ change.TimeStart.ToString("yyyy-MM-dd HH:mm:ss") }', ")
                sqlCmd.Append($"    '{ change.Status }'        , ")
                sqlCmd.Append($"     { change.Shift }          , ")
                sqlCmd.Append($"     { change.Cycletime }      , ")
                sqlCmd.Append($"    ''                           ")
                sqlCmd.Append($" );                              ")

            ElseIf change.Action = "D" Then

                change.TimeStart = change.OriginalDate

                sqlCmd.Append($"DELETE FROM csi_database.{ tableName }     ")
                sqlCmd.Append($" WHERE                                     ")
                sqlCmd.Append($"    Date_ = '{ change.OriginalDate.ToString("yyyy-MM-dd HH:mm:ss") }'; ")

            End If

            sqlCmd.Append($"INSERT INTO csi_database.tbl_adjust_rows")
            sqlCmd.Append($" (                                 ")
            sqlCmd.Append($"    AdjustmentId                 , ")
            sqlCmd.Append($"    Action                       , ")
            sqlCmd.Append($"    Machine                      , ")
            sqlCmd.Append($"    Date                         , ")
            sqlCmd.Append($"    NewDate                      , ")
            sqlCmd.Append($"    Status                       , ")
            sqlCmd.Append($"    NewStatus                    , ")
            sqlCmd.Append($"    Cycletime                    , ")
            sqlCmd.Append($"    NewCycletime                   ")
            sqlCmd.Append($" ) VALUES (                        ")
            sqlCmd.Append($"     { adjId }                   , ")
            sqlCmd.Append($"    '{ change.Action }'          , ")
            sqlCmd.Append($"    '{ enetMachName }'           , ")
            sqlCmd.Append($"    '{ change.OriginalDate.ToString("yyyy-MM-dd HH:mm:ss") }' , ")
            sqlCmd.Append($"    '{ change.TimeStart.ToString("yyyy-MM-dd HH:mm:ss") }'    , ")
            sqlCmd.Append($"    '{ change.OriginalStatus }'  , ")
            sqlCmd.Append($"    '{ change.Status }'          , ")
            sqlCmd.Append($"     { change.OriginalCycletime }, ")
            sqlCmd.Append($"     { change.Cycletime }          ")
            sqlCmd.Append($" );                                ")

        Next

        Try
            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())
        Catch ex As Exception
            Log.Error(ex)
        End Try

        isChanging = False
        listChanges = New List(Of TimelineChange)()
        txtDescription.ResetText()

        EnableEditButtons(False)

        btnLoad_Click(Me, New EventArgs())

    End Sub


    Private Sub btnCancelChange_Click(sender As Object, e As EventArgs) Handles btnCancelChange.Click

        isChanging = False
        listChanges = New List(Of TimelineChange)()
        txtDescription.ResetText()

        EnableEditButtons(False)

        btnLoad_Click(Me, New EventArgs())

    End Sub


    Private Sub txtDescription_TextChanged(sender As Object, e As EventArgs) Handles txtDescription.TextChanged

        btnSaveChange.Enabled = txtDescription.Enabled And (listChanges.Count > 0 And txtDescription.Text.Length > 10)
        btnCancelChange.Enabled = txtDescription.Enabled And (listChanges.Count > 0 Or txtDescription.Text.Length > 0)

    End Sub


    Private Sub timePickerStart_ValueChanged(sender As Object, e As EventArgs) Handles timePickerStart.ValueChanged

        lblLimitStartMin.ForeColor = Color.Blue
        lblLimitStartMax.ForeColor = Color.Blue

        If editMode Then
            If timePickerStart.Value < minTimeStart Then
                timePickerStart.Value = minTimeStart
                MessageBox.Show("The cycle cannot start before the start time of the previus cycle.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                lblLimitStartMin.ForeColor = Color.Red
            End If

            If timePickerStart.Value > maxTimeStart Then
                timePickerStart.Value = maxTimeStart
                MessageBox.Show("The cycle cannot start after the end time of the next cycle.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                lblLimitStartMax.ForeColor = Color.Red
            End If
        ElseIf insertMode Then

        End If

        If timePickerStart.Value >= timePickerEnd.Value Then
            timePickerEnd.Value = timePickerStart.Value.AddSeconds(1)
        End If

    End Sub


    Private Sub timePickerEnd_ValueChanged(sender As Object, e As EventArgs) Handles timePickerEnd.ValueChanged

        lblLimitEndMin.ForeColor = Color.Blue
        lblLimitEndMax.ForeColor = Color.Blue

        If editMode Then

            If timePickerEnd.Value < minTimeEnd Then
                timePickerEnd.Value = minTimeEnd
                MessageBox.Show("The cycle cannot end before the start time of the previus cycle.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                lblLimitEndMin.ForeColor = Color.Red
            End If

            If timePickerEnd.Value > maxTimeEnd Then
                timePickerEnd.Value = maxTimeEnd
                MessageBox.Show("The cycle cannot end after the end time of the next cycle.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                lblLimitEndMax.ForeColor = Color.Red
            End If

        ElseIf insertMode Then

        End If

        If timePickerStart.Value >= timePickerEnd.Value Then
            timePickerStart.Value = timePickerEnd.Value.AddSeconds(-1)
        End If

    End Sub


    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Dim selectedIdx As Integer = dgStatus.SelectedRows(0).Index

        regBefore = listMachineStatus.FirstOrDefault(Function(m) m.TimeId = dgStatus.Rows(selectedIdx).Cells("TimeId").Value)

        If selectedIdx < dgStatus.Rows.Count - 1 Then
            regAfter = listMachineStatus.FirstOrDefault(Function(m) m.TimeId = dgStatus.Rows(selectedIdx + 1).Cells("TimeId").Value)
        Else
            regAfter = listMachineStatus.FirstOrDefault(Function(m) m.TimeId = dgStatus.Rows(selectedIdx).Cells("TimeId").Value)
        End If

        regToEdit = New TimelineChange()
        regToEdit.TimeId = 0
        regToEdit.Shift = regBefore.Shift
        regToEdit.HasBefore = True
        regToEdit.HasAfter = selectedIdx < dgStatus.Rows.Count - 1

        minTimeStart = regBefore.TimeStart
        maxTimeStart = regAfter.TimeEnd.AddSeconds(-2)
        minTimeEnd = regBefore.TimeStart.AddSeconds(2)
        maxTimeEnd = regAfter.TimeEnd

        If regToEdit.HasBefore Then minTimeStart = minTimeStart.AddSeconds(1)
        If regToEdit.HasAfter Then maxTimeEnd = maxTimeEnd.AddSeconds(-1)


        lblInformation.Text = "Insert Mode"

        cmbStatus.SelectedIndex = -1
        timePickerStart.Value = timePickerStart.Value.AddSeconds(1)
        timePickerEnd.Value = timePickerStart.Value.AddSeconds(1)
        lblLimitStartMin.Text = $"Min.- {minTimeStart.ToLongTimeString()}"
        lblLimitStartMax.Text = $"Max.- {maxTimeStart.ToLongTimeString()}"
        lblLimitEndMin.Text = $"Min.- {minTimeEnd.ToLongTimeString()}"
        lblLimitEndMax.Text = $"Max.- {maxTimeEnd.ToLongTimeString()}"
        lblLimitStartMin.ForeColor = Color.Blue
        lblLimitEndMin.ForeColor = Color.Blue

        cmbStatus.Enabled = True
        timePickerStart.Enabled = True
        timePickerEnd.Enabled = True

        btnEdit.Enabled = False
        btnNew.Enabled = False
        btnApply.Enabled = True
        btnCancel.Enabled = True

        insertMode = True

    End Sub


    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Dim selectedIdx As Integer = dgStatus.SelectedRows(0).Index

        If selectedIdx > 0 Then
            regBefore = listMachineStatus.FirstOrDefault(Function(m) m.TimeId = dgStatus.Rows(selectedIdx - 1).Cells("TimeId").Value)
        Else
            regBefore = listMachineStatus.FirstOrDefault(Function(m) m.TimeId = dgStatus.Rows(selectedIdx).Cells("TimeId").Value)
        End If

        If selectedIdx < dgStatus.Rows.Count - 1 Then
            regAfter = listMachineStatus.FirstOrDefault(Function(m) m.TimeId = dgStatus.Rows(selectedIdx + 1).Cells("TimeId").Value)
        Else
            regAfter = listMachineStatus.FirstOrDefault(Function(m) m.TimeId = dgStatus.Rows(selectedIdx).Cells("TimeId").Value)
        End If

        regToEdit = listMachineStatus.FirstOrDefault(Function(m) m.TimeId = dgStatus.Rows(selectedIdx).Cells("TimeId").Value)
        regToEdit.HasBefore = selectedIdx > 0
        regToEdit.HasAfter = selectedIdx < dgStatus.Rows.Count - 1

        minTimeStart = regBefore.TimeStart
        maxTimeStart = regAfter.TimeEnd.AddSeconds(-2)
        minTimeEnd = regBefore.TimeStart.AddSeconds(2)
        maxTimeEnd = regAfter.TimeEnd

        If regToEdit.HasBefore Then minTimeStart = minTimeStart.AddSeconds(1)
        If regToEdit.HasAfter Then maxTimeEnd = maxTimeEnd.AddSeconds(-1)

        lblInformation.Text = "Edit Mode"
        lblLimitStartMin.Text = $"Min.- {minTimeStart.ToLongTimeString()}"
        lblLimitStartMax.Text = $"Max.- {maxTimeStart.ToLongTimeString()}"
        lblLimitEndMin.Text = $"Min.- {minTimeEnd.ToLongTimeString()}"
        lblLimitEndMax.Text = $"Max.- {maxTimeEnd.ToLongTimeString()}"
        lblLimitStartMin.ForeColor = Color.Blue
        lblLimitEndMin.ForeColor = Color.Blue

        cmbStatus.Enabled = True
        timePickerStart.Enabled = True
        timePickerEnd.Enabled = True

        btnEdit.Enabled = False
        btnNew.Enabled = False
        btnApply.Enabled = True
        btnCancel.Enabled = True

        editMode = True

    End Sub


    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click

        Dim timeStart As DateTime
        Dim timeEnd As DateTime
        Dim newTimeId As Integer
        Dim sqlCmd As Text.StringBuilder = New System.Text.StringBuilder()
        Dim action As String = IIf(editMode, "E", "I")
        Dim adjustment As Integer = 0

        If cmbStatus.SelectedIndex < 0 Then Return

        Dim tableName As String = Support.GetMachineTableName(enetMachName)

        If timePickerStart.Value.Date > DateTimePicker.Value.Date Then adjustment = -1

        regToEdit.Status = cmbStatus.Text
        regToEdit.TimeStart = timePickerStart.Value
        regToEdit.TimeEnd = timePickerEnd.Value
        regToEdit.Cycletime = timePickerEnd.Value.TimeOfDay.TotalSeconds - timePickerStart.Value.TimeOfDay.TotalSeconds

        newTimeId = timePickerStart.Value.TimeOfDay.TotalSeconds
        If newTimeId < regBefore.TimeId Then newTimeId += 86400
        regToEdit.TimeId = newTimeId

        If insertMode Then regToEdit.OriginalDate = regToEdit.TimeStart

        Dim change As TimelineChange = New TimelineChange()
        regToEdit.CopyPropertiesTo(change)
        change.Action = action
        listChanges.Add(change)

        regBefore.TimeEnd = regToEdit.TimeStart
        timeStart = regBefore.TimeStart.AddDays(adjustment)
        timeEnd = regBefore.TimeEnd.AddDays(adjustment)
        regBefore.Cycletime = timeEnd.TimeOfDay.TotalSeconds - timeStart.TimeOfDay.TotalSeconds

        newTimeId = timeStart.TimeOfDay.TotalSeconds
        If newTimeId < regBefore.TimeId Then newTimeId += 86400
        regBefore.TimeId = newTimeId

        If regBefore.Cycletime <> regBefore.OriginalCycletime Then
            change = New TimelineChange()
            regBefore.CopyPropertiesTo(change)
            change.Action = "E"
            listChanges.Add(change)
        End If

        regAfter.TimeStart = regToEdit.TimeEnd
        timeStart = regAfter.TimeStart.AddDays(adjustment)
        timeEnd = regAfter.TimeEnd.AddDays(adjustment)
        regAfter.Cycletime = timeEnd.TimeOfDay.TotalSeconds - timeStart.TimeOfDay.TotalSeconds

        newTimeId = timeStart.TimeOfDay.TotalSeconds
        If newTimeId < regBefore.TimeId Then newTimeId += 86400
        regAfter.TimeId = newTimeId

        If regAfter.TimeStart <> regAfter.OriginalDate Then
            change = New TimelineChange()
            regAfter.CopyPropertiesTo(change)
            change.Action = "E"
            listChanges.Add(change)
        End If

        If insertMode Then
            listMachineStatus.Insert(dgStatus.SelectedRows(0).Index + 1, regToEdit)
        End If

        editMode = False
        insertMode = False

        LoadStatusDatagrid(listMachineStatus)
        dgStatus_SelectionChanged(Me, New EventArgs())

    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        dgStatus_SelectionChanged(Me, New EventArgs())

    End Sub


    Private Sub btnDeleteLines_Click(sender As Object, e As EventArgs) Handles btnDeleteLines.Click

        Dim linesToRemove As List(Of TimelineChange) = New List(Of TimelineChange)()

        Dim newLine As Boolean = True

        Dim lineBefore As TimelineChange
        Dim idxBefore As Integer = 0

        Dim idx As Integer = 0

        Dim hasSelectedLine = False

        Do While idx < dgStatus.Rows.Count
            If dgStatus.Rows(idx).Cells("Selected").Value Then
                hasSelectedLine = True
                Exit Do
            End If
            idx += 1
        Loop

        If Not hasSelectedLine Then
            MessageBox.Show("No records selected!")
            Return
        End If

        If Not MessageBox.Show("Do you confirm the exclusion of the selected records?", "Exclusion", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) = DialogResult.Yes Then
            Return
        End If


        idx = 0
        Do While idx < dgStatus.Rows.Count

            Do
                idx += 1

                If idx = dgStatus.Rows.Count Then Exit Do

            Loop Until dgStatus.Rows(idx).Cells("Selected").Value

            If idx = dgStatus.Rows.Count Then
                Exit Do
            End If

            'Save the line before the deleted lines
            Dim statusLineBefore As TimelineChange = listMachineStatus.FirstOrDefault(Function(m) m.TimeId = dgStatus.Rows(idx - 1).Cells("TimeId").Value)

            lineBefore = New TimelineChange()
            statusLineBefore.CopyPropertiesTo(lineBefore)
            lineBefore.Action = "E"

            listChanges.Add(lineBefore)
            idxBefore = listChanges.Count - 1

            Do While dgStatus.Rows(idx).Cells("Selected").Value

                Dim statusLine As TimelineChange = listMachineStatus.FirstOrDefault(Function(m) m.TimeId = dgStatus.Rows(idx).Cells("TimeId").Value)

                statusLineBefore.Cycletime += statusLine.Cycletime
                listChanges(idxBefore).Cycletime += statusLine.Cycletime

                Dim change = New TimelineChange()
                statusLine.CopyPropertiesTo(change)
                change.Action = "D"

                listChanges.Add(change)

                listMachineStatus.Remove(statusLine)
                idx += 1
            Loop

            If idx < dgStatus.Rows.Count Then

                Dim statusLineAfter As TimelineChange = listMachineStatus.FirstOrDefault(Function(m) m.TimeId = dgStatus.Rows(idx).Cells("TimeId").Value)

                If statusLineAfter.Status = statusLineBefore.Status Then

                    statusLineBefore.Cycletime += statusLineAfter.Cycletime
                    listChanges(idxBefore).Cycletime += statusLineAfter.Cycletime

                    Dim change = New TimelineChange()
                    statusLineAfter.CopyPropertiesTo(change)
                    change.Action = "D"

                    listChanges.Add(change)

                    listMachineStatus.Remove(statusLineAfter)

                End If

            End If

        Loop

        LoadStatusDatagrid(listMachineStatus)

    End Sub


    Private Sub EnableEditButtons(enable As Boolean)

        btnStartChange.Enabled = Not enable

        btnNew.Enabled = enable
        btnEdit.Enabled = enable
        btnDeleteLines.Enabled = enable
    End Sub

End Class





Imports CSI_Library
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Server.Library
Imports CSIFLEX.Utilities

Public Class form_raw_values

    Public CSI_LIB As New CSI_Library.CSI_Library

    Dim machineId As Integer

    Dim listChanges As List(Of TimelineItem)

    Dim timelineItems As List(Of TimelineItem)
    Dim timelineSource As BindingSource

    Dim timelineSelectedItens As List(Of Integer)

    Dim editItems As List(Of EditItem)
    Dim editSource As BindingSource

    Dim regBefore As TimelineItem
    Dim regToEdit As TimelineItem
    Dim regAfter As TimelineItem

    Dim maxValue As Integer
    Dim selectedTimeId As Integer

    Dim minTimeStart As DateTime
    Dim maxTimeStart As DateTime
    Dim minTimeEnd As DateTime
    Dim maxTimeEnd As DateTime

    Dim isChanging As Boolean = False
    Dim editMode As Boolean = False
    Dim insertMode As Boolean = False

    Dim connString As String

    Private tblMachineName As String
    Private enetMachineName_ As String
    Public Property EnetMachineName() As String
        Get
            Return enetMachineName_
        End Get
        Set(ByVal value As String)
            enetMachineName_ = value
            tblMachineName = CSI_LIB.RenameMachine(enetMachineName_)
        End Set
    End Property

    Private shift_ As Integer
    Public Property CurrentShift() As Integer
        Get
            Return shift_
        End Get
        Set(ByVal value As Integer)
            shift_ = value
        End Set
    End Property

    Private dateNextDay As DateTime
    Private dateTimeline_ As DateTime
    Public Property DateTimeline() As DateTime
        Get
            Return dateTimeline_
        End Get
        Set(ByVal value As DateTime)
            dateTimeline_ = value
            dateNextDay = dateTimeline_.AddDays(1)
        End Set
    End Property

    Private tblDatasource As DataView
    Public Property Datasource() As DataView

        Get
            Return tblDatasource
        End Get

        Set(ByVal value As DataView)

            tblDatasource = value

            Dim startDateTime As DateTime = tblDatasource.Table.Rows(0)("Date_")
            Dim timeIdAdj As Integer = 0

            timelineItems = New List(Of TimelineItem)()

            For Each rowChange As DataRow In tblDatasource.Table.Rows

                Dim change As TimelineItem = New TimelineItem()

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
                change.PartNumber = IIf(Not IsDBNull(rowChange("PartNumber")), rowChange("PartNumber"), "")
                change.Comments = IIf(Not IsDBNull(rowChange("Comments")), rowChange("Comments"), "")
                change.Cycletime = Integer.Parse(rowChange("cycletime").ToString())
                change.OriginalCycletime = Integer.Parse(rowChange("cycletime").ToString())
                change.Shift = Integer.Parse(rowChange("shift").ToString())
                change.TimeStart = datetimeChange
                change.OriginalDate = DateTime.Parse(rowChange("Date_").ToString())
                change.TimeEnd = datetimeChange.AddSeconds(change.Cycletime)

                If Not change.Status.StartsWith("_") Then

                    If change.Cycletime > maxValue Then maxValue = change.Cycletime

                    timelineItems.Add(change)

                End If
            Next

            timelineItems = timelineItems.OrderBy(Function(t) t.TimeId).ToList()

        End Set

    End Property


    Private Sub form_raw_values_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        connString = CSI_Library.CSI_Library.MySqlConnectionString

        timelineItems = New List(Of TimelineItem)()
        timelineSource = New BindingSource()
        timelineSource.DataSource = timelineItems

        dgvTimeline.AutoGenerateColumns = False
        dgvTimeline.DataSource = timelineSource

        editItems = New List(Of EditItem)()
        editSource = New BindingSource()
        editSource.DataSource = editItems

        dgvEdits.AutoGenerateColumns = False
        dgvEdits.DataSource = editSource

        machineId = CSI_Library.CSI_Library.MachineId
        listChanges = New List(Of TimelineItem)()

        timelineSelectedItens = New List(Of Integer)()

        LoadDgvTimeline()
        LoadDgvEdits()
        LoadComboStatus()

        rdbTimeline.Checked = True

        rdbEdit_CheckedChanged(Me, New EventArgs())

        DisableEditMode()

        Return

    End Sub


    Private Sub LoadDgvTimeline()

        dgvTimeline.AutoGenerateColumns = False

        Dim sqlCmd As New Text.StringBuilder()
        sqlCmd.Append($"SELECT * FROM                             ")
        sqlCmd.Append($"      csi_database.tbl_{ tblMachineName } ")
        sqlCmd.Append($" WHERE                                    ")
        sqlCmd.Append($"      time_ > '{ dateTimeline_.ToString("yyyy-MM-dd") }' ")
        sqlCmd.Append($"  AND time_ < '{ dateNextDay.ToString("yyyy-MM-dd") }'   ")
        sqlCmd.Append($"  AND shift =  { shift_ }                 ")
        sqlCmd.Append($"  AND status NOT LIKE '\_%'               ")
        sqlCmd.Append($" ORDER BY time_                           ")

        Dim dtTimeline = MySqlAccess.GetDataTable(sqlCmd.ToString(), connString)

        If dtTimeline.Rows.Count = 0 Then Me.Close()

        Dim startDateTime As DateTime = dtTimeline.Rows(0)("Date_")
        Dim timeIdAdj As Integer = 0

        timelineItems.Clear()

        For Each rowChange As DataRow In dtTimeline.Rows

            Dim change As TimelineItem = New TimelineItem()

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
            change.PartNumber = IIf(Not IsDBNull(rowChange("PartNumber")), rowChange("PartNumber"), "")
            change.OriginalPartNumber = change.PartNumber
            change.OperatorName = IIf(Not IsDBNull(rowChange("Operator")), rowChange("Operator"), "")
            change.OriginalOperatorName = change.OperatorName
            change.Comments = IIf(Not IsDBNull(rowChange("Comments")), rowChange("Comments"), "")
            change.OriginalComments = change.Comments
            change.Cycletime = Integer.Parse(rowChange("cycletime").ToString())
            change.OriginalCycletime = Integer.Parse(rowChange("cycletime").ToString())
            change.Shift = Integer.Parse(rowChange("shift").ToString())
            change.TimeStart = datetimeChange
            change.OriginalDate = DateTime.Parse(rowChange("Date_").ToString())
            change.TimeEnd = datetimeChange.AddSeconds(change.Cycletime)

            If change.Cycletime > maxValue Then maxValue = change.Cycletime

            timelineItems.Add(change)
        Next

        timelineSource.ResetBindings(False)
        dgvFillBars(dgvTimeline)

        dgvTimelineItems_SelectionChanged(Me, New EventArgs())

        btnStartChange.Enabled = CSI_Library.CSI_Library.userEditTimeline

        AllowSaveChanges()

    End Sub


    Private Sub LoadDgvEdits()

        Dim sqlCmd As New Text.StringBuilder()
        sqlCmd.Append($"SELECT                                                               ")
        sqlCmd.Append($"    *                                                                ")
        sqlCmd.Append($" FROM                                                                ")
        sqlCmd.Append($"    csi_database.vw_adjustment                                       ")
        sqlCmd.Append($" WHERE                                                               ")
        sqlCmd.Append($"    MachineId  =  { machineId }                                      ")
        sqlCmd.Append($"AND AdjustDate = '{ dateTimeline_.ToString("yyyy-MM-dd HH:mm:ss") }' ")
        sqlCmd.Append($"AND Shift      =  { shift_ }                                         ")

        Dim tblEdits As DataTable = MySqlAccess.GetDataTable(sqlCmd.ToString(), connString)

        editItems.Clear()

        For Each editRow As DataRow In tblEdits.Rows
            Dim editItem = New EditItem()
            editItem.EditId = Integer.Parse(editRow("Id").ToString())
            editItem.Action = IIf(editRow("Action") = "E", "Edited", IIf(editRow("Action") = "I", "Inserted", "Deleted"))
            editItem.OriginalStatus = editRow("Status")
            editItem.NewStatus = editRow("NewStatus")
            editItem.OriginalTimeStart = DateTime.Parse(editRow("Date").ToString())
            editItem.NewTimeStart = DateTime.Parse(editRow("NewDate").ToString())
            editItem.OriginalCycletime = Integer.Parse(editRow("Cycletime").ToString())
            editItem.NewCycletime = Integer.Parse(editRow("NewCycletime").ToString())
            editItem.OriginalPartNumber = editRow("PartNumber").ToString()
            editItem.NewPartNumber = editRow("NewPartNumber").ToString()
            editItem.OriginalOperatorName = editRow("Operator").ToString()
            editItem.NewOperatorName = editRow("NewOperator").ToString()
            editItem.OriginalComments = editRow("Comments").ToString()
            editItem.NewComments = editRow("NewComments").ToString()
            editItem.EditBy = editRow("CreatedBy").ToString()
            editItem.EditWhen = DateTime.Parse(editRow("CreatedWhen").ToString())
            editItem.EditDescription = editRow("Description").ToString()

            editItems.Add(editItem)
        Next

        editSource.ResetBindings(False)
        dgvFillBars(dgvEdits)

    End Sub


    Private Sub LoadComboStatus()

        Dim tblStatus As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_mach_status WHERE MachineId = { machineId }", connString)
        cmbStatus.Items.Clear()

        For Each row As DataRow In tblStatus.Rows
            Dim status = row("Status")
            cmbStatus.Items.Add(status)
        Next
        cmbStatus.Items.Remove("PRODUCTION")
        cmbStatus.SelectedIndex = -1

    End Sub


    'Private Sub LoadComboChanges()

    '    RemoveHandler cmbChanges.SelectedIndexChanged, AddressOf cmbChanges_SelectedIndexChanged

    '    Dim tblChanges As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.tbl_adjustment WHERE MachineId = { machineId } AND AdjustDate = '{dateTimeline_.ToString("yyyy-MM-dd HH:mm:ss")}' AND Shift = { shift_ }")
    '    cmbChanges.Items.Clear()
    '    cmbChanges.Items.Add(New Tuple(Of Integer, String)(0, "Current Timeline"))

    '    For Each row As DataRow In tblChanges.Rows
    '        Dim changeDate As DateTime = row("CreatedWhen")
    '        Dim change As Tuple(Of Integer, String) = New Tuple(Of Integer, String)(row("Id"), changeDate.ToString("MMM dd, yyyy HH:mm"))

    '        cmbChanges.Items.Add(change)
    '    Next
    '    cmbChanges.ValueMember = "Item1"
    '    cmbChanges.DisplayMember = "Item2"
    '    cmbChanges.SelectedIndex = 0

    '    AddHandler cmbChanges.SelectedIndexChanged, AddressOf cmbChanges_SelectedIndexChanged

    'End Sub


    Private Sub dgvTimelineItems_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTimeline.SelectionChanged

        If dgvTimeline.SelectedRows.Count = 0 Then Return

        Dim row As DataGridViewRow = dgvTimeline.SelectedRows(0)

        RegistryToEdit()

        selectedTimeId = row.Cells("TimeId").Value
        timePickerStart.Value = row.Cells("TimeStart").Value
        timePickerEnd.Value = row.Cells("TimeEnd").Value
        cmbStatus.SelectedItem = row.Cells("Status").Value
        txtComments.Text = row.Cells("Comments").Value
        txtPartNumber.Text = row.Cells("PartNr").Value
        txtOperator.Text = row.Cells("Oper").Value

        If Not isChanging Then
            Return
        End If

        'If insertMode Then
        '    btnNew_Click(Me, New EventArgs())
        '    Return
        'End If

        'lblLimitStartMin.ResetText()
        'lblLimitStartMax.ResetText()
        'lblLimitEndMin.ResetText()
        'lblLimitEndMax.ResetText()

        'cmbStatus.Enabled = False
        'timePickerStart.Enabled = False
        'timePickerEnd.Enabled = False

        'btnEdit.Enabled = True
        'btnNew.Enabled = True
        'btnApply.Enabled = False
        'btnCancel.Enabled = False

        'editMode = False
        'insertMode = False

    End Sub


    Private Sub dgvTimelineItems_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) 'Handles dgvTimeline.CellBeginEdit

        Dim row = e.RowIndex
        Dim col = e.ColumnIndex

        If row < 0 Or col <> dgvTimeline.Columns("Selected").Index Then
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


    Private Sub dgvTimelineItems_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvTimeline.RowValidating

        'If editMode Then
        '    e.Cancel = True
        '    Return
        'End If

    End Sub


    Private Sub dgvFillBars(dgv As DataGridView)

        Dim action As String
        Dim status As String
        Dim cycletime As Integer
        Dim statusImage As Image
        Dim imageSize As Integer

        If dgv.Rows.Count = 0 Then Return

        maxValue = timelineItems.Select(Function(m) m.Cycletime).Max()

        For Each row As DataGridViewRow In dgv.Rows

            If dgv.Name = "dgvTimeline" Then
                status = row.Cells("Status").Value
                cycletime = row.Cells("Cycletime").Value
            Else
                status = row.Cells("NewStatus").Value
                cycletime = row.Cells("NewCycletime").Value
            End If

            If Not String.IsNullOrEmpty(status) Then

                imageSize = Int((cycletime * 150) / maxValue)

                If imageSize <= 0 Then
                    imageSize = 2
                End If

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

                If dgv.Name = "dgvTimeline" Then
                    row.Cells("Image").Value = statusImage
                Else
                    row.Cells("EditImage").Value = statusImage

                    action = row.Cells("Action").Value

                    Select Case action
                        Case "Edited"
                            row.Cells("Action").Style.BackColor = Color.Yellow
                        Case "Inserted"
                            row.Cells("Action").Style.BackColor = Color.LightGreen
                        Case "Deleted"
                            row.Cells("Action").Style.BackColor = Color.Red
                    End Select

                    If row.Cells("NewStatus").Value <> row.Cells("OriginalStatus").Value Then
                        row.Cells("NewStatus").Style.Font = New Font(dgvEdits.Font, FontStyle.Bold)
                    End If

                    If row.Cells("NewTimeStart").Value <> row.Cells("OriginalTimeStart").Value Then
                        row.Cells("NewTimeStart").Style.Font = New Font(dgvEdits.Font, FontStyle.Bold)
                    End If

                    If row.Cells("NewCycletime").Value <> row.Cells("OriginalCycletime").Value Then
                        row.Cells("NewCycletime").Style.Font = New Font(dgvEdits.Font, FontStyle.Bold)
                    End If

                    If row.Cells("NewPartNumber").Value <> row.Cells("OriginalPartNumber").Value Then
                        row.Cells("NewPartNumber").Style.Font = New Font(dgvEdits.Font, FontStyle.Bold)
                    End If

                    If row.Cells("NewOperator").Value <> row.Cells("OriginalOperator").Value Then
                        row.Cells("NewOperator").Style.Font = New Font(dgvEdits.Font, FontStyle.Bold)
                    End If

                    If row.Cells("NewComments").Value <> row.Cells("OriginalComments").Value Then
                        row.Cells("NewComments").Style.Font = New Font(dgvEdits.Font, FontStyle.Bold)
                    End If

                End If

            End If
        Next

    End Sub


    'Private Sub cmbChanges_SelectedIndexChanged(sender As Object, e As EventArgs)

    '    If cmbChanges.SelectedIndex < 0 Then Return

    '    txtDescription.ResetText()
    '    lblInfo.ResetText()
    '    btnStartChange.Enabled = False

    '    If cmbChanges.SelectedIndex = 0 Then
    '        LoadDataGrid()
    '        Return
    '    End If

    '    Dim timelineChanges = New List(Of TimelineItem)()

    '    Dim changeId As Integer = cmbChanges.SelectedItem.Item1

    '    Dim sqlCmd As Text.StringBuilder = New System.Text.StringBuilder()
    '    sqlCmd.Append($"SELECT                            ")
    '    sqlCmd.Append($"	A.Id                        , ")
    '    sqlCmd.Append($"	A.Machine                   , ")
    '    sqlCmd.Append($"	A.AdjustDate                , ")
    '    sqlCmd.Append($"	A.Shift                     , ")
    '    sqlCmd.Append($"	A.Description               , ")
    '    sqlCmd.Append($"	A.CreatedWhen               , ")
    '    sqlCmd.Append($"	A.CreatedBy                 , ")
    '    sqlCmd.Append($"    R.Id RowId                  , ")
    '    sqlCmd.Append($"    R.Action                    , ")
    '    sqlCmd.Append($"    R.Date                      , ")
    '    sqlCmd.Append($"    R.NewDate                   , ")
    '    sqlCmd.Append($"    R.Status                    , ")
    '    sqlCmd.Append($"    R.NewStatus                 , ")
    '    sqlCmd.Append($"    R.Cycletime                 , ")
    '    sqlCmd.Append($"    R.NewCycletime                ")
    '    sqlCmd.Append($" FROM                             ")
    '    sqlCmd.Append($"	csi_database.tbl_adjustment A ")
    '    sqlCmd.Append($"	INNER JOIN csi_database.tbl_adjust_rows R ON A.Id = R.AdjustmentId ")
    '    sqlCmd.Append($" WHERE A.Id = { changeId }        ")
    '    sqlCmd.Append($" ORDER BY R.Id                    ")

    '    Dim tblChange = MySqlAccess.GetDataTable(sqlCmd.ToString())
    '    Dim change As TimelineItem

    '    If tblChange.Rows.Count > 0 Then
    '        Dim createdWhen As DateTime = tblChange.Rows(0)("CreatedWhen")
    '        lblInfo.Text = $"Changed by { tblChange.Rows(0)("CreatedBy") } on { createdWhen.ToString("MMM dd, yyyy - HH:mm") }"
    '        txtDescription.Text = tblChange.Rows(0)("Description")
    '    End If

    '    For Each rowChange In tblChange.Rows

    '        change = New TimelineItem()
    '        change.Action = IIf(rowChange("Action") = "E", "Edited", IIf(rowChange("Action") = "D", "Deleted", "Inserted"))
    '        change.Shift = Integer.Parse(rowChange("shift").ToString())

    '        change.Status = rowChange("NewStatus")
    '        change.OriginalStatus = rowChange("status")

    '        change.TimeStart = rowChange("NewDate")
    '        change.OriginalDate = rowChange("Date")

    '        change.Cycletime = rowChange("NewCycletime")
    '        change.OriginalCycletime = rowChange("cycletime")

    '        timelineChanges.Add(change)
    '    Next

    '    RemoveHandler dgvTimeline.SelectionChanged, AddressOf dgvTimelineItems_SelectionChanged

    '    dgvTimeline.Columns.Clear()

    '    Dim newColumn As DataGridViewColumn

    '    newColumn = New DataGridViewTextBoxColumn()
    '    newColumn.Name = "Action"
    '    newColumn.HeaderText = "Action"
    '    newColumn.DataPropertyName = "Action"
    '    newColumn.Width = 60
    '    dgvTimeline.Columns.Add(newColumn)

    '    newColumn = New DataGridViewTextBoxColumn()
    '    newColumn.Name = "OriginalStatus"
    '    newColumn.HeaderText = "Original Status"
    '    newColumn.DataPropertyName = "OriginalStatus"
    '    newColumn.Width = 120
    '    dgvTimeline.Columns.Add(newColumn)

    '    newColumn = New DataGridViewTextBoxColumn()
    '    newColumn.Name = "Status"
    '    newColumn.HeaderText = "New Status"
    '    newColumn.DataPropertyName = "Status"
    '    newColumn.Width = 120
    '    dgvTimeline.Columns.Add(newColumn)

    '    newColumn = New DataGridViewTextBoxColumn()
    '    newColumn.Name = "OriginalDate"
    '    newColumn.HeaderText = "Original Start"
    '    newColumn.DataPropertyName = "OriginalDate"
    '    newColumn.Width = 150
    '    dgvTimeline.Columns.Add(newColumn)
    '    dgvTimeline.Columns("OriginalDate").DefaultCellStyle.Format = "MM-dd-yyyy HH:mm:ss"

    '    newColumn = New DataGridViewTextBoxColumn()
    '    newColumn.Name = "TimeStart"
    '    newColumn.HeaderText = "New Start"
    '    newColumn.DataPropertyName = "TimeStart"
    '    newColumn.Width = 150
    '    dgvTimeline.Columns.Add(newColumn)
    '    dgvTimeline.Columns("TimeStart").DefaultCellStyle.Format = "MM-dd-yyyy HH:mm:ss"

    '    newColumn = New DataGridViewTextBoxColumn()
    '    newColumn.Name = "OriginalCycletime"
    '    newColumn.HeaderText = "Original Cycletime"
    '    newColumn.DataPropertyName = "OriginalCycletime"
    '    newColumn.Width = 80
    '    dgvTimeline.Columns.Add(newColumn)

    '    newColumn = New DataGridViewTextBoxColumn()
    '    newColumn.Name = "Cycletime"
    '    newColumn.HeaderText = "New Cycletime"
    '    newColumn.DataPropertyName = "Cycletime"
    '    newColumn.Width = 80
    '    dgvTimeline.Columns.Add(newColumn)

    '    newColumn = New DataGridViewImageColumn()
    '    newColumn.Name = "Image"
    '    newColumn.HeaderText = ""
    '    newColumn.Width = 300
    '    dgvTimeline.Columns.Add(newColumn)
    '    dgvTimeline.Columns("Image").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

    '    dgvTimeline.DataSource = Nothing
    '    dgvTimeline.Rows.Clear()
    '    dgvTimeline.DataSource = timelineChanges
    '    dgvTimeline.Refresh()

    '    dgvTimelineItemsFillBars()

    'End Sub


    Private Sub btnStartChange_Click(sender As Object, e As EventArgs) Handles btnStartChange.Click

        isChanging = True

        txtDescription.Enabled = True
        txtDescription.ReadOnly = False

        EnableEditMode()

        listChanges = New List(Of TimelineItem)()

    End Sub


    Private Sub btnSaveChange_Click(sender As Object, e As EventArgs) Handles btnSaveChange.Click

        If Not MessageBox.Show("Do you confirm the changes in the timeline?", "Confirmation!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) = DialogResult.Yes Then
            Return
        End If

        Dim sqlCmd As Text.StringBuilder = New System.Text.StringBuilder()
        Dim adjId As Integer

        Dim tableName As String = Support.GetMachineTableName(enetMachineName_)

        Dim adjustment As Integer = 0
        If timePickerStart.Value.Date > dateTimeline_.Date Then adjustment = -1

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
        sqlCmd.Append($"    '{ enetMachineName_ }'           , ")
        sqlCmd.Append($"    '{ dateTimeline_.Date.ToString("yyyy-MM-dd") }'   , ")
        sqlCmd.Append($"     { shift_ }                      , ")
        sqlCmd.Append($"    '{ txtDescription.Text }'        , ")
        sqlCmd.Append($"    '{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }', ")
        sqlCmd.Append($"    '{ CSI_Library.CSI_Library.username }'              ")
        sqlCmd.Append($" );                                    ")
        sqlCmd.Append($" SELECT LAST_INSERT_ID();              ")

        adjId = MySqlAccess.ExecuteScalar(sqlCmd.ToString(), connString)

        sqlCmd.Clear()

        For Each change As TimelineItem In listChanges

            If change.Action = "E" Then

                change.TimeStart = change.TimeStart.AddDays(adjustment)

                sqlCmd.Append($"UPDATE csi_database.{ tableName } SET       ")
                sqlCmd.Append($"    time_      = '{ change.TimeStart.ToString("yyyy-MM-dd HH:mm:ss") }', ")
                sqlCmd.Append($"    Date_      = '{ change.TimeStart.ToString("yyyy-MM-dd HH:mm:ss") }', ")
                sqlCmd.Append($"    Status     = '{ change.Status }'      , ")
                sqlCmd.Append($"    Cycletime  =  { change.Cycletime }    , ")
                sqlCmd.Append($"    PartNumber = '{ change.PartNumber }'  , ")
                sqlCmd.Append($"    Operator   = '{ change.OperatorName }', ")
                sqlCmd.Append($"    Comments   = '{ change.Comments }'      ")
                sqlCmd.Append($" WHERE                                      ")
                sqlCmd.Append($"     Status = '{ change.OriginalStatus }'   ")
                sqlCmd.Append($" AND Date_  = '{ change.OriginalDate.ToString("yyyy-MM-dd HH:mm:ss") }';  ")

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
                sqlCmd.Append($"    partNumber          , ")
                sqlCmd.Append($"    operator            , ")
                sqlCmd.Append($"    comments              ")
                sqlCmd.Append($" ) VALUES (               ")
                sqlCmd.Append($"     { change.TimeStart.Month }, ")
                sqlCmd.Append($"     { change.TimeStart.Day }  , ")
                sqlCmd.Append($"     { change.TimeStart.Year } , ")
                sqlCmd.Append($"    '{ change.TimeStart.ToString("yyyy-MM-dd HH:mm:ss") }', ")
                sqlCmd.Append($"    '{ change.TimeStart.ToString("yyyy-MM-dd HH:mm:ss") }', ")
                sqlCmd.Append($"    '{ change.Status }'        , ")
                sqlCmd.Append($"     { change.Shift }          , ")
                sqlCmd.Append($"     { change.Cycletime }      , ")
                sqlCmd.Append($"    '{ change.PartNumber }'    , ")
                sqlCmd.Append($"    '{ change.OperatorName }'  , ")
                sqlCmd.Append($"    '{ change.Comments }'        ")
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
            sqlCmd.Append($"    PartNumber                   , ")
            sqlCmd.Append($"    NewPartNumber                , ")
            sqlCmd.Append($"    Operator                     , ")
            sqlCmd.Append($"    NewOperator                  , ")
            sqlCmd.Append($"    Comments                     , ")
            sqlCmd.Append($"    NewComments                  , ")
            sqlCmd.Append($"    Cycletime                    , ")
            sqlCmd.Append($"    NewCycletime                   ")
            sqlCmd.Append($" ) VALUES (                        ")
            sqlCmd.Append($"     { adjId }                   , ")
            sqlCmd.Append($"    '{ change.Action }'          , ")
            sqlCmd.Append($"    '{ enetMachineName_ }'       , ")
            sqlCmd.Append($"    '{ change.OriginalDate.ToString("yyyy-MM-dd HH:mm:ss") }' , ")
            sqlCmd.Append($"    '{ change.TimeStart.ToString("yyyy-MM-dd HH:mm:ss") }'    , ")
            sqlCmd.Append($"    '{ change.OriginalStatus }'      , ")
            sqlCmd.Append($"    '{ change.Status }'              , ")
            sqlCmd.Append($"    '{ change.OriginalPartNumber }'  , ")
            sqlCmd.Append($"    '{ change.PartNumber }'          , ")
            sqlCmd.Append($"    '{ change.OriginalOperatorName }', ")
            sqlCmd.Append($"    '{ change.OperatorName }'        , ")
            sqlCmd.Append($"    '{ change.OriginalComments }'    , ")
            sqlCmd.Append($"    '{ change.Comments }'            , ")
            sqlCmd.Append($"     { change.OriginalCycletime }    , ")
            sqlCmd.Append($"     { change.Cycletime }              ")
            sqlCmd.Append($" );                                    ")
        Next

        Try
            MySqlAccess.ExecuteNonQuery(sqlCmd.ToString(), connString)
        Catch ex As Exception
            Log.Error(ex)
        End Try

        isChanging = False
        listChanges = New List(Of TimelineItem)()
        txtDescription.ResetText()

        LoadDgvTimeline()
        LoadDgvEdits()

        editSource.ResetBindings(False)
        dgvFillBars(dgvEdits)

        DisableEditMode()

    End Sub


    Private Sub btnCancelChange_Click(sender As Object, e As EventArgs) Handles btnCancelChange.Click

        isChanging = False
        listChanges = New List(Of TimelineItem)()
        txtDescription.ResetText()

        LoadDgvTimeline()

        DisableEditMode()

    End Sub


    Private Sub txtDescription_TextChanged(sender As Object, e As EventArgs) Handles txtDescription.TextChanged

        AllowSaveChanges()

    End Sub


    Private Sub AllowSaveChanges()

        btnSaveChange.Enabled = listChanges.Count > 0 'txtDescription.Enabled And (listChanges.Count > 0 And txtDescription.Text.Length > 10)
        btnCancelChange.Enabled = listChanges.Count > 0 'txtDescription.Enabled And (listChanges.Count > 0 Or txtDescription.Text.Length > 0)
        btnStartChange.Enabled = False

    End Sub


    Private Sub timePickerStart_ValueChanged(sender As Object, e As EventArgs) Handles timePickerStart.ValueChanged

        lblLimitStartMin.ForeColor = Color.Blue
        lblLimitStartMax.ForeColor = Color.Blue

        If editMode And rdbStatus.Checked Then
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

        If editMode And rdbStatus.Checked Then

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


    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        rdbStatus.Checked = False
        rdbStatus.Enabled = True
        rdbPartNumber.Checked = False
        rdbPartNumber.Enabled = True
        rdbOperator.Checked = False
        rdbOperator.Enabled = True

        btnEdit.Enabled = False
        btnNew.Enabled = False
        btnDeleteLines.Enabled = False
        btnApply.Enabled = True
        btnCancel.Enabled = True

        editMode = True

        RegistryToEdit()

    End Sub


    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        rdbStatus.Checked = True

        cmbStatus.Enabled = True
        timePickerStart.Enabled = True
        timePickerEnd.Enabled = True

        btnEdit.Enabled = False
        btnNew.Enabled = False
        btnDeleteLines.Enabled = False
        btnApply.Enabled = True
        btnCancel.Enabled = True

        insertMode = True

        RegistryToEdit()

    End Sub


    Private Sub RegistryToEdit()


        'Dim selectedIdx As Integer = dgvTimeline.SelectedRows(0).Index


        'lblLimitStartMin.Text = $"Min.- {minTimeStart.ToLongTimeString()}"
        'lblLimitStartMax.Text = $"Max.- {maxTimeStart.ToLongTimeString()}"
        'lblLimitEndMin.Text = $"Min.- {minTimeEnd.ToLongTimeString()}"
        'lblLimitEndMax.Text = $"Max.- {maxTimeEnd.ToLongTimeString()}"
        'lblLimitStartMin.ForeColor = Color.Blue
        'lblLimitEndMin.ForeColor = Color.Blue

        If Not editMode And Not insertMode Then Return

        regBefore = Nothing
        regAfter = Nothing

        Dim selectedIdx As Integer = dgvTimeline.SelectedRows(0).Index

        If insertMode Then

            regBefore = timelineItems.FirstOrDefault(Function(m) m.TimeId = dgvTimeline.Rows(selectedIdx).Cells("TimeId").Value)

            If selectedIdx < dgvTimeline.Rows.Count - 1 Then
                regAfter = timelineItems.FirstOrDefault(Function(m) m.TimeId = dgvTimeline.Rows(selectedIdx + 1).Cells("TimeId").Value)
            End If

            regToEdit = New TimelineItem()
            regToEdit.TimeId = 0
            regToEdit.Shift = regBefore.Shift
            regToEdit.PartNumber = regBefore.PartNumber
            regToEdit.OperatorName = regBefore.OperatorName
            regToEdit.HasBefore = True
            regToEdit.HasAfter = selectedIdx < dgvTimeline.Rows.Count - 1

            minTimeStart = regBefore.TimeStart
            minTimeEnd = regBefore.TimeStart.AddSeconds(2)
            maxTimeStart = regBefore.TimeStart
            maxTimeEnd = regBefore.TimeStart.AddSeconds(2)

            If Not regAfter Is Nothing Then
                maxTimeStart = regAfter.TimeEnd.AddSeconds(-2)
                maxTimeEnd = regAfter.TimeEnd
            End If

        ElseIf editMode Then

            regToEdit = timelineItems.FirstOrDefault(Function(m) m.TimeId = dgvTimeline.Rows(selectedIdx).Cells("TimeId").Value)
            regToEdit.HasBefore = selectedIdx > 0
            regToEdit.HasAfter = selectedIdx < dgvTimeline.Rows.Count - 1

            minTimeStart = regToEdit.TimeStart
            maxTimeStart = regToEdit.TimeStart
            minTimeEnd = regToEdit.TimeEnd
            maxTimeEnd = regToEdit.TimeEnd

            If regToEdit.HasBefore Then
                regBefore = timelineItems.FirstOrDefault(Function(m) m.TimeId = dgvTimeline.Rows(selectedIdx - 1).Cells("TimeId").Value)
                minTimeStart = regBefore.TimeStart.AddSeconds(1)
                maxTimeStart = regToEdit.TimeEnd.AddSeconds(-1)
                minTimeEnd = regBefore.TimeStart.AddSeconds(2)
            End If

            If regToEdit.HasAfter Then
                regAfter = timelineItems.FirstOrDefault(Function(m) m.TimeId = dgvTimeline.Rows(selectedIdx + 1).Cells("TimeId").Value)
                maxTimeStart = regAfter.TimeEnd.AddSeconds(-2)
                minTimeEnd = regToEdit.TimeStart.AddSeconds(1)
                maxTimeEnd = regAfter.TimeEnd.AddSeconds(-1)
            End If

        End If


        If regToEdit.HasBefore Then minTimeStart = minTimeStart.AddSeconds(1)
        If regToEdit.HasAfter Then maxTimeEnd = maxTimeEnd.AddSeconds(-1)

        'cmbStatus.SelectedIndex = -1
        'timePickerStart.Value = timePickerStart.Value.AddSeconds(1)
        'timePickerEnd.Value = timePickerStart.Value.AddSeconds(1)
        lblLimitStartMin.Text = $"Min.- {minTimeStart.ToLongTimeString()}"
        lblLimitStartMax.Text = $"Max.- {maxTimeStart.ToLongTimeString()}"
        lblLimitEndMin.Text = $"Min.- {minTimeEnd.ToLongTimeString()}"
        lblLimitEndMax.Text = $"Max.- {maxTimeEnd.ToLongTimeString()}"
        lblLimitStartMin.ForeColor = Color.Blue
        lblLimitEndMin.ForeColor = Color.Blue

        If rdbStatus.Checked Then
            timePickerStart.Enabled = minTimeStart <> maxTimeStart
            timePickerEnd.Enabled = minTimeEnd <> maxTimeEnd
        End If

    End Sub



    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click

        Dim timeStart As DateTime
        Dim timeEnd As DateTime
        Dim newTimeId As Integer
        Dim sqlCmd As Text.StringBuilder = New System.Text.StringBuilder()
        Dim action As String = IIf(editMode, "E", "I")
        Dim adjustment As Integer = 0

        If cmbStatus.SelectedIndex < 0 Then Return

        'Dim tableName As String = Support.GetMachineTableName(enetMachineName_)

        If rdbStatus.Checked Then

            If timePickerStart.Value.Date > dateTimeline_.Date Then adjustment = -1

            regToEdit.Status = cmbStatus.Text
            regToEdit.Comments = txtComments.Text
            regToEdit.TimeStart = timePickerStart.Value
            regToEdit.TimeEnd = timePickerEnd.Value
            regToEdit.Cycletime = timePickerEnd.Value.TimeOfDay.TotalSeconds - timePickerStart.Value.TimeOfDay.TotalSeconds

            newTimeId = timePickerStart.Value.TimeOfDay.TotalSeconds
            If regToEdit.HasBefore Then If newTimeId < regBefore.TimeId Then newTimeId += 86400
            regToEdit.TimeId = newTimeId

            If insertMode Then regToEdit.OriginalDate = regToEdit.TimeStart

            Dim change As TimelineItem = New TimelineItem()
            regToEdit.CopyPropertiesTo(change)
            change.Action = action
            listChanges.Add(change)

            If regToEdit.HasBefore Then
                regBefore.TimeEnd = regToEdit.TimeStart
                timeStart = regBefore.TimeStart.AddDays(adjustment)
                timeEnd = regBefore.TimeEnd.AddDays(adjustment)
                regBefore.Cycletime = timeEnd.TimeOfDay.TotalSeconds - timeStart.TimeOfDay.TotalSeconds

                newTimeId = timeStart.TimeOfDay.TotalSeconds
                If newTimeId < regBefore.TimeId Then newTimeId += 86400
                regBefore.TimeId = newTimeId

                If regBefore.Cycletime <> regBefore.OriginalCycletime Then
                    change = New TimelineItem()
                    regBefore.CopyPropertiesTo(change)
                    change.Action = "E"
                    listChanges.Add(change)
                End If
            End If

            If regToEdit.HasAfter Then
                regAfter.TimeStart = regToEdit.TimeEnd
                timeStart = regAfter.TimeStart.AddDays(adjustment)
                timeEnd = regAfter.TimeEnd.AddDays(adjustment)
                regAfter.Cycletime = timeEnd.TimeOfDay.TotalSeconds - timeStart.TimeOfDay.TotalSeconds

                newTimeId = timeStart.TimeOfDay.TotalSeconds
                If regToEdit.HasBefore Then If newTimeId < regBefore.TimeId Then newTimeId += 86400

                regAfter.TimeId = newTimeId

                If regAfter.TimeStart <> regAfter.OriginalDate Then
                    change = New TimelineItem()
                    regAfter.CopyPropertiesTo(change)
                    change.Action = "E"
                    listChanges.Add(change)
                End If
            End If

            If insertMode Then
                timelineItems.Insert(dgvTimeline.SelectedRows(0).Index + 1, regToEdit)
            End If

        Else

            Dim hasSelection As Boolean = False

            For Each row As DataGridViewRow In dgvTimeline.Rows

                If Boolean.Parse(row.Cells("Selected").Value) Then

                    Dim timelineItem As TimelineItem = timelineItems.FirstOrDefault(Function(m) m.TimeId = row.Cells("TimeId").Value)

                    Dim change = New TimelineItem()
                    timelineItem.CopyPropertiesTo(change)
                    change.Action = "E"
                    change.OriginalComments = timelineItem.Comments
                    change.OriginalCycletime = timelineItem.Cycletime
                    change.PartNumber = IIf(rdbPartNumber.Checked, txtPartNumber.Text, timelineItem.PartNumber)
                    change.OperatorName = IIf(rdbOperator.Checked, txtOperator.Text, timelineItem.OperatorName)
                    change.OriginalPartNumber = timelineItem.PartNumber
                    change.OriginalOperatorName = timelineItem.OperatorName

                    listChanges.Add(change)

                    timelineItem.PartNumber = IIf(rdbPartNumber.Checked, txtPartNumber.Text, timelineItem.PartNumber)
                    timelineItem.OperatorName = IIf(rdbOperator.Checked, txtOperator.Text, timelineItem.OperatorName)
                    hasSelection = True
                End If

            Next

            If Not hasSelection Then
                MessageBox.Show("No lines were selected to be changed.")
                Return
            End If

        End If

        editMode = False
        insertMode = False

        timelineSource.ResetBindings(False)
        dgvFillBars(dgvTimeline)

        dgvTimelineItems_SelectionChanged(Me, New EventArgs())
        btnEdit.Enabled = True
        btnNew.Enabled = True
        btnDeleteLines.Enabled = True
        btnApply.Enabled = False
        btnCancel.Enabled = False

        AllowSaveChanges()

    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        editMode = False
        insertMode = False

        btnEdit.Enabled = True
        btnNew.Enabled = True
        btnDeleteLines.Enabled = true
        btnApply.Enabled = False
        btnCancel.Enabled = False

        rdbStatus.Checked = False
        rdbPartNumber.Checked = False
        rdbOperator.Checked = False
        rdbStatus.Enabled = False
        rdbPartNumber.Enabled = False
        rdbOperator.Enabled = False

        'DisableEditMode()

        dgvTimelineItems_SelectionChanged(Me, New EventArgs())

    End Sub


    Private Sub btnDeleteLines_Click(sender As Object, e As EventArgs) Handles btnDeleteLines.Click

        Dim linesToRemove As List(Of TimelineItem) = New List(Of TimelineItem)()

        Dim newLine As Boolean = True

        Dim idxBefore As Integer = 0

        Dim idx As Integer = 0

        Dim hasSelectedLine = False

        Do While idx < dgvTimeline.Rows.Count
            If dgvTimeline.Rows(idx).Cells("Selected").Value Then
                hasSelectedLine = True
                Exit Do
            End If
            idx += 1
        Loop

        If Not hasSelectedLine Then
            MessageBox.Show("No records selected!")
            Return
        End If

        If dgvTimeline.Rows.Count = 1 Then
            MessageBox.Show("You can't delete the only one line of the table!")
            Return
        End If

        If Not MessageBox.Show("Do you confirm the exclusion of the selected records?", "Exclusion", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) = DialogResult.Yes Then
            Return
        End If

        idx = 0
        Do While idx < dgvTimeline.Rows.Count

            While Not dgvTimeline.Rows(idx).Cells("Selected").Value
                idx += 1
                If idx = dgvTimeline.Rows.Count Then Exit Do
            End While

            If idx = dgvTimeline.Rows.Count Then
                Exit Do
            End If

            If dgvTimeline.Rows.Count = 1 Then
                Exit Do
            End If

            Dim hasBefore As Boolean = False
            Dim lineBefore As TimelineItem
            Dim lineAfter As TimelineItem
            Dim accumulator As TimelineItem

            If idx > 0 Then
                hasBefore = True
                lineBefore = timelineItems.FirstOrDefault(Function(m) m.TimeId = dgvTimeline.Rows(idx - 1).Cells("TimeId").Value)
                lineBefore.Action = "E"
                listChanges.Add(lineBefore)
            End If

            Do While dgvTimeline.Rows(idx).Cells("Selected").Value

                If hasBefore Or idx < dgvTimeline.Rows.Count Then

                    Dim lineDelete As TimelineItem = timelineItems.FirstOrDefault(Function(m) m.TimeId = dgvTimeline.Rows(idx).Cells("TimeId").Value)
                    lineDelete.Action = "D"
                    listChanges.Add(lineDelete)

                    If hasBefore Then
                        lineBefore.Cycletime += lineDelete.Cycletime
                        lineBefore.TimeEnd = lineDelete.TimeEnd
                    Else
                        If accumulator Is Nothing Then
                            accumulator = New TimelineItem()
                            accumulator.TimeStart = lineDelete.TimeStart
                            accumulator.Cycletime = 0
                        End If
                        accumulator.Cycletime += lineDelete.Cycletime
                    End If

                    If Not hasBefore Then
                        lineAfter = timelineItems.FirstOrDefault(Function(m) m.TimeId = dgvTimeline.Rows(idx + 1).Cells("TimeId").Value)
                        lineAfter.Action = "E"
                        lineAfter.TimeStart = lineDelete.TimeStart
                        lineAfter.Cycletime += lineDelete.Cycletime
                    End If
                End If

                idx += 1
                If idx = dgvTimeline.Rows.Count Then Exit Do
            Loop

            If Not hasBefore And Not lineAfter Is Nothing Then
                listChanges.Add(lineAfter)
            End If
        Loop

        For Each line As TimelineItem In listChanges
            If line.Action = "D" Then
                Dim lineToDelete As TimelineItem = timelineItems.FirstOrDefault(Function(m) m.TimeId = line.TimeId)
                timelineItems.Remove(lineToDelete)
            End If
        Next

        timelineSource.ResetBindings(False)
        dgvFillBars(dgvTimeline)

        AllowSaveChanges()

    End Sub


    Private Sub DisableEditMode()

        Dim enable As Boolean = False

        btnStartChange.Enabled = True
        btnSaveChange.Enabled = enable
        btnCancelChange.Enabled = enable
        txtDescription.ReadOnly = True

        btnNew.Enabled = enable
        btnEdit.Enabled = enable
        btnDeleteLines.Enabled = enable
        btnApply.Enabled = enable
        btnCancel.Enabled = enable

        rdbStatus.Checked = False
        rdbStatus.Enabled = False
        rdbPartNumber.Checked = False
        rdbPartNumber.Enabled = False
        rdbOperator.Checked = False
        rdbOperator.Enabled = False

        rdbTimeline.Enabled = True
        rdbEdits.Enabled = True

        btnClearSelection_Click(Me, New EventArgs())
        editMode = False
        insertMode = False

    End Sub


    Private Sub EnableEditMode()

        Dim enable As Boolean = True

        btnStartChange.Enabled = Not enable
        btnCancelChange.Enabled = enable

        btnNew.Enabled = enable
        btnEdit.Enabled = enable
        btnDeleteLines.Enabled = enable

        rdbTimeline.Enabled = False
        rdbEdits.Enabled = False

    End Sub


    Private Sub rdbDisplay_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTimeline.CheckedChanged

        dgvTimeline.Visible = rdbTimeline.Checked
        dgvEdits.Visible = rdbEdits.Checked

        If dgvTimeline.Visible Then
            lblTitle.Text = "Timeline - Raw Data"
            btnStartChange.Enabled = True
            btnMarkSelected.Visible = True
            btnClearSelection.Visible = True
        Else
            lblTitle.Text = "Timeline - Edits"
            DisableEditMode()
            btnStartChange.Enabled = False
            btnMarkSelected.Visible = False
            btnClearSelection.Visible = False
        End If

    End Sub


    Private Sub rdbEdit_CheckedChanged(sender As Object, e As EventArgs) Handles rdbStatus.CheckedChanged, rdbPartNumber.CheckedChanged, rdbOperator.CheckedChanged

        pnlStatus.Enabled = rdbStatus.Checked
        pnlPartNumber.Enabled = rdbPartNumber.Checked
        pnlOperator.Enabled = rdbOperator.Checked

        If rdbStatus.Checked Then

            'Dim selectedIdx As Integer = dgvTimeline.SelectedRows(0).Index

            'regToEdit = timelineItems.FirstOrDefault(Function(m) m.TimeId = dgvTimeline.Rows(selectedIdx).Cells("TimeId").Value)
            'regToEdit.HasBefore = selectedIdx > 0
            'regToEdit.HasAfter = selectedIdx < dgvTimeline.Rows.Count - 1

            'minTimeStart = regToEdit.TimeStart
            'maxTimeStart = regToEdit.TimeStart
            'minTimeEnd = regToEdit.TimeEnd
            'maxTimeEnd = regToEdit.TimeEnd

            'regBefore = Nothing
            'If regToEdit.HasBefore Then
            '    regBefore = timelineItems.FirstOrDefault(Function(m) m.TimeId = dgvTimeline.Rows(selectedIdx - 1).Cells("TimeId").Value)
            '    minTimeStart = regBefore.TimeStart.AddSeconds(1)
            '    maxTimeStart = regToEdit.TimeEnd.AddSeconds(-1)
            '    minTimeEnd = regBefore.TimeStart.AddSeconds(2)
            'End If

            'regAfter = Nothing
            'If regToEdit.HasAfter Then
            '    regAfter = timelineItems.FirstOrDefault(Function(m) m.TimeId = dgvTimeline.Rows(selectedIdx + 1).Cells("TimeId").Value)
            '    maxTimeStart = regAfter.TimeEnd.AddSeconds(-2)
            '    minTimeEnd = regToEdit.TimeStart.AddSeconds(1)
            '    maxTimeEnd = regAfter.TimeEnd.AddSeconds(-1)
            'End If

            'lblLimitStartMin.Text = $"Min.- {minTimeStart.ToLongTimeString()}"
            'lblLimitStartMax.Text = $"Max.- {maxTimeStart.ToLongTimeString()}"
            'lblLimitEndMin.Text = $"Min.- {minTimeEnd.ToLongTimeString()}"
            'lblLimitEndMax.Text = $"Max.- {maxTimeEnd.ToLongTimeString()}"
            'lblLimitStartMin.ForeColor = Color.Blue
            'lblLimitEndMin.ForeColor = Color.Blue

            'cmbStatus.Enabled = True
            'timePickerStart.Enabled = minTimeStart <> maxTimeStart
            'timePickerEnd.Enabled = minTimeEnd <> maxTimeEnd

        End If

    End Sub


    Private Sub btnMarkSelected_Click(sender As Object, e As EventArgs) Handles btnMarkSelected.Click

        For Each row As DataGridViewRow In dgvTimeline.Rows
            If row.Selected Then
                row.Cells(1).Value = True
            End If
        Next

    End Sub


    Private Sub btnClearSelection_Click(sender As Object, e As EventArgs) Handles btnClearSelection.Click

        For Each row As DataGridViewRow In dgvTimeline.Rows
            row.Cells(1).Value = False
        Next

    End Sub

End Class



Public Class TimelineItem

    Private _timeId As Integer
    Public Property TimeId() As Integer
        Get
            Return _timeId
        End Get
        Set(ByVal value As Integer)
            _timeId = value
        End Set
    End Property

    Private _selected As Boolean
    Public Property Selected() As Boolean
        Get
            Return _selected
        End Get
        Set(ByVal value As Boolean)
            _selected = value
        End Set
    End Property

    Private _originalDate As DateTime
    Public Property OriginalDate() As DateTime
        Get
            Return _originalDate
        End Get
        Set(ByVal value As DateTime)
            _originalDate = value
        End Set
    End Property

    Private _action As String
    Public Property Action() As String
        Get
            Return _action
        End Get
        Set(ByVal value As String)
            _action = value
        End Set
    End Property

    Private _status As String
    Public Property Status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property

    Private _originalStatus As String
    Public Property OriginalStatus() As String
        Get
            Return _originalStatus
        End Get
        Set(ByVal value As String)
            _originalStatus = value
        End Set
    End Property

    Private _comments As String
    Public Property Comments() As String
        Get
            Return _comments
        End Get
        Set(ByVal value As String)
            _comments = value
        End Set
    End Property

    Private _originalComments As String
    Public Property OriginalComments() As String
        Get
            Return _originalComments
        End Get
        Set(ByVal value As String)
            _originalComments = value
        End Set
    End Property

    Private _partNr As String
    Public Property PartNumber() As String
        Get
            Return _partNr
        End Get
        Set(ByVal value As String)
            _partNr = value
        End Set
    End Property

    Private _originalPartNr As String
    Public Property OriginalPartNumber() As String
        Get
            Return _originalPartNr
        End Get
        Set(ByVal value As String)
            _originalPartNr = value
        End Set
    End Property

    Private _operator As String
    Public Property OperatorName() As String
        Get
            Return _operator
        End Get
        Set(ByVal value As String)
            _operator = value
        End Set
    End Property

    Private _originalOperator As String
    Public Property OriginalOperatorName() As String
        Get
            Return _originalOperator
        End Get
        Set(ByVal value As String)
            _originalOperator = value
        End Set
    End Property

    Private _timeStart As DateTime
    Public Property TimeStart() As DateTime
        Get
            Return _timeStart
        End Get
        Set(ByVal value As DateTime)
            _timeStart = value
        End Set
    End Property

    Private _timeEnd As DateTime
    Public Property TimeEnd() As DateTime
        Get
            Return _timeEnd
        End Get
        Set(ByVal value As DateTime)
            _timeEnd = value
        End Set
    End Property

    Private _shift As Integer
    Public Property Shift() As Integer
        Get
            Return _shift
        End Get
        Set(ByVal value As Integer)
            _shift = value
        End Set
    End Property

    Private _cycletime As Integer
    Public Property Cycletime() As Integer
        Get
            Return _cycletime
        End Get
        Set(ByVal value As Integer)
            _cycletime = value
        End Set
    End Property

    Private _originalCycletime As Integer
    Public Property OriginalCycletime() As Integer
        Get
            Return _originalCycletime
        End Get
        Set(ByVal value As Integer)
            _originalCycletime = value
        End Set
    End Property

    Private _hasBefore As Boolean
    Public Property HasBefore() As Boolean
        Get
            Return _hasBefore
        End Get
        Set(ByVal value As Boolean)
            _hasBefore = value
        End Set
    End Property

    Private _hasAfter As Boolean
    Public Property HasAfter() As Boolean
        Get
            Return _hasAfter
        End Get
        Set(ByVal value As Boolean)
            _hasAfter = value
        End Set
    End Property

End Class


Public Class EditItem

    Private _editId As Integer
    Public Property EditId() As Integer
        Get
            Return _editId
        End Get
        Set(ByVal value As Integer)
            _editId = value
        End Set
    End Property

    Private _action As String
    Public Property Action() As String
        Get
            Return _action
        End Get
        Set(ByVal value As String)
            _action = value
        End Set
    End Property

    Private _originalStatus As String
    Public Property OriginalStatus() As String
        Get
            Return _originalStatus
        End Get
        Set(ByVal value As String)
            _originalStatus = value
        End Set
    End Property

    Private _newStatus As String
    Public Property NewStatus() As String
        Get
            Return _newStatus
        End Get
        Set(ByVal value As String)
            _newStatus = value
        End Set
    End Property

    Private _originalTimeStart As DateTime
    Public Property OriginalTimeStart() As DateTime
        Get
            Return _originalTimeStart
        End Get
        Set(ByVal value As DateTime)
            _originalTimeStart = value
        End Set
    End Property

    Private _newTimeStart As DateTime
    Public Property NewTimeStart() As DateTime
        Get
            Return _newTimeStart
        End Get
        Set(ByVal value As DateTime)
            _newTimeStart = value
        End Set
    End Property

    Private _originalCycletime As Integer
    Public Property OriginalCycletime() As Integer
        Get
            Return _originalCycletime
        End Get
        Set(ByVal value As Integer)
            _originalCycletime = value
        End Set
    End Property

    Private _newCycletime As Integer
    Public Property NewCycletime() As Integer
        Get
            Return _newCycletime
        End Get
        Set(ByVal value As Integer)
            _newCycletime = value
        End Set
    End Property

    Private _originalPartNumber As String
    Public Property OriginalPartNumber() As String
        Get
            Return _originalPartNumber
        End Get
        Set(ByVal value As String)
            _originalPartNumber = value
        End Set
    End Property

    Private _newPartNumber As String
    Public Property NewPartNumber() As String
        Get
            Return _newPartNumber
        End Get
        Set(ByVal value As String)
            _newPartNumber = value
        End Set
    End Property

    Private _originalOperatorName As String
    Public Property OriginalOperatorName() As String
        Get
            Return _originalOperatorName
        End Get
        Set(ByVal value As String)
            _originalOperatorName = value
        End Set
    End Property

    Private _newOperatorName As String
    Public Property NewOperatorName() As String
        Get
            Return _newOperatorName
        End Get
        Set(ByVal value As String)
            _newOperatorName = value
        End Set
    End Property

    Private _originalComments As String
    Public Property OriginalComments() As String
        Get
            Return _originalComments
        End Get
        Set(ByVal value As String)
            _originalComments = value
        End Set
    End Property

    Private _newComments As String
    Public Property NewComments() As String
        Get
            Return _newComments
        End Get
        Set(ByVal value As String)
            _newComments = value
        End Set
    End Property


    Private _editWhen As DateTime
    Public Property EditWhen() As DateTime
        Get
            Return _editWhen
        End Get
        Set(ByVal value As DateTime)
            _editWhen = value
        End Set
    End Property

    Private _editBy As String
    Public Property EditBy() As String
        Get
            Return _editBy
        End Get
        Set(ByVal value As String)
            _editBy = value
        End Set
    End Property

    Private _editDescription As String
    Public Property EditDescription() As String
        Get
            Return _editDescription
        End Get
        Set(ByVal value As String)
            _editDescription = value
        End Set
    End Property

End Class



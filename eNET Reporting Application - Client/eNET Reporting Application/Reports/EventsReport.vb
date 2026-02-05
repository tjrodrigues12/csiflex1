Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities

Public Class EventsReport

    Dim connString As String = CSI_Library.CSI_Library.MySqlConnectionString

    Dim lstPartNumbers As List(Of ListPartNumberItem)
    Dim dicMachines As Dictionary(Of Integer, String)
    Dim lstMachines As List(Of String)
    Dim lstShifts As New List(Of Integer)(New Integer() {1, 2, 3})

    Dim dicMachineNameId As Dictionary(Of String, Integer)
    Dim lstMachPartNumbers As List(Of Tuple(Of Integer, String, String))

    Dim lstCheckedMachines As List(Of String)
    Dim lstCheckedPartNumb As List(Of String)

    Dim gridDetailsItems As List(Of ERGridItem)
    Dim gridDetailsSource As BindingSource

    Dim gridSummaryItems As List(Of ERGridItem)
    Dim gridSummarySource As BindingSource


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub EventsReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        rBtnDay.Checked = True
        dtpDayFrom.Value = DateTime.Today.AddDays(-1)
        dtpDayTo.Value = dtpDayFrom.Value
        chkShift1.Checked = True
        chkShift2.Checked = True
        chkShift3.Checked = True

        rBtnSearchForMachines.Checked = True

        lstPartNumbers = New List(Of ListPartNumberItem)()
        lstCheckedMachines = New List(Of String)()
        lstCheckedPartNumb = New List(Of String)()

        Dim tbMachines = MySqlAccess.GetDataTable("SELECT Id, Machine_Name FROM csi_auth.tbl_ehub_conf WHERE Monstate = 1 ORDER BY Machine_Name;", connString)
        dicMachineNameId = New Dictionary(Of String, Integer)()
        For Each machine As DataRow In tbMachines.Rows
            dicMachineNameId.Add(machine("Machine_Name"), machine("Id"))
        Next

        gridDetailsItems = New List(Of ERGridItem)()
        gridDetailsSource = New BindingSource()
        gridDetailsSource.DataSource = gridDetailsItems
        dgvDetails.AutoGenerateColumns = False
        dgvDetails.DataSource = gridDetailsSource

        gridSummaryItems = New List(Of ERGridItem)()
        gridSummarySource = New BindingSource()
        gridSummarySource.DataSource = gridSummaryItems
        dgvSummary.AutoGenerateColumns = False
        dgvSummary.DataSource = gridSummarySource

        LoadMachines()
        LoadPartNumbers()
        btnGenerateReport_Click(Me, New EventArgs())

    End Sub


    Private Sub rBtnPeriod_CheckedChanged(sender As Object, e As EventArgs) Handles rBtnDay.CheckedChanged, rBtnCustom.CheckedChanged

        dtpDayTo.Enabled = rBtnCustom.Checked
        chkShift1.Enabled = rBtnDay.Checked
        chkShift2.Enabled = rBtnDay.Checked
        chkShift3.Enabled = rBtnDay.Checked

        If Not dtpDayTo.Enabled Then
            dtpDayTo.Value = dtpDayFrom.Value
        Else
            chkShift1.Checked = True
            chkShift2.Checked = True
            chkShift3.Checked = True
        End If

    End Sub

    Private Sub dtpDayFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtpDayFrom.ValueChanged
        dtpDayTo.Value = dtpDayFrom.Value
    End Sub

    Private Sub chkShift_CheckedChanged(sender As Object, e As EventArgs) Handles chkShift1.CheckedChanged, chkShift2.CheckedChanged, chkShift3.CheckedChanged

        Dim check = DirectCast(sender, CheckBox)

        If check.Name.EndsWith("1") Then
            If check.Checked Then lstShifts.Add(1) Else lstShifts.Remove(1)
        ElseIf check.Name.EndsWith("2") Then
            If check.Checked Then lstShifts.Add(2) Else lstShifts.Remove(2)
        ElseIf check.Name.EndsWith("3") Then
            If check.Checked Then lstShifts.Add(3) Else lstShifts.Remove(3)
        End If
    End Sub

    Private Sub btnReloadMachines_Click(sender As Object, e As EventArgs) Handles btnReloadMachines.Click
        LoadMachines()
        LoadPartNumbers()
        btnGenerateReport_Click(Me, New EventArgs())
    End Sub


    Private Sub rBtnSearch_CheckedChanged(sender As Object, e As EventArgs) Handles rBtnSearchForMachines.CheckedChanged

        Dim pointLbl1 As Point = New Point(164, 14)
        Dim pointLbl2 As Point = New Point(404, 14)

        Dim pointBox1 As Point = New Point(164, 42)
        Dim pointBox2 As Point = New Point(404, 42)

        Dim pointChk1 As Point = New Point(166, 176)
        Dim pointChk2 As Point = New Point(406, 176)

        If rBtnSearchForMachines.Checked Then
            tViewMachines.Visible = True
            lBoxMachines.Visible = False
            lblMachines.Location = pointLbl1
            lblPartNumbers.Location = pointLbl2
            'tViewMachines.Location = pointBox1
            lboxPartNumber.Location = pointBox2

            chkAllPartNumbers.Location = pointChk2
            chkAllPartNumbers.Visible = True
            chkAllMachines.Visible = False
        Else
            tViewMachines.Visible = False
            lBoxMachines.Visible = True
            lblPartNumbers.Location = pointLbl1
            lblMachines.Location = pointLbl2
            lboxPartNumber.Location = pointBox1
            'tViewMachines.Location = pointBox2

            chkAllPartNumbers.Location = pointChk1
            chkAllPartNumbers.Visible = True
            chkAllMachines.Visible = True
        End If

    End Sub

    Private Sub tViewMachines_AfterCheck(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tViewMachines.AfterCheck

        RemoveHandler tViewMachines.AfterCheck, AddressOf tViewMachines_AfterCheck

        CheckAllChildNodes(e.Node)

        CheckParentNode(e.Node)

        If e.Node.Nodes.Count = 0 Then
            Dim mach = e.Node.Text
            For i As Integer = 0 To lBoxMachines.Items.Count - 1
                If lBoxMachines.Items(i).ToString() = mach Then
                    lBoxMachines.SetItemChecked(i, e.Node.Checked)
                End If
            Next

            Try
                If e.Node.Checked Then
                    lstCheckedMachines.AddOrIgnore(mach)
                Else
                    lstCheckedMachines.Remove(mach)
                End If
            Catch ex As Exception
            End Try
        End If

        LoadPartNumbers()

        AddHandler tViewMachines.AfterCheck, AddressOf tViewMachines_AfterCheck
    End Sub

    Private Sub chkAllPartNumbers_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllPartNumbers.CheckedChanged
        For i As Integer = 0 To lboxPartNumber.Items.Count - 1
            lboxPartNumber.SetItemChecked(i, chkAllPartNumbers.Checked)
        Next
    End Sub

    Private Sub lboxPartNumber_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lboxPartNumber.ItemCheck

        RemoveHandler lBoxMachines.ItemCheck, AddressOf lBoxMachines_ItemCheck

        Dim itemChecked = lboxPartNumber.Items(e.Index)
        Dim itemNewValue = e.NewValue

        Try
            If itemNewValue = CheckState.Checked Then
                lstCheckedPartNumb.Add(itemChecked)
            Else
                lstCheckedPartNumb.Remove(itemChecked)
            End If
        Catch ex As Exception
        End Try

        lBoxMachines.DataSource = lstMachPartNumbers.Where(Function(w) lstCheckedPartNumb.Contains(w.Item3)).Select(Function(x) x.Item2).Distinct().OrderBy(Function(y) y).ToList()
        For i As Integer = 0 To lBoxMachines.Items.Count - 1
            lBoxMachines.SetItemChecked(i, True)
        Next

        AddHandler lBoxMachines.ItemCheck, AddressOf lBoxMachines_ItemCheck

    End Sub

    Private Sub chkAllMachines_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllMachines.CheckedChanged
        For i As Integer = 0 To lBoxMachines.Items.Count - 1
            lBoxMachines.SetItemChecked(i, chkAllMachines.Checked)
        Next
    End Sub

    Private Sub lBoxMachines_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lBoxMachines.ItemCheck

        RemoveHandler lboxPartNumber.ItemCheck, AddressOf lboxPartNumber_ItemCheck

        Dim itemChecked = lBoxMachines.Items(e.Index)
        Dim itemNewValue = e.NewValue

        Try
            If itemNewValue = CheckState.Checked Then
                lstCheckedMachines.AddOrIgnore(itemChecked)
            Else
                lstCheckedMachines.Remove(itemChecked)
            End If
        Catch ex As Exception
        End Try

        AddHandler lboxPartNumber.ItemCheck, AddressOf lboxPartNumber_ItemCheck

    End Sub


    Private Sub btnGenerateReport_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        lstMachines = New List(Of String)()
        dicMachines = New Dictionary(Of Integer, String)()

        If lstCheckedMachines.Count = 0 Then
            MessageBox.Show("No machine selected!")
            Return
        End If

        gridDetailsItems.Clear()
        gridSummaryItems.Clear()

        lstCheckedMachines.Sort()

        For Each machineName As String In lstCheckedMachines

            LoadGridDetails(machineName)

            LoadGridSummary(machineName)

        Next

        gridDetailsSource.ResetBindings(False)

    End Sub

    Private Sub btnExportCsv_Click(sender As Object, e As EventArgs) Handles btnExportCsv.Click

        If TabControl.SelectedIndex = 0 Then
            DGVExportCsv.DataGridViewExportCsv(dgvSummary, $"CSIFLEX_SummaryReportCSV_{DateTime.Now.ToString("yyyyMMdd_HHmm")}", CSIFLEXSettings.Instance.ReportFolder)
        Else
            DGVExportCsv.DataGridViewExportCsv(dgvDetails, $"CSIFLEX_DetailsReportCSV_{DateTime.Now.ToString("yyyyMMdd_HHmm")}", CSIFLEXSettings.Instance.ReportFolder)
        End If
    End Sub


    Private Sub LoadGridDetails(machineName As String)

        Dim shift As Integer = 0
        Dim partNumber As String = ""
        Dim query = $"SELECT * FROM csi_database.vw_machinestate WHERE MachineId = {dicMachineNameId(machineName)} AND EventDateTime >= '{dtpDayFrom.Value.ToString("yyyy-MM-dd")}' AND EventDateTime < '{dtpDayTo.Value.AddDays(1).ToString("yyyy-MM-dd 12:00:00")}' ORDER BY EventDateTime;"

        Dim table As DataTable = MySqlAccess.GetDataTable(query, connString)
        Dim gridItem As ERGridItem

        For Each row As DataRow In table.Rows

            partNumber = row("PartNumber")
            shift = row("shift")

            If lstCheckedPartNumb.Contains(partNumber) And lstShifts.Contains(shift) Then

                Dim eventDate As DateTime = row("EventDateTime")

                gridItem = New ERGridItem()
                gridItem.MachineId = row("MachineId")
                gridItem.MachineName = machineName
                gridItem.DateTime = eventDate.ToString("yyyy-MM-dd HH:mm:ss")
                gridItem.DateTimeId = (eventDate.DayOfYear * 86400) + eventDate.TimeOfDay.Seconds()
                gridItem.Shift = row("Shift")
                gridItem.State = row("Status")
                gridItem.PartNumber = partNumber
                gridItem.Operation = row("Operation")
                gridItem.Operator = row("OperatorRefId")
                gridItem.FeedrateOvr = IIf(Not IsDBNull(row("FeedOverride")), row("FeedOverride"), "")
                gridItem.RapidOvr = IIf(Not IsDBNull(row("RapidOverride")), row("RapidOverride"), "")
                gridItem.SpindleOvr = IIf(Not IsDBNull(row("SpindleOverride")), row("SpindleOverride"), "")

                If shift = 0 And gridItem.Shift > 1 Then
                Else
                    If gridItem.DateTime > dtpDayTo.Value.AddDays(1) And gridItem.Shift = 1 Then
                        Exit For
                    End If

                    shift = gridItem.Shift
                    If rBtnDay.Checked And ((chkShift1.Checked And gridItem.Shift = 1) Or (chkShift2.Checked And gridItem.Shift = 2) Or (chkShift3.Checked And gridItem.Shift = 3)) Then
                        gridDetailsItems.Add(gridItem)
                    ElseIf Not rBtnDay.Checked Then
                        gridDetailsItems.Add(gridItem)
                    End If

                End If

            End If
        Next

    End Sub

    Private Sub LoadGridSummary(machineName As String)

        Dim shift As Integer = 0
        Dim partNumber As String = ""

        Dim machId = dicMachineNameId(machineName)
        Dim tbName = CSIFLEXSettings.MachinesIdNames(machId).Item3
        Dim dateStart As String = dtpDayFrom.Value.ToString("yyyy-MM-dd")
        Dim dateEnd As String = dtpDayTo.Value.AddDays(1).ToString("yyyy-MM-dd")

        Dim query = $"SELECT * FROM csi_database.{tbName} WHERE ShiftDate >= '{dateStart}' AND ShiftDate < '{dateEnd}' AND status NOT LIKE '\\_%';"
        Dim tbChanges = MySqlAccess.GetDataTable(query, connString)


        Dim dicMachPNumCycle As Dictionary(Of String, (qttCycles As Integer, ttlCycles As Integer)) = New Dictionary(Of String, (Integer, Integer))()
        Dim dicMachPNumCycleAvg As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)()
        Dim key As String
        Dim cycleTime As Integer
        Dim status As String

        For Each row As DataRow In tbChanges.Rows
            key = $"PN:{row("PartNumber")},OP:{row("Operation")},OP:{row("Operator")}"

            If Not dicMachPNumCycle.ContainsKey(key) Then
                dicMachPNumCycle.Add(key, (qttCycles:=0, ttlCycles:=0))
                dicMachPNumCycleAvg.Add(key, 0)
            End If

            status = row("Status")
            cycleTime = row("CycleTime")
            If status = "CYCLE ON" Then
                Dim tup = (qttCycles:=dicMachPNumCycle(key).qttCycles + 1, ttlCycles:=dicMachPNumCycle(key).ttlCycles + cycleTime)
                dicMachPNumCycle(key) = tup
                dicMachPNumCycleAvg(key) = tup.ttlCycles / tup.qttCycles
            End If
        Next

        Dim gridItem As ERGridItem
        Dim avgCycle As Integer

        For Each row As DataRow In tbChanges.Rows

            key = $"PN:{row("PartNumber")},OP:{row("Operation")},OP:{row("Operator")}"

            avgCycle = 0
            If row("Status") = "CYCLE ON" Then
                avgCycle = dicMachPNumCycleAvg(key)
            End If

            partNumber = row("PartNumber")
            shift = row("shift")

            If lstCheckedPartNumb.Contains(partNumber) And lstShifts.Contains(shift) Then

                Dim eventDate As DateTime = row("Date_")

                gridItem = New ERGridItem()
                gridItem.MachineId = machId
                gridItem.MachineName = machineName
                gridItem.DateTime = eventDate.ToString("yyyy-MM-dd HH:mm:ss")
                gridItem.DateTimeId = (eventDate.DayOfYear * 86400) + eventDate.TimeOfDay.TotalSeconds
                gridItem.Shift = row("Shift")
                gridItem.State = row("Status")
                gridItem.Comment = IIf(Not IsDBNull(row("Comments")), row("Comments"), "")
                gridItem.PartNumber = row("PartNumber")
                gridItem.Operation = row("Operation")
                gridItem.Operator = row("Operator")
                gridItem.CycleTimeSec = row("CycleTime")
                gridItem.AverageCycle = avgCycle
                gridSummaryItems.Add(gridItem)
            End If

        Next

        gridSummarySource.ResetBindings(False)

        'dgvFillBars(dgvSummary)

    End Sub

    Private Sub LoadMachPartNumbers()

        lstMachPartNumbers = New List(Of Tuple(Of Integer, String, String))()

        Dim dateStart As String = dtpDayFrom.Value.ToString("yyyy-MM-dd")
        Dim dateEnd As String = dtpDayTo.Value.AddDays(1).ToString("yyyy-MM-dd")
        Dim query As String = $"SELECT MachineId, Machine_Name, PartNumber FROM csi_database.vw_machinestate WHERE EventDateTime > '{dateStart}' AND EventDateTime < '{dateEnd}' GROUP BY MachineId, Machine_Name, PartNumber;"

        Dim dbPartNumber = MySqlAccess.GetDataTable(query, connString)

        For Each row As DataRow In dbPartNumber.Rows
            Dim machId As Integer = row("MachineId")
            Dim machName As String = row("Machine_Name")
            Dim partNumber As String = row("PartNumber")
            lstMachPartNumbers.Add(New Tuple(Of Integer, String, String)(machId, machName, partNumber))
        Next

    End Sub

    Private Sub LoadPartNumbers()

        LoadMachPartNumbers()

        lboxPartNumber.DataSource = lstMachPartNumbers.Select(Function(x) x.Item3).Distinct().OrderBy(Function(y) y).ToList()
        lBoxMachines.DataSource = lstMachPartNumbers.Select(Function(x) x.Item2).Distinct().OrderBy(Function(y) y).ToList()

        For i As Integer = 0 To lboxPartNumber.Items.Count - 1
            lboxPartNumber.SetItemChecked(i, True)
        Next

        'For i As Integer = 0 To lBoxMachines.Items.Count - 1
        '    lBoxMachines.SetItemChecked(i, True)
        'Next

    End Sub

    Private Sub LoadMachines()

        Dim mchLine As String = ""
        Dim mchName As String = ""
        Dim mchPref As String = ""
        Dim nodeLevel As Integer = 0
        Dim currentNode As TreeNode = New TreeNode()
        Dim allMachinesNode As TreeNode

        tViewMachines.Nodes.Clear()

        For Each machine As String In CSIFLEXSettings.GroupMachines

            mchName = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.EnetName = machine Or m.Id.ToString() = machine).MachineName
            If mchName Is Nothing Then mchName = machine

            mchPref = mchName.Substring(0, 3)

            Dim node As TreeNode = New TreeNode()

            If mchName.Length > 4 Then
                node.Text = mchName.Substring(4)
            Else
                node.Text = mchName
            End If

            If mchPref = "_MT" Then

                tViewMachines.Nodes.Add(node)
                currentNode = node
                nodeLevel = 1

            ElseIf mchPref = "_ST" Then

                If nodeLevel = 1 Then
                    currentNode.Nodes.Add(node)
                Else
                    currentNode.Parent.Nodes.Add(node)
                End If
                currentNode = node
                nodeLevel = 2

                If node.Text.ToUpper().StartsWith("ALL MACH") Then
                    allMachinesNode = node
                End If
            Else
                Dim newNode = currentNode.Nodes.Add(mchName, mchName)

                Try
                    lstCheckedMachines.AddOrIgnore(mchName)
                Catch ex As Exception
                End Try
            End If

        Next

        allMachinesNode.Checked = True

        tViewMachines.Nodes(0).Expand()
        tViewMachines.Nodes(0).Nodes(0).Expand()

    End Sub

    Private Sub LoadMachinesList(parentNode As TreeNode)

        Dim mchName = ""
        Dim mchId = 0
        Dim machine As KeyValuePair(Of Integer, Tuple(Of String, String, String))

        For Each childNode As TreeNode In parentNode.Nodes
            If childNode.Nodes.Count > 0 Then
                LoadMachinesList(childNode)
            Else
                If childNode.Checked Then

                    mchName = childNode.Text
                    machine = CSIFLEXSettings.MachinesIdNames.FirstOrDefault(Function(m) m.Value.Item1 = mchName)

                    If machine.Key <> 0 And Not dicMachines.ContainsValue(mchName) Then
                        dicMachines.Add(machine.Key, childNode.Text)
                    End If

                End If
            End If
        Next

    End Sub

    Private Sub CheckAllChildNodes(parentNode As TreeNode)

        For Each childNode As TreeNode In parentNode.Nodes
            childNode.Checked = parentNode.Checked
            CheckAllChildNodes(childNode)
        Next

    End Sub

    Private Sub CheckParentNode(node As TreeNode)

        Dim parentNode As TreeNode = node.Parent
        Dim checkParent As Boolean = True

        If node.Parent Is Nothing Then Return

        For Each childNode As TreeNode In parentNode.Nodes
            If Not childNode.Checked Then
                checkParent = False
            End If
        Next

        parentNode.Checked = checkParent

    End Sub

    Private Sub dgvFillBars(dgv As DataGridView)

        Dim action As String
        Dim status As String
        Dim cycletime As Integer
        Dim statusImage As Image
        Dim imageSize As Integer
        Dim maxValue As Integer
        Dim avgValue As Integer
        Dim avgMaxValue As Integer
        Dim avgMinValue As Integer

        If dgv.Rows.Count = 0 Then Return

        For Each row As DataGridViewRow In dgv.Rows

            status = row.Cells("col2State").Value
            cycletime = row.Cells("col2CycletimeSec").Value
            avgValue = row.Cells("col2CycleAvg").Value
            maxValue = avgValue * 2
            avgMinValue = avgValue * 0.9
            avgMaxValue = avgValue * 1.1

            If status = "CYCLE ON" Then

                imageSize = Int((cycletime * 150) / maxValue)

                If imageSize <= 0 Then
                    imageSize = 2
                End If

                If cycletime < avgMinValue Then
                    statusImage = New Bitmap(ImageList.Images("RedBar"), imageSize, 18)
                ElseIf cycletime > avgMaxValue Then
                    statusImage = New Bitmap(ImageList.Images("YellowBar"), imageSize, 18)
                Else
                    statusImage = New Bitmap(ImageList.Images("GreenBar"), imageSize, 18)
                End If

                row.Cells("col2Graphic").Value = statusImage
            Else
                row.Cells("col2Graphic").Value = New Bitmap(ImageList.Images("WhiteBar"), 150, 18)
            End If

            'row.Cells("col2Graphic").Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        Next

        dgv.Columns()("col2Graphic").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

    End Sub

    Private Sub tViewMachines_Resize(sender As Object, e As EventArgs) Handles tViewMachines.Resize
        lBoxMachines.Size = New Size(lBoxMachines.Width, tViewMachines.Height - 20)
    End Sub
End Class


Public Class ListPartNumberItem

    Private _machineId As Integer
    Private _machineName As String
    Private _partNumber As String

    Public Property MachineId As Integer
        Get
            Return _machineId
        End Get
        Set(value As Integer)
            _machineId = value
        End Set
    End Property

    Public Property MachineName As String
        Get
            Return _machineName
        End Get
        Set(value As String)
            _machineName = value
        End Set
    End Property

    Public Property PartNumber As String
        Get
            Return _partNumber
        End Get
        Set(value As String)
            _partNumber = value
        End Set
    End Property
End Class

Public Class ERGridItem

    'Private _id As Integer
    Private _machineId As Integer
    Private _dateTime As String
    Private _dateTimeId As Integer
    Private _shift As Integer
    Private _shiftDate As DateTime
    Private _machineName As String
    Private _state As String
    Private _partNumber As String
    Private _operation As String
    Private _operator As String
    Private _feedrateOvr As String
    Private _rapidOvr As String
    Private _spindleOvr As String
    Private _cycleTimeSec As Integer
    Private _averageCycle As Integer
    Private _comment As String

    'Public Property Id As Integer
    '    Get
    '        Return _id
    '    End Get
    '    Set(value As Integer)
    '        _id = value
    '    End Set
    'End Property

    Public Property MachineId As Integer
        Get
            Return _machineId
        End Get
        Set(value As Integer)
            _machineId = value
        End Set
    End Property

    Public Property DateTime As String
        Get
            Return _dateTime
        End Get
        Set(value As String)
            _dateTime = value
        End Set
    End Property

    Public Property DateTimeId As Integer
        Get
            Return _dateTimeId
        End Get
        Set(value As Integer)
            _dateTimeId = value
        End Set
    End Property

    Public Property Shift As Integer
        Get
            Return _shift
        End Get
        Set(value As Integer)
            _shift = value
        End Set
    End Property

    Public Property ShiftDate As Date
        Get
            Return _shiftDate
        End Get
        Set(value As Date)
            _shiftDate = value
        End Set
    End Property

    Public Property MachineName As String
        Get
            Return _machineName
        End Get
        Set(value As String)
            _machineName = value
        End Set
    End Property

    Public Property State As String
        Get
            Return _state
        End Get
        Set(value As String)
            _state = value
        End Set
    End Property

    Public Property PartNumber As String
        Get
            Return _partNumber
        End Get
        Set(value As String)
            _partNumber = value
        End Set
    End Property

    Public Property Operation As String
        Get
            Return _operation
        End Get
        Set(value As String)
            _operation = value
        End Set
    End Property

    Public Property [Operator] As String
        Get
            Return _operator
        End Get
        Set(value As String)
            _operator = value
        End Set
    End Property

    Public Property FeedrateOvr As String
        Get
            Return _feedrateOvr
        End Get
        Set(value As String)
            _feedrateOvr = value
        End Set
    End Property

    Public Property RapidOvr As String
        Get
            Return _rapidOvr
        End Get
        Set(value As String)
            _rapidOvr = value
        End Set
    End Property

    Public Property SpindleOvr As String
        Get
            Return _spindleOvr
        End Get
        Set(value As String)
            _spindleOvr = value
        End Set
    End Property

    Public Property CycleTimeSec As Integer
        Get
            Return _cycleTimeSec
        End Get
        Set(value As Integer)
            _cycleTimeSec = value
        End Set
    End Property

    Public ReadOnly Property CycleTimeHrs As String
        Get
            Return (_cycleTimeSec / 3600).ToString("0.0000000")
        End Get
    End Property

    Public Property AverageCycle As Integer
        Get
            Return _averageCycle
        End Get
        Set(value As Integer)
            _averageCycle = value
        End Set
    End Property

    Public Property Comment As String
        Get
            Return _comment
        End Get
        Set(value As String)
            _comment = value
        End Set
    End Property

End Class
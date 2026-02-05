Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities

Public Class CSVFilterForm

    Dim rootPath As String = CSI_Library.CSI_Library.ClientRootPath
    Dim connString As String = CSI_Library.CSI_Library.MySqlConnectionString

    Dim dicMachines As Dictionary(Of Integer, String)
    Dim lstMachines As List(Of String)

    Dim gridReportItems As List(Of GridReportItem)
    Dim gridReportSource As BindingSource

    Dim periodStart As DateTime
    Dim periodEnd As DateTime


    Private Sub CSVFilterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim boxLocation As Point = New Point(10, 80)
        Dim boxFilterSize As Size = New Size(666, 330)
        Dim boxGridLocation As Point = New Point(14, 350)
        Dim boxGridSize As Size = New Size(960, 400)
        Dim meFormSize As Size = New Size(1000, 800)

        gboxDay.Location = boxLocation
        gboxDay.Visible = False
        chkShift1.Checked = True
        chkShift2.Checked = True
        chkShift3.Checked = True

        gboxWeek.Location = boxLocation
        gboxWeek.Visible = False

        gboxMonth.Location = boxLocation
        gboxMonth.Visible = False

        gboxCustom.Location = boxLocation
        gboxCustom.Visible = False

        gboxFilter.Size = boxFilterSize

        Me.Size = meFormSize

        gboxGrid.Location = boxGridLocation
        gboxGrid.Size = boxGridSize

        cmbWeekStart.SelectedIndex = 0
        dtpDay.Value = DateTime.Today.AddDays(-1)
        dtpWeek.Value = DateTime.Today.AddDays(-7)
        dtpMonth.Value = DateTime.Today.AddMonths(-1)
        rBtnDay.Checked = True

        gridReportItems = New List(Of GridReportItem)()
        gridReportSource = New BindingSource()
        gridReportSource.DataSource = gridReportItems

        dgvReport.AutoGenerateColumns = False
        dgvReport.DataSource = gridReportSource

        LoadMachines()

    End Sub


    Private Sub rBtnPeriods_CheckedChanged(sender As Object, e As EventArgs) Handles rBtnDay.CheckedChanged, rBtnWeek.CheckedChanged, rBtnMonth.CheckedChanged, rBtnCustom.CheckedChanged

        gboxDay.Visible = rBtnDay.Checked
        gboxWeek.Visible = rBtnWeek.Checked
        gboxMonth.Visible = rBtnMonth.Checked
        gboxCustom.Visible = rBtnCustom.Checked

    End Sub


    Private Sub LoadMachines()

        Dim file As System.IO.StreamReader
        Dim mchLine As String = ""
        Dim mchName As String = ""
        Dim mchPref As String = ""
        Dim nodeLevel As Integer = 0
        Dim currentNode As TreeNode

        'file = My.Computer.FileSystem.OpenTextFileReader(rootPath & "\sys\MonList.csys")

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

            Else
                Dim newNode = currentNode.Nodes.Add(mchName, mchName)
            End If

        Next


        'While Not file.EndOfStream

        '    mchLine = file.ReadLine()
        '    mchName = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.EnetName = mchLine Or m.Id.ToString() = mchLine).MachineName
        '    If String.IsNullOrEmpty(mchName) Then mchName = mchLine

        '    mchPref = mchName.Substring(0, 3)

        '    Dim node As TreeNode = New TreeNode()

        '    If mchName.Length > 4 Then
        '        node.Text = mchName.Substring(4)
        '    Else
        '        node.Text = mchName
        '    End If

        '    If mchPref = "_MT" Then

        '        tViewMachines.Nodes.Add(node)
        '        currentNode = node
        '        nodeLevel = 1

        '    ElseIf mchPref = "_ST" Then

        '        If nodeLevel = 1 Then
        '            currentNode.Nodes.Add(node)
        '        Else
        '            currentNode.Parent.Nodes.Add(node)
        '        End If
        '        currentNode = node
        '        nodeLevel = 2

        '    Else
        '        currentNode.Nodes.Add(mchName, mchName)
        '    End If

        'End While

        tViewMachines.Nodes(0).Expand()
        tViewMachines.Nodes(0).Nodes(0).Expand()

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


    Private Sub tViewMachines_AfterCheck(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tViewMachines.AfterCheck

        RemoveHandler tViewMachines.AfterCheck, AddressOf tViewMachines_AfterCheck

        CheckAllChildNodes(e.Node)

        CheckParentNode(e.Node)

        AddHandler tViewMachines.AfterCheck, AddressOf tViewMachines_AfterCheck

    End Sub


    Private Sub btnGenerateReport_Click(sender As Object, e As EventArgs) Handles btnGenerateReport.Click

        lstMachines = New List(Of String)()
        dicMachines = New Dictionary(Of Integer, String)()

        For Each childNode As TreeNode In tViewMachines.Nodes
            LoadMachinesList(childNode)
        Next

        If dicMachines.Count = 0 Then
            MessageBox.Show("No machine selected!")
            Return
        End If

        gridReportItems.Clear()

        For Each item As KeyValuePair(Of Integer, String) In dicMachines

            Dim shift As Integer = 0
            Dim query = $"SELECT * FROM csi_database.tbl_machinestate WHERE MachineId = {item.Key} AND EventDateTime >= '{periodStart.ToString("yyyy-MM-dd 00:00:00")}' AND EventDateTime < '{periodEnd.ToString("yyyy-MM-dd 12:00:00")}' "

            Dim table As DataTable = MySqlAccess.GetDataTable(query, connString)
            Dim gridItem As GridReportItem

            For Each row As DataRow In table.Rows

                gridItem = New GridReportItem()
                gridItem.MachineId = row("MachineId")
                gridItem.MachineName = item.Value
                gridItem.EventDateTime = row("EventDateTime")
                gridItem.Shift = row("Shift")
                gridItem.Status = row("Status")
                gridItem.Comment = IIf(Not IsDBNull(row("Comment")), row("Comment"), "")
                gridItem.PartNumber = row("PartNumber")
                gridItem.Operation = row("Operation")
                gridItem.PartCount = IIf(Not IsDBNull(row("PartCount")), row("PartCount"), 0)
                gridItem.Feedrate = IIf(Not IsDBNull(row("FeedOverride")), row("FeedOverride"), "")
                gridItem.Rapid = IIf(Not IsDBNull(row("RapidOverride")), row("RapidOverride"), "")
                gridItem.Spindle = IIf(Not IsDBNull(row("SpindleOverride")), row("SpindleOverride"), "")

                If shift = 0 And gridItem.Shift > 1 Then
                Else
                    If gridItem.EventDateTime > periodEnd And gridItem.Shift = 1 Then
                        Exit For
                    End If

                    shift = gridItem.Shift
                    If rBtnDay.Checked And ((chkShift1.Checked And gridItem.Shift = 1) Or (chkShift2.Checked And gridItem.Shift = 2) Or (chkShift3.Checked And gridItem.Shift = 3)) Then
                        gridReportItems.Add(gridItem)
                    ElseIf Not rBtnDay.Checked Then
                        gridReportItems.Add(gridItem)
                    End If

                End If

            Next
        Next

        gridReportSource.ResetBindings(False)

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

    Private Sub DayCalcPeriod(sender As Object, e As EventArgs) Handles rBtnDay.CheckedChanged, dtpDay.ValueChanged

        periodStart = dtpDay.Value.Date
        periodEnd = periodStart.AddDays(1)

    End Sub

    Private Sub WeekCalcPeriod(sender As Object, e As EventArgs) Handles rBtnWeek.CheckedChanged, dtpWeek.ValueChanged, cmbWeekStart.SelectedIndexChanged

        If cmbWeekStart.SelectedIndex < 0 Then Return

        Dim weekDay = dtpWeek.Value.DayOfWeek
        periodStart = dtpWeek.Value.Date.AddDays(weekDay * -1)
        periodEnd = periodStart.AddDays(7)

        If cmbWeekStart.SelectedIndex = 1 Then
            periodStart = periodStart.AddDays(1)
            periodEnd = periodEnd.AddDays(1)
        End If

        Dim msg = $"From  {periodStart.ToShortDateString}  to  {periodEnd.AddDays(-1).ToShortDateString}"

        lblWeekPeriod.Text = msg

    End Sub

    Private Sub MonthCalcPeriod(sender As Object, e As EventArgs) Handles rBtnMonth.CheckedChanged, dtpMonth.ValueChanged

        Dim day = dtpMonth.Value.Day - 1
        periodStart = dtpMonth.Value.Date.AddDays(day * -1)
        periodEnd = periodStart.AddMonths(1)

        Dim msg = $"From  {periodStart.ToShortDateString}  to  {periodEnd.AddDays(-1).ToShortDateString}"

        lblMonthPeriod.Text = msg

    End Sub

    Private Sub CustomCalcPeriod(sender As Object, e As EventArgs) Handles rBtnCustom.CheckedChanged, dtpCustomStart.ValueChanged, dtpCustomEnd.ValueChanged

        periodStart = dtpCustomStart.Value.Date
        periodEnd = dtpCustomEnd.Value.Date.AddDays(1)

    End Sub

    Private Sub btnExportCsv_Click(sender As Object, e As EventArgs) Handles btnExportCsv.Click
        DGVExportCsv.DataGridViewExportCsv(dgvReport, $"CSIFLEX_reportCSV_{DateTime.Now.ToString("yyyyMMdd_HHmm")}", CSIFLEXSettings.Instance.ReportFolder)
    End Sub

End Class

Public Class GridReportItem
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

    Private _eventDatetime As DateTime
    Public Property EventDateTime() As DateTime
        Get
            Return _eventDatetime
        End Get
        Set(ByVal value As DateTime)
            _eventDatetime = value
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

    Private _status As String
    Public Property Status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property

    Private _comment As String
    Public Property Comment() As String
        Get
            Return _comment
        End Get
        Set(ByVal value As String)
            _comment = value
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

    Private _operation As String
    Public Property Operation() As String
        Get
            Return _operation
        End Get
        Set(ByVal value As String)
            _operation = value
        End Set
    End Property

    Private _partCount As Integer
    Public Property PartCount() As Integer
        Get
            Return _partCount
        End Get
        Set(ByVal value As Integer)
            _partCount = value
        End Set
    End Property

    Private _feedrate As String
    Public Property Feedrate() As String
        Get
            Return _feedrate
        End Get
        Set(ByVal value As String)
            _feedrate = value
        End Set
    End Property

    Private _rapid As String
    Public Property Rapid() As String
        Get
            Return _rapid
        End Get
        Set(ByVal value As String)
            _rapid = value
        End Set
    End Property

    Private _spindle As String
    Public Property Spindle() As String
        Get
            Return _spindle
        End Get
        Set(ByVal value As String)
            _spindle = value
        End Set
    End Property
End Class
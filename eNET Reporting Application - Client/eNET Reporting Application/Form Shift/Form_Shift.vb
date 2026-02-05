Imports System.Globalization
Imports System.Text
Imports System.Windows
Imports System.Windows.Forms.DataVisualization.Charting
Imports CSI_Library.CSI_Library
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities

Public Class Form_Shift

    Public shiftno As Integer = 0
    Public machineId As Integer = 0
    Public machineName As String = ""
    Public enetMachineName As String = ""
    Public dateTimeline As DateTime

    Public Structure periode
        Dim machine_name As String
        Dim date_ As String
        Dim shift As Dictionary(Of String, Double)
    End Structure


    Public h As Integer, h0 As Integer
    Public saved_points As New Dictionary(Of String, List(Of DataPoint)), original_height(2) As Integer

    Dim shift As DataView
    Dim shiftData As DataView

    Dim alpha As Integer = 255
    Dim colors As Dictionary(Of String, Integer)
    Dim colors_Ucase As New Dictionary(Of String, Integer)

    Dim bckColorCycleOn As Color
    Dim bckColorCycleOff As Color
    Dim bckColorSetup As Color
    Dim bckColorOthers As Color

    Dim shiftStart As DateTime
    Dim shiftEnd As DateTime

    Dim overrideItens As List(Of OverrideItem)
    Dim labels As List(Of String) = New List(Of String)()
    Dim toolTip As ToolTip = New ToolTip()
    Dim prevPosition As Drawing.Point? = Nothing
    Dim serieFeedrate As Series
    Dim serieRapid As Series
    Dim serieSpindle As Series


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub FormShift_Load(sender As Object, e As EventArgs)

        SetFormConfiguration()

        SetBackColors()

        Dim combovalue = Split(Report_PieChart.CB_Report.Text, " : ")
        machineName = combovalue(0)
        enetMachineName = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.MachineName = machineName).EnetName
        dateTimeline = combovalue(1)

        shiftData = New DataView(Report_PieChart.Timeline)

        shiftStart = shiftData.Item(0).Item("Date_")
        shiftEnd = shiftData.Item(shiftData.Count - 1).Item("Date_")

        Dim sec = shiftStart.Second
        shiftStart = shiftStart.AddSeconds(sec * -1)

        If shiftData.Item(shiftData.Count - 1).Item("Status").contains("PARTNO") Then
            shiftEnd = shiftEnd.AddSeconds(shiftData.Item(shiftData.Count - 2).Item("Cycletime"))
        Else
            shiftEnd = shiftEnd.AddSeconds(shiftData.Item(shiftData.Count - 1).Item("Cycletime"))
        End If

        If shiftEnd < shiftStart Then shiftEnd = shiftEnd.AddDays(1)

        Me.Text = $"SHIFT {shiftno} - {shiftStart.ToString("dd-MMM-yyyy HH:mm")} / {shiftEnd.ToString("dd-MMM-yyyy HH:mm")}"

        LoadPartNumbersChart()

        LoadTimelineChart()

        LoadOverrideChart()

        LoadHistoryChart()

        btnExpande.PerformClick()

    End Sub


    Private Sub SetFormConfiguration()

        Me.Height = 425
        Me.Width = 800
        Me.WindowState = FormWindowState.Normal
        Me.TopLevel = False
        Me.TopMost = False
        Me.Parent = Reporting_application

        Dim pointX = Report_PieChart.Location.X + Report_PieChart.DataGridView1.Location.X
        Dim pointY = Report_PieChart.Location.Y + Report_PieChart.DataGridView1.Location.Y

        Me.Location = New System.Drawing.Point(pointX, pointY)

    End Sub


    Private Sub SetBackColors()

        Try
            'colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)
            colors = CSIFLEXSettings.StatusColors

            For i = 0 To colors.Count - 1
                colors_Ucase.Add(UCase(colors.Keys(i)).ToString().Replace(" ", ""), colors.Values(i))
            Next

            bckColorCycleOn = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE ON"))
            bckColorCycleOn = Color.FromArgb(alpha, bckColorCycleOn.R, bckColorCycleOn.G, bckColorCycleOn.B)

            bckColorCycleOff = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE OFF"))
            bckColorCycleOff = Color.FromArgb(alpha, bckColorCycleOff.R, bckColorCycleOff.G, bckColorCycleOff.B)

            bckColorSetup = System.Drawing.ColorTranslator.FromWin32(colors("SETUP"))
            bckColorSetup = Color.FromArgb(alpha, bckColorSetup.R, bckColorSetup.G, bckColorSetup.B)

        Catch ex As Exception
            bckColorCycleOn = Color.Green
            bckColorCycleOff = Color.Red
            bckColorSetup = Color.Blue
        End Try

        bckColorOthers = Color.Yellow

    End Sub


    Private Sub SetChartTimeline()

        chartTimeline.ChartAreas(0).AxisX.Minimum = shiftStart.ToOADate
        chartTimeline.ChartAreas(0).AxisX.Maximum = shiftEnd.ToOADate

        chartTimeline.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Seconds
        chartTimeline.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm"
        chartTimeline.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
        chartTimeline.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds

        chartTimeline.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
        chartTimeline.ChartAreas(0).CursorX.Interval = 1
        chartTimeline.ChartAreas(0).AxisX.ScaleView.MinSize = 1
        chartTimeline.ChartAreas(0).AxisX.ScaleView.Zoomable = True

        chartTimeline.ChartAreas(0).CursorX.IsUserEnabled = True
        chartTimeline.ChartAreas(0).CursorX.IsUserSelectionEnabled = True
        chartTimeline.ChartAreas(0).AxisX.ScrollBar.Enabled = False

        chartTimeline.ChartAreas(0).AxisX.LabelStyle.Enabled = False

    End Sub


    Private Sub SetChartPartNumbers()

        chartPartNumbers.Series("Cycle").XValueType = ChartValueType.DateTime
        chartPartNumbers.Series("Cycle").SmartLabelStyle.Enabled = True
        chartPartNumbers.Series("Cycle").SmartLabelStyle.AllowOutsidePlotArea = True
        chartPartNumbers.Series("Cycle").SmartLabelStyle.IsMarkerOverlappingAllowed = False
        chartPartNumbers.Series("Cycle").SmartLabelStyle.IsOverlappedHidden = True
        chartPartNumbers.Series("Cycle").SmartLabelStyle.CalloutLineAnchorCapStyle = LineAnchorCapStyle.Round
        chartPartNumbers.Series("Cycle").SmartLabelStyle.CalloutStyle = LabelCalloutStyle.Box
        chartPartNumbers.Series("Cycle").SmartLabelStyle.CalloutBackColor = Color.DarkOrange
        chartPartNumbers.Series("Cycle").LabelBackColor = Color.DarkOrange
        chartPartNumbers.Series("Cycle").LabelBorderColor = Color.Black

        chartPartNumbers.ChartAreas(0).AxisX.Minimum = shiftStart.ToOADate
        chartPartNumbers.ChartAreas(0).AxisX.Maximum = shiftEnd.ToOADate

        chartPartNumbers.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Seconds
        chartPartNumbers.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm"
        chartPartNumbers.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
        chartPartNumbers.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds

        chartPartNumbers.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
        chartPartNumbers.ChartAreas(0).CursorX.Interval = 1
        chartPartNumbers.ChartAreas(0).AxisX.ScaleView.MinSize = 1
        chartPartNumbers.ChartAreas(0).AxisX.ScaleView.Zoomable = True

        chartPartNumbers.ChartAreas(0).CursorX.IsUserEnabled = True
        chartPartNumbers.ChartAreas(0).CursorX.IsUserSelectionEnabled = True
        chartPartNumbers.ChartAreas(0).AxisX.ScrollBar.Enabled = False

    End Sub


    Private Sub LoadPartNumbersChart()

        Dim status As String
        Dim eventTime As DateTime
        Dim cycleTime As Integer
        Dim firstRow As Boolean = True
        Dim tempColor As Color
        Dim previousStatus As String = ""
        Dim previousEventTime As DateTime = Now

        shiftData.RowFilter = $"SHIFT = { shiftno } AND Status LIKE '%PARTN%'"

        For Each line As DataRowView In shiftData

            status = line.Item("Status")
            eventTime = line.Item("Date_")
            cycleTime = line.Item("Cycletime")

            If chartPartNumbers.ChartAreas(0).AxisX.Maximum < eventTime.ToOADate Then
                chartPartNumbers.ChartAreas(0).AxisX.Maximum = eventTime.ToOADate
            End If

            If firstRow Then

                tempColor = rnd_color()
                chartPartNumbers.Series("Cycle").Points.AddXY(eventTime.ToOADate, 70)
                chartPartNumbers.Series("Cycle").Points(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = tempColor
                chartPartNumbers.Series("Cycle").Points(chartPartNumbers.Series("Cycle").Points.Count - 1).LabelBackColor = tempColor
                firstRow = False
            Else

                chartPartNumbers.Series("Cycle").Points.AddXY((previousEventTime.AddSeconds(-DateDiff(DateInterval.Second, eventTime, previousEventTime) / 2).ToOADate), 70)
                chartPartNumbers.Series("Cycle").Points(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = tempColor

                chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).Label = previousStatus
                chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).SetCustomProperty("LabelStyle", "Top")

                chartPartNumbers.Series("Cycle").Points.AddXY(eventTime.ToOADate, 70)
                chartPartNumbers.Series("Cycle").Points(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = tempColor

                tempColor = rnd_color()
            End If

            previousEventTime = eventTime
            previousStatus = status.Remove(0, 8).ToString()

        Next

        If shiftData.Count > 0 Then
            chartPartNumbers.Series("Cycle").Points.AddXY((previousEventTime.AddSeconds(-DateDiff(DateInterval.Second, shiftEnd, previousEventTime) / 2).ToOADate), 70)

            If previousStatus = "" Then previousStatus = "No Part N°"

            chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).Label = previousStatus
            chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).SetCustomProperty("LabelStyle", "Top")
            chartPartNumbers.Series("Cycle").Points.AddXY(shiftEnd.ToOADate, 70)
        End If

        SetChartPartNumbers()

    End Sub


    Private Sub LoadTimelineChart()

        Dim status As String
        Dim eventTime As DateTime
        Dim eventTimePrev As DateTime
        Dim cycleTime As Integer
        Dim firstRow As Boolean = True
        Dim tempColor As Color
        Dim previousStatus As String = ""
        Dim previousEventTime As DateTime = Now

        shiftData.RowFilter = $"SHIFT = { shiftno } AND Status NOT LIKE '%PARTN%'"

        For Each line As DataRowView In shiftData

            status = line.Item("Status")
            eventTime = line.Item("Date_")
            cycleTime = line.Item("Cycletime")
            tempColor = GetBackColor(status)

            If eventTime < eventTimePrev Then eventTime = eventTime.AddDays(1)
            eventTimePrev = eventTime

            If chartTimeline.ChartAreas(0).AxisX.Maximum < eventTime.ToOADate Then
                chartTimeline.ChartAreas(0).AxisX.Maximum = eventTime.ToOADate
            End If

            chartTimeline.Series("Cycle").Points.AddXY(eventTime.ToOADate, 100)
            chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = tempColor

            chartTimeline.Series("Cycle").Points.AddXY(eventTime.AddSeconds(cycleTime).ToOADate, 100)
            chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = tempColor

            AddLabel(status)
        Next

        chartTimeline.Series("Cycle").Points.AddXY(shiftEnd.ToOADate, 100)

        SetChartTimeline()

    End Sub


    Private Sub LoadOverrideChart()

        Dim sqlCmd As StringBuilder = New StringBuilder()

        sqlCmd.Append($"SELECT * FROM                     ")
        sqlCmd.Append($"    csi_database.tbl_machinestate ")
        sqlCmd.Append($"WHERE                             ")
        sqlCmd.Append($"     machineId = {machineId}      ")
        sqlCmd.Append($" AND EventDateTime >= '{shiftStart.MySqlString()}'")
        sqlCmd.Append($" AND EventDateTime <= '{shiftEnd.MySqlString()}'  ")


        Dim dbEvents As DataTable = MySqlAccess.GetDataTable(sqlCmd.ToString(), CSI_Library.CSI_Library.MySqlConnectionString)

        Dim lstEventsFeedrate As List(Of EventDataModel) = New List(Of EventDataModel)()
        Dim lstEventsRapid As List(Of EventDataModel) = New List(Of EventDataModel)()
        Dim lstEventsSpindle As List(Of EventDataModel) = New List(Of EventDataModel)()

        Dim eventFeedrate As EventDataModel = New EventDataModel()
        Dim eventRapid As EventDataModel = New EventDataModel()
        Dim eventSpindle As EventDataModel = New EventDataModel()

        Dim lastFeedrate As Integer = 0
        Dim lastRapid As Integer = 0
        Dim lastSpindle As Integer = 0
        Dim lstDatetime As DateTime

        overrideItens = New List(Of OverrideItem)()

        Dim cycleTime = 0

        Try
            For Each row As DataRow In dbEvents.Rows

                Dim change As OverrideItem = New OverrideItem()

                change.EventDateTime = DateTime.Parse(row("EventDateTime").ToString())
                change.Status = row("SystemStatus")
                change.PartNumber = IIf(Not IsDBNull(row("PartNumber")), row("PartNumber"), "")
                change.Operation = IIf(Not IsDBNull(row("Operation")), row("Operation"), "")
                change.Feedrate = IIf(Not IsDBNull(row("FeedOverride")), row("FeedOverride"), lastFeedrate)
                change.Rapid = IIf(Not IsDBNull(row("RapidOverride")), row("RapidOverride"), lastRapid)
                change.Spindle = IIf(Not IsDBNull(row("SpindleOverride")), row("SpindleOverride"), lastSpindle)

                If overrideItens.Count > 0 Then
                    cycleTime = change.EventDateTime.TimeOfDay.TotalSeconds - lstDatetime.TimeOfDay.TotalSeconds
                    If cycleTime < 0 Then cycleTime += 86400
                    overrideItens(overrideItens.Count - 1).CycleTime = cycleTime
                End If
                lstDatetime = change.EventDateTime

                overrideItens.Add(change)
                lastFeedrate = change.Feedrate
                lastRapid = change.Rapid
                lastSpindle = change.Spindle
            Next

            If overrideItens.Count > 0 Then
                cycleTime = shiftEnd.TimeOfDay.TotalSeconds - lstDatetime.TimeOfDay.TotalSeconds
                If cycleTime < 0 Then cycleTime += 86400

                Dim lastChange As OverrideItem = overrideItens(overrideItens.Count - 1)
                lastChange.CycleTime = cycleTime

                Dim change As OverrideItem = New OverrideItem()
                change.EventDateTime = lastChange.EventDateTime.AddSeconds(cycleTime)
                change.Status = lastChange.Status
                change.PartNumber = lastChange.PartNumber
                change.Operation = lastChange.Operation
                change.Feedrate = lastChange.Feedrate
                change.Rapid = lastChange.Rapid
                change.Spindle = lastChange.Spindle
                change.CycleTime = 0
                overrideItens.Add(change)
            Else
                Dim change As OverrideItem = New OverrideItem()
                change.EventDateTime = shiftStart
                change.Feedrate = 0
                change.Rapid = 0
                change.Spindle = 0
                change.CycleTime = 0
                overrideItens.Add(change)
                change.EventDateTime = shiftEnd
                overrideItens.Add(change)
            End If
        Catch ex As Exception
        End Try

        chartOverride.ChartAreas(0).AxisX.Minimum = shiftStart.ToOADate
        chartOverride.ChartAreas(0).AxisX.Maximum = shiftEnd.ToOADate

        chartOverride.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        chartOverride.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Seconds
        chartOverride.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm"
        chartOverride.ChartAreas(0).AxisX.LabelStyle.Font = New System.Drawing.Font(chartOverride.ChartAreas(0).AxisX.LabelStyle.Font.FontFamily, 9)
        chartOverride.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds

        chartOverride.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds

        chartOverride.ChartAreas(0).AxisY.Enabled = AxisEnabled.False

        chartOverride.ChartAreas(0).AxisY2.MajorGrid.Enabled = True
        chartOverride.ChartAreas(0).AxisY2.MajorGrid.LineColor = Color.SkyBlue

        chartOverride.ChartAreas(0).AxisY2.Title = ""
        chartOverride.ChartAreas(0).AxisY2.IntervalType = DateTimeIntervalType.Number
        chartOverride.ChartAreas(0).AxisY2.LabelStyle.Enabled = True
        chartOverride.ChartAreas(0).AxisY2.LabelStyle.Format = "{0}%"
        chartOverride.ChartAreas(0).AxisY2.Enabled = AxisEnabled.True

        chartOverride.ChartAreas(0).AxisX.ScaleView.Zoomable = True
        chartOverride.ChartAreas(0).CursorX.AutoScroll = True
        chartOverride.ChartAreas(0).CursorX.IsUserSelectionEnabled = True

        chartOverride.Legends.Clear()
        chartOverride.Legends.Add("Legends")
        chartOverride.Legends(0).Docking = Docking.Bottom

        chartOverride.Series.Clear()

        Dim serieStatus = chartOverride.Series.Add("Status")
        serieStatus.ChartType = SeriesChartType.Area
        serieStatus.IsVisibleInLegend = False
        serieStatus.YAxisType = AxisType.Primary

        chkFeedrate.Enabled = True
        chkRapid.Enabled = True
        chkSpindle.Enabled = True

        chkFeedrate.Checked = True
        chkRapid.Checked = True
        chkSpindle.Checked = True

    End Sub


    Private Sub LoadHistoryChart()

        Dim dicHist As Dictionary(Of DateTime, Dictionary(Of String, Decimal)) = New Dictionary(Of DateTime, Dictionary(Of String, Decimal))()

        Dim timeOfDay As DateTime = New DateTime(shiftStart.Year, shiftStart.Month, shiftStart.Day, shiftStart.Hour, 0, 0)
        Do While timeOfDay <= shiftEnd
            dicHist.Add(timeOfDay, New Dictionary(Of String, Decimal)())
            timeOfDay = timeOfDay.AddHours(1)
        Loop

        Dim lstStatus As List(Of String) = New List(Of String)()
        Dim status = ""
        For Each line As DataRowView In shiftData
            status = line.Item("status")
            If Not lstStatus.Contains(status) And Not status.StartsWith("_") Then
                lstStatus.Add(status)
                For Each dicItem As KeyValuePair(Of DateTime, Dictionary(Of String, Decimal)) In dicHist
                    dicItem.Value.Add(status, 0)
                Next
            End If
        Next

        chartHistory.ChartAreas(0).AxisX.IntervalType = ChartValueType.DateTime

        For Each item As String In lstStatus
            chartHistory.Series.Add(item)
            chartHistory.Series(item).ChartType = SeriesChartType.Column
            chartHistory.Series(item).XValueType = ChartValueType.DateTime
            chartHistory.Series(item).ToolTip = item
            chartHistory.Series(item).IsXValueIndexed = True
            chartHistory.Series(item).Color = GetBackColor(item)
        Next


        Dim eventTime As DateTime = New DateTime()
        Dim eventTimePrev As DateTime = New DateTime()
        Dim cycleTime As Integer = 0
        Dim totalTime As Integer = 0
        Dim diffTime As Integer = 0
        Dim cyclePerc As Integer = 0

        For Each line As DataRowView In shiftData

            status = line.Item("status")
            eventTime = line.Item("Date_")
            cycleTime = line.Item("Cycletime")

            If eventTime < eventTimePrev Then eventTime = eventTime.AddDays(1)
            eventTimePrev = eventTime

            eventTime = eventTime.AddMinutes(-eventTime.Minute).AddSeconds(-eventTime.Second)

            Do While (cycleTime + totalTime) > 3600

                diffTime = 3600 - totalTime

                dicHist(eventTime)(status) += ((diffTime / 3600) * 100)

                totalTime = 0
                eventTime = eventTime.AddHours(1)
                cycleTime -= diffTime
            Loop

            dicHist(eventTime)(status) += ((cycleTime / 3600) * 100)
            totalTime += cycleTime
        Next

        Try

            For Each dicItem As KeyValuePair(Of DateTime, Dictionary(Of String, Decimal)) In dicHist

                For Each statusItem As KeyValuePair(Of String, Decimal) In dicItem.Value

                    chartHistory.Series(statusItem.Key).Points.AddXY(dicItem.Key, statusItem.Value)

                Next

            Next

        Catch ex As Exception
            Dim msg = ex
        End Try

        chartHistory.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Hours
        chartHistory.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm"
        chartHistory.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Hours
        chartHistory.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Hours

    End Sub


    Private Function GetBackColor(status As String) As Color

        Dim tempColor As Color

        Select Case status
            Case "CYCLE ON"
                tempColor = bckColorCycleOn
            Case "CYCLE OFF"
                tempColor = bckColorCycleOff
            Case "SETUP"
                tempColor = bckColorSetup
            Case Else
                Try
                    tempColor = ColorTranslator.FromWin32(colors_Ucase(UCase(status.Replace(" ", ""))))
                Catch ex As Exception
                    tempColor = bckColorOthers
                End Try
        End Select

        Return tempColor

    End Function


    Private Sub AddLabel(status As String)

        If labels.Contains(status) Then Return

        labels.Add(status)

        Dim statusColor As Color = GetBackColor(status)
        Dim newLabel As Label = New Label()
        newLabel.Name = status
        newLabel.Text = status
        newLabel.BackColor = statusColor
        newLabel.Visible = True
        newLabel.AutoSize = True
        newLabel.Padding = New Padding(3)

        Dim contrastcolor As Color
        Dim rdif As Integer, gdif As Integer, bdif As Integer

        If (statusColor.R > 100 And statusColor.R < 150) Then rdif = 150 Else rdif = 255
        If (statusColor.G > 100 And statusColor.G < 150) Then gdif = 150 Else gdif = 255
        If (statusColor.B > 100 And statusColor.B < 150) Then bdif = 150 Else bdif = 255

        contrastcolor = Color.FromArgb(rdif - statusColor.R, gdif - statusColor.G, bdif - statusColor.B)
        newLabel.ForeColor = contrastcolor

        FlowLayoutPanel1.Controls.Add(newLabel)
        FlowLayoutPanel1.Refresh()

    End Sub


    Private Sub ChartsZoomChange(sender As Object, e As MouseEventArgs) Handles chartPartNumbers.MouseUp, chartTimeline.MouseUp, chartOverride.MouseUp


        If sender.Name = "chartPartNumbers" Then
            If e.Button = MouseButtons.Right Then
                chartPartNumbers.ChartAreas(0).AxisX.ScaleView.ZoomReset()
            End If
            chartTimeline.ChartAreas(0).AxisX.ScaleView = chartPartNumbers.ChartAreas(0).AxisX.ScaleView
            chartOverride.ChartAreas(0).AxisX.ScaleView = chartPartNumbers.ChartAreas(0).AxisX.ScaleView
        ElseIf sender.Name = "chartTimeline" Then
            If e.Button = MouseButtons.Right Then
                chartTimeline.ChartAreas(0).AxisX.ScaleView.ZoomReset()
            End If
            chartPartNumbers.ChartAreas(0).AxisX.ScaleView = chartTimeline.ChartAreas(0).AxisX.ScaleView
            chartOverride.ChartAreas(0).AxisX.ScaleView = chartTimeline.ChartAreas(0).AxisX.ScaleView
        ElseIf sender.Name = "chartOverride" Then
            If e.Button = MouseButtons.Right Then
                chartOverride.ChartAreas(0).AxisX.ScaleView.ZoomReset()
            End If
            chartPartNumbers.ChartAreas(0).AxisX.ScaleView = chartOverride.ChartAreas(0).AxisX.ScaleView
            chartTimeline.ChartAreas(0).AxisX.ScaleView = chartOverride.ChartAreas(0).AxisX.ScaleView
        End If

        chartPartNumbers.Refresh()
        chartTimeline.Refresh()
        chartOverride.Refresh()

    End Sub


    Private Sub chartOverride_MouseMove(sender As Object, e As MouseEventArgs) Handles chartOverride.MouseMove

        Dim pos As Drawing.Point = e.Location

        If prevPosition IsNot Nothing Then
            If prevPosition.HasValue And pos = prevPosition.Value Then Return
        End If

        toolTip.RemoveAll()

        prevPosition = pos
        Dim results = chartOverride.HitTest(pos.X, pos.Y, False, ChartElementType.DataPoint)

        For Each result In results

            If result.ChartElementType = ChartElementType.DataPoint Then

                Dim prop As DataPoint = result.Object
                If prop IsNot Nothing Then

                    Dim pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue)
                    Dim pointYPixel = result.ChartArea.AxisY2.ValueToPixelPosition(prop.YValues(0))

                    If (Math.Abs(pos.X - pointXPixel) < 3 And Math.Abs(pos.Y - pointYPixel) < 3) Then

                        Try
                            Dim ovrDateTime = DateTime.FromOADate(prop.XValue)
                            Dim ovrItem As OverrideItem = overrideItens.First(Function(i) i.EventDateTime = ovrDateTime)
                            Dim legend As StringBuilder = New StringBuilder()
                            legend.Append($"Status: {ovrItem.Status}{vbNewLine}")
                            legend.Append($"Time: {ovrItem.EventDateTime.ToString("HH:mm:ss")}{vbNewLine}")
                            legend.Append($"Feedrate: {ovrItem.Feedrate}%{vbNewLine}")
                            legend.Append($"Rapid: {ovrItem.Rapid}%{vbNewLine}")
                            legend.Append($"Spindle: {ovrItem.Spindle}%")

                            toolTip.Show(legend.ToString(), chartOverride, pos.X + 10, pos.Y - 15)

                        Catch ex As Exception

                        End Try
                        'Else
                        '    ToolTip1.RemoveAll()
                    End If

                End If

            End If

        Next

    End Sub


    Private Sub chkFeedrate_CheckedChanged(sender As Object, e As EventArgs) Handles chkFeedrate.CheckedChanged

        If chkFeedrate.Checked And chartOverride.Series.IndexOf("Feedrate") = -1 Then
            serieFeedrate = chartOverride.Series.Add("Feedrate")
            serieFeedrate.ChartType = SeriesChartType.StepLine
            serieFeedrate.IsVisibleInLegend = True
            serieFeedrate.BorderWidth = 2
            serieFeedrate.Color = Color.Blue
            serieFeedrate.YAxisType = AxisType.Secondary
            serieFeedrate.LegendText = "Feedrate"
            serieFeedrate.MarkerStyle = MarkerStyle.Circle
            serieFeedrate.MarkerSize = 5
            chartOverride.Series("Feedrate").Points.DataBind(overrideItens, "EventDatetime", "Feedrate", Nothing)
        ElseIf Not chkFeedrate.Checked And chartOverride.Series.IndexOf("Feedrate") > -1 Then
            Dim serie = chartOverride.Series("Feedrate")
            chartOverride.Series.Remove(serie)
        End If
    End Sub


    Private Sub chkRapid_CheckedChanged(sender As Object, e As EventArgs) Handles chkRapid.CheckedChanged

        If chkRapid.Checked And chartOverride.Series.IndexOf("Rapid") = -1 Then
            serieRapid = chartOverride.Series.Add("Rapid")
            serieRapid.ChartType = SeriesChartType.StepLine
            serieRapid.IsVisibleInLegend = True
            serieRapid.BorderWidth = 2
            serieRapid.Color = Color.Red
            serieRapid.YAxisType = AxisType.Secondary
            serieRapid.LegendText = "Rapid"
            serieRapid.MarkerStyle = MarkerStyle.Circle
            serieRapid.MarkerSize = 5
            chartOverride.Series("Rapid").Points.DataBind(overrideItens, "EventDatetime", "Rapid", Nothing)
        ElseIf Not chkRapid.Checked And chartOverride.Series.IndexOf("Rapid") > -1 Then
            Dim serie = chartOverride.Series("Rapid")
            chartOverride.Series.Remove(serie)
        End If
    End Sub


    Private Sub chkSpindle_CheckedChanged(sender As Object, e As EventArgs) Handles chkSpindle.CheckedChanged

        If chkSpindle.Checked And chartOverride.Series.IndexOf("Spindle") = -1 Then
            serieSpindle = chartOverride.Series.Add("Spindle")
            serieSpindle.ChartType = SeriesChartType.StepLine
            serieSpindle.IsVisibleInLegend = True
            serieSpindle.BorderWidth = 2
            serieSpindle.Color = Color.Green
            serieSpindle.YAxisType = AxisType.Secondary
            serieSpindle.LegendText = "Spindle"
            serieSpindle.MarkerStyle = MarkerStyle.Circle
            serieSpindle.MarkerSize = 5
            chartOverride.Series("Spindle").Points.DataBind(overrideItens, "EventDatetime", "Spindle", Nothing)
        ElseIf Not chkSpindle.Checked And chartOverride.Series.IndexOf("Spindle") > -1 Then
            Dim serie = chartOverride.Series("Spindle")
            chartOverride.Series.Remove(serie)
        End If
    End Sub




    Private Sub Form_Shift_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        FormShift_Load(sender, e)
        Return

        Me.Height = 375 'Report_PieChart.DataGridView1.Height + 30
        Me.Width = 800 'Report_PieChart.DataGridView1.Width + Report_PieChart.ListBox1.Width + 30
        Me.WindowState = FormWindowState.Normal
        Me.TopLevel = False
        Me.TopMost = False
        Me.Parent = Reporting_application

        Dim list_of_status As New List(Of String), cycle_ As Integer = 0
        Dim last_loop As Boolean = False

        Dim img As Image = My.Resources.d
        img.RotateFlip(RotateFlipType.Rotate180FlipNone)
        Button1.Image = img

        Dim colors_dict As New Dictionary(Of String, String) 'status/R G B
        Dim colors_dict_rand As New Dictionary(Of String, String) 'status/R G B , if generated 
        ' Specifying IntervalType
        chartHistory.ChartAreas(0).AxisX.IntervalType = ChartValueType.DateTime


        Dim start_shift As DateTime, end_shift As DateTime
        Dim time_ As DateTime

        shift = New DataView(Report_PieChart.Timeline)
        Me.Location = New System.Drawing.Point(Report_PieChart.Location.X + Report_PieChart.DataGridView1.Location.X, Report_PieChart.Location.Y + Report_PieChart.DataGridView1.Location.Y)


        Dim previous_AM_PM As String = ""
        For Each row As DataRowView In shift
            time_ = row.Item("Date_")
            If previous_AM_PM = "PM" And time_.ToString("tt") = "AM" Then
                time_ = time_.AddDays(1)
                row.Item("Date_") = time_.ToString()
            Else
                previous_AM_PM = time_.ToString("tt")
            End If
        Next

        Dim status As String = "", Hourly_stat(1) As periode, item_ As Integer = 0, PreviousCycle As Integer, overshoot As Integer = 0, partno As String = ""

        Hourly_stat(1) = New periode

        Dim total As Integer = 0

        Dim alpha As Integer = 255

        Dim colors As Dictionary(Of String, Integer)
        Dim colors_Ucase As New Dictionary(Of String, Integer)
        Dim backcolors As Color
        Dim backcolors2 As Color
        Dim backcolors3 As Color
        Dim backcolors4 As Color = Color.Yellow

        Dim csilib As New CSI_Library.CSI_Library(False)

        csilib.connectionString = CSIFLEXSettings.Instance.ConnectionString

        Try

            'colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)
            colors = CSIFLEXSettings.StatusColors

            For i = 0 To colors.Count - 1
                colors_Ucase.Add(UCase(colors.Keys(i)).ToString().Replace(" ", ""), colors.Values(i))
            Next

            backcolors = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE ON"))
            backcolors = Color.FromArgb(alpha, backcolors.R, backcolors.G, backcolors.B)

            backcolors2 = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE OFF"))
            backcolors2 = Color.FromArgb(alpha, backcolors2.R, backcolors2.G, backcolors2.B)

            backcolors3 = System.Drawing.ColorTranslator.FromWin32(colors("SETUP"))
            backcolors3 = Color.FromArgb(alpha, backcolors3.R, backcolors3.G, backcolors3.B)

        Catch ex As Exception
            backcolors = Color.Green
            backcolors2 = Color.Red
            backcolors3 = Color.Blue

        End Try


        Try
            ' The list of the available status in this hour, and the start and end time.******************************************
            shift.RowFilter = "shift=" & shiftno
            For Each row As DataRowView In shift
                status = row.Item("status")

                ' CSIFLEX needs the total number of status in this shift:
                If Not list_of_status.Contains(status) And (status <> "_SH_START" And status <> "_SH_END" And Not status.Contains("_PARTN")) Then list_of_status.Add(status)

            Next
            Dim test As Dictionary(Of String, String) = Reporting_application.ShiftSetup

            Dim combovalue As String() = Split(Report_PieChart.CB_Report.Text, " : ")


            ' start and end shift time          
            start_shift = shift.Item(0).Item("Date_")
            end_shift = shift.Item(shift.Count - 1).Item("Date_")

            If shift.Item(shift.Count - 1).Item("Status").contains("PARTNO") Then
                end_shift = end_shift.AddSeconds(shift.Item(shift.Count - 2).Item("Cycletime"))
            Else
                end_shift = end_shift.AddSeconds(shift.Item(shift.Count - 1).Item("Cycletime"))
            End If

            '*********************************************************************************************************************

            Dim hour As Date = start_shift.AddMinutes(-start_shift.Minute).AddSeconds(-start_shift.Second)
            'hour = start_shift.AddSeconds(-start_shift.Second)

            Dim PreviousCycleTime As Integer = 0
            Dim PreviousCycleName As String = "", PreviousCycleHour As Date

            last_loop = False

            ' Get stat every hour:
            Dim endhour As Integer = end_shift.TimeOfDay.Hours
            Dim starthour As Integer = start_shift.TimeOfDay.Hours
            If (hour.TimeOfDay.Hours > end_shift.TimeOfDay.Hours) Then endhour = endhour + 24

            While (starthour < endhour) And (last_loop = False)
                item_ += 1
                total = 3600

                'New hour in the hourly timeline
                ReDim Preserve Hourly_stat(item_)
                Hourly_stat(item_ - 1).shift = New Dictionary(Of String, Double)
                Hourly_stat(item_ - 1).date_ = hour.Hour

                'filter the shift for just this hour
                shift.RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat,
                     "Date_ >= #{0}#", hour) & " and " & String.Format(CultureInfo.InvariantCulture.DateTimeFormat,
                     "Date_ <= #{0}#", hour.AddHours(1)) & " and Shift=" & shiftno

                'residus of the privious hour :
                If overshoot <> 0 Then
                    'if a cycle started at H-1
                    If overshoot > 3660 Then
                        Hourly_stat(item_ - 1).shift.Add(PreviousCycleName, 3600)
                        overshoot = overshoot - 3600
                    Else
                        Hourly_stat(item_ - 1).shift.Add(PreviousCycleName, overshoot)
                        overshoot = 0
                    End If
                End If

                'Cycle times for this hour
                For Each row In shift
                    status = row.Item("status")

                    If Not (status.Contains("_PARTN") Or status.Contains("_SH_START") Or status.Contains("_SH_END")) Then
                        'Add new stattus if not present
                        If Not Hourly_stat(item_ - 1).shift.ContainsKey(status) Then
                            Hourly_stat(item_ - 1).shift.Add(status, row.Item("cycletime"))
                        Else
                            ' Else add the cycle time 
                            Hourly_stat(item_ - 1).shift.Item(status) = Hourly_stat(item_ - 1).shift.Item(status) + row.Item("cycletime")
                        End If
                    End If

                    PreviousCycleHour = row.Item("Date_")
                    PreviousCycleName = row.Item("status")
                    PreviousCycle = row.Item("CycleTime")
                Next

                ' - Last status overshoot :


                If PreviousCycleName <> "" And Not (status.Contains("_PARTN") Or status.Contains("_SH_START") Or status.Contains("_SH_END")) Then
                    If PreviousCycle > DateDiff(DateInterval.Second, PreviousCycleHour, hour.AddHours(1)) And overshoot = 0 Then
                        overshoot = PreviousCycle - DateDiff(DateInterval.Second, PreviousCycleHour, hour.AddHours(1))
                        Hourly_stat(item_ - 1).shift.Item(PreviousCycleName) = Hourly_stat(item_ - 1).shift.Item(PreviousCycleName) - PreviousCycle + DateDiff(DateInterval.Second, PreviousCycleHour, hour.AddHours(1))
                        ' MessageBox.Show(DateDiff(DateInterval.Second, PreviousCycleHour, hour.AddHours(1)))
                    End If
                End If

                Hourly_stat(item_ - 1).shift.Add("total", total)
                hour = hour.AddHours(1)
                starthour = starthour + 1
                If ((DateDiff(DateInterval.Minute, hour, end_shift)) < 59) And ((DateDiff(DateInterval.Minute, hour, end_shift)) > 0) Then last_loop = True

            End While
            last_loop = False

            '*********************************************************************************************************************



            ReDim Preserve Hourly_stat(UBound(Hourly_stat) - 1)

            Dim HexCode As String, red As Integer, green As Integer, blue As Integer
            Dim dic As Dictionary(Of String, String) = New Dictionary(Of String, String)

            For Each rows As KeyValuePair(Of String, String) In Report_PieChart.colors
                dic.Add(rows.Key.ToString(), rows.Value)

            Next



            'Add series to chart2 for the 'OTHER' status
            For Each item In list_of_status
                If True Then '
                    chartHistory.Series.Add(item)
                    chartHistory.Series(item).ChartType = SeriesChartType.Column
                    chartHistory.Series(item).XValueType = ChartValueType.DateTime
                    chartHistory.Series(item).ToolTip = item
                    chartHistory.Series(item).IsXValueIndexed = True

                    Dim temp As String = item
                    If (item = "_CON") Then
                        temp = "CYCLE ON"
                    ElseIf (item = "_COFF") Then
                        temp = "CYCLE OFF"
                    ElseIf (item = "_SETUP") Then
                        temp = "SETUP"
                    ElseIf (item = "_OTHER") Then
                        temp = "OTHER"
                    Else

                    End If

                    If dic.ContainsKey(temp) Then 'Report_PieChart.colors.ContainsKey(item) Then '(UCase(item.Replace(" ", ""))) Then
                        Dim color As Color
                        color = System.Drawing.ColorTranslator.FromWin32(dic(temp.Replace(" ", "")))
                        color = Color.FromArgb(alpha, color.R, color.G, color.B)
                        chartHistory.Series(item).Color = color
                        If Not colors_dict.ContainsKey(item.Replace(" ", "")) Then colors_dict.Add(item.Replace(" ", ""), HexCode)

                    Else

                        '  Dim someColor As Color = Color.FromArgb(rand1(0, 256), Random.Next(0, 256), Random.Next(0, 256))
                        Dim alreadyChoosenColors As List(Of Color) = New List(Of Color)
                        Dim rand As New Random()
                        Dim redColor As Integer = rand.Next(0, 255)
                        Dim greenColor As Integer = rand.Next(0, 255)
                        Dim blueColor As Integer = rand.Next(0, 255)

                        Dim someColor As Color

                        If colors_Ucase.ContainsKey(temp.Replace(" ", "")) Then
                            someColor = System.Drawing.ColorTranslator.FromWin32(colors_Ucase(temp.Replace(" ", "")))
                        Else
                            someColor = rnd_color()
                            colors_Ucase.Add(temp.Replace(" ", ""), System.Drawing.ColorTranslator.ToWin32(someColor))
                            colors.Add(temp, System.Drawing.ColorTranslator.ToWin32(someColor))
                        End If

                        someColor = System.Drawing.ColorTranslator.FromWin32(colors_Ucase(temp.Replace(" ", "")))
                        someColor = Color.FromArgb(alpha, someColor.R, someColor.G, someColor.B)
                        red = someColor.R
                        green = someColor.G
                        blue = someColor.B
                        chartHistory.Series(item).Color = someColor
                        HexCode = "ff" & System.Drawing.ColorTranslator.ToHtml(someColor).Remove(0, 1)

                        If Not colors_dict.ContainsKey(item.Replace(" ", "")) Then colors_dict.Add(item.Replace(" ", ""), HexCode)
                    End If

                End If
            Next


            'Convert the cycle times to percent and Fill the chart 2 : Hourly TimeLine
            For Each item In Hourly_stat 'For each hour
                If item.shift Is Nothing Then
                Else

                    For Each status In list_of_status

                        If item.shift.Keys.Contains(status) Then
                            If total = 0 Then
                                chartHistory.Series(status).Points.AddXY(Convert.ToDateTime(item.date_ & ":00"), 0)
                            Else
                                chartHistory.Series(status).Points.AddXY(Convert.ToDateTime(item.date_ & ":00"), item.shift.Item(status) * 100 / item.shift.Item("total"))
                            End If
                        Else
                            chartHistory.Series(status).Points.AddXY(Convert.ToDateTime(item.date_ & ":00"), 0)
                        End If

                    Next
                End If
            Next


            chartHistory.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Hours
            chartHistory.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm:ss"
            chartHistory.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Hours
            chartHistory.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Hours


            '*********************************************************************************************************************
            '*********************************************************************************************************************
            ' TimeLine:
            '*********************************************************************************************************************
            '*********************************************************************************************************************



            Dim Cycle_time As Integer = 0
            shift.RowFilter = "SHIFT =" & shiftno
            time_ = shift.Item(0).Item("Date_")

            Dim t0 As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)
            chartTimeline.Series("Cycle").Points.AddXY(t0.ToOADate, 100)
            If Not shift.Item(0).Item("status").ToString().Contains("PARTNO") Then
                Cycle_time = shift.Item(0).Item("cycletime")
                chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Label = New TimeSpan(0, 0, Cycle_time).ToString()
            End If


            chartTimeline.ChartAreas(0).AxisX.Minimum = t0.ToOADate
            chartTimeline.ChartAreas(0).AxisX.Maximum = end_shift.ToOADate


            chartPartNumbers.ChartAreas(0).AxisX.Minimum = t0.ToOADate
            chartPartNumbers.ChartAreas(0).AxisX.Maximum = end_shift.ToOADate

            Dim stat As String = shift.Item(0).Item("status")

            If stat <> "" And Not stat.Contains("_PARTN") Then

                If (loadingasCON And stat = "LOADING") Then
                    stat = "CYCLE ON"
                End If

                If (stat = "_CON" Or stat = "CYCLE ON") Then
                    chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = bckColorCycleOn
                    chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).LabelBackColor = bckColorCycleOn
                ElseIf (stat = "_COFF" Or stat = "CYCLE OFF") Then
                    chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = bckColorCycleOff
                    chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).LabelBackColor = bckColorCycleOff
                ElseIf (stat = "_SETUP" Or stat = "SETUP") Then
                    chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = bckColorSetup
                    chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).LabelBackColor = bckColorSetup
                Else
                    Dim color As Color
                    color = System.Drawing.ColorTranslator.FromWin32(colors(stat))
                    color = Color.FromArgb(alpha, color.R, color.G, color.B)
                    chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = color
                    chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).LabelBackColor = color
                End If
            Else

                chartPartNumbers.Series("Cycle").Points.AddXY(t0.ToOADate, 70)

            End If


            Dim Previous_status As String = "" ' for the labels
            Dim i_ As Integer = 0
            Dim tmp_color As System.Drawing.Color

            Dim first_row As Boolean = True
            Dim PreviousTime As DateTime
            Dim LastPreviousTime As DateTime

            Cycle_time = 0

            For Each row In shift

                '   Call annotation(row)
                time_ = row.Item("Date_")

                '  status = row.item("status")
                Dim t As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)


                If (Not status.Contains("PARTN")) And status <> "" Then

                    '////////////////////////////////////////////////////////////////////////
                    'change the minimum or maximum of the Axis in case the DB is not in order
                    If chartTimeline.ChartAreas(0).AxisX.Minimum > t.ToOADate Then
                        chartTimeline.ChartAreas(0).AxisX.Minimum = t.ToOADate
                    End If

                    If chartTimeline.ChartAreas(0).AxisX.Maximum < t.ToOADate Then
                        chartTimeline.ChartAreas(0).AxisX.Maximum = t.ToOADate
                    End If

                    '////////////////////////////////////////////////////////////////////////

                    chartTimeline.Series("Cycle").Points.AddXY(t.ToOADate, 100)
                    If Cycle_time <> 0 Then chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).Label = New TimeSpan(0, 0, Cycle_time).ToString()

                    If (loadingasCON And status = "LOADING") Then
                        status = "CYCLE ON"
                    End If

                    If (status = "_CON" Or status = "CYCLE ON") Then
                        chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = bckColorCycleOn
                        chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).LabelBackColor = bckColorCycleOn
                    ElseIf (status = "_COFF" Or status = "CYCLE OFF") Then
                        chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = bckColorCycleOff
                        chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).LabelBackColor = bckColorCycleOff
                    ElseIf (status = "_SETUP" Or status = "SETUP") Then
                        chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = bckColorSetup
                        chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).LabelBackColor = bckColorSetup
                    Else
                        Dim color As Color
                        Try
                            color = System.Drawing.ColorTranslator.FromWin32(colors_Ucase(UCase(status.Replace(" ", ""))))
                            color = Color.FromArgb(alpha, color.R, color.G, color.B)
                            chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = color
                            chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).LabelBackColor = color
                        Catch ex As Exception
                            MessageBox.Show("CSIFLEX can not display correctly the Time line : the color for '" & status.ToString() & "' was absent")
                            color = Drawing.Color.White 'backcolors4
                            color = Color.FromArgb(alpha, color.R, color.G, color.B)
                            chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = color
                            chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).LabelBackColor = color
                        End Try
                    End If

                Else
                    ' IF PART :::
                    If status <> "" Then
                        If chartPartNumbers.ChartAreas(0).AxisX.Maximum < t.ToOADate Then
                            chartPartNumbers.ChartAreas(0).AxisX.Maximum = t.ToOADate
                        End If
                        If first_row = False Then

                            chartPartNumbers.Series("Cycle").Points.AddXY((PreviousTime.AddSeconds(-DateDiff(DateInterval.Second, t, PreviousTime) / 2).ToOADate), 70)
                            chartPartNumbers.Series("Cycle").Points(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = tmp_color

                            chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).Label = Previous_status
                            chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).SetCustomProperty("LabelStyle", "Top")

                            chartPartNumbers.Series("Cycle").Points.AddXY(t.ToOADate, 70)
                            chartPartNumbers.Series("Cycle").Points(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = tmp_color

                            tmp_color = rnd_color()
                        Else
                            tmp_color = rnd_color()
                            chartPartNumbers.Series("Cycle").Points.AddXY(t.ToOADate, 70)
                            chartPartNumbers.Series("Cycle").Points(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = tmp_color
                            chartPartNumbers.Series("Cycle").Points(chartPartNumbers.Series("Cycle").Points.Count - 1).LabelBackColor = tmp_color
                            first_row = False

                        End If

                        PreviousTime = t
                        Previous_status = status.Remove(0, 8).ToString()

                    End If
                End If

                status = row.item("status")
                Cycle_time = row.Item("cycletime")
                LastPreviousTime = row.Item("Date_")

            Next


            chartTimeline.Series("Cycle").Points.AddXY(end_shift.ToOADate, 100)
            chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).Color
            chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).Label = New TimeSpan(0, 0, Cycle_time).ToString()

            If (loadingasCON And status = "LOADING") Then
                status = "CYCLE ON"
            End If

            If (status = "_CON" Or status = "CYCLE ON") Then
                chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = bckColorCycleOn
                chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).LabelBackColor = bckColorCycleOn
            ElseIf (status = "_COFF" Or status = "CYCLE OFF") Then
                chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = bckColorCycleOff
                chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).LabelBackColor = bckColorCycleOff
            ElseIf (status = "_SETUP" Or status = "SETUP") Then
                chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = bckColorSetup
                chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).LabelBackColor = bckColorSetup
            ElseIf (status.Contains("PARTN")) Then

                If chartPartNumbers.ChartAreas(0).AxisX.Maximum < end_shift.ToOADate Then
                    chartPartNumbers.ChartAreas(0).AxisX.Maximum = end_shift.ToOADate
                End If

                chartPartNumbers.Series("Cycle").Points.AddXY(LastPreviousTime, 70)
                chartPartNumbers.Series("Cycle").Points(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = tmp_color

                chartPartNumbers.Series("Cycle").Points.AddXY((end_shift.AddSeconds(-DateDiff(DateInterval.Second, end_shift, LastPreviousTime) / 2).ToOADate), 70)
                chartPartNumbers.Series("Cycle").Points(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = tmp_color

                chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).Label = status.Remove(0, 8).ToString()
                chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).SetCustomProperty("LabelStyle", "Top")

                chartPartNumbers.Series("Cycle").Points.AddXY(end_shift.ToOADate, 70)
                chartPartNumbers.Series("Cycle").Points(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = tmp_color

            Else

                Dim color As Color
                Try
                    color = System.Drawing.ColorTranslator.FromWin32(colors_Ucase(UCase(status.Replace(" ", ""))))
                    color = Color.FromArgb(alpha, color.R, color.G, color.B)
                    chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Color = color
                    chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).LabelBackColor = color
                Catch ex As Exception
                    MessageBox.Show("CSIFLEX cannot display correctly the Time line : the key '" & status.ToString() & "' was absent")
                    color = bckColorOthers
                    color = Color.FromArgb(alpha, color.R, color.G, color.B)
                    chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 2).Color = color
                End Try
            End If


            If Not (status.Contains("PARTN")) Then
                If chartPartNumbers.Series("Cycle").Points.Count > 0 Then
                    chartPartNumbers.Series("Cycle").Points.AddXY((PreviousTime.AddSeconds(-DateDiff(DateInterval.Second, end_shift, PreviousTime) / 2).ToOADate), 70)
                    If Previous_status = "" Then Previous_status = "No Part N°"
                    chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).Label = Previous_status
                    chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).SetCustomProperty("LabelStyle", "Top")

                    chartPartNumbers.Series("Cycle").Points.AddXY(end_shift.ToOADate, 70)
                Else
                    chartPartNumbers.Series("Cycle").Points.AddXY(t0.ToOADate, 70)
                    chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = Color.LightGray
                    chartPartNumbers.Series("Cycle").Points.AddXY(end_shift.ToOADate, 70)
                    chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = Color.LightGray
                    chartPartNumbers.Series("Cycle").Points.AddXY((start_shift.AddSeconds(-DateDiff(DateInterval.Second, end_shift, start_shift) / 2).ToOADate), 70)
                    chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).Label = "No data for the parts"
                    chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).SetCustomProperty("LabelStyle", "Top")
                    chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).LabelBackColor = Color.LightGray
                    chartPartNumbers.Series("Cycle").Points.Item(chartPartNumbers.Series("Cycle").Points.Count - 1).Color = Color.LightGray
                End If
            End If

            chartTimeline.Series("Cycle").XValueType = ChartValueType.DateTime
            chartTimeline.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Seconds
            chartTimeline.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm:ss"
            chartTimeline.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
            chartTimeline.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds

            chartTimeline.Series("Cycle").SmartLabelStyle.AllowOutsidePlotArea = True
            chartTimeline.Series("Cycle").SmartLabelStyle.IsMarkerOverlappingAllowed = False
            chartTimeline.Series("Cycle").SmartLabelStyle.IsOverlappedHidden = True
            chartTimeline.Series("Cycle").SmartLabelStyle.CalloutLineAnchorCapStyle = LineAnchorCapStyle.None
            chartTimeline.Series("Cycle").SmartLabelStyle.MaxMovingDistance = 1
            chartTimeline.Series("Cycle").SmartLabelStyle.CalloutStyle = LabelCalloutStyle.Box
            chartTimeline.Series("Cycle").LabelBorderColor = Color.Black
            chartTimeline.Series("Cycle").SmartLabelStyle.Enabled = False


            combovalue = Split(Report_PieChart.CB_Report.Text, " : ")
            machineName = combovalue(0)
            enetMachineName = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.MachineName = machineName).EnetName
            dateTimeline = combovalue(1)

            Me.Text = $"Machine : { machineName }   -   Shift { shiftno }, Date : { combovalue(1) }"


            addbox(colors, colors_Ucase)
            h0 = SplitContainer1.Panel1.Height
            h = SplitContainer1.Panel2.Height

            btnExpande.PerformClick()


            '*********************************************************************************************************************
            '*********************************************************************************************************************
            ' PARTS:
            '*********************************************************************************************************************
            '*********************************************************************************************************************

            chartPartNumbers.Series("Cycle").XValueType = ChartValueType.DateTime
            chartPartNumbers.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Seconds
            chartPartNumbers.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm:ss"
            chartPartNumbers.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
            chartPartNumbers.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds

            chartPartNumbers.Series("Cycle").SmartLabelStyle.Enabled = True
            chartPartNumbers.Series("Cycle").SmartLabelStyle.AllowOutsidePlotArea = True
            chartPartNumbers.Series("Cycle").SmartLabelStyle.IsMarkerOverlappingAllowed = False
            chartPartNumbers.Series("Cycle").SmartLabelStyle.IsOverlappedHidden = True
            chartPartNumbers.Series("Cycle").SmartLabelStyle.CalloutLineAnchorCapStyle = LineAnchorCapStyle.Round
            chartPartNumbers.Series("Cycle").SmartLabelStyle.CalloutStyle = LabelCalloutStyle.Box
            chartPartNumbers.Series("Cycle").SmartLabelStyle.CalloutBackColor = Color.DarkOrange
            chartPartNumbers.Series("Cycle").LabelBackColor = Color.DarkOrange
            chartPartNumbers.Series("Cycle").LabelBorderColor = Color.Black

            chartTimeline.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
            chartTimeline.ChartAreas(0).CursorX.Interval = 1
            chartPartNumbers.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
            chartPartNumbers.ChartAreas(0).CursorX.Interval = 1

            chartPartNumbers.ChartAreas(0).AxisX.ScaleView.MinSize = 1
            chartTimeline.ChartAreas(0).AxisX.ScaleView.MinSize = 1

            chartPartNumbers.ChartAreas(0).AxisX.ScaleView.Zoomable = True
            chartTimeline.ChartAreas(0).AxisX.ScaleView.Zoomable = True

            chartPartNumbers.ChartAreas(0).CursorX.IsUserEnabled = True
            chartPartNumbers.ChartAreas(0).CursorX.IsUserSelectionEnabled = True
            chartTimeline.ChartAreas(0).CursorX.IsUserEnabled = True
            chartTimeline.ChartAreas(0).CursorX.IsUserSelectionEnabled = True


            chartTimeline.ChartAreas(0).AxisX.ScrollBar.Enabled = False
            chartPartNumbers.ChartAreas(0).AxisX.ScrollBar.Enabled = False

        Catch ex As Exception
            MessageBox.Show("CSIFLEX can not display the Time line") ' & ex.Message)
            csilib.LogClientError("CSIFLEX can not display the Time line" & ex.Message)
        End Try

    End Sub


    Private Function rnd_color() As System.Drawing.Color

        Dim MyAlpha As Integer
        Dim MyRed As Integer
        Dim MyGreen As Integer
        Dim MyBlue As Integer
        ' Initialize the random-number generator.
        Randomize()
        ' Generate random value between 1 and 6.
        MyAlpha = CInt(Int((254 * Rnd()) + 0))
        ' Initialize the random-number generator.
        Randomize()
        ' Generate random value between 1 and 6.
        MyRed = CInt(Int((254 * Rnd()) + 0))
        ' Initialize the random-number generator.
        Randomize()
        ' Generate random value between 1 and 6.
        MyGreen = CInt(Int((254 * Rnd()) + 0))
        ' Initialize the random-number generator.
        Randomize()
        ' Generate random value between 1 and 6.
        MyBlue = CInt(Int((254 * Rnd()) + 0))

        Return Color.FromArgb(MyAlpha, MyRed, MyGreen, MyBlue)
    End Function

    'ANNOTATION'''''''''''''''''''''''''''''''''''''''''
    Private Sub annotation(ByRef row As DataRowView)
        Dim time_ As New DateTime, cycle_ As New Integer

        time_ = row.Item("Date_")
        cycle_ = row.Item("cycletime")
        Dim t As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)

        Dim annot As RectangleAnnotation = New RectangleAnnotation
        annot.Name = t.Second & ":" & t.Minute & ":" & t.Hour
        annot.Text = ToDHMS(cycle_) ' & vbCrLf & "Start : " & t.Hour.ToString() & "h" & t.Minute.ToString() & "m" & t.Second.ToString() & vbCrLf & "End :" & vbCrLf & "Part No :"

        '  Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).AxisLabel =

        annot.X = t.ToOADate

        'annot.AnchorOffsetX = 
        annot.BackColor = Color.BlanchedAlmond
        annot.AllowMoving = True
        annot.AllowSelecting = True
        annot.AllowTextEditing = True
        annot.IsMultiline = True
        annot.SmartLabelStyle.Enabled = True
        annot.SmartLabelStyle.IsMarkerOverlappingAllowed = False
        annot.SmartLabelStyle.IsOverlappedHidden = True
        annot.BackSecondaryColor = Color.White
        annot.BackGradientStyle = GradientStyle.LeftRight
        annot.ShadowColor = Color.Black

        annot.Y = 80

        chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).Label = ToDHMS(cycle_)
        chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).LabelBackColor = Color.White
        chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).LabelBorderColor = Color.Black
        chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1).LabelBorderDashStyle = ChartDashStyle.Solid

        chartTimeline.Annotations.Add(annot)
        chartTimeline.Annotations.Item(chartTimeline.Series("Cycle").Points.Count - 1).AnchorDataPoint = chartTimeline.Series("Cycle").Points(chartTimeline.Series("Cycle").Points.Count - 1)

    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' secondes to DHMS
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Private Function ToDHMS(inSeconds As Integer) As String

        Dim seconds As Integer
        seconds = inSeconds Mod 60
        inSeconds = (inSeconds - seconds) / 60
        Dim minutes As Integer
        minutes = inSeconds Mod 60
        inSeconds = (inSeconds - minutes) / 60
        Dim hours As Integer
        hours = inSeconds Mod 24
        inSeconds = (inSeconds - hours) / 24
        Dim days As Integer
        days = inSeconds
        hours = hours + days * 24

        If seconds = 59 Then
            seconds = 0
            minutes = minutes + 1
        End If

        If minutes = 59 Or minutes = 60 Then
            minutes = 0
            hours = hours + 1
        End If

        If minutes < 10 Then
            If hours < 10 Then
                Return "0" & hours & "h" & "0" & minutes & "m" & seconds
            Else
                Return hours & "h" & "0" & minutes & "m" & seconds
            End If
        Else
            If hours < 10 Then
                Return "0" & hours & "h" & minutes & "m" & seconds
            Else
                Return hours & "h" & minutes & "m" & seconds
            End If
        End If

    End Function


    Private Sub addbox(colors As Dictionary(Of String, Integer), colors_Ucase As Dictionary(Of String, Integer))

        Dim alpha As Integer = 255
        Dim backcolors As Color
        Dim backcolors2 As Color
        Dim backcolors3 As Color
        Try
            backcolors = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE ON"))
            backcolors = Color.FromArgb(alpha, backcolors.R, backcolors.G, backcolors.B)

            backcolors2 = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE OFF"))
            backcolors2 = Color.FromArgb(alpha, backcolors2.R, backcolors2.G, backcolors2.B)

            backcolors3 = System.Drawing.ColorTranslator.FromWin32(colors("SETUP"))
            backcolors3 = Color.FromArgb(alpha, backcolors3.R, backcolors3.G, backcolors3.B)

        Catch ex As Exception
            backcolors = Color.Green
            backcolors2 = Color.Red
            backcolors3 = Color.Blue

        End Try

        For Each serie In chartHistory.Series
            Dim box As New Label
            With box
                .Name = serie.Name

                Dim csilib As New CSI_Library.CSI_Library(False)

                csilib.connectionString = CSIFLEXSettings.Instance.ConnectionString

                If (loadingasCON And serie.Name = "LOADING") Then
                    serie.Name = "CYCLE ON"
                End If

                If (serie.Name = "_CON" Or serie.Name = "CYCLE ON") Then
                    .Text = "CYCLE ON"
                    box.BackColor = backcolors
                ElseIf (serie.Name = "_COFF" Or serie.Name = "CYCLE OFF") Then
                    .Text = "CYCLE OFF"
                    box.BackColor = backcolors2
                ElseIf (serie.Name = "_SETUP" Or serie.Name = "SETUP") Then
                    .Text = "SETUP"
                    box.BackColor = backcolors3
                Else
                    .Text = serie.Name
                    box.BackColor = System.Drawing.ColorTranslator.FromWin32(colors_Ucase(UCase(serie.Name).Replace(" ", "")))
                    box.BackColor = Color.FromArgb(alpha, box.BackColor.R, box.BackColor.G, box.BackColor.B)
                End If

                ''simple alternative
                Dim contrastcolor As Color
                Dim rdif As Integer, gdif As Integer, bdif As Integer

                If (serie.Color.R > 100 And serie.Color.R < 150) Then
                    rdif = 150
                Else
                    rdif = 255
                End If

                If (serie.Color.G > 100 And serie.Color.G < 150) Then
                    gdif = 150
                Else
                    gdif = 255
                End If

                If (serie.Color.B > 100 And serie.Color.B < 150) Then
                    bdif = 150
                Else
                    bdif = 255
                End If

                contrastcolor = Color.FromArgb(rdif - serie.Color.R, gdif - serie.Color.G, bdif - serie.Color.B)

                .Cursor = Cursors.Hand
                .Tag = serie.Name
                .ForeColor = contrastcolor 'Color.White
                '.BackColor = serie.Color
                .Visible = True
                .AutoSize = True

            End With

            'AddHandler box.Click, AddressOf clicked
            FlowLayoutPanel1.Controls.Add(box)
        Next

        FlowLayoutPanel1.Refresh()
    End Sub


    Public Shared Sub ColorToHSV(color As Color, ByRef hue As Double, ByRef saturation As Double, ByRef value As Double)

        Dim max As Integer = Math.Max(color.R, Math.Max(color.G, color.B))
        Dim min As Integer = Math.Min(color.R, Math.Min(color.G, color.B))

        hue = color.GetHue()
        saturation = If((max = 0), 0, 1.0 - (1.0 * min / max))
        value = max / 255.0

    End Sub


    Public Shared Function ColorFromHSV(hue As Double, saturation As Double, value As Double) As Color

        Dim hi As Integer = Convert.ToInt32(Math.Floor(hue / 60)) Mod 6
        Dim f As Double = hue / 60 - Math.Floor(hue / 60)

        value = value * 255
        Dim v As Integer = Convert.ToInt32(value)
        Dim p As Integer = Convert.ToInt32(value * (1 - saturation))
        Dim q As Integer = Convert.ToInt32(value * (1 - f * saturation))
        Dim t As Integer = Convert.ToInt32(value * (1 - (1 - f) * saturation))

        If hi = 0 Then
            Return Color.FromArgb(255, v, t, p)
        ElseIf hi = 1 Then
            Return Color.FromArgb(255, q, v, p)
        ElseIf hi = 2 Then
            Return Color.FromArgb(255, p, v, t)
        ElseIf hi = 3 Then
            Return Color.FromArgb(255, p, q, v)
        ElseIf hi = 4 Then
            Return Color.FromArgb(255, t, p, v)
        Else
            Return Color.FromArgb(255, v, p, q)
        End If

    End Function

#Region "Clic"

    'Unzoom with right clic
    Private Sub Chart2_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles chartHistory.Click
        If (e.Button.CompareTo(Forms.MouseButtons.Right) = 0) Then
            chartHistory.ChartAreas(0).AxisX.ScaleView.ZoomReset()
        End If
    End Sub


    'Dim Locked As Boolean
    'Private Sub CB_Lock_CheckedChanged(sender As Object, e As EventArgs)
    '    Locked = True
    'End Sub


    Private Sub Disp_labels_CheckedChanged(sender As Object, e As EventArgs) Handles Disp_labels.CheckedChanged

        If Disp_labels.Checked = True Then
            chartTimeline.Series("Cycle").SmartLabelStyle.Enabled = True
        Else
            chartTimeline.Series("Cycle").SmartLabelStyle.Enabled = False
        End If

    End Sub


    Private Sub BTN_RAW_VALUES_Click(sender As Object, e As EventArgs) Handles BTN_RAW_VALUES.Click

        Dim raw_form As New form_raw_values

        raw_form.Text = "Raw data without processing, " + Me.Text
        raw_form.CurrentShift = shiftno
        raw_form.MachineName = machineName
        raw_form.EnetMachineName = enetMachineName
        raw_form.DateTimeline = dateTimeline
        raw_form.ShiftStart = shiftStart
        raw_form.ShiftEnd = shiftEnd
        raw_form.Show()

    End Sub


    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles chartTimeline.Click

    End Sub

#End Region


#Region "Hide"

    ' Hide the Timeline button
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        original_height(0) = SplitContainer1.Height

        If SplitContainer1.Panel1Collapsed = True Then
            SplitContainer1.Panel1Collapsed = False
            ' Me.Height = Me.Height + h0

            Dim img As Image = My.Resources.d
            img.RotateFlip(RotateFlipType.Rotate180FlipNone)
            Button1.Image = img

        Else

            Dim img As Image = My.Resources.d
            img.RotateFlip(RotateFlipType.RotateNoneFlipNone)

            Button1.Image = img

            h0 = SplitContainer1.Panel1.Height
            SplitContainer1.Panel1Collapsed = True
            '  Me.Height = Me.Height - h0
        End If
    End Sub


    ' Hide the Hourly Timeline button
    Private Sub btnExpande_Click(sender As Object, e As EventArgs) Handles btnExpande.Click

        original_height(1) = SplitContainer1.Height

        SuspendLayout()

        If SplitContainer1.Panel2Collapsed = True Then

            SplitContainer1.Panel2Collapsed = False

            Me.Height = Me.Height + 250

            Dim img As Image = My.Resources.d
            img.RotateFlip(RotateFlipType.Rotate180FlipNone)
            btnExpande.Image = img

        Else

            Dim img As Image = My.Resources.d
            img.RotateFlip(RotateFlipType.RotateNoneFlipNone)

            btnExpande.Image = img

            h = SplitContainer1.Panel2.Height
            If Me.Height > 425 Then Me.Height = Me.Height - 250
            SplitContainer1.Panel2Collapsed = True

        End If

        ResumeLayout()

    End Sub
#End Region


End Class


Public Class EventDataModel
    Private _eventDateTime As DateTime
    Public Property EventDateTime() As DateTime
        Get
            Return _eventDateTime
        End Get
        Set(ByVal value As DateTime)
            _eventDateTime = value
        End Set
    End Property

    Private _value As Integer
    Public Property Value() As Integer
        Get
            Return _value
        End Get
        Set(ByVal value As Integer)
            _value = value
        End Set
    End Property
End Class


Public Class OverrideItem

    Private _eventDateTime As DateTime
    Public Property EventDateTime() As DateTime
        Get
            Return _eventDateTime
        End Get
        Set(ByVal value As DateTime)
            _eventDateTime = value
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

    Private _operatorName As String
    Public Property OperatorName() As String
        Get
            Return _operatorName
        End Get
        Set(ByVal value As String)
            _operatorName = value
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

    Private _cycleTime As Integer
    Public Property CycleTime() As Integer
        Get
            Return _cycleTime
        End Get
        Set(ByVal value As Integer)
            _cycleTime = value
        End Set
    End Property

    Private _feedrate As Integer
    Public Property Feedrate() As Integer
        Get
            Return _feedrate
        End Get
        Set(ByVal value As Integer)
            _feedrate = value
        End Set
    End Property

    Private _rapid As Integer
    Public Property Rapid() As Integer
        Get
            Return _rapid
        End Get
        Set(ByVal value As Integer)
            _rapid = value
        End Set
    End Property

    Private _spindle As Integer
    Public Property Spindle() As Integer
        Get
            Return _spindle
        End Get
        Set(ByVal value As Integer)
            _spindle = value
        End Set
    End Property

End Class

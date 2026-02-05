Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows
Imports System.Drawing
Imports System.Drawing.Drawing2D
 
Imports CSI_Library.CSI_Library

Imports System.Globalization

Public Class Form_Shift_1

    Public Structure periode
        Dim machine_name As String
        Dim date_ As String
        Dim shift1 As Dictionary(Of String, Double)

    End Structure
    Public h As Integer, h0 As Integer
    Public saved_points As New Dictionary(Of String, List(Of DataPoint)), original_height(2) As Integer

    Private Sub Form15_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim list_of_status As New List(Of String), cycle_ As Integer = 0
        Dim last_loop As Boolean = False

        Dim img As Image = My.Resources.d
        img.RotateFlip(RotateFlipType.Rotate180FlipNone)
        Button1.Image = img

        Dim colors_dict As New Dictionary(Of String, String) 'status/R G B
        Dim colors_dict_rand As New Dictionary(Of String, String) 'status/R G B , if generated 
        ' Specifying IntervalType
        Chart2.ChartAreas(0).AxisX.IntervalType = ChartValueType.DateTime


        Dim start_shift As DateTime, end_shift As DateTime
        Dim time_ As DateTime, shift As New DataView(Report_PieChart.Timeline)
        Dim status As String = "", Hourly_stat(1) As periode, item_ As Integer = 0, PreviousCycle As Integer, overshoot As Integer = 0

        Hourly_stat(1) = New periode

        Dim total As Integer = 0

        Try
            ' The list of the available status in this hour, and the start and end time.******************************************
            shift.RowFilter = "shift=1"
            For Each row As DataRowView In shift
                status = row.Item("status")

                ' CSIFLEX needs the total number of status in this shift:
                If Not list_of_status.Contains(status) And (status <> "_SH_START" And status <> "_SH_END" And Not status.Contains("_PARTNO")) Then list_of_status.Add(status)

            Next
            Dim test As Dictionary(Of String, String) = Reporting_application.ShiftSetup

            Dim combovalue As String() = Split(Report_PieChart.CB_Report.Text, " : ")


            ' start and end shift time          
            start_shift = shift.Item(0).Item("Time_")
            end_shift = shift.Item(shift.Count - 1).Item("Time_")
            end_shift = end_shift.AddSeconds(shift.Item(shift.Count - 1).Item("Cycletime"))

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
                Hourly_stat(item_ - 1).shift1 = New Dictionary(Of String, Double)
                Hourly_stat(item_ - 1).date_ = hour.Hour

                'filter the shift for just this hour
                shift.RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat,
                     "Time_ >= #{0}#", hour) & " and " & String.Format(CultureInfo.InvariantCulture.DateTimeFormat,
                     "Time_ <= #{0}#", hour.AddHours(1)) & " and Shift=" & 1

                'residus of the privious hour :
                If overshoot <> 0 Then
                    'if a cycle started at H-1
                    If overshoot > 3660 Then
                        Hourly_stat(item_ - 1).shift1.Add(PreviousCycleName, 3600)
                        overshoot = overshoot - 3600
                    Else
                        Hourly_stat(item_ - 1).shift1.Add(PreviousCycleName, overshoot)
                        overshoot = 0
                    End If
                End If

                'Cycle times for this hour
                For Each row In shift
                    status = row.Item("status")

                    If Not (status.Contains("_PARTNO") Or status.Contains("_SH_START") Or status.Contains("_SH_END")) Then
                        'Add new stattus if not present
                        If Not Hourly_stat(item_ - 1).shift1.ContainsKey(status) Then
                            Hourly_stat(item_ - 1).shift1.Add(status, row.Item("cycletime"))
                        Else
                            ' Else add the cycle time 
                            Hourly_stat(item_ - 1).shift1.Item(status) = Hourly_stat(item_ - 1).shift1.Item(status) + row.Item("cycletime")
                        End If
                    End If

                    PreviousCycleHour = row.Item("Time_")
                    PreviousCycleName = row.Item("status")
                    PreviousCycle = row.Item("CycleTime")
                Next

                ' - Last status overshoot :


                If PreviousCycleName <> "" And Not (status.Contains("_PARTNO") Or status.Contains("_SH_START") Or status.Contains("_SH_END")) Then
                    If PreviousCycle > DateDiff(DateInterval.Second, PreviousCycleHour, hour.AddHours(1)) And overshoot = 0 Then
                        overshoot = PreviousCycle - DateDiff(DateInterval.Second, PreviousCycleHour, hour.AddHours(1))
                        Hourly_stat(item_ - 1).shift1.Item(PreviousCycleName) = Hourly_stat(item_ - 1).shift1.Item(PreviousCycleName) - PreviousCycle + DateDiff(DateInterval.Second, PreviousCycleHour, hour.AddHours(1))
                        ' MessageBox.Show(DateDiff(DateInterval.Second, PreviousCycleHour, hour.AddHours(1)))
                    End If
                End If

                Hourly_stat(item_ - 1).shift1.Add("total", total)
                hour = hour.AddHours(1)
                starthour = starthour + 1
                If ((DateDiff(DateInterval.Minute, hour, end_shift)) < 59) And ((DateDiff(DateInterval.Minute, hour, end_shift)) > 0) Then last_loop = True

            End While
            last_loop = False

            '*********************************************************************************************************************
            Dim alpha As Integer = 255
            Dim backcolors As Color
            Dim backcolors2 As Color
            Dim backcolors3 As Color

            Dim colors As Dictionary(Of String, Integer)
            colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_eNET)

            backcolors = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE ON"))
            backcolors = Color.FromArgb(alpha, backcolors.R, backcolors.G, backcolors.B)

            backcolors2 = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE OFF"))
            backcolors2 = Color.FromArgb(alpha, backcolors2.R, backcolors2.G, backcolors2.B)

            backcolors3 = System.Drawing.ColorTranslator.FromWin32(colors("SETUP"))
            backcolors3 = Color.FromArgb(alpha, backcolors3.R, backcolors3.G, backcolors3.B)


            ReDim Preserve Hourly_stat(UBound(Hourly_stat) - 1)

            Dim HexCode As String, red As Integer, green As Integer, blue As Integer
            Dim dic As Dictionary(Of String, String) = Report_PieChart.colors
            'Add series to chart2 for the 'OTHER' status
            For Each item In list_of_status
                If (item <> "_CON" And item <> "_COFF" And item <> "_SETUP") Then
                    Chart2.Series.Add(item)
                    Chart2.Series(item).ChartType = SeriesChartType.Column
                    Chart2.Series(item).XValueType = ChartValueType.DateTime
                    Chart2.Series(item).ToolTip = item
                    Chart2.Series(item).IsXValueIndexed = True
                    If (Chart2.Series(item).Name <> "_CON" And Chart2.Series(item).Name <> "_COFF" And Chart2.Series(item).Name <> "_SETUP") Then

                        If Report_PieChart.colors.ContainsKey(UCase(item.Replace(" ", ""))) Then

                            HexCode = (Report_PieChart.colors.Item(UCase(item.Replace(" ", "")))).ToString
                            red = Val("&H" & Mid(HexCode, 1, 2))
                            green = Val("&H" & Mid(HexCode, 3, 2))
                            blue = Val("&H" & Mid(HexCode, 5, 2))
                            Chart2.Series(item).Color = Color.FromArgb(red, green, blue)
                            If Not colors_dict.ContainsKey(item.Replace(" ", "")) Then colors_dict.Add(item.Replace(" ", ""), HexCode)
                        Else

                            '  Dim someColor As Color = Color.FromArgb(rand1(0, 256), Random.Next(0, 256), Random.Next(0, 256))
                            Dim alreadyChoosenColors As List(Of Color) = New List(Of Color)
                            Dim rand As New Random()
                            Dim redColor As Integer = rand.Next(0, 255)
                            Dim greenColor As Integer = rand.Next(0, 255)
                            Dim blueColor As Integer = rand.Next(0, 255)

                            Dim someColor As Color = Color.FromArgb(redColor, greenColor, blueColor)
                            red = someColor.R
                            green = someColor.G
                            blue = someColor.B
                            Chart2.Series(item).Color = Color.FromArgb(red, green, blue)
                            HexCode = "ff" & System.Drawing.ColorTranslator.ToHtml(someColor).Remove(0, 1)

                            If Not colors_dict.ContainsKey(item.Replace(" ", "")) Then colors_dict.Add(item.Replace(" ", ""), HexCode)
                        End If



                    End If
                End If
            Next


            'Convert the cycle times to percent and Fill the chart 2 : Hourly TimeLine
            For Each item In Hourly_stat 'For each hour
                If item.shift1 Is Nothing Then
                Else

                    For Each status In list_of_status

                        If item.shift1.Keys.Contains(status) Then
                            If total = 0 Then
                                Chart2.Series(status).Points.AddXY(Convert.ToDateTime(item.date_ & ":00"), 0)
                            Else
                                Chart2.Series(status).Points.AddXY(Convert.ToDateTime(item.date_ & ":00"), item.shift1.Item(status) * 100 / item.shift1.Item("total"))
                            End If
                        Else
                            Chart2.Series(status).Points.AddXY(Convert.ToDateTime(item.date_ & ":00"), 0)
                        End If

                    Next
                End If
            Next


            Chart2.Series("_CON").ToolTip = "Cycle On"
            Chart2.Series("_COFF").ToolTip = "Cycle Off"
            Chart2.Series("_SETUP").ToolTip = "Setup"



            Chart2.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm:ss"

            Chart2.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Hours
            Chart2.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm:ss"
            Chart2.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Hours
            Chart2.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Hours


            '*********************************************************************************************************************
            '*********************************************************************************************************************
            ' TimeLine:
            '*********************************************************************************************************************
            '*********************************************************************************************************************

            shift.RowFilter = "SHIFT =" & 1
            time_ = shift.Item(0).Item("Time_")
            Dim t0 As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)
            Chart1.Series("Cycle").Points.AddXY(t0.ToOADate, 100)
            Chart1.ChartAreas(0).AxisX.Minimum = t0.ToOADate
            Chart1.ChartAreas(0).AxisX.Maximum = end_shift.ToOADate

            'status = shift.Item(0).Item("status")


            Dim stat As String = shift.Item(0).Item("status")
            If stat <> "" And Not stat.Contains("_PARTNO") Then
                Select Case stat
                    Case "_CON"
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors
                    Case "_COFF"
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors2
                    Case "_SETUP"
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors3
                    Case ""
                    Case Else
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Chart2.Series(status).Color
                End Select
            End If



            For Each row In shift

                '   Call annotation(row)
                time_ = row.Item("Time_")

                Dim t As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)


                If (Not status.Contains("PARTNO")) And status <> "" Then

                    Chart1.Series("Cycle").Points.AddXY(t.ToOADate, 100)

                    Select Case status
                        Case "_CON"
                            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors
                        Case "_COFF"
                            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors2
                        Case "_SETUP"
                            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors3
                        Case ""
                        Case Else
                            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Chart2.Series(status).Color
                    End Select
                End If

                status = row.item("status")

            Next

            Chart1.Series("Cycle").Points.AddXY(end_shift.ToOADate, 100)

            Select Case status
                Case "_CON"
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors
                Case "_COFF"
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors2
                Case "_SETUP"
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors3
                Case ""

                Case Else
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Chart2.Series(status).Color
            End Select



            Chart1.Series("Cycle").XValueType = ChartValueType.DateTime
            Chart1.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Seconds
            Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm:ss"
            Chart1.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
            Chart1.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds
            Chart1.Series("Cycle").SmartLabelStyle.Enabled = True
            Chart1.Series("Cycle").SmartLabelStyle.IsMarkerOverlappingAllowed = False
            Chart1.Series("Cycle").SmartLabelStyle.IsOverlappedHidden = True


            combovalue = Split(Report_PieChart.CB_Report.Text, " : ")
            Me.Text = "Machine : " & combovalue(0) & "   -   Shift 1, Date : " & combovalue(1)


            addbox()
            h0 = SplitContainer1.Panel1.Height
            h = SplitContainer1.Panel2.Height
            Button2.PerformClick()
            Me.Height = Report_PieChart.DataGridView1.Height
        Catch ex As Exception
            MessageBox.Show("CSIFLEX cannot display the Time line : " & ex.Message)
        End Try
    End Sub

    'ANNOTATION'''''''''''''''''''''''''''''''''''''''''
    Private Sub annotation(ByRef row As DataRowView)
        Dim time_ As New DateTime, cycle_ As New Integer

        time_ = row.Item("Time_")
        cycle_ = row.Item("cycletime")
        Dim t As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)


        Dim annot As RectangleAnnotation = New RectangleAnnotation
        annot.Name = t.Second & ":" & t.Minute & ":" & t.Hour
        annot.Text = ToDHMS(cycle_) ' & vbCrLf & "Start : " & t.Hour.ToString & "h" & t.Minute.ToString & "m" & t.Second.ToString & vbCrLf & "End :" & vbCrLf & "Part No :"

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

        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Label = ToDHMS(cycle_)
        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).LabelBackColor = Color.White
        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).LabelBorderColor = Color.Black
        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).LabelBorderDashStyle = ChartDashStyle.Solid


        Chart1.Annotations.Add(annot)
        Chart1.Annotations.Item(Chart1.Series("Cycle").Points.Count - 1).AnchorDataPoint = Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1)

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

    Private Sub addbox()

        For Each serie In Chart2.Series
            Dim box As New Label
            With box
                .Name = serie.Name
                Select Case serie.Name
                    Case "_CON"
                        .Text = "Cycle Onaaaaaaaaaaaaaaa"
                    Case "_COFF"
                        .Text = "Cycle Off"
                    Case "_SETUP"
                        .Text = "SETUP"
                    Case Else
                        .Text = serie.Name
                End Select

                '''''''''''''''''inverse color for background


                'Dim original As Color = serie.Color 'Color.FromArgb(50, 120, 200)
                '' original = {Name=ff3278c8, ARGB=(255, 50, 120, 200)}

                'Dim hue As Double
                'Dim saturation As Double
                'Dim value As Double
                'ColorToHSV(original, hue, saturation, value)
                '' hue        = 212.0
                '' saturation = 0.75
                '' value      = 0.78431372549019607

                'Dim copy As Color = ColorFromHSV(360 - hue, saturation, value)
                '' copy = {Name=ff3278c8, ARGB=(255, 50, 120, 200)}

                '' Compare that to the HSL values that the .NET framework provides: 
                'original.GetHue()
                '' 212.0
                'original.GetSaturation()
                '' 0.6
                'original.GetBrightness()
                '' 0.490196079

                ''simple alternative
                Dim contrastcolor As Color
                contrastcolor = Color.FromArgb(255 - serie.Color.R, 255 - serie.Color.G, 255 - serie.Color.B)

                ''''''''''''''''''''''''''''''''''''



                .Cursor = Cursors.Hand
                .Tag = serie.Name
                .ForeColor = contrastcolor 'Color.White
                .BackColor = serie.Color
                .Visible = True
            End With

            AddHandler box.Click, AddressOf clicked
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
    'Click on the labels
    Private Sub clicked(ByVal sender As Object, ByVal e As System.EventArgs)

        'Dim shift As New DataView(Form7.Timeline)
        'Dim time_ As Date, status As String, pts As Integer = 0

        'shift.RowFilter = "SHIFT =" & 1

        'If sender.ToString.Contains("Cycle On") Then
        '    If saved_points.ContainsKey("Cycle On") Then
        '        'For Each Point As DataPoint In saved_points.Item("Cycle On")
        '        '    Chart1.Series(0).Points(Point.XValue).Color = Color.Green
        '        'Next
        '        saved_points.Remove("Cycle On")

        '        For Each row In shift
        '            time_ = row.Item("Time_")
        '            Dim t As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)
        '            If status = "_CON" Then Chart1.Series("Cycle").Points(pts).Color = Color.Green

        '            status = row.item("status")
        '            pts += 1
        '        Next
        '    Else
        '        Dim points As New List(Of DataPoint)
        '        saved_points.Add("Cycle On", points)
        '        For Each Point As DataPoint In Chart1.Series(0).Points
        '            If Point.Color = Color.Green Then Point.Color = Color.Transparent
        '            points.Add(Point)
        '        Next
        '        '  saved_points.Item("Cycle On") = points
        '    End If

        'End If
        'If (sender.ToString.Contains("Cycle Off")) Then
        '    If saved_points.ContainsKey("Cycle Off") Then
        '        saved_points.Remove("Cycle Off")
        '        For Each row In shift
        '            time_ = row.Item("Time_")
        '            Dim t As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)
        '            If status = "_COFF" Then Chart1.Series("Cycle").Points(pts).Color = Color.Red

        '            status = row.item("status")
        '            pts += 1
        '        Next

        '    Else
        '        Dim points As New List(Of DataPoint)
        '        saved_points.Add("Cycle Off", points)
        '        For Each Point As DataPoint In Chart1.Series(0).Points
        '            If Point.Color = Color.Red Then Point.Color = Color.Transparent
        '            points.Add(Point)
        '        Next
        '        saved_points.Item("Cycle Off") = points
        '    End If
        'End If

        'If sender.ToString.Contains("SETUP") Then
        '    If saved_points.ContainsKey("SETUP") Then
        '        For Each row In shift
        '            time_ = row.Item("Time_")
        '            Dim t As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)
        '            If status = "_SETUP" Then Chart1.Series("Cycle").Points(pts).Color = Color.Blue

        '            status = row.item("status")
        '            pts += 1
        '        Next

        '    Else
        '        Dim points As New List(Of DataPoint)
        '        saved_points.Add("SETUP", points)
        '        For Each Point As DataPoint In Chart1.Series(0).Points
        '            If Point.Color = Color.Blue Then Point.Color = Color.Transparent
        '            points.Add(Point)
        '        Next
        '        saved_points.Item("SETUP") = points
        '    End If
        'End If

    End Sub

    'Unzoom with right clic
    Private Sub Chart2_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart2.Click
        If (e.Button.CompareTo(Forms.MouseButtons.Right) = 0) Then
            Chart2.ChartAreas(0).AxisX.ScaleView.ZoomReset()
        End If
    End Sub

    Private Sub Chart2_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart2.DoubleClick
        'If (e.Button.CompareTo(Forms.MouseButtons.Left) = 0) Then
        '    Dim HTR As HitTestResult
        '    Dim t As DateTime
        '    HTR = Chart2.HitTest(e.X, e.Y)
        '    If HTR.PointIndex > -1 Then t = Date.FromOADate((Chart2.Series(0).Points(HTR.PointIndex).XValue))
        '    Chart1.ChartAreas(0).AxisX.ScaleView.Zoom(HTR.PointIndex, HTR.PointIndex + 1)
        'End If
    End Sub

    Private Sub Chart1_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart1.Click
        If (e.Button.CompareTo(Forms.MouseButtons.Right) = 0) Then
            Chart1.ChartAreas(0).AxisX.ScaleView.ZoomReset()
        End If
    End Sub


#End Region



#Region "Hide"
    ' Hide the Timeline button
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        original_height(0) = SplitContainer1.Height

        If SplitContainer1.Panel1Collapsed = True Then
            SplitContainer1.Panel1Collapsed = False
            Me.Height = Me.Height + h0

            Dim img As Image = My.Resources.d
            img.RotateFlip(RotateFlipType.Rotate180FlipNone)
            Button1.Image = img

        Else

            Dim img As Image = My.Resources.d
            img.RotateFlip(RotateFlipType.RotateNoneFlipNone)

            Button1.Image = img


            h0 = SplitContainer1.Panel1.Height
            SplitContainer1.Panel1Collapsed = True
            Me.Height = Me.Height - h0
        End If
    End Sub


    ' Hide the Hourly Timeline button
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        original_height(1) = SplitContainer1.Height

        If SplitContainer1.Panel2Collapsed = True Then


            Dim img As Image = My.Resources.d
            img.RotateFlip(RotateFlipType.Rotate180FlipNone)
            Button2.Image = img

            SplitContainer1.Panel2Collapsed = False
            Me.Height = Me.Height + h

        Else

            Dim img As Image = My.Resources.d
            img.RotateFlip(RotateFlipType.RotateNoneFlipNone)

            Button2.Image = img

            h = SplitContainer1.Panel2.Height
            SplitContainer1.Panel2Collapsed = True
            Me.Height = Me.Height - h
        End If
    End Sub
#End Region



End Class
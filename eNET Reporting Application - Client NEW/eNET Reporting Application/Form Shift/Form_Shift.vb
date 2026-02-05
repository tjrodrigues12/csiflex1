Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Collections.Specialized
Imports System.Collections.Generic

Imports CSI_Library.CSI_Library

Imports System.Globalization
Imports System.IO

Public Class Form_Shift

    Public shiftno As Integer = 0

    Public Structure periode
        Dim machine_name As String
        Dim date_ As String
        Dim shift As Dictionary(Of String, Double)

    End Structure
    ' Dim colorList As New NameValueCollection
    'Dim colorList As List(Of KeyValuePair(Of String, System.Drawing.Color)) =
    '    New List(Of KeyValuePair(Of String, System.Drawing.Color))

    '  Dim colorList As New Dictionary(Of String, System.Drawing.Color)

    Public h As Integer, h0 As Integer
    Public saved_points As New Dictionary(Of String, List(Of DataPoint)), original_height(2) As Integer
    Dim shift As DataView 'New DataView(Form7.Timeline)

    Private Sub Form_Shift_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Height = 280 'Report_PieChart.DataGridView1.Height + 30
        Me.Width = 793 'Report_PieChart.DataGridView1.Width + Report_PieChart.ListBox1.Width + 30
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
        Chart2.ChartAreas(0).AxisX.IntervalType = ChartValueType.DateTime


        Dim start_shift As DateTime, end_shift As DateTime
        Dim time_ As DateTime

        If (shiftno = 1) Then
            shift = New DataView(Report_PieChart.Timeline)
            Me.Location = New System.Drawing.Point(Report_PieChart.Location.X + Report_PieChart.DataGridView1.Location.X, Report_PieChart.Location.Y + Report_PieChart.DataGridView1.Location.Y)
        ElseIf (shiftno = 2) Then
            shift = New DataView(Report_PieChart.Timeline2)
        ElseIf (shiftno = 3) Then
            shift = New DataView(Report_PieChart.Timeline3)
        End If

        Dim previous_AM_PM As String = ""
        For Each row As DataRowView In shift
            time_ = row.Item("Time_")
            If previous_AM_PM = "PM" And time_.ToString("tt") = "AM" Then
                time_ = time_.AddDays(1)
                row.Item("Time_") = time_.ToString()
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

        Dim csilib As New CSI_Library.CSI_Library
        'Dim loadingascon = False 'csilib.GetLoadingAsCON()
        'If (csilib.CheckLic(2) = 3) Then
        '    Try
        '        Dim db_authPath As String = Nothing
        '        Dim directory As String = csilib.getRootPath()
        '        If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
        '            Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
        '                db_authPath = reader.ReadLine()
        '            End Using
        '        End If
        '        Dim connectionString As String
        '        connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

        '        loadingascon = csilib.GetLoadingAsCON(connectionString)
        '    Catch ex As Exception

        '    End Try
        'End If


        Try

            colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)

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
            start_shift = shift.Item(0).Item("Time_")
            end_shift = shift.Item(shift.Count - 1).Item("Time_")
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
                     "Time_ >= #{0}#", hour) & " and " & String.Format(CultureInfo.InvariantCulture.DateTimeFormat,
                     "Time_ <= #{0}#", hour.AddHours(1)) & " and Shift=" & shiftno

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

                    PreviousCycleHour = row.Item("Time_")
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
                    Chart2.Series.Add(item)
                    Chart2.Series(item).ChartType = SeriesChartType.Column
                    Chart2.Series(item).XValueType = ChartValueType.DateTime
                    Chart2.Series(item).ToolTip = item
                    Chart2.Series(item).IsXValueIndexed = True

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

                    If True Then
                        If dic.ContainsKey(temp) Then 'Report_PieChart.colors.ContainsKey(item) Then '(UCase(item.Replace(" ", ""))) Then
                            Dim color As Color
                            color = System.Drawing.ColorTranslator.FromWin32(dic(temp.Replace(" ", "")))
                            color = Color.FromArgb(alpha, color.R, color.G, color.B)
                            Chart2.Series(item).Color = color
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
                            Chart2.Series(item).Color = someColor
                            HexCode = "ff" & System.Drawing.ColorTranslator.ToHtml(someColor).Remove(0, 1)

                            If Not colors_dict.ContainsKey(item.Replace(" ", "")) Then colors_dict.Add(item.Replace(" ", ""), HexCode)
                        End If



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
                                Chart2.Series(status).Points.AddXY(Convert.ToDateTime(item.date_ & ":00"), 0)
                            Else
                                Chart2.Series(status).Points.AddXY(Convert.ToDateTime(item.date_ & ":00"), item.shift.Item(status) * 100 / item.shift.Item("total"))
                            End If
                        Else
                            Chart2.Series(status).Points.AddXY(Convert.ToDateTime(item.date_ & ":00"), 0)
                        End If

                    Next
                End If
            Next


            Chart2.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Hours
            Chart2.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm:ss"
            Chart2.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Hours
            Chart2.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Hours


            '*********************************************************************************************************************
            '*********************************************************************************************************************
            ' TimeLine:
            '*********************************************************************************************************************
            '*********************************************************************************************************************



            Dim Cycle_time As Integer = 0
            shift.RowFilter = "SHIFT =" & shiftno
            time_ = shift.Item(0).Item("Time_")

            Dim t0 As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)
            Chart1.Series("Cycle").Points.AddXY(t0.ToOADate, 100)
            If Not shift.Item(0).Item("status").ToString().Contains("PARTNO") Then
                Cycle_time = shift.Item(0).Item("cycletime")
                Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Label = New TimeSpan(0, 0, Cycle_time).ToString()
            End If


            Chart1.ChartAreas(0).AxisX.Minimum = t0.ToOADate
            Chart1.ChartAreas(0).AxisX.Maximum = end_shift.ToOADate


            Chart_parts.ChartAreas(0).AxisX.Minimum = t0.ToOADate
            Chart_parts.ChartAreas(0).AxisX.Maximum = end_shift.ToOADate
            'status = shift.Item(0).Item("status")




            Dim stat As String = shift.Item(0).Item("status")
            If stat <> "" And Not stat.Contains("_PARTN") Then

                If (loadingasCON And stat = "LOADING") Then
                    stat = "CYCLE ON"
                End If


                If (stat = "_CON" Or stat = "CYCLE ON") Then
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).LabelBackColor = backcolors
                ElseIf (stat = "_COFF" Or stat = "CYCLE OFF") Then
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors2
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).LabelBackColor = backcolors2
                ElseIf (stat = "_SETUP" Or stat = "SETUP") Then
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors3
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).LabelBackColor = backcolors3
                Else
                    Dim color As Color
                    color = System.Drawing.ColorTranslator.FromWin32(colors(stat))
                    color = Color.FromArgb(alpha, color.R, color.G, color.B)
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = color
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).LabelBackColor = color
                End If

                ' Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).ToolTip = status
                ' Chart1.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).Label = "    " + stat
            Else
                ' IF PART :::



                Chart_parts.Series("Cycle").Points.AddXY(t0.ToOADate, 70)
                '  Chart_parts.Series("Cycle").Points(Chart_parts.Series("Cycle").Points.Count - 1).Color = Color.Aqua

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
                time_ = row.Item("Time_")

                '  status = row.item("status")
                Dim t As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)


                If (Not status.Contains("PARTN")) And status <> "" Then

                    '////////////////////////////////////////////////////////////////////////
                    'change the minimum or maximum of the Axis in case the DB is not in order
                    If Chart1.ChartAreas(0).AxisX.Minimum > t.ToOADate Then
                        Chart1.ChartAreas(0).AxisX.Minimum = t.ToOADate
                    End If

                    If Chart1.ChartAreas(0).AxisX.Maximum < t.ToOADate Then
                        Chart1.ChartAreas(0).AxisX.Maximum = t.ToOADate
                    End If

                    '////////////////////////////////////////////////////////////////////////

                    Chart1.Series("Cycle").Points.AddXY(t.ToOADate, 100)
                    If Cycle_time <> 0 Then Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).Label = New TimeSpan(0, 0, Cycle_time).ToString()

                    If (loadingasCON And status = "LOADING") Then
                        status = "CYCLE ON"
                    End If

                    If (status = "_CON" Or status = "CYCLE ON") Then
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).LabelBackColor = backcolors
                    ElseIf (status = "_COFF" Or status = "CYCLE OFF") Then
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors2
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).LabelBackColor = backcolors2
                    ElseIf (status = "_SETUP" Or status = "SETUP") Then
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors3
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).LabelBackColor = backcolors3
                    Else
                        Dim color As Color
                        Try
                            color = System.Drawing.ColorTranslator.FromWin32(colors_Ucase(UCase(status.Replace(" ", ""))))
                            color = Color.FromArgb(alpha, color.R, color.G, color.B)
                            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = color
                            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).LabelBackColor = color
                            'Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Chart2.Series(status).Color
                        Catch ex As Exception
                            MessageBox.Show("CSIFLEX can not display correctly the Time line : the color for '" & status.ToString() & "' was absent")
                            color = Drawing.Color.White 'backcolors4
                            color = Color.FromArgb(alpha, color.R, color.G, color.B)
                            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = color
                            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).LabelBackColor = color
                        End Try
                    End If


                    '  If first_row = False And Chart_parts.Series("Cycle").Points.Count > 1 Then Chart1.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 2).Label = status
                Else
                    ' IF PART :::
                    If status <> "" Then
                        If Chart_parts.ChartAreas(0).AxisX.Maximum < t.ToOADate Then
                            Chart_parts.ChartAreas(0).AxisX.Maximum = t.ToOADate
                        End If
                        If first_row = False Then

                            Chart_parts.Series("Cycle").Points.AddXY((PreviousTime.AddSeconds(-DateDiff(DateInterval.Second, t, PreviousTime) / 2).ToOADate), 70)
                            Chart_parts.Series("Cycle").Points(Chart_parts.Series("Cycle").Points.Count - 1).Color = tmp_color

                            Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).Label = Previous_status
                            Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).SetCustomProperty("LabelStyle", "Top")

                            Chart_parts.Series("Cycle").Points.AddXY(t.ToOADate, 70)
                            Chart_parts.Series("Cycle").Points(Chart_parts.Series("Cycle").Points.Count - 1).Color = tmp_color

                            tmp_color = rnd_color()
                        Else
                            tmp_color = rnd_color()
                            Chart_parts.Series("Cycle").Points.AddXY(t.ToOADate, 70)
                            Chart_parts.Series("Cycle").Points(Chart_parts.Series("Cycle").Points.Count - 1).Color = tmp_color
                            Chart_parts.Series("Cycle").Points(Chart_parts.Series("Cycle").Points.Count - 1).LabelBackColor = tmp_color
                            first_row = False

                        End If

                        PreviousTime = t
                        Previous_status = status.Remove(0, 8).ToString()


                    End If
                End If

                status = row.item("status")
                Cycle_time = row.Item("cycletime")
                LastPreviousTime = row.Item("Time_")

            Next







            Chart1.Series("Cycle").Points.AddXY(end_shift.ToOADate, 100)
            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).Color
            '  Chart_parts.Series("Cycle").Points.AddXY(end_shift.ToOADate, 40)
            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).Label = New TimeSpan(0, 0, Cycle_time).ToString()



            If (loadingasCON And status = "LOADING") Then
                status = "CYCLE ON"
            End If

            If (status = "_CON" Or status = "CYCLE ON") Then
                Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors
                Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).LabelBackColor = backcolors
            ElseIf (status = "_COFF" Or status = "CYCLE OFF") Then
                Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors2
                Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).LabelBackColor = backcolors2
            ElseIf (status = "_SETUP" Or status = "SETUP") Then
                Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = backcolors3
                Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).LabelBackColor = backcolors3
            ElseIf (status.Contains("PARTN")) Then

                'Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).Label = status
                'Chart_parts.Series("Cycle").Points(Chart_parts.Series("Cycle").Points.Count - 1).LabelBackColor = tmp_color

                If Chart_parts.ChartAreas(0).AxisX.Maximum < end_shift.ToOADate Then
                    Chart_parts.ChartAreas(0).AxisX.Maximum = end_shift.ToOADate
                End If

                Chart_parts.Series("Cycle").Points.AddXY(LastPreviousTime, 70)
                Chart_parts.Series("Cycle").Points(Chart_parts.Series("Cycle").Points.Count - 1).Color = tmp_color

                Chart_parts.Series("Cycle").Points.AddXY((end_shift.AddSeconds(-DateDiff(DateInterval.Second, end_shift, LastPreviousTime) / 2).ToOADate), 70)
                Chart_parts.Series("Cycle").Points(Chart_parts.Series("Cycle").Points.Count - 1).Color = tmp_color

                Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).Label = status.Remove(0, 8).ToString()
                Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).SetCustomProperty("LabelStyle", "Top")

                Chart_parts.Series("Cycle").Points.AddXY(end_shift.ToOADate, 70)
                Chart_parts.Series("Cycle").Points(Chart_parts.Series("Cycle").Points.Count - 1).Color = tmp_color

            Else

                Dim color As Color
                Try
                    color = System.Drawing.ColorTranslator.FromWin32(colors_Ucase(UCase(status.Replace(" ", ""))))
                    color = Color.FromArgb(alpha, color.R, color.G, color.B)
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = color
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).LabelBackColor = color
                    ' Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Chart2.Series(status).Color
                Catch ex As Exception
                    MessageBox.Show("CSIFLEX cannot display correctly the Time line : the key '" & status.ToString() & "' was absent")
                    color = backcolors4
                    color = Color.FromArgb(alpha, color.R, color.G, color.B)
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 2).Color = color
                    ' Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Chart2.Series(status).Color
                End Try
            End If


            If Not (status.Contains("PARTN")) Then
                If Chart_parts.Series("Cycle").Points.Count > 0 Then
                    Chart_parts.Series("Cycle").Points.AddXY((PreviousTime.AddSeconds(-DateDiff(DateInterval.Second, end_shift, PreviousTime) / 2).ToOADate), 70)
                    'Chart_parts.Series("Cycle").Points(Chart_parts.Series("Cycle").Points.Count - 1).Color = Color.Aqua
                    If Previous_status = "" Then Previous_status = "No Part N°"
                    Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).Label = Previous_status
                    Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).SetCustomProperty("LabelStyle", "Top")

                    Chart_parts.Series("Cycle").Points.AddXY(end_shift.ToOADate, 70)
                    ' Chart_parts.Series("Cycle").Points(Chart_parts.Series("Cycle").Points.Count - 1).Color = Color.Aqua
                Else
                    Chart_parts.Series("Cycle").Points.AddXY(t0.ToOADate, 70)
                    Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).Color = Color.LightGray
                    Chart_parts.Series("Cycle").Points.AddXY(end_shift.ToOADate, 70)
                    Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).Color = Color.LightGray
                    Chart_parts.Series("Cycle").Points.AddXY((start_shift.AddSeconds(-DateDiff(DateInterval.Second, end_shift, start_shift) / 2).ToOADate), 70)
                    Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).Label = "No data for the parts"
                    Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).SetCustomProperty("LabelStyle", "Top")
                    Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).LabelBackColor = Color.LightGray
                    Chart_parts.Series("Cycle").Points.Item(Chart_parts.Series("Cycle").Points.Count - 1).Color = Color.LightGray
                End If
            End If

            Chart1.Series("Cycle").XValueType = ChartValueType.DateTime
            Chart1.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Seconds
            Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm:ss"
            Chart1.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
            Chart1.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds


            'Chart1.Series("Cycle").SmartLabelStyle.Enabled = False
            'Chart1.Series("Cycle").SmartLabelStyle.AllowOutsidePlotArea = True
            'Chart1.Series("Cycle").SmartLabelStyle.IsMarkerOverlappingAllowed = False
            'Chart1.Series("Cycle").SmartLabelStyle.IsOverlappedHidden = True
            'Chart1.Series("Cycle").SmartLabelStyle.CalloutLineAnchorCapStyle = LineAnchorCapStyle.Round
            'Chart1.Series("Cycle").SmartLabelStyle.CalloutStyle = LabelCalloutStyle.Box
            'Chart1.Series("Cycle").SmartLabelStyle.CalloutBackColor = Color.White



            Chart1.Series("Cycle").SmartLabelStyle.AllowOutsidePlotArea = True
            Chart1.Series("Cycle").SmartLabelStyle.IsMarkerOverlappingAllowed = False
            Chart1.Series("Cycle").SmartLabelStyle.IsOverlappedHidden = True
            Chart1.Series("Cycle").SmartLabelStyle.CalloutLineAnchorCapStyle = LineAnchorCapStyle.None
            Chart1.Series("Cycle").SmartLabelStyle.MaxMovingDistance = 1
            Chart1.Series("Cycle").SmartLabelStyle.CalloutStyle = LabelCalloutStyle.Box
            ' Chart1.Series("Cycle").SmartLabelStyle.CalloutBackColor = Color.DarkOrange
            ' Chart1.Series("Cycle").LabelBackColor = Color.DarkOrange
            Chart1.Series("Cycle").LabelBorderColor = Color.Black
            Chart1.Series("Cycle").SmartLabelStyle.Enabled = False


            combovalue = Split(Report_PieChart.CB_Report.Text, " : ")
            Me.Text = "Machine : " & combovalue(0) & "   -   Shift " & shiftno & ", Date : " & combovalue(1)


            addbox(colors, colors_Ucase)
            h0 = SplitContainer1.Panel1.Height
            h = SplitContainer1.Panel2.Height

            '  SplitContainer1.SplitterDistance = 200
            Button2.PerformClick()
            ' Me.Height = Report_PieChart.DataGridView1.Height





            '*********************************************************************************************************************
            '*********************************************************************************************************************
            ' PARTS:
            '*********************************************************************************************************************
            '*********************************************************************************************************************



            Chart_parts.Series("Cycle").XValueType = ChartValueType.DateTime
            Chart_parts.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Seconds
            Chart_parts.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm:ss"
            Chart_parts.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
            Chart_parts.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds

            Chart_parts.Series("Cycle").SmartLabelStyle.Enabled = True
            Chart_parts.Series("Cycle").SmartLabelStyle.AllowOutsidePlotArea = True
            Chart_parts.Series("Cycle").SmartLabelStyle.IsMarkerOverlappingAllowed = False
            Chart_parts.Series("Cycle").SmartLabelStyle.IsOverlappedHidden = True
            Chart_parts.Series("Cycle").SmartLabelStyle.CalloutLineAnchorCapStyle = LineAnchorCapStyle.Round
            'Chart_parts.Series("Cycle").SmartLabelStyle.MinMovingDistance = 10
            Chart_parts.Series("Cycle").SmartLabelStyle.CalloutStyle = LabelCalloutStyle.Box
            Chart_parts.Series("Cycle").SmartLabelStyle.CalloutBackColor = Color.DarkOrange
            Chart_parts.Series("Cycle").LabelBackColor = Color.DarkOrange
            Chart_parts.Series("Cycle").LabelBorderColor = Color.Black
            'Chart_parts.Visible = False


            Chart1.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
            Chart1.ChartAreas(0).CursorX.Interval = 1
            Chart_parts.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
            Chart_parts.ChartAreas(0).CursorX.Interval = 1

            'Chart_parts.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds
            'Chart1.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds

            'Chart_parts.ChartAreas(0).AxisX.ScaleView.SmallScrollSizeType = DateTimeIntervalType.Seconds
            'Chart1.ChartAreas(0).AxisX.ScaleView.SmallScrollSizeType = DateTimeIntervalType.Seconds

            Chart_parts.ChartAreas(0).AxisX.ScaleView.MinSize = 1
            Chart1.ChartAreas(0).AxisX.ScaleView.MinSize = 1

            Chart_parts.ChartAreas(0).AxisX.ScaleView.Zoomable = True
            Chart1.ChartAreas(0).AxisX.ScaleView.Zoomable = True

            Chart_parts.ChartAreas(0).CursorX.IsUserEnabled = True
            Chart_parts.ChartAreas(0).CursorX.IsUserSelectionEnabled = True
            Chart1.ChartAreas(0).CursorX.IsUserEnabled = True
            Chart1.ChartAreas(0).CursorX.IsUserSelectionEnabled = True


            Chart1.ChartAreas(0).AxisX.ScrollBar.Enabled = False
            Chart_parts.ChartAreas(0).AxisX.ScrollBar.Enabled = False







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

        time_ = row.Item("Time_")
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

    Private Sub addbox(colors As Dictionary(Of String, Integer), colors_Ucase As Dictionary(Of String, Integer))
        Dim alpha As Integer = 255


        ' Dim colors As Dictionary(Of String, Integer) 
        '  Dim colors_Ucase As New Dictionary(Of String, Integer)
        Dim backcolors As Color
        Dim backcolors2 As Color
        Dim backcolors3 As Color
        Try


            '   colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)

            'For i = 0 To colors.Count - 1
            '    colors_Ucase.Add(UCase(colors.Keys(i)), colors.Values(i))
            'Next

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

        For Each serie In Chart2.Series
            Dim box As New Label
            With box
                .Name = serie.Name

                Dim csilib As New CSI_Library.CSI_Library
                ''Dim loadingascon = csilib.GetLoadingAsCON()
                'Dim loadingascon = False 'csilib.GetLoadingAsCON()
                'If (csilib.CheckLic(2) = 3) Then
                '    Try
                '        Dim db_authPath As String = Nothing
                '        Dim directory As String = csilib.getRootPath()
                '        If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
                '            Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
                '                db_authPath = reader.ReadLine()
                '            End Using
                '        End If
                '        Dim connectionString As String
                '        connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

                '        loadingascon = csilib.GetLoadingAsCON(connectionString)
                '    Catch ex As Exception

                '    End Try
                'End If

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
                    '' box.BackColor = Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color
                    box.BackColor = System.Drawing.ColorTranslator.FromWin32(colors_Ucase(UCase(serie.Name).Replace(" ", "")))
                    box.BackColor = Color.FromArgb(alpha, box.BackColor.R, box.BackColor.G, box.BackColor.B)
                End If



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

                ''''''''''''''''''''''''''''''''''''



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
    Private Sub Chart2_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart2.Click
        If (e.Button.CompareTo(Forms.MouseButtons.Right) = 0) Then
            Chart2.ChartAreas(0).AxisX.ScaleView.ZoomReset()
        End If
    End Sub
    Private Sub Chart_Parts_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart_parts.Click
        If (e.Button.CompareTo(Forms.MouseButtons.Right) = 0) Then
            Chart_parts.ChartAreas(0).AxisX.ScaleView.ZoomReset()
            Chart1.ChartAreas(0).AxisX.ScaleView.ZoomReset()
            Chart1.Refresh()
            Chart_parts.Refresh()
        End If
    End Sub

    Private Sub Chart1_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart1.Click
        If (e.Button.CompareTo(Forms.MouseButtons.Right) = 0) Then
            Chart1.ChartAreas(0).AxisX.ScaleView.ZoomReset()
            Chart_parts.ChartAreas(0).AxisX.ScaleView.ZoomReset()
            Chart_parts.Refresh()
            Chart1.Refresh()
        End If
    End Sub


    'Dim mouse_down_pos As Decimal
    'Dim mouse_up_pos As Decimal
    'Dim size As Decimal
    'Private Sub Chart1_up(sender As Object, e As MouseEventArgs) Handles Chart1.MouseUp

    '    ' MsgBox("start:" & e.NewSelectionStart. & "  -  " & "end:" & e.NewSelectionEnd)
    '    'Chart_parts.ChartAreas(0).AxisX.ScaleView = Chart1.ChartAreas(0).AxisX.ScaleView
    '    'Chart_parts.Refresh()
    '    'Chart1.Refresh()
    '    'If (e.Button.CompareTo(Forms.MouseButtons.Left) = 0) Then

    '    '    Dim result As HitTestResult = Chart1.HitTest(e.X, e.Y)

    '    '    If (result.ChartElementType = ChartElementType.DataPoint) Then
    '    '        mouse_up_pos = (Chart1.Series(0).Points(result.PointIndex).XValue)
    '    '    End If
    '    '    size = mouse_up_pos - mouse_down_pos


    '    '    Chart1.ChartAreas(0).AxisX.ScaleView.Size = If(size > 0, size, -size)
    '    '    Chart1.ChartAreas(0).AxisX.ScaleView.Position = mouse_down_pos
    '    '    mouse_down_pos = 0
    '    '    mouse_up_pos = 0
    '    '    size = 0
    '    'End If
    'End Sub

    'Private Sub Chart1_down(sender As Object, e As MouseEventArgs) Handles Chart1.MouseDown

    '    If (e.Button.CompareTo(Forms.MouseButtons.Left) = 0) Then
    '        Dim result As HitTestResult = Chart1.HitTest(e.X, e.Y)

    '        If (result.ChartElementType = ChartElementType.DataPoint) Then
    '            mouse_down_pos = (Chart1.Series(0).Points(result.PointIndex).XValue)
    '        End If
    '    End If

    'End Sub

    Private Sub Chart1_ZoomChange(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart1.MouseUp
        RemoveHandler Chart_parts.MouseUp, AddressOf Chart_parts_ZoomChange

        If (e.Button.CompareTo(Forms.MouseButtons.Left) = 0) Then
            Chart_parts.ChartAreas(0).AxisX.ScaleView = Chart1.ChartAreas(0).AxisX.ScaleView
            Chart_parts.Refresh()
            Chart1.Refresh()
        Else
            Chart_parts.Refresh()
            Chart1.Refresh()
        End If


        AddHandler Chart_parts.MouseUp, AddressOf Chart_parts_ZoomChange
    End Sub

    Private Sub Chart_parts_ZoomChange(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart_parts.MouseUp

        RemoveHandler Chart1.MouseUp, AddressOf Chart1_ZoomChange

        If (e.Button.CompareTo(Forms.MouseButtons.Left) = 0) Then
            Chart1.ChartAreas(0).AxisX.ScaleView = Chart_parts.ChartAreas(0).AxisX.ScaleView
            Chart_parts.Refresh()
            Chart1.Refresh()
        Else
            Chart_parts.Refresh()
            Chart1.Refresh()
        End If

        AddHandler Chart1.MouseUp, AddressOf Chart1_ZoomChange

    End Sub



    Dim Locked As Boolean
    Private Sub CB_Lock_CheckedChanged(sender As Object, e As EventArgs)
        Locked = True
    End Sub

    Private Sub Disp_labels_CheckedChanged(sender As Object, e As EventArgs) Handles Disp_labels.CheckedChanged
        If Disp_labels.Checked = True Then
            Chart1.Series("Cycle").SmartLabelStyle.Enabled = True
        Else
            Chart1.Series("Cycle").SmartLabelStyle.Enabled = False
        End If
    End Sub

    Private Sub Chart_Parts_Click(sender As Object, e As EventArgs) Handles Chart_parts.Click

    End Sub

    Private Sub BTN_RAW_VALUES_Click(sender As Object, e As EventArgs) Handles BTN_RAW_VALUES.Click
        Dim raw_form As New form_raw_values




        raw_form.Text = "Raw data without processing, " + Me.Text
        raw_form.DGV_VALUES.DataSource = shift
        raw_form.Show()
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
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        original_height(1) = SplitContainer1.Height
        SuspendLayout()

        If SplitContainer1.Panel2Collapsed = True Then
            SplitContainer1.Panel2Collapsed = False
            Me.Height = Me.Height + 250

            Dim img As Image = My.Resources.d
            img.RotateFlip(RotateFlipType.Rotate180FlipNone)
            Button2.Image = img


        Else

            Dim img As Image = My.Resources.d
            img.RotateFlip(RotateFlipType.RotateNoneFlipNone)

            Button2.Image = img

            h = SplitContainer1.Panel2.Height
            If Me.Height > 288 Then Me.Height = Me.Height - 250
            SplitContainer1.Panel2Collapsed = True


        End If
        ResumeLayout()

    End Sub
#End Region



End Class
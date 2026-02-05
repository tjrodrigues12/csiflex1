Imports System.Data.SQLite
Imports System.Windows.Forms.DataVisualization.Charting
Imports CSI_Library.CSI_DATA
Imports CSI_Library.CSI_Library
Imports MathNet.Numerics

Public Class Report_parts

    Dim temporary_periode_returned As CSI_Library.CSI_DATA.periode
    Public CSI_LIB As New CSI_Library.CSI_Library(False)

    Private Sub BTN_Return_Click(sender As Object, e As EventArgs) Handles BTN_Return.Click
        Me.Close()
    End Sub

#Region " some public var"
    Public part_dataset As DataSet = New DataSet
    Public part_from_machine_tbl As DataSet = New DataSet

    Public median_short_c1 As Integer
    Public median_long_c1 As Integer
    Public median_good_c1 As Integer

    Public median_short_c2 As Integer
    Public median_long_c2 As Integer
    Public median_good_c2 As Integer

    Public median_short_c3 As Integer
    Public median_long_c3 As Integer
    Public median_good_c3 As Integer

    Public max_time_activated As Boolean = False

    Public total_cycle_1 As Integer = 0, good_C_1 As Integer = 0, short_c_1 As Integer = 0, long_c_1 As Integer = 0, total_c_1 As Integer
    Public avg_time_1 As TimeSpan, min_time_1 As TimeSpan = New TimeSpan(0, 0, 0, 0, 1), max_time_1 As TimeSpan

    Public total_cycle_2 As Integer = 0, good_C_2 As Integer = 0, short_c_2 As Integer = 0, long_c_2 As Integer = 0, total_c_2 As Integer
    Public avg_time_2 As TimeSpan, min_time_2 As TimeSpan = New TimeSpan(0, 0, 0, 0, 1), max_time_2 As TimeSpan

    Public total_cycle_3 As Integer = 0, good_C_3 As Integer = 0, short_c_3 As Integer = 0, long_c_3 As Integer = 0, total_c_3 As Integer
    Public avg_time_3 As TimeSpan, min_time_3 As TimeSpan = New TimeSpan(0, 0, 0, 0, 1), max_time_3 As TimeSpan

    Public One_milisec As TimeSpan = New TimeSpan(0, 0, 0, 0, 1), jesus_birth As Date = Now.AddYears(-10)

    Public operator_ As String = ""
    Public date_ As String = ""
    Public shift_ As String = ""

    Public partbegin1 As Date, partend1 As Date
    Public partbegin2 As Date, partend2 As Date
    Public partbegin3 As Date, partend3 As Date


    Public lbl_NO_PART_visible As Boolean = False
    Public lbl_NO_PART2_visible As Boolean = False
    Public lbl_NO_PART3_visible As Boolean = False

    Public total_part1 As Integer = 0, total_part2 As Integer = 0, total_part3 As Integer = 0
    Public bad_part1 As Integer = 0, bad_part2 As Integer = 0, bad_part3 As Integer = 0

    Public min_th As String, max_th As String

    Public Selected_parts_date As Date
    Public selected_part As String
    Public min_max_type As String = ""
    ' "" =not set
    ' _MINMAX
    ' _MIN_ONLY
    ' _CYCOUNT
    ' _CYCRATE


#End Region

    Private Sub Report_parts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BTN_Return.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone)
        Height = Config_report.Height
        Me.MdiParent = Reporting_application
        Me.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Size.Width, 25)
        Me.BackColor = Color.FromArgb(237, 239, 240)

        CSI_LIB.connectionString = CSIFLEXSettings.Instance.ConnectionString

        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        Selected_parts_date = (Config_report.Selected_parts_date)
        date_ = Config_report.Selected_parts_date.ToString("MMMM dd, yyyy")
        selected_part = Config_report.Selected_parts
        part_dataset = CSI_LIB.Get_parts_details(selected_part, Config_report.DTP_StartDate.Value, Config_report.DTP_EndDate.Value, Config_report.Selected_machine_partrep, Welcome.CSIF_version)

        BW_machine_perf.RunWorkerAsync()

        partbegin1 = jesus_birth

        BW_process_shift1.RunWorkerAsync()
        BW_Process_shift2.RunWorkerAsync()
        BW_Process_shift3.RunWorkerAsync()

        If Not part_dataset.Tables("operator") Is Nothing Then
            Dim tmp_view As DataView = New DataView(part_dataset.Tables("operator"))
            tmp_view.RowFilter = "trx_date = #" & (Selected_parts_date.ToShortDateString) & "#"
            If tmp_view.Count <> 0 Then
                operator_ = tmp_view(0)("OPERATOR")
            Else
                operator_ = "No operator recorded"
            End If
        Else
            operator_ = "No data for the operators"
        End If


        Dim Totalpart As Integer = 0
        Dim badpart As Integer = 0


        If Not part_dataset.Tables("Reportedparts") Is Nothing Then
            If part_dataset.Tables("Reportedparts").Rows.Count <> 0 Then

                'shift1:
                Dim tmp_view As DataView = New DataView(part_dataset.Tables("Reportedparts"))
                Dim filter_ As String = " trx_time >= #" & partbegin1 & "# AND trx_time <= #" & partend1 & "#"
                Dim tmp_datarows() As DataRow = part_dataset.Tables("Reportedparts").Select(filter_)
                If tmp_datarows.Count > 0 Then
                    total_part1 = tmp_datarows(tmp_datarows.Count - 1).Item("total_parts")
                    bad_part1 = tmp_datarows(tmp_datarows.Count - 1).Item("bad_parts")
                    Chart_number1.Series(0).Points.Add(100 - bad_part1 * 100 / total_part1)
                    Chart_number1.Series(0).Points(Chart_number1.Series(0).Points.Count - 1).LegendText = "Good part: " & (total_part1 - bad_part1).ToString()
                    Chart_number1.Series(0).Points.Add(bad_part1 * 100 / total_part1)
                    Chart_number1.Series(0).Points(Chart_number1.Series(0).Points.Count - 1).LegendText = "Bad part: " & bad_part1
                    Chart_number1.Legends(0).Title = "total part: " & total_part1
                End If


                'shift2:
                tmp_view = New DataView(part_dataset.Tables("Reportedparts"))
                filter_ = " trx_time >= #" & partbegin2 & "# AND trx_time <= #" & partend2 & "#"
                tmp_datarows = part_dataset.Tables("Reportedparts").Select(filter_)
                If tmp_datarows.Count > 0 Then
                    total_part2 = tmp_datarows(tmp_datarows.Count - 1).Item("total_parts")
                    bad_part2 = tmp_datarows(tmp_datarows.Count - 1).Item("bad_parts")
                    Chart_number2.Series(0).Points.Add(100 - bad_part2 * 100 / total_part2)
                    Chart_number2.Series(0).Points(Chart_number2.Series(0).Points.Count - 1).LegendText = "Good part: " & (total_part2 - bad_part2).ToString()
                    Chart_number2.Series(0).Points.Add(bad_part2 * 100 / total_part2)
                    Chart_number2.Series(0).Points(Chart_number2.Series(0).Points.Count - 1).LegendText = "Bad part: " & bad_part2
                    Chart_number2.Legends(0).Title = "total part: " & total_part2
                End If


                'shift3:
                tmp_view = New DataView(part_dataset.Tables("Reportedparts"))
                filter_ = " trx_time >= #" & partbegin3 & "# AND trx_time <= #" & partend3 & "#"
                tmp_datarows = part_dataset.Tables("Reportedparts").Select(filter_)
                If tmp_datarows.Count > 0 Then
                    total_part3 = tmp_datarows(tmp_datarows.Count - 1).Item("total_parts")
                    bad_part3 = tmp_datarows(tmp_datarows.Count - 1).Item("bad_parts")
                    Chart_number3.Series(0).Points.Add(100 - bad_part3 * 100 / total_part3)
                    Chart_number3.Series(0).Points(Chart_number3.Series(0).Points.Count - 1).LegendText = "Good part: " & (total_part3 - bad_part3).ToString()
                    Chart_number3.Series(0).Points.Add(bad_part3 * 100 / total_part3)
                    Chart_number3.Series(0).Points(Chart_number3.Series(0).Points.Count - 1).LegendText = "Bad part: " & bad_part3
                    Chart_number3.Legends(0).Title = "total part: " & total_part3
                End If


            Else
                nodata1.Visible = True
                Chart_number1.Series(0).Points.Add(100)
                Chart_number1.Series(0).Points(0).LegendText = "No data"
                Chart_number1.Series(0).Points(0).Color = Color.Red
                nodata2.Visible = True
                Chart_number2.Series(0).Points.Add(100)
                Chart_number2.Series(0).Points(0).LegendText = "No data"
                Chart_number2.Series(0).Points(0).Color = Color.Red
                nodata3.Visible = True
                Chart_number3.Series(0).Points.Add(100)
                Chart_number3.Series(0).Points(0).LegendText = "No data"
                Chart_number3.Series(0).Points(0).Color = Color.Red
            End If

        Else
            nodata1.Visible = True
            Chart_number1.Series(0).Points.Add(100)
            Chart_number1.Series(0).Points(0).LegendText = "No data"
            Chart_number1.Series(0).Points(0).Color = Color.Red
            nodata2.Visible = True
            Chart_number2.Series(0).Points.Add(100)
            Chart_number2.Series(0).Points(0).LegendText = "No data"
            Chart_number2.Series(0).Points(0).Color = Color.Red
            nodata3.Visible = True
            Chart_number3.Series(0).Points.Add(100)
            Chart_number3.Series(0).Points(0).LegendText = "No data"
            Chart_number3.Series(0).Points(0).Color = Color.Red
        End If




        LBL_Date.Text = date_
        LBL_operator.Text = "Operator: " & operator_
        LBL_partName.Text = Config_report.Selected_parts
        lbl_machine.Text = If((part_dataset.Tables("partnumbers").Rows.Count <> 0), "Machine: " & part_dataset.Tables("partnumbers").Rows(0).Item("machine"), "")
        Chart_Gauge.Invalidate()

        Me.Visible = True

    End Sub

    Private Sub show_chart_gauge_1(avg_time As TimeSpan, min_time As TimeSpan, short_c As Integer, good_C As Integer, long_c As Integer, total_c As Integer)
        Dim min_percent As Integer = 0
        Chart_Gauge.Series(0).Points.Clear()

        If total_c <> 0 Then min_percent = (min_time.TotalSeconds * 100 / total_c)
        Chart_Gauge.Series(0).Points.AddXY("", min_percent)
        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).Color = Color.Gray
        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).ToolTip = "Min cycle time = " & min_time.ToString("hh\:mm\:ss")
        Dim new_green_part As Integer = 12.5 - min_percent
        If new_green_part > 0 Then
            Chart_Gauge.Series(0).Points.AddXY("", (12.5 - min_percent))
        Else
            Chart_Gauge.Series(0).Points.AddXY("", (0))
        End If

        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).Color = Color.Yellow
        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).Label = short_c '"min: " & min_time.ToString()
        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).ToolTip = "Median cycle time =" & TimeSpan.FromSeconds(median_short_c1).ToString("hh\:mm\:ss") & ", " & short_c & " parts made"
        Dim green_percent As Integer = 25
        Dim yellow_percent As Integer = 12.5
        If min_max_type = "_MIN_ONLY" Then
            green_percent = 40.5
            yellow_percent = 0
        End If
        If new_green_part < 0 Then green_percent = green_percent + 12.5 + new_green_part
        Chart_Gauge.Series(0).Points.AddXY("", green_percent)
        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).Color = Color.GreenYellow
        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).Label = good_C '"Avg: " & avg_time.ToString()
        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).ToolTip = "Average cycle time =" & avg_time.ToString("hh\:mm\:ss") & ", " & good_C & " parts made"

        Chart_Gauge.Series(0).Points.AddXY("", yellow_percent)
        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).Color = Color.Yellow
        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).Label = If(yellow_percent = 0, "", long_c) 'long_c '"Max: " & max_time.ToString()

        Chart_Gauge.Series(0).Points.AddXY("", 50)
        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).Color = Color.Transparent
        Chart_Gauge.Series(0).Points(Chart_Gauge.Series(0).Points.Count - 1).LegendText = ""
    End Sub
    Private Sub show_chart_gauge_2(avg_time As TimeSpan, min_time As TimeSpan, short_c As Integer, good_C As Integer, long_c As Integer, total_c As Integer)
        chart_gauge2.Series(0).Points.Clear()
        Dim min_percent As Integer = 0
        If total_c <> 0 Then min_percent = (min_time.TotalSeconds * 100 / total_c)

        chart_gauge2.Series(0).Points.AddXY("", min_percent)
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).Color = Color.Gray
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).ToolTip = "Min cycle time = " & min_time.ToString("hh\:mm\:ss")

        chart_gauge2.Series(0).Points.AddXY("", (12.5 - min_percent))
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).Color = Color.Yellow
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).Label = short_c '"min: " & min_time.ToString()
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).ToolTip = "Median cycle time =" & TimeSpan.FromSeconds(median_short_c1).ToString("hh\:mm\:ss") & ", " & short_c & " parts made"
        Dim green_percent As Integer = 25
        Dim yellow_percent As Integer = 12.5
        If min_max_type = "_MIN_ONLY" Then
            green_percent = 40.5
            yellow_percent = 0
        End If

        chart_gauge2.Series(0).Points.AddXY("", green_percent)
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).Color = Color.GreenYellow
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).Label = good_C '"Avg: " & avg_time.ToString()
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).ToolTip = "Average cycle time =" & avg_time.ToString("hh\:mm\:ss") & ", " & good_C & " parts made"

        chart_gauge2.Series(0).Points.AddXY("", yellow_percent)
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).Color = Color.Yellow
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).Label = If(yellow_percent = 0, "", long_c) 'long_c '"Max: " & max_time.ToString()

        chart_gauge2.Series(0).Points.AddXY("", 50)
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).Color = Color.Transparent
        chart_gauge2.Series(0).Points(chart_gauge2.Series(0).Points.Count - 1).LegendText = ""
    End Sub
    Private Sub show_chart_gauge_3(avg_time As TimeSpan, min_time As TimeSpan, short_c As Integer, good_C As Integer, long_c As Integer, total_c As Integer)
        Dim min_percent As Integer = 0
        chart_gauge3.Series(0).Points.Clear()
        If total_c <> 0 Then min_percent = (min_time.TotalSeconds * 100 / total_c)

        chart_gauge3.Series(0).Points.AddXY("", min_percent)
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).Color = Color.Gray
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).ToolTip = "Min cycle time = " & min_time.ToString("hh\:mm\:ss")

        chart_gauge3.Series(0).Points.AddXY("", (12.5 - min_percent))
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).Color = Color.Yellow
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).Label = short_c '"min: " & min_time.ToString()
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).ToolTip = "Median cycle time =" & TimeSpan.FromSeconds(median_short_c1).ToString("hh\:mm\:ss") & ", " & short_c & " parts made"

        Dim green_percent As Integer = 25
        Dim yellow_percent As Integer = 12.5
        If min_max_type = "_MIN_ONLY" Then
            green_percent = 40.5
            yellow_percent = 0
        End If


        chart_gauge3.Series(0).Points.AddXY("", green_percent)
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).Color = Color.GreenYellow
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).Label = good_C '"Avg: " & avg_time.ToString()
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).ToolTip = "Average cycle time =" & avg_time.ToString("hh\:mm\:ss") & ", " & good_C & " parts made"

        chart_gauge3.Series(0).Points.AddXY("", yellow_percent)
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).Color = Color.Yellow
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).Label = If(yellow_percent = 0, "", long_c) 'long_c '"Max: " & max_time.ToString()

        chart_gauge3.Series(0).Points.AddXY("", 50)
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).Color = Color.Transparent
        chart_gauge3.Series(0).Points(chart_gauge3.Series(0).Points.Count - 1).LegendText = ""
    End Sub

#Region "get part info "
    Public angle As Integer
    Public min1, max1 As Integer ' min max (real), shift 1
    Public lbl_min_th_text As String = ""
    Public lbl_max_th_text As String = ""
    Public PictureBox_needle_image As Bitmap

    Private Sub getpartinfo_1(shift As Integer, avg_time As TimeSpan, good_C As Integer, short_c As Integer, long_c As Integer, total_c As Integer)

        Dim mch As DataView = New DataView(part_dataset.Tables("tbl_mch"))
        Dim mchfilter_ As String = "shift = 1 AND Date_ >= #" & Selected_parts_date & "# AND Date_ <= #" & Selected_parts_date.Add(TimeSpan.FromSeconds(86399)) & "#"
        Dim mch_datarows() As DataRow = part_dataset.Tables("tbl_mch").Select(mchfilter_)
        Dim infos() As String = mch(0).Item("partnumber").ToString().Split(";")
        Dim iSpan As TimeSpan = TimeSpan.FromSeconds(infos(1))

        Dim type() As String = part_dataset.Tables("tbl_mch").Rows(0).Item("partnumber").split(";")
        min_max_type = type(type.Count - 2)

        lbl_min_th_text = "Normal min" & vbCrLf & iSpan.ToString("hh\:mm\:ss")
        iSpan = TimeSpan.FromSeconds(infos(2))
        lbl_max_th_text = "Normal max" & vbCrLf & iSpan.ToString("hh\:mm\:ss")


        If infos(1) <> 0 Then
            Dim avg As Integer = avg_time.TotalSeconds * 12.5 / infos(1)
            angle = avg * 45 / 12.5
        Else
            angle = 0
        End If

        Dim bmp As New Bitmap(PictureBox_needle.Image)
        bmp = DrawRotateImage(bmp, angle)
        PictureBox_needle_image = bmp

        'mchfilter_ = "STATUS ='Cycle ON' AND shift = 1 AND DATE_ >= #" & Config_report.Selected_parts_date & "# AND DATE_ <= #" & Config_report.Selected_parts_date.Add(TimeSpan.FromSeconds(86399)) & "#"

        ' Dim CON_datarows As DataRow() = part_dataset.Tables("tbl_mch").Select(mchfilter_)

        Dim avg_short_Shift1 As Integer = 0
        Dim avg_long_Shift1 As Integer = 0
        Dim CON_datarows As DataRow()

        If short_c <> 0 Then
            avg_short_Shift1 = part_dataset.Tables("tbl_mch").Compute("avg(cycletime)", "status Like 'CYCLE ON' and cycletime <='" & infos(1) & "'")
            CON_datarows = part_dataset.Tables("tbl_mch").Select("status = 'CYCLE ON' AND cycletime<='" & infos(1) & "'", "cycletime")
            median_short_c1 = CON_datarows((CON_datarows.Count - 1) / 2).Item("cycletime")
        End If
        If long_c <> 0 Then
            avg_long_Shift1 = part_dataset.Tables("tbl_mch").Compute("avg(cycletime)", "status Like 'CYCLE ON' and cycletime >='" & infos(2) & "'")
            CON_datarows = part_dataset.Tables("tbl_mch").Select("status = 'CYCLE ON' AND cycletime >='" & infos(2) & "'", "cycletime")
            median_long_c1 = CON_datarows((CON_datarows.Count - 1) / 2).Item("cycletime")
        End If

        short_avg_angle = (avg_short_Shift1 * 100 / total_c) * 45 / 12.5
        long_avg_angle = (avg_long_Shift1 * 100 / total_c) * 45 / 12.5
    End Sub

    Public lbl_min_th2_text As String = ""
    Public lbl_max_th2_text As String = ""
    Public PictureBox2_needle_image As Bitmap
    Private Sub getpartinfo_2(avg_time As TimeSpan, good_C As Integer, short_c As Integer, long_c As Integer, total_c As Integer)

        Dim mch As DataView = New DataView(part_dataset.Tables("tbl_mch"))
        Dim mchfilter_ As String = "shift = 2 AND Date_ >= #" & Selected_parts_date & "# AND Date_ <= #" & Selected_parts_date.Add(TimeSpan.FromSeconds(86399)) & "#"
        Dim mch_datarows() As DataRow = part_dataset.Tables("tbl_mch").Select(mchfilter_)
        Dim infos() As String = mch(0).Item("partnumber").ToString().Split(";")
        Dim iSpan As TimeSpan = TimeSpan.FromSeconds(infos(1))

        Dim type() As String = part_dataset.Tables("tbl_mch").Rows(0).Item("partnumber").split(";")
        min_max_type = type(type.Count - 2)

        lbl_min_th2_text = "Normal min" & vbCrLf & iSpan.ToString("hh\:mm\:ss")
        iSpan = TimeSpan.FromSeconds(infos(2))
        lbl_max_th2_text = "Normal max" & vbCrLf & iSpan.ToString("hh\:mm\:ss")


        If infos(1) <> 0 Then
            Dim avg As Integer = avg_time.TotalSeconds * 12.5 / infos(1)
            angle = avg * 45 / 12.5
        Else
            angle = 0
        End If

        Dim bmp As New Bitmap(PictureBox2_needle.Image)
        bmp = DrawRotateImage(bmp, angle)
        PictureBox2_needle_image = bmp

        'mchfilter_ = "STATUS ='Cycle ON' AND shift = 1 AND DATE_ >= #" & Config_report.Selected_parts_date & "# AND DATE_ <= #" & Config_report.Selected_parts_date.Add(TimeSpan.FromSeconds(86399)) & "#"

        ' Dim CON_datarows As DataRow() = part_dataset.Tables("tbl_mch").Select(mchfilter_)

        Dim avg_short_Shift2 As Integer = 0
        Dim avg_long_Shift2 As Integer = 0
        Dim CON_datarows As DataRow()

        If short_c <> 0 Then
            avg_short_Shift2 = part_dataset.Tables("tbl_mch").Compute("avg(cycletime)", "status Like 'CYCLE ON' and cycletime <='" & infos(1) & "'")
            CON_datarows = part_dataset.Tables("tbl_mch").Select("status = 'CYCLE ON' AND cycletime<='" & infos(1) & "'", "cycletime")
            median_short_c2 = CON_datarows((CON_datarows.Count - 1) / 2).Item("cycletime")
        End If
        If long_c <> 0 Then
            avg_long_Shift2 = part_dataset.Tables("tbl_mch").Compute("avg(cycletime)", "status Like 'CYCLE ON' and cycletime >='" & infos(2) & "'")
            CON_datarows = part_dataset.Tables("tbl_mch").Select("status = 'CYCLE ON' AND cycletime >='" & infos(2) & "'", "cycletime")
            median_long_c2 = CON_datarows((CON_datarows.Count - 1) / 2).Item("cycletime")
        End If

        short_avg_angle2 = (avg_short_Shift2 * 100 / total_c) * 45 / 12.5
        long_avg_angle2 = (avg_long_Shift2 * 100 / total_c) * 45 / 12.5
    End Sub

    Public lbl_min_th3_text As String = ""
    Public lbl_max_th3_text As String = ""
    Public PictureBox3_needle_image As Bitmap
    Private Sub getpartinfo_3(avg_time As TimeSpan, good_C As Integer, short_c As Integer, long_c As Integer, total_c As Integer)

        Dim mch As DataView = New DataView(part_dataset.Tables("tbl_mch"))
        Dim mchfilter_ As String = "shift = 3 AND Date_ >= #" & Selected_parts_date & "# AND Date_ <= #" & Selected_parts_date.Add(TimeSpan.FromSeconds(86399)) & "#"
        Dim mch_datarows() As DataRow = part_dataset.Tables("tbl_mch").Select(mchfilter_)
        Dim infos() As String = mch(0).Item("partnumber").ToString().Split(";")
        Dim iSpan As TimeSpan = TimeSpan.FromSeconds(infos(1))

        Dim type() As String = part_dataset.Tables("tbl_mch").Rows(0).Item("partnumber").split(";")
        min_max_type = type(type.Count - 2)

        lbl_min_th3_text = "Normal min" & vbCrLf & iSpan.ToString("hh\:mm\:ss")
        iSpan = TimeSpan.FromSeconds(infos(2))
        lbl_max_th3_text = "Normal max" & vbCrLf & iSpan.ToString("hh\:mm\:ss")


        If infos(1) <> 0 Then
            Dim avg As Integer = avg_time.TotalSeconds * 12.5 / infos(1)
            angle = avg * 45 / 12.5
        Else
            angle = 0
        End If

        Dim bmp As New Bitmap(PictureBox3_needle.Image)
        bmp = DrawRotateImage(bmp, angle)
        PictureBox3_needle_image = bmp

        'mchfilter_ = "STATUS ='Cycle ON' AND shift = 1 AND DATE_ >= #" & Config_report.Selected_parts_date & "# AND DATE_ <= #" & Config_report.Selected_parts_date.Add(TimeSpan.FromSeconds(86399)) & "#"

        ' Dim CON_datarows As DataRow() = part_dataset.Tables("tbl_mch").Select(mchfilter_)

        Dim avg_short_Shift3 As Integer = 0
        Dim avg_long_Shift3 As Integer = 0
        Dim CON_datarows As DataRow()

        If short_c <> 0 Then
            avg_short_Shift3 = part_dataset.Tables("tbl_mch").Compute("avg(cycletime)", "status Like 'CYCLE ON' and cycletime <='" & infos(1) & "'")
            CON_datarows = part_dataset.Tables("tbl_mch").Select("status = 'CYCLE ON' AND cycletime<='" & infos(1) & "'", "cycletime")
            median_short_c3 = CON_datarows((CON_datarows.Count - 1) / 2).Item("cycletime")
        End If
        If long_c <> 0 Then
            avg_long_Shift3 = part_dataset.Tables("tbl_mch").Compute("avg(cycletime)", "status Like 'CYCLE ON' and cycletime >='" & infos(2) & "'")
            CON_datarows = part_dataset.Tables("tbl_mch").Select("status = 'CYCLE ON' AND cycletime >='" & infos(2) & "'", "cycletime")
            median_long_c3 = CON_datarows((CON_datarows.Count - 1) / 2).Item("cycletime")
        End If

        short_avg_angle3 = (avg_short_Shift3 * 100 / total_c) * 45 / 12.5
        long_avg_angle3 = (avg_long_Shift3 * 100 / total_c) * 45 / 12.5
    End Sub

#End Region


    Private Sub Chart_cycle_number1_Click(sender As Object, e As EventArgs)


        If Not part_dataset.Tables("partnumbers") Is Nothing Then
            Dim tmp_view As DataView = New DataView(part_dataset.Tables("partnumbers"))

            Dim filter_ As String = "end_time >= #" & Selected_parts_date & "# AND end_time <= #" & Selected_parts_date.Add(TimeSpan.FromSeconds(86399)) & "#"
            Dim tmp_datarows() As DataRow = part_dataset.Tables("partnumbers").Select(filter_)
            If tmp_datarows.Count <> 0 Then

                Chart3.Series("good").Points.Clear()
                Chart3.Series("range").Points.Clear()

                Dim i As Integer = 0
                For Each row As DataRow In tmp_datarows
                    Chart3.Series("good").Points.AddXY(i, row.Item("good_cycle").ToString())
                    Chart3.Series("range").Points.AddXY(i, row.Item("long_cycle").ToString(), row.Item("short_cycle").ToString())
                    i += 1
                Next
            End If
        End If
    End Sub



#Region " Graphics"

    Public short_avg_angle As Decimal = 0
    Public long_avg_angle As Decimal = 0

    Public short_avg_angle2 As Decimal = 0
    Public long_avg_angle2 As Decimal = 0

    Public short_avg_angle3 As Decimal = 0

    Private Sub lbl_max_th_Click(sender As Object, e As EventArgs) Handles lbl_max_th.Click
        If lbl_NO_PART.Visible = False Then ActivatedMaxTime.ShowDialog()
    End Sub

    Private Sub Close_details_Click(sender As Object, e As EventArgs) Handles BT_Close_parts.Click
        Close()

    End Sub

    Private Sub PB_warning_Click(sender As Object, e As EventArgs) Handles PB_warning.Click
        ActivatedMaxTime.ShowDialog()
    End Sub

    Public long_avg_angle3 As Decimal = 0

    Public tt1 As ToolTip = New ToolTip()

    Private Sub Panel_title_Paint(sender As Object, e As PaintEventArgs) Handles Panel_title.Paint
        Using p As New Pen(Color.FromArgb(217, 217, 217), 2.0)
            e.Graphics.DrawRectangle(p, 0, 0, Panel_title.Width, Panel_title.Height)
        End Using
    End Sub

    Private Sub Panel_OnedayInfos_Paint(sender As Object, e As PaintEventArgs) Handles Panel_OnedayInfos.Paint
        Using p As New Pen(Color.FromArgb(217, 217, 217), 2.0)
            e.Graphics.DrawRectangle(p, 0, 0, Panel_OnedayInfos.Width, Panel_OnedayInfos.Height)
        End Using
    End Sub

    'Private Sub Panel_additionalinfo_Paint(sender As Object, e As PaintEventArgs) Handles Panel_additionalinfo.Paint
    '    Using p As New Pen(Color.FromArgb(227, 227, 227), 2.0)
    '        e.Graphics.DrawRectangle(p, 0, 0, Panel_additionalinfo.Width, Panel_additionalinfo.Height)
    '    End Using
    'End Sub


    Private Sub LBL_AvgTime_Click(sender As Object, e As EventArgs) Handles LBL_AvgTime.Click
        TB_change_avg.Visible = Not (TB_change_avg.Visible)
    End Sub
    Private Sub LBL_AvgTime2_Click(sender As Object, e As EventArgs) Handles lbl_avgtime2.Click
        TB_change_avg2.Visible = Not (TB_change_avg2.Visible)
    End Sub
    Private Sub LBL_AvgTime3_Click(sender As Object, e As EventArgs) Handles lbl_avgtime3.Click
        TB_change_avg3.Visible = Not (TB_change_avg3.Visible)
    End Sub


    Private Function rot(p As PointF, theta As Decimal, X_position As Integer, Y_position As Integer) As Point
        Dim p_dash As New Point
        p_dash.X = p.X * Math.Cos(theta) + p.Y * Math.Sin(theta)
        p_dash.Y = -p.X * Math.Sin(theta) + p.Y * Math.Cos(theta)
        p_dash.X = p_dash.X + X_position
        p_dash.Y = p_dash.Y + Y_position
        Return p_dash
    End Function

    Dim gp1 As Drawing2D.GraphicsPath
    Dim gp2 As Drawing2D.GraphicsPath

    Private Sub undock_Click(sender As Object, e As EventArgs) Handles undock.Click
        If Me.MdiParent Is Nothing Then
            Me.MdiParent = Reporting_application
            BT_Close_parts.Visible = True
            Me.FormBorderStyle = FormBorderStyle.None
            undock.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone)
        Else
            Me.MdiParent = Nothing
            BT_Close_parts.Visible = False

            Me.FormBorderStyle = FormBorderStyle.FixedSingle
            undock.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone)
            Me.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Size.Width, 25)
        End If

    End Sub
    Private Function DrawRotateImage(b As Bitmap, angle As Single) As Bitmap
        Dim returnBitmap As New Bitmap(b.Width, b.Height) 'new empty bitmap to hold rotated image
        Dim g As Graphics = Graphics.FromImage(returnBitmap) 'make a graphics object from the empty bitmap

        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

        g.TranslateTransform(CSng(b.Width) / 2, CSng(b.Height) / 2) 'move rotation point to center of image
        g.RotateTransform(angle) 'rotate
        g.TranslateTransform(-CSng(b.Width) / 2, -CSng(b.Height) / 2) 'move image back
        g.DrawImage(b, New Point(0, 0)) 'draw passed in image onto graphics object

        Return returnBitmap
    End Function

#End Region



#Region "thread shit 1"
    Private Sub BW_process_shift1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW_process_shift1.DoWork
        Dim shift_list As New List(Of Integer)

        'shift1
        If Not part_dataset.Tables("partnumbers") Is Nothing Then
            Dim tmp_view As DataView = New DataView(part_dataset.Tables("partnumbers"))

            Dim filter_ As String = "shift = 1 AND end_time >= #" & Selected_parts_date & "# AND end_time <= #" & Selected_parts_date.Add(TimeSpan.FromSeconds(86399)) & "#"
            Dim tmp_datarows() As DataRow = part_dataset.Tables("partnumbers").Select(filter_)
            If tmp_datarows.Count <> 0 Then
                shift_ = "1"
                For Each item As DataRow In tmp_datarows

                    total_cycle_1 = total_cycle_1 + item.Item("total_cycle")
                    good_C_1 = good_C_1 + item.Item("good_cycle")
                    short_c_1 = short_c_1 + item.Item("short_cycle")
                    long_c_1 = long_c_1 + item.Item("long_cycle")

                    If DateTime.Compare(item.Item("start_time"), partbegin1) < 0 Or partbegin1 = jesus_birth Then partbegin1 = item.Item("start_time")
                    If DateTime.Compare(item.Item("end_time"), partend1) > 0 Then partend1 = item.Item("end_time")

                    If TimeSpan.Compare(TimeSpan.Parse(item.Item("max_cycle_time").ToString()), max_time_1) > 0 Then max_time_1 = TimeSpan.Parse(item.Item("max_cycle_time"))
                    If min_time_1 = One_milisec Or TimeSpan.Compare(TimeSpan.Parse(item.Item("min_cycle_time")), min_time_1) < 0 Then min_time_1 = TimeSpan.Parse(item.Item("min_cycle_time"))
                    avg_time_1 = avg_time_1 + TimeSpan.Parse(item.Item("avg_good_cycle_time"))

                Next
            End If

            total_c_1 = (avg_time_1.TotalSeconds + min_time_1.TotalSeconds + max_time_1.TotalSeconds)

            If total_c_1 <> 0 Then
                getpartinfo_1(1, avg_time_1, good_C_1, short_c_1, long_c_1, total_c_1)
            Else
                '  get_data_from_mch()
                get_min_max_th()

                Dim bmp As New Bitmap(PictureBox_needle.Image)
                bmp = DrawRotateImage(bmp, 0)
                PictureBox_needle_image = bmp
                lbl_NO_PART_visible = True

            End If

        Else
            shift_ = "-"
        End If
    End Sub
    Private Sub BW_process_shift1_RunWorkerCompleted(ByVal sender As System.Object,
                             ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW_process_shift1.RunWorkerCompleted

        ' If total_c_1 <> 0 Then
        lbl_min_th.Text = lbl_min_th_text
        lbl_max_th.Text = If(min_max_type = "_MIN_ONLY", "No max Cycle time for this part", lbl_max_th_text)
        If (min_max_type = "_MIN_ONLY") Then PB_warning.Visible = True
        'Else
        '    lbl_min_th.Text = ""
        '    lbl_max_th.Text = ""
        'End If

        PictureBox_needle.Image = PictureBox_needle_image

        show_chart_gauge_1(avg_time_1, min_time_1, short_c_1, good_C_1, long_c_1, total_c_1)


        total_cycle_1 = 0
        good_C_1 = 0
        short_c_1 = 0
        long_c_1 = 0

        LBL_AvgTime.Text = "Average time " & vbCrLf & avg_time_1.ToString("hh\:mm\:ss")
        LBL_min.Text = "Min time " & vbCrLf & min_time_1.ToString("hh\:mm\:ss")
        LBL_max.Text = "Max time " & vbCrLf & max_time_1.ToString("hh\:mm\:ss")


        If lbl_NO_PART_visible = True Then lbl_NO_PART.Visible = True

        max_time_1 = Nothing
        min_time_1 = Nothing
        avg_time_1 = Nothing

    End Sub

#End Region

#Region "thread shit 2"
    Private Sub BW_Process_shift2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW_Process_shift2.DoWork
        'shift2
        If Not part_dataset.Tables("partnumbers") Is Nothing Then
            Dim tmp_view As DataView = New DataView(part_dataset.Tables("partnumbers"))
            Dim filter_ As String = "shift = 2 AND end_time >= #" & Selected_parts_date & "# AND end_time <= #" & Selected_parts_date.Add(TimeSpan.FromSeconds(86399)) & "#"
            Dim tmp_datarows() As DataRow = part_dataset.Tables("partnumbers").Select(filter_)
            If tmp_datarows.Count <> 0 Then
                shift_ = "2"
                For Each item As DataRow In tmp_datarows
                    total_cycle_2 = total_cycle_2 + item.Item("total_cycle")
                    good_C_2 = good_C_2 + item.Item("good_cycle")
                    short_c_2 = short_c_2 + item.Item("short_cycle")
                    long_c_2 = long_c_2 + item.Item("long_cycle")

                    If DateTime.Compare(item.Item("start_time"), partbegin2) < 0 Or partbegin2 = jesus_birth Then partbegin2 = item.Item("start_time")
                    If DateTime.Compare(item.Item("end_time"), partend2) > 0 Then partend2 = item.Item("end_time")

                    If TimeSpan.Compare(TimeSpan.Parse(item.Item("max_cycle_time").ToString()), max_time_2) > 0 Then max_time_2 = TimeSpan.Parse(item.Item("max_cycle_time"))
                    If min_time_2 = One_milisec Or TimeSpan.Compare(TimeSpan.Parse(item.Item("min_cycle_time")), min_time_2) < 0 Then min_time_2 = TimeSpan.Parse(item.Item("min_cycle_time"))
                    avg_time_2 = avg_time_2 + TimeSpan.Parse(item.Item("avg_good_cycle_time"))
                Next
            End If
            total_c_2 = (avg_time_2.TotalSeconds + min_time_2.TotalSeconds + max_time_2.TotalSeconds)

            If total_c_2 <> 0 Then
                getpartinfo_2(avg_time_2, good_C_2, short_c_2, long_c_2, total_c_2)
            Else
                Dim bmp As New Bitmap(PictureBox2_needle.Image)
                bmp = DrawRotateImage(bmp, 0)
                PictureBox2_needle_image = bmp
                lbl_NO_PART2_visible = True
            End If




        Else
            shift_ = "-"


        End If
    End Sub
    Private Sub BW_process_shift2_RunWorkerCompleted(ByVal sender As System.Object,
                             ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW_Process_shift2.RunWorkerCompleted


        If total_c_2 <> 0 Then
            lbl_min_th2.Text = lbl_min_th2_text
            lbl_max_th2.Text = If(min_max_type = "_MIN_ONLY", "No max Cycle time for this part", lbl_max_th2_text)
            PB_warning.Visible = True
        Else
            lbl_min_th2.Text = ""
            lbl_max_th2.Text = ""
        End If

        PictureBox2_needle.Image = PictureBox2_needle_image

        show_chart_gauge_2(avg_time_2, min_time_2, short_c_2, good_C_2, long_c_2, total_c_2)


        total_cycle_2 = 0
        good_C_2 = 0
        short_c_2 = 0
        long_c_2 = 0

        lbl_avgtime2.Text = "Average time " & vbCrLf & avg_time_2.ToString("hh\:mm\:ss")
        lbl_min2.Text = "Min time " & vbCrLf & min_time_2.ToString("hh\:mm\:ss")
        lbl_max2.Text = "Max time " & vbCrLf & max_time_2.ToString("hh\:mm\:ss")

        If lbl_NO_PART2_visible = True Then lbl_NO_PART2.Visible = True

        max_time_2 = Nothing
        min_time_2 = Nothing
        avg_time_2 = Nothing

    End Sub

#End Region

#Region "thread shit 3"
    Private Sub BW_process_shift3_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW_Process_shift3.DoWork
        Dim shift_list As New List(Of Integer)

        'shift3
        If Not part_dataset.Tables("partnumbers") Is Nothing Then
            Dim tmp_view As DataView = New DataView(part_dataset.Tables("partnumbers"))

            Dim filter_ As String = "shift = 3 AND end_time >= #" & Selected_parts_date & "# AND end_time <= #" & Selected_parts_date.Add(TimeSpan.FromSeconds(86399)) & "#"
            Dim tmp_datarows() As DataRow = part_dataset.Tables("partnumbers").Select(filter_)
            If tmp_datarows.Count <> 0 Then
                shift_ = "3"
                For Each item As DataRow In tmp_datarows

                    total_cycle_3 = total_cycle_3 + item.Item("total_cycle")
                    good_C_3 = good_C_3 + item.Item("good_cycle")
                    short_c_3 = short_c_3 + item.Item("short_cycle")
                    long_c_3 = long_c_3 + item.Item("long_cycle")

                    If DateTime.Compare(item.Item("start_time"), partbegin3) < 0 Or partbegin3 = jesus_birth Then partbegin3 = item.Item("start_time")
                    If DateTime.Compare(item.Item("end_time"), partend3) > 0 Then partend3 = item.Item("end_time")

                    If TimeSpan.Compare(TimeSpan.Parse(item.Item("max_cycle_time").ToString()), max_time_3) > 0 Then max_time_3 = TimeSpan.Parse(item.Item("max_cycle_time"))
                    If min_time_3 = One_milisec Or TimeSpan.Compare(TimeSpan.Parse(item.Item("min_cycle_time")), min_time_3) < 0 Then min_time_3 = TimeSpan.Parse(item.Item("min_cycle_time"))
                    avg_time_3 = avg_time_3 + TimeSpan.Parse(item.Item("avg_good_cycle_time"))

                Next
            End If

            total_c_3 = (avg_time_3.TotalSeconds + min_time_3.TotalSeconds + max_time_3.TotalSeconds)

            If total_c_3 <> 0 Then
                getpartinfo_3(avg_time_3, good_C_3, short_c_3, long_c_3, total_c_3)
            Else
                Dim bmp As New Bitmap(PictureBox3_needle.Image)
                bmp = DrawRotateImage(bmp, 0)
                PictureBox3_needle_image = bmp
                lbl_NO_PART3_visible = True
            End If

        Else
            shift_ = "-"
        End If
    End Sub
    Private Sub BW_process_shift3_RunWorkerCompleted(ByVal sender As System.Object,
                             ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW_Process_shift3.RunWorkerCompleted


        If total_c_3 <> 0 Then
            lbl_min_th3.Text = lbl_min_th3_text
            lbl_max_th3.Text = If(min_max_type = "_MIN_ONLY", "No max Cycle time for this part", lbl_max_th3_text)
            PB_warning.Visible = True
        Else
            lbl_min_th3.Text = ""
            lbl_max_th3.Text = ""
        End If
        PictureBox3_needle.Image = PictureBox3_needle_image

        show_chart_gauge_3(avg_time_3, min_time_3, short_c_3, good_C_3, long_c_3, total_c_3)


        total_cycle_3 = 0
        good_C_3 = 0
        short_c_3 = 0
        long_c_3 = 0

        lbl_avgtime3.Text = "Average time " & vbCrLf & avg_time_3.ToString("hh\:mm\:ss")
        lbl_min3.Text = "Min time " & vbCrLf & min_time_3.ToString("hh\:mm\:ss")
        lbl_max3.Text = "Max time " & vbCrLf & max_time_3.ToString("hh\:mm\:ss")

        If lbl_NO_PART3_visible = True Then lbl_NO_PART3.Visible = True

        max_time_3 = Nothing
        min_time_3 = Nothing
        avg_time_3 = Nothing

    End Sub

#End Region

#Region "thread machine perf"
    Public mch_perf_period As Dictionary(Of String, Double) = New Dictionary(Of String, Double)
    Private Sub BW_machine_perf_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW_machine_perf.DoWork
        perf.shift1 = New Dictionary(Of String, Double)
        perf.shift2 = New Dictionary(Of String, Double)
        perf.shift3 = New Dictionary(Of String, Double)


        Dim C_Time As Object
        Dim C_Time_double As Double = 0

        Dim filter_ As String = "Date_ >= #" & Selected_parts_date & "# AND Date_ <= #" & Selected_parts_date.Add(TimeSpan.FromSeconds(86399 - Selected_parts_date.TimeOfDay.TotalSeconds)) & "#"




        If part_dataset.Tables("tbl_mch").Rows.Count <> 0 Then
            Dim mch_table As DataTable = part_dataset.Tables("tbl_mch").Select(filter_).CopyToDataTable

            Dim type() As String = mch_table.Rows(0).Item("partnumber").split(";")
            min_max_type = type(type.Count - 2)
            Dim unique_stat_table As DataTable = mch_table.DefaultView.ToTable(True, "status")


            For Each item As DataRow In unique_stat_table.Rows
                'period :
                If Not item.Item("status").ToString().StartsWith("_PARTNO") Then
                    '   mch_perf_period
                    C_Time = mch_table.Compute("sum(cycletime)", "status Like '" & item.Item("status").ToString() & "'")
                    If IsDBNull(C_Time) Then
                        C_Time_double = 0
                    Else
                        C_Time_double = C_Time
                    End If
                    mch_perf_period.Add(item.Item("status").ToString(), If(C_Time Is Nothing, 0, C_Time_double))
                End If



                'shift1:
                If Not item.Item("status").ToString().StartsWith("_PARTNO") Then
                    C_Time = mch_table.Compute("sum(cycletime)", "status Like '" & item.Item("status").ToString() & "' AND SHIFT = '1'")
                    If IsDBNull(C_Time) Then
                        C_Time_double = 0
                    Else
                        C_Time_double = C_Time
                    End If
                    perf.shift1.Add(item.Item("status").ToString(), If(C_Time Is Nothing, 0, C_Time_double))
                End If

                'shift2:
                If Not item.Item("status").ToString().StartsWith("_PARTNO") Then
                    C_Time = mch_table.Compute("sum(cycletime)", "status Like '" & item.Item("status").ToString() & "' AND SHIFT = '2'")
                    If IsDBNull(C_Time) Then
                        C_Time_double = 0
                    Else
                        C_Time_double = C_Time
                    End If
                    perf.shift2.Add(item.Item("status").ToString(), If(C_Time Is Nothing, 0, C_Time_double))
                End If

                'shift3:
                If Not item.Item("status").ToString().StartsWith("_PARTNO") Then
                    C_Time = mch_table.Compute("sum(cycletime)", "status Like '" & item.Item("status").ToString() & "' AND SHIFT = '3'")
                    If IsDBNull(C_Time) Then
                        C_Time_double = 0
                    Else
                        C_Time_double = C_Time
                    End If
                    perf.shift3.Add(item.Item("status").ToString(), If(C_Time Is Nothing, 0, C_Time_double))
                End If

            Next

        Else


            'pas de table
        End If


    End Sub
    Private Sub BW_machine_perf_RunWorkerCompleted(ByVal sender As System.Object,
                             ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW_machine_perf.RunWorkerCompleted


        Dim total As Integer = mch_perf_period.Values.Sum
        Dim total_other As Integer = total
        If total <> 0 Then
            '%
            If mch_perf_period.ContainsKey("CYCLE ON") Then
                LBL_CycleOn_Period.Text = (mch_perf_period.Item("CYCLE ON") * (100 / total)).ToString("00.00")
                total_other = total_other - mch_perf_period.Item("CYCLE ON")
            End If
            If mch_perf_period.ContainsKey("CYCLE OFF") Then
                LBL_CycleOff_Period.Text = (mch_perf_period.Item("CYCLE OFF") * (100 / total)).ToString("00.00")
                total_other = total_other - mch_perf_period.Item("CYCLE OFF")
            End If
            If mch_perf_period.ContainsKey("SETUP") Then
                LBL_Setup_Period.Text = (mch_perf_period.Item("SETUP") * (100 / total)).ToString("00.00")
                total_other = total_other - mch_perf_period.Item("SETUP")
            End If
            LBL_Other_Period.Text = (total_other * (100 / total)).ToString("00.00")
            LBL_Total_Period.Text = (100).ToString("00.00")

            'H
            If mch_perf_period.ContainsKey("CYCLE ON") Then
                LBL_CycleOn_Period_h.Text = TimeSpan.FromSeconds(mch_perf_period.Item("CYCLE ON")).ToString("hh\:mm")
            End If
            If mch_perf_period.ContainsKey("CYCLE OFF") Then
                LBL_CycleOff_Period_h.Text = TimeSpan.FromSeconds(mch_perf_period.Item("CYCLE OFF")).ToString("hh\:mm")
            End If
            If mch_perf_period.ContainsKey("SETUP") Then
                LBL_Cyclesetup_Period_h.Text = TimeSpan.FromSeconds(mch_perf_period.Item("SETUP")).ToString("hh\:mm")
            End If
            LBL_CycleOther_Period_h.Text = TimeSpan.FromSeconds(total_other).ToString("hh\:mm")
            LBL_Cycletotal_Period_h.Text = TimeSpan.FromSeconds(total).ToString("hh\:mm")

        End If

        total = perf.shift1.Values.Sum
        If total <> 0 Then
            For Each item In perf.shift1.Keys
                DGV_status.Rows.Add(item, (perf.shift1.Item(item) * (100 / total)).ToString("00.00"), TimeSpan.FromSeconds(perf.shift1.Item(item)).ToString("hh\:mm"))
            Next
        End If

        'Set_shift_butt_pressed(1)


        total = perf.shift2.Values.Sum
        DGV_status2.Rows.Clear()
        If total <> 0 Then
            For Each item In perf.shift2.Keys
                DGV_status2.Rows.Add(item, (perf.shift2.Item(item) * (100 / total)).ToString("00.00"), TimeSpan.FromSeconds(perf.shift2.Item(item)).ToString("hh\:mm"))
            Next
        End If
        'Set_shift_butt_pressed(2)

        total = perf.shift3.Values.Sum
        DGV_status3.Rows.Clear()
        If total <> 0 Then
            For Each item In perf.shift3.Keys
                DGV_status3.Rows.Add(item, (perf.shift3.Item(item) * (100 / total)).ToString("00.00"), TimeSpan.FromSeconds(perf.shift3.Item(item)).ToString("hh\:mm"))
            Next
        End If
        ' Set_shift_butt_pressed(3)
    End Sub
#End Region

#Region "alternative get data, from the machine tbl"

    Private Sub get_min_max_th()
        Dim mch As DataView = New DataView(part_dataset.Tables("tbl_mch"))
        ' Dim mchfilter_ As String = "shift = 1 AND Date_ >= #" & Selected_parts_date & "# AND Date_ <= #" & Selected_parts_date.Add(TimeSpan.FromSeconds(86399)) & "#"
        Dim mchfilter_ As String = "Date_ >= #" & Selected_parts_date & "# AND Date_ <= #" & Selected_parts_date.Add(TimeSpan.FromSeconds(86399 - Selected_parts_date.TimeOfDay.TotalSeconds)) & "#"

        Dim mch_datarows() As DataRow

        If part_dataset.Tables("tbl_mch").Rows.Count <> 0 Then
            mch_datarows = part_dataset.Tables("tbl_mch").Select(mchfilter_)

            Dim infos() As String = mch(0).Item("partnumber").ToString().Split(";")
            Dim iSpan As TimeSpan = TimeSpan.FromSeconds(infos(1))

            Dim type() As String = part_dataset.Tables("tbl_mch").Rows(0).Item("partnumber").split(";")
            min_max_type = type(type.Count - 2)

            lbl_min_th_text = "Normal min" & vbCrLf & iSpan.ToString("hh\:mm\:ss")
            iSpan = TimeSpan.FromSeconds(infos(2))
            lbl_max_th_text = "Normal max" & vbCrLf & iSpan.ToString("hh\:mm\:ss")
        End If
    End Sub


    Private Sub get_data_from_mch()
        ' must use :
        'Selected_parts_date 'the date
        'selected_part ' the part





        Dim filter_ As String = "Date_ >= #" & Selected_parts_date & "# AND Date_ <= #" & Selected_parts_date.Add(TimeSpan.FromSeconds(86399 - Selected_parts_date.TimeOfDay.TotalSeconds)) & "#"




        If part_dataset.Tables("tbl_mch").Rows.Count <> 0 Then
            Dim mch_table As DataTable = part_dataset.Tables("tbl_mch").Select(filter_).CopyToDataTable



            Dim unique_stat_table As DataTable = mch_table.DefaultView.ToTable(True, "status")


            For Each item As DataRow In unique_stat_table.Rows
                'period :
                If Not item.Item("status").ToString().StartsWith("_PARTNO") Then
                    '   mch_perf_period
                    'C_Time = mch_table.Compute("sum(cycletime)", "status Like '" & item.Item("status").ToString() & "'")
                    'If IsDBNull(C_Time) Then
                    '    C_Time_double = 0
                    'Else
                    '    C_Time_double = C_Time
                    'End If
                    '  mch_perf_period.Add(item.Item("status").ToString(), If(C_Time Is Nothing, 0, C_Time_double))
                End If



                'shift1:
                If Not item.Item("status").ToString().StartsWith("_PARTNO") Then
                    'C_Time = mch_table.Compute("sum(cycletime)", "status Like '" & item.Item("status").ToString() & "' AND SHIFT = '1'")
                    'If IsDBNull(C_Time) Then
                    '    C_Time_double = 0
                    'Else
                    '    C_Time_double = C_Time
                    'End If
                    'perf.shift1.Add(item.Item("status").ToString(), If(C_Time Is Nothing, 0, C_Time_double))
                End If

                'shift2:
                If Not item.Item("status").ToString().StartsWith("_PARTNO") Then
                    'C_Time = mch_table.Compute("sum(cycletime)", "status Like '" & item.Item("status").ToString() & "' AND SHIFT = '2'")
                    'If IsDBNull(C_Time) Then
                    '    C_Time_double = 0
                    'Else
                    '    C_Time_double = C_Time
                    'End If
                    'perf.shift2.Add(item.Item("status").ToString(), If(C_Time Is Nothing, 0, C_Time_double))
                End If

                'shift3:
                If Not item.Item("status").ToString().StartsWith("_PARTNO") Then
                    'C_Time = mch_table.Compute("sum(cycletime)", "status Like '" & item.Item("status").ToString() & "' AND SHIFT = '3'")
                    'If IsDBNull(C_Time) Then
                    '    C_Time_double = 0
                    'Else
                    '    C_Time_double = C_Time
                    'End If
                    'perf.shift3.Add(item.Item("status").ToString(), If(C_Time Is Nothing, 0, C_Time_double))
                End If

            Next

        Else


            'pas de table
        End If



    End Sub
#End Region



    Private Sub Chart_Gauge_Click(sender As Object, pnt As PaintEventArgs) Handles Chart_Gauge.Paint

        'Green arrow:
        My.Resources.ResourceManager.GetObject("Arrow_green.png")
        Dim g As Graphics = pnt.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        Dim Bitmap As Bitmap = New Bitmap(My.Resources.Arrow_green, New Size(10, 17))
        Dim locationX As Integer = (Chart_Gauge.ChartAreas(0).InnerPlotPosition.X + Chart_Gauge.ChartAreas(0).Position.Width + 10)
        Dim locationY As Integer = (Chart_Gauge.ChartAreas(0).Position.Y + 5)
        g.DrawImage(Bitmap, locationX, locationY)


        'Red arrow min:

        'position to the centre ( local (0,0) )
        locationX = Chart_Gauge.Location.X + 12 + Chart_Gauge.ChartAreas(0).InnerPlotPosition.X + Chart_Gauge.ChartAreas(0).Position.Width - 7
        locationY = Chart_Gauge.Location.Y + Chart_Gauge.ChartAreas(0).Position.Height + 10

        Dim r As Integer = 87
        locationX = locationX - r * Math.Cos(-short_avg_angle * (Math.PI / 180))
        locationY = locationY + r * Math.Sin(-short_avg_angle * (Math.PI / 180))
        Dim ptsArray As PointF() = {New PointF(75.0F, 120.0F), New PointF(70.0F, 100.0F), New PointF(65.0F, 120.0F), New PointF(75.0F, 120.0F)}

        Dim i As Integer = 0
        For Each point In ptsArray

            'translation to (0,0)
            point.X = point.X - 70
            point.Y = point.Y - 100

            'rotation
            ptsArray(i) = rot(point, (-90 - short_avg_angle) * (Math.PI / 180), locationX, locationY)
            i += 1
        Next

        gp1 = New Drawing2D.GraphicsPath(Drawing2D.FillMode.Alternate)
        gp1.AddLines(ptsArray)
        gp1.CloseFigure()

        pnt.Graphics.FillPath(Brushes.Red, gp1)
        pnt.Graphics.DrawLines(Pens.Black, ptsArray)
    End Sub
    Private Sub Chart_Gauge2_Click(sender As Object, pnt As PaintEventArgs) Handles chart_gauge2.Paint

        'Green arrow:
        My.Resources.ResourceManager.GetObject("Arrow_green.png")
        Dim g As Graphics = pnt.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        Dim Bitmap As Bitmap = New Bitmap(My.Resources.Arrow_green, New Size(10, 17))
        Dim locationX As Integer = (Chart_Gauge.ChartAreas(0).InnerPlotPosition.X + Chart_Gauge.ChartAreas(0).Position.Width + 10)
        Dim locationY As Integer = (Chart_Gauge.ChartAreas(0).Position.Y + 5)
        g.DrawImage(Bitmap, locationX, locationY)


        'Red arrow min:

        'position to the centre ( local (0,0) )
        locationX = chart_gauge2.Location.X + 12 + chart_gauge2.ChartAreas(0).InnerPlotPosition.X + chart_gauge2.ChartAreas(0).Position.Width - 7
        locationY = chart_gauge2.Location.Y + chart_gauge2.ChartAreas(0).Position.Height + 10

        Dim r As Integer = 87
        locationX = locationX - r * Math.Cos(-short_avg_angle2 * (Math.PI / 180))
        locationY = locationY + r * Math.Sin(-short_avg_angle2 * (Math.PI / 180))
        Dim ptsArray As PointF() = {New PointF(75.0F, 120.0F), New PointF(70.0F, 100.0F), New PointF(65.0F, 120.0F), New PointF(75.0F, 120.0F)}

        Dim i As Integer = 0
        For Each point In ptsArray

            'translation to (0,0)
            point.X = point.X - 70
            point.Y = point.Y - 100

            'rotation
            ptsArray(i) = rot(point, (-90 - short_avg_angle2) * (Math.PI / 180), locationX, locationY)
            i += 1
        Next

        gp2 = New Drawing2D.GraphicsPath(Drawing2D.FillMode.Alternate)
        gp2.AddLines(ptsArray)
        gp2.CloseFigure()

        pnt.Graphics.FillPath(Brushes.Red, gp2)
        pnt.Graphics.DrawLines(Pens.Black, ptsArray)
    End Sub
    Private Sub Chart_Gauge3_Click(sender As Object, pnt As PaintEventArgs) Handles chart_gauge3.Paint

        'Green arrow:
        My.Resources.ResourceManager.GetObject("Arrow_green.png")
        Dim g As Graphics = pnt.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        Dim Bitmap As Bitmap = New Bitmap(My.Resources.Arrow_green, New Size(10, 17))
        Dim locationX As Integer = (Chart_Gauge.ChartAreas(0).InnerPlotPosition.X + Chart_Gauge.ChartAreas(0).Position.Width + 10)
        Dim locationY As Integer = (Chart_Gauge.ChartAreas(0).Position.Y + 5)
        g.DrawImage(Bitmap, locationX, locationY)


        'Red arrow min:

        'position to the centre ( local (0,0) )
        locationX = chart_gauge3.Location.X + 12 + chart_gauge3.ChartAreas(0).InnerPlotPosition.X + chart_gauge3.ChartAreas(0).Position.Width - 7
        locationY = chart_gauge3.Location.Y + chart_gauge3.ChartAreas(0).Position.Height + 10

        Dim r As Integer = 87
        locationX = locationX - r * Math.Cos(-short_avg_angle3 * (Math.PI / 180))
        locationY = locationY + r * Math.Sin(-short_avg_angle3 * (Math.PI / 180))
        Dim ptsArray As PointF() = {New PointF(75.0F, 120.0F), New PointF(70.0F, 100.0F), New PointF(65.0F, 120.0F), New PointF(75.0F, 120.0F)}

        Dim i As Integer = 0
        For Each point In ptsArray

            'translation to (0,0)
            point.X = point.X - 70
            point.Y = point.Y - 100

            'rotation
            ptsArray(i) = rot(point, (-90 - short_avg_angle3) * (Math.PI / 180), locationX, locationY)
            i += 1
        Next

        gp2 = New Drawing2D.GraphicsPath(Drawing2D.FillMode.Alternate)
        gp2.AddLines(ptsArray)
        gp2.CloseFigure()

        pnt.Graphics.FillPath(Brushes.Red, gp2)
        pnt.Graphics.DrawLines(Pens.Black, ptsArray)
    End Sub


    Public Lock__ As Boolean = True
    Public perf As New CSI_Library.CSI_DATA.periode


    Private Sub lock_Click(sender As Object, e As EventArgs) Handles Lock.Click
        If Lock__ = True Then
            Lock.BackgroundImage = My.Resources.Unlock
            Lock__ = False
        Else
            Lock.BackgroundImage = My.Resources.lock
            Lock__ = True
        End If
    End Sub
    '    Function machine_perf(date_start As Date, date_end As Date, partno As String) As CSI_Library.CSI_DATA.Mch_perf_byPart


    '        Dim tmp_table As DataSet = New DataSet("table")

    '        Dim ThirdDim As Integer = 0

    '        Dim i As Integer
    '        Dim active_machines(1) As String

    '        Dim shft As Integer

    '        Dim sorted_stats(4) As String
    '        Dim percent(7) As Integer

    '        '  Dim results As DateTime()
    '        Dim k As Integer = 0
    '        Dim final_time As Double
    '        Dim total1 As Double = 0
    '        Dim total2 As Double = 0
    '        Dim total3 As Double = 0
    '        '      Dim rang_ As Integer
    '        Dim DaysInMonth As Integer

    '        Dim last_loop_ As Boolean = False

    '        Dim final_time_other(4) As Double
    '        ' Dim nb_mch As Integer

    '        final_time = 0


    '        Dim periode_return As periode()
    '        Dim mysqldb As Boolean
    '        mysqldb = True

    '        If Not (Welcome.CSIF_version) Then
    '            '*************************************************************************************************************************************************'
    '            '**** DB Connection
    '            '*************************************************************************************************************************************************'
    '            Dim cnt As SQLiteConnection
    '            mysqldb = False
    '            Try
    '                cnt = New SQLiteConnection(sqlitedbpath)
    '                cnt.Open()

    '                If cnt.State = 1 Then
    '                Else
    '                    MessageBox.Show("Connection to the database failed")
    '                    GoTo End_
    '                End If
    '            Catch ex As Exception
    '                MessageBox.Show(" Enable to establish a connection to the database ") ' & ex.Message), vbCritical + vbSystemModal)
    '                GoTo End_
    '            End Try

    '            '*************************************************************************************************************************************************'
    '            '**** DB Connection -END
    '            '*************************************************************************************************************************************************'


    '            i = 0
    '            Try
    '                '******************************************************************************************************************************
    '                'For each selected machine:====================================================================================================
    '                '==============================================================================================================================

    '                For Each items In machine
    '                    '  For shft = 1 To 3
    '                    If items <> "" Then
    '                        machine(i) = RenameMachine(machine(i))

    '                        k = 0
    '                        final_time = Nothing

    '                        total1 = 0
    '                        total2 = 0
    '                        total3 = 0

    '                        ' Building the QueR ====================================================
    '                        Dim adapter As New SQLiteDataAdapter
    '                        Dim reader As SQLiteDataReader
    '                        Dim table_ As DataTable = New DataTable("tmp_table")
    '                        Dim tmp_table_cmd As New SQLiteCommand

    '                        Try
    '                            Dim query As String
    '                            query = "Select shift,status,sum(cycletime) as cycletime  from [tbl_" + renamedmachine + "]

    '                            If use_mysql Then
    '                                query += " from CSI_Database.tbl_" + renamedmachine
    '                            Else
    '                                query += " from [tbl_" + renamedmachine + "]"
    '                            End If

    '                            If Not IsNothing(startdate) And Not IsNothing(enddate) Then
    '                                query += " where Date_ between '" + startdate.ToString("yyyy-MM-dd") + "' and '" + enddate.AddDays(1).ToString("yyyy-MM-dd") + "'"
    '                            End If

    '                            query += " group by year_,month_,day_,shift,status"

    '                            Return query


    '                            tmp_table_cmd.Connection = cnt
    '                            reader = tmp_table_cmd.ExecuteReader
    '                            adapter.SelectCommand = tmp_table_cmd
    '                            table_.Load(reader)
    '                        Catch ex As Exception
    '                            Dim cmd As New SQLiteCommand("CREATE TABLE if not exists tbl_" & machine(i) & " (month_ integer, day_ integer, year_ integer ,time_ datetime,Date_ datetime, status varchar(255), shift integer,cycletime double, UNIQUE KEY (time_,status))", cnt)
    '                            cmd.ExecuteNonQuery()
    '                        End Try



    '                        '====================================================================== tmp_table ready.



    '                        Dim periode_ As New periode
    '                        periode_.shift1 = New Dictionary(Of String, Double)
    '                        periode_.shift2 = New Dictionary(Of String, Double)
    '                        periode_.shift3 = New Dictionary(Of String, Double)

    '                        ' Select available status ====================================================
    '                        Dim available_status As DataTable

    '                        Dim VIEW As DataView = New DataView(table_)

    '                        For shft = 1 To 3
    '                            VIEW.RowFilter = "SHIFT =" & shft
    '                            'ReDim stat(-1)

    '                            k = 0
    '                            'ReDim stat(0)

    '                            Dim stat As String()



    '                            Dim l As Integer = 0
    '                            available_status = VIEW.ToTable(True, "status")

    '                            For Each row In available_status.Rows
    '                                If (available_status(l).Item("status") <> "_SH_START") And (available_status(l).Item("status") <> "_SH_END") Then
    '                                    ReDim Preserve stat(k + 1)
    '                                    stat(k) = available_status(l).Item("status")
    '                                    k = k + 1
    '                                End If
    '                                l = l + 1
    '                            Next
    '                            If IsNothing(stat) Then
    '                            Else
    '                                If UBound(stat) <> 0 Then
    '                                    ReDim Preserve stat(UBound(stat) - 1)
    '                                End If
    '                            End If

    '                            '======================================================================= all Status in stat()

    '                            Dim CycleTime As Object

    '                            If Not IsNothing(stat) Then


    '                                'for each shift:
    '                                ' For shft = 1 To 3
    '                                For l = 0 To UBound(stat)

    '                                    CycleTime = table_.Compute("sum(cycletime)", "status='" & stat(l) & "' and shift=" & shft)
    '                                    If IsDBNull(CycleTime) Then CycleTime = 0

    '                                    'SQLITE
    '                                    'Dim loadingascon = GetLoadingAsCON()
    '                                    'If (loadingascon And stat(1) = "LOADING") Then
    '                                    '    stat(1) = "CYCLE ON"
    '                                    'End If

    '                                    If shft = 1 Then

    '                                        If stat(l) = "_CON" Or stat(l) = "CYCLE ON" Then
    '                                            periode_.shift1.Add("CYCLE ON", CycleTime)
    '                                        ElseIf stat(l) = "_COFF" Or stat(l) = "CYCLE OFF" Then
    '                                            periode_.shift1.Add("CYCLE OFF", CycleTime)
    '                                        ElseIf stat(l) = "_SETUP" Or stat(l) = "SETUP" Then
    '                                            periode_.shift1.Add("SETUP", CycleTime)
    '                                        Else
    '                                            periode_.shift1.Add(stat(l), CycleTime)
    '                                        End If


    '                                    ElseIf shft = 2 Then
    '                                        If stat(l) = "_CON" Or stat(l) = "CYCLE ON" Then
    '                                            periode_.shift2.Add("CYCLE ON", CycleTime)
    '                                        ElseIf stat(l) = "_COFF" Or stat(l) = "CYCLE OFF" Then
    '                                            periode_.shift2.Add("CYCLE OFF", CycleTime)
    '                                        ElseIf stat(l) = "_SETUP" Or stat(l) = "SETUP" Then
    '                                            periode_.shift2.Add("SETUP", CycleTime)
    '                                        Else
    '                                            periode_.shift2.Add(stat(l), CycleTime)
    '                                        End If


    '                                    ElseIf shft = 3 Then
    '                                        If stat(l) = "_CON" Or stat(l) = "CYCLE ON" Then
    '                                            periode_.shift3.Add("CYCLE ON", CycleTime)
    '                                        ElseIf stat(l) = "_COFF" Or stat(l) = "CYCLE OFF" Then
    '                                            periode_.shift3.Add("CYCLE OFF", CycleTime)
    '                                        ElseIf stat(l) = "_SETUP" Or stat(l) = "SETUP" Then
    '                                            periode_.shift3.Add("SETUP", CycleTime)
    '                                        Else
    '                                            periode_.shift3.Add(stat(l), CycleTime)
    '                                        End If

    '                                    End If


    '                                Next l
    '                            Else
    '                                '"""
    '                            End If
    'No_stat:
    '                            stat = Nothing
    '                        Next shft

    '                        periode_.shift1.OrderBy((Function(x) x.Key)).ToDictionary(Function(x) x.Key, Function(y) y.Value)
    '                        periode_.shift2.OrderBy((Function(x) x.Key)).ToDictionary(Function(x) x.Key, Function(y) y.Value)
    '                        periode_.shift3.OrderBy((Function(x) x.Key)).ToDictionary(Function(x) x.Key, Function(y) y.Value)

    '                        If date1.Date = date2.Date Then
    '                            periode_.date_ = date1.Day & " " & strmonth(date1.Month) & " " & date1.Year
    '                        Else
    '                            If date1.Day = 1 And date1.Day = System.DateTime.DaysInMonth(date2.Year, date2.Month) Then
    '                                periode_.date_ = strmonth(date1.Month) & " " & date1.Year
    '                            Else
    '                                If date1.Year = date2.Year Then

    '                                    periode_.date_ = date1.Day & " " & strmonth(date1.Month) & " to " & date2.Day & " " & strmonth(date2.Month) & " " & (date2.Year)
    '                                Else
    '                                    periode_.date_ = date1.Day & " " & strmonth(date1.Month) & " " & (date1.Year) & " to " & date2.Day & " " & strmonth(date2.Month) & " " & (date2.Year)
    '                                End If
    '                            End If
    '                        End If

    '                        periode_.machine_name = machine(i)

    '                        ReDim Preserve periode_return(i + 1)
    '                        periode_return(i) = periode_

    '                        i = i + 1
    '                    End If
    '                Next '/item /machine ======================================================================================+==================
    '                ''============================================================================================================================
    '            Catch ex As EvaluateException

    '            Catch ex As Exception
    '                'MessageBox.Show(TraceMessage(ex.Message))
    '                MessageBox.Show("Error while parsing machine data")
    '            End Try

    'no_loop:
    '            cnt.Close()
    '            ThirdDim = ThirdDim - 1
    '        Else
    '            '*************************************************************************************************************************************************'
    '            '**** DB Connection
    '            '*************************************************************************************************************************************************'
    '            Dim cnt As MySqlConnection
    '            Dim cntstr As String = ""
    '            Try
    '                Dim db_authPath As String = Nothing
    '                Dim directory As String = getRootPath()
    '                If (File.Exists(directory + "/sys/SrvDBpath.csys")) Then
    '                    Using reader As New StreamReader(directory + "/sys/SrvDBpath.csys")
    '                        db_authPath = reader.ReadLine()
    '                    End Using
    '                End If

    '                Dim server = db_authPath
    '                Dim connectionString As String
    '                connectionString = "SERVER=" + server + ";" + "DATABASE=csi_database;" + MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"

    '                cnt = New MySqlConnection(connectionString)
    '                cnt.Open()

    '                If cnt.State = 1 Then
    '                Else
    '                    MessageBox.Show("Connection to the database failed")
    '                    GoTo End_
    '                End If

    '                cntstr = connectionString
    '            Catch ex As Exception
    '                MessageBox.Show(" Enable to establish a connection to the database ") ' & ex.Message), vbCritical + vbSystemModal)
    '                GoTo End_
    '            End Try


    '            '*************************************************************************************************************************************************'
    '            '**** DB Connection -END
    '            '*************************************************************************************************************************************************'

    '            Dim first_insertion As Boolean = True ' for daily reports

    '            i = 0

    '            Try
    '                '******************************************************************************************************************************
    '                'For each selected machine:====================================================================================================
    '                '==============================================================================================================================

    '                For Each items In machine
    '                    '  For shft = 1 To 3
    '                    If items <> "" Then
    '                        machine(i) = RenameMachine(machine(i))

    '                        k = 0
    '                        final_time = Nothing

    '                        total1 = 0
    '                        total2 = 0
    '                        total3 = 0

    '                        ' Building the QueR ====================================================
    '                        Dim adapter As New MySqlDataAdapter
    '                        Dim reader As MySqlDataReader
    '                        Dim table_ As DataTable = New DataTable("tmp_table")
    '                        Dim tmp_table_cmd As New MySqlCommand
    '                        tmp_table_cmd.CommandText = ShiftTableQuery(mysqldb, machine(i), date1, date2)
    '                        tmp_table_cmd.Connection = cnt
    '                        reader = tmp_table_cmd.ExecuteReader
    '                        adapter.SelectCommand = tmp_table_cmd
    '                        table_.Load(reader)


    '                        Dim periode_ As New periode
    '                        periode_.shift1 = New Dictionary(Of String, Double)
    '                        periode_.shift2 = New Dictionary(Of String, Double)
    '                        periode_.shift3 = New Dictionary(Of String, Double)

    '                        ' Select available status ====================================================
    '                        Dim available_status As DataTable

    '                        Dim VIEW As DataView = New DataView(table_)

    '                        For shft = 1 To 3
    '                            VIEW.RowFilter = "SHIFT =" & shft
    '                            'ReDim stat(-1)

    '                            k = 0
    '                            'ReDim stat(0)

    '                            Dim stat As String()



    '                            Dim l As Integer = 0
    '                            available_status = VIEW.ToTable(True, "status")

    '                            For Each row In available_status.Rows
    '                                If (available_status(l).Item("status") <> "_SH_START") And (available_status(l).Item("status") <> "_SH_END") Then
    '                                    ReDim Preserve stat(k + 1)
    '                                    stat(k) = available_status(l).Item("status")
    '                                    k = k + 1
    '                                End If
    '                                l = l + 1
    '                            Next
    '                            If IsNothing(stat) Then
    '                            Else
    '                                If UBound(stat) <> 0 Then
    '                                    ReDim Preserve stat(UBound(stat) - 1)
    '                                End If
    '                            End If

    '                            '======================================================================= all Status in stat()

    '                            Dim CycleTime As Object

    '                            If Not IsNothing(stat) Then


    '                                'for each shift:
    '                                ' For shft = 1 To 3
    '                                For l = 0 To UBound(stat)

    '                                    CycleTime = table_.Compute("sum(cycletime)", "status='" & stat(l) & "' and shift=" & shft)
    '                                    If IsDBNull(CycleTime) Then CycleTime = 0

    '                                    'Dim loadingascon = GetLoadingAsCON(cntstr)
    '                                    'If (stat(1) IsNot Nothing) Then
    '                                    '    If (loadingasCON And stat(1) = "LOADING") Then
    '                                    '        stat(1) = "CYCLE ON"
    '                                    '    End If
    '                                    'ElseIf (stat(0) = "LOADING") Then
    '                                    '    stat(0) = "CYCLE ON"
    '                                    'End If
    '                                    If (loadingasCON And stat(l) = "LOADING") Then
    '                                        stat(l) = "CYCLE ON"
    '                                    End If


    '                                    If shft = 1 Then

    '                                        If stat(l) = "_CON" Or stat(l) = "CYCLE ON" Then
    '                                            periode_.shift1.Add("CYCLE ON", CycleTime)
    '                                        ElseIf stat(l) = "_COFF" Or stat(l) = "CYCLE OFF" Then
    '                                            periode_.shift1.Add("CYCLE OFF", CycleTime)
    '                                        ElseIf stat(l) = "_SETUP" Or stat(l) = "SETUP" Then
    '                                            periode_.shift1.Add("SETUP", CycleTime)
    '                                        Else
    '                                            periode_.shift1.Add(stat(l), CycleTime)
    '                                        End If


    '                                    ElseIf shft = 2 Then
    '                                        If stat(l) = "_CON" Or stat(l) = "CYCLE ON" Then
    '                                            periode_.shift2.Add("CYCLE ON", CycleTime)
    '                                        ElseIf stat(l) = "_COFF" Or stat(l) = "CYCLE OFF" Then
    '                                            periode_.shift2.Add("CYCLE OFF", CycleTime)
    '                                        ElseIf stat(l) = "_SETUP" Or stat(l) = "SETUP" Then
    '                                            periode_.shift2.Add("SETUP", CycleTime)
    '                                        Else
    '                                            periode_.shift2.Add(stat(l), CycleTime)
    '                                        End If


    '                                    ElseIf shft = 3 Then
    '                                        If stat(l) = "_CON" Or stat(l) = "CYCLE ON" Then
    '                                            periode_.shift3.Add("CYCLE ON", CycleTime)
    '                                        ElseIf stat(l) = "_COFF" Or stat(l) = "CYCLE OFF" Then
    '                                            periode_.shift3.Add("CYCLE OFF", CycleTime)
    '                                        ElseIf stat(l) = "_SETUP" Or stat(l) = "SETUP" Then
    '                                            periode_.shift3.Add("SETUP", CycleTime)
    '                                        Else
    '                                            periode_.shift3.Add(stat(l), CycleTime)
    '                                        End If


    '                                    End If

    '                                Next l
    '                            Else
    '                                '"""
    '                            End If
    '                            'No_stat:
    '                            stat = Nothing
    '                        Next shft

    '                        periode_.shift1.OrderBy((Function(x) x.Key)).ToDictionary(Function(x) x.Key, Function(y) y.Value)
    '                        periode_.shift2.OrderBy((Function(x) x.Key)).ToDictionary(Function(x) x.Key, Function(y) y.Value)
    '                        periode_.shift3.OrderBy((Function(x) x.Key)).ToDictionary(Function(x) x.Key, Function(y) y.Value)

    '                        If date1.Date = date2.Date Then
    '                            periode_.date_ = date1.Day & " " & strmonth(date1.Month) & " " & date1.Year
    '                        Else
    '                            If date1.Day = 1 And date1.Day = System.DateTime.DaysInMonth(date2.Year, date2.Month) Then
    '                                periode_.date_ = strmonth(date1.Month) & " " & date1.Year
    '                            Else
    '                                If date1.Year = date2.Year Then

    '                                    periode_.date_ = date1.Day & " " & strmonth(date1.Month) & " to " & date2.Day & " " & strmonth(date2.Month) & " " & (date2.Year)
    '                                Else
    '                                    periode_.date_ = date1.Day & " " & strmonth(date1.Month) & " " & (date1.Year) & " to " & date2.Day & " " & strmonth(date2.Month) & " " & (date2.Year)
    '                                End If
    '                            End If
    '                        End If

    '                        periode_.machine_name = machine(i)

    '                        ReDim Preserve periode_return(i + 1)
    '                        periode_return(i) = periode_

    '                        i = i + 1
    '                    End If
    '                Next '/item /machine ======================================================================================+==================
    '                ''============================================================================================================================

    '            Catch ex As Exception
    '                'MessageBox.Show(TraceMessage(ex.Message))
    '                MessageBox.Show("Error while parsing machine data")
    '            End Try

    '            cnt.Close()
    '            ThirdDim = ThirdDim - 1
    '        End If

    'End_:


    '        ' With shifts





    '    End Function



End Class
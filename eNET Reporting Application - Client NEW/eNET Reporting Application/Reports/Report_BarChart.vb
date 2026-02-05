Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows
Imports System.IO
Imports System.IO.File
Imports CSI_Library.CSI_DATA
Imports System.Threading
Imports System.Globalization
Imports System.Drawing.Printing

'PRINT

'Imports System.Drawing

Public Class Report_BarChart
    Public data(4) As Integer
    Public data2(89) As Integer
    Public data3(89) As Integer
    Public data4(89) As Integer
    Public data5(89) As Integer
    Public data99(89) As Integer
    Public Pie_chart_source As Integer

    Public maxZoom As Integer
    Public consolidated_shift_array(26, 3, 1) As String

    Public temporary_periode_returned As periode()
    Public consolidated As Boolean = False
    Public target_ As Integer = 0
    Public targetColor_ As String = 0
    Public consolidated_periode(1) As periode
    Public Big_consolidated_periode(1, 89) As periode
    Public mousePoint As Point
    Public newy As Integer
    Public CSI_LIB As New CSI_Library.CSI_Library
    Public Other_color As Color
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Load Report_BarChart
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Report_BarChart_Loaded(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' Me.ResumeLayout()
    End Sub

    Public colors As Dictionary(Of String, Integer)

    Private Sub Report_BC(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Using p As New Pen(Color.FromArgb(217, 217, 217), 2.0)
            e.Graphics.DrawRectangle(p, 0, 0, Me.Width, Me.Height)
        End Using
    End Sub

    Private Sub Report_BarChart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim p As New PictureBox
        'Dim rect As New Rectangle
        'rect.Location = Me.Location
        'rect.Size = Me.Size
        '  Me.Dock = DockStyle.None

        ' Me.SuspendLayout()

        Config_report.BTN_Create.Text = "Update"
        Dim alpha As Integer = 255
        Dim backcolors As Color
        Dim backcolors2 As Color
        Dim backcolors3 As Color

        Dim foreColor As Color
        Dim foreColor2 As Color
        Dim foreColor3 As Color


        colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)


        Try

            backcolors = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE ON"))
            backcolors = Color.FromArgb(alpha, backcolors.R, backcolors.G, backcolors.B)
            foreColor = getContrastColor(backcolors)

            Label1.BackColor = backcolors
            LBL_CycleOn_Period.BackColor = backcolors
            LBL_CycleOn_Shift1.BackColor = backcolors
            LBL_CycleOn_Shift2.BackColor = backcolors
            LBL_CycleOn_Shift3.BackColor = backcolors
            Label1.ForeColor = foreColor
            LBL_CycleOn_Period.ForeColor = foreColor
            LBL_CycleOn_Shift1.ForeColor = foreColor
            LBL_CycleOn_Shift2.ForeColor = foreColor
            LBL_CycleOn_Shift3.ForeColor = foreColor



            backcolors2 = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE OFF"))
            backcolors2 = Color.FromArgb(alpha, backcolors2.R, backcolors2.G, backcolors2.B)
            foreColor2 = getContrastColor(backcolors2)

            Label2.BackColor = backcolors2
            LBL_CycleOff_Period.BackColor = backcolors2
            LBL_CycleOff_Shift1.BackColor = backcolors2
            LBL_CycleOff_Shift2.BackColor = backcolors2
            LBL_CycleOff_Shift3.BackColor = backcolors2
            Label2.ForeColor = foreColor2
            LBL_CycleOff_Period.ForeColor = foreColor2
            LBL_CycleOff_Shift1.ForeColor = foreColor2
            LBL_CycleOff_Shift2.ForeColor = foreColor2
            LBL_CycleOff_Shift3.ForeColor = foreColor2



            backcolors3 = System.Drawing.ColorTranslator.FromWin32(colors("SETUP"))
            backcolors3 = Color.FromArgb(alpha, backcolors3.R, backcolors3.G, backcolors3.B)
            foreColor3 = getContrastColor(backcolors3)
            Label3.BackColor = backcolors3
            LBL_Setup_Period.BackColor = backcolors3
            LBL_Setup_Shift1.BackColor = backcolors3
            LBL_Setup_Shift2.BackColor = backcolors3
            LBL_Setup_Shift3.BackColor = backcolors3
            Label3.ForeColor = foreColor3
            LBL_Setup_Period.ForeColor = foreColor3
            LBL_Setup_Shift1.ForeColor = foreColor3
            LBL_Setup_Shift2.ForeColor = foreColor3
            LBL_Setup_Shift3.ForeColor = foreColor3

        Catch ex As Exception
            System.Console.WriteLine(ex.Message)
        End Try



        Try


            Me.MdiParent = Reporting_application
            Me.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Size.Width, 0)
            Me.Height = Config_report.Height

            If Exists(rootPath & "\sys\targetColor_.csys") Then
                Using reader As StreamReader = New StreamReader(rootPath & "\sys\targetColor_.csys")
                    targetColor_ = CInt(reader.ReadLine())
                    reader.Close()
                End Using
            End If

            If File.Exists(rootPath & "\sys\otherColor_.csys") Then
                Using reader As StreamReader = New StreamReader(rootPath & "\sys\otherColor_.csys")
                    Dim col As String = CInt(reader.ReadLine())
                    reader.Close()
                    Other_color = Color.FromArgb(col)
                End Using
            Else
                Other_color = Color.FromArgb(-32768)
            End If

            If Exists(rootPath & "\sys\target_.csys") Then
                Using reader As StreamReader = New StreamReader(rootPath & "\sys\target_.csys")
                    target_ = CInt(reader.ReadLine()) 'this line will call NumericUpDownValueChanged
                    reader.Close()
                End Using
            End If


            Call target(Val(target_), Val(targetColor_))
            ' GroupBox3.ForeColor = Color.White



            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            ' Me.BackColor = Color.Transparent
            SetStyle(ControlStyles.DoubleBuffer, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)



            CB_Report.SelectedIndex = CB_Report.Items.Count - 1


            If Welcome.CSIF_version = 1 Then

                'Chart1.Palette 
                Chart2.Series("Series3").Color = Color.Transparent
                Chart2.Series("Series4").Color = Color.Transparent

            Else
                Chart2.Series("Series1").Color = backcolors
                Chart2.Series("Series2").Color = backcolors2
                Chart2.Series("Series3").Color = backcolors3
                Chart2.Series("Series4").Color = Other_color
            End If

        Catch ex As Exception
            CSI_LIB.LogClientError("load barGraph rep error : " & ex.Message)
        End Try





    End Sub

    Function getContrastColor(backColor As Color)
        Dim contrastcolor As Color
        Dim rdif As Integer, gdif As Integer, bdif As Integer

        If (backColor.R > 100 And backColor.R < 150) Then
            rdif = 150
        Else
            rdif = 255
        End If

        If (backColor.G > 100 And backColor.G < 150) Then
            gdif = 150
        Else
            gdif = 255
        End If

        If (backColor.B > 100 And backColor.B < 150) Then
            bdif = 150
        Else
            bdif = 255
        End If

        contrastcolor = Color.FromArgb(rdif - backColor.R, gdif - backColor.G, bdif - backColor.B)

        Return contrastcolor
    End Function

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Set the target line
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Sub target(target_ As Integer, targetColor As Integer)
        Try
            Dim datatarget(89) As Integer
            Dim i As Integer
            For i = 0 To 89
                datatarget(i) = target_
            Next
            Chart2.Series("Series5").Points.DataBindY(datatarget)

            'Chart2.ChartAreas(0).AxisY.StripLines.Clear()
            'Dim stripLineTarget As StripLine = New StripLine()
            'stripLineTarget.StripWidth = 0
            'stripLineTarget.BorderColor = Color.FromArgb(targetColor)
            'stripLineTarget.BackColor = System.Drawing.Color.RosyBrown
            'stripLineTarget.BackGradientStyle = GradientStyle.LeftRight
            'stripLineTarget.BorderWidth = 2
            'stripLineTarget.BorderDashStyle = ChartDashStyle.DashDot
            'stripLineTarget.IntervalOffset = target_


            'Chart2.ChartAreas(0).AxisY.StripLines.Add(stripLineTarget)



            'Chart2.ChartAreas(0).AxisY.LabelStyle.Enabled = True


            'Dim cl As New CustomLabel
            'cl.FromPosition = target_
            'cl.ToPosition = target_ + 1
            'cl.Text = target_.ToString()
            'cl.RowIndex = 0

            'Chart2.ChartAreas(0).AxisY.CustomLabels.Clear()
            'Chart2.ChartAreas(0).AxisY.CustomLabels.Add(cl)

            'Chart2.DataBind()
        Catch ex As Exception
            MessageBox.Show("Could not set the target line ") ' & ex.Message)
        End Try

    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Combobox
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Report.SelectedIndexChanged

        Dim mach As String = CB_Report.SelectedItem.ToString()
        mach = mach.Substring(0, mach.IndexOf(":")).Trim
        LBL_MachineName.Text = mach
        mach = CSI_LIB.RenameMachine(mach)

        If consolidated = False Then
            refresh_Report_BarChart(Config_report.big_periode_returned, Config_report.periode_returned, Config_report.machine_Report_BarChart)
        Else
            refresh_Report_BarChart(Big_consolidated_periode, consolidated_periode, 0)
        End If

        If Machine_util_det.Visible = True Then
            Machine_util_det.Close()
        End If
        If Welcome.CSIF_version = 1 Then

            'Chart1.Palette 
            Chart2.Series("Series3").Color = Color.Transparent
            Chart2.Series("Series4").Color = Color.Transparent


        End If
    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' check if valid date
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Function checkValidDate(dates As String(), days As Integer) As Boolean

        Dim day_end As Integer
        Dim month_end As Integer
        Dim year_end As Integer

        Dim day_start As Integer
        Dim month_start As Integer
        Dim year_start As Integer

        day_start = dates(0)
        day_end = dates(1)
        month_start = dates(2)
        month_end = dates(3)
        year_start = dates(4)
        year_end = dates(5)

        If (day_start <> day_end Or month_end <> month_start) And days = 1 Then
            Return False
        End If

        Return True

    End Function

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' refresh the form 9 display / Fill the Cycle time / Charting
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    Private Sub refresh_Report_BarChart(ByRef big_periode_returned As periode(,), ByRef periode_ As periode(), indx As Integer)
        Dim Text As String() = Split(CB_Report.Text, " : ")
        Text(0) = CSI_LIB.RenameMachine(Text(0))
        Dim i As Integer

        Dim machine As String
        Dim date_ As String

        Dim first_ As Boolean = True
        Dim day_end As Integer
        Dim month_end As Integer
        Dim year_end As Integer

        Dim day_start As Integer
        Dim month_start As Integer
        Dim year_start As Integer

        Dim total1 As Double 'Integer
        '  Dim j As Integer
        Try
            If consolidated = False Then
                For i = 0 To UBound(periode_) - 1

                    machine = periode_(i).machine_name
                    date_ = periode_(i).date_

                    If machine = Text(0) And date_ = Text(1) Then
                        '
                        If BTN_Unit.Text = "h" Then
                            Call Config_report.fill_Report_BarChart_shift(periode_, i)
                        Else
                            Call Config_report.fill_Report_BarChart_shift_hours(periode_, i)
                        End If


                        Config_report.machine_Report_BarChart = i
                        indx = i
                        GoTo done_
                    End If
                Next
            Else
                If BTN_Unit.Text = "h" Then
                    Call Config_report.fill_Report_BarChart_shift(periode_, 0)
                Else
                    Call Config_report.fill_Report_BarChart_shift_hours(periode_, 0)
                End If
            End If
done_:
        Catch ex As Exception
            MessageBox.Show("Consolidation Error")
        End Try


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' periode array '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim con_time, coff_time, other_time, setup_time As Double


        If (periode_(indx).shift1.ContainsKey("CYCLE ON")) Then
            con_time = periode_(indx).shift1.Item("CYCLE ON")
        End If
        If (periode_(indx).shift2.ContainsKey("CYCLE ON")) Then
            con_time += periode_(indx).shift2.Item("CYCLE ON")
        End If
        If (periode_(indx).shift3.ContainsKey("CYCLE ON")) Then
            con_time += periode_(indx).shift3.Item("CYCLE ON")
        End If

        If (periode_(indx).shift1.ContainsKey("CYCLE OFF")) Then
            coff_time = periode_(indx).shift1.Item("CYCLE OFF")
        End If
        If (periode_(indx).shift2.ContainsKey("CYCLE OFF")) Then
            coff_time += periode_(indx).shift2.Item("CYCLE OFF")
        End If
        If (periode_(indx).shift3.ContainsKey("CYCLE OFF")) Then
            coff_time += periode_(indx).shift3.Item("CYCLE OFF")
        End If


        If (periode_(indx).shift1.ContainsKey("SETUP")) Then
            setup_time = periode_(indx).shift1.Item("SETUP")
        End If
        If (periode_(indx).shift2.ContainsKey("SETUP")) Then
            setup_time += periode_(indx).shift2.Item("SETUP")
        End If
        If (periode_(indx).shift3.ContainsKey("SETUP")) Then
            setup_time += periode_(indx).shift3.Item("SETUP")
        End If

        For Each key In (periode_(indx).shift1.Keys)
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                other_time += periode_(indx).shift1.Item(key)
            End If
        Next
        For Each key In (periode_(indx).shift2.Keys)
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                other_time += periode_(indx).shift2.Item(key)
            End If
        Next
        For Each key In (periode_(indx).shift3.Keys)
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                other_time += periode_(indx).shift3.Item(key)
            End If
        Next

        Try
            Dim total As Double = other_time + setup_time + coff_time + con_time

            If (Welcome.CSIF_version <> 1) Then 'std
                If total <> 0 Then

                    If BTN_Unit.Text = "h" Then
                        LBL_CycleOn_Period.Text = ((con_time) * 100 / total).ToString("00.00") 'Math.Round((con_time) * 100 / total).ToString("00.00")
                        LBL_CycleOff_Period.Text = ((coff_time) * 100 / total).ToString("00.00") 'Math.Round((coff_time) * 100 / total).ToString("00.00")
                        LBL_Setup_Period.Text = ((setup_time) * 100 / total).ToString("00.00") 'Math.Round((setup_time) * 100 / total).ToString("00.00")
                        LBL_Other_Period.Text = ((other_time) * 100 / total).ToString("00.00") 'Math.Round((other_time) * 100 / total).ToString("00.00")
                        LBL_Total_Period.Text = ((con_time + coff_time + setup_time + other_time) * 100 / total).ToString("00.00") '"100.00"
                    Else
                        'LBL_CycleOn_Period.Text = ((con_time)).ToString("00.00")
                        'LBL_CycleOff_Period.Text = ((coff_time)).ToString("00.00")
                        'LBL_Setup_Period.Text = ((setup_time)).ToString("00.00")
                        'LBL_Other_Period.Text = ((other_time)).ToString("00.00")
                        'LBL_Total_Period.Text = ((con_time + coff_time + setup_time + other_time)).ToString("00.00")

                        LBL_CycleOn_Period.Text = Config_report.uptimeToDHMS(con_time)
                        LBL_CycleOff_Period.Text = Config_report.uptimeToDHMS(coff_time)
                        LBL_Setup_Period.Text = Config_report.uptimeToDHMS(setup_time)
                        LBL_Other_Period.Text = Config_report.uptimeToDHMS(other_time)
                        LBL_Total_Period.Text = Config_report.uptimeToDHMS(con_time + coff_time + setup_time + other_time)


                    End If


                Else
                    LBL_CycleOn_Period.Text = ("00.00")
                    LBL_CycleOff_Period.Text = ("00.00")
                    LBL_Setup_Period.Text = ("00.00")
                    LBL_Other_Period.Text = ("00.00")
                    LBL_Total_Period.Text = ("00.00")
                End If

                ' Pie CHart ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                data(0) = Val(LBL_CycleOn_Period.Text)
                data(1) = Val(LBL_CycleOff_Period.Text)
                data(2) = Val(LBL_Setup_Period.Text)
                data(3) = Val(LBL_Other_Period.Text)
                Chart1.Series("Series1").Points.DataBindY(data)


                Chart1.Series("Series1").Points.Item(0).ToolTip = "Cycle ON : " + Config_report.uptimeToDHMS(con_time).ToString()
                Chart1.Series("Series1").Points.Item(1).ToolTip = "Cycle Off : " + Config_report.uptimeToDHMS(coff_time).ToString()
                Chart1.Series("Series1").Points.Item(2).ToolTip = "Setup : " + Config_report.uptimeToDHMS(setup_time).ToString()
                Chart1.Series("Series1").Points.Item(3).ToolTip = "Other causes of downtime : " + Config_report.uptimeToDHMS(other_time).ToString()


                'If Welcome.CSIF_version = 1 Then
                '    Chart1.Series("Series1").Points.Item(2).Color = Color.Transparent
                '    Chart1.Series("Series1").Points.Item(3).Color = Color.Transparent
                'End If

                total1 = total
            Else 'lite
                coff_time = coff_time + setup_time + other_time
                If total <> 0 Then
                    LBL_CycleOn_Period.Text = ((con_time) * 100 / total).ToString("00.00") 'Math.Round((con_time) * 100 / total).ToString("00.00")
                    LBL_CycleOff_Period.Text = ((coff_time) * 100 / total).ToString("00.00") 'Math.Round((coff_time) * 100 / total).ToString("00.00")
                    LBL_Setup_Period.Text = ("00.00") 'Math.Round((setup_time) * 100 / total).ToString("00.00")
                    LBL_Other_Period.Text = ("00.00") 'Math.Round((other_time) * 100 / total).ToString("00.00")
                    LBL_Total_Period.Text = ((con_time + coff_time) * 100 / total).ToString("00.00") '"100.00"
                Else
                    LBL_CycleOn_Period.Text = ("00.00")
                    LBL_CycleOff_Period.Text = ("00.00")
                    LBL_Setup_Period.Text = ("00.00")
                    LBL_Other_Period.Text = ("00.00")
                    LBL_Total_Period.Text = ("00.00")
                End If

                ' Pie CHart ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                data(0) = Val(LBL_CycleOn_Period.Text)
                data(1) = Val(LBL_CycleOff_Period.Text)
                data(2) = Val(LBL_Setup_Period.Text)
                data(3) = Val(LBL_Other_Period.Text)
                Chart1.Series("Series1").Points.DataBindY(data)
                'If Welcome.CSIF_version = 1 Then
                '    Chart1.Series("Series1").Points.Item(2).Color = Color.Transparent
                '    Chart1.Series("Series1").Points.Item(3).Color = Color.Transparent
                'End If
                total1 = total
            End If

        Catch ex As Exception
            MessageBox.Show("Could not display the selected cycle times") ' & ex.Message)
        End Try

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Big Chart ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Try
            year_start = Config_report.DTP_StartDate.Value.Year
            year_end = Config_report.DTP_EndDate.Value.Year
            month_start = Config_report.DTP_StartDate.Value.Month
            month_end = Config_report.DTP_EndDate.Value.Month
            day_start = Config_report.DTP_StartDate.Value.Day
            day_end = Config_report.DTP_EndDate.Value.Day

            Chart2.Series("Series1").SmartLabelStyle.Enabled = False
            Dim dates_1 As String()
            Dim days_ As Integer = Config_report.days



            If (Welcome.CSIF_version <> 1) Then 'std
                'chart 2 - Serie1 = CYCLE ON
                For i = 1 To 89 'doit verifier si CYCLE ON existe dedans
                    dates_1 = Split(big_periode_returned(indx, i).date_)
                    If checkValidDate(dates_1, days_) Then
                        data2(89 - i) = big_periode_returned(indx, i).shift1.Item("CYCLE ON")
                    Else
                        data2(89 - i) = 0
                    End If
                Next
                Chart2.Series("Series1").Points.DataBindY(data2)

                'Dim i_ As Integer = 0
                'For i_ = 0 To 89
                '    data99(i_) = data2(i_) * 100 / data2.Max
                'Next
                'Try


                '    Chart2.Series("Series5").Points.DataBindY(data99)
                '    Dim typeRegression As String = "Linear"
                '    '"Exponential";//
                '    ' The number of days for Forecasting
                '    Dim forecasting As String = "1"
                '    ' Show Error as a range chart.
                '    Dim [error] As String = "false"
                '    ' Show Forecasting Error as a range chart.
                '    Dim forecastingError As String = "false"
                '    ' Formula parameters
                '    Dim parameters As String = typeRegression + "," + forecasting + "," + [error] + "," + forecastingError
                '    'Chart2.Series("Series5").Sort(PointSortOrder.Ascending, "Y")
                '    ' Create Forecasting Series.
                '    Chart2.Series("Series5").Sort(PointSortOrder.Ascending, "X")

                '    Chart2.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, parameters, Chart2.Series("Series5"), Chart2.Series("Series5"))
                '    Chart2.Series("Series5").Points.RemoveAt(90)
                '    'Dim mean_ As Integer = (Chart2.Series("Series5").Points(0).YValues(0) + Chart2.Series("Series5").Points(89).YValues(0)) / 2
                '    'If mean_ > 50 Then
                '    '    For Each point As DataPoint In Chart2.Series("Series5").Points
                '    '        point.YValues(0) = point.YValues(0) - (50 - mean_)
                '    '    Next
                '    'Else
                '    '    For Each point As DataPoint In Chart2.Series("Series5").Points
                '    '        point.YValues(0) = point.YValues(0) + (50 - mean_)
                '    '    Next
                '    'End If

                'Catch ex As Exception

                'End Try



                'chart 2 - Serie2 = CYCLE OFF
                For i = 1 To 89
                    dates_1 = Split(big_periode_returned(indx, i).date_)
                    If checkValidDate(dates_1, days_) Then
                        data3(89 - i) = big_periode_returned(indx, i).shift1.Item("CYCLE OFF")
                    Else
                        data3(89 - i) = 0
                    End If
                Next
                Chart2.Series("Series2").Points.DataBindY(data3)

                'chart 2 - Serie3 = Setup
                For i = 1 To 89
                    dates_1 = Split(big_periode_returned(indx, i).date_)
                    If checkValidDate(dates_1, days_) Then
                        data4(89 - i) = big_periode_returned(indx, i).shift1.Item("SETUP")
                    Else
                        data4(89 - i) = 0
                    End If

                Next
                Chart2.Series("Series3").Points.DataBindY(data4)

                'chart 2 - Serie4 = Other
                For i = 1 To 89
                    dates_1 = Split(big_periode_returned(indx, i).date_)
                    If checkValidDate(dates_1, days_) Then
                        data5(89 - i) = big_periode_returned(indx, i).shift1.Item("OTHER")
                    Else
                        data5(89 - i) = 0
                    End If
                Next
                Chart2.Series("Series4").Points.DataBindY(data5)
            Else 'lite
                'chart 2 - Serie1 = CYCLE ON
                For i = 1 To 89 'doit verifier si CYCLE ON existe dedans
                    dates_1 = Split(big_periode_returned(indx, i).date_)
                    If checkValidDate(dates_1, days_) Then
                        data2(89 - i) = big_periode_returned(indx, i).shift1.Item("CYCLE ON")
                    End If
                Next
                Chart2.Series("Series1").Points.DataBindY(data2)



                'Dim i_ As Integer = 0
                'For i_ = 0 To 89
                '    data99(i_) = data2(i_) * 100 / data2.Max
                'Next
                'Chart2.Series("Series5").Points.DataBindY(data99)

                Dim typeRegression As String = "Linear"
                '"Exponential";//
                ' The number of days for Forecasting
                Dim forecasting As String = "1"
                ' Show Error as a range chart.
                Dim [error] As String = "false"
                ' Show Forecasting Error as a range chart.
                Dim forecastingError As String = "false"
                ' Formula parameters
                Dim parameters As String = typeRegression + "," + forecasting + "," + [error] + "," + forecastingError
                Chart2.Series("Series5").Sort(PointSortOrder.Ascending, "X")
                ' Create Forecasting Series.
                Chart2.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, parameters, Chart2.Series(0), Chart2.Series("TrendLine"))

                '=======================================================
                'Service provided by Telerik (www.telerik.com)
                'Conversion powered by NRefactory.
                'Twitter: @telerik
                'Facebook: facebook.com/telerik
                '=======================================================




                'chart 2 - Serie2 = CYCLE OFF
                For i = 1 To 89
                    dates_1 = Split(big_periode_returned(indx, i).date_)
                    If checkValidDate(dates_1, days_) Then
                        data3(89 - i) = big_periode_returned(indx, i).shift1.Item("CYCLE OFF") + big_periode_returned(indx, i).shift1.Item("SETUP") + big_periode_returned(indx, i).shift1.Item("OTHER")
                    End If
                Next
                Chart2.Series("Series2").Points.DataBindY(data3)



                'chart 2 - Serie3 = Setup
                For i = 1 To 89
                    dates_1 = Split(big_periode_returned(indx, i).date_)
                    If checkValidDate(dates_1, days_) Then
                        data4(89 - i) = 0 'big_periode_returned(indx, i).shift1.Item("SETUP")
                    End If
                Next
                Chart2.Series("Series3").Points.DataBindY(data4)

                'chart 2 - Serie4 = Other
                For i = 1 To 89
                    dates_1 = Split(big_periode_returned(indx, i).date_)
                    If checkValidDate(dates_1, days_) Then
                        data5(89 - i) = 0 'big_periode_returned(indx, i).shift1.Item("OTHER")
                    End If
                Next
                Chart2.Series("Series4").Points.DataBindY(data5)
            End If



            'Dim dataR As Integer() = CSI_LIB.Effectue_Regression(data2, data3, data4, data5)
            'Chart3.Series.Add("Trend")
            'Chart3.Series("Trend").ChartType = SeriesChartType.FastLine
            'Chart3.Series("Trend").Points.DataBindY(dataR)
            Dim dates_ As String()

            Chart2.Series("Series1").Points(89).AxisLabel = " "

            For i = 1 To 89
                dates_ = Split(big_periode_returned(indx, i).date_)
                day_start = dates_(0)
                day_end = dates_(1)
                month_start = dates_(2)
                month_end = dates_(3)
                year_start = dates_(4)
                year_end = dates_(5)

                If checkValidDate(dates_, days_) Then

                    If day_start = day_end And month_end = month_start Then
                        Chart2.Series("Series1").Points(89 - i).AxisLabel = day_end.ToString() & " " & SetupForm.mois_(month_end) & " " & year_end.ToString()
                    Else
                        Chart2.Series("Series1").Points(89 - i).AxisLabel = "From " & day_start.ToString() & " " & SetupForm.mois_(month_start) & " To " & day_end.ToString() & " " & SetupForm.mois_(month_end) & " " & year_end.ToString()
                    End If

                    Try


                        Dim alpha As Integer = 255
                        Dim backcolors As Color
                        Dim backcolors2 As Color
                        Dim backcolors3 As Color

                        Dim colors As Dictionary(Of String, Integer)
                        colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)

                        backcolors = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE ON"))
                        backcolors = Color.FromArgb(alpha, backcolors.R, backcolors.G, backcolors.B)

                        backcolors2 = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE OFF"))
                        backcolors2 = Color.FromArgb(alpha, backcolors2.R, backcolors2.G, backcolors2.B)

                        backcolors3 = System.Drawing.ColorTranslator.FromWin32(colors("SETUP"))
                        backcolors3 = Color.FromArgb(alpha, backcolors3.R, backcolors3.G, backcolors3.B)

                        ' Transparency / tooltips , the date is in Chart2.Series("Series1").Points(89 - i).AxisLabel only
                        Chart2.Series("Series1").Points(89 - i).Color = backcolors 'Color.FromArgb(228, Chart2.Series("Series1").Points(89 - i).Color)
                        Chart2.Series("Series1").Points(89 - i).ToolTip = Chart2.Series("Series1").Points(89 - i).AxisLabel & " " & Config_report.uptimeToDHMS(Chart2.Series("Series1").Points(89 - i).YValues(0))
                        Chart2.Series("Series2").Points(89 - i).Color = backcolors2 'Color.FromArgb(228, Chart2.Series("Series2").Points(89 - i).Color)
                        Chart2.Series("Series2").Points(89 - i).ToolTip = Chart2.Series("Series2").Points(89 - i).AxisLabel & " " & Config_report.uptimeToDHMS(Chart2.Series("Series2").Points(89 - i).YValues(0))
                        Chart2.Series("Series3").Points(89 - i).Color = backcolors3 'Color.FromArgb(228, Chart2.Series("Series3").Points(89 - i).Color)
                        Chart2.Series("Series3").Points(89 - i).ToolTip = Chart2.Series("Series3").Points(89 - i).AxisLabel & " " & Config_report.uptimeToDHMS(Chart2.Series("Series3").Points(89 - i).YValues(0))
                        Chart2.Series("Series4").Points(89 - i).Color = Other_color
                        Chart2.Series("Series4").Points(89 - i).ToolTip = Chart2.Series("Series4").Points(89 - i).AxisLabel & "  " & Config_report.uptimeToDHMS(Chart2.Series("Series4").Points(89 - i).YValues(0))

                        Chart1.Series("Series1").Points.Item(0).Color = backcolors
                        Chart1.Series("Series1").Points.Item(1).Color = backcolors2
                        Chart1.Series("Series1").Points.Item(2).Color = backcolors3
                        Chart1.Series("Series1").Points.Item(3).Color = Other_color

                        'Chart2.Series("Series1").Color = backcolors
                        'Chart2.Series("Series2").Color = backcolors2
                        'Chart2.Series("Series3").Color = backcolors3
                        'Chart2.Series("Series4").Color = Color.Yellow

                    Catch ex As Exception
                        Chart2.Series("Series1").Points(89 - i).Color = Color.FromArgb(228, Chart2.Series("Series1").Points(89 - i).Color)
                        Chart2.Series("Series1").Points(89 - i).ToolTip = Chart2.Series("Series1").Points(89 - i).AxisLabel & " " & Config_report.uptimeToDHMS(Chart2.Series("Series1").Points(89 - i).YValues(0))
                        Chart2.Series("Series2").Points(89 - i).Color = Color.FromArgb(228, Chart2.Series("Series2").Points(89 - i).Color)
                        Chart2.Series("Series2").Points(89 - i).ToolTip = Chart2.Series("Series1").Points(89 - i).AxisLabel & " " & Config_report.uptimeToDHMS(Chart2.Series("Series2").Points(89 - i).YValues(0))
                        Chart2.Series("Series3").Points(89 - i).Color = Color.FromArgb(228, Chart2.Series("Series3").Points(89 - i).Color)
                        Chart2.Series("Series3").Points(89 - i).ToolTip = Chart2.Series("Series1").Points(89 - i).AxisLabel & " " & Config_report.uptimeToDHMS(Chart2.Series("Series3").Points(89 - i).YValues(0))
                        Chart2.Series("Series4").Points(89 - i).Color = Color.FromArgb(228, Chart2.Series("Series4").Points(89 - i).Color)
                        Chart2.Series("Series4").Points(89 - i).ToolTip = Chart2.Series("Series1").Points(89 - i).AxisLabel & "  " & Config_report.uptimeToDHMS(Chart2.Series("Series4").Points(89 - i).YValues(0))
                    End Try
                Else
                    Chart2.Series("Series1").Points(89 - i).AxisLabel = day_end.ToString() & " " & SetupForm.mois_(month_end) & " " & year_end.ToString()
                    '  Chart2.Series("Series1").Points(89 - i).AxisLabel = "From " & day_start.ToString() & " " & SetupForm.mois_(month_start) & " To " & day_end.ToString() & " " & SetupForm.mois_(month_end) & " " & year_end.ToString()
                End If
            Next





            Dim tmptotal As Double = (big_periode_returned(indx, 0).shift1.Item("SETUP") + big_periode_returned(indx, 0).shift1.Item("OTHER")) * 100
            big_periode_returned(indx, 0).shift1.Item("OTHER") = (big_periode_returned(indx, 0).shift1.Item("OTHER") + big_periode_returned(indx, 0).shift1.Item("SETUP"))
            big_periode_returned(indx, 0).shift1.Item("SETUP") = 0

            If Welcome.CSIF_version = 1 Then
                'Label3.BackColor = Color.Transparent 'setup
                'Label3.ForeColor = Color.Black 'setup text
                'Label7.BackColor = Color.Transparent 'other

                'Label26.BackColor = Color.Transparent 'setup
                'Label22.BackColor = Color.Transparent 'other

                'Label39.BackColor = Color.Transparent 'setup
                'Label32.BackColor = Color.Transparent 'other

                'Label49.BackColor = Color.Transparent 'setup
                'Label45.BackColor = Color.Transparent 'other

                'Label61.BackColor = Color.Transparent 'setup
                'Label55.BackColor = Color.Transparent 'other

                'Chart1.Palette 
                Chart2.Series("Series3").Color = Color.Transparent
                Chart2.Series("Series4").Color = Color.Transparent

                'Label26.Text = Math.Round((big_periode_returned(indx, 0).shift1.Item("SETUP")) * 100 / total).ToString("00.00")
                'If total1 = 0 Then
                LBL_Other_Period.Text = "" '("00.00")
                'Else
                'commented because it is included in CYCLE OFF
                'Label22.Text = Math.Round(tmptotal / total1).ToString("00.00")
                'End If

                LBL_Setup_Period.Text = ""
                LBL_Setup_Shift1.Text = ""
                LBL_Setup_Shift2.Text = ""
                LBL_Setup_Shift3.Text = ""
                LBL_Other_Shift1.Text = ""
                LBL_Other_Shift2.Text = ""
                LBL_Other_Shift3.Text = ""
                Label5.Text = ""
                Label6.Text = ""
            Else
                'Label3.BackColor = Color.Transparent 'setup
                Label7.BackColor = Other_color

                LBL_Other_Period.BackColor = Other_color
                LBL_Other_Shift1.BackColor = Other_color
                LBL_Other_Shift2.BackColor = Other_color
                LBL_Other_Shift3.BackColor = Other_color

                'Label26.BackColor = Color.Transparent 'setup
                'Label22.BackColor = Color.Transparent 'other

                'Label39.BackColor = Color.Transparent 'setup
                'Label32.BackColor = Color.Transparent 'other

                'Label49.BackColor = Color.Transparent 'setup
                'Label45.BackColor = Color.Transparent 'other

                'Label61.BackColor = Color.Transparent 'setup
                'Label55.BackColor = Color.Transparent 'other

                'Chart1.Palette 
                'Chart2.Series("Series3").Color = Color.Transparent
                'Chart2.Series("Series4").Color = Color.Transparent

                'Label26.Text = Math.Round((big_periode_returned(indx, 0).shift1.Item("SETUP")) * 100 / total).ToString("00.00")
                '''''''''''''''''''I DONT KNOW WHY THIS IS HERE
                'If total1 = 0 Then
                '    Label22.Text = ("00.00")
                'Else
                '    'commented because it is included in CYCLE OFF
                '    Label22.Text = (tmptotal / total1).ToString("00.00") 'Math.Round(tmptotal / total1).ToString("00.00")
                'End If
                ''''''''''''''''''''''''''''

                'Label26.Text = ""
                'Label39.Text = ""



                'Label5.Text = ""
                'Label6.Text = ""
            End If

        Catch ex As Exception
            MessageBox.Show("Could Not display the Evolution chart") ' & ex.Message)
        End Try


        'Chart2.Series("Series1").Color = Color.Green
        'Chart2.Series("Series2").Color = Color.Red
        'Chart2.Series("Series3").Color = Color.Blue
        'Chart2.Series("Series4").Color = Color.Yellow

        'Chart2.Series("Series1").Points(89).Color = Color.Green
        Chart2.Series("Series5").BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash

    End Sub






    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Next
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub BTN_Next_Click(sender As Object, e As EventArgs) Handles BTN_Next.Click

        'If BTN_Unit.Text = "%" Then
        '    BTN_Unit.Text = "h"
        'End If

        Dim index_ As Integer
        index_ = CB_Report.SelectedIndex
        If index_ = CB_Report.Items.Count - 1 Then index_ = -1
        CB_Report.SelectedIndex = index_ + 1

    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Prev
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub BTN_Prev_Click(sender As Object, e As EventArgs) Handles BTN_Prev.Click

        'If BTN_Unit.Text = "%" Then
        '    BTN_Unit.Text = "h"
        'End If

        Dim index_ As Integer
        index_ = CB_Report.SelectedIndex
        If index_ = 0 Then index_ = CB_Report.Items.Count
        CB_Report.SelectedIndex = index_ - 1
    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Evolution Chart = Splines
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Chart2.Series("Series1").ChartType = SeriesChartType.Spline
        Chart2.Series("Series2").ChartType = SeriesChartType.Spline
        Chart2.Series("Series3").ChartType = SeriesChartType.Spline
        Chart2.Series("Series4").ChartType = SeriesChartType.Spline
    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Evolution Chart = lines
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button3_Click_1(sender As Object, e As EventArgs)
        'Chart2.Series("Series1").ChartType = SeriesChartType.Line
        'Chart2.Series("Series2").hartType = SeriesChartType.Line
        'Chart2.Series("Series3").ChartType = SeriesChartType.Line
        'Chart2.Series("Series4").ChartType = SeriesChartType.Line
    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Evolution Chart = StackedColumn
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button5_Click(sender As Object, e As EventArgs)
        Chart2.Series("Series1").ChartType = SeriesChartType.StackedColumn100
        Chart2.Series("Series2").ChartType = SeriesChartType.StackedColumn100
        Chart2.Series("Series3").ChartType = SeriesChartType.StackedColumn100
        Chart2.Series("Series4").ChartType = SeriesChartType.StackedColumn100
    End Sub


    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Chart = 3D 
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    Private Sub BTN_3D_Click(sender As Object, e As EventArgs) Handles BTN_3D.Click
        If Chart2.ChartAreas(0).Area3DStyle.Enable3D = True Then
            Chart2.ChartAreas(0).Area3DStyle.Enable3D = False
            BTN_3D.ForeColor = Color.Black

            BTN_3D.Text = "3D"


        Else
            Chart2.ChartAreas(0).Area3DStyle.Enable3D = True
            BTN_3D.ForeColor = Color.Red

            BTN_3D.Text = "2D"

        End If
    End Sub



    'Public Xchartwaarde As Integer
    'Public Ychartwaarde As Integer

    'Private Sub Chart2_MouseMove(sender As Object, e As MouseEventArgs)

    '    Dim result As HitTestResult = Chart2.HitTest(e.X, e.Y)

    '    If result.PointIndex >= 0 Then
    '        Dim dp As DataPoint = Chart2.Series(0).Points(result.PointIndex)

    '        Xchartwaarde = dp.XValue
    '        Ychartwaarde = dp.YValues(0)
    '    End If
    'End Sub



    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Evolution Chart = step
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Chart2.Series("Series1").ChartType = SeriesChartType.StepLine
        Chart2.Series("Series2").ChartType = SeriesChartType.StepLine
        Chart2.Series("Series3").ChartType = SeriesChartType.StepLine
        Chart2.Series("Series4").ChartType = SeriesChartType.StepLine
    End Sub



    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' move 3d chart
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub chart2_MouseMove(sender As Object, e As MouseEventArgs) Handles Chart2.MouseMove


        If (e.Button = System.Windows.Forms.MouseButtons.Left) Then

            If (mousePoint = Nothing) Then
                mousePoint.X = e.Location.X
                mousePoint.Y = e.Location.Y
            Else

                newy = Chart2.ChartAreas(0).Area3DStyle.Rotation + ((e.Location.X - mousePoint.X))
                If (newy < -75) Then newy = -80
                If (newy > 75) Then newy = 80

                Chart2.ChartAreas(0).Area3DStyle.Rotation = newy

                newy = Chart2.ChartAreas(0).Area3DStyle.Inclination + ((e.Location.Y - mousePoint.Y))
                If (newy < -90) Then newy = -89
                If (newy > 90) Then newy = 89

                Chart2.ChartAreas(0).Area3DStyle.Inclination = newy

                mousePoint.X = e.Location.X
                mousePoint.Y = e.Location.Y
            End If
        End If

    End Sub


    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Zoom 6 month
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub BTN_Zoom6M_Click(sender As Object, e As EventArgs) Handles BTN_Zoom6M.Click

        If Config_report.days = 1 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(Config_report.DTP_EndDate.Value.Day + 0.5, 89.5)
        Else
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(88 - 6 * Math.Round(Config_report.DTP_EndDate.Value.Day / 7) + 0.5, 89.5)
        End If

    End Sub


    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Zoom 3 month
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub BTN_Zoom3M_Click(sender As Object, e As EventArgs) Handles BTN_Zoom3M.Click

        If Config_report.days = 1 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(29 - Config_report.DTP_EndDate.Value.Day + 0.5, 89.5)
        Else
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(88 - 3 * Math.Round(Config_report.DTP_EndDate.Value.Day / 7) + 0.5, 89.5)
        End If

    End Sub


    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Zoom 1 month
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub BTN_Zoom1M_Click(sender As Object, e As EventArgs) Handles BTN_Zoom1M.Click

        If Config_report.days = 1 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(88 - Config_report.DTP_EndDate.Value.Day + 0.5, 89.5)
        Else
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(88 - Math.Round(Config_report.DTP_EndDate.Value.Day / 7) + 0.5, 89.5)
        End If
    End Sub



    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Zoom 1 week
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub BTN_Zoom1W_Click(sender As Object, e As EventArgs) Handles BTN_Zoom1W.Click
        If Config_report.days = 1 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((88 + Val(Reporting_application.week_(0)) - Config_report.DTP_EndDate.Value.DayOfWeek) + 0.5, 89.5)
        Else
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(88.5, 89.5)
        End If
    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Reset view
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub BTN_ResetView_Click(sender As Object, e As EventArgs) Handles BTN_ResetView.Click
        ' 1Day
        Config_report.BTN_Create.PerformClick()
        'If DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) = 0 Then
        '    If Config_report.DTP_StartDate.Value.DayOfWeek > 1 Then
        '        Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Reporting_application.week_(0)) - Config_report.DTP_StartDate.Value.DayOfWeek) + 0.5, 89.5)
        '    Else
        '        Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Reporting_application.week_(0)) - Config_report.DTP_StartDate.Value.DayOfWeek) - 0.5 - 7 + Val(Reporting_application.week_(0)), 89.5)
        '    End If
        'End If

        '' +1Day AND < 28 DAYS
        'If DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) < 30 And DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) <> 0 Then
        '    Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) - 0.5), 89.5)
        'End If



        'If DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) > 29 Then
        '    Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - Math.Round(DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) / 7) - 1) + 0.5, 89.5)
        'End If
    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Data by weeks
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub BTN_Week_Click(sender As Object, e As EventArgs) Handles BTN_Week.Click
        Config_report.days = 6
        Dim actual_index As Integer = CB_Report.SelectedIndex
        ' Dim scale_view As AxisScaleView = Chart2.ChartAreas(0).AxisX.ScaleView

        Config_report.periode_returned = CSI_LIB.Detailled(Config_report.DTP_StartDate.Value, Config_report.DTP_EndDate.Value, Config_report.read_tree(), True)
        Config_report.big_periode_returned = CSI_LIB.Evolution(Config_report.DTP_StartDate.Value, Config_report.DTP_EndDate.Value, Config_report.read_tree(), Config_report.days, True)
        Call Config_report.Fill_Combo_Report_BarChart(Config_report.periode_returned)

        CB_Report.SelectedIndex = CB_Report.Items.Count - 1

        'CHART ZOOM :----------------------------------------------------------------------------------------------

        ' 1Day
        If DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) = 0 Then
            If Config_report.DTP_StartDate.Value.DayOfWeek > 1 Then
                Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Reporting_application.week_(0)) - Config_report.DTP_StartDate.Value.DayOfWeek) + 0.5, 89.5)
            Else
                Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Reporting_application.week_(0)) - Config_report.DTP_StartDate.Value.DayOfWeek) - 0.5 - 7 + Val(Reporting_application.week_(0)), 89.5)
            End If
        End If

        ' +1Day AND < 28 DAYS
        If DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) < 30 And DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) <> 0 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) - 0.5), 89.5)
        End If

        ' +28 DAYS
        If DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) > 29 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - Math.Round(DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) / 7) - 1) + 0.5, 89.5)
        End If
        CB_Report.SelectedIndex = actual_index
        '  Chart2.ChartAreas(0).AxisX.ScaleView = scale_view
    End Sub


    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Data by days
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub BTN_Day_Click(sender As Object, e As EventArgs) Handles BTN_Day.Click
        Config_report.days = 1
        Dim actual_index As Integer = CB_Report.SelectedIndex
        ' Dim scale_view As AxisScaleView = Chart2.ChartAreas(0).AxisX.ScaleView

        Config_report.periode_returned = CSI_LIB.Detailled(Config_report.DTP_StartDate.Value, Config_report.DTP_EndDate.Value, Config_report.read_tree(), True)
        Config_report.big_periode_returned = CSI_LIB.Evolution(Config_report.DTP_StartDate.Value, Config_report.DTP_EndDate.Value, Config_report.read_tree(), Config_report.days, True)
        Call Config_report.Fill_Combo_Report_BarChart(Config_report.periode_returned)
        CB_Report.SelectedIndex = CB_Report.Items.Count - 1

        'CHART ZOOM :----------------------------------------------------------------------------------------------

        ' 1Day
        If DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) = 0 Then
            If Config_report.DTP_StartDate.Value.DayOfWeek > 1 Then
                Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Reporting_application.week_(0)) - Config_report.DTP_StartDate.Value.DayOfWeek) + 0.5, 89.5)
            Else
                Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Reporting_application.week_(0)) - Config_report.DTP_StartDate.Value.DayOfWeek) - 0.5 - 7 + Val(Reporting_application.week_(0)), 89.5)
            End If
        End If

        ' +1Day AND < 28 DAYS
        If DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) < 30 And DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) <> 0 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) - 0.5), 89.5)
        End If

        ' +28 DAYS
        If DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date) > 29 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - Math.Round(DateDiff(DateInterval.Day, Config_report.DTP_StartDate.Value.Date, Config_report.DTP_EndDate.Value.Date)) - 1) + 0.5, 89.5)
        End If
        CB_Report.SelectedIndex = actual_index
        '  Chart2.ChartAreas(0).AxisX.ScaleView = scale_view
    End Sub




    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' H <-> %
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub BTN_Unit_Click(sender As Object, e As EventArgs) Handles BTN_Unit.Click

        If consolidated = False Then

            If BTN_Unit.Text = "%" Then
                BTN_Unit.Text = "h"

                Label30.Text = "Shift 1 (%)"
                Label43.Text = "Shift 2 (%)"
                Label53.Text = "Shift 3 (%)"
                Label20.Text = "Periode (%)"
                Call Config_report.fill_Report_BarChart_shift(Config_report.periode_returned, Config_report.machine_Report_BarChart)

                '1period
                Dim total As Double = Config_report.big_periode_returned(Config_report.machine_Report_BarChart, 0).shift1.Values.Sum()


                If total > 0 Then


                    Dim total_con As Double, total_coff As Double, total_setup As Double, total_other As Double

                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.ContainsKey("CYCLE ON") Then
                        total_con += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.Item("CYCLE ON")
                    End If
                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.ContainsKey("CYCLE ON") Then
                        total_con += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.Item("CYCLE ON")
                    End If
                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.ContainsKey("CYCLE ON") Then
                        total_con += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.Item("CYCLE ON")
                    End If

                    'total_coff = Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")
                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.ContainsKey("CYCLE OFF") Then
                        total_coff += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.Item("CYCLE OFF")
                    End If
                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.ContainsKey("CYCLE OFF") Then
                        total_coff += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.Item("CYCLE OFF")
                    End If
                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.ContainsKey("CYCLE OFF") Then
                        total_coff += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.Item("CYCLE OFF")
                    End If


                    If (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.ContainsKey("SETUP")) Then
                        total_setup += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.Item("SETUP")
                    End If
                    If (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.ContainsKey("SETUP")) Then
                        total_setup += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.Item("SETUP")
                    End If
                    If (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.ContainsKey("SETUP")) Then
                        total_setup += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.Item("SETUP")
                    End If

                    'total_other = Form3.periode_returned(Form3.machine_form9).shift1.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift2.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift3.Item("OTHER")
                    For Each key In (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.Item(key)
                        End If
                    Next
                    For Each key In (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.Item(key)
                        End If
                    Next
                    For Each key In (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.Item(key)
                        End If
                    Next

                    total = (total_con + total_coff + total_setup + total_other)

                    If (Welcome.CSIF_version <> 1) Then
                        LBL_CycleOn_Period.Text = (total_con * 100 / total).ToString("00.00")
                        LBL_CycleOff_Period.Text = (total_coff * 100 / total).ToString("00.00")
                        LBL_Setup_Period.Text = (total_setup * 100 / total).ToString("00.00")
                        LBL_Other_Period.Text = (total_other * 100 / total).ToString("00.00")
                        LBL_Total_Period.Text = ((total_con + total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                    Else
                        LBL_CycleOn_Period.Text = (total_con * 100 / total).ToString("00.00")
                        LBL_CycleOff_Period.Text = ((total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                        LBL_Setup_Period.Text = "" '("00.00")
                        LBL_Other_Period.Text = "" '("00.00")
                        LBL_Total_Period.Text = ((total_con + total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                    End If



                Else
                    LBL_CycleOn_Period.Text = ("00.00")
                    LBL_CycleOff_Period.Text = ("00.00")
                    LBL_Setup_Period.Text = ("00.00")
                    LBL_Other_Period.Text = ("00.00")
                    LBL_Total_Period.Text = ("00.00")
                End If
            Else

                BTN_Unit.Text = "%"
                Label30.Text = "Shift 1 (h)"
                Label43.Text = "Shift 2 (h)"
                Label53.Text = "Shift 3 (h)"
                Label20.Text = "Periode (h)"
                Call Config_report.fill_Report_BarChart_shift_hours(Config_report.periode_returned, Config_report.machine_Report_BarChart)

                Dim total As Double = Config_report.big_periode_returned(Config_report.machine_Report_BarChart, 0).shift1.Values.Sum()

                If total > 0 Then


                    ''''''''''''''''''''''''''''''
                    Dim total_con As Double, total_coff As Double, total_setup As Double, total_other As Double
                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.ContainsKey("CYCLE ON") Then
                        total_con += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.Item("CYCLE ON")
                    End If
                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.ContainsKey("CYCLE ON") Then
                        total_con += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.Item("CYCLE ON")
                    End If
                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.ContainsKey("CYCLE ON") Then
                        total_con += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.Item("CYCLE ON")
                    End If

                    'total_coff = Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")
                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.ContainsKey("CYCLE OFF") Then
                        total_coff += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.Item("CYCLE OFF")
                    End If
                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.ContainsKey("CYCLE OFF") Then
                        total_coff += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.Item("CYCLE OFF")
                    End If
                    If Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.ContainsKey("CYCLE OFF") Then
                        total_coff += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.Item("CYCLE OFF")
                    End If

                    If (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.ContainsKey("SETUP")) Then
                        total_setup += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.Item("SETUP")
                    End If
                    If (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.ContainsKey("SETUP")) Then
                        total_setup += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.Item("SETUP")
                    End If
                    If (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.ContainsKey("SETUP")) Then
                        total_setup += Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.Item("SETUP")
                    End If

                    'total_other = Form3.periode_returned(Form3.machine_form9).shift1.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift2.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift3.Item("OTHER")
                    For Each key In (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + Config_report.periode_returned(Config_report.machine_Report_BarChart).shift1.Item(key)
                        End If
                    Next
                    For Each key In (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + Config_report.periode_returned(Config_report.machine_Report_BarChart).shift2.Item(key)
                        End If
                    Next
                    For Each key In (Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + Config_report.periode_returned(Config_report.machine_Report_BarChart).shift3.Item(key)
                        End If
                    Next

                    If (Welcome.CSIF_version <> 1) Then
                        LBL_CycleOn_Period.Text = Config_report.uptimeToDHMS(total_con)
                        LBL_CycleOff_Period.Text = Config_report.uptimeToDHMS(total_coff)
                        LBL_Setup_Period.Text = Config_report.uptimeToDHMS(total_setup)
                        LBL_Other_Period.Text = Config_report.uptimeToDHMS(total_other)
                        LBL_Total_Period.Text = Config_report.uptimeToDHMS(total_con + total_coff + total_setup + total_other)
                    Else
                        LBL_CycleOn_Period.Text = Config_report.uptimeToDHMS(total_con)
                        LBL_CycleOff_Period.Text = Config_report.uptimeToDHMS(total_coff + total_setup + total_other)
                        LBL_Setup_Period.Text = "" '("00:00")
                        LBL_Other_Period.Text = "" '("00:00")
                        LBL_Total_Period.Text = Config_report.uptimeToDHMS(total_con + total_coff + total_setup + total_other)
                    End If

                Else
                    LBL_CycleOn_Period.Text = ("00:00")
                    LBL_CycleOff_Period.Text = ("00:00")
                    LBL_Setup_Period.Text = ("00:00")
                    LBL_Other_Period.Text = ("00:00")
                    LBL_Total_Period.Text = ("00:00")

                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''



            End If

        Else ' Consolidated


            '''''''''''''''''MODIFIED FROM NOT CONSOLIDATED

            If BTN_Unit.Text = "%" Then
                BTN_Unit.Text = "h"

                Label30.Text = "Shift 1 (%)"
                Label43.Text = "Shift 2 (%)"
                Label53.Text = "Shift 3 (%)"
                Label20.Text = "Periode (%)"
                'Call Form3.fill_form9_shift(Form3.periode_returned, Form3.machine_form9)
                Call Config_report.fill_Report_BarChart_shift(consolidated_periode, 0)



                Dim total As Double = consolidated_periode(0).shift1.Values.Sum()



                If total > 0 Then

                    Dim total_con As Double, total_coff As Double, total_setup As Double, total_other As Double
                    total_con = consolidated_periode(0).shift1.Item("CYCLE ON") + consolidated_periode(0).shift2.Item("CYCLE ON") + consolidated_periode(0).shift3.Item("CYCLE ON")
                    total_coff = consolidated_periode(0).shift1.Item("CYCLE OFF") + consolidated_periode(0).shift2.Item("CYCLE OFF") + consolidated_periode(0).shift3.Item("CYCLE OFF") 'Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")
                    total_setup = consolidated_periode(0).shift1.Item("SETUP") + consolidated_periode(0).shift2.Item("SETUP") + consolidated_periode(0).shift3.Item("SETUP") 'Form3.periode_returned(Form3.machine_form9).shift1.Item("SETUP") + Form3.periode_returned(Form3.machine_form9).shift2.Item("SETUP") + Form3.periode_returned(Form3.machine_form9).shift3.Item("SETUP")

                    For Each key In (consolidated_periode(0).shift1.Keys) '(Form3.periode_returned(Form3.machine_form9).shift1.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + consolidated_periode(0).shift1.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift1.Item(key)
                        End If
                    Next
                    For Each key In (consolidated_periode(0).shift2.Keys) '(Form3.periode_returned(Form3.machine_form9).shift2.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + consolidated_periode(0).shift2.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift2.Item(key)
                        End If
                    Next
                    For Each key In (consolidated_periode(0).shift3.Keys) '(Form3.periode_returned(Form3.machine_form9).shift3.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + consolidated_periode(0).shift3.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift3.Item(key)
                        End If
                    Next

                    total = (total_con + total_coff + total_setup + total_other)


                    If (Welcome.CSIF_version <> 1) Then
                        LBL_CycleOn_Period.Text = (total_con * 100 / total).ToString("00.00")
                        LBL_CycleOff_Period.Text = (total_coff * 100 / total).ToString("00.00")
                        LBL_Setup_Period.Text = (total_setup * 100 / total).ToString("00.00")
                        LBL_Other_Period.Text = (total_other * 100 / total).ToString("00.00")
                        LBL_Total_Period.Text = ((total_con + total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                    Else
                        LBL_CycleOn_Period.Text = (total_con * 100 / total).ToString("00.00")
                        LBL_CycleOff_Period.Text = ((total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                        LBL_Setup_Period.Text = "" '("00.00")
                        LBL_Other_Period.Text = "" '("00.00")
                        LBL_Total_Period.Text = ((total_con + total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                    End If



                Else
                    LBL_CycleOn_Period.Text = ("00.00")
                    LBL_CycleOff_Period.Text = ("00.00")
                    LBL_Setup_Period.Text = ("00.00")
                    LBL_Other_Period.Text = ("00.00")
                    LBL_Total_Period.Text = ("00.00")
                End If
            Else

                BTN_Unit.Text = "%"
                Label30.Text = "Shift 1 (h)"
                Label43.Text = "Shift 2 (h)"
                Label53.Text = "Shift 3 (h)"
                Label20.Text = "Periode (h)"

                'Call Form3.fill_form9_shift_hours(Form3.periode_returned, Form3.machine_form9)
                Call Config_report.fill_Report_BarChart_shift_hours(consolidated_periode, 0)


                Dim total As Double = consolidated_periode(0).shift1.Values.Sum()

                If total > 0 Then


                    ''''''''''''''''''''''''''''''
                    Dim total_con As Double, total_coff As Double, total_setup As Double, total_other As Double
                    total_con = consolidated_periode(0).shift1.Item("CYCLE ON") + consolidated_periode(0).shift2.Item("CYCLE ON") + consolidated_periode(0).shift3.Item("CYCLE ON")
                    total_coff = consolidated_periode(0).shift1.Item("CYCLE OFF") + consolidated_periode(0).shift2.Item("CYCLE OFF") + consolidated_periode(0).shift3.Item("CYCLE OFF") 'Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")
                    total_setup = consolidated_periode(0).shift1.Item("SETUP") + consolidated_periode(0).shift2.Item("SETUP") + consolidated_periode(0).shift3.Item("SETUP") 'Form3.periode_returned(Form3.machine_form9).shift1.Item("SETUP") + Form3.periode_returned(Form3.machine_form9).shift2.Item("SETUP") + Form3.periode_returned(Form3.machine_form9).shift3.Item("SETUP")
                    'total_other = Form3.periode_returned(Form3.machine_form9).shift1.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift2.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift3.Item("OTHER")

                    For Each key In (consolidated_periode(0).shift1.Keys) '(Form3.periode_returned(Form3.machine_form9).shift1.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + consolidated_periode(0).shift1.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift1.Item(key)
                        End If
                    Next
                    For Each key In (consolidated_periode(0).shift2.Keys) '(Form3.periode_returned(Form3.machine_form9).shift2.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + consolidated_periode(0).shift2.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift2.Item(key)
                        End If
                    Next
                    For Each key In (consolidated_periode(0).shift3.Keys) '(Form3.periode_returned(Form3.machine_form9).shift3.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                            total_other = total_other + consolidated_periode(0).shift3.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift3.Item(key)
                        End If
                    Next

                    If (Welcome.CSIF_version <> 1) Then
                        LBL_CycleOn_Period.Text = Config_report.uptimeToDHMS(total_con)
                        LBL_CycleOff_Period.Text = Config_report.uptimeToDHMS(total_coff)
                        LBL_Setup_Period.Text = Config_report.uptimeToDHMS(total_setup)
                        LBL_Other_Period.Text = Config_report.uptimeToDHMS(total_other)
                        LBL_Total_Period.Text = Config_report.uptimeToDHMS(total_con + total_coff + total_setup + total_other)

                    Else
                        LBL_CycleOn_Period.Text = Config_report.uptimeToDHMS(total_con)
                        LBL_CycleOff_Period.Text = Config_report.uptimeToDHMS(total_coff + total_setup + total_other)
                        LBL_Setup_Period.Text = "" '("00:00")
                        LBL_Other_Period.Text = "" '("00:00")
                        LBL_Total_Period.Text = Config_report.uptimeToDHMS(total_con + total_coff + total_setup + total_other)
                    End If

                Else
                    LBL_CycleOn_Period.Text = ("00:00")
                    LBL_CycleOff_Period.Text = ("00:00")
                    LBL_Setup_Period.Text = ("00:00")
                    LBL_Other_Period.Text = ("00:00")
                    LBL_Total_Period.Text = ("00:00")

                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''


            End If

        End If
        If Welcome.CSIF_version = 1 Then Label5.Text = ""
        If Welcome.CSIF_version = 1 Then Label6.Text = ""
    End Sub





    Sub Chart1_mouse_hover(sender As Object, e As EventArgs) Handles Chart1.MouseHover

        Chart1.Series("Series1").Points(3).CustomProperties = "Exploded = true"

    End Sub

    Sub Chart1_mouse_leave(sender As Object, e As EventArgs) Handles Chart1.MouseLeave

        Chart1.Series("Series1").Points(3).CustomProperties = "Exploded = false"

    End Sub



    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Clic on the pie chart / Report_BarChart FORM
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

        If Welcome.CSIF_version <> 1 Then

            'If Machine_util_det.machine_util_det_wasactive = True Then
            '    Machine_util_det.Visible = False

            'End If

            Chart1.Cursor = Cursors.Hand
            If consolidated = False Then
                Report_PieChart.CB_Report.Items.Clear()
                ' Config_report.periode_returned = CSI_LIB.Detailled(Config_report.DTP_StartDate.Value, Config_report.DTP_EndDate.Value, Config_report.read_tree(), True)
                'Dim tmp_periode_returned As periode()
                'tmp_periode_returned = CSI_LIB.Detailled(date1, date2, machine, True)

                'If tmp_periode_returned.Length <= 2 Then
                '    tmp_periode_returned(0).machine_name = RealName
                'End If
                Pie_chart_source = 0
                Call Config_report.Fill_Combobox_detailled(Config_report.periode_returned)
                Report_PieChart.MdiParent = Reporting_application

                Report_PieChart.CB_Report.SelectedIndex = Config_report.machine_Report_BarChart
                Me.Visible = False

            Else
                Pie_chart_source = 3
                Report_PieChart.CB_Report.Items.Clear()
                Call Config_report.Fill_Combobox_detailled(consolidated_periode)
                Report_PieChart.MdiParent = Reporting_application
                Report_PieChart.CB_Report.SelectedIndex = 0
                Report_PieChart.SuspendLayout()
                Me.Visible = False
            End If
        Else
            Chart1.Cursor = Cursors.Arrow
        End If
    End Sub



    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Consolidate
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub CB_Consolidate_CheckedChanged(sender As Object, e As EventArgs) Handles CB_Consolidate.CheckedChanged
        'If BTN_Unit.Text = "%" Then
        '    BTN_Unit.Text = "h"
        'End If

        consolidate()




    End Sub

    Private Sub consolidate()

        Try

            If CB_Consolidate.Checked = True Then
                consolidated = True
                CB_Consolidate.Text = "Unbind"
                CB_Consolidate.ForeColor = Color.Black

                consolidated_periode(0).date_ = Config_report.periode_returned(0).date_
                consolidated_periode(0).machine_name = "Consolidated"

                consolidated_periode(0).shift1 = New Dictionary(Of String, Double)
                consolidated_periode(0).shift1.Add("CYCLE ON", 0)
                consolidated_periode(0).shift1.Add("CYCLE OFF", 0)
                consolidated_periode(0).shift1.Add("SETUP", 0)
                'consolidated_periode(0).shift1.Add("OTHER", 0)

                consolidated_periode(0).shift2 = New Dictionary(Of String, Double)
                consolidated_periode(0).shift2.Add("CYCLE ON", 0)
                consolidated_periode(0).shift2.Add("CYCLE OFF", 0)
                consolidated_periode(0).shift2.Add("SETUP", 0)
                'consolidated_periode(0).shift2.Add("OTHER", 0)

                consolidated_periode(0).shift3 = New Dictionary(Of String, Double)
                consolidated_periode(0).shift3.Add("CYCLE ON", 0)
                consolidated_periode(0).shift3.Add("CYCLE OFF", 0)
                consolidated_periode(0).shift3.Add("SETUP", 0)
                'consolidated_periode(0).shift3.Add("OTHER", 0)



                'declaration not needed in FOR statement
                'Dim i As Integer ', j As Integer
                For i = 0 To (UBound(Config_report.periode_returned) - 1)
                    For Each key In Config_report.periode_returned(i).shift1

                        Select Case key.Key
                            Case "CYCLE ON"
                                consolidated_periode(0).shift1.Item("CYCLE ON") = consolidated_periode(0).shift1.Item("CYCLE ON") + Config_report.periode_returned(i).shift1.Item("CYCLE ON")
                            Case "CYCLE OFF"
                                consolidated_periode(0).shift1.Item("CYCLE OFF") = consolidated_periode(0).shift1.Item("CYCLE OFF") + Config_report.periode_returned(i).shift1.Item("CYCLE OFF")
                            Case "SETUP"
                                consolidated_periode(0).shift1.Item("SETUP") = consolidated_periode(0).shift1.Item("SETUP") + Config_report.periode_returned(i).shift1.Item("SETUP")
                            Case Else
                                'If Not key.Key.Contains("PARTNO") Then consolidated_periode(0).shift1.Item("OTHER") = consolidated_periode(0).shift1.Item("OTHER") + Form3.periode_returned(i).shift1.Item(key.Key)
                                If (consolidated_periode(0).shift1.ContainsKey(key.Key)) Then
                                    consolidated_periode(0).shift1.Item(key.Key) = consolidated_periode(0).shift1.Item(key.Key) + Config_report.periode_returned(i).shift1.Item(key.Key)
                                Else
                                    consolidated_periode(0).shift1.Add(key.Key, Config_report.periode_returned(i).shift1.Item(key.Key))
                                End If
                        End Select
                    Next

                    For Each key In Config_report.periode_returned(i).shift2
                        Select Case key.Key
                            Case "CYCLE ON"
                                consolidated_periode(0).shift2.Item("CYCLE ON") = consolidated_periode(0).shift2.Item("CYCLE ON") + Config_report.periode_returned(i).shift2.Item("CYCLE ON")
                            Case "CYCLE OFF"
                                consolidated_periode(0).shift2.Item("CYCLE OFF") = consolidated_periode(0).shift2.Item("CYCLE OFF") + Config_report.periode_returned(i).shift2.Item("CYCLE OFF")
                            Case "SETUP"
                                consolidated_periode(0).shift2.Item("SETUP") = consolidated_periode(0).shift2.Item("SETUP") + Config_report.periode_returned(i).shift2.Item("SETUP")
                            Case Else
                                'If Not key.Key.Contains("PARTNO") Then consolidated_periode(0).shift2.Item("OTHER") = consolidated_periode(0).shift2.Item("OTHER") + Form3.periode_returned(i).shift2.Item(key.Key)
                                If (consolidated_periode(0).shift2.ContainsKey(key.Key)) Then
                                    consolidated_periode(0).shift2.Item(key.Key) = consolidated_periode(0).shift2.Item(key.Key) + Config_report.periode_returned(i).shift2.Item(key.Key)
                                Else
                                    consolidated_periode(0).shift2.Add(key.Key, Config_report.periode_returned(i).shift2.Item(key.Key))
                                End If
                        End Select
                    Next

                    For Each key In Config_report.periode_returned(i).shift3
                        Select Case key.Key
                            Case "CYCLE ON"
                                consolidated_periode(0).shift3.Item("CYCLE ON") = consolidated_periode(0).shift3.Item("CYCLE ON") + Config_report.periode_returned(i).shift3.Item("CYCLE ON")
                            Case "CYCLE OFF"
                                consolidated_periode(0).shift3.Item("CYCLE OFF") = consolidated_periode(0).shift3.Item("CYCLE OFF") + Config_report.periode_returned(i).shift3.Item("CYCLE OFF")
                            Case "SETUP"
                                consolidated_periode(0).shift3.Item("SETUP") = consolidated_periode(0).shift3.Item("SETUP") + Config_report.periode_returned(i).shift3.Item("SETUP")
                            Case Else
                                'If Not key.Key.Contains("PARTNO") Then consolidated_periode(0).shift3.Item("OTHER") = consolidated_periode(0).shift3.Item("OTHER") + Form3.periode_returned(i).shift3.Item(key.Key)
                                If (consolidated_periode(0).shift3.ContainsKey(key.Key)) Then
                                    consolidated_periode(0).shift3.Item(key.Key) = consolidated_periode(0).shift3.Item(key.Key) + Config_report.periode_returned(i).shift3.Item(key.Key)
                                Else
                                    consolidated_periode(0).shift3.Add(key.Key, Config_report.periode_returned(i).shift3.Item(key.Key))
                                End If
                        End Select
                    Next
                Next


                For i = 0 To 89  ' Col
                    Big_consolidated_periode(0, i).machine_name = "Consolidate"
                    Big_consolidated_periode(0, i).shift1 = New Dictionary(Of String, Double)
                    Big_consolidated_periode(0, i).shift1.Add("CYCLE ON", 0)
                    Big_consolidated_periode(0, i).shift1.Add("CYCLE OFF", 0)
                    Big_consolidated_periode(0, i).shift1.Add("SETUP", 0)
                    Big_consolidated_periode(0, i).shift1.Add("OTHER", 0)

                    For j = 0 To UBound(Config_report.big_periode_returned, 1) ' Row, machines ... 
                        Big_consolidated_periode(0, i).date_ = Config_report.big_periode_returned(j, i).date_
                        Big_consolidated_periode(0, i).shift1.Item("CYCLE ON") = Big_consolidated_periode(0, i).shift1.Item("CYCLE ON") + Val(Config_report.big_periode_returned(j, i).shift1.Item("CYCLE ON"))
                        Big_consolidated_periode(0, i).shift1.Item("CYCLE OFF") = Big_consolidated_periode(0, i).shift1.Item("CYCLE OFF") + Val(Config_report.big_periode_returned(j, i).shift1.Item("CYCLE OFF"))
                        Big_consolidated_periode(0, i).shift1.Item("SETUP") = Big_consolidated_periode(0, i).shift1.Item("SETUP") + Val(Config_report.big_periode_returned(j, i).shift1.Item("SETUP"))
                        Big_consolidated_periode(0, i).shift1.Item("OTHER") = Big_consolidated_periode(0, i).shift1.Item("OTHER") + Val(Config_report.big_periode_returned(j, i).shift1.Item("OTHER"))
                    Next
                Next

                CB_Report.Items.Clear()

                Config_report.Fill_Combo_Report_BarChart(consolidated_periode)

                refresh_Report_BarChart(Big_consolidated_periode, consolidated_periode, 0)
                CB_Report.SelectedIndex = CB_Report.Items.Count - 1

                CB_Report.Enabled = False

            Else

                consolidated = False
                CB_Consolidate.Text = "Consolidate"
                ' CheckBox1.ForeColor = Color.WhiteSmoke
                CB_Report.Enabled = True
                CB_Report.Items.Clear()

                Config_report.Fill_Combo_Report_BarChart(Config_report.periode_returned)
                CB_Report.SelectedIndex = CB_Report.Items.Count - 1
                refresh_Report_BarChart(Config_report.big_periode_returned, Config_report.periode_returned, Config_report.machine_Report_BarChart)


            End If
        Catch ex As Exception
            MessageBox.Show("Could not consolidate the data", MsgBoxStyle.Critical)
        End Try

    End Sub




    Dim firstClic As Boolean = True
    Dim Notdoubleclic As Boolean = False
    Dim millisec As Integer = 0
    Dim date1 As Date, date2 As Date
    Sub Clictimer_Tick(ByVal sender As Object,
            ByVal e As EventArgs) Handles Clictimer.Tick
        millisec += 100
        If millisec >= SystemInformation.DoubleClickTime And firstClic = False Then
            Clictimer.Stop()
            'Notdoubleclic = True

            If Machine_util_det.Visible = True Then
                Machine_util_det.Close()
            End If
            If active_point.PointIndex <> -1 And CB_Consolidate.Checked = False Then
                Pie_chart_source = 2
                Reporting_application.Cursor = Cursors.WaitCursor

                temporary_periode_returned = CSI_LIB.Detailled(date1, date2, Config_report.read_tree(), True)
                Machine_util_det.Show()
                Reporting_application.Cursor = Cursors.Arrow
            End If
            firstClic = True
            millisec = 0


        End If
    End Sub


#Region "identifier_if drag"

    Dim mouse_pos_y As Integer
    Dim new_mouse_y As Integer
    Private Sub Chart2_Click_dn(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart2.MouseDown
        mouse_pos_y = e.Y

    End Sub
#End Region


    Dim active_point As HitTestResult
    Private Sub Chart2_Click_up(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart2.MouseUp


        If Welcome.CSIF_version <> 1 Then

            new_mouse_y = e.Y
            If (mouse_pos_y <> new_mouse_y) Then
                Clictimer.Stop()
                firstClic = False

            End If

            If (e.Button.CompareTo(Forms.MouseButtons.Right) = 0) Then
                Chart2.ChartAreas(0).AxisX.ScaleView.ZoomReset()
            Else


                If firstClic Then
                    firstClic = False
                    Clictimer.Start()
                Else
                    Clictimer.Stop()
                    firstClic = True
                    Notdoubleclic = False
                End If
                If (e.Button.CompareTo(Forms.MouseButtons.Left) = 0) Then
                    Dim HTR As HitTestResult
                    '  Dim SelectDataPoint As DataPoint

                    HTR = Chart2.HitTest(e.X, e.Y)
                    active_point = HTR
                    ' SelectDataPoint = Chart2.Series(0).Points(HTR.PointIndex)
                    If (HTR.PointIndex >= 0 And HTR.PointIndex <= 89) Then

                        Dim date_ As String() = Split(Chart2.Series("Series1").Points(HTR.PointIndex).AxisLabel.ToString(), " ")

                        Dim cultures As CultureInfo = New CultureInfo("fr-FR")
                        If Config_report.days = 1 Then
                            'date1 = Convert.ToDateTime((date_(0) & "," & MONTH_(date_(1)) & ", 20" & date_(2)), cultures)
                            date1 = Convert.ToDateTime((date_(0) & "," & MONTH_(date_(1)) & ", " & date_(2)), cultures)
                            date2 = date1
                        End If

                        If Config_report.days > 1 Then
                            If MONTH_(date_(2)) <= MONTH_(date_(5)) Then
                                date1 = Convert.ToDateTime((date_(1) & "," & MONTH_(date_(2)) & ", " & date_(6)), cultures)
                            Else
                                date1 = Convert.ToDateTime((date_(1) & "," & MONTH_(date_(2)) & ", " & (Val(date_(6)) - 1).ToString()), cultures)
                            End If
                            date2 = Convert.ToDateTime((date_(4) & "," & MONTH_(date_(5)) & ", " & date_(6)), cultures)
                        End If
                    End If
                End If
                temporary_periode_returned = Nothing
                'temporary_periode_returned = CSI_LIB.Detailled(date1, date2, Config_report.read_tree(), True)
                'Machine_util_det.Show() 
            End If


        End If




    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' MONTH_ : 
    ' para : Integer, Month in numbers
    ' out : String, first 3 lettres of the month name
    '-----------------------------------------------------------------------------------------------------------------------

    Public Function MONTH_(MONTH As String) As Integer
        Try
            Select Case MONTH
                Case "Jan"
                    Return 1
                Case "Feb"
                    Return 2
                Case "Mar"
                    Return 3
                Case "Apr"
                    Return 4
                Case "May"
                    Return 5
                Case "Jun"
                    Return 6
                Case "Jul"
                    Return 7
                Case "Aug"
                    Return 8
                Case "Sep"
                    Return 9
                Case "Oct"
                    Return 10
                Case "Nov"
                    Return 11
                Case "Dec"
                    Return 12
            End Select
        Catch ex As Exception
            'MessageBox.Show("1" & ex.Message)
            Return 0
        End Try
        Return 0
    End Function


    '-----------------------------------------------------------------------------------------------------------------------
    ' MOVE FORME 9
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Dim _MouseDown As Boolean, _MouseX As Integer, _MouseY As Integer
    Private Sub Report_BarChart_mouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        _MouseDown = True
        _MouseX = e.X
        _MouseY = e.Y

    End Sub

    Private Sub Report_BarChart_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        _MouseDown = False

    End Sub


    Private Sub Report_BarChart_Mousemove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove


        If _MouseDown = True Then
            Reporting_application.SuspendLayout()
            Me.Left = Me.Left + (e.X - _MouseX)
            Me.Top = Me.Top + (e.Y - _MouseY)
            'If Me.Top < 20 Then Me.Top = 0
            'If Me.Left < 20 Then Me.Left = 0
            Reporting_application.ResumeLayout(True)

        End If

    End Sub



    Public tmp_periode_returned As periode()
    Private Sub Chart2_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart2.DoubleClick

        If Welcome.CSIF_version <> 1 Then
            If (e.Button.CompareTo(Forms.MouseButtons.Left) = 0) Then
                Dim HTR As HitTestResult
                '  Dim SelectDataPoint As DataPoint

                HTR = Chart2.HitTest(e.X, e.Y)
                ' SelectDataPoint = Chart2.Series(0).Points(HTR.PointIndex)
                If (HTR.PointIndex >= 0 And HTR.PointIndex <= 89) Then
                    Dim date1 As Date, date2 As Date
                    Dim date_ As String() = Split(Chart2.Series("Series1").Points(HTR.PointIndex).AxisLabel.ToString(), " ")
                    Dim cultures As CultureInfo = New CultureInfo("fr-FR")
                    If Config_report.days = 1 Then
                        'date1 = Convert.ToDateTime((date_(0) & "," & MONTH_(date_(1)) & ", 20" & date_(2)), cultures)
                        date1 = Convert.ToDateTime((date_(0) & "," & MONTH_(date_(1)) & ", " & date_(2)), cultures)
                        date2 = date1
                    End If

                    If Config_report.days > 1 Then
                        If MONTH_(date_(2)) <= MONTH_(date_(5)) Then
                            date1 = Convert.ToDateTime((date_(1) & "," & MONTH_(date_(2)) & ", " & date_(6)), cultures)
                        Else
                            date1 = Convert.ToDateTime((date_(1) & "," & MONTH_(date_(2)) & ", " & (Val(date_(6)) - 1).ToString()), cultures)
                        End If
                        date2 = Convert.ToDateTime((date_(4) & "," & MONTH_(date_(5)) & ", " & date_(6)), cultures)
                    End If

                    If consolidated = False Then
                        Report_PieChart.CB_Report.Items.Clear()
                        Dim RealName As String
                        Dim Text As String() = Split(CB_Report.Text, " : ")
                        RealName = Text(0)
                        Text(0) = CSI_LIB.RenameMachine(Text(0))
                        Dim machine(0) As String
                        machine(0) = Text(0)
                        Pie_chart_source = 1
                        tmp_periode_returned = CSI_LIB.Detailled(date1, date2, machine, True)

                        If tmp_periode_returned.Length <= 2 Then
                            tmp_periode_returned(0).machine_name = RealName
                        End If

                        Call Config_report.Fill_Combobox_detailled(tmp_periode_returned)
                        Report_PieChart.CB_Report.SelectedIndex = 0
                        Report_PieChart.MdiParent = Reporting_application

                        Me.Visible = False
                    Else
                        'Form7.ComboBox1.Items.Clear()
                        'Call Form3.Fill_Combobox_detailled(consolidated_periode)
                    End If



                End If

            End If
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        Dim webAddress As String = "http://www.csiflex.com//"
        Process.Start(webAddress)
    End Sub


    'Dim WithEvents mPrintDocument As New PrintDocument
    'Dim mPrintBitMap As Bitmap


    'Private Sub m_PrintDocument_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles mPrintDocument.PrintPage
    '    ' Draw the image centered.
    '    Dim lWidth As Integer = e.MarginBounds.X + (e.MarginBounds.Width - mPrintBitMap.Width) \ 2
    '    Dim lHeight As Integer = e.MarginBounds.Y + (e.MarginBounds.Height - mPrintBitMap.Height) \ 2
    '    e.Graphics.DrawImage(mPrintBitMap, lWidth, lheight)

    '    ' There's only one page.
    '    e.HasMorePages = False
    'End Sub

    'Private WithEvents printDocument1 As New PrintDocument



    Private Sub BTN_trends_Click(sender As Object, e As EventArgs) Handles BTN_trends.Click
        ' Chart2.ChartAreas(0).
        If Chart2.Series("Series5").Color = Color.OrangeRed Then
            Chart2.Series("Series5").Color = Color.Transparent
        Else
            Chart2.Series("Series5").Color = Color.OrangeRed
        End If

    End Sub

    Private Sub Button3_Click_2(sender As Object, e As EventArgs)
        MsgBox(Reporting_application.Width)
    End Sub


    Private Sub printDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias


        'Dim newMargins As System.Drawing.Printing.Margins
        'newMargins = New System.Drawing.Printing.Margins(0.2, 0.2, 0.2, 0.2)
        'PrintDocument1.DefaultPageSettings.Margins = newMargins


        Dim lWidth As Integer = e.MarginBounds.X '+ (e.MarginBounds.Width - memoryImage.Width) \ 2
        Dim lHeight As Integer = e.MarginBounds.Y '+ (e.MarginBounds.Height - memoryImage.Height) \ 2
        '    e.Graphics.DrawImage(mPrintBitMap, lWidth, lheight)
        e.Graphics.DrawImage(memoryImage, 60, 60)
    End Sub

    Dim memoryImage As Bitmap

    Private Sub CaptureScreen()

        'memoryImage.SetResolution(600, 600)
        Dim myGraphics As Graphics = Me.CreateGraphics()
        myGraphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        myGraphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        myGraphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        Dim s As Drawing.Size = Me.Size
        memoryImage = New Bitmap(s.Width - 5, s.Height - 5, myGraphics)
        'memoryImage.SetResolution(600, 600)
        Dim memoryGraphics As Graphics = Graphics.FromImage(memoryImage)
        memoryGraphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        memoryGraphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        memoryGraphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        memoryGraphics.CopyFromScreen(Reporting_application.Location.X + Me.Location.X + 15, Reporting_application.Location.Y + Me.Location.Y + 75, 0, 0, s)
        '  memoryImage.Save("ZOMG AMAZING DRAWING.png", System.Drawing.Imaging.ImageFormat.Png)
    End Sub

    Private Sub LBL_MachineName_TextChanged(sender As Object, e As EventArgs) Handles LBL_MachineName.TextChanged
        LBL_MachineName.Font = New Font(LBL_MachineName.Font.FontFamily, 50, LBL_MachineName.Font.Style)

        Dim mesur_ As Integer = System.Windows.Forms.TextRenderer.MeasureText(LBL_MachineName.Text,
   New Font(LBL_MachineName.Font.FontFamily, LBL_MachineName.Font.Size, LBL_MachineName.Font.Style)).Width

        While (LBL_MachineName.Width < mesur_)

            LBL_MachineName.Font = New Font(LBL_MachineName.Font.FontFamily, LBL_MachineName.Font.Size - 0.5F, LBL_MachineName.Font.Style)
            mesur_ = System.Windows.Forms.TextRenderer.MeasureText(LBL_MachineName.Text,
     New Font(LBL_MachineName.Font.FontFamily, LBL_MachineName.Font.Size, LBL_MachineName.Font.Style)).Width
        End While
    End Sub

    Private Sub BTN_snapSHot_Click(sender As Object, e As EventArgs) Handles BTN_snapSHot.Click
        SnapShot.Show()

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Try
            'source:http://msdn.microsoft.com/en-us/library/6he9hz8c(v=vs.110).aspx?cs-save-lang=1&cs-lang=vb#code-snippet-1
            CaptureScreen()
            'PrintDocument1.Print()

            '' Copy the form's image into a bitmap.
            'mPrintBitMap = New Bitmap(Me.Width, Me.Width)
            'Dim lRect As System.Drawing.Rectangle
            'lRect.Width = Me.Width
            'lRect.Height = Me.Width
            'Me.DrawToBitmap(mPrintBitMap, lRect)


            '' Make a PrintDocument and print.
            'mPrintDocument = New PrintDocument
            '' mPrintDocument.Print()

            'PrintDocument1.OriginAtMargins = True
            'Dim margins As New System.Drawing.Printing.Margins(5, 0, 120, 100)



            Dim ps As New PageSettings
            ps.Landscape = True

            ' ps.Margins = margins
            PrintDocument1.DefaultPageSettings = ps

            PrintDialog1.Document = PrintDocument1
            PrintDialog1.PrinterSettings.DefaultPageSettings.Landscape = True

            If (PrintDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then

                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.ShowDialog()

            End If
        Catch ex As Exception
            'MessageBox.Show("2" & ex.Message)
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Report_PartNumber.setData()
        Report_PartNumber.CB_Report.Items.Clear()
        Call Config_report.Fill_Combobox_partnumber(Config_report.periode_returned)

        Report_PartNumber.MdiParent = Reporting_application
        Report_PartNumber.CB_Report.SelectedIndex = Config_report.machine_Report_BarChart
        Me.Visible = False


    End Sub

    Private Sub Report_BarChart_ResizeEnd(sender As Object, e As EventArgs) Handles Me.SizeChanged
        GroupBox_evo.Height = Me.Height - 370
    End Sub
End Class
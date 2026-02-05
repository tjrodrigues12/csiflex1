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

Public Class Form9
    Public data(4) As Integer
    Public data2(89) As Integer
    Public data3(89) As Integer
    Public data4(89) As Integer
    Public data5(89) As Integer
    Public maxZoom As Integer
    Public consolidated_shift_array(26, 3, 1) As String

    Public consolidated As Boolean = False
    Public target_ As Integer = 0
    Public targetColor_ As String = 0
    Public consolidated_periode(1) As periode
    Public Big_consolidated_periode(1, 89) As periode
    Public mousePoint As Point
    Public newy As Integer
    Public CSI_LIB As New CSI_Library.CSI_Library
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Load form9
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Form9_Loaded(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.ResumeLayout()
    End Sub
    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim p As New PictureBox
        'Dim rect As New Rectangle
        'rect.Location = Me.Location
        'rect.Size = Me.Size

        '   me.PictureBox1.Controls.AddRange(new Control() me.picturebox1)
        Try

            Me.SuspendLayout()
            Me.MdiParent = Form1
            Me.Location = New System.Drawing.Point(Form3.Location.X + Form3.Size.Width, 25)


            If Exists(System.Windows.Forms.Application.StartupPath & "\sys\targetColor_.sys") Then
                Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\targetColor_.sys")
                    targetColor_ = CInt(reader.ReadLine())
                    reader.Close()
                End Using
            End If

            If Exists(System.Windows.Forms.Application.StartupPath & "\sys\target_.sys") Then
                Using reader As StreamReader = New StreamReader(Forms.Application.StartupPath & "\sys\target_.sys")
                    target_ = CInt(reader.ReadLine()) 'this line will call NumericUpDownValueChanged
                    reader.Close()
                End Using
            End If


            Call target(Val(target_), Val(targetColor_))
            ' GroupBox3.ForeColor = Color.White



            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            Me.BackColor = Color.Transparent
            SetStyle(ControlStyles.DoubleBuffer, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)



            ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1


            If Welcome.CSIF_version = 1 Then

                'Chart1.Palette 
                Chart2.Series("Series3").Color = Color.Transparent
                Chart2.Series("Series4").Color = Color.Transparent


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Set the target line
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Sub target(target_ As Integer, targetColor As Integer)
        Try


            Chart2.ChartAreas(0).AxisY.StripLines.Clear()
            Dim stripLineTarget As StripLine = New StripLine()
            stripLineTarget.StripWidth = 0
            stripLineTarget.BorderColor = Color.FromArgb(targetColor)
            stripLineTarget.BackColor = System.Drawing.Color.RosyBrown
            stripLineTarget.BackGradientStyle = GradientStyle.LeftRight
            stripLineTarget.BorderWidth = 2
            stripLineTarget.BorderDashStyle = ChartDashStyle.DashDot
            stripLineTarget.IntervalOffset = target_
            'stripLineTarget.Text = "Target (" & target_ & ")"
            'stripLineTarget.TextAlignment = StringAlignment.Far
            'stripLineTarget.TextLineAlignment = StringAlignment.Far
            'stripLineTarget.TextOrientation = TextOrientation.Auto

            Chart2.ChartAreas(0).AxisY.StripLines.Add(stripLineTarget)


            'text target label
            'Chart2.ChartAreas(0).AxisY.LabelStyle.Enabled = True
            'Chart2.ChartAreas(0).AxisY.IsLabelAutoFit = True
            'Chart2.ChartAreas(0).AxisY.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90
            Chart2.ChartAreas(0).AxisY.LabelStyle.Enabled = True
            'Chart2.ChartAreas(0).AxisY.LineColor = System.Drawing.Color.Transparent
            'Chart2.ChartAreas(0).AxisY.MajorGrid.Enabled = False
            'Chart2.ChartAreas(0).AxisY.MajorTickMark.Enabled = False

            Dim cl As New CustomLabel
            cl.FromPosition = target_
            cl.ToPosition = target_ + 1
            cl.Text = target_.ToString()
            cl.RowIndex = 0
        
            Chart2.ChartAreas(0).AxisY.CustomLabels.Clear()
            Chart2.ChartAreas(0).AxisY.CustomLabels.Add(cl)

            Chart2.DataBind()
        Catch ex As Exception
            MessageBox.Show("Could not set the target line : " & ex.Message)
        End Try

    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Combobox
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        Dim mach As String = ComboBox1.SelectedItem.ToString()
        mach = mach.Substring(0, mach.IndexOf(":")).Trim
        LBL_MachineName.Text = mach

        If consolidated = False Then
            refresh_form9(Form3.big_periode_returned, Form3.periode_returned, Form3.machine_form9)
        Else
            refresh_form9(Big_consolidated_periode, consolidated_periode, 0)
        End If

        If Welcome.CSIF_version = 1 Then

            'Chart1.Palette 
            Chart2.Series("Series3").Color = Color.Transparent
            Chart2.Series("Series4").Color = Color.Transparent


        End If
    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' refresh the form 9 display / Fill the Cycle time / Charting
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub refresh_form9(ByRef big_periode_returned As periode(,), ByRef periode_ As periode(), indx As Integer)
        Dim Text As String() = Split(ComboBox1.Text, " : ")
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
                        Call Form3.fill_form9_shift(periode_, i)

                        Form3.machine_form9 = i
                        indx = i
                        GoTo done_
                    End If
                Next
            Else
                Call Form3.fill_form9_shift(periode_, 0)
            End If
done_:
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Consolidation Error")
        End Try


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' periode array '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim con_time, coff_time, other_time, setup_time As Double
        'con_time = big_periode_returned(indx, 0).shift1.Item("CYCLE ON") ' + big_periode_returned(indx, 0).shift2.Item("CYCLE ON") + big_periode_returned(indx, 0).shift3.Item("CYCLE ON")
        'coff_time = big_periode_returned(indx, 0).shift1.Item("CYCLE OFF") ' + big_periode_returned(indx, 0).shift2.Item("CYCLE OFF") + big_periode_returned(indx, 0).shift3.Item("CYCLE OFF")
        ''other_time = big_periode_returned(indx, 0).shift1.Item("OTHER")
        'setup_time = big_periode_returned(indx, 0).shift1.Item("SETUP") ' + big_periode_returned(indx, 0).shift2.Item("SETUP") + big_periode_returned(indx, 0).shift3.Item("SETUP")



        If periode_(indx).shift1.ContainsKey("CYCLE ON") Then
            con_time += periode_(indx).shift1.Item("CYCLE ON")
        End If
        If periode_(indx).shift2.ContainsKey("CYCLE ON") Then
            con_time += periode_(indx).shift2.Item("CYCLE ON")
        End If
        If periode_(indx).shift3.ContainsKey("CYCLE ON") Then
            con_time += periode_(indx).shift3.Item("CYCLE ON")
        End If

        If periode_(indx).shift1.ContainsKey("CYCLE OFF") Then
            coff_time += periode_(indx).shift1.Item("CYCLE OFF")
        End If
        If periode_(indx).shift2.ContainsKey("CYCLE OFF") Then
            coff_time += periode_(indx).shift2.Item("CYCLE OFF")
        End If
        If periode_(indx).shift3.ContainsKey("CYCLE OFF") Then
            coff_time += periode_(indx).shift3.Item("CYCLE OFF")
        End If



        'con_time = periode_(indx).shift1.Item("CYCLE ON") + periode_(indx).shift2.Item("CYCLE ON") + periode_(indx).shift3.Item("CYCLE ON")
        'coff_time = periode_(indx).shift1.Item("CYCLE OFF") + periode_(indx).shift2.Item("CYCLE OFF") + periode_(indx).shift3.Item("CYCLE OFF")

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
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                other_time += periode_(indx).shift1.Item(key)
            End If
        Next
        For Each key In (periode_(indx).shift2.Keys)
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                other_time += periode_(indx).shift2.Item(key)
            End If
        Next
        For Each key In (periode_(indx).shift3.Keys)
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                other_time += periode_(indx).shift3.Item(key)
            End If
        Next

        Try
            Dim total As Double = other_time + setup_time + coff_time + con_time

            If (Welcome.CSIF_version <> 1) Then 'std
                If total <> 0 Then
                    Label28.Text = ((con_time) * 100 / total).ToString("00.00") 'Math.Round((con_time) * 100 / total).ToString("00.00")
                    Label27.Text = ((coff_time) * 100 / total).ToString("00.00") 'Math.Round((coff_time) * 100 / total).ToString("00.00")
                    Label26.Text = ((setup_time) * 100 / total).ToString("00.00") 'Math.Round((setup_time) * 100 / total).ToString("00.00")
                    Label22.Text = ((other_time) * 100 / total).ToString("00.00") 'Math.Round((other_time) * 100 / total).ToString("00.00")
                    Label21.Text = ((con_time + coff_time + setup_time + other_time) * 100 / total).ToString("00.00") '"100.00"
                Else
                    Label28.Text = ("00.00")
                    Label27.Text = ("00.00")
                    Label26.Text = ("00.00")
                    Label22.Text = ("00.00")
                    Label21.Text = ("00.00")
                End If

                ' Pie CHart ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                data(0) = Val(Label28.Text)
                data(1) = Val(Label27.Text)
                data(2) = Val(Label26.Text)
                data(3) = Val(Label22.Text)
                Chart1.Series("Series1").Points.DataBindY(data)
                'If Welcome.CSIF_version = 1 Then
                '    Chart1.Series("Series1").Points.Item(2).Color = Color.Transparent
                '    Chart1.Series("Series1").Points.Item(3).Color = Color.Transparent
                'End If
                total1 = total
            Else 'lite
                coff_time = coff_time + setup_time + other_time
                If total <> 0 Then
                    Label28.Text = ((con_time) * 100 / total).ToString("00.00") 'Math.Round((con_time) * 100 / total).ToString("00.00")
                    Label27.Text = ((coff_time) * 100 / total).ToString("00.00") 'Math.Round((coff_time) * 100 / total).ToString("00.00")
                    Label26.Text = ("00.00") 'Math.Round((setup_time) * 100 / total).ToString("00.00")
                    Label22.Text = ("00.00") 'Math.Round((other_time) * 100 / total).ToString("00.00")
                    Label21.Text = ((con_time + coff_time) * 100 / total).ToString("00.00") '"100.00"
                Else
                    Label28.Text = ("00.00")
                    Label27.Text = ("00.00")
                    Label26.Text = ("00.00")
                    Label22.Text = ("00.00")
                    Label21.Text = ("00.00")
                End If

                ' Pie CHart ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                data(0) = Val(Label28.Text)
                data(1) = Val(Label27.Text)
                data(2) = Val(Label26.Text)
                data(3) = Val(Label22.Text)
                Chart1.Series("Series1").Points.DataBindY(data)
                'If Welcome.CSIF_version = 1 Then
                '    Chart1.Series("Series1").Points.Item(2).Color = Color.Transparent
                '    Chart1.Series("Series1").Points.Item(3).Color = Color.Transparent
                'End If
                total1 = total
            End If

        Catch ex As Exception
            MessageBox.Show("Could not display the selected cycle times" & ex.Message)
        End Try

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Big Chart ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Try
            year_start = Form3.DateTimePicker1.Value.Year
            year_end = Form3.DateTimePicker2.Value.Year
            month_start = Form3.DateTimePicker1.Value.Month
            month_end = Form3.DateTimePicker2.Value.Month
            day_start = Form3.DateTimePicker1.Value.Day
            day_end = Form3.DateTimePicker2.Value.Day

            Chart2.Series("Series1").SmartLabelStyle.Enabled = False

            If (Welcome.CSIF_version <> 1) Then 'std
                'chart 2 - Serie1 = Cycle on
                For i = 1 To 89 'doit verifier si cycle on existe dedans
                    data2(89 - i) = big_periode_returned(indx, i).shift1.Item("CYCLE ON")
                Next
                Chart2.Series("Series1").Points.DataBindY(data2)

                'chart 2 - Serie2 = Cycle off
                For i = 1 To 89
                    data3(89 - i) = big_periode_returned(indx, i).shift1.Item("CYCLE OFF")
                Next
                Chart2.Series("Series2").Points.DataBindY(data3)

                'chart 2 - Serie3 = Setup
                For i = 1 To 89
                    data4(89 - i) = big_periode_returned(indx, i).shift1.Item("SETUP")
                Next
                Chart2.Series("Series3").Points.DataBindY(data4)

                'chart 2 - Serie4 = Other
                For i = 1 To 89
                    data5(89 - i) = big_periode_returned(indx, i).shift1.Item("OTHER")
                Next
                Chart2.Series("Series4").Points.DataBindY(data5)
            Else 'lite
                'chart 2 - Serie1 = Cycle on
                For i = 1 To 89 'doit verifier si cycle on existe dedans
                    data2(89 - i) = big_periode_returned(indx, i).shift1.Item("CYCLE ON")
                Next
                Chart2.Series("Series1").Points.DataBindY(data2)

                'chart 2 - Serie2 = Cycle off
                For i = 1 To 89
                    data3(89 - i) = big_periode_returned(indx, i).shift1.Item("CYCLE OFF") + big_periode_returned(indx, i).shift1.Item("SETUP") + big_periode_returned(indx, i).shift1.Item("OTHER")
                Next
                Chart2.Series("Series2").Points.DataBindY(data3)

                'chart 2 - Serie3 = Setup
                For i = 1 To 89
                    data4(89 - i) = 0 'big_periode_returned(indx, i).shift1.Item("SETUP")
                Next
                Chart2.Series("Series3").Points.DataBindY(data4)

                'chart 2 - Serie4 = Other
                For i = 1 To 89
                    data5(89 - i) = 0 'big_periode_returned(indx, i).shift1.Item("OTHER")
                Next
                Chart2.Series("Series4").Points.DataBindY(data5)
            End If



            'Dim dataR As Integer() = CSI_LIB.Effectue_Regression(data2, data3, data4, data5)
            'Chart3.Series.Add("Trend")
            'Chart3.Series("Trend").ChartType = SeriesChartType.FastLine
            'Chart3.Series("Trend").Points.DataBindY(dataR)
            Dim dates_ As String()
            For i = 1 To 89
                dates_ = Split(big_periode_returned(indx, i).date_)

                day_start = dates_(0)
                day_end = dates_(1)
                month_start = dates_(2)
                month_end = dates_(3)
                year_start = dates_(4)
                year_end = dates_(5)

                If day_start = day_end And month_end = month_start Then
                    Chart2.Series("Series1").Points(89 - i).AxisLabel = day_end.ToString & " " & SetupForm.mois_(month_end) & " " & year_end.ToString
                Else
                    Chart2.Series("Series1").Points(89 - i).AxisLabel = "From " & day_start.ToString & " " & SetupForm.mois_(month_start) & " to " & day_end.ToString & " " & SetupForm.mois_(month_end) & " " & year_end.ToString
                End If

                ' Transparency / tooltips , the date is in Chart2.Series("Series1").Points(89 - i).AxisLabel only
                Chart2.Series("Series1").Points(89 - i).Color = Color.FromArgb(228, Chart2.Series("Series1").Points(89 - i).Color)
                Chart2.Series("Series1").Points(89 - i).ToolTip = Chart2.Series("Series1").Points(89 - i).AxisLabel & " " & Form3.uptimeToDHMS(Chart2.Series("Series1").Points(89 - i).YValues(0))
                Chart2.Series("Series2").Points(89 - i).Color = Color.FromArgb(228, Chart2.Series("Series2").Points(89 - i).Color)
                Chart2.Series("Series2").Points(89 - i).ToolTip = Chart2.Series("Series1").Points(89 - i).AxisLabel & " " & Form3.uptimeToDHMS(Chart2.Series("Series2").Points(89 - i).YValues(0))
                Chart2.Series("Series3").Points(89 - i).Color = Color.FromArgb(228, Chart2.Series("Series3").Points(89 - i).Color)
                Chart2.Series("Series3").Points(89 - i).ToolTip = Chart2.Series("Series1").Points(89 - i).AxisLabel & " " & Form3.uptimeToDHMS(Chart2.Series("Series3").Points(89 - i).YValues(0))
                Chart2.Series("Series4").Points(89 - i).Color = Color.FromArgb(228, Chart2.Series("Series4").Points(89 - i).Color)
                Chart2.Series("Series4").Points(89 - i).ToolTip = Chart2.Series("Series1").Points(89 - i).AxisLabel & "  " & Form3.uptimeToDHMS(Chart2.Series("Series4").Points(89 - i).YValues(0))
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
                Label22.Text = "" '("00.00")
                'Else
                'commented because it is included in Cycle OFF
                'Label22.Text = Math.Round(tmptotal / total1).ToString("00.00")
                'End If

                Label26.Text = ""
                Label39.Text = ""
                Label49.Text = ""
                Label61.Text = ""
                Label32.Text = ""
                Label45.Text = ""
                Label55.Text = ""
                Label5.Text = ""
                Label6.Text = ""
            Else
                'Label3.BackColor = Color.Transparent 'setup
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
                'Chart2.Series("Series3").Color = Color.Transparent
                'Chart2.Series("Series4").Color = Color.Transparent

                'Label26.Text = Math.Round((big_periode_returned(indx, 0).shift1.Item("SETUP")) * 100 / total).ToString("00.00")
                '''''''''''''''''''I DONT KNOW WHY THIS IS HERE
                'If total1 = 0 Then
                '    Label22.Text = ("00.00")
                'Else
                '    'commented because it is included in Cycle OFF
                '    Label22.Text = (tmptotal / total1).ToString("00.00") 'Math.Round(tmptotal / total1).ToString("00.00")
                'End If
                ''''''''''''''''''''''''''''

                'Label26.Text = ""
                'Label39.Text = ""



                'Label5.Text = ""
                'Label6.Text = ""
            End If

        Catch ex As Exception
            MessageBox.Show("Could not display the Evolution chart : " & ex.Message)
        End Try


    End Sub






    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Next
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Button23.Text = "%" Then
            Button23.Text = "h"
        End If

        Dim index_ As Integer
        index_ = ComboBox1.SelectedIndex
        If index_ = ComboBox1.Items.Count - 1 Then index_ = -1
        ComboBox1.SelectedIndex = index_ + 1

    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Prev
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If Button23.Text = "%" Then
            Button23.Text = "h"
        End If

        Dim index_ As Integer
        index_ = ComboBox1.SelectedIndex
        If index_ = 0 Then index_ = ComboBox1.Items.Count
        ComboBox1.SelectedIndex = index_ - 1
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

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If Chart2.ChartAreas(0).Area3DStyle.Enable3D = True Then
            Chart2.ChartAreas(0).Area3DStyle.Enable3D = False
            Button9.ForeColor = Color.Black

            Button9.Text = "3D"


        Else
            Chart2.ChartAreas(0).Area3DStyle.Enable3D = True
            Button9.ForeColor = Color.Red

            Button9.Text = "2D"

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
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        If Form3.days = 1 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(Form3.DateTimePicker2.Value.Day + 0.5, 89.5)
        Else
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(88 - 6 * Math.Round(Form3.DateTimePicker2.Value.Day / 7) + 0.5, 89.5)
        End If

    End Sub


    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Zoom 3 month
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        If Form3.days = 1 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(29 - Form3.DateTimePicker2.Value.Day + 0.5, 89.5)
        Else
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(88 - 3 * Math.Round(Form3.DateTimePicker2.Value.Day / 7) + 0.5, 89.5)
        End If

    End Sub


    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Zoom 1 month
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        If Form3.days = 1 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(88 - Form3.DateTimePicker2.Value.Day + 0.5, 89.5)
        Else
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(88 - Math.Round(Form3.DateTimePicker2.Value.Day / 7) + 0.5, 89.5)
        End If
    End Sub



    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Zoom 1 week
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        If Form3.days = 1 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((88 + Val(Form1.week_(0)) - Form3.DateTimePicker2.Value.DayOfWeek) + 0.5, 89.5)
        Else
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom(88.5, 89.5)
        End If
    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Reset view
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        ' 1Day
        If DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) = 0 Then
            If Form3.DateTimePicker1.Value.DayOfWeek > 1 Then
                Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Form1.week_(0)) - Form3.DateTimePicker1.Value.DayOfWeek) + 0.5, 89.5)
            Else
                Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Form1.week_(0)) - Form3.DateTimePicker1.Value.DayOfWeek) - 0.5 - 7 + Val(Form1.week_(0)), 89.5)
            End If
        End If

        ' +1Day AND < 28 DAYS
        If DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) < 30 And DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) <> 0 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) - 0.5), 89.5)
        End If



        If DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) > 29 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - Math.Round(DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) / 7) - 1) + 0.5, 89.5)
        End If
    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Data by weeks
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Form3.days = 6


        Form3.periode_returned = CSI_LIB.Detailled(Form3.DateTimePicker1.Value, Form3.DateTimePicker2.Value, Form3.read_tree(), True)
        Form3.big_periode_returned = CSI_LIB.Evolution(Form3.DateTimePicker1.Value, Form3.DateTimePicker2.Value, Form3.read_tree(), Form3.days, True)
        Call Form3.Fill_Combo_Form9(Form3.periode_returned)

        ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1

        'CHART ZOOM :----------------------------------------------------------------------------------------------

        ' 1Day
        If DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) = 0 Then
            If Form3.DateTimePicker1.Value.DayOfWeek > 1 Then
                Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Form1.week_(0)) - Form3.DateTimePicker1.Value.DayOfWeek) + 0.5, 89.5)
            Else
                Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Form1.week_(0)) - Form3.DateTimePicker1.Value.DayOfWeek) - 0.5 - 7 + Val(Form1.week_(0)), 89.5)
            End If
        End If

        ' +1Day AND < 28 DAYS
        If DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) < 30 And DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) <> 0 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) - 0.5), 89.5)
        End If

        ' +28 DAYS
        If DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) > 29 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - Math.Round(DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) / 7) - 1) + 0.5, 89.5)
        End If

    End Sub


    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Data by days
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click


        Form3.days = 1
        Form3.periode_returned = CSI_LIB.Detailled(Form3.DateTimePicker1.Value, Form3.DateTimePicker2.Value, Form3.read_tree(), True)
        Form3.big_periode_returned = CSI_LIB.Evolution(Form3.DateTimePicker1.Value, Form3.DateTimePicker2.Value, Form3.read_tree(), Form3.days, True)
        Call Form3.Fill_Combo_Form9(Form3.periode_returned)
        ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1

        'CHART ZOOM :----------------------------------------------------------------------------------------------

        ' 1Day
        If DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) = 0 Then
            If Form3.DateTimePicker1.Value.DayOfWeek > 1 Then
                Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Form1.week_(0)) - Form3.DateTimePicker1.Value.DayOfWeek) + 0.5, 89.5)
            Else
                Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 + Val(Form1.week_(0)) - Form3.DateTimePicker1.Value.DayOfWeek) - 0.5 - 7 + Val(Form1.week_(0)), 89.5)
            End If
        End If

        ' +1Day AND < 28 DAYS
        If DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) < 30 And DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) <> 0 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) - 0.5), 89.5)
        End If

        ' +28 DAYS
        If DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date) > 29 Then
            Chart2.ChartAreas(0).AxisX.ScaleView.Zoom((89 - Math.Round(DateDiff(DateInterval.Day, Form3.DateTimePicker1.Value.Date, Form3.DateTimePicker2.Value.Date)) - 1) + 0.5, 89.5)
        End If

    End Sub




    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' H <-> %
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click

        If consolidated = False Then

            If Button23.Text = "%" Then
                Button23.Text = "h"

                Label30.Text = "Shift 1 (%)"
                Label43.Text = "Shift 2 (%)"
                Label53.Text = "Shift 3 (%)"
                Label20.Text = "Periode (%)"
                Call Form3.fill_form9_shift(Form3.periode_returned, Form3.machine_form9)

                '1period
                Dim total As Double = Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Values.Sum()
                '    Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("OTHER") +
                '    Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("SETUP") +
                '    Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE OFF") +
                'Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE ON")



                If total > 0 Then
                    'Label28.Text = ((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE ON")) * 100 / total).ToString("00.00") 'Math.Round((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE ON")) * 100 / total).ToString("00.00")
                    'Label27.Text = ((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE OFF")) * 100 / total).ToString("00.00") 'Math.Round((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE OFF")) * 100 / total).ToString("00.00")

                    'If Welcome.CSIF_version <> 1 Then
                    '    Label26.Text = ((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("SETUP")) * 100 / total).ToString("00.00") 'Math.Round((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("SETUP")) * 100 / total).ToString("00.00")
                    '    Label22.Text = ((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("OTHER")) * 100 / total).ToString("00.00") 'Math.Round((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("OTHER")) * 100 / total).ToString("00.00")
                    'End If
                    'Label21.Text = "100.00"

                    Dim total_con As Double, total_coff As Double, total_setup As Double, total_other As Double

                    'total_con = Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE ON") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE ON") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE ON")
                    If Form3.periode_returned(Form3.machine_form9).shift1.ContainsKey("CYCLE ON") Then
                        total_con += Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE ON")
                    End If
                    If Form3.periode_returned(Form3.machine_form9).shift2.ContainsKey("CYCLE ON") Then
                        total_con += Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE ON")
                    End If
                    If Form3.periode_returned(Form3.machine_form9).shift3.ContainsKey("CYCLE ON") Then
                        total_con += Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE ON")
                    End If

                    'total_coff = Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")
                    If Form3.periode_returned(Form3.machine_form9).shift1.ContainsKey("CYCLE OFF") Then
                        total_coff += Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF")
                    End If
                    If Form3.periode_returned(Form3.machine_form9).shift2.ContainsKey("CYCLE OFF") Then
                        total_coff += Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF")
                    End If
                    If Form3.periode_returned(Form3.machine_form9).shift3.ContainsKey("CYCLE OFF") Then
                        total_coff += Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")
                    End If


                    If (Form3.periode_returned(Form3.machine_form9).shift1.ContainsKey("SETUP")) Then
                        total_setup += Form3.periode_returned(Form3.machine_form9).shift1.Item("SETUP")
                    End If
                    If (Form3.periode_returned(Form3.machine_form9).shift2.ContainsKey("SETUP")) Then
                        total_setup += Form3.periode_returned(Form3.machine_form9).shift2.Item("SETUP")
                    End If
                    If (Form3.periode_returned(Form3.machine_form9).shift3.ContainsKey("SETUP")) Then
                        total_setup += Form3.periode_returned(Form3.machine_form9).shift3.Item("SETUP")
                    End If

                    'total_other = Form3.periode_returned(Form3.machine_form9).shift1.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift2.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift3.Item("OTHER")
                    For Each key In (Form3.periode_returned(Form3.machine_form9).shift1.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + Form3.periode_returned(Form3.machine_form9).shift1.Item(key)
                        End If
                    Next
                    For Each key In (Form3.periode_returned(Form3.machine_form9).shift2.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + Form3.periode_returned(Form3.machine_form9).shift2.Item(key)
                        End If
                    Next
                    For Each key In (Form3.periode_returned(Form3.machine_form9).shift3.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + Form3.periode_returned(Form3.machine_form9).shift3.Item(key)
                        End If
                    Next

                    total = (total_con + total_coff + total_setup + total_other)

                    If (Welcome.CSIF_version <> 1) Then
                        Label28.Text = (total_con * 100 / total).ToString("00.00")
                        Label27.Text = (total_coff * 100 / total).ToString("00.00")
                        Label26.Text = (total_setup * 100 / total).ToString("00.00")
                        Label22.Text = (total_other * 100 / total).ToString("00.00")
                        Label21.Text = ((total_con + total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                    Else
                        Label28.Text = (total_con * 100 / total).ToString("00.00")
                        Label27.Text = ((total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                        Label26.Text = "" '("00.00")
                        Label22.Text = "" '("00.00")
                        Label21.Text = ((total_con + total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                    End If



                Else
                    Label28.Text = ("00.00")
                    Label27.Text = ("00.00")
                    Label26.Text = ("00.00")
                    Label22.Text = ("00.00")
                    Label21.Text = ("00.00")
                End If
            Else

                Button23.Text = "%"
                Label30.Text = "Shift 1 (h)"
                Label43.Text = "Shift 2 (h)"
                Label53.Text = "Shift 3 (h)"
                Label20.Text = "Periode (h)"
                Call Form3.fill_form9_shift_hours(Form3.periode_returned, Form3.machine_form9)

                Dim total As Double = Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Values.Sum()
                '    Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("OTHER") +
                '   Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("SETUP") +
                '   Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE OFF") +
                'Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE ON")

                If total > 0 Then


                    ''''''''''''''''''''''''''''''
                    Dim total_con As Double, total_coff As Double, total_setup As Double, total_other As Double
                    'total_con = Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE ON") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE ON") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE ON")
                    'total_coff = Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")

                    'total_con = Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE ON") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE ON") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE ON")
                    If Form3.periode_returned(Form3.machine_form9).shift1.ContainsKey("CYCLE ON") Then
                        total_con += Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE ON")
                    End If
                    If Form3.periode_returned(Form3.machine_form9).shift2.ContainsKey("CYCLE ON") Then
                        total_con += Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE ON")
                    End If
                    If Form3.periode_returned(Form3.machine_form9).shift3.ContainsKey("CYCLE ON") Then
                        total_con += Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE ON")
                    End If

                    'total_coff = Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")
                    If Form3.periode_returned(Form3.machine_form9).shift1.ContainsKey("CYCLE OFF") Then
                        total_coff += Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF")
                    End If
                    If Form3.periode_returned(Form3.machine_form9).shift2.ContainsKey("CYCLE OFF") Then
                        total_coff += Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF")
                    End If
                    If Form3.periode_returned(Form3.machine_form9).shift3.ContainsKey("CYCLE OFF") Then
                        total_coff += Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")
                    End If

                    If (Form3.periode_returned(Form3.machine_form9).shift1.ContainsKey("SETUP")) Then
                        total_setup += Form3.periode_returned(Form3.machine_form9).shift1.Item("SETUP")
                    End If
                    If (Form3.periode_returned(Form3.machine_form9).shift2.ContainsKey("SETUP")) Then
                        total_setup += Form3.periode_returned(Form3.machine_form9).shift2.Item("SETUP")
                    End If
                    If (Form3.periode_returned(Form3.machine_form9).shift3.ContainsKey("SETUP")) Then
                        total_setup += Form3.periode_returned(Form3.machine_form9).shift3.Item("SETUP")
                    End If

                    'total_other = Form3.periode_returned(Form3.machine_form9).shift1.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift2.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift3.Item("OTHER")
                    For Each key In (Form3.periode_returned(Form3.machine_form9).shift1.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + Form3.periode_returned(Form3.machine_form9).shift1.Item(key)
                        End If
                    Next
                    For Each key In (Form3.periode_returned(Form3.machine_form9).shift2.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + Form3.periode_returned(Form3.machine_form9).shift2.Item(key)
                        End If
                    Next
                    For Each key In (Form3.periode_returned(Form3.machine_form9).shift3.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + Form3.periode_returned(Form3.machine_form9).shift3.Item(key)
                        End If
                    Next

                    If (Welcome.CSIF_version <> 1) Then
                        Label28.Text = Form3.uptimeToDHMS(total_con)
                        Label27.Text = Form3.uptimeToDHMS(total_coff)
                        Label26.Text = Form3.uptimeToDHMS(total_setup)
                        Label22.Text = Form3.uptimeToDHMS(total_other)
                        Label21.Text = Form3.uptimeToDHMS(total_con + total_coff + total_setup + total_other)
                    Else
                        Label28.Text = Form3.uptimeToDHMS(total_con)
                        Label27.Text = Form3.uptimeToDHMS(total_coff + total_setup + total_other)
                        Label26.Text = "" '("00:00")
                        Label22.Text = "" '("00:00")
                        Label21.Text = Form3.uptimeToDHMS(total_con + total_coff + total_setup + total_other)
                    End If

                Else
                    Label28.Text = ("00:00")
                    Label27.Text = ("00:00")
                    Label26.Text = ("00:00")
                    Label22.Text = ("00:00")
                    Label21.Text = ("00:00")

                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''

                'Label28.Text = (Form3.uptimeToDHMS(Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE ON")))
                'Label27.Text = (Form3.uptimeToDHMS(Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE OFF")))
                'If Welcome.CSIF_version <> 1 Then
                '    Label26.Text = (Form3.uptimeToDHMS(Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("SETUP")))
                '    Label22.Text = (Form3.uptimeToDHMS(Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("OTHER")))
                'End If

                'Label21.Text = Form3.uptimeToDHMS(total)

            End If

        Else ' Consolidated

            ''''''''''''''''''''''''ORIGINAL CODE
            'If Button23.Text = "%" Then
            '    'Button23.Text = "h"
            '    'Label30.Text = "Shift 1 (%)"
            '    'Label43.Text = "Shift 2 (%)"
            '    'Label53.Text = "Shift 3 (%)"
            '    'Label20.Text = "Periode (%)"

            '    'Call Form3.fill_form9_shift(consolidated_periode, 0)

            '    'Dim total As Double = Big_consolidated_periode(0, 0).shift1.Item("OTHER") +
            '    '  Big_consolidated_periode(0, 0).shift1.Item("SETUP") +
            '    '  Big_consolidated_periode(0, 0).shift1.Item("CYCLE OFF") +
            '    '   Big_consolidated_periode(0, 0).shift1.Item("CYCLE ON")
            '    'If total <> 0 Then
            '    '    'Label28.Text = Math.Round((Big_consolidated_periode(0, 0).shift1.Item("CYCLE ON")) * 100 / total).ToString("00.00")
            '    '    'Label27.Text = Math.Round((Big_consolidated_periode(0, 0).shift1.Item("CYCLE OFF")) * 100 / total).ToString("00.00")
            '    '    'Label26.Text = Math.Round((Big_consolidated_periode(0, 0).shift1.Item("SETUP")) * 100 / total).ToString("00.00")
            '    '    'Label22.Text = Math.Round((Big_consolidated_periode(0, 0).shift1.Item("OTHER")) * 100 / total).ToString("00.00")
            '    '    Label28.Text = ((Big_consolidated_periode(0, 0).shift1.Item("CYCLE ON")) * 100 / total).ToString("00.00")
            '    '    Label27.Text = ((Big_consolidated_periode(0, 0).shift1.Item("CYCLE OFF")) * 100 / total).ToString("00.00")
            '    '    Label26.Text = ((Big_consolidated_periode(0, 0).shift1.Item("SETUP")) * 100 / total).ToString("00.00")
            '    '    Label22.Text = ((Big_consolidated_periode(0, 0).shift1.Item("OTHER")) * 100 / total).ToString("00.00")
            '    '    Label21.Text = "100.00"
            '    'Else
            '    '    Label28.Text = ("00.00")
            '    '    Label27.Text = ("00.00")
            '    '    Label26.Text = ("00.00")
            '    '    Label22.Text = ("00.00")
            '    '    Label21.Text = ("00.00")
            '    'End If


            '    'Else
            '    '    Button23.Text = "%"
            '    '    Label30.Text = "Shift 1 (h)"
            '    '    Label43.Text = "Shift 2 (h)"
            '    '    Label53.Text = "Shift 3 (h)"
            '    '    Label20.Text = "Periode (h)"

            '    '    'Call Form3.fill_form9_shift_hours(consolidated_periode, 0)
            '    '    Call Form3.fill_form9_shift(consolidated_periode, 0)

            '    '    Dim total As Double = Big_consolidated_periode(0, 0).shift1.Item("OTHER") +
            '    '   Big_consolidated_periode(0, 0).shift1.Item("SETUP") +
            '    '   Big_consolidated_periode(0, 0).shift1.Item("CYCLE OFF") +
            '    '    Big_consolidated_periode(0, 0).shift1.Item("CYCLE ON")

            '    '    Label28.Text = (Form3.uptimeToDHMS(Big_consolidated_periode(0, 0).shift1.Item("CYCLE ON")))
            '    '    Label27.Text = (Form3.uptimeToDHMS(Big_consolidated_periode(0, 0).shift1.Item("CYCLE OFF")))
            '    '    Label26.Text = (Form3.uptimeToDHMS(Big_consolidated_periode(0, 0).shift1.Item("SETUP")))
            '    '    Label22.Text = (Form3.uptimeToDHMS(Big_consolidated_periode(0, 0).shift1.Item("OTHER")))
            '    '    Label21.Text = Form3.uptimeToDHMS(total)

            'End If

            '''''''''''''''''MODIFIED FROM NOT CONSOLIDATED

            If Button23.Text = "%" Then
                Button23.Text = "h"

                Label30.Text = "Shift 1 (%)"
                Label43.Text = "Shift 2 (%)"
                Label53.Text = "Shift 3 (%)"
                Label20.Text = "Periode (%)"
                'Call Form3.fill_form9_shift(Form3.periode_returned, Form3.machine_form9)
                Call Form3.fill_form9_shift(consolidated_periode, 0)

                '1period
                'Dim total As Double = Big_consolidated_periode(0, 0).shift1.Item("OTHER") +
                '  Big_consolidated_periode(0, 0).shift1.Item("SETUP") +
                '  Big_consolidated_periode(0, 0).shift1.Item("CYCLE OFF") +
                '   Big_consolidated_periode(0, 0).shift1.Item("CYCLE ON")

                Dim total As Double = consolidated_periode(0).shift1.Values.Sum()
                '  consolidated_periode(0).shift1.Item("OTHER") +
                'consolidated_periode(0).shift1.Item("SETUP") +
                'consolidated_periode(0).shift1.Item("CYCLE OFF") +
                ' consolidated_periode(0).shift1.Item("CYCLE ON")


                'Dim total As Double = Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("OTHER") +
                '    Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("SETUP") +
                '    Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE OFF") +
                '    Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE ON")



                If total > 0 Then
                    'Label28.Text = ((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE ON")) * 100 / total).ToString("00.00") 'Math.Round((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE ON")) * 100 / total).ToString("00.00")
                    'Label27.Text = ((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE OFF")) * 100 / total).ToString("00.00") 'Math.Round((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE OFF")) * 100 / total).ToString("00.00")

                    'If Welcome.CSIF_version <> 1 Then
                    '    Label26.Text = ((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("SETUP")) * 100 / total).ToString("00.00") 'Math.Round((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("SETUP")) * 100 / total).ToString("00.00")
                    '    Label22.Text = ((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("OTHER")) * 100 / total).ToString("00.00") 'Math.Round((Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("OTHER")) * 100 / total).ToString("00.00")
                    'End If
                    'Label21.Text = "100.00"

                    Dim total_con As Double, total_coff As Double, total_setup As Double, total_other As Double
                    'Label28.Text = ((Big_consolidated_periode(0, 0).shift1.Item("CYCLE ON")) * 100 / total).ToString("00.00")
                    total_con = consolidated_periode(0).shift1.Item("CYCLE ON") + consolidated_periode(0).shift2.Item("CYCLE ON") + consolidated_periode(0).shift3.Item("CYCLE ON") '+ Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE ON") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE ON")
                    total_coff = consolidated_periode(0).shift1.Item("CYCLE OFF") + consolidated_periode(0).shift2.Item("CYCLE OFF") + consolidated_periode(0).shift3.Item("CYCLE OFF") 'Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")
                    total_setup = consolidated_periode(0).shift1.Item("SETUP") + consolidated_periode(0).shift2.Item("SETUP") + consolidated_periode(0).shift3.Item("SETUP") 'Form3.periode_returned(Form3.machine_form9).shift1.Item("SETUP") + Form3.periode_returned(Form3.machine_form9).shift2.Item("SETUP") + Form3.periode_returned(Form3.machine_form9).shift3.Item("SETUP")
                    'total_other = Form3.periode_returned(Form3.machine_form9).shift1.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift2.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift3.Item("OTHER")

                    For Each key In (consolidated_periode(0).shift1.Keys) '(Form3.periode_returned(Form3.machine_form9).shift1.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + consolidated_periode(0).shift1.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift1.Item(key)
                        End If
                    Next
                    For Each key In (consolidated_periode(0).shift2.Keys) '(Form3.periode_returned(Form3.machine_form9).shift2.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + consolidated_periode(0).shift2.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift2.Item(key)
                        End If
                    Next
                    For Each key In (consolidated_periode(0).shift3.Keys) '(Form3.periode_returned(Form3.machine_form9).shift3.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + consolidated_periode(0).shift3.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift3.Item(key)
                        End If
                    Next

                    total = (total_con + total_coff + total_setup + total_other)


                    If (Welcome.CSIF_version <> 1) Then
                        Label28.Text = (total_con * 100 / total).ToString("00.00")
                        Label27.Text = (total_coff * 100 / total).ToString("00.00")
                        Label26.Text = (total_setup * 100 / total).ToString("00.00")
                        Label22.Text = (total_other * 100 / total).ToString("00.00")
                        Label21.Text = ((total_con + total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                    Else
                        Label28.Text = (total_con * 100 / total).ToString("00.00")
                        Label27.Text = ((total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                        Label26.Text = "" '("00.00")
                        Label22.Text = "" '("00.00")
                        Label21.Text = ((total_con + total_coff + total_setup + total_other) * 100 / total).ToString("00.00")
                    End If



                Else
                    Label28.Text = ("00.00")
                    Label27.Text = ("00.00")
                    Label26.Text = ("00.00")
                    Label22.Text = ("00.00")
                    Label21.Text = ("00.00")
                End If
            Else

                Button23.Text = "%"
                Label30.Text = "Shift 1 (h)"
                Label43.Text = "Shift 2 (h)"
                Label53.Text = "Shift 3 (h)"
                Label20.Text = "Periode (h)"

                'Call Form3.fill_form9_shift_hours(Form3.periode_returned, Form3.machine_form9)
                Call Form3.fill_form9_shift_hours(consolidated_periode, 0)

                'Dim total As Double = Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("OTHER") +
                '   Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("SETUP") +
                '   Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE OFF") +
                '   Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE ON")

                Dim total As Double = consolidated_periode(0).shift1.Values.Sum()
                'consolidated_periode(0).shift1.Item("SETUP") +
                'consolidated_periode(0).shift1.Item("CYCLE OFF") +
                ' consolidated_periode(0).shift1.Item("CYCLE ON")

                If total > 0 Then


                    ''''''''''''''''''''''''''''''
                    Dim total_con As Double, total_coff As Double, total_setup As Double, total_other As Double
                    total_con = consolidated_periode(0).shift1.Item("CYCLE ON") + consolidated_periode(0).shift2.Item("CYCLE ON") + consolidated_periode(0).shift3.Item("CYCLE ON") ' + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE ON") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE ON")
                    total_coff = consolidated_periode(0).shift1.Item("CYCLE OFF") + consolidated_periode(0).shift2.Item("CYCLE OFF") + consolidated_periode(0).shift3.Item("CYCLE OFF") 'Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")
                    total_setup = consolidated_periode(0).shift1.Item("SETUP") + consolidated_periode(0).shift2.Item("SETUP") + consolidated_periode(0).shift3.Item("SETUP") 'Form3.periode_returned(Form3.machine_form9).shift1.Item("SETUP") + Form3.periode_returned(Form3.machine_form9).shift2.Item("SETUP") + Form3.periode_returned(Form3.machine_form9).shift3.Item("SETUP")
                    'total_other = Form3.periode_returned(Form3.machine_form9).shift1.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift2.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift3.Item("OTHER")

                    For Each key In (consolidated_periode(0).shift1.Keys) '(Form3.periode_returned(Form3.machine_form9).shift1.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + consolidated_periode(0).shift1.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift1.Item(key)
                        End If
                    Next
                    For Each key In (consolidated_periode(0).shift2.Keys) '(Form3.periode_returned(Form3.machine_form9).shift2.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + consolidated_periode(0).shift2.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift2.Item(key)
                        End If
                    Next
                    For Each key In (consolidated_periode(0).shift3.Keys) '(Form3.periode_returned(Form3.machine_form9).shift3.Keys)
                        If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTNO")) Then
                            total_other = total_other + consolidated_periode(0).shift3.Item(key) 'Form3.periode_returned(Form3.machine_form9).shift3.Item(key)
                        End If
                    Next

                    '(Form3.uptimeToDHMS(Big_consolidated_periode(0, 0).shift1.Item("CYCLE ON")))
                    If (Welcome.CSIF_version <> 1) Then
                        Label28.Text = Form3.uptimeToDHMS(total_con)
                        Label27.Text = Form3.uptimeToDHMS(total_coff)
                        Label26.Text = Form3.uptimeToDHMS(total_setup)
                        Label22.Text = Form3.uptimeToDHMS(total_other)
                        Label21.Text = Form3.uptimeToDHMS(total_con + total_coff + total_setup + total_other)
                    Else
                        Label28.Text = Form3.uptimeToDHMS(total_con)
                        Label27.Text = Form3.uptimeToDHMS(total_coff + total_setup + total_other)
                        Label26.Text = "" '("00:00")
                        Label22.Text = "" '("00:00")
                        Label21.Text = Form3.uptimeToDHMS(total_con + total_coff + total_setup + total_other)
                    End If

                Else
                    Label28.Text = ("00:00")
                    Label27.Text = ("00:00")
                    Label26.Text = ("00:00")
                    Label22.Text = ("00:00")
                    Label21.Text = ("00:00")

                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''

                'Label28.Text = (Form3.uptimeToDHMS(Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE ON")))
                'Label27.Text = (Form3.uptimeToDHMS(Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("CYCLE OFF")))
                'If Welcome.CSIF_version <> 1 Then
                '    Label26.Text = (Form3.uptimeToDHMS(Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("SETUP")))
                '    Label22.Text = (Form3.uptimeToDHMS(Form3.big_periode_returned(Form3.machine_form9, 0).shift1.Item("OTHER")))
                'End If

                'Label21.Text = Form3.uptimeToDHMS(total)

            End If

        End If
        If Welcome.CSIF_version = 1 Then Label5.Text = ""
        If Welcome.CSIF_version = 1 Then Label6.Text = ""
    End Sub











    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Clic on the pie chart / form9
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

        If Welcome.CSIF_version <> 1 Then
            Chart1.Cursor = Cursors.Hand
            If consolidated = False Then
                Form7.ComboBox1.Items.Clear()
                Form3.periode_returned = CSI_LIB.Detailled(Form3.DateTimePicker1.Value, Form3.DateTimePicker2.Value, Form3.read_tree(), True)
                Call Form3.Fill_Combobox_detailled(Form3.periode_returned)

                Form7.MdiParent = Form1


                Form7.ComboBox1.SelectedIndex = Form3.machine_form9
                Me.Visible = False

            Else
                Form7.ComboBox1.Items.Clear()

                Call Form3.Fill_Combobox_detailled(consolidated_periode)


                Form7.MdiParent = Form1

                Form7.ComboBox1.SelectedIndex = 0
                Form7.SuspendLayout()
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
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If Button23.Text = "%" Then
            Button23.Text = "h"
        End If

        consolidate()
    End Sub

    Private Sub consolidate()

        Try

            If CheckBox1.Checked = True Then
                consolidated = True
                CheckBox1.Text = "Unbind"
                CheckBox1.ForeColor = Color.Black

                consolidated_periode(0).date_ = Form3.periode_returned(0).date_
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
                For i = 0 To (UBound(Form3.periode_returned) - 1)
                    For Each key In Form3.periode_returned(i).shift1

                        Select Case key.Key
                            Case "CYCLE ON"
                                consolidated_periode(0).shift1.Item("CYCLE ON") = consolidated_periode(0).shift1.Item("CYCLE ON") + Form3.periode_returned(i).shift1.Item("CYCLE ON")
                            Case "CYCLE OFF"
                                consolidated_periode(0).shift1.Item("CYCLE OFF") = consolidated_periode(0).shift1.Item("CYCLE OFF") + Form3.periode_returned(i).shift1.Item("CYCLE OFF")
                            Case "SETUP"
                                consolidated_periode(0).shift1.Item("SETUP") = consolidated_periode(0).shift1.Item("SETUP") + Form3.periode_returned(i).shift1.Item("SETUP")
                            Case Else
                                'If Not key.Key.Contains("PARTNO") Then consolidated_periode(0).shift1.Item("OTHER") = consolidated_periode(0).shift1.Item("OTHER") + Form3.periode_returned(i).shift1.Item(key.Key)
                                If (consolidated_periode(0).shift1.ContainsKey(key.Key)) Then
                                    consolidated_periode(0).shift1.Item(key.Key) = consolidated_periode(0).shift1.Item(key.Key) + Form3.periode_returned(i).shift1.Item(key.Key)
                                Else
                                    consolidated_periode(0).shift1.Add(key.Key, Form3.periode_returned(i).shift1.Item(key.Key))
                                End If
                        End Select
                    Next

                    For Each key In Form3.periode_returned(i).shift2
                        Select Case key.Key
                            Case "CYCLE ON"
                                consolidated_periode(0).shift2.Item("CYCLE ON") = consolidated_periode(0).shift2.Item("CYCLE ON") + Form3.periode_returned(i).shift2.Item("CYCLE ON")
                            Case "CYCLE OFF"
                                consolidated_periode(0).shift2.Item("CYCLE OFF") = consolidated_periode(0).shift2.Item("CYCLE OFF") + Form3.periode_returned(i).shift2.Item("CYCLE OFF")
                            Case "SETUP"
                                consolidated_periode(0).shift2.Item("SETUP") = consolidated_periode(0).shift2.Item("SETUP") + Form3.periode_returned(i).shift2.Item("SETUP")
                            Case Else
                                'If Not key.Key.Contains("PARTNO") Then consolidated_periode(0).shift2.Item("OTHER") = consolidated_periode(0).shift2.Item("OTHER") + Form3.periode_returned(i).shift2.Item(key.Key)
                                If (consolidated_periode(0).shift2.ContainsKey(key.Key)) Then
                                    consolidated_periode(0).shift2.Item(key.Key) = consolidated_periode(0).shift2.Item(key.Key) + Form3.periode_returned(i).shift2.Item(key.Key)
                                Else
                                    consolidated_periode(0).shift2.Add(key.Key, Form3.periode_returned(i).shift2.Item(key.Key))
                                End If
                        End Select
                    Next

                    For Each key In Form3.periode_returned(i).shift3
                        Select Case key.Key
                            Case "CYCLE ON"
                                consolidated_periode(0).shift3.Item("CYCLE ON") = consolidated_periode(0).shift3.Item("CYCLE ON") + Form3.periode_returned(i).shift3.Item("CYCLE ON")
                            Case "CYCLE OFF"
                                consolidated_periode(0).shift3.Item("CYCLE OFF") = consolidated_periode(0).shift3.Item("CYCLE OFF") + Form3.periode_returned(i).shift3.Item("CYCLE OFF")
                            Case "SETUP"
                                consolidated_periode(0).shift3.Item("SETUP") = consolidated_periode(0).shift3.Item("SETUP") + Form3.periode_returned(i).shift3.Item("SETUP")
                            Case Else
                                'If Not key.Key.Contains("PARTNO") Then consolidated_periode(0).shift3.Item("OTHER") = consolidated_periode(0).shift3.Item("OTHER") + Form3.periode_returned(i).shift3.Item(key.Key)
                                If (consolidated_periode(0).shift3.ContainsKey(key.Key)) Then
                                    consolidated_periode(0).shift3.Item(key.Key) = consolidated_periode(0).shift3.Item(key.Key) + Form3.periode_returned(i).shift3.Item(key.Key)
                                Else
                                    consolidated_periode(0).shift3.Add(key.Key, Form3.periode_returned(i).shift3.Item(key.Key))
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

                    For j = 0 To UBound(Form3.big_periode_returned, 1) ' Row, machines ... 
                        Big_consolidated_periode(0, i).date_ = Form3.big_periode_returned(j, i).date_
                        Big_consolidated_periode(0, i).shift1.Item("CYCLE ON") = Big_consolidated_periode(0, i).shift1.Item("CYCLE ON") + Val(Form3.big_periode_returned(j, i).shift1.Item("CYCLE ON"))
                        Big_consolidated_periode(0, i).shift1.Item("CYCLE OFF") = Big_consolidated_periode(0, i).shift1.Item("CYCLE OFF") + Val(Form3.big_periode_returned(j, i).shift1.Item("CYCLE OFF"))
                        Big_consolidated_periode(0, i).shift1.Item("SETUP") = Big_consolidated_periode(0, i).shift1.Item("SETUP") + Val(Form3.big_periode_returned(j, i).shift1.Item("SETUP"))
                        Big_consolidated_periode(0, i).shift1.Item("OTHER") = Big_consolidated_periode(0, i).shift1.Item("OTHER") + Val(Form3.big_periode_returned(j, i).shift1.Item("OTHER"))
                    Next
                Next

                ComboBox1.Items.Clear()

                Form3.Fill_Combo_Form9(consolidated_periode)

                refresh_form9(Big_consolidated_periode, consolidated_periode, 0)
                ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1

                ComboBox1.Enabled = False

            Else

                consolidated = False
                CheckBox1.Text = "Consolidate"
                ' CheckBox1.ForeColor = Color.WhiteSmoke
                ComboBox1.Enabled = True
                ComboBox1.Items.Clear()

                Form3.Fill_Combo_Form9(Form3.periode_returned)

                refresh_form9(Form3.big_periode_returned, Form3.periode_returned, Form3.machine_form9)
                ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1

            End If
        Catch ex As Exception
            MessageBox.Show("Could not consolidate the data : " & ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub


    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs)
        Form6.Location = GroupBox2.Location + Me.Location



        Form6.Show()


    End Sub



    Private Sub Chart2_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart2.Click



        If (e.Button.CompareTo(Forms.MouseButtons.Right) = 0) Then
            Chart2.ChartAreas(0).AxisX.ScaleView.ZoomReset()
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
            MessageBox.Show(ex.Message)
            Return 0
        End Try
    End Function


    '-----------------------------------------------------------------------------------------------------------------------
    ' MOVE FORME 9
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Dim _MouseDown As Boolean, _MouseX As Integer, _MouseY As Integer
    Private Sub form9_mouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        _MouseDown = True
        _MouseX = e.X
        _MouseY = e.Y

    End Sub

    Private Sub Form9_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        _MouseDown = False

    End Sub


    Private Sub Form9_Mousemove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove


        If _MouseDown = True Then
            Form1.SuspendLayout()
            Me.Left = Me.Left + (e.X - _MouseX)
            Me.Top = Me.Top + (e.Y - _MouseY)
            'If Me.Top < 20 Then Me.Top = 0
            'If Me.Left < 20 Then Me.Left = 0
            Form1.ResumeLayout(True)

        End If

    End Sub




    Private Sub Chart2_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart2.DoubleClick
        Dim tmp_periode_returned As periode()
        If Welcome.CSIF_version <> 1 Then
            If (e.Button.CompareTo(Forms.MouseButtons.Left) = 0) Then
                Dim HTR As HitTestResult
                '  Dim SelectDataPoint As DataPoint

                HTR = Chart2.HitTest(e.X, e.Y)
                ' SelectDataPoint = Chart2.Series(0).Points(HTR.PointIndex)
                If (HTR.PointIndex >= 0 And HTR.PointIndex <= 89) Then
                    Dim date1 As Date, date2 As Date
                    Dim date_ As String() = Split(Chart2.Series("Series1").Points(HTR.PointIndex).AxisLabel.ToString, " ")
                    Dim cultures As CultureInfo = New CultureInfo("fr-FR")
                    If Form3.days = 1 Then
                        date1 = Convert.ToDateTime((date_(0) & "," & MONTH_(date_(1)) & ", 20" & date_(2)), cultures)
                        date2 = date1
                    End If

                    If Form3.days > 1 Then
                        If MONTH_(date_(2)) <= MONTH_(date_(5)) Then
                            date1 = Convert.ToDateTime((date_(1) & "," & MONTH_(date_(2)) & ", 20" & date_(6)), cultures)
                        Else
                            date1 = Convert.ToDateTime((date_(1) & "," & MONTH_(date_(2)) & ", 20" & (Val(date_(6)) - 1).ToString), cultures)
                        End If
                        date2 = Convert.ToDateTime((date_(4) & "," & MONTH_(date_(5)) & ", 20" & date_(6)), cultures)
                    End If

                    If consolidated = False Then
                        Form7.ComboBox1.Items.Clear()
                        Dim Text As String() = Split(ComboBox1.Text, " : ")
                        Dim machine(0) As String
                        machine(0) = Text(0)


                        tmp_periode_returned = CSI_LIB.Detailled(date1, date2, machine, True)
                        Call Form3.Fill_Combobox_detailled(tmp_periode_returned)
                        Form7.ComboBox1.SelectedIndex = 0
                        Form7.MdiParent = Form1

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

    Private Sub printDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        Dim lWidth As Integer = e.MarginBounds.X + (e.MarginBounds.Width - memoryImage.Width) \ 2
        Dim lHeight As Integer = e.MarginBounds.Y + (e.MarginBounds.Height - memoryImage.Height) \ 2
        '    e.Graphics.DrawImage(mPrintBitMap, lWidth, lheight)
        e.Graphics.DrawImage(memoryImage, lWidth, lHeight)
    End Sub

    Dim memoryImage As New Bitmap(Me.Size.Width, Me.Size.Height)

    Private Sub CaptureScreen()

        'memoryImage.SetResolution(600, 600)
        Dim myGraphics As Graphics = Me.CreateGraphics()
        myGraphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        myGraphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        myGraphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        Dim s As Drawing.Size = Me.Size
        memoryImage = New Bitmap(s.Width, s.Height, myGraphics)
        'memoryImage.SetResolution(600, 600)
        Dim memoryGraphics As Graphics = Graphics.FromImage(memoryImage)
        memoryGraphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        memoryGraphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        memoryGraphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        memoryGraphics.CopyFromScreen(Me.Location.X, Me.Location.Y, 0, 0, s)
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
            Dim margins As New System.Drawing.Printing.Margins(5, 0, 120, 100)



            Dim ps As New PageSettings
            ps.Landscape = True
            ps.Margins = margins
            PrintDocument1.DefaultPageSettings = ps

            PrintDialog1.Document = PrintDocument1
            PrintDialog1.PrinterSettings.DefaultPageSettings.Landscape = True

            If (PrintDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then

                PrintPreviewDialog1.Document = PrintDocument1
                PrintPreviewDialog1.ShowDialog()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub Chart2_PostPaint(sender As Object, e As ChartPaintEventArgs) Handles Chart2.PostPaint
        'Dim o As Object = e.ChartElement

        'If o.[GetType]().Equals(GetType(System.Windows.Forms.DataVisualization.Charting.Series)) Then
        '    'Dim xVal As Single = CSng(e.ChartGraphics.GetPositionFromAxis(e.Chart.ChartAreas(0).Name, System.Windows.Forms.DataVisualization.Charting.AxisName.X, 50))
        '    'Dim pt As PointF = e.ChartGraphics.GetAbsolutePoint(New PointF(xVal, 0))
        '    'e.ChartGraphics.Graphics.DrawLine(Pens.Red, pt, New PointF(pt.X, e.Chart.ClientSize.Height))

        '    Dim targettext As String = e.Chart.ChartAreas(0).AxisY.StripLines(0).Text
        '    Dim drawfont As Font = New Font("Arial", 20)
        '    Dim drawbrush As SolidBrush = New SolidBrush(Color.Black)

        '    e.ChartGraphics.Graphics.DrawString(targettext, drawfont, drawbrush, 10, 10)
        'End If
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click

    End Sub
End Class
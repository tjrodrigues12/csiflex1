Public Class Machine_util_det

    Public CSI_LIB As New CSI_Library.CSI_Library
    Public data(4) As Integer ' for the pie chart
    Public top4 As New Dictionary(Of String, Integer)
    Public machine_util_det_wasactive As Boolean
    Public periode_analysed As CSI_Library.CSI_DATA.periode()
    Public index_selected As Integer
    Public Shift1_other, Shift2_other, Shift3_other As New Dictionary(Of String, String)
    Private Sub Machine_util_det_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TopMost = True
        MdiParent = Reporting_application

        periode_analysed = Report_BarChart.temporary_periode_returned
        index_selected = Report_BarChart.CB_Report.SelectedIndex
        LBL_period.Text = periode_analysed(index_selected).date_

        LBL_machinename.Text = CSI_LIB.RealNameMachine(periode_analysed(index_selected).machine_name)

        Me.Location = New Point(Report_BarChart.Size.Width + Config_report.Size.Width, Report_BarChart.Location.Y)
        Me.Height = Report_BarChart.Height

        machine_util_det_wasactive = True

        fill_shift1()
        fill_shift2()
        fill_shift3()

        for_period()

        find_top4_reason()

        tooltip()

        Color__()
        Reporting_application.PerformLayout()

    End Sub

    Dim backcolors As Color
    Dim backcolors2 As Color
    Dim backcolors3 As Color

    Dim foreColor As Color
    Dim foreColor2 As Color
    Dim foreColor3 As Color

    Private Sub Color__()
        Dim alpha As Integer = 255




        backcolors = System.Drawing.ColorTranslator.FromWin32(Report_BarChart.colors("CYCLE ON"))
        backcolors = Color.FromArgb(alpha, backcolors.R, backcolors.G, backcolors.B)
        foreColor = Report_BarChart.getContrastColor(backcolors)

        Label1.BackColor = backcolors
        LBL_CycleOn_Period.BackColor = backcolors
        LBL_CycleOn_Shift1.BackColor = backcolors
        LBL_CycleOn_Shift2.BackColor = backcolors
        LBL_CycleOn_Shift3.BackColor = backcolors
        Label1.ForeColor = ForeColor
        LBL_CycleOn_Period.ForeColor = ForeColor
        LBL_CycleOn_Shift1.ForeColor = ForeColor
        LBL_CycleOn_Shift2.ForeColor = ForeColor
        LBL_CycleOn_Shift3.ForeColor = foreColor



        backcolors2 = System.Drawing.ColorTranslator.FromWin32(Report_BarChart.colors("CYCLE OFF"))
        backcolors2 = Color.FromArgb(alpha, backcolors2.R, backcolors2.G, backcolors2.B)
        foreColor2 = Report_BarChart.getContrastColor(backcolors2)

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



        backcolors3 = System.Drawing.ColorTranslator.FromWin32(Report_BarChart.colors("SETUP"))
        backcolors3 = Color.FromArgb(alpha, backcolors3.R, backcolors3.G, backcolors3.B)
        foreColor3 = Report_BarChart.getContrastColor(backcolors3)
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

        Label7.BackColor = Report_BarChart.Other_color

        LBL_Other_Period.BackColor = Report_BarChart.Other_color
        LBL_Other_Shift1.BackColor = Report_BarChart.Other_color
        LBL_Other_Shift2.BackColor = Report_BarChart.Other_color
        LBL_Other_Shift3.BackColor = Report_BarChart.Other_color

        Chart1.Series("Series1").Points.Item(0).Color = backcolors
        Chart1.Series("Series1").Points.Item(1).Color = backcolors2
        Chart1.Series("Series1").Points.Item(2).Color = backcolors3
        Chart1.Series("Series1").Points.Item(3).Color = Report_BarChart.Other_color
        'Chart1.Series("Series1").Points(3).CustomProperties = "Exploded = true"
    End Sub
    Private Sub tooltip()

        Dim tt1 As ToolTip = New ToolTip
        Dim tt2 As ToolTip = New ToolTip
        Dim tt3 As ToolTip = New ToolTip

        Dim tooltip_text As String = ""
        For Each key In Shift1_other.Keys
            tooltip_text = tooltip_text + vbCrLf + key + " : " + Shift1_other.Item(key)
        Next
        tt1.SetToolTip(LBL_Other_Shift1, tooltip_text)
        tooltip_text = ""
        For Each key In Shift2_other.Keys
            tooltip_text = tooltip_text + vbCrLf + key + " : " + Shift2_other.Item(key)
        Next
        tt2.SetToolTip(LBL_Other_Shift2, tooltip_text)
        tooltip_text = ""
        For Each key In Shift3_other.Keys
            tooltip_text = tooltip_text + vbCrLf + key + " : " + Shift3_other.Item(key)
        Next
        tt3.SetToolTip(LBL_Other_Shift3, tooltip_text)
        tooltip_text = ""



        'Chart1.Series("Series1").Points(89 - i).ToolTip = Chart2.Series("Series1").Points(89 - i).AxisLabel & " " & Config_report.uptimeToDHMS(Chart2.Series("Series1").Points(89 - i).YValues(0))

        ' Config_report.uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE ON"))
    End Sub
    Private Sub Machine_util_det_acvti(sender As Object, e As EventArgs) Handles MyBase.Activated

        Me.Location = New Point(Report_BarChart.Size.Width + Config_report.Size.Width, Report_BarChart.Location.Y)
        ' MsgBox(Reporting_application.Location.X)

        ' Me.Location = New Point(Report_BarChart.Location.X, Report_BarChart.Location.Y + Report_BarChart.GroupBox2.Location.Y - 30)
    End Sub
    Private Sub machine_util_det_close(sender As Object, e As EventArgs) Handles MyBase.Closed
        machine_util_det_wasactive = False
    End Sub

    Private Sub fill_shift1()
        Dim calc As Double = 0, total As Double = 0, verif As Double = 0
        Shift1_other.Clear()

        'Shift1 percentage :
        For Each key In periode_analysed(index_selected).shift1.Keys

            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_analysed(index_selected).shift1.Item(key)
        Next

        If (total <> 0) Then

            If BTN_Unit.Text = "h" Then

                If periode_analysed(index_selected).shift1.ContainsKey("CYCLE ON") Then
                    LBL_CycleOn_Shift1.Text = (periode_analysed(index_selected).shift1.Item("CYCLE ON") * 100 / total).ToString("00.00")
                    verif = periode_analysed(index_selected).shift1.Item("CYCLE ON")
                Else
                    LBL_CycleOn_Shift1.Text = ("00.00")
                End If

                If periode_analysed(index_selected).shift1.ContainsKey("CYCLE OFF") Then

                    LBL_CycleOff_Shift1.Text = (periode_analysed(index_selected).shift1.Item("CYCLE OFF") * 100 / total).ToString("00.00") 'Math.Round(periode_analysed(index_selected).shift1.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                    verif = verif + periode_analysed(index_selected).shift1.Item("CYCLE OFF")
                Else
                    LBL_CycleOff_Shift1.Text = ("00.00")
                End If

                If periode_analysed(index_selected).shift1.ContainsKey("SETUP") Then
                    LBL_Setup_Shift1.Text = (periode_analysed(index_selected).shift1.Item("SETUP") * 100 / total).ToString("00.00") 'Math.Round(periode_analysed(index_selected).shift1.Item("SETUP") * 100 / total).ToString("00.00")
                    verif = verif + periode_analysed(index_selected).shift1.Item("SETUP")
                Else
                    LBL_Setup_Shift1.Text = "00.00"
                End If

            Else

                If periode_analysed(index_selected).shift1.ContainsKey("CYCLE ON") Then

                    LBL_CycleOn_Shift1.Text = Config_report.uptimeToDHMS(periode_analysed(index_selected).shift1.Item("CYCLE ON"))
                    verif = periode_analysed(index_selected).shift1.Item("CYCLE ON")
                Else
                    LBL_CycleOn_Shift1.Text = ("00:00")
                End If

                If periode_analysed(index_selected).shift1.ContainsKey("CYCLE OFF") Then

                    LBL_CycleOff_Shift1.Text = Config_report.uptimeToDHMS(periode_analysed(index_selected).shift1.Item("CYCLE OFF"))
                    verif = verif + periode_analysed(index_selected).shift1.Item("CYCLE OFF")
                Else
                    LBL_CycleOff_Shift1.Text = ("00:00")
                End If

                If periode_analysed(index_selected).shift1.ContainsKey("SETUP") Then
                    LBL_Setup_Shift1.Text = Config_report.uptimeToDHMS(periode_analysed(index_selected).shift1.Item("SETUP"))
                    verif = verif + periode_analysed(index_selected).shift1.Item("SETUP")
                Else
                    LBL_Setup_Shift1.Text = "00:00"
                End If

            End If
            If Welcome.CSIF_version = 1 Then LBL_Setup_Shift1.Text = ""
            For Each key In (periode_analysed(index_selected).shift1.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then

                    Shift1_other.Add(key, Config_report.uptimeToDHMS(periode_analysed(index_selected).shift1.Item(key)))

                    calc = calc + periode_analysed(index_selected).shift1.Item(key)

                    If top4.ContainsKey(key) Then
                        top4.Item(key) = top4.Item(key) + periode_analysed(index_selected).shift1.Item(key)
                    Else
                        top4.Add(key, periode_analysed(index_selected).shift1.Item(key))
                    End If
                End If
                ' calc = calc + periode_analysed(index_selected).shift2.Item(key)
                '  If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 2 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc

            If BTN_Unit.Text = "h" Then
                LBL_Other_Shift1.Text = (calc * 100 / total).ToString("00.00") 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
                LBL_Total_Shift1.Text = (verif * 100 / total).ToString("00.00") 'Math.Round(verif * 100 / total).ToString("00.00")
            Else
                LBL_Other_Shift1.Text = Config_report.uptimeToDHMS(calc)
                LBL_Total_Shift1.Text = Config_report.uptimeToDHMS(verif)
            End If

        Else
            LBL_CycleOn_Shift1.Text = ("00.00")
            LBL_CycleOff_Shift1.Text = ("00.00")
            LBL_Setup_Shift1.Text = "00.00"
            LBL_Other_Shift1.Text = "00.00"
            LBL_Total_Shift1.Text = "00.00"
        End If
        If Welcome.CSIF_version = 1 Then
            LBL_Setup_Shift1.Text = ""
            LBL_Other_Shift1.Text = ""
            If total = 0 Then
                LBL_CycleOff_Shift1.Text = ("00.00")
            Else
                '  Form9.Label45.Text = Math.Round((periode_analysed(index_selected).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_analysed(index_selected).shift1.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_analysed(index_selected).shift1.Item("CYCLE OFF")
                    If (periode_analysed(index_selected).shift1.ContainsKey("SETUP")) Then
                        totaloff += periode_analysed(index_selected).shift1.Item("SETUP")
                    End If
                    totaloff += calc

                    LBL_CycleOff_Shift1.Text = ((totaloff) * 100 / total).ToString("00.00") 'Math.Round((periode_analysed(index_selected).shift1.Item("CYCLE OFF") + periode_analysed(index_selected).shift1.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    LBL_CycleOff_Shift1.Text = ((calc) * 100 / total).ToString("00.00") 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If


        total = 0
        verif = 0
        calc = 0
    End Sub
    Private Sub fill_shift2()
        Dim calc As Double = 0, total As Double = 0, verif As Double = 0
        Shift2_other.Clear()

        'shift2 percentage :
        For Each key In periode_analysed(index_selected).shift2.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_analysed(index_selected).shift2.Item(key)
        Next

        If (total <> 0) Then

            If BTN_Unit.Text = "h" Then

                If periode_analysed(index_selected).shift2.ContainsKey("CYCLE ON") Then
                    LBL_CycleOn_Shift2.Text = (periode_analysed(index_selected).shift2.Item("CYCLE ON") * 100 / total).ToString("00.00")
                    verif = periode_analysed(index_selected).shift2.Item("CYCLE ON")
                Else
                    LBL_CycleOn_Shift2.Text = ("00.00")
                End If

                If periode_analysed(index_selected).shift2.ContainsKey("CYCLE OFF") Then

                    LBL_CycleOff_Shift2.Text = (periode_analysed(index_selected).shift2.Item("CYCLE OFF") * 100 / total).ToString("00.00") 'Math.Round(periode_analysed(index_selected).shift2.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                    verif = verif + periode_analysed(index_selected).shift2.Item("CYCLE OFF")
                Else
                    LBL_CycleOff_Shift2.Text = ("00.00")
                End If

                If periode_analysed(index_selected).shift2.ContainsKey("SETUP") Then
                    LBL_Setup_Shift2.Text = (periode_analysed(index_selected).shift2.Item("SETUP") * 100 / total).ToString("00.00") 'Math.Round(periode_analysed(index_selected).shift2.Item("SETUP") * 100 / total).ToString("00.00")
                    verif = verif + periode_analysed(index_selected).shift2.Item("SETUP")
                Else
                    LBL_Setup_Shift2.Text = "00.00"
                End If

            Else

                If periode_analysed(index_selected).shift2.ContainsKey("CYCLE ON") Then

                    LBL_CycleOn_Shift2.Text = Config_report.uptimeToDHMS(periode_analysed(index_selected).shift2.Item("CYCLE ON"))
                    verif = periode_analysed(index_selected).shift2.Item("CYCLE ON")
                Else
                    LBL_CycleOn_Shift2.Text = ("00:00")
                End If

                If periode_analysed(index_selected).shift2.ContainsKey("CYCLE OFF") Then

                    LBL_CycleOff_Shift2.Text = Config_report.uptimeToDHMS(periode_analysed(index_selected).shift2.Item("CYCLE OFF"))
                    verif = verif + periode_analysed(index_selected).shift2.Item("CYCLE OFF")
                Else
                    LBL_CycleOff_Shift2.Text = ("00:00")
                End If

                If periode_analysed(index_selected).shift2.ContainsKey("SETUP") Then
                    LBL_Setup_Shift2.Text = Config_report.uptimeToDHMS(periode_analysed(index_selected).shift2.Item("SETUP"))
                    verif = verif + periode_analysed(index_selected).shift2.Item("SETUP")
                Else
                    LBL_Setup_Shift2.Text = "00:00"
                End If

            End If
            If Welcome.CSIF_version = 1 Then LBL_Setup_Shift2.Text = ""
            For Each key In (periode_analysed(index_selected).shift2.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                    calc = calc + periode_analysed(index_selected).shift2.Item(key)
                    Shift2_other.Add(key, Config_report.uptimeToDHMS(periode_analysed(index_selected).shift2.Item(key)))
                    If top4.ContainsKey(key) Then
                        top4.Item(key) = top4.Item(key) + periode_analysed(index_selected).shift2.Item(key)
                    Else
                        top4.Add(key, periode_analysed(index_selected).shift2.Item(key))
                    End If
                End If
                ' calc = calc + periode_analysed(index_selected).shift2.Item(key)
                '  If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 2 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc

            If BTN_Unit.Text = "h" Then
                LBL_Other_Shift2.Text = (calc * 100 / total).ToString("00.00") 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
                LBL_Total_Shift2.Text = (verif * 100 / total).ToString("00.00") 'Math.Round(verif * 100 / total).ToString("00.00")
            Else
                LBL_Other_Shift2.Text = Config_report.uptimeToDHMS(calc)
                LBL_Total_Shift2.Text = Config_report.uptimeToDHMS(verif)
            End If

        Else
            LBL_CycleOn_Shift2.Text = ("00.00")
            LBL_CycleOff_Shift2.Text = ("00.00")
            LBL_Setup_Shift2.Text = "00.00"
            LBL_Other_Shift2.Text = "00.00"
            LBL_Total_Shift2.Text = "00.00"
        End If
        If Welcome.CSIF_version = 1 Then
            LBL_Setup_Shift2.Text = ""
            LBL_Other_Shift2.Text = ""
            If total = 0 Then
                LBL_CycleOff_Shift2.Text = ("00.00")
            Else
                '  Form9.Label45.Text = Math.Round((periode_analysed(index_selected).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_analysed(index_selected).shift2.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_analysed(index_selected).shift2.Item("CYCLE OFF")
                    If (periode_analysed(index_selected).shift2.ContainsKey("SETUP")) Then
                        totaloff += periode_analysed(index_selected).shift2.Item("SETUP")
                    End If
                    totaloff += calc

                    LBL_CycleOff_Shift2.Text = ((totaloff) * 100 / total).ToString("00.00") 'Math.Round((periode_analysed(index_selected).shift2.Item("CYCLE OFF") + periode_analysed(index_selected).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    LBL_CycleOff_Shift2.Text = ((calc) * 100 / total).ToString("00.00") 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If


        total = 0
        verif = 0
        calc = 0
    End Sub
    Private Sub fill_shift3()
        Dim calc As Double = 0, total As Double = 0, verif As Double = 0

        Shift3_other.Clear()
        'shift3 percentage :
        For Each key In periode_analysed(index_selected).shift3.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_analysed(index_selected).shift3.Item(key)
        Next

        If (total <> 0) Then

            If BTN_Unit.Text = "h" Then

                If periode_analysed(index_selected).shift3.ContainsKey("CYCLE ON") Then
                    LBL_CycleOn_Shift3.Text = (periode_analysed(index_selected).shift3.Item("CYCLE ON") * 100 / total).ToString("00.00")
                    verif = periode_analysed(index_selected).shift3.Item("CYCLE ON")
                Else
                    LBL_CycleOn_Shift3.Text = ("00.00")
                End If

                If periode_analysed(index_selected).shift3.ContainsKey("CYCLE OFF") Then

                    LBL_CycleOff_Shift3.Text = (periode_analysed(index_selected).shift3.Item("CYCLE OFF") * 100 / total).ToString("00.00") 'Math.Round(periode_analysed(index_selected).shift3.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                    verif = verif + periode_analysed(index_selected).shift3.Item("CYCLE OFF")
                Else
                    LBL_CycleOff_Shift3.Text = ("00.00")
                End If

                If periode_analysed(index_selected).shift3.ContainsKey("SETUP") Then
                    LBL_Setup_Shift3.Text = (periode_analysed(index_selected).shift3.Item("SETUP") * 100 / total).ToString("00.00") 'Math.Round(periode_analysed(index_selected).shift3.Item("SETUP") * 100 / total).ToString("00.00")
                    verif = verif + periode_analysed(index_selected).shift3.Item("SETUP")
                Else
                    LBL_Setup_Shift3.Text = "00.00"
                End If

            Else

                If periode_analysed(index_selected).shift3.ContainsKey("CYCLE ON") Then

                    LBL_CycleOn_Shift3.Text = Config_report.uptimeToDHMS(periode_analysed(index_selected).shift3.Item("CYCLE ON"))
                    verif = periode_analysed(index_selected).shift3.Item("CYCLE ON")
                Else
                    LBL_CycleOn_Shift3.Text = ("00:00")
                End If

                If periode_analysed(index_selected).shift3.ContainsKey("CYCLE OFF") Then

                    LBL_CycleOff_Shift3.Text = Config_report.uptimeToDHMS(periode_analysed(index_selected).shift3.Item("CYCLE OFF"))
                    verif = verif + periode_analysed(index_selected).shift3.Item("CYCLE OFF")
                Else
                    LBL_CycleOff_Shift3.Text = ("00:00")
                End If

                If periode_analysed(index_selected).shift3.ContainsKey("SETUP") Then
                    LBL_Setup_Shift3.Text = Config_report.uptimeToDHMS(periode_analysed(index_selected).shift3.Item("SETUP"))
                    verif = verif + periode_analysed(index_selected).shift3.Item("SETUP")
                Else
                    LBL_Setup_Shift3.Text = "00:00"
                End If

            End If
            If Welcome.CSIF_version = 1 Then LBL_Setup_Shift3.Text = ""
            For Each key In (periode_analysed(index_selected).shift3.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                    calc = calc + periode_analysed(index_selected).shift3.Item(key)
                    Shift3_other.Add(key, Config_report.uptimeToDHMS(periode_analysed(index_selected).shift3.Item(key)))
                    If top4.ContainsKey(key) Then
                        top4.Item(key) = top4.Item(key) + periode_analysed(index_selected).shift3.Item(key)
                    Else
                        top4.Add(key, periode_analysed(index_selected).shift3.Item(key))
                    End If
                End If
                ' calc = calc + periode_analysed(index_selected).shift3.Item(key)
                '  If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 2 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc

            If BTN_Unit.Text = "h" Then
                LBL_Other_Shift3.Text = (calc * 100 / total).ToString("00.00") 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
                LBL_Total_Shift3.Text = (verif * 100 / total).ToString("00.00") 'Math.Round(verif * 100 / total).ToString("00.00")
            Else
                LBL_Other_Shift3.Text = Config_report.uptimeToDHMS(calc)
                LBL_Total_Shift3.Text = Config_report.uptimeToDHMS(verif)
            End If

        Else
            LBL_CycleOn_Shift3.Text = ("00.00")
            LBL_CycleOff_Shift3.Text = ("00.00")
            LBL_Setup_Shift3.Text = "00.00"
            LBL_Other_Shift3.Text = "00.00"
            LBL_Total_Shift3.Text = "00.00"
        End If
        If Welcome.CSIF_version = 1 Then
            LBL_Setup_Shift3.Text = ""
            LBL_Other_Shift3.Text = ""
            If total = 0 Then
                LBL_CycleOff_Shift3.Text = ("00.00")
            Else
                '  Form9.Label45.Text = Math.Round((periode_analysed(index_selected).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_analysed(index_selected).shift3.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_analysed(index_selected).shift3.Item("CYCLE OFF")
                    If (periode_analysed(index_selected).shift3.ContainsKey("SETUP")) Then
                        totaloff += periode_analysed(index_selected).shift3.Item("SETUP")
                    End If
                    totaloff += calc

                    LBL_CycleOff_Shift3.Text = ((totaloff) * 100 / total).ToString("00.00") 'Math.Round((periode_analysed(index_selected).shift3.Item("CYCLE OFF") + periode_analysed(index_selected).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    LBL_CycleOff_Shift3.Text = ((calc) * 100 / total).ToString("00.00") 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If


        total = 0
        verif = 0
        calc = 0
    End Sub
    Private Sub for_period()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' periode array '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim con_time, coff_time, other_time, setup_time As Double


        If (periode_analysed(index_selected).shift1.ContainsKey("CYCLE ON")) Then
            con_time = periode_analysed(index_selected).shift1.Item("CYCLE ON")
        End If
        If (periode_analysed(index_selected).shift2.ContainsKey("CYCLE ON")) Then
            con_time += periode_analysed(index_selected).shift2.Item("CYCLE ON")
        End If
        If (periode_analysed(index_selected).shift3.ContainsKey("CYCLE ON")) Then
            con_time += periode_analysed(index_selected).shift3.Item("CYCLE ON")
        End If

        If (periode_analysed(index_selected).shift1.ContainsKey("CYCLE OFF")) Then
            coff_time = periode_analysed(index_selected).shift1.Item("CYCLE OFF")
        End If
        If (periode_analysed(index_selected).shift2.ContainsKey("CYCLE OFF")) Then
            coff_time += periode_analysed(index_selected).shift2.Item("CYCLE OFF")
        End If
        If (periode_analysed(index_selected).shift3.ContainsKey("CYCLE OFF")) Then
            coff_time += periode_analysed(index_selected).shift3.Item("CYCLE OFF")
        End If


        If (periode_analysed(index_selected).shift1.ContainsKey("SETUP")) Then
            setup_time = periode_analysed(index_selected).shift1.Item("SETUP")
        End If
        If (periode_analysed(index_selected).shift2.ContainsKey("SETUP")) Then
            setup_time += periode_analysed(index_selected).shift2.Item("SETUP")
        End If
        If (periode_analysed(index_selected).shift3.ContainsKey("SETUP")) Then
            setup_time += periode_analysed(index_selected).shift3.Item("SETUP")
        End If

        For Each key In (periode_analysed(index_selected).shift1.Keys)
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                other_time += periode_analysed(index_selected).shift1.Item(key)
            End If
        Next
        For Each key In (periode_analysed(index_selected).shift2.Keys)
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                other_time += periode_analysed(index_selected).shift2.Item(key)
            End If
        Next
        For Each key In (periode_analysed(index_selected).shift3.Keys)
            If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                other_time += periode_analysed(index_selected).shift3.Item(key)
            End If
        Next

        Dim total As Double = other_time + setup_time + coff_time + con_time

        LBL_CycleOn_Period.Text = ((con_time) * 100 / total).ToString("00.00") 'Math.Round((con_time) * 100 / total).ToString("00.00")
        LBL_CycleOff_Period.Text = ((coff_time) * 100 / total).ToString("00.00") 'Math.Round((coff_time) * 100 / total).ToString("00.00")
        LBL_Setup_Period.Text = ((setup_time) * 100 / total).ToString("00.00") 'Math.Round((setup_time) * 100 / total).ToString("00.00")
        LBL_Other_Period.Text = ((other_time) * 100 / total).ToString("00.00") 'Math.Round((other_time) * 100 / total).ToString("00.00")
        LBL_Total_Period.Text = ((con_time + coff_time + setup_time + other_time) * 100 / total).ToString("00.00") '"100.00"


        ' Pie CHart ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        data(0) = Val(LBL_CycleOn_Period.Text)
        data(1) = Val(LBL_CycleOff_Period.Text)
        data(2) = Val(LBL_Setup_Period.Text)
        data(3) = Val(LBL_Other_Period.Text)
        Chart1.Series("Series1").Points.DataBindY(data)


        Chart1.Series("Series1").Points.Item(0).ToolTip = "Cycle On : " + Config_report.uptimeToDHMS(con_time).ToString()
        Chart1.Series("Series1").Points.Item(1).ToolTip = "Cycle Off : " + Config_report.uptimeToDHMS(coff_time).ToString()
        Chart1.Series("Series1").Points.Item(2).ToolTip = "Setup : " + Config_report.uptimeToDHMS(setup_time).ToString()
        Chart1.Series("Series1").Points.Item(3).ToolTip = "Other causes of downtime : " + Config_report.uptimeToDHMS(other_time).ToString()

        Chart1.Series("Series1").Points.Item(0).Color = backcolors
        Chart1.Series("Series1").Points.Item(1).Color = backcolors2
        Chart1.Series("Series1").Points.Item(2).Color = backcolors3
        Chart1.Series("Series1").Points.Item(3).Color = Report_BarChart.Other_color

    End Sub

    Private Sub find_top4_reason()
        Dim sorted = From pair In top4
                     Order By pair.Value Descending
        Dim sortedDictionary = sorted.ToDictionary(Function(p) p.Key, Function(p) p.Value)
        Dim subtotal As Double = 0

        For Each value In sortedDictionary
            subtotal = value.Value + subtotal
        Next

        Dim status As String = "", percent As String = "", keyname As String = ""
        Dim tempdouble As Double
        Dim wordLenght As Integer = 20

        Dim k As Integer = 0
        For Each key In sortedDictionary
            If key.Key.Length > wordLenght Then
                keyname = key.Key.Substring(0, wordLenght)
            Else
                keyname = key.Key
            End If
            status = status & vbCrLf & keyname
            tempdouble = key.Value / subtotal
            tempdouble = tempdouble * 100
            percent = percent & vbCrLf & ": " & Config_report.uptimeToDHMS(key.Value) & ", " & tempdouble.ToString("00.00") & "%" '(Math.Round(key.Value * 100 / subtotal)).ToString() & " %"
            k = k + 1
            If k = 4 Then Exit For
        Next

        Label5.Text = status
        Label5.TextAlign = ContentAlignment.TopCenter

        Label6.Text = percent

    End Sub

    Private Sub Machine_util_det_paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Using p As New Pen(Color.FromArgb(217, 217, 217), 2.0)
            e.Graphics.DrawRectangle(p, 0, 0, Me.Width, Me.Height)
        End Using
    End Sub
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' H <-> %
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub BTN_Unit_Click(sender As Object, e As EventArgs) Handles BTN_Unit.Click



        If BTN_Unit.Text = "%" Then
            BTN_Unit.Text = "h"

            Label30.Text = "Shift 1 (%)"
            Label43.Text = "Shift 2 (%)"
            Label53.Text = "Shift 3 (%)"
            Label20.Text = "Periode (%)"


            fill_shift1()
            fill_shift2()
            fill_shift3()

            for_period()

            'find_top4_reason()
        Else

            BTN_Unit.Text = "%"
            Label30.Text = "Shift 1 (h)"
            Label43.Text = "Shift 2 (h)"
            Label53.Text = "Shift 3 (h)"
            Label20.Text = "Periode (h)"

            Call fill_Report_BarChart_shift_hours(periode_analysed, index_selected)

            Dim total As Double = periode_analysed(index_selected).shift1.Values.Sum


            If total > 0 Then


                ''''''''''''''''''''''''''''''
                Dim total_con As Double, total_coff As Double, total_setup As Double, total_other As Double
                If periode_analysed(Config_report.machine_Report_BarChart).shift1.ContainsKey("CYCLE ON") Then
                    total_con += periode_analysed(Config_report.machine_Report_BarChart).shift1.Item("CYCLE ON")
                End If
                If periode_analysed(Config_report.machine_Report_BarChart).shift2.ContainsKey("CYCLE ON") Then
                    total_con += periode_analysed(Config_report.machine_Report_BarChart).shift2.Item("CYCLE ON")
                End If
                If periode_analysed(Config_report.machine_Report_BarChart).shift3.ContainsKey("CYCLE ON") Then
                    total_con += periode_analysed(Config_report.machine_Report_BarChart).shift3.Item("CYCLE ON")
                End If

                'total_coff = Form3.periode_returned(Form3.machine_form9).shift1.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift2.Item("CYCLE OFF") + Form3.periode_returned(Form3.machine_form9).shift3.Item("CYCLE OFF")
                If periode_analysed(Config_report.machine_Report_BarChart).shift1.ContainsKey("CYCLE OFF") Then
                    total_coff += periode_analysed(Config_report.machine_Report_BarChart).shift1.Item("CYCLE OFF")
                End If
                If periode_analysed(Config_report.machine_Report_BarChart).shift2.ContainsKey("CYCLE OFF") Then
                    total_coff += periode_analysed(Config_report.machine_Report_BarChart).shift2.Item("CYCLE OFF")
                End If
                If periode_analysed(Config_report.machine_Report_BarChart).shift3.ContainsKey("CYCLE OFF") Then
                    total_coff += periode_analysed(Config_report.machine_Report_BarChart).shift3.Item("CYCLE OFF")
                End If

                If (periode_analysed(Config_report.machine_Report_BarChart).shift1.ContainsKey("SETUP")) Then
                    total_setup += periode_analysed(Config_report.machine_Report_BarChart).shift1.Item("SETUP")
                End If
                If (periode_analysed(Config_report.machine_Report_BarChart).shift2.ContainsKey("SETUP")) Then
                    total_setup += periode_analysed(Config_report.machine_Report_BarChart).shift2.Item("SETUP")
                End If
                If (periode_analysed(Config_report.machine_Report_BarChart).shift3.ContainsKey("SETUP")) Then
                    total_setup += periode_analysed(Config_report.machine_Report_BarChart).shift3.Item("SETUP")
                End If

                'total_other = Form3.periode_returned(Form3.machine_form9).shift1.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift2.Item("OTHER") + Form3.periode_returned(Form3.machine_form9).shift3.Item("OTHER")
                For Each key In (periode_analysed(Config_report.machine_Report_BarChart).shift1.Keys)
                    If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                        total_other = total_other + periode_analysed(Config_report.machine_Report_BarChart).shift1.Item(key)
                    End If
                Next
                For Each key In (periode_analysed(Config_report.machine_Report_BarChart).shift2.Keys)
                    If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                        total_other = total_other + periode_analysed(Config_report.machine_Report_BarChart).shift2.Item(key)
                    End If
                Next
                For Each key In (periode_analysed(Config_report.machine_Report_BarChart).shift3.Keys)
                    If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                        total_other = total_other + periode_analysed(Config_report.machine_Report_BarChart).shift3.Item(key)
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




        If Welcome.CSIF_version = 1 Then Label5.Text = ""
        If Welcome.CSIF_version = 1 Then Label6.Text = ""
    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' FILL shifts in hh:mm in Report_BarChart with periode_returned
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Public Sub fill_Report_BarChart_shift_hours(periode_returned As CSI_Library.CSI_DATA.periode(), i As Integer)

        Dim calc As Double = 0, total As Double = 0, verif As Double = 0


        'Shift2 percentage :
        For Each key In periode_returned(i).shift1.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift1.Item(key)
        Next
        If (total <> 0) Then
            If periode_returned(i).shift1.ContainsKey("CYCLE ON") Then
                LBL_CycleOn_Shift1.Text = Config_report.uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE ON"))
                verif = periode_returned(i).shift1.Item("CYCLE ON")
            Else
                LBL_CycleOn_Shift1.Text = ("00:00")
            End If

            If periode_returned(i).shift1.ContainsKey("CYCLE OFF") Then
                LBL_CycleOff_Shift1.Text = Config_report.uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE OFF")) 'Math.Round(periode_returned(i).shift1.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift1.Item("CYCLE OFF")
            Else
                LBL_CycleOff_Shift1.Text = ("00:00")
            End If

            If periode_returned(i).shift1.ContainsKey("SETUP") Then
                LBL_Setup_Shift1.Text = Config_report.uptimeToDHMS(periode_returned(i).shift1.Item("SETUP")) 'Math.Round(periode_returned(i).shift1.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift1.Item("SETUP")
            Else
                LBL_Setup_Shift1.Text = "00:00"
            End If

            If Welcome.CSIF_version = 1 Then
                LBL_Setup_Shift1.Text = ""
            End If

            For Each key In (periode_returned(i).shift1.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                    calc = calc + periode_returned(i).shift1.Item(key)

                    'If top4.ContainsKey(key) Then
                    '    top4.Item(key) = top4.Item(key) + periode_returned(i).shift1.Item(key)
                    'Else
                    '    top4.Add(key, periode_returned(i).shift1.Item(key))
                    'End If
                End If
                ' calc = calc + periode_returned(i).shift1.Item(key)
                '  If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 2 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc
            LBL_Other_Shift1.Text = Config_report.uptimeToDHMS(calc) 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            LBL_Total_Shift1.Text = Config_report.uptimeToDHMS(verif) 'Math.Round(verif * 100 / total).ToString("00.00")
        Else
            LBL_CycleOn_Shift1.Text = "00:00"
            LBL_CycleOff_Shift1.Text = "00:00"
            LBL_Setup_Shift1.Text = "00:00"
            LBL_Other_Shift1.Text = "00:00"
            LBL_Total_Shift1.Text = "00:00"
        End If
        If Welcome.CSIF_version = 1 Then
            LBL_Setup_Shift1.Text = ""
            LBL_Other_Shift1.Text = ""
            If total = 0 Then
                LBL_CycleOff_Shift1.Text = ("00:00")
            Else
                '  Form9.Label32.Text = Math.Round((periode_returned(i).shift1.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift1.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift1.Item("CYCLE OFF")
                    If (periode_returned(i).shift1.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift1.Item("SETUP")
                    End If
                    totaloff += calc

                    LBL_CycleOff_Shift1.Text = Config_report.uptimeToDHMS(totaloff)
                    'Form9.Label40.Text = Config_report.uptimeToDHMS(periode_returned(i).shift1.Item("CYCLE OFF") + periode_returned(i).shift1.Item("SETUP") + calc) 'Math.Round((periode_returned(i).shift1.Item("CYCLE OFF") + periode_returned(i).shift1.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    LBL_CycleOff_Shift1.Text = Config_report.uptimeToDHMS(calc) 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If

        total = 0
        verif = 0
        calc = 0

        ''''''''''''''From regular fill_form9 function
        'Shift2 percentage :
        For Each key In periode_returned(i).shift2.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift2.Item(key)
        Next
        If (total <> 0) Then
            If periode_returned(i).shift2.ContainsKey("CYCLE ON") Then
                LBL_CycleOn_Shift2.Text = Config_report.uptimeToDHMS(periode_returned(i).shift2.Item("CYCLE ON"))
                verif = periode_returned(i).shift2.Item("CYCLE ON")
            Else
                LBL_CycleOn_Shift2.Text = ("00:00")
            End If

            If periode_returned(i).shift2.ContainsKey("CYCLE OFF") Then
                LBL_CycleOff_Shift2.Text = Config_report.uptimeToDHMS(periode_returned(i).shift2.Item("CYCLE OFF")) 'Math.Round(periode_returned(i).shift2.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift2.Item("CYCLE OFF")
            Else
                LBL_CycleOff_Shift2.Text = ("00:00")
            End If

            If periode_returned(i).shift2.ContainsKey("SETUP") Then
                LBL_Setup_Shift2.Text = Config_report.uptimeToDHMS(periode_returned(i).shift2.Item("SETUP")) 'Math.Round(periode_returned(i).shift2.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift2.Item("SETUP")
            Else
                LBL_Setup_Shift2.Text = "00:00"
            End If

            If Welcome.CSIF_version = 1 Then
                LBL_Setup_Shift2.Text = ""
            End If

            For Each key In (periode_returned(i).shift2.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                    calc = calc + periode_returned(i).shift2.Item(key)

                    'If top4.ContainsKey(key) Then
                    '    top4.Item(key) = top4.Item(key) + periode_returned(i).shift2.Item(key)
                    'Else
                    '    top4.Add(key, periode_returned(i).shift2.Item(key))
                    'End If
                End If
                ' calc = calc + periode_returned(i).shift2.Item(key)
                '  If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 2 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc
            LBL_Other_Shift2.Text = Config_report.uptimeToDHMS(calc) 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            LBL_Total_Shift2.Text = Config_report.uptimeToDHMS(verif) 'Math.Round(verif * 100 / total).ToString("00.00")
        Else
            LBL_CycleOn_Shift2.Text = "00:00"
            LBL_CycleOff_Shift2.Text = "00:00"
            LBL_Setup_Shift2.Text = "00:00"
            LBL_Other_Shift2.Text = "00:00"
            LBL_Total_Shift2.Text = "00:00"
        End If
        If Welcome.CSIF_version = 1 Then
            LBL_Setup_Shift2.Text = ""
            LBL_Other_Shift2.Text = ""
            If total = 0 Then
                LBL_CycleOff_Shift2.Text = ("00:00")
            Else
                '  Form9.Label45.Text = Math.Round((periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift2.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift2.Item("CYCLE OFF")
                    If (periode_returned(i).shift2.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift2.Item("SETUP")
                    End If
                    totaloff += calc

                    LBL_CycleOff_Shift2.Text = Config_report.uptimeToDHMS(totaloff)

                    'Form9.Label50.Text = Config_report.uptimeToDHMS(periode_returned(i).shift2.Item("CYCLE OFF") + periode_returned(i).shift2.Item("SETUP") + calc) 'Math.Round((periode_returned(i).shift2.Item("CYCLE OFF") + periode_returned(i).shift2.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    LBL_CycleOff_Shift2.Text = Config_report.uptimeToDHMS(calc) 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If

        total = 0
        verif = 0
        calc = 0


        'Shift3 percentage :
        For Each key In periode_returned(i).shift3.Keys
            If (Not (key.Contains("_PARTN"))) And (Not (key.Contains("_SH_START")) And Not (key.Contains("_SH_END"))) Then total = total + periode_returned(i).shift3.Item(key)
        Next
        If (total <> 0) Then
            If periode_returned(i).shift3.ContainsKey("CYCLE ON") Then
                LBL_CycleOn_Shift3.Text = Config_report.uptimeToDHMS(periode_returned(i).shift3.Item("CYCLE ON"))
                verif = periode_returned(i).shift3.Item("CYCLE ON")
            Else
                LBL_CycleOn_Shift3.Text = ("00:00")
            End If

            If periode_returned(i).shift3.ContainsKey("CYCLE OFF") Then
                LBL_CycleOff_Shift3.Text = Config_report.uptimeToDHMS(periode_returned(i).shift3.Item("CYCLE OFF")) 'Math.Round(periode_returned(i).shift3.Item("CYCLE OFF") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift3.Item("CYCLE OFF")
            Else
                LBL_CycleOff_Shift3.Text = ("00:00")
            End If

            If periode_returned(i).shift3.ContainsKey("SETUP") Then
                LBL_Setup_Shift3.Text = Config_report.uptimeToDHMS(periode_returned(i).shift3.Item("SETUP")) 'Math.Round(periode_returned(i).shift3.Item("SETUP") * 100 / total).ToString("00.00")
                verif = verif + periode_returned(i).shift3.Item("SETUP")
            Else
                LBL_Setup_Shift3.Text = "00:00"
            End If

            If Welcome.CSIF_version = 1 Then
                LBL_Setup_Shift3.Text = ""
            End If

            For Each key In (periode_returned(i).shift3.Keys)
                If key <> "CYCLE ON" And key <> "CYCLE OFF" And key <> "SETUP" And Not (key.Contains("PARTN")) Then
                    calc = calc + periode_returned(i).shift3.Item(key)

                    'If top4.ContainsKey(key) Then
                    '    top4.Item(key) = top4.Item(key) + periode_returned(i).shift3.Item(key)
                    'Else
                    '    top4.Add(key, periode_returned(i).shift3.Item(key))
                    'End If
                End If
                ' calc = calc + periode_returned(i).shift3.Item(key)
                '  If key.Contains("PARTNO") Then If Not (Form9.ListBox1.Items.Contains("Shift 1 : " & key.Remove(0, 8))) Then Form9.ListBox1.Items.Add(("Shift 2 : " & key.Remove(0, 8)))
            Next
            verif = verif + calc
            LBL_Other_Shift3.Text = Config_report.uptimeToDHMS(calc) 'Math.Round(calc * 100 / total).ToString("00.00") '+ Val(general_array(3, 2, ThirdDim)) + Val(general_array(4, 2, ThirdDim)) + Val(general_array(5, 2, ThirdDim)))
            LBL_Total_Shift3.Text = Config_report.uptimeToDHMS(verif) 'Math.Round(verif * 100 / total).ToString("00.00")
        Else
            LBL_CycleOn_Shift3.Text = "00:00"
            LBL_CycleOff_Shift3.Text = "00:00"
            LBL_Setup_Shift3.Text = "00:00"
            LBL_Other_Shift3.Text = "00:00"
            LBL_Total_Shift3.Text = "00:00"
        End If
        If Welcome.CSIF_version = 1 Then
            LBL_Setup_Shift3.Text = ""
            LBL_Other_Shift3.Text = ""
            If total = 0 Then
                LBL_CycleOff_Shift3.Text = ("00:00")
            Else
                '  Form9.Label55.Text = Math.Round((periode_returned(i).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                If (periode_returned(i).shift3.ContainsKey("CYCLE OFF")) Then
                    Dim totaloff As Double = 0
                    totaloff = periode_returned(i).shift3.Item("CYCLE OFF")
                    If (periode_returned(i).shift3.ContainsKey("SETUP")) Then
                        totaloff += periode_returned(i).shift3.Item("SETUP")
                    End If
                    totaloff += calc

                    LBL_CycleOff_Shift3.Text = Config_report.uptimeToDHMS(totaloff)

                    'Form9.Label62.Text = Config_report.uptimeToDHMS(periode_returned(i).shift3.Item("CYCLE OFF") + periode_returned(i).shift3.Item("SETUP") + calc) 'Math.Round((periode_returned(i).shift3.Item("CYCLE OFF") + periode_returned(i).shift3.Item("SETUP") + calc) * 100 / total).ToString("00.00")
                Else
                    LBL_CycleOff_Shift3.Text = Config_report.uptimeToDHMS(calc) 'Math.Round((calc) * 100 / total).ToString("00.00")
                End If
            End If
        End If



    End Sub

    Private Sub Chart1_hover(sender As Object, e As EventArgs) Handles Chart1.MouseHover
        Chart1.Series("Series1").Points(3).CustomProperties = "Exploded = true"

    End Sub

    Sub Chart1_mouse_leave(sender As Object, e As EventArgs) Handles Chart1.MouseLeave
        Chart1.Series("Series1").Points(3).CustomProperties = "Exploded = false"

    End Sub

    Private Sub undock_Click(sender As Object, e As EventArgs) Handles undock.Click
        If Me.MdiParent Is Nothing Then

            Me.MdiParent = Reporting_application
            Me.FormBorderStyle = FormBorderStyle.None
            'Me.Height = Report_BarChart.Height
            undock.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone)
            Dim w = Report_BarChart.Size.Width
            Dim w2 = Config_report.Size.Width
            Me.Location = New Point(Report_BarChart.Size.Width + Config_report.Size.Width, Report_BarChart.Location.Y)
        Else
            Me.MdiParent = Nothing
            Me.FormBorderStyle = FormBorderStyle.FixedSingle
            Me.FormBorderStyle = FormBorderStyle.SizableToolWindow
            undock.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone)
            'Me.Location = New Point(Report_BarChart.Size.Width + Config_report.Size.Width, Report_BarChart.Location.Y)
        End If
        Reporting_application.PerformLayout()
    End Sub

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click
        'Me.Visible = False
        Report_BarChart.Visible = False



        If Welcome.CSIF_version <> 1 Then
            Chart1.Cursor = Cursors.Hand
            '  If consolidated = False Then
            ' Report_PieChart.CB_Report.Items.Clear()


            Call Config_report.Fill_Combobox_detailled(periode_analysed)


            Report_PieChart.MdiParent = Reporting_application
            'For Each item In Report_BarChart.CB_Report.Items
            '    ' Dim i As Integer = Report_BarChart.CB_Report.FindStringExact(item.ToString())

            '    If item.ToString().Contains(LBL_machinename.Text) Then Report_PieChart.CB_Report.SelectedIndex = Report_BarChart.CB_Report.FindStringExact(item.ToString())

            'Next
            Report_PieChart.CB_Report.SelectedIndex = index_selected

            Report_PieChart.CB_Report.Enabled = False
            Report_PieChart.BTN_Prev.Enabled = False
            Report_PieChart.BTN_Next.Enabled = False

            'Else
            '    Report_PieChart.CB_Report.Items.Clear()

            '    Call Config_report.Fill_Combobox_detailled(consolidated_periode)


            '    Report_PieChart.MdiParent = Reporting_application

            '    Report_PieChart.CB_Report.SelectedIndex = 0
            '    Report_PieChart.SuspendLayout()
            '    Me.Visible = False
            'End If
        Else
            Chart1.Cursor = Cursors.Arrow
        End If


    End Sub

    Private Sub Close_details_Click(sender As Object, e As EventArgs) Handles Close_details.Click
        machine_util_det_wasactive = False
        Close()
    End Sub

    Private Sub Machine_util_det_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        If Me.IsMdiChild = True Then Me.Height = Report_BarChart.Height
    End Sub
End Class
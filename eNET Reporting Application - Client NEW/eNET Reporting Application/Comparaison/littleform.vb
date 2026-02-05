Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows
Imports System.Drawing
Imports System.Drawing.Drawing2D

Imports CSI_Library.CSI_Library

Imports System.Globalization

Public Class littleform

    Private Sub littleform_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Dim status As String = ""
            Dim shift As New DataView(CF.TimeLine(UBound(CF.form_) - 1))
            shift.RowFilter = "SHIFT =" & CF.shiftofeachform(UBound(CF.form_) - 1)

            ' start and end shift time          
            Dim start_shift As DateTime = shift.Item(0).Item("Time_")
            Dim end_shift As DateTime = shift.Item(shift.Count - 1).Item("Time_")
            end_shift = end_shift.AddSeconds(shift.Item(shift.Count - 1).Item("Cycletime"))


            Dim time_ As DateTime = shift.Item(0).Item("Time_")
            Dim t0 As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)
            Chart1.Series("Cycle").Points.AddXY(t0.ToOADate, 100)
            Chart1.ChartAreas(0).AxisX.Minimum = t0.ToOADate
            Chart1.ChartAreas(0).AxisX.Maximum = end_shift.ToOADate

            'status = shift.Item(0).Item("status")


            Dim stat As String = shift.Item(0).Item("status")
            If stat <> "" And Not stat.Contains("_PARTNO") Then
                Select Case stat
                    Case "_CON"
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Color.Green
                    Case "_COFF"
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Color.Red
                    Case "_SETUP"
                        Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Color.Blue
                    Case ""
                    Case Else
                        '   Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Chart2.Series(status).Color
                End Select
            End If



            For Each row In shift

                '   Call annotation(row)
                time_ = row.Item("Time_")
                status = row.item("status")
                Dim t As New System.DateTime(time_.Year, time_.Month, time_.Day, time_.Hour, time_.Minute, time_.Second)

                If (Not status.Contains("PARTNO")) And status <> "" Then

                    Chart1.Series("Cycle").Points.AddXY(t.ToOADate, 100)

                    Select Case status
                        Case "_CON"
                            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Color.Orange
                        Case "_COFF"
                            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Color.Red
                        Case "_SETUP"
                            Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Color.Blue
                        Case ""
                        Case Else
                            '    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Chart2.Series(status).Color
                    End Select
                End If

                status = row.item("status")

            Next

            Chart1.Series("Cycle").Points.AddXY(end_shift.ToOADate, 100)

            Select Case status
                Case "_CON"
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Color.Green
                Case "_COFF"
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Color.Red
                Case "_SETUP"
                    Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Color.Blue
                Case ""

                Case Else
                    ' Chart1.Series("Cycle").Points(Chart1.Series("Cycle").Points.Count - 1).Color = Chart2.Series(status).Color
            End Select



            Chart1.Series("Cycle").XValueType = ChartValueType.DateTime
            Chart1.ChartAreas(0).AxisX.IntervalType = DateTimeIntervalType.Seconds
            Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "HH:mm:ss"
            Chart1.ChartAreas(0).CursorX.IntervalType = DateTimeIntervalType.Seconds
            Chart1.ChartAreas(0).AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds
            Chart1.Series("Cycle").SmartLabelStyle.Enabled = True
            Chart1.Series("Cycle").SmartLabelStyle.IsMarkerOverlappingAllowed = False
            Chart1.Series("Cycle").SmartLabelStyle.IsOverlappedHidden = True




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

    'Private Sub addbox()

    '    For Each serie In Chart2.Series
    '        Dim box As New Label
    '        With box
    '            .Name = serie.Name
    '            Select Case serie.Name
    '                Case "_CON"
    '                    .Text = "Cycle On"
    '                Case "_COFF"
    '                    .Text = "Cycle Off"
    '                Case "_SETUP"
    '                    .Text = "SETUP"
    '                Case Else
    '                    .Text = serie.Name
    '            End Select

    '            .Cursor = Cursors.Hand
    '            .Tag = serie.Name
    '            .ForeColor = Color.White
    '            .BackColor = serie.Color
    '            .Visible = True
    '        End With

    '        AddHandler box.Click, AddressOf clicked
    '        FlowLayoutPanel1.Controls.Add(box)
    '    Next

    '    FlowLayoutPanel1.Refresh()
    'End Sub


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


    Private Sub Chart1_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Chart1.Click
        If (e.Button.CompareTo(Forms.MouseButtons.Right) = 0) Then
            Chart1.ChartAreas(0).AxisX.ScaleView.ZoomReset()
        End If
    End Sub


#End Region
End Class
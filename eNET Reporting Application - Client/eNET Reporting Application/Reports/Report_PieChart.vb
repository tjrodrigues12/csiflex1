Imports CSI_Library.CSI_Library
Imports System.Windows.Forms
Imports System.Drawing.Printing
Imports System.Windows.Forms.DataVisualization.Charting
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities

Public Class Report_PieChart

    Public CSI_LIB As New CSI_Library.CSI_Library(False)
    Public Timeline As New DataTable
    Public Timeline2 As New DataTable
    Public Timeline3 As New DataTable
    Public colors As New Dictionary(Of String, String)

    Private Sub Report_PieChart_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        If Form_Shift.Visible = True Then Form_Shift.Close()
        If formshift.Visible = True Then formshift.Close()
        'If formshift2.Visible = True Then formshift2.Close()
        'If formshift3.Visible = True Then formshift3.Close()
    End Sub

    Private Sub Report_PieChart_closed(sender As Object, e As EventArgs) Handles MyBase.FormClosed
        Try
            If Machine_util_det.Visible = True Then

                Machine_util_det.Chart1.Enabled = True

                Machine_util_det.Location = New System.Drawing.Point(Report_BarChart.Size.Width + Config_report.Size.Width, Report_BarChart.Location.Y)
                Machine_util_det.Height = Report_BarChart.Height

            End If
        Catch ex As Exception

        End Try
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------
    ' Report_PieChart Load
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Report_PieChart_Loaded(sender As Object, e As EventArgs) Handles MyBase.Shown
        DataGridView1.ClearSelection()
        DataGridView2.ClearSelection()
        DataGridView3.ClearSelection()


        Machine_util_det.Chart1.Enabled = False


        Me.ResumeLayout()

    End Sub




    Private Sub Report_PieChart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SuspendLayout()
        '   Me.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, 25)
        'Try
        '    Dim colors As Dictionary(Of String, Integer)

        '    'colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)
        '    colors = CSIFLEXSettings.StatusColors

        'Catch ex As Exception
        '    MessageBox.Show("CSIFLEX has encoutred an error while loading the machine color status colors from eNET ") ' & ex.Message)
        'End Try

        CSI_LIB.connectionString = CSIFLEXSettings.Instance.ConnectionString

        Try
            Config_report.BTN_Create.Text = "Update"

            'Dim data1(7) As Integer
            'Dim data2(7) As Integer
            'Dim data3(7) As Integer
            'Dim cycleTime As Double = 0



            'data1(0) = Val(Label28.Text)
            'data1(1) = Val(Label27.Text)
            'data1(2) = Val(Label26.Text)
            'data1(3) = Val(Label25.Text) ' + Val(Label24.Text) + Val(Label23.Text) + Val(Label22.Text)
            'data1 = Config_report.Piechart_data1

            'Chart1.Series("Series1").Points.DataBindY(data1)
            'If data1(3) <> 0 Then Chart1.Series("Series1").Points(3).Label = "Other"

            'data2(0) = Val(Label39.Text)
            'data2(1) = Val(Label38.Text)
            'data2(2) = Val(Label37.Text)
            'data2(3) = Val(Label36.Text) '+ Val(Label35.Text) + Val(Label34.Text) + Val(Label33.Text)

            ' Chart2.Series("Series1").Points.DataBindY(data2)
            ' If data2(3) <> 0 Then Chart2.Series("Series1").Points(3).Label = "Other"

            'data3(0) = Val(Label67.Text)
            'data3(1) = Val(Label66.Text)
            'data3(2) = Val(Label65.Text)
            'data3(3) = Val(Label64.Text) ' + Val(Label63.Text) + Val(Label62.Text) + Val(Label61.Text)

            ' Chart3.Series("Series1").Points.DataBindY(data3)
            ' If data3(3) <> 0 Then Chart3.Series("Series1").Points(3).Label = "Other"

            Me.MdiParent = Reporting_application



            BTN_Return.Visible = True
            'Label29.Visible = True


            Me.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, Config_report.Location.Y)
            Me.Height = Config_report.Height

            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            ' Me.BackColor = Color.Transparent
            SetStyle(ControlStyles.DoubleBuffer, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)




            ' Me.StartPosition = FormStartPosition.Manual


            '  Me.Location = Me.PointToClient(New Point(0.0))

            DataGridView1.DefaultCellStyle.SelectionBackColor = SystemColors.ControlDark
            DataGridView1.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText
            DataGridView2.DefaultCellStyle.SelectionBackColor = SystemColors.ControlDark
            DataGridView2.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText
            DataGridView3.DefaultCellStyle.SelectionBackColor = SystemColors.ControlDark
            DataGridView3.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText



        Catch ex As Exception
            MessageBox.Show("CSIFLEX has encoutred an error while loading the page") ' & ex.Message)
        End Try


        'Disable gridview sorting
        Dim i As Integer
        For i = 0 To DataGridView1.Columns.Count - 1
            DataGridView1.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i
        For i = 0 To DataGridView2.Columns.Count - 1
            DataGridView2.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i
        For i = 0 To DataGridView3.Columns.Count - 1
            DataGridView3.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i




        'Try
        '    Dim alpha As Integer = 255

        '    Dim backcolors As Color
        '    Dim backcolors2 As Color
        '    Dim backcolors3 As Color


        '    Dim colors As New Dictionary(Of String, Integer)
        '    colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)
        '    backcolors = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE ON"))
        '    backcolors = Color.FromArgb(alpha, backcolors.R, backcolors.G, backcolors.B)

        '    backcolors2 = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE OFF"))
        '    backcolors2 = Color.FromArgb(alpha, backcolors2.R, backcolors2.G, backcolors2.B)

        '    backcolors3 = System.Drawing.ColorTranslator.FromWin32(colors("SETUP"))
        '    backcolors3 = Color.FromArgb(alpha, backcolors3.R, backcolors3.G, backcolors3.B)

        '    Chart1.Series("Series1").Points.Item(0).Color = backcolors
        '    Chart1.Series("Series1").Points.Item(1).Color = backcolors2
        '    Chart1.Series("Series1").Points.Item(2).Color = backcolors3
        '    Chart1.Series("Series1").Points.Item(3).Color = Color.Yellow
        '    Chart1.Series("Series1").Points(3).CustomProperties = "Exploded = true"

        '    Chart2.Series("Series1").Points.Item(0).Color = backcolors
        '    Chart2.Series("Series1").Points.Item(1).Color = backcolors2
        '    Chart2.Series("Series1").Points.Item(2).Color = backcolors3
        '    Chart2.Series("Series1").Points.Item(3).Color = Color.Yellow
        '    Chart2.Series("Series1").Points(3).CustomProperties = "Exploded = true"

        '    Chart3.Series("Series1").Points.Item(0).Color = backcolors
        '    Chart3.Series("Series1").Points.Item(1).Color = backcolors2
        '    Chart3.Series("Series1").Points.Item(2).Color = backcolors3
        '    Chart3.Series("Series1").Points.Item(3).Color = Color.Yellow
        '    Chart3.Series("Series1").Points(3).CustomProperties = "Exploded = true"
        'Catch ex As Exception

        'End Try

        'adapt scroll bar
        DataGrid_ScrollBar(DataGridView1)
        DataGrid_ScrollBar(DataGridView2)
        DataGrid_ScrollBar(DataGridView3)

        Try
            If Machine_util_det.Visible = True Then



                Machine_util_det.Location = New System.Drawing.Point(Report_BarChart.Size.Width + Config_report.Size.Width, Report_BarChart.Location.Y)
                Machine_util_det.Height = Report_BarChart.Height



            End If
        Catch ex As Exception

        End Try
        ResumeLayout()

    End Sub

    Private Sub Report_PieChart_paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Using p As New Pen(Color.FromArgb(217, 217, 217), 2.0)
            e.Graphics.DrawRectangle(p, 0, 0, Me.Width, Me.Height)
        End Using


    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' adapt the datagrid with the scrollBar
    '---------------------------------------------------------- -------------------------------------------------------------
    Private Sub DataGrid_ScrollBar(DataGridView As DataGridView)
        Dim height As Integer = DataGridView.Rows.GetRowsHeight(DataGridViewElementStates.Displayed)
        Dim marge As Integer = 20
        Dim scrollWidth As Integer = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth

        If (DataGridView.Height > height + marge) Then
            If DataGridView.ScrollBars = ScrollBars.Vertical Then
                DataGridView.Columns(0).Width = DataGridView.Columns(0).Width + scrollWidth
            End If
            DataGridView.ScrollBars = ScrollBars.None
        Else
            If DataGridView.ScrollBars = ScrollBars.None Then
                DataGridView.Columns(0).Width = DataGridView.Columns(0).Width - scrollWidth
            End If
            DataGridView.ScrollBars = ScrollBars.Vertical
        End If
    End Sub

#Region "Move the forme"
    '-----------------------------------------------------------------------------------------------------------------------
    ' MOVE THE FORM
    '  
    '-----------------------------------------------------------------------------------------------------------------------
    Dim _MouseDown As Boolean, _MouseX As Integer, _MouseY As Integer
    Private Sub LIVE_mouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        _MouseDown = True
        _MouseX = e.X
        _MouseY = e.Y
        Me.Text = MousePosition.X & " " & MousePosition.Y
    End Sub

    Private Sub LIVE_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        _MouseDown = False

    End Sub

    Private Sub LIVE_Mousemove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove


        If _MouseDown = True Then
            Reporting_application.SuspendLayout()
            Me.Left = Me.Left + (e.X - _MouseX)
            Me.Top = Me.Top + (e.Y - _MouseY)
            If Me.Top < 20 Then Me.Top = 50
            '  If Me.Left < 20 Then Me.Left = 0
            Reporting_application.ResumeLayout(True)

        End If

    End Sub
#End Region

#Region "Return prev next combobox"
    '-----------------------------------------------------------------------------------------------------------------------
    ' Combo change
    '---------------------------------------------------------- -------------------------------------------------------------
    Private Sub CB_Report_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Report.SelectedIndexChanged

        Dim Text As String()
        Dim i As Integer

        Text = Split(CB_Report.Text, ":").Select(Function(t) t.Trim()).ToArray()

        Dim machineName = Text(0)
        Dim machTblName = CSI_LIB.RenameMachine(machineName)
        'Dim dateReport As DateTime = Text(1)

        Text(0) = CSI_LIB.RenameMachine(Text(0))

        Dim machineId = MySqlAccess.ExecuteScalar($"SELECT Id FROM csi_auth.tbl_ehub_conf WHERE machine_name = '{machineName}';", CSI_Library.CSI_Library.MySqlConnectionString)
        CSI_Library.CSI_Library.MachineId = machineId

        Dim machine As String
        Dim date_ As String
        Dim data1(7) As Integer
        Dim data2(7) As Integer
        Dim data3(7) As Integer

        Dim alpha As Integer = 255

        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        DataGridView3.Rows.Clear()
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()

        Try
            For i = 0 To UBound(Config_report.periode_returned) - 1

                If Report_BarChart.Pie_chart_source = 0 Then

                    machine = Config_report.periode_returned(i).machine_name
                    date_ = Config_report.periode_returned(i).date_

                    If machine = machineName And date_ = Text(1) Then
                        Call Config_report.fill_form_7(Config_report.periode_returned(i))
                        Exit For
                    End If

                ElseIf Report_BarChart.Pie_chart_source = 1 Then

                    machine = Report_BarChart.tmp_periode_returned(i).machine_name
                    date_ = Report_BarChart.tmp_periode_returned(i).date_

                    If machine = machineName And date_ = Text(1) Then
                        Call Config_report.fill_form_7(Report_BarChart.tmp_periode_returned(i))
                        Exit For
                    End If

                ElseIf Report_BarChart.Pie_chart_source = 2 Then

                    machine = Report_BarChart.temporary_periode_returned(i).machine_name
                    date_ = Report_BarChart.temporary_periode_returned(i).date_

                    If machine = Text(0) And date_ = Text(1) Then
                        Call Config_report.fill_form_7(Report_BarChart.temporary_periode_returned(i))
                        Exit For
                    End If

                Else '=3

                    machine = Report_BarChart.consolidated_periode(i).machine_name
                    date_ = Report_BarChart.consolidated_periode(i).date_

                    If machine = Text(0) And date_ = Text(1) Then
                        Call Config_report.fill_form_7(Report_BarChart.consolidated_periode(i))
                        Exit For
                    End If

                End If
            Next

        Catch ex As Exception
            Log.Error("Shift Error.", ex)
        End Try

    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' NEXT
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub BTN_Next_Click(sender As Object, e As EventArgs) Handles BTN_Next.Click
        Dim index_ As Integer
        index_ = CB_Report.SelectedIndex
        If index_ = CB_Report.Items.Count - 1 Then index_ = -1
        CB_Report.SelectedIndex = index_ + 1
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' prev
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub BTN_Prev_Click(sender As Object, e As EventArgs) Handles BTN_Prev.Click
        Dim index_ As Integer
        index_ = CB_Report.SelectedIndex
        If index_ = 0 Then index_ = CB_Report.Items.Count
        CB_Report.SelectedIndex = index_ - 1
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' return
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub BTN_Return_Click(sender As Object, e As EventArgs) Handles BTN_Return.Click

        'If Form3.CheckBox1.Checked = True Then
        'Else
        Report_BarChart.Visible = True
        Report_BarChart.MdiParent = Reporting_application
        ' Report_BarChart.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, 25)
        'If Machine_util_det.machine_util_det_wasactive = True Then
        '    Machine_util_det.Visible = True
        '    Machine_util_det.Show()
        'End If
        Me.Close()
        '   End If

    End Sub

#End Region

#Region "Clic On graph"

    Dim formshift As New Form_Shift

    Private Sub Chart_Click(sender As Object, e As EventArgs) Handles Chart1.Click, Chart2.Click, Chart3.Click

        Dim date_ As Date, machineName As String, EnetName As String, tableName As String, period As String

        Dim shift As Integer = 0

        Dim dgv As DataGridView = New DataGridView()

        Dim obj = DirectCast(sender, Chart)
        If obj.Name = "Chart1" Then
            shift = 1
            dgv = DataGridView1
        ElseIf obj.Name = "Chart2" Then
            shift = 2
            dgv = DataGridView2
        ElseIf obj.Name = "Chart3" Then
            shift = 3
            dgv = DataGridView3
        End If

        Try

            If Machine_util_det.machine_util_det_wasactive = True Then

                Machine_util_det.Visible = False

            End If

            If Not CB_Report.SelectedText.StartsWith("consolidated") Then

                Dim total As Integer = 0

                'For index As Integer = 0 To DataGridView1.RowCount - 1
                '    total += Convert.ToInt32(DataGridView1.Rows(index).Cells(2).Value)
                'Next
                For index As Integer = 0 To dgv.RowCount - 1
                    total += Convert.ToInt32(dgv.Rows(index).Cells(2).Value)
                Next

                If total = 0 Then
                    MessageBox.Show("No data available for this shift")
                Else
                    machineName = Split(CB_Report.Text, ":").Select(Function(t) t.Trim()).ToArray()(0)
                    period = Split(CB_Report.Text, ":").Select(Function(t) t.Trim()).ToArray()(1)

                    Dim machineInfo As MachineInfo = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.MachineName = machineName)
                    EnetName = machineInfo.EnetName
                    tableName = CSI_LIB.RenameMachine(EnetName)

                    If UBound(period.Split(" ")) > 2 Then
                        MessageBox.Show("The Timeline is available for a periode of 1 day")
                    Else

                        date_ = period

                        Timeline = CSI_LIB.TimeLine(date_, tableName, shift)

                        formshift = New Form_Shift
                        formshift.shiftno = shift
                        formshift.machineId = machineInfo.Id
                        formshift.machineName = machineInfo.MachineName

                        formshift.TopMost = True
                        formshift.Show()

                        Dim s As Screen
                        s = Screen.FromControl(Me)
                        formshift.Location = Reporting_application.Location + New Point(Me.Location.X + dgv.Location.X + Panel2.Location.X, Me.Location.Y + dgv.Location.Y + Panel2.Location.Y)

                    End If
                    ' = System.Windows.Forms.FormStartPosition(Me.Location.X + 219, 96)
                End If
            Else
                MessageBox.Show("No timeLine available for consolidated data, return to the previous window, and unbind to see a timeline (On a periode of one shift, on one day)")
            End If

        Catch ex As Exception
            MessageBox.Show("CSIFLEX cannot display the Timeline ") ' & ex.Message)
        End Try

    End Sub

    'Dim formshift2 As New Form_Shift

    'Private Sub Chart2_Click(sender As Object, e As EventArgs) 'Handles Chart2.Click
    '    Dim date_ As Date, machine_ As String, Combovalue As String()
    '    Try
    '        If Not CB_Report.SelectedText.StartsWith("consolidated") Then
    '            Dim total As Integer = 0
    '            For index As Integer = 0 To DataGridView2.RowCount - 1
    '                total += Convert.ToInt32(DataGridView2.Rows(index).Cells(2).Value)
    '            Next

    '            If total = 0 Then
    '                MessageBox.Show("No data available for this shift")
    '            Else
    '                Combovalue = Split(CB_Report.Text, " : ")
    '                Combovalue(0) = CSI_LIB.RenameMachine(Combovalue(0))

    '                machine_ = Combovalue(0)


    '                If UBound(Combovalue(1).Split(" ")) > 2 Then
    '                    MessageBox.Show("The Timeline is available for a periode of 1 day")
    '                Else
    '                    Dim machineName = Split(CB_Report.Text, ":").Select(Function(t) t.Trim()).ToArray()(0)
    '                    Dim machineInfo As MachineInfo = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.MachineName = machineName)

    '                    date_ = Combovalue(1)
    '                    Timeline2 = CSI_LIB.TimeLine(date_, machine_, 2)

    '                    formshift2 = New Form_Shift
    '                    formshift2.shiftno = 2
    '                    formshift2.machineId = machineInfo.Id
    '                    formshift2.machineName = machineInfo.MachineName

    '                    formshift2.TopMost = True
    '                    formshift2.Show()
    '                    Dim s As Screen
    '                    s = Screen.FromControl(Me)
    '                    formshift2.Location = Reporting_application.Location + New Point(8 + Me.Location.X + DataGridView2.Location.X + Panel3.Location.X, 56 + Me.Location.Y + DataGridView2.Location.Y + Panel3.Location.Y)

    '                End If
    '            End If
    '        Else
    '            MessageBox.Show("No timeLine available for consolidated data, return to the previous window, and unbind to see a timeline (On a periode of one shift, on one day)")
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("CSIFLEX cannot display the Timeline ") ' & ex.Message)
    '    End Try
    'End Sub

    'Dim formshift3 As New Form_Shift

    'Private Sub Chart3_Click(sender As Object, e As EventArgs) 'Handles Chart3.Click
    '    Dim date_ As Date, machine_ As String, combovalue As String()
    '    Try
    '        If Not CB_Report.SelectedText.StartsWith("Consolidated") Then
    '            Dim total As Integer = 0
    '            For index As Integer = 0 To DataGridView3.RowCount - 1
    '                total += Convert.ToInt32(DataGridView3.Rows(index).Cells(2).Value)
    '            Next
    '            If total = 0 Then
    '                MessageBox.Show("No data available for this shift")
    '            Else
    '                combovalue = Split(CB_Report.Text, " : ")
    '                combovalue(0) = CSI_LIB.RenameMachine(combovalue(0))

    '                machine_ = combovalue(0)

    '                If UBound(combovalue(1).Split(" ")) > 2 Then
    '                    MessageBox.Show("The Timeline is available for a periode of 1 day")
    '                Else
    '                    Dim machineName = Split(CB_Report.Text, ":").Select(Function(t) t.Trim()).ToArray()(0)
    '                    Dim machineInfo As MachineInfo = CSI_Library.CSI_Library.MachinesInfo.FirstOrDefault(Function(m) m.MachineName = machineName)

    '                    date_ = combovalue(1)
    '                    Timeline3 = CSI_LIB.TimeLine(date_, machine_, 3)

    '                    formshift3 = New Form_Shift
    '                    formshift3.shiftno = 3
    '                    formshift3.machineId = machineInfo.Id
    '                    formshift3.machineName = machineInfo.MachineName

    '                    formshift3.TopMost = True
    '                    formshift3.Show()

    '                    Dim s As Screen
    '                    s = Screen.FromControl(Me)
    '                    formshift3.Location = Reporting_application.Location + New Point(8 + Me.Location.X + DataGridView3.Location.X + Panel4.Location.X, 56 + Me.Location.Y + DataGridView3.Location.Y + Panel4.Location.Y)

    '                End If
    '            End If
    '        Else
    '            MessageBox.Show("No timeLine available for consolidated data, return to the previous window, and unbind to see a timeline (On a periode of one shift, on one day)")
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("CSIFLEX cannot display the Timeline ") ' & ex.Message)
    '    End Try
    'End Sub
#End Region

#Region "hover on graph"
    Private Sub Chart1_hover(sender As Object, e As EventArgs) Handles Chart1.MouseHover
        Dim index As Integer
        For index = 3 To Chart1.Series("Series1").Points.Count - 1
            Chart1.Series("Series1").Points(index).CustomProperties = "Exploded = true"
        Next
    End Sub

    Sub Chart1_mouse_leave(sender As Object, e As EventArgs) Handles Chart1.MouseLeave
        Dim index As Integer
        For index = 3 To Chart1.Series("Series1").Points.Count - 1
            Chart1.Series("Series1").Points(index).CustomProperties = "Exploded = false"
        Next

    End Sub

    Private Sub Chart2_hover(sender As Object, e As EventArgs) Handles Chart2.MouseHover
        Dim index As Integer
        For index = 3 To Chart2.Series("Series1").Points.Count - 1
            Chart2.Series("Series1").Points(index).CustomProperties = "Exploded = true"
        Next
    End Sub

    Sub Chart2_mouse_leave(sender As Object, e As EventArgs) Handles Chart2.MouseLeave
        Dim index As Integer
        For index = 3 To Chart2.Series("Series1").Points.Count - 1
            Chart2.Series("Series1").Points(index).CustomProperties = "Exploded = false"
        Next
    End Sub

    Private Sub Chart3_hover(sender As Object, e As EventArgs) Handles Chart3.MouseHover
        Dim index As Integer
        For index = 3 To Chart3.Series("Series1").Points.Count - 1
            Chart3.Series("Series1").Points(index).CustomProperties = "Exploded = true"
        Next
    End Sub

    Sub Chart3_mouse_leave(sender As Object, e As EventArgs) Handles Chart3.MouseLeave
        Dim index As Integer
        For index = 3 To Chart3.Series("Series1").Points.Count - 1
            Chart3.Series("Series1").Points(index).CustomProperties = "Exploded = false"
        Next
    End Sub

#End Region





    Dim WithEvents mPrintDocument As New PrintDocument
    Dim mPrintBitMap As Bitmap

    Private Sub Label29_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub undock_Click(sender As Object, e As EventArgs) Handles undock.Click
        If Me.MdiParent Is Nothing Then

            Me.MdiParent = Reporting_application
            Me.FormBorderStyle = FormBorderStyle.None
            Me.Height = Config_report.Height
            undock.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone)
            Me.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, 0)
        Else
            Me.MdiParent = Nothing
            Me.FormBorderStyle = FormBorderStyle.FixedSingle
            Me.FormBorderStyle = FormBorderStyle.SizableToolWindow
            undock.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipNone)
            '  Me.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, 0)
        End If
    End Sub

    Private Sub m_PrintDocument_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles mPrintDocument.PrintPage
        ' Draw the image centered.
        Dim lWidth As Integer = e.MarginBounds.X + (e.MarginBounds.Width - mPrintBitMap.Width) \ 2
        Dim lHeight As Integer = e.MarginBounds.Y + (e.MarginBounds.Height - mPrintBitMap.Height) \ 2
        e.Graphics.DrawImage(mPrintBitMap, lWidth, lHeight)

        ' There's only one page.
        e.HasMorePages = False
    End Sub
    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Try

            ' Copy the form's image into a bitmap.
            mPrintBitMap = New Bitmap(Me.Width, Me.Width)
            Dim lRect As System.Drawing.Rectangle
            lRect.Width = Me.Width
            lRect.Height = Me.Width
            Me.DrawToBitmap(mPrintBitMap, lRect)


            ' Make a PrintDocument and print.
            mPrintDocument = New PrintDocument
            ' mPrintDocument.Print()

            mPrintDocument.OriginAtMargins = True
            Dim margins As New System.Drawing.Printing.Margins(5, 0, 80, 100)



            Dim ps As New PageSettings
            ps.Landscape = True
            ps.Margins = margins
            mPrintDocument.DefaultPageSettings = ps

            PrintDialog1.Document = mPrintDocument
            PrintDialog1.PrinterSettings.DefaultPageSettings.Landscape = True

            If (PrintDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then

                PrintPreviewDialog1.Document = mPrintDocument
                PrintPreviewDialog1.ShowDialog()

            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

End Class
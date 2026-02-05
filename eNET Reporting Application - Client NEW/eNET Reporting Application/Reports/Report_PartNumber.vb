Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows
Imports System.IO
Imports System.IO.File
Imports CSI_Library.CSI_DATA
Imports System.Threading
Imports System.Globalization
Imports System.Drawing.Printing
Imports System.Data.SQLite
Imports MySql.Data.MySqlClient
Imports System.ComponentModel.ListSortDirection
Imports System.Windows.Forms.DataGridViewColumn
'PRINT

'Imports System.Drawing

Public Class Report_PartNumber
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
    Public partData As DataTable
    Public percentageData As DataTable
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath

    'report variables
    Private dateStart As String
    Private dateEnd As String
    Private license As Integer
    Private machine_() As String
    Private realNameMachine_() As String



    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Load Report_BarChart
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Report_BarChart_Loaded(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.ResumeLayout()
    End Sub
    Private Sub Report_BarChart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = Reporting_application

        'Dim alpha As Integer = 255
        'Dim backcolors As Color
        'Dim backcolors2 As Color
        'Dim backcolors3 As Color

        'Dim foreColor As Color
        'Dim foreColor2 As Color
        'Dim foreColor3 As Color

        'Dim colors As Dictionary(Of String, Integer)
        'colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)

        Try

            Me.SuspendLayout()
            Me.MdiParent = Reporting_application
            Me.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Size.Width, 25)

            'If Exists(rootPath & "\sys\targetColor_.csys") Then
            '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\targetColor_.csys")
            '        targetColor_ = CInt(reader.ReadLine())
            '        reader.Close()
            '    End Using
            'End If

            'If Exists(rootPath & "\sys\target_.csys") Then
            '    Using reader As StreamReader = New StreamReader(rootPath & "\sys\target_.csys")
            '        target_ = CInt(reader.ReadLine()) 'this line will call NumericUpDownValueChanged
            '        reader.Close()
            '    End Using
            'End If


            'SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            'Me.BackColor = Color.Transparent
            'SetStyle(ControlStyles.DoubleBuffer, True)
            'SetStyle(ControlStyles.AllPaintingInWmPaint, True)



            CB_Report.SelectedIndex = CB_Report.Items.Count - 1

        Catch ex As Exception
            'MessageBox.Show("3" & ex.Message)
        End Try


    End Sub

    Public Sub setData()
        '// variables setting
        dateStart = Config_report.dateStart.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))
        dateEnd = Config_report.dateEnd.ToString("yyyy-MM-dd", CultureInfo.CreateSpecificCulture("en-US"))
        license = CSI_LIB.CheckLic(2)
        setMachineTable()
        '//

        partData = setPartNo()
        percentageData = setPercentage()
    End Sub

    Public Sub setMachineTable()
        machine_ = Config_report.machineList
        realNameMachine_ = machine_.Clone

        For i = 0 To UBound(machine_)
            machine_(i) = CSI_LIB.RenameMachine(machine_(i))
            realNameMachine_(i) = CSI_LIB.RealNameMachine(realNameMachine_(i))
        Next
    End Sub

    Public Function setPartNo() As DataTable

        Dim command As String = ""
        Dim bigTable, temp_table As New DataTable

        If Not license = 3 Then 'If CSI_Lib.isClientSQlite Then

            Dim reader As SQLiteDataReader
            Dim tmp_table_cmd As New SQLiteCommand

            Using sqlConn As SQLiteConnection = New SQLiteConnection(CSI_Library.CSI_Library.sqlitedbpath)
                sqlConn.Open()
                If machine_.Length > 0 Then
                    For i = 0 To UBound(machine_)

                        command = "SELECT '" + realNameMachine_(i) + "' as MchName, date(time_) as date, Mid(Status, 9)  as partName, count(Status) as counting " +
                            " FROM  tbl_" & machine_(i) & "  " +
                            " WHERE Status LIKE '_PARTNO:%' and time_  between '" + dateStart + "' and '" + dateEnd + "'  " +
                            "group by Status, date(time_) "

                        command = "SELECT '" + realNameMachine_(i) + "' as MchName, strftime('%Y-%m-%d', time_) as date, substr(Status, 9)  as partName, (count(Status)*1.0) as counting " +
                            " FROM  [tbl_" & machine_(i) & "]  " +
                            " WHERE [Status] LIKE '_PARTNO:%' and time_  between '" + dateStart + "' and '" + dateEnd + "'  " +
                            "group by Status, strftime('%Y-%m-%d', time_)"

                        temp_table.Clear()
                        tmp_table_cmd.CommandText = command
                        tmp_table_cmd.Connection = sqlConn

                        reader = tmp_table_cmd.ExecuteReader
                        temp_table.Load(reader)
                        bigTable.Merge(temp_table)

                    Next
                End If
            End Using

        Else
            Dim reader As MySqlDataReader
            Dim tmp_table_cmd As New MySqlCommand

            Dim db_authPath As String = Nothing
            If (File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
                Using streader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")
                    db_authPath = streader.ReadLine()
                End Using
            End If
            Dim connectionString As String
            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + CSI_Library.CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"


            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)
                sqlConn.Open()
                If machine_.Length > 0 Then
                    For i = 0 To UBound(machine_)

                        temp_table = New DataTable

                        command = "SELECT '" + realNameMachine_(i) + "' as MchName, date(time_) as date, Mid(Status, 9)  as partName, count(Status) as counting " +
                            " FROM  tbl_" & machine_(i) & "  " +
                            " WHERE Status LIKE '_PARTNO:%' and time_  between '" + dateStart + "' and '" + dateEnd + "'  " +
                            "group by Status, date(time_) "

                        temp_table.Clear()
                        tmp_table_cmd.CommandText = command
                        tmp_table_cmd.Connection = sqlConn

                        reader = tmp_table_cmd.ExecuteReader
                        temp_table.Load(reader)
                        bigTable.Merge(temp_table)

                    Next
                End If


            End Using
        End If

        Return bigTable
    End Function

    Public Function setPercentage() As DataTable

        Dim command As String = ""
        Dim PerTable, temp_table As New DataTable

        If Not license = 3 Then 'If CSI_Lib.isClientSQlite Then

            Dim reader As SQLiteDataReader
            Dim tmp_table_cmd As New SQLiteCommand

            Using sqlConn As SQLiteConnection = New SQLiteConnection(CSI_Library.CSI_Library.sqlitedbpath)
                sqlConn.Open()
                If machine_.Length > 0 Then
                    For i = 0 To UBound(machine_)



                        command = "SELECT '" + realNameMachine_(i) + "' as MchName, substr(Status, 9)  as partName, count(Status)*1 as occurrence, round((count(Status)*1.0 / (SELECT (count(Status)*1.0) as percentage  FROM  [tbl_" & machine_(i) & "]   WHERE [Status] LIKE '_PARTNO:%' and time_   between '" + dateStart + "' and '" + dateEnd + "') * 100.0), 2) as percentage " +
                            " FROM  [tbl_" & machine_(i) & "]  " +
                            " WHERE [Status] LIKE '_PARTNO:%' and time_  between '" + dateStart + "' and '" + dateEnd + "'  " +
                            "group by Status order by percentage desc"



                        temp_table.Clear()
                        tmp_table_cmd.CommandText = command
                        tmp_table_cmd.Connection = sqlConn

                        reader = tmp_table_cmd.ExecuteReader
                        temp_table.Load(reader)
                        PerTable.Merge(temp_table)

                    Next
                End If

            End Using

        Else
            Dim reader As MySqlDataReader
            Dim tmp_table_cmd As New MySqlCommand

            Dim db_authPath As String = Nothing
            If (File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
                Using streader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")
                    db_authPath = streader.ReadLine()
                End Using
            End If
            Dim connectionString As String
            connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + CSI_Library.CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"


            Using sqlConn As MySqlConnection = New MySqlConnection(connectionString)
                sqlConn.Open()
                If machine_.Length > 0 Then
                    For i = 0 To UBound(machine_)

                        temp_table = New DataTable

                        command = "SELECT '" + realNameMachine_(i) + "' as MchName, Mid(Status, 9)  as partName, count(Status)*1 as occurrence, round((count(Status)*1.0 / (SELECT (count(Status)*1.0) as percentage  FROM  tbl_" & machine_(i) & "   WHERE Status LIKE '_PARTNO:%' and time_   between '" + dateStart + "' and '" + dateEnd + "') * 100.0), 2) as percentage " +
                            " FROM  tbl_" & machine_(i) & "  " +
                            " WHERE Status LIKE '_PARTNO:%' and time_  between '" + dateStart + "' and '" + dateEnd + "'  " +
                            "group by Status order by percentage desc"

                        temp_table.Clear()
                        tmp_table_cmd.CommandText = command
                        tmp_table_cmd.Connection = sqlConn

                        reader = tmp_table_cmd.ExecuteReader
                        temp_table.Load(reader)
                        PerTable.Merge(temp_table)

                    Next
                End If
            End Using
        End If

        Return PerTable
    End Function


    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Combobox
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Report.SelectedIndexChanged

        CreateReportsByMachine()

    End Sub

    Private Sub CreateReportsByMachine()
        Dim mach As String = CB_Report.SelectedItem.ToString()
        mach = mach.Substring(0, mach.IndexOf(":")).Trim
        LBL_MachineName.Text = mach
        mach = CSI_LIB.RenameMachine(mach)


        DGV_partnumber.DataSource = SelectMachineData(mach, partData)
        DGV_partnumber.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill

        DGV_percentage.DataSource = SelectMachineData(mach, percentageData)
        DGV_percentage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill

        createPieChart(mach, percentageData)
        DGV_top.ClearSelection()
    End Sub

    Private Sub CreateReportsByParts()
        Dim part As String = CB_partNumber.SelectedItem.ToString()
        LBL_MachineName.Text = part

        PartFilter()
        DGV_partnumber.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        Dim copyDataTable As DataTable = percentageData.Copy()
        DGV_percentage.DataSource = SelectPartData(part, copyDataTable)
        DGV_percentage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill

        createPieChartByPart(part, copyDataTable)
        DGV_top.ClearSelection()
    End Sub

    Private Sub createPieChart(mach As String, data As DataTable)
        Dim data1(4) As Integer
        Dim result() As DataRow = data.Select("MchName = '" + CSI_LIB.RealNameMachine(mach) + "'").Clone
        Dim count As Integer = 0
        Dim otherPercentage As Double = 100
        Dim otherOccurence As Integer
        Dim totalOccurence As Integer = 0

        DGV_top.Columns(0).HeaderText = "Part Numbers"
        DGV_top.Rows.Clear()

        For Each row As DataRow In result

            totalOccurence += row.Item(2)

            If (count < 3) Then
                data1(count) = row.Item(3)
                otherPercentage -= data1(count)

                count += 1

                DGV_top.Rows.Add(row.Item(1), row.Item(2), row.Item(3))
            Else

                data1(count) = otherPercentage
                otherOccurence += row.Item(2)

            End If

        Next

        DGV_top.Rows.Add("Others", otherOccurence, otherPercentage)
        setTableStyle(DGV_top, DGV_top.Rows.Count)
        DGV_top.Rows.Add("Total", totalOccurence, "100")

        Chart1.Series("Series1").Points.DataBindY(data1)
    End Sub

    Private Sub createPieChartByPart(part As String, data As DataTable)
        Dim data1(4) As Integer
        Dim result() As DataRow = data.Select("partName = '" + part + "'").Clone
        Dim count As Integer = 0
        Dim otherPercentage As Double = 100
        Dim otherOccurence As Integer
        Dim totalOccurence As Integer = 0

        DGV_top.Columns(0).HeaderText = "Machines"

        DGV_top.Rows.Clear()

        For Each row As DataRow In result

            totalOccurence += row.Item(2)
        Next

        For Each row As DataRow In result

            If (count < 3) Then
                data1(count) = row.Item(2) / totalOccurence * 100
                otherPercentage -= data1(count)

                count += 1

                DGV_top.Rows.Add(row.Item(0), row.Item(2), row.Item(2) / totalOccurence * 100)
            Else

                data1(count) = otherPercentage
                otherOccurence += row.Item(2)

            End If

        Next

        DGV_top.Rows.Add("Others", otherOccurence, otherPercentage)
        setTableStyle(DGV_top, DGV_top.Rows.Count)
        DGV_top.Rows.Add("Total", totalOccurence, "100")

        Chart1.Series("Series1").Points.DataBindY(data1)
    End Sub

    Private Sub setTableStyle(dgv As DataGridView, nbRow As Integer)

        Try
            dgv.Rows(0).DefaultCellStyle.BackColor = Color.Green
            dgv.Rows(0).DefaultCellStyle.SelectionBackColor = Color.Green
            dgv.Rows(0).DefaultCellStyle.SelectionForeColor = Color.Black

            dgv.Rows(1).DefaultCellStyle.BackColor = Color.Red
            dgv.Rows(1).DefaultCellStyle.SelectionBackColor = Color.Red
            dgv.Rows(1).DefaultCellStyle.SelectionForeColor = Color.Black

            dgv.Rows(2).DefaultCellStyle.BackColor = Color.Blue
            dgv.Rows(2).DefaultCellStyle.SelectionBackColor = Color.Blue
            dgv.Rows(2).DefaultCellStyle.SelectionForeColor = Color.Black

        Catch ex As Exception

        End Try

        dgv.Rows(nbRow - 1).DefaultCellStyle.BackColor = Color.Yellow
        dgv.Rows(nbRow - 1).DefaultCellStyle.SelectionBackColor = Color.Yellow
        dgv.Rows(nbRow - 1).DefaultCellStyle.SelectionForeColor = Color.Black
    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' SelectMachineData
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Function SelectMachineData(mach As String, data As DataTable) As DataTable
        Dim result() As DataRow = data.Select("MchName = '" + CSI_LIB.RealNameMachine(mach) + "'").Clone
        Dim dt As DataTable = data.Clone
        dt.Columns.Remove("MchName")


        For Each row As DataRow In result
            dt.ImportRow(row)
        Next

        Return dt
    End Function

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' SelectPartData
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Function SelectPartData(part As String, data As DataTable) As DataTable
        Dim result() As DataRow = data.Select("partName = '" + part + "'").Clone
        Dim dt As DataTable = data.Clone
        Dim totalOccurence As Double
        dt.Columns.Remove("partName")

        For Each row As DataRow In result
            totalOccurence += row.Item(2)
        Next

        For Each row As DataRow In result
            row(3) = row(2) / totalOccurence * 100
            dt.ImportRow(row)
        Next

        Return dt
    End Function

    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    '-----------------------------------------------------------------------------------------------------------------------
    ' Next
    '-----------------------------------------------------------------------------------------------------------------------
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub BTN_Next_Click(sender As Object, e As EventArgs) Handles BTN_Next.Click

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
            'MessageBox.Show("2" & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Report_BarChart.Visible = True
        Report_BarChart.MdiParent = Reporting_application
        Report_BarChart.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, 25)

        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If (Button2.Text = "by part") Then
            Button2.Text = "by machines"
            CB_Report.Enabled = False
            BTN_Prev.Enabled = False
            BTN_Next.Enabled = False
            CB_partNumber.Enabled = True
            Label2.Enabled = True
            TB_PartsNumber.Enabled = True
            PartFilter()
            DGV_partnumber.DataSource = Nothing
            DGV_percentage.DataSource = Nothing
            DGV_top.Rows.Clear()
            Dim data1(1) As Integer
            Chart1.Series("Series1").Points.DataBindY(data1)
            ' set_CbPartNumber()
            CB_filter()
            CB_partNumber.SelectedIndex = 0
        Else
            Button2.Text = "by part"
            CB_Report.Enabled = True
            BTN_Prev.Enabled = True
            BTN_Next.Enabled = True
            CB_partNumber.Enabled = False
            Label2.Enabled = False
            TB_PartsNumber.Enabled = False

            CreateReportsByMachine()
        End If


    End Sub

    Private Sub set_CbPartNumber()
        For Each row As DataRow In percentageData.Rows
            CB_partNumber.Items.Add(row("partName"))
        Next
    End Sub

    Private Sub PartFilter()
        Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
        ' Change culture to en-US.
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")

        If Not (partData Is Nothing) Then
            Dim tmp_table_rows As System.Data.DataRow() = partData.Select()

            If UBound(tmp_table_rows) > 0 Then
                Dim tmp_table_ As System.Data.DataTable = tmp_table_rows.CopyToDataTable

                Dim view As New DataView(tmp_table_)
                view.RowFilter = ("partName like '" + CB_partNumber.Text & "*'")
                DGV_partnumber.DataSource = view
            Else
                Dim view As New DataView(partData)
                view.RowFilter = ("partName like '" + CB_partNumber.Text & "*'")


                DGV_partnumber.DataSource = Nothing


            End If
        End If
        DGV_partnumber.Refresh()
        Thread.CurrentThread.CurrentCulture = originalCulture
    End Sub

    Private Sub CB_partNumber_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_partNumber.SelectedIndexChanged

        CreateReportsByParts()

    End Sub

    Private Sub TB_PartsNumber_KeyUp(sender As Object, e As KeyEventArgs) Handles TB_PartsNumber.KeyUp
        CB_filter()
    End Sub

    Private Sub CB_filter()

        CB_partNumber.Items.Clear()

        If Not (percentageData Is Nothing) Then
            Dim tmp_table_rows As System.Data.DataRow() = percentageData.Select()

            If UBound(tmp_table_rows) > 0 Then
                Dim tmp_table_ As System.Data.DataTable = tmp_table_rows.CopyToDataTable

                Dim view As New DataView(tmp_table_)
                view.RowFilter = ("partName like '" + TB_PartsNumber.Text & "*'")
                Dim dt As DataTable = view.ToTable()


                For Each row As DataRow In dt.Rows
                    CB_partNumber.Items.Add(row("partName"))
                Next
            Else
                Dim view As New DataView(percentageData)
                view.RowFilter = ("partName like '" + TB_PartsNumber.Text & "*'")


                DGV_partnumber.DataSource = Nothing


            End If
        End If
    End Sub

    Private Sub TB_PartsNumber_TextChanged(sender As Object, e As EventArgs) Handles TB_PartsNumber.TextChanged

    End Sub

    Private Sub BTN_Return_Click(sender As Object, e As EventArgs) Handles BTN_Return.Click
        Report_BarChart.Visible = True
        Report_BarChart.MdiParent = Reporting_application
        Report_BarChart.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Width, 25)
        'If Machine_util_det.machine_util_det_wasactive = True Then
        '    Machine_util_det.Visible = True
        '    Machine_util_det.Show()
        'End If
        Me.Close()
    End Sub
End Class
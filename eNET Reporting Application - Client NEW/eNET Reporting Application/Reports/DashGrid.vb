'Globalvar
Imports CSI_Library

'powerpack
'Imports Microsoft.VisualBasic.PowerPacks


Public Class DashGrid

    Private scrollTimer As New Timer()
    Private refreshTimer As New Timer()
    Private scrollJump As Integer = 1

    Dim cellpadding As Integer = 5
    Dim nbcolumn As Integer = 7

    Dim colors As Dictionary(Of String, Integer)

    Private Sub DashGrid_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DGV_LiveStatus.AllowDrop = True

        'P_Dash.AutoScrollMinSize = New Size(0, 1000)
        'AddHandler P_Dash.MouseMove, AddressOf P_Dash_MouseMove
        'AddHandler P_Dash.DragEnter, AddressOf P_Dash_DragEnter
        'AddHandler P_Dash.DragOver, AddressOf P_Dash_DragOver
        'AddHandler P_Dash.QueryContinueDrag, AddressOf P_Dash_QueryContinueDrag
        'AddHandler scrollTimer.Tick, AddressOf scrollTimer_Tick
        AddHandler refreshTimer.Tick, AddressOf refreshTimer_Tick

        Me.SuspendLayout()
        Me.MdiParent = Reporting_application
        Me.Location = New System.Drawing.Point(285, 25)

        'Resize
        Dim freespace = New Size(Reporting_application.Width - Config_report.Width - 40, Reporting_application.Height - Reporting_application.MenuStrip1.Height)
        Me.Size = freespace
        DGV_LiveStatus.Size = New Size(Me.Width, Me.Height)

        'SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        'Me.BackColor = Color.Transparent

        'SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        'Me.DoubleBuffered = True
        'SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        'SetStyle(ControlStyles.UserPaint, True)
        'Me.UpdateStyles()

        colors = GetEnetGraphColor(Reporting_application.chemin_eNET)

        GetData()
        scrollTimer.Start()

        refreshTimer.Interval = 1000 * 5 '1000 * 60 * 5 ' refresh every 5 min
        refreshTimer.Start()


    End Sub

    Private Sub DashGrid_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.ResumeLayout()
        'P_Dash.ResumeLayout()
    End Sub

    Private Function GetRectDimension(percentage As Integer) As Integer
        GetRectDimension = 0
        If (percentage > 0 And percentage < 100) Then
            GetRectDimension = ((DGV_LiveStatus.Width - (cellpadding * nbcolumn)) * (percentage / 100))
        End If
    End Function

    'Public Class MyPanel
    '    Inherits System.Windows.Forms.Panel
    '    Public Sub New()
    '        Me.SetStyle(System.Windows.Forms.ControlStyles.UserPaint Or System.Windows.Forms.ControlStyles.AllPaintingInWmPaint Or System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, True)
    '    End Sub
    'End Class

    Private Sub GetData()
        Dim ListOfMachineSelected As String() = Config_report.read_tree()
        'DGV_LiveStatus.DataSource = ListOfMachineSelected
        Dim table As DataTable = New DataTable()
        table.Columns.Add("Machine")
        table.Columns.Add("Status")
        table.Columns.Add("PartNumber")
        table.Columns.Add("CycleCount")
        table.Columns.Add("LastCycle")
        table.Columns.Add("CurrentCycle")
        table.Columns.Add("FeedOverride")


        'Dim rect_tmp As Rectangle 'Microsoft.VisualBasic.PowerPacks.RectangleShape

        Dim start_x As Integer = 0
        Dim start_y As Integer = 0
        Dim width_mach As Double = GetRectDimension(15)
        Dim width_status As Double = GetRectDimension(15)
        Dim width_partno As Double = GetRectDimension(20)
        Dim width_cyccnt As Double = GetRectDimension(8)
        Dim width_lastcyc As Double = GetRectDimension(15)
        Dim width_currcyc As Double = GetRectDimension(15)
        Dim width_feed As Double = GetRectDimension(8)

        Dim height As Integer = 35 'same height for everyone

        'Dim bordercolor As Color, backcolor As Color
        'bordercolor = Color.CadetBlue
        'backcolor = Color.White

        'rect_tmp = DrawRectangle(True, start_y + cellpadding, start_x + cellpadding, width_mach, height, backcolor, bordercolor, "Machine")
        'rect_tmp = DrawRectangle(True, rect_tmp.Top, rect_tmp.Left + rect_tmp.Width + cellpadding, width_status, height, backcolor, bordercolor, "Status")
        'rect_tmp = DrawRectangle(True, rect_tmp.Top, rect_tmp.Left + rect_tmp.Width + cellpadding, width_partno, height, backcolor, bordercolor, "PartNumber")
        'rect_tmp = DrawRectangle(True, rect_tmp.Top, rect_tmp.Left + rect_tmp.Width + cellpadding, width_cyccnt, height, backcolor, bordercolor, "Count")
        'rect_tmp = DrawRectangle(True, rect_tmp.Top, rect_tmp.Left + rect_tmp.Width + cellpadding, width_lastcyc, height, backcolor, bordercolor, "LastCycle")
        'rect_tmp = DrawRectangle(True, rect_tmp.Top, rect_tmp.Left + rect_tmp.Width + cellpadding, width_currcyc, height, backcolor, bordercolor, "CurrentCycle")
        'rect_tmp = DrawRectangle(True, rect_tmp.Top, rect_tmp.Left + rect_tmp.Width + cellpadding, width_feed, height, backcolor, bordercolor, "FeedOvrd")

        Dim row As DataRow
        Dim status As String, partnu As String, cyclecount As Integer, lastcycle As Date, currentcycle As Date, feedoverride As Double
        Dim lastcyc_str As String, currcyc_str As String

        'reset temp for new panel
        'rect_tmp = New Rectangle

        For Each s As String In ListOfMachineSelected
            'Get Status
            If (GlobalVariables.ListOfMachine.ContainsKey(s)) Then
                'System.Console.WriteLine(GlobalVariables.ListOfMachine.Item(s).Statut)
                row = table.NewRow()
                row("Machine") = s
                status = GlobalVariables.ListOfMachine.Item(s).Statut.Trim()
                partnu = GlobalVariables.ListOfMachine.Item(s).PartNo.Trim()
                cyclecount = GlobalVariables.ListOfMachine.Item(s).CycleCount
                lastcycle = GlobalVariables.ListOfMachine.Item(s).LastCycle
                currentcycle = GlobalVariables.ListOfMachine.Item(s).CurrentCycle
                feedoverride = GlobalVariables.ListOfMachine.Item(s).feedOverride

                lastcyc_str = lastcycle.ToString("HH:mm:ss") 'lastcycle.Hour.ToString() 'lastcycle.ToShortTimeString()
                currcyc_str = currentcycle.ToString("HH:mm:ss")

                row("Status") = status
                row("PartNumber") = partnu
                row("CycleCount") = cyclecount
                row("LastCycle") = lastcyc_str
                row("CurrentCycle") = currcyc_str
                row("FeedOverride") = feedoverride

                table.Rows.Add(row)


            End If
        Next

        DGV_LiveStatus.DataSource = table
        'last column is filling, other are percentage
        DGV_LiveStatus.Columns(DGV_LiveStatus.Columns.Count - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        'Dim imgcell As New DataGridViewImageCell
        'imgcell.
        'DGV_LiveStatus(1, 1) = imgcell
        'P_Dash.AutoScroll = True
        'DGV_LiveStatus.AutoScrollMinSize = New Size(rect_tmp.Right + cellpadding, rect_tmp.Bottom + cellpadding)


        'DGV_LiveStatus.Rows(0).Cells(0).Style.Font = smallFont
        'DGV_LiveStatus.InvalidateCell(0, 0)

        ''dgvStatsTable.Invalidate()
        'DGV_LiveStatus.Refresh()


        'Change cell font
        Dim fontsize As Single
        fontsize = 1.7F
        Dim bigfont = New Font(DGV_LiveStatus.Font.FontFamily, DGV_LiveStatus.Font.Size * fontsize) 'New Font("Arial", 8.5F, GraphicsUnit.Pixel)


        'DGV_LiveStatus.col()
        DGV_LiveStatus.ColumnHeadersDefaultCellStyle.Font = bigfont
        For Each c As DataGridViewColumn In DGV_LiveStatus.Columns
            c.DefaultCellStyle.Font = bigfont
        Next



        'For Each r As DataGridViewRow In DGV_LiveStatus.Rows
        '    r.DefaultCellStyle.Font = bigfont
        'Next

        'P_Dash.Controls.Add(New rect)
        'DrawRectangle(10, 10, 75, 25, Color.Lime, Color.Red, "WOOHOO")
        'DrawRectangle(10, 40, 75, 25, Color.Lime, Color.Red, "WOOHOO")

    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' the colors from eNET for each status, gives a dictionary of (color,status), needs the eNET dir rep = Form1.chemin_eNET
    '-----------------------------------------------------------------------------------------------------------------------
    Function GetEnetGraphColor(eNETrep As String) As Dictionary(Of String, Integer)
        Dim file As System.IO.StreamReader

        Dim color_list As New Dictionary(Of String, Integer), line As String()

        If Not My.Computer.FileSystem.FileExists(eNETrep + "\_SETUP\GraphColor.csys") Then

        Else
            file = My.Computer.FileSystem.OpenTextFileReader(eNETrep + "\_SETUP\GraphColor.csys")
            While Not file.EndOfStream
                line = file.ReadLine().Split(",")
                If line(1) <> "" Then
                    If color_list.ContainsKey(line(1)) Then

                    Else
                        color_list.Add(line(1).ToUpperInvariant, line(0))
                    End If

                End If

            End While
            file.Close()
        End If

        GetEnetGraphColor = color_list
    End Function

    'Private Function DrawRectangle(top As Integer, left As Integer, width As Integer, height As Integer, backcolor As Color, bordercolor As Color, desc As String) As Rectangle 'Microsoft.VisualBasic.PowerPacks.RectangleShape
    '    'Dim canvas As New Microsoft.VisualBasic.PowerPacks.ShapeContainer
    '    'Dim rect1 As New Microsoft.VisualBasic.PowerPacks.RectangleShape
    '    ' Set the form as the parent of the ShapeContainer.
    '    'If (header) Then
    '    '    canvas.Parent = DGV_LiveStatus
    '    'Else
    '    '    canvas.Parent = DGV_LiveStatus
    '    'End If
    '    ' Set the ShapeContainer as the parent of the RectangleShape.
    '    rect1.Parent = canvas
    '    ' Set the properties of the rectangle.
    '    rect1.Left = left
    '    rect1.Top = top
    '    rect1.Width = width
    '    rect1.Height = height
    '    rect1.BackColor = backcolor
    '    rect1.BorderColor = bordercolor
    '    rect1.AccessibleDescription = desc

    '    rect1.BorderStyle = Drawing2D.DashStyle.Solid
    '    'rect1.BackStyle = BackStyle.Opaque
    '    rect1.BorderWidth = 1
    '    rect1.CornerRadius = 3

    '    'AddHandler rect1.Paint, AddressOf Me.RectangleShape_Paint 'Me.P_Dash_Paint

    '    DrawRectangle = New Rectangle(rect1.Location, rect1.Size)

    'End Function

    'Private Sub RectangleShape_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) 'Handles RectangleShape1.Paint

    '    Dim rect As Microsoft.VisualBasic.PowerPacks.RectangleShape
    '    rect = sender

    '    Dim s As String = rect.AccessibleDescription
    '    'Dim f As Font = Me.Font

    '    Dim p As New Point(rect.Location.X + 3, rect.Location.Y + 5)

    '    Dim g As Graphics = e.Graphics

    '    'g.DrawString(s, f, Brushes.Black, p)


    '    Dim center_text_height As Double = 28
    '    Dim centertext As SolidBrush = New SolidBrush(rect.BorderColor) '(Color.Black)

    '    Dim center_rect As Rectangle = New Rectangle(rect.Location, rect.Size) 'gp.GetBounds()
    '    'new RectangleF(points[0].X, points[0].Y, center_text_width, center_text_height);
    '    Dim str_font As New Font("Helvetica Neue", center_text_height, FontStyle.Regular, GraphicsUnit.Pixel)

    '    Dim sf As New StringFormat()
    '    sf.Alignment = StringAlignment.Center
    '    sf.LineAlignment = StringAlignment.Center

    '    'Dim text As String = s 'e.Chart.Series("CYCLE").Points(0).YValues(0).ToString()
    '    'qtydone.Trim();
    '    Dim fits As Boolean = False
    '    Dim size As Integer = CInt(center_text_height)
    '    Do
    '        If str_font IsNot Nothing Then
    '            str_font.Dispose()
    '        End If

    '        str_font = New Font("Helvetica Neue", size, FontStyle.Regular, GraphicsUnit.Pixel)

    '        Dim stringSize As SizeF = g.MeasureString(s, str_font, CInt(center_rect.Width), sf)

    '        fits = (stringSize.Height < center_rect.Height)
    '        size -= 2
    '    Loop While Not fits

    '    g.DrawString(s, str_font, centertext, center_rect, sf)


    '    g.Dispose()

    'End Sub



    'Private Sub P_Dash_MouseMove(sender As Object, e As MouseEventArgs) Handles P_Dash.MouseMove
    '    If e.Button = MouseButtons.Left Then
    '        P_Dash.DoDragDrop("test", DragDropEffects.Move)
    '    End If
    'End Sub

    'Private Sub P_Dash_DragEnter(sender As Object, e As DragEventArgs) Handles P_Dash.DragEnter
    '    e.Effect = DragDropEffects.Move
    'End Sub

    'Private Sub P_Dash_DragOver(sender As Object, e As DragEventArgs) Handles P_Dash.DragOver
    '    Dim p As Point = P_Dash.PointToClient(New Point(e.X, e.Y))
    '    If p.Y < 16 Then
    '        scrollJump = -20
    '        If Not scrollTimer.Enabled Then
    '            scrollTimer.Start()
    '        End If
    '    ElseIf p.Y > P_Dash.ClientSize.Height - 16 Then
    '        scrollJump = 20
    '        If Not scrollTimer.Enabled Then
    '            scrollTimer.Start()
    '        End If
    '    Else
    '        If scrollTimer.Enabled Then
    '            scrollTimer.[Stop]()
    '        End If
    '    End If
    'End Sub

    'Private Sub P_Dash_QueryContinueDrag(sender As Object, e As QueryContinueDragEventArgs) Handles P_Dash.QueryContinueDrag
    '    If e.Action <> DragAction.[Continue] Then
    '        scrollTimer.[Stop]()
    '    End If
    'End Sub

    'Private Sub scrollTimer_Tick(sender As Object, e As EventArgs)
    '    'If P_Dash.ClientRectangle.Contains(P_Dash.PointToClient(MousePosition)) Then
    '    '    Dim p As Point = P_Dash.AutoScrollPosition
    '    '    P_Dash.AutoScrollPosition = New Point(-p.X, -p.Y + scrollJump)
    '    'Else
    '    '    scrollTimer.[Stop]()
    '    'End If

    '    Dim p As Point = DGV_LiveStatus.AutoScrollPosition
    '    Dim vs As VScrollProperties = DGV_LiveStatus.VerticalScroll
    '    If (-p.Y = vs.Maximum - vs.LargeChange + 1) Then
    '        'scrolled to the bottom
    '        'System.Console.WriteLine("reached bottom")
    '        DGV_LiveStatus.AutoScrollPosition = New Point(0, 0)
    '    Else
    '        DGV_LiveStatus.AutoScrollPosition = New Point(-p.X, -p.Y + scrollJump)
    '    End If

    '    'If (-p.Y >= P_Dash.VerticalScroll.Maximum) Then
    '    '    P_Dash.AutoScrollPosition = New Point(0, 0)
    '    'Else
    '    '    P_Dash.AutoScrollPosition = New Point(-p.X, -p.Y + scrollJump)
    '    'End If

    'End Sub

    Private Sub refreshTimer_Tick(sender As Object, e As EventArgs)
        GC.Collect()
        GetData()
    End Sub

    'Private Sub calcfontsize(ByRef graph As Graphics, rect As Rectangle)

    '    Dim center_text_height As Double = 35
    '    Dim centertext As SolidBrush = New SolidBrush(Color.Black)

    '    Dim center_rect As Rectangle = rect 'New Rectangle(100, 100, 100, 100) 'gp.GetBounds()
    '    'new RectangleF(points[0].X, points[0].Y, center_text_width, center_text_height);
    '    Dim str_font As New Font("Helvetica Neue", center_text_height, FontStyle.Regular, GraphicsUnit.Pixel)

    '    Dim sf As New StringFormat()
    '    sf.Alignment = StringAlignment.Center
    '    sf.LineAlignment = StringAlignment.Center

    '    Dim text As String = "text" 'e.Chart.Series("CYCLE").Points(0).YValues(0).ToString()
    '    'qtydone.Trim();
    '    Dim fits As Boolean = False
    '    Dim size As Integer = CInt(center_text_height)
    '    Do
    '        If str_font IsNot Nothing Then
    '            str_font.Dispose()
    '        End If

    '        str_font = New Font("Helvetica Neue", size, FontStyle.Regular, GraphicsUnit.Pixel)

    '        Dim stringSize As SizeF = graph.MeasureString(text, str_font, CInt(center_rect.Width), sf)

    '        fits = (stringSize.Height < center_rect.Height)
    '        size -= 2
    '    Loop While Not fits

    '    graph.DrawString(text, str_font, centertext, center_rect, sf)
    'End Sub

    'Private Sub P_Dash_MouseEnter(sender As Object, e As EventArgs)
    '    scrollTimer.Stop()
    '    refreshTimer.Stop()
    'End Sub

    'Private Sub P_Dash_MouseLeave(sender As Object, e As EventArgs)
    '    'If (TypeOf e Is MouseEventArgs) Then
    '    '    Dim mouse As MouseEventArgs = e
    '    '    'P_Dash.PointToClient(New Point(mouse.X, mouse.Y))
    '    '    If (mouse.X < P_Dash.Width And mouse.Y < P_Dash.Height) Then
    '    '        scrollTimer.Start()
    '    '        refreshTimer.Start()
    '    '    End If
    '    'End If

    '    'Dim clientPoint As Point = P_Dash.PointToClient(Cursor.Position)
    '    'If P_Dash.DisplayRectangle.Contains(clientPoint) Then
    '    'Else
    '    '    scrollTimer.Start()
    '    '    refreshTimer.Start()
    '    'End If

    '    'If (Cursor.Position.X < Location.X Or Cursor.Position.Y < Location.Y Or Cursor.Position.X > Location.X + Width - 1 Or Cursor.Position.Y > Location.Y + Height - 1) Then
    '    scrollTimer.Start()
    '    refreshTimer.Start()
    '    'End If

    '    'Dim pnl As Panel = CType(sender, Panel)
    '    'Dim rc As Rectangle = pnl.RectangleToScreen(pnl.ClientRectangle)
    '    'If Not rc.Contains(Cursor.Position) Then
    '    '    'pnl.Visible = False
    '    '    scrollTimer.Start()
    '    '    refreshTimer.Start()
    '    'Else
    '    '    'Debug.Print("MouseLeave() but cursor still within panel bounds")
    '    'End If
    'End Sub

    ''Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
    ''    Dim clientPoint As Point = PointToClient(Cursor.Position)
    ''    If Me.DisplayRectangle.Contains(clientPoint) Then
    ''        Return
    ''    End If
    ''    MyBase.OnMouseLeave(e)
    ''End Sub


    'Private Sub P_Dash_MouseHover(sender As Object, e As EventArgs)
    '    scrollTimer.Stop()
    '    refreshTimer.Stop()
    'End Sub

    Private Sub DGV_LiveStatus_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DGV_LiveStatus.CellPainting
        '''''''''''''''''''''''headers
        'Dim c As Color = If((e.State = (System.Windows.Forms.DataGridViewElementStates.Displayed Or System.Windows.Forms.DataGridViewElementStates.Selected Or System.Windows.Forms.DataGridViewElementStates.Visible)), e.CellStyle.SelectionBackColor, e.CellStyle.BackColor)

        'Dim br As New System.Drawing.Drawing2D.LinearGradientBrush(e.CellBounds, c, System.Windows.Forms.ControlPaint.LightLight(c), 360)
        'e.Graphics.FillRectangle(br, e.CellBounds)
        'e.Paint(e.CellBounds, System.Windows.Forms.DataGridViewPaintParts.Border Or System.Windows.Forms.DataGridViewPaintParts.ContentForeground)
        '''''''''''''''''''''''''''

        'Dim br2 As SolidBrush = New SolidBrush(Color.Red)

        'If (e.RowIndex Mod 2 = 0) Then
        '    If (e.ColumnIndex Mod 2 = 0) Then
        '        e.Graphics.FillRectangle(br2, e.CellBounds)
        '        e.Paint(e.CellBounds, System.Windows.Forms.DataGridViewPaintParts.Border Or System.Windows.Forms.DataGridViewPaintParts.ContentForeground)
        '    End If
        'End If

        'Dim br2 As New SolidBrush(backcolor)
        'e.Graphics.FillRectangle(br2, e.CellBounds)
        e.Paint(e.CellBounds, System.Windows.Forms.DataGridViewPaintParts.Border Or System.Windows.Forms.DataGridViewPaintParts.ContentForeground)


        'e.Handled = True
        'br.Dispose()
        ''''''''''''''' from rect paint


        '    Dim rect As Microsoft.VisualBasic.PowerPacks.RectangleShape
        '    rect = sender

        '    Dim s As String = rect.AccessibleDescription
        '    'Dim f As Font = Me.Font

        '    Dim p As New Point(rect.Location.X + 3, rect.Location.Y + 5)

        '    Dim g As Graphics = e.Graphics

        '    'g.DrawString(s, f, Brushes.Black, p)


        '    Dim center_text_height As Double = 28
        '    Dim centertext As SolidBrush = New SolidBrush(rect.BorderColor) '(Color.Black)

        '    Dim center_rect As Rectangle = New Rectangle(rect.Location, rect.Size) 'gp.GetBounds()
        '    'new RectangleF(points[0].X, points[0].Y, center_text_width, center_text_height);
        '    Dim str_font As New Font("Helvetica Neue", center_text_height, FontStyle.Regular, GraphicsUnit.Pixel)

        '    Dim sf As New StringFormat()
        '    sf.Alignment = StringAlignment.Center
        '    sf.LineAlignment = StringAlignment.Center

        '    'Dim text As String = s 'e.Chart.Series("CYCLE").Points(0).YValues(0).ToString()
        '    'qtydone.Trim();
        '    Dim fits As Boolean = False
        '    Dim size As Integer = CInt(center_text_height)
        '    Do
        '        If str_font IsNot Nothing Then
        '            str_font.Dispose()
        '        End If

        '        str_font = New Font("Helvetica Neue", size, FontStyle.Regular, GraphicsUnit.Pixel)

        '        Dim stringSize As SizeF = g.MeasureString(s, str_font, CInt(center_rect.Width), sf)

        '        fits = (stringSize.Height < center_rect.Height)
        '        size -= 2
        '    Loop While Not fits

        '    g.DrawString(s, str_font, centertext, center_rect, sf)


        '    g.Dispose()

    End Sub

    Private Sub DGV_LiveStatus_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGV_LiveStatus.CellFormatting
        'e.Value = DataGridViewImageCell

        Dim bordercolor As Color, backcolor As Color
        bordercolor = Color.CadetBlue
        backcolor = Color.White
        'content
        If (e.RowIndex >= 0 And e.ColumnIndex = 1) Then


            Dim row As DataGridViewRow
            row = DGV_LiveStatus.Rows(e.RowIndex)
            Dim status As String = row.Cells("Status").Value

            'Draw table
            Dim alpha As Integer = 255

            If (colors.ContainsKey(status)) Then
                bordercolor = Color.Black
                backcolor = Color.FromArgb(colors(status))
                backcolor = Color.FromArgb(alpha, backcolor.R, backcolor.G, backcolor.B) 'gives alpha else it is transparent
            ElseIf (status = "CYCLE ON") Then
                bordercolor = Color.Black
                backcolor = Color.Green
            ElseIf (status = "CYCLE OFF") Then
                bordercolor = Color.Black
                backcolor = Color.Red
            ElseIf (status = "SETUP") Then
                bordercolor = Color.White
                backcolor = Color.Blue
            Else
                bordercolor = Color.Red
                backcolor = Color.White
            End If

            'DGV_LiveStatus(e.ColumnIndex, e.RowIndex).Style.BackColor = backcolor


        End If

        'change only status column
        If (e.ColumnIndex = 1) Then
            e.CellStyle.BackColor = backcolor
            e.CellStyle.ForeColor = bordercolor
        End If

    End Sub

    Private Sub DGV_LiveStatus_SelectionChanged(sender As Object, e As EventArgs) Handles DGV_LiveStatus.SelectionChanged
        DGV_LiveStatus.ClearSelection()
    End Sub


End Class
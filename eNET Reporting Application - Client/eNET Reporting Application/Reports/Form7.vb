Imports CSI_Library.CSI_Library
Imports System.Windows.Forms
Imports System.Drawing.Printing

Public Class Form7

    Public CSI_LIB As New CSI_Library.CSI_Library
    Public Timeline As New DataTable
    Public Timeline2 As New DataTable
    Public Timeline3 As New DataTable
    Public colors As New Dictionary(Of String, String)

    Private Sub form7_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        If Form_Shift_1.Visible = True Then Form_Shift_1.Close()
        If Form_shift_2.Visible = True Then Form_shift_2.Close()
        If Form_Shift_3.Visible = True Then Form_Shift_3.Close()
        If Form_Shift.Visible = True Then Form_Shift.Close()
    End Sub
    '-----------------------------------------------------------------------------------------------------------------------
    ' Form7 Load
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Form7_Loaded(sender As Object, e As EventArgs) Handles MyBase.Shown
        DataGridView1.ClearSelection()
        DataGridView2.ClearSelection()
        DataGridView3.ClearSelection()

        Me.ResumeLayout()

    End Sub
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SuspendLayout()
        Try
            Dim data1(7) As Integer
            Dim data2(7) As Integer
            Dim data3(7) As Integer

            data1(0) = Val(Label28.Text)
            data1(1) = Val(Label27.Text)
            data1(2) = Val(Label26.Text)
            data1(3) = Val(Label25.Text) ' + Val(Label24.Text) + Val(Label23.Text) + Val(Label22.Text)

            Chart1.Series("Series1").Points.DataBindY(data1)

            data2(0) = Val(Label39.Text)
            data2(1) = Val(Label38.Text)
            data2(2) = Val(Label37.Text)
            data2(3) = Val(Label36.Text) '+ Val(Label35.Text) + Val(Label34.Text) + Val(Label33.Text)

            Chart2.Series("Series1").Points.DataBindY(data2)

            data3(0) = Val(Label67.Text)
            data3(1) = Val(Label66.Text)
            data3(2) = Val(Label65.Text)
            data3(3) = Val(Label64.Text) ' + Val(Label63.Text) + Val(Label62.Text) + Val(Label61.Text)

            Chart3.Series("Series1").Points.DataBindY(data3)

            Me.MdiParent = Form1


            Button3.Visible = True
            Label29.Visible = True


            Me.Location = New Point(285, 25)
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            Me.BackColor = Color.Transparent
            SetStyle(ControlStyles.DoubleBuffer, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)




            ' Me.StartPosition = FormStartPosition.Manual


            '  Me.Location = Me.PointToClient(New Point(0.0))

        Catch ex As Exception
            MessageBox.Show("CSIFLEX has encoutred an error while loading the page : " & ex.Message)
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

        DataGridView1.DefaultCellStyle.SelectionBackColor = SystemColors.ControlDark
        DataGridView1.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText
        DataGridView2.DefaultCellStyle.SelectionBackColor = SystemColors.ControlDark
        DataGridView2.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText
        DataGridView3.DefaultCellStyle.SelectionBackColor = SystemColors.ControlDark
        DataGridView3.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText



        Try
            colors = CSI_LIB.colors(Form1.chemin_eNET)
        Catch ex As Exception
            MessageBox.Show("CSIFLEX has encoutred an error while loading the machine color status colors from eNET : " & ex.Message)
        End Try
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
            Form1.SuspendLayout()
            Me.Left = Me.Left + (e.X - _MouseX)
            Me.Top = Me.Top + (e.Y - _MouseY)
            If Me.Top < 20 Then Me.Top = 50
            '  If Me.Left < 20 Then Me.Left = 0
            Form1.ResumeLayout(True)

        End If

    End Sub
#End Region

#Region "Return prev next combobox"
    '-----------------------------------------------------------------------------------------------------------------------
    ' Combo change
    '---------------------------------------------------------- -------------------------------------------------------------
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim Text As String()
        Dim i As Integer
        Text = Split(ComboBox1.Text, " : ")
        Dim machine As String
        Dim date_ As String
        Dim data1(7) As Integer
        Dim data2(7) As Integer
        Dim data3(7) As Integer

        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        DataGridView3.Rows.Clear()
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()

        For i = 0 To UBound(Form3.periode_returned) - 1
            machine = Form3.periode_returned(i).machine_name
            date_ = Form3.periode_returned(i).date_
            If machine = Text(0) And date_ = Text(1) Then
                Call Form3.fill_form_7(Form3.periode_returned(i))
                GoTo good
            End If
        Next
good:
        data1(0) = Val(Label28.Text)
        data1(1) = Val(Label27.Text)
        data1(2) = Val(Label26.Text)
        data1(3) = Val(Label25.Text) '+ Val(Label24.Text) + Val(Label23.Text) + Val(Label22.Text)

        Chart1.Series("Series1").Points.DataBindY(data1)

        data2(0) = Val(Label39.Text)
        data2(1) = Val(Label38.Text)
        data2(2) = Val(Label37.Text)
        data2(3) = Val(Label36.Text) ' + Val(Label35.Text) + Val(Label34.Text) + Val(Label33.Text)

        Chart2.Series("Series1").Points.DataBindY(data2)

        data3(0) = Val(Label67.Text)
        data3(1) = Val(Label66.Text)
        data3(2) = Val(Label65.Text)
        data3(3) = Val(Label64.Text) ' + Val(Label63.Text) + Val(Label62.Text) + Val(Label61.Text)

        Chart3.Series("Series1").Points.DataBindY(data3)
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' NEXT
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim index_ As Integer
        index_ = ComboBox1.SelectedIndex
        If index_ = ComboBox1.Items.Count - 1 Then index_ = -1
        ComboBox1.SelectedIndex = index_ + 1
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' prev
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim index_ As Integer
        index_ = ComboBox1.SelectedIndex
        If index_ = 0 Then index_ = ComboBox1.Items.Count
        ComboBox1.SelectedIndex = index_ - 1
    End Sub

    '-----------------------------------------------------------------------------------------------------------------------
    ' return
    '-----------------------------------------------------------------------------------------------------------------------
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'If Form3.CheckBox1.Checked = True Then
        'Else
        Form9.Visible = True
        Form9.MdiParent = Form1
        Form9.Location = New System.Drawing.Point(268, 25)

        Me.Close()
        '   End If

    End Sub

#End Region

#Region "Clic On graph"



    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click
        Dim date_ As Date, machine_ As String, Combovalue As String()
        Try
            If Not ComboBox1.SelectedText.StartsWith("consolidated") Then
                If Val(Label25.Text) + Val(Label26.Text) + Val(Label27.Text) + Val(Label28.Text) = 0 Then
                    MessageBox.Show("No data available for this shift")
                Else
                    Combovalue = Split(ComboBox1.Text, " : ")
                    machine_ = Combovalue(0)
                    If UBound(Combovalue(1).Split(" ")) > 2 Then
                        MessageBox.Show("The Timeline is available for a periode of 1 day")
                    Else


                        date_ = Combovalue(1)

                        Timeline = CSI_LIB.TimeLine(date_, machine_, 1)

                        Dim formshift1 As New Form_Shift
                        formshift1.shiftno = 1

                        formshift1.TopMost = True
                        formshift1.Show()

                        Dim s As Screen
                        s = Screen.FromControl(Me)
                        formshift1.Location = Form1.Location + New Point(8 + Me.Location.X + GroupBox2.Location.X + DataGridView1.Location.X + Panel2.Location.X, 56 + Me.Location.Y + GroupBox2.Location.Y + DataGridView1.Location.Y + Panel2.Location.Y)

                        'Form_Shift_1.TopMost = True
                        'Form_Shift_1.Show()

                        'Dim s As Screen
                        's = Screen.FromControl(Me)
                        'Form_Shift_1.Location = Form1.Location + New Point(8 + Me.Location.X + GroupBox2.Location.X + DataGridView1.Location.X + Panel2.Location.X, 56 + Me.Location.Y + GroupBox2.Location.Y + DataGridView1.Location.Y + Panel2.Location.Y)
                    End If
                    ' = System.Windows.Forms.FormStartPosition(Me.Location.X + 219, 96)
                End If
            Else
                MessageBox.Show("No timeLine available for consolidated data, return to the previous window, and unbind to see a timeline (On a periode of one shift, on one day)")
            End If

        Catch ex As Exception
            MessageBox.Show("CSIFLEX cannot display the Timeline : " & ex.Message)
        End Try

    End Sub

    Private Sub Chart2_Click(sender As Object, e As EventArgs) Handles Chart2.Click
        Dim date_ As Date, machine_ As String, Combovalue As String()
        Try
            If Not ComboBox1.SelectedText.StartsWith("consolidated") Then
                If Val(Label36.Text) + Val(Label37.Text) + Val(Label38.Text) + Val(Label39.Text) = 0 Then
                    MessageBox.Show("No data available for this shift")
                Else
                    Combovalue = Split(ComboBox1.Text, " : ")


                    machine_ = Combovalue(0)


                    If UBound(Combovalue(1).Split(" ")) > 2 Then
                        MessageBox.Show("The Timeline is available for a periode of 1 day")
                    Else
                        date_ = Combovalue(1)
                        Timeline2 = CSI_LIB.TimeLine(date_, machine_, 2)

                        Dim formshift2 As New Form_Shift
                        formshift2.shiftno = 2

                        formshift2.TopMost = True
                        formshift2.Show()
                        Dim s As Screen
                        s = Screen.FromControl(Me)
                        formshift2.Location = Form1.Location + New Point(8 + Me.Location.X + GroupBox2.Location.X + DataGridView2.Location.X + Panel3.Location.X, 56 + Me.Location.Y + GroupBox2.Location.Y + DataGridView2.Location.Y + Panel3.Location.Y)

                        'Form_shift_2.TopMost = True
                        'Form_shift_2.Show()
                        'Dim s As Screen
                        's = Screen.FromControl(Me)
                        'Form_shift_2.Location = Form1.Location + New Point(8 + Me.Location.X + GroupBox2.Location.X + DataGridView2.Location.X + Panel3.Location.X, 56 + Me.Location.Y + GroupBox2.Location.Y + DataGridView2.Location.Y + Panel3.Location.Y)
                    End If
                End If
            Else
                MessageBox.Show("No timeLine available for consolidated data, return to the previous window, and unbind to see a timeline (On a periode of one shift, on one day)")
            End If
        Catch ex As Exception
            MessageBox.Show("CSIFLEX cannot display the Timeline : " & ex.Message)
        End Try
    End Sub

    Private Sub Chart3_Click(sender As Object, e As EventArgs) Handles Chart3.Click
        Dim date_ As Date, machine_ As String, combovalue As String()
        Try
            If Not ComboBox1.SelectedText.StartsWith("Consolidated") Then
                If Val(Label64.Text) + Val(Label65.Text) + Val(Label66.Text) + Val(Label67.Text) = 0 Then
                    MessageBox.Show("No data available for this shift")
                Else
                    combovalue = Split(ComboBox1.Text, " : ")


                    machine_ = combovalue(0)

                    If UBound(combovalue(1).Split(" ")) > 2 Then
                        MessageBox.Show("The Timeline is available for a periode of 1 day")
                    Else
                        date_ = combovalue(1)
                        Timeline3 = CSI_LIB.TimeLine(date_, machine_, 3)
                        'Timeline3 = CSI_LIB.TimeLine_from_MON(date_, machine_, 3, Form1.chemin_eNET)

                        Dim formshift3 As New Form_Shift
                        formshift3.shiftno = 3

                        formshift3.TopMost = True
                        formshift3.Show()

                        Dim s As Screen
                        s = Screen.FromControl(Me)
                        formshift3.Location = Form1.Location + New Point(8 + Me.Location.X + GroupBox2.Location.X + DataGridView3.Location.X + Panel4.Location.X, 56 + Me.Location.Y + GroupBox2.Location.Y + DataGridView3.Location.Y + Panel4.Location.Y)


                        'Form_Shift_3.TopMost = True
                        'Form_Shift_3.Show()
                        ''Form_Shift_3_bis.Show()
                        'Dim s As Screen
                        's = Screen.FromControl(Me)
                        'Form_Shift_3.Location = Form1.Location + New Point(8 + Me.Location.X + GroupBox2.Location.X + DataGridView3.Location.X + Panel4.Location.X, 56 + Me.Location.Y + GroupBox2.Location.Y + DataGridView3.Location.Y + Panel4.Location.Y)

                    End If
                End If
            Else
                MessageBox.Show("No timeLine available for consolidated data, return to the previous window, and unbind to see a timeline (On a periode of one shift, on one day)")
            End If
        Catch ex As Exception
            MessageBox.Show("CSIFLEX cannot display the Timeline : " & ex.Message)
        End Try
    End Sub
#End Region


    Dim WithEvents mPrintDocument As New PrintDocument
    Dim mPrintBitMap As Bitmap


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
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

    End Sub
End Class
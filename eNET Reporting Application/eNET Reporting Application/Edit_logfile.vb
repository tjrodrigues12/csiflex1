Imports System.IO





Public Class Edit_logfile
    Public Lastline As String = ""
    Public Parsed As String()
    Public Table_ As New DataTable
    Public Headline As String = ""
    Public separatorArray As String()
    Public tbl As New DataTable




    Private Sub BT_BROWSE_Click(sender As Object, e As EventArgs) Handles BT_BROWSE.Click

        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()


        Try

            openFileDialog1.Filter = "All files (*.*)|*.*"
            openFileDialog1.RestoreDirectory = True

            If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                myStream = openFileDialog1.OpenFile()
                TB_PATH.Text = openFileDialog1.FileName
            End If
        Catch Ex As Exception
            MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
        Finally
            ' Check this again, since we need to make sure we didn't throw an exception on open. 
            If (myStream IsNot Nothing) Then
                myStream.Close()
            End If
        End Try



    End Sub

    Function Myfunction() As String()
        Dim myArray_(-1) As String

        If DataGridView2.SelectedRows.Count <> 0 Then
            Dim row_ As DataGridViewRow = DataGridView2.SelectedRows.Item(0)
            Dim Nbitems_ As Integer

            Nbitems_ = row_.Cells.Count

            For i As Integer = 0 To Nbitems_
                DataGridView1.Columns.Add(i, Nothing)
            Next
            Dim list_ As New List(Of String)

            For Each cell As DataGridViewCell In row_.Cells
                If Not IsDBNull(cell.Value) Then list_.Add(cell.Value)
            Next
            myArray_ = list_.ToArray()


        End If

        Return myArray_

    End Function


    Function stockseparator()
        Dim separator As New List(Of String)()
        Dim newseparator As String
        If CHK_TAB.Checked Then
            newseparator = "    "
            'Add tab as separator
            separator.Add(newseparator)
        End If
        If CHK_COMA.Checked Then
            newseparator = ","
            'Add coma as separator
            separator.Add(newseparator)
        End If
        If CHK_SEMICOL.Checked Then
            newseparator = ";"
            'Add semicolon as separator
            separator.Add(newseparator)
        End If
        If CHK_SPACE.Checked Then
            newseparator = " "
            'Add space as separator
            separator.Add(newseparator)
        End If
        If CHK_OTHER.Checked Then
            newseparator = TextBox2.Text
            'Add other as separator
            separator.Add(newseparator)
        End If
        If CHK_MERGEDEL.Checked Then
            newseparator = ";"
            'Add Merge Delimiters as separator
            separator.Add(newseparator)
        End If
        separatorArray = separator.ToArray()

        Return separatorArray
    End Function

    Private Sub BT_OK_Click(sender As Object, e As EventArgs) Handles BT_OK.Click
        DataGridView1.DataSource = Nothing
        DataGridView2.DataSource = Nothing
        Dim tfp As New FileIO.TextFieldParser(TB_PATH.Text)

        ' Tell the TextFieldParse to expect a Delimited file.
        tfp.TextFieldType = FileIO.FieldType.Delimited

        Dim first As Boolean


        ' Tell our TextFieldParser what the parameters are.
        tfp.Delimiters = stockseparator()

        If Verification() Then

            Try
                ' Have we read to the end of the file?
                While tfp.EndOfData = False
                    If first = False Then
                        Dim lines As String() = tfp.ReadFields

                        If UBound(lines) > tbl.Columns.Count - 1 Then
                            For i As Integer = tbl.Columns.Count To UBound(lines)
                                tbl.Columns.Add(i)
                            Next
                        End If
                        tbl.Rows.Add(lines)
                    End If
                    ' Get the fields of the current line, it automatically moves to the next position.
                    If first = True Then

                        Dim lines As String() = tfp.ReadFields
                        For i As Integer = 0 To UBound(lines)
                            tbl.Columns.Add(i)
                        Next
                        first = False

                        tbl.Rows.Add(lines)

                    End If

                End While
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            ' Remember to close the parser.
            tfp.Close()

            DataGridView2.DataSource = tbl

            Parsed = Myfunction()
            Headlines_(Parsed)

        End If
    End Sub

    Private Sub Headlines_(Tabparse As String())
        If Not (Parsed Is Nothing) Then

            DataGridView1.Columns.Add("1", Nothing)
            For i As Integer = 0 To UBound(Parsed)
                DataGridView1.Columns.Add(i, Nothing)
            Next
            DataGridView1.Rows.Add()
            DataGridView1.Rows.Add()
            For i As Integer = 0 To UBound(Parsed)
                Dim dgvcc As New DataGridViewComboBoxCell

                dgvcc.Items.Add("Machine Name")
                dgvcc.Items.Add("Date")
                dgvcc.Items.Add("Shift")
                dgvcc.Items.Add("Start time")
                dgvcc.Items.Add("End time")
                dgvcc.Items.Add("Elapsed time")
                dgvcc.Items.Add("Machine Status")
                dgvcc.Items.Add("Head Pallet")
                dgvcc.Items.Add("Comments")
                DataGridView1.Rows(0).Cells(i) = dgvcc
                DataGridView1.Rows(1).Cells(i) = New DataGridViewTextBoxCell
                DataGridView1.Rows(1).Cells(i).Value = Parsed(i)
            Next

        End If




    End Sub


    Function Verification() As Boolean
        Dim Good As Boolean = False
        If CHK_TAB.Checked = False And _
            CHK_COMA.Checked = False And _
            CHK_SEMICOL.Checked = False And _
            CHK_SPACE.Checked = False And _
            CHK_OTHER.Checked = False And _
            CHK_MERGEDEL.Checked = False And _
            Good = False Then
            MessageBox.Show("No separator selected ")
        Else : Good = True
        End If
        Return Good
    End Function

    Function Parse_(lastline As String) As String()

        Dim words As String()

        separatorArray = stockseparator()
        words = lastline.Split(separatorArray, StringSplitOptions.None)
        Return words

    End Function
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles BT_CANCEL.Click
        Me.Close()
    End Sub
    

    'Private Sub RB_FIXED_CheckedChanged(sender As Object, e As EventArgs) Handles RB_FIXED.CheckedChanged
    '    If RB_FIXED.Checked Then
    '        RB_SEPARATED.Enabled = False
    '        CHK_TAB.Enabled = False
    '        CHK_COMA.Enabled = False
    '        CHK_SEMICOL.Enabled = False
    '        CHK_SPACE.Enabled = False
    '        CHK_OTHER.Enabled = False
    '        CHK_MERGEDEL.Enabled = False
    '    End If
    'End Sub


    'Private Sub RB_SEPARATED_CheckedChanged(sender As Object, e As EventArgs) Handles RB_SEPARATED.CheckedChanged
    '    If RB_SEPARATED.Checked Then
    '        RB_FIXED.Enabled = False
    '    End If
    'End Sub





    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        ' Define a new TextFieldParser, passing it our file

        Dim tfp As New FileIO.TextFieldParser(TB_PATH.Text)



        Dim first As Boolean = True

        ' Tell the TextFieldParse to expect a Delimited file.
        tfp.TextFieldType = FileIO.FieldType.Delimited

        DataGridView1.Columns.Add("1", Nothing)
        For i As Integer = 0 To UBound(Parsed)
            DataGridView1.Columns.Add(i, Nothing)
        Next
        DataGridView1.Columns.Add("1", Nothing)
        For i As Integer = 0 To UBound(Parsed)
            DataGridView1.Columns.Add(i, Nothing)
        Next

        ' Tell our TextFieldParser what the parameters are.
        tfp.Delimiters = separatorArray

        ' Read the file in Try-Catch Block in case of file problems


        'Look for the index of Machine Status
        Dim dgvcc As DataGridViewComboBoxCell
        Dim index_ As Integer = 0

        For i = 0 To UBound(Parsed)
            dgvcc = DataGridView1.Rows(0).Cells(i)
            If dgvcc.Value = "Machine Status" Then
                index_ = i
            End If
        Next

        Dim view As New DataView(tbl)
        Dim tbldistinct As New DataTable
        tbldistinct = view.ToTable(True, tbl.Columns.Item(index_).ColumnName)

        For i = 0 To tbldistinct.Rows.Count - 1
            CB_CYCLEON.Items.Add(tbldistinct.Rows(i).Item(0).ToString())
        Next

    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As EventArgs) Handles DataGridView2.SelectionChanged, DataGridView2.CellContentClick
        Parsed = Myfunction()
        Headlines_(Parsed)
    End Sub


    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click

        ' Define a new TextFieldParser, passing it our file

        Dim tfp As New FileIO.TextFieldParser(TB_PATH.Text)



        Dim first As Boolean = True

        ' Tell the TextFieldParse to expect a Delimited file.
        tfp.TextFieldType = FileIO.FieldType.Delimited

        DataGridView1.Columns.Add("1", Nothing)
        For i As Integer = 0 To UBound(Parsed)
            DataGridView1.Columns.Add(i, Nothing)
        Next
        DataGridView1.Columns.Add("1", Nothing)
        For i As Integer = 0 To UBound(Parsed)
            DataGridView1.Columns.Add(i, Nothing)
        Next

        ' Tell our TextFieldParser what the parameters are.
        tfp.Delimiters = separatorArray

        ' Read the file in Try-Catch Block in case of file problems


        'Look for the index of Machine Status
        Dim dgvcc As DataGridViewComboBoxCell
        Dim index_ As Integer = 0

        For i = 0 To UBound(Parsed)
            dgvcc = DataGridView1.Rows(0).Cells(i)
            If dgvcc.Value = "Machine Status" Then
                index_ = i
            End If
        Next

        Dim view As New DataView(tbl)
        Dim tbldistinct As New DataTable
        tbldistinct = view.ToTable(True, tbl.Columns.Item(index_).ColumnName)

        For i = 0 To tbldistinct.Rows.Count - 1
            ComboBox1.Items.Add(tbldistinct.Rows(i).Item(0).ToString())
        Next
    End Sub

    Private Sub Edit_logfile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
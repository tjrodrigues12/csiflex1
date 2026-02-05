Imports System.Windows.Forms.DataVisualization.Charting


Public Class Form10


    Public data(4) As Integer
    Public data2(1) As Integer
    Public data3(1) As Integer
    Public data4(1) As Integer
    Public data5(1) As Integer
    Public maxZoom As Integer
    Dim dataSHIFT1(24) As Integer
    Dim dataSHIFT2(24) As Integer
    Dim dataSHIFT3(24) As Integer



    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Label53.Text = "Shift 3"
        data(0) = Val(Label41.Text)
        data(1) = Val(Label40.Text)
        data(2) = Val(Label39.Text)
        data(3) = Val(Label38.Text) + Val(Label37.Text) + Val(Label36.Text) + Val(Label32.Text)

        Chart1.Series("Series1").Points.DataBindY(data)



        data2(0) = Val(Label28.Text)
        data3(0) = Val(Label27.Text)
        data4(0) = Val(Label26.Text)
        data5(0) = Val(Label25.Text) + Val(Label24.Text) + Val(Label23.Text) + Val(Label22.Text)

        Chart2.Series("Series1").Points.DataBindY(data2)
        Chart2.Series("Series2").Points.DataBindY(data3)
        Chart2.Series("Series3").Points.DataBindY(data4)
        Chart2.Series("Series4").Points.DataBindY(data5)

        data2(0) = Val(Label51.Text)
        data3(0) = Val(Label50.Text)
        data4(0) = Val(Label49.Text)
        data5(0) = Val(Label48.Text) + Val(Label47.Text) + Val(Label46.Text) + Val(Label45.Text)

        Chart3.Series("Series1").Points.DataBindY(data2)
        Chart3.Series("Series2").Points.DataBindY(data3)
        Chart3.Series("Series3").Points.DataBindY(data4)
        Chart3.Series("Series4").Points.DataBindY(data5)

        data2(0) = Val(Label63.Text)
        data3(0) = Val(Label62.Text)
        data4(0) = Val(Label61.Text)
        data5(0) = Val(Label60.Text) + Val(Label59.Text) + Val(Label56.Text) + Val(Label55.Text)

        Chart4.Series("Series1").Points.DataBindY(data2)
        Chart4.Series("Series2").Points.DataBindY(data3)
        Chart4.Series("Series3").Points.DataBindY(data4)
        Chart4.Series("Series4").Points.DataBindY(data5)
        Me.Left = 375
        Me.Top = 50
        Me.MdiParent = Form1



    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim Text As String() = Split(ComboBox1.Text, " - ")
        Dim i As Integer
        Dim str As String
        Dim machine As String
        Dim date_ As String

        For i = 0 To UBound(Form3.general_array, 3) - 1
            machine = Form3.general_array(0, 0, i)
            date_ = Form3.general_array(1, 0, i)
            If machine = Text(0) And date_ = Text(1) Then Call Form3.fill_form10_percent(Form3.general_array, i)
        Next



        data(0) = Val(label41.Text)
        data(1) = Val(Label40.Text)
        data(2) = Val(Label39.Text)
        data(3) = Val(Label38.Text) + Val(Label37.Text) + Val(Label36.Text) + Val(Label32.Text)

        Chart1.Series("Series1").Points.DataBindY(data)


        data2(0) = Val(Label28.Text)
        data3(0) = Val(Label27.Text)
        data4(0) = Val(Label26.Text)
        data5(0) = Val(Label25.Text) + Val(Label24.Text) + Val(Label23.Text) + Val(Label22.Text)

        Chart2.Series("Series1").Points.DataBindY(data2)
        Chart2.Series("Series2").Points.DataBindY(data3)
        Chart2.Series("Series3").Points.DataBindY(data4)
        Chart2.Series("Series4").Points.DataBindY(data5)

        data2(0) = Val(Label51.Text)
        data3(0) = Val(Label50.Text)
        data4(0) = Val(Label49.Text)
        data5(0) = Val(Label48.Text) + Val(Label47.Text) + Val(Label46.Text) + Val(Label45.Text)

        Chart3.Series("Series1").Points.DataBindY(data2)
        Chart3.Series("Series2").Points.DataBindY(data3)
        Chart3.Series("Series3").Points.DataBindY(data4)
        Chart3.Series("Series4").Points.DataBindY(data5)

        data2(0) = Val(Label63.Text)
        data3(0) = Val(Label62.Text)
        data4(0) = Val(Label61.Text)
        data5(0) = Val(Label60.Text) + Val(Label59.Text) + Val(Label56.Text) + Val(Label55.Text)

        Chart4.Series("Series1").Points.DataBindY(data2)
        Chart4.Series("Series2").Points.DataBindY(data3)
        Chart4.Series("Series3").Points.DataBindY(data4)
        Chart4.Series("Series4").Points.DataBindY(data5)


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim index_ As Integer
        index_ = ComboBox1.SelectedIndex
        If index_ = 0 Then index_ = ComboBox1.Items.Count
        ComboBox1.SelectedIndex = index_ - 1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim index_ As Integer
        index_ = ComboBox1.SelectedIndex
        If index_ = ComboBox1.Items.Count - 1 Then index_ = -1
        ComboBox1.SelectedIndex = index_ + 1
    End Sub

  
   

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Chart2.Series("Series1").ChartType = SeriesChartType.Pie
        Chart3.Series("Series1").ChartType = SeriesChartType.Pie
        Chart4.Series("Series1").ChartType = SeriesChartType.Pie
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs)
        Chart2.Series("Series1").ChartType = SeriesChartType.Column
        Chart3.Series("Series1").ChartType = SeriesChartType.Column
        Chart4.Series("Series1").ChartType = SeriesChartType.Column
    End Sub


End Class
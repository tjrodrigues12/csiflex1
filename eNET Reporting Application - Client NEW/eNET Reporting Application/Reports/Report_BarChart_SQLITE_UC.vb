'Charts
Imports System.Windows.Forms.DataVisualization.Charting

'Streams and file
Imports System.IO.File
Imports System.IO

Public Class Report_BarChart_SQLITE_UC

    Private _machines As String()
    Private _consolidated As Boolean
    Private _report_data As DataTable
    Private rootPath As String = CSI_Library.CSI_Library.ClientRootPath



    Public Sub New(machines As String())
        InitializeComponent()

        Me.Location = New System.Drawing.Point(Config_report.Location.X + Config_report.Size.Width, 25)

        If (machines.Count > 0) Then
            _machines = machines
            LBL_MachineName.Text = _machines(0)

            CB_Report.SelectedIndex = CB_Report.Items.Count - 1

        Else
            MessageBox.Show("The list of machine is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
    Private Sub Report_BarChart_SQLITE_UC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Sub SetColor()
        Dim alpha As Integer = 255
        Dim backcolors As Color
        Dim backcolors2 As Color
        Dim backcolors3 As Color

        Dim colors As Dictionary(Of String, Integer)
        colors = Dashboard.GetEnetGraphColor(Reporting_application.chemin_Color)


        Try

            backcolors = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE ON"))
            backcolors = Color.FromArgb(alpha, backcolors.R, backcolors.G, backcolors.B)
            Label1.BackColor = backcolors
            LBL_CycleOn_Period.BackColor = backcolors
            LBL_CycleOn_Shift1.BackColor = backcolors
            LBL_CycleOn_Shift2.BackColor = backcolors
            LBL_CycleOn_Shift3.BackColor = backcolors

            backcolors2 = System.Drawing.ColorTranslator.FromWin32(colors("CYCLE OFF"))
            backcolors2 = Color.FromArgb(alpha, backcolors2.R, backcolors2.G, backcolors2.B)
            Label2.BackColor = backcolors2
            LBL_CycleOff_Period.BackColor = backcolors2
            LBL_CycleOff_Shift1.BackColor = backcolors2
            LBL_CycleOff_Shift2.BackColor = backcolors2
            LBL_CycleOff_Shift3.BackColor = backcolors2

            backcolors3 = System.Drawing.ColorTranslator.FromWin32(colors("SETUP"))
            backcolors3 = Color.FromArgb(alpha, backcolors3.R, backcolors3.G, backcolors3.B)
            Label3.BackColor = backcolors3
            LBL_Setup_Period.BackColor = backcolors3
            LBL_Setup_Shift1.BackColor = backcolors3
            LBL_Setup_Shift2.BackColor = backcolors3
            LBL_Setup_Shift3.BackColor = backcolors3

        Catch ex As Exception
            System.Console.WriteLine(ex.Message)
        End Try

    End Sub
    Sub DisplayTargetLine()

        Dim targetvalue As Integer
        Dim targetcolor As Integer

        If Exists(rootPath & "\sys\targetColor_.csys") Then
            Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\targetColor_.csys")
                targetcolor = CInt(reader.ReadLine())
                reader.Close()
            End Using
        End If

        If Exists(rootPath & "\sys\target_.csys") Then
            Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\target_.csys")
                targetvalue = CInt(reader.ReadLine()) 'this line will call NumericUpDownValueChanged
                reader.Close()
            End Using
        End If

        If (targetvalue > 0) Then
            Call target(targetvalue, targetcolor)
        End If

    End Sub

    Sub target(targetvalue As Integer, targetColor As Integer)
        Try


            Chart2.ChartAreas(0).AxisY.StripLines.Clear()
            Dim stripLineTarget As StripLine = New StripLine()
            stripLineTarget.StripWidth = 0
            stripLineTarget.BorderColor = Color.FromArgb(targetColor)
            stripLineTarget.BackColor = System.Drawing.Color.RosyBrown
            stripLineTarget.BackGradientStyle = GradientStyle.LeftRight
            stripLineTarget.BorderWidth = 2
            stripLineTarget.BorderDashStyle = ChartDashStyle.DashDot
            stripLineTarget.IntervalOffset = targetvalue

            Chart2.ChartAreas(0).AxisY.StripLines.Add(stripLineTarget)

            Chart2.ChartAreas(0).AxisY.LabelStyle.Enabled = True

            Dim cl As New CustomLabel
            cl.FromPosition = targetvalue
            cl.ToPosition = targetvalue + 1
            cl.Text = targetvalue.ToString()
            cl.RowIndex = 0

            Chart2.ChartAreas(0).AxisY.CustomLabels.Clear()
            Chart2.ChartAreas(0).AxisY.CustomLabels.Add(cl)

            Chart2.DataBind()
        Catch ex As Exception
            MessageBox.Show("Could not set the target line ") ' & ex.Message)
        End Try

    End Sub

End Class

Public Class Monitoring

    Public control(1) As Live
    Private Sub Monitoring_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '=========================================================================
        ' Position + Transparent BG
        'Me.MdiParent = Form1
        'Me.Location = New Point(Form3.Width, 10)
        'SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        'Me.BackColor = Color.Transparent
        'SetStyle(ControlStyles.DoubleBuffer, True)
        'SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        '=========================================================================
        ' Set the color in the MDI client.
        For Each ctl As Control In Me.Controls
            If TypeOf ctl Is MdiClient Then
                ctl.BackColor = Me.BackColor
            End If
        Next ctl
        '=========================================================================



        Call add_control()
        Call add_control()
    End Sub

    'Add a new monitoring control
    Private Sub add_control()

        ReDim Preserve control(UBound(control) + 1)
        control(UBound(control)) = New Live
        control(UBound(control)).Label1.Text = "status " & UBound(control)
        control(UBound(control)).MdiParent = Me
        control(UBound(control)).Show()
        '  FlowLayoutPanel1.Controls.Add(control(UBound(control)))
    End Sub
End Class
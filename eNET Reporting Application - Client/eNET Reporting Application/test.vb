Public Class test
    Private Sub test_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Dim startRadius As Integer = 30
        Dim endRadius As Integer = 50
        Dim startAngle As Single = 180.0F
        Dim sweepAngle As Single = 90.0F
        Dim heigh As Integer = 200

        Dim rect As New Rectangle(200, 200, heigh, heigh)
        Dim lBrush As New Drawing.Drawing2D.LinearGradientBrush(rect, Color.Blue, Color.Red, Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal)
        Dim p As New Pen(lBrush, endRadius - startRadius)

        Dim g As Graphics = Me.CreateGraphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.DrawArc(p, rect, startAngle, sweepAngle)


        'Dim rect1 As New Rectangle(100 / 2, 100 / 2, 0 * 2, 90 * 2)

        'g.DrawEllipse(p, rect1)

    End Sub


    Private Sub Draw_Ahmed_pie(startRadius As Integer, endRadius As Integer, startAngle As Single, sweepAngle As Single, heigh As Integer)


    End Sub

End Class
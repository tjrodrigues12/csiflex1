Public Class SplashScreen

    Private Sub SplashScreen_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        System.Threading.Thread.Sleep(3000)
        Me.Hide()

    End Sub
End Class
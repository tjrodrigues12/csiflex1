Public Class ClientSettings

    Public DatabaseIP As String = ""

    Public LoginUser As String = ""

    Public Password As String = ""

    Public EnetIPAddress As String = ""

    Public EnetIPPort As String = 80

    Public ReportsFolder As String = ""

    Public RefreshRate As Integer = 1000

    Public FirstDayOfWeek As Integer = 1

    Public LastDayOfWeek As Integer = 5

    Public FirstMonthOfYear As Integer = 1

    Public OtherColor As Integer = -32768

    Public TargetColor As Integer = -32768

    Public StartupDisplayType As Integer = 1

    Public StartupReportDays As Integer = 0

    Public StartupMachines As List(Of String)

    Public Sub New()
        StartupMachines = New List(Of String)()
    End Sub

End Class

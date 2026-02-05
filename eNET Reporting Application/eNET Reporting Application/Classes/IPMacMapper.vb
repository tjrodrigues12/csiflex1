Imports System.IO

Public Class IPMacMapper

    Private Shared list As List(Of IPAndMac)

    Private Shared Function ExecuteCommandLine(ByVal file As String, ByVal Optional arguments As String = "") As StreamReader
        Dim startInfo As ProcessStartInfo = New ProcessStartInfo()
        startInfo.CreateNoWindow = True
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardOutput = True
        startInfo.FileName = file
        startInfo.Arguments = arguments
        Dim process As Process = Process.Start(startInfo)
        Return process.StandardOutput
    End Function

    Private Shared Sub InitializeGetIPsAndMac()
        If list IsNot Nothing Then Return
        Dim arpStream = ExecuteCommandLine("arp", "-a")
        Dim result As List(Of String) = New List(Of String)()

        While Not arpStream.EndOfStream
            Dim line = arpStream.ReadLine().Trim()
            result.Add(line)
        End While

        list = result.Where(Function(x) Not String.IsNullOrEmpty(x) AndAlso (x.Contains("dynamic") OrElse x.Contains("static"))).[Select](Function(x)
                                                                                                                                              Dim parts As String() = x.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                                                                                                                                              Return New IPAndMac With {
                                                                                                                                                  .IP = parts(0).Trim(),
                                                                                                                                                  .MAC = parts(1).Trim()
                                                                                                                                              }
                                                                                                                                          End Function).ToList()
    End Sub

    Public Shared Function FindIPFromMacAddress(ByVal macAddress As String) As String
        InitializeGetIPsAndMac()
        Dim item As IPAndMac = list.SingleOrDefault(Function(x) x.MAC.ToUpper() = macAddress.ToUpper())
        If item Is Nothing Then Return Nothing
        Return item.IP
    End Function

    Public Shared Function FindMacFromIPAddress(ByVal ip As String) As String
        InitializeGetIPsAndMac()
        Dim item As IPAndMac = list.SingleOrDefault(Function(x) x.IP = ip)
        If item Is Nothing Then Return Nothing
        Return item.MAC
    End Function

    Private Class IPAndMac
        Public Property IP As String
        Public Property MAC As String
    End Class

End Class

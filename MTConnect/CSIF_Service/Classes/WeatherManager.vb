Imports CSIFLEX.Database.Access
Imports CSIFLEX.Utilities

Public Class WeatherManager

    Public Async Function GetWeather() As Task(Of Dictionary(Of String, String))

        Dim dicWeather As New Dictionary(Of String, String)
        Dim dicWeatherByCity As New Dictionary(Of String, String)

        Dim devices = MySqlAccess.GetDataTable("SELECT * FROM csi_database.tbl_deviceconfig WHERE temperature = 'on' AND detail_temperature <> ''")

        If devices.Rows.Count > 0 Then

            For Each device As DataRow In devices.Rows

                Dim deviceIP = device("IP")
                Dim city = device("detail_temperature")
                Dim units = device("degree")
                Dim key = $"{city}{units}"

                Try
                    If dicWeatherByCity.ContainsKey(key) Then
                        dicWeather.Add(deviceIP, dicWeatherByCity(key))
                    Else
                        Dim temp = CSIFLEX.Utilities.OpenWeather.GetTemperature(city, units)
                        dicWeatherByCity.Add(key, temp)
                        dicWeather.Add(deviceIP, temp)
                    End If
                Catch ex As Exception
                End Try
            Next
        End If

        Return dicWeather

    End Function

    Public Sub GetWeather(ByRef dicWeather As Dictionary(Of String, String))

        dicWeather = New Dictionary(Of String, String)
        Dim dicWeatherByCity As New Dictionary(Of String, String)

        Dim devices = MySqlAccess.GetDataTable("SELECT * FROM csi_database.tbl_deviceconfig WHERE temperature = 'on'")

        If devices.Rows.Count > 0 Then

            For Each device As DataRow In devices.Rows

                Dim deviceIP = device("IP")
                Dim city = device("detail_temperature")
                Dim units = device("degree")

                If dicWeatherByCity.ContainsKey(city) Then
                    dicWeather.Add(deviceIP, dicWeatherByCity(city))
                Else
                    Dim temp = CSIFLEX.Utilities.OpenWeather.GetTemperature(city, units)
                    dicWeatherByCity.Add(city, temp)
                    dicWeather.Add(deviceIP, temp)
                End If
            Next
        End If

    End Sub

End Class

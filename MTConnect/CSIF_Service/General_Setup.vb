
Imports CSI_Library
Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Net
Imports System.Configuration

Public Class General_Setup

    Public Shared Sub update_Port(portNumber As String, res As HttpListenerResponse)
        Dim db_connection As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try

            If IO.File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys") Then
                IO.File.Delete(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
            End If

            Using writer As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                writer.Write(portNumber)
                writer.Close()
            End Using


            db_connection.Open()
            If db_connection.State = ConnectionState.Open Then
                Dim cmdCREATTION As New MySqlCommand("CREATE TABLE if not exists CSI_Database.tbl_RM_Port (port integer);", db_connection)
                Dim cmd_result As Integer
                cmd_result = cmdCREATTION.ExecuteNonQuery()

                Dim query As String = "UPDATE CSI_database.tbl_RM_Port  SET " +
                                                 " port = @port_"

                Dim cmd As New MySqlCommand(query, db_connection)
                cmd.Parameters.AddWithValue("@port_", CInt(portNumber))

                If (cmd.ExecuteNonQuery() = 0) Then
                    cmd.CommandText = "INSERT INTO CSI_database.tbl_RM_Port " +
                        "(port) " + "VALUES (@port_)"
                    cmd.ExecuteNonQuery()
                End If
                db_connection.Close()
            End If
            'After updating db we need to make sure that program is using the latest port
            change_IP(False, res)
        Catch ex As Exception
            If db_connection.State = ConnectionState.Open Then db_connection.Close()
            MsgBox("Could not save the port : " & ex.Message)
            sendJson(res, "{Could not save the port" & ex.Message & "}")
        End Try
    End Sub

#If False Then
    Public Shared Sub WriteToFile(text As String)
        Try
            Dim path As String
            path = ConfigurationManager.AppSettings("DONUT_LOG_FILE")

            Using writer As New StreamWriter(path, True)
                writer.WriteLine(String.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")))
                writer.Close()
            End Using
        Catch ex As Exception
            Dim error1 As String
            error1 = ex.Message
        End Try
    End Sub
#End If



    Private Shared Sub change_IP(checkfile As Boolean, res As HttpListenerResponse)
        Dim IP_ As String = ""
        Dim CSI_Lib As New CSI_Library.CSI_Library(True)
        Dim IP_0 As String = ""
        If checkfile = True Then
            Try
                If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")) Then
                    Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")
                        IP_0 = reader.ReadLine
                        reader.Close()
                    End Using
                End If
            Catch ex As Exception
                CSI_Lib.LogServerError("At reading srvip  from csys: " & ex.Message, 1)
            End Try

        End If

        Dim IPv4Address As New List(Of String)
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                IPv4Address.Add(ipheal.ToString())
            End If
        Next

        If IPv4Address.Count = 0 Then
            sendJson(res, "{status:No IP address was found}")
        End If


        If IPv4Address.Count > 1 And checkfile = True Then
            If IPv4Address.Contains(IP_0) Then GoTo end_
        End If

        IP_ = IPv4Address.Item(0)

        Try
            ' If File.Exists((CSI_Lib.serverRootPath & "\sys\srv_ip_.csys")) Then
            Using w_ As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\srv_ip_.csys")
                w_.WriteLine(IP_)
            End Using
            '  End If

        Catch ex As Exception
            CSI_Lib.LogServerError("At writing srv_ip to csys: " & ex.Message, 1)
        End Try



        Dim PORT As String = ""
        Try
            If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")) Then
                Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                    PORT = reader.ReadLine
                    reader.Close()
                End Using
            Else
                PORT = "8008"
            End If
        Catch ex As Exception
            CSI_Lib.LogServerError("At reading RM Port from csys: " & ex.Message, 1)
        End Try


        Dim endUserConfigUrl As String = """http://" & IP_ & ":" & PORT & "/enduserconfig"";"
        Dim newestMachinesRecordsUrl As String = """http://" & IP_ & ":" & PORT & "/refresh"";"
        Dim timelineUrl As String = """http://" & IP_ & ":" & PORT & "/timeline"";"


        Dim r As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global.js")
        Dim w As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global-.js")
        Dim line As String = ""

        While ((Not r.EndOfStream))
            line = r.ReadLine()
            If (line.StartsWith("var endUserConfigUrl =")) Then
                w.WriteLine("var endUserConfigUrl =" & endUserConfigUrl)

            ElseIf (line.StartsWith("var newestMachinesRecordsUrl =")) Then
                w.WriteLine("var newestMachinesRecordsUrl =" & newestMachinesRecordsUrl)

            ElseIf (line.StartsWith("var timelineUrl = ")) Then
                w.WriteLine("var timelineUrl = " & timelineUrl)

            Else
                w.WriteLine(line)
            End If

        End While
        r.Close()
        w.Close()

        File.Delete(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\global.js")
        Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(CSI_Library.CSI_Library.serverRootPath & "\html\html\js\Global-.js", "global.js")
end_:
    End Sub

    Shared Sub sendJson(response As HttpListenerResponse, message As String)
        Dim output As System.IO.Stream
        Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(message)
        response.ContentLength64 = buffer.Length
        response.AppendHeader("Content-Type", "application/json")
        response.AppendHeader("Access-Control-Allow-Origin", "*")
        output = response.OutputStream
        output.Write(buffer, 0, buffer.Length)
        output.Close()
        output.Dispose()
    End Sub

    Public Shared Sub WriteToFile(text As String)
        Try
            Dim path As String
            path = ConfigurationManager.AppSettings("DONUT_LOG_FILE")


            Using writer As New StreamWriter(path, True)
                writer.WriteLine(String.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")))
                writer.Close()
            End Using
        Catch ex As Exception
            Dim error1 As String
            error1 = ex.Message
        End Try
    End Sub
End Class

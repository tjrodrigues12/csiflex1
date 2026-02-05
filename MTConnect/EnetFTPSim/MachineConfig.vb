'Imports System.Threading
Imports System.IO
Imports System.Text
Imports System.Net
Imports System.Timers

Public Class MachineConfig
    Private Property _machname As String
    public Property _enetftpip As String
    Public Property _enetftppwd As String
    Private Property _enetpos As String
    Private Property _sendfolder As String
    Private Property _receivefolder As String
    Private Property _ftpfilename As String
    'Average time per Cycle ON in minute
    Private Property _maxcontime As Integer
    Private Property _cmd_con As String
    Private Property _cmd_coff As String
    Private Property _cmd_partno As String
    Private Property _two_head As Boolean
    Private Property _current_head As Integer
    Private Property _cmd_con1 As String
    Private Property _cmd_coff1 As String
    Private Property _cmd_partno1 As String
    Private Property _cmd_con2 As String
    Private Property _cmd_coff2 As String
    Private Property _cmd_partno2 As String
    Private Property _cmd_prod As String
    Private Property _cmd_setup As String

    Private Property _cmd_others As New List(Of String)

    Public previousstatus As String = "CYCLE OFF"
    Public currentstatus As String = "CYCLE OFF"
    Dim randompns As New List(Of String)


    Public machTimer As Timer
    'INIT
    Dim _rnd As Random
    Dim rng_tricked As RNGesus '(New Integer() {150, 40, 15, 3})
    Dim rng_number As Integer = 0
    Dim delay As Integer = 1


    Public Sub New(machname As String, machpos As String, maxcontime As Integer, pns As List(Of String), sendfolder As String, receivefolder As String, enetftpip As String, enetftppwd As String, ByRef rnd As Random)
        _machname = machname
        _enetpos = machpos
        _sendfolder = sendfolder
        _receivefolder = receivefolder
        _enetftpip = enetftpip
        _enetftppwd = enetftppwd
        _maxcontime = maxcontime
        _two_head = False
        _rnd = rnd
        _current_head = 1

        randompns = pns
    End Sub

    ReadOnly Property MachineName As String
        Get
            Return _machname
        End Get
    End Property

    ReadOnly Property EnetPos As String
        Get
            Return _enetpos
        End Get
    End Property

    Property FTPFileName As String
        Get
            Return _ftpfilename
        End Get
        Set(value As String)
            _ftpfilename = value
        End Set
    End Property

    Property MaxCONTime As Integer
        Get
            Return _maxcontime
        End Get
        Set(value As Integer)
            _maxcontime = value
        End Set
    End Property

    Property TwoHead As Boolean
        Get
            Return _two_head
        End Get
        Set(value As Boolean)
            _two_head = value
        End Set
    End Property

    ReadOnly Property Cmd_CON As String
        Get
            Return _cmd_con
        End Get
        'Set(value As String)
        '    _cmd_con = value
        'End Set
    End Property

    ReadOnly Property Cmd_COFF As String
        Get
            Return _cmd_coff
        End Get
        'Set(value As String)
        '    _cmd_coff = value
        'End Set
    End Property

    ReadOnly Property Cmd_PARTNO As String
        Get
            Return _cmd_partno
        End Get
        'Set(value As String)
        '    _cmd_partno = value
        'End Set
    End Property

    Property Cmd_CON1 As String
        Get
            Return _cmd_con1
        End Get
        Set(value As String)
            _cmd_con1 = value
        End Set
    End Property

    Property Cmd_COFF1 As String
        Get
            Return _cmd_coff1
        End Get
        Set(value As String)
            _cmd_coff1 = value
        End Set
    End Property

    Property Cmd_PARTNO1 As String
        Get
            Return _cmd_partno1
        End Get
        Set(value As String)
            _cmd_partno1 = value
        End Set
    End Property

    Property Cmd_CON2 As String
        Get
            Return _cmd_con2
        End Get
        Set(value As String)
            _cmd_con2 = value
        End Set
    End Property

    Property Cmd_COFF2 As String
        Get
            Return _cmd_coff2
        End Get
        Set(value As String)
            _cmd_coff2 = value
        End Set
    End Property

    Property Cmd_PARTNO2 As String
        Get
            Return _cmd_partno2
        End Get
        Set(value As String)
            _cmd_partno2 = value
        End Set
    End Property

    Property Cmd_PROD As String
        Get
            Return _cmd_prod
        End Get
        Set(value As String)
            _cmd_prod = value
        End Set
    End Property

    Property Cmd_SETUP As String
        Get
            Return _cmd_setup
        End Get
        Set(value As String)
            _cmd_setup = value
        End Set
    End Property

    Property Cmd_OTHERS As List(Of String)
        Get
            Return _cmd_others
        End Get
        Set(value As List(Of String))
            _cmd_others = New List(Of String)(value)
        End Set
    End Property

    Private Function CheckConnInfo() As Boolean
        Dim res As Boolean = False

        If (_enetftpip.Length > 0) And (FTPFileName.Length > 0) Then
            res = True
        End If

        Return res
    End Function

    Private Function GetCMDPartno() As String
        Dim cmd As String = ""


        Dim randompn As String = randompns(_rnd.Next(10))
        Dim partcycletime = _rnd.Next(1, _maxcontime * 1.5) 'gets random part cycle time between 1 minute and maxcontime*2 

        Dim mincycletime = partcycletime * 0.75 '25% variance
        Dim maxcycletime = partcycletime * 1.25

        'cmd = (Cmd_PARTNO & randompn)
        cmd = (Cmd_PARTNO & randompn & "," & mincycletime & "," & maxcycletime)

        Return cmd
    End Function


    Public Sub StartTimer()
        'set All machines to Cycle off
        If (CheckConnInfo()) Then
            'Initial set to head 1
            _cmd_con = Cmd_CON1
            _cmd_coff = Cmd_COFF1
            _cmd_partno = Cmd_PARTNO1

            currentstatus = Cmd_COFF
            previousstatus = currentstatus
            Dim randompn As String = randompns(_rnd.Next(10))
            'SendCMDEnetFTP(Cmd_PARTNO & randompn) 'random partnumber
            SendCMDEnetFTP(GetCMDPartno) 'random partnumber
            System.Threading.Thread.Sleep(500)
            SendCMDEnetFTP(Cmd_COFF)
            SendCMDEnetFTP(Cmd_PROD)
            'Thread.Sleep(500)
            'SendCMDEnetFTP(Cmd_PARTNO & randompn) 'second call because sometimes the first one is not registered
            machTimer = New Timer(1) '1 millisecond
            ' machTimer.SynchronizingObject = Main.BTN_Start
            AddHandler machTimer.Elapsed, AddressOf machtimer_Ticked
            machTimer.AutoReset = False
            machTimer.Start()
        End If
    End Sub

    Private Sub machtimer_Ticked()
        Try
            machTimer.Stop()
            'generate reports
            StartWork()
        Catch ex As Exception
            Console.WriteLine("Machine:" & MachineName & ";General error in timer_ticked method:" & ex.ToString())
        Finally
            machTimer.Start()
        End Try
    End Sub

    Private Sub ChangeHead()
        If (TwoHead) Then
            If (_current_head = 1) Then
                _current_head = 2
                _cmd_con = Cmd_CON2
                _cmd_coff = Cmd_COFF2
                _cmd_partno = Cmd_PARTNO2
            Else
                _current_head = 1
                _cmd_con = Cmd_CON1
                _cmd_coff = Cmd_COFF1
                _cmd_partno = Cmd_PARTNO1
            End If
        Else
            _current_head = 1
            _cmd_con = Cmd_CON1
            _cmd_coff = Cmd_COFF1
            _cmd_partno = Cmd_PARTNO1
        End If
    End Sub

    Private Sub StartWork()
        Try
            If (currentstatus = Cmd_CON) Then
                'OPTIONS:
                'stay in CON
                'COFF
                'OTHERS
                If (previousstatus = Cmd_CON) Then
                    '10% chance to stay CON
                    '70% chance to go COFF
                    '20% chance to go OTHERS
                    rng_tricked = New RNGesus(New Integer() {10, 70, 20})
                    rng_number = rng_tricked.NextValue

                    If (rng_number = 0) Then
                        'STAY CON
                        previousstatus = currentstatus
                    ElseIf (rng_number = 1) Then
                        'GO COFF
                        SendCMDEnetFTP(Cmd_COFF)
                        SendCMDEnetFTP(Cmd_PROD)
                    ElseIf (rng_number = 2) Then
                        'GO OTHERS
                        'otherstatusindex = rnd.Next(otherstatuscnt)
                        SendCMDEnetFTP(Cmd_OTHERS(_rnd.Next(Cmd_OTHERS.Count)))
                    End If
                Else
                    '30% chance to stay CON
                    '60% chance to go COFF
                    '10% chance to go OTHERS
                    rng_tricked = New RNGesus(New Integer() {30, 60, 10})
                    rng_number = rng_tricked.NextValue

                    If (rng_number = 0) Then
                        'STAY CON
                        previousstatus = currentstatus
                    ElseIf (rng_number = 1) Then
                        'GO COFF
                        SendCMDEnetFTP(Cmd_COFF)
                        SendCMDEnetFTP(Cmd_PROD)
                    ElseIf (rng_number = 2) Then
                        'GO OTHERS
                        'otherstatusindex = rnd.Next(otherstatuscnt)
                        SendCMDEnetFTP(Cmd_OTHERS(_rnd.Next(Cmd_OTHERS.Count)))
                    End If
                End If
            ElseIf (currentstatus = Cmd_COFF) Then
                'Options:
                'Stay in COFF
                'CON
                'SETUP
                'OTHERS
                'PARTNO:IF SETUP YES, else NO
                If (previousstatus = Cmd_COFF) Then
                    '10% chance to stay COFF
                    '65% chance to go CON
                    '10% chance to go SETUP
                    '15% chance to go OTHERS
                    rng_tricked = New RNGesus(New Integer() {10, 65, 10, 15})
                    rng_number = rng_tricked.NextValue

                    If (rng_number = 0) Then
                        'STAY COFF
                        previousstatus = currentstatus
                    ElseIf (rng_number = 1) Then
                        'GO CON
                        SendCMDEnetFTP(Cmd_PROD)
                        SendCMDEnetFTP(Cmd_CON)
                    ElseIf (rng_number = 2) Then
                        'GO SETUP
                        SendCMDEnetFTP(Cmd_SETUP)
                        'changepn = True
                        System.Threading.Thread.Sleep(500)

                        ChangeHead()

                        'SendCMDEnetFTP(Cmd_PARTNO & randompns(_rnd.Next(10)))
                        SendCMDEnetFTP(GetCMDPartno)
                    ElseIf (rng_number = 3) Then
                        'GO OTHERS
                        'otherstatusindex = rnd.Next(otherstatuscnt)
                        SendCMDEnetFTP(Cmd_OTHERS(_rnd.Next(Cmd_OTHERS.Count)))
                    End If
                Else
                    '20% chance to stay COFF
                    '60% chance to go CON
                    '10% chance to go SETUP
                    '10% chance to go OTHERS
                    rng_tricked = New RNGesus(New Integer() {20, 60, 10, 10})
                    rng_number = rng_tricked.NextValue

                    If (rng_number = 0) Then
                        'STAY COFF
                        previousstatus = currentstatus
                    ElseIf (rng_number = 1) Then
                        'GO CON
                        SendCMDEnetFTP(Cmd_PROD)
                        SendCMDEnetFTP(Cmd_CON)
                    ElseIf (rng_number = 2) Then
                        'GO SETUP
                        SendCMDEnetFTP(Cmd_SETUP)
                        'changepn = True
                        System.Threading.Thread.Sleep(500)
                        ChangeHead()
                        'SendCMDEnetFTP(Cmd_PARTNO & randompns(_rnd.Next(10)))
                        SendCMDEnetFTP(GetCMDPartno)
                    ElseIf (rng_number = 3) Then
                        'GO OTHERS
                        'otherstatusindex = rnd.Next(otherstatuscnt)
                        SendCMDEnetFTP(Cmd_OTHERS(_rnd.Next(Cmd_OTHERS.Count)))
                    End If
                End If
            ElseIf (currentstatus = Cmd_SETUP) Then
                'Options:
                'Stay in SETUP
                'CON
                'OTHERS
                'PARTNO:NO Already changed when SETUP happens after COFF or OTHERS (only way to get in setup)
                If (previousstatus = Cmd_SETUP) Then
                    '5% chance to stay SETUP
                    '90% chance to go CON
                    '5% chance to go OTHERS
                    rng_tricked = New RNGesus(New Integer() {5, 90, 5})
                    rng_number = rng_tricked.NextValue

                    If (rng_number = 0) Then
                        'STAY SETUP
                        previousstatus = currentstatus
                    ElseIf (rng_number = 1) Then
                        'GO CON
                        SendCMDEnetFTP(Cmd_PROD)
                        SendCMDEnetFTP(Cmd_CON)
                    ElseIf (rng_number = 2) Then
                        'GO OTHERS
                        'otherstatusindex = rnd.Next(otherstatuscnt)
                        SendCMDEnetFTP(Cmd_OTHERS(_rnd.Next(Cmd_OTHERS.Count)))
                    End If
                Else
                    '15% chance to stay SETUP
                    '80% chance to go CON
                    '5% chance to go OTHERS
                    rng_tricked = New RNGesus(New Integer() {15, 80, 5})
                    rng_number = rng_tricked.NextValue

                    If (rng_number = 0) Then
                        'STAY SETUP
                        previousstatus = currentstatus
                    ElseIf (rng_number = 1) Then
                        'GO CON
                        SendCMDEnetFTP(Cmd_PROD)
                        SendCMDEnetFTP(Cmd_CON)
                    ElseIf (rng_number = 2) Then
                        'GO OTHERS
                        'otherstatusindex = rnd.Next(otherstatuscnt)
                        SendCMDEnetFTP(Cmd_OTHERS(_rnd.Next(Cmd_OTHERS.Count)))
                    End If
                End If
            Else
                'OPTIONS:
                'Stay in current other
                'SETUP
                'COFF
                'OTHERS
                'PARTNO IF SETUP
                If (Cmd_OTHERS.Contains(previousstatus)) Then
                    '25% chance to stay OTHER
                    '60% chance to go COFF
                    '15% chance to go SETUP
                    rng_tricked = New RNGesus(New Integer() {25, 60, 15})
                    rng_number = rng_tricked.NextValue

                    If (rng_number = 0) Then
                        'STAY OTHER
                        previousstatus = currentstatus
                    ElseIf (rng_number = 1) Then
                        'GO COFF
                        SendCMDEnetFTP(Cmd_COFF)
                        SendCMDEnetFTP(Cmd_PROD)
                    ElseIf (rng_number = 2) Then
                        'GO SETUP
                        SendCMDEnetFTP(Cmd_SETUP)
                        'changepn = True
                        System.Threading.Thread.Sleep(500)
                        ChangeHead()
                        'SendCMDEnetFTP(Cmd_PARTNO & randompns(_rnd.Next(10)))
                        SendCMDEnetFTP(GetCMDPartno)
                    End If
                Else
                    '35% chance to stay OTHER
                    '60% chance to go COFF
                    '5% chance to go SETUP
                    rng_tricked = New RNGesus(New Integer() {35, 60, 5})
                    rng_number = rng_tricked.NextValue

                    If (rng_number = 0) Then
                        'STAY OTHER
                        previousstatus = currentstatus
                    ElseIf (rng_number = 1) Then
                        'GO COFF
                        SendCMDEnetFTP(Cmd_COFF)
                        SendCMDEnetFTP(Cmd_PROD)
                    ElseIf (rng_number = 2) Then
                        'GO SETUP
                        SendCMDEnetFTP(Cmd_SETUP)
                        'changepn = True
                        System.Threading.Thread.Sleep(500)
                        ChangeHead()
                        'SendCMDEnetFTP(Cmd_PARTNO & randompns(_rnd.Next(10)))
                        SendCMDEnetFTP(GetCMDPartno)
                    End If
                End If
            End If

            'use current status after SendCMD because the value is changed there
            If (currentstatus = Cmd_CON) Then
                delay = _rnd.Next(120, _maxcontime * 60) 'between 10 seconds and maxcontime
            ElseIf (currentstatus = Cmd_COFF) Then
                delay = _rnd.Next(15, 10 * 60) 'between 5 seconds and 10min
            ElseIf (currentstatus = Cmd_SETUP) Then
                delay = _rnd.Next(1 * 60, 20 * 60) 'between 1min and 20min
            Else
                delay = _rnd.Next(10 * 60, 10 * 60) 'between 1min and 10min
            End If

            Console.WriteLine(MachineName & ": Current status=" & currentstatus & "; Duration=" & (delay / 60) & " minutes")
            'Delay in seconds

            machTimer.Interval = (delay * 100)

        Catch ex As Exception
            Console.WriteLine(MachineName & " Exception:" & ex.Message)
        End Try
        'End While

    End Sub

    Public Sub StopTimer()
        Try
            machTimer.Stop()
        Catch ex As Exception
            Console.WriteLine("Exception trying to stop the timer:" + ex.Message)
        End Try

        Cleanup()
    End Sub

    'Set machines back to COFF without partnumbers
    Private Sub Cleanup()
        SendCMDEnetFTP(Cmd_PARTNO & "") 'empty partnumber
        System.Threading.Thread.Sleep(500)
        SendCMDEnetFTP(Cmd_COFF)
        SendCMDEnetFTP(Cmd_PROD)
    End Sub

    'From Steve's cycle generator
    Dim response As System.Net.FtpWebResponse
    Private Sub SendCMDEnetFTP(command As String)
        If command <> Cmd_PROD And Not command.StartsWith(Cmd_PARTNO) Then
            SyncLock (Main.STATUS__)
                If Main.STATUS__.ContainsKey(MachineName) Then
                    Main.STATUS__(MachineName) = command
                Else
                    Main.STATUS__.Add(MachineName, command)
                End If
            End SyncLock



        End If
        Threading.Thread.Sleep(500)

        '                           Try
        '                               Dim URI As String = "ftp://" & _enetftpip & "/" & FTPFileName
        '                               'Dim connection As String = "ftp://" & _enetftpip & "/" & FTPFileName
        '                               Dim request As System.Net.FtpWebRequest = DirectCast(System.Net.FtpWebRequest.Create(URI), System.Net.FtpWebRequest)

        '                               'request.Credentials = New System.Net.NetworkCredential(FTP_User1, FTP_PW)
        '                               request.Credentials = New System.Net.NetworkCredential(MachineName, _enetftppwd)
        '                               request.Method = System.Net.WebRequestMethods.Ftp.UploadFile
        '                               request.UsePassive = False
        '                               'request.KeepAlive = False

        '                               Dim encoding As New System.Text.UTF8Encoding()
        '                               'Dim file() As Byte = encoding.GetBytes("CSTART")
        '                               'Threading.Thread.Sleep(1000)
        '                               Dim file() As Byte = encoding.GetBytes(command)
        '                               Dim strz As System.IO.Stream = request.GetRequestStream()

        '                               strz.Write(file, 0, file.Length)
        '                               strz.Close()
        '                               strz.Dispose()

        '                               Try
        '                                   Do
        '                                       response = request.GetResponse()
        '                                       Threading.Thread.Sleep(500)
        '                                   Loop While (response.StatusDescription <> "226 TRANSFER COMPLETE" & vbCrLf)
        '                               Catch ex As Exception
        '                                   MsgBox("Enet response err : " + ex.Message)
        '                               End Try



        '                               'Shift Current status to previous if not prod cmd
        '                               If Not (command = Cmd_PROD) And Not (command.StartsWith(Cmd_PARTNO)) Then
        '                                   previousstatus = currentstatus
        '                                   currentstatus = command
        '                               End If

        '                           Catch ex As Exception
        '                               If response IsNot Nothing Then

        '                               Else
        '                                   MessageBox.Show("Unable to send command via FTP:" + ex.Message + vbCrLf + "enet response : " + response.StatusDescription)

        '                               End If
        '                               'Console.WriteLine("Unable to send command via FTP:" + ex.Message)

        '                           End Try




    End Sub

    'Private Sub SendCMDEnetFTP(ip As String, pwd As String, filename As String, machinename As String, command As String)
    'Private Sub SendCMDEnetFTP(command As String)
    '    Try
    '        Dim connection As String = "ftp://" & _enetftpip & "/" & FTPFileName
    '        Dim request As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(connection), System.Net.FtpWebRequest)
    '        request.Credentials = New System.Net.NetworkCredential(MachineName, _enetftppwd)
    '        request.Timeout = 1000 * 60 'timeout after 1 min
    '        request.UsePassive = False
    '        request.KeepAlive = True
    '        request.ConnectionGroupName = "EnetSIM"
    '        request.ServicePoint.ConnectionLimit = 8
    '        request.Method = System.Net.WebRequestMethods.Ftp.UploadFile

    '        Dim file_b() As Byte = Encoding.ASCII.GetBytes(command) 'GetBytes(command)


    '        ' Create a Uri instance with the specified URI string.
    '        ' If the URI is not correctly formed, the Uri constructor
    '        ' will throw an exception.
    '        Dim waitObject As ManualResetEvent

    '        Dim state As New FtpState()

    '        ' asynchronous operations.
    '        state.Request = request
    '        state.FileByte = file_b 'FTPFileName


    '        ' Get the event to wait on.
    '        waitObject = state.OperationComplete

    '        ' Asynchronously get the stream for the file contents.
    '        request.BeginGetRequestStream(New AsyncCallback(AddressOf EndGetStreamCallback), state)

    '        ' Block the current thread until all operations are complete.
    '        waitObject.WaitOne()


    '        ' The operations either completed or threw an exception.
    '        If state.OperationException IsNot Nothing Then
    '            Throw state.OperationException
    '            'Else
    '            'Console.WriteLine("The operation completed - {0}", state.StatusDescription)
    '        End If


    '        ''Dim strz As System.IO.Stream = Await request.GetRequestStreamAsync()
    '        'Dim strz As System.IO.Stream = request.GetRequestStream()
    '        'strz.Write(file_b, 0, file_b.Length)
    '        'strz.Close()
    '        'strz.Dispose()

    '        'Shift Current status to previous if not prod cmd
    '        If Not (command = Cmd_PROD) And Not (command.StartsWith(Cmd_PARTNO)) Then
    '            previousstatus = currentstatus
    '            currentstatus = command
    '        End If

    '    Catch ex As Exception
    '        'Console.WriteLine("Unable to send command via FTP:" + ex.Message)
    '        MessageBox.Show("Unable to send command via FTP:" + ex.Message)
    '    End Try

    'End Sub

    'Private Sub EndGetStreamCallback(ByVal ar As IAsyncResult)
    '    Dim state As FtpState = CType(ar.AsyncState, FtpState)

    '    Dim requestStream As Stream = Nothing
    '    ' End the asynchronous call to get the request stream.
    '    Try
    '        requestStream = state.Request.EndGetRequestStream(ar)
    '        ' Copy the file contents to the request stream.

    '        'Const bufferLength As Integer = 2048
    '        'Dim buffer(bufferLength - 1) As Byte
    '        'Dim count As Integer = 0
    '        'Dim readBytes As Integer = 0
    '        'Dim stream As FileStream = File.OpenRead(state.FileName)
    '        'Do
    '        '    readBytes = stream.Read(buffer, 0, bufferLength)
    '        '    requestStream.Write(buffer, 0, readBytes)
    '        '    count += readBytes
    '        'Loop While readBytes <> 0
    '        'Console.WriteLine("Writing {0} bytes to the stream.", count)

    '        requestStream.Write(state.FileByte, 0, state.FileByte.Length)

    '        ' IMPORTANT: Close the request stream before sending the request.
    '        requestStream.Close()
    '        ' Asynchronously get the response to the upload request.
    '        state.Request.BeginGetResponse(New AsyncCallback(AddressOf EndGetResponseCallback), state)
    '        ' Return exceptions to the main application thread.
    '    Catch e As Exception
    '        Console.WriteLine("Could not get the request stream.")
    '        state.OperationException = e
    '        state.OperationComplete.Set()
    '        Return
    '    End Try

    'End Sub

    '' The EndGetResponseCallback method  
    '' completes a call to BeginGetResponse.
    'Private Sub EndGetResponseCallback(ByVal ar As IAsyncResult)
    '    Dim state As FtpState = CType(ar.AsyncState, FtpState)
    '    Dim response As FtpWebResponse = Nothing
    '    Try
    '        response = CType(state.Request.EndGetResponse(ar), FtpWebResponse)
    '        response.Close()
    '        state.StatusDescription = response.StatusDescription
    '        ' Signal the main application thread that 
    '        ' the operation is complete.
    '        state.OperationComplete.Set()
    '        ' Return exceptions to the main application thread.
    '    Catch e As Exception
    '        Console.WriteLine("Error getting response.")
    '        state.OperationException = e
    '        state.OperationComplete.Set()
    '    End Try
    'End Sub

    'Private Shared Function GetBytes(str As String) As Byte()
    '    'Dim bytes As Byte() = New Byte(str.Length * 2 - 1) {}
    '    'System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length)
    '    Dim bytes As Byte() ' = New Byte(str.Length - 1) {}
    '    bytes = Encoding.ASCII.GetBytes(str)
    '    Return bytes
    'End Function

    'Private Shared Function GetString(bytes As Byte()) As String
    '    Dim chars As Char() = New Char(bytes.Length / 2 - 1) {}
    '    System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length)
    '    Return New String(chars)
    'End Function

    'Private Shared _readWriteLock As New ReaderWriterLockSlim()

    'Public Sub WriteToFileThreadSafe(path As String, text As String)
    '    ' Set Status to Locked
    '    _readWriteLock.EnterWriteLock()
    '    Try
    '        'write file
    '        'File.WriteAllText(path, text)
    '        ' Append text to the file
    '        Using sw As StreamWriter = File.CreateText(path)
    '            sw.WriteLine(text)
    '            sw.Close()
    '        End Using
    '    Finally
    '        ' Release lock
    '        _readWriteLock.ExitWriteLock()
    '    End Try
    'End Sub


End Class

Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Threading

Public Class Main

    Dim CONFIGFILENAME As String = "enetsimconfig.ini"

    Dim enetftpip As String = "0.0.0.0"
    Dim enetftppwd As String = ""
    Dim enetpath As String = "C:\_eNETDNC"
    Dim machlist As New List(Of MachineConfig)

    Const chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"

    'Shared stopThreads As Boolean = False
    'Dim threads As New List(Of Thread) 'if you wish to use List
    'or
    'Dim dThreads As New Dictionary(Of String, Thread) 'if you wish to use dictionary
    Public rate As Integer
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            'Check config file
            If Not (CheckConfig(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\" & CONFIGFILENAME)) Then
                CreateConfig()
                If Not (CheckConfig(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\" & CONFIGFILENAME)) Then
                    MessageBox.Show("There is an error with your configuration file. The software will exit.", "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Dispose()
                End If
            End If
            'if not exist, ask and save
            'load enet config (ftp machine+status)
            LoadMachineList()

            If (machlist.Count > 0) Then
                CB_MachList.DataSource = machlist
                CB_MachList.DisplayMember = "MachineName"
            Else
                CB_MachList.DataSource = Nothing
                CB_MachList.DisplayMember = ""
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading the software")
            Environment.Exit(0)
        End Try
    End Sub

    Private Sub CreateConfig()
        Dim frm_Config As New Config
        frm_Config.ShowDialog()
    End Sub

    Private Function CheckConfig(filename As String) As Boolean
        Dim config As Boolean = False

        If (File.Exists(filename)) Then
            Dim lines As String() = File.ReadAllLines(filename)
            For Each line As String In lines
                If (line.StartsWith("ENETFTPIP:")) Then
                    enetftpip = line.Substring(line.IndexOf(":") + 1)
                ElseIf (line.StartsWith("ENETFTPPWD:")) Then
                    enetftppwd = line.Substring(line.IndexOf(":") + 1)
                ElseIf (line.StartsWith("ENETPATH:")) Then
                    enetpath = line.Substring(line.IndexOf(":") + 1)
                End If
            Next
            config = True
        End If

        Return config

    End Function


    Private Sub LoadMachineList()
        'Read eHUBConf and MonSetup
        'read ehubconf
        Dim tmachlist As New List(Of MachineConfig)
        machlist.Clear()
        Dim ehubfile As String = enetpath & "\_SETUP\eHUBConf.sys"
        Dim monsetupfile As String = enetpath & "\_SETUP\MonSetup.sys"


        If (File.Exists(ehubfile) And File.Exists(monsetupfile)) Then
            Dim line As String = ""
            Dim tempsection As String = ""
            Dim ehubsections As New List(Of String)
            Using fs As New StreamReader(File.OpenRead(ehubfile))
                While Not (fs.EndOfStream)
                    line = fs.ReadLine()
                    If (line.Length > 0) Then
                        tempsection = tempsection & Environment.NewLine & line
                    Else
                        If (tempsection.Length > 0) Then
                            ehubsections.Add(tempsection)
                            tempsection = ""
                        End If
                    End If
                End While
            End Using

            Dim rand As New Random
            For Each section In ehubsections
                If (section.IndexOf("FI:1") > 0) Then
                    Dim machname As String = GetEnetValue(section, "NM:")
                    'Dim mon_on As String = GetEnetValue(section, "ON:")
                    Dim machpos As String = section.Trim.Substring(0, section.IndexOf(":") - 2)
                    Dim sendfolder As String = GetEnetValue(section, "DT:")
                    Dim receivefolder As String = GetEnetValue(section, "DR:")
                    Dim maxcontime = rand.Next(10, 121) 'CON time between 1 and 120 minutes
                    Dim randompns As New List(Of String)
                    For i = 0 To 9
                        Dim str0 As String = RandomString(rand, rand.Next(2, 2))
                        Dim str1 As String = RandomString(rand, rand.Next(4, 4))
                        randompns.Add(str0 + "-" + str1)
                        '  randompns.Add(RandomString(rand, rand.Next(5, 10)))
                    Next

                    tmachlist.Add(New MachineConfig(machname, machpos, maxcontime, randompns, sendfolder, receivefolder, enetftpip, enetftppwd, rand))
                End If
            Next

            Dim monsetupsections As New List(Of String)
            Dim lastline As String = ""
            Dim emptyparam As Boolean = False
            Using fs As New StreamReader(File.OpenRead(monsetupfile))
                While Not (fs.EndOfStream)
                    line = fs.ReadLine()
                    If (line.Length > 0) Then
                        tempsection = tempsection & Environment.NewLine & line
                        emptyparam = False
                    Else
                        If (tempsection.Length > 0) And Not emptyparam Then
                            If (lastline.StartsWith("DF:")) Then
                                emptyparam = True
                            Else
                                monsetupsections.Add(tempsection)
                                tempsection = ""
                            End If
                        End If
                    End If
                    lastline = line
                End While
            End Using

            For Each mach In tmachlist
                For Each section In monsetupsections
                    Dim sectionpos As String = section.Trim.Substring(0, section.IndexOf(":") - 2)
                    If (sectionpos = mach.EnetPos) Then
                        'Dim cmd_dprod As String = GetEnetValue(section, "DD:").Split(",")(0)
                        Dim mon_on As String = GetEnetValue(section, "ON:")
                        Dim two_head As Boolean = If(GetEnetValue(section, "TH:") = "0", False, True)
                        Dim ftpfilename As String = GetEnetValue(section, "DD:").Split(",")(1)
                        Dim cmd_partno1 As String = GetEnetValue(section, "N1:")
                        Dim cmd_partno2 As String = GetEnetValue(section, "PI:")
                        Dim cmd_CON1 As String = GetEnetValue(section, "PS:")
                        Dim cmd_COFF1 As String = GetEnetValue(section, "DE:")
                        Dim cmd_CON2 As String = GetEnetValue(section, "P2:")
                        Dim cmd_COFF2 As String = GetEnetValue(section, "D2:")
                        Dim cmd_prod As String = GetEnetValue(section, "PR:")
                        Dim cmd_setup As String = GetEnetValue(section, "TR:")
                        Dim tnameothers As String = GetEnetMultiValue(section, "DF:", "CM:")
                        Dim name_others As New List(Of String)
                        If (tnameothers.Length > 0) Then
                            name_others = tnameothers.Split(Environment.NewLine).ToList
                        End If
                        Dim tcmdothers As String = GetEnetMultiValue(section, "CM:", "AC:")
                        Dim cmd_others As New List(Of String)
                        If (tcmdothers.Length > 0) Then
                            cmd_others = tcmdothers.Split(Environment.NewLine).ToList
                        End If
                        Dim others As New List(Of String)
                        Dim cnt_others As Integer = If(name_others.Count <= cmd_others.Count, name_others.Count, cmd_others.Count)
                        For i = 0 To cnt_others - 1
                            'others.Add(name_others(i).Trim, cmd_others(i).Trim)
                            others.Add(cmd_others(i).Trim)
                        Next

                        mach.FTPFileName = ftpfilename
                        mach.TwoHead = two_head
                        mach.Cmd_PARTNO1 = cmd_partno1
                        mach.Cmd_CON1 = cmd_CON1
                        mach.Cmd_COFF1 = cmd_COFF1
                        mach.Cmd_PARTNO2 = cmd_partno2
                        mach.Cmd_CON2 = cmd_CON2
                        mach.Cmd_COFF2 = cmd_COFF2
                        mach.Cmd_PROD = cmd_prod
                        mach.Cmd_SETUP = cmd_setup
                        mach.Cmd_OTHERS = others

                        If (mon_on = "1") Then
                            machlist.Add(mach)
                        End If

                    End If
                Next
            Next
        Else
            MessageBox.Show("Enet is missing configuration files eHUBCONF.sys or MonSetup.sys")
        End If
    End Sub


    Private Function GetEnetValue(section As String, parameter As String) As String
        Dim value As String = ""

        Dim startpos = section.IndexOf(parameter) + parameter.Length
        Dim endpos = section.IndexOf(Environment.NewLine, section.IndexOf(parameter) + 1)

        value = section.Substring(startpos, endpos - startpos)

        Return value
    End Function

    Private Function GetEnetMultiValue(section As String, startparameter As String, endparameter As String) As String
        Dim value As String = ""

        Dim startpos = section.IndexOf(startparameter) + startparameter.Length
        Dim endpos = section.IndexOf(endparameter)

        value = section.Substring(startpos, endpos - startpos).Trim

        Return value
    End Function

    Public Shared STATUS__ As New Dictionary(Of String, String)
    ' Dim dispatcher__ As 
    Private Sub BTN_Start_Click(sender As Object, e As EventArgs) Handles BTN_Start.Click
        LBL_Status.Text = "Starting"
        ' Me.dispatcher__ = dispatcher__.CurrentDispatcher
        'Dim mach As MachineConfig = machlist(0)
        For Each mach In machlist

            'mach.machTimer.SynchronizingObject = Me.BTN_Start
            mach.StartTimer()

            Thread.Sleep(500)
        Next
        'machlist(0).StartTimer()

        LBL_Status.Text = "Running"



        While (1)
            For Each mach In machlist
                Try
                    If STATUS__.ContainsKey(mach.MachineName) Then
                        Dim Command As String = STATUS__(mach.MachineName)
                        Dim URI As String = "ftp://" & mach._enetftpip & "/" & mach.FTPFileName
                        Dim request As System.Net.FtpWebRequest = DirectCast(System.Net.FtpWebRequest.Create(URI), System.Net.FtpWebRequest)
                        ' request.ServicePoint.ConnectionLimit = 1
                        request.Credentials = New System.Net.NetworkCredential(mach.MachineName, mach._enetftppwd)
                        request.Method = System.Net.WebRequestMethods.Ftp.UploadFile
                        request.UsePassive = False
                        Dim encoding As New System.Text.UTF8Encoding()
                        Dim file() As Byte = encoding.GetBytes(Command)
                        Dim strz As System.IO.Stream = request.GetRequestStream()
                        strz.Write(file, 0, file.Length)
                        strz.Close()
                        strz.Dispose()

                        If Not (Command = mach.Cmd_PROD) And Not (Command.StartsWith(mach.Cmd_PARTNO)) Then
                            mach.previousstatus = mach.currentstatus
                            mach.currentstatus = Command
                        End If

                    End If
                Catch ex As Exception
                    MsgBox("  err : " + ex.Message)
                End Try
            Next
            Thread.Sleep(1000)
        End While
    End Sub

    Private Sub BTN_Stop_Click(sender As Object, e As EventArgs) Handles BTN_Stop.Click
        'For Each thread In threads
        '    thread.Abort()
        '    thread()
        '    'thread.Join(3000)
        'Next
        LBL_Status.Text = "Stopping"
        For Each mach In machlist
            mach.StopTimer()
            Thread.Sleep(1000)
        Next
        LBL_Status.Text = "Stopped"
    End Sub

    'TOO STRONG
    Private Function RandomString(rand As Random, length As Integer) As String
        'Dim random = rand
        Return New String(Enumerable.Repeat(chars, length).[Select](Function(s) s(rand.[Next](s.Length))).ToArray())
    End Function

    Private Sub CB_MachList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_MachList.SelectedIndexChanged
        NUD_MaxCONTime.Value = machlist(CB_MachList.SelectedIndex).MaxCONTime
    End Sub


    Private Sub NUD_Value_changed()
        machlist(CB_MachList.SelectedIndex).MaxCONTime = NUD_MaxCONTime.Value
    End Sub



    Private Sub NUD_MaxCONTime_ValueChanged(sender As Object, e As EventArgs) Handles NUD_MaxCONTime.ValueChanged
        NUD_Value_changed()
    End Sub

    Private Sub BTN_Save_Click(sender As Object, e As EventArgs) Handles BTN_Save.Click
        'Does nothing except removing focus from the NUD control
        'changing focus triggers the ValueChanged of the NUD control
    End Sub
End Class

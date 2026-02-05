Imports System.IO
Imports System.Net
Imports System.Text

Public Class EnetAPI

    Public Shared Function LoadMachineList(enetpath As String, Optional lockedmachine As String = "All") As List(Of MachineConfig)
        'Read eHUBConf and MonSetup
        'read ehubconf
        Dim ehubfile As String = enetpath & "\_SETUP\eHUBConf.sys"
        Dim monsetupfile As String = enetpath & "\_SETUP\MonSetup.sys"
            Dim machlist As New List(Of MachineConfig)

            If (File.Exists(ehubfile) And File.Exists(monsetupfile)) Then
                'split per machine section
                'parse each section to find FI:1
                'if found, add machine to list
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


                'Dim ehubsections = ehubtext.Split(Environment.NewLine & Environment.NewLine, StringSplitOptions.None)

                For Each section In ehubsections
                    If (section.IndexOf("FI:1") > 0) Then
                        Dim machname As String = GetEnetValue(section, "NM:")
                        Dim machpos As String = section.Trim.Substring(0, section.IndexOf(":") - 2)
                        Dim sendfolder As String = GetEnetValue(section, "DT:")
                        Dim receivefolder As String = GetEnetValue(section, "DR:")
                        If (lockedmachine = "All") Then
                            machlist.Add(New MachineConfig(machname, machpos, False, sendfolder, receivefolder))
                        ElseIf (lockedmachine = machname) Then
                            machlist.Add(New MachineConfig(machname, machpos, False, sendfolder, receivefolder))
                        End If

                    End If
                Next

                'read monsetup and split per machine section
                'for each section
                'match machpos
                'Dim monsetuptext As String = File.ReadAllText(monsetupfile)
                Dim monsetupsections As New List(Of String) 'monsetupfile.Split(Environment.NewLine)
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
                fs.Close()
            End Using
            'New Code \By Bhavik To Filter New Version of ENET MonsetUp File
            Dim ListWithNoSpace As String = String.Empty
            For Each lines As String In monsetupsections
                lines.Replace(vbCrLf & vbCrLf, vbCrLf)
                ListWithNoSpace = ListWithNoSpace & lines
            Next
            monsetupsections.Clear()
            For I As Integer = 1 To 16
                For J As Integer = 1 To 8
                    monsetupsections.Add(GetEnetMachinesByFilter(ListWithNoSpace, Convert.ToString(I) & "," & Convert.ToString(J), Convert.ToString(I) & "," & Convert.ToString(J + 1)))
                Next
            Next
            For Each mach In machlist
                    For Each section In monsetupsections
                    Dim sectionpos As String = section.Trim.Substring(0, section.IndexOf(":"))
                    If (sectionpos = mach.EnetPos) Then
                            'Dim cmd_dprod As String = GetEnetValue(section, "DD:").Split(",")(0)
                            Dim two_heads As Boolean = If(GetEnetValue(section, "TH:") = "0", False, True)
                            Dim ftpfilename As String = GetEnetValue(section, "DD:").Split(",")(1)
                            Dim cmd_partno As String = GetEnetValue(section, "N1:")
                            Dim cmd_partno2 As String = GetEnetValue(section, "PI:")
                            Dim cmd_CON As String = GetEnetValue(section, "PS:")
                            Dim cmd_COFF As String = GetEnetValue(section, "DE:")
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
                            Dim others As New Dictionary(Of String, String)
                            Dim cnt_others As Integer = If(name_others.Count <= cmd_others.Count, name_others.Count, cmd_others.Count)
                            For i = 0 To cnt_others - 1
                                others.Add(name_others(i).Trim, cmd_others(i).Trim)
                            Next

                            mach.TwoHeads = two_heads
                            mach.FTPFileName = ftpfilename
                            mach.Cmd_PARTNO = cmd_partno
                            mach.Cmd_CON = cmd_CON
                            mach.Cmd_COFF = cmd_COFF
                            mach.Cmd_PARTNO2 = cmd_partno2
                            mach.Cmd_CON2 = cmd_CON2
                            mach.Cmd_COFF2 = cmd_COFF2
                            mach.Cmd_PROD = cmd_prod
                            mach.Cmd_SETUP = cmd_setup
                            mach.Cmd_OTHERS = others


                        End If
                    Next
                Next


                'CB_MachList.DataSource = machlist
                'CB_MachList.DisplayMember = "MachineName"

                'Else
                '   MessageBox.Show("Enet is missing configuration files eHUBCONF.sys or MonSetup.sys")
            End If

            Return machlist
            End Function


    Private Shared Function GetEnetMachinesByFilter(section As String, startparameter As String, endparameter As String) As String
        Dim value As String = ""

        Dim startpos = section.IndexOf(startparameter) + startparameter.Length
        Dim endpos = section.IndexOf(endparameter)
        If endpos = -1 Then
            Dim endparaupdate = endparameter.Split(",")
            Dim newfirstdigit = Convert.ToString(Convert.ToInt32(endparaupdate(0)) + 1)
            Dim newendparameter = newfirstdigit & ",1"
            endpos = section.IndexOf(newendparameter)
        End If
        If endpos = -1 Then
            If startpos - 4 < 0 Then
                value = section.Substring(startpos - 3).Trim
            Else
                value = section.Substring(startpos - 4).Trim
            End If
        Else
            If startpos - 4 < 0 Then
                value = section.Substring(startpos - 3, (endpos - startpos) + 3).Trim
            Else
                value = section.Substring(startpos - 4, (endpos - startpos) + 4).Trim
            End If
        End If

        Return value
    End Function

    Private Shared Function GetEnetValue(section As String, parameter As String) As String
        Dim value As String = ""


        Dim startpos = section.IndexOf(parameter) + parameter.Length
        Dim endpos = section.IndexOf(Environment.NewLine, section.IndexOf(parameter) + 1)

        value = section.Substring(startpos, endpos - startpos)

        Return value
    End Function

    Private Shared Function GetEnetMultiValue(section As String, startparameter As String, endparameter As String) As String
        Dim value As String = ""

        Dim startpos = section.IndexOf(startparameter) + startparameter.Length
        Dim endpos = section.IndexOf(endparameter)

        value = section.Substring(startpos, endpos - startpos).Trim

        Return value
    End Function

    Public Shared Sub SendCMDEnetFTP(ip As String, pwd As String, filename As String, machinename As String, command As String)
        Try
            Dim connection As String = "ftp://" & ip & "/" & filename
            Dim request As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(connection), System.Net.FtpWebRequest)
            request.Credentials = New System.Net.NetworkCredential(machinename, pwd)
            request.UsePassive = False
            request.KeepAlive = False
            request.Method = System.Net.WebRequestMethods.Ftp.UploadFile
            Dim filepath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & filename
            File.WriteAllText(filepath, command)
            Dim file_b() As Byte = System.IO.File.ReadAllBytes(filepath)
            Dim strz As System.IO.Stream = request.GetRequestStream()
            strz.Write(file_b, 0, file_b.Length)
            strz.Close()
            strz.Dispose()
            'Dim ftpResponse As FtpWebResponse = request.GetResponse()
            'MessageBox.Show("FTP Command Send Successfully For Machine :" & machinename & Environment.NewLine & " Command sent is " & command & Environment.NewLine & " Status : " & ftpResponse.StatusDescription & Environment.NewLine & " StatusCode : " & ftpResponse.StatusCode & Environment.NewLine & " Response URI : " & ftpResponse.ResponseUri.ToString() & Environment.NewLine & " ExitMessage : " & ftpResponse.ExitMessage)
            'ftpResponse.Close()
            'ftpResponse.Dispose()
        Catch ex As Exception
            MessageBox.Show("Error :" & ex.Message)
        End Try
    End Sub

    Private Shared Function GetBytes(str As String) As Byte()
        'Dim bytes As Byte() = New Byte(str.Length * 2 - 1) {}
        'System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length)
        Dim bytes As Byte() ' = New Byte(str.Length - 1) {}
        bytes = Encoding.ASCII.GetBytes(str)
        'System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length)
        Return bytes
    End Function

    Private Shared Function GetString(bytes As Byte()) As String
        Dim chars As Char() = New Char(bytes.Length / 2 - 1) {}
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length)
        Return New String(chars)
    End Function

End Class

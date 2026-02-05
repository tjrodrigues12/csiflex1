Imports System.IO
Imports CSI_Library.RainMeterAPI
Imports MySql.Data.MySqlClient
Public Class RMConfig

    Dim csilib As New CSI_Library.CSI_Library(False)
    Dim port_ As String = "8008"

    Private Sub RMConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        csilib = New CSI_Library.CSI_Library(False)

        csilib.connectionString = CSIFLEXSettings.Instance.ConnectionString

        Try
            If File.Exists((CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys")) Then

                Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys")
                    'TB_Port.Text = reader.ReadLine.ToString()
                    'port_ = TB_Port.Text

                    port_ = reader.ReadLine.ToString()
                    reader.Close()
                End Using

            End If
        Catch ex As Exception
            MsgBox("Could not load the Rainmeter port")
            csilib.LogClientError(ex.Message)
        End Try


        'CheckRMInstalled()
        StartRainmeter()

    End Sub

    'Private Sub CheckRMInstalled()
    '    Try
    '        If Not (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Rainmeter")) Then
    '            Dim res As DialogResult = MessageBox.Show("Rainmeter needs to be installed if you want to continue. Do you want to install Rainmeter?", "Rainmeter", MessageBoxButtons.YesNo, MessageBoxIcon.Hand)
    '            If (res = Windows.Forms.DialogResult.Yes) Then
    '                InstallRMStandard()
    '                ConfigureRMFirstStart()
    '            Else
    '                Dispose()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("Error trying to install Rainmeter. Please install it manually.") ' + ex.Message)
    '        Dispose()
    '    End Try

    'End Sub

    'Private Sub ConfigureRMFirstStart()
    '    Try
    '        ConfigureFirstStart()
    '    Catch ex As Exception
    '        MessageBox.Show("Unable to configure Rainmeter") ' + ex.Message)
    '        Dispose()
    '    End Try
    'End Sub
    Dim db_authPath As String = Nothing
    Dim RM_port_ As String = ""



    Private Sub StartRainmeter()
        If (StartRM()) Then
            'Load machines for user
            Try
                'If (csilib.CheckLic(2) = 3) Then
                Dim machlist As New List(Of String)
                If csilib.CheckLic(2) = 3 Then 'If CSI_Lib.isClientSQlite Then


                    If (File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
                        Using reader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")
                            db_authPath = reader.ReadLine()
                        End Using
                    End If
                    Dim connectionString As String
                    connectionString = "SERVER=" + db_authPath + ";" + "DATABASE=csi_database;" + CSI_Library.CSI_Library.MySqlServerBaseString '"DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";"


                    machlist = GetExternalSrcMachineListClient(connectionString)
                ElseIf csilib.CheckLic(2) = 2 Then
                    machlist = GetExternalSrcMachineListSTD()
                Else
                    MessageBox.Show("You must upgrade your license to use this feature")
                    Dispose()
                End If

                For Each mach In machlist
                    CLB_RMMachines.Items.Add(mach)
                Next

                Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Rainmeter\Rainmeter.ini"
                Dim filetext = File.ReadAllText(path)
                Dim index As Integer = 0
                RemoveHandler CLB_RMMachines.ItemCheck, AddressOf CLB_RMMachines_ItemCheck
                For Each mach In machlist
                    If (filetext.IndexOf("[CSIFlex\" & mach & "]" & Environment.NewLine & "Active=1") > 0) Then
                        CLB_RMMachines.SetItemChecked(index, True)
                    End If
                    index += 1
                Next
                AddHandler CLB_RMMachines.ItemCheck, AddressOf CLB_RMMachines_ItemCheck

                'Dim skinlist = GetActiveCSISkin()

            Catch ex As Exception
                MessageBox.Show("Error loading configuration")

                csilib.LogClientError("Error loading configuration:" + ex.Message)
                Dispose()
            End Try

        Else
            MessageBox.Show("Unable to start Rainmeter. Make sure you have installed and updated Rainmeter to version 3.3 or higher. You can download Rainmeter from www.rainmeter.net", "Rainmeter not found", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Dispose()
        End If
    End Sub

    Private Function GetExternalSrcMachineListClient(mysqlcntstr As String) As List(Of String)
        Dim res As New List(Of String)

        Try
            Dim mySqlCnt As New MySqlConnection

            mySqlCnt = New MySqlConnection(mysqlcntstr) 'CSI_Library.CSI_Library.MySqlConnectionString)

            mySqlCnt.Open()

            ' Dim mysql23 As String = "SELECT machines from csi_auth.users where username_ = '" + CSI_Library.CSI_Library.username + "'"

            Dim mysql23 As String = "SELECT machines from csi_auth.users where username_ = '" + Reporting_application.username_ + "'"


            Dim cmdCreateDeviceTable23 As New MySqlCommand(mysql23, mySqlCnt)
            Dim mysqlReader23 As MySqlDataReader = cmdCreateDeviceTable23.ExecuteReader
            Dim dTable_message23 As New DataTable()
            dTable_message23.Load(mysqlReader23)
            If (dTable_message23.Rows.Count >= 1) Then
                Dim tempres As List(Of String) = dTable_message23.Rows(0)(0).ToString().Split(",").ToList()
                For Each tmach As String In tempres
                    tmach = tmach.Trim
                    If (tmach.Length > 0) Then
                        res.Add(tmach)
                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show("Unable to load the machines for the current user") ' & ex.Message)
        End Try

        Return res
    End Function

    Private Function GetExternalSrcMachineListSTD() As List(Of String)
        Dim res As New List(Of String)

        Try
            Using file As New StreamReader(csilib.eNET_path + "\_SETUP\MonList.sys")

                While Not file.EndOfStream
                    Dim machine As String = file.ReadLine

                    If machine.StartsWith("_MT_") Or machine.StartsWith("_ST_") Then
                        'nothing
                    Else
                        res.Add(machine.Trim)
                    End If
                End While
            End Using
        Catch ex As Exception
            MessageBox.Show("Unable to load the machines") ' & ex.Message)
        End Try

        Return res
    End Function

    Private Sub Done_Click(sender As Object, e As EventArgs) Handles Done.Click
        Try
            'If port_ <> TB_Port.Text Then
            '    port_ = TB_Port.Text
            If File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys") Then
                File.Delete(CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys")
            End If

            'Using writer As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
            '    writer.Write(TB_Port.Text)
            '    writer.Close()
            'End Using

            Dim db_authPath As String
            If (File.Exists(CSI_Library.CSI_Library.ClientRootPath & "\sys\SrvDBpath.csys")) Then
                Using streader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath + "\sys\SrvDBpath.csys")
                    db_authPath = streader.ReadLine()
                End Using
            End If

            Dim cnt As MySqlConnection = New MySqlConnection("SERVER=" + db_authPath + ";" + "DATABASE=csi_auth;" + CSI_Library.CSI_Library.MySqlServerBaseString)
            cnt.Open()
            If cnt.State = ConnectionState.Open Then


                Dim query As String = "select port from  CSI_database.tbl_RM_Port"

                Dim cmd As New MySqlCommand(query, cnt)
                Dim dtrdr As MySqlDataReader


                dtrdr = cmd.ExecuteReader
                dtrdr.Read()
                port_ = dtrdr.Item(0)
                cnt.Close()
            End If

            Using writer As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.ClientRootPath & "\sys\RM_port_.csys")
                writer.Write(port_)
                writer.Close()
            End Using

            MsgBox(" CSIFlex will restart rainmeter")
            Me.Cursor = Cursors.WaitCursor
            SetupForm.Cursor = Cursors.WaitCursor
            '   restart_service()
            QuitRM()

            Dim tasks As New List(Of Task)()

            Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Rainmeter\Skins\CSIFlex\"
            Dim fld() As String = Directory.GetDirectories(path)

            For Each folder In (fld)
                Dim t As New Task(Sub()
                                      Dim info_ As New DirectoryInfo(folder)
                                      If info_.Name <> "@Resources" Then
                                          RemoveSkin(info_.Name)

                                          If (File.Exists(CSI_Library.CSI_Library.ClientRootPath + "/sys/SrvDBpath.csys")) Then
                                              Using reader As New StreamReader(CSI_Library.CSI_Library.ClientRootPath + "/sys/SrvDBpath.csys")
                                                  db_authPath = reader.ReadLine()
                                              End Using
                                          End If

                                          CreateMachineSkin(0, info_.Name, db_authPath & ":" & port_)
                                      End If
                                  End Sub)
                tasks.Add(t)
                t.Start()
            Next
            Task.WaitAll(tasks.ToArray)

            StartRM()










            'Else

            'End If

        Catch ex As Exception
            MsgBox("Could not save the port")
            csilib.LogClientError(ex.Message)
        End Try


        Me.Cursor = Cursors.Default
        SetupForm.Cursor = Cursors.Default

        Dispose()
    End Sub


    Private Sub CLB_RMMachines_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CLB_RMMachines.ItemCheck
        Dim machname As String = CLB_RMMachines.SelectedItem.ToString()
        'reverse if because the check happens after the event
        'using selectedindexchanged event wasn't working well
        If Not (CLB_RMMachines.GetItemChecked(CLB_RMMachines.SelectedIndex)) Then
            CreateMachineSkin(0, machname, db_authPath & ":" & Get_RM_PORT(CSI_Library.CSI_Library.ClientRootPath))
        Else
            RemoveSkin(machname)
        End If
    End Sub

End Class
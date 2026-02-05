Imports System.IO
Imports System.Threading
Imports CSI_Library.RainMeterAPI
Imports CSIFLEX.Database.Access
Imports CSIFLEX.Server.Settings
Imports CSIFLEX.Utilities
Imports MySql.Data.MySqlClient

Public Class RMConfig

    Public machlist = GetExternalSrcMachineList(CSI_Library.CSI_Library.MySqlConnectionString)

    Dim mySqlCnt As New MySqlConnection
    Dim cnt As MySqlConnection = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
    Public port_ As String

    Dim enetMachines As Dictionary(Of String, String)


    Private Sub RMConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If File.Exists((CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")) Then

                Using reader As StreamReader = New StreamReader(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                    TB_Port.Text = reader.ReadLine.ToString()
                    'ALlo
                    port_ = TB_Port.Text
                    reader.Close()
                End Using

            End If
        Catch ex As Exception
            MsgBox("Could not load the Rainmeter port")
            CSI_Lib.LogServerError(ex.Message, 1)
        End Try


        StartRainmeter()

    End Sub

    'Private Sub CheckRMInstalled()
    '    Try
    '        If Not (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Rainmeter")) Then
    '            Dim res As DialogResult = MessageBox.Show("Rainmeter needs to be installed if you want to continue. Do you want to install Rainmeter?", "Rainmeter", MessageBoxButtons.YesNo, MessageBoxIcon.Hand)
    '            If (res = Windows.Forms.DialogResult.Yes) Then
    '                InstallRMAsAdmin()
    '                ConfigureRMFirstStart()
    '            Else
    '                Dispose()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("Error trying to install Rainmeter:" + ex.Message)
    '        Dispose()
    '    End Try

    'End Sub

    'Private Sub ConfigureRMFirstStart()
    '    Try
    '        ConfigureFirstStart()
    '    Catch ex As Exception
    '        MessageBox.Show("Unable to configure Rainmeter:" + ex.Message)
    '        Dispose()
    '    End Try
    'End Sub

    Private Sub StartRainmeter()

        If (StartRM()) Then
            'Load machines for user
            Try
                For Each mach In machlist
                    clbMachines.Items.Add(mach)
                Next

                Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Rainmeter\Rainmeter.ini"
                Dim filetext = File.ReadAllText(path)
                Dim index As Integer = 0

                RemoveHandler clbMachines.ItemCheck, AddressOf clbMachines_ItemCheck
                For Each mach In machlist
                    If (filetext.IndexOf("[CSIFlex\" & mach & "]" & Environment.NewLine & "Active=1") > 0) Then
                        clbMachines.SetItemChecked(index, True)
                    End If
                    index += 1
                Next
                AddHandler clbMachines.ItemCheck, AddressOf clbMachines_ItemCheck

                'Dim skinlist = GetActiveCSISkin()

            Catch ex As Exception
                MessageBox.Show("Error loading configuration")
                Log.Error("Error loading configuration:" + ex.Message, ex)
                Dispose()
            End Try

        Else

            'For the first time install Rainmeter
            Dim result = MessageBox.Show("Do you want to install Rainmeter ?", "Install Rainmeter", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                Process.Start("C:\Program Files (x86)\CSI Flex Server\tools\Rainmeter-4.1.exe")
            ElseIf result = DialogResult.No Then
                Me.Close()
            End If

            Dispose()

        End If

    End Sub

    Private Function GetExternalSrcMachineList(mysqlcntstr As String) As List(Of String)

        Dim res As New List(Of String)

        Dim dicMachines As Dictionary(Of String, String) = New Dictionary(Of String, String)()
        enetMachines = New Dictionary(Of String, String)()

        Try

            Dim machines = MySqlAccess.GetDataTable("SELECT Id, Machine_name, EnetMachineName FROM csi_auth.tbl_ehub_conf WHERE MonState = 1", mysqlcntstr)

            For Each row As DataRow In machines.Rows
                dicMachines.Add(row("Id"), row("Machine_name"))
                enetMachines.Add(row("Machine_name"), row("EnetMachineName"))
            Next

            Dim userMachines As String = MySqlAccess.ExecuteScalar($"SELECT machines FROM csi_auth.users WHERE username_ = '{CSI_Library.CSI_Library.username}'", mysqlcntstr)

            If String.IsNullOrEmpty(userMachines) Then Return res

            If userMachines.ToUpper() <> "ALL" Then
                For Each machId As String In userMachines.Split(",")
                    If dicMachines.ContainsKey(machId.Trim()) Then
                        res.Add(dicMachines(machId.Trim()))
                    Else
                        res.Add(machId.Trim())
                    End If
                Next
            Else
                For Each mach As KeyValuePair(Of String, String) In dicMachines
                    res.Add(mach.Value)
                Next
            End If


        Catch ex As Exception

            MessageBox.Show("Unable to load the machines: " & ex.Message)

        End Try

        Return res
    End Function

    Private Sub Done_Click(sender As Object, e As EventArgs) Handles Done.Click

        Try
            If port_ <> TB_Port.Text Then
                port_ = TB_Port.Text
                If File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys") Then
                    File.Delete(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                End If

                Using writer As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\RM_port_.csys")
                    writer.Write(TB_Port.Text)
                    writer.Close()
                End Using


                cnt.Open()
                If cnt.State = ConnectionState.Open Then
                    Dim cmdCREATTION As New MySqlCommand("CREATE TABLE if not exists CSI_Database.tbl_RM_Port (port integer);", cnt)
                    Dim cmd_result As Integer
                    cmd_result = cmdCREATTION.ExecuteNonQuery()

                    Dim query As String = "UPDATE CSI_database.tbl_RM_Port  SET " +
                                                                             " port = @port_"

                    Dim cmd As New MySqlCommand(query, cnt)
                    cmd.Parameters.AddWithValue("@port_", CInt(TB_Port.Text))

                    If (cmd.ExecuteNonQuery() = 0) Then
                        cmd.CommandText = "INSERT INTO CSI_database.tbl_RM_Port " +
                                                    "(port) " + "VALUES (@port_)"
                        cmd.ExecuteNonQuery()
                    End If
                    'cnt.Close()


                End If
                MsgBox("The port has been updated, CSIFlex will restart the service and rainmeter")
                Me.Cursor = Cursors.WaitCursor
                SetupForm2.Cursor = Cursors.WaitCursor
                restart_service()
                'The below code is new one
                'FocasLibrary.Tools.AdapterManagement.Restart("CSIFlexServerService")
                QuitRM()

                Dim tasks As New List(Of Task)()

                Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Rainmeter\Skins\CSIFlex\"
                Dim fld() As String = Directory.GetDirectories(path)

                For Each folder In (fld)
                    Dim t As New Task(Sub()
                                          Dim info_ As New DirectoryInfo(folder)
                                          If info_.Name <> "@Resources" Then
                                              RemoveSkin(info_.Name)
                                              CreateMachineSkin(0, info_.Name, SetupForm2.GetIPv4Address() & ":" & port_)
                                          End If
                                      End Sub)
                    tasks.Add(t)
                    t.Start()
                Next
                Task.WaitAll(tasks.ToArray)

                StartRM()










            Else

            End If

        Catch ex As Exception
            MsgBox("Could not save the port")
            CSI_Lib.LogServerError(ex.Message, 1)
        Finally
            cnt.Close()
        End Try


        Me.Cursor = Cursors.Default
        SetupForm2.Cursor = Cursors.Default
        Dispose()



    End Sub

    Private Sub restart_service()

        SetupForm2.btnStopService_Click(Me, System.EventArgs.Empty)
        While (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") <> ServiceTools.ServiceState.Stop)
            Thread.Sleep(1000)
        End While

        SetupForm2.lblServiceState.Text = "Stopped"
        SetupForm2.lblServiceState.BackColor = Color.Red


        SetupForm2.btnStartService_Click(Me, System.EventArgs.Empty)
        While (ServiceTools.ServiceInstaller.GetServiceStatus("CSIFlexServerService") <> ServiceTools.ServiceState.Run)
            Thread.Sleep(1000)
        End While
        SetupForm2.lblServiceState.Text = "Running"
        SetupForm2.lblServiceState.BackColor = Color.LimeGreen
        'SetupForm2.LBL_ServiceState.BackColor = Color.Green

    End Sub


    Private Sub reload_skins()
        Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\Rainmeter\Skins\CSIFlex\"
        Dim fld() As String = Directory.GetDirectories(path)

        For Each folder In fld

            If folder <> "@Resources" Then
                RemoveSkin(folder)

                CreateMachineSkin(0, folder, SetupForm2.GetIPv4Address() & ":" & port_)
            End If

        Next
    End Sub


    Private Sub clbMachines_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbMachines.ItemCheck

        Dim machName As String = clbMachines.SelectedItem.ToString()
        'reverse if because the check happens after the event
        'using selectedindexchanged event wasn't working well

        If Not (clbMachines.GetItemChecked(clbMachines.SelectedIndex)) Then

            Dim ip_ As String = SetupForm2.GetIPv4Address()

            ip_ = ServerSettings.ServerIPAddress

            CreateMachineSkin(enetMachines(machName), machName, ip_ & ":" & Get_RM_PORT(CSI_Library.CSI_Library.serverRootPath))

        Else
            RemoveSkin(machName)
        End If

    End Sub

    Private Sub TB_Port_TextChanged(sender As Object, e As EventArgs) Handles TB_Port.TextChanged
        If Not IsNumeric(TB_Port.Text) Then
            MsgBox("A port is a numeric value only")
        End If
    End Sub

End Class
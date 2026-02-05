Imports System.IO
Imports EnetStatusChanger.EnetAPI

Public Class EnetStatusChanger

    'Dim FTPFILENAME As String = "CSI.FTP"
    Dim CONFIGFILENAME As String = "csiconfig.ini"

    Dim enetftpip As String = "0.0.0.0"
    Dim enetftppwd As String = ""
    Dim enetpath As String = "C:\_eNETDNC"
    Dim machlist As New List(Of MachineConfig)
    Dim lockedmachine As String = ""

    Private Sub EnetStatusChanger_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Check config file
            If Not (CheckConfig(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\" & CONFIGFILENAME)) Then
                CreateConfig()
                If Not (CheckConfig(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\" & CONFIGFILENAME)) Then
                    MessageBox.Show("There is an error with your configuration file. The software will exit.", "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Dispose()
                End If
            End If
            Dim path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\" & CONFIGFILENAME
            'if not exist, ask and save
            'load enet config (ftp machine+status)
            machlist = LoadMachineList(enetpath, lockedmachine)

            If (machlist.Count > 0) Then
                CB_MachList.DataSource = machlist
                CB_MachList.DisplayMember = "MachineName"
            Else
                CB_MachList.DataSource = Nothing
                CB_MachList.DisplayMember = ""
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading the software" & ex.StackTrace.ToString())
            If File.Exists("C:\EnetStatusChangerLog.txt") Then
                File.Delete("C:\EnetStatusChangerLog.txt")
                IO.File.AppendAllText("C:\EnetStatusChangerLog.txt", String.Format("{0}{1}", Environment.NewLine, ex.StackTrace.ToString()))
            End If
            'Environment.Exit(0)
        End Try
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
                ElseIf (line.StartsWith("LOCKEDMACHINE:")) Then
                    lockedmachine = line.Substring(line.IndexOf(":") + 1)
                End If
            Next
            config = True
        End If

        Return config

    End Function

    Private Sub CreateConfig()
        Dim frm_Config As New Config
        frm_Config.ShowDialog()
    End Sub


    Private Function CheckConnInfo() As Boolean
        Dim res As Boolean = False

        If (enetftpip.Length > 0) And (machlist(CB_MachList.SelectedIndex).FTPFileName.Length > 0) Then
            res = True
        End If


        If (res = False) Then
            MessageBox.Show("You need to specify the connection information to enet's FTP server.", "Connection info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If
        Return res
    End Function

    Private Sub BTN_SetPartNo_Click(sender As Object, e As EventArgs) Handles BTN_SetPartNo.Click
        Dim cmd As String = machlist(CB_MachList.SelectedIndex).Cmd_PARTNO
        Dim head As Integer = CB_Heads.SelectedIndex

        If (head = 1) Then
            cmd = machlist(CB_MachList.SelectedIndex).Cmd_PARTNO2
        End If

        If (CheckConnInfo() And CB_MachList.SelectedIndex >= 0) Then
            SendCMDEnetFTP(enetftpip, enetftppwd, machlist(CB_MachList.SelectedIndex).FTPFileName, machlist(CB_MachList.SelectedIndex).MachineName, cmd & TB_PartNo.Text)
            ClearForm()
        End If
    End Sub

    Private Sub BTN_ChangeStatus_Click(sender As Object, e As EventArgs) Handles BTN_ChangeStatus.Click
        Dim cmd As String = CB_Status.SelectedValue

        Dim head As Integer = CB_Heads.SelectedIndex

        If (head = 1 And CB_Status.SelectedIndex < 2) Then
            If (CB_Status.SelectedIndex = 0) Then
                cmd = machlist(CB_MachList.SelectedIndex).Cmd_CON2
            ElseIf (CB_Status.SelectedIndex = 1) Then
                cmd = machlist(CB_MachList.SelectedIndex).Cmd_COFF2
            End If
        End If

        If (CheckConnInfo() And CB_MachList.SelectedIndex >= 0) Then
            SendCMDEnetFTP(enetftpip, enetftppwd, machlist(CB_MachList.SelectedIndex).FTPFileName, machlist(CB_MachList.SelectedIndex).MachineName, cmd)
            ClearForm()
        End If

    End Sub

    Private Sub ClearForm()
        TB_PartNo.Clear()
        'CB_MachList.SelectedIndex = -1
        'CB_Heads.SelectedIndex = 0
        CB_Status.SelectedIndex = 0
    End Sub



    Private Sub CB_MachList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_MachList.SelectedIndexChanged
        'Dim machname As String = CB_MachList.SelectedItem

        Dim status As New Dictionary(Of String, String)
        status.Add("CYCLE ON", machlist(CB_MachList.SelectedIndex).Cmd_CON)
        status.Add("CYCLE OFF", machlist(CB_MachList.SelectedIndex).Cmd_COFF)
        status.Add("PRODUCTION", machlist(CB_MachList.SelectedIndex).Cmd_PROD)
        status.Add("SETUP", machlist(CB_MachList.SelectedIndex).Cmd_SETUP)

        For Each kvp As KeyValuePair(Of String, String) In machlist(CB_MachList.SelectedIndex).Cmd_OTHERS
            status.Add(kvp.Key, kvp.Value)
        Next

        CB_Status.DataSource = New BindingSource(status, Nothing)
        CB_Status.DisplayMember = "Key"
        CB_Status.ValueMember = "Value"

        CB_Heads.SelectedIndex = 0
        If (machlist(CB_MachList.SelectedIndex).TwoHeads) Then
            CB_Heads.Enabled = True
        Else
            CB_Heads.Enabled = False
        End If

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class

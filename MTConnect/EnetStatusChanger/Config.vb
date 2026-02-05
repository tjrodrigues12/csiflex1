Imports System.IO
Imports EnetStatusChanger.EnetAPI
Public Class Config

    'Dim FTPFILENAME As String = "CSI.FTP"
    Dim CONFIGFILENAME As String = "csiconfig.ini"
    Dim machlist As New List(Of MachineConfig)

    Private Sub BTN_Save_Click(sender As Object, e As EventArgs) Handles BTN_Save.Click
        Try
            If (CheckConfig()) Then
                Dim filename As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\" & CONFIGFILENAME
                Dim lines(3) As String

                lines(0) = "ENETFTPIP:" & TB_IP.Text
                lines(1) = "ENETFTPPWD:" & TB_Pwd.Text
                lines(2) = "ENETPATH:" & TB_Path.Text
                lines(3) = "LOCKEDMACHINE:" & machlist(CB_MachList.SelectedIndex).MachineName

                File.WriteAllLines(filename, lines)

                Dispose()
            Else
                MessageBox.Show("Please input valid IP and path to enet folder", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            MessageBox.Show("Error writing configuration file" & ex.Message)

        End Try
    End Sub

    Private Function CheckConfig() As Boolean
        Dim config As Boolean = False

        If (TB_IP.Text.Length > 0) And (TB_Path.Text.Length > 0) Then
            config = True
        End If

        Return config
    End Function

    Private Sub BTN_EnetPathBrowse_Click(sender As Object, e As EventArgs) Handles BTN_EnetPathBrowse.Click
        Try
            Dim fbd As New FolderBrowserDialog
            fbd.SelectedPath = "C:\_eNETDNC" 'default PAth for ENET
            fbd.ShowNewFolderButton = False

            If (fbd.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                TB_Path.Text = fbd.SelectedPath
                'Else
                '    TB_Path.Text = "\\10.0.10.189\c\_eNETDNC" 'Changed because network settings are not set to my local PC
            End If

            machlist = LoadMachineList(TB_Path.Text)
            If (machlist.Count > 0) Then
                machlist.Insert(0, New MachineConfig("All", "0", False, "", ""))
                CB_MachList.DataSource = machlist
                CB_MachList.DisplayMember = "MachineName"
            Else
                CB_MachList.DataSource = Nothing
                CB_MachList.DisplayMember = ""
            End If
        Catch ex As Exception
            MessageBox.Show("Eroor in loading MachineList :" & ex.Message() & " StakeTrace : " & ex.StackTrace())
        End Try


    End Sub

    Private Sub CB_MachList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_MachList.SelectedIndexChanged
        If (CB_MachList.SelectedIndex >= 0) Then
            BTN_Save.Enabled = True
        Else
            BTN_Save.Enabled = False
        End If
    End Sub

    Private Sub Config_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
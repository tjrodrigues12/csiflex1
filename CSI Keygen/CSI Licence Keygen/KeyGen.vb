'Imports System.Security.Cryptography
'Imports System.Management
Imports System.IO
'Imports System.Text
'Imports System.Runtime.InteropServices


'http://msdn.microsoft.com/en-us/library/windows/desktop/aa389273(v=vs.85).aspx


Public Class KeyGen

    Dim key As LicenseKey
    Dim keyPath As String

    Private Sub keygen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub




    Private Sub CreateKey()
        Dim browser As New SaveFileDialog()
        'browser.ShowDialog()
        browser.Filter = "dl1 files (*.dl1)|*.dl1"
        'browser.FilterIndex = 2
        'browser.RestoreDirectory = True

        If browser.ShowDialog() = DialogResult.OK Then

            Try
                Using writer As StreamWriter = New StreamWriter(browser.FileName, False)
                    writer.Write(key.GetKey)
                    writer.Close()
                End Using
            Catch ex As Exception
                MessageBox.Show("Unable to save the license : " & ex.Message)
            End Try
        End If
        
    End Sub

    'Private Sub writeKey()
    '    Try
    '        Using writer As StreamWriter = New StreamWriter(KeyPath, False)
    '            writer.Write(key.GetKey)
    '            writer.Close()
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show("Unable to read the cryptedKey version : " & ex.Message)
    '    End Try
    'End Sub

    

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Environment.Exit(0)
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        CreateKey()

        CB_Version.SelectedIndex = -1
        DTP_ExpirationDate.Value = Date.Now
        TB_Key.Text = ""

        GB_LicenseInfos.Enabled = False
        SaveToolStripMenuItem.Enabled = False
        key = Nothing
    End Sub

    Private Sub LicenseFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LicenseFileToolStripMenuItem.Click

        'Dim keypath As String
        Dim encryptedKey As String
        'Dim tempTab() As String
        OpenFileDialog_KeyGen.Filter = "dl1 files (*.dl1)|*.dl1"

        If (OpenFileDialog_KeyGen.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            keyPath = OpenFileDialog_KeyGen.FileName

            Try
                Using r As StreamReader = New StreamReader(KeyPath, False)
                    encryptedKey = r.ReadLine
                    '                    tempTab = tempDecryptedKey.Split(";")
                    '                   decryptedKey = tempTab(0) + ";" + tempTab(1) + ";exp:"

                    r.Close()
                    'keyOK = True

                    key = New LicenseKey(encryptedKey, False)
                    GB_LicenseInfos.Enabled = True
                    SaveToolStripMenuItem.Enabled = True
                    DisplayKeyInfo()
                End Using

            Catch ex As Exception
                MessageBox.Show("Unable to read the cryptedKey : " & ex.Message)
                'keyOK = False
            End Try
        End If

    End Sub


    Private Sub DisplayKeyInfo()
        TB_Key.Text = key.GetKey
        DTP_ExpirationDate.Value = key.GetExpirationDate
        CB_Version.SelectedItem = CreateCBVersionString(key.GetVersion)
    End Sub

    Private Function CreateCBVersionString(version As Integer) As String
        Dim version_str As String = "1-Lite"

        If (version = 0) Then
            version_str = "0-Server"
        ElseIf (version = 1) Then
            version_str = "1-Lite"
        ElseIf (version = 2) Then
            version_str = "2-Standard"
        ElseIf (version = 3) Then
            version_str = "3-ClientServer"
        End If

        Return version_str
    End Function

    Private Function GetCBVersion() As Integer
        Dim version As Integer = 1

        If (CB_Version.SelectedItem = "0-Server") Then
            version = 0
        ElseIf (CB_Version.SelectedItem = "1-Lite") Then
            version = 1
        ElseIf (CB_Version.SelectedItem = "2-Standard") Then
            version = 2
        ElseIf (CB_Version.SelectedItem = "3-ClientServer") Then
            version = 3
        End If

        Return version
    End Function
    Private Sub OfflineInformationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OfflineInformationToolStripMenuItem.Click
        Dim Offline_form As New OfflineString
        If (Offline_form.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            key = New LicenseKey(Offline_form.offlineInfos, True)
            GB_LicenseInfos.Enabled = True
            SaveToolStripMenuItem.Enabled = True
            DisplayKeyInfo()
        End If
    End Sub

    Private Sub CB_Version_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Version.SelectedIndexChanged
        key.SetVersion(GetCBVersion)
        DisplayKeyInfo()
    End Sub

    Private Sub DTP_ExpirationDate_ValueChanged(sender As Object, e As EventArgs) Handles DTP_ExpirationDate.ValueChanged
        key.SetExpirationDate(DTP_ExpirationDate.Value)
        DisplayKeyInfo()
    End Sub

    Private Sub EncryptedStringToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EncryptedStringToolStripMenuItem.Click
        Dim Encrpted_Form As New EncrytpedString
        If (Encrpted_Form.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            key = New LicenseKey(Encrpted_Form.encryptedString, False)
            GB_LicenseInfos.Enabled = True
            SaveToolStripMenuItem.Enabled = True
            DisplayKeyInfo()
        End If
    End Sub
End Class




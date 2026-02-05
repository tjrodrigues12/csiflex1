Imports System.Security.Cryptography
Imports System.Management
'Imports System.IO
'Imports System.Text
'Imports System.Runtime.InteropServices

Public Class LicenseKey

    Dim _key_encrypted As String
    Dim _key_decrypted As String
    Dim _version As Integer
    Dim _expiration_date As Date
    Dim _cpuid As String
    Dim _hddid As String

    Public Function GetKey() As String
        Return _key_encrypted
    End Function

    Public Function GetVersion() As Integer
        Return _version
    End Function

    Public Sub SetVersion(version As Integer)
        _version = version
        GenerateKey()
    End Sub

    Public Function GetExpirationDate() As Date
        Return _expiration_date
    End Function

    Public Sub SetExpirationDate(exp_date As Date)
        _expiration_date = exp_date
        GenerateKey()
    End Sub

    Public Sub New(encrypted_key As String, partial_infos As Boolean)

        If (partial_infos) Then
            'ParsePartialEncryptedKey(encrypted_key)
            ParsePartialKey(encrypted_key, False)
            GenerateKey()
        Else
            _key_encrypted = encrypted_key
            _key_decrypted = DecryptKey(_key_encrypted)
            ParseDecryptedKey(_key_decrypted)
        End If

    End Sub

    Public Sub New()
        'create local key
    End Sub

    Private Function DecryptKey(key_encrypted As String) As String

        Dim decryptedKey As String = ""
        Try
            decryptedKey = AES_Decrypt(key_encrypted, "license")
        Catch ex As Exception
            MessageBox.Show("Unable to read the cryptedKey version : " & ex.Message)
        End Try

        Return decryptedKey

    End Function


    Private Sub GenerateKey()


        If (_cpuid.Split(":").Length > 1) Then

            _cpuid = _cpuid.Split(":")(1)

        End If

        If (_hddid.Split(":").Length > 1) Then

            _hddid = _hddid.Split(":")(1)

        End If


        _key_decrypted = "cpuid:" + _cpuid + ";hdd:" + _hddid + ";exp:" + _expiration_date.ToShortDateString() + ";version:" + _version.ToString() + ";"

        _key_encrypted = AES_Encrypt(_key_decrypted, "license")
    End Sub

    Private Sub ParseDecryptedKey(key_decrypted As String)

        Dim splittedString() As String

        Try
            splittedString = key_decrypted.Split(";")

            Dim infos As List(Of String) = New List(Of String)

            For Each s As String In splittedString
                If (s.Length > 0) Then
                    infos.Add(s.Substring(s.IndexOf(":") + 1))
                End If
            Next

            If (infos.Count >= 4) Then
                Dim expdate As DateTime = New DateTime
                If (DateTime.TryParse(infos(2), expdate)) Then
                    _expiration_date = expdate
                Else
                    _expiration_date = Date.Now
                End If

                Dim tmpversion As Integer
                If (Integer.TryParse(infos(3), tmpversion)) Then
                    _version = tmpversion
                End If

                _cpuid = infos(0)
                _hddid = infos(1)

            End If
        Catch ex As Exception
            MessageBox.Show("Unable to parse the key : " & ex.Message)
        End Try

    End Sub


    Private Sub ParsePartialKey(partialkey As String, encrypted As Boolean)

        Dim partialkey_decrypted As String
        Dim tempTab() As String

        Try
            If (encrypted) Then
                partialkey_decrypted = DecryptKey(partialkey)
            Else
                partialkey_decrypted = partialkey
            End If

            tempTab = partialkey_decrypted.Split(";")
            _cpuid = tempTab(0)
            _hddid = tempTab(1)

            _expiration_date = Date.Now
            _version = 1

        Catch ex As Exception
            MessageBox.Show("Unable to parse the key : " & ex.Message)
        End Try


    End Sub


#Region "ulitities"

    Private Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            AES.Padding = Security.Cryptography.PaddingMode.Zeros
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    Private Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            AES.Padding = Security.Cryptography.PaddingMode.Zeros
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Return Nothing

        End Try
    End Function


    Private Function GetDriveSerialNumber(ByVal drive As String) As String

        Dim driveSerial As String = String.Empty
        Dim driveFixed As String = System.IO.Path.GetPathRoot(drive)
        driveFixed = Replace(driveFixed, "\", String.Empty)

        Using querySearch As New System.Management.ManagementObjectSearcher("SELECT VolumeSerialNumber FROM Win32_LogicalDisk Where Name = '" & driveFixed & "'")

            Using queryCollection As ManagementObjectCollection = querySearch.Get()

                Dim moItem As ManagementObject

                For Each moItem In queryCollection

                    driveSerial = CStr(moItem.Item("VolumeSerialNumber"))

                    Exit For
                Next
            End Using
        End Using
        Return driveSerial
    End Function

    Private Function GetCPUSerialNumber() As String

        'http://msdn.microsoft.com/en-us/library/windows/desktop/aa394373(v=vs.85).aspx

        Dim cpuSerial As String = String.Empty

        Using querySearch As New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard")

            Using queryCollection As ManagementObjectCollection = querySearch.Get()

                For Each moItem As ManagementObject In queryCollection

                    cpuSerial = CStr(moItem.Item("SerialNumber"))
                    cpuSerial = cpuSerial & " " & CStr(moItem.Item("SerialNumber"))

                    Exit For
                Next
            End Using
        End Using
        Return cpuSerial
    End Function

#End Region

End Class

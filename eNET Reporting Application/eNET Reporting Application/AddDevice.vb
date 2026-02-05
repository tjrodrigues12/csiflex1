Imports CSIFLEX.Database.Access
Imports MySql.Data.MySqlClient

Public Class AddDevice

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        SetupForm2.copy = String.Empty
    End Sub

    Private Function Ping() As Boolean

        Dim res As Boolean = False

        If IP_TB.Text.EndsWith("*") Then
            Return True
        End If

        Try
            'ping ip
            If My.Computer.Network.Ping(IP_TB.Text) Then

                res = True

            End If

        Catch ex As Exception
            res = False
        End Try

        Return res

    End Function

    Private Sub Form_Closing(sender As Object, e As EventArgs) Handles Me.Closed

        SetupForm2.copy = String.Empty
        'Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not (IP_TB.Text = "" Or Name_TB.Text = "") Then

            If IP_TB.Text.Split(".").Count = 0 And IP_TB.Text.Split("-").Count = 0 Then
                MessageBox.Show("You must enter a valide IP/MAC address")
            Else

                If Not (IP_TB.Text = "127.0.0.1") Then

                    If (CB_DeviceType.SelectedItem = "Computer" Or Ping()) Then

                        Dim name As String = Name_TB.Text
                        SetupForm2.treeviewDevices.Nodes(0).Nodes.Add("0", name)

                        Try

                            Dim newDevice As New DashboardDevice()
                            Dim deviceCopyId As Integer = 0

                            If Not SetupForm2.copy = String.Empty Then ' Before It was SetupForm2.copy = ""
                                newDevice.LoadDeviceByName(SetupForm2.copy)
                                deviceCopyId = newDevice.DeviceId
                                newDevice.DeviceId = 0
                            End If

                            newDevice.IpAddress = IP_TB.Text
                            newDevice.DeviceName = Name_TB.Text

                            newDevice.SaveDevice()

                            SetupForm2.treeviewDevices.Nodes(0).Nodes(SetupForm2.treeviewDevices.Nodes(0).Nodes.Count - 1).Name = newDevice.DeviceId

                            Dim dtMessages = MySqlAccess.GetDataTable($"Select message, Priority from CSI_database.tbl_messages WHERE DeviceId = { deviceCopyId }")
                            Dim sqlCmd As New Text.StringBuilder()
                            Dim message = ""
                            Dim priority = ""

                            For Each row As DataRow In dtMessages.Rows

                                message = row("Message").ToString()
                                priority = row("Priority").ToString()

                                sqlCmd.Clear()
                                sqlCmd.Append($"INSERT IGNORE INTO          ")
                                sqlCmd.Append($"  csi_database.tbl_messages ")
                                sqlCmd.Append($"  (                         ")
                                sqlCmd.Append($"     DeviceId  ,            ")
                                sqlCmd.Append($"     Name ,                 ")
                                sqlCmd.Append($"     Message   ,            ")
                                sqlCmd.Append($"     Priority               ")
                                sqlCmd.Append($"  )                         ")
                                sqlCmd.Append($"  VALUES                    ")
                                sqlCmd.Append($"  (                         ")
                                sqlCmd.Append($"      {newDevice.DeviceId}, ")
                                sqlCmd.Append($"     '{Name_TB.Text}'     , ")
                                sqlCmd.Append($"     '{message}'          , ")
                                sqlCmd.Append($"     '{priority}'           ")
                                sqlCmd.Append($"  )                       ; ")

                                MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

                            Next

                        Catch ex As Exception
                            MessageBox.Show("Error :" & ex.Message)
                        End Try

                        SetupForm2.copy = String.Empty
                        SetupForm2.treeviewDevices.ExpandAll()

                        For Each node As TreeNode In SetupForm2.treeviewDevices.Nodes(0).Nodes
                            If node.Text = name Then
                                SetupForm2.treeviewDevices.SelectedNode = node
                            End If
                        Next

                        Me.Close()
                    Else
                        MessageBox.Show("Unable to ping this IP")
                    End If
                Else
                    MessageBox.Show("127.0.0.1 is a loopback address. You can use this IP in your browser but not to configure the server")
                End If
            End If
        Else
            MessageBox.Show("You must enter an IP/MAC and a name")
        End If
    End Sub

    Private Sub AddDevice_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If (CSI_Library.CSI_Library.DuplicateDevice = True) Then
            ' Case :: Duplicate a Device
            If SetupForm2.DeviceType = "Computer/tablet/phone" Then
                CB_DeviceType.SelectedIndex = 1
            ElseIf SetupForm2.DeviceType = "LR1" Then
                CB_DeviceType.SelectedIndex = 0
            End If
            CSI_Library.CSI_Library.DuplicateDevice = False
        Else
            'Normal Loading For Add a New Device
            CB_DeviceType.SelectedIndex = 1
        End If

    End Sub

    Private Sub CB_DeviceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_DeviceType.SelectedIndexChanged

        If (CB_DeviceType.SelectedItem = "LR1") Then
            LBL_DeviceID.Text = "Device IP"
        Else
            LBL_DeviceID.Text = "Device MAC/IP"
            'MessageBox.Show("To get the mac address of the computer, run the command getmac in a console.", "MAC address", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub


End Class
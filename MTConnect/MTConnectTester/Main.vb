Imports OpenNETCF.MTConnect
'Imports OpenNETCF.MTConnect.Client
'Imports OpenNETCF.MTConnect.EntityClient
Imports OpenNETCF.Web

Public Class Main

    Private Sub BTN_Test_Click(sender As Object, e As EventArgs) Handles BTN_Test.Click
        If (TB_ip.Text.Length > 0) Then
            Dim urn As String = TB_ip.Text
            If (TB_AgentPort.Text.Length > 0) Then
                urn += ":" + TB_AgentPort.Text
            Else
                urn += ":80"
            End If

            Try
                CB_MachineName.SelectedIndex = -1
                CB_MachineName.Items.Clear()
                Dim m_client As EntityClient
                m_client = New EntityClient(urn)

                Dim Devices = m_client.Current()
                Dim dev As DeviceCollection = Devices
                If (dev.Count > 0) Then
                    For Each machinename In dev

                        Dim item As New ComboboxItem()
                        item.Text = machinename.Name
                        item.Value = m_client.GetDataItemById("avail").Value

                        CB_MachineName.Items.Add(item)
                    Next
                    CB_MachineName.Enabled = True
                    CB_MachineName.SelectedIndex = 0
                Else
                    MessageBox.Show("No device found from this IP. Check your MTConnect agent.")
                    CB_MachineName.Enabled = True
                End If
            Catch ex As Exception
                MessageBox.Show("Unable to retrieve any information from this IP. Check your adapter configuration or if your agent is running correctly.")
            End Try
        Else
            MessageBox.Show("Please enter an ip address")
        End If
    End Sub

    Public Class ComboboxItem
        Public Property Text() As String
            Get
                Return m_Text
            End Get
            Set(value As String)
                m_Text = Value
            End Set
        End Property
        Private m_Text As String
        Public Property Value() As Object
            Get
                Return m_Value
            End Get
            Set(value As Object)
                m_Value = Value
            End Set
        End Property
        Private m_Value As Object

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    Private Sub CB_MachineName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_MachineName.SelectedIndexChanged
        LBL_Mode.Text = "Adapter:"
        If (CB_MachineName.SelectedIndex >= 0) Then
            Dim cb As ComboboxItem
            cb = CB_MachineName.SelectedItem
            LBL_Mode.Text = "Adapter:" + cb.Value.ToString
        End If
    End Sub
End Class

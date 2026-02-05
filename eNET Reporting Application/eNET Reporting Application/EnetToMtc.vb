Imports System.Net
Imports OpenNETCF.MTConnect
Imports OpenNETCF.MTConnect.Client
Imports OpenNETCF.MTConnect.EntityClient
Imports OpenNETCF.Web

Public Class EnetToMtc

    Public IP As String

    Private Sub BTN_Ok_Click(sender As Object, e As EventArgs) Handles BTN_Ok.Click

        If TB_Address.Text.StartsWith("http") Then
        Else
            TB_Address.Text = "http://" & TB_Address.Text
        End If

        Try


            Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(TB_Address.Text), HttpWebRequest)
            ' Set the  'Timeout' property of the HttpWebRequest to 2000 milliseconds.
            myHttpWebRequest.Timeout = 10000
            ' A HttpWebResponse object is created and is GetResponse Property of the HttpWebRequest associated with it  
            Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
            If myHttpWebResponse.StatusCode = HttpStatusCode.OK Then


                IP = TB_Address.Text
                'Dim temp_client As New EntityClient(TB_Adress.Text)
                'Dim mtcrules As New CreateMTCrules(temp_client)
                'mtcrules.ShowDialog()
                Dim group As Integer = CBX_EnetGroup.SelectedItem
                Dim groupnum As Integer = CBX_EnetGroupNum.SelectedItem

                'Edit_mtconnect.enettomtc = True
                'Edit_mtconnect.monitoringfilenumber = ((group - 1) * 8 + groupnum)
                'Edit_mtconnect.IP = TB_Address.Text
                ''Edit_mtconnect.Location = Me.Location
                '' dtaGridView1.Rows(SetupForm.DataGridView1.Rows.Count - 1).Cells(2).Value = TB_Adress.Text
                ''  SetupForm.DataGridView1.Rows.Add(True, currentrow(0), TB_Adress.Text, "Check", "")

                'Edit_mtconnect.ShowDialog()


                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()

            Else
                MessageBox.Show("No response from this address")
            End If

        Catch Ex As System.UriFormatException
            TB_Address.Text = ""
            MessageBox.Show("You must enter an address")

        Catch Ex As System.Net.WebException
            TB_Address.Text = "http://"
            MessageBox.Show("You must enter a valid addresse")
        End Try


    End Sub

    Public devicename As String = ""


    Private Sub EnetToMtc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i = 0 To 15
            CBX_EnetGroup.Items.Add(i + 1)
        Next
        CBX_EnetGroup.SelectedIndex = 0

        For i = 0 To 7
            CBX_EnetGroupNum.Items.Add(i + 1)
        Next
        CBX_EnetGroupNum.SelectedIndex = 0

    End Sub

End Class
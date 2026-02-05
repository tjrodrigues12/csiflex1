Imports System.Net
Imports OpenNETCF.MTConnect
Imports OpenNETCF.MTConnect.Client
Imports OpenNETCF.MTConnect.EntityClient
Imports OpenNETCF.Web

Public Class FocasADD


    Public IP As String
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub BTN_Ok_Click(sender As Object, e As EventArgs) Handles BTN_Ok.Click

        Dim GoodAddress As Boolean = True


        For Each row As DataGridViewRow In SetupForm2.gridviewMachines.Rows
            If TB_Address.Text = row.Cells(2).Value.ToString() Then
                MessageBox.Show("This addresse is already used")
                GoodAddress = False
            End If
        Next

        If GoodAddress = True Then

            If TB_Address.Text.StartsWith("http") Then
            Else
                TB_Address.Text = "http://" & TB_Address.Text
            End If

            Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(TB_Address.Text), HttpWebRequest)
            ' Set the  'Timeout' property of the HttpWebRequest to 2000 milliseconds.
            myHttpWebRequest.Timeout = 10000
            ' A HttpWebResponse object is created and is GetResponse Property of the HttpWebRequest associated with it  
            Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
            If myHttpWebResponse.StatusCode = HttpStatusCode.OK Then

                IP = TB_Address.Text
                'Dim temp_client As New EntityClient(TextBox1.Text)
                'Dim mtcrules As New CreateMTCrules(temp_client)
                'mtcrules.ShowDialog()

                Edit_Focas.IP = TB_Address.Text
                'Edit_mtconnect.Location = Me.Location
                ' dtaGridView1.Rows(SetupForm2.DataGridView1.Rows.Count - 1).Cells(2).Value = TextBox1.Text
                '  SetupForm2.DataGridView1.Rows.Add(True, currentrow(0), TextBox1.Text, "Check", "")

                Edit_Focas.ShowDialog()
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()

                'SetupForm2.TV_Sources.Nodes(0).Nodes.Add(SetupForm2.TextBox1.Text, SetupForm2.ComboBox5.Text.ToString())

            Else
                MessageBox.Show("No response from this adsress")
            End If
        End If

    End Sub

    Public devicename As String = ""


End Class
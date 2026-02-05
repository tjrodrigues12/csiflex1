Imports System.Net
Imports OpenNETCF.MTConnect
Imports OpenNETCF.MTConnect.Client
Imports OpenNETCF.MTConnect.EntityClient
Imports OpenNETCF.Web

Public Class MTC_IP

    

    Private Sub BTN_Ok_Click(sender As Object, e As EventArgs) Handles BTN_Ok.Click


        Try
            If TB_IpAdress.Text <> "" Then
                If TB_IpAdress.Text.StartsWith("http") Then
                Else
                    TB_IpAdress.Text = "http://" & TB_IpAdress.Text
                End If

                Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(TB_IpAdress.Text), HttpWebRequest)
                ' Set the  'Timeout' property of the HttpWebRequest to 2000 milliseconds.
                myHttpWebRequest.Timeout = 2000
                ' A HttpWebResponse object is created and is GetResponse Property of the HttpWebRequest associated with it  
                Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
                If myHttpWebResponse.StatusCode = HttpStatusCode.OK Then


                    Edit_mtconnect.IP = TB_IpAdress.Text

                    'SetupForm.DGV_Source.Rows(SetupForm.DGV_Source.Rows.Count - 1).Cells(2).Value = TB_IpAdress.Text

                    Edit_mtconnect.Show()
                    Me.Close()

                Else
                    MessageBox.Show("No response from this adsress")
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public devicename As String = ""


    Private Sub MTC_IP_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Location = New Drawing.Point(SetupForm.Location.X + (SetupForm.Width - Me.Width) / 2, (SetupForm.Height - Me.Height) / 2 + SetupForm.Location.Y)
    End Sub
End Class
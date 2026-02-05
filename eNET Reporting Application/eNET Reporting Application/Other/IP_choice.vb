Imports System.Threading
Imports System.ServiceProcess

Public Class IP_choice
    Private Sub ok_Click(sender As Object, e As EventArgs) Handles ok.Click
        If LB_list.SelectedItem = LB_IPCheckChanged.Text Then
            'IP not changed 
            SetupForm2.IP_ = LB_list.SelectedItem
            CSI_Library.CSI_Library.IPChange = 1
            Me.Close()
        Else
            'IP will change
            'Dim str As String = "Processing..."
            'SetupForm2.LBL_IP.Text = "Web Server IP address : " & str
            'SetupForm2.LBL_IP.BackColor = Color.YellowGreen
            SetupForm2.IP_ = LB_list.SelectedItem
            CSI_Library.CSI_Library.IPChange = 2
            Me.Hide()
            SetupForm2.BringToFront()
            Me.Close()
        End If
        'End If
    End Sub
    Public Sub WaitForStatus(desiredStatus As ServiceControllerStatus)

    End Sub

End Class
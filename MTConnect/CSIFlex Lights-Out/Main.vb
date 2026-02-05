Imports CSI_Reporting_Application

Public Class Main

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MACHINE_CHOICE.Show()

        Dim MachineIP As String = "agent.mtconnect.org"
        Dim f As New CSI_Reporting_Application.Adv_MTC_cond_edit
        f.MdiParent = Me
        f.Show()
    End Sub
End Class

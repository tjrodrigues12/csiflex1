Imports CSIFLEX.Database.Access

Public Class MonitoringUnitsControl

    Private Sub MonitoringUnitsControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim Units As DataTable = MySqlAccess.GetDataTable("SELECT * FROM csi_auth.view_monitoringunits;")

        dgvMonitoringUnits.DataSource = Units

    End Sub

End Class

Imports System.ComponentModel
Imports System.IO
Imports CSI_Library
Imports CSIFLEX.Database.Access
Imports CSIFLEX.License.Data
Imports CSIFLEX.Server.Library.DataModel
Imports CSIFLEX.Utilities
Imports FocasLibrary.Tools

Partial Public Class SetupForm2

    Dim setSortOrder As ListSortDirection
    Dim idSortColumn As Integer = -1

    Public objAdapterInfo As New FocasLibrary.Components.AdapterInfo()
    Public objMainWindow As New FocasLibrary.MainWindow()

    Public Sub Load_DGV_CSIConnector()

        Try
            If idSortColumn < 0 Then idSortColumn = 0

            Select Case grvConnectors.SortOrder
                Case SortOrder.Ascending
                    setSortOrder = ListSortDirection.Ascending
                Case SortOrder.Descending
                    setSortOrder = ListSortDirection.Descending
                Case SortOrder.None
                    setSortOrder = ListSortDirection.Ascending
            End Select

            grvConnectors.DataSource = Nothing

            Dim sqlCmd As Text.StringBuilder = New Text.StringBuilder()

            sqlCmd.Append("SELECT                        ")
            sqlCmd.Append("    MachineName             , ")
            sqlCmd.Append("    MachineIP               , ")
            sqlCmd.Append("    MTCMachine              , ")
            sqlCmd.Append("    ConnectorType           , ")
            sqlCmd.Append("    eNETMachineName         , ")
            sqlCmd.Append("    FocasPort               , ")
            sqlCmd.Append("    ControllerType          , ")
            sqlCmd.Append("    AgentIP                 , ")
            sqlCmd.Append("    AgentPort               , ")
            sqlCmd.Append("    Manufacturer            , ")
            sqlCmd.Append("    AdapterPort             , ")
            sqlCmd.Append("    Id                        ")
            sqlCmd.Append("FROM                          ")
            sqlCmd.Append("    CSI_auth.tbl_CSIConnector ")

            Dim results As DataTable = MySqlAccess.GetDataTable(sqlCmd.ToString())

            grvConnectors.DataSource = results
            grvConnectors.Columns(0).HeaderCell.Value = "Machine Name"
            grvConnectors.Columns(1).HeaderCell.Value = "Machine IP"
            grvConnectors.Columns(2).HeaderCell.Value = "MTC Machine"
            grvConnectors.Columns(3).HeaderCell.Value = "Connection Type"
            grvConnectors.Columns(4).HeaderCell.Value = "eNET Machine Name"
            grvConnectors.Columns(5).HeaderCell.Value = "Focas Port"
            grvConnectors.Columns(6).HeaderCell.Value = "Controller Type"
            grvConnectors.Columns(7).HeaderCell.Value = "Agent Machine IP"
            grvConnectors.Columns(8).HeaderCell.Value = "Port"
            grvConnectors.Columns(9).HeaderCell.Value = "Manufacturer"
            grvConnectors.Columns(10).HeaderCell.Value = "AdapterPort"
            grvConnectors.Columns(11).HeaderCell.Value = "Id"
            grvConnectors.Columns(5).Visible = False
            grvConnectors.Columns(6).Visible = False
            grvConnectors.Columns(7).Visible = True
            grvConnectors.Columns(8).Visible = True
            grvConnectors.Columns(9).Visible = False
            grvConnectors.Columns(10).Visible = False
            grvConnectors.Columns(11).Visible = False
            grvConnectors.Columns(8).Width = 80

            grvConnectors.Sort(grvConnectors.Columns(idSortColumn), setSortOrder)


        Catch ex As Exception
            MessageBox.Show("Error :::" + ex.Message)
        End Try

    End Sub

    Private Sub btnAddConnector_Click(sender As Object, e As EventArgs) Handles btnAddConnector.Click

        Try

            Dim license As New CSILicenseLibrary()

            If Not license.IsLicenseValid(LicenseProducts.CSIFLEXFocasMtc, grvConnectors.Rows.Count + 1) Then
                MessageBox.Show("You don't have license to execute this action. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com")
                Return
            End If

            Dim frm_mtcadd As New MtcFocasADD()

            frm_mtcadd.ShowDialog()

        Catch ex As Exception

            MessageBox.Show("Error adding MTConnect machine, see log.")

            Log.Error(ex)

        End Try
    End Sub

    Private Sub btnEditConnector_Click(sender As Object, e As EventArgs) Handles btnEditConnector.Click

        If grvConnectors.SelectedCells.Count > 0 Then

            Dim selectedrowindex As Integer = grvConnectors.SelectedCells(0).RowIndex
            Dim selectedRow As DataGridViewRow = grvConnectors.Rows(selectedrowindex)

            Dim ConnectionType = Convert.ToString(selectedRow.Cells("ConnectorType").Value)

            Dim connectorId = Convert.ToInt16(selectedRow.Cells("Id").Value)

            Dim frm_mtcadd As New MtcFocasADD(connectorId)

            frm_mtcadd.ShowDialog()

            Load_DGV_CSIConnector()
        Else
            MessageBox.Show("Please select a Machine to Edit")
        End If

    End Sub

    Private Sub btnDeleteConnector_Click(sender As Object, e As EventArgs) Handles btnDeleteConnector.Click

        'We have two types of machines here MTConnect and Focas So Delete Logic is Different 
        If grvConnectors.SelectedCells.Count <= 0 Then
            MessageBox.Show("Please select a Machine to Delete")
            Return
        End If

        Dim result = MessageBox.Show("Do you really want to delete this Machine ?", "Delete Machine", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then

            Dim selectedrowindex As Integer = grvConnectors.SelectedCells(0).RowIndex
            Dim selectedRow As DataGridViewRow = grvConnectors.Rows(selectedrowindex)

            Dim connectorId As Integer = CInt(selectedRow.Cells("Id").Value)
            Dim connector = New Connector(connectorId)

            Dim machinename As String = Convert.ToString(selectedRow.Cells("MachineName").Value)
            Dim machineip As String = Convert.ToString(selectedRow.Cells("MachineIP").Value)
            Dim MachineType As String = Convert.ToString(selectedRow.Cells("ConnectorType").Value)
            Dim enetMachinename As String = Convert.ToString(selectedRow.Cells("eNETMachineName").Value)

            Me.Cursor = Cursors.WaitCursor

            If MachineType = "MTConnect" Then


            ElseIf MachineType = "Focas" Then

                objAdapterInfo.ServiceName = connector.AdapterServiceName

                objAdapterInfo.Path = System.IO.Path.Combine(Paths.ADAPTERS, connector.AdapterServiceName)

                objAdapterInfo.DeviceName = machinename

                objMainWindow.UninstallAdapter(objAdapterInfo)
                objMainWindow.UninstallAgent(connector.MachineName, connector.AgentServiceName)

                Dim res = IsDirectoryEmpty(Paths.ADAPTERS)

            End If

            connector.DeleteConnector()

            If CSI_Lib.MCHS_.ContainsKey(enetMachinename) Then
                CSI_Lib.MCHS_.Remove(enetMachinename)
            End If

            Me.Cursor = Cursors.Default
            MessageBox.Show("eNETMachine " + enetMachinename + " configurations deleted successfully !")
            Load_DGV_CSIConnector()

        ElseIf result = DialogResult.No Then
            'Do nothing here 
        End If

    End Sub

    Private Sub btnMonitoringUnits_Click(sender As Object, e As EventArgs) Handles btnMonitoringUnits.Click

        Dim form As MonitoringUnitsList = New MonitoringUnitsList()

        form.StartPosition = FormStartPosition.CenterScreen
        form.ShowDialog()

    End Sub

    Public Function IsDirectoryEmpty(path As String) As Boolean
        Return Not Directory.EnumerateFileSystemEntries(path).Any()
    End Function

End Class

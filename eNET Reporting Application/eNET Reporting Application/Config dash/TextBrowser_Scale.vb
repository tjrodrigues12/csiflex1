Imports CSIFLEX.Database.Access
Imports MySql.Data.MySqlClient

Public Class TextBrowser_Scale

    Dim loadingMode = False

    Dim deviceId = 0

    Private Sub TextBrowser_Scale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        deviceId = SetupForm2.deviceId
        LoadScales()
    End Sub

    Private Sub LoadScales()

        Dim dt As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_database.tbl_deviceconfig2 Where deviceId = { deviceId };")

        If dt.Rows.Count <> 0 Then

            loadingMode = True

            LBL_TextScale.Text = dt.Rows(0)("scale").ToString()
            LBL_BrowserScale.Text = dt.Rows(0)("browserzoom").ToString()
            lblTimeline.Text = dt.Rows(0)("TimeLineBarHeight").ToString()
            lblMachineName.Text = dt.Rows(0)("MachineNameText").ToString()
            lblMachineNameColumn.Text = dt.Rows(0)("MachineNameWidth").ToString()

            TrkBr_Textscale.Value = dt.Rows(0)("scale")
            TrkBr_Browserscale.Value = dt.Rows(0)("browserzoom")
            TrkBr_Timeline.Value = dt.Rows(0)("TimeLineBarHeight")
            TrkBr_MachineName.Value = dt.Rows(0)("MachineNameText")
            TrkBr_MachineNameColumn.Value = dt.Rows(0)("MachineNameWidth")

            Dim perPage = dt.Rows(0)("RemoveLastRow").ToString()

            If perPage = "0" Or perPage = "on" Or perPage = "off" Then
                txtMachinesPerPage.Text = "0"
                rbAuto.Checked = True
            Else
                txtMachinesPerPage.Text = perPage
                rbAuto.Checked = False
            End If

            rbCusto.Checked = Not rbAuto.Checked
            txtMachinesPerPage.Visible = rbCusto.Checked

            AddHandler txtMachinesPerPage.TextChanged, AddressOf txtMachinesPerPage_TextChanged
            AddHandler rbAuto.CheckedChanged, AddressOf radioButton_CheckedChanged
            AddHandler rbCusto.CheckedChanged, AddressOf radioButton_CheckedChanged

            loadingMode = False
        End If

    End Sub


    Private Sub TrkBr_Textscale_Scroll(sender As Object, e As EventArgs) Handles TrkBr_Textscale.Scroll
        LBL_TextScale.Text = TrkBr_Textscale.Value
    End Sub

    Private Sub TrkBr_Browserscale_Scroll(sender As Object, e As EventArgs) Handles TrkBr_Browserscale.Scroll
        LBL_BrowserScale.Text = TrkBr_Browserscale.Value
    End Sub

    Private Sub TrkBr_Timeline_Scroll(sender As Object, e As EventArgs) Handles TrkBr_Timeline.Scroll
        lblTimeline.Text = TrkBr_Timeline.Value
    End Sub

    Private Sub TrkBr_MachineName_Scroll(sender As Object, e As EventArgs) Handles TrkBr_MachineName.Scroll
        lblMachineName.Text = TrkBr_MachineName.Value
    End Sub

    Private Sub TrkBr_MachineNameColumn_Scroll(sender As Object, e As EventArgs) Handles TrkBr_MachineNameColumn.Scroll
        lblMachineNameColumn.Text = TrkBr_MachineNameColumn.Value
    End Sub

    Private Sub TrkBr_Textscale_MouseUp(sender As Object, e As MouseEventArgs) Handles TrkBr_Textscale.MouseUp

        Dim scaleValue As Int16 = Int16.Parse(LBL_TextScale.Text)
        Dim sqlCmd = $"UPDATE CSI_database.tbl_deviceConfig2 SET scale = { scaleValue } WHERE deviceId = {deviceId}"

        Try
            MySqlAccess.ExecuteNonQuery(sqlCmd)
            SetupForm2.RefreshDevice(deviceId)
        Catch ex As Exception
            MessageBox.Show("Could not update the setting : " & ex.StackTrace.ToString())
        End Try

    End Sub

    Private Sub TrkBr_Browserscale_MouseUp(sender As Object, e As MouseEventArgs) Handles TrkBr_Browserscale.MouseUp

        Dim scaleValue As Int16 = Int16.Parse(LBL_BrowserScale.Text)
        Dim sqlCmd = $"UPDATE CSI_database.tbl_deviceConfig2 SET browserzoom = { scaleValue } WHERE deviceId = {deviceId}"

        Try
            MySqlAccess.ExecuteNonQuery(sqlCmd)
            SetupForm2.RefreshDevice(deviceId)
        Catch ex As Exception
            MessageBox.Show("Could not update the setting : " & ex.StackTrace.ToString())
        End Try

    End Sub

    Private Sub TrkBr_Timeline_MouseUp(sender As Object, e As MouseEventArgs) Handles TrkBr_Timeline.MouseUp

        Dim scaleValue As Int16 = Int16.Parse(lblTimeline.Text)
        Dim sqlCmd = $"UPDATE CSI_database.tbl_deviceConfig2 SET TimeLineBarHeight = { scaleValue } WHERE deviceId = {deviceId}"

        Try
            MySqlAccess.ExecuteNonQuery(sqlCmd)
            SetupForm2.RefreshDevice(deviceId)
        Catch ex As Exception
            MessageBox.Show("Could not update the setting : " & ex.StackTrace.ToString())
        End Try

    End Sub

    Private Sub TrkBr_MachineName_MouseUp(sender As Object, e As MouseEventArgs) Handles TrkBr_MachineName.MouseUp

        Dim scaleValue As Int16 = Int16.Parse(lblMachineName.Text)
        Dim sqlCmd = $"UPDATE CSI_database.tbl_deviceConfig2 SET MachineNameText = { scaleValue } WHERE deviceId = {deviceId}"

        Try
            MySqlAccess.ExecuteNonQuery(sqlCmd)
            SetupForm2.RefreshDevice(deviceId)
        Catch ex As Exception
            MessageBox.Show("Could not update the setting : " & ex.StackTrace.ToString())
        End Try

    End Sub

    Private Sub TrkBr_MachineNameColumn_MouseUp(sender As Object, e As MouseEventArgs) Handles TrkBr_MachineNameColumn.MouseUp

        Dim scaleValue As Int16 = Int16.Parse(lblMachineNameColumn.Text)
        Dim sqlCmd = $"UPDATE CSI_database.tbl_deviceConfig2 SET MachineNameWidth = { scaleValue } WHERE deviceId = {deviceId}"

        Try
            MySqlAccess.ExecuteNonQuery(sqlCmd)
            SetupForm2.RefreshDevice(deviceId)
        Catch ex As Exception
            MessageBox.Show("Could not update the setting : " & ex.StackTrace.ToString())
        End Try

    End Sub

    Private Sub txtMachinesPerPage_TextChanged(sender As Object, e As EventArgs)

        Dim qtt As Integer

        If Not Integer.TryParse(txtMachinesPerPage.Text, qtt) Then
            MessageBox.Show("The value of Machine Per Page must be an integer number")
        End If

        Dim sqlCmd = $"UPDATE CSI_database.tbl_deviceConfig2 SET RemoveLastRow = '{ txtMachinesPerPage.Text }' WHERE deviceId = {deviceId}"

        If Not loadingMode Then
            Try
                MySqlAccess.ExecuteNonQuery(sqlCmd)
                SetupForm2.RefreshDevice(deviceId)
            Catch ex As Exception
                MessageBox.Show("Could not update the setting : " & ex.StackTrace.ToString())
            End Try
        End If

    End Sub

    Private Sub radioButton_CheckedChanged(sender As Object, e As EventArgs)
        txtMachinesPerPage.Visible = rbCusto.Checked

        If rbAuto.Checked Then
            txtMachinesPerPage.Text = "0"
        Else
            txtMachinesPerPage.Select()
        End If
    End Sub

End Class
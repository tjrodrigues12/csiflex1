Imports System.IO
Imports System.Xml
Imports OpenNETCF.MTConnect

Public Class adv_mtc_Notifications



    Public home_path As String = CSI_Library.CSI_Library.serverRootPath
    Public eNetname As String = String.Empty
    Public MTCMachinename As String = String.Empty
    Dim Current_conditions As New List(Of ICondition)
    Dim list_of_Status As New List(Of String)

    Private Sub BW_conditions_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BW_conditions.DoWork


        Dim device_index As Integer = 0

        Dim m_client As EntityClient = New EntityClient(IP)
        Dim Current_stream As DataStream = m_client.Current()
        Dim Current_device As DeviceStream

        Dim Devices = m_client.Probe()
        Dim dev As DeviceCollection = Devices
        Current_conditions.Clear()

        For Each device In Devices
            If (MachineName = device.Name) Then
                Current_device = Current_stream.DeviceStreams(device_index)
                Dim current_component_stream() As ComponentStream = Current_device.ComponentStreams
                For Each Component As ComponentStream In current_component_stream
                    For Each condition As ICondition In Component.Conditions
                        Current_conditions.Add(condition)
                    Next
                Next
            End If
            device_index += 1
        Next




    End Sub

    Private Sub BW_conditions_completed(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW_conditions.RunWorkerCompleted
        PB_processing.Visible = False
        LBL_RETR.Visible = False
        Dim LVI As ListViewItem = New ListViewItem
        LVI.Text = "IDs"

        Dim list_of_cond As New List(Of String)

        For Each item In Current_conditions
            ' If item.Value <> "Unavailable" Then
            list_of_cond.Add(item.DataItemID + " (" + item.Type + ")")
            '  End If
        Next


        'read from setup ??



        'merge
        Dim cond_column As New List(Of String)
        DGV_Cond.Rows.Clear()
        If DGV_Cond IsNot Nothing Then
            For Each row As DataGridViewRow In DGV_Cond.Rows
                If row.Cells(1).Value IsNot Nothing Then cond_column.Add(row.Cells(1).Value)
            Next
        End If
        For Each status As String In list_of_cond
            If Not cond_column.Contains(status) Then
                If status <> "" Then DGV_Cond.Rows.Add(status, False, False, 0)
            End If
        Next

    End Sub

    Public MachineName As String = Adv_MTC_cond_edit.MachineName
    Public IP As String = Adv_MTC_cond_edit.MachineIP
    Private tbl_notif_status As New DataTable
    Private tbl_notif_cond As New DataTable


    Private Sub save_setup()



        'save the status datagridview   
        Dim table_ As New DataTable("notif_stat")
        For Each col As DataGridViewColumn In DGV_stat.Columns
            table_.Columns.Add(col.HeaderText)
        Next


        For Each row As DataGridViewRow In DGV_stat.Rows
            Dim tablerow As DataRow = table_.NewRow
            ' If Not (row.Cells.Item(1).Value Is Nothing Or row.Cells.Item(2).Value Is Nothing Or row.Cells.Item(3).Value Is Nothing) Then

            For Each cell As DataGridViewCell In row.Cells
                tablerow(cell.ColumnIndex) = If(cell.Value Is Nothing, "", cell.EditedFormattedValue)
            Next
                table_.Rows.Add(tablerow)
            ' End If
        Next

        ' Create a file name to write to.
        If File.Exists(home_path & "\sys\Conditions\" & eNetname & "\" & "notif_stat.xml") Then
            File.Delete(home_path & "\sys\Conditions\" & eNetname & "\" & "notif_stat.xml")
        End If

        ' Create the FileStream to write with.
        Dim stream As New System.IO.FileStream _
            (home_path & "\sys\Conditions\" & eNetname & "\" & "notif_stat.xml", System.IO.FileMode.CreateNew)

        table_.WriteXml(stream)
        stream.Close()




        'save the cond datagridview   
        table_ = New DataTable("notif_cond")
        For Each col As DataGridViewColumn In DGV_Cond.Columns
            table_.Columns.Add(col.HeaderText)
        Next


        For Each row As DataGridViewRow In DGV_Cond.Rows
            Dim tablerow As DataRow = table_.NewRow

            For Each cell As DataGridViewCell In row.Cells
                tablerow(cell.ColumnIndex) = If(cell.Value Is Nothing, "", cell.EditedFormattedValue)
            Next
            table_.Rows.Add(tablerow)
        Next

        ' Create a file name to write to.
        If File.Exists(home_path & "\sys\Conditions\" & eNetname & "\" & "notif_cond.xml") Then
            File.Delete(home_path & "\sys\Conditions\" & eNetname & "\" & "notif_cond.xml")
        End If

        ' Create the FileStream to write with.
        stream = New System.IO.FileStream _
            (home_path & "\sys\Conditions\" & eNetname & "\" & "notif_cond.xml", System.IO.FileMode.CreateNew)

        table_.WriteXml(stream)
        stream.Close()







    End Sub




    Private Sub adv_mtc_Notifications_close(sender As Object, e As EventArgs) Handles MyBase.Closed
        save_setup()
    End Sub

    Private Sub adv_mtc_Notifications_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' check the available conditions from the agent
        If Not BW_conditions.IsBusy Then BW_conditions.RunWorkerAsync()

        ' Read the status dgv from setup file
        read_setup()

        Dim stat_column As New List(Of String)
        If DGV_stat IsNot Nothing Then
            For Each row As DataGridViewRow In DGV_stat.Rows
                If row.Cells(1).Value IsNot Nothing Then stat_column.Add(row.Cells(1).Value)
            Next
        End If
        For Each status As String In list_of_Status
            If Not stat_column.Contains(status) Then
                If status <> "" Then DGV_stat.Rows.Add(False, status, 0)
            End If
        Next




    End Sub



    Private Sub read_setup()



        ' Check the available status from the setup config files (to chck if new status available)
        list_of_Status = read_status()

        If File.Exists(home_path & "\sys\Conditions\" & eNetname & "\" & "notif_stat.xml") Then
            Dim xmlFile As XmlReader
            xmlFile = XmlReader.Create(home_path & "\sys\Conditions\" & eNetname & "\" & "notif_stat.xml")

            tbl_notif_status.Clear()

            tbl_notif_status = New DataTable("notif_stat")
            For Each col As DataGridViewColumn In DGV_stat.Columns
                tbl_notif_status.Columns.Add(col.HeaderText)
            Next


            tbl_notif_status.ReadXml(xmlFile)
            xmlFile.Close()

            DGV_stat.Rows.Clear()
            For Each row As DataRow In tbl_notif_status.Rows
                If row(1) <> "" Then
                    DGV_stat.Rows.Add(row(0), row(1), row(2))
                End If
            Next
        End If


    End Sub

    Private Function read_status() As List(Of String)

        Dim line() As String
        Dim list_of_Status As New List(Of String)

        Try
            If File.Exists(home_path & "\sys\Conditions\" & eNetname & "\Conditions.csys") Then
                list_of_Status.Clear()
                Using reader As StreamReader = New StreamReader(home_path & "\sys\Conditions\" & eNetname & "\Conditions.csys")
                    While Not (reader.EndOfStream)
                        line = reader.ReadLine().Split(":")
                        If line.Length > 0 Then list_of_Status.Add(line(0))
                    End While
                    reader.Close()
                End Using
            End If
        Catch ex As Exception
            MsgBox("Error while retrieving the machine status :  " + ex.Message)
        End Try

        Return list_of_Status

    End Function


End Class
Imports MySql.Data.MySqlClient

Public Class UninstallAndBackUp
    Dim mysqlcon As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
    Private Sub BTN_No_Click(sender As Object, e As EventArgs) Handles BTN_No.Click
        'Uninstalls without backup
    End Sub

    Private Sub BTN_Yes_Click(sender As Object, e As EventArgs) Handles BTN_Yes.Click

        'Uninstalls with two different optoions 1)With configuration or 2)With Configuration and Data

        Try

            If RBTN_ConfigOnly.Checked = False And RBTN_DataPlus.Checked = False Then
                MessageBox.Show("Please Select One BackUp Option")
            ElseIf RBTN_ConfigOnly.Checked = True Then 'Option ::1  With configuration Only
                mysqlcon.Open()
                Dim myList As New List(Of String)
                myList.Clear()
                Dim selecttables As String = "SELECT tbl_renamemachines.table_name FROM csi_database.tbl_renamemachines;"
                Dim cmdselecttables As New MySqlCommand(selecttables, mysqlcon)
                Dim mysqlDatareader As MySqlDataReader = cmdselecttables.ExecuteReader()
                While mysqlDatareader.Read()
                    myList.Add(mysqlDatareader("table_name").ToString())
                End While
                mysqlDatareader.Close()
                Dim length As Integer
                length = myList.Count
                For Each val As String In myList
                    Dim dropTables As String = "DROP TABLE IF EXISTS csi_database.tbl_" & val.ToString() & ";"
                    Dim cmd As New MySqlCommand(dropTables, mysqlcon)
                    cmd.ExecuteNonQuery()
                Next
                Dim dropMachinePerfDB As String = "DROP DATABASE if exists csi_machineperf;"
                Dim cmddropMachinePerfDB As New MySqlCommand(dropMachinePerfDB, mysqlcon)
                cmddropMachinePerfDB.ExecuteNonQuery()

            ElseIf RBTN_DataPlus.Checked = True Then 'Option :::2 With Configuration and Data
                'Option 2 is here 
            End If
            If (Not System.IO.Directory.Exists("C:\Program Files (x86)\CSIFlex Server Database BackUp\")) Then
                System.IO.Directory.CreateDirectory("C:\Program Files (x86)\CSIFlex Server Database BackUp\")
            End If

            Dim folderBrowserdialouge1 As New FolderBrowserDialog()
            folderBrowserdialouge1.RootFolder = Environment.SpecialFolder.MyComputer
            folderBrowserdialouge1.SelectedPath = "C:\Program Files (x86)\CSIFlex Server Database BackUp\"
            If folderBrowserdialouge1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                MessageBox.Show("We are here now :" & folderBrowserdialouge1.SelectedPath.ToString())
                Dim cmd As MySqlCommand = New MySqlCommand
                'cmd.Connection = conn
                Dim mb As MySqlBackup = New MySqlBackup(cmd)
                mb.ExportToFile(folderBrowserdialouge1.SelectedPath.ToString() & "\backup.sql")
            End If
            'mysqlcon.Close()
            Me.Close()
            MessageBox.Show("Uninstallation is completed !")
        Catch ex As Exception
            MessageBox.Show("Error while create a BackUp :" & ex.Message)
        Finally
            mysqlcon.Close()
        End Try
    End Sub

    Private Sub BTN_Cancel_Click(sender As Object, e As EventArgs) Handles BTN_Cancel.Click
        'This closes without doing anything
        Me.Close()
    End Sub
End Class
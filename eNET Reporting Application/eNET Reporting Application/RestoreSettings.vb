Imports System.IO
Imports System.Text.RegularExpressions
Imports CSIFLEX.Server.Library

Public Class RestoreSettings

    Dim fileInfo As FileInfo
    Dim lines As List(Of String)
    Dim tables As List(Of TableRestore)

    Private Sub btnSelectFile_Click(sender As Object, e As EventArgs) Handles btnSelectFile.Click

        Dim dialog As OpenFileDialog = New OpenFileDialog()
        dialog.Filter = "SQL Backup (*.sql)|*.sql"
        dialog.InitialDirectory = $"C:\CSIFLEX\BackupSettings"

        If dialog.ShowDialog() <> DialogResult.OK Then Return

        fileInfo = New FileInfo(dialog.FileName)

        txtSourceFile.Text = fileInfo.Name

        lines = New List(Of String)()
        lines.AddRange(File.ReadLines(fileInfo.FullName))

        Dim regex As Regex = New Regex("(?<=`).*?(?=`)")

        Dim tableRestore As TableRestore = New TableRestore()

        tables = New List(Of TableRestore)()
        For Each line In lines

            If line.StartsWith("LOCK TABLES ") Then
                Dim matches = regex.Matches(line)
                Dim database = matches(0).Value
                Dim table = matches(2).Value

                tableRestore = New TableRestore()
                tableRestore.Database = database
                tableRestore.TableName = table
                tableRestore.CommandLines = New List(Of String)()
                tableRestore.CommandLines.Add(line)
                tableRestore.CommandLines.Add($"TRUNCATE TABLE `{database}`.`{table}`;")

            ElseIf line.StartsWith("REPLACE INTO") Then
                tableRestore.CommandLines.Add(line)

            ElseIf line.StartsWith("UNLOCK TABLES;") Then
                tableRestore.CommandLines.Add(line)
                tables.Add(tableRestore)

            End If
        Next

        Dim defaultCellStyle As DataGridViewCellStyle = dgvTables.DefaultCellStyle()

        dgvTables.Columns.Clear()
        dgvTables.DefaultCellStyle = defaultCellStyle

        Dim column As DataGridViewColumn

        column = New DataGridViewCheckBoxColumn()
        column.Name = "selectedTable"
        column.DataPropertyName = "selectedTable"
        column.HeaderText = ""
        column.Visible = True
        column.ReadOnly = False
        dgvTables.Columns.Add(column)

        column = New DataGridViewTextBoxColumn()
        column.Name = "databaseName"
        column.DataPropertyName = "database"
        column.HeaderText = "Database"
        column.Visible = True
        column.ReadOnly = True
        dgvTables.Columns.Add(column)

        column = New DataGridViewTextBoxColumn()
        column.Name = "tableName"
        column.DataPropertyName = "tableName"
        column.HeaderText = "Table Name"
        column.Visible = True
        column.ReadOnly = True
        dgvTables.Columns.Add(column)

        dgvTables.Columns("selectedTable").Width = 60
        dgvTables.Columns("databaseName").Width = 200

        dgvTables.AutoGenerateColumns = False
        dgvTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvTables.DataSource = tables

    End Sub

    Private Sub ckbSelectAllTables_CheckedChanged(sender As Object, e As EventArgs) Handles ckbSelectAllTables.CheckedChanged

        For Each row As DataGridViewRow In dgvTables.Rows
            row.Cells("selectedTable").Value = ckbSelectAllTables.Checked
        Next

    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click

        If Not MessageBox.Show("Do you confirm the restoration of the selected tables? All current data in these tables will be lost.", "Restore tables", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) = DialogResult.Yes Then
            Return
        End If

        Dim commandLines = New List(Of String)()

        For Each row As DataGridViewRow In dgvTables.Rows

            If row.Cells("selectedTable").Value Then

                Dim tableName = row.Cells("tableName").Value

                Dim tableRestore As TableRestore = tables.FirstOrDefault(Function(t) t.TableName = tableName)

                commandLines.AddRange(tableRestore.CommandLines)
            End If

        Next

        Dim restoreFile = Path.Combine($"C:\CSIFLEX\BackupSettings", "RestoreFile.sql")
        File.WriteAllLines(restoreFile, commandLines)

        If BackupSettings.RestoreFile($"C:\CSIFLEX\MySQL\mysql-8.0.18-winx64\bin", restoreFile, "") Then
            MessageBox.Show("Table(s) restored.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("ERROR trying to restore the table(s). Ckeck the log file.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

End Class


Public Class TableRestore

    Private _selectedTable As Boolean
    Public Property SelectedTable() As Boolean
        Get
            Return _selectedTable
        End Get
        Set(ByVal value As Boolean)
            _selectedTable = value
        End Set
    End Property

    Private _tableName As String
    Public Property TableName() As String
        Get
            Return _tableName
        End Get
        Set(ByVal value As String)
            _tableName = value
        End Set
    End Property

    Private _database As String
    Public Property Database() As String
        Get
            Return _database
        End Get
        Set(ByVal value As String)
            _database = value
        End Set
    End Property

    Private _commandLines As List(Of String)
    Public Property CommandLines() As List(Of String)
        Get
            Return _commandLines
        End Get
        Set(ByVal value As List(Of String))
            _commandLines = value
        End Set
    End Property
End Class
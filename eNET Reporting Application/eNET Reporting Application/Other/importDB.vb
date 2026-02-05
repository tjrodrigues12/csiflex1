Imports System.IO
Imports CSI_Library
Imports MySql.Data.MySqlClient

Public Class importDB
    Dim conn As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim OPen_ As Boolean
        OPen_ = OFD_.ShowDialog()
        If (OPen_ = True) Then
            LB_selected.DataSource = OFD_.FileNames.ToList
            Dim to_path As String = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

            For Each file In OFD_.FileNames
                If System.IO.File.Exists(to_path & "\" & My.Computer.FileSystem.GetName(file)) Then System.IO.File.Delete(to_path & "\" & My.Computer.FileSystem.GetName(file))
                System.IO.File.Copy(file, to_path & "\" & My.Computer.FileSystem.GetName(file))
            Next

        End If

    End Sub

    Private Sub init_configdevice2() ' Usefull after instal version 1.8.6.7 for the first time
        Try
            'Using 
            Dim cmd As New MySqlCommand("SELECT name, IP FROM CSI_database.tbl_deviceconfig", conn)
            conn.Open()
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim results As New DataTable()
            adapter.Fill(results)

            If results.Rows.Count <> 0 Then
                For Each r As DataRow In results.Rows
                    cmd = New MySqlCommand("insert ignore into CSI_Database.tbl_deviceConfig2 (name, IP_adress,timeline,trends,trendspercent,trendcompare,dateformat,devicetype,scale) VALUES('" + r(0) + "','" + r(1) + "',true,true,20,'shift','dd-MM-yyyy HH:mm:ss','" & "Computer" & "',100);", conn)
                    cmd.ExecuteNonQuery()
                Next
            End If
            'conn.Close()
            'End Using
        Catch ex As Exception
            MessageBox.Show("Database Connection prolem :" & ex.Message() & " StackTrace : " & ex.StackTrace())
        Finally
            conn.Close()
        End Try

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles BTN_import.Click

        LBL_ImpRes.Text = "Result:"
        Try
            Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\CSI Flex Server\mysql\mysql-8.0.18-winx64\bin\mysql.exe"
            'Check if x86 exist
            If Not File.Exists(path) Then
                path = Environment.ExpandEnvironmentVariables("%ProgramW6432%") + "\CSI Flex Server\mysql\mysql-8.0.18-winx64\bin\mysql.exe" ' Local Testing 
            End If

            If File.Exists(path) Then
                Dim process As New System.Diagnostics.Process()
                Dim startInfo As New System.Diagnostics.ProcessStartInfo()
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                'startInfo.FileName = @"C:\Users\rbcou\Source\Repos\CSIFlex1\mysql\mysql-5.7.14-win32\bin\mysql.exe";
                startInfo.FileName = path
                startInfo.Arguments = "-u root --password=CSIF1337 csi_auth -e ""source csi_auth.sql"""
                process.StartInfo = startInfo
                process.Start()
                process.WaitForExit()
                process = New System.Diagnostics.Process()
                startInfo = New System.Diagnostics.ProcessStartInfo()
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                startInfo.FileName = path
                startInfo.Arguments = "-u root --password=CSIF1337 csi_database -e ""source csi_database.sql"""
                process.StartInfo = startInfo
                process.Start()
                process.WaitForExit()

                LBL_ImpRes.Text = "Result: Import succesful"

                SetupForm2.Load_DGV_CSIConnector()
                ' SetupForm2.DGV_Mtc.Refresh()
                SetupForm2.load_DeviceName()
                ' SetupForm2.Devices_TV.Refresh()

            Else
                LBL_ImpRes.Text = Convert.ToString("Error: Unable to find mysql.exe at ") & path
            End If

            If SetupForm2.BgWorker_CreateDB.IsBusy Then
                MsgBox("The settings have been imported, you may have to reboot CSIFlex after the database creation to see these settings.")
            Else
                MsgBox("The settings have been imported, you may have to reboot CSIFlex to see these settings.")
            End If

        Catch ex As Exception
            Console.WriteLine("Exception:" + ex.Message)
            LBL_ImpRes.Text = "Error: " + ex.Message
        End Try

        init_configdevice2()

    End Sub
End Class
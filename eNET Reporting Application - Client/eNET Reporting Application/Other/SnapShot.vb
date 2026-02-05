Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary


'Public Class serialisable_periode_

'    <Serializable()>
'    Public Structure Serialisable_periode

'        Dim per As csi
'    End Structure
'End Class

Public Class SnapShot
    Private Sub BTN_take_snap_Click(sender As Object, e As EventArgs) Handles BTN_take_snap.Click
        Dim CSILIB As New CSI_Library.CSI_Library(False)
        Try
            Dim rootPath As String = CSI_Library.CSI_Library.ClientRootPath

            ' Create file by FileStream class
            If File.Exists(rootPath & "\sys\Snap_perf.bin") Then File.Delete(rootPath & "\sys\Snap_perf.bin")
            Dim fs As FileStream = New FileStream(rootPath & "\sys\Snap_perf.bin", FileMode.OpenOrCreate)

            ' Creat binary object
            Dim bf As New BinaryFormatter()

            ' Serialize object to file

            If Report_BarChart.consolidated_periode(0).date_ Is Nothing Then
                bf.Serialize(fs, Config_report.periode_returned)
            Else
                bf.Serialize(fs, Report_BarChart.consolidated_periode)
            End If

            fs.Close()
        Catch ex As Exception
            MsgBox("Could not create a SnapShot")
            CSILIB.LogClientError(ex.Message)
        End Try



        'to read it:
        '' Open file and deserialize to object again
        'Dim fsRead As New FileStream("C:\test.bin", FileMode.Open)
        'Dim objTest As Object = bf.Deserialize(fsRead)
        'fsRead.Close()


    End Sub
End Class
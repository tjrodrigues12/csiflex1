Imports System.IO
Imports System.IO.Compression
Imports System.Windows
Imports System.Data.OleDb
Imports Microsoft.Reporting.WinForms
Imports System.Globalization
Imports CSI_Library
Imports System.Runtime.CompilerServices

Public Class SetupForm_Reporting


    Private active_machines() As String
    Private TypeDePeriode As String
    Private Qry_Tbl_MachineName As String
    Private Qry_Tbl_DataMachine As String
    Public CSI_Lib As CSI_Library.CSI_Library

    Private Sub SetupForm_Reporting_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim file As System.IO.StreamReader
        Dim imglst As New ImageList
        CSI_Lib = New CSI_Library.CSI_Library

        Dim list2 As New List(Of String)

        Dim mt_present As Boolean = False
        Dim st_present As Boolean = False

        Dim prefixTemp As String ' temp string, multiple use

        Dim machine() As String ' File -> read line -> machine()

        TreeView_machine.Nodes.Clear()


        If Not My.Computer.FileSystem.FileExists(System.Windows.Forms.Application.StartupPath + "\sys\MonList.sys") Then
            MsgBox("The file 'Monlist.sys' is missing")
        Else

            file = My.Computer.FileSystem.OpenTextFileReader(System.Windows.Forms.Application.StartupPath + "\sys\MonList.sys")
            Dim i As Integer = 0
            Dim j As Integer = -1
            Dim k As Integer = -1
            Dim cpt As Integer = 0
            While Not file.EndOfStream

                ReDim Preserve machine(i + 1)
                machine(i) = file.ReadLine()

                prefixTemp = Strings.Left(machine(i), 3)

                Dim node As New TreeNode

                If Strings.Len(machine(i)) > 4 Then
                    node.ImageKey = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
                    node.Text = Strings.Right(machine(i), Strings.Len(machine(i)) - 4)
                Else
                    node.ImageKey = machine(i)
                    node.Text = machine(i)
                End If

                Select Case prefixTemp
                    Case "_MT"
                        mt_present = True
                        st_present = False

                        list2.Add(machine(i))
                        TreeView_machine.Nodes.Add(node)
                        cpt = -1
                        j = j + 1
                    Case "_ST"
                        st_present = True
                        cpt += 1
                        If mt_present = False Then
                            TreeView_machine.Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
                            'TreeView_machine.Nodes.Add(node)
                        Else
                            TreeView_machine.Nodes(j).Nodes.Add(Strings.Right(machine(i), Strings.Len(machine(i)) - 4), Strings.Right(machine(i), Strings.Len(machine(i)) - 4))
                            'TreeView_machine.Nodes(j).Nodes.Add(node)
                        End If

                        k += 1

                    Case Else
                        If (prefixTemp <> "_ST") And (prefixTemp <> "_MT") Then
                            If st_present = False And mt_present = True Then
                                TreeView_machine.Nodes(j).Nodes.Add(machine(i), machine(i))

                                cpt += 1

                            ElseIf mt_present = False And st_present = True Then

                                TreeView_machine.Nodes(k).Nodes.Add(machine(i), machine(i))



                            ElseIf mt_present = False And st_present = False Then

                                TreeView_machine.Nodes.Add(machine(i), machine(i))


                            Else

                                TreeView_machine.Nodes(j).Nodes(cpt).Nodes.Add(machine(i), machine(i))


                            End If

                        End If
                End Select

                i = i + 1
            End While
        End If

        Me.ReportViewer1.RefreshReport()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles BTN_BrowseFolder.Click
        Dim folderDlg As New FolderBrowserDialog

        folderDlg.ShowNewFolderButton = False
        If (folderDlg.ShowDialog() = DialogResult.OK) Then
            tb_pathReport.Text = folderDlg.SelectedPath
            Dim root As Environment.SpecialFolder = folderDlg.RootFolder
        End If


    End Sub

    Public Function read_tree() As String()
        ' checked_in_TreeView_machine.Clear()


        ReDim active_machines(0)

        Dim aNode As TreeNode

        For Each aNode In TreeView_machine.Nodes
            PrintRecursive(aNode)
        Next
        ' Return checked_in_TreeView_machine
        ReDim Preserve active_machines(UBound(active_machines) - 1)
        Return active_machines

    End Function


    Private Sub PrintRecursive(ByVal n As TreeNode)
        If n.Checked = True And n.Nodes.Count = 0 Then
            '   checked_in_TreeView_machine.Add(n.Text)
            If n.Name <> "" Then
                active_machines(UBound(active_machines)) = n.Name
                ReDim Preserve active_machines(UBound(active_machines) + 1)
            End If
        End If

        Dim aNode As TreeNode
        For Each aNode In n.Nodes
            PrintRecursive(aNode)
        Next
    End Sub


    Sub generateSqlQuery()

        Dim ListOfMachineSelected As String() = read_tree()
        Dim i As Integer = 0

        Qry_Tbl_MachineName = ""
        Qry_Tbl_DataMachine = ""

        For Each machine In ListOfMachineSelected

            machine = machine.Replace(" ", "").Replace("-", "")

            Qry_Tbl_MachineName += "SELECT 'tbl_" + machine + "' AS MchName, status FROM tbl_" + machine + " "
            Qry_Tbl_DataMachine += "SELECT 'tbl_" + machine + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, iif(status = '_COFF', cycletime,0) as COFF, iif(status = '_CON', cycletime,0) as CON, iif(status = '_SETUP', cycletime,0) as SETUP, iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP', cycletime,0) as OTHER  from tbl_" + machine + " "

            If Not i = (ListOfMachineSelected.Length - 1) Then

                Qry_Tbl_MachineName += " union "
                Qry_Tbl_DataMachine += " union "

            End If
            i += 1
        Next



    End Sub

    Private Sub setControl(enable As Boolean)

        DTPicker.Enabled = enable
        tb_pathReport.Enabled = enable
        TreeView_machine.Enabled = enable
        GroupBox2.Enabled = enable
        BTN_SetDefaultPath.Enabled = enable
        BTN_BrowseFolder.Enabled = enable

    End Sub

    Public Function FileInUse(ByVal sFile As String) As Boolean
        If System.IO.File.Exists(sFile) Then
            Try
                Dim F As Short = FreeFile()
                FileOpen(F, sFile, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FileClose(F)
            Catch

                Return True
            End Try
        End If
        Return False
    End Function



    Private Sub BTN_GenerateReport_Click(sender As Object, e As EventArgs) Handles BTN_GenerateReport.Click
       
        BTN_GenerateReport.Enabled = False
        setControl(False)
        PB_loading.Visible = True
        BackgroundWorker.RunWorkerAsync()

    End Sub

    Public Sub generateReport()


        If Not (tb_pathReport.Text = "") Then


            If Not Directory.Exists(System.Windows.Forms.Application.StartupPath & "\reports_templates") Then
                IO.File.WriteAllBytes(System.Windows.Forms.Application.StartupPath & "\reports_templates.zip", My.Resources.reports_templates)


                ZipFile.ExtractToDirectory(System.Windows.Forms.Application.StartupPath & "\reports_templates.zip", _
                                      System.Windows.Forms.Application.StartupPath)
            End If

            Dim ReportViewer1 As ReportViewer = New ReportViewer()
            generateSqlQuery()
            Dim Paramet(1) As ReportParameter

            Dim MondayOfWeek As DateTime

            ReportViewer1.ProcessingMode = ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = System.Windows.Forms.Application.StartupPath + "//reports_templates//mainReport.rdlc"
            ReportViewer1.LocalReport.Refresh()


            MondayOfWeek = New DateTime(DTPicker.Value.Year, DTPicker.Value.Month, DTPicker.Value.Day)


            If (CB_Weekly.Checked) Then
                TypeDePeriode = "ww"

                Paramet(0) = New ReportParameter("reportType", TypeDePeriode)
                Paramet(1) = New ReportParameter("date", IntToMonday(DatePart(TypeDePeriode, DTPicker.Value, FirstDayOfWeek.Monday), DTPicker.Value))

                ReportViewer1.LocalReport.SetParameters(Paramet)
                Call ReloadReport(ReportViewer1)
                ReportViewer1.RefreshReport()
                saveReport(ReportViewer1, "weelky")
            End If

            If (CB_Monthly.Checked) Then
                TypeDePeriode = "m"
                Paramet(0) = New ReportParameter("reportType", TypeDePeriode)
                Paramet(1) = New ReportParameter("date", DTPicker.Value)



                ReportViewer1.LocalReport.SetParameters(Paramet)
                Call ReloadReport(ReportViewer1)
                ReportViewer1.RefreshReport()
                saveReport(ReportViewer1, "monthly")
            End If

            If (CB_Daily.Checked) Then

                ReportViewer1.LocalReport.ReportPath = System.Windows.Forms.Application.StartupPath + "//reports_templates//mainDaily.rdlc"
                ReportViewer1.LocalReport.Refresh()
                Dim ParametersD(1) As ReportParameter
                TypeDePeriode = "y"
                ParametersD(0) = New ReportParameter("reportType", TypeDePeriode)
                ParametersD(1) = New ReportParameter("datenow", String.Format("{0:dd-MMM-yyyy}", DTPicker.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US"))))
                ReportViewer1.LocalReport.SetParameters(ParametersD)
                Call ReloadReport(ReportViewer1)
                ReportViewer1.RefreshReport()
                saveReport(ReportViewer1, "daily")

            End If



            Dim repReport As DirectoryInfo = New DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\reports_templates")

            For Each fily As FileInfo In repReport.GetFiles()

                fily.Delete()

            Next

            Directory.Delete(System.Windows.Forms.Application.StartupPath & "\reports_templates")
            File.Delete(System.Windows.Forms.Application.StartupPath & "\reports_templates.zip")




            MsgBox("report(s) completed")
        Else
            MsgBox("Please select a folder")

        End If

    End Sub

    Public Function toMonday(datee As Date) As DateTime
        Dim today As Date = datee
        Dim dayIndex As Integer = Today.DayOfWeek
        If dayIndex < DayOfWeek.Monday Then
            dayIndex += 7
        End If
        Dim dayDiff As Integer = dayIndex - DayOfWeek.Monday
        Dim monday As Date = Today.AddDays(-dayDiff)
        Return monday
    End Function

    Public Function toFirst(datee As Date) As DateTime
        Return DateAdd("m", datepart("m", datee), DateSerial(Year(datee), 1, 1))
    End Function

    Public Function IntToMonday(integ As Integer, param As DateTime) As DateTime

        Dim ret As DateTime

        ret = DateAdd("ww", integ - 1, DateSerial(Year(param), 1, 1))

        Return toMonday(ret)

    End Function

    Public Function IntToFirst(integ As Integer, param As DateTime) As DateTime

        Dim ret As DateTime = New DateTime(Year(param), integ, 1)
        Return ret

    End Function



    Private Sub saveReport(viewer As ReportViewer, typeDeRap As String)


        Dim warnings As Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim bytes As Byte()
        Dim fs As FileStream
        bytes = viewer.LocalReport.Render("PDF", _
        Nothing, mimeType, _
        encoding, extension, streamids, warnings)

        While (FileInUse(tb_pathReport.Text + "/report " + typeDeRap + "_" + DTPicker.Value.ToString("MMMddyyyy", CultureInfo.CreateSpecificCulture("en-US")) + ".pdf") = True)

            MsgBox("A file with the same name is already opened, please close:" + System.Environment.NewLine + "- report " + typeDeRap + "_" + DTPicker.Value.ToString("MMMddyyyy", CultureInfo.CreateSpecificCulture("en-US")) + ".pdf")

        End While



        fs = New FileStream(tb_pathReport.Text + "/report " + typeDeRap + "_" + DTPicker.Value.ToString("MMMddyyyy", CultureInfo.CreateSpecificCulture("en-US")) + ".pdf", FileMode.Create)
        fs.Write(bytes, 0, bytes.Length)
        fs.Close()




    End Sub

    Private Sub localReport_SubreportProcessing(sender As Object, e As SubreportProcessingEventArgs)


        e.DataSources.Add(New ReportDataSource("DataSet_data", setMachineData(True, TypeDePeriode, e.Parameters(0).Values(0))))

        If (TypeDePeriode = "ww" Or TypeDePeriode = "m") Then
            e.DataSources.Add(New ReportDataSource("DataSet_data4", setMachineData(False, TypeDePeriode, e.Parameters(0).Values(0))))
        End If

        e.DataSources.Add(New ReportDataSource("DataSet_4reasons", set4Reason(True, TypeDePeriode, e.Parameters(0).Values(0))))

        If (TypeDePeriode = "ww" Or TypeDePeriode = "m") Then
            e.DataSources.Add(New ReportDataSource("DataSet_4reasons4", set4Reason(False, TypeDePeriode, e.Parameters(0).Values(0))))
        End If

        If (TypeDePeriode = "y") Then
            e.DataSources.Add(New ReportDataSource("DataSet_History", setHistoryDaily(TypeDePeriode, e.Parameters(0).Values(0))))
            e.DataSources.Add(New ReportDataSource("DataSet_timeLine", setTimeLine(e.Parameters(0).Values(0))))
            e.DataSources.Add(New ReportDataSource("DataSet_PartNo", setPartNo(True, TypeDePeriode, e.Parameters(0).Values(0))))
        Else
            e.DataSources.Add(New ReportDataSource("DataSet_History", setHistory(TypeDePeriode, e.Parameters(0).Values(0))))
        End If

    End Sub

    Private Function setTimeLine(machineName As String) As DataTable


        Dim TableName As String = "TimeLine"


        Using sqlConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + CSI_Lib.db_path(True) + "\CSI_Database.mdb")

            Dim adap As OleDbDataAdapter = New OleDbDataAdapter(
                "SELECT iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP' , 'OTHER' , Status) as ReasonName, time_, shift, cycletime from " + machineName + " where status not like '_PARTNO%' and datepart('y',#" + String.Format("{0:dd-MM-yyyy}", DTPicker.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US"))) + "#) - datepart('y',[time_])=0 order by time_  ", sqlConn)


            Dim ds As ReportingDataset = New ReportingDataset()

            Dim returnvalue As Integer = adap.Fill(ds, TableName)

            Return ds.TimeLine

        End Using
    End Function

    Private Function setMachineData(OnePeriod As Boolean, PeriodType As String, machineName As String) As DataTable

        Dim BetweenStr, tableName As String

        If (OnePeriod = True) Then


            BetweenStr = " 0 and 0"


            tableName = "Tbl_DataMachine"

        Else

            BetweenStr = "0 and 4"
            tableName = "Tbl_DataMachine4wk"
        End If



        Using sqlConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + CSI_Lib.db_path(True) + "\CSI_Database.mdb")

            Dim adap As OleDbDataAdapter
            If PeriodType = "y" Then
                adap = New OleDbDataAdapter(
                    "SELECT mchName, Sum(detailCycleTime) AS [Totalcycletime], Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther, shift FROM ( " +
                    "SELECT '" + machineName + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, iif(status = '_COFF', cycletime,0) as COFF, iif(status = '_CON', cycletime,0) as CON, iif(status = '_SETUP', cycletime,0) as SETUP, iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP', cycletime,0) as OTHER, shift  from " + machineName + " " +
                    " ) where (   datepart('" + PeriodType + "',#" + DTPicker.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "#) - datepart('" + PeriodType + "',[detailDate])) between " + BetweenStr + " GROUP BY mchName,shift", sqlConn)
            Else
                adap = New OleDbDataAdapter(
                    "SELECT mchName, Sum(detailCycleTime) AS [Totalcycletime], Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther FROM ( " +
                     "SELECT '" + machineName + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, iif(status = '_COFF', cycletime,0) as COFF, iif(status = '_CON', cycletime,0) as CON, iif(status = '_SETUP', cycletime,0) as SETUP, iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP', cycletime,0) as OTHER, shift  from " + machineName + " " +
                    " ) where (   datepart('" + PeriodType + "',#" + DTPicker.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "#-1) - datepart('" + PeriodType + "',[detailDate]-1)) between " + BetweenStr + " GROUP BY mchName", sqlConn)
            End If

            Dim ds As ReportingDataset = New ReportingDataset()

            Dim returnvalue As Integer = adap.Fill(ds, tableName)


            If (OnePeriod = True) Then
                Return ds.Tbl_DataMachine
            Else
                Return ds.Tbl_DataMachine4wk
            End If

        End Using
    End Function

    Private Function setPartNo(OnePeriod As Boolean, PeriodType As String, machineName As String) As DataTable

        Dim BetweenStr, tableName As String

        If (OnePeriod = True) Then

            BetweenStr = "0 and 0"
            tableName = "tbl_partsNumber"

        Else

            BetweenStr = "0 and 4"
            tableName = "tbl_partsNumber"

        End If

        Using sqlConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + CSI_Lib.db_path(True) + "\CSI_Database.mdb")



            Dim adap As OleDbDataAdapter = New OleDbDataAdapter(
                        "select top 10  '" + machineName + "' as  mchName,  iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP' and CycleTime =0 ,Status, 'OTHER') as partName, Shift from " + machineName + " " +
            "where (Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP' and CycleTime =0) =true and " +
            " (   datepart('" + PeriodType + "',#" + DTPicker.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "#) - datepart('" + PeriodType + "',[Date_]-1)) between " + BetweenStr + " GROUP BY " +
            " '" + machineName + "', Shift , iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP' and CycleTime =0 ,Status, 'OTHER')  order by  sum(cycletime) desc", sqlConn)

            Dim ds As ReportingDataset = New ReportingDataset()

            Dim returnvalue As Integer = adap.Fill(ds, tableName)

            If (OnePeriod = True) Then
                Return ds.tbl_partsNumber
            Else
                Return ds.tbl_partsNumber
            End If
        End Using

    End Function

    Private Function set4Reason(OnePeriod As Boolean, PeriodType As String, machineName As String) As DataTable

        Dim BetweenStr, tableName As String

        If (OnePeriod = True) Then

            BetweenStr = "0 and 0"
            tableName = "Tbl_Top4Reason"

        Else

            BetweenStr = "0 and 4"
            tableName = "Tbl_Top4Reason4wk"

        End If

        Using sqlConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + CSI_Lib.db_path(True) + "\CSI_Database.mdb")



            Dim adap As OleDbDataAdapter = New OleDbDataAdapter(
                        "select top 4  '" + machineName + "' as  mchName,  iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP' and CycleTime >0 ,Status, 'OTHER') as ReasonName,   sum(cycletime) as CycleTime from " + machineName + " " +
            "where (Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP' and CycleTime >0) =true and " +
            " (   datepart('" + PeriodType + "',#" + DTPicker.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "#) - datepart('" + PeriodType + "',[Date_]" + IIf(PeriodType = "y", "", "-1") + ")) between " + BetweenStr + " GROUP BY " +
            " '" + machineName + "',   iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP' and CycleTime >0 ,Status, 'OTHER')  order by  sum(cycletime) desc", sqlConn)

            Dim ds As ReportingDataset = New ReportingDataset()

            Dim returnvalue As Integer = adap.Fill(ds, tableName)

            If (OnePeriod = True) Then
                Return ds.Tbl_Top4Reason
            Else
                Return ds.Tbl_Top4Reason4wk
            End If
        End Using

    End Function
    Private Function getPartNumber() As DataTable

        Using sqlConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + CSI_Lib.db_path(True) + "\CSI_Database.mdb")

            Dim adap As OleDbDataAdapter = New OleDbDataAdapter(
               "SELECT DISTINCT MchName FROM ( " + Qry_Tbl_MachineName + " ) tablee where (status NOT LIKE '_PARTN*') group by MchName ", sqlConn)


            Dim ds As ReportingDataset = New ReportingDataset()

            Dim returnvalue As Integer = adap.Fill(ds, "tbl_MachineName")

            Return ds.Tbl_MachineName

        End Using

    End Function

    Private Function setMachineName() As DataTable

        Using sqlConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + CSI_Lib.db_path(True) + "\CSI_Database.mdb")

            Dim adap As OleDbDataAdapter = New OleDbDataAdapter(
               "SELECT DISTINCT MchName FROM ( " + Qry_Tbl_MachineName + " ) tablee where (status NOT LIKE '_PARTN*') group by MchName ", sqlConn)


            Dim ds As ReportingDataset = New ReportingDataset()

            Dim returnvalue As Integer = adap.Fill(ds, "tbl_MachineName")

            Return ds.Tbl_MachineName

        End Using

    End Function

    Private Function setHistoryDaily(PeriodType As String, machineName As String) As DataTable

        Dim BetweenStr, tableName As String


        BetweenStr = "0 and 13"




        tableName = "Tbl_History18"

        Using sqlConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + CSI_Lib.db_path(True) + "\CSI_Database.mdb")


            Dim adap As OleDbDataAdapter = New OleDbDataAdapter(
            "SELECT  mchName,  Format([detailDate], 'MMM/dd/yyyy') as  WeekNumber ,  Sum(detailCycleTime) AS [Totalcycletime], Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther FROM " +
            "(select '" + machineName + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, iif(status = '_COFF', cycletime,0) as COFF, iif(status = '_CON', cycletime,0) as CON, iif(status = '_SETUP', cycletime,0) as SETUP, iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP', cycletime,0) as OTHER  from " + machineName + " " +
            ") where datepart('" + PeriodType + "',#" + String.Format("{0:dd-MMM-yyyy}", DTPicker.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US"))) + "#) - datepart('" + PeriodType + "',[detailDate]) between " + BetweenStr + " GROUP BY " +
            " mchName,  Format([detailDate], 'MMM/dd/yyyy') order by    Format([detailDate], 'MMM/dd/yyyy')", sqlConn)


            Dim ds As ReportingDataset = New ReportingDataset()

            Dim returnvalue As Integer = adap.Fill(ds, tableName)

            Return ds.Tbl_History18

        End Using

    End Function

    Private Function setHistory(PeriodType As String, machineName As String) As DataTable




        Dim BetweenStr, tableName As String

        BetweenStr = "0 and 17"
        tableName = "Tbl_History18"

        Using sqlConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + CSI_Lib.db_path(True) + "\CSI_Database.mdb")



            Dim adap As OleDbDataAdapter = New OleDbDataAdapter(
            "SELECT  mchName ,  datepart('" + PeriodType + "',[detailDate],2) as weeknumber ,  Sum(detailCycleTime) AS [Totalcycletime], Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther FROM " +
            "(select '" + machineName + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, iif(status = '_COFF', cycletime,0) as COFF, iif(status = '_CON', cycletime,0) as CON, iif(status = '_SETUP', cycletime,0) as SETUP, iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP', cycletime,0) as OTHER  from " + machineName + " " +
            ") where datepart('" + PeriodType + "',#" + String.Format("{0:dd-MM-yyyy}", DTPicker.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US"))) + "#,2) - datepart('" + PeriodType + "',[detailDate],2) between " + BetweenStr + " GROUP BY " +
            " mchName,  datepart('" + PeriodType + "',[detailDate],2)  order by   datepart('" + PeriodType + "',[detailDate],2)", sqlConn)

            'Dim adap As OleDbDataAdapter = New OleDbDataAdapter(
            '"SELECT  mchName , datepart('" + PeriodType + "',[detailDate])  as weeknumber,  Sum(detailCycleTime) AS [Totalcycletime], Sum(CON) as CycleOn, Sum(COFF) as CycleOff, Sum(SETUP) as SumSetup, Sum(OTHER) as SumOther FROM " +
            '"(select '" + machineName + "' as  mchName,   cycletime as detailCycleTime, date_ as detailDate, iif(status = '_COFF', cycletime,0) as COFF, iif(status = '_CON', cycletime,0) as CON, iif(status = '_SETUP', cycletime,0) as SETUP, iif(Status<>'_CON' and Status<>'_COFF' and Status<>'_SETUP', cycletime,0) as OTHER  from " + machineName + " " +
            '") where datepart('" + PeriodType + "',#" + String.Format("{0:dd-MM-yyyy}", DTPicker.Value.ToString("MMM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US"))) + "#) - datepart('" + PeriodType + "',[detailDate]) between " + BetweenStr + " GROUP BY " +
            '" mchName, datepart('" + PeriodType + "',[detailDate])  order by   datepart('" + PeriodType + "',[detailDate])", sqlConn)


            Dim ds As ReportingDataset = New ReportingDataset()

            Dim returnvalue As Integer = adap.Fill(ds, tableName)

            If Not (PeriodType = "y") Then
                For Each b As DataRow In ds.Tbl_History18

                    If (PeriodType = "ww") Then
                        b("WeekNumber") = IntToMonday(b("WeekNumber"), DTPicker.Value)
                    ElseIf (PeriodType = "m") Then
                        b("WeekNumber") = IntToFirst(b("WeekNumber"), DTPicker.Value)
                    End If
                Next
            End If
            Return ds.Tbl_History18

        End Using

    End Function
    Private Sub ReloadReport(viewer As ReportViewer)

        If (viewer.LocalReport.DataSources.Count > 0) Then
            viewer.LocalReport.DataSources.RemoveAt(0)
        End If

        viewer.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", setMachineName()))
        AddHandler viewer.LocalReport.SubreportProcessing, AddressOf localReport_SubreportProcessing

    End Sub

    Private Sub SetupForm_Reporting_MaximizedBoundsChanged(sender As Object, e As EventArgs) Handles Me.MaximizedBoundsChanged

    End Sub


    Private Sub frm_Closing(ByVal sender As Object, _
  ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub TreeView_machine_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeView_machine.AfterCheck
        RemoveHandler TreeView_machine.AfterCheck, AddressOf TreeView_machine_AfterCheck

        Call CheckAllChildNodes(e.Node)

        Dim machines As String() = read_tree()


        If e.Node.Checked Then
            If e.Node.Parent Is Nothing = False Then
                Dim allChecked As Boolean = True
                Call IsEveryChildChecked(e.Node.Parent, allChecked)
                If allChecked Then
                    e.Node.Parent.Checked = True
                    Call ShouldParentsBeChecked(e.Node.Parent)
                End If
            End If
        Else
            Dim parentNode As TreeNode = e.Node.Parent
            While parentNode Is Nothing = False
                parentNode.Checked = False
                parentNode = parentNode.Parent
            End While
        End If

        AddHandler TreeView_machine.AfterCheck, AddressOf TreeView_machine_AfterCheck
    End Sub

    Private Sub IsEveryChildChecked(ByVal parentNode As TreeNode, ByRef checkValue As Boolean)
        For Each node As TreeNode In parentNode.Nodes
            Call IsEveryChildChecked(node, checkValue)
            If Not node.Checked Then
                checkValue = False
            End If
        Next
    End Sub

    Private Sub CheckAllChildNodes(ByVal parentNode As TreeNode)
        For Each childNode As TreeNode In parentNode.Nodes
            childNode.Checked = parentNode.Checked
            CheckAllChildNodes(childNode)
        Next
    End Sub


    Private Sub ShouldParentsBeChecked(ByVal startNode As TreeNode)
        If startNode.Parent Is Nothing = False Then
            Dim allChecked As Boolean = True
            Call IsEveryChildChecked(startNode.Parent, allChecked)
            If allChecked Then
                startNode.Parent.Checked = True
                Call ShouldParentsBeChecked(startNode.Parent)
            End If
        End If
    End Sub




    Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker.DoWork
        Try
            generateReport()
        Catch ex As Exception
            MsgBox(CSI_Lib.TraceMessage("Erreur: " + ex.Message() + "\n" + ex.Source))
        End Try

    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker.RunWorkerCompleted

        PB_loading.Visible = False
        BTN_GenerateReport.Enabled = True
        setControl(True)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles BTN_SetDefaultPath.Click
        If Not Directory.Exists(System.Windows.Forms.Application.StartupPath + "\reports") Then
            Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\reports")
        End If

        tb_pathReport.Clear()
        tb_pathReport.Text = System.Windows.Forms.Application.StartupPath + "\reports"

    End Sub

    Private Sub tb_pathReport_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles tb_pathReport.MouseDoubleClick
        tb_pathReport.SelectAll()
    End Sub
End Class
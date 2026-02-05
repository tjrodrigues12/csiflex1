Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient
Imports OpenNETCF.MTConnect
Imports OpenNETCF.MTConnect.Client
Imports OpenNETCF.MTConnect.EntityClient
Imports OpenNETCF.Web




Public Class MtcADD
    Public IP As String
    Private Sub BTN_Ok_Click(sender As Object, e As EventArgs) Handles BTN_Ok.Click
#Region "Apply Button Code"
        If BTN_Ok.Text = "Apply" Then
            If (TB_AddressIP.Text.Length > 0 And TB_AddressIP.Text.Length > 0) Then
                Dim GoodName As Boolean = True
                For Each row As DataGridViewRow In SetupForm2.grvConnectors.Rows
                    If CB_MachineName.SelectedItem.ToString() = row.Cells(0).Value.ToString() Then
                        MessageBox.Show("This machine is already in CSIFlex")
                        GoodName = False
                    ElseIf CB_eNETMachineName.Text = row.Cells(3).Value.ToString() Then
                        MessageBox.Show("This eNET Machine is already in CSIFlex")
                        GoodName = False
                    End If
                Next
                If (GoodName = True) Then
                    If TB_AddressIP.Text.StartsWith("http") Then
                    Else
                        TB_AddressIP.Text = "http://" & TB_AddressIP.Text
                    End If
                    Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(TB_AddressIP.Text), HttpWebRequest)
                    ' Set the  'Timeout' property of the HttpWebRequest to 2000 milliseconds.
                    myHttpWebRequest.Timeout = 10000
                    ' A HttpWebResponse object is created and is GetResponse Property of the HttpWebRequest associated with it  
                    Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
                    If myHttpWebResponse.StatusCode = HttpStatusCode.OK Then
                        '    If Check_if_exists_inenet() Then
                        If Not Directory.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & CB_MachineName.SelectedItem.ToString() & "\") Then
                            Directory.CreateDirectory(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & CB_MachineName.SelectedItem.ToString() & "\")
                        End If
                        insertmachineindb(CB_MachineName.SelectedItem.ToString(), TB_AddressIP.Text, CB_eNETMachineName.Text)
                        'insertmachineindb(CB_eNETMachineName.Text, TB_AddressIP.Text, CB_MachineName.SelectedItem.ToString())
                        SetupForm2.Load_DGV_CSIConnector()
                        Adv_MTC_cond_edit.MachineName = Convert.ToString(CB_MachineName.Text)
                        Adv_MTC_cond_edit.MachineIP = Convert.ToString(TB_AddressIP.Text)
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        Me.Close()
                        Adv_MTC_cond_edit.TopMost = True
                        Adv_MTC_cond_edit.Show()
                    Else
                        MessageBox.Show("No response from this address")
                    End If
                End If
            Else
                MessageBox.Show("Please select an IP and machine name")
            End If
#End Region
#Region "Update Button Code"
        ElseIf BTN_Ok.Text = "Update" Then
            If (TB_AddressIP.Text.Length > 0 And TB_AddressIP.Text.Length > 0) Then
                Dim GoodName As Boolean = True
                For Each row As DataGridViewRow In SetupForm2.grvConnectors.Rows
                    'If TB_MachineName.Text = row.Cells(0).Value.ToString() Then
                    '    MessageBox.Show("This machine is already in CSIFlex")
                    '    GoodName = False
                    If CB_eNETMachineName.Text = row.Cells(3).Value.ToString() Then
                        MessageBox.Show("This eNET Machine is already in CSIFlex")
                        GoodName = False
                    End If
                Next

                If (GoodName = True) Then
                    If CB_eNETMachineName.Text = String.Empty Then
                        MessageBox.Show("eNET Machine Name should not be empty !")
                    Else
                        updatemachineindb(TB_MachineName.Text, TB_AddressIP.Text, CB_eNETMachineName.Text)
                        SetupForm2.Load_DGV_CSIConnector()
                        'Me.Close()
                        BTN_Ok.Visible = False
                        CB_eNETMachineName.Visible = False
                        TB_eNETMachineName.Text = CB_eNETMachineName.Text
                        TB_eNETMachineName.Visible = True
                        BTN_ChangeENET.Visible = True
                        BTN_More.Visible = True
                        BTN_GetMachineNames.Visible = False
                    End If
                End If
            End If
        End If
#End Region


    End Sub

    Private Function Check_if_exists_inenet() As Boolean

        Try
            Dim mySqlCnt As New MySqlConnection
            mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            mySqlCnt.Open()

            Dim mysql23 As String = "SELECT * FROM csi_database.tbl_renamemachines  where original_name = 'CMTS-1';"
            Dim cmdCreateDeviceTable23 As New MySqlCommand(mysql23, mySqlCnt)
            Dim mysqlReader23 As MySqlDataReader = cmdCreateDeviceTable23.ExecuteReader
            Dim dTable_ As New DataTable()
            dTable_.Load(mysqlReader23)
            mySqlCnt.Close()
            If dTable_.Rows.Count <> 0 Then
                Return True
            Else
                MessageBox.Show("This machine does not exist in enet, you have to add it in enet first, as ftp machine.", "ERROR !")
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show("Unable to check the database" & ex.Message)
            Return False
        End Try
    End Function
    Private Sub updatemachineindb(MachineName As String, MachineIP As String, eNETMachineName As String)
        Dim mysqlConn As New MySqlConnection
        Dim mysqlCommand As New MySqlCommand
        mysqlConn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString
        Try
            mysqlConn.Open()
            'Dim updatemachines As New MySqlCommand("UPDATE IGNORE `csi_auth`.`tbl_csiconnector` SET `eNETMachineName` ='" & eNETMachineName & "',`MachineIP`='" & MachineIP & "' WHERE `MachineName`='" & MachineName & "';", mysqlConn)
            Dim updatemachines As New MySqlCommand("UPDATE IGNORE `csi_auth`.`tbl_csiconnector` SET `eNETMachineName` ='" & eNETMachineName & "' WHERE `MachineName`='" & MachineName & "' AND `MachineIP`='" & MachineIP & "';", mysqlConn)
            updatemachines.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Update Querry Error :" + ex.Message)
        End Try
    End Sub
    Private Sub insertmachineindb(MachineName As String, MachineIP As String, eNETMachineName As String)
        If MachineIP = String.Empty Or MachineName = String.Empty Or eNETMachineName = String.Empty Then
            MessageBox.Show("IP Addrees, Machine Name or eNET Machine Name  should not be empty !")
        Else
            Dim info As String = MachineName & " : " & MachineIP ' LB_MachineAddress.Text
            Dim colonindex As Integer = info.IndexOf(":", info.IndexOf(":") + 1)
            If (colonindex > info.IndexOf(":")) Then
                colonindex = info.IndexOf(":")
                Dim newname As String = info.Substring(0, colonindex).Trim
                Dim newip As String = info.Substring(colonindex + 1, info.Length - colonindex - 1).Trim


                Dim mysqlConn As New MySqlConnection
                Dim mysqlCommand As New MySqlCommand

                mysqlConn.ConnectionString = CSI_Library.CSI_Library.MySqlConnectionString
                mysqlConn.Open()

                'mysqlCommand = New MySqlCommand("UPDATE CSI_auth.tbl_CSIConnector SET MachineName = '" + newname + "', MachineIP='" + newip + "' " +
                '"WHERE MachineName = '" & MachineName & "' and MachineIP = '" & MachineIP & "' ", mysqlConn)


                Dim cmd4 As New MySqlCommand("INSERT INTO CSI_auth.tbl_CSIConnector VALUES ('" & MachineName & "', '" & MachineIP & "', '" & CB_Connector.SelectedItem.ToString() & "', 'CYCLE ON','" & "" & "','msg','cn5','Fovr','c3','" & eNETMachineName & "')", mysqlConn)
                cmd4.ExecuteNonQuery()


                ' mysqlCommand.ExecuteNonQuery()

                mysqlConn.Close()
            Else
                MessageBox.Show("Column not found, unable to save name and IP")
            End If
        End If

    End Sub

    'Public devicename As String = ""

    Private Sub CB_Connector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Connector.SelectedIndexChanged
        If (CB_Connector.SelectedItem.ToString() = "MTConnect") Then
            PB_Connector.Visible = False
            PB_Connector.Image = Global.CSI_Reporting_Application.My.Resources.MTConnectLogo
            PB_Connector.Refresh()
            PB_Connector.Visible = True
        ElseIf (CB_Connector.SelectedItem.ToString() = "Focas") Then
            PB_Connector.Visible = False
            PB_Connector.Image = Global.CSI_Reporting_Application.My.Resources.FANUC
            PB_Connector.Refresh()
            PB_Connector.Visible = True
        End If
    End Sub
    Public Sub GetAllENETMachines() 'This Function is used to load Ehub Config Files in Database 
        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try
            mySqlCnt.Open()
            Dim ehubfilepath As String = ""
            Dim Monsetupfilepath As String = ""
            Dim allconst As New AllStringConstants.StringConstant
            ehubfilepath = allconst.SERVER_ENET_PATH & allconst.EHUB_CONF_FILE_NAME
            Monsetupfilepath = allconst.SERVER_ENET_PATH & allconst.MON_SETUP_FILE_NAME
            If File.Exists(ehubfilepath) And File.Exists(Monsetupfilepath) Then
                Dim fileData() As String = File.ReadAllLines(ehubfilepath) 'Read All Lines from EhubConf file
                Dim MonData() As String = File.ReadAllLines(Monsetupfilepath) 'Read All Lines from MonSetUp file
                Dim splitInput As New List(Of String)
                Dim splitMonData As New List(Of String)
                For Each line As String In fileData
                    If line.Contains("NM:") Then
                        splitInput.Add(line.Replace("NM:", "")) 'Name of the ENET Machine
                    ElseIf line.Contains("FI:") Then
                        splitInput.Add(line.Replace("FI:", "")) 'Connection Type_ This value doesn't define all parameters but when it's 1 then it indicates FTP Connection
                    End If
                Next
                For Each line1 As String In MonData
                    If line1.Contains("ON:") Then 'Machine Monitoring Status If 1 then Monitoring is ON IF 0 then Monitoring is OFF
                        splitMonData.Add(line1.Replace("ON:", ""))
                    ElseIf line1.Contains("TH:") Then
                        splitMonData.Add(line1.Split(":")(1)) 'Stores the TH Parameter Values
                    ElseIf line1.Contains("DD:") Then
                        Dim FileFTPName As String = line1.Split(",")(1)
                        splitMonData.Add(FileFTPName)
                    ElseIf line1.Contains("DA") Then
                        splitMonData.Add(line1.Split(":")(1))
                    End If
                Next
                Dim fileLength As Integer = splitInput.Count
                Dim MonFilelength As Integer = splitMonData.Count
                Dim k As Integer, l As Integer, p As Integer, q As Integer, querycount As Integer, Counter As Integer, r As Integer
                Dim MonitoringId As String = ""
                Dim MachineName As String = ""
                Dim StringMonId As String = ""
                Dim Machine_Filename As String = ""
                Dim MachineLabel As String = ""
                Dim TH_Value As Integer
                Dim Monstate As Integer
                Dim FTPFileName As String
                Dim EnetDept As String
                'Dim MonSetUpId As Integer, 
                Dim Con_type As Integer
                Dim InsertQuerry As String
                k = 1
                l = 1
                p = 0
                q = 1
                r = 0
                querycount = 0
                Counter = 0
                While q < fileLength
                    If k <= 16 And l <= 8 Then
                        MonitoringId = Convert.ToString(k) + "," + Convert.ToString(l)
                        MachineLabel = "MCH_" + Convert.ToString(k) + Convert.ToString(l)
                        Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & ".SYS_"
                        StringMonId = Convert.ToString(MonitoringId)
                        MachineName = Convert.ToString(splitInput(p))
                        'MonSetUpId = 0
                        If (Convert.ToInt32(splitInput(q))) = 1 Then 'Machines whose (FI:1) For future it will be called as Connection Type 5 where  (FI:1,EH:0 and EO:0) 
                            Con_type = 5
                        Else
                            Con_type = 0
                        End If
                        Monstate = Convert.ToInt32(splitMonData(r))
                        TH_Value = Convert.ToInt32(splitMonData(r + 1)) 'Represent TH value
                        FTPFileName = splitMonData(r + 2).ToString()
                        EnetDept = splitMonData(r + 3).ToString()
                        If TH_Value = 1 Then
                            '2 Head MonitorData010.SYS_,MonitorData011.SYS_
                            Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & "0" & ".SYS_" & "," & "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & "1" & ".SYS_"
                            InsertQuerry = "INSERT into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`,`Monstate`,`machine_label`,`ftpfilename`,`CurrentStatus`,`CurrentPartNumber`,`EnetDept`,`TH_State`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "','" & Monstate & "','" & MachineLabel & "','" & FTPFileName & "','','','" & EnetDept & "','" & TH_Value & "')ON DUPLICATE KEY UPDATE `machine_name`='" & MachineName & "',`Con_type`='" & Con_type & "';"
                        ElseIf TH_Value = 2 Then
                            '2 Pallet MonitorData010.SYS_,MonitorData011.SYS_
                            Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & "0" & ".SYS_" & "," & "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & "1" & ".SYS_"
                            InsertQuerry = "INSERT into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`,`Monstate`,`machine_label`,`ftpfilename`,`CurrentStatus`,`CurrentPartNumber`,`EnetDept`,`TH_State`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "','" & Monstate & "','" & MachineLabel & "','" & FTPFileName & "','','','" & EnetDept & "','" & TH_Value & "')ON DUPLICATE KEY UPDATE `machine_name`='" & MachineName & "',`Con_type`='" & Con_type & "';"
                        Else
                            Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & ".SYS_"
                            InsertQuerry = "INSERT into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`,`Monstate`,`machine_label`,`ftpfilename`,`CurrentStatus`,`CurrentPartNumber`,`EnetDept`,`TH_State`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "','" & Monstate & "','" & MachineLabel & "','" & FTPFileName & "','','','" & EnetDept & "','" & TH_Value & "')ON DUPLICATE KEY UPDATE `machine_name`='" & MachineName & "',`Con_type`='" & Con_type & "';"
                        End If
                        'InsertQuerry = "REPLACE into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "');"
                        Dim cmdEhubConfInsert = New MySqlCommand(InsertQuerry, mySqlCnt)
                        querycount = cmdEhubConfInsert.ExecuteNonQuery()
                        'querycount = Convert.ToInt32(cmdEhubConfInsert.ExecuteScalar())
                        'querycount = mysql_affected_rows()
                        If querycount > 1 Then
                            Counter += 1
                        End If
                        l += 1
                        p += 2
                        q += 2
                        r += 4
                        If l > 8 Then
                            l = 1
                            k += 1
                        End If
                    End If
                End While
                If Counter > 0 Then  'This Logic checking if there is any change in EHUb Config File 
                    'MessageBox.Show("Please Change the eNET Machine Name corresponfing to your Machine Name !")
                Else
                    'MessageBox.Show("No  Updates in EHubConfig File !")
                End If
            Else
                MessageBox.Show("EhubConf.sys Or MonSetUp.sys file is not exists or you don't have authority to access it")
            End If
        Catch ex As Exception
            MessageBox.Show("EhubConf Table has error while inserting data : " & ex.Message)
        Finally
            mySqlCnt.Close()
        End Try
    End Sub
    'Public Sub GetAllENETMachines() 'This Function is used to load Ehub Config Files in Database 
    '    Dim mySqlCnt As New MySqlConnection
    '    mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
    '    Try
    '        mySqlCnt.Open()
    '        Dim ehubfilepath As String = ""
    '        Dim Monsetupfilepath As String = ""
    '        Dim allconst As New AllStringConstants.StringConstant
    '        ehubfilepath = allconst.SERVER_ENET_PATH & allconst.EHUB_CONF_FILE_NAME
    '        Monsetupfilepath = allconst.SERVER_ENET_PATH & allconst.MON_SETUP_FILE_NAME
    '        If File.Exists(ehubfilepath) And File.Exists(Monsetupfilepath) Then
    '            Dim fileData() As String = File.ReadAllLines(ehubfilepath) 'Read All Lines from EhubConf file
    '            Dim MonData() As String = File.ReadAllLines(Monsetupfilepath) 'Read All Lines from MonSetUp file
    '            Dim splitInput As New List(Of String)
    '            Dim splitMonData As New List(Of String)
    '            For Each line As String In fileData
    '                If line.Contains("NM:") Then
    '                    splitInput.Add(line.Replace("NM:", "")) 'Name of the ENET Machine
    '                ElseIf line.Contains("FI:") Then
    '                    splitInput.Add(line.Replace("FI:", "")) 'Connection Type_ This value doesn't define all parameters but when it's 1 then it indicates FTP Connection
    '                End If
    '            Next
    '            For Each line1 As String In MonData
    '                If line1.Contains("ON:") Then 'Machine Monitoring Status If 1 then Monitoring is ON IF 0 then Monitoring is OFF
    '                    splitMonData.Add(line1.Replace("ON:", ""))
    '                ElseIf line1.Contains("DD:") Then
    '                    Dim FileFTPName As String = line1.Split(",")(1)
    '                    splitMonData.Add(FileFTPName)
    '                ElseIf line1.Contains("DA") Then
    '                    splitMonData.Add(line1.Split(":")(1))
    '                End If
    '            Next
    '            Dim fileLength As Integer = splitInput.Count
    '            Dim MonFilelength As Integer = splitMonData.Count
    '            Dim k As Integer, l As Integer, p As Integer, q As Integer, querycount As Integer, Counter As Integer, r As Integer
    '            Dim MonitoringId As String = ""
    '            Dim MachineName As String = ""
    '            Dim StringMonId As String = ""
    '            Dim Machine_Filename As String = ""
    '            Dim MachineLabel As String = ""
    '            Dim Monstate As Integer
    '            Dim FTPFileName As String
    '            Dim EnetDept As String
    '            'Dim MonSetUpId As Integer, 
    '            Dim Con_type As Integer
    '            Dim InsertQuerry As String
    '            k = 1
    '            l = 1
    '            p = 0
    '            q = 1
    '            r = 0
    '            querycount = 0
    '            Counter = 0
    '            While q < fileLength
    '                If k <= 16 And l <= 8 Then
    '                    MonitoringId = Convert.ToString(k) + "," + Convert.ToString(l)
    '                    MachineLabel = "MCH_" + Convert.ToString(k) + Convert.ToString(l)
    '                    Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & ".SYS_"
    '                    StringMonId = Convert.ToString(MonitoringId)
    '                    MachineName = Convert.ToString(splitInput(p))
    '                    'MonSetUpId = 0
    '                    If (Convert.ToInt32(splitInput(q))) = 1 Then 'Machines whose (FI:1) For future it will be called as Connection Type 5 where  (FI:1,EH:0 and EO:0) 
    '                        Con_type = 5
    '                    Else
    '                        Con_type = 0
    '                    End If
    '                    Monstate = Convert.ToInt32(splitMonData(r))
    '                    FTPFileName = splitMonData(r + 1).ToString()
    '                    EnetDept = splitMonData(r + 2).ToString()
    '                    'InsertQuerry = "REPLACE into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "');"
    '                    InsertQuerry = "INSERT into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`,`Monstate`,`machine_label`,`ftpfilename`,`CurrentStatus`,`CurrentPartNumber`,`EnetDept`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "','" & Monstate & "','" & MachineLabel & "','" & FTPFileName & "','','','" & EnetDept & "')ON DUPLICATE KEY UPDATE `machine_name`='" & MachineName & "',`Con_type`='" & Con_type & "';"
    '                    Dim cmdEhubConfInsert = New MySqlCommand(InsertQuerry, mySqlCnt)
    '                    querycount = cmdEhubConfInsert.ExecuteNonQuery()
    '                    'querycount = Convert.ToInt32(cmdEhubConfInsert.ExecuteScalar())
    '                    'querycount = mysql_affected_rows()
    '                    If querycount > 1 Then
    '                        Counter += 1
    '                    End If
    '                    l += 1
    '                    p += 2
    '                    q += 2
    '                    r += 3
    '                    If l > 8 Then
    '                        l = 1
    '                        k += 1
    '                    End If
    '                End If
    '            End While
    '            If Counter > 0 Then  'This Logic checking if there is any change in EHUb Config File 
    '                'MessageBox.Show("Please Change the eNET Machine Name corresponfing to your Machine Name !")
    '            Else
    '                'MessageBox.Show("No  Updates in EHubConfig File !")
    '            End If
    '        Else
    '            MessageBox.Show("EhubConf.sys Or MonSetUp.sys file is not exists or you don't have authority to access it")
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("EhubConf Table has error while inserting data : " & ex.Message)
    '    Finally
    '        mySqlCnt.Close()
    '    End Try
    'End Sub
    'Public Sub GetAllENETMachines() 'This Function is used to load Ehub Config Files in Database 
    '    Dim mySqlCnt As New MySqlConnection
    '    mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
    '    Try
    '        mySqlCnt.Open()
    '        Dim ehubfilepath As String = ""
    '        Dim Monsetupfilepath As String = ""
    '        Dim allconst As New AllStringConstants.StringConstant
    '        ehubfilepath = allconst.SERVER_ENET_PATH & allconst.EHUB_CONF_FILE_NAME
    '        Monsetupfilepath = allconst.SERVER_ENET_PATH & allconst.MON_SETUP_FILE_NAME
    '        If File.Exists(ehubfilepath) And File.Exists(Monsetupfilepath) Then
    '            Dim fileData() As String = File.ReadAllLines(ehubfilepath) 'Read All Lines from EhubConf file
    '            Dim MonData() As String = File.ReadAllLines(Monsetupfilepath) 'Read All Lines from MonSetUp file
    '            Dim splitInput As New List(Of String)
    '            Dim splitMonData As New List(Of String)
    '            For Each line As String In fileData
    '                If line.Contains("NM:") Then
    '                    splitInput.Add(line.Replace("NM:", "")) 'Name of the ENET Machine
    '                ElseIf line.Contains("FI:") Then
    '                    splitInput.Add(line.Replace("FI:", "")) 'Connection Type_ This value doesn't define all parameters but when it's 1 then it indicates FTP Connection
    '                End If
    '            Next
    '            For Each line1 As String In MonData
    '                If line1.Contains("ON:") Then 'Machine Monitoring Status If 1 then Monitoring is ON IF 0 then Monitoring is OFF
    '                    splitMonData.Add(line1.Replace("ON:", ""))
    '                End If
    '            Next
    '            Dim fileLength As Integer = splitInput.Count
    '            Dim MonFilelength As Integer = splitMonData.Count
    '            Dim k As Integer, l As Integer, p As Integer, q As Integer, querycount As Integer, Counter As Integer, r As Integer
    '            Dim MonitoringId As String = ""
    '            Dim MachineName As String = ""
    '            Dim StringMonId As String = ""
    '            Dim Machine_Filename As String = ""
    '            Dim Monstate As Integer
    '            'Dim MonSetUpId As Integer, 
    '            Dim Con_type As Integer
    '            Dim InsertQuerry As String
    '            k = 1
    '            l = 1
    '            p = 0
    '            q = 1
    '            r = 0
    '            querycount = 0
    '            Counter = 0
    '            While q < fileLength
    '                If k <= 16 And l <= 8 Then
    '                    MonitoringId = Convert.ToString(k) + "," + Convert.ToString(l)
    '                    Machine_Filename = "MonitorData" & (k - 1).ToString("x") & Convert.ToString(l - 1) & ".SYS_"
    '                    StringMonId = Convert.ToString(MonitoringId)
    '                    MachineName = Convert.ToString(splitInput(p))
    '                    'MonSetUpId = 0
    '                    If (Convert.ToInt32(splitInput(q))) = 1 Then 'Machines whose (FI:1) For future it will be called as Connection Type 5 where  (FI:1,EH:0 and EO:0) 
    '                        Con_type = 5
    '                    Else
    '                        Con_type = 0
    '                    End If
    '                    Monstate = Convert.ToInt32(splitMonData(r))
    '                    'InsertQuerry = "REPLACE into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "');"
    '                    InsertQuerry = "INSERT into csi_auth.tbl_ehub_conf(`monitoring_id`,`machine_name`,`Con_type`,`monitoring_filename`,`Monstate`)VALUES('" & StringMonId & "','" & MachineName & "','" & Con_type & "','" & Machine_Filename & "','" & Monstate & "')ON DUPLICATE KEY UPDATE `machine_name`='" & MachineName & "',`Con_type`='" & Con_type & "';"
    '                    Dim cmdEhubConfInsert = New MySqlCommand(InsertQuerry, mySqlCnt)
    '                    querycount = cmdEhubConfInsert.ExecuteNonQuery()
    '                    'querycount = Convert.ToInt32(cmdEhubConfInsert.ExecuteScalar())
    '                    'querycount = mysql_affected_rows()
    '                    If querycount > 1 Then
    '                        Counter += 1
    '                    End If
    '                    l += 1
    '                    p += 2
    '                    q += 2
    '                    r += 1
    '                    If l > 8 Then
    '                        l = 1
    '                        k += 1
    '                    End If
    '                End If
    '            End While
    '            If Counter > 0 Then  'This Logic checking if there is any change in EHUb Config File 
    '                'MessageBox.Show("Please Change the eNET Machine Name corresponfing to your Machine Name !")
    '            Else
    '                'MessageBox.Show("No  Updates in EHubConfig File !")
    '            End If
    '        Else
    '            MessageBox.Show("EhubConf.sys Or MonSetUp.sys file is not exists or you don't have authority to access it")
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("EhubConf Table has error while inserting data : " & ex.Message)
    '    Finally
    '        mySqlCnt.Close()
    '    End Try
    'End Sub
    Private Sub LoadENETMachinestoCombobox() 'This function loads CB_eNETMachineName with all eNETMachines who have FTP Connections means  Con_type = 5
        Dim mySqlCnt As New MySqlConnection
        mySqlCnt = New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
        Try
            mySqlCnt.Open()
            Dim SelectEhubQuery As String
            SelectEhubQuery = "SELECT `tbl_ehub_conf`.`machine_name` FROM `csi_auth`.`tbl_ehub_conf` WHERE `tbl_ehub_conf`.`Con_type` ='5' AND Monstate ='1';"
            Dim cmdSelectEhubQuery = New MySqlCommand(SelectEhubQuery, mySqlCnt)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmdSelectEhubQuery)
            Dim ds = New DataSet()
            da.Fill(ds)
            'Dim dt As New DataTable("tbl_ehub_conf")
            'da.Fill(dt)
            'If dt.Rows.Count > 0 Then
            CB_eNETMachineName.DataSource = ds.Tables(0)
            CB_eNETMachineName.DisplayMember = "eNET Machine Name"
            CB_eNETMachineName.ValueMember = "machine_name"
            CB_eNETMachineName.SelectedIndex = -1
            'End If
        Catch ex As Exception
            MessageBox.Show("EhubConf Table Connection Error : " & ex.Message)
        Finally
            mySqlCnt.Close()
        End Try
    End Sub
    Private Sub MtcADD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CB_Connector.SelectedIndex = 0
        GetAllENETMachines()
        LoadENETMachinestoCombobox()
    End Sub

    Public Sub New(ByVal FormType As String, ByVal ConnectionType As String, ByVal IPAddress As String, ByVal MachineName As String, ByVal eNETMachineName As String)
        ' This call is required by the designer.
        InitializeComponent()
        LB_FieldOPENER.Text = FormType
        If LB_FieldOPENER.Text = "ButtonAddClick" Then
            '  MessageBox.Show("Add Button clicked in CSI Connector")
            'BTN_More.Enabled = False
            CB_eNETMachineName.Enabled = True
            BTN_More.Visible = False
            BTN_Ok.Enabled = False
        ElseIf LB_FieldOPENER.Text = "ButtonEditClick" Then
            ' MessageBox.Show("Edit Button clicked in CSI Connector")
            BTN_More.Enabled = True
            BTN_More.Visible = True
            BTN_Ok.Visible = False
            BTN_ChangeENET.Visible = True
            BTN_GetMachineNames.Visible = False
            CB_MachineName.Visible = False
            CB_MachineName.Enabled = False
            CB_eNETMachineName.Enabled = True
            CB_eNETMachineName.Visible = False
            CB_Connector.Visible = False
            TB_AddressIP.Enabled = False
            TB_ConnectorType.Visible = True
            TB_eNETMachineName.Visible = True
            TB_MachineName.Visible = True
            TB_ConnectorType.Text = ConnectionType
            TB_MachineName.Text = MachineName
            TB_eNETMachineName.Text = eNETMachineName
            CB_Connector.Text = ConnectionType
            TB_AddressIP.Text = IPAddress
#If False Then
            'Adv_MTC_cond_edit.MachineName = MachineName
            Adv_MTC_cond_edit.MachineName = eNETMachineName
            Adv_MTC_cond_edit.MachineIP = IPAddress

            If Not Directory.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & MachineName & "\") Then
                Directory.CreateDirectory(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & MachineName & "\")
            End If

            If Not File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & MachineName & "\ip.csys") Then
                Using writer As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & MachineName & "\ip.csys")
                    writer.WriteLine(Convert.ToString(IPAddress))
                    writer.Close()
                End Using
            End If

            Adv_MTC_cond_edit.Show()
#End If
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub BTN_GetMachineNames_Click(sender As Object, e As EventArgs) Handles BTN_GetMachineNames.Click
        If (TB_AddressIP.Text.Length > 0) Then
            Try
                CB_MachineName.Items.Clear()
                Dim m_client As EntityClient
                m_client = New EntityClient(TB_AddressIP.Text)

                Dim Devices
                Devices = m_client.Probe()
                Dim dev As DeviceCollection = Devices
                If (dev.Count > 0) Then
                    For Each machinename In dev
                        CB_MachineName.Items.Add(machinename.Name)
                    Next
                    BTN_Ok.Enabled = True
                    CB_MachineName.Enabled = True
                    CB_eNETMachineName.Enabled = True
                    CB_MachineName.SelectedIndex = 0
                Else
                    MessageBox.Show("No device found from this IP")
                    CB_MachineName.Enabled = False
                    CB_eNETMachineName.Enabled = False
                    BTN_Ok.Enabled = False
                End If
            Catch ex As Exception
                MessageBox.Show("Unable to retrieve any information from this IP")
                CB_MachineName.Enabled = False
                CB_eNETMachineName.Enabled = False
                BTN_Ok.Enabled = False
            End Try
        Else
            MessageBox.Show("Please input an IP address")
        End If
    End Sub

    Private Sub BTN_Cancel_Click(sender As Object, e As EventArgs) Handles BTN_Cancel.Click
        Me.Close()
    End Sub

    Private Sub BTN_More_Click(sender As Object, e As EventArgs) Handles BTN_More.Click
        'Adv_MTC_cond_edit.MachineName = MachineName
        'Adv_MTC_cond_edit.MachineName = CB_MachineName.SelectedItem.ToString()
        Adv_MTC_cond_edit.MachineName = Convert.ToString(TB_MachineName.Text)
        Adv_MTC_cond_edit.MachineIP = Convert.ToString(TB_AddressIP.Text)

        If Not Directory.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & Convert.ToString(TB_MachineName.Text) & "\") Then
            Directory.CreateDirectory(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & Convert.ToString(TB_MachineName.Text) & "\")
        End If

        If Not File.Exists(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & Convert.ToString(TB_MachineName.Text) & "\ip.csys") Then
            Using writer As StreamWriter = New StreamWriter(CSI_Library.CSI_Library.serverRootPath & "\sys\Conditions\" & Convert.ToString(TB_MachineName.Text) & "\ip.csys")
                writer.WriteLine(Convert.ToString(TB_AddressIP.Text))
                writer.Close()
            End Using
        End If
        Me.Close()
        Adv_MTC_cond_edit.TopMost = True
        Adv_MTC_cond_edit.Show()
    End Sub

    Private Sub BTN_ChangeENET_Click(sender As Object, e As EventArgs) Handles BTN_ChangeENET.Click
        BTN_Ok.Visible = True
        BTN_Ok.Text = "Update"
        CB_eNETMachineName.Visible = True
        CB_eNETMachineName.Text = TB_eNETMachineName.Text
        'TB_AddressIP.Enabled = True
        TB_eNETMachineName.Visible = False
        BTN_ChangeENET.Visible = False
        BTN_More.Visible = False
        BTN_GetMachineNames.Visible = False
    End Sub
End Class
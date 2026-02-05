Imports CSI_Library.CSI_Library
Imports MySql.Data.MySqlClient
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks


Namespace AllStringConstants
    Public Class StringConstant
        Public ServerPath As New CSI_Library.CSI_Library(True)
        Public SERVER_ENET_PATH As String = ServerPath.eNET_path() 'ENET Folder Path
        Public MON_SETUP_FILE_NAME As String = "\_SETUP\MonSetup.sys" 'MonSetup Folder Path
        Public SHIFT_SETUP_FILE_NAME As String = "\_SETUP\ShiftSetup2.sys" 'ShiftSetup Folder Path
        Public EHUB_CONF_FILE_NAME As String = "\_SETUP\eHUBConf.sys" 'EhubConfig File Folder Path
        Public MONITORING_FOLDER_PATH As String = "\_MONITORING\"
        Public TMP_FOLDER_PATH As String = "\_TMP\"
    End Class
End Namespace

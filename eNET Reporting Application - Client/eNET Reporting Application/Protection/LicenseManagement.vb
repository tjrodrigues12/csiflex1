Imports System.IO
Imports CSI_Library
Imports CSIFLEX.Database.Access
Imports CSIFLEX.License.Data
Imports CSIFLEX.Utilities
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json


Public Class LicenseManagement

    Dim licenseClient As License

    Dim licenseLib As CSILicenseLibrary

    Dim companyId As Guid

    Dim expiryDays As Integer = 30

    Dim requestFile As String = "CSIFLEXLicenseRequest.bin"

    Dim isLicenseValid As Boolean = False

    Dim strConnection As String = ""

    Sub New(strConnection_ As String)

        ' This call is required by the designer.
        InitializeComponent()

        strConnection = strConnection_

        licenseLib = New CSILicenseLibrary(strConnection)

        licenseLib.LoadLicense(LicenseProducts.CSIFLEXClient_Server, licenseClient)

    End Sub


    Private Sub LicenseManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblComputerName.Text = LocalMachineInformation.MyComputerName()
        lblComputerId.Text = LocalMachineInformation.ComputerId()

        Dim license As DataTable = MySqlAccess.GetDataTable($"SELECT CompanyId, CompanyName FROM csi_auth.tbl_license ORDER BY LicenseStatus, LicenseDate DESC", strConnection)

        If license.Rows.Count > 0 Then
            companyId = New Guid(license.Rows(0)("CompanyId").ToString())
            txtCompanyName.Text = license.Rows(0)("CompanyName").ToString()
            lblCompanyName.Text = license.Rows(0)("CompanyName").ToString()
        Else
            companyId = New Guid()
        End If
        lblCompanyId.Text = companyId.ToString()

        LoadClient()

        EditMode(True)

        If Not licenseClient.Status = "NotFound" Then
            btnTempLicense.Enabled = False
        End If

    End Sub


    Private Sub LoadClient()

        If IsNothing(licenseClient) Then
            licenseLib.LoadLicense(LicenseProducts.CSIFLEXClient_Server, licenseClient)
        End If

        If Not licenseClient.Status = "NotFound" Then

            lblCompanyName.Text = licenseClient.CompanyName

            lblServerLicType.ForeColor = Color.Blue
            If licenseClient.LicenseType = "Temporary" Then
                lblServerLicType.ForeColor = Color.Red
            End If
            lblServerLicType.Text = licenseClient.LicenseType

            lblServerExpiry.ForeColor = Color.Blue
            If licenseClient.ExpiryDate.ToString("yyyy-MM-dd") = "2030-01-01" Then
                lblServerExpiry.Text = "Permanent"
            Else
                lblServerExpiry.Text = licenseClient.ExpiryDate.ToString("MMM-dd-yyyy")
            End If

            lblContactEmail.Text = licenseClient.ContactEmail
            lblContactName.Text = licenseClient.ContactName
            lblContactPhone.Text = licenseClient.ContactPhone

            txtCompanyName.Text = licenseClient.CompanyName
            txtContactEmail.Text = licenseClient.ContactEmail
            txtContactPerson.Text = licenseClient.ContactName
            txtContactPhone.Text = licenseClient.ContactPhone

            If licenseClient.Status.ToUpper() = "VALID" Then

                isLicenseValid = True

                Dim diff = DateDiff(DateInterval.Day, Today, licenseClient.ExpiryDate)

                If diff <= 10 Then
                    lblServerExpiry.ForeColor = Color.Red
                    lblServerStatus.ForeColor = Color.Red
                    lblServerStatus.Text = $"Your CSIFLEX license will expiry in {diff} days. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                Else
                    lblServerStatus.Text = "Active"
                End If
            Else

                lblServerStatus.ForeColor = Color.Red

                If licenseClient.Status.StartsWith("ERR02") Or licenseClient.Status.StartsWith("ERR03") Then
                    lblServerStatus.Text = $"{licenseClient.Status.Substring(0, 5)}. Your CSIFLEX license is invalid or corrupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                ElseIf licenseClient.Status.StartsWith("EXP01") Then
                    lblServerStatus.Text = licenseClient.Status.Split(";")(1)
                ElseIf licenseClient.Status.StartsWith("EXP02") Then
                    lblServerStatus.Text = licenseClient.Status.Split(";")(1)
                    isLicenseValid = True
                Else
                    lblServerStatus.Text = $"{licenseClient.Status.Substring(0, 5)}. Error trying to validate your CSIFLEX. Close the application and try again. If the continues please, contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                End If
            End If

        ElseIf licenseLib.HasPreviousLicense(LicenseProducts.CSIFLEXClient_Server) Then

            lblServerStatus.Text = "A previous installation of CSIFLEX Client has been detected. Your license file is invalid or currupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
            lblServerStatus.ForeColor = Color.Red
        Else

            lblServerLicType.Text = "No license"
            lblServerExpiry.Text = ""
            lblServerStatus.Text = "Please, fill the form to request a license."
            lblServerStatus.ForeColor = Color.Red
        End If

    End Sub


    Private Sub btnTempLicense_Click(sender As Object, e As EventArgs) Handles btnTempLicense.Click

        If String.IsNullOrEmpty(txtCompanyName.Text) Then
            MessageBox.Show("You must to inform the Company name")
            Return
        End If

        Dim tempLicense As New LicenseBase()
        Dim hadSuccess As Boolean = True
        Dim expiryDate As DateTime = Today.AddDays(expiryDays)

        tempLicense.CompanyId = companyId
        tempLicense.LicenseDate = Today
        tempLicense.ComputerId = lblComputerId.Text
        tempLicense.CompanyName = txtCompanyName.Text
        tempLicense.LicenseType = "Temporary"

        'Generate the CSIFLEX Client license if it doesn't exist
        If licenseClient.Status = "NotFound" Then
            tempLicense.UniqueId = Guid.NewGuid()

            tempLicense.LicensesQuantity = 1
            tempLicense.ProductName = LicenseProducts.CSIFLEXClient_Server
            tempLicense.ExpiryDate = expiryDate

            Try
                tempLicense = licenseLib.GenerateLicense(tempLicense)
                SaveLicense(tempLicense, True)
                tempLicense.CopyPropertiesTo(Of LicenseBase)(licenseClient)
                licenseClient.ContactName = txtContactPerson.Text
                licenseClient.ContactEmail = txtContactEmail.Text
                licenseClient.ContactPhone = txtContactPhone.Text

                'LoadClient()
            Catch ex As Exception
                hadSuccess = False
            End Try
        Else
            expiryDate = licenseClient.ExpiryDate
        End If

        LicenseManagement_Load(Me, New EventArgs())

        If hadSuccess Then
            isLicenseValid = True
            MessageBox.Show($"License generated with success.")
        Else
            MessageBox.Show($"Error trying to generate the temporary license.")
        End If

    End Sub


    Private Sub btnRequestLicense_Click(sender As Object, e As EventArgs) Handles btnRequestLicense.Click

        Dim msg As New Text.StringBuilder()
        msg.Append("Do you confirm the request to the follow product(s)?")
        msg.Append(vbNewLine & vbNewLine & "- CSIFLEX Client")

        If Not MessageBox.Show(msg.ToString(), "Confirme request license", MessageBoxButtons.OKCancel) = DialogResult.OK Then
            Return
        End If

        Dim licenseReq As New CSIFLEX.License.Data.LicenseRequest()

        licenseReq.UniqueId = Guid.NewGuid()
        licenseReq.CompanyId = companyId
        licenseReq.CompanyName = txtCompanyName.Text
        licenseReq.ContactEmail = txtContactEmail.Text
        licenseReq.ContactName = txtContactPerson.Text
        licenseReq.PhoneNumber = txtContactPhone.Text
        licenseReq.ComputerId = lblComputerId.Text
        licenseReq.ComputerName = lblComputerName.Text

        Dim reqProdClient As New LicenseProduct()
        reqProdClient.ProductName = LicenseProducts.CSIFLEXClient_Server
        reqProdClient.NumberLicenses = 1
        If Not IsNothing(licenseClient) Then
            reqProdClient.ExpiryDate = licenseClient.ExpiryDate
        End If

        licenseReq.Products.Add(reqProdClient)

        Dim json = JsonConvert.SerializeObject(licenseReq)

        Dim saveFile As New SaveFileDialog()
        saveFile.FileName = requestFile
        saveFile.Filter = "BIN Files (*.bin*) | *.bin"
        saveFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments

        If saveFile.ShowDialog = DialogResult.OK Then

            'Dim file = My.Computer.FileSystem.GetFileInfo(saveFile.FileName())
            'My.Computer.FileSystem.CombinePath(file.DirectoryName, requestFile)

            Try
                Using tw As TextWriter = New StreamWriter(saveFile.FileName(), False)
                    tw.Write(json)
                End Using

            Catch ex As Exception

                MessageBox.Show(ex.Message)

            End Try
        End If

    End Sub


    Private Sub btnImportLicense_Click(sender As Object, e As EventArgs) Handles btnImportLicense.Click

        Dim openFile As New OpenFileDialog()

        openFile.Filter = "LIC Files (*.lic*) | *.lic"
        openFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments

        If openFile.ShowDialog() = DialogResult.OK Then

            Dim path = openFile.FileName()

            Try
                Dim content = File.ReadAllText(path)

                Dim activation As CSIFLEX.License.Data.LicenseRequest = JsonConvert.DeserializeObject(Of CSIFLEX.License.Data.LicenseRequest)(content)

                ValidateProduct(activation, LicenseProducts.CSIFLEXClient_Server)

            Catch ex As Exception
                Log.Error(ex)
            End Try

        End If

    End Sub


    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        If isLicenseValid Then
            DialogResult = DialogResult.OK
        Else
            DialogResult = DialogResult.Cancel
        End If

    End Sub


    Private Sub SaveLicense(license As LicenseBase, isNew As Boolean)

        Dim sqlCmd As New Text.StringBuilder()

        sqlCmd.Append($"UPDATE CSI_auth.tbl_license                   ")
        sqlCmd.Append($"   SET                                        ")
        sqlCmd.Append($"     LicenseStatus = 'Closed'                 ")
        sqlCmd.Append($"   WHERE                                      ")
        sqlCmd.Append($"     ProductName   = '{ license.ProductName }'")

        MySqlAccess.ExecuteNonQuery(sqlCmd.ToString(), strConnection)

        sqlCmd.Clear()
        sqlCmd.Append("INSERT INTO CSI_auth.tbl_license ")
        sqlCmd.Append("(                                ")
        sqlCmd.Append("    `UniqueId`         ,         ")
        sqlCmd.Append("    `CompanyId`        ,         ")
        sqlCmd.Append("    `ProductName`      ,         ")
        sqlCmd.Append("    `CompanyName`      ,         ")
        sqlCmd.Append("    `ComputerName`     ,         ")
        sqlCmd.Append("    `ComputerId`       ,         ")
        sqlCmd.Append("    `ContactName`      ,         ")
        sqlCmd.Append("    `ContactEmail`     ,         ")
        sqlCmd.Append("    `ContactPhone`     ,         ")
        sqlCmd.Append("    `LicenseType`      ,         ")
        sqlCmd.Append("    `LicensesQuantity` ,         ")
        sqlCmd.Append("    `LicenseDate`      ,         ")
        sqlCmd.Append("    `ExpiryDate`       ,         ")
        sqlCmd.Append("    `LicenseStatus`    ,         ")
        sqlCmd.Append("    `HashCode`                   ")
        sqlCmd.Append(")                                ")
        sqlCmd.Append("VALUES                           ")
        sqlCmd.Append("(                                ")
        sqlCmd.Append("    @UniqueId          ,         ")
        sqlCmd.Append("    @CompanyId         ,         ")
        sqlCmd.Append("    @ProductName       ,         ")
        sqlCmd.Append("    @CompanyName       ,         ")
        sqlCmd.Append("    @ComputerName      ,         ")
        sqlCmd.Append("    @ComputerId        ,         ")
        sqlCmd.Append("    @ContactName       ,         ")
        sqlCmd.Append("    @ContactEmail      ,         ")
        sqlCmd.Append("    @ContactPhone      ,         ")
        sqlCmd.Append("    @LicenseType       ,         ")
        sqlCmd.Append("    @LicensesQuantity  ,         ")
        sqlCmd.Append("    @LicenseDate       ,         ")
        sqlCmd.Append("    @ExpiryDate        ,         ")
        sqlCmd.Append("    'Active'           ,         ")
        sqlCmd.Append("    @HashCode                    ")
        sqlCmd.Append(")                                ")
        'sqlCmd.Append("ON DUPLICATE KEY UPDATE          ")
        'sqlCmd.Append("    `CompanyId`        = @CompanyId       , ")
        'sqlCmd.Append("    `ComputerId`       = @ComputerId      , ")
        'sqlCmd.Append("    `LicenseType`      = @LicenseType     , ")
        'sqlCmd.Append("    `LicensesQuantity` = @LicensesQuantity, ")
        'sqlCmd.Append("    `LicenseDate`      = @LicenseDate     , ")
        'sqlCmd.Append("    `ExpiryDate`       = @ExpiryDate      , ")
        'sqlCmd.Append("    `HashCode`         = @HashCode          ")

        Dim cmmd As New MySqlCommand(sqlCmd.ToString())
        cmmd.Parameters.AddWithValue("@UniqueId", license.UniqueId.ToString())
        cmmd.Parameters.AddWithValue("@CompanyId", license.CompanyId)
        cmmd.Parameters.AddWithValue("@ProductName", license.ProductName)
        cmmd.Parameters.AddWithValue("@CompanyName", license.CompanyName)
        cmmd.Parameters.AddWithValue("@ComputerName", lblComputerName.Text)
        cmmd.Parameters.AddWithValue("@ComputerId", license.ComputerId)
        cmmd.Parameters.AddWithValue("@ContactName", txtContactPerson.Text)
        cmmd.Parameters.AddWithValue("@ContactEmail", txtContactEmail.Text)
        cmmd.Parameters.AddWithValue("@ContactPhone", txtContactPhone.Text)
        cmmd.Parameters.AddWithValue("@LicenseType", license.LicenseType)
        cmmd.Parameters.AddWithValue("@LicensesQuantity", license.LicensesQuantity)
        cmmd.Parameters.AddWithValue("@LicenseDate", license.LicenseDate)
        cmmd.Parameters.AddWithValue("@ExpiryDate", license.ExpiryDate)
        cmmd.Parameters.AddWithValue("@HashCode", license.HashCode)

        Try
            MySqlAccess.ExecuteNonQuery(cmmd, strConnection)
        Catch ex As Exception
            MessageBox.Show($"Error trying to generate the temporary license. \n{ex.Message}")
        End Try

    End Sub


    Private Sub ValidateProduct(activation As CSIFLEX.License.Data.LicenseRequest, product As String)

        Dim license As LicenseBase = New LicenseBase()

        activation.CopyPropertiesTo(Of LicenseBase)(license)

        Dim prodActivation = activation.Products.FirstOrDefault(Function(p) p.ProductName = product)

        If Not IsNothing(prodActivation) Then

            license.UniqueId = Guid.NewGuid
            license.ProductName = product
            license.LicenseDate = DateTime.Today
            license.LicensesQuantity = prodActivation.NumberLicenses
            license.ExpiryDate = prodActivation.ExpiryDate
            license.ComputerId = LocalMachineInformation.ComputerId
            license.HashCode = prodActivation.HashCode

            If licenseLib.IsActivationValid(license) Then

                SaveLicense(license, True)

                licenseLib.SaveLocalLicense(license)

                licenseClient = Nothing
                LoadClient()
            Else
                lblServerStatus.Text = "Error trying to load your new CSIFLEX license. The license file is invalid or currupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
            End If
        End If

    End Sub


    Private Sub EditMode(editMode As Boolean)

        txtCompanyName.Visible = editMode
        txtContactPerson.Visible = editMode
        txtContactEmail.Visible = editMode
        txtContactPhone.Visible = editMode

        lblCompanyName.Visible = Not editMode
        lblContactName.Visible = Not editMode
        lblContactEmail.Visible = Not editMode
        lblContactPhone.Visible = Not editMode

        chkServer.Visible = editMode

    End Sub

End Class



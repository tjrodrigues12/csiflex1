Imports System.IO
Imports CSI_Library
Imports CSIFLEX.Database.Access
Imports CSIFLEX.License.Data
Imports CSIFLEX.Utilities
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports CSIFLEX.eNET.Library
Imports System.Windows

Public Class LicenseManagement

    Dim licenseServer As license
    Dim licenseMobile As license
    Dim licenseFocas As license

    Dim licenseLib As CSILicenseLibrary

    Dim companyId As Guid

    Dim expiryDays As Integer = 30

    Dim requestFile As String = "CSIFLEXLicenseRequest.bin"

    Dim isLicenseValid As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        licenseLib = New CSILicenseLibrary()

        licenseLib.LoadLicense(LicenseProducts.CSIFLEXServer, licenseServer)

        licenseLib.LoadLicense(LicenseProducts.CSIFLEXWebApp, licenseMobile)

        licenseLib.LoadLicense(LicenseProducts.CSIFLEXFocasMtc, licenseFocas)

    End Sub


    Private Sub LicenseManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim screenHeight = SystemParameters.VirtualScreenHeight

        If Me.Height > screenHeight Then
            Me.Height = screenHeight * 0.95
        End If

        lblComputerName.Text = LocalMachineInformation.MyComputerName()
        lblComputerId.Text = LocalMachineInformation.ComputerId()

        Dim license As DataTable = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_license ORDER BY LicenseStatus, LicenseDate DESC")

        If license.Rows.Count > 0 Then
            companyId = New Guid(license.Rows(0)("CompanyId").ToString())
            txtCompanyName.Text = license.Rows(0)("CompanyName").ToString()
            lblCompanyName.Text = license.Rows(0)("CompanyName").ToString()

            lblContactEmail.Text = license.Rows(0)("ContactEmail").ToString()
            txtContactEmail.Text = license.Rows(0)("ContactEmail").ToString()

            lblContactName.Text = license.Rows(0)("ContactName").ToString()
            txtContactPerson.Text = license.Rows(0)("ContactName").ToString()

            lblContactPhone.Text = license.Rows(0)("ContactPhone").ToString()
            txtContactPhone.Text = license.Rows(0)("ContactPhone").ToString()

        Else
            companyId = New Guid()
        End If
        lblCompanyId.Text = companyId.ToString()

        LoadServer()

        LoadFocas()

        LoadMobile()
        'If licenseLib.IsMobileAvailable() Then
        '    LoadMobile()
        'Else
        '    lblMobileStatus.Text = "Product is not available."
        '    grpMobile.Enabled = False
        'End If

        btnTempLicense.Enabled = licenseServer.Status = "NotFound" Or licenseFocas.Status = "NotFound" Or licenseMobile.Status = "NotFound" '(licenseLib.IsMobileAvailable() And licenseMobile.Status = "NotFound")

        EditMode(True)

    End Sub


    Private Sub LoadServer()

        If IsNothing(licenseServer) Then
            licenseLib.LoadLicense(LicenseProducts.CSIFLEXServer, licenseServer)
        End If

        If Not licenseServer.Status = "NotFound" Then

            lblServerLicType.ForeColor = Color.Blue
            If licenseServer.LicenseType = "Temporary" Then
                lblServerLicType.ForeColor = Color.Red
            End If
            lblServerLicType.Text = licenseServer.LicenseType

            lblServerExpiry.ForeColor = Color.Blue
            If licenseServer.ExpiryDate.ToString("yyyy-MM-dd") = "2030-01-01" Then
                lblServerExpiry.Text = "Permanent"
            Else
                lblServerExpiry.Text = licenseServer.ExpiryDate.ToString("MMM-dd-yyyy")
            End If

            If licenseServer.Status.ToUpper() = "VALID" Then

                isLicenseValid = True

                Dim diff = DateDiff(DateInterval.Day, Today, licenseServer.ExpiryDate)

                If diff <= 10 Then
                    lblServerExpiry.ForeColor = Color.Red
                    lblServerStatus.ForeColor = Color.Red
                    lblServerStatus.Text = $"Your CSIFLEX license will expiry in {diff} days. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                Else
                    lblServerStatus.Text = "Active"
                End If
            Else

                lblServerStatus.ForeColor = Color.Red

                If licenseServer.Status.StartsWith("ERR02") Or licenseServer.Status.StartsWith("ERR03") Then
                    lblServerStatus.Text = $"{licenseServer.Status.Substring(0, 5)}. Your CSIFLEX license is invalid or corrupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                ElseIf licenseServer.Status.StartsWith("EXP01") Then
                    lblServerStatus.Text = licenseServer.Status.Split(";")(1)
                ElseIf licenseServer.Status.StartsWith("EXP02") Then
                    lblServerStatus.Text = licenseServer.Status.Split(";")(1)
                    isLicenseValid = True
                Else
                    lblServerStatus.Text = $"{licenseServer.Status.Substring(0, 5)}. Error trying to validate your CSIFLEX. Close the application and try again. If the continues please, contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                End If
            End If

        ElseIf licenseLib.HasPreviousLicense(LicenseProducts.CSIFLEXServer) Then

            lblServerStatus.Text = "A previous installation of CSIFLEX Server has been detected. Your license file is invalid or currupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
            lblServerStatus.ForeColor = Color.Red
        Else

            companyId = Guid.NewGuid()
            lblServerLicType.Text = "No license"
            lblServerExpiry.Text = ""
            lblServerStatus.Text = "Please, fill the form to request a license."
            lblServerStatus.ForeColor = Color.Red
        End If

    End Sub


    Private Sub LoadMobile()

        If IsNothing(licenseMobile) Then
            licenseLib.LoadLicense(LicenseProducts.CSIFLEXWebApp, licenseMobile)
        End If

        If Not licenseMobile.Status = "NotFound" Then

            lblMobileLicType.ForeColor = Color.Blue
            If licenseMobile.LicenseType = "Temporary" Then
                lblMobileLicType.ForeColor = Color.Red
            End If
            lblMobileLicType.Text = licenseMobile.LicenseType

            lblMobileExpiry.ForeColor = Color.Blue
            If licenseMobile.ExpiryDate.ToString("yyyy-MM-dd") = "2030-01-01" Then
                lblMobileExpiry.Text = "Permanent"
            Else
                lblMobileExpiry.Text = licenseMobile.ExpiryDate.ToString("MMM-dd-yyyy")
            End If

            txtNumberMobile.Text = licenseMobile.LicensesQuantity.ToString()

            If licenseMobile.Status.ToUpper() = "VALID" Then
                Dim diff = DateDiff(DateInterval.Day, Today, licenseMobile.ExpiryDate)
                If diff <= 10 Then
                    lblMobileExpiry.ForeColor = Color.Red
                    lblMobileStatus.ForeColor = Color.Red
                    lblMobileStatus.Text = $"Your CSIFLEX license will expiry in {diff} days. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                Else
                    lblMobileStatus.Text = "Active"
                End If
            Else

                lblMobileStatus.ForeColor = Color.Red

                If licenseMobile.Status.StartsWith("ERR02") Or licenseMobile.Status.StartsWith("ERR03") Then
                    lblMobileStatus.Text = $"{licenseMobile.Status.Substring(0, 5)}. Your CSIFLEX license is invalid or corrupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                ElseIf licenseMobile.Status.StartsWith("EXP01") Then
                    lblMobileStatus.Text = licenseMobile.Status.Split(";")(1)
                ElseIf licenseMobile.Status.StartsWith("EXP02") Then
                    lblMobileStatus.Text = licenseMobile.Status.Split(";")(1)
                    isLicenseValid = True
                Else
                    lblMobileStatus.Text = $"{licenseMobile.Status.Substring(0, 5)}. Error trying to validate your CSIFLEX. Close the application and try again. If the continues please, contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                End If
            End If

        ElseIf licenseLib.HasPreviousLicense(LicenseProducts.CSIFLEXWebApp) Then

            lblMobileStatus.Text = "A previous installation of CSIFLEX has been detected. Your license file is invalid or currupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
            lblMobileStatus.ForeColor = Color.Red
        Else

            lblMobileLicType.Text = "No license"
            lblMobileExpiry.Text = ""
            lblMobileStatus.Text = "Please, select this product and fill the form to request a license."
            lblMobileStatus.ForeColor = Color.Red
        End If

    End Sub


    Private Sub LoadFocas()

        If IsNothing(licenseFocas) Then
            licenseLib.LoadLicense(LicenseProducts.CSIFLEXFocasMtc, licenseFocas)
        End If

        If Not licenseFocas.Status = "NotFound" Then

            lblFocasLicType.ForeColor = Color.Blue
            If licenseFocas.LicenseType = "Temporary" Then
                lblFocasLicType.ForeColor = Color.Red
            End If
            lblFocasLicType.Text = licenseFocas.LicenseType

            lblFocasExpiry.ForeColor = Color.Blue
            If licenseFocas.ExpiryDate.ToString("yyyy-MM-dd") = "2030-01-01" Then
                lblFocasExpiry.Text = "Permanent"
            Else
                lblFocasExpiry.Text = licenseFocas.ExpiryDate.ToString("MMM-dd-yyyy")
            End If

            txtNumberFocas.Text = licenseFocas.LicensesQuantity.ToString()

            If licenseFocas.Status.ToUpper() = "VALID" Then
                Dim diff = DateDiff(DateInterval.Day, Today, licenseFocas.ExpiryDate)
                If diff <= 10 Then
                    lblFocasExpiry.ForeColor = Color.Red
                    lblFocasStatus.ForeColor = Color.Red
                    lblFocasStatus.Text = $"Your CSIFLEX license will expiry in {diff} days. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                Else
                    lblFocasStatus.Text = "Active"
                End If
            Else

                lblFocasStatus.ForeColor = Color.Red

                If licenseFocas.Status.StartsWith("ERR02") Or licenseFocas.Status.StartsWith("ERR03") Then
                    lblFocasStatus.Text = $"{licenseFocas.Status.Substring(0, 5)}. Your CSIFLEX license is invalid or corrupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                ElseIf licenseFocas.Status.StartsWith("EXP01") Then
                    lblFocasStatus.Text = licenseFocas.Status.Split(";")(1)
                ElseIf licenseFocas.Status.StartsWith("EXP02") Then
                    lblFocasStatus.Text = licenseFocas.Status.Split(";")(1)
                    isLicenseValid = True
                Else
                    lblFocasStatus.Text = $"{licenseFocas.Status.Substring(0, 5)}. Error trying to validate your CSIFLEX. Close the application and try again. If the continues please, contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                End If
            End If

        ElseIf licenseLib.HasPreviousLicense(LicenseProducts.CSIFLEXFocasMtc) Then

            lblFocasStatus.Text = "A previous installation of CSIFLEX has been detected. Your license file is invalid or currupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
            lblFocasStatus.ForeColor = Color.Red
        Else

            lblFocasLicType.Text = "No license"
            lblFocasExpiry.Text = ""
            lblFocasStatus.Text = "Please, select this product and fill the form to request a license."
            lblFocasStatus.ForeColor = Color.Red
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

        'Generate the CSIFLEX Server license if it doesn't exist
        If licenseServer.Status = "NotFound" Then
            tempLicense.UniqueId = Guid.NewGuid()

            tempLicense.LicensesQuantity = 1
            tempLicense.ProductName = LicenseProducts.CSIFLEXServer
            tempLicense.ExpiryDate = expiryDate

            Try
                tempLicense = licenseLib.GenerateLicense(tempLicense)
                SaveLicense(tempLicense, True)
                tempLicense.CopyPropertiesTo(Of LicenseBase)(licenseServer)
                licenseServer.contactname = txtContactPerson.Text
                licenseServer.contactemail = txtContactEmail.Text
                licenseServer.contactphone = txtContactPhone.Text

                'LoadServer()
            Catch ex As Exception
                hadSuccess = False
            End Try
        Else
            expiryDate = licenseServer.ExpiryDate
        End If

        'Generate the CSIFLEX Focas/MTC license if it doesn't exist and it is checked
        If licenseFocas.Status = "NotFound" And chkFocas.Checked Then
            tempLicense.UniqueId = Guid.NewGuid()

            tempLicense.LicensesQuantity = 5
            tempLicense.ProductName = LicenseProducts.CSIFLEXFocasMtc
            tempLicense.ExpiryDate = expiryDate

            Try
                tempLicense = licenseLib.GenerateLicense(tempLicense)
                SaveLicense(tempLicense, True)
                tempLicense.CopyPropertiesTo(Of LicenseBase)(licenseFocas)

                'LoadFocas()
            Catch ex As Exception
                hadSuccess = False
            End Try
        End If

        'Generate the CSIFLEX Mobile license if it doesn't exist and it is checked
        If licenseMobile.Status = "NotFound" And chkMobile.Checked Then
            tempLicense.UniqueId = Guid.NewGuid()

            tempLicense.LicensesQuantity = 1
            tempLicense.ProductName = LicenseProducts.CSIFLEXWebApp
            tempLicense.ExpiryDate = expiryDate

            Try
                tempLicense = licenseLib.GenerateLicense(tempLicense)
                SaveLicense(tempLicense, True)
                tempLicense.CopyPropertiesTo(Of LicenseBase)(licenseMobile)

            Catch ex As Exception
                hadSuccess = False
            End Try
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
        msg.Append(vbNewLine & vbNewLine & "- CSIFLEX Server")

        If chkFocas.Checked Then
            msg.Append(vbNewLine & vbNewLine & "- CSIFLEX FOCAS/MTC Connections")
        End If
        If chkMobile.Checked Then
            msg.Append(vbNewLine & vbNewLine & "- CSIFLEX WebApp")
        End If

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

        Dim reqProdServer As New LicenseProduct()
        reqProdServer.ProductName = LicenseProducts.CSIFLEXServer
        reqProdServer.NumberLicenses = 1
        If Not IsNothing(licenseServer) Then
            reqProdServer.ExpiryDate = licenseServer.ExpiryDate
        End If

        licenseReq.Products.Add(reqProdServer)

        If chkFocas.Checked Then
            Dim reqProdFocas As New LicenseProduct()
            reqProdFocas.ProductName = LicenseProducts.CSIFLEXFocasMtc
            reqProdFocas.NumberLicenses = txtNumberFocas.Text
            If Not IsNothing(licenseFocas) Then
                reqProdFocas.ExpiryDate = licenseFocas.ExpiryDate
            End If

            licenseReq.Products.Add(reqProdFocas)
        End If

        If chkMobile.Checked Then
            Dim reqProdMobile As New LicenseProduct()
            reqProdMobile.ProductName = LicenseProducts.CSIFLEXWebApp
            reqProdMobile.NumberLicenses = txtNumberMobile.Text
            If Not IsNothing(licenseMobile) Then
                reqProdMobile.ExpiryDate = licenseMobile.ExpiryDate
            End If

            licenseReq.Products.Add(reqProdMobile)
        End If

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

                companyId = activation.CompanyId
                txtCompanyName.Text = activation.CompanyName
                lblCompanyName.Text = activation.CompanyName

                lblContactEmail.Text = activation.ContactEmail
                txtContactEmail.Text = activation.ContactEmail

                lblContactName.Text = activation.ContactName
                txtContactPerson.Text = activation.ContactName

                lblContactPhone.Text = activation.PhoneNumber
                txtContactPhone.Text = activation.PhoneNumber

                ValidateProduct(activation, LicenseProducts.CSIFLEXServer)

                ValidateProduct(activation, LicenseProducts.CSIFLEXWebApp)

                ValidateProduct(activation, LicenseProducts.CSIFLEXFocasMtc)

            Catch ex As Exception

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

        MySqlAccess.ExecuteNonQuery(sqlCmd.ToString())

        sqlCmd.Clear()
        sqlCmd.Append("INSERT INTO CSI_auth.tbl_license ")
        sqlCmd.Append("(                                ")
        sqlCmd.Append("    `UniqueId`         ,         ")
        sqlCmd.Append("    `CompanyId`        ,         ")
        sqlCmd.Append("    `ProductId`        ,         ")
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
        sqlCmd.Append("    @ProductId         ,         ")
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
        cmmd.Parameters.AddWithValue("@ProductId", license.ProductId)
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
            MySqlAccess.ExecuteNonQuery(cmmd)
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
            license.ProductId = LicenseProducts.CSIFLEXProducts.FirstOrDefault(Function(v) v.Value = product).Key
            license.LicenseDate = DateTime.Today
            license.LicensesQuantity = prodActivation.NumberLicenses
            license.ExpiryDate = prodActivation.ExpiryDate
            license.ComputerId = LocalMachineInformation.ComputerId
            license.HashCode = prodActivation.HashCode

            If licenseLib.IsActivationValid(license) Then

                SaveLicense(license, True)

                licenseLib.SaveLocalLicense(license)

                Select Case product
                    Case LicenseProducts.CSIFLEXServer
                        licenseServer = Nothing
                        LoadServer()
                    Case LicenseProducts.CSIFLEXWebApp
                        licenseMobile = Nothing
                        LoadMobile()
                    Case LicenseProducts.CSIFLEXFocasMtc
                        licenseFocas = Nothing
                        LoadFocas()
                End Select
            Else
                Select Case product
                    Case LicenseProducts.CSIFLEXServer
                        lblServerStatus.Text = "Error trying to load your new CSIFLEX license. The license file is invalid or currupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                    Case LicenseProducts.CSIFLEXWebApp
                        lblMobileStatus.Text = "Error trying to load your new CSIFLEX license. The license file is invalid or currupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                    Case LicenseProducts.CSIFLEXFocasMtc
                        lblFocasStatus.Text = "Error trying to load your new CSIFLEX license. The license file is invalid or currupted. Please contact your CSIFLEX reseller or email CSIFLEX support team at support@csiflex.com"
                End Select
            End If
        End If


    End Sub


    Private Sub EditMode(editMode As Boolean)

        txtCompanyName.Visible = editMode
        txtContactPerson.Visible = editMode
        txtContactEmail.Visible = editMode
        txtContactPhone.Visible = editMode

        txtNumberFocas.Enabled = editMode
        txtNumberMobile.Enabled = editMode

        lblCompanyName.Visible = Not editMode
        lblContactName.Visible = Not editMode
        lblContactEmail.Visible = Not editMode
        lblContactPhone.Visible = Not editMode

        chkServer.Visible = editMode
        chkFocas.Visible = editMode
        chkMobile.Visible = editMode

    End Sub

End Class



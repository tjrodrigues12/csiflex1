Imports CSIFLEX.Database.Access
Imports CSIFLEX.License.Data
Imports CSIFLEX.License.Library
Imports CSIFLEX.Utilities

Public Class CSILicenseLibrary

    Dim licenseLib As LicenseLibrary

    Private Product_ As String

    Private strConnection As String = Nothing

    Private license As License

    Public ReadOnly Property Product() As String
        Get
            Return Product_
        End Get
    End Property


    Sub New()

        licenseLib = New LicenseLibrary()

    End Sub

    Sub New(strConnection_ As String)

        strConnection = strConnection_

        licenseLib = New LicenseLibrary()

    End Sub

    Public Function CurrentLicense(product As String) As License

        LoadLicense(product, license)

        Return license

    End Function


    Public Function IsLicenseValid(product As String) As Boolean

        'Dim license As New License()

        LoadLicense(product, license)

        Return license.Status.ToUpper() = "VALID"

    End Function


    Public Function DaysToExpiry(product As String) As Integer

        'Dim license As New License()

        LoadLicense(product, license)

        Return DateDiff(DateInterval.Day, Today, license.ExpiryDate)

    End Function


    Public Function IsLicenseValid(product As String, licenseQuantity As Integer) As Boolean

        'Dim license As New License()

        LoadLicense(product, license)

        Return license.Status.ToUpper() = "VALID" And license.LicensesQuantity >= licenseQuantity

    End Function


    Public Function IsLicenseValid(license As LicenseBase) As Boolean
        Return licenseLib.IsLicenseValid(license).ToUpper() = "VALID"
    End Function


    Public Function IsActivationValid(license As LicenseBase) As Boolean
        Return licenseLib.IsActivationValid(license).ToUpper() = "VALID"
    End Function


    Public Function IsMobileAvailable() As Boolean

        Return ServiceTools.ServiceInstaller.ServiceIsInstalled("CSIFlex Mobile Server")

    End Function


    Public Function HasPreviousLicense(product As String) As Boolean

        Return licenseLib.hasPreviousLicense(product)

    End Function


    Public Sub LoadCsiflexClientLicense(ByRef license As License)

        Dim licenseBase = licenseLib.GetLocalLicenses().FirstOrDefault(Function(lic) lic.ProductName.StartsWith("CSIFLEX Client"))
        license = New License()

        If licenseBase IsNot Nothing Then
            licenseBase.CopyPropertiesTo(Of License)(license)
            license.Status = licenseLib.IsLicenseValid(license)
        Else
            license.Status = "NotFound"
        End If

    End Sub


    Public Sub LoadLicense(product As String, ByRef license As License)


        Dim licenseBase = licenseLib.GetLocalLicense(product)

        If licenseBase Is Nothing Then

            '== Drausio
            '== This validation was made because an error in the license system that was not save right the license in the license file, only in the database
            '======================================================================================================================================================
            Dim hasValidLicenseInDatabase As Boolean = False

            Dim dbLicenses As DataTable

            If IsNothing(strConnection) Then
                dbLicenses = MySqlAccess.GetDataTable($"SELECT * FROM CSI_auth.tbl_license WHERE ProductName = '{product}' AND LicenseStatus = 'Active'")
            Else
                dbLicenses = MySqlAccess.GetDataTable($"SELECT * FROM CSI_auth.tbl_license WHERE ProductName = '{product}' AND LicenseStatus = 'Active'", strConnection)
            End If


            If dbLicenses.Rows.Count > 0 Then
                Dim dbLicense As LicenseBase = New LicenseBase()
                dbLicense.CompanyId = Guid.Parse(dbLicenses.Rows(0)("CompanyId").ToString())
                dbLicense.CompanyName = dbLicenses.Rows(0)("CompanyName").ToString()
                dbLicense.ComputerId = dbLicenses.Rows(0)("ComputerId").ToString()
                dbLicense.ExpiryDate = dbLicenses.Rows(0)("ExpiryDate").ToString()
                dbLicense.LicensesQuantity = dbLicenses.Rows(0)("LicensesQuantity").ToString()
                dbLicense.LicenseType = dbLicenses.Rows(0)("LicenseType").ToString()
                dbLicense.ProductName = dbLicenses.Rows(0)("ProductName").ToString()
                dbLicense.HashCode = dbLicenses.Rows(0)("HashCode").ToString()

                If licenseLib.IsActivationValid(dbLicense) = "Valid" Then
                    licenseLib.SaveLocalLicense(dbLicense)
                    licenseBase = dbLicense
                    hasValidLicenseInDatabase = True
                End If
            End If
            '======================================================================================================================================================

            If Not hasValidLicenseInDatabase Then
                license = New License()
                license.ExpiryDate = Today
                license.Status = "NotFound"
                Return
            End If
        End If

        If license Is Nothing Then license = New License()

        licenseBase.CopyPropertiesTo(Of License)(license)

        license.Status = licenseLib.IsLicenseValid(licenseBase)

        Dim dtLicense As DataTable

        If IsNothing(strConnection) Then
            dtLicense = MySqlAccess.GetDataTable($"SELECT * FROM CSI_auth.tbl_license WHERE ProductName = '{product}' AND LicenseStatus <> 'Closed' ORDER BY LicenseDate DESC LIMIT 1")
        Else
            dtLicense = MySqlAccess.GetDataTable($"SELECT * FROM CSI_auth.tbl_license WHERE ProductName = '{product}' AND LicenseStatus <> 'Closed' ORDER BY LicenseDate DESC LIMIT 1", strConnection)
        End If

        If dtLicense.Rows.Count > 0 Then

            Dim licenseRow As DataRow = dtLicense.Rows(0)

            license.UniqueId = Guid.Parse(licenseRow("UniqueId").ToString())
            license.LicenseDate = licenseRow("LicenseDate")
            license.ProductName = licenseRow("ProductName").ToString()
            license.CompanyName = licenseRow("CompanyName").ToString()
            license.ComputerName = licenseRow("ComputerName").ToString()
            license.ContactName = licenseRow("ContactName").ToString()
            license.ContactEmail = licenseRow("ContactEmail").ToString()
            license.ContactPhone = licenseRow("ContactPhone").ToString()
        End If

    End Sub


    Public Function GenerateLicense(license As LicenseBase) As LicenseBase
        Return licenseLib.GenerateLicense(license)
    End Function


    Public Sub SaveLocalLicense(license As LicenseBase)

        licenseLib.SaveLocalLicense(license)
    End Sub

End Class


Public Class license
    Inherits LicenseBase

    Public Property computername As String

    Public Property contactname As String

    Public Property contactemail As String

    Public Property contactphone As String

End Class

'Public Class _LicenseRequest

'    Public Property UniqueId As Guid

'    Public Property Product As String

'    Public Property CompanyName As String

'    Public Property ContactName As String

'    Public Property ContactEmail As String

'    Public Property PhoneNumber As String

'    Public Property ComputerId As String

'    Public Property NumberMachines As Integer

'End Class
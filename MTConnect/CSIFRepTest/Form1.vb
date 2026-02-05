Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim reportViewer1 As ReportViewer = New ReportViewer()

        'Set the processing mode for the ReportViewer to Local
        reportViewer1.ProcessingMode = ProcessingMode.Local

        Dim localReport As LocalReport
        localReport = reportViewer1.LocalReport

        localReport.DataSources.Clear()

        localReport.ReportEmbeddedResource = "CSIFRepTest.mainReport.rdlc"

        'Dim dataset As New DataSet("MachineData")
        Dim machinelist As New List(Of String)({"MC-21", "MC-25", "MC-26"})

        'Dim machinename As String = "MC-26"

        'Get the sales order data
        'GetMachineData(machinename, dataset)
        'machinelist = GetMachineList()

        'Create a report data source for the sales order data
        Dim dsMachineList As New ReportDataSource()
        dsMachineList.Name = "MachineList"
        dsMachineList.Value = machinelist 'dataset.Tables("MachineList")

        localReport.DataSources.Add(dsMachineList)

        AddHandler localReport.SubreportProcessing, AddressOf localReport_SubreportProcessing

        'Get the sales order detail data
        Dim machinename As String = "MC-1"
        Dim tempds As New DataSet
        GetShiftData(machinename, tempds)

        'Create a report data source for the sales 
        'order detail data
        Dim dsShiftData As New ReportDataSource()
        dsShiftData.Name = "ShiftData"
        dsShiftData.Value = tempds.Tables("ShiftData")

        localReport.DataSources.Add(dsShiftData)

        'Create a report parameter for the sales order number 
        Dim rpMachineName As New ReportParameter()
        rpMachineName.Name = "MachineName"
        rpMachineName.Values.Add("MC-26")

        'Set the report parameters for the report
        Dim parameters() As ReportParameter = {rpMachineName}
        localReport.SetParameters(parameters)

        'Refresh the report
        reportViewer1.RefreshReport()

    End Sub

    Private Sub localReport_SubreportProcessing()

    End Sub

    Private Sub GetMachineData(ByVal machinename As String, ByRef dsMachineData As DataSet)

        Dim sqlMachineData As String = _
            "SELECT SOH.SalesOrderNumber, S.Name AS Store, " & _
            "       SOH.OrderDate, C.FirstName AS SalesFirstName, " & _
            "       C.LastName AS SalesLastName, E.Title AS " & _
            "       SalesTitle, SOH.PurchaseOrderNumber, " & _
            "       SM.Name AS ShipMethod, BA.AddressLine1 " & _
            "       AS BillAddress1, BA.AddressLine2 AS " & _
            "       BillAddress2, BA.City AS BillCity, " & _
            "       BA.PostalCode AS BillPostalCode, BSP.Name " & _
            "       AS BillStateProvince, BCR.Name AS " & _
            "       BillCountryRegion, SA.AddressLine1 AS " & _
            "       ShipAddress1, SA.AddressLine2 AS " & _
            "       ShipAddress2, SA.City AS ShipCity, " & _
            "       SA.PostalCode AS ShipPostalCode, SSP.Name " & _
            "       AS ShipStateProvince, SCR.Name AS " & _
            "       ShipCountryRegion, CC.Phone AS CustPhone, " & _
            "       CC.FirstName AS CustFirstName, CC.LastName " & _
            "       AS CustLastName " & _
            "FROM   Person.Address SA INNER JOIN " & _
            "       Person.StateProvince SSP ON " & _
            "       SA.StateProvinceID = SSP.StateProvinceID " & _
            "       INNER JOIN Person.CountryRegion SCR ON " & _
            "       SSP.CountryRegionCode = SCR.CountryRegionCode " & _
            "       RIGHT OUTER JOIN Sales.SalesOrderHeader SOH " & _
            "       LEFT OUTER JOIN  Person.Contact CC ON " & _
            "       SOH.ContactID = CC.ContactID LEFT OUTER JOIN" & _
            "       Person.Address BA INNER JOIN " & _
            "       Person.StateProvince BSP ON " & _
            "       BA.StateProvinceID = BSP.StateProvinceID " & _
            "       INNER JOIN Person.CountryRegion BCR ON " & _
            "       BSP.CountryRegionCode = " & _
            "       BCR.CountryRegionCode ON SOH.BillToAddressID " & _
            "       = BA.AddressID ON  SA.AddressID = " & _
            "       SOH.ShipToAddressID LEFT OUTER JOIN " & _
            "       Person.Contact C RIGHT OUTER JOIN " & _
            "       HumanResources.Employee E ON C.ContactID = " & _
            "       E.ContactID ON SOH.SalesPersonID = " & _
            "       E.EmployeeID LEFT OUTER JOIN " & _
            "       Purchasing.ShipMethod SM ON SOH.ShipMethodID " & _
            "       = SM.ShipMethodID LEFT OUTER JOIN Sales.Store" & _
            "        S ON SOH.CustomerID = S.CustomerID " & _
            "WHERE  (SOH.SalesOrderNumber = @SalesOrderNumber)"

        Using connection As New MySqlConnection("Data Source=(local); " +
                                              "Initial Catalog=AdventureWorks; " +
                                              "Integrated Security=SSPI")

            Dim command As New MySqlCommand(sqlMachineData, connection)

            Dim parameter As New MySqlParameter("SalesOrderNumber", machinename)
            command.Parameters.Add(parameter)

            Dim salesOrderAdapter As New MySqlDataAdapter(command)

            salesOrderAdapter.Fill(dsMachineData, "MachineData")

        End Using

    End Sub

    Private Sub GetShiftData(ByVal machinename As String, ByRef dsShiftData As DataSet)

        Dim sqlSalesOrderDetail As String = _
            "SELECT  SOD.SalesOrderDetailID, SOD.OrderQty, " & _
            "        SOD.UnitPrice, CASE WHEN " & _
            "        SOD.UnitPriceDiscount IS NULL THEN 0 " & _
            "        ELSE SOD.UnitPriceDiscount END AS " & _
            "        UnitPriceDiscount, SOD.LineTotal, " & _
            "        SOD.CarrierTrackingNumber, " & _
            "        SOD.SalesOrderID, P.Name, P.ProductNumber " & _
            "FROM    Sales.SalesOrderDetail SOD INNER JOIN " & _
            "        Production.Product P ON SOD.ProductID = " & _
            "        P.ProductID INNER JOIN " & _
            "        Sales.SalesOrderHeader SOH ON " & _
            "        SOD.SalesOrderID = SOH.SalesOrderID " & _
            "WHERE   (SOH.SalesOrderNumber = @SalesOrderNumber) " & _
            "ORDER BY SOD.SalesOrderDetailID"

        Using connection As New MySqlConnection("Data Source=(local); " +
                                                "Initial Catalog=AdventureWorks; " +
                                                "Integrated Security=SSPI")

            Dim command As New MySqlCommand(sqlSalesOrderDetail, connection)

            Dim parameter As New MySqlParameter("SalesOrderNumber", machinename)
            command.Parameters.Add(parameter)

            Dim salesOrderDetailAdapter As New MySqlDataAdapter(command)

            salesOrderDetailAdapter.Fill(dsShiftData, "ShiftData")

        End Using

    End Sub

End Class

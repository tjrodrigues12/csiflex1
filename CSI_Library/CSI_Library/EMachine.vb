Imports System.Globalization

Public Class EMachine

    Public MchId As Integer
    Public MchName As String
    Public EnetMchName As String
    Public Statut As String
    Public StatusDateTime As DateTime
    Public StatusDateSeconds As Long
    Public StatusDateString As String
    Public Shift As String
    Public PartNo As String
    Public Condition As String
    Public ServerName As String
    Public CycleCount As Integer
    Public LastCycle As String
    Public CurrentCycle As String
    Public ElapsedTime As String
    Public feedOverride As Double
    Public spindleOverride As Double
    Public OperatorRefId As String
    Public Comment As String

    Public Timeline As String

    Public Sub New()
        MchName = Nothing
        Statut = Nothing
        PartNo = Nothing
    End Sub

    Public Sub New(mchId As Integer, mchName As String, Statut As String, shift As String, PartNo As String, Condition As String, ServerName As String, CycleCount As Integer, lastcycle As String, currentcycle As String, elapsedtime As String, feedOver As String)

        Try
            Me.Timeline = ""

            Me.MchId = mchId
            Me.MchName = mchName 'dr.Item("machine")
            Me.Statut = Statut 'dr.Item("status")
            Me.Shift = shift 'dr.Item("shift")
            Me.PartNo = PartNo 'dr.Item("PartNumber")
            Me.Condition = Condition
            Me.ServerName = ServerName

            Me.CycleCount = CycleCount 'Integer.Parse(dr.Item("CycleCount"))

            Me.LastCycle = lastcycle 'dr.Item("LastCycle")

            Me.CurrentCycle = currentcycle ''dr.Item("CurrentCycle")

            Me.ElapsedTime = elapsedtime 'dr.Item("ElapsedTime")

            Dim feed As String = feedOver 'dr.Item("feedOverride")

            If (feed.Trim().Length > 0) Then
                Dim feedwithoutpercentage As String
                feedwithoutpercentage = feed.Trim.Replace("%", "")
                Dim tempdouble As Double = 0
                Double.TryParse(feedwithoutpercentage, tempdouble)
                Me.feedOverride = tempdouble
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub New(dr As DataRow, serverName As String)

        Try
            Me.Timeline = ""

            Try
                Me.MchId = dr.Item("MachineId")
            Catch ex As Exception

            End Try

            Try
                Me.MchName = dr.Item("Machine")
            Catch ex As Exception

            End Try

            Try
                Me.EnetMchName = dr.Item("EnetMachine")
            Catch ex As Exception

            End Try


            Try
                Me.Statut = dr.Item("Status")
            Catch ex As Exception

            End Try

            Try
                Me.StatusDateString = dr.Item("StatusDatetime")

                DateTime.TryParseExact(Me.StatusDateString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, Me.StatusDateTime)

                Me.StatusDateSeconds = CLng(Me.StatusDateTime.TimeOfDay.TotalSeconds)

            Catch ex As Exception

            End Try

            Try
                Me.Shift = dr.Item("Shift")
            Catch ex As Exception

            End Try


            Try
                Me.PartNo = dr.Item("PartNumber")
            Catch ex As Exception

            End Try


            Try
                If Not (serverName = "enet") Then Me.Condition = dr.Item("Condition")
            Catch ex As Exception

            End Try


            Try
                Me.CycleCount = Integer.Parse(dr.Item("CycleCount"))
            Catch ex As Exception

            End Try

            Me.ServerName = serverName

            'new data

            Try
                Me.OperatorRefId = dr.Item("OperatorRefId")
            Catch ex As Exception

            End Try

            Try
                Me.Comment = dr.Item("Comment")
            Catch ex As Exception

            End Try



            Try
                Dim tempInt As Integer
                Integer.TryParse(dr.Item("CycleCount"), tempInt)
                Me.CycleCount = tempInt
            Catch ex As Exception

            End Try

            Try
                Me.LastCycle = dr.Item("LastCycle")
            Catch ex As Exception

            End Try

            Try
                Me.CurrentCycle = dr.Item("CurrentCycle")
            Catch ex As Exception

            End Try

            Try
                Me.ElapsedTime = dr.Item("ElapsedTime")
            Catch ex As Exception

            End Try


            'Dim feed As String = dr.Item("FeedOverride")

            'If (feed.Trim().Length > 0) Then
            '    Dim feedwithoutpercentage As String
            '    feedwithoutpercentage = feed.Trim.Replace("%", "")
            '    Dim tempdouble As Double = 0
            '    Double.TryParse(feedwithoutpercentage, tempdouble)
            '    Me.feedOverride = tempdouble
            'End If

            Dim feed As Integer = -1
            Integer.TryParse(dr.Item("FeedOverride"), feed)
            feedOverride = feed

            Dim spindle As Integer = -1
            Integer.TryParse(dr.Item("SpindleOverride"), spindle)
            spindleOverride = spindle


        Catch ex As Exception

        End Try

    End Sub

End Class

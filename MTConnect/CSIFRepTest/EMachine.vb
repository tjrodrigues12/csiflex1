Public Class EMachine
    Public MchName As String
    Public Statut As String
    Public Shift As String
    'Public TimelineReset As Boolean
    'Public StatusChanged As Boolean
    Public PartNo As String
    Public Condition As String
    Public ServerName As String
    'new data
    Public CycleCount As Integer
    'Public LastCycle As Date
    'Public CurrentCycle As Date
    'Public ElapsedTime As Date
    Public LastCycle As String
    Public CurrentCycle As String
    Public ElapsedTime As String
    Public feedOverride As Double

    Public Timeline As String

    Public Sub New()
        MchName = Nothing
        Statut = Nothing
        PartNo = Nothing
    End Sub

    Public Sub New(mchName As String, Statut As String, shift As String, PartNo As String, Condition As String, ServerName As String, CycleCount As Integer, lastcycle As String, currentcycle As String, elapsedtime As String, feedOver As String)
        'Me.MchName = mchName
        'Me.Statut = Statut
        'Me.PartNo = PartNo
        'Me.Condition = Condition
        'Me.ServerName = ServerName

        ''new data
        'Me.CycleCount = CycleCount
        ''Me.LastCycle = LastCycle
        ''Me.CurrentCycle = CurrentCycle


        Try
            Me.Timeline = ""

            Me.MchName = mchName 'dr.Item("machine")
            Me.Statut = Statut 'dr.Item("status")
            Me.Shift = shift 'dr.Item("shift")
            Me.PartNo = PartNo 'dr.Item("PartNumber")

            'If Not (ServerName = "enet") Then Me.Condition = dr.Item("Condition")
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
            'Me.TimelineReset = False
            'Me.StatusChanged = False
            Me.Timeline = ""

            Try
                Me.MchName = dr.Item("machine")
            Catch ex As Exception

            End Try


            Try
                Me.Statut = dr.Item("status")
            Catch ex As Exception

            End Try

            Try
                Me.Shift = dr.Item("shift")
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
                Dim tempInt As Integer
                Integer.TryParse(dr.Item("CycleCount"), tempInt)
                Me.CycleCount = tempInt
            Catch ex As Exception

            End Try


            'Dim d As String = dr.Item("LastCycle")
            'Dim lastindex As Integer = d.IndexOf(":")
            'Dim hh As String = d.Substring(0, lastindex)
            'Dim mm As String = d.Substring(lastindex + 1, d.IndexOf(":", lastindex + 1) - lastindex - 1)
            'lastindex = d.IndexOf(":", lastindex + 1)
            'Dim ss As String = d.Substring(lastindex + 1)
            'Dim dt = New DateTime(1900, 1, 1, Integer.Parse(hh), Integer.Parse(mm), Integer.Parse(ss))
            'Me.LastCycle = dt

            Try
                Me.LastCycle = dr.Item("LastCycle")
            Catch ex As Exception

            End Try


            'd = dr.Item("CurrentCycle")
            'lastindex = d.IndexOf(":")
            'hh = d.Substring(0, lastindex)
            'mm = d.Substring(lastindex + 1, d.IndexOf(":", lastindex + 1) - lastindex - 1)
            'lastindex = d.IndexOf(":", lastindex + 1)
            'ss = d.Substring(lastindex + 1)
            'dt = New DateTime(1900, 1, 1, Integer.Parse(hh), Integer.Parse(mm), Integer.Parse(ss))
            'Me.CurrentCycle = dt

            'Try
            '    Me.CurrentCycle = dt
            'Catch ex As Exception

            'End Try

            Try
                Me.CurrentCycle = dr.Item("CurrentCycle")
            Catch ex As Exception

            End Try



            'd = dr.Item("ElapsedTime")
            'lastindex = d.IndexOf(":")
            'hh = d.Substring(0, lastindex)
            'mm = d.Substring(lastindex + 1, d.IndexOf(":", lastindex + 1) - lastindex - 1)
            'lastindex = d.IndexOf(":", lastindex + 1)
            'ss = d.Substring(lastindex + 1)
            'dt = New DateTime(1900, 1, 1, Integer.Parse(hh), Integer.Parse(mm), Integer.Parse(ss))
            'Me.ElapsedTime = dt

            'Try
            '    Me.ElapsedTime = dt
            'Catch ex As Exception

            'End Try

            Try
                Me.ElapsedTime = dr.Item("ElapsedTime")
            Catch ex As Exception

            End Try


            Dim feed As String = dr.Item("feedOverride")
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
End Class

Public Class CSI_DATA


    Implements IDisposable

    Public Shared CSIFLEX_VERSION As Integer = 18610
    Public Structure TimeLine
        Dim machine_name As String
        Dim date_ As String
        Dim Detailled_Shift1 As Detailled_Shift()
        Dim Detailled_Shift2 As Detailled_Shift()
        Dim Detailled_Shift3 As Detailled_Shift()
    End Structure

    Public Structure Detailled_Shift
        Dim Status As String
        Dim Time As DateTime
    End Structure

    'Public Structure part_perf
    '    Dim part As String
    '    Dim shift As Integer
    '    Dim date_ As DateTime
    '    Dim status As Dictionary(Of String, Integer)
    'End Structure

    Public Structure Mch_perf_byPart
        Dim part As String
        Dim Detailled_Shift1 As Detailled_Shift()
        Dim Detailled_Shift2 As Detailled_Shift()
        Dim Detailled_Shift3 As Detailled_Shift()
    End Structure

    <Serializable()>
    Public Structure periode
        Dim machine_name As String
        Dim date_ As String
        Dim shift1 As Dictionary(Of String, Double)
        Dim shift2 As Dictionary(Of String, Double)
        Dim shift3 As Dictionary(Of String, Double)
    End Structure

    ' Keep track of when the object is disposed. 
    Protected disposed As Boolean = False

    ' This method disposes the base object's resources. 
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                ' Insert code to free managed resources. 
            End If
            ' Insert code to free unmanaged resources. 
        End If
        Me.disposed = True
    End Sub

#Region " IDisposable Support "
    ' Do not change or add Overridable to these methods. 
    ' Put cleanup code in Dispose(ByVal disposing As Boolean). 
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region

    Public Sub New()

        'Dim machine_name As String = ""
        'Dim date_ As Date
        'Dim shift1 As Dictionary(Of String, Double)
        'Dim shift2 As Dictionary(Of String, Double)
        'Dim shift3 As Dictionary(Of String, Double)



    End Sub
End Class

Public Class part_perf


    Dim part As String = ""
    Dim shift_ As Integer = 0
    Dim date_ As DateTime
    Dim status As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
    Dim comment As String
    Public Sub New()
        part = ""
        shift_ = 0
        status = New Dictionary(Of String, Integer)
        comment = ""
    End Sub

    Public Sub clear()
        part = ""
        shift_ = 0
        status.Clear()
        comment = ""
    End Sub

    Public Property minmax() As String
        Get
            Return comment
        End Get
        Set(ByVal value As String)
            comment = value
        End Set
    End Property

    Public Sub identification(date_ As DateTime, shift As Integer, comment__ As String)
        part = comment__.Split(";")(0)
        shift_ = shift
        comment = comment__
    End Sub

    Public Property name() As Integer
        Get
            Return part
        End Get
        Set(ByVal value As Integer)
            part = value
        End Set
    End Property

    Public ReadOnly Property value(status_ As String) As Integer
        Get
            Return status.Item(status_)
        End Get
    End Property

    Public Sub add(status_ As String, value_ As Integer)

        If status.ContainsKey(status_) Then
            status.Item(status_) = status.Item(status_) + value_
        Else
            status.Add(status_, value_)
        End If

    End Sub


    Public Property shift() As Integer
        Get
            Return shift
        End Get
        Set(ByVal value As Integer)
            shift = value
        End Set
    End Property

    Public Property date_T() As DateTime
        Get
            Return date_
        End Get
        Set(ByVal value As DateTime)
            date_ = value
        End Set
    End Property




End Class

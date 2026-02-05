Public Class TimelineChange

    Private _timeid As Integer
    Public Property timeid() As Integer
        Get
            Return _timeid
        End Get
        Set(ByVal value As Integer)
            _timeid = value
        End Set
    End Property

    Private _selected As Boolean
    Public Property selected() As Boolean
        Get
            Return _selected
        End Get
        Set(ByVal value As Boolean)
            _selected = value
        End Set
    End Property

    Private _originaldate As DateTime
    Public Property originaldate() As DateTime
        Get
            Return _originaldate
        End Get
        Set(ByVal value As DateTime)
            _originaldate = value
        End Set
    End Property

    Private _action As String
    Public Property action() As String
        Get
            Return _action
        End Get
        Set(ByVal value As String)
            _action = value
        End Set
    End Property

    Private _status As String
    Public Property status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property

    Private _originalstatus As String
    Public Property originalstatus() As String
        Get
            Return _originalstatus
        End Get
        Set(ByVal value As String)
            _originalstatus = value
        End Set
    End Property

    Private _timestart As DateTime
    Public Property timestart() As DateTime
        Get
            Return _timestart
        End Get
        Set(ByVal value As DateTime)
            _timestart = value
        End Set
    End Property

    Private _timeend As DateTime
    Public Property timeend() As DateTime
        Get
            Return _timeend
        End Get
        Set(ByVal value As DateTime)
            _timeend = value
        End Set
    End Property

    Private _shift As Integer
    Public Property shift() As Integer
        Get
            Return _shift
        End Get
        Set(ByVal value As Integer)
            _shift = value
        End Set
    End Property

    Private _cycletime As Integer
    Public Property cycletime() As Integer
        Get
            Return _cycletime
        End Get
        Set(ByVal value As Integer)
            _cycletime = value
        End Set
    End Property

    Private _originalcycletime As Integer
    Public Property originalcycletime() As Integer
        Get
            Return _originalcycletime
        End Get
        Set(ByVal value As Integer)
            _originalcycletime = value
        End Set
    End Property

    Private _hasbefore As Boolean
    Public Property hasbefore() As Boolean
        Get
            Return _hasbefore
        End Get
        Set(ByVal value As Boolean)
            _hasbefore = value
        End Set
    End Property

    Private _hasafter As Boolean
    Public Property hasafter() As Boolean
        Get
            Return _hasafter
        End Get
        Set(ByVal value As Boolean)
            _hasafter = value
        End Set
    End Property

End Class

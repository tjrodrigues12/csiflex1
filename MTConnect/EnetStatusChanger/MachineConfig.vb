Public Class MachineConfig
    Private Property _machname As String
    Private Property _machlabel As String
    Private Property _enetpos As String
    Private Property _two_heads As Boolean
    Private Property _sendfolder As String
    Private Property _receivefolder As String
    Private Property _ftpfilename As String
    Private Property _cmd_con As String
    Private Property _cmd_coff As String
    Private Property _cmd_con2 As String
    Private Property _cmd_coff2 As String
    Private Property _cmd_prod As String
    Private Property _cmd_setup As String
    Private Property _cmd_partno As String
    Private Property _cmd_partno2 As String
    Private Property _cmd_others As New Dictionary(Of String, String)

    Public Sub New(machname As String, machpos As String, two_heads As Boolean, sendfolder As String, receivefolder As String)
        _machname = machname
        _enetpos = machpos
        _two_heads = two_heads
        _sendfolder = sendfolder
        _receivefolder = receivefolder
    End Sub

    ReadOnly Property MachineName As String
        Get
            Return _machname
        End Get
    End Property

    Property MachineLabel As String
        Get
            Return _machlabel
        End Get
        Set(value As String)
            _machlabel = value
        End Set
    End Property

    ReadOnly Property EnetPos As String
        Get
            Return _enetpos
        End Get
    End Property

    Property TwoHeads As String
        Get
            Return _two_heads
        End Get
        Set(value As String)
            _two_heads = value
        End Set
    End Property


    Property FTPFileName As String
        Get
            Return _ftpfilename
        End Get
        Set(value As String)
            _ftpfilename = value
        End Set
    End Property

    Property Cmd_CON As String
        Get
            Return _cmd_con
        End Get
        Set(value As String)
            _cmd_con = value
        End Set
    End Property

    Property Cmd_COFF As String
        Get
            Return _cmd_coff
        End Get
        Set(value As String)
            _cmd_coff = value
        End Set
    End Property

    Property Cmd_CON2 As String
        Get
            Return _cmd_con2
        End Get
        Set(value As String)
            _cmd_con2 = value
        End Set
    End Property

    Property Cmd_COFF2 As String
        Get
            Return _cmd_coff2
        End Get
        Set(value As String)
            _cmd_coff2 = value
        End Set
    End Property

    Property Cmd_PROD As String
        Get
            Return _cmd_prod
        End Get
        Set(value As String)
            _cmd_prod = value
        End Set
    End Property

    Property Cmd_SETUP As String
        Get
            Return _cmd_setup
        End Get
        Set(value As String)
            _cmd_setup = value
        End Set
    End Property

    Property Cmd_PARTNO As String
        Get
            Return _cmd_partno
        End Get
        Set(value As String)
            _cmd_partno = value
        End Set
    End Property

    Property Cmd_PARTNO2 As String
        Get
            Return _cmd_partno2
        End Get
        Set(value As String)
            _cmd_partno2 = value
        End Set
    End Property

    Property Cmd_OTHERS As Dictionary(Of String, String)
        Get
            Return _cmd_others
        End Get
        Set(value As Dictionary(Of String, String))
            _cmd_others = New Dictionary(Of String, String)(value)
        End Set
    End Property

End Class

Imports System
Imports System.Net
Imports System.Threading

Imports System.IO
Public Class FtpState
    Private wait As ManualResetEvent
    Private _request As FtpWebRequest
    'Private _fileName As String
    Private _filebyte() As Byte
    Private _operationException As Exception = Nothing
    Private status As String

    Public Sub New()
        wait = New ManualResetEvent(False)
    End Sub

    Public ReadOnly Property OperationComplete() As ManualResetEvent
        Get
            Return wait
        End Get
    End Property

    Public Property Request() As FtpWebRequest
        Get
            Return _request
        End Get
        Set(ByVal value As FtpWebRequest)
            _request = value
        End Set
    End Property

    'Public Property FileName() As String
    '    Get
    '        Return _fileName
    '    End Get
    '    Set(ByVal value As String)
    '        _fileName = value
    '    End Set
    'End Property

    Public Property FileByte() As Byte()
        Get
            Return _filebyte
        End Get
        Set(ByVal value As Byte())
            _filebyte = value
        End Set
    End Property
    Public Property OperationException() As Exception
        Get
            Return _operationException
        End Get
        Set(ByVal value As Exception)
            _operationException = value
        End Set
    End Property
    Public Property StatusDescription() As String
        Get
            Return status
        End Get
        Set(ByVal value As String)
            status = value
        End Set
    End Property
End Class
'Public Class AsynchronousFtpUpLoader
'    ' Command line arguments are two strings:
'    ' 1. The url that is the name of the file being uploaded to the server.
'    ' 2. The name of the file on the local machine.
'    '
'    Public Shared Sub Main(ByVal args() As String)
'        ' Create a Uri instance with the specified URI string.
'        ' If the URI is not correctly formed, the Uri constructor
'        ' will throw an exception.
'        Dim waitObject As ManualResetEvent

'        Dim target As New Uri(args(0))
'        Dim fileName As String = args(1)
'        Dim state As New FtpState()
'        Dim request As FtpWebRequest = CType(WebRequest.Create(target), FtpWebRequest)
'        request.Method = WebRequestMethods.Ftp.UploadFile

'        ' This example uses anonymous logon.
'        ' The request is anonymous by default; the credential does not have to be specified. 
'        ' The example specifies the credential only to
'        ' control how actions are logged on the server.

'        request.Credentials = New NetworkCredential("anonymous", "janeDoe@contoso.com")

'        ' Store the request in the object that we pass into the
'        ' asynchronous operations.
'        state.Request = request
'        state.FileName = fileName

'        ' Get the event to wait on.
'        waitObject = state.OperationComplete

'        ' Asynchronously get the stream for the file contents.
'        request.BeginGetRequestStream(New AsyncCallback(AddressOf EndGetStreamCallback), state)

'        ' Block the current thread until all operations are complete.
'        waitObject.WaitOne()

'        ' The operations either completed or threw an exception.
'        If state.OperationException IsNot Nothing Then
'            Throw state.OperationException
'        Else
'            Console.WriteLine("The operation completed - {0}", state.StatusDescription)
'        End If
'    End Sub
'    Private Shared Sub EndGetStreamCallback(ByVal ar As IAsyncResult)
'        Dim state As FtpState = CType(ar.AsyncState, FtpState)

'        Dim requestStream As Stream = Nothing
'        ' End the asynchronous call to get the request stream.
'        Try
'            requestStream = state.Request.EndGetRequestStream(ar)
'            ' Copy the file contents to the request stream.
'            Const bufferLength As Integer = 2048
'            Dim buffer(bufferLength - 1) As Byte
'            Dim count As Integer = 0
'            Dim readBytes As Integer = 0
'            Dim stream As FileStream = File.OpenRead(state.FileName)
'            Do
'                readBytes = stream.Read(buffer, 0, bufferLength)
'                requestStream.Write(buffer, 0, readBytes)
'                count += readBytes
'            Loop While readBytes <> 0
'            Console.WriteLine("Writing {0} bytes to the stream.", count)
'            ' IMPORTANT: Close the request stream before sending the request.
'            requestStream.Close()
'            ' Asynchronously get the response to the upload request.
'            state.Request.BeginGetResponse(New AsyncCallback(AddressOf EndGetResponseCallback), state)
'            ' Return exceptions to the main application thread.
'        Catch e As Exception
'            Console.WriteLine("Could not get the request stream.")
'            state.OperationException = e
'            state.OperationComplete.Set()
'            Return
'        End Try

'    End Sub

'    ' The EndGetResponseCallback method  
'    ' completes a call to BeginGetResponse.
'    Private Shared Sub EndGetResponseCallback(ByVal ar As IAsyncResult)
'        Dim state As FtpState = CType(ar.AsyncState, FtpState)
'        Dim response As FtpWebResponse = Nothing
'        Try
'            response = CType(state.Request.EndGetResponse(ar), FtpWebResponse)
'            response.Close()
'            state.StatusDescription = response.StatusDescription
'            ' Signal the main application thread that 
'            ' the operation is complete.
'            state.OperationComplete.Set()
'            ' Return exceptions to the main application thread.
'        Catch e As Exception
'            Console.WriteLine("Error getting response.")
'            state.OperationException = e
'            state.OperationComplete.Set()
'        End Try
'    End Sub
'End Class

Imports System.Runtime.CompilerServices

Public Module Extentions

    <Extension()>
    Public Function FirstUpCase(aString As String) As String

        Dim newString = aString.First().ToString().ToUpper() + aString.Substring(1)

        Return newString

    End Function

End Module

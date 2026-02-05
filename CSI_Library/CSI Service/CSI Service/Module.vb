Imports System.Threading
Imports CSI_Library




Imports System.IO.File
Imports System.IO

Imports System.Data.OleDb
Imports System.IO.IOException
Imports System.Text


'' '' '' ''Public dTime As Date
'' '' '' ''Dim lNum As Long

'' '' '' ''Sub RunOnTime()
'' '' '' ''    dTime = Now + TimeSerial(0, 0, 10)
'' '' '' ''    Application.OnTime(dTime, "RunOnTime")

'' '' '' ''    lNum = lNum + 1
'' '' '' ''    If lNum = 3 Then
'' '' '' ''        Run "CancelOnTime"
'' '' '' ''    Else
'' '' '' ''        MsgBox lNum
'' '' '' ''    End If

'' '' '' ''End Sub

'' '' '' ''Sub CancelOnTime()
'' '' '' ''    Application.OnTime(dTime, "RunOnTime", , False)
'' '' '' ''End Sub





Module Module1
    
    Public sched As Boolean = True


    Public path_db As String
    Public path_eNET As String
    Public week_ As String = 15
    Public CSI_Lib As CSI_Library.CSI_Library

    '-----------------------------------------------------------------------------------------------------------------------
    ' Main funct
    '-----------------------------------------------------------------------------------------------------------------------

    Sub Main()

        CSI_Lib = New CSI_Library.CSI_Library

        Dim checkFileInfo As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)

        If CSI_Lib.CheckFile("Setup_.csys", False) = True Then GoTo Stop_ ' missing file = true
        Using reader As StreamReader = New StreamReader(checkFileInfo.DirectoryName & "\" & "Setup_.csys")
            path_eNET = reader.ReadLine()
        End Using

        If CSI_Lib.CheckFile("Setupdb_.csys", False) = True Then GoTo Stop_ ' missing file = true
        Using reader As StreamReader = New StreamReader(checkFileInfo.DirectoryName & "\" & "Setupdb_.csys")
            path_db = reader.ReadLine()
        End Using

        If CSI_Lib.CheckFile("Setupdt_.csys", False) = True Then GoTo Stop_ ' missing file = true

        If CSI_Lib.CheckFile("auto_.csys", False) = True Then GoTo Stop_ ' missing file = true

        If CSI_Lib.Checkpath(path_db, False) = True Then GoTo Stop_ ' bad path = true
        If CSI_Lib.Checkpath(path_eNET, False) = True Then GoTo Stop_ ' bad path = true
        Dim updated As Boolean = False
Begin_:

        Dim last_date As String 'Last autoreport info
        Dim maj_auto As String 'export folder  
        Dim hour(2) As String

        Try
            Using reader As StreamReader = New StreamReader(checkFileInfo.DirectoryName & "\auto_.csys")
                maj_auto = reader.ReadLine() 'Folder
                last_date = reader.ReadLine() ' last date
                hour = Split(reader.ReadLine(), ":") ' hour of gen
                reader.Close()
            End Using

            CSI_Library.CSI_Library.Stand(Val(hour(0)), Val(hour(1))) 'Sleep


        Catch ex As Exception
            '  Dim checkFileInfo As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
            Using writer As StreamWriter = New StreamWriter(checkFileInfo.DirectoryName & "\Log_auto_reporting.csys", True)
                writer.WriteLine(Now.ToString & "  :  " & ex.ToString)
                writer.Close()
            End Using
            GoTo Stop_
        End Try

        ' Updates the database
        If updated = False Then
            'Call CSI_Lib.UpdateDB(path_eNET, path_db, False)
            'CSI_Lib.not_first_update(path_db, path_eNET, "mdb", False)
            CSI_Lib.UpdateDB_Mysql()
            updated = True
        End If


        Dim dates_ As String() = Split(last_date, "/")
        Dim d As Integer = Val(dates_(0))
        Dim m As Integer = Val(dates_(1))
        Dim y As Integer = Val(dates_(2)) - 2000

        ' if plus de 14 jour, ----> weekly
        If Now.Day <> d And Now.Month = m And (Now.Year - 2000) = y Then
            While d <> Now.Day Or m <> Now.Month Or Val(dates_(2)) <> Now.Year
                d = d + 1 ' The day after the last day
                If d = System.DateTime.DaysInMonth(y + 2000, m) + 1 Then 'If after the last day of the month
                    d = 1
                    m = m + 1
                    If m = 13 Then
                        m = 1
                        y = y + 1
                    End If
                End If

                '
            End While
        End If
        'Call generate

        Try
            If System.IO.File.Exists(checkFileInfo.DirectoryName & "\Auto_.csys") Then
                System.IO.File.Delete(checkFileInfo.DirectoryName & "\Auto_.csys")
                Using writer As StreamWriter = New StreamWriter(checkFileInfo.DirectoryName & "\Auto_.csys")
                    writer.Write(maj_auto & vbCrLf)
                    Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
                    writer.Write(todaysdate & vbCrLf)
                    writer.Write(hour(0).ToString & ":" & hour(1).ToString)
                    writer.Close()
                End Using
            End If
        Catch ex As Exception
            '  Dim checkFileInfo As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
            Using writer As StreamWriter = New StreamWriter(checkFileInfo.DirectoryName & "\Log_auto_reporting.csys", True)
                writer.WriteLine(Now.ToString & "  :  " & ex.ToString)
                writer.Close()
            End Using
            GoTo Stop_
        End Try
fin:
        If sched = False Then GoTo Stop_
        GoTo Begin_
Stop_:
    End Sub


    '-----------------------------------------------------------------------------------------------------------------------
    ' check if all data files are available
    '-----------------------------------------------------------------------------------------------------------------------
    Function CheckFiles() As Boolean
        Dim err As String = ""

        Dim checkFileInfo As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Try
            If Exists(checkFileInfo.DirectoryName & "\Setup_.csys") Then
                Using reader As StreamReader = New StreamReader(checkFileInfo.DirectoryName & "\Setup_.csys")
                    path_eNET = reader.ReadLine()
                End Using
            Else
                err = ("Setup_.sys not found")
            End If
        Catch ex As Exception
            err = ex.Message
        End Try

        If err <> "" Then
            Using writer As StreamWriter = New StreamWriter(checkFileInfo.DirectoryName & "\Log_auto_reporting.csys", True)
                writer.WriteLine(Now.ToString & "  :  " & err)
                writer.Close()
            End Using
            Return True
        End If






        If Exists(checkFileInfo.DirectoryName & "\Setupdb_.csys") Then
            Using reader As StreamReader = New StreamReader(checkFileInfo.DirectoryName & "\Setupdb_.csys")
                path_db = reader.ReadLine()
            End Using
        Else
            MsgBox("Please specify the Database folder")

            Return True
        End If

        If Exists(checkFileInfo.DirectoryName & "\Setupdt_.csys") Then
            Using reader As StreamReader = New StreamReader(checkFileInfo.DirectoryName & "\Setupdt_.csys")
                week_ = reader.ReadLine()
            End Using
        Else
            Using writer As StreamWriter = New StreamWriter(checkFileInfo.DirectoryName & "\Setupdt_.csys")
                writer.Write("15")
                writer.Close()
            End Using
        End If
       
        Return False
    End Function


   

End Module

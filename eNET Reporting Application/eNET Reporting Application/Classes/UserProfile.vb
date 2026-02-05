Imports CSIFLEX.Database.Access
Imports Encryption.Utilities
Imports MySql.Data.MySqlClient

Public Class UserProfile

    Public Property UserName As String = ""

    Public Property Password As String = ""

    Public Property Salt As String = ""

    Public Property FirstName As String = ""

    Public Property LastName As String = ""

    Public Property DisplayName As String = ""

    Public Property Email As String = ""

    Public Property UserType As String = ""

    Public Property RefId As String = ""

    Public Property Title As String = ""

    Public Property Dept As String = ""

    Public Property Machines As String = ""

    Public Property PhoneExt As String = ""

    Public Property EditTimeline As Boolean = False

    Public Property EditPartNumber As Boolean = False

    Public ReadOnly Property IsAdmin As Boolean
        Get
            Return UserType = "admin"
        End Get
    End Property

    Sub New()

    End Sub

    Sub New(userName As String)
        LoadUser(userName)
    End Sub

    Public Sub LoadUser(userName_ As String)

        Dim sqlCmd = $"SELECT * FROM CSI_auth.users WHERE username_ = '{userName_}'"

        Dim dtUser = MySqlAccess.GetDataTable(sqlCmd)

        If dtUser.Rows.Count = 0 Then
            Return
        End If

        Dim rowUser = dtUser.Rows(0)

        UserName = rowUser("username_").ToString()
        Password = rowUser("password_").ToString()
        Salt = rowUser("salt_").ToString()
        FirstName = rowUser("firstname_").ToString()
        DisplayName = rowUser("displayname").ToString()
        LastName = rowUser("Name_").ToString()
        Email = rowUser("email_").ToString()
        UserType = rowUser("usertype").ToString()
        RefId = rowUser("refId").ToString()
        Title = rowUser("title").ToString()
        Dept = rowUser("dept").ToString()
        Machines = rowUser("machines").ToString()
        PhoneExt = rowUser("phoneext").ToString()
        EditTimeline = rowUser("EditTimeline")
        EditPartNumber = rowUser("EditMasterPartData")

    End Sub

    Public Sub SaveUser(Optional savePassword As Boolean = False)

        If String.IsNullOrEmpty(UserName) Then
            Return
        End If

        Dim sqlCmd As New Text.StringBuilder()

        sqlCmd.Append($"INSERT INTO csi_auth.users    ")
        sqlCmd.Append($" (                            ")
        sqlCmd.Append($"    username_ ,               ")
        sqlCmd.Append($"    Name_     ,               ")
        sqlCmd.Append($"    firstname_,               ")
        sqlCmd.Append($"    email_    ,               ")
        sqlCmd.Append($"    usertype  ,               ")
        sqlCmd.Append($"    machines  ,               ")
        sqlCmd.Append($"    refId     ,               ")
        sqlCmd.Append($"    title     ,               ")
        sqlCmd.Append($"    dept      ,               ")
        sqlCmd.Append($"    phoneext                  ")
        sqlCmd.Append($" )                            ")
        sqlCmd.Append($" VALUES                       ")
        sqlCmd.Append($" (                            ")
        sqlCmd.Append($"    @username_ ,              ")
        sqlCmd.Append($"    @Name_     ,              ")
        sqlCmd.Append($"    @firstname_,              ")
        sqlCmd.Append($"    @email_    ,              ")
        sqlCmd.Append($"    @usertype  ,              ")
        sqlCmd.Append($"    @machines  ,              ")
        sqlCmd.Append($"    @refId     ,              ")
        sqlCmd.Append($"    @title     ,              ")
        sqlCmd.Append($"    @dept      ,              ")
        sqlCmd.Append($"    @phoneext                 ")
        sqlCmd.Append($" )                            ")
        sqlCmd.Append($" ON DUPLICATE KEY UPDATE      ")
        sqlCmd.Append($"    Name_      = @Name_     , ")
        sqlCmd.Append($"    firstname_ = @firstname_, ")
        sqlCmd.Append($"    email_     = @email_    , ")
        sqlCmd.Append($"    usertype   = @usertype  , ")
        sqlCmd.Append($"    machines   = @machines  , ")
        sqlCmd.Append($"    refId      = @refId     , ")
        sqlCmd.Append($"    title      = @title     , ")
        sqlCmd.Append($"    dept       = @dept      , ")
        sqlCmd.Append($"    phoneext   = @phoneext    ")

        Dim sqlCommand As New MySqlCommand(sqlCmd.ToString())
        sqlCommand.Parameters.AddWithValue("@username_", UserName)
        sqlCommand.Parameters.AddWithValue("@Name_", LastName)
        sqlCommand.Parameters.AddWithValue("@firstname_", FirstName)
        sqlCommand.Parameters.AddWithValue("@email_", Email)
        sqlCommand.Parameters.AddWithValue("@usertype", UserType)
        sqlCommand.Parameters.AddWithValue("@machines", Machines)
        sqlCommand.Parameters.AddWithValue("@refId", RefId)
        sqlCommand.Parameters.AddWithValue("@title", Title)
        sqlCommand.Parameters.AddWithValue("@dept", Dept)
        sqlCommand.Parameters.AddWithValue("@phoneext", PhoneExt)

        MySqlAccess.ExecuteNonQuery(sqlCommand)

        If savePassword Then

            Dim derivedKey = HashHelper.CreatePBKDF2Hash(Password)

            sqlCmd.Clear()
            sqlCmd.Append($"UPDATE csi_auth.users                                        ")
            sqlCmd.Append($" SET                                                         ")
            sqlCmd.Append($"    password_ = '{Convert.ToBase64String(derivedKey.Hash)}', ")
            sqlCmd.Append($"    salt_     = '{Convert.ToBase64String(derivedKey.Salt)}'  ")
            sqlCmd.Append($" WHERE                                                       ")
            sqlCmd.Append($"    username_ = @username_                                   ")

            sqlCommand = New MySqlCommand(sqlCmd.ToString())
            sqlCommand.Parameters.AddWithValue("@username_", UserName)

            MySqlAccess.ExecuteNonQuery(sqlCommand)

        End If

    End Sub

End Class

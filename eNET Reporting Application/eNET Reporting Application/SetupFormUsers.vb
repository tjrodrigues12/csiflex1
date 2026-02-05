Imports CSI_Library
Imports CSIFLEX.Database.Access
Imports Encryption.Utilities
Imports MySql.Data.MySqlClient

Partial Public Class SetupForm2

    Private userMachines As String = ""
    Private isNewUser As Boolean = False
    Private loadingUser As Boolean = True
    Private userId As Integer = 0

    Private Sub UsersTabSelected()

        treeViewUsers.Nodes(0).Nodes.Clear()
        Load_groupes()

        Dim dtUsers As DataTable = MySqlAccess.GetDataTable("SELECT * from CSI_auth.users ORDER BY UserType, UserName_")

        Dim userType = ""
        Dim userTypeNode As TreeNode
        Dim groupName As String = ""

        For Each user As DataRow In dtUsers.Rows

            If userTypeNode Is Nothing Or Not user("UserType").ToString().FirstUpCase() = userType Then

                userType = user("UserType").ToString().FirstUpCase()

                Select Case True
                    Case userType.StartsWith("Admin")
                        groupName = "Administrators"
                    Case userType.StartsWith("User")
                        groupName = "Users"
                    Case userType.StartsWith("Super")
                        groupName = "Supervisors"
                    Case userType.StartsWith("Mach")
                        groupName = "Machines"
                    Case userType.StartsWith("Opera")
                        groupName = "Operators"
                End Select

                If userTypeNode Is Nothing Then userTypeNode = New TreeNode(groupName)

                If userTypeNode.Nodes.Count > 0 Then
                    treeViewUsers.Nodes(0).Nodes.Add(userTypeNode)
                    userTypeNode = New TreeNode(groupName)
                End If
            End If

            Dim newNode As TreeNode = New TreeNode()
            newNode.Tag = user("Id").ToString()
            newNode.Name = user("UserName_").ToString()
            newNode.Text = user("UserName_").ToString()

            userTypeNode.Nodes.Add(newNode)
        Next

        If userTypeNode.Nodes.Count > 0 Then
            treeViewUsers.Nodes(0).Nodes.Add(userTypeNode)
            userTypeNode = New TreeNode()
        End If

        treeViewUsers.ExpandAll()

        treeviewMachines.Nodes.Clear()
        treeviewMachines.Nodes.Add("All Machines")

        For Each row As DataGridViewRow In gridviewMachines.Rows

            Dim node_ As New TreeNode, node_0 As New TreeNode

            If Not IsNothing(row.Cells("machines").Value) And Boolean.Parse(row.Cells("isMonitored").Value) Then
                node_.Tag = row.Cells("Id").Value
                node_.Name = row.Cells("machines").Value.ToString()
                node_.Text = row.Cells("machines").Value.ToString()

                treeviewMachines.Nodes(0).Nodes.Add(node_)

            End If
        Next

        'add other groups
        For Each node In treeviewGroupsOfMachines.Nodes
            treeviewMachines.Nodes.Add(node.clone)
        Next

        treeviewMachines.Nodes(0).EnsureVisible()

        treeviewMachines.Visible = False

        GB_Users.Visible = False
        'This code selects the first element of User's tree
        Dim nodes As TreeNodeCollection = treeViewUsers.Nodes(0).Nodes
        If nodes.Count > 0 Then
            ' Select the root node
            treeViewUsers.SelectedNode = treeViewUsers.Nodes(0).Nodes(0)
        End If
        btnShowPassword.Visible = True
    End Sub

    Private Sub btnAddUser_Click_1(sender As Object, e As EventArgs) Handles btnAddUser.Click

        clearuser()

        GB_Users.Visible = True
        treeviewMachines.Visible = True
        treeviewMachines.ExpandAll()

        treeViewUsers.Nodes(0).Nodes.Add("New User", "New User")
        treeViewUsers.SelectedNode = treeViewUsers.Nodes(0).Nodes(treeViewUsers.Nodes(0).Nodes.Count - 1)

        cmbUserType.Enabled = True

        txtUserName.Enabled = True
        txtUserName.Select()

        txtPassword.Enabled = True
        btnShowPassword.Visible = True

        isNewUser = True

    End Sub

    Private Sub treeViewUsers_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treeViewUsers.AfterSelect

        userId = e.Node.Tag

        'If userId Is Nothing Then userId = 0

        If (treeViewUsers.SelectedNode.Text = treeViewUsers.Nodes(0).Text) Then
            GB_Users.Visible = False
            treeviewMachines.Visible = False
        Else
            clearuser()
            loaduser()

            GB_Users.Visible = True
            treeviewMachines.ExpandAll()
            treeviewMachines.Visible = True
            btnSaveUser.Enabled = False
            treeviewMachines.Nodes(0).EnsureVisible()
        End If

    End Sub

    Private Sub treeViewUsers_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles treeViewUsers.NodeMouseClick

        If e.Button = Windows.Forms.MouseButtons.Right Then

            Try
                If e.Node.Text <> treeViewUsers.SelectedNode.Text Then
                    treeViewUsers.SelectedNode = e.Node
                End If

                ' Point where the mouse is clicked.
                Dim p As New Drawing.Point(e.X, e.Y)

                ' Get the node that the user has clicked.
                Dim node As TreeNode = treeViewUsers.GetNodeAt(p)
                If node IsNot Nothing Then

                    ' Select the node the user has clicked.
                    ' The node appears selected until the menu is displayed on the screen.
                    Dim m_OldSelectNode = treeviewDevices.SelectedNode
                    treeviewDevices.SelectedNode = node

                    ' Find the appropriate ContextMenu depending on the selected node.
                    'Dim nodename As String = Convert.ToString(node.Name)

                    If (node.Level = 0) Then
                        Dim cms = New ContextMenuStrip
                        Dim item1 = cms.Items.Add("Add User")
                        item1.Tag = 1
                        AddHandler item1.Click, AddressOf menuChoice2
                        cms.Show(treeViewUsers, p)
                    ElseIf (node.Level = 2 And Not treeViewUsers.SelectedNode.Text.ToUpper().Equals("ADMIN")) Then
                        Dim cms = New ContextMenuStrip
                        Dim item2 = cms.Items.Add("Delete User")
                        item2.Tag = 2
                        AddHandler item2.Click, AddressOf menuChoice2
                        cms.Show(treeViewUsers, p)
                    End If

                End If
            Catch ex As Exception
                MessageBox.Show("Error while adding user, see log")
                CSI_Lib.LogServerError("Error adding user:" + ex.Message, 1)
            End Try
        End If
    End Sub

    Private Sub User_TextChanged(sender As Object, e As EventArgs) Handles txtUserName.TextChanged, txtLastName.TextChanged, txtFirstName.TextChanged,
                                                                           txtPassword.TextChanged, txtUserTitle.TextChanged, txtUserRefID.TextChanged,
                                                                           txtUserExtention.TextChanged, txtUserEmail.TextChanged, txtUserDept.TextChanged,
                                                                           txtDisplayName.TextChanged, chkEditTimeline.CheckedChanged, chkEditPartNumber.CheckedChanged
        enableORnotSave()
    End Sub

    Private Sub cmbUserType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUserType.SelectedIndexChanged

        enableORnotSave()

        RemoveHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

        If Not loadingUser And treeviewMachines.Nodes.Count > 0 Then 'This checks if All Machines and Group Nodes exists in treeView6

            Dim checkAll As Boolean = (cmbUserType.SelectedItem.ToString().StartsWith("Admin"))

            For Each n As TreeNode In treeviewMachines.Nodes
                n.Checked = checkAll

                If n.Nodes.Count > 0 Then
                    For Each n1 As TreeNode In n.Nodes
                        n1.Checked = checkAll
                        If n1.Nodes.Count > 0 Then
                            For Each n2 As TreeNode In n1.Nodes
                                n2.Checked = checkAll
                            Next
                        End If
                    Next
                End If
            Next
        End If

        AddHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

    End Sub

    Private Sub btnEditPassword_Click(sender As Object, e As EventArgs) Handles btnEditPassword.Click
        txtPassword.Enabled = Not txtPassword.Enabled
        btnShowPassword.Visible = txtPassword.Enabled
    End Sub

    Private Sub btnShowPassword_MouseDown(sender As Object, e As EventArgs) Handles btnShowPassword.MouseDown
        txtPassword.PasswordChar = ControlChars.NullChar
    End Sub

    Private Sub btnShowPassword_MouseUp(sender As Object, e As EventArgs) Handles btnShowPassword.MouseUp
        txtPassword.PasswordChar = "*"c
    End Sub

    Private Sub treeviewMachines_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles treeviewMachines.AfterCheck

        Dim added_machine As New List(Of String)

        Dim nodeChecked As TreeNode = e.Node
        Dim nodeName As String = e.Node.Text
        Dim isNodeChecked As Boolean = e.Node.Checked
        Dim isAllMachines As Boolean = (nodeName = "All Machines")
        Dim isNodeGroup As Boolean = nodeChecked.Nodes.Count > 0
        Dim machineList As String = ""

        enableORnotSave()

        RemoveHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

        If cmbUserType.SelectedItem = "Machine" And e.Node.Checked And Not userMachines = "" Then

            e.Node.Checked = False

            AddHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck
            Return
        End If

        If isNodeGroup Then
            If nodeName = "All Machines" Then
                For Each node As TreeNode In treeviewMachines.Nodes
                    ChangeAllNode(node, isNodeChecked)
                Next
            Else
                ChangeGroupNode(nodeChecked, isNodeChecked)
            End If
        Else
            For Each node As TreeNode In treeviewMachines.Nodes
                ChangeMachineNode(node, nodeName, isNodeChecked)
            Next
        End If

        For Each node As TreeNode In treeviewMachines.Nodes(0).Nodes
            If node.Checked Then
                machineList &= $",{node.Tag}"
            End If
        Next

        If Not String.IsNullOrEmpty(machineList) Then machineList = machineList.Substring(1)

        'Partially Selected Checkbox Logic is here 
        Dim SelectedMachineCount As Integer
        Dim NoofGrps As Integer
        Dim AllMachineCount As Integer
        Dim totalgrp As Integer
        Dim groups_ As String = ""

        totalgrp = 0
        SelectedMachineCount = 0
        NoofGrps = 0
        AllMachineCount = 0

        'All Machines
        For Each SNode As TreeNode In treeviewMachines.Nodes(0).Nodes
            If SNode.Checked Then
                SelectedMachineCount += 1
            End If
            AllMachineCount += 1
        Next

        If SelectedMachineCount < AllMachineCount And SelectedMachineCount > 0 Then
            treeviewMachines.Nodes(0).ForeColor = Color.DarkGray
            treeviewMachines.Nodes(0).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
            treeviewMachines.Nodes(0).Text += String.Empty
        ElseIf SelectedMachineCount = AllMachineCount And SelectedMachineCount > 0 Then
            groups_ += treeviewMachines.Nodes(0).Text & ", "
            treeviewMachines.Nodes(0).ForeColor = Color.Black
            treeviewMachines.Nodes(0).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
            treeviewMachines.Nodes(0).Text += String.Empty
        Else
            treeviewMachines.Nodes(0).ForeColor = Color.Black
            treeviewMachines.Nodes(0).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Regular)
        End If

        'Group of Machines
        NoofGrps = treeviewMachines.Nodes(1).Nodes.Count
        For Each TotalGroups As TreeNode In treeviewMachines.Nodes(1).Nodes
            If TotalGroups.Checked Then
                totalgrp += 1
            End If
        Next

        If totalgrp < NoofGrps And totalgrp > 0 Then
            treeviewMachines.Nodes(1).ForeColor = Color.DarkGray
            treeviewMachines.Nodes(1).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
            treeviewMachines.Nodes(1).Text += String.Empty
        ElseIf totalgrp = NoofGrps And totalgrp > 0 Then
            treeviewMachines.Nodes(1).ForeColor = Color.Black
            treeviewMachines.Nodes(1).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
            treeviewMachines.Nodes(1).Text += String.Empty
        Else
            treeviewMachines.Nodes(1).ForeColor = Color.Black
            treeviewMachines.Nodes(1).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Regular)
        End If

        'Particular Group Machines
        Dim Groups(NoofGrps - 1) As Integer

        For grp As Integer = 0 To (Groups.Length - 1)
            Dim SelectedGroupCount As Integer
            SelectedGroupCount = 0
            Groups.SetValue(treeviewMachines.Nodes(1).Nodes(grp).Nodes.Count, grp)
            For Each GNode As TreeNode In treeviewMachines.Nodes(1).Nodes(grp).Nodes
                If GNode.Checked Then
                    SelectedGroupCount += 1
                End If
            Next
            If SelectedGroupCount < Groups(grp) And SelectedGroupCount > 0 Then
                treeviewMachines.Nodes(1).Nodes(grp).Checked = False
                treeviewMachines.Nodes(1).Nodes(grp).ForeColor = Color.DarkGray
                treeviewMachines.Nodes(1).Nodes(grp).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
                treeviewMachines.Nodes(1).Nodes(grp).Text += String.Empty
                treeviewMachines.Nodes(1).ForeColor = Color.DarkGray
                treeviewMachines.Nodes(1).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
                treeviewMachines.Nodes(1).Text += String.Empty
            ElseIf SelectedGroupCount = Groups(grp) And SelectedGroupCount > 0 Then
                treeviewMachines.Nodes(1).Nodes(grp).Checked = True
                treeviewMachines.Nodes(1).Nodes(grp).ForeColor = Color.Black
                treeviewMachines.Nodes(1).Nodes(grp).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Bold)
                treeviewMachines.Nodes(1).Nodes(grp).Text += String.Empty
                groups_ += treeviewMachines.Nodes(1).Nodes(grp).Text & ","
            Else
                treeviewMachines.Nodes(1).Nodes(grp).Checked = False
                treeviewMachines.Nodes(1).Nodes(grp).ForeColor = Color.Black
                treeviewMachines.Nodes(1).Nodes(grp).NodeFont = New Font(treeviewMachines.Font, System.Drawing.FontStyle.Regular)
            End If
        Next

        groups_ = groups_.TrimEnd(",")

        userMachines = IIf(isAllMachines, "ALL", machineList)

        AddHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

    End Sub

    Private Sub btnSaveUser_Click(sender As Object, e As EventArgs) Handles btnSaveUser.Click

        If isNewUser Then
            Dim userExists As Boolean = MySqlAccess.ExecuteScalar($"SELECT EXISTS(SELECT username_ FROM csi_auth.users WHERE username_ = '{txtUserName.Text}')")
            If userExists Then
                MessageBox.Show("User Name already exists. Choose another user name.")
                Return
            End If
            Dim refid = MySqlAccess.GetDataTable($"SELECT username_ FROM csi_auth.users WHERE refid = '{txtUserRefID.Text}';")
            If refid.Rows.Count > 0 Then
                MessageBox.Show($"The reference ID '{txtUserRefID.Text}' has already been registered with the user '{refid.Rows(0)(0)}'")
                Return
            End If
        End If

        If txtPassword.Enabled And String.IsNullOrEmpty(txtPassword.Text) Then
            MessageBox.Show("Password field is required.")
            Return
        End If

        Dim userType As String = cmbUserType.SelectedItem.ToString()

        If txtUserName.Text = "Mobile" Then
            userType = "Admin"
            txtPassword.Text = ";p4csiflex"
        End If

        Dim sqlCmd As New Text.StringBuilder()

        If userId = 0 Then
            sqlCmd.Append($"INSERT INTO csi_auth.users ")
            sqlCmd.Append($" (                         ")
            sqlCmd.Append($"    username_   ,          ")
            sqlCmd.Append($"    Name_       ,          ")
            sqlCmd.Append($"    firstname_  ,          ")
            sqlCmd.Append($"    displayname ,          ")
            sqlCmd.Append($"    email_      ,          ")
            sqlCmd.Append($"    usertype    ,          ")
            sqlCmd.Append($"    machines    ,          ")
            sqlCmd.Append($"    refId       ,          ")
            sqlCmd.Append($"    title       ,          ")
            sqlCmd.Append($"    dept        ,          ")
            sqlCmd.Append($"    phoneext    ,          ")
            sqlCmd.Append($"    EditTimeline,          ")
            sqlCmd.Append($"    EditMasterPartData     ")
            sqlCmd.Append($" )                         ")
            sqlCmd.Append($" VALUES                    ")
            sqlCmd.Append($" (                         ")
            sqlCmd.Append($"    @username_   ,         ")
            sqlCmd.Append($"    @Name_       ,         ")
            sqlCmd.Append($"    @firstname_  ,         ")
            sqlCmd.Append($"    @displayname ,         ")
            sqlCmd.Append($"    @email_      ,         ")
            sqlCmd.Append($"    @usertype    ,         ")
            sqlCmd.Append($"    @machines    ,         ")
            sqlCmd.Append($"    @refId       ,         ")
            sqlCmd.Append($"    @title       ,         ")
            sqlCmd.Append($"    @dept        ,         ")
            sqlCmd.Append($"    @phoneext    ,         ")
            sqlCmd.Append($"    @EditTimeline,         ")
            sqlCmd.Append($"    @EditMasterPartData    ")
            sqlCmd.Append($" )                         ")
        Else
            sqlCmd.Append($"UPDATE csi_auth.users SET         ")
            sqlCmd.Append($"    username_    = @username_   , ")
            sqlCmd.Append($"    Name_        = @Name_       , ")
            sqlCmd.Append($"    firstname_   = @firstname_  , ")
            sqlCmd.Append($"    displayname  = @displayname , ")
            sqlCmd.Append($"    email_       = @email_      , ")
            sqlCmd.Append($"    usertype     = @usertype    , ")
            sqlCmd.Append($"    machines     = @machines    , ")
            sqlCmd.Append($"    refId        = @refId       , ")
            sqlCmd.Append($"    title        = @title       , ")
            sqlCmd.Append($"    dept         = @dept        , ")
            sqlCmd.Append($"    phoneext     = @phoneext    , ")
            sqlCmd.Append($"    EditTimeline = @Edittimeline, ")
            sqlCmd.Append($"    EditMasterPartData = @EditMasterPartData ")
            sqlCmd.Append($" WHERE Id = {userId}              ")
        End If

        Dim sqlCommand As New MySqlCommand(sqlCmd.ToString())
        sqlCommand.Parameters.AddWithValue("@username_", txtUserName.Text)
        sqlCommand.Parameters.AddWithValue("@Name_", txtLastName.Text)
        sqlCommand.Parameters.AddWithValue("@firstname_", txtFirstName.Text)
        sqlCommand.Parameters.AddWithValue("@displayname", IIf(String.IsNullOrEmpty(txtDisplayName.Text), $"{txtLastName.Text}, {txtFirstName.Text}", txtDisplayName.Text))
        sqlCommand.Parameters.AddWithValue("@email_", txtUserEmail.Text)
        sqlCommand.Parameters.AddWithValue("@usertype", userType)
        sqlCommand.Parameters.AddWithValue("@machines", userMachines)
        sqlCommand.Parameters.AddWithValue("@refId", txtUserRefID.Text)
        sqlCommand.Parameters.AddWithValue("@title", txtUserTitle.Text)
        sqlCommand.Parameters.AddWithValue("@dept", txtUserDept.Text)
        sqlCommand.Parameters.AddWithValue("@phoneext", txtUserExtention.Text)
        sqlCommand.Parameters.AddWithValue("@EditTimeline", chkEditTimeline.Checked)
        sqlCommand.Parameters.AddWithValue("@EditMasterPartData", chkEditPartNumber.Checked)

        MySqlAccess.ExecuteNonQuery(sqlCommand)

        MySqlAccess.ExecuteNonQuery($"UPDATE csi_auth.users SET refid = id WHERE id = {userId} AND (refid = '' OR refid IS NULL);")

        If txtPassword.Enabled Then

            Dim derivedKey = HashHelper.CreatePBKDF2Hash(txtPassword.Text) 'AES_Encrypt(TB_Pass.Text, "pass").ToString()

            sqlCmd.Clear()
            sqlCmd.Append($"UPDATE csi_auth.users                                        ")
            sqlCmd.Append($" SET                                                         ")
            sqlCmd.Append($"    password_ = '{Convert.ToBase64String(derivedKey.Hash)}', ")
            sqlCmd.Append($"    salt_     = '{Convert.ToBase64String(derivedKey.Salt)}'  ")
            sqlCmd.Append($" WHERE                                                       ")
            sqlCmd.Append($"    username_ = @username_                                   ")

            sqlCommand = New MySqlCommand(sqlCmd.ToString())
            sqlCommand.Parameters.AddWithValue("@username_", txtUserName.Text)

            MySqlAccess.ExecuteNonQuery(sqlCommand)
        End If


        If treeViewUsers.SelectedNode IsNot Nothing Then

            Dim selectedNode As TreeNode = treeViewUsers.SelectedNode
            selectedNode.Text = txtUserName.Text
            selectedNode.Name = selectedNode.Text

            If selectedNode.Parent.Text = "User Types" Then

                treeViewUsers.SelectedNode.Remove()

                Dim groupNode As TreeNode

                Select Case True
                    Case userType.StartsWith("Admin")
                        userType = "Administrators"
                    Case userType.StartsWith("User")
                        userType = "Users"
                    Case userType.StartsWith("Super")
                        userType = "Supervisors"
                    Case userType.StartsWith("Mach")
                        userType = "Machines"
                    Case userType.StartsWith("Opera")
                        userType = "Operators"
                End Select

                For Each n As TreeNode In treeViewUsers.Nodes(0).Nodes
                    If n.Text = userType Then
                        groupNode = n
                    End If
                Next

                If groupNode IsNot Nothing Then
                    groupNode.Nodes.Add(selectedNode)
                Else
                    treeViewUsers.Nodes(0).Nodes.Add(cmbUserType.SelectedItem.ToString()).Nodes.Add(selectedNode)
                End If

            End If

            loaduser()
        End If

        Dim admin As Boolean = False
        If (cmbUserType.SelectedItem.ToString().StartsWith("Admin")) Then
            admin = True
        End If

        txtPassword.Text = ""

        enableORnotSave()

        MessageBox.Show("Your changes have been saved !")

    End Sub


    Private Sub menuChoice2(ByVal sender As Object, ByVal e As EventArgs)

        Dim item = CType(sender, ToolStripMenuItem)
        Dim selection = CInt(item.Tag)

        If (selection = 1) Then
            clearuser()
            GB_Users.Visible = True
            treeviewMachines.Visible = True
            treeviewMachines.ExpandAll()
            treeViewUsers.Nodes(0).Nodes.Add("New User")
            treeViewUsers.SelectedNode = treeViewUsers.Nodes(0).Nodes("New User")
        End If

        If (selection = 2) Then
            Dim cntsql As New MySqlConnection(CSI_Library.CSI_Library.MySqlConnectionString)
            cntsql.Open()
            Dim cmdsql As New MySqlCommand("delete from CSI_auth.users where username_ = '" & treeViewUsers.SelectedNode.Text & "'", cntsql)
            cmdsql.ExecuteNonQuery()
            treeViewUsers.SelectedNode.Remove()
        End If
    End Sub


    Private Sub ChangeAllNode(nodeBase As TreeNode, checked As Boolean)

        nodeBase.Checked = checked

        For Each node As TreeNode In nodeBase.Nodes

            If node.Nodes.Count > 0 Then
                ChangeAllNode(node, checked)
            Else
                node.Checked = checked
            End If
        Next

    End Sub

    Private Function ChangeMachineNode(nodeBase As TreeNode, machine As String, checked As Boolean) As Boolean

        Dim grpChecked As Boolean = True

        For Each node As TreeNode In nodeBase.Nodes

            If node.Nodes.Count > 0 Then
                If Not ChangeMachineNode(node, machine, checked) Then grpChecked = False
            Else
                If node.Text = machine Then
                    node.Checked = checked
                End If

                If Not node.Checked Then grpChecked = False
            End If
        Next

        nodeBase.Checked = grpChecked

        Return grpChecked

    End Function

    Private Sub ChangeGroupNode(nodeBase As TreeNode, checked As Boolean)

        nodeBase.Checked = checked

        For Each node As TreeNode In nodeBase.Nodes
            If node.Nodes.Count > 0 Then
                ChangeGroupNode(node, checked)
            Else
                ChangeMachineNode(treeviewMachines.Nodes(0), node.Text, checked)
                If treeviewMachines.Nodes.Count > 1 Then
                    ChangeMachineNode(treeviewMachines.Nodes(1), node.Text, checked)
                End If
            End If
        Next

    End Sub

    Private Sub loaduser()

        Dim userProfile = New UserProfile(treeViewUsers.SelectedNode.Name)

        loadingUser = True

        clearuser()

        txtUserName.Enabled = False
        txtPassword.Enabled = False
        'btnShowPassword.Visible = False
        cmbUserType.Enabled = True
        btnSaveUser.Enabled = True
        btnEditPassword.Enabled = True

        If Not userProfile.UserName = "" Then

            txtUserName.Text = userProfile.UserName
            txtLastName.Text = userProfile.LastName
            txtFirstName.Text = userProfile.FirstName
            txtDisplayName.Text = userProfile.DisplayName
            txtUserEmail.Text = userProfile.Email
            txtUserRefID.Text = userProfile.RefId
            txtUserTitle.Text = userProfile.Title
            txtUserDept.Text = userProfile.Dept
            txtUserExtention.Text = userProfile.PhoneExt
            cmbUserType.SelectedIndex = cmbUserType.FindStringExact(userProfile.UserType.FirstUpCase())
            chkEditTimeline.Checked = userProfile.EditTimeline
            chkEditPartNumber.Checked = userProfile.EditPartNumber

            If userProfile.UserType.FirstUpCase().StartsWith("Admin") Then
                cmbUserType.SelectedIndex = cmbUserType.FindStringExact("Administrator")
            End If

            If userProfile.UserName.FirstUpCase().StartsWith("Admin") Then
                cmbUserType.Enabled = False
            End If

            isNewUser = False

        ElseIf treeViewUsers.SelectedNode.Name = "" Then

            btnSaveUser.Enabled = False
            isNewUser = False

        Else

            txtUserName.Enabled = True
            txtUserName.Text = ""
            txtPassword.Enabled = True
            btnShowPassword.Visible = True

            isNewUser = True

        End If

        If txtUserName.Text = "Mobile" Then
            txtUserName.Enabled = False
            cmbUserType.Enabled = False
            btnEditPassword.Enabled = False
        End If

        loadingUser = False

        LoadMachines()

    End Sub

    Private Sub clearuser()

        txtUserName.Text = ""
        txtLastName.Text = ""
        txtFirstName.Text = ""
        txtDisplayName.Text = ""
        txtPassword.Text = ""
        txtUserEmail.Text = ""
        txtUserRefID.Text = ""
        txtUserTitle.Text = ""
        txtUserDept.Text = ""
        txtUserExtention.Text = ""
        chkEditTimeline.Checked = False
        chkEditPartNumber.Checked = False

        cmbUserType.Enabled = True

        RemoveHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

        If treeviewMachines.Nodes.Count > 0 Then 'This checks if All Machines and Group Nodes exists in treeView6

            For Each n As TreeNode In treeviewMachines.Nodes
                n.Checked = False

                If n.Nodes.Count > 0 Then
                    For Each n1 As TreeNode In n.Nodes
                        n1.Checked = False
                        If n1.Nodes.Count > 0 Then
                            For Each n2 As TreeNode In n1.Nodes
                                n2.Checked = False
                            Next
                        End If
                    Next
                End If
            Next
        End If

        userMachines = ""

        AddHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

    End Sub

    Private Sub LoadMachines()

        Dim tableMachines = MySqlAccess.GetDataTable($"SELECT * FROM csi_auth.tbl_ehub_conf")

        Dim tbUserMachines As DataTable = MySqlAccess.GetDataTable($"SELECT machines from CSI_auth.users WHERE username_ = '{ treeViewUsers.SelectedNode.Text }'")

        If tbUserMachines.Rows.Count > 0 Then

            Dim machines As String = tbUserMachines.Rows(0).Item(0).ToString()
            Dim lstMachines As List(Of String) = New List(Of String)()

            If machines = "ALL" Then
                'machines = ""
                For Each mch As DataRow In tableMachines.Rows
                    If mch("MonState") = 1 Then
                        lstMachines.Add(mch("Id"))
                        'machines = String.Concat(machines, $",{mch("Id")}")
                    End If
                Next

                'machines = machines.Substring(1)
            Else
                lstMachines.AddRange(machines.Split(",").Select(Function(s) s.Trim()))
            End If

            RemoveHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

            CheckMachines(treeviewMachines.Nodes, lstMachines)

            AddHandler treeviewMachines.AfterCheck, AddressOf treeviewMachines_AfterCheck

            treeviewMachines.Nodes(0).EnsureVisible()

            Return

            'For Each p As TreeNode In treeviewMachines.Nodes
            '    p.Checked = False
            '    For Each n As TreeNode In p.Nodes
            '        n.Checked = False
            '        For Each m As TreeNode In n.Nodes
            '            m.Checked = False
            '        Next
            '    Next
            'Next

            'Dim dt As List(Of String) = machines.Split(",").Select(Function(s) s.Trim()).ToList()

            'If dt.Count = 0 Then
            '    Return
            'End If

            'Dim machId As Integer = 0
            'Dim row As DataRow

            'For Each machine As String In dt

            '    If Not String.IsNullOrEmpty(machine) And Not Integer.TryParse(machine, machId) Then
            '        row = tableMachines.Rows.Cast(Of DataRow).Where(Function(r) r.Item("EnetMachineName").ToString() = machine).FirstOrDefault()
            '        machId = row.Item("Id")
            '    End If

            '    For Each p As TreeNode In treeviewMachines.Nodes

            '        If p.Tag = machId Then
            '            p.Checked = True
            '        Else
            '            For Each n As TreeNode In p.Nodes
            '                If n.Tag = machId Then
            '                    n.Checked = True
            '                Else
            '                    For Each m As TreeNode In n.Nodes
            '                        If m.Tag = machId Then
            '                            m.Checked = True
            '                        End If
            '                    Next
            '                End If
            '            Next
            '        End If
            '    Next
            'Next

            'treeviewMachines.Nodes(0).EnsureVisible()

        End If
    End Sub

    Private Sub enableORnotSave()
        Try
            If (txtUserName.Text <> "" And txtLastName.Text <> "" And txtFirstName.Text <> "" And cmbUserType.SelectedItem <> Nothing) Then
                btnSaveUser.Enabled = True
            Else
                btnSaveUser.Enabled = False
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CheckMachines(nodeCollection As TreeNodeCollection, machines As List(Of String))

        For Each node As TreeNode In nodeCollection

            If node.Nodes.Count > 0 Then
                CheckMachines(node.Nodes, machines)
            Else
                node.Checked = machines.Contains(node.Tag)
            End If

        Next

    End Sub

End Class

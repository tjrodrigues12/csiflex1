Public NotInheritable Class AboutBox1

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Définissez le titre du formulaire.
        Dim ApplicationTitle As String = "CSI Flex Intelligent Reporting Application"
        'If My.Application.Info.Title <> "" Then
        '    ApplicationTitle = My.Application.Info.Title
        'Else
        '    ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        'End If
        Me.Text = String.Format("À propos de {0}", ApplicationTitle)
        ' Initialisez tout le texte affiché dans la boîte de dialogue À propos de.
        ' TODO: personnalisez les informations d'assembly de l'application dans le volet "Application" de la 
        '    boîte de dialogue Propriétés du projet (sous le menu "Projet").
        Me.LabelProductName.Text = "CSI Flex Intelligent Reporting Application"
        ' Me.LabelVersion.Text = "1.8.6.4"
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = "CSI Flex"

        Dim credits As String = "Customer support:" & System.Environment.NewLine & System.Environment.NewLine &
"CAMSolutions is committed to providing you with every opportunity to communicate directly with us so you benefit by helping us continuously improve our products, services, and build our growing community. We want to help you utilise CSIFlex efficiently to improve your manufacturing environment by focusing in the real problems." & System.Environment.NewLine & System.Environment.NewLine &
"CSIFlex's annual maintenance fee includes software updates and remote technical support, so you can be confident that you will always have access to expert technical resources and the latest software. Subscription services provide the latest product releases, direct access to CSIFlex technical resources via phone, email, and chat." & System.Environment.NewLine & System.Environment.NewLine &
"To contact customer support:" & System.Environment.NewLine &
"-	Email: support@CSIFlex.com" & System.Environment.NewLine &
"-	Toll free phone: 1.888.289.5617" & System.Environment.NewLine &
"-	Phone: 1.514.394.7931" & System.Environment.NewLine &
"-	Phones are staffed 8:30am - 6pm EST, Monday-Friday, excluding Canada national holidays" & System.Environment.NewLine &
"-	Website: www.CSIFlex.com" & System.Environment.NewLine & System.Environment.NewLine &
"When contacting customer support, the following information may be needed to properly diagnose your issue:" & System.Environment.NewLine &
"-	CSIFlex version number" & System.Environment.NewLine &
"-	Environment details (operating system, hardware, graphics card)" & System.Environment.NewLine &
"-	Brief description of your issue" & System.Environment.NewLine &
"-	Detailed steps to reproduce the issue" & System.Environment.NewLine &
"-	Related files (log files, data files)" & System.Environment.NewLine & System.Environment.NewLine &
"Gathering this information before contacting customer support could help us find a resolution more quickly."

        Me.TextBoxDescription.Text = credits & System.Environment.NewLine & My.Application.Info.Description
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub


    Private Sub LogoPictureBox_Click(sender As Object, e As EventArgs) Handles LogoPictureBox.Click
        System.Diagnostics.Process.Start("http://csiflex.com")
    End Sub

    Private Sub LabelVersion_Click(sender As Object, e As EventArgs) Handles LabelVersion.Click
        Changes.ShowDialog()
    End Sub
End Class

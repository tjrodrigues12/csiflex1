Public Class Contact_info
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim theStringBuilder As New System.Text.StringBuilder()

        theStringBuilder.Append("mailto:support@CSIFlex.com")

        'theStringBuilder.Append("&subject=My subject")

        ' theStringBuilder.Append("&body=My email body")

        System.Diagnostics.Process.Start(theStringBuilder.ToString())
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim theStringBuilder As New System.Text.StringBuilder()

        theStringBuilder.Append("mailto:support@CSIFlex.com")

        'theStringBuilder.Append("&subject=My subject")

        ' theStringBuilder.Append("&body=My email body")

        System.Diagnostics.Process.Start(theStringBuilder.ToString())
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim webAddress As String = "http://www.csiflex.ca/"
        Process.Start(webAddress)
    End Sub

    Private Sub Contact_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.SelectionLength = 0
    End Sub
End Class
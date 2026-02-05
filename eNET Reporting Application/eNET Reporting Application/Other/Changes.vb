Public Class Changes
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub Changes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim s As String = ""
        's = "Version 2.1.8.4 - 2016 feb 02" & vbCrLf & vbCrLf & vbCrLf
        's = s & "New : This 'What's new' window."
        's = s & "New : After the installation CSIFlex will check how many years of data are available, and asks which year do you want to import."
        's = s & "New : When no parts were made, the parts timeline gives a continuous gray TimeLine with the message 'no part where made'" & vbCrLf & vbCrLf
        's = s & "Fixe : If a part does not have a name ... the parts timeline will display 'No Part Number'" & vbCrLf & vbCrLf
        's = s & "Fixe : Stability improvements"


        Dim sb = New System.Text.StringBuilder()
        sb.Append("{\rtf1\ansi")


        sb.Append("--- \b Version 1.8.6.8\b0  :  2016 Nov 25 \line \line ")

        sb.Append("\b Fixe : \b0 Interface update. \line \line ")
        sb.Append("\b New : \b0  CSIFlex will send the status 'MCH UNREACHABLE' if an MTConnect Machine does not respond after 2sec.  \line \line ")
        sb.Append("\b Fixe : \b0  MTConnect Library stability fixe \line \line ")
        sb.Append("\b Fixe : \b0  MTConnect connection TimeOut fixe \line \line ")
        sb.Append("\b Fixe : \b0  The PartNumbers with MTConnect  \line \line ")
        sb.Append("\b Fixe : \b0 Connect a device with the MAC address  \line \line ")
        sb.Append("\b Fixe : \b0  Stability fixe with the Utilization computing  \line \line ")
        sb.Append("\b Fixe : \b0  Performances.  \line \line ")


        sb.Append("--- \b Version 1.8.6.7\b0  :  2016 Aug 26 \line \line ")

        sb.Append("\b New : \b0 CSIFlex works now without wamp. \line \line ")
        sb.Append("\b New : \b0 Embedded web server more resource efficient. \line \line ")
        sb.Append("\b New : \b0 A new integrated web dashboard. \line \line ")

        sb.Append("\b Fixe : \b0 New architecture to generate the automated reports. \line \line ")
        sb.Append("\b Fixe : \b0 Stability. \line \line ")

        sb.Append("--- \b Version 1.8.6.6\b0  :  2016 may 31 \line \line ")

        sb.Append("\b New : \b0  The Advanced MTConnect/Focas Editor, to define status, conditions and notifications with mtconnect/Focas machines. \line \line ")

        sb.Append("\b New : \b0  Remember Login/password. CSIFlex will store one login/password per windows username. \line \line ")

        sb.Append("\b New : \b0  Test email button in the email server config window. \line \line ")

        ' sb.Append("\b Fixe : \b0  Instability fixes. \line \line ")

        sb.Append("\b Fix : \b0  Stability improvements, bug fixes. \line \line ")

        sb.Append("--- \b Version 1.8.6.5\b0  :  2016 Apr 04 \line \line ")

        sb.Append("\b Fix : \b0  Stability improvements, bug fixes. \line \line ")

        'sb.Append("\b Fixe : \b0  CSIFlex was not working on a fresh installed eNET with no CSV files. \line \line ")

        'sb.Append("\b New : \b0  Rainmeter will be configured to use the CSIFlex server HTTP server. \line \line ")

        'sb.Append("--- \b Version 1.8.6.4\b0  :  2016 Mar 08 \line \line ")

        sb.Append("\b New : \b0  This 'What's new' window. \line \line ")


        'sb.Append("\b Fixe : \b0  Stability improvements, bug fixes \line \line")
        'sb.Append("\b New : \b0  Interface design improvement. \line \line ")

        'sb.Append("\b Fixe : \b0  The buttons 'by days' and 'by weeks' in the main dashboard change the selected machine when multiple machines are selected. \line \line ")
        'sb.Append("\b New : \b0  Doubleclic and right clic on a cell in the partnumber list. \line \line ")
        'sb.Append("\b Fixe : \b0  The part filtering not working on a french computer \line \line")
        'sb.Append("\b Fixe : \b0  Print page \line \line")
        'sb.Append("\b New : \b0  After the first installation CSIFlex will check how many years of data are available, and asks which year do you want to import. \line \line ")
        'sb.Append("\b Fixe : \b0  When no parts were made, the parts timeline gives a continuous gray TimeLine with the message 'no part where made' . \line \line ")
        'sb.Append("\b Fixe : \b0  If a part does not have a name ... the parts timeline will display 'No Part Number' \line \line")

        'sb.Append("\b New  : \b0  The cycles time are now displayed on the TimeLine \line \line")

        sb.Append("}")
        RTB_liste.Rtf = sb.ToString()




    End Sub
End Class
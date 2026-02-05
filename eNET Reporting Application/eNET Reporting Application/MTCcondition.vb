Imports System.IO
Imports System.Text


Public Class MTCcondition

    Public condition_result As String
    Private Sub MTCcondition_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'If frm_editmtc.Visible = True Then
        '    LBL_title.Text = "Condition with : " & Edit_mtconnect.TV_MTC.SelectedNode.Text
        'Else
        '    LBL_title.Text = "Condition with : " & Edit_Focas.TreeView1.SelectedNode.Text
        'End If
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        CBX_Operator.Items.Add("==")
        CBX_Operator.Items.Add(">=")
        CBX_Operator.Items.Add("<=")
        CBX_Operator.Items.Add(">")
        CBX_Operator.Items.Add("<")
        CBX_Operator.Items.Add("!=")

    End Sub

    Private Sub BTN_Done_Click(sender As Object, e As EventArgs) Handles BTN_Done.Click

        If (CBX_Operator.SelectedIndex >= 0 And TB_Value.Text.Length > 0) Then
            condition_result = CBX_Operator.SelectedItem & checkvalue(TB_Value.Text)
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            MessageBox.Show("Please enter correct values.", "value error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub

    Private Function checkvalue(value As String) As String
        Dim doubletmp As Double
        Dim str_result
        If (Double.TryParse(value, doubletmp)) Then
            str_result = value
        Else
            str_result = """" & value & """"
        End If

        Return str_result
    End Function
End Class
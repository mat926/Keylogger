Public Class advancedemailsettings

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        My.Settings.smtpserver = TextBox1.Text
        My.Settings.smtpport = TextBox2.Text
        If CheckBox1.Checked = True Then
            My.Settings.enablessl = True
        Else
            My.Settings.enablessl = False
        End If
        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub advancedemailsettings_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.smtpserver
        TextBox2.Text = My.Settings.smtpport
        If My.Settings.enablessl = True Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class
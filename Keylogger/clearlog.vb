Public Class clearlog

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = My.Settings.password = True Then
            Me.Close()
            If MsgBox("Are you sure you want to clear the log? This will also delete the log file stored on your computer.", MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                Try

                    If My.Computer.FileSystem.DirectoryExists(My.Settings.directory) = True Then

                        Form1.RichTextBox1.Text = Nothing
                        IO.File.Delete(My.Settings.directory & "\log.txt")
                        MsgBox("Done!", MsgBoxStyle.Information)
                    Else
                        MsgBox("Directory doesn't exist, directory will now be created", MsgBoxStyle.Critical)
                        Form1.RichTextBox1.Text = Nothing
                        My.Computer.FileSystem.CreateDirectory(My.Settings.directory)

                    End If
                Catch ex As Exception
                    MsgBox("Failed to clear the log :( ", MsgBoxStyle.Critical)
                End Try
            End If
        Else
            MsgBox("Invalid Password!", MsgBoxStyle.Critical)
            Me.Close()
        End If


    End Sub

    Private Sub clearlog_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Focus()
        Me.AcceptButton = Button1
    End Sub
End Class
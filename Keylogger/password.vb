Public Class password

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = My.Settings.password = True And TextBox2.Text = TextBox3.Text Then
            Try
                If TextBox2.Text = Nothing And TextBox3.Text = Nothing Then
                    If MsgBox("Warning! Leaving the password blank can let anyone access the program. Are you sure you want to put in a blank password? ", MsgBoxStyle.YesNo) = DialogResult.Yes Then
                        My.Settings.password = Nothing
                        My.Settings.Save()
                        MsgBox("Password has been set.", MsgBoxStyle.Information)
                        Me.Close()

                    Else
                    End If
                Else
                    My.Settings.password = TextBox2.Text
                    My.Settings.Save()
                    MsgBox("Password has been set.", MsgBoxStyle.Information)
                    Me.Close()
                End If
            Catch ex As Exception
                MsgBox("An error has occured while trying to set your password :(", MsgBoxStyle.Critical)
            End Try
         
        Else
           

            If TextBox1.Text = My.Settings.password = False Then
                MsgBox("Old password is incorrect.", MsgBoxStyle.Critical)

            End If
            If TextBox2.Text = TextBox3.Text = False Then
                MsgBox("Passwords do not match", MsgBoxStyle.Critical)
            End If
           
        End If

        If My.Settings.password = Nothing Then
            Form1.Label6.Show()

        Else
            Form1.Label6.Hide()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class
Public Class showkey

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = My.Settings.password = True Then
            
            Form1.Show()
            Form1.WindowState = FormWindowState.Normal
            Me.Close()
            Form1.NotifyIcon1.Visible = False

        Else
            MsgBox("No!", MsgBoxStyle.Critical)
            Me.Close()
        End If
    End Sub

    Private Sub showkey_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
       
    End Sub

    Private Sub showkey_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
       

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
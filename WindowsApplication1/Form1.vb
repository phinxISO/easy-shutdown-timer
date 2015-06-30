Imports Microsoft.Win32
Public Class Form1
    Private mPrevPos As New Point
    Dim start As Boolean = False
    Dim VR As Decimal
    Dim tmlft As Date
    Dim tmmin As Date = "00:00:01"
    Dim tmmin1 As Date


    Private Sub PictureBox1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        'Making the picture box can move form
        Dim delta As New Size(e.X - mPrevPos.X, e.Y - mPrevPos.Y)
        If (e.Button = MouseButtons.Left) Then
            Me.Location += delta
            mPrevPos = e.Location - delta
        Else
            mPrevPos = e.Location
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'timer for the sowftware watch
        Label1.Text = Date.Now.ToString("h:")
        Label3.Text = Date.Now.ToString("ss")
        Label4.Text = Date.Now.ToString("mm")
        Label6.Text = Date.Now.ToString("tt ")
    End Sub

    Private Sub ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ex.Click
        'exit label
        End
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        ' label "-" which i can minimize the software into a Notify start menu Icon with show in start menu
        Me.NotifyIcon1.Visible = True
        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = True

    End Sub
    Private Sub NotifyIcon1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.Click
        ' Notify button which i can maximize the software into a window
        Me.WindowState = FormWindowState.Normal
        Me.NotifyIcon1.Visible = False
        Me.ShowInTaskbar = True
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        ' label "W" which i can minimize the software into a Notify start menu Icon
        Form2.Show()
        Me.NotifyIcon1.Visible = True
        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = False
        Form2.ShowInTaskbar = False
    End Sub

  

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'check REG value for the soft starup
        Try
            If CheckBox1.Checked = False Then

                Dim regKey2 As RegistryKey
                regKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                If regKey2.GetValue("Easy shutdown timer").ToString() = Application.ExecutablePath Then
                    CheckBox1.Checked = True
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        ' REG value for starup the soft by check box
        If CheckBox1.Checked = True Then
            Dim regKey1 As RegistryKey
            regKey1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
            regKey1.SetValue("Easy shutdown timer", Application.ExecutablePath)
        Else
            Dim regKey3 As RegistryKey
            regKey3 = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
            regKey3.DeleteValue("Easy shutdown timer")
        End If
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        'start button stop event
        If start = True Then
            start = False
            PictureBox2.Image = My.Resources.start
            Timer2.Enabled = False
            stoping()
        Else
            'start button start event with check which shutdown timer the user chose
            start = True
            PictureBox2.Image = My.Resources.puse
            starting()
            If RadioButton1.Checked = True Then
                Dim x23 As Date = TextBox1.Text + ":" + TextBox2.Text + ":" + TextBox3.Text + "AM"
                Dim V As Date = Label1.Text + Date.Now.ToString("mm") + ":" + Date.Now.ToString("ss") + "PM "
                AD()
                Timer4.Enabled = True
                Timer2.Enabled = True
            Else
                NBM()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'check the user "tt" choose .. and change it
        If Button2.Text = "PM " Then
            Button2.Text = "AM "
        Else
            Button2.Text = "PM "
        End If
    End Sub

    Private Sub TextBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Click
        'events for helping user to get all textbox when click
        TextBox1.SelectAll()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        'To prevent writing letters inside the text box "only numbers"
        If Char.IsDigit(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        ' To determine the allowed values within textbox
        If TextBox1.Text <> "00" Then
            If Val(TextBox1.Text).ToString = 0 Or Val(TextBox1.Text).ToString > 12 Then
                MsgBox("Hours should be between 1 : 12 !")
                TextBox1.Focus()
                TextBox1.Text = "12"
                TextBox1.SelectAll()
            End If
        End If
        'to get pretty shape of time numbers inside the textbox
        If Len(TextBox1.Text).ToString = 1 Then
            TextBox1.Text = "0" & Val(TextBox1.Text)
        End If
    End Sub
    Private Sub TextBox2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Click
        'events for helping user to get all textbox when click
        TextBox2.SelectAll()
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        'To prevent writing letters inside the text box "only numbers"
        If Char.IsDigit(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        ' To determine the allowed values within textbox
        If Val(TextBox2.Text).ToString >= 60 Then
            MsgBox("Minutes must be less than 60")
            TextBox2.Focus()
            TextBox2.Text = "59"
            TextBox2.SelectAll()
        End If
        'to get pretty shape of time numbers inside the textbox
        If Len(TextBox2.Text).ToString = 1 Then
            TextBox2.Text = "0" & Val(TextBox2.Text)
        End If
    End Sub
    Private Sub Textbox3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.Click
        'events for helping user to get all textbox when click
        TextBox3.SelectAll()
    End Sub

    Private Sub Textbox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        'To prevent writing letters inside the text box "only numbers"
        If Char.IsDigit(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub Textbox3_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.Leave
        ' To determine the allowed values within textbox
        If Val(TextBox3.Text).ToString >= 60 Then
            MsgBox("Seconds must be less than 60")
            TextBox3.Focus()
            TextBox3.Text = "59"
            TextBox3.SelectAll()
        End If
        'to get pretty shape of time numbers inside the textbox
        If Len(TextBox3.Text).ToString = 1 Then
            TextBox3.Text = "0" & Val(TextBox3.Text)
        End If
    End Sub
   
    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        'for enable and disable the soft elements betweet the two timer typs
        stoping()
    End Sub
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        'the "shutdown in this time " timer :- :)
        Dim h As String = Date.Now.ToString("h ")
        Dim s As String = Date.Now.ToString("ss")
        Dim m As String = Date.Now.ToString("mm")
        Dim t As String = Date.Now.ToString("tt ")
        If Val(TextBox1.Text) = Val(h) And Val(TextBox2.Text) = Val(m) And Val(TextBox3.Text) = Val(s) And Button2.Text = t.ToString Then
            PictureBox2.Image = My.Resources.start
            start = False
            Timer2.Enabled = False
            Shell("cmd /c" & "shutdown -s", AppWinStyle.Hide) 'to shutdown
            stoping()
        End If
    End Sub
    Private Sub Textbox4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.Click
        'events for helping user to get all textbox when click
        TextBox4.SelectAll()
    End Sub

    Private Sub Textbox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        'To prevent writing letters inside the text box "only numbers"
        If Char.IsDigit(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
            e.Handled = True
        End If
    End Sub
    Private Sub Textbox4_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.Leave
        ' To determine the allowed values within textbox
        If Val(TextBox4.Text).ToString >= 24 Then
            MsgBox("Must be less than 24")
            TextBox4.Focus()
            TextBox4.Text = "23"
            TextBox5.Text = "59"
            TextBox6.Text = "59"
            TextBox4.SelectAll()
        End If
        'to get pretty shape of time numbers inside the textbox
        If TextBox4.Text = "" Then
            TextBox4.Text = "00"
        End If
        If Len(TextBox4.Text).ToString = 1 Then
            TextBox4.Text = "0" & Val(TextBox4.Text)
        End If
    End Sub
    Private Sub Textbox5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox5.Click
        'events for helping user to get all textbox when click
        TextBox5.SelectAll()
    End Sub

    Private Sub Textbox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        'To prevent writing letters inside the text box "only numbers"
        If Char.IsDigit(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub Textbox5_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox5.Leave
        ' To determine the allowed values within textbox
        If Val(TextBox5.Text).ToString >= 60 Then
            MsgBox("Must be less than 59")
            TextBox5.Focus()
            TextBox5.Text = "59"
            TextBox5.SelectAll()
        End If
        'to get pretty shape of time numbers inside the textbox
        If TextBox5.Text = "" Then
            TextBox5.Text = "00"
        End If
        If Len(TextBox5.Text).ToString = 1 Then
            TextBox5.Text = "0" & Val(TextBox5.Text)
        End If
    End Sub
    Private Sub Textbox6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox6.Click
        'events for helping user to get all textbox when click
        TextBox6.SelectAll()
    End Sub

    Private Sub Textbox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        'To prevent writing letters inside the text box "only numbers"
        If Char.IsDigit(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub Textbox6_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox6.Leave
        ' To determine the allowed values within textbox
        If Val(TextBox5.Text).ToString >= 60 Then
            MsgBox("Must be less than 59")
            TextBox6.Focus()
            TextBox6.Text = "59"
            TextBox6.SelectAll()
        End If
        'to get pretty shape of time numbers inside the textbox
        If TextBox6.Text = "" Then
            TextBox6.Text = "00"
        End If
        If Len(TextBox6.Text).ToString = 1 Then
            TextBox6.Text = "0" & Val(TextBox6.Text)
        End If
    End Sub

    Protected Sub NBM()
        ' the "shutdown after this time " stopwatch its better than Timer element "After several experiments"
        ' with MS i got very accurate calculation of stopwatch start & stop
        tmmin1 = TextBox4.Text + ":" + TextBox5.Text + ":" + TextBox6.Text
        Timer4.Enabled = True
        VR = ((Val(TextBox4.Text) * 60 * 60) + (Val(TextBox5.Text) * 60) + (Val(TextBox6.Text))) * 1024
        Try
            B1.Maximum = (VR / 1024) - 1
        Catch
        End Try
        Timer3.Enabled = True
        Dim stopW As New Stopwatch
        stopW.Start()
        Do While stopW.ElapsedMilliseconds < VR
            Application.DoEvents()

            If start = False Then
                GoTo 1
            End If
        Loop
        Shell("cmd /c" & "shutdown -s", AppWinStyle.Hide) 'to shutdown
        PictureBox2.Image = My.Resources.start
        start = False
        stoping()
1:
        Timer3.Enabled = False
        stopW.Stop()
        stopW.Reset()
    End Sub
    Protected Sub starting()
        ' for disable some elements in the soft when the timers fet start
        RadioButton1.Enabled = False
        RadioButton2.Enabled = False
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        Button2.Enabled = False
    End Sub
    Protected Sub stoping()
        'this is for check the soft elemets and get enable elements and disable others in right way after stopping the timers "form any type"
        Timer2.Enabled = False
        B1.Value = 0
        'get the time left label(8) postion after stopping the timers
        Label8.Text = "0:0:0"
        Timer2.Enabled = False
        If RadioButton2.Checked = True Then
            TextBox4.Enabled = True
            TextBox5.Enabled = True
            TextBox6.Enabled = True
            TextBox1.Enabled = False
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            Button2.Enabled = False
            RadioButton1.Enabled = True
            RadioButton2.Enabled = True
        Else
            TextBox4.Enabled = False
            TextBox5.Enabled = False
            TextBox6.Enabled = False
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            Button2.Enabled = True
            RadioButton1.Enabled = True
            RadioButton2.Enabled = True
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        'progress bar timer value controler
        Try
            B1.Value += 1
        Catch
            Timer3.Enabled = False
        End Try
    End Sub

    Protected Sub AD()
        'time left timer which get very Accurate calculation
        Dim x23 As Date = TextBox1.Text + ":" + TextBox2.Text + ":" + TextBox3.Text + Button2.Text
        Dim V As Date = Date.Now.ToString(" h").Remove(0, 1) + ":" + Date.Now.ToString("mm") + ":" + Date.Now.ToString("ss") + Date.Now.ToString("tt")
        Dim z As Date = "12:00:00"
        Try
            tmlft = (x23.TimeOfDay - V.TimeOfDay).ToString
        Catch
            tmlft = (x23.TimeOfDay - V.TimeOfDay + z.TimeOfDay + z.TimeOfDay).ToString
        End Try
        Label8.Text = tmlft.TimeOfDay.ToString
        tmmin1 = Label8.Text
        B1.Maximum = ((Val(tmlft.Hour.ToString) * 60 * 60) + (Val(tmlft.Minute.ToString) * 60) + (Val(tmlft.Second.ToString))) - 2
        Timer3.Enabled = True
    End Sub

    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        'its the time left timer controler
        Try
            tmmin1 = (tmmin1.TimeOfDay - tmmin.TimeOfDay).ToString
            Label8.Text = tmmin1.Hour.ToString + ":" + tmmin1.Minute.ToString + ":" + tmmin1.Second.ToString
        Catch
            Timer4.Enabled = False
        End Try
        If start = False Then
            Timer4.Enabled = False
        End If
    End Sub
    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ' To make the program more tact ... not duplicate forms on the same screen
        Form2.Hide()
    End Sub
End Class

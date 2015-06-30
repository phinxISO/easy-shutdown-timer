Public Class Form2
    Private mPrevPos As New Point
    Dim b As Boolean
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Keep pace with updates with form 1 timers start and stop
        Label1.Text = Date.Now.ToString("h:")
        Label3.Text = Date.Now.ToString("ss ")
        Label4.Text = Date.Now.ToString("mm ")
        B2.Maximum = Form1.B1.Maximum
        B2.Value = Form1.B1.Value
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'get the form1 design
        Me.BackgroundImage = Form1.BackgroundImage
        PictureBox1.BackgroundImage = Form1.BackgroundImage
    End Sub
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        ' set the form position
        Dim scr = Screen.FromPoint(Me.Location)
        Me.Location = New Point(scr.WorkingArea.Right - Me.Width, scr.WorkingArea.Top)
        MyBase.OnLoad(e)
        Me.Location = New Point(Me.Location.X + 200, 14)
    End Sub

    Private Sub B2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B2.Click
        ' the form movment
        If b = True Then
            b = False
            Me.Location = New Point(Me.Location.X + 200, Me.Location.Y)
        Else
            b = True
            Me.Location = New Point(Me.Location.X - 200, Me.Location.Y)
        End If
    End Sub

    Private Sub ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ex.Click
        ' hide the from when the user chose
        Me.Hide()
        Me.WindowState = FormWindowState.Normal
        Form1.ShowInTaskbar = True
        Form1.Show()
    End Sub

    Private Sub B2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles B2.MouseMove
        ' form Move vertically only
        Dim delta As New Size("0", e.Y - mPrevPos.Y)
        If (e.Button = MouseButtons.Left) Then
            Me.Location += delta
            mPrevPos = e.Location - delta
        Else
            mPrevPos = e.Location
        End If
    End Sub
End Class
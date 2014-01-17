Public Class frmMain

    Private Sub frmMain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If My.Application.LoginVal Then
            If DialogResult.Yes = MessageBox.Show("The Application is about to close would you like to save? ", My.Application.Info.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        End If
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' set to maximized so the program wont be put in the background out of focus
        Me.WindowState = FormWindowState.Maximized
        Me.Text = Application.ProductName & " " & Application.ProductVersion

        mainTimer.Enabled = True
        mainTimer.Start()

        Me.mainAppStatus.Text = ""

        If Not My.Application.LoginVal Then
            mainLogin.ShowDialog()
        End If


        ' when every thing is loaded we can perform a status check and display an appropriate text
        Me.mainAppStatus.Text = "Ready ..."

    End Sub


    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub



    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        mainAbout.ShowDialog()
    End Sub



    Private Sub mainTimer_Tick(sender As Object, e As EventArgs) Handles mainTimer.Tick
        mainAppStatus2.Text = DateTime.Now.ToLongTimeString
    End Sub

  
    Private Sub mainAppStatus2_Click(sender As Object, e As EventArgs) Handles mainAppStatus2.Click
        mainAppStatus3.Visible = False
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        mainOptions.ShowDialog()
    End Sub
End Class

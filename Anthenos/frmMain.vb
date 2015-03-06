Imports System.Net.NetworkInformation
'Imports Microsoft.VisualBasic.Devices
Public Class frmMain

    Private Sub frmMain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ' check current time
        If (My.Application.LoginVal = True) Then
            My.Application.cur_t = Now
            My.Application.elasped = My.Application.cur_t.Subtract(My.Application.start_t)
            ' has the session expired
            If (My.Application.elasped.TotalSeconds >= Convert.ToDouble(30)) Then
                My.Application.LoginVal = False
                mainAppStatus3.Text = "Session expired"
                MsgBox("Session time has elasped", MsgBoxStyle.Information, "Session has expired")
                mainLogin.ShowDialog()
            End If
        End If
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If My.Application.LoginVal Then
            ' ask user if they want to save
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

        ' start the clock timer
        mainTimer.Enabled = True
        mainTimer.Start()

        Me.mainAppStatus.Text = ""

        ' is the user logged in
        If Not My.Application.LoginVal Then
            mainLogin.ShowDialog()
        Else

            ' getting connected to the server
            If NetworkInterface.GetIsNetworkAvailable() Then
                Try
                    ' setting status text
                    Me.mainAppStatus.Text = "Attempting to Connect"
                    ' ping server
                    My.Computer.Network.Ping("andrebonner.com", 5000)
                Catch ex As PingException
                    ' exception caught and thrown
                    MsgBox("The application could not connect to the server at this time.", MsgBoxStyle.Exclamation, "Server Error")
                    ' show details for debug purposes
                    Console.Write(ex.Message)
                End Try
            Else
                ' network adapter cant be found.
                MsgBox("The network is not available", MsgBoxStyle.Exclamation, "No Network")
                Application.Exit()
            End If

            ' when every thing is loaded we can perform a status check and display an appropriate text
            Me.mainAppStatus.Text = "Ready ..."
        End If
    End Sub


    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        ' shutdown application
        Application.Exit()
    End Sub



    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        ' display the About Dialog
        mainAbout.ShowDialog()
    End Sub



    Private Sub mainTimer_Tick(sender As Object, e As EventArgs) Handles mainTimer.Tick
        ' timer tick updates the status label
        mainAppStatus2.Text = DateTime.Now.ToLongTimeString

    End Sub


    Private Sub mainAppStatus2_Click(sender As Object, e As EventArgs) Handles mainAppStatus2.Click
        ' mainAppStatus3.Visible = False
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        mainOptions.ShowDialog()
    End Sub

    Private Sub SignOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SignOutToolStripMenuItem.Click
        ' end the user session
        My.Application.LoginVal = False
        mainAppStatus3.Text = "Signed out"
        mainLogin.ShowDialog()
    End Sub
End Class

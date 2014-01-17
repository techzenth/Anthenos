Public Class mainLogin

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Public loginAttempts As Integer
    Public mydb As New My.MyDatabase

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        ' is the user already logged in
        If My.Application.LoginVal Then
            Me.Close()
        Else
            ' check login credentials
            If checkLogin() Then
                MessageBox.Show("Welcome " & UsernameTextBox.Text & " ", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                loginAttempts = loginAttempts + 1
                If loginAttempts >= 3 Then
                    MessageBox.Show("Number of login attempts exceed allowed","Login",MessageBoxButtons.OK,MessageBoxIcon.Warning)
                    Application.Exit()
                Else
                    frmMain.mainAppStatus.Text = "Login attempt " & loginAttempts.ToString
                    MessageBox.Show("Login could not be verified", "Login UnSuccessful", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.PasswordTextBox.Clear()
                    Me.UsernameTextBox.SelectAll()
                End If
            End If
        End If

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Application.Exit()
    End Sub

    Private Sub mainLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Me.UsernameTextBox.Focus()
    End Sub

    Public Function checkLogin() As Boolean
        If Me.UsernameTextBox.Text.Length <= 0 Or Me.PasswordTextBox.Text.Length <= 0 Then
            My.Application.LoginVal = False
            Return My.Application.LoginVal
        Else
            ' check username and password against database
            '1 open connection to database

            mydb.openDb()

            '2 query database table for username
            Dim dset As DataSet
            dset = mydb.query("SELECT id, password FROM users WHERE username='" & UsernameTextBox.Text.Trim & "' ")

            '3 is the password matching anything in the database
            Dim dTable As DataTable = dset.Tables(0)
            If dTable.Rows.Count > 0 Then
                If PasswordTextBox.Text <> dTable.Rows(0).Item(1) Then
                    'MessageBox.Show("The username and password combination is incorrect", "Login Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    My.Application.LoginVal = False
                    Return My.Application.LoginVal
                Else
                    Dim roleId = checkRole(dTable, dset)
                    mydb.closeDb()

                    frmMain.mainAppStatus3.Text = " Logged in as " & roleId
                    My.Application.LoginVal = True
                    Return My.Application.LoginVal
                End If
            End If
        End If
        My.Application.LoginVal = False
        Return My.Application.LoginVal
    End Function

    Function checkRole(ByVal dTable As DataTable, ByVal dset As DataSet)
        Dim dset2 As DataSet
        dset2 = mydb.query("SELECT roleID FROM user_role WHERE userID=" & dTable.Rows(0).Item(0) & " ")

        Dim dtable2 As DataTable = dset2.Tables(0)
        If dtable2.Rows.Count > 0 Then
            dset = mydb.query("SELECT role FROM roles WHERE ID=" & dtable2.Rows(0).Item(0) & " ")
            dTable = dset.Tables(0)

            'MessageBox.Show("Role : " & dTable.Rows(0).Item(0), "Login continue", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Return dTable.Rows(0).Item(0)
    End Function
End Class


Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient



Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        ' this boolean indicates a logged in user
        Public LoginVal As Boolean = False
        ' start and current time values
        Public start_t, cur_t As DateTime
        ' session duration
        Public elasped As TimeSpan

        Private Sub MyApplication_NetworkAvailabilityChanged(sender As Object, e As Devices.NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
            Console.Write("Network Availabilty Change")
        End Sub
    End Class

    Public Class MyDatabase
        Private con As OleDbConnection = New OleDbConnection()
        Public Sub openDb()
            If con.State = ConnectionState.Closed Then
                Try
                    con.ConnectionString = Anthenos.My.MySettings.Default.anthenosConnectionString
                    con.Open()
                Catch e As Exception
                    MsgBox(e.Message, MsgBoxStyle.Critical, "Athenos Error!")

                End Try
            End If
        End Sub

        Public Function query(ByVal sql As String) As DataSet
            Dim dset As New DataSet
            Dim myadapter As New OleDbDataAdapter(sql, con)
            myadapter.Fill(dset)
            myadapter.Dispose()

            Return dset
        End Function
        Public Sub closeDb()
            con.Close()
        End Sub
    End Class

    'Public Class Profile
    '    Private uname As String
    '    Private pwd As String

    '    Public Function getUserName() As String
    '        Return uname
    '    End Function
    '    Public Function getPassword() As String
    '        Return pwd
    '    End Function

    '    Public Sub setUsername(u As String)
    '        uname = u
    '    End Sub

    '    Public Sub setPassword(p As String)
    '        pwd = p
    '    End Sub


    'End Class


End Namespace


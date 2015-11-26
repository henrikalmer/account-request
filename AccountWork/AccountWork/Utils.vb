Imports System.DirectoryServices.AccountManagement

Public Class Utils
    Public Shared Function GetUserName() As String
        Dim User = UserPrincipal.Current
        Return User.SamAccountName
    End Function

    Public Shared Function GetUserFullName() As String
        Dim User = UserPrincipal.Current
        Return User.GivenName & " " & User.Surname
    End Function

    Public Shared Function GetUserEmail() As String
        Dim User = UserPrincipal.Current
        Return User.EmailAddress
    End Function

    Public Shared Function GetUserPhoneNo() As String
        Dim User = UserPrincipal.Current
        Return User.VoiceTelephoneNumber
    End Function
End Class

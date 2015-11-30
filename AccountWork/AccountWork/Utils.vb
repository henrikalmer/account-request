Imports System.DirectoryServices.AccountManagement
Imports System.Text

Public Class Utils
    Public Shared Function GetUserName() As String
        Return UserPrincipal.Current.SamAccountName
    End Function

    Public Shared Function GetUserFullName() As String
        Dim User = UserPrincipal.Current
        Return User.GivenName & " " & User.Surname
    End Function

    Public Shared Function GetUserEmail() As String
        Return UserPrincipal.Current.EmailAddress
    End Function

    Public Shared Function GetUserPhoneNo() As String
        Return UserPrincipal.Current.VoiceTelephoneNumber
    End Function

    Public Shared Function GetUserCity() As String
        Dim SysInfo = CreateObject("ADSystemInfo")
        Dim City = ""
        Try
            Dim AdUser = GetObject("LDAP://" & SysInfo.UserName)
            City = AdUser.l
        Catch ex As Exception
            City = "okand"
        End Try
        Return City
    End Function

    Public Shared Function RemoveAccentMarks(S As String) As String
        Dim NormalizedString As String = S.Normalize(NormalizationForm.FormD)
        Dim SB As New StringBuilder()
        Dim c As Char
        For i = 0 To NormalizedString.Length - 1
            c = NormalizedString(i)
            If Globalization.CharUnicodeInfo.GetUnicodeCategory(c) <> Globalization.UnicodeCategory.NonSpacingMark Then
                SB.Append(c)
            End If
        Next
        Return SB.ToString
    End Function

    Public Shared Function GetUserRegEmail() As String
        Dim City = GetUserCity()
        Return "registrator." & LCase(RemoveAccentMarks(City)) & "@ekobrottsmyndigheten.se"
    End Function
End Class

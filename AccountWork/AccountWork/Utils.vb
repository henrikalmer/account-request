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
        Dim AdUser = GetObject("LDAP://" & SysInfo.UserName)
        Return AdUser.l
    End Function

    Public Shared Function RemoveAccentMarks(S As String) As String
        Dim NormalizedString As String = S.Normalize(NormalizationForm.FormD)
        Dim SB As New StringBuilder()
        Dim c As Char
        For i = 0 To NormalizedString.Length - 1
            c = NormalizedString(i)
            If Globalization.CharUnicodeInfo.GetUnicodeCategory(c) = Globalization.UnicodeCategory.NonSpacingMark Then
                SB.Append(c)
            End If
        Next
        Return SB.ToString
    End Function
End Class

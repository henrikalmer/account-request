Public Class Utils
    Public Shared Function GetActiveDirectoryUserName() As String
        Return Security.Principal.WindowsIdentity.GetCurrent.Name.Replace("\", "/")
    End Function

    Public Shared Function GetActiveDirectoryEmail() As String
        Dim DomainUser As String = Security.Principal.WindowsIdentity.GetCurrent.Name.Replace("\", "/")
        Dim AdEntry As New DirectoryServices.DirectoryEntry("WinNT://" & DomainUser)
        Return AdEntry.Properties("Email").Value
    End Function
End Class

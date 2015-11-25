Public Class Utils
    Public Shared Function GetActiveDirectoryUserName() As String
        Dim DomainUser As String = Security.Principal.WindowsIdentity.GetCurrent.Name.Replace("\", "/")
        Dim AdEntry As New DirectoryServices.DirectoryEntry("WinNT://" & DomainUser)
        Return AdEntry.Name
    End Function

    Public Shared Function GetActiveDirectoryFullName() As String
        Dim DomainUser As String = Security.Principal.WindowsIdentity.GetCurrent.Name.Replace("\", "/")
        Dim AdEntry As New DirectoryServices.DirectoryEntry("WinNT://" & DomainUser)
        Return AdEntry.Properties("FullName").Value
    End Function

    Public Shared Function GetActiveDirectoryEmail() As String
        Dim DomainUser As String = Security.Principal.WindowsIdentity.GetCurrent.Name.Replace("\", "/")
        Dim AdEntry As New DirectoryServices.DirectoryEntry("WinNT://" & DomainUser)
        Return AdEntry.Properties("Mail").Value
    End Function
End Class

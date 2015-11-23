Public Class Utils
    Public Shared Function GetActiveDirectoryUserName() As String
        Dim DomainUser As String = Security.Principal.WindowsIdentity.GetCurrent.Name.Replace("\", "/")
        Dim AdEntry As New DirectoryServices.DirectoryEntry("WinNT://" & DomainUser)
        Return AdEntry.Properties("FullName").Value
    End Function
End Class

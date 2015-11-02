Namespace Domain
    Public Class RequestInfo
        Public Property EbNumber As String
        Public Property RequestId As Int32
        Public Property BankName As String
        Public Property TypeOfRequest As String
        Public Property Timestamp As Date
        Public Property SerializedRequest As String
        Public Property Comment As String
        Public Property rObj As RequestObject

        Public Sub New()
            If (SerializedRequest IsNot Nothing) Then
                rObj = New RequestObject(SerializedRequest, "xml")
            End If
        End Sub
    End Class
End Namespace
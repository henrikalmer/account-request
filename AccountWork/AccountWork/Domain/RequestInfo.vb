Namespace Domain
    Public Class RequestInfo
        Public Property EbNumber As String
        Public Property Prosecutor As String
        Public Property RequestId As Integer
        Public Property BankName As String
        Public Property TypeId As Integer
        Public Property TypeString As String
        Public Property Timestamp As Date
        Public Property SerializedRequest As String
        Public Property Comment As String

        Private Property rObj As RequestObject
        Public ReadOnly Property Parameters As String
            Get
                If (rObj Is Nothing) Then
                    If (SerializedRequest Is Nothing) Then
                        Return String.Empty
                    End If
                    rObj = New RequestObject(SerializedRequest, "json")
                End If
                Dim response = ""
                If (rObj.IdNumber IsNot Nothing) Then
                    response &= "Personnr/Orgnr: " & rObj.IdNumber
                ElseIf (rObj.AccountNumber IsNot Nothing)
                    response &= "Kontonr: " & rObj.AccountNumber
                Else
                    response = "Tom fråga"
                End If
                response &= vbNewLine & "Från " & rObj.PeriodStartDate.ToString("d")
                response &= vbNewLine & "Till " & rObj.PeriodEndDate.ToString("d")
                Return response
            End Get
        End Property
    End Class
End Namespace
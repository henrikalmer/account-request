Namespace Domain
    Public Class Request
        Private _Id As Int32
        Public Property UserId As String
        Public Property BankId As Int32
        Public Property Timestamp As Date
        Public Property TypeOfRequest As String


        Public Property Id() As Int32
            Get
                Return _Id
            End Get
            Protected Set(value As Int32)
                _Id = value
            End Set
        End Property
    End Class
End Namespace

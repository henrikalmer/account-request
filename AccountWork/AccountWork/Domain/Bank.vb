Namespace Domain
    Public Class Bank
        Private _Id As Int32
        Public Property Name As String
        Public Property ClearingNumber As Int32
        Public Property ClearingNumberIntervalStart As Int32
        Public Property ClearingNumberIntervalEnd As Int32
        Public Property Email As String
        Public Property sPhoneNo As String
        Public Property sFaxNo As String
        Public Property sAdress As String



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

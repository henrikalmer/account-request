Imports System.ComponentModel.DataAnnotations.Schema

Namespace Domain
    Public Class Bank
        Private _Id As Int32
        Public Property Name As String
        Public Property ClearingNumberIntervalStart As Integer
        Public Property ClearingNumberIntervalEnd As Integer?
        Public Property Email As String

        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
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

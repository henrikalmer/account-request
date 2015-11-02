Imports System.ComponentModel.DataAnnotations.Schema

Namespace Domain
    Public Class Request
        Private _Id As Int32
        Public Property EbNumber As String
        Public Property UserId As String
        Public Property BankId As Int32
        <ForeignKey("BankId")>
        Public Property Bank As ClearingNumber
        Public Property Timestamp As Date
        Public Property TypeOfRequest As String
        Public Property SerializedRequest As String
        Public Property Comment As String

        Public Property Id() As Int32
            Get
                Return _Id
            End Get
            Protected Set(value As Int32)
                _Id = value
            End Set
        End Property

        Public Sub New()
        End Sub
        Public Sub New(EbNo As String, B As ClearingNumber, rType As String, IdNumber As String, AccountNumber As String, PeriodStartDate As Date, PeriodEndDate As Date)
            EbNumber = EbNo
            UserId = "TestUser"
            Bank = B
            TypeOfRequest = rType
            Dim rObj = New RequestObject()
            rObj.TypeOfRequest = rType
            rObj.IdNumber = IdNumber
            rObj.AccountNumber = AccountNumber
            rObj.PeriodStartDate = ToDateString(PeriodStartDate)
            rObj.PeriodEndDate = ToDateString(PeriodEndDate)
            SerializedRequest = rObj.ToJson()
            Timestamp = Now
        End Sub

        Private Function ToDateString(ts As Date) As String
            Return ts.ToString()
        End Function
    End Class
End Namespace

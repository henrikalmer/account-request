Imports System.ComponentModel.DataAnnotations.Schema

Namespace Domain
    Public Class Request
        Private _Id As Integer
        Public Property EbNumber As String
        Public Property Prosecutor As String
        Public Property UserId As String
        Public Property BankId As Integer
        <ForeignKey("BankId")>
        Public Property Bank As ClearingNumber
        Public Property Timestamp As Date
        Public Property TypeOfRequest As String
        Public Property SerializedRequest As String
        Public Property Comment As String

        Public Property Id() As Integer
            Get
                Return _Id
            End Get
            Protected Set(value As Integer)
                _Id = value
            End Set
        End Property

        Public Sub New()
        End Sub
        Public Sub New(EbNo As String, P As String, B As ClearingNumber, rType As String, IdNumber As String, AccountNumber As String, PeriodStartDate As Date, PeriodEndDate As Date)
            EbNumber = EbNo
            Prosecutor = P
            UserId = Utils.GetActiveDirectoryUserName()
            Bank = B
            TypeOfRequest = rType
            Dim rObj = New RequestObject()
            rObj.EbNumber = EbNumber
            rObj.Prosecutor = Prosecutor
            rObj.TypeOfRequest = rType
            rObj.IdNumber = IdNumber
            rObj.AccountNumber = AccountNumber
            rObj.PeriodStartDate = PeriodStartDate
            rObj.PeriodEndDate = PeriodEndDate
            SerializedRequest = rObj.ToJson()
            Timestamp = Now
        End Sub
    End Class
End Namespace

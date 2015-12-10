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
        Public Property TypeId As Integer
        Public Property TypeString As String
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
        Public Sub New(EbNo As String, P As String, SecrecyDate As Date, B As ClearingNumber, tId As Integer, tString As String, IdNumber As String, AccountNumber As String, PeriodStartDate As Date, PeriodEndDate As Date, IncludeStatements As Boolean)
            EbNumber = EbNo
            Prosecutor = P
            UserId = Utils.GetUserName()
            Dim UserEmail = LCase(Utils.GetUserEmail())
            Bank = B
            TypeId = tId
            TypeString = tString
            Dim rObj = New RequestObject()
            rObj.EbNumber = EbNumber
            rObj.Prosecutor = Prosecutor
            rObj.SecrecyDate = SecrecyDate
            rObj.Contact = UserEmail
            rObj.TypeId = TypeId
            rObj.TypeString = TypeString
            rObj.IdNumber = IdNumber
            rObj.AccountNumber = AccountNumber
            rObj.PeriodStartDate = PeriodStartDate
            rObj.PeriodEndDate = PeriodEndDate
            rObj.IncludeStatements = IncludeStatements
            SerializedRequest = rObj.ToJson()
            Timestamp = Now
        End Sub
    End Class
End Namespace

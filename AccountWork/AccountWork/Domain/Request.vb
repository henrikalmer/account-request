Namespace Domain
    Public Class Request
        Private _Id As Int32
        Public Property UserId As String
        Public Property BankId As Int32
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
        Public Sub New(bId As Int32, rType As String, IdNumber As String, AccountNumber As String, PeriodStartDate As Date, PeriodEndDate As Date)
            UserId = "TestUser"
            BankId = bId
            TypeOfRequest = rType
            Dim rObj = New RequestObject()
            rObj.TypeOfRequest = rType
            rObj.IdNumber = IdNumber
            rObj.AccountNumber = AccountNumber
            rObj.PeriodStartDate = ToDateString(PeriodStartDate)
            rObj.PeriodEndDate = ToDateString(PeriodEndDate)
            SerializedRequest = rObj.ToXml()
            Timestamp = Now.ToUniversalTime()
        End Sub

        Private Function ToDateString(ts As Date) As String
            Return ts.ToUniversalTime().ToString()
        End Function
    End Class
End Namespace

Imports MediatorLib
Imports AccountWork.Domain

Public Class MainWindowViewModel
    Inherits BaseViewModel

    Public Property Control As MainWindow
    Public Property CurrentCase As New EbCaseViewModel()
    Public ReadOnly Property EngagementFormIsValid As Boolean
        Get
            Return CurrentCase.IsValid And Control.engagementForm.ErrorMessage = String.Empty
        End Get
    End Property
    Public ReadOnly Property AccountFormIsValid As Boolean
        Get
            Return CurrentCase.IsValid And Control.accountHolderForm.ErrorMessage = String.Empty
        End Get
    End Property
    Public ReadOnly Property TransactionSmallFormIsValid As Boolean
        Get
            Return CurrentCase.IsValid And Control.transactionSmallForm.ErrorMessage = String.Empty
        End Get
    End Property
    Public ReadOnly Property TransactionMediumFormIsValid As Boolean
        Get
            Return CurrentCase.IsValid And Control.transactionMediumForm.ErrorMessage = String.Empty
        End Get
    End Property

    Public ReadOnly Property AllBanks As List(Of ClearingNumber)
        Get
            Return Db.AllBanksWithEmail
        End Get
    End Property

    Public Sub New(ctrl As MainWindow)
        ' Register all decorated methods to the Mediator
        Control = ctrl
        VMMediator.Register(Me)
    End Sub

    <MediatorMessageSink(MediatorMessages.FormValidationStatusChanged, ParameterType:=GetType(Message))>
    Public Sub ListenForValidationChanges(m As Message)
        OnPropertyChanged("EngagementFormIsValid")
        OnPropertyChanged("AccountFormIsValid")
        OnPropertyChanged("TransactionSmallFormIsValid")
        OnPropertyChanged("TransactionMediumFormIsValid")
    End Sub

    Public Function CreateRequest(TypeId As Integer, TypeString As String, SecrecyDate As Date, Bank As ClearingNumber, Pnr As String, AccNr As String, StartDate As Date, EndDate As Date, IncludeStatements As Boolean) As Request
        Dim EbNo = CurrentCase.EbNumber
        Dim P = CurrentCase.Prosecutor
        Dim Req As New Request(EbNo, P, SecrecyDate, Bank, TypeId, TypeString, Pnr, AccNr, StartDate, EndDate, IncludeStatements)
        Db.Requests.Add(Req)
        Db.SaveChanges()
        ' Update request id in child request object
        Dim ReqObj As New RequestObject(Req.SerializedRequest, "json")
        ReqObj.RequestId = Req.Id
        Req.SerializedRequest = ReqObj.ToJson()
        Db.SaveChanges()
        VMMediator.NotifyColleagues(MediatorMessages.RequestAdded,
                                    New Message("User created a new Request."))
        Return Req
    End Function
End Class
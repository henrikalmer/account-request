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
    Public ReadOnly Property TransactionFormIsValid As Boolean
        Get
            Return CurrentCase.IsValid And Control.transactionForm.ErrorMessage = String.Empty
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
        OnPropertyChanged("TransactionFormIsValid")
    End Sub

    Public Sub CreateRequest()
        Using Db = New AccountWorkDbContext()
            Dim EbNo = CurrentCase.EbNumber
            Dim P = CurrentCase.Prosecutor
            Dim Bank = (From X In Db.ClearingNumbers Select X Where X.Id = 98).SingleOrDefault()
            Db.Requests.Add(New Request(EbNo, P, Bank, "1. Engagemangsförfrågan", "198407120172", Nothing, Today, Today))
            Db.SaveChanges()
            VMMediator.NotifyColleagues(MediatorMessages.RequestAdded,
                                        New Message("User created a new Request."))
        End Using
    End Sub
End Class
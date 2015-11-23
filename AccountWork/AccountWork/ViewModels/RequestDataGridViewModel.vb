Imports System.Collections.ObjectModel
Imports AccountWork.Domain
Imports MediatorLib

Public Class RequestDataGridViewModel
    Inherits BaseViewModel

    Public ReadOnly Property Requests As ObservableCollection(Of RequestInfo)
        Get
            Dim UserId = Utils.GetActiveDirectoryUserName()
            Dim Reqs = From R In Db.Requests
                       Where R.UserId = UserId
                       Select New RequestInfo With {
                           .EbNumber = R.EbNumber,
                           .Prosecutor = R.Prosecutor,
                           .RequestId = R.Id,
                           .BankName = R.Bank.Name,
                           .TypeOfRequest = R.TypeOfRequest,
                           .Timestamp = R.Timestamp,
                           .SerializedRequest = R.SerializedRequest,
                           .Comment = R.Comment
                       }
            Return New ObservableCollection(Of RequestInfo)(Reqs.ToList())
        End Get
    End Property

    Public Sub New()
        VMMediator.Register(Me)
    End Sub

    <MediatorMessageSink(MediatorMessages.RequestAdded, ParameterType:=GetType(Message))>
    Public Sub ListenForDbUpdates(m As Message)
        OnPropertyChanged("Requests")
    End Sub
End Class

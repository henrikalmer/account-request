Imports System.Collections.ObjectModel
Imports AccountWork.Domain

Public Class RequestDataGridViewModel
    Inherits BaseViewModel

    Public Property Requests As ObservableCollection(Of RequestInfo)

    Public Sub New()
        Dim reqs = From R In Db.Requests
                   Select New RequestInfo With {
                       .EbNumber = R.EbNumber,
                       .RequestId = R.Id,
                       .BankName = R.Bank.Name,
                       .TypeOfRequest = R.TypeOfRequest,
                       .Timestamp = R.Timestamp,
                       .SerializedRequest = R.SerializedRequest,
                       .Comment = R.Comment
                   }
        Requests = New ObservableCollection(Of RequestInfo)(reqs.ToList())
    End Sub
End Class

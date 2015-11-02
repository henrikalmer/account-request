Imports System.Collections.ObjectModel
Imports AccountWork.Domain

Public Class RequestDataGridViewModel
    Inherits BaseViewModel

    Public Property Requests As ObservableCollection(Of Request)

    Public Sub New()
        Dim reqs = From request In Db.Requests
                   Join bank In Db.ClearingNumbers
                       On request.BankId Equals bank.Id
                   Order By request.Timestamp Descending
                   Select request

        Requests = New ObservableCollection(Of Request)(reqs.ToList())
        Dim test = ""
    End Sub
End Class

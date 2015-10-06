Imports System.Collections.ObjectModel
Imports AccountWork.Domain

Public Class BankDataGridViewModel
    Inherits BaseViewModel

    Public Property ClearingNumbers As ObservableCollection(Of ClearingNumber)

    Public Property HasChanges() As Boolean
        Get
            Return Db.ChangeTracker.HasChanges()
        End Get
        Set(value As Boolean)
            OnPropertyChanged("HasChanges")
        End Set
    End Property

    Public Sub New()
        ClearingNumbers = New ObservableCollection(Of ClearingNumber)(Db.ClearingNumbers.ToList())
    End Sub

    Public Sub SaveChanges()
        Db.SaveChanges()
        OnPropertyChanged("HasChanges")
    End Sub
End Class

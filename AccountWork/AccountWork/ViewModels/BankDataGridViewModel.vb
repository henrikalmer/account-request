Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data.Entity
Imports AccountWork.Domain

Public Class BankDataGridViewModel
    Implements INotifyPropertyChanged
    Implements IDisposable

    Protected Db As New AccountWorkDbContext()

    Public Property ClearingNumbers As ObservableCollection(Of ClearingNumber)

    Public Property HasChanges() As Boolean
        Get
            'Db.ChangeTracker.DetectChanges()
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

#Region "INotifyPropertyChanged"
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
#End Region

#Region "IDisposable"
    Public Sub Dispose() Implements IDisposable.Dispose
        Db.Dispose()
    End Sub
#End Region
End Class

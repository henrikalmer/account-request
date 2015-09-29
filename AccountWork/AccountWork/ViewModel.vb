Imports System.ComponentModel
Imports AccountWork.Domain

Public Class ViewModel
    Implements INotifyPropertyChanged

    Public Property BankNames As List(Of String)

    Public Sub New()
        Using Db = New AccountWorkDbContext()
            BankNames = Db.Banks.Select(Function(x) x.Name).Distinct().ToList()
            BankNames.Sort()
        End Using
    End Sub

#Region "INotifyPropertyChanged"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub

#End Region

End Class

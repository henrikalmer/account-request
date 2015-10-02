Imports System.ComponentModel
Imports AccountWork.Domain

Public Class ViewModel
    Implements INotifyPropertyChanged
    Implements IDataErrorInfo

    Public Property BankNames As List(Of String)
    Public Property CurrentCase As EbCase

    Public Sub New()
        CurrentCase = New EbCase()
        Using Db = New AccountWorkDbContext()
            BankNames = Db.ClearingNumbers.Select(Function(x) x.Name).Distinct().ToList()
            BankNames.Sort()
        End Using
    End Sub

#Region "IDataErrorInfo"

    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            Dim [error] As String = TryCast(CurrentCase, IDataErrorInfo)(columnName)
            CommandManager.InvalidateRequerySuggested()
            Return [error]
        End Get
    End Property

    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Return TryCast(CurrentCase, IDataErrorInfo).[Error]
        End Get
    End Property

#End Region

#Region "INotifyPropertyChanged"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub

#End Region

End Class

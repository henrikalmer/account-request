Imports System.ComponentModel
Imports AccountWork.Domain

Public Class ViewModel
    Implements INotifyPropertyChanged
    Implements IDataErrorInfo

    Public Property BankNames As List(Of String)
    Public Property CurrentCase As EbCase
    Public Property Errors As New Dictionary(Of String, String)

    Public Sub New()
        CurrentCase = New EbCase()
        Using Db = New AccountWorkDbContext()
            BankNames = Db.ClearingNumbers.Select(Function(x) x.Name).Distinct().ToList()
            BankNames.Sort()
        End Using
    End Sub

    Public ReadOnly Property IsValid As Boolean
        Get
            Return Errors.Count = 0 And CurrentCase.IsValid
        End Get
    End Property

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
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
#End Region
End Class

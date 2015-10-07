Imports MediatorLib

Public Class BankFinderViewModel
    Inherits BaseViewModel

    Public ReadOnly Property BankNames As List(Of String)
        Get
            Dim Names = Db.ClearingNumbers.Select(Function(x) x.Name).Distinct().ToList()
            Names.Sort()
            Return Names
        End Get
    End Property

    Public Sub New()
        ' Register all decorated methods to the Mediator
        VMMediator.Register(Me)
    End Sub

    <MediatorMessageSink(MediatorMessages.ClearingNumbersUpdated, ParameterType:=GetType(Message))>
    Public Sub ListenForDbUpdates(m As Message)
        OnPropertyChanged("BankNames")
    End Sub
End Class

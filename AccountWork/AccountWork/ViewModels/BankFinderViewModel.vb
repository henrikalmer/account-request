Public Class BankFinderViewModel
    Inherits BaseViewModel

    Public Property BankNames As List(Of String)

    Public Sub New()
        BankNames = Db.ClearingNumbers.Select(Function(x) x.Name).Distinct().ToList()
        BankNames.Sort()
    End Sub
End Class

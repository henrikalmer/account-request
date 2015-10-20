Imports AccountWork.Domain

Public Class IdNumberAndPeriodForm
    Inherits BaseControl

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        layoutRoot.DataContext = New IdNumberAndPeriodFormViewModel()
    End Sub

    Private Sub allBanksCheckbox_Click(sender As Object, e As RoutedEventArgs) Handles allBanksCheckbox.Click
        Select Case allBanksCheckbox.IsChecked
            Case False
                bankFinder.Enable()
            Case True
                bankFinder.Disable()
        End Select
    End Sub
End Class

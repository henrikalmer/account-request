Public Class IdNumberAndPeriodForm
    Private Sub allBanksCheckbox_Click(sender As Object, e As RoutedEventArgs) Handles allBanksCheckbox.Click
        Select Case allBanksCheckbox.IsChecked
            Case False
                bankFinder.Enable()
            Case True
                bankFinder.Disable()
        End Select
    End Sub
End Class

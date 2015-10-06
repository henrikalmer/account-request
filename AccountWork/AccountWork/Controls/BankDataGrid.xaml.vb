Public Class BankDataGrid
    Private Sub dataGridEditButton_Click(sender As Object, e As RoutedEventArgs) Handles dataGridEditButton.Click
        Select Case clearingNumberDataGrid.IsReadOnly
            Case True
                clearingNumberDataGrid.IsReadOnly = False
                dataGridEditButton.Content = "Lås redigering"
            Case False
                clearingNumberDataGrid.IsReadOnly = True
                dataGridEditButton.Content = "Lås upp för redigering"
        End Select
    End Sub

    Private Sub clearingNumberDataGrid_CellEditEnding(sender As Object, e As DataGridCellEditEndingEventArgs) Handles clearingNumberDataGrid.CellEditEnding
        DataContext.HasChanges = True
    End Sub

    Private Sub dataGridSaveButton_Click(sender As Object, e As RoutedEventArgs) Handles dataGridSaveButton.Click
        DataContext.SaveChanges()
    End Sub
End Class

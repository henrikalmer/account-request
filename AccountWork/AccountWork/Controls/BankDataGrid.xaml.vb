Imports AccountWork.Domain

Public Class BankDataGrid
    Inherits BaseControl

    Private Sub dataGridEditButton_Click(sender As Object, e As RoutedEventArgs) Handles dataGridEditButton.Click
        Select Case clearingNumberDataGrid.IsReadOnly
            Case True
                clearingNumberDataGrid.IsReadOnly = False
                dataGridAddRowButton.IsEnabled = True
                dataGridRemoveRowsButton.IsEnabled = True
                dataGridEditButton.Content = "Lås redigering"
            Case False
                clearingNumberDataGrid.IsReadOnly = True
                dataGridAddRowButton.IsEnabled = False
                dataGridRemoveRowsButton.IsEnabled = False
                dataGridEditButton.Content = "Lås upp för redigering"
        End Select
    End Sub

    Private Sub clearingNumberDataGrid_CellEditEnding(sender As Object, e As DataGridCellEditEndingEventArgs) Handles clearingNumberDataGrid.CellEditEnding
        DataContext.HasChanges = True
    End Sub

    Private Sub dataGridAddRowButton_Click(sender As Object, e As RoutedEventArgs) Handles dataGridAddRowButton.Click
        Dim row As New ClearingNumber()
        DataContext.Add(row)
        clearingNumberDataGrid.ScrollIntoView(row)
    End Sub

    Private Sub dataGridRemoveRowsButton_Click(sender As Object, e As RoutedEventArgs) Handles dataGridRemoveRowsButton.Click
        While clearingNumberDataGrid.SelectedItems.Count > 0
            Dim Row = clearingNumberDataGrid.Items(clearingNumberDataGrid.SelectedIndex)
            DataContext.Remove(Row)
        End While
    End Sub

    Private Sub dataGridSaveButton_Click(sender As Object, e As RoutedEventArgs) Handles dataGridSaveButton.Click
        DataContext.SaveChanges()
    End Sub

    Private Sub dataGridResetButton_Click(sender As Object, e As RoutedEventArgs) Handles dataGridResetButton.Click
        DataContext.Reset()
    End Sub
End Class

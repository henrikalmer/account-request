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
End Class

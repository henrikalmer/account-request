Public Class RequestDataGrid
    Inherits BaseControl

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        layoutRoot.DataContext = New RequestDataGridViewModel()
    End Sub

    Private Sub dataGridDeleteButton_Click(sender As Object, e As RoutedEventArgs) Handles dataGridDeleteButton.Click
        layoutRoot.DataContext.Remove(requestHistoryDataGrid.SelectedItem.requestId)
    End Sub
End Class

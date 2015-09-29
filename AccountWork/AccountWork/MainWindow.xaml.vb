Imports AccountWork.Domain

Class MainWindow
    Private Sub checkBox_Click(sender As Object, e As RoutedEventArgs) Handles checkBox.Click
        Select Case checkBox.IsChecked
            Case False
                bankFinder.Enable()
            Case True
                bankFinder.Disable()
        End Select
    End Sub
End Class

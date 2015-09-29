Imports AccountWork.Domain

Class MainWindow
    Private Sub checkBox_Click(sender As Object, e As RoutedEventArgs) Handles checkBox.Click
        Select Case checkBox.IsChecked
            Case False
                bankTextBox.IsEnabled = True
                clearingNumberTextBox.IsEnabled = True
            Case True
                bankTextBox.Text = ""
                clearingNumberTextBox.Text = ""
                bankTextBox.IsEnabled = False
                clearingNumberTextBox.IsEnabled = False
        End Select
    End Sub

    Private Sub searchClearing_Click(sender As Object, e As RoutedEventArgs) Handles searchClearing.Click
        bankTextBox.Text = ""
        If Trim(clearingNumberTextBox.Text) <> "" Then
            ' Do both interval search and distinct search in Db.
            Using Db = New AccountWorkDbContext()
                Dim Query = From X In Db.ClearingNumbers
                            Order By X.Name
                            Select X
                            Where X.ClearingNumberIntervalStart = clearingNumberTextBox.Text _
                                Or (clearingNumberTextBox.Text >= X.ClearingNumberIntervalStart _
                                And clearingNumberTextBox.Text <= X.ClearingNumberIntervalEnd)

                Dim Item As ClearingNumber = Query.FirstOrDefault()
                If (Not Item Is Nothing) Then
                    bankTextBox.Text = Item.Name
                End If
            End Using
        End If
    End Sub
End Class

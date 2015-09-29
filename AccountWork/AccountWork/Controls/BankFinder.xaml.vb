Imports AccountWork.Domain

Public Class BankFinder
    Private Sub clearingTextBox_TextChanged(sender As Object, e As TextChangedEventArgs)
        Dim textBox As TextBox = sender

        Dim number As Integer
        If (textBox.Text.Length = 4 And Integer.TryParse(textBox.Text, number)) Then
            Using Db = New AccountWorkDbContext()
                Dim Query = From X In Db.ClearingNumbers
                            Order By X.Name
                            Select X
                            Where X.ClearingNumberIntervalStart = number _
                                And X.ClearingNumberIntervalEnd Is Nothing
                Dim Item As ClearingNumber = Query.SingleOrDefault()

                If (Item Is Nothing) Then
                    Query = From X In Db.ClearingNumbers
                            Order By X.Name
                            Select X
                            Where number >= X.ClearingNumberIntervalStart _
                                And number <= X.ClearingNumberIntervalEnd
                            Order By X.ClearingNumberIntervalStart Descending
                    Item = Query.FirstOrDefault()
                End If

                If (Not Item Is Nothing) Then
                    bankComboBox.SelectedValue = Item.Name
                End If
            End Using
        Else
            bankComboBox.SelectedIndex = -1
        End If
    End Sub
End Class

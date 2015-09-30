Imports AccountWork.Domain

Class MainWindow
    Private Sub checkBox_Click(sender As Object, e As RoutedEventArgs) Handles chkOrderALL.Checked
        Select Case chkOrderALL.IsChecked
            Case False
                bankFinder.Enable()
            Case True
                bankFinder.Disable()
        End Select
    End Sub

    Private Sub nameTextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles nameTextBox.TextChanged

    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        'Generate word file for order of type

        Dim MailOrderAttachment As New WordDocument
        MailOrderAttachment.parseGenerateOrder("c:\temp\kontobestmall.dotx", Me.ebNumberTextBox.Text, Me.aklTextBox.Text, Me.pnrTextBox.Text, Me.nameTextBox.Text, Me.nameTextBox.Text, "bank", Me.dateStartDatePicker.Text.ToString, Me.dateEndDatePicker.Text.ToString, Me.tabControl.SelectedItem.ToString)

    End Sub
End Class

Imports AccountWork.Domain

Class MainWindow
    Private Sub checkBox_Click(sender As Object, e As RoutedEventArgs) Handles chkOrderALL.Checked
        'Select Case chkOrderALL.IsChecked
        '    Case False
        '        bankFinder.Enable()
        '    Case True
        '        bankFinder.Disable()
        'End Select
    End Sub

    Private Sub nameTextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles nameTextBox.TextChanged

    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        'Generate word file for order of type
        Dim tmpTabItem As New TabItem
        tmpTabItem = Me.tabControl.SelectedItem

        Dim MailOrderAttachment As New WordDocument

        Select Case tmpTabItem.Header.ToString
            Case "Engagemangsförfrågan"
                If chkOrderALL.IsChecked = False Then
                    MailOrderAttachment.parseGenerateOrder("c:\temp\kontobestmall.dotx", Me.ebNumberTextBox.Text, Me.aklTextBox.Text, Me.pnrTextBox.Text, Me.nameTextBox.Text, bankFinder.bankComboBox.Text, bankFinder.clearingTextBox.Text, Me.dateStartDatePicker.Text.ToString, Me.dateEndDatePicker.Text.ToString, Me.cardNumberTextBox.Text, Me.phoneNumberTextBox.Text, Me.bankCardReaderTextBox.Text, Me.phoneNumber2TextBox.Text, tmpTabItem.Header.ToString)
                Else
                    MailOrderAttachment.parseGenerateOrder("c:\temp\kontobestmall.dotx", Me.ebNumberTextBox.Text, Me.aklTextBox.Text, Me.pnrTextBox.Text, Me.nameTextBox.Text, " ÖPPEN FRÅGA ALLA BANKER", "ÖPPEN FRÅGA ALLA CLEARINGNR", Me.dateStartDatePicker.Text.ToString, Me.dateEndDatePicker.Text.ToString, Me.cardNumberTextBox.Text, Me.phoneNumberTextBox.Text, Me.bankCardReaderTextBox.Text, Me.phoneNumber2TextBox.Text, tmpTabItem.Header.ToString)
                End If
            Case "Kontotecknarförfrågan"
                'todo
            Case "Förenklat kontoutdrag"
                'todo
        End Select

        tmpTabItem = Nothing

    End Sub

    Private Sub chkOrderALL_Click(sender As Object, e As RoutedEventArgs) Handles chkOrderALL.Click
        Select Case chkOrderALL.IsChecked
            Case False
                bankFinder.Enable()
            Case True
                bankFinder.Disable()
        End Select
    End Sub
End Class

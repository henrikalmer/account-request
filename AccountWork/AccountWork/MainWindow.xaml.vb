Imports AccountWork.Domain

Class MainWindow
    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        'Generate word file for order of type
        Dim tmpTabItem As New TabItem
        tmpTabItem = Me.tabControl.SelectedItem

        Dim MailOrderAttachment As New WordDocument
        'spara dokumentet någonstans, som ole-obj i en sqlite? inte på G:\ eller H:\ i vart fall
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
        MailOrderAttachment = Nothing

        'email the file to relevant bank(s)
        'whereTo As String, cc As String, attachment As String, strtype As String, strSubj As String
        ' Dim sendRequest As New OutlookCommunicator
        ' sendRequest.MailBanks(whereTo:=banken@banken.se, attachment:=minfil.docx, cc:=regbrevlådan, strSubj:=ebnumret, strtype:=engagemang/konto etc )
    End Sub

    Private Sub chkOrderALL_Click(sender As Object, e As RoutedEventArgs) Handles chkOrderALL.Click
        Select Case chkOrderALL.IsChecked
            Case False
                bankFinder.Enable()
            Case True
                bankFinder.Disable()
        End Select
    End Sub

    Private Sub button2_Click(sender As Object, e As RoutedEventArgs) Handles button2.Click
        Dim test As New OutlookCommunicator

        MsgBox(test.CheckIfNewMailFromBanks("EB 12345-15"))
    End Sub
End Class

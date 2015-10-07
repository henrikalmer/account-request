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
                Dim EbNumber = ebNumberTextBox.Text
                Dim Prosecutor = aklTextBox.Text
                Dim Pnr = engagementForm.pnrTextBox.Text
                Dim Name = engagementForm.nameTextBox.Text
                Dim PeriodStart = engagementForm.dateStartDatePicker.Text.ToString
                Dim PeriodEnd = engagementForm.dateEndDatePicker.Text.ToString
                Dim CardNumber = cardNumberTextBox.Text
                Dim PhoneNumber = phoneNumberTextBox.Text
                Dim BankCardReader = bankCardReaderTextBox.Text
                Dim PhoneNumber2 = phoneNumber2TextBox.Text
                Dim TabHeader = tmpTabItem.Header.ToString
                Dim BankName = ""
                Dim BankClearing = ""
                If engagementForm.allBanksCheckbox.IsChecked = False Then
                    BankName = engagementForm.bankFinder.bankComboBox.Text
                    BankClearing = engagementForm.bankFinder.clearingTextBox.Text
                Else
                    BankName = "ÖPPEN FRÅGA ALLA BANKER"
                    BankClearing = "ÖPPEN FRÅGA ALLA CLEARINGNR"
                End If
                MailOrderAttachment.parseGenerateOrder("c:\temp\kontobestmall.dotx",
                                                       EbNumber, Prosecutor, Pnr,
                                                       Name, BankName, BankClearing,
                                                       PeriodStart, PeriodEnd,
                                                       CardNumber, PhoneNumber,
                                                       BankCardReader, PhoneNumber2,
                                                       TabHeader)
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

    Private Sub button2_Click(sender As Object, e As RoutedEventArgs) Handles button2.Click
        Dim test As New OutlookCommunicator

        MsgBox(test.CheckIfNewMailFromBanks("EB 12345-15"))
    End Sub

    Private Sub ebNumberTextBox_LostFocus(sender As Object, e As RoutedEventArgs) Handles ebNumberTextBox.LostFocus
        DataContext.CurrentCase.NormalizeEbNumber()
    End Sub

    Private Sub tabControl_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles tabControl.SelectionChanged

    End Sub
End Class

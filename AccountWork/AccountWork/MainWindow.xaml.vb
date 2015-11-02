Imports System.Globalization
Imports System.Threading
Imports AccountWork.Domain

Class MainWindow
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Thread.CurrentThread.CurrentCulture = New CultureInfo("sv-SE")
        layoutRoot.DataContext = New MainWindowViewModel(Me)
    End Sub

    Private Sub engagementButton_Click(sender As Object, e As RoutedEventArgs) Handles engagementButton.Click
        'Generate word file for order of type
        Dim tmpTabItem As New TabItem
        tmpTabItem = Me.tabControl.SelectedItem

        Dim MailOrderAttachment As New WordDocument
        'spara dokumentet någonstans, som ole-obj i en sqlite? inte på G:\ eller H:\ i vart fall
        Dim EbNumber = ebNumberTextBox.Text
        Dim Prosecutor = aklTextBox.Text
        Dim Pnr = engagementForm.pnrTextBox.Text
        Dim PeriodStart = engagementForm.dateStartDatePicker.Text.ToString
        Dim PeriodEnd = engagementForm.dateEndDatePicker.Text.ToString
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
                                               BankName, BankClearing,
                                               PeriodStart, PeriodEnd)
        tmpTabItem = Nothing
        MailOrderAttachment = Nothing

        'email the file to relevant bank(s)
        'whereTo As String, cc As String, attachment As String, strtype As String, strSubj As String
        ' Dim sendRequest As New OutlookCommunicator
        ' sendRequest.MailBanks(whereTo:=banken@banken.se, attachment:=minfil.docx, cc:=regbrevlådan, strSubj:=ebnumret, strtype:=engagemang/konto etc )
    End Sub

    Private Sub accountButton_Click(sender As Object, e As RoutedEventArgs) Handles accountButton.Click
        'Generate word file for order of type
        Dim tmpTabItem As New TabItem
        tmpTabItem = Me.tabControl.SelectedItem

        Dim MailOrderAttachment As New WordDocument
        'spara dokumentet någonstans, som ole-obj i en sqlite? inte på G:\ eller H:\ i vart fall
        Dim EbNumber = ebNumberTextBox.Text
        Dim Prosecutor = aklTextBox.Text
        Dim AccountNumber = engagementForm.pnrTextBox.Text
        Dim PeriodStart = engagementForm.dateStartDatePicker.Text.ToString
        Dim PeriodEnd = engagementForm.dateEndDatePicker.Text.ToString
        Dim BankName = engagementForm.bankFinder.bankComboBox.Text
        Dim BankClearing = engagementForm.bankFinder.clearingTextBox.Text
        Dim TabHeader = tmpTabItem.Header.ToString
        'MailOrderAttachment.parseGenerateOrder("c:\temp\kontobestmall.dotx",
        '                                       EbNumber, Prosecutor,
        '                                       AccountNumber BankName,
        '                                       BankClearing, PeriodStart,
        '                                       PeriodEnd, TabHeader)
        tmpTabItem = Nothing
        MailOrderAttachment = Nothing

        'email the file to relevant bank(s)
        'whereTo As String, cc As String, attachment As String, strtype As String, strSubj As String
        ' Dim sendRequest As New OutlookCommunicator
        ' sendRequest.MailBanks(whereTo:=banken@banken.se, attachment:=minfil.docx, cc:=regbrevlådan, strSubj:=ebnumret, strtype:=engagemang/konto etc )
    End Sub

    Private Sub transactionButton_Click(sender As Object, e As RoutedEventArgs) Handles transactionButton.Click
        'Generate word file for order of type
        Dim tmpTabItem As New TabItem
        tmpTabItem = Me.tabControl.SelectedItem

        Dim MailOrderAttachment As New WordDocument
        'spara dokumentet någonstans, som ole-obj i en sqlite? inte på G:\ eller H:\ i vart fall
        Dim EbNumber = ebNumberTextBox.Text
        Dim Prosecutor = aklTextBox.Text
        Dim AccountNumber = engagementForm.pnrTextBox.Text
        Dim PeriodStart = engagementForm.dateStartDatePicker.Text.ToString
        Dim PeriodEnd = engagementForm.dateEndDatePicker.Text.ToString
        Dim BankName = engagementForm.bankFinder.bankComboBox.Text
        Dim BankClearing = engagementForm.bankFinder.clearingTextBox.Text
        Dim TabHeader = tmpTabItem.Header.ToString
        'MailOrderAttachment.parseGenerateOrder("c:\temp\kontobestmall.dotx",
        '                                       EbNumber, Prosecutor,
        '                                       AccountNumber BankName,
        '                                       BankClearing, PeriodStart,
        '                                       PeriodEnd, TabHeader)
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
        layoutRoot.DataContext.CurrentCase.NormalizeEbNumber()
    End Sub

    Private Sub button1_Click(sender As Object, e As RoutedEventArgs) Handles button1.Click
        Using Db = New AccountWorkDbContext()
            Dim Bank = (From X In Db.ClearingNumbers Select X Where X.Id = 98).SingleOrDefault()
            Db.Requests.Add(New Request(Bank, "1", Nothing, "6500123456789", Today, Today))
            Db.SaveChanges()
        End Using

        'axel testar wordklassen
        Dim j As New WordDocument
        'j.parseGenerateOrder("", "hh", "hh", "hh", "hh", "hh", "hh", "hh", "hh")
    End Sub
End Class

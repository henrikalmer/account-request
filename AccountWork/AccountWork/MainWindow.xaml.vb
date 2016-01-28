
Imports System.IO
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
        Dim TypeId As Integer = 1
        Dim TypeString As String = "Engagemangsförfrågan"
        Dim SecrecyDate = If(layoutRoot.DataContext.CurrentCase.Secrecy, layoutRoot.DataContext.CurrentCase.SecrecyDate, Nothing)
        Dim Bank As ClearingNumber = engagementForm.layoutRoot.DataContext.Bank
        Dim Pnr As String = engagementForm.pnrTextBox.Text
        Dim PeriodStart As Date = engagementForm.dateStartDatePicker.SelectedDate
        Dim PeriodEnd As Date = engagementForm.dateEndDatePicker.SelectedDate
        Dim IncludeStatements As String = engagementForm.requestStatementsComboBox.SelectedValue
        Dim Req As Request
        If (Bank Is Nothing) Then
            For Each B In layoutRoot.DataContext.AllBanks
                Req = layoutRoot.DataContext.CreateRequest(TypeId, TypeString, SecrecyDate, B, Pnr, Nothing, PeriodStart, PeriodEnd, IncludeStatements)
                GenerateEmail(Req)
            Next
        Else
            Req = layoutRoot.DataContext.CreateRequest(TypeId, TypeString, SecrecyDate, Bank, Pnr, Nothing, PeriodStart, PeriodEnd, IncludeStatements)
            GenerateEmail(Req)
        End If
    End Sub

    Private Sub accountButton_Click(sender As Object, e As RoutedEventArgs) Handles accountButton.Click
        Dim TypeId As Integer = 2
        Dim TypeString As String = "Kontotecknarförfrågan"
        Dim SecrecyDate = If(layoutRoot.DataContext.CurrentCase.Secrecy, layoutRoot.DataContext.CurrentCase.SecrecyDate, Nothing)
        Dim Bank As ClearingNumber = accountHolderForm.layoutRoot.DataContext.Bank
        Dim AccNo As String = accountHolderForm.bankFinder.clearingTextBox.Text
        Dim PeriodStart As Date = accountHolderForm.dateStartDatePicker.SelectedDate
        Dim PeriodEnd As Date = accountHolderForm.dateEndDatePicker.SelectedDate
        Dim Req As Request = layoutRoot.DataContext.CreateRequest(TypeId, TypeString, SecrecyDate, Bank, Nothing, AccNo, PeriodStart, PeriodEnd, "Nej")
        GenerateEmail(Req)
    End Sub

    Private Sub transactionMediumButton_Click(sender As Object, e As RoutedEventArgs) Handles transactionMediumButton.Click
        Dim TypeId As Integer = 3
        Dim TypeString As String = "Kontoutdrag medium"
        Dim SecrecyDate = If(layoutRoot.DataContext.CurrentCase.Secrecy, layoutRoot.DataContext.CurrentCase.SecrecyDate, Nothing)
        Dim Bank As ClearingNumber = transactionMediumForm.layoutRoot.DataContext.Bank
        Dim AccNo As String = transactionMediumForm.bankFinder.clearingTextBox.Text
        Dim PeriodStart As Date = transactionMediumForm.dateStartDatePicker.SelectedDate
        Dim PeriodEnd As Date = transactionMediumForm.dateEndDatePicker.SelectedDate
        Dim Req As Request = layoutRoot.DataContext.CreateRequest(TypeId, TypeString, SecrecyDate, Bank, Nothing, AccNo, PeriodStart, PeriodEnd, "Nej")
        GenerateEmail(Req)
    End Sub

    Private Sub transactionSmallButton_Click(sender As Object, e As RoutedEventArgs) Handles transactionSmallButton.Click
        Dim TypeId As Integer = 4
        Dim TypeString As String = "Kontoutdrag small"
        Dim SecrecyDate = If(layoutRoot.DataContext.CurrentCase.Secrecy, layoutRoot.DataContext.CurrentCase.SecrecyDate, Nothing)
        Dim Bank As ClearingNumber = transactionSmallForm.layoutRoot.DataContext.Bank
        Dim AccNo As String = transactionSmallForm.bankFinder.clearingTextBox.Text
        Dim PeriodStart As Date = transactionSmallForm.dateStartDatePicker.SelectedDate
        Dim PeriodEnd As Date = transactionSmallForm.dateEndDatePicker.SelectedDate
        Dim Req As Request = layoutRoot.DataContext.CreateRequest(TypeId, TypeString, SecrecyDate, Bank, Nothing, AccNo, PeriodStart, PeriodEnd, "Nej")
        GenerateEmail(Req)
    End Sub

    Private Sub GenerateEmail(Req As Request)
        Dim ReqObj As New RequestObject(Req.SerializedRequest, "json")
        Dim WordGenerator As New WordGenerator
        Dim WordAttachment = WordGenerator.Generate(ReqObj, Req.Id)
        Dim XmlAttachment As String = Path.GetTempPath & Req.Id & ".xml"
        Dim JsonAttachment As String = Path.GetTempPath & Req.Id & ".json"
        My.Computer.FileSystem.WriteAllText(XmlAttachment, ReqObj.ToXml(), False)
        My.Computer.FileSystem.WriteAllText(JsonAttachment, ReqObj.ToJson(), False)
        Dim OutlookCommunicator As New OutlookCommunicator
        Dim Recipient As String
        If (Req.Bank Is Nothing) Then
            Recipient = layoutRoot.DataContext.AllBanksRecipientString
        Else
            Recipient = Req.Bank.Email
        End If
        Dim CC = "" 'Utils.GetUserRegEmail()
        OutlookCommunicator.Generate(Recipient, CC, WordAttachment, XmlAttachment, JsonAttachment, ReqObj.TypeString, ReqObj.EbNumber)
    End Sub

    Private Sub ebNumberTextBox_LostFocus(sender As Object, e As RoutedEventArgs) Handles ebNumberTextBox.LostFocus
        layoutRoot.DataContext.CurrentCase.NormalizeEbNumber()
        layoutRoot.DataContext.CurrentCase.SearchProsecutors()
    End Sub
End Class

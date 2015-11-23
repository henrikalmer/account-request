
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
        Dim Type As String = "1. Engagemangsförfrågan"
        Dim Bank As ClearingNumber = engagementForm.layoutRoot.DataContext.Bank
        Dim Pnr As String = engagementForm.pnrTextBox.Text
        Dim PeriodStart As Date = engagementForm.dateStartDatePicker.DisplayDate
        Dim PeriodEnd As Date = engagementForm.dateEndDatePicker.DisplayDate
        Dim Req As Request = layoutRoot.DataContext.CreateRequest(Type, Bank, Pnr, Nothing, PeriodStart, PeriodEnd)
        GenerateEmail(Req)
    End Sub

    Private Sub accountButton_Click(sender As Object, e As RoutedEventArgs) Handles accountButton.Click
        Dim Type As String = "2. Kontotecknarförfrågan"
        Dim Bank As ClearingNumber = accountHolderForm.layoutRoot.DataContext.Bank
        Dim AccNo As String = accountHolderForm.bankFinder.clearingTextBox.Text
        Dim PeriodStart As Date = accountHolderForm.dateStartDatePicker.DisplayDate
        Dim PeriodEnd As Date = accountHolderForm.dateEndDatePicker.DisplayDate
        Dim Req As Request = layoutRoot.DataContext.CreateRequest(Type, Bank, Nothing, AccNo, PeriodStart, PeriodEnd)
        GenerateEmail(Req)
    End Sub

    Private Sub transactionButton_Click(sender As Object, e As RoutedEventArgs) Handles transactionButton.Click
        Dim Type As String = "3. Förenklat kontoutdrag"
        Dim Bank As ClearingNumber = transactionForm.layoutRoot.DataContext.Bank
        Dim AccNo As String = transactionForm.bankFinder.clearingTextBox.Text
        Dim PeriodStart As Date = transactionForm.dateStartDatePicker.DisplayDate
        Dim PeriodEnd As Date = transactionForm.dateEndDatePicker.DisplayDate
        Dim Req As Request = layoutRoot.DataContext.CreateRequest(Type, Bank, Nothing, AccNo, PeriodStart, PeriodEnd)
        GenerateEmail(Req)
    End Sub

    Private Sub GenerateEmail(Req As Request)
        Dim ReqObj As New RequestObject(Req.SerializedRequest, "json")
        Dim WordGenerator As New WordDocument
        Dim WordAttachment = WordGenerator.Generate(ReqObj, Req.Id)
        Dim XmlAttachment As String = Path.GetTempPath & Req.Id & ".xml"
        Dim JsonAttachment As String = Path.GetTempPath & Req.Id & ".json"
        My.Computer.FileSystem.WriteAllText(XmlAttachment, ReqObj.ToXml(), False)
        My.Computer.FileSystem.WriteAllText(JsonAttachment, ReqObj.ToJson(), False)
        Dim OutlookCommunicator As New OutlookCommunicator
        OutlookCommunicator.Generate("banken@banken.se", "kansli@ekobrottsmyndigheten.se", WordAttachment, XmlAttachment, JsonAttachment, ReqObj.TypeOfRequest)
    End Sub

    Private Sub ebNumberTextBox_LostFocus(sender As Object, e As RoutedEventArgs) Handles ebNumberTextBox.LostFocus
        layoutRoot.DataContext.CurrentCase.NormalizeEbNumber()
        layoutRoot.DataContext.CurrentCase.SearchProsecutors()
    End Sub
End Class

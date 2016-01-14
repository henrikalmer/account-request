Imports Outlook = Microsoft.Office.Interop.Outlook

Public Class OutlookCommunicator
    WithEvents Momentary_session As Outlook.Application

    Public Sub Generate(Recipient As String, CC As String, WordFile As String, XmlFile As String, JsonFile As String, Type As String, EbNumber As String)
        Dim App As New Outlook.Application
        Dim Email As Outlook.MailItem
        ' Find users outbox
        Dim MapiNamespace As Outlook.NameSpace = App.GetNamespace("MAPI")
        Dim Drafts As Outlook.MAPIFolder = MapiNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderOutbox)
        ' Put together email and place in the users outbox
        Try
            Email = App.CreateItem(Outlook.OlItemType.olMailItem)
            Dim Recipients As Outlook.Recipients = Email.Recipients
            Recipients.Add(Recipient)
            If (CC <> "") Then
                Dim CCRecipient = Recipients.Add(CC)
                CCRecipient.Type = Outlook.OlMailRecipientType.olCC
            End If
            Dim RetVal = Recipients.ResolveAll()
            Email.Subject = "Pilot: Begäran om uppgift i ärende " & EbNumber
            Email.Body = "Vi beställer härmed in " & LCase(Type) & " enligt bifogad fil." & vbNewLine
            Email.BodyFormat = Outlook.OlBodyFormat.olFormatRichText
            Dim Attachments As Outlook.Attachments = Email.Attachments
            Attachments.Add(WordFile)
            Attachments.Add(XmlFile)
            Attachments.Add(JsonFile)
            Email.Save()
            Email.Move(Drafts)
            MsgBox("Mail är nu placerat i din utkorg redo att skickas. Ångrar du dig, ta helt enkelt bort mailet från utkorgen.", MsgBoxStyle.OkOnly)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Mail could not be sent")
        Finally
            Email = Nothing
            App = Nothing
        End Try
    End Sub
End Class

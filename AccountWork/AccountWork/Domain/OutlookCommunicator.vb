Imports outlookappINTERFACE = Microsoft.Office.Interop.Outlook
Imports System.Runtime.InteropServices


Public Class OutlookCommunicator
    Public Sub MailBanks(whereTo As String, cc As String, attachment As String, strtype As String, strSubj As String)

        Dim outlookapp As New outlookappINTERFACE.Application

        Dim mail As Microsoft.Office.Interop.Outlook.MailItem = Nothing
        Dim mailRecipients As Microsoft.Office.Interop.Outlook.Recipients = Nothing
        Dim mailRecipient As Microsoft.Office.Interop.Outlook.Recipient = Nothing
        Dim ccRecipient As Microsoft.Office.Interop.Outlook.Recipient = Nothing
        Dim omNamespace As Microsoft.Office.Interop.Outlook.NameSpace = OutlookApp.GetNamespace("MAPI")
        Dim omDrafts As Microsoft.Office.Interop.Outlook.MAPIFolder = omNamespace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderOutbox)
        Try

            mail = OutlookApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem)
            mail.Subject = "Beställning av kontoutdrag/engagemang"
            mail.Attachments.Add(attachment)
            mail.Body = "Vi beställer härmed in " & LCase(strtype) & " enligt bifogad fil. "

            If whereTo <> "" Then
                mailRecipients = mail.Recipients
                mailRecipient = mailRecipients.Add(whereTo)
                If cc <> "" Then
                    ccRecipient = mailRecipients.Add(cc)
                    ccRecipient.Resolve()
                    ccRecipient.Type = 2
                End If
                mailRecipient.Resolve()

                mail.Subject = strSubj
            End If



            mail.Move(omDrafts)
            MsgBox("Mail är nu placerat i din utkorg redo att skickas. Ångrar du dig, ta helt enkelt bort mailet från utkorgen.", MsgBoxStyle.OkOnly)
            'If (mailRecipient.Resolved) Then
            '    'mail.Send()

            '    'Dim omMailItem As Microsoft.Office.Interop.Outlook.MailItem = CType(omDrafts.Items.Add, mail)
            '    'mail.Move(omDrafts)

            '    MsgBox("Mail är nu placerat i din utkorg redo att skickas. Ångrar du dig, ta helt enkelt bort mailet från utkorgen.", MsgBoxStyle.OkOnly)
            'Else
            '    System.Windows.Forms.MessageBox.Show(
            '        "There is no such record in your address book.")
            'End If
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.Message,
                "An exception is occured in the code of add-in.")
        Finally
            If Not IsNothing(mailRecipient) Then Marshal.ReleaseComObject(mailRecipient)
            If Not IsNothing(mailRecipients) Then Marshal.ReleaseComObject(mailRecipients)
            If Not IsNothing(ccRecipient) Then Marshal.ReleaseComObject(ccRecipient)
            If Not IsNothing(mail) Then Marshal.ReleaseComObject(mail)
            If Not IsNothing(omNamespace) Then Marshal.ReleaseComObject(omNamespace)
            If Not IsNothing(omDrafts) Then Marshal.ReleaseComObject(omDrafts)
        End Try
    End Sub

End Class

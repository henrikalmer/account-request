Imports outlookappINTERFACE = Microsoft.Office.Interop.Outlook
Imports System.Runtime.InteropServices


Public Class OutlookCommunicator
    WithEvents Momentary_session As outlookappINTERFACE.Application
    Public Event eBankAnswer()
    Public Sub New()

    End Sub

    Public Sub MailBanks(whereTo As String, cc As String, attachment As String, strtype As String, strSubj As String)

        Dim outlookapp As New outlookappINTERFACE.Application

        Dim mail As Microsoft.Office.Interop.Outlook.MailItem = Nothing
        Dim mailRecipients As Microsoft.Office.Interop.Outlook.Recipients = Nothing
        Dim mailRecipient As Microsoft.Office.Interop.Outlook.Recipient = Nothing
        Dim ccRecipient As Microsoft.Office.Interop.Outlook.Recipient = Nothing
        Dim omNamespace As Microsoft.Office.Interop.Outlook.NameSpace = outlookapp.GetNamespace("MAPI")
        Dim omDrafts As Microsoft.Office.Interop.Outlook.MAPIFolder = omNamespace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderOutbox)
        Try

            mail = outlookapp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem)
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

    Public Function CheckIfNewMailFromBanks(sEbNr As String) As String

        Dim olNS As outlookappINTERFACE.NameSpace
        Dim InputFolder As outlookappINTERFACE.MAPIFolder
        Dim olMail As outlookappINTERFACE.Items
        Dim item As outlookappINTERFACE.MailItem
        Dim sRetValue As String = ""
        Momentary_session = GetObject(, "Outlook.Application")
        olNS = Momentary_session.GetNamespace("MAPI")
        ' InputFolder = olNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox)
        InputFolder = olNS.Folders("axelthor@live.com").Folders("Inkorgen")
        olMail = InputFolder.Items.Restrict("[UnRead] = True")


        If olMail.Count > 0 Then
            RaiseEvent eBankAnswer() 'gör nåt ball med den

            For Each item In olMail
                If InStr(LCase(item.Subject.ToString), LCase(sEbNr)) > 0 Then
                    'Stop
                    'copy with attachement to our database
                    'db.save.blob (olmail.attachement + olmail.contents.text.tostring etc)
                    '  MsgBox("nytt mail " & sEbNr & item.Body.ToString)
                    sRetValue = "svar från banken gällande " & sEbNr & item.Body.ToString & " läggs in i databasen.."
                End If
            Next
        End If

        Momentary_session = Nothing
        If Not IsNothing(olMail) Then Marshal.ReleaseComObject(olMail)
        If Not IsNothing(olNS) Then Marshal.ReleaseComObject(olNS)
        If Not IsNothing(item) Then Marshal.ReleaseComObject(item)


        Return sRetValue
    End Function


End Class

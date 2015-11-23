Imports System.IO
Imports Word = Microsoft.Office.Interop.Word
Imports AccountWork.Domain

Public Class WordGenerator
    Private ReadOnly Property WordTemplate As Byte()
        Get
            Return My.Resources.kontobestmall
        End Get
    End Property

    Public Function Generate(ReqObj As RequestObject, RequestId As Integer) As String
        ' Define content
        Dim DomainUser As String = Security.Principal.WindowsIdentity.GetCurrent.Name.Replace("\", "/")
        Dim AdEntry As New DirectoryServices.DirectoryEntry("WinNT://" & DomainUser)
        Dim FullName As String = AdEntry.Properties("FullName").Value
        ' Create temp file
        Dim TmpPath As String = Path.GetTempPath() & Path.GetRandomFileName()
        Dim AttachmentPath As String = Path.GetTempPath() & "Begäran " & RequestId & ".doc"
        Using fs As FileStream = File.Create(TmpPath)
            fs.Write(WordTemplate, 0, WordTemplate.Length)
            fs.Close()
        End Using
        ' Write document
        Dim WordApp As Word.Application = CreateObject("Word.Application")
        Dim Documents As Word.Documents = WordApp.Documents
        Dim Doc As Word.Document = Documents.Add(TmpPath)
        Dim Paragraph As Word.Paragraph = Doc.Paragraphs.Add()
        With Paragraph
            .Range.Text = "Begäran om uppgifter enligt 11 § lag (2004: 297) om bank- och finansieringsrörelse"
            .Range.Font.Bold = True
            .Format.SpaceAfter = 24
            .Range.InsertParagraphAfter()
        End With

        Paragraph = Doc.Paragraphs.Add()
        With Paragraph
            .Range.Text = "I pågående förundersökning " & ReqObj.EbNumber &
                " begär åklagare att uppgifter enligt 11 § lag (2004:297) om" &
                " bank- och finansieringsrörelse om enskildes förhållanden lämnas ut enligt följande:"
            .Range.Font.Bold = False
            .Format.SpaceAfter = 12
            .Range.InsertParagraphAfter()
        End With

        Paragraph = Doc.Paragraphs.Add()
        With Paragraph
            .Range.Font.Bold = False
            .Format.SpaceAfter = 12
            If ReqObj.TypeOfRequest = "1. Engagemangsförfrågan" Then
                .Range.Text = "Engagemangsförfrågan bla bla"
                .Range.Text &= "Personnr: " & ReqObj.IdNumber
            ElseIf ReqObj.TypeOfRequest = "2. Kontotecknarförfrågan" Then
                .Range.Text = "Begäran om kontotecknarförfrågan bla bla"
                .Range.Text &= "Kontonummer: " & ReqObj.AccountNumber
            ElseIf ReqObj.TypeOfRequest = "3. Förenklat kontoutdrag" Then
                .Range.Text = "Begäran om förenklat kontoutdrag bla bla"
                .Range.Text &= "Kontonummer: " & ReqObj.AccountNumber
            End If
            .Range.InsertParagraphAfter()
        End With

        Paragraph = Doc.Paragraphs.Add()
        With Paragraph
            .Range.Text = "Period, startdatum: " & ReqObj.PeriodStartDate
            .Range.Text &= "Period, slutdatum: " & ReqObj.PeriodEndDate
            .Range.Text &= "På uppdrag av åklagare " & ReqObj.Prosecutor
            .Range.Text &= "Med vänlig hälsning, " & FullName
            .Range.Font.Bold = False
            .Range.InsertParagraphAfter()
        End With

        Doc.SaveAs2(AttachmentPath)
        WordApp.Quit()
        Return AttachmentPath
    End Function
End Class
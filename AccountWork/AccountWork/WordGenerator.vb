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

        Dim ParameterString As String = String.Empty
        Paragraph = Doc.Paragraphs.Add()
        With Paragraph
            If ReqObj.TypeId = 1 Then
                .Range.Text = "Begäran om engagemangsförfrågan (frågetyp 1)." & vbNewLine
                ParameterString = "Personnummer:" & vbTab & ReqObj.IdNumber
            ElseIf ReqObj.TypeId = 2 Then
                .Range.Text = "Begäran om kontotecknarförfrågan (frågetyp 2)." & vbNewLine
                ParameterString = "Kontonummer:" & vbTab & ReqObj.AccountNumber
            ElseIf ReqObj.TypeId = 3 Then
                .Range.Text = "Begäran om förenklat kontoutdrag (frågetyp 3)." & vbNewLine
                ParameterString = "Kontonummer:" & vbTab & ReqObj.AccountNumber
            End If
            .Range.Font.Bold = False
            .Format.SpaceAfter = 0
            .Range.InsertParagraphAfter()
        End With

        Paragraph = Doc.Paragraphs.Add()
        With Paragraph
            .Range.Text = ParameterString &
                vbNewLine & "Period, startdatum: " & vbTab & ReqObj.PeriodStartDate &
                vbNewLine & "Period, slutdatum: " & vbTab & ReqObj.PeriodEndDate &
                vbNewLine
            .Range.Text &= "På uppdrag av åklagare " & ReqObj.Prosecutor & "." & vbNewLine
            .Range.Text &= "Svar önskas till " & ReqObj.Contact &
                " med CC till " & Utils.GetUserRegEmail() & ". Vid frågor kontakta mig på mail " &
                ReqObj.Contact & " eller telefon " & Utils.GetUserPhoneNo() & "." & vbNewLine
            .Range.Text &= "Med vänlig hälsning, " & Utils.GetUserFullName()
            .Range.Font.Bold = False
            .Format.SpaceAfter = 12
            .Range.InsertParagraphAfter()
        End With

        Doc.SaveAs2(AttachmentPath)
        WordApp.Quit()
        Return AttachmentPath
    End Function
End Class
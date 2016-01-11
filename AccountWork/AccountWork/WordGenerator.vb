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
        Doc.Activate()

        FindAndReplace(WordApp, "<%EbNo%>", ReqObj.EbNumber)
        FindAndReplace(WordApp, "<%ReqType%>", ReqObj.TypeId & ". " & ReqObj.TypeString)
        FindAndReplace(WordApp, "<%IdNo%>", ReqObj.IdNumber)
        FindAndReplace(WordApp, "<%AccNo%>", ReqObj.AccountNumber)
        FindAndReplace(WordApp, "<%RequestStatements%>", ReqObj.IncludeStatements)
        FindAndReplace(WordApp, "<%PeriodStart%>", ReqObj.PeriodStartDate.ToString("d"))
        FindAndReplace(WordApp, "<%PeriodEnd%>", ReqObj.PeriodEndDate.ToString("d"))

        If (Not ReqObj.SecrecyDate = Nothing) Then
            Dim SecrecyText As String = "Förundersökningsledaren har enligt 1 kap. 12 § lag (2004:297) " &
                "om bank- och finansieringsrörelse, förordnat att kreditinstitutet samt dess " &
                "styrelseledamöter och anställda inte får röja för kunden eller " &
                "för någon utomstående att uppgifterna ha lämnats enligt 11 § eller " &
                "att det pågår en förundersökning eller ett ärende om rättslig " &
                "hjälp i brottmål. Förbudet gäller tills vidare dock längst till " &
                "och med den " & ReqObj.SecrecyDate.ToString("d") & "."
            Doc.Paragraphs(28).Range.InsertParagraphBefore()
            Doc.Paragraphs(28).Range.Text = SecrecyText
        End If

        FindAndReplace(WordApp, "<%Name%>", Utils.GetUserFullName)
        FindAndReplace(WordApp, "<%Email%>", ReqObj.Contact)
        FindAndReplace(WordApp, "<%Prosecutor%>", ReqObj.Prosecutor)

        Doc.SaveAs2(AttachmentPath)
        WordApp.Quit()

        Return AttachmentPath
    End Function

    Private Sub FindAndReplace(doc As Word.Application, findText As Object, replaceWithText As Object)
        ' Define options
        Dim matchCase As Object = False
        Dim matchWholeWord As Object = True
        Dim matchWildCards As Object = False
        Dim matchSoundsLike As Object = False
        Dim matchAllWordForms As Object = False
        Dim forward As Object = True
        Dim format As Object = False
        Dim matchKashida As Object = False
        Dim matchDiacritics As Object = False
        Dim matchAlefHamza As Object = False
        Dim matchControl As Object = False
        Dim read_only As Object = False
        Dim visible As Object = True
        Dim replace As Object = 2
        Dim wrap As Object = 1
        ' Execute find and replace
        doc.Selection.Find.Execute(findText, matchCase, matchWholeWord, matchWildCards,
                                   matchSoundsLike, matchAllWordForms, forward, wrap,
                                   format, replaceWithText, replace, matchKashida,
                                   matchDiacritics, matchAlefHamza, matchControl)
    End Sub
End Class
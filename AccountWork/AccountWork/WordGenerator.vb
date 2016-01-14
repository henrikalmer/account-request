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

        ' replace holders with values
        FindAndReplace(WordApp, "<%EbNo%>", ReqObj.EbNumber)
        FindAndReplace(WordApp, "<%IdNo%>", ReqObj.IdNumber)
        FindAndReplace(WordApp, "<%AccNo%>", ReqObj.AccountNumber)
        FindAndReplace(WordApp, "<%PeriodStart%>", ReqObj.PeriodStartDate.ToString("d"))
        FindAndReplace(WordApp, "<%PeriodEnd%>", ReqObj.PeriodEndDate.ToString("d"))

        ' replace question type checkboxes with x:es
        Dim uncheckedPar As Integer = -1
        Dim checkedPar As Integer = -1
        Dim checkedIndex As Integer = -1
        Dim uncheckedP1 As New List(Of Integer) From {19, 42}
        Dim uncheckedP2 As New List(Of Integer) From {2, 24}
        If (ReqObj.TypeId = 1) Then
            checkedPar = 6
            uncheckedPar = 7
            checkedIndex = 19
            uncheckedP1.Remove(checkedIndex)
        ElseIf (ReqObj.TypeId = 2) Then
            checkedPar = 6
            uncheckedPar = 7
            checkedIndex = 42
            uncheckedP1.Remove(checkedIndex)
        ElseIf (ReqObj.TypeId = 3) Then
            checkedPar = 7
            uncheckedPar = 6
            checkedIndex = 2
            uncheckedP2.Remove(checkedIndex)
        ElseIf (ReqObj.TypeId = 4) Then
            checkedPar = 7
            uncheckedPar = 6
            checkedIndex = 24
            uncheckedP2.Remove(checkedIndex)
        End If
        ' add x to mark checked choice
        Doc.Paragraphs(checkedPar).Range.Text = Doc.Paragraphs(checkedPar).Range.Text.Insert(checkedIndex, "x")
        Doc.Paragraphs(uncheckedPar).Range.Text = Doc.Paragraphs(uncheckedPar).Range.Text
        ' add empty checkboxes as wingdings symbols
        Dim Font As Object = "Wingdings"
        Dim Unicode As Object = Type.Missing
        Dim Bias As Object = Type.Missing
        Dim subRange As Word.Range
        Dim index As Integer
        For Each ix As Integer In uncheckedP1
            subRange = Doc.Paragraphs(6).Range
            index = subRange.Start + ix
            subRange.SetRange(index, index)
            subRange.InsertSymbol(168, Font, Unicode, Bias)
        Next
        For Each ix As Integer In uncheckedP2
            subRange = Doc.Paragraphs(7).Range
            index = subRange.Start + ix
            subRange.SetRange(index, index)
            subRange.InsertSymbol(168, Font, Unicode, Bias)
        Next

        ' replace include statements checkboxes with x:es
        checkedIndex = -1
        Dim unchecked As New List(Of Integer) From {25, 31, 43}
        If (ReqObj.IncludeStatements = "Nej") Then
            checkedIndex = 25
        ElseIf (ReqObj.IncludeStatements = "Ja, Small") Then
            checkedIndex = 31
        ElseIf (ReqObj.IncludeStatements = "Ja, Medium") Then
            checkedIndex = 43
        End If
        unchecked.Remove(checkedIndex)
        ' add x to mark checked choice
        Doc.Paragraphs(11).Range.Text = Doc.Paragraphs(11).Range.Text.Insert(checkedIndex, "x")
        ' add empty checkboxes as wingdings symbols
        For Each ix As Integer In unchecked
            subRange = Doc.Paragraphs(11).Range
            index = subRange.Start + ix
            subRange.SetRange(index, index)
            subRange.InsertSymbol(168, Font, Unicode, Bias)
        Next

        ' add secrecy paragraph
        If (Not ReqObj.SecrecyDate = Nothing) Then
            Dim SecrecyText As String = "Förundersökningsledaren har enligt 1 kap. 12 § lag (2004:297) " &
                "om bank- och finansieringsrörelse, förordnat att kreditinstitutet samt dess " &
                "styrelseledamöter och anställda inte får röja för kunden eller " &
                "för någon utomstående att uppgifterna ha lämnats enligt 11 § eller " &
                "att det pågår en förundersökning eller ett ärende om rättslig " &
                "hjälp i brottmål. Förbudet gäller tills vidare dock längst till " &
                "och med den " & ReqObj.SecrecyDate.ToString("d") & "."
            Doc.Paragraphs(18).Range.InsertParagraphBefore()
            Doc.Paragraphs(18).Range.Text = SecrecyText
            Doc.Paragraphs(18).Range.InsertParagraphBefore()
        End If

        ' add contact info
        FindAndReplace(WordApp, "<%Name%>", Utils.GetUserFullName)
        FindAndReplace(WordApp, "<%Email%>", ReqObj.Contact)
        FindAndReplace(WordApp, "<%Prosecutor%>", ReqObj.Prosecutor)

        ' save and quit
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
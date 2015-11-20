Imports System.IO
Imports Word = Microsoft.Office.Interop.Word

Namespace Domain
    Public Class WordDocument
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
            Dim TmpFile As String = Path.GetTempPath & "Begäran " & RequestId & ".doc"
            My.Computer.FileSystem.WriteAllBytes(TmpFile, WordTemplate, False)
            ' Write document
            Dim WordApp As Word.Application = CreateObject("Word.Application")
            With WordApp
                .Visible = True
                .Documents.Add(TmpFile)
                .ActiveDocument.Range.Font.Bold = True
                .Selection.TypeText("Begäran om uppgifter enligt 11 § lag (2004: 297) om bank- och finansieringsrörelse")
                .Selection.TypeParagraph()
                .Selection.Font.Bold = False
                .Selection.TypeParagraph()
                .Selection.TypeParagraph()
                .Selection.TypeText("I pågående förundersökning " & ReqObj.EbNumber & " begär åklagare att uppgifter enligt 11 § lag (2004:297) om bank- och finansieringsrörelse om enskildes förhållanden lämnas ut enligt följande:")
                .Selection.TypeParagraph()
                .Selection.TypeParagraph()
                If ReqObj.TypeOfRequest = "1. Engagemangsförfrågan" Then
                    .Selection.TypeText("Engagemangsförfrågan bla bla")
                    .Selection.TypeParagraph()
                    .Selection.TypeParagraph()
                    .Selection.TypeText("Personnr: " & ReqObj.IdNumber)
                    .Selection.TypeParagraph()
                End If

                If ReqObj.TypeOfRequest = "2. Kontotecknarförfrågan" Then
                    .Selection.TypeText("Begäran om kontotecknarförfrågan bla bla")
                    .Selection.TypeParagraph()
                    .Selection.TypeParagraph()
                    .Selection.TypeText("Kontonummer: " & ReqObj.AccountNumber)
                    .Selection.TypeParagraph()
                End If

                If ReqObj.TypeOfRequest = "3. Förenklat kontoutdrag" Then
                    .Selection.TypeText("Begäran om förenklat kontoutdrag bla bla")
                    .Selection.TypeParagraph()
                    .Selection.TypeParagraph()
                    .Selection.TypeText("Kontonummer: " & ReqObj.AccountNumber)
                    .Selection.TypeParagraph()
                End If

                .Selection.TypeText("Period, startdatum: " & ReqObj.PeriodStartDate)
                .Selection.TypeParagraph()
                .Selection.TypeText("Period, slutdatum: " & ReqObj.PeriodEndDate)
                .Selection.TypeParagraph()
                .Selection.TypeText("På uppdrag av åklagare " & ReqObj.Prosecutor)
                .Selection.TypeParagraph()
                .Selection.TypeText("Med vänlig hälsning, " & FullName)
            End With
            WordApp.Documents.Close()
            Return TmpFile
        End Function
    End Class
End Namespace



Imports word = Microsoft.Office.Interop.Word
Namespace Domain
    Public Class WordDocument
        Public Sub parseGenerateOrder(sPath2template As String, sEBnr As String, sAklname As String, sPnr As String, sName As String, sBankName As String, sClearingno As String, sStartdate As String, sEnddate As String, sType As String)
            Dim sDomainUser As String = System.Security.Principal.WindowsIdentity.GetCurrent.Name.Replace("\", "/")
            Dim sADEntry As New DirectoryServices.DirectoryEntry("WinNT://" & sDomainUser)
            Dim sFullName As String = sADEntry.Properties("FullName").Value
            Dim oWord As Word.Application
            oWord = CreateObject("Word.Application")
            With oWord
                Stop
                .Visible = True
                .Documents.Add("c:\temp\kontobestmall.dotx")
                .ActiveDocument.Range.Font.Bold = True
                .Selection.TypeText("Begäran om uppgifter enligt 11 § lag (2004:297) om bank- och finansieringsrörelse")
                .Selection.TypeParagraph()
                .Selection.Font.Bold = False
                .Selection.TypeParagraph()
                .Selection.TypeParagraph()
                .Selection.TypeText("I pågående förundersökning " & sEBnr & " begär åklagare att uppgifter enligt 11 § lag (2004:297) om bank- och finansieringsrörelse om enskildes förhållanden lämnas ut enligt följande:")
                .Selection.TypeParagraph()
                .Selection.TypeParagraph()
                If sType = "Engagemangsförfrågan" Then
                    .Selection.TypeText("Förundersökningen har givit nedanstående ingångsparametrar och vill se om ni, baserat på dessa parametrar kan se om det finns engagemang hos er.")
                    .Selection.TypeParagraph()
                    .Selection.TypeParagraph()
                End If
            End With
        End Sub
    End Class
End Namespace



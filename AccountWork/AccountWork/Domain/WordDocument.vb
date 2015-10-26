Imports word = Microsoft.Office.Interop.Word
Namespace Domain
    Public Class WordDocument
        Public Sub parseGenerateOrder(sPath2template As String, sEBnr As String, sAklname As String, sPnr As String, sBankName As String, sClearingno As String, sStartdate As String, sEnddate As String, sType As String)
            Dim sDomainUser As String = System.Security.Principal.WindowsIdentity.GetCurrent.Name.Replace("\", "/")
            Dim sADEntry As New DirectoryServices.DirectoryEntry("WinNT://" & sDomainUser)
            Dim sFullName As String = sADEntry.Properties("FullName").Value
            Dim oWord As word.Application
            oWord = CreateObject("Word.Application")

            'sPath2template As String -template path '
            'sEBnr As String -ebnummer globalt '
            'sAklname As String -åkl namn globalt
            'sPnr As String -personnr '
            'sName As String -mt namn '
            'sBankName As String -bankens namn om ej all '
            'sClearingno As String -bankens clearing om ej all '
            'sStartdate As String -
            'sEnddate As String -
            'sKortnr As String -ev kortnr'
            'sPhoneno As String -tfn mt'
            'sBankreader As String -bankdosa, bankID'
            'sPhone2 As String -tfn som blivit påladdat från kontot vi frågar på '
            'sType As String -typen av förfrågan, input för besthist

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
                .Selection.TypeText("I pågående förundersökning " & UCase(sEBnr) & " begär åklagare att uppgifter enligt 11 § lag (2004:297) om bank- och finansieringsrörelse om enskildes förhållanden lämnas ut enligt följande:")
                .Selection.TypeParagraph()
                .Selection.TypeParagraph()
                If sType = "Engagemangsförfrågan" Then
                    .Selection.TypeText("Förundersökningen har givit nedanstående ingångsparametrar och vill se om ni, baserat på dessa parametrar kan se om det finns engagemang hos er.")
                    .Selection.TypeParagraph()
                    .Selection.TypeParagraph()

                    If Trim(sPnr) <> "" Then
                        .Selection.TypeText("Personnr: " & sPnr)
                        .Selection.TypeParagraph()
                    End If

                    If Trim(sClearingno) <> "" Then
                        .Selection.TypeText("Clearingnr: " & sClearingno)
                        .Selection.TypeParagraph()
                    End If

                    If Trim(sBankName) <> "" Then
                        .Selection.TypeText("Bank (namn): " & sBankName)
                        .Selection.TypeParagraph()
                    End If
                End If

                .Selection.TypeText("På uppdrag av åklagaren: " & sAklname)
                .Selection.TypeParagraph()
                .Selection.TypeText("Mvh " & sFullName)
                'end same for all
            End With
        End Sub
    End Class
End Namespace



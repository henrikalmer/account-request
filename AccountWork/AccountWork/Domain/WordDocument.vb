Imports word = Microsoft.Office.Interop.Word

Namespace Domain
    Public Class WordDocument
        Private Function wFile() As Byte()
            Dim obj As Object = My.Resources.kontobestmall
            Return CType(obj, Byte())
        End Function

        Public Function parseGenerateOrder(sEBnr As String, sAklname As String, sPnr As String, sBankName As String, sClearingno As String, sStartdate As String, sEnddate As String, sType As String) As String
            Dim sDomainUser As String = Security.Principal.WindowsIdentity.GetCurrent.Name.Replace("\", "/")
            Dim sADEntry As New DirectoryServices.DirectoryEntry("WinNT://" & sDomainUser)
            Dim sFullName As String = sADEntry.Properties("FullName").Value
            Dim oWord As word.Application
            oWord = CreateObject("Word.Application")

            Dim myTempFile As String = IO.Path.GetTempPath & "\mytemp.dotx"
            My.Computer.FileSystem.WriteAllBytes(myTempFile, wFile, False)

            With oWord
                Stop
                .Visible = True
                .Documents.Add(myTempFile)
                .ActiveDocument.Range.Font.Bold = True
                .Selection.TypeText("Begäran om uppgifter enligt 11 § lag (2004: 297) om bank- och finansieringsrörelse")
                .Selection.TypeParagraph()
                .Selection.Font.Bold = False
                .Selection.TypeParagraph()
                .Selection.TypeParagraph()
                .Selection.TypeText("I pågående förundersökning " & UCase(sEBnr) & " begär åklagare att uppgifter enligt 11 § lag (2004:297) om bank- och finansieringsrörelse om enskildes förhållanden lämnas ut enligt följande:")
                .Selection.TypeParagraph()
                .Selection.TypeParagraph()
                If sType = "Engagemangsförfrågan" Then
                    .Selection.TypeText("Engagemangsförfrågan bla bla")
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

                If sType = "Kontotecknarförfrågan" Then
                    .Selection.TypeText("Begäran om kontotecknarförfrågan bla bla")
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

                If sType = "Förenklat Kontoutdrag" Then
                    .Selection.TypeText("Begäran om förenklat kontoutdrag bla bla")
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
            Return myTempFile
        End Function
    End Class
End Namespace



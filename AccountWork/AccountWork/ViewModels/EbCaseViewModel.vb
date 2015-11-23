Imports System.ComponentModel
Imports System.Text.RegularExpressions

Namespace Domain
    Public Class EbCaseViewModel
        Inherits BaseViewModel
        Implements IDataErrorInfo

        Public Property EbNumber As String
        Public Property Prosecutor As String

        Private ReadOnly EbRegex As New Regex("^(EB)[- ]*([\d]+)[- ]*([\d]{2})$", RegexOptions.IgnoreCase)
        Private Errors As New Dictionary(Of String, String)

        Public ReadOnly Property IsValid As Boolean
            Get
                Return Errors.Count = 0
            End Get
        End Property

        Private Function ValidateEbNumber() As String
            If (String.IsNullOrEmpty(EbNumber)) Then
                Return "Ange ett EB-nummer för ärendet"
            End If
            Dim match As Match = EbRegex.Match(EbNumber)
            If Not match.Success Then
                Return "Angivet EB-nummer verkar inte vara giltigt. Ange numret på formatet ''EB 1234-56''"
            End If
            Return String.Empty
        End Function

        Private Function ValidateProsecutor() As String
            If (String.IsNullOrEmpty(Prosecutor)) Then
                Return "Ange åklagarens namn"
            End If
            Return String.Empty
        End Function

        ' Parses and normalizes the EbNumber property to the form 'EB 1234-56'
        Public Sub NormalizeEbNumber()
            If (Not String.IsNullOrEmpty(EbNumber)) Then
                Dim OriginalEbNumber = EbNumber
                EbNumber = UCase(EbRegex.Replace(EbNumber, "$1 $2-$3"))
                If (Not EbNumber = OriginalEbNumber) Then
                    OnPropertyChanged("EbNumber")
                End If
            End If
        End Sub

        ' Search database for the prosecutor for the current EB number
        Public Sub SearchProsecutors()
            Dim Query = From X In Db.Requests
                        Select X
                        Where X.EbNumber = EbNumber
                        Order By X.Timestamp Descending
            Dim Req As Request = Query.FirstOrDefault()
            If (Not Req Is Nothing) Then
                Prosecutor = Req.Prosecutor
                OnPropertyChanged("Prosecutor")
            End If
        End Sub

#Region "IDataErrorInfo"
        Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
            Get
                Dim ErrorMessage As String = String.Empty
                For Each err As KeyValuePair(Of String, String) In Errors
                    ErrorMessage &= err.Value & Environment.NewLine
                Next
                Return ErrorMessage.Trim()
            End Get
        End Property

        Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
            Get
                ' Validate input
                Dim validationResult As String = String.Empty
                Select Case columnName
                    Case "EbNumber"
                        validationResult = ValidateEbNumber()
                        Exit Select
                    Case "Prosecutor"
                        validationResult = ValidateProsecutor()
                        Exit Select
                    Case Else
                        Throw New ApplicationException("Unknown Property being validated on EbCase.")
                End Select
                ' Update error dictionary
                If (validationResult = String.Empty) Then
                    If (Errors.ContainsKey(columnName)) Then
                        Errors.Remove(columnName)
                    End If
                Else
                    If (Errors.ContainsKey(columnName)) Then
                        Errors(columnName) = validationResult
                    Else
                        Errors.Add(columnName, validationResult)
                    End If
                End If
                OnPropertyChanged("Error")
                OnPropertyChanged("IsValid")
                VMMediator.NotifyColleagues(MediatorMessages.FormValidationStatusChanged,
                                            New Message("Validation status changed in form."))
                Return validationResult
            End Get
        End Property
#End Region
    End Class
End Namespace

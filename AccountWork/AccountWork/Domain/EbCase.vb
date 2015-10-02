Imports System.ComponentModel
Imports System.Text.RegularExpressions

Namespace Domain
    Public Class EbCase
        Implements INotifyPropertyChanged
        Implements IDataErrorInfo

        Public Property EbNumber As String
        Public Property Prosecutor As String

        Private ReadOnly EbRegex As Regex = New Regex("^(EB)[- ]*([\d]+)[- ]*([\d]{2})$", RegexOptions.IgnoreCase)

        Private Function ValidateEbNumber() As String
            If (String.IsNullOrEmpty(EbNumber)) Then
                Return "Du måste ange ett EB-nummer för ärendet"
            End If
            Dim match As Match = EbRegex.Match(EbNumber)
            If Not match.Success Then
                Return "Angivet EB-nummer verkar inte vara giltigt. Ange numret på formatet ''EB 1234-56''"
            End If
            Return ""
        End Function

        Private Function ValidateProsecutor() As String
            If (String.IsNullOrEmpty(Prosecutor)) Then
                Return "Du måste ange åklagarens namn"
            End If
            Return ""
        End Function

        ' Parses and normalizes the EbNumber property to the form 'EB 1234-56'
        Public Sub NormalizeEbNumber()
            Dim OriginalEbNumber = EbNumber
            EbNumber = UCase(EbRegex.Replace(EbNumber, "$1 $2-$3"))
            If (Not EbNumber = OriginalEbNumber) Then
                OnPropertyChanged("EbNumber")
            End If
        End Sub

#Region "IDataErrorInfo"
        Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
            Get
                Dim validationResult As String = Nothing
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
                Return validationResult
            End Get
        End Property
#End Region

#Region "INotifyPropertyChanged"

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
            If Me.PropertyChangedEvent IsNot Nothing Then
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
            End If
        End Sub

#End Region
    End Class
End Namespace

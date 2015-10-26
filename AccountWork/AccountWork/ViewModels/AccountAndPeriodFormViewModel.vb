Imports System.ComponentModel
Imports AccountWork.Domain

Public Class AccountAndPeriodFormViewModel
    Inherits BaseViewModel
    Implements IDataErrorInfo

#Region "Properties"
    Public Property Bank As ClearingNumber
    Private _PeriodStartDate As Date = Date.Today
    Private _PeriodEndDate As Date = Date.Today
    Public Property PeriodStartDate As Date
        Get
            Return _PeriodStartDate
        End Get
        Set(value As Date)
            _PeriodStartDate = value
            OnPropertyChanged("PeriodStartDate")
            OnPropertyChanged("PeriodEndDate")
        End Set
    End Property
    Public Property PeriodEndDate As Date
        Get
            Return _PeriodEndDate
        End Get
        Set(value As Date)
            _PeriodEndDate = value
            OnPropertyChanged("PeriodStartDate")
            OnPropertyChanged("PeriodEndDate")
        End Set
    End Property
    Public Property Errors As New Dictionary(Of String, String)
#End Region

#Region "ValidationRules"
    Public ReadOnly Property IsValid As Boolean
        Get
            Return Errors.Count = 0
        End Get
    End Property

    Private Function ValidatePeriod() As String
        Dim result = Date.Compare(PeriodStartDate, PeriodEndDate)
        If result > 0 Then
            Return "Slutdatum måste vara efter startdatum"
        End If
        Return String.Empty
    End Function

    Private Function ValidateBankChoice() As String
        If (Bank Is Nothing) Then
            Return "Ange ett giltigt kontonummer."
        End If
        Return String.Empty
    End Function
#End Region

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
            Dim errorKey = columnName
            Dim validationResult As String = String.Empty
            Select Case columnName
                Case "Bank"
                    validationResult = ValidateBankChoice()
                    Exit Select
                Case "PeriodStartDate"
                    validationResult = ValidatePeriod()
                    errorKey = "Period"
                    Exit Select
                Case "PeriodEndDate"
                    validationResult = ValidatePeriod()
                    errorKey = "Period"
                    Exit Select
                Case "Error"
                    Return String.Empty
                Case Else
                    Throw New ApplicationException("Unknown Property being validated on AccountAndPeriodFormViewModel.")
            End Select
            ' Update error dictionary
            If (validationResult = String.Empty) Then
                If (Errors.ContainsKey(errorKey)) Then
                    Errors.Remove(errorKey)
                End If
            Else
                If (Errors.ContainsKey(errorKey)) Then
                    Errors(errorKey) = validationResult
                Else
                    Errors.Add(errorKey, validationResult)
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

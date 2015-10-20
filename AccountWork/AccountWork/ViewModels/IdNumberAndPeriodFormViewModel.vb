Imports System.ComponentModel
Imports AccountWork.Domain

Public Class IdNumberAndPeriodFormViewModel
    Inherits BaseViewModel
    Implements IDataErrorInfo

    Public Property BankFinderVM As BankFinderViewModel
    Public Property AllBanks As Boolean = False
    Public Property Bank As ClearingNumber
    Public Property IdNumber As String
    Public Property Name As String
    Public Property PeriodStartDate As Date = Date.Now
    Public Property PeriodEndDate As Date = Date.Now
    Public Property Errors As New Dictionary(Of String, String)

    Public ReadOnly Property IsValid As Boolean
        Get
            Return Errors.Count = 0
        End Get
    End Property

    Private Function ValidateIdNumber() As String
        If (String.IsNullOrEmpty(IdNumber)) Then
            Return "Ange ett 10-siffrigt personnummer utan bindestreck."
        End If
        Return String.Empty
    End Function

    Private Function ValidatePeriod() As String
        Return String.Empty
    End Function

    Private Function ValidateBankChoice() As String
        If (AllBanks = False And Bank Is Nothing) Then
            Return "Ange en bank eller kryssa i checkboxen för att fråga samtliga banker."
        End If
        Return String.Empty
    End Function

#Region "IDataErrorInfo"
    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            ' Validate input
            Dim validationResult As String = String.Empty
            Select Case columnName
                Case "Bank"
                    validationResult = ValidateBankChoice()
                    Exit Select
                Case "AllBanks"
                    validationResult = ValidateBankChoice()
                    Exit Select
                Case "IdNumber"
                    validationResult = ValidateIdNumber()
                    Exit Select
                Case "PeriodStartDate"
                    validationResult = ValidatePeriod()
                    Exit Select
                Case "PeriodEndDate"
                    validationResult = ValidatePeriod()
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
            Return validationResult
        End Get
    End Property

    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Dim ErrorMessage As String = String.Empty
            For Each err As KeyValuePair(Of String, String) In Errors
                ErrorMessage &= err.Value & Environment.NewLine
            Next
            Return ErrorMessage
        End Get
    End Property
#End Region
End Class

Imports System.ComponentModel
Imports AccountWork.Domain

Public Class IdNumberAndPeriodFormViewModel
    Inherits BaseViewModel
    Implements IDataErrorInfo

    Public Property BankFinderVM As BankFinderViewModel
    Public Property CurrentCase As EbCase
    Public Property AllBanks As Boolean = False
    Public Property Bank As ClearingNumber
    Public Property IdNumber As String
    Public Property Name As String
    Public Property PeriodStartDate As Date = DateTime.Now
    Public Property PeriodEndDate As Date = DateTime.Now
    Public Property Errors As New Dictionary(Of String, String)

    Public Sub New()
        CurrentCase = New EbCase()
    End Sub

    Public ReadOnly Property IsValid As Boolean
        Get
            Return Errors.Count = 0 And CurrentCase.IsValid
        End Get
    End Property

    Private Function ValidateIdNumber() As String
        Return String.Empty
    End Function

    Private Function ValidatePeriodStartDate() As String
        Return String.Empty
    End Function

    Private Function ValidatePeriodEndDate() As String
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
            Dim [error] As String = TryCast(CurrentCase, IDataErrorInfo)(columnName)
            CommandManager.InvalidateRequerySuggested()
            Return [error]
        End Get
    End Property

    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Return TryCast(CurrentCase, IDataErrorInfo).[Error]
        End Get
    End Property
#End Region
End Class

Imports System.ComponentModel
Imports AccountWork.Domain

Public Class EngagementFormViewModel
    Inherits BaseViewModel
    Implements IDataErrorInfo

    Public Property CurrentCase As EbCase
    Public Property Errors As New Dictionary(Of String, String)

    Public Sub New()
        CurrentCase = New EbCase()
    End Sub

    Public ReadOnly Property IsValid As Boolean
        Get
            Return Errors.Count = 0 And CurrentCase.IsValid
        End Get
    End Property

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

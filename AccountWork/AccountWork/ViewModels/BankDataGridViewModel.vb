﻿Imports System.Collections.ObjectModel
Imports System.Data.Entity
Imports AccountWork.Domain

Public Class BankDataGridViewModel
    Inherits BaseViewModel

    Public Property ClearingNumbers As ObservableCollection(Of ClearingNumber)

    Public Property HasChanges() As Boolean
        Get
            Return Db.ChangeTracker.HasChanges()
        End Get
        Set(value As Boolean)
            OnPropertyChanged("HasChanges")
        End Set
    End Property

    Public ReadOnly Property EditButtonsVisibility As String
        Get
            Dim UserId As String = Utils.GetUserName()
            Dim PermittedUsers As List(Of String) = My.Resources.Superusers.Split(";").ToList()
            If (PermittedUsers.Contains(UserId)) Then
                Return "Visible"
            End If
            Return "Hidden"
        End Get
    End Property

    Public Sub New()
        ClearingNumbers = New ObservableCollection(Of ClearingNumber)(Db.ClearingNumbers.ToList())
    End Sub

    Public Sub Reset()
        Db.Dispose()
        ClearingNumbers = Nothing
        Db = New AccountWorkDbContext()
        ClearingNumbers = New ObservableCollection(Of ClearingNumber)(Db.ClearingNumbers.ToList())
        OnPropertyChanged("ClearingNumbers")
        OnPropertyChanged("HasChanges")
    End Sub

    Public Sub Add(row As ClearingNumber)
        ClearingNumbers.Add(row)
        Db.ClearingNumbers.Add(row)
        OnPropertyChanged("HasChanges")
    End Sub

    Public Sub Remove(row As ClearingNumber)
        ClearingNumbers.Remove(row)
        If (Not Db.Entry(row).State = EntityState.Detached) Then
            Db.ClearingNumbers.Remove(row)
        End If
        OnPropertyChanged("HasChanges")
    End Sub

    Public Sub SaveChanges()
        Db.SaveChanges()
        OnPropertyChanged("HasChanges")
        VMMediator.NotifyColleagues(MediatorMessages.ClearingNumbersUpdated,
                                    New Message("Clearing numbers updated from bank data grid."))
    End Sub
End Class

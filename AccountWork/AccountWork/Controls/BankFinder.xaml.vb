Imports System.ComponentModel
Imports AccountWork.Domain

Public Class BankFinder
    Inherits BaseControl

    Public Shared ReadOnly BankProperty = DependencyProperty.
        Register("Bank", GetType(ClearingNumber), GetType(BankFinder),
                 New UIPropertyMetadata())

    Public Property Bank() As ClearingNumber
        Get
            Return TryCast(GetValue(BankProperty), ClearingNumber)
        End Get
        Set(value As ClearingNumber)
            SetValue(BankProperty, value)
        End Set
    End Property

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim test = Me
        layoutRoot.DataContext = New BankFinderViewModel()
    End Sub

    Public Sub Enable()
        IsEnabled = True
        bankComboBox.IsEnabled = True
        clearingTextBox.IsEnabled = True
    End Sub

    Public Sub Disable()
        bankComboBox.SelectedIndex = -1
        clearingTextBox.Text = ""
        bankComboBox.IsEnabled = False
        clearingTextBox.IsEnabled = False
        IsEnabled = False
    End Sub

    Private Sub clearingTextBox_TextChanged(sender As Object, e As TextChangedEventArgs)
        Dim textBox As TextBox = sender

        If (textBox.Text.Length >= 4) Then
            Dim txt = textBox.Text.Substring(0, 4)
            Dim number As Integer
            If (Integer.TryParse(txt, number)) Then
                Using Db = New AccountWorkDbContext()
                    Dim Query = From X In Db.ClearingNumbers
                                Select X
                                Where X.ClearingNumberIntervalStart = number _
                                    And X.ClearingNumberIntervalEnd Is Nothing
                    Dim Item As ClearingNumber = Query.SingleOrDefault()

                    If (Item Is Nothing) Then
                        Query = From X In Db.ClearingNumbers
                                Select X
                                Where number >= X.ClearingNumberIntervalStart _
                                    And number <= X.ClearingNumberIntervalEnd
                                Order By X.ClearingNumberIntervalStart Descending
                        Item = Query.FirstOrDefault()
                    End If

                    If (Not Item Is Nothing) Then
                        bankComboBox.SelectedValue = Item.Name
                        Bank = Item
                    End If
                End Using
            Else
                bankComboBox.SelectedIndex = -1
            End If
        Else
            bankComboBox.SelectedIndex = -1
        End If
    End Sub
End Class

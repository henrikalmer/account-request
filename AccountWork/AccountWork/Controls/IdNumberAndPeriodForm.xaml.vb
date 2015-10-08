Imports AccountWork.Domain

Public Class IdNumberAndPeriodForm
    Inherits BaseControl

    Public Shared ReadOnly BankProperty As DependencyProperty = DependencyProperty.
        Register("Bank", GetType(ClearingNumber), GetType(IdNumberAndPeriodForm),
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
        Dim B As New Binding()
        B.Source = bankFinder
        B.Path = New PropertyPath("Bank")
        'B.Mode = BindingMode.OneWay
        B.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        'BindingOperations.SetBinding(Testing12, ContentProperty, B)
        BindingOperations.SetBinding(Me, BankProperty, B)
    End Sub

    Private Sub allBanksCheckbox_Click(sender As Object, e As RoutedEventArgs) Handles allBanksCheckbox.Click
        Select Case allBanksCheckbox.IsChecked
            Case False
                bankFinder.Enable()
            Case True
                bankFinder.Disable()
        End Select
    End Sub
End Class

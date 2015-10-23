Imports AccountWork.Domain

Public Class IdNumberAndPeriodForm
    Inherits BaseControl

    Public Shared ReadOnly ErrorMessageProperty = DependencyProperty.
        Register("ErrorMessage", GetType(String), GetType(IdNumberAndPeriodForm),
                 New UIPropertyMetadata())

    Public Property ErrorMessage() As String
        Get
            Return TryCast(GetValue(ErrorMessageProperty), String)
        End Get
        Set(value As String)
            SetValue(ErrorMessageProperty, value)
        End Set
    End Property

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        layoutRoot.DataContext = New IdNumberAndPeriodFormViewModel()
        Dim B As New Binding()
        B.Source = layoutRoot.DataContext
        B.Path = New PropertyPath("Error")
        BindingOperations.SetBinding(Me, ErrorMessageProperty, B)
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

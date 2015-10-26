Public Class AccountAndPeriodForm
    Inherits BaseControl

    Public Shared ReadOnly ErrorMessageProperty = DependencyProperty.
        Register("ErrorMessage", GetType(String), GetType(AccountAndPeriodForm),
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
        layoutRoot.DataContext = New AccountAndPeriodFormViewModel()
        Dim B As New Binding()
        B.Source = layoutRoot.DataContext
        B.Path = New PropertyPath("Error")
        B.Mode = BindingMode.OneWay
        BindingOperations.SetBinding(Me, ErrorMessageProperty, B)
    End Sub
End Class

Public Class RequestDataGrid
    Inherits BaseControl

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        layoutRoot.DataContext = New RequestDataGridViewModel()
    End Sub
End Class

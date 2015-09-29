Imports AccountWork.Domain

Class MainWindow
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Using Db = New AccountWorkDbContext()
            Dim Query = From X In Db.Banks
                        Order By X.Name
                        Select X

            For Each Item As Bank In Query
                Console.WriteLine(Item.ClearingNumberIntervalStart & ", " & Item.ClearingNumberIntervalEnd & ": " & Item.Name)
            Next
        End Using
    End Sub
End Class

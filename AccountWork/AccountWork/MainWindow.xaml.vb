Imports AccountWork.Domain

Class MainWindow
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Using Db = New AccountWorkDbContext()
            Dim B = New Bank With {
                .Name = "Test Bank",
                .ClearingNumberIntervalStart = 1201,
                .ClearingNumberIntervalEnd = 1299
            }
            Db.Banks.Add(B)
            Db.SaveChanges()

            Dim Query = From X In Db.Banks
                        Order By X.Name
                        Select X

            For Each Item As Bank In Query
                Console.WriteLine(Item.Name)
            Next
        End Using
    End Sub
End Class

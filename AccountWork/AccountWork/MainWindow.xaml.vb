Imports AccountWork.Domain

Class MainWindow
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Using Db = New AccountWorkDbContext()
        '    Dim B = New Bank With {
        '        .Name = "Test Bank",
        '        .ClearingNumberIntervalStart = 1201,
        '        .ClearingNumberIntervalEnd = 1299
        '    }
        '    Db.Banks.Add(B)
        '    Db.SaveChanges()

        'Dim Query = From X In Db.Banks
        '                Order By X.Name
        '                Select X

        '    For Each Item As Bank In Query
        '        Console.WriteLine(Item.Name)
        '    Next
        'End Using
    End Sub



    Private Sub checkBox_Click(sender As Object, e As RoutedEventArgs) Handles checkBox.Click
        Select Case checkBox.IsChecked
            Case False
                bankTextBox.IsEnabled = True
                clearingNumberTextBox.IsEnabled = True
            Case True
                bankTextBox.Text = ""
                clearingNumberTextBox.Text = ""
                bankTextBox.IsEnabled = False
                clearingNumberTextBox.IsEnabled = False

        End Select
    End Sub

    Private Sub clearingNumberTextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles clearingNumberTextBox.TextChanged



    End Sub

    Private Sub searchClearing_Click(sender As Object, e As RoutedEventArgs) Handles searchClearing.Click

        bankTextBox.Text = ""
        If Trim(clearingNumberTextBox.Text) <> "" Then

            'do both interval search and distinct search in Db
            Using Db = New AccountWorkDbContext()
                Dim Query = From X In Db.Banks
                            Order By X.Name
                            Select X
                            Where X.ClearingNumber = clearingNumberTextBox.Text Or (clearingNumberTextBox.Text >= X.ClearingNumberIntervalStart And clearingNumberTextBox.Text <= X.ClearingNumberIntervalEnd)

                Try
                    For Each Item As Bank In Query
                        'Console.WriteLine(Item.Name)
                        bankTextBox.Text = Item.Name
                    Next

                Catch ex As Exception
                End Try



            End Using


        End If
    End Sub

    Private Sub checkBox_Checked(sender As Object, e As RoutedEventArgs) Handles checkBox.Checked

    End Sub
End Class

Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports SQLite.CodeFirst

Namespace Domain
    Public Class AccountWorkDbContext
        Inherits DbContext

        Public Property ClearingNumbers As DbSet(Of ClearingNumber)
        Public Property Requests As DbSet(Of Request)

        Public ReadOnly Property AllBankEmails As HashSet(Of String)
            Get
                Dim Emails = From C In ClearingNumbers
                             Where C.MayContact = True
                             Select C.Email
                Return New HashSet(Of String)(Emails.ToList())
            End Get
        End Property

        Public Sub New()
            MyBase.New("AccountWorkDbContext")
        End Sub

        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            MyBase.OnModelCreating(modelBuilder)

            modelBuilder.Conventions.Remove(Of PluralizingTableNameConvention)()
            Dim sqliteConnectionInitializer = New SqliteCreateDatabaseIfNotExists(Of AccountWorkDbContext)(modelBuilder)
            Database.SetInitializer(sqliteConnectionInitializer)
        End Sub
    End Class
End Namespace

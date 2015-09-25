Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports SQLite.CodeFirst

Namespace Domain
    Public Class AccountWorkDbContext
        Inherits DbContext

        Public Property Banks As DbSet(Of Bank)
        Public Property Requests As DbSet(Of Request)

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


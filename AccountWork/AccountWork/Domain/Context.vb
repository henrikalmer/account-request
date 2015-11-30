Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports SQLite.CodeFirst

Namespace Domain
    Public Class AccountWorkDbContext
        Inherits DbContext

        Public Property ClearingNumbers As DbSet(Of ClearingNumber)
        Public Property Requests As DbSet(Of Request)

        Public ReadOnly Property AllBanksWithEmail As List(Of ClearingNumber)
            Get
                Dim BankGroups As IQueryable(Of IEnumerable(Of ClearingNumber)) =
                    From C In ClearingNumbers
                    Where C.MayContact = True
                    Group By C.Email, C.MayContact Into G = Group
                    Select G
                Dim Banks = From BG In BankGroups.ToList() Select BG.First()
                Return New List(Of ClearingNumber)(Banks)
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

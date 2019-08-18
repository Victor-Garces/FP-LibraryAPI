namespace Library.Persistence

open Microsoft.EntityFrameworkCore
open Library.Models

type LibraryContext(options: DbContextOptions<LibraryContext>) =
    inherit DbContext(options)
    
      override __.OnModelCreating modelBuilder = 
          modelBuilder.Entity<Book>().HasKey(fun book -> (book.Id) :> obj) |> ignore
          modelBuilder.Entity<Book>().Property(fun book -> book.Id).ValueGeneratedOnAdd() |> ignore
      
    [<DefaultValue>]
    val mutable books : DbSet<Book>
    member __.Book 
        with get() = __.books 
        and set value = __.books <- value


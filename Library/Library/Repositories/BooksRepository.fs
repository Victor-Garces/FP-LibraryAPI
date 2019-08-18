namespace Library.Repositories

open Library.Persistence

type BooksRepository (context : LibraryContext) =

    member __.GetAll () = context.Book
    member __.GetBookById id = context.Book |> Seq.tryFind (fun f -> f.Id = id)
    


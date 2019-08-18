namespace Library.Repositories

open Library.Persistence

type PagesRepository (context : LibraryContext) =

    member __.GetAll () = context.Page |> Seq.toList
    member __.GetPageById id = context.Page |> Seq.tryFind (fun f -> f.Id = id)
    member __.GetAllByBookId bookId = context.Page |> Seq.filter (fun f -> f.BookId = bookId) |> Seq.toList
    


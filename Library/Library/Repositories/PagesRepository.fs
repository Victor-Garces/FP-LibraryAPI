namespace Library.Repositories

open Library.Persistence

type PagesRepository (context : LibraryContext) =

    member __.GetAll () = context.Page
    member __.GetPageById id = context.Page |> Seq.tryFind (fun f -> f.Id = id)
    


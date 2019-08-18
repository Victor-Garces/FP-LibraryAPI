namespace Library.Services

open Library.Repositories
open Library.Models

type IPagesService = 
    abstract member GetAll : unit -> List<Page>
    abstract member GetById : int64 -> Page

type PageService (pagesRepository: PagesRepository) = 
    interface IPagesService with
        member __.GetAll () = pagesRepository.GetAll() |> Seq.toList
        member __.GetById (id : int64) =
            let result = pagesRepository.GetPageById(id)
            match result with
            | Some value -> value
            | None -> 
                { 
                    Id = int64(0)
                    Content = ""
                    BookId = int64(0)
                }
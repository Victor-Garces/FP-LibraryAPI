namespace Library.Services

open Library.Repositories
open Library.Models

type IPagesService = 
    abstract member GetAll : unit -> List<Page>
    abstract member GetById : int64 -> Page
    abstract member GetAllByBookId : int64 -> List<Page>

type PageService (pagesRepository: PagesRepository) = 
    interface IPagesService with
        member __.GetAll () = pagesRepository.GetAll()
        member __.GetAllByBookId (id : int64) = pagesRepository.GetAllByBookId(id)
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
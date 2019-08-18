namespace Library.Services

open Library.Repositories
open Library.Models

type IBooksService = 
    abstract member GetAll : unit -> List<Book>
    abstract member GetById : int64 -> Book

type BookService (booksRepository: BooksRepository) = 
    interface IBooksService with
        member __.GetAll () = booksRepository.GetAll()
        member __.GetById (id : int64) =
            let result = booksRepository.GetBookById(id)
            match result with
            | Some value -> value
            | None -> 
                { 
                    Id = int64(0)
                    Title = ""
                    Author = ""
                }
namespace Library.Controllers

open Microsoft.AspNetCore.Mvc
open Library.Services
open Library.Models

[<Route("api/books")>]
[<ApiController>]
type BooksController (booksServices : IBooksService) =
    inherit ControllerBase ()

    [<HttpGet>]
    member __.Get() =
        let books = booksServices.GetAll()
        ActionResult<List<Book>>(books)

    [<HttpGet("{id}")>]
    member __.Get(id : int64) =
        let book = booksServices.GetById(id)
        ActionResult<Book>(book)


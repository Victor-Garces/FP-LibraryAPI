namespace Library.Controllers

open Microsoft.AspNetCore.Mvc
open Library.Services
open Library.Models

[<Route("api/books")>]
[<ApiController>]
type BooksController (booksServices : IBooksService, pagesServices : IPagesService) =
    inherit ControllerBase ()

    [<HttpGet>]
    member __.Get() =
        let books = booksServices.GetAll()
        ActionResult<List<Book>>(books)

    [<HttpGet("{id}")>]
    member __.GetBook(id : int64) =
        let book = booksServices.GetById(id)
        ActionResult<Book>(book)

    [<HttpGet("{id}/pages/{pageId}")>]
    member __.GetPage(pageId : int64) =
        let page = pagesServices.GetById(pageId)
        ActionResult<Page>(page)


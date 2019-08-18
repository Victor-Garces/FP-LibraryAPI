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
        let newPage = { page with Content = __.GetPageHtmlFormat (page.Content) }
        ActionResult<Page>(newPage)

    member __.GetPageHtmlFormat (content : string) =
        let header = "<html><head></head><body>"
        let paragrahps = content.Split("\r\n") |> Seq.filter (fun a -> a <> "") |> Seq.map (fun a -> "<p>"  + a + "<p/>") |> String.concat ""
        let footer = "</body></html>"
        header + paragrahps + footer


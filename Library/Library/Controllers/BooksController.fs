namespace Library.Controllers

open Microsoft.AspNetCore.Mvc
open Library.Services
open Library.Models
open Library.BookDTO

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
        let pagesIds = pagesServices.GetAllByBookId(book.Id) |> Seq.map (fun page -> page.Id) |> Seq.toList
        let newBook = __.BuildBookDTOP(book.Id, book.Title, book.Author, pagesIds)
        ActionResult<BookDTO>(newBook)

    [<HttpGet("{id}/pages/{pageId}")>]
    member __.GetPage(pageId : int64) =
        let page = pagesServices.GetById(pageId)
        let newPage = { page with Content = __.GetPageHtmlFormat (page.Content) }
        ActionResult<Page>(newPage)

    member __.GetPageHtmlFormat (content : string) : string =
        let header = "<html><head></head><body>"
        let paragrahps = content.Split("\r\n") |> Seq.filter (fun a -> a <> "") |> Seq.map (fun a -> "<p>"  + a + "<p/>") |> String.concat ""
        let footer = "</body></html>"
        header + paragrahps + footer

    member __.BuildBookDTOP (id : int64, title : string, author : string, pages : int64 list) : BookDTO =
        { Id = id; Title = title; Author = author; Pages = pages }


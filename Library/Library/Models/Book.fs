namespace Library.Models

[<CLIMutable>]
type Book = 
    {
        Id : int64
        Title : string
        Author : string
        Pages : Page list
    }
and [<CLIMutable>] Page = 
    {
        Id : int64
        Content : string
        BookId : int64
    }

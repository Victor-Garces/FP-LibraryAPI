namespace Library

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Library.Services
open Library.Persistence
open Microsoft.EntityFrameworkCore
open Library.Repositories
open Newtonsoft.Json

type Startup private () =
    new (configuration: IConfiguration) as __ =
        Startup() then
        __.Configuration <- configuration

    member __.ConfigureServices(services: IServiceCollection) =
        services.AddDbContext<LibraryContext> (fun (options : DbContextOptionsBuilder) ->  
            options.UseSqlServer(__.Configuration.GetConnectionString("LibraryDatabase")) |> ignore |> ignore |> ignore) |> ignore

        __.BuildServices (services)
        __.BuildRepositories (services)

        services.AddMvc()
                .AddJsonOptions(fun options -> options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore |> ignore) 
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1) |> ignore

    member __.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
        else
            app.UseHsts() |> ignore

        app.UseHttpsRedirection() |> ignore
        app.UseMvc() |> ignore

    member val Configuration : IConfiguration = null with get, set

    member __.BuildServices (services : IServiceCollection) =
        services.AddScoped<IBooksService, BookService>() |> ignore
        services.AddScoped<IPagesService, PageService>() |> ignore

    member __.BuildRepositories (services : IServiceCollection) =
        services.AddScoped<BooksRepository, BooksRepository>() |> ignore
        services.AddScoped<PagesRepository, PagesRepository>() |> ignore
        

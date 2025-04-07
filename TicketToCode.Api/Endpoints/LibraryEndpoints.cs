using Microsoft.AspNetCore.Http;
using TicketToCode.Api.Services;
using TicketToCode.Core.Models;

namespace TicketToCode.Api.Endpoints
{

public  class LibraryEndpoints : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/books", (ILibraryService service) => Results.Ok(service.GetAllBooks()));
        app.MapGet("/books/{id:int}", (int id, ILibraryService service) =>
            service.GetBookById(id) is Book book ? Results.Ok(book) : Results.NotFound());

        app.MapPost("/books", (Book book, ILibraryService service) => Results.Ok(service.AddBook(book)));

        app.MapDelete("/books/{id:int}", (int id, ILibraryService service) =>
            service.DeleteBook(id) ? Results.NoContent() : Results.NotFound());

        app.MapPost("/borrow/{bookId:int}/{userId:int}", (int bookId, int userId, ILibraryService service) =>
            service.BorrowBook(bookId, userId) is Loan loan ? Results.Ok(loan) : Results.BadRequest("Book is not available or user is invalid"));

        app.MapPost("/return/{loanId:int}", (int loanId, ILibraryService service) =>
            service.ReturnBook(loanId) ? Results.Ok("Book returned successfully") : Results.BadRequest("Invalid loan ID"));



            app.MapGet("/users/{userId:int}/loans", (int userId, ILibraryService service) =>
            Results.Ok(service.GetUserLoans(userId)));

        app.MapGet("/stats/most-loaned-books", (ILibraryService service) =>
            Results.Ok(service.GetMostLoanedBooks()));

        app.MapPut("/books/{id:int}", (int id, Book updatedBook, ILibraryService service) =>
    service.UpdateBook(id, updatedBook) is Book book ? Results.Ok(book) : Results.NotFound());

    }
}}
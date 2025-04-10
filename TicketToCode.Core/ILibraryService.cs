using TicketToCode.Core.Models;
using System.Collections.Generic;

namespace TicketToCode.Core.Services
{
    public interface ILibraryService
    {
        List<Book> GetAllBooks();
        Book? GetBookById(int id);
        Book AddBook(Book book);
        bool DeleteBook(int id);
        Loan? BorrowBook(int bookId, int userId);
        bool ReturnBook(int loanId);
        Book? UpdateBook(int id, Book updatedBook);

        // Extra funktioner
        List<Book> GetMostLoanedBooks();
        List<Loan> GetUserLoans(int userId);
    }
}

using TicketToCode.Core.Data;
using TicketToCode.Core.Models;

namespace TicketToCode.Api.Services;

public interface ILibraryService
{
    List<Book> GetAllBooks();
    Book? GetBookById(int id);
    Book AddBook(Book book);
    bool DeleteBook(int id);
    Loan? BorrowBook(int bookId, int userId);
    bool ReturnBook(int loanId);

    // Metoder för de nya funktionerna
    List<Book> GetMostLoanedBooks();
    List<Loan> GetUserLoans(int userId);
}

public class LibraryService : ILibraryService
{
    private readonly IDatabase _database;

    public LibraryService(IDatabase database)
    {
        _database = database;
    }

    public List<Book> GetAllBooks() => _database.Books;

    public Book? GetBookById(int id) => _database.Books.FirstOrDefault(b => b.Id == id);

    public Book AddBook(Book book)
    {
        book.Id = _database.Books.Count + 1;
        _database.Books.Add(book);
        return book;
    }

    public bool DeleteBook(int id)
    {
        var book = GetBookById(id);
        if (book == null) return false;
        _database.Books.Remove(book);
        return true;
    }

    public Loan? BorrowBook(int bookId, int userId)
    {
        var book = GetBookById(bookId);
        if (book == null || !book.IsAvailable) return null;

        book.IsAvailable = false;
        var loan = new Loan { Id = _database.Loans.Count + 1, BookId = bookId, UserId = userId };
        _database.Loans.Add(loan);
        return loan;
    }

    public bool ReturnBook(int loanId)
    {
        var loan = _database.Loans.FirstOrDefault(l => l.Id == loanId);
        if (loan == null || loan.ReturnDate != null) return false;

        loan.ReturnDate = DateTime.UtcNow;
        var book = GetBookById(loan.BookId);
        if (book != null) book.IsAvailable = true;
        return true;
    }

    // Metod för att hämta de mest utlånade böckerna
    public List<Book> GetMostLoanedBooks()
    {
        var mostLoanedBooks = _database.Loans
            .GroupBy(l => l.BookId)
            .OrderByDescending(g => g.Count())
            .Take(5)  //  de fem mest utlånade böckerna
            .Select(g => _database.Books.FirstOrDefault(b => b.Id == g.Key))
            .ToList();

        return mostLoanedBooks;
    }

    public List<Loan> GetUserLoans(int userId)
    {
        return _database.Loans
            .Where(l => l.UserId == userId && l.ReturnDate == null)  // Lån som ej är återlämnade
            .ToList();
    }
}


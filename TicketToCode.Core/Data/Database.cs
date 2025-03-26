using TicketToCode.Core.Models;

namespace TicketToCode.Core.Data;

public interface IDatabase
{
    List<Event> Events { get; set; }
    List<User> Users { get; set; }
    List<Loan> Loans { get; set; }
    List<Book> Books { get; set; } 
}

public class Database : IDatabase
{
    public List<Event> Events { get; set; } = new List<Event>();
    public List<User> Users { get; set; } = new List<User>();
    public List<Loan> Loans { get; set; } = new List<Loan>();
    public List<Book> Books { get; set; } = new List<Book>(); 
}


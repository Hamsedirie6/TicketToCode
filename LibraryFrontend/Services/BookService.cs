using System.Net.Http.Json;
using LibraryFrontend.Models;

public class BookService
{
    private readonly HttpClient _http;

    public BookService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Book>> GetAllBooks()
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<Book>>("books");
            Console.WriteLine($"[BookService] Fick tillbaka {result?.Count ?? 0} böcker.");
            return result ?? new List<Book>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BookService] FEL: {ex.Message}");
            throw; // kasta vidare för att Blazor ska visa fel i UI
        }
    }

    public async Task<Book?> GetBookById(int id)
    {
        try
        {
            return await _http.GetFromJsonAsync<Book>($"books/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BookService] Fel vid hämtning av bok med ID {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> AddBook(Book book)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("books", book);
            Console.WriteLine($"[BookService] POST-status: {response.StatusCode}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BookService] Fel vid tillägg av bok: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateBook(int id, Book book)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"books/{id}", book);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BookService] Fel vid uppdatering av bok med ID {id}: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteBook(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"books/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BookService] Fel vid radering av bok med ID {id}: {ex.Message}");
            return false;
        }
    }
}

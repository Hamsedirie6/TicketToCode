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
        return await _http.GetFromJsonAsync<List<Book>>("books") ?? new List<Book>();
    }

    public async Task<Book?> GetBookById(int id)
    {
        return await _http.GetFromJsonAsync<Book>($"books/{id}");
    }

    public async Task<bool> AddBook(Book book)
    {
        var response = await _http.PostAsJsonAsync("books", book);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateBook(int id, Book book)
    {
        var response = await _http.PutAsJsonAsync($"books/{id}", book);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteBook(int id)
    {
        var response = await _http.DeleteAsync($"books/{id}");
        return response.IsSuccessStatusCode;
    }
}
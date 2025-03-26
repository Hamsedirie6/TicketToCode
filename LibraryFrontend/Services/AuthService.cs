using System.Net.Http.Json;
using LibraryFrontend.Models;

public class AuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> Login(string username, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/login", new { Username = username, Password = password });
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Register(string username, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/register", new { Username = username, Password = password });
        return response.IsSuccessStatusCode;
    }
}

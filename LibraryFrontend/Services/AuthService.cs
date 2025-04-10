using System.Net.Http.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using LibraryFrontend.Models;

public class AuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public bool IsLoggedIn { get; private set; } = false;
    public string? Username { get; private set; }
    public string? Role { get; private set; }

    public async Task<bool> Login(string username, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/login", new { Username = username, Password = password });

        if (response.IsSuccessStatusCode)
        {
            // Försök hämta roll från API:t om det finns i svaret
            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            Username = username;
            Role = result?.Role ?? "User";
            IsLoggedIn = true;
            return true;
        }

        return false;
    }

    public async Task<bool> Register(string username, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/register", new { Username = username, Password = password });
        return response.IsSuccessStatusCode;
    }

    public void Logout()
    {
        IsLoggedIn = false;
        Username = null;
        Role = null;
    }
}

public class AuthResponse
{
    public string Role { get; set; } = "User";
}


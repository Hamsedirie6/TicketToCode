using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LibraryFrontend;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient + services
builder.Services.AddHttpClient<BookService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7206/");
});
builder.Services.AddScoped<BookService>(); // ← Lägg till detta!

builder.Services.AddHttpClient<AuthService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7206/");
});

builder.Services.AddHttpClient<BookService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7206/");
});


builder.Services.AddScoped<AuthService>(); // ← Lägg till detta också!



await builder.Build().RunAsync();

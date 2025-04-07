using TicketToCode.Client.Components;
using TicketToCode.Client.Services; // 👈 Importera din UserService

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ✅ Lägg till HttpClient för att anropa API:t
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7206/") // <-- Anpassa vid behov
});

// ✅ Lägg till HttpContextAccessor så vi kan läsa cookies
builder.Services.AddHttpContextAccessor();

// ✅ Lägg till vår UserService som läser roll från cookie
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

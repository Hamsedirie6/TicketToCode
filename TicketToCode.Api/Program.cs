using TicketToCode.Api.Endpoints;
using TicketToCode.Api.Services;
using TicketToCode.Core.Data;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IDatabase, Database>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILibraryService, LibraryService>();

// ✅ Lägg till CORS-policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Cookie authentication
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

// ✅ Aktivera CORS före Authentication/Authorization
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapEndpoints<Program>();

app.Run();

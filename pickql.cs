// dotnet add package Microsoft.EntityFrameworkCore.Sqlite

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {

}

// For the Atttributes
app.MapControllers();

app.Run();

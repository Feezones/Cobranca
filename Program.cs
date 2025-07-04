using FitBack.DataBase;
using FitBack.Repositories;
using FitBack.Controllers;

SQLitePCL.Batteries.Init();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Inicializa o banco
DbInitializer.Initialize(connectionString);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var repository = new DividaRepository(connectionString);

var app = builder.Build();

app.UseCors("AllowAll");

// Mapeia os endpoints
app.MapDividaEndpoints(repository);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

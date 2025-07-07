using FitBack.Controllers;
using FitBack.DataBase;
using FitBack.Repositories;
using FitBack.Services;

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

builder.Services.AddSingleton(new UsuarioRepository(connectionString));
builder.Services.AddSingleton(new DividaRepository(connectionString));
builder.Services.AddSingleton(new JwtService("sua-chave-super-secreta-aqui")); // Troque por algo forte


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var repository = new DividaRepository(connectionString);

var app = builder.Build();

app.UseCors("AllowAll");

// Mapeia os endpoints
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

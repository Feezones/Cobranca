using FitBack.Controllers;
using FitBack.DataBase;
using FitBack.DbInit;
using FitBack.Repositories;
using FitBack.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

SQLitePCL.Batteries.Init();

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqliteConnection(connectionString);
});

builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<DividaRepository>();

var jwtKey = "sua-chave-super-secreta-com-32-caracteres!"; // Pode colocar no appsettings se quiser

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSingleton(new JwtService(jwtKey)); // Troque por algo forte


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
    DatabaseInitializer.Initialize(connection);
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

// Mapeia os endpoints
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseDefaultFiles(); // Procura automaticamente por index.html
app.UseStaticFiles();  // Serve arquivos da wwwroot


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

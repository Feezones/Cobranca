using Dapper;
using FitBack.Models;
using Microsoft.Data.Sqlite;

namespace FitBack.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;
        public UsuarioRepository(string connectionString) => _connectionString = connectionString;

        public Usuario? GetByEmail(string email)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuarios WHERE Email = @Email", new { Email = email });
        }

        public void Add(Usuario usuario)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Execute(@"INSERT INTO Usuarios (Nome, Email, SenhaHash) VALUES (@Nome, @Email, @SenhaHash)", usuario);
        }
    }
}

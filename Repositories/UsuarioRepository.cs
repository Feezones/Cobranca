using Dapper;
using FitBack.Models;
using Microsoft.Data.SqlClient;

namespace FitBack.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;
        public UsuarioRepository(string connectionString) => _connectionString = connectionString;

        public Usuario? GetByEmail(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuarios WHERE Email = @Email", new { Email = email });
        }

        public async Task Criar(Usuario usuario)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                @"INSERT INTO Usuarios (Nome, Email, SenhaHash)
              VALUES (@Nome, @Email, @SenhaHash)", usuario);
        }

        public async Task<bool> EmailExiste(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.QueryFirstOrDefaultAsync<Usuario>(
                "SELECT * FROM Usuarios WHERE Email = @Email", new { Email = email });

            return result != null;
        }
    }
}

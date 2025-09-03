using Dapper;
using FitBack.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FitBack.Repositories
{
    public class UsuarioRepository
    {
        private readonly IDbConnection _connectionString;

        public UsuarioRepository(IDbConnection connectionString)
        {
            _connectionString = connectionString;
        }

        public Usuario? GetByEmail(string email)
        {
            return _connectionString.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuario WHERE Email = @Email", new { Email = email });
        }

        public async Task Criar(Usuario usuario)
        {
            await _connectionString.ExecuteAsync(
                @"INSERT INTO Usuario (Nome, Email, SenhaHash)
              VALUES (@Nome, @Email, @SenhaHash)", usuario);
        }

        public async Task<bool> EmailExiste(string email)
        {
            var result = await _connectionString.QueryFirstOrDefaultAsync<Usuario>(
                "SELECT * FROM Usuario WHERE Email = @Email", new { Email = email });

            return result != null;
        }
    }
}

using Dapper;
using FitBack.Models;
using Microsoft.Data.SqlClient;

namespace FitBack.Repositories
{
    public class DividaRepository
    {
        private readonly string _connectionString;

        public DividaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Divida> GetByUser(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Divida>("SELECT * FROM Dividas WHERE UserId = @UserId", new { UserId = userId });
        }

        public void Add(Divida divida)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
            INSERT INTO Dividas 
            (Nome, Origem, ValorTotal, TotalParcelas, ParcelaAtual, ValorParcela, DataPagamento, ProximoVencimento, UserId)
            VALUES
            (@Nome, @Origem, @ValorTotal, @TotalParcelas, @ParcelaAtual, @ValorParcela, @DataPagamento, @ProximoVencimento, @UserId);
        ";
            connection.Execute(query, divida);
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute("DELETE FROM Dividas WHERE Id = @Id", new { Id = id });
        }

        public void Update(Divida divida)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
            UPDATE Dividas SET
            Nome = @Nome,
            Origem = @Origem,
            ValorTotal = @ValorTotal,
            TotalParcelas = @TotalParcelas,
            ParcelaAtual = @ParcelaAtual,
            ValorParcela = @ValorParcela,
            DataPagamento = @DataPagamento,
            ProximoVencimento = @ProximoVencimento
            WHERE Id = @Id;
        ";
            connection.Execute(query, divida);
        }
    }
}

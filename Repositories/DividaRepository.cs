using Dapper;
using FitBack.Models;
using Microsoft.Data.Sqlite;

namespace FitBack.Repositories
{
    public class DividaRepository
    {
        private readonly string _connectionString;

        public DividaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Divida> GetAll()
        {
            using var connection = new SqliteConnection(_connectionString);
            return connection.Query<Divida>("SELECT * FROM Dividas");
        }

        public void Add(Divida divida)
        {
            using var connection = new SqliteConnection(_connectionString);
            var query = @"
            INSERT INTO Dividas 
            (Nome, Origem, ValorTotal, TotalParcelas, ParcelaAtual, ValorParcela, DataPagamento, ProximoVencimento)
            VALUES
            (@Nome, @Origem, @ValorTotal, @TotalParcelas, @ParcelaAtual, @ValorParcela, @DataPagamento, @ProximoVencimento);
        ";
            connection.Execute(query, divida);
        }

        public void Delete(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Execute("DELETE FROM Dividas WHERE Id = @Id", new { Id = id });
        }

        public void Update(Divida divida)
        {
            using var connection = new SqliteConnection(_connectionString);
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

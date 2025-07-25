using Dapper;
using FitBack.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FitBack.Repositories
{
    public class DividaRepository
    {
        private readonly IDbConnection _connectionString;

        public DividaRepository(IDbConnection connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Divida> GetByUser(int userId)
        {
            return _connectionString.Query<Divida>("SELECT * FROM Dividas WHERE UsuarioId = @UserId", new { UserId = userId });
        }

        public void Add(Divida divida)
        {
            var query = @"
            INSERT INTO Dividas 
            (Nome, Origem, ValorTotal, TotalParcelas, ParcelaAtual, ValorParcela, DataPagamento, ProximoVencimento, UserId)
            VALUES
            (@Nome, @Origem, @ValorTotal, @TotalParcelas, @ParcelaAtual, @ValorParcela, @DataPagamento, @ProximoVencimento, @UserId);
        ";
            _connectionString.Execute(query, divida);
        }

        public void Delete(int id)
        {
            _connectionString.Execute("DELETE FROM Dividas WHERE Id = @Id", new { Id = id });
        }

        public void Update(Divida divida)
        {
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
            _connectionString.Execute(query, divida);
        }
    }
}

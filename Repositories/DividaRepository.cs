using Dapper;
using FitBack.Models;
using Microsoft.Data.SqlClient;
using System.Collections;
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

        public async Task<DividaMensal> GetByUser(int userId)
        {
            var dividaMensal = await _connectionString.QueryFirstOrDefaultAsync<DividaMensal>(
                "SELECT * FROM DividaMensal WHERE UserId = @UserId", new { UserId = userId });

            if (dividaMensal != null)
                dividaMensal.DividaDiaria = await GetByMes(dividaMensal.Id);

            return dividaMensal;
        }

        public async Task<List<DividaDiaria>> GetByMes(int mesId)
        {
            var dividaDiaria = await _connectionString.QueryAsync<DividaDiaria>(
                "SELECT * FROM DividaDiaria WHERE IdDividaMensal = @IdDividaMensal", new { IdDividaMensal = mesId });

            return dividaDiaria.ToList();
        }

        public void Add(Divida divida)
        {
            var query = @"
            INSERT INTO Dividas 
            (Nome, Origem, ValorTotal, TotalParcelas, ParcelaAtual, ValorParcela, DataPagamento, ProximoVencimento, UsuarioId)
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

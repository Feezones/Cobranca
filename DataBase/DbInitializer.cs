using Microsoft.Data.Sqlite;

namespace FitBack.DataBase
{
    public static class DbInitializer
    {
        public static void Initialize(string connectionString)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Usuarios (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    Email TEXT NOT NULL UNIQUE,
                    SenhaHash TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Dividas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    Origem TEXT NOT NULL,
                    ValorTotal REAL NOT NULL,
                    TotalParcelas INTEGER NOT NULL,
                    ParcelaAtual INTEGER NOT NULL,
                    ValorParcela REAL NOT NULL,
                    DataPagamento TEXT NOT NULL,
                    ProximoVencimento TEXT NOT NULL,
                    UserId INTEGER NOT NULL
                );
            ";

            command.ExecuteNonQuery();
        }
    }
}

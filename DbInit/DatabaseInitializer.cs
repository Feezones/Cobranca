using Dapper;
using System.Data;

namespace FitBack.DbInit
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IDbConnection connection)
        {
            // Criação das tabelas, se não existirem
            connection.Execute(@"
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
                UsuarioId INTEGER NOT NULL,
                FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
            );
        ");
        }
    }
    }

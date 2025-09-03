-- Criar banco de dados
CREATE DATABASE ControleDividasDB;
GO

USE ControleDividasDB;
GO

-- =====================
-- TABELA USUARIO
-- =====================
CREATE TABLE Usuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    SenhaHash NVARCHAR(255) NOT NULL
);

-- =====================
-- TABELA DIVIDA MENSAL
-- =====================
CREATE TABLE DividaMensal (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Data DATE NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    Entrada DECIMAL(10,2) NOT NULL,
    Saida DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DividaMensal_Usuario FOREIGN KEY (UserId) 
        REFERENCES Usuario(Id)
);

-- =====================
-- TABELA DIVIDA DIARIA
-- =====================
CREATE TABLE DividaDiaria (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdDividaMensal INT NOT NULL,
    Nome NVARCHAR(255) NOT NULL,
    Data DATETIME NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    FormaPagamento NVARCHAR(100) NOT NULL,
    Entrada BIT NOT NULL,
    Saida BIT NOT NULL,
    CONSTRAINT FK_DividaDiaria_DividaMensal FOREIGN KEY (IdDividaMensal) 
        REFERENCES DividaMensal(Id)
);










-- Inserir usuário
INSERT INTO Usuario (Nome, Email, SenhaHash)
VALUES ('Felipe Brag', 'felipe@email.com', 'hash_senha_123');

-- Inserir dívidas mensais (para o usuário criado)
INSERT INTO DividaMensal (UserId, Data, Total, Entrada, Saida)
VALUES 
(1, '2025-09-01', 3000.00, 1500.00, 1500.00),
(1, '2025-10-01', 2500.00, 1000.00, 1500.00);

-- Inserir dívidas diárias (vinculadas à primeira dívida mensal)
INSERT INTO DividaDiaria (IdDividaMensal, Nome, Data, Valor, FormaPagamento, Entrada, Saida)
VALUES
(1, 'Compra Supermercado', '2025-09-03', 350.00, 'Cartão Crédito', 0, 1),
(1, 'Recebimento Freelancer', '2025-09-05', 800.00, 'Pix', 1, 0),
(1, 'Conta de Luz', '2025-09-08', 200.00, 'Débito', 0, 1);

-- Inserir dívidas diárias (vinculadas à segunda dívida mensal)
INSERT INTO DividaDiaria (IdDividaMensal, Nome, Data, Valor, FormaPagamento, Entrada, Saida)
VALUES
(2, 'Compra Roupas', '2025-10-04', 500.00, 'Cartão Crédito', 0, 1),
(2, 'Salário', '2025-10-05', 2000.00, 'Depósito', 1, 0);


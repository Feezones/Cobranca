# 💰 Sistema de Controle Financeiro Pessoal

Este projeto é um sistema web para gerenciamento de dívidas e finanças pessoais. Usuários podem se cadastrar, fazer login e registrar dívidas com controle de parcelas, datas e valores.

## 🧱 Tecnologias Utilizadas

- **Frontend:**
  - HTML5, CSS3 e JavaScript
  - Bootstrap 5.3
- **Backend:**
  - ASP.NET Core Web API
  - Dapper para acesso ao banco
  - SQLite como banco de dados local

## 🔐 Funcionalidades

- Autenticação com JWT
- Cadastro de novos usuários
- Login e logout com armazenamento de token
- Criação de dívidas com:
  - Nome, origem, valor total
  - Parcelamento (total e atual)
  - Valor da parcela
  - Data de pagamento e vencimento
- Consulta das dívidas por usuário autenticado

## 📁 Estrutura do Projeto

ControleFinanceiro/
├── backend/
│ ├── Controllers/
│ │ └── AuthController.cs
│ │ └── DividasController.cs
│ ├── Models/
│ ├── Repository/
│ ├── Program.cs
│ └── appsettings.json
├── frontend/
│ ├── auth/
│ │ └── login.html
│ │ └── cadastro.html
│ │ └── login.js
│ ├── dividas/
│ │ └── index.html
│ │ └── dividas.js
│ └── assets/



## 🚀 Como Executar Localmente

### Backend (ASP.NET Core)
1. Navegue até a pasta `backend`.
2. Abra o terminal e execute:
   ```bash
   dotnet restore
   dotnet run

# TaskManager API

## Descrição

Este projeto consiste em uma API REST para gerenciamento de tarefas (Task Manager), desenvolvida em .NET 8 com ASP.NET Core.

O objetivo é fornecer uma estrutura organizada e escalável para criação, consulta, atualização e remoção de tarefas, além de permitir o gerenciamento de usuários e consultas com filtros e resumos.

A aplicação segue princípios de separação de responsabilidades e boas práticas de arquitetura, incluindo uso de camadas, injeção de dependência e acesso a dados via Entity Framework Core.

---

## Tecnologias Utilizadas

* .NET 8 (ASP.NET Core Web API)
* Entity Framework Core (Code First)
* SQL Server (via Docker)
* FluentValidation
* Swagger / OpenAPI
* xUnit (testes unitários)
* EF Core InMemory (para testes)

---

## Arquitetura do Projeto

A solução está organizada em múltiplas camadas:

```
TaskManager/
├── TaskManager.API              # Controllers, configuração e middleware
├── TaskManager.Application      # Services, DTOs, validações e interfaces
├── TaskManager.Domain           # Entidades e enums
├── TaskManager.Infrastructure   # DbContext, repositórios e migrations
└── TaskManager.Tests            # Testes unitários
```

### Responsabilidades

* **API**: exposição dos endpoints e configuração da aplicação
* **Application**: regras de negócio e orquestração
* **Domain**: modelo de domínio (entidades e enums)
* **Infrastructure**: acesso a dados e persistência
* **Tests**: validação automatizada do comportamento da aplicação

---

## Funcionalidades

### Tarefas

* Criar tarefa
* Listar tarefas
* Buscar tarefa por ID
* Atualizar tarefa
* Remover tarefa

Cada tarefa possui:

* Id
* Título
* Descrição
* Status (Pendente, EmProgresso, Concluida)
* Prioridade (Baixa, Média, Alta)
* Data de criação
* Data de conclusão
* UsuarioId

---

### Usuários

* Criar usuário
* Listar usuários
* Buscar usuário por ID

Cada usuário possui:

* Id
* Nome
* Email
* Senha (armazenada como hash)

---

### Funcionalidades adicionais

* Filtro de tarefas por status, prioridade e usuário
* Paginação de resultados
* Resumo de tarefas por status de um usuário

---

## Validação

A aplicação utiliza FluentValidation para validação dos dados de entrada, garantindo consistência e clareza nas mensagens de erro.

---

## Tratamento de Erros

Foi implementado um middleware global para captura de exceções, retornando respostas padronizadas em formato JSON.

---

## Banco de Dados

O projeto utiliza SQL Server com Entity Framework Core (Code First).

### Executando com Docker

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" \
-p 1433:1433 --name taskmanager-sql -d mcr.microsoft.com/mssql/server:2022-latest
```

---

## Migrations

Para criar e atualizar o banco de dados:

```bash
dotnet ef migrations add InitialCreate -p TaskManager.Infrastructure -s TaskManager.API
dotnet ef database update -p TaskManager.Infrastructure -s TaskManager.API
```

---

## Configuração

No arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=TaskManagerDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True"
  }
}
```

---

## Executando a aplicação

```bash
dotnet run --project TaskManager.API
```

A API estará disponível em:

```
http://localhost:5075
```

Swagger:

```
http://localhost:5075/swagger
```

---

## Testes

O projeto contém testes unitários utilizando xUnit e banco em memória.

Para executar:

```bash
dotnet test
```

---

## Decisões Técnicas

* Utilização de arquitetura em camadas para melhor organização e manutenção
* Repository Pattern para abstração do acesso a dados
* DTOs para controle de entrada e saída da API
* FluentValidation para validação mais flexível e desacoplada
* EF Core InMemory para testes rápidos e isolados

---

## Possíveis Melhorias

* Implementação de autenticação com JWT
* Uso de CQRS com MediatR
* Logs estruturados com Serilog
* Cache em memória para endpoints de listagem
* Docker Compose para orquestração completa da aplicação

---

## Autor

Projeto desenvolvido como parte de avaliação técnica para vaga de desenvolvedor .NET.

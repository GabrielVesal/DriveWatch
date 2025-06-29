# 🚗 DriveWatch - Sistema de Controle de Acesso de Veículos

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()

## 📋 Índice

- [Visão Geral](#visão-geral)
- [Funcionalidades](#funcionalidades)
- [Arquitetura](#arquitetura)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Instalação e Configuração](#instalação-e-configuração)
- [Como Usar](#como-usar)
- [API Endpoints](#api-endpoints)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Contribuição](#contribuição)
- [Licença](#licença)

## 🎯 Visão Geral

O **DriveWatch** é um sistema completo de controle de acesso de veículos desenvolvido em **.NET 8** seguindo os princípios da **Clean Architecture** e **CQRS (Command Query Responsibility Segregation)**. O sistema permite registrar entradas e saídas de veículos, gerenciar informações de motoristas e gerar relatórios de acesso.

### 🎨 Diagrama de Arquitetura

```mermaid
flowchart TB
    subgraph "Presentation Layer"
        A[API Controllers]
        B[Swagger UI]
    end
    
    subgraph "Application Layer"
        C[Commands]
        D[Queries]
        E[Handlers]
        F[Validators]
    end
    
    subgraph "Domain Layer"
        G[Entities]
        H[Contracts]
        I[Business Rules]
    end
    
    subgraph "Infrastructure Layer"
        J[Repositories]
        K[Dapper Micro ORM]
        L[Database Context]
        M[SQL Server]
    end
    
    A --> C
    A --> D
    C --> E
    D --> E
    E --> F
    E --> G
    E --> H
    H --> J
    J --> K
    K --> L
    L --> M
    B --> A
    
    style A fill:#e1f5fe
    style B fill:#e1f5fe
    style C fill:#f3e5f5
    style D fill:#f3e5f5
    style E fill:#f3e5f5
    style F fill:#f3e5f5
    style G fill:#e8f5e8
    style H fill:#e8f5e8
    style I fill:#e8f5e8
    style J fill:#fff3e0
    style K fill:#fff3e0
    style L fill:#fff3e0
    style M fill:#fff3e0
```

### 🔄 Fluxo de Dados

```mermaid
sequenceDiagram
    participant Client
    participant API
    participant MediatR
    participant Handler
    participant Repository
    participant Database
    
    Client->>API: POST /api/VehicleAccess
    API->>MediatR: Send Command
    MediatR->>Handler: Handle Command
    Handler->>Repository: Create Vehicle
    Repository->>Database: INSERT
    Database-->>Repository: Success
    Repository-->>Handler: Vehicle Entity
    Handler-->>MediatR: Result
    MediatR-->>API: Response
    API-->>Client: 201 Created
```

## ✨ Funcionalidades

- ✅ **Registro de Entrada**: Cadastro completo de veículos com informações detalhadas
- ✅ **Registro de Saída**: Controle de horário de saída dos veículos
- ✅ **Atualização de Dados**: Modificação de informações de veículos registrados
- ✅ **Exclusão de Registros**: Remoção de entradas do sistema
- ✅ **Consulta por ID**: Busca específica de veículos por identificador
- ✅ **Listagem Completa**: Visualização de todos os registros
- ✅ **Validação de Dados**: Verificação automática de informações
- ✅ **Documentação API**: Swagger UI integrado
- ✅ **Tratamento de Erros**: Middleware personalizado para exceções

## 🏗️ Arquitetura

O projeto segue a **Clean Architecture** com separação clara de responsabilidades:

### 📁 Estrutura das Camadas

```mermaid
graph TD
    subgraph "DriveWatch Solution"
        subgraph "API Layer"
            A1[Controllers]
            A2[Middleware]
            A3[Program.cs]
        end
        
        subgraph "Application Layer"
            B1[Commands]
            B2[Queries]
            B3[Handlers]
            B4[Validators]
        end
        
        subgraph "Domain Layer"
            C1[Entities]
            C2[Contracts]
        end
        
        subgraph "Infrastructure Layer"
            D1[Repositories]
            D2[Data Context]
            D3[SQL Scripts]
        end
    end
    
    A1 --> B1
    A1 --> B2
    B1 --> B3
    B2 --> B3
    B3 --> C1
    B3 --> C2
    C2 --> D1
    D1 --> D2
    D2 --> D3
    
    style A1 fill:#bbdefb
    style A2 fill:#bbdefb
    style A3 fill:#bbdefb
    style B1 fill:#e1bee7
    style B2 fill:#e1bee7
    style B3 fill:#e1bee7
    style B4 fill:#e1bee7
    style C1 fill:#c8e6c9
    style C2 fill:#c8e6c9
    style D1 fill:#ffe0b2
    style D2 fill:#ffe0b2
    style D3 fill:#ffe0b2
```

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Versão | Propósito |
|------------|--------|-----------|
| **.NET 8** | 8.0 | Framework principal |
| **ASP.NET Core** | 8.0 | Web API |
| **Dapper** | 2.1.66 | Micro ORM para acesso a dados |
| **SQL Server** | 2019 | Banco de dados |
| **MediatR** | 12.5.0 | Implementação CQRS |
| **FluentValidation** | 12.0.0 | Validação de dados |
| **Swagger** | 6.5.0 | Documentação API |
| **Docker** | - | Containerização |

### 🔍 Dapper - Micro ORM

O projeto utiliza o **Dapper** como micro ORM para acesso ao banco de dados SQL Server. O Dapper oferece:

- ⚡ **Performance**: Mapeamento rápido entre objetos e consultas SQL
- 🔧 **Flexibilidade**: Controle total sobre as consultas SQL
- 🛡️ **Segurança**: Proteção contra SQL Injection através de parâmetros
- 📦 **Simplicidade**: API simples e intuitiva

#### Exemplo de Uso no Projeto:

```csharp
// Consulta simples com Dapper
public async Task<VehicleAccess?> GetByIdAsync(int id, CancellationToken cancellationToken)
{
    using var conn = _dbcontext.CreateConnection();
    
    var command = new CommandDefinition(
        "SELECT * FROM VehicleAccesses WHERE Id = @id",
        new { id },
        cancellationToken: cancellationToken);
    
    return await conn.QueryFirstOrDefaultAsync<VehicleAccess>(command);
}

// Inserção com retorno do ID gerado
public async Task InsertAsync(VehicleAccess entity, CancellationToken cancellationToken)
{
    using var conn = _dbcontext.CreateConnection();
    
    var sql = @"INSERT INTO VehicleAccesses 
        (Plate, DriverName, VehicleType, PeopleCount, EntryTime, ExitTime, Observations) 
        VALUES 
        (@Plate, @DriverName, @VehicleType, @PeopleCount, @EntryTime, @ExitTime, @Observations);       
        SELECT CAST(SCOPE_IDENTITY() AS INT);";
    
    var command = new CommandDefinition(sql, entity, cancellationToken: cancellationToken);
    
    entity.Id = await conn.ExecuteScalarAsync<int>(command);
}
```

## 🚀 Instalação e Configuração

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### 📦 Passos para Instalação

1. **Clone o repositório**
   ```bash
   git clone https://github.com/gabrielvesal/DriveWatch.git
   cd DriveWatch
   ```

2. **Inicie o banco de dados**
   ```bash
   docker-compose up -d
   ```

3. **Execute o script SQL**
   ```sql
   -- Conecte ao SQL Server e execute o script em:
   -- Infra/Data/CreateDatabaseAndTable.sql
   ```

4. **Configure a string de conexão**
   ```json
   // API/appsettings.json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost,1433;Database=DriveWatch;User Id=sa;Password=Pass@word;TrustServerCertificate=true;"
     }
   }
   ```

5. **Execute o projeto**
   ```bash
   cd API
   dotnet restore
   dotnet run
   ```

6. **Acesse a documentação**
   ```
   https://localhost:7001/swagger
   ```

## 📖 Como Usar

### 🔧 Configuração Inicial

1. **Banco de Dados**: O sistema utiliza SQL Server 2019 via Docker
2. **Porta**: A API roda na porta 7001 (HTTPS)
3. **Documentação**: Swagger UI disponível em `/swagger`

### 📝 Exemplos de Uso

#### Criar Entrada de Veículo
```http
POST /api/VehicleAccess
Content-Type: application/json

{
  "plate": "ABC-1234",
  "driverName": "João Silva",
  "vehicleType": "Carro",
  "peopleCount": 2,
  "observations": "Entrega de documentos"
}
```

#### Registrar Saída
```http
PUT /api/VehicleAccess/1/exit
```

#### Buscar Veículo por ID
```http
GET /api/VehicleAccess/1
```

#### Listar Todos os Veículos
```http
GET /api/VehicleAccess
```

## 🔌 API Endpoints

### 📋 Endpoints Disponíveis

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `POST` | `/api/VehicleAccess` | Criar entrada de veículo | 201, 400 |
| `PUT` | `/api/VehicleAccess/{id}/exit` | Registrar saída | 200, 404 |
| `PUT` | `/api/VehicleAccess` | Atualizar dados | 200, 404 |
| `DELETE` | `/api/VehicleAccess/{id}` | Excluir registro | 204, 404 |
| `GET` | `/api/VehicleAccess/{id}` | Buscar por ID | 200, 404 |
| `GET` | `/api/VehicleAccess` | Listar todos | 200 |

### 📊 Modelo de Dados

```json
{
  "id": 1,
  "plate": "ABC-1234",
  "driverName": "João Silva",
  "vehicleType": "Carro",
  "peopleCount": 2,
  "observations": "Entrega de documentos",
  "entryTime": "2024-01-15T10:30:00",
  "exitTime": "2024-01-15T11:45:00"
}
```

## 📁 Estrutura do Projeto

```
DriveWatch/
├── 📁 API/                    # Camada de apresentação
│   ├── 📁 Controllers/        # Controladores da API
│   ├── 📁 Middleware/         # Middleware personalizado
│   ├── 📁 Errors/            # Tratamento de erros
│   └── Program.cs            # Configuração da aplicação
│
├── 📁 Application/            # Camada de aplicação
│   ├── 📁 Commands/          # Comandos CQRS
│   ├── 📁 Queries/           # Consultas CQRS
│   ├── 📁 Validators/        # Validações
│   └── 📁 Error/             # Erros de validação
│
├── 📁 Domain/                 # Camada de domínio
│   ├── 📁 Entities/          # Entidades do domínio
│   └── 📁 Contracts/         # Contratos/Interfaces
│
├── 📁 Infra/                  # Camada de infraestrutura
    ├── 📁 Data/              # Contexto de dados
    ├── 📁 Repositories/      # Implementação dos repositórios
    └── 📁 Docker/ 
           docker-compose.yml  # Configurações Docker

```

### 🔍 Detalhamento das Camadas

#### 🎯 API Layer
- **Controllers**: Endpoints REST da aplicação
- **Middleware**: Tratamento global de exceções
- **Program.cs**: Configuração de dependências e pipeline

#### ⚙️ Application Layer
- **Commands**: Operações de escrita (Create, Update, Delete)
- **Queries**: Operações de leitura (Get, GetAll)
- **Handlers**: Lógica de negócio para comandos e queries
- **Validators**: Validação de dados de entrada

#### 🏛️ Domain Layer
- **Entities**: Modelos de domínio (VehicleAccess)
- **Contracts**: Interfaces dos repositórios

#### 🗄️ Infrastructure Layer
- **Repositories**: Implementação do acesso a dados usando Dapper
- **Data Context**: Contexto do banco de dados e gerenciamento de conexões
- **SQL Scripts**: Scripts de criação do banco
- **Dapper**: Micro ORM para mapeamento entre objetos e consultas SQL

## 🔄 Padrões de Design

### CQRS (Command Query Responsibility Segregation)

```mermaid
graph LR
    subgraph "Commands"
        A[CreateVehicleEntryCommand]
        B[UpdateVehicleEntryCommand]
        C[DeleteVehicleEntryCommand]
        D[RegisterExitCommand]
    end
    
    subgraph "Queries"
        E[GetVehicleEntryByIdQuery]
        F[GetAllVehicleEntriesQuery]
    end
    
    subgraph "Handlers"
        G[CreateVehicleEntryHandler]
        H[UpdateVehicleEntryHandler]
        I[DeleteVehicleEntryHandler]
        J[RegisterExitHandler]
        K[GetVehicleEntryByIdHandler]
        L[GetAllVehicleEntriesHandler]
    end
    
    A --> G
    B --> H
    C --> I
    D --> J
    E --> K
    F --> L
    
    style A fill:#ffcdd2
    style B fill:#ffcdd2
    style C fill:#ffcdd2
    style D fill:#ffcdd2
    style E fill:#c8e6c9
    style F fill:#c8e6c9
```

### Clean Architecture

```mermaid
graph TD
    subgraph "Dependency Direction"
        A[API] --> B[Application]
        B --> C[Domain]
        A --> C
        D[Infrastructure] --> C
    end
    
    subgraph "Layers"
        A
        B
        C
        D
    end
    
    style A fill:#e3f2fd
    style B fill:#f3e5f5
    style C fill:#e8f5e8
    style D fill:#fff3e0
```

## 🚀 Deploy

### Docker
```bash
# Build da imagem
docker build -t drivewatch .

# Executar container
docker run -p 7001:7001 drivewatch
```

### Azure
```bash
# Publicar no Azure App Service
dotnet publish -c Release
az webapp deploy --resource-group myResourceGroup --name myApp --src-path ./bin/Release/net8.0/publish
```

## 🤝 Contribuição

1. **Fork** o projeto
2. Crie uma **branch** para sua feature (`git checkout -b feature/AmazingFeature`)
3. **Commit** suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. **Push** para a branch (`git push origin feature/AmazingFeature`)
5. Abra um **Pull Request**

### 📋 Checklist para Contribuição

- [ ] Código segue os padrões do projeto
- [ ] Documentação foi atualizada
- [ ] Build passa sem erros
- [ ] Validações foram implementadas

## 📄 Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.


<div align="center">
  <p>Feito com ❤️ usando .NET 8 e Clean Architecture</p>
  <p>⭐ Se este projeto te ajudou, considere dar uma estrela!</p>
</div> 
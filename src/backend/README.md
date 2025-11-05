# Backend - .NET 8 API

The backend consists of three main projects:

## Projects

### 1. ChatbotPlatform.API
Main Web API project containing:
- Controllers (REST endpoints)
- Middleware (auth, rate limiting, error handling)
- Program.cs (application setup)
- appsettings.json (configuration)

### 2. ChatbotPlatform.Jobs
Background job processing using Hangfire:
- Document indexing jobs
- Usage metrics aggregation
- Scheduled maintenance tasks

### 3. ChatbotPlatform.Core
Shared library containing:
- Entity models
- Database context (EF Core)
- Services (OpenAI, Vector, Cache, RAG)
- Repositories
- DTOs and interfaces

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (via Docker or local)
- Redis (via Docker or local)
- ChromaDB (via Docker)

### Setup

1. **Restore packages:**
   ```bash
   cd API
   dotnet restore
   ```

2. **Update connection strings:**
   Edit `appsettings.json` or use environment variables

3. **Run migrations:**
   ```bash
   dotnet ef database update --project Core
   ```

4. **Run the API:**
   ```bash
   dotnet run
   ```

API will be available at `https://localhost:5001`

### Creating Projects

#### API Project
```bash
cd src/backend/API
dotnet new webapi -n ChatbotPlatform.API -f net8.0
```

#### Jobs Project
```bash
cd src/backend/Jobs
dotnet new worker -n ChatbotPlatform.Jobs -f net8.0
```

#### Core Project
```bash
cd src/backend/Core
dotnet new classlib -n ChatbotPlatform.Core -f net8.0
```

### Required Packages

**Core:**
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

**API:**
```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Swashbuckle.AspNetCore
dotnet add package Hangfire.AspNetCore
dotnet add package StackExchange.Redis
dotnet add package Azure.Storage.Blobs
dotnet add package OpenAI
```

## Architecture

```
API/
├── Controllers/
│   ├── AuthController.cs
│   ├── ChatbotsController.cs
│   ├── DocumentsController.cs
│   ├── ConversationsController.cs
│   ├── LeadsController.cs
│   ├── AnalyticsController.cs
│   └── PublicChatController.cs
├── Middleware/
│   ├── RateLimitingMiddleware.cs
│   └── ErrorHandlingMiddleware.cs
├── Program.cs
└── appsettings.json

Core/
├── Entities/
│   ├── Client.cs
│   ├── Chatbot.cs
│   ├── Document.cs
│   ├── Conversation.cs
│   ├── Message.cs
│   ├── Lead.cs
│   └── UsageMetric.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Services/
│   ├── EncryptionService.cs
│   ├── OpenAIService.cs
│   ├── VectorService.cs
│   ├── CacheService.cs
│   ├── RAGService.cs
│   └── ChatbotService.cs
├── Repositories/
│   └── (Repository implementations)
└── DTOs/
    └── (Data transfer objects)

Jobs/
├── Jobs/
│   ├── DocumentIndexingJob.cs
│   └── UsageAggregationJob.cs
└── Program.cs
```

## Development

### Running Tests
```bash
dotnet test
```

### Database Migrations
```bash
# Add migration
dotnet ef migrations add MigrationName --project Core

# Apply migration
dotnet ef database update --project Core

# Rollback migration
dotnet ef database update PreviousMigrationName --project Core
```

### Debugging
Use Visual Studio, VS Code, or Rider with the .NET debugger.

## API Documentation

When running in Development mode, Swagger is available at:
`https://localhost:5001/swagger`

## See Also
- [Phase 1: Foundation & Infrastructure](../../docs/phases/Phase-01-Foundation-Infrastructure.md)
- [Phase 2: Database & Core Models](../../docs/phases/Phase-02-Database-Core-Models.md)
- [Phase 3: Security & OpenAI Integration](../../docs/phases/Phase-03-Security-OpenAI-Integration.md)

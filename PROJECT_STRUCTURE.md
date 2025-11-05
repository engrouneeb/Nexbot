# Project Structure Overview

Complete directory structure for the Multi-Tenant AI Chatbot Platform.

## Directory Tree

```
CalimaticChatBot/
│
├── README.md                          # Main project documentation
├── LICENSE                            # MIT License
├── CONTRIBUTING.md                    # Contribution guidelines
├── PROJECT_STRUCTURE.md               # This file
├── .gitignore                         # Git ignore rules
│
├── docs/                              # Documentation
│   ├── Execution-Plan-Complete.md     # Full 14-week execution plan
│   ├── QUICK_START.md                 # Quick start guide
│   └── phases/                        # Phase-by-phase guides
│       ├── Phase-01-Foundation-Infrastructure.md
│       ├── Phase-02-Database-Core-Models.md
│       ├── Phase-03-Security-OpenAI-Integration.md
│       ├── Phase-04-Vector-Database-Caching.md
│       ├── Phase-05-Document-Processing-Pipeline.md
│       ├── Phase-06-RAG-Chat-API.md
│       ├── Phase-07-Public-API-Widget.md
│       ├── Phase-08-Admin-Dashboard-Part1.md
│       ├── Phase-09-Admin-Dashboard-Part2.md
│       ├── Phase-10-Conversations-Leads.md
│       ├── Phase-11-Analytics-Admin-Chatbot.md
│       ├── Phase-12-Testing-Optimization.md
│       ├── Phase-13-Deployment-Preparation.md
│       └── Phase-14-Launch-Iteration.md
│
├── src/                               # Source code
│   ├── backend/                       # .NET 8 Backend
│   │   ├── README.md
│   │   ├── API/                       # Web API project
│   │   │   ├── Controllers/
│   │   │   ├── Middleware/
│   │   │   ├── Program.cs
│   │   │   └── appsettings.json
│   │   ├── Jobs/                      # Background jobs (Hangfire)
│   │   │   ├── Jobs/
│   │   │   └── Program.cs
│   │   └── Core/                      # Shared library
│   │       ├── Entities/
│   │       ├── Data/
│   │       ├── Services/
│   │       ├── Repositories/
│   │       └── DTOs/
│   │
│   ├── frontend/                      # Frontend applications
│   │   ├── README.md
│   │   └── admin-dashboard/           # Angular 17+ admin dashboard
│   │       ├── src/
│   │       │   ├── app/
│   │       │   │   ├── core/
│   │       │   │   ├── shared/
│   │       │   │   ├── features/
│   │       │   │   └── layout/
│   │       │   ├── assets/
│   │       │   ├── environments/
│   │       │   └── styles.scss
│   │       ├── angular.json
│   │       ├── package.json
│   │       └── tsconfig.json
│   │
│   └── widget/                        # Embeddable JavaScript widget
│       ├── README.md
│       ├── chatbot-widget.js
│       ├── styles.css
│       └── index.html                 # Test page
│
├── infrastructure/                    # Infrastructure configuration
│   ├── docker/                        # Docker configuration
│   │   ├── docker-compose.yml         # Services: ChromaDB, Redis, SQL Server
│   │   ├── .env.example               # Environment variables template
│   │   └── Dockerfile.*               # Individual service Dockerfiles
│   │
│   └── kubernetes/                    # Kubernetes manifests
│       ├── namespace.yaml
│       ├── deployments/
│       ├── services/
│       ├── configmaps/
│       └── secrets/
│
├── tests/                             # Test suites
│   ├── unit/                          # Unit tests
│   ├── integration/                   # Integration tests
│   └── e2e/                           # End-to-end tests
│
└── scripts/                           # Automation scripts
    ├── setup-dev-environment.sh       # Development setup
    ├── generate-keys.sh               # Generate security keys
    ├── backup-db.sh                   # Database backup
    └── deploy.sh                      # Deployment script
```

## Key Files Description

### Root Level

| File | Purpose |
|------|---------|
| `README.md` | Main project documentation, quick start, tech stack |
| `LICENSE` | MIT License |
| `CONTRIBUTING.md` | Contribution guidelines and coding standards |
| `.gitignore` | Git ignore rules for .NET, Angular, Node.js, Docker |

### Documentation (`docs/`)

| File | Purpose |
|------|---------|
| `Execution-Plan-Complete.md` | Full 14-week implementation roadmap |
| `QUICK_START.md` | Step-by-step setup guide |
| `phases/Phase-*.md` | Detailed phase-by-phase implementation guides |

### Backend (`src/backend/`)

#### API Project
- **Controllers:** REST API endpoints
- **Middleware:** Authentication, rate limiting, error handling
- **Program.cs:** Application startup and configuration
- **appsettings.json:** Application settings

#### Jobs Project
- **Jobs:** Background job implementations (Hangfire)
- Document indexing, usage aggregation, maintenance tasks

#### Core Project
- **Entities:** Database entity models (EF Core)
- **Data:** DbContext and migrations
- **Services:** Business logic (OpenAI, Vector, Cache, RAG)
- **Repositories:** Data access layer
- **DTOs:** Data transfer objects

### Frontend (`src/frontend/admin-dashboard/`)

#### Angular Application
- **core:** Services, guards, interceptors
- **shared:** Reusable components, pipes, models
- **features:** Feature modules (chatbots, documents, conversations, leads, analytics)
- **layout:** Layout components (header, sidebar, footer)
- **environments:** Environment-specific configurations

### Widget (`src/widget/`)

- **chatbot-widget.js:** Main widget script (vanilla JavaScript)
- **styles.css:** Widget styling
- **index.html:** Test page for local development

### Infrastructure (`infrastructure/`)

#### Docker
- **docker-compose.yml:** Orchestrates ChromaDB, Redis, SQL Server
- **.env.example:** Template for environment variables
- **Dockerfile.*:** Individual service containers

#### Kubernetes
- Production deployment manifests
- Namespaces, deployments, services, configmaps, secrets

### Tests (`tests/`)

- **unit:** Unit tests for services and components
- **integration:** Integration tests for API endpoints
- **e2e:** End-to-end tests for complete user flows

### Scripts (`scripts/`)

- **setup-dev-environment.sh:** Automated development setup
- **generate-keys.sh:** Generate encryption and JWT keys
- **backup-db.sh:** Database backup script
- **deploy.sh:** Deployment automation

## Technology Stack by Component

### Backend
- **.NET 8** - Web API framework
- **Entity Framework Core** - ORM
- **SQL Server** - Primary database
- **Hangfire** - Background jobs
- **JWT** - Authentication
- **Swagger** - API documentation

### AI & Vector
- **OpenAI API** - GPT-4o, embeddings
- **ChromaDB** - Vector database
- **RAG Pipeline** - Retrieval-Augmented Generation

### Caching & Storage
- **Redis** - Caching and rate limiting
- **Azure Blob / AWS S3** - Document storage

### Frontend
- **Angular 17+** - Admin dashboard framework
- **Tailwind CSS** - Styling
- **Chart.js** - Analytics visualization
- **Vanilla JS** - Embeddable widget

### Infrastructure
- **Docker** - Containerization
- **Kubernetes** - Orchestration
- **GitHub Actions** - CI/CD

## Development Workflow

1. **Start Infrastructure:**
   ```bash
   cd infrastructure/docker
   docker-compose up -d
   ```

2. **Run Backend:**
   ```bash
   cd src/backend/API
   dotnet run
   ```

3. **Run Frontend:**
   ```bash
   cd src/frontend/admin-dashboard
   ng serve
   ```

4. **Test Widget:**
   ```bash
   cd src/widget
   python -m http.server 3000
   ```

## Deployment Architecture

```
┌─────────────────────────────────────────────────────┐
│                  Load Balancer                       │
└──────────────────┬──────────────────────────────────┘
                   │
        ┌──────────┼──────────┐
        ▼          ▼          ▼
    ┌──────┐  ┌──────┐  ┌──────┐
    │ API  │  │ API  │  │ API  │  (Multiple instances)
    │ Node │  │ Node │  │ Node │
    └──────┘  └──────┘  └──────┘
        │          │          │
        └──────────┼──────────┘
                   │
        ┌──────────┼──────────────┐
        ▼          ▼              ▼
    ┌────────┐ ┌───────┐  ┌──────────┐
    │   SQL  │ │ Redis │  │ ChromaDB │
    │ Server │ │       │  │          │
    └────────┘ └───────┘  └──────────┘
```

## Next Steps

1. Follow the [Quick Start Guide](docs/QUICK_START.md)
2. Begin with [Phase 1](docs/phases/Phase-01-Foundation-Infrastructure.md)
3. Follow the [Execution Plan](docs/Execution-Plan-Complete.md)

## Support

For questions or issues, refer to:
- [README.md](README.md) - Main documentation
- [CONTRIBUTING.md](CONTRIBUTING.md) - Development guidelines
- [docs/](docs/) - Detailed documentation

Happy building! 🚀

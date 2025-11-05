# Multi-Tenant AI Chatbot Platform

A comprehensive, production-ready chatbot platform that enables businesses to deploy custom AI-powered chatbots with RAG (Retrieval-Augmented Generation) capabilities.

## Overview

This platform allows clients to:
- Create and manage multiple AI chatbots
- Upload and index custom knowledge bases (PDF, DOCX, TXT, HTML)
- Embed chatbots on their websites via a simple JavaScript widget
- Capture and manage leads from conversations
- Track usage analytics and costs
- Bring their own OpenAI API keys (BYOK model)

## Tech Stack

### Backend
- **.NET 8** - Web API and background jobs
- **Entity Framework Core** - ORM for SQL Server
- **SQL Server** - Primary relational database
- **Hangfire** - Background job processing

### AI & Vector Database
- **OpenAI API** - GPT-4o, GPT-4o-mini, text-embedding-3-small
- **ChromaDB** - Vector database for embeddings
- **RAG Pipeline** - Retrieval-Augmented Generation

### Caching & Storage
- **Redis** - Caching and rate limiting
- **Azure Blob Storage / AWS S3** - Document storage

### Frontend
- **Angular 17+** - Admin dashboard
- **Tailwind CSS** - Styling
- **Chart.js** - Analytics visualization
- **Vanilla JavaScript** - Embeddable widget (no dependencies)

### Infrastructure
- **Docker & Docker Compose** - Containerization
- **Kubernetes** - Production orchestration (optional)
- **GitHub Actions** - CI/CD pipeline

## Key Features

### For Platform Admin
- Multi-tenant architecture with complete data isolation
- Encrypted API key storage (AES-256)
- Comprehensive analytics and cost tracking
- Internal admin chatbot for platform support

### For Clients
- Easy chatbot creation and configuration
- Document upload with automatic processing
- Customizable widget appearance (colors, avatar, messages)
- Lead capture with AI-powered intent detection
- Conversation history and transcript export
- Usage metrics and cost estimation

### For End Users
- Fast, responsive chat interface
- Mobile-friendly design
- Context-aware responses using RAG
- Lead capture forms when intent detected

## Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                        Client Website                        │
│                    (Embedded Widget JS)                      │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│                    .NET 8 Web API                            │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐     │
│  │ Public Chat  │  │  Auth API    │  │  Admin API   │     │
│  │  Controller  │  │  Controller  │  │  Controller  │     │
│  └──────────────┘  └──────────────┘  └──────────────┘     │
└───────────────────────────┬─────────────────────────────────┘
                            │
        ┌───────────────────┼───────────────────┐
        ▼                   ▼                   ▼
┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│  SQL Server  │    │    Redis     │    │  ChromaDB    │
│  (Metadata)  │    │  (Cache +    │    │  (Vectors)   │
│              │    │  Rate Limit) │    │              │
└──────────────┘    └──────────────┘    └──────────────┘
                            │
                            ▼
                    ┌──────────────┐
                    │   Hangfire   │
                    │ (Background  │
                    │    Jobs)     │
                    └──────────────┘
```

## Project Structure

```
CalimaticChatBot/
├── src/
│   ├── backend/
│   │   ├── API/                    # Main Web API project
│   │   ├── Jobs/                   # Background job processing
│   │   └── Core/                   # Shared entities, services, interfaces
│   ├── frontend/
│   │   └── admin-dashboard/        # Angular admin dashboard
│   └── widget/                     # Embeddable JavaScript widget
├── infrastructure/
│   ├── docker/                     # Docker Compose files
│   └── kubernetes/                 # K8s deployment manifests
├── docs/
│   ├── phases/                     # 14 phase implementation guides
│   └── Execution-Plan-Complete.md  # Full project roadmap
├── tests/
│   ├── unit/                       # Unit tests
│   ├── integration/                # Integration tests
│   └── e2e/                        # End-to-end tests
└── scripts/                        # Automation scripts
```

## Quick Start

### Prerequisites
- Docker and Docker Compose
- .NET 8 SDK
- Node.js 18+ and npm
- OpenAI API key

### 1. Clone Repository
```bash
git clone <repository-url>
cd CalimaticChatBot
```

### 2. Set Up Environment Variables
```bash
cp infrastructure/docker/.env.example infrastructure/docker/.env
# Edit .env with your configurations
```

### 3. Start Infrastructure Services
```bash
cd infrastructure/docker
docker-compose up -d
```

This starts:
- ChromaDB on port 8000
- Redis on port 6379
- SQL Server on port 1433

### 4. Run Backend API
```bash
cd src/backend/API
dotnet restore
dotnet ef database update
dotnet run
```

API will be available at `https://localhost:5001`

### 5. Run Admin Dashboard
```bash
cd src/frontend/admin-dashboard
npm install
ng serve
```

Dashboard will be available at `http://localhost:4200`

### 6. Test Widget
Open `src/widget/index.html` in a browser or serve it locally:
```bash
cd src/widget
python -m http.server 3000
```

## Development Roadmap

The project is divided into 14 phases over 14 weeks:

| Phase | Duration | Focus |
|-------|----------|-------|
| [Phase 1](docs/phases/Phase-01-Foundation-Infrastructure.md) | Weeks 1-2 | Foundation & Infrastructure |
| [Phase 2](docs/phases/Phase-02-Database-Core-Models.md) | Week 2 | Database & Core Models |
| [Phase 3](docs/phases/Phase-03-Security-OpenAI-Integration.md) | Week 3 | Security & OpenAI Integration |
| [Phase 4](docs/phases/Phase-04-Vector-Database-Caching.md) | Week 4 | Vector Database & Caching |
| [Phase 5](docs/phases/Phase-05-Document-Processing-Pipeline.md) | Week 5 | Document Processing Pipeline |
| [Phase 6](docs/phases/Phase-06-RAG-Chat-API.md) | Week 6 | RAG & Chat API |
| [Phase 7](docs/phases/Phase-07-Public-API-Widget.md) | Week 7 | Public API & Widget |
| [Phase 8](docs/phases/Phase-08-Admin-Dashboard-Part1.md) | Week 8 | Admin Dashboard Part 1 |
| [Phase 9](docs/phases/Phase-09-Admin-Dashboard-Part2.md) | Week 9 | Admin Dashboard Part 2 |
| [Phase 10](docs/phases/Phase-10-Conversations-Leads.md) | Week 10 | Conversations & Leads |
| [Phase 11](docs/phases/Phase-11-Analytics-Admin-Chatbot.md) | Week 11 | Analytics & Admin Chatbot |
| [Phase 12](docs/phases/Phase-12-Testing-Optimization.md) | Week 12 | Testing & Optimization |
| [Phase 13](docs/phases/Phase-13-Deployment-Preparation.md) | Week 13 | Deployment Preparation |
| [Phase 14](docs/phases/Phase-14-Launch-Iteration.md) | Week 14+ | Launch & Iteration |

See the complete [Execution Plan](docs/Execution-Plan-Complete.md) for detailed implementation steps.

## Environment Variables

Key environment variables (see `.env.example` for complete list):

```env
# Database
SQL_SERVER_CONNECTION_STRING=Server=localhost,1433;Database=ChatbotPlatform;...

# Redis
REDIS_CONNECTION_STRING=localhost:6379

# ChromaDB
CHROMADB_URL=http://localhost:8000

# Security
ENCRYPTION_KEY=<32-character-key>
JWT_SECRET=<64-character-secret>

# OpenAI
PLATFORM_OPENAI_API_KEY=sk-...

# Storage
STORAGE_TYPE=azure  # or aws, local
AZURE_STORAGE_CONNECTION_STRING=...
```

## API Documentation

### Public Endpoints (No Auth)
- `GET /api/public/bot/{botId}/config` - Get bot configuration
- `POST /api/public/conversation/{botId}/start` - Start conversation
- `POST /api/public/chat/{botId}` - Send message
- `POST /api/public/conversation/{conversationId}/capture-lead` - Capture lead

### Admin Endpoints (JWT Auth Required)
- `POST /api/auth/login` - Client login
- `GET /api/chatbots` - List chatbots
- `POST /api/chatbots` - Create chatbot
- `POST /api/documents/upload` - Upload document
- `GET /api/conversations` - List conversations
- `GET /api/leads` - List leads
- `GET /api/analytics` - Get analytics

Full API documentation available at `/swagger` when running in development mode.

## Widget Embed Code

To embed a chatbot on any website:

```html
<script src="https://your-platform.com/widget/chatbot-widget.js" 
        data-bot-id="your-bot-id" 
        defer>
</script>
```

## Testing

### Run Unit Tests
```bash
cd src/backend/API
dotnet test
```

### Run Integration Tests
```bash
cd tests/integration
dotnet test
```

### Run E2E Tests
```bash
cd tests/e2e
npm run test:e2e
```

## Deployment

### Docker Compose (Simple)
```bash
docker-compose -f infrastructure/docker/docker-compose.prod.yml up -d
```

### Kubernetes (Production)
```bash
kubectl apply -f infrastructure/kubernetes/
```

See [Phase 13: Deployment Preparation](docs/phases/Phase-13-Deployment-Preparation.md) for detailed deployment guides.

## Security

- AES-256 encryption for API keys
- JWT authentication with refresh tokens
- Rate limiting (global, per-bot, per-IP)
- CORS configuration for widget domains
- SQL injection prevention via EF Core
- XSS protection in chat messages
- HTTPS enforced in production

## Cost Estimation

### Development Phase (12 weeks)
- Team: $60K - $120K
- Infrastructure: $200 - $500/month
- Tools: $500 - $1,000

### Monthly Operating (First 6 months)
- Infrastructure: $500 - $1,500
- OpenAI API: $400 - $1,000 (varies with usage)
- Monitoring: $100 - $300
- Support: $2,000 - $5,000

**Total Monthly:** $3,000 - $8,000

## Success Metrics

- **Uptime:** >99.5%
- **API Response Time:** p95 <500ms
- **Error Rate:** <0.1%
- **Active Chatbots:** 50+ in first 3 months
- **Conversations:** 10K+ per month
- **Client Retention:** >90% after 6 months

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for development guidelines.

## License

This project is licensed under the MIT License - see [LICENSE](LICENSE) file for details.

## Support

For questions or issues:
- Create an issue in this repository
- Email: support@your-platform.com
- Documentation: [docs/](docs/)

## Acknowledgments

Built with:
- [OpenAI API](https://openai.com/)
- [ChromaDB](https://www.trychroma.com/)
- [.NET 8](https://dotnet.microsoft.com/)
- [Angular](https://angular.io/)

---

**Ready to build?** Start with [Phase 1: Foundation & Infrastructure](docs/phases/Phase-01-Foundation-Infrastructure.md)

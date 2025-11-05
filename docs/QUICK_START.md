# Quick Start Guide

Get the Multi-Tenant Chatbot Platform running in under 10 minutes!

## Prerequisites

Before you begin, ensure you have:
- [Docker Desktop](https://www.docker.com/products/docker-desktop) installed
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed
- [Node.js 18+](https://nodejs.org/) installed
- [Git](https://git-scm.com/) installed
- An [OpenAI API key](https://platform.openai.com/api-keys)

## Step-by-Step Setup

### 1. Clone the Repository

```bash
git clone <repository-url>
cd CalimaticChatBot
```

### 2. Run Setup Script (Automated)

**On macOS/Linux:**
```bash
chmod +x scripts/setup-dev-environment.sh
./scripts/setup-dev-environment.sh
```

**On Windows:**
Use Git Bash or WSL to run the script, or follow the manual steps below.

### 3. Manual Setup (Alternative)

#### 3.1 Generate Security Keys
```bash
# Generate encryption key
openssl rand -base64 32

# Generate JWT secret
openssl rand -base64 64
```

#### 3.2 Create Environment File
```bash
cp infrastructure/docker/.env.example infrastructure/docker/.env
```

Edit `infrastructure/docker/.env` and add:
- Your generated keys
- Your OpenAI API key
- Any other custom configurations

#### 3.3 Start Infrastructure Services
```bash
cd infrastructure/docker
docker-compose up -d
```

This starts:
- **ChromaDB** on port 8000
- **Redis** on port 6379
- **SQL Server** on port 1433

#### 3.4 Verify Services are Running
```bash
# Check ChromaDB
curl http://localhost:8000/api/v1/heartbeat

# Check Redis
docker exec chatbot-redis redis-cli ping

# Check SQL Server
docker exec chatbot-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong@Password123" -Q "SELECT 1"
```

### 4. Set Up Backend (.NET API)

```bash
cd src/backend/API

# Restore dependencies
dotnet restore

# Create initial database migration (if not exists)
dotnet ef migrations add InitialCreate --project ../Core

# Apply database migrations
dotnet ef database update

# Run the API
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`
- Swagger: `https://localhost:5001/swagger`

### 5. Set Up Frontend (Angular Dashboard)

Open a new terminal:

```bash
cd src/frontend/admin-dashboard

# Install dependencies
npm install

# Start development server
ng serve
```

The dashboard will be available at `http://localhost:4200`

### 6. Test the Widget

Open `src/widget/index.html` in a browser or serve it:

```bash
cd src/widget
python -m http.server 3000
# Or use any static server
```

Visit `http://localhost:3000`

## First Login

### Create Test Account

The application seeds a test account on first run:
- **Email:** admin@testcompany.com
- **Password:** Test123!

Or create a new account via the registration endpoint:

```bash
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "companyName": "My Company",
    "email": "user@example.com",
    "password": "SecurePassword123!"
  }'
```

## Create Your First Chatbot

1. Log in to the dashboard at `http://localhost:4200`
2. Navigate to **Chatbots** → **Create New**
3. Fill in:
   - **Name:** Support Bot
   - **OpenAI API Key:** Your API key
   - **Model:** gpt-4o-mini (recommended for testing)
   - **Welcome Message:** Hello! How can I help you?
4. Click **Save**

## Upload Knowledge Base

1. Go to **Documents** in the sidebar
2. Click **Upload** or drag and drop files
3. Supported formats: PDF, DOCX, TXT, HTML
4. Wait for processing (status will change to "Completed")
5. Documents are automatically indexed into ChromaDB

## Embed the Widget

1. In the chatbot detail page, find the **Embed Code** section
2. Copy the code snippet:
   ```html
   <script src="https://localhost:5001/widget/chatbot-widget.js" 
           data-bot-id="your-bot-id" 
           defer>
   </script>
   ```
3. Paste it into any HTML page before the closing `</body>` tag
4. Open the page and test the chatbot!

## Troubleshooting

### Docker Services Won't Start
```bash
# Check Docker is running
docker ps

# View service logs
docker-compose logs chromadb
docker-compose logs redis
docker-compose logs sqlserver
```

### Database Migration Fails
```bash
# Drop and recreate database
dotnet ef database drop --force
dotnet ef database update
```

### Port Already in Use
Change ports in `docker-compose.yml` or kill the process using the port:
```bash
# Find process using port 5001
lsof -i :5001

# Kill process
kill -9 <PID>
```

### ChromaDB Connection Error
Ensure ChromaDB is running:
```bash
docker ps | grep chromadb
curl http://localhost:8000/api/v1/heartbeat
```

### OpenAI API Errors
- Verify your API key is valid
- Check you have credits available
- Ensure the key has proper permissions

## Next Steps

- Read the [Full Execution Plan](./Execution-Plan-Complete.md)
- Follow [Phase 1: Foundation & Infrastructure](./phases/Phase-01-Foundation-Infrastructure.md)
- Explore the [API Documentation](https://localhost:5001/swagger)
- Check out [Contributing Guidelines](../CONTRIBUTING.md)

## Common Tasks

### Reset Development Environment
```bash
# Stop all containers
docker-compose down

# Remove volumes (WARNING: deletes all data)
docker-compose down -v

# Restart
docker-compose up -d
```

### View Logs
```bash
# API logs
cd src/backend/API
dotnet run

# Docker service logs
docker-compose logs -f chromadb
docker-compose logs -f redis
docker-compose logs -f sqlserver
```

### Run Tests
```bash
# Backend tests
cd src/backend/API
dotnet test

# Frontend tests
cd src/frontend/admin-dashboard
npm run test
```

## Need Help?

- Check the [Documentation](./README.md)
- Review [Common Issues](./TROUBLESHOOTING.md)
- Create an issue on GitHub
- Contact support@chatbotplatform.com

Happy building! 🚀

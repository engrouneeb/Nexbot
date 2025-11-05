#!/bin/bash

# Multi-Tenant Chatbot Platform - Development Environment Setup Script
# This script sets up the complete development environment

set -e  # Exit on any error

echo "=========================================="
echo "Chatbot Platform - Dev Environment Setup"
echo "=========================================="
echo ""

# Check prerequisites
echo "Checking prerequisites..."

command -v docker >/dev/null 2>&1 || { echo "ERROR: Docker is not installed. Please install Docker first."; exit 1; }
command -v docker-compose >/dev/null 2>&1 || { echo "ERROR: Docker Compose is not installed. Please install Docker Compose first."; exit 1; }
command -v dotnet >/dev/null 2>&1 || { echo "ERROR: .NET 8 SDK is not installed. Please install .NET 8 SDK first."; exit 1; }
command -v node >/dev/null 2>&1 || { echo "ERROR: Node.js is not installed. Please install Node.js 18+ first."; exit 1; }

echo "✓ All prerequisites installed"
echo ""

# Set up environment file
echo "Setting up environment variables..."
if [ ! -f "infrastructure/docker/.env" ]; then
    cp infrastructure/docker/.env.example infrastructure/docker/.env
    echo "✓ Created .env file from template"
    echo "⚠️  IMPORTANT: Edit infrastructure/docker/.env with your actual values!"
else
    echo "✓ .env file already exists"
fi
echo ""

# Generate encryption keys
echo "Generating security keys..."
ENCRYPTION_KEY=$(openssl rand -base64 32)
JWT_SECRET=$(openssl rand -base64 64)

echo "Generated keys (add these to your .env file):"
echo "ENCRYPTION_KEY=$ENCRYPTION_KEY"
echo "JWT_SECRET=$JWT_SECRET"
echo ""

# Start Docker services
echo "Starting Docker services (ChromaDB, Redis, SQL Server)..."
cd infrastructure/docker
docker-compose up -d
echo "✓ Docker services started"
echo ""

# Wait for services to be ready
echo "Waiting for services to be ready..."
sleep 10

# Test ChromaDB
echo "Testing ChromaDB connection..."
curl -s http://localhost:8000/api/v1/heartbeat > /dev/null && echo "✓ ChromaDB is running" || echo "✗ ChromaDB connection failed"

# Test Redis
echo "Testing Redis connection..."
docker exec chatbot-redis redis-cli ping > /dev/null 2>&1 && echo "✓ Redis is running" || echo "✗ Redis connection failed"

# Test SQL Server
echo "Testing SQL Server connection..."
sleep 5  # Give SQL Server more time
docker exec chatbot-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong@Password123" -Q "SELECT 1" > /dev/null 2>&1 && echo "✓ SQL Server is running" || echo "✗ SQL Server connection failed"

cd ../..

echo ""
echo "=========================================="
echo "Setup Complete!"
echo "=========================================="
echo ""
echo "Next steps:"
echo "1. Edit infrastructure/docker/.env with your configurations"
echo "2. Navigate to src/backend/API and run: dotnet restore"
echo "3. Run database migrations: dotnet ef database update"
echo "4. Start the API: dotnet run"
echo "5. Navigate to src/frontend/admin-dashboard and run: npm install"
echo "6. Start the dashboard: ng serve"
echo ""
echo "View services:"
echo "- ChromaDB: http://localhost:8000"
echo "- API: https://localhost:5001"
echo "- Dashboard: http://localhost:4200"
echo ""

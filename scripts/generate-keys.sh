#!/bin/bash

# Generate secure encryption and JWT keys for the chatbot platform

echo "=========================================="
echo "Security Keys Generator"
echo "=========================================="
echo ""

echo "Generating ENCRYPTION_KEY (32 bytes, base64 encoded)..."
ENCRYPTION_KEY=$(openssl rand -base64 32)
echo "ENCRYPTION_KEY=$ENCRYPTION_KEY"
echo ""

echo "Generating JWT_SECRET (64 bytes, base64 encoded)..."
JWT_SECRET=$(openssl rand -base64 64)
echo "JWT_SECRET=$JWT_SECRET"
echo ""

echo "=========================================="
echo "Add these to your .env file"
echo "=========================================="
echo ""
echo "ENCRYPTION_KEY=$ENCRYPTION_KEY"
echo "JWT_SECRET=$JWT_SECRET"
echo ""
echo "⚠️  IMPORTANT: Never commit these keys to version control!"

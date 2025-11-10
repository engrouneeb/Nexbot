# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["ChatbotPlatform.API/ChatbotPlatform.API.csproj", "ChatbotPlatform.API/"]
COPY ["ChatbotPlatform.Core/ChatbotPlatform.Core.csproj", "ChatbotPlatform.Core/"]
RUN dotnet restore "ChatbotPlatform.API/ChatbotPlatform.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/ChatbotPlatform.API"
RUN dotnet build "ChatbotPlatform.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "ChatbotPlatform.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 5050
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatbotPlatform.API.dll"]

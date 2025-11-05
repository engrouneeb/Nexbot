# Contributing to Multi-Tenant Chatbot Platform

Thank you for your interest in contributing to the Multi-Tenant Chatbot Platform! This document provides guidelines and instructions for contributing to the project.

## Table of Contents
- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Workflow](#development-workflow)
- [Coding Standards](#coding-standards)
- [Commit Guidelines](#commit-guidelines)
- [Pull Request Process](#pull-request-process)
- [Testing Guidelines](#testing-guidelines)
- [Documentation](#documentation)

## Code of Conduct

This project adheres to a code of conduct that all contributors are expected to follow:

- Be respectful and inclusive
- Welcome newcomers and help them get started
- Focus on constructive feedback
- Maintain professional communication
- Respect differing viewpoints and experiences

## Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js 18+ and npm
- Docker and Docker Compose
- Git
- Your favorite IDE (Visual Studio, VS Code, Rider)

### Setting Up Development Environment

1. **Fork and Clone**
   ```bash
   git clone https://github.com/your-username/chatbot-platform.git
   cd chatbot-platform
   ```

2. **Set Up Environment Variables**
   ```bash
   cp infrastructure/docker/.env.example infrastructure/docker/.env
   # Edit .env with your configuration
   ```

3. **Start Infrastructure**
   ```bash
   cd infrastructure/docker
   docker-compose up -d
   ```

4. **Run Backend**
   ```bash
   cd src/backend/API
   dotnet restore
   dotnet ef database update
   dotnet run
   ```

5. **Run Frontend**
   ```bash
   cd src/frontend/admin-dashboard
   npm install
   ng serve
   ```

## Development Workflow

### Branching Strategy

We follow a simplified Git Flow:

- `main` - Production-ready code
- `develop` - Integration branch for features
- `feature/*` - New features
- `bugfix/*` - Bug fixes
- `hotfix/*` - Urgent production fixes

### Creating a Feature Branch

```bash
git checkout develop
git pull origin develop
git checkout -b feature/your-feature-name
```

### Working on Your Feature

1. Make your changes in small, logical commits
2. Write tests for new functionality
3. Update documentation as needed
4. Ensure all tests pass
5. Follow coding standards

## Coding Standards

### C# / .NET

- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful variable and method names
- Keep methods small and focused (Single Responsibility Principle)
- Use async/await for I/O operations
- Add XML documentation comments for public APIs

**Example:**
```csharp
/// <summary>
/// Retrieves a chatbot by its unique identifier.
/// </summary>
/// <param name="id">The chatbot ID</param>
/// <returns>The chatbot if found, null otherwise</returns>
public async Task<Chatbot?> GetByIdAsync(Guid id)
{
    return await _context.Chatbots
        .Include(c => c.Documents)
        .FirstOrDefaultAsync(c => c.Id == id);
}
```

### TypeScript / Angular

- Follow [Angular Style Guide](https://angular.io/guide/styleguide)
- Use TypeScript strict mode
- Use reactive programming (RxJS) where appropriate
- Keep components focused and small
- Use services for business logic

**Example:**
```typescript
export class ChatbotService {
  private readonly apiUrl = `${environment.apiUrl}/api/chatbots`;

  constructor(private http: HttpClient) {}

  getChatbots(): Observable<Chatbot[]> {
    return this.http.get<Chatbot[]>(this.apiUrl).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    console.error('An error occurred:', error.message);
    return throwError(() => new Error('Something went wrong'));
  }
}
```

### JavaScript (Widget)

- Use ES6+ features
- Keep code vanilla (no framework dependencies)
- Minimize bundle size
- Add JSDoc comments
- Ensure cross-browser compatibility

### General Guidelines

- **Naming Conventions:**
  - Classes: `PascalCase`
  - Methods: `PascalCase` (C#), `camelCase` (JS/TS)
  - Variables: `camelCase`
  - Constants: `UPPER_SNAKE_CASE`
  - Private fields: `_camelCase` (C#)

- **File Organization:**
  - One class per file
  - Related files in same directory
  - Use meaningful file names

- **Error Handling:**
  - Always handle exceptions
  - Log errors with context
  - Return meaningful error messages
  - Never expose sensitive information

## Commit Guidelines

### Commit Message Format

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Types
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (formatting, no logic change)
- `refactor`: Code refactoring
- `test`: Adding or updating tests
- `chore`: Maintenance tasks

### Examples

```
feat(chatbot): add support for custom system prompts

- Allow users to customize the system prompt
- Add validation for prompt length
- Update chatbot form with new field

Closes #123
```

```
fix(auth): resolve JWT token expiration issue

Fixed bug where refresh tokens weren't being validated correctly,
causing users to be logged out prematurely.

Fixes #456
```

## Pull Request Process

### Before Submitting

1. **Ensure your code builds:**
   ```bash
   dotnet build
   npm run build
   ```

2. **Run all tests:**
   ```bash
   dotnet test
   npm run test
   ```

3. **Check code formatting:**
   ```bash
   dotnet format
   npm run lint
   ```

4. **Update documentation** if needed

5. **Rebase on latest develop:**
   ```bash
   git fetch origin
   git rebase origin/develop
   ```

### Submitting a Pull Request

1. Push your branch to your fork:
   ```bash
   git push origin feature/your-feature-name
   ```

2. Open a pull request on GitHub

3. Fill out the PR template completely:
   - Clear description of changes
   - Link to related issues
   - Screenshots (if UI changes)
   - Testing instructions

4. Request review from maintainers

### PR Review Process

- At least one approval required
- All CI checks must pass
- No merge conflicts
- Code follows project standards
- Tests cover new functionality
- Documentation is updated

### After Approval

Maintainers will merge your PR using "Squash and Merge" to keep history clean.

## Testing Guidelines

### Unit Tests

- Test individual components in isolation
- Mock external dependencies
- Aim for >80% code coverage
- Use descriptive test names

**Example (C#):**
```csharp
[Fact]
public async Task GetByIdAsync_ValidId_ReturnsChat bot()
{
    // Arrange
    var chatbotId = Guid.NewGuid();
    var expectedChatbot = new Chatbot { Id = chatbotId, Name = "Test Bot" };
    _mockRepository.Setup(r => r.GetByIdAsync(chatbotId))
        .ReturnsAsync(expectedChatbot);

    // Act
    var result = await _service.GetByIdAsync(chatbotId);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(expectedChatbot.Id, result.Id);
}
```

### Integration Tests

- Test interactions between components
- Use test database (not production!)
- Clean up test data after each test
- Test happy paths and error cases

### E2E Tests

- Test complete user workflows
- Use Playwright or Cypress
- Test on multiple browsers
- Include mobile viewports

## Documentation

### Code Documentation

- Add XML comments to public APIs (C#)
- Add JSDoc comments to exported functions (JS/TS)
- Keep comments up-to-date with code changes
- Explain *why*, not *what* (code should be self-explanatory)

### User Documentation

- Update relevant docs in `docs/` folder
- Add examples and screenshots
- Keep language clear and concise
- Update README if needed

### API Documentation

- Document all endpoints
- Include request/response examples
- Specify authentication requirements
- Note any rate limits or restrictions

## Project Structure

```
src/
├── backend/
│   ├── API/              # Web API controllers
│   ├── Jobs/             # Background jobs
│   └── Core/             # Shared code, entities, services
├── frontend/
│   └── admin-dashboard/  # Angular app
└── widget/               # JavaScript widget
```

## Need Help?

- Check existing issues and discussions
- Read the [documentation](docs/)
- Ask questions in GitHub Discussions
- Reach out to maintainers

## Recognition

Contributors will be recognized in:
- GitHub contributors page
- Project README
- Release notes

Thank you for contributing! 🎉

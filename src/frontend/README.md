# Frontend - Angular Admin Dashboard

Modern Angular 17+ admin dashboard for managing chatbots, documents, conversations, and leads.

## Features
- Multi-tenant chatbot management
- Document upload and processing
- Real-time conversation monitoring
- Lead management and CRM integration
- Analytics and usage metrics
- Responsive design (mobile-friendly)

## Getting Started

### Prerequisites
- Node.js 18+
- npm or yarn
- Angular CLI 17+

### Initial Setup

1. **Create Angular project:**
   ```bash
   cd src/frontend/admin-dashboard
   ng new admin-dashboard --routing --style=scss
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Install additional packages:**
   ```bash
   # UI Components
   npm install @angular/material @angular/cdk
   # Or use PrimeNG
   npm install primeng primeicons

   # Tailwind CSS
   npm install -D tailwindcss postcss autoprefixer
   npx tailwindcss init

   # Charts
   npm install chart.js ng2-charts

   # HTTP and Forms
   npm install @angular/common @angular/forms

   # Utilities
   npm install ngx-clipboard
   npm install date-fns
   ```

4. **Configure Tailwind CSS:**
   
   Edit `tailwind.config.js`:
   ```javascript
   module.exports = {
     content: [
       "./src/**/*.{html,ts}",
     ],
     theme: {
       extend: {},
     },
     plugins: [],
   }
   ```

   Add to `src/styles.scss`:
   ```scss
   @tailwind base;
   @tailwind components;
   @tailwind utilities;
   ```

5. **Run development server:**
   ```bash
   ng serve
   ```

   Navigate to `http://localhost:4200`

## Project Structure

```
admin-dashboard/
├── src/
│   ├── app/
│   │   ├── core/
│   │   │   ├── services/
│   │   │   │   ├── auth.service.ts
│   │   │   │   ├── chatbot.service.ts
│   │   │   │   ├── document.service.ts
│   │   │   │   ├── conversation.service.ts
│   │   │   │   └── lead.service.ts
│   │   │   ├── guards/
│   │   │   │   └── auth.guard.ts
│   │   │   └── interceptors/
│   │   │       └── auth.interceptor.ts
│   │   ├── shared/
│   │   │   ├── components/
│   │   │   ├── pipes/
│   │   │   └── models/
│   │   ├── features/
│   │   │   ├── auth/
│   │   │   │   ├── login/
│   │   │   │   └── register/
│   │   │   ├── dashboard/
│   │   │   ├── chatbots/
│   │   │   │   ├── chatbot-list/
│   │   │   │   ├── chatbot-form/
│   │   │   │   └── chatbot-detail/
│   │   │   ├── documents/
│   │   │   │   ├── document-list/
│   │   │   │   └── document-upload/
│   │   │   ├── conversations/
│   │   │   │   ├── conversation-list/
│   │   │   │   └── conversation-detail/
│   │   │   ├── leads/
│   │   │   │   ├── lead-list/
│   │   │   │   └── lead-detail/
│   │   │   └── analytics/
│   │   ├── layout/
│   │   │   ├── header/
│   │   │   ├── sidebar/
│   │   │   └── footer/
│   │   ├── app.component.ts
│   │   ├── app.routes.ts
│   │   └── app.config.ts
│   ├── assets/
│   ├── environments/
│   │   ├── environment.ts
│   │   └── environment.prod.ts
│   └── styles.scss
├── angular.json
├── package.json
└── tsconfig.json
```

## Routes

```typescript
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', canActivate: [AuthGuard], children: [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'chatbots', component: ChatbotListComponent },
    { path: 'chatbots/new', component: ChatbotFormComponent },
    { path: 'chatbots/:id', component: ChatbotDetailComponent },
    { path: 'chatbots/:id/edit', component: ChatbotFormComponent },
    { path: 'documents', component: DocumentListComponent },
    { path: 'conversations', component: ConversationListComponent },
    { path: 'conversations/:id', component: ConversationDetailComponent },
    { path: 'leads', component: LeadListComponent },
    { path: 'leads/:id', component: LeadDetailComponent },
    { path: 'analytics', component: AnalyticsComponent },
  ]},
  { path: '**', redirectTo: '/dashboard' }
];
```

## Environment Configuration

**environment.ts:**
```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001/api',
  widgetUrl: 'http://localhost:5000/widget'
};
```

**environment.prod.ts:**
```typescript
export const environment = {
  production: true,
  apiUrl: 'https://api.yourdomain.com/api',
  widgetUrl: 'https://widget.yourdomain.com'
};
```

## Key Services

### AuthService
```typescript
export class AuthService {
  login(email: string, password: string): Observable<LoginResponse>
  logout(): void
  isAuthenticated(): boolean
  getToken(): string | null
}
```

### ChatbotService
```typescript
export class ChatbotService {
  getChatbots(): Observable<Chatbot[]>
  getChatbot(id: string): Observable<Chatbot>
  createChatbot(chatbot: CreateChatbotDto): Observable<Chatbot>
  updateChatbot(id: string, chatbot: UpdateChatbotDto): Observable<Chatbot>
  deleteChatbot(id: string): Observable<void>
}
```

## Development

### Running Tests
```bash
# Unit tests
ng test

# E2E tests
ng e2e

# Code coverage
ng test --code-coverage
```

### Building for Production
```bash
ng build --configuration production
```

Output will be in `dist/` directory.

### Linting
```bash
ng lint
```

## Styling

The dashboard uses Tailwind CSS for utility-first styling. Common patterns:

```html
<div class="flex items-center justify-between p-4 bg-white rounded-lg shadow">
  <h2 class="text-xl font-bold">Title</h2>
  <button class="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600">
    Action
  </button>
</div>
```

## See Also
- [Phase 8: Admin Dashboard Part 1](../../docs/phases/Phase-08-Admin-Dashboard-Part1.md)
- [Phase 9: Admin Dashboard Part 2](../../docs/phases/Phase-09-Admin-Dashboard-Part2.md)

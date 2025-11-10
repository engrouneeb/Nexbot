using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ChatbotPlatform.Core.Entities;
using ChatbotPlatform.Core.Enums;

namespace ChatbotPlatform.Core.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();

                await context.Database.MigrateAsync();
                logger.LogInformation("✅ Database migrations applied successfully");

                if (await context.Clients.AnyAsync())
                {
                    logger.LogInformation("ℹ️ Database already seeded - skipping");
                    return;
                }

                logger.LogInformation("🌱 Starting database seeding...");

                var testClient = new Client
                {
                    CompanyName = "Acme Corporation",
                    Email = "admin@acme.com",
                    ContactPerson = "John Doe",
                    Phone = "+1 (555) 123-4567",
                    Address = "123 Tech Street, Silicon Valley, CA 94025",
                    IsActive = true,
                    MaxChatbots = 10,
                    MaxDocumentsPerChatbot = 200,
                    MonthlyTokenLimit = 5000000
                };

                await context.Clients.AddAsync(testClient);
                await context.SaveChangesAsync();
                logger.LogInformation($"✅ Created client: {testClient.CompanyName}");

                var testUser = new User
                {
                    ClientId = testClient.Id,
                    UserName = "admin@acme.com",
                    Email = "admin@acme.com",
                    EmailConfirmed = true,
                    FirstName = "John",
                    LastName = "Doe",
                    IsActive = true,
                    PhoneNumber = "+1 (555) 123-4567",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(testUser, "Admin@123456");
                
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create test user: {errors}");
                }

                logger.LogInformation($"✅ Created user: {testUser.Email}");

                var testChatbot = new Chatbot
                {
                    ClientId = testClient.Id,
                    Name = "Customer Support Bot",
                    Description = "AI-powered customer support assistant",
                    Status = ChatbotStatus.Draft,
                    OpenAIApiKeyEncrypted = "TEMP_PLACEHOLDER",
                    Model = "gpt-4o-mini",
                    SystemPrompt = "You are a helpful customer support assistant.",
                    MaxTokens = 1000,
                    Temperature = 0.7,
                    TopK = 5,
                    PrimaryColor = "#007bff",
                    BotName = "Acme Support Bot",
                    WelcomeMessage = "Hello! 👋 How can I help you today?",
                    EnableLeadCapture = true,
                    RateLimitPerMinute = 100,
                    ChromaCollectionName = $"chatbot_{Guid.NewGuid():N}"
                };

                await context.Chatbots.AddAsync(testChatbot);
                await context.SaveChangesAsync();
                logger.LogInformation($"✅ Created chatbot: {testChatbot.Name}");

                var documents = new[]
                {
                    new Document
                    {
                        ChatbotId = testChatbot.Id,
                        FileName = "product-catalog.pdf",
                        FileType = "pdf",
                        FileSizeBytes = 1024 * 500,
                        Status = DocumentStatus.Pending
                    },
                    new Document
                    {
                        ChatbotId = testChatbot.Id,
                        FileName = "faq-document.docx",
                        FileType = "docx",
                        FileSizeBytes = 1024 * 200,
                        Status = DocumentStatus.Pending
                    }
                };

                await context.Documents.AddRangeAsync(documents);
                await context.SaveChangesAsync();
                logger.LogInformation($"✅ Created {documents.Length} sample documents");

                logger.LogInformation("🎉 Database seeding completed successfully!");
                logger.LogInformation($"   Email: {testUser.Email}");
                logger.LogInformation($"   Password: Admin@123456");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ An error occurred seeding the database");
                throw;
            }
        }
    }
}
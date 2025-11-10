using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChatbotPlatform.Core.Entities;

namespace ChatbotPlatform.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Microsoft.AspNetCore.Identity.IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Chatbot> Chatbots { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<Conversation> Conversations { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Lead> Leads { get; set; } = null!;
        public DbSet<UsageMetric> UsageMetrics { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CLIENT CONFIGURATION
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.ContactPerson).IsRequired().HasMaxLength(200);

                entity.HasMany(e => e.Users)
                    .WithOne(e => e.Client)
                    .HasForeignKey(e => e.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Chatbots)
                    .WithOne(e => e.Client)
                    .HasForeignKey(e => e.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // USER CONFIGURATION
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // CHATBOT CONFIGURATION
            modelBuilder.Entity<Chatbot>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.OpenAIApiKeyEncrypted).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ChromaCollectionName).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.ChromaCollectionName).IsUnique();

                entity.HasMany(e => e.Documents)
                    .WithOne(e => e.Chatbot)
                    .HasForeignKey(e => e.ChatbotId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Conversations)
                    .WithOne(e => e.Chatbot)
                    .HasForeignKey(e => e.ChatbotId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // DOCUMENT CONFIGURATION
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FileType).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => new { e.ChatbotId, e.Status });
            });

            // CONVERSATION CONFIGURATION
            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SessionId).IsRequired().HasMaxLength(100);
                entity.Property(e => e.EstimatedCost).HasPrecision(18, 6);
                entity.HasIndex(e => e.SessionId);

                entity.HasMany(e => e.Messages)
                    .WithOne(e => e.Conversation)
                    .HasForeignKey(e => e.ConversationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Lead)
                    .WithOne(e => e.Conversation)
                    .HasForeignKey<Conversation>(e => e.LeadId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            // MESSAGE CONFIGURATION
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Cost).HasPrecision(18, 6);
                entity.HasIndex(e => new { e.ConversationId, e.CreatedAt });
            });

            // LEAD CONFIGURATION
            modelBuilder.Entity<Lead>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Email);
                entity.HasIndex(e => new { e.ClientId, e.Status });

                entity.HasOne(e => e.Chatbot)
                    .WithMany()
                    .HasForeignKey(e => e.ChatbotId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Client)
                    .WithMany()
                    .HasForeignKey(e => e.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // USAGE METRIC CONFIGURATION
            modelBuilder.Entity<UsageMetric>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.TotalCost).HasPrecision(18, 6);
                entity.HasIndex(e => new { e.ClientId, e.ChatbotId, e.Date }).IsUnique();
            });

            // SOFT DELETE FILTERS
            modelBuilder.Entity<Client>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Chatbot>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Document>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Conversation>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Message>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Lead>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<UsageMetric>().HasQueryFilter(e => !e.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
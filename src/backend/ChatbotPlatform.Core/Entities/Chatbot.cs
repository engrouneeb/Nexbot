using System;
using System.Collections.Generic;
using ChatbotPlatform.Core.Enums;

namespace ChatbotPlatform.Core.Entities
{
    public class Chatbot : BaseEntity
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ChatbotStatus Status { get; set; } = ChatbotStatus.Draft;
        
        public string OpenAIApiKeyEncrypted { get; set; } = string.Empty;
        public string Model { get; set; } = "gpt-4o-mini";
        public string SystemPrompt { get; set; } = "You are a helpful AI assistant.";
        public int MaxTokens { get; set; } = 1000;
        public double Temperature { get; set; } = 0.7;
        public int TopK { get; set; } = 5;
        
        public string PrimaryColor { get; set; } = "#007bff";
        public string BotName { get; set; } = "Assistant";
        public string? AvatarUrl { get; set; }
        public string WelcomeMessage { get; set; } = "Hello! How can I help you today?";
        
        public bool EnableLeadCapture { get; set; } = true;
        public int RateLimitPerMinute { get; set; } = 100;
        public string ChromaCollectionName { get; set; } = string.Empty;
        
        public Client Client { get; set; } = null!;
        public ICollection<Document> Documents { get; set; } = new List<Document>();
        public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
    }
}
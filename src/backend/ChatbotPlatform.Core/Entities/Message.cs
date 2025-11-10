using System;
using ChatbotPlatform.Core.Enums;

namespace ChatbotPlatform.Core.Entities
{
    public class Message : BaseEntity
    {
        public Guid ConversationId { get; set; }
        public MessageRole Role { get; set; }
        public string Content { get; set; } = string.Empty;
        
        public int InputTokens { get; set; } = 0;
        public int OutputTokens { get; set; } = 0;
        public decimal Cost { get; set; } = 0;
        
        public string? RetrievedContext { get; set; }
        public string? SourceDocuments { get; set; }
        
        public Conversation Conversation { get; set; } = null!;
    }
}
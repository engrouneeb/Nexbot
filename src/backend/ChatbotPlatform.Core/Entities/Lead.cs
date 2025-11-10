using System;
using ChatbotPlatform.Core.Enums;

namespace ChatbotPlatform.Core.Entities
{
    public class Lead : BaseEntity
    {
        public Guid ConversationId { get; set; }
        public Guid ChatbotId { get; set; }
        public Guid ClientId { get; set; }
        
        public string Email { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Company { get; set; }
        public string? Message { get; set; }
        
        public LeadStatus Status { get; set; } = LeadStatus.New;
        public int QualityScore { get; set; } = 0;
        public string? AssignedToUserId { get; set; }
        public string? Notes { get; set; }
        
        public string? ConversationSummary { get; set; }
        public string? DetectedIntent { get; set; }
        
        public Conversation Conversation { get; set; } = null!;
        public Chatbot Chatbot { get; set; } = null!;
        public Client Client { get; set; } = null!;
    }
}
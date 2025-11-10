using System;
using System.Collections.Generic;

namespace ChatbotPlatform.Core.Entities
{
    public class Conversation : BaseEntity
    {
        public Guid ChatbotId { get; set; }
        public string SessionId { get; set; } = string.Empty;
        
        public string? VisitorIp { get; set; }
        public string? UserAgent { get; set; }
        public string? ReferrerUrl { get; set; }
        public string? VisitorCountry { get; set; }
        
        public int MessageCount { get; set; } = 0;
        public int TotalInputTokens { get; set; } = 0;
        public int TotalOutputTokens { get; set; } = 0;
        public decimal EstimatedCost { get; set; } = 0;
        public DateTime? EndedAt { get; set; }
        public TimeSpan? Duration { get; set; }
        
        public bool LeadCaptured { get; set; } = false;
        public Guid? LeadId { get; set; }
        
        public Chatbot Chatbot { get; set; } = null!;
        public Lead? Lead { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
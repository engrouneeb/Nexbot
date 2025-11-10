using System;

namespace ChatbotPlatform.Core.Entities
{
    public class UsageMetric : BaseEntity
    {
        public Guid ClientId { get; set; }
        public Guid? ChatbotId { get; set; }
        public DateTime Date { get; set; }
        
        public int TotalConversations { get; set; } = 0;
        public int TotalMessages { get; set; } = 0;
        public int InputTokens { get; set; } = 0;
        public int OutputTokens { get; set; } = 0;
        public decimal TotalCost { get; set; } = 0;
        
        public int LeadsCaptured { get; set; } = 0;
        public int LeadsConverted { get; set; } = 0;
        
        public Client Client { get; set; } = null!;
        public Chatbot? Chatbot { get; set; }
    }
}
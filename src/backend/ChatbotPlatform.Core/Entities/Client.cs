using System;
using System.Collections.Generic;

namespace ChatbotPlatform.Core.Entities
{
    public class Client : BaseEntity
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        
        public int MaxChatbots { get; set; } = 5;
        public int MaxDocumentsPerChatbot { get; set; } = 100;
        public int MonthlyTokenLimit { get; set; } = 1000000;
        
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Chatbot> Chatbots { get; set; } = new List<Chatbot>();
        public ICollection<UsageMetric> UsageMetrics { get; set; } = new List<UsageMetric>();
    }
}
using System;
using ChatbotPlatform.Core.Enums;

namespace ChatbotPlatform.Core.Entities
{
    public class Document : BaseEntity
    {
        public Guid ChatbotId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public string? BlobUrl { get; set; }
        public string? SourceUrl { get; set; }
        public DocumentStatus Status { get; set; } = DocumentStatus.Pending;
        
        public int ChunkCount { get; set; } = 0;
        public int TotalTokens { get; set; } = 0;
        public DateTime? ProcessedAt { get; set; }
        public string? ErrorMessage { get; set; }
        
        public Chatbot Chatbot { get; set; } = null!;
    }
}
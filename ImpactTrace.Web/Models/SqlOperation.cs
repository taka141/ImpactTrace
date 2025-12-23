using System;
using System.ComponentModel.DataAnnotations;

namespace ImpactTrace.Web.Models
{
    public class SqlOperation
    {
        public int Id { get; set; }

        public int RecordingId { get; set; }

        public Recording Recording { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string TableName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string OperationType { get; set; } = string.Empty; // INSERT, UPDATE, DELETE

        [Required]
        public string SqlText { get; set; } = string.Empty;

        public DateTime ExecutedAt { get; set; }
    }
}

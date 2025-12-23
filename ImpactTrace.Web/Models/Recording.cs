using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImpactTrace.Web.Models
{
    public class Recording
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsRecording { get; set; }

        public List<SqlOperation> Operations { get; set; } = new();
    }
}

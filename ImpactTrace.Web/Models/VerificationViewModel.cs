using System;
using System.Collections.Generic;
using System.Linq;

namespace ImpactTrace.Web.Models
{
    public class VerificationViewModel
    {
        public List<RecordingDetailViewModel> Recordings { get; set; } = new();
        public string? FilterOperationName { get; set; }
        public string? FilterTableName { get; set; }
        public DateTime? FilterStartTime { get; set; }
        public DateTime? FilterEndTime { get; set; }
    }

    public class RecordingDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int OperationCount { get; set; }
        public List<SqlOperation> Operations { get; set; } = new();
    }
}

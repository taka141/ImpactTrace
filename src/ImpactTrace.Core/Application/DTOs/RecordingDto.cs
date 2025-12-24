namespace ImpactTrace.Core.Application.DTOs;

public record RecordingDto(
    int Id,
    string Name,
    DateTime StartTime,
    DateTime? EndTime,
    string Status,
    int OperationCount
);

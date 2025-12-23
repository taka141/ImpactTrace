namespace ImpactTrace.Core.Application.DTOs;

public record RecordingDetailDto(
    int Id,
    string Name,
    DateTime StartTime,
    DateTime? EndTime,
    string Status,
    List<SqlOperationDto> Operations
);

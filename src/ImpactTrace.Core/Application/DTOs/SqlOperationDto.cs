namespace ImpactTrace.Core.Application.DTOs;

public record SqlOperationDto(
    int Id,
    int RecordingId,
    string TableName,
    string OperationType,
    string SqlText,
    DateTime ExecutedAt
);

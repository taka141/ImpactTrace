using ImpactTrace.Core.Domain.ValueObjects;

namespace ImpactTrace.Core.Domain.Entities;

/// <summary>
/// SQL Operation entity
/// </summary>
public class SqlOperation : Entity
{
    public int RecordingId { get; private set; }
    public TableName TableName { get; private set; } = null!;
    public OperationType OperationType { get; private set; }
    public SqlText SqlText { get; private set; } = null!;
    public DateTime ExecutedAt { get; private set; }

    private SqlOperation() { } // For EF Core

    public static SqlOperation Create(
        int recordingId,
        TableName tableName,
        OperationType operationType,
        SqlText sqlText)
    {
        return new SqlOperation
        {
            RecordingId = recordingId,
            TableName = tableName,
            OperationType = operationType,
            SqlText = sqlText,
            ExecutedAt = DateTime.Now
        };
    }
}

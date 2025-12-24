using ImpactTrace.Core.Domain.ValueObjects;

namespace ImpactTrace.Core.Domain.Entities;

/// <summary>
/// Recording aggregate root
/// </summary>
public class Recording : Entity
{
    private readonly List<SqlOperation> _operations = new();

    public RecordingName Name { get; private set; } = null!;
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public RecordingStatus Status { get; private set; }

    public IReadOnlyCollection<SqlOperation> Operations => _operations.AsReadOnly();

    private Recording() { } // For EF Core

    public static Recording Create(RecordingName name)
    {
        return new Recording
        {
            Name = name,
            StartTime = DateTime.Now,
            Status = RecordingStatus.Active
        };
    }

    public void Stop()
    {
        if (Status != RecordingStatus.Active)
            throw new InvalidOperationException("Cannot stop a recording that is not active.");

        EndTime = DateTime.Now;
        Status = RecordingStatus.Completed;
    }

    public void AddOperation(SqlOperation operation)
    {
        if (Status != RecordingStatus.Active)
            throw new InvalidOperationException("Cannot add operations to an inactive recording.");

        _operations.Add(operation);
    }
}

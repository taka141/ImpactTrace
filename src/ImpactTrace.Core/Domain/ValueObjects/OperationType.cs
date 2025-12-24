namespace ImpactTrace.Core.Domain.ValueObjects;

public enum OperationType
{
    Insert,
    Update,
    Delete
}

public static class OperationTypeExtensions
{
    public static string ToSqlKeyword(this OperationType type) => type switch
    {
        OperationType.Insert => "INSERT",
        OperationType.Update => "UPDATE",
        OperationType.Delete => "DELETE",
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };

    public static OperationType FromSqlKeyword(string keyword) => keyword.ToUpperInvariant() switch
    {
        "INSERT" => OperationType.Insert,
        "UPDATE" => OperationType.Update,
        "DELETE" => OperationType.Delete,
        _ => throw new ArgumentException($"Unknown operation type: {keyword}")
    };
}

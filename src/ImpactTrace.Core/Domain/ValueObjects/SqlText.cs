namespace ImpactTrace.Core.Domain.ValueObjects;

/// <summary>
/// Value object for SQL text
/// </summary>
public record SqlText
{
    public string Value { get; }

    public SqlText(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("SQL text cannot be empty.", nameof(value));

        Value = value;
    }

    public static implicit operator string(SqlText text) => text.Value;
    public static explicit operator SqlText(string value) => new(value);
}

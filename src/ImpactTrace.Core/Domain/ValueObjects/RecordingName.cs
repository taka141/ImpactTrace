namespace ImpactTrace.Core.Domain.ValueObjects;

/// <summary>
/// Value object for recording name
/// </summary>
public record RecordingName
{
    public string Value { get; }

    public RecordingName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Recording name cannot be empty.", nameof(value));

        if (value.Length > 200)
            throw new ArgumentException("Recording name cannot exceed 200 characters.", nameof(value));

        Value = value.Trim();
    }

    public static implicit operator string(RecordingName name) => name.Value;
    public static explicit operator RecordingName(string value) => new(value);
}

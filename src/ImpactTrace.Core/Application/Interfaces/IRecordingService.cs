using ImpactTrace.Core.Application.DTOs;

namespace ImpactTrace.Core.Application.Interfaces;

/// <summary>
/// Recording service interface - Application Service
/// </summary>
public interface IRecordingService
{
    Task<RecordingDto> StartRecordingAsync(string name);
    Task<RecordingDto> StopRecordingAsync();
    Task<RecordingDto?> GetActiveRecordingAsync();
    Task<IReadOnlyList<RecordingDto>> GetAllRecordingsAsync();
    Task<RecordingDetailDto?> GetRecordingDetailAsync(int id);
    Task CaptureTestSqlAsync();
}

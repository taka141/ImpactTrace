using ImpactTrace.Core.Application.DTOs;

namespace ImpactTrace.Core.Application.Interfaces;

/// <summary>
/// Export service interface
/// </summary>
public interface IExportService
{
    Task<byte[]> ExportToExcelAsync(int recordingId);
    Task<byte[]> ExportToCsvAsync(int recordingId);
}

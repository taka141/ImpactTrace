using ImpactTrace.Core.Domain.Entities;

namespace ImpactTrace.Core.Domain.Repositories;

/// <summary>
/// Repository interface for Recording aggregate
/// </summary>
public interface IRecordingRepository
{
    Task<Recording?> GetByIdAsync(int id);
    Task<IReadOnlyList<Recording>> GetAllAsync();
    Task<Recording?> GetActiveRecordingAsync();
    Task AddAsync(Recording recording);
    Task UpdateAsync(Recording recording);
    Task<int> SaveChangesAsync();
}

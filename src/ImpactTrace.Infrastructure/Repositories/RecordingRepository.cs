using ImpactTrace.Core.Domain.Entities;
using ImpactTrace.Core.Domain.Repositories;
using ImpactTrace.Core.Domain.ValueObjects;
using ImpactTrace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ImpactTrace.Infrastructure.Repositories;

public class RecordingRepository : IRecordingRepository
{
    private readonly ApplicationDbContext _context;

    public RecordingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Recording?> GetByIdAsync(int id)
    {
        return await _context.Recordings
            .Include("_operations")
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IReadOnlyList<Recording>> GetAllAsync()
    {
        return await _context.Recordings
            .Include("_operations")
            .OrderByDescending(r => r.StartTime)
            .ToListAsync();
    }

    public async Task<Recording?> GetActiveRecordingAsync()
    {
        return await _context.Recordings
            .Include("_operations")
            .FirstOrDefaultAsync(r => r.Status == RecordingStatus.Active);
    }

    public async Task AddAsync(Recording recording)
    {
        await _context.Recordings.AddAsync(recording);
    }

    public async Task UpdateAsync(Recording recording)
    {
        _context.Recordings.Update(recording);
        await Task.CompletedTask;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

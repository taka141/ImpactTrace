using ImpactTrace.Core.Application.DTOs;
using ImpactTrace.Core.Application.Interfaces;
using ImpactTrace.Core.Domain.Entities;
using ImpactTrace.Core.Domain.Repositories;
using ImpactTrace.Core.Domain.ValueObjects;

namespace ImpactTrace.Infrastructure.Services;

public class RecordingService : IRecordingService
{
    private readonly IRecordingRepository _repository;
    private readonly ISqlInterceptorService _sqlInterceptor;

    public RecordingService(IRecordingRepository repository, ISqlInterceptorService sqlInterceptor)
    {
        _repository = repository;
        _sqlInterceptor = sqlInterceptor;
    }

    public async Task<RecordingDto> StartRecordingAsync(string name)
    {
        var activeRecording = await _repository.GetActiveRecordingAsync();
        if (activeRecording != null)
            throw new InvalidOperationException("Cannot start a new recording while another is active.");

        var recording = Recording.Create(new RecordingName(name));
        await _repository.AddAsync(recording);
        await _repository.SaveChangesAsync();

        return MapToDto(recording);
    }

    public async Task<RecordingDto> StopRecordingAsync()
    {
        var recording = await _repository.GetActiveRecordingAsync();
        if (recording == null)
            throw new InvalidOperationException("No active recording to stop.");

        recording.Stop();
        await _repository.UpdateAsync(recording);
        await _repository.SaveChangesAsync();

        return MapToDto(recording);
    }

    public async Task<RecordingDto?> GetActiveRecordingAsync()
    {
        var recording = await _repository.GetActiveRecordingAsync();
        return recording != null ? MapToDto(recording) : null;
    }

    public async Task<IReadOnlyList<RecordingDto>> GetAllRecordingsAsync()
    {
        var recordings = await _repository.GetAllAsync();
        return recordings.Select(MapToDto).ToList();
    }

    public async Task<RecordingDetailDto?> GetRecordingDetailAsync(int id)
    {
        var recording = await _repository.GetByIdAsync(id);
        if (recording == null)
            return null;

        return new RecordingDetailDto(
            recording.Id,
            recording.Name,
            recording.StartTime,
            recording.EndTime,
            recording.Status.ToString(),
            recording.Operations.Select(op => new SqlOperationDto(
                op.Id,
                op.RecordingId,
                op.TableName,
                op.OperationType.ToSqlKeyword(),
                op.SqlText,
                op.ExecutedAt
            )).ToList()
        );
    }

    public async Task CaptureTestSqlAsync()
    {
        var testSqls = new[]
        {
            "INSERT INTO Users (Name, Email) VALUES ('Test User', 'test@example.com')",
            "UPDATE Products SET Price = 100 WHERE Id = 1",
            "DELETE FROM Orders WHERE Status = 'Cancelled'",
            "INSERT INTO Customers (FirstName, LastName) VALUES ('John', 'Doe')",
            "UPDATE Inventory SET Quantity = Quantity - 1 WHERE ProductId = 5"
        };

        foreach (var sql in testSqls)
        {
            await _sqlInterceptor.InterceptSqlAsync(sql);
            await Task.Delay(100);
        }
    }

    private static RecordingDto MapToDto(Recording recording)
    {
        return new RecordingDto(
            recording.Id,
            recording.Name,
            recording.StartTime,
            recording.EndTime,
            recording.Status.ToString(),
            recording.Operations.Count
        );
    }
}

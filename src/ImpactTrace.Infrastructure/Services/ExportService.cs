using System.IO;
using ClosedXML.Excel;
using ImpactTrace.Core.Application.Interfaces;
using ImpactTrace.Core.Domain.Repositories;

namespace ImpactTrace.Infrastructure.Services;

public class ExportService : IExportService
{
    private readonly IRecordingRepository _repository;

    public ExportService(IRecordingRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> ExportToExcelAsync(int recordingId)
    {
        var recording = await _repository.GetByIdAsync(recordingId);
        if (recording == null)
            throw new InvalidOperationException($"Recording with ID {recordingId} not found.");

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("SQL Operations");

        // Headers
        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Table Name";
        worksheet.Cell(1, 3).Value = "Operation Type";
        worksheet.Cell(1, 4).Value = "SQL Text";
        worksheet.Cell(1, 5).Value = "Executed At";

        var headerRange = worksheet.Range(1, 1, 1, 5);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

        // Data
        int row = 2;
        foreach (var op in recording.Operations)
        {
            worksheet.Cell(row, 1).Value = op.Id;
            worksheet.Cell(row, 2).Value = op.TableName.Value;
            worksheet.Cell(row, 3).Value = op.OperationType.ToSqlKeyword();
            worksheet.Cell(row, 4).Value = op.SqlText.Value;
            worksheet.Cell(row, 5).Value = op.ExecutedAt.ToString("yyyy-MM-dd HH:mm:ss");
            row++;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<byte[]> ExportToCsvAsync(int recordingId)
    {
        var recording = await _repository.GetByIdAsync(recordingId);
        if (recording == null)
            throw new InvalidOperationException($"Recording with ID {recordingId} not found.");

        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);

        // Header
        await writer.WriteLineAsync("ID,Table Name,Operation Type,SQL Text,Executed At");

        // Data
        foreach (var op in recording.Operations)
        {
            var sqlText = op.SqlText.Value.Replace("\"", "\"\"");
            await writer.WriteLineAsync(
                $"{op.Id},\"{op.TableName.Value}\",\"{op.OperationType.ToSqlKeyword()}\",\"{sqlText}\",\"{op.ExecutedAt:yyyy-MM-dd HH:mm:ss}\"");
        }

        await writer.FlushAsync();
        return stream.ToArray();
    }
}

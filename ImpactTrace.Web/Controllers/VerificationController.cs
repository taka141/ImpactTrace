using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ImpactTrace.Web.Data;
using ImpactTrace.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImpactTrace.Web.Controllers
{
    public class VerificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VerificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? filterOperationName, string? filterTableName, 
            DateTime? filterStartTime, DateTime? filterEndTime)
        {
            var query = _context.Recordings
                .Include(r => r.Operations)
                .AsQueryable();

            var recordings = await query
                .OrderByDescending(r => r.StartTime)
                .ToListAsync();

            var viewModel = new VerificationViewModel
            {
                FilterOperationName = filterOperationName,
                FilterTableName = filterTableName,
                FilterStartTime = filterStartTime,
                FilterEndTime = filterEndTime
            };

            foreach (var recording in recordings)
            {
                var operations = recording.Operations.AsQueryable();

                // Apply filters
                if (!string.IsNullOrWhiteSpace(filterOperationName))
                {
                    operations = operations.Where(o => o.OperationType.Contains(filterOperationName, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(filterTableName))
                {
                    operations = operations.Where(o => o.TableName.Contains(filterTableName, StringComparison.OrdinalIgnoreCase));
                }

                if (filterStartTime.HasValue)
                {
                    operations = operations.Where(o => o.ExecutedAt >= filterStartTime.Value);
                }

                if (filterEndTime.HasValue)
                {
                    operations = operations.Where(o => o.ExecutedAt <= filterEndTime.Value);
                }

                var filteredOps = operations.ToList();

                viewModel.Recordings.Add(new RecordingDetailViewModel
                {
                    Id = recording.Id,
                    Name = recording.Name,
                    StartTime = recording.StartTime,
                    EndTime = recording.EndTime,
                    OperationCount = filteredOps.Count,
                    Operations = filteredOps
                });
            }

            // Only show recordings that have operations after filtering
            viewModel.Recordings = viewModel.Recordings.Where(r => r.OperationCount > 0).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> ExportToExcel(int recordingId)
        {
            var recording = await _context.Recordings
                .Include(r => r.Operations)
                .FirstOrDefaultAsync(r => r.Id == recordingId);

            if (recording == null)
            {
                return NotFound();
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("SQL Operations");

            // Add headers
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Table Name";
            worksheet.Cell(1, 3).Value = "Operation Type";
            worksheet.Cell(1, 4).Value = "SQL Text";
            worksheet.Cell(1, 5).Value = "Executed At";

            // Style headers
            var headerRange = worksheet.Range(1, 1, 1, 5);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Add data
            int row = 2;
            foreach (var op in recording.Operations)
            {
                worksheet.Cell(row, 1).Value = op.Id;
                worksheet.Cell(row, 2).Value = op.TableName;
                worksheet.Cell(row, 3).Value = op.OperationType;
                worksheet.Cell(row, 4).Value = op.SqlText;
                worksheet.Cell(row, 5).Value = op.ExecutedAt.ToString("yyyy-MM-dd HH:mm:ss");
                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            // Save to memory stream
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"Recording_{recording.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public async Task<IActionResult> ExportToCsv(int recordingId)
        {
            var recording = await _context.Recordings
                .Include(r => r.Operations)
                .FirstOrDefaultAsync(r => r.Id == recordingId);

            if (recording == null)
            {
                return NotFound();
            }

            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);

            // Write CSV header
            await writer.WriteLineAsync("ID,Table Name,Operation Type,SQL Text,Executed At");

            // Write data rows
            foreach (var op in recording.Operations)
            {
                var sqlText = op.SqlText.Replace("\"", "\"\""); // Escape quotes
                await writer.WriteLineAsync($"{op.Id},\"{op.TableName}\",\"{op.OperationType}\",\"{sqlText}\",\"{op.ExecutedAt:yyyy-MM-dd HH:mm:ss}\"");
            }

            await writer.FlushAsync();
            stream.Position = 0;

            var fileName = $"Recording_{recording.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            return File(stream.ToArray(), "text/csv", fileName);
        }

        public async Task<IActionResult> Details(int id)
        {
            var recording = await _context.Recordings
                .Include(r => r.Operations)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recording == null)
            {
                return NotFound();
            }

            return View(recording);
        }
    }
}

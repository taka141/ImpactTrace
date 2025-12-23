using System;
using System.Linq;
using System.Threading.Tasks;
using ImpactTrace.Web.Data;
using ImpactTrace.Web.Models;
using ImpactTrace.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImpactTrace.Web.Controllers
{
    public class RecordingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISqlInterceptorService _sqlInterceptor;

        public RecordingController(ApplicationDbContext context, ISqlInterceptorService sqlInterceptor)
        {
            _context = context;
            _sqlInterceptor = sqlInterceptor;
        }

        public async Task<IActionResult> Index()
        {
            var recordings = await _context.Recordings
                .Include(r => r.Operations)
                .OrderByDescending(r => r.StartTime)
                .ToListAsync();

            var currentRecordingId = _sqlInterceptor.GetCurrentRecordingId();
            ViewBag.CurrentRecordingId = currentRecordingId;

            return View(recordings);
        }

        [HttpPost]
        public async Task<IActionResult> StartRecording(string recordingName)
        {
            if (string.IsNullOrWhiteSpace(recordingName))
            {
                TempData["Error"] = "Recording name is required.";
                return RedirectToAction(nameof(Index));
            }

            var recording = new Recording
            {
                Name = recordingName.Trim(),
                StartTime = DateTime.Now,
                IsRecording = true
            };

            _context.Recordings.Add(recording);
            await _context.SaveChangesAsync();

            _sqlInterceptor.SetCurrentRecordingId(recording.Id);

            TempData["Success"] = $"Recording '{recording.Name}' started.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> StopRecording()
        {
            var currentRecordingId = _sqlInterceptor.GetCurrentRecordingId();
            
            if (currentRecordingId == null)
            {
                TempData["Error"] = "No active recording to stop.";
                return RedirectToAction(nameof(Index));
            }

            var recording = await _context.Recordings.FindAsync(currentRecordingId.Value);
            if (recording != null)
            {
                recording.EndTime = DateTime.Now;
                recording.IsRecording = false;
                await _context.SaveChangesAsync();

                _sqlInterceptor.SetCurrentRecordingId(null);

                TempData["Success"] = $"Recording '{recording.Name}' stopped.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> TestSql()
        {
            // Simulate SQL operations for testing
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
                _sqlInterceptor.InterceptSql(sql);
                await Task.Delay(100); // Small delay to simulate different execution times
            }

            TempData["Success"] = $"Generated {testSqls.Length} test SQL operations.";
            return RedirectToAction(nameof(Index));
        }
    }
}

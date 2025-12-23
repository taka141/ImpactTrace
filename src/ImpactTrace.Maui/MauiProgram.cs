using ImpactTrace.Core.Application.Interfaces;
using ImpactTrace.Core.Domain.Repositories;
using ImpactTrace.Infrastructure.Data;
using ImpactTrace.Infrastructure.Repositories;
using ImpactTrace.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ImpactTrace.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Add Blazor Hybrid services
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // Database configuration
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "impacttrace.db");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        // Register repositories (Infrastructure layer)
        builder.Services.AddScoped<IRecordingRepository, RecordingRepository>();

        // Register application services (Application layer)
        builder.Services.AddScoped<IRecordingService, RecordingService>();
        builder.Services.AddScoped<ISqlInterceptorService, SqlInterceptorService>();
        builder.Services.AddScoped<IExportService, ExportService>();

        var app = builder.Build();

        // Initialize database
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        }

        return app;
    }
}

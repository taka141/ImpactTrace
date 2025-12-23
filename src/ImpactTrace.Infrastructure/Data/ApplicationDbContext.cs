using ImpactTrace.Core.Domain.Entities;
using ImpactTrace.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImpactTrace.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Recording> Recordings { get; set; } = null!;
    public DbSet<SqlOperation> SqlOperations { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Value converters for value objects
        var recordingNameConverter = new ValueConverter<RecordingName, string>(
            v => v.Value,
            v => new RecordingName(v));

        var tableNameConverter = new ValueConverter<TableName, string>(
            v => v.Value,
            v => new TableName(v));

        var sqlTextConverter = new ValueConverter<SqlText, string>(
            v => v.Value,
            v => new SqlText(v));

        // Recording configuration
        modelBuilder.Entity<Recording>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .HasConversion(recordingNameConverter)
                .HasMaxLength(200)
                .IsRequired();
            entity.Property(e => e.Status)
                .HasConversion<string>();
            entity.HasMany<SqlOperation>("_operations")
                .WithOne()
                .HasForeignKey(e => e.RecordingId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // SqlOperation configuration
        modelBuilder.Entity<SqlOperation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TableName)
                .HasConversion(tableNameConverter)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(e => e.OperationType)
                .HasConversion<string>()
                .IsRequired();
            entity.Property(e => e.SqlText)
                .HasConversion(sqlTextConverter)
                .IsRequired();
        });
    }
}

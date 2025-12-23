using ImpactTrace.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ImpactTrace.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recording> Recordings { get; set; } = null!;
        public DbSet<SqlOperation> SqlOperations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recording>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasMany(e => e.Operations)
                    .WithOne(e => e.Recording)
                    .HasForeignKey(e => e.RecordingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SqlOperation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TableName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.OperationType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.SqlText).IsRequired();
            });
        }
    }
}

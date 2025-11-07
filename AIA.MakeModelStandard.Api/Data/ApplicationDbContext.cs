using Microsoft.EntityFrameworkCore;
using AIA.MakeModelStandard.Api.Models;

namespace AIA.MakeModelStandard.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<AminRecord> AminRecords { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure indexes for better query performance
        modelBuilder.Entity<AminRecord>()
            .HasIndex(a => a.AminNumber)
            .IsUnique();

        modelBuilder.Entity<AminRecord>()
            .HasIndex(a => new { a.Make, a.Model });

        modelBuilder.Entity<AminRecord>()
            .HasIndex(a => a.Year);

        // Seed some sample data for development/testing
        modelBuilder.Entity<AminRecord>().HasData(
            new AminRecord
            {
                Id = 1,
                AminNumber = "AMIN-2024-001",
                Year = 2024,
                Make = "Cessna",
                Model = "172 Skyhawk",
                FaaManufacturerModelNumber = "172, 172N, 172P, 172R, 172S",
                CreatedDate = DateTime.UtcNow
            },
            new AminRecord
            {
                Id = 2,
                AminNumber = "AMIN-2024-002",
                Year = 2024,
                Make = "Piper",
                Model = "PA-28 Cherokee",
                FaaManufacturerModelNumber = "PA-28-140, PA-28-150, PA-28-160, PA-28-180",
                CreatedDate = DateTime.UtcNow
            },
            new AminRecord
            {
                Id = 3,
                AminNumber = "AMIN-2024-003",
                Year = 2024,
                Make = "Beechcraft",
                Model = "Bonanza",
                FaaManufacturerModelNumber = "35, A36, B36TC, F33A, G36",
                CreatedDate = DateTime.UtcNow
            }
        );
    }
}

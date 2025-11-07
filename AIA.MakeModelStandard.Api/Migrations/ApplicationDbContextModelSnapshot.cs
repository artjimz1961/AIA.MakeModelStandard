using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using AIA.MakeModelStandard.Api.Data;

#nullable disable

namespace AIA.MakeModelStandard.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AIA.MakeModelStandard.Api.Models.AminRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AminNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FaaManufacturerModelNumber")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AminNumber")
                        .IsUnique();

                    b.HasIndex("Year");

                    b.HasIndex("Make", "Model");

                    b.ToTable("AminRecords");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AminNumber = "AMIN-2024-001",
                            CreatedDate = new DateTime(2025, 11, 7, 14, 42, 44, 0, DateTimeKind.Utc),
                            FaaManufacturerModelNumber = "172, 172N, 172P, 172R, 172S",
                            Make = "Cessna",
                            Model = "172 Skyhawk",
                            Year = 2024
                        },
                        new
                        {
                            Id = 2,
                            AminNumber = "AMIN-2024-002",
                            CreatedDate = new DateTime(2025, 11, 7, 14, 42, 44, 0, DateTimeKind.Utc),
                            FaaManufacturerModelNumber = "PA-28-140, PA-28-150, PA-28-160, PA-28-180",
                            Make = "Piper",
                            Model = "PA-28 Cherokee",
                            Year = 2024
                        },
                        new
                        {
                            Id = 3,
                            AminNumber = "AMIN-2024-003",
                            CreatedDate = new DateTime(2025, 11, 7, 14, 42, 44, 0, DateTimeKind.Utc),
                            FaaManufacturerModelNumber = "35, A36, B36TC, F33A, G36",
                            Make = "Beechcraft",
                            Model = "Bonanza",
                            Year = 2024
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AIA.MakeModelStandard.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AminRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AminNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FaaManufacturerModelNumber = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AminRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AminRecords_AminNumber",
                table: "AminRecords",
                column: "AminNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AminRecords_Make_Model",
                table: "AminRecords",
                columns: new[] { "Make", "Model" });

            migrationBuilder.CreateIndex(
                name: "IX_AminRecords_Year",
                table: "AminRecords",
                column: "Year");

            migrationBuilder.InsertData(
                table: "AminRecords",
                columns: new[] { "Id", "AminNumber", "CreatedDate", "FaaManufacturerModelNumber", "Make", "Model", "ModifiedDate", "Year" },
                values: new object[,]
                {
                    { 1, "AMIN-2024-001", new DateTime(2025, 11, 7, 14, 42, 44, 0, DateTimeKind.Utc), "172, 172N, 172P, 172R, 172S", "Cessna", "172 Skyhawk", null, 2024 },
                    { 2, "AMIN-2024-002", new DateTime(2025, 11, 7, 14, 42, 44, 0, DateTimeKind.Utc), "PA-28-140, PA-28-150, PA-28-160, PA-28-180", "Piper", "PA-28 Cherokee", null, 2024 },
                    { 3, "AMIN-2024-003", new DateTime(2025, 11, 7, 14, 42, 44, 0, DateTimeKind.Utc), "35, A36, B36TC, F33A, G36", "Beechcraft", "Bonanza", null, 2024 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AminRecords");
        }
    }
}

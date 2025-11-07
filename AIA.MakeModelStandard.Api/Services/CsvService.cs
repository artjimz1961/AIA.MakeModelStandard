using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using AIA.MakeModelStandard.Api.Models;

namespace AIA.MakeModelStandard.Api.Services;

public interface ICsvService
{
    byte[] GenerateCsv<T>(IEnumerable<T> records);
}

public class CsvService : ICsvService
{
    public byte[] GenerateCsv<T>(IEnumerable<T> records)
    {
        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
        using var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        });

        csvWriter.WriteRecords(records);
        streamWriter.Flush();

        return memoryStream.ToArray();
    }
}

/// <summary>
/// DTO for AMIN export (excludes internal fields like Id, CreatedDate, ModifiedDate)
/// </summary>
public class AminExportDto
{
    public string AminNumber { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string? FaaManufacturerModelNumber { get; set; }

    public static AminExportDto FromAminRecord(AminRecord record)
    {
        return new AminExportDto
        {
            AminNumber = record.AminNumber,
            Year = record.Year,
            Make = record.Make,
            Model = record.Model,
            FaaManufacturerModelNumber = record.FaaManufacturerModelNumber
        };
    }
}

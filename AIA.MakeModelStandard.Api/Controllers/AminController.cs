using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AIA.MakeModelStandard.Api.Data;
using AIA.MakeModelStandard.Api.Models;
using AIA.MakeModelStandard.Api.Services;

namespace AIA.MakeModelStandard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AminController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ICsvService _csvService;
    private readonly ILogger<AminController> _logger;

    public AminController(
        ApplicationDbContext context,
        ICsvService csvService,
        ILogger<AminController> logger)
    {
        _context = context;
        _csvService = csvService;
        _logger = logger;
    }

    /// <summary>
    /// Get all AMIN records as JSON
    /// </summary>
    /// <returns>List of AMIN records in JSON format</returns>
    [HttpGet("json")]
    [ProducesResponseType(typeof(IEnumerable<AminExportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AminExportDto>>> GetAminListJson()
    {
        try
        {
            var records = await _context.AminRecords
                .OrderBy(a => a.Year)
                .ThenBy(a => a.Make)
                .ThenBy(a => a.Model)
                .ToListAsync();

            var exportData = records.Select(AminExportDto.FromAminRecord);

            _logger.LogInformation("Retrieved {Count} AMIN records in JSON format", records.Count);

            return Ok(exportData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AMIN records as JSON");
            return StatusCode(500, "An error occurred while retrieving the data");
        }
    }

    /// <summary>
    /// Download all AMIN records as CSV file
    /// </summary>
    /// <returns>CSV file with AMIN records</returns>
    [HttpGet("csv")]
    [Produces("text/csv")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DownloadAminListCsv()
    {
        try
        {
            var records = await _context.AminRecords
                .OrderBy(a => a.Year)
                .ThenBy(a => a.Make)
                .ThenBy(a => a.Model)
                .ToListAsync();

            var exportData = records.Select(AminExportDto.FromAminRecord).ToList();

            var csvBytes = _csvService.GenerateCsv(exportData);

            var fileName = $"AMIN_List_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv";

            _logger.LogInformation("Generated CSV file with {Count} AMIN records", records.Count);

            return File(csvBytes, "text/csv", fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating AMIN CSV file");
            return StatusCode(500, "An error occurred while generating the CSV file");
        }
    }

    /// <summary>
    /// Get AMIN records filtered by year
    /// </summary>
    /// <param name="year">The year to filter by</param>
    /// <returns>List of AMIN records for the specified year</returns>
    [HttpGet("json/year/{year}")]
    [ProducesResponseType(typeof(IEnumerable<AminExportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AminExportDto>>> GetAminListByYear(int year)
    {
        try
        {
            var records = await _context.AminRecords
                .Where(a => a.Year == year)
                .OrderBy(a => a.Make)
                .ThenBy(a => a.Model)
                .ToListAsync();

            var exportData = records.Select(AminExportDto.FromAminRecord);

            _logger.LogInformation("Retrieved {Count} AMIN records for year {Year}", records.Count, year);

            return Ok(exportData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AMIN records for year {Year}", year);
            return StatusCode(500, "An error occurred while retrieving the data");
        }
    }

    /// <summary>
    /// Get AMIN records filtered by make
    /// </summary>
    /// <param name="make">The manufacturer/make to filter by</param>
    /// <returns>List of AMIN records for the specified make</returns>
    [HttpGet("json/make/{make}")]
    [ProducesResponseType(typeof(IEnumerable<AminExportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AminExportDto>>> GetAminListByMake(string make)
    {
        try
        {
            var records = await _context.AminRecords
                .Where(a => a.Make.ToLower().Contains(make.ToLower()))
                .OrderBy(a => a.Year)
                .ThenBy(a => a.Model)
                .ToListAsync();

            var exportData = records.Select(AminExportDto.FromAminRecord);

            _logger.LogInformation("Retrieved {Count} AMIN records for make '{Make}'", records.Count, make);

            return Ok(exportData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AMIN records for make {Make}", make);
            return StatusCode(500, "An error occurred while retrieving the data");
        }
    }

    /// <summary>
    /// Get database statistics
    /// </summary>
    /// <returns>Statistics about the AMIN database</returns>
    [HttpGet("stats")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetStats()
    {
        try
        {
            var totalRecords = await _context.AminRecords.CountAsync();
            var distinctMakes = await _context.AminRecords
                .Select(a => a.Make)
                .Distinct()
                .CountAsync();
            var years = await _context.AminRecords
                .Select(a => a.Year)
                .Distinct()
                .OrderBy(y => y)
                .ToListAsync();

            var stats = new
            {
                TotalRecords = totalRecords,
                DistinctMakes = distinctMakes,
                Years = years,
                YearRange = years.Any() ? $"{years.First()} - {years.Last()}" : "N/A"
            };

            return Ok(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AMIN statistics");
            return StatusCode(500, "An error occurred while retrieving statistics");
        }
    }
}

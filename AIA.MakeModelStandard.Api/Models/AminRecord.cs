using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIA.MakeModelStandard.Api.Models;

/// <summary>
/// Represents an AMIN (Aircraft Manufacturer Index Number) record
/// </summary>
[Table("AminRecords")]
public class AminRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string AminNumber { get; set; } = string.Empty;

    [Required]
    public int Year { get; set; }

    [Required]
    [StringLength(100)]
    public string Make { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// FAA Manufacturer Model Number (can be multiple, comma-separated)
    /// </summary>
    [StringLength(500)]
    public string? FaaManufacturerModelNumber { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? ModifiedDate { get; set; }
}

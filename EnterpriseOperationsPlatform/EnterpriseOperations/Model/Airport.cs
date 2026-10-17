using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseOperations.Models;

[Table("Airports", Schema = "dbo")]
public class Airport
{
    [Key]
    [StringLength(6)]
    [Column(TypeName = "char(6)")]
    public string AirportCode { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    [Column(TypeName = "varchar(30)")]
    public string AirportName { get; set; } = string.Empty;

    [StringLength(30)]
    [Column(TypeName = "varchar(30)")]
    public string? Location { get; set; }

    [StringLength(200)]
    [Column(TypeName = "varchar(200)")]
    public string? Description { get; set; }
}

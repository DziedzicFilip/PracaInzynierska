using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class ObecnoscDto
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    public DateOnly Data { get; set; }
    [Required]
    public string Status { get; set; } = null!;
    public TimeOnly? GodzinaPrzyjscia { get; set; }
    public TimeOnly? GodzinaOdbioru { get; set; }
    public int? RodzicOdbierajacyId { get; set; }
    public int? OsobaUpowaznionaId { get; set; }
    public string? MetodaRejestracji { get; set; }
}

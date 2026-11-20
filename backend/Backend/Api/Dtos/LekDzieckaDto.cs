using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class LekDzieckaDto
{
    public int Id { get; set; }
    public int DzieckoId { get; set; }
    [Required]
    public string NazwaLeku { get; set; } = null!;
    [Required]
    public string Dawka { get; set; } = null!;
    public TimeOnly GodzinaPodania { get; set; }
    public DateOnly? DataOd { get; set; }
    public DateOnly? DataDo { get; set; }
    public string? Uwagi { get; set; }
    public bool ZgodaRodzica { get; set; }
}
